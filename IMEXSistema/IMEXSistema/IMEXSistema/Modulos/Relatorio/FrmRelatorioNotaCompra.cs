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
    public partial class FrmRelatorioNotaCompra : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public LIS_ESTOQUEESCollection LIS_ESTOQUEESColl = new LIS_ESTOQUEESCollection();
        LIS_FORNECEDORCollection LIS_FORNECEDORColl = new LIS_FORNECEDORCollection();

        LIS_FORNECEDORProvider LIS_FORNECEDORP = new LIS_FORNECEDORProvider();

        public FrmRelatorioNotaCompra()
        {
            InitializeComponent();
        }

        private void FrmRelatorioNotaCompra_Load(object sender, EventArgs e)
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

            //Fornecedor
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", LIS_ESTOQUEESColl[0].IDFORNECEDOR.ToString()));
            LIS_FORNECEDORColl =  LIS_FORNECEDORP.ReadCollectionByParameter(RowRelatorio);

            //Produtos Movimentação
            LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
            LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDESTOQUEES", "System.Int32", "=", LIS_ESTOQUEESColl[0].IDESTOQUEES.ToString()));
            LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);
            

            this.LIS_FORNECEDORCollectionBindingSource.DataSource = LIS_FORNECEDORColl;
            this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;
            this.LIS_ESTOQUEESCollectionBindingSource.DataSource = LIS_ESTOQUEESColl;
            this.LIS_MOVPRODUTOESCollectionBindingSource.DataSource = LIS_MOVPRODUTOESColl;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;

            this.reportViewer1.RefreshReport();
        }
    }
}
