using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.NFCeServiceClient;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using CDSSoftware;
using DarumaFramework_NFCe;
using log4net;
using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace BmsSoftware.Modulos.Vendas
{
    public delegate void DelegateThreadFinished();
    public partial class FrmFecharVenda : Form
    {
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();

        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        //Arquivos da Dll
        private static readonly ILog Log = LogManager.GetLogger(typeof(FrmMain));
        private static string m_strPathToSaveFile = String.Empty;
        private static string m_XmlNode = String.Empty;
        private static string m_DirectoryName = String.Empty;
        private static string m_FileNameXmlEmulador = String.Empty;

        // events used to stop worker thread
        ManualResetEvent EventStopThread;
        ManualResetEvent EventThreadStopped;

         // Delegate instances used to call user interface functions from worker thread:
        public DelegateThreadFinished m_DelegateThreadFinished;

        string edNomeArquivo = string.Empty; //NomedoArquivoXMl
        string strQrcode_Public = string.Empty;

        LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();

        public int CUPOMELETRONICOID = -1;
       // Boolean NotaFiscalGerado = false;

        Utility Util = new Utility();

        public FrmFecharVenda()
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

        private void FrmFecharVenda_Load(object sender, EventArgs e)
        {
            try
            {
                AbreNewSystem2();

                lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");              

                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                GetFuncionario();
                GetDropCliente();             

                if (BmsSoftware.ConfigNFCe.Default.IdCliente.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdCliente))
                    cbCliente.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCliente);

                USUARIOProvider USUARIOP = new USUARIOProvider();
                int idvendedor = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);
                cbFuncionario.SelectedValue = idvendedor;

                EMPRESATy = EMPRESAP.Read(1);

                CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(CUPOMELETRONICOID);
                lblNotaFiscal.Text = CUPOMELETRONICOTy.NUMERONFCE.ToString();
                TotalCupom(CUPOMELETRONICOID);
                AddItemProdutosNFCe(CUPOMELETRONICOID);
                txtTotalVenda.Text = TotalCupom(CUPOMELETRONICOID).ToString("n2");

                if (CUPOMELETRONICOTy.IDSTATUSNFCE != 4) //Aberto
                {
                    cbCliente.SelectedValue = CUPOMELETRONICOTy.IDCLIENTE;
                    cbFuncionario.SelectedValue = CUPOMELETRONICOTy.IDVENDEDOR;
                }             

               // if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4) //Aberto
                 //   btnReceber.Enabled = true;
              //  else
              //      btnReceber.Enabled = false;

                txtValoPagoDinheiro.Text = txtTotalVenda.Text;
                txtValoPagoDinheiro.Focus();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AbreNewSystem2()
        {
            try
            {
                string NameArquivoOpen = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\NSNFCeClient.jar";
                if (!File.Exists(NameArquivoOpen))
                    MessageBox.Show("Arquivo não localizado: NSNFCeClient.jar");
                else
                {
                    System.Diagnostics.Process Processo1 = null;

                    //    //Verifica se o executavel NFCe da News Systems este aberto
                    Process[] pname = Process.GetProcessesByName("javaw");
                    if (pname.Length == 0)
                        Processo1 = System.Diagnostics.Process.Start(BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\NSNFCeClient.jar");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                MessageBox.Show("Erro ao Abrir o NSNFCeClient.jar");
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
    

        private Decimal TotalCupom(int CUPOMELETRONICOID)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
                LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                foreach (var item in LIS_PRODUTONFCEColl)
                {
                    result += Convert.ToDecimal(item.VALORTOTAL);
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }     

        private void GetDropCliente()
        {
            try
            {
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null, "NOME");

                cbCliente.DisplayMember = "NOME";
                cbCliente.ValueMember = "IDCLIENTE";

                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                CLIENTETy.IDCLIENTE = -1;
                CLIENTEColl.Add(CLIENTETy);

                Phydeaux.Utilities.DynamicComparer<CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLIENTEEntity>(cbCliente.DisplayMember);

                CLIENTEColl.Sort(comparer.Comparer);
                cbCliente.DataSource = CLIENTEColl;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void GetFuncionario()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
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

                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValorDesconto_Validated(object sender, EventArgs e)
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

                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void FrmFecharVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.F3)
                this.Close();

            if (e.KeyCode == Keys.F8)
                button2_Click(null, null);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text == "00,00")
                TextBoxSelec.Text = string.Empty;
        }

        private void txtValorDesconto_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text == "00,00")
                TextBoxSelec.Text = string.Empty;
        }

        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text == "00,00")
                TextBoxSelec.Text = string.Empty;
        }

        private void txtPorcDesconto_Leave(object sender, EventArgs e)
        {
          
        }    

        private void button2_Click(object sender, EventArgs e)
        {
            if(Validacoes())
            {
                if (CUPOMELETRONICOTy.IDSTATUSNFCE == 1)//Enviado)
                {
                    DialogResult dr = MessageBox.Show("Cupom já Emitido, Deseja Imprimir?",
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        CreaterCursor Cr = new CreaterCursor();
                        this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                        string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Remessas\REIMP_NFE_" + CUPOMELETRONICOTy.NUMERONFCE + ".txt";
                        StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252));
                        try
                        {
                            escrever.WriteLine("REIMPRIME|1|");
                            escrever.WriteLine("A|" + CUPOMELETRONICOTy.CHAVEACESSO + "|");
                            escrever.Close();
                            this.Cursor = Cursors.Default;
                        }
                        catch (Exception ex)
                        {
                            escrever.Close();
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        CreaterCursor Cr = new CreaterCursor();
                        this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                        CUPOMELETRONICOTy.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                        CUPOMELETRONICOTy.IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

                        CUPOMELETRONICOTy.VALORFINAL = Convert.ToDecimal(txtTotalVenda.Text);
                        CUPOMELETRONICOTy.VALORPAGO = Convert.ToDecimal(txtTotalVenda.Text);
                        CUPOMELETRONICOTy.VALORTROCO = Convert.ToDecimal(txtTroco.Text);
                        CUPOMELETRONICOTy.PORCDESCONTO = 0;
                        CUPOMELETRONICOTy.VALORDESCONTO = 0;

                        CUPOMELETRONICOTy.VALOPAGODINHEIRO = Convert.ToDecimal(txtValoPagoDinheiro.Text);
                        CUPOMELETRONICOTy.VALOPAGOCARTAOCREDITO = Convert.ToDecimal(txtValoPagoCartaoCredito.Text);
                        CUPOMELETRONICOTy.VALOPAGOVALEREFEICAO = Convert.ToDecimal(txtValoPagoValeRefeicao.Text);
                        CUPOMELETRONICOTy.VALORPAGOCHEQUE = Convert.ToDecimal(txtValorPagoCheque.Text);
                        CUPOMELETRONICOTy.VALOPAGOCARTAODEBITO = Convert.ToDecimal(txtValoPagoCartãoDebito.Text);
                        CUPOMELETRONICOTy.VALOPAGOOUTROS = Convert.ToDecimal(txtValoPagoOutros.Text);
                        CUPOMELETRONICOTy.VALORTROCO = Convert.ToDecimal(txtTroco.Text);

                        if (cboperadoracartao.SelectedIndex == 0)
                            CUPOMELETRONICOTy.CODOPERADORACARTAO = 1;//Visa
                        else if (cboperadoracartao.SelectedIndex == 1)
                            CUPOMELETRONICOTy.CODOPERADORACARTAO = 2;//Mastercard
                        else if (cboperadoracartao.SelectedIndex == 2)
                            CUPOMELETRONICOTy.CODOPERADORACARTAO = 3;//American Expres
                        else if (cboperadoracartao.SelectedIndex == 3)
                            CUPOMELETRONICOTy.CODOPERADORACARTAO = 4;//Sorocred
                        else if (cboperadoracartao.SelectedIndex == 4)
                            CUPOMELETRONICOTy.CODOPERADORACARTAO = 99;//Outros

                        CUPOMELETRONICOTy.NOMEOPERADORACARTAO = cboperadoracartao.Text;
                        CUPOMELETRONICOTy.NUMEROAUTORIZACARTAO = txtNumeroAutorizaCartao.Text;

                        CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                        CUPOMELETRONICOID = -1;

                       // MessageBox.Show("Cupom fechado com sucesso!");
                        
                        //Busca o proximo numero de NFCe
                        if(CUPOMELETRONICOTy.NUMERONFCE == -1)
                            lblNotaFiscal.Text = BuscarProximoNNFCe().ToString();

                         if(VerificaDuplicidade(CUPOMELETRONICOTy.NUMERONFCE))
                              lblNotaFiscal.Text = BuscarProximoNNFCe().ToString();

                        //Verifica se Existe Duplicidade

                        Application.DoEvents();

                        CUPOMELETRONICOTy.NUMERONFCE = Convert.ToInt32(lblNotaFiscal.Text);//Enviado
                        CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);

                        GerarArquivoNewsSystems(lblNotaFiscal.Text);                       

                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }
      

        private Boolean VerificaDuplicidade(int? NumeroNota)
        {
            Boolean Result = false;

            try
            {
                string arquivo = @BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Processados\nsConcluido\NFCe_" + NumeroNota + ".txt";

                if (File.Exists(arquivo))
                {
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (linha.IndexOf("Duplicidade") != -1)//Autorizado o uso da NF-e, autorizacao fora de prazo
                            {
                                Result = true;
                            }                            
                        }
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return Result;
            }
        }



        private void GerarArquivoNewsSystems(string NumeroNota)
        {
            string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Remessas\NFCe_" + NumeroNota + ".txt";
            StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252));

            try
            {
                
                Application.DoEvents();
                this.Text = "Aguarde... Enviando Cupom NFCe";

                //////////////Atributos da NFC-e\\\\\\\\\\\\\\\\\
                escrever.WriteLine("NOTA FISCAL|1|");

                //Versão do leiaute(V1.0).
                //Tamanho 4 - Obrigatorio
                string versao = Util.LimiterText(BmsSoftware.ConfigNFCe.Default.VersaoSchema.Trim(), 4);
                escrever.WriteLine("A|"+ versao + "|NFe|");
                ///////////////////////////\\\\\\\\\\\\\\\\\\\\\\\

                //////////////Inicio - Identificadores da NFC-e\\\\\\\\\\\\\\\\\
                //1 - Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE de código de unidades da federação
                //Tamanho 2 - Obrigatorio
                string cUF = BuscaCodigoUF(EMPRESATy.UF).ToString();

                //2 - Código numérico que compõe a Chave de Acesso. Número aleatório gerado pelo emitente para cada NF-e para evitar acessos indevidos da NF-e. (v2.
                //Tamanho 8 - Opcional
                string cNF = "";

                //3 - Informar a natureza da operação de que decorrer a saída ou a entrada, tais como: venda, compra, 
                //transferência, devolução, importação, consignação, remessa (para fins de demonstração, de industrialização ou outra), 
                //conforme previsto na alínea 'i', inciso I, art. 19 do CONVÊNIO S/Nº, de 15 de dezembro de 1970
                //Tamanho 60 - Obrigatorio
                string natOp = "VENDA";

                // 4 - 0 - pagamento à vista; 1 - pagamento à prazo; 2 - outros.
                //Tamanho 1 - Obrigatorio
                string indPag = "0";

                //5 - 65=NFC-e, utilizada nas operações de vendas no varejo, onde não for exigida a NF-e por dispositivo legal.
                //Tamanho 2 - Obrigatorio
                string mod = Util.LimiterText(BmsSoftware.ConfigNFCe.Default.ModeloDocFiscal.Trim(),2);

                //6- Série 890 - 899 de uso exclusivo para emissão de NF - e avulsa, pelo contribuinte com seu certificado digital, 
                //através do site do Fisco(procEmi = 2). (v2.0) Serie 900 - 999 – uso exclusivo de NF-e emitidas no SCAN. (v2.0)
                //Tamanho 3 - Obrigatorio
                string serie = Util.LimiterText(BmsSoftware.ConfigNFCe.Default.SerieNFCe.Trim(), 3);

                //7 - Número do Documento Fiscal.
                //Tamanho 9 - Obrigatorio
                string nNF = Util.LimiterText(NumeroNota, 9);

                //8 - Formato AAAA-MM-DDThh:mm:ss TZD UTC - Universal Coordinated Time, onde TZD pode ser -02:00 (Fernando de Noronha), 
                //-03:00 (Brasília) ou -04:00 (Manaus), no horário de verão serão - 01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
                //Obrigatorio
                string dhEmi = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); ; 
                string FusoHorario = BmsSoftware.ConfigNFCe.Default.DiferencaFuxoHorario;
                dhEmi = dhEmi + "T" + DateTime.Now.ToString("HH:mm:ss") + FusoHorario;

                //9 - Formato AAAA-MM-DDThh:mm:ss TZD UTC - Universal Coordinated Time, onde TZD pode ser -02:00 (Fernando de Noronha),
                //-03:00 (Brasília) ou -04:00 (Manaus), no horário de verão serão - 01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
                //Opcional
                string dhSaiEnt = "";

                //10 // 1=Operação interna;
                //Tamanho 1 - Obrigatorio
                string idDest = "1";

                //11 - Tipo de Operação //1 - saída
                //Tamanho 1 - Obrigatorio
                string tpNF = "1";

                //12 - Informar o município de ocorrência do fato gerador do ICMS. Utilizar a Tabela do IBGE
                //Tamanho 7 - Obrigatorio
                string cMunFG = BuscaCodigoCidads(EMPRESATy.CIDADE, EMPRESATy.UF).ToString();

                //13 - 0=Sem geração de DANFE;
                //1 = DANFE normal, Retrato;
                //2 = DANFE normal, Paisagem;
                //3 = DANFE Simplificado;
                //4 = DANFE NFC - e;
                //5 = DANFE NFC - e em mensagem eletrônica.
                //   Nota: O envio de mensagem eletrônica pode ser feita de forma simultânea com a impressão do DANFE.Usar o tpImp = 
                //5 na NFC-e quando esta for a única forma de disponibilização do DANFE.
                //Tamanho 1 - Obrigatorio
                string tpImp = "4";

                //14 - Forma de Emissão da NF-e
                //1 =Emissão normal (não em contingência);
                //4 = Contingência DPEC(Declaração Prévia da Emissão em Contingência);
                //6 = Contingência SVC - AN(SEFAZ Virtual de Contingência do AN);
                //7 = Contingência SVC - RS(SEFAZ Virtual de Contingência do RS);
                //9 = Contingência off - line da NFC-e;
                //Obs: Para NFCe somente é válida a opção de contingência: 9 - Contingência Off - Line e, a critério da UF, opção 4 - Contingência EPEC.
                //Tamanho 1 - Obrigatorio
                string tpEmis = "1";

                //15 - Dígito Verificador da Chave de Acesso da NF-e
                //Informar o DV da Chave de Acesso da NF - e, o DV será calculado com a aplicação do algoritmo módulo 11(base 2, 9) da Chave de Acesso. 
                //(vide item 5 do Manual de Integração).
                //Não informar para a importação.Caso seja informado, será desconsiderado.
                //Tamanho 1 - Obrigatorio
                string cDV = "";

                var r = new Random((int)DateTime.Now.Ticks);
                int codaletorio = r.Next(10000000, 99999999);
                string strAno = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(2);
                string strMes = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                string strAnoMesEmissao = strAno + strMes;
                cDV = CriarChaveNfei(Convert.ToInt32(cUF), strAnoMesEmissao, Util.RetiraLetras(EMPRESATy.CNPJCPF), mod, Convert.ToInt32(serie), Convert.ToInt32(nNF), codaletorio).ToString();
                CUPOMELETRONICOTy.CHAVEACESSO = cDV;
                CUPOMELETRONICOP.Save(CUPOMELETRONICOTy); //Salva a chave de acesso
                cDV = cDV.Substring(cDV.Length - 1);

                //16 - dentificação do Ambiente
                //1 - Produção/ 2 - Homologação
                //Tamanho 1 - Obrigatorio
                string tpAmb = "1";
                if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "P")
                    tpAmb = "1";
                else if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")
                    tpAmb = "2";

                //17 Finalidade de emissão da NF-e //1 = NF-e normal;
                //Tamanho 1 - Obrigatorio
                string finNFe = "1";

                //18 Processo de emissão da NF-e // 
                //Identificador do processo de emissão da NF-e:
                //0 - emissão de NF-e com aplicativo do contribuinte;
                //1 - emissão de NF-e avulsa pelo Fisco;
                //2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;
                //3 - emissão NF - e pelo contribuinte com aplicativo fornecido pelo Fisco.
                //Deverá SEMPRE ser preenchido com 3 se o Software Emissor for utilizado para a emissão.
                string procEmi = "0";

                //19 - Versão do Processo de emissão da NF-e
                //Identificador da versão do processo de emissão (informar a versão do aplicativo emissor de NF-e). 
                //Caso informado versão diferente, o aplicativo, no momento da importação, realizará a atualização do campo automaticamente.
                string verProc = BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems;


                //20 - Data e Hora da entrada em contingência
                //Formato AAAA-MM-DDThh:mm:ss TZD UTC - Universal Coordinated Time, onde TZD pode ser -02:00 (Fernando de Noronha), -03:00 (Brasília) 
                //ou -04:00 (Manaus), no horário de verão serão - 01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
                //Opcional
                string dhCont = "";

                //21 - Justificativa da entrada em contingência
                //Opcional
                string xJust = "";

                //22 - Indica operação com Consumidor final
                //0=Não;
                //1 = Consumidor final;
                string indFinal = "1";// 

                //23 - Indicador de presença do comprador no estabelecimento comercial no momento da operação
                //0=Não se aplica (por exemplo, para a Nota Fiscal complementar ou de ajuste);
                //1 = Operação presencial;
                //4 = NFC - e em operação com entrega em domicílio;
                string indPres = "1"; 

                escrever.WriteLine("B|" + cUF + "|" + cNF + "|" + natOp + "|" + indPag + "|" + mod + "|" + serie + "|" + nNF + "|" + dhEmi + "|" + dhSaiEnt + "|" +
                                    idDest + "|" + tpNF + "|" + cMunFG + "|" + tpImp + "|" + tpEmis + "|" + cDV + "|" + tpAmb + "|" + finNFe + "|" + procEmi + "|" +
                                    verProc + "|" + dhCont + "|" + xJust + "|" + indFinal + "|" + indPres+ "|");
                ///////////////////////////Fim - Identificadores da NFC-e \\\\\\\\\\\\\\\\\\\\\\\

                ///////////////////////////Inicio - Informações do Cupom Fiscal referenciado\\\\\\\\\\\\\\\\\\\\\\\

                ///////////////////////////Fim - Informações do Cupom Fiscal referenciado\\\\\\\\\\\\\\\\\\\\\\\


                //////////////////Inicio - Identificação do Emitente da Nota Fiscal Consumidor eletrônica\\\\\\\\\\\\\\\\\\\\
                //1 - Razão Social ou Nome do emitente
                //Tamanho 60 - Obrigatorio
                string xNome = Util.LimiterText(EMPRESATy.NOMECLIENTE, 60);

                //2 - Nome fantasia
                //Tamanho 60 - Opcional
                string xFant = Util.LimiterText(EMPRESATy.NOMEFANTASIA, 60);
                if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")//Homologação
                    xFant = Util.LimiterText("NF - E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", 60);

                //3 - IE 
                //Campo de informação obrigatória  nos casos de emissão própria (procEmi = 0, 2 ou 3).
                //Tamanho 14 - Obrigatorio
                string IE = Util.RetiraLetras(EMPRESATy.IE);

                //4 - IE do Substituto Tributário
                //Informar a IE do ST da UF de destino da mercadoria, quando houver a retenção do ICMS ST para a UF de destino.
                //Tamanho 14 - Opcional
                string IEST = "";

                //5 - Inscrição Municipal
                //Este campo deve ser informado, quando ocorrer a emissão de NF-e conjugada, com prestação de serviços sujeitos ao ISSQN e 
                //fornecimento de peças sujeitos ao ICMS.
                //Tamanho 15 - Opcional
                string IM = "";

                //6 - CNAE fiscal
                //Este campo deve ser informado quando o campo IM for informado.
                //Tamanho 7 - Opcional
                string CNAE = "";

                //7 - Código do Regime Tributário
                //Este campo será obrigatoriamente preenchido com: 
                //1 – Simples Nacional;
                // 2 – Simples Nacional – excesso de sublimite de receita bruta;
                // 3 – Regime Normal. (v2.0). 
                string CRT = BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT;
                escrever.WriteLine("C|" + xNome + "|" + xFant + "|" + IE + "|" + IEST + "|" + IM + "|" + CNAE + "|" + CRT+ "|");

                //1 - CNPJ do emitente
                //Informar o CNPJ ou CPF do emitente. Em se tratando de emissão de NF-e avulsa pelo Fisco, as informações do remente 
                //serão informadas neste grupo. O CNPJ ou CPF deverão ser informados com os zeros não significativos.
                string CNPJ = Util.RetiraLetras(EMPRESATy.CNPJCPF);
                escrever.WriteLine("C02|" + CNPJ+ "|");
                //////////////////Fim - Identificação do Emitente da Nota Fiscal Consumidor eletrônica\\\\\\\\\\\\\\\\\\\\

                //////////////////Inicio - Endereço do Emitente da Nota Fiscal consumidor eletrônica\\\\\\\\\\\\\\\\\\\\\\\\
                //01 - Logradouro
                //Tamanho 60 - Obrig
                string xLgr = Util.LimiterText(EMPRESATy.ENDERECO, 60);//1 Logradouro - Tam.60

                //02 - Número
                //Tamanho 60 - Obrig
                string nro = Util.LimiterText(EMPRESATy.NUMERO, 60);

                //03 - Complemento
                //Tamanho 60 - Opcional
                string xCpl = Util.LimiterText(EMPRESATy.COMPLEMENTO, 60);

                //04 - Bairro
                 //Tamanho 60 - Obrig
               string xBairro = Util.LimiterText(EMPRESATy.BAIRRO, 60);

                //05 - Código do município
                //Utilizar a Tabela do IBGE. Informar ‘9999999 ‘para operações com o exterior
                //Tamanho 7 - Obrig
                string cMun = BuscaCodigoCidads(EMPRESATy.CIDADE, EMPRESATy.UF).ToString();

                //06 - Nome do município
                //Tamanho 60 - Obrig
                //nformar ‘EXTERIOR ‘para operações com o exterior. Para importação, será considerado o código do município para apresentação do nome do município.
                string xMun = Util.LimiterText(EMPRESATy.CIDADE,60);

                //07 - Sigla da UF
                //Informar ‘EX ‘para operações com o exterior.
                //Tamanho 2 - Obrig
                string UF = Util.LimiterText(EMPRESATy.UF, 2);

                //08 - Código do CEP
                //Informar os zeros não significativos
                //Tamanho 8 - Obrig
                string CEP = Util.RetiraLetras(EMPRESATy.CEP);

                //09 - Código do País
                //Utilizar a Tabela do BACEN.
                //Tamanho 4 - Opcional
                string cPais = "1058"; //9 Código do País - Utilizar a Tabela do BACEN.

                //10 - Nome do País
                //Tamanho 60 - Obrig
                string xPais = "BRASIL";//10 Nome do País

                //11 - Telefone
                //Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o
                //código do país + código da localidade + número do telefone (v.2.0)
                //Tamanho 14 - Opcional
                string fone = Util.RetiraLetras(EMPRESATy.TELEFONE);


                escrever.WriteLine("C05|" + xLgr + "|" + nro + "|" + xCpl + "|" + xBairro + "|" + cMun + "|" + xMun + "|" + UF + "|" + CEP + "|" + cPais + "|" + xPais + 
                                      "|" + fone + "|");
                //////////////////FIM - Endereço do Emitente da Nota Fiscal consumidor eletrônica\\\\\\\\\\\\\\\\\\\\\\\\

                /////////////////INICIO - Identificação do Destinatário da Nota Fiscal Consumidor \\\\\\\\\\\\\\\\\\\\\\\\\
                /////Opcional para a NFC-e.
                if (Convert.ToInt32(cbCliente.SelectedValue) > 1)
                {
                    //Dados do Cliente
                    LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
                    LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_CLIENTEColl.Count == 0)
                        MessageBox.Show("Cliente não selecionado!");

                    if (LIS_CLIENTEColl.Count > 0)
                    {
                        //1 - Razão Social ou nome do destinatário
                        //Opcional para a NFC-e.
                        //Tamanho: 60 - Opcional
                        //  string xNome_2 = Util.LimiterText(LIS_CLIENTEColl[0].NOME, 60);
                        string xNome_2 = TxtNomeCliente.Text;
                        if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")//Homologação
                            xNome_2 = Util.LimiterText("NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", 60);

                        //2 - IE
                        //Informar a IE quando o destinatário for contribuinte do ICMS.
                        // Informar ISENTO quando o destinatário for contribuinte do ICMS, mas não estiver obrigado à inscrição no cadastro de contribuintes do ICMS.
                        // Não informar se o destinatário não for contribuinte do ICMS.
                        //Tamanho:14 - Obrig
                        string IE_2 = Util.LimiterText(LIS_CLIENTEColl[0].IE, 14);

                        //3 - Inscrição na SUFRAMA
                        //Obrigatório, nas operações que se beneficiam de incentivos fiscais existentes nas áreas sob controle da SUFRAMA.
                        //A omissão da Inscrição SUFRAMA impede o processamento da operação pelo Sistema de Mercadoria Nacional da SUFRAMA e a 
                        //liberação da Declaração de Ingresso, prejudicando a comprovação do ingresso / internamento da mercadoria nas áreas sob 
                        //controle da SUFRAMA.
                        //Tamanho:8 - 9 - Opcional
                        string ISUF = "";

                        //4 - email
                        //Informar o email do destinatário. O campo pode ser utilizado para informar o e-mail de recepção da NF-e indicada pelo destinatário (v2.0) 
                        //Tamanho:60 - Opcional
                        string email = Util.LimiterText(LIS_CLIENTEColl[0].EMAILCLIENTE, 60);

                        //5 - Indicador da IE do Destinatário
                       // 1 = Contribuinte ICMS(informar a IE do destinatário);
                       // 2 = Contribuinte isento de Inscrição no cadastro de Contribuintes do ICMS;
                       // 9 = Não Contribuinte, que pode ou não possuir Inscrição Estadual no Cadastro de Contribuintes do ICMS;
                       // Nota 1: No caso de NFC-e informar indIEDest = 9 e não informar a tag IE do destinatário;
                       // Nota 2: No caso de operação com o Exterior informar indIEDest = 9 e não informar a tag IE do destinatário;
                       // Nota 3: No caso de Contribuinte Isento de Inscrição(indIEDest = 2), não informar a tag IE do destinatário.
                         string indIEDest = "";

                        //6 - Inscrição Municipal
                        //Inscrição Municipal do Prestador do Serviço. Informado na emissão de NF-e conjugada, com itens de produtos sujeitos ao 
                        //ICMS e itens de serviços sujeitos ao ISSQN.
                        string IM_2 = "";

                        escrever.WriteLine("E|" + xNome_2 + "|" + IE_2 + "|" + ISUF + "|" + email + "|" + indIEDest + "|" + IM_2);

                        //1 - CNPJ do destinatário
                        //Informar os zeros não significativos. Não informar esta tag se operação com Exterior. Nota: Campo não aceita o valor Nulo
                        //Tamanho:14 - Obrig
                        string CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";

                        //2 - CPF do destinatário
                        //Informar os zeros não significativos.
                        // Tamanho:11 - Obrig
                        string CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";
                        escrever.WriteLine("E02|" + Util.RetiraLetras(CNPJ_2) + "|" + Util.RetiraLetras(CPF) + "|");


                        //Endereço do Destinatário da NFC-e
                        //1 - Logradouro
                        // Tamanho:60 - Obrig
                        string xLgr_2 = Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1, 60);

                        //2 - Número
                        // Tamanho:60 - Obrig
                        string nro_2 = Util.LimiterText(LIS_CLIENTEColl[0].NUMEROENDER, 60);

                        //3 - Complemento
                        // Tamanho:60 - Obrig
                        string xCpl_2 = Util.LimiterText(LIS_CLIENTEColl[0].COMPLEMENTO1, 60);

                        //4 - Bairro
                        // Tamanho:60 - Obrig
                        string xBairro_2 = Util.LimiterText(LIS_CLIENTEColl[0].BAIRRO1, 60);

                        //5 - Código do município
                        // Tamanho:7 - Obrig
                        string cMun_2 = BuscaCodigoCidads(LIS_CLIENTEColl[0].MUNICIPIO, LIS_CLIENTEColl[0].UF).ToString();

                        //6 - Nome do município
                        // Tamanho:60 - Obrig
                        string xMun_2 = Util.LimiterText(LIS_CLIENTEColl[0].MUNICIPIO, 60);

                        //7 - Sigla da UF
                        // Tamanho:2 - Obrig
                        string UF_2 = Util.LimiterText(LIS_CLIENTEColl[0].UF, 2);

                        //8 - Código do CEP
                        // Tamanho:8 - Obrig
                        string CEP_2 = Util.LimiterText(LIS_CLIENTEColl[0].CEP1, 8);

                        //9 - Código do País
                        string cPais_2 = "1058";

                        //10 - Nome do País
                        // Tamanho:60 - Obrig
                        string xPais_2 = "BRASIL";

                        //11 - Telefone
                        // Tamanho:14 - Obrig
                        string fone_2 = Util.RetiraLetras(LIS_CLIENTEColl[0].TELEFONE1);

                        escrever.WriteLine("E5|" + xLgr_2 + "|" + nro_2 + "|" + xCpl_2 + "|" + xBairro_2 + "|" + cMun_2 + "|" + xMun_2 + "|" + UF_2 + "|" + CEP_2 +
                                           "|" + cPais_2 + "|" + xPais_2 + "|" + fone_2+ "|");
                    }
                    else
                    {
                        string CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                        string CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";

                        if (txtCPFCNPJ.Text.Trim() != string.Empty)
                        {
                            string xNome_2 = TxtNomeCliente.Text;
                            if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")//Homologação
                                xNome_2 = Util.LimiterText("NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", 60);
                            escrever.WriteLine("E|" + xNome_2 + "|| | |1||");

                            CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                            CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";
                            escrever.WriteLine("E02|" + CNPJ_2 + "|" + CPF + "|");
                        }
                        else
                        {
                            escrever.WriteLine("E|||||1||");
                            CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                            CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";
                            escrever.WriteLine("E02|" + CNPJ_2 + "|" + CPF + "|");
                        }

                        escrever.WriteLine("E05||||||||||||");
                    }
                }
                else
                {
                    string CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                    string CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";

                    if (txtCPFCNPJ.Text.Trim() != string.Empty)
                    {
                        string xNome_2 = TxtNomeCliente.Text;
                        if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")//Homologação
                            xNome_2 = Util.LimiterText("NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", 60);
                        escrever.WriteLine("E|" + xNome_2 + "|| | |1||");

                        CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                        CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";
                        escrever.WriteLine("E02|" + CNPJ_2 + "|" + CPF + "|");
                    }
                    else
                    {
                        escrever.WriteLine("E|||||1||");
                        CNPJ_2 = txtCPFCNPJ.Text.Length > 12 ? txtCPFCNPJ.Text : "";
                        CPF = txtCPFCNPJ.Text.Length < 12 ? txtCPFCNPJ.Text : "";
                        escrever.WriteLine("E02|" + CNPJ_2 + "|" + CPF + "|");
                    }

                     escrever.WriteLine("E05||||||||||||");
                }
                /////////////////FIM - Identificação do Destinatário da Nota Fiscal Consumidor \\\\\\\\\\\\\\\\\\\\\\\\\

                //////////////FIM - Detalhamento de Produtos da NFC-e - máximo = 990\\\\\\\\\\\\\\\\\\\\\\

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOTy.CUPOMELETRONICOID.ToString()));
                LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);                
                //////////////INICIO - Detalhamento de Produtos da NFC-e - máximo = 990\\\\\\\\\\\\\\\\\\\\\\

                ///////////////////////INICIO - Produtos da NFC-e\\\\\\\\\\\\\\\\\\\

                int i = 0;
                foreach (var item in LIS_PRODUTONFCEColl)
                {
                    //1 - Número do item
                    //Número do item (1 a 990)
                    //tamanho: 1 a 3 - Obrigatorio
                    string nItem = (i + 1).ToString();//1 - Número do item (1 a 990)

                    //2 - Informações Adicionais do Produto
                    //Norma referenciada, informações complementares, etc.
                    //Tamanho: 500 - Opcional
                    string infAdProd = "";  
                    escrever.WriteLine("H|" + nItem + "|" + infAdProd + "|");

                    //Dados dos Produto
                    LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
                    LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", item.IDPRODUTO.ToString()));
                    LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                    //1 - Código do produto ou serviço
                    //Preencher com CFOP, caso se trate de itens não relacionados com mercadorias/produto e que o 
                    //contribuinte não possua codificação própria. Formato ”CFOP9999”
                    //Tamanho: 60 - Obrigatorio
                    string cProd = item.IDPRODUTO.ToString().Trim();

                    //2 - GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras
                    //Preencher com o código GTIN-8, GTIN-12, GTIN-13 ou GTIN-14 (antigos códigos EAN, UPC e DUN-14), não informar em caso de o produto não possuir este código.
                    //Tamanho: 14 - Obrigatorio
                    string cEAN = item.CODBARRA.ToString().Trim();

                    //3 - Descrição do produto ou serviço
                    //Nota: Para NFC-e emitida no ambiente de Homologação deverá ser informado: NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL
                    //Tamanho : 120 - Obrigatorio
                    string xProd = Util.LimiterText(item.NOMEPRODUTO.Trim(), 120);
                    if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")//Homologação
                        xProd = Util.LimiterText("NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL", 120);

                    //4 - Código NCM com 8 dígitos ou  2 dígitos(gênero)
                    //Código NCM (8 posições), informar o gênero (posição do capítulo do NCM) quando a operação não for de comércio exterior 
                    //(importação/ exportação) ou o produto não seja tributado pelo IPI. 
                    //Em caso de serviço informar o código 99(v2.0)
                    //tamanho: 8 - Obrigatorio
                    string NCM = Util.LimiterText(LIS_PRODUTOSColl[0].NCMSH.Trim(), 8);

                    //5 - EX_TIPI
                    //Preencher de acordo com o código EX da TIPI. Em caso de serviço, não preencher.
                    //tamanho: 3 - Opcional
                    string EXTIPI = PRODUTOSTy.EXTIPI;

                    //6 - Código Fiscal de Operações e Prestações
                    //Utilizar Tabela de CF
                    //Tamanho: 4 - Obrigatorio
                    string CFOP = PRODUTOSTy.CFOP;
                    if (PRODUTOSTy.CFOP.Trim() == String.Empty)
                    {
                        CFOPEntity CFOPTy = new CFOPEntity();
                        CFOPProvider CFOPP = new CFOPProvider();
                        CFOPTy = CFOPP.Read(Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP));
                        if (CFOPTy != null)
                            CFOP = CFOPTy.CODCFOP;
                        else
                            CFOP = "5102";
                    }

                    //7 - Unidade Comercial
                    //nformar a unidade de comercialização do produto.
                    //Tamanho : 6 - Obrigatorio
                    string uCom = Util.LimiterText(LIS_PRODUTOSColl[0].NOMEUNIDADE.Trim(), 6);

                    //8 - Quantidade Comercial
                    //Informar a quantidade de comercialização do produto.
                    //tamanho: 15 - Obrigatorio
                    string qCom = Convert.ToDecimal(item.QUANTIDADE).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                    //9 - Valor Unitário de comercialização
                    //Informar o valor unitário de comercialização do produto, campo meramente informativo, o contribuinte pode utilizar a precisão 
                    //dejada (0-10 decimais). Para efeitos de cálculo, o valor unitário será obtido pela divisão do valor do produto pela quantidade comercial
                    string vUnCom = Convert.ToDecimal(item.VALORUNITARIO).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                    //10 - Valor Total Bruto dos Produtos ou Serviços
                    //tamanho 15 - Obrigatorio
                    string vProd = Convert.ToDecimal(item.VALORTOTAL).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                    //11 - GTIN (Global Trade Item Number) da unidade tributável, antigo código EAN ou código de barras
                    //Preencher com o código GTIN-8, GTIN-12, GTIN-13 ou GTIN-14 (antigos códigos EAN, UPC e DUN-14) da unidade tributável do produto, 
                    //não informar em caso de o produto não possuir este código.
                    //Tamanho: 14 Obrigatorio
                    string cEANTrib = Util.LimiterText(PRODUTOSTy.CODBARRA,14);

                    //12 - Unidade Tributável
                    //tamanho : 06 
                    string uTrib = uCom;

                    //13 - Quantidade Tributável
                    //tamanho: 15 Obrigatorio
                    string qTrib = qCom;

                    //14 - Valor Unitário de tributação
                    //Informar o valor unitário de tributação do produto, campo meramente informativo, o contribuinte pode utilizar a precisão 
                    //desejada (0-10 decimais). Para efeitos de cálculo, o valor unitário será obtido pela divisão do valor do produto pela quantidade tributável.
                    string vUnTrib = vUnCom;

                    //15 - Valor Total do Frete
                    //Tamanho: 15 - Opcional
                    string vFrete = "0.00";

                    //16 - Valor Total do Seguro
                    //Tamanho: 15 - Opcional
                    string vSeg = "0.00";

                    //17 - Valor do Desconto	
                    //Tamanho: 15 - Opcional
                    string vDesc = "0.00";

                    //18- Outras despesas acessórias
                    //Tamanho: 15 - Opcional
                    string vOutro = "0.00";

                    //19 -Indica se valor do Item (vProd) entra no valor total da NF-e (vProd)
                    //Este campo deverá ser preenchido com: 
                    //0 – o valor do item(vProd) não compõe o valor total da NF-e(vProd)
                    //1 – o valor do item(vProd) compõe o valor total da NF - e(vProd)
                    string indTot = "1";

                    //20 - Número do pedido de compra
                    //Informação de interesse do emissor para controle.
                    //Tamannho: 15 Opciaonal
                    string xPed = "";

                    //21 - Item do pedido de compra
                    //Tamanho 6 - Opicional
                    string nItemPed = "";

                    //22 - Número de controle da FCI - Ficha de Conteúdo de Importação
                    //Código Informação relacionada com a Resolução 13/2012 do Senado Federal. 
                    //Formato: Algarismos, letras maiúsculas de "A" a "F" e o caractere hífen. Exemplo: B01F70AF-10BF-4B1F-848C- 65FF57F616FE
                    //Tamanho: 36 Opcional.
                    string nFCI = "";

                    //23 - Código Especificador da Subst. Tributária - CEST
                    //código Especificador da Substituição Tributária - CEST, que estabelece a sistemática de uniformização e identificação das mercadorias e bens passíveis de sujeição aos regimes de substituição
                    //tributária e de antecipação de recolhimento do ICMS.
                    //TamanhO; 7
                    string CEST = PRODUTOSTy.CEST;

                    escrever.WriteLine("I|" + cProd + "|" + cEAN + "|" + xProd + "|" + NCM + "|" + EXTIPI + "|" + CFOP + "|" + uCom + "|" + qCom + "|" +
                                              vUnCom + "|" + vProd + "|" + cEANTrib + "|" + uTrib + "|" + qTrib + "|" + vUnTrib + "|" + vFrete + "|" +
                                              vSeg + "|" + vDesc + "|" + vOutro + "|" + indTot + "|" + xPed + "|" + nItemPed + "|" + nFCI +"|" + CEST +"|");


                    /////Inicio - Grupo de Tributos incidentes no Produto ou Serviço\\\
                    string vTotTrib = Convert.ToDecimal(item.VLTRIBUTOAPROX).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));//1 -Valor aproximado total de tributosfederais, estaduais e municipais
                    escrever.WriteLine("M|" + vTotTrib  + "|");
                    /////FIM - Grupo de Tributos incidentes no Produto ou Serviço\\\

                   
                    CSTEntity CSTNFeTy = new CSTEntity();
                    CSTProvider CSTP = new CSTProvider();                    
                    CSTNFeTy = CSTP.Read(Convert.ToInt32(item.IDCST));

                    if(CSTNFeTy == null)
                        MessageBox.Show("CST do produto : " + item.NOMEPRODUTO + " não selecionado!");

                    //Busca Origem da Mercadoria
                    ORIGEMMERCADORIAEntity ORIGEMMERCADORIANFeTy = new ORIGEMMERCADORIAEntity();
                    ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                    ORIGEMMERCADORIANFeTy = ORIGEMMERCADORIAP.Read(Convert.ToInt32(CSTNFeTy.IDORIGEM));

                    string orig = "";//1 - Origem da mercadoria
                    string CST = ""; //2 - Tributação do ICMS
                    string modBC = "";//3 - Modalidade de determinação da BC do ICMS //0 - Margem Valor Agregado (%);1 - Pauta (Valor);2 - Preço Tabelado Máx. (valor);3 - valor da operação.
                    string vBC = "";// 4 - Valor da BC do ICMS
                    string pICMS = "";// 5 - Alíquota do imposto
                    string vICMS = "";// 6 - Valor do ICMS

                    ////CST - 00 - Tributada integralmente\\\\\\\
                    if (CSTNFeTy.CODIGO == "00")
                    {
                        //ICMS Normal e ST
                        escrever.WriteLine("N|");

                        //1 - Origem da mercadoria	
                        //Origem da mercadoria:
                        //tamanho 1 - Obrigatorio
                       // 0 - Nacional, exceto as indicadas nos códigos 3 a 5;
                       // 1 - Estrangeira - Importação direta, exceto a indicada no código 6;
                       // 2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7;
                       // 3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40 %;
                       // 4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes;
                       // 5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40 %;
                       // 6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX;
                       // 7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista da CAMEX.
                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria

                        //2 - Tributação do ICMS
                        //Tributação do ICMS: 00 - Tributada integralmente.
                        //Tamanho 2 - obrigatorio
                        CST = CSTNFeTy.CODIGO ;

                        //3 - Modalidade  de  determinação  da  BC do ICMS
                        //0 - Margem Valor Agregado (%);
                        //1 - Pauta(Valor);
                        //2 - Preço Tabelado Máx. (valor);
                        //3 - valor da operação
                        //tamanho: 1 - Obriagtorio
                        modBC = "3";

                        //4 - Valor da BC do ICMS
                        //tamanho 15 Obrigatorio
                        vBC = Convert.ToDecimal(item.BASEICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// 4 - Valor da BC do ICMS

                        //5 - Alíquota do imposto
                        //Tamanho 5 - Obriagotiro
                        pICMS = Convert.ToDecimal(item.ALICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// 5 - Alíquota do imposto

                        //6 - Valor do ICMS
                        //Tamanho 15 - Obriagorio
                        vICMS = Convert.ToDecimal(item.VALORICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// 6 - Valor do ICMS

                        escrever.WriteLine("N02|" + orig + "|" + CST + "|" + modBC + "|" + vBC + "|" + pICMS + "|" + vICMS + "|");
                    }
                    ////CST - 00 - Tributada integralmente\\\\\\\

                    /////////////Inicio - CST - 10 - Tributada e com cobrança do ICMS por substituição tributária\\\\\\\\\\\
                    if (CSTNFeTy.CODIGO == "10")
                    {
                        escrever.WriteLine("N|");

                        //1 - Origem da mercadoria
                        //Origem da mercadoria:
                        //0 - Nacional, exceto as indicadas nos códigos 3 a 5;
                        //1 - Estrangeira - Importação direta, exceto a indicada no código 6;
                        //2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7;
                        //3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40 %;
                        //4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes;
                        //5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40 %;
                        //6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX;
                        //7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista da CAMEX.
                        //Tamanho: 1 Obrigatorio
                        orig = ORIGEMMERCADORIANFeTy.CODIGO;

                        //2 - Tributação do ICMS
                        //Tributação pelo ICMS 10 - Tributada e com cobrança do ICMS por substituição tributária
                        //tamanho: 2 - Obrigatorio
                        CST = CSTNFeTy.CODIGO;

                        //3 - Modalidade  de  determinação  da  BC  do ICMS
                        //0 - Margem Valor Agregado (%);
                        //1 - Pauta(Valor);
                        //2 - Preço Tabelado Máx. (valor);
                        //3 - valor da operação.
                        modBC = "3";

                        //4 - Valor da BC do ICMS
                        //tamanho 15 Obrigatorio
                        vBC = Convert.ToDecimal(item.BASEICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                        //5 - Alíquota do imposto
                        //tamanho 15 Obrigatorio
                        pICMS = Convert.ToDecimal(item.ALICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                        //6 - Valor do ICMS
                        //tamanho 15 Obrigatorio
                        vICMS = Convert.ToDecimal(item.VALORICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                        //7 - Modalidade  de  determinação  da  BC  do ICMS ST
                        //0 - Preço tabelado ou máximo  sugerido;
                        //1 - Lista Negativa(valor);
                        //2 - Lista Positiva(valor);
                        //3 - Lista Neutra(valor);
                        //4 - Margem Valor Agregado(%);
                        //5 - Pauta(valor);
                        string modBCST = "4";

                        //8 - Percentual da margem de valor Adicionado do ICMS ST
                        //tamanho 5 - opcional
                        string pMVAST = Convert.ToDecimal(LIS_PRODUTOSColl[0].MARGEMLUCRO).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                        //9 - Percentual da Redução de BC do ICMS ST
                        //tamanho 5 - opcional
                        string pRedBCST = "0.00"; //9 Valor da BC do ICMS ST

                        //10 - Valor da BC do ICMS ST
                        //tamanho 15 - opcional
                        string vBCST = CalculoBaseST(Convert.ToInt32(item.IDPRODUTO),Convert.ToDecimal(item.VALORTOTAL), 0,0).ToString(); //10 - Valor da BC do ICMS ST
                        vBCST = Convert.ToDecimal(vBCST).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                        //11 - Alíquota do imposto do ICMS ST
                        //tamanho 5 - opcional
                        string pICMSST =Convert.ToDecimal(LIS_PRODUTOSColl[0].ICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// 11 - Alíquota do imposto do ICMS ST

                        //12 - Valor do ICMS ST
                        //Valor do ICMS ST retido
                        //Valor do ICMS ST retido
                        string vICMSST = CalculoICMSST(Convert.ToInt32(item.IDPRODUTO),Convert.ToDecimal(item.VALORTOTAL), 0,0).ToString();// 12 - Valor do ICMS ST

                        escrever.WriteLine("N03|" + orig + "|" + CST + "|" + modBC + "|" + vBC + "|" + pICMS + "|" + vICMS + "|" + modBCST + "|" + pMVAST + "|" + pRedBCST + "|" +
                                                  vBCST + "|" + pICMSST + "|" +vICMSST + "|") ;
                    }
                    /////////////FIM - CST - 10 - Tributada e com cobrança do ICMS por substituição tributária\\\\\\\\\\\

                    /////////////INICIO - CST - 20 - Com redução de base de cálculo \\\\\\\\\\\\\\\\\\\\
                    if (CSTNFeTy.CODIGO == "20")
                    {
                        escrever.WriteLine("N|");

                        //1 - Origem da mercadoria
                        //Origem da mercadoria:
                        //0 - Nacional, exceto as indicadas nos códigos 3 a 5;
                        //1 - Estrangeira - Importação direta, exceto a indicada no código 6;
                        //2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7;
                        //3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40 %;
                        //4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes;
                        //5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40 %;
                        //6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX;
                        //7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista da CAMEX.
                         orig = ORIGEMMERCADORIANFeTy.CODIGO;

                        //2 - Tributação do ICMS
                        //Tributação pelo ICMS 20 - Com redução de base de cálculo
                        CST = CSTNFeTy.CODIGO;

                        //3 - Modalidade  de  determinação  da  BC  do ICMS
                        //0 - Margem Valor Agregado (%);
                        //1 - Pauta(Valor);
                        //2 - Preço Tabelado Máx. (valor);
                        //3 - valor da operação
                        modBC = "3";

                        //4-Percentual da Redução de BC
                        string pRedBC = "0.00";

                        //5 - Valor da BC do ICMS
                        vBC = "0.00";

                        //6 - Alíquota do imposto
                        pICMS = "0.00";

                        //7 - Valor do ICMS
                        vICMS = "0.00";// 7 - Valor do ICMS

                        //8 - Valor do ICMS desonerado
                        //Informar apenas nos motivos de desoneração documentados abaixo.
                        string vICMSDeson = "0.00";

                        //9Motivo da desoneração do ICMS
                        //Campo será preenchido quando o campo anterior estiver preenchido. Informar o motivo da desoneração:
                        //3 = Uso na agropecuária;
                        //9 = Outros;
                        //12 = Órgão de fomento e desenvolvimento agropecuári
                        string motDesICMS = "";// 8 -Motivo da desoneração do ICMS

                        escrever.WriteLine("N04|" + orig + "|" + CST + "|" + modBC + "|" + pRedBC + "|" + vBC + "|" +pICMS + "|" + vICMS + "|" + vICMSDeson + 
                                              "|" + motDesICMS);
                    }
                    /////////////FIM - CST - 20 - Com redução de base de cálculo \\\\\\\\\\\\\\\\\\\\

                    /////////////INICIO - CST - 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributáriao \\\\\\\\\\\\\\\\\\\\
                    if (CSTNFeTy.CODIGO == "30")
                    {
                        escrever.WriteLine("N|");

                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        CST = CSTNFeTy.CODIGO; //2 - Tributação do ICMS

                        //Modalidade  de  determinação  da  BC  do ICMS ST
                        //0 - Preço tabelado ou máximo  sugerido;
                        //1 - Lista Negativa(valor);
                        //2 - Lista Positiva(valor);
                        // 3 - Lista Neutra(valor);
                        //4 - Margem Valor Agregado(%);
                        //5 - Pauta(valor);
                        string modBCST = "4";

                        string pMVAST = "0.00";// Percentual da margem de valor Adicionado do ICMS ST
                        string pRedBCST = "0.00";//Percentual da Redução de BC do ICMS ST
                        string vBCST = "0.00";//Valor da BC do ICMS ST
                        string pICMSST = "0";//Alíquota do imposto do ICMS ST
                        string vICMSST = "0.00";//Valor do ICMS ST
                        string vICMSDeson = "0.00";//Valor do ICMS desonerado

                        //Motivo da desoneração do ICMS
                        //Campo será preenchido quando o campo anterior estiver preenchido.Informar o motivo da desoneração:
                        //6 = Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio(Resolução 714 / 88 e 790 / 94 – CONTRAN e suas alterações);
                        //7 = SUFRAMA;
                       //9 = Outros;
                        string motDesICMS = "9";
                        escrever.WriteLine("N05|" + orig + "|" + CST + "|" + modBCST + "|" + pMVAST + "|" + pRedBCST + "|" + vBCST + "|" + pICMSST + "|"  + vICMSST + "|"+ vICMSDeson + "|");
                    }
                    /////////////FIM - CST - 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária \\\\\\\\\\\\\\\\\\\\

                    /////////////INICIO - CST - 40 - Isenta, 41 - Não tributada e 50 - Suspensãoo \\\\\\\\\\\\\\\\\\\\
                    if (CSTNFeTy.CODIGO == "40")
                    {
                        escrever.WriteLine("N|");

                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        CST = CSTNFeTy.CODIGO ; //2 - Tributação do ICMS
                        vICMS = Convert.ToDecimal(item.VALORICMS).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// 3 - Valor do ICMS
                        string motDesICMS = "9";// 4  - Motivo da desoneração do ICMS
                        escrever.WriteLine("N06|" + orig + "|" + CST + "|" + vICMS + "|" + motDesICMS + "|");
                    }
                    /////////////FIM - CST - 40 - Isenta, 41 - Não tributada e 50 - Suspensão \\\\\\\\\\\\\\\\\\\\

                     ////CST - 60 - ICMS cobrado anteriormente por substituição tributária\\\\\\
                    if (CSTNFeTy.CODIGO == "60")
                    {
                        escrever.WriteLine("N|");


                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        CST = CSTNFeTy.CODIGO ; //2 - Tributação do ICMS
                        string vBCSTRet = "0.00";//3 - Valor da BC do ICMS ST retido
                        string vICMSSTRet = "0.00";//4 -Valor do ICMS ST retido
                        
                        escrever.WriteLine("N08|" + orig + "|" + CST + "|" + vBCSTRet + "|" + vICMSSTRet + "|");
                    }
                    ////CST - 60 - ICMS cobrado anteriormente por substituição tributária\\\\\\\

                    ////CST - 90 - Outros\\\\\\
                    if (CSTNFeTy.CODIGO == "90")
                    {
                        escrever.WriteLine("N|");


                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        CST = CSTNFeTy.CODIGO; //2 - Tributação do ICMS
                        modBC = "3";//3 - Modalidade  de  determinação da BC do ICMS
                        string pRedBC = "0";//4 - Percentual da Redução de BC
                        vBC = "0.00";//5 - Valor da BC do ICMS
                        pICMS = "0";//6 - Alíquota do imposto
                        vICMS = "0.00";//7 - Valor do ICMS

                        //8 - Modalidade  de  determinação da BC do ICMS ST
                        //0 - Preço tabelado ou máximo sugerido;
                        //1 - Lista Negativa(valor);
                        //2 - Lista Positiva(valor);
                        //3 - Lista Neutra(valor);
                        //4 - Margem Valor Agregado(%);
                        //5 - Pauta(valor);
                        string modBCST = "4";

                      //  escrever.WriteLine("N10|" + orig + "|" + CST + "|" + vBCSTRet + "|" + vICMSSTRet + "|");
                    }
                    ////CST - 90 - Outros\\\\\\

                    ////INICIO CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=101\\\\\\
                    if (CSTNFeTy.CODIGO == "101")
                    {
                        escrever.WriteLine("N|");

                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        string CSOSN = CSTNFeTy.CODIGO ; //2 - Tributação do ICMS
                        string pCredSN = "0.00";//3 - VAlíquota aplicável de cálculo do crédito (Simples Nacional).
                        string vCredICMSN = "0.00";//4 -Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
                        
                        escrever.WriteLine("N10c|" + orig + "|" + CSOSN + "|" + pCredSN + "|" + vCredICMSN + "|");
                    }
                  ////FIM - CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=101\\\\\\

                  ////INICIO CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=102, 103, 300 ou 400\\\\\\
                    if (CSTNFeTy.CODIGO == "102" || CSTNFeTy.CODIGO == "103" || CSTNFeTy.CODIGO == "300" || CSTNFeTy.CODIGO == "400" )
                    {
                        escrever.WriteLine("N|");

                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        string CSOSN = CSTNFeTy.CODIGO ; //2 - Tributação do ICMS                      
                        
                        escrever.WriteLine("N10d|" + orig + "|" + CSOSN + "|" );
                    }
                 ////FIM CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=102, 103, 300 ou 400\\\\\\

                     ////INICIO CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=201\\\\\\
                    if (CSTNFeTy.CODIGO == "201"  )
                    {
                        escrever.WriteLine("N|");

                        orig = ORIGEMMERCADORIANFeTy.CODIGO;//1 - Origem da mercadoria
                        string CSOSN = CSTNFeTy.CODIGO ; //2 - Tributação do ICMS                      
                        string modBCST = "0.00";//3 - Modalidade de determinação da BC do ICMS ST
                        string pMVAST = "0.00";//4 -Percentual da margem de valor Adicionado do ICMS ST
                        string pRedBCST = "0.00";//5 - Percentual da Redução de BC do ICMS ST
                        string vBCST = "0.00";// 6 - Valor da BC do ICMS ST
                        string pICMSST = "0.00";//7 - Alíquota do imposto do ICMS ST
                        string vICMSST = "0.00";//8 - Valor do ICMS ST
                        string pCredSN = "0.00";//9 - Alíquota aplicável de cálculo do crédito (SIMPLES NACIONAL).
                        string vCredICMSSN = "0.00";//10 - Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (SIMPLES NACIONAL)
                        escrever.WriteLine("N10e|" + orig + "|" + CSOSN + "|" + modBCST + "|" + pMVAST + "|" + pRedBCST+ "|" + vBCST + "|" + pICMSST + "|" +vICMSST + "|" +
                                                     pCredSN + "|" +vCredICMSSN + "|");
                    }
                 ////FIM CST - Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=201\\\\\\

                    //Grupo PIS
                    escrever.WriteLine("Q|");
                    // PIS - grupo de PIS não tributado
                    // string CST_Q03 = Util.LimiterText(LIS_PRODUTOSColl[0].CSTPIS, 2);//1 - Código de Situação Tributária do PIS
                    //string vBC_Q03 = "0.00"; //2 - Valor da Base de Cálculo do PIS
                    //string pPIS_Q03 = "0.00";// 3 -Alíquota do PIS (em percentual)
                    //string vPIS_Q03 = "0.00";// 4 -Valor do PIS
                    // escrever.WriteLine("Q03|" + CST_Q03 + "|" + vBC_Q03 + "|" + pPIS_Q03 + "|" + vPIS_Q03 + "|");

                    ////PIS - INICIO  - grupo de PIS não tributado/////
                    //1 Código de Situação Tributária do PIS
                    //04 - Operação Tributável (tributação monofásica (alíquota zero));
                    //05 - Operação Tributável(Substituição Tributária);
                    //06 - Operação Tributável(alíquota zero);
                    //07 - Operação Isenta da Contribuição;
                    //08 - Operação Sem Incidência da Contribuição;
                    //09 - Operação com Suspensão da Contribuiçã
                    CST = "07";
                    escrever.WriteLine("Q04|" + CST + "|");

                    ////PIS  - FIM - grupo de PIS não tributado/////


                    //Grupo COFINS
                    escrever.WriteLine("S|");

                    //COFINS - COFINS - grupo de COFINS não tributado
                    //1 - Código de Situação Tributária do COFINS
                    //04 - Operação Tributável(tributação monofásica(alíquota zero));
                    //05 - Operação Tributável(Substituição Tributária);
                    //06 - Operação Tributável(alíquota zero);
                    //07 - Operação Isenta da Contribuição;
                    //08 - Operação Sem Incidência da Contribuição;
                    //09 - Operação com Suspensão da Contribuição
                    CST = "07";
                    escrever.WriteLine("S04|" + CST + "|");


                    i++;
                }
                ///////////////////////FIM - Produtos da NFC-e\\\\\\\\\\\\\\\\\\\

                
                
                //////////////////////INICIO - Valores Totais da NF-e\\\\\\\\\\\\\\\\\\\
               // Valores Totais da NF-e
                escrever.WriteLine("W|");

                //1 - Base de Cálculo do ICMS;
                string vBC_W02 =  SomaValorTotalBaseICMS(Convert.ToInt32(CUPOMELETRONICOTy.CUPOMELETRONICOID)).ToString("n2");
                vBC_W02 = Convert.ToDecimal(vBC_W02).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                //2 - Valor Total do ICMS
                string vICMS_W02 = SomaValorTotalICMS(Convert.ToInt32(CUPOMELETRONICOTy.CUPOMELETRONICOID)).ToString("n2");
                vICMS_W02 = Convert.ToDecimal( vICMS_W02).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                string vBCST_W02 = "0.00";//3 - Base de Cálculo do ICMS ST
                string vST = "0.00"; // 4 - Valor Total do ICMS ST;

                // 5 - Valor Total dos produtos e serviços
                string vProd_W02 = SomaValorTotalProduto(Convert.ToInt32(CUPOMELETRONICOTy.CUPOMELETRONICOID)).ToString("n2");
                vProd_W02 = Convert.ToDecimal( vProd_W02).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                // 6 - Valor Total do Frete
                string vFrete_W02 = "0.00";

                //7 - Valor Total do Seguro
                string vSeg_W02 = "0.00";

                //8  - Valor Total do Desconto
                string vDesc_W02 =  Convert.ToDecimal(CUPOMELETRONICOTy.VALORDESCONTO).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                // 9 - Valor Total do II
                string vII = "0.00";

                // 10 - Valor Total do IPI
                string vIPI = "0.00";

                // 11  - Valor do PIS
                string vPIS = "0.00";

                // 12 - Valor do COFINS
                string vCOFINS = "0.00";

                // 13 - Outras Despesas acessórias
                string vOutro_W02 = "0.00";

                // 14 - Valor Total da NF-e
                string vNF =  Convert.ToDecimal(CUPOMELETRONICOTy.TOTALNOTA).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                // 15 - Valor aproximado total de tributosfederais, estaduais e municipais
                string vTotTrib_W02 = SomaValorTotalTributos(CUPOMELETRONICOTy.CUPOMELETRONICOID).ToString("n2"); 
                vTotTrib_W02 =  Convert.ToDecimal(vTotTrib_W02).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                // 16 - Valor Total do ICMS desonerado
                string vICMSDeson_W02 = "0.00"; 

                escrever.WriteLine("W02|" + vBC_W02 + "|" +vICMS_W02+ "|" + vBCST_W02 + "|" +vST + "|" + vProd_W02 + "|" + vFrete_W02 + "|" + vSeg_W02 + "|"
                                          +  vDesc_W02 + "|" + vII + "|" + vIPI + "|" + vPIS + "|" + vCOFINS + "|" + vOutro_W02 + "|" + vNF + "|"
                                          + vTotTrib_W02 + "|" + vICMSDeson_W02 + "|");
                ///////////////////////FIM - Valores Totais da NF-e\\\\\\\\\\\\\\\\\\\

                 ///////////////////////INICIO - Formas de Pagamento\\\\\\\\\\\\\\\\\\\              

                //Informações do Transporte da NF-e
                string modFrete = "9"; //1 Modalidade do frete - 9- Sem frete.
                escrever.WriteLine("X|" + modFrete + "|");
                 
                //Dados da Cobrança
                //01=Dinheiro02=Cheque03=Cartão de Crédito04=Cartão de Débito05=Crédito Loja10=Vale Alimentação11=Vale Refeição12=Vale Presente13=Vale Combustível99=Outros
                escrever.WriteLine("Y|");
                string vPag = ""; //2 - Valor do Pagamento     
              
                if (Convert.ToDecimal(txtValoPagoDinheiro.Text) > 0)  // 01=Dinheiro    
                {
                    // vPag =  Convert.ToDecimal(txtValoPagoDinheiro.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    vPag = Convert.ToDecimal(txtTotalVenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    escrever.WriteLine("YA01|01|" + vPag + "|");
                }

                if (Convert.ToDecimal(txtValorPagoCheque.Text) > 0)  // 02=Cheque    
                {
                    //vPag = Convert.ToDecimal(txtValorPagoCheque.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    vPag = Convert.ToDecimal(txtTotalVenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    escrever.WriteLine("YA01|02|" + vPag + "|");
                }

                if (Convert.ToDecimal(txtValoPagoCartaoCredito.Text) > 0)  // 03=Cartão de Crédito  
                {
                    //vPag = Convert.ToDecimal(txtValoPagoCartaoCredito.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    vPag = Convert.ToDecimal(txtTotalVenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    escrever.WriteLine("YA01|03|" + vPag + "|");
                }

                if (Convert.ToDecimal(txtValoPagoCartãoDebito.Text) > 0)  // 04=Cartão de Crédito    
                {
                    // vPag = Convert.ToDecimal(txtValoPagoCartãoDebito.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    vPag = Convert.ToDecimal(txtTotalVenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    escrever.WriteLine("YA01|04|" + vPag + "|");
                }

                if (Convert.ToDecimal(txtValoPagoValeRefeicao.Text) > 0)  // 11=Vale Refeição 
                {
                    //   vPag = Convert.ToDecimal(txtValoPagoValeRefeicao.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento
                    vPag = Convert.ToDecimal(txtTotalVenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento 
                    escrever.WriteLine("YA01|11|" + vPag + "|");
                }

                if (Convert.ToDecimal(txtValoPagoOutros.Text) > 0)  // 99=outros
                {
                    vPag = Convert.ToDecimal(txtValoPagoOutros.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ; //2 - Valor do Pagamento
                    escrever.WriteLine("YA01|99|" + vPag + "|");
                }

                ///////////////////////FIM - Formas de Pagamento\\\\\\\\\\\\\\\\\\\
               
                 if ((Convert.ToDecimal(txtValoPagoCartaoCredito.Text) > 0 || Convert.ToDecimal(txtValoPagoCartãoDebito.Text) > 0))
                {
                    ///////////////////////INICIO - Grupo de Cartões\\\\\\\\\\\\\\\\\\\
                    string CNPJ_YA04 = ""; // 1 - Informar o CNPJ da Credenciadora de cartão de crédito /débito

                    string tBand = ""; // 2 - Bandeira da operadora de cartão de crédito e/ou débito //01=Visa 02=Mastercard 03=American Express 04=Sorocred 99=Outros
                    if (cboperadoracartao.SelectedIndex == 0)
                    {
                        tBand = "01"; // Visa
                        CNPJ_YA04 = BmsSoftware.ConfigNFCe.Default.CNPJCartaoVisa;
                    }
                    else if (cboperadoracartao.SelectedIndex == 1)
                    {
                        tBand = "02"; //Mastercard
                        CNPJ_YA04 = BmsSoftware.ConfigNFCe.Default.CNPJCartaoMarterCard;
                    }
                    else if (cboperadoracartao.SelectedIndex == 2)
                    {
                        tBand = "03"; //American Express
                        CNPJ_YA04 = BmsSoftware.ConfigNFCe.Default.CNPJCartaoAmericanExpress;
                    }
                    else if (cboperadoracartao.SelectedIndex == 3)
                    {
                        tBand = "04"; //Sorocred
                        CNPJ_YA04 = BmsSoftware.ConfigNFCe.Default.CNPJCartaoSorocred;
                    }
                    else if (cboperadoracartao.SelectedIndex == 4)
                    {
                        tBand = "99"; //Outros
                        CNPJ_YA04 = BmsSoftware.ConfigNFCe.Default.CNPJCartaoOutros;
                    }

                    string cAut = txtNumeroAutorizaCartao.Text;//3 - Número de autorização da operação cartão de crédito e/ou débito
                    escrever.WriteLine("YA04|" + CNPJ_YA04 + "|" + vPag + "|" + cAut + "|");
                    ///////////////////////FIM INICIO - Grupo de Cartões\\\\\\\\\\\\\\\\\\\
                }

               // Informações Adicionais da NF-e
                 string infAdFisco = "";//Informações Adicionais de Interesse do Fisco
                 string infCpl = "";//Informações Complementares de interesse do Contribuinte

                //Informações sobre o troco
                if (Convert.ToDecimal(txtTroco.Text) > 0)
                    infCpl = "Troco: " + txtTroco.Text;

                 escrever.WriteLine("Z|" + infAdFisco + "|" + infCpl + " Vendedor: "+ cbFuncionario.Text +" |");

                escrever.Close();

                VerificaSituacaodoArquivo(NumeroNota);

                Application.DoEvents();
                this.Text = "Fechar Venda";
            }
            catch (Exception ex)
            {
                escrever.Close();
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public string CriarChaveNfei(int _codUf, string _anoMesEmissao, string _cnpjEmitente, string _modelo, int _serie, int _numero, long _codigoNumerico)
        {
            // uf 2 caracteres           Ex: SP          35
            // periodo 4 caracteres      Ex: Set/20011   1109 
            // cnpj 14                   Ex: CNPJ        08951491000168
            // modelo 2                  Ex: NFe         55
            // serie 3                   Ex: Serie 1     001
            // numero 9                  Ex: NFe 1101    000001101
            // tpEmis 1                  Ex: 1
            // codigo numero 8           Ex: CodNumérico 75345491
            // dv 1                      Ex: Dig.Veriif  calculado...

            // Ex: chave = 3511090895149100016855001000001101
            string chave = _codUf.ToString() + _anoMesEmissao + _cnpjEmitente + _modelo.PadLeft(2, '0') + _serie.ToString().PadLeft(3, '0') + _numero.ToString().PadLeft(9, '0');

            // chave += "1" + _codigoNumerico.ToString().PadLeft(8, '0'); // Ex: CodNumérico: 75345491 
            // Chave = 3511090895149100016855001000001101175345491
            //string dig = HelperAcme.Modulo11(chave);

            // chave += dig;
            return "NFe" + chave; // Ex: Resultado: 35110908951491000168550010000011011753454916
        }

        private void VerificaSituacaodoArquivo(string NumeroNota)
        {
            try
            {
                Boolean arquivoSelec = false;
                string arquivo = @BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Processados\nsConcluido\NFCe_" + NumeroNota + ".txt";
                //

                //100 Autorizado o uso da NF-e
                //101 Cancelamento de NF-e homologado
                //102 Inutilização de número homologado
                //103 Lote recebido com sucesso
                //104 Lote processado
                //105 Lote em processamento
                //106 Lote não localizado
                //107 Serviço em Operação
                //108 Serviço Paralisado Momentaneamente (curto prazo)
                //109 Serviço Paralisado sem Previsão
                //110 Uso Denegado
                //111 Consulta cadastro com uma ocorrência
                //112 Consulta cadastro com mais de uma ocorrência

                //aguarda para verificar se o arquivo foi enviado com sucesso
                int i = 0;
                do
                {
                    System.Threading.Thread.Sleep(1000); //Aguarda 1 segundos
                    i++;

                    this.Text = "Aguarde... Enviando Cupom NFCe: ( "+ i +" Segundos )";
                    Application.DoEvents();

                    if (i > 30)
                        break;
                }
                while (!File.Exists(arquivo));

                if (File.Exists(arquivo))
                {
                    arquivoSelec = true;
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (linha.IndexOf("Autorizado o uso da NF-e") != -1)//Autorizado o uso da NF-e, autorizacao fora de prazo
                            {
                                CUPOMELETRONICOTy.PROTOCOLO = Util.RetiraLetras(linha.Substring(3, 16));
                                CUPOMELETRONICOTy.IDSTATUSNFCE = 1;//Enviado
                                CUPOMELETRONICOTy.CHAVEACESSO = Util.RetiraLetras(linha.Substring(78, 46));
                                CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                                arquivoSelec = true;
                                this.Close();
                            }
                            else if (linha.IndexOf("contingencia") != -1)////RP|||OFFLINE|Emitido em contingencia offline|NFe43141207364617000135650000000016909000016909
                            {
                                CUPOMELETRONICOTy.IDSTATUSNFCE = 6;//Contigencia
                                CUPOMELETRONICOTy.CHAVEACESSO = Util.RetiraLetras(linha.Substring(46, 46));
                                CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                                arquivoSelec = true;
                                this.Close();
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Cupom Não Enviado, Tente Novamente!");

                if(!arquivoSelec)
                    MessageBox.Show("Cupom Não Enviado, Tente Novamente!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal CalculoBaseST(int CodProduto, decimal valorTotal, decimal ValorFrete, decimal ValorOutros)
        {
            decimal result = 0;

            try
            {
                PRODUTOSEntity PRODUTOS_Calc_ST = new PRODUTOSEntity();
                PRODUTOS_Calc_ST = PRODUTOSP.Read(CodProduto);
                if (PRODUTOS_Calc_ST != null && PRODUTOS_Calc_ST.FLAGICMSST.Trim() == "S")
                {

                    //ICMS da operação própria
                    decimal ICMS = (valorTotal * Convert.ToDecimal(PRODUTOS_Calc_ST.ICMS)) / 100;

                    //BCST = (Valor mercadoria + frete + IPI + outras despesas) + margem de lucro
                    decimal ValorIPI = ((valorTotal * Convert.ToDecimal(PRODUTOS_Calc_ST.IPI)) / 100);
                    decimal MargemLucro = Convert.ToDecimal(PRODUTOS_Calc_ST.PORCMARGEMLUCRO);
                    decimal TotalGeralST = valorTotal + ValorFrete + ValorIPI + ValorOutros;
                    TotalGeralST = TotalGeralST + ((TotalGeralST * MargemLucro) / 100);
                    result = TotalGeralST;

                    return result;
                }
                else
                {
                    result = 0;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                MessageBox.Show("Erro no cálculo do ICMS ST");
                return result;
            }
        }

        private decimal CalculoICMSST(int CodProduto, decimal valorTotal, decimal ValorFrete, decimal ValorOutros)
        {
            decimal result = 0;

            try
            {
                PRODUTOSEntity PRODUTOS_Calc_ST = new PRODUTOSEntity();
                PRODUTOS_Calc_ST = PRODUTOSP.Read(CodProduto);
                if (PRODUTOS_Calc_ST != null && PRODUTOS_Calc_ST.FLAGICMSST.Trim() == "S")
                {

                    //ICMS da operação própria
                    decimal ICMS = (valorTotal * Convert.ToDecimal(PRODUTOS_Calc_ST.ICMS)) / 100;

                    //BCST = (Valor mercadoria + frete + IPI + outras despesas) + margem de lucro
                    decimal ValorIPI = ((valorTotal * Convert.ToDecimal(PRODUTOS_Calc_ST.IPI)) / 100);
                    decimal MargemLucro = Convert.ToDecimal(PRODUTOS_Calc_ST.PORCMARGEMLUCRO);
                    decimal TotalGeralST = valorTotal + ValorFrete + ValorIPI + ValorOutros;
                    TotalGeralST = TotalGeralST + ((TotalGeralST * MargemLucro) / 100);                

                    //ICMS ST
                    decimal ICMSST = (Convert.ToDecimal(TotalGeralST) * Convert.ToDecimal(PRODUTOS_Calc_ST.ICMS)) / 100;
                    result = (Convert.ToDecimal(ICMSST) - Convert.ToDecimal(ICMS));

                    return result;
                }
                else
                {
                    result = 0;
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                MessageBox.Show("Erro no cálculo do ICMS ST");
                return result;
            }
        } 
       

        private int BuscarProximoNNFCe()
        {
            int result = 1;

            try
            {
                CUPOMELETRONICOCollection CUPOMELETRONICOColl_Prox = new CUPOMELETRONICOCollection();                ;
                CUPOMELETRONICOColl_Prox = CUPOMELETRONICOP.ReadCollectionByParameter(null, "NUMERONFCE DESC");
                
                if (CUPOMELETRONICOColl_Prox.Count > 0)
                {
                    result = Convert.ToInt32(CUPOMELETRONICOColl_Prox[0].NUMERONFCE) + 1;
                }

                if (BmsSoftware.ConfigNFCe.Default.NInicialNFCe.Trim() != string.Empty)
                {
                    result = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.NInicialNFCe.Trim());

                    //Limpa o numero inicial apos utilizar
                    BmsSoftware.ConfigNFCe.Default.NInicialNFCe = string.Empty;
                    BmsSoftware.ConfigNFCe.Default.Save();
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }  
        

        private decimal SomaValorTotalTributos(int CUPOMELETRONICOID)
        {
            decimal Return = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
            LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

            foreach (var item in LIS_PRODUTONFCEColl)
            {
                Return += Convert.ToDecimal(item.VLTRIBUTOAPROX);
            }

            return Return;
        }


        private decimal SomaValorTotalProduto(int CUPOMELETRONICOID)
        {
            decimal Return = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
            LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

            foreach (var item in LIS_PRODUTONFCEColl)
            {
                Return += Convert.ToDecimal(item.VALORTOTAL);
            }

            return Return;
        }

        private decimal SomaValorTotalBaseICMS(int CUPOMELETRONICOID)
        {
            decimal Return = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
            LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

            foreach (var item in LIS_PRODUTONFCEColl)
            {
                if(item.IDCST != 13) // cst/cson 0102
                    Return += Convert.ToDecimal(item.BASEICMS);
            }

            return Return;
        }

        private decimal SomaValorTotalICMS(int CUPOMELETRONICOID)
        {
            decimal Return = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
            LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

            foreach (var item in LIS_PRODUTONFCEColl)
            {
                if (item.IDCST != 13) // cst/cson 0102
                    Return += Convert.ToDecimal(item.VALORICMS);
            }

            return Return;
        } 

        private int BuscaCodigoCidads(string MUNICIPIO, string UF)
        {
            int result = -1;

            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "collate pt_br like", "%" + MUNICIPIO.Replace("'", "") + "%"));
            RowRelatorio.Add(new RowsFiltro("UF", "System.String", "=", UF));
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_MUNICIPIOSColl.Count > 0)
            {
                result = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
            }

            return result;
        }

        private int BuscaCodigoUF(string UF)
        {
            int uf = -1;
            switch (UF)
            {
                case "AC": uf = 12; break;
                case "AL": uf = 27; break;
                case "AP": uf = 16; break;
                case "AM": uf = 13; break;
                case "BA": uf = 29; break;
                case "CE": uf = 23; break;
                case "DF": uf = 53; break;
                case "GO": uf = 52; break;
                case "MA": uf = 21; break;
                case "MG": uf = 31; break;
                case "ES": uf = 32; break;
                case "MS": uf = 50; break;
                case "MT": uf = 51; break;
                case "PA": uf = 15; break;
                case "PB": uf = 25; break;
                case "PE": uf = 26; break;
                case "PI": uf = 22; break;
                case "PR": uf = 41; break;
                case "RJ": uf = 33; break;
                case "RN": uf = 24; break;
                case "RO": uf = 11; break;
                case "RR": uf = 14; break;
                case "RS": uf = 43; break;
                case "SC": uf = 42; break;
                case "SE": uf = 28; break;
                case "SP": uf = 35; break;
                case "TO": uf = 17; break;
            }

            return uf;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                string MsgErro = "Cliente não Selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) > 1 && !ValidaCliente(Convert.ToInt32(cbCliente.SelectedValue)))
            {
                string MsgErro = "Dados do Cliente Inválido!";
                MessageBox.Show(MsgErro);
                result = false;
            }           
            else if (Convert.ToInt32(cbFuncionario.SelectedValue) < 1)
            {
                string MsgErro = "Vendedor não Selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if(Util.RetiraLetras(txtCPFCNPJ.Text).Length > 0 && Util.RetiraLetras(txtCPFCNPJ.Text).Length  < 12 && !ValidacoesLibrary.ValidaCPF(txtCPFCNPJ.Text) )
            {
                string MsgErro = "CPF Inválido!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (Util.RetiraLetras(txtCPFCNPJ.Text).Length > 0  && Util.RetiraLetras(txtCPFCNPJ.Text).Length > 12 && !ValidacoesLibrary.ValidaCNPJ(txtCPFCNPJ.Text))
            {
                string MsgErro = "CNPJ Inválido!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (Convert.ToDecimal(txtValoPagoCartaoCredito.Text) > 0 || Convert.ToDecimal(txtValoPagoCartãoDebito.Text) > 0)
            {
                if(cboperadoracartao.SelectedIndex < 0)
                {
                    string MsgErro = "Selecione a Operadora de Cartão de Crédito/Débito!";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
               
                if(txtNumeroAutorizaCartao.Text.Trim() == string.Empty)
                {
                    string MsgErro = "Digite o Número de Autorização";
                    MessageBox.Show(MsgErro);

                    (new FrmConfiguarcaoNFCe()).ShowDialog();

                    result = false;
                }

                if(cboperadoracartao.SelectedIndex == 0 &&  BmsSoftware.ConfigNFCe.Default.CNPJCartaoVisa.Trim() == string.Empty)
                {
                    string MsgErro = "Informar o CNPJ da Credenciadora de Cartão de Crédito/Débito! (Visa)";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
                else if (cboperadoracartao.SelectedIndex == 1 && BmsSoftware.ConfigNFCe.Default.CNPJCartaoMarterCard.Trim() == string.Empty)
                {
                    string MsgErro = "Informar o CNPJ da Credenciadora de cartão de Crédito/Débito! (MarterCard)";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
                else if (cboperadoracartao.SelectedIndex == 2 && BmsSoftware.ConfigNFCe.Default.CNPJCartaoAmericanExpress.Trim() == string.Empty)
                {
                    string MsgErro = "Informar o CNPJ da Credenciadora de cartão de crédito /débito! (American Express)";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
                else if (cboperadoracartao.SelectedIndex == 3 && BmsSoftware.ConfigNFCe.Default.CNPJCartaoSorocred.Trim() == string.Empty)
                {
                    string MsgErro = "Informar o CNPJ da Credenciadora de cartão de Crédito/Débito! (Sorocred)";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
                else if (cboperadoracartao.SelectedIndex == 4 && BmsSoftware.ConfigNFCe.Default.CNPJCartaoOutros.Trim() == string.Empty)
                {
                    string MsgErro = "Informar o CNPJ da Credenciadora de cartão de Crédito/Débito! (Outros)";
                    MessageBox.Show(MsgErro);
                    result = false;
                }
            }
            else if (Convert.ToDecimal(txtValoPagoDinheiro.Text) == 0 && Convert.ToDecimal(txtValoPagoCartaoCredito.Text) == 0
                    && Convert.ToDecimal(txtValoPagoValeRefeicao.Text) == 0 && Convert.ToDecimal(txtValorPagoCheque.Text) == 0
                    && Convert.ToDecimal(txtValoPagoCartãoDebito.Text) == 0 && Convert.ToDecimal(txtValoPagoOutros.Text) == 0) 
            {
                string MsgErro = "Informe o Valor Pago!";
                MessageBox.Show(MsgErro);
                txtValoPagoDinheiro.Focus();
                result = false;
            }           
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean ValidaCliente(int IDCLIENTE)
        {
            Boolean Result = true;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_CLIENTEColl.Count > 0)
            {
                if (LIS_CLIENTEColl[0].CPF == "   .   .   -" && LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -")
                {
                    MessageBox.Show("CPF/CNPJ Inválido");
                    Result = false;
                }
                else  if (LIS_CLIENTEColl[0].ENDERECO1.Trim() == string.Empty)
                {
                    MessageBox.Show("Endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].NUMEROENDER.Trim() == string.Empty)
                {
                    MessageBox.Show("Número do endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].BAIRRO1.Trim() == string.Empty)
                {
                    MessageBox.Show("Número do endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].CEP1.Trim() == "     -")
                {
                    MessageBox.Show("CEP Inválido");
                    Result = false;
                }
            }

            return Result;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                    }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmVariosLancamentoReceberNFCe frm = new FrmVariosLancamentoReceberNFCe())
            {
                         
                frm.CodCliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.NumPedido = CUPOMELETRONICOTy.CUPOMELETRONICOID.ToString();
                frm.DataPedido = lblData.Text;
                frm.ValorPedido = txtTotalVenda.Text;
                frm.ShowDialog();
            }          
        }

        private void cbTipo_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void txtValoPagoDinheiro_Validating(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoCartaoCredito_Validating(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValorPagoCheque_Validating(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoCartãoDebito_Validating(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoDinheiro_Leave(object sender, EventArgs e)
        {
           
        }
               
        private void txtValoPagoCartaoCredito_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtValoPagoValeRefeicao_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtValorPagoCheque_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtValoPagoCartãoDebito_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtValoPagoOutros_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtValoPagoValeRefeicao_Validating(object sender, CancelEventArgs e)
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
                    
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValorPagoCheque_Validating_1(object sender, CancelEventArgs e)
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
                   
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoCartãoDebito_Validating_1(object sender, CancelEventArgs e)
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
                    
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoOutros_Validating(object sender, CancelEventArgs e)
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
                    
                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void txtValoPagoDinheiro_Leave_1(object sender, EventArgs e)
        {
            try
            {
                if (Util.RetiraLetras(txtValoPagoDinheiro.Text).Trim() == string.Empty)
                    txtValoPagoDinheiro.Text = "0,00";

                if (Util.RetiraLetras(txtTroco.Text).Trim() == string.Empty)
                    txtTroco.Text = "0,00";

                txtTroco.Text = (Convert.ToDecimal(txtValoPagoDinheiro.Text) - Convert.ToDecimal(txtTotalVenda.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o troco!");
            }
        }

        private void txtCPFCNPJ_Leave(object sender, EventArgs e)
        {
            if (Util.RetiraLetras(txtCPFCNPJ.Text).Length > 0)
            {
                if (Util.RetiraLetras(txtCPFCNPJ.Text).Length < 12 && !ValidacoesLibrary.ValidaCPF(txtCPFCNPJ.Text))
                {
                    string MsgErro = "CPF Inválido!";
                    MessageBox.Show(MsgErro);
                    txtCPFCNPJ.Focus();
                }
                else if (Util.RetiraLetras(txtCPFCNPJ.Text).Length > 12 && !ValidacoesLibrary.ValidaCNPJ(txtCPFCNPJ.Text))
                {
                    string MsgErro = "CNPJ Inválido!";
                    MessageBox.Show(MsgErro);
                    txtCPFCNPJ.Focus();
                }
            }
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            txtCPFCNPJ.Text = string.Empty;
            TxtNomeCliente.Text = string.Empty;

            if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
            {
                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTETy = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue));

                if (CLIENTETy != null &&CLIENTETy.IDCLIENTE > 1)
                {
                    string CPFCNPJ = CLIENTETy.CPF;
                    if (Util.RetiraLetras(CPFCNPJ).Length > 0)
                    {
                        txtCPFCNPJ.Text = Util.RetiraLetras(CPFCNPJ);
                        TxtNomeCliente.Text = CLIENTETy.NOME;
                    }
                    else
                    {
                        CPFCNPJ = CLIENTETy.CNPJ;
                        txtCPFCNPJ.Text = Util.RetiraLetras(CPFCNPJ);
                        TxtNomeCliente.Text = CLIENTETy.NOME;
                    }
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
           
        }
    }
}

 