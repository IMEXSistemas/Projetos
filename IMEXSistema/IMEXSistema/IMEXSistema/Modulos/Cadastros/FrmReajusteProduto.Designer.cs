namespace BmsSoftware.Modulos.Cadastros
{
    partial class FrmReajusteProduto
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkPrecoVenda3 = new System.Windows.Forms.CheckBox();
            this.chkCustoInicial = new System.Windows.Forms.CheckBox();
            this.chkCustoFinal = new System.Windows.Forms.CheckBox();
            this.chkPrecoVenda1 = new System.Windows.Forms.CheckBox();
            this.chkPrecoVenda2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPorcReajuste = new System.Windows.Forms.TextBox();
            this.txtPorcDesconto = new System.Windows.Forms.TextBox();
            this.btnApagarEstoque = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnApagarEstoque);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 266);
            this.panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 231);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(248, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.chkPrecoVenda3);
            this.groupBox1.Controls.Add(this.chkCustoInicial);
            this.groupBox1.Controls.Add(this.chkCustoFinal);
            this.groupBox1.Controls.Add(this.chkPrecoVenda1);
            this.groupBox1.Controls.Add(this.chkPrecoVenda2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPorcReajuste);
            this.groupBox1.Controls.Add(this.txtPorcDesconto);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 182);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reajuste/Desconto";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Desconto/Reajuste";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkPrecoVenda3
            // 
            this.chkPrecoVenda3.AutoSize = true;
            this.chkPrecoVenda3.Location = new System.Drawing.Point(17, 114);
            this.chkPrecoVenda3.Name = "chkPrecoVenda3";
            this.chkPrecoVenda3.Size = new System.Drawing.Size(94, 17);
            this.chkPrecoVenda3.TabIndex = 8;
            this.chkPrecoVenda3.Text = "Preço Venda3";
            this.chkPrecoVenda3.UseVisualStyleBackColor = true;
            // 
            // chkCustoInicial
            // 
            this.chkCustoInicial.AutoSize = true;
            this.chkCustoInicial.Location = new System.Drawing.Point(120, 68);
            this.chkCustoInicial.Name = "chkCustoInicial";
            this.chkCustoInicial.Size = new System.Drawing.Size(110, 17);
            this.chkCustoInicial.TabIndex = 6;
            this.chkCustoInicial.Text = "Valor Custo Inicial";
            this.chkCustoInicial.UseVisualStyleBackColor = true;
            // 
            // chkCustoFinal
            // 
            this.chkCustoFinal.AutoSize = true;
            this.chkCustoFinal.Location = new System.Drawing.Point(120, 91);
            this.chkCustoFinal.Name = "chkCustoFinal";
            this.chkCustoFinal.Size = new System.Drawing.Size(105, 17);
            this.chkCustoFinal.TabIndex = 7;
            this.chkCustoFinal.Text = "Valor Custo Final";
            this.chkCustoFinal.UseVisualStyleBackColor = true;
            // 
            // chkPrecoVenda1
            // 
            this.chkPrecoVenda1.AutoSize = true;
            this.chkPrecoVenda1.Location = new System.Drawing.Point(17, 68);
            this.chkPrecoVenda1.Name = "chkPrecoVenda1";
            this.chkPrecoVenda1.Size = new System.Drawing.Size(94, 17);
            this.chkPrecoVenda1.TabIndex = 4;
            this.chkPrecoVenda1.Text = "Preço Venda1";
            this.chkPrecoVenda1.UseVisualStyleBackColor = true;
            // 
            // chkPrecoVenda2
            // 
            this.chkPrecoVenda2.AutoSize = true;
            this.chkPrecoVenda2.Location = new System.Drawing.Point(17, 91);
            this.chkPrecoVenda2.Name = "chkPrecoVenda2";
            this.chkPrecoVenda2.Size = new System.Drawing.Size(94, 17);
            this.chkPrecoVenda2.TabIndex = 5;
            this.chkPrecoVenda2.Text = "Preço Venda2";
            this.chkPrecoVenda2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reajuste %:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Desconto %:";
            // 
            // txtPorcReajuste
            // 
            this.txtPorcReajuste.Location = new System.Drawing.Point(14, 32);
            this.txtPorcReajuste.MaxLength = 6;
            this.txtPorcReajuste.Name = "txtPorcReajuste";
            this.txtPorcReajuste.Size = new System.Drawing.Size(100, 20);
            this.txtPorcReajuste.TabIndex = 1;
            this.txtPorcReajuste.Text = "0,00";
            this.txtPorcReajuste.Validating += new System.ComponentModel.CancelEventHandler(this.txtPorcReajuste_Validating);
            // 
            // txtPorcDesconto
            // 
            this.txtPorcDesconto.Location = new System.Drawing.Point(120, 32);
            this.txtPorcDesconto.MaxLength = 6;
            this.txtPorcDesconto.Name = "txtPorcDesconto";
            this.txtPorcDesconto.Size = new System.Drawing.Size(100, 20);
            this.txtPorcDesconto.TabIndex = 3;
            this.txtPorcDesconto.Tag = "";
            this.txtPorcDesconto.Text = "0,00";
            this.txtPorcDesconto.Validating += new System.ComponentModel.CancelEventHandler(this.txtPorcDesconto_Validating);
            // 
            // btnApagarEstoque
            // 
            this.btnApagarEstoque.Location = new System.Drawing.Point(29, 200);
            this.btnApagarEstoque.Name = "btnApagarEstoque";
            this.btnApagarEstoque.Size = new System.Drawing.Size(226, 23);
            this.btnApagarEstoque.TabIndex = 4;
            this.btnApagarEstoque.Text = "Apagar os itens de estoque na pesquisa";
            this.btnApagarEstoque.UseVisualStyleBackColor = true;
            this.btnApagarEstoque.Click += new System.EventHandler(this.btnApagarEstoque_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmReajusteProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 266);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmReajusteProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reajuste/Desconto de Preços";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReajusteProduto_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReajusteProduto_KeyDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPorcDesconto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPorcReajuste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnApagarEstoque;
        private System.Windows.Forms.CheckBox chkPrecoVenda2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPrecoVenda3;
        private System.Windows.Forms.CheckBox chkCustoInicial;
        private System.Windows.Forms.CheckBox chkCustoFinal;
        private System.Windows.Forms.CheckBox chkPrecoVenda1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}