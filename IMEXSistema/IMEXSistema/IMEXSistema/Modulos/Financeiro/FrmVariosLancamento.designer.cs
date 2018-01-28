namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmVariosLancamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVariosLancamento));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dataGridDuplFornecedor = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOMEFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVECTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDUPLICATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CENTROCUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtVlParcelas = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNParcelas = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnCadFornecedor = new System.Windows.Forms.Button();
            this.cbFornecedor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.btnCadCentroCusto = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.cbCentroCusto = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDuplicata = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mkdataInicial = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnlimpa = new System.Windows.Forms.Button();
            this.mkDataVecto = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiasVencimento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnSair = new System.Windows.Forms.Button();
            this.chkVectoFixo = new System.Windows.Forms.CheckBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplFornecedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(13, 103);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Lançar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnLancDupl_Click);
            // 
            // dataGridDuplFornecedor
            // 
            this.dataGridDuplFornecedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDuplFornecedor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.NOMEFORNECEDOR,
            this.NUMERO,
            this.DATAEMISSAO,
            this.DATAVECTO,
            this.VALORDUPLICATA,
            this.CENTROCUSTO});
            this.dataGridDuplFornecedor.Location = new System.Drawing.Point(12, 149);
            this.dataGridDuplFornecedor.Name = "dataGridDuplFornecedor";
            this.dataGridDuplFornecedor.ReadOnly = true;
            this.dataGridDuplFornecedor.Size = new System.Drawing.Size(1059, 272);
            this.dataGridDuplFornecedor.TabIndex = 107;
            this.dataGridDuplFornecedor.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridDuplFornecedor_CellMouseClick);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Excluir";
            this.Column2.Image = ((System.Drawing.Image)(resources.GetObject("Column2.Image")));
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Editar";
            this.Column1.Image = ((System.Drawing.Image)(resources.GetObject("Column1.Image")));
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // NOMEFORNECEDOR
            // 
            this.NOMEFORNECEDOR.DataPropertyName = "NOMEFORNECEDOR";
            this.NOMEFORNECEDOR.HeaderText = "Fornecedor";
            this.NOMEFORNECEDOR.Name = "NOMEFORNECEDOR";
            this.NOMEFORNECEDOR.ReadOnly = true;
            this.NOMEFORNECEDOR.Width = 300;
            // 
            // NUMERO
            // 
            this.NUMERO.DataPropertyName = "NUMERO";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NUMERO.DefaultCellStyle = dataGridViewCellStyle9;
            this.NUMERO.HeaderText = "Duplicata";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.ReadOnly = true;
            // 
            // DATAEMISSAO
            // 
            this.DATAEMISSAO.DataPropertyName = "DATAEMISSAO";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.DATAEMISSAO.DefaultCellStyle = dataGridViewCellStyle10;
            this.DATAEMISSAO.HeaderText = "Emissão";
            this.DATAEMISSAO.Name = "DATAEMISSAO";
            this.DATAEMISSAO.ReadOnly = true;
            // 
            // DATAVECTO
            // 
            this.DATAVECTO.DataPropertyName = "DATAVECTO";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.DATAVECTO.DefaultCellStyle = dataGridViewCellStyle11;
            this.DATAVECTO.HeaderText = "Vecto";
            this.DATAVECTO.Name = "DATAVECTO";
            this.DATAVECTO.ReadOnly = true;
            // 
            // VALORDUPLICATA
            // 
            this.VALORDUPLICATA.DataPropertyName = "VALORDUPLICATA";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.VALORDUPLICATA.DefaultCellStyle = dataGridViewCellStyle12;
            this.VALORDUPLICATA.HeaderText = "Valor";
            this.VALORDUPLICATA.Name = "VALORDUPLICATA";
            this.VALORDUPLICATA.ReadOnly = true;
            // 
            // CENTROCUSTO
            // 
            this.CENTROCUSTO.DataPropertyName = "CENTROCUSTO";
            this.CENTROCUSTO.HeaderText = "Centro de Custo";
            this.CENTROCUSTO.Name = "CENTROCUSTO";
            this.CENTROCUSTO.ReadOnly = true;
            this.CENTROCUSTO.Width = 200;
            // 
            // txtVlParcelas
            // 
            this.txtVlParcelas.Location = new System.Drawing.Point(158, 77);
            this.txtVlParcelas.MaxLength = 20;
            this.txtVlParcelas.Name = "txtVlParcelas";
            this.txtVlParcelas.Size = new System.Drawing.Size(88, 20);
            this.txtVlParcelas.TabIndex = 3;
            this.txtVlParcelas.Text = "0,00";
            this.txtVlParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVlParcelas.Validating += new System.ComponentModel.CancelEventHandler(this.txtVlParcelas_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(155, 61);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 13);
            this.label19.TabIndex = 104;
            this.label19.Text = "Vl. Parcelas:";
            // 
            // txtNParcelas
            // 
            this.txtNParcelas.Location = new System.Drawing.Point(430, 77);
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
            this.label18.Location = new System.Drawing.Point(427, 61);
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
            this.btnCadFornecedor.Click += new System.EventHandler(this.btnCadFornecedor_Click);
            // 
            // cbFornecedor
            // 
            this.cbFornecedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFornecedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFornecedor.FormattingEnabled = true;
            this.cbFornecedor.Location = new System.Drawing.Point(12, 32);
            this.cbFornecedor.Name = "cbFornecedor";
            this.cbFornecedor.Size = new System.Drawing.Size(336, 21);
            this.cbFornecedor.TabIndex = 0;
            this.cbFornecedor.Enter += new System.EventHandler(this.cbFornecedor_Enter);
            this.cbFornecedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbFornecedor_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Fornecedor :";
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(10, 448);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 113;
            this.lblObsField.Text = "Obs.:";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(110, 426);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 115;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 426);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 114;
            this.label33.Text = "Total da pesquisa:";
            // 
            // btnCadCentroCusto
            // 
            this.btnCadCentroCusto.FlatAppearance.BorderSize = 0;
            this.btnCadCentroCusto.Location = new System.Drawing.Point(689, 33);
            this.btnCadCentroCusto.Name = "btnCadCentroCusto";
            this.btnCadCentroCusto.Size = new System.Drawing.Size(26, 21);
            this.btnCadCentroCusto.TabIndex = 118;
            this.btnCadCentroCusto.UseVisualStyleBackColor = true;
            this.btnCadCentroCusto.Click += new System.EventHandler(this.btnCadCentroCusto_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(384, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 13);
            this.label22.TabIndex = 117;
            this.label22.Text = "Centro de Custos:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.FormattingEnabled = true;
            this.cbCentroCusto.Location = new System.Drawing.Point(386, 33);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(297, 21);
            this.cbCentroCusto.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDuplicata
            // 
            this.txtDuplicata.Location = new System.Drawing.Point(13, 77);
            this.txtDuplicata.MaxLength = 20;
            this.txtDuplicata.Name = "txtDuplicata";
            this.txtDuplicata.Size = new System.Drawing.Size(143, 20);
            this.txtDuplicata.TabIndex = 2;
            this.txtDuplicata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 184;
            this.label1.Text = "Nº Duplicata:";
            // 
            // mkdataInicial
            // 
            this.mkdataInicial.Location = new System.Drawing.Point(253, 77);
            this.mkdataInicial.Mask = "00/00/0000";
            this.mkdataInicial.Name = "mkdataInicial";
            this.mkdataInicial.Size = new System.Drawing.Size(79, 20);
            this.mkdataInicial.TabIndex = 4;
            this.mkdataInicial.ValidatingType = typeof(System.DateTime);
            this.mkdataInicial.Validating += new System.ComponentModel.CancelEventHandler(this.mkdataInicial_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(250, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 187;
            this.label2.Text = "Emissão:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(514, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Campo obrigatório";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 192;
            this.button1.Text = "Extrato de Contas a Pagar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnlimpa
            // 
            this.btnlimpa.Image = ((System.Drawing.Image)(resources.GetObject("btnlimpa.Image")));
            this.btnlimpa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlimpa.Location = new System.Drawing.Point(120, 103);
            this.btnlimpa.Name = "btnlimpa";
            this.btnlimpa.Size = new System.Drawing.Size(101, 23);
            this.btnlimpa.TabIndex = 193;
            this.btnlimpa.Text = "Limpar";
            this.btnlimpa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnlimpa.UseVisualStyleBackColor = true;
            this.btnlimpa.Click += new System.EventHandler(this.button2_Click);
            // 
            // mkDataVecto
            // 
            this.mkDataVecto.Location = new System.Drawing.Point(341, 77);
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
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(338, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 195;
            this.label9.Text = "1º Vecto:";
            // 
            // txtDiasVencimento
            // 
            this.txtDiasVencimento.Location = new System.Drawing.Point(502, 77);
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
            this.label10.Location = new System.Drawing.Point(499, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 198;
            this.label10.Text = "Dias Intervalo:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 133);
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
            this.btnSair.Location = new System.Drawing.Point(380, 103);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(101, 23);
            this.btnSair.TabIndex = 201;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // chkVectoFixo
            // 
            this.chkVectoFixo.AutoSize = true;
            this.chkVectoFixo.Location = new System.Drawing.Point(514, 108);
            this.chkVectoFixo.Name = "chkVectoFixo";
            this.chkVectoFixo.Size = new System.Drawing.Size(107, 17);
            this.chkVectoFixo.TabIndex = 202;
            this.chkVectoFixo.Text = "Vencimento Fixo:";
            this.chkVectoFixo.UseVisualStyleBackColor = true;
            this.chkVectoFixo.Click += new System.EventHandler(this.chkVectoFixo_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(1044, 123);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(27, 23);
            this.btnImprimir.TabIndex = 204;
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(982, 123);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 301;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(1013, 123);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 300;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // FrmVariosLancamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 470);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.chkVectoFixo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtDiasVencimento);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.mkDataVecto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnlimpa);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mkdataInicial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDuplicata);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCadCentroCusto);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cbCentroCusto);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblObsField);
            this.Controls.Add(this.btnCadFornecedor);
            this.Controls.Add(this.cbFornecedor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridDuplFornecedor);
            this.Controls.Add(this.txtVlParcelas);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtNParcelas);
            this.Controls.Add(this.label18);
            this.KeyPreview = true;
            this.Name = "FrmVariosLancamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vários Lançamento a Pagar";
            this.Load += new System.EventHandler(this.FrmVariosLancamento_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmVariosLancamento_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplFornecedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dataGridDuplFornecedor;
        private System.Windows.Forms.TextBox txtVlParcelas;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtNParcelas;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnCadFornecedor;
        private System.Windows.Forms.ComboBox cbFornecedor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btnCadCentroCusto;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cbCentroCusto;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtDuplicata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mkdataInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnlimpa;
        private System.Windows.Forms.MaskedTextBox mkDataVecto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiasVencimento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.CheckBox chkVectoFixo;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVECTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDUPLICATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CENTROCUSTO;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
    }
}