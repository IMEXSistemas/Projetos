namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmHelpDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelpDesk));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblobsfield = new System.Windows.Forms.Label();
            this.tabControlHelpDesk = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodAtendimento = new System.Windows.Forms.TextBox();
            this.maskDtaPrev = new System.Windows.Forms.MaskedTextBox();
            this.txtnomeSolicitante = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.btnCadDeparta = new System.Windows.Forms.Button();
            this.btnTipoSolicitante = new System.Windows.Forms.Button();
            this.bntCadSituacao = new System.Windows.Forms.Button();
            this.txtSolucao = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPrioridade = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbDepartamento = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataInc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTelefoneSolicitante = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailSolicitante = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTipoSolicitante = new System.Windows.Forms.ComboBox();
            this.txtSolicitacao = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.IDHELPDESK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAINCLUSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATASOLUCAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPOSOLICITANTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEDEPARTAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRIORIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFUNCIONARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESOLICITANTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpaPesquisa = new System.Windows.Forms.Button();
            this.chkBoxAcumulaPesquisa = new System.Windows.Forms.CheckBox();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.txtCriterioPesquisa = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.cbTipoPesquisa = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cbCamposPesquisa = new System.Windows.Forms.ComboBox();
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
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.tabControlHelpDesk.SuspendLayout();
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
            this.panel1.Controls.Add(this.lblobsfield);
            this.panel1.Controls.Add(this.tabControlHelpDesk);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 518);
            this.panel1.TabIndex = 0;
            // 
            // lblobsfield
            // 
            this.lblobsfield.AutoSize = true;
            this.lblobsfield.ForeColor = System.Drawing.Color.Blue;
            this.lblobsfield.Location = new System.Drawing.Point(13, 500);
            this.lblobsfield.Name = "lblobsfield";
            this.lblobsfield.Size = new System.Drawing.Size(32, 13);
            this.lblobsfield.TabIndex = 10;
            this.lblobsfield.Text = "Obs.:";
            // 
            // tabControlHelpDesk
            // 
            this.tabControlHelpDesk.Controls.Add(this.tabPage1);
            this.tabControlHelpDesk.Controls.Add(this.tabPage3);
            this.tabControlHelpDesk.Location = new System.Drawing.Point(12, 88);
            this.tabControlHelpDesk.Name = "tabControlHelpDesk";
            this.tabControlHelpDesk.SelectedIndex = 0;
            this.tabControlHelpDesk.Size = new System.Drawing.Size(523, 404);
            this.tabControlHelpDesk.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtCodAtendimento);
            this.tabPage1.Controls.Add(this.maskDtaPrev);
            this.tabPage1.Controls.Add(this.txtnomeSolicitante);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.cbFuncionario);
            this.tabPage1.Controls.Add(this.btnCadDeparta);
            this.tabPage1.Controls.Add(this.btnTipoSolicitante);
            this.tabPage1.Controls.Add(this.bntCadSituacao);
            this.tabPage1.Controls.Add(this.txtSolucao);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.cbPrioridade);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.cbDepartamento);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cbStatus);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDataInc);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtTelefoneSolicitante);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtEmailSolicitante);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cbTipoSolicitante);
            this.tabPage1.Controls.Add(this.txtSolicitacao);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(515, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dados Cadastrais";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 85;
            this.label13.Text = "Cod. Atendimento:";
            // 
            // txtCodAtendimento
            // 
            this.txtCodAtendimento.Location = new System.Drawing.Point(11, 34);
            this.txtCodAtendimento.MaxLength = 100;
            this.txtCodAtendimento.Name = "txtCodAtendimento";
            this.txtCodAtendimento.Size = new System.Drawing.Size(102, 20);
            this.txtCodAtendimento.TabIndex = 1;
            this.txtCodAtendimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodAtendimento_KeyDown);
            this.txtCodAtendimento.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txtCodAtendimento.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodAtendimento_Validating);
            // 
            // maskDtaPrev
            // 
            this.maskDtaPrev.Location = new System.Drawing.Point(202, 34);
            this.maskDtaPrev.Mask = "00/00/0000";
            this.maskDtaPrev.Name = "maskDtaPrev";
            this.maskDtaPrev.Size = new System.Drawing.Size(77, 20);
            this.maskDtaPrev.TabIndex = 2;
            this.maskDtaPrev.ValidatingType = typeof(System.DateTime);
            this.maskDtaPrev.Validating += new System.ComponentModel.CancelEventHandler(this.maskDtaPrev_Validating);
            // 
            // txtnomeSolicitante
            // 
            this.txtnomeSolicitante.Location = new System.Drawing.Point(11, 153);
            this.txtnomeSolicitante.MaxLength = 100;
            this.txtnomeSolicitante.Name = "txtnomeSolicitante";
            this.txtnomeSolicitante.Size = new System.Drawing.Size(232, 20);
            this.txtnomeSolicitante.TabIndex = 7;
            this.txtnomeSolicitante.Validated += new System.EventHandler(this.txtnomeSolicitante_Validated);
            this.txtnomeSolicitante.Validating += new System.ComponentModel.CancelEventHandler(this.txtnomeSolicitante_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 13);
            this.label11.TabIndex = 84;
            this.label11.Text = "Nome do Solicitante:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(269, 111);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(234, 21);
            this.cbFuncionario.TabIndex = 6;
            // 
            // btnCadDeparta
            // 
            this.btnCadDeparta.Location = new System.Drawing.Point(327, 71);
            this.btnCadDeparta.Name = "btnCadDeparta";
            this.btnCadDeparta.Size = new System.Drawing.Size(31, 23);
            this.btnCadDeparta.TabIndex = 81;
            this.btnCadDeparta.UseVisualStyleBackColor = true;
            this.btnCadDeparta.Click += new System.EventHandler(this.btnCadDeparta_Click);
            // 
            // btnTipoSolicitante
            // 
            this.btnTipoSolicitante.Location = new System.Drawing.Point(232, 109);
            this.btnTipoSolicitante.Name = "btnTipoSolicitante";
            this.btnTipoSolicitante.Size = new System.Drawing.Size(31, 23);
            this.btnTipoSolicitante.TabIndex = 80;
            this.btnTipoSolicitante.UseVisualStyleBackColor = true;
            this.btnTipoSolicitante.Click += new System.EventHandler(this.btnTipoSolicitante_Click);
            // 
            // bntCadSituacao
            // 
            this.bntCadSituacao.Location = new System.Drawing.Point(472, 151);
            this.bntCadSituacao.Name = "bntCadSituacao";
            this.bntCadSituacao.Size = new System.Drawing.Size(31, 23);
            this.bntCadSituacao.TabIndex = 79;
            this.bntCadSituacao.UseVisualStyleBackColor = true;
            this.bntCadSituacao.Click += new System.EventHandler(this.bntCadSituacao_Click);
            // 
            // txtSolucao
            // 
            this.txtSolucao.Location = new System.Drawing.Point(11, 309);
            this.txtSolucao.MaxLength = 500;
            this.txtSolucao.Multiline = true;
            this.txtSolucao.Name = "txtSolucao";
            this.txtSolucao.Size = new System.Drawing.Size(492, 58);
            this.txtSolucao.TabIndex = 12;
            this.txtSolucao.Tag = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 293);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Solução:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(364, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Prioridade:";
            // 
            // cbPrioridade
            // 
            this.cbPrioridade.FormattingEnabled = true;
            this.cbPrioridade.Location = new System.Drawing.Point(367, 75);
            this.cbPrioridade.Name = "cbPrioridade";
            this.cbPrioridade.Size = new System.Drawing.Size(136, 21);
            this.cbPrioridade.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Departamento:";
            // 
            // cbDepartamento
            // 
            this.cbDepartamento.FormattingEnabled = true;
            this.cbDepartamento.Location = new System.Drawing.Point(11, 73);
            this.cbDepartamento.Name = "cbDepartamento";
            this.cbDepartamento.Size = new System.Drawing.Size(310, 21);
            this.cbDepartamento.TabIndex = 3;
            this.cbDepartamento.Enter += new System.EventHandler(this.cbDepartamento_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Situação:";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(249, 153);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(217, 21);
            this.cbStatus.TabIndex = 8;
            this.cbStatus.Enter += new System.EventHandler(this.cbStatus_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data:";
            // 
            // txtDataInc
            // 
            this.txtDataInc.Enabled = false;
            this.txtDataInc.Location = new System.Drawing.Point(119, 34);
            this.txtDataInc.MaxLength = 100;
            this.txtDataInc.Name = "txtDataInc";
            this.txtDataInc.Size = new System.Drawing.Size(77, 20);
            this.txtDataInc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Previsão:";
            // 
            // txtTelefoneSolicitante
            // 
            this.txtTelefoneSolicitante.Location = new System.Drawing.Point(11, 192);
            this.txtTelefoneSolicitante.MaxLength = 100;
            this.txtTelefoneSolicitante.Name = "txtTelefoneSolicitante";
            this.txtTelefoneSolicitante.Size = new System.Drawing.Size(232, 20);
            this.txtTelefoneSolicitante.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Telefone do Solicitante:";
            // 
            // txtEmailSolicitante
            // 
            this.txtEmailSolicitante.Location = new System.Drawing.Point(249, 192);
            this.txtEmailSolicitante.MaxLength = 100;
            this.txtEmailSolicitante.Name = "txtEmailSolicitante";
            this.txtEmailSolicitante.Size = new System.Drawing.Size(254, 20);
            this.txtEmailSolicitante.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Email do Solicitante:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Funcionário:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tipo de Solicitante:";
            // 
            // cbTipoSolicitante
            // 
            this.cbTipoSolicitante.FormattingEnabled = true;
            this.cbTipoSolicitante.Location = new System.Drawing.Point(11, 111);
            this.cbTipoSolicitante.Name = "cbTipoSolicitante";
            this.cbTipoSolicitante.Size = new System.Drawing.Size(215, 21);
            this.cbTipoSolicitante.TabIndex = 5;
            this.cbTipoSolicitante.Enter += new System.EventHandler(this.cbTipoSolicitante_Enter);
            // 
            // txtSolicitacao
            // 
            this.txtSolicitacao.Location = new System.Drawing.Point(11, 231);
            this.txtSolicitacao.MaxLength = 500;
            this.txtSolicitacao.Multiline = true;
            this.txtSolicitacao.Name = "txtSolicitacao";
            this.txtSolicitacao.Size = new System.Drawing.Size(492, 59);
            this.txtSolicitacao.TabIndex = 11;
            this.txtSolicitacao.Tag = "";
            this.txtSolicitacao.Validated += new System.EventHandler(this.txtSolicitacao_Validated);
            this.txtSolicitacao.Validating += new System.ComponentModel.CancelEventHandler(this.txtSolicitacao_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Solicitação:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblTotalPesquisa);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.DataGriewDados);
            this.tabPage3.Controls.Add(this.btnLimpaPesquisa);
            this.tabPage3.Controls.Add(this.chkBoxAcumulaPesquisa);
            this.tabPage3.Controls.Add(this.btnPesquisa);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.txtCriterioPesquisa);
            this.tabPage3.Controls.Add(this.label35);
            this.tabPage3.Controls.Add(this.cbTipoPesquisa);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.cbCamposPesquisa);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(515, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pesquisa";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(105, 356);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 42;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 356);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 41;
            this.label33.Text = "Total da pesquisa:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDHELPDESK,
            this.DATAINCLUSAO,
            this.DATASOLUCAO,
            this.TIPOSOLICITANTE,
            this.NOMEDEPARTAMENTO,
            this.NOMESTATUS,
            this.NOMEPRIORIDADE,
            this.NOMEFUNCIONARIO,
            this.NOMESOLICITANTE});
            this.DataGriewDados.Location = new System.Drawing.Point(6, 100);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(503, 250);
            this.DataGriewDados.TabIndex = 40;
            this.DataGriewDados.Enter += new System.EventHandler(this.DataGriewDados_Enter);
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            this.DataGriewDados.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGriewDados_ColumnHeaderMouseClick);
            this.DataGriewDados.Leave += new System.EventHandler(this.DataGriewDados_Leave);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewDados_KeyDown);
            // 
            // IDHELPDESK
            // 
            this.IDHELPDESK.DataPropertyName = "IDHELPDESK";
            this.IDHELPDESK.HeaderText = "Cod. Atendimento";
            this.IDHELPDESK.Name = "IDHELPDESK";
            this.IDHELPDESK.ReadOnly = true;
            // 
            // DATAINCLUSAO
            // 
            this.DATAINCLUSAO.DataPropertyName = "DATAINCLUSAO";
            this.DATAINCLUSAO.HeaderText = "Inclusão";
            this.DATAINCLUSAO.Name = "DATAINCLUSAO";
            this.DATAINCLUSAO.ReadOnly = true;
            // 
            // DATASOLUCAO
            // 
            this.DATASOLUCAO.DataPropertyName = "DATASOLUCAO";
            this.DATASOLUCAO.HeaderText = "Previsão Solução";
            this.DATASOLUCAO.Name = "DATASOLUCAO";
            this.DATASOLUCAO.ReadOnly = true;
            // 
            // TIPOSOLICITANTE
            // 
            this.TIPOSOLICITANTE.DataPropertyName = "TIPOSOLICITANTE";
            this.TIPOSOLICITANTE.HeaderText = "Tipo Solicitante";
            this.TIPOSOLICITANTE.Name = "TIPOSOLICITANTE";
            this.TIPOSOLICITANTE.ReadOnly = true;
            // 
            // NOMEDEPARTAMENTO
            // 
            this.NOMEDEPARTAMENTO.DataPropertyName = "NOMEDEPARTAMENTO";
            this.NOMEDEPARTAMENTO.HeaderText = "Departamento";
            this.NOMEDEPARTAMENTO.Name = "NOMEDEPARTAMENTO";
            this.NOMEDEPARTAMENTO.ReadOnly = true;
            this.NOMEDEPARTAMENTO.Width = 200;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            // 
            // NOMEPRIORIDADE
            // 
            this.NOMEPRIORIDADE.DataPropertyName = "NOMEPRIORIDADE";
            this.NOMEPRIORIDADE.HeaderText = "Prioridade";
            this.NOMEPRIORIDADE.Name = "NOMEPRIORIDADE";
            this.NOMEPRIORIDADE.ReadOnly = true;
            // 
            // NOMEFUNCIONARIO
            // 
            this.NOMEFUNCIONARIO.DataPropertyName = "NOMEFUNCIONARIO";
            this.NOMEFUNCIONARIO.HeaderText = "Funcionario";
            this.NOMEFUNCIONARIO.Name = "NOMEFUNCIONARIO";
            this.NOMEFUNCIONARIO.ReadOnly = true;
            this.NOMEFUNCIONARIO.Width = 200;
            // 
            // NOMESOLICITANTE
            // 
            this.NOMESOLICITANTE.DataPropertyName = "NOMESOLICITANTE";
            this.NOMESOLICITANTE.HeaderText = "Solicitante";
            this.NOMESOLICITANTE.Name = "NOMESOLICITANTE";
            this.NOMESOLICITANTE.ReadOnly = true;
            this.NOMESOLICITANTE.Width = 200;
            // 
            // btnLimpaPesquisa
            // 
            this.btnLimpaPesquisa.Location = new System.Drawing.Point(403, 71);
            this.btnLimpaPesquisa.Name = "btnLimpaPesquisa";
            this.btnLimpaPesquisa.Size = new System.Drawing.Size(106, 23);
            this.btnLimpaPesquisa.TabIndex = 39;
            this.btnLimpaPesquisa.Text = "Limpa Pesquisa";
            this.btnLimpaPesquisa.UseVisualStyleBackColor = true;
            this.btnLimpaPesquisa.Click += new System.EventHandler(this.btnLimpaPesquisa_Click);
            // 
            // chkBoxAcumulaPesquisa
            // 
            this.chkBoxAcumulaPesquisa.AutoSize = true;
            this.chkBoxAcumulaPesquisa.Location = new System.Drawing.Point(2, 30);
            this.chkBoxAcumulaPesquisa.Name = "chkBoxAcumulaPesquisa";
            this.chkBoxAcumulaPesquisa.Size = new System.Drawing.Size(116, 17);
            this.chkBoxAcumulaPesquisa.TabIndex = 38;
            this.chkBoxAcumulaPesquisa.Text = "Acumular Pesquisa";
            this.chkBoxAcumulaPesquisa.UseVisualStyleBackColor = true;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(322, 71);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 37;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(2, 57);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(103, 13);
            this.label36.TabIndex = 36;
            this.label36.Text = "Critério de Pesquisa:";
            // 
            // txtCriterioPesquisa
            // 
            this.txtCriterioPesquisa.Location = new System.Drawing.Point(5, 74);
            this.txtCriterioPesquisa.Name = "txtCriterioPesquisa";
            this.txtCriterioPesquisa.Size = new System.Drawing.Size(311, 20);
            this.txtCriterioPesquisa.TabIndex = 35;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(369, 14);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(92, 13);
            this.label35.TabIndex = 34;
            this.label35.Text = "Tipo de Pesquisa:";
            // 
            // cbTipoPesquisa
            // 
            this.cbTipoPesquisa.FormattingEnabled = true;
            this.cbTipoPesquisa.Location = new System.Drawing.Point(372, 30);
            this.cbTipoPesquisa.Name = "cbTipoPesquisa";
            this.cbTipoPesquisa.Size = new System.Drawing.Size(121, 21);
            this.cbTipoPesquisa.TabIndex = 33;
            this.cbTipoPesquisa.Text = "Campo de Pesquisa";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(121, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(109, 13);
            this.label34.TabIndex = 32;
            this.label34.Text = "Campos de Pesquisa:";
            // 
            // cbCamposPesquisa
            // 
            this.cbCamposPesquisa.FormattingEnabled = true;
            this.cbCamposPesquisa.Location = new System.Drawing.Point(124, 30);
            this.cbCamposPesquisa.Name = "cbCamposPesquisa";
            this.cbCamposPesquisa.Size = new System.Drawing.Size(242, 21);
            this.cbCamposPesquisa.TabIndex = 31;
            this.cbCamposPesquisa.Text = "Campo de Pesquisa";
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
            this.toolStrip1.Size = new System.Drawing.Size(546, 51);
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
            this.menuStrip1.Size = new System.Drawing.Size(546, 24);
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
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // FrmHelpDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 518);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmHelpDesk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Desk";
            this.Load += new System.EventHandler(this.FrmHelpDesk_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmHelpDesk_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHelpDesk_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlHelpDesk.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControlHelpDesk;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtSolicitacao;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDataInc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTipoSolicitante;
        private System.Windows.Forms.TextBox txtEmailSolicitante;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTelefoneSolicitante;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPrioridade;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbDepartamento;
        private System.Windows.Forms.TextBox txtSolucao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTipoSolicitante;
        private System.Windows.Forms.Button bntCadSituacao;
        private System.Windows.Forms.Button btnCadDeparta;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.TextBox txtnomeSolicitante;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkBoxAcumulaPesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtCriterioPesquisa;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cbTipoPesquisa;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbCamposPesquisa;
        private System.Windows.Forms.Button btnLimpaPesquisa;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.MaskedTextBox maskDtaPrev;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCodAtendimento;
        private System.Windows.Forms.Label lblobsfield;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDHELPDESK;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAINCLUSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATASOLUCAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPOSOLICITANTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEDEPARTAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRIORIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFUNCIONARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESOLICITANTE;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}