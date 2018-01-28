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
    public partial class FrmNotaServicoRelatorio : Form
    {
        LIS_NOTASERVICOCollection LIS_NOTASERVICOColl = new LIS_NOTASERVICOCollection();
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_SERVICONPSCollection LIS_SERVICONPSColl = new LIS_SERVICONPSCollection();
        
        LIS_NOTASERVICOProvider LIS_NOTASERVICOP = new LIS_NOTASERVICOProvider();
        LIS_SERVICONPSProvider LIS_SERVICONPSP = new LIS_SERVICONPSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int _IDNOTASERVICO = -1;

        public FrmNotaServicoRelatorio()
        {
            InitializeComponent();
        }

        private void FrmNotaServicoRelatorio_Load(object sender, EventArgs e)
        {
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

            //Dados da Nota de Servico
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTASERVICO", "System.Int32", "=", _IDNOTASERVICO.ToString()));
            LIS_NOTASERVICOColl = LIS_NOTASERVICOP.ReadCollectionByParameter(RowRelatorio);

            //Dados do Cliente
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_NOTASERVICOColl[0].IDCLIENTE.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //Nota Servico
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTASERVICO", "System.Int32", "=", _IDNOTASERVICO.ToString()));
            LIS_NOTASERVICOColl = LIS_NOTASERVICOP.ReadCollectionByParameter(RowRelatorio);

            //Servicos
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTASERVICO", "System.Int32", "=", _IDNOTASERVICO.ToString()));
            LIS_SERVICONPSColl = LIS_SERVICONPSP.ReadCollectionByParameter(RowRelatorio);
      

            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[1];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar); 
            reportViewer1.LocalReport.SetParameters(p);
      

            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.LIS_NOTASERVICOCollectionBindingSource.DataSource = LIS_NOTASERVICOColl;
            this.LIS_SERVICONPSCollectionBindingSource.DataSource = LIS_SERVICONPSColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
