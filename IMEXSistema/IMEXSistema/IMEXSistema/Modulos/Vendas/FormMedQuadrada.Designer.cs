namespace BmsSoftware.Modulos.Vendas
{
    partial class FormMedQuadrada
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
            this.btnSair = new System.Windows.Forms.Button();
            this.btnTransfere = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txtTotalM2ProdM2 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtLarguraProdM2 = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtAlturaProdM2 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnTransfere);
            this.panel1.Controls.Add(this.label44);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.txtTotalM2ProdM2);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.txtLarguraProdM2);
            this.panel1.Controls.Add(this.label41);
            this.panel1.Controls.Add(this.txtAlturaProdM2);
            this.panel1.Controls.Add(this.label40);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 94);
            this.panel1.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(185, 54);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnTransfere
            // 
            this.btnTransfere.Location = new System.Drawing.Point(63, 54);
            this.btnTransfere.Name = "btnTransfere";
            this.btnTransfere.Size = new System.Drawing.Size(112, 23);
            this.btnTransfere.TabIndex = 3;
            this.btnTransfere.Text = "&Transfere Valor";
            this.btnTransfere.UseVisualStyleBackColor = true;
            this.btnTransfere.Click += new System.EventHandler(this.btnTransfere_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(204, 26);
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
            // txtTotalM2ProdM2
            // 
            this.txtTotalM2ProdM2.Location = new System.Drawing.Point(221, 23);
            this.txtTotalM2ProdM2.MaxLength = 20;
            this.txtTotalM2ProdM2.Name = "txtTotalM2ProdM2";
            this.txtTotalM2ProdM2.Size = new System.Drawing.Size(80, 20);
            this.txtTotalM2ProdM2.TabIndex = 2;
            this.txtTotalM2ProdM2.Text = "0,0000";
            this.txtTotalM2ProdM2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(218, 9);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(29, 13);
            this.label42.TabIndex = 271;
            this.label42.Text = "MT2";
            // 
            // txtLarguraProdM2
            // 
            this.txtLarguraProdM2.Location = new System.Drawing.Point(121, 23);
            this.txtLarguraProdM2.MaxLength = 20;
            this.txtLarguraProdM2.Name = "txtLarguraProdM2";
            this.txtLarguraProdM2.Size = new System.Drawing.Size(80, 20);
            this.txtLarguraProdM2.TabIndex = 1;
            this.txtLarguraProdM2.Text = "0,0000";
            this.txtLarguraProdM2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLarguraProdM2.Validating += new System.ComponentModel.CancelEventHandler(this.txtLarguraProdM2_Validating);
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
            // txtAlturaProdM2
            // 
            this.txtAlturaProdM2.Location = new System.Drawing.Point(15, 23);
            this.txtAlturaProdM2.MaxLength = 20;
            this.txtAlturaProdM2.Name = "txtAlturaProdM2";
            this.txtAlturaProdM2.Size = new System.Drawing.Size(80, 20);
            this.txtAlturaProdM2.TabIndex = 0;
            this.txtAlturaProdM2.Text = "0,0000";
            this.txtAlturaProdM2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlturaProdM2.Validating += new System.ComponentModel.CancelEventHandler(this.txtAlturaProdM2_Validating);
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
            // FormMedQuadrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 94);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FormMedQuadrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medida por M2";
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
        private System.Windows.Forms.TextBox txtTotalM2ProdM2;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtLarguraProdM2;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtAlturaProdM2;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}