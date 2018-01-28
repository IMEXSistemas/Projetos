namespace BmsSoftware.Modulos.Help
{
    partial class FrmHelpCSTPISCONFINS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelpCSTPISCONFINS));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 392);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "CST PIS=01 Operaçao Tributável (Base de cálculo = valor da operação aliquota norm" +
                "al (cumulativo/não cumulativo);",
            "CST PIS=02 Operaçao Tributável (Base de cálculo = valor da operação (aliquota dif" +
                "erenciada));",
            "CST PIS=03 Operaçao Tributável (Base de cálculo = base de cálculo = quantidade ve" +
                "ndida x alíquota por unidade de produto);",
            "CST PIS=04 Operaçao Tributável (tributação monofásica (aliquota zero);",
            "CST PIS=06 Operaçao Tributável (alíquota zero);",
            "CST PIS=07 Operação Isenta da Contribuição",
            "CST PIS=08 Operação Sem Incidência da Contribuição",
            "CST PIS=09 Operação com Suspensão da Contribuição",
            "CST PIS=99 Outras Operações.",
            "CST PIS=Substituição Tributária",
            "e",
            "CST COFINS=01 Operaçao Tributável (Base de cálculo = valor da operação aliquota n" +
                "ormal (cumulativo/não cumulativo);",
            "CST COFINS=02 Operaçao Tributável (Base de cálculo = valor da operação (aliquota " +
                "diferenciada));",
            "CST COFINS=03 Operaçao Tributável (Base de cálculo = base de cálculo = quantidade" +
                " vendida x alíquota por unidade de produto);",
            "CST COFINS=04 Operaçao Tributável (tributação monofásica (aliquota zero);",
            "CST COFINS=06 Operaçao Tributável (alíquota zero);",
            "CST COFINS=07 Operação Isenta da Contribuição",
            "CST COFINS=08 Operação Sem Incidência da Contribuição",
            "CST COFINS=09 Operação com Suspensão da Contribuição",
            "CST COFINS=99 Outras Operações.",
            "CST COFINS=Substituição Tributária"});
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(654, 392);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FrmHelpCSTPISCONFINS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 392);
            this.Controls.Add(this.panel1);
            this.Name = "FrmHelpCSTPISCONFINS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help CST PIS/CONFINS/IPI";
            this.Load += new System.EventHandler(this.FrmHelpCSTPISCONFINS_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHelpCSTPISCONFINS_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
    }
}