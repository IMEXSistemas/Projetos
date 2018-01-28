using BMSSoftware.Modulos.Cadastros;
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
    public partial class FrmComissaoTerceiro : Form
    {
        Utility Util = new Utility(); 

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_COMISSAOTERCCollection LIS_COMISSAOTERCColl = new LIS_COMISSAOTERCCollection();

        COMISSAOTERCProvider COMISSAOTERCP = new COMISSAOTERCProvider();
        LIS_COMISSAOTERCProvider LIS_COMISSAOTERCP = new LIS_COMISSAOTERCProvider();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
        
        public int _IDPEDIDO = -1;

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmComissaoTerceiro()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }


        private void RegisterFocusEvents(Control.ControlCollection controls)
        {

            foreach (Control control in controls)
            {
                if ((control is TextBox) ||
                  (control is RichTextBox) ||
                  (control is ComboBox) ||
                  (control is MaskedTextBox))
                {
                    control.Enter += new EventHandler(controlFocus_Enter);
                    control.Leave += new EventHandler(controlFocus_Leave);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDCOMISSAOTERC = -1;
        public COMISSAOTERCEntity Entity
        {
            get
            {
                        
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);//   INTEGER
                decimal PORCENTAGEM = Convert.ToDecimal(txtPorComisVend.Text); //   NUMERIC(15,2)
                decimal VALOR = Convert.ToDecimal(txtValComissao.Text); //   NUMERIC(15,2)

                return new COMISSAOTERCEntity(_IDCOMISSAOTERC, _IDPEDIDO, IDFUNCIONARIO, PORCENTAGEM, VALOR);
            }
            set
            {

                if (value != null)
                {
                    _IDCOMISSAOTERC = value.IDCOMISSAOTERC;
                    _IDPEDIDO = Convert.ToInt32(value.IDPEDIDO);                   
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    txtPorComisVend.Text = Convert.ToDecimal(value.PORCENTAGEM).ToString("n2");
                    txtValComissao.Text = Convert.ToDecimal(value.VALOR).ToString("n2");

                    errorProvider1.Clear();
                }
                else
                {
                    _IDCOMISSAOTERC = -1;
                    cbFuncionario.SelectedValue = -1;
                    txtPorComisVend.Text = "0,00";
                    txtValComissao.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmComissaoTerceiro_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                cbVendedor.Image = Util.GetAddressImage(6);
               
                btnAddImagem.Image = Util.GetAddressImage(15);
                btnLimpaPesquisa.Image = Util.GetAddressImage(16);
                btnSair.Image = Util.GetAddressImage(21);

                GetFuncionario();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

                lblNumPedido.Text = LIS_PEDIDOColl[0].IDPEDIDO.ToString().PadLeft(6, '0');
                lblValorPedido.Text = Convert.ToDecimal(LIS_PEDIDOColl[0].TOTALPEDIDO).ToString("n2");
                lblCliente.Text = LIS_PEDIDOColl[0].NOMECLIENTE;

                ListaComissaoTerceiros(_IDPEDIDO);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorComisVend_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorComisVend.Text))
            {
                errorProvider1.SetError(txtPorComisVend, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                Double f = Convert.ToDouble(txtPorComisVend.Text);
                txtPorComisVend.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtPorComisVend, "");

                CalculaComissao();
            } 
        }

        private void CalculaComissao()
        {
            if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);

                if (PorcComissVend != Convert.ToDecimal(txtPorComisVend.Text))
                {
                    txtValComissao.Text = ((Convert.ToDecimal(lblValorPedido.Text) * Convert.ToDecimal(txtPorComisVend.Text)) / 100).ToString("n2");
                }
                else
                {
                    txtPorComisVend.Text = PorcComissVend.ToString("n2");
                    txtValComissao.Text = ((Convert.ToDecimal(lblValorPedido.Text) * PorcComissVend) / 100).ToString("n2");
                }
            }
        }

        private void txtValComissao_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValComissao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValComissao.Text))
                {
                    errorProvider1.SetError(txtValComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValComissao.Text);
                    txtValComissao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValComissao, "");
                }
            }
            else
                txtValComissao.Text = "0,00";
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
        }

        private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncionario.DataSource = FUNCIONARIOColl;

            cbFuncionario.SelectedIndex = 0;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Comissão de Terceiros");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void btnAddImagem_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    CalculaComissao();
                    _IDCOMISSAOTERC = COMISSAOTERCP.Save(Entity);
                    ListaComissaoTerceiros(_IDPEDIDO);
                    Entity = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }



        private void ListaComissaoTerceiros(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_COMISSAOTERCColl = LIS_COMISSAOTERCP.ReadCollectionByParameter(RowRelatorio);

               
                lblTotalPesquisa.Text = LIS_COMISSAOTERCColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_COMISSAOTERCEntity LIS_COMISSAOTERCTy = new LIS_COMISSAOTERCEntity();
                LIS_COMISSAOTERCTy.VALOR = SumTotalPesquisa("VALOR");
                LIS_COMISSAOTERCColl.Add(LIS_COMISSAOTERCTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_COMISSAOTERCColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_COMISSAOTERCEntity item in LIS_COMISSAOTERCColl)
            {
                if (NomeCampo == "VALOR")
                    valortotal += Convert.ToDecimal(item.VALOR);               
            }

            return valortotal;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbFuncionario.SelectedIndex == 0)
            {
                errorProvider1.SetError(label9, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValComissao.Text))
            {
                errorProvider1.SetError(txtValComissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorComisVend.Text))
            {
                errorProvider1.SetError(txtPorComisVend, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Entity = null;
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_COMISSAOTERCColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_COMISSAOTERCColl[rowindex].IDCOMISSAOTERC);
                    Entity = COMISSAOTERCP.Read(CodSelect);

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {

                            CodSelect = Convert.ToInt32(LIS_COMISSAOTERCColl[rowindex].IDCOMISSAOTERC);
                            COMISSAOTERCP.Delete(CodSelect);
                            ListaComissaoTerceiros(_IDPEDIDO);
                            Entity = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                           
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
