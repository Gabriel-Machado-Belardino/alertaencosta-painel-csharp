using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Dialogos;

namespace AlertaEncosta.Painel.Telas;

/// <summary>Pontos de apoio (abrigos): capacidade, ocupação em tempo real e ranking por setor.</summary>
public partial class UcPontosApoio : UserControl, ITela
{
    private PainelContexto? _ctx;
    private bool _carregando;

    private sealed record LinhaPonto(string Id, string Nome, string Endereco, int Capacidade, int Ocupacao,
        int Vagas, string Lotacao, string Acessivel, string Situacao) : ILinha;

    public UcPontosApoio()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        var direita = DataGridViewContentAlignment.MiddleRight;
        Tema.Coluna(grade, nameof(LinhaPonto.Id), "Código", 80);
        Tema.Coluna(grade, nameof(LinhaPonto.Nome), "Nome", 300);
        Tema.Coluna(grade, nameof(LinhaPonto.Endereco), "Endereço", 280, preencher: true);
        Tema.Coluna(grade, nameof(LinhaPonto.Capacidade), "Capacidade", 100, alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaPonto.Ocupacao), "Abrigados", 95, alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaPonto.Vagas), "Vagas", 80, alinhamento: direita);
        Tema.Coluna(grade, nameof(LinhaPonto.Lotacao), "Lotação", 90, alinhamento: DataGridViewContentAlignment.MiddleCenter);
        Tema.Coluna(grade, nameof(LinhaPonto.Acessivel), "Acessível", 90, alinhamento: DataGridViewContentAlignment.MiddleCenter);
        Tema.Coluna(grade, nameof(LinhaPonto.Situacao), "Situação", 110);
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    public void Atualizar()
    {
        if (_ctx is null) return;
        var pontos = _ctx.Dados.PontosApoio.Listar();
        var linhas = pontos.OrderBy(p => p.Id).Select(p => new LinhaPonto(p.Id, p.Nome, p.Endereco, p.Capacidade, p.Ocupacao,
            p.VagasDisponiveis, $"{p.PercentualOcupacao:0}%", p.Acessivel ? "Sim ♿" : "Não",
            !p.Ativo ? "Desativado" : p.Lotado ? "LOTADO" : "Recebendo")).ToList();
        Ui.Preencher(grade, linhas);

        var ativos = pontos.Where(p => p.Ativo).ToList();
        lblResumo.Text = $"Total: {ativos.Sum(p => p.Ocupacao)} abrigados · {ativos.Sum(p => p.VagasDisponiveis)} vagas livres em {ativos.Count} ponto(s) ativos";

        _carregando = true;
        Ui.PreencherSetores(cmbSetorRanking, _ctx.Dados.Setores.Listar(), incluirTodos: false);
        _carregando = false;
        AtualizarRanking();
        AtualizarBotoes();
    }

    /// <summary>Mostra a ordem em que o <see cref="Core.Servicos.SeletorPontoApoio"/> escolheria os pontos para o setor.</summary>
    private void AtualizarRanking()
    {
        lstRanking.Items.Clear();
        var setorId = Ui.SetorSelecionado(cmbSetorRanking);
        var setor = setorId is null ? null : _ctx!.Dados.Setores.ObterPorId(setorId);
        if (setor is null) return;

        var moradores = _ctx!.Dados.Moradores.Listar().Where(m => m.SetorId == setor.Id).ToList();
        var precisaAcessivel = moradores.Any(m => m.EhVulneravel);
        var ranking = _ctx.Seletor.Ranquear(setor.Centro, _ctx.Dados.PontosApoio.Listar(), moradores.Count, precisaAcessivel);

        lstRanking.Items.Add($"{moradores.Count} morador(es) no setor{(precisaAcessivel ? ", com pessoas que precisam de local acessível" : "")}:");
        if (ranking.Count == 0)
            lstRanking.Items.Add("   ⚠ Nenhum ponto de apoio com vaga! Acione abrigo extra.");
        for (var i = 0; i < ranking.Count; i++)
            lstRanking.Items.Add($"   {i + 1}º  {ranking[i].Descricao}{(ranking[i].CabeTodos ? "" : "  (não cabem todos)")}");
    }

    private PontoApoio? Selecionado()
    {
        var id = Ui.IdSelecionado(grade);
        return id is null ? null : _ctx?.Dados.PontosApoio.ObterPorId(id);
    }

    private void AtualizarBotoes()
    {
        var p = Selecionado();
        btnEditar.Enabled = p is not null;
        btnEntrada.Enabled = p is { Ativo: true, Lotado: false };
        btnSaida.Enabled = p is { Ocupacao: > 0 };
    }

    private void grade_SelectionChanged(object? sender, EventArgs e) => AtualizarBotoes();

    private void grade_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEditar_Click(sender, e);
    }

    private void grade_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null) return;
        var nome = grade.Columns[e.ColumnIndex].Name;
        if (nome == nameof(LinhaPonto.Lotacao) && e.Value is string s && int.TryParse(s.TrimEnd('%'), out var pct))
        {
            e.CellStyle.BackColor = pct >= 100 ? Tema.Perigo : pct >= 80 ? Tema.Atencao : e.CellStyle.BackColor;
            e.CellStyle.ForeColor = pct >= 100 ? Color.White : Tema.TextoPrincipal;
        }
        else if (nome == nameof(LinhaPonto.Situacao) && e.Value is "LOTADO")
        {
            e.CellStyle.ForeColor = Tema.Perigo;
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
        }
    }

    private void cmbSetorRanking_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_carregando) AtualizarRanking();
    }

    private void btnNovo_Click(object? sender, EventArgs e)
    {
        using var dialogo = new FormPontoApoio(_ctx!, null);
        if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            _ctx!.Notificar($"Ponto de apoio {dialogo.Ponto?.Nome} cadastrado.");
    }

    private void btnEditar_Click(object? sender, EventArgs e)
    {
        var p = Selecionado();
        if (p is null) return;
        using var dialogo = new FormPontoApoio(_ctx!, p);
        if (dialogo.ShowDialog(FindForm()) == DialogResult.OK)
            _ctx!.Notificar($"Ponto de apoio {p.Nome} atualizado.");
    }

    private void btnEntrada_Click(object? sender, EventArgs e) => Movimentar(entrada: true);

    private void btnSaida_Click(object? sender, EventArgs e) => Movimentar(entrada: false);

    private void Movimentar(bool entrada)
    {
        var p = Selecionado();
        if (p is null) return;
        var pessoas = (int)numPessoas.Value;
        Ui.Executar(FindForm(), () =>
        {
            if (entrada) p.RegistrarEntrada(pessoas); else p.RegistrarSaida(pessoas);
            _ctx!.Dados.PontosApoio.Salvar();
            _ctx.Notificar($"{p.Nome}: {(entrada ? "chegaram" : "saíram")} {pessoas} pessoa(s). Vagas: {p.VagasDisponiveis}.");
        });
    }
}
