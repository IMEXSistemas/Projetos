namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchMaterial
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCadProduto = new System.Windows.Forms.Button();
            this.DataGriewSearch = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtNomePesquisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NOMEMATERIAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDMATERIAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUEATUAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEUNIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCadProduto);
            this.panel1.Controls.Add(this.DataGriewSearch);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.txtNomePesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 256);
            this.panel1.TabIndex = 0;
            // 
            // btnCadProduto
            // 
            this.btnCadProduto.FlatAppearance.BorderSize = 0;
            this.btnCadProduto.Location = new System.Drawing.Point(508, 30);
            this.btnCadProduto.Name = "btnCadProduto";
            this.btnCadProduto.Size = new System.Drawing.Size(26, 21);
            this.btnCadProduto.TabIndex = 96;
            this.btnCadProduto.UseVisualStyleBackColor = true;
            this.btnCadProduto.Click += new System.EventHandler(this.btnCadProduto_Click);
            // 
            // DataGriewSearch
            // 
            this.DataGriewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEMATERIAL,
            this.IDMATERIAL,
            this.VALORVENDA1,
            this.VALORVENDA2,
            this.VALORVENDA3,
            this.ESTOQUEATUAL,
            this.NOMEUNIDADE});
            this.DataGriewSearch.Location = new System.Drawing.Point(12, 56);
            this.DataGriewSearch.Name = "DataGriewSearch";
            this.DataGriewSearch.ReadOnly = true;
            this.DataGriewSearch.Size = new System.Drawing.Size(684, 188);
            this.DataGriewSearch.TabIndex = 43;
            this.DataGriewSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewSearch_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(621, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(540, 27);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtNomePesquisa
            // 
            this.txtNomePesquisa.Location = new System.Drawing.Point(12, 30);
            this.txtNomePesquisa.Name = "txtNomePesquisa";
            this.txtNomePesquisa.Size = new System.Drawing.Size(490, 20);
            this.txtNomePesquisa.TabIndex = 0;
            this.txtNomePesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome:";
            // 
            // NOMEMATERIAL
            // 
            this.NOMEMATERIAL.DataPropertyName = "NOMEMATERIAL";
            this.NOMEMATERIAL.HeaderText = "Nome";
            this.NOMEMATERIAL.Name = "NOMEMATERIAL";
            this.NOMEMATERIAL.ReadOnly = true;
            this.NOMEMATERIAL.Width = 300;
            // 
            // IDMATERIAL
            // 
            this.IDMATERIAL.DataPropertyName = "IDMATERIAL";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDMATERIAL.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDMATERIAL.HeaderText = "Código";
            this.IDMATERIAL.Name = "IDMATERIAL";
            this.IDMATERIAL.ReadOnly = true;
            this.IDMATERIAL.Width = 50;
            // 
            // VALORVENDA1
            // 
            this.VALORVENDA1.DataPropertyName = "VALORVENDA1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.VALORVENDA1.DefaultCellStyle = dataGridViewCellStyle2;
            this.VALORVENDA1.HeaderText = "Valor Venda 1";
            this.VALORVENDA1.Name = "VALORVENDA1";
            this.VALORVENDA1.ReadOnly = true;
            // 
            // VALORVENDA2
            // 
            this.VALORVENDA2.DataPropertyName = "VALORVENDA2";
            this.VALORVENDA2.HeaderText = "Valor Venda 2";
            this.VALORVENDA2.Name = "VALORVENDA2";
            this.VALORVENDA2.ReadOnly = true;
            // 
            // VALORVENDA3
            // 
            this.VALORVENDA3.DataPropertyName = "VALORVENDA3";
            this.VALORVENDA3.HeaderText = "Valor Venda 3";
            this.VALORVENDA3.Name = "VALORVENDA3";
            this.VALORVENDA3.ReadOnly = true;
            // 
            // ESTOQUEATUAL
            // 
            this.ESTOQUEATUAL.DataPropertyName = "ESTOQUEATUAL";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.ESTOQUEATUAL.DefaultCellStyle = dataGridViewCellStyle3;
            this.ESTOQUEATUAL.HeaderText = "Estoque";
            this.ESTOQUEATUAL.Name = "ESTOQUEATUAL";
            this.ESTOQUEATUAL.ReadOnly = true;
            // 
            // NOMEUNIDADE
            // 
            this.NOMEUNIDADE.DataPropertyName = "NOMEUNIDADE";
            this.NOMEUNIDADE.HeaderText = "Und";
            this.NOMEUNIDADE.Name = "NOMEUNIDADE";
            this.NOMEUNIDADE.ReadOnly = true;
            // 
            // FrmSearchMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 256);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Material";
            this.Load += new System.EventHandler(this.FrmSearchFornecedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearchFornecedor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomePesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewSearch;
        private System.Windows.Forms.Button btnCadProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEMATERIAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDMATERIAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUEATUAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEUNIDADE;


    }
}