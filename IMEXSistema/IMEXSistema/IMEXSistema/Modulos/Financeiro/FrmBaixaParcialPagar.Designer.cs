namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmBaixaParcialPagar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaixaParcialPagar));
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Pagto:";
            // 
            // msktDataPagto
            // 
            this.msktDataPagto.Location = new System.Drawing.Point(165, 25);
            this.msktDataPagto.Mask = "00/00/0000";
            this.msktDataPagto.Name = "msktDataPagto";
            this.msktDataPagto.Size = new System.Drawing.Size(79, 20);
            this.msktDataPagto.TabIndex = 0;
            this.msktDataPagto.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Valor Pagto:";
            // 
            // txtValorPago
            // 
            this.txtValorPago.Location = new System.Drawing.Point(250, 24);
            this.txtValorPago.MaxLength = 20;
            this.txtValorPago.Name = "txtValorPago";
            this.txtValorPago.Size = new System.Drawing.Size(94, 20);
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
            this.button1.Location = new System.Drawing.Point(15, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Baixa Parcial";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(126, 154);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 3;
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
            this.lblNDuplicata.Location = new System.Drawing.Point(12, 31);
            this.lblNDuplicata.Name = "lblNDuplicata";
            this.lblNDuplicata.Size = new System.Drawing.Size(70, 13);
            this.lblNDuplicata.TabIndex = 11;
            this.lblNDuplicata.Text = "lblNDuplicata";
            // 
            // btnAddCentroCusto
            // 
            this.btnAddCentroCusto.FlatAppearance.BorderSize = 0;
            this.btnAddCentroCusto.Location = new System.Drawing.Point(318, 126);
            this.btnAddCentroCusto.Name = "btnAddCentroCusto";
            this.btnAddCentroCusto.Size = new System.Drawing.Size(26, 21);
            this.btnAddCentroCusto.TabIndex = 117;
            this.btnAddCentroCusto.UseVisualStyleBackColor = true;
            this.btnAddCentroCusto.Click += new System.EventHandler(this.btnAddCentroCusto_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 116;
            this.label5.Text = "Centro de Custos:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.FormattingEnabled = true;
            this.cbCentroCusto.Location = new System.Drawing.Point(15, 127);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(297, 21);
            this.cbCentroCusto.TabIndex = 113;
            this.cbCentroCusto.Enter += new System.EventHandler(this.cbCentroCusto_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 115;
            this.label4.Text = "Tipo de Pagamento:";
            // 
            // btnCadTipo
            // 
            this.btnCadTipo.FlatAppearance.BorderSize = 0;
            this.btnCadTipo.Location = new System.Drawing.Point(250, 87);
            this.btnCadTipo.Name = "btnCadTipo";
            this.btnCadTipo.Size = new System.Drawing.Size(26, 21);
            this.btnCadTipo.TabIndex = 114;
            this.btnCadTipo.UseVisualStyleBackColor = true;
            this.btnCadTipo.Click += new System.EventHandler(this.btnCadTipo_Click);
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(15, 87);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(229, 21);
            this.cbTipo.TabIndex = 112;
            this.cbTipo.Enter += new System.EventHandler(this.cbTipo_Enter);
            // 
            // chkEntraCaixa
            // 
            this.chkEntraCaixa.AutoSize = true;
            this.chkEntraCaixa.Location = new System.Drawing.Point(15, 51);
            this.chkEntraCaixa.Name = "chkEntraCaixa";
            this.chkEntraCaixa.Size = new System.Drawing.Size(107, 17);
            this.chkEntraCaixa.TabIndex = 111;
            this.chkEntraCaixa.Text = "Entrada no Caixa";
            this.chkEntraCaixa.UseVisualStyleBackColor = true;
            this.chkEntraCaixa.CheckedChanged += new System.EventHandler(this.chkEntraCaixa_CheckedChanged);
            // 
            // FrmBaixaParcialPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 186);
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
            this.Name = "FrmBaixaParcialPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baixa Parcial da Duplicata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBaixar_FormClosing);
            this.Load += new System.EventHandler(this.FrmBaixar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
    }
}