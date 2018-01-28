using BmsSoftware.Classes.BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VVX;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmProdutosDuplicata : Form
    {
        Utility Util = new Utility();

        public LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
        LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
        LIS_ITPECASFECHOSCollection LIS_ITPECASFECHOSColl = new LIS_ITPECASFECHOSCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();

        public FrmProdutosDuplicata()
        {
            InitializeComponent();
        }

        private void FrmProdutosDuplicata_Load(object sender, EventArgs e)
        {
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

           PreencheDados();
        }
  

        private void PreencheDados()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                DataGridRelaDupl.Rows.Clear();//Limpa dados do Grid

                foreach (var item in LIS_DUPLICATARECEBERColl)
                {
                    if(item.IDDUPLICATARECEBER != null)
                    {
                        DataGridViewRow rowC = new DataGridViewRow();//Cabeçalho   
                        rowC.CreateCells(DataGridRelaDupl, item.NUMERO, Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2"), Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyy"), item.NOMECLIENTE, " ", "", "");
                        rowC.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGridRelaDupl.Rows.Add(rowC);
                        
                        //Limpa Collecions
                        LIS_PRODUTOSPEDIDOColl.Clear();
                        LIS_PRODUTOSPEDIDOMTQColl.Clear();
                        LIS_PRODUTONFEColl.Clear();
                        LIS_ITPECASFECHOSColl.Clear();

                        //Dados do Produto
                        ListaProdutos(item.NOTAFISCAL);

                        DataGridViewRow rowCP = new DataGridViewRow();//Cabeçalho   
                        rowCP.CreateCells(DataGridRelaDupl, "", "", "", "PRODUTOS", "Quant.", "Vl.Unitário", "Vl Total");
                        rowCP.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGridRelaDupl.Rows.Add(rowCP);

                        //Dados de Pedido
                        foreach (var itemP in LIS_PRODUTOSPEDIDOColl)
                        {
                            DataGridViewRow rowP = new DataGridViewRow();//Cabeçalho   
                            rowP.CreateCells(DataGridRelaDupl, "", "", "", itemP.NOMEPRODUTO, Convert.ToDecimal(itemP.QUANTIDADE).ToString("n2"), Convert.ToDecimal(itemP.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(itemP.VALORTOTAL).ToString("n2"));
                            rowP.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGridRelaDupl.Rows.Add(rowP);
                        }

                        //Dados de Pedodo MT
                        foreach (var itemP2 in LIS_PRODUTOSPEDIDOMTQColl)
                        {
                            DataGridViewRow rowP2 = new DataGridViewRow();//Cabeçalho   
                            rowP2.CreateCells(DataGridRelaDupl, "", "", "", itemP2.NOMEPRODUTO, Convert.ToDecimal(itemP2.QUANTIDADE).ToString("n2"), Convert.ToDecimal(itemP2.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(itemP2.VALORTOTAL).ToString("n2"));
                            rowP2.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGridRelaDupl.Rows.Add(rowP2);
                        }

                        //Dados Nota Fiscal
                        foreach (var itemNF in LIS_PRODUTONFEColl)
                        {
                            DataGridViewRow rowNF = new DataGridViewRow();//Cabeçalho   
                            rowNF.CreateCells(DataGridRelaDupl, "", "", "", itemNF.NOMEPRODUTO, Convert.ToDecimal(itemNF.QUANTIDADE).ToString("n2"), Convert.ToDecimal(itemNF.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(itemNF.VALORTOTAL).ToString("n2"));
                            rowNF.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGridRelaDupl.Rows.Add(rowNF);
                        }

                         //Dados OS
                        foreach (var itemOS in LIS_ITPECASFECHOSColl)
                        {
                            DataGridViewRow rowOS = new DataGridViewRow();//Cabeçalho   
                            rowOS.CreateCells(DataGridRelaDupl, "", "", "", itemOS.NOMEPRODUTO, Convert.ToDecimal(itemOS.QUANTIDADE).ToString("n2"), Convert.ToDecimal(itemOS.VALORUNITARIO).ToString("n2"), Convert.ToDecimal(itemOS.VALORTOTAL).ToString("n2"));
                            rowOS.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGridRelaDupl.Rows.Add(rowOS);
                        }

                        //RodaPE
                        DataGridViewRow rowRP = new DataGridViewRow();//Cabeçalho   
                        rowRP.CreateCells(DataGridRelaDupl, "---------", "-----------", "------------", "----------------------------------", "-------", "--------", "--------", "--------");
                        rowRP.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGridRelaDupl.Rows.Add(rowRP);
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: "+ ex.Message);
            }
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

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.String", "=", NOTAFISCAL.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                //Produto MT2 Linear                
                LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaProdutosNF(string NOTAFISCAL)
        {
            try
            {
                
                LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();

                RowsFiltroCollection RowNF = new RowsFiltroCollection();
                RowNF.Add(new RowsFiltro("NFISCALE", "System.String", "=", Util.RetiraLetras(NOTAFISCAL).ToString()));
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowNF);

                int IDNOTAFISCAL = -1;
                if (LIS_NOTAFISCALEColl.Count > 0)
                    IDNOTAFISCAL = Convert.ToInt32(LIS_NOTAFISCALEColl[0].IDNOTAFISCALE);

                
                LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.String", "=", IDNOTAFISCAL.ToString()));

                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);
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
                

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.String", "=", NOTAFISCAL.ToString()));

                LIS_ITPECASFECHOSColl = LIS_ITPECASFECHOSP.ReadCollectionByParameter(RowRelatorio);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos Por Duplicata");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGridRelaDupl, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Produtos Por Duplicata";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGridRelaDupl;
                frm.ShowDialog();
            }
        }
    }
}
