#nullable disable
namespace AlertaEncosta.Painel
{
    partial class FormPrincipal
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
            this.components = new System.ComponentModel.Container();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.menuArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAbrirPasta = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRestaurar = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorArquivo = new System.Windows.Forms.ToolStripSeparator();
            this.itemSair = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIntegracao = new System.Windows.Forms.ToolStripMenuItem();
            this.itemImportar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSimulacao = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGerarAgora = new System.Windows.Forms.ToolStripMenuItem();
            this.itemModoDemo = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorSimulacao = new System.Windows.Forms.ToolStripSeparator();
            this.itemCenarioEstiagem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCenarioModerada = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCenarioTempestade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAjuda = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSobre = new System.Windows.Forms.ToolStripMenuItem();
            this.painelCabecalho = new System.Windows.Forms.Panel();
            this.lblRelogio = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.painelAviso = new System.Windows.Forms.Panel();
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnEmitirRecomendados = new System.Windows.Forms.Button();
            this.abas = new System.Windows.Forms.TabControl();
            this.abaVisaoGeral = new System.Windows.Forms.TabPage();
            this.ucVisaoGeral = new AlertaEncosta.Painel.Telas.UcVisaoGeral();
            this.abaLeituras = new System.Windows.Forms.TabPage();
            this.ucLeituras = new AlertaEncosta.Painel.Telas.UcLeituras();
            this.abaAlertas = new System.Windows.Forms.TabPage();
            this.ucAlertas = new AlertaEncosta.Painel.Telas.UcAlertas();
            this.abaMoradores = new System.Windows.Forms.TabPage();
            this.ucMoradores = new AlertaEncosta.Painel.Telas.UcMoradores();
            this.abaPontosApoio = new System.Windows.Forms.TabPage();
            this.ucPontosApoio = new AlertaEncosta.Painel.Telas.UcPontosApoio();
            this.abaOcorrencias = new System.Windows.Forms.TabPage();
            this.ucOcorrencias = new AlertaEncosta.Painel.Telas.UcOcorrencias();
            this.barraStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDemo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPasta = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerRelogio = new System.Windows.Forms.Timer(this.components);
            this.timerDemo = new System.Windows.Forms.Timer(this.components);
            this.menuPrincipal.SuspendLayout();
            this.painelCabecalho.SuspendLayout();
            this.painelAviso.SuspendLayout();
            this.abas.SuspendLayout();
            this.abaVisaoGeral.SuspendLayout();
            this.abaLeituras.SuspendLayout();
            this.abaAlertas.SuspendLayout();
            this.abaMoradores.SuspendLayout();
            this.abaPontosApoio.SuspendLayout();
            this.abaOcorrencias.SuspendLayout();
            this.barraStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArquivo,
            this.menuIntegracao,
            this.menuSimulacao,
            this.menuAjuda});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1360, 27);
            this.menuPrincipal.TabIndex = 0;
            // 
            // menuArquivo
            // 
            this.menuArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAbrirPasta,
            this.itemRestaurar,
            this.separadorArquivo,
            this.itemSair});
            this.menuArquivo.Name = "menuArquivo";
            this.menuArquivo.Text = "&Arquivo";
            // 
            // itemAbrirPasta
            // 
            this.itemAbrirPasta.Name = "itemAbrirPasta";
            this.itemAbrirPasta.Text = "Abrir pasta de &dados";
            this.itemAbrirPasta.Click += new System.EventHandler(this.itemAbrirPasta_Click);
            // 
            // itemRestaurar
            // 
            this.itemRestaurar.Name = "itemRestaurar";
            this.itemRestaurar.Text = "&Restaurar dados de exemplo...";
            this.itemRestaurar.Click += new System.EventHandler(this.itemRestaurar_Click);
            // 
            // separadorArquivo
            // 
            this.separadorArquivo.Name = "separadorArquivo";
            // 
            // itemSair
            // 
            this.itemSair.Name = "itemSair";
            this.itemSair.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.itemSair.Text = "&Sair";
            this.itemSair.Click += new System.EventHandler(this.itemSair_Click);
            // 
            // menuIntegracao
            // 
            this.menuIntegracao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemImportar});
            this.menuIntegracao.Name = "menuIntegracao";
            this.menuIntegracao.Text = "&Integração";
            // 
            // itemImportar
            // 
            this.itemImportar.Name = "itemImportar";
            this.itemImportar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.itemImportar.Text = "&Importar leituras (ESP32 / Wokwi / API)...";
            this.itemImportar.Click += new System.EventHandler(this.itemImportar_Click);
            // 
            // menuSimulacao
            // 
            this.menuSimulacao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemGerarAgora,
            this.itemModoDemo,
            this.separadorSimulacao,
            this.itemCenarioEstiagem,
            this.itemCenarioModerada,
            this.itemCenarioTempestade});
            this.menuSimulacao.Name = "menuSimulacao";
            this.menuSimulacao.Text = "&Simulação";
            // 
            // itemGerarAgora
            // 
            this.itemGerarAgora.Name = "itemGerarAgora";
            this.itemGerarAgora.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.itemGerarAgora.Text = "&Gerar uma leitura por setor agora";
            this.itemGerarAgora.Click += new System.EventHandler(this.itemGerarAgora_Click);
            // 
            // itemModoDemo
            // 
            this.itemModoDemo.CheckOnClick = true;
            this.itemModoDemo.Name = "itemModoDemo";
            this.itemModoDemo.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.itemModoDemo.Text = "&Modo demonstração (leitura a cada 4 s)";
            this.itemModoDemo.CheckedChanged += new System.EventHandler(this.itemModoDemo_CheckedChanged);
            // 
            // separadorSimulacao
            // 
            this.separadorSimulacao.Name = "separadorSimulacao";
            // 
            // itemCenarioEstiagem
            // 
            this.itemCenarioEstiagem.Name = "itemCenarioEstiagem";
            this.itemCenarioEstiagem.Text = "Cenário: &Estiagem";
            this.itemCenarioEstiagem.Click += new System.EventHandler(this.itemCenario_Click);
            // 
            // itemCenarioModerada
            // 
            this.itemCenarioModerada.Name = "itemCenarioModerada";
            this.itemCenarioModerada.Text = "Cenário: Chuva &moderada";
            this.itemCenarioModerada.Click += new System.EventHandler(this.itemCenario_Click);
            // 
            // itemCenarioTempestade
            // 
            this.itemCenarioTempestade.Checked = true;
            this.itemCenarioTempestade.Name = "itemCenarioTempestade";
            this.itemCenarioTempestade.Text = "Cenário: &Tempestade";
            this.itemCenarioTempestade.Click += new System.EventHandler(this.itemCenario_Click);
            // 
            // menuAjuda
            // 
            this.menuAjuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSobre});
            this.menuAjuda.Name = "menuAjuda";
            this.menuAjuda.Text = "A&juda";
            // 
            // itemSobre
            // 
            this.itemSobre.Name = "itemSobre";
            this.itemSobre.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.itemSobre.Text = "&Sobre o AlertaEncosta";
            this.itemSobre.Click += new System.EventHandler(this.itemSobre_Click);
            // 
            // painelCabecalho
            // 
            this.painelCabecalho.BackColor = System.Drawing.Color.FromArgb(22, 50, 84);
            this.painelCabecalho.Controls.Add(this.lblRelogio);
            this.painelCabecalho.Controls.Add(this.lblSubtitulo);
            this.painelCabecalho.Controls.Add(this.lblTitulo);
            this.painelCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelCabecalho.Location = new System.Drawing.Point(0, 27);
            this.painelCabecalho.Name = "painelCabecalho";
            this.painelCabecalho.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.painelCabecalho.Size = new System.Drawing.Size(1360, 70);
            this.painelCabecalho.TabIndex = 1;
            // 
            // lblRelogio
            // 
            this.lblRelogio.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRelogio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRelogio.ForeColor = System.Drawing.Color.White;
            this.lblRelogio.Location = new System.Drawing.Point(1004, 6);
            this.lblRelogio.Name = "lblRelogio";
            this.lblRelogio.Size = new System.Drawing.Size(340, 58);
            this.lblRelogio.TabIndex = 2;
            this.lblRelogio.Text = "00:00:00";
            this.lblRelogio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(200, 214, 232);
            this.lblSubtitulo.Location = new System.Drawing.Point(19, 41);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Vila Brasilândia · São Paulo/SP · monitoramento de encostas e córregos (dados simulados)";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(16, 6);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "AlertaEncosta · Painel da Defesa Civil";
            // 
            // painelAviso
            // 
            this.painelAviso.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.painelAviso.Controls.Add(this.lblAviso);
            this.painelAviso.Controls.Add(this.btnEmitirRecomendados);
            this.painelAviso.Dock = System.Windows.Forms.DockStyle.Top;
            this.painelAviso.Location = new System.Drawing.Point(0, 97);
            this.painelAviso.Name = "painelAviso";
            this.painelAviso.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.painelAviso.Size = new System.Drawing.Size(1360, 50);
            this.painelAviso.TabIndex = 2;
            this.painelAviso.Visible = false;
            // 
            // lblAviso
            // 
            this.lblAviso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAviso.ForeColor = System.Drawing.Color.White;
            this.lblAviso.Location = new System.Drawing.Point(16, 6);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(1088, 38);
            this.lblAviso.TabIndex = 0;
            this.lblAviso.Text = "Alerta recomendado";
            this.lblAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEmitirRecomendados
            // 
            this.btnEmitirRecomendados.BackColor = System.Drawing.Color.White;
            this.btnEmitirRecomendados.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEmitirRecomendados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmitirRecomendados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEmitirRecomendados.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnEmitirRecomendados.Location = new System.Drawing.Point(1104, 6);
            this.btnEmitirRecomendados.Name = "btnEmitirRecomendados";
            this.btnEmitirRecomendados.Size = new System.Drawing.Size(240, 38);
            this.btnEmitirRecomendados.TabIndex = 1;
            this.btnEmitirRecomendados.Text = "Emitir alerta agora";
            this.btnEmitirRecomendados.UseVisualStyleBackColor = false;
            this.btnEmitirRecomendados.Click += new System.EventHandler(this.btnEmitirRecomendados_Click);
            // 
            // abas
            // 
            this.abas.Controls.Add(this.abaVisaoGeral);
            this.abas.Controls.Add(this.abaLeituras);
            this.abas.Controls.Add(this.abaAlertas);
            this.abas.Controls.Add(this.abaMoradores);
            this.abas.Controls.Add(this.abaPontosApoio);
            this.abas.Controls.Add(this.abaOcorrencias);
            this.abas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abas.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.abas.ItemSize = new System.Drawing.Size(150, 34);
            this.abas.Location = new System.Drawing.Point(0, 147);
            this.abas.Name = "abas";
            this.abas.Padding = new System.Drawing.Point(14, 6);
            this.abas.SelectedIndex = 0;
            this.abas.Size = new System.Drawing.Size(1360, 591);
            this.abas.TabIndex = 3;
            // 
            // abaVisaoGeral
            // 
            this.abaVisaoGeral.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaVisaoGeral.Controls.Add(this.ucVisaoGeral);
            this.abaVisaoGeral.Name = "abaVisaoGeral";
            this.abaVisaoGeral.Padding = new System.Windows.Forms.Padding(8);
            this.abaVisaoGeral.TabIndex = 0;
            this.abaVisaoGeral.Text = "Visão geral";
            // 
            // ucVisaoGeral
            // 
            this.ucVisaoGeral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVisaoGeral.Name = "ucVisaoGeral";
            this.ucVisaoGeral.TabIndex = 0;
            // 
            // abaLeituras
            // 
            this.abaLeituras.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaLeituras.Controls.Add(this.ucLeituras);
            this.abaLeituras.Name = "abaLeituras";
            this.abaLeituras.Padding = new System.Windows.Forms.Padding(8);
            this.abaLeituras.TabIndex = 1;
            this.abaLeituras.Text = "Leituras dos sensores";
            // 
            // ucLeituras
            // 
            this.ucLeituras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLeituras.Name = "ucLeituras";
            this.ucLeituras.TabIndex = 0;
            // 
            // abaAlertas
            // 
            this.abaAlertas.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaAlertas.Controls.Add(this.ucAlertas);
            this.abaAlertas.Name = "abaAlertas";
            this.abaAlertas.Padding = new System.Windows.Forms.Padding(8);
            this.abaAlertas.TabIndex = 2;
            this.abaAlertas.Text = "Alertas";
            // 
            // ucAlertas
            // 
            this.ucAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAlertas.Name = "ucAlertas";
            this.ucAlertas.TabIndex = 0;
            // 
            // abaMoradores
            // 
            this.abaMoradores.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaMoradores.Controls.Add(this.ucMoradores);
            this.abaMoradores.Name = "abaMoradores";
            this.abaMoradores.Padding = new System.Windows.Forms.Padding(8);
            this.abaMoradores.TabIndex = 3;
            this.abaMoradores.Text = "Moradores";
            // 
            // ucMoradores
            // 
            this.ucMoradores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMoradores.Name = "ucMoradores";
            this.ucMoradores.TabIndex = 0;
            // 
            // abaPontosApoio
            // 
            this.abaPontosApoio.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaPontosApoio.Controls.Add(this.ucPontosApoio);
            this.abaPontosApoio.Name = "abaPontosApoio";
            this.abaPontosApoio.Padding = new System.Windows.Forms.Padding(8);
            this.abaPontosApoio.TabIndex = 4;
            this.abaPontosApoio.Text = "Pontos de apoio";
            // 
            // ucPontosApoio
            // 
            this.ucPontosApoio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPontosApoio.Name = "ucPontosApoio";
            this.ucPontosApoio.TabIndex = 0;
            // 
            // abaOcorrencias
            // 
            this.abaOcorrencias.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.abaOcorrencias.Controls.Add(this.ucOcorrencias);
            this.abaOcorrencias.Name = "abaOcorrencias";
            this.abaOcorrencias.Padding = new System.Windows.Forms.Padding(8);
            this.abaOcorrencias.TabIndex = 5;
            this.abaOcorrencias.Text = "Ocorrências";
            // 
            // ucOcorrencias
            // 
            this.ucOcorrencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOcorrencias.Name = "ucOcorrencias";
            this.ucOcorrencias.TabIndex = 0;
            // 
            // barraStatus
            // 
            this.barraStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblDemo,
            this.lblPasta});
            this.barraStatus.Location = new System.Drawing.Point(0, 738);
            this.barraStatus.Name = "barraStatus";
            this.barraStatus.Size = new System.Drawing.Size(1360, 26);
            this.barraStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Pronto.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDemo
            // 
            this.lblDemo.Name = "lblDemo";
            this.lblDemo.Text = "Simulação desligada";
            // 
            // lblPasta
            // 
            this.lblPasta.Name = "lblPasta";
            this.lblPasta.Text = "Dados:";
            // 
            // timerRelogio
            // 
            this.timerRelogio.Enabled = true;
            this.timerRelogio.Interval = 1000;
            this.timerRelogio.Tick += new System.EventHandler(this.timerRelogio_Tick);
            // 
            // timerDemo
            // 
            this.timerDemo.Interval = 4000;
            this.timerDemo.Tick += new System.EventHandler(this.timerDemo_Tick);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.ClientSize = new System.Drawing.Size(1360, 764);
            this.Controls.Add(this.abas);
            this.Controls.Add(this.painelAviso);
            this.Controls.Add(this.painelCabecalho);
            this.Controls.Add(this.barraStatus);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlertaEncosta · Painel da Defesa Civil";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.painelCabecalho.ResumeLayout(false);
            this.painelCabecalho.PerformLayout();
            this.painelAviso.ResumeLayout(false);
            this.abas.ResumeLayout(false);
            this.abaVisaoGeral.ResumeLayout(false);
            this.abaLeituras.ResumeLayout(false);
            this.abaAlertas.ResumeLayout(false);
            this.abaMoradores.ResumeLayout(false);
            this.abaPontosApoio.ResumeLayout(false);
            this.abaOcorrencias.ResumeLayout(false);
            this.barraStatus.ResumeLayout(false);
            this.barraStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuArquivo;
        private System.Windows.Forms.ToolStripMenuItem itemAbrirPasta;
        private System.Windows.Forms.ToolStripMenuItem itemRestaurar;
        private System.Windows.Forms.ToolStripSeparator separadorArquivo;
        private System.Windows.Forms.ToolStripMenuItem itemSair;
        private System.Windows.Forms.ToolStripMenuItem menuIntegracao;
        private System.Windows.Forms.ToolStripMenuItem itemImportar;
        private System.Windows.Forms.ToolStripMenuItem menuSimulacao;
        private System.Windows.Forms.ToolStripMenuItem itemGerarAgora;
        private System.Windows.Forms.ToolStripMenuItem itemModoDemo;
        private System.Windows.Forms.ToolStripSeparator separadorSimulacao;
        private System.Windows.Forms.ToolStripMenuItem itemCenarioEstiagem;
        private System.Windows.Forms.ToolStripMenuItem itemCenarioModerada;
        private System.Windows.Forms.ToolStripMenuItem itemCenarioTempestade;
        private System.Windows.Forms.ToolStripMenuItem menuAjuda;
        private System.Windows.Forms.ToolStripMenuItem itemSobre;
        private System.Windows.Forms.Panel painelCabecalho;
        private System.Windows.Forms.Label lblRelogio;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel painelAviso;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Button btnEmitirRecomendados;
        private System.Windows.Forms.TabControl abas;
        private System.Windows.Forms.TabPage abaVisaoGeral;
        private AlertaEncosta.Painel.Telas.UcVisaoGeral ucVisaoGeral;
        private System.Windows.Forms.TabPage abaLeituras;
        private AlertaEncosta.Painel.Telas.UcLeituras ucLeituras;
        private System.Windows.Forms.TabPage abaAlertas;
        private AlertaEncosta.Painel.Telas.UcAlertas ucAlertas;
        private System.Windows.Forms.TabPage abaMoradores;
        private AlertaEncosta.Painel.Telas.UcMoradores ucMoradores;
        private System.Windows.Forms.TabPage abaPontosApoio;
        private AlertaEncosta.Painel.Telas.UcPontosApoio ucPontosApoio;
        private System.Windows.Forms.TabPage abaOcorrencias;
        private AlertaEncosta.Painel.Telas.UcOcorrencias ucOcorrencias;
        private System.Windows.Forms.StatusStrip barraStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblDemo;
        private System.Windows.Forms.ToolStripStatusLabel lblPasta;
        private System.Windows.Forms.Timer timerRelogio;
        private System.Windows.Forms.Timer timerDemo;
    }
}
