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
    public partial class FrmRelaReserva_2 : Form
    {
        public int codclienteSelec = -1;
        public int IDRESERVASELEC= -1;
        public string controleSelec = string.Empty;
        public string vlpagoSelec = "0";
        public string nomeempresa = string.Empty;
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_RESERVACollection LIS_RESERVAColl = new LIS_RESERVACollection();
        LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl = new LIS_PRODUTORESERVACollection();


        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelaReserva_2()
        {
            InitializeComponent();
        }

        private void FrmRelaReserva_2_Load(object sender, EventArgs e)
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

 
            nomeempresa = EMPRESAColl[0].NOMEFANTASIA;
 
            //Dados do Cliente
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", codclienteSelec.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //Dados do Reserva
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", IDRESERVASELEC.ToString()));
            LIS_RESERVAProvider LIS_RESERVAP = new LIS_RESERVAProvider();
            LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(RowRelatorio);

            //Dados do Produto Reserva
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", IDRESERVASELEC.ToString()));
            LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP  = new LIS_PRODUTORESERVAProvider();
            LIS_PRODUTORESERVAColl = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[5];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("cpfcnpj", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("controle", controleSelec);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("vlpago", vlpagoSelec);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("nomeempresa", nomeempresa);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", IDRESERVASELEC.ToString().PadLeft(6, '0'));
            

            reportViewer1.LocalReport.SetParameters(p);

            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.LIS_RESERVAEntityBindingSource.DataSource = LIS_RESERVAColl;
            this.LIS_PRODUTORESERVACollectionBindingSource.DataSource = LIS_PRODUTORESERVAColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
