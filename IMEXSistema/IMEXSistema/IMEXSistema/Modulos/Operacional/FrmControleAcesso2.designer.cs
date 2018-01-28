namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmControleAcesso2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControleAcesso2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblObsField = new System.Windows.Forms.Label();
            this.tabControlMarca = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTela = new System.Windows.Forms.Button();
            this.btnCadGrupoTela = new System.Windows.Forms.Button();
            this.btnCadNivel = new System.Windows.Forms.Button();
            this.cbTelas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGrupoTelas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cbnivel2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTelas = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSBGrava = new System.Windows.Forms.ToolStripButton();
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
            this.IDFORMULARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMETELA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acesso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gravar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apagar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEGRUPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControlMarca.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelas)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(967, 533);
            this.panel1.TabIndex = 0;
            // 
            // lblObsField
            // 
            this.lblObsField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(9, 502);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 82;
            this.lblObsField.Text = "Obs.:";
            // 
            // tabControlMarca
            // 
            this.tabControlMarca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMarca.Controls.Add(this.tabPage2);
            this.tabControlMarca.Controls.Add(this.tabPage1);
            this.tabControlMarca.Location = new System.Drawing.Point(12, 87);
            this.tabControlMarca.Name = "tabControlMarca";
            this.tabControlMarca.SelectedIndex = 0;
            this.tabControlMarca.Size = new System.Drawing.Size(929, 401);
            this.tabControlMarca.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnTela);
            this.tabPage2.Controls.Add(this.btnCadGrupoTela);
            this.tabPage2.Controls.Add(this.btnCadNivel);
            this.tabPage2.Controls.Add(this.cbTelas);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbGrupoTelas);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.linkLabel2);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.cbnivel2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvTelas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(921, 375);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Dados Cadastrais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTela
            // 
            this.btnTela.FlatAppearance.BorderSize = 0;
            this.btnTela.Location = new System.Drawing.Point(425, 99);
            this.btnTela.Name = "btnTela";
            this.btnTela.Size = new System.Drawing.Size(26, 21);
            this.btnTela.TabIndex = 181;
            this.btnTela.UseVisualStyleBackColor = true;
            this.btnTela.Click += new System.EventHandler(this.btnTela_Click);
            // 
            // btnCadGrupoTela
            // 
            this.btnCadGrupoTela.FlatAppearance.BorderSize = 0;
            this.btnCadGrupoTela.Location = new System.Drawing.Point(425, 59);
            this.btnCadGrupoTela.Name = "btnCadGrupoTela";
            this.btnCadGrupoTela.Size = new System.Drawing.Size(26, 21);
            this.btnCadGrupoTela.TabIndex = 180;
            this.btnCadGrupoTela.UseVisualStyleBackColor = true;
            this.btnCadGrupoTela.Click += new System.EventHandler(this.btnCadGrupoTela_Click);
            // 
            // btnCadNivel
            // 
            this.btnCadNivel.FlatAppearance.BorderSize = 0;
            this.btnCadNivel.Location = new System.Drawing.Point(425, 19);
            this.btnCadNivel.Name = "btnCadNivel";
            this.btnCadNivel.Size = new System.Drawing.Size(26, 21);
            this.btnCadNivel.TabIndex = 179;
            this.btnCadNivel.UseVisualStyleBackColor = true;
            this.btnCadNivel.Click += new System.EventHandler(this.btnCadNivel_Click);
            // 
            // cbTelas
            // 
            this.cbTelas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTelas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTelas.FormattingEnabled = true;
            this.cbTelas.Location = new System.Drawing.Point(18, 99);
            this.cbTelas.Name = "cbTelas";
            this.cbTelas.Size = new System.Drawing.Size(401, 21);
            this.cbTelas.TabIndex = 177;
            this.cbTelas.SelectedValueChanged += new System.EventHandler(this.cbTelas_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 178;
            this.label3.Text = "Tela:";
            // 
            // cbGrupoTelas
            // 
            this.cbGrupoTelas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGrupoTelas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupoTelas.FormattingEnabled = true;
            this.cbGrupoTelas.Location = new System.Drawing.Point(18, 59);
            this.cbGrupoTelas.Name = "cbGrupoTelas";
            this.cbGrupoTelas.Size = new System.Drawing.Size(401, 21);
            this.cbGrupoTelas.TabIndex = 175;
            this.cbGrupoTelas.SelectedValueChanged += new System.EventHandler(this.cbGrupoTelas_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 176;
            this.label2.Text = "Grupo de Telas:";
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(786, 127);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(115, 13);
            this.linkLabel2.TabIndex = 174;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Remover Acesso Total";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(711, 127);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(69, 13);
            this.linkLabel1.TabIndex = 173;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Acesso Total";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cbnivel2
            // 
            this.cbnivel2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbnivel2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbnivel2.FormattingEnabled = true;
            this.cbnivel2.Location = new System.Drawing.Point(18, 19);
            this.cbnivel2.Name = "cbnivel2";
            this.cbnivel2.Size = new System.Drawing.Size(401, 21);
            this.cbnivel2.TabIndex = 170;
            this.cbnivel2.SelectedValueChanged += new System.EventHandler(this.cbnivel2_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 171;
            this.label1.Text = "Nível:";
            // 
            // dgvTelas
            // 
            this.dgvTelas.AllowUserToAddRows = false;
            this.dgvTelas.AllowUserToDeleteRows = false;
            this.dgvTelas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDFORMULARIO,
            this.NOMETELA2,
            this.Acesso,
            this.Gravar,
            this.Apagar,
            this.NOMEGRUPO});
            this.dgvTelas.Location = new System.Drawing.Point(18, 143);
            this.dgvTelas.Name = "dgvTelas";
            this.dgvTelas.Size = new System.Drawing.Size(883, 205);
            this.dgvTelas.TabIndex = 169;
            this.dgvTelas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelas_CellEndEdit);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(921, 375);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Controle por Tela";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(44, 44);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBGrava,
            this.TSBPrint,
            this.TSBVolta});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(967, 51);
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
            this.menuStrip1.Size = new System.Drawing.Size(967, 24);
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
            this.novoToolStripMenuItem.Visible = false;
            // 
            // apagaToolStripMenuItem
            // 
            this.apagaToolStripMenuItem.Name = "apagaToolStripMenuItem";
            this.apagaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.apagaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.apagaToolStripMenuItem.Text = "&Apaga";
            this.apagaToolStripMenuItem.Visible = false;
            // 
            // pesquisaToolStripMenuItem
            // 
            this.pesquisaToolStripMenuItem.Name = "pesquisaToolStripMenuItem";
            this.pesquisaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.pesquisaToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.pesquisaToolStripMenuItem.Text = "&Pesquisar";
            this.pesquisaToolStripMenuItem.Visible = false;
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
            // IDFORMULARIO
            // 
            this.IDFORMULARIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IDFORMULARIO.DataPropertyName = "IDFORMULARIO";
            this.IDFORMULARIO.FillWeight = 137.0816F;
            this.IDFORMULARIO.Frozen = true;
            this.IDFORMULARIO.HeaderText = "Código";
            this.IDFORMULARIO.Name = "IDFORMULARIO";
            this.IDFORMULARIO.ReadOnly = true;
            this.IDFORMULARIO.Width = 60;
            // 
            // NOMETELA2
            // 
            this.NOMETELA2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NOMETELA2.DataPropertyName = "NOMETELA";
            this.NOMETELA2.HeaderText = "Tela";
            this.NOMETELA2.Name = "NOMETELA2";
            this.NOMETELA2.ReadOnly = true;
            this.NOMETELA2.Width = 300;
            // 
            // Acesso
            // 
            this.Acesso.DataPropertyName = "FLAGACESSA";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Acesso.DefaultCellStyle = dataGridViewCellStyle1;
            this.Acesso.HeaderText = "Acesso";
            this.Acesso.MinimumWidth = 80;
            this.Acesso.Name = "Acesso";
            // 
            // Gravar
            // 
            this.Gravar.DataPropertyName = "FLAGALTERA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Gravar.DefaultCellStyle = dataGridViewCellStyle2;
            this.Gravar.HeaderText = "Gravar";
            this.Gravar.MinimumWidth = 80;
            this.Gravar.Name = "Gravar";
            // 
            // Apagar
            // 
            this.Apagar.DataPropertyName = "FLAGAPAGA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Apagar.DefaultCellStyle = dataGridViewCellStyle3;
            this.Apagar.HeaderText = "Apagar";
            this.Apagar.MinimumWidth = 80;
            this.Apagar.Name = "Apagar";
            // 
            // NOMEGRUPO
            // 
            this.NOMEGRUPO.DataPropertyName = "NOMEGRUPO";
            this.NOMEGRUPO.HeaderText = "Grupo";
            this.NOMEGRUPO.Name = "NOMEGRUPO";
            // 
            // FrmControleAcesso2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 533);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmControleAcesso2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Acesso";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMarca_FormClosing);
            this.Load += new System.EventHandler(this.FrmTipoRegiao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMarca_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMarca.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelas)).EndInit();
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
        private System.Windows.Forms.ToolStripButton TSBPrint;
        private System.Windows.Forms.ToolStripButton TSBVolta;
        private System.Windows.Forms.TabControl tabControlMarca;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvTelas;
        private System.Windows.Forms.ComboBox cbnivel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbTelas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbGrupoTelas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTela;
        private System.Windows.Forms.Button btnCadGrupoTela;
        private System.Windows.Forms.Button btnCadNivel;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDFORMULARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMETELA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acesso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gravar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apagar;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEGRUPO;
    }
}