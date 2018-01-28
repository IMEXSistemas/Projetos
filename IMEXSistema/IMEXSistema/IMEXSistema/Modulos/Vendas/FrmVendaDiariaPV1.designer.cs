namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmVendaDiariaPV1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVendaDiariaPV1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotalPago = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgFormPagto = new System.Windows.Forms.DataGridView();
            this.NOMEFORMPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORPAGO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridRelaPedido = new System.Windows.Forms.DataGridView();
            this.IDPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORPAGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEVENDEDOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFORMAPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGORCAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mkdatafinal = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.mkDtInicial = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.sfdExportToxcel = new System.Windows.Forms.SaveFileDialog();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.RbTodosPesquisa = new System.Windows.Forms.RadioButton();
            this.rbVendasPesquisa = new System.Windows.Forms.RadioButton();
            this.rbOrcamentoPesquisa = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFormPagto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaPedido)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.txtTotalPago);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTotalRegistros);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnConsultar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.DataGridRelaPedido);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 556);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtTotalPago
            // 
            this.txtTotalPago.Font = new System.Drawing.Font("Arial Narrow", 18.25F);
            this.txtTotalPago.Location = new System.Drawing.Point(838, 417);
            this.txtTotalPago.Name = "txtTotalPago";
            this.txtTotalPago.ReadOnly = true;
            this.txtTotalPago.Size = new System.Drawing.Size(119, 35);
            this.txtTotalPago.TabIndex = 197;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 18.25F);
            this.label3.Location = new System.Drawing.Point(833, 384);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 30);
            this.label3.TabIndex = 196;
            this.label3.Text = "Total Pago:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Arial Narrow", 18.25F);
            this.txtTotal.Location = new System.Drawing.Point(704, 417);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(119, 35);
            this.txtTotal.TabIndex = 194;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 18.25F);
            this.label1.Location = new System.Drawing.Point(699, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 30);
            this.label1.TabIndex = 193;
            this.label1.Text = "Total Pedido:";
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(15, 356);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(105, 13);
            this.lblTotalRegistros.TabIndex = 192;
            this.lblTotalRegistros.Text = "Total de Registros: 0";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(516, 61);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(101, 23);
            this.btnSair.TabIndex = 191;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(409, 61);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(101, 23);
            this.btnImprimir.TabIndex = 190;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(302, 61);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(101, 23);
            this.btnConsultar.TabIndex = 189;
            this.btnConsultar.Text = "&Pesquisar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgFormPagto);
            this.groupBox1.Location = new System.Drawing.Point(12, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 160);
            this.groupBox1.TabIndex = 188;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resumo por Formas de Pagamento";
            // 
            // dtgFormPagto
            // 
            this.dtgFormPagto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFormPagto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEFORMPAGTO,
            this.dataGridViewTextBoxColumn5,
            this.VALORPAGO2});
            this.dtgFormPagto.Location = new System.Drawing.Point(6, 19);
            this.dtgFormPagto.Name = "dtgFormPagto";
            this.dtgFormPagto.ReadOnly = true;
            this.dtgFormPagto.Size = new System.Drawing.Size(572, 122);
            this.dtgFormPagto.TabIndex = 189;
            // 
            // NOMEFORMPAGTO
            // 
            this.NOMEFORMPAGTO.DataPropertyName = "NOMEFORMPAGTO";
            this.NOMEFORMPAGTO.HeaderText = "Forma de Pagamento";
            this.NOMEFORMPAGTO.Name = "NOMEFORMPAGTO";
            this.NOMEFORMPAGTO.ReadOnly = true;
            this.NOMEFORMPAGTO.Width = 350;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "TOTALPEDIDO";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn5.HeaderText = "Vl. Pedido";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // VALORPAGO2
            // 
            this.VALORPAGO2.DataPropertyName = "VALORPAGO";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.VALORPAGO2.DefaultCellStyle = dataGridViewCellStyle8;
            this.VALORPAGO2.HeaderText = "Vl. Pago";
            this.VALORPAGO2.Name = "VALORPAGO2";
            this.VALORPAGO2.ReadOnly = true;
            this.VALORPAGO2.Width = 80;
            // 
            // DataGridRelaPedido
            // 
            this.DataGridRelaPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridRelaPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDPEDIDO,
            this.DTEMISSAO,
            this.NOMECLIENTE,
            this.TOTALPEDIDO,
            this.VALORPAGO,
            this.NOMEVENDEDOR,
            this.NOMEFORMAPAGTO,
            this.FLAGORCAMENTO});
            this.DataGridRelaPedido.Location = new System.Drawing.Point(12, 91);
            this.DataGridRelaPedido.Name = "DataGridRelaPedido";
            this.DataGridRelaPedido.ReadOnly = true;
            this.DataGridRelaPedido.Size = new System.Drawing.Size(1000, 255);
            this.DataGridRelaPedido.TabIndex = 187;
            // 
            // IDPEDIDO
            // 
            this.IDPEDIDO.DataPropertyName = "IDPEDIDO";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "000000";
            this.IDPEDIDO.DefaultCellStyle = dataGridViewCellStyle9;
            this.IDPEDIDO.HeaderText = "Nº Pedido";
            this.IDPEDIDO.Name = "IDPEDIDO";
            this.IDPEDIDO.ReadOnly = true;
            // 
            // DTEMISSAO
            // 
            this.DTEMISSAO.DataPropertyName = "DTEMISSAO";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.DTEMISSAO.DefaultCellStyle = dataGridViewCellStyle10;
            this.DTEMISSAO.HeaderText = "Data";
            this.DTEMISSAO.Name = "DTEMISSAO";
            this.DTEMISSAO.ReadOnly = true;
            this.DTEMISSAO.Width = 80;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 300;
            // 
            // TOTALPEDIDO
            // 
            this.TOTALPEDIDO.DataPropertyName = "TOTALPEDIDO";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.TOTALPEDIDO.DefaultCellStyle = dataGridViewCellStyle11;
            this.TOTALPEDIDO.HeaderText = "Vl. Pedido";
            this.TOTALPEDIDO.Name = "TOTALPEDIDO";
            this.TOTALPEDIDO.ReadOnly = true;
            // 
            // VALORPAGO
            // 
            this.VALORPAGO.DataPropertyName = "VALORPAGO";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.VALORPAGO.DefaultCellStyle = dataGridViewCellStyle12;
            this.VALORPAGO.HeaderText = "Vl. Pago";
            this.VALORPAGO.Name = "VALORPAGO";
            this.VALORPAGO.ReadOnly = true;
            // 
            // NOMEVENDEDOR
            // 
            this.NOMEVENDEDOR.DataPropertyName = "NOMEVENDEDOR";
            this.NOMEVENDEDOR.HeaderText = "Vendedor";
            this.NOMEVENDEDOR.Name = "NOMEVENDEDOR";
            this.NOMEVENDEDOR.ReadOnly = true;
            this.NOMEVENDEDOR.Width = 200;
            // 
            // NOMEFORMAPAGTO
            // 
            this.NOMEFORMAPAGTO.DataPropertyName = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.HeaderText = "Forma de Pagamento";
            this.NOMEFORMAPAGTO.Name = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.ReadOnly = true;
            this.NOMEFORMAPAGTO.Width = 200;
            // 
            // FLAGORCAMENTO
            // 
            this.FLAGORCAMENTO.DataPropertyName = "FLAGORCAMENTO";
            this.FLAGORCAMENTO.HeaderText = "Orçamento";
            this.FLAGORCAMENTO.Name = "FLAGORCAMENTO";
            this.FLAGORCAMENTO.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mkdatafinal);
            this.groupBox2.Controls.Add(this.bntDateSelecFinal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mkDtInicial);
            this.groupBox2.Controls.Add(this.bntDateSelecInicial);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 73);
            this.groupBox2.TabIndex = 186;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Emissão";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "Final:";
            // 
            // mkdatafinal
            // 
            this.mkdatafinal.Location = new System.Drawing.Point(140, 35);
            this.mkdatafinal.Mask = "00/00/0000";
            this.mkdatafinal.Name = "mkdatafinal";
            this.mkdatafinal.Size = new System.Drawing.Size(79, 20);
            this.mkdatafinal.TabIndex = 184;
            this.mkdatafinal.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(225, 35);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 186;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 182;
            this.label2.Text = "Inicial:";
            // 
            // mkDtInicial
            // 
            this.mkDtInicial.Location = new System.Drawing.Point(23, 35);
            this.mkDtInicial.Mask = "00/00/0000";
            this.mkDtInicial.Name = "mkDtInicial";
            this.mkDtInicial.Size = new System.Drawing.Size(79, 20);
            this.mkDtInicial.TabIndex = 181;
            this.mkDtInicial.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(108, 35);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 183;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.RbTodosPesquisa);
            this.groupBox7.Controls.Add(this.rbVendasPesquisa);
            this.groupBox7.Controls.Add(this.rbOrcamentoPesquisa);
            this.groupBox7.Location = new System.Drawing.Point(775, 40);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(237, 44);
            this.groupBox7.TabIndex = 295;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tipo";
            // 
            // RbTodosPesquisa
            // 
            this.RbTodosPesquisa.AutoSize = true;
            this.RbTodosPesquisa.Checked = true;
            this.RbTodosPesquisa.Location = new System.Drawing.Point(162, 17);
            this.RbTodosPesquisa.Name = "RbTodosPesquisa";
            this.RbTodosPesquisa.Size = new System.Drawing.Size(55, 17);
            this.RbTodosPesquisa.TabIndex = 2;
            this.RbTodosPesquisa.TabStop = true;
            this.RbTodosPesquisa.Text = "Todos";
            this.RbTodosPesquisa.UseVisualStyleBackColor = true;
            // 
            // rbVendasPesquisa
            // 
            this.rbVendasPesquisa.AutoSize = true;
            this.rbVendasPesquisa.Location = new System.Drawing.Point(98, 17);
            this.rbVendasPesquisa.Name = "rbVendasPesquisa";
            this.rbVendasPesquisa.Size = new System.Drawing.Size(56, 17);
            this.rbVendasPesquisa.TabIndex = 1;
            this.rbVendasPesquisa.Text = "Venda";
            this.rbVendasPesquisa.UseVisualStyleBackColor = true;
            // 
            // rbOrcamentoPesquisa
            // 
            this.rbOrcamentoPesquisa.AutoSize = true;
            this.rbOrcamentoPesquisa.Location = new System.Drawing.Point(13, 17);
            this.rbOrcamentoPesquisa.Name = "rbOrcamentoPesquisa";
            this.rbOrcamentoPesquisa.Size = new System.Drawing.Size(77, 17);
            this.rbOrcamentoPesquisa.TabIndex = 0;
            this.rbOrcamentoPesquisa.Text = "Orçamento";
            this.rbOrcamentoPesquisa.UseVisualStyleBackColor = true;
            // 
            // FrmVendaDiariaPV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 556);
            this.Controls.Add(this.panel1);
            this.Name = "FrmVendaDiariaPV1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Venda Diária";
            this.Load += new System.EventHandler(this.FrmVendaDiariaPV1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFormPagto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaPedido)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mkdatafinal;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mkDtInicial;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.DataGridView DataGridRelaPedido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridView dtgFormPagto;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog sfdExportToxcel;
        private System.Windows.Forms.TextBox txtTotalPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORPAGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEVENDEDOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORMAPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGORCAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORMPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORPAGO2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton RbTodosPesquisa;
        private System.Windows.Forms.RadioButton rbVendasPesquisa;
        private System.Windows.Forms.RadioButton rbOrcamentoPesquisa;
    }
}