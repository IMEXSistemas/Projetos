namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatorioNotaCompra
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_ESTOQUEESCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_FORNECEDORCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_MOVPRODUTOESCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_ESTOQUEESCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_FORNECEDORCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_MOVPRODUTOESCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EMPRESACollectionBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.LIS_ESTOQUEESCollectionBindingSource;
            reportDataSource4.Name = "DataSet4";
            reportDataSource4.Value = this.LIS_FORNECEDORCollectionBindingSource;
            reportDataSource5.Name = "DataSet5";
            reportDataSource5.Value = this.LIS_MOVPRODUTOESCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.NotaCompra.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(918, 407);
            this.reportViewer1.TabIndex = 0;
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataSource = typeof(BMSworks.Model.EMPRESACollection);
            // 
            // ARQUIVOBINARIOCollectionBindingSource
            // 
            this.ARQUIVOBINARIOCollectionBindingSource.DataSource = typeof(BMSworks.Model.ARQUIVOBINARIOCollection);
            // 
            // LIS_ESTOQUEESCollectionBindingSource
            // 
            this.LIS_ESTOQUEESCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_ESTOQUEESCollection);
            // 
            // LIS_FORNECEDORCollectionBindingSource
            // 
            this.LIS_FORNECEDORCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_FORNECEDORCollection);
            // 
            // LIS_MOVPRODUTOESCollectionBindingSource
            // 
            this.LIS_MOVPRODUTOESCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_MOVPRODUTOESCollection);
            // 
            // FrmRelatorioNotaCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 407);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatorioNotaCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota de Compra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRelatorioNotaCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_ESTOQUEESCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_FORNECEDORCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_MOVPRODUTOESCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_ESTOQUEESCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_FORNECEDORCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_MOVPRODUTOESCollectionBindingSource;
    }
}