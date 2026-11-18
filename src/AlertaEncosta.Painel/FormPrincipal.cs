using System.Diagnostics;
using System.Reflection;
using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Telas;

namespace AlertaEncosta.Painel;

/// <summary>
/// Janela principal: cabeçalho, faixa de "alerta recomendado", abas e barra de status.
/// Coordena as telas (cada aba é um UserControl que implementa <see cref="ITela"/>).
/// </summary>
public partial class FormPrincipal : Form
{
    private readonly PainelContexto _ctx;
    private readonly ITela[] _telas;

    public static string Versao =>
        Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0] ?? "1.0.0";

    internal TabControl Abas => abas;

    public FormPrincipal(PainelContexto contexto)
    {
        _ctx = contexto;
        InitializeComponent();

        Text = $"{Ui.Titulo} · v{Versao}";
        try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch (ArgumentException) { /* sem ícone */ }
        _telas = new ITela[] { ucVisaoGeral, ucLeituras, ucAlertas, ucMoradores, ucPontosApoio, ucOcorrencias };
        foreach (var tela in _telas) tela.Inicializar(_ctx);

        _ctx.DadosAlterados += (_, _) => AtualizarTudo();
        _ctx.Mensagem += (_, mensagem) => lblStatus.Text = mensagem;
        ucVisaoGeral.AbrirLeiturasDoSetor += (_, setorId) =>
        {
            ucLeituras.FiltrarSetor(setorId);
            abas.SelectedTab = abaLeituras;
        };

        lblPasta.Text = $"Dados: {_ctx.Dados.Pasta}";
        AtualizarCenario();
        AtualizarRelogio();
        AtualizarTudo();
    }

    private void AtualizarTudo()
    {
        foreach (var tela in _telas) tela.Atualizar();
        AtualizarAviso();
        AtualizarTitulosDasAbas();
    }

    /// <summary>Faixa vermelha: setores em MÉDIO/ALTO sem alerta compatível (decisão fica com o operador).</summary>
    private void AtualizarAviso()
    {
        var setores = _ctx.SetoresComAlertaRecomendado();
        painelAviso.Visible = setores.Count > 0;
        if (setores.Count == 0) return;

        var lista = string.Join(" · ", setores.Select(s => $"{s.Nome} ({s.RiscoAtual.Rotulo()})"));
        lblAviso.Text = $"⚠  Alerta recomendado: {lista}";
        painelAviso.BackColor = setores.Any(s => s.RiscoAtual == NivelRisco.Alto) ? Tema.Perigo : Color.FromArgb(191, 110, 0);
        btnEmitirRecomendados.Text = setores.Count == 1 ? "Emitir alerta agora" : $"Emitir {setores.Count} alertas agora";
    }

    private void AtualizarTitulosDasAbas()
    {
        var alertas = _ctx.Dados.Alertas.Listar().Count(a => a.Ativo);
        var ajuda = _ctx.Dados.Moradores.Listar().Count(m => m.Situacao == SituacaoMorador.PrecisaAjuda);
        var ocorrencias = _ctx.Dados.Ocorrencias.Listar().Count(o => o.Status != StatusOcorrencia.Resolvida);

        abaAlertas.Text = alertas > 0 ? $"Alertas ({alertas})" : "Alertas";
        abaMoradores.Text = ajuda > 0 ? $"Moradores ({ajuda} ajuda)" : "Moradores";
        abaOcorrencias.Text = ocorrencias > 0 ? $"Ocorrências ({ocorrencias})" : "Ocorrências";
    }

    private void AtualizarRelogio() =>
        lblRelogio.Text = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}\nOperador: {_ctx.Operador}";

    private void AtualizarCenario()
    {
        itemCenarioEstiagem.Checked = _ctx.Cenario == CenarioChuva.Estiagem;
        itemCenarioModerada.Checked = _ctx.Cenario == CenarioChuva.ChuvaModerada;
        itemCenarioTempestade.Checked = _ctx.Cenario == CenarioChuva.Tempestade;
        lblDemo.Text = timerDemo.Enabled
            ? $"● Simulação automática: {PainelContexto.DescreverCenario(_ctx.Cenario)}"
            : $"Simulação desligada (cenário: {PainelContexto.DescreverCenario(_ctx.Cenario)})";
        lblDemo.ForeColor = timerDemo.Enabled ? Tema.Perigo : Tema.TextoSecundario;
    }

    // ---------------- Eventos ----------------

    private void timerRelogio_Tick(object? sender, EventArgs e) => AtualizarRelogio();

    private void timerDemo_Tick(object? sender, EventArgs e)
    {
        try
        {
            var resumo = _ctx.GerarLeiturasSimuladas();
            _ctx.Notificar(Acoes.DescreverResumo(resumo, "Simulação automática"));
        }
        catch (Exception ex)
        {
            timerDemo.Stop();
            itemModoDemo.Checked = false;
            Ui.MostrarErro(this, ex);
        }
    }

    private void btnEmitirRecomendados_Click(object? sender, EventArgs e)
    {
        var setores = _ctx.SetoresComAlertaRecomendado();
        if (setores.Count == 1)
        {
            Acoes.EmitirAlerta(this, _ctx, setores[0].Id);
            return;
        }

        var lista = string.Join("\n", setores.Select(s => $"• {s.Nome} ({s.RiscoAtual.Rotulo()})"));
        if (!Ui.Confirmar(this, $"Emitir/escalar alerta para {setores.Count} setores?\n\n{lista}")) return;

        var resultados = new List<string>();
        foreach (var setor in setores)
            Ui.Executar(this, () =>
            {
                var r = _ctx.Alertas.Emitir(setor.Id, _ctx.Operador);
                resultados.Add($"• {r.Alerta.Id} {setor.Nome}: {r.Alerta.Nivel.Rotulo()}, {r.MoradoresAvisados} avisados, " +
                               $"ponto de apoio {r.PontoApoio?.Ponto.Nome ?? "NENHUM COM VAGA"}");
            });

        _ctx.Notificar($"{resultados.Count} alerta(s) emitidos/escalados.");
        if (resultados.Count > 0) Ui.Informar(this, string.Join("\n", resultados));
    }

    private void itemImportar_Click(object? sender, EventArgs e) => Acoes.ImportarLeituras(this, _ctx);

    private void itemGerarAgora_Click(object? sender, EventArgs e) => Acoes.GerarLeiturasSimuladas(this, _ctx);

    private void itemModoDemo_CheckedChanged(object? sender, EventArgs e)
    {
        timerDemo.Enabled = itemModoDemo.Checked;
        AtualizarCenario();
        _ctx.Notificar(timerDemo.Enabled ? "Modo demonstração ligado." : "Modo demonstração desligado.");
    }

    private void itemCenario_Click(object? sender, EventArgs e)
    {
        _ctx.Cenario = sender == itemCenarioEstiagem ? CenarioChuva.Estiagem
                     : sender == itemCenarioModerada ? CenarioChuva.ChuvaModerada
                     : CenarioChuva.Tempestade;
        AtualizarCenario();
    }

    private void itemAbrirPasta_Click(object? sender, EventArgs e)
    {
        if (_ctx.Dados.Pasta is null) return;
        Ui.Executar(this, () => Process.Start(new ProcessStartInfo("explorer.exe", $"\"{_ctx.Dados.Pasta}\"") { UseShellExecute = true }));
    }

    private void itemRestaurar_Click(object? sender, EventArgs e)
    {
        if (_ctx.Dados.Pasta is null) return;
        if (!Ui.Confirmar(this, "Apagar todos os dados do painel e voltar para os dados de exemplo?\n\nO painel será reiniciado.")) return;

        foreach (var arquivo in Directory.GetFiles(_ctx.Dados.Pasta, "*.json"))
            File.Delete(arquivo);
        Application.Restart();
        Environment.Exit(0);
    }

    private void itemSair_Click(object? sender, EventArgs e) => Close();

    private void itemSobre_Click(object? sender, EventArgs e) => Ui.Informar(this,
        $"AlertaEncosta · Painel da Defesa Civil · v{Versao}\n\n" +
        "Ferramenta do agente da Defesa Civil para monitorar os sensores do bairro, emitir alertas com rota " +
        "para o ponto de apoio, acompanhar quem confirmou estar seguro e atender ocorrências.\n\n" +
        "FIAP · Global Solution 2026.2 · CitizenTech · C# Software Development\n\n" +
        "Gustavo Neri Santos (RM560239)\nMatheus Aparecido Rocha Plati (RM559813)\nGustavo Pandolfo Meroni (RM560271)\n" +
        "Gabriel Machado Belardino (RM550121)\nGustavo Franco Tárano (RM559616)\n\n" +
        "Todos os dados são simulados (LGPD).");

    private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
    {
        timerDemo.Stop();
        try { _ctx.Dados.SalvarTudo(); }
        catch (Exception ex) { Ui.MostrarErro(this, ex); }
    }
}
