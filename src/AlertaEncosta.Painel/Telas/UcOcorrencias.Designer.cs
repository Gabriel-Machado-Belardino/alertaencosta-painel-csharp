#nullable disable
namespace AlertaEncosta.Painel.Telas
{
    partial class UcOcorrencias
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnNova = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnResolver = new System.Windows.Forms.Button();
            this.lblResumo = new System.Windows.Forms.Label();
            this.grade = new System.Windows.Forms.DataGridView();
            this.txtDetalhe = new System.Windows.Forms.TextBox();
            this.barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).BeginInit();
            this.SuspendLayout();
            // 
            // barra
            // 
            this.barra.AutoSize = true;
            this.barra.Controls.Add(this.lblStatus);
            this.barra.Controls.Add(this.cmbStatus);
            this.barra.Controls.Add(this.btnNova);
            this.barra.Controls.Add(this.btnIniciar);
            this.barra.Controls.Add(this.btnResolver);
            this.barra.Controls.Add(this.lblResumo);
            this.barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.barra.Name = "barra";
            this.barra.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.barra.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Mostrar:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pendentes (abertas e em atendimento)",
            "Todas",
            "Só abertas",
            "Só em atendimento",
            "Só resolvidas"});
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(3, 6, 16, 3);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(280, 25);
            this.cmbStatus.TabIndex = 0;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // btnNova
            // 
            this.btnNova.Name = "btnNova";
            this.btnNova.TabIndex = 1;
            this.btnNova.Tag = "primario";
            this.btnNova.Text = "+ Registrar ocorrência";
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.TabIndex = 2;
            this.btnIniciar.Text = "Enviar equipe (iniciar atendimento)";
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnResolver
            // 
            this.btnResolver.Name = "btnResolver";
            this.btnResolver.TabIndex = 3;
            this.btnResolver.Text = "✔ Resolver";
            this.btnResolver.Click += new System.EventHandler(this.btnResolver_Click);
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
            this.grade.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grade_CellFormatting);
            this.grade.SelectionChanged += new System.EventHandler(this.grade_SelectionChanged);
            // 
            // txtDetalhe
            // 
            this.txtDetalhe.BackColor = System.Drawing.Color.White;
            this.txtDetalhe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDetalhe.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtDetalhe.Multiline = true;
            this.txtDetalhe.Name = "txtDetalhe";
            this.txtDetalhe.ReadOnly = true;
            this.txtDetalhe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalhe.Size = new System.Drawing.Size(1300, 100);
            this.txtDetalhe.TabIndex = 2;
            // 
            // UcOcorrencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.Controls.Add(this.grade);
            this.Controls.Add(this.txtDetalhe);
            this.Controls.Add(this.barra);
            this.Name = "UcOcorrencias";
            this.Size = new System.Drawing.Size(1300, 560);
            this.barra.ResumeLayout(false);
            this.barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel barra;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnResolver;
        private System.Windows.Forms.Label lblResumo;
        private System.Windows.Forms.DataGridView grade;
        private System.Windows.Forms.TextBox txtDetalhe;
    }
}
