#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcMoradores
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
            this.lblFiltro = new System.Windows.Forms.Label();
            this.cmbFiltro = new System.Windows.Forms.ComboBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnSeguro = new System.Windows.Forms.Button();
            this.btnAjuda = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.grade = new System.Windows.Forms.DataGridView();
            this.lblRodape = new System.Windows.Forms.Label();
            this.barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).BeginInit();
            this.SuspendLayout();
            // 
            // barra
            // 
            this.barra.AutoSize = true;
            this.barra.Controls.Add(this.lblSetor);
            this.barra.Controls.Add(this.cmbSetor);
            this.barra.Controls.Add(this.lblFiltro);
            this.barra.Controls.Add(this.cmbFiltro);
            this.barra.Controls.Add(this.btnNovo);
            this.barra.Controls.Add(this.btnEditar);
            this.barra.Controls.Add(this.btnSeguro);
            this.barra.Controls.Add(this.btnAjuda);
            this.barra.Controls.Add(this.btnExcluir);
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
            this.cmbSetor.Margin = new System.Windows.Forms.Padding(3, 6, 12, 3);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(260, 25);
            this.cmbSetor.TabIndex = 0;
            this.cmbSetor.SelectedIndexChanged += new System.EventHandler(this.filtro_Changed);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Text = "Mostrar:";
            // 
            // cmbFiltro
            // 
            this.cmbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltro.Items.AddRange(new object[] {
            "Todos os moradores",
            "Fila de busca ativa",
            "Só quem pediu ajuda",
            "Só pessoas com prioridade"});
            this.cmbFiltro.Margin = new System.Windows.Forms.Padding(3, 6, 16, 3);
            this.cmbFiltro.Name = "cmbFiltro";
            this.cmbFiltro.Size = new System.Drawing.Size(210, 25);
            this.cmbFiltro.TabIndex = 1;
            this.cmbFiltro.SelectedIndexChanged += new System.EventHandler(this.filtro_Changed);
            // 
            // btnNovo
            // 
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.TabIndex = 2;
            this.btnNovo.Tag = "primario";
            this.btnNovo.Text = "+ Cadastrar morador";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnSeguro
            // 
            this.btnSeguro.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.btnSeguro.Name = "btnSeguro";
            this.btnSeguro.TabIndex = 4;
            this.btnSeguro.Text = "✔ Em local seguro";
            this.btnSeguro.Click += new System.EventHandler(this.btnSeguro_Click);
            // 
            // btnAjuda
            // 
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.TabIndex = 5;
            this.btnAjuda.Text = "Pediu ajuda";
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.TabIndex = 6;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
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
            // lblRodape
            // 
            this.lblRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRodape.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblRodape.Name = "lblRodape";
            this.lblRodape.Padding = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.lblRodape.Size = new System.Drawing.Size(1300, 32);
            this.lblRodape.TabIndex = 2;
            this.lblRodape.Text = "🔒 Dados simulados. Telefones aparecem mascarados (LGPD: privacidade por padrão).";
            // 
            // UcMoradores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.grade);
            this.Controls.Add(this.lblRodape);
            this.Controls.Add(this.barra);
            this.Name = "UcMoradores";
            this.Size = new System.Drawing.Size(1300, 560);
            this.barra.ResumeLayout(false);
            this.barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel barra;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.ComboBox cmbFiltro;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnSeguro;
        private System.Windows.Forms.Button btnAjuda;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.DataGridView grade;
        private System.Windows.Forms.Label lblRodape;
    }
}
