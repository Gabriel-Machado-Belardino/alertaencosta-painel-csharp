using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

/// <summary>
/// Limiares de risco. Os valores padrão são os mesmos do firmware da estação ESP32
/// (wokwi/config.h do repositório de Physical Computing), para o painel e a estação
/// "falarem a mesma língua". São didáticos: em produção devem ser calibrados com Cemaden/CGE-SP.
/// </summary>
public sealed record LimiaresRisco
{
    public double ChuvaMedioMm { get; init; } = 10;
    public double ChuvaAltoMm { get; init; } = 20;
    public int SoloMedioPct { get; init; } = 60;
    public int SoloSaturadoPct { get; init; } = 80;
    public double NivelMedioCm { get; init; } = 80;
    public double NivelAltoCm { get; init; } = 140;

    /// <summary>Quantas leituras seguidas mais baixas são necessárias para o risco do setor descer.</summary>
    public int LeiturasParaBaixar { get; init; } = 3;

    public static LimiaresRisco Padrao { get; } = new();

    public void Validar()
    {
        if (ChuvaMedioMm <= 0 || ChuvaAltoMm <= ChuvaMedioMm)
            throw new DominioException("Limiares de chuva inválidos: ALTO deve ser maior que MÉDIO.");
        if (SoloMedioPct is <= 0 or > 100 || SoloSaturadoPct <= SoloMedioPct || SoloSaturadoPct > 100)
            throw new DominioException("Limiares de solo inválidos.");
        if (NivelMedioCm <= 0 || NivelAltoCm <= NivelMedioCm)
            throw new DominioException("Limiares de nível inválidos: ALTO deve ser maior que MÉDIO.");
        if (LeiturasParaBaixar < 1)
            throw new DominioException("A histerese precisa de pelo menos 1 leitura.");
    }
}
