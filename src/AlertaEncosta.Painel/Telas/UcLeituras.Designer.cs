#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcLeituras
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
            this.btnNova = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnSimular = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grade = new System.Windows.Forms.DataGridView();
            this.lblDetalhe = new System.Windows.Forms.Label();
            this.barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).BeginInit();
            this.SuspendLayout();
            // 
            // barra
            // 
            this.barra.AutoSize = true;
            this.barra.Controls.Add(this.lblSetor);
            this.barra.Controls.Add(this.cmbSetor);
            this.barra.Controls.Add(this.btnNova);
            this.barra.Controls.Add(this.btnImportar);
            this.barra.Controls.Add(this.btnSimular);
            this.barra.Controls.Add(this.btnExcluir);
            this.barra.Controls.Add(this.lblTotal);
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
            this.cmbSetor.Margin = new System.Windows.Forms.Padding(3, 6, 16, 3);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(300, 25);
            this.cmbSetor.TabIndex = 0;
            this.cmbSetor.SelectedIndexChanged += new System.EventHandler(this.cmbSetor_SelectedIndexChanged);
            // 
            // btnNova
            // 
            this.btnNova.Name = "btnNova";
            this.btnNova.TabIndex = 1;
            this.btnNova.Tag = "primario";
            this.btnNova.Text = "+ Nova leitura manual";
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.TabIndex = 2;
            this.btnImportar.Text = "Importar da estação (JSON / Wokwi / API)";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnSimular
            // 
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.TabIndex = 3;
            this.btnSimular.Text = "Simular leitura (F5)";
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.TabIndex = 4;
            this.btnExcluir.Text = "Excluir leitura";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(88, 96, 108);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(16, 10, 3, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "0 leituras";
            // 
            // grade
            // 
            this.grade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grade.Name = "grade";
            this.grade.TabIndex = 1;
            this.grade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grade_CellFormatting);
            this.grade.SelectionChanged += new System.EventHandler(this.grade_SelectionChanged);
            // 
            // lblDetalhe
            // 
            this.lblDetalhe.BackColor = System.Drawing.Color.White;
            this.lblDetalhe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDetalhe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDetalhe.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblDetalhe.Name = "lblDetalhe";
            this.lblDetalhe.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.lblDetalhe.Size = new System.Drawing.Size(1300, 64);
            this.lblDetalhe.TabIndex = 2;
            this.lblDetalhe.Text = "Selecione uma leitura para ver como o risco foi calculado.";
            // 
            // UcLeituras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.grade);
            this.Controls.Add(this.lblDetalhe);
            this.Controls.Add(this.barra);
            this.Name = "UcLeituras";
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
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView grade;
        private System.Windows.Forms.Label lblDetalhe;
    }
}
