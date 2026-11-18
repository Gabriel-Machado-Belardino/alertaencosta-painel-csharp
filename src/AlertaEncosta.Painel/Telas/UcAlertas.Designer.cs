#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcAlertas
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
            this.barra = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSetor = new System.Windows.Forms.Label();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.btnSimularRespostas = new System.Windows.Forms.Button();
            this.btnEncerrar = new System.Windows.Forms.Button();
            this.chkSomenteAtivos = new System.Windows.Forms.CheckBox();
            this.grade = new System.Windows.Forms.DataGridView();
            this.tabelaDetalhe = new System.Windows.Forms.TableLayoutPanel();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.painelConfirmacoes = new System.Windows.Forms.Panel();
            this.lblContagem = new System.Windows.Forms.Label();
            this.barraSeguros = new AlertaEncosta.Painel.Componentes.BarraMeta();
            this.lblPercentual = new System.Windows.Forms.Label();
            this.barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).BeginInit();
            this.tabelaDetalhe.SuspendLayout();
            this.painelConfirmacoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // barra
            // 
            this.barra.AutoSize = true;
            this.barra.Controls.Add(this.lblSetor);
            this.barra.Controls.Add(this.cmbSetor);
            this.barra.Controls.Add(this.btnEmitir);
            this.barra.Controls.Add(this.btnSimularRespostas);
            this.barra.Controls.Add(this.btnEncerrar);
            this.barra.Controls.Add(this.chkSomenteAtivos);
            this.barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.barra.Name = "barra";
            this.barra.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.barra.TabIndex = 0;
            // 
            // lblSetor
            // 
            this.lblSetor.AutoSize = true;
            this.lblSetor.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Text = "Setor:";
            // 
            // cmbSetor
            // 
            this.cmbSetor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetor.Margin = new System.Windows.Forms.Padding(3, 6, 8, 3);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(300, 25);
            this.cmbSetor.TabIndex = 0;
            this.cmbSetor.SelectedIndexChanged += new System.EventHandler(this.cmbSetor_SelectedIndexChanged);
            // 
            // btnEmitir
            // 
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.TabIndex = 1;
            this.btnEmitir.Tag = "primario";
            this.btnEmitir.Text = "🔔 Emitir / escalar alerta";
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // btnSimularRespostas
            // 
            this.btnSimularRespostas.Margin = new System.Windows.Forms.Padding(24, 3, 3, 3);
            this.btnSimularRespostas.Name = "btnSimularRespostas";
            this.btnSimularRespostas.TabIndex = 2;
            this.btnSimularRespostas.Text = "Simular respostas do app";
            this.btnSimularRespostas.Click += new System.EventHandler(this.btnSimularRespostas_Click);
            // 
            // btnEncerrar
            // 
            this.btnEncerrar.Name = "btnEncerrar";
            this.btnEncerrar.TabIndex = 3;
            this.btnEncerrar.Tag = "perigo";
            this.btnEncerrar.Text = "Encerrar alerta";
            this.btnEncerrar.Click += new System.EventHandler(this.btnEncerrar_Click);
            // 
            // chkSomenteAtivos
            // 
            this.chkSomenteAtivos.AutoSize = true;
            this.chkSomenteAtivos.Margin = new System.Windows.Forms.Padding(24, 9, 3, 3);
            this.chkSomenteAtivos.Name = "chkSomenteAtivos";
            this.chkSomenteAtivos.TabIndex = 4;
            this.chkSomenteAtivos.Text = "Mostrar só alertas ativos";
            this.chkSomenteAtivos.CheckedChanged += new System.EventHandler(this.chkSomenteAtivos_CheckedChanged);
            // 
            // grade
            // 
            this.grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grade.Name = "grade";
            this.grade.TabIndex = 1;
            this.grade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grade_CellFormatting);
            this.grade.SelectionChanged += new System.EventHandler(this.grade_SelectionChanged);
            // 
            // tabelaDetalhe
            // 
            this.tabelaDetalhe.ColumnCount = 2;
            this.tabelaDetalhe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tabelaDetalhe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tabelaDetalhe.Controls.Add(this.txtMensagem, 0, 0);
            this.tabelaDetalhe.Controls.Add(this.painelConfirmacoes, 1, 0);
            this.tabelaDetalhe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabelaDetalhe.Name = "tabelaDetalhe";
            this.tabelaDetalhe.RowCount = 1;
            this.tabelaDetalhe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabelaDetalhe.Size = new System.Drawing.Size(1300, 130);
            this.tabelaDetalhe.TabIndex = 2;
            // 
            // txtMensagem
            // 
            this.txtMensagem.BackColor = System.Drawing.Color.White;
            this.txtMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMensagem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.ReadOnly = true;
            this.txtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensagem.TabIndex = 0;
            this.txtMensagem.Text = "Selecione um alerta para ver a mensagem enviada aos moradores.";
            // 
            // painelConfirmacoes
            // 
            this.painelConfirmacoes.BackColor = System.Drawing.Color.White;
            this.painelConfirmacoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.painelConfirmacoes.Controls.Add(this.lblContagem);
            this.painelConfirmacoes.Controls.Add(this.barraSeguros);
            this.painelConfirmacoes.Controls.Add(this.lblPercentual);
            this.painelConfirmacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.painelConfirmacoes.Name = "painelConfirmacoes";
            this.painelConfirmacoes.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.painelConfirmacoes.TabIndex = 1;
            // 
            // lblContagem
            // 
            this.lblContagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContagem.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblContagem.Name = "lblContagem";
            this.lblContagem.TabIndex = 2;
            this.lblContagem.Text = "";
            // 
            // barraSeguros
            // 
            this.barraSeguros.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraSeguros.Name = "barraSeguros";
            this.barraSeguros.Size = new System.Drawing.Size(400, 24);
            this.barraSeguros.TabIndex = 1;
            // 
            // lblPercentual
            // 
            this.lblPercentual.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPercentual.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPercentual.Name = "lblPercentual";
            this.lblPercentual.Size = new System.Drawing.Size(400, 32);
            this.lblPercentual.TabIndex = 0;
            this.lblPercentual.Text = "Confirmações";
            // 
            // UcAlertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.grade);
            this.Controls.Add(this.tabelaDetalhe);
            this.Controls.Add(this.barra);
            this.Name = "UcAlertas";
            this.Size = new System.Drawing.Size(1300, 560);
            this.barra.ResumeLayout(false);
            this.barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).EndInit();
            this.tabelaDetalhe.ResumeLayout(false);
            this.tabelaDetalhe.PerformLayout();
            this.painelConfirmacoes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel barra;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Button btnEmitir;
        private System.Windows.Forms.Button btnSimularRespostas;
        private System.Windows.Forms.Button btnEncerrar;
        private System.Windows.Forms.CheckBox chkSomenteAtivos;
        private System.Windows.Forms.DataGridView grade;
        private System.Windows.Forms.TableLayoutPanel tabelaDetalhe;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Panel painelConfirmacoes;
        private System.Windows.Forms.Label lblContagem;
        private AlertaEncosta.Painel.Componentes.BarraMeta barraSeguros;
        private System.Windows.Forms.Label lblPercentual;
    }
}
