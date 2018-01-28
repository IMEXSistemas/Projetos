namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmNCM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNCM));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblObsField = new System.Windows.Forms.Label();
            this.tabControlMarca = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAlImportacao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlNacional = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCodNCM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnLimpaPesquisa = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.chkBoxAcumulaPesquisa = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtCriterioPesquisa = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.cbTipoPesquisa = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cbCamposPesquisa = new System.Windows.Forms.ComboBox();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.CODNCM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALNACIONAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALIMPORTACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.voltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.tabControlMarca.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.tabControlMarca);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 464);
            this.panel1.TabIndex = 0;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(9, 442);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 82;
            this.lblObsField.Text = "Obs.:";
            // 
            // tabControlMarca
            // 
            this.tabControlMarca.Controls.Add(this.tabPage1);
            this.tabControlMarca.Controls.Add(this.tabPage3);
            this.tabControlMarca.Location = new System.Drawing.Point(12, 87);
            this.tabControlMarca.Name = "tabControlMarca";
            this.tabControlMarca.SelectedIndex = 0;
            this.tabControlMarca.Size = new System.Drawing.Size(867, 344);
            this.tabControlMarca.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtAlImportacao);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtAlNacional);
            this.tabPage1.Controls.Add(this.label65);
            this.tabPage1.Controls.Add(this.txtDescricao);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtCodNCM);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(859, 318);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dados Cadastrais";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 235;
            this.button1.Text = "Excluir NCM Repetido";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAlImportacao
            // 
            this.txtAlImportacao.Location = new System.Drawing.Point(230, 32);
            this.txtAlImportacao.MaxLength = 20;
            this.txtAlImportacao.Name = "txtAlImportacao";
            this.txtAlImportacao.Size = new System.Drawing.Size(69, 20);
            this.txtAlImportacao.TabIndex = 233;
            this.txtAlImportacao.Text = "0,00";
            this.txtAlImportacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlImportacao.Enter += new System.EventHandler(this.txtAlImportacao_Enter);
            this.txtAlImportacao.Validating += new System.ComponentModel.CancelEventHandler(this.txtAlImportacao_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 234;
            this.label2.Text = "Aliq. Importação";
            // 
            // txtAlNacional
            // 
            this.txtAlNacional.Location = new System.Drawing.Point(152, 32);
            this.txtAlNacional.MaxLength = 20;
            this.txtAlNacional.Name = "txtAlNacional";
            this.txtAlNacional.Size = new System.Drawing.Size(69, 20);
            this.txtAlNacional.TabIndex = 231;
            this.txtAlNacional.Text = "0,00";
            this.txtAlNacional.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlNacional.Enter += new System.EventHandler(this.txtAlNacional_Enter);
            this.txtAlNacional.Validating += new System.ComponentModel.CancelEventHandler(this.txtAlNacional_Validating);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(149, 16);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(72, 13);
            this.label65.TabIndex = 232;
            this.label65.Text = "Aliq. Nacional";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(11, 71);
            this.txtDescricao.MaxLength = 100;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(822, 212);
            this.txtDescricao.TabIndex = 2;
            this.txtDescricao.Tag = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Descrição";
            // 
            // txtCodNCM
            // 
            this.txtCodNCM.Location = new System.Drawing.Point(11, 32);
            this.txtCodNCM.MaxLength = 50;
            this.txtCodNCM.Name = "txtCodNCM";
            this.txtCodNCM.Size = new System.Drawing.Size(133, 20);
            this.txtCodNCM.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cód. NCM:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnpdf);
            this.tabPage3.Controls.Add(this.btnExcel);
            this.tabPage3.Controls.Add(this.btnPrint);
            this.tabPage3.Controls.Add(this.btnLimpaPesquisa);
            this.tabPage3.Controls.Add(this.btnPesquisa);
            this.tabPage3.Controls.Add(this.chkBoxAcumulaPesquisa);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.txtCriterioPesquisa);
            this.tabPage3.Controls.Add(this.label35);
            this.tabPage3.Controls.Add(this.cbTipoPesquisa);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.cbCamposPesquisa);
            this.tabPage3.Controls.Add(this.lblTotalPesquisa);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.DataGriewDados);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(859, 318);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pesquisa";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(751, 46);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 305;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(782, 46);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 304;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(813, 46);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 303;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnLimpaPesquisa
            // 
            this.btnLimpaPesquisa.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLimpaPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpaPesquisa.Image")));
            this.btnLimpaPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpaPesquisa.Location = new System.Drawing.Point(630, 43);
            this.btnLimpaPesquisa.Name = "btnLimpaPesquisa";
            this.btnLimpaPesquisa.Size = new System.Drawing.Size(106, 23);
            this.btnLimpaPesquisa.TabIndex = 39;
            this.btnLimpaPesquisa.Text = "Limpa Pesquisa";
            this.btnLimpaPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpaPesquisa.UseVisualStyleBackColor = true;
            this.btnLimpaPesquisa.Click += new System.EventHandler(this.btnLimpaPesquisa_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(549, 43);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 38;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // chkBoxAcumulaPesquisa
            // 
            this.chkBoxAcumulaPesquisa.AutoSize = true;
            this.chkBoxAcumulaPesquisa.Location = new System.Drawing.Point(9, 6);
            this.chkBoxAcumulaPesquisa.Name = "chkBoxAcumulaPesquisa";
            this.chkBoxAcumulaPesquisa.Size = new System.Drawing.Size(116, 17);
            this.chkBoxAcumulaPesquisa.TabIndex = 37;
            this.chkBoxAcumulaPesquisa.Text = "Acumular Pesquisa";
            this.chkBoxAcumulaPesquisa.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(318, 29);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(103, 13);
            this.label36.TabIndex = 36;
            this.label36.Text = "Critério de Pesquisa:";
            // 
            // txtCriterioPesquisa
            // 
            this.txtCriterioPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCriterioPesquisa.CausesValidation = false;
            this.txtCriterioPesquisa.Location = new System.Drawing.Point(321, 46);
            this.txtCriterioPesquisa.Name = "txtCriterioPesquisa";
            this.txtCriterioPesquisa.Size = new System.Drawing.Size(222, 20);
            this.txtCriterioPesquisa.TabIndex = 35;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(191, 29);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(92, 13);
            this.label35.TabIndex = 34;
            this.label35.Text = "Tipo de Pesquisa:";
            // 
            // cbTipoPesquisa
            // 
            this.cbTipoPesquisa.FormattingEnabled = true;
            this.cbTipoPesquisa.Location = new System.Drawing.Point(194, 45);
            this.cbTipoPesquisa.Name = "cbTipoPesquisa";
            this.cbTipoPesquisa.Size = new System.Drawing.Size(121, 21);
            this.cbTipoPesquisa.TabIndex = 33;
            this.cbTipoPesquisa.Text = "Campo de Pesquisa";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 29);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(109, 13);
            this.label34.TabIndex = 32;
            this.label34.Text = "Campos de Pesquisa:";
            // 
            // cbCamposPesquisa
            // 
            this.cbCamposPesquisa.FormattingEnabled = true;
            this.cbCamposPesquisa.Location = new System.Drawing.Point(8, 45);
            this.cbCamposPesquisa.Name = "cbCamposPesquisa";
            this.cbCamposPesquisa.Size = new System.Drawing.Size(180, 21);
            this.cbCamposPesquisa.TabIndex = 31;
            this.cbCamposPesquisa.Text = "Campo de Pesquisa";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(100, 297);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 2;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 297);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Total da pesquisa:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CODNCM,
            this.DESCRICAO,
            this.ALNACIONAL,
            this.ALIMPORTACAO});
            this.DataGriewDados.Location = new System.Drawing.Point(6, 72);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(832, 222);
            this.DataGriewDados.TabIndex = 0;
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            this.DataGriewDados.Enter += new System.EventHandler(this.DataGriewDados_Enter);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewDados_KeyDown);
            this.DataGriewDados.Leave += new System.EventHandler(this.DataGriewDados_Leave);
            // 
            // CODNCM
            // 
            this.CODNCM.DataPropertyName = "CODNCM";
            this.CODNCM.HeaderText = "Cód. NCM";
            this.CODNCM.Name = "CODNCM";
            this.CODNCM.ReadOnly = true;
            this.CODNCM.Width = 150;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.DataPropertyName = "DESCRICAO";
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.ReadOnly = true;
            this.DESCRICAO.Width = 400;
            // 
            // ALNACIONAL
            // 
            this.ALNACIONAL.DataPropertyName = "ALNACIONAL";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.ALNACIONAL.DefaultCellStyle = dataGridViewCellStyle1;
            this.ALNACIONAL.HeaderText = "Al. Nacional";
            this.ALNACIONAL.Name = "ALNACIONAL";
            this.ALNACIONAL.ReadOnly = true;
            this.ALNACIONAL.Width = 80;
            // 
            // ALIMPORTACAO
            // 
            this.ALIMPORTACAO.DataPropertyName = "ALIMPORTACAO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.ALIMPORTACAO.DefaultCellStyle = dataGridViewCellStyle2;
            this.ALIMPORTACAO.HeaderText = "Al. Importação";
            this.ALIMPORTACAO.Name = "ALIMPORTACAO";
            this.ALIMPORTACAO.ReadOnly = true;
            this.ALIMPORTACAO.Width = 80;
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
            this.toolStrip1.Size = new System.Drawing.Size(891, 51);
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
            this.menuStrip1.Size = new System.Drawing.Size(891, 24);
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
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.relatórioToolStripMenuItem.Text = "Relatório";
            this.relatórioToolStripMenuItem.Click += new System.EventHandler(this.relatórioToolStripMenuItem_Click);
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
            // FrmNCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 464);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmNCM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de NCM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMarca_FormClosing);
            this.Load += new System.EventHandler(this.FrmTipoRegiao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarca_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMarca.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCodNCM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.TextBox txtAlImportacao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlNacional;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.CheckBox chkBoxAcumulaPesquisa;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtCriterioPesquisa;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cbTipoPesquisa;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbCamposPesquisa;
        private System.Windows.Forms.Button btnLimpaPesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODNCM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALNACIONAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIMPORTACAO;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}