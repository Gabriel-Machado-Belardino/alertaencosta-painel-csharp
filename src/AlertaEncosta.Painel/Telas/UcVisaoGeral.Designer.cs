#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcVisaoGeral
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tabelaRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.tabelaIndicadores = new System.Windows.Forms.TableLayoutPanel();
            this.cartaoSetores = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.cartaoAlertas = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.cartaoSeguros = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.cartaoAjuda = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.cartaoAntecedencia = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.cartaoOcorrencias = new AlertaEncosta.Painel.Componentes.CartaoIndicador();
            this.barraAcoes = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSetores = new System.Windows.Forms.Label();
            this.btnEmitirAlerta = new System.Windows.Forms.Button();
            this.btnVerLeituras = new System.Windows.Forms.Button();
            this.btnGerarLeitura = new System.Windows.Forms.Button();
            this.divisor = new System.Windows.Forms.SplitContainer();
            this.painelSetores = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDetalhe = new System.Windows.Forms.Label();
            this.grupoFila = new System.Windows.Forms.GroupBox();
            this.gradeFila = new System.Windows.Forms.DataGridView();
            this.barraFila = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFilaSeguro = new System.Windows.Forms.Button();
            this.btnFilaAjuda = new System.Windows.Forms.Button();
            this.tabelaRaiz.SuspendLayout();
            this.tabelaIndicadores.SuspendLayout();
            this.barraAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).BeginInit();
            this.divisor.Panel1.SuspendLayout();
            this.divisor.Panel2.SuspendLayout();
            this.divisor.SuspendLayout();
            this.grupoFila.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradeFila)).BeginInit();
            this.barraFila.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabelaRaiz
            // 
            this.tabelaRaiz.ColumnCount = 1;
            this.tabelaRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabelaRaiz.Controls.Add(this.tabelaIndicadores, 0, 0);
            this.tabelaRaiz.Controls.Add(this.barraAcoes, 0, 1);
            this.tabelaRaiz.Controls.Add(this.divisor, 0, 2);
            this.tabelaRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabelaRaiz.Location = new System.Drawing.Point(0, 0);
            this.tabelaRaiz.Name = "tabelaRaiz";
            this.tabelaRaiz.RowCount = 3;
            this.tabelaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tabelaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabelaRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabelaRaiz.Size = new System.Drawing.Size(1300, 560);
            this.tabelaRaiz.TabIndex = 0;
            // 
            // tabelaIndicadores
            // 
            this.tabelaIndicadores.ColumnCount = 6;
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tabelaIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.7F));
            this.tabelaIndicadores.Controls.Add(this.cartaoSetores, 0, 0);
            this.tabelaIndicadores.Controls.Add(this.cartaoAlertas, 1, 0);
            this.tabelaIndicadores.Controls.Add(this.cartaoSeguros, 2, 0);
            this.tabelaIndicadores.Controls.Add(this.cartaoAjuda, 3, 0);
            this.tabelaIndicadores.Controls.Add(this.cartaoAntecedencia, 4, 0);
            this.tabelaIndicadores.Controls.Add(this.cartaoOcorrencias, 5, 0);
            this.tabelaIndicadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabelaIndicadores.Margin = new System.Windows.Forms.Padding(0);
            this.tabelaIndicadores.Name = "tabelaIndicadores";
            this.tabelaIndicadores.RowCount = 1;
            this.tabelaIndicadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabelaIndicadores.TabIndex = 0;
            // 
            // cartaoSetores
            // 
            this.cartaoSetores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoSetores.Name = "cartaoSetores";
            this.cartaoSetores.Titulo = "Setores em risco";
            // 
            // cartaoAlertas
            // 
            this.cartaoAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoAlertas.Name = "cartaoAlertas";
            this.cartaoAlertas.Titulo = "Alertas ativos";
            // 
            // cartaoSeguros
            // 
            this.cartaoSeguros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoSeguros.Name = "cartaoSeguros";
            this.cartaoSeguros.Titulo = "Em local seguro";
            // 
            // cartaoAjuda
            // 
            this.cartaoAjuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoAjuda.Name = "cartaoAjuda";
            this.cartaoAjuda.Titulo = "Pedidos de ajuda";
            // 
            // cartaoAntecedencia
            // 
            this.cartaoAntecedencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoAntecedencia.Name = "cartaoAntecedencia";
            this.cartaoAntecedencia.Titulo = "Antecedência média";
            // 
            // cartaoOcorrencias
            // 
            this.cartaoOcorrencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartaoOcorrencias.Name = "cartaoOcorrencias";
            this.cartaoOcorrencias.Titulo = "Ocorrências abertas";
            // 
            // barraAcoes
            // 
            this.barraAcoes.AutoSize = true;
            this.barraAcoes.Controls.Add(this.lblSetores);
            this.barraAcoes.Controls.Add(this.btnEmitirAlerta);
            this.barraAcoes.Controls.Add(this.btnVerLeituras);
            this.barraAcoes.Controls.Add(this.btnGerarLeitura);
            this.barraAcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barraAcoes.Name = "barraAcoes";
            this.barraAcoes.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.barraAcoes.TabIndex = 1;
            this.barraAcoes.WrapContents = false;
            // 
            // lblSetores
            // 
            this.lblSetores.AutoSize = true;
            this.lblSetores.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSetores.ForeColor = System.Drawing.Color.FromArgb(22, 50, 84);
            this.lblSetores.Margin = new System.Windows.Forms.Padding(3, 8, 24, 3);
            this.lblSetores.Name = "lblSetores";
            this.lblSetores.Text = "Mapa de setores (clique para selecionar)";
            // 
            // btnEmitirAlerta
            // 
            this.btnEmitirAlerta.Name = "btnEmitirAlerta";
            this.btnEmitirAlerta.TabIndex = 0;
            this.btnEmitirAlerta.Tag = "primario";
            this.btnEmitirAlerta.Text = "🔔 Emitir alerta para o setor";
            this.btnEmitirAlerta.Click += new System.EventHandler(this.btnEmitirAlerta_Click);
            // 
            // btnVerLeituras
            // 
            this.btnVerLeituras.Name = "btnVerLeituras";
            this.btnVerLeituras.TabIndex = 1;
            this.btnVerLeituras.Text = "Ver leituras do setor";
            this.btnVerLeituras.Click += new System.EventHandler(this.btnVerLeituras_Click);
            // 
            // btnGerarLeitura
            // 
            this.btnGerarLeitura.Name = "btnGerarLeitura";
            this.btnGerarLeitura.TabIndex = 2;
            this.btnGerarLeitura.Text = "Simular nova leitura (F5)";
            this.btnGerarLeitura.Click += new System.EventHandler(this.btnGerarLeitura_Click);
            // 
            // divisor
            // 
            this.divisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisor.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.divisor.Name = "divisor";
            // 
            // divisor.Panel1
            // 
            this.divisor.Panel1.Controls.Add(this.painelSetores);
            this.divisor.Panel1.Controls.Add(this.lblDetalhe);
            // 
            // divisor.Panel2
            // 
            this.divisor.Panel2.Controls.Add(this.grupoFila);
            this.divisor.Size = new System.Drawing.Size(1294, 390);
            this.divisor.SplitterDistance = 700;
            this.divisor.SplitterWidth = 8;
            this.divisor.TabIndex = 2;
            // 
            // painelSetores
            // 
            this.painelSetores.AutoScroll = true;
            this.painelSetores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelSetores.Name = "painelSetores";
            this.painelSetores.TabIndex = 0;
            // 
            // lblDetalhe
            // 
            this.lblDetalhe.BackColor = System.Drawing.Color.White;
            this.lblDetalhe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDetalhe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDetalhe.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblDetalhe.Name = "lblDetalhe";
            this.lblDetalhe.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.lblDetalhe.Size = new System.Drawing.Size(700, 64);
            this.lblDetalhe.TabIndex = 1;
            this.lblDetalhe.Text = "Selecione um setor.";
            // 
            // grupoFila
            // 
            this.grupoFila.Controls.Add(this.gradeFila);
            this.grupoFila.Controls.Add(this.barraFila);
            this.grupoFila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grupoFila.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.grupoFila.ForeColor = System.Drawing.Color.FromArgb(22, 50, 84);
            this.grupoFila.Name = "grupoFila";
            this.grupoFila.Padding = new System.Windows.Forms.Padding(8);
            this.grupoFila.TabIndex = 0;
            this.grupoFila.TabStop = false;
            this.grupoFila.Text = "Fila de busca ativa (quem atender primeiro)";
            // 
            // gradeFila
            // 
            this.gradeFila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradeFila.Name = "gradeFila";
            this.gradeFila.TabIndex = 0;
            this.gradeFila.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gradeFila_CellFormatting);
            // 
            // barraFila
            // 
            this.barraFila.AutoSize = true;
            this.barraFila.Controls.Add(this.btnFilaSeguro);
            this.barraFila.Controls.Add(this.btnFilaAjuda);
            this.barraFila.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barraFila.Name = "barraFila";
            this.barraFila.TabIndex = 1;
            // 
            // btnFilaSeguro
            // 
            this.btnFilaSeguro.Name = "btnFilaSeguro";
            this.btnFilaSeguro.TabIndex = 0;
            this.btnFilaSeguro.Text = "✔ Resgatado / em local seguro";
            this.btnFilaSeguro.Click += new System.EventHandler(this.btnFilaSeguro_Click);
            // 
            // btnFilaAjuda
            // 
            this.btnFilaAjuda.Name = "btnFilaAjuda";
            this.btnFilaAjuda.TabIndex = 1;
            this.btnFilaAjuda.Text = "Registrar pedido de ajuda";
            this.btnFilaAjuda.Click += new System.EventHandler(this.btnFilaAjuda_Click);
            // 
            // UcVisaoGeral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.tabelaRaiz);
            this.Name = "UcVisaoGeral";
            this.Size = new System.Drawing.Size(1300, 560);
            this.tabelaRaiz.ResumeLayout(false);
            this.tabelaRaiz.PerformLayout();
            this.tabelaIndicadores.ResumeLayout(false);
            this.barraAcoes.ResumeLayout(false);
            this.barraAcoes.PerformLayout();
            this.divisor.Panel1.ResumeLayout(false);
            this.divisor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).EndInit();
            this.divisor.ResumeLayout(false);
            this.grupoFila.ResumeLayout(false);
            this.grupoFila.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradeFila)).EndInit();
            this.barraFila.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabelaRaiz;
        private System.Windows.Forms.TableLayoutPanel tabelaIndicadores;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoSetores;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoAlertas;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoSeguros;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoAjuda;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoAntecedencia;
        private AlertaEncosta.Painel.Componentes.CartaoIndicador cartaoOcorrencias;
        private System.Windows.Forms.FlowLayoutPanel barraAcoes;
        private System.Windows.Forms.Label lblSetores;
        private System.Windows.Forms.Button btnEmitirAlerta;
        private System.Windows.Forms.Button btnVerLeituras;
        private System.Windows.Forms.Button btnGerarLeitura;
        private System.Windows.Forms.SplitContainer divisor;
        private System.Windows.Forms.FlowLayoutPanel painelSetores;
        private System.Windows.Forms.Label lblDetalhe;
        private System.Windows.Forms.GroupBox grupoFila;
        private System.Windows.Forms.DataGridView gradeFila;
        private System.Windows.Forms.FlowLayoutPanel barraFila;
        private System.Windows.Forms.Button btnFilaSeguro;
        private System.Windows.Forms.Button btnFilaAjuda;
    }
}
