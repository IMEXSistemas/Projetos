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
    public partial class FrmBaixaLoteReceber : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        DUPLICATARECEBEREntity DUPLICATARECEBERTy = new DUPLICATARECEBEREntity();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();

        public FrmBaixaLoteReceber()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        int _IDMOVCTCORRENTE = -1;
        public MOVCONTACORRENTEEntity Entity
        {
            get
            {
                string NUMMOVIMENTACAOM = LIS_DUPLICATARECEBERColl[0].NUMERO;
                int IDCONTACORRENTE = Convert.ToInt32(cbContaCorrente.SelectedValue);
                int IDMOVIMENTACAO = Convert.ToInt32(cbNomeMovimentacao.SelectedValue);
                int IDTIPOMOVCAIXA = 1;//Crédito
                decimal VALOR = IDTIPOMOVCAIXA == 2 ? (Convert.ToDecimal(txtValorPago.Text) * -1) : Convert.ToDecimal(txtValorPago.Text);

                DateTime DATAMOVIMENTACAO = Convert.ToDateTime(msktDataPagto.Text);

                string OBSERVACAO = "Baixa parcial de duplicata a receber numero: " + LIS_DUPLICATARECEBERColl[0].NUMERO;

                return new MOVCONTACORRENTEEntity(_IDMOVCTCORRENTE, NUMMOVIMENTACAOM, IDCONTACORRENTE, IDMOVIMENTACAO,
                                                   IDTIPOMOVCAIXA, OBSERVACAO, VALOR, DATAMOVIMENTACAO);
            }
        }

       
        public int _idCliente = -1;
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

                decimal ValorBaixa = Convert.ToDecimal(txtValorPago.Text);
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    if (ValorBaixa > 0)
                    {
                        DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(Convert.ToInt32(item.IDDUPLICATARECEBER));
                        DUPLICATARECEBERTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);

                        if (ValorBaixa >= DUPLICATARECEBERTy.VALORDEVEDOR)
                        {
                            DUPLICATARECEBERTy.VALORPAGO = DUPLICATARECEBERTy.VALORDEVEDOR;
                            DUPLICATARECEBERTy.IDSTATUS = 3;//Pago 
                            ValorBaixa -= Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR);
                            DUPLICATARECEBERTy.VALORDEVEDOR = 0;
                        }
                        else
                        {
                            DUPLICATARECEBERTy.VALORPAGO = ValorBaixa;
                            DUPLICATARECEBERTy.IDSTATUS = 4;//Pago Parcial
                            DUPLICATARECEBERTy.VALORDEVEDOR = DUPLICATARECEBERTy.VALORDEVEDOR - ValorBaixa;
                            ValorBaixa -= Convert.ToDecimal(DUPLICATARECEBERTy.VALORPAGO);
                            DUPLICATARECEBERTy.OBSERVACAO += "( Pago Parcial - Valor Pagto: " + Convert.ToDecimal(DUPLICATARECEBERTy.VALORPAGO).ToString("n2") + " Data Pagto: " + msktDataPagto.Text + " ) ";
                        }


                        //Entra movimentacao de conta corrente
                        if (Convert.ToInt32(cbContaCorrente.SelectedValue) > 0)
                        {
                            MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
                            MOVCONTACORRENTEP.Save(Entity);
                        }

                        DUPLICATARECEBERP.Save(DUPLICATARECEBERTy);
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
            CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = DUPLICATARECEBERTy.NUMERO;
            CaixaTy.OBSERVACAO = "Baixa em lote do Cliente: " + GetNameCliente(Convert.ToInt32(DUPLICATARECEBERTy.IDCLIENTE));

            try
            {
                CaixaP.Save(CaixaTy);
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private string GetNameCliente(int IdCliente)
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            return CLIENTEP.Read(IdCliente).NOME;
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
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
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

                this.MinimizeBox = false; 
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                
                GetDropTipoDuplicata();//
                GetDropCentroCusto();
                GetDropContaCorrente();
                GetDropMoviConta();

                btnCadTipo.Image = Util.GetAddressImage(6);
                btnAddCentroCusto.Image = Util.GetAddressImage(6);
                btnCadContCorrent.Image = Util.GetAddressImage(6);
                btnNomeMoviment.Image = Util.GetAddressImage(6);

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", _idCliente.ToString(),"and"));
                RowRelatorio.Add(new RowsFiltro("VALORDEVEDOR","System.Decimal", "<>", "0"));
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
                msktDataPagto.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SumTotalPesquisada();
                txtValorPago.Focus();

                this.Cursor = Cursors.Default;	
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;	

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

        private void btnNomeMoviment_Click(object sender, EventArgs e)
        {
            using (FrmMovimentacaoConta frm = new FrmMovimentacaoConta())
            {
                frm.ShowDialog();
                GetDropMoviConta();                
            }
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
