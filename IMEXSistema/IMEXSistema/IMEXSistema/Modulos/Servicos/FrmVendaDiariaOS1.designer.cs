namespace BmsSoftware.Modulos.Servicos
{
    partial class FrmVendaDiariaOS1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rdVenda = new System.Windows.Forms.RadioButton();
            this.rdOrcamento = new System.Windows.Forms.RadioButton();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgFormPagto = new System.Windows.Forms.DataGridView();
            this.NOMEFORMPAGTO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALFECHOS2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridRelaPedido = new System.Windows.Forms.DataGridView();
            this.IDORDEMSERVICO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALFECHOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFUNCIONARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFORMAPAGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mkdatafinal = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.mkDtInicial = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFormPagto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaPedido)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox5);
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
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rbTodos);
            this.groupBox5.Controls.Add(this.rdVenda);
            this.groupBox5.Controls.Add(this.rdOrcamento);
            this.groupBox5.Location = new System.Drawing.Point(623, 41);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(264, 44);
            this.groupBox5.TabIndex = 215;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo";
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(172, 19);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 2;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // rdVenda
            // 
            this.rdVenda.AutoSize = true;
            this.rdVenda.Location = new System.Drawing.Point(97, 19);
            this.rdVenda.Name = "rdVenda";
            this.rdVenda.Size = new System.Drawing.Size(56, 17);
            this.rdVenda.TabIndex = 1;
            this.rdVenda.Text = "Venda";
            this.rdVenda.UseVisualStyleBackColor = true;
            // 
            // rdOrcamento
            // 
            this.rdOrcamento.AutoSize = true;
            this.rdOrcamento.Location = new System.Drawing.Point(13, 19);
            this.rdOrcamento.Name = "rdOrcamento";
            this.rdOrcamento.Size = new System.Drawing.Size(77, 17);
            this.rdOrcamento.TabIndex = 0;
            this.rdOrcamento.Text = "Orçamento";
            this.rdOrcamento.UseVisualStyleBackColor = true;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(587, 433);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(132, 31);
            this.txtTotal.TabIndex = 194;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.Location = new System.Drawing.Point(582, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 26);
            this.label1.TabIndex = 193;
            this.label1.Text = "Total";
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
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(302, 61);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(101, 23);
            this.btnConsultar.TabIndex = 189;
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgFormPagto);
            this.groupBox1.Location = new System.Drawing.Point(12, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 160);
            this.groupBox1.TabIndex = 188;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resumo por Formas de Pagamento";
            // 
            // dtgFormPagto
            // 
            this.dtgFormPagto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFormPagto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOMEFORMPAGTO2,
            this.TOTALFECHOS2});
            this.dtgFormPagto.Location = new System.Drawing.Point(6, 19);
            this.dtgFormPagto.Name = "dtgFormPagto";
            this.dtgFormPagto.ReadOnly = true;
            this.dtgFormPagto.Size = new System.Drawing.Size(514, 122);
            this.dtgFormPagto.TabIndex = 189;
            // 
            // NOMEFORMPAGTO2
            // 
            this.NOMEFORMPAGTO2.DataPropertyName = "NOMEFORMAPAGTO";
            this.NOMEFORMPAGTO2.HeaderText = "Forma de Pagamento";
            this.NOMEFORMPAGTO2.Name = "NOMEFORMPAGTO2";
            this.NOMEFORMPAGTO2.ReadOnly = true;
            this.NOMEFORMPAGTO2.Width = 300;
            // 
            // TOTALFECHOS2
            // 
            this.TOTALFECHOS2.DataPropertyName = "TOTALFECHOS";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.TOTALFECHOS2.DefaultCellStyle = dataGridViewCellStyle1;
            this.TOTALFECHOS2.HeaderText = "Valor Total";
            this.TOTALFECHOS2.Name = "TOTALFECHOS2";
            this.TOTALFECHOS2.ReadOnly = true;
            this.TOTALFECHOS2.Width = 150;
            // 
            // DataGridRelaPedido
            // 
            this.DataGridRelaPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridRelaPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDORDEMSERVICO,
            this.DATAEMISSAO,
            this.NOMECLIENTE,
            this.TOTALFECHOS,
            this.NOMEFUNCIONARIO,
            this.NOMEFORMAPAGTO});
            this.DataGridRelaPedido.Location = new System.Drawing.Point(12, 91);
            this.DataGridRelaPedido.Name = "DataGridRelaPedido";
            this.DataGridRelaPedido.ReadOnly = true;
            this.DataGridRelaPedido.Size = new System.Drawing.Size(1000, 255);
            this.DataGridRelaPedido.TabIndex = 187;
            // 
            // IDORDEMSERVICO
            // 
            this.IDORDEMSERVICO.DataPropertyName = "IDORDEMSERVICO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDORDEMSERVICO.DefaultCellStyle = dataGridViewCellStyle2;
            this.IDORDEMSERVICO.HeaderText = "O. de Serviço";
            this.IDORDEMSERVICO.Name = "IDORDEMSERVICO";
            this.IDORDEMSERVICO.ReadOnly = true;
            // 
            // DATAEMISSAO
            // 
            this.DATAEMISSAO.DataPropertyName = "DATAEMISSAO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.DATAEMISSAO.DefaultCellStyle = dataGridViewCellStyle3;
            this.DATAEMISSAO.HeaderText = "Data";
            this.DATAEMISSAO.Name = "DATAEMISSAO";
            this.DATAEMISSAO.ReadOnly = true;
            this.DATAEMISSAO.Width = 80;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 300;
            // 
            // TOTALFECHOS
            // 
            this.TOTALFECHOS.DataPropertyName = "TOTALFECHOS";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.TOTALFECHOS.DefaultCellStyle = dataGridViewCellStyle4;
            this.TOTALFECHOS.HeaderText = "Valor Total";
            this.TOTALFECHOS.Name = "TOTALFECHOS";
            this.TOTALFECHOS.ReadOnly = true;
            // 
            // NOMEFUNCIONARIO
            // 
            this.NOMEFUNCIONARIO.DataPropertyName = "NOMEFUNCIONARIO";
            this.NOMEFUNCIONARIO.HeaderText = "Funcionário";
            this.NOMEFUNCIONARIO.Name = "NOMEFUNCIONARIO";
            this.NOMEFUNCIONARIO.ReadOnly = true;
            this.NOMEFUNCIONARIO.Width = 200;
            // 
            // NOMEFORMAPAGTO
            // 
            this.NOMEFORMAPAGTO.DataPropertyName = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.HeaderText = "Forma de Pagamento";
            this.NOMEFORMAPAGTO.Name = "NOMEFORMAPAGTO";
            this.NOMEFORMAPAGTO.ReadOnly = true;
            this.NOMEFORMAPAGTO.Width = 200;
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
            this.bntDateSelecFinal.Text = "...";
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
            this.bntDateSelecInicial.Text = "...";
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // FrmVendaDiariaOS1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 556);
            this.Controls.Add(this.panel1);
            this.Name = "FrmVendaDiariaOS1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Venda Diária - Ordem de Serviço";
            this.Load += new System.EventHandler(this.FrmVendaDiariaPV1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFormPagto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRelaPedido)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn IDORDEMSERVICO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALFECHOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFUNCIONARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORMAPAGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFORMPAGTO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALFECHOS2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rdVenda;
        private System.Windows.Forms.RadioButton rdOrcamento;
    }
}