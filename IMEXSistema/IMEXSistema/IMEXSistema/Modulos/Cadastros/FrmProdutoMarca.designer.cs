namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmProdutoMarca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProdutoMarca));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.chkFiscal = new System.Windows.Forms.CheckBox();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPRODUTOFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODBARRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCALIZACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATACADASTRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUEATUAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEMARCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMarca
            // 
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(15, 25);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(349, 21);
            this.cbMarca.TabIndex = 83;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 84;
            this.label10.Text = "Marca:";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(15, 49);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 85;
            this.btnPesquisa.Text = "&Consultar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(96, 49);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 86;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(177, 49);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 87;
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
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.CODPRODUTOFORNECEDOR,
            this.CODBARRA,
            this.LOCALIZACAO,
            this.DATACADASTRO,
            this.VALORVENDA1,
            this.ESTOQUEATUAL,
            this.Column1,
            this.Column2,
            this.NOMEMARCA});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 78);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.Size = new System.Drawing.Size(826, 294);
            this.DataGriewDados.TabIndex = 88;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(110, 380);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 90;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(12, 380);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 13);
            this.label45.TabIndex = 89;
            this.label45.Text = "Total da pesquisa:";
            // 
            // chkFiscal
            // 
            this.chkFiscal.AutoSize = true;
            this.chkFiscal.Location = new System.Drawing.Point(370, 29);
            this.chkFiscal.Name = "chkFiscal";
            this.chkFiscal.Size = new System.Drawing.Size(53, 17);
            this.chkFiscal.TabIndex = 92;
            this.chkFiscal.Text = "Fiscal";
            this.chkFiscal.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(754, 51);
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
            this.btnExcel.Location = new System.Drawing.Point(785, 51);
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
            this.btnPrint.Location = new System.Drawing.Point(816, 51);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 303;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button1_Click);
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome do Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.Width = 400;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle7;
            this.IDPRODUTO.HeaderText = "Código";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.Width = 50;
            // 
            // CODPRODUTOFORNECEDOR
            // 
            this.CODPRODUTOFORNECEDOR.DataPropertyName = "CODPRODUTOFORNECEDOR";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.CODPRODUTOFORNECEDOR.DefaultCellStyle = dataGridViewCellStyle8;
            this.CODPRODUTOFORNECEDOR.HeaderText = "Cód. Referência";
            this.CODPRODUTOFORNECEDOR.Name = "CODPRODUTOFORNECEDOR";
            this.CODPRODUTOFORNECEDOR.Width = 200;
            // 
            // CODBARRA
            // 
            this.CODBARRA.DataPropertyName = "CODBARRA";
            this.CODBARRA.HeaderText = "Código de Barra";
            this.CODBARRA.Name = "CODBARRA";
            // 
            // LOCALIZACAO
            // 
            this.LOCALIZACAO.DataPropertyName = "LOCALIZACAO";
            this.LOCALIZACAO.HeaderText = "Localização";
            this.LOCALIZACAO.Name = "LOCALIZACAO";
            // 
            // DATACADASTRO
            // 
            this.DATACADASTRO.DataPropertyName = "DATACADASTRO";
            this.DATACADASTRO.HeaderText = "Data Cadastro";
            this.DATACADASTRO.Name = "DATACADASTRO";
            // 
            // VALORVENDA1
            // 
            this.VALORVENDA1.DataPropertyName = "VALORVENDA1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.VALORVENDA1.DefaultCellStyle = dataGridViewCellStyle9;
            this.VALORVENDA1.HeaderText = "Valor de Venda 1";
            this.VALORVENDA1.Name = "VALORVENDA1";
            // 
            // ESTOQUEATUAL
            // 
            this.ESTOQUEATUAL.DataPropertyName = "ESTOQUEATUAL";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.ESTOQUEATUAL.DefaultCellStyle = dataGridViewCellStyle10;
            this.ESTOQUEATUAL.HeaderText = "Estoque Atual";
            this.ESTOQUEATUAL.Name = "ESTOQUEATUAL";
            // 
            // Column1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column1.HeaderText = "Valor de Venda 2";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column2.HeaderText = "Valor de Venda 3";
            this.Column2.Name = "Column2";
            // 
            // NOMEMARCA
            // 
            this.NOMEMARCA.DataPropertyName = "NOMEMARCA";
            this.NOMEMARCA.HeaderText = "Marca";
            this.NOMEMARCA.Name = "NOMEMARCA";
            this.NOMEMARCA.Width = 200;
            // 
            // FrmProdutoMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 414);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chkFiscal);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.label10);
            this.Name = "FrmProdutoMarca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produtos por Marca";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmProdutoGrupoCategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.CheckBox chkFiscal;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPRODUTOFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODBARRA;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCALIZACAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATACADASTRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUEATUAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEMARCA;
    }
}