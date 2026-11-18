using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>
/// Cartão de indicador (KPI) desenhado à mão: título, valor grande e detalhe.
/// A faixa colorida à esquerda indica o estado (verde = ok, amarelo = atenção, vermelho = crítico).
/// Pode ser arrastado da Toolbox no designer.
/// </summary>
[DesignerCategory("Code")]
public class CartaoIndicador : Control
{
    private string _titulo = "Indicador";
    private string _valor = "0";
    private string _detalhe = string.Empty;
    private Color _corEstado = Color.FromArgb(46, 125, 50);

    public CartaoIndicador()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Size = new Size(200, 100);
        Margin = new Padding(6);
    }

    [Category("AlertaEncosta"), DefaultValue("Indicador")]
    public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }

    [Category("AlertaEncosta"), DefaultValue("0")]
    public string Valor { get => _valor; set { _valor = value; Invalidate(); } }

    [Category("AlertaEncosta"), DefaultValue("")]
    public string Detalhe { get => _detalhe; set { _detalhe = value; Invalidate(); } }

    [Category("AlertaEncosta"), DefaultValue(typeof(Color), "46, 125, 50")]
    public Color CorEstado { get => _corEstado; set { _corEstado = value; Invalidate(); } }

    public void Definir(string valor, string detalhe, Color corEstado)
    {
        _valor = valor;
        _detalhe = detalhe;
        _corEstado = corEstado;
        Invalidate();
    }

    private int S(double valor) => (int)Math.Round(valor * DeviceDpi / 96.0);

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var area = new Rectangle(0, 0, Width - 1, Height - 1);
        using (var caminho = Desenho.Arredondado(area, S(10)))
        {
            using var fundo = new SolidBrush(Tema.Cartao);
            using var borda = new Pen(Tema.Borda);
            g.FillPath(fundo, caminho);
            g.DrawPath(borda, caminho);
        }

        using (var faixa = new SolidBrush(_corEstado))
            g.FillRectangle(faixa, new Rectangle(0, S(10), S(6), Height - S(20)));

        var x = S(18);
        var largura = Width - x - S(8);
        using var fonteTitulo = Tema.Fonte(9F, FontStyle.Bold);
        using var fonteValor = Tema.Fonte(20F, FontStyle.Bold);
        using var fonteDetalhe = Tema.Fonte(9F);

        var flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
        var corValor = _corEstado == Tema.Sucesso ? Tema.TextoPrincipal : _corEstado == Tema.Atencao ? Color.FromArgb(150, 90, 0) : _corEstado;
        TextRenderer.DrawText(g, _titulo.ToUpperInvariant(), fonteTitulo, new Rectangle(x, S(10), largura, S(20)), Tema.TextoSecundario, flags);
        TextRenderer.DrawText(g, _valor, fonteValor, new Rectangle(x, S(30), largura, S(40)), corValor, flags);
        TextRenderer.DrawText(g, _detalhe, fonteDetalhe, new Rectangle(x, Height - S(28), largura, S(20)), Tema.TextoSecundario, flags);
    }
}

internal static class Desenho
{
    public static GraphicsPath Arredondado(Rectangle r, int raio)
    {
        var d = raio * 2;
        var p = new GraphicsPath();
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }
}
