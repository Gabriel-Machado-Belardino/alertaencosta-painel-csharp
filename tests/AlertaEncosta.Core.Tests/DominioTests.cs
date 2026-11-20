using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Tests;

public class DominioTests
{
    private static readonly DateTime Agora = new(2026, 11, 10, 14, 0, 0);

    [Fact]
    public void Leitura_valida_valores_fora_da_faixa()
    {
        Assert.Throws<DominioException>(() => Fabrica.Leitura(solo: 120));
        Assert.Throws<DominioException>(() => Fabrica.Leitura(chuva: -1));
        Assert.Throws<DominioException>(() => Fabrica.Leitura(nivel: 999));
        Assert.Throws<DominioException>(() => new LeituraSensor("EST-1", "BRAS-S01", DateTime.Now.AddHours(2), 1, 1, 10, 10, OrigemLeitura.Manual));
    }

    [Fact]
    public void Leitura_nao_aceita_total_menor_que_janela()
    {
        var ex = Assert.Throws<DominioException>(() =>
            new LeituraSensor("EST-1", "BRAS-S01", Agora, chuvaMm: 10, chuvaTotalMm: 5, 50, 10, OrigemLeitura.Manual));
        Assert.Contains("chuva total", ex.Message);
    }

    [Fact]
    public void Ponto_de_apoio_nao_passa_da_capacidade()
    {
        var p = Fabrica.Ponto("PA-01", capacidade: 10);
        p.RegistrarEntrada(8);

        var ex = Assert.Throws<DominioException>(() => p.RegistrarEntrada(3));
        Assert.Contains("2 vaga", ex.Message);
        Assert.Equal(8, p.Ocupacao);

        p.RegistrarSaida(8);
        Assert.Equal(0, p.Ocupacao);
        Assert.Throws<DominioException>(() => p.RegistrarSaida(1));
    }

    [Fact]
    public void Ponto_de_apoio_nao_reduz_capacidade_abaixo_da_ocupacao()
    {
        var p = Fabrica.Ponto("PA-01", capacidade: 50);
        p.RegistrarEntrada(30);
        Assert.Throws<DominioException>(() => p.AlterarDados(p.Nome, p.Endereco, p.Local, 20, true, true));
        Assert.Throws<DominioException>(() => p.AlterarDados(p.Nome, p.Endereco, p.Local, 50, true, ativo: false));
    }

    [Fact]
    public void Morador_normaliza_e_mascara_telefone()
    {
        var m = new Morador("MOR-001", "Maria", "BRAS-S01", "Rua A, 10", "(11) 91234-5678", CondicaoEspecial.Idoso);
        Assert.Equal("11912345678", m.Telefone);
        Assert.Equal("(**) *****-5678", m.TelefoneMascarado);
        Assert.Throws<DominioException>(() => Morador.NormalizarTelefone("1234"));
    }

    [Fact]
    public void Morador_segue_fluxo_do_alerta()
    {
        var m = Fabrica.Morador("MOR-001", "BRAS-S01");
        Assert.Throws<DominioException>(() => m.ConfirmarLocalSeguro(Agora)); // sem alerta

        m.ReceberAlerta("ALT-0001", Agora);
        Assert.Equal(SituacaoMorador.Alertado, m.Situacao);

        m.PedirAjuda(Agora);
        Assert.Equal(SituacaoMorador.PrecisaAjuda, m.Situacao);

        m.ConfirmarLocalSeguro(Agora); // resgatado
        Assert.Throws<DominioException>(() => m.PedirAjuda(Agora));

        m.EncerrarAlerta(Agora);
        Assert.Equal(SituacaoMorador.SemAlerta, m.Situacao);
        Assert.Null(m.AlertaId);
    }

    [Fact]
    public void Edicao_invalida_nao_altera_nada()
    {
        var m = new Morador("MOR-001", "Maria", "BRAS-S01", "Rua A, 10", "11912345678", CondicaoEspecial.Nenhuma);
        Assert.Throws<DominioException>(() => m.AlterarCadastro("Novo Nome", "BRAS-S02", "Rua B, 20", "123", CondicaoEspecial.Idoso));
        Assert.Equal(("Maria", "BRAS-S01", CondicaoEspecial.Nenhuma), (m.Nome, m.SetorId, m.Condicao));

        var p = Fabrica.Ponto("PA-01", capacidade: 50);
        Assert.Throws<DominioException>(() => p.AlterarDados("Outro nome", "x", p.Local, 80, false, true)); // endereço curto
        Assert.Equal(("Ponto PA-01", 50), (p.Nome, p.Capacidade));
    }

    [Fact]
    public void Morador_em_alerta_nao_muda_de_setor()
    {
        var m = Fabrica.Morador("MOR-001", "BRAS-S01");
        m.ReceberAlerta("ALT-0001", Agora);
        Assert.Throws<DominioException>(() => m.AlterarCadastro(m.Nome, "BRAS-S02", m.Endereco, m.Telefone, m.Condicao));
    }

    [Fact]
    public void Ocorrencia_respeita_ordem_dos_status()
    {
        var o = new Ocorrencia("OCO-0001", "BRAS-S01", TipoOcorrencia.Trinca, CanalOcorrencia.AppMorador,
            "Rua do Morro, 10", "Trinca nova na parede da sala", false, Agora);

        Assert.Throws<DominioException>(() => o.Resolver("ok, resolvido", Agora)); // pulou etapa
        o.IniciarAtendimento("Equipe 1", Agora.AddMinutes(30));
        Assert.Throws<DominioException>(() => o.IniciarAtendimento("Equipe 2", Agora));
        o.Resolver("Vistoria feita, sem risco", Agora.AddHours(2));

        Assert.Equal(StatusOcorrencia.Resolvida, o.Status);
        Assert.Equal(30, o.TempoDeEspera(Agora.AddDays(1)).TotalMinutes);
    }

    [Fact]
    public void Alerta_nao_existe_em_risco_baixo_e_so_escala_para_cima()
    {
        Assert.Throws<DominioException>(() =>
            new Alerta("ALT-0001", "BRAS-S01", NivelRisco.Baixo, "Mensagem qualquer de teste", null, 1, "Op", Agora));

        var a = new Alerta("ALT-0001", "BRAS-S01", NivelRisco.Medio, "Mensagem qualquer de teste", null, 1, "Op", Agora);
        Assert.Throws<DominioException>(() => a.Escalar(NivelRisco.Medio, "Mensagem qualquer de teste", null, 1, Agora));
        a.Escalar(NivelRisco.Alto, "Mensagem nova de teste ALTO", null, 1, Agora.AddMinutes(5));
        Assert.Equal(NivelRisco.Alto, a.Nivel);

        a.Encerrar(Agora.AddHours(1));
        Assert.False(a.Ativo);
        Assert.Throws<DominioException>(() => a.Encerrar(Agora.AddHours(2)));
    }

    [Fact]
    public void Distancia_haversine_aproximada()
    {
        // Praça da Sé -> Av. Paulista/MASP: cerca de 2,6 km em linha reta
        var se = new Coordenada(-23.5503, -46.6339);
        var masp = new Coordenada(-23.5614, -46.6559);
        Assert.InRange(se.DistanciaKm(masp), 2.3, 2.8);
    }

    [Theory]
    [InlineData("MEDIO", NivelRisco.Medio)]
    [InlineData("médio", NivelRisco.Medio)]
    [InlineData(" alto ", NivelRisco.Alto)]
    [InlineData("BAIXO", NivelRisco.Baixo)]
    public void Converte_texto_da_estacao(string texto, NivelRisco esperado)
    {
        Assert.True(NivelRiscoExtensoes.TentarConverter(texto, out var n));
        Assert.Equal(esperado, n);
    }
}
