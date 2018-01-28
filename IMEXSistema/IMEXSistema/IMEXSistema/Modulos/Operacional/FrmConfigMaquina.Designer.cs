namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmConfigMaquina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigMaquina));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblobsField = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lklRestaurar = new System.Windows.Forms.LinkLabel();
            this.lkTestConexao = new System.Windows.Forms.LinkLabel();
            this.btnInstall = new System.Windows.Forms.Button();
            this.txtLocalInstall = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btImagens = new System.Windows.Forms.Button();
            this.txtLocalImagens = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bntCadBanco = new System.Windows.Forms.Button();
            this.txtLocalBancoDados = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtPortaRede = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPortaMatricial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtMsg4Ticket = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMsg3Ticket = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMsg2Ticket = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMsg1Ticket = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbImpressoraTicket = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblobsField);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 370);
            this.panel1.TabIndex = 0;
            // 
            // lblobsField
            // 
            this.lblobsField.AutoSize = true;
            this.lblobsField.BackColor = System.Drawing.SystemColors.Control;
            this.lblobsField.ForeColor = System.Drawing.Color.Blue;
            this.lblobsField.Location = new System.Drawing.Point(13, 306);
            this.lblobsField.Name = "lblobsField";
            this.lblobsField.Size = new System.Drawing.Size(307, 13);
            this.lblobsField.TabIndex = 102;
            this.lblobsField.Text = "Após salvar as configurações é necessario reiniciar o programa.";
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(97, 335);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 3;
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(16, 335);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Salvar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(404, 286);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lklRestaurar);
            this.tabPage1.Controls.Add(this.lkTestConexao);
            this.tabPage1.Controls.Add(this.btnInstall);
            this.tabPage1.Controls.Add(this.txtLocalInstall);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btImagens);
            this.tabPage1.Controls.Add(this.txtLocalImagens);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.bntCadBanco);
            this.tabPage1.Controls.Add(this.txtLocalBancoDados);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(396, 260);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Arquivos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lklRestaurar
            // 
            this.lklRestaurar.AutoSize = true;
            this.lklRestaurar.Location = new System.Drawing.Point(240, 136);
            this.lklRestaurar.Name = "lklRestaurar";
            this.lklRestaurar.Size = new System.Drawing.Size(156, 13);
            this.lklRestaurar.TabIndex = 206;
            this.lklRestaurar.TabStop = true;
            this.lklRestaurar.Text = "Restaurar Configuração Padrão";
            this.lklRestaurar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklRestaurar_LinkClicked);
            this.lklRestaurar.LocationChanged += new System.EventHandler(this.lklRestaurar_LocationChanged);
            // 
            // lkTestConexao
            // 
            this.lkTestConexao.AutoSize = true;
            this.lkTestConexao.Location = new System.Drawing.Point(272, 12);
            this.lkTestConexao.Name = "lkTestConexao";
            this.lkTestConexao.Size = new System.Drawing.Size(82, 13);
            this.lkTestConexao.TabIndex = 205;
            this.lkTestConexao.TabStop = true;
            this.lkTestConexao.Text = "Testar Conexão";
            this.lkTestConexao.Click += new System.EventHandler(this.lkTestConexao_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(358, 111);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(31, 23);
            this.btnInstall.TabIndex = 204;
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // txtLocalInstall
            // 
            this.txtLocalInstall.Location = new System.Drawing.Point(17, 113);
            this.txtLocalInstall.MaxLength = 500;
            this.txtLocalInstall.Name = "txtLocalInstall";
            this.txtLocalInstall.Size = new System.Drawing.Size(335, 20);
            this.txtLocalInstall.TabIndex = 203;
            this.txtLocalInstall.TextChanged += new System.EventHandler(this.txtLocalInstall_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 202;
            this.label6.Text = "Local de Instalação:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // btImagens
            // 
            this.btImagens.Location = new System.Drawing.Point(358, 69);
            this.btImagens.Name = "btImagens";
            this.btImagens.Size = new System.Drawing.Size(31, 23);
            this.btImagens.TabIndex = 201;
            this.btImagens.UseVisualStyleBackColor = true;
            this.btImagens.Click += new System.EventHandler(this.btImagens_Click);
            // 
            // txtLocalImagens
            // 
            this.txtLocalImagens.Location = new System.Drawing.Point(17, 71);
            this.txtLocalImagens.MaxLength = 500;
            this.txtLocalImagens.Name = "txtLocalImagens";
            this.txtLocalImagens.Size = new System.Drawing.Size(335, 20);
            this.txtLocalImagens.TabIndex = 200;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 199;
            this.label5.Text = "Local das Imagens:";
            // 
            // bntCadBanco
            // 
            this.bntCadBanco.Location = new System.Drawing.Point(358, 25);
            this.bntCadBanco.Name = "bntCadBanco";
            this.bntCadBanco.Size = new System.Drawing.Size(31, 23);
            this.bntCadBanco.TabIndex = 198;
            this.bntCadBanco.UseVisualStyleBackColor = true;
            this.bntCadBanco.Click += new System.EventHandler(this.bntCadBanco_Click);
            // 
            // txtLocalBancoDados
            // 
            this.txtLocalBancoDados.Location = new System.Drawing.Point(17, 28);
            this.txtLocalBancoDados.MaxLength = 500;
            this.txtLocalBancoDados.Name = "txtLocalBancoDados";
            this.txtLocalBancoDados.Size = new System.Drawing.Size(335, 20);
            this.txtLocalBancoDados.TabIndex = 197;
            this.txtLocalBancoDados.Enter += new System.EventHandler(this.txtLocalBancoDados_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 196;
            this.label1.Text = "Local do Banco de Dados:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtPortaRede);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtPortaMatricial);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(396, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Impressora";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtPortaRede
            // 
            this.txtPortaRede.Location = new System.Drawing.Point(6, 66);
            this.txtPortaRede.MaxLength = 500;
            this.txtPortaRede.Name = "txtPortaRede";
            this.txtPortaRede.Size = new System.Drawing.Size(369, 20);
            this.txtPortaRede.TabIndex = 201;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 200;
            this.label4.Text = "Caminho da Impressora na Rede:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 197;
            // 
            // txtPortaMatricial
            // 
            this.txtPortaMatricial.Location = new System.Drawing.Point(6, 27);
            this.txtPortaMatricial.MaxLength = 500;
            this.txtPortaMatricial.Name = "txtPortaMatricial";
            this.txtPortaMatricial.Size = new System.Drawing.Size(369, 20);
            this.txtPortaMatricial.TabIndex = 195;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 194;
            this.label2.Text = "Porta da Impressora:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtMsg4Ticket);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.txtMsg3Ticket);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.txtMsg2Ticket);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.txtMsg1Ticket);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.cbImpressoraTicket);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(396, 260);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Ticket";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtMsg4Ticket
            // 
            this.txtMsg4Ticket.Location = new System.Drawing.Point(9, 188);
            this.txtMsg4Ticket.MaxLength = 50;
            this.txtMsg4Ticket.Name = "txtMsg4Ticket";
            this.txtMsg4Ticket.Size = new System.Drawing.Size(361, 20);
            this.txtMsg4Ticket.TabIndex = 219;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 218;
            this.label14.Text = "Mensagem Linha 4:";
            // 
            // txtMsg3Ticket
            // 
            this.txtMsg3Ticket.Location = new System.Drawing.Point(9, 149);
            this.txtMsg3Ticket.MaxLength = 50;
            this.txtMsg3Ticket.Name = "txtMsg3Ticket";
            this.txtMsg3Ticket.Size = new System.Drawing.Size(361, 20);
            this.txtMsg3Ticket.TabIndex = 217;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 216;
            this.label13.Text = "Mensagem Linha 3:";
            // 
            // txtMsg2Ticket
            // 
            this.txtMsg2Ticket.Location = new System.Drawing.Point(9, 110);
            this.txtMsg2Ticket.MaxLength = 50;
            this.txtMsg2Ticket.Name = "txtMsg2Ticket";
            this.txtMsg2Ticket.Size = new System.Drawing.Size(361, 20);
            this.txtMsg2Ticket.TabIndex = 215;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 214;
            this.label12.Text = "Mensagem Linha 2:";
            // 
            // txtMsg1Ticket
            // 
            this.txtMsg1Ticket.Location = new System.Drawing.Point(9, 71);
            this.txtMsg1Ticket.MaxLength = 50;
            this.txtMsg1Ticket.Name = "txtMsg1Ticket";
            this.txtMsg1Ticket.Size = new System.Drawing.Size(361, 20);
            this.txtMsg1Ticket.TabIndex = 213;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 212;
            this.label11.Text = "Mensagem Linha 1:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 13);
            this.label10.TabIndex = 211;
            this.label10.Text = "Impressoras Instalada:";
            // 
            // cbImpressoraTicket
            // 
            this.cbImpressoraTicket.FormattingEnabled = true;
            this.cbImpressoraTicket.Location = new System.Drawing.Point(9, 28);
            this.cbImpressoraTicket.Name = "cbImpressoraTicket";
            this.cbImpressoraTicket.Size = new System.Drawing.Size(361, 21);
            this.cbImpressoraTicket.TabIndex = 210;
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
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog1";
            this.openFileDialog3.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog3_FileOk);
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.FileName = "openFileDialog1";
            this.openFileDialog4.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog4_FileOk);
            // 
            // FrmConfigMaquina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 370);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmConfigMaquina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração por Computador";
            this.Load += new System.EventHandler(this.FrmConfigMaquina_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConfigMaquina_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtPortaRede;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPortaMatricial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtLocalBancoDados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button bntCadBanco;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblobsField;
        private System.Windows.Forms.Button btImagens;
        private System.Windows.Forms.TextBox txtLocalImagens;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.TextBox txtLocalInstall;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.LinkLabel lkTestConexao;
        private System.Windows.Forms.LinkLabel lklRestaurar;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtMsg4Ticket;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMsg3Ticket;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMsg2Ticket;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMsg1Ticket;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbImpressoraTicket;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
    }
}