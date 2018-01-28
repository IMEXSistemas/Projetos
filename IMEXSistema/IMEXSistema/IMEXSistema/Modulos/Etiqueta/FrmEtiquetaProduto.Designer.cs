namespace BmsSoftware.Modulos.Etiqueta
{
    partial class FrmEtiquetaProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEtiquetaProduto));
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pDEtiqueta6095 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.chkCodigoBarra = new System.Windows.Forms.CheckBox();
            this.pDEtiqueta6080 = new System.Drawing.Printing.PrintDocument();
            this.pDEtiqueta6187 = new System.Drawing.Printing.PrintDocument();
            this.chkNExibirPreco = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RbCodReferencia = new System.Windows.Forms.RadioButton();
            this.rbCodiNenhuma = new System.Windows.Forms.RadioButton();
            this.rbCodBarra = new System.Windows.Forms.RadioButton();
            this.cbModeloEtiqueta = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pDEtiqueta_POLIFIX_2060 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(326, 119);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(52, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Repetir ";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 165);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(93, 165);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pDEtiqueta6095
            // 
            this.pDEtiqueta6095.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6080_BeginPrint);
            this.pDEtiqueta6095.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6080_EndPrint);
            this.pDEtiqueta6095.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDEtiqueta6080_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // chkCodigoBarra
            // 
            this.chkCodigoBarra.AutoSize = true;
            this.chkCodigoBarra.Checked = true;
            this.chkCodigoBarra.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCodigoBarra.Location = new System.Drawing.Point(12, 119);
            this.chkCodigoBarra.Name = "chkCodigoBarra";
            this.chkCodigoBarra.Size = new System.Drawing.Size(130, 17);
            this.chkCodigoBarra.TabIndex = 5;
            this.chkCodigoBarra.Text = "Exibir Código de Barra";
            this.chkCodigoBarra.UseVisualStyleBackColor = true;
            // 
            // pDEtiqueta6080
            // 
            this.pDEtiqueta6080.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6080_BeginPrint_1);
            this.pDEtiqueta6080.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6080_EndPrint_1);
            this.pDEtiqueta6080.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDEtiqueta6080_PrintPage_1);
            // 
            // pDEtiqueta6187
            // 
            this.pDEtiqueta6187.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6187_BeginPrint);
            this.pDEtiqueta6187.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta6187_EndPrint);
            this.pDEtiqueta6187.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDEtiqueta6187_PrintPage);
            // 
            // chkNExibirPreco
            // 
            this.chkNExibirPreco.AutoSize = true;
            this.chkNExibirPreco.Location = new System.Drawing.Point(148, 119);
            this.chkNExibirPreco.Name = "chkNExibirPreco";
            this.chkNExibirPreco.Size = new System.Drawing.Size(105, 17);
            this.chkNExibirPreco.TabIndex = 6;
            this.chkNExibirPreco.Text = "Não Exibir Preço";
            this.chkNExibirPreco.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RbCodReferencia);
            this.groupBox2.Controls.Add(this.rbCodiNenhuma);
            this.groupBox2.Controls.Add(this.rbCodBarra);
            this.groupBox2.Location = new System.Drawing.Point(12, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 57);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exibir Código";
            // 
            // RbCodReferencia
            // 
            this.RbCodReferencia.AutoSize = true;
            this.RbCodReferencia.Location = new System.Drawing.Point(6, 19);
            this.RbCodReferencia.Name = "RbCodReferencia";
            this.RbCodReferencia.Size = new System.Drawing.Size(77, 17);
            this.RbCodReferencia.TabIndex = 2;
            this.RbCodReferencia.Text = "Referência";
            this.RbCodReferencia.UseVisualStyleBackColor = true;
            // 
            // rbCodiNenhuma
            // 
            this.rbCodiNenhuma.AutoSize = true;
            this.rbCodiNenhuma.Location = new System.Drawing.Point(171, 19);
            this.rbCodiNenhuma.Name = "rbCodiNenhuma";
            this.rbCodiNenhuma.Size = new System.Drawing.Size(65, 17);
            this.rbCodiNenhuma.TabIndex = 1;
            this.rbCodiNenhuma.Text = "Nenhum";
            this.rbCodiNenhuma.UseVisualStyleBackColor = true;
            // 
            // rbCodBarra
            // 
            this.rbCodBarra.AutoSize = true;
            this.rbCodBarra.Checked = true;
            this.rbCodBarra.Location = new System.Drawing.Point(102, 19);
            this.rbCodBarra.Name = "rbCodBarra";
            this.rbCodBarra.Size = new System.Drawing.Size(50, 17);
            this.rbCodBarra.TabIndex = 0;
            this.rbCodBarra.TabStop = true;
            this.rbCodBarra.Text = "Barra";
            this.rbCodBarra.UseVisualStyleBackColor = true;
            // 
            // cbModeloEtiqueta
            // 
            this.cbModeloEtiqueta.FormattingEnabled = true;
            this.cbModeloEtiqueta.Items.AddRange(new object[] {
            "PIMACO 6095",
            "PIMACO 6080",
            "PIMACO 6187",
            "POLIFIX 2060"});
            this.cbModeloEtiqueta.Location = new System.Drawing.Point(12, 29);
            this.cbModeloEtiqueta.Name = "cbModeloEtiqueta";
            this.cbModeloEtiqueta.Size = new System.Drawing.Size(396, 21);
            this.cbModeloEtiqueta.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Modelo de Etiqueta:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(338, 13);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Configuração";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pDEtiqueta_POLIFIX_2060
            // 
            this.pDEtiqueta_POLIFIX_2060.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta_POLIFIX_2060_BeginPrint);
            this.pDEtiqueta_POLIFIX_2060.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.pDEtiqueta_POLIFIX_2060_EndPrint);
            this.pDEtiqueta_POLIFIX_2060.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDEtiqueta_POLIFIX_2060_PrintPage);
            // 
            // FrmEtiquetaProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 195);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbModeloEtiqueta);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkNExibirPreco);
            this.Controls.Add(this.chkCodigoBarra);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "FrmEtiquetaProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiqueta por Produto";
            this.Load += new System.EventHandler(this.FrmEtiquetaCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Drawing.Printing.PrintDocument pDEtiqueta6095;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.CheckBox chkCodigoBarra;
        private System.Drawing.Printing.PrintDocument pDEtiqueta6080;
        private System.Drawing.Printing.PrintDocument pDEtiqueta6187;
        private System.Windows.Forms.CheckBox chkNExibirPreco;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RbCodReferencia;
        private System.Windows.Forms.RadioButton rbCodiNenhuma;
        private System.Windows.Forms.RadioButton rbCodBarra;
        private System.Windows.Forms.ComboBox cbModeloEtiqueta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Drawing.Printing.PrintDocument pDEtiqueta_POLIFIX_2060;
    }
}