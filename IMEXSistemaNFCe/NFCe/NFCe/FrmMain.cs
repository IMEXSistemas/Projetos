using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.NFCeServiceClient;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using winfit.Modulos.Operacional;

namespace BmsSoftware
{
    public partial class FrmMain : Form
    {
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
        USUARIOProvider USUARIOP = new USUARIOProvider();
        PRODUTONFCEProvider PRODUTONFCEP = new PRODUTONFCEProvider();
        LIS_CUPOMELETRONICOProvider LIS_CUPOMELETRONICOP = new LIS_CUPOMELETRONICOProvider();
        LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
        STATUSNFCEProvider STATUSNFCEP = new STATUSNFCEProvider();

        private static readonly ILog Log = LogManager.GetLogger(typeof(FrmMain));
        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();

        LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl = new LIS_CUPOMELETRONICOCollection();
        LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();
        string MensagemErroBloqueio = "";

        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        public FrmMain()
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
            // (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            toolStripStatusLabel1.Text = "IMEX Sistemas - F3 Nova Venda - F8 Fechar Venda";
            //lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            //(sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _CUPOMELETRONICOID = -1;
        int? numeronfce = -1;
        public CUPOMELETRONICOEntity Entity
        {
            get
            {
                        
                int idcliente = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCliente);   // integer,
                string SERIE = BmsSoftware.ConfigNFCe.Default.SerieNFCe;
                DateTime dtemissao = DateTime.Now;      // date,
                DateTime dtsaida = DateTime.Now;      // date,
                string horasaida = DateTime.Now.ToString("hh:mm");         // char(5),

                int? idcfop = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP);
                if (PRODUTOSTy.CFOP.Trim() != string.Empty)
                    idcfop = BuscaCFOP(PRODUTOSTy.CFOP);

                int? idstatusnfce = 4;//4 Aberto
                Decimal totalnota = Convert.ToDecimal(txtTotalVenda.Text);         // decimal(15,2),
                int idvendedor =  Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);        // integer,
                Decimal valorpago  = 0;        // numeric(15,2),
                Decimal valordevedor   = 0;    // numeric(15,2),
                Decimal valortroco  = 0;       // numeric(15,2),
                string chaveacesso = "";     // varchar(100),
                string observacao  = "";     //varchar(2000),
                int idtipopagamento = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdTipoPagamento);  //  integer
                int IDLOCALCOBRANCA= Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdLocalCobranca);  //  integer
                int IDFORMAPAGTO= Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdFormaPagto);  //  integer
                Decimal VALORFINAL = Convert.ToDecimal(txtTotalVenda.Text);
                Decimal PORCDESCONTO = 0;
                Decimal VALORDESCONTO = 0;

                string FLAGENVIADO = "N";
                string FLAGCONTINGENCIA = "N";
                string STRQRCODE = string.Empty;
                string PROTOCOLO = string.Empty;
                string AMBIENTE = BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente;
                string PROTOCOLOCANCEL = string.Empty;

                int CODOPERADORACARTAO = -1;
                string NOMEOPERADORACARTAO= string.Empty;
                string NUMEROAUTORIZACARTAO = string.Empty;

               decimal  ValoPagoDinheiro = 0;
                decimal ValoPagoCartaoCredito = 0;
                decimal ValoPagoValeRefeicao = 0;
                decimal ValorPagoCheque = 0;
                decimal ValoPagoCartaoDebito = 0;
                decimal ValoPagoOutros = 0;

                return new CUPOMELETRONICOEntity(_CUPOMELETRONICOID, numeronfce, SERIE, idcliente, dtemissao, dtsaida, horasaida,
                                                idcfop, totalnota, idvendedor, valorpago,
                                                valordevedor, valortroco, chaveacesso,
                                                observacao, idstatusnfce, idtipopagamento, IDLOCALCOBRANCA,
                                                IDFORMAPAGTO,VALORFINAL, PORCDESCONTO,  VALORDESCONTO,
                                                FLAGENVIADO, FLAGCONTINGENCIA, STRQRCODE,
                                                PROTOCOLO, AMBIENTE, PROTOCOLOCANCEL, CODOPERADORACARTAO,
                                                NOMEOPERADORACARTAO, NUMEROAUTORIZACARTAO, ValoPagoDinheiro,
                                                ValoPagoCartaoCredito, ValoPagoValeRefeicao,  ValorPagoCheque,
                                                ValoPagoCartaoDebito,  ValoPagoOutros);
            }
            set
            {

                if (value != null)
                {
                    _CUPOMELETRONICOID = value.CUPOMELETRONICOID;
                    AbriSituacaoNFCe(Convert.ToInt32(value.IDSTATUSNFCE));
                    numeronfce = value.NUMERONFCE;
                    errorProvider1.Clear();
                }
                else
                {
                    _CUPOMELETRONICOID = -1;
                    txtDescProduto.Text = "CAIXA LIVRE";
                    txtDescProduto.GotFocus += txtDescProduto_GotFocus;
                    txtQuant.Text = "1";
                    txtValorUnit.Text = "0,00";
                    txtTotalProduto.Text = "0,00";
                    txtDescProduto.Enabled = true;
                    errorProvider1.Clear();
                }
            }
        }

        private void AbriSituacaoNFCe(int IDSTATUSNFCE)
        {
            lblSituacao.Text = "Situação: " + STATUSNFCEP.Read(IDSTATUSNFCE).NOME;

            if(IDSTATUSNFCE == 1)// 1 - Enviado
            {
                lblSituacao.ForeColor = Color.Blue;
            }
            else if(IDSTATUSNFCE == 2)//Cancelado
            {
                lblSituacao.ForeColor = Color.Red;
            }
            else if (IDSTATUSNFCE == 3)//Inutilizado
            {
                lblSituacao.ForeColor = Color.Green;
            }
            else if (IDSTATUSNFCE == 4)//Aberto
            {
                lblSituacao.ForeColor = Color.Black;
            }
            else if (IDSTATUSNFCE == 5)//Fechado
            {
                lblSituacao.ForeColor = Color.Orange;
            }
        }

        int _PRODUTONFCEID = -1;
        public PRODUTONFCEEntity Entity2
        {
            get
            {

                   int  IDPRODUTO  = PRODUTOSTy.IDPRODUTO; //        INTEGER,
                   decimal QUANTIDADE =  Convert.ToDecimal(txtQuant.Text); //         NUMERIC(15,4),
                   decimal VALORUNITARIO   =  Convert.ToDecimal(txtValorUnit.Text); //   NUMERIC(15,4),
                   decimal VALORTOTAL  = Convert.ToDecimal(QUANTIDADE *  VALORUNITARIO);//      NUMERIC(15,4),
                   decimal ALICMS = Convert.ToDecimal(PRODUTOSTy.ICMS);//            NUMERIC(15,2),
                   decimal BASEICMS  = VALORTOTAL; //         NUMERIC(15,2),
                   decimal REDICMS   = 0;//         NUMERIC(15,2),
                   decimal VALORICMS  = (VALORTOTAL *  ALICMS) /100;    // NUMERIC(15,2),
                   decimal ALIPI   = Convert.ToDecimal(PRODUTOSTy.IPI);//           NUMERIC(15,2),
                   decimal VALORIPI = Convert.ToDecimal(VALORTOTAL *  ALIPI) /100;    // NUMERIC(15,2),
                   int IDUNIDADE  = Convert.ToInt32(PRODUTOSTy.IDUNIDADE);

                    int IDCFOP  = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP);
                    if (PRODUTOSTy.CFOP.Trim() != string.Empty)
                        IDCFOP = BuscaCFOP(PRODUTOSTy.CFOP); 

                   decimal ALIQPIS  = Convert.ToDecimal(PRODUTOSTy.ALIQPIS);//           NUMERIC(15,2),
                   decimal VALORPIS = (VALORTOTAL *  ALIQPIS) /100; 
                   decimal ALIQCOFINS  = Convert.ToDecimal(PRODUTOSTy.ALIQCOFINS);//
                   decimal  VALORCOFINS  = (VALORTOTAL *  ALIQCOFINS) /100; 
                   decimal VLBASEST = 0;//           NUMERIC(15,2),
                   decimal VLICMSST = 0;//           NUMERIC(15,2),
                   decimal VLALIQST =0;//          NUMERIC(15,2),
                   decimal VLOUTROS   = 0;//        NUMERIC(15,2),
                   decimal VLTRIBUTOAPROX = CalculoTributoAprox(PRODUTOSTy.NCMSH);
                   int? IDCST = PRODUTOSTy.IDCST;

                return new PRODUTONFCEEntity(_PRODUTONFCEID, _CUPOMELETRONICOID , IDPRODUTO , QUANTIDADE, VALORUNITARIO, VALORTOTAL,
                                                ALICMS, BASEICMS , REDICMS , VALORICMS , ALIPI, VALORIPI, IDUNIDADE, IDCFOP,
                                                ALIQPIS, VALORPIS, ALIQCOFINS , VALORCOFINS, VLBASEST, VLICMSST , VLALIQST,
                                                 VLOUTROS , VLTRIBUTOAPROX, 0, IDCST);
            }
            set
            {

                if (value != null)
                {
                    _PRODUTONFCEID = value.PRODUTONFCEID;                  
                    errorProvider1.Clear();
                }
                else
                {
                    _PRODUTONFCEID = -1;
                    PRODUTOSTy = null;
                    txtQuant.Text = "1";
                    txtValorUnit.Text = "0,00";
                    txtTotalProduto.Text = "0,00";
                    //txtDescProduto.Text = string.Empty;
                    txtDescProduto.Focus();

                    if (_CUPOMELETRONICOID == -1)
                    {
                        txtDescProduto.Text = "CAIXA LIVRE";
                        txtDescProduto.GotFocus += txtDescProduto_GotFocus;
                        txtDescProduto.SelectAll();
                    }

                    errorProvider1.Clear();
                }
            }
        }

        public int BuscaCFOP(string CFOP)
        {
            int result = -1;

            try
            {
               RowRelatorio.Clear();
               RowRelatorio.Add(new RowsFiltro("CODCFOP", "System.String", "=", CFOP));

               CFOPCollection CFOPColl = new CFOPCollection();
               CFOPProvider CFOPP = new CFOPProvider();
               CFOPColl = CFOPP.ReadCollectionByParameter(RowRelatorio);
                
                if (CFOPColl.Count > 0)
                    result = CFOPColl[0].IDCFOP;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Decimal TotalCupom(int CUPOMELETRONICOID )
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

        private void ExibirDadosCupom(int CUPOMELETRONICOID)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));
                LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                Entity = CUPOMELETRONICOP.Read(CUPOMELETRONICOID);
                
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESATy = EMPRESAP.Read(1);

               
                tbProdutos.Text = String.Empty;
                tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;
                tbProdutos.Text += Util.centeredString(EMPRESATy.NOMEFANTASIA, 50) + Environment.NewLine;
                string CNPJEMPRESA = String.Format(@"{0:00\.000\.000\/0000\-00}", EMPRESATy.CNPJCPF);
                tbProdutos.Text += Util.centeredString(CNPJEMPRESA, 50) + Environment.NewLine;
                tbProdutos.Text += Util.centeredString(DateTime.Now.ToString("dd/MM/yyyy"),50) + Environment.NewLine;

                //tbProdutos.Text += @"Data: " + DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine;
                //tbProdutos.Text += @"Nome: " + EMPRESATy.NOMEFANTASIA + Environment.NewLine;
                //tbProdutos.Text += @"CNPJ: " + String.Format(@"{0:00\.000\.000\/0000\-00}", EMPRESATy.CNPJCPF) + Environment.NewLine;
                tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;


                //Dados Do Produto
                string header = String.Format("{0,12}{1,8}{2,12}\n", "Item", "Codigo", "Descrição");
                string subheader = String.Format("{0,12}{1,12}{2,8}\n", "", "Unidade", "Valor");
                tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;
                tbProdutos.Text += header + Environment.NewLine;
                tbProdutos.Text += subheader + Environment.NewLine;
                int itemProduto = 1;


                //for (int i = LIS_PRODUTONFCEColl.Count; i > LIS_PRODUTONFCEColl.Count; i--)
                foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
                {
                    tbProdutos.Text += Environment.NewLine;
                    tbProdutos.Text += String.Format("{0,6}{1,8}  {2,12}\n", itemProduto.ToString().PadLeft(3, '0'), item.IDPRODUTO.ToString(), item.NOMEPRODUTO);
                    tbProdutos.Text += String.Format("{0,12}{1,12}{2,8}\n", "      ", Convert.ToDecimal(item.QUANTIDADE).ToString("n4") + " x ", Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"));
                    tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;
                    itemProduto++;
                }
                tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;
                tbProdutos.Text += @"Lista de Produto - F10 para editar" + Environment.NewLine;
                tbProdutos.Text += @"-----------------------------------------------------" + Environment.NewLine;

                
                this.tbProdutos.Focus();
                int textLength = tbProdutos.Text.Length;
                tbProdutos.SelectionStart = textLength;
                tbProdutos.ScrollToCaret();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

         private decimal CalculoTributoAprox(string CODNCM)
        {
             decimal rsult = 0;
            try
            {
                RowRelatorio.Clear();

                if (CODNCM != string.Empty)
                    RowRelatorio.Add(new RowsFiltro("CODNCM", "System.String", "=", CODNCM.ToString()));

                NCMCollection NCMColl = new NCMCollection();
                NCMProvider NCMP = new NCMProvider();

                NCMColl = NCMP.ReadCollectionByParameter(RowRelatorio);

                if (NCMColl.Count > 0)
                {
                    if (txtQuant.Text.TrimEnd().TrimStart() == string.Empty)
                        txtQuant.Text = "0,00";

                    if (txtValorUnit.Text.TrimEnd().TrimStart() == string.Empty)
                        txtValorUnit.Text = "0,00";

                    decimal TotalProduto = Convert.ToDecimal(txtQuant.Text) * Convert.ToDecimal(txtValorUnit.Text);
                    decimal TributoAprox = (TotalProduto * Convert.ToDecimal(NCMColl[0].ALNACIONAL)) / 100;

                   rsult = TributoAprox;
                }

                return rsult;
            }
            catch (Exception ex)
            {
                return rsult;
                MessageBox.Show("Não foi possível efetuar cálculo de tributos aproximados!");
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();

             if (Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCliente) < 1)
            {
                string MsgErro = "Cliente não selecionado!";
                MessageBox.Show(MsgErro);

                (new FrmConfiguarcaoNFCe()).ShowDialog();

                result = false;
            }
            else if (Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdTipoPagamento) < 1)
            {
                string MsgErro = "Tipo de Pagamento não selecionado!";
                MessageBox.Show(MsgErro);

                (new FrmConfiguarcaoNFCe()).ShowDialog();

                result = false;
            }
            else if (Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdFormaPagto) < 1)
            {
                string MsgErro = "Forma de Pagamento não selecionado!";
                MessageBox.Show(MsgErro);

                (new FrmConfiguarcaoNFCe()).ShowDialog();

                result = false;
            }
            else if (Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdLocalCobranca) < 1)
            {
                string MsgErro = "Local de Cobrança não selecionado!";
                MessageBox.Show(MsgErro);

                (new FrmConfiguarcaoNFCe()).ShowDialog();

                result = false;
            }
            else if(PRODUTOSTy == null)
            {
                string MsgErro = "Produto Não selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            if (Convert.ToDecimal(txtQuant.Text) < 1)
            {
                string MsgErro = "Quantidade inválida!!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (ConfigNFCe.Default.NaoPermitirValorUnitarioZerado.Trim() == "S" && (Convert.ToDecimal(txtQuant.Text) < Convert.ToDecimal("0,01")))
            {
                string MsgErro = "Valor unitário inválido!";
                MessageBox.Show(MsgErro);
                result = false;
            }

            else if (Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP) < 1)
            {
                string MsgErro = "CFOP não selecionado!";
                MessageBox.Show(MsgErro);

                (new FrmConfiguarcaoNFCe()).ShowDialog();

                result = false;
            }
            else if (PRODUTOSTy == null || Convert.ToInt32(PRODUTOSTy.IDUNIDADE) < 1)
            {
                string MsgErro = "Unidade do produto não selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (PRODUTOSTy.IDCST < 0 || PRODUTOSTy.IDCST == null)
            {
                string MsgErro = "CST do produto não selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (PRODUTOSTy.IDUNIDADE < 0 || PRODUTOSTy.IDUNIDADE == null)
            {
                string MsgErro = "Unidade do produto não selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }
            else if (PRODUTOSTy.NCMSH.Trim() == string.Empty)
            {
                string MsgErro = "NCM do produto não selecionado!";
                MessageBox.Show(MsgErro);
                result = false;
            }  
            else
                errorProvider1.Clear();


            return result;
        }   

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _CUPOMELETRONICOID = CUPOMELETRONICOP.Save(Entity);
                    GravaProduto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GravaProduto()
        {
            try
            {
                if (Validacoes())
                {
                    PRODUTONFCEP.Save(Entity2);                  
                    txtTotalVenda.Text =  TotalCupom(_CUPOMELETRONICOID).ToString("n2");
                    ExibirDadosCupom(_CUPOMELETRONICOID);
                    CUPOMELETRONICOP.Save(Entity);
                    Entity2 = null;

                    if (BmsSoftware.ConfigNFCe.Default.EmitirSomAdicionarProduto == "S")
                         Beep(1000, 300);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetEmpresa();
            timer1.Start();
            VerificaAcesso();

            toolStripStatusLabel1.Text = "IMEX Sistemas - F3 Nova Venda - F8 Fechar Venda";

            VerificaExisteNFCeContigencia2();

            //Imagem inicial

            if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png"))
            {
                byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png");
                MemoryStream stream = new MemoryStream(Logo);
                pictureBox1.Image = Image.FromStream(stream);
            }

            Entity = null;
            Entity2 = null;

            if (BmsSoftware.ConfigNFCe.Default.MaximizarTelaAbrir.Trim() == "S")
                WindowState = FormWindowState.Maximized;

              if (BmsSoftware.ConfigNFCe.Default.NaoPermitirAlteraValorUnitário.Trim() == "S")
                  txtValorUnit.Enabled = false;
              else
                  txtValorUnit.Enabled = true;

            if (BmsSoftware.ConfigNFCe.Default.NaoPermitirAlterarQuantidade.Trim() == "S")
                txtQuant.Enabled = false;
              else
                txtQuant.Enabled = true;          

            OpenImageFundo();

           // System.Diagnostics.Process Processo1 = null;            
            if (BmsSoftware.ConfigNFCe.Default.EmpresaIntegra == "1") //1 News Systems 
            {
               AbreNewSystem2();
            }

            if (!VerificaUso4())
            {
                if (MensagemErroBloqueio == string.Empty)
                {

                    MessageBox.Show("Entre em contato com a " + BmsSoftware.ConfigSistema1.Default.NomeEmpresa + " " + BmsSoftware.ConfigSistema1.Default.site,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show(MensagemErroBloqueio,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                }

                //Notifica a IMEX do uso do sistema por email
               // EnviarEmailUso();
                this.Close();              

            }
            else
            {
                //Notifica a IMEX do uso do sistema por email
              //  EnviarEmailUso();
            }

            txtDescProduto.GotFocus += txtDescProduto_GotFocus;

        }

        void txtDescProduto_GotFocus(object sender, EventArgs e)
        {
            txtDescProduto.SelectAll();
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

                    //    //Verifica se o executavel NFCe da News Systems esta aberto
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

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private Boolean VerificaUso4()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                WebRequest req = WebRequest.Create("http://143.95.75.225/~imexsist/registros/securitysoftware3.xml");
                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string sCaminhoDoArquivo = reader.ReadToEnd();

                reader.Close();
                res.Close();

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESATy = EMPRESAP.Read(1);

                XmlDocument xmlDoc = new XmlDocument();
                // xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                //xmlDoc.LoadXml(xmlDoc.InnerXml);
                xmlDoc.LoadXml(sCaminhoDoArquivo);

                //Pegando elemento pelo nome da TAG
                XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                //Usando foreach para localizar o cnpj
                foreach (XmlNode xn in xnList)
                {
                    if (Util.RetiraLetras(xn["cnpjcpf"].InnerText.Trim()) == Util.RetiraLetras(EMPRESATy.CNPJCPF.Trim()))
                    {
                        MensagemErroBloqueio = xn["mensagem"].InnerText.Trim();
                        if (xn["bloqueiototal"].InnerText.Trim() == "S")
                            result = false;
                        else
                            result = true;

                        break;
                    }

                }

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                result = true;
                return result;

            }

        }

        private void OpenImageFundo()
        {
            try
            {
                if (BmsSoftware.ConfigNFCe.Default.fundologo != string.Empty)
                {
                    byte[] logo = GetFoto(BmsSoftware.ConfigNFCe.Default.fundologo);
                    MemoryStream stream = new MemoryStream(logo);
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
             
        }

        public static byte[] GetFoto(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }   

        private void GetEmpresa()
        {
            //'nome da empresa
            try
            {
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                lblNomeEmpresa.Text = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }          

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void configuraCaminhosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        string TimeNow = string.Empty;
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeNow = DateTime.Now.ToLongTimeString();
            labelDayDate.Text = DateTime.Now.ToLongDateString() + " - " + TimeNow;
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerificaExisteNFCeContigencia2();
            this.Close();
        }


        //Procedure para atualizar status de NFCe em Contigencia
        private void VerificaExisteNFCeContigencia2()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "6")); //6 - Contigencia
                LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_CONT = new LIS_CUPOMELETRONICOCollection();
                LIS_CUPOMELETRONICOColl_CONT = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID DESC");

                foreach (var item in LIS_CUPOMELETRONICOColl_CONT)
                {
                     VerificaArquivoContingencia(item.NUMERONFCE.ToString(), Convert.ToInt32(item.CUPOMELETRONICOID));
                }

                AvisoNFCeContigencia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void VerificaArquivoContingencia(string NumeroNota, int CUPOMELETRONICOID)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Processados\nsConcluido\contingencia_ret.txt";

                Application.DoEvents();
                this.Text = "Aguarde.. processando...";

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

                //usando a instrução using os recursos são liberados após a conclusão da operação

                if (File.Exists(arquivo))
                {
                    CUPOMELETRONICOEntity CUPOMELETRONICOTy_2 = new CUPOMELETRONICOEntity();
                    CUPOMELETRONICOTy_2 = CUPOMELETRONICOP.Read(CUPOMELETRONICOID);

                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (Util.RetiraLetras(CUPOMELETRONICOTy_2.CHAVEACESSO) != string.Empty)
                            {
                                if (linha.IndexOf(Util.RetiraLetras(CUPOMELETRONICOTy_2.CHAVEACESSO)) != -1)//Chave de acesso na linha
                                {
                                    if (linha.IndexOf("Autorizado") != -1)//Autorizado
                                    {
                                        CUPOMELETRONICOTy_2.IDSTATUSNFCE = 1;//Enviado
                                        CUPOMELETRONICOTy_2.PROTOCOLO = Util.RetiraLetras(linha.Substring(3, 16));
                                        CUPOMELETRONICOP.Save(CUPOMELETRONICOTy_2);
                                    }                                  
                                }
                            }
                        }
                    }                    
                }
                else
                {
                    Application.DoEvents();
                }

                this.Cursor = Cursors.Default;
                this.Text = "  IMEX Sistemas - NFCe Nota Fiscal de Consumidor Eletrônica";
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                this.Text = "  IMEX Sistemas - NFCe Nota Fiscal de Consumidor Eletrônica";
                MessageBox.Show("Erro técnico: " + ex.Message);
            };
        }

        private void AvisoNFCeContigencia()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "6")); //6 - Contigencia
                LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_CONT = new LIS_CUPOMELETRONICOCollection();
                LIS_CUPOMELETRONICOColl_CONT = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID DESC");

                int contador = 0;
                foreach (var item in LIS_CUPOMELETRONICOColl_CONT)
                {
                    contador++;
                }

                this.Cursor = Cursors.Default;

                if (contador > 0)
                    MessageBox.Show("Exite(m) NFCe não Enviado(s) a SEFAZ!");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            };
        }      

        private void configurarCaminhosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmConfigMaquina()).ShowDialog();
        }

        private void empresaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            (new FrmEmpresa()).ShowDialog();
        }

        private void configuraçãoDeSistemaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            (new FrmConfiguarcaoNFCe()).ShowDialog();
            OpenImageFundo();
        }

        private void tbProdutos_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescProduto_Enter(object sender, EventArgs e)
        {
           // lblObsField.Text = "Código do Produto, pressione Ctrl+E para pesquisar.";
            toolStripStatusLabel1.Text = "Código do Produto, pressione Ctrl+E para pesquisar - F3 Nova Venda - F8 Fechar Venda";
            txtDescProduto.GotFocus += txtDescProduto_GotFocus;
            txtDescProduto.SelectAll();
        }

        private void txtDescProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisaProduto();
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
                        PRODUTOSTy = PRODUTOSP.Read(result);
                        txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                        txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                        txtQuant.Focus();
                        SomaTotaProduto();

                        if (BmsSoftware.ConfigNFCe.Default.AdicionaProdutoAposLeituraCódigoBarra.Trim() == "S")
                            Grava();
                    }
                }
            }

        }

        private void PesquisaProduto()
        {
            PRODUTOSTy = null;
            PRODUTOSTy = PesquisaCodBarra(txtDescProduto.Text);            

            if (PRODUTOSTy == null)
            {
                DialogResult dr = MessageBox.Show("Código de Barra inválido, deseja efetuar a pesquisa?",
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
                            txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                            txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                            txtQuant.Focus();
                            SomaTotaProduto();

                            if (BmsSoftware.ConfigNFCe.Default.AdicionaProdutoAposLeituraCódigoBarra.Trim() == "S")
                                Grava();
                        }
                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                txtQuant.Focus();
                SomaTotaProduto();

                if (BmsSoftware.ConfigNFCe.Default.AdicionaProdutoAposLeituraCódigoBarra.Trim() == "S")
                    Grava();
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

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void SomaTotaProduto()
        {
            try
            {
                if (ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtValorUnit.Text))
                    txtTotalProduto.Text = (Convert.ToDecimal(txtQuant.Text) * Convert.ToDecimal(txtValorUnit.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtQuant_Validated(object sender, EventArgs e)
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
                TextBoxSelec.Text = "1";
        }

        private void txtValorUnit_Validating(object sender, CancelEventArgs e)
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
                TextBoxSelec.Text = "0,00";
        }

        private void txtDescProduto_Leave(object sender, EventArgs e)
        {
            SomaTotaProduto();
        }

        private void txtQuant_Leave(object sender, EventArgs e)
        {
            SomaTotaProduto();
        }

        private void txtValorUnit_Leave(object sender, EventArgs e)
        {
            SomaTotaProduto(); 
        }

        private void fecharVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerificaExisteNFCeContigencia2();

            using (FrmFecharVenda frm = new FrmFecharVenda())
            {
                if (_CUPOMELETRONICOID != -1)
                {
                    frm.CUPOMELETRONICOID = _CUPOMELETRONICOID;
                    frm.ShowDialog();

                    if (frm.CUPOMELETRONICOID == -1)
                    {
                        novaVendaToolStripMenuItem_Click(null, null);
                    }
                    else
                    {
                        if (frm.CUPOMELETRONICOID != -1)
                        {
                            CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();
                            CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(frm.CUPOMELETRONICOID);

                            if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4) //Aberto
                            {
                                txtQuant.Enabled = true;
                                txtValorUnit.Enabled = true;
                                txtDescProduto.Enabled = true;
                            }
                            else
                            {
                                txtQuant.Enabled = false;
                                txtValorUnit.Enabled = false;
                                txtDescProduto.Text = string.Empty;
                                txtDescProduto.Enabled = false;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Venda não selecionada!");
                }
            }           

        }    


        private void txtTotalProduto_Enter(object sender, EventArgs e)
        {
            Grava();
        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FrmPesquisaNFCe frm = new FrmPesquisaNFCe())
            {
                frm.ShowDialog();

                if (frm.Result > 0)
                {
                    ExibirDadosCupom(frm.Result);
                    txtTotalVenda.Text = Convert.ToDecimal(TotalCupom(frm.Result)).ToString("n2");

                    if (frm.Result!= -1)
                    {
                        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();
                        CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(frm.Result);

                        if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4) //Aberto
                        {
                            txtQuant.Enabled = true;
                            txtValorUnit.Enabled = true;
                            txtDescProduto.Enabled = true;
                            txtTotalProduto.Enabled = true;
                            txtTotalProduto.Enabled = true;
                        }
                        else
                        {
                            txtQuant.Enabled = false;
                            txtValorUnit.Enabled = false;
                            txtDescProduto.Text = string.Empty;
                            txtDescProduto.Enabled = false;
                            txtTotalProduto.Enabled = false;
                            txtTotalProduto.Enabled = false;
                        }
                    }
                }
            }
        }

        private void novaVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tbProdutos.Text = string.Empty;
            txtTotalVenda.Text = "0,00";

            txtDescProduto.Enabled = true;
            txtTotalProduto.Enabled = true;
            txtTotalProduto.Enabled = true;

            if (BmsSoftware.ConfigNFCe.Default.NaoPermitirAlteraValorUnitário.Trim() == "S")
                txtValorUnit.Enabled = false;
            else
                txtValorUnit.Enabled = true;

            if (BmsSoftware.ConfigNFCe.Default.NaoPermitirAlterarQuantidade.Trim() == "S")
                txtQuant.Enabled = false;
            else
                txtQuant.Enabled = true;
        }

        private void excluirItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_CUPOMELETRONICOID != -1)
            {

                using (FrmItensVenda frm = new FrmItensVenda())
                {
                    try
                    {
                        frm.CUPOMELETRONICOID = _CUPOMELETRONICOID;
                        frm.ShowDialog();
                        ExibirDadosCupom(_CUPOMELETRONICOID);
                        txtTotalVenda.Text = TotalCupom(_CUPOMELETRONICOID).ToString("n2");

                        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();
                        CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(_CUPOMELETRONICOID);

                        if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4) //Aberto
                        {
                            CUPOMELETRONICOTy.TOTALNOTA = Convert.ToDecimal(txtTotalVenda.Text); ;
                            CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Cupom não selecionado!");
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmSobre()).ShowDialog();
        }

        private void cupomEletrônicoNFCeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private string ObterNomePathNfceXslt()
        {
            using (NDC.Push("ObterNomePathNfceXslt"))
            {
                string xsltpath = String.Empty;
                string assembly = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (assembly != null)
                {
                    // xsltpath = assembly.ToLower().Replace("\\bin\\debug", "\\nfce");
                    xsltpath = BmsSoftware.ConfigSistema1.Default.PathInstall;
                }
                return xsltpath;
            }
        }

        private void vendaPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmVendaProduto()).ShowDialog();
        }

        private void vendaPorTipoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmVendaTipoPagamento()).ShowDialog();
        }

        private void cancelarCupomEletrônicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

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


        private NFCeService NewWebService()
        {
            var ws = new NFCeService { Url = BmsSoftware.ConfigNFCe.Default.UrlNfCeService };
            //WebProxy wp = GetProxy();
            //if (wp != null)
            //    ws.Proxy = wp;
            return (ws);
        }

        private void aberturaDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAberturaCaixa frm = new FrmAberturaCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void importarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchPedidoNFe frm = new FrmSearchPedidoNFe())
            {
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {

                    DialogResult dr = MessageBox.Show("Deseja Realmente Gerar Cupom Eletronico pelo Pedido nº " + result + " ?",
                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        if (result > 0)
                        {
                            try
                            {

                                CreaterCursor Cr = new CreaterCursor();
                                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                                //Produto
                                //Armazena dados Produto do Pedido
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", result.ToString()));

                                LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
                                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

                                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                                //Altera  o Pedido para Orçamento
                               // if(LIS_PRODUTOSPEDIDOColl.Count > 0)
                               //     AlteraPedido(LIS_PRODUTOSPEDIDOColl[0].IDPEDIDO);

                                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                                {
                                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                                    if (PRODUTOSTy != null)
                                    {
                                        txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                                        txtValorUnit.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n2");
                                        txtQuant.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n2");

                                     
                                        SomaTotaProduto();
                                        Grava();
                                    }
                                }

                                this.Cursor = Cursors.Default;

                                MessageBox.Show("Produto(s) importado(s) com sucesso!");

                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Não foi possível importar o pedido!");
                                MessageBox.Show("Erro Técnico: " + ex.Message);

                            }
                        }

                    }
                }
            }
        }

        //Altera  o pedido para orçamento
        private void AlteraPedido(int? IDPEDIDO)
        {
            try
            {
                PEDIDOProvider PEDIDOP = new PEDIDOProvider();
                PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                PEDIDOTy = PEDIDOP.Read(Convert.ToInt32(IDPEDIDO));
                if(PEDIDOTy != null)
                {
                   // PEDIDOTy.FLAGORCAMENTO = "S";
                  //  PEDIDOP.Save(PEDIDOTy);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private void txtQuant_Enter(object sender, EventArgs e)
        {

        }

        private void sangriaNoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSangria frm = new FrmSangria())
            {
                frm.ShowDialog();
            }
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            novaVendaToolStripMenuItem_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fecharVendaToolStripMenuItem_Click(null, null);
        }

        private void vendaPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaVendedorPedNormal frm = new FrmVendaVendedorPedNormal())
            {
                frm.ShowDialog();
            }
        }

        private void excluirCupomNFCeToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (_CUPOMELETRONICOID == -1)
             {
                MessageBox.Show("Não Existe Cupom NFCe Selecionado!");
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                     ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                   
                    CUPOMELETRONICOEntity CUPOMELETRONICOTy_2 = new CUPOMELETRONICOEntity();
                    CUPOMELETRONICOTy_2 = CUPOMELETRONICOP.Read(_CUPOMELETRONICOID);

                    if (CUPOMELETRONICOTy_2.IDSTATUSNFCE == 4)
                    {
                        //Apaga os produtos do cupom
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", _CUPOMELETRONICOID.ToString()));
                        PRODUTONFCECollection PRODUTONFCEColl = new PRODUTONFCECollection();
                        PRODUTONFCEProvider PRODUTONFCEP = new PRODUTONFCEProvider();
                        PRODUTONFCEColl = PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);
                        foreach (PRODUTONFCEEntity item in PRODUTONFCEColl)
                        {
                            PRODUTONFCEP.Delete(item.PRODUTONFCEID);
                        }

                        CUPOMELETRONICOP.Delete(_CUPOMELETRONICOID);

                        novaVendaToolStripMenuItem_Click(null, null);

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                    else
                    {
                        MessageBox.Show("Cupom NFCe já Emitido, Não é Possível Excluir!");
                    }
                }
            }
        }
    }
}
