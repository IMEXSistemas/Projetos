namespace BmsSoftware.Modulos.FrmSearch
{
    partial class FrmSearchPedidoNFe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchPedidoNFe));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DataGriewSearch = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDSTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEVENDEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDVENDEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORCOMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMELOCALCOBRANCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDLOCALCOBRANCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCCENTROCUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CENTROCUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCENTROCUSTOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFORMAPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORDEVEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDFORMAPAGAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtNomePesquisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DataGriewSearch);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.txtNomePesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 256);
            this.panel1.TabIndex = 0;
            // 
            // DataGriewSearch
            // 
            this.DataGriewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.IDPEDIDO,
            this.DTEMISSAO,
            this.TOTALPEDIDO,
            this.NOMECLIENTE,
            this.NOMESTATUS,
            this.IDCLIENTE,
            this.IDSTATUS,
            this.NOMEVENDEDOR,
            this.IDVENDEDOR,
            this.VALORCOMISSAO,
            this.NOMELOCALCOBRANCA,
            this.IDLOCALCOBRANCA,
            this.DESCCENTROCUSTO,
            this.CENTROCUSTO,
            this.IDCENTROCUSTOS,
            this.NOMEFORMAPAGTO,
            this.VALORDEVEDOR,
            this.IDFORMAPAGAMENTO});
            this.DataGriewSearch.Location = new System.Drawing.Point(12, 56);
            this.DataGriewSearch.Name = "DataGriewSearch";
            this.DataGriewSearch.ReadOnly = true;
            this.DataGriewSearch.Size = new System.Drawing.Size(767, 177);
            this.DataGriewSearch.TabIndex = 97;
            this.DataGriewSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewSearch_CellDoubleClick);
            this.DataGriewSearch.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGriewSearch_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Visualizar";
            this.Column1.Image = ((System.Drawing.Image)(resources.GetObject("Column1.Image")));
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // IDPEDIDO
            // 
            this.IDPEDIDO.DataPropertyName = "IDPEDIDO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDPEDIDO.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDPEDIDO.HeaderText = "Nº Pedido";
            this.IDPEDIDO.Name = "IDPEDIDO";
            this.IDPEDIDO.ReadOnly = true;
            this.IDPEDIDO.Width = 70;
            // 
            // DTEMISSAO
            // 
            this.DTEMISSAO.DataPropertyName = "DTEMISSAO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.DTEMISSAO.DefaultCellStyle = dataGridViewCellStyle2;
            this.DTEMISSAO.HeaderText = "Data";
            this.DTEMISSAO.Name = "DTEMISSAO";
            this.DTEMISSAO.ReadOnly = true;
            this.DTEMISSAO.Width = 80;
            // 
            // TOTALPEDIDO
            // 
            this.TOTALPEDIDO.DataPropertyName = "TOTALPEDIDO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.TOTALPEDIDO.DefaultCellStyle = dataGridViewCellStyle3;
            this.TOTALPEDIDO.HeaderText = "Total Pedido";
            this.TOTALPEDIDO.Name = "TOTALPEDIDO";
            this.TOTALPEDIDO.ReadOnly = true;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente";
            this.NOMECLIENTE.MaxInputLength = 50;
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 200;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            // 
            // IDCLIENTE
            // 
            this.IDCLIENTE.DataPropertyName = "IDCLIENTE";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDCLIENTE.DefaultCellStyle = dataGridViewCellStyle4;
            this.IDCLIENTE.HeaderText = "Cód. Cliente";
            this.IDCLIENTE.Name = "IDCLIENTE";
            this.IDCLIENTE.ReadOnly = true;
            this.IDCLIENTE.Width = 70;
            // 
            // IDSTATUS
            // 
            this.IDSTATUS.DataPropertyName = "IDSTATUS";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDSTATUS.DefaultCellStyle = dataGridViewCellStyle5;
            this.IDSTATUS.HeaderText = "Cód. Status";
            this.IDSTATUS.Name = "IDSTATUS";
            this.IDSTATUS.ReadOnly = true;
            this.IDSTATUS.Width = 50;
            // 
            // NOMEVENDEDOR
            // 
            this.NOMEVENDEDOR.DataPropertyName = "NOMEVENDEDOR";
            this.NOMEVENDEDOR.HeaderText = "Vendedor";
            this.NOMEVENDEDOR.Name = "NOMEVENDEDOR";
            this.NOMEVENDEDOR.ReadOnly = true;
            this.NOMEVENDEDOR.Width = 150;
            // 
            // IDVENDEDOR
            // 
            this.IDVENDEDOR.DataPropertyName = "IDVENDEDOR";
            this.IDVENDEDOR.HeaderText = "Cód. Vendedor";
            this.IDVENDEDOR.Name = "IDVENDEDOR";
            this.IDVENDEDOR.ReadOnly = true;
            this.IDVENDEDOR.Width = 80;
            // 
            // VALORCOMISSAO
            // 
            this.VALORCOMISSAO.DataPropertyName = "VALORCOMISSAO";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.VALORCOMISSAO.DefaultCellStyle = dataGridViewCellStyle6;
            this.VALORCOMISSAO.HeaderText = "Comissão";
            this.VALORCOMISSAO.Name = "VALORCOMISSAO";
            this.VALORCOMISSAO.ReadOnly = true;
            // 
            // NOMELOCALCOBRANCA
            // 
            this.NOMELOCALCOBRANCA.DataPropertyName = "NOMELOCALCOBRANCA";
            this.NOMELOCALCOBRANCA.HeaderText = "Local Cobrança";
            this.NOMELOCALCOBRANCA.Name = "NOMELOCALCOBRANCA";
            this.NOMELOCALCOBRANCA.ReadOnly = true;
            // 
            // IDLOCALCOBRANCA
            // 
            this.IDLOCALCOBRANCA.DataPropertyName = "IDLOCALCOBRANCA";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDLOCALCOBRANCA.DefaultCellStyle = dataGridViewCellStyle7;
            this.IDLOCALCOBRANCA.HeaderText = "Cód. Local Cobrança";
            this.IDLOCALCOBRANCA.Name = "IDLOCALCOBRANCA";
            this.IDLOCALCOBRANCA.ReadOnly = true;
            this.IDLOCALCOBRANCA.Width = 50;
            // 
            // DESCCENTROCUSTO
            // 
            this.DESCCENTROCUSTO.DataPropertyName = "DESCCENTROCUSTO";
            this.DESCCENTROCUSTO.HeaderText = "Desc. Centro Custo";
            this.DESCCENTROCUSTO.Name = "DESCCENTROCUSTO";
            this.DESCCENTROCUSTO.ReadOnly = true;
            // 
            // CENTROCUSTO
            // 
            this.CENTROCUSTO.DataPropertyName = "CENTROCUSTO";
            this.CENTROCUSTO.HeaderText = "Centro Custo";
            this.CENTROCUSTO.Name = "CENTROCUSTO";
            this.CENTROCUSTO.ReadOnly = true;
            // 
            // IDCENTROCUSTOS
            // 
            this.IDCENTROCUSTOS.DataPropertyName = "IDCENTROCUSTOS";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDCENTROCUSTOS.DefaultCellStyle = dataGridViewCellStyle8;
            this.IDCENTROCUSTOS.HeaderText = "Cód. Centro Custo";
            this.IDCENTROCUSTOS.Name = "IDCENTROCUSTOS";
            this.IDCENTROCUSTOS.ReadOnly = true;
            this.IDCENTROCUSTOS.Width = 50;
            // 
            // NOMEFORMAPAGTO
            // 
            this.NOMEFORMAPAGTO.DataPropertyName = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.HeaderText = "Forma Pagto";
            this.NOMEFORMAPAGTO.Name = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.ReadOnly = true;
            // 
            // VALORDEVEDOR
            // 
            this.VALORDEVEDOR.DataPropertyName = "VALORDEVEDOR";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.VALORDEVEDOR.DefaultCellStyle = dataGridViewCellStyle9;
            this.VALORDEVEDOR.HeaderText = "Total Devedor";
            this.VALORDEVEDOR.Name = "VALORDEVEDOR";
            this.VALORDEVEDOR.ReadOnly = true;
            // 
            // IDFORMAPAGAMENTO
            // 
            this.IDFORMAPAGAMENTO.DataPropertyName = "IDFORMAPAGAMENTO";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDFORMAPAGAMENTO.DefaultCellStyle = dataGridViewCellStyle10;
            this.IDFORMAPAGAMENTO.HeaderText = "Cód. Forma Pagto";
            this.IDFORMAPAGAMENTO.Name = "IDFORMAPAGAMENTO";
            this.IDFORMAPAGAMENTO.ReadOnly = true;
            this.IDFORMAPAGAMENTO.Width = 50;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(184, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Sair";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(103, 27);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 1;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtNomePesquisa
            // 
            this.txtNomePesquisa.Location = new System.Drawing.Point(12, 30);
            this.txtNomePesquisa.Name = "txtNomePesquisa";
            this.txtNomePesquisa.Size = new System.Drawing.Size(85, 20);
            this.txtNomePesquisa.TabIndex = 0;
            this.txtNomePesquisa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNomePesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomePesquisa_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Pedido";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmSearchPedidoNFe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 256);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSearchPedidoNFe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Pedido";
            this.Load += new System.EventHandler(this.FrmSearchFornecedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSearchFornecedor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomePesquisa;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView DataGriewSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEVENDEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDVENDEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORCOMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMELOCALCOBRANCA;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDLOCALCOBRANCA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCCENTROCUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CENTROCUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCENTROCUSTOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORMAPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORDEVEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDFORMAPAGAMENTO;


    }
}