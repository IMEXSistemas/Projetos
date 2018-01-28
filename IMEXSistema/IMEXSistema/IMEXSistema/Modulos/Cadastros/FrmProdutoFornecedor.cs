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

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmProdutoFornecedor : Form
    {
        Utility Util = new Utility();

        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        LIS_PRODUTOCOTACAOFORNECEDORCollection PRODUTOCOTACAOFORNECEDORColl = new LIS_PRODUTOCOTACAOFORNECEDORCollection();

        LIS_PRODUTOCOTACAOFORNECEDORProvider PRODUTOCOTACAOFORNECEDORP = new LIS_PRODUTOCOTACAOFORNECEDORProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmProdutoFornecedor()
        {
            InitializeComponent();
        }

        private void FrmProdutoFornecedor_Load(object sender, EventArgs e)
        {
            GetDropFornecedor();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void GetDropFornecedor()
        {
            try
            {
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

                cbFornecedor.DisplayMember = "NOME";
                cbFornecedor.ValueMember = "IDFORNECEDOR";

                FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
                FORNECEDORTy.IDFORNECEDOR = -1;
                FORNECEDORColl.Add(FORNECEDORTy);

                Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedor.DisplayMember);

                FORNECEDORColl.Sort(comparer.Comparer);
                cbFornecedor.DataSource = FORNECEDORColl;

                cbFornecedor.SelectedIndex = 0;
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();
                int idfornecedor = Convert.ToInt32(cbFornecedor.SelectedValue);
                if (idfornecedor > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", idfornecedor.ToString()));

                PRODUTOCOTACAOFORNECEDORColl = PRODUTOCOTACAOFORNECEDORP.ReadCollectionByParameter(RowRelatorio);

                //Remove ID  repetido  
                LIS_PRODUTOCOTACAOFORNECEDORCollection PRODUTOCOTACAOFORNECEDOR2Coll = new LIS_PRODUTOCOTACAOFORNECEDORCollection();
                foreach (LIS_PRODUTOCOTACAOFORNECEDOREntity item in PRODUTOCOTACAOFORNECEDORColl)
                {
                    if (PRODUTOCOTACAOFORNECEDOR2Coll.Find(delegate(LIS_PRODUTOCOTACAOFORNECEDOREntity item2)
                    {
                        return
                            (item2.IDFORNECEDOR == item.IDFORNECEDOR);
                    }) == null)
                    {
                        PRODUTOCOTACAOFORNECEDOR2Coll.Add(item);
                    }
                }

                PRODUTOCOTACAOFORNECEDORColl.Clear();
                PRODUTOCOTACAOFORNECEDORColl = PRODUTOCOTACAOFORNECEDOR2Coll;

                PreencheGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

      

        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                dataGridFornproduto.Rows.Clear();
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();

                foreach (var PRODUTOCOTACAOFORNECEDORTy in PRODUTOCOTACAOFORNECEDORColl)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string Telefone = FORNECEDORP.Read(Convert.ToInt32(PRODUTOCOTACAOFORNECEDORTy.IDFORNECEDOR)).TELEFONE1;
                    row2.CreateCells(dataGridFornproduto, PRODUTOCOTACAOFORNECEDORTy.NOMEFORNECEDOR, Telefone);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridFornproduto.Rows.Add(row2);

                    //Produtos do Fornecedor
                    LIS_PRODUTOCOTACAOFORNECEDORCollection PRODUTOCOTACAOFORNECEDOR2Coll = new LIS_PRODUTOCOTACAOFORNECEDORCollection();
                    PRODUTOCOTACAOFORNECEDOR2Coll = ProdutoFornecedor(Convert.ToInt32(PRODUTOCOTACAOFORNECEDORTy.IDFORNECEDOR));

                    if (PRODUTOCOTACAOFORNECEDOR2Coll.Count > 0)
                    {
                        DataGridViewRow row2_1 = new DataGridViewRow();
                        row2_1.CreateCells(dataGridFornproduto, "Nome Produto", "Valor Compra");
                        row2_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        dataGridFornproduto.Rows.Add(row2_1);
                    }

                    foreach (var PRODUTOCOTACAOFORNECEDOR2Ty in PRODUTOCOTACAOFORNECEDOR2Coll)
                    {
                        DataGridViewRow row2_2 = new DataGridViewRow();
                        row2_2.CreateCells(dataGridFornproduto, PRODUTOCOTACAOFORNECEDORTy.NOMEPRODUTO, Convert.ToDecimal(PRODUTOCOTACAOFORNECEDORTy.VALORCOMPRA).ToString("n2"));
                        row2_2.DefaultCellStyle.Font = new Font("Arial", 8);
                        dataGridFornproduto.Rows.Add(row2_2);
                    }

                    DataGridViewRow rowLinha_1 = new DataGridViewRow();
                    rowLinha_1.CreateCells(dataGridFornproduto, "__________________________________________________________________", "_________");
                    rowLinha_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridFornproduto.Rows.Add(rowLinha_1);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private LIS_PRODUTOCOTACAOFORNECEDORCollection ProdutoFornecedor(int idfornecedor)
        {
            LIS_PRODUTOCOTACAOFORNECEDORCollection PRODUTOCOTACAOFORNECEDORColl = new LIS_PRODUTOCOTACAOFORNECEDORCollection();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", idfornecedor.ToString()));

            PRODUTOCOTACAOFORNECEDORColl = PRODUTOCOTACAOFORNECEDORP.ReadCollectionByParameter(RowRelatorio);

            return PRODUTOCOTACAOFORNECEDORColl;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos por Fornecedor");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridFornproduto, RelatorioTitulo, this.Name, false);
            }
            catch (Exception ex)
            {
                
                  MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Produtos por Fornecedor";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = dataGridFornproduto;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(dataGridFornproduto, "csv");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos por Fornecedor");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dataGridFornproduto, RelatorioTitulo, this.Name);
        }

    }
}
