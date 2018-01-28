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
    public partial class FrmEstoqueChapa : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
        
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmEstoqueChapa()
        {
            InitializeComponent();
        }

        private void FrmRelacaoEstoqueAtual_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        
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
                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                int Contador = 0;
                foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                {
                   
                    DataGridViewRow row2 = new DataGridViewRow();
                    string NOMEPRODUTO = LIS_PRODUTOSTy.NOMEPRODUTO;
                    string IDPRODUTO = LIS_PRODUTOSTy.IDPRODUTO.ToString();
                    string CODPRODUTOFORNECEDOR = LIS_PRODUTOSTy.CODPRODUTOFORNECEDOR;
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO));
                    
                    //Estoque
                    decimal ESTOQUEATUAL = 0;
                    if (mkstUltimaMovimentacao.Text == "  /  /")
                        ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO), false);
                    else
                        ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO), mkstUltimaMovimentacao.Text, false);
                    
                    decimal VLUNITARIO = Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL);                  

                    decimal Total = Convert.ToDecimal(ESTOQUEATUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORCUSTOFINAL);
                    decimal VLVENDA1 = Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1);
                    decimal TotalVenda1 = Convert.ToDecimal(ESTOQUEATUAL) * Convert.ToDecimal(LIS_PRODUTOSTy.VALORVENDA1);
                    decimal AlturaChapa = Convert.ToDecimal(PRODUTOSTy.ALTURACHAPA);
                    decimal LarguraChapa = Convert.ToDecimal(PRODUTOSTy.LARGURACHAPA);
                    decimal TotalChapa = AlturaChapa * LarguraChapa;
                   
                    decimal TotalEstoqueChapa = 0;
                    if (TotalChapa > 0)
                        TotalEstoqueChapa = ESTOQUEATUAL / TotalChapa;                   

                    if (rbEstoqueNegativo.Checked && ESTOQUEATUAL < 0)
                    {
                       
                            row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, AlturaChapa, LarguraChapa, ESTOQUEATUAL.ToString("n4"), TotalEstoqueChapa);
                            row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row2);
                            Contador++;
                            TotalGeral += Total;
                            TotalGeralVenda1 += TotalVenda1;
                        
                    }
                    else if (rbEstoqueZerado.Checked && ESTOQUEATUAL == 0)
                    {
                      
                            row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, AlturaChapa, LarguraChapa, ESTOQUEATUAL.ToString("n4"), TotalEstoqueChapa);
                            row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row2);
                            Contador++;
                            TotalGeral += Total;
                            TotalGeralVenda1 += TotalVenda1;
                        
                    }
                    else if (rbEstoqueMaiorqueZero.Checked && ESTOQUEATUAL > 0)
                    {
                            row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, AlturaChapa, LarguraChapa, ESTOQUEATUAL.ToString("n4"), TotalEstoqueChapa);
                            row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row2);
                            Contador++;
                            TotalGeral += Total;
                            TotalGeralVenda1 += TotalVenda1;
                        
                    }
                    else if (rbTodos.Checked)
                    {
                       
                            row2.CreateCells(DataGriewDados, IDPRODUTO, CODPRODUTOFORNECEDOR, NOMEPRODUTO, AlturaChapa, LarguraChapa, ESTOQUEATUAL.ToString("n4"), TotalEstoqueChapa);
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
                row3.CreateCells(DataGriewDados,  string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
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
    }
}


