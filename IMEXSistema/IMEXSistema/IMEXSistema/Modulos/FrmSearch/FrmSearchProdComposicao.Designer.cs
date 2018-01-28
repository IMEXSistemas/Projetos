namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchProdComposicao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchProdComposicao));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridDadosComposicao = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.chkBoxAcumulaPesquisa = new System.Windows.Forms.CheckBox();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.cbTipoPesquisa = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.cbCamposPesquisa = new System.Windows.Forms.ComboBox();
            this.btnCadProduto = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPRODUTOFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUEATUAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEUNIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtCriterioPesquisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LARGURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGMATERIAPRIMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDadosComposicao)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dataGridDadosComposicao);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chkBoxAcumulaPesquisa);
            this.panel1.Controls.Add(this.lblTotalPesquisa);
            this.panel1.Controls.Add(this.label45);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.cbTipoPesquisa);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.cbCamposPesquisa);
            this.panel1.Controls.Add(this.btnCadProduto);
            this.panel1.Controls.Add(this.DataGriewDados);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.txtCriterioPesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 486);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 13);
            this.label2.TabIndex = 112;
            this.label2.Text = "Composição dos Produtos";
            // 
            // dataGridDadosComposicao
            // 
            this.dataGridDadosComposicao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDadosComposicao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column7,
            this.ALTURA,
            this.LARGURA,
            this.FLAGMATERIAPRIMA});
            this.dataGridDadosComposicao.Location = new System.Drawing.Point(14, 317);
            this.dataGridDadosComposicao.Name = "dataGridDadosComposicao";
            this.dataGridDadosComposicao.ReadOnly = true;
            this.dataGridDadosComposicao.Size = new System.Drawing.Size(970, 157);
            this.dataGridDadosComposicao.TabIndex = 111;
            this.dataGridDadosComposicao.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDadosComposicao_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb3);
            this.groupBox1.Controls.Add(this.rb2);
            this.groupBox1.Controls.Add(this.rb1);
            this.groupBox1.Location = new System.Drawing.Point(753, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 48);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preço Venda";
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(145, 24);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(31, 17);
            this.rb3.TabIndex = 2;
            this.rb3.Text = "3";
            this.rb3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(79, 24);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(31, 17);
            this.rb2.TabIndex = 1;
            this.rb2.Text = "2";
            this.rb2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(21, 24);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(31, 17);
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "1";
            this.rb1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // chkBoxAcumulaPesquisa
            // 
            this.chkBoxAcumulaPesquisa.AutoSize = true;
            this.chkBoxAcumulaPesquisa.Location = new System.Drawing.Point(15, 12);
            this.chkBoxAcumulaPesquisa.Name = "chkBoxAcumulaPesquisa";
            this.chkBoxAcumulaPesquisa.Size = new System.Drawing.Size(116, 17);
            this.chkBoxAcumulaPesquisa.TabIndex = 103;
            this.chkBoxAcumulaPesquisa.Text = "Acumular Pesquisa";
            this.chkBoxAcumulaPesquisa.UseVisualStyleBackColor = true;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(110, 272);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 102;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(12, 272);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 13);
            this.label45.TabIndex = 101;
            this.label45.Text = "Total da pesquisa:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(187, 32);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(92, 13);
            this.label42.TabIndex = 100;
            this.label42.Text = "Tipo de Pesquisa:";
            // 
            // cbTipoPesquisa
            // 
            this.cbTipoPesquisa.FormattingEnabled = true;
            this.cbTipoPesquisa.Location = new System.Drawing.Point(190, 48);
            this.cbTipoPesquisa.Name = "cbTipoPesquisa";
            this.cbTipoPesquisa.Size = new System.Drawing.Size(121, 21);
            this.cbTipoPesquisa.TabIndex = 99;
            this.cbTipoPesquisa.Text = "Campo de Pesquisa";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(12, 32);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(109, 13);
            this.label43.TabIndex = 98;
            this.label43.Text = "Campos de Pesquisa:";
            // 
            // cbCamposPesquisa
            // 
            this.cbCamposPesquisa.FormattingEnabled = true;
            this.cbCamposPesquisa.Location = new System.Drawing.Point(14, 48);
            this.cbCamposPesquisa.Name = "cbCamposPesquisa";
            this.cbCamposPesquisa.Size = new System.Drawing.Size(170, 21);
            this.cbCamposPesquisa.TabIndex = 97;
            this.cbCamposPesquisa.Text = "Campo de Pesquisa";
            // 
            // btnCadProduto
            // 
            this.btnCadProduto.FlatAppearance.BorderSize = 0;
            this.btnCadProduto.Location = new System.Drawing.Point(559, 48);
            this.btnCadProduto.Name = "btnCadProduto";
            this.btnCadProduto.Size = new System.Drawing.Size(26, 21);
            this.btnCadProduto.TabIndex = 96;
            this.btnCadProduto.UseVisualStyleBackColor = true;
            this.btnCadProduto.Click += new System.EventHandler(this.btnCadProduto_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.CODPRODUTOFORNECEDOR,
            this.ESTOQUEATUAL,
            this.NOMEUNIDADE,
            this.VALORVENDA1,
            this.VALORVENDA2,
            this.VALORVENDA3});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 87);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(972, 182);
            this.DataGriewDados.TabIndex = 43;
            this.DataGriewDados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellContentClick);
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewDados.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGriewDados_CellMouseClick);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewSearch_KeyDown);
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 300;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle5;
            this.IDPRODUTO.HeaderText = "Código";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.ReadOnly = true;
            this.IDPRODUTO.Width = 50;
            // 
            // CODPRODUTOFORNECEDOR
            // 
            this.CODPRODUTOFORNECEDOR.DataPropertyName = "CODPRODUTOFORNECEDOR";
            this.CODPRODUTOFORNECEDOR.HeaderText = "Cód. Referência";
            this.CODPRODUTOFORNECEDOR.Name = "CODPRODUTOFORNECEDOR";
            this.CODPRODUTOFORNECEDOR.ReadOnly = true;
            // 
            // ESTOQUEATUAL
            // 
            this.ESTOQUEATUAL.DataPropertyName = "ESTOQUEATUAL";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.ESTOQUEATUAL.DefaultCellStyle = dataGridViewCellStyle6;
            this.ESTOQUEATUAL.HeaderText = "Estoque";
            this.ESTOQUEATUAL.Name = "ESTOQUEATUAL";
            this.ESTOQUEATUAL.ReadOnly = true;
            // 
            // NOMEUNIDADE
            // 
            this.NOMEUNIDADE.DataPropertyName = "NOMEUNIDADE";
            this.NOMEUNIDADE.HeaderText = "Und";
            this.NOMEUNIDADE.Name = "NOMEUNIDADE";
            this.NOMEUNIDADE.ReadOnly = true;
            this.NOMEUNIDADE.Width = 70;
            // 
            // VALORVENDA1
            // 
            this.VALORVENDA1.DataPropertyName = "VALORVENDA1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.VALORVENDA1.DefaultCellStyle = dataGridViewCellStyle7;
            this.VALORVENDA1.HeaderText = "Valor Venda 1";
            this.VALORVENDA1.Name = "VALORVENDA1";
            this.VALORVENDA1.ReadOnly = true;
            // 
            // VALORVENDA2
            // 
            this.VALORVENDA2.DataPropertyName = "VALORVENDA2";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.VALORVENDA2.DefaultCellStyle = dataGridViewCellStyle8;
            this.VALORVENDA2.HeaderText = "Valor Venda 2";
            this.VALORVENDA2.Name = "VALORVENDA2";
            this.VALORVENDA2.ReadOnly = true;
            // 
            // VALORVENDA3
            // 
            this.VALORVENDA3.DataPropertyName = "VALORVENDA3";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.VALORVENDA3.DefaultCellStyle = dataGridViewCellStyle9;
            this.VALORVENDA3.HeaderText = "Valor Venda 3";
            this.VALORVENDA3.Name = "VALORVENDA3";
            this.VALORVENDA3.ReadOnly = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(672, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "&Sair";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(591, 46);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtCriterioPesquisa
            // 
            this.txtCriterioPesquisa.Location = new System.Drawing.Point(317, 49);
            this.txtCriterioPesquisa.Name = "txtCriterioPesquisa";
            this.txtCriterioPesquisa.Size = new System.Drawing.Size(236, 20);
            this.txtCriterioPesquisa.TabIndex = 0;
            this.txtCriterioPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pesquisa:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "IDPRODUTO";
            this.Column3.HeaderText = "Cod. Produto";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "NOMEPRODUTO";
            this.Column4.HeaderText = "Produto";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 500;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "QUANTIDADE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column7.HeaderText = "Quant.";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 50;
            // 
            // ALTURA
            // 
            this.ALTURA.DataPropertyName = "ALTURA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N4";
            this.ALTURA.DefaultCellStyle = dataGridViewCellStyle2;
            this.ALTURA.HeaderText = "Altura";
            this.ALTURA.Name = "ALTURA";
            this.ALTURA.ReadOnly = true;
            this.ALTURA.Width = 80;
            // 
            // LARGURA
            // 
            this.LARGURA.DataPropertyName = "LARGURA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N4";
            this.LARGURA.DefaultCellStyle = dataGridViewCellStyle3;
            this.LARGURA.HeaderText = "largura";
            this.LARGURA.Name = "LARGURA";
            this.LARGURA.ReadOnly = true;
            this.LARGURA.Width = 80;
            // 
            // FLAGMATERIAPRIMA
            // 
            this.FLAGMATERIAPRIMA.DataPropertyName = "FLAGMATERIAPRIMA";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAGMATERIAPRIMA.DefaultCellStyle = dataGridViewCellStyle4;
            this.FLAGMATERIAPRIMA.HeaderText = "Mat. Prima";
            this.FLAGMATERIAPRIMA.Name = "FLAGMATERIAPRIMA";
            this.FLAGMATERIAPRIMA.ReadOnly = true;
            this.FLAGMATERIAPRIMA.Width = 60;
            // 
            // FrmSearchProdComposicao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 486);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchProdComposicao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Produto/Composição";
            this.Load += new System.EventHandler(this.FrmSearchFornecedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearchFornecedor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDadosComposicao)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCriterioPesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Button btnCadProduto;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox cbCamposPesquisa;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cbTipoPesquisa;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkBoxAcumulaPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPRODUTOFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUEATUAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEUNIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA3;
        private System.Windows.Forms.DataGridView dataGridDadosComposicao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn LARGURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGMATERIAPRIMA;


    }
}