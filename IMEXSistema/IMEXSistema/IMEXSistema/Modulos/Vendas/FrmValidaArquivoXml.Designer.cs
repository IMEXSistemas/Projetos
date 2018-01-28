namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmValidaArquivoXml
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
            this.cbbSchema = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnValidar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.btnValidar);
            this.panel1.Controls.Add(this.cbbSchema);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 156);
            this.panel1.TabIndex = 0;
            // 
            // cbbSchema
            // 
            this.cbbSchema.FormattingEnabled = true;
            this.cbbSchema.Items.AddRange(new object[] {
            "nfe_v2.00.xsd",
            "enviNFe_v2.00.xsd",
            "procNFe_v2.00.xsd",
            "cancNFe_v2.00.xsd",
            "inutNFe_v2.00.xsd"});
            this.cbbSchema.Location = new System.Drawing.Point(15, 61);
            this.cbbSchema.Name = "cbbSchema";
            this.cbbSchema.Size = new System.Drawing.Size(248, 21);
            this.cbbSchema.TabIndex = 17;
            this.cbbSchema.Text = "nfe_v2.00.xsd";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 45);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 13);
            this.label29.TabIndex = 16;
            this.label29.Text = "Schema:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(184, 26);
            this.label20.TabIndex = 15;
            this.label20.Text = "Clique em \"Validar Arquivo\" e localize\r\n o arquivo a ser validado.";
            // 
            // btnValidar
            // 
            this.btnValidar.Location = new System.Drawing.Point(90, 94);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(105, 29);
            this.btnValidar.TabIndex = 18;
            this.btnValidar.Text = "Validar Arquivo";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 133);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 14);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Status";
            // 
            // FrmValidaArquivoXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 156);
            this.Controls.Add(this.panel1);
            this.Name = "FrmValidaArquivoXml";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validar Arquivo NFe";
            this.Load += new System.EventHandler(this.FrmValidaArquivoXml_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.ComboBox cbbSchema;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblStatus;
    }
}