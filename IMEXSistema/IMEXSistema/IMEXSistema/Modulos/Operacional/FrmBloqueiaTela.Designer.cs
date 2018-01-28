namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmBloqueiaTela
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBloqueiaTela));
            this.cbTela = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.cbusuario = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.DtBloqueio = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOMETELA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEUSUARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltTotalRegistro = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DtBloqueio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbTela
            // 
            this.cbTela.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTela.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTela.FormattingEnabled = true;
            this.cbTela.Location = new System.Drawing.Point(12, 24);
            this.cbTela.Name = "cbTela";
            this.cbTela.Size = new System.Drawing.Size(367, 21);
            this.cbTela.TabIndex = 305;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(12, 9);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(31, 13);
            this.label58.TabIndex = 306;
            this.label58.Text = "Tela:";
            // 
            // cbusuario
            // 
            this.cbusuario.FormattingEnabled = true;
            this.cbusuario.Location = new System.Drawing.Point(385, 24);
            this.cbusuario.Name = "cbusuario";
            this.cbusuario.Size = new System.Drawing.Size(336, 21);
            this.cbusuario.TabIndex = 307;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 308;
            this.label4.Text = "Usuário com Permissão:";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(12, 51);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 309;
            this.btnAdd.Text = "&Salvar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(93, 51);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 310;
            this.btnSair.Text = "S&air";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // DtBloqueio
            // 
            this.DtBloqueio.AllowUserToAddRows = false;
            this.DtBloqueio.AllowUserToDeleteRows = false;
            this.DtBloqueio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DtBloqueio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtBloqueio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn5,
            this.NOMETELA,
            this.NOMEUSUARIO});
            this.DtBloqueio.Location = new System.Drawing.Point(15, 80);
            this.DtBloqueio.Name = "DtBloqueio";
            this.DtBloqueio.Size = new System.Drawing.Size(697, 256);
            this.DtBloqueio.TabIndex = 311;
            this.DtBloqueio.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtBloqueio_CellDoubleClick);
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "Excluir";
            this.dataGridViewImageColumn5.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn5.Image")));
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            this.dataGridViewImageColumn5.Width = 50;
            // 
            // NOMETELA
            // 
            this.NOMETELA.DataPropertyName = "NOMETELA";
            this.NOMETELA.HeaderText = "Tela";
            this.NOMETELA.Name = "NOMETELA";
            this.NOMETELA.ReadOnly = true;
            this.NOMETELA.Width = 300;
            // 
            // NOMEUSUARIO
            // 
            this.NOMEUSUARIO.DataPropertyName = "NOMEUSUARIO";
            this.NOMEUSUARIO.HeaderText = "Usuário com Permissão:";
            this.NOMEUSUARIO.Name = "NOMEUSUARIO";
            this.NOMEUSUARIO.ReadOnly = true;
            this.NOMEUSUARIO.Width = 300;
            // 
            // lbltTotalRegistro
            // 
            this.lbltTotalRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbltTotalRegistro.AutoSize = true;
            this.lbltTotalRegistro.Location = new System.Drawing.Point(12, 341);
            this.lbltTotalRegistro.Name = "lbltTotalRegistro";
            this.lbltTotalRegistro.Size = new System.Drawing.Size(105, 13);
            this.lbltTotalRegistro.TabIndex = 312;
            this.lbltTotalRegistro.Text = "Total de Registros: 0";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmBloqueiaTela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 363);
            this.Controls.Add(this.lbltTotalRegistro);
            this.Controls.Add(this.DtBloqueio);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbusuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTela);
            this.Controls.Add(this.label58);
            this.Name = "FrmBloqueiaTela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloqueio de Tela";
            this.Load += new System.EventHandler(this.FrmBloqueiaTela_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtBloqueio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTela;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox cbusuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView DtBloqueio;
        private System.Windows.Forms.Label lbltTotalRegistro;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMETELA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEUSUARIO;
    }
}