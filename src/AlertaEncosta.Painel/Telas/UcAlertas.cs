using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Telas;

/// <summary>Alertas emitidos: mensagem enviada, ponto de apoio e confirmações dos moradores.</summary>
public partial class UcAlertas : UserControl, ITela
{
    private PainelContexto? _ctx;
    private bool _carregando;
    private readonly Random _aleatorio = new();

    private sealed record LinhaAlerta(string Id, string Setor, string Nivel, DateTime EmitidoEm, string Duracao,
        string PontoApoio, int Alertados, int Seguros, int Ajuda, int SemResposta, string Status) : ILinha;

    public UcAlertas()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        var centro = DataGridViewContentAlignment.MiddleCenter;
        Tema.Coluna(grade, nameof(LinhaAlerta.Id), "Alerta", 90);
        Tema.Coluna(grade, nameof(LinhaAlerta.Setor), "Setor", 270);
        Tema.Coluna(grade, nameof(LinhaAlerta.Nivel), "Nível", 90);
        Tema.Coluna(grade, nameof(LinhaAlerta.EmitidoEm), "Emitido em", 140, "dd/MM HH:mm:ss");
        Tema.Coluna(grade, nameof(LinhaAlerta.Duracao), "Duração", 100);
        Tema.Coluna(grade, nameof(LinhaAlerta.PontoApoio), "Ponto de apoio indicado", 240, preencher: true);
        Tema.Coluna(grade, nameof(LinhaAlerta.Alertados), "Avisados", 85, alinhamento: centro);
        Tema.Coluna(grade, nameof(LinhaAlerta.Seguros), "Seguros", 80, alinhamento: centro);
        Tema.Coluna(grade, nameof(LinhaAlerta.Ajuda), "Ajuda", 70, alinhamento: centro);
        Tema.Coluna(grade, nameof(LinhaAlerta.SemResposta), "Sem resposta", 105, alinhamento: centro);
        Tema.Coluna(grade, nameof(LinhaAlerta.Status), "Status", 150);
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    public void Atualizar()
    {
        if (_ctx is null) return;
        _carregando = true;
        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar().OrderByDescending(s => s.RiscoAtual), incluirTodos: false);
        _carregando = false;

        var agora = DateTime.Now;
        var moradores = _ctx.Dados.Moradores.Listar();
        var linhas = _ctx.Dados.Alertas.Listar()
            .Where(a => !chkSomenteAtivos.Checked || a.Ativo)
            .OrderByDescending(a => a.Ativo).ThenByDescending(a => a.EmitidoEm)
            .Select(a =>
            {
                var doAlerta = moradores.Where(m => m.AlertaId == a.Id).ToList();
                var ponto = a.PontoApoioId is null ? "—" : _ctx.Dados.PontosApoio.ObterPorId(a.PontoApoioId)?.Nome ?? a.PontoApoioId;
                return new LinhaAlerta(a.Id, $"{a.SetorId} · {_ctx.NomeSetor(a.SetorId)}", a.Nivel.Rotulo(), a.EmitidoEm,
                    Ui.Duracao(a.Duracao(agora)), ponto, a.MoradoresAlertados,
                    doAlerta.Count(m => m.Situacao == SituacaoMorador.EmLocalSeguro),
                    doAlerta.Count(m => m.Situacao == SituacaoMorador.PrecisaAjuda),
                    doAlerta.Count(m => m.Situacao == SituacaoMorador.Alertado),
                    a.Ativo ? "ATIVO" : $"Encerrado {a.EncerradoEm:dd/MM HH:mm}");
            }).ToList();

        Ui.Preencher(grade, linhas);
        AtualizarBotoes();
        AtualizarDetalhe();
    }

    private Alerta? AlertaSelecionado()
    {
        var id = Ui.IdSelecionado(grade);
        return id is null ? null : _ctx?.Dados.Alertas.ObterPorId(id);
    }

    private void AtualizarBotoes()
    {
        var setorId = Ui.SetorSelecionado(cmbSetor);
        var setor = setorId is null ? null : _ctx!.Dados.Setores.ObterPorId(setorId);
        btnEmitir.Enabled = setor is not null && _ctx!.Monitoramento.AlertaRecomendado(setor);
        btnEmitir.Text = setor is not null && _ctx!.Monitoramento.AlertaAtivo(setor.Id) is not null
            ? "🔔 Escalar alerta" : "🔔 Emitir alerta";

        var alerta = AlertaSelecionado();
        btnEncerrar.Enabled = alerta?.Ativo == true;
        btnSimularRespostas.Enabled = alerta?.Ativo == true;
    }

    private void AtualizarDetalhe()
    {
        var alerta = AlertaSelecionado();
        if (alerta is null)
        {
            txtMensagem.Text = "Selecione um alerta para ver a mensagem enviada aos moradores.";
            lblPercentual.Text = "Confirmações";
            lblContagem.Text = string.Empty;
            barraSeguros.Valor = 0;
            return;
        }

        txtMensagem.Text = $"Mensagem enviada ({alerta.Nivel.Rotulo()}, por {alerta.Operador}):{Environment.NewLine}{alerta.Mensagem}";

        var doAlerta = _ctx!.Dados.Moradores.Listar().Where(m => m.AlertaId == alerta.Id).ToList();
        if (!alerta.Ativo || doAlerta.Count == 0)
        {
            lblPercentual.Text = alerta.Ativo ? "Nenhum morador cadastrado no setor" : "Alerta encerrado";
            lblPercentual.ForeColor = Tema.TextoSecundario;
            lblContagem.Text = alerta.Ativo ? string.Empty : $"Duração: {Ui.Duracao(alerta.Duracao(DateTime.Now))}";
            barraSeguros.Valor = 0;
            return;
        }

        var seguros = doAlerta.Count(m => m.Situacao == SituacaoMorador.EmLocalSeguro);
        var percentual = 100.0 * seguros / doAlerta.Count;
        barraSeguros.Valor = percentual;
        lblPercentual.Text = $"{percentual:0}% em local seguro (meta {Indicadores.MetaPercentualSeguros:0}%)";
        lblPercentual.ForeColor = percentual >= Indicadores.MetaPercentualSeguros ? Tema.Sucesso : Tema.Perigo;
        lblContagem.Text =
            $"{seguros} seguro(s) · {doAlerta.Count(m => m.Situacao == SituacaoMorador.PrecisaAjuda)} pediram ajuda · " +
            $"{doAlerta.Count(m => m.Situacao == SituacaoMorador.Alertado)} sem resposta{Environment.NewLine}" +
            "Sem resposta + pedidos de ajuda entram na fila de busca ativa (Visão geral).";
    }

    private void cmbSetor_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_carregando) AtualizarBotoes();
    }

    private void chkSomenteAtivos_CheckedChanged(object? sender, EventArgs e) => Atualizar();

    private void grade_SelectionChanged(object? sender, EventArgs e)
    {
        AtualizarBotoes();
        AtualizarDetalhe();
    }

    private void grade_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        var nome = grade.Columns[e.ColumnIndex].Name;
        if (nome == nameof(LinhaAlerta.Nivel)) Tema.PintarRisco(e);
        else if (nome == nameof(LinhaAlerta.Ajuda) && e.Value is int n && n > 0 && e.CellStyle is not null)
        {
            e.CellStyle.ForeColor = Tema.Perigo;
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
        }
        else if (nome == nameof(LinhaAlerta.Status) && e.Value is "ATIVO" && e.CellStyle is not null)
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
    }

    private void btnEmitir_Click(object? sender, EventArgs e) => Acoes.EmitirAlerta(FindForm(), _ctx!, Ui.SetorSelecionado(cmbSetor));

    private void btnSimularRespostas_Click(object? sender, EventArgs e)
    {
        var alerta = AlertaSelecionado();
        if (alerta is null) return;
        Ui.Executar(FindForm(), () =>
        {
            var (seguros, ajuda, sem) = _ctx!.Alertas.SimularRespostas(alerta.Id, _aleatorio);
            _ctx.Notificar($"Respostas simuladas do app para {alerta.Id}: {seguros} seguros, {ajuda} pedidos de ajuda, {sem} sem resposta.");
        });
    }

    private void btnEncerrar_Click(object? sender, EventArgs e)
    {
        var alerta = AlertaSelecionado();
        if (alerta is null) return;

        var aviso = _ctx!.Alertas.AvisoAntesDeEncerrar(alerta.Id);
        var pergunta = $"Encerrar o alerta {alerta.Id} ({_ctx.NomeSetor(alerta.SetorId)})?" +
                       (aviso is null ? string.Empty : $"\n\n{aviso}\n\nEncerrar mesmo assim?");
        if (!Ui.Confirmar(FindForm(), pergunta)) return;

        Ui.Executar(FindForm(), () =>
        {
            _ctx.Alertas.Encerrar(alerta.Id);
            _ctx.Notificar($"Alerta {alerta.Id} encerrado.");
        });
    }
}
