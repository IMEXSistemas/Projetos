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
    public partial class FrmProcessoHistorico : Form
    {
        public LIS_HISTORPROCESSOCollection LIS_HISTORPROCESSOColl = new LIS_HISTORPROCESSOCollection();
        public FrmProcessoHistorico()
        {
            InitializeComponent();
        }

        private void FrmProcessoHistorico_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            LIS_HISTORPROCESSOCollectionBindingSource.DataSource = LIS_HISTORPROCESSOColl;
            this.reportViewer1.RefreshReport();
        }
    }
}
