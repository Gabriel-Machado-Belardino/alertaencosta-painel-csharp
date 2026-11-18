using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Telas;

/// <summary>
/// Visão geral: indicadores de impacto, mapa de setores (blocos coloridos pelo risco)
/// e a fila de busca ativa calculada pelo <see cref="PriorizadorMoradores"/>.
/// </summary>
public partial class UcVisaoGeral : UserControl, ITela
{
    private PainelContexto? _ctx;
    private string? _setorSelecionado;
    private readonly Dictionary<string, BlocoSetor> _blocos = new();

    /// <summary>Pede à janela principal para abrir a aba de leituras filtrada pelo setor.</summary>
    public event EventHandler<string>? AbrirLeiturasDoSetor;

    private sealed record LinhaFila(string Id, int Pontos, string Nome, string Setor, string Situacao, string Motivo) : ILinha;

    public UcVisaoGeral()
    {
        InitializeComponent();
        Tema.Aplicar(this);
        gradeFila.Font = Tema.Fonte(10F);
        Tema.Coluna(gradeFila, nameof(LinhaFila.Pontos), "Pts", 48, alinhamento: DataGridViewContentAlignment.MiddleCenter);
        Tema.Coluna(gradeFila, nameof(LinhaFila.Nome), "Morador", 170, preencher: true).FillWeight = 110;
        Tema.Coluna(gradeFila, nameof(LinhaFila.Setor), "Setor", 84);
        Tema.Coluna(gradeFila, nameof(LinhaFila.Situacao), "Situação", 118);
        Tema.Coluna(gradeFila, nameof(LinhaFila.Motivo), "Por quê", 200, preencher: true).FillWeight = 140;
    }

    public void Inicializar(PainelContexto contexto) => _ctx = contexto;

    public void Atualizar()
    {
        if (_ctx is null) return;
        AtualizarIndicadores();
        AtualizarSetores();
        AtualizarFila();
    }

    private void AtualizarIndicadores()
    {
        var ind = CalculadoraIndicadores.Calcular(_ctx!.Dados, DateTime.Now);
        var totalSetores = _ctx.Dados.Setores.Quantidade;

        cartaoSetores.Definir(
            ind.SetoresEmAlto > 0 ? $"{ind.SetoresEmAlto} em ALTO" : ind.SetoresEmMedio > 0 ? $"{ind.SetoresEmMedio} em MÉDIO" : "Nenhum",
            $"{ind.SetoresEmAlto} alto · {ind.SetoresEmMedio} médio · {totalSetores} setores",
            ind.SetoresEmAlto > 0 ? Tema.Perigo : ind.SetoresEmMedio > 0 ? Tema.Atencao : Tema.Sucesso);

        cartaoAlertas.Definir(ind.AlertasAtivos.ToString(),
            $"{ind.MoradoresAlertados} moradores avisados",
            ind.AlertasAtivos > 0 ? Tema.Atencao : Tema.Sucesso);

        if (ind.MoradoresAlertados == 0)
            cartaoSeguros.Definir("—", "sem alerta ativo · meta 80%", Tema.Sucesso);
        else
            cartaoSeguros.Definir($"{ind.PercentualSeguros:0}%",
                $"{ind.MoradoresEmLocalSeguro} de {ind.MoradoresAlertados} confirmaram · meta 80%",
                ind.MetaSegurosAtingida ? Tema.Sucesso : Tema.Atencao);

        cartaoAjuda.Definir(ind.PedidosDeAjuda.ToString(),
            $"{ind.VulneraveisSemConfirmacao} prioritário(s) sem confirmação",
            ind.PedidosDeAjuda > 0 ? Tema.Perigo : ind.VulneraveisSemConfirmacao > 0 ? Tema.Atencao : Tema.Sucesso);

        cartaoAntecedencia.Definir(
            ind.AntecedenciaMediaMin is { } min ? Ui.Duracao(TimeSpan.FromMinutes(min)) : "—",
            "alerta → pico do córrego · meta ≥ 2 h",
            ind.AntecedenciaMediaMin is null || ind.AntecedenciaMediaMin >= Indicadores.MetaAntecedenciaMin ? Tema.Sucesso : Tema.Atencao);

        cartaoOcorrencias.Definir(ind.OcorrenciasAbertas.ToString(),
            ind.TempoMedioEsperaOcorrenciaMin is { } espera ? $"espera média {Ui.Duracao(TimeSpan.FromMinutes(espera))}" : "sem ocorrências",
            ind.OcorrenciasAbertas > 0 ? Tema.Atencao : Tema.Sucesso);
    }

    private void AtualizarSetores()
    {
        var resumos = _ctx!.Monitoramento.Resumos();
        var limiares = _ctx.Monitoramento.Calculadora.Limiares;

        // Recria os blocos só quando a lista de setores muda (evita piscar a tela).
        var ids = resumos.Select(r => r.Setor.Id).ToList();
        if (!ids.SequenceEqual(_blocos.Keys))
        {
            painelSetores.SuspendLayout();
            painelSetores.Controls.Clear();
            foreach (var bloco in _blocos.Values) bloco.Dispose();
            _blocos.Clear();
            foreach (var id in ids)
            {
                var bloco = new BlocoSetor();
                bloco.Click += (_, _) => SelecionarSetor(id);
                bloco.DoubleClick += (_, _) => AbrirLeiturasDoSetor?.Invoke(this, id);
                _blocos[id] = bloco;
                painelSetores.Controls.Add(bloco);
            }
            painelSetores.ResumeLayout();
        }

        foreach (var r in resumos) _blocos[r.Setor.Id].Definir(r, limiares);

        if (_setorSelecionado is null || !_blocos.ContainsKey(_setorSelecionado))
            _setorSelecionado = ids.FirstOrDefault();
        SelecionarSetor(_setorSelecionado);
    }

    private void SelecionarSetor(string? setorId)
    {
        _setorSelecionado = setorId;
        foreach (var (id, bloco) in _blocos) bloco.Selecionado = id == setorId;

        var resumo = setorId is null ? null : _blocos.GetValueOrDefault(setorId)?.Resumo;
        btnEmitirAlerta.Enabled = resumo is not null && _ctx!.Monitoramento.AlertaRecomendado(resumo.Setor);
        btnVerLeituras.Enabled = resumo is not null;

        if (resumo is null)
        {
            lblDetalhe.Text = "Selecione um setor.";
            return;
        }

        var s = resumo.Setor;
        var motivos = resumo.Avaliacao?.MotivosEmTexto ?? "Sem leituras.";
        var alerta = resumo.AlertaAtivo is { } a ? $" Alerta {a.Id} ativo desde {a.EmitidoEm:HH:mm}." : string.Empty;
        lblDetalhe.Text = $"{s.Nome} ({s.Id}) · risco {s.RiscoAtual.Rotulo()}: {s.RiscoAtual.Orientacao()}{alerta}\n{motivos}";
        lblDetalhe.ForeColor = s.RiscoAtual == NivelRisco.Alto ? Tema.Perigo : Tema.TextoPrincipal;
    }

    private void AtualizarFila()
    {
        var fila = PriorizadorMoradores.Priorizar(_ctx!.Dados.Moradores.Listar(), _ctx.RiscoPorSetor());
        var linhas = fila.Select(p => new LinhaFila(p.Morador.Id, p.Pontuacao, p.Morador.Nome, p.Morador.SetorId,
            p.Morador.Situacao == SituacaoMorador.PrecisaAjuda ? "PEDIU AJUDA" : "Sem resposta", p.Motivo)).ToList();
        Ui.Preencher(gradeFila, linhas);
        grupoFila.Text = linhas.Count == 0
            ? "Fila de busca ativa: ninguém aguardando"
            : $"Fila de busca ativa: {linhas.Count} pessoa(s) — atender de cima para baixo";
        btnFilaSeguro.Enabled = btnFilaAjuda.Enabled = linhas.Count > 0;
    }

    private void gradeFila_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null || gradeFila.Columns[e.ColumnIndex].Name != nameof(LinhaFila.Situacao)) return;
        if (e.Value is "PEDIU AJUDA")
        {
            e.CellStyle.ForeColor = Tema.Perigo;
            e.CellStyle.Font = Tema.Fonte(10F, FontStyle.Bold);
        }
    }

    private void btnEmitirAlerta_Click(object? sender, EventArgs e) => Acoes.EmitirAlerta(FindForm(), _ctx!, _setorSelecionado);

    private void btnVerLeituras_Click(object? sender, EventArgs e)
    {
        if (_setorSelecionado is not null) AbrirLeiturasDoSetor?.Invoke(this, _setorSelecionado);
    }

    private void btnGerarLeitura_Click(object? sender, EventArgs e) => Acoes.GerarLeiturasSimuladas(FindForm(), _ctx!);

    private void btnFilaSeguro_Click(object? sender, EventArgs e)
    {
        var id = Ui.IdSelecionado(gradeFila);
        if (id is null) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Alertas.MarcarLocalSeguro(id);
            _ctx.Notificar($"{_ctx.Dados.Moradores.ObterPorId(id)?.Nome} marcado(a) como em local seguro.");
        });
    }

    private void btnFilaAjuda_Click(object? sender, EventArgs e)
    {
        var id = Ui.IdSelecionado(gradeFila);
        if (id is null) return;
        Ui.Executar(FindForm(), () =>
        {
            _ctx!.Alertas.MarcarPedidoAjuda(id);
            _ctx.Notificar($"Pedido de ajuda registrado para {_ctx.Dados.Moradores.ObterPorId(id)?.Nome}.");
        });
    }
}
