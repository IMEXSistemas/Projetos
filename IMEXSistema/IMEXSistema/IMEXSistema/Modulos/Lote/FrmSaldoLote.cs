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

namespace BmsSoftware.Modulos.Lote
{
    public partial class FrmSaldoLote : Form
    {
        LOTECollection LOTEColl = new LOTECollection();
        LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl = new LIS_ESTOQUELOTECollection();
        PRODUTOSCollection ProdutoColl = new PRODUTOSCollection();

        LOTEProvider LOTEP = new LOTEProvider();
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();        

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public string _CodLote = string.Empty;

        public FrmSaldoLote()
        {
            InitializeComponent();
        }

        private void FrmSaldoLote_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            btnLote.Image = Util.GetAddressImage(6);

            if(_CodLote != string.Empty)
            {
                txtNumeroLote.Text = _CodLote;
                GetProdutoLote();
            }
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }

        private void txtNumeroLote_Leave(object sender, EventArgs e)
        {
            LoteExiste(txtNumeroLote.Text);
        }

        private Boolean LoteExiste(string CodLote)
        {
            Boolean Result = false;

            try
            {
                if (CodLote.Trim() != string.Empty)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote));
                    LOTEColl.Clear();
                    LOTEColl = LOTEP.ReadCollectionByParameter(RowRelatorio);

                    if (LOTEColl.Count > 0)
                    {
                        GetProdutoLote();
                        Result = true;
                    }
                    else
                    {
                        MessageBox.Show("Lote: " + CodLote + " Não Existe!");
                        txtNumeroLote.Focus();
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return Result;
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Saldo por Lote";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Saldo Por Lote");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Saldo Por Lote");

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

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
           
        }

        private void GetProdutoLote()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", txtNumeroLote.Text));

                LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl_2 = new LIS_ESTOQUELOTECollection();
                LIS_ESTOQUELOTEColl_2 = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATA");

                ProdutoColl.Clear();
                foreach (var item in LIS_ESTOQUELOTEColl_2)
                {
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy.IDPRODUTO = Convert.ToInt32(item.IDPRODUTO);
                    PRODUTOSTy.NOMEPRODUTO = item.NOMEPRODUTO;
                    ProdutoColl.Add(PRODUTOSTy);
                }

                //Remove Produto Repetido
                PRODUTOSCollection PRODUTOSColl_2 = new PRODUTOSCollection();
                foreach (PRODUTOSEntity item in ProdutoColl)
                {
                    if (PRODUTOSColl_2.Find(delegate (PRODUTOSEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                    {
                        PRODUTOSColl_2.Add(item);
                    }
                }
                ProdutoColl = PRODUTOSColl_2;
                GetDropProduto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropProduto()
        {
            try
            {             
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if(Validacoes())
                Pesquisa();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNumeroLote.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            {
                errorProvider1.SetError(label14, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }            
            else
                errorProvider1.Clear();

            return result;
        }

        private void Pesquisa()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", txtNumeroLote.Text));
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
                RowRelatorio.Add(new RowsFiltro("FLAGATIVO", "System.String", "=", "S"));

                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATA");

                //Busca Saldo 
                Decimal SaldoLote = SaldoProduto();                
                LIS_ESTOQUELOTEEntity LIS_ESTOQUELOTETy = new LIS_ESTOQUELOTEEntity();
                LIS_ESTOQUELOTETy.CODLOTE = "Saldo: ";
                LIS_ESTOQUELOTETy.QUANTIDADE = SaldoLote;
                LIS_ESTOQUELOTEColl.Add(LIS_ESTOQUELOTETy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ESTOQUELOTEColl;

                lblTotalPesquisa.Text = LIS_ESTOQUELOTEColl.Count.ToString();

                PaintGrid();

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

        private decimal SaldoProduto()
        {
            decimal result = 0;
            try
            {
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

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_ESTOQUELOTEEntity item in LIS_ESTOQUELOTEColl)
                {
                    if (item.FLAGTIPO != null && item.FLAGTIPO.Trim() == "S")
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    else if (item.FLAGTIPO != null && item.FLAGTIPO.Trim() == "E")
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

    }
}
