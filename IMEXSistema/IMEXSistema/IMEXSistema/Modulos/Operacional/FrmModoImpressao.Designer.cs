namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmModoImpressao
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.brnCancelar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMatricial = new System.Windows.Forms.RadioButton();
            this.rbGrafico = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbRede = new System.Windows.Forms.RadioButton();
            this.rblocal = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.brnCancelar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 170);
            this.panel1.TabIndex = 0;
            // 
            // brnCancelar
            // 
            this.brnCancelar.Location = new System.Drawing.Point(176, 134);
            this.brnCancelar.Name = "brnCancelar";
            this.brnCancelar.Size = new System.Drawing.Size(75, 23);
            this.brnCancelar.TabIndex = 2;
            this.brnCancelar.Text = "&Sair";
            this.brnCancelar.UseVisualStyleBackColor = true;
            this.brnCancelar.Click += new System.EventHandler(this.brnCancelar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(95, 134);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMatricial);
            this.groupBox1.Controls.Add(this.rbGrafico);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modo de Impressão";
            // 
            // rbMatricial
            // 
            this.rbMatricial.AutoSize = true;
            this.rbMatricial.Location = new System.Drawing.Point(203, 20);
            this.rbMatricial.Name = "rbMatricial";
            this.rbMatricial.Size = new System.Drawing.Size(64, 17);
            this.rbMatricial.TabIndex = 1;
            this.rbMatricial.TabStop = true;
            this.rbMatricial.Text = "&Matricial";
            this.rbMatricial.UseVisualStyleBackColor = true;
            // 
            // rbGrafico
            // 
            this.rbGrafico.AutoSize = true;
            this.rbGrafico.Location = new System.Drawing.Point(55, 20);
            this.rbGrafico.Name = "rbGrafico";
            this.rbGrafico.Size = new System.Drawing.Size(118, 17);
            this.rbGrafico.TabIndex = 0;
            this.rbGrafico.TabStop = true;
            this.rbGrafico.Text = "&Jato de Tinta/Laser";
            this.rbGrafico.UseVisualStyleBackColor = true;
            this.rbGrafico.CheckedChanged += new System.EventHandler(this.rbGrafico_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbRede);
            this.groupBox2.Controls.Add(this.rblocal);
            this.groupBox2.Location = new System.Drawing.Point(12, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 56);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Local de Impressão";
            // 
            // rbRede
            // 
            this.rbRede.AutoSize = true;
            this.rbRede.Location = new System.Drawing.Point(203, 20);
            this.rbRede.Name = "rbRede";
            this.rbRede.Size = new System.Drawing.Size(51, 17);
            this.rbRede.TabIndex = 1;
            this.rbRede.TabStop = true;
            this.rbRede.Text = "&Rede";
            this.rbRede.UseVisualStyleBackColor = true;
            // 
            // rblocal
            // 
            this.rblocal.AutoSize = true;
            this.rblocal.Location = new System.Drawing.Point(55, 20);
            this.rblocal.Name = "rblocal";
            this.rblocal.Size = new System.Drawing.Size(51, 17);
            this.rblocal.TabIndex = 0;
            this.rblocal.TabStop = true;
            this.rblocal.Text = "&Local";
            this.rblocal.UseVisualStyleBackColor = true;
            // 
            // FrmModoImpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 170);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModoImpressao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Impressão";
            this.Load += new System.EventHandler(this.FrmModoImpressao_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMatricial;
        private System.Windows.Forms.RadioButton rbGrafico;
        private System.Windows.Forms.Button brnCancelar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbRede;
        private System.Windows.Forms.RadioButton rblocal;
    }
}