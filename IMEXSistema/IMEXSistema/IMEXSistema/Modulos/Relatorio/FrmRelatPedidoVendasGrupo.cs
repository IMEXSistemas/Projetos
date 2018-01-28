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
    public partial class FrmRelatPedidoVendasGrupo : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

        public Boolean ExibirApenasVlUnitario = false;
        public int idcliente = -1;
        public int IDPEDIDO = -1;
        public string NaoExibirValores = "true";

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmRelatPedidoVendasGrupo()
        {
            InitializeComponent();
        }

        private void FrmRelatPedidoVendas_Load(object sender, EventArgs e)
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

            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOPrint = new LIS_PRODUTOSPEDIDOCollection();

            string titulo = "Nº PEDIDO " + IDPEDIDO.ToString().PadLeft(6, '0');
            if (LIS_PEDIDOColl[0].FLAGORCAMENTO.TrimEnd() == "S")
                titulo = "Nº ORÇAMENTO " + IDPEDIDO.ToString().PadLeft(6, '0');

            decimal totalproduto = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);
                LIS_PRODUTOSPEDIDOPrint.Add(item);
            }


            //Exibi preço do MT no pedido
            PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
            PEDIDOProvider PEDIDOP = new PEDIDOProvider();
            PEDIDOTy = PEDIDOP.Read(Convert.ToInt32(LIS_PEDIDOColl[0].IDPEDIDO));
            int contadoritem = 0;
            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);
                LIS_PRODUTOSPEDIDOEntity LIS_PRODUTOSPEDIDOTy = new LIS_PRODUTOSPEDIDOEntity();
                LIS_PRODUTOSPEDIDOTy.IDPRODUTO = item.IDPRODUTO;
                LIS_PRODUTOSPEDIDOTy.NOMEPRODUTO = item.NOMEPRODUTO;
                LIS_PRODUTOSPEDIDOTy.DADOSADICIONAIS = item.DADOADICIONAIS;
                LIS_PRODUTOSPEDIDOTy.QUANTIDADE = item.QUANTIDADE;

                if (PEDIDOTy.FLAGVLMETRO.TrimEnd() == "S")
                    LIS_PRODUTOSPEDIDOTy.VALORUNITARIO = item.VALORMETRO;
                else
                    LIS_PRODUTOSPEDIDOTy.VALORUNITARIO = item.VALORUNITARIO;

                LIS_PRODUTOSPEDIDOTy.NOMECOR = item.NOMECOR;
                LIS_PRODUTOSPEDIDOTy.IDAMBIENTE = item.IDAMBIENTE;
                LIS_PRODUTOSPEDIDOTy.NOMEAMBIENTE = item.NOMEAMBIENTE;
                LIS_PRODUTOSPEDIDOTy.ALTURA = item.ALTURA;
                LIS_PRODUTOSPEDIDOTy.LARGURA = item.LARGURA;
                LIS_PRODUTOSPEDIDOTy.TOTALMT = item.MT2;

                if (!ExibirApenasVlUnitario)
                    LIS_PRODUTOSPEDIDOTy.VALORTOTAL = 0;
                else
                    LIS_PRODUTOSPEDIDOTy.VALORTOTAL =item.VALORTOTAL;

                LIS_PRODUTOSPEDIDOTy.FLAGEXIBIR = item.FLAGEXIBIR;
                LIS_PRODUTOSPEDIDOPrint.Add(LIS_PRODUTOSPEDIDOTy);

                contadoritem++;
            }

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[4];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", titulo.ToString().PadLeft(6, '0'));
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("totalproduto", totalproduto.ToString("n2"));
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("NaoExibirValores", NaoExibirValores);

            reportViewer1.LocalReport.SetParameters(p);

            //Remover produtos que não vao ser exibido
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDO2 = new LIS_PRODUTOSPEDIDOCollection();
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOPrint)
            {
                if (item.FLAGEXIBIR.TrimEnd().TrimStart() == "S")
                    LIS_PRODUTOSPEDIDO2.Add(item);
            }


            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_PEDIDOCollectionBindingSource.DataSource = LIS_PEDIDOColl;
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDO2;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
            
        }
    }
}

