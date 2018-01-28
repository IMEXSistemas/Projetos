namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmCartaCorrecao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCartaCorrecao));
            this.txtCCeCorrecao = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtNFeCCe = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtSequencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbManaus = new System.Windows.Forms.RadioButton();
            this.rbFernandoNoronha = new System.Windows.Forms.RadioButton();
            this.rbBrasilia = new System.Windows.Forms.RadioButton();
            this.chkHorariVerao = new System.Windows.Forms.CheckBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCCeCorrecao
            // 
            this.txtCCeCorrecao.Location = new System.Drawing.Point(13, 132);
            this.txtCCeCorrecao.Multiline = true;
            this.txtCCeCorrecao.Name = "txtCCeCorrecao";
            this.txtCCeCorrecao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCCeCorrecao.Size = new System.Drawing.Size(515, 261);
            this.txtCCeCorrecao.TabIndex = 19;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(12, 112);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(61, 14);
            this.label39.TabIndex = 18;
            this.label39.Text = "Correção:";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNFeCCe
            // 
            this.txtNFeCCe.Location = new System.Drawing.Point(12, 26);
            this.txtNFeCCe.Name = "txtNFeCCe";
            this.txtNFeCCe.ReadOnly = true;
            this.txtNFeCCe.Size = new System.Drawing.Size(516, 20);
            this.txtNFeCCe.TabIndex = 20;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(10, 9);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(34, 14);
            this.label38.TabIndex = 17;
            this.label38.Text = "NF-e:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.Image")));
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.Location = new System.Drawing.Point(13, 399);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 21;
            this.btnEnviar.Text = "&Enviar";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(175, 399);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 22;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtSequencia
            // 
            this.txtSequencia.Location = new System.Drawing.Point(12, 66);
            this.txtSequencia.MaxLength = 10;
            this.txtSequencia.Name = "txtSequencia";
            this.txtSequencia.Size = new System.Drawing.Size(64, 20);
            this.txtSequencia.TabIndex = 24;
            this.txtSequencia.Text = "1";
            this.txtSequencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 23;
            this.label1.Text = "Sequência:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbManaus);
            this.groupBox1.Controls.Add(this.rbFernandoNoronha);
            this.groupBox1.Controls.Add(this.rbBrasilia);
            this.groupBox1.Location = new System.Drawing.Point(219, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 50);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horário";
            // 
            // rbManaus
            // 
            this.rbManaus.AutoSize = true;
            this.rbManaus.Location = new System.Drawing.Point(227, 20);
            this.rbManaus.Name = "rbManaus";
            this.rbManaus.Size = new System.Drawing.Size(63, 17);
            this.rbManaus.TabIndex = 2;
            this.rbManaus.Text = "Manaus";
            this.rbManaus.UseVisualStyleBackColor = true;
            // 
            // rbFernandoNoronha
            // 
            this.rbFernandoNoronha.AutoSize = true;
            this.rbFernandoNoronha.Location = new System.Drawing.Point(92, 20);
            this.rbFernandoNoronha.Name = "rbFernandoNoronha";
            this.rbFernandoNoronha.Size = new System.Drawing.Size(129, 17);
            this.rbFernandoNoronha.TabIndex = 1;
            this.rbFernandoNoronha.Text = "Fernando de Noronha";
            this.rbFernandoNoronha.UseVisualStyleBackColor = true;
            // 
            // rbBrasilia
            // 
            this.rbBrasilia.AutoSize = true;
            this.rbBrasilia.Checked = true;
            this.rbBrasilia.Location = new System.Drawing.Point(19, 20);
            this.rbBrasilia.Name = "rbBrasilia";
            this.rbBrasilia.Size = new System.Drawing.Size(60, 17);
            this.rbBrasilia.TabIndex = 0;
            this.rbBrasilia.TabStop = true;
            this.rbBrasilia.Text = "Brasília";
            this.rbBrasilia.UseVisualStyleBackColor = true;
            // 
            // chkHorariVerao
            // 
            this.chkHorariVerao.AutoSize = true;
            this.chkHorariVerao.Location = new System.Drawing.Point(422, 109);
            this.chkHorariVerao.Name = "chkHorariVerao";
            this.chkHorariVerao.Size = new System.Drawing.Size(106, 17);
            this.chkHorariVerao.TabIndex = 26;
            this.chkHorariVerao.Text = "Horário de Verão";
            this.chkHorariVerao.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(94, 399);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 27;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrmCartaCorrecao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 434);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.chkHorariVerao);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSequencia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtCCeCorrecao);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.txtNFeCCe);
            this.Controls.Add(this.label38);
            this.Name = "FrmCartaCorrecao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carta de Correção";
            this.Load += new System.EventHandler(this.FrmCartaCorrecao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCCeCorrecao;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtNFeCCe;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtSequencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBrasilia;
        private System.Windows.Forms.RadioButton rbManaus;
        private System.Windows.Forms.RadioButton rbFernandoNoronha;
        private System.Windows.Forms.CheckBox chkHorariVerao;
        private System.Windows.Forms.Button btnImprimir;
    }
}