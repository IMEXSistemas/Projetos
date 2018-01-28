namespace BmsSoftware.Modulos.FrmSped.FrmFiscal
{
    partial class FrmSpedFiscal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpedFiscal));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVersaoLayout = new System.Windows.Forms.ComboBox();
            this.cbFinalidadeArquivo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPerfilApreArquivo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpkFim = new System.Windows.Forms.DateTimePicker();
            this.dtpkInicial = new System.Windows.Forms.DateTimePicker();
            this.cbInventario = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.cbContador = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCadContador = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblObervacao = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sped Fiscal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Versão do Layout:";
            // 
            // cbVersaoLayout
            // 
            this.cbVersaoLayout.FormattingEnabled = true;
            this.cbVersaoLayout.Items.AddRange(new object[] {
            "001 - Versão 1.00",
            "002 - Versão 1.01",
            "003 - Versão 1.02",
            "004 - Versão 1.03",
            "005 - Versão 1.04",
            "006 - Versão 1.05",
            "007 - Versão 1.06",
            "008 - Versão 1.07",
            "009 - Versão 1.08",
            "010 - Versão 1.09",
            "011 - Versão 1.10"});
            this.cbVersaoLayout.Location = new System.Drawing.Point(12, 111);
            this.cbVersaoLayout.Name = "cbVersaoLayout";
            this.cbVersaoLayout.Size = new System.Drawing.Size(277, 21);
            this.cbVersaoLayout.TabIndex = 3;
            // 
            // cbFinalidadeArquivo
            // 
            this.cbFinalidadeArquivo.FormattingEnabled = true;
            this.cbFinalidadeArquivo.Items.AddRange(new object[] {
            "0 - Remessa do arquivo original",
            "1 - Remessa do arquivo substituto"});
            this.cbFinalidadeArquivo.Location = new System.Drawing.Point(298, 111);
            this.cbFinalidadeArquivo.Name = "cbFinalidadeArquivo";
            this.cbFinalidadeArquivo.Size = new System.Drawing.Size(277, 21);
            this.cbFinalidadeArquivo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Finalidade do Arquivo:";
            // 
            // cbPerfilApreArquivo
            // 
            this.cbPerfilApreArquivo.FormattingEnabled = true;
            this.cbPerfilApreArquivo.Items.AddRange(new object[] {
            "A - Perfil A",
            "B - Perfil B",
            "C - Perfil C"});
            this.cbPerfilApreArquivo.Location = new System.Drawing.Point(12, 153);
            this.cbPerfilApreArquivo.Name = "cbPerfilApreArquivo";
            this.cbPerfilApreArquivo.Size = new System.Drawing.Size(277, 21);
            this.cbPerfilApreArquivo.TabIndex = 7;
            this.cbPerfilApreArquivo.SelectedValueChanged += new System.EventHandler(this.cbPerfilApreArquivo_SelectedValueChanged);
            this.cbPerfilApreArquivo.Enter += new System.EventHandler(this.cbPerfilApreArquivo_Enter);
            this.cbPerfilApreArquivo.Leave += new System.EventHandler(this.cbPerfilApreArquivo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Perfil de Apresentação do Arquivo:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpkFim);
            this.groupBox1.Controls.Add(this.dtpkInicial);
            this.groupBox1.Location = new System.Drawing.Point(298, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Final:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ínicial:";
            // 
            // dtpkFim
            // 
            this.dtpkFim.CustomFormat = "dd/MM/yyyy";
            this.dtpkFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkFim.Location = new System.Drawing.Point(143, 41);
            this.dtpkFim.Name = "dtpkFim";
            this.dtpkFim.Size = new System.Drawing.Size(105, 20);
            this.dtpkFim.TabIndex = 1;
            // 
            // dtpkInicial
            // 
            this.dtpkInicial.CustomFormat = "dd/MM/yyyy";
            this.dtpkInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkInicial.Location = new System.Drawing.Point(32, 41);
            this.dtpkInicial.Name = "dtpkInicial";
            this.dtpkInicial.Size = new System.Drawing.Size(105, 20);
            this.dtpkInicial.TabIndex = 0;
            // 
            // cbInventario
            // 
            this.cbInventario.FormattingEnabled = true;
            this.cbInventario.Items.AddRange(new object[] {
            "00 - Sem Inventário",
            "01 – No final no período",
            "02 – Na mudança de forma de tributação da mercadoria (ICMS)",
            "03 – Na solicitação da baixa cadastral, paralisação temporária e outras situações" +
                "",
            "04 – Na alteração de regime de pagamento – condição do contribuinte",
            "05 – Por determinação dos fiscos"});
            this.cbInventario.Location = new System.Drawing.Point(12, 193);
            this.cbInventario.Name = "cbInventario";
            this.cbInventario.Size = new System.Drawing.Size(277, 21);
            this.cbInventario.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Inventário:";
            // 
            // btnGerar
            // 
            this.btnGerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGerar.Image")));
            this.btnGerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerar.Location = new System.Drawing.Point(419, 260);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(75, 23);
            this.btnGerar.TabIndex = 12;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(500, 259);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 13;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // cbContador
            // 
            this.cbContador.FormattingEnabled = true;
            this.cbContador.Items.AddRange(new object[] {
            "00 - Sem Inventário",
            "01 – No final no período",
            "02 – Na mudança de forma de tributação da mercadoria (ICMS)",
            "03 – Na solicitação da baixa cadastral, paralisação temporária e outras situações" +
                "",
            "04 – Na alteração de regime de pagamento – condição do contribuinte",
            "05 – Por determinação dos fiscos"});
            this.cbContador.Location = new System.Drawing.Point(12, 233);
            this.cbContador.Name = "cbContador";
            this.cbContador.Size = new System.Drawing.Size(277, 21);
            this.cbContador.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Contador:";
            // 
            // btnCadContador
            // 
            this.btnCadContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadContador.FlatAppearance.BorderSize = 0;
            this.btnCadContador.Location = new System.Drawing.Point(298, 232);
            this.btnCadContador.Name = "btnCadContador";
            this.btnCadContador.Size = new System.Drawing.Size(26, 21);
            this.btnCadContador.TabIndex = 171;
            this.btnCadContador.UseVisualStyleBackColor = true;
            this.btnCadContador.Click += new System.EventHandler(this.btnCadContador_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblObervacao
            // 
            this.lblObervacao.AutoSize = true;
            this.lblObervacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblObervacao.Location = new System.Drawing.Point(12, 272);
            this.lblObervacao.Name = "lblObervacao";
            this.lblObervacao.Size = new System.Drawing.Size(68, 13);
            this.lblObervacao.TabIndex = 172;
            this.lblObervacao.Text = "Observação:";
            // 
            // FrmSpedFiscal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 294);
            this.Controls.Add(this.lblObervacao);
            this.Controls.Add(this.btnCadContador);
            this.Controls.Add(this.cbContador);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.cbInventario);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbPerfilApreArquivo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFinalidadeArquivo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbVersaoLayout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmSpedFiscal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sped Fiscal - ICMS/IPI";
            this.Load += new System.EventHandler(this.FrmSpedFiscal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbVersaoLayout;
        private System.Windows.Forms.ComboBox cbFinalidadeArquivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPerfilApreArquivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpkFim;
        private System.Windows.Forms.DateTimePicker dtpkInicial;
        private System.Windows.Forms.ComboBox cbInventario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ComboBox cbContador;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCadContador;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblObervacao;
    }
}