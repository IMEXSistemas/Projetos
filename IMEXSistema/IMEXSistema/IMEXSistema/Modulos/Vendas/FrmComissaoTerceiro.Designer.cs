namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmComissaoTerceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComissaoTerceiro));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbVendedor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFuncionario = new System.Windows.Forms.ComboBox();
            this.btnAddImagem = new System.Windows.Forms.Button();
            this.btnLimpaPesquisa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumPedido = new System.Windows.Forms.Label();
            this.lblValorPedido = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtValComissao = new System.Windows.Forms.TextBox();
            this.txtPorComisVend = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOMEFUNC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORCENTAGEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbVendedor
            // 
            this.cbVendedor.FlatAppearance.BorderSize = 0;
            this.cbVendedor.Location = new System.Drawing.Point(343, 75);
            this.cbVendedor.Name = "cbVendedor";
            this.cbVendedor.Size = new System.Drawing.Size(26, 21);
            this.cbVendedor.TabIndex = 211;
            this.cbVendedor.UseVisualStyleBackColor = true;
            this.cbVendedor.Click += new System.EventHandler(this.cbVendedor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 210;
            this.label9.Text = "Funcionário:";
            // 
            // cbFuncionario
            // 
            this.cbFuncionario.FormattingEnabled = true;
            this.cbFuncionario.Location = new System.Drawing.Point(15, 75);
            this.cbFuncionario.Name = "cbFuncionario";
            this.cbFuncionario.Size = new System.Drawing.Size(322, 21);
            this.cbFuncionario.TabIndex = 0;
            // 
            // btnAddImagem
            // 
            this.btnAddImagem.Image = ((System.Drawing.Image)(resources.GetObject("btnAddImagem.Image")));
            this.btnAddImagem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddImagem.Location = new System.Drawing.Point(12, 102);
            this.btnAddImagem.Name = "btnAddImagem";
            this.btnAddImagem.Size = new System.Drawing.Size(116, 23);
            this.btnAddImagem.TabIndex = 3;
            this.btnAddImagem.Text = "&Adicionar";
            this.btnAddImagem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddImagem.UseVisualStyleBackColor = true;
            this.btnAddImagem.Click += new System.EventHandler(this.btnAddImagem_Click);
            // 
            // btnLimpaPesquisa
            // 
            this.btnLimpaPesquisa.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpaPesquisa.Image")));
            this.btnLimpaPesquisa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpaPesquisa.Location = new System.Drawing.Point(134, 102);
            this.btnLimpaPesquisa.Name = "btnLimpaPesquisa";
            this.btnLimpaPesquisa.Size = new System.Drawing.Size(116, 23);
            this.btnLimpaPesquisa.TabIndex = 4;
            this.btnLimpaPesquisa.Text = "&Limpar";
            this.btnLimpaPesquisa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpaPesquisa.UseVisualStyleBackColor = true;
            this.btnLimpaPesquisa.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 244;
            this.label1.Text = "Pedido Venda:";
            // 
            // lblNumPedido
            // 
            this.lblNumPedido.AutoSize = true;
            this.lblNumPedido.Location = new System.Drawing.Point(95, 9);
            this.lblNumPedido.Name = "lblNumPedido";
            this.lblNumPedido.Size = new System.Drawing.Size(43, 13);
            this.lblNumPedido.TabIndex = 245;
            this.lblNumPedido.Text = "000000";
            // 
            // lblValorPedido
            // 
            this.lblValorPedido.AutoSize = true;
            this.lblValorPedido.Location = new System.Drawing.Point(195, 9);
            this.lblValorPedido.Name = "lblValorPedido";
            this.lblValorPedido.Size = new System.Drawing.Size(28, 13);
            this.lblValorPedido.TabIndex = 247;
            this.lblValorPedido.Text = "0,00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 246;
            this.label3.Text = "Valor:";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(60, 34);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(85, 13);
            this.lblCliente.TabIndex = 249;
            this.lblCliente.Text = "Nome do Cliente";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 248;
            this.label4.Text = "Cliente:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(438, 59);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 253;
            this.label20.Text = "Valor Comissão:";
            // 
            // txtValComissao
            // 
            this.txtValComissao.Location = new System.Drawing.Point(441, 75);
            this.txtValComissao.MaxLength = 20;
            this.txtValComissao.Name = "txtValComissao";
            this.txtValComissao.Size = new System.Drawing.Size(75, 20);
            this.txtValComissao.TabIndex = 2;
            this.txtValComissao.Text = "0,00";
            this.txtValComissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValComissao.Validating += new System.ComponentModel.CancelEventHandler(this.txtValComissao_Validating);
            // 
            // txtPorComisVend
            // 
            this.txtPorComisVend.Location = new System.Drawing.Point(376, 75);
            this.txtPorComisVend.MaxLength = 6;
            this.txtPorComisVend.Name = "txtPorComisVend";
            this.txtPorComisVend.Size = new System.Drawing.Size(59, 20);
            this.txtPorComisVend.TabIndex = 1;
            this.txtPorComisVend.Text = "0,00";
            this.txtPorComisVend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorComisVend.Validating += new System.ComponentModel.CancelEventHandler(this.txtPorComisVend_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(373, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 252;
            this.label7.Text = "% Comissão:";
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn3,
            this.dataGridViewImageColumn1,
            this.NOMEFUNC,
            this.PORCENTAGEM,
            this.VALOR});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 131);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(606, 219);
            this.DataGriewDados.TabIndex = 5;
            this.DataGriewDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGriewDados_CellDoubleClick);
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "Editar";
            this.dataGridViewImageColumn3.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn3.Image")));
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.ReadOnly = true;
            this.dataGridViewImageColumn3.Width = 55;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Excluir";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // NOMEFUNC
            // 
            this.NOMEFUNC.DataPropertyName = "NOMEFUNC";
            this.NOMEFUNC.HeaderText = "Funcionário";
            this.NOMEFUNC.Name = "NOMEFUNC";
            this.NOMEFUNC.ReadOnly = true;
            this.NOMEFUNC.Width = 300;
            // 
            // PORCENTAGEM
            // 
            this.PORCENTAGEM.DataPropertyName = "PORCENTAGEM";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.PORCENTAGEM.DefaultCellStyle = dataGridViewCellStyle1;
            this.PORCENTAGEM.HeaderText = "%";
            this.PORCENTAGEM.Name = "PORCENTAGEM";
            this.PORCENTAGEM.ReadOnly = true;
            this.PORCENTAGEM.Width = 50;
            // 
            // VALOR
            // 
            this.VALOR.DataPropertyName = "VALOR";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.VALOR.DefaultCellStyle = dataGridViewCellStyle2;
            this.VALOR.HeaderText = "Valor";
            this.VALOR.Name = "VALOR";
            this.VALOR.ReadOnly = true;
            this.VALOR.Width = 90;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(112, 363);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 256;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(12, 363);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 255;
            this.label33.Text = "Total da pesquisa:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(579, 115);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(42, 13);
            this.linkLabel5.TabIndex = 257;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Imprimir";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(256, 102);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(116, 23);
            this.btnSair.TabIndex = 258;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmComissaoTerceiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 385);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.linkLabel5);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtValComissao);
            this.Controls.Add(this.txtPorComisVend);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblValorPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNumPedido);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLimpaPesquisa);
            this.Controls.Add(this.btnAddImagem);
            this.Controls.Add(this.cbVendedor);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbFuncionario);
            this.KeyPreview = true;
            this.Name = "FrmComissaoTerceiro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comissão de Terceiros";
            this.Load += new System.EventHandler(this.FrmComissaoTerceiro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbVendedor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFuncionario;
        private System.Windows.Forms.Button btnAddImagem;
        private System.Windows.Forms.Button btnLimpaPesquisa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumPedido;
        private System.Windows.Forms.Label lblValorPedido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtValComissao;
        private System.Windows.Forms.TextBox txtPorComisVend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEFUNC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORCENTAGEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.Button btnSair;
    }
}