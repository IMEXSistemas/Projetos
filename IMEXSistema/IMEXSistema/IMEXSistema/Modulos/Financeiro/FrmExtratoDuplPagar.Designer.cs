namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmExtratoDuplPagar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExtratoDuplPagar));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalSelecionado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBaixaLote = new System.Windows.Forms.Button();
            this.btnBaixaParcial = new System.Windows.Forms.Button();
            this.btnBaixa = new System.Windows.Forms.Button();
            this.lblTotalRecebido = new System.Windows.Forms.Label();
            this.lblTotalDevedor = new System.Windows.Forms.Label();
            this.lblTotalDuplicata = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpaPesquisa = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.dataGridDuplicatas = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVECTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDUPLICATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDEVEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORPAGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIASATRASO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTodas = new System.Windows.Forms.RadioButton();
            this.rbPagas = new System.Windows.Forms.RadioButton();
            this.rbVencer = new System.Windows.Forms.RadioButton();
            this.rbVencidas = new System.Windows.Forms.RadioButton();
            this.lblObsField = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFornecedor = new System.Windows.Forms.ComboBox();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplicatas)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnpdf);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtTotalSelecionado);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnBaixaLote);
            this.panel1.Controls.Add(this.btnBaixaParcial);
            this.panel1.Controls.Add(this.btnBaixa);
            this.panel1.Controls.Add(this.lblTotalRecebido);
            this.panel1.Controls.Add(this.lblTotalDevedor);
            this.panel1.Controls.Add(this.lblTotalDuplicata);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnLimpaPesquisa);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.dataGridDuplicatas);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbFornecedor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 472);
            this.panel1.TabIndex = 0;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(814, 78);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 305;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(843, 78);
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
            this.btnPrint.Location = new System.Drawing.Point(872, 78);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 303;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(525, 389);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 250;
            this.label9.Text = "Total Selecionado:";
            // 
            // txtTotalSelecionado
            // 
            this.txtTotalSelecionado.Location = new System.Drawing.Point(528, 405);
            this.txtTotalSelecionado.Name = "txtTotalSelecionado";
            this.txtTotalSelecionado.ReadOnly = true;
            this.txtTotalSelecionado.Size = new System.Drawing.Size(93, 20);
            this.txtTotalSelecionado.TabIndex = 249;
            this.txtTotalSelecionado.Text = "0,00";
            this.txtTotalSelecionado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(701, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 190;
            this.label8.Text = "PAGO PARCIAL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(654, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 189;
            this.label7.Text = "PAGO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(590, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 188;
            this.label6.Text = "ABERTO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(525, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 187;
            this.label5.Text = "VENCIDA";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(255, 61);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 180;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(677, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 179;
            this.button1.Text = "&Estorna Duplicata";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnBaixaLote
            // 
            this.btnBaixaLote.Location = new System.Drawing.Point(571, 60);
            this.btnBaixaLote.Name = "btnBaixaLote";
            this.btnBaixaLote.Size = new System.Drawing.Size(100, 23);
            this.btnBaixaLote.TabIndex = 176;
            this.btnBaixaLote.Text = "Baixa em L&ote";
            this.btnBaixaLote.UseVisualStyleBackColor = true;
            this.btnBaixaLote.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnBaixaParcial
            // 
            this.btnBaixaParcial.Location = new System.Drawing.Point(465, 60);
            this.btnBaixaParcial.Name = "btnBaixaParcial";
            this.btnBaixaParcial.Size = new System.Drawing.Size(100, 23);
            this.btnBaixaParcial.TabIndex = 175;
            this.btnBaixaParcial.Text = "Baixa Pa&rcial";
            this.btnBaixaParcial.UseVisualStyleBackColor = true;
            this.btnBaixaParcial.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBaixa
            // 
            this.btnBaixa.Location = new System.Drawing.Point(359, 60);
            this.btnBaixa.Name = "btnBaixa";
            this.btnBaixa.Size = new System.Drawing.Size(100, 23);
            this.btnBaixa.TabIndex = 174;
            this.btnBaixa.Text = "&Baixa Total";
            this.btnBaixa.UseVisualStyleBackColor = true;
            this.btnBaixa.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTotalRecebido
            // 
            this.lblTotalRecebido.AutoSize = true;
            this.lblTotalRecebido.Location = new System.Drawing.Point(101, 420);
            this.lblTotalRecebido.Name = "lblTotalRecebido";
            this.lblTotalRecebido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalRecebido.Size = new System.Drawing.Size(28, 13);
            this.lblTotalRecebido.TabIndex = 173;
            this.lblTotalRecebido.Text = "0,00";
            this.lblTotalRecebido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalDevedor
            // 
            this.lblTotalDevedor.AutoSize = true;
            this.lblTotalDevedor.Location = new System.Drawing.Point(100, 398);
            this.lblTotalDevedor.Name = "lblTotalDevedor";
            this.lblTotalDevedor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalDevedor.Size = new System.Drawing.Size(28, 13);
            this.lblTotalDevedor.TabIndex = 172;
            this.lblTotalDevedor.Text = "0,00";
            this.lblTotalDevedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalDuplicata
            // 
            this.lblTotalDuplicata.AutoSize = true;
            this.lblTotalDuplicata.Location = new System.Drawing.Point(100, 376);
            this.lblTotalDuplicata.Name = "lblTotalDuplicata";
            this.lblTotalDuplicata.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalDuplicata.Size = new System.Drawing.Size(28, 13);
            this.lblTotalDuplicata.TabIndex = 171;
            this.lblTotalDuplicata.Text = "0,00";
            this.lblTotalDuplicata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 170;
            this.label4.Text = "Total Recebido:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 169;
            this.label3.Text = "Total Devedor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 168;
            this.label1.Text = "Total Duplicata:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(174, 61);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 167;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnLimpaPesquisa
            // 
            this.btnLimpaPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpaPesquisa.Image")));
            this.btnLimpaPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpaPesquisa.Location = new System.Drawing.Point(93, 61);
            this.btnLimpaPesquisa.Name = "btnLimpaPesquisa";
            this.btnLimpaPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnLimpaPesquisa.TabIndex = 166;
            this.btnLimpaPesquisa.Text = "&Limpar";
            this.btnLimpaPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpaPesquisa.UseVisualStyleBackColor = true;
            this.btnLimpaPesquisa.Click += new System.EventHandler(this.btnLimpa_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(12, 61);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // dataGridDuplicatas
            // 
            this.dataGridDuplicatas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDuplicatas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.NUMERO,
            this.DATAVECTO,
            this.DATAPAGTO,
            this.VALORDUPLICATA,
            this.VALORDEVEDOR,
            this.VALORPAGO,
            this.NOMESTATUS,
            this.DIASATRASO});
            this.dataGridDuplicatas.Location = new System.Drawing.Point(12, 107);
            this.dataGridDuplicatas.Name = "dataGridDuplicatas";
            this.dataGridDuplicatas.ReadOnly = true;
            this.dataGridDuplicatas.Size = new System.Drawing.Size(885, 256);
            this.dataGridDuplicatas.TabIndex = 164;
            this.dataGridDuplicatas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDuplicatas_CellContentClick);
            this.dataGridDuplicatas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDuplicatas_CellEnter);
            this.dataGridDuplicatas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridDuplicatas_CellFormatting);
            this.dataGridDuplicatas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridDuplicatas_ColumnHeaderMouseClick);
            this.dataGridDuplicatas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridDuplicatas_KeyDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Baixa";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // NUMERO
            // 
            this.NUMERO.DataPropertyName = "NUMERO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NUMERO.DefaultCellStyle = dataGridViewCellStyle1;
            this.NUMERO.HeaderText = "Duplicata";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.ReadOnly = true;
            // 
            // DATAVECTO
            // 
            this.DATAVECTO.DataPropertyName = "DATAVECTO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.DATAVECTO.DefaultCellStyle = dataGridViewCellStyle2;
            this.DATAVECTO.HeaderText = "Dt. Vecto";
            this.DATAVECTO.Name = "DATAVECTO";
            this.DATAVECTO.ReadOnly = true;
            this.DATAVECTO.Width = 80;
            // 
            // DATAPAGTO
            // 
            this.DATAPAGTO.DataPropertyName = "DATAPAGTO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.DATAPAGTO.DefaultCellStyle = dataGridViewCellStyle3;
            this.DATAPAGTO.HeaderText = "Dt Pagto";
            this.DATAPAGTO.Name = "DATAPAGTO";
            this.DATAPAGTO.ReadOnly = true;
            this.DATAPAGTO.Width = 80;
            // 
            // VALORDUPLICATA
            // 
            this.VALORDUPLICATA.DataPropertyName = "VALORDUPLICATA";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.VALORDUPLICATA.DefaultCellStyle = dataGridViewCellStyle4;
            this.VALORDUPLICATA.HeaderText = "Vl. Duplicata";
            this.VALORDUPLICATA.Name = "VALORDUPLICATA";
            this.VALORDUPLICATA.ReadOnly = true;
            // 
            // VALORDEVEDOR
            // 
            this.VALORDEVEDOR.DataPropertyName = "VALORDEVEDOR";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.VALORDEVEDOR.DefaultCellStyle = dataGridViewCellStyle5;
            this.VALORDEVEDOR.HeaderText = "Vl. Devedor";
            this.VALORDEVEDOR.Name = "VALORDEVEDOR";
            this.VALORDEVEDOR.ReadOnly = true;
            // 
            // VALORPAGO
            // 
            this.VALORPAGO.DataPropertyName = "VALORPAGO";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.VALORPAGO.DefaultCellStyle = dataGridViewCellStyle6;
            this.VALORPAGO.HeaderText = "Vl. Recebido";
            this.VALORPAGO.Name = "VALORPAGO";
            this.VALORPAGO.ReadOnly = true;
            this.VALORPAGO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            // 
            // DIASATRASO
            // 
            this.DIASATRASO.DataPropertyName = "DIASATRASO";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DIASATRASO.DefaultCellStyle = dataGridViewCellStyle7;
            this.DIASATRASO.HeaderText = "Atraso";
            this.DIASATRASO.Name = "DIASATRASO";
            this.DIASATRASO.ReadOnly = true;
            this.DIASATRASO.Width = 70;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTodas);
            this.groupBox1.Controls.Add(this.rbPagas);
            this.groupBox1.Controls.Add(this.rbVencer);
            this.groupBox1.Controls.Add(this.rbVencidas);
            this.groupBox1.Location = new System.Drawing.Point(359, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 42);
            this.groupBox1.TabIndex = 163;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status da Duplicatas";
            // 
            // rbTodas
            // 
            this.rbTodas.AutoSize = true;
            this.rbTodas.Location = new System.Drawing.Point(296, 18);
            this.rbTodas.Name = "rbTodas";
            this.rbTodas.Size = new System.Drawing.Size(55, 17);
            this.rbTodas.TabIndex = 174;
            this.rbTodas.Text = "Todas";
            this.rbTodas.UseVisualStyleBackColor = true;
            // 
            // rbPagas
            // 
            this.rbPagas.AutoSize = true;
            this.rbPagas.Location = new System.Drawing.Point(206, 18);
            this.rbPagas.Name = "rbPagas";
            this.rbPagas.Size = new System.Drawing.Size(55, 17);
            this.rbPagas.TabIndex = 173;
            this.rbPagas.Text = "Pagas";
            this.rbPagas.UseVisualStyleBackColor = true;
            // 
            // rbVencer
            // 
            this.rbVencer.AutoSize = true;
            this.rbVencer.Location = new System.Drawing.Point(115, 18);
            this.rbVencer.Name = "rbVencer";
            this.rbVencer.Size = new System.Drawing.Size(69, 17);
            this.rbVencer.TabIndex = 172;
            this.rbVencer.Text = "A Vencer";
            this.rbVencer.UseVisualStyleBackColor = true;
            // 
            // rbVencidas
            // 
            this.rbVencidas.AutoSize = true;
            this.rbVencidas.Checked = true;
            this.rbVencidas.Location = new System.Drawing.Point(18, 18);
            this.rbVencidas.Name = "rbVencidas";
            this.rbVencidas.Size = new System.Drawing.Size(69, 17);
            this.rbVencidas.TabIndex = 171;
            this.rbVencidas.TabStop = true;
            this.rbVencidas.Text = "Vencidas";
            this.rbVencidas.UseVisualStyleBackColor = true;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(9, 450);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 158;
            this.lblObsField.Text = "Obs.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 157;
            this.label2.Text = "Fornecedor:";
            // 
            // cbFornecedor
            // 
            this.cbFornecedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFornecedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFornecedor.FormattingEnabled = true;
            this.cbFornecedor.Location = new System.Drawing.Point(12, 33);
            this.cbFornecedor.Name = "cbFornecedor";
            this.cbFornecedor.Size = new System.Drawing.Size(341, 21);
            this.cbFornecedor.TabIndex = 0;
            this.cbFornecedor.Enter += new System.EventHandler(this.cbCliente_Enter);
            this.cbFornecedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCliente_KeyDown);
            this.cbFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCliente_KeyPress);
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmExtratoDuplPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 472);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmExtratoDuplPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrato de Contas a Pagar por Fornecedor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmExtratoDuplReceber_FormClosing);
            this.Load += new System.EventHandler(this.FrmExtratoDuplReceber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExtratoDuplReceber_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDuplicatas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbFornecedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpaPesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.DataGridView dataGridDuplicatas;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton rbVencer;
        private System.Windows.Forms.RadioButton rbVencidas;
        private System.Windows.Forms.RadioButton rbTodas;
        private System.Windows.Forms.RadioButton rbPagas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalRecebido;
        private System.Windows.Forms.Label lblTotalDevedor;
        private System.Windows.Forms.Label lblTotalDuplicata;
        private System.Windows.Forms.Button btnBaixaLote;
        private System.Windows.Forms.Button btnBaixaParcial;
        private System.Windows.Forms.Button btnBaixa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVECTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDUPLICATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDEVEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORPAGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIASATRASO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalSelecionado;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}