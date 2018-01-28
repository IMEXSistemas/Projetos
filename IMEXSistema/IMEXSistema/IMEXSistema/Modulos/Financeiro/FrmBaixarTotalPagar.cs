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
    public partial class FrmBaixarTotalPagar : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        DUPLICATAPAGAREntity DUPLICATAPAGARTy = new DUPLICATAPAGAREntity();
        public LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        public FrmBaixarTotalPagar()
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

        int _IDMOVCTCORRENTE = -1;
        public MOVCONTACORRENTEEntity Entity
        {
            get
            {
                string NUMMOVIMENTACAOM = Util.LimiterText(txtDuplicatas.Text, 20);
                int IDCONTACORRENTE = Convert.ToInt32(cbContaCorrente.SelectedValue);
                int IDMOVIMENTACAO = Convert.ToInt32(cbNomeMovimentacao.SelectedValue);
                int IDTIPOMOVCAIXA = 2;//Debito
                decimal VALOR = IDTIPOMOVCAIXA == 2 ? (Convert.ToDecimal(txtValorPago.Text) * -1) : Convert.ToDecimal(txtValorPago.Text);

                DateTime DATAMOVIMENTACAO = Convert.ToDateTime(msktDataPagto.Text);

                string OBSERVACAO = "Baixa de duplicata a pagar: " + txtDuplicatas.Text;

                return new MOVCONTACORRENTEEntity(_IDMOVCTCORRENTE, NUMMOVIMENTACAOM, IDCONTACORRENTE, IDMOVIMENTACAO,
                                                   IDTIPOMOVCAIXA, OBSERVACAO, VALOR, DATAMOVIMENTACAO);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                if (LIS_DUPLICATAPAGARColl.Count == 1)
                    BaixarTotal();
                else if (LIS_DUPLICATAPAGARColl.Count > 1)
                    BaixarTotalLote();
            }
        }

        private void BaixarTotalLote()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar a duplicata?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                decimal valorPago = Convert.ToDecimal(txtValorPago.Text);
                foreach (var item in LIS_DUPLICATAPAGARColl)
                {
                    DUPLICATAPAGARTy = DUPLICATAPAGARP.Read(Convert.ToInt32(item.IDDUPLICATAPAGAR));
                   DUPLICATAPAGARTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                   DUPLICATAPAGARTy.IDSTATUS = 3; //Pago

                    if (valorPago > item.VALORDEVEDOR)
                    {
                        DUPLICATAPAGARTy.VALORPAGO = item.VALORDEVEDOR;
                        valorPago = valorPago - Convert.ToDecimal(item.VALORDEVEDOR);
                    }
                    else
                    {
                        DUPLICATAPAGARTy.VALORPAGO = valorPago;
                        valorPago = 0;
                    }

                    //Calculo de dias de atraso
                    TimeSpan date = Convert.ToDateTime(msktDataPagto.Text) - Convert.ToDateTime(DUPLICATAPAGARTy.DATAVECTO);
                    int DIASATRASO = date.Days;

                    if (DIASATRASO < 0)
                        DUPLICATAPAGARTy.DIASATRASO = 0;
                    else
                        DUPLICATAPAGARTy.DIASATRASO = DIASATRASO;

                    DUPLICATAPAGARTy.VALORDEVEDOR = 0;
                    DUPLICATAPAGARP.Save(DUPLICATAPAGARTy);

                    //Entra movimentacao de conta corrente
                    if (Convert.ToInt32(cbContaCorrente.SelectedValue) > 0)
                    {
                        MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
                        MOVCONTACORRENTEP.Save(Entity);
                    }
                }

                //Entrada do Caixa
                if (chkEntraCaixa.Checked)
                    EntradaCaixaLote();

                MessageBox.Show("Duplicata baixada com sucesso!");


                this.Close();
            }
        }

        private void EntradaCaixaLote()
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
            CaixaTy.OBSERVACAO = "Duplicata Nº " + txtDuplicatas.Text + " Fornecedor: " + LIS_DUPLICATAPAGARColl[0].NOMEFORNECEDOR;

            try
            {
                CaixaP.Save(CaixaTy);
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private void BaixarTotal()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar a duplicata?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                if (LIS_DUPLICATAPAGARColl.Count == 1)
                {
                    _idDuplicata = Convert.ToInt32(LIS_DUPLICATAPAGARColl[0].IDDUPLICATAPAGAR);
                    DUPLICATAPAGARTy = DUPLICATAPAGARP.Read(_idDuplicata);
                }

                DUPLICATAPAGARTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                DUPLICATAPAGARTy.IDSTATUS = 3; //Pago
                DUPLICATAPAGARTy.VALORPAGO = Convert.ToDecimal(txtValorPago.Text);
                DUPLICATAPAGARTy.VALORDEVEDOR = 0;
                DUPLICATAPAGARTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
                

                //Calculo de dias de atraso
                TimeSpan date = Convert.ToDateTime(msktDataPagto.Text) - Convert.ToDateTime(DUPLICATAPAGARTy.DATAVECTO);
                int DIASATRASO = date.Days;

                if (DIASATRASO < 0)
                    DUPLICATAPAGARTy.DIASATRASO = 0;
                else
                    DUPLICATAPAGARTy.DIASATRASO = DIASATRASO;

                DUPLICATAPAGARP.Save(DUPLICATAPAGARTy);


                //Entra movimentacao de conta corrente
                if (Convert.ToInt32(cbContaCorrente.SelectedValue) > 0)
                {
                    MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
                    MOVCONTACORRENTEP.Save(Entity);
                }


                //Entrada do Caixa
                if (chkEntraCaixa.Checked)
                    EntradaCaixa();

                MessageBox.Show("Duplicata baixada com sucesso!");
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
            else if (Convert.ToInt32(cbContaCorrente.SelectedValue) > 0 && Convert.ToInt32(cbNomeMovimentacao.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbContaCorrente, ConfigMessage.Default.FieldErro);
                errorProvider1.SetError(cbNomeMovimentacao, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
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
                this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
                GetDropTipoDuplicata();//
                GetDropCentroCusto();
                btnCadTipo.Image = Util.GetAddressImage(6);
                btnAddCentroCusto.Image = Util.GetAddressImage(6);
                btnSair.Image = Util.GetAddressImage(21);

                btnNomeMoviment.Image = Util.GetAddressImage(6);
                btnCadContCorrent.Image = Util.GetAddressImage(6);

                //Armazena na classe de transporte para efetuar a baixa
                if (_idDuplicata != -1)
                {
                    DUPLICATAPAGARTy = DUPLICATAPAGARP.Read(_idDuplicata);

                    if (DUPLICATAPAGARTy != null && DUPLICATAPAGARTy.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = DUPLICATAPAGARTy.IDCENTROCUSTO;

                    ////Efetua a consulta para exibir dados da duplicata selecionada
                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    RowRelatorio.Add(new RowsFiltro("IDDUPLICATAPAGAR", "System.Int32", "=", _idDuplicata.ToString()));

                    LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_DUPLICATAPAGARColl.Count > 0)
                    {
                        txtDuplicatas.Text = LIS_DUPLICATAPAGARColl[0].NUMERO;
                        txtValorPago.Text = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[0].VALORDEVEDOR).ToString("n2");
                    }
                }
                else
                {
                    decimal totalPago = 0;
                    foreach (var item in LIS_DUPLICATAPAGARColl)
                    {
                        txtDuplicatas.Text += item.NUMERO + " / ";
                        totalPago += Convert.ToDecimal(item.VALORDEVEDOR);
                        txtValorPago.Text = totalPago.ToString("n2");

                        if(item.IDCENTROCUSTO != null)
                            cbCentroCusto.SelectedValue = item.IDCENTROCUSTO;
                    }

                    txtDuplicatas.Text = txtDuplicatas.Text.Substring(0, txtDuplicatas.Text.Length - 3);
                }

                msktDataPagto.Text = DateTime.Now.ToString("dd/MM/yyyy");             

                GetDropContaCorrente();
                GetDropMoviConta();

                if (LIS_DUPLICATAPAGARColl[0].IDSTATUS == 3)
                {
                    MessageBox.Show("Duplicata já baixada!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao abrir a duplicata!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
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

        private void chkEntraCaixa_CheckedChanged(object sender, EventArgs e)
        {
            cbTipo.Enabled = chkEntraCaixa.Checked;
            cbCentroCusto.Enabled = chkEntraCaixa.Checked; 
        }

        private void cbTipo_Enter(object sender, EventArgs e)
        {
            GetDropTipoDuplicata();
        }

        private void cbCentroCusto_Enter(object sender, EventArgs e)
        {
            GetDropCentroCusto();
        }

        private void btnCadContCorrent_Click(object sender, EventArgs e)
        {
            using (FrmContaBancaria frm = new FrmContaBancaria())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbContaCorrente.SelectedValue);
                GetDropContaCorrente();
                cbContaCorrente.SelectedValue = CodSelec;
            }
        }

        private void btnNomeMoviment_Click(object sender, EventArgs e)
        {
            using (FrmMovimentacaoConta frm = new FrmMovimentacaoConta())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbNomeMovimentacao.SelectedValue);
                GetDropMoviConta();
                cbNomeMovimentacao.SelectedValue = CodSelec;
            }
        }

        private void GetDropContaCorrente()
        {
            CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
            CONTACORRENTECollection CONTACORRENTEColl = new CONTACORRENTECollection();
            CONTACORRENTEColl = CONTACORRENTEP.ReadCollectionByParameter(null, "NOMECONTA");

            cbContaCorrente.DisplayMember = "NOMECONTA";
            cbContaCorrente.ValueMember = "IDCONTACORRENTE";

            CONTACORRENTEEntity CONTACORRENTETy = new CONTACORRENTEEntity();
            CONTACORRENTETy.NOMECONTA = ConfigMessage.Default.MsgDrop;
            CONTACORRENTETy.IDCONTACORRENTE = -1;
            CONTACORRENTEColl.Add(CONTACORRENTETy);

            Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity>(cbContaCorrente.DisplayMember);

            CONTACORRENTEColl.Sort(comparer.Comparer);
            cbContaCorrente.DataSource = CONTACORRENTEColl;

            cbContaCorrente.SelectedIndex = 0;
        }

        private void GetDropMoviConta()
        {
            MOVIMENTACAOCONTAProvider MOVIMENTACAOCONTAP = new MOVIMENTACAOCONTAProvider();
            MOVIMENTACAOCONTACollection MOVIMENTACAOCONTAColl = new MOVIMENTACAOCONTACollection();
            MOVIMENTACAOCONTAColl = MOVIMENTACAOCONTAP.ReadCollectionByParameter(null, "NOMEMOVIMENTACAO");

            cbNomeMovimentacao.DisplayMember = "NOMEMOVIMENTACAO";
            cbNomeMovimentacao.ValueMember = "IDMOVICONTA";

            MOVIMENTACAOCONTAEntity MOVIMENTACAOCONTATy = new MOVIMENTACAOCONTAEntity();
            MOVIMENTACAOCONTATy.NOMEMOVIMENTACAO = ConfigMessage.Default.MsgDrop;
            MOVIMENTACAOCONTATy.IDMOVICONTA = -1;
            MOVIMENTACAOCONTAColl.Add(MOVIMENTACAOCONTATy);

            Phydeaux.Utilities.DynamicComparer<MOVIMENTACAOCONTAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MOVIMENTACAOCONTAEntity>(cbNomeMovimentacao.DisplayMember);

            MOVIMENTACAOCONTAColl.Sort(comparer.Comparer);
            cbNomeMovimentacao.DataSource = MOVIMENTACAOCONTAColl;

            cbNomeMovimentacao.SelectedIndex = 0;
        }
    }
}
