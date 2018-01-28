using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Cadastros;
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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmListaReserva : Form
    {

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl = new LIS_PRODUTORESERVACollection();
        LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP = new LIS_PRODUTORESERVAProvider();

        public FrmListaReserva()
        {
            InitializeComponent();
        }

        private void FrmListaReserva_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnCadProduto.Image = Util.GetAddressImage(6);
            GetDropProdutos();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Reservas");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosProduto, RelatorioTitulo, this.Name);
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

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();

                string DataInicial = Util.ConverStringDateSearch(dateTimePickerInicio.Text);
                string DataFinal = Util.ConverStringDateSearch(dateTimePickerFim.Text);

                if (rbDataRetirada.Checked)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DATARETIRADA", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATARETIRADA", "System.DateTime", "<=", DataFinal));
                }
                else if (rbDataEntrega.Checked)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", "<=", DataFinal));
                }

                if(Convert.ToInt32(cbProduto.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));

                LIS_PRODUTORESERVAColl = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTORESERVAColl.Count.ToString();

                PreencheGrid();

            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void PreencheGrid()
        {
            try
            {
                DGDadosProduto.Rows.Clear();

                foreach (var LIS_PRODUTORESERVATy in LIS_PRODUTORESERVAColl)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    string NomeCliente = RetornaNomeCliente(Convert.ToInt32(LIS_PRODUTORESERVATy.IDRESERVA));
                    string DATARETIRADA = Convert.ToDateTime(LIS_PRODUTORESERVATy.DATARETIRADA).ToString("dd/MM/yyyy");
                    string DATAENTREGA = Convert.ToDateTime(LIS_PRODUTORESERVATy.DATAENTREGA).ToString("dd/MM/yyyy");
                    row2.CreateCells(DGDadosProduto, LIS_PRODUTORESERVATy.IDRESERVA, DATARETIRADA, DATAENTREGA, NomeCliente, LIS_PRODUTORESERVATy.NOMEPRODUTO, LIS_PRODUTORESERVATy.QUANT, LIS_PRODUTORESERVATy.FLAGNOVARESERVA);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DGDadosProduto.Rows.Add(row2);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na consulta: " + ex.Message);
            }

        }

        private string RetornaNomeCliente(int IDRESERVA)
        {
            string result = string.Empty;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", IDRESERVA.ToString()));
            LIS_RESERVAProvider LIS_RESERVAP = new LIS_RESERVAProvider();
            LIS_RESERVACollection LIS_RESERVAColl = new LIS_RESERVACollection();
            LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_RESERVAColl.Count > 0)
                result = LIS_RESERVAColl[0].NOMECLIENTE;

            return result;
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();
                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutos()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProduto.DisplayMember = "NOMEPRODUTO";
            cbProduto.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

            PRODUTOSColl.Sort(comparer.Comparer);
            cbProduto.DataSource = PRODUTOSColl;

            cbProduto.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosProduto, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DGDadosProduto, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Lista da Pesquisa";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DGDadosProduto;
                frm.ShowDialog();
            }
        }      
    }
}
