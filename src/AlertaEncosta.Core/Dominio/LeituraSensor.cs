using System.Text.Json.Serialization;

namespace AlertaEncosta.Core.Dominio;

/// <summary>De onde veio a leitura.</summary>
public enum OrigemLeitura
{
    Manual = 0,       // digitada pelo operador (ex.: medição em campo)
    EstacaoEsp32 = 1, // JSON enviado pela estação (Wokwi/ESP32) ou importado da API
    Simulada = 2      // gerada pelo modo demonstração
}

/// <summary>
/// Objeto de dados "Leitura de sensor" da arquitetura: chuva (mm), umidade do solo (%),
/// nível da água (cm), data e hora. É imutável depois de criada.
/// </summary>
public class LeituraSensor : IEntidade
{
    // Limites físicos aceitos (validação de entrada; valores fora disso indicam sensor com defeito).
    public const double ChuvaMaximaMm = 500;
    public const double NivelMaximoCm = 400;

    [JsonInclude] public string Id { get; private set; } = string.Empty;
    [JsonInclude] public string EstacaoId { get; private set; } = string.Empty;
    [JsonInclude] public string SetorId { get; private set; } = string.Empty;
    [JsonInclude] public DateTime DataHora { get; private set; }
    /// <summary>Chuva acumulada na janela de tempo da estação (mm).</summary>
    [JsonInclude] public double ChuvaMm { get; private set; }
    /// <summary>Chuva total desde que a estação ligou (mm).</summary>
    [JsonInclude] public double ChuvaTotalMm { get; private set; }
    [JsonInclude] public int UmidadeSoloPct { get; private set; }
    [JsonInclude] public double NivelAguaCm { get; private set; }
    /// <summary>Risco calculado na própria estação (plano B local). Nulo em leituras manuais.</summary>
    [JsonInclude] public NivelRisco? RiscoEstacao { get; private set; }
    [JsonInclude] public OrigemLeitura Origem { get; private set; }

    [JsonConstructor]
    private LeituraSensor() { }

    public LeituraSensor(string estacaoId, string setorId, DateTime dataHora,
        double chuvaMm, double chuvaTotalMm, int umidadeSoloPct, double nivelAguaCm,
        OrigemLeitura origem, NivelRisco? riscoEstacao = null, string? id = null)
    {
        EstacaoId = Validar.TextoObrigatorio(estacaoId, "o código da estação", 3, 20).ToUpperInvariant();
        SetorId = Validar.TextoObrigatorio(setorId, "o setor", 3, 20).ToUpperInvariant();

        if (dataHora > DateTime.Now.AddMinutes(10))
            throw new DominioException("A data da leitura não pode estar no futuro.");
        if (dataHora < new DateTime(2020, 1, 1))
            throw new DominioException("A data da leitura é muito antiga (antes de 2020).");
        DataHora = dataHora;

        ChuvaMm = Math.Round(Validar.Faixa(chuvaMm, 0, ChuvaMaximaMm, "a chuva na janela", "mm"), 1);
        ChuvaTotalMm = Math.Round(Validar.Faixa(chuvaTotalMm, 0, 100_000, "a chuva total", "mm"), 1);
        if (ChuvaTotalMm < ChuvaMm)
            throw new DominioException("A chuva total não pode ser menor que a chuva da janela.");

        UmidadeSoloPct = Validar.Faixa(umidadeSoloPct, 0, 100, "a umidade do solo");
        NivelAguaCm = Math.Round(Validar.Faixa(nivelAguaCm, 0, NivelMaximoCm, "o nível da água", "cm"), 1);

        Origem = origem;
        RiscoEstacao = riscoEstacao;
        Id = string.IsNullOrWhiteSpace(id) ? $"LEI-{Guid.NewGuid():N}"[..16].ToUpperInvariant() : id;
    }

    public override string ToString() =>
        $"{SetorId} {DataHora:dd/MM HH:mm} chuva={ChuvaMm}mm solo={UmidadeSoloPct}% nível={NivelAguaCm}cm";
}
