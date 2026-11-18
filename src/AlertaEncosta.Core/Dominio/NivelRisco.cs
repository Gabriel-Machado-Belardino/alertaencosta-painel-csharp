namespace AlertaEncosta.Core.Dominio;

/// <summary>Nível de risco de um setor ou de uma leitura. A ordem importa: BAIXO &lt; MÉDIO &lt; ALTO.</summary>
public enum NivelRisco
{
    Baixo = 0,
    Medio = 1,
    Alto = 2
}

public static class NivelRiscoExtensoes
{
    /// <summary>Texto curto em caixa alta, igual ao que aparece no LCD da estação.</summary>
    public static string Rotulo(this NivelRisco nivel) => nivel switch
    {
        NivelRisco.Baixo => "BAIXO",
        NivelRisco.Medio => "MÉDIO",
        NivelRisco.Alto => "ALTO",
        _ => nivel.ToString().ToUpperInvariant()
    };

    /// <summary>Orientação em linguagem simples para o operador e para o morador.</summary>
    public static string Orientacao(this NivelRisco nivel) => nivel switch
    {
        NivelRisco.Baixo => "Situação normal. Seguir monitorando.",
        NivelRisco.Medio => "Atenção: preparar a saída e acompanhar os avisos.",
        NivelRisco.Alto => "Perigo: sair agora e ir para o ponto de apoio.",
        _ => string.Empty
    };

    /// <summary>
    /// Converte o texto enviado pela estação ESP32 ("BAIXO", "MEDIO", "ALTO") ou digitado pelo
    /// usuário ("médio", "Alto"...). Retorna false se o texto não for reconhecido.
    /// </summary>
    public static bool TentarConverter(string? texto, out NivelRisco nivel)
    {
        nivel = NivelRisco.Baixo;
        if (string.IsNullOrWhiteSpace(texto)) return false;

        var normalizado = texto.Trim().ToUpperInvariant().Replace("É", "E");
        switch (normalizado)
        {
            case "BAIXO": nivel = NivelRisco.Baixo; return true;
            case "MEDIO": nivel = NivelRisco.Medio; return true;
            case "ALTO": nivel = NivelRisco.Alto; return true;
            default: return false;
        }
    }

    public static NivelRisco Maior(NivelRisco a, NivelRisco b) => a >= b ? a : b;
}
