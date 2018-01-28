namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmIniciarContador
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
            this.cbTabela = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExecutarComando = new System.Windows.Forms.Button();
            this.txtComando1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComando2 = new System.Windows.Forms.TextBox();
            this.cbChavePrimaria = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExecTodaTabelas = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chErro = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbTabela
            // 
            this.cbTabela.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTabela.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTabela.FormattingEnabled = true;
            this.cbTabela.Location = new System.Drawing.Point(15, 25);
            this.cbTabela.Name = "cbTabela";
            this.cbTabela.Size = new System.Drawing.Size(365, 21);
            this.cbTabela.TabIndex = 217;
            this.cbTabela.SelectedValueChanged += new System.EventHandler(this.cbTabela_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 216;
            this.label6.Text = "Nome Tabela:";
            // 
            // btnExecutarComando
            // 
            this.btnExecutarComando.Location = new System.Drawing.Point(12, 255);
            this.btnExecutarComando.Name = "btnExecutarComando";
            this.btnExecutarComando.Size = new System.Drawing.Size(181, 23);
            this.btnExecutarComando.TabIndex = 218;
            this.btnExecutarComando.Text = "Executar Tabela Selecionada";
            this.btnExecutarComando.UseVisualStyleBackColor = true;
            this.btnExecutarComando.Click += new System.EventHandler(this.btnExecutarComando_Click);
            // 
            // txtComando1
            // 
            this.txtComando1.Enabled = false;
            this.txtComando1.Location = new System.Drawing.Point(15, 105);
            this.txtComando1.Multiline = true;
            this.txtComando1.Name = "txtComando1";
            this.txtComando1.Size = new System.Drawing.Size(365, 43);
            this.txtComando1.TabIndex = 219;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 220;
            this.label1.Text = "Comando 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 222;
            this.label2.Text = "Comando 2:";
            // 
            // txtComando2
            // 
            this.txtComando2.Enabled = false;
            this.txtComando2.Location = new System.Drawing.Point(15, 167);
            this.txtComando2.Multiline = true;
            this.txtComando2.Name = "txtComando2";
            this.txtComando2.Size = new System.Drawing.Size(365, 82);
            this.txtComando2.TabIndex = 221;
            // 
            // cbChavePrimaria
            // 
            this.cbChavePrimaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbChavePrimaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbChavePrimaria.FormattingEnabled = true;
            this.cbChavePrimaria.Location = new System.Drawing.Point(15, 65);
            this.cbChavePrimaria.Name = "cbChavePrimaria";
            this.cbChavePrimaria.Size = new System.Drawing.Size(365, 21);
            this.cbChavePrimaria.TabIndex = 224;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 223;
            this.label3.Text = "Chave Primária:";
            // 
            // btnExecTodaTabelas
            // 
            this.btnExecTodaTabelas.Location = new System.Drawing.Point(199, 255);
            this.btnExecTodaTabelas.Name = "btnExecTodaTabelas";
            this.btnExecTodaTabelas.Size = new System.Drawing.Size(181, 23);
            this.btnExecTodaTabelas.TabIndex = 225;
            this.btnExecTodaTabelas.Text = "Executar Todas as Tabelas";
            this.btnExecTodaTabelas.UseVisualStyleBackColor = true;
            this.btnExecTodaTabelas.Click += new System.EventHandler(this.btnExecTodaTabelas_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 286);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(365, 23);
            this.progressBar1.TabIndex = 226;
            // 
            // chErro
            // 
            this.chErro.AutoSize = true;
            this.chErro.Location = new System.Drawing.Point(15, 315);
            this.chErro.Name = "chErro";
            this.chErro.Size = new System.Drawing.Size(76, 17);
            this.chErro.TabIndex = 227;
            this.chErro.Text = "Exibir Erro ";
            this.chErro.UseVisualStyleBackColor = true;
            // 
            // FrmIniciarContador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 339);
            this.Controls.Add(this.chErro);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExecTodaTabelas);
            this.Controls.Add(this.cbChavePrimaria);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComando2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtComando1);
            this.Controls.Add(this.btnExecutarComando);
            this.Controls.Add(this.cbTabela);
            this.Controls.Add(this.label6);
            this.Name = "FrmIniciarContador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Contador de Registros";
            this.Load += new System.EventHandler(this.FrmIniciarContador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTabela;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExecutarComando;
        private System.Windows.Forms.TextBox txtComando1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtComando2;
        private System.Windows.Forms.ComboBox cbChavePrimaria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExecTodaTabelas;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chErro;
    }
}