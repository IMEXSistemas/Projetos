namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmExtratoContaBancaria
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExtratoContaBancaria));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSaldoGeral = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mkdatafinal = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mkDtInicial = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.NUMMOVIMENTACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMETIPOMOVCAIXA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEMOVIMENTACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAMOVIMENTACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEBANCO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomeconta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomecontacorr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AGENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDMOVCTCORRENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbContaCorrente = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSaldoGeral);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.DataGriewDados);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnConsultar);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cbContaCorrente);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 186);
            this.panel1.TabIndex = 0;
            // 
            // chkSaldoGeral
            // 
            this.chkSaldoGeral.AutoSize = true;
            this.chkSaldoGeral.Checked = true;
            this.chkSaldoGeral.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaldoGeral.Location = new System.Drawing.Point(15, 132);
            this.chkSaldoGeral.Name = "chkSaldoGeral";
            this.chkSaldoGeral.Size = new System.Drawing.Size(109, 17);
            this.chkSaldoGeral.TabIndex = 278;
            this.chkSaldoGeral.Text = "Exibir Saldo Geral";
            this.chkSaldoGeral.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mkdatafinal);
            this.groupBox2.Controls.Add(this.bntDateSelecFinal);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.mkDtInicial);
            this.groupBox2.Controls.Add(this.bntDateSelecInicial);
            this.groupBox2.Location = new System.Drawing.Point(15, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 73);
            this.groupBox2.TabIndex = 277;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Movimentação";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "Final:";
            // 
            // mkdatafinal
            // 
            this.mkdatafinal.Location = new System.Drawing.Point(171, 35);
            this.mkdatafinal.Mask = "00/00/0000";
            this.mkdatafinal.Name = "mkdatafinal";
            this.mkdatafinal.Size = new System.Drawing.Size(79, 20);
            this.mkdatafinal.TabIndex = 184;
            this.mkdatafinal.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(256, 35);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 186;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 182;
            this.label1.Text = "Inicial:";
            // 
            // mkDtInicial
            // 
            this.mkDtInicial.Location = new System.Drawing.Point(54, 35);
            this.mkDtInicial.Mask = "00/00/0000";
            this.mkDtInicial.Name = "mkDtInicial";
            this.mkDtInicial.Size = new System.Drawing.Size(79, 20);
            this.mkDtInicial.TabIndex = 181;
            this.mkDtInicial.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(139, 35);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 183;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NUMMOVIMENTACAO,
            this.VALOR,
            this.NOMETIPOMOVCAIXA,
            this.NOMEMOVIMENTACAO,
            this.DATAMOVIMENTACAO,
            this.NOMEBANCO,
            this.nomeconta,
            this.nomecontacorr,
            this.AGENCIA,
            this.IDMOVCTCORRENTE});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 203);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(332, 97);
            this.DataGriewDados.TabIndex = 276;
            this.DataGriewDados.Visible = false;
            // 
            // NUMMOVIMENTACAO
            // 
            this.NUMMOVIMENTACAO.DataPropertyName = "NUMMOVIMENTACAO";
            this.NUMMOVIMENTACAO.HeaderText = "Nº Mov.";
            this.NUMMOVIMENTACAO.Name = "NUMMOVIMENTACAO";
            this.NUMMOVIMENTACAO.ReadOnly = true;
            this.NUMMOVIMENTACAO.Width = 90;
            // 
            // VALOR
            // 
            this.VALOR.DataPropertyName = "VALOR";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.VALOR.DefaultCellStyle = dataGridViewCellStyle1;
            this.VALOR.HeaderText = "Valor Movimentação";
            this.VALOR.Name = "VALOR";
            this.VALOR.ReadOnly = true;
            // 
            // NOMETIPOMOVCAIXA
            // 
            this.NOMETIPOMOVCAIXA.DataPropertyName = "NOMETIPOMOVCAIXA";
            this.NOMETIPOMOVCAIXA.HeaderText = "Tipo Mov.";
            this.NOMETIPOMOVCAIXA.Name = "NOMETIPOMOVCAIXA";
            this.NOMETIPOMOVCAIXA.ReadOnly = true;
            this.NOMETIPOMOVCAIXA.Width = 80;
            // 
            // NOMEMOVIMENTACAO
            // 
            this.NOMEMOVIMENTACAO.DataPropertyName = "NOMEMOVIMENTACAO";
            this.NOMEMOVIMENTACAO.HeaderText = "Nome Movimentação";
            this.NOMEMOVIMENTACAO.Name = "NOMEMOVIMENTACAO";
            this.NOMEMOVIMENTACAO.ReadOnly = true;
            this.NOMEMOVIMENTACAO.Width = 200;
            // 
            // DATAMOVIMENTACAO
            // 
            this.DATAMOVIMENTACAO.DataPropertyName = "DATAMOVIMENTACAO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.DATAMOVIMENTACAO.DefaultCellStyle = dataGridViewCellStyle2;
            this.DATAMOVIMENTACAO.HeaderText = "Data";
            this.DATAMOVIMENTACAO.Name = "DATAMOVIMENTACAO";
            this.DATAMOVIMENTACAO.ReadOnly = true;
            this.DATAMOVIMENTACAO.Width = 90;
            // 
            // NOMEBANCO
            // 
            this.NOMEBANCO.DataPropertyName = "NOMEBANCO";
            this.NOMEBANCO.HeaderText = "Nome Banco";
            this.NOMEBANCO.Name = "NOMEBANCO";
            this.NOMEBANCO.ReadOnly = true;
            this.NOMEBANCO.Width = 200;
            // 
            // nomeconta
            // 
            this.nomeconta.DataPropertyName = "nomeconta";
            this.nomeconta.HeaderText = "Nome Conta Corrente";
            this.nomeconta.Name = "nomeconta";
            this.nomeconta.ReadOnly = true;
            this.nomeconta.Width = 200;
            // 
            // nomecontacorr
            // 
            this.nomecontacorr.DataPropertyName = "nomecontacorr";
            this.nomecontacorr.HeaderText = "Nº Conta";
            this.nomecontacorr.Name = "nomecontacorr";
            this.nomecontacorr.ReadOnly = true;
            // 
            // AGENCIA
            // 
            this.AGENCIA.DataPropertyName = "AGENCIA";
            this.AGENCIA.HeaderText = "Agência";
            this.AGENCIA.Name = "AGENCIA";
            this.AGENCIA.ReadOnly = true;
            // 
            // IDMOVCTCORRENTE
            // 
            this.IDMOVCTCORRENTE.DataPropertyName = "IDMOVCTCORRENTE";
            this.IDMOVCTCORRENTE.HeaderText = "Código";
            this.IDMOVCTCORRENTE.Name = "IDMOVCTCORRENTE";
            this.IDMOVCTCORRENTE.ReadOnly = true;
            this.IDMOVCTCORRENTE.Width = 70;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(96, 155);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 275;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(15, 155);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 273;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(99, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 268;
            this.label8.Text = "*";
            // 
            // cbContaCorrente
            // 
            this.cbContaCorrente.FormattingEnabled = true;
            this.cbContaCorrente.Location = new System.Drawing.Point(15, 25);
            this.cbContaCorrente.Name = "cbContaCorrente";
            this.cbContaCorrente.Size = new System.Drawing.Size(332, 21);
            this.cbContaCorrente.TabIndex = 266;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 267;
            this.label2.Text = "Conta Bancária:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmExtratoContaBancaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 186);
            this.Controls.Add(this.panel1);
            this.Name = "FrmExtratoContaBancaria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrato de Conta Bancária";
            this.Load += new System.EventHandler(this.FrmExtratoContaBancaria_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbContaCorrente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mkdatafinal;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mkDtInicial;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMMOVIMENTACAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMETIPOMOVCAIXA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEMOVIMENTACAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAMOVIMENTACAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEBANCO;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeconta;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomecontacorr;
        private System.Windows.Forms.DataGridViewTextBoxColumn AGENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDMOVCTCORRENTE;
        private System.Windows.Forms.CheckBox chkSaldoGeral;
    }
}