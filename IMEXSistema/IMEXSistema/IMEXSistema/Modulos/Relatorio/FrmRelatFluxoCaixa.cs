using BMSworks.Firebird;
using BMSworks.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Relatorio
{
    public partial class FrmRelatFluxoCaixa : Form
    {
        public LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        public LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        public string TotalReceber = string.Empty;
        public string TotalPagar = string.Empty;
        public string Saldo = string.Empty;

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelatFluxoCaixa()
        {
            InitializeComponent();
        }

        private void FrmRelatFluxoCaixa_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESACollection EMPRESAColl = new EMPRESACollection();
            EMPRESAColl = EMPRESAP.ReadCollectionByParameter(null);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOCollection ARQUIVOBINARIOColl = new ARQUIVOBINARIOCollection();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDARQUIVOBINARIO", "System.Int32", "=", CONFISISTEMAty.IDARQUIVOBINARIO1.ToString()));
                    ARQUIVOBINARIOColl = ARQUIVOBINARIOP.ReadCollectionByParameter(RowRelatorio);
                    this.ARQUIVOBINARIOCollectionBindingSource.DataSource = ARQUIVOBINARIOColl;
                }
            }

              //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[3];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("TotalReceber", TotalReceber);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("TotalPagar", TotalPagar);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("Saldo",Saldo);


            reportViewer1.LocalReport.SetParameters(p);

            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            LIS_DUPLICATAPAGARCollectionBindingSource.DataSource = LIS_DUPLICATAPAGARColl;
            LIS_DUPLICATARECEBERCollectionBindingSource.DataSource = LIS_DUPLICATARECEBERColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();


        }
    }
}
