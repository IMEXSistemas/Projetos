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
    public partial class FrmInventarioEstoque : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
        
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
      
        public FrmInventarioEstoque()
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

            btnConsultar.Image = Util.GetAddressImage(20);
            btnPrint2.Image = Util.GetAddressImage(19);
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
            string MSGRelatorio = "Inventário de Estoque ";

            if (Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)
                MSGRelatorio += " - Grupo Categoria: " + cbGrupoCategoria.Text;

            if (Convert.ToInt32(cbMarca.SelectedValue) > 0)
                MSGRelatorio += " - Marca: " + cbMarca.Text;

            string RelatorioTitulo = InputBox("Estoque Atual", ConfigSistema1.Default.NomeEmpresa, MSGRelatorio);

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
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                RowRelatorio.Clear();
                int IDGRUPOCATEGORIA = Convert.ToInt32(cbGrupoCategoria.SelectedValue);
                int IDMARCA = Convert.ToInt32(cbMarca.SelectedValue);
                if (IDGRUPOCATEGORIA > 0)
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", IDGRUPOCATEGORIA.ToString()));

                if (IDMARCA > 0)
                    RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", IDMARCA.ToString()));

                if (rbOrdemCodigo.Checked)
                    LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTO");
                else if (rbOrdemCodigoReferencia.Checked)
                     LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "CODPRODUTOFORNECEDOR");
                else
                     LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                
                this.Cursor = Cursors.Default;	

                PreencheGrid();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;	
               MessageBox.Show("Erro na pesquisa!");
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeral = 0;
        decimal TotalGeralVenda1 = 0;
        private void PreencheGrid()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

               

                TotalGeral = 0;
                TotalGeralVenda1 = 0;

                DataGriewDados.Rows.Clear();
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                int Contador = 0;
                foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string NOMEPRODUTO = LIS_PRODUTOSTy.NOMEPRODUTO;
                    string IDPRODUTO = LIS_PRODUTOSTy.IDPRODUTO.ToString();
                    string CODPRODUTOFORNECEDOR = LIS_PRODUTOSTy.CODPRODUTOFORNECEDOR;
                    
                    //Estoque
                    decimal ESTOQUEATUAL = 0;
                    if (MkDataInicial.Text == "  /  /")
                        ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO), chkFiscal.Checked);
                    else
                        ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO), MkDataInicial.Text, MkDataFinal.Text, chkFiscal.Checked);

                    string UND = LIS_PRODUTOSTy.NOMEUNIDADE;
                    
                    decimal VLUNITARIO = Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL);
                    if (rbCustoInicial.Checked)
                        VLUNITARIO = Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOINICIAL);

                    decimal Total = Convert.ToDecimal(ESTOQUEATUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL);
                    decimal VLVENDA1 = Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1);
                    decimal TotalVenda1 = Convert.ToDecimal(ESTOQUEATUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1);

                   

                    if (rbEstoqueNegativo.Checked && ESTOQUEATUAL < 0)
                    {
                        row2.CreateCells(DataGriewDados,  IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, UND, ESTOQUEATUAL.ToString(), VLUNITARIO.ToString("n4"), Total.ToString("n2"), VLVENDA1.ToString("n4"), TotalVenda1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                        TotalGeral += Total;
                        TotalGeralVenda1 += TotalVenda1;
                    }
                    else if (rbEstoqueZerado.Checked && ESTOQUEATUAL == 0)
                    {
                        row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, UND, ESTOQUEATUAL.ToString(), VLUNITARIO.ToString("n4"), Total.ToString("n2"), VLVENDA1.ToString("n4"), TotalVenda1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                        TotalGeral += Total;
                        TotalGeralVenda1 += TotalVenda1;
                    }
                    else if (rbEstoqueMaiorqueZero.Checked && ESTOQUEATUAL > 0)
                    {
                        row2.CreateCells(DataGriewDados,  IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, UND, ESTOQUEATUAL.ToString(), VLUNITARIO.ToString("n4"), Total.ToString("n2"), VLVENDA1.ToString("n4"), TotalVenda1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                        TotalGeral += Total;
                        TotalGeralVenda1 += TotalVenda1;
                    }
                    else if (rbTodos.Checked)
                    {
                        row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, UND, ESTOQUEATUAL.ToString(), VLUNITARIO.ToString("n4"), Total.ToString("n2"), VLVENDA1.ToString("n4"), TotalVenda1.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row2);
                        Contador++;
                        TotalGeral += Total;
                        TotalGeralVenda1 += TotalVenda1;
                    }

                    lblTotalPesquisa.Text = Contador.ToString();

                    Application.DoEvents();
                }

                
                //Total Geral
                DataGridViewRow row3 = new DataGridViewRow();
                row3.CreateCells(DataGriewDados,  string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Total Geral: ", TotalGeral.ToString("n2"), string.Empty, TotalGeralVenda1.ToString("n2"));
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

        private void button4_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Inventário de Estoque");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Inventário de Estoque";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
    }
}


