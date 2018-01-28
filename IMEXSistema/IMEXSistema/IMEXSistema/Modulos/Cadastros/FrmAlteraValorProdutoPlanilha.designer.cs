namespace BmsSoftware.Modulos.Telemarketing
{
    partial class FrmAlteraValorProdutoPlanilha
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPrecoVenda3 = new System.Windows.Forms.RadioButton();
            this.rbPrecoVenda2 = new System.Windows.Forms.RadioButton();
            this.rbPrecoVenda1 = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvaDados = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPesquisOrigem = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaminhoBDFiredOrigem = new System.Windows.Forms.TextBox();
            this.bntCadBanco = new System.Windows.Forms.Button();
            this.txtSheet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConecAccess = new System.Windows.Forms.Button();
            this.DgBDOrigem = new System.Windows.Forms.DataGridView();
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
            this.panel1.Controls.Add(this.rbPrecoVenda3);
            this.panel1.Controls.Add(this.rbPrecoVenda2);
            this.panel1.Controls.Add(this.rbPrecoVenda1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnSalvaDados);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 495);
            this.panel1.TabIndex = 0;
            // 
            // rbPrecoVenda3
            // 
            this.rbPrecoVenda3.AutoSize = true;
            this.rbPrecoVenda3.Location = new System.Drawing.Point(529, 434);
            this.rbPrecoVenda3.Name = "rbPrecoVenda3";
            this.rbPrecoVenda3.Size = new System.Drawing.Size(96, 17);
            this.rbPrecoVenda3.TabIndex = 233;
            this.rbPrecoVenda3.Text = "Preço Venda 3";
            this.rbPrecoVenda3.UseVisualStyleBackColor = true;
            // 
            // rbPrecoVenda2
            // 
            this.rbPrecoVenda2.AutoSize = true;
            this.rbPrecoVenda2.Location = new System.Drawing.Point(427, 434);
            this.rbPrecoVenda2.Name = "rbPrecoVenda2";
            this.rbPrecoVenda2.Size = new System.Drawing.Size(96, 17);
            this.rbPrecoVenda2.TabIndex = 232;
            this.rbPrecoVenda2.Text = "Preço Venda 2";
            this.rbPrecoVenda2.UseVisualStyleBackColor = true;
            // 
            // rbPrecoVenda1
            // 
            this.rbPrecoVenda1.AutoSize = true;
            this.rbPrecoVenda1.Checked = true;
            this.rbPrecoVenda1.Location = new System.Drawing.Point(325, 434);
            this.rbPrecoVenda1.Name = "rbPrecoVenda1";
            this.rbPrecoVenda1.Size = new System.Drawing.Size(96, 17);
            this.rbPrecoVenda1.TabIndex = 231;
            this.rbPrecoVenda1.TabStop = true;
            this.rbPrecoVenda1.Text = "Preço Venda 1";
            this.rbPrecoVenda1.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 460);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(747, 23);
            this.progressBar1.TabIndex = 230;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(145, 431);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 229;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnSalvaDados
            // 
            this.btnSalvaDados.Location = new System.Drawing.Point(12, 431);
            this.btnSalvaDados.Name = "btnSalvaDados";
            this.btnSalvaDados.Size = new System.Drawing.Size(127, 23);
            this.btnSalvaDados.TabIndex = 228;
            this.btnSalvaDados.Text = "Salvar Dados";
            this.btnSalvaDados.UseVisualStyleBackColor = true;
            this.btnSalvaDados.Click += new System.EventHandler(this.btnSalvaDados_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPesquisOrigem);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCaminhoBDFiredOrigem);
            this.groupBox1.Controls.Add(this.bntCadBanco);
            this.groupBox1.Controls.Add(this.txtSheet);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnConecAccess);
            this.groupBox1.Controls.Add(this.DgBDOrigem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 413);
            this.groupBox1.TabIndex = 212;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel ( Origem )";
            // 
            // lblPesquisOrigem
            // 
            this.lblPesquisOrigem.AutoSize = true;
            this.lblPesquisOrigem.Location = new System.Drawing.Point(9, 385);
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
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "Dados:";
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
            // txtSheet
            // 
            this.txtSheet.Location = new System.Drawing.Point(596, 32);
            this.txtSheet.MaxLength = 50;
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(141, 20);
            this.txtSheet.TabIndex = 207;
            this.txtSheet.Text = "plan1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(593, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 208;
            this.label3.Text = "Sheet:";
            // 
            // btnConecAccess
            // 
            this.btnConecAccess.Location = new System.Drawing.Point(493, 30);
            this.btnConecAccess.Name = "btnConecAccess";
            this.btnConecAccess.Size = new System.Drawing.Size(97, 23);
            this.btnConecAccess.TabIndex = 205;
            this.btnConecAccess.Text = "Conectar BD";
            this.btnConecAccess.UseVisualStyleBackColor = true;
            this.btnConecAccess.Click += new System.EventHandler(this.btnConecAccess_Click);
            // 
            // DgBDOrigem
            // 
            this.DgBDOrigem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBDOrigem.Location = new System.Drawing.Point(9, 80);
            this.DgBDOrigem.Name = "DgBDOrigem";
            this.DgBDOrigem.ReadOnly = true;
            this.DgBDOrigem.Size = new System.Drawing.Size(718, 302);
            this.DgBDOrigem.TabIndex = 200;
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
            // FrmAlteraValorProdutoPlanilha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 495);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAlteraValorProdutoPlanilha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Altera Preço por Planilha";
            this.Load += new System.EventHandler(this.FrmPreClientePlanilha_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtSheet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConecAccess;
        private System.Windows.Forms.DataGridView DgBDOrigem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvaDados;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton rbPrecoVenda3;
        private System.Windows.Forms.RadioButton rbPrecoVenda2;
        private System.Windows.Forms.RadioButton rbPrecoVenda1;
    }
}