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
    public partial class FrmReciboAvulsoRelat : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmReciboAvulsoRelat()
        {
            InitializeComponent();
        }

        public string DataReciboSelec = " ";
        public string ValorReciboSelec = " ";
        public string ValorExtensoSelec = " ";
        public string CPFCNPJSelec = " ";
        public string ReferenteSelec = " ";
        public string ObservacacoSelec = " ";
        
        
        public int CodClienteSelec = -1;

        private void FrmReciboAvulsoRelat_Load(object sender, EventArgs e)
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


            //Dados do cliente
            //Dados do Cliente
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", CodClienteSelec.ToString()));
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
            CPFCNPJSelec = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[6];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("DataRecibo", DataReciboSelec);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("ValorRecibo", ValorReciboSelec);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("ValorExtenso", ValorExtensoSelec);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("CPFCNPJ", CPFCNPJSelec);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("Referente", ReferenteSelec);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("Observacaco", ObservacacoSelec);           
            
            
                
            reportViewer1.LocalReport.SetParameters(p);
            
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
