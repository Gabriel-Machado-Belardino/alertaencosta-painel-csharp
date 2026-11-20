using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Persistencia;

/// <summary>
/// Persistência básica em arquivo JSON (um arquivo por coleção). A gravação é atômica:
/// escreve num arquivo temporário e depois troca, para não corromper os dados se o
/// computador desligar no meio (falta de energia em dia de temporal é comum).
/// </summary>
public class RepositorioJson<T> : RepositorioMemoria<T> where T : class, IEntidade
{
    public static readonly JsonSerializerOptions Opcoes = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // mantém acentos legíveis no arquivo
        Converters = { new JsonStringEnumConverter() }
    };

    public string CaminhoArquivo { get; }

    public RepositorioJson(string caminhoArquivo)
    {
        CaminhoArquivo = caminhoArquivo;
        Carregar();
    }

    private void Carregar()
    {
        if (!File.Exists(CaminhoArquivo)) return;

        try
        {
            var json = File.ReadAllText(CaminhoArquivo);
            var lidos = JsonSerializer.Deserialize<List<T>>(json, Opcoes) ?? new List<T>();
            lock (Trava)
            {
                Itens.Clear();
                Itens.AddRange(lidos);
            }
        }
        catch (JsonException ex)
        {
            // Não apaga o arquivo do usuário: guarda uma cópia e começa vazio.
            var copia = CaminhoArquivo + $".corrompido-{DateTime.Now:yyyyMMddHHmmss}";
            File.Copy(CaminhoArquivo, copia, overwrite: true);
            throw new DominioException(
                $"O arquivo {Path.GetFileName(CaminhoArquivo)} está corrompido e foi copiado para {Path.GetFileName(copia)}. Detalhe: {ex.Message}");
        }
    }

    public override void Salvar()
    {
        string json;
        lock (Trava) json = JsonSerializer.Serialize(Itens, Opcoes);

        var pasta = Path.GetDirectoryName(CaminhoArquivo);
        if (!string.IsNullOrEmpty(pasta)) Directory.CreateDirectory(pasta);

        var temporario = CaminhoArquivo + ".tmp";
        File.WriteAllText(temporario, json);
        File.Move(temporario, CaminhoArquivo, overwrite: true);
    }
}
