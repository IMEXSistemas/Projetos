namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmReciboAvulso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReciboAvulso));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkModelo2 = new System.Windows.Forms.CheckBox();
            this.btnCadCliente = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbduasvias = new System.Windows.Forms.RadioButton();
            this.rbumavia = new System.Windows.Forms.RadioButton();
            this.msktDataEmissao = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReferente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValorRecibo = new System.Windows.Forms.TextBox();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument5 = new System.Drawing.Printing.PrintDocument();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkModelo2);
            this.panel1.Controls.Add(this.btnCadCliente);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.msktDataEmissao);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtObs);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtReferente);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtValorRecibo);
            this.panel1.Controls.Add(this.cbCliente);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 306);
            this.panel1.TabIndex = 0;
            // 
            // chkModelo2
            // 
            this.chkModelo2.AutoSize = true;
            this.chkModelo2.Checked = true;
            this.chkModelo2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkModelo2.Location = new System.Drawing.Point(13, 223);
            this.chkModelo2.Name = "chkModelo2";
            this.chkModelo2.Size = new System.Drawing.Size(136, 17);
            this.chkModelo2.TabIndex = 172;
            this.chkModelo2.Text = "Modelo de Impressão 2";
            this.chkModelo2.UseVisualStyleBackColor = true;
            // 
            // btnCadCliente
            // 
            this.btnCadCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadCliente.FlatAppearance.BorderSize = 0;
            this.btnCadCliente.Location = new System.Drawing.Point(341, 30);
            this.btnCadCliente.Name = "btnCadCliente";
            this.btnCadCliente.Size = new System.Drawing.Size(26, 21);
            this.btnCadCliente.TabIndex = 171;
            this.btnCadCliente.Text = "...";
            this.btnCadCliente.UseVisualStyleBackColor = true;
            this.btnCadCliente.Click += new System.EventHandler(this.btnCadCliente_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbduasvias);
            this.groupBox1.Controls.Add(this.rbumavia);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 55);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nº de Vias";
            // 
            // rbduasvias
            // 
            this.rbduasvias.AutoSize = true;
            this.rbduasvias.Location = new System.Drawing.Point(220, 19);
            this.rbduasvias.Name = "rbduasvias";
            this.rbduasvias.Size = new System.Drawing.Size(73, 17);
            this.rbduasvias.TabIndex = 6;
            this.rbduasvias.Text = "Duas Vias";
            this.rbduasvias.UseVisualStyleBackColor = true;
            // 
            // rbumavia
            // 
            this.rbumavia.AutoSize = true;
            this.rbumavia.Checked = true;
            this.rbumavia.Location = new System.Drawing.Point(129, 19);
            this.rbumavia.Name = "rbumavia";
            this.rbumavia.Size = new System.Drawing.Size(65, 17);
            this.rbumavia.TabIndex = 5;
            this.rbumavia.TabStop = true;
            this.rbumavia.Text = "Uma Via";
            this.rbumavia.UseVisualStyleBackColor = true;
            // 
            // msktDataEmissao
            // 
            this.msktDataEmissao.Location = new System.Drawing.Point(373, 29);
            this.msktDataEmissao.Mask = "00/00/0000";
            this.msktDataEmissao.Name = "msktDataEmissao";
            this.msktDataEmissao.Size = new System.Drawing.Size(79, 20);
            this.msktDataEmissao.TabIndex = 1;
            this.msktDataEmissao.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(370, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Data:";
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(10, 243);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 13;
            this.lblObsField.Text = "Obs.:";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(94, 271);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(13, 271);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 7;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Observação:";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(10, 110);
            this.txtObs.MaxLength = 5000;
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObs.Size = new System.Drawing.Size(436, 46);
            this.txtObs.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Referente:";
            // 
            // txtReferente
            // 
            this.txtReferente.Location = new System.Drawing.Point(135, 70);
            this.txtReferente.Name = "txtReferente";
            this.txtReferente.Size = new System.Drawing.Size(314, 20);
            this.txtReferente.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Valor:";
            // 
            // txtValorRecibo
            // 
            this.txtValorRecibo.Location = new System.Drawing.Point(10, 70);
            this.txtValorRecibo.MaxLength = 10;
            this.txtValorRecibo.Name = "txtValorRecibo";
            this.txtValorRecibo.Size = new System.Drawing.Size(116, 20);
            this.txtValorRecibo.TabIndex = 3;
            this.txtValorRecibo.Text = "0,00";
            this.txtValorRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorRecibo.Enter += new System.EventHandler(this.txtValorRecibo_Enter);
            this.txtValorRecibo.Validating += new System.ComponentModel.CancelEventHandler(this.txtValorRecibo_Validating);
            // 
            // cbCliente
            // 
            this.cbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(13, 30);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(319, 21);
            this.cbCliente.TabIndex = 0;
            this.cbCliente.Enter += new System.EventHandler(this.cbCliente_Enter);
            this.cbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCliente_KeyDown);
            this.cbCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCliente_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cliente:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument5
            // 
            this.printDocument5.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument5_PrintPage);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // FrmReciboAvulso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 306);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmReciboAvulso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emissão de Recibo Avulso - Cliente";
            this.Load += new System.EventHandler(this.FrmReciboAvulso_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReciboAvulso_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValorRecibo;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReferente;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox msktDataEmissao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbduasvias;
        private System.Windows.Forms.RadioButton rbumavia;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btnCadCliente;
        private System.Windows.Forms.CheckBox chkModelo2;
    }
}