namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmFecharVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFecharVenda));
            this.label9 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalVenda = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnReceber = new System.Windows.Forms.Button();
            this.lblNotaFiscal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.cboperadoracartao = new System.Windows.Forms.ComboBox();
            this.txtNumeroAutorizaCartao = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtValoPagoDinheiro = new System.Windows.Forms.TextBox();
            this.txtValoPagoCartaoCredito = new System.Windows.Forms.TextBox();
            this.txtValoPagoValeRefeicao = new System.Windows.Forms.TextBox();
            this.txtValorPagoCheque = new System.Windows.Forms.TextBox();
            this.txtValoPagoCartãoDebito = new System.Windows.Forms.TextBox();
            this.txtValoPagoOutros = new System.Windows.Forms.TextBox();
            this.txtTroco = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCPFCNPJ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtNomeCliente = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 245;
            this.label9.Text = "Vendedor:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFuncionario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFuncionario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(9, 269);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(589, 47);
            this.cbFuncionario.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 247;
            this.label5.Text = "Total Venda:";
            // 
            // txtTotalVenda
            // 
            this.txtTotalVenda.BackColor = System.Drawing.Color.Yellow;
            this.txtTotalVenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVenda.Location = new System.Drawing.Point(9, 25);
            this.txtTotalVenda.MaxLength = 20;
            this.txtTotalVenda.Name = "txtTotalVenda";
            this.txtTotalVenda.ReadOnly = true;
            this.txtTotalVenda.Size = new System.Drawing.Size(158, 47);
            this.txtTotalVenda.TabIndex = 0;
            this.txtTotalVenda.Text = "0,00";
            this.txtTotalVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 319);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 13);
            this.label17.TabIndex = 257;
            this.label17.Text = "Cliente:";
            // 
            // cbCliente
            // 
            this.cbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(9, 334);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(586, 47);
            this.cbCliente.TabIndex = 11;
            this.cbCliente.SelectedValueChanged += new System.EventHandler(this.cbCliente_SelectedValueChanged);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(9, 436);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 15;
            this.btnSair.Text = "F3 - Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnReceber
            // 
            this.btnReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceber.Image = ((System.Drawing.Image)(resources.GetObject("btnReceber.Image")));
            this.btnReceber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceber.Location = new System.Drawing.Point(118, 436);
            this.btnReceber.Name = "btnReceber";
            this.btnReceber.Size = new System.Drawing.Size(153, 40);
            this.btnReceber.TabIndex = 14;
            this.btnReceber.Text = "F8 -Fechar Venda";
            this.btnReceber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReceber.UseVisualStyleBackColor = true;
            this.btnReceber.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblNotaFiscal
            // 
            this.lblNotaFiscal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotaFiscal.AutoSize = true;
            this.lblNotaFiscal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNotaFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotaFiscal.Location = new System.Drawing.Point(496, 29);
            this.lblNotaFiscal.Name = "lblNotaFiscal";
            this.lblNotaFiscal.Size = new System.Drawing.Size(49, 13);
            this.lblNotaFiscal.TabIndex = 266;
            this.lblNotaFiscal.Text = "000000";
            this.lblNotaFiscal.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(448, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 267;
            this.label10.Text = "NFCe:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblData
            // 
            this.lblData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData.AutoSize = true;
            this.lblData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(448, 9);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(34, 13);
            this.lblData.TabIndex = 268;
            this.lblData.Text = "Data";
            this.lblData.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(541, 319);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(57, 13);
            this.linkLabel2.TabIndex = 270;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Duplicatas";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 13);
            this.label11.TabIndex = 272;
            this.label11.Text = "Operadora de Cartão de Crédito/Débito";
            // 
            // cboperadoracartao
            // 
            this.cboperadoracartao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboperadoracartao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboperadoracartao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboperadoracartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboperadoracartao.FormattingEnabled = true;
            this.cboperadoracartao.Items.AddRange(new object[] {
            "Visa",
            "Mastercard",
            "American Express",
            "Sorocred",
            "Outros"});
            this.cboperadoracartao.Location = new System.Drawing.Point(9, 219);
            this.cboperadoracartao.Name = "cboperadoracartao";
            this.cboperadoracartao.Size = new System.Drawing.Size(280, 28);
            this.cboperadoracartao.TabIndex = 8;
            // 
            // txtNumeroAutorizaCartao
            // 
            this.txtNumeroAutorizaCartao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumeroAutorizaCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroAutorizaCartao.Location = new System.Drawing.Point(300, 219);
            this.txtNumeroAutorizaCartao.MaxLength = 20;
            this.txtNumeroAutorizaCartao.Name = "txtNumeroAutorizaCartao";
            this.txtNumeroAutorizaCartao.Size = new System.Drawing.Size(301, 26);
            this.txtNumeroAutorizaCartao.TabIndex = 9;
            this.txtNumeroAutorizaCartao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(297, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 13);
            this.label12.TabIndex = 274;
            this.label12.Text = "Número de Autorização";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 276;
            this.label13.Text = "Dinheiro:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(179, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 278;
            this.label14.Text = "Cartão de Crédito:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(176, 140);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 280;
            this.label15.Text = "Cartão de Débito:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 140);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 282;
            this.label16.Text = "Cheque:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 284;
            this.label6.Text = "Vale Refeição:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(344, 140);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 286;
            this.label18.Text = "Outros:";
            // 
            // txtValoPagoDinheiro
            // 
            this.txtValoPagoDinheiro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValoPagoDinheiro.BackColor = System.Drawing.Color.Yellow;
            this.txtValoPagoDinheiro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValoPagoDinheiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValoPagoDinheiro.Location = new System.Drawing.Point(9, 90);
            this.txtValoPagoDinheiro.MaxLength = 20;
            this.txtValoPagoDinheiro.Name = "txtValoPagoDinheiro";
            this.txtValoPagoDinheiro.Size = new System.Drawing.Size(158, 47);
            this.txtValoPagoDinheiro.TabIndex = 2;
            this.txtValoPagoDinheiro.Text = "00,00";
            this.txtValoPagoDinheiro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValoPagoDinheiro.Leave += new System.EventHandler(this.txtValoPagoDinheiro_Leave_1);
            this.txtValoPagoDinheiro.Validating += new System.ComponentModel.CancelEventHandler(this.txtValoPagoDinheiro_Validating);
            // 
            // txtValoPagoCartaoCredito
            // 
            this.txtValoPagoCartaoCredito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValoPagoCartaoCredito.BackColor = System.Drawing.Color.Yellow;
            this.txtValoPagoCartaoCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValoPagoCartaoCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValoPagoCartaoCredito.Location = new System.Drawing.Point(179, 90);
            this.txtValoPagoCartaoCredito.MaxLength = 20;
            this.txtValoPagoCartaoCredito.Name = "txtValoPagoCartaoCredito";
            this.txtValoPagoCartaoCredito.Size = new System.Drawing.Size(158, 47);
            this.txtValoPagoCartaoCredito.TabIndex = 3;
            this.txtValoPagoCartaoCredito.Text = "00,00";
            this.txtValoPagoCartaoCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValoPagoCartaoCredito.Validating += new System.ComponentModel.CancelEventHandler(this.txtValoPagoCartaoCredito_Validating);
            // 
            // txtValoPagoValeRefeicao
            // 
            this.txtValoPagoValeRefeicao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValoPagoValeRefeicao.BackColor = System.Drawing.Color.Yellow;
            this.txtValoPagoValeRefeicao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValoPagoValeRefeicao.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValoPagoValeRefeicao.Location = new System.Drawing.Point(347, 90);
            this.txtValoPagoValeRefeicao.MaxLength = 20;
            this.txtValoPagoValeRefeicao.Name = "txtValoPagoValeRefeicao";
            this.txtValoPagoValeRefeicao.Size = new System.Drawing.Size(158, 47);
            this.txtValoPagoValeRefeicao.TabIndex = 4;
            this.txtValoPagoValeRefeicao.Text = "00,00";
            this.txtValoPagoValeRefeicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValoPagoValeRefeicao.Validating += new System.ComponentModel.CancelEventHandler(this.txtValoPagoValeRefeicao_Validating);
            // 
            // txtValorPagoCheque
            // 
            this.txtValorPagoCheque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValorPagoCheque.BackColor = System.Drawing.Color.Yellow;
            this.txtValorPagoCheque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValorPagoCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorPagoCheque.Location = new System.Drawing.Point(9, 154);
            this.txtValorPagoCheque.MaxLength = 20;
            this.txtValorPagoCheque.Name = "txtValorPagoCheque";
            this.txtValorPagoCheque.Size = new System.Drawing.Size(158, 47);
            this.txtValorPagoCheque.TabIndex = 5;
            this.txtValorPagoCheque.Text = "00,00";
            this.txtValorPagoCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorPagoCheque.Validating += new System.ComponentModel.CancelEventHandler(this.txtValorPagoCheque_Validating_1);
            // 
            // txtValoPagoCartãoDebito
            // 
            this.txtValoPagoCartãoDebito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValoPagoCartãoDebito.BackColor = System.Drawing.Color.Yellow;
            this.txtValoPagoCartãoDebito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValoPagoCartãoDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValoPagoCartãoDebito.Location = new System.Drawing.Point(179, 154);
            this.txtValoPagoCartãoDebito.MaxLength = 20;
            this.txtValoPagoCartãoDebito.Name = "txtValoPagoCartãoDebito";
            this.txtValoPagoCartãoDebito.Size = new System.Drawing.Size(158, 47);
            this.txtValoPagoCartãoDebito.TabIndex = 6;
            this.txtValoPagoCartãoDebito.Text = "00,00";
            this.txtValoPagoCartãoDebito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValoPagoCartãoDebito.Validating += new System.ComponentModel.CancelEventHandler(this.txtValoPagoCartãoDebito_Validating_1);
            // 
            // txtValoPagoOutros
            // 
            this.txtValoPagoOutros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValoPagoOutros.BackColor = System.Drawing.Color.Yellow;
            this.txtValoPagoOutros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValoPagoOutros.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValoPagoOutros.Location = new System.Drawing.Point(347, 154);
            this.txtValoPagoOutros.MaxLength = 20;
            this.txtValoPagoOutros.Name = "txtValoPagoOutros";
            this.txtValoPagoOutros.Size = new System.Drawing.Size(158, 47);
            this.txtValoPagoOutros.TabIndex = 7;
            this.txtValoPagoOutros.Text = "00,00";
            this.txtValoPagoOutros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValoPagoOutros.Validating += new System.ComponentModel.CancelEventHandler(this.txtValoPagoOutros_Validating);
            // 
            // txtTroco
            // 
            this.txtTroco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTroco.BackColor = System.Drawing.Color.Yellow;
            this.txtTroco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTroco.Location = new System.Drawing.Point(179, 25);
            this.txtTroco.MaxLength = 20;
            this.txtTroco.Name = "txtTroco";
            this.txtTroco.ReadOnly = true;
            this.txtTroco.Size = new System.Drawing.Size(158, 47);
            this.txtTroco.TabIndex = 1;
            this.txtTroco.Text = "00,00";
            this.txtTroco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 288;
            this.label1.Text = "Troco:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 387);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 290;
            this.label2.Text = "CPF/CNPJ do Cliente:";
            // 
            // txtCPFCNPJ
            // 
            this.txtCPFCNPJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCPFCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPFCNPJ.Location = new System.Drawing.Point(402, 402);
            this.txtCPFCNPJ.MaxLength = 20;
            this.txtCPFCNPJ.Name = "txtCPFCNPJ";
            this.txtCPFCNPJ.Size = new System.Drawing.Size(193, 26);
            this.txtCPFCNPJ.TabIndex = 13;
            this.txtCPFCNPJ.Leave += new System.EventHandler(this.txtCPFCNPJ_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 387);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 292;
            this.label3.Text = "Nome do Cliente:";
            // 
            // TxtNomeCliente
            // 
            this.TxtNomeCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNomeCliente.Location = new System.Drawing.Point(9, 402);
            this.TxtNomeCliente.MaxLength = 60;
            this.TxtNomeCliente.Name = "TxtNomeCliente";
            this.TxtNomeCliente.Size = new System.Drawing.Size(387, 26);
            this.TxtNomeCliente.TabIndex = 12;
            // 
            // FrmFecharVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 481);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtNomeCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCPFCNPJ);
            this.Controls.Add(this.txtTroco);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValoPagoOutros);
            this.Controls.Add(this.txtValoPagoCartãoDebito);
            this.Controls.Add(this.txtValorPagoCheque);
            this.Controls.Add(this.txtValoPagoValeRefeicao);
            this.Controls.Add(this.txtValoPagoCartaoCredito);
            this.Controls.Add(this.txtValoPagoDinheiro);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtNumeroAutorizaCartao);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboperadoracartao);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblNotaFiscal);
            this.Controls.Add(this.btnReceber);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTotalVenda);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbFuncionario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmFecharVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fechar Venda";
            this.Load += new System.EventHandler(this.FrmFecharVenda_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFecharVenda_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalVenda;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Button btnReceber;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNotaFiscal;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumeroAutorizaCartao;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboperadoracartao;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtValoPagoOutros;
        private System.Windows.Forms.TextBox txtValoPagoCartãoDebito;
        private System.Windows.Forms.TextBox txtValorPagoCheque;
        private System.Windows.Forms.TextBox txtValoPagoValeRefeicao;
        private System.Windows.Forms.TextBox txtValoPagoCartaoCredito;
        private System.Windows.Forms.TextBox txtValoPagoDinheiro;
        private System.Windows.Forms.TextBox txtTroco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCPFCNPJ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtNomeCliente;
    }
}