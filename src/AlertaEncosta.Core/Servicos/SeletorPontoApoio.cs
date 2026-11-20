using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

public sealed record OpcaoPontoApoio(PontoApoio Ponto, double DistanciaKm, bool CabeTodos)
{
    public string Descricao =>
        $"{Ponto.Nome} · {DistanciaKm:0.0} km · {Ponto.VagasDisponiveis} vagas" +
        (Ponto.Acessivel ? " · acessível" : string.Empty);
}

/// <summary>
/// Escolhe o ponto de apoio para onde o alerta vai mandar os moradores.
/// Algoritmo:
///  1. descarta pontos desativados ou lotados;
///  2. se o setor tem pessoas vulneráveis, prefere pontos acessíveis (se existir algum com vaga);
///  3. ordena: primeiro os que comportam todo mundo, depois pela menor distância (Haversine).
/// </summary>
public sealed class SeletorPontoApoio
{
    public IReadOnlyList<OpcaoPontoApoio> Ranquear(Coordenada origem, IEnumerable<PontoApoio> pontos,
        int pessoas, bool precisaAcessivel)
    {
        var candidatos = pontos.Where(p => p.Ativo && !p.Lotado).ToList();

        if (precisaAcessivel && candidatos.Any(p => p.Acessivel))
            candidatos = candidatos.Where(p => p.Acessivel).ToList();

        return candidatos
            .Select(p => new OpcaoPontoApoio(p, origem.DistanciaKm(p.Local), p.VagasDisponiveis >= pessoas))
            .OrderByDescending(o => o.CabeTodos)
            .ThenBy(o => o.DistanciaKm)
            .ThenByDescending(o => o.Ponto.VagasDisponiveis)
            .ToList();
    }

    public OpcaoPontoApoio? Selecionar(Coordenada origem, IEnumerable<PontoApoio> pontos, int pessoas, bool precisaAcessivel) =>
        Ranquear(origem, pontos, pessoas, precisaAcessivel).FirstOrDefault();
}
