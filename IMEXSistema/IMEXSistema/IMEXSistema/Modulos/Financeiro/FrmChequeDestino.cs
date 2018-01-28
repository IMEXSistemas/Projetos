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

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmChequeDestino : Form
    {
        DESTINOCHEQUECollection DESTINOCHEQUEColl = new DESTINOCHEQUECollection();
        DESTINOCHEQUEProvider DESTINOCHEQUEP = new DESTINOCHEQUEProvider();


        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
        public FrmChequeDestino()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Cheeque por Destino");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

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

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNomeClienteFornDestino.Text.TrimEnd().TrimStart() == string.Empty)
            {
                errorProvider1.SetError(txtNomeClienteFornDestino, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }            
            else
                errorProvider1.Clear();


            return result;
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("NOMEDESTINO", "System.String", "LIKE", txtNomeClienteFornDestino.Text));
                    
                    DESTINOCHEQUEColl = DESTINOCHEQUEP.ReadCollectionByParameter(RowRelatorio);
                    lblTotalRegistros.Text = DESTINOCHEQUEColl.Count.ToString();

                    if (DESTINOCHEQUEColl.Count > 0)
                        PreencheGrid();
                    else
                        MessageBox.Show("Não foi possível efetuar a pesquisa!");

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }
        decimal ValorTotal = 0;
        private void PreencheGrid()
        {
            try
            {
                ValorTotal = 0;

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DataGriewDados.Rows.Clear();

                foreach (var DESTINOCHEQUETy in DESTINOCHEQUEColl)
                {
                    //Busca Dados do Cheque
                    LIS_CHEQUECollection LIS_CHEQUEColl = new LIS_CHEQUECollection();
                    LIS_CHEQUEProvider LIS_CHEQUEP = new LIS_CHEQUEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCHEQUE", "System.Int32", "=", DESTINOCHEQUETy.IDCHEQUE.ToString()));
                    LIS_CHEQUEColl = LIS_CHEQUEP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_CHEQUEColl.Count > 0)
                    {
                        string Destino = txtNomeClienteFornDestino.Text;
                        string NUMERO = LIS_CHEQUEColl[0].NUMERO;
                        string AGENCIA = LIS_CHEQUEColl[0].AGENCIA;
                        string CONTA = LIS_CHEQUEColl[0].CONTA;
                        string DIGCONTA = LIS_CHEQUEColl[0].DIGCONTA;

                        string VALOR = Convert.ToDecimal(LIS_CHEQUEColl[0].VALOR).ToString("n2");
                        ValorTotal += Convert.ToDecimal(LIS_CHEQUEColl[0].VALOR);

                        string ENTRADA = Convert.ToDateTime(LIS_CHEQUEColl[0].ENTRADA).ToString("dd/MM/yyyy");
                        string BOMPARA = Convert.ToDateTime(LIS_CHEQUEColl[0].BOMPARA).ToString("dd/MM/yyyy");
                        string CENTROCUSTO = LIS_CHEQUEColl[0].CENTROCUSTO;
                        string DESCCENTROCUSTO = LIS_CHEQUEColl[0].DESCCENTROCUSTO;
                        string NOMESTATUS = LIS_CHEQUEColl[0].NOMESTATUS;
                        string NOMEFUNC = LIS_CHEQUEColl[0].NOMEFUNC;
                        string TIPORECEBIMENTO = LIS_CHEQUEColl[0].TIPORECEBIMENTO;
                        string NOMECLIENTEFORNEC = LIS_CHEQUEColl[0].NOMECLIENTEFORNEC;
                        string TITULAR = LIS_CHEQUEColl[0].TITULAR;


                        DataGridViewRow row2 = new DataGridViewRow();
                        row2.CreateCells(DataGriewDados, Destino, NUMERO, AGENCIA, CONTA, DIGCONTA, VALOR, ENTRADA, BOMPARA, CENTROCUSTO, DESCCENTROCUSTO, NOMESTATUS, NOMEFUNC, TIPORECEBIMENTO, NOMECLIENTEFORNEC, TITULAR);
                        row2.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row2);
                    }
                }


                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, string.Empty, string.Empty, string.Empty, string.Empty, "Total", ValorTotal.ToString("n2"), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                 this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void FrmChequeDestino_Load(object sender, EventArgs e)
        {
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnConsulta.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void rbClienteDestino_Click(object sender, EventArgs e)
        {
            using (FrmSearchCliente frm = new FrmSearchCliente())
            {
                txtNomeClienteFornDestino.Text = string.Empty;
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    txtNomeClienteFornDestino.Text = CLIENTEP.Read(Convert.ToInt32(result)).NOME;
                }
            }


            txtNomeClienteFornDestino.Enabled = false;
        }

        private void rbFornecDestino_Click(object sender, EventArgs e)
        {
            using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
            {
                txtNomeClienteFornDestino.Text = string.Empty;
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                    txtNomeClienteFornDestino.Text = FORNECEDORP.Read(Convert.ToInt32(result)).NOME;
                }
            }

            txtNomeClienteFornDestino.Enabled = false;
        }

        private void rbOutroDestino_CheckedChanged(object sender, EventArgs e)
        {
            txtNomeClienteFornDestino.Text = string.Empty;
            txtNomeClienteFornDestino.Enabled = true;
         
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Cheque Por Destino";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Cheque Por Destino");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
