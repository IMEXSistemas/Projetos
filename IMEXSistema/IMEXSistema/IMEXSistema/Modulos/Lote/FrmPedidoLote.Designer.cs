namespace BmsSoftware.Modulos.Lote
{
    partial class FrmPedidoLote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedidoLote));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMERODOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGTIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODLOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVALIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGATIVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.DATA,
            this.NUMERODOC,
            this.FLAGTIPO,
            this.CODLOTE,
            this.DATAVALIDADE,
            this.QUANTIDADE,
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.FLAGATIVO});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 41);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(1012, 427);
            this.DataGriewDados.TabIndex = 1;
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(106, 475);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 4;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(12, 475);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 3;
            this.label33.Text = "Total da pesquisa:";
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(936, 12);
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
            this.btnExcel.Location = new System.Drawing.Point(967, 12);
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
            this.btnPrint.Location = new System.Drawing.Point(998, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 303;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdicionar.Image")));
            this.btnAdicionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionar.Location = new System.Drawing.Point(15, 12);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 362;
            this.btnAdicionar.Text = "&Adicionar";
            this.btnAdicionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(96, 12);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 361;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(796, 22);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 363;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Excluir todos os produtos";
            this.linkLabel1.DoubleClick += new System.EventHandler(this.linkLabel1_DoubleClick);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Excluir";
            this.Column4.Image = ((System.Drawing.Image)(resources.GetObject("Column4.Image")));
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 50;
            // 
            // DATA
            // 
            this.DATA.DataPropertyName = "DATA";
            this.DATA.HeaderText = "Data";
            this.DATA.Name = "DATA";
            this.DATA.ReadOnly = true;
            // 
            // NUMERODOC
            // 
            this.NUMERODOC.DataPropertyName = "NUMERODOC";
            this.NUMERODOC.HeaderText = "Nº Doc";
            this.NUMERODOC.Name = "NUMERODOC";
            this.NUMERODOC.ReadOnly = true;
            // 
            // FLAGTIPO
            // 
            this.FLAGTIPO.DataPropertyName = "FLAGTIPO";
            this.FLAGTIPO.HeaderText = "Tipo";
            this.FLAGTIPO.Name = "FLAGTIPO";
            this.FLAGTIPO.ReadOnly = true;
            this.FLAGTIPO.Width = 50;
            // 
            // CODLOTE
            // 
            this.CODLOTE.DataPropertyName = "CODLOTE";
            this.CODLOTE.HeaderText = "Lote";
            this.CODLOTE.Name = "CODLOTE";
            this.CODLOTE.ReadOnly = true;
            // 
            // DATAVALIDADE
            // 
            this.DATAVALIDADE.DataPropertyName = "DATAVALIDADE";
            this.DATAVALIDADE.HeaderText = "Validade";
            this.DATAVALIDADE.Name = "DATAVALIDADE";
            this.DATAVALIDADE.ReadOnly = true;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.DataPropertyName = "QUANTIDADE";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTIDADE.DefaultCellStyle = dataGridViewCellStyle10;
            this.QUANTIDADE.HeaderText = "Quant.";
            this.QUANTIDADE.Name = "QUANTIDADE";
            this.QUANTIDADE.ReadOnly = true;
            this.QUANTIDADE.Width = 80;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.NOMEPRODUTO.DefaultCellStyle = dataGridViewCellStyle11;
            this.NOMEPRODUTO.HeaderText = "Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 300;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle12;
            this.IDPRODUTO.HeaderText = "Cód. Produto";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.ReadOnly = true;
            // 
            // FLAGATIVO
            // 
            this.FLAGATIVO.DataPropertyName = "FLAGATIVO";
            this.FLAGATIVO.HeaderText = "Ativo";
            this.FLAGATIVO.Name = "FLAGATIVO";
            this.FLAGATIVO.ReadOnly = true;
            this.FLAGATIVO.Width = 50;
            // 
            // FrmPedidoLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 497);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.DataGriewDados);
            this.Name = "FrmPedidoLote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lote Por Pedido";
            this.Load += new System.EventHandler(this.FrmPedidoLote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERODOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGTIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODLOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVALIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGATIVO;
    }
}