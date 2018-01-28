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
    public partial class FrmRelacaoEstoqueAtual : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
        
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
        public FrmRelacaoEstoqueAtual()
        {
            InitializeComponent();
        }

        private void FrmRelacaoEstoqueAtual_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnPrint.Image = Util.GetAddressImage(19);
            btnClose.Image = Util.GetAddressImage(21);

            GetDropGrupoCategoria();
            GetDropMarca();
        }

        private void GetDropMarca()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropGrupoCategoria()
        {
            try
            {
                GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
                GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Estoque Atual", ConfigSistema1.Default.NomeEmpresa, "Relação de Estoque Atual");

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();
                int IDGRUPOCATEGORIA = Convert.ToInt32(cbGrupoCategoria.SelectedValue);
                int IDMARCA = Convert.ToInt32(cbMarca.SelectedValue);
                if (IDGRUPOCATEGORIA > 0)
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", IDGRUPOCATEGORIA.ToString()));

                if (IDMARCA > 0)
                    RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", IDMARCA.ToString()));

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

               

                PreencheGrid();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Erro na pesquisa!");
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheGrid()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DataGriewDados.Rows.Clear();
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                int Contador = 0;
                foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string NOMEPRODUTO = LIS_PRODUTOSTy.NOMEPRODUTO;
                    string IDPRODUTO = LIS_PRODUTOSTy.IDPRODUTO.ToString();
                    string CODPRODUTOFORNECEDOR = LIS_PRODUTOSTy.CODPRODUTOFORNECEDOR;
                    decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO), chkFiscal.Checked);
                    decimal PRECOVENDA1 = Convert.ToInt32(LIS_PRODUTOSTy.VALORVENDA1);

                    if (rbEstoqueNegativo.Checked && ESTOQUEATUAL < 0)
                    {
                        row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR, ESTOQUEATUAL.ToString(), PRECOVENDA1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                    }
                    else if (rbEstoqueZerado.Checked && ESTOQUEATUAL == 0)
                    {
                        row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR, ESTOQUEATUAL.ToString(), PRECOVENDA1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                    }
                    else if (rbEstoqueMaiorqueZero.Checked && ESTOQUEATUAL > 0)
                    {
                        row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR, ESTOQUEATUAL.ToString(), PRECOVENDA1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                    }
                    else if (rbTodos.Checked)
                    {
                        row2.CreateCells(DataGriewDados, NOMEPRODUTO, IDPRODUTO, CODPRODUTOFORNECEDOR, ESTOQUEATUAL.ToString(), PRECOVENDA1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                    }

                    lblTotalPesquisa.Text = Contador.ToString();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Estoque Atual";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Estoque Atual");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
