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
    public partial class FrmPedidoMT3 : Form
    {
        public int idcliente = -1;
        public int IDPEDIDOMARC = -1;
        string listaproduto1 = "false";
        string listamaterial = "false";
        string listaproduto2 = "false";
        
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        public LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();
        public LIS_MATERIALPEDIDOCollection LIS_MATERIALPEDIDOColl = new LIS_MATERIALPEDIDOCollection();
        public LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
        
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmPedidoMT3()
        {
            InitializeComponent();
        }

        private void FrmPedidoMT3_Load(object sender, EventArgs e)
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

            //Dados do Pedido
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDOMARC.ToString()));
            LIS_PEDIDOMARCCollection LIS_PEDIDOMARCColl = new LIS_PEDIDOMARCCollection();
            LIS_PEDIDOMARCProvider LIS_PEDIDOMARCP = new LIS_PEDIDOMARCProvider();
            LIS_PEDIDOMARCColl = LIS_PEDIDOMARCP.ReadCollectionByParameter(RowRelatorio);


            //Dados do Cliente
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", idcliente.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[4];
            if (cpfcnpjPar == string.Empty)
                cpfcnpjPar = " ";

            if (LIS_PRODUTOPEDMARC2Coll.Count > 0)
                listaproduto1 = "true";

            if (LIS_MATERIALPEDIDOColl.Count > 0)
                listamaterial = "true";

            if (LIS_PRODUTOSPEDMARCColl.Count > 0)
                listaproduto2 = "true";


            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("listaproduto1", listaproduto1);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("listamaterial", listamaterial);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("listaproduto2", listaproduto2);            
            reportViewer1.LocalReport.SetParameters(p);


            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.LIS_PEDIDOMARCCollectionBindingSource.DataSource = LIS_PEDIDOMARCColl;
            this.LIS_PRODUTOPEDMARC2CollectionBindingSource.DataSource = LIS_PRODUTOPEDMARC2Coll;
            this.LIS_MATERIALPEDIDOCollectionBindingSource.DataSource = LIS_MATERIALPEDIDOColl;
            this.LIS_PRODUTOSPEDMARCCollectionBindingSource.DataSource = LIS_PRODUTOSPEDMARCColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
