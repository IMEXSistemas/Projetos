namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatOrdemServico6
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
            this.ARQUIVOBINARIOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_CLIENTECollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_ORDEMSERVICOSFECHCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOOSFECHCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_SERVICOOSFECHCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_EQUIPAMENTOOSFECHCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_ORDEMSERVICOSFECHCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOOSFECHCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOOSFECHCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_EQUIPAMENTOOSFECHCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ARQUIVOBINARIOCollectionBindingSource
            // 
            this.ARQUIVOBINARIOCollectionBindingSource.DataSource = typeof(BMSworks.Model.ARQUIVOBINARIOCollection);
            // 
            // LIS_CLIENTECollectionBindingSource
            // 
            this.LIS_CLIENTECollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_CLIENTECollection);
            // 
            // LIS_ORDEMSERVICOSFECHCollectionBindingSource
            // 
            this.LIS_ORDEMSERVICOSFECHCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_ORDEMSERVICOSFECHCollection);
            // 
            // LIS_PRODUTOOSFECHCollectionBindingSource
            // 
            this.LIS_PRODUTOOSFECHCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOOSFECHCollection);
            // 
            // LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOMTQOSCollection);
            // 
            // LIS_SERVICOOSFECHCollectionBindingSource
            // 
            this.LIS_SERVICOOSFECHCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_SERVICOOSFECHCollection);
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataSource = typeof(BMSworks.Model.EMPRESACollection);
            // 
            // LIS_EQUIPAMENTOOSFECHCollectionBindingSource
            // 
            this.LIS_EQUIPAMENTOOSFECHCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_EQUIPAMENTOOSFECHCollection);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource2.Name = "DataSet3";
            reportDataSource2.Value = this.LIS_CLIENTECollectionBindingSource;
            reportDataSource3.Name = "DataSet4";
            reportDataSource3.Value = this.LIS_ORDEMSERVICOSFECHCollectionBindingSource;
            reportDataSource4.Name = "DataSet5";
            reportDataSource4.Value = this.LIS_PRODUTOOSFECHCollectionBindingSource;
            reportDataSource5.Name = "DataSet6";
            reportDataSource5.Value = this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource;
            reportDataSource6.Name = "DataSet7";
            reportDataSource6.Value = this.LIS_SERVICOOSFECHCollectionBindingSource;
            reportDataSource7.Name = "DataSet1";
            reportDataSource7.Value = this.EMPRESACollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.OrdemServicoMod6.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(997, 467);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmRelatOrdemServico6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 467);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatOrdemServico6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ordem de Serviços - Modelo 6";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRelatPedidoVendas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_ORDEMSERVICOSFECHCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOOSFECHCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_SERVICOOSFECHCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_EQUIPAMENTOOSFECHCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_CLIENTECollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOOSFECHCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_ORDEMSERVICOSFECHCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_SERVICOOSFECHCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_EQUIPAMENTOOSFECHCollectionBindingSource;
    }
}