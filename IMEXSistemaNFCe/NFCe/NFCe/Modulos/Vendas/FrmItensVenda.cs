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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmItensVenda : Form
    {
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
        LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
        PRODUTONFCEProvider PRODUTONFCEP = new PRODUTONFCEProvider();
        PRODUTONFCEEntity PRODUTONFCETy = new PRODUTONFCEEntity();

        CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public int CUPOMELETRONICOID = -1;

        public FrmItensVenda()
        {
            InitializeComponent();
        }

        private void FrmItensVenda_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                AddItemProdutosNFCe(CUPOMELETRONICOID);
                ExibirDadosCupom(CUPOMELETRONICOID);
                GetDropProdutos();

                btnSair.Image = Util.GetAddressImage(21);
                btnSalvar.Image = Util.GetAddressImage(15);
                btnLimpar.Image = Util.GetAddressImage(16);

                CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(CUPOMELETRONICOID);

                if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4)// Aberto
                {
                    dtgItensCupom.Enabled = true;
                    cbProduto.Enabled = true;
                    txtQuant.Enabled = true;
                    txtvalorunit.Enabled = true;
                    btnSalvar.Enabled = true;
                    btnLimpar.Enabled = true;
                }
                else
                {
                    dtgItensCupom.Enabled = false;
                    cbProduto.Enabled = false;
                    txtQuant.Enabled = false;
                    txtvalorunit.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnLimpar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AddItemProdutosNFCe(int CUPOMELETRONICOID)
        {
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl_iTEM = new LIS_PRODUTONFCECollection();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
            LIS_PRODUTONFCEColl_iTEM = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio, "PRODUTONFCEID");

            PRODUTONFCEProvider PRODUTONFCEP = new PRODUTONFCEProvider();
            int itemProduto = 1;
            foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl_iTEM)
            {
                PRODUTONFCEEntity PRODUTONFCETy = new PRODUTONFCEEntity();
                PRODUTONFCETy = PRODUTONFCEP.Read(Convert.ToInt32(item.PRODUTONFCEID));
                PRODUTONFCETy.ITEM = itemProduto;
                PRODUTONFCEP.Save(PRODUTONFCETy);
                itemProduto++;
            }
        }

        private void GetDropProdutos()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

    

        private void ExibirDadosCupom(int CUPOMELETRONICOID)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
                LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                lblNumItens.Text = "Nº de Itens: " + LIS_PRODUTONFCEColl.Count.ToString();

                dtgItensCupom.AutoGenerateColumns = false;
                dtgItensCupom.DataSource = LIS_PRODUTONFCEColl;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void dtgItensCupom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 2)
            {
                if ((dtgItensCupom.Rows.Count - 2) > e.RowIndex)
                    e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dtgItensCupom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgItensCupom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTONFCEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    try
                    {
                        if (LIS_PRODUTONFCEColl[rowindex].IDSTATUSNFCE == 4) // Aberto
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTONFCEColl[rowindex].PRODUTONFCEID);
                            PRODUTONFCETy = PRODUTONFCEP.Read(CodSelect);
                            txtQuant.Text = Convert.ToDecimal(PRODUTONFCETy.QUANTIDADE).ToString("n2");
                            cbProduto.SelectedValue = PRODUTONFCETy.IDPRODUTO;
                            txtvalorunit.Text = Convert.ToDecimal(PRODUTONFCETy.VALORUNITARIO).ToString("n2");
                            txtVlTotal.Text = Convert.ToDecimal(PRODUTONFCETy.VALORTOTAL).ToString("n2");
                        }
                        else
                            MessageBox.Show("Cupom Já Emitido, Não é Possível Alterar!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro técnico: "+ ex.Message);
                    }
                    
                }
                else if (ColumnSelecionada == 1)//Excluir
                {

                    if (LIS_PRODUTONFCEColl[rowindex].IDSTATUSNFCE == 4) // Aberto
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTONFCEColl[rowindex].PRODUTONFCEID);
                                PRODUTONFCEP.Delete(CodSelect);
                                ExibirDadosCupom(CUPOMELETRONICOID);
                                MessageBox.Show(ConfigMessage.Default.MsgDelete2);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                                MessageBox.Show("Erro técnico: " + ex.Message);
                            }
                        }
                    }
                    else
                        MessageBox.Show("Cupom Já Emitido, Não é Possível Excluir!");

                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    PRODUTONFCETy.IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                    PRODUTONFCETy.QUANTIDADE = Convert.ToDecimal(txtQuant.Text);
                    PRODUTONFCETy.VALORUNITARIO = Convert.ToDecimal(txtvalorunit.Text);
                    PRODUTONFCETy.VALORTOTAL = Convert.ToDecimal(txtVlTotal.Text);
                    PRODUTONFCEP.Save(PRODUTONFCETy);
                    ExibirDadosCupom(CUPOMELETRONICOID);
                    MessageBox.Show(ConfigMessage.Default.MsgSave);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
            {
                errorProvider1.SetError(txtvalorunit, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtvalorunit.Text))
            {
                errorProvider1.SetError(txtvalorunit, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text) || Convert.ToDecimal(txtQuant.Text) <= 0)
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            PRODUTONFCETy = null;
            cbProduto.SelectedValue = -1;
            txtvalorunit.Text = "0,000";
            txtVlTotal.Text = "00,00";
        }

        private void txtQuant_Leave(object sender, EventArgs e)
        {
            SomaTotal();
        }

        private void SomaTotal()
        {
            try
            {
                decimal ValorUnitario = Convert.ToDecimal(txtvalorunit.Text);
                decimal Quantidade = Convert.ToDecimal(txtQuant.Text);
                decimal ValorTotal = 0;

                if (ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
                {
                    ValorTotal = ValorUnitario * Quantidade;
                    txtVlTotal.Text = Convert.ToDecimal(ValorTotal).ToString("n2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtvalorunit_Leave(object sender, EventArgs e)
        {
            SomaTotal();
        }

        private void txtQuant_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaTotal();
                }
            }
            else
                TextBoxSelec.Text = "1,00";
        }

        private void txtvalorunit_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaTotal();
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) && (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                        txtQuant.Focus();
                    }
                }
            }
        }

    }
}
