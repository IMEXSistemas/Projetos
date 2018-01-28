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
    public partial class FrmRelatorioPedidoFesta : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_PEDIDOFESTACollection LIS_PEDIDOFESTAColl = new LIS_PEDIDOFESTACollection();
        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();

        LIS_PEDIDOFESTAProvider LIS_PEDIDOFESTAP = new LIS_PEDIDOFESTAProvider();

        public int IDCLIENTE = -1;
        public int IDPEDIDOFESTA = -1;
        public string VisualizaProduto = "false";
        public string VisualizaServico = "false";
        decimal TotalServicoProdutos = 0;

        public string NomeEmpresa = string.Empty;
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmRelatorioPedidoFesta()
        {
            InitializeComponent();
        }



        private void FrmRelatorioPedidoFesta_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.reportViewer1.RefreshReport();


            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESACollection EMPRESAColl = new EMPRESACollection();
            EMPRESAColl = EMPRESAP.ReadCollectionByParameter(null);

            NomeEmpresa = EMPRESAColl[0].NOMEFANTASIA;

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
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;


            //Dados do Pedido
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));
            LIS_PEDIDOFESTAProvider LIS_PEDIDOFESTAP = new LIS_PEDIDOFESTAProvider();
            LIS_PEDIDOFESTAColl = LIS_PEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

            //Dados do Produto
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));
            LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();
            LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();
            LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

            ///Dados do Servico
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));
            LIS_SERVICOPEDIDOFESTAProvider LIS_SERVICOPEDIDOFESTAP = new LIS_SERVICOPEDIDOFESTAProvider();
             LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl = new LIS_SERVICOPEDIDOFESTACollection();
             LIS_SERVICOPEDIDOFESTAColl = LIS_SERVICOPEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

            foreach (var item in LIS_SERVICOPEDIDOFESTAColl)
	            {
                    VisualizaServico = "true";
                    TotalServicoProdutos += Convert.ToDecimal(item.VALORTOTAL);
	            }

            foreach (var item in LIS_PRODUTOSPEDFESTAColl)
            {
                VisualizaProduto = "true";
                TotalServicoProdutos += Convert.ToDecimal(item.VALORTOTAL);
            }

            
            

            string titulo = "Nº PEDIDO " + IDPEDIDOFESTA.ToString().PadLeft(6, '0');
            if (LIS_PEDIDOFESTAColl[0].FLAGORCAMENTO.TrimEnd() == "S")
                titulo = "Nº ORÇAMENTO " + IDPEDIDOFESTA.ToString().PadLeft(6, '0');  

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[6];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", titulo.ToString().PadLeft(6, '0'));
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("nomeempresa", NomeEmpresa);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("VisualizaProduto", VisualizaProduto);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("TotalServicoProdutos", TotalServicoProdutos.ToString("n2"));
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("VisualizaServico", VisualizaServico);
            reportViewer1.LocalReport.SetParameters(p);


            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_PEDIDOFESTAEntityBindingSource.DataSource = LIS_PEDIDOFESTAColl;
            this.LIS_PRODUTOSPEDFESTACollectionBindingSource.DataSource = LIS_PRODUTOSPEDFESTAColl;
            this.LIS_SERVICOPEDIDOFESTACollectionBindingSource.DataSource = LIS_SERVICOPEDIDOFESTAColl;
            

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();


            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
