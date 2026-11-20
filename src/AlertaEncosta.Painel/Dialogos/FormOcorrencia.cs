using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Painel.Componentes;
using AlertaEncosta.Painel.Telas;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>Processo "Registrar ocorrência" (trinca, alagamento, deslizamento...).</summary>
public partial class FormOcorrencia : Form
{
    private readonly PainelContexto _ctx;

    public Ocorrencia? Ocorrencia { get; private set; }

    public FormOcorrencia(PainelContexto contexto)
    {
        _ctx = contexto;
        InitializeComponent();
        Tema.Aplicar(this);

        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar(), incluirTodos: false);
        foreach (var t in Enum.GetValues<TipoOcorrencia>())
            cmbTipo.Items.Add(new Ui.Opcao<TipoOcorrencia>(t, UcOcorrencias.DescreverTipo(t)));
        foreach (var c in Enum.GetValues<CanalOcorrencia>())
            cmbCanal.Items.Add(new Ui.Opcao<CanalOcorrencia>(c, UcOcorrencias.DescreverCanal(c)));
        cmbTipo.SelectedIndex = 0;
        cmbCanal.SelectedIndex = 0;
    }

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        var setor = Ui.SetorSelecionado(cmbSetor) ?? string.Empty;
        var tipo = ((Ui.Opcao<TipoOcorrencia>)cmbTipo.SelectedItem!).Valor;
        var canal = ((Ui.Opcao<CanalOcorrencia>)cmbCanal.SelectedItem!).Valor;

        var ok = Ui.Executar(this, () =>
        {
            Ocorrencia = new Ocorrencia(ContextoDados.ProximoCodigo(_ctx.Dados.Ocorrencias, "OCO"), setor, tipo, canal,
                txtLocal.Text, txtDescricao.Text, chkPessoasEmRisco.Checked, DateTime.Now);
            _ctx.Dados.Ocorrencias.Adicionar(Ocorrencia);
            _ctx.Dados.Ocorrencias.Salvar();
        });

        if (ok) DialogResult = DialogResult.OK;
    }
}
