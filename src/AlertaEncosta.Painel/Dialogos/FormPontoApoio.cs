using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>Cadastro/edição de ponto de apoio (escola, igreja, CEU que recebe moradores).</summary>
public partial class FormPontoApoio : Form
{
    private readonly PainelContexto _ctx;
    private readonly PontoApoio? _existente;

    public PontoApoio? Ponto { get; private set; }

    public FormPontoApoio(PainelContexto contexto, PontoApoio? existente)
    {
        _ctx = contexto;
        _existente = existente;
        InitializeComponent();
        Tema.Aplicar(this);

        if (existente is null)
        {
            Text = "Novo ponto de apoio";
            txtCodigo.Text = ContextoDados.ProximoCodigo(_ctx.Dados.PontosApoio, "PA", 2);
            return;
        }

        Text = $"Editar {existente.Id}";
        txtCodigo.Text = existente.Id;
        txtCodigo.ReadOnly = true;
        txtNome.Text = existente.Nome;
        txtEndereco.Text = existente.Endereco;
        numLatitude.Value = (decimal)existente.Local.Latitude;
        numLongitude.Value = (decimal)existente.Local.Longitude;
        numCapacidade.Value = existente.Capacidade;
        chkAcessivel.Checked = existente.Acessivel;
        chkAtivo.Checked = existente.Ativo;
    }

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        var local = new Coordenada((double)numLatitude.Value, (double)numLongitude.Value);
        var ok = Ui.Executar(this, () =>
        {
            if (_existente is null)
            {
                Ponto = new PontoApoio(txtCodigo.Text, txtNome.Text, txtEndereco.Text, local, (int)numCapacidade.Value, chkAcessivel.Checked);
                if (!chkAtivo.Checked) Ponto.AlterarDados(Ponto.Nome, Ponto.Endereco, local, Ponto.Capacidade, Ponto.Acessivel, ativo: false);
                _ctx.Dados.PontosApoio.Adicionar(Ponto);
            }
            else
            {
                _existente.AlterarDados(txtNome.Text, txtEndereco.Text, local, (int)numCapacidade.Value, chkAcessivel.Checked, chkAtivo.Checked);
                Ponto = _existente;
            }
            _ctx.Dados.PontosApoio.Salvar();
        });

        if (ok) DialogResult = DialogResult.OK;
    }
}
