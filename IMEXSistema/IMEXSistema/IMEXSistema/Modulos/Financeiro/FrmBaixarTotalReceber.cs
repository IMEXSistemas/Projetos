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
using System.IO;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBaixarTotalReceber : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        DUPLICATARECEBEREntity DUPLICATARECEBERTy = new DUPLICATARECEBEREntity();
        public LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();

        string ObsRecibo = string.Empty;
        public FrmBaixarTotalReceber()
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
                int IDTIPOMOVCAIXA = 1;//Crédito
                decimal VALOR = IDTIPOMOVCAIXA == 2 ? (Convert.ToDecimal(txtValorPago.Text) * -1) : Convert.ToDecimal(txtValorPago.Text);

                DateTime DATAMOVIMENTACAO = Convert.ToDateTime(msktDataPagto.Text);

                string OBSERVACAO = "Baixa de duplicata a receber numero: " + txtDuplicatas.Text;

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
                if (LIS_DUPLICATARECEBERColl.Count == 1)
                    BaixarTotal();
                else if (LIS_DUPLICATARECEBERColl.Count > 1)
                    BaixarTotalLote();
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

        private void BaixarTotalLote()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar a duplicata?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    decimal valorPago = Convert.ToDecimal(txtValorPago.Text);
                    foreach (var item in LIS_DUPLICATARECEBERColl)
                    {
                        DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(Convert.ToInt32(item.IDDUPLICATARECEBER));
                        DUPLICATARECEBERTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                        DUPLICATARECEBERTy.IDSTATUS = 3; //Pago

                        if(valorPago > item.VALORDEVEDOR)
                        {
                            DUPLICATARECEBERTy.VALORPAGO = item.VALORDEVEDOR;
                            valorPago = valorPago - Convert.ToDecimal(item.VALORDEVEDOR);
                        }
                        else
                        {
                            DUPLICATARECEBERTy.VALORPAGO = valorPago;
                            valorPago = 0;
                        }                       

                        DUPLICATARECEBERTy.VALORDEVEDOR = 0;

                        //Calculo de dias de atraso
                        TimeSpan date = Convert.ToDateTime(msktDataPagto.Text) - Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO);
                        int DIASATRASO = date.Days;

                        if (DIASATRASO < 0)
                            DUPLICATARECEBERTy.DIASATRASO = 0;
                        else
                            DUPLICATARECEBERTy.DIASATRASO = DIASATRASO;

                        DUPLICATARECEBERP.Save(DUPLICATARECEBERTy);

                        //Entra movimentacao de conta corrente
                        if (Convert.ToInt32(cbContaCorrente.SelectedValue) > 0)
                        {
                            MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
                            MOVCONTACORRENTEP.Save(Entity);
                        }

                        //Entrada do Caixa
                        if (chkEntraCaixa.Checked)
                            EntradaCaixaLote(DUPLICATARECEBERTy.NUMERO, Convert.ToDecimal(DUPLICATARECEBERTy.VALORPAGO));
                    }

                    if (chkImprimirRecibo.Checked)
                        ImprimirRecibo1Via();
                   

                    MessageBox.Show("Duplicata baixada com sucesso!");


                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possível baixar parcialmente a duplicata!");
                }
            }
        }

        private void BaixarTotal()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente baixar a duplicata?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (LIS_DUPLICATARECEBERColl.Count == 1)
                    {
                        _idDuplicata = Convert.ToInt32(LIS_DUPLICATARECEBERColl[0].IDDUPLICATARECEBER);
                        DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(_idDuplicata);
                    }

                    DUPLICATARECEBERTy.DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                    DUPLICATARECEBERTy.IDSTATUS = 3; //Pago
                    DUPLICATARECEBERTy.VALORPAGO = Convert.ToDecimal(txtValorPago.Text);
                    DUPLICATARECEBERTy.VALORDEVEDOR = 0;

                    //Calculo de dias de atraso
                    TimeSpan date = Convert.ToDateTime(msktDataPagto.Text) - Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO);
                    int DIASATRASO = date.Days;

                    if (DIASATRASO < 0)
                        DUPLICATARECEBERTy.DIASATRASO = 0;
                    else
                        DUPLICATARECEBERTy.DIASATRASO = DIASATRASO;


                    DUPLICATARECEBERP.Save(DUPLICATARECEBERTy);

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

                    if (chkImprimirRecibo.Checked)
                        ImprimirRecibo1Via();


                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi fazer a baixa da duplicata!");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        

        private void ImprimirRecibo1Via()
        {
            ObsRecibo = InputBox("Observação do Recibo", ConfigSistema1.Default.NomeEmpresa, "");

            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument4.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 400);

                objPrintPreview.Document = printDocument4;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
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
            CaixaTy.OBSERVACAO = "Duplicata Nº " + DUPLICATARECEBERTy.NUMERO + " Cliente: " + GetNameCliente(Convert.ToInt32(DUPLICATARECEBERTy.IDCLIENTE));

            try
            {
                CaixaP.Save(CaixaTy);
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private void EntradaCaixaLote(string NDOCUMENTO, decimal VALORPAGO)
        {
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
            CaixaTy.VALOR = VALORPAGO;
            CaixaTy.DATAMOV = Convert.ToDateTime(msktDataPagto.Text);
            CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = NDOCUMENTO;
            CaixaTy.OBSERVACAO = "Duplicata Nº " + NDOCUMENTO + " Cliente: " + GetNameCliente(Convert.ToInt32(LIS_DUPLICATARECEBERColl[0].IDCLIENTE));

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

        private void FrmBaixar_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
                GetDropTipoDuplicata();//
                GetDropCentroCusto();
                btnCadTipo.Image = Util.GetAddressImage(6);
                btnAddCentroCusto.Image = Util.GetAddressImage(6);
                btnNomeMoviment.Image = Util.GetAddressImage(6);
                btnCadContCorrent.Image = Util.GetAddressImage(6);
                btnSair.Image = Util.GetAddressImage(21);

                //Armazena na classe de transporte para efetuar a baixa
                if (_idDuplicata != -1)
                {
                    DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(_idDuplicata);

                    if (DUPLICATARECEBERTy != null && DUPLICATARECEBERTy.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = DUPLICATARECEBERTy.IDCENTROCUSTO;

                    ////Efetua a consulta para exibir dados da duplicata selecionada
                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    RowRelatorio.Add(new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", _idDuplicata.ToString()));

                    LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_DUPLICATARECEBERColl.Count > 0)
                    {
                        txtDuplicatas.Text = LIS_DUPLICATARECEBERColl[0].NUMERO;
                        txtValorPago.Text = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[0].VALORDEVEDOR).ToString("n2");
                    }
                }
                else
                {
                    decimal totalPago = 0;
                    foreach (var item in LIS_DUPLICATARECEBERColl)
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

                if (LIS_DUPLICATARECEBERColl[0].IDSTATUS == 3)
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

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                GetDropTipoDuplicata();
                cbTipo.SelectedValue = CodSelec;
            }
        }

        private void chkEntraCaixa_CheckedChanged(object sender, EventArgs e)
        {
            cbTipo.Enabled = chkEntraCaixa.Checked;
            cbCentroCusto.Enabled = chkEntraCaixa.Checked;
        }

        private void btnAddCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetDropCentroCusto();

                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Recibo 1 um via
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 330);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

               // e.Graphics.DrawString("R$ " + Convert.ToDecimal(DUPLICATARECEBERTy.VALORPAGO).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                e.Graphics.DrawString("R$ " +  txtValorPago.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(txtValorPago.Text));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 140);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 160);
                e.Graphics.DrawString("Nome:     " + LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 180);

                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString("CNPJ/CPF: " + CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 200);

                e.Graphics.DrawString("Endereço: " + LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 220);
                e.Graphics.DrawString("Cidade:   " + LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 240);
                e.Graphics.DrawString("UF: " + LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 240);
                e.Graphics.DrawString("CEP: " + LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 240);

                e.Graphics.DrawString("Referente Duplicata nº: " + DUPLICATARECEBERTy.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 260);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 280, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 280);
                e.Graphics.DrawString(Util.QuebraString(ObsRecibo, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 295);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 280, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 310);
                e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 325);


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void btnCadCheque_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmCheque frm = new FrmCheque())
                {
                    frm._idClienteSelec = Convert.ToInt32(DUPLICATARECEBERTy.IDCLIENTE);
                    frm._valorcheque = Convert.ToDecimal(txtValorPago.Text);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void cbContaCorrente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnNomeMoviment_Click(object sender, EventArgs e)
        {

        }

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtValor_Validating(object sender, CancelEventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void btnNomeMoviment_Click_1(object sender, EventArgs e)
        {
            using (FrmContaBancaria frm = new FrmContaBancaria())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbContaCorrente.SelectedValue);
                GetDropMoviConta();
                cbContaCorrente.SelectedValue = CodSelec;
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
