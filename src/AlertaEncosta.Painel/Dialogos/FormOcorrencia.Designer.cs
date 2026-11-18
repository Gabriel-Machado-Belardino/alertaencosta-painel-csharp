#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormOcorrencia
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
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblCanal = new System.Windows.Forms.Label();
            this.cmbCanal = new System.Windows.Forms.ComboBox();
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.chkPessoasEmRisco = new System.Windows.Forms.CheckBox();
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
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tabela.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabela.Controls.Add(this.lblSetor, 0, 0);
            this.tabela.Controls.Add(this.cmbSetor, 1, 0);
            this.tabela.Controls.Add(this.lblTipo, 0, 1);
            this.tabela.Controls.Add(this.cmbTipo, 1, 1);
            this.tabela.Controls.Add(this.lblCanal, 0, 2);
            this.tabela.Controls.Add(this.cmbCanal, 1, 2);
            this.tabela.Controls.Add(this.lblLocal, 0, 3);
            this.tabela.Controls.Add(this.txtLocal, 1, 3);
            this.tabela.Controls.Add(this.lblDescricao, 0, 4);
            this.tabela.Controls.Add(this.txtDescricao, 1, 4);
            this.tabela.Controls.Add(this.chkPessoasEmRisco, 1, 5);
            this.tabela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabela.Name = "tabela";
            this.tabela.Padding = new System.Windows.Forms.Padding(12);
            this.tabela.RowCount = 6;
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tabela.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            // 
            // lblTipo
            // 
            this.lblTipo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTipo.AutoSize = true;
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Text = "Tipo:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.TabIndex = 1;
            // 
            // lblCanal
            // 
            this.lblCanal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCanal.AutoSize = true;
            this.lblCanal.Name = "lblCanal";
            this.lblCanal.Text = "Recebida por:";
            // 
            // cmbCanal
            // 
            this.cmbCanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCanal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanal.Name = "cmbCanal";
            this.cmbCanal.TabIndex = 2;
            // 
            // lblLocal
            // 
            this.lblLocal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLocal.AutoSize = true;
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Text = "Local:";
            // 
            // txtLocal
            // 
            this.txtLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLocal.MaxLength = 120;
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.PlaceholderText = "Rua, número ou ponto de referência";
            this.txtLocal.TabIndex = 3;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Text = "O que está acontecendo:";
            this.lblDescricao.MaximumSize = new System.Drawing.Size(124, 0);
            // 
            // txtDescricao
            // 
            this.txtDescricao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescricao.MaxLength = 500;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.TabIndex = 4;
            // 
            // chkPessoasEmRisco
            // 
            this.chkPessoasEmRisco.AutoSize = true;
            this.chkPessoasEmRisco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkPessoasEmRisco.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.chkPessoasEmRisco.Name = "chkPessoasEmRisco";
            this.chkPessoasEmRisco.TabIndex = 5;
            this.chkPessoasEmRisco.Text = "Há pessoas em risco no local (vai para o topo da lista)";
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
            this.btnSalvar.Text = "Registrar ocorrência";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // FormOcorrencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(600, 440);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.painelBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOcorrencia";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar ocorrência";
            this.tabela.ResumeLayout(false);
            this.tabela.PerformLayout();
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabela;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblCanal;
        private System.Windows.Forms.ComboBox cmbCanal;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.CheckBox chkPessoasEmRisco;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
