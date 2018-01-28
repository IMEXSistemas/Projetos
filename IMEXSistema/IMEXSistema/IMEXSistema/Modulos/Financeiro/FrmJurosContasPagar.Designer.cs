namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmJurosContasPagar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtOutrasTaxa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTaxa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJurosDiario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMultaAtraso = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkCalculoJuro = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkCalculoJuro);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtOutrasTaxa);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtTaxa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtJurosDiario);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMultaAtraso);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 184);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Salvar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOutrasTaxa
            // 
            this.txtOutrasTaxa.Location = new System.Drawing.Point(120, 99);
            this.txtOutrasTaxa.Name = "txtOutrasTaxa";
            this.txtOutrasTaxa.Size = new System.Drawing.Size(100, 20);
            this.txtOutrasTaxa.TabIndex = 7;
            this.txtOutrasTaxa.Text = "0,00";
            this.txtOutrasTaxa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOutrasTaxa.Enter += new System.EventHandler(this.txtOutrasTaxa_Enter);
            this.txtOutrasTaxa.Validating += new System.ComponentModel.CancelEventHandler(this.txtOutrasTaxa_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Outras Taxas R$:";
            // 
            // txtTaxa
            // 
            this.txtTaxa.Location = new System.Drawing.Point(14, 99);
            this.txtTaxa.Name = "txtTaxa";
            this.txtTaxa.Size = new System.Drawing.Size(100, 20);
            this.txtTaxa.TabIndex = 5;
            this.txtTaxa.Text = "0,00";
            this.txtTaxa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxa.Enter += new System.EventHandler(this.txtTaxa_Enter);
            this.txtTaxa.Validating += new System.ComponentModel.CancelEventHandler(this.txtTaxa_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Taxa R$:";
            // 
            // txtJurosDiario
            // 
            this.txtJurosDiario.Location = new System.Drawing.Point(120, 58);
            this.txtJurosDiario.Name = "txtJurosDiario";
            this.txtJurosDiario.Size = new System.Drawing.Size(79, 20);
            this.txtJurosDiario.TabIndex = 3;
            this.txtJurosDiario.Text = "0,000";
            this.txtJurosDiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtJurosDiario.Enter += new System.EventHandler(this.txtJurosDiario_Enter);
            this.txtJurosDiario.Validating += new System.ComponentModel.CancelEventHandler(this.txtJurosDiario_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Juros Diário (%):";
            // 
            // txtMultaAtraso
            // 
            this.txtMultaAtraso.Location = new System.Drawing.Point(14, 58);
            this.txtMultaAtraso.Name = "txtMultaAtraso";
            this.txtMultaAtraso.Size = new System.Drawing.Size(100, 20);
            this.txtMultaAtraso.TabIndex = 1;
            this.txtMultaAtraso.Text = "0,00";
            this.txtMultaAtraso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMultaAtraso.Enter += new System.EventHandler(this.txtMultaAtraso_Enter);
            this.txtMultaAtraso.Validating += new System.ComponentModel.CancelEventHandler(this.txtMultaAtraso_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Multa por atraso R$:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkCalculoJuro
            // 
            this.chkCalculoJuro.AutoSize = true;
            this.chkCalculoJuro.Location = new System.Drawing.Point(13, 13);
            this.chkCalculoJuro.Name = "chkCalculoJuro";
            this.chkCalculoJuro.Size = new System.Drawing.Size(92, 17);
            this.chkCalculoJuro.TabIndex = 10;
            this.chkCalculoJuro.Text = "Calcular Juros";
            this.chkCalculoJuro.UseVisualStyleBackColor = true;
            // 
            // FrmJurosContasPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 184);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmJurosContasPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Juros - Contas a Pagar";
            this.Load += new System.EventHandler(this.FrmJurosContasPagar_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmJurosContasPagar_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmJurosContasPagar_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJurosDiario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMultaAtraso;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtOutrasTaxa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTaxa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkCalculoJuro;
    }
}