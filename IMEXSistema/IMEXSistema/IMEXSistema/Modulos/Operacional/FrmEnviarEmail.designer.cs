namespace winfit.Modulos.Outros
{
    partial class FrmEnviarEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnviarEmail));
            this.txtParaEmail = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAssuntoEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbMensagem = new System.Windows.Forms.ComboBox();
            this.btnCadCliente = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DGAnexo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkConSegSSL = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGAnexo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtParaEmail
            // 
            this.txtParaEmail.Location = new System.Drawing.Point(17, 25);
            this.txtParaEmail.Name = "txtParaEmail";
            this.txtParaEmail.Size = new System.Drawing.Size(264, 20);
            this.txtParaEmail.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 188;
            this.label13.Text = "Email Destino:";
            // 
            // txtAssuntoEmail
            // 
            this.txtAssuntoEmail.Location = new System.Drawing.Point(17, 102);
            this.txtAssuntoEmail.Name = "txtAssuntoEmail";
            this.txtAssuntoEmail.Size = new System.Drawing.Size(598, 20);
            this.txtAssuntoEmail.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 190;
            this.label2.Text = "Assunto:";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(16, 141);
            this.txtMensagem.MaxLength = 1000;
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensagem.Size = new System.Drawing.Size(598, 171);
            this.txtMensagem.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 192;
            this.label6.Text = "Mensagem:";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(96, 477);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 194;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbMensagem
            // 
            this.cbMensagem.FormattingEnabled = true;
            this.cbMensagem.Location = new System.Drawing.Point(17, 62);
            this.cbMensagem.Name = "cbMensagem";
            this.cbMensagem.Size = new System.Drawing.Size(565, 21);
            this.cbMensagem.TabIndex = 3;
            this.cbMensagem.SelectedValueChanged += new System.EventHandler(this.cbMensagem_SelectedValueChanged);
            // 
            // btnCadCliente
            // 
            this.btnCadCliente.FlatAppearance.BorderSize = 0;
            this.btnCadCliente.Location = new System.Drawing.Point(588, 62);
            this.btnCadCliente.Name = "btnCadCliente";
            this.btnCadCliente.Size = new System.Drawing.Size(26, 21);
            this.btnCadCliente.TabIndex = 208;
            this.btnCadCliente.Text = "...";
            this.btnCadCliente.UseVisualStyleBackColor = true;
            this.btnCadCliente.Click += new System.EventHandler(this.btnCadCliente_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 207;
            this.label4.Text = "Cadastro de Mensagem:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // DGAnexo
            // 
            this.DGAnexo.AllowUserToAddRows = false;
            this.DGAnexo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAnexo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.DGAnexo.Location = new System.Drawing.Point(16, 347);
            this.DGAnexo.Name = "DGAnexo";
            this.DGAnexo.Size = new System.Drawing.Size(597, 124);
            this.DGAnexo.TabIndex = 6;
            this.DGAnexo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGAnexo_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Excluir";
            this.Column1.Image = ((System.Drawing.Image)(resources.GetObject("Column1.Image")));
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Arquivo";
            this.Column2.Name = "Column2";
            this.Column2.Width = 500;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(16, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 215;
            this.button1.Text = "Anexos";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(15, 477);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "&Enviar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(287, 25);
            this.txtPorta.MaxLength = 4;
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(65, 20);
            this.txtPorta.TabIndex = 1;
            this.txtPorta.Text = "0";
            this.txtPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 219;
            this.label1.Text = "Porta:";
            // 
            // chkConSegSSL
            // 
            this.chkConSegSSL.AutoSize = true;
            this.chkConSegSSL.Location = new System.Drawing.Point(358, 28);
            this.chkConSegSSL.Name = "chkConSegSSL";
            this.chkConSegSSL.Size = new System.Drawing.Size(153, 17);
            this.chkConSegSSL.TabIndex = 2;
            this.chkConSegSSL.Text = "Usar conexão segura: SSL";
            this.chkConSegSSL.UseVisualStyleBackColor = true;
            // 
            // FrmEnviarEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 512);
            this.Controls.Add(this.chkConSegSSL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPorta);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DGAnexo);
            this.Controls.Add(this.cbMensagem);
            this.Controls.Add(this.btnCadCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAssuntoEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtParaEmail);
            this.Controls.Add(this.label13);
            this.Name = "FrmEnviarEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Email";
            this.Load += new System.EventHandler(this.FrmContato_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmContato_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGAnexo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtParaEmail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAssuntoEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbMensagem;
        private System.Windows.Forms.Button btnCadCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView DGAnexo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkConSegSSL;
    }
}