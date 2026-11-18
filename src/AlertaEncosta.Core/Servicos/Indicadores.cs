using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;

namespace AlertaEncosta.Core.Servicos;

/// <summary>Indicadores de impacto do AlertaEncosta (Ficha da CitizenTech).</summary>
public sealed record Indicadores(
    int SetoresEmAlto,
    int SetoresEmMedio,
    int AlertasAtivos,
    int MoradoresAlertados,
    int MoradoresEmLocalSeguro,
    int PedidosDeAjuda,
    int VulneraveisSemConfirmacao,
    int OcorrenciasAbertas,
    double? AntecedenciaMediaMin,
    double? TempoMedioEsperaOcorrenciaMin)
{
    /// <summary>Meta do piloto: 80% dos alertados confirmam que estão em local seguro.</summary>
    public const double MetaPercentualSeguros = 80;

    /// <summary>Meta do piloto: alerta pelo menos 2 h (120 min) antes do pico.</summary>
    public const double MetaAntecedenciaMin = 120;

    public double PercentualSeguros => MoradoresAlertados == 0 ? 0 : 100.0 * MoradoresEmLocalSeguro / MoradoresAlertados;
    public bool MetaSegurosAtingida => MoradoresAlertados > 0 && PercentualSeguros >= MetaPercentualSeguros;
}

public static class CalculadoraIndicadores
{
    /// <summary>Janela máxima depois do alerta em que procuramos o pico do evento.</summary>
    public static readonly TimeSpan JanelaPico = TimeSpan.FromHours(12);

    public static Indicadores Calcular(ContextoDados dados, DateTime agora)
    {
        var setores = dados.Setores.Listar();
        var alertas = dados.Alertas.Listar();
        var moradores = dados.Moradores.Listar();
        var ocorrencias = dados.Ocorrencias.Listar();

        var alertados = moradores.Where(m => m.AlertaId is not null).ToList();
        var esperas = ocorrencias.Select(o => o.TempoDeEspera(agora).TotalMinutes).ToList();

        return new Indicadores(
            SetoresEmAlto: setores.Count(s => s.RiscoAtual == NivelRisco.Alto),
            SetoresEmMedio: setores.Count(s => s.RiscoAtual == NivelRisco.Medio),
            AlertasAtivos: alertas.Count(a => a.Ativo),
            MoradoresAlertados: alertados.Count,
            MoradoresEmLocalSeguro: alertados.Count(m => m.Situacao == SituacaoMorador.EmLocalSeguro),
            PedidosDeAjuda: alertados.Count(m => m.Situacao == SituacaoMorador.PrecisaAjuda),
            VulneraveisSemConfirmacao: alertados.Count(m => m.EhVulneravel && m.Situacao != SituacaoMorador.EmLocalSeguro),
            OcorrenciasAbertas: ocorrencias.Count(o => o.Status != StatusOcorrencia.Resolvida),
            AntecedenciaMediaMin: AntecedenciaMedia(alertas, dados.Leituras.Listar(), agora),
            TempoMedioEsperaOcorrenciaMin: esperas.Count == 0 ? null : esperas.Average());
    }

    /// <summary>
    /// Antecedência = minutos entre a emissão do alerta e o PICO do evento no setor
    /// (a leitura com maior nível do córrego; empate pela chuva) dentro da janela do alerta.
    /// Quanto maior, mais tempo o morador teve para sair. Retorna null se não houver dados.
    /// </summary>
    public static double? AntecedenciaMedia(IEnumerable<Alerta> alertas, IEnumerable<LeituraSensor> leituras, DateTime agora)
    {
        var todas = leituras.ToList();
        var valores = new List<double>();

        foreach (var alerta in alertas)
        {
            var fim = alerta.EncerradoEm ?? agora;
            if (fim - alerta.EmitidoEm > JanelaPico) fim = alerta.EmitidoEm + JanelaPico;

            var pico = todas
                .Where(l => l.SetorId == alerta.SetorId && l.DataHora >= alerta.EmitidoEm && l.DataHora <= fim)
                .OrderByDescending(l => l.NivelAguaCm)
                .ThenByDescending(l => l.ChuvaMm)
                .FirstOrDefault();

            if (pico is not null)
                valores.Add((pico.DataHora - alerta.EmitidoEm).TotalMinutes);
        }

        return valores.Count == 0 ? null : valores.Average();
    }
}
