namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmRelatorioPersonalizado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelatorioPersonalizado));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblObsField = new System.Windows.Forms.Label();
            this.tabControlRelatPersonalizada = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pBoxOrientacao = new System.Windows.Forms.PictureBox();
            this.rdbPaisagem = new System.Windows.Forms.RadioButton();
            this.rdbRetrato = new System.Windows.Forms.RadioButton();
            this.cbTela = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ckordenar = new System.Windows.Forms.CheckBox();
            this.ckSomatorio = new System.Windows.Forms.CheckBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnIncluirCampo = new System.Windows.Forms.Button();
            this.dataGridCampos = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOMECAMPOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAMANHO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTamanho = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCamposTela = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.NOMERELATORIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDRELATORIOPERSONALIZADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tabControlRelatPersonalizada.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxOrientacao)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCampos)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.tabControlRelatPersonalizada);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 377);
            this.panel1.TabIndex = 0;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(13, 355);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 82;
            this.lblObsField.Text = "Obs.:";
            // 
            // tabControlRelatPersonalizada
            // 
            this.tabControlRelatPersonalizada.Controls.Add(this.tabPage1);
            this.tabControlRelatPersonalizada.Controls.Add(this.tabPage3);
            this.tabControlRelatPersonalizada.Controls.Add(this.tabPage2);
            this.tabControlRelatPersonalizada.Location = new System.Drawing.Point(12, 78);
            this.tabControlRelatPersonalizada.Name = "tabControlRelatPersonalizada";
            this.tabControlRelatPersonalizada.SelectedIndex = 0;
            this.tabControlRelatPersonalizada.Size = new System.Drawing.Size(431, 270);
            this.tabControlRelatPersonalizada.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.cbTela);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtObservacao);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtNome);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(423, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dados Cadastrais";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pBoxOrientacao);
            this.groupBox1.Controls.Add(this.rdbPaisagem);
            this.groupBox1.Controls.Add(this.rdbRetrato);
            this.groupBox1.Location = new System.Drawing.Point(11, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 65);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orientação";
            // 
            // pBoxOrientacao
            // 
            this.pBoxOrientacao.Location = new System.Drawing.Point(142, 19);
            this.pBoxOrientacao.Name = "pBoxOrientacao";
            this.pBoxOrientacao.Size = new System.Drawing.Size(43, 40);
            this.pBoxOrientacao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxOrientacao.TabIndex = 20;
            this.pBoxOrientacao.TabStop = false;
            // 
            // rdbPaisagem
            // 
            this.rdbPaisagem.AutoSize = true;
            this.rdbPaisagem.Location = new System.Drawing.Point(24, 42);
            this.rdbPaisagem.Name = "rdbPaisagem";
            this.rdbPaisagem.Size = new System.Drawing.Size(71, 17);
            this.rdbPaisagem.TabIndex = 19;
            this.rdbPaisagem.Text = "Paisagem";
            this.rdbPaisagem.UseVisualStyleBackColor = true;
            this.rdbPaisagem.Click += new System.EventHandler(this.rdbPaisagem_Click);
            // 
            // rdbRetrato
            // 
            this.rdbRetrato.AutoSize = true;
            this.rdbRetrato.Checked = true;
            this.rdbRetrato.Location = new System.Drawing.Point(24, 19);
            this.rdbRetrato.Name = "rdbRetrato";
            this.rdbRetrato.Size = new System.Drawing.Size(60, 17);
            this.rdbRetrato.TabIndex = 18;
            this.rdbRetrato.TabStop = true;
            this.rdbRetrato.Text = "Retrato";
            this.rdbRetrato.UseVisualStyleBackColor = true;
            this.rdbRetrato.Click += new System.EventHandler(this.rdbRetrato_Click);
            // 
            // cbTela
            // 
            this.cbTela.FormattingEnabled = true;
            this.cbTela.Location = new System.Drawing.Point(11, 136);
            this.cbTela.Name = "cbTela";
            this.cbTela.Size = new System.Drawing.Size(397, 21);
            this.cbTela.TabIndex = 10;
            this.cbTela.SelectionChangeCommitted += new System.EventHandler(this.cbTela_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tela:";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(11, 71);
            this.txtObservacao.MaxLength = 100;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(397, 46);
            this.txtObservacao.TabIndex = 2;
            this.txtObservacao.Tag = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Observação:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(11, 32);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(397, 20);
            this.txtNome.TabIndex = 1;
            this.txtNome.Validating += new System.ComponentModel.CancelEventHandler(this.txtNome_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do Relatório:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ckordenar);
            this.tabPage3.Controls.Add(this.ckSomatorio);
            this.tabPage3.Controls.Add(this.lblTipo);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.btnIncluirCampo);
            this.tabPage3.Controls.Add(this.dataGridCampos);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtTamanho);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.cbCamposTela);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(423, 244);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Campos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ckordenar
            // 
            this.ckordenar.AutoSize = true;
            this.ckordenar.Location = new System.Drawing.Point(222, 73);
            this.ckordenar.Name = "ckordenar";
            this.ckordenar.Size = new System.Drawing.Size(140, 17);
            this.ckordenar.TabIndex = 38;
            this.ckordenar.Text = "Ordenar por este campo";
            this.ckordenar.UseVisualStyleBackColor = true;
            // 
            // ckSomatorio
            // 
            this.ckSomatorio.AutoSize = true;
            this.ckSomatorio.Location = new System.Drawing.Point(222, 50);
            this.ckSomatorio.Name = "ckSomatorio";
            this.ckSomatorio.Size = new System.Drawing.Size(132, 17);
            this.ckSomatorio.TabIndex = 37;
            this.ckSomatorio.Text = "Campo com Somatório";
            this.ckSomatorio.UseVisualStyleBackColor = true;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(9, 64);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(28, 13);
            this.lblTipo.TabIndex = 36;
            this.lblTipo.Text = "Tipo";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(120, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnIncluirCampo
            // 
            this.btnIncluirCampo.Location = new System.Drawing.Point(9, 88);
            this.btnIncluirCampo.Name = "btnIncluirCampo";
            this.btnIncluirCampo.Size = new System.Drawing.Size(105, 23);
            this.btnIncluirCampo.TabIndex = 34;
            this.btnIncluirCampo.Text = "Adicionar Campo";
            this.btnIncluirCampo.UseVisualStyleBackColor = true;
            this.btnIncluirCampo.Click += new System.EventHandler(this.btnIncluirCampo_Click);
            // 
            // dataGridCampos
            // 
            this.dataGridCampos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCampos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.NOMECAMPOS,
            this.TAMANHO});
            this.dataGridCampos.Location = new System.Drawing.Point(9, 119);
            this.dataGridCampos.Name = "dataGridCampos";
            this.dataGridCampos.ReadOnly = true;
            this.dataGridCampos.Size = new System.Drawing.Size(397, 119);
            this.dataGridCampos.TabIndex = 33;
            this.dataGridCampos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridCampos_CellContentClick);
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
            // NOMECAMPOS
            // 
            this.NOMECAMPOS.DataPropertyName = "NOMECAMPOS";
            this.NOMECAMPOS.HeaderText = "Campo";
            this.NOMECAMPOS.Name = "NOMECAMPOS";
            this.NOMECAMPOS.ReadOnly = true;
            this.NOMECAMPOS.Width = 200;
            // 
            // TAMANHO
            // 
            this.TAMANHO.DataPropertyName = "TAMANHO";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.TAMANHO.DefaultCellStyle = dataGridViewCellStyle1;
            this.TAMANHO.HeaderText = "Tamanho";
            this.TAMANHO.Name = "TAMANHO";
            this.TAMANHO.ReadOnly = true;
            this.TAMANHO.Width = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Tipo:";
            // 
            // txtTamanho
            // 
            this.txtTamanho.Location = new System.Drawing.Point(154, 63);
            this.txtTamanho.MaxLength = 3;
            this.txtTamanho.Name = "txtTamanho";
            this.txtTamanho.Size = new System.Drawing.Size(52, 20);
            this.txtTamanho.TabIndex = 29;
            this.txtTamanho.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Tamanho:";
            // 
            // cbCamposTela
            // 
            this.cbCamposTela.FormattingEnabled = true;
            this.cbCamposTela.Items.AddRange(new object[] {
            "Selecione..."});
            this.cbCamposTela.Location = new System.Drawing.Point(9, 23);
            this.cbCamposTela.Name = "cbCamposTela";
            this.cbCamposTela.Size = new System.Drawing.Size(397, 21);
            this.cbCamposTela.TabIndex = 27;
            this.cbCamposTela.SelectionChangeCommitted += new System.EventHandler(this.cbCamposTela_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Campo:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblTotalPesquisa);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.DataGriewDados);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(423, 244);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Pesquisa";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(103, 228);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 5;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 228);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 4;
            this.label33.Text = "Total da pesquisa:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMERELATORIO,
            this.IDRELATORIOPERSONALIZADO});
            this.DataGriewDados.Location = new System.Drawing.Point(6, 6);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(404, 209);
            this.DataGriewDados.TabIndex = 3;
            this.DataGriewDados.Enter += new System.EventHandler(this.DataGriewDados_Enter);
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            this.DataGriewDados.Leave += new System.EventHandler(this.DataGriewDados_Leave);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewDados_KeyDown);
            // 
            // NOMERELATORIO
            // 
            this.NOMERELATORIO.DataPropertyName = "NOMERELATORIO";
            this.NOMERELATORIO.HeaderText = "Nome";
            this.NOMERELATORIO.Name = "NOMERELATORIO";
            this.NOMERELATORIO.ReadOnly = true;
            this.NOMERELATORIO.Width = 300;
            // 
            // IDRELATORIOPERSONALIZADO
            // 
            this.IDRELATORIOPERSONALIZADO.DataPropertyName = "IDRELATORIOPERSONALIZADO";
            this.IDRELATORIOPERSONALIZADO.HeaderText = "Código";
            this.IDRELATORIOPERSONALIZADO.Name = "IDRELATORIOPERSONALIZADO";
            this.IDRELATORIOPERSONALIZADO.ReadOnly = true;
            this.IDRELATORIOPERSONALIZADO.Width = 50;
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
            this.toolStrip1.Size = new System.Drawing.Size(455, 51);
            this.toolStrip1.TabIndex = 8;
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
            this.menuStrip1.Size = new System.Drawing.Size(455, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gravaToolStripMenuItem
            // 
            this.gravaToolStripMenuItem.Name = "gravaToolStripMenuItem";
            this.gravaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.gravaToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.gravaToolStripMenuItem.Text = "&Salva";
            this.gravaToolStripMenuItem.Click += new System.EventHandler(this.gravaToolStripMenuItem_Click);
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.novoToolStripMenuItem.Text = "&Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // apagaToolStripMenuItem
            // 
            this.apagaToolStripMenuItem.Name = "apagaToolStripMenuItem";
            this.apagaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.apagaToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.apagaToolStripMenuItem.Text = "&Apaga";
            this.apagaToolStripMenuItem.Click += new System.EventHandler(this.apagaToolStripMenuItem_Click);
            // 
            // pesquisaToolStripMenuItem
            // 
            this.pesquisaToolStripMenuItem.Name = "pesquisaToolStripMenuItem";
            this.pesquisaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.pesquisaToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.pesquisaToolStripMenuItem.Text = "&Pesquisar";
            this.pesquisaToolStripMenuItem.Click += new System.EventHandler(this.pesquisaToolStripMenuItem_Click);
            // 
            // relatórioToolStripMenuItem
            // 
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.relatórioToolStripMenuItem.Text = "Relatório";
            this.relatórioToolStripMenuItem.Click += new System.EventHandler(this.relatórioToolStripMenuItem_Click);
            // 
            // voltaToolStripMenuItem
            // 
            this.voltaToolStripMenuItem.Name = "voltaToolStripMenuItem";
            this.voltaToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
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
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // FrmRelatorioPersonalizado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 377);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRelatorioPersonalizado";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Personalizado";
            this.Load += new System.EventHandler(this.FrmRelatorioPersonalizado_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRelatorioPersonalizado_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlRelatPersonalizada.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxOrientacao)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCampos)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControlRelatPersonalizada;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cbTela;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.RadioButton rdbRetrato;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbPaisagem;
        private System.Windows.Forms.PictureBox pBoxOrientacao;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnIncluirCampo;
        private System.Windows.Forms.DataGridView dataGridCampos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTamanho;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCamposTela;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMERELATORIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRELATORIOPERSONALIZADO;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECAMPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAMANHO;
        private System.Windows.Forms.CheckBox ckSomatorio;
        private System.Windows.Forms.CheckBox ckordenar;
        private System.Windows.Forms.Label lblObsField;
    }
}