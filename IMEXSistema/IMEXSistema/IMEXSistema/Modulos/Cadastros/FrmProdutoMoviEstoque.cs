using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.FrmSearch;
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
    public partial class FrmProdutoMoviEstoque : Form
    {
        Utility Util = new Utility();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        PRODUTOSCollection ProdutoColl = new PRODUTOSCollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();

        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
        public FrmProdutoMoviEstoque()
        {
            InitializeComponent();
        }

        private void FrmMovimentacaoEntrada_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
          
            GetDropProduto();
            GetDropFornecedor();
            GetDropGrupoCategoria();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void GetDropGrupoCategoria()
        {
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

        private void GetDropFornecedor()
        {
            try
            {
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
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

        private void GetDropProduto()
        {
            
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            ProdutoColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProduto.DisplayMember = "NOMEPRODUTO";
            cbProduto.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            ProdutoColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

            ProdutoColl.Sort(comparer.Comparer);
            cbProduto.DataSource = ProdutoColl;

            cbProduto.SelectedIndex = 0;
        }    

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();

                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                if (IDPRODUTO > 0)
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));

                 int IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);
                if (IDFORNECEDOR > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", IDFORNECEDOR.ToString()));

                int IDGRUPOCATEGORIA = Convert.ToInt32(cbGrupoCategoria.SelectedValue);
                if (IDGRUPOCATEGORIA > 0)
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", IDGRUPOCATEGORIA.ToString()));

                RowRelatorio.Add(new RowsFiltro("IDTIPOMOVIMENTACAO", "System.Int32", "=", "1"));//1 - Entrada
                RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePicker_Inicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "<=",Util.ConverStringDateSearch(dateTimePicker_Final.Text)));
                
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO DESC");

                PreencheGrid();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeral = 0;
        decimal TotalQuant = 0;
        private void PreencheGrid()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                TotalGeral = 0;
                TotalQuant = 0;

                DataGriewDados.Rows.Clear();
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                
                foreach (var LIS_MOVPRODUTOESTy in LIS_MOVPRODUTOESColl)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string DATA = Convert.ToDateTime(LIS_MOVPRODUTOESTy.DATAMOVIM).ToString("dd/MM/yyyy");
                    string NUMERODOC = LIS_MOVPRODUTOESTy.NDOCUMENTO;
                    string NOMEFORNECEDOR = FORNECEDORP.Read(Convert.ToInt32(LIS_MOVPRODUTOESTy.IDFORNECEDOR)).NOME;
                    string NOMEPRODUTO = LIS_MOVPRODUTOESTy.NOMEPRODUTO;
                    string IDPRODUTO = LIS_MOVPRODUTOESTy.IDPRODUTO.ToString();
                    decimal VLUNITARIO = Convert.ToDecimal(LIS_MOVPRODUTOESTy.VALORCUNITARIO);
                    decimal Total = Convert.ToDecimal(LIS_MOVPRODUTOESTy.VALORCUNITARIO) * Convert.ToDecimal(LIS_MOVPRODUTOESTy.QUANTIDADE);
                    decimal QUANTIDADE = Convert.ToDecimal(LIS_MOVPRODUTOESTy.QUANTIDADE);

                    row2.CreateCells(DataGriewDados, DATA, NUMERODOC, NOMEFORNECEDOR, NOMEPRODUTO, QUANTIDADE, VLUNITARIO, Total.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                    TotalGeral += Total;
                    TotalQuant += Convert.ToDecimal(LIS_MOVPRODUTOESTy.QUANTIDADE);

                    lblTotalRegistro.Text = "Total de Registro: " + LIS_MOVPRODUTOESColl.Count.ToString();
                }

                //Total Geral
                DataGridViewRow row3 = new DataGridViewRow();
                row3.CreateCells(DataGriewDados, string.Empty, string.Empty, string.Empty, string.Empty, TotalQuant, "Total Geral: ", TotalGeral.ToString("n2"));
                row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row3);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string MSGRelatorio = "Movimentação de Entrada ";          

            string RelatorioTitulo = InputBox("Movimentação de Entrada", ConfigSistema1.Default.NomeEmpresa, MSGRelatorio);

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

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbProduto.SelectedValue = result;
                }
            }

        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowindex = e.RowIndex;
                if (LIS_MOVPRODUTOESColl.Count > 0 && rowindex > -1)
                {
                    int ColumnSelecionada = e.ColumnIndex;
                    int CodSelect = Convert.ToInt32(LIS_MOVPRODUTOESColl[rowindex].IDESTOQUEES);

                    using (FrmEstoque frm = new FrmEstoque())
                    {
                        frm._IDESTOQUEES = CodSelect;
                        frm.ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa! Erro Técnico: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Movimentação de Entrada no Estoque");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Movimentação de Entrada no Estoque";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
    }
}
