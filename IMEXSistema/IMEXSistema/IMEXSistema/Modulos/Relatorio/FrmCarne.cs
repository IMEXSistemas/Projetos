using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
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
    public partial class FrmCarne : Form
    {
        Utility Util = new Utility();

        string _Cedente = " ";
        string _NomeCliente = " ";
        string _CPF_CNPJ = " ";
        string _EnderecoCliente = " ";

        public LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmCarne()
        {
            InitializeComponent();
        }

        private void FrmCarne_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESACollection EMPRESAColl = new EMPRESACollection();
                EMPRESAColl = EMPRESAP.ReadCollectionByParameter(null);

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_DUPLICATARECEBERColl[0].IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                _Cedente = EMPRESAColl[0].NOMEFANTASIA;
                _NomeCliente = LIS_CLIENTEColl[0].NOME;
                _CPF_CNPJ = LIS_CLIENTEColl[0].CPF;
                if (LIS_CLIENTEColl[0].CNPJ != "  .   .   /    -")
                    _CPF_CNPJ = LIS_CLIENTEColl[0].CNPJ;

                _EnderecoCliente = Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER + " - " + LIS_CLIENTEColl[0].MUNICIPIO + "/" + LIS_CLIENTEColl[0].UF, 50);

                Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[4];
                p[0] = new Microsoft.Reporting.WinForms.ReportParameter("Cedente", _Cedente);
                p[1] = new Microsoft.Reporting.WinForms.ReportParameter("NomeCliente", _NomeCliente);
                p[2] = new Microsoft.Reporting.WinForms.ReportParameter("CPF_CNPJ", _CPF_CNPJ);
                p[3] = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoCliente", _EnderecoCliente);
                reportViewer1.LocalReport.SetParameters(p);

                this.LIS_DUPLICATARECEBERCollectionBindingSource.DataSource = LIS_DUPLICATARECEBERColl;


                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ´tecnico: " + ex.Message);
            }
        }
    }
}
