using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Persistencia;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel;

internal static class Program
{
    /// <summary>
    /// Ponto de entrada do Painel da Defesa Civil.
    /// Argumentos opcionais:
    ///   --dados "C:\pasta"            usa outra pasta de dados (padrão: %LocalAppData%\AlertaEncosta\PainelDefesaCivil)
    ///   --capturar-telas "C:\pasta"   gera um PNG de cada aba e fecha (usado nas evidências do README)
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => Ui.MostrarErro(null, e.Exception);

        var pasta = Argumento(args, "--dados") ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlertaEncosta", "PainelDefesaCivil");
        var pastaCapturas = Argumento(args, "--capturar-telas");

        ContextoDados dados;
        try
        {
            dados = ContextoDados.EmPasta(pasta);
            DadosIniciais.Popular(dados, DateTime.Now);
        }
        catch (DominioException ex)
        {
            MessageBox.Show(ex.Message, Ui.Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var contexto = new PainelContexto(dados, Environment.UserName);
        var formulario = new FormPrincipal(contexto);

        if (pastaCapturas is not null)
            formulario.Shown += (_, _) => CapturaDeTelas.Executar(formulario, contexto, pastaCapturas);

        Application.Run(formulario);
    }

    private static string? Argumento(string[] args, string nome)
    {
        var i = Array.FindIndex(args, a => string.Equals(a, nome, StringComparison.OrdinalIgnoreCase));
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }
}
