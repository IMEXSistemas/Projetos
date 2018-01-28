namespace BmsSoftware.Modulos.Vendas
{
    partial class FrmAddProdutoPedMateriaPrimacs
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
            this.btnCadProd = new System.Windows.Forms.Button();
            this.cbProdutoMTQ = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.lblObsField = new System.Windows.Forms.Label();
            this.btnCadMatPrima = new System.Windows.Forms.Button();
            this.cbMateriaPrima = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelMTQ = new System.Windows.Forms.Button();
            this.btnAdicionaMTQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCadProd
            // 
            this.btnCadProd.FlatAppearance.BorderSize = 0;
            this.btnCadProd.Location = new System.Drawing.Point(420, 24);
            this.btnCadProd.Name = "btnCadProd";
            this.btnCadProd.Size = new System.Drawing.Size(26, 21);
            this.btnCadProd.TabIndex = 503;
            this.btnCadProd.UseVisualStyleBackColor = true;
            this.btnCadProd.Click += new System.EventHandler(this.btnCadProdMTQ_Click);
            // 
            // cbProdutoMTQ
            // 
            this.cbProdutoMTQ.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProdutoMTQ.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProdutoMTQ.FormattingEnabled = true;
            this.cbProdutoMTQ.Location = new System.Drawing.Point(12, 24);
            this.cbProdutoMTQ.Name = "cbProdutoMTQ";
            this.cbProdutoMTQ.Size = new System.Drawing.Size(402, 21);
            this.cbProdutoMTQ.TabIndex = 501;
            this.cbProdutoMTQ.Enter += new System.EventHandler(this.cbProdutoMTQ_Enter);
            this.cbProdutoMTQ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProdutoMTQ_KeyDown);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(12, 9);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(77, 13);
            this.label58.TabIndex = 502;
            this.label58.Text = "Produtos Final:";
            // 
            // lblObsField
            // 
            this.lblObsField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblObsField.AutoSize = true;
            this.lblObsField.ForeColor = System.Drawing.Color.Blue;
            this.lblObsField.Location = new System.Drawing.Point(9, 285);
            this.lblObsField.Name = "lblObsField";
            this.lblObsField.Size = new System.Drawing.Size(32, 13);
            this.lblObsField.TabIndex = 504;
            this.lblObsField.Text = "Obs.:";
            // 
            // btnCadMatPrima
            // 
            this.btnCadMatPrima.FlatAppearance.BorderSize = 0;
            this.btnCadMatPrima.Location = new System.Drawing.Point(420, 63);
            this.btnCadMatPrima.Name = "btnCadMatPrima";
            this.btnCadMatPrima.Size = new System.Drawing.Size(26, 21);
            this.btnCadMatPrima.TabIndex = 507;
            this.btnCadMatPrima.UseVisualStyleBackColor = true;
            // 
            // cbMateriaPrima
            // 
            this.cbMateriaPrima.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMateriaPrima.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMateriaPrima.FormattingEnabled = true;
            this.cbMateriaPrima.Location = new System.Drawing.Point(12, 63);
            this.cbMateriaPrima.Name = "cbMateriaPrima";
            this.cbMateriaPrima.Size = new System.Drawing.Size(402, 21);
            this.cbMateriaPrima.TabIndex = 505;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 506;
            this.label1.Text = "Matéria Prima:";
            // 
            // btnCancelMTQ
            // 
            this.btnCancelMTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelMTQ.Location = new System.Drawing.Point(88, 90);
            this.btnCancelMTQ.Name = "btnCancelMTQ";
            this.btnCancelMTQ.Size = new System.Drawing.Size(61, 23);
            this.btnCancelMTQ.TabIndex = 508;
            this.btnCancelMTQ.Text = "Cancelar";
            this.btnCancelMTQ.UseVisualStyleBackColor = true;
            // 
            // btnAdicionaMTQ
            // 
            this.btnAdicionaMTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionaMTQ.Location = new System.Drawing.Point(12, 90);
            this.btnAdicionaMTQ.Name = "btnAdicionaMTQ";
            this.btnAdicionaMTQ.Size = new System.Drawing.Size(70, 23);
            this.btnAdicionaMTQ.TabIndex = 509;
            this.btnAdicionaMTQ.Text = "Adicionar";
            this.btnAdicionaMTQ.UseVisualStyleBackColor = true;
            // 
            // FrmAddProdutoPedMateriaPrimacs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 307);
            this.Controls.Add(this.btnCancelMTQ);
            this.Controls.Add(this.btnAdicionaMTQ);
            this.Controls.Add(this.btnCadMatPrima);
            this.Controls.Add(this.cbMateriaPrima);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblObsField);
            this.Controls.Add(this.btnCadProd);
            this.Controls.Add(this.cbProdutoMTQ);
            this.Controls.Add(this.label58);
            this.Name = "FrmAddProdutoPedMateriaPrimacs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produto por Matéria Prima";
            this.Load += new System.EventHandler(this.FrmAddProdutoPedMateriaPrimacs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadProd;
        private System.Windows.Forms.ComboBox cbProdutoMTQ;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label lblObsField;
        private System.Windows.Forms.Button btnCadMatPrima;
        private System.Windows.Forms.ComboBox cbMateriaPrima;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelMTQ;
        private System.Windows.Forms.Button btnAdicionaMTQ;
    }
}