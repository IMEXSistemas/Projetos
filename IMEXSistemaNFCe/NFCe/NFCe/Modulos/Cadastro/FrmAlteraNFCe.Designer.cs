namespace BmsSoftware.Modulos.Cadastro
{
    partial class FrmAlteraNFCe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlteraNFCe));
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNotaFiscal = new System.Windows.Forms.Label();
            this.txtChave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProtocolo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLimpaPesquisa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(15, 39);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(226, 21);
            this.cbStatus.TabIndex = 295;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(12, 24);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(40, 13);
            this.label52.TabIndex = 296;
            this.label52.Text = "Status:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 298;
            this.label10.Text = "NFCe:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblNotaFiscal
            // 
            this.lblNotaFiscal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotaFiscal.AutoSize = true;
            this.lblNotaFiscal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNotaFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotaFiscal.Location = new System.Drawing.Point(60, 9);
            this.lblNotaFiscal.Name = "lblNotaFiscal";
            this.lblNotaFiscal.Size = new System.Drawing.Size(49, 13);
            this.lblNotaFiscal.TabIndex = 297;
            this.lblNotaFiscal.Text = "000000";
            this.lblNotaFiscal.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtChave
            // 
            this.txtChave.Location = new System.Drawing.Point(15, 79);
            this.txtChave.MaxLength = 44;
            this.txtChave.Name = "txtChave";
            this.txtChave.Size = new System.Drawing.Size(297, 20);
            this.txtChave.TabIndex = 299;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 300;
            this.label2.Text = "Chave NFCe:";
            // 
            // txtProtocolo
            // 
            this.txtProtocolo.Location = new System.Drawing.Point(321, 79);
            this.txtProtocolo.MaxLength = 20;
            this.txtProtocolo.Name = "txtProtocolo";
            this.txtProtocolo.Size = new System.Drawing.Size(226, 20);
            this.txtProtocolo.TabIndex = 301;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 302;
            this.label1.Text = "Procolo:";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(95, 105);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 306;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLimpaPesquisa
            // 
            this.btnLimpaPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpaPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpaPesquisa.Image")));
            this.btnLimpaPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpaPesquisa.Location = new System.Drawing.Point(15, 105);
            this.btnLimpaPesquisa.Name = "btnLimpaPesquisa";
            this.btnLimpaPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnLimpaPesquisa.TabIndex = 305;
            this.btnLimpaPesquisa.Text = "Salvar";
            this.btnLimpaPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpaPesquisa.UseVisualStyleBackColor = true;
            this.btnLimpaPesquisa.Click += new System.EventHandler(this.btnLimpaPesquisa_Click);
            // 
            // FrmAlteraNFCe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 134);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLimpaPesquisa);
            this.Controls.Add(this.txtProtocolo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblNotaFiscal);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label52);
            this.Name = "FrmAlteraNFCe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Altera NFCe";
            this.Load += new System.EventHandler(this.FrmAlteraNFCe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNotaFiscal;
        private System.Windows.Forms.TextBox txtChave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProtocolo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLimpaPesquisa;
    }
}