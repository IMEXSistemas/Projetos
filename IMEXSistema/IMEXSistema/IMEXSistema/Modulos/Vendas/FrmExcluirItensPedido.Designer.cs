namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmExcluirItensPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExcluirItensPedido));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGDadosProduto = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.FLAGEXIBIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DADOSADICIONAIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomecor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalProduto = new System.Windows.Forms.TextBox();
            this.lblNumProdutos = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.chkCodRef = new System.Windows.Forms.CheckBox();
            this.chkPesqCodBarra = new System.Windows.Forms.CheckBox();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosProduto)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDadosProduto
            // 
            this.DGDadosProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDadosProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDadosProduto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.FLAGEXIBIR,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.DADOSADICIONAIS,
            this.nomecor});
            this.DGDadosProduto.Location = new System.Drawing.Point(12, 54);
            this.DGDadosProduto.Name = "DGDadosProduto";
            this.DGDadosProduto.ReadOnly = true;
            this.DGDadosProduto.Size = new System.Drawing.Size(868, 331);
            this.DGDadosProduto.TabIndex = 233;
            this.DGDadosProduto.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDadosProduto_CellDoubleClick);
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Excluir";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 50;
            // 
            // FLAGEXIBIR
            // 
            this.FLAGEXIBIR.DataPropertyName = "FLAGEXIBIR";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAGEXIBIR.DefaultCellStyle = dataGridViewCellStyle1;
            this.FLAGEXIBIR.HeaderText = "Exibir";
            this.FLAGEXIBIR.Name = "FLAGEXIBIR";
            this.FLAGEXIBIR.ReadOnly = true;
            this.FLAGEXIBIR.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NOMEPRODUTO";
            this.dataGridViewTextBoxColumn1.HeaderText = "Produto";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 300;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "QUANTIDADE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn2.HeaderText = "Quant.";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "VALORUNITARIO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "Vl. Unit.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "VALORTOTAL";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "Vl. Total";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // DADOSADICIONAIS
            // 
            this.DADOSADICIONAIS.DataPropertyName = "DADOSADICIONAIS";
            this.DADOSADICIONAIS.HeaderText = "Dados Adicionais Produtos";
            this.DADOSADICIONAIS.Name = "DADOSADICIONAIS";
            this.DADOSADICIONAIS.ReadOnly = true;
            this.DADOSADICIONAIS.Width = 200;
            // 
            // nomecor
            // 
            this.nomecor.DataPropertyName = "NOMECOR";
            this.nomecor.HeaderText = "Cor";
            this.nomecor.Name = "nomecor";
            this.nomecor.ReadOnly = true;
            // 
            // txtTotalProduto
            // 
            this.txtTotalProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalProduto.Location = new System.Drawing.Point(594, 391);
            this.txtTotalProduto.Name = "txtTotalProduto";
            this.txtTotalProduto.ReadOnly = true;
            this.txtTotalProduto.Size = new System.Drawing.Size(78, 20);
            this.txtTotalProduto.TabIndex = 236;
            this.txtTotalProduto.Text = "0,00";
            this.txtTotalProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNumProdutos
            // 
            this.lblNumProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumProdutos.AutoSize = true;
            this.lblNumProdutos.Location = new System.Drawing.Point(12, 394);
            this.lblNumProdutos.Name = "lblNumProdutos";
            this.lblNumProdutos.Size = new System.Drawing.Size(91, 13);
            this.lblNumProdutos.TabIndex = 238;
            this.lblNumProdutos.Text = "Nº de Produtos: 0";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(494, 394);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(94, 13);
            this.label28.TabIndex = 237;
            this.label28.Text = "Total de Produtos:";
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(9, 25);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(442, 21);
            this.cbProduto.TabIndex = 239;
            this.cbProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProduto_KeyDown);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(9, 9);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(52, 13);
            this.label60.TabIndex = 240;
            this.label60.Text = "Produtos:";
            // 
            // chkCodRef
            // 
            this.chkCodRef.AutoSize = true;
            this.chkCodRef.Location = new System.Drawing.Point(594, 27);
            this.chkCodRef.Name = "chkCodRef";
            this.chkCodRef.Size = new System.Drawing.Size(149, 17);
            this.chkCodRef.TabIndex = 344;
            this.chkCodRef.Text = "Pesquisa Cód. Referência";
            this.chkCodRef.UseVisualStyleBackColor = true;
            // 
            // chkPesqCodBarra
            // 
            this.chkPesqCodBarra.AutoSize = true;
            this.chkPesqCodBarra.Checked = true;
            this.chkPesqCodBarra.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPesqCodBarra.Location = new System.Drawing.Point(466, 27);
            this.chkPesqCodBarra.Name = "chkPesqCodBarra";
            this.chkPesqCodBarra.Size = new System.Drawing.Size(122, 17);
            this.chkPesqCodBarra.TabIndex = 343;
            this.chkPesqCodBarra.Text = "Pesquisa Cód. Barra";
            this.chkPesqCodBarra.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(12, 433);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 345;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FrmExcluirItensPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 468);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.chkCodRef);
            this.Controls.Add(this.chkPesqCodBarra);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.txtTotalProduto);
            this.Controls.Add(this.lblNumProdutos);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.DGDadosProduto);
            this.Name = "FrmExcluirItensPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excluir Itens do Pedido";
            this.Load += new System.EventHandler(this.FrmExcluirItensPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDadosProduto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDadosProduto;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGEXIBIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DADOSADICIONAIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomecor;
        private System.Windows.Forms.TextBox txtTotalProduto;
        private System.Windows.Forms.Label lblNumProdutos;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.CheckBox chkCodRef;
        private System.Windows.Forms.CheckBox chkPesqCodBarra;
        private System.Windows.Forms.Button btnSair;
    }
}