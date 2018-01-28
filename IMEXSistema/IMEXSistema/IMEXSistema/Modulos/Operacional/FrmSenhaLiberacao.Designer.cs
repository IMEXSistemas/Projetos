namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmSenhaLiberacao
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkSite = new System.Windows.Forms.LinkLabel();
            this.lbemail = new System.Windows.Forms.Label();
            this.lblNomeEmpresa = new System.Windows.Forms.Label();
            this.lblFaltam = new System.Windows.Forms.Label();
            this.btnAtivaDepois = new System.Windows.Forms.Button();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.txtSenhaLiberacao = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtChaveProduto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumLicenca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltelefone = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbltelefone);
            this.panel1.Controls.Add(this.linkSite);
            this.panel1.Controls.Add(this.lbemail);
            this.panel1.Controls.Add(this.lblNomeEmpresa);
            this.panel1.Controls.Add(this.lblFaltam);
            this.panel1.Controls.Add(this.btnAtivaDepois);
            this.panel1.Controls.Add(this.btnLiberar);
            this.panel1.Controls.Add(this.txtSenhaLiberacao);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtChaveProduto);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtNumLicenca);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 221);
            this.panel1.TabIndex = 0;
            // 
            // linkSite
            // 
            this.linkSite.AutoSize = true;
            this.linkSite.Location = new System.Drawing.Point(12, 184);
            this.linkSite.Name = "linkSite";
            this.linkSite.Size = new System.Drawing.Size(41, 13);
            this.linkSite.TabIndex = 27;
            this.linkSite.TabStop = true;
            this.linkSite.Text = "linkSite";
            this.linkSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSite_LinkClicked);
            // 
            // lbemail
            // 
            this.lbemail.AutoSize = true;
            this.lbemail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbemail.Location = new System.Drawing.Point(12, 197);
            this.lbemail.Name = "lbemail";
            this.lbemail.Size = new System.Drawing.Size(39, 13);
            this.lbemail.TabIndex = 26;
            this.lbemail.Text = "lbemail";
            // 
            // lblNomeEmpresa
            // 
            this.lblNomeEmpresa.AutoSize = true;
            this.lblNomeEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeEmpresa.Location = new System.Drawing.Point(12, 171);
            this.lblNomeEmpresa.Name = "lblNomeEmpresa";
            this.lblNomeEmpresa.Size = new System.Drawing.Size(86, 13);
            this.lblNomeEmpresa.TabIndex = 24;
            this.lblNomeEmpresa.Text = "lblNomeEmpresa";
            // 
            // lblFaltam
            // 
            this.lblFaltam.AutoSize = true;
            this.lblFaltam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltam.Location = new System.Drawing.Point(122, 129);
            this.lblFaltam.Name = "lblFaltam";
            this.lblFaltam.Size = new System.Drawing.Size(121, 13);
            this.lblFaltam.TabIndex = 21;
            this.lblFaltam.Text = "Faltam 0 dias, ligue:";
            this.lblFaltam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAtivaDepois
            // 
            this.btnAtivaDepois.Location = new System.Drawing.Point(348, 83);
            this.btnAtivaDepois.Name = "btnAtivaDepois";
            this.btnAtivaDepois.Size = new System.Drawing.Size(92, 23);
            this.btnAtivaDepois.TabIndex = 4;
            this.btnAtivaDepois.Text = "Ativar Depois";
            this.btnAtivaDepois.UseVisualStyleBackColor = true;
            this.btnAtivaDepois.Click += new System.EventHandler(this.btnAtivaDepois_Click);
            // 
            // btnLiberar
            // 
            this.btnLiberar.Location = new System.Drawing.Point(348, 54);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(92, 23);
            this.btnLiberar.TabIndex = 3;
            this.btnLiberar.Text = "Liberar";
            this.btnLiberar.UseVisualStyleBackColor = true;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // txtSenhaLiberacao
            // 
            this.txtSenhaLiberacao.Location = new System.Drawing.Point(125, 106);
            this.txtSenhaLiberacao.Name = "txtSenhaLiberacao";
            this.txtSenhaLiberacao.Size = new System.Drawing.Size(207, 20);
            this.txtSenhaLiberacao.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Senha de Liberação:";
            // 
            // txtChaveProduto
            // 
            this.txtChaveProduto.Location = new System.Drawing.Point(125, 80);
            this.txtChaveProduto.Name = "txtChaveProduto";
            this.txtChaveProduto.ReadOnly = true;
            this.txtChaveProduto.Size = new System.Drawing.Size(207, 20);
            this.txtChaveProduto.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Chave do Produto:";
            // 
            // txtNumLicenca
            // 
            this.txtNumLicenca.Location = new System.Drawing.Point(125, 54);
            this.txtNumLicenca.Name = "txtNumLicenca";
            this.txtNumLicenca.ReadOnly = true;
            this.txtNumLicenca.Size = new System.Drawing.Size(207, 20);
            this.txtNumLicenca.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Número da Licença:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "favor ligar para número abaixo e pedir a senha de liberação.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Para que você possa destravar o software para o uso completo,";
            // 
            // lbltelefone
            // 
            this.lbltelefone.AutoSize = true;
            this.lbltelefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltelefone.Location = new System.Drawing.Point(268, 129);
            this.lbltelefone.Name = "lbltelefone";
            this.lbltelefone.Size = new System.Drawing.Size(66, 13);
            this.lbltelefone.TabIndex = 28;
            this.lbltelefone.Text = "lbltelefone";
            this.lbltelefone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSenhaLiberacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 221);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmSenhaLiberacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmSenhaLiberacao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSenhaLiberacao_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAtivaDepois;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.TextBox txtSenhaLiberacao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtChaveProduto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumLicenca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFaltam;
        private System.Windows.Forms.Label lblNomeEmpresa;
        private System.Windows.Forms.Label lbemail;
        private System.Windows.Forms.LinkLabel linkSite;
        private System.Windows.Forms.Label lbltelefone;
    }
}