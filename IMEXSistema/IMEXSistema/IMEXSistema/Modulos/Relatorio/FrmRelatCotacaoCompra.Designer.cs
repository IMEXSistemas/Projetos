namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatCotacaoCompra
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.LIS_FORNECEDORCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_COTACAOCOMPRACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOCOTACAOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_FORNECEDORCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_COTACAOCOMPRACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOCOTACAOCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ARQUIVOBINARIOCollectionBindingSource
            // 
            this.ARQUIVOBINARIOCollectionBindingSource.DataMember = "ARQUIVOBINARIOCollection";
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataMember = "EMPRESACollection";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.EMPRESACollectionBindingSource;
            reportDataSource3.Name = "DataSet6";
            reportDataSource3.Value = this.LIS_FORNECEDORCollectionBindingSource;
            reportDataSource4.Name = "DataSet3";
            reportDataSource4.Value = this.LIS_COTACAOCOMPRACollectionBindingSource;
            reportDataSource5.Name = "DataSet4";
            reportDataSource5.Value = this.LIS_PRODUTOCOTACAOCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.CotacaoCompra.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(903, 464);
            this.reportViewer1.TabIndex = 0;
            // 
            // LIS_FORNECEDORCollectionBindingSource
            // 
            this.LIS_FORNECEDORCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_FORNECEDORCollection);
            // 
            // LIS_COTACAOCOMPRACollectionBindingSource
            // 
            this.LIS_COTACAOCOMPRACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_COTACAOCOMPRACollection);
            // 
            // LIS_PRODUTOCOTACAOCollectionBindingSource
            // 
            this.LIS_PRODUTOCOTACAOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOCOTACAOCollection);
            // 
            // FrmRelatCotacaoCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 464);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRelatCotacaoCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotação de Compra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCotacaoCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_FORNECEDORCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_COTACAOCOMPRACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOCOTACAOCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_FORNECEDORCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_COTACAOCOMPRACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOCOTACAOCollectionBindingSource;
    }
}