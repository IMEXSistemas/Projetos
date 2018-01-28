namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmEstoqueChapa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstoqueChapa));
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPRODUTOFORNECEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEstoqueMaiorqueZero = new System.Windows.Forms.RadioButton();
            this.rbEstoqueZerado = new System.Windows.Forms.RadioButton();
            this.rbEstoqueNegativo = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.cbGrupoCategoria = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mkstUltimaMovimentacao = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbOrdemNomeProduto = new System.Windows.Forms.RadioButton();
            this.rbOrdemCodigoReferencia = new System.Windows.Forms.RadioButton();
            this.rbOrdemCodigo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDPRODUTO,
            this.CODPRODUTOFORNECEDOR,
            this.NOMEPRODUTO,
            this.Column3,
            this.Column4,
            this.ESTOQUE,
            this.Column2});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 139);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.Size = new System.Drawing.Size(1015, 358);
            this.DataGriewDados.TabIndex = 41;
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
            this.CODPRODUTOFORNECEDOR.Width = 110;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome do Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.Width = 450;
            // 
            // Column3
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "n4";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "Altura";
            this.Column3.Name = "Column3";
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "n4";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Largura";
            this.Column4.Name = "Column4";
            this.Column4.Width = 70;
            // 
            // ESTOQUE
            // 
            this.ESTOQUE.DataPropertyName = "ESTOQUE";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "n4";
            this.ESTOQUE.DefaultCellStyle = dataGridViewCellStyle5;
            this.ESTOQUE.HeaderText = "Estoque MT2";
            this.ESTOQUE.Name = "ESTOQUE";
            // 
            // Column2
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column2.HeaderText = "Estoque Chapa";
            this.Column2.Name = "Column2";
            this.Column2.Width = 110;
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
            this.rbEstoqueMaiorqueZero.Checked = true;
            this.rbEstoqueMaiorqueZero.Location = new System.Drawing.Point(338, 20);
            this.rbEstoqueMaiorqueZero.Name = "rbEstoqueMaiorqueZero";
            this.rbEstoqueMaiorqueZero.Size = new System.Drawing.Size(139, 17);
            this.rbEstoqueMaiorqueZero.TabIndex = 3;
            this.rbEstoqueMaiorqueZero.TabStop = true;
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
            this.rbTodos.Location = new System.Drawing.Point(18, 20);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 0;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(13, 110);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 43;
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(94, 110);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 44;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(175, 110);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 45;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button3_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Última Movimentação:";
            // 
            // mkstUltimaMovimentacao
            // 
            this.mkstUltimaMovimentacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mkstUltimaMovimentacao.Location = new System.Drawing.Point(598, 26);
            this.mkstUltimaMovimentacao.Mask = "00/00/0000";
            this.mkstUltimaMovimentacao.Name = "mkstUltimaMovimentacao";
            this.mkstUltimaMovimentacao.Size = new System.Drawing.Size(109, 20);
            this.mkstUltimaMovimentacao.TabIndex = 53;
            this.mkstUltimaMovimentacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mkstUltimaMovimentacao.ValidatingType = typeof(System.DateTime);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbOrdemNomeProduto);
            this.groupBox3.Controls.Add(this.rbOrdemCodigoReferencia);
            this.groupBox3.Controls.Add(this.rbOrdemCodigo);
            this.groupBox3.Location = new System.Drawing.Point(713, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 41);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ordenar";
            // 
            // rbOrdemNomeProduto
            // 
            this.rbOrdemNomeProduto.AutoSize = true;
            this.rbOrdemNomeProduto.Checked = true;
            this.rbOrdemNomeProduto.Location = new System.Drawing.Point(190, 17);
            this.rbOrdemNomeProduto.Name = "rbOrdemNomeProduto";
            this.rbOrdemNomeProduto.Size = new System.Drawing.Size(93, 17);
            this.rbOrdemNomeProduto.TabIndex = 2;
            this.rbOrdemNomeProduto.TabStop = true;
            this.rbOrdemNomeProduto.Text = "Nome Produto";
            this.rbOrdemNomeProduto.UseVisualStyleBackColor = true;
            // 
            // rbOrdemCodigoReferencia
            // 
            this.rbOrdemCodigoReferencia.AutoSize = true;
            this.rbOrdemCodigoReferencia.Location = new System.Drawing.Point(82, 17);
            this.rbOrdemCodigoReferencia.Name = "rbOrdemCodigoReferencia";
            this.rbOrdemCodigoReferencia.Size = new System.Drawing.Size(102, 17);
            this.rbOrdemCodigoReferencia.TabIndex = 1;
            this.rbOrdemCodigoReferencia.Text = "Cód. Referência";
            this.rbOrdemCodigoReferencia.UseVisualStyleBackColor = true;
            // 
            // rbOrdemCodigo
            // 
            this.rbOrdemCodigo.AutoSize = true;
            this.rbOrdemCodigo.Location = new System.Drawing.Point(18, 17);
            this.rbOrdemCodigo.Name = "rbOrdemCodigo";
            this.rbOrdemCodigo.Size = new System.Drawing.Size(58, 17);
            this.rbOrdemCodigo.TabIndex = 0;
            this.rbOrdemCodigo.Text = "Código";
            this.rbOrdemCodigo.UseVisualStyleBackColor = true;
            // 
            // FrmEstoqueChapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 529);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.mkstUltimaMovimentacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbGrupoCategoria);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGriewDados);
            this.Name = "FrmEstoqueChapa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estoque por Chapa";
            this.Load += new System.EventHandler(this.FrmRelacaoEstoqueAtual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ComboBox cbGrupoCategoria;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mkstUltimaMovimentacao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbOrdemNomeProduto;
        private System.Windows.Forms.RadioButton rbOrdemCodigoReferencia;
        private System.Windows.Forms.RadioButton rbOrdemCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPRODUTOFORNECEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}