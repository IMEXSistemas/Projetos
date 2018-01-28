using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
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
    public partial class FrmRelatCotacaoCompra : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public int _IDCOTACAOCOMPRA = -1;
        public FrmRelatCotacaoCompra()
        {
            InitializeComponent();
        }

        private void FrmCotacaoCompra_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
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

                //Cotacao Compra
                LIS_COTACAOCOMPRACollection LIS_COTACAOCOMPRAColl = new LIS_COTACAOCOMPRACollection();
                LIS_COTACAOCOMPRAProvider LIS_COTACAOCOMPRAP = new LIS_COTACAOCOMPRAProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "=", _IDCOTACAOCOMPRA.ToString()));
                LIS_COTACAOCOMPRAColl = LIS_COTACAOCOMPRAP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_COTACAOCOMPRAColl.Count > 0)
                {
                    //Dados Fornecedor
                    LIS_FORNECEDORCollection LIS_FORNECEDORColl = new LIS_FORNECEDORCollection();
                    LIS_FORNECEDORProvider LIS_FORNECEDORP = new LIS_FORNECEDORProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", LIS_COTACAOCOMPRAColl[0].IDFORNECEDOR.ToString()));
                    LIS_FORNECEDORColl = LIS_FORNECEDORP.ReadCollectionByParameter(RowRelatorio);

                    FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                    FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                    FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(LIS_FORNECEDORColl[0].IDFORNECEDOR));

                    //Produto Compra Cotação
                    LIS_PRODUTOCOTACAOCollection LIS_PRODUTOCOTACAOColl = new LIS_PRODUTOCOTACAOCollection();
                    LIS_PRODUTOCOTACAOProvider LIS_PRODUTOCOTACAOP = new LIS_PRODUTOCOTACAOProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "=", _IDCOTACAOCOMPRA.ToString()));
                    LIS_PRODUTOCOTACAOColl = LIS_PRODUTOCOTACAOP.ReadCollectionByParameter(RowRelatorio);

                    this.LIS_PRODUTOCOTACAOCollectionBindingSource.DataSource = LIS_PRODUTOCOTACAOColl;
                    this.LIS_COTACAOCOMPRACollectionBindingSource.DataSource = LIS_COTACAOCOMPRAColl;
                    this.LIS_FORNECEDORCollectionBindingSource.DataSource = LIS_FORNECEDORColl;
                    this.EMPRESACollectionBindingSource.DataSource = EMPRESAColl;

                    //setando os parametro
                    Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[2];
                    p[0] = new Microsoft.Reporting.WinForms.ReportParameter("NumeroEndereco", FORNECEDORTy.NUMERO);
                    p[1] = new Microsoft.Reporting.WinForms.ReportParameter("NumeroCotacao", "Cotação: " + _IDCOTACAOCOMPRA.ToString().PadLeft(6, '0'));
                    reportViewer1.LocalReport.SetParameters(p);

                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.Percent;
                    this.reportViewer1.RefreshReport();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Não foi possível visualizar o relatório!");
                }

                

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
            
        }
    }
}
