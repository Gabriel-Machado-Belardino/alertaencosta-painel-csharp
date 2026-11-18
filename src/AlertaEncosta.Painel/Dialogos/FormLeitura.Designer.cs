#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormLeitura
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
            this.tabela = new System.Windows.Forms.TableLayoutPanel();
            this.lblSetor = new System.Windows.Forms.Label();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.lblEstacao = new System.Windows.Forms.Label();
            this.txtEstacao = new System.Windows.Forms.TextBox();
            this.lblDataHora = new System.Windows.Forms.Label();
            this.dtpDataHora = new System.Windows.Forms.DateTimePicker();
            this.lblChuva = new System.Windows.Forms.Label();
            this.numChuva = new System.Windows.Forms.NumericUpDown();
            this.lblChuvaTotal = new System.Windows.Forms.Label();
            this.numChuvaTotal = new System.Windows.Forms.NumericUpDown();
            this.lblSolo = new System.Windows.Forms.Label();
            this.numSolo = new System.Windows.Forms.NumericUpDown();
            this.lblNivel = new System.Windows.Forms.Label();
            this.numNivel = new System.Windows.Forms.NumericUpDown();
            this.painelPrevia = new System.Windows.Forms.Panel();
            this.lblPreviaMotivos = new System.Windows.Forms.Label();
            this.lblPreviaNivel = new System.Windows.Forms.Label();
            this.painelBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChuva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuvaTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSolo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNivel)).BeginInit();
            this.painelPrevia.SuspendLayout();
            this.painelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabela
            // 
            this.tabela.AutoSize = true;
            this.tabela.ColumnCount = 2;
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabela.Controls.Add(this.lblSetor, 0, 0);
            this.tabela.Controls.Add(this.cmbSetor, 1, 0);
            this.tabela.Controls.Add(this.lblEstacao, 0, 1);
            this.tabela.Controls.Add(this.txtEstacao, 1, 1);
            this.tabela.Controls.Add(this.lblDataHora, 0, 2);
            this.tabela.Controls.Add(this.dtpDataHora, 1, 2);
            this.tabela.Controls.Add(this.lblChuva, 0, 3);
            this.tabela.Controls.Add(this.numChuva, 1, 3);
            this.tabela.Controls.Add(this.lblChuvaTotal, 0, 4);
            this.tabela.Controls.Add(this.numChuvaTotal, 1, 4);
            this.tabela.Controls.Add(this.lblSolo, 0, 5);
            this.tabela.Controls.Add(this.numSolo, 1, 5);
            this.tabela.Controls.Add(this.lblNivel, 0, 6);
            this.tabela.Controls.Add(this.numNivel, 1, 6);
            this.tabela.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabela.Name = "tabela";
            this.tabela.Padding = new System.Windows.Forms.Padding(12, 12, 12, 4);
            this.tabela.RowCount = 7;
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.TabIndex = 0;
            // 
            // lblSetor
            // 
            this.lblSetor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSetor.AutoSize = true;
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Text = "Setor:";
            // 
            // cmbSetor
            // 
            this.cmbSetor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSetor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.TabIndex = 0;
            this.cmbSetor.SelectedIndexChanged += new System.EventHandler(this.cmbSetor_SelectedIndexChanged);
            // 
            // lblEstacao
            // 
            this.lblEstacao.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstacao.AutoSize = true;
            this.lblEstacao.Name = "lblEstacao";
            this.lblEstacao.Text = "Estação:";
            // 
            // txtEstacao
            // 
            this.txtEstacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEstacao.Name = "txtEstacao";
            this.txtEstacao.ReadOnly = true;
            this.txtEstacao.TabStop = false;
            // 
            // lblDataHora
            // 
            this.lblDataHora.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDataHora.AutoSize = true;
            this.lblDataHora.Name = "lblDataHora";
            this.lblDataHora.Text = "Data e hora da medição:";
            // 
            // dtpDataHora
            // 
            this.dtpDataHora.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpDataHora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDataHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataHora.Name = "dtpDataHora";
            this.dtpDataHora.TabIndex = 1;
            // 
            // lblChuva
            // 
            this.lblChuva.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblChuva.AutoSize = true;
            this.lblChuva.Name = "lblChuva";
            this.lblChuva.Text = "Chuva na última hora (mm):";
            // 
            // numChuva
            // 
            this.numChuva.DecimalPlaces = 1;
            this.numChuva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numChuva.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            this.numChuva.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numChuva.Name = "numChuva";
            this.numChuva.TabIndex = 2;
            this.numChuva.ValueChanged += new System.EventHandler(this.valor_Changed);
            // 
            // lblChuvaTotal
            // 
            this.lblChuvaTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblChuvaTotal.AutoSize = true;
            this.lblChuvaTotal.Name = "lblChuvaTotal";
            this.lblChuvaTotal.Text = "Chuva acumulada total (mm):";
            // 
            // numChuvaTotal
            // 
            this.numChuvaTotal.DecimalPlaces = 1;
            this.numChuvaTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numChuvaTotal.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numChuvaTotal.Name = "numChuvaTotal";
            this.numChuvaTotal.TabIndex = 3;
            // 
            // lblSolo
            // 
            this.lblSolo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSolo.AutoSize = true;
            this.lblSolo.Name = "lblSolo";
            this.lblSolo.Text = "Umidade do solo (%):";
            // 
            // numSolo
            // 
            this.numSolo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSolo.Name = "numSolo";
            this.numSolo.TabIndex = 4;
            this.numSolo.Value = new decimal(new int[] { 40, 0, 0, 0 });
            this.numSolo.ValueChanged += new System.EventHandler(this.valor_Changed);
            // 
            // lblNivel
            // 
            this.lblNivel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNivel.AutoSize = true;
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Text = "Nível do córrego (cm):";
            // 
            // numNivel
            // 
            this.numNivel.DecimalPlaces = 1;
            this.numNivel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numNivel.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
            this.numNivel.Name = "numNivel";
            this.numNivel.TabIndex = 5;
            this.numNivel.Value = new decimal(new int[] { 20, 0, 0, 0 });
            this.numNivel.ValueChanged += new System.EventHandler(this.valor_Changed);
            // 
            // painelPrevia
            // 
            this.painelPrevia.BackColor = System.Drawing.Color.White;
            this.painelPrevia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.painelPrevia.Controls.Add(this.lblPreviaMotivos);
            this.painelPrevia.Controls.Add(this.lblPreviaNivel);
            this.painelPrevia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelPrevia.Name = "painelPrevia";
            this.painelPrevia.TabIndex = 1;
            // 
            // lblPreviaMotivos
            // 
            this.lblPreviaMotivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreviaMotivos.Name = "lblPreviaMotivos";
            this.lblPreviaMotivos.Padding = new System.Windows.Forms.Padding(10, 8, 10, 4);
            this.lblPreviaMotivos.Text = "";
            // 
            // lblPreviaNivel
            // 
            this.lblPreviaNivel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPreviaNivel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPreviaNivel.Name = "lblPreviaNivel";
            this.lblPreviaNivel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblPreviaNivel.Size = new System.Drawing.Size(560, 44);
            this.lblPreviaNivel.Text = "Prévia do risco";
            this.lblPreviaNivel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // painelBotoes
            // 
            this.painelBotoes.AutoSize = true;
            this.painelBotoes.Controls.Add(this.btnSalvar);
            this.painelBotoes.Controls.Add(this.btnCancelar);
            this.painelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelBotoes.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.painelBotoes.Name = "painelBotoes";
            this.painelBotoes.Padding = new System.Windows.Forms.Padding(8);
            this.painelBotoes.TabIndex = 2;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Tag = "primario";
            this.btnSalvar.Text = "Registrar leitura";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // FormLeitura
            // 
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(580, 560);
            this.Controls.Add(this.painelPrevia);
            this.Controls.Add(this.painelBotoes);
            this.Controls.Add(this.tabela);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLeitura";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nova leitura manual";
            this.tabela.ResumeLayout(false);
            this.tabela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChuva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuvaTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSolo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNivel)).EndInit();
            this.painelPrevia.ResumeLayout(false);
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabela;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblEstacao;
        private System.Windows.Forms.TextBox txtEstacao;
        private System.Windows.Forms.Label lblDataHora;
        private System.Windows.Forms.DateTimePicker dtpDataHora;
        private System.Windows.Forms.Label lblChuva;
        private System.Windows.Forms.NumericUpDown numChuva;
        private System.Windows.Forms.Label lblChuvaTotal;
        private System.Windows.Forms.NumericUpDown numChuvaTotal;
        private System.Windows.Forms.Label lblSolo;
        private System.Windows.Forms.NumericUpDown numSolo;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.NumericUpDown numNivel;
        private System.Windows.Forms.Panel painelPrevia;
        private System.Windows.Forms.Label lblPreviaMotivos;
        private System.Windows.Forms.Label lblPreviaNivel;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
