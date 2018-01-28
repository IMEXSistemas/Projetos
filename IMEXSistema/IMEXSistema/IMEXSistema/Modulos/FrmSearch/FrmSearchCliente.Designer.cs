namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchCliente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblObsField = new System.Windows.Forms.Label();
            this.btnCadFornecedor = new System.Windows.Forms.Button();
            this.DataGriewSearch = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtNomePesquisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ENDERECO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMEROENDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMPLEMENTO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bairro1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefone1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefone2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MUNICIPIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.btnCadFornecedor);
            this.panel1.Controls.Add(this.DataGriewSearch);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtNomePesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 445);
            this.panel1.TabIndex = 0;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(12, 423);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 159;
            this.lblObsField.Text = "Obs.:";
            // 
            // btnCadFornecedor
            // 
            this.btnCadFornecedor.FlatAppearance.BorderSize = 0;
            this.btnCadFornecedor.Location = new System.Drawing.Point(611, 30);
            this.btnCadFornecedor.Name = "btnCadFornecedor";
            this.btnCadFornecedor.Size = new System.Drawing.Size(26, 21);
            this.btnCadFornecedor.TabIndex = 96;
            this.btnCadFornecedor.UseVisualStyleBackColor = true;
            this.btnCadFornecedor.Click += new System.EventHandler(this.btnCadFornecedor_Click);
            // 
            // DataGriewSearch
            // 
            this.DataGriewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOME,
            this.IDCLIENTE,
            this.ENDERECO1,
            this.NUMEROENDER,
            this.COMPLEMENTO1,
            this.Bairro1,
            this.Telefone1,
            this.Telefone2,
            this.Fax,
            this.CPF,
            this.CNPJ,
            this.UF,
            this.MUNICIPIO});
            this.DataGriewSearch.Location = new System.Drawing.Point(12, 56);
            this.DataGriewSearch.Name = "DataGriewSearch";
            this.DataGriewSearch.ReadOnly = true;
            this.DataGriewSearch.Size = new System.Drawing.Size(1007, 355);
            this.DataGriewSearch.TabIndex = 43;
            this.DataGriewSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewSearch.Enter += new System.EventHandler(this.DataGriewSearch_Enter);
            this.DataGriewSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewSearch_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(643, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "&Sair";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtNomePesquisa
            // 
            this.txtNomePesquisa.Location = new System.Drawing.Point(12, 30);
            this.txtNomePesquisa.Name = "txtNomePesquisa";
            this.txtNomePesquisa.Size = new System.Drawing.Size(593, 20);
            this.txtNomePesquisa.TabIndex = 0;
            this.txtNomePesquisa.Enter += new System.EventHandler(this.txtNomePesquisa_Enter);
            this.txtNomePesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyDown);
            this.txtNomePesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pesquisa;";
            // 
            // NOME
            // 
            this.NOME.DataPropertyName = "NOME";
            this.NOME.HeaderText = "Nome";
            this.NOME.Name = "NOME";
            this.NOME.ReadOnly = true;
            this.NOME.Width = 300;
            // 
            // IDCLIENTE
            // 
            this.IDCLIENTE.DataPropertyName = "IDCLIENTE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDCLIENTE.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDCLIENTE.HeaderText = "Código";
            this.IDCLIENTE.Name = "IDCLIENTE";
            this.IDCLIENTE.ReadOnly = true;
            this.IDCLIENTE.Width = 50;
            // 
            // ENDERECO1
            // 
            this.ENDERECO1.DataPropertyName = "ENDERECO1";
            this.ENDERECO1.HeaderText = "Endereço";
            this.ENDERECO1.Name = "ENDERECO1";
            this.ENDERECO1.ReadOnly = true;
            this.ENDERECO1.Width = 300;
            // 
            // NUMEROENDER
            // 
            this.NUMEROENDER.DataPropertyName = "NUMEROENDER";
            this.NUMEROENDER.HeaderText = "Nº";
            this.NUMEROENDER.Name = "NUMEROENDER";
            this.NUMEROENDER.ReadOnly = true;
            this.NUMEROENDER.Width = 50;
            // 
            // COMPLEMENTO1
            // 
            this.COMPLEMENTO1.DataPropertyName = "COMPLEMENTO1";
            this.COMPLEMENTO1.HeaderText = "Complemento";
            this.COMPLEMENTO1.Name = "COMPLEMENTO1";
            this.COMPLEMENTO1.ReadOnly = true;
            // 
            // Bairro1
            // 
            this.Bairro1.DataPropertyName = "Bairro1";
            this.Bairro1.HeaderText = "Bairro";
            this.Bairro1.Name = "Bairro1";
            this.Bairro1.ReadOnly = true;
            this.Bairro1.Width = 200;
            // 
            // Telefone1
            // 
            this.Telefone1.DataPropertyName = "Telefone1";
            this.Telefone1.HeaderText = "Telefone 1";
            this.Telefone1.Name = "Telefone1";
            this.Telefone1.ReadOnly = true;
            // 
            // Telefone2
            // 
            this.Telefone2.DataPropertyName = "Telefone2";
            this.Telefone2.HeaderText = "Celular";
            this.Telefone2.Name = "Telefone2";
            this.Telefone2.ReadOnly = true;
            // 
            // Fax
            // 
            this.Fax.DataPropertyName = "Fax";
            this.Fax.HeaderText = "Telefone 2";
            this.Fax.Name = "Fax";
            this.Fax.ReadOnly = true;
            // 
            // CPF
            // 
            this.CPF.DataPropertyName = "CPF";
            this.CPF.HeaderText = "CPF";
            this.CPF.Name = "CPF";
            this.CPF.ReadOnly = true;
            // 
            // CNPJ
            // 
            this.CNPJ.DataPropertyName = "CNPJ";
            this.CNPJ.HeaderText = "CNPJ";
            this.CNPJ.Name = "CNPJ";
            this.CNPJ.ReadOnly = true;
            this.CNPJ.Width = 120;
            // 
            // UF
            // 
            this.UF.DataPropertyName = "UF";
            this.UF.HeaderText = "UF";
            this.UF.Name = "UF";
            this.UF.ReadOnly = true;
            this.UF.Width = 30;
            // 
            // MUNICIPIO
            // 
            this.MUNICIPIO.DataPropertyName = "MUNICIPIO";
            this.MUNICIPIO.HeaderText = "Cidade";
            this.MUNICIPIO.Name = "MUNICIPIO";
            this.MUNICIPIO.ReadOnly = true;
            this.MUNICIPIO.Width = 300;
            // 
            // FrmSearchCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 445);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Cliente";
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
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewSearch;
        private System.Windows.Forms.Button btnCadFornecedor;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ENDERECO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMEROENDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPLEMENTO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bairro1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefone1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefone2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fax;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn UF;
        private System.Windows.Forms.DataGridViewTextBoxColumn MUNICIPIO;
    }
}