namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmProdutoClientePedidoSimples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProdutoClientePedidoSimples));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.mkdDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.mkdDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bntDateSelec = new System.Windows.Forms.Button();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGDadosServicos = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbGrupoCategoria = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDtVecto = new System.Windows.Forms.RadioButton();
            this.rbDataEmissao = new System.Windows.Forms.RadioButton();
            this.rbOrdenarProduto = new System.Windows.Forms.RadioButton();
            this.rbOrdenarPedido = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lbltTotalMtLinear = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdTodos = new System.Windows.Forms.RadioButton();
            this.rdVenda = new System.Windows.Forms.RadioButton();
            this.rdOrcamento = new System.Windows.Forms.RadioButton();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSupervisor = new System.Windows.Forms.ComboBox();
            this.IDPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVECTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORUNITARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORTOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESUPERVISOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosServicos)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(15, 25);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(396, 21);
            this.cbProduto.TabIndex = 185;
            this.cbProduto.Enter += new System.EventHandler(this.cbServico_Enter);
            this.cbProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbServico_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 186;
            this.label9.Text = "Produtos:";
            // 
            // mkdDataInicial
            // 
            this.mkdDataInicial.Location = new System.Drawing.Point(19, 35);
            this.mkdDataInicial.Mask = "00/00/0000";
            this.mkdDataInicial.Name = "mkdDataInicial";
            this.mkdDataInicial.Size = new System.Drawing.Size(79, 20);
            this.mkdDataInicial.TabIndex = 237;
            this.mkdDataInicial.ValidatingType = typeof(System.DateTime);
            this.mkdDataInicial.Validating += new System.ComponentModel.CancelEventHandler(this.mkdDataInicial_Validating);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 20);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(37, 13);
            this.label42.TabIndex = 238;
            this.label42.Text = "Inicial:";
            // 
            // mkdDataFinal
            // 
            this.mkdDataFinal.Location = new System.Drawing.Point(138, 36);
            this.mkdDataFinal.Mask = "00/00/0000";
            this.mkdDataFinal.Name = "mkdDataFinal";
            this.mkdDataFinal.Size = new System.Drawing.Size(79, 20);
            this.mkdDataFinal.TabIndex = 239;
            this.mkdDataFinal.ValidatingType = typeof(System.DateTime);
            this.mkdDataFinal.Validating += new System.ComponentModel.CancelEventHandler(this.mkdDataFinal_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 240;
            this.label1.Text = "Final:";
            // 
            // bntDateSelec
            // 
            this.bntDateSelec.FlatAppearance.BorderSize = 0;
            this.bntDateSelec.Location = new System.Drawing.Point(104, 34);
            this.bntDateSelec.Name = "bntDateSelec";
            this.bntDateSelec.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelec.TabIndex = 241;
            this.bntDateSelec.UseVisualStyleBackColor = true;
            this.bntDateSelec.Click += new System.EventHandler(this.bntDateSelec_Click);
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(223, 34);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 242;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.mkdDataInicial);
            this.groupBox1.Controls.Add(this.bntDateSelecFinal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bntDateSelec);
            this.groupBox1.Controls.Add(this.mkdDataFinal);
            this.groupBox1.Location = new System.Drawing.Point(428, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 77);
            this.groupBox1.TabIndex = 244;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Emissão";
            // 
            // DGDadosServicos
            // 
            this.DGDadosServicos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDadosServicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDadosServicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDPEDIDO,
            this.DTEMISSAO,
            this.DATAVECTO,
            this.NOMEPRODUTO,
            this.quantidade,
            this.VALORUNITARIO,
            this.VALORTOTAL,
            this.NOMECLIENTE,
            this.NOMESTATUS,
            this.NOMESUPERVISOR});
            this.DGDadosServicos.Location = new System.Drawing.Point(6, 56);
            this.DGDadosServicos.Name = "DGDadosServicos";
            this.DGDadosServicos.ReadOnly = true;
            this.DGDadosServicos.Size = new System.Drawing.Size(1004, 285);
            this.DGDadosServicos.TabIndex = 245;
            this.DGDadosServicos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosServicos_CellDoubleClick);
            this.DGDadosServicos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosServicos_CellEnter);
            this.DGDadosServicos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGDadosServicos_CellFormatting);
            this.DGDadosServicos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGDadosServicos_ColumnHeaderMouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 535);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1051, 24);
            this.statusStrip1.TabIndex = 246;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1036, 19);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Toggle Key Test";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(9, 26);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 247;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(90, 26);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 248;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(171, 26);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 249;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbGrupoCategoria
            // 
            this.cbGrupoCategoria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGrupoCategoria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupoCategoria.FormattingEnabled = true;
            this.cbGrupoCategoria.Location = new System.Drawing.Point(15, 67);
            this.cbGrupoCategoria.Name = "cbGrupoCategoria";
            this.cbGrupoCategoria.Size = new System.Drawing.Size(197, 21);
            this.cbGrupoCategoria.TabIndex = 252;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 253;
            this.label10.Text = "Grupo/Categoria:";
            // 
            // cbMarca
            // 
            this.cbMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(15, 106);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(197, 21);
            this.cbMarca.TabIndex = 254;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 255;
            this.label8.Text = "Marca:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(15, 133);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 399);
            this.tabControl1.TabIndex = 256;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnpdf);
            this.tabPage1.Controls.Add(this.btnExcel);
            this.tabPage1.Controls.Add(this.btnPrint);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lbltTotalMtLinear);
            this.tabPage1.Controls.Add(this.DGDadosServicos);
            this.tabPage1.Controls.Add(this.btnImprimir);
            this.tabPage1.Controls.Add(this.btnPesquisa);
            this.tabPage1.Controls.Add(this.btnSair);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1016, 373);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Produtos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(927, 27);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 302;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(956, 27);
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
            this.btnPrint.Location = new System.Drawing.Point(985, 27);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 300;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbDtVecto);
            this.groupBox2.Controls.Add(this.rbDataEmissao);
            this.groupBox2.Controls.Add(this.rbOrdenarProduto);
            this.groupBox2.Controls.Add(this.rbOrdenarPedido);
            this.groupBox2.Location = new System.Drawing.Point(443, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 44);
            this.groupBox2.TabIndex = 258;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordenar";
            // 
            // rbDtVecto
            // 
            this.rbDtVecto.AutoSize = true;
            this.rbDtVecto.Location = new System.Drawing.Point(259, 16);
            this.rbDtVecto.Name = "rbDtVecto";
            this.rbDtVecto.Size = new System.Drawing.Size(76, 17);
            this.rbDtVecto.TabIndex = 3;
            this.rbDtVecto.Text = "Dt. Vencto";
            this.rbDtVecto.UseVisualStyleBackColor = true;
            // 
            // rbDataEmissao
            // 
            this.rbDataEmissao.AutoSize = true;
            this.rbDataEmissao.Location = new System.Drawing.Point(163, 16);
            this.rbDataEmissao.Name = "rbDataEmissao";
            this.rbDataEmissao.Size = new System.Drawing.Size(81, 17);
            this.rbDataEmissao.TabIndex = 2;
            this.rbDataEmissao.Text = "Dt. Emissão";
            this.rbDataEmissao.UseVisualStyleBackColor = true;
            // 
            // rbOrdenarProduto
            // 
            this.rbOrdenarProduto.AutoSize = true;
            this.rbOrdenarProduto.Location = new System.Drawing.Point(86, 16);
            this.rbOrdenarProduto.Name = "rbOrdenarProduto";
            this.rbOrdenarProduto.Size = new System.Drawing.Size(62, 17);
            this.rbOrdenarProduto.TabIndex = 1;
            this.rbOrdenarProduto.Text = "Produto";
            this.rbOrdenarProduto.UseVisualStyleBackColor = true;
            // 
            // rbOrdenarPedido
            // 
            this.rbOrdenarPedido.AutoSize = true;
            this.rbOrdenarPedido.Checked = true;
            this.rbOrdenarPedido.Location = new System.Drawing.Point(13, 16);
            this.rbOrdenarPedido.Name = "rbOrdenarPedido";
            this.rbOrdenarPedido.Size = new System.Drawing.Size(58, 17);
            this.rbOrdenarPedido.TabIndex = 0;
            this.rbOrdenarPedido.TabStop = true;
            this.rbOrdenarPedido.Text = "Pedido";
            this.rbOrdenarPedido.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 252;
            this.label2.Text = "Total da pesquisa:";
            // 
            // lbltTotalMtLinear
            // 
            this.lbltTotalMtLinear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbltTotalMtLinear.AutoSize = true;
            this.lbltTotalMtLinear.Location = new System.Drawing.Point(106, 348);
            this.lbltTotalMtLinear.Name = "lbltTotalMtLinear";
            this.lbltTotalMtLinear.Size = new System.Drawing.Size(13, 13);
            this.lbltTotalMtLinear.TabIndex = 253;
            this.lbltTotalMtLinear.Text = "0";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rdTodos);
            this.groupBox5.Controls.Add(this.rdVenda);
            this.groupBox5.Controls.Add(this.rdOrcamento);
            this.groupBox5.Location = new System.Drawing.Point(794, 45);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 44);
            this.groupBox5.TabIndex = 257;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo";
            // 
            // rdTodos
            // 
            this.rdTodos.AutoSize = true;
            this.rdTodos.Checked = true;
            this.rdTodos.Location = new System.Drawing.Point(158, 19);
            this.rdTodos.Name = "rdTodos";
            this.rdTodos.Size = new System.Drawing.Size(55, 17);
            this.rdTodos.TabIndex = 2;
            this.rdTodos.TabStop = true;
            this.rdTodos.Text = "Todos";
            this.rdTodos.UseVisualStyleBackColor = true;
            // 
            // rdVenda
            // 
            this.rdVenda.AutoSize = true;
            this.rdVenda.Location = new System.Drawing.Point(96, 19);
            this.rdVenda.Name = "rdVenda";
            this.rdVenda.Size = new System.Drawing.Size(56, 17);
            this.rdVenda.TabIndex = 1;
            this.rdVenda.Text = "Venda";
            this.rdVenda.UseVisualStyleBackColor = true;
            // 
            // rdOrcamento
            // 
            this.rdOrcamento.AutoSize = true;
            this.rdOrcamento.Location = new System.Drawing.Point(13, 19);
            this.rdOrcamento.Name = "rdOrcamento";
            this.rdOrcamento.Size = new System.Drawing.Size(77, 17);
            this.rdOrcamento.TabIndex = 0;
            this.rdOrcamento.Text = "Orçamento";
            this.rdOrcamento.UseVisualStyleBackColor = true;
            // 
            // cbCliente
            // 
            this.cbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(218, 106);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(308, 21);
            this.cbCliente.TabIndex = 259;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 258;
            this.label3.Text = "Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(529, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 261;
            this.label4.Text = "Vendedor:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFuncionario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(532, 106);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(238, 21);
            this.cbFuncionario.TabIndex = 260;
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(218, 67);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(193, 21);
            this.cbStatus.TabIndex = 262;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 263;
            this.label5.Text = "Status/Situação:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(777, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 265;
            this.label6.Text = "Supervisor:";
            // 
            // cbSupervisor
            // 
            this.cbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSupervisor.FormattingEnabled = true;
            this.cbSupervisor.Location = new System.Drawing.Point(780, 106);
            this.cbSupervisor.Name = "cbSupervisor";
            this.cbSupervisor.Size = new System.Drawing.Size(238, 21);
            this.cbSupervisor.TabIndex = 264;
            // 
            // IDPEDIDO
            // 
            this.IDPEDIDO.DataPropertyName = "IDPEDIDO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "000000";
            this.IDPEDIDO.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDPEDIDO.HeaderText = "Pedido";
            this.IDPEDIDO.Name = "IDPEDIDO";
            this.IDPEDIDO.ReadOnly = true;
            // 
            // DTEMISSAO
            // 
            this.DTEMISSAO.DataPropertyName = "dtemissao";
            this.DTEMISSAO.HeaderText = "Data Emissão";
            this.DTEMISSAO.Name = "DTEMISSAO";
            this.DTEMISSAO.ReadOnly = true;
            // 
            // DATAVECTO
            // 
            this.DATAVECTO.DataPropertyName = "DATAVECTO";
            this.DATAVECTO.HeaderText = "Data Vecto";
            this.DATAVECTO.Name = "DATAVECTO";
            this.DATAVECTO.ReadOnly = true;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 300;
            // 
            // quantidade
            // 
            this.quantidade.DataPropertyName = "quantidade";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.quantidade.DefaultCellStyle = dataGridViewCellStyle2;
            this.quantidade.HeaderText = "Quant.";
            this.quantidade.Name = "quantidade";
            this.quantidade.ReadOnly = true;
            this.quantidade.Width = 80;
            // 
            // VALORUNITARIO
            // 
            this.VALORUNITARIO.DataPropertyName = "VALORUNITARIO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.VALORUNITARIO.DefaultCellStyle = dataGridViewCellStyle3;
            this.VALORUNITARIO.HeaderText = "Vl. Unitário";
            this.VALORUNITARIO.Name = "VALORUNITARIO";
            this.VALORUNITARIO.ReadOnly = true;
            // 
            // VALORTOTAL
            // 
            this.VALORTOTAL.DataPropertyName = "VALORTOTAL";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.VALORTOTAL.DefaultCellStyle = dataGridViewCellStyle4;
            this.VALORTOTAL.HeaderText = "Valor Total";
            this.VALORTOTAL.Name = "VALORTOTAL";
            this.VALORTOTAL.ReadOnly = true;
            this.VALORTOTAL.Width = 90;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 300;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status/Situação";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            // 
            // NOMESUPERVISOR
            // 
            this.NOMESUPERVISOR.DataPropertyName = "NOMESUPERVISOR";
            this.NOMESUPERVISOR.HeaderText = "Supervisor";
            this.NOMESUPERVISOR.Name = "NOMESUPERVISOR";
            this.NOMESUPERVISOR.ReadOnly = true;
            this.NOMESUPERVISOR.Width = 200;
            // 
            // FrmProdutoClientePedidoSimples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 559);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSupervisor);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFuncionario);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbGrupoCategoria);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label9);
            this.Name = "FrmProdutoClientePedidoSimples";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Produtos por Cliente - Pedido Simples";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmServicoCliente_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosServicos)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox mkdDataInicial;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.MaskedTextBox mkdDataFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bntDateSelec;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGDadosServicos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbGrupoCategoria;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbltTotalMtLinear;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdTodos;
        private System.Windows.Forms.RadioButton rdVenda;
        private System.Windows.Forms.RadioButton rdOrcamento;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOrdenarProduto;
        private System.Windows.Forms.RadioButton rbOrdenarPedido;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbDtVecto;
        private System.Windows.Forms.RadioButton rbDataEmissao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSupervisor;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVECTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORUNITARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORTOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESUPERVISOR;
    }
}