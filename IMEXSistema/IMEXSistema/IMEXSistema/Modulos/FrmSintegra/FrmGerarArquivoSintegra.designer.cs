namespace BmsSoftware.Modulos.FrmSintegra
{
    partial class FrmGerarArquivoSintegra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGerarArquivoSintegra));
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCodigoConvenio = new System.Windows.Forms.ComboBox();
            this.cbNaturezaInformacao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFinalidadeArquivo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpkFim = new System.Windows.Forms.DateTimePicker();
            this.dtpkInicial = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtNomeContato = new System.Windows.Forms.TextBox();
            this.chkregistro54 = new System.Windows.Forms.CheckBox();
            this.chkRegistro60M = new System.Windows.Forms.CheckBox();
            this.chkRegistro60A = new System.Windows.Forms.CheckBox();
            this.chkRegistro60D = new System.Windows.Forms.CheckBox();
            this.chkRegistro60I = new System.Windows.Forms.CheckBox();
            this.chkRegistro60R = new System.Windows.Forms.CheckBox();
            this.chkInventario = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGerar.Image")));
            this.btnGerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerar.Location = new System.Drawing.Point(12, 298);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(75, 23);
            this.btnGerar.TabIndex = 0;
            this.btnGerar.Text = "&Gerar";
            this.btnGerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(93, 298);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 1;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Código do Convênio:";
            // 
            // cbCodigoConvenio
            // 
            this.cbCodigoConvenio.FormattingEnabled = true;
            this.cbCodigoConvenio.Items.AddRange(new object[] {
            "1 - Convênio 57/95 Versão 31/99 Alt. 30/02",
            "2 - Convênio 57/95 Versão 69/02 Alt. 142/02",
            "3 - Convênio 57/95 Alt. 76/03"});
            this.cbCodigoConvenio.Location = new System.Drawing.Point(15, 26);
            this.cbCodigoConvenio.Name = "cbCodigoConvenio";
            this.cbCodigoConvenio.Size = new System.Drawing.Size(442, 21);
            this.cbCodigoConvenio.TabIndex = 3;
            // 
            // cbNaturezaInformacao
            // 
            this.cbNaturezaInformacao.FormattingEnabled = true;
            this.cbNaturezaInformacao.Location = new System.Drawing.Point(15, 67);
            this.cbNaturezaInformacao.Name = "cbNaturezaInformacao";
            this.cbNaturezaInformacao.Size = new System.Drawing.Size(442, 21);
            this.cbNaturezaInformacao.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Natureza das Informações:";
            // 
            // cbFinalidadeArquivo
            // 
            this.cbFinalidadeArquivo.FormattingEnabled = true;
            this.cbFinalidadeArquivo.Location = new System.Drawing.Point(15, 108);
            this.cbFinalidadeArquivo.Name = "cbFinalidadeArquivo";
            this.cbFinalidadeArquivo.Size = new System.Drawing.Size(442, 21);
            this.cbFinalidadeArquivo.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Finalidade do Arquivo:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpkFim);
            this.groupBox1.Controls.Add(this.dtpkInicial);
            this.groupBox1.Location = new System.Drawing.Point(12, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 76);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(124, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Final:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ínicial:";
            // 
            // dtpkFim
            // 
            this.dtpkFim.CustomFormat = "dd/MM/yyyy";
            this.dtpkFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkFim.Location = new System.Drawing.Point(127, 38);
            this.dtpkFim.Name = "dtpkFim";
            this.dtpkFim.Size = new System.Drawing.Size(105, 20);
            this.dtpkFim.TabIndex = 1;
            this.dtpkFim.ValueChanged += new System.EventHandler(this.dtpkFim_ValueChanged);
            // 
            // dtpkInicial
            // 
            this.dtpkInicial.CustomFormat = "dd/MM/yyyy";
            this.dtpkInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkInicial.Location = new System.Drawing.Point(16, 38);
            this.dtpkInicial.Name = "dtpkInicial";
            this.dtpkInicial.Size = new System.Drawing.Size(105, 20);
            this.dtpkInicial.TabIndex = 0;
            this.dtpkInicial.ValueChanged += new System.EventHandler(this.dtpkInicial_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(12, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Registro: 10,11,50,70 e 90";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Nome do Contato na Empresa:";
            // 
            // txtNomeContato
            // 
            this.txtNomeContato.Location = new System.Drawing.Point(15, 148);
            this.txtNomeContato.Name = "txtNomeContato";
            this.txtNomeContato.Size = new System.Drawing.Size(442, 20);
            this.txtNomeContato.TabIndex = 11;
            // 
            // chkregistro54
            // 
            this.chkregistro54.AutoSize = true;
            this.chkregistro54.Checked = true;
            this.chkregistro54.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkregistro54.Location = new System.Drawing.Point(243, 174);
            this.chkregistro54.Name = "chkregistro54";
            this.chkregistro54.Size = new System.Drawing.Size(97, 17);
            this.chkregistro54.TabIndex = 12;
            this.chkregistro54.Text = "Registro 54/75";
            this.chkregistro54.UseVisualStyleBackColor = true;
            this.chkregistro54.CheckedChanged += new System.EventHandler(this.chkregistro54_CheckedChanged);
            // 
            // chkRegistro60M
            // 
            this.chkRegistro60M.AutoSize = true;
            this.chkRegistro60M.Checked = true;
            this.chkRegistro60M.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRegistro60M.Location = new System.Drawing.Point(363, 174);
            this.chkRegistro60M.Name = "chkRegistro60M";
            this.chkRegistro60M.Size = new System.Drawing.Size(89, 17);
            this.chkRegistro60M.TabIndex = 13;
            this.chkRegistro60M.Text = "Registro 60M";
            this.chkRegistro60M.UseVisualStyleBackColor = true;
            // 
            // chkRegistro60A
            // 
            this.chkRegistro60A.AutoSize = true;
            this.chkRegistro60A.Checked = true;
            this.chkRegistro60A.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRegistro60A.Location = new System.Drawing.Point(363, 197);
            this.chkRegistro60A.Name = "chkRegistro60A";
            this.chkRegistro60A.Size = new System.Drawing.Size(87, 17);
            this.chkRegistro60A.TabIndex = 14;
            this.chkRegistro60A.Text = "Registro 60A";
            this.chkRegistro60A.UseVisualStyleBackColor = true;
            // 
            // chkRegistro60D
            // 
            this.chkRegistro60D.AutoSize = true;
            this.chkRegistro60D.Location = new System.Drawing.Point(362, 243);
            this.chkRegistro60D.Name = "chkRegistro60D";
            this.chkRegistro60D.Size = new System.Drawing.Size(88, 17);
            this.chkRegistro60D.TabIndex = 15;
            this.chkRegistro60D.Text = "Registro 60D";
            this.chkRegistro60D.UseVisualStyleBackColor = true;
            this.chkRegistro60D.Visible = false;
            // 
            // chkRegistro60I
            // 
            this.chkRegistro60I.AutoSize = true;
            this.chkRegistro60I.Location = new System.Drawing.Point(362, 266);
            this.chkRegistro60I.Name = "chkRegistro60I";
            this.chkRegistro60I.Size = new System.Drawing.Size(83, 17);
            this.chkRegistro60I.TabIndex = 16;
            this.chkRegistro60I.Text = "Registro 60I";
            this.chkRegistro60I.UseVisualStyleBackColor = true;
            this.chkRegistro60I.Visible = false;
            // 
            // chkRegistro60R
            // 
            this.chkRegistro60R.AutoSize = true;
            this.chkRegistro60R.Location = new System.Drawing.Point(362, 220);
            this.chkRegistro60R.Name = "chkRegistro60R";
            this.chkRegistro60R.Size = new System.Drawing.Size(88, 17);
            this.chkRegistro60R.TabIndex = 17;
            this.chkRegistro60R.Text = "Registro 60R";
            this.chkRegistro60R.UseVisualStyleBackColor = true;
            this.chkRegistro60R.Visible = false;
            // 
            // chkInventario
            // 
            this.chkInventario.AutoSize = true;
            this.chkInventario.Location = new System.Drawing.Point(260, 304);
            this.chkInventario.Name = "chkInventario";
            this.chkInventario.Size = new System.Drawing.Size(164, 17);
            this.chkInventario.TabIndex = 20;
            this.chkInventario.Text = "Arquivos de Inventário 74/75";
            this.chkInventario.UseVisualStyleBackColor = true;
            this.chkInventario.CheckedChanged += new System.EventHandler(this.chkInventario_CheckedChanged);
            // 
            // FrmGerarArquivoSintegra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 328);
            this.Controls.Add(this.chkInventario);
            this.Controls.Add(this.chkRegistro60R);
            this.Controls.Add(this.chkRegistro60I);
            this.Controls.Add(this.chkRegistro60D);
            this.Controls.Add(this.chkRegistro60A);
            this.Controls.Add(this.chkRegistro60M);
            this.Controls.Add(this.chkregistro54);
            this.Controls.Add(this.txtNomeContato);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbFinalidadeArquivo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbNaturezaInformacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbCodigoConvenio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnGerar);
            this.Name = "FrmGerarArquivoSintegra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar Arquivo Sintegra";
            this.Load += new System.EventHandler(this.FrmGerarArquivoSintegra_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCodigoConvenio;
        private System.Windows.Forms.ComboBox cbNaturezaInformacao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFinalidadeArquivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpkFim;
        private System.Windows.Forms.DateTimePicker dtpkInicial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtNomeContato;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkregistro54;
        private System.Windows.Forms.CheckBox chkRegistro60M;
        private System.Windows.Forms.CheckBox chkRegistro60A;
        private System.Windows.Forms.CheckBox chkRegistro60D;
        private System.Windows.Forms.CheckBox chkRegistro60I;
        private System.Windows.Forms.CheckBox chkRegistro60R;
        private System.Windows.Forms.CheckBox chkInventario;
    }
}