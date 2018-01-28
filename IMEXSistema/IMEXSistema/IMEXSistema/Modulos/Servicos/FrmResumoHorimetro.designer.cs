namespace BmsSoftware.Modulos.Servicos
{
    partial class FrmResumoHorimetro
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbEquipamento = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.DataGriewDados = new System.Windows.Forms.DataGridView();
            this.IDEQUIPAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMEEQUIPAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATAMANUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KMMANUTENCAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KMATUAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KMPROXMANUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEquipamento
            // 
            this.cbEquipamento.FormattingEnabled = true;
            this.cbEquipamento.Location = new System.Drawing.Point(15, 24);
            this.cbEquipamento.Name = "cbEquipamento";
            this.cbEquipamento.Size = new System.Drawing.Size(394, 21);
            this.cbEquipamento.TabIndex = 212;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 9);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(138, 13);
            this.label21.TabIndex = 213;
            this.label21.Text = "Descrição do Equipamento:";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(15, 52);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 214;
            this.btnPesquisa.Text = "&Pesquisa";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(96, 52);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 215;
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(177, 52);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 216;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // DataGriewDados
            // 
            this.DataGriewDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriewDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDEQUIPAMENTO,
            this.NOMEEQUIPAMENTO,
            this.DATAMANUT,
            this.KMMANUTENCAO,
            this.KMATUAL,
            this.KMPROXMANUT});
            this.DataGriewDados.Location = new System.Drawing.Point(15, 81);
            this.DataGriewDados.Name = "DataGriewDados";
            this.DataGriewDados.ReadOnly = true;
            this.DataGriewDados.Size = new System.Drawing.Size(855, 282);
            this.DataGriewDados.TabIndex = 217;
            // 
            // IDEQUIPAMENTO
            // 
            this.IDEQUIPAMENTO.DataPropertyName = "IDEQUIPAMENTO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDEQUIPAMENTO.DefaultCellStyle = dataGridViewCellStyle3;
            this.IDEQUIPAMENTO.HeaderText = "Cód. Equipamento";
            this.IDEQUIPAMENTO.Name = "IDEQUIPAMENTO";
            this.IDEQUIPAMENTO.ReadOnly = true;
            // 
            // NOMEEQUIPAMENTO
            // 
            this.NOMEEQUIPAMENTO.DataPropertyName = "NOMEEQUIPAMENTO";
            this.NOMEEQUIPAMENTO.HeaderText = "Nome Equipamento";
            this.NOMEEQUIPAMENTO.Name = "NOMEEQUIPAMENTO";
            this.NOMEEQUIPAMENTO.ReadOnly = true;
            this.NOMEEQUIPAMENTO.Width = 300;
            // 
            // DATAMANUT
            // 
            this.DATAMANUT.DataPropertyName = "DATAMANUT";
            this.DATAMANUT.HeaderText = "Data Manutenção";
            this.DATAMANUT.Name = "DATAMANUT";
            this.DATAMANUT.ReadOnly = true;
            this.DATAMANUT.Width = 80;
            // 
            // KMMANUTENCAO
            // 
            this.KMMANUTENCAO.DataPropertyName = "KMMANUTENCAO";
            this.KMMANUTENCAO.HeaderText = "Km Manutenção";
            this.KMMANUTENCAO.Name = "KMMANUTENCAO";
            this.KMMANUTENCAO.ReadOnly = true;
            // 
            // KMATUAL
            // 
            this.KMATUAL.DataPropertyName = "KMATUAL";
            this.KMATUAL.HeaderText = "Km Atual";
            this.KMATUAL.Name = "KMATUAL";
            this.KMATUAL.ReadOnly = true;
            // 
            // KMPROXMANUT
            // 
            this.KMPROXMANUT.DataPropertyName = "KMPROXMANUT";
            this.KMPROXMANUT.HeaderText = "Km Prox. Manutenção";
            this.KMPROXMANUT.Name = "KMPROXMANUT";
            this.KMPROXMANUT.ReadOnly = true;
            // 
            // FrmResumoHorimetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 397);
            this.Controls.Add(this.DataGriewDados);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.cbEquipamento);
            this.Controls.Add(this.label21);
            this.Name = "FrmResumoHorimetro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumo de Km / Horímetro";
            this.Load += new System.EventHandler(this.FrmResumoHorimetro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGriewDados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEquipamento;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView DataGriewDados;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDEQUIPAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMEEQUIPAMENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATAMANUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn KMMANUTENCAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn KMATUAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn KMPROXMANUT;
    }
}