namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmItensVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItensVenda));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgItensCupom = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomeproduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORUNITARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORTOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNumItens = new System.Windows.Forms.Label();
            this.cbProduto = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.txtQuant = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtVlTotal = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtvalorunit = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgItensCupom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgItensCupom
            // 
            this.dtgItensCupom.AllowUserToAddRows = false;
            this.dtgItensCupom.AllowUserToDeleteRows = false;
            this.dtgItensCupom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgItensCupom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgItensCupom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn4,
            this.dataGridViewImageColumn5,
            this.Item,
            this.nomeproduto,
            this.QUANTIDADE,
            this.VALORUNITARIO,
            this.VALORTOTAL});
            this.dtgItensCupom.Location = new System.Drawing.Point(12, 77);
            this.dtgItensCupom.Name = "dtgItensCupom";
            this.dtgItensCupom.Size = new System.Drawing.Size(821, 338);
            this.dtgItensCupom.TabIndex = 310;
            this.dtgItensCupom.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgItensCupom_CellContentClick);
            this.dtgItensCupom.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgItensCupom_CellDoubleClick);
            this.dtgItensCupom.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtgItensCupom_CellFormatting);
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.HeaderText = "Editar";
            this.dataGridViewImageColumn4.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn4.Image")));
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.ReadOnly = true;
            this.dataGridViewImageColumn4.Width = 50;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "Excluir";
            this.dataGridViewImageColumn5.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn5.Image")));
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            this.dataGridViewImageColumn5.Width = 50;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Item.DefaultCellStyle = dataGridViewCellStyle1;
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.Width = 50;
            // 
            // nomeproduto
            // 
            this.nomeproduto.DataPropertyName = "NOMEPRODUTO";
            this.nomeproduto.HeaderText = "Produto";
            this.nomeproduto.Name = "nomeproduto";
            this.nomeproduto.ReadOnly = true;
            this.nomeproduto.Width = 400;
            // 
            // QUANTIDADE
            // 
            this.QUANTIDADE.DataPropertyName = "QUANTIDADE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTIDADE.DefaultCellStyle = dataGridViewCellStyle2;
            this.QUANTIDADE.HeaderText = "Quant.";
            this.QUANTIDADE.Name = "QUANTIDADE";
            this.QUANTIDADE.ReadOnly = true;
            this.QUANTIDADE.Width = 50;
            // 
            // VALORUNITARIO
            // 
            this.VALORUNITARIO.DataPropertyName = "VALORUNITARIO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.VALORUNITARIO.DefaultCellStyle = dataGridViewCellStyle3;
            this.VALORUNITARIO.HeaderText = "Vl. Unit.";
            this.VALORUNITARIO.Name = "VALORUNITARIO";
            this.VALORUNITARIO.ReadOnly = true;
            this.VALORUNITARIO.Width = 80;
            // 
            // VALORTOTAL
            // 
            this.VALORTOTAL.DataPropertyName = "VALORTOTAL";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.VALORTOTAL.DefaultCellStyle = dataGridViewCellStyle4;
            this.VALORTOTAL.HeaderText = "Vl. Total";
            this.VALORTOTAL.Name = "VALORTOTAL";
            this.VALORTOTAL.ReadOnly = true;
            this.VALORTOTAL.Width = 80;
            // 
            // lblNumItens
            // 
            this.lblNumItens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumItens.AutoSize = true;
            this.lblNumItens.Location = new System.Drawing.Point(12, 431);
            this.lblNumItens.Name = "lblNumItens";
            this.lblNumItens.Size = new System.Drawing.Size(72, 13);
            this.lblNumItens.TabIndex = 313;
            this.lblNumItens.Text = "Nº de Itens: 0";
            // 
            // cbProduto
            // 
            this.cbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProduto.FormattingEnabled = true;
            this.cbProduto.Location = new System.Drawing.Point(15, 25);
            this.cbProduto.Name = "cbProduto";
            this.cbProduto.Size = new System.Drawing.Size(605, 21);
            this.cbProduto.TabIndex = 314;
            this.cbProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProduto_KeyDown);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(12, 9);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(52, 13);
            this.label58.TabIndex = 315;
            this.label58.Text = "Produtos:";
            // 
            // txtQuant
            // 
            this.txtQuant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuant.Location = new System.Drawing.Point(626, 25);
            this.txtQuant.MaxLength = 5;
            this.txtQuant.Name = "txtQuant";
            this.txtQuant.Size = new System.Drawing.Size(59, 20);
            this.txtQuant.TabIndex = 316;
            this.txtQuant.Text = "1,00";
            this.txtQuant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuant.Leave += new System.EventHandler(this.txtQuant_Leave);
            this.txtQuant.Validating += new System.ComponentModel.CancelEventHandler(this.txtQuant_Validating);
            // 
            // label57
            // 
            this.label57.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(623, 10);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(42, 13);
            this.label57.TabIndex = 317;
            this.label57.Text = "Quant.:";
            // 
            // txtVlTotal
            // 
            this.txtVlTotal.Location = new System.Drawing.Point(766, 25);
            this.txtVlTotal.MaxLength = 20;
            this.txtVlTotal.Name = "txtVlTotal";
            this.txtVlTotal.ReadOnly = true;
            this.txtVlTotal.Size = new System.Drawing.Size(65, 20);
            this.txtVlTotal.TabIndex = 321;
            this.txtVlTotal.Text = "0,00";
            this.txtVlTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(763, 10);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(49, 13);
            this.label48.TabIndex = 322;
            this.label48.Text = "Vl. Total ";
            // 
            // txtvalorunit
            // 
            this.txtvalorunit.Location = new System.Drawing.Point(691, 25);
            this.txtvalorunit.MaxLength = 20;
            this.txtvalorunit.Name = "txtvalorunit";
            this.txtvalorunit.Size = new System.Drawing.Size(70, 20);
            this.txtvalorunit.TabIndex = 320;
            this.txtvalorunit.Text = "0,00";
            this.txtvalorunit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvalorunit.Leave += new System.EventHandler(this.txtvalorunit_Leave);
            this.txtvalorunit.Validating += new System.ComponentModel.CancelEventHandler(this.txtvalorunit_Validating);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(688, 10);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(61, 13);
            this.label56.TabIndex = 319;
            this.label56.Text = "Vl. Unitário:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(677, 51);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 323;
            this.btnSalvar.Text = "&Alterar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpar.Image")));
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.Location = new System.Drawing.Point(756, 51);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 324;
            this.btnLimpar.Text = "&Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(758, 421);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 325;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FrmItensVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 453);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtVlTotal);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.txtvalorunit);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.txtQuant);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.cbProduto);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.lblNumItens);
            this.Controls.Add(this.dtgItensCupom);
            this.Name = "FrmItensVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Itens da Venda";
            this.Load += new System.EventHandler(this.FrmItensVenda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgItensCupom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgItensCupom;
        private System.Windows.Forms.Label lblNumItens;
        private System.Windows.Forms.ComboBox cbProduto;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox txtQuant;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtVlTotal;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtvalorunit;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeproduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORUNITARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORTOTAL;
    }
}