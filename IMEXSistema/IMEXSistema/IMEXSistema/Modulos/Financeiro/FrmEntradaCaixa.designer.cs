namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmEntradaCaixa
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
            this.btnCaixa = new System.Windows.Forms.Button();
            this.btnEntradaCaixa = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.btnCadTipo = new System.Windows.Forms.Button();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.txtVlPago = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCaixa
            // 
            this.btnCaixa.Location = new System.Drawing.Point(156, 61);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Size = new System.Drawing.Size(79, 23);
            this.btnCaixa.TabIndex = 287;
            this.btnCaixa.Text = "&Caixa";
            this.btnCaixa.UseVisualStyleBackColor = true;
            this.btnCaixa.Click += new System.EventHandler(this.btnCaixa_Click);
            // 
            // btnEntradaCaixa
            // 
            this.btnEntradaCaixa.Location = new System.Drawing.Point(30, 61);
            this.btnEntradaCaixa.Name = "btnEntradaCaixa";
            this.btnEntradaCaixa.Size = new System.Drawing.Size(120, 23);
            this.btnEntradaCaixa.TabIndex = 286;
            this.btnEntradaCaixa.Text = "&Lançar no Caixa";
            this.btnEntradaCaixa.UseVisualStyleBackColor = true;
            this.btnEntradaCaixa.Click += new System.EventHandler(this.button1_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(103, 13);
            this.label30.TabIndex = 285;
            this.label30.Text = "Tipo de Pagamento:";
            // 
            // btnCadTipo
            // 
            this.btnCadTipo.FlatAppearance.BorderSize = 0;
            this.btnCadTipo.Location = new System.Drawing.Point(195, 24);
            this.btnCadTipo.Name = "btnCadTipo";
            this.btnCadTipo.Size = new System.Drawing.Size(26, 21);
            this.btnCadTipo.TabIndex = 284;
            this.btnCadTipo.Text = "...";
            this.btnCadTipo.UseVisualStyleBackColor = true;
            this.btnCadTipo.Click += new System.EventHandler(this.btnCadTipo_Click);
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Location = new System.Drawing.Point(16, 25);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(172, 21);
            this.cbTipo.TabIndex = 283;
            // 
            // txtVlPago
            // 
            this.txtVlPago.Location = new System.Drawing.Point(275, 26);
            this.txtVlPago.MaxLength = 20;
            this.txtVlPago.Name = "txtVlPago";
            this.txtVlPago.Size = new System.Drawing.Size(64, 20);
            this.txtVlPago.TabIndex = 288;
            this.txtVlPago.Text = "0,00";
            this.txtVlPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVlPago.Validating += new System.ComponentModel.CancelEventHandler(this.txtVlPago_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(227, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 289;
            this.label10.Text = "V. Pago:";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(241, 61);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(79, 23);
            this.btnSair.TabIndex = 290;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmEntradaCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 95);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.txtVlPago);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCaixa);
            this.Controls.Add(this.btnEntradaCaixa);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.btnCadTipo);
            this.Controls.Add(this.cbTipo);
            this.Name = "FrmEntradaCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrada do Caixa";
            this.Load += new System.EventHandler(this.FrmEntradaCaixa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnEntradaCaixa;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnCadTipo;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.TextBox txtVlPago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}