namespace BmsSoftware.Modulos.Vendas
{
    partial class FormMedCubico
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
            this.txtComprimento = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnTransfere = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txtTotalM3 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtLargura = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtComprimento);
            this.panel1.Controls.Add(this.label38);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnTransfere);
            this.panel1.Controls.Add(this.label44);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.txtTotalM3);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.txtLargura);
            this.panel1.Controls.Add(this.label41);
            this.panel1.Controls.Add(this.txtAltura);
            this.panel1.Controls.Add(this.label40);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 94);
            this.panel1.TabIndex = 0;
            // 
            // txtComprimento
            // 
            this.txtComprimento.Location = new System.Drawing.Point(227, 23);
            this.txtComprimento.MaxLength = 20;
            this.txtComprimento.Name = "txtComprimento";
            this.txtComprimento.Size = new System.Drawing.Size(80, 20);
            this.txtComprimento.TabIndex = 2;
            this.txtComprimento.Text = "0,0000";
            this.txtComprimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtComprimento.Validating += new System.ComponentModel.CancelEventHandler(this.txtComprimento_Validating);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(224, 9);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(71, 13);
            this.label38.TabIndex = 275;
            this.label38.Text = "Comprimento:";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(234, 59);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnTransfere
            // 
            this.btnTransfere.Location = new System.Drawing.Point(119, 59);
            this.btnTransfere.Name = "btnTransfere";
            this.btnTransfere.Size = new System.Drawing.Size(105, 23);
            this.btnTransfere.TabIndex = 4;
            this.btnTransfere.Text = "&Transfere Valor";
            this.btnTransfere.UseVisualStyleBackColor = true;
            this.btnTransfere.Click += new System.EventHandler(this.btnTransfere_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(313, 26);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(13, 13);
            this.label44.TabIndex = 273;
            this.label44.Text = "=";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(101, 26);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(14, 13);
            this.label43.TabIndex = 272;
            this.label43.Text = "X";
            // 
            // txtTotalM3
            // 
            this.txtTotalM3.Location = new System.Drawing.Point(332, 23);
            this.txtTotalM3.MaxLength = 20;
            this.txtTotalM3.Name = "txtTotalM3";
            this.txtTotalM3.Size = new System.Drawing.Size(80, 20);
            this.txtTotalM3.TabIndex = 3;
            this.txtTotalM3.Text = "0,0000";
            this.txtTotalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(329, 9);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(29, 13);
            this.label42.TabIndex = 271;
            this.label42.Text = "MT3";
            // 
            // txtLargura
            // 
            this.txtLargura.Location = new System.Drawing.Point(121, 23);
            this.txtLargura.MaxLength = 20;
            this.txtLargura.Name = "txtLargura";
            this.txtLargura.Size = new System.Drawing.Size(80, 20);
            this.txtLargura.TabIndex = 1;
            this.txtLargura.Text = "0,0000";
            this.txtLargura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLargura.Validating += new System.ComponentModel.CancelEventHandler(this.txtLarguraProdM2_Validating);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(118, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(46, 13);
            this.label41.TabIndex = 270;
            this.label41.Text = "Largura:";
            // 
            // txtAltura
            // 
            this.txtAltura.Location = new System.Drawing.Point(15, 23);
            this.txtAltura.MaxLength = 20;
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(80, 20);
            this.txtAltura.TabIndex = 0;
            this.txtAltura.Text = "0,0000";
            this.txtAltura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAltura.Validating += new System.ComponentModel.CancelEventHandler(this.txtAlturaProdM2_Validating);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(12, 9);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(37, 13);
            this.label40.TabIndex = 269;
            this.label40.Text = "Altura:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 276;
            this.label1.Text = "X";
            // 
            // FormMedCubico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 94);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FormMedCubico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medida por M3";
            this.Load += new System.EventHandler(this.FormMedQuadrada_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMedQuadrada_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnTransfere;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtTotalM3;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtLargura;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtComprimento;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label1;
    }
}