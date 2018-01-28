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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmExcluirItensPedido : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public int _IDPEDIDO = -1;

        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        public FrmExcluirItensPedido()
        {
            InitializeComponent();
        }

        private void FrmExcluirItensPedido_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                btnSair.Image = Util.GetAddressImage(21);

                ListaProdutoPedido(_IDPEDIDO);
                GetDropProdutos();

                chkPesqCodBarra.Checked = false;
                chkCodRef.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.TipoPesquisaProduto == "1")//Código de Referencia
                    chkCodRef.Checked = true;
                else if (BmsSoftware.ConfigSistema1.Default.TipoPesquisaProduto == "2")//Código de Barra
                    chkPesqCodBarra.Checked = true;

                cbProduto.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropProdutos()
        {
            try
            {
                LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTOCOD");

                cbProduto.DisplayMember = "NOMEPRODUTOCOD";
                cbProduto.ValueMember = "IDPRODUTO";

                LIS_PRODUTOSEntity PRODUTOSTy = new LIS_PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTOCOD = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                LIS_PRODUTOSColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>(cbProduto.DisplayMember);

                LIS_PRODUTOSColl.Sort(comparer.Comparer);
                cbProduto.DataSource = LIS_PRODUTOSColl;

                cbProduto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaProdutoPedido(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

                SumTotalProdutosPedido();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro " + ex.Message);
            }
          
        }

        private void SumTotalProdutosPedido()
        {
            try
            {
                decimal total = 0;
                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    total += Convert.ToDecimal(item.VALORTOTAL);
                }

                txtTotalProduto.Text = total.ToString("n2");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void DGDadosProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Excluir
                {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO);
                                PRODUTOSPEDIDOP.Delete(CodSelect);
                                ListaProdutoPedido(_IDPEDIDO);
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                            catch (Exception ex)
                            {
                                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                                MessageBox.Show("Erro técnico: " + ex.Message);

                            }
                        }

                  
                }
            }
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkPesqCodBarra.Checked || chkCodRef.Checked)
                    PesquisaProduto(cbProduto.Text);
            }

            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                        //txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
        private void PesquisaProduto(string IDPRODUTO)
        {
            PRODUTOSTy = null;
            if (chkPesqCodBarra.Checked)
                PRODUTOSTy = PesquisaCodBarra(cbProduto.Text);
            else if (chkCodRef.Checked)
                PRODUTOSTy = PesquisaCodReferencia(cbProduto.Text);
            else
            {
                if (ValidacoesLibrary.ValidaTipoInt32(cbProduto.Text))
                {
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(IDPRODUTO));
                    if (PRODUTOSTy != null)
                        cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                }
            }

            if (PRODUTOSTy == null)
            {
                cbProduto.Focus();
                DialogResult dr = MessageBox.Show("Código inválido, deseja efetuar a pesquisa?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmSearchProduto frm = new FrmSearchProduto())
                    {
                        frm.ShowDialog();
                        var result = frm.Result;

                        if (result > 0)
                        {
                            PRODUTOSTy = PRODUTOSP.Read(result);
                            cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                            LocalizarProdutoPedido(PRODUTOSTy.IDPRODUTO);                         
                        }
                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                LocalizarProdutoPedido(PRODUTOSTy.IDPRODUTO);                  
            }
        }

        private void LocalizarProdutoPedido(int IDPRODUTO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl_2 = new LIS_PRODUTOSPEDIDOCollection();
                LIS_PRODUTOSPEDIDOColl_2 = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);
                
                if (LIS_PRODUTOSPEDIDOColl_2.Count > 0)
                {
                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl_2[0].IDPRODPEDIDO));
                    ListaProdutoPedido(_IDPEDIDO);
                    Beep(1000, 300);
                    cbProduto.Focus();
                }
                else
                {
                    Util.ExibirMSg("Produto não localizado!", "Red");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro " + ex.Message);
            }

        }

        private PRODUTOSEntity PesquisaCodBarra(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODBARRA", "System.String", "=", Pesquisa));

                PRODUTOSCollection PRODUTOSPESCODBARRACOLL = new PRODUTOSCollection();
                PRODUTOSPESCODBARRACOLL = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOSPESCODBARRACOLL.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOSPESCODBARRACOLL[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return PRODUTOSPESBARRATY;

        }

        private PRODUTOSEntity PesquisaCodReferencia(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", Pesquisa));

                PRODUTOSCollection PRODUTOSPESCODBARRACOLL = new PRODUTOSCollection();
                PRODUTOSPESCODBARRACOLL = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOSPESCODBARRACOLL.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOSPESCODBARRACOLL[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return PRODUTOSPESBARRATY;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
