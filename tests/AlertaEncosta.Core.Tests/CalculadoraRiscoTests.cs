using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Core.Tests;

public class CalculadoraRiscoTests
{
    private readonly CalculadoraRisco _calc = new();

    [Theory]
    [InlineData(0, 30, 10, NivelRisco.Baixo)]
    [InlineData(9.9, 59, 79.9, NivelRisco.Baixo)]
    [InlineData(10, 30, 10, NivelRisco.Medio)]   // chuva no limiar MÉDIO
    [InlineData(20, 30, 10, NivelRisco.Alto)]    // chuva no limiar ALTO
    [InlineData(0, 60, 10, NivelRisco.Medio)]    // solo encharcado sozinho = MÉDIO
    [InlineData(0, 100, 10, NivelRisco.Medio)]   // solo sozinho nunca vira ALTO
    [InlineData(0, 30, 80, NivelRisco.Medio)]    // nível MÉDIO
    [InlineData(0, 30, 140, NivelRisco.Alto)]    // nível ALTO
    public void Avalia_pelo_maior_sinal(double chuva, int solo, double nivel, NivelRisco esperado)
    {
        Assert.Equal(esperado, _calc.Avaliar(chuva, solo, nivel).Nivel);
    }

    [Fact]
    public void Regra_combinada_chuva_media_com_solo_saturado_vira_alto()
    {
        var a = _calc.Avaliar(chuvaMm: 11, umidadeSoloPct: 85, nivelAguaCm: 10);

        Assert.Equal(NivelRisco.Alto, a.Nivel);
        Assert.True(a.RegraCombinadaAtiva);
        Assert.Contains(a.Motivos, m => m.Contains("Regra combinada"));
    }

    [Fact]
    public void Solo_saturado_sem_chuva_nao_ativa_regra_combinada()
    {
        var a = _calc.Avaliar(chuvaMm: 2, umidadeSoloPct: 95, nivelAguaCm: 10);
        Assert.Equal(NivelRisco.Medio, a.Nivel);
        Assert.False(a.RegraCombinadaAtiva);
    }

    [Fact]
    public void Tudo_normal_nao_tem_motivos()
    {
        var a = _calc.Avaliar(0, 20, 5);
        Assert.Empty(a.Motivos);
        Assert.Equal("Todos os sinais dentro do normal.", a.MotivosEmTexto);
    }

    [Fact]
    public void Limiares_incoerentes_sao_recusados()
    {
        Assert.Throws<DominioException>(() => new CalculadoraRisco(new LimiaresRisco { ChuvaMedioMm = 30, ChuvaAltoMm = 20 }));
    }
}
