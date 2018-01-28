namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmFluxoContas2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFluxoContas2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDuplicataPaga = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdBCompleto = new System.Windows.Forms.RadioButton();
            this.rdResumo = new System.Windows.Forms.RadioButton();
            this.lblValorSaldo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblValorTotalPagar = new System.Windows.Forms.Label();
            this.lblValorTotalReceber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalreceber = new System.Windows.Forms.Label();
            this.DataGridRelaDupl = new System.Windows.Forms.DataGridView();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVECTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDUPLICATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdPagamento = new System.Windows.Forms.RadioButton();
            this.rbEmissao = new System.Windows.Forms.RadioButton();
            this.rbVencimento = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.mkdatafinal = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.mkDtInicial = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaDupl)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnpdf);
            this.panel1.Controls.Add(this.chkDuplicataPaga);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.lblValorSaldo);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblValorTotalPagar);
            this.panel1.Controls.Add(this.lblValorTotalReceber);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1075, 620);
            this.panel1.TabIndex = 0;
            // 
            // chkDuplicataPaga
            // 
            this.chkDuplicataPaga.AutoSize = true;
            this.chkDuplicataPaga.Location = new System.Drawing.Point(481, 64);
            this.chkDuplicataPaga.Name = "chkDuplicataPaga";
            this.chkDuplicataPaga.Size = new System.Drawing.Size(132, 17);
            this.chkDuplicataPaga.TabIndex = 201;
            this.chkDuplicataPaga.Text = "Exibir Duplicatas Paga";
            this.chkDuplicataPaga.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdBCompleto);
            this.groupBox4.Controls.Add(this.rdResumo);
            this.groupBox4.Location = new System.Drawing.Point(479, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 46);
            this.groupBox4.TabIndex = 200;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Imprimir";
            // 
            // rdBCompleto
            // 
            this.rdBCompleto.AutoSize = true;
            this.rdBCompleto.Location = new System.Drawing.Point(135, 19);
            this.rdBCompleto.Name = "rdBCompleto";
            this.rdBCompleto.Size = new System.Drawing.Size(69, 17);
            this.rdBCompleto.TabIndex = 190;
            this.rdBCompleto.Text = "Completo";
            this.rdBCompleto.UseVisualStyleBackColor = true;
            // 
            // rdResumo
            // 
            this.rdResumo.AutoSize = true;
            this.rdResumo.Checked = true;
            this.rdResumo.Location = new System.Drawing.Point(18, 19);
            this.rdResumo.Name = "rdResumo";
            this.rdResumo.Size = new System.Drawing.Size(64, 17);
            this.rdResumo.TabIndex = 189;
            this.rdResumo.TabStop = true;
            this.rdResumo.Text = "Resumo";
            this.rdResumo.UseVisualStyleBackColor = true;
            // 
            // lblValorSaldo
            // 
            this.lblValorSaldo.AutoSize = true;
            this.lblValorSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorSaldo.Location = new System.Drawing.Point(200, 593);
            this.lblValorSaldo.Name = "lblValorSaldo";
            this.lblValorSaldo.Size = new System.Drawing.Size(28, 13);
            this.lblValorSaldo.TabIndex = 199;
            this.lblValorSaldo.Text = "0,00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(148, 593);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 198;
            this.label8.Text = "Saldo:";
            // 
            // lblValorTotalPagar
            // 
            this.lblValorTotalPagar.AutoSize = true;
            this.lblValorTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotalPagar.Location = new System.Drawing.Point(200, 571);
            this.lblValorTotalPagar.Name = "lblValorTotalPagar";
            this.lblValorTotalPagar.Size = new System.Drawing.Size(28, 13);
            this.lblValorTotalPagar.TabIndex = 197;
            this.lblValorTotalPagar.Text = "0,00";
            // 
            // lblValorTotalReceber
            // 
            this.lblValorTotalReceber.AutoSize = true;
            this.lblValorTotalReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotalReceber.Location = new System.Drawing.Point(200, 551);
            this.lblValorTotalReceber.Name = "lblValorTotalReceber";
            this.lblValorTotalReceber.Size = new System.Drawing.Size(28, 13);
            this.lblValorTotalReceber.TabIndex = 196;
            this.lblValorTotalReceber.Text = "0,00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 571);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 195;
            this.label4.Text = "Total de Contas a Pagar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 551);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 194;
            this.label3.Text = "Total de Contas a Receber:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 193;
            this.label1.Text = "RESUMO";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(12, 77);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 192;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 48);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 191;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(12, 19);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 190;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalreceber);
            this.groupBox1.Controls.Add(this.DataGridRelaDupl);
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1040, 396);
            this.groupBox1.TabIndex = 188;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contas a Receber/ Contas a Pagar";
            // 
            // lblTotalreceber
            // 
            this.lblTotalreceber.AutoSize = true;
            this.lblTotalreceber.Location = new System.Drawing.Point(13, 380);
            this.lblTotalreceber.Name = "lblTotalreceber";
            this.lblTotalreceber.Size = new System.Drawing.Size(100, 13);
            this.lblTotalreceber.TabIndex = 187;
            this.lblTotalreceber.Text = "Total de registros: 0";
            // 
            // DataGridRelaDupl
            // 
            this.DataGridRelaDupl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridRelaDupl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NUMERO,
            this.Column1,
            this.NOMECLIENTE,
            this.DATAEMISSAO,
            this.DATAVECTO,
            this.DATAPAGTO,
            this.VALORDUPLICATA,
            this.Column2,
            this.NOMESTATUS});
            this.DataGridRelaDupl.Location = new System.Drawing.Point(16, 19);
            this.DataGridRelaDupl.Name = "DataGridRelaDupl";
            this.DataGridRelaDupl.ReadOnly = true;
            this.DataGridRelaDupl.Size = new System.Drawing.Size(1011, 346);
            this.DataGridRelaDupl.TabIndex = 186;
            this.DataGridRelaDupl.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridRelaDupl_CellFormatting);
            // 
            // NUMERO
            // 
            this.NUMERO.DataPropertyName = "NUMERO";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NUMERO.DefaultCellStyle = dataGridViewCellStyle11;
            this.NUMERO.HeaderText = "Duplicata";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tipo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente/Fornecedor";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 300;
            // 
            // DATAEMISSAO
            // 
            this.DATAEMISSAO.DataPropertyName = "DATAEMISSAO";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "d";
            dataGridViewCellStyle12.NullValue = null;
            this.DATAEMISSAO.DefaultCellStyle = dataGridViewCellStyle12;
            this.DATAEMISSAO.HeaderText = "Dt. Emissão";
            this.DATAEMISSAO.Name = "DATAEMISSAO";
            this.DATAEMISSAO.ReadOnly = true;
            this.DATAEMISSAO.Width = 90;
            // 
            // DATAVECTO
            // 
            this.DATAVECTO.DataPropertyName = "DATAVECTO";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "d";
            dataGridViewCellStyle13.NullValue = null;
            this.DATAVECTO.DefaultCellStyle = dataGridViewCellStyle13;
            this.DATAVECTO.HeaderText = "Dt. Vecto";
            this.DATAVECTO.Name = "DATAVECTO";
            this.DATAVECTO.ReadOnly = true;
            this.DATAVECTO.Width = 80;
            // 
            // DATAPAGTO
            // 
            this.DATAPAGTO.DataPropertyName = "DATAPAGTO";
            this.DATAPAGTO.HeaderText = "Dt. Pago";
            this.DATAPAGTO.Name = "DATAPAGTO";
            this.DATAPAGTO.ReadOnly = true;
            // 
            // VALORDUPLICATA
            // 
            this.VALORDUPLICATA.DataPropertyName = "VALORDUPLICATA";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.VALORDUPLICATA.DefaultCellStyle = dataGridViewCellStyle14;
            this.VALORDUPLICATA.HeaderText = "Vl. Duplicata";
            this.VALORDUPLICATA.Name = "VALORDUPLICATA";
            this.VALORDUPLICATA.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column2.HeaderText = "Vl. Pago";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdPagamento);
            this.groupBox2.Controls.Add(this.rbEmissao);
            this.groupBox2.Controls.Add(this.rbVencimento);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mkdatafinal);
            this.groupBox2.Controls.Add(this.bntDateSelecFinal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mkDtInicial);
            this.groupBox2.Controls.Add(this.bntDateSelecInicial);
            this.groupBox2.Location = new System.Drawing.Point(93, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 92);
            this.groupBox2.TabIndex = 187;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // rdPagamento
            // 
            this.rdPagamento.AutoSize = true;
            this.rdPagamento.Location = new System.Drawing.Point(250, 25);
            this.rdPagamento.Name = "rdPagamento";
            this.rdPagamento.Size = new System.Drawing.Size(79, 17);
            this.rdPagamento.TabIndex = 190;
            this.rdPagamento.Text = "Pagamento";
            this.rdPagamento.UseVisualStyleBackColor = true;
            // 
            // rbEmissao
            // 
            this.rbEmissao.AutoSize = true;
            this.rbEmissao.Location = new System.Drawing.Point(144, 25);
            this.rbEmissao.Name = "rbEmissao";
            this.rbEmissao.Size = new System.Drawing.Size(64, 17);
            this.rbEmissao.TabIndex = 188;
            this.rbEmissao.Text = "Emissão";
            this.rbEmissao.UseVisualStyleBackColor = true;
            // 
            // rbVencimento
            // 
            this.rbVencimento.AutoSize = true;
            this.rbVencimento.Checked = true;
            this.rbVencimento.Location = new System.Drawing.Point(27, 25);
            this.rbVencimento.Name = "rbVencimento";
            this.rbVencimento.Size = new System.Drawing.Size(81, 17);
            this.rbVencimento.TabIndex = 187;
            this.rbVencimento.TabStop = true;
            this.rbVencimento.Text = "Vencimento";
            this.rbVencimento.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "Final:";
            // 
            // mkdatafinal
            // 
            this.mkdatafinal.Location = new System.Drawing.Point(203, 61);
            this.mkdatafinal.Mask = "00/00/0000";
            this.mkdatafinal.Name = "mkdatafinal";
            this.mkdatafinal.Size = new System.Drawing.Size(79, 20);
            this.mkdatafinal.TabIndex = 184;
            this.mkdatafinal.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(288, 61);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 186;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 182;
            this.label2.Text = "Inicial:";
            // 
            // mkDtInicial
            // 
            this.mkDtInicial.Location = new System.Drawing.Point(86, 61);
            this.mkDtInicial.Mask = "00/00/0000";
            this.mkDtInicial.Name = "mkDtInicial";
            this.mkDtInicial.Size = new System.Drawing.Size(79, 20);
            this.mkDtInicial.TabIndex = 181;
            this.mkDtInicial.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(171, 61);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 183;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(959, 90);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 307;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(990, 89);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 306;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(1021, 89);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 305;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmFluxoContas2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 620);
            this.Controls.Add(this.panel1);
            this.Name = "FrmFluxoContas2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fluxo de Contas Receber/Pagar";
            this.Load += new System.EventHandler(this.FrmFluxoContas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaDupl)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DataGridRelaDupl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mkdatafinal;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mkDtInicial;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.RadioButton rbEmissao;
        private System.Windows.Forms.RadioButton rbVencimento;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTotalreceber;
        private System.Windows.Forms.Label lblValorTotalReceber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValorSaldo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblValorTotalPagar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdBCompleto;
        private System.Windows.Forms.RadioButton rdResumo;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.CheckBox chkDuplicataPaga;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVECTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDUPLICATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.RadioButton rdPagamento;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}