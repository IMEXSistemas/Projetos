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
    public partial class FrmPolifix2060 : Form
    {

       public PRODUTOSCollection PRODUTOS_Etiqueta = new PRODUTOSCollection();

       public int Left_Esquerda = 150;
       public int Right_Direito = 100;
       public int Top_Topo = 130;
       public int Bottom_Inferior = 150;

        public FrmPolifix2060()
        {
            InitializeComponent();
        }

        private void FrmPolifix2060_Load(object sender, EventArgs e)
        {
            this.PRODUTOSCollectionBindingSource.DataSource = PRODUTOS_Etiqueta;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
