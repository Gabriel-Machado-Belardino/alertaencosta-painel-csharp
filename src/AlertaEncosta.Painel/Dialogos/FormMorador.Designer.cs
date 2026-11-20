#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormMorador
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblSetor = new System.Windows.Forms.Label();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.MaskedTextBox();
            this.lblCondicao = new System.Windows.Forms.Label();
            this.cmbCondicao = new System.Windows.Forms.ComboBox();
            this.lblLgpd = new System.Windows.Forms.Label();
            this.painelBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabela.SuspendLayout();
            this.painelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabela
            // 
            this.tabela.ColumnCount = 2;
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabela.Controls.Add(this.lblNome, 0, 0);
            this.tabela.Controls.Add(this.txtNome, 1, 0);
            this.tabela.Controls.Add(this.lblSetor, 0, 1);
            this.tabela.Controls.Add(this.cmbSetor, 1, 1);
            this.tabela.Controls.Add(this.lblEndereco, 0, 2);
            this.tabela.Controls.Add(this.txtEndereco, 1, 2);
            this.tabela.Controls.Add(this.lblTelefone, 0, 3);
            this.tabela.Controls.Add(this.txtTelefone, 1, 3);
            this.tabela.Controls.Add(this.lblCondicao, 0, 4);
            this.tabela.Controls.Add(this.cmbCondicao, 1, 4);
            this.tabela.Controls.Add(this.lblLgpd, 0, 5);
            this.tabela.SetColumnSpan(this.lblLgpd, 2);
            this.tabela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabela.Name = "tabela";
            this.tabela.Padding = new System.Windows.Forms.Padding(12);
            this.tabela.RowCount = 6;
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabela.TabIndex = 0;
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
            this.txtNome.TabIndex = 0;
            // 
            // lblSetor
            // 
            this.lblSetor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSetor.AutoSize = true;
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Text = "Setor de risco:";
            // 
            // cmbSetor
            // 
            this.cmbSetor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSetor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.TabIndex = 1;
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
            // lblTelefone
            // 
            this.lblTelefone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Text = "Celular:";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Mask = "(00) 00000-0000";
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(160, 25);
            this.txtTelefone.TabIndex = 3;
            // 
            // lblCondicao
            // 
            this.lblCondicao.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCondicao.AutoSize = true;
            this.lblCondicao.Name = "lblCondicao";
            this.lblCondicao.Text = "Condição especial:";
            // 
            // cmbCondicao
            // 
            this.cmbCondicao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCondicao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondicao.Name = "cmbCondicao";
            this.cmbCondicao.TabIndex = 4;
            // 
            // lblLgpd
            // 
            this.lblLgpd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLgpd.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblLgpd.Name = "lblLgpd";
            this.lblLgpd.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblLgpd.Text = "🔒 Coletamos só o necessário para avisar e socorrer (LGPD). A condição especial coloca a pessoa na frente da fila de busca ativa. No protótipo use apenas dados fictícios.";
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
            // FormMorador
            // 
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(560, 370);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.painelBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMorador";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cadastro de morador";
            this.tabela.ResumeLayout(false);
            this.tabela.PerformLayout();
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabela;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.MaskedTextBox txtTelefone;
        private System.Windows.Forms.Label lblCondicao;
        private System.Windows.Forms.ComboBox cmbCondicao;
        private System.Windows.Forms.Label lblLgpd;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
