using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AlertaEncosta.Core.Dominio;

/// <summary>Condição que dá prioridade no atendimento (busca ativa).</summary>
public enum CondicaoEspecial
{
    Nenhuma = 0,
    Idoso = 1,
    MobilidadeReduzida = 2,
    DeficienciaVisual = 3,
    DeficienciaAuditiva = 4,
    Gestante = 5,
    Acamado = 6
}

/// <summary>Situação do morador em relação ao alerta ativo do setor.</summary>
public enum SituacaoMorador
{
    SemAlerta = 0,
    Alertado = 1,       // recebeu o alerta e ainda não respondeu
    EmLocalSeguro = 2,  // confirmou no app que está seguro (ou foi resgatado)
    PrecisaAjuda = 3    // pediu ajuda para sair
}

/// <summary>
/// Cadastro do morador do setor de risco. No protótipo todos os dados são SIMULADOS (LGPD);
/// o telefone é exibido mascarado nas telas.
/// </summary>
public partial class Morador : IEntidade
{
    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string Nome { get; private set; } = string.Empty;
    [JsonInclude] public string SetorId { get; private set; } = string.Empty;
    [JsonInclude] public string Endereco { get; private set; } = string.Empty;
    [JsonInclude] public string Telefone { get; private set; } = string.Empty;
    [JsonInclude] public CondicaoEspecial Condicao { get; private set; }
    [JsonInclude] public SituacaoMorador Situacao { get; private set; } = SituacaoMorador.SemAlerta;
    [JsonInclude] public string? AlertaId { get; private set; }
    [JsonInclude] public DateTime? SituacaoAtualizadaEm { get; private set; }

    public bool EhVulneravel => Condicao != CondicaoEspecial.Nenhuma;

    /// <summary>Telefone para exibição: só os 4 últimos dígitos (privacidade por padrão).</summary>
    public string TelefoneMascarado =>
        Telefone.Length >= 4 ? $"(**) *****-{Telefone[^4..]}" : "não informado";

    [JsonConstructor]
    private Morador() { }

    public Morador(string id, string nome, string setorId, string endereco, string telefone, CondicaoEspecial condicao)
    {
        Id = Validar.TextoObrigatorio(id, "o código do morador", 3, 20).ToUpperInvariant();
        AlterarCadastro(nome, setorId, endereco, telefone, condicao);
    }

    public void AlterarCadastro(string nome, string setorId, string endereco, string telefone, CondicaoEspecial condicao)
    {
        // Valida tudo antes de alterar: uma edição inválida não deixa o cadastro pela metade.
        var novoNome = Validar.TextoObrigatorio(nome, "o nome", 3, 80);
        var novoSetor = Validar.TextoObrigatorio(setorId, "o setor", 3, 20).ToUpperInvariant();
        var novoEndereco = Validar.TextoObrigatorio(endereco, "o endereço", 5, 120);
        var novoTelefone = NormalizarTelefone(telefone);
        if (!Enum.IsDefined(condicao)) throw new DominioException("Condição especial inválida.");
        if (AlertaId is not null && novoSetor != SetorId)
            throw new DominioException("Não é possível mudar o setor de um morador com alerta ativo.");

        (Nome, SetorId, Endereco, Telefone, Condicao) = (novoNome, novoSetor, novoEndereco, novoTelefone, condicao);
    }

    /// <summary>Aceita "(11) 91234-5678", "11912345678" etc. Guarda só os dígitos (10 ou 11).</summary>
    public static string NormalizarTelefone(string? telefone)
    {
        var digitos = ApenasDigitos().Replace(telefone ?? string.Empty, string.Empty);
        if (digitos.Length is not (10 or 11))
            throw new DominioException("Telefone inválido: use DDD + número, ex.: (11) 91234-5678.");
        return digitos;
    }

    // ----- Fluxo do alerta (máquina de estados simples) -----

    public void ReceberAlerta(string alertaId, DateTime quando)
    {
        AlertaId = alertaId;
        // Quem já está em local seguro continua seguro se o alerta for escalado.
        if (Situacao is SituacaoMorador.SemAlerta)
            Situacao = SituacaoMorador.Alertado;
        SituacaoAtualizadaEm = quando;
    }

    public void ConfirmarLocalSeguro(DateTime quando)
    {
        if (AlertaId is null)
            throw new DominioException($"{Nome} não está em um alerta ativo.");
        Situacao = SituacaoMorador.EmLocalSeguro;
        SituacaoAtualizadaEm = quando;
    }

    public void PedirAjuda(DateTime quando)
    {
        if (AlertaId is null)
            throw new DominioException($"{Nome} não está em um alerta ativo.");
        if (Situacao == SituacaoMorador.EmLocalSeguro)
            throw new DominioException($"{Nome} já confirmou que está em local seguro.");
        Situacao = SituacaoMorador.PrecisaAjuda;
        SituacaoAtualizadaEm = quando;
    }

    /// <summary>Alerta encerrado: o morador volta à situação normal.</summary>
    public void EncerrarAlerta(DateTime quando)
    {
        AlertaId = null;
        Situacao = SituacaoMorador.SemAlerta;
        SituacaoAtualizadaEm = quando;
    }

    [GeneratedRegex(@"\D")]
    private static partial Regex ApenasDigitos();

    public override string ToString() => $"{Nome} ({SetorId})";
}
