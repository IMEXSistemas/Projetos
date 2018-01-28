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
    public partial class FrmBaixaLotePagar : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        DUPLICATAPAGAREntity DUPLICATAPAGARTy = new DUPLICATAPAGAREntity();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        public FrmBaixaLotePagar()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }
       
        public int _idFornecedor = -1;
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
                BaixarLote();
        }

        private void BaixarLote()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar em lote as duplicatas?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
              
                decimal ValorBaixa = Convert.ToDecimal (txtValorPago.Text);
                foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
                {
                    if (ValorBaixa > 0)
                    {
                        DUPLICATAPAGARTy = DUPLICATAPAGARP.Read(Convert.ToInt32(item.IDDUPLICATAPAGAR));
                        DUPLICATAPAGARTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);                       

                        if (ValorBaixa >= DUPLICATAPAGARTy.VALORDEVEDOR)
                        {
                            DUPLICATAPAGARTy.VALORPAGO = DUPLICATAPAGARTy.VALORDEVEDOR;
                            DUPLICATAPAGARTy.IDSTATUS = 3;//Pago 
                            ValorBaixa -= Convert.ToDecimal(DUPLICATAPAGARTy.VALORDEVEDOR);
                            DUPLICATAPAGARTy.VALORDEVEDOR = 0;
                        }
                        else
                        {
                            DUPLICATAPAGARTy.VALORPAGO = ValorBaixa;
                            DUPLICATAPAGARTy.IDSTATUS = 4;//Pago Parcial
                            DUPLICATAPAGARTy.VALORDEVEDOR = DUPLICATAPAGARTy.VALORDEVEDOR - ValorBaixa;
                            ValorBaixa -= Convert.ToDecimal(DUPLICATAPAGARTy.VALORPAGO);
                            DUPLICATAPAGARTy.OBSERVACAO += "( Pago Parcial - Valor Pagto: " + Convert.ToDecimal(DUPLICATAPAGARTy.VALORPAGO).ToString("n2") + " Data Pagto: " + msktDataPagto.Text + " ) ";
                        }

                        DUPLICATAPAGARP.Save(DUPLICATAPAGARTy);
                    }
                    else
                        break;
                 
                }

                //Entrada do Caixa
                if (chkEntraCaixa.Checked)
                    EntradaCaixa();


                MessageBox.Show("Duplicata baixada em lote com sucesso!");
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

        
        decimal TotalDevedor = 0;
        public void SumTotalPesquisada()
        {
            TotalDevedor = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                TotalDevedor += Convert.ToDecimal(item.VALORDEVEDOR);
            }
           
            lblTotalDevedor.Text = TotalDevedor.ToString("n2");
            txtValorPago.Text = TotalDevedor.ToString("n2");  
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


                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", _idFornecedor.ToString(),"and"));
                RowRelatorio.Add(new RowsFiltro("VALORDEVEDOR","System.Decimal", "<>", "0"));
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
                msktDataPagto.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SumTotalPesquisada();
                txtValorPago.Focus();

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
