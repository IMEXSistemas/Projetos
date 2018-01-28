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
    public partial class FrmPlanilhaEstoqueManual : Form
    {
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider Lis_PRODUTOSP = new LIS_PRODUTOSProvider();

        public FrmPlanilhaEstoqueManual()
        {
            InitializeComponent();
        }

        private void FrmPlanilhaEstoqueManual_Load(object sender, EventArgs e)
        {
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            GetDropGrupoCategoria();
            GetDropMarca();

        }

        private void GetDropMarca()
        {
            MARCAProvider MARCAP = new MARCAProvider();
            MARCACollection MarcaColl = new MARCACollection();
            MarcaColl = MARCAP.ReadCollectionByParameter(null, "NOME");

            cbMarca.DisplayMember = "NOME";
            cbMarca.ValueMember = "IDMARCA";

            MARCAEntity MARCATy = new MARCAEntity();
            MARCATy.NOME = ConfigMessage.Default.MsgDrop;
            MARCATy.IDMARCA = -1;
            MarcaColl.Add(MARCATy);

            Phydeaux.Utilities.DynamicComparer<MARCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MARCAEntity>(cbMarca.DisplayMember);

            MarcaColl.Sort(comparer.Comparer);
            cbMarca.DataSource = MarcaColl;

            cbMarca.SelectedIndex = 0;
        }

        private void GetDropGrupoCategoria()
        {
            GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
            GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
            GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null, "NOME");

            cbGrupoCategoria.DisplayMember = "NOME";
            cbGrupoCategoria.ValueMember = "IDGRUPOCATEGORIA";

            GRUPOCATEGORIAEntity GRUPOCATEGORIATy = new GRUPOCATEGORIAEntity();
            GRUPOCATEGORIATy.NOME = ConfigMessage.Default.MsgDrop;
            GRUPOCATEGORIATy.IDGRUPOCATEGORIA = -1;
            GRUPOCATEGORIAColl.Add(GRUPOCATEGORIATy);

            Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity>(cbGrupoCategoria.DisplayMember);

            GRUPOCATEGORIAColl.Sort(comparer.Comparer);
            cbGrupoCategoria.DataSource = GRUPOCATEGORIAColl;

            cbGrupoCategoria.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            btnPesquisa_Click(null, null);

            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Inventário do Estoque Manual");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
            btnPesquisa_Click(null, null);

            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            btnPesquisa_Click(null, null);

            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Inventário do Estoque Manual";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();

                if(Convert.ToInt32(cbMarca.SelectedValue) >0)
                    RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", cbMarca.SelectedValue.ToString()));

                if (Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", cbGrupoCategoria.SelectedValue.ToString()));

                LIS_PRODUTOSColl = Lis_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();

                PreencheGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheGrid()
        {
            try
            {
                decimal TotalGeralVenda1 = 0;
                decimal TotalGeralCompra1 = 0;

                DataGriewDados.Rows.Clear();

                foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                {
                    DataGridViewRow row1 = new DataGridViewRow();

                    Decimal TotalVenda1 = Convert.ToDecimal(LIS_PRODUTOSTy.ESTOQUEMANUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1);
                    Decimal TotalCompra = Convert.ToDecimal(LIS_PRODUTOSTy.ESTOQUEMANUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL);

                    row1.CreateCells(DataGriewDados, LIS_PRODUTOSTy.IDPRODUTO, LIS_PRODUTOSTy.NOMEPRODUTO, Convert.ToDecimal(LIS_PRODUTOSTy.ESTOQUEMANUAL).ToString(),
                                                     Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1).ToString("n2"), TotalVenda1.ToString("n2"), Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL).ToString("n2"), 
                                                     TotalCompra.ToString("n2"));
                    row1.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row1);

                    TotalGeralVenda1 += TotalVenda1;
                    TotalGeralCompra1 += TotalCompra;
                }

                //Rodape
                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, "", "", "", "Total :", TotalGeralVenda1.ToString("n2"), "Total :", TotalGeralCompra1.ToString("n2"));
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DataGriewDados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DataGriewDados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                        if (ValidacoesLibrary.ValidaTipoDecimal(DataGriewDados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            string ValueCell_ESTOQUEMANUAL = DataGriewDados.Rows[e.RowIndex].Cells[2].Value.ToString().TrimEnd().ToUpper();
                            string ValueCell_VALORVENDA1 = DataGriewDados.Rows[e.RowIndex].Cells[3].Value.ToString().TrimEnd().ToUpper();
                            string ValueCell_VALORCOMPRA1 = DataGriewDados.Rows[e.RowIndex].Cells[5].Value.ToString().TrimEnd().ToUpper();

                            //Salva Dados no produto
                            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                            int CodPRoduto = Convert.ToInt32(DataGriewDados.Rows[e.RowIndex].Cells[0].Value.ToString());
                            PRODUTOSTy = PRODUTOSP.Read(CodPRoduto);
                            if (PRODUTOSTy != null)
                            {
                                PRODUTOSTy.ESTOQUEMANUAL = Convert.ToDecimal(ValueCell_ESTOQUEMANUAL);
                                PRODUTOSTy.VALORVENDA1 = Convert.ToDecimal(ValueCell_VALORVENDA1);
                                PRODUTOSTy.VALORCUSTOFINAL = Convert.ToDecimal(ValueCell_VALORCOMPRA1);
                                PRODUTOSP.Save(PRODUTOSTy);

                                //Soma Total Venda1/Compra1
                                Decimal TotalVenda1 = Convert.ToDecimal(PRODUTOSTy.ESTOQUEMANUAL * PRODUTOSTy.VALORVENDA1);
                                Decimal TotalCompra1 = Convert.ToDecimal(PRODUTOSTy.ESTOQUEMANUAL * PRODUTOSTy.VALORCUSTOFINAL);
                                DataGriewDados.Rows[e.RowIndex].Cells[4].Value = TotalVenda1.ToString("n2");
                                DataGriewDados.Rows[e.RowIndex].Cells[6].Value = TotalCompra1.ToString("n2");
                            }
                        }
                        else
                        {
                            DataGriewDados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                            MessageBox.Show("Valor inválido!!");
                        }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
           
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            btnPesquisa_Click(null, null);

            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Inventário do Estoque Manual");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
