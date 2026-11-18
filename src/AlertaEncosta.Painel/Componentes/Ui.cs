using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Painel.Componentes;

/// <summary>Linha exibida numa grade. Todas as linhas têm o Id da entidade para recuperar a seleção.</summary>
internal interface ILinha
{
    string Id { get; }
}

/// <summary>Utilitários de interface: tratamento de erros, confirmação e seleção em grades.</summary>
internal static class Ui
{
    public const string Titulo = "AlertaEncosta · Painel da Defesa Civil";

    /// <summary>
    /// Executa uma ação do usuário. Erros de regra de negócio (DominioException) viram um aviso
    /// amigável; qualquer outro erro é mostrado sem derrubar o painel.
    /// </summary>
    public static bool Executar(IWin32Window? dono, Action acao)
    {
        try
        {
            acao();
            return true;
        }
        catch (DominioException ex)
        {
            Avisar(dono, ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            MostrarErro(dono, ex);
            return false;
        }
    }

    public static void Avisar(IWin32Window? dono, string mensagem) =>
        MessageBox.Show(dono, mensagem, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public static void Informar(IWin32Window? dono, string mensagem) =>
        MessageBox.Show(dono, mensagem, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static bool Confirmar(IWin32Window? dono, string pergunta) =>
        MessageBox.Show(dono, pergunta, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    public static void MostrarErro(IWin32Window? dono, Exception ex)
    {
        if (ex is DominioException) { Avisar(dono, ex.Message); return; }
        MessageBox.Show(dono, $"Ocorreu um erro inesperado, mas o painel continua funcionando.\n\n{ex.Message}",
            Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static string? IdSelecionado(DataGridView grade) => (grade.CurrentRow?.DataBoundItem as ILinha)?.Id;

    /// <summary>Atualiza a grade mantendo a linha que estava selecionada.</summary>
    public static void Preencher<T>(DataGridView grade, IReadOnlyList<T> linhas) where T : ILinha
    {
        var selecionado = IdSelecionado(grade);
        grade.DataSource = linhas.ToList();
        if (selecionado is null) return;

        foreach (DataGridViewRow linha in grade.Rows)
        {
            if (linha.DataBoundItem is ILinha l && l.Id == selecionado)
            {
                grade.CurrentCell = linha.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                break;
            }
        }
    }

    /// <summary>Preenche uma ComboBox de setores, com a opção "Todos" opcional, preservando a seleção.</summary>
    public static void PreencherSetores(ComboBox combo, IEnumerable<Setor> setores, bool incluirTodos)
    {
        var anterior = (combo.SelectedItem as OpcaoSetor)?.Id;
        var opcoes = new List<OpcaoSetor>();
        if (incluirTodos) opcoes.Add(new OpcaoSetor(null, "Todos os setores"));
        opcoes.AddRange(setores.OrderBy(s => s.Id).Select(s => new OpcaoSetor(s.Id, $"{s.Id} · {s.Nome}")));

        combo.BeginUpdate();
        combo.Items.Clear();
        foreach (var o in opcoes) combo.Items.Add(o);
        var indice = opcoes.FindIndex(o => o.Id == anterior);
        combo.SelectedIndex = combo.Items.Count == 0 ? -1 : Math.Max(0, indice);
        combo.EndUpdate();
    }

    public static string? SetorSelecionado(ComboBox combo) => (combo.SelectedItem as OpcaoSetor)?.Id;

    public static void SelecionarSetor(ComboBox combo, string? setorId)
    {
        for (var i = 0; i < combo.Items.Count; i++)
            if (combo.Items[i] is OpcaoSetor o && o.Id == setorId) { combo.SelectedIndex = i; return; }
    }

    public sealed record OpcaoSetor(string? Id, string Texto)
    {
        public override string ToString() => Texto;
    }

    /// <summary>Item genérico para ComboBox de enums com texto amigável.</summary>
    public sealed record Opcao<T>(T Valor, string Texto)
    {
        public override string ToString() => Texto;
    }

    public static string Duracao(TimeSpan t) =>
        t.TotalMinutes < 1 ? "agora" :
        t.TotalHours < 1 ? $"{(int)t.TotalMinutes} min" :
        t.TotalDays < 1 ? $"{(int)t.TotalHours} h {t.Minutes:00} min" :
        $"{(int)t.TotalDays} d {t.Hours} h";
}
