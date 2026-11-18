using System.Text.Json.Serialization;

namespace AlertaEncosta.Core.Dominio;

/// <summary>
/// Setor de risco mapeado (área da encosta/córrego monitorada por uma estação).
/// O risco atual só muda pelo método <see cref="AtualizarRisco"/>, chamado pelo serviço de monitoramento.
/// </summary>
public class Setor : IEntidade
{
    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string Nome { get; private set; } = string.Empty;
    [JsonInclude] public string Bairro { get; private set; } = string.Empty;
    [JsonInclude] public string EstacaoId { get; private set; } = string.Empty;
    [JsonInclude] public Coordenada Centro { get; private set; }
    [JsonInclude] public NivelRisco RiscoAtual { get; private set; } = NivelRisco.Baixo;
    [JsonInclude] public DateTime? RiscoAtualizadoEm { get; private set; }

    [JsonConstructor]
    private Setor() { }

    public Setor(string id, string nome, string bairro, string estacaoId, Coordenada centro)
    {
        Id = Validar.TextoObrigatorio(id, "o código do setor", 3, 20).ToUpperInvariant();
        Nome = Validar.TextoObrigatorio(nome, "o nome do setor", 3, 80);
        Bairro = Validar.TextoObrigatorio(bairro, "o bairro", 3, 80);
        EstacaoId = Validar.TextoObrigatorio(estacaoId, "o código da estação", 3, 20).ToUpperInvariant();
        centro.Validar();
        Centro = centro;
    }

    /// <summary>Registra o novo nível de risco. Retorna true se houve mudança.</summary>
    public bool AtualizarRisco(NivelRisco novo, DateTime quando)
    {
        var mudou = novo != RiscoAtual;
        RiscoAtual = novo;
        RiscoAtualizadoEm = quando;
        return mudou;
    }

    public override string ToString() => $"{Id} · {Nome}";
}
