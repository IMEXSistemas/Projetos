namespace BmsSoftware.Modulos.Financeiro
{
    partial class FrmChequeDestino
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChequeDestino));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOutroDestino = new System.Windows.Forms.RadioButton();
            this.rbFornecDestino = new System.Windows.Forms.RadioButton();
            this.rbClienteDestino = new System.Windows.Forms.RadioButton();
            this.txtNomeClienteFornDestino = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AGENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIGCONTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ENTRADA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOMPARA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CENTROCUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCCENTROCUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEFUNC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPORECEBIMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTEFORNEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITULAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.btnpdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOutroDestino);
            this.groupBox2.Controls.Add(this.rbFornecDestino);
            this.groupBox2.Controls.Add(this.rbClienteDestino);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 43);
            this.groupBox2.TabIndex = 285;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Destino";
            // 
            // rbOutroDestino
            // 
            this.rbOutroDestino.AutoSize = true;
            this.rbOutroDestino.Checked = true;
            this.rbOutroDestino.Location = new System.Drawing.Point(249, 20);
            this.rbOutroDestino.Name = "rbOutroDestino";
            this.rbOutroDestino.Size = new System.Drawing.Size(56, 17);
            this.rbOutroDestino.TabIndex = 14;
            this.rbOutroDestino.TabStop = true;
            this.rbOutroDestino.Text = "Outros";
            this.rbOutroDestino.UseVisualStyleBackColor = true;
            this.rbOutroDestino.CheckedChanged += new System.EventHandler(this.rbOutroDestino_CheckedChanged);
            // 
            // rbFornecDestino
            // 
            this.rbFornecDestino.AutoSize = true;
            this.rbFornecDestino.Location = new System.Drawing.Point(129, 20);
            this.rbFornecDestino.Name = "rbFornecDestino";
            this.rbFornecDestino.Size = new System.Drawing.Size(79, 17);
            this.rbFornecDestino.TabIndex = 13;
            this.rbFornecDestino.Text = "Fornecedor";
            this.rbFornecDestino.UseVisualStyleBackColor = true;
            this.rbFornecDestino.Click += new System.EventHandler(this.rbFornecDestino_Click);
            // 
            // rbClienteDestino
            // 
            this.rbClienteDestino.AutoSize = true;
            this.rbClienteDestino.Location = new System.Drawing.Point(36, 20);
            this.rbClienteDestino.Name = "rbClienteDestino";
            this.rbClienteDestino.Size = new System.Drawing.Size(57, 17);
            this.rbClienteDestino.TabIndex = 12;
            this.rbClienteDestino.Text = "Cliente";
            this.rbClienteDestino.UseVisualStyleBackColor = true;
            this.rbClienteDestino.Click += new System.EventHandler(this.rbClienteDestino_Click);
            // 
            // txtNomeClienteFornDestino
            // 
            this.txtNomeClienteFornDestino.Location = new System.Drawing.Point(15, 74);
            this.txtNomeClienteFornDestino.MaxLength = 20;
            this.txtNomeClienteFornDestino.Name = "txtNomeClienteFornDestino";
            this.txtNomeClienteFornDestino.Size = new System.Drawing.Size(337, 20);
            this.txtNomeClienteFornDestino.TabIndex = 289;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 13);
            this.label9.TabIndex = 290;
            this.label9.Text = "Nome Cliente/Fornecedor:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.AllowUserToOrderColumns = true;
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.NUMERO,
            this.AGENCIA,
            this.CONTA,
            this.DIGCONTA,
            this.VALOR,
            this.ENTRADA,
            this.BOMPARA,
            this.CENTROCUSTO,
            this.DESCCENTROCUSTO,
            this.NOMESTATUS,
            this.NOMEFUNC,
            this.TIPORECEBIMENTO,
            this.NOMECLIENTEFORNEC,
            this.TITULAR});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 100);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(809, 286);
            this.DataGriewDados.TabIndex = 291;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Destino";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 250;
            // 
            // NUMERO
            // 
            this.NUMERO.DataPropertyName = "NUMERO";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NUMERO.DefaultCellStyle = dataGridViewCellStyle11;
            this.NUMERO.HeaderText = "Nº Cheque";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.ReadOnly = true;
            this.NUMERO.Width = 150;
            // 
            // AGENCIA
            // 
            this.AGENCIA.DataPropertyName = "AGENCIA";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.AGENCIA.DefaultCellStyle = dataGridViewCellStyle12;
            this.AGENCIA.HeaderText = "Agencia";
            this.AGENCIA.Name = "AGENCIA";
            this.AGENCIA.ReadOnly = true;
            // 
            // CONTA
            // 
            this.CONTA.DataPropertyName = "CONTA";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CONTA.DefaultCellStyle = dataGridViewCellStyle13;
            this.CONTA.HeaderText = "Conta";
            this.CONTA.Name = "CONTA";
            this.CONTA.ReadOnly = true;
            // 
            // DIGCONTA
            // 
            this.DIGCONTA.DataPropertyName = "DIGCONTA";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DIGCONTA.DefaultCellStyle = dataGridViewCellStyle14;
            this.DIGCONTA.HeaderText = "Dig. Conta";
            this.DIGCONTA.Name = "DIGCONTA";
            this.DIGCONTA.ReadOnly = true;
            this.DIGCONTA.Width = 50;
            // 
            // VALOR
            // 
            this.VALOR.DataPropertyName = "VALOR";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VALOR.DefaultCellStyle = dataGridViewCellStyle15;
            this.VALOR.HeaderText = "Valor";
            this.VALOR.Name = "VALOR";
            this.VALOR.ReadOnly = true;
            // 
            // ENTRADA
            // 
            this.ENTRADA.DataPropertyName = "ENTRADA";
            this.ENTRADA.HeaderText = "Entrada";
            this.ENTRADA.Name = "ENTRADA";
            this.ENTRADA.ReadOnly = true;
            // 
            // BOMPARA
            // 
            this.BOMPARA.DataPropertyName = "BOMPARA";
            this.BOMPARA.HeaderText = "Bom Para";
            this.BOMPARA.Name = "BOMPARA";
            this.BOMPARA.ReadOnly = true;
            // 
            // CENTROCUSTO
            // 
            this.CENTROCUSTO.DataPropertyName = "CENTROCUSTO";
            this.CENTROCUSTO.HeaderText = "Centro Custo";
            this.CENTROCUSTO.Name = "CENTROCUSTO";
            this.CENTROCUSTO.ReadOnly = true;
            // 
            // DESCCENTROCUSTO
            // 
            this.DESCCENTROCUSTO.DataPropertyName = "DESCCENTROCUSTO";
            this.DESCCENTROCUSTO.HeaderText = "Desc, Centro Custo";
            this.DESCCENTROCUSTO.Name = "DESCCENTROCUSTO";
            this.DESCCENTROCUSTO.ReadOnly = true;
            this.DESCCENTROCUSTO.Width = 150;
            // 
            // NOMESTATUS
            // 
            this.NOMESTATUS.DataPropertyName = "NOMESTATUS";
            this.NOMESTATUS.HeaderText = "Status / Situação";
            this.NOMESTATUS.Name = "NOMESTATUS";
            this.NOMESTATUS.ReadOnly = true;
            this.NOMESTATUS.Width = 150;
            // 
            // NOMEFUNC
            // 
            this.NOMEFUNC.DataPropertyName = "NOMEFUNC";
            this.NOMEFUNC.HeaderText = "Funcionário";
            this.NOMEFUNC.Name = "NOMEFUNC";
            this.NOMEFUNC.ReadOnly = true;
            this.NOMEFUNC.Width = 200;
            // 
            // TIPORECEBIMENTO
            // 
            this.TIPORECEBIMENTO.DataPropertyName = "TIPORECEBIMENTO";
            this.TIPORECEBIMENTO.HeaderText = "Tipo";
            this.TIPORECEBIMENTO.Name = "TIPORECEBIMENTO";
            this.TIPORECEBIMENTO.ReadOnly = true;
            this.TIPORECEBIMENTO.Width = 40;
            // 
            // NOMECLIENTEFORNEC
            // 
            this.NOMECLIENTEFORNEC.DataPropertyName = "NOMECLIENTEFORNEC";
            this.NOMECLIENTEFORNEC.HeaderText = "Nome Cliente/Fornecedor";
            this.NOMECLIENTEFORNEC.Name = "NOMECLIENTEFORNEC";
            this.NOMECLIENTEFORNEC.ReadOnly = true;
            this.NOMECLIENTEFORNEC.Width = 250;
            // 
            // TITULAR
            // 
            this.TITULAR.DataPropertyName = "TITULAR";
            this.TITULAR.HeaderText = "Outros / Terceiros / Titular do cheque:";
            this.TITULAR.Name = "TITULAR";
            this.TITULAR.ReadOnly = true;
            this.TITULAR.Width = 250;
            // 
            // btnConsulta
            // 
            this.btnConsulta.Image = ((System.Drawing.Image)(resources.GetObject("btnConsulta.Image")));
            this.btnConsulta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsulta.Location = new System.Drawing.Point(358, 72);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(75, 23);
            this.btnConsulta.TabIndex = 292;
            this.btnConsulta.Text = "&Consultar";
            this.btnConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsulta.UseVisualStyleBackColor = true;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(439, 72);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 293;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(520, 72);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 294;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(12, 389);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(105, 13);
            this.lblTotalRegistros.TabIndex = 295;
            this.lblTotalRegistros.Text = "Total de Registros: 0";
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdf.Location = new System.Drawing.Point(737, 72);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(25, 23);
            this.btnpdf.TabIndex = 307;
            this.btnpdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnpdf.UseVisualStyleBackColor = true;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(768, 71);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(25, 23);
            this.btnExcel.TabIndex = 306;
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(799, 71);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(25, 23);
            this.btnPrint.TabIndex = 305;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmChequeDestino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 429);
            this.Controls.Add(this.btnpdf);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnConsulta);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.txtNomeClienteFornDestino);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmChequeDestino";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheque Por Destino";
            this.Load += new System.EventHandler(this.FrmChequeDestino_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOutroDestino;
        private System.Windows.Forms.RadioButton rbFornecDestino;
        private System.Windows.Forms.RadioButton rbClienteDestino;
        private System.Windows.Forms.TextBox txtNomeClienteFornDestino;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn AGENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIGCONTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ENTRADA;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOMPARA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CENTROCUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCCENTROCUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMESTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFUNC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPORECEBIMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTEFORNEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITULAR;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}