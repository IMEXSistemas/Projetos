namespace BmsSoftware.Modulos.Etiqueta
{
    partial class FrmEtiquetaPorProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEtiquetaPorProduto));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCadProdutos = new System.Windows.Forms.Button();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEMARCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODBARRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltTotalRegistros = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCadProdutos
            // 
            this.btnCadProdutos.FlatAppearance.BorderSize = 0;
            this.btnCadProdutos.Location = new System.Drawing.Point(544, 35);
            this.btnCadProdutos.Name = "btnCadProdutos";
            this.btnCadProdutos.Size = new System.Drawing.Size(26, 21);
            this.btnCadProdutos.TabIndex = 201;
            this.btnCadProdutos.UseVisualStyleBackColor = true;
            this.btnCadProdutos.Click += new System.EventHandler(this.btnCadProdutos_Click);
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(12, 35);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(526, 21);
            this.cbProduto.TabIndex = 199;
            this.cbProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProduto_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 200;
            this.label14.Text = "Produtos:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.NOMEPRODUTO,
            this.VALORVENDA1,
            this.NOMEMARCA,
            this.CODBARRA});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 91);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.Size = new System.Drawing.Size(982, 300);
            this.DataGriewDados.TabIndex = 202;
            this.DataGriewDados.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGriewDados_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Excluir";
            this.Column1.Image = ((System.Drawing.Image)(resources.GetObject("Column1.Image")));
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome do Produto";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.Width = 450;
            // 
            // VALORVENDA1
            // 
            this.VALORVENDA1.DataPropertyName = "VALORVENDA1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.VALORVENDA1.DefaultCellStyle = dataGridViewCellStyle1;
            this.VALORVENDA1.HeaderText = "Venda 1";
            this.VALORVENDA1.Name = "VALORVENDA1";
            this.VALORVENDA1.Width = 80;
            // 
            // NOMEMARCA
            // 
            this.NOMEMARCA.DataPropertyName = "NOMEMARCA";
            this.NOMEMARCA.HeaderText = "Marca";
            this.NOMEMARCA.Name = "NOMEMARCA";
            this.NOMEMARCA.Width = 150;
            // 
            // CODBARRA
            // 
            this.CODBARRA.DataPropertyName = "CODBARRA";
            this.CODBARRA.HeaderText = "Cód. Barra";
            this.CODBARRA.Name = "CODBARRA";
            this.CODBARRA.Width = 200;
            // 
            // lbltTotalRegistros
            // 
            this.lbltTotalRegistros.AutoSize = true;
            this.lbltTotalRegistros.Location = new System.Drawing.Point(12, 400);
            this.lbltTotalRegistros.Name = "lbltTotalRegistros";
            this.lbltTotalRegistros.Size = new System.Drawing.Size(105, 13);
            this.lbltTotalRegistros.TabIndex = 203;
            this.lbltTotalRegistros.Text = "Total de Registros: 0";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(12, 62);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 204;
            this.btnAdd.Text = "&Adicionar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(93, 62);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 205;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(174, 62);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 206;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FrmEtiquetaPorProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 422);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbltTotalRegistros);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnCadProdutos);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label14);
            this.Name = "FrmEtiquetaPorProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiqueta por Produto";
            this.Load += new System.EventHandler(this.FrmEtiquetaPorProduto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadProdutos;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lbltTotalRegistros;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEMARCA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODBARRA;
    }
}