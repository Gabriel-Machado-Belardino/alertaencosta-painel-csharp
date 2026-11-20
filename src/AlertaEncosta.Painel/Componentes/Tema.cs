using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>
/// Identidade visual do painel: alto contraste, letras grandes e cores de risco consistentes
/// (princípio "Acessibilidade e linguagem simples" da arquitetura).
/// </summary>
internal static class Tema
{
    public static readonly Color Fundo = Color.FromArgb(244, 246, 249);
    public static readonly Color Cabecalho = Color.FromArgb(22, 50, 84);
    public static readonly Color Destaque = Color.FromArgb(232, 119, 34);
    public static readonly Color Cartao = Color.White;
    public static readonly Color Borda = Color.FromArgb(214, 219, 226);
    public static readonly Color TextoPrincipal = Color.FromArgb(28, 33, 40);
    public static readonly Color TextoSecundario = Color.FromArgb(88, 96, 108);
    public static readonly Color Sucesso = Color.FromArgb(46, 125, 50);
    public static readonly Color Perigo = Color.FromArgb(198, 40, 40);
    public static readonly Color Atencao = Color.FromArgb(245, 166, 35);

    public const string FonteBase = "Segoe UI";

    public static Font Fonte(float tamanho, FontStyle estilo = FontStyle.Regular) => new(FonteBase, tamanho, estilo);

    public static Color CorRisco(NivelRisco nivel) => nivel switch
    {
        NivelRisco.Alto => Perigo,
        NivelRisco.Medio => Atencao,
        _ => Sucesso
    };

    /// <summary>Texto preto sobre amarelo e branco sobre verde/vermelho (contraste AA).</summary>
    public static Color TextoSobreRisco(NivelRisco nivel) => nivel == NivelRisco.Medio ? Color.Black : Color.White;

    /// <summary>Aplica o estilo padrão em toda a árvore de controles (chamado no construtor das telas).</summary>
    public static void Aplicar(Control raiz)
    {
        foreach (Control c in raiz.Controls)
        {
            switch (c)
            {
                case Button b: EstilizarBotao(b); break;
                case DataGridView g: EstilizarGrade(g); break;
            }
            if (c.HasChildren) Aplicar(c);
        }
    }

    /// <summary>Botões com Tag = "primario" ficam laranja; Tag = "perigo" ficam vermelhos.</summary>
    public static void EstilizarBotao(Button b)
    {
        var tipo = b.Tag as string;
        b.FlatStyle = FlatStyle.Flat;
        b.Cursor = Cursors.Hand;
        b.AutoSize = true;
        b.MinimumSize = new Size(0, 34);
        b.Padding = new Padding(8, 2, 8, 2);
        b.UseVisualStyleBackColor = false;
        b.FlatAppearance.BorderSize = 1;

        if (tipo is "primario" or "perigo") b.Font = Fonte(10F, FontStyle.Bold);

        // Botão desabilitado fica cinza (FlatStyle com cor própria não muda sozinho).
        void Colorir(object? _, EventArgs __)
        {
            (b.BackColor, b.ForeColor, b.FlatAppearance.BorderColor) = (b.Enabled, tipo) switch
            {
                (false, _) => (Color.FromArgb(233, 236, 240), Color.FromArgb(150, 156, 165), Borda),
                (true, "primario") => (Destaque, Color.White, Destaque),
                (true, "perigo") => (Perigo, Color.White, Perigo),
                _ => (Color.White, TextoPrincipal, Borda)
            };
        }
        Colorir(null, EventArgs.Empty);
        b.EnabledChanged += Colorir;
    }

    public static void EstilizarGrade(DataGridView g)
    {
        g.AutoGenerateColumns = false;
        g.AllowUserToAddRows = false;
        g.AllowUserToDeleteRows = false;
        g.AllowUserToResizeRows = false;
        g.ReadOnly = true;
        g.MultiSelect = false;
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        g.RowHeadersVisible = false;
        g.BackgroundColor = Cartao;
        g.BorderStyle = BorderStyle.None;
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        g.GridColor = Borda;
        g.EnableHeadersVisualStyles = false;
        g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        g.ColumnHeadersHeight = 36;
        g.ColumnHeadersDefaultCellStyle.BackColor = Cabecalho;
        g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Cabecalho;
        g.ColumnHeadersDefaultCellStyle.Font = Fonte(9.5F, FontStyle.Bold);
        g.ColumnHeadersDefaultCellStyle.Padding = new Padding(4, 0, 0, 0);
        g.DefaultCellStyle.Font = Fonte(10F);
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 230, 248);
        g.DefaultCellStyle.SelectionForeColor = TextoPrincipal;
        g.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);
        g.RowTemplate.Height = 32;
    }

    public static DataGridViewTextBoxColumn Coluna(DataGridView grade, string propriedade, string titulo,
        int largura = 110, string? formato = null, bool preencher = false, DataGridViewContentAlignment? alinhamento = null)
    {
        var coluna = new DataGridViewTextBoxColumn
        {
            Name = propriedade,
            DataPropertyName = propriedade,
            HeaderText = titulo,
            Width = largura,
            SortMode = DataGridViewColumnSortMode.NotSortable,
            AutoSizeMode = preencher ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.None
        };
        if (formato is not null) coluna.DefaultCellStyle.Format = formato;
        if (alinhamento is not null) coluna.DefaultCellStyle.Alignment = alinhamento.Value;
        grade.Columns.Add(coluna);
        return coluna;
    }

    /// <summary>Pinta a célula com a cor do risco quando o texto é BAIXO/MÉDIO/ALTO.</summary>
    public static void PintarRisco(DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null || !NivelRiscoExtensoes.TentarConverter(e.Value as string, out var nivel)) return;
        e.CellStyle.BackColor = CorRisco(nivel);
        e.CellStyle.ForeColor = TextoSobreRisco(nivel);
        e.CellStyle.SelectionBackColor = CorRisco(nivel);
        e.CellStyle.SelectionForeColor = TextoSobreRisco(nivel);
        e.CellStyle.Font = Fonte(10F, FontStyle.Bold);
        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }
}
