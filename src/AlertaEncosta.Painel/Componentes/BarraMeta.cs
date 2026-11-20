using System.ComponentModel;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>
/// Barra de progresso com a marca da meta (ex.: 80% dos moradores em local seguro).
/// Verde quando atinge a meta, vermelha abaixo dela. Sem animação: o valor aparece na hora.
/// </summary>
[DesignerCategory("Code")]
public class BarraMeta : Control
{
    private double _valor;
    private double _meta = 80;

    public BarraMeta()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        Size = new Size(300, 24);
    }

    /// <summary>Percentual de 0 a 100.</summary>
    [Category("AlertaEncosta"), DefaultValue(0d)]
    public double Valor { get => _valor; set { _valor = Math.Clamp(value, 0, 100); Invalidate(); } }

    [Category("AlertaEncosta"), DefaultValue(80d)]
    public double Meta { get => _meta; set { _meta = Math.Clamp(value, 0, 100); Invalidate(); } }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var area = new Rectangle(0, 0, Width - 1, Height - 1);
        using (var fundo = new SolidBrush(Color.FromArgb(232, 236, 241)))
            g.FillRectangle(fundo, area);

        var largura = (int)(area.Width * _valor / 100.0);
        using (var cor = new SolidBrush(_valor >= _meta ? Tema.Sucesso : Tema.Perigo))
            g.FillRectangle(cor, new Rectangle(0, 0, largura, area.Height + 1));

        var x = (int)(area.Width * _meta / 100.0);
        using (var caneta = new Pen(Tema.Cabecalho, Math.Max(2, DeviceDpi / 48)))
            g.DrawLine(caneta, x, 0, x, Height);
        using (var borda = new Pen(Tema.Borda))
            g.DrawRectangle(borda, area);
    }
}
