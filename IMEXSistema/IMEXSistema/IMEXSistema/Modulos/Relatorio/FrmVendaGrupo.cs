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
    public partial class FrmVendaGrupo : Form
    {
        public LIS_PEDIDOCollection LIS_PEDIDOFiltroPrintColl = new LIS_PEDIDOCollection();

        public FrmVendaGrupo()
        {
            InitializeComponent();
        }

        private void FrmVendaGrupo_Load(object sender, EventArgs e)
        {
            LIS_PEDIDOEntityBindingSource.DataSource = LIS_PEDIDOFiltroPrintColl;
            reportViewer1.PerformLayout();
            this.reportViewer1.RefreshReport();
        }
    }
}
