using System.Globalization;
using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Painel.Componentes;

namespace AlertaEncosta.Painel.Dialogos;

/// <summary>
/// Integração com a estação ESP32 (Physical Computing) e com a API REST (SOA):
/// interpreta o JSON, valida cada leitura e só libera a importação do que é válido.
/// </summary>
public partial class FormImportarLeituras : Form
{
    private readonly PainelContexto _ctx;
    private List<LeituraSensor> _validas = new();

    public IReadOnlyList<LeituraSensor> Leituras => _validas;

    public FormImportarLeituras(PainelContexto contexto)
    {
        _ctx = contexto;
        InitializeComponent();
        Tema.Aplicar(this);
        txtUrl.Text = _ctx.UrlApi;
    }

    private void Validar()
    {
        var resultado = _ctx.Importador.Interpretar(txtConteudo.Text, DateTime.Now);
        var erros = resultado.Erros.ToList();

        // Leituras de setores que o painel não conhece seriam recusadas no registro: avisa já.
        _validas = new List<LeituraSensor>();
        foreach (var l in resultado.Leituras)
        {
            if (_ctx.Dados.Setores.Existe(l.SetorId)) _validas.Add(l);
            else erros.Add($"Setor {l.SetorId} (estação {l.EstacaoId}) não está cadastrado no painel.");
        }

        lstErros.Items.Clear();
        foreach (var e in erros) lstErros.Items.Add("✖ " + e);
        if (erros.Count == 0 && !string.IsNullOrWhiteSpace(txtConteudo.Text))
            lstErros.Items.Add("Nenhum problema encontrado. Linhas sem JSON ([LEITURA], [WIFI]...) são ignoradas.");
        lstErros.ForeColor = erros.Count == 0 ? Tema.TextoSecundario : Tema.Perigo;

        if (string.IsNullOrWhiteSpace(txtConteudo.Text))
        {
            lblResultado.Text = "Nada para importar ainda.";
            lblResultado.ForeColor = Tema.TextoSecundario;
        }
        else
        {
            var calc = _ctx.Monitoramento.Calculadora;
            var altos = _validas.Count(l => calc.Avaliar(l).Nivel == NivelRisco.Alto);
            lblResultado.Text = $"✔ {_validas.Count} leitura(s) válida(s)" +
                                (altos > 0 ? $" ({altos} em risco ALTO)" : string.Empty) +
                                (erros.Count > 0 ? $" · ✖ {erros.Count} problema(s)" : string.Empty);
            lblResultado.ForeColor = _validas.Count > 0 ? Tema.Sucesso : Tema.Perigo;
        }
        btnImportar.Enabled = _validas.Count > 0;
        btnImportar.Text = _validas.Count > 0 ? $"Importar {_validas.Count} leitura(s)" : "Importar leituras válidas";
    }

    private void txtConteudo_TextChanged(object? sender, EventArgs e) => Validar();

    private void btnAbrirArquivo_Click(object? sender, EventArgs e)
    {
        if (dlgAbrir.ShowDialog(this) != DialogResult.OK) return;
        Ui.Executar(this, () =>
        {
            var info = new FileInfo(dlgAbrir.FileName);
            if (info.Length > 5 * 1024 * 1024) throw new DominioException("Arquivo muito grande (máximo 5 MB).");
            txtConteudo.Text = File.ReadAllText(info.FullName);
        });
    }

    /// <summary>Exemplo no formato exato do firmware (linhas do Serial Monitor), com horário atual.</summary>
    private void btnExemplo_Click(object? sender, EventArgs e)
    {
        string Linha(int segundosAtras, double chuva, int solo, double nivel, string risco) =>
            $"[API] POST http://httpbin.org/post -> HTTP 200 | {{\"estacaoId\":\"EST-BRAS-01\",\"setorId\":\"BRAS-S01\"," +
            $"\"timestamp\":\"{DateTime.UtcNow.AddSeconds(-segundosAtras):yyyy-MM-ddTHH:mm:ssZ}\",\"chuvaMm\":{F(chuva)}," +
            $"\"chuvaTotalMm\":{F(chuva)},\"umidadeSoloPct\":{solo},\"nivelAguaCm\":{F(nivel)},\"riscoLocal\":\"{risco}\",\"uptimeMs\":{90000 - segundosAtras * 1000}}}";

        txtConteudo.Text = string.Join(Environment.NewLine,
            "=== AlertaEncosta - estacao EST-BRAS-01 (setor BRAS-S01) ===",
            "[WIFI] conectado, IP 10.10.0.2",
            Linha(24, 4.0, 52, 18.2, "BAIXO"),
            "[LEITURA] chuva=11.0mm solo=63% nivel=31.5cm risco=MEDIO fila=0",
            "[ALERTA] risco subiu: BAIXO -> MEDIO",
            Linha(16, 11.0, 63, 31.5, "MEDIO"),
            Linha(8, 16.0, 84, 58.0, "ALTO"),
            "[ALERTA] risco subiu: MEDIO -> ALTO",
            Linha(1, 22.0, 87, 96.4, "ALTO"));
    }

    private static string F(double v) => v.ToString("0.0", CultureInfo.InvariantCulture);

    /// <summary>Usado pela captura de telas da documentação.</summary>
    internal void ColarExemplo() => btnExemplo_Click(this, EventArgs.Empty);

    private void btnLimpar_Click(object? sender, EventArgs e) => txtConteudo.Clear();

    private async void btnBuscarApi_Click(object? sender, EventArgs e)
    {
        btnBuscarApi.Enabled = false;
        UseWaitCursor = true;
        lblResultado.Text = "Consultando a API...";
        lblResultado.ForeColor = Tema.TextoSecundario;
        try
        {
            var corpo = await _ctx.ClienteApi.BuscarTextoAsync(txtUrl.Text);
            _ctx.UrlApi = txtUrl.Text.Trim();
            txtConteudo.Text = corpo;
        }
        catch (Exception ex)
        {
            lblResultado.Text = "Não foi possível consultar a API.";
            lblResultado.ForeColor = Tema.Perigo;
            Ui.MostrarErro(this, ex);
        }
        finally
        {
            UseWaitCursor = false;
            btnBuscarApi.Enabled = true;
        }
    }

    private void btnImportar_Click(object? sender, EventArgs e)
    {
        if (_validas.Count == 0) return;
        DialogResult = DialogResult.OK;
    }
}
