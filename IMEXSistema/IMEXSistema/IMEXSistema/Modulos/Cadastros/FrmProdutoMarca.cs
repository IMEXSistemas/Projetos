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
    public partial class FrmProdutoMarca : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();

        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmProdutoMarca()
        {
            InitializeComponent();
        }

        private void FrmProdutoGrupoCategoria_Load(object sender, EventArgs e)
        {

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropMarca();
        }

        private void GetDropMarca()
        {
            MARCAProvider MARCAP = new MARCAProvider();
            MARCACollection MARCAColl = new MARCACollection();
            MARCAColl = MARCAP.ReadCollectionByParameter(null, "NOME");

            cbMarca.DisplayMember = "NOME";
            cbMarca.ValueMember = "IDMARCA";

            MARCAEntity MARCATy = new MARCAEntity();
            MARCATy.NOME = ConfigMessage.Default.MsgDrop;
            MARCATy.IDMARCA = -1;
            MARCAColl.Add(MARCATy);

            Phydeaux.Utilities.DynamicComparer<MARCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MARCAEntity>(cbMarca.DisplayMember);

            MARCAColl.Sort(comparer.Comparer);
            cbMarca.DataSource = MARCAColl;

            cbMarca.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string NomeMarca = string.Empty;
                
                if (Convert.ToInt32(cbMarca.SelectedValue) > 0)
                    NomeMarca = cbMarca.Text;

                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produto por Marca: " + NomeMarca);

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
            RowRelatorio.Clear();
            int IDMARCA = Convert.ToInt32(cbMarca.SelectedValue);
            if (IDMARCA > 0)
                RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", IDMARCA.ToString(), "NOMEPRODUTO"));

            LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

            //Remove ID  repetido  
            LIS_PRODUTOSCollection LIS_PRODUTOS2Coll = new LIS_PRODUTOSCollection();
            foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
            {
                if (LIS_PRODUTOS2Coll.Find(delegate(LIS_PRODUTOSEntity item2)
                {
                    return
                        (item2.IDMARCA == item.IDMARCA);
                }) == null)
                {
                    LIS_PRODUTOS2Coll.Add(item);
                }
            }
            
            LIS_PRODUTOSColl.Clear();
            LIS_PRODUTOSColl = LIS_PRODUTOS2Coll;

            PreencheGrid();
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
                RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", LIS_PRODUTOSTy.IDMARCA.ToString(), "NOMEPRODUTO"));

                LIS_PRODUTOSCollection LIS_PRODUTOSColl3 = new LIS_PRODUTOSCollection();
                LIS_PRODUTOSColl3 = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                
                //Titulo
                DataGridViewRow row1 = new DataGridViewRow();
                row1.CreateCells(DataGriewDados, "Marca: " + LIS_PRODUTOSTy.NOMEMARCA, string.Empty, string.Empty,
                                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

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
                    string VALORVENDA2 = Convert.ToDecimal(LIS_PRODUTOS3Ty.VALORVENDA2).ToString("n2");
                    string VALORVENDA3 = Convert.ToDecimal(LIS_PRODUTOS3Ty.VALORVENDA3).ToString("n2");
                    string ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOS3Ty.IDPRODUTO), chkFiscal.Checked).ToString();
                    string NOMEMARCA = LIS_PRODUTOS3Ty.NOMEMARCA;

                    row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR,
                                                    CODBARRA, LOCALIZACAO, DATACADASTRO, VALORVENDA1, ESTOQUEATUAL, VALORVENDA2, VALORVENDA3, NOMEMARCA);

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
                frm.TituloSelec = "Produtos por Marca";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos por Marca");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

    }
}
