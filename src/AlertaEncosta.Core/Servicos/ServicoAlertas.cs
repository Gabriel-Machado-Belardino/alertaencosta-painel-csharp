using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;

namespace AlertaEncosta.Core.Servicos;

public sealed record ResultadoEmissao(Alerta Alerta, bool FoiEscalado, OpcaoPontoApoio? PontoApoio, int MoradoresAvisados);

/// <summary>
/// Processo de negócio "Emitir alerta de risco". Regras:
///  • só emite se o setor estiver em MÉDIO ou ALTO;
///  • um setor tem no máximo UM alerta ativo: se o risco subir, o alerta é escalado;
///  • o alerta já sai com o ponto de apoio escolhido pelo <see cref="SeletorPontoApoio"/>;
///  • todos os moradores do setor passam para "Alertado".
/// No sistema completo a API envia o push pelo Expo; aqui o painel registra o alerta.
/// </summary>
public sealed class ServicoAlertas
{
    private readonly ContextoDados _dados;
    private readonly SeletorPontoApoio _seletor;
    private readonly Func<DateTime> _agora;

    public ServicoAlertas(ContextoDados dados, SeletorPontoApoio seletor, Func<DateTime>? agora = null)
    {
        _dados = dados;
        _seletor = seletor;
        _agora = agora ?? (() => DateTime.Now);
    }

    public ResultadoEmissao Emitir(string setorId, string operador)
    {
        var setor = _dados.Setores.ObterPorId(setorId)
            ?? throw new DominioException("Selecione um setor válido.");

        if (setor.RiscoAtual == NivelRisco.Baixo)
            throw new DominioException($"O setor {setor.Nome} está com risco BAIXO. O alerta só é emitido em MÉDIO ou ALTO.");

        var ativo = _dados.Alertas.Listar().FirstOrDefault(a => a.Ativo && a.SetorId == setor.Id);
        if (ativo is not null && ativo.Nivel >= setor.RiscoAtual)
            throw new DominioException($"O setor {setor.Nome} já tem o alerta {ativo.Id} ativo em {ativo.Nivel.Rotulo()}.");

        var moradores = _dados.Moradores.Listar().Where(m => m.SetorId == setor.Id).ToList();
        var precisaAcessivel = moradores.Any(m => m.EhVulneravel);
        var opcao = _seletor.Selecionar(setor.Centro, _dados.PontosApoio.Listar(), moradores.Count, precisaAcessivel);
        var mensagem = MontarMensagem(setor, setor.RiscoAtual, opcao);
        var agora = _agora();

        Alerta alerta;
        var escalado = ativo is not null;
        if (ativo is not null)
        {
            ativo.Escalar(setor.RiscoAtual, mensagem, opcao?.Ponto.Id, moradores.Count, agora);
            alerta = ativo;
        }
        else
        {
            alerta = new Alerta(ContextoDados.ProximoCodigo(_dados.Alertas, "ALT"), setor.Id, setor.RiscoAtual,
                mensagem, opcao?.Ponto.Id, moradores.Count, operador, agora);
            _dados.Alertas.Adicionar(alerta);
        }

        foreach (var m in moradores) m.ReceberAlerta(alerta.Id, agora);

        _dados.Alertas.Salvar();
        _dados.Moradores.Salvar();
        return new ResultadoEmissao(alerta, escalado, opcao, moradores.Count);
    }

    /// <summary>Texto curto e direto (linguagem simples) que vai para o celular do morador.</summary>
    public static string MontarMensagem(Setor setor, NivelRisco nivel, OpcaoPontoApoio? opcao)
    {
        var destino = opcao is null
            ? "Procure um local alto e seguro e aguarde a Defesa Civil."
            : $"Ponto de apoio: {opcao.Ponto.Nome}, {opcao.Ponto.Endereco} ({opcao.DistanciaKm:0.0} km).";

        return nivel == NivelRisco.Alto
            ? $"ALERTA ALTO - {setor.Nome}: risco de deslizamento/alagamento. SAIA AGORA. {destino} Precisa de ajuda para sair? Responda AJUDA no app ou ligue 199."
            : $"ATENÇÃO - {setor.Nome}: chuva forte e risco aumentando. Prepare documentos e remédios e fique pronto para sair. {destino}";
    }

    /// <summary>Aviso exibido antes de encerrar (null = pode encerrar sem perguntar).</summary>
    public string? AvisoAntesDeEncerrar(string alertaId)
    {
        var alerta = ObterAtivo(alertaId);
        var setor = _dados.Setores.ObterPorId(alerta.SetorId);
        var pendentes = _dados.Moradores.Listar().Count(m => m.AlertaId == alerta.Id && m.Situacao == SituacaoMorador.PrecisaAjuda);

        var avisos = new List<string>();
        if (setor is not null && setor.RiscoAtual > NivelRisco.Baixo)
            avisos.Add($"o setor ainda está em risco {setor.RiscoAtual.Rotulo()}");
        if (pendentes > 0)
            avisos.Add($"{pendentes} morador(es) pediram ajuda e não foram marcados como seguros");
        return avisos.Count == 0 ? null : "Atenção: " + string.Join(" e ", avisos) + ".";
    }

    public void Encerrar(string alertaId)
    {
        var alerta = ObterAtivo(alertaId);
        var agora = _agora();
        alerta.Encerrar(agora);
        foreach (var m in _dados.Moradores.Listar().Where(m => m.AlertaId == alerta.Id))
            m.EncerrarAlerta(agora);

        _dados.Alertas.Salvar();
        _dados.Moradores.Salvar();
    }

    public void MarcarLocalSeguro(string moradorId)
    {
        var m = _dados.Moradores.ObterPorId(moradorId) ?? throw new DominioException("Morador não encontrado.");
        m.ConfirmarLocalSeguro(_agora());
        _dados.Moradores.Salvar();
    }

    public void MarcarPedidoAjuda(string moradorId)
    {
        var m = _dados.Moradores.ObterPorId(moradorId) ?? throw new DominioException("Morador não encontrado.");
        m.PedirAjuda(_agora());
        _dados.Moradores.Salvar();
    }

    /// <summary>
    /// Modo demonstração: simula as respostas que chegariam do app do morador.
    /// Pessoas vulneráveis têm mais chance de pedir ajuda.
    /// </summary>
    public (int Seguros, int PedidosAjuda, int SemResposta) SimularRespostas(string alertaId, Random aleatorio)
    {
        var alerta = ObterAtivo(alertaId);
        int seguros = 0, ajuda = 0, sem = 0;
        var agora = _agora();

        foreach (var m in _dados.Moradores.Listar().Where(m => m.AlertaId == alerta.Id && m.Situacao == SituacaoMorador.Alertado))
        {
            var sorteio = aleatorio.NextDouble();
            var chanceAjuda = m.EhVulneravel ? 0.35 : 0.08;
            var chanceSeguro = m.EhVulneravel ? 0.45 : 0.75;

            if (sorteio < chanceAjuda) { m.PedirAjuda(agora); ajuda++; }
            else if (sorteio < chanceAjuda + chanceSeguro) { m.ConfirmarLocalSeguro(agora); seguros++; }
            else sem++;
        }

        _dados.Moradores.Salvar();
        return (seguros, ajuda, sem);
    }

    private Alerta ObterAtivo(string alertaId)
    {
        var alerta = _dados.Alertas.ObterPorId(alertaId) ?? throw new DominioException("Alerta não encontrado.");
        if (!alerta.Ativo) throw new DominioException($"O alerta {alerta.Id} já foi encerrado.");
        return alerta;
    }
}
