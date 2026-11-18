#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormPontoApoio
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
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.numLatitude = new System.Windows.Forms.NumericUpDown();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.numLongitude = new System.Windows.Forms.NumericUpDown();
            this.lblCapacidade = new System.Windows.Forms.Label();
            this.numCapacidade = new System.Windows.Forms.NumericUpDown();
            this.chkAcessivel = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.painelBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLatitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidade)).BeginInit();
            this.painelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabela
            // 
            this.tabela.ColumnCount = 2;
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabela.Controls.Add(this.lblCodigo, 0, 0);
            this.tabela.Controls.Add(this.txtCodigo, 1, 0);
            this.tabela.Controls.Add(this.lblNome, 0, 1);
            this.tabela.Controls.Add(this.txtNome, 1, 1);
            this.tabela.Controls.Add(this.lblEndereco, 0, 2);
            this.tabela.Controls.Add(this.txtEndereco, 1, 2);
            this.tabela.Controls.Add(this.lblLatitude, 0, 3);
            this.tabela.Controls.Add(this.numLatitude, 1, 3);
            this.tabela.Controls.Add(this.lblLongitude, 0, 4);
            this.tabela.Controls.Add(this.numLongitude, 1, 4);
            this.tabela.Controls.Add(this.lblCapacidade, 0, 5);
            this.tabela.Controls.Add(this.numCapacidade, 1, 5);
            this.tabela.Controls.Add(this.chkAcessivel, 1, 6);
            this.tabela.Controls.Add(this.chkAtivo, 1, 7);
            this.tabela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabela.Name = "tabela";
            this.tabela.Padding = new System.Windows.Forms.Padding(12);
            this.tabela.RowCount = 8;
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(120, 25);
            this.txtCodigo.TabIndex = 0;
            // 
            // lblNome
            // 
            this.lblNome.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNome.AutoSize = true;
            this.lblNome.Name = "lblNome";
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNome.MaxLength = 80;
            this.txtNome.Name = "txtNome";
            this.txtNome.TabIndex = 1;
            // 
            // lblEndereco
            // 
            this.lblEndereco.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Text = "Endereço:";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEndereco.MaxLength = 120;
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.TabIndex = 2;
            // 
            // lblLatitude
            // 
            this.lblLatitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Text = "Latitude:";
            // 
            // numLatitude
            // 
            this.numLatitude.DecimalPlaces = 4;
            this.numLatitude.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            this.numLatitude.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            this.numLatitude.Minimum = new decimal(new int[] { 90, 0, 0, -2147483648 });
            this.numLatitude.Name = "numLatitude";
            this.numLatitude.Size = new System.Drawing.Size(120, 25);
            this.numLatitude.TabIndex = 3;
            this.numLatitude.Value = new decimal(new int[] { 2345, 0, 0, -2147352576 });
            // 
            // lblLongitude
            // 
            this.lblLongitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Text = "Longitude:";
            // 
            // numLongitude
            // 
            this.numLongitude.DecimalPlaces = 4;
            this.numLongitude.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            this.numLongitude.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            this.numLongitude.Minimum = new decimal(new int[] { 180, 0, 0, -2147483648 });
            this.numLongitude.Name = "numLongitude";
            this.numLongitude.Size = new System.Drawing.Size(120, 25);
            this.numLongitude.TabIndex = 4;
            this.numLongitude.Value = new decimal(new int[] { 4669, 0, 0, -2147352576 });
            // 
            // lblCapacidade
            // 
            this.lblCapacidade.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCapacidade.AutoSize = true;
            this.lblCapacidade.Name = "lblCapacidade";
            this.lblCapacidade.Text = "Capacidade (pessoas):";
            // 
            // numCapacidade
            // 
            this.numCapacidade.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.numCapacidade.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCapacidade.Name = "numCapacidade";
            this.numCapacidade.Size = new System.Drawing.Size(120, 25);
            this.numCapacidade.TabIndex = 5;
            this.numCapacidade.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // chkAcessivel
            // 
            this.chkAcessivel.AutoSize = true;
            this.chkAcessivel.Name = "chkAcessivel";
            this.chkAcessivel.TabIndex = 6;
            this.chkAcessivel.Text = "Acessível (rampa, banheiro adaptado)";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.TabIndex = 7;
            this.chkAtivo.Text = "Ativo (pode receber moradores)";
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
            this.painelBotoes.TabIndex = 1;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Tag = "primario";
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // FormPontoApoio
            // 
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(560, 420);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.painelBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPontoApoio";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ponto de apoio";
            this.tabela.ResumeLayout(false);
            this.tabela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLatitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidade)).EndInit();
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabela;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.NumericUpDown numLatitude;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.NumericUpDown numLongitude;
        private System.Windows.Forms.Label lblCapacidade;
        private System.Windows.Forms.NumericUpDown numCapacidade;
        private System.Windows.Forms.CheckBox chkAcessivel;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
