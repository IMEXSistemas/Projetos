namespace BmsSoftware.Classes.BMSworks.UI
{
    partial class FomExportPDF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FomExportPDF));
            this.lblColumnsToPrint = new System.Windows.Forms.Label();
            this.ctlColumnsToPrintCHKLBX = new System.Windows.Forms.CheckedListBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtNomeArquivo = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.chkExibirData = new System.Windows.Forms.CheckBox();
            this.chkSelecionar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblColumnsToPrint
            // 
            this.lblColumnsToPrint.AutoSize = true;
            this.lblColumnsToPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnsToPrint.Location = new System.Drawing.Point(9, 10);
            this.lblColumnsToPrint.Name = "lblColumnsToPrint";
            this.lblColumnsToPrint.Size = new System.Drawing.Size(132, 13);
            this.lblColumnsToPrint.TabIndex = 19;
            this.lblColumnsToPrint.Text = "Colunas para Exportar";
            // 
            // ctlColumnsToPrintCHKLBX
            // 
            this.ctlColumnsToPrintCHKLBX.CheckOnClick = true;
            this.ctlColumnsToPrintCHKLBX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlColumnsToPrintCHKLBX.FormattingEnabled = true;
            this.ctlColumnsToPrintCHKLBX.Location = new System.Drawing.Point(12, 26);
            this.ctlColumnsToPrintCHKLBX.Name = "ctlColumnsToPrintCHKLBX";
            this.ctlColumnsToPrintCHKLBX.Size = new System.Drawing.Size(232, 214);
            this.ctlColumnsToPrintCHKLBX.TabIndex = 18;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(345, 246);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 20;
            this.btnImprimir.Text = "&Exportar";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(426, 246);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 21;
            this.btnSair.Text = "&Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtNomeArquivo
            // 
            this.txtNomeArquivo.AcceptsReturn = true;
            this.txtNomeArquivo.Location = new System.Drawing.Point(250, 26);
            this.txtNomeArquivo.MaxLength = 20;
            this.txtNomeArquivo.Name = "txtNomeArquivo";
            this.txtNomeArquivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNomeArquivo.Size = new System.Drawing.Size(229, 20);
            this.txtNomeArquivo.TabIndex = 22;
            this.txtNomeArquivo.Text = "ListadaPesquisa";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(250, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(108, 13);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "Nome do Arquivo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(247, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Titulo:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.AcceptsReturn = true;
            this.txtTitulo.Location = new System.Drawing.Point(250, 65);
            this.txtTitulo.MaxLength = 50;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTitulo.Size = new System.Drawing.Size(229, 20);
            this.txtTitulo.TabIndex = 24;
            // 
            // chkExibirData
            // 
            this.chkExibirData.AutoSize = true;
            this.chkExibirData.Checked = true;
            this.chkExibirData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExibirData.Location = new System.Drawing.Point(253, 92);
            this.chkExibirData.Name = "chkExibirData";
            this.chkExibirData.Size = new System.Drawing.Size(77, 17);
            this.chkExibirData.TabIndex = 26;
            this.chkExibirData.Text = "Exibir Data";
            this.chkExibirData.UseVisualStyleBackColor = true;
            // 
            // chkSelecionar
            // 
            this.chkSelecionar.AutoSize = true;
            this.chkSelecionar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelecionar.Location = new System.Drawing.Point(12, 251);
            this.chkSelecionar.Name = "chkSelecionar";
            this.chkSelecionar.Size = new System.Drawing.Size(107, 18);
            this.chkSelecionar.TabIndex = 27;
            this.chkSelecionar.Text = "Selec. Todos";
            this.chkSelecionar.UseVisualStyleBackColor = true;
            this.chkSelecionar.Click += new System.EventHandler(this.chkSelecionar_Click);
            // 
            // FomExportPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 275);
            this.Controls.Add(this.chkSelecionar);
            this.Controls.Add(this.chkExibirData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtNomeArquivo);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblColumnsToPrint);
            this.Controls.Add(this.ctlColumnsToPrintCHKLBX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FomExportPDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar para PDF";
            this.Load += new System.EventHandler(this.FomExportPDF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblColumnsToPrint;
        internal System.Windows.Forms.CheckedListBox ctlColumnsToPrintCHKLBX;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        internal System.Windows.Forms.TextBox txtNomeArquivo;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.CheckBox chkExibirData;
        internal System.Windows.Forms.CheckBox chkSelecionar;
    }
}