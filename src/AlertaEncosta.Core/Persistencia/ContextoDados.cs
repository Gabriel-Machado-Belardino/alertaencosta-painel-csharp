using System.Text.RegularExpressions;
using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Persistencia;

/// <summary>Reúne todos os repositórios da aplicação (o "banco de dados" do painel).</summary>
public sealed partial class ContextoDados
{
    public IRepositorio<Setor> Setores { get; }
    public IRepositorio<LeituraSensor> Leituras { get; }
    public IRepositorio<PontoApoio> PontosApoio { get; }
    public IRepositorio<Morador> Moradores { get; }
    public IRepositorio<Alerta> Alertas { get; }
    public IRepositorio<Ocorrencia> Ocorrencias { get; }

    /// <summary>Pasta onde ficam os JSON (nula quando em memória).</summary>
    public string? Pasta { get; }

    private ContextoDados(string? pasta,
        IRepositorio<Setor> setores, IRepositorio<LeituraSensor> leituras, IRepositorio<PontoApoio> pontos,
        IRepositorio<Morador> moradores, IRepositorio<Alerta> alertas, IRepositorio<Ocorrencia> ocorrencias)
    {
        Pasta = pasta;
        Setores = setores;
        Leituras = leituras;
        PontosApoio = pontos;
        Moradores = moradores;
        Alertas = alertas;
        Ocorrencias = ocorrencias;
    }

    public static ContextoDados EmPasta(string pasta)
    {
        Directory.CreateDirectory(pasta);
        return new ContextoDados(pasta,
            new RepositorioJson<Setor>(Path.Combine(pasta, "setores.json")),
            new RepositorioJson<LeituraSensor>(Path.Combine(pasta, "leituras.json")),
            new RepositorioJson<PontoApoio>(Path.Combine(pasta, "pontos-apoio.json")),
            new RepositorioJson<Morador>(Path.Combine(pasta, "moradores.json")),
            new RepositorioJson<Alerta>(Path.Combine(pasta, "alertas.json")),
            new RepositorioJson<Ocorrencia>(Path.Combine(pasta, "ocorrencias.json")));
    }

    public static ContextoDados EmMemoria() => new(null,
        new RepositorioMemoria<Setor>(), new RepositorioMemoria<LeituraSensor>(),
        new RepositorioMemoria<PontoApoio>(), new RepositorioMemoria<Morador>(),
        new RepositorioMemoria<Alerta>(), new RepositorioMemoria<Ocorrencia>());

    public bool Vazio => Setores.Quantidade == 0;

    public void SalvarTudo()
    {
        Setores.Salvar();
        Leituras.Salvar();
        PontosApoio.Salvar();
        Moradores.Salvar();
        Alertas.Salvar();
        Ocorrencias.Salvar();
    }

    /// <summary>
    /// Próximo código sequencial no formato PREFIXO-0001, olhando o maior número já usado
    /// (não reaproveita códigos de registros excluídos).
    /// </summary>
    public static string ProximoCodigo<T>(IRepositorio<T> repositorio, string prefixo, int digitos = 4)
        where T : class, IEntidade
    {
        var maior = repositorio.Listar()
            .Select(e => SufixoNumerico().Match(e.Id))
            .Where(m => m.Success && TemPrefixo(m, prefixo))
            .Select(m => int.Parse(m.Groups["n"].Value))
            .DefaultIfEmpty(0)
            .Max();
        return $"{prefixo}-{(maior + 1).ToString().PadLeft(digitos, '0')}";

        static bool TemPrefixo(Match m, string p) =>
            string.Equals(m.Groups["p"].Value, p, StringComparison.OrdinalIgnoreCase);
    }

    [GeneratedRegex(@"^(?<p>[A-Za-z]+)-(?<n>\d+)$")]
    private static partial Regex SufixoNumerico();
}
