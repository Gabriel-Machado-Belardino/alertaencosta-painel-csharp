using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Integracao;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Painel;

/// <summary>
/// Composição da aplicação: cria os serviços uma única vez e avisa as telas quando os dados mudam
/// (padrão Observer via evento). As telas nunca mexem nos repositórios direto: passam pelos serviços.
/// </summary>
public sealed class PainelContexto
{
    public ContextoDados Dados { get; }
    public ServicoMonitoramento Monitoramento { get; }
    public ServicoAlertas Alertas { get; }
    public SeletorPontoApoio Seletor { get; }
    public SimuladorLeituras Simulador { get; }
    public ImportadorLeituras Importador { get; }
    public ClienteApiLeituras ClienteApi { get; }
    public string Operador { get; }

    /// <summary>Endpoint GET da API do grupo que devolve leituras no contrato da estação.</summary>
    public string UrlApi { get; set; } = "http://localhost:5000/api/leituras";

    public CenarioChuva Cenario { get; set; } = CenarioChuva.Tempestade;

    /// <summary>Disparado depois de qualquer alteração; as telas se atualizam.</summary>
    public event EventHandler? DadosAlterados;

    /// <summary>Mensagem curta para a barra de status.</summary>
    public event EventHandler<string>? Mensagem;

    public PainelContexto(ContextoDados dados, string operador)
    {
        Dados = dados;
        Operador = string.IsNullOrWhiteSpace(operador) ? "operador" : operador;
        Seletor = new SeletorPontoApoio();
        Monitoramento = new ServicoMonitoramento(dados, new CalculadoraRisco());
        Alertas = new ServicoAlertas(dados, Seletor);
        Simulador = new SimuladorLeituras();
        Importador = new ImportadorLeituras();
        ClienteApi = new ClienteApiLeituras(new HttpClient { Timeout = TimeSpan.FromSeconds(6) }, Importador);
    }

    public void Notificar(string mensagem)
    {
        DadosAlterados?.Invoke(this, EventArgs.Empty);
        Mensagem?.Invoke(this, $"{DateTime.Now:HH:mm:ss} · {mensagem}");
    }

    public IReadOnlyDictionary<string, NivelRisco> RiscoPorSetor() =>
        Dados.Setores.Listar().ToDictionary(s => s.Id, s => s.RiscoAtual);

    public string NomeSetor(string setorId) => Dados.Setores.ObterPorId(setorId)?.Nome ?? setorId;

    /// <summary>Gera uma leitura simulada para cada setor (modo demonstração).</summary>
    public ResumoImportacao GerarLeiturasSimuladas()
    {
        var agora = DateTime.Now;
        var leituras = Dados.Setores.Listar()
            .Select(s => Simulador.Gerar(s, Monitoramento.LeiturasDoSetor(s.Id).FirstOrDefault(), Cenario, agora))
            .ToList();
        return Monitoramento.RegistrarVarias(leituras);
    }

    /// <summary>Setores em MÉDIO/ALTO sem alerta compatível (mostrados na faixa de aviso).</summary>
    public IReadOnlyList<Setor> SetoresComAlertaRecomendado() =>
        Dados.Setores.Listar().Where(Monitoramento.AlertaRecomendado).OrderByDescending(s => s.RiscoAtual).ToList();

    public static string DescreverCenario(CenarioChuva c) => c switch
    {
        CenarioChuva.Estiagem => "Estiagem",
        CenarioChuva.ChuvaModerada => "Chuva moderada",
        CenarioChuva.Tempestade => "Tempestade",
        _ => c.ToString()
    };
}
