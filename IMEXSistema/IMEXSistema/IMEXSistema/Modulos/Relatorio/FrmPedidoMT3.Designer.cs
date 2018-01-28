namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmPedidoMT3
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_CLIENTECollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PEDIDOMARCCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOPEDMARC2CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_MATERIALPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDMARCCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOMARCCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOPEDMARC2CollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_MATERIALPEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDMARCCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.EMPRESACollectionBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.LIS_CLIENTECollectionBindingSource;
            reportDataSource4.Name = "DataSet4";
            reportDataSource4.Value = this.LIS_PEDIDOMARCCollectionBindingSource;
            reportDataSource5.Name = "DataSet5";
            reportDataSource5.Value = this.LIS_PRODUTOPEDMARC2CollectionBindingSource;
            reportDataSource6.Name = "DataSet6";
            reportDataSource6.Value = this.LIS_MATERIALPEDIDOCollectionBindingSource;
            reportDataSource7.Name = "DataSet7";
            reportDataSource7.Value = this.LIS_PRODUTOSPEDMARCCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.PedidoMT3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(902, 463);
            this.reportViewer1.TabIndex = 0;
            // 
            // ARQUIVOBINARIOCollectionBindingSource
            // 
            this.ARQUIVOBINARIOCollectionBindingSource.DataSource = typeof(BMSworks.Model.ARQUIVOBINARIOCollection);
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataSource = typeof(BMSworks.Model.EMPRESACollection);
            // 
            // LIS_CLIENTECollectionBindingSource
            // 
            this.LIS_CLIENTECollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_CLIENTECollection);
            // 
            // LIS_PEDIDOMARCCollectionBindingSource
            // 
            this.LIS_PEDIDOMARCCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PEDIDOMARCCollection);
            // 
            // LIS_PRODUTOPEDMARC2CollectionBindingSource
            // 
            this.LIS_PRODUTOPEDMARC2CollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOPEDMARC2Collection);
            // 
            // LIS_MATERIALPEDIDOCollectionBindingSource
            // 
            this.LIS_MATERIALPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_MATERIALPEDIDOCollection);
            // 
            // LIS_PRODUTOSPEDMARCCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDMARCCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDMARCCollection);
            // 
            // FrmPedidoMT3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 463);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmPedidoMT3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido MT3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPedidoMT3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOMARCCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOPEDMARC2CollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_MATERIALPEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDMARCCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_CLIENTECollectionBindingSource;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PEDIDOMARCCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOPEDMARC2CollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_MATERIALPEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDMARCCollectionBindingSource;
    }
}