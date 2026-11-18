using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>Fluxos de tela usados em mais de um lugar (faixa de aviso, visão geral e aba de alertas).</summary>
internal static class Acoes
{
    /// <summary>Fluxo "Emitir alerta de risco": confirma, emite e mostra a mensagem que vai para os moradores.</summary>
    public static void EmitirAlerta(IWin32Window? dono, PainelContexto ctx, string? setorId)
    {
        if (setorId is null)
        {
            Ui.Avisar(dono, "Selecione um setor primeiro.");
            return;
        }

        var setor = ctx.Dados.Setores.ObterPorId(setorId);
        if (setor is null) return;

        var moradores = ctx.Dados.Moradores.Listar().Count(m => m.SetorId == setor.Id);
        var ativo = ctx.Monitoramento.AlertaAtivo(setor.Id);
        var pergunta = ativo is null
            ? $"Emitir alerta {setor.RiscoAtual.Rotulo()} para o setor {setor.Nome}?\n\n{moradores} morador(es) cadastrados receberão a mensagem com o ponto de apoio indicado."
            : $"O setor {setor.Nome} já tem o alerta {ativo.Id} em {ativo.Nivel.Rotulo()}.\n\nEscalar para {setor.RiscoAtual.Rotulo()} e reenviar a mensagem?";

        if (setor.RiscoAtual > NivelRisco.Baixo && !Ui.Confirmar(dono, pergunta)) return;

        Ui.Executar(dono, () =>
        {
            var r = ctx.Alertas.Emitir(setor.Id, ctx.Operador);
            var ponto = r.PontoApoio is null
                ? "Nenhum ponto de apoio com vaga! Acione abrigo extra."
                : $"{r.PontoApoio.Ponto.Nome} ({r.PontoApoio.DistanciaKm:0.0} km, {r.PontoApoio.Ponto.VagasDisponiveis} vagas{(r.PontoApoio.CabeTodos ? "" : " - NÃO cabem todos")})";

            ctx.Notificar($"Alerta {r.Alerta.Id} {(r.FoiEscalado ? "escalado" : "emitido")} para {setor.Nome} ({r.Alerta.Nivel.Rotulo()}).");
            Ui.Informar(dono,
                $"Alerta {r.Alerta.Id} {(r.FoiEscalado ? "escalado" : "emitido")} - {r.MoradoresAvisados} morador(es) avisados.\n\n" +
                $"Ponto de apoio: {ponto}\n\nMensagem enviada:\n\"{r.Alerta.Mensagem}\"");
        });
    }

    /// <summary>Gera uma leitura simulada por setor e informa na barra de status.</summary>
    public static void GerarLeiturasSimuladas(IWin32Window? dono, PainelContexto ctx)
    {
        Ui.Executar(dono, () =>
        {
            var resumo = ctx.GerarLeiturasSimuladas();
            ctx.Notificar(DescreverResumo(resumo, $"Simulação ({PainelContexto.DescreverCenario(ctx.Cenario)})"));
        });
    }

    /// <summary>Fluxo "Coleta de leituras de sensores": abre o diálogo de importação e registra as leituras válidas.</summary>
    public static void ImportarLeituras(IWin32Window? dono, PainelContexto ctx)
    {
        using var dialogo = new Dialogos.FormImportarLeituras(ctx);
        if (dialogo.ShowDialog(dono) != DialogResult.OK) return;

        Ui.Executar(dono, () =>
        {
            var resumo = ctx.Monitoramento.RegistrarVarias(dialogo.Leituras);
            var texto = DescreverResumo(resumo, "Importação");
            ctx.Notificar(texto);
            if (resumo.Erros.Count > 0) texto += "\n\nIgnoradas:\n• " + string.Join("\n• ", resumo.Erros.Take(10));
            Ui.Informar(dono, texto);
        });
    }

    public static string DescreverResumo(ResumoImportacao resumo, string origem)
    {
        var subiram = resumo.Resultados.Where(r => r.RiscoSubiu).Select(r => $"{r.Setor.Id} → {r.RiscoAtual.Rotulo()}").ToList();
        var texto = $"{origem}: {resumo.Importadas} leitura(s) registradas";
        if (resumo.Ignoradas > 0) texto += $", {resumo.Ignoradas} ignorada(s)";
        if (subiram.Count > 0) texto += $". Risco subiu: {string.Join(", ", subiram)}";
        return texto + ".";
    }
}
