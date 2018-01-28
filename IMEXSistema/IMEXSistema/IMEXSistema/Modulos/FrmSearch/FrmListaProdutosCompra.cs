using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSworks.UI;
using BmsSoftware.Classes.BMSworks.UI;
using VVX;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmListaProdutosCompra : Form
    {
        Utility Util = new Utility();
        public string NFSelec = "-1";
        public FrmListaProdutosCompra()
        {
            InitializeComponent();
        }

        private void FrmListaProdutosCompra_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            if (NFSelec != "-1")
                ListaProdutos(NFSelec);
        }

        private void ListaProdutos(string NOTAFISCAL)
        {
            //retira a Sigla da venda - PD/OS e NF
            if (NOTAFISCAL != string.Empty)
            {
                string sigla = NOTAFISCAL.Substring(0, 2);
                string NF = NOTAFISCAL.Substring(2, NOTAFISCAL.Length - 2);

                switch (sigla)
                {
                    case ("PD"):
                        ListaProdutosPD(NF);
                        break;
                    case ("NF"):
                        ListaProdutosNF(NF);
                        break;
                    case ("OS"):
                        ListaProdutosOS(NF);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ListaProdutosPD(string NOTAFISCAL)
        {
            try
            {
                LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.String", "=", NOTAFISCAL.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.Rows.Clear();
                foreach (var item in LIS_PRODUTOSPEDIDOColl)
                {
                    DataGridViewRow rowTop = new DataGridViewRow();
                    rowTop.CreateCells(DGDadosProduto, item.NOMEPRODUTO, Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"));
                    rowTop.DefaultCellStyle.Font = new Font("Arial", 8);
                    DGDadosProduto.Rows.Add(rowTop);
                }

                //Produto MT2 Linear 
                LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                foreach (var item in LIS_PRODUTOSPEDIDOMTQColl)
                {
                    DataGridViewRow rowTop = new DataGridViewRow();
                    rowTop.CreateCells(DGDadosProduto, item.NOMEPRODUTO,  Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"));
                    rowTop.DefaultCellStyle.Font = new Font("Arial", 8);
                    DGDadosProduto.Rows.Add(rowTop);
                }

                lblQuantProdutos.Text = "Quantidade de Produtos: " + (LIS_PRODUTOSPEDIDOMTQColl.Count + LIS_PRODUTOSPEDIDOColl.Count).ToString();

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPesquisa,
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }

        private void ListaProdutosNF(string NOTAFISCAL)
        {
            try
            {
                LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
                LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();
               
                RowsFiltroCollection RowNF = new RowsFiltroCollection();
                RowNF.Add(new RowsFiltro("NFISCALE", "System.String", "=", Util.RetiraLetras(NOTAFISCAL).ToString()));
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowNF);                

                int IDNOTAFISCAL = -1;
                if (LIS_NOTAFISCALEColl.Count > 0)
                    IDNOTAFISCAL = Convert.ToInt32(LIS_NOTAFISCALEColl[0].IDNOTAFISCALE);

                LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.String", "=", IDNOTAFISCAL.ToString()));

                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTONFEColl;
                lblQuantProdutos.Text = "Quantidade de Produtos: " + LIS_PRODUTONFEColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaProdutosOS(string NOTAFISCAL)
        {
            try
            {
                LIS_ITPECASFECHOSProvider LIS_ITPECASFECHOSP = new LIS_ITPECASFECHOSProvider();
                LIS_ITPECASFECHOSCollection LIS_ITPECASFECHOSColl = new LIS_ITPECASFECHOSCollection();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.String", "=", NOTAFISCAL.ToString()));

                LIS_ITPECASFECHOSColl = LIS_ITPECASFECHOSP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_ITPECASFECHOSColl;
                lblQuantProdutos.Text = "Quantidade de Produtos: " + LIS_ITPECASFECHOSColl.Count.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPesquisa,
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }

        private void FrmListaProdutosCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Lista de Produtos";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DGDadosProduto;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DGDadosProduto, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Produtos");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosProduto, RelatorioTitulo, this.Name);
        }

        public static string InputBox(string prompt, string title, string defaultValue)
        {
            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
