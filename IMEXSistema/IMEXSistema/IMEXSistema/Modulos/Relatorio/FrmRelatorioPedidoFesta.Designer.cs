namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatorioPedidoFesta
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
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_CLIENTECollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PEDIDOFESTAEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSFESTASCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.LIS_PRODUTOSPEDFESTACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_SERVICOPEDIDOFESTACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOFESTAEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSFESTASCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDFESTACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOPEDIDOFESTACollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataSource = typeof(BMSworks.Model.EMPRESACollection);
            // 
            // ARQUIVOBINARIOCollectionBindingSource
            // 
            this.ARQUIVOBINARIOCollectionBindingSource.DataSource = typeof(BMSworks.Model.ARQUIVOBINARIOCollection);
            // 
            // LIS_CLIENTECollectionBindingSource
            // 
            this.LIS_CLIENTECollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_CLIENTECollection);
            // 
            // LIS_PEDIDOFESTAEntityBindingSource
            // 
            this.LIS_PEDIDOFESTAEntityBindingSource.DataSource = typeof(BMSworks.Model.LIS_PEDIDOFESTAEntity);
            // 
            // LIS_PRODUTOSFESTASCollectionBindingSource
            // 
            this.LIS_PRODUTOSFESTASCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSFESTASCollection);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EMPRESACollectionBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.LIS_CLIENTECollectionBindingSource;
            reportDataSource4.Name = "DataSet4";
            reportDataSource4.Value = this.LIS_PEDIDOFESTAEntityBindingSource;
            reportDataSource5.Name = "DataSet5";
            reportDataSource5.Value = this.LIS_PRODUTOSPEDFESTACollectionBindingSource;
            reportDataSource6.Name = "DataSet6";
            reportDataSource6.Value = this.LIS_SERVICOPEDIDOFESTACollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.PedidoFestaEvento2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1107, 553);
            this.reportViewer1.TabIndex = 0;
            // 
            // LIS_PRODUTOSPEDFESTACollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDFESTACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDFESTACollection);
            // 
            // LIS_SERVICOPEDIDOFESTACollectionBindingSource
            // 
            this.LIS_SERVICOPEDIDOFESTACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_SERVICOPEDIDOFESTACollection);
            // 
            // FrmRelatorioPedidoFesta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 553);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatorioPedidoFesta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido Festa/Eventos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRelatorioPedidoFesta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOFESTAEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSFESTASCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDFESTACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOPEDIDOFESTACollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_CLIENTECollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PEDIDOFESTAEntityBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSFESTASCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDFESTACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_SERVICOPEDIDOFESTACollectionBindingSource;


    }
}