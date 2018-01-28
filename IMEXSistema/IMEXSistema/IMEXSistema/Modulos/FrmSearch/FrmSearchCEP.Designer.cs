namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchCEP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mktxtCep1 = new System.Windows.Forms.MaskedTextBox();
            this.lblObsField = new System.Windows.Forms.Label();
            this.DataGriewSearch = new System.Windows.Forms.DataGridView();
            this.MUNICIPIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRespostaServidor = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRespostaServidor);
            this.panel1.Controls.Add(this.mktxtCep1);
            this.panel1.Controls.Add(this.lblObsField);
            this.panel1.Controls.Add(this.DataGriewSearch);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 273);
            this.panel1.TabIndex = 0;
            // 
            // mktxtCep1
            // 
            this.mktxtCep1.Location = new System.Drawing.Point(12, 30);
            this.mktxtCep1.Mask = "00000-999";
            this.mktxtCep1.Name = "mktxtCep1";
            this.mktxtCep1.Size = new System.Drawing.Size(77, 20);
            this.mktxtCep1.TabIndex = 160;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(12, 251);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(299, 13);
            this.lblObsField.TabIndex = 159;
            this.lblObsField.Text = "A pesquisa será efetuada diretamente no sistema dos Correios";
            // 
            // DataGriewSearch
            // 
            this.DataGriewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MUNICIPIO,
            this.UF});
            this.DataGriewSearch.Location = new System.Drawing.Point(12, 56);
            this.DataGriewSearch.Name = "DataGriewSearch";
            this.DataGriewSearch.ReadOnly = true;
            this.DataGriewSearch.Size = new System.Drawing.Size(579, 188);
            this.DataGriewSearch.TabIndex = 43;
            this.DataGriewSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewSearch.Enter += new System.EventHandler(this.DataGriewSearch_Enter);
            this.DataGriewSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGriewSearch_KeyDown);
            // 
            // MUNICIPIO
            // 
            this.MUNICIPIO.DataPropertyName = "MUNICIPIO";
            this.MUNICIPIO.HeaderText = "Cidade";
            this.MUNICIPIO.Name = "MUNICIPIO";
            this.MUNICIPIO.ReadOnly = true;
            this.MUNICIPIO.Width = 450;
            // 
            // UF
            // 
            this.UF.DataPropertyName = "UF";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.UF.DefaultCellStyle = dataGridViewCellStyle6;
            this.UF.HeaderText = "UF";
            this.UF.Name = "UF";
            this.UF.ReadOnly = true;
            this.UF.Width = 70;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(176, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(95, 27);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            this.btnPesquisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnPesquisa_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CEP:";
            // 
            // lblRespostaServidor
            // 
            this.lblRespostaServidor.AutoSize = true;
            this.lblRespostaServidor.Location = new System.Drawing.Point(258, 36);
            this.lblRespostaServidor.Name = "lblRespostaServidor";
            this.lblRespostaServidor.Size = new System.Drawing.Size(112, 13);
            this.lblRespostaServidor.TabIndex = 161;
            this.lblRespostaServidor.Text = "Resposta do Servidor:";
            // 
            // FrmSearchCEP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 273);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchCEP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar de CEP";
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
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewSearch;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.MaskedTextBox mktxtCep1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MUNICIPIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn UF;
        private System.Windows.Forms.Label lblRespostaServidor;


    }
}