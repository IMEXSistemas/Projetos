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
    public partial class FrmProdutoGrupoCategoria : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();

        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmProdutoGrupoCategoria()
        {
            InitializeComponent();
        }

        private void FrmProdutoGrupoCategoria_Load(object sender, EventArgs e)
        {
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropGrupoCategoria();
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string NomegrupoCategoria = string.Empty;
                
                if (Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)
                    NomegrupoCategoria = cbGrupoCategoria.Text;

                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produto por Grupo/Categoria: " + NomegrupoCategoria);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                RowRelatorio.Clear();
                int idgrupocategoria = Convert.ToInt32(cbGrupoCategoria.SelectedValue);
                if (idgrupocategoria > 0)
                    RowRelatorio.Add(new RowsFiltro("idgrupocategoria", "System.Int32", "=", idgrupocategoria.ToString()));

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                //Remove ID  repetido  
                LIS_PRODUTOSCollection LIS_PRODUTOS2Coll = new LIS_PRODUTOSCollection();
                foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
                {
                    if (LIS_PRODUTOS2Coll.Find(delegate(LIS_PRODUTOSEntity item2)
                    {
                        return
                            (item2.IDGRUPOCATEGORIA == item.IDGRUPOCATEGORIA);
                    }) == null)
                    {
                        LIS_PRODUTOS2Coll.Add(item);
                    }
                }

                LIS_PRODUTOSColl.Clear();
                LIS_PRODUTOSColl = LIS_PRODUTOS2Coll;

                PreencheGrid();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();

            foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idgrupocategoria", "System.Int32", "=", LIS_PRODUTOSTy.IDGRUPOCATEGORIA.ToString()));

                LIS_PRODUTOSCollection LIS_PRODUTOSColl3 = new LIS_PRODUTOSCollection();
                LIS_PRODUTOSColl3 = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                //Titulo
                DataGridViewRow row1 = new DataGridViewRow();
                row1.CreateCells(DataGriewDados, "Grupo/Categoria: " + LIS_PRODUTOSTy.NOMEGRUPOCATEGORIA, string.Empty, string.Empty,
                                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row1);

                foreach (var LIS_PRODUTOS3Ty in LIS_PRODUTOSColl3)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string NOMEPRODUTO = LIS_PRODUTOS3Ty.NOMEPRODUTO;
                    string IDPRODUTO = LIS_PRODUTOS3Ty.IDPRODUTO.ToString();
                    string CODPRODUTOFORNECEDOR = LIS_PRODUTOS3Ty.CODPRODUTOFORNECEDOR;
                    string CODBARRA = LIS_PRODUTOS3Ty.CODBARRA;
                    string LOCALIZACAO = LIS_PRODUTOS3Ty.LOCALIZACAO;
                    string DATACADASTRO = Convert.ToDateTime(LIS_PRODUTOS3Ty.DATACADASTRO).ToString("dd/MM/yyyy");
                    string VALORVENDA1 = Convert.ToDecimal(LIS_PRODUTOS3Ty.VALORVENDA1).ToString("n2");
                    string ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOS3Ty.IDPRODUTO), chkFiscal.Checked).ToString();
                    string NOMEMARCA = LIS_PRODUTOS3Ty.NOMEMARCA;

                    row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR,
                                                    CODBARRA, LOCALIZACAO, DATACADASTRO, VALORVENDA1, ESTOQUEATUAL, NOMEMARCA);

                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                }


                lblTotalPesquisa.Text = (DataGriewDados.Rows.Count - 1).ToString();
            }

            this.Cursor = Cursors.Default;
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Produtos por Grupo/Categoria";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos por Grupo/Categoria");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

    }
}
