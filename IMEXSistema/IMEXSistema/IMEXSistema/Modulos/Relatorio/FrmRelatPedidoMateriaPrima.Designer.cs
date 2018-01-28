namespace BmsSoftware.Modulos.Relatorio
{
    partial class FrmRelatPedidoMateriaPrima
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
            this.LIS_PEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESACollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EMPRESAEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESAEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource)).BeginInit();
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
            // LIS_PEDIDOCollectionBindingSource
            // 
            this.LIS_PEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PEDIDOCollection);
            // 
            // LIS_PRODUTOSPEDIDOCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOCollection);
            // 
            // EMPRESACollectionBindingSource
            // 
            this.EMPRESACollectionBindingSource.DataSource = typeof(BMSworks.Model.EMPRESACollection);
            // 
            // EMPRESAEntityBindingSource
            // 
            this.EMPRESAEntityBindingSource.DataSource = typeof(BMSworks.Model.EMPRESAEntity);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.ARQUIVOBINARIOCollectionBindingSource;
            reportDataSource2.Name = "DataSet3";
            reportDataSource2.Value = this.LIS_CLIENTECollectionBindingSource;
            reportDataSource3.Name = "DataSet4";
            reportDataSource3.Value = this.LIS_PEDIDOCollectionBindingSource;
            reportDataSource4.Name = "DataSet5";
            reportDataSource4.Value = this.LIS_PRODUTOSPEDIDOCollectionBindingSource;
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.EMPRESACollectionBindingSource;
            reportDataSource6.Name = "DataSet6";
            reportDataSource6.Value = this.LIS_PRODUTOSPEDIDOCollectionBindingSource;
            reportDataSource7.Name = "DataSet7";
            reportDataSource7.Value = this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BmsSoftware.Modulos.Relatorio.PedidoMateriaPrima.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(997, 467);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOMTQOSCollection);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOCollection);
            // 
            // LIS_PRODUTOSPEDIDOMTQCollectionBindingSource
            // 
            this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource.DataSource = typeof(BMSworks.Model.LIS_PRODUTOSPEDIDOMTQCollection);
            // 
            // FrmRelatPedidoMateriaPrima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 467);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRelatPedidoMateriaPrima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Pedido por Matéria Prima";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRelatPedidoVendas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ARQUIVOBINARIOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_CLIENTECollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESACollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMPRESAEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EMPRESACollectionBindingSource;
        private System.Windows.Forms.BindingSource ARQUIVOBINARIOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_CLIENTECollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDIDOCollectionBindingSource;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource;
        private System.Windows.Forms.BindingSource EMPRESAEntityBindingSource;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource LIS_PRODUTOSPEDIDOMTQCollectionBindingSource;
    }
}