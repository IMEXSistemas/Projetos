namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmVeiculosCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVeiculosCliente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtMarcaModelo = new System.Windows.Forms.TextBox();
            this.label148 = new System.Windows.Forms.Label();
            this.btnCadCor = new System.Windows.Forms.Button();
            this.cbCor = new System.Windows.Forms.ComboBox();
            this.txtChassis = new System.Windows.Forms.TextBox();
            this.label147 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label146 = new System.Windows.Forms.Label();
            this.txtAnoFab = new System.Windows.Forms.TextBox();
            this.label145 = new System.Windows.Forms.Label();
            this.DSVeiculoCliente = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.PLACA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marcamodelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANOFABR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANOMODELO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label144 = new System.Windows.Forms.Label();
            this.btnCadMarcaModelo = new System.Windows.Forms.Button();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label143 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSVeiculoCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cliente:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotalPesquisa);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.txtMarcaModelo);
            this.groupBox2.Controls.Add(this.label148);
            this.groupBox2.Controls.Add(this.btnCadCor);
            this.groupBox2.Controls.Add(this.cbCor);
            this.groupBox2.Controls.Add(this.txtChassis);
            this.groupBox2.Controls.Add(this.label147);
            this.groupBox2.Controls.Add(this.txtModelo);
            this.groupBox2.Controls.Add(this.label146);
            this.groupBox2.Controls.Add(this.txtAnoFab);
            this.groupBox2.Controls.Add(this.label145);
            this.groupBox2.Controls.Add(this.DSVeiculoCliente);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label144);
            this.groupBox2.Controls.Add(this.btnCadMarcaModelo);
            this.groupBox2.Controls.Add(this.txtPlaca);
            this.groupBox2.Controls.Add(this.label143);
            this.groupBox2.Location = new System.Drawing.Point(15, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 333);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Veículo";
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(108, 317);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 232;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 317);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(94, 13);
            this.label33.TabIndex = 231;
            this.label33.Text = "Total da pesquisa:";
            // 
            // txtMarcaModelo
            // 
            this.txtMarcaModelo.Location = new System.Drawing.Point(102, 32);
            this.txtMarcaModelo.MaxLength = 100;
            this.txtMarcaModelo.Name = "txtMarcaModelo";
            this.txtMarcaModelo.Size = new System.Drawing.Size(454, 20);
            this.txtMarcaModelo.TabIndex = 2;
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(316, 55);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(26, 13);
            this.label148.TabIndex = 229;
            this.label148.Text = "Cor:";
            // 
            // btnCadCor
            // 
            this.btnCadCor.FlatAppearance.BorderSize = 0;
            this.btnCadCor.Location = new System.Drawing.Point(577, 70);
            this.btnCadCor.Name = "btnCadCor";
            this.btnCadCor.Size = new System.Drawing.Size(26, 21);
            this.btnCadCor.TabIndex = 228;
            this.btnCadCor.UseVisualStyleBackColor = true;
            this.btnCadCor.Click += new System.EventHandler(this.btnCadCor_Click);
            // 
            // cbCor
            // 
            this.cbCor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCor.FormattingEnabled = true;
            this.cbCor.Location = new System.Drawing.Point(319, 71);
            this.cbCor.Name = "cbCor";
            this.cbCor.Size = new System.Drawing.Size(252, 21);
            this.cbCor.TabIndex = 6;
            // 
            // txtChassis
            // 
            this.txtChassis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChassis.Location = new System.Drawing.Point(10, 71);
            this.txtChassis.MaxLength = 50;
            this.txtChassis.Name = "txtChassis";
            this.txtChassis.Size = new System.Drawing.Size(303, 20);
            this.txtChassis.TabIndex = 5;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(10, 55);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(46, 13);
            this.label147.TabIndex = 226;
            this.label147.Text = "Chassis:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(620, 32);
            this.txtModelo.MaxLength = 4;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(48, 20);
            this.txtModelo.TabIndex = 4;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(618, 16);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(69, 13);
            this.label146.TabIndex = 224;
            this.label146.Text = "Ano/Modelo:";
            // 
            // txtAnoFab
            // 
            this.txtAnoFab.Location = new System.Drawing.Point(562, 32);
            this.txtAnoFab.MaxLength = 4;
            this.txtAnoFab.Name = "txtAnoFab";
            this.txtAnoFab.Size = new System.Drawing.Size(46, 20);
            this.txtAnoFab.TabIndex = 3;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(560, 16);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(52, 13);
            this.label145.TabIndex = 222;
            this.label145.Text = "Ano/Fab:";
            // 
            // DSVeiculoCliente
            // 
            this.DSVeiculoCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DSVeiculoCliente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewImageColumn2,
            this.PLACA,
            this.marcamodelo,
            this.ANOFABR,
            this.ANOMODELO});
            this.DSVeiculoCliente.Location = new System.Drawing.Point(10, 97);
            this.DSVeiculoCliente.Name = "DSVeiculoCliente";
            this.DSVeiculoCliente.ReadOnly = true;
            this.DSVeiculoCliente.Size = new System.Drawing.Size(755, 216);
            this.DSVeiculoCliente.TabIndex = 220;
            this.DSVeiculoCliente.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DSVeiculoCliente_CellDoubleClick);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Editar";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Excluir";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 50;
            // 
            // PLACA
            // 
            this.PLACA.DataPropertyName = "PLACA";
            this.PLACA.HeaderText = "Placa";
            this.PLACA.Name = "PLACA";
            this.PLACA.ReadOnly = true;
            // 
            // marcamodelo
            // 
            this.marcamodelo.DataPropertyName = "marcamodelo";
            this.marcamodelo.HeaderText = "Marca/Modelo";
            this.marcamodelo.Name = "marcamodelo";
            this.marcamodelo.ReadOnly = true;
            this.marcamodelo.Width = 350;
            // 
            // ANOFABR
            // 
            this.ANOFABR.DataPropertyName = "ANOFABR";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ANOFABR.DefaultCellStyle = dataGridViewCellStyle1;
            this.ANOFABR.HeaderText = "Ano/Fabr";
            this.ANOFABR.Name = "ANOFABR";
            this.ANOFABR.ReadOnly = true;
            this.ANOFABR.Width = 70;
            // 
            // ANOMODELO
            // 
            this.ANOMODELO.DataPropertyName = "ANOMODELO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ANOMODELO.DefaultCellStyle = dataGridViewCellStyle2;
            this.ANOMODELO.HeaderText = "Ano/Modelo";
            this.ANOMODELO.Name = "ANOMODELO";
            this.ANOMODELO.ReadOnly = true;
            this.ANOMODELO.Width = 70;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(690, 28);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdVeiculo_Click);
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(98, 16);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(80, 13);
            this.label144.TabIndex = 218;
            this.label144.Text = "Marca/Modelo:";
            // 
            // btnCadMarcaModelo
            // 
            this.btnCadMarcaModelo.FlatAppearance.BorderSize = 0;
            this.btnCadMarcaModelo.Location = new System.Drawing.Point(525, 32);
            this.btnCadMarcaModelo.Name = "btnCadMarcaModelo";
            this.btnCadMarcaModelo.Size = new System.Drawing.Size(26, 21);
            this.btnCadMarcaModelo.TabIndex = 217;
            this.btnCadMarcaModelo.UseVisualStyleBackColor = true;
            // 
            // txtPlaca
            // 
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Location = new System.Drawing.Point(10, 32);
            this.txtPlaca.MaxLength = 8;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(86, 20);
            this.txtPlaca.TabIndex = 1;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(10, 16);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(37, 13);
            this.label143.TabIndex = 4;
            this.label143.Text = "Placa:";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(15, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 220;
            this.button1.Text = "&Sair";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(15, 25);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(771, 20);
            this.txtNome.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmVeiculosCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 425);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label1);
            this.Name = "FrmVeiculosCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Veículos do Cliente";
            this.Load += new System.EventHandler(this.FrmVeiculosCliente_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSVeiculoCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Button btnCadCor;
        private System.Windows.Forms.ComboBox cbCor;
        private System.Windows.Forms.TextBox txtChassis;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.TextBox txtAnoFab;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.DataGridView DSVeiculoCliente;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLACA;
        private System.Windows.Forms.DataGridViewTextBoxColumn marcamodelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANOFABR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANOMODELO;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Button btnCadMarcaModelo;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMarcaModelo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label33;
    }
}