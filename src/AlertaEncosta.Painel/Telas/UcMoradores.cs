using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Dialogos;

namespace AlertaEncosta.Painel.Telas;

/// <summary>Cadastro de moradores (simulado) e acompanhamento de quem confirmou estar seguro.</summary>
public partial class UcMoradores : UserControl, ITela
{
    private PainelContexto? _ctx;
    private bool _carregando;

    private sealed record LinhaMorador(string Id, string Prioridade, string Nome, string Setor, string Condicao,
        string Situacao, string Telefone, string Endereco, string Motivo) : ILinha;

    public UcMoradores()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        cmbFiltro.SelectedIndex = 0;
        Tema.Coluna(grade, nameof(LinhaMorador.Prioridade), "Prioridade", 90, alinhamento: DataGridViewContentAlignment.MiddleCenter);
        Tema.Coluna(grade, nameof(LinhaMorador.Id), "Código", 80);
        Tema.Coluna(grade, nameof(LinhaMorador.Nome), "Nome", 200);
        Tema.Coluna(grade, nameof(LinhaMorador.Setor), "Setor", 90);
        Tema.Coluna(grade, nameof(LinhaMorador.Condicao), "Condição especial", 160);
        Tema.Coluna(grade, nameof(LinhaMorador.Situacao), "Situação no alerta", 180);
        Tema.Coluna(grade, nameof(LinhaMorador.Telefone), "Telefone", 130);
        Tema.Coluna(grade, nameof(LinhaMorador.Endereco), "Endereço", 220, preencher: true);
        Tema.Coluna(grade, nameof(LinhaMorador.Motivo), "Motivo da prioridade", 220, preencher: true);
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    public void Atualizar()
    {
        if (_ctx is null) return;
        _carregando = true;
        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar(), incluirTodos: true);
        _carregando = false;

        var setor = Ui.SetorSelecionado(cmbSetor);
        var todos = _ctx.Dados.Moradores.Listar().Where(m => setor is null || m.SetorId == setor).ToList();
        var fila = PriorizadorMoradores.Priorizar(todos, _ctx.RiscoPorSetor()).ToDictionary(p => p.Morador.Id);

        IEnumerable<Morador> filtrados = cmbFiltro.SelectedIndex switch
        {
            1 => todos.Where(m => fila.ContainsKey(m.Id)),
            2 => todos.Where(m => m.Situacao == SituacaoMorador.PrecisaAjuda),
            3 => todos.Where(m => m.EhVulneravel),
            _ => todos
        };

        var linhas = filtrados
            .OrderByDescending(m => fila.TryGetValue(m.Id, out var p) ? p.Pontuacao : 0)
            .ThenBy(m => m.SetorId).ThenBy(m => m.Nome)
            .Select(m =>
            {
                fila.TryGetValue(m.Id, out var p);
                return new LinhaMorador(m.Id, p is null ? "—" : p.Pontuacao.ToString(), m.Nome, m.SetorId,
                    PriorizadorMoradores.DescreverCondicao(m.Condicao),
                    PriorizadorMoradores.DescreverSituacao(m.Situacao), m.TelefoneMascarado, m.Endereco, p?.Motivo ?? string.Empty);
            }).ToList();

        Ui.Preencher(grade, linhas);
        lblRodape.Text = $"{linhas.Count} de {todos.Count} morador(es) · {todos.Count(m => m.EhVulneravel)} com prioridade · " +
                         $"{fila.Count} na fila de busca ativa.   🔒 Dados simulados; telefones mascarados (LGPD: privacidade por padrão).";
        AtualizarBotoes();
    }

    private Morador? Selecionado()
    {
        var id = Ui.IdSelecionado(grade);
        return id is null ? null : _ctx?.Dados.Moradores.ObterPorId(id);
    }

    private void AtualizarBotoes()
    {
        var m = Selecionado();
        btnEditar.Enabled = btnExcluir.Enabled = m is not null;
        btnSeguro.Enabled = m?.AlertaId is not null && m.Situacao != SituacaoMorador.EmLocalSeguro;
        btnAjuda.Enabled = m?.AlertaId is not null && m.Situacao == SituacaoMorador.Alertado;
    }

    private void filtro_Changed(object? sender, EventArgs e)
    {
        if (!_carregando) Atualizar();
    }

    private void grade_SelectionChanged(object? sender, EventArgs e) => AtualizarBotoes();

    private void grade_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEditar_Click(sender, e);
    }

    private void grade_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null || grade.Columns[e.ColumnIndex].Name != nameof(LinhaMorador.Situacao) || e.Value is not string s) return;
        if (s.StartsWith("PRECISA")) { e.CellStyle.ForeColor = Tema.Perigo; e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold); }
        else if (s.StartsWith("Em local")) e.CellStyle.ForeColor = Tema.Sucesso;
        else if (s.StartsWith("Alertado")) e.CellStyle.ForeColor = Color.FromArgb(150, 90, 0);
    }

    private void btnNovo_Click(object? sender, EventArgs e)
    {
        using var dialogo = new FormMorador(_ctx!, null, Ui.SetorSelecionado(cmbSetor));
        if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            _ctx!.Notificar($"Morador {dialogo.Morador?.Nome} cadastrado.");
    }

    private void btnEditar_Click(object? sender, EventArgs e)
    {
        var m = Selecionado();
        if (m is null) return;
        using var dialogo = new FormMorador(_ctx!, m, null);
        if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            _ctx!.Notificar($"Cadastro de {m.Nome} atualizado.");
    }

    private void btnSeguro_Click(object? sender, EventArgs e)
    {
        var m = Selecionado();
        if (m is null) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Alertas.MarcarLocalSeguro(m.Id);
            _ctx.Notificar($"{m.Nome} está em local seguro.");
        });
    }

    private void btnAjuda_Click(object? sender, EventArgs e)
    {
        var m = Selecionado();
        if (m is null) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Alertas.MarcarPedidoAjuda(m.Id);
            _ctx.Notificar($"Pedido de ajuda registrado: {m.Nome}.");
        });
    }

    private void btnExcluir_Click(object? sender, EventArgs e)
    {
        var m = Selecionado();
        if (m is null) return;
        if (m.AlertaId is not null)
        {
            Ui.Avisar(FindForm(), "Este morador está em um alerta ativo. Encerre o alerta antes de excluir o cadastro.");
            return;
        }
        if (!Ui.Confirmar(FindForm(), $"Excluir o cadastro de {m.Nome}? (direito de eliminação de dados - LGPD)")) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Dados.Moradores.Remover(m.Id);
            _ctx.Dados.Moradores.Salvar();
            _ctx.Notificar($"Cadastro {m.Id} excluído.");
        });
    }
}
