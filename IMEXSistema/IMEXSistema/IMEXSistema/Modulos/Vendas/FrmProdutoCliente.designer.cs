namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmProdutoCliente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProdutoCliente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.mkdDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.mkdDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bntDateSelec = new System.Windows.Forms.Button();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mkdHoraFinal = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mkdHoraInicial = new System.Windows.Forms.MaskedTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.DGDadosServicos = new System.Windows.Forms.DataGridView();
            this.DTEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORTOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cbGrupoCategoria = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkImpressaoTicket = new System.Windows.Forms.CheckBox();
            this.btnpdf2 = new System.Windows.Forms.Button();
            this.btnExcel2 = new System.Windows.Forms.Button();
            this.btnPrint2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbltTotalMtLinear = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPEDIDO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MT2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdTodos = new System.Windows.Forms.RadioButton();
            this.rdVenda = new System.Windows.Forms.RadioButton();
            this.rdOrcamento = new System.Windows.Forms.RadioButton();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosServicos)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.cbProduto.Size = new System.Drawing.Size(358, 21);
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
            this.groupBox1.Controls.Add(this.mkdHoraFinal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.mkdHoraInicial);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.mkdDataInicial);
            this.groupBox1.Controls.Add(this.bntDateSelecFinal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bntDateSelec);
            this.groupBox1.Controls.Add(this.mkdDataFinal);
            this.groupBox1.Location = new System.Drawing.Point(428, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 100);
            this.groupBox1.TabIndex = 244;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // mkdHoraFinal
            // 
            this.mkdHoraFinal.Location = new System.Drawing.Point(90, 74);
            this.mkdHoraFinal.Mask = "90:00";
            this.mkdHoraFinal.Name = "mkdHoraFinal";
            this.mkdHoraFinal.Size = new System.Drawing.Size(62, 20);
            this.mkdHoraFinal.TabIndex = 303;
            this.mkdHoraFinal.Text = "2359";
            this.mkdHoraFinal.Validating += new System.ComponentModel.CancelEventHandler(this.mkdHoraFinal_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(87, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 304;
            this.label5.Text = "Final:";
            // 
            // mkdHoraInicial
            // 
            this.mkdHoraInicial.Location = new System.Drawing.Point(19, 74);
            this.mkdHoraInicial.Mask = "90:00";
            this.mkdHoraInicial.Name = "mkdHoraInicial";
            this.mkdHoraInicial.Size = new System.Drawing.Size(62, 20);
            this.mkdHoraInicial.TabIndex = 301;
            this.mkdHoraInicial.Text = "0000";
            this.mkdHoraInicial.Validating += new System.ComponentModel.CancelEventHandler(this.mkdHoraInicial_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(16, 58);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 302;
            this.label24.Text = "Inicial:";
            // 
            // DGDadosServicos
            // 
            this.DGDadosServicos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDadosServicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDadosServicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DTEMISSAO,
            this.IDPEDIDO,
            this.NOMEPRODUTO,
            this.VALORTOTAL,
            this.quantidade,
            this.TOTALMT,
            this.NOMECLIENTE});
            this.DGDadosServicos.Location = new System.Drawing.Point(6, 48);
            this.DGDadosServicos.Name = "DGDadosServicos";
            this.DGDadosServicos.ReadOnly = true;
            this.DGDadosServicos.Size = new System.Drawing.Size(921, 313);
            this.DGDadosServicos.TabIndex = 245;
            this.DGDadosServicos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosServicos_CellDoubleClick);
            this.DGDadosServicos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosServicos_CellEnter);
            this.DGDadosServicos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGDadosServicos_ColumnHeaderMouseClick);
            // 
            // DTEMISSAO
            // 
            this.DTEMISSAO.DataPropertyName = "dtemissao";
            this.DTEMISSAO.HeaderText = "Data";
            this.DTEMISSAO.Name = "DTEMISSAO";
            this.DTEMISSAO.ReadOnly = true;
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
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 300;
            // 
            // VALORTOTAL
            // 
            this.VALORTOTAL.DataPropertyName = "VALORTOTAL";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.VALORTOTAL.DefaultCellStyle = dataGridViewCellStyle2;
            this.VALORTOTAL.HeaderText = "Valor";
            this.VALORTOTAL.Name = "VALORTOTAL";
            this.VALORTOTAL.ReadOnly = true;
            this.VALORTOTAL.Width = 90;
            // 
            // quantidade
            // 
            this.quantidade.DataPropertyName = "quantidade";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.quantidade.DefaultCellStyle = dataGridViewCellStyle3;
            this.quantidade.HeaderText = "Quant.";
            this.quantidade.Name = "quantidade";
            this.quantidade.ReadOnly = true;
            this.quantidade.Width = 80;
            // 
            // TOTALMT
            // 
            this.TOTALMT.DataPropertyName = "TOTALMT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.TOTALMT.DefaultCellStyle = dataGridViewCellStyle4;
            this.TOTALMT.HeaderText = "Total MT";
            this.TOTALMT.Name = "TOTALMT";
            this.TOTALMT.ReadOnly = true;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "NomeCliente";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 300;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 624);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(965, 24);
            this.statusStrip1.TabIndex = 246;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(950, 19);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Toggle Key Test";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(708, 144);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 247;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(870, 144);
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
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(106, 376);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 251;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 376);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 250;
            this.label33.Text = "Total da pesquisa:";
            // 
            // cbGrupoCategoria
            // 
            this.cbGrupoCategoria.FormattingEnabled = true;
            this.cbGrupoCategoria.Location = new System.Drawing.Point(15, 67);
            this.cbGrupoCategoria.Name = "cbGrupoCategoria";
            this.cbGrupoCategoria.Size = new System.Drawing.Size(358, 21);
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
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(15, 106);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(358, 21);
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
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 176);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(941, 428);
            this.tabControl1.TabIndex = 256;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkImpressaoTicket);
            this.tabPage1.Controls.Add(this.btnpdf2);
            this.tabPage1.Controls.Add(this.btnExcel2);
            this.tabPage1.Controls.Add(this.btnPrint2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lbltTotalMtLinear);
            this.tabPage1.Controls.Add(this.DGDadosServicos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(933, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Produto/Produto MT Linear";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkImpressaoTicket
            // 
            this.chkImpressaoTicket.AutoSize = true;
            this.chkImpressaoTicket.Checked = true;
            this.chkImpressaoTicket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImpressaoTicket.Location = new System.Drawing.Point(9, 25);
            this.chkImpressaoTicket.Name = "chkImpressaoTicket";
            this.chkImpressaoTicket.Size = new System.Drawing.Size(124, 17);
            this.chkImpressaoTicket.TabIndex = 297;
            this.chkImpressaoTicket.Text = "Impressão em Ticket";
            this.chkImpressaoTicket.UseVisualStyleBackColor = true;
            // 
            // btnpdf2
            // 
            this.btnpdf2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf2.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf2.Image")));
            this.btnpdf2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf2.Location = new System.Drawing.Point(842, 19);
            this.btnpdf2.Name = "btnpdf2";
            this.btnpdf2.Size = new System.Drawing.Size(25, 23);
            this.btnpdf2.TabIndex = 303;
            this.btnpdf2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf2.UseVisualStyleBackColor = true;
            this.btnpdf2.Click += new System.EventHandler(this.btnpdf2_Click);
            // 
            // btnExcel2
            // 
            this.btnExcel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel2.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel2.Image")));
            this.btnExcel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel2.Location = new System.Drawing.Point(873, 19);
            this.btnExcel2.Name = "btnExcel2";
            this.btnExcel2.Size = new System.Drawing.Size(25, 23);
            this.btnExcel2.TabIndex = 302;
            this.btnExcel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel2.UseVisualStyleBackColor = true;
            this.btnExcel2.Click += new System.EventHandler(this.btnExcel2_Click);
            // 
            // btnPrint2
            // 
            this.btnPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint2.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint2.Image")));
            this.btnPrint2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint2.Location = new System.Drawing.Point(902, 19);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.Size = new System.Drawing.Size(25, 23);
            this.btnPrint2.TabIndex = 301;
            this.btnPrint2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint2.UseVisualStyleBackColor = true;
            this.btnPrint2.Click += new System.EventHandler(this.btnPrint2_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 252;
            this.label2.Text = "Total da pesquisa:";
            // 
            // lbltTotalMtLinear
            // 
            this.lbltTotalMtLinear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbltTotalMtLinear.AutoSize = true;
            this.lbltTotalMtLinear.Location = new System.Drawing.Point(106, 376);
            this.lbltTotalMtLinear.Name = "lbltTotalMtLinear";
            this.lbltTotalMtLinear.Size = new System.Drawing.Size(13, 13);
            this.lbltTotalMtLinear.TabIndex = 253;
            this.lbltTotalMtLinear.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnpdf);
            this.tabPage2.Controls.Add(this.btnExcel);
            this.tabPage2.Controls.Add(this.btnPrint);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.lblTotalPesquisa);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(933, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Produto MT2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(842, 19);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 300;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(873, 19);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 299;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(902, 19);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 298;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.IDPEDIDO2,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.MT2,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView1.Location = new System.Drawing.Point(6, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(921, 313);
            this.dataGridView1.TabIndex = 249;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "dtemissao";
            this.dataGridViewTextBoxColumn1.HeaderText = "Data";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // IDPEDIDO2
            // 
            this.IDPEDIDO2.DataPropertyName = "IDPEDIDO";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "000000";
            this.IDPEDIDO2.DefaultCellStyle = dataGridViewCellStyle5;
            this.IDPEDIDO2.HeaderText = "Pedido";
            this.IDPEDIDO2.Name = "IDPEDIDO2";
            this.IDPEDIDO2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NOMEPRODUTO";
            this.dataGridViewTextBoxColumn2.HeaderText = "Produto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "VALORTOTAL";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn3.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "quantidade";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn4.HeaderText = "Quant.";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // MT2
            // 
            this.MT2.DataPropertyName = "MT2";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.MT2.DefaultCellStyle = dataGridViewCellStyle8;
            this.MT2.HeaderText = "Total MT2";
            this.MT2.Name = "MT2";
            this.MT2.ReadOnly = true;
            this.MT2.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "NOMECLIENTE";
            this.dataGridViewTextBoxColumn6.HeaderText = "NomeCliente";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 300;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rdTodos);
            this.groupBox5.Controls.Add(this.rdVenda);
            this.groupBox5.Controls.Add(this.rdOrcamento);
            this.groupBox5.Location = new System.Drawing.Point(708, 45);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 44);
            this.groupBox5.TabIndex = 257;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo";
            // 
            // rdTodos
            // 
            this.rdTodos.AutoSize = true;
            this.rdTodos.Location = new System.Drawing.Point(158, 19);
            this.rdTodos.Name = "rdTodos";
            this.rdTodos.Size = new System.Drawing.Size(55, 17);
            this.rdTodos.TabIndex = 2;
            this.rdTodos.Text = "Todos";
            this.rdTodos.UseVisualStyleBackColor = true;
            // 
            // rdVenda
            // 
            this.rdVenda.AutoSize = true;
            this.rdVenda.Checked = true;
            this.rdVenda.Location = new System.Drawing.Point(96, 19);
            this.rdVenda.Name = "rdVenda";
            this.rdVenda.Size = new System.Drawing.Size(56, 17);
            this.rdVenda.TabIndex = 1;
            this.rdVenda.TabStop = true;
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
            this.cbCliente.Location = new System.Drawing.Point(382, 145);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(306, 21);
            this.cbCliente.TabIndex = 259;
            this.cbCliente.Enter += new System.EventHandler(this.cbCliente_Enter);
            this.cbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCliente_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 258;
            this.label3.Text = "Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 261;
            this.label4.Text = "Funcionário:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFuncionario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(15, 146);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(358, 21);
            this.cbFuncionario.TabIndex = 260;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(789, 144);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 284;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrmProdutoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 648);
            this.Controls.Add(this.btnImprimir);
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
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label9);
            this.Name = "FrmProdutoCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumo do Caixa Diário (Produtos por Cliente)";
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cbGrupoCategoria;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbltTotalMtLinear;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdTodos;
        private System.Windows.Forms.RadioButton rdVenda;
        private System.Windows.Forms.RadioButton rdOrcamento;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORTOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPEDIDO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn MT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnpdf2;
        private System.Windows.Forms.Button btnExcel2;
        private System.Windows.Forms.Button btnPrint2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.CheckBox chkImpressaoTicket;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.MaskedTextBox mkdHoraFinal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mkdHoraInicial;
        private System.Windows.Forms.Label label24;
    }
}