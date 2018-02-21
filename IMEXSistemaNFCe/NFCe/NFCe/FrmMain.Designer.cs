namespace BmsSoftware
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblINS = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNUM = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCAPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelDayDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNomeEmpresa = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarCaminhosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empresaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçãoDeSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aberturaDeCaixaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharVendaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaVendaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sangriaNoCaixaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vendaPorProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendaPorTipoDePagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendaPorVendedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescProduto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValorUnit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQuant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalProduto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalVenda = new System.Windows.Forms.TextBox();
            this.tbProdutos = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblSituacao = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.excluirCupomNFCeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblINS,
            this.lblNUM,
            this.lblCAPS,
            this.labelDayDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1227, 24);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(546, 19);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Toggle Key Test";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblINS
            // 
            this.lblINS.AutoSize = false;
            this.lblINS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lblINS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblINS.DoubleClickEnabled = true;
            this.lblINS.Name = "lblINS";
            this.lblINS.Size = new System.Drawing.Size(40, 19);
            this.lblINS.Text = "INS";
            // 
            // lblNUM
            // 
            this.lblNUM.AutoSize = false;
            this.lblNUM.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lblNUM.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblNUM.DoubleClickEnabled = true;
            this.lblNUM.Name = "lblNUM";
            this.lblNUM.Size = new System.Drawing.Size(40, 19);
            this.lblNUM.Text = "NUM";
            // 
            // lblCAPS
            // 
            this.lblCAPS.AutoSize = false;
            this.lblCAPS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lblCAPS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblCAPS.DoubleClickEnabled = true;
            this.lblCAPS.Name = "lblCAPS";
            this.lblCAPS.Size = new System.Drawing.Size(40, 19);
            this.lblCAPS.Text = "CAPS";
            // 
            // labelDayDate
            // 
            this.labelDayDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.labelDayDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelDayDate.Name = "labelDayDate";
            this.labelDayDate.Size = new System.Drawing.Size(546, 19);
            this.labelDayDate.Spring = true;
            this.labelDayDate.Text = "01-01-01";
            // 
            // lblNomeEmpresa
            // 
            this.lblNomeEmpresa.AutoSize = true;
            this.lblNomeEmpresa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNomeEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeEmpresa.Location = new System.Drawing.Point(12, 45);
            this.lblNomeEmpresa.Name = "lblNomeEmpresa";
            this.lblNomeEmpresa.Size = new System.Drawing.Size(100, 13);
            this.lblNomeEmpresa.TabIndex = 6;
            this.lblNomeEmpresa.Text = "lblNomeEmpresa";
            this.lblNomeEmpresa.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.utilitárioToolStripMenuItem,
            this.relatóriosToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1227, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurarCaminhosToolStripMenuItem,
            this.empresaToolStripMenuItem,
            this.configuraçãoDeSistemaToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "&Arquivo";
            // 
            // configurarCaminhosToolStripMenuItem
            // 
            this.configurarCaminhosToolStripMenuItem.Name = "configurarCaminhosToolStripMenuItem";
            this.configurarCaminhosToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.configurarCaminhosToolStripMenuItem.Text = "&Configurar Caminhos";
            this.configurarCaminhosToolStripMenuItem.Click += new System.EventHandler(this.configurarCaminhosToolStripMenuItem_Click);
            // 
            // empresaToolStripMenuItem
            // 
            this.empresaToolStripMenuItem.Name = "empresaToolStripMenuItem";
            this.empresaToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.empresaToolStripMenuItem.Text = "&Empresa";
            this.empresaToolStripMenuItem.Click += new System.EventHandler(this.empresaToolStripMenuItem_Click_1);
            // 
            // configuraçãoDeSistemaToolStripMenuItem
            // 
            this.configuraçãoDeSistemaToolStripMenuItem.Name = "configuraçãoDeSistemaToolStripMenuItem";
            this.configuraçãoDeSistemaToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.configuraçãoDeSistemaToolStripMenuItem.Text = "Configuração de Sistema";
            this.configuraçãoDeSistemaToolStripMenuItem.Click += new System.EventHandler(this.configuraçãoDeSistemaToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // utilitárioToolStripMenuItem
            // 
            this.utilitárioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aberturaDeCaixaToolStripMenuItem,
            this.excluirItemToolStripMenuItem,
            this.excluirCupomNFCeToolStripMenuItem,
            this.fecharVendaToolStripMenuItem,
            this.importarPedidoToolStripMenuItem,
            this.novaVendaToolStripMenuItem,
            this.sangriaNoCaixaToolStripMenuItem});
            this.utilitárioToolStripMenuItem.Name = "utilitárioToolStripMenuItem";
            this.utilitárioToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.utilitárioToolStripMenuItem.Text = "&Utilitário";
            // 
            // aberturaDeCaixaToolStripMenuItem
            // 
            this.aberturaDeCaixaToolStripMenuItem.Name = "aberturaDeCaixaToolStripMenuItem";
            this.aberturaDeCaixaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.aberturaDeCaixaToolStripMenuItem.Text = "Abertura de Caixa";
            this.aberturaDeCaixaToolStripMenuItem.Click += new System.EventHandler(this.aberturaDeCaixaToolStripMenuItem_Click);
            // 
            // excluirItemToolStripMenuItem
            // 
            this.excluirItemToolStripMenuItem.Name = "excluirItemToolStripMenuItem";
            this.excluirItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.excluirItemToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.excluirItemToolStripMenuItem.Text = "&Excluir/Alterar Item";
            this.excluirItemToolStripMenuItem.Click += new System.EventHandler(this.excluirItemToolStripMenuItem_Click);
            // 
            // fecharVendaToolStripMenuItem
            // 
            this.fecharVendaToolStripMenuItem.Name = "fecharVendaToolStripMenuItem";
            this.fecharVendaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.fecharVendaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.fecharVendaToolStripMenuItem.Text = "&Fechar Venda";
            this.fecharVendaToolStripMenuItem.Click += new System.EventHandler(this.fecharVendaToolStripMenuItem_Click);
            // 
            // importarPedidoToolStripMenuItem
            // 
            this.importarPedidoToolStripMenuItem.Name = "importarPedidoToolStripMenuItem";
            this.importarPedidoToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.importarPedidoToolStripMenuItem.Text = "Importar Pedido";
            this.importarPedidoToolStripMenuItem.Click += new System.EventHandler(this.importarPedidoToolStripMenuItem_Click);
            // 
            // novaVendaToolStripMenuItem
            // 
            this.novaVendaToolStripMenuItem.Name = "novaVendaToolStripMenuItem";
            this.novaVendaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.novaVendaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.novaVendaToolStripMenuItem.Text = "&Nova Venda";
            this.novaVendaToolStripMenuItem.Click += new System.EventHandler(this.novaVendaToolStripMenuItem_Click);
            // 
            // sangriaNoCaixaToolStripMenuItem
            // 
            this.sangriaNoCaixaToolStripMenuItem.Name = "sangriaNoCaixaToolStripMenuItem";
            this.sangriaNoCaixaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.sangriaNoCaixaToolStripMenuItem.Text = "Sangria no Caixa";
            this.sangriaNoCaixaToolStripMenuItem.Click += new System.EventHandler(this.sangriaNoCaixaToolStripMenuItem_Click);
            // 
            // relatóriosToolStripMenuItem
            // 
            this.relatóriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pesquisaToolStripMenuItem1,
            this.vendaPorProdutoToolStripMenuItem,
            this.vendaPorTipoDePagamentoToolStripMenuItem,
            this.vendaPorVendedorToolStripMenuItem});
            this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.relatóriosToolStripMenuItem.Text = "&Relatórios";
            // 
            // pesquisaToolStripMenuItem1
            // 
            this.pesquisaToolStripMenuItem1.Name = "pesquisaToolStripMenuItem1";
            this.pesquisaToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.pesquisaToolStripMenuItem1.Size = new System.Drawing.Size(353, 22);
            this.pesquisaToolStripMenuItem1.Text = "&Pesquisa";
            this.pesquisaToolStripMenuItem1.Click += new System.EventHandler(this.pesquisaToolStripMenuItem1_Click);
            // 
            // vendaPorProdutoToolStripMenuItem
            // 
            this.vendaPorProdutoToolStripMenuItem.Name = "vendaPorProdutoToolStripMenuItem";
            this.vendaPorProdutoToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.vendaPorProdutoToolStripMenuItem.Text = "Resumo Diário Caixa (Venda por Produto)";
            this.vendaPorProdutoToolStripMenuItem.Click += new System.EventHandler(this.vendaPorProdutoToolStripMenuItem_Click);
            // 
            // vendaPorTipoDePagamentoToolStripMenuItem
            // 
            this.vendaPorTipoDePagamentoToolStripMenuItem.Name = "vendaPorTipoDePagamentoToolStripMenuItem";
            this.vendaPorTipoDePagamentoToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.vendaPorTipoDePagamentoToolStripMenuItem.Text = "Resumo Diário Caixa (Venda por Tipo de Pagamento)";
            this.vendaPorTipoDePagamentoToolStripMenuItem.Click += new System.EventHandler(this.vendaPorTipoDePagamentoToolStripMenuItem_Click);
            // 
            // vendaPorVendedorToolStripMenuItem
            // 
            this.vendaPorVendedorToolStripMenuItem.Name = "vendaPorVendedorToolStripMenuItem";
            this.vendaPorVendedorToolStripMenuItem.Size = new System.Drawing.Size(353, 22);
            this.vendaPorVendedorToolStripMenuItem.Text = "Vendas por Vendedor";
            this.vendaPorVendedorToolStripMenuItem.Click += new System.EventHandler(this.vendaPorVendedorToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.sobreToolStripMenuItem.Text = "&Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Produto - Ctrl+e para abrir a localização de produtos:";
            // 
            // txtDescProduto
            // 
            this.txtDescProduto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescProduto.BackColor = System.Drawing.Color.Yellow;
            this.txtDescProduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescProduto.Location = new System.Drawing.Point(15, 82);
            this.txtDescProduto.MaxLength = 30;
            this.txtDescProduto.Name = "txtDescProduto";
            this.txtDescProduto.Size = new System.Drawing.Size(1200, 116);
            this.txtDescProduto.TabIndex = 0;
            this.txtDescProduto.Text = "CAIXA LIVRE";
            this.txtDescProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescProduto.Enter += new System.EventHandler(this.txtDescProduto_Enter);
            this.txtDescProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescProduto_KeyDown);
            this.txtDescProduto.Leave += new System.EventHandler(this.txtDescProduto_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 84;
            this.label4.Text = "R$ Unitário:";
            // 
            // txtValorUnit
            // 
            this.txtValorUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValorUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorUnit.ForeColor = System.Drawing.Color.Blue;
            this.txtValorUnit.Location = new System.Drawing.Point(438, 286);
            this.txtValorUnit.MaxLength = 20;
            this.txtValorUnit.Name = "txtValorUnit";
            this.txtValorUnit.Size = new System.Drawing.Size(256, 47);
            this.txtValorUnit.TabIndex = 2;
            this.txtValorUnit.Text = "0,00";
            this.txtValorUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorUnit.Leave += new System.EventHandler(this.txtValorUnit_Leave);
            this.txtValorUnit.Validating += new System.ComponentModel.CancelEventHandler(this.txtValorUnit_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Quantidade:";
            // 
            // txtQuant
            // 
            this.txtQuant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuant.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuant.ForeColor = System.Drawing.Color.Blue;
            this.txtQuant.Location = new System.Drawing.Point(438, 220);
            this.txtQuant.MaxLength = 5;
            this.txtQuant.Name = "txtQuant";
            this.txtQuant.Size = new System.Drawing.Size(256, 47);
            this.txtQuant.TabIndex = 1;
            this.txtQuant.Text = "1";
            this.txtQuant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuant.Enter += new System.EventHandler(this.txtQuant_Enter);
            this.txtQuant.Leave += new System.EventHandler(this.txtQuant_Leave);
            this.txtQuant.Validated += new System.EventHandler(this.txtQuant_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(437, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 86;
            this.label1.Text = "R$ Sub Total:";
            // 
            // txtTotalProduto
            // 
            this.txtTotalProduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalProduto.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalProduto.Location = new System.Drawing.Point(440, 352);
            this.txtTotalProduto.MaxLength = 20;
            this.txtTotalProduto.Name = "txtTotalProduto";
            this.txtTotalProduto.ReadOnly = true;
            this.txtTotalProduto.Size = new System.Drawing.Size(254, 47);
            this.txtTotalProduto.TabIndex = 3;
            this.txtTotalProduto.Text = "0,00";
            this.txtTotalProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalProduto.Enter += new System.EventHandler(this.txtTotalProduto_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 402);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "R$ Total da Venda:";
            // 
            // txtTotalVenda
            // 
            this.txtTotalVenda.BackColor = System.Drawing.Color.Yellow;
            this.txtTotalVenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVenda.Location = new System.Drawing.Point(440, 418);
            this.txtTotalVenda.MaxLength = 20;
            this.txtTotalVenda.Name = "txtTotalVenda";
            this.txtTotalVenda.ReadOnly = true;
            this.txtTotalVenda.Size = new System.Drawing.Size(254, 62);
            this.txtTotalVenda.TabIndex = 4;
            this.txtTotalVenda.Text = "0,00";
            this.txtTotalVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbProdutos
            // 
            this.tbProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbProdutos.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbProdutos.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProdutos.Location = new System.Drawing.Point(15, 204);
            this.tbProdutos.Multiline = true;
            this.tbProdutos.Name = "tbProdutos";
            this.tbProdutos.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbProdutos.Size = new System.Drawing.Size(405, 383);
            this.tbProdutos.TabIndex = 89;
            this.tbProdutos.Text = "NFCe";
            this.tbProdutos.TextChanged += new System.EventHandler(this.tbProdutos_TextChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblSituacao
            // 
            this.lblSituacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSituacao.AutoSize = true;
            this.lblSituacao.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSituacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSituacao.Location = new System.Drawing.Point(1113, 45);
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Size = new System.Drawing.Size(102, 13);
            this.lblSituacao.TabIndex = 94;
            this.lblSituacao.Text = "Situação: Aberto";
            this.lblSituacao.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(723, 204);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(492, 351);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 237;
            this.pictureBox1.TabStop = false;
            // 
            // excluirCupomNFCeToolStripMenuItem
            // 
            this.excluirCupomNFCeToolStripMenuItem.Name = "excluirCupomNFCeToolStripMenuItem";
            this.excluirCupomNFCeToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.excluirCupomNFCeToolStripMenuItem.Text = "Excluir Cupom NFCe Atual";
            this.excluirCupomNFCeToolStripMenuItem.Click += new System.EventHandler(this.excluirCupomNFCeToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1227, 663);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblSituacao);
            this.Controls.Add(this.tbProdutos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTotalVenda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalProduto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValorUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtQuant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescProduto);
            this.Controls.Add(this.lblNomeEmpresa);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  IMEX Sistemas - NFCe Nota Fiscal de Consumidor Eletrônica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel lblINS;
        public System.Windows.Forms.ToolStripStatusLabel lblNUM;
        public System.Windows.Forms.ToolStripStatusLabel lblCAPS;
        public System.Windows.Forms.ToolStripStatusLabel labelDayDate;
        public System.Windows.Forms.Label lblNomeEmpresa;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem configurarCaminhosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem empresaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem configuraçãoDeSistemaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        public System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDescProduto;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtValorUnit;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtQuant;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTotalProduto;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtTotalVenda;
        public System.Windows.Forms.TextBox tbProdutos;
        public System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.ToolStripMenuItem utilitárioToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem excluirItemToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem fecharVendaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem relatóriosToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem pesquisaToolStripMenuItem1;
        public System.Windows.Forms.Label lblSituacao;
        public System.Windows.Forms.ToolStripMenuItem novaVendaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem vendaPorProdutoToolStripMenuItem;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ToolStripMenuItem vendaPorTipoDePagamentoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem aberturaDeCaixaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem importarPedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sangriaNoCaixaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendaPorVendedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excluirCupomNFCeToolStripMenuItem;
    }
}