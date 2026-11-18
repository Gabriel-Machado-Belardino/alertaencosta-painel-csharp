using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Core.Tests;

public class ServicosTests
{
    private static readonly DateTime T0 = Fabrica.Base;

    [Fact]
    public void Risco_sobe_na_hora_e_recomenda_alerta()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        var r = mon.Registrar(Fabrica.Leitura(chuva: 22, quando: T0));

        Assert.Equal(NivelRisco.Baixo, r.RiscoAnterior);
        Assert.Equal(NivelRisco.Alto, r.RiscoAtual);
        Assert.True(r.RiscoSubiu);
        Assert.True(r.AlertaRecomendado);
    }

    [Fact]
    public void Risco_so_desce_depois_de_tres_leituras_mais_baixas()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        mon.Registrar(Fabrica.Leitura(chuva: 25, quando: T0));
        Assert.Equal(NivelRisco.Alto, mon.Registrar(Fabrica.Leitura(chuva: 0, quando: T0.AddMinutes(1))).RiscoAtual);
        Assert.Equal(NivelRisco.Alto, mon.Registrar(Fabrica.Leitura(chuva: 0, quando: T0.AddMinutes(2))).RiscoAtual);
        Assert.Equal(NivelRisco.Baixo, mon.Registrar(Fabrica.Leitura(chuva: 0, quando: T0.AddMinutes(3))).RiscoAtual);
    }

    [Fact]
    public void Oscilacao_no_limiar_nao_derruba_o_risco()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        mon.Registrar(Fabrica.Leitura(chuva: 12, quando: T0));                       // MÉDIO
        mon.Registrar(Fabrica.Leitura(chuva: 9, quando: T0.AddMinutes(1)));          // abaixo
        mon.Registrar(Fabrica.Leitura(chuva: 10.5, quando: T0.AddMinutes(2)));       // volta a MÉDIO
        var r = mon.Registrar(Fabrica.Leitura(chuva: 9, quando: T0.AddMinutes(3)));  // abaixo de novo

        Assert.Equal(NivelRisco.Medio, r.RiscoAtual);
    }

    [Fact]
    public void Leitura_antiga_importada_nao_muda_o_risco_atual()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        mon.Registrar(Fabrica.Leitura(chuva: 0, quando: T0));
        var r = mon.Registrar(Fabrica.Leitura(chuva: 30, quando: T0.AddHours(-3)));

        Assert.Equal(NivelRisco.Baixo, r.RiscoAtual);
        Assert.Equal(2, ctx.Leituras.Quantidade);
    }

    [Fact]
    public void Recusa_setor_desconhecido_e_leitura_duplicada()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        Assert.Throws<DominioException>(() => mon.Registrar(Fabrica.Leitura(setor: "XXX-99")));
        mon.Registrar(Fabrica.Leitura(quando: T0));
        Assert.Throws<DominioException>(() => mon.Registrar(Fabrica.Leitura(quando: T0)));
    }

    [Fact]
    public void Importacao_em_lote_conta_validas_e_ignoradas()
    {
        var ctx = Fabrica.ContextoComSetor();
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);

        var resumo = mon.RegistrarVarias(new[]
        {
            Fabrica.Leitura(chuva: 22, quando: T0),
            Fabrica.Leitura(setor: "NAO-EXISTE", quando: T0),
            Fabrica.Leitura(chuva: 5, quando: T0.AddMinutes(-5))
        });

        Assert.Equal(2, resumo.Importadas);
        Assert.Equal(1, resumo.Ignoradas);
        Assert.Single(resumo.SetoresParaAlertar);
    }

    [Fact]
    public void Seletor_prefere_quem_cabe_todos_depois_o_mais_perto()
    {
        var origem = new Coordenada(-23.45, -46.69);
        var perto = Fabrica.Ponto("PA-PERTO", capacidade: 5, lat: -23.451, lon: -46.691);
        var longe = Fabrica.Ponto("PA-LONGE", capacidade: 100, lat: -23.47, lon: -46.71);

        var escolhido = new SeletorPontoApoio().Selecionar(origem, new[] { perto, longe }, pessoas: 20, precisaAcessivel: false);

        Assert.Equal("PA-LONGE", escolhido!.Ponto.Id);
        Assert.True(escolhido.CabeTodos);
    }

    [Fact]
    public void Seletor_prioriza_acessivel_e_ignora_lotado_ou_inativo()
    {
        var origem = new Coordenada(-23.45, -46.69);
        var naoAcessivel = Fabrica.Ponto("PA-1", acessivel: false, lat: -23.4501, lon: -46.6901);
        var acessivel = Fabrica.Ponto("PA-2", acessivel: true, lat: -23.46, lon: -46.70);
        var lotado = Fabrica.Ponto("PA-3", capacidade: 1, lat: -23.4500, lon: -46.6900);
        lotado.RegistrarEntrada(1);

        var seletor = new SeletorPontoApoio();
        Assert.Equal("PA-2", seletor.Selecionar(origem, new[] { naoAcessivel, acessivel, lotado }, 1, precisaAcessivel: true)!.Ponto.Id);
        Assert.Equal("PA-1", seletor.Selecionar(origem, new[] { naoAcessivel, acessivel, lotado }, 1, precisaAcessivel: false)!.Ponto.Id);
        Assert.Null(seletor.Selecionar(origem, new[] { lotado }, 1, false));
    }

    [Fact]
    public void Emissao_de_alerta_avisa_moradores_e_escala_sem_duplicar()
    {
        var ctx = Fabrica.ContextoComSetor("BRAS-S01", "BRAS-S02");
        ctx.PontosApoio.Adicionar(Fabrica.Ponto("PA-01"));
        ctx.Moradores.Adicionar(Fabrica.Morador("MOR-001", "BRAS-S01", CondicaoEspecial.Idoso));
        ctx.Moradores.Adicionar(Fabrica.Morador("MOR-002", "BRAS-S01"));
        ctx.Moradores.Adicionar(Fabrica.Morador("MOR-003", "BRAS-S02"));
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);
        var alertas = new ServicoAlertas(ctx, new SeletorPontoApoio(), () => T0);

        Assert.Throws<DominioException>(() => alertas.Emitir("BRAS-S01", "Op")); // ainda BAIXO

        mon.Registrar(Fabrica.Leitura(chuva: 12, quando: T0)); // MÉDIO
        var r1 = alertas.Emitir("BRAS-S01", "Op");
        Assert.Equal("ALT-0001", r1.Alerta.Id);
        Assert.Equal(2, r1.MoradoresAvisados);
        Assert.Equal("PA-01", r1.PontoApoio!.Ponto.Id);
        Assert.Contains("Ponto de apoio", r1.Alerta.Mensagem);
        Assert.Equal(SituacaoMorador.SemAlerta, ctx.Moradores.ObterPorId("MOR-003")!.Situacao);

        Assert.Throws<DominioException>(() => alertas.Emitir("BRAS-S01", "Op")); // já tem alerta MÉDIO

        mon.Registrar(Fabrica.Leitura(chuva: 25, quando: T0.AddMinutes(10))); // ALTO
        var r2 = alertas.Emitir("BRAS-S01", "Op");
        Assert.True(r2.FoiEscalado);
        Assert.Equal("ALT-0001", r2.Alerta.Id);
        Assert.Equal(1, ctx.Alertas.Quantidade);
        Assert.Equal(NivelRisco.Alto, r2.Alerta.Nivel);
    }

    [Fact]
    public void Encerrar_alerta_avisa_pendencias_e_libera_moradores()
    {
        var ctx = Fabrica.ContextoComSetor();
        ctx.Moradores.Adicionar(Fabrica.Morador("MOR-001", "BRAS-S01"));
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);
        var alertas = new ServicoAlertas(ctx, new SeletorPontoApoio(), () => T0);
        mon.Registrar(Fabrica.Leitura(chuva: 25, quando: T0));
        var alerta = alertas.Emitir("BRAS-S01", "Op").Alerta;
        alertas.MarcarPedidoAjuda("MOR-001");

        var aviso = alertas.AvisoAntesDeEncerrar(alerta.Id);
        Assert.Contains("ALTO", aviso);
        Assert.Contains("pediram ajuda", aviso);

        alertas.Encerrar(alerta.Id);
        Assert.False(alerta.Ativo);
        Assert.Equal(SituacaoMorador.SemAlerta, ctx.Moradores.ObterPorId("MOR-001")!.Situacao);
    }

    [Fact]
    public void Priorizador_coloca_pedido_de_ajuda_vulneravel_no_topo()
    {
        var alerta = "ALT-0001";
        var a = Fabrica.Morador("MOR-A", "BRAS-S01");                                   // alertado
        var b = Fabrica.Morador("MOR-B", "BRAS-S01", CondicaoEspecial.Acamado);          // vulnerável alertado
        var c = Fabrica.Morador("MOR-C", "BRAS-S01");                                   // pediu ajuda
        var d = Fabrica.Morador("MOR-D", "BRAS-S01", CondicaoEspecial.Idoso);            // vulnerável pediu ajuda
        var e = Fabrica.Morador("MOR-E", "BRAS-S01");                                   // seguro: fora da fila
        foreach (var m in new[] { a, b, c, d, e }) m.ReceberAlerta(alerta, T0);
        c.PedirAjuda(T0); d.PedirAjuda(T0); e.ConfirmarLocalSeguro(T0);

        var fila = PriorizadorMoradores.Priorizar(new[] { a, b, c, d, e },
            new Dictionary<string, NivelRisco> { ["BRAS-S01"] = NivelRisco.Alto });

        Assert.Equal(new[] { "MOR-D", "MOR-C", "MOR-B", "MOR-A" }, fila.Select(p => p.Morador.Id));
        Assert.Equal(150, fila[0].Pontuacao); // 100 ajuda + 30 vulnerável + 20 setor ALTO
    }

    [Fact]
    public void Indicadores_calculam_percentual_e_antecedencia()
    {
        var ctx = Fabrica.ContextoComSetor();
        for (var i = 1; i <= 5; i++) ctx.Moradores.Adicionar(Fabrica.Morador($"MOR-00{i}", "BRAS-S01"));
        var mon = new ServicoMonitoramento(ctx, new CalculadoraRisco(), () => T0);
        var alertas = new ServicoAlertas(ctx, new SeletorPontoApoio(), () => T0);

        mon.Registrar(Fabrica.Leitura(chuva: 12, nivel: 50, quando: T0));
        alertas.Emitir("BRAS-S01", "Op");
        mon.Registrar(Fabrica.Leitura(chuva: 25, nivel: 160, quando: T0.AddMinutes(150))); // pico 2h30 depois
        mon.Registrar(Fabrica.Leitura(chuva: 5, nivel: 60, quando: T0.AddMinutes(200)));
        foreach (var id in new[] { "MOR-001", "MOR-002", "MOR-003", "MOR-004" }) alertas.MarcarLocalSeguro(id);

        var ind = CalculadoraIndicadores.Calcular(ctx, T0.AddMinutes(210));

        Assert.Equal(5, ind.MoradoresAlertados);
        Assert.Equal(80, ind.PercentualSeguros);
        Assert.True(ind.MetaSegurosAtingida);
        Assert.Equal(150, ind.AntecedenciaMediaMin);
    }

    [Fact]
    public void Tendencia_detecta_corrego_subindo()
    {
        var leituras = new[]
        {
            Fabrica.Leitura(nivel: 20, quando: T0),
            Fabrica.Leitura(nivel: 22, quando: T0.AddMinutes(10)),
            Fabrica.Leitura(nivel: 21, quando: T0.AddMinutes(20)),
            Fabrica.Leitura(nivel: 40, quando: T0.AddMinutes(30)),
        };
        Assert.Equal(Tendencia.Subindo, AnalisadorTendencia.Analisar(leituras));
        Assert.Equal(Tendencia.Estavel, AnalisadorTendencia.Analisar(leituras.Take(1)));
    }

    [Fact]
    public void Proximo_codigo_nao_reaproveita_numeros()
    {
        var ctx = ContextoDados.EmMemoria();
        ctx.Ocorrencias.Adicionar(new Ocorrencia("OCO-0007", "BRAS-S01", TipoOcorrencia.Outro, CanalOcorrencia.Vistoria,
            "Rua de Teste, 1", "Descrição de teste longa", false, T0));
        Assert.Equal("OCO-0008", ContextoDados.ProximoCodigo(ctx.Ocorrencias, "OCO"));
        Assert.Equal("ALT-0001", ContextoDados.ProximoCodigo(ctx.Alertas, "ALT"));
    }

    [Fact]
    public void Simulador_gera_leituras_validas_e_tempestade_eleva_risco()
    {
        var setor = Fabrica.Setor();
        var sim = new SimuladorLeituras(new Random(1));
        var calc = new CalculadoraRisco();
        LeituraSensor? ultima = null;
        for (var i = 0; i < 6; i++) ultima = sim.Gerar(setor, ultima, CenarioChuva.Tempestade, T0.AddMinutes(i));

        Assert.Equal(NivelRisco.Alto, calc.Avaliar(ultima!).Nivel);
        Assert.Equal(OrigemLeitura.Simulada, ultima!.Origem);
    }
}
