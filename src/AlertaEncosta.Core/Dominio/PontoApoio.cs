using System.Text.Json.Serialization;

namespace AlertaEncosta.Core.Dominio;

/// <summary>
/// Abrigo temporário (escola, igreja, CEU...) que recebe moradores durante a evacuação.
/// A ocupação nunca passa da capacidade: entradas e saídas são validadas aqui.
/// </summary>
public class PontoApoio : IEntidade
{
    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string Nome { get; private set; } = string.Empty;
    [JsonInclude] public string Endereco { get; private set; } = string.Empty;
    [JsonInclude] public Coordenada Local { get; private set; }
    [JsonInclude] public int Capacidade { get; private set; }
    [JsonInclude] public int Ocupacao { get; private set; }
    /// <summary>Tem rampa/banheiro adaptado: necessário para idosos e pessoas com deficiência.</summary>
    [JsonInclude] public bool Acessivel { get; private set; }
    [JsonInclude] public bool Ativo { get; private set; } = true;

    public int VagasDisponiveis => Ativo ? Capacidade - Ocupacao : 0;
    public double PercentualOcupacao => Capacidade == 0 ? 0 : 100.0 * Ocupacao / Capacidade;
    public bool Lotado => VagasDisponiveis <= 0;

    [JsonConstructor]
    private PontoApoio() { }

    public PontoApoio(string id, string nome, string endereco, Coordenada local, int capacidade, bool acessivel)
    {
        Id = Validar.TextoObrigatorio(id, "o código do ponto de apoio", 3, 20).ToUpperInvariant();
        AlterarDados(nome, endereco, local, capacidade, acessivel, ativo: true);
    }

    public void AlterarDados(string nome, string endereco, Coordenada local, int capacidade, bool acessivel, bool ativo)
    {
        // Valida tudo antes de alterar: uma edição inválida não deixa o registro pela metade.
        var novoNome = Validar.TextoObrigatorio(nome, "o nome do ponto de apoio", 3, 80);
        var novoEndereco = Validar.TextoObrigatorio(endereco, "o endereço", 5, 120);
        local.Validar();
        Validar.Faixa(capacidade, 1, 5000, "a capacidade");
        if (capacidade < Ocupacao)
            throw new DominioException($"A capacidade não pode ser menor que a ocupação atual ({Ocupacao} pessoas).");
        if (!ativo && Ocupacao > 0)
            throw new DominioException("Não é possível desativar um ponto de apoio com pessoas abrigadas.");

        (Nome, Endereco, Local, Capacidade, Acessivel, Ativo) = (novoNome, novoEndereco, local, capacidade, acessivel, ativo);
    }

    public void RegistrarEntrada(int pessoas)
    {
        if (!Ativo) throw new DominioException($"{Nome} está desativado.");
        if (pessoas <= 0) throw new DominioException("Informe uma quantidade de pessoas maior que zero.");
        if (pessoas > VagasDisponiveis)
            throw new DominioException($"{Nome} só tem {VagasDisponiveis} vaga(s). Encaminhe o restante para outro ponto de apoio.");
        Ocupacao += pessoas;
    }

    public void RegistrarSaida(int pessoas)
    {
        if (pessoas <= 0) throw new DominioException("Informe uma quantidade de pessoas maior que zero.");
        if (pessoas > Ocupacao)
            throw new DominioException($"Só há {Ocupacao} pessoa(s) em {Nome}.");
        Ocupacao -= pessoas;
    }

    public override string ToString() => $"{Nome} ({VagasDisponiveis} vagas)";
}
