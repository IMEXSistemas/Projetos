namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmVendaProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVendaProduto));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.msktDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.msktDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ChkTickePrint = new System.Windows.Forms.CheckBox();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(12, 106);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 292;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bntDateSelecFinal);
            this.groupBox1.Controls.Add(this.bntDateSelecInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.msktDataFinal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.msktDataInicial);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 70);
            this.groupBox1.TabIndex = 291;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(228, 34);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 179;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(109, 34);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 178;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 177;
            this.label1.Text = "Final";
            // 
            // msktDataFinal
            // 
            this.msktDataFinal.Location = new System.Drawing.Point(143, 35);
            this.msktDataFinal.Mask = "00/00/0000";
            this.msktDataFinal.Name = "msktDataFinal";
            this.msktDataFinal.Size = new System.Drawing.Size(79, 20);
            this.msktDataFinal.TabIndex = 176;
            this.msktDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 175;
            this.label4.Text = "Inicial:";
            // 
            // msktDataInicial
            // 
            this.msktDataInicial.Location = new System.Drawing.Point(24, 35);
            this.msktDataInicial.Mask = "00/00/0000";
            this.msktDataInicial.Name = "msktDataInicial";
            this.msktDataInicial.Size = new System.Drawing.Size(79, 20);
            this.msktDataInicial.TabIndex = 174;
            this.msktDataInicial.ValidatingType = typeof(System.DateTime);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(174, 106);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 290;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.descricao,
            this.quantidade,
            this.total,
            this.Column1});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 155);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGriewDados.Size = new System.Drawing.Size(927, 277);
            this.DataGriewDados.TabIndex = 293;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(293, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 295;
            this.label9.Text = "Vendedor:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFuncionario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(296, 28);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(395, 21);
            this.cbFuncionario.TabIndex = 294;
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(296, 68);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(395, 21);
            this.cbProduto.TabIndex = 305;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(293, 52);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(52, 13);
            this.label58.TabIndex = 306;
            this.label58.Text = "Produtos:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(296, 106);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(395, 21);
            this.cbStatus.TabIndex = 309;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(293, 92);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(40, 13);
            this.label52.TabIndex = 310;
            this.label52.Text = "Status:";
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(852, 104);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 315;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(883, 104);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 314;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(914, 104);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 313;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(93, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 316;
            this.button1.Text = "&Imprimir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChkTickePrint
            // 
            this.ChkTickePrint.AutoSize = true;
            this.ChkTickePrint.Checked = true;
            this.ChkTickePrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkTickePrint.Location = new System.Drawing.Point(12, 135);
            this.ChkTickePrint.Name = "ChkTickePrint";
            this.ChkTickePrint.Size = new System.Drawing.Size(124, 17);
            this.ChkTickePrint.TabIndex = 317;
            this.ChkTickePrint.Text = "Impressão em Ticket";
            this.ChkTickePrint.UseVisualStyleBackColor = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Cupom NFCe";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Data";
            this.Column3.Name = "Column3";
            // 
            // descricao
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descricao.DefaultCellStyle = dataGridViewCellStyle1;
            this.descricao.HeaderText = "Produto";
            this.descricao.Name = "descricao";
            this.descricao.Width = 400;
            // 
            // quantidade
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantidade.DefaultCellStyle = dataGridViewCellStyle2;
            this.quantidade.HeaderText = "Quantidade";
            this.quantidade.Name = "quantidade";
            this.quantidade.Width = 80;
            // 
            // total
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.DefaultCellStyle = dataGridViewCellStyle3;
            this.total.HeaderText = "Vl Unt.";
            this.total.Name = "total";
            this.total.Width = 80;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Vl. Total";
            this.Column1.Name = "Column1";
            // 
            // FrmVendaProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 462);
            this.Controls.Add(this.ChkTickePrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbFuncionario);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSair);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmVendaProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumo Diário Caixa (Vendas Por Produto)";
            this.Load += new System.EventHandler(this.FrmVendaProduto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox msktDataFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox msktDataInicial;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ChkTickePrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}