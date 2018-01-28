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
    public partial class FrmRelatVendaDiaria : Form
    {
        public LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        public LIS_PEDIDO2Collection LIS_PEDIDO2Coll = new LIS_PEDIDO2Collection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmRelatVendaDiaria()
        {
            InitializeComponent();
        }

        private void FrmRelatVendaDiaria_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

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

              this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
              this.LIS_PEDIDOCollectionBindingSource.DataSource = LIS_PEDIDOColl;
              this.LIS_PEDIDO2CollectionBindingSource.DataSource = LIS_PEDIDO2Coll;

              this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
              this.reportViewer1.ZoomMode = ZoomMode.Percent;
              this.reportViewer1.RefreshReport();
        
        }
    }
}
