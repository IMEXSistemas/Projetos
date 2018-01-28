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
    public partial class FrmRelatPedidoOticaMod2 : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        public LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
        public LIS_SERVICOPEDOTICACollection LIS_SERVICOPEDOTICAColl = new LIS_SERVICOPEDOTICACollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public string  listaproduto = "false";
        public string  listaservico = "false";
        public string NomeLaboratorio = string.Empty;
        public string ExibirPerto = "true";
        public string ExibirMedio = "true";
        public string ExibirLonge = "true";
        
        public int idcliente = -1;
        public int IDPEDIDOOTICA = -1;

        public FrmRelatPedidoOticaMod2()
        {
            InitializeComponent();
        }

        private void FrmRelatPedidoEconomico_Load(object sender, EventArgs e)
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

            //Dados do Cliente
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", idcliente.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //Dados do Pedido Otica
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
            LIS_PEDIDOOTICACollection LIS_PEDIDOOTICAColl = new LIS_PEDIDOOTICACollection();
            LIS_PEDIDOOTICAProvider LIS_PEDIDOOTICAP = new LIS_PEDIDOOTICAProvider();
            LIS_PEDIDOOTICAColl = LIS_PEDIDOOTICAP.ReadCollectionByParameter(RowRelatorio);

            //Diagnóstico  Perto
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
            DIAGPERTOPEDIDOProvider DIAGPERTOPEDIDOP = new DIAGPERTOPEDIDOProvider();
            DIAGPERTOPEDIDOCollection DIAGPERTOPEDIDOColl = new DIAGPERTOPEDIDOCollection();
            DIAGPERTOPEDIDOColl = DIAGPERTOPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            //Diagnóstico  Medio
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
            DIAGMEDIOPEDIDOProvider DIAGMEDIOPEDIDOP = new DIAGMEDIOPEDIDOProvider();
            DIAGMEDIOPEDIDOCollection DIAGMEDIOPEDIDOColl = new DIAGMEDIOPEDIDOCollection();
            DIAGMEDIOPEDIDOColl = DIAGMEDIOPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            //Diagnóstico  Longe
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
            DIAGLONGEPEDIDOProvider DIAGLONGEPEDIDOP = new DIAGLONGEPEDIDOProvider();
            DIAGLONGEPEDIDOCollection DIAGLONGEPEDIDOColl = new DIAGLONGEPEDIDOCollection();
            DIAGLONGEPEDIDOColl = DIAGLONGEPEDIDOP.ReadCollectionByParameter(RowRelatorio);
            
            if (LIS_PRODUTOSPEDOTICAColl.Count > 0)
                listaproduto = "true";

            if (LIS_SERVICOPEDOTICAColl.Count > 0)
                listaservico = "true";

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[7];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("listaproduto", listaproduto);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("listaservico", listaservico);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("NomeLaboratorio", NomeLaboratorio);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("ExibirPerto", ExibirPerto);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("ExibirMedio", ExibirMedio);
            p[6] = new Microsoft.Reporting.WinForms.ReportParameter("ExibirLonge", ExibirLonge);            
            reportViewer1.LocalReport.SetParameters(p);

            EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            LIS_PEDIDOOTICACollectionBindingSource.DataSource = LIS_PEDIDOOTICAColl;
            LIS_SERVICOPEDOTICACollectionBindingSource.DataSource = LIS_SERVICOPEDOTICAColl;
            LIS_PRODUTOSPEDOTICACollectionBindingSource.DataSource = LIS_PRODUTOSPEDOTICAColl;
            DIAGPERTOPEDIDOCollectionBindingSource.DataSource = DIAGPERTOPEDIDOColl;
            DIAGMEDIOPEDIDOCollectionBindingSource.DataSource = DIAGMEDIOPEDIDOColl;
            DIAGLONGEPEDIDOCollectionBindingSource.DataSource = DIAGLONGEPEDIDOColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
