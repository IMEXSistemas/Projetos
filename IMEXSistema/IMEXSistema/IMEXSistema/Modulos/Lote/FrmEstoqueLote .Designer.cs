namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmEstoqueLote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstoqueLote));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.tabControlMarca = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLote = new System.Windows.Forms.Button();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNao = new System.Windows.Forms.RadioButton();
            this.rbSim = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RbSaida = new System.Windows.Forms.RadioButton();
            this.RbEntrada = new System.Windows.Forms.RadioButton();
            this.txtNumeroLote = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNotaFiscal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCadProdutos = new System.Windows.Forms.Button();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtQuanProduto = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.maskedtxtData = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.btnSeach = new System.Windows.Forms.Button();
            this.label91 = new System.Windows.Forms.Label();
            this.txtPesquisaRapida = new System.Windows.Forms.TextBox();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSBGrava = new System.Windows.Forms.ToolStripButton();
            this.TSBNovo = new System.Windows.Forms.ToolStripButton();
            this.TSBDelete = new System.Windows.Forms.ToolStripButton();
            this.TSBFiltro = new System.Windows.Forms.ToolStripButton();
            this.TSBPrint = new System.Windows.Forms.ToolStripButton();
            this.TSBVolta = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gravaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apagaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saldoDoLoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saldoDoLotePorProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.lotesComSaldoPositivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMERODOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGTIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODLOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVALIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGATIVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControlMarca.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.tabControlMarca);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 543);
            this.panel1.TabIndex = 0;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(794, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(94, 13);
            this.label25.TabIndex = 85;
            this.label25.Text = "Campo Obrigatório";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(882, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Campo obrigatório";
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(13, 521);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 82;
            this.lblObsField.Text = "Obs.:";
            // 
            // tabControlMarca
            // 
            this.tabControlMarca.Controls.Add(this.tabPage2);
            this.tabControlMarca.Controls.Add(this.tabPage3);
            this.tabControlMarca.Location = new System.Drawing.Point(12, 87);
            this.tabControlMarca.Name = "tabControlMarca";
            this.tabControlMarca.SelectedIndex = 0;
            this.tabControlMarca.Size = new System.Drawing.Size(866, 431);
            this.tabControlMarca.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLote);
            this.tabPage2.Controls.Add(this.txtObservacao);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.txtNumeroLote);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtNotaFiscal);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnCadProdutos);
            this.tabPage2.Controls.Add(this.cbProduto);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtQuanProduto);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.maskedtxtData);
            this.tabPage2.Controls.Add(this.bntDateSelec);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(858, 405);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Dados Cadastrais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLote
            // 
            this.btnLote.FlatAppearance.BorderSize = 0;
            this.btnLote.Location = new System.Drawing.Point(414, 18);
            this.btnLote.Name = "btnLote";
            this.btnLote.Size = new System.Drawing.Size(26, 21);
            this.btnLote.TabIndex = 350;
            this.btnLote.UseVisualStyleBackColor = true;
            this.btnLote.Click += new System.EventHandler(this.btnLote_Click);
            // 
            // txtObservacao
            // 
            this.txtObservacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservacao.Location = new System.Drawing.Point(9, 104);
            this.txtObservacao.MaxLength = 100;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(829, 276);
            this.txtObservacao.TabIndex = 225;
            this.txtObservacao.Tag = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 226;
            this.label8.Text = "Observação:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbNao);
            this.groupBox1.Controls.Add(this.rbSim);
            this.groupBox1.Location = new System.Drawing.Point(607, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 50);
            this.groupBox1.TabIndex = 224;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ativo";
            // 
            // rbNao
            // 
            this.rbNao.AutoSize = true;
            this.rbNao.Location = new System.Drawing.Point(74, 22);
            this.rbNao.Name = "rbNao";
            this.rbNao.Size = new System.Drawing.Size(45, 17);
            this.rbNao.TabIndex = 1;
            this.rbNao.Text = "Não";
            this.rbNao.UseVisualStyleBackColor = true;
            // 
            // rbSim
            // 
            this.rbSim.AutoSize = true;
            this.rbSim.Checked = true;
            this.rbSim.Location = new System.Drawing.Point(6, 22);
            this.rbSim.Name = "rbSim";
            this.rbSim.Size = new System.Drawing.Size(42, 17);
            this.rbSim.TabIndex = 0;
            this.rbSim.TabStop = true;
            this.rbSim.Text = "Sim";
            this.rbSim.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.RbSaida);
            this.groupBox2.Controls.Add(this.RbEntrada);
            this.groupBox2.Location = new System.Drawing.Point(457, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 50);
            this.groupBox2.TabIndex = 223;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo";
            // 
            // RbSaida
            // 
            this.RbSaida.AutoSize = true;
            this.RbSaida.Location = new System.Drawing.Point(74, 22);
            this.RbSaida.Name = "RbSaida";
            this.RbSaida.Size = new System.Drawing.Size(54, 17);
            this.RbSaida.TabIndex = 1;
            this.RbSaida.Text = "Saída";
            this.RbSaida.UseVisualStyleBackColor = true;
            // 
            // RbEntrada
            // 
            this.RbEntrada.AutoSize = true;
            this.RbEntrada.Checked = true;
            this.RbEntrada.Location = new System.Drawing.Point(6, 22);
            this.RbEntrada.Name = "RbEntrada";
            this.RbEntrada.Size = new System.Drawing.Size(62, 17);
            this.RbEntrada.TabIndex = 0;
            this.RbEntrada.TabStop = true;
            this.RbEntrada.Text = "Entrada";
            this.RbEntrada.UseVisualStyleBackColor = true;
            // 
            // txtNumeroLote
            // 
            this.txtNumeroLote.Location = new System.Drawing.Point(271, 19);
            this.txtNumeroLote.MaxLength = 8;
            this.txtNumeroLote.Name = "txtNumeroLote";
            this.txtNumeroLote.Size = new System.Drawing.Size(137, 20);
            this.txtNumeroLote.TabIndex = 209;
            this.txtNumeroLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumeroLote.Leave += new System.EventHandler(this.txtNumeroLote_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(268, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 210;
            this.label7.Text = "Nº Lote:";
            // 
            // txtNotaFiscal
            // 
            this.txtNotaFiscal.Location = new System.Drawing.Point(129, 19);
            this.txtNotaFiscal.MaxLength = 8;
            this.txtNotaFiscal.Name = "txtNotaFiscal";
            this.txtNotaFiscal.Size = new System.Drawing.Size(137, 20);
            this.txtNotaFiscal.TabIndex = 207;
            this.txtNotaFiscal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(126, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 208;
            this.label4.Text = "Nº Documento:";
            // 
            // btnCadProdutos
            // 
            this.btnCadProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadProdutos.FlatAppearance.BorderSize = 0;
            this.btnCadProdutos.Location = new System.Drawing.Point(526, 57);
            this.btnCadProdutos.Name = "btnCadProdutos";
            this.btnCadProdutos.Size = new System.Drawing.Size(26, 21);
            this.btnCadProdutos.TabIndex = 206;
            this.btnCadProdutos.UseVisualStyleBackColor = true;
            this.btnCadProdutos.Click += new System.EventHandler(this.btnCadProdutos_Click);
            // 
            // cbProduto
            // 
            this.cbProduto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(68, 58);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(454, 21);
            this.cbProduto.TabIndex = 204;
            this.cbProduto.Enter += new System.EventHandler(this.cbProduto_Enter);
            this.cbProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProduto_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(68, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 205;
            this.label14.Text = "Produtos:";
            // 
            // txtQuanProduto
            // 
            this.txtQuanProduto.Location = new System.Drawing.Point(9, 59);
            this.txtQuanProduto.MaxLength = 20;
            this.txtQuanProduto.Name = "txtQuanProduto";
            this.txtQuanProduto.Size = new System.Drawing.Size(53, 20);
            this.txtQuanProduto.TabIndex = 202;
            this.txtQuanProduto.Text = "1,0000";
            this.txtQuanProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(6, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 203;
            this.label13.Text = "Quant.:";
            // 
            // maskedtxtData
            // 
            this.maskedtxtData.Location = new System.Drawing.Point(9, 19);
            this.maskedtxtData.Mask = "00/00/0000";
            this.maskedtxtData.Name = "maskedtxtData";
            this.maskedtxtData.Size = new System.Drawing.Size(79, 20);
            this.maskedtxtData.TabIndex = 83;
            this.maskedtxtData.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelec
            // 
            this.bntDateSelec.FlatAppearance.BorderSize = 0;
            this.bntDateSelec.Location = new System.Drawing.Point(94, 19);
            this.bntDateSelec.Name = "bntDateSelec";
            this.bntDateSelec.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelec.TabIndex = 85;
            this.bntDateSelec.UseVisualStyleBackColor = true;
            this.bntDateSelec.Click += new System.EventHandler(this.bntDateSelecFabri_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 84;
            this.label2.Text = "Data:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label38);
            this.tabPage3.Controls.Add(this.label37);
            this.tabPage3.Controls.Add(this.btnSeach);
            this.tabPage3.Controls.Add(this.label91);
            this.tabPage3.Controls.Add(this.txtPesquisaRapida);
            this.tabPage3.Controls.Add(this.btnpdf);
            this.tabPage3.Controls.Add(this.btnExcel);
            this.tabPage3.Controls.Add(this.btnPrint);
            this.tabPage3.Controls.Add(this.lblTotalPesquisa);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.DataGriewDados);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(858, 405);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pesquisa";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Location = new System.Drawing.Point(785, 380);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(61, 13);
            this.label38.TabIndex = 312;
            this.label38.Text = "(S) Saída";
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Blue;
            this.label37.Location = new System.Drawing.Point(714, 380);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(71, 13);
            this.label37.TabIndex = 311;
            this.label37.Text = "(E) Entrada";
            // 
            // btnSeach
            // 
            this.btnSeach.FlatAppearance.BorderSize = 0;
            this.btnSeach.Image = ((System.Drawing.Image)(resources.GetObject("btnSeach.Image")));
            this.btnSeach.Location = new System.Drawing.Point(313, 29);
            this.btnSeach.Name = "btnSeach";
            this.btnSeach.Size = new System.Drawing.Size(26, 22);
            this.btnSeach.TabIndex = 310;
            this.btnSeach.UseVisualStyleBackColor = true;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(6, 13);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(90, 13);
            this.label91.TabIndex = 309;
            this.label91.Text = "Pesquisa Rápida:";
            // 
            // txtPesquisaRapida
            // 
            this.txtPesquisaRapida.CausesValidation = false;
            this.txtPesquisaRapida.Location = new System.Drawing.Point(9, 30);
            this.txtPesquisaRapida.Name = "txtPesquisaRapida";
            this.txtPesquisaRapida.Size = new System.Drawing.Size(309, 20);
            this.txtPesquisaRapida.TabIndex = 308;
            this.txtPesquisaRapida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquisaRapida_KeyDown);
            this.txtPesquisaRapida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPesquisaRapida_KeyUp);
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(755, 27);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 302;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(786, 27);
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
            this.btnPrint.Location = new System.Drawing.Point(817, 27);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 300;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(100, 380);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 2;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 380);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Total da pesquisa:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.DATA,
            this.NUMERODOC,
            this.FLAGTIPO,
            this.CODLOTE,
            this.QUANTIDADE,
            this.DATAVALIDADE,
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.FLAGATIVO});
            this.DataGriewDados.Location = new System.Drawing.Point(9, 59);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(833, 305);
            this.DataGriewDados.TabIndex = 0;
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            this.DataGriewDados.Enter += new System.EventHandler(this.DataGriewDados_Enter);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewDados_KeyDown);
            this.DataGriewDados.Leave += new System.EventHandler(this.DataGriewDados_Leave);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(44, 44);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBGrava,
            this.TSBNovo,
            this.TSBDelete,
            this.TSBFiltro,
            this.TSBPrint,
            this.TSBVolta});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(890, 51);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStripCadatro";
            // 
            // TSBGrava
            // 
            this.TSBGrava.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBGrava.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBGrava.Name = "TSBGrava";
            this.TSBGrava.Size = new System.Drawing.Size(23, 48);
            this.TSBGrava.Text = "toolStripButton1";
            this.TSBGrava.ToolTipText = "Grava";
            this.TSBGrava.Click += new System.EventHandler(this.TSBGrava_Click);
            // 
            // TSBNovo
            // 
            this.TSBNovo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBNovo.Image = ((System.Drawing.Image)(resources.GetObject("TSBNovo.Image")));
            this.TSBNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBNovo.Name = "TSBNovo";
            this.TSBNovo.Size = new System.Drawing.Size(48, 48);
            this.TSBNovo.Text = "toolStripButton2";
            this.TSBNovo.Click += new System.EventHandler(this.TSBNovo_Click);
            // 
            // TSBDelete
            // 
            this.TSBDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBDelete.Image = ((System.Drawing.Image)(resources.GetObject("TSBDelete.Image")));
            this.TSBDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBDelete.Name = "TSBDelete";
            this.TSBDelete.Size = new System.Drawing.Size(48, 48);
            this.TSBDelete.Text = "toolStripButton1";
            this.TSBDelete.Click += new System.EventHandler(this.TSBDelete_Click);
            // 
            // TSBFiltro
            // 
            this.TSBFiltro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBFiltro.Image = ((System.Drawing.Image)(resources.GetObject("TSBFiltro.Image")));
            this.TSBFiltro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBFiltro.Name = "TSBFiltro";
            this.TSBFiltro.Size = new System.Drawing.Size(48, 48);
            this.TSBFiltro.Text = "toolStripButton3";
            this.TSBFiltro.Click += new System.EventHandler(this.TSBFiltro_Click);
            // 
            // TSBPrint
            // 
            this.TSBPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBPrint.Image = ((System.Drawing.Image)(resources.GetObject("TSBPrint.Image")));
            this.TSBPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBPrint.Name = "TSBPrint";
            this.TSBPrint.Size = new System.Drawing.Size(48, 48);
            this.TSBPrint.Text = "toolStripButton4";
            this.TSBPrint.Click += new System.EventHandler(this.TSBPrint_Click);
            // 
            // TSBVolta
            // 
            this.TSBVolta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBVolta.Image = ((System.Drawing.Image)(resources.GetObject("TSBVolta.Image")));
            this.TSBVolta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSBVolta.Name = "TSBVolta";
            this.TSBVolta.Size = new System.Drawing.Size(48, 48);
            this.TSBVolta.Text = "toolStripButton1";
            this.TSBVolta.Click += new System.EventHandler(this.TSBVolta_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gravaToolStripMenuItem,
            this.novoToolStripMenuItem,
            this.apagaToolStripMenuItem,
            this.pesquisaToolStripMenuItem,
            this.relatórioToolStripMenuItem,
            this.voltaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(890, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gravaToolStripMenuItem
            // 
            this.gravaToolStripMenuItem.Name = "gravaToolStripMenuItem";
            this.gravaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.gravaToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.gravaToolStripMenuItem.Text = "&Salva";
            this.gravaToolStripMenuItem.Click += new System.EventHandler(this.gravaToolStripMenuItem_Click);
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.novoToolStripMenuItem.Text = "&Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // apagaToolStripMenuItem
            // 
            this.apagaToolStripMenuItem.Name = "apagaToolStripMenuItem";
            this.apagaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.apagaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.apagaToolStripMenuItem.Text = "&Apaga";
            this.apagaToolStripMenuItem.Click += new System.EventHandler(this.apagaToolStripMenuItem_Click);
            // 
            // pesquisaToolStripMenuItem
            // 
            this.pesquisaToolStripMenuItem.Name = "pesquisaToolStripMenuItem";
            this.pesquisaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.pesquisaToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.pesquisaToolStripMenuItem.Text = "&Pesquisar";
            this.pesquisaToolStripMenuItem.Click += new System.EventHandler(this.pesquisaToolStripMenuItem_Click);
            // 
            // relatórioToolStripMenuItem
            // 
            this.relatórioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lotesComSaldoPositivoToolStripMenuItem,
            this.saldoDoLoteToolStripMenuItem,
            this.saldoDoLotePorProdutoToolStripMenuItem});
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.relatórioToolStripMenuItem.Text = "Relatório";
            this.relatórioToolStripMenuItem.Click += new System.EventHandler(this.relatórioToolStripMenuItem_Click);
            // 
            // saldoDoLoteToolStripMenuItem
            // 
            this.saldoDoLoteToolStripMenuItem.Name = "saldoDoLoteToolStripMenuItem";
            this.saldoDoLoteToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.saldoDoLoteToolStripMenuItem.Text = "Saldo do Lote";
            this.saldoDoLoteToolStripMenuItem.Click += new System.EventHandler(this.saldoDoLoteToolStripMenuItem_Click);
            // 
            // saldoDoLotePorProdutoToolStripMenuItem
            // 
            this.saldoDoLotePorProdutoToolStripMenuItem.Name = "saldoDoLotePorProdutoToolStripMenuItem";
            this.saldoDoLotePorProdutoToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.saldoDoLotePorProdutoToolStripMenuItem.Text = "Saldo do Lote por Produto";
            this.saldoDoLotePorProdutoToolStripMenuItem.Click += new System.EventHandler(this.saldoDoLotePorProdutoToolStripMenuItem_Click);
            // 
            // voltaToolStripMenuItem
            // 
            this.voltaToolStripMenuItem.Name = "voltaToolStripMenuItem";
            this.voltaToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.voltaToolStripMenuItem.Text = "&Volta";
            this.voltaToolStripMenuItem.Click += new System.EventHandler(this.voltaToolStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // lotesComSaldoPositivoToolStripMenuItem
            // 
            this.lotesComSaldoPositivoToolStripMenuItem.Name = "lotesComSaldoPositivoToolStripMenuItem";
            this.lotesComSaldoPositivoToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lotesComSaldoPositivoToolStripMenuItem.Text = "Lotes com Saldo Positivo";
            this.lotesComSaldoPositivoToolStripMenuItem.Click += new System.EventHandler(this.lotesComSaldoPositivoToolStripMenuItem_Click);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Editar";
            this.Column3.Image = ((System.Drawing.Image)(resources.GetObject("Column3.Image")));
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Excluir";
            this.Column4.Image = ((System.Drawing.Image)(resources.GetObject("Column4.Image")));
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 50;
            // 
            // DATA
            // 
            this.DATA.DataPropertyName = "DATA";
            this.DATA.HeaderText = "Data";
            this.DATA.Name = "DATA";
            this.DATA.ReadOnly = true;
            // 
            // NUMERODOC
            // 
            this.NUMERODOC.DataPropertyName = "NUMERODOC";
            this.NUMERODOC.HeaderText = "Nº Doc";
            this.NUMERODOC.Name = "NUMERODOC";
            this.NUMERODOC.ReadOnly = true;
            // 
            // FLAGTIPO
            // 
            this.FLAGTIPO.DataPropertyName = "FLAGTIPO";
            this.FLAGTIPO.HeaderText = "Tipo";
            this.FLAGTIPO.Name = "FLAGTIPO";
            this.FLAGTIPO.ReadOnly = true;
            this.FLAGTIPO.Width = 50;
            // 
            // CODLOTE
            // 
            this.CODLOTE.DataPropertyName = "CODLOTE";
            this.CODLOTE.HeaderText = "Lote";
            this.CODLOTE.Name = "CODLOTE";
            this.CODLOTE.ReadOnly = true;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.DataPropertyName = "QUANTIDADE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTIDADE.DefaultCellStyle = dataGridViewCellStyle1;
            this.QUANTIDADE.HeaderText = "Quant.";
            this.QUANTIDADE.Name = "QUANTIDADE";
            this.QUANTIDADE.ReadOnly = true;
            this.QUANTIDADE.Width = 80;
            // 
            // DATAVALIDADE
            // 
            this.DATAVALIDADE.DataPropertyName = "DATAVALIDADE";
            this.DATAVALIDADE.HeaderText = "Validade";
            this.DATAVALIDADE.Name = "DATAVALIDADE";
            this.DATAVALIDADE.ReadOnly = true;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.NOMEPRODUTO.DefaultCellStyle = dataGridViewCellStyle2;
            this.NOMEPRODUTO.HeaderText = "Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 300;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle3;
            this.IDPRODUTO.HeaderText = "Cód. Produto";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.ReadOnly = true;
            // 
            // FLAGATIVO
            // 
            this.FLAGATIVO.DataPropertyName = "FLAGATIVO";
            this.FLAGATIVO.HeaderText = "Ativo";
            this.FLAGATIVO.Name = "FLAGATIVO";
            this.FLAGATIVO.ReadOnly = true;
            this.FLAGATIVO.Width = 50;
            // 
            // FrmEstoqueLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 543);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmEstoqueLote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estoque do Lote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMarca_FormClosing);
            this.Load += new System.EventHandler(this.FrmTipoRegiao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarca_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMarca.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gravaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apagaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pesquisaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatórioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voltaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSBGrava;
        private System.Windows.Forms.ToolStripButton TSBNovo;
        private System.Windows.Forms.ToolStripButton TSBDelete;
        private System.Windows.Forms.ToolStripButton TSBFiltro;
        private System.Windows.Forms.ToolStripButton TSBPrint;
        private System.Windows.Forms.ToolStripButton TSBVolta;
        private System.Windows.Forms.TabControl tabControlMarca;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSeach;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox txtPesquisaRapida;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MaskedTextBox maskedtxtData;
        private System.Windows.Forms.Button bntDateSelec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuanProduto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCadProdutos;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNotaFiscal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumeroLote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNao;
        private System.Windows.Forms.RadioButton rbSim;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RbSaida;
        private System.Windows.Forms.RadioButton RbEntrada;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button btnLote;
        private System.Windows.Forms.ToolStripMenuItem saldoDoLoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saldoDoLotePorProdutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lotesComSaldoPositivoToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERODOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGTIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODLOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVALIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGATIVO;
    }
}