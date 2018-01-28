namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmAcertoEstoque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAcertoEstoque));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvaDados = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lblRegistrosVerificados = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSeparador = new System.Windows.Forms.TextBox();
            this.lblPesquisOrigem = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaminhoBDFiredOrigem = new System.Windows.Forms.TextBox();
            this.bntCadBanco = new System.Windows.Forms.Button();
            this.btnConecAccess = new System.Windows.Forms.Button();
            this.DgBDOrigem = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBDOrigem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(99, 464);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 23);
            this.btnSair.TabIndex = 232;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvaDados
            // 
            this.btnSalvaDados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvaDados.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvaDados.Image")));
            this.btnSalvaDados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvaDados.Location = new System.Drawing.Point(13, 464);
            this.btnSalvaDados.Name = "btnSalvaDados";
            this.btnSalvaDados.Size = new System.Drawing.Size(80, 23);
            this.btnSalvaDados.TabIndex = 231;
            this.btnSalvaDados.Text = "Salvar";
            this.btnSalvaDados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvaDados.UseVisualStyleBackColor = true;
            this.btnSalvaDados.Click += new System.EventHandler(this.btnSalvaDados_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblRegistrosVerificados);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSeparador);
            this.groupBox1.Controls.Add(this.lblPesquisOrigem);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCaminhoBDFiredOrigem);
            this.groupBox1.Controls.Add(this.bntCadBanco);
            this.groupBox1.Controls.Add(this.btnConecAccess);
            this.groupBox1.Controls.Add(this.DgBDOrigem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 446);
            this.groupBox1.TabIndex = 230;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arquivo CSV ( Origem )";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(173, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 23);
            this.button2.TabIndex = 217;
            this.button2.Text = "Acerto de Estoque";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblRegistrosVerificados
            // 
            this.lblRegistrosVerificados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistrosVerificados.AutoSize = true;
            this.lblRegistrosVerificados.Location = new System.Drawing.Point(662, 96);
            this.lblRegistrosVerificados.Name = "lblRegistrosVerificados";
            this.lblRegistrosVerificados.Size = new System.Drawing.Size(118, 13);
            this.lblRegistrosVerificados.TabIndex = 216;
            this.lblRegistrosVerificados.Text = "Registros Verificados: 0";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(9, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 215;
            this.button1.Text = "Busca Estoque Atual";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(596, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 213;
            this.label3.Text = "Separador:";
            // 
            // txtSeparador
            // 
            this.txtSeparador.Location = new System.Drawing.Point(599, 32);
            this.txtSeparador.MaxLength = 1;
            this.txtSeparador.Name = "txtSeparador";
            this.txtSeparador.ReadOnly = true;
            this.txtSeparador.Size = new System.Drawing.Size(48, 20);
            this.txtSeparador.TabIndex = 214;
            this.txtSeparador.Text = ";";
            this.txtSeparador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPesquisOrigem
            // 
            this.lblPesquisOrigem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPesquisOrigem.AutoSize = true;
            this.lblPesquisOrigem.Location = new System.Drawing.Point(7, 430);
            this.lblPesquisOrigem.Name = "lblPesquisOrigem";
            this.lblPesquisOrigem.Size = new System.Drawing.Size(118, 13);
            this.lblPesquisOrigem.TabIndex = 210;
            this.lblPesquisOrigem.Text = "Número de Registros: 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "Dados do Arquivo CSV:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caminho:";
            // 
            // txtCaminhoBDFiredOrigem
            // 
            this.txtCaminhoBDFiredOrigem.Location = new System.Drawing.Point(9, 32);
            this.txtCaminhoBDFiredOrigem.Name = "txtCaminhoBDFiredOrigem";
            this.txtCaminhoBDFiredOrigem.Size = new System.Drawing.Size(441, 20);
            this.txtCaminhoBDFiredOrigem.TabIndex = 1;
            // 
            // bntCadBanco
            // 
            this.bntCadBanco.Location = new System.Drawing.Point(456, 29);
            this.bntCadBanco.Name = "bntCadBanco";
            this.bntCadBanco.Size = new System.Drawing.Size(31, 23);
            this.bntCadBanco.TabIndex = 199;
            this.bntCadBanco.Text = "...";
            this.bntCadBanco.UseVisualStyleBackColor = true;
            this.bntCadBanco.Click += new System.EventHandler(this.bntCadBanco_Click);
            // 
            // btnConecAccess
            // 
            this.btnConecAccess.Image = ((System.Drawing.Image)(resources.GetObject("btnConecAccess.Image")));
            this.btnConecAccess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConecAccess.Location = new System.Drawing.Point(493, 30);
            this.btnConecAccess.Name = "btnConecAccess";
            this.btnConecAccess.Size = new System.Drawing.Size(97, 23);
            this.btnConecAccess.TabIndex = 205;
            this.btnConecAccess.Text = "Abrir Arquivo";
            this.btnConecAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConecAccess.UseVisualStyleBackColor = true;
            this.btnConecAccess.Click += new System.EventHandler(this.btnConecAccess_Click);
            // 
            // DgBDOrigem
            // 
            this.DgBDOrigem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgBDOrigem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBDOrigem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5});
            this.DgBDOrigem.Location = new System.Drawing.Point(9, 112);
            this.DgBDOrigem.Name = "DgBDOrigem";
            this.DgBDOrigem.ReadOnly = true;
            this.DgBDOrigem.Size = new System.Drawing.Size(772, 315);
            this.DgBDOrigem.TabIndex = 200;
            // 
            // Column3
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column3.HeaderText = "Código";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Estoque Real";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Estoque Atual";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Acerto";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tipo";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 200;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmAcertoEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 499);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvaDados);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAcertoEstoque";
            this.Text = "Acerto de Estoque";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBDOrigem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvaDados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSeparador;
        private System.Windows.Forms.Label lblPesquisOrigem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaminhoBDFiredOrigem;
        private System.Windows.Forms.Button bntCadBanco;
        private System.Windows.Forms.Button btnConecAccess;
        private System.Windows.Forms.DataGridView DgBDOrigem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblRegistrosVerificados;
        private System.Windows.Forms.Button button2;
    }
}