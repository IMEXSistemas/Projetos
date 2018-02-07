using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;
using System.IO;
using BmsSoftware.Modulos.Financeiro;
using System.Globalization;
using BmsSoftware.Modulos.FrmSearch;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using System.Xml;
using System.Drawing.Imaging;
using BMSworks.UI.BMSworks.UI;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmConfigSistema : Form
    {
        Utility Util = new Utility();

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();

        CONFIGBOLETACollection CONFIGBOLETAColl = new CONFIGBOLETACollection();
        LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();

        string _UFEmit = string.Empty;

        public FrmConfigSistema()
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

                if (control is ComboBox)
                {
                    control.KeyDown += new KeyEventHandler(controlFocus_KeyDown);
                    control.KeyPress += new KeyPressEventHandler(controlFocus_KeyPress);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        void controlFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            lblobsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }


        int _IDCONFIGSISTEMA = -1;
        public CONFISISTEMAEntity Entity
        {
            get
            {
                string FLAGLOGORELATORIO = "S";

                int? IDARQUIVOBINARIO = _IDARQUIVOBINARIO;
                if(_IDARQUIVOBINARIO == -1)
                    IDARQUIVOBINARIO  = null;

                int? IDCONFIGBOLETA = null;
                if(cbBoleta.SelectedIndex > 0)
                    IDCONFIGBOLETA = Convert.ToInt32(cbBoleta.SelectedValue);

                string FLAGCOMPENTREGABOLETA =  "N";
                string FLAGCARNEBOLETA = "N";

                int PRAZOOS = Convert.ToInt32(txtPrazoPedidoOS.Text);
                int PRAZOORCAMENTO = 0;

                string FLAGVENDADEBITO = chkDebitoCliente.Checked == true ? "S" : "N";

                string FLAGPEDBAIXAESTOQUE = "N";

                int? TEMPOGARANTIA = 0;

                string FLAGCOMISSAO = "N";

                string MSGFECHOS = txtMsgFechOS.Text.TrimEnd().TrimStart();
                string MSGPEDIDO = txtMsgPedido.Text.TrimEnd().TrimStart();
                string MSGCONSIGNACAO = string.Empty;

                string FLAGFECHOSESTOQUE = "N";

                string SERIENF = "";

                string FLAGSOMAIPI = "N";
                string FLAGSOMASEGURO =  "N";
                string FLAGJANELAS = chkFlagJanela.Checked == true ? "S" : "N";

                string SERIENFE = TxtSerieNFE.Text;
                string FLAGSOMAIPINFE = "N";
                string FLAGSOMASEGURANFE = "N";
                string FLAGCOMISSAONFE ="N";

                string MODELONFE = txtModeloNFE.Text;

                decimal ALISSQN = 0;

                string INSCMUNICIPAL = txtInscMunicipal.Text;
                decimal ALIPIS = 0;
                decimal ALICOFINS = 0;

                string FLAGBASEISSQN = "N";

                int CODMUNIBGE = Convert.ToInt32(txtCodMunIBGE.Text);
                int CODUFIBGE = Convert.ToInt32(txtCodUFIBGE.Text);

                // Ambiente - 1 Produção - 2 Homologação
                rbHomologacaoNFe.Checked = false;
                string FLAGAMBIENTENFE = rbProducaoNFe.Checked == true ? "1" : "2";
                rbHomologacaoNFe.Checked = !rbProducaoNFe.Checked;      

               string SERIALCERTFDIGITAL =txtSerialCertDigital.Text;
               string NAMECERTFDIGITAL = txtNameCertDigital.Text;
               string VALIDADECERTDIGITAL = txtValidadeCertDigital.Text;

               string FLAGLOGONFE = "S"; 

               string USUARIOPROXY = "";
               string SENHAPROXY = "";
               int IDVERSAOXMLNFE = Convert.ToInt32(cbVersaoXml.SelectedValue);
               string NOMEFANTASIA = txtRazaoSocialEmpresa.Text;
               string CNAE = TxtCNAE.Text;
               string IEST = txtIEST.Text;
               string CRT = txtCRT.Text;
               string FLAGALIQIPICONFIS = "N";

               int PORTAEMAIL = 0;

               string EMAILCS = "";
               string SMTP = "";
               string SENHAEMAIL = "";
               string CONFSEGSSL = "N";
               string HOSTPROXY = "";
               string PORTAPROXY = "";
               string FLAGNFESERVICOS =  "N";
               string NOTAFISCALINICIAL = "";
               string MSGINICIALNFE = txtMsgInicialNFe.Text.TrimEnd().TrimStart();

               int LARGLAMINA = 0;
               int NIVELOTIMIZ = 0;
                string SCHEMAXML = cbbSchema.Text;

                string CASADECPRINTDANFE = "2";
                if (rbCasaDecTres.Checked)
                    CASADECPRINTDANFE = "3";
                else if (rbCasaDecQuatro.Checked)
                    CASADECPRINTDANFE = "4";

                string FLAGPLANOCORTE = "N";
                string FLAGCODREFERENCIA = chkExibCodRefe.Checked == true ? "S" : "N";
                string FLAGCUPOMFISCAL = "N";
                string FLAGPEDIDOMT = chkPedidoMT.Checked == true ? "S" : "N";
                string ESTOQUENEGATIVO = chkEstoqueNegativo.Checked == true ? "S" : "N";
                string FLAGCPFCNPJPEDIDO = chkExibirCPFCNPJPedido.Checked == true ? "S" : "N";
                string FLAGCPDIGISAT  = "N";
                string PATHRECEPDIGISAT = "";
                string FLAGBAIXAESTOQUENF = chkBaixaEstoqueNFe.Checked == true ? "S" : "N";
                string OPERADORASMS = "";
                string FLAGLIMITECREDITO = chkLimitCredCliente.Checked == true ? "S" : "N";
                string FLAGHABNFE = "S";
                string FLAGMSGFECHA = chkMsgUsuario.Checked == true ? "S" : "N";
                string FLAGCUPOMFAST = "N";
                string FLAGBACKUP = chkBackupAtu.Checked == true ? "S" : "N";
                string FLAGCSTECF = "S";
                string FLAGCODREFNFE = chkCodRefNfe.Checked == true ? "S" : "N";

                string TOKENIMEXAPP = TxtTokenIMEXApp.Text.Trim();
                string IDASPNETUSERSINCLUSAO = txtIDASPNETUSERSINCLUSAO.Text.Trim();
                string IDEMPRESAIMEXAPP = TxtIDEmpresaIMEXAPP.Text.Trim();
                string IDREPRESIMEXAPP = txtIDRepresIMEXAPP.Text.Trim();
                string FLAGIMEXAPP = chkFlagIMEXApp.Checked == true ? "S" : "N";

                return new CONFISISTEMAEntity(_IDCONFIGSISTEMA, FLAGLOGORELATORIO, IDARQUIVOBINARIO,
                                              IDCONFIGBOLETA, FLAGCOMPENTREGABOLETA, FLAGCARNEBOLETA,
                                              PRAZOOS, PRAZOORCAMENTO, FLAGVENDADEBITO, FLAGPEDBAIXAESTOQUE,
                                              TEMPOGARANTIA, FLAGCOMISSAO, MSGFECHOS, MSGPEDIDO, MSGCONSIGNACAO,
                                              FLAGFECHOSESTOQUE, SERIENF, FLAGSOMAIPI, FLAGSOMASEGURO,
                                              FLAGJANELAS, SERIENFE, FLAGSOMAIPINFE, FLAGSOMASEGURANFE,
                                              FLAGCOMISSAONFE, MODELONFE, ALISSQN, INSCMUNICIPAL,
                                              ALIPIS, ALICOFINS, FLAGBASEISSQN, CODMUNIBGE, CODUFIBGE,
                                              FLAGAMBIENTENFE, SERIALCERTFDIGITAL, NAMECERTFDIGITAL,
                                              VALIDADECERTDIGITAL, FLAGLOGONFE, USUARIOPROXY,
                                              SENHAPROXY, IDVERSAOXMLNFE, NOMEFANTASIA,CNAE, IEST,
                                              CRT, FLAGALIQIPICONFIS, PORTAEMAIL, EMAILCS, SMTP, SENHAEMAIL,
                                              CONFSEGSSL, HOSTPROXY, PORTAPROXY, FLAGNFESERVICOS,
                                              NOTAFISCALINICIAL, MSGINICIALNFE, LARGLAMINA, NIVELOTIMIZ,
                                              SCHEMAXML, CASADECPRINTDANFE, FLAGPLANOCORTE,
                                              FLAGCODREFERENCIA, FLAGCUPOMFISCAL, FLAGPEDIDOMT,
                                              ESTOQUENEGATIVO, FLAGCPFCNPJPEDIDO, FLAGCPDIGISAT,
                                              PATHRECEPDIGISAT, FLAGBAIXAESTOQUENF, OPERADORASMS,
                                              FLAGLIMITECREDITO, FLAGHABNFE, FLAGMSGFECHA, FLAGCUPOMFAST,
                                              FLAGBACKUP, FLAGCSTECF, FLAGCODREFNFE, TOKENIMEXAPP,
                                              IDASPNETUSERSINCLUSAO, IDEMPRESAIMEXAPP, IDREPRESIMEXAPP,
                                              FLAGIMEXAPP);
            }
            set
            {
                if (value != null)
                {
                    _IDCONFIGSISTEMA = value.IDCONFIGSISTEMA;

                    if (value.IDARQUIVOBINARIO1 != null)
                        _IDARQUIVOBINARIO = Convert.ToInt32(value.IDARQUIVOBINARIO1);

                    if (value.IDCONFIGBOLETA != null)
                        cbBoleta.SelectedValue = value.IDCONFIGBOLETA;
                    else
                        cbBoleta.SelectedIndex = 0;                   

                    chkDebitoCliente.Checked = value.FLAGVENDADEBITO == "S" ? true : false; 
                    txtMsgFechOS.Text = value.MSGFECHOS;
                    txtMsgPedido.Text = value.MSGPEDIDO;
                    txtPrazoPedidoOS.Text = value.PRAZOOS.ToString();
                  
                    chkFlagJanela.Checked = value.FLAGJANELAS == "S" ? true : false;

                    TxtSerieNFE.Text  = value.SERIENFE;                   

                    txtModeloNFE.Text = value.MODELONFE;

                    txtInscMunicipal.Text = value.INSCMUNICIPAL;

                    txtCodMunIBGE.Text = value.CODMUNIBGE.ToString();
                    txtCodUFIBGE.Text = Convert.ToInt32(value.CODUFIBGE).ToString();
                    
                    // Ambiente - 1 Produção - 2 Homologação
                    rbHomologacaoNFe.Checked = false;
                    rbProducaoNFe.Checked = value.FLAGAMBIENTENFE == "1" ? true : false;
                    rbHomologacaoNFe.Checked = !rbProducaoNFe.Checked;                

                    txtNameCertDigital.Text = value.NAMECERTFDIGITAL;
                    txtSerialCertDigital.Text = value.SERIALCERTFDIGITAL;
                    txtValidadeCertDigital.Text = value.VALIDADECERTDIGITAL;

                    cbVersaoXml.SelectedValue = value.IDVERSAOXMLNFE;
                    txtRazaoSocialEmpresa.Text = value.NOMEFANTASIA;
                    TxtCNAE.Text = value.CNAE;
                    txtIEST.Text = value.IEST;
                    txtCRT.Text = value.CRT;
                   
                    txtMsgInicialNFe.Text = value.MSGINICIALNFE;                 

                    cbbSchema.Text = value.SCHEMAXML;

                    if (value.CASADECPRINTDANFE == "2")
                        rbCasaDecDuas.Checked = true;
                    else if (value.CASADECPRINTDANFE == "3")
                        rbCasaDecTres.Checked = true;
                    else if (value.CASADECPRINTDANFE == "4")
                        rbCasaDecQuatro.Checked = true;
                   
                    chkExibCodRefe.Checked = value.FLAGCODREFERENCIA.TrimEnd() == "S" ? true : false;                    
                    chkPedidoMT.Checked = value.FLAGPEDIDOMT.TrimEnd() == "S" ? true : false;
                    chkEstoqueNegativo.Checked = value.ESTOQUENEGATIVO.TrimEnd() == "S" ? true : false;
                    chkExibirCPFCNPJPedido.Checked = value.FLAGCPFCNPJPEDIDO.TrimEnd() == "S" ? true : false;                  
                    chkBaixaEstoqueNFe.Checked = value.FLAGBAIXAESTOQUENF.TrimEnd() == "S" ? true : false;
                    chkLimitCredCliente.Checked = value.FLAGLIMITECREDITO.TrimEnd() == "S" ? true : false;
                    chkMsgUsuario.Checked = value.FLAGMSGFECHA.TrimEnd() == "S" ? true : false;                   
                    chkBackupAtu.Checked = value.FLABACKUP.TrimEnd() == "S" ? true : false;
                    chkCodRefNfe.Checked = value.FLAGCODREFNFE.TrimEnd() == "S" ? true : false;

                    TxtTokenIMEXApp.Text = value.TOKENIMEXAPP;
                    txtIDASPNETUSERSINCLUSAO.Text = value.IDASPNETUSERSINCLUSAO;
                    TxtIDEmpresaIMEXAPP.Text = value.IDEMPRESAIMEXAPP;
                    txtIDRepresIMEXAPP.Text = value.IDREPRESIMEXAPP;
                    chkFlagIMEXApp.Checked = value.FLAGIMEXAPP.TrimEnd() == "S" ? true : false;

                    errorProvider1.Clear();
                }
               
            }
        }

        int _IDARQUIVOBINARIO = -1;
        byte[] _FOTO = null;
        public ARQUIVOBINARIOEntity Entity2
        {
            get
            {
                string NOME = txtNomeFoto.Text;
                string TIPO = openFileDialog1.FileName.ToString();
                string OBSERVACAO = string.Empty;

                return new ARQUIVOBINARIOEntity(_IDARQUIVOBINARIO, NOME, TIPO, OBSERVACAO, _FOTO);
            }
            set
            {

                if (value != null)
                {
                    _IDARQUIVOBINARIO = value.IDARQUIVOBINARIO;
                    txtNomeFoto.Text = value.NOME;
                 
                    _FOTO = value.FOTO;
                    MemoryStream stream = new MemoryStream(_FOTO);
                    pictureBox1.Image = Image.FromStream(stream);

                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    _IDARQUIVOBINARIO = -1;
                    _FOTO = null;
                    pictureBox1.Image = null;
                    txtNomeFoto.Text = string.Empty;
                }
            }
        }


        private void FrmConfigSistema_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            btnMaquina.Image = Util.GetAddressImage(7);
            btnSalva.Image = Util.GetAddressImage(15);
            btnSair.Image = Util.GetAddressImage(21);

            try
            {
                GetDropBoletaBancaria();
                GetDropVersaoXML();
                btnCadBoleto.Image = Util.GetAddressImage(6);

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

                try
                {
                   
                    CONFISISTEMATy = CONFISISTEMAP.Read(1);
                    Entity = CONFISISTEMATy;

                    int? CodArquivoBinario1 = CONFISISTEMATy.IDARQUIVOBINARIO1;

                    if (CodArquivoBinario1 != null)
                        Entity2 = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMATy.IDARQUIVOBINARIO1));
                    else
                    {
                        Entity2 = null;
                        _FOTO = null;
                        pictureBox1.Image = null;
                        txtNomeFoto.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Erro onload: " + ex.Message);
                }

                GetDropMunicipio();
               
                cbMunicipio.SelectedValue =CONFISISTEMATy.CODMUNIBGE;            

                chkPedidoOtica.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica == "S")
                    chkPedidoOtica.Checked = true;

                ChkExibirConhTransporte.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagExibirConhTransporte == "S")
                    ChkExibirConhTransporte.Checked = true;              

                chkAutoMecanica.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagAutoMecanica == "S")
                    chkAutoMecanica.Checked = true;

                         chkFestaEventos.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")     
                        chkFestaEventos.Checked = true;            

                chkNaoControlaEstoque.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque == "S")
                    chkNaoControlaEstoque.Checked = true;

                 chkFestaEventos.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")
                    chkFestaEventos.Checked = true;

                 chkTerraplenagem.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "S")
                     chkTerraplenagem.Checked = true;

                 chekvendobriPedido.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagVendedorObrigatorio == "S")
                     chekvendobriPedido.Checked = true;

                 chcentrocustoobrigapedido.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio == "S")
                     chcentrocustoobrigapedido.Checked = true;

                 chkOrdenarProdutoCodigoPedido.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagOrdenarProdutoPedido == "S")
                     chkOrdenarProdutoCodigoPedido.Checked = true;

                 chkTelaClienteResumida.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida == "S")
                     chkTelaClienteResumida.Checked = true;

                 chkTelaProdutoResumida.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida == "S")
                     chkTelaProdutoResumida.Checked = true;

                 chkMetroLinear.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagMetroLinear == "S")
                     chkMetroLinear.Checked = true;

                 chkAluguelRoupa.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa == "S")
                     chkAluguelRoupa.Checked = true;

                 chkTelemarketing.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagTelemarketing == "S")
                     chkTelemarketing.Checked = true;

                 chkEmissorNFe.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe == "S")
                     chkEmissorNFe.Checked = true;

                 chkSpedSintegra.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagSpedSintegra == "S")
                     chkSpedSintegra.Checked = true;

                 chkContador.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe == "S")
                     chkContador.Checked = true;

                 chkVenctoSuporte.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte == "S")
                     chkVenctoSuporte.Checked = true;

                 chkSinconDados.Checked = false;
                 if (BmsSoftware.ConfigSistema1.Default.DownloadSicron == "S")
                     chkSinconDados.Checked = true;

                 chkAvisoRenovacaoSuporte.Checked = true;
                 if (BmsSoftware.ConfigSistema1.Default.FlagExibirMsgRenovacaoSuporte == "S")
                    chkAvisoRenovacaoSuporte.Checked = true;
                 else if (BmsSoftware.ConfigSistema1.Default.FlagExibirMsgRenovacaoSuporte == "N")
                    chkAvisoRenovacaoSuporte.Checked = false;

                chkMesas.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                    chkMesas.Checked = true;

                chPedidBalcao.Checked = false;
                if (BmsSoftware.ConfigSistema1.Default.PedidoBalcao == "S")
                    chPedidBalcao.Checked = true;
              
                 if(BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS.Trim() != string.Empty)
                     cbModeloImpressao.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS) - 1;

                txtPorcAcrescimoPedid.Text = BmsSoftware.ConfigSistema1.Default.AcrescimoPedido;

                OpenWebServiceNfe();

                this.Cursor = Cursors.Default;	
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;	
                 MessageBox.Show(ConfigMessage.Default.MsgErroOpenRegistro);
            }
        }

        private void OpenWebServiceNfe()
        {
            try
            {
                txtRecepcao.Text = BmsSoftware.WebServiceNfe.Default.Recepcao;
                txtRetRecepcao.Text = BmsSoftware.WebServiceNfe.Default.RetRecepcao;
                txtInutilizacao.Text = BmsSoftware.WebServiceNfe.Default.Inutilizacao;
                txtConsultaProtocolo.Text = BmsSoftware.WebServiceNfe.Default.ConsultaProtocolo;
                txtStatusServico.Text = BmsSoftware.WebServiceNfe.Default.StatusServico;
                txtConsultaCadastro.Text = BmsSoftware.WebServiceNfe.Default.ConsultaCadastro;
                txtRecepcaoEvento.Text = BmsSoftware.WebServiceNfe.Default.RecepcaoEvento;
                txtAutorizacao.Text = BmsSoftware.WebServiceNfe.Default.Autorizacao;
                txtRetAutorizacao.Text = BmsSoftware.WebServiceNfe.Default.RetAutorizacao;
                txtDownloadNF.Text = BmsSoftware.WebServiceNfe.Default.DownloadNFe;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropMunicipio()
        {
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(null, "MUNICIPIO");

            cbMunicipio.DisplayMember = "MUNIUF";
            cbMunicipio.ValueMember = "COD_MUN_IBGE";

            LIS_MUNICIPIOSEntity LIS_MUNICIPIOSTy = new LIS_MUNICIPIOSEntity();
            LIS_MUNICIPIOSTy.MUNICIPIO = ConfigMessage.Default.MsgDrop;
            LIS_MUNICIPIOSTy.COD_MUN_IBGE = -1;
            LIS_MUNICIPIOSColl.Add(LIS_MUNICIPIOSTy);

            Phydeaux.Utilities.DynamicComparer<LIS_MUNICIPIOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MUNICIPIOSEntity>(cbMunicipio.DisplayMember);

            LIS_MUNICIPIOSColl.Sort(comparer.Comparer);
            cbMunicipio.DataSource = LIS_MUNICIPIOSColl;
        }

        private void GetDropVersaoXML()
        {
            VERSAOXMLNFEProvider VERSAOXMLNFEP = new VERSAOXMLNFEProvider();

            cbVersaoXml.DisplayMember = "NOME";
            cbVersaoXml.ValueMember = "IDVERSAOXMLNFE";

           cbVersaoXml.DataSource = VERSAOXMLNFEP.ReadCollectionByParameter(null);
        }

        private void GetDropBoletaBancaria()
        {
            CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
            CONFIGBOLETAColl = CONFIGBOLETAP.ReadCollectionByParameter(null, "NOME");

            cbBoleta.DisplayMember = "NOME";
            cbBoleta.ValueMember = "IDCONFIGBOLETA";

            CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
            CONFIGBOLETATy.NOME = ConfigMessage.Default.MsgDrop;
            CONFIGBOLETATy.IDCONFIGBOLETA = -1;
            CONFIGBOLETAColl.Add(CONFIGBOLETATy);

            Phydeaux.Utilities.DynamicComparer<CONFIGBOLETAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONFIGBOLETAEntity>(cbBoleta.DisplayMember);

            CONFIGBOLETAColl.Sort(comparer.Comparer);
            cbBoleta.DataSource = CONFIGBOLETAColl;

            cbBoleta.SelectedIndex = 0;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                    try
                    {
                        //Condição para salvar foto
                        if (txtNomeFoto.Text != string.Empty)
                            _IDARQUIVOBINARIO = ARQUIVOBINARIOP.Save(Entity2);
                    }
                    catch (Exception ex)
                    {
                        
                       MessageBox.Show("Erro Técnico Foto: " + ex.Message);
                    }

                    try
                    {
                           
                        if (chkPedidoOtica.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica = "N";

                        if (ChkExibirConhTransporte.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagExibirConhTransporte = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagExibirConhTransporte = "N";

                        if (chkAutoMecanica.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagAutoMecanica = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagAutoMecanica = "N";

                        if (chkFestaEventos.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagFestaEventos = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagFestaEventos = "N";

                        if (chkNaoControlaEstoque.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque = "N";

                        if (chkTerraplenagem.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem = "N";

                        if (chekvendobriPedido.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagVendedorObrigatorio = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagVendedorObrigatorio = "N";

                        if (chcentrocustoobrigapedido.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio = "N";                       

                        if (chkOrdenarProdutoCodigoPedido.Checked)
                               BmsSoftware.ConfigSistema1.Default.FlagOrdenarProdutoPedido = "S";
                        else
                               BmsSoftware.ConfigSistema1.Default.FlagOrdenarProdutoPedido = "N";

                        if (chkTelaClienteResumida.Checked)
                            BmsSoftware.ConfigSistema1.Default.FlagClienteResumida = "S";
                        else
                            BmsSoftware.ConfigSistema1.Default.FlagClienteResumida = "N";

                         if (chkTelaProdutoResumida.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida = "S";
                        else
                             BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida = "N";                       

                         if (chkMetroLinear.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagMetroLinear = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagMetroLinear = "N";

                         if (chkAluguelRoupa.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa = "N";

                         if (chkTelemarketing.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagTelemarketing = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagTelemarketing = "N";

                         if (chkEmissorNFe.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe = "N";

                         if (chkSpedSintegra.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagSpedSintegra = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagSpedSintegra = "N";

                         if (chkContador.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagContadorNFe = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagContadorNFe = "N";

                         if (chkVenctoSuporte.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte = "N";

                         if (chkSinconDados.Checked)
                             BmsSoftware.ConfigSistema1.Default.DownloadSicron = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.DownloadSicron = "N";

                         if (chkAvisoRenovacaoSuporte.Checked)
                             BmsSoftware.ConfigSistema1.Default.FlagExibirMsgRenovacaoSuporte = "S";
                         else
                             BmsSoftware.ConfigSistema1.Default.FlagExibirMsgRenovacaoSuporte = "N";

                    if (chPedidBalcao.Checked)
                        BmsSoftware.ConfigSistema1.Default.PedidoBalcao = "S";
                    else
                        BmsSoftware.ConfigSistema1.Default.PedidoBalcao = "N";

                    if (chkMesas.Checked)
                        BmsSoftware.ConfigSistema1.Default.HabilitarMesas = "S";
                    else
                        BmsSoftware.ConfigSistema1.Default.HabilitarMesas = "N";                 

                         BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS = Convert.ToString(cbModeloImpressao.SelectedIndex + 1);

                    BmsSoftware.ConfigSistema1.Default.AcrescimoPedido = txtPorcAcrescimoPedid.Text;

                    CONFISISTEMAP.Save(Entity);      
                             BmsSoftware.ConfigSistema1.Default.Save();
                             SaveWebServiceNfe();
                             SaveRegistroWebService();
                             SaveRegistro();
                             Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                    }
                    catch (Exception ex)
                    {
                        
                        MessageBox.Show("Erro Técnico CONFISISTEMA: " + ex.Message);
                    }                   
                
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
               MessageBox.Show("Erro Técnico: " +ex.Message);
            }
        }
        private void SaveWebServiceNfe()
        {
            try
            {
                 BmsSoftware.WebServiceNfe.Default.Recepcao = txtRecepcao.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.RetRecepcao = txtRetRecepcao.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.Inutilizacao = txtInutilizacao.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.ConsultaProtocolo = txtConsultaProtocolo.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.StatusServico = txtStatusServico.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.ConsultaCadastro = txtConsultaCadastro.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.RecepcaoEvento = txtRecepcaoEvento.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.Autorizacao = txtAutorizacao.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.RetAutorizacao = txtRetAutorizacao.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.DownloadNFe = txtDownloadNF.Text.Trim();
                 BmsSoftware.WebServiceNfe.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o WebService da NFe");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SaveRegistroWebService()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("nfe", true);

            try
            {
                key.SetValue("NfeRecepcao", txtRecepcao.Text.Trim());
                key.SetValue("NfeRetRecepcao", txtRetRecepcao.Text.Trim());
                key.SetValue("NfeInutilizacao", txtInutilizacao.Text.Trim());
                key.SetValue("NfeConsultaProtocolo", txtConsultaProtocolo.Text.Trim());
                key.SetValue("NfeStatusServico", txtStatusServico.Text.Trim());
                key.SetValue("NfeConsultaCadastro", txtConsultaCadastro.Text.Trim());
                key.SetValue("NFeRecepcaoEvento", txtRecepcaoEvento.Text.Trim());
                key.SetValue("NFeAutorizacao", txtAutorizacao.Text.Trim());
                key.SetValue("NFeRetAutorizacao", txtRetAutorizacao.Text.Trim());
                key.SetValue("NfeDownloadNF", txtDownloadNF.Text.Trim());
                key.Close();
            }
            catch (Exception ex)
            {
                key.Close();
                MessageBox.Show("Erro técnico: " + ex.Message);
                MessageBox.Show("Erro ao chave nfe");
            }
        }

        private void SaveRegistro()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("nfe", true);
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"nfe\eMail", true);
            RegistryKey key3 = Registry.CurrentUser.OpenSubKey(@"nfe\Proxy", true);
           // RegistryKey key4 = Registry.CurrentUser.OpenSubKey("nfeapp", true);
            try
            {
                if (key == null)
                    key = Registry.CurrentUser.CreateSubKey("nfe");
                else
                {
                   
                    //1 Produção - 2 Homologação
                    string Ambiente =  rbProducaoNFe.Checked == true ? "1" : "2";
                    key.SetValue("Ambiente", Ambiente);

                    //Dados Empresa
                    EMPRESAEntity EmpresaTy = new EMPRESAEntity();
                    EmpresaTy = EMPRESAP.Read(1);

                    string CNPJ = EmpresaTy.CNPJCPF.Replace(".", "").Replace("-", "").Replace("/", "");
                    key.SetValue("CNPJ", CNPJ);

                 //   if (!chkExibiLogoNFe.Checked)
                       key.SetValue("DanfeLogo", ConfigSistema1.Default.PathInstall + @"\nfe\logo.jpg");

                    string DataPacket = ConfigSistema1.Default.PathInstall + @"\nfe\NfeDtPkt.xtr";
                    key.SetValue("DataPacket", DataPacket);

                    string DataPacketFormSeg = ConfigSistema1.Default.PathInstall + @"\nfe\NfeDtPkt-FS.xtr";
                    key.SetValue("DataPacketFormSeg", DataPacketFormSeg);

                    string DataPacketFS = ConfigSistema1.Default.PathInstall + @"\nfe\NfeDtPkt-FS.xtr";
                    key.SetValue("DataPacketFS", DataPacketFormSeg);

                    string DataPacketCCe = ConfigSistema1.Default.PathInstall + @"\nfe\CCeDtPkt.xtr";
                    key.SetValue("DataPacketCCe", DataPacketCCe);

                    key.SetValue("Modelo", txtModeloNFE.Text);

                    //Senha de Validação versao nfedllcsh-v6.6.9
                  //  key.SetValue("val-ASS", "cmFmYWVsbWlyYW5kYTIwMDhAZ21haWwuY29tIzE4IzE3LzAyLzIwMTYjMDQxMDEzMjIwMDAxODgjSU1FWCBTSVNURU1BUyM3MjQ1QTk4N0JENjQ1NDZDMzdGQUJGNzZBMTA2QjA1QiM=");
                  //  key.SetValue("val-MD5", "A2A74AE1F4FBC7944F5C79FCF879C1C1");
                    

                    string FormatoItemUnt = ",0.0000";
                    string FormatoItemQtd = ",0.####";
                    string CASADECPRINTDANFE = CONFISISTEMAP.Read(1).CASADECPRINTDANFE;
                    if (CASADECPRINTDANFE == "2")
                    {
                        FormatoItemUnt = ",0.00";
                        FormatoItemQtd = ",0.##";
                    }
                    else if (CASADECPRINTDANFE == "3")
                    {
                        FormatoItemUnt = ",0.000";
                        FormatoItemQtd = ",0.###";
                    }
                    else if (CASADECPRINTDANFE == "4")
                    {
                        FormatoItemUnt = ",0.0000";
                        FormatoItemQtd = ",0.####";
                    }

                    key.SetValue("FormatoItemUnt", FormatoItemUnt);
                    key.SetValue("FormatoItemQtd", FormatoItemQtd);

                    key.SetValue("NoSerieCertificado", txtSerialCertDigital.Text);
                    key.SetValue("PathPrincipal", ConfigSistema1.Default.PathInstall );
                    key.SetValue("Serie", TxtSerieNFE.Text);
                    key.SetValue("UnidadeFederada", _UFEmit);
                    key.SetValue("VerProc", cbVersaoXml.Text);
                    key.SetValue("UnidadeFederadaCodigo", CodigodeUFIBGE(EMPRESAP.Read(1).UF).ToString().TrimEnd());
                    

                     key.SetValue("Schemas", "nfe\\schemas");

                     key.SetValue("indSinc", "0");
                    

                    //Registro Email
                    key2.SetValue("eMail", "");
                    key2.SetValue("Senha", "");
                    key2.SetValue("ServidorSMTP", "");
                    key2.SetValue("SSL", "0");
                    key2.SetValue("Porta", "");                    

                    //Proxy
                    key3.SetValue("CodMunicipio", txtCodMunIBGE.Text);
                    key3.SetValue("Host", "");
                    key3.SetValue("Porta", "");
                    key3.SetValue("Senha", "");
                    key3.SetValue("Usuario", "");
                    key3.SetValue("IE", EmpresaTy.IE.Replace(".", "").Replace("-", ""));
                    key3.SetValue("Municipio", txtCodUFIBGE.Text); 
                                    
                                       
                }
            }
            finally
            {
                key.Close();
                key2.Close();
                key3.Close();
              //  key4.Close();
            }
        }      

        public int CodigodeUFIBGE(string UF)
        {
            int uf = -1;
            switch (UF)
            {
                case "AC" :uf = 12; break;
                case "AL"  :uf = 27; break;
                case "AP" :uf = 16; break;
                case "AM" :uf =13; break;
                case "BA" :uf = 29; break;
                case "CE": uf = 23; break;
                case "DF" :uf =  53; break;
                case "GO" :uf = 52; break;
                case "MA" :uf = 21 ; break;
                case "MG" :uf = 31; break;
                case "ES" :uf = 32; break;
                case "MS" :uf = 50; break;
                case "MT" :uf = 51; break;
                case "PA" :uf = 15; break;
                case "PB" :uf = 25; break;
                case "PE" :uf = 26; break;
                case "PI"  :uf = 22; break;
                case "PR" :uf = 41; break;
                case "RJ" :uf = 33; break;
                case "RN" : uf = 24; break;
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


        private void FrmConfigSistema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void txtNomeFoto_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Logo para o relatório, recomendado o tamanho de: 180px de Largura  e 80px de Altura";
        }

        private void chkLogoRelatorio_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Logo para o relatório, recomendado o tamanho de: 180px de Largura  e 80px de Altura";
        }

        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtNomeFoto.Text = openFileDialog1.FileName.ToString();

                //Salva no regedit
                RegistryKey key = Registry.CurrentUser.OpenSubKey("nfe", true);
                key.SetValue("DanfeLogo", txtNomeFoto.Text);
                key.Close();

                _FOTO = GetFoto(openFileDialog1.FileName);

                MemoryStream stream = new MemoryStream(_FOTO);
                pictureBox1.Image = Image.FromStream(stream);

                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                //Redimensionando Imagens
                if (txtNomeFoto.Text != string.Empty)
                {
                    if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\Images\fundologo.jpg"))
                        File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\Images\fundologo.jpg");

                    SaveFileDialog dialog = new SaveFileDialog();
                   // int width = Convert.ToInt32(pictureBox1.Height);
                   // int height = Convert.ToInt32(pictureBox1.Width);

                     int width = 222;
                     int height = 76;

                    Bitmap bmp = new Bitmap( width, height);
                    pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                 
                    bmp.Save(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\Images\fundologo.jpg");                   

                    txtNomeFoto.Text = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\Images\fundologo.jpg";
                    _FOTO = GetFoto(txtNomeFoto.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
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

        private void linkExcluirFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDARQUIVOBINARIO == -1)
            {
                if (txtNomeFoto.Text != string.Empty)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        _FOTO = null;
                        txtNomeFoto.Text = string.Empty;
                        pictureBox1.Image = null;
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        int IDARQUIVOBINARIO = _IDARQUIVOBINARIO;
                        _IDARQUIVOBINARIO = -1;
                        CONFISISTEMAP.Save(Entity);
                        ARQUIVOBINARIOP.Delete(IDARQUIVOBINARIO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity2 = null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                    }
                }
            }
        }

        private void cbBoleta_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Selecione a boleta bancária para a cobrança.";
           
        }

        private void btnCadBoleto_Click(object sender, EventArgs e)
        {

            using (FrmBoletaBancaria frm = new FrmBoletaBancaria())
            {
                int CodSelec = Convert.ToInt32(cbBoleta.SelectedValue);
                frm._IDCONFIGBOLETA = CodSelec;
                frm.ShowDialog();
                GetDropBoletaBancaria();
                cbBoleta.SelectedValue = CodSelec;
            }
        }       

        private void txtPortaMatricial_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Para imprimir local, digite LPT1 ou outra porta.";
        }
        
        private void txtPrazoOS_Enter(object sender, EventArgs e)
        {
             lblobsField.Text = "Prazo de entrega para a Ordem de Serviço";
        }

        private void txtALigISSQN_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtALigISSQN_Enter(object sender, EventArgs e)
        {
       

        }

        private void txtAliqPIS_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtAliqCOFINS_Validating(object sender, CancelEventArgs e)
        {
          
        }

        private void txtAliqPIS_Enter(object sender, EventArgs e)
        {

        }
        private void txtAliqCOFINS_Enter(object sender, EventArgs e)
        {
            
        }

        private void cbMunicipio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMunicipio.SelectedValue) > 0)
            {
                MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();
                MUNICIPIOSEntity MUNICIPIOSTy = new MUNICIPIOSEntity();
                MUNICIPIOSTy = MUNICIPIOSP.Read(Convert.ToInt32(cbMunicipio.SelectedValue));
                ESTADOProvider ESTADOP = new ESTADOProvider();

                if (MUNICIPIOSTy != null)
                {
                    txtCodMunIBGE.Text = MUNICIPIOSTy.COD_MUN_IBGE.ToString();
                    txtCodUFIBGE.Text = MUNICIPIOSTy.COD_UF_IBGE.ToString();
                    
                    //Busca dados do Estado
                    _UFEmit = ESTADOP.Read(Convert.ToInt32(txtCodUFIBGE.Text)).UF;
                }
            }
        }

        private void cbMunicipio_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Selecione a cidade ou pressione Ctrl+E para pesquisar.";
        }

        private void cbMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchCidade frm = new FrmSearchCidade())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        cbMunicipio.SelectedValue = result;
                }
            }
        }

        private void btnCertDigital_Click(object sender, EventArgs e)
        {
            SelecionarCertificado();
        }

        public void SelecionarCertificado()
        {
            X509Certificate2 oCertificado;
            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
                MessageBox.Show(msgResultado, "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                oX509Cert = scollection[0];
                oCertificado = oX509Cert;
                txtNameCertDigital.Text = oCertificado.IssuerName.Name;
                txtSerialCertDigital.Text = oCertificado.SerialNumber;
                txtValidadeCertDigital.Text = oCertificado.NotBefore + " à " + oCertificado.NotAfter;
            }
        }

        private void txtCRT_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "1 – Simples Nacional; 2 – Simples Nacional – excesso de sublimite de receita bruta; 3 – Regime Normal";
        }

        private void button1_Click(object sender, EventArgs e)
        {

          
        }

        private void rbProducaoNFe_Click(object sender, EventArgs e)
        {
          
        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void txtRazaoSocialEmpresa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Selecione a pasta";
            openFileDialog3.CheckFileExists = false;

            openFileDialog3.FileName = "[Obter Pasta…]";
            openFileDialog3.Filter = "Folders|no.files";

            openFileDialog3.ShowDialog(); 
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(cbVersaoXml.Text == "3.10")
                PreencherWebService_3_10();
            else if (cbVersaoXml.Text == "4.00")
                PreencherWebService_4_00();
        }

        private void PreencherWebService_4_00()
        {
            try
            {
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                if (EMPRESATy.UF == "BA")
                {
                    txtInutilizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx".Trim();
                    txtStatusServico.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx".Trim();
                    txtConsultaCadastro.Text = "https://nfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx".Trim();
                    txtAutorizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx".Trim();
                    txtRetAutorizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx".Trim();
                }
                else if (EMPRESATy.UF == "CE")
                {

                }
                else if (EMPRESATy.UF == "GO")
                {
                    txtInutilizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsd".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl".Trim();
                    txtStatusServico.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl".Trim();
                    txtConsultaCadastro.Text = "https://nfe.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl".Trim();
                    txtAutorizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl".Trim();
                    txtRetAutorizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl".Trim();                  
                }
                else if (EMPRESATy.UF == "MA")
                {
                    txtInutilizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx".Trim();
                    txtConsultaProtocolo.Text = "https://www.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx".Trim();
                    txtStatusServico.Text = "https://www.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx".Trim();
                    txtRecepcaoEvento.Text = "https://www.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx";
                    txtAutorizacao.Text = "	https://www.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx".Trim();
                    txtRetAutorizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx".Trim();
                }
                else if (EMPRESATy.UF == "MG")
                {
                 
                    txtRecepcao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"; //NfeRecepcao	2.00 / 3.10
                    txtRetRecepcao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"; //NfeRetRecepcao	2.00 / 3.10
                    txtInutilizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2"; //NfeInutilizacao	2.00 / 3.10
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2"; //NfeConsultaProtocolo	2.00 / 3.10
                    txtStatusServico.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2"; //NfeStatusServico	2.00 / 3.10
                    txtConsultaCadastro.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2"; //NfeConsultaCadastro	2.00
                    txtRecepcaoEvento.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento"; //RecepcaoEvento	1.00
                    txtAutorizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao";//NFeAutorizacao	3.10
                    txtRetAutorizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"; //NFeRetAutorizacao	3.10

                    //Versao 4.00
                    txtInutilizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4";
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsulta4";
                    txtStatusServico.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4";
                    txtAutorizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4";
                    txtRetAutorizacao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4";

                }
                else if (EMPRESATy.UF == "MS")
                {
                    txtInutilizacao.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeInutilizacao4".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeConsultaProtocolo4".Trim();
                    txtStatusServico.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeStatusServico4".Trim();
                    txtConsultaCadastro.Text = "https://nfe.fazenda.ms.gov.br/ws/CadConsultaCadastro4".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeRecepcaoEvento4".Trim();
                    txtAutorizacao.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeAutorizacao4".Trim();
                    txtRetAutorizacao.Text = "https://nfe.fazenda.ms.gov.br/ws/NFeRetAutorizacao4".Trim();
                }
                else if (EMPRESATy.UF == "MT")
                {
                    txtInutilizacao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4?wsdl".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl".Trim();
                    txtStatusServico.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4?wsdl".Trim();
                    txtConsultaCadastro.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4?wsdll".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4?wsdl".Trim();
                    txtAutorizacao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4?wsd".Trim();
                    txtRetAutorizacao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4?wsdl".Trim();
                }
                else if (EMPRESATy.UF == "PE")
                {

                }
                else if (EMPRESATy.UF == "PR")
                {
                    txtInutilizacao.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl".Trim();
                    txtStatusServico.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl".Trim();
                    txtConsultaCadastro.Text = "https://nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl".Trim();
                    txtAutorizacao.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl".Trim();
                    txtRetAutorizacao.Text = "https://nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl".Trim();
                }
                else if (EMPRESATy.UF == "RS")
                {
                    txtInutilizacao.Text = "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx".Trim();
                    txtStatusServico.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx".Trim();
                    txtConsultaCadastro.Text = "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx".Trim();
                    txtAutorizacao.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx".Trim();
                    txtRetAutorizacao.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx".Trim();
                } 
                else if (EMPRESATy.UF == "SP")
                {
                    txtInutilizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx".Trim();
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx".Trim();
                    txtStatusServico.Text = "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx".Trim();
                    txtConsultaCadastro.Text = "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx".Trim();
                    txtRecepcaoEvento.Text = "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx".Trim();
                    txtAutorizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx".Trim();
                    txtRetAutorizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx".Trim();
                }               
                else if (EMPRESATy.UF == "ES")
                {
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx";
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx";
                }
                else if (EMPRESATy.UF == "AC")
                {
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx";
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx";
                }                
                else if (EMPRESATy.UF == "AM")
                {
                  
                   
                }                
                else if (EMPRESATy.UF == "SC")
                {
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtRecepcao.Text = "";
                    txtRetRecepcao.Text = "";
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";

                    //Versao 4.0
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx";
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx";

                }
                else if (EMPRESATy.UF == "PA")
                {
                    
                }
                else if (EMPRESATy.UF == "DF")
                {
                   
                }
                else
                {
                    MessageBox.Show("Não foi localizado web service a UF: " + EMPRESATy.UF);
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro ao preencher o web services.");
                MessageBox.Show("Erro técnico: " + EX.Message);
            }



        }

        private void PreencherWebService_3_10()
        {
            try
            {
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();            
                EMPRESATy = EMPRESAP.Read(1);

                if(EMPRESATy.UF == "MG")
                {
                    txtRecepcao.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"; //NfeRecepcao	2.00 / 3.10
                    txtRetRecepcao.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"; //NfeRetRecepcao	2.00 / 3.10
                    txtInutilizacao.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2"; //NfeInutilizacao	2.00 / 3.10
                    txtConsultaProtocolo.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2"; //NfeConsultaProtocolo	2.00 / 3.10
                    txtStatusServico.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2"; //NfeStatusServico	2.00 / 3.10
                    txtConsultaCadastro.Text ="https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2"; //NfeConsultaCadastro	2.00
                    txtRecepcaoEvento.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento"; //RecepcaoEvento	1.00
                    txtAutorizacao.Text= "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao";//NFeAutorizacao	3.10
                    txtRetAutorizacao.Text="https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"; //NFeRetAutorizacao	3.10
                    
                }
                else if (EMPRESATy.UF == "RS")
                {
                    txtRecepcao.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtRetRecepcao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2";
                    txtInutilizacao.Text = "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"; 
                    txtConsultaProtocolo.Text ="https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"; 
                    txtConsultaCadastro.Text = "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"; 
                    txtAutorizacao.Text = "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text ="https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text="https://nfe.sefazrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "PR")
                {
                    txtRecepcao.Text = "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2";
                    txtRetRecepcao.Text = "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2";
                    txtInutilizacao.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3";
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeConsulta3";
                    txtStatusServico.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3";
                    txtConsultaCadastro.Text ="https://nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2"; 
                    txtRecepcaoEvento.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento";
                    txtAutorizacao.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3";
                    txtRetAutorizacao.Text = "https://nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3";
                }
                else if (EMPRESATy.UF == "SP")
                {
                    txtRecepcao.Text = "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx";
                   // txtRetRecepcao.Text = ""; https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2";
                    txtInutilizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx";
                    txtConsultaCadastro.Text = "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx";
                    txtAutorizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "CE")
                {
                    txtRecepcao.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl";
                    txtRetRecepcao.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl";
                    txtInutilizacao.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl";
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl";
                    txtStatusServico.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl";
                    txtConsultaCadastro.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl";
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl";
                    txtAutorizacao.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl";
                    txtRetAutorizacao.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl";
                    txtDownloadNF.Text = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl";
                }
                else if (EMPRESATy.UF == "ES")
                {
                    txtRecepcao.Text = "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2";
                    txtRetRecepcao.Text = "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2";
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "	https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
                    txtConsultaCadastro.Text = "https://app.sefaz.es.gov.br/ConsultaCadastroService/CadConsultaCadastro2.asmx2";
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://nfe.svrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "AC")
                {
                  
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx";
                    txtRecepcao.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx";
                    txtRetRecepcao.Text = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://nfe.sefazrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "MT")
                {
                    txtRecepcao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl";
                    txtRetRecepcao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl";
                    txtInutilizacao.Text ="https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsd";
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl";
                    txtStatusServico.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl";
                    txtConsultaCadastro.Text ="https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl";
                    txtRecepcaoEvento.Text ="https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl";
                    txtAutorizacao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl";
                    txtRetAutorizacao.Text = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl";
                    txtDownloadNF.Text = "";
                }
                else if (EMPRESATy.UF == "AM")
                {
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento";
                    txtRecepcao.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2";
                    txtRetRecepcao.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2";
                    txtInutilizacao.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2";
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2";
                    txtStatusServico.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2";
                    txtConsultaCadastro.Text = "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2";
                    txtAutorizacao.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao";
                    txtRetAutorizacao.Text = "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao";
                    txtDownloadNF.Text = "";
                }
                else if (EMPRESATy.UF == "BA")
                {
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx";
                    txtRecepcao.Text = "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx";
                    txtRetRecepcao.Text = "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx";
                    txtInutilizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx";
                    txtStatusServico.Text = "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx";
                    txtConsultaCadastro.Text = "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx";
                    txtAutorizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "SC")
                {
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtRecepcao.Text = "";
                    txtRetRecepcao.Text = "";
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "PA")
                {
                    txtRecepcaoEvento.Text = "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx";
                    txtRecepcao.Text = "";
                    txtRetRecepcao.Text = "";
                    txtInutilizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx";
                    txtConsultaCadastro.Text = "";
                    txtAutorizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";
                }
                else if (EMPRESATy.UF == "DF")
                {
                    txtConsultaCadastro.Text = "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx";
                    txtRecepcaoEvento.Text = "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
                    txtInutilizacao.Text = "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
                    txtAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
                    txtDownloadNF.Text = "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx";

                    txtRecepcao.Text = "";
                    txtRetRecepcao.Text = "";
                    
                }
                else if (EMPRESATy.UF == "GO")
                {
                    txtConsultaCadastro.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl";
                    txtRecepcaoEvento.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl";
                    txtInutilizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl";
                    txtConsultaProtocolo.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl";
                    txtStatusServico.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl";
                    txtAutorizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl";
                    txtRetAutorizacao.Text = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl";
                    txtDownloadNF.Text = "";

                    txtRecepcao.Text = "";
                    txtRetRecepcao.Text = "";

                }
                else if (EMPRESATy.UF == "MA")
                {
                    txtInutilizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx";
                    txtConsultaProtocolo.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx";
                    txtStatusServico.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx";
                    txtAutorizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx";
                    txtRetAutorizacao.Text = "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx";
                }
                else
                {
                    MessageBox.Show("Não foi localizado web service a UF: " + EMPRESATy.UF);
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro ao preencher o web services.");
                MessageBox.Show("Erro técnico: " + EX.Message);
            }

            

        }

        private void btnVerServicoNFe_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {


                nfec.nfecsharp nfe = new nfec.nfecsharp();
                string msgStatusServico = nfe.NfeStatusServico();

                if (msgStatusServico != string.Empty)
                    MessageBox.Show(msgStatusServico);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void msktDtSuporte_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }
       
    }
}

