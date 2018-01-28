namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmBaixaParcialReceber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaixaParcialReceber));
            this.label1 = new System.Windows.Forms.Label();
            this.msktDataPagto = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValorPago = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblNDuplicata = new System.Windows.Forms.Label();
            this.btnAddCentroCusto = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCentroCusto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCadTipo = new System.Windows.Forms.Button();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.chkEntraCaixa = new System.Windows.Forms.CheckBox();
            this.chkImprimirRecibo = new System.Windows.Forms.CheckBox();
            this.printDocument4 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNomeMoviment = new System.Windows.Forms.Button();
            this.cbNomeMovimentacao = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCadContCorrent = new System.Windows.Forms.Button();
            this.cbContaCorrente = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Pagto:";
            // 
            // msktDataPagto
            // 
            this.msktDataPagto.Location = new System.Drawing.Point(172, 25);
            this.msktDataPagto.Mask = "00/00/0000";
            this.msktDataPagto.Name = "msktDataPagto";
            this.msktDataPagto.Size = new System.Drawing.Size(79, 20);
            this.msktDataPagto.TabIndex = 0;
            this.msktDataPagto.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Valor Pagto:";
            // 
            // txtValorPago
            // 
            this.txtValorPago.Location = new System.Drawing.Point(257, 25);
            this.txtValorPago.MaxLength = 20;
            this.txtValorPago.Name = "txtValorPago";
            this.txtValorPago.Size = new System.Drawing.Size(87, 20);
            this.txtValorPago.TabIndex = 1;
            this.txtValorPago.Text = "0,00";
            this.txtValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorPago.Leave += new System.EventHandler(this.txtValorPago_Leave);
            this.txtValorPago.Validating += new System.ComponentModel.CancelEventHandler(this.txtValorPago_Validating);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(14, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Baixa Parcial";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(125, 275);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 6;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Duplicata:";
            // 
            // lblNDuplicata
            // 
            this.lblNDuplicata.AutoSize = true;
            this.lblNDuplicata.Location = new System.Drawing.Point(12, 28);
            this.lblNDuplicata.Name = "lblNDuplicata";
            this.lblNDuplicata.Size = new System.Drawing.Size(70, 13);
            this.lblNDuplicata.TabIndex = 11;
            this.lblNDuplicata.Text = "lblNDuplicata";
            // 
            // btnAddCentroCusto
            // 
            this.btnAddCentroCusto.FlatAppearance.BorderSize = 0;
            this.btnAddCentroCusto.Location = new System.Drawing.Point(318, 129);
            this.btnAddCentroCusto.Name = "btnAddCentroCusto";
            this.btnAddCentroCusto.Size = new System.Drawing.Size(26, 21);
            this.btnAddCentroCusto.TabIndex = 110;
            this.btnAddCentroCusto.UseVisualStyleBackColor = true;
            this.btnAddCentroCusto.Click += new System.EventHandler(this.btnAddCentroCusto_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 109;
            this.label5.Text = "Centro de Custos:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.FormattingEnabled = true;
            this.cbCentroCusto.Location = new System.Drawing.Point(15, 130);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(297, 21);
            this.cbCentroCusto.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Tipo de Pagamento:";
            // 
            // btnCadTipo
            // 
            this.btnCadTipo.FlatAppearance.BorderSize = 0;
            this.btnCadTipo.Location = new System.Drawing.Point(250, 90);
            this.btnCadTipo.Name = "btnCadTipo";
            this.btnCadTipo.Size = new System.Drawing.Size(26, 21);
            this.btnCadTipo.TabIndex = 106;
            this.btnCadTipo.UseVisualStyleBackColor = true;
            this.btnCadTipo.Click += new System.EventHandler(this.btnCadTipo_Click);
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(15, 90);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(229, 21);
            this.cbTipo.TabIndex = 3;
            // 
            // chkEntraCaixa
            // 
            this.chkEntraCaixa.AutoSize = true;
            this.chkEntraCaixa.Location = new System.Drawing.Point(15, 54);
            this.chkEntraCaixa.Name = "chkEntraCaixa";
            this.chkEntraCaixa.Size = new System.Drawing.Size(107, 17);
            this.chkEntraCaixa.TabIndex = 2;
            this.chkEntraCaixa.Text = "Entrada no Caixa";
            this.chkEntraCaixa.UseVisualStyleBackColor = true;
            this.chkEntraCaixa.CheckedChanged += new System.EventHandler(this.chkEntraCaixa_CheckedChanged);
            // 
            // chkImprimirRecibo
            // 
            this.chkImprimirRecibo.AutoSize = true;
            this.chkImprimirRecibo.Location = new System.Drawing.Point(135, 54);
            this.chkImprimirRecibo.Name = "chkImprimirRecibo";
            this.chkImprimirRecibo.Size = new System.Drawing.Size(98, 17);
            this.chkImprimirRecibo.TabIndex = 111;
            this.chkImprimirRecibo.Text = "Imprimir Recibo";
            this.chkImprimirRecibo.UseVisualStyleBackColor = true;
            // 
            // printDocument4
            // 
            this.printDocument4.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument4_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNomeMoviment);
            this.groupBox1.Controls.Add(this.cbNomeMovimentacao);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnCadContCorrent);
            this.groupBox1.Controls.Add(this.cbContaCorrente);
            this.groupBox1.Location = new System.Drawing.Point(14, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 114);
            this.groupBox1.TabIndex = 178;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conta Bancária";
            // 
            // btnNomeMoviment
            // 
            this.btnNomeMoviment.FlatAppearance.BorderSize = 0;
            this.btnNomeMoviment.Location = new System.Drawing.Point(293, 72);
            this.btnNomeMoviment.Name = "btnNomeMoviment";
            this.btnNomeMoviment.Size = new System.Drawing.Size(26, 21);
            this.btnNomeMoviment.TabIndex = 265;
            this.btnNomeMoviment.UseVisualStyleBackColor = true;
            this.btnNomeMoviment.Click += new System.EventHandler(this.btnNomeMoviment_Click);
            // 
            // cbNomeMovimentacao
            // 
            this.cbNomeMovimentacao.FormattingEnabled = true;
            this.cbNomeMovimentacao.Location = new System.Drawing.Point(9, 72);
            this.cbNomeMovimentacao.Name = "cbNomeMovimentacao";
            this.cbNomeMovimentacao.Size = new System.Drawing.Size(278, 21);
            this.cbNomeMovimentacao.TabIndex = 263;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 264;
            this.label7.Text = "Nome da Movimentação:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 175;
            this.label6.Text = "Conta Bancária:";
            // 
            // btnCadContCorrent
            // 
            this.btnCadContCorrent.FlatAppearance.BorderSize = 0;
            this.btnCadContCorrent.Location = new System.Drawing.Point(293, 32);
            this.btnCadContCorrent.Name = "btnCadContCorrent";
            this.btnCadContCorrent.Size = new System.Drawing.Size(26, 21);
            this.btnCadContCorrent.TabIndex = 176;
            this.btnCadContCorrent.UseVisualStyleBackColor = true;
            this.btnCadContCorrent.Click += new System.EventHandler(this.btnCadContCorrent_Click);
            // 
            // cbContaCorrente
            // 
            this.cbContaCorrente.FormattingEnabled = true;
            this.cbContaCorrente.Location = new System.Drawing.Point(9, 32);
            this.cbContaCorrente.Name = "cbContaCorrente";
            this.cbContaCorrente.Size = new System.Drawing.Size(278, 21);
            this.cbContaCorrente.TabIndex = 174;
            // 
            // FrmBaixaParcialReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 307);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkImprimirRecibo);
            this.Controls.Add(this.btnAddCentroCusto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCentroCusto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCadTipo);
            this.Controls.Add(this.cbTipo);
            this.Controls.Add(this.chkEntraCaixa);
            this.Controls.Add(this.lblNDuplicata);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtValorPago);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.msktDataPagto);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "FrmBaixaParcialReceber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baixa Parcial da Duplicata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBaixar_FormClosing);
            this.Load += new System.EventHandler(this.FrmBaixar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox msktDataPagto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValorPago;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblNDuplicata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddCentroCusto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCentroCusto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCadTipo;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.CheckBox chkEntraCaixa;
        private System.Windows.Forms.CheckBox chkImprimirRecibo;
        private System.Drawing.Printing.PrintDocument printDocument4;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNomeMoviment;
        private System.Windows.Forms.ComboBox cbNomeMovimentacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCadContCorrent;
        private System.Windows.Forms.ComboBox cbContaCorrente;
    }
}