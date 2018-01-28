namespace BmsSoftware.Modulos.Atualizacao
{
    partial class FrmUpdateBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateBD));
            this.btnAtualizaBD = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkAtulizaEXE = new System.Windows.Forms.CheckBox();
            this.chkAtualizaBD = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.chkAtualizaNFe = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAtualizaBD
            // 
            this.btnAtualizaBD.Image = ((System.Drawing.Image)(resources.GetObject("btnAtualizaBD.Image")));
            this.btnAtualizaBD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizaBD.Location = new System.Drawing.Point(13, 12);
            this.btnAtualizaBD.Name = "btnAtualizaBD";
            this.btnAtualizaBD.Size = new System.Drawing.Size(102, 23);
            this.btnAtualizaBD.TabIndex = 205;
            this.btnAtualizaBD.Text = "&Atualizar";
            this.btnAtualizaBD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAtualizaBD.UseVisualStyleBackColor = true;
            this.btnAtualizaBD.Click += new System.EventHandler(this.btnAtualizaBD_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(120, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 206;
            this.btnExit.Text = "&Sair";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(13, 97);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(381, 13);
            this.lblMsg.TabIndex = 207;
            this.lblMsg.Text = "Antes de atualizar o Banco de Dados faça uma cópia de segurança do mesmo.";
            // 
            // chkAtulizaEXE
            // 
            this.chkAtulizaEXE.AutoSize = true;
            this.chkAtulizaEXE.Checked = true;
            this.chkAtulizaEXE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtulizaEXE.Location = new System.Drawing.Point(107, 41);
            this.chkAtulizaEXE.Name = "chkAtulizaEXE";
            this.chkAtulizaEXE.Size = new System.Drawing.Size(90, 17);
            this.chkAtulizaEXE.TabIndex = 208;
            this.chkAtulizaEXE.Text = "Atualizar EXE";
            this.chkAtulizaEXE.UseVisualStyleBackColor = true;
            this.chkAtulizaEXE.Visible = false;
            // 
            // chkAtualizaBD
            // 
            this.chkAtualizaBD.AutoSize = true;
            this.chkAtualizaBD.Checked = true;
            this.chkAtualizaBD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtualizaBD.Location = new System.Drawing.Point(203, 41);
            this.chkAtualizaBD.Name = "chkAtualizaBD";
            this.chkAtualizaBD.Size = new System.Drawing.Size(149, 17);
            this.chkAtualizaBD.TabIndex = 209;
            this.chkAtualizaBD.Text = "Atualizar Banco de Dados";
            this.chkAtualizaBD.UseVisualStyleBackColor = true;
            this.chkAtualizaBD.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 66);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(445, 23);
            this.progressBar1.TabIndex = 210;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // chkAtualizaNFe
            // 
            this.chkAtualizaNFe.AutoSize = true;
            this.chkAtualizaNFe.Location = new System.Drawing.Point(12, 41);
            this.chkAtualizaNFe.Name = "chkAtualizaNFe";
            this.chkAtualizaNFe.Size = new System.Drawing.Size(89, 17);
            this.chkAtualizaNFe.TabIndex = 211;
            this.chkAtualizaNFe.Text = "Atualizar NFe";
            this.chkAtualizaNFe.UseVisualStyleBackColor = true;
            // 
            // FrmUpdateBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 125);
            this.Controls.Add(this.chkAtualizaNFe);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkAtualizaBD);
            this.Controls.Add(this.chkAtulizaEXE);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAtualizaBD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUpdateBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualizar IMEX Sistemas";
            this.Load += new System.EventHandler(this.FrmAtualizaBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtualizaBD;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkAtulizaEXE;
        private System.Windows.Forms.CheckBox chkAtualizaBD;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox chkAtualizaNFe;
    }
}