using System.Net.Http.Headers;
using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Integracao;

/// <summary>
/// Busca leituras na API REST do AlertaEncosta (GET que devolve um array no contrato da estação).
/// Se a API estiver fora do ar, o painel continua funcionando com os dados locais
/// (princípio "funcionar com pouca conectividade").
/// </summary>
public sealed class ClienteApiLeituras
{
    private readonly HttpClient _http;
    private readonly ImportadorLeituras _importador;

    public ClienteApiLeituras(HttpClient http, ImportadorLeituras importador)
    {
        _http = http;
        _importador = importador;
    }

    public static Uri ValidarUrl(string? url)
    {
        if (!Uri.TryCreate(url?.Trim(), UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            throw new DominioException("Informe uma URL completa, começando com http:// ou https://.");
        return uri;
    }

    public async Task<ResultadoInterpretacao> BuscarAsync(string url, DateTime agora, CancellationToken cancelamento = default) =>
        _importador.Interpretar(await BuscarTextoAsync(url, cancelamento).ConfigureAwait(false), agora);

    /// <summary>Faz o GET e devolve o corpo (JSON) sem interpretar, para o operador conferir antes de importar.</summary>
    public async Task<string> BuscarTextoAsync(string url, CancellationToken cancelamento = default)
    {
        var uri = ValidarUrl(url);
        using var requisicao = new HttpRequestMessage(HttpMethod.Get, uri);
        requisicao.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            using var resposta = await _http.SendAsync(requisicao, cancelamento).ConfigureAwait(false);
            if (!resposta.IsSuccessStatusCode)
                throw new DominioException($"A API respondeu HTTP {(int)resposta.StatusCode} ({resposta.ReasonPhrase}).");

            return await resposta.Content.ReadAsStringAsync(cancelamento).ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            throw new DominioException($"Sem conexão com a API ({ex.Message}). O painel segue com os dados locais.");
        }
        catch (TaskCanceledException) when (!cancelamento.IsCancellationRequested)
        {
            throw new DominioException("A API demorou demais para responder. O painel segue com os dados locais.");
        }
    }
}
