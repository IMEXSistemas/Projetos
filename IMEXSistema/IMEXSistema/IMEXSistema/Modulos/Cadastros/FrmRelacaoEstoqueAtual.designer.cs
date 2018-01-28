﻿namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmRelacaoEstoqueAtual
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelacaoEstoqueAtual));
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPRODUTOFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEstoqueMaiorqueZero = new System.Windows.Forms.RadioButton();
            this.rbEstoqueZerado = new System.Windows.Forms.RadioButton();
            this.rbEstoqueNegativo = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbGrupoCategoria = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.chkFiscal = new System.Windows.Forms.CheckBox();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.CODPRODUTOFORNECEDOR,
            this.ESTOQUE,
            this.Column1});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 139);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.Size = new System.Drawing.Size(942, 358);
            this.DataGriewDados.TabIndex = 41;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDPRODUTO.HeaderText = "Código";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.Width = 50;
            // 
            // CODPRODUTOFORNECEDOR
            // 
            this.CODPRODUTOFORNECEDOR.DataPropertyName = "CODPRODUTOFORNECEDOR";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.CODPRODUTOFORNECEDOR.DefaultCellStyle = dataGridViewCellStyle2;
            this.CODPRODUTOFORNECEDOR.HeaderText = "Cód. Referência";
            this.CODPRODUTOFORNECEDOR.Name = "CODPRODUTOFORNECEDOR";
            this.CODPRODUTOFORNECEDOR.Width = 200;
            // 
            // ESTOQUE
            // 
            this.ESTOQUE.DataPropertyName = "ESTOQUE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ESTOQUE.DefaultCellStyle = dataGridViewCellStyle3;
            this.ESTOQUE.HeaderText = "Estoque";
            this.ESTOQUE.Name = "ESTOQUE";
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Preço Venda 1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 110;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbEstoqueMaiorqueZero);
            this.groupBox1.Controls.Add(this.rbEstoqueZerado);
            this.groupBox1.Controls.Add(this.rbEstoqueNegativo);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 50);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalhes";
            // 
            // rbEstoqueMaiorqueZero
            // 
            this.rbEstoqueMaiorqueZero.AutoSize = true;
            this.rbEstoqueMaiorqueZero.Location = new System.Drawing.Point(338, 20);
            this.rbEstoqueMaiorqueZero.Name = "rbEstoqueMaiorqueZero";
            this.rbEstoqueMaiorqueZero.Size = new System.Drawing.Size(139, 17);
            this.rbEstoqueMaiorqueZero.TabIndex = 3;
            this.rbEstoqueMaiorqueZero.Text = "Estoque Maior que Zero";
            this.rbEstoqueMaiorqueZero.UseVisualStyleBackColor = true;
            // 
            // rbEstoqueZerado
            // 
            this.rbEstoqueZerado.AutoSize = true;
            this.rbEstoqueZerado.Location = new System.Drawing.Point(219, 20);
            this.rbEstoqueZerado.Name = "rbEstoqueZerado";
            this.rbEstoqueZerado.Size = new System.Drawing.Size(101, 17);
            this.rbEstoqueZerado.TabIndex = 2;
            this.rbEstoqueZerado.Text = "Estoque Zerado";
            this.rbEstoqueZerado.UseVisualStyleBackColor = true;
            // 
            // rbEstoqueNegativo
            // 
            this.rbEstoqueNegativo.AutoSize = true;
            this.rbEstoqueNegativo.Location = new System.Drawing.Point(91, 20);
            this.rbEstoqueNegativo.Name = "rbEstoqueNegativo";
            this.rbEstoqueNegativo.Size = new System.Drawing.Size(110, 17);
            this.rbEstoqueNegativo.TabIndex = 1;
            this.rbEstoqueNegativo.Text = "Estoque Negativo";
            this.rbEstoqueNegativo.UseVisualStyleBackColor = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(18, 20);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(13, 110);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 43;
            this.btnPesquisa.Text = "&Consultar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(94, 110);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 44;
            this.btnPrint.Text = "&Imprimir";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(175, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "&Sair";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbGrupoCategoria
            // 
            this.cbGrupoCategoria.FormattingEnabled = true;
            this.cbGrupoCategoria.Location = new System.Drawing.Point(15, 25);
            this.cbGrupoCategoria.Name = "cbGrupoCategoria";
            this.cbGrupoCategoria.Size = new System.Drawing.Size(312, 21);
            this.cbGrupoCategoria.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Grupo/Categoria:";
            // 
            // cbMarca
            // 
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(334, 24);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(243, 21);
            this.cbMarca.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Marca:";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(110, 507);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 51;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(12, 507);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 13);
            this.label45.TabIndex = 50;
            this.label45.Text = "Total da pesquisa:";
            // 
            // chkFiscal
            // 
            this.chkFiscal.AutoSize = true;
            this.chkFiscal.Location = new System.Drawing.Point(518, 86);
            this.chkFiscal.Name = "chkFiscal";
            this.chkFiscal.Size = new System.Drawing.Size(53, 17);
            this.chkFiscal.TabIndex = 56;
            this.chkFiscal.Text = "Fiscal";
            this.chkFiscal.UseVisualStyleBackColor = true;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(867, 110);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 302;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(898, 110);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 301;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(929, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 300;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmRelacaoEstoqueAtual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 529);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkFiscal);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbGrupoCategoria);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGriewDados);
            this.Name = "FrmRelacaoEstoqueAtual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relação de Estoque Atual";
            this.Load += new System.EventHandler(this.FrmRelacaoEstoqueAtual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEstoqueMaiorqueZero;
        private System.Windows.Forms.RadioButton rbEstoqueZerado;
        private System.Windows.Forms.RadioButton rbEstoqueNegativo;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbGrupoCategoria;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPRODUTOFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.CheckBox chkFiscal;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button button1;
    }
}