namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmAlterarStatusNFe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlterarStatusNFe));
            this.dgvNFe = new System.Windows.Forms.DataGridView();
            this.NFISCALE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGCANCELADA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGENVIADA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAGINUTILIZADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTALNOTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTEMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMECLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalPesquisa = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNFe)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNFe
            // 
            this.dgvNFe.AllowUserToAddRows = false;
            this.dgvNFe.AllowUserToDeleteRows = false;
            this.dgvNFe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNFe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNFe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNFe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NFISCALE,
            this.FLAGCANCELADA,
            this.FLAGENVIADA,
            this.FLAGINUTILIZADO,
            this.TOTALNOTA,
            this.DTEMISSAO,
            this.NOMECLIENTE});
            this.dgvNFe.Location = new System.Drawing.Point(12, 21);
            this.dgvNFe.Name = "dgvNFe";
            this.dgvNFe.Size = new System.Drawing.Size(943, 314);
            this.dgvNFe.TabIndex = 170;
            this.dgvNFe.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNFe_CellEndEdit);
            // 
            // NFISCALE
            // 
            this.NFISCALE.DataPropertyName = "NFISCALE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NFISCALE.DefaultCellStyle = dataGridViewCellStyle1;
            this.NFISCALE.HeaderText = "Nota Fiscal";
            this.NFISCALE.Name = "NFISCALE";
            this.NFISCALE.ReadOnly = true;
            // 
            // FLAGCANCELADA
            // 
            this.FLAGCANCELADA.DataPropertyName = "FLAGCANCELADA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAGCANCELADA.DefaultCellStyle = dataGridViewCellStyle2;
            this.FLAGCANCELADA.HeaderText = "Cancelado";
            this.FLAGCANCELADA.Name = "FLAGCANCELADA";
            // 
            // FLAGENVIADA
            // 
            this.FLAGENVIADA.DataPropertyName = "FLAGENVIADA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAGENVIADA.DefaultCellStyle = dataGridViewCellStyle3;
            this.FLAGENVIADA.HeaderText = "Enviado";
            this.FLAGENVIADA.Name = "FLAGENVIADA";
            // 
            // FLAGINUTILIZADO
            // 
            this.FLAGINUTILIZADO.DataPropertyName = "FLAGINUTILIZADO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAGINUTILIZADO.DefaultCellStyle = dataGridViewCellStyle4;
            this.FLAGINUTILIZADO.HeaderText = "Inutilizado";
            this.FLAGINUTILIZADO.Name = "FLAGINUTILIZADO";
            // 
            // TOTALNOTA
            // 
            this.TOTALNOTA.DataPropertyName = "TOTALNOTA";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.TOTALNOTA.DefaultCellStyle = dataGridViewCellStyle5;
            this.TOTALNOTA.HeaderText = "Valor";
            this.TOTALNOTA.Name = "TOTALNOTA";
            // 
            // DTEMISSAO
            // 
            this.DTEMISSAO.DataPropertyName = "DTEMISSAO";
            this.DTEMISSAO.HeaderText = "Emissão";
            this.DTEMISSAO.Name = "DTEMISSAO";
            this.DTEMISSAO.ReadOnly = true;
            // 
            // NOMECLIENTE
            // 
            this.NOMECLIENTE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NOMECLIENTE.DataPropertyName = "NOMECLIENTE";
            this.NOMECLIENTE.HeaderText = "Cliente";
            this.NOMECLIENTE.Name = "NOMECLIENTE";
            this.NOMECLIENTE.ReadOnly = true;
            this.NOMECLIENTE.Width = 350;
            // 
            // lblTotalPesquisa
            // 
            this.lblTotalPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPesquisa.AutoSize = true;
            this.lblTotalPesquisa.Location = new System.Drawing.Point(112, 352);
            this.lblTotalPesquisa.Name = "lblTotalPesquisa";
            this.lblTotalPesquisa.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPesquisa.TabIndex = 172;
            this.lblTotalPesquisa.Text = "0";
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(12, 352);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(94, 13);
            this.label51.TabIndex = 171;
            this.label51.Text = "Total da pesquisa:";
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(880, 342);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 173;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(799, 342);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 174;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmAlterarStatusNFe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 374);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.lblTotalPesquisa);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.dgvNFe);
            this.Name = "FrmAlterarStatusNFe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Status NFe";
            this.Load += new System.EventHandler(this.FrmAlterarStatusNFe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNFe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNFe;
        private System.Windows.Forms.Label lblTotalPesquisa;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFISCALE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGCANCELADA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGENVIADA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLAGINUTILIZADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTALNOTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTEMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMECLIENTE;
        private System.Windows.Forms.Button btnImprimir;
    }
}