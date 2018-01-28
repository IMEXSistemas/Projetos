namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmConfigPedBalcao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigPedBalcao));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCodigoProduto = new System.Windows.Forms.RadioButton();
            this.rbPesquisaCodigoReferencia = new System.Windows.Forms.RadioButton();
            this.rbPesquisaCodigoBarra = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTipoTicket = new System.Windows.Forms.ComboBox();
            this.btnCadLocaPagto = new System.Windows.Forms.Button();
            this.cbLocalCobranca = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cadTransportadora = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbTransportadora = new System.Windows.Forms.ComboBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.cbFormaPagto = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFormPagamento = new System.Windows.Forms.Button();
            this.btnCadCentroCusto = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.cbCentroCusto = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.chkPagoCaixa = new System.Windows.Forms.CheckBox();
            this.btnCadTipo = new System.Windows.Forms.Button();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chAbreGaveta = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chAbreGaveta);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbTipoTicket);
            this.panel1.Controls.Add(this.btnCadLocaPagto);
            this.panel1.Controls.Add(this.cbLocalCobranca);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.cadTransportadora);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cbTransportadora);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.cbFormaPagto);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnFormPagamento);
            this.panel1.Controls.Add(this.btnCadCentroCusto);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.cbCentroCusto);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 269);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCodigoProduto);
            this.groupBox1.Controls.Add(this.rbPesquisaCodigoReferencia);
            this.groupBox1.Controls.Add(this.rbPesquisaCodigoBarra);
            this.groupBox1.Location = new System.Drawing.Point(259, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 58);
            this.groupBox1.TabIndex = 249;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Pesquisa do Produto";
            // 
            // rbCodigoProduto
            // 
            this.rbCodigoProduto.AutoSize = true;
            this.rbCodigoProduto.Location = new System.Drawing.Point(244, 28);
            this.rbCodigoProduto.Name = "rbCodigoProduto";
            this.rbCodigoProduto.Size = new System.Drawing.Size(101, 17);
            this.rbCodigoProduto.TabIndex = 2;
            this.rbCodigoProduto.TabStop = true;
            this.rbCodigoProduto.Text = " Código Produto";
            this.rbCodigoProduto.UseVisualStyleBackColor = true;
            // 
            // rbPesquisaCodigoReferencia
            // 
            this.rbPesquisaCodigoReferencia.AutoSize = true;
            this.rbPesquisaCodigoReferencia.Location = new System.Drawing.Point(122, 28);
            this.rbPesquisaCodigoReferencia.Name = "rbPesquisaCodigoReferencia";
            this.rbPesquisaCodigoReferencia.Size = new System.Drawing.Size(116, 17);
            this.rbPesquisaCodigoReferencia.TabIndex = 1;
            this.rbPesquisaCodigoReferencia.TabStop = true;
            this.rbPesquisaCodigoReferencia.Text = " Código Referência";
            this.rbPesquisaCodigoReferencia.UseVisualStyleBackColor = true;
            // 
            // rbPesquisaCodigoBarra
            // 
            this.rbPesquisaCodigoBarra.AutoSize = true;
            this.rbPesquisaCodigoBarra.Location = new System.Drawing.Point(15, 28);
            this.rbPesquisaCodigoBarra.Name = "rbPesquisaCodigoBarra";
            this.rbPesquisaCodigoBarra.Size = new System.Drawing.Size(101, 17);
            this.rbPesquisaCodigoBarra.TabIndex = 0;
            this.rbPesquisaCodigoBarra.TabStop = true;
            this.rbPesquisaCodigoBarra.Text = "Código de Barra";
            this.rbPesquisaCodigoBarra.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 248;
            this.label1.Text = "Modelo de Impressão:";
            // 
            // cbTipoTicket
            // 
            this.cbTipoTicket.FormattingEnabled = true;
            this.cbTipoTicket.Items.AddRange(new object[] {
            "Modelo 1 ( Porta Serial )",
            "Modelo 2",
            "Modelo 3",
            "Modelo 4 ( Porta Serial )",
            "Modelo 5",
            "Modelo 6 ( Carta/A4)",
            "Modelo 7 ( Carta/A4) Ecônomico"});
            this.cbTipoTicket.Location = new System.Drawing.Point(15, 213);
            this.cbTipoTicket.Name = "cbTipoTicket";
            this.cbTipoTicket.Size = new System.Drawing.Size(285, 21);
            this.cbTipoTicket.TabIndex = 247;
            this.cbTipoTicket.SelectedIndexChanged += new System.EventHandler(this.cbTipoTicket_SelectedIndexChanged);
            // 
            // btnCadLocaPagto
            // 
            this.btnCadLocaPagto.FlatAppearance.BorderSize = 0;
            this.btnCadLocaPagto.Location = new System.Drawing.Point(635, 173);
            this.btnCadLocaPagto.Name = "btnCadLocaPagto";
            this.btnCadLocaPagto.Size = new System.Drawing.Size(26, 21);
            this.btnCadLocaPagto.TabIndex = 246;
            this.btnCadLocaPagto.UseVisualStyleBackColor = true;
            this.btnCadLocaPagto.Click += new System.EventHandler(this.btnCadLocaPagto_Click);
            // 
            // cbLocalCobranca
            // 
            this.cbLocalCobranca.FormattingEnabled = true;
            this.cbLocalCobranca.Location = new System.Drawing.Point(344, 173);
            this.cbLocalCobranca.Name = "cbLocalCobranca";
            this.cbLocalCobranca.Size = new System.Drawing.Size(285, 21);
            this.cbLocalCobranca.TabIndex = 244;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(341, 157);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(100, 13);
            this.label29.TabIndex = 245;
            this.label29.Text = "Local de Cobrança:";
            // 
            // cadTransportadora
            // 
            this.cadTransportadora.FlatAppearance.BorderSize = 0;
            this.cadTransportadora.Location = new System.Drawing.Point(306, 173);
            this.cadTransportadora.Name = "cadTransportadora";
            this.cadTransportadora.Size = new System.Drawing.Size(26, 21);
            this.cadTransportadora.TabIndex = 243;
            this.cadTransportadora.UseVisualStyleBackColor = true;
            this.cadTransportadora.Click += new System.EventHandler(this.cadTransportadora_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 242;
            this.label10.Text = "Transportes:";
            // 
            // cbTransportadora
            // 
            this.cbTransportadora.FormattingEnabled = true;
            this.cbTransportadora.Location = new System.Drawing.Point(15, 173);
            this.cbTransportadora.Name = "cbTransportadora";
            this.cbTransportadora.Size = new System.Drawing.Size(285, 21);
            this.cbTransportadora.TabIndex = 241;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(96, 240);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 240;
            this.btnSair.Text = "Sai&r";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(15, 240);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 239;
            this.btnSalvar.Text = "&Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // cbFormaPagto
            // 
            this.cbFormaPagto.FormattingEnabled = true;
            this.cbFormaPagto.Location = new System.Drawing.Point(344, 133);
            this.cbFormaPagto.Name = "cbFormaPagto";
            this.cbFormaPagto.Size = new System.Drawing.Size(285, 21);
            this.cbFormaPagto.TabIndex = 236;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(341, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 13);
            this.label8.TabIndex = 237;
            this.label8.Text = "Formas de Pagamento:";
            // 
            // btnFormPagamento
            // 
            this.btnFormPagamento.FlatAppearance.BorderSize = 0;
            this.btnFormPagamento.Location = new System.Drawing.Point(635, 133);
            this.btnFormPagamento.Name = "btnFormPagamento";
            this.btnFormPagamento.Size = new System.Drawing.Size(26, 21);
            this.btnFormPagamento.TabIndex = 238;
            this.btnFormPagamento.UseVisualStyleBackColor = true;
            this.btnFormPagamento.Click += new System.EventHandler(this.btnFormPagamento_Click);
            // 
            // btnCadCentroCusto
            // 
            this.btnCadCentroCusto.FlatAppearance.BorderSize = 0;
            this.btnCadCentroCusto.Location = new System.Drawing.Point(306, 132);
            this.btnCadCentroCusto.Name = "btnCadCentroCusto";
            this.btnCadCentroCusto.Size = new System.Drawing.Size(26, 21);
            this.btnCadCentroCusto.TabIndex = 235;
            this.btnCadCentroCusto.UseVisualStyleBackColor = true;
            this.btnCadCentroCusto.Click += new System.EventHandler(this.btnCadCentroCusto_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 117);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 13);
            this.label22.TabIndex = 234;
            this.label22.Text = "Centro de Custos:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.FormattingEnabled = true;
            this.cbCentroCusto.Location = new System.Drawing.Point(15, 133);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(285, 21);
            this.cbCentroCusto.TabIndex = 233;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.chkPagoCaixa);
            this.groupBox3.Controls.Add(this.btnCadTipo);
            this.groupBox3.Controls.Add(this.cbTipo);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 102);
            this.groupBox3.TabIndex = 230;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Entrada do Valor Pago no Caixa";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 45);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(103, 13);
            this.label30.TabIndex = 103;
            this.label30.Text = "Tipo de Pagamento:";
            // 
            // chkPagoCaixa
            // 
            this.chkPagoCaixa.AutoSize = true;
            this.chkPagoCaixa.Location = new System.Drawing.Point(9, 19);
            this.chkPagoCaixa.Name = "chkPagoCaixa";
            this.chkPagoCaixa.Size = new System.Drawing.Size(177, 17);
            this.chkPagoCaixa.TabIndex = 86;
            this.chkPagoCaixa.Text = "Entrada do Valor Pago no Caixa";
            this.chkPagoCaixa.UseVisualStyleBackColor = true;
            // 
            // btnCadTipo
            // 
            this.btnCadTipo.FlatAppearance.BorderSize = 0;
            this.btnCadTipo.Location = new System.Drawing.Point(189, 60);
            this.btnCadTipo.Name = "btnCadTipo";
            this.btnCadTipo.Size = new System.Drawing.Size(26, 21);
            this.btnCadTipo.TabIndex = 102;
            this.btnCadTipo.UseVisualStyleBackColor = true;
            this.btnCadTipo.Click += new System.EventHandler(this.btnCadTipo_Click);
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(10, 61);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(172, 21);
            this.cbTipo.TabIndex = 101;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chAbreGaveta
            // 
            this.chAbreGaveta.AutoSize = true;
            this.chAbreGaveta.Location = new System.Drawing.Point(344, 200);
            this.chAbreGaveta.Name = "chAbreGaveta";
            this.chAbreGaveta.Size = new System.Drawing.Size(85, 17);
            this.chAbreGaveta.TabIndex = 250;
            this.chAbreGaveta.Text = "Abrir Gaveta";
            this.chAbreGaveta.UseVisualStyleBackColor = true;
            // 
            // FrmConfigPedBalcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 269);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmConfigPedBalcao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração do Pedido Balcão";
            this.Load += new System.EventHandler(this.FrmConfigPedBalcao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConfigPedBalcao_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkPagoCaixa;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnCadTipo;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Button btnCadCentroCusto;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cbCentroCusto;
        private System.Windows.Forms.ComboBox cbFormaPagto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFormPagamento;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button cadTransportadora;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbTransportadora;
        private System.Windows.Forms.Button btnCadLocaPagto;
        private System.Windows.Forms.ComboBox cbLocalCobranca;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTipoTicket;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPesquisaCodigoReferencia;
        private System.Windows.Forms.RadioButton rbPesquisaCodigoBarra;
        private System.Windows.Forms.RadioButton rbCodigoProduto;
        private System.Windows.Forms.CheckBox chAbreGaveta;
    }
}