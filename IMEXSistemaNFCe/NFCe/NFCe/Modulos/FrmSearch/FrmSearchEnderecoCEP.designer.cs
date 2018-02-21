namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchEnderecoCEP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchEnderecoCEP));
            this.txtendereco = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtUF = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.DataGriewSearch = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtendereco
            // 
            this.txtendereco.Location = new System.Drawing.Point(138, 32);
            this.txtendereco.MaxLength = 100;
            this.txtendereco.Name = "txtendereco";
            this.txtendereco.Size = new System.Drawing.Size(573, 20);
            this.txtendereco.TabIndex = 23;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(138, 15);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(56, 13);
            this.label30.TabIndex = 24;
            this.label30.Text = "Endereço:";
            // 
            // txtUF
            // 
            this.txtUF.Location = new System.Drawing.Point(661, 71);
            this.txtUF.MaxLength = 100;
            this.txtUF.Name = "txtUF";
            this.txtUF.ReadOnly = true;
            this.txtUF.Size = new System.Drawing.Size(49, 20);
            this.txtUF.TabIndex = 35;
            this.txtUF.TextChanged += new System.EventHandler(this.txtUF1_TextChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(658, 54);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(24, 13);
            this.label32.TabIndex = 37;
            this.label32.Text = "UF:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label32.Click += new System.EventHandler(this.label32_Click);
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(12, 71);
            this.txtCidade.MaxLength = 100;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.ReadOnly = true;
            this.txtCidade.Size = new System.Drawing.Size(640, 20);
            this.txtCidade.TabIndex = 34;
            this.txtCidade.TextChanged += new System.EventHandler(this.txtCidade1_TextChanged);
            this.txtCidade.Enter += new System.EventHandler(this.txtCidade1_Enter);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 36;
            this.label27.Text = "Cidade:";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(96, 97);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 39;
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(12, 97);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 38;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // DataGriewSearch
            // 
            this.DataGriewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.DataGriewSearch.Location = new System.Drawing.Point(12, 126);
            this.DataGriewSearch.Name = "DataGriewSearch";
            this.DataGriewSearch.ReadOnly = true;
            this.DataGriewSearch.Size = new System.Drawing.Size(696, 188);
            this.DataGriewSearch.TabIndex = 44;
            this.DataGriewSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CEP";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Complemento";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Bairro";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 250;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(12, 317);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(63, 13);
            this.lblTotalRegistros.TabIndex = 45;
            this.lblTotalRegistros.Text = "Registros: 0";
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Rua",
            "Avenida",
            "Rodovia",
            "Travessa",
            "Outros"});
            this.cbTipo.Location = new System.Drawing.Point(12, 31);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(120, 21);
            this.cbTipo.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Tipo:";
            // 
            // FrmSearchEnderecoCEP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 336);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTipo);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.DataGriewSearch);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.txtUF);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtendereco);
            this.Controls.Add(this.label30);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmSearchEnderecoCEP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta CEP por Endereço";
            this.Load += new System.EventHandler(this.FrmSearchEnderecoCEP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtendereco;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtUF;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.DataGridView DataGriewSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTipo;
    }
}