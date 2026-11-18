using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Dialogos;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>
/// Gera as imagens de evidência do README: executa um cenário de tempestade, emite os alertas,
/// simula as respostas do app e salva um PNG de cada aba e dos principais diálogos.
/// Uso: PainelDefesaCivil.exe --dados "%TEMP%\alertaencosta-demo" --capturar-telas docs\images
/// (use SEMPRE uma pasta de dados separada: o cenário altera os dados).
/// </summary>
internal static class CapturaDeTelas
{
    public static void Executar(FormPrincipal form, PainelContexto ctx, string pasta)
    {
        Directory.CreateDirectory(pasta);
        var escala = form.DeviceDpi / 96.0;
        form.WindowState = FormWindowState.Normal;
        form.Bounds = new Rectangle(0, 0, (int)(1440 * escala), (int)(900 * escala));
        var abas = form.Abas;

        void Salvar(Control c, string nome)
        {
            Application.DoEvents();
            using var bmp = new Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bmp, new Rectangle(Point.Empty, c.Size));
            bmp.Save(Path.Combine(pasta, nome), System.Drawing.Imaging.ImageFormat.Png);
        }

        abas.SelectedIndex = 0;
        Salvar(form, "01-visao-geral-normal.png");

        // Tempestade chegando: cada setor com uma intensidade, 4 leituras espaçadas de 20 s.
        var cenarios = new Dictionary<string, CenarioChuva>
        {
            ["BRAS-S01"] = CenarioChuva.Tempestade,
            ["BRAS-S02"] = CenarioChuva.ChuvaModerada,
            ["BRAS-S03"] = CenarioChuva.Tempestade,
            ["BRAS-S04"] = CenarioChuva.Estiagem,
        };
        var agora = DateTime.Now;
        for (var rodada = 4; rodada >= 1; rodada--)
        {
            var leituras = ctx.Dados.Setores.Listar().Select(s => ctx.Simulador.Gerar(s,
                ctx.Monitoramento.LeiturasDoSetor(s.Id).FirstOrDefault(),
                cenarios.GetValueOrDefault(s.Id, CenarioChuva.Estiagem), agora.AddSeconds(-20 * rodada)));
            ctx.Monitoramento.RegistrarVarias(leituras);
        }
        ctx.Notificar("Cenário de demonstração: tempestade sobre a Vila Brasilândia.");
        Salvar(form, "02-visao-geral-alerta-recomendado.png");

        // Operador emite os alertas recomendados; moradores respondem pelo app.
        var aleatorio = new Random(7);
        foreach (var setor in ctx.SetoresComAlertaRecomendado())
        {
            var r = ctx.Alertas.Emitir(setor.Id, ctx.Operador);
            ctx.Alertas.SimularRespostas(r.Alerta.Id, aleatorio);
        }
        var pa = ctx.Dados.PontosApoio.ObterPorId("PA-01");
        pa?.RegistrarEntrada(Math.Min(38, pa.VagasDisponiveis));
        var oco = new Ocorrencia(Core.Persistencia.ContextoDados.ProximoCodigo(ctx.Dados.Ocorrencias, "OCO"), "BRAS-S01",
            TipoOcorrencia.Deslizamento, CanalOcorrencia.Telefone199, "Escadaria Azul, altura do nº 80",
            "Barranco cedeu atrás de duas casas; moradores idosos sem conseguir sair.", true, DateTime.Now.AddMinutes(-12));
        ctx.Dados.Ocorrencias.Adicionar(oco);
        ctx.Dados.SalvarTudo();
        ctx.Notificar("Alertas emitidos e respostas do app recebidas.");

        abas.SelectedIndex = 0;
        Salvar(form, "03-visao-geral-com-alertas.png");
        string[] nomes = { "", "04-leituras.png", "05-alertas.png", "06-moradores.png", "07-pontos-de-apoio.png", "08-ocorrencias.png" };
        for (var i = 1; i < abas.TabCount; i++)
        {
            abas.SelectedIndex = i;
            Salvar(form, nomes[i]);
        }
        abas.SelectedIndex = 0;

        using (var leitura = new FormLeitura(ctx, "BRAS-S02"))
        {
            leitura.PreencherExemplo(chuva: 12, solo: 85, nivel: 40);
            leitura.Show(form);
            Salvar(leitura, "09-dialogo-leitura-manual.png");
            leitura.Close();
        }

        using (var importar = new FormImportarLeituras(ctx))
        {
            importar.ColarExemplo();
            importar.Show(form);
            Salvar(importar, "10-dialogo-importar-wokwi.png");
            importar.Close();
        }

        form.Close();
    }
}
