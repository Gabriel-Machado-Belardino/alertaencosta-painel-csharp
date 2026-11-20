using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Integracao;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Core.Tests;

public class IntegracaoEPersistenciaTests
{
    private static readonly DateTime Agora = new(2026, 11, 10, 10, 0, 0);
    private readonly ImportadorLeituras _imp = new();

    [Fact]
    public void Importa_json_do_contrato_da_estacao()
    {
        const string json = """
        {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","timestamp":null,"chuvaMm":11.0,
         "chuvaTotalMm":11.0,"umidadeSoloPct":87,"nivelAguaCm":8.4,"riscoLocal":"ALTO","uptimeMs":34855}
        """;

        var r = _imp.Interpretar(json, Agora);

        Assert.Empty(r.Erros);
        var l = Assert.Single(r.Leituras);
        Assert.Equal("BRAS-S01", l.SetorId);
        Assert.Equal(87, l.UmidadeSoloPct);
        Assert.Equal(NivelRisco.Alto, l.RiscoEstacao);
        Assert.Equal(Agora, l.DataHora); // sem NTP: carimbado na chegada
        Assert.Equal(OrigemLeitura.EstacaoEsp32, l.Origem);
    }

    [Fact]
    public void Importa_linhas_do_serial_monitor_do_wokwi_e_ignora_o_resto()
    {
        const string serial = """
        === AlertaEncosta - estacao EST-BRAS-01 (setor BRAS-S01) ===
        [WIFI] conectado, IP 10.10.0.2
        [LEITURA] chuva=11.0mm solo=87% nivel=8.4cm risco=ALTO fila=0
        [API] POST http://httpbin.org/post -> HTTP 200 | {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","timestamp":"2026-11-18T02:00:27Z","chuvaMm":11.0,"chuvaTotalMm":11.0,"umidadeSoloPct":87,"nivelAguaCm":8.4,"riscoLocal":"ALTO","uptimeMs":34855}
        [API] POST http://httpbin.org/post -> HTTP 200 | {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","timestamp":"2026-11-18T02:00:35Z","chuvaMm":12.0,"chuvaTotalMm":12.0,"umidadeSoloPct":87,"nivelAguaCm":9.0,"riscoLocal":"ALTO","uptimeMs":42855}
        """;

        var r = _imp.Interpretar(serial, Agora);

        Assert.Empty(r.Erros);
        Assert.Equal(2, r.Leituras.Count);
        Assert.Equal(new DateTimeOffset(2026, 11, 18, 2, 0, 27, TimeSpan.Zero).LocalDateTime, r.Leituras[0].DataHora);
    }

    [Fact]
    public void Importa_array_e_reporta_itens_invalidos_sem_perder_os_validos()
    {
        const string json = """
        [
          {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","chuvaMm":5,"umidadeSoloPct":40,"nivelAguaCm":20},
          {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","chuvaMm":5,"umidadeSoloPct":140,"nivelAguaCm":20},
          {"estacaoId":"EST-BRAS-01","chuvaMm":5,"umidadeSoloPct":40}
        ]
        """;

        var r = _imp.Interpretar(json, Agora);

        Assert.Single(r.Leituras);
        Assert.Equal(2, r.Erros.Count);
        Assert.Contains(r.Erros, e => e.StartsWith("Item 2") && e.Contains("umidade do solo"));
        Assert.Contains(r.Erros, e => e.StartsWith("Item 3") && e.Contains("setorId") && e.Contains("nivelAguaCm"));
    }

    [Fact]
    public void Texto_sem_json_gera_erro_explicativo()
    {
        var r = _imp.Interpretar("[LEITURA] chuva=0mm", Agora);
        Assert.Empty(r.Leituras);
        Assert.Contains("Nenhuma leitura JSON", Assert.Single(r.Erros));
    }

    [Fact]
    public void Url_da_api_e_validada()
    {
        Assert.Throws<DominioException>(() => ClienteApiLeituras.ValidarUrl("ftp://x"));
        Assert.Throws<DominioException>(() => ClienteApiLeituras.ValidarUrl("localhost/api"));
        Assert.Equal("https", ClienteApiLeituras.ValidarUrl("https://api.exemplo/leituras").Scheme);
    }

    [Fact]
    public void Persistencia_json_grava_e_recarrega_entidades_encapsuladas()
    {
        var pasta = Path.Combine(Path.GetTempPath(), "alertaencosta-teste-" + Guid.NewGuid().ToString("N"));
        try
        {
            var ctx = ContextoDados.EmPasta(pasta);
            DadosIniciais.Popular(ctx, Agora);
            var morador = ctx.Moradores.Listar()[0];
            morador.ReceberAlerta("ALT-0001", Agora);
            ctx.PontosApoio.ObterPorId("PA-01")!.RegistrarEntrada(7);
            ctx.SalvarTudo();

            var recarregado = ContextoDados.EmPasta(pasta);

            Assert.Equal(4, recarregado.Setores.Quantidade);
            Assert.Equal(24, recarregado.Moradores.Quantidade);
            Assert.Equal(48 + 11, recarregado.Leituras.Quantidade); // 12 por setor + evento de ontem
            var historico = Assert.Single(recarregado.Alertas.Listar());
            Assert.False(historico.Ativo);
            Assert.Equal(148, CalculadoraIndicadores.AntecedenciaMedia(recarregado.Alertas.Listar(), recarregado.Leituras.Listar(), Agora));
            Assert.Equal(7, recarregado.PontosApoio.ObterPorId("PA-01")!.Ocupacao);
            var m2 = recarregado.Moradores.ObterPorId(morador.Id)!;
            Assert.Equal(SituacaoMorador.Alertado, m2.Situacao);
            Assert.Equal(morador.Telefone, m2.Telefone);
            Assert.Equal(-23.4470, recarregado.Setores.ObterPorId("BRAS-S01")!.Centro.Latitude);
            Assert.Equal(StatusOcorrencia.Resolvida, recarregado.Ocorrencias.ObterPorId("OCO-0002")!.Status);
            Assert.Contains("Brasilândia", File.ReadAllText(Path.Combine(pasta, "setores.json"))); // acentos legíveis
        }
        finally
        {
            if (Directory.Exists(pasta)) Directory.Delete(pasta, recursive: true);
        }
    }

    [Fact]
    public void Dados_iniciais_nao_duplicam_ao_rodar_de_novo()
    {
        var ctx = ContextoDados.EmMemoria();
        DadosIniciais.Popular(ctx, Agora);
        DadosIniciais.Popular(ctx, Agora);
        Assert.Equal(4, ctx.Setores.Quantidade);
    }
}
