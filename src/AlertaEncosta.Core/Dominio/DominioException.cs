namespace AlertaEncosta.Core.Dominio;

/// <summary>
/// Erro de regra de negócio ou de validação. A mensagem é escrita para o operador
/// (linguagem simples) e é exibida diretamente na tela.
/// </summary>
public class DominioException : Exception
{
    public DominioException(string mensagem) : base(mensagem) { }
}

/// <summary>Validações reutilizadas pelas entidades (guard clauses).</summary>
internal static class Validar
{
    public static string TextoObrigatorio(string? valor, string campo, int minimo = 1, int maximo = 200)
    {
        var texto = valor?.Trim() ?? string.Empty;
        if (texto.Length < minimo)
            throw new DominioException(minimo <= 1
                ? $"Informe {campo}."
                : $"{Capitalizar(campo)} precisa ter pelo menos {minimo} caracteres.");
        if (texto.Length > maximo)
            throw new DominioException($"{Capitalizar(campo)} pode ter no máximo {maximo} caracteres.");
        return texto;
    }

    public static double Faixa(double valor, double minimo, double maximo, string campo, string unidade)
    {
        if (double.IsNaN(valor) || double.IsInfinity(valor))
            throw new DominioException($"{Capitalizar(campo)} não é um número válido.");
        if (valor < minimo || valor > maximo)
            throw new DominioException($"{Capitalizar(campo)} deve ficar entre {minimo} e {maximo} {unidade}.".TrimEnd());
        return valor;
    }

    public static int Faixa(int valor, int minimo, int maximo, string campo)
    {
        if (valor < minimo || valor > maximo)
            throw new DominioException($"{Capitalizar(campo)} deve ficar entre {minimo} e {maximo}.");
        return valor;
    }

    private static string Capitalizar(string texto) =>
        string.IsNullOrEmpty(texto) ? texto : char.ToUpperInvariant(texto[0]) + texto[1..];
}
