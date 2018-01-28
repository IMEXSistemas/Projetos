namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmPedidoMT3_Economico
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource13 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource14 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_CLIENTECollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PEDIDOMARCCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOPEDMARC2CollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_MATERIALPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDMARCCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOMARCCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOPEDMARC2CollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_MATERIALPEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDMARCCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
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
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource8.Name = "DataSet2";
            reportDataSource8.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource9.Name = "DataSet1";
            reportDataSource9.Value = this.EMPRESACollectionBindingSource;
            reportDataSource10.Name = "DataSet3";
            reportDataSource10.Value = this.LIS_CLIENTECollectionBindingSource;
            reportDataSource11.Name = "DataSet4";
            reportDataSource11.Value = this.LIS_PEDIDOMARCCollectionBindingSource;
            reportDataSource12.Name = "DataSet5";
            reportDataSource12.Value = this.LIS_PRODUTOPEDMARC2CollectionBindingSource;
            reportDataSource13.Name = "DataSet6";
            reportDataSource13.Value = this.LIS_MATERIALPEDIDOCollectionBindingSource;
            reportDataSource14.Name = "DataSet7";
            reportDataSource14.Value = this.LIS_PRODUTOSPEDMARCCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource8);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource11);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource12);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource13);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource14);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.PedidoMT3_Economico.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(902, 463);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmPedidoMT3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 463);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmPedidoMT3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido MT3 - Modelo Econômico";
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