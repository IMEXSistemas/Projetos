using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;
using System.Windows.Forms;
using BmsSoftware.Modulos.Cadastros;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBaixaParcialPagar : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        DUPLICATAPAGAREntity DUPLICATAPAGARTy = new DUPLICATAPAGAREntity();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();


        public FrmBaixaParcialPagar()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }
       
        public int _idDuplicata = -1;
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


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

     
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
                BaixarParcial();
        }

        private void BaixarParcial()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar parcialmente a duplicata?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                DUPLICATAPAGARTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                DUPLICATAPAGARTy.IDSTATUS = 4; //Pago Parcial
                DUPLICATAPAGARTy.VALORPAGO = Convert.ToDecimal(txtValorPago.Text);
                DUPLICATAPAGARTy.VALORDEVEDOR = Convert.ToDecimal(DUPLICATAPAGARTy.VALORDEVEDOR) - Convert.ToDecimal(txtValorPago.Text);
                DUPLICATAPAGARTy.OBSERVACAO += "( Pago Parcial - Valor Pagto: " + txtValorPago.Text + " Data Pagto: " + msktDataPagto.Text + " ) ";
                DUPLICATAPAGARP.Save(DUPLICATAPAGARTy);

                //Entrada do Caixa
                if (chkEntraCaixa.Checked)
                    EntradaCaixa();

                MessageBox.Show("Duplicata baixada parcialmente com sucesso!");
                this.Close();
            }
        }

        private void EntradaCaixa()
        {
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
            CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
            CaixaTy.DATAMOV = Convert.ToDateTime(msktDataPagto.Text);
            CaixaTy.IDTIPOMOVCAIXA = 2;// Debito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = DUPLICATAPAGARTy.NUMERO;
            CaixaTy.OBSERVACAO = "Duplicata Nº " + DUPLICATAPAGARTy.NUMERO + " Fornecedor: " + GetNameFornecedor(Convert.ToInt32(DUPLICATAPAGARTy.IDFORNECEDOR));

            try
            {
                CaixaP.Save(CaixaTy);
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private string GetNameFornecedor(int IdFornecedor)
        {
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
            return FORNECEDORP.Read(IdFornecedor).NOME;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (msktDataPagto.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataPagto.Text))
            {
                errorProvider1.SetError(msktDataPagto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                msktDataPagto.Focus();
                result = false;
            }
            else if (txtValorPago.Text.Trim().Length == 0 || txtValorPago.Text == "0,00" ||
                !ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text) || Convert.ToDecimal(txtValorPago.Text) < 0)
            {
                errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                txtValorPago.Focus();
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void FrmBaixar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void txtValorPago_Leave(object sender, EventArgs e)
        {

        }

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorPago.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text))
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorPago.Text);
                    txtValorPago.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPago, "");
                }
            }
            else
            {
                txtValorPago.Text = "0,00";
                errorProvider1.SetError(txtValorPago, "");
            }
        }

        private void FrmBaixar_Load(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog; this.FormBorderStyle = FormBorderStyle.FixedDialog;
                GetDropTipoDuplicata();//
                GetDropCentroCusto();
                btnCadTipo.Image = Util.GetAddressImage(6);
                btnAddCentroCusto.Image = Util.GetAddressImage(6);
                btnSair.Image = Util.GetAddressImage(21);
             

                //Armazena na classe de transporte para efetuar a baixa
                DUPLICATAPAGARTy = DUPLICATAPAGARP.Read(_idDuplicata);

                //Efetua a consulta para exibir dados da duplicata selecionada
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDDUPLICATAPAGAR", "System.Int32", "=", _idDuplicata.ToString()));

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio);
                lblNDuplicata.Text = LIS_DUPLICATAPAGARColl[0].NUMERO;
                msktDataPagto.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtValorPago.Text = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[0].VALORDEVEDOR).ToString("n2");
            

                if (LIS_DUPLICATAPAGARColl[0].IDSTATUS == 3)
                {
                    MessageBox.Show("Duplicata já baixada!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                    this.Close();
                }

                this.Cursor = Cursors.Default;	
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao abrir a duplicata!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                this.Cursor = Cursors.Default;	
            }

        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
        }

        private void GetDropCentroCusto()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCusto.DisplayMember = "DESCRICAO";
            cbCentroCusto.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCusto.DataSource = CENTROCUSTOSColl;

            cbCentroCusto.SelectedIndex = 0;
        }

        private void chkEntraCaixa_CheckedChanged(object sender, EventArgs e)
        {
            cbTipo.Enabled = chkEntraCaixa.Checked;
            cbCentroCusto.Enabled = chkEntraCaixa.Checked; 
        }

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
            }
        }

        private void btnAddCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
            }
        }

        private void cbTipo_Enter(object sender, EventArgs e)
        {
            GetDropTipoDuplicata();
        }

        private void cbCentroCusto_Enter(object sender, EventArgs e)
        {
            GetDropCentroCusto();
        }       

     
    }
}
