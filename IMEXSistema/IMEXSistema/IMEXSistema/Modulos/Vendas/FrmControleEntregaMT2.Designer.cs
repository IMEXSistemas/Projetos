namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmControleEntregaMT2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControleEntregaMT2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.tabControlMarca = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantPend = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblNumRegistros = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbVendedor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQuantPedido = new System.Windows.Forms.TextBox();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mkdtPedido = new System.Windows.Forms.MaskedTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.msktDataEntrega = new System.Windows.Forms.MaskedTextBox();
            this.txtQuanEntregue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DGDadosProduto = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.nomeproduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTENTREGUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAENTREGA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomefuncionario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCodPedido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.relatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.tabControlMarca.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosProduto)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.tabControlMarca);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 639);
            this.panel1.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(829, 607);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(88, 23);
            this.btnSair.TabIndex = 241;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(827, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 239;
            this.label8.Text = "Campos obrigatório:";
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(13, 604);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 82;
            this.lblObsField.Text = "Obs.:";
            // 
            // tabControlMarca
            // 
            this.tabControlMarca.Controls.Add(this.tabPage2);
            this.tabControlMarca.Location = new System.Drawing.Point(12, 45);
            this.tabControlMarca.Name = "tabControlMarca";
            this.tabControlMarca.SelectedIndex = 0;
            this.tabControlMarca.Size = new System.Drawing.Size(925, 556);
            this.tabControlMarca.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtQuantPend);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.lblNumRegistros);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.cbVendedor);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cbFuncionario);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtQuantPedido);
            this.tabPage2.Controls.Add(this.cbProduto);
            this.tabPage2.Controls.Add(this.label60);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.mkdtPedido);
            this.tabPage2.Controls.Add(this.btnCancel);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.label59);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.msktDataEntrega);
            this.tabPage2.Controls.Add(this.txtQuanEntregue);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.DGDadosProduto);
            this.tabPage2.Controls.Add(this.txtCodPedido);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(917, 530);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Dados Cadastrais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 258;
            this.label1.Text = "Quant. Pend.:";
            // 
            // txtQuantPend
            // 
            this.txtQuantPend.Location = new System.Drawing.Point(472, 63);
            this.txtQuantPend.MaxLength = 5;
            this.txtQuantPend.Name = "txtQuantPend";
            this.txtQuantPend.ReadOnly = true;
            this.txtQuantPend.Size = new System.Drawing.Size(78, 20);
            this.txtQuantPend.TabIndex = 257;
            this.txtQuantPend.Text = "0,00";
            this.txtQuantPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(136, 94);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 256;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Excluir todos os produtos";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblNumRegistros
            // 
            this.lblNumRegistros.AutoSize = true;
            this.lblNumRegistros.Location = new System.Drawing.Point(10, 501);
            this.lblNumRegistros.Name = "lblNumRegistros";
            this.lblNumRegistros.Size = new System.Drawing.Size(93, 13);
            this.lblNumRegistros.TabIndex = 255;
            this.lblNumRegistros.Text = "Nº de Registros: 0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(627, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 13);
            this.label15.TabIndex = 254;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(660, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 13);
            this.label14.TabIndex = 253;
            this.label14.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(283, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 13);
            this.label13.TabIndex = 252;
            this.label13.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(64, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 13);
            this.label10.TabIndex = 250;
            this.label10.Text = "*";
            // 
            // cbVendedor
            // 
            this.cbVendedor.FlatAppearance.BorderSize = 0;
            this.cbVendedor.Location = new System.Drawing.Point(526, 23);
            this.cbVendedor.Name = "cbVendedor";
            this.cbVendedor.Size = new System.Drawing.Size(26, 21);
            this.cbVendedor.TabIndex = 249;
            this.cbVendedor.UseVisualStyleBackColor = true;
            this.cbVendedor.Click += new System.EventHandler(this.cbVendedor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 248;
            this.label9.Text = "Funcionário:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(215, 23);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(305, 21);
            this.cbFuncionario.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(385, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 246;
            this.label7.Text = "Quant. Pedido:";
            // 
            // txtQuantPedido
            // 
            this.txtQuantPedido.Location = new System.Drawing.Point(385, 63);
            this.txtQuantPedido.MaxLength = 5;
            this.txtQuantPedido.Name = "txtQuantPedido";
            this.txtQuantPedido.ReadOnly = true;
            this.txtQuantPedido.Size = new System.Drawing.Size(78, 20);
            this.txtQuantPedido.TabIndex = 4;
            this.txtQuantPedido.Text = "0,00";
            this.txtQuantPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(9, 62);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(366, 21);
            this.cbProduto.TabIndex = 3;
            this.cbProduto.SelectedValueChanged += new System.EventHandler(this.cbProduto_SelectedValueChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(6, 47);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(52, 13);
            this.label60.TabIndex = 244;
            this.label60.Text = "Produtos:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(123, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 242;
            this.label6.Text = "Data Pedido:";
            // 
            // mkdtPedido
            // 
            this.mkdtPedido.Location = new System.Drawing.Point(126, 24);
            this.mkdtPedido.Mask = "00/00/0000";
            this.mkdtPedido.Name = "mkdtPedido";
            this.mkdtPedido.ReadOnly = true;
            this.mkdtPedido.Size = new System.Drawing.Size(79, 20);
            this.mkdtPedido.TabIndex = 1;
            this.mkdtPedido.ValidatingType = typeof(System.DateTime);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(792, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(672, 59);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Adicionar Entrega";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(566, 46);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(88, 13);
            this.label59.TabIndex = 238;
            this.label59.Text = "Quant. Entregue:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(556, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 236;
            this.label5.Text = "Data Entrega:";
            // 
            // msktDataEntrega
            // 
            this.msktDataEntrega.Location = new System.Drawing.Point(559, 23);
            this.msktDataEntrega.Mask = "00/00/0000";
            this.msktDataEntrega.Name = "msktDataEntrega";
            this.msktDataEntrega.Size = new System.Drawing.Size(79, 20);
            this.msktDataEntrega.TabIndex = 6;
            this.msktDataEntrega.ValidatingType = typeof(System.DateTime);
            this.msktDataEntrega.Validating += new System.ComponentModel.CancelEventHandler(this.msktDataEmissao_Validating);
            // 
            // txtQuanEntregue
            // 
            this.txtQuanEntregue.Location = new System.Drawing.Point(569, 62);
            this.txtQuanEntregue.MaxLength = 5;
            this.txtQuanEntregue.Name = "txtQuanEntregue";
            this.txtQuanEntregue.Size = new System.Drawing.Size(85, 20);
            this.txtQuanEntregue.TabIndex = 5;
            this.txtQuanEntregue.Text = "1,00";
            this.txtQuanEntregue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuanEntregue.Validating += new System.ComponentModel.CancelEventHandler(this.txtQuanProduto_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 234;
            this.label4.Text = "Produtos entregue:";
            // 
            // DGDadosProduto
            // 
            this.DGDadosProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDadosProduto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.nomeproduto,
            this.QUANTPEDIDO,
            this.QUANTENTREGUE,
            this.DATAENTREGA,
            this.nomefuncionario});
            this.DGDadosProduto.Location = new System.Drawing.Point(17, 111);
            this.DGDadosProduto.Name = "DGDadosProduto";
            this.DGDadosProduto.ReadOnly = true;
            this.DGDadosProduto.Size = new System.Drawing.Size(880, 377);
            this.DGDadosProduto.TabIndex = 9;
            this.DGDadosProduto.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosProduto_CellDoubleClick);
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Excluir";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 50;
            // 
            // nomeproduto
            // 
            this.nomeproduto.DataPropertyName = "nomeproduto";
            this.nomeproduto.HeaderText = "Produto";
            this.nomeproduto.Name = "nomeproduto";
            this.nomeproduto.ReadOnly = true;
            this.nomeproduto.Width = 250;
            // 
            // QUANTPEDIDO
            // 
            this.QUANTPEDIDO.DataPropertyName = "QUANTPEDIDO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTPEDIDO.DefaultCellStyle = dataGridViewCellStyle1;
            this.QUANTPEDIDO.HeaderText = "Quant. Pedido";
            this.QUANTPEDIDO.Name = "QUANTPEDIDO";
            this.QUANTPEDIDO.ReadOnly = true;
            this.QUANTPEDIDO.Width = 80;
            // 
            // QUANTENTREGUE
            // 
            this.QUANTENTREGUE.DataPropertyName = "QUANTENTREGUE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTENTREGUE.DefaultCellStyle = dataGridViewCellStyle2;
            this.QUANTENTREGUE.HeaderText = "Quant. Entregue";
            this.QUANTENTREGUE.Name = "QUANTENTREGUE";
            this.QUANTENTREGUE.ReadOnly = true;
            this.QUANTENTREGUE.Width = 80;
            // 
            // DATAENTREGA
            // 
            this.DATAENTREGA.DataPropertyName = "DATAENTREGA";
            this.DATAENTREGA.HeaderText = "Data Entrega";
            this.DATAENTREGA.Name = "DATAENTREGA";
            this.DATAENTREGA.ReadOnly = true;
            // 
            // nomefuncionario
            // 
            this.nomefuncionario.DataPropertyName = "nomefuncionario";
            this.nomefuncionario.HeaderText = "Funcionário";
            this.nomefuncionario.Name = "nomefuncionario";
            this.nomefuncionario.ReadOnly = true;
            this.nomefuncionario.Width = 250;
            // 
            // txtCodPedido
            // 
            this.txtCodPedido.Location = new System.Drawing.Point(9, 24);
            this.txtCodPedido.MaxLength = 50;
            this.txtCodPedido.Name = "txtCodPedido";
            this.txtCodPedido.ReadOnly = true;
            this.txtCodPedido.Size = new System.Drawing.Size(112, 20);
            this.txtCodPedido.TabIndex = 0;
            this.txtCodPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pedido:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.relatórioToolStripMenuItem,
            this.voltaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(949, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // relatórioToolStripMenuItem
            // 
            this.relatórioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.completoToolStripMenuItem});
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.relatórioToolStripMenuItem.Text = "Relatório";
            // 
            // completoToolStripMenuItem
            // 
            this.completoToolStripMenuItem.Name = "completoToolStripMenuItem";
            this.completoToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.completoToolStripMenuItem.Text = "Entrega";
            this.completoToolStripMenuItem.Click += new System.EventHandler(this.completoToolStripMenuItem_Click);
            // 
            // voltaToolStripMenuItem
            // 
            this.voltaToolStripMenuItem.Name = "voltaToolStripMenuItem";
            this.voltaToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
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
            // FrmControleEntregaMT2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 639);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmControleEntregaMT2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Entrega - Produto MT2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMarca_FormClosing);
            this.Load += new System.EventHandler(this.FrmTipoRegiao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarca_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMarca.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosProduto)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem relatórioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voltaToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMarca;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtCodPedido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DGDadosProduto;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox msktDataEntrega;
        private System.Windows.Forms.TextBox txtQuanEntregue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mkdtPedido;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQuantPedido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cbVendedor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblNumRegistros;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeproduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTENTREGUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAENTREGA;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomefuncionario;
        private System.Windows.Forms.ToolStripMenuItem completoToolStripMenuItem;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantPend;
    }
}