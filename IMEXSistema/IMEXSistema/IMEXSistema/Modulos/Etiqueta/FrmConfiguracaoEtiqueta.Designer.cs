namespace BmsSoftware.Modulos.Etiqueta
{
    partial class FrmConfiguracaoEtiqueta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracaoEtiqueta));
            this.label2 = new System.Windows.Forms.Label();
            this.cbModeloEtiqueta = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NUpD_Left_Esquerda = new System.Windows.Forms.NumericUpDown();
            this.NUpD_Right_Direito = new System.Windows.Forms.NumericUpDown();
            this.NUpD_Top_Topo = new System.Windows.Forms.NumericUpDown();
            this.NUpD_Botto_Inferior = new System.Windows.Forms.NumericUpDown();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.NUpD_Width_Largura = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.NUpD_Height_Altura = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Left_Esquerda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Right_Direito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Top_Topo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Botto_Inferior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Width_Largura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Height_Altura)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Modelo de Etiqueta:";
            // 
            // cbModeloEtiqueta
            // 
            this.cbModeloEtiqueta.FormattingEnabled = true;
            this.cbModeloEtiqueta.Items.AddRange(new object[] {
            "POLIFIX 2060"});
            this.cbModeloEtiqueta.Location = new System.Drawing.Point(12, 25);
            this.cbModeloEtiqueta.Name = "cbModeloEtiqueta";
            this.cbModeloEtiqueta.Size = new System.Drawing.Size(396, 21);
            this.cbModeloEtiqueta.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Bottom ( Inferior )";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Right ( Direito )";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Top ( Topo )";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Left ( Esquerda )";
            // 
            // NUpD_Left_Esquerda
            // 
            this.NUpD_Left_Esquerda.Location = new System.Drawing.Point(12, 65);
            this.NUpD_Left_Esquerda.Name = "NUpD_Left_Esquerda";
            this.NUpD_Left_Esquerda.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Left_Esquerda.TabIndex = 26;
            this.NUpD_Left_Esquerda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Left_Esquerda.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // NUpD_Right_Direito
            // 
            this.NUpD_Right_Direito.Location = new System.Drawing.Point(107, 65);
            this.NUpD_Right_Direito.Name = "NUpD_Right_Direito";
            this.NUpD_Right_Direito.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Right_Direito.TabIndex = 27;
            this.NUpD_Right_Direito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Right_Direito.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // NUpD_Top_Topo
            // 
            this.NUpD_Top_Topo.Location = new System.Drawing.Point(12, 104);
            this.NUpD_Top_Topo.Name = "NUpD_Top_Topo";
            this.NUpD_Top_Topo.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Top_Topo.TabIndex = 28;
            this.NUpD_Top_Topo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Top_Topo.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // NUpD_Botto_Inferior
            // 
            this.NUpD_Botto_Inferior.Location = new System.Drawing.Point(107, 104);
            this.NUpD_Botto_Inferior.Name = "NUpD_Botto_Inferior";
            this.NUpD_Botto_Inferior.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Botto_Inferior.TabIndex = 29;
            this.NUpD_Botto_Inferior.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Botto_Inferior.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(93, 137);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 31;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(12, 137);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 30;
            this.btnAdd.Text = "Salvar";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // NUpD_Width_Largura
            // 
            this.NUpD_Width_Largura.Location = new System.Drawing.Point(202, 65);
            this.NUpD_Width_Largura.Name = "NUpD_Width_Largura";
            this.NUpD_Width_Largura.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Width_Largura.TabIndex = 33;
            this.NUpD_Width_Largura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Width_Largura.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUpD_Width_Largura.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Width ( Largura)";
            this.label1.Visible = false;
            // 
            // NUpD_Height_Altura
            // 
            this.NUpD_Height_Altura.Location = new System.Drawing.Point(202, 104);
            this.NUpD_Height_Altura.Name = "NUpD_Height_Altura";
            this.NUpD_Height_Altura.Size = new System.Drawing.Size(85, 20);
            this.NUpD_Height_Altura.TabIndex = 35;
            this.NUpD_Height_Altura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUpD_Height_Altura.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUpD_Height_Altura.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Height ( Altura )";
            this.label7.Visible = false;
            // 
            // FrmConfiguracaoEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 172);
            this.Controls.Add(this.NUpD_Height_Altura);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.NUpD_Width_Largura);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.NUpD_Botto_Inferior);
            this.Controls.Add(this.NUpD_Top_Topo);
            this.Controls.Add(this.NUpD_Right_Direito);
            this.Controls.Add(this.NUpD_Left_Esquerda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbModeloEtiqueta);
            this.Name = "FrmConfiguracaoEtiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Etiqueta";
            this.Load += new System.EventHandler(this.FrmConfiguracaoEtiqueta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Left_Esquerda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Right_Direito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Top_Topo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Botto_Inferior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Width_Largura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUpD_Height_Altura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbModeloEtiqueta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NUpD_Left_Esquerda;
        private System.Windows.Forms.NumericUpDown NUpD_Right_Direito;
        private System.Windows.Forms.NumericUpDown NUpD_Top_Topo;
        private System.Windows.Forms.NumericUpDown NUpD_Botto_Inferior;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown NUpD_Width_Largura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUpD_Height_Altura;
        private System.Windows.Forms.Label label7;
    }
}