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
    public partial class FrmRelatTicket : Form
    {
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

        public int IDPEDIDO = -1;
        public string msg1ticket =" ";
        public string msg2ticket = " ";
        public string msg3ticket = " ";
        public string msg4ticket = " ";

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelatTicket()
        {
            InitializeComponent();
        }

        private void FrmRelatTicket_Load(object sender, EventArgs e)
        {
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESACollection EMPRESAColl = new EMPRESACollection();
            EMPRESAColl = EMPRESAP.ReadCollectionByParameter(null);

            //Dados do Pedido
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);          

            msg1ticket = BmsSoftware.ConfigSistema1.Default.msg1ticket;
            msg2ticket = BmsSoftware.ConfigSistema1.Default.msg2ticket;
            msg3ticket = BmsSoftware.ConfigSistema1.Default.msg3ticket;
            msg4ticket = BmsSoftware.ConfigSistema1.Default.msg4ticket;

            //setando os parametro
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[6];
            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("NomeEmpresa", EMPRESAColl[0].NOMEFANTASIA);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("CNPJ", EMPRESAColl[0].CNPJCPF);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("msg1ticket", msg1ticket);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("msg2ticket", msg2ticket);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("msg3ticket", msg3ticket);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("msg4ticket", msg4ticket);
            this.reportViewer1.LocalReport.SetParameters(p);


            this.LIS_PEDIDOCollectionBindingSource.DataSource = LIS_PEDIDOColl;
            this.EMPRESAEntityBindingSource.DataSource = EMPRESAColl;
            this.LIS_PRODUTOSPEDIDOCollectionBindingSource.DataSource = LIS_PRODUTOSPEDIDOColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
