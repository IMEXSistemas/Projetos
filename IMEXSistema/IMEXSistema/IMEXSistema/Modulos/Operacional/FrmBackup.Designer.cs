namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBackup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.rbTodoSistema = new System.Windows.Forms.RadioButton();
            this.rbBancoDados = new System.Windows.Forms.RadioButton();
            this.btnSair = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMaquina = new System.Windows.Forms.Button();
            this.txtCaminhoBackup = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.rbTodoSistema);
            this.panel1.Controls.Add(this.rbBancoDados);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnMaquina);
            this.panel1.Controls.Add(this.txtCaminhoBackup);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 143);
            this.panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 80);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(345, 24);
            this.progressBar1.TabIndex = 105;
            // 
            // rbTodoSistema
            // 
            this.rbTodoSistema.AutoSize = true;
            this.rbTodoSistema.Location = new System.Drawing.Point(189, 57);
            this.rbTodoSistema.Name = "rbTodoSistema";
            this.rbTodoSistema.Size = new System.Drawing.Size(109, 17);
            this.rbTodoSistema.TabIndex = 104;
            this.rbTodoSistema.Text = "Sistema Completo";
            this.rbTodoSistema.UseVisualStyleBackColor = true;
            this.rbTodoSistema.Click += new System.EventHandler(this.rbTodoSistema_Click);
            // 
            // rbBancoDados
            // 
            this.rbBancoDados.AutoSize = true;
            this.rbBancoDados.Checked = true;
            this.rbBancoDados.Location = new System.Drawing.Point(16, 57);
            this.rbBancoDados.Name = "rbBancoDados";
            this.rbBancoDados.Size = new System.Drawing.Size(159, 17);
            this.rbBancoDados.TabIndex = 103;
            this.rbBancoDados.TabStop = true;
            this.rbBancoDados.Text = "Somente o Banco de Dados";
            this.rbBancoDados.UseVisualStyleBackColor = true;
            this.rbBancoDados.Click += new System.EventHandler(this.rbBancoDados_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(97, 110);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 102;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(16, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 101;
            this.button1.Text = "&Backup";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMaquina
            // 
            this.btnMaquina.BackColor = System.Drawing.SystemColors.Control;
            this.btnMaquina.Location = new System.Drawing.Point(330, 24);
            this.btnMaquina.Name = "btnMaquina";
            this.btnMaquina.Size = new System.Drawing.Size(31, 23);
            this.btnMaquina.TabIndex = 100;
            this.btnMaquina.UseVisualStyleBackColor = false;
            this.btnMaquina.Click += new System.EventHandler(this.btnMaquina_Click);
            // 
            // txtCaminhoBackup
            // 
            this.txtCaminhoBackup.Location = new System.Drawing.Point(16, 26);
            this.txtCaminhoBackup.Name = "txtCaminhoBackup";
            this.txtCaminhoBackup.ReadOnly = true;
            this.txtCaminhoBackup.Size = new System.Drawing.Size(308, 20);
            this.txtCaminhoBackup.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Destino da Cópia:";
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
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 143);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup - Cópia de Segurança";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBackup_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCaminhoBackup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMaquina;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbTodoSistema;
        private System.Windows.Forms.RadioButton rbBancoDados;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}