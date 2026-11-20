using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;
using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>
/// "Mapa de setores": bloco clicável com a situação de um setor (risco, chuva, solo, nível,
/// tendência e alerta). As barras mostram o valor em relação ao limiar de ALTO.
/// </summary>
[DesignerCategory("Code")]
public class BlocoSetor : Control
{
    private static readonly CultureInfo Br = CultureInfo.GetCultureInfo("pt-BR");
    private ResumoSetor? _resumo;
    private LimiaresRisco _limiares = LimiaresRisco.Padrao;
    private bool _selecionado;
    private bool _mouseSobre;

    public BlocoSetor()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.Selectable, true);
        Size = new Size(310, 196);
        Margin = new Padding(8);
        Cursor = Cursors.Hand;
        TabStop = true;
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ResumoSetor? Resumo => _resumo;

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Selecionado
    {
        get => _selecionado;
        set { _selecionado = value; Invalidate(); }
    }

    /// <summary>Converte pixels de projeto (96 DPI) para a escala real do monitor.</summary>
    private int S(double valor) => (int)Math.Round(valor * DeviceDpi / 96.0);

    public void Definir(ResumoSetor resumo, LimiaresRisco limiares)
    {
        Size = new Size(S(310), S(196));
        _resumo = resumo;
        _limiares = limiares;
        AccessibleName = $"Setor {resumo.Setor.Nome}, risco {resumo.Setor.RiscoAtual.Rotulo()}";
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e) { _mouseSobre = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _mouseSobre = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { Focus(); base.OnMouseDown(e); }
    protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
    protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter or Keys.Space) OnClick(EventArgs.Empty);
        base.OnKeyDown(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? Tema.Fundo);
        if (_resumo is null) return;

        var setor = _resumo.Setor;
        var risco = setor.RiscoAtual;
        var corRisco = Tema.CorRisco(risco);

        var area = new Rectangle(S(1), S(1), Width - S(3), Height - S(3));
        using (var caminho = Desenho.Arredondado(area, S(12)))
        {
            using var fundo = new SolidBrush(_mouseSobre ? Color.FromArgb(252, 252, 253) : Tema.Cartao);
            using var borda = new Pen(_selecionado || Focused ? Tema.Cabecalho : Tema.Borda, _selecionado ? S(3) : 1);
            g.FillPath(fundo, caminho);
            g.DrawPath(borda, caminho);
        }

        // Cabeçalho colorido com o nível de risco.
        var cabecalho = new Rectangle(area.X, area.Y, area.Width, S(44));
        using (var caminhoCab = new GraphicsPath())
        {
            caminhoCab.AddArc(cabecalho.X, cabecalho.Y, S(24), S(24), 180, 90);
            caminhoCab.AddArc(cabecalho.Right - S(24), cabecalho.Y, S(24), S(24), 270, 90);
            caminhoCab.AddLine(cabecalho.Right, cabecalho.Bottom, cabecalho.X, cabecalho.Bottom);
            caminhoCab.CloseFigure();
            using var pincel = new SolidBrush(corRisco);
            g.FillPath(pincel, caminhoCab);
        }

        var textoCab = Tema.TextoSobreRisco(risco);
        using var fNome = Tema.Fonte(11F, FontStyle.Bold);
        using var fRisco = Tema.Fonte(13F, FontStyle.Bold);
        using var fNormal = Tema.Fonte(9.5F);
        using var fNegrito = Tema.Fonte(9.5F, FontStyle.Bold);
        var esquerda = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
        var direita = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;

        TextRenderer.DrawText(g, setor.Nome, fNome, new Rectangle(S(14), S(4), Width - S(110), S(22)), textoCab, esquerda);
        TextRenderer.DrawText(g, $"{setor.Id} · {setor.EstacaoId}", fNormal, new Rectangle(S(14), S(24), Width - S(110), S(18)), textoCab, esquerda);
        TextRenderer.DrawText(g, risco.Rotulo(), fRisco, new Rectangle(Width - S(104), S(4), S(90), S(38)), textoCab, direita);

        var u = _resumo.UltimaLeitura;
        var y = S(52);
        if (u is null)
        {
            TextRenderer.DrawText(g, "Sem leituras ainda.", fNormal, new Rectangle(S(14), y, Width - S(28), S(22)), Tema.TextoSecundario, esquerda);
        }
        else
        {
            y = Barra(g, y, "Chuva", $"{u.ChuvaMm.ToString("0.#", Br)} mm", u.ChuvaMm / _limiares.ChuvaAltoMm, fNormal, fNegrito);
            y = Barra(g, y, "Solo", $"{u.UmidadeSoloPct}%", u.UmidadeSoloPct / 100.0, fNormal, fNegrito);
            y = Barra(g, y, "Córrego", $"{u.NivelAguaCm.ToString("0", Br)} cm", u.NivelAguaCm / _limiares.NivelAltoCm, fNormal, fNegrito);
        }

        y += S(2);
        var corTendencia = _resumo.Tendencia == Tendencia.Subindo ? Tema.Perigo
                         : _resumo.Tendencia == Tendencia.Descendo ? Tema.Sucesso : Tema.TextoSecundario;
        TextRenderer.DrawText(g, _resumo.Tendencia.Simbolo(), fNegrito, new Rectangle(S(14), y, S(120), S(20)), corTendencia, esquerda);
        var quando = u is null ? "" : $"leitura {u.DataHora:HH:mm:ss}";
        TextRenderer.DrawText(g, quando, fNormal, new Rectangle(S(130), y, Width - S(144), S(20)), Tema.TextoSecundario, direita);
        y += S(22);

        TextRenderer.DrawText(g, $"{_resumo.Moradores} moradores · {_resumo.Vulneraveis} prioritários", fNormal,
            new Rectangle(S(14), y, Width - S(28), S(20)), Tema.TextoSecundario, esquerda);
        y += S(22);

        string alerta;
        Color corAlerta;
        if (_resumo.AlertaAtivo is { } a)
        {
            alerta = $"🔔 {a.Id} ativo ({a.Nivel.Rotulo()})" + (_resumo.PedidosAjuda > 0 ? $" · {_resumo.PedidosAjuda} pedido(s) de ajuda" : "");
            corAlerta = _resumo.PedidosAjuda > 0 ? Tema.Perigo : Tema.Cabecalho;
        }
        else if (risco > NivelRisco.Baixo)
        {
            alerta = "⚠ Alerta recomendado";
            corAlerta = Tema.Perigo;
        }
        else
        {
            alerta = "Sem alerta ativo";
            corAlerta = Tema.TextoSecundario;
        }
        TextRenderer.DrawText(g, alerta, fNegrito, new Rectangle(S(14), y, Width - S(28), S(20)), corAlerta, esquerda);
    }

    private int Barra(Graphics g, int y, string rotulo, string valor, double fracao, Font fNormal, Font fNegrito)
    {
        var esquerda = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
        TextRenderer.DrawText(g, rotulo, fNormal, new Rectangle(S(14), y, S(70), S(20)), Tema.TextoSecundario, esquerda);
        TextRenderer.DrawText(g, valor, fNegrito, new Rectangle(Width - S(84), y, S(70), S(20)), Tema.TextoPrincipal,
            TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

        var trilho = new Rectangle(S(86), y + S(6), Width - S(86) - S(92), S(9));
        using (var fundo = new SolidBrush(Color.FromArgb(232, 236, 241)))
            g.FillRectangle(fundo, trilho);

        var f = Math.Clamp(fracao, 0, 1);
        var cor = f >= 1 ? Tema.Perigo : f >= 0.5 ? Tema.Atencao : Tema.Sucesso;
        using (var preenchimento = new SolidBrush(cor))
            g.FillRectangle(preenchimento, new Rectangle(trilho.X, trilho.Y, (int)(trilho.Width * f), trilho.Height));
        return y + S(22);
    }
}
