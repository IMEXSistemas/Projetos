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

namespace BmsSoftware.Modulos.Lote
{
    public partial class FrmSaldoLotePositivo : Form
    {
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();
        LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl = new LIS_ESTOQUELOTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmSaldoLotePositivo()
        {
            InitializeComponent();
        }

        private void FrmSaldoLotePositivo_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Lotes com Saldo Positivo";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lotes com Saldo Positivo");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lotes com Saldo Positivo");

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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            PesquisasLotes();
        }

        private void PesquisasLotes()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                //Pesquisa Todos os Estoques de Lote
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGTIPO", "System.String", "=","E"));
                RowRelatorio.Add(new RowsFiltro("FLAGATIVO", "System.String", "=", "S"));
                
                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE");

                //Limpa Grid 
                DataGriewDados.Rows.Clear();
                foreach (var item in LIS_ESTOQUELOTEColl)
                {
                    Decimal SaldoEstoqueLote = SaldoProduto(item.CODLOTE, Convert.ToInt32(item.IDPRODUTO));

                    if (SaldoEstoqueLote > 0)
                        PreencherGrid(item.CODLOTE, item.IDPRODUTO, SaldoEstoqueLote);

                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private decimal SaldoProduto(string CodLote, int CodProduto)
        {
            decimal result = 0;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", CodProduto.ToString()));
                RowRelatorio.Add(new RowsFiltro("FLAGATIVO", "System.String", "=", "S"));

                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                decimal Entrada = 0;
                decimal Saida = 0;
                decimal Saldo = 0;
                foreach (var item in LIS_ESTOQUELOTEColl)
                {
                    if (item.FLAGATIVO == "S" && item.FLAGTIPO == "E")
                        Entrada += Convert.ToDecimal(item.QUANTIDADE);
                    else if (item.FLAGATIVO == "S" && item.FLAGTIPO == "S")
                        Saida += Convert.ToDecimal(item.QUANTIDADE);
                }

                Saldo = Entrada - Saida;
                result = Saldo;
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void PreencherGrid(string CodLote, int? IdProduto,  Decimal SaldoLote)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote.ToString()));
                RowRelatorio.Add(new RowsFiltro("IdProduto", "System.Int32", "=", IdProduto.ToString()));
                RowRelatorio.Add(new RowsFiltro("FLAGTIPO", "System.String", "=", "E"));
                RowRelatorio.Add(new RowsFiltro("FLAGATIVO", "System.String", "=", "S"));
                
                LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl2 = new LIS_ESTOQUELOTECollection();
                LIS_ESTOQUELOTEColl2 = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE");

                foreach (var item in LIS_ESTOQUELOTEColl2)
                {
                    string _CodLote = item.CODLOTE;
                    string _ValidadeLote = Convert.ToDateTime(item.DATAVALIDADE).ToString("dd/MM/yyyy");
                    string _NomeProduto = item.NOMEPRODUTO;
                    string _CodProduto = item.IDPRODUTO.ToString();

                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, _CodLote, _ValidadeLote, SaldoLote.ToString("n2") , _NomeProduto, _CodProduto);
                    row1.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row1);
                }

           }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
