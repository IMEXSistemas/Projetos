namespace BmsSoftware.Modulos.Servicos
{
    partial class FrmConfiguraServico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguraServico));
            this.cbRegimeTributacao = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCodNatureza = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInscMunicipalPrestador = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.cbOptanteSimples = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodEspecAtividadeFederal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodEspecAtividadeMunicipal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelEdiServico = new System.Windows.Forms.Button();
            this.btnAddServico = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.txtLocalInstall = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.txtAliquota = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtInscricaoEstadual = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbRegimeTributacao
            // 
            this.cbRegimeTributacao.FormattingEnabled = true;
            this.cbRegimeTributacao.Items.AddRange(new object[] {
            "1 - Microempresa Municipal",
            "2 – Estimativa",
            "3 – Sociedade de Profissionais",
            "4 – Cooperativa",
            "5 – Microempresário Individual (MEI)",
            "6 – Microempresário e Empresa de Pequeno Porte (ME EPP)",
            "7 – Tributação por Faturamento (Variável)",
            "8 – Fixo",
            "9 – Isenção",
            "10 – Imune",
            "11 - Exigibilidade suspensa por decisão judicial",
            "12 - Exigibilidade suspensa por procedimento"});
            this.cbRegimeTributacao.Location = new System.Drawing.Point(220, 25);
            this.cbRegimeTributacao.Name = "cbRegimeTributacao";
            this.cbRegimeTributacao.Size = new System.Drawing.Size(232, 21);
            this.cbRegimeTributacao.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 189;
            this.label5.Text = "Regime Tributação";
            // 
            // cbCodNatureza
            // 
            this.cbCodNatureza.FormattingEnabled = true;
            this.cbCodNatureza.Items.AddRange(new object[] {
            "1 - Tributação no município",
            "2 – Tributação fora do município",
            "3 – Isenção",
            "4 – Imune",
            "5 – Exigibilidade suspensa por decisão Judicial",
            "6 – Exigibilidade suspensa por procedimento administrativo"});
            this.cbCodNatureza.Location = new System.Drawing.Point(15, 25);
            this.cbCodNatureza.Name = "cbCodNatureza";
            this.cbCodNatureza.Size = new System.Drawing.Size(196, 21);
            this.cbCodNatureza.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 187;
            this.label3.Text = "Cod. Natureza";
            // 
            // txtInscMunicipalPrestador
            // 
            this.txtInscMunicipalPrestador.Location = new System.Drawing.Point(15, 65);
            this.txtInscMunicipalPrestador.MaxLength = 15;
            this.txtInscMunicipalPrestador.Name = "txtInscMunicipalPrestador";
            this.txtInscMunicipalPrestador.Size = new System.Drawing.Size(196, 20);
            this.txtInscMunicipalPrestador.TabIndex = 3;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(12, 49);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(161, 13);
            this.label82.TabIndex = 237;
            this.label82.Text = "Inscrição Municipal do Prestador";
            // 
            // cbOptanteSimples
            // 
            this.cbOptanteSimples.FormattingEnabled = true;
            this.cbOptanteSimples.Items.AddRange(new object[] {
            "1 – Sim",
            "2 – Não"});
            this.cbOptanteSimples.Location = new System.Drawing.Point(220, 65);
            this.cbOptanteSimples.Name = "cbOptanteSimples";
            this.cbOptanteSimples.Size = new System.Drawing.Size(132, 21);
            this.cbOptanteSimples.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 238;
            this.label1.Text = "Optante pelo Simples";
            // 
            // txtCodEspecAtividadeFederal
            // 
            this.txtCodEspecAtividadeFederal.Location = new System.Drawing.Point(15, 143);
            this.txtCodEspecAtividadeFederal.MaxLength = 10;
            this.txtCodEspecAtividadeFederal.Name = "txtCodEspecAtividadeFederal";
            this.txtCodEspecAtividadeFederal.Size = new System.Drawing.Size(222, 20);
            this.txtCodEspecAtividadeFederal.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 13);
            this.label2.TabIndex = 241;
            this.label2.Text = "Código de Especificação da Atividade Federal";
            // 
            // txtCodEspecAtividadeMunicipal
            // 
            this.txtCodEspecAtividadeMunicipal.Location = new System.Drawing.Point(246, 143);
            this.txtCodEspecAtividadeMunicipal.MaxLength = 20;
            this.txtCodEspecAtividadeMunicipal.Name = "txtCodEspecAtividadeMunicipal";
            this.txtCodEspecAtividadeMunicipal.Size = new System.Drawing.Size(232, 20);
            this.txtCodEspecAtividadeMunicipal.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 13);
            this.label4.TabIndex = 243;
            this.label4.Text = "Código de Especificação da Atividade Municipal";
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(461, 25);
            this.txtSerie.MaxLength = 5;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(70, 20);
            this.txtSerie.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(458, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 245;
            this.label6.Text = "Série";
            // 
            // btnCancelEdiServico
            // 
            this.btnCancelEdiServico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelEdiServico.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelEdiServico.Image")));
            this.btnCancelEdiServico.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelEdiServico.Location = new System.Drawing.Point(98, 183);
            this.btnCancelEdiServico.Name = "btnCancelEdiServico";
            this.btnCancelEdiServico.Size = new System.Drawing.Size(77, 23);
            this.btnCancelEdiServico.TabIndex = 11;
            this.btnCancelEdiServico.Text = "S&air";
            this.btnCancelEdiServico.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelEdiServico.UseVisualStyleBackColor = true;
            this.btnCancelEdiServico.Click += new System.EventHandler(this.btnCancelEdiServico_Click);
            // 
            // btnAddServico
            // 
            this.btnAddServico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddServico.Image = ((System.Drawing.Image)(resources.GetObject("btnAddServico.Image")));
            this.btnAddServico.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddServico.Location = new System.Drawing.Point(15, 183);
            this.btnAddServico.Name = "btnAddServico";
            this.btnAddServico.Size = new System.Drawing.Size(77, 23);
            this.btnAddServico.TabIndex = 10;
            this.btnAddServico.Text = "&Salvar";
            this.btnAddServico.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddServico.UseVisualStyleBackColor = true;
            this.btnAddServico.Click += new System.EventHandler(this.btnAddServico_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(699, 63);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(31, 23);
            this.btnInstall.TabIndex = 250;
            this.btnInstall.Text = "...";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // txtLocalInstall
            // 
            this.txtLocalInstall.Location = new System.Drawing.Point(358, 65);
            this.txtLocalInstall.MaxLength = 500;
            this.txtLocalInstall.Name = "txtLocalInstall";
            this.txtLocalInstall.Size = new System.Drawing.Size(335, 20);
            this.txtLocalInstall.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(355, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 13);
            this.label7.TabIndex = 248;
            this.label7.Text = "Local que salva o arquivo texto";
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog1";
            this.openFileDialog3.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog3_FileOk);
            // 
            // txtAliquota
            // 
            this.txtAliquota.Location = new System.Drawing.Point(487, 143);
            this.txtAliquota.MaxLength = 4;
            this.txtAliquota.Name = "txtAliquota";
            this.txtAliquota.Size = new System.Drawing.Size(46, 20);
            this.txtAliquota.TabIndex = 9;
            this.txtAliquota.Text = "0,00";
            this.txtAliquota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAliquota.Enter += new System.EventHandler(this.txtAliquota_Enter);
            this.txtAliquota.Validating += new System.ComponentModel.CancelEventHandler(this.txtAliquota_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(484, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 252;
            this.label8.Text = "Alíquota";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtInscricaoEstadual
            // 
            this.txtInscricaoEstadual.Location = new System.Drawing.Point(17, 104);
            this.txtInscricaoEstadual.MaxLength = 20;
            this.txtInscricaoEstadual.Name = "txtInscricaoEstadual";
            this.txtInscricaoEstadual.Size = new System.Drawing.Size(196, 20);
            this.txtInscricaoEstadual.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 13);
            this.label9.TabIndex = 254;
            this.label9.Text = "Inscrição Estadual do Prestador";
            // 
            // FrmConfiguraServico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 218);
            this.Controls.Add(this.txtInscricaoEstadual);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAliquota);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.txtLocalInstall);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancelEdiServico);
            this.Controls.Add(this.btnAddServico);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCodEspecAtividadeMunicipal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCodEspecAtividadeFederal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOptanteSimples);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInscMunicipalPrestador);
            this.Controls.Add(this.label82);
            this.Controls.Add(this.cbRegimeTributacao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCodNatureza);
            this.Controls.Add(this.label3);
            this.Name = "FrmConfiguraServico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração da Nota Fiscal Serviço";
            this.Load += new System.EventHandler(this.FrmConfiguraServico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRegimeTributacao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCodNatureza;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInscMunicipalPrestador;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.ComboBox cbOptanteSimples;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodEspecAtividadeFederal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodEspecAtividadeMunicipal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelEdiServico;
        private System.Windows.Forms.Button btnAddServico;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.TextBox txtLocalInstall;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.TextBox txtAliquota;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtInscricaoEstadual;
        private System.Windows.Forms.Label label9;
    }
}