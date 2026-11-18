#nullable disable
namespace AlertaEncosta.Painel.Dialogos
{
    partial class FormEntradaTexto
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
            this.lblPergunta = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.painelBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPergunta
            // 
            this.lblPergunta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPergunta.Name = "lblPergunta";
            this.lblPergunta.Padding = new System.Windows.Forms.Padding(12, 12, 12, 4);
            this.lblPergunta.Size = new System.Drawing.Size(480, 56);
            this.lblPergunta.Text = "Pergunta";
            // 
            // txtValor
            // 
            this.txtValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValor.MaxLength = 500;
            this.txtValor.Name = "txtValor";
            this.txtValor.TabIndex = 0;
            // 
            // painelBotoes
            // 
            this.painelBotoes.AutoSize = true;
            this.painelBotoes.Controls.Add(this.btnOk);
            this.painelBotoes.Controls.Add(this.btnCancelar);
            this.painelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.painelBotoes.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.painelBotoes.Name = "painelBotoes";
            this.painelBotoes.Padding = new System.Windows.Forms.Padding(8);
            this.painelBotoes.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.TabIndex = 0;
            this.btnOk.Tag = "primario";
            this.btnOk.Text = "Confirmar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            // 
            // FormEntradaTexto
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(480, 150);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.painelBotoes);
            this.Controls.Add(this.lblPergunta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEntradaTexto";
            this.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AlertaEncosta";
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPergunta;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.FlowLayoutPanel painelBotoes;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancelar;
    }
}
