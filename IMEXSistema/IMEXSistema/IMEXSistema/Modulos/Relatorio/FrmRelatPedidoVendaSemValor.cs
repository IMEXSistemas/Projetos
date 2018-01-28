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
    public partial class FrmRelatPedidoVendaSemValor : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

        public int idcliente = -1;
        public int IDPEDIDO = -1;
        public string NomeEmpresa = string.Empty;
        public string VisualizaProduto1 = "false";
        public string VisualizaProduto2 = "false";
        public string NaoExibirValores = "true";
        public string NaoExibirImagemProduto = "true";
        public string NaoExibirTotalMT2 = "false";

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelatPedidoVendaSemValor()
        {
            InitializeComponent();
        }

        private void FrmRelatPedidoVenda3_Load(object sender, EventArgs e)
        {

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
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", idcliente.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            string cpfcnpjPar = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //Dados do Pedido
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
            LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            //Dados do Produto
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            //Dados do Produto MTQ
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

            string titulo = "Nº PEDIDO " + IDPEDIDO.ToString().PadLeft(6, '0');
            if (LIS_PEDIDOColl[0].FLAGORCAMENTO.TrimEnd() == "S")
                titulo = "Nº ORÇAMENTO " + IDPEDIDO.ToString().PadLeft(6, '0');

            decimal totalproduto = 0;


            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);

            }

            //Exibi preço do MT no pedido
            PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
            PEDIDOProvider PEDIDOP = new PEDIDOProvider();
            PEDIDOTy = PEDIDOP.Read(Convert.ToInt32(LIS_PEDIDOColl[0].IDPEDIDO));
            int contadoritem = 0;
            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);

                if (PEDIDOTy.FLAGVLMETRO.TrimEnd() == "S")
                    LIS_PRODUTOSPEDIDOMTQColl[contadoritem].VALORUNITARIO = item.VALORMETRO;

                if (NaoExibirImagemProduto == "true")
                    LIS_PRODUTOSPEDIDOMTQColl[contadoritem].FOTO = null;

                contadoritem++;
            }

            if (LIS_PRODUTOSPEDIDOMTQColl.Count > 0)
                VisualizaProduto1 = "true";

            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
                VisualizaProduto2 = "true";

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[8];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", titulo.ToString().PadLeft(6, '0'));
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("totalproduto", totalproduto.ToString("N2"));
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("nomeempresa", NomeEmpresa);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("VisualizaProduto1", VisualizaProduto1);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("VisualizaProduto2", VisualizaProduto2);
            p[6] = new Microsoft.Reporting.WinForms.ReportParameter("NaoExibirValores", NaoExibirValores);
            p[7] = new Microsoft.Reporting.WinForms.ReportParameter("NaoExibirTotalMT2", NaoExibirTotalMT2);

            reportViewer1.LocalReport.SetParameters(p);

            //Remover produtos que não vao ser exibido
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDO2 = new LIS_PRODUTOSPEDIDOCollection();
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                if (item.FLAGEXIBIR.TrimEnd().TrimStart() == "S")
                    LIS_PRODUTOSPEDIDO2.Add(item);
            }

            //Remover produtos que não vao ser exibido
            LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQ2 = new LIS_PRODUTOSPEDIDOMTQCollection();
            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                if (item.FLAGEXIBIR.TrimEnd().TrimStart() == "S")
                    LIS_PRODUTOSPEDIDOMTQ2.Add(item);
            }

            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_PEDIDOCollectionBindingSource.DataSource = LIS_PEDIDOColl;
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDO2;
            this.LIS_PRODUTOSPEDIDOMTQCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDOMTQ2;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
