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
    public partial class FrmRelatOrdemServicoMec : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();
        LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
        LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
        LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();

        public int idcliente = -1;
        public int IDORDEMSERVICO = -1;
        public string listaproduto1Selec = "false";
        public string listaservicoSelec = "false";
        public string kmatualSelec = " ";
        public string kmproximanutSelec = " ";
        public string marca = " ";
        public string AnoVeiculo = " ";
        public string Seguradora = " ";
        public string CorVeiculo = " ";
        public string ChassisVeiculo = " ";

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmRelatOrdemServicoMec()
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
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
            LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();
            LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio);           

            //Dados do Produto
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
            LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
            LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            //Dados Servicos
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
            LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();
            LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_SERVICOOSFECHColl.Count > 0)
                listaservicoSelec = "true";

            string titulo = "Nº O.S " + IDORDEMSERVICO.ToString().PadLeft(6, '0');
            if (LIS_ORDEMSERVICOSFECHColl[0].FLAGORCAMENTO.TrimEnd() == "S")
                titulo = "Nº ORÇAMENTO " + IDORDEMSERVICO.ToString().PadLeft(6, '0');

            decimal totalproduto = 0;
            foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
            {
                totalproduto += Convert.ToDecimal(item.VALORTOTAL);
                listaproduto1Selec = "true";
            }

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[12];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", cpfcnpjPar);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("titulo", titulo.ToString().PadLeft(6, '0'));
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("totalproduto", totalproduto.ToString("N2"));
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("listaproduto1", listaproduto1Selec);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("listaservico", listaservicoSelec);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("kmatual", kmatualSelec);
            p[6] = new Microsoft.Reporting.WinForms.ReportParameter("kmproximanut", kmproximanutSelec);
            p[7] = new Microsoft.Reporting.WinForms.ReportParameter("Marca", marca);
            p[8] = new Microsoft.Reporting.WinForms.ReportParameter("AnoVeiculo", AnoVeiculo);          
            p[9] = new Microsoft.Reporting.WinForms.ReportParameter("Seguradora", Seguradora);
            p[10] = new Microsoft.Reporting.WinForms.ReportParameter("CorVeiculo", CorVeiculo);
            p[11] = new Microsoft.Reporting.WinForms.ReportParameter("ChassisVeiculo", ChassisVeiculo);
            
            

            reportViewer1.LocalReport.SetParameters(p);

            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_ORDEMSERVICOSFECHCollectionBindingSource.DataSource = LIS_ORDEMSERVICOSFECHColl;
            this.LIS_PRODUTOOSFECHCollectionBindingSource.DataSource = LIS_PRODUTOOSFECHColl;
            this.LIS_PRODUTOSPEDIDOMTQOSCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDOMTQOSColl;
            this.LIS_SERVICOOSFECHCollectionBindingSource.DataSource = LIS_SERVICOOSFECHColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
            
        }
    }
}
