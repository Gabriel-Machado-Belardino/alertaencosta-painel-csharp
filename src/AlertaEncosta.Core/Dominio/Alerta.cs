using System.Text.Json.Serialization;

namespace AlertaEncosta.Core.Dominio;

/// <summary>
/// Alerta emitido pela Defesa Civil para um setor: nível, mensagem, ponto de apoio indicado e
/// quantos moradores foram avisados. As confirmações ficam no próprio <see cref="Morador"/>.
/// </summary>
public class Alerta : IEntidade
{
    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string SetorId { get; private set; } = string.Empty;
    [JsonInclude] public NivelRisco Nivel { get; private set; }
    [JsonInclude] public string Mensagem { get; private set; } = string.Empty;
    [JsonInclude] public string? PontoApoioId { get; private set; }
    [JsonInclude] public DateTime EmitidoEm { get; private set; }
    [JsonInclude] public DateTime? EscaladoEm { get; private set; }
    [JsonInclude] public DateTime? EncerradoEm { get; private set; }
    [JsonInclude] public int MoradoresAlertados { get; private set; }
    [JsonInclude] public string Operador { get; private set; } = string.Empty;

    public bool Ativo => EncerradoEm is null;

    [JsonConstructor]
    private Alerta() { }

    public Alerta(string id, string setorId, NivelRisco nivel, string mensagem, string? pontoApoioId,
        int moradoresAlertados, string operador, DateTime emitidoEm)
    {
        if (nivel == NivelRisco.Baixo)
            throw new DominioException("Não se emite alerta com risco BAIXO.");
        Id = Validar.TextoObrigatorio(id, "o código do alerta", 3, 20);
        SetorId = Validar.TextoObrigatorio(setorId, "o setor", 3, 20);
        Nivel = nivel;
        Mensagem = Validar.TextoObrigatorio(mensagem, "a mensagem do alerta", 10, 500);
        PontoApoioId = pontoApoioId;
        MoradoresAlertados = Math.Max(0, moradoresAlertados);
        Operador = Validar.TextoObrigatorio(operador, "o operador", 2, 60);
        EmitidoEm = emitidoEm;
    }

    /// <summary>O risco subiu (MÉDIO → ALTO) com o alerta ainda ativo: atualiza nível e mensagem.</summary>
    public void Escalar(NivelRisco novoNivel, string novaMensagem, string? pontoApoioId, int moradoresAlertados, DateTime quando)
    {
        if (!Ativo) throw new DominioException("Este alerta já foi encerrado.");
        if (novoNivel <= Nivel)
            throw new DominioException($"O alerta já está em {Nivel.Rotulo()}.");
        Nivel = novoNivel;
        Mensagem = Validar.TextoObrigatorio(novaMensagem, "a mensagem do alerta", 10, 500);
        PontoApoioId = pontoApoioId ?? PontoApoioId;
        MoradoresAlertados = Math.Max(MoradoresAlertados, moradoresAlertados);
        EscaladoEm = quando;
    }

    public void Encerrar(DateTime quando)
    {
        if (!Ativo) throw new DominioException("Este alerta já foi encerrado.");
        if (quando < EmitidoEm) throw new DominioException("O encerramento não pode ser antes da emissão.");
        EncerradoEm = quando;
    }

    public TimeSpan Duracao(DateTime agora) => (EncerradoEm ?? agora) - EmitidoEm;
}
