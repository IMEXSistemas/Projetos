using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;

namespace BmsSoftware.Modulos.Relatorio
{
    public partial class FrmGrupoCaixa : Form
    {
        public  LIS_CAIXA2Collection LIS_CAIXAFiltroPrintColl = new LIS_CAIXA2Collection();
        public FrmGrupoCaixa()
        {
            InitializeComponent();
        }

        private void FrmGrupoCaixa_Load(object sender, EventArgs e)
        {
            LIS_CAIXACollectionBindingSource.DataSource = LIS_CAIXAFiltroPrintColl;           
            this.reportViewer1.RefreshReport();
        }
    }
}
