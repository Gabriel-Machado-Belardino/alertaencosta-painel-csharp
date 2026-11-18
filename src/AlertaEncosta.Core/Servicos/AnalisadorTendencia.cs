using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

public enum Tendencia
{
    Estavel = 0,
    Subindo = 1,
    Descendo = 2
}

/// <summary>
/// Compara a leitura mais recente com a média das anteriores para dizer se a situação
/// está piorando. Ajuda o operador a agir ANTES do limiar (antecedência do alerta).
/// </summary>
public static class AnalisadorTendencia
{
    public const double VariacaoNivelCm = 5;
    public const double VariacaoChuvaMm = 2;

    /// <param name="leituras">Leituras de UM setor, em qualquer ordem.</param>
    /// <param name="janela">Quantas leituras anteriores entram na média.</param>
    public static Tendencia Analisar(IEnumerable<LeituraSensor> leituras, int janela = 3)
    {
        var ordenadas = leituras.OrderByDescending(l => l.DataHora).Take(janela + 1).ToList();
        if (ordenadas.Count < 2) return Tendencia.Estavel;

        var atual = ordenadas[0];
        var anteriores = ordenadas.Skip(1).ToList();
        var difNivel = atual.NivelAguaCm - anteriores.Average(l => l.NivelAguaCm);
        var difChuva = atual.ChuvaMm - anteriores.Average(l => l.ChuvaMm);

        if (difNivel >= VariacaoNivelCm || (difChuva >= VariacaoChuvaMm && difNivel > -VariacaoNivelCm))
            return Tendencia.Subindo;
        if (difNivel <= -VariacaoNivelCm || (difChuva <= -VariacaoChuvaMm && difNivel < VariacaoNivelCm))
            return Tendencia.Descendo;
        return Tendencia.Estavel;
    }

    public static string Simbolo(this Tendencia t) => t switch
    {
        Tendencia.Subindo => "▲ subindo",
        Tendencia.Descendo => "▼ descendo",
        _ => "● estável"
    };
}
