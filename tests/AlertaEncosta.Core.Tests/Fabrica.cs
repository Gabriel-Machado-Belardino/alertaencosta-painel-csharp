using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;

namespace AlertaEncosta.Core.Tests;

/// <summary>Objetos de teste prontos (Object Mother).</summary>
internal static class Fabrica
{
    public static readonly DateTime Base = new(2026, 11, 10, 10, 0, 0);

    public static LeituraSensor Leitura(string setor = "BRAS-S01", double chuva = 0, int solo = 30, double nivel = 10,
        DateTime? quando = null, string estacao = "EST-BRAS-01") =>
        new(estacao, setor, quando ?? Base, chuva, chuva, solo, nivel, OrigemLeitura.Manual);

    public static Setor Setor(string id = "BRAS-S01") =>
        new(id, $"Setor {id}", "Vila Brasilândia", "EST-" + id, new Coordenada(-23.45, -46.69));

    public static PontoApoio Ponto(string id, int capacidade = 100, bool acessivel = true, double lat = -23.45, double lon = -46.69) =>
        new(id, $"Ponto {id}", "Rua de Teste, 100", new Coordenada(lat, lon), capacidade, acessivel);

    public static Morador Morador(string id, string setor, CondicaoEspecial condicao = CondicaoEspecial.Nenhuma) =>
        new(id, $"Morador {id}", setor, "Rua de Teste, 1", "11912345678", condicao);

    public static ContextoDados ContextoComSetor(params string[] setores)
    {
        var ctx = ContextoDados.EmMemoria();
        foreach (var s in setores.DefaultIfEmpty("BRAS-S01")) ctx.Setores.Adicionar(Setor(s));
        return ctx;
    }
}
