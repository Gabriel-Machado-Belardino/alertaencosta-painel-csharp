using System.Text.Json.Serialization;

namespace AlertaEncosta.Core.Dominio;

public enum TipoOcorrencia
{
    Trinca = 0,          // trinca em parede/terreno: sinal de movimentação da encosta
    Alagamento = 1,
    Deslizamento = 2,
    QuedaDeArvore = 3,
    PedidoDeResgate = 4,
    Outro = 5
}

public enum StatusOcorrencia
{
    Aberta = 0,
    EmAtendimento = 1,
    Resolvida = 2
}

public enum CanalOcorrencia
{
    AppMorador = 0,
    Telefone199 = 1,
    LiderComunitario = 2,
    Vistoria = 3
}

/// <summary>
/// Reporte de ocorrência (trinca, alagamento, deslizamento...). O status segue a ordem
/// Aberta → Em atendimento → Resolvida; qualquer outro salto é recusado.
/// </summary>
public class Ocorrencia : IEntidade
{
    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string SetorId { get; private set; } = string.Empty;
    [JsonInclude] public TipoOcorrencia Tipo { get; private set; }
    [JsonInclude] public CanalOcorrencia Canal { get; private set; }
    [JsonInclude] public string Local { get; private set; } = string.Empty;
    [JsonInclude] public string Descricao { get; private set; } = string.Empty;
    /// <summary>Há pessoas em perigo no local (aumenta a prioridade).</summary>
    [JsonInclude] public bool PessoasEmRisco { get; private set; }
    [JsonInclude] public StatusOcorrencia Status { get; private set; } = StatusOcorrencia.Aberta;
    [JsonInclude] public DateTime AbertaEm { get; private set; }
    [JsonInclude] public DateTime? AtendimentoIniciadoEm { get; private set; }
    [JsonInclude] public DateTime? ResolvidaEm { get; private set; }
    [JsonInclude] public string? Responsavel { get; private set; }
    [JsonInclude] public string? Parecer { get; private set; }

    [JsonConstructor]
    private Ocorrencia() { }

    public Ocorrencia(string id, string setorId, TipoOcorrencia tipo, CanalOcorrencia canal,
        string local, string descricao, bool pessoasEmRisco, DateTime abertaEm)
    {
        Id = Validar.TextoObrigatorio(id, "o código da ocorrência", 3, 20);
        SetorId = Validar.TextoObrigatorio(setorId, "o setor", 3, 20);
        if (!Enum.IsDefined(tipo)) throw new DominioException("Tipo de ocorrência inválido.");
        if (!Enum.IsDefined(canal)) throw new DominioException("Canal inválido.");
        Tipo = tipo;
        Canal = canal;
        Local = Validar.TextoObrigatorio(local, "o local (rua, número ou referência)", 5, 120);
        Descricao = Validar.TextoObrigatorio(descricao, "a descrição", 10, 500);
        PessoasEmRisco = pessoasEmRisco;
        AbertaEm = abertaEm;
    }

    public void IniciarAtendimento(string responsavel, DateTime quando)
    {
        if (Status != StatusOcorrencia.Aberta)
            throw new DominioException($"A ocorrência {Id} já está {DescreverStatus(Status)}.");
        Responsavel = Validar.TextoObrigatorio(responsavel, "a equipe responsável", 2, 60);
        AtendimentoIniciadoEm = quando < AbertaEm ? AbertaEm : quando;
        Status = StatusOcorrencia.EmAtendimento;
    }

    public void Resolver(string parecer, DateTime quando)
    {
        if (Status != StatusOcorrencia.EmAtendimento)
            throw new DominioException(Status == StatusOcorrencia.Aberta
                ? "Inicie o atendimento antes de resolver a ocorrência."
                : $"A ocorrência {Id} já foi resolvida.");
        Parecer = Validar.TextoObrigatorio(parecer, "o parecer da vistoria", 5, 500);
        ResolvidaEm = quando < AtendimentoIniciadoEm ? AtendimentoIniciadoEm : quando;
        Status = StatusOcorrencia.Resolvida;
    }

    /// <summary>Tempo entre a abertura e o início do atendimento (ou até agora, se ainda aberta).</summary>
    public TimeSpan TempoDeEspera(DateTime agora) => (AtendimentoIniciadoEm ?? agora) - AbertaEm;

    public static string DescreverStatus(StatusOcorrencia status) => status switch
    {
        StatusOcorrencia.Aberta => "aberta",
        StatusOcorrencia.EmAtendimento => "em atendimento",
        StatusOcorrencia.Resolvida => "resolvida",
        _ => status.ToString()
    };
}
