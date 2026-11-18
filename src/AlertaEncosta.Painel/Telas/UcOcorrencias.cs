using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Dialogos;

namespace AlertaEncosta.Painel.Telas;

/// <summary>
/// Processo "Atender ocorrência": reportes de trincas, alagamentos e deslizamentos
/// (vindos do app do morador, do 199 ou do líder comunitário), ordenados por urgência.
/// </summary>
public partial class UcOcorrencias : UserControl, ITela
{
    private PainelContexto? _ctx;

    private sealed record LinhaOcorrencia(string Id, DateTime AbertaEm, string Setor, string Tipo, string Canal,
        string Local, string PessoasEmRisco, string Status, string Responsavel, string Espera) : ILinha;

    public UcOcorrencias()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        cmbStatus.SelectedIndex = 0;
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Id), "Código", 90);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.AbertaEm), "Aberta em", 130, "dd/MM HH:mm");
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Setor), "Setor", 90);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Tipo), "Tipo", 140);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Canal), "Canal", 140);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Local), "Local", 220, preencher: true);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.PessoasEmRisco), "Pessoas em risco", 130, alinhamento: DataGridViewContentAlignment.MiddleCenter);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Status), "Status", 130);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Responsavel), "Equipe", 130);
        Tema.Coluna(grade, nameof(LinhaOcorrencia.Espera), "Espera", 100);
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    public void Atualizar()
    {
        if (_ctx is null) return;
        var agora = DateTime.Now;
        var todas = _ctx.Dados.Ocorrencias.Listar();

        var filtradas = cmbStatus.SelectedIndex switch
        {
            1 => todas,
            2 => todas.Where(o => o.Status == StatusOcorrencia.Aberta),
            3 => todas.Where(o => o.Status == StatusOcorrencia.EmAtendimento),
            4 => todas.Where(o => o.Status == StatusOcorrencia.Resolvida),
            _ => todas.Where(o => o.Status != StatusOcorrencia.Resolvida)
        };

        // Urgência: pendentes primeiro, com pessoas em risco, risco do setor, e quem espera há mais tempo.
        var risco = _ctx.RiscoPorSetor();
        var linhas = filtradas
            .OrderBy(o => o.Status == StatusOcorrencia.Resolvida)
            .ThenByDescending(o => o.PessoasEmRisco)
            .ThenByDescending(o => risco.GetValueOrDefault(o.SetorId))
            .ThenBy(o => o.AbertaEm)
            .Select(o => new LinhaOcorrencia(o.Id, o.AbertaEm, o.SetorId, DescreverTipo(o.Tipo), DescreverCanal(o.Canal), o.Local,
                o.PessoasEmRisco ? "SIM" : "não", Ocorrencia.DescreverStatus(o.Status), o.Responsavel ?? "—",
                o.Status == StatusOcorrencia.Aberta ? Ui.Duracao(o.TempoDeEspera(agora)) : "—"))
            .ToList();

        Ui.Preencher(grade, linhas);
        lblResumo.Text = $"{todas.Count(o => o.Status == StatusOcorrencia.Aberta)} aberta(s) · " +
                         $"{todas.Count(o => o.Status == StatusOcorrencia.EmAtendimento)} em atendimento · " +
                         $"{todas.Count(o => o.Status == StatusOcorrencia.Resolvida)} resolvida(s)";
        AtualizarDetalhe();
    }

    public static string DescreverTipo(TipoOcorrencia t) => t switch
    {
        TipoOcorrencia.Trinca => "Trinca",
        TipoOcorrencia.Alagamento => "Alagamento",
        TipoOcorrencia.Deslizamento => "Deslizamento",
        TipoOcorrencia.QuedaDeArvore => "Queda de árvore",
        TipoOcorrencia.PedidoDeResgate => "Pedido de resgate",
        _ => "Outro"
    };

    public static string DescreverCanal(CanalOcorrencia c) => c switch
    {
        CanalOcorrencia.AppMorador => "App do morador",
        CanalOcorrencia.Telefone199 => "Telefone 199",
        CanalOcorrencia.LiderComunitario => "Líder comunitário",
        _ => "Vistoria"
    };

    private Ocorrencia? Selecionada()
    {
        var id = Ui.IdSelecionado(grade);
        return id is null ? null : _ctx?.Dados.Ocorrencias.ObterPorId(id);
    }

    private void AtualizarDetalhe()
    {
        var o = Selecionada();
        btnIniciar.Enabled = o?.Status == StatusOcorrencia.Aberta;
        btnResolver.Enabled = o?.Status == StatusOcorrencia.EmAtendimento;
        if (o is null)
        {
            txtDetalhe.Text = "Selecione uma ocorrência.";
            return;
        }

        var nl = Environment.NewLine;
        var texto = $"{o.Id} · {DescreverTipo(o.Tipo)} em {o.Local} ({_ctx!.NomeSetor(o.SetorId)}) · via {DescreverCanal(o.Canal)}{nl}{o.Descricao}";
        if (o.AtendimentoIniciadoEm is { } ini) texto += $"{nl}Equipe {o.Responsavel} enviada em {ini:dd/MM HH:mm} (espera de {Ui.Duracao(o.TempoDeEspera(DateTime.Now))}).";
        if (o.ResolvidaEm is { } fim) texto += $"{nl}Resolvida em {fim:dd/MM HH:mm}: {o.Parecer}";
        txtDetalhe.Text = texto;
    }

    private void cmbStatus_SelectedIndexChanged(object? sender, EventArgs e) => Atualizar();

    private void grade_SelectionChanged(object? sender, EventArgs e) => AtualizarDetalhe();

    private void grade_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null) return;
        var nome = grade.Columns[e.ColumnIndex].Name;
        if (nome == nameof(LinhaOcorrencia.PessoasEmRisco) && e.Value is "SIM")
        {
            e.CellStyle.BackColor = Tema.Perigo;
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
        }
        else if (nome == nameof(LinhaOcorrencia.Status) && e.Value is "aberta")
        {
            e.CellStyle.ForeColor = Tema.Perigo;
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
        }
    }

    private void btnNova_Click(object? sender, EventArgs e)
    {
        using var dialogo = new FormOcorrencia(_ctx!);
        if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            _ctx!.Notificar($"Ocorrência {dialogo.Ocorrencia?.Id} registrada.");
    }

    private void btnIniciar_Click(object? sender, EventArgs e)
    {
        var o = Selecionada();
        if (o is null) return;
        var equipe = FormEntradaTexto.Perguntar(FindForm(), "Enviar equipe", $"Qual equipe vai atender a ocorrência {o.Id}?", "Equipe Norte 1");
        if (equipe is null) return;
        Ui.Executar(FindForm(), () =>
        {
            o.IniciarAtendimento(equipe, DateTime.Now);
            _ctx!.Dados.Ocorrencias.Salvar();
            _ctx.Notificar($"{o.Id}: {equipe} a caminho.");
        });
    }

    private void btnResolver_Click(object? sender, EventArgs e)
    {
        var o = Selecionada();
        if (o is null) return;
        var parecer = FormEntradaTexto.Perguntar(FindForm(), "Resolver ocorrência", $"Parecer da vistoria de {o.Id} (o que foi feito?):", string.Empty, multilinha: true);
        if (parecer is null) return;
        Ui.Executar(FindForm(), () =>
        {
            o.Resolver(parecer, DateTime.Now);
            _ctx!.Dados.Ocorrencias.Salvar();
            _ctx.Notificar($"{o.Id} resolvida.");
        });
    }
}
