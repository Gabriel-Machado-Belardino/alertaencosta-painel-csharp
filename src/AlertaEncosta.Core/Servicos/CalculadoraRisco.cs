using System.Globalization;
using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

/// <summary>Resultado da avaliação de uma leitura: o nível final e o porquê, em texto simples.</summary>
public sealed record AvaliacaoRisco(
    NivelRisco Nivel,
    NivelRisco RiscoChuva,
    NivelRisco RiscoSolo,
    NivelRisco RiscoNivelAgua,
    bool RegraCombinadaAtiva,
    IReadOnlyList<string> Motivos)
{
    public string MotivosEmTexto => Motivos.Count == 0 ? "Todos os sinais dentro do normal." : string.Join(" ", Motivos);
}

/// <summary>
/// Regra de decisão do risco (a mesma da estação ESP32):
/// o risco é o MAIOR entre chuva, solo e nível da água, mais a regra combinada
/// "chuva ≥ MÉDIO e solo saturado ⇒ ALTO" (condição clássica de deslizamento).
/// O solo sozinho nunca passa de MÉDIO.
/// </summary>
public sealed class CalculadoraRisco
{
    private static readonly CultureInfo Br = CultureInfo.GetCultureInfo("pt-BR");

    public LimiaresRisco Limiares { get; }

    public CalculadoraRisco(LimiaresRisco? limiares = null)
    {
        Limiares = limiares ?? LimiaresRisco.Padrao;
        Limiares.Validar();
    }

    public AvaliacaoRisco Avaliar(LeituraSensor leitura) =>
        Avaliar(leitura.ChuvaMm, leitura.UmidadeSoloPct, leitura.NivelAguaCm);

    public AvaliacaoRisco Avaliar(double chuvaMm, int umidadeSoloPct, double nivelAguaCm)
    {
        var l = Limiares;
        var motivos = new List<string>();

        var riscoChuva = chuvaMm >= l.ChuvaAltoMm ? NivelRisco.Alto
                       : chuvaMm >= l.ChuvaMedioMm ? NivelRisco.Medio
                       : NivelRisco.Baixo;
        if (riscoChuva > NivelRisco.Baixo)
            motivos.Add($"Chuva de {Fmt(chuvaMm)} mm na janela ({riscoChuva.Rotulo()} a partir de " +
                        $"{Fmt(riscoChuva == NivelRisco.Alto ? l.ChuvaAltoMm : l.ChuvaMedioMm)} mm).");

        var riscoSolo = umidadeSoloPct >= l.SoloMedioPct ? NivelRisco.Medio : NivelRisco.Baixo;
        if (riscoSolo > NivelRisco.Baixo)
            motivos.Add($"Solo com {umidadeSoloPct}% de umidade (encharcado a partir de {l.SoloMedioPct}%).");

        var riscoNivel = nivelAguaCm >= l.NivelAltoCm ? NivelRisco.Alto
                       : nivelAguaCm >= l.NivelMedioCm ? NivelRisco.Medio
                       : NivelRisco.Baixo;
        if (riscoNivel > NivelRisco.Baixo)
            motivos.Add($"Córrego com {Fmt(nivelAguaCm)} cm ({riscoNivel.Rotulo()} a partir de " +
                        $"{Fmt(riscoNivel == NivelRisco.Alto ? l.NivelAltoCm : l.NivelMedioCm)} cm).");

        var nivel = NivelRiscoExtensoes.Maior(riscoChuva, NivelRiscoExtensoes.Maior(riscoSolo, riscoNivel));

        var combinada = riscoChuva >= NivelRisco.Medio && umidadeSoloPct >= l.SoloSaturadoPct;
        if (combinada)
        {
            if (nivel < NivelRisco.Alto)
                motivos.Add($"Regra combinada: chuva relevante com solo saturado (≥ {l.SoloSaturadoPct}%) eleva para ALTO.");
            nivel = NivelRisco.Alto;
        }

        return new AvaliacaoRisco(nivel, riscoChuva, riscoSolo, riscoNivel, combinada, motivos);
    }

    private static string Fmt(double valor) => valor.ToString("0.#", Br);
}
