namespace BmsSoftware.Modulos.Servicos
{
    partial class FrmListaManutencao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaManutencao));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.maskedtxtDataProxManut = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.maskedtxtData = new System.Windows.Forms.MaskedTextBox();
            this.bntDateSelecInicial = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.btnCadEsquip = new System.Windows.Forms.Button();
            this.cbEquipamento = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bntDateSelecProxManut2 = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.chkSomaProduto = new System.Windows.Forms.CheckBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IDMANUTEESQUIPAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEEQUIPAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAMANUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMETIPOMANUTENCAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALORMANUTENCAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblObsField = new System.Windows.Forms.Label();
            this.chkEmpresContrata = new System.Windows.Forms.CheckBox();
            this.cbTipoManutencao = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // maskedtxtDataProxManut
            // 
            this.maskedtxtDataProxManut.Location = new System.Drawing.Point(140, 32);
            this.maskedtxtDataProxManut.Mask = "00/00/0000";
            this.maskedtxtDataProxManut.Name = "maskedtxtDataProxManut";
            this.maskedtxtDataProxManut.Size = new System.Drawing.Size(79, 20);
            this.maskedtxtDataProxManut.TabIndex = 259;
            this.maskedtxtDataProxManut.ValidatingType = typeof(System.DateTime);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(137, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 264;
            this.label11.Text = "Data Final::";
            // 
            // maskedtxtData
            // 
            this.maskedtxtData.Location = new System.Drawing.Point(23, 32);
            this.maskedtxtData.Mask = "00/00/0000";
            this.maskedtxtData.Name = "maskedtxtData";
            this.maskedtxtData.Size = new System.Drawing.Size(79, 20);
            this.maskedtxtData.TabIndex = 258;
            this.maskedtxtData.ValidatingType = typeof(System.DateTime);
            // 
            // bntDateSelecInicial
            // 
            this.bntDateSelecInicial.FlatAppearance.BorderSize = 0;
            this.bntDateSelecInicial.Location = new System.Drawing.Point(108, 32);
            this.bntDateSelecInicial.Name = "bntDateSelecInicial";
            this.bntDateSelecInicial.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecInicial.TabIndex = 263;
            this.bntDateSelecInicial.UseVisualStyleBackColor = true;
            this.bntDateSelecInicial.Click += new System.EventHandler(this.bntDateSelecInicial_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 17);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(63, 13);
            this.label25.TabIndex = 262;
            this.label25.Text = "Data Inicial:";
            // 
            // btnCadEsquip
            // 
            this.btnCadEsquip.FlatAppearance.BorderSize = 0;
            this.btnCadEsquip.Location = new System.Drawing.Point(415, 24);
            this.btnCadEsquip.Name = "btnCadEsquip";
            this.btnCadEsquip.Size = new System.Drawing.Size(26, 21);
            this.btnCadEsquip.TabIndex = 261;
            this.btnCadEsquip.UseVisualStyleBackColor = true;
            this.btnCadEsquip.Click += new System.EventHandler(this.btnCadEsquip_Click);
            // 
            // cbEquipamento
            // 
            this.cbEquipamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEquipamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEquipamento.FormattingEnabled = true;
            this.cbEquipamento.Location = new System.Drawing.Point(15, 24);
            this.cbEquipamento.Name = "cbEquipamento";
            this.cbEquipamento.Size = new System.Drawing.Size(394, 21);
            this.cbEquipamento.TabIndex = 257;
            this.cbEquipamento.Enter += new System.EventHandler(this.cbEquipamento_Enter);
            this.cbEquipamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbEquipamento_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 9);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(138, 13);
            this.label21.TabIndex = 260;
            this.label21.Text = "Descrição do Equipamento:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bntDateSelecProxManut2);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.bntDateSelecInicial);
            this.groupBox1.Controls.Add(this.maskedtxtDataProxManut);
            this.groupBox1.Controls.Add(this.maskedtxtData);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(447, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 70);
            this.groupBox1.TabIndex = 266;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            // 
            // bntDateSelecProxManut2
            // 
            this.bntDateSelecProxManut2.FlatAppearance.BorderSize = 0;
            this.bntDateSelecProxManut2.Location = new System.Drawing.Point(225, 31);
            this.bntDateSelecProxManut2.Name = "bntDateSelecProxManut2";
            this.bntDateSelecProxManut2.Size = new System.Drawing.Size(26, 21);
            this.bntDateSelecProxManut2.TabIndex = 265;
            this.bntDateSelecProxManut2.UseVisualStyleBackColor = true;
            this.bntDateSelecProxManut2.Click += new System.EventHandler(this.bntDateSelecProxManut2_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(14, 140);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 267;
            this.btnConsultar.Text = "&Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSair
            // 
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(176, 140);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 268;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // chkSomaProduto
            // 
            this.chkSomaProduto.AutoSize = true;
            this.chkSomaProduto.Location = new System.Drawing.Point(14, 96);
            this.chkSomaProduto.Name = "chkSomaProduto";
            this.chkSomaProduto.Size = new System.Drawing.Size(177, 17);
            this.chkSomaProduto.TabIndex = 269;
            this.chkSomaProduto.Text = "Exibir Total de Produtos Usados";
            this.chkSomaProduto.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(95, 140);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 272;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDMANUTEESQUIPAMENTO,
            this.NOMEEQUIPAMENTO,
            this.DATAMANUT,
            this.NOMETIPOMANUTENCAO,
            this.VALORMANUTENCAO});
            this.dataGridView1.Location = new System.Drawing.Point(14, 173);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(791, 359);
            this.dataGridView1.TabIndex = 273;
            // 
            // IDMANUTEESQUIPAMENTO
            // 
            this.IDMANUTEESQUIPAMENTO.DataPropertyName = "IDMANUTEESQUIPAMENTO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDMANUTEESQUIPAMENTO.DefaultCellStyle = dataGridViewCellStyle1;
            this.IDMANUTEESQUIPAMENTO.HeaderText = "Controle";
            this.IDMANUTEESQUIPAMENTO.Name = "IDMANUTEESQUIPAMENTO";
            this.IDMANUTEESQUIPAMENTO.ReadOnly = true;
            // 
            // NOMEEQUIPAMENTO
            // 
            this.NOMEEQUIPAMENTO.DataPropertyName = "NOMEEQUIPAMENTO";
            this.NOMEEQUIPAMENTO.HeaderText = "Nome Equipamento";
            this.NOMEEQUIPAMENTO.Name = "NOMEEQUIPAMENTO";
            this.NOMEEQUIPAMENTO.ReadOnly = true;
            this.NOMEEQUIPAMENTO.Width = 250;
            // 
            // DATAMANUT
            // 
            this.DATAMANUT.DataPropertyName = "DATAMANUT";
            this.DATAMANUT.HeaderText = "Data Manutenção";
            this.DATAMANUT.Name = "DATAMANUT";
            this.DATAMANUT.ReadOnly = true;
            this.DATAMANUT.Width = 110;
            // 
            // NOMETIPOMANUTENCAO
            // 
            this.NOMETIPOMANUTENCAO.DataPropertyName = "NOMETIPOMANUTENCAO";
            this.NOMETIPOMANUTENCAO.HeaderText = "Tipo Manutenção";
            this.NOMETIPOMANUTENCAO.Name = "NOMETIPOMANUTENCAO";
            this.NOMETIPOMANUTENCAO.ReadOnly = true;
            this.NOMETIPOMANUTENCAO.Width = 170;
            // 
            // VALORMANUTENCAO
            // 
            this.VALORMANUTENCAO.DataPropertyName = "VALORMANUTENCAO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.VALORMANUTENCAO.DefaultCellStyle = dataGridViewCellStyle2;
            this.VALORMANUTENCAO.HeaderText = "Valor";
            this.VALORMANUTENCAO.Name = "VALORMANUTENCAO";
            this.VALORMANUTENCAO.ReadOnly = true;
            // 
            // lblObsField
            // 
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(11, 545);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 274;
            this.lblObsField.Text = "Obs.:";
            // 
            // chkEmpresContrata
            // 
            this.chkEmpresContrata.AutoSize = true;
            this.chkEmpresContrata.Location = new System.Drawing.Point(14, 119);
            this.chkEmpresContrata.Name = "chkEmpresContrata";
            this.chkEmpresContrata.Size = new System.Drawing.Size(150, 17);
            this.chkEmpresContrata.TabIndex = 275;
            this.chkEmpresContrata.Text = "Exibir Empresa Contratada";
            this.chkEmpresContrata.UseVisualStyleBackColor = true;
            // 
            // cbTipoManutencao
            // 
            this.cbTipoManutencao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTipoManutencao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTipoManutencao.FormattingEnabled = true;
            this.cbTipoManutencao.Location = new System.Drawing.Point(15, 64);
            this.cbTipoManutencao.Name = "cbTipoManutencao";
            this.cbTipoManutencao.Size = new System.Drawing.Size(394, 21);
            this.cbTipoManutencao.TabIndex = 276;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 277;
            this.label3.Text = "Tipo de Manutenção:";
            // 
            // FrmListaManutencao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 567);
            this.Controls.Add(this.cbTipoManutencao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkEmpresContrata);
            this.Controls.Add(this.lblObsField);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.chkSomaProduto);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCadEsquip);
            this.Controls.Add(this.cbEquipamento);
            this.Controls.Add(this.label21);
            this.Name = "FrmListaManutencao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumo da Manutenção";
            this.Load += new System.EventHandler(this.FrmListaManutencao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedtxtDataProxManut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox maskedtxtData;
        private System.Windows.Forms.Button bntDateSelecInicial;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnCadEsquip;
        private System.Windows.Forms.ComboBox cbEquipamento;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntDateSelecProxManut2;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.CheckBox chkSomaProduto;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.CheckBox chkEmpresContrata;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDMANUTEESQUIPAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEEQUIPAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAMANUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMETIPOMANUTENCAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALORMANUTENCAO;
        private System.Windows.Forms.ComboBox cbTipoManutencao;
        private System.Windows.Forms.Label label3;
    }
}