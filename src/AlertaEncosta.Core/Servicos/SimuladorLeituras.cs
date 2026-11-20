using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

public enum CenarioChuva
{
    Estiagem = 0,
    ChuvaModerada = 1,
    Tempestade = 2
}

/// <summary>
/// Gera leituras SIMULADAS e plausíveis para a demonstração (regra da GS: nada de dados reais).
/// Cada nova leitura parte da anterior: a chuva muda aos poucos, o solo encharca com a chuva
/// e o córrego sobe com atraso.
/// </summary>
public sealed class SimuladorLeituras
{
    private readonly Random _aleatorio;

    public SimuladorLeituras(Random? aleatorio = null) => _aleatorio = aleatorio ?? new Random();

    public LeituraSensor Gerar(Setor setor, LeituraSensor? anterior, CenarioChuva cenario, DateTime quando)
    {
        var (min, max) = cenario switch
        {
            CenarioChuva.Tempestade => (16.0, 34.0),
            CenarioChuva.ChuvaModerada => (5.0, 14.0),
            _ => (0.0, 1.5)
        };

        var alvoChuva = min + _aleatorio.NextDouble() * (max - min);
        var chuvaAnterior = anterior?.ChuvaMm ?? 0;
        var chuva = Limitar(chuvaAnterior * 0.4 + alvoChuva * 0.6, 0, LeituraSensor.ChuvaMaximaMm);

        var soloAnterior = anterior?.UmidadeSoloPct ?? 35;
        var deltaSolo = chuva switch
        {
            >= 15 => _aleatorio.Next(5, 10),
            >= 5 => _aleatorio.Next(1, 4),
            _ => -_aleatorio.Next(1, 4)
        };
        var solo = (int)Limitar(soloAnterior + deltaSolo, 20, 100);

        var alvoNivel = 10 + chuva * 4.5 + (solo >= 80 ? 25 : 0);
        var nivelAnterior = anterior?.NivelAguaCm ?? 12;
        var nivel = Limitar(nivelAnterior * 0.55 + alvoNivel * 0.45 + (_aleatorio.NextDouble() * 4 - 2), 0, 200);

        var total = (anterior?.ChuvaTotalMm ?? 0) + chuva * 0.25;
        if (total < chuva) total = chuva;

        return new LeituraSensor(setor.EstacaoId, setor.Id, quando, chuva, total, solo, nivel, OrigemLeitura.Simulada);
    }

    private static double Limitar(double valor, double min, double max) => Math.Min(max, Math.Max(min, valor));
}
