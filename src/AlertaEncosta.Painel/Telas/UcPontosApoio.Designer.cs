#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcPontosApoio
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
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblPessoas = new System.Windows.Forms.Label();
            this.numPessoas = new System.Windows.Forms.NumericUpDown();
            this.btnEntrada = new System.Windows.Forms.Button();
            this.btnSaida = new System.Windows.Forms.Button();
            this.lblResumo = new System.Windows.Forms.Label();
            this.grade = new System.Windows.Forms.DataGridView();
            this.grupoRanking = new System.Windows.Forms.GroupBox();
            this.lstRanking = new System.Windows.Forms.ListBox();
            this.barraRanking = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSetorRanking = new System.Windows.Forms.Label();
            this.cmbSetorRanking = new System.Windows.Forms.ComboBox();
            this.lblExplicacao = new System.Windows.Forms.Label();
            this.barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPessoas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).BeginInit();
            this.grupoRanking.SuspendLayout();
            this.barraRanking.SuspendLayout();
            this.SuspendLayout();
            // 
            // barra
            // 
            this.barra.AutoSize = true;
            this.barra.Controls.Add(this.btnNovo);
            this.barra.Controls.Add(this.btnEditar);
            this.barra.Controls.Add(this.lblPessoas);
            this.barra.Controls.Add(this.numPessoas);
            this.barra.Controls.Add(this.btnEntrada);
            this.barra.Controls.Add(this.btnSaida);
            this.barra.Controls.Add(this.lblResumo);
            this.barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.barra.Name = "barra";
            this.barra.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.barra.TabIndex = 0;
            // 
            // btnNovo
            // 
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.TabIndex = 0;
            this.btnNovo.Tag = "primario";
            this.btnNovo.Text = "+ Novo ponto de apoio";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblPessoas
            // 
            this.lblPessoas.AutoSize = true;
            this.lblPessoas.Margin = new System.Windows.Forms.Padding(24, 10, 3, 0);
            this.lblPessoas.Name = "lblPessoas";
            this.lblPessoas.Text = "Pessoas:";
            // 
            // numPessoas
            // 
            this.numPessoas.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.numPessoas.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numPessoas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numPessoas.Name = "numPessoas";
            this.numPessoas.Size = new System.Drawing.Size(70, 25);
            this.numPessoas.TabIndex = 2;
            this.numPessoas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnEntrada
            // 
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.TabIndex = 3;
            this.btnEntrada.Text = "▲ Registrar chegada";
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // btnSaida
            // 
            this.btnSaida.Name = "btnSaida";
            this.btnSaida.TabIndex = 4;
            this.btnSaida.Text = "▼ Registrar saída";
            this.btnSaida.Click += new System.EventHandler(this.btnSaida_Click);
            // 
            // lblResumo
            // 
            this.lblResumo.AutoSize = true;
            this.lblResumo.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblResumo.Margin = new System.Windows.Forms.Padding(16, 10, 3, 0);
            this.lblResumo.Name = "lblResumo";
            this.lblResumo.Text = "";
            // 
            // grade
            // 
            this.grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grade.Name = "grade";
            this.grade.TabIndex = 1;
            this.grade.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grade_CellDoubleClick);
            this.grade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grade_CellFormatting);
            this.grade.SelectionChanged += new System.EventHandler(this.grade_SelectionChanged);
            // 
            // grupoRanking
            // 
            this.grupoRanking.Controls.Add(this.lstRanking);
            this.grupoRanking.Controls.Add(this.lblExplicacao);
            this.grupoRanking.Controls.Add(this.barraRanking);
            this.grupoRanking.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grupoRanking.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.grupoRanking.ForeColor = System.Drawing.Color.FromArgb(22, 50, 84);
            this.grupoRanking.Name = "grupoRanking";
            this.grupoRanking.Padding = new System.Windows.Forms.Padding(8);
            this.grupoRanking.Size = new System.Drawing.Size(1300, 200);
            this.grupoRanking.TabIndex = 2;
            this.grupoRanking.TabStop = false;
            this.grupoRanking.Text = "Para onde mandar os moradores? (ranking usado no alerta)";
            // 
            // lstRanking
            // 
            this.lstRanking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRanking.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lstRanking.ForeColor = System.Drawing.Color.FromArgb(28, 33, 40);
            this.lstRanking.IntegralHeight = false;
            this.lstRanking.ItemHeight = 19;
            this.lstRanking.Name = "lstRanking";
            this.lstRanking.TabIndex = 1;
            // 
            // barraRanking
            // 
            this.barraRanking.AutoSize = true;
            this.barraRanking.Controls.Add(this.lblSetorRanking);
            this.barraRanking.Controls.Add(this.cmbSetorRanking);
            this.barraRanking.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraRanking.Name = "barraRanking";
            this.barraRanking.TabIndex = 0;
            // 
            // lblSetorRanking
            // 
            this.lblSetorRanking.AutoSize = true;
            this.lblSetorRanking.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSetorRanking.ForeColor = System.Drawing.Color.FromArgb(28, 33, 40);
            this.lblSetorRanking.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblSetorRanking.Name = "lblSetorRanking";
            this.lblSetorRanking.Text = "Setor de origem:";
            // 
            // cmbSetorRanking
            // 
            this.cmbSetorRanking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetorRanking.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSetorRanking.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.cmbSetorRanking.Name = "cmbSetorRanking";
            this.cmbSetorRanking.Size = new System.Drawing.Size(300, 25);
            this.cmbSetorRanking.TabIndex = 0;
            this.cmbSetorRanking.SelectedIndexChanged += new System.EventHandler(this.cmbSetorRanking_SelectedIndexChanged);
            // 
            // lblExplicacao
            // 
            this.lblExplicacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblExplicacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExplicacao.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblExplicacao.Name = "lblExplicacao";
            this.lblExplicacao.Size = new System.Drawing.Size(1284, 22);
            this.lblExplicacao.TabIndex = 2;
            this.lblExplicacao.Text = "Regra: descarta lotados/desativados → se há pessoas com prioridade, só acessíveis → primeiro os que comportam todos → menor distância (Haversine).";
            // 
            // UcPontosApoio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.grade);
            this.Controls.Add(this.grupoRanking);
            this.Controls.Add(this.barra);
            this.Name = "UcPontosApoio";
            this.Size = new System.Drawing.Size(1300, 560);
            this.barra.ResumeLayout(false);
            this.barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPessoas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).EndInit();
            this.grupoRanking.ResumeLayout(false);
            this.grupoRanking.PerformLayout();
            this.barraRanking.ResumeLayout(false);
            this.barraRanking.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel barra;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lblPessoas;
        private System.Windows.Forms.NumericUpDown numPessoas;
        private System.Windows.Forms.Button btnEntrada;
        private System.Windows.Forms.Button btnSaida;
        private System.Windows.Forms.Label lblResumo;
        private System.Windows.Forms.DataGridView grade;
        private System.Windows.Forms.GroupBox grupoRanking;
        private System.Windows.Forms.ListBox lstRanking;
        private System.Windows.Forms.FlowLayoutPanel barraRanking;
        private System.Windows.Forms.Label lblSetorRanking;
        private System.Windows.Forms.ComboBox cmbSetorRanking;
        private System.Windows.Forms.Label lblExplicacao;
    }
}
