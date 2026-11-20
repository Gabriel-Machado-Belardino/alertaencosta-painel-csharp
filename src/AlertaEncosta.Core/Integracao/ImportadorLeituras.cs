using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Integracao;

/// <summary>
/// Contrato JSON da estação ESP32 (o mesmo que o firmware envia no POST para a API):
/// {"estacaoId":"EST-BRAS-01","setorId":"BRAS-S01","timestamp":"2026-11-18T02:00:27Z",
///  "chuvaMm":11.0,"chuvaTotalMm":11.0,"umidadeSoloPct":87,"nivelAguaCm":8.4,"riscoLocal":"ALTO","uptimeMs":34855}
/// </summary>
public sealed class LeituraEstacaoDto
{
    [JsonPropertyName("estacaoId")] public string? EstacaoId { get; set; }
    [JsonPropertyName("setorId")] public string? SetorId { get; set; }
    [JsonPropertyName("timestamp")] public string? Timestamp { get; set; }
    [JsonPropertyName("chuvaMm")] public double? ChuvaMm { get; set; }
    [JsonPropertyName("chuvaTotalMm")] public double? ChuvaTotalMm { get; set; }
    [JsonPropertyName("umidadeSoloPct")] public double? UmidadeSoloPct { get; set; }
    [JsonPropertyName("nivelAguaCm")] public double? NivelAguaCm { get; set; }
    [JsonPropertyName("riscoLocal")] public string? RiscoLocal { get; set; }
    [JsonPropertyName("uptimeMs")] public long? UptimeMs { get; set; }
}

public sealed record ResultadoInterpretacao(IReadOnlyList<LeituraSensor> Leituras, IReadOnlyList<string> Erros);

/// <summary>
/// Converte texto em leituras. Aceita:
///  • um objeto JSON ou um array de objetos (arquivo .json / resposta da API);
///  • linhas do Serial Monitor do Wokwi, ex.: "[API] POST http://... -> HTTP 200 | {...json...}".
/// Cada leitura é validada pelo domínio; as inválidas viram mensagens de erro, sem derrubar as outras.
/// </summary>
public sealed class ImportadorLeituras
{
    private static readonly JsonSerializerOptions Opcoes = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public ResultadoInterpretacao Interpretar(string? texto, DateTime agora)
    {
        var leituras = new List<LeituraSensor>();
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(texto))
        {
            erros.Add("Nenhum conteúdo para importar.");
            return new ResultadoInterpretacao(leituras, erros);
        }

        var conteudo = texto.Trim();

        // 1) Documento JSON inteiro (array ou objeto, inclusive formatado em várias linhas).
        if (conteudo.StartsWith('[') || conteudo.StartsWith('{'))
        {
            try
            {
                var dtos = conteudo.StartsWith('[')
                    ? JsonSerializer.Deserialize<List<LeituraEstacaoDto?>>(conteudo, Opcoes) ?? new List<LeituraEstacaoDto?>()
                    : new List<LeituraEstacaoDto?> { JsonSerializer.Deserialize<LeituraEstacaoDto>(conteudo, Opcoes) };

                for (var i = 0; i < dtos.Count; i++)
                    Converter(dtos[i], $"Item {i + 1}", agora, leituras, erros);
                return new ResultadoInterpretacao(leituras, erros);
            }
            catch (JsonException)
            {
                // Pode ser várias linhas com um objeto em cada (NDJSON): tenta linha a linha abaixo.
            }
        }

        // 2) Linha a linha (Serial Monitor do Wokwi / NDJSON).
        var linhas = conteudo.Split('\n');
        var encontrouJson = false;
        for (var i = 0; i < linhas.Length; i++)
        {
            var linha = linhas[i];
            var inicio = linha.IndexOf('{');
            var fim = linha.LastIndexOf('}');
            if (inicio < 0 || fim <= inicio) continue; // linha sem JSON ([LEITURA], [WIFI]...): ignora

            encontrouJson = true;
            var trecho = linha[inicio..(fim + 1)];
            try
            {
                Converter(JsonSerializer.Deserialize<LeituraEstacaoDto>(trecho, Opcoes), $"Linha {i + 1}", agora, leituras, erros);
            }
            catch (JsonException ex)
            {
                erros.Add($"Linha {i + 1}: JSON inválido ({ex.Message.Split('.')[0]}).");
            }
        }

        if (!encontrouJson)
            erros.Add("Nenhuma leitura JSON encontrada. Cole o JSON da estação ou as linhas \"[API] POST ...\" do Serial Monitor do Wokwi.");

        return new ResultadoInterpretacao(leituras, erros);
    }

    private static void Converter(LeituraEstacaoDto? dto, string onde, DateTime agora,
        List<LeituraSensor> leituras, List<string> erros)
    {
        if (dto is null) { erros.Add($"{onde}: vazio."); return; }

        var faltando = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.EstacaoId)) faltando.Add("estacaoId");
        if (string.IsNullOrWhiteSpace(dto.SetorId)) faltando.Add("setorId");
        if (dto.ChuvaMm is null) faltando.Add("chuvaMm");
        if (dto.UmidadeSoloPct is null) faltando.Add("umidadeSoloPct");
        if (dto.NivelAguaCm is null) faltando.Add("nivelAguaCm");
        if (faltando.Count > 0)
        {
            erros.Add($"{onde}: campo(s) obrigatório(s) ausente(s): {string.Join(", ", faltando)}.");
            return;
        }

        // timestamp null = estação ainda sem NTP: o painel "carimba" na chegada.
        var dataHora = agora;
        if (!string.IsNullOrWhiteSpace(dto.Timestamp))
        {
            if (!DateTimeOffset.TryParse(dto.Timestamp, CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out var dto2))
            {
                erros.Add($"{onde}: timestamp \"{dto.Timestamp}\" inválido (use ISO 8601, ex.: 2026-11-18T02:00:27Z).");
                return;
            }
            dataHora = dto2.ToLocalTime().DateTime;
        }

        NivelRisco? riscoEstacao = NivelRiscoExtensoes.TentarConverter(dto.RiscoLocal, out var r) ? r : null;

        try
        {
            var solo = dto.UmidadeSoloPct!.Value;
            if (solo % 1 != 0) solo = Math.Round(solo);
            leituras.Add(new LeituraSensor(
                dto.EstacaoId!, dto.SetorId!, dataHora,
                dto.ChuvaMm!.Value, dto.ChuvaTotalMm ?? dto.ChuvaMm!.Value,
                (int)solo, dto.NivelAguaCm!.Value,
                OrigemLeitura.EstacaoEsp32, riscoEstacao));
        }
        catch (DominioException ex)
        {
            erros.Add($"{onde}: {ex.Message}");
        }
    }
}
