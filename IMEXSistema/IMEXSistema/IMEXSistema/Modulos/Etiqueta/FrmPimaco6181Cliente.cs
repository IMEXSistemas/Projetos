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

namespace BmsSoftware.Modulos.Etiqueta
{
    public partial class FrmPimaco6181Cliente : Form
    {

       public LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();

       public int Left_Esquerda = 150;
       public int Right_Direito = 100;
       public int Top_Topo = 130;
       public int Bottom_Inferior = 150;

       public FrmPimaco6181Cliente()
        {
            InitializeComponent();
        }

        private void FrmPolifix2060_Load(object sender, EventArgs e)
        {
            this.LIS_CLIENTECollectionBindingSource.DataSource = LIS_CLIENTEColl;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }
    }
}
