namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchProduto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtNomePesquisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.NOMEPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEUNIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORVENDA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODBARRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTotalPesquisa);
            this.panel1.Controls.Add(this.label45);
            this.panel1.Controls.Add(this.DataGriewDados);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.txtNomePesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 434);
            this.panel1.TabIndex = 0;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(107, 412);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 102;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(9, 412);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 13);
            this.label45.TabIndex = 101;
            this.label45.Text = "Total da pesquisa:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEPRODUTO,
            this.IDPRODUTO,
            this.NOMEUNIDADE,
            this.VALORVENDA1,
            this.CODBARRA});
            this.DataGriewDados.Location = new System.Drawing.Point(12, 87);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(900, 322);
            this.DataGriewDados.TabIndex = 43;
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewDados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewSearch_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(672, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisa.Location = new System.Drawing.Point(591, 46);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtNomePesquisa
            // 
            this.txtNomePesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomePesquisa.Location = new System.Drawing.Point(12, 49);
            this.txtNomePesquisa.Name = "txtNomePesquisa";
            this.txtNomePesquisa.Size = new System.Drawing.Size(573, 20);
            this.txtNomePesquisa.TabIndex = 0;
            this.txtNomePesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyDown);
            this.txtNomePesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCriterioPesquisa_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pesquisa:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // NOMEPRODUTO
            // 
            this.NOMEPRODUTO.DataPropertyName = "NOMEPRODUTO";
            this.NOMEPRODUTO.HeaderText = "Nome";
            this.NOMEPRODUTO.Name = "NOMEPRODUTO";
            this.NOMEPRODUTO.ReadOnly = true;
            this.NOMEPRODUTO.Width = 400;
            // 
            // IDPRODUTO
            // 
            this.IDPRODUTO.DataPropertyName = "IDPRODUTO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDPRODUTO.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDPRODUTO.HeaderText = "Código";
            this.IDPRODUTO.Name = "IDPRODUTO";
            this.IDPRODUTO.ReadOnly = true;
            this.IDPRODUTO.Width = 80;
            // 
            // NOMEUNIDADE
            // 
            this.NOMEUNIDADE.DataPropertyName = "NOMEUNIDADE";
            this.NOMEUNIDADE.HeaderText = "Und";
            this.NOMEUNIDADE.Name = "NOMEUNIDADE";
            this.NOMEUNIDADE.ReadOnly = true;
            this.NOMEUNIDADE.Width = 70;
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
            // CODBARRA
            // 
            this.CODBARRA.DataPropertyName = "CODBARRA";
            this.CODBARRA.HeaderText = "Cód. de Barra";
            this.CODBARRA.Name = "CODBARRA";
            this.CODBARRA.ReadOnly = true;
            this.CODBARRA.Width = 200;
            // 
            // FrmSearchProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 434);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Produto";
            this.Load += new System.EventHandler(this.FrmSearchFornecedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearchFornecedor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomePesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEUNIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORVENDA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODBARRA;
    }
}