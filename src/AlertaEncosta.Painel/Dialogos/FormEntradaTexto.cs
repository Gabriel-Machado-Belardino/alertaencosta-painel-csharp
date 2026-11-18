using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>Caixa de diálogo simples para pedir um texto ao operador (equipe, parecer...).</summary>
public partial class FormEntradaTexto : Form
{
    public FormEntradaTexto()
    {
        InitializeComponent();
        Tema.Aplicar(this);
    }

    /// <summary>Retorna o texto digitado, ou null se o operador cancelar.</summary>
    public static string? Perguntar(IWin32Window? dono, string titulo, string pergunta, string valorInicial, bool multilinha = false)
    {
        using var f = new FormEntradaTexto();
        f.Text = titulo;
        f.lblPergunta.Text = pergunta;
        f.txtValor.Text = valorInicial;
        if (multilinha)
        {
            f.txtValor.Multiline = true;
            f.txtValor.ScrollBars = ScrollBars.Vertical;
            f.AcceptButton = null; // Enter quebra linha
            f.ClientSize = new Size(f.ClientSize.Width, f.ClientSize.Height + 90);
        }
        f.txtValor.SelectAll();
        return f.ShowDialog(dono) == DialogResult.OK ? f.txtValor.Text.Trim() : null;
    }
}
