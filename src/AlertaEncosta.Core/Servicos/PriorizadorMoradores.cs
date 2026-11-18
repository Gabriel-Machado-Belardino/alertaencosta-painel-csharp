using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Servicos;

public sealed record PrioridadeMorador(Morador Morador, int Pontuacao, string Motivo);

/// <summary>
/// Fila de busca ativa da Defesa Civil: quem atender primeiro.
/// Pontuação (quanto maior, mais urgente):
///   pediu ajuda .......................... +100
///   alertado e ainda não respondeu ....... +40
///   pessoa vulnerável (idoso, PcD...) .... +30  (+10 se acamado ou mobilidade reduzida)
///   setor em ALTO / MÉDIO ................ +20 / +10
/// Quem já está em local seguro ou não tem alerta ativo não entra na fila.
/// Empate: quem está esperando há mais tempo vem primeiro.
/// </summary>
public static class PriorizadorMoradores
{
    public static IReadOnlyList<PrioridadeMorador> Priorizar(
        IEnumerable<Morador> moradores, IReadOnlyDictionary<string, NivelRisco> riscoPorSetor)
    {
        var fila = new List<PrioridadeMorador>();

        foreach (var m in moradores)
        {
            if (m.Situacao is SituacaoMorador.SemAlerta or SituacaoMorador.EmLocalSeguro)
                continue;

            var pontos = 0;
            var motivos = new List<string>();

            if (m.Situacao == SituacaoMorador.PrecisaAjuda) { pontos += 100; motivos.Add("pediu ajuda"); }
            else { pontos += 40; motivos.Add("sem resposta ao alerta"); }

            if (m.EhVulneravel)
            {
                pontos += 30;
                if (m.Condicao is CondicaoEspecial.Acamado or CondicaoEspecial.MobilidadeReduzida) pontos += 10;
                motivos.Add(DescreverCondicao(m.Condicao).ToLowerInvariant());
            }

            if (riscoPorSetor.TryGetValue(m.SetorId, out var risco) && risco > NivelRisco.Baixo)
            {
                pontos += risco == NivelRisco.Alto ? 20 : 10;
                motivos.Add($"setor em {risco.Rotulo()}");
            }

            fila.Add(new PrioridadeMorador(m, pontos, string.Join(", ", motivos)));
        }

        return fila
            .OrderByDescending(p => p.Pontuacao)
            .ThenBy(p => p.Morador.SituacaoAtualizadaEm ?? DateTime.MaxValue)
            .ThenBy(p => p.Morador.Nome, StringComparer.CurrentCulture)
            .ToList();
    }

    public static string DescreverCondicao(CondicaoEspecial c) => c switch
    {
        CondicaoEspecial.Nenhuma => "Nenhuma",
        CondicaoEspecial.Idoso => "Idoso(a)",
        CondicaoEspecial.MobilidadeReduzida => "Mobilidade reduzida",
        CondicaoEspecial.DeficienciaVisual => "Deficiência visual",
        CondicaoEspecial.DeficienciaAuditiva => "Deficiência auditiva",
        CondicaoEspecial.Gestante => "Gestante",
        CondicaoEspecial.Acamado => "Acamado(a)",
        _ => c.ToString()
    };

    public static string DescreverSituacao(SituacaoMorador s) => s switch
    {
        SituacaoMorador.SemAlerta => "Sem alerta",
        SituacaoMorador.Alertado => "Alertado (sem resposta)",
        SituacaoMorador.EmLocalSeguro => "Em local seguro",
        SituacaoMorador.PrecisaAjuda => "PRECISA DE AJUDA",
        _ => s.ToString()
    };
}
