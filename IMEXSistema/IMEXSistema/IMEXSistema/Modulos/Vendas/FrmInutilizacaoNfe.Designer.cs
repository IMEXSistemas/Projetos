namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmInutilizacaoNfe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInutilizacaoNfe));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnInutilzar = new System.Windows.Forms.Button();
            this.txtJustInut = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtFim = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtIni = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtAno = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnInutilzar);
            this.panel1.Controls.Add(this.txtJustInut);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.txtFim);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.txtIni);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.txtAno);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 165);
            this.panel1.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(189, 128);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnInutilzar
            // 
            this.btnInutilzar.Image = ((System.Drawing.Image)(resources.GetObject("btnInutilzar.Image")));
            this.btnInutilzar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInutilzar.Location = new System.Drawing.Point(108, 128);
            this.btnInutilzar.Name = "btnInutilzar";
            this.btnInutilzar.Size = new System.Drawing.Size(75, 23);
            this.btnInutilzar.TabIndex = 4;
            this.btnInutilzar.Text = "Inutilizar";
            this.btnInutilzar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInutilzar.UseVisualStyleBackColor = true;
            this.btnInutilzar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtJustInut
            // 
            this.txtJustInut.Location = new System.Drawing.Point(98, 81);
            this.txtJustInut.Multiline = true;
            this.txtJustInut.Name = "txtJustInut";
            this.txtJustInut.Size = new System.Drawing.Size(263, 41);
            this.txtJustInut.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(17, 87);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(67, 14);
            this.label27.TabIndex = 16;
            this.label27.Text = "Justificativa:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFim
            // 
            this.txtFim.Location = new System.Drawing.Point(98, 57);
            this.txtFim.Name = "txtFim";
            this.txtFim.Size = new System.Drawing.Size(85, 20);
            this.txtFim.TabIndex = 2;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(12, 61);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 14);
            this.label26.TabIndex = 15;
            this.label26.Text = "Numero Final:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIni
            // 
            this.txtIni.Location = new System.Drawing.Point(98, 33);
            this.txtIni.Name = "txtIni";
            this.txtIni.Size = new System.Drawing.Size(85, 20);
            this.txtIni.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(8, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 14);
            this.label25.TabIndex = 14;
            this.label25.Text = "Numero Inicial:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAno
            // 
            this.txtAno.Location = new System.Drawing.Point(98, 9);
            this.txtAno.MaxLength = 2;
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(40, 20);
            this.txtAno.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(54, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 14);
            this.label16.TabIndex = 13;
            this.label16.Text = "Ano:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmInutilizacaoNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 165);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmInutilizacaoNfe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inutilização de NFe";
            this.Load += new System.EventHandler(this.FrmInutilizacaoNfe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInutilizacaoNfe_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnInutilzar;
        private System.Windows.Forms.TextBox txtJustInut;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtFim;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtIni;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtAno;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}