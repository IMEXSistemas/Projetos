namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmOutrosTipodePagamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutrosTipodePagamento));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mkdHoraFinal = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mkdHoraInicial = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecFinal = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.msktDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.msktDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rdVenda = new System.Windows.Forms.RadioButton();
            this.rdOrcamento = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.chkImpressaoTicket = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.Image")));
            this.btnPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa.Location = new System.Drawing.Point(15, 59);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 288;
            this.btnPesquisa.Text = "&Pesquisar";
            this.btnPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mkdHoraFinal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mkdHoraInicial);
            this.groupBox1.Controls.Add(this.bntDateSelecFinal);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.bntDateSelecInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.msktDataFinal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.msktDataInicial);
            this.groupBox1.Location = new System.Drawing.Point(275, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 111);
            this.groupBox1.TabIndex = 287;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            // 
            // mkdHoraFinal
            // 
            this.mkdHoraFinal.Location = new System.Drawing.Point(95, 74);
            this.mkdHoraFinal.Mask = "90:00";
            this.mkdHoraFinal.Name = "mkdHoraFinal";
            this.mkdHoraFinal.Size = new System.Drawing.Size(62, 20);
            this.mkdHoraFinal.TabIndex = 299;
            this.mkdHoraFinal.Text = "2359";
            this.mkdHoraFinal.Validating += new System.ComponentModel.CancelEventHandler(this.mkdHoraFinal_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(92, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 300;
            this.label2.Text = "Final:";
            // 
            // mkdHoraInicial
            // 
            this.mkdHoraInicial.Location = new System.Drawing.Point(24, 74);
            this.mkdHoraInicial.Mask = "90:00";
            this.mkdHoraInicial.Name = "mkdHoraInicial";
            this.mkdHoraInicial.Size = new System.Drawing.Size(62, 20);
            this.mkdHoraInicial.TabIndex = 297;
            this.mkdHoraInicial.Text = "0000";
            this.mkdHoraInicial.Validating += new System.ComponentModel.CancelEventHandler(this.mkdHoraInicial_Validating);
            // 
            // bntDateSelecFinal
            // 
            this.bntDateSelecFinal.FlatAppearance.BorderSize = 0;
            this.bntDateSelecFinal.Location = new System.Drawing.Point(228, 34);
            this.bntDateSelecFinal.Name = "bntDateSelecFinal";
            this.bntDateSelecFinal.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecFinal.TabIndex = 179;
            this.bntDateSelecFinal.UseVisualStyleBackColor = true;
            this.bntDateSelecFinal.Click += new System.EventHandler(this.bntDateSelecFinal_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(21, 58);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 298;
            this.label24.Text = "Inicial:";
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(109, 34);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 178;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 177;
            this.label1.Text = "Final";
            // 
            // msktDataFinal
            // 
            this.msktDataFinal.Location = new System.Drawing.Point(143, 35);
            this.msktDataFinal.Mask = "00/00/0000";
            this.msktDataFinal.Name = "msktDataFinal";
            this.msktDataFinal.Size = new System.Drawing.Size(79, 20);
            this.msktDataFinal.TabIndex = 176;
            this.msktDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 175;
            this.label4.Text = "Inicial:";
            // 
            // msktDataInicial
            // 
            this.msktDataInicial.Location = new System.Drawing.Point(24, 35);
            this.msktDataInicial.Mask = "00/00/0000";
            this.msktDataInicial.Name = "msktDataInicial";
            this.msktDataInicial.Size = new System.Drawing.Size(79, 20);
            this.msktDataInicial.TabIndex = 174;
            this.msktDataInicial.ValidatingType = typeof(System.DateTime);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(177, 59);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 284;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(96, 59);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 283;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.total});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 112);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGriewDados.Size = new System.Drawing.Size(798, 300);
            this.DataGriewDados.TabIndex = 292;
            this.DataGriewDados.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGriewDados_CellFormatting);
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "Outros Tipo de Pagamento";
            this.Column1.Name = "Column1";
            this.Column1.Width = 650;
            // 
            // total
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.DefaultCellStyle = dataGridViewCellStyle4;
            this.total.HeaderText = "Total R$";
            this.total.Name = "total";
            this.total.Width = 80;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rbTodos);
            this.groupBox5.Controls.Add(this.rdVenda);
            this.groupBox5.Controls.Add(this.rdOrcamento);
            this.groupBox5.Location = new System.Drawing.Point(569, 38);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(244, 44);
            this.groupBox5.TabIndex = 293;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo";
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(167, 19);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 2;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // rdVenda
            // 
            this.rdVenda.AutoSize = true;
            this.rdVenda.Checked = true;
            this.rdVenda.Location = new System.Drawing.Point(97, 19);
            this.rdVenda.Name = "rdVenda";
            this.rdVenda.Size = new System.Drawing.Size(56, 17);
            this.rdVenda.TabIndex = 1;
            this.rdVenda.TabStop = true;
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 295;
            this.label9.Text = "Vendedor:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(15, 27);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(254, 21);
            this.cbFuncionario.TabIndex = 294;
            // 
            // chkImpressaoTicket
            // 
            this.chkImpressaoTicket.AutoSize = true;
            this.chkImpressaoTicket.Checked = true;
            this.chkImpressaoTicket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImpressaoTicket.Location = new System.Drawing.Point(15, 89);
            this.chkImpressaoTicket.Name = "chkImpressaoTicket";
            this.chkImpressaoTicket.Size = new System.Drawing.Size(124, 17);
            this.chkImpressaoTicket.TabIndex = 296;
            this.chkImpressaoTicket.Text = "Impressão em Ticket";
            this.chkImpressaoTicket.UseVisualStyleBackColor = true;
            // 
            // FrmOutrosTipodePagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 424);
            this.Controls.Add(this.chkImpressaoTicket);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbFuncionario);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Name = "FrmOutrosTipodePagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumo do Caixa Diário (Tipos de Pagamentos)";
            this.Load += new System.EventHandler(this.FrmProdutosMaisVendidos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntDateSelecFinal;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox msktDataFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox msktDataInicial;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rdVenda;
        private System.Windows.Forms.RadioButton rdOrcamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.CheckBox chkImpressaoTicket;
        private System.Windows.Forms.MaskedTextBox mkdHoraFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mkdHoraInicial;
        private System.Windows.Forms.Label label24;
    }
}