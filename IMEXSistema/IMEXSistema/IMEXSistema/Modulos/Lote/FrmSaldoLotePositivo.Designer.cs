namespace BmsSoftware.Modulos.Lote
{
    partial class FrmSaldoLotePositivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaldoLotePositivo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.CODLOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAVALIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(12, 12);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 360;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(174, 12);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 359;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(93, 12);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 358;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(835, 12);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 364;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(866, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 363;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(897, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 362;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CODLOTE,
            this.DATAVALIDADE,
            this.QUANTIDADE,
            this.NOMEPRODUTO,
            this.IDPRODUTO});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 41);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(914, 383);
            this.DataGriewDados.TabIndex = 361;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(103, 434);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 367;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(9, 434);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 366;
            this.label33.Text = "Total da pesquisa:";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTIDADE.DefaultCellStyle = dataGridViewCellStyle1;
            this.QUANTIDADE.HeaderText = "Saldo";
            this.QUANTIDADE.Name = "QUANTIDADE";
            this.QUANTIDADE.ReadOnly = true;
            this.QUANTIDADE.Width = 150;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.NOMEPRODUTO.DefaultCellStyle = dataGridViewCellStyle2;
            this.NOMEPRODUTO.HeaderText = "Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 400;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            this.IDPRODUTO.HeaderText = "Cód. Produto";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.ReadOnly = true;
            // 
            // FrmSaldoLotePositivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 463);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Name = "FrmSaldoLotePositivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lotes com Saldo Positivo";
            this.Load += new System.EventHandler(this.FrmSaldoLotePositivo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODLOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAVALIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
    }
}