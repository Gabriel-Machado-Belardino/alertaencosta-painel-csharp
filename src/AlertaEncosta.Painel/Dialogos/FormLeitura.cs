using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>
/// Leitura digitada pelo operador (ex.: medição de campo, estação fora do ar).
/// Enquanto o operador digita, a prévia mostra o risco calculado e o motivo.
/// </summary>
public partial class FormLeitura : Form
{
    private readonly PainelContexto _ctx;

    /// <summary>Leitura validada pelo domínio (preenchida ao clicar em Registrar).</summary>
    public LeituraSensor? Leitura { get; private set; }

    public FormLeitura(PainelContexto contexto, string? setorInicial)
    {
        _ctx = contexto;
        InitializeComponent();
        Tema.Aplicar(this);

        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar(), incluirTodos: false);
        if (setorInicial is not null) Ui.SelecionarSetor(cmbSetor, setorInicial);
        dtpDataHora.MaxDate = DateTime.Now.AddMinutes(5);
        dtpDataHora.Value = DateTime.Now;
        AtualizarEstacao();
        AtualizarPrevia();
    }

    private void AtualizarEstacao()
    {
        var id = Ui.SetorSelecionado(cmbSetor);
        txtEstacao.Text = id is null ? string.Empty : _ctx.Dados.Setores.ObterPorId(id)?.EstacaoId ?? string.Empty;
    }

    private void AtualizarPrevia()
    {
        var a = _ctx.Monitoramento.Calculadora.Avaliar((double)numChuva.Value, (int)numSolo.Value, (double)numNivel.Value);
        lblPreviaNivel.Text = $"Prévia: risco {a.Nivel.Rotulo()}";
        lblPreviaNivel.BackColor = Tema.CorRisco(a.Nivel);
        lblPreviaNivel.ForeColor = Tema.TextoSobreRisco(a.Nivel);
        lblPreviaMotivos.Text = $"{a.Nivel.Orientacao()}\n\n{a.MotivosEmTexto}";

        if (numChuvaTotal.Value < numChuva.Value) numChuvaTotal.Value = numChuva.Value;
    }

    private void cmbSetor_SelectedIndexChanged(object? sender, EventArgs e) => AtualizarEstacao();

    /// <summary>Usado pela captura de telas da documentação.</summary>
    internal void PreencherExemplo(double chuva, int solo, double nivel)
    {
        numChuva.Value = (decimal)chuva;
        numSolo.Value = solo;
        numNivel.Value = (decimal)nivel;
    }

    private void valor_Changed(object? sender, EventArgs e) => AtualizarPrevia();

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        var setorId = Ui.SetorSelecionado(cmbSetor);
        if (setorId is null)
        {
            Ui.Avisar(this, "Escolha o setor.");
            return;
        }

        // A validação é do domínio (LeituraSensor): a tela só traduz o erro para o operador.
        if (Ui.Executar(this, () => Leitura = new LeituraSensor(txtEstacao.Text, setorId, dtpDataHora.Value,
                (double)numChuva.Value, (double)numChuvaTotal.Value, (int)numSolo.Value, (double)numNivel.Value,
                OrigemLeitura.Manual)))
        {
            DialogResult = DialogResult.OK;
        }
    }
}
