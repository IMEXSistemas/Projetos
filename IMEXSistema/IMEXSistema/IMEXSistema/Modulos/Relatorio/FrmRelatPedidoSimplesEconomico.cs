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
    public partial class FrmRelatPedidoSimplesEconomico : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

        public int idcliente = -1;
        public int IDPEDIDO = -1;
        public string dataretiradaSelec = string.Empty;
        public string datauso = string.Empty;
        public string obsanexo = string.Empty;
        public string nomeempresa = string.Empty;


        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelatPedidoSimplesEconomico()
        {
            InitializeComponent();
        }

        private void FrmPedidoSimples_Load(object sender, EventArgs e)
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

            string titulo = "VENDA: " + IDPEDIDO.ToString().PadLeft(6, '0');
            if (LIS_PEDIDOColl[0].FLAGORCAMENTO.TrimEnd() == "S")
                titulo = "ALUGUEL: " + IDPEDIDO.ToString().PadLeft(6, '0');

            decimal totalproduto = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);
            }

            //Adiciona Dados de Medidas
            int Contador = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                if(item.QUADRIL > 0 || item.BUSTO > 0 || item.CINTURA > 0)
                    LIS_PRODUTOSPEDIDOColl[Contador].NOMEPRODUTO = LIS_PRODUTOSPEDIDOColl[Contador].NOMEPRODUTO + " Busto/Torax: "  + Convert.ToDecimal(item.BUSTO).ToString("n2") +
                        " Cintura: " + item.CINTURA + " Quadril: " + item.QUADRIL;

                if (item.COLARINHO > 0 || item.MANGA > 0 || item.ALTURA > 0 || item.BARRA > 0)
                    LIS_PRODUTOSPEDIDOColl[Contador].NOMEPRODUTO += " Colarinho: " + Convert.ToDecimal(item.COLARINHO).ToString("n2") +
                        " Manga: " + Convert.ToDecimal(item.MANGA).ToString("n2") + " Comprimento: " + Convert.ToDecimal(item.ALTURA).ToString("n2") + " Barra: " + Convert.ToDecimal(item.ALTURA).ToString("n2");


                Contador++;
            }


            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[7];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", titulo.ToString().PadLeft(6, '0'));
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("totalproduto", totalproduto.ToString("N2"));
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("dataretirada", dataretiradaSelec);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("obsanexo", obsanexo);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("nomeempresa", nomeempresa);
            p[6] = new Microsoft.Reporting.WinForms.ReportParameter("datauso", datauso);
            
            

            reportViewer1.LocalReport.SetParameters(p);

            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_PEDIDOCollectionBindingSource.DataSource = LIS_PEDIDOColl;
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDOColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
