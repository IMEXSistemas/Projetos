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
    public partial class RelatItensProdutoFesta : Form
    {
        public int IDITENSFESTAS = -1;

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public RelatItensProdutoFesta()
        {
            InitializeComponent();
        }

        private void RelatItensProdutoFesta_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.reportViewer1.RefreshReport();


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

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDITENSFESTAS", "System.Int32", "=", IDITENSFESTAS.ToString()));
            LIS_ITENSFESTASProvider LIS_ITENSFESTASP = new LIS_ITENSFESTASProvider();
            LIS_ITENSFESTASCollection LIS_ITENSFESTASColl = new LIS_ITENSFESTASCollection();
            LIS_ITENSFESTASColl = LIS_ITENSFESTASP.ReadCollectionByParameter(RowRelatorio);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDITENSFESTAS", "System.Int32", "=", IDITENSFESTAS.ToString()));
            LIS_PRODUTOSFESTASProvider LIS_PRODUTOSFESTASP = new LIS_PRODUTOSFESTASProvider();
            LIS_PRODUTOSFESTASCollection LIS_PRODUTOSFESTASColl = new LIS_PRODUTOSFESTASCollection();
            LIS_PRODUTOSFESTASColl = LIS_PRODUTOSFESTASP.ReadCollectionByParameter(RowRelatorio);

          
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            LIS_PRODUTOSFESTASCollectionBindingSource.DataSource = LIS_PRODUTOSFESTASColl;
            LIS_ITENSFESTASCollectionBindingSource.DataSource = LIS_ITENSFESTASColl;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.RefreshReport();

        }
    }
}
