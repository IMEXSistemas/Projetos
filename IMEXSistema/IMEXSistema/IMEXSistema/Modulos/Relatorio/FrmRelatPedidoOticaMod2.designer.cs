namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatPedidoOticaMod2
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_CLIENTECollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.LIS_PEDIDOOTICACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDOTICACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_SERVICOPEDOTICACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DIAGPERTOPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DIAGMEDIOPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DIAGLONGEPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOOTICACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDOTICACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOPEDOTICACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGPERTOPEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGMEDIOPEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGLONGEPEDIDOCollectionBindingSource)).BeginInit();
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
            // LIS_CLIENTECollectionBindingSource
            // 
            this.LIS_CLIENTECollectionBindingSource.DataMember = "LIS_CLIENTECollection";
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
            reportDataSource4.Value = this.LIS_PEDIDOOTICACollectionBindingSource;
            reportDataSource5.Name = "DataSet5";
            reportDataSource5.Value = this.LIS_PRODUTOSPEDOTICACollectionBindingSource;
            reportDataSource6.Name = "DataSet6";
            reportDataSource6.Value = this.LIS_PRODUTOSPEDOTICACollectionBindingSource;
            reportDataSource7.Name = "DataSet7";
            reportDataSource7.Value = this.LIS_SERVICOPEDOTICACollectionBindingSource;
            reportDataSource8.Name = "DataSet8";
            reportDataSource8.Value = this.DIAGPERTOPEDIDOCollectionBindingSource;
            reportDataSource9.Name = "DataSet9";
            reportDataSource9.Value = this.DIAGMEDIOPEDIDOCollectionBindingSource;
            reportDataSource10.Name = "DataSet10";
            reportDataSource10.Value = this.DIAGLONGEPEDIDOCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource8);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.PedidoOticaMod2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(937, 407);
            this.reportViewer1.TabIndex = 0;
            // 
            // LIS_PEDIDOOTICACollectionBindingSource
            // 
            this.LIS_PEDIDOOTICACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PEDIDOOTICACollection);
            // 
            // LIS_PRODUTOSPEDOTICACollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDOTICACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDOTICACollection);
            // 
            // LIS_SERVICOPEDOTICACollectionBindingSource
            // 
            this.LIS_SERVICOPEDOTICACollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_SERVICOPEDOTICACollection);
            // 
            // DIAGPERTOPEDIDOCollectionBindingSource
            // 
            this.DIAGPERTOPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.DIAGPERTOPEDIDOCollection);
            // 
            // DIAGMEDIOPEDIDOCollectionBindingSource
            // 
            this.DIAGMEDIOPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.DIAGMEDIOPEDIDOCollection);
            // 
            // DIAGLONGEPEDIDOCollectionBindingSource
            // 
            this.DIAGLONGEPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.DIAGLONGEPEDIDOCollection);
            // 
            // FrmRelatPedidoOticaMod2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 407);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatPedidoOticaMod2";
            this.Text = "Pedido Modelo 2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRelatPedidoEconomico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOOTICACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDOTICACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOPEDOTICACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGPERTOPEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGMEDIOPEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DIAGLONGEPEDIDOCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_CLIENTECollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PEDIDOOTICACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDOTICACollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_SERVICOPEDOTICACollectionBindingSource;
        private System.Windows.Forms.BindingSource DIAGPERTOPEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource DIAGMEDIOPEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource DIAGLONGEPEDIDOCollectionBindingSource;
    }
}