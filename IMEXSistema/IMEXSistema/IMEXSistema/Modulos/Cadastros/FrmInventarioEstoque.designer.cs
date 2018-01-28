namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmInventarioEstoque
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventarioEstoque));
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPRODUTOFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEstoqueMaiorqueZero = new System.Windows.Forms.RadioButton();
            this.rbEstoqueZerado = new System.Windows.Forms.RadioButton();
            this.rbEstoqueNegativo = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnPrint2 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbGrupoCategoria = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MkDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCustoFinal = new System.Windows.Forms.RadioButton();
            this.rbCustoInicial = new System.Windows.Forms.RadioButton();
            this.chkFiscal = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbOrdemNomeProduto = new System.Windows.Forms.RadioButton();
            this.rbOrdemCodigoReferencia = new System.Windows.Forms.RadioButton();
            this.rbOrdemCodigo = new System.Windows.Forms.RadioButton();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.MkDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDPRODUTO,
            this.CODPRODUTOFORNECEDOR,
            this.NOMEPRODUTO,
            this.Column1,
            this.ESTOQUE,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 139);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.Size = new System.Drawing.Size(1158, 358);
            this.DataGriewDados.TabIndex = 41;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle8;
            this.IDPRODUTO.HeaderText = "Código";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.Width = 50;
            // 
            // CODPRODUTOFORNECEDOR
            // 
            this.CODPRODUTOFORNECEDOR.DataPropertyName = "CODPRODUTOFORNECEDOR";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.CODPRODUTOFORNECEDOR.DefaultCellStyle = dataGridViewCellStyle9;
            this.CODPRODUTOFORNECEDOR.HeaderText = "Cód. Referência";
            this.CODPRODUTOFORNECEDOR.Name = "CODPRODUTOFORNECEDOR";
            this.CODPRODUTOFORNECEDOR.Width = 110;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome do Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.Width = 350;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Und";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // ESTOQUE
            // 
            this.ESTOQUE.DataPropertyName = "ESTOQUE";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ESTOQUE.DefaultCellStyle = dataGridViewCellStyle10;
            this.ESTOQUE.HeaderText = "Estoque";
            this.ESTOQUE.Name = "ESTOQUE";
            this.ESTOQUE.Width = 80;
            // 
            // Column2
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column2.HeaderText = "Vl. Unitário";
            this.Column2.Name = "Column2";
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column3.HeaderText = "Vl. Total";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = null;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column4.HeaderText = "Vl. Venda1";
            this.Column4.Name = "Column4";
            this.Column4.Width = 90;
            // 
            // Column5
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column5.HeaderText = "Vl. Total";
            this.Column5.Name = "Column5";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbEstoqueMaiorqueZero);
            this.groupBox1.Controls.Add(this.rbEstoqueZerado);
            this.groupBox1.Controls.Add(this.rbEstoqueNegativo);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 50);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalhes";
            // 
            // rbEstoqueMaiorqueZero
            // 
            this.rbEstoqueMaiorqueZero.AutoSize = true;
            this.rbEstoqueMaiorqueZero.Checked = true;
            this.rbEstoqueMaiorqueZero.Location = new System.Drawing.Point(338, 20);
            this.rbEstoqueMaiorqueZero.Name = "rbEstoqueMaiorqueZero";
            this.rbEstoqueMaiorqueZero.Size = new System.Drawing.Size(139, 17);
            this.rbEstoqueMaiorqueZero.TabIndex = 3;
            this.rbEstoqueMaiorqueZero.TabStop = true;
            this.rbEstoqueMaiorqueZero.Text = "Estoque Maior que Zero";
            this.rbEstoqueMaiorqueZero.UseVisualStyleBackColor = true;
            // 
            // rbEstoqueZerado
            // 
            this.rbEstoqueZerado.AutoSize = true;
            this.rbEstoqueZerado.Location = new System.Drawing.Point(219, 20);
            this.rbEstoqueZerado.Name = "rbEstoqueZerado";
            this.rbEstoqueZerado.Size = new System.Drawing.Size(101, 17);
            this.rbEstoqueZerado.TabIndex = 2;
            this.rbEstoqueZerado.Text = "Estoque Zerado";
            this.rbEstoqueZerado.UseVisualStyleBackColor = true;
            // 
            // rbEstoqueNegativo
            // 
            this.rbEstoqueNegativo.AutoSize = true;
            this.rbEstoqueNegativo.Location = new System.Drawing.Point(91, 20);
            this.rbEstoqueNegativo.Name = "rbEstoqueNegativo";
            this.rbEstoqueNegativo.Size = new System.Drawing.Size(110, 17);
            this.rbEstoqueNegativo.TabIndex = 1;
            this.rbEstoqueNegativo.Text = "Estoque Negativo";
            this.rbEstoqueNegativo.UseVisualStyleBackColor = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(18, 20);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(13, 110);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 43;
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnPrint2
            // 
            this.btnPrint2.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint2.Image")));
            this.btnPrint2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint2.Location = new System.Drawing.Point(94, 110);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.Size = new System.Drawing.Size(75, 23);
            this.btnPrint2.TabIndex = 44;
            this.btnPrint2.Text = "&Imprimir";
            this.btnPrint2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint2.UseVisualStyleBackColor = true;
            this.btnPrint2.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(175, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "&Sair";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbGrupoCategoria
            // 
            this.cbGrupoCategoria.FormattingEnabled = true;
            this.cbGrupoCategoria.Location = new System.Drawing.Point(15, 25);
            this.cbGrupoCategoria.Name = "cbGrupoCategoria";
            this.cbGrupoCategoria.Size = new System.Drawing.Size(312, 21);
            this.cbGrupoCategoria.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Grupo/Categoria:";
            // 
            // cbMarca
            // 
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(334, 24);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(243, 21);
            this.cbMarca.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Marca:";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(110, 507);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 51;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(12, 507);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 13);
            this.label45.TabIndex = 50;
            this.label45.Text = "Total da pesquisa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Data Inicial:";
            // 
            // MkDataInicial
            // 
            this.MkDataInicial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MkDataInicial.Location = new System.Drawing.Point(598, 24);
            this.MkDataInicial.Mask = "00/00/0000";
            this.MkDataInicial.Name = "MkDataInicial";
            this.MkDataInicial.Size = new System.Drawing.Size(75, 20);
            this.MkDataInicial.TabIndex = 53;
            this.MkDataInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MkDataInicial.ValidatingType = typeof(System.DateTime);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCustoFinal);
            this.groupBox2.Controls.Add(this.rbCustoInicial);
            this.groupBox2.Location = new System.Drawing.Point(518, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 50);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Preço";
            // 
            // rbCustoFinal
            // 
            this.rbCustoFinal.AutoSize = true;
            this.rbCustoFinal.Checked = true;
            this.rbCustoFinal.Location = new System.Drawing.Point(130, 19);
            this.rbCustoFinal.Name = "rbCustoFinal";
            this.rbCustoFinal.Size = new System.Drawing.Size(101, 17);
            this.rbCustoFinal.TabIndex = 1;
            this.rbCustoFinal.TabStop = true;
            this.rbCustoFinal.Text = "Vl. Unitário Final";
            this.rbCustoFinal.UseVisualStyleBackColor = true;
            // 
            // rbCustoInicial
            // 
            this.rbCustoInicial.AutoSize = true;
            this.rbCustoInicial.Location = new System.Drawing.Point(18, 19);
            this.rbCustoInicial.Name = "rbCustoInicial";
            this.rbCustoInicial.Size = new System.Drawing.Size(106, 17);
            this.rbCustoInicial.TabIndex = 0;
            this.rbCustoInicial.Text = "Vl. Unitário Inicial";
            this.rbCustoInicial.UseVisualStyleBackColor = true;
            // 
            // chkFiscal
            // 
            this.chkFiscal.AutoSize = true;
            this.chkFiscal.Checked = true;
            this.chkFiscal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFiscal.Location = new System.Drawing.Point(518, 109);
            this.chkFiscal.Name = "chkFiscal";
            this.chkFiscal.Size = new System.Drawing.Size(53, 17);
            this.chkFiscal.TabIndex = 55;
            this.chkFiscal.Text = "Fiscal";
            this.chkFiscal.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbOrdemNomeProduto);
            this.groupBox3.Controls.Add(this.rbOrdemCodigoReferencia);
            this.groupBox3.Controls.Add(this.rbOrdemCodigo);
            this.groupBox3.Location = new System.Drawing.Point(780, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 50);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ordenar";
            // 
            // rbOrdemNomeProduto
            // 
            this.rbOrdemNomeProduto.AutoSize = true;
            this.rbOrdemNomeProduto.Location = new System.Drawing.Point(190, 17);
            this.rbOrdemNomeProduto.Name = "rbOrdemNomeProduto";
            this.rbOrdemNomeProduto.Size = new System.Drawing.Size(93, 17);
            this.rbOrdemNomeProduto.TabIndex = 2;
            this.rbOrdemNomeProduto.Text = "Nome Produto";
            this.rbOrdemNomeProduto.UseVisualStyleBackColor = true;
            // 
            // rbOrdemCodigoReferencia
            // 
            this.rbOrdemCodigoReferencia.AutoSize = true;
            this.rbOrdemCodigoReferencia.Location = new System.Drawing.Point(82, 17);
            this.rbOrdemCodigoReferencia.Name = "rbOrdemCodigoReferencia";
            this.rbOrdemCodigoReferencia.Size = new System.Drawing.Size(102, 17);
            this.rbOrdemCodigoReferencia.TabIndex = 1;
            this.rbOrdemCodigoReferencia.Text = "Cód. Referência";
            this.rbOrdemCodigoReferencia.UseVisualStyleBackColor = true;
            // 
            // rbOrdemCodigo
            // 
            this.rbOrdemCodigo.AutoSize = true;
            this.rbOrdemCodigo.Checked = true;
            this.rbOrdemCodigo.Location = new System.Drawing.Point(18, 17);
            this.rbOrdemCodigo.Name = "rbOrdemCodigo";
            this.rbOrdemCodigo.Size = new System.Drawing.Size(58, 17);
            this.rbOrdemCodigo.TabIndex = 0;
            this.rbOrdemCodigo.TabStop = true;
            this.rbOrdemCodigo.Text = "Código";
            this.rbOrdemCodigo.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(1083, 110);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 302;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(1114, 110);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 301;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(1145, 110);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 300;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button4_Click);
            // 
            // MkDataFinal
            // 
            this.MkDataFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MkDataFinal.Location = new System.Drawing.Point(679, 24);
            this.MkDataFinal.Mask = "00/00/0000";
            this.MkDataFinal.Name = "MkDataFinal";
            this.MkDataFinal.Size = new System.Drawing.Size(75, 20);
            this.MkDataFinal.TabIndex = 304;
            this.MkDataFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MkDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(676, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 303;
            this.label2.Text = "Data Final";
            // 
            // FrmInventarioEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 529);
            this.Controls.Add(this.MkDataFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkFiscal);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.MkDataInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbGrupoCategoria);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint2);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGriewDados);
            this.Name = "FrmInventarioEstoque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventário do Estoque";
            this.Load += new System.EventHandler(this.FrmRelacaoEstoqueAtual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEstoqueMaiorqueZero;
        private System.Windows.Forms.RadioButton rbEstoqueZerado;
        private System.Windows.Forms.RadioButton rbEstoqueNegativo;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnPrint2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbGrupoCategoria;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MkDataInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPRODUTOFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCustoFinal;
        private System.Windows.Forms.RadioButton rbCustoInicial;
        private System.Windows.Forms.CheckBox chkFiscal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbOrdemNomeProduto;
        private System.Windows.Forms.RadioButton rbOrdemCodigoReferencia;
        private System.Windows.Forms.RadioButton rbOrdemCodigo;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.MaskedTextBox MkDataFinal;
        private System.Windows.Forms.Label label2;
    }
}