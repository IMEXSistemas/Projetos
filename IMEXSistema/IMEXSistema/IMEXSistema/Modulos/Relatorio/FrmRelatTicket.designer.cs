namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatTicket
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EMPRESAEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESAEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EMPRESAEntityBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.LIS_PEDIDOCollectionBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.LIS_PRODUTOSPEDIDOCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.Ticket.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(444, 473);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // EMPRESAEntityBindingSource
            // 
            this.EMPRESAEntityBindingSource.DataSource = typeof(BMSworks.Model.EMPRESAEntity);
            // 
            // LIS_PEDIDOCollectionBindingSource
            // 
            this.LIS_PEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PEDIDOCollection);
            // 
            // LIS_PRODUTOSPEDIDOCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOCollection);
            // 
            // FrmRelatTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 473);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Modelo 3";
            this.Load += new System.EventHandler(this.FrmRelatTicket_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESAEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EMPRESAEntityBindingSource;
        private System.Windows.Forms.BindingSource LIS_PEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDIDOCollectionBindingSource;
    }
}