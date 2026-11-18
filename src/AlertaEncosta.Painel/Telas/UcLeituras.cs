using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Dialogos;

namespace AlertaEncosta.Painel.Telas;

/// <summary>Histórico de leituras dos sensores, com o risco recalculado pelo painel e o risco da estação.</summary>
public partial class UcLeituras : UserControl, ITela
{
    private const int MaximoNaGrade = 500;
    private PainelContexto? _ctx;
    private bool _carregando;

    private sealed record LinhaLeitura(string Id, DateTime DataHora, string Setor, string Estacao, double Chuva,
        double ChuvaTotal, int Solo, double Nivel, string Risco, string RiscoEstacao, string Origem) : ILinha;

    public UcLeituras()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        var direita = DataGridViewContentAlignment.MiddleRight;
        Tema.Coluna(grade, nameof(LinhaLeitura.DataHora), "Data/hora", 150, "dd/MM/yyyy HH:mm:ss");
        Tema.Coluna(grade, nameof(LinhaLeitura.Setor), "Setor", 100);
        Tema.Coluna(grade, nameof(LinhaLeitura.Estacao), "Estação", 110);
        Tema.Coluna(grade, nameof(LinhaLeitura.Chuva), "Chuva janela (mm)", 140, "0.0", alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaLeitura.ChuvaTotal), "Chuva total (mm)", 130, "0.0", alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaLeitura.Solo), "Solo (%)", 90, alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaLeitura.Nivel), "Córrego (cm)", 110, "0.0", alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaLeitura.Risco), "Risco (painel)", 120);
        Tema.Coluna(grade, nameof(LinhaLeitura.RiscoEstacao), "Risco (estação)", 120);
        Tema.Coluna(grade, nameof(LinhaLeitura.Origem), "Origem", 140, preencher: true);
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    /// <summary>Usado pela visão geral ("Ver leituras do setor").</summary>
    public void FiltrarSetor(string setorId)
    {
        Ui.SelecionarSetor(cmbSetor, setorId);
        Atualizar();
    }

    public void Atualizar()
    {
        if (_ctx is null) return;
        _carregando = true;
        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar(), incluirTodos: true);
        _carregando = false;

        var setor = Ui.SetorSelecionado(cmbSetor);
        var calc = _ctx.Monitoramento.Calculadora;
        var todas = _ctx.Dados.Leituras.Listar()
            .Where(l => setor is null || l.SetorId == setor)
            .OrderByDescending(l => l.DataHora)
            .ToList();

        var linhas = todas.Take(MaximoNaGrade).Select(l => new LinhaLeitura(l.Id, l.DataHora, l.SetorId, l.EstacaoId,
            l.ChuvaMm, l.ChuvaTotalMm, l.UmidadeSoloPct, l.NivelAguaCm, calc.Avaliar(l).Nivel.Rotulo(),
            l.RiscoEstacao?.Rotulo() ?? "—", DescreverOrigem(l.Origem))).ToList();

        Ui.Preencher(grade, linhas);
        lblTotal.Text = todas.Count > MaximoNaGrade
            ? $"{todas.Count} leituras (mostrando as {MaximoNaGrade} mais recentes)"
            : $"{todas.Count} leitura(s)";
        AtualizarDetalhe();
    }

    private void AtualizarDetalhe()
    {
        var id = Ui.IdSelecionado(grade);
        var leitura = id is null ? null : _ctx?.Dados.Leituras.ObterPorId(id);
        btnExcluir.Enabled = leitura is not null;
        if (leitura is null || _ctx is null)
        {
            lblDetalhe.Text = "Selecione uma leitura para ver como o risco foi calculado.";
            return;
        }

        var a = _ctx.Monitoramento.Calculadora.Avaliar(leitura);
        var divergencia = leitura.RiscoEstacao is { } re && re != a.Nivel
            ? $"  (a estação calculou {re.Rotulo()}: ela usa histerese local e a janela de chuva do firmware)"
            : string.Empty;
        lblDetalhe.Text = $"{leitura.SetorId} em {leitura.DataHora:dd/MM HH:mm:ss} → {a.Nivel.Rotulo()}{divergencia}\n{a.MotivosEmTexto}";
    }

    private static string DescreverOrigem(OrigemLeitura o) => o switch
    {
        OrigemLeitura.EstacaoEsp32 => "Estação ESP32",
        OrigemLeitura.Simulada => "Simulada (demo)",
        _ => "Manual (operador)"
    };

    private void cmbSetor_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_carregando) Atualizar();
    }

    private void grade_SelectionChanged(object? sender, EventArgs e) => AtualizarDetalhe();

    private void grade_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        var nome = grade.Columns[e.ColumnIndex].Name;
        if (nome is nameof(LinhaLeitura.Risco) or nameof(LinhaLeitura.RiscoEstacao)) Tema.PintarRisco(e);
    }

    private void btnNova_Click(object? sender, EventArgs e)
    {
        using var dialogo = new FormLeitura(_ctx!, Ui.SetorSelecionado(cmbSetor));
        if (dialogo.ShowDialog(FindForm()) != DialogResult.OK || dialogo.Leitura is null) return;

        Ui.Executar(FindForm(), () =>
        {
            var r = _ctx!.Monitoramento.Registrar(dialogo.Leitura);
            var texto = $"Leitura registrada em {r.Setor.Nome}: {r.Avaliacao.Nivel.Rotulo()}.";
            if (r.RiscoSubiu) texto += $" Risco do setor subiu de {r.RiscoAnterior.Rotulo()} para {r.RiscoAtual.Rotulo()}.";
            _ctx.Notificar(texto);
        });
    }

    private void btnImportar_Click(object? sender, EventArgs e) => Acoes.ImportarLeituras(FindForm(), _ctx!);

    private void btnSimular_Click(object? sender, EventArgs e) => Acoes.GerarLeiturasSimuladas(FindForm(), _ctx!);

    private void btnExcluir_Click(object? sender, EventArgs e)
    {
        var id = Ui.IdSelecionado(grade);
        if (id is null) return;
        if (!Ui.Confirmar(FindForm(), "Excluir esta leitura? Use apenas para corrigir leituras digitadas erradas.\nO risco do setor será recalculado.")) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Monitoramento.Excluir(id);
            _ctx.Notificar("Leitura excluída e setor recalculado.");
        });
    }
}
