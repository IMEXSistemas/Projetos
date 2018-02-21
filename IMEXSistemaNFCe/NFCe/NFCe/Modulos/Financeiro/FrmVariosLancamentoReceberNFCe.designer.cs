namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmVariosLancamentoReceberNFCe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVariosLancamentoReceberNFCe));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txtNParcelas = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnCadFornecedor = new System.Windows.Forms.Button();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDuplicata = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mkdataInicial = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mkDataVecto = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiasVencimento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnSair = new System.Windows.Forms.Button();
            this.chkVectoFixo = new System.Windows.Forms.CheckBox();
            this.cbLocalCobranca = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtVlPedido = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument6 = new System.Drawing.Printing.PrintDocument();
            this.cbFormaPagto = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridDuplFornecedor = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVECTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDUPLICATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplFornecedor)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(13, 150);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(101, 23);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnLancDupl_Click);
            // 
            // txtNParcelas
            // 
            this.txtNParcelas.Location = new System.Drawing.Point(430, 124);
            this.txtNParcelas.MaxLength = 3;
            this.txtNParcelas.Name = "txtNParcelas";
            this.txtNParcelas.Size = new System.Drawing.Size(64, 20);
            this.txtNParcelas.TabIndex = 6;
            this.txtNParcelas.Text = "1";
            this.txtNParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNParcelas.Validating += new System.ComponentModel.CancelEventHandler(this.txtNParcelas_Validating);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(427, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 13);
            this.label18.TabIndex = 103;
            this.label18.Text = "Nº Parcelas:";
            // 
            // btnCadFornecedor
            // 
            this.btnCadFornecedor.FlatAppearance.BorderSize = 0;
            this.btnCadFornecedor.Location = new System.Drawing.Point(354, 31);
            this.btnCadFornecedor.Name = "btnCadFornecedor";
            this.btnCadFornecedor.Size = new System.Drawing.Size(26, 21);
            this.btnCadFornecedor.TabIndex = 112;
            this.btnCadFornecedor.UseVisualStyleBackColor = true;
            this.btnCadFornecedor.Visible = false;
            this.btnCadFornecedor.Click += new System.EventHandler(this.btnCadFornecedor_Click);
            // 
            // cbCliente
            // 
            this.cbCliente.Enabled = false;
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(12, 32);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(336, 21);
            this.cbCliente.TabIndex = 0;
            this.cbCliente.Enter += new System.EventHandler(this.cbFornecedor_Enter);
            this.cbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbFornecedor_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Cliente :";
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(10, 499);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 113;
            this.lblObsField.Text = "Obs.:";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(117, 477);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 115;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 477);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 114;
            this.label33.Text = "Total da pesquisa:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDuplicata
            // 
            this.txtDuplicata.Location = new System.Drawing.Point(13, 124);
            this.txtDuplicata.MaxLength = 20;
            this.txtDuplicata.Name = "txtDuplicata";
            this.txtDuplicata.ReadOnly = true;
            this.txtDuplicata.Size = new System.Drawing.Size(143, 20);
            this.txtDuplicata.TabIndex = 2;
            this.txtDuplicata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 184;
            this.label1.Text = "Nº Duplicata:";
            // 
            // mkdataInicial
            // 
            this.mkdataInicial.Location = new System.Drawing.Point(253, 124);
            this.mkdataInicial.Mask = "00/00/0000";
            this.mkdataInicial.Name = "mkdataInicial";
            this.mkdataInicial.ReadOnly = true;
            this.mkdataInicial.Size = new System.Drawing.Size(79, 20);
            this.mkdataInicial.TabIndex = 4;
            this.mkdataInicial.ValidatingType = typeof(System.DateTime);
            this.mkdataInicial.Validating += new System.ComponentModel.CancelEventHandler(this.mkdataInicial_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 187;
            this.label2.Text = "Emissão:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(531, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Campo obrigatório";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(60, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 189;
            this.label4.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(85, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 190;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(305, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 13);
            this.label7.TabIndex = 191;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(398, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 196;
            this.label8.Text = "*";
            // 
            // mkDataVecto
            // 
            this.mkDataVecto.Location = new System.Drawing.Point(341, 124);
            this.mkDataVecto.Mask = "00/00/0000";
            this.mkDataVecto.Name = "mkDataVecto";
            this.mkDataVecto.Size = new System.Drawing.Size(79, 20);
            this.mkDataVecto.TabIndex = 5;
            this.mkDataVecto.ValidatingType = typeof(System.DateTime);
            this.mkDataVecto.Enter += new System.EventHandler(this.mkDataVecto_Enter);
            this.mkDataVecto.Validating += new System.ComponentModel.CancelEventHandler(this.mkDataVecto_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(338, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 195;
            this.label9.Text = "1º Vecto:";
            // 
            // txtDiasVencimento
            // 
            this.txtDiasVencimento.Location = new System.Drawing.Point(502, 124);
            this.txtDiasVencimento.MaxLength = 3;
            this.txtDiasVencimento.Name = "txtDiasVencimento";
            this.txtDiasVencimento.Size = new System.Drawing.Size(64, 20);
            this.txtDiasVencimento.TabIndex = 7;
            this.txtDiasVencimento.Text = "30";
            this.txtDiasVencimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiasVencimento.Enter += new System.EventHandler(this.txtDiasVencimento_Enter);
            this.txtDiasVencimento.Validating += new System.ComponentModel.CancelEventHandler(this.txtDiasVencimento_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(499, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 198;
            this.label10.Text = "Dias Intervalo:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 181);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(132, 13);
            this.linkLabel1.TabIndex = 200;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Excluir todas as duplicatas";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(256, 150);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(106, 23);
            this.btnSair.TabIndex = 201;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // chkVectoFixo
            // 
            this.chkVectoFixo.AutoSize = true;
            this.chkVectoFixo.Location = new System.Drawing.Point(499, 180);
            this.chkVectoFixo.Name = "chkVectoFixo";
            this.chkVectoFixo.Size = new System.Drawing.Size(107, 17);
            this.chkVectoFixo.TabIndex = 202;
            this.chkVectoFixo.Text = "Vencimento Fixo:";
            this.chkVectoFixo.UseVisualStyleBackColor = true;
            this.chkVectoFixo.Click += new System.EventHandler(this.chkVectoFixo_Click);
            // 
            // cbLocalCobranca
            // 
            this.cbLocalCobranca.FormattingEnabled = true;
            this.cbLocalCobranca.Location = new System.Drawing.Point(12, 72);
            this.cbLocalCobranca.Name = "cbLocalCobranca";
            this.cbLocalCobranca.Size = new System.Drawing.Size(334, 21);
            this.cbLocalCobranca.TabIndex = 204;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 205;
            this.label12.Text = "Local de Cobrança:";
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(352, 72);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(254, 21);
            this.cbTipo.TabIndex = 207;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(349, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 208;
            this.label13.Text = "Tipo:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(227, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 13);
            this.label11.TabIndex = 199;
            this.label11.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(155, 108);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 104;
            this.label19.Text = "Valor Cupom:";
            // 
            // txtVlPedido
            // 
            this.txtVlPedido.Location = new System.Drawing.Point(158, 124);
            this.txtVlPedido.MaxLength = 20;
            this.txtVlPedido.Name = "txtVlPedido";
            this.txtVlPedido.ReadOnly = true;
            this.txtVlPedido.Size = new System.Drawing.Size(88, 20);
            this.txtVlPedido.TabIndex = 3;
            this.txtVlPedido.Text = "0,00";
            this.txtVlPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVlPedido.Validating += new System.ComponentModel.CancelEventHandler(this.txtVlParcelas_Validating);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(120, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 23);
            this.button2.TabIndex = 210;
            this.button2.Text = "&Imprimir Duplicata";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            // printDocument6
            // 
            this.printDocument6.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument6_PrintPage);
            // 
            // cbFormaPagto
            // 
            this.cbFormaPagto.FormattingEnabled = true;
            this.cbFormaPagto.Location = new System.Drawing.Point(392, 33);
            this.cbFormaPagto.Name = "cbFormaPagto";
            this.cbFormaPagto.Size = new System.Drawing.Size(214, 21);
            this.cbFormaPagto.TabIndex = 211;
            this.cbFormaPagto.SelectionChangeCommitted += new System.EventHandler(this.cbFormaPagto_SelectionChangeCommitted);
            this.cbFormaPagto.SelectedValueChanged += new System.EventHandler(this.cbFormaPagto_SelectedValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(389, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 212;
            this.label14.Text = "Condições de Pagamento:";
            // 
            // dataGridDuplFornecedor
            // 
            this.dataGridDuplFornecedor.AllowUserToAddRows = false;
            this.dataGridDuplFornecedor.AllowUserToDeleteRows = false;
            this.dataGridDuplFornecedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDuplFornecedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDuplFornecedor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn5,
            this.NUMERO,
            this.DATAVECTO,
            this.DATAEMISSAO,
            this.VALORDUPLICATA});
            this.dataGridDuplFornecedor.Location = new System.Drawing.Point(13, 203);
            this.dataGridDuplFornecedor.Name = "dataGridDuplFornecedor";
            this.dataGridDuplFornecedor.Size = new System.Drawing.Size(610, 258);
            this.dataGridDuplFornecedor.TabIndex = 310;
            this.dataGridDuplFornecedor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgProdMTQ_CellDoubleClick);
            this.dataGridDuplFornecedor.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgProdMTQ_CellEndEdit);
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "Excluir";
            this.dataGridViewImageColumn5.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn5.Image")));
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            this.dataGridViewImageColumn5.Width = 50;
            // 
            // NUMERO
            // 
            this.NUMERO.DataPropertyName = "NUMERO";
            this.NUMERO.HeaderText = "Duplicata";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.ReadOnly = true;
            // 
            // DATAVECTO
            // 
            this.DATAVECTO.DataPropertyName = "DATAVECTO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DATAVECTO.DefaultCellStyle = dataGridViewCellStyle1;
            this.DATAVECTO.HeaderText = "Dt. Vecto";
            this.DATAVECTO.Name = "DATAVECTO";
            // 
            // DATAEMISSAO
            // 
            this.DATAEMISSAO.DataPropertyName = "DATAEMISSAO";
            this.DATAEMISSAO.HeaderText = "Dt. Emissão";
            this.DATAEMISSAO.Name = "DATAEMISSAO";
            this.DATAEMISSAO.ReadOnly = true;
            // 
            // VALORDUPLICATA
            // 
            this.VALORDUPLICATA.DataPropertyName = "VALORDUPLICATA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.VALORDUPLICATA.DefaultCellStyle = dataGridViewCellStyle2;
            this.VALORDUPLICATA.HeaderText = "Vl .Duplicata";
            this.VALORDUPLICATA.Name = "VALORDUPLICATA";
            this.VALORDUPLICATA.ReadOnly = true;
            this.VALORDUPLICATA.Width = 200;
            // 
            // FrmVariosLancamentoReceberNFCe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 525);
            this.Controls.Add(this.dataGridDuplFornecedor);
            this.Controls.Add(this.cbFormaPagto);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cbTipo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbLocalCobranca);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chkVectoFixo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDiasVencimento);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mkDataVecto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mkdataInicial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDuplicata);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblObsField);
            this.Controls.Add(this.btnCadFornecedor);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtVlPedido);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtNParcelas);
            this.Controls.Add(this.label18);
            this.Name = "FrmVariosLancamentoReceberNFCe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lançamento Duplicatas a Receber";
            this.Load += new System.EventHandler(this.FrmVariosLancamento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplFornecedor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtNParcelas;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnCadFornecedor;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtDuplicata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mkdataInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox mkDataVecto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiasVencimento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.CheckBox chkVectoFixo;
        private System.Windows.Forms.ComboBox cbLocalCobranca;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVlPedido;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument6;
        private System.Windows.Forms.ComboBox cbFormaPagto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridDuplFornecedor;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVECTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDUPLICATA;
    }
}