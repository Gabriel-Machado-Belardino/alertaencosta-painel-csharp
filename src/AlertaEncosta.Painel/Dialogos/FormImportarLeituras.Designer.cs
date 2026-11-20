#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormImportarLeituras
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblInstrucoes = new System.Windows.Forms.Label();
            this.barraApi = new System.Windows.Forms.FlowLayoutPanel();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnBuscarApi = new System.Windows.Forms.Button();
            this.barraArquivo = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAbrirArquivo = new System.Windows.Forms.Button();
            this.btnExemplo = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.txtConteudo = new System.Windows.Forms.TextBox();
            this.painelResultado = new System.Windows.Forms.Panel();
            this.lstErros = new System.Windows.Forms.ListBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.painelBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dlgAbrir = new System.Windows.Forms.OpenFileDialog();
            this.barraApi.SuspendLayout();
            this.barraArquivo.SuspendLayout();
            this.painelResultado.SuspendLayout();
            this.painelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstrucoes
            // 
            this.lblInstrucoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstrucoes.Name = "lblInstrucoes";
            this.lblInstrucoes.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.lblInstrucoes.Size = new System.Drawing.Size(900, 70);
            this.lblInstrucoes.Text = "Cole o JSON enviado pela estação ESP32 (um objeto ou um array) ou as linhas \"[API] POST ... | {...}\" copiadas do Serial Monitor do Wokwi. Também dá para abrir um arquivo .json ou buscar na API REST do AlertaEncosta. As leituras são validadas antes de entrar no painel.";
            // 
            // barraApi
            // 
            this.barraApi.AutoSize = true;
            this.barraApi.Controls.Add(this.lblUrl);
            this.barraApi.Controls.Add(this.txtUrl);
            this.barraApi.Controls.Add(this.btnBuscarApi);
            this.barraApi.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraApi.Name = "barraApi";
            this.barraApi.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.barraApi.TabIndex = 0;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Text = "API (GET):";
            // 
            // txtUrl
            // 
            this.txtUrl.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(460, 25);
            this.txtUrl.TabIndex = 0;
            // 
            // btnBuscarApi
            // 
            this.btnBuscarApi.Name = "btnBuscarApi";
            this.btnBuscarApi.TabIndex = 1;
            this.btnBuscarApi.Text = "Buscar na API";
            this.btnBuscarApi.Click += new System.EventHandler(this.btnBuscarApi_Click);
            // 
            // barraArquivo
            // 
            this.barraArquivo.AutoSize = true;
            this.barraArquivo.Controls.Add(this.btnAbrirArquivo);
            this.barraArquivo.Controls.Add(this.btnExemplo);
            this.barraArquivo.Controls.Add(this.btnLimpar);
            this.barraArquivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraArquivo.Name = "barraArquivo";
            this.barraArquivo.Padding = new System.Windows.Forms.Padding(6, 0, 6, 4);
            this.barraArquivo.TabIndex = 1;
            // 
            // btnAbrirArquivo
            // 
            this.btnAbrirArquivo.Name = "btnAbrirArquivo";
            this.btnAbrirArquivo.TabIndex = 0;
            this.btnAbrirArquivo.Text = "Abrir arquivo .json...";
            this.btnAbrirArquivo.Click += new System.EventHandler(this.btnAbrirArquivo_Click);
            // 
            // btnExemplo
            // 
            this.btnExemplo.Name = "btnExemplo";
            this.btnExemplo.TabIndex = 1;
            this.btnExemplo.Text = "Colar exemplo do Serial Monitor (Wokwi)";
            this.btnExemplo.Click += new System.EventHandler(this.btnExemplo_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.TabIndex = 2;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // txtConteudo
            // 
            this.txtConteudo.AcceptsReturn = true;
            this.txtConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConteudo.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtConteudo.MaxLength = 2000000;
            this.txtConteudo.Multiline = true;
            this.txtConteudo.Name = "txtConteudo";
            this.txtConteudo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConteudo.TabIndex = 2;
            this.txtConteudo.WordWrap = false;
            this.txtConteudo.TextChanged += new System.EventHandler(this.txtConteudo_TextChanged);
            // 
            // painelResultado
            // 
            this.painelResultado.Controls.Add(this.lstErros);
            this.painelResultado.Controls.Add(this.lblResultado);
            this.painelResultado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelResultado.Name = "painelResultado";
            this.painelResultado.Padding = new System.Windows.Forms.Padding(8, 4, 8, 0);
            this.painelResultado.Size = new System.Drawing.Size(900, 150);
            this.painelResultado.TabIndex = 3;
            // 
            // lstErros
            // 
            this.lstErros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErros.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.lstErros.HorizontalScrollbar = true;
            this.lstErros.IntegralHeight = false;
            this.lstErros.Name = "lstErros";
            this.lstErros.TabIndex = 1;
            // 
            // lblResultado
            // 
            this.lblResultado.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(884, 30);
            this.lblResultado.Text = "Nada para importar ainda.";
            // 
            // painelBotoes
            // 
            this.painelBotoes.AutoSize = true;
            this.painelBotoes.Controls.Add(this.btnImportar);
            this.painelBotoes.Controls.Add(this.btnCancelar);
            this.painelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelBotoes.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.painelBotoes.Name = "painelBotoes";
            this.painelBotoes.Padding = new System.Windows.Forms.Padding(8);
            this.painelBotoes.TabIndex = 4;
            // 
            // btnImportar
            // 
            this.btnImportar.Enabled = false;
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.TabIndex = 0;
            this.btnImportar.Tag = "primario";
            this.btnImportar.Text = "Importar leituras válidas";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // dlgAbrir
            // 
            this.dlgAbrir.Filter = "Leituras JSON (*.json;*.txt;*.log)|*.json;*.txt;*.log|Todos os arquivos (*.*)|*.*";
            this.dlgAbrir.Title = "Abrir leituras da estação";
            // 
            // FormImportarLeituras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.txtConteudo);
            this.Controls.Add(this.painelResultado);
            this.Controls.Add(this.painelBotoes);
            this.Controls.Add(this.barraArquivo);
            this.Controls.Add(this.barraApi);
            this.Controls.Add(this.lblInstrucoes);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "FormImportarLeituras";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importar leituras da estação ESP32";
            this.barraApi.ResumeLayout(false);
            this.barraApi.PerformLayout();
            this.barraArquivo.ResumeLayout(false);
            this.barraArquivo.PerformLayout();
            this.painelResultado.ResumeLayout(false);
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblInstrucoes;
        private System.Windows.Forms.FlowLayoutPanel barraApi;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnBuscarApi;
        private System.Windows.Forms.FlowLayoutPanel barraArquivo;
        private System.Windows.Forms.Button btnAbrirArquivo;
        private System.Windows.Forms.Button btnExemplo;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.TextBox txtConteudo;
        private System.Windows.Forms.Panel painelResultado;
        private System.Windows.Forms.ListBox lstErros;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.OpenFileDialog dlgAbrir;
    }
}
