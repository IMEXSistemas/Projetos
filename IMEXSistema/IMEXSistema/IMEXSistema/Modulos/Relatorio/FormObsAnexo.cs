using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Relatorio
{
    public partial class FormObsAnexo : Form
    {
       public string _ObservacoAnexo = " ";
        public string _Pedido = " ";
        public string _DataPedido = " ";
        public FormObsAnexo()
        {
            InitializeComponent();
        }

        private void FormObsAnexo_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

               Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[3];
                p[0] = new Microsoft.Reporting.WinForms.ReportParameter("Observacao", _ObservacoAnexo.Trim());
                p[1] = new Microsoft.Reporting.WinForms.ReportParameter("Pedido", _Pedido);
                p[2] = new Microsoft.Reporting.WinForms.ReportParameter("DataPedido", _DataPedido);
                this.reportViewer2.LocalReport.SetParameters(p);

                this.reportViewer2.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer2.ZoomMode = ZoomMode.Percent;
                this.reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
            this.reportViewer2.RefreshReport();
        }
    }
}
