namespace ScriptBDBMS
{
    partial class FrmScriptDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScriptDB));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bntCadBanco = new System.Windows.Forms.Button();
            this.txtLocalBancoDados = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCountColecao = new System.Windows.Forms.Label();
            this.dataGridDados = new System.Windows.Forms.DataGridView();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnExecuta = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbComando = new System.Windows.Forms.RadioButton();
            this.rdColecao = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtScriptBD = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.bntCadBanco);
            this.panel1.Controls.Add(this.txtLocalBancoDados);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblCountColecao);
            this.panel1.Controls.Add(this.dataGridDados);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnExecuta);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rdbComando);
            this.panel1.Controls.Add(this.rdColecao);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtScriptBD);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 464);
            this.panel1.TabIndex = 0;
            // 
            // bntCadBanco
            // 
            this.bntCadBanco.Location = new System.Drawing.Point(620, 24);
            this.bntCadBanco.Name = "bntCadBanco";
            this.bntCadBanco.Size = new System.Drawing.Size(31, 23);
            this.bntCadBanco.TabIndex = 201;
            this.bntCadBanco.Text = "...";
            this.bntCadBanco.UseVisualStyleBackColor = true;
            this.bntCadBanco.Click += new System.EventHandler(this.bntCadBanco_Click);
            // 
            // txtLocalBancoDados
            // 
            this.txtLocalBancoDados.Location = new System.Drawing.Point(13, 27);
            this.txtLocalBancoDados.MaxLength = 500;
            this.txtLocalBancoDados.Name = "txtLocalBancoDados";
            this.txtLocalBancoDados.Size = new System.Drawing.Size(601, 20);
            this.txtLocalBancoDados.TabIndex = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 199;
            this.label3.Text = "Local do Banco de Dados:";
            // 
            // lblCountColecao
            // 
            this.lblCountColecao.AutoSize = true;
            this.lblCountColecao.Location = new System.Drawing.Point(19, 439);
            this.lblCountColecao.Name = "lblCountColecao";
            this.lblCountColecao.Size = new System.Drawing.Size(118, 13);
            this.lblCountColecao.TabIndex = 8;
            this.lblCountColecao.Text = "Número de Registros: 0";
            // 
            // dataGridDados
            // 
            this.dataGridDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDados.Location = new System.Drawing.Point(19, 300);
            this.dataGridDados.Name = "dataGridDados";
            this.dataGridDados.ReadOnly = true;
            this.dataGridDados.Size = new System.Drawing.Size(632, 132);
            this.dataGridDados.TabIndex = 7;
            // 
            // btnSair
            // 
            this.btnSair.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(118, 265);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 6;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnExecuta
            // 
            this.btnExecuta.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuta.Image")));
            this.btnExecuta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecuta.Location = new System.Drawing.Point(19, 265);
            this.btnExecuta.Name = "btnExecuta";
            this.btnExecuta.Size = new System.Drawing.Size(93, 23);
            this.btnExecuta.TabIndex = 1;
            this.btnExecuta.Text = "&Executar ";
            this.btnExecuta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecuta.UseVisualStyleBackColor = true;
            this.btnExecuta.Click += new System.EventHandler(this.btnExecuta_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Script de Execução";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // rdbComando
            // 
            this.rdbComando.AutoSize = true;
            this.rdbComando.Location = new System.Drawing.Point(128, 238);
            this.rdbComando.Name = "rdbComando";
            this.rdbComando.Size = new System.Drawing.Size(115, 17);
            this.rdbComando.TabIndex = 3;
            this.rdbComando.Text = "Executar Comando";
            this.rdbComando.UseVisualStyleBackColor = true;
            // 
            // rdColecao
            // 
            this.rdColecao.AutoSize = true;
            this.rdColecao.Checked = true;
            this.rdColecao.Location = new System.Drawing.Point(13, 238);
            this.rdColecao.Name = "rdColecao";
            this.rdColecao.Size = new System.Drawing.Size(109, 17);
            this.rdColecao.TabIndex = 2;
            this.rdColecao.TabStop = true;
            this.rdColecao.Text = "Executar Coleção";
            this.rdColecao.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(16, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ATENÇÃO: Utilize Esta Tela Somente com Ajuda do Suporte.";
            // 
            // txtScriptBD
            // 
            this.txtScriptBD.Location = new System.Drawing.Point(13, 76);
            this.txtScriptBD.Multiline = true;
            this.txtScriptBD.Name = "txtScriptBD";
            this.txtScriptBD.Size = new System.Drawing.Size(638, 130);
            this.txtScriptBD.TabIndex = 0;
            this.txtScriptBD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScriptBD_KeyDown);
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
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(547, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 208;
            this.button1.Text = "&Limpar Script";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmScriptDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 464);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmScriptDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Banco de Dados";
            this.Load += new System.EventHandler(this.FrmScriptDBBMS_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbComando;
        private System.Windows.Forms.RadioButton rdColecao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScriptBD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExecuta;
        private System.Windows.Forms.DataGridView dataGridDados;
        private System.Windows.Forms.Label lblCountColecao;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button bntCadBanco;
        private System.Windows.Forms.TextBox txtLocalBancoDados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button button1;
    }
}