using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;

namespace AlertaEncosta.Core.Servicos;

/// <summary>Resultado de registrar uma leitura: como ela foi avaliada e o que mudou no setor.</summary>
public sealed record ResultadoLeitura(
    LeituraSensor Leitura,
    Setor Setor,
    AvaliacaoRisco Avaliacao,
    NivelRisco RiscoAnterior,
    NivelRisco RiscoAtual,
    bool AlertaRecomendado)
{
    public bool RiscoSubiu => RiscoAtual > RiscoAnterior;
    public bool RiscoDesceu => RiscoAtual < RiscoAnterior;
}

public sealed record ResumoImportacao(int Importadas, int Ignoradas, IReadOnlyList<string> Erros,
    IReadOnlyList<ResultadoLeitura> Resultados)
{
    /// <summary>Setores cujo risco subiu e que ainda não têm alerta compatível.</summary>
    public IReadOnlyList<Setor> SetoresParaAlertar =>
        Resultados.Where(r => r.AlertaRecomendado).Select(r => r.Setor).DistinctBy(s => s.Id).ToList();
}

/// <summary>Situação consolidada de um setor para o painel.</summary>
public sealed record ResumoSetor(
    Setor Setor,
    LeituraSensor? UltimaLeitura,
    AvaliacaoRisco? Avaliacao,
    Tendencia Tendencia,
    Alerta? AlertaAtivo,
    int Moradores,
    int Vulneraveis,
    int PedidosAjuda);

/// <summary>
/// Processo de negócio "Monitorar condições do setor": recebe as leituras, avalia o risco,
/// aplica a histerese (sobe na hora, desce devagar) e diz quando um alerta é recomendado.
/// </summary>
public sealed class ServicoMonitoramento
{
    private readonly ContextoDados _dados;
    private readonly Func<DateTime> _agora;

    public CalculadoraRisco Calculadora { get; }

    public ServicoMonitoramento(ContextoDados dados, CalculadoraRisco calculadora, Func<DateTime>? agora = null)
    {
        _dados = dados;
        Calculadora = calculadora;
        _agora = agora ?? (() => DateTime.Now);
    }

    public ResultadoLeitura Registrar(LeituraSensor leitura, bool salvar = true)
    {
        var setor = _dados.Setores.ObterPorId(leitura.SetorId)
            ?? throw new DominioException($"O setor {leitura.SetorId} não está cadastrado no painel.");

        var duplicada = _dados.Leituras.Listar().Any(l =>
            l.EstacaoId == leitura.EstacaoId && l.SetorId == leitura.SetorId &&
            Math.Abs((l.DataHora - leitura.DataHora).TotalSeconds) < 1);
        if (duplicada)
            throw new DominioException($"Leitura duplicada: a estação {leitura.EstacaoId} já enviou uma leitura em {leitura.DataHora:dd/MM HH:mm:ss}.");

        _dados.Leituras.Adicionar(leitura);
        var avaliacao = Calculadora.Avaliar(leitura);
        var anterior = setor.RiscoAtual;

        // Leituras antigas (importadas fora de ordem) entram no histórico mas não mudam o risco atual.
        var maisRecente = LeiturasDoSetor(setor.Id).First();
        if (ReferenceEquals(maisRecente, leitura))
            setor.AtualizarRisco(AplicarHisterese(setor, avaliacao.Nivel), leitura.DataHora);

        if (salvar)
        {
            _dados.Leituras.Salvar();
            _dados.Setores.Salvar();
        }

        return new ResultadoLeitura(leitura, setor, avaliacao, anterior, setor.RiscoAtual, AlertaRecomendado(setor));
    }

    /// <summary>Registra várias leituras (importação). Erros de uma leitura não impedem as outras.</summary>
    public ResumoImportacao RegistrarVarias(IEnumerable<LeituraSensor> leituras)
    {
        var resultados = new List<ResultadoLeitura>();
        var erros = new List<string>();
        var ignoradas = 0;

        foreach (var leitura in leituras.OrderBy(l => l.DataHora))
        {
            try
            {
                resultados.Add(Registrar(leitura, salvar: false));
            }
            catch (DominioException ex)
            {
                ignoradas++;
                erros.Add(ex.Message);
            }
        }

        _dados.Leituras.Salvar();
        _dados.Setores.Salvar();
        return new ResumoImportacao(resultados.Count, ignoradas, erros, resultados);
    }

    /// <summary>
    /// Histerese: se o risco calculado for maior ou igual ao atual, vale na hora.
    /// Para DESCER, as últimas N leituras precisam estar todas abaixo do risco atual
    /// (evita alerta "pisca-pisca" quando o valor oscila no limiar).
    /// </summary>
    public NivelRisco AplicarHisterese(Setor setor, NivelRisco calculado)
    {
        if (calculado >= setor.RiscoAtual) return calculado;

        var n = Calculadora.Limiares.LeiturasParaBaixar;
        var ultimas = LeiturasDoSetor(setor.Id).Take(n).Select(l => Calculadora.Avaliar(l).Nivel).ToList();
        if (ultimas.Count < n || ultimas.Any(nivel => nivel >= setor.RiscoAtual))
            return setor.RiscoAtual;

        return ultimas.Max();
    }

    /// <summary>Exclui uma leitura (ex.: digitada errada) e recalcula o setor pela leitura mais recente.</summary>
    public void Excluir(string leituraId)
    {
        var leitura = _dados.Leituras.ObterPorId(leituraId)
            ?? throw new DominioException("Leitura não encontrada.");
        _dados.Leituras.Remover(leituraId);

        var setor = _dados.Setores.ObterPorId(leitura.SetorId);
        if (setor is not null)
        {
            var ultima = LeiturasDoSetor(setor.Id).FirstOrDefault();
            setor.AtualizarRisco(ultima is null ? NivelRisco.Baixo : Calculadora.Avaliar(ultima).Nivel, _agora());
        }

        _dados.Leituras.Salvar();
        _dados.Setores.Salvar();
    }

    public bool AlertaRecomendado(Setor setor)
    {
        if (setor.RiscoAtual == NivelRisco.Baixo) return false;
        var ativo = AlertaAtivo(setor.Id);
        return ativo is null || ativo.Nivel < setor.RiscoAtual;
    }

    public Alerta? AlertaAtivo(string setorId) =>
        _dados.Alertas.Listar().FirstOrDefault(a => a.Ativo && a.SetorId == setorId);

    public IReadOnlyList<LeituraSensor> LeiturasDoSetor(string setorId) =>
        _dados.Leituras.Listar()
            .Where(l => string.Equals(l.SetorId, setorId, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(l => l.DataHora)
            .ToList();

    public IReadOnlyList<ResumoSetor> Resumos()
    {
        var moradores = _dados.Moradores.Listar();
        return _dados.Setores.Listar()
            .Select(s =>
            {
                var leituras = LeiturasDoSetor(s.Id);
                var ultima = leituras.FirstOrDefault();
                var doSetor = moradores.Where(m => m.SetorId == s.Id).ToList();
                return new ResumoSetor(s, ultima,
                    ultima is null ? null : Calculadora.Avaliar(ultima),
                    AnalisadorTendencia.Analisar(leituras),
                    AlertaAtivo(s.Id),
                    doSetor.Count,
                    doSetor.Count(m => m.EhVulneravel),
                    doSetor.Count(m => m.Situacao == SituacaoMorador.PrecisaAjuda));
            })
            .OrderByDescending(r => r.Setor.RiscoAtual)
            .ThenBy(r => r.Setor.Id)
            .ToList();
    }
}
