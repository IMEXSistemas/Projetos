namespace BmsSoftware.Modulos.Telemarketing
{
    partial class FrmClienteArquivoCSV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClienteArquivoCSV));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvaDados = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSeparador = new System.Windows.Forms.TextBox();
            this.lblPesquisOrigem = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaminhoBDFiredOrigem = new System.Windows.Forms.TextBox();
            this.bntCadBanco = new System.Windows.Forms.Button();
            this.btnConecAccess = new System.Windows.Forms.Button();
            this.DgBDOrigem = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBDOrigem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnSalvaDados);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 355);
            this.panel1.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(101, 316);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 23);
            this.btnSair.TabIndex = 229;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvaDados
            // 
            this.btnSalvaDados.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvaDados.Image")));
            this.btnSalvaDados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvaDados.Location = new System.Drawing.Point(15, 316);
            this.btnSalvaDados.Name = "btnSalvaDados";
            this.btnSalvaDados.Size = new System.Drawing.Size(80, 23);
            this.btnSalvaDados.TabIndex = 228;
            this.btnSalvaDados.Text = "Salvar";
            this.btnSalvaDados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvaDados.UseVisualStyleBackColor = true;
            this.btnSalvaDados.Click += new System.EventHandler(this.btnSalvaDados_Click);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(750, 298);
            this.groupBox1.TabIndex = 212;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arquivo CSV ( Origem )";
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
            this.lblPesquisOrigem.AutoSize = true;
            this.lblPesquisOrigem.Location = new System.Drawing.Point(6, 279);
            this.lblPesquisOrigem.Name = "lblPesquisOrigem";
            this.lblPesquisOrigem.Size = new System.Drawing.Size(118, 13);
            this.lblPesquisOrigem.TabIndex = 210;
            this.lblPesquisOrigem.Text = "Número de Registros: 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
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
            this.DgBDOrigem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBDOrigem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1});
            this.DgBDOrigem.Location = new System.Drawing.Point(9, 80);
            this.DgBDOrigem.Name = "DgBDOrigem";
            this.DgBDOrigem.ReadOnly = true;
            this.DgBDOrigem.Size = new System.Drawing.Size(718, 193);
            this.DgBDOrigem.TabIndex = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nome";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 450;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Telefone";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmClienteArquivoCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 355);
            this.Controls.Add(this.panel1);
            this.Name = "FrmClienteArquivoCSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Cliente Por CSV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPreClienteArquivoCSV_FormClosing);
            this.Load += new System.EventHandler(this.FrmPreClientePlanilha_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBDOrigem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPesquisOrigem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaminhoBDFiredOrigem;
        private System.Windows.Forms.Button bntCadBanco;
        private System.Windows.Forms.Button btnConecAccess;
        private System.Windows.Forms.DataGridView DgBDOrigem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvaDados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSeparador;
    }
}