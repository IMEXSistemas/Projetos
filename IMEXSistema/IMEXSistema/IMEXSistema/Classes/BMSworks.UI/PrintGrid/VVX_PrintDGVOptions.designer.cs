namespace VVX
{
    partial class PrintOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintOptions));
            this.rdoSelectedRows = new System.Windows.Forms.RadioButton();
            this.ctlPrintAllRowsRBTN = new System.Windows.Forms.RadioButton();
            this.ctlPrintToFitPageWidthCHK = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctlPrintTitleTBX = new System.Windows.Forms.TextBox();
            this.gboxRowsToPrint = new System.Windows.Forms.GroupBox();
            this.lblColumnsToPrint = new System.Windows.Forms.Label();
            this.ctlColumnsToPrintCHKLBX = new System.Windows.Forms.CheckedListBox();
            this.chkPaisagem = new System.Windows.Forms.CheckBox();
            this.chkData = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSelecionar = new System.Windows.Forms.CheckBox();
            this.gboxRowsToPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoSelectedRows
            // 
            this.rdoSelectedRows.AutoSize = true;
            this.rdoSelectedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSelectedRows.Location = new System.Drawing.Point(91, 19);
            this.rdoSelectedRows.Name = "rdoSelectedRows";
            this.rdoSelectedRows.Size = new System.Drawing.Size(95, 17);
            this.rdoSelectedRows.TabIndex = 1;
            this.rdoSelectedRows.TabStop = true;
            this.rdoSelectedRows.Text = "Selecionado";
            this.rdoSelectedRows.UseVisualStyleBackColor = true;
            // 
            // ctlPrintAllRowsRBTN
            // 
            this.ctlPrintAllRowsRBTN.AutoSize = true;
            this.ctlPrintAllRowsRBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlPrintAllRowsRBTN.Location = new System.Drawing.Point(9, 19);
            this.ctlPrintAllRowsRBTN.Name = "ctlPrintAllRowsRBTN";
            this.ctlPrintAllRowsRBTN.Size = new System.Drawing.Size(60, 17);
            this.ctlPrintAllRowsRBTN.TabIndex = 0;
            this.ctlPrintAllRowsRBTN.TabStop = true;
            this.ctlPrintAllRowsRBTN.Text = "Todos";
            this.ctlPrintAllRowsRBTN.UseVisualStyleBackColor = true;
            // 
            // ctlPrintToFitPageWidthCHK
            // 
            this.ctlPrintToFitPageWidthCHK.AutoSize = true;
            this.ctlPrintToFitPageWidthCHK.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ctlPrintToFitPageWidthCHK.Checked = true;
            this.ctlPrintToFitPageWidthCHK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctlPrintToFitPageWidthCHK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ctlPrintToFitPageWidthCHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlPrintToFitPageWidthCHK.Location = new System.Drawing.Point(253, 132);
            this.ctlPrintToFitPageWidthCHK.Name = "ctlPrintToFitPageWidthCHK";
            this.ctlPrintToFitPageWidthCHK.Size = new System.Drawing.Size(190, 18);
            this.ctlPrintToFitPageWidthCHK.TabIndex = 21;
            this.ctlPrintToFitPageWidthCHK.Text = "Ajustar à Largura da Página";
            this.ctlPrintToFitPageWidthCHK.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 13);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Título de Impressão";
            // 
            // ctlPrintTitleTBX
            // 
            this.ctlPrintTitleTBX.AcceptsReturn = true;
            this.ctlPrintTitleTBX.Location = new System.Drawing.Point(9, 25);
            this.ctlPrintTitleTBX.Multiline = true;
            this.ctlPrintTitleTBX.Name = "ctlPrintTitleTBX";
            this.ctlPrintTitleTBX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ctlPrintTitleTBX.Size = new System.Drawing.Size(446, 40);
            this.ctlPrintTitleTBX.TabIndex = 19;
            // 
            // gboxRowsToPrint
            // 
            this.gboxRowsToPrint.Controls.Add(this.rdoSelectedRows);
            this.gboxRowsToPrint.Controls.Add(this.ctlPrintAllRowsRBTN);
            this.gboxRowsToPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxRowsToPrint.Location = new System.Drawing.Point(253, 84);
            this.gboxRowsToPrint.Name = "gboxRowsToPrint";
            this.gboxRowsToPrint.Size = new System.Drawing.Size(202, 42);
            this.gboxRowsToPrint.TabIndex = 18;
            this.gboxRowsToPrint.TabStop = false;
            this.gboxRowsToPrint.Text = "Linhas para Imprimir";
            // 
            // lblColumnsToPrint
            // 
            this.lblColumnsToPrint.AutoSize = true;
            this.lblColumnsToPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnsToPrint.Location = new System.Drawing.Point(6, 68);
            this.lblColumnsToPrint.Name = "lblColumnsToPrint";
            this.lblColumnsToPrint.Size = new System.Drawing.Size(128, 13);
            this.lblColumnsToPrint.TabIndex = 17;
            this.lblColumnsToPrint.Text = "Colunas para Imprimir";
            // 
            // ctlColumnsToPrintCHKLBX
            // 
            this.ctlColumnsToPrintCHKLBX.CheckOnClick = true;
            this.ctlColumnsToPrintCHKLBX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlColumnsToPrintCHKLBX.FormattingEnabled = true;
            this.ctlColumnsToPrintCHKLBX.Location = new System.Drawing.Point(9, 84);
            this.ctlColumnsToPrintCHKLBX.Name = "ctlColumnsToPrintCHKLBX";
            this.ctlColumnsToPrintCHKLBX.Size = new System.Drawing.Size(232, 244);
            this.ctlColumnsToPrintCHKLBX.TabIndex = 13;
            this.ctlColumnsToPrintCHKLBX.Click += new System.EventHandler(this.ctlColumnsToPrintCHKLBX_Click);
            // 
            // chkPaisagem
            // 
            this.chkPaisagem.AutoSize = true;
            this.chkPaisagem.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPaisagem.Checked = true;
            this.chkPaisagem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaisagem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkPaisagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPaisagem.Location = new System.Drawing.Point(253, 156);
            this.chkPaisagem.Name = "chkPaisagem";
            this.chkPaisagem.Size = new System.Drawing.Size(121, 18);
            this.chkPaisagem.TabIndex = 22;
            this.chkPaisagem.Text = "Modo Paisagem";
            this.chkPaisagem.UseVisualStyleBackColor = true;
            // 
            // chkData
            // 
            this.chkData.AutoSize = true;
            this.chkData.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkData.Checked = true;
            this.chkData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkData.Location = new System.Drawing.Point(253, 180);
            this.chkData.Name = "chkData";
            this.chkData.Size = new System.Drawing.Size(94, 18);
            this.chkData.TabIndex = 23;
            this.chkData.Text = "Exibir Data";
            this.chkData.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(302, 326);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "Imprimir";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(383, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Sair";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkSelecionar
            // 
            this.chkSelecionar.AutoSize = true;
            this.chkSelecionar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelecionar.Location = new System.Drawing.Point(9, 331);
            this.chkSelecionar.Name = "chkSelecionar";
            this.chkSelecionar.Size = new System.Drawing.Size(92, 18);
            this.chkSelecionar.TabIndex = 26;
            this.chkSelecionar.Text = "Selecionar";
            this.chkSelecionar.UseVisualStyleBackColor = true;
            this.chkSelecionar.Click += new System.EventHandler(this.chkSelecionar_Click);
            // 
            // PrintOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 355);
            this.Controls.Add(this.chkSelecionar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkData);
            this.Controls.Add(this.chkPaisagem);
            this.Controls.Add(this.ctlPrintToFitPageWidthCHK);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctlPrintTitleTBX);
            this.Controls.Add(this.gboxRowsToPrint);
            this.Controls.Add(this.lblColumnsToPrint);
            this.Controls.Add(this.ctlColumnsToPrintCHKLBX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PrintOptions";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opções de Impressão";
            this.Load += new System.EventHandler(this.OnLoadForm);
            this.gboxRowsToPrint.ResumeLayout(false);
            this.gboxRowsToPrint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton rdoSelectedRows;
        internal System.Windows.Forms.RadioButton ctlPrintAllRowsRBTN;
        internal System.Windows.Forms.CheckBox ctlPrintToFitPageWidthCHK;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.TextBox ctlPrintTitleTBX;
        internal System.Windows.Forms.GroupBox gboxRowsToPrint;
        internal System.Windows.Forms.Label lblColumnsToPrint;
        internal System.Windows.Forms.CheckedListBox ctlColumnsToPrintCHKLBX;
        internal System.Windows.Forms.CheckBox chkPaisagem;
        internal System.Windows.Forms.CheckBox chkData;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.CheckBox chkSelecionar;

    }
}