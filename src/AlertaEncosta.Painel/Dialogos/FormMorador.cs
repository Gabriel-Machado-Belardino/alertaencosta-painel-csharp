using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Core.Servicos;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>Processo "Cadastrar morador e endereço": inclusão e edição.</summary>
public partial class FormMorador : Form
{
    private readonly PainelContexto _ctx;
    private readonly Morador? _existente;

    public Morador? Morador { get; private set; }

    public FormMorador(PainelContexto contexto, Morador? existente, string? setorInicial)
    {
        _ctx = contexto;
        _existente = existente;
        InitializeComponent();
        Tema.Aplicar(this);

        Ui.PreencherSetores(cmbSetor, _ctx.Dados.Setores.Listar(), incluirTodos: false);
        foreach (var c in Enum.GetValues<CondicaoEspecial>())
            cmbCondicao.Items.Add(new Ui.Opcao<CondicaoEspecial>(c, PriorizadorMoradores.DescreverCondicao(c)));
        cmbCondicao.SelectedIndex = 0;

        if (existente is null)
        {
            Text = "Cadastrar morador";
            if (setorInicial is not null) Ui.SelecionarSetor(cmbSetor, setorInicial);
            return;
        }

        Text = $"Editar morador {existente.Id}";
        txtNome.Text = existente.Nome;
        Ui.SelecionarSetor(cmbSetor, existente.SetorId);
        cmbSetor.Enabled = existente.AlertaId is null; // não troca de setor durante um alerta
        txtEndereco.Text = existente.Endereco;
        txtTelefone.Text = existente.Telefone;
        cmbCondicao.SelectedIndex = (int)existente.Condicao;
    }

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        var setor = Ui.SetorSelecionado(cmbSetor) ?? string.Empty;
        var condicao = (cmbCondicao.SelectedItem as Ui.Opcao<CondicaoEspecial>)?.Valor ?? CondicaoEspecial.Nenhuma;

        var ok = Ui.Executar(this, () =>
        {
            if (_existente is null)
            {
                Morador = new Morador(ContextoDados.ProximoCodigo(_ctx.Dados.Moradores, "MOR", 3),
                    txtNome.Text, setor, txtEndereco.Text, txtTelefone.Text, condicao);
                _ctx.Dados.Moradores.Adicionar(Morador);
            }
            else
            {
                _existente.AlterarCadastro(txtNome.Text, setor, txtEndereco.Text, txtTelefone.Text, condicao);
                Morador = _existente;
            }
            _ctx.Dados.Moradores.Salvar();
        });

        if (ok) DialogResult = DialogResult.OK;
    }
}
