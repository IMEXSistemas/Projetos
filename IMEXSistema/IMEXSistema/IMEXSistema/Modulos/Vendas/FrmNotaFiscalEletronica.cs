using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using System.IO;
using BmsSoftware.UI;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using BmsSoftware.Modulos.Servicos;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web.Services;
using VVX;
using BmsSoftware.Modulos.Financeiro;
using Microsoft.Win32;
using System.Diagnostics;
using BmsSoftware.Classes.BMSworks.UI;
using winfit.Modulos.Outros;
using System.Collections;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Modulos.FrmSintegra;
using System.Net;
using BMSSoftware.Modulos.Operacional;
using System.Net.Mail;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmNotaFiscalEletronica : Form
    {
        Utility Util = new Utility();

        int paginaAtual = 0;
        int IndexRegistro = 0;
        string RelatorioTitulo = string.Empty;
        string MensagemErroBloqueio = string.Empty;
        string FLAGCOMISSAONFE = string.Empty;
        string FLAGBASEISSQN = string.Empty;
        Boolean _DANFEVALIDO = false;

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        CFOPCollection CFOPColl = new CFOPCollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        CLASSFISCALCollection CLASSFISCALColl = new CLASSFISCALCollection();
        LIS_CSTCollection LIS_CSTColl = new LIS_CSTCollection();
        LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        CFOPCollection CFOProdutoPColl = new CFOPCollection();
        LIS_SERVICONFECollection LIS_SERVICONFEColl = new LIS_SERVICONFECollection();

        CFOPProvider CFOPP = new CFOPProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CLASSFISCALProvider CLASSFISCALP = new CLASSFISCALProvider();
        NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
        LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();
        PRODUTONFEProvider PRODUTONFEP = new PRODUTONFEProvider();
        LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();
        TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
        ESTADOProvider EstadoP = new ESTADOProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        MENSAGEMProvider MENSAGEMNP = new MENSAGEMProvider();
        ESTOQUELOTEProvider ESTOQUELOTEP = new ESTOQUELOTEProvider();
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();

        CLASSFISCALEntity CLASSFISCALTY = new CLASSFISCALEntity();
        TRANSPORTADORAEntity TranspotadorNFEspTy = new TRANSPORTADORAEntity();
        ESTADOEntity EstadoNFEspTy = new ESTADOEntity();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        //Variaveis de validação da NFe
        Boolean NfeAssinar = false;
        Boolean NfeValidar = false;
        Boolean NfeCriarLote = false;
        Boolean ExibirMensagem = false;
        Boolean NfeSituacao = false;
        Boolean NfeAutorizacao = false;
        Boolean NfeConsultaProcessamento = false;

        public Boolean FlagExibirMensagem = false;
        public Boolean ExibiDados = false;

        //Gerar NFe atraves do pedido
        public int NumPedidoSimples = -1;
        public string _NOTAFISCALE = "-1";
        public Boolean FlagGerarNFePedidoSimples = false;
        
        public FrmNotaFiscalEletronica()
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
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmNotaFiscalEspelho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }

        }

        int _IDNOTAFISCALE = -1;
        string _FLAGARQUIVOXML = "N";
        string _FLAGASSINATURA = "N";
        string _FLAGCANCELADA = "N";
        string _FLAGENVIADA = "N";
        string _FLAGINUTILIZADO = "N";
        string _FLAGVALIDADA = "N";
        string _ARQUIVOLOTE = string.Empty;
        string _RECIBONFE = string.Empty;
        string _SITUACAONFE = string.Empty;
        public NOTAFISCALEEntity Entity
        {
            get
            {
                string NOTAFISCALE = txtNotaFiscal.Text;

                //Apaga na configuração a nota fiscal inicial
                if (CONFISISTEMATy.NOTAFISCALINICIAL != string.Empty)
                {
                    CONFISISTEMATy.NOTAFISCALINICIAL = string.Empty;
                    CONFISISTEMAP.Save(CONFISISTEMATy);
                }

                string SERIE = txtSerieNF.Text.Trim();
                 int  IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);

                DateTime? DTSAIDA = null;
                if (mskDtSaida.Text != "  /  /")
                    DTSAIDA = Convert.ToDateTime(mskDtSaida.Text);

                string HORASAIDA = string.Empty;
                if (mkdHoraSaida.Text == "  :")
                    mkdHoraSaida.Text = string.Empty;
                else
                {
                    HORASAIDA = String.Format("{0:HH:mm:ss}", mkdHoraSaida.Text);
                    HORASAIDA = HORASAIDA + ":00";
                }
                int IDTIPOMOVIM = rbSaida.Checked ? 2 : 1; //1-Entrara; 2-Saida
                int IDCFOP = Convert.ToInt32(cbCFOP.SelectedValue);
                string INSCESTSTRIB = string.Empty;

                if (TxtBaseCalcICMS.Text == string.Empty)
                    TxtBaseCalcICMS.Text = "0,00";
                decimal? BASECALCICMS = Convert.ToDecimal(TxtBaseCalcICMS.Text);

                if (TxtValorICMS.Text == string.Empty)
                    TxtValorICMS.Text = "0,00";
                decimal? VALORICMS = Convert.ToDecimal(TxtValorICMS.Text);

                if (txtBaseCalcICMSSubs.Text == string.Empty)
                    txtBaseCalcICMSSubs.Text = "0,00";
                decimal? BASECALCICMSLSUB = Convert.ToDecimal(txtBaseCalcICMSSubs.Text);

                if (TxtValorICMSSubst.Text == string.Empty)
                    TxtValorICMSSubst.Text = "0,00";
                decimal? VALORICMSSUB = Convert.ToDecimal(TxtValorICMSSubst.Text);

                if (txtValorFrete.Text == string.Empty)
                    txtValorFrete.Text = "0,00";
                decimal? VALORFRETE = Convert.ToDecimal(txtValorFrete.Text);

                if (txtValorSeguro.Text == string.Empty)
                    txtValorSeguro.Text = "0,00";
                decimal? VALORSEGURO = Convert.ToDecimal(txtValorSeguro.Text);

                if (TxtBaseCalcICMS.Text == string.Empty)
                    TxtBaseCalcICMS.Text = "0,00";
                decimal? OUTRADESPES = Convert.ToDecimal(txtOutraDespAcess.Text);

                if (txtValorTotalIPI.Text == string.Empty)
                    txtValorTotalIPI.Text = "0,00";
                decimal? TOTALIPI = Convert.ToDecimal(txtValorTotalIPI.Text);

                if (txtValorTotalProdutos.Text == string.Empty)
                    txtValorTotalProdutos.Text = "0,00";
                decimal? TOTALPRODUTOS = Convert.ToDecimal(txtValorTotalProdutos.Text);

                if (txtTotalNota.Text == string.Empty)
                    txtTotalNota.Text = "0,00";
                decimal? TOTALNOTA = Convert.ToDecimal(txtTotalNota.Text);

                int? IDVENDEDOR = null;
                if (cbFuncionario.SelectedIndex > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

                decimal VALORCOMISSAO = Convert.ToDecimal(txtValComissao.Text);

                int? IDTRANSPORTES = null;
                if (cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                string PLACA = txtPlaca.Text.ToUpper().Trim();

                string UFTRANSPORTE = txtUFTransp.Text;

                decimal QUANT = Convert.ToDecimal(txtQuant.Text);
                string ESPECIE = txtEspecie.Text.Trim();
                string MARCA = txtMarca.Text.Trim();
                string NUMEROS = txtNumeros.Text.Trim();

                if (txtPesoBruto.Text == string.Empty)
                    txtPesoBruto.Text = "0,00";
                decimal PESOBRUTO = Convert.ToDecimal(txtPesoBruto.Text);

                if (txtPesoLiquido.Text == string.Empty)
                    txtPesoLiquido.Text = "0,00";
                decimal PESOLIQUIDO = Convert.ToDecimal(txtPesoLiquido.Text);

                string INFOCOMPLEM = txtObservacao.Text.Trim();

                int? IDCENTROCUSTO = null;
                if (cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int? IDFORMAPAGTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                    IDFORMAPAGTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                int? IDLOCALCOBRANCA = null;
                if (cbLocalCobranca.SelectedIndex > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                int? IDSTATUS = null;
                if (cbStatus.SelectedIndex > -1)
                    IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);

                if (txtValorPago.Text == string.Empty)
                    txtValorPago.Text = "0,00";
                decimal VALORPAGO = Convert.ToDecimal(txtValorPago.Text);

                if (txtValorDev.Text == string.Empty)
                    txtValorDev.Text = "0,00";
                decimal VALORDEVEDOR = Convert.ToDecimal(txtValorDev.Text);

                int frete = cdFrete.SelectedIndex;

                decimal PORCDESCONTO = 0;
                decimal VALORDESCONTO = Convert.ToDecimal(txtDescontoProduto.Text);
                decimal PORCACRESCIMO = 0;
                decimal VALORACRESCIMO = 0;


                if (txtValorPis.Text == string.Empty)
                    txtValorPis.Text = "0,00";
                decimal VALORPIS = Convert.ToDecimal(txtValorPis.Text);

                if (txtconfins.Text == string.Empty)
                    txtconfins.Text = "0,00";
                decimal VALORCONFINS = Convert.ToDecimal(txtconfins.Text);

                string CODANTT = txtANTT.Text.Trim();

                if (txtValorTotalServico.Text == string.Empty)
                    txtValorTotalServico.Text = "0,00";
                decimal VALORTOTALSERVICO = Convert.ToDecimal(txtValorTotalServico.Text);

                if (txtBasCalcISSQN.Text == string.Empty)
                    txtBasCalcISSQN.Text = "0,00";
                decimal BASECALCISSQN = Convert.ToDecimal(txtBasCalcISSQN.Text);

                if (txtAliISSQN.Text == string.Empty)
                    txtAliISSQN.Text = "0,00";
                decimal ALIQISSQN = Convert.ToDecimal(txtAliISSQN.Text);

                if (txtValorISSQN.Text == string.Empty)
                    txtValorISSQN.Text = "0,00";
                decimal VALORISSQN = Convert.ToDecimal(txtValorISSQN.Text);

                string CHAVEACESSO = txtChaveAcesso.Text.Trim();

                decimal ALIQCREDICMS = Convert.ToDecimal(txtAliqCredICMS.Text);
                decimal VALORCREDICMS = Convert.ToDecimal(txtValorCredICMS.Text);

                string FLAGTIPOPAGAMENTO = string.Empty;
                if (rbAvista.Checked)
                    FLAGTIPOPAGAMENTO = "0";         				//<indPag> //0:Pagamento a vista - 1:Pagamento Prazo -2:Outrosx'
                else if (rbPrazo.Checked)
                    FLAGTIPOPAGAMENTO = "1";
                else if (rbOutros.Checked)
                    FLAGTIPOPAGAMENTO = "2";

                string NUMPEDIDO = txtNumPedido.Text.Trim();

                string FLAGDEVOLUCAO = chkDevolucao.Checked ? "S" : "N";
                string CHAVEDEVOLUCAO = txtNFReferenciaDevolucao.Text.Trim();
                
                string CNPJEMISSOR = "";
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);
                CNPJEMISSOR = EMPRESATy.CNPJCPF.Trim();

                string INFADFISCO = txtinfAdFisco.Text.Trim();

                return new NOTAFISCALEEntity(_IDNOTAFISCALE, NOTAFISCALE, SERIE, IDCLIENTE, DTEMISSAO, DTSAIDA,
                                             HORASAIDA, IDTIPOMOVIM, IDCFOP, INSCESTSTRIB, BASECALCICMS,
                                             VALORICMS, BASECALCICMSLSUB, VALORICMSSUB, VALORFRETE,
                                             VALORSEGURO, OUTRADESPES, TOTALIPI,
                                             TOTALPRODUTOS, TOTALNOTA, IDVENDEDOR, VALORCOMISSAO,
                                             IDTRANSPORTES, PLACA, UFTRANSPORTE, QUANT, ESPECIE,
                                             MARCA, NUMEROS, PESOBRUTO, PESOLIQUIDO, INFOCOMPLEM,
                                             IDCENTROCUSTO, IDFORMAPAGTO, IDLOCALCOBRANCA,
                                             IDSTATUS, VALORPAGO, VALORDEVEDOR, frete,
                                             PORCDESCONTO, VALORDESCONTO, PORCACRESCIMO, VALORACRESCIMO,
                                             VALORPIS, VALORCONFINS, CODANTT,
                                             VALORTOTALSERVICO, BASECALCISSQN, ALIQISSQN, VALORISSQN,
                                             CHAVEACESSO, _FLAGARQUIVOXML, _FLAGASSINATURA, _FLAGCANCELADA,
                                             _FLAGENVIADA, _FLAGINUTILIZADO, _FLAGVALIDADA, _ARQUIVOLOTE,
                                             _RECIBONFE, _SITUACAONFE, ALIQCREDICMS, VALORCREDICMS,
                                             FLAGTIPOPAGAMENTO, NUMPEDIDO, FLAGDEVOLUCAO,
                                             CHAVEDEVOLUCAO, CNPJEMISSOR, INFADFISCO);
            }
            set
            {

                if (value != null)
                {
                    _IDNOTAFISCALE = value.IDNOTAFISCALE;
                    _NOTAFISCALE = value.NOTAFISCALE;
                    txtNotaFiscal.Text = value.NOTAFISCALE.ToString().PadLeft(8, '0');
                    txtNotaFiscal.Enabled = false;
                    txtSerieNF.Text = value.SERIE;
                    cbCliente.SelectedValue = value.IDCLIENTE;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");

                    if (value.DTSAIDA != null)
                        mskDtSaida.Text = Convert.ToDateTime(value.DTSAIDA).ToString("dd/MM/yyyy");
                    else
                        mskDtSaida.Text = "  /  /";

                    mkdHoraSaida.Text = value.HORASAIDA;

                    //Entrada 1
                    //Saida 2
                    rbSaida.Checked = value.IDTIPOMOVIM == 2 ? true : false;
                    rdEntrada.Checked = !rbSaida.Checked;

                    cbCFOP.SelectedValue = value.IDCFOP;

                    TxtBaseCalcICMS.Text = Convert.ToDecimal(value.BASECALCICMS).ToString("n2");
                    TxtValorICMS.Text = Convert.ToDecimal(value.VALORICMS).ToString("n2");
                    txtBaseCalcICMSSubs.Text = Convert.ToDecimal(value.BASECALCICMSLSUB).ToString("n2");
                    TxtValorICMSSubst.Text = Convert.ToDecimal(value.VALORICMSSUB).ToString("n2");
                    txtValorFrete.Text = Convert.ToDecimal(value.VALORFRETE).ToString("n2");
                    txtValorSeguro.Text = Convert.ToDecimal(value.VALORSEGURO).ToString("n2");
                    txtOutraDespAcess.Text = Convert.ToDecimal(value.OUTRADESPES).ToString("n2");
                    txtValorTotalIPI.Text = Convert.ToDecimal(value.TOTALIPI).ToString("n2");
                    txtValorTotalProdutos.Text = Convert.ToDecimal(value.TOTALPRODUTOS).ToString("n2");
                    txtTotalNota.Text = Convert.ToDecimal(value.TOTALNOTA).ToString("n2");
                    txtTotalNota2.Text = Convert.ToDecimal(value.TOTALNOTA).ToString("n2");
                    txtTotalFinanceiro.Text = Convert.ToDecimal(value.TOTALNOTA).ToString("n2");

                    if (value.IDVENDEDOR != null)
                        cbFuncionario.SelectedValue = value.IDVENDEDOR;
                    else
                        cbFuncionario.SelectedIndex = 0;

                    txtValComissao.Text = Convert.ToDecimal(value.VALORCOMISSAO).ToString("n2");

                    if (value.IDTRANSPORTES != null)
                        cbTransportadora.SelectedValue = value.IDTRANSPORTES;
                    else
                        cbTransportadora.SelectedIndex = 0;

                    txtPlaca.Text = value.PLACA;

                    txtUFTransp.Text = value.UFTRANSPORTE;

                    txtQuant.Text = Convert.ToDecimal(value.QUANT).ToString();
                    txtEspecie.Text = value.ESPECIE;
                    txtMarca.Text = value.MARCANFE;
                    txtNumeros.Text = value.NUMEROS;
                    txtPesoBruto.Text = Convert.ToDecimal(value.PESOBRUTO).ToString("n2");
                    txtPesoLiquido.Text = Convert.ToDecimal(value.PESOLIQUIDO).ToString("n2");
                    txtObservacao.Text = value.INFOCOMPLEM;

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    if (value.IDFORMAPAGTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    if (value.IDLOCALCOBRANCA != null)
                        cbLocalCobranca.SelectedValue = value.IDLOCALCOBRANCA;
                    else
                        cbLocalCobranca.SelectedIndex = 0;

                    if (value.IDSTATUS != null)
                        cbStatus.SelectedValue = value.IDSTATUS;
                    else
                        cbStatus.SelectedIndex = 0;

                    txtValorPago.Text = Convert.ToDecimal(value.VALORPAGO).ToString("n2");
                    txtValorDev.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");
                    txtDescontoProduto.Text = Convert.ToDecimal(value.VALORDESCONTO).ToString("n2");

                    cdFrete.SelectedIndex = Convert.ToInt32(value.FRETE);
                    txtValorPis.Text = Convert.ToDecimal(value.VALORPIS).ToString("n2");
                    txtconfins.Text = Convert.ToDecimal(value.VALORCONFINS).ToString("n2");
                    txtANTT.Text = value.CODANTT;

                    txtValorTotalServico.Text = Convert.ToDecimal(value.VALORTOTALSERVICO).ToString("n2");
                    txtBasCalcISSQN.Text = Convert.ToDecimal(value.BASECALCISSQN).ToString("n2");
                    txtAliISSQN.Text = Convert.ToDecimal(value.ALIQISSQN).ToString("n2");
                    txtValorISSQN.Text = Convert.ToDecimal(value.VALORISSQN).ToString("n2");
                    txtChaveAcesso.Text = value.CHAVEACESSO;

                    _FLAGARQUIVOXML = value.FLAGARQUIVOXML;
                    _FLAGASSINATURA = value.FLAGASSINATURA;
                    _FLAGCANCELADA = value.FLAGCANCELADA;
                    _FLAGENVIADA = value.FLAGENVIADA;
                    _FLAGINUTILIZADO = value.FLAGINUTILIZADO;
                    _FLAGVALIDADA = value.FLAGVALIDADA;
                    _ARQUIVOLOTE = value.ARQUIVOLOTE.ToString().PadLeft(12, '0');
                    lblLote.Text = "Lote: " + _ARQUIVOLOTE.ToString();
                    _RECIBONFE = value.RECIBONFE;
                    _SITUACAONFE = value.SITUACAONFE;
                    txtReciboRecp.Text = _RECIBONFE;

                    if (_FLAGCANCELADA == "S")
                        lblSituacao.Text = "CANCELADA";
                    else if (_FLAGENVIADA == "S")
                        lblSituacao.Text = "ENVIADA";
                    else if (_FLAGINUTILIZADO == "S")
                        lblSituacao.Text = "INUTILIZADA";
                    else
                        lblSituacao.Text = "Não Selecionada";

                    if (value.ALIQCREDICMS != null)
                        txtAliqCredICMS.Text = Convert.ToDecimal(value.ALIQCREDICMS).ToString("n2");

                    if (value.VALORCREDICMS != null)
                        txtValorCredICMS.Text = Convert.ToDecimal(value.VALORCREDICMS).ToString("n2");

                    if (value.FLAGTIPOPAGAMENTO == "0")
                        rbAvista.Checked = true;
                    else if (value.FLAGTIPOPAGAMENTO == "1")
                        rbPrazo.Checked = true;
                    else if (value.FLAGTIPOPAGAMENTO == "2")
                        rbOutros.Checked = true;

                    txtNumPedido.Text = value.NUMPEDIDO;

                    chkDevolucao.Checked  = false;
                    if(value.FLAGDEVOLUCAO.Trim() == "S")
                          chkDevolucao.Checked  = true;

                    txtNFReferenciaDevolucao.Text = value.CHAVEDEVOLUCAO;
                    txtinfAdFisco.Text = value.INFADFISCO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDNOTAFISCALE = -1;
                    _NOTAFISCALE = "-1";
                    //Proximo Numero da Nota Fiscal
                    GetNextNF();

                    //Limpa produtos
                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);
                    //Limpa Duplciata
                    GridDuplicatasNF(-1, txtNotaFiscal.Text);
                    txtNotaFiscal.Enabled = true;

                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    mskDtSaida.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    mkdHoraSaida.Text = DateTime.Now.ToString("HH:mm");
                    rbSaida.Checked = true;
                    rdEntrada.Checked = !rbSaida.Checked;

                    //Busca o numero de serie no ConfigSistema                    
                    txtSerieNF.Text = CONFISISTEMAP.Read(1).SERIENFE;

                   // cbCFOP.SelectedIndex = 0;
                    TxtBaseCalcICMS.Text = "0,00";
                    TxtValorICMS.Text = "0,00";
                    txtBaseCalcICMSSubs.Text = "0,00";
                    TxtValorICMSSubst.Text = "0,00";
                    txtValorFrete.Text = "0,00";
                    txtValorSeguro.Text = "0,00";
                    txtOutraDespAcess.Text = "0,00";
                    txtValorTotalIPI.Text = "0,00";
                    txtValorTotalProdutos.Text = "0,00";
                    txtTotalNota.Text = "0,00";
                    txtTotalNota2.Text = "0,00";
                    txtTotalFinanceiro.Text = "0,00";
                    cbFuncionario.SelectedIndex = 0;
                    txtValComissao.Text = "0,00";
                    cbTransportadora.SelectedIndex = 0;

                    txtPlaca.Text = string.Empty;
                    txtUFTransp.Text = string.Empty;
                    txtQuant.Text = "1";
                    txtEspecie.Text = string.Empty;
                    txtMarca.Text = string.Empty;
                    txtNumeros.Text = string.Empty;
                    txtPesoBruto.Text = "0,00";
                    txtPesoLiquido.Text = "0,00";
                    txtObservacao.Text = CONFISISTEMAP.Read(1).MSGINICIALNFE;
                    cbCentroCusto.SelectedIndex = 0;
                    cbFormaPagto.SelectedIndex = 0;
                    cbLocalCobranca.SelectedIndex = 0;
                    cbStatus.SelectedValue = 52;
                    txtValorPago.Text = "0,00";
                    txtValorDev.Text = "0,00";

                    txtValorPis.Text = "0,00";
                    txtconfins.Text = "0,00";

                    cdFrete.SelectedIndex = 0;
                    txtANTT.Text = string.Empty;

                    txtValorTotalServico.Text = "0,00";
                    txtBasCalcISSQN.Text = "0,00";
                    txtAliISSQN.Text = "0,00";
                    txtValorISSQN.Text = "0,00";
                    txtChaveAcesso.Text = string.Empty;

                    _FLAGARQUIVOXML = "N";
                    _FLAGASSINATURA = "N";
                    _FLAGCANCELADA = "N";
                    _FLAGENVIADA = "N";
                    _FLAGINUTILIZADO = "N";

                    _ARQUIVOLOTE = string.Empty;
                    _RECIBONFE = string.Empty;
                    _SITUACAONFE = string.Empty;
                    txtReciboRecp.Text = String.Empty;

                    lblSituacao.Text = "Não Selecionada";

                    txtAliqCredICMS.Text = "0,00";
                    txtValorCredICMS.Text = "0,00";

                    rbAvista.Checked = true;

                    txtNumPedido.Text = string.Empty;
                    lblStatusEnvioNFe.Text = "Status Nota:";
                    lblLote.Text = "Lote: ";

                    chkDevolucao.Checked = false;
                    chkAjuste.Checked = false;
                    txtNFReferenciaDevolucao.Text = string.Empty;
                    txtinfAdFisco.Text = string.Empty;

                    errorProvider1.Clear();
                    txtNotaFiscal.Focus();
                }
            }
        }

        int _IDPRODUTONFE = -1;
        public PRODUTONFEEntity Entity2
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);           
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
                decimal VALORTOTAL = Convert.ToDecimal(VALORUNITARIO.ToString("n4")) * Convert.ToDecimal(QUANTIDADE.ToString("n4"));
                decimal COMISSAO = 0;

                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAONFE == "N")
                {
                    decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                    COMISSAO = (VALORTOTAL * PorcComissao) / 100;
                }

                if (txtAlICMS.Text == string.Empty)
                    txtAlICMS.Text = "0,00";
                decimal ALICMS = Convert.ToDecimal(txtAlICMS.Text);

                if (txtRedICMS.Text == string.Empty)
                    txtRedICMS.Text = "0,00";
                decimal REDICMS = Convert.ToDecimal(txtRedICMS.Text);

                decimal VALORICMS = 0;
                decimal BASEICMS = 0;

                if (REDICMS == 0)
                {
                    VALORICMS = (VALORTOTAL * ALICMS) / 100;
                    VALORICMS = Convert.ToDecimal(VALORICMS.ToString("n2"));
                    BASEICMS = Convert.ToDecimal(VALORTOTAL);
                    BASEICMS = Convert.ToDecimal(BASEICMS.ToString("n2"));
                }
                else
                {
                    BASEICMS = VALORTOTAL - ((VALORTOTAL * REDICMS) / 100);
                    VALORICMS = Convert.ToDecimal((BASEICMS * ALICMS) / 100);
                    VALORICMS = Convert.ToDecimal(VALORICMS.ToString("n2"));
                }

                //Busca Dados Produto
                PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
                PRODUTOSTY = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                decimal ALIPI = Convert.ToDecimal(PRODUTOSTY.IPI);
                decimal VALORIPI = (VALORTOTAL * ALIPI) / 100;

                int? IDCLASSFISCAL = null;
               
                int IDCST = Convert.ToInt32(cbSTrib.SelectedValue);
                int IDUNIDADE = Convert.ToInt32(cbUnd.SelectedValue);

                int? IDCFOP = null;
                if (Convert.ToInt32(cbCFOPProduto.SelectedValue) > 0)
                    IDCFOP = Convert.ToInt32(cbCFOPProduto.SelectedValue);

                string CSTPISCOFISNS = PRODUTOSTY.CSTPISCONFIS;
                decimal ALIQPIS = 0;
                decimal ALIQCOFINS = 0;
                decimal VALORPIS = 0;
                decimal VALORCOFINS = 0;

                if (CONFISISTEMATy.FLAGALIQIPICONFIS == "N")
                {
                    ALIQPIS = Convert.ToDecimal(PRODUTOSTY.ALIQPIS);
                    ALIQCOFINS = Convert.ToDecimal(PRODUTOSTY.ALIQCOFINS);
                    VALORPIS = (VALORTOTAL * ALIQPIS) / 100;
                    VALORCOFINS = (VALORTOTAL * ALIQCOFINS) / 100;
                }

                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconProduto.Text);
                decimal DESCONTOPRODUTO = Convert.ToDecimal(txtValorDescontoProduto.Text);

                string DESCPRODUTO2 = txtInformAddProduto.Text;

                if (txtVlFreteProd.Text == string.Empty)
                    txtVlFreteProd.Text = "0,00";
                decimal VLFRETE = Convert.ToDecimal(txtVlFreteProd.Text);

                if (txtVlAproxTributos.Text == string.Empty)
                    txtVlAproxTributos.Text = "0,00";
                decimal VLTRIBUTOAPROX = Convert.ToDecimal(txtVlAproxTributos.Text);

                decimal VLBASEST = Convert.ToDecimal(txtBaseST.Text);
                decimal VLICMSST = Convert.ToDecimal(txtICMSST.Text);
                decimal VLOUTROS = Convert.ToDecimal(txtOutrosProdutos.Text);
                

                return new PRODUTONFEEntity(_IDPRODUTONFE, _IDNOTAFISCALE, IDPRODUTO, QUANTIDADE,
                                           VALORUNITARIO, VALORTOTAL, COMISSAO, ALICMS, REDICMS, VALORICMS,
                                           ALIPI, VALORIPI, IDCLASSFISCAL, IDCST, BASEICMS, IDUNIDADE,
                                           IDCFOP, CSTPISCOFISNS, ALIQPIS, ALIQCOFINS, VALORPIS, VALORCOFINS,
                                           PORCDESCONTO, DESCONTOPRODUTO, DESCPRODUTO2, VLFRETE, VLTRIBUTOAPROX,
                                           VLBASEST, VLICMSST, VLOUTROS);

            }
            set
            {
                if (value != null)
                {
                    _IDPRODUTONFE = value.IDPRODUTONFE;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n4");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n4");
                    txtAlICMS.Text = Convert.ToDecimal(value.ALICMS).ToString("n2");
                    txtRedICMS.Text = Convert.ToDecimal(value.REDICMS).ToString("n2");                 

                    cbSTrib.SelectedValue = value.IDCST;
                    cbUnd.SelectedValue = value.IDUNIDADE;

                    if (value.IDCFOP != null)
                        cbCFOPProduto.SelectedValue = value.IDCFOP;
                    else
                        cbCFOPProduto.SelectedIndex = 0;

                    txtPorcDesconProduto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtValorDescontoProduto.Text = Convert.ToDecimal(value.DESCONTOPRODUTO).ToString("n2");
                    txtVlFreteProd.Text = Convert.ToDecimal(value.VLFRETE).ToString("n2");

                    txtInformAddProduto.Text = value.DESCPRODUTO2;
                    txtVlAproxTributos.Text = Convert.ToDecimal(value.VLTRIBUTOAPROX).ToString("n2");

                    txtBaseST.Text = Convert.ToDecimal(value.VLBASEST).ToString("n2");
                    txtICMSST.Text = Convert.ToDecimal(value.VLICMSST).ToString("n2");
                    txtOutrosProdutos.Text = Convert.ToDecimal(value.VLOUTROS).ToString("n2");
                 
                    errorProvider1.Clear();
                }
                else
                {

                    txtQuanProduto.Text = "1,0000";
                    txtValorUnitProd.Text = "0,0000";
                    txtVlTotal.Text = "0,00";

                    _IDPRODUTONFE = -1;
                    cbProduto.SelectedIndex = 0;
                    txtAlICMS.Text = "0,00";
                    txtRedICMS.Text = "0,00";                   
                    cbSTrib.SelectedIndex = 0;
                    cbUnd.SelectedIndex = 0;

                    txtPorcDesconProduto.Text = "0,00";
                    txtValorDescontoProduto.Text = "0,00";
                    txtVlFreteProd.Text = "0,00";
                    txtInformAddProduto.Text = string.Empty;
                    txtVlAproxTributos.Text = "0,00";

                    txtBaseST.Text = "0,00";
                    txtICMSST.Text = "0,00";
                    txtOutrosProdutos.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }


        private void GetNextNF()
        {
            try
            {
                if (CONFISISTEMATy.NOTAFISCALINICIAL == string.Empty)
                {
                    RowRelatorio.Clear();                   
                    
                    if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    {
                        EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                        EMPRESATy = EMPRESAP.Read(1);
                        RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                    }
                    
                    NOTAFISCALECollection NOTAFISCALECollNextNF = new NOTAFISCALECollection();
                    NOTAFISCALECollNextNF = NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE");
                    
                    if (NOTAFISCALECollNextNF.Count > 0)
                    {
                        int indice = NOTAFISCALECollNextNF.Count - 1;
                        int UltNF = Convert.ToInt32(NOTAFISCALECollNextNF[indice].NOTAFISCALE);
                        txtNotaFiscal.Text = Convert.ToString(UltNF + 1);
                        txtNotaFiscal.Text = txtNotaFiscal.Text.PadLeft(8, '0');
                    }
                    else
                        txtNotaFiscal.Text = "1".PadLeft(8, '0');
                }
                else
                    txtNotaFiscal.Text = CONFISISTEMATy.NOTAFISCALINICIAL.PadLeft(8, '0');
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void FrmNotaFiscalEspelho_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            ativarEmpresaEmissoraDeNFeToolStripMenuItem.Visible = false;
            if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
            {
                ativarEmpresaEmissoraDeNFeToolStripMenuItem.Visible = true;
            }

            GetToolStripButtonCadastro();
            GetDropTipoDuplicata();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnCadCOP.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadMensagem.Image = Util.GetAddressImage(6);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            GetDropCliente();           

            GetDropCFOP();           
            GetDropUnidade();
            GetDropProdutos();
            GetFuncionario();
            GetTransporte();
            GetDropCentroCusto();
            GetDropFormaPgto();
            GetDropLocalCobranca();
            GetDropStatus();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();           
            GetDropSTribu();
            GetDropCFOPProduto();
            GetConfiSistema();
            VerificaAcesso();
            GetDropMensagem();

            cdFrete.SelectedIndex = 0;

            EMPRESAEntity EMPRESATy = new EMPRESAEntity();
            EMPRESATy = EMPRESAP.Read(1);
            if (EMPRESATy != null)
            {
                this.Text = "Nota Fiscal Eletronica - NFe " + EMPRESATy.NOMEFANTASIA + " " + EMPRESATy.CNPJCPF;
            }

            //Gerar Pedido simples
            if (FlagGerarNFePedidoSimples)
            {
                GerarNFePedidoSimples(NumPedidoSimples);
            }
            else
                Entity = null;

            if (ExibiDados)
            {
                tabControl1.SelectTab(1);
                rdbEnviada.Checked = true;

                // Primeiro Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia igual a 1 
                DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                msktDataInicial.Text = primeiroDia.ToString("dd/MM/yyyy");

                // Ultimo Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia é a quantidade de dias que o mês corrente possui.
                //A função DateTime.DaysInMonth recebe como parametro o ano(int) e o mês(int) e retorna a quantidade de dias(int). 
                DateTime ultimoDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                msktDataFinal.Text = ultimoDia.ToString("dd/MM/yyyy");

                btnPesquisa_Click(null, null);
            }          

            this.Cursor = Cursors.Default;
        }      
     

        private Boolean VerificaUso3()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                if (BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe.Trim() == "S")
                {
                    result = false;

                    string sCaminhoDoArquivo = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\securitysoftware3.xml";
                    Util.BaixarArquivoFTP("registros/securitysoftware3.xml", sCaminhoDoArquivo); 
                    

                    EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                    EMPRESATy = EMPRESAP.Read(1);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                    xmlDoc.LoadXml(xmlDoc.InnerXml);
                    // xmlDoc.LoadXml(sCaminhoDoArquivo);

                    //Pegando elemento pelo nome da TAG
                    XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                    //Usando foreach para localizar o cnpj
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn["cnpjcpf"].InnerText.Trim() == EMPRESATy.CNPJCPF.Trim())
                        {
                           result = true;
                           break;
                        }

                    }

                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                result = false;
                return result;
            }

        }

        private void GerarNFePedidoSimples(int NumPedido)
        {

            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                Entity = null;
                Entity2 = null;

                GetConfiSistema();
                tabControl1.SelectTab(0);
                tabControl2.SelectTab(0);

                //Armazena Dados do Pedido
                PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                PEDIDOProvider PEDIDOP = new PEDIDOProvider();
                PEDIDOTy = PEDIDOP.Read(NumPedido);

                //Cliente
                cbCliente.SelectedValue = PEDIDOTy.IDCLIENTE;

                //CFOP
                string UFEmpresa = EMPRESAP.Read(1).UF;
                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", CLIENTEP.Read(Convert.ToInt32(PEDIDOTy.IDCLIENTE)).COD_MUN_IBGE.ToString()));
                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                string UFCliente = LIS_MUNICIPIOSColl[0].UF;

                if (UFEmpresa != UFCliente)
                    cbCFOP.SelectedValue = 4;
                else
                    cbCFOP.SelectedValue = 2;

                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                //Produto
                //Armazena dados Produto do Pedido
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", NumPedido.ToString()));
                txtNumPedido.Text = NumPedido.ToString();

                LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    cbProduto.SelectedValue = item.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");
                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n4");
                    txtVlTotal.Text = (Convert.ToDecimal(item.QUANTIDADE) * Convert.ToDecimal(item.VALORUNITARIO)).ToString("n2");
                    txtInformAddProduto.Text = item.DADOSADICIONAIS;
                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;

                    CalculoTributoAprox(txtNCM.Text);
                    CalculoICMSST();

                    PRODUTONFEP.Save(Entity2);
                    Entity2 = null;
                }

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", NumPedido.ToString()));

                LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                {
                    if (item.FLAGEXIBIR.TrimEnd() == "S")
                    {
                        cbProduto.SelectedValue = item.IDPRODUTO;
                        if (item.MT2 > 0 && item.FLAGBAIXAESTMT == "S")
                        {
                            txtQuanProduto.Text = (Convert.ToDecimal(item.MT2) * Convert.ToDecimal(item.QUANTIDADE)).ToString();
                            txtValorUnitProd.Text = Convert.ToDecimal(item.VALORMETRO).ToString("n4");

                        }
                        else
                        {
                            txtQuanProduto.Text = (Convert.ToDecimal(item.QUANTIDADE)).ToString(); ;
                            txtValorUnitProd.Text = (Convert.ToDecimal(item.VALORTOTAL) / Convert.ToDecimal(item.QUANTIDADE)).ToString("n4");
                        }

                        cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                        CalculoTributoAprox(txtNCM.Text);
                        PRODUTONFEP.Save(Entity2);
                        Entity2 = null;
                    }
                }

                ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                //Cobrança
                if (PEDIDOTy.IDFORMAPAGAMENTO != null)
                    cbFormaPagto.SelectedValue = PEDIDOTy.IDFORMAPAGAMENTO;

                if (PEDIDOTy.IDLOCALCOBRANCA != null)
                    cbLocalCobranca.SelectedValue = PEDIDOTy.IDLOCALCOBRANCA;

                if (PEDIDOTy.IDFORMAPAGAMENTO != null && PEDIDOTy.IDLOCALCOBRANCA != null)
                {
                    DialogResult dr2 = MessageBox.Show("Deseja gerar Duplicatas?",
                    ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr2 == DialogResult.Yes)
                        SaveDuplicatas();
                }

                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                this.Cursor = Cursors.Default;

                MessageBox.Show("Nota Fiscal Gerada com sucesso!");

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível gerar Nota Fiscal!");
                MessageBox.Show("Erro Técnico: " + ex.Message);

            }
        }

        

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void GetConfiSistema()
        {

            CONFISISTEMATy = CONFISISTEMAP.Read(1);

        }       

        private void GetDropSTribu()
        {
            try
            {
                LIS_CSTColl = LIS_CSTP.ReadCollectionByParameter(null, "CODCOMPL");

                cbSTrib.DisplayMember = "CodCompl";
                cbSTrib.ValueMember = "IDCST";

                LIS_CSTEntity LIS_CSTTy = new LIS_CSTEntity();
                LIS_CSTTy.CODCOMPL = ConfigMessage.Default.MsgDrop;
                LIS_CSTTy.IDCST = -1;
                LIS_CSTColl.Add(LIS_CSTTy);

                cbSTrib.DataSource = LIS_CSTColl;
                cbSTrib.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
            cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PreencheDropCamposPesquisa()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("descricao", typeof(string)));
            list.Columns.Add(new DataColumn("nomecampo_tipo", typeof(string)));

            for (int i = 0; i < DataGriewDados.ColumnCount; i++)
            {
                list.Rows.Add(list.NewRow());
            }

            int indexCol = 0;
            int Col = 0;
            foreach (DataGridViewColumn Columns in DataGriewDados.Columns)
            {
                list.Rows[indexCol][Col] = Columns.HeaderText;
                list.Rows[indexCol][Col + 1] = Columns.DataPropertyName;
                indexCol++;
            }


            cbCamposPesquisa.DataSource = list;
            cbCamposPesquisa.DisplayMember = "descricao";
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void GetDropStatus()
        {
            try
            {
                //13 Nota Fiscal
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "13");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

                STATUSProvider STATUSP = new STATUSProvider();
                cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUS";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropLocalCobranca()
        {
            try
            {
                LOCALCOBRANCAProvider LOCALCOBRANCAP = new LOCALCOBRANCAProvider();
                LOCALCOBRANCAColl = LOCALCOBRANCAP.ReadCollectionByParameter(null, "NOME");

                cbLocalCobranca.DisplayMember = "NOME";
                cbLocalCobranca.ValueMember = "IDLOCALCOBRANCA";

                LOCALCOBRANCAEntity LOCALCOBRANCATy = new LOCALCOBRANCAEntity();
                LOCALCOBRANCATy.NOME = ConfigMessage.Default.MsgDrop;
                LOCALCOBRANCATy.IDLOCALCOBRANCA = -1;
                LOCALCOBRANCAColl.Add(LOCALCOBRANCATy);

                Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity>(cbLocalCobranca.DisplayMember);

                LOCALCOBRANCAColl.Sort(comparer.Comparer);
                cbLocalCobranca.DataSource = LOCALCOBRANCAColl;

                cbLocalCobranca.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropFormaPgto()
        {
            try
            {
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                FORMAPAGAMENTOColl = FORMAPAGAMENTOP.ReadCollectionByParameter(null, "NOME");

                cbFormaPagto.DisplayMember = "NOME";
                cbFormaPagto.ValueMember = "IDFORMAPAGAMENTO";

                FORMAPAGAMENTOEntity FORMAPAGAMENTOTy = new FORMAPAGAMENTOEntity();
                FORMAPAGAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
                FORMAPAGAMENTOTy.IDFORMAPAGAMENTO = -1;
                FORMAPAGAMENTOColl.Add(FORMAPAGAMENTOTy);

                Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity>(cbFormaPagto.DisplayMember);

                FORMAPAGAMENTOColl.Sort(comparer.Comparer);
                cbFormaPagto.DataSource = FORMAPAGAMENTOColl;

                cbFormaPagto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCentroCusto()
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetTransporte()
        {
            try
            {
                TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
                TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

                cbTransportadora.DisplayMember = "NOME";
                cbTransportadora.ValueMember = "IDTRANSPORTADORA";

                TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
                TRANSPORTADORATy.NOME = ConfigMessage.Default.MsgDrop;
                TRANSPORTADORATy.IDTRANSPORTADORA = -1;
                TRANSPORTADORAColl.Add(TRANSPORTADORATy);

                Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(cbTransportadora.DisplayMember);

                TRANSPORTADORAColl.Sort(comparer.Comparer);
                cbTransportadora.DataSource = TRANSPORTADORAColl;

                cbTransportadora.SelectedIndex = 0;
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

        private void GetDropProdutos()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));
                PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

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
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropUnidade()
        {
            try
            {
                UNIDADEProvider UNIDADEP = new UNIDADEProvider();
                cbUnd.DataSource = UNIDADEP.ReadCollectionByParameter(null, "NOME");

                cbUnd.DisplayMember = "NOME";
                cbUnd.ValueMember = "IDUNIDADE";

                cbUnd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }       

        private void GetDropCliente()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                LIS_CLIENTE2Provider LIS_CLIENTE2P = new LIS_CLIENTE2Provider();
                LIS_CLIENTE2Collection LIS_CLIENTE2Coll = new LIS_CLIENTE2Collection();
                LIS_CLIENTE2Coll = LIS_CLIENTE2P.ReadCollectionByParameter(null, "NOMECLIENTE");

                cbCliente.DisplayMember = "NOMECLIENTE";
                cbCliente.ValueMember = "IDCLIENTE";

                LIS_CLIENTE2Entity LIS_CLIENTETy = new LIS_CLIENTE2Entity();
                LIS_CLIENTETy.NOMECLIENTE = ConfigMessage.Default.MsgDrop;
                LIS_CLIENTETy.IDCLIENTE = -1;
                LIS_CLIENTE2Coll.Add(LIS_CLIENTETy);

                Phydeaux.Utilities.DynamicComparer<LIS_CLIENTE2Entity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CLIENTE2Entity>(cbCliente.DisplayMember);

                LIS_CLIENTE2Coll.Sort(comparer.Comparer);
                cbCliente.DataSource = LIS_CLIENTE2Coll;

                cbCliente.SelectedIndex = 0;               
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCFOP()
        {
            try
            {
                CFOPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

                cbCFOP.DisplayMember = "CODCFOP";
                cbCFOP.ValueMember = "IDCFOP";

                CFOPEntity CFOPTy = new CFOPEntity();
                CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
                CFOPTy.IDCFOP = -1;
                CFOPColl.Add(CFOPTy);

                Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>(cbCFOP.DisplayMember);

                CFOPColl.Sort(comparer.Comparer);
                cbCFOP.DataSource = CFOPColl;

                cbCFOP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCFOPProduto()
        {
            try
            {
                CFOProdutoPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

                cbCFOPProduto.DisplayMember = "CODCFOP";
                cbCFOPProduto.ValueMember = "IDCFOP";

                CFOPEntity CFOPTy = new CFOPEntity();
                CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
                CFOPTy.IDCFOP = -1;
                CFOProdutoPColl.Add(CFOPTy);

                Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>("CODCFOP");

                CFOPColl.Sort(comparer.Comparer);
                cbCFOPProduto.DataSource = CFOProdutoPColl;

                cbCFOPProduto.SelectedIndex = CFOProdutoPColl.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNPedido_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nota Fiscal, código gerado automaticamente.";
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
           

            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
            {
                using (FrmCliente frm = new FrmCliente())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;

                    frm.ShowDialog();

                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
            else
            {
                using (FrmCliente2 frm = new FrmCliente2())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;

                    frm.ShowDialog();

                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }

        }

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbCliente.SelectedValue = result;
                    }
                }
            }

        }

        private void mskDtSaida_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mskDtSaida.Text != "  /  /" && !ValidacoesLibrary.ValidaTipoDateTime(mskDtSaida.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void rbSaida_CheckedChanged(object sender, EventArgs e)
        {
            rdEntrada.Checked = !rbSaida.Checked;
        }

        private void rdEntrada_CheckedChanged(object sender, EventArgs e)
        {
            rbSaida.Checked = !rdEntrada.Checked;
        }

        private void btnCadCOP_Click(object sender, EventArgs e)
        {
            using (FrmCFOP frm = new FrmCFOP())
            {
                int CodSelec = Convert.ToInt32(cbCFOP.SelectedValue);
                frm._IdCFOP = CodSelec;
                frm.ShowDialog();
                
                GetDropCFOP();
                GetDropCFOPProduto();
                cbCFOP.SelectedValue = CodSelec;
            }
        }

        private void cbCFOP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCFOP.SelectedIndex > 0)
            {
                txtDesCFOP.Text = (CFOPP.Read(Convert.ToInt32(cbCFOP.SelectedValue)).DESCRICAO);
                cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
            }
            else
                txtDesCFOP.Text = string.Empty;

        }

        private void mkdHoraSaida_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraSaida.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraSaida.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraSaida, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
                PRODUTOSTY = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                if (PRODUTOSTY != null)
                {
                    txtNCM.Text = PRODUTOSTY.NCMSH;
                    cbUnd.SelectedValue = PRODUTOSTY.IDUNIDADE;
                    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTY.VALORVENDA1).ToString("n2");
                    
                    if (PRODUTOSTY.IDCST != null)
                        cbSTrib.SelectedValue = PRODUTOSTY.IDCST;
                }

                //Classificacao Fiscal
                if (PRODUTOSTY.IDCLASSIFICACAO != null)
                {
                    CLASSFISCALTY = CLASSFISCALP.Read(Convert.ToInt32(PRODUTOSTY.IDCLASSIFICACAO));                    
                    txtAlICMS.Text = Convert.ToDecimal(CLASSFISCALTY.ALIQICMS).ToString("n2");
                    txtRedICMS.Text = Convert.ToDecimal(CLASSFISCALTY.BASEREDUZIDA).ToString("n2");
                }
                else
                {
                    if (PRODUTOSTY.FLAGICMSST.Trim() == "N") //Cálculo de ICMS ST
                        txtAlICMS.Text = Convert.ToDecimal(PRODUTOSTY.ICMS).ToString("n2");

                   txtRedICMS.Text = "0,00";
                }

                //Situação Tributaria
                CSTProvider CSTPP = new CSTProvider();
                CSTEntity CSTTy = new CSTEntity();
                ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                if (PRODUTOSTY.IDCST != null)
                    cbSTrib.SelectedValue = PRODUTOSTY.IDCST;
                else
                    cbSTrib.SelectedValue = -1;

            }
            else
            {
                cbUnd.SelectedIndex = 0;
                txtValorUnitProd.Text = "0,00";
            }
        }

        private void GetMensagemClassFiscal(int IDMENSAGEMNFE)
        {
            try
            {
                MENSAGEMNFEEntity MensagemTyNfe = new MENSAGEMNFEEntity();
                MENSAGEMNFEProvider MENSAGEMNFEP = new MENSAGEMNFEProvider();
                MensagemTyNfe = MENSAGEMNFEP.Read(IDMENSAGEMNFE);

                if (MensagemTyNfe != null)
                    txtObservacao.Text += " - " + MensagemTyNfe.MENSAGEM;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                        cbProduto_SelectedValueChanged(null, null);

                        txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe == "S")
            {
                using (FrmProduto2 frm = new FrmProduto2())
                {
                    int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    frm._IDPRODUTO = CodSelec;
                    frm.ShowDialog();
                    GetDropUnidade();
                    GetDropSTribu();
                    GetDropProdutos();
                    cbProduto.SelectedValue = CodSelec;
                }
            }
            else
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
                {
                    using (FrmProduto frm = new FrmProduto())
                    {
                        int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                        frm._IDPRODUTO = CodSelec;
                        frm.ShowDialog();
                        GetDropUnidade();
                        GetDropSTribu();
                        GetDropProdutos();
                        cbProduto.SelectedValue = CodSelec;
                    }
                }
                else
                {
                    using (FrmProduto2 frm = new FrmProduto2())
                    {
                        int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                        frm._IDPRODUTO = CodSelec;
                        frm.ShowDialog();
                        GetDropUnidade();
                        GetDropSTribu();
                        GetDropProdutos();
                        cbProduto.SelectedValue = CodSelec;
                    }
                }
            }
           
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void txtClassFiscal_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Classificação Fiscal do Produto, pressione Ctrl+E para pesquisar.";
        }

        private void txtSTrib_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CST - Código da Situação Tributária do Produto, pressione Ctrl+E para pesquisar.";
        }

        private void txtSTrib_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmCST frm = new FrmCST())
                {
                    frm.ShowDialog();
                }
            }
        }

        private void txtClassFiscal_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmClassFiscal frm = new FrmClassFiscal())
                {
                    frm.ShowDialog();
                }
            }
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtQuanProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
                {
                    errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtQuanProduto.Text);
                    txtQuanProduto.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtQuanProduto, "");
                }
            }
            else
                txtQuanProduto.Text = "0,0000";
        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorUnitProd.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
                {
                    errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorUnitProd.Text);
                    txtValorUnitProd.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtValorUnitProd, "");
                }
            }
            else
                txtValorUnitProd.Text = "0,0000";
        }

        private void txtAlICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAlICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAlICMS.Text))
                {
                    errorProvider1.SetError(txtAlICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtAlICMS.Text);
                    txtAlICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAlICMS, "");
                }
            }
            else
                txtAlICMS.Text = "0,00";
        }

        private void txtRedICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtRedICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtRedICMS.Text))
                {
                    errorProvider1.SetError(txtRedICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtRedICMS.Text);
                    txtRedICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtRedICMS, "");
                }
            }
            else
                txtRedICMS.Text = "0,00";
        }

        private void TxtBaseCalcICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (TxtBaseCalcICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TxtBaseCalcICMS.Text))
                {
                    errorProvider1.SetError(TxtBaseCalcICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TxtBaseCalcICMS.Text);
                    TxtBaseCalcICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TxtBaseCalcICMS, "");
                }
            }
            else
                TxtBaseCalcICMS.Text = "0,00";

        }

        private void TxtValorICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (TxtValorICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TxtValorICMS.Text))
                {
                    errorProvider1.SetError(TxtValorICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TxtValorICMS.Text);
                    TxtValorICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TxtValorICMS, "");
                }
            }
            else
                TxtValorICMS.Text = "0,00";
        }

        private void txtBaseCalcICMSSubs_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtBaseCalcICMSSubs.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseCalcICMSSubs.Text))
                {
                    errorProvider1.SetError(txtBaseCalcICMSSubs, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtBaseCalcICMSSubs.Text);
                    txtBaseCalcICMSSubs.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtBaseCalcICMSSubs, "");
                }
            }
            else
                txtBaseCalcICMSSubs.Text = "0,00";
        }

        private void txtValorFrete_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorFrete.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorFrete.Text))
                {
                    errorProvider1.SetError(txtValorFrete, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorFrete.Text);
                    txtValorFrete.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorFrete, "");
                }
            }
            else
                txtValorFrete.Text = "0,00";
        }

        private void txtValorSeguro_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorSeguro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorSeguro.Text))
                {
                    errorProvider1.SetError(txtValorSeguro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorSeguro.Text);
                    txtValorSeguro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorSeguro, "");
                }
            }
            else
                txtValorSeguro.Text = "0,00";
        }

        private void txtOutraDespAcess_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtOutraDespAcess.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtOutraDespAcess.Text))
                {
                    errorProvider1.SetError(txtOutraDespAcess, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtOutraDespAcess.Text);
                    txtOutraDespAcess.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtOutraDespAcess, "");
                }
            }
            else
                txtOutraDespAcess.Text = "0,00";
        }

        private void txtValorTotalIPI_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorTotalIPI.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotalIPI.Text))
                {
                    errorProvider1.SetError(txtValorTotalIPI, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorTotalIPI.Text);
                    txtValorTotalIPI.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorTotalIPI, "");
                }
            }
            else
                txtValorTotalIPI.Text = "0,00";

        }

        private void txtValorTotalProdutos_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorTotalProdutos.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotalProdutos.Text))
                {
                    errorProvider1.SetError(txtValorTotalProdutos, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorTotalProdutos.Text);
                    txtValorTotalProdutos.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorTotalProdutos, "");
                }
            }
            else
                txtValorTotalProdutos.Text = "0,00";

        }

        private void txtTotalNota_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalNota.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalNota.Text))
                {
                    errorProvider1.SetError(txtTotalNota, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtTotalNota.Text);
                    txtTotalNota.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalNota, "");
                }
            }
            else
                txtTotalNota.Text = "0,00";
        }

        private void TxtBaseCalcICMS_Enter(object sender, EventArgs e)
        {
            if (TxtBaseCalcICMS.Text == "0,00")
                TxtBaseCalcICMS.Text = string.Empty;
        }

        private void TxtValorICMS_Enter(object sender, EventArgs e)
        {
            if (TxtValorICMS.Text == "0,00")
                TxtValorICMS.Text = string.Empty;
        }

        private void txtBaseCalcICMSSubs_Enter(object sender, EventArgs e)
        {
            if (txtBaseCalcICMSSubs.Text == "0,00")
                txtBaseCalcICMSSubs.Text = string.Empty;
        }

        private void TxtValorICMSSubst_Enter(object sender, EventArgs e)
        {
            if (TxtValorICMSSubst.Text == "0,00")
                TxtValorICMSSubst.Text = string.Empty;
        }

        private void txtValorFrete_Enter(object sender, EventArgs e)
        {
            if (txtValorFrete.Text == "0,00")
                txtValorFrete.Text = string.Empty;
        }

        private void txtValorSeguro_Enter(object sender, EventArgs e)
        {
            if (txtValorSeguro.Text == "0,00")
                txtValorSeguro.Text = string.Empty;
        }

        private void txtOutraDespAcess_Enter(object sender, EventArgs e)
        {
            if (txtOutraDespAcess.Text == "0,00")
                txtOutraDespAcess.Text = string.Empty;
        }

        private void txtValorTotalIPI_Enter(object sender, EventArgs e)
        {
            if (txtValorTotalIPI.Text == "0,00")
                txtValorTotalIPI.Text = string.Empty;
        }

        private void txtValorTotalProdutos_Enter(object sender, EventArgs e)
        {
            if (txtValorTotalProdutos.Text == "0,00")
                txtValorTotalProdutos.Text = string.Empty;
        }

        private void txtTotalNota_Enter(object sender, EventArgs e)
        {
            if (txtTotalNota.Text == "0,00")
                txtTotalNota.Text = string.Empty;
        }

        private void txtValorUnitProd_Enter(object sender, EventArgs e)
        {
            if (txtValorUnitProd.Text == "0,00")
                txtValorUnitProd.Text = string.Empty;
        }

        private void txtAlICMS_Enter(object sender, EventArgs e)
        {
            if (txtAlICMS.Text == "0,00")
                txtAlICMS.Text = string.Empty;
        }

        private void txtRedICMS_Enter(object sender, EventArgs e)
        {
            if (txtRedICMS.Text == "0,00")
                txtRedICMS.Text = string.Empty;
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

                if (cbFuncionario.SelectedIndex > 0)
                {
                    //"S" para comissão sobre o total o pedido
                    //"N" para comissão pelo total dos produto
                    if (FLAGCOMISSAONFE == "S")
                    {
                        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                        decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);

                        if (PorcComissVend != Convert.ToDecimal(txtPorComisVend.Text))
                        {
                            txtValComissao.Text = ((Convert.ToDecimal(txtTotalFinanceiro.Text) * Convert.ToDecimal(txtPorComisVend.Text)) / 100).ToString("n2");
                        }
                        else
                        {
                            txtPorComisVend.Text = PorcComissVend.ToString("n2");
                            txtValComissao.Text = ((Convert.ToDecimal(txtTotalFinanceiro.Text) * PorcComissVend) / 100).ToString("n2");
                        }

                    }
                    else
                    {
                        MessageBox.Show("A Comissão esta sendo calculada pelo total de produto!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button2);
                    }
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

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorPago.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text))
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorPago.Text);
                    txtValorPago.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPago, "");

                    txtValorDev.Text = (Convert.ToDecimal(txtTotalFinanceiro.Text) - Convert.ToDecimal(txtValorPago.Text)).ToString("n2");

                }
            }
            else
                txtValorPago.Text = "0,00";
        }

        private void txtPorComisVend_Enter(object sender, EventArgs e)
        {
            if (txtPorComisVend.Text == "0,00")
                txtPorComisVend.Text = string.Empty;
        }

        private void txtValComissao_Enter(object sender, EventArgs e)
        {
            if (txtValComissao.Text == "0,00")
                txtValComissao.Text = string.Empty;
        }

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            if (txtValorPago.Text == "0,00")
                txtValorPago.Text = string.Empty;
        }

        private void TxtValorICMSSubst_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (TxtValorICMSSubst.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TxtValorICMSSubst.Text))
                {
                    errorProvider1.SetError(TxtValorICMSSubst, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TxtValorICMSSubst.Text);
                    TxtValorICMSSubst.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TxtValorICMSSubst, "");
                }
            }
            else
                TxtValorICMSSubst.Text = "0,00";
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
        }

        private void cadTransportadora_Click(object sender, EventArgs e)
        {
            using (FrmTransportadora frm = new FrmTransportadora())
            {
                int CodSelec = Convert.ToInt32(cbTransportadora.SelectedValue);
                frm._IDTRANSPORTADORA = CodSelec;
                frm.ShowDialog();               
                
                GetTransporte();
                cbTransportadora.SelectedValue = CodSelec;
            }
        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetDropCentroCusto();

                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void btnFormPagamento_Click(object sender, EventArgs e)
        {
            using (FrmFormasPagamento frm = new FrmFormasPagamento())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
                GetDropFormaPgto();
                cbFormaPagto.SelectedValue = CodSelec;
            }
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
            }
        }

        private void txtQuant_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtQuant.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtQuant.Text))
                {
                    errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    errorProvider1.SetError(txtQuant, "");
                }
            }
            else
                txtQuant.Text = "1";
        }

        private void txtQuant_Enter(object sender, EventArgs e)
        {
            if (txtQuant.Text == string.Empty)
                txtQuant.Text = "1";
        }

        private void txtPesoLiquido_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPesoLiquido.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPesoLiquido.Text))
                {
                    errorProvider1.SetError(txtPesoLiquido, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPesoLiquido.Text);
                    txtPesoLiquido.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPesoLiquido, "");
                }
            }
            else
                txtPesoLiquido.Text = "0,00";
        }

        private void txtPesoBruto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPesoBruto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPesoBruto.Text))
                {
                    errorProvider1.SetError(txtPesoBruto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPesoBruto.Text);
                    txtPesoBruto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPesoBruto, "");
                }
            }
            else
                txtPesoBruto.Text = "0,00";
        }

        private void txtPesoBruto_Enter(object sender, EventArgs e)
        {
            if (txtPesoBruto.Text == "0,00")
                txtPesoBruto.Text = string.Empty;
        }

        private void txtPesoLiquido_Enter(object sender, EventArgs e)
        {
            if (txtPesoLiquido.Text == "0,00")
                txtPesoLiquido.Text = string.Empty;
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE != -1)
            {
                 Grava();
           }
            else if (VerificaPlanos())
            {
                Grava();
            }
        }

        private Boolean VerificaPlanos()
        {
            Boolean result = true;

            try
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S")
                {
                    LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl_Total = new LIS_NOTAFISCALECollection();

                    DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime ultimoDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(primeiroDia.ToString()));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(ultimoDia.ToString()));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    LIS_NOTAFISCALEColl_Total = LIS_NOTAFISCALEP.ReadCollectionByParameter(Filtro);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos));

                    if (RECURSOSPLANOTy != null)
                    {
                        int QuantNF = Convert.ToInt32(RECURSOSPLANOTy.NOTAFISCAL);

                        if (LIS_NOTAFISCALEColl_Total.Count < QuantNF)
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show("Limite de Nota Fiscal atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

                            result = false;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }



        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    if (_IDNOTAFISCALE == -1)
                    {
                        //Verifica se existe a NOTAFISCAL
                        if (VerificaExisteNFNovo(txtNotaFiscal.Text))
                        {
                            DialogResult dr = MessageBox.Show("N.F já existe, deseja continuar?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);
                                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                                txtNotaFiscal.Enabled = false;
                                
                            }
                        }
                        else
                        {
                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            txtNotaFiscal.Enabled = false;
                        }
                    }
                    else
                    {
                        //Verifica se existe a NOTAFISCAL
                        if (VerificaExisteNFSalva(txtNotaFiscal.Text))
                        {
                            DialogResult dr = MessageBox.Show("N.F já existe, deseja continuar?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);
                                btnPesquisa_Click(null, null);
                                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                                txtNotaFiscal.Enabled = false;
                            }
                        }
                        else
                        {
                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);
                            btnPesquisa_Click(null, null);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            txtNotaFiscal.Enabled = false;
                        }
                    }                  

                }

                

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean ValidacoesNFe()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (!ValidacoesLibrary.ValidaTipoInt32(txtQuant.Text) || Convert.ToInt32(txtQuant.Text) ==0)
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                tabControl2.SelectTab(2);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!validaDadosCliente(Convert.ToInt32(cbCliente.SelectedValue)))
            {
                  errorProvider1.SetError(cbCliente, ConfigMessage.Default.FieldErro);
                  result = false;
            }
            else if (LIS_PRODUTONFEColl.Count == 0)
            {
                string msg = "Não existe produto adicionado na Nota Fiscal";
                MessageBox.Show(msg);
                errorProvider1.SetError(linkLabel2, msg);
                result = false;
            }
            else if (chkDevolucao.Checked && txtNFReferenciaDevolucao.Text == string.Empty)
            {
                string msg = "Campo de Nota Fiscal referência obrigatório";
                MessageBox.Show(msg);
                errorProvider1.SetError(label41, msg);
                result = false;
            }
            if(_FLAGENVIADA == "S")
            {
                string msg = "Nota Fiscal já processada, não é possível gerar novamente!";
                MessageBox.Show(msg);
                errorProvider1.SetError(label41, msg);
                result = false;
            }
            //else if (!VerificaUso4())
            //{
            //    MessageBox.Show(MensagemErroBloqueio);
            //    result = false;
            //}   
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean VerificaUso4()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                
                string sCaminhoDoArquivo = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\securitysoftware4.xml";
                Util.BaixarArquivoFTP("registros/securitysoftware4.xml", sCaminhoDoArquivo); 

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                xmlDoc.LoadXml(xmlDoc.InnerXml);
                // xmlDoc.LoadXml(sCaminhoDoArquivo);

                //Pegando elemento pelo nome da TAG
                XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                //Usando foreach para localizar o cnpj
                foreach (XmlNode xn in xnList)
                {
                    if (xn["cnpjcpf"].InnerText.Trim() == EMPRESATy.CNPJCPF.Trim())
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
                //MessageBox.Show("Erro técnico: " + ex.Message);
                result = true;
                return result;

            }

        }

        private Boolean validaDadosCliente(int IDCLIENTE)
        {
            Boolean result = true;

            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
            CLIENTETy = CLIENTEP.Read(IDCLIENTE);
            string MSG = string.Empty;
            if (CLIENTETy == null)
            {
                MSG = "Cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.CNPJ == "  .   .   /    -" && CLIENTETy.CPF == "   .   .   -")
            {
                MSG = "CNPJ/CPF do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.COD_MUN_IBGE == null)
            {
                MSG = "Cidade do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.ENDERECO1.TrimEnd() == string.Empty)
            {
                MSG = "Endereço do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.NUMEROENDER.TrimEnd() == string.Empty)
            {
                MSG = "Número no endereço do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.CEP1 == "     -")
            {
                MSG = "CEP do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }
            else if (CLIENTETy.BAIRRO1.TrimEnd() == string.Empty)
            {
                MSG = "Bairro do cliente inválido!";
                MessageBox.Show(MSG);
                result = false;
            }

            return result;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (!ValidacoesLibrary.ValidaTipoInt32(txtNotaFiscal.Text))
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (Convert.ToInt32(txtNotaFiscal.Text) < 1)
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (mkdHoraSaida.Text == "  :")
            {
                errorProvider1.SetError(label24, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if(_IDNOTAFISCALE == -1 && !VerificaPlanos())
            {
                result = false;
            }           
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbCFOP.SelectedValue) < 1)
            {
                errorProvider1.SetError(label5, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                errorProvider1.SetError(label52, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (mskDtSaida.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(mskDtSaida.Text))
            {
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtNotaFiscal.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (chkDevolucao.Checked && txtNFReferenciaDevolucao.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(label41, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if ( txtNFReferenciaDevolucao.Text.Trim().Length > 0 && txtNFReferenciaDevolucao.Text.Trim().Length < 44)
            {
                string MsgErro = "Chave de acesso inválida. A chave de acesso deve ter 44 dígitos.";
                errorProvider1.SetError(label41, MsgErro);
                Util.ExibirMSg(MsgErro, "Red");
                result = false;
            }
            else if (!chkDevolucao.Checked && txtNFReferenciaDevolucao.Text.Trim().Length > 0)
            {
                errorProvider1.SetError(label41, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida! não é possível alterar!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
                result = false;
            }


            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean VerificaExisteNFNovo(string NotaFiscalE)
        {
            try
            {
                NOTAFISCALECollection NOTAFISCALECollNextNF = new NOTAFISCALECollection();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.Int32", "=", NotaFiscalE));

                NOTAFISCALECollNextNF = NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE");
                if (NOTAFISCALECollNextNF.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return false;
            }
        }

        private Boolean VerificaExisteNFSalva(string NotaFiscal)
        {
            try
            {
                NOTAFISCALECollection NOTAFISCALECollNextNF = new NOTAFISCALECollection();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NotaFiscal, "and"));
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "<>", _IDNOTAFISCALE.ToString()));

                NOTAFISCALECollNextNF = NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE");
                if (NOTAFISCALECollNextNF.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return false;
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();


                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (rbCancelada.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGCANCELADA", "System.String", "=", "S");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                else if (rdbEnviada.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGENVIADA", "System.String", "=", "S");
                    filtroProfile = new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(Filtro, "IDNOTAFISCALE DESC");

                //Colocando somatorio no final da lista
                LIS_NOTAFISCALEEntity LIS_NOTAFISCALETy = new LIS_NOTAFISCALEEntity();
                LIS_NOTAFISCALETy.TOTALIPI = SumTotalPesquisa("TOTALIPI");
                LIS_NOTAFISCALETy.TOTALNOTA = SumTotalPesquisa("TOTALNOTA");
                LIS_NOTAFISCALETy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_NOTAFISCALETy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_NOTAFISCALETy.VALORCONFINS = SumTotalPesquisa("VALORCONFINS");
                LIS_NOTAFISCALETy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_NOTAFISCALETy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_NOTAFISCALETy.VALORFRETE = SumTotalPesquisa("VALORFRETE");
                LIS_NOTAFISCALETy.VALORICMS = SumTotalPesquisa("VALORICMS");
                LIS_NOTAFISCALETy.VALORICMSSUB = SumTotalPesquisa("VALORICMSSUB");
                LIS_NOTAFISCALETy.VALORTOTALSERVICO = SumTotalPesquisa("VALORTOTALSERVICO");
                LIS_NOTAFISCALETy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_NOTAFISCALETy.OUTRADESPES = SumTotalPesquisa("OUTRADESPES");
                    

                LIS_NOTAFISCALEColl.Add(LIS_NOTAFISCALETy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_NOTAFISCALEColl;

                lblTotalPesquisa.Text = (LIS_NOTAFISCALEColl.Count - 1).ToString();
            }
            else
                PesquisaFiltro();

            ColorGrid();

            this.Cursor = Cursors.Default;
        }

        private void ColorGrid()
        {
            int i = 0;
            foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
            {

                if (item.FLAGCANCELADA == "S")
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (item.FLAGENVIADA == "S")
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (item.FLAGINUTILIZADO == "S")
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Lime;
                }


                i++;
            }
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControl1.SelectedIndex == 2)
                {
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
            }
            else
            {
                errorProvider1.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            try
            {
                // referente ao tipo de campo
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_NOTAFISCALEColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_NOTAFISCALEColl;
                }

                // Retorna o tipo de campo para pesquisa Ex.: String, Integer, Date...
                string Tipo = DataGriewDados.Columns[cbCamposPesquisa.SelectedValue.ToString()].ValueType.FullName;

                if (Tipo.Length > 20)
                    Tipo = Util.GetTypeCell(Tipo);//Retorna o texto resumido do tipo

                string Valor = txtCriterioPesquisa.Text;

                //Verifica se o valor digitado e compativel com
                // o tipo de pesquisa
                if (ValidacoesLibrary.ValidaTipoPesquisa(Tipo, Valor))
                {
                    if (Tipo == "System.DateTime")//formata data para pesquisa.
                        Valor = Util.ConverStringDateSearch(txtCriterioPesquisa.Text);
                    else if (Tipo == "System.Decimal")//formata Numeric para pesquisa.
                        Valor = Util.ConverStringDecimalSearch(txtCriterioPesquisa.Text);

                    filtroProfile = new RowsFiltro(campo, Tipo, cbTipoPesquisa.SelectedValue.ToString(), Valor);

                    if (!chkBoxAcumulaPesquisa.Checked)//Acumular pesquisa
                        Filtro.Clear();

                    Filtro.Insert(Filtro.Count, filtroProfile);

                    if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                    {
                        filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    if (rbCancelada.Checked)
                    {
                        filtroProfile = new RowsFiltro("FLAGCANCELADA", "System.String", "=", "S");
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }
                    else if (rdbEnviada.Checked)
                    {
                        filtroProfile = new RowsFiltro("FLAGENVIADA", "System.String", "=", "S");
                        filtroProfile = new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N");
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(Filtro, "IDNOTAFISCALE DESC");

                    //Colocando somatorio no final da lista
                    LIS_NOTAFISCALEEntity LIS_NOTAFISCALETy = new LIS_NOTAFISCALEEntity();
                    LIS_NOTAFISCALETy.TOTALIPI = SumTotalPesquisa("TOTALIPI");
                    LIS_NOTAFISCALETy.TOTALNOTA = SumTotalPesquisa("TOTALNOTA");
                    LIS_NOTAFISCALETy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                    LIS_NOTAFISCALETy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                    LIS_NOTAFISCALETy.VALORCONFINS = SumTotalPesquisa("VALORCONFINS");
                    LIS_NOTAFISCALETy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                    LIS_NOTAFISCALETy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                    LIS_NOTAFISCALETy.VALORFRETE = SumTotalPesquisa("VALORFRETE");
                    LIS_NOTAFISCALETy.VALORICMS = SumTotalPesquisa("VALORICMS");
                    LIS_NOTAFISCALETy.VALORICMSSUB = SumTotalPesquisa("VALORICMSSUB");
                    LIS_NOTAFISCALETy.VALORTOTALSERVICO = SumTotalPesquisa("VALORTOTALSERVICO");
                    LIS_NOTAFISCALETy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                    LIS_NOTAFISCALETy.OUTRADESPES = SumTotalPesquisa("OUTRADESPES");
                    LIS_NOTAFISCALEColl.Add(LIS_NOTAFISCALETy);

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_NOTAFISCALEColl;

                    lblTotalPesquisa.Text = (LIS_NOTAFISCALEColl.Count - 1).ToString();
                }
                else
                {
                    MessageBox.Show(ConfigMessage.Default.searchFieldType);
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                    txtCriterioPesquisa.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
            {
                if (NomeCampo == "TOTALIPI")
                    valortotal += Convert.ToDecimal(item.TOTALIPI);
                else if (NomeCampo == "TOTALNOTA")
                    valortotal += Convert.ToDecimal(item.TOTALNOTA);
                else if (NomeCampo == "TOTALPRODUTOS")
                    valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
                else if (NomeCampo == "VALORCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VALORCOMISSAO);
                else if (NomeCampo == "VALORCONFINS")
                    valortotal += Convert.ToDecimal(item.VALORCONFINS);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORFRETE")
                    valortotal += Convert.ToDecimal(item.VALORFRETE);
                else if (NomeCampo == "VALORICMS")
                    valortotal += Convert.ToDecimal(item.VALORICMS);
                else if (NomeCampo == "VALORICMSSUB")
                    valortotal += Convert.ToDecimal(item.VALORICMSSUB);
                else if (NomeCampo == "VALORTOTALSERVICO")
                    valortotal += Convert.ToDecimal(item.VALORTOTALSERVICO);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
                else if (NomeCampo == "OUTRADESPES")
                    valortotal += Convert.ToDecimal(item.OUTRADESPES);
                        


            }

            return valortotal;
        }

        private void txtNotaFiscal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtNotaFiscal.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtNotaFiscal.Text))
                {
                    errorProvider1.SetError(txtPesoBruto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    txtNotaFiscal.Text = txtNotaFiscal.Text.PadLeft(8, '0');
                    errorProvider1.Clear();
                }
            }

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;

                GetConfiSistema();
                tabControl1.SelectTab(0);
                tabControl2.SelectTab(0);
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;

                GetConfiSistema();
                tabControl1.SelectTab(0);
                tabControl2.SelectTab(0);
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                frm._IDSTATUS = CodSelec;
                frm.ShowDialog();
                
                GetDropStatus();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
        }

        private Boolean ValidacoesDelete()
        {
            Boolean result = true;

            errorProvider1.Clear();

            // Ambiente - 1 Produção - 2 Homologação
            if (CONFISISTEMAP.Read(1).FLAGAMBIENTENFE == "1" && ConsultSiTNFe())
            {
                MessageBox.Show("Não é possível excluir, NF-e já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else if (LIS_PRODUTONFEColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar a Nota Fiscal é necessário remover os Produtos!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);

                tabControl1.SelectTab(0);
                tabControl2.SelectTab(0);
                result = false;
            }
            else if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar a Nota Fiscal é necessário remover as Duplicatas!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

                tabControl1.SelectTab(0);
                tabControl2.SelectTab(4);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void Delete()
        {
            if (_IDNOTAFISCALE == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");

                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else
            {

                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        NOTAFISCALEP.Delete(_IDNOTAFISCALE);

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        
                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            int CodigoSelect = -1;
            if (LIS_NOTAFISCALEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_NOTAFISCALEColl[rowindex].IDNOTAFISCALE);
                    tabControl1.SelectTab(0);
                    tabControl2.SelectTab(0);

                    Entity = NOTAFISCALEP.Read(CodigoSelect);
                    txtNotaFiscal.Focus();

                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    CodigoSelect = Convert.ToInt32(LIS_NOTAFISCALEColl[rowindex].IDNOTAFISCALE);

                    Entity = NOTAFISCALEP.Read(CodigoSelect);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);

                    apagaToolStripMenuItem_Click(null, null);

                    Entity = null;
                    Entity2 = null;
                    
                }
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CodigoSelect = Convert.ToInt32(LIS_NOTAFISCALEColl[rowindex].IDNOTAFISCALE);
                    Entity = NOTAFISCALEP.Read(CodigoSelect);
                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);

                    imprimirDanfeEmPDFToolStripMenuItem1_Click(null, null);

                    Entity = null;
                    Entity2 = null;

                }
            }
        }

       

        private void cbClassFiscal_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Classificação Fiscal, pressione Ctrl+E para pesquisar.";
        }       

        private void cbSTrib_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmCST frm = new FrmCST())
                {
                    frm.ShowDialog();
                    int CodSelec = Convert.ToInt32(cbSTrib.SelectedValue);
                    GetDropSTribu();
                    cbSTrib.SelectedValue = CodSelec;
                }
            }
        }

        private void cbSTrib_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Situação Tributária, pressione Ctrl+E para pesquisar.";
        }       

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
              GravaProduto();
        }

        private void GravaProduto()
        {
            try
            {
                if (Validacoes() && ValidacoesProdutos())
                {
                    CalculoICMSST();
                    //Calculo de tributos aproximados
                    CalculoTributoAprox(txtNCM.Text);

                    _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);
                    
                    txtNotaFiscal.Enabled = false;

                    SaveLoteEstoque();//Salva Lote Estoque
                    PRODUTONFEP.Save(Entity2);
                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                    //Salva NOTAFISCAL
                    NOTAFISCALEP.Save(Entity);                  

                    Entity2 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");                    
                    cbProduto.Focus();
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SaveLoteEstoque()
        {
            try
            {
                Decimal SaldoLote = ConsultaLoteProduto();
                string MSGAdicionalProduto = string.Empty;
                //Consultar se exite Lote para o produto vendido
                if (SaldoLote > 0)
                {
                    DialogResult dr = MessageBox.Show("Deseja Adicionar Informações Lote(s) neste Produto?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        decimal? QuantVendida = Convert.ToDecimal(txtQuanProduto.Text);
                        SaldoProdutoLote(Convert.ToInt32(cbProduto.SelectedValue.ToString()), QuantVendida);
                    }
                }                             

            }
            catch (Exception ex)
            {
                Util.ExibirMSg("Erro ao Salvar Estoque Lote!", "Red");
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void SaldoProdutoLote(int? IdProduto, decimal? quantidadeV)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IdProduto.ToString()));
                LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl_2 = new LIS_ESTOQUELOTECollection();
                LIS_ESTOQUELOTEColl_2 = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE");

                decimal QuantVendida = Convert.ToDecimal(quantidadeV);

                foreach (var item in LIS_ESTOQUELOTEColl_2)
                {
                    decimal SaldoPorlote = 0;
                    SaldoPorlote = ConsultaSaldoLote(item.IDPRODUTO, item.IDLOTE);

                    if (QuantVendida > 0 && SaldoPorlote > 0)
                    {
                        //Salva ESTOQUELOTE
                        ESTOQUELOTEEntity ESTOQUELOTETy = new ESTOQUELOTEEntity();
                        ESTOQUELOTETy.IDESTOQUELOTE = -1;

                        if (QuantVendida > SaldoPorlote)
                        {
                            QuantVendida -= SaldoPorlote;
                            ESTOQUELOTETy.QUANTIDADE = SaldoPorlote;
                        }
                        else if (QuantVendida <= SaldoPorlote)
                        {
                            ESTOQUELOTETy.QUANTIDADE = QuantVendida;
                            QuantVendida -= QuantVendida;
                        }

                        ESTOQUELOTETy.IDLOTE = item.IDLOTE;
                        ESTOQUELOTETy.IDPRODUTO = Convert.ToInt32(item.IDPRODUTO);
                        ESTOQUELOTETy.NUMERODOC = "NF" + txtNotaFiscal.Text;
                        ESTOQUELOTETy.DATA = Convert.ToDateTime(msktDataEmissao.Text); ;
                        ESTOQUELOTETy.FLAGTIPO = "S"; //SAIDA
                        ESTOQUELOTETy.FLAGATIVO = "S";//ATIVO SIM
                        ESTOQUELOTETy.OBSERVACAO = "";
                        txtInformAddProduto.Text += "Lote:" + item.CODLOTE + " Quant.:" + ESTOQUELOTETy.QUANTIDADE + " ";
                        ESTOQUELOTEP.Save(ESTOQUELOTETy);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                }

                Util.ExibirMSg("Estoque Lote Salvo com Sucesso!", "Blue");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        //Consultar se exite Lote para o produto vendido
        private decimal ConsultaSaldoLote(int? IDPRODUTO, int? IDLOTE)
        {
            decimal result = 0;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDLOTE", "System.Int32", "=", IDLOTE.ToString()));
                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                decimal Entrada = 0;
                decimal Saida = 0;
                decimal Saldo = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    if (item.FLAGATIVO == "S" && item.FLAGTIPO == "E")
                        Entrada += Convert.ToDecimal(item.QUANTIDADE);
                    else if (item.FLAGATIVO == "S" && item.FLAGTIPO == "S")
                        Saida += Convert.ToDecimal(item.QUANTIDADE);
                }

                Saldo = Entrada - Saida;
                result = Saldo;
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        //Consultar se exite Lote para o produto vendido
        private decimal ConsultaLoteProduto()
        {
            decimal result = 0;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();                
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                decimal Entrada = 0;
                decimal Saida = 0;
                decimal Saldo = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    if (item.FLAGATIVO == "S" && item.FLAGTIPO == "E")
                        Entrada += Convert.ToDecimal(item.QUANTIDADE);
                    else if (item.FLAGATIVO == "S" && item.FLAGTIPO == "S")
                        Saida += Convert.ToDecimal(item.QUANTIDADE);
                }

                Saldo = Entrada - Saida;
                result = Saldo;
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void CalculoPeso()
        {
            decimal? pesobruto = 0;

            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                pesobruto += item.QUANTIDADE * PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO)).PESO;
            }

            txtPesoBruto.Text = Convert.ToDecimal(pesobruto).ToString("n3");
        }

        private void ListaProdutoNotaFiscal(int IDNOTAFISCALE)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", IDNOTAFISCALE.ToString()));
            LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);
            
            DGDadosProduto.AutoGenerateColumns = false;
            DGDadosProduto.DataSource = LIS_PRODUTONFEColl;
            lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTONFEColl.Count.ToString();

            txtDescontoProduto.Text = BuscaDescontoProduto().ToString("n2");

            SumTotalOutros();
            SumTotalFrete();
            SumTotalProdutosNF();
            SumTotalComissao();
            SumIPI();
            SumICMS();
            SumBASEICMS();
            SumTotalNF();

            SumBASEICMSST();
            SumICMSST();

            CalculoPeso();
        }

        private void SumTotalFrete()
        {
            decimal vFrete_ICMSTot = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                vFrete_ICMSTot += Convert.ToDecimal(item.VLFRETE);
            }

            txtValorFrete.Text = vFrete_ICMSTot.ToString("n2");
        }

        private void SumTotalOutros()
        {
            decimal VLOUTROS = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                VLOUTROS += Convert.ToDecimal(item.VLOUTROS);
            }

            txtOutraDespAcess.Text = VLOUTROS.ToString("n2");
        }


        private void SumBASEICMS()
        {
            decimal TotalIBaseICMS = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                if(item.ALICMS > 0)
                    TotalIBaseICMS += Convert.ToDecimal(item.BASEICMS);
            }

            TxtBaseCalcICMS.Text = TotalIBaseICMS.ToString("n2");
        }

        private void SumBASEICMSST()
        {
            decimal TotalBaseICMSST = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                TotalBaseICMSST += Convert.ToDecimal(item.VLBASEST);
            }

            txtBaseCalcICMSSubs.Text = TotalBaseICMSST.ToString("n2");
        }

        private void SumICMSST()
        {
            decimal TotalCMSST = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                TotalCMSST += Convert.ToDecimal(item.VLICMSST);
            }

            TxtValorICMSSubst.Text = TotalCMSST.ToString("n2");
        }

        private void SumTotalProdutosNF()
        {
            decimal total = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
            txtValorTotalProdutos.Text = total.ToString("n2");
        }

        private void SumIPI()
        {
            decimal TotalIPI = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                TotalIPI += Convert.ToDecimal(item.VALORIPI);
            }

            txtValorTotalIPI.Text = TotalIPI.ToString("n2");
        }

        private void SumICMS()
        {
            decimal TotalICMS = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                TotalICMS += Convert.ToDecimal(item.VALORICMS);
                TotalICMS = Convert.ToDecimal(TotalICMS.ToString("n2"));
            }

             TxtValorICMS.Text = TotalICMS.ToString("n2");
        }

        private void SumTotalComissao()
        {
            decimal TotalComissao = 0;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                TotalComissao += Convert.ToDecimal(item.COMISSAO);
            }

            txtValComissao.Text = TotalComissao.ToString("n2");
        }

        private void SumPISCOFINSTotalNF()
        {
            if (CONFISISTEMATy.FLAGALIQIPICONFIS == "S")
            {
                txtValorPis.Text = ((Convert.ToDecimal(txtTotalNota.Text) * Convert.ToDecimal(txtAliPIS.Text)) / 100).ToString("n2");
                txtconfins.Text = ((Convert.ToDecimal(txtTotalNota.Text) * Convert.ToDecimal(txtAliCofins.Text)) / 100).ToString("n2");
            }
            else
            {
                decimal ValorPis = 0;
                decimal ValorCofins = 0;
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    ValorPis += Convert.ToDecimal(item.VALORPIS);
                    ValorCofins += Convert.ToDecimal(item.VALORCOFINS);
                }

                txtValorPis.Text = ValorPis.ToString("n2");
                txtconfins.Text = ValorCofins.ToString("n2");
            }
        }

        private void SumISSQN()
        {
            if (FLAGBASEISSQN == "S")
            {
                txtBasCalcISSQN.Text = txtTotalNota.Text;
                txtValorISSQN.Text = ((Convert.ToDecimal(txtBasCalcISSQN.Text) * Convert.ToDecimal(txtAliISSQN.Text)) / 100).ToString("n2");
            }
            else
            {
                txtBasCalcISSQN.Text = txtValorTotalServico.Text;
                txtValorISSQN.Text = ((Convert.ToDecimal(txtBasCalcISSQN.Text) * Convert.ToDecimal(txtAliISSQN.Text)) / 100).ToString("n2");
            }

        }

        private void SumTotalNF()
        {
            try
            {
                //Configuração do Sistema
                CONFISISTEMAEntity CONFISISTEMAty = new CONFISISTEMAEntity();
                CONFISISTEMAty = CONFISISTEMAP.Read(1);

                decimal ValorTotalNF = 0;
                // decimal ValorFrete = cdFrete.SelectedIndex == 1 ? Convert.ToDecimal(txtValorFrete.Text) : 0;
                decimal ValorFrete = Convert.ToDecimal(txtValorFrete.Text);
                decimal TotalIPI = Convert.ToDecimal(txtValorTotalIPI.Text);
                // decimal TotalSeguro = CONFISISTEMAty.FLAGSOMASEGURANFE == "S" ? Convert.ToDecimal(txtValorSeguro.Text) : 0;
                decimal TotalSeguro = Convert.ToDecimal(txtValorSeguro.Text);


                ValorTotalNF = Convert.ToDecimal(txtValorTotalProdutos.Text) +
                               Convert.ToDecimal(txtValorTotalServico.Text) +
                               TotalIPI + ValorFrete + TotalSeguro +
                               Convert.ToDecimal(txtOutraDespAcess.Text) -
                               Convert.ToDecimal(txtDescontoProduto.Text);

                txtTotalNota.Text = ValorTotalNF.ToString("n2");
                txtTotalNota2.Text = ValorTotalNF.ToString("n2");
                txtTotalFinanceiro.Text = txtTotalNota.Text;
                SumPISCOFINSTotalNF();

                txtValorDev.Text = (Convert.ToDecimal(txtTotalFinanceiro.Text) - Convert.ToDecimal(txtValorPago.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean ValidacoesProdutos()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbProduto.SelectedValue) < 0)
            {
                errorProvider1.SetError(label14, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                
                result = false;

            }
            else if (Convert.ToInt32(cbUnd.SelectedValue) < 0)
            {
                errorProvider1.SetError(label32, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }           
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(label12, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text) || Convert.ToDecimal(txtQuanProduto.Text) == 0)
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtAlICMS.Text))
            {
                errorProvider1.SetError(txtAlICMS, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtRedICMS.Text))
            {
                errorProvider1.SetError(txtRedICMS, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbSTrib.SelectedValue) <= 0)
            {
                errorProvider1.SetError(label34, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbCFOPProduto.SelectedValue) <= 0)
            {
                errorProvider1.SetError(label56, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtNCM.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(label75, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (CONFISISTEMAP.Read(1).ESTOQUENEGATIVO.TrimEnd() == "S")
            {
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(cbProduto.SelectedValue), true);
                if (ESTOQUEATUAL < Convert.ToDecimal(txtQuanProduto.Text))
                {
                    string Msgerro = "Estoque não pode ficar negativo!";
                    errorProvider1.SetError(txtQuanProduto, Msgerro);
                    Util.ExibirMSg(Msgerro, "Red");
                    result = false;
                }

            }
            else

                errorProvider1.Clear();


            return result;
        }

        private void DGDadosProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTONFEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;
                if (ColumnSelecionada == 0)//Excluir
                {
                    // Ambiente - 1 Produção - 2 Homologação
                    if (CONFISISTEMAP.Read(1).FLAGAMBIENTENFE == "1" && ConsultSiTNFe())
                    {
                        MessageBox.Show("Não é possivel excluir, NF-e já emitida!",
                                     ConfigSistema1.Default.NomeEmpresa,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error,
                                     MessageBoxDefaultButton.Button1);
                    }
                    else
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTONFEColl[rowindex].IDPRODUTONFE);
                                int IDPRODUTO = Convert.ToInt32(LIS_PRODUTONFEColl[rowindex].IDPRODUTO);
                                ExcluirEstoqueLote(IDPRODUTO);
                                PRODUTONFEP.Delete(CodSelect);                             

                                ListaProdutoNotaFiscal(_IDNOTAFISCALE);
                                Entity2 = null;

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");

                                NOTAFISCALEP.Save(Entity);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    }
                }
                if (ColumnSelecionada == 1)//Editar Produto
                {
                    int IDPRODUTO = Convert.ToInt32(LIS_PRODUTONFEColl[rowindex].IDPRODUTO);

                    if (BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe == "S")
                    {
                        using (FrmProduto2 frm = new FrmProduto2())
                        {
                            frm._IDPRODUTO = IDPRODUTO;
                            frm.ShowDialog();
                            GetDropUnidade();
                            GetDropProdutos();
                        }
                    }
                    else
                    {
                        if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
                        {
                            using (FrmProduto frm = new FrmProduto())
                            {
                                frm._IDPRODUTO = IDPRODUTO;
                                frm.ShowDialog();
                                GetDropUnidade();
                                GetDropProdutos();
                            }
                        }
                        else
                        {
                            using (FrmProduto2 frm = new FrmProduto2())
                            {
                                frm._IDPRODUTO = IDPRODUTO;
                                frm.ShowDialog();
                                GetDropUnidade();
                                GetDropProdutos();
                            }
                        }
                    }

                }

            }
        }

        private void ExcluirEstoqueLote(int IDPRODUTO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "=", "NF"+txtNotaFiscal.Text));
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));

                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);                

                int Contador = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    ESTOQUELOTEP.Delete(Convert.ToInt32(item.IDESTOQUELOTE));
                    Contador++;
                }

                if(Contador > 0)
                    Util.ExibirMSg("Estoque Lote Excluido com Sucesso!", "blue");
             
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cdFrete_SelectedValueChanged(object sender, EventArgs e)
        {
            SumTotalNF();
        }

        private void txtValorFrete_Leave(object sender, EventArgs e)
        {
            if (txtValorFrete.Text == string.Empty)
                txtValorFrete.Text = "0,00";

            SumTotalNF();
        }

        private void txtValorSeguro_Leave(object sender, EventArgs e)
        {
            if (txtValorSeguro.Text == string.Empty)
                txtValorSeguro.Text = "0,00";

            SumTotalNF();
        }

        private void txtValorTotalIPI_Leave(object sender, EventArgs e)
        {
            if (txtValorTotalIPI.Text == string.Empty)
                txtValorTotalIPI.Text = "0,00";

            SumTotalNF();

            try
            {
                // txtTotalNota.Text = (Convert.ToDecimal(txtTotalNota.Text) + Convert.ToDecimal(txtValorTotalIPI.Text)).ToString("n2");
                txtTotalNota.Text = (Convert.ToDecimal(txtTotalProduto.Text) + (Convert.ToDecimal(TxtValorICMSSubst.Text) * LIS_PRODUTONFEColl.Count) + Convert.ToDecimal(txtValorTotalIPI.Text) ).ToString("n2");
                txtTotalNota2.Text = (Convert.ToDecimal(txtTotalProduto.Text) + (Convert.ToDecimal(TxtValorICMSSubst.Text) * LIS_PRODUTONFEColl.Count) + Convert.ToDecimal(txtValorTotalIPI.Text)).ToString("n2");

            }
            catch (Exception)
            {

                MessageBox.Show("Erro no cálculo");
            }
        }

        private void txtOutraDespAcess_Leave(object sender, EventArgs e)
        {
            if (txtOutraDespAcess.Text == string.Empty)
                txtOutraDespAcess.Text = "0,00";

            SumTotalNF();
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show("Antes de adicionar os Financeiro é necessário gravar a Nota Fiscal!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (ValidaDuplicatas())
                {
                    try
                    {
                        SaveDuplicatas();

                        //Salva a forma de pagamento no Pedido
                        NOTAFISCALEP.Save(Entity);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                    catch (Exception)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    }
                }
            }
        }

        private void SaveDuplicatas()
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", cbFormaPagto.SelectedValue.ToString()));

            ITENSFORMAPAGTOProvider ITENSFORMAPAGTOP = new ITENSFORMAPAGTOProvider();
            ITENSFORMAPAGTOCollection ITENSFORMAPAGTOColl = new ITENSFORMAPAGTOCollection();

            ITENSFORMAPAGTOColl = ITENSFORMAPAGTOP.ReadCollectionByParameter(RowRelatorio, "IDITENSFORMAPAGTO");

            foreach (ITENSFORMAPAGTOEntity item in ITENSFORMAPAGTOColl)
            {
                DUPLICATARECEBEREntity DUPLICATARECEBERty = new DUPLICATARECEBEREntity();
                DUPLICATARECEBERty.IDDUPLICATARECEBER = -1;
                DUPLICATARECEBERty.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                if (cbFuncionario.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = txtNotaFiscal.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());

                decimal Valor = Convert.ToDecimal(txtValorDev.Text);
                //Calculando desconto
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                decimal PorcDesconto = Convert.ToDecimal(FORMAPAGAMENTOP.Read(Convert.ToInt32(item.IDFORMAPAGAMENTO)).PORCDESCONTO);
                if (PorcDesconto > 0)
                    Valor -= ((Valor * PorcDesconto) / 100);


                // Valor = (Convert.ToDecimal(txtTotalOS.Text) * Convert.ToDecimal(item.PORCPAGTO)) / 100;
                Valor = Valor * Convert.ToDecimal(item.PORCPAGTO) / 100;

                //Calculando juros
                Valor = ((Valor * Convert.ToDecimal(item.PORCJUROS)) / 100) + Valor;
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = Valor;
                DUPLICATARECEBERty.IDSTATUS = 1;//Aberto
                DUPLICATARECEBERty.NOTAFISCAL = "NFE" + txtNotaFiscal.Text;

                //Comissao Vendedor
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAONFE == "N")
                {
                    DUPLICATARECEBERty.COMISSAO = Convert.ToDecimal(txtValComissao.Text) / ITENSFORMAPAGTOColl.Count;
                }
                else
                    DUPLICATARECEBERty.COMISSAO = (Valor * Convert.ToDecimal(txtPorComisVend.Text)) / 100;

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas da Nota Fiscal
                    GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }
            }
        }

        private void GridDuplicatasNF(int idcliente, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "NFE" + numero));

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");
                dataGridDupl.AutoGenerateColumns = false;
                dataGridDupl.DataSource = LIS_DUPLICATARECEBERColl;

                if (LIS_DUPLICATARECEBERColl.Count > 0)
                    rbPrazo.Checked = true;
                else
                    rbAvista.Checked = true;

                SumFinanceiroNF();
            }
            catch (Exception ex)
            {
            
                MessageBox.Show("Erro técnico: " + ex.Message);
            
                MessageBox.Show("Erro ao pesquisar duplicatas!!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }

        private void SumFinanceiroNF()
        {
            try
            {
                decimal valorTotal = 0;
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    valorTotal += Convert.ToDecimal(item.VALORDUPLICATA);
                }

                lblTotalFinanceiro.Text = valorTotal.ToString();
                Double f = Convert.ToDouble(lblTotalFinanceiro.Text);
                lblTotalFinanceiro.Text = string.Format("{0:n2}", f);

                lblTotalFinanceiro.Text = "Total de Duplicatas: " + lblTotalFinanceiro.Text;
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean ValidaDuplicatas()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbFormaPagto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFormaPagto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void dataGridDupl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATARECEBERColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                            DUPLICATARECEBERP.Delete(CodSelect);
                            GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            SumFinanceiroNF();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //editar
                {
                    using (FrmContasReceber  frm = new FrmContasReceber())
                    {
                        frm.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                        frm.ShowDialog();

                        GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);
                    }
                    
                }
                else if (ColumnSelecionada == 2) //Boleto
                {
                    ImprimirBoletaDireto(Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER));
                }
	
	
            }
        }

        private void ImprimirBoletaDireto(int IDDUPLICATARECEBER)
        {
            //Selecionar a boleta do config
            CONFISISTEMAEntity ConfigSistemaTy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            ConfigSistemaTy = CONFISISTEMAP.Read(1);
            if (ConfigSistemaTy.IDCONFIGBOLETA == null)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroBoletaSelec,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Busca qual banco sera impresso o boleto
                CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
                CONFIGBOLETAEntity CONFIGBOLETATy = CONFIGBOLETAP.Read(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA));

                PrintBoletoBancario PrintBoletoBancario = new PrintBoletoBancario();

                try
                {
                    switch (CONFIGBOLETATy.IDBANCO)
                    {
                        case 5:
                            //Banco Brasil.
                            PrintBoletoBancario.ImprimiBoletaBrasil(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                    IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                    ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 3:
                            // Banco Basa. 
                            PrintBoletoBancario.ImprimiBoletaBasa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 12:
                            // Banco Santander.                            
                            PrintBoletoBancario.ImprimiBoletaSantander(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 2:
                            //Banco Banrisul
                            PrintBoletoBancario.ImprimiBoletaBanrisul(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 7:
                            //Banco BRB
                            PrintBoletoBancario.ImprimiBoletaBRB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 6:
                            //Banco Caixa
                            PrintBoletoBancario.ImprimiBoletaCaixa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                     IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                     ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 4:
                            //Bradesco
                            PrintBoletoBancario.ImprimiBoletaBradesco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 9:
                            //Itau
                            PrintBoletoBancario.ImprimiBoletaItau(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 13:
                            //Sudameris();
                            PrintBoletoBancario.ImprimiBoletaSudameris(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 10:
                            //Real
                            PrintBoletoBancario.ImprimiBoletaReal(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                       IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                       ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 8:
                            //HSBC
                            PrintBoletoBancario.ImprimiBoletaHSBC(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 14:
                            //Unibanco
                            PrintBoletoBancario.ImprimiBoletaUnibanco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 11:
                            //Safra
                            PrintBoletoBancario.ImprimiBoletaSafra(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 15:
                            //SICOOB
                            PrintBoletoBancario.ImprimirBoletaSICOOB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
		

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_NOTAFISCALEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private decimal SomaTotal()
        {
            decimal result = 0;

            foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
            {
                result += Convert.ToDecimal(item.TOTALNOTA);
            }
            return result;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_NOTAFISCALEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeralProduto();
            }
        }

        private void ImprimirListaGeralProduto()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de NF com Produtos");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            try
            {
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                printDialog1.Document = printDocument3;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument3;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //'Cabecalho
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 60, config.MargemDireita, 60);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 160, config.MargemDireita, 160);

                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        //'Imagem
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 550, 68);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 68);

                //Titulo
                e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("N.F", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 120, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 340, 170);
                e.Graphics.DrawString("Vendedor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, 170);
                e.Graphics.DrawString("Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_NOTAFISCALEColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_NOTAFISCALEColl.Count)
                {
                    if (LIS_NOTAFISCALEColl[IndexRegistro].IDNOTAFISCALE != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_NOTAFISCALEColl[IndexRegistro].NFISCALE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_NOTAFISCALEColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_NOTAFISCALEColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_NOTAFISCALEColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_NOTAFISCALEColl[IndexRegistro].NOMEVENDEDOR, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_NOTAFISCALEColl[IndexRegistro].TOTALNOTA).ToString("n2");
                        e.Graphics.DrawString(TotalFOS, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    //Listar os produtos
                    LIS_PRODUTONFECollection LIS_PRODUTONFEPrintColl = new LIS_PRODUTONFECollection();
                    LIS_PRODUTONFEPrintColl = ProdutoRel(Convert.ToInt32(LIS_NOTAFISCALEColl[IndexRegistro].IDNOTAFISCALE));
                    e.Graphics.DrawString("Cod.Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Produtos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEPrintColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Util.LimiterText(item.IDPRODUTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 25), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.QUANTIDADE.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                    }
                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    string linhasepar = "------------------------------------------------------------------------------------------";
                    string linhasepar2 = "------------------------------------------------------------------------------------------";
                    e.Graphics.DrawString(linhasepar + linhasepar2, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    //IndexRegistro++;
                    //config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_NOTAFISCALEColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_NOTAFISCALEColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


                    //Rodape
                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                    e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                    config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                    config.LinhaAtual++;
                    e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private LIS_PRODUTONFECollection ProdutoRel(int IDNOTAFISCALE)
        {
            LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", IDNOTAFISCALE.ToString()));

            LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTONFEColl;
        }      

        private void boletasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");

            }
            else ImprimirBoleta();
        }

        private void ImprimirBoleta()
        {
            //Selecionar a boleta do config
            CONFISISTEMAEntity ConfigSistemaTy = new CONFISISTEMAEntity();
            ConfigSistemaTy = CONFISISTEMAP.Read(1);
            if (ConfigSistemaTy.IDCONFIGBOLETA == null)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroBoletaSelec,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Busca qual banco sera impresso o boleto
                CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
                CONFIGBOLETAEntity CONFIGBOLETATy = CONFIGBOLETAP.Read(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA));

                PrintBoletoBancario PrintBoletoBancario = new PrintBoletoBancario();

                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    try
                    {
                        switch (CONFIGBOLETATy.IDBANCO)
                        {
                            case 5:
                                //Banco Brasil.
                                PrintBoletoBancario.ImprimiBoletaBrasil(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                        Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                        ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 3:
                                // Banco Basa. 
                                PrintBoletoBancario.ImprimiBoletaBasa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 12:
                                // Banco Santander.                            
                                PrintBoletoBancario.ImprimiBoletaSantander(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 2:
                                //Banco Banrisul
                                PrintBoletoBancario.ImprimiBoletaBanrisul(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 7:
                                //Banco BRB
                                PrintBoletoBancario.ImprimiBoletaBRB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 6:
                                //Banco Caixa
                                PrintBoletoBancario.ImprimiBoletaCaixa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                         Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                         ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 4:
                                //Bradesco
                                PrintBoletoBancario.ImprimiBoletaBradesco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 9:
                                //Itau
                                PrintBoletoBancario.ImprimiBoletaItau(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 13:
                                //Sudameris();
                                PrintBoletoBancario.ImprimiBoletaSudameris(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                         Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 10:
                                //Real
                                PrintBoletoBancario.ImprimiBoletaReal(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                           Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                           ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 8:
                                //HSBC
                                PrintBoletoBancario.ImprimiBoletaHSBC(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 14:
                                //Unibanco
                                PrintBoletoBancario.ImprimiBoletaUnibanco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                          Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 11:
                                //Safra
                                PrintBoletoBancario.ImprimiBoletaSafra(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                         Convert.ToInt32(item.IDDUPLICATARECEBER), ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                          ConfigSistemaTy.FLAGCARNEBOLETA);
                                break;
                            case 15:
                                ////SICOOB
                                PrintBoletoBancario.ImprimirBoletaSICOOB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                         Convert.ToInt32(item.IDDUPLICATARECEBER));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }


            }

        }

        private void viaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");

            }
            else
                ImprimirDuplicata1Via();
        }

        int _CodDuplicata = -1;
        private void ImprimirDuplicata1Via()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument4;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
                    printDocument4.DefaultPageSettings.PaperSize = new
                    System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                    objPrintPreview.Document = printDocument4;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 470);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
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

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                DUPLICATARECEBEREntity item = DUPLICATARECEBERP.Read(_CodDuplicata);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 140, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 145);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 155);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 200);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 200);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 215);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


                //Valor por extenso
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 360, config.MargemDireita - 20, 50);
                e.Graphics.DrawString("VALOR POR", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 370);
                e.Graphics.DrawString("EXTENSO", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 385);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(item.VALORDEVEDOR));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 385);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 415);
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMECLIENTE + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }

        }

        private void viaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
            }
            else
                ImprimirDuplicata2Vias();
        }

        private void ImprimirDuplicata2Vias()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument5;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
                    objPrintPreview.Document = printDocument5;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void printDocument5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio para imprimir a 1º via da Duplicata
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 470);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
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

                DUPLICATARECEBEREntity item = DUPLICATARECEBERP.Read(_CodDuplicata);

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(item.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 140, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 145);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 155);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 200);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 200);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 215);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


                //Valor por extenso
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 360, config.MargemDireita - 20, 50);
                e.Graphics.DrawString("VALOR POR", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 370);
                e.Graphics.DrawString("EXTENSO", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 385);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(item.VALORDEVEDOR));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 385);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 415);
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMECLIENTE + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);
                //Fim da impressão da 1º via da duplicata


                //Inicio para imprimir a 2º via da Duplicata
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 510, config.MargemDireita, 470);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 510, config.MargemDireita, 100);

                //Logomarca
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 518);
                    }
                }

                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 518);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 533);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 548);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 548);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 563);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 578);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 593);

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 518);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 533);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 548);

                //Espaço para dados da duplicata 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 620);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 660);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 620);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 660);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 620);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(item.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 660);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 620);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 660);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 620);
                e.Graphics.DrawString(Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 660);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 620, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 625);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 635);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 685);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 685);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 700);


                //Dados do Cliente
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 750, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 755);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 755);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 770);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 770);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 785);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 785);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 785);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 785);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 785);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 785);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 800);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 800);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 800);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 815);
                //Condição para exibir o CPF ou CNPJ
                CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 815);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 815);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 815);

                //Valor por extenso
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 840, config.MargemDireita - 20, 50);
                e.Graphics.DrawString("VALOR POR", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 850);
                e.Graphics.DrawString("EXTENSO", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 865);
                NpExtenso.SetNumero(Convert.ToDecimal(item.VALORDEVEDOR));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 865);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 895);
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMECLIENTE + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 910);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 940);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 955);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 955);
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }

        }

        private void compostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControl1.SelectTab(0);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);
            }
            else
                ImprimirDuplicata1ViaComposta();
        }

        private void ImprimirDuplicata1ViaComposta()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument6;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument6.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                objPrintPreview.Document = printDocument6;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument6_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 470);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
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

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                //Filtro das duplicatas compostas
                RowsFiltroCollection RowDuplicata = new RowsFiltroCollection();
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "NFE" + txtNotaFiscal.Text));
                DUPLICATARECEBERCollection DUPLICATARECEBERCollC = new DUPLICATARECEBERCollection();
                DUPLICATARECEBERCollC = DUPLICATARECEBERP.ReadCollectionByParameter(RowDuplicata, "DATAVECTO");

                //Busca o ultimo vecto
                //e soma os totais da duplicata
                Decimal TotalDuplicata = 0;
                DateTime UltimoVecto = Convert.ToDateTime(DUPLICATARECEBERCollC[DUPLICATARECEBERCollC.Count - 1].DATAVECTO);
                foreach (DUPLICATARECEBEREntity item in DUPLICATARECEBERCollC)
                {
                    TotalDuplicata += Convert.ToDecimal(item.VALORDEVEDOR);
                    UltimoVecto = Convert.ToDateTime(item.DATAVECTO);
                }


                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString("PD" + txtNotaFiscal.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString("PD" + txtNotaFiscal.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(UltimoVecto).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 140, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 145);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 155);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 200);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 200);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 215);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


                //Valor por extenso
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 360, config.MargemDireita - 20, 50);
                e.Graphics.DrawString("VALOR POR", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 370);
                e.Graphics.DrawString("EXTENSO", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 385);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(TotalDuplicata));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 385);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 415);
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMECLIENTE + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);


                //Alinhamento dos valores
                StringFormat formataString = new StringFormat();
                formataString.Alignment = StringAlignment.Far;
                formataString.LineAlignment = StringAlignment.Far;

                //Rodape com a informação sobre todas as duplicatas
                //1º Coluna
                e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 10, 510);
                e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 100, 510);
                e.Graphics.DrawString("Vecto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 150, 510);

                //2º Coluna
                if (DUPLICATARECEBERCollC.Count > 3)
                {
                    e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 220, 510);
                    e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 310, 510);
                    e.Graphics.DrawString("Vecto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 360, 510);
                }

                //3º Coluna
                if (DUPLICATARECEBERCollC.Count > 6)
                {
                    e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 430, 510);
                    e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 520, 510);
                    e.Graphics.DrawString("Vecto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 570, 510);
                }

                int linha = 525;
                int linha2 = 525;
                int linha3 = 525;
                for (int i = 0; i < DUPLICATARECEBERCollC.Count; i++)
                {
                    if (i < 3)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, linha);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 140, linha + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 150, linha);
                        linha = linha + 15;
                    }
                    else if (i < 6)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 220, linha2);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, linha2 + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 360, linha2);
                        linha2 = linha2 + 15;
                    }
                    else if (i < 9)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 430, linha3);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 560, linha3 + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 570, linha3);
                        linha3 = linha3 + 15;
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void tabuladorDeFormulárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void notaFiscalTabuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if ( _IDNOTAFISCALE != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
            {
                Grava();
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void cbCFOPProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbCFOPProduto.SelectedValue) > 0)
                txtNatOperProduto.Text = (CFOPP.Read(Convert.ToInt32(cbCFOPProduto.SelectedValue)).DESCRICAO);
            else
                txtNatOperProduto.Text = string.Empty;
        }

        private void imprimirDANFEToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void ImprimirDANFE(Boolean valido)
        {
            IndexRegistro = 0;
            try
            {
                _DANFEVALIDO = valido;
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument2;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument2_BeginPrint(object sender, PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;

                Graphics objG = e.Graphics;
                //RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                float esquerda = 10;
                float linha = 20;
                float largura = 620;
                float altura = 30;
                float raio = 2;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);

                Font FonteNormal = new Font("Arial", 6);
                FonteNormal = new Font("Arial", 8, FontStyle.Bold);


                e.Graphics.DrawString("RECEBEMOS DE " + EMPRESATy.NOMECLIENTE + " OS PRODUTOS/SERVIÇOS CONSTANTES DA NOTA FISCAL INDICADO AO LADO", FonteNormal, Brushes.Black, esquerda + 5, linha);

                esquerda = 630; linha = 20; largura = 160; altura = 60;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 15, FontStyle.Bold);
                e.Graphics.DrawString("NF-e", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 8, FontStyle.Bold);

                e.Graphics.DrawString("Nº " + Entity.NOTAFISCALE, FonteNormal, Brushes.Black, esquerda + 5, linha + 30);

                e.Graphics.DrawString("SÉRIE " + Entity.SERIE, FonteNormal, Brushes.Black, esquerda + 5, linha + 40);

                FonteNormal = new Font("Arial", 6);
                esquerda = 10; linha = 50; largura = 220; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("DATA DO RECEBIMENTO", FonteNormal, Brushes.Black, esquerda + 5, linha);

                esquerda = 230; linha = 50; largura = 400; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("IDENTIFICAÇÃO E ASSINATURA DO RECEBEDOR", FonteNormal, Brushes.Black, esquerda + 5, linha);

                float[] dashValues = { 5, 5, 5, 5 };
                Pen blackPen = new Pen(Color.Black, 1);
                blackPen.DashPattern = dashValues;
                e.Graphics.DrawLine(blackPen, new Point(10, 85), new Point(790, 85));

                esquerda = 140;
                //Condição para exibir logo
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGONFE == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        //'Imagem
                        e.Graphics.DrawImage(Image.FromStream(stream), esquerda + 5, 130);
                    }
                }

                FonteNormal = new Font("Arial", 6);
                esquerda = 10; linha = 95; largura = 330; altura = 120;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("IDENTIFICAÇÃO DO EMITENTE", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, FonteNormal, Brushes.Black, esquerda + 5, linha + 30);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, FonteNormal, Brushes.Black, esquerda + 5, linha + 40);
                e.Graphics.DrawString(EMPRESATy.BAIRRO, FonteNormal, Brushes.Black, esquerda + 5, linha + 50);
                e.Graphics.DrawString(EMPRESATy.CIDADE + " " + EMPRESATy.UF, FonteNormal, Brushes.Black, esquerda + 5, linha + 60);
                e.Graphics.DrawString(EMPRESATy.CEP, FonteNormal, Brushes.Black, esquerda + 5, linha + 70);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, FonteNormal, Brushes.Black, esquerda + 5, linha + 80);

                FonteNormal = new Font("Arial", 15, FontStyle.Bold);
                esquerda = 340; linha = 95; largura = 130; altura = 120;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("DANFE", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("Documento Auxiliar da Nota", FonteNormal, Brushes.Black, esquerda + 5, linha + 20);
                e.Graphics.DrawString("Fiscal Eletrônica", FonteNormal, Brushes.Black, esquerda + 5, linha + 30);

                e.Graphics.DrawString("0 - ENTRADA", FonteNormal, Brushes.Black, esquerda + 5, linha + 50);
                e.Graphics.DrawString("1 - SAÍDA", FonteNormal, Brushes.Black, esquerda + 5, linha + 60);

                esquerda = 450; linha = linha + 50; largura = 15; altura = 20;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                //Entrada = 0
                //Saida 1
                string TipoNF = Entity.IDTIPOMOVIM == 2 ? "1" : "0";
                e.Graphics.DrawString(TipoNF, FonteNormal, Brushes.Black, esquerda + 6, linha + 5);

                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                esquerda = 340;
                e.Graphics.DrawString("Nº " + Entity.NOTAFISCALE, FonteNormal, Brushes.Black, esquerda + 5, linha + 30);
                e.Graphics.DrawString("SÉRIE " + Entity.SERIE, FonteNormal, Brushes.Black, esquerda + 5, linha + 40);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("Folha 1/1", FonteNormal, Brushes.Black, esquerda + 5, linha + 60);

                //Codigo de Barra da chave de acesso
                esquerda = 470; linha = 95; largura = 320; altura = 100;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                PictureBox pict1 = new PictureBox();
                BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();

                barcode.Height = 69;
                barcode.Width = 300;

                string CHAVEACESSO = string.Empty;
                if (_DANFEVALIDO)
                {
                    CHAVEACESSO = Entity.CHAVEACESSO.Replace("N", "").Replace("F", "").Replace("e", "");

                    pict1.Image = barcode.Encode(BarcodeLib.TYPE.CODE128, CHAVEACESSO);
                    esquerda = 475;
                    e.Graphics.DrawString("CHAVE DE ACESSO", FonteNormal, Brushes.Black, esquerda + 5, linha);

                    e.Graphics.DrawImage(pict1.Image, esquerda, linha + 10);
                    e.Graphics.DrawString(CHAVEACESSO, FonteNormal, Brushes.Black, esquerda + 5, linha + 90);
                }

                esquerda = 470; linha = 195; largura = 320; altura = 20;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("Consulta de autenticidade no portal nacional da NF-e ", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("www.nfe.fazenda.gov.br/portal ou no site da Sefaz Autorizada.", FonteNormal, Brushes.Black, esquerda + 5, linha + 10);


                //Armazena na coleção da Nota Fiscal Eletrônica
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", Entity.IDNOTAFISCALE.ToString()));
                LIS_NOTAFISCALECollection LIS_NOTAFISCALECollPrint = new LIS_NOTAFISCALECollection();
                LIS_NOTAFISCALECollPrint = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                //Natureza da operação
                esquerda = 10; linha = 215; largura = 460; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("NATUREZA DA OPERAÇÃO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].DESCCFOP, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                FonteNormal = new Font("Arial", 6);
                esquerda = 470; linha = 215; largura = 320; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("PROTOCOLO DE AUTORIZAÇÃO DE USO", FonteNormal, Brushes.Black, esquerda + 5, linha);

                esquerda = 10; linha = 245; largura = 230; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("INSCRIÇÃO ESTADUAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(EMPRESATy.IE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                FonteNormal = new Font("Arial", 6);
                esquerda = 240; linha = 245; largura = 260; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("INSCRIÇÃO ESTADUAL DO SUBST. TRIBUT.", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString(Entity.INSCESTSTRIB, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 500; linha = 245; largura = 290; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                e.Graphics.DrawString("CNPJ", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(EMPRESATy.CNPJCPF, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 290; largura = 500; altura = 30;
                e.Graphics.DrawString("DESTINATÁRIO / REMETENTE", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("NOME / RAZÃO SOCIAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].NOMECLIENTE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 510; linha = 290; largura = 200; altura = 30;
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("CNPJ", FonteNormal, Brushes.Black, esquerda + 5, linha);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);

                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_NOTAFISCALECollPrint[0].IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);


                string CNPJCPF = LIS_CLIENTEColl[0].CPF != "   .   .   -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CNPJCPF, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 710; linha = 290; largura = 80; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("DATA EMISSÃO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_NOTAFISCALECollPrint[0].DTEMISSAO).ToString("dd/MM/yyyy"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 320; largura = 400; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("ENDEREÇO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 410; linha = 320; largura = 200; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("BAIRRO / DISTRITO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].BAIRRO1, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 610; linha = 320; largura = 100; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("CEP", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 710; linha = 320; largura = 80; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("DATA SAIDA", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                if (LIS_NOTAFISCALECollPrint[0].DTSAIDA != null)
                    e.Graphics.DrawString(Convert.ToDateTime(LIS_NOTAFISCALECollPrint[0].DTSAIDA).ToString("dd/MM/yyyy"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 350; largura = 400; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("MUNICÍPIO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 410; linha = 350; largura = 30; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("UF", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 440; linha = 350; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("FONE / FAX", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 550; linha = 350; largura = 160; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("INSCRIÇÃO ESTADUAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 710; linha = 350; largura = 80; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("HORA DA SAÍDA", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].HORASAIDA, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 395; largura = 200; altura = 30;
                e.Graphics.DrawString("CÁLCULO DO IMPOSTO", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("BASE DE CÁLCULO DO ICMS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].BASECALCICMS).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 210; linha = 395; largura = 100; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO ICMS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORICMS).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 310; linha = 395; largura = 150; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("BASE DE CÁLCULO DO ICMS S.T", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].BASECALCICMSLSUB).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 460; linha = 395; largura = 140; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO ICMS SUBSTITUIÇÃO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORICMSSUB).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 600; linha = 395; largura = 70; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO PIS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORPIS).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 670; linha = 395; largura = 120; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR TOTAL PRODUTOS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].TOTALPRODUTOS).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO FRETE", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORFRETE).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 120; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO SEGURO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORSEGURO).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 230; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("DESCONTO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORDESCONTO).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 340; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("OUTRAS DESPESAS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].OUTRADESPES).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 450; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR TOTAL DO IPI", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].TOTALIPI).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 560; linha = 425; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO COFINS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].VALORCONFINS).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 670; linha = 425; largura = 120; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR TOTAL NOTA", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_NOTAFISCALECollPrint[0].TOTALNOTA).ToString("n2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 470; largura = 250; altura = 30;
                e.Graphics.DrawString("TRANSPORTADOR / VOLUMES TRANSPORTADOS", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("NOME / RAZÃO SOCIAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].NOMETRANSPORTADORA, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 260; linha = 470; largura = 120; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("FRETE POR CONTA", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                string FreteConta = LIS_NOTAFISCALECollPrint[0].FRETE == 0 ? "(0) Emitente" : "(2) Destinatário";
                e.Graphics.DrawString(FreteConta, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 380; linha = 470; largura = 150; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("CÓDIGO ANTT", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].CODANTT, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 530; linha = 470; largura = 100; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("PLACA DO VÉICULO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].PLACA, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 630; linha = 470; largura = 30; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("UF", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(LIS_NOTAFISCALECollPrint[0].UFTRANSPORTE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                //Armazena dados da Tranportadora
                TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
                string CNPJTranp = string.Empty;
                string ENDERTranp = string.Empty;
                string CIDADETranp = string.Empty;
                string UFTranp = string.Empty;
                string IETranp = string.Empty;
                if (Entity.IDTRANSPORTES != null)
                {
                    TRANSPORTADORATy = TRANSPORTADORAP.Read(Convert.ToInt32(Entity.IDTRANSPORTES));
                    CNPJTranp = TRANSPORTADORATy.CNPJ;
                    ENDERTranp = TRANSPORTADORATy.ENDERECO;
                    CIDADETranp = TRANSPORTADORATy.CIDADE;
                    UFTranp = TRANSPORTADORATy.UF;
                    IETranp = TRANSPORTADORATy.IE;
                }

                esquerda = 660; linha = 470; largura = 130; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("CNPJ/CPF", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(CNPJTranp, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);


                esquerda = 10; linha = 500; largura = 300; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("ENDEREÇO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(ENDERTranp, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 310; linha = 500; largura = 320; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("MUNICÍPIO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(CIDADETranp, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 630; linha = 500; largura = 30; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("UF", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(UFTranp, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 660; linha = 500; largura = 130; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("INSCRIÇÃO ESTADUAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(IETranp, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 10; linha = 530; largura = 70; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("QUANTIDADE", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.QUANT).ToString(), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 80; linha = 530; largura = 200; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("ESPÉCIE", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Entity.ESPECIE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 280; linha = 530; largura = 200; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("MARCA", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Entity.MARCANFE, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 480; linha = 530; largura = 100; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("NÚMERO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Entity.NUMEROS, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 580; linha = 530; largura = 100; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("PESO BRUTO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.PESOBRUTO).ToString(), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 680; linha = 530; largura = 110; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("PESO LÍQUIDO", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.PESOLIQUIDO).ToString(), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                //Produtos
                esquerda = 10; linha = 575; largura = 80; altura = 15;
                e.Graphics.DrawString("DADOS DO PRODUTO/SERVIÇO", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("CÓDIGO PRODUTO", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 90; linha = 575; largura = 240; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("DESCRIÇÃO DO PRODUTO / SERVIÇO", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 330; linha = 575; largura = 60; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("NCM/SH", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 390; linha = 575; largura = 30; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 3);
                e.Graphics.DrawString("CST/CSOSN", FonteNormal, Brushes.Black, esquerda + 1, linha + 5);

                esquerda = 420; linha = 575; largura = 35; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("CFOP", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 455; linha = 575; largura = 30; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("UND", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 485; linha = 575; largura = 40; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("QUANT", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 525; linha = 575; largura = 40; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("VL.UNIT.", FonteNormal, Brushes.Black, esquerda + 5, linha + 5);

                esquerda = 565; linha = 575; largura = 50; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("VALOR", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("TOTAL", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                esquerda = 615; linha = 575; largura = 40; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("B.CALC.", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("ICMS", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                esquerda = 655; linha = 575; largura = 40; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("VALOR", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("ICMS", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                esquerda = 695; linha = 575; largura = 35; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("VALOR", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("IPI", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                esquerda = 730; linha = 575; largura = 30; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("ALÍQ.", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("ICMS", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                esquerda = 760; linha = 575; largura = 30; altura = 15;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 5);
                e.Graphics.DrawString("ALÍQ.", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString("IPI", FonteNormal, Brushes.Black, esquerda + 5, linha + 7);

                //Percorre a coleção de Produtos
                esquerda = 10; linha = 586; largura = 80; altura = 15;
                FonteNormal = new Font("Arial", 5);

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;

                int linhaBorda = 590;
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 80, 20);
                    e.Graphics.DrawString(Convert.ToString(item.IDPRODUTO).ToString(), FonteNormal, Brushes.Black, esquerda + 20, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 320, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO + " " + item.DESCPRODUTO2, 40), FonteNormal, Brushes.Black, 100, linha + 5);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 380, 20);
                    e.Graphics.DrawString(item.NCMSH, FonteNormal, Brushes.Black, 340, linha + 5);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 410, 20);
                    e.Graphics.DrawString(GetCST(Convert.ToInt32(item.IDCST)), FonteNormal, Brushes.Black, 400, linha + 5);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 445, 20);
                    e.Graphics.DrawString(item.CODCFOP, FonteNormal, Brushes.Black, 425, linha + 5);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 475, 20);
                    e.Graphics.DrawString(item.NOMEUNIDADE, FonteNormal, Brushes.Black, 460, linha + 5);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 515, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), FonteNormal, Brushes.Black, 495 + 30, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 555, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), FonteNormal, Brushes.Black, 540 + 25, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 605, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), FonteNormal, Brushes.Black, 590 + 25, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 645, 20);
                    FonteNormal = new Font("Arial", 4);
                    e.Graphics.DrawString(Convert.ToDecimal(item.BASEICMS).ToString("n2"), FonteNormal, Brushes.Black, 630 + 25, linha + 13, stringFormat);

                    FonteNormal = new Font("Arial", 5);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 685, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORICMS).ToString("n2"), FonteNormal, Brushes.Black, 670 + 25, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 720, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORIPI).ToString("n2"), FonteNormal, Brushes.Black, 705 + 25, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 750, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.ALICMS).ToString("n2"), FonteNormal, Brushes.Black, 735 + 25, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, esquerda, linhaBorda, 780, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.ALIPI).ToString("n2"), FonteNormal, Brushes.Black, 765 + 25, linha + 15, stringFormat);

                    linhaBorda = linhaBorda + 10;
                    linha = linha + 10;
                }

                if (!_DANFEVALIDO)
                {
                    esquerda = 100; linha = linha + 30; largura = 200; altura = 30;
                    FonteNormal = new Font("Arial", 30, FontStyle.Bold);
                    e.Graphics.DrawString("SEM VALOR FISCAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                    linha = linha + 30;
                }

                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                esquerda = 10; linha = linha + 30; largura = 200; altura = 30;
                e.Graphics.DrawString("CÁLCULO DO ISSQN", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("INSCRIÇÃO MUNICIPAL", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(CONFISISTEMATy.INSCMUNICIPAL, FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 210; largura = 200; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR TOTAL DOS SERVIÇOS", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORTOTALSERVICO).ToString("N2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 410; largura = 200; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("BASE DE CÁCULO DO ISSQN", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.BASECALCISSQN).ToString("N2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 610; largura = 180; altura = 30;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("VALOR DO ISSQN", FonteNormal, Brushes.Black, esquerda + 5, linha);
                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORISSQN).ToString("N2"), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                FonteNormal = new Font("Arial", 6, FontStyle.Bold);
                esquerda = 10; linha = linha + 45; largura = 500; altura = 100;
                e.Graphics.DrawString("DADOS ADICIONAIS", FonteNormal, Brushes.Black, esquerda + 5, linha - 10);
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("INFORMAÇÕES COMPLEMENTARES", FonteNormal, Brushes.Black, esquerda + 5, linha);
                e.Graphics.DrawString(Util.QuebraString(Entity.INFOCOMPLEM, 120), FonteNormal, Brushes.Black, esquerda + 5, linha + 15);

                esquerda = 510; largura = 280; altura = 100;
                RoundRectangle(objG, objP, esquerda, linha, largura, altura, raio);
                FonteNormal = new Font("Arial", 6);
                e.Graphics.DrawString("RESERVADO AO FISCO", FonteNormal, Brushes.Black, esquerda + 5, linha);

            }
            catch
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        GraphicsPath objGP = new GraphicsPath();
        Pen objP = new Pen(Color.Black);
        public void RoundRectangle(Graphics objG, Pen objP, float h, float v, float width, float height, float radius)
        {
            GraphicsPath objGP = new GraphicsPath();
            objGP.AddLine(h + radius, v, h + width - (radius * 2), v);
            objGP.AddArc(h + width - (radius * 2), v, radius * 2, radius * 2, 270, 90);
            objGP.AddLine(h + width, v + radius, h + width, v + height - (radius * 2));
            objGP.AddArc(h + width - (radius * 2), v + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner
            objGP.AddLine(h + width - (radius * 2), v + height, h + radius, v + height);
            objGP.AddArc(h, v + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            objGP.AddLine(h, v + height - (radius * 2), h, v + radius);
            objGP.AddArc(h, v, radius * 2, radius * 2, 180, 90);
            objGP.CloseFigure();
            objG.DrawPath(objP, objGP);
            objGP.Dispose();
        }

        private string GetCST(int IDCST)
        {
            string result = string.Empty;
            LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();
            LIS_CSTCollection LIS_CSTColl = new LIS_CSTCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCST", "System.Int32", "=", IDCST.ToString()));
            LIS_CSTColl = LIS_CSTP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_CSTColl.Count > 0)
                result = LIS_CSTColl[0].CODCOMPL;

            return result;

        }

        private void txtValorPis_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorPis.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPis.Text))
                {
                    errorProvider1.SetError(txtValorPis, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorPis.Text);
                    txtValorPis.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPis, "");
                }
            }
            else
                txtValorPis.Text = "0,00";
        }

        private void txtconfins_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtconfins.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtconfins.Text))
                {
                    errorProvider1.SetError(txtconfins, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtconfins.Text);
                    txtconfins.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtconfins, "");
                }
            }
            else
                txtconfins.Text = "0,00";
        }

        private void txtValorPis_Enter(object sender, EventArgs e)
        {
            if (txtValorPis.Text == "0,00")
                txtValorPis.Text = string.Empty;
        }

        private void txtconfins_Enter(object sender, EventArgs e)
        {
            if (txtconfins.Text == "0,00")
                txtconfins.Text = string.Empty;
        }

        private void cbTransportadora_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTransportadora.SelectedIndex > 0)
            {
                txtANTT.Text = TRANSPORTADORAP.Read(Convert.ToInt32(cbTransportadora.SelectedValue)).CODANTT;
            }
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
        }


        private void cbServico_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o serviço ou pressione Ctrl+E para pesquisar.";
        }



        private void DGDadosServicos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SERVICONFEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_SERVICONFEColl[rowindex].IDSERVICONFE);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_SERVICONFEColl[rowindex].IDSERVICONFE);
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


        private void txtValorTotalServico_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorTotalServico.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotalServico.Text))
                {
                    errorProvider1.SetError(txtValorTotalServico, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorTotalServico.Text);
                    txtValorTotalServico.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorTotalServico, "");
                }
            }
            else
                txtValorTotalServico.Text = "0,00";
        }

        private void txtAliCofins_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAliCofins.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliCofins.Text))
                {
                    errorProvider1.SetError(txtAliCofins, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtAliCofins.Text);
                    txtAliCofins.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliCofins, "");
                }
            }
            else
                txtAliCofins.Text = "0,00";
        }

        private void txtAliPIS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAliPIS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliPIS.Text))
                {
                    errorProvider1.SetError(txtAliPIS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtAliPIS.Text);
                    txtAliPIS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliPIS, "");
                }
            }
            else
                txtAliPIS.Text = "0,00";
        }

        private void txtAliISSQN_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAliISSQN.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliISSQN.Text))
                {
                    errorProvider1.SetError(txtAliISSQN, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtAliISSQN.Text);
                    txtAliISSQN.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliISSQN, "");

                    txtValorISSQN.Text = (Convert.ToDecimal(txtBasCalcISSQN.Text) * Convert.ToDecimal(txtAliISSQN.Text) / 100).ToString("n2");
                }
            }
            else
                txtAliISSQN.Text = "0,00";
        }

        private void txtValorISSQN_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorISSQN.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorISSQN.Text))
                {
                    errorProvider1.SetError(txtValorISSQN, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorISSQN.Text);
                    txtValorISSQN.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorISSQN, "");

                    txtValorISSQN.Text = (Convert.ToDecimal(txtBasCalcISSQN.Text) * Convert.ToDecimal(txtAliISSQN.Text) / 100).ToString("n2");
                }
            }
            else
                txtValorISSQN.Text = "0,00";
        }

        private void txtAliPIS_Enter(object sender, EventArgs e)
        {
            if (txtAliPIS.Text == "0,00")
                txtAliPIS.Text = string.Empty;
        }

        private void txtAliCofins_Enter(object sender, EventArgs e)
        {
            if (txtAliCofins.Text == "0,00")
                txtAliCofins.Text = string.Empty;
        }

        private void txtAliISSQN_Enter(object sender, EventArgs e)
        {
            if (txtAliISSQN.Text == "0,00")
                txtAliISSQN.Text = string.Empty;
        }

        private void txtValorISSQN_Enter(object sender, EventArgs e)
        {
            if (txtValorISSQN.Text == "0,00")
                txtValorISSQN.Text = string.Empty;
        }

        private void xMLEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                XMLEnvio();
                _FLAGARQUIVOXML = "S";
                //Salvando a Chave de Acesso após criar o arquivo XML
                NOTAFISCALEP.Save(Entity);

            }
        }

        private void XMLEnvio()
        {
            //Armazena na coleção da Nota Fiscal Eletrônica
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", Entity.NOTAFISCALE.ToString()));
            LIS_NOTAFISCALECollection LIS_NOTAFISCALECollXML = new LIS_NOTAFISCALECollection();
            LIS_NOTAFISCALECollXML = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

            //Armazena Dados do Destinatario
            CLIENTEEntity ClienteTyDest = new CLIENTEEntity();
            CLIENTEProvider ClienteP = new CLIENTEProvider();
            ClienteTyDest = ClienteP.Read(Convert.ToInt32(LIS_NOTAFISCALECollXML[0].IDCLIENTE));

            //Armazena dados do Emitente
            EMPRESAEntity EmpresaTy = new EMPRESAEntity();
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EmpresaTy = EMPRESAP.Read(1);

            //abrindo (ou criando) o documento XML para escrita

            string caminhoxml = ConfigSistema1.Default.PathXmlEnvio + @"\NFe" + Entity.NOTAFISCALE + ".xml";
            XmlTextWriter escritor = new XmlTextWriter(caminhoxml, System.Text.Encoding.UTF8);

            //comando de formatacao do documento (indentacao) 
            escritor.Formatting = Formatting.Indented;

            //iniciando o documento 
            escritor.WriteStartDocument();

            //escrevendo o elemento raiz do documento 
            escritor.WriteStartElement("NFe");
            escritor.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");

            escritor.WriteStartElement("infNFe");
            nfec.nfecsharp nfe2 = new nfec.nfecsharp();
            txtChaveAcesso.Text = GeraChaveAcessoNFE(EmpresaTy.CNPJCPF, Entity.NOTAFISCALE);

            escritor.WriteAttributeString("Id", "NFe" + txtChaveAcesso.Text);//Codigo de Acesso + NF

            escritor.WriteAttributeString("versao", "2.00");

            //INICIO DA CRIAÇÃO DO XML

            //INICIO IDENTIFICAÇÃO DA NF-E
            escritor.WriteStartElement("ide");

            //informar o código da UF do emitente do Documento Fiscal, 
            //utilizar a codificação do IBGE (Ex. SP->35, RS->43, etc.)
            escritor.WriteElementString("cUF", CONFISISTEMATy.CODUFIBGE.ToString());

            //informar o código numérico que compõe a Chave de Acesso. 
            //Número aleatório gerado pelo emitente para cada NF-e para evitar acessos indevidos da NF-e.
            escritor.WriteElementString("cNF", Entity.NOTAFISCALE);

            //informar a natureza da operação de que decorrer a saída ou a entrada, 
            //tais como: venda, compra, transferência, devolução, importação, consignação, 
            //remessa (para fins de demonstração, de industrialização ou outra), 
            //conforme previsto na alínea 'i', inciso I, art. 19 do CONVÊNIO S/Nº, de 15 de dezembro de 1970.
            escritor.WriteElementString("natOp", LIS_NOTAFISCALECollXML[0].CODCFOP.Replace(".", ""));

            //informar o indicador da forma de pagamento: 
            //0 - pagamento à vista; 1 - pagamento à prazo; 2 - outros.
            string indpag = LIS_DUPLICATARECEBERColl.Count > 0 ? "1" : "0";
            escritor.WriteElementString("indPag", indpag);

            // informar o código do Modelo do Documento Fiscal, código 55 para a NF-e.
            escritor.WriteElementString("mod", CONFISISTEMATy.MODELONFE);

            // informar a série do Documento Fiscal, informar 0 (zero) para série única.
            //A emissão normal pode utilizar série de 0-889, 
            //a emissão em contingência SCAN deve utilizar série 900-999.
            escritor.WriteElementString("serie", CONFISISTEMATy.SERIENFE);

            //informar o Número do Documento Fiscal.
            escritor.WriteElementString("nNF", Entity.NOTAFISCALE);

            //informar a data de emissão do Documento Fiscal.
            escritor.WriteElementString("dEmi", Convert.ToDateTime(Entity.DTEMISSAO).ToString("yyyy-MM-dd"));

            //informar a data de saída ou entrada da mercadoria ou do produto, pode ser omitido.
            string dSaiEnt = string.Empty;
            if (Entity.DTSAIDA != null)
                dSaiEnt = Convert.ToDateTime(Entity.DTSAIDA).ToString("yyyy-MM-dd");
            escritor.WriteElementString("dSaiEnt", dSaiEnt);

            //informar o código do tipo do Documento Fiscal: 0 - entrada / 1 - saída
            string tpNFe = Entity.IDTIPOMOVIM == 2 ? "1" : "0";
            escritor.WriteElementString("tpNFe", tpNFe);

            // informar o formato de impressão do DANFE: 1-retrato / 2-paisagem.
            escritor.WriteElementString("tpImp", "1");

            // informar o código da forma de emissão: 
            //1 - Normal - emissão normal; 
            //2 - Contingência FS - emissão em contingência com impressão do DANFE em Formulário de Segurança;
            //3 - Contingência SCAN - emissão em contingência no Sistema de Contingência do Ambiente Nacional - SCAN;
            //4 - Contingência DPEC - emissão em contingência com envio da Declaração Prévia de 
            //Emissão em Contingência - DPEC;
            //5 - Contingência FS-DA - emissão em contingência com impressão do DANFE em 
            //Formulário de Segurança para Impressão de Documento Auxiliar de Documento Fiscal 
            //Eletrônico (FS-DA).
            escritor.WriteElementString("tpEmis", "1");

            //informar o código do dígito verificador - DV da Chave de Acesso da NF-e, 
            //o DV será calculado com a aplicação do algoritmo módulo 11 (base 2,9) da Chave de Acesso.
            escritor.WriteElementString("cDV", "1");

            //informar o código de identificação do Ambiente: 1-Produção/ 2-Homologação
            escritor.WriteElementString("tpAmb", CONFISISTEMATy.FLAGAMBIENTENFE);

            //infformar o código da finalidade de emissão da NF-e:
            //1- NF-e normal; 2-NF-e complementar; 3 - NF-e de ajuste.
            escritor.WriteElementString("finNFe", "1");

            //informar o código de identificação do processo de emissão da NF-e:
            //Identificador do processo de emissão da NF-e:
            //0 - emissão de NF-e com aplicativo do contribuinte; 
            //1 - emissão de NF-e avulsa pelo Fisco; 
            //2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, 
            //através do site do Fisco;
            //3- emissão NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
            escritor.WriteElementString("procEmi", "0");

            //informar a versão do processo de emissão da NF-e utilizada (aplicativo emissor de NF-e).
            escritor.WriteElementString("verProc", "NFe_Util_v1.4");

            escritor.WriteEndElement();

            //FIM IDENTIFICAÇÃO DA NF-E

            //INICIO EMITENTE DA NF-E
            escritor.WriteStartElement("emit");


            string CNPJCPF = EmpresaTy.CNPJCPF.Replace(".", "").Replace("-", "").Replace("/", "");
            if (CNPJCPF.Length > 11)
                //informar o CNPJ do emitente, sem formatação ou máscara
                escritor.WriteElementString("CNPJ", CNPJCPF);
            else
                //Informar o CPF do emitente, sem formatação ou máscara, 
                //utilizado apenas quando o fisco emite a nota fiscal
                escritor.WriteElementString("CPF", CNPJCPF);


            //informar a razão social do emitente
            escritor.WriteElementString("xNome", EmpresaTy.NOMECLIENTE);

            //informar o logradouro do emitente
            escritor.WriteElementString("xLgr", EmpresaTy.ENDERECO);

            //informar o número do endereço do emitente, campo obrigatório. 
            //Informar S/N ou . (ponto) ou - (traço) 
            //para evitar falha de schema XML quando não houver número.
            escritor.WriteElementString("nro", "S/N");

            //informar o bairro do endereço do emitente
            escritor.WriteElementString("xBairro", EmpresaTy.BAIRRO);

            //informar o código do município na codificação do IBGE com 7 dígitos
            escritor.WriteElementString("cMun", CONFISISTEMATy.CODMUNIBGE.ToString());

            //informar o nome do município
            escritor.WriteElementString("xMun", EmpresaTy.CIDADE);

            //informar a sigla da UF
            escritor.WriteElementString("UF", EmpresaTy.UF);

            //informar o código do pais na codificação do BACEN, se informado deve ser 1058
            escritor.WriteElementString("cPais", "1058");

            //informar o nome do país, se informado deve ser Brasil ou BRASIL
            escritor.WriteElementString("xPais", "BRASIL");

            //informar o nome do país, se informado deve ser Brasil ou BRASIL
            escritor.WriteElementString("IE", EmpresaTy.IE);

            escritor.WriteEndElement();
            //FIM EMITENTE DA NF-E

            //INICIO DESTINATARIO DA NF-E
            escritor.WriteStartElement("dest");


            //informar o nome do país, se informado deve ser Brasil ou BRASIL
            string CNPJDest = ClienteTyDest.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            string CPFDest = ClienteTyDest.CPF.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            if (CNPJDest.Length > 0)
                //informar o CNPJ do emitente, sem formatação ou máscara
                escritor.WriteElementString("CNPJ", CNPJDest);
            else
                //Informar o CPF do emitente, sem formatação ou máscara, 
                //utilizado apenas quando o fisco emite a nota fiscal
                escritor.WriteElementString("CPF", CPFDest);

            //informar a razão social do destinatário
            escritor.WriteElementString("xNome", ClienteTyDest.NOME);

            //Endereço do Destinatario
            escritor.WriteStartElement("enderDest");
            //informar o logradouro do destinatário
            escritor.WriteElementString("xLgr", ClienteTyDest.ENDERECO1);

            //informar o número do endereço do destinatário, campo obrigatório. 
            //Informar S/N ou . (ponto) ou - (traço) para evitar falha de schema XML 
            //quando não houver número.
            string nro = ClienteTyDest.NUMEROENDER == string.Empty ? "S/N" : ClienteTyDest.NUMEROENDER;
            escritor.WriteElementString("nro", nro);

            //informar o complemento do endereço do destinatário, pode ser omitido
            escritor.WriteElementString("xCpl", ClienteTyDest.COMPLEMENTO1);

            //informar o bairro do endereço do destinatário
            escritor.WriteElementString("xBairro", ClienteTyDest.BAIRRO1);

            //informar o código do município na codificação do IBGE com 7 dígitos
            escritor.WriteElementString("cMun", ClienteTyDest.COD_MUN_IBGE.ToString());

            //informar o nome do município
            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
            
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", ClienteTyDest.COD_MUN_IBGE.ToString()));
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
            escritor.WriteElementString("xMun", LIS_MUNICIPIOSColl[0].MUNICIPIO);

            //informar a sigla da UF
            escritor.WriteElementString("UF", LIS_MUNICIPIOSColl[0].UF);

            //informar o CEP, sem formatação ou máscara, pode ser omitido
            escritor.WriteElementString("CEP", ClienteTyDest.CEP1.Replace("-", ""));

            //informar o código do pais na codificação do BACEN, se informado deve ser 1058
            escritor.WriteElementString("cPais", "1058");

            //informar o nome do país, se informado deve ser Brasil ou BRASIL
            escritor.WriteElementString("xPais", "BRASIL");
            escritor.WriteEndElement();

            //Informar a IE do destinatário, sem formatação ou máscara
            if (ClienteTyDest.IE != string.Empty)
                escritor.WriteElementString("IE", ClienteTyDest.IE);

            escritor.WriteEndElement();
            //FIM DESTINATARIO DA NF-E

            //INICIO Listar os produtos da nf
            int i = 1;
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {

                //Armazena dados do produto
                PRODUTOSEntity ProdutoNFe = new PRODUTOSEntity();
                ProdutoNFe = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                escritor.WriteStartElement("det");
                escritor.WriteAttributeString("nItem", i.ToString());
                escritor.WriteStartElement("prod");

                //informar o código do produto ou serviço. 
                //Preencher com CFOP, caso se trate de itens não relacionados 
                //com mercadorias/produtos e que o contribuinte não possua codificação própria. 
                //Formato ”CFOP9999”.
                escritor.WriteElementString("cProd", item.IDPRODUTO.ToString());

                //informar o GTIN (Global Trade Item Number) do produto, antigo código EAN 
                //ou código de barras. Preencher com o código GTIN-8, GTIN-12, 
                //GTIN-13 ou GTIN-14 (antigos códigos EAN, UPC e DUN-14), 
                //não informar o conteúdo da TAG em caso de o produto não possuir este código.
                escritor.WriteElementString("cEAN", ProdutoNFe.CODBARRA);

                //informar a descrição do produto ou serviço.
                escritor.WriteElementString("xProd", item.NOMEPRODUTO);

                //informar o CFOP - Código Fiscal de Operações e Prestações.
                escritor.WriteElementString("CFOP", item.CODCFOP.Replace(".", ""));

                //informar a unidade de comercialização do produto (Ex. pc, und, dz, kg, etc.).
                escritor.WriteElementString("uCOM", item.NOMEUNIDADE);

                //informar a quantidade de comercialização do produto.
                escritor.WriteElementString("qCOM", Convert.ToDouble(item.QUANTIDADE).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o valor unitário de comercialização do produto.
                escritor.WriteElementString("vUnCOM", Convert.ToDouble(item.VALORUNITARIO).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o valor total bruto do produto ou serviços.
                escritor.WriteElementString("vProd", Convert.ToDouble(item.VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o GTIN (Global Trade Item Number) da unidade de tributação do produto, 
                //antigo código EAN ou código de barras.
                //Preencher com o código GTIN-8, GTIN-12, GTIN-13 ou GTIN-14 
                //(antigos códigos EAN, UPC e DUN-14), não informar o conteúdo da TAG em caso de o
                //produto não possuir este código.
                escritor.WriteElementString("cEANTrib", ProdutoNFe.CODBARRA);

                //informar a unidade de tributação do produto (Ex. pc, und, dz, kg, etc.).
                escritor.WriteElementString("uTriv", item.NOMEUNIDADE);

                //informar a quantidade de tributação do produto.
                escritor.WriteElementString("qTriv", Convert.ToDouble(item.QUANTIDADE).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o valor unitário de tributação do produto..
                escritor.WriteElementString("vUnTriv", Convert.ToDouble(item.VALORUNITARIO).ToString("n2").Replace(".", "").Replace(",", "."));

                //fecha o elemento prod
                escritor.WriteEndElement();

                //Imposto Produto
                escritor.WriteStartElement("imposto");
                //ICMS
                escritor.WriteStartElement("ICMS");

                //ICMS
                //Busca a tributação
                CSTEntity CSTNFeTy = new CSTEntity();
                CSTProvider CSTP = new CSTProvider();
                CSTNFeTy = CSTP.Read(Convert.ToInt32(item.IDCST));

                escritor.WriteStartElement("ICMS" + CSTNFeTy.CODIGO);

                //Busca Origem da Mercadoria
                ORIGEMMERCADORIAEntity ORIGEMMERCADORIANFeTy = new ORIGEMMERCADORIAEntity();
                ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                ORIGEMMERCADORIANFeTy = ORIGEMMERCADORIAP.Read(Convert.ToInt32(CSTNFeTy.IDORIGEM));
                //Origem da mercadoria:
                //0 – Nacional; 
                //1 – Estrangeira – Importação direta; 
                //2 – Estrangeira – Adquirida no mercado interno
                escritor.WriteElementString("orig", ORIGEMMERCADORIANFeTy.CODIGO);

                //Tributação do ICMS 
                escritor.WriteElementString("CST", CSTNFeTy.CODIGO);

                if (item.VALORICMS > 0)
                {
                    //Modalidade de determinação da BC do ICMS 
                    //0 - Margem Valor Agregado (%); 
                    //1 - Pauta (Valor); 
                    //2 - Preço Tabelado Máx. (valor); 
                    //3 - valor da operação.
                    escritor.WriteElementString("modBC", "0");

                    //informar o Percentual de redução da BC ICMS ST
                    if (item.REDICMS > 0)
                        escritor.WriteElementString("pRedBC", Convert.ToDouble(item.REDICMS).ToString("n2").Replace(".", "").Replace(",", "."));

                    //informar o Valor da BC do ICMS do ICMS da operação própria
                    escritor.WriteElementString("vBC", Convert.ToDouble(item.BASEICMS).ToString("n2").Replace(".", "").Replace(",", "."));

                    //informar a Alíquota do ICMS do ICMS da operação própria
                    escritor.WriteElementString("pICMS", Convert.ToDouble(item.ALICMS).ToString("n2").Replace(".", "").Replace(",", "."));

                    //informar o Valor do ICMS do ICMS da operação própria
                    escritor.WriteElementString("vICMS", Convert.ToDouble(item.VALORICMS).ToString("n2").Replace(".", "").Replace(",", "."));
                }

                //fecha o elemento ICMS 
                escritor.WriteEndElement();

                //fecha o elemento ICMS
                escritor.WriteEndElement();
                //fecha o elemento imposto
                escritor.WriteEndElement();

                //fecha o elemebto det
                escritor.WriteEndElement();
                i++;
            }
            //FIM Listar os produtos da nf


            //INICIO Listar os serviços da nfe
            foreach (LIS_SERVICONFEEntity item in LIS_SERVICONFEColl)
            {

                escritor.WriteStartElement("det");
                escritor.WriteAttributeString("nItem", i.ToString());
                escritor.WriteStartElement("prod");

                escritor.WriteElementString("cProd", item.IDSERVICO.ToString());

                //informar a descrição do  serviço.
                escritor.WriteElementString("xProd", item.NOMESERVICO);

                //informar a quantidade de comercialização do serviço.
                escritor.WriteElementString("qCOM", Convert.ToDouble(item.QUANTIDADE).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o valor unitário de comercialização do serviço.
                escritor.WriteElementString("vUnCOM", Convert.ToDouble(item.VALORUNITARIO).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o valor total bruto do produto ou serviços.
                escritor.WriteElementString("vProd", Convert.ToDouble(item.VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", "."));

                //fecha o elemento prod
                escritor.WriteEndElement();

                //fecha o elemebto det
                escritor.WriteEndElement();
                i++;
            }
            //FIM Listar os serviços da nf

            //Inicio Total
            escritor.WriteStartElement("total");

            //Inicio Total ICMS
            escritor.WriteStartElement("ICMSTot");

            //informar o somatório da BC do ICMS informado nos itens
            escritor.WriteElementString("vBC", Convert.ToDouble(Entity.BASECALCICMS).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de ICMS informado nos itens
            escritor.WriteElementString("vICMS", Convert.ToDouble(Entity.VALORICMS).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório da BC ST informado nos itens
            escritor.WriteElementString("vBCST", Convert.ToDouble(Entity.BASECALCICMSLSUB).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório do ICMS ST informado nos itens
            escritor.WriteElementString("vST", Convert.ToDouble(Entity.VALORICMSSUB).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de valor dos produtos informado nos itens
            escritor.WriteElementString("vProd", Convert.ToDouble(Entity.TOTALPRODUTOS).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de valor do Frete informado nos itens
            escritor.WriteElementString("vFrete", Convert.ToDouble(Entity.VALORFRETE).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório valor do Seguro informado nos itens
            escritor.WriteElementString("vSeg", Convert.ToDouble(Entity.VALORSEGURO).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório do Desconto informado nos itens
            escritor.WriteElementString("vDesc", Convert.ToDouble(Entity.VALORDESCONTO).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de II informado nos itens
            escritor.WriteElementString("vII", "0");

            //informar o somatório de IPI informado nos itens
            escritor.WriteElementString("vIPI", Convert.ToDouble(Entity.TOTALIPI).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de PIS informado nos itens
            escritor.WriteElementString("vPIS", Convert.ToDouble(Entity.VALORPIS).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de COFINS informado nos itens
            escritor.WriteElementString("vCOFINS", Convert.ToDouble(Entity.VALORCONFINS).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o somatório de vOutro informado nos itens
            escritor.WriteElementString("vOutro", Convert.ToDouble(Entity.OUTRADESPES).ToString("n2").Replace(".", "").Replace(",", "."));

            //informar o valor total a NF
            escritor.WriteElementString("vNF", Convert.ToDouble(Entity.TOTALNOTA).ToString("n2").Replace(".", "").Replace(",", "."));


            escritor.WriteEndElement();//Fim ICMSTOT


            if (Entity.VALORTOTALSERVICO > 0)
            {
                //Inicio ISSQnTot
                escritor.WriteStartElement("ISSQnTot");

                //informar o valor total do Serviços Pretados
                escritor.WriteElementString("vServ", Convert.ToDouble(Entity.VALORTOTALSERVICO).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o somatório da BC do ISS informado nos itens de Serviços 
                escritor.WriteElementString("vBC", Convert.ToDouble(Entity.BASECALCISSQN).ToString("n2").Replace(".", "").Replace(",", "."));

                //informar o somatório de ISS informado nos itens de Serviços
                escritor.WriteElementString("vISS", Convert.ToDouble(Entity.VALORISSQN).ToString("n2").Replace(".", "").Replace(",", "."));

                /*
               //informar o somatório de PIS informado nos itens de Serviços
              // escritor.WriteElementString("vPIS", Convert.ToDouble(Entity.VALORISSQN).ToString("n2").Replace(".", "").Replace(",", "."));

               //informar o somatório de COFINS informado nos itens de Serviços
               // escritor.WriteElementString("vCOFINS", Convert.ToDouble(Entity.VALORISSQN).ToString("n2").Replace(".", "").Replace(",", "."));
                */
                escritor.WriteEndElement();
                //Fim ISSQnTot 

            }
            escritor.WriteEndElement();
            //Fim Total

            //Inicio Transporte
            escritor.WriteStartElement("transp");

            //informar a modalidade do frete:
            //0-por conta do emitente;
            //1-por conta do destinatário
            string modFrete = Entity.FRETE == 1 ? "1" : "0";
            escritor.WriteElementString("modFrete", modFrete);

            if (Entity.IDTRANSPORTES != null)
            {
                //Inicio transporta
                escritor.WriteStartElement("transporta");
                //Armazena dados Da transportadora
                TRANSPORTADORAEntity TRANSPORTADORANFeTy = new TRANSPORTADORAEntity();
                TRANSPORTADORANFeTy = TRANSPORTADORAP.Read(Convert.ToInt32(Entity.IDTRANSPORTES));

                escritor.WriteElementString("CNPJ", TRANSPORTADORANFeTy.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""));
                escritor.WriteElementString("xNome", TRANSPORTADORANFeTy.NOME);
                escritor.WriteElementString("IE", TRANSPORTADORANFeTy.IE.Replace(".", "").Replace("-", "."));
                escritor.WriteElementString("XEnder", TRANSPORTADORANFeTy.ENDERECO + " - " + TRANSPORTADORANFeTy.BAIRRO);
                escritor.WriteElementString("XMun", TRANSPORTADORANFeTy.CIDADE);
                escritor.WriteElementString("XUf", TRANSPORTADORANFeTy.UF);
                //Fim transporta
                escritor.WriteEndElement();
            }

            //Inicio transporta
            escritor.WriteStartElement("veicTransp");

            //informar a placa do veículo, somente letras e dígitos
            escritor.WriteElementString("placa", Entity.PLACA.Replace(".", "").Replace("-", ""));

            //informar a sigla da UF do registro do veículo
            escritor.WriteElementString("UF", Entity.UFTRANSPORTE);

            //informar o RNTC - Registro Nacional de Transportador de Carga (ANTT)
            escritor.WriteElementString("RNTC", Entity.CODANTT);
            escritor.WriteEndElement();
            //Fim transporta    

            //Inicio Vol
            escritor.WriteStartElement("vol");

            //informar a quantidade de volume transportados
            escritor.WriteElementString("qVol", Convert.ToDecimal(Entity.QUANT).ToString("n2").Replace(".", "").Replace(",", "."));
            //informar a espécie dos volume transportados
            escritor.WriteElementString("esp", Entity.ESPECIE);
            //informar a marca dos volume transportados
            escritor.WriteElementString("marca", Entity.MARCANFE);
            //informar a numeração dos volume transportados
            escritor.WriteElementString("nVol", Entity.NUMEROS);
            //informar o peso líquido em kg dos volumes transportados
            escritor.WriteElementString("pesoL", Convert.ToDecimal(Entity.PESOLIQUIDO).ToString("n2").Replace(".", "").Replace(",", "."));
            //informar o peso líquido em kg dos volumes transportados
            escritor.WriteElementString("pesoB", Convert.ToDecimal(Entity.PESOBRUTO).ToString("n2").Replace(".", "").Replace(",", "."));

            escritor.WriteEndElement();
            //Fim Vol

            //Inicio infAdic
            escritor.WriteStartElement("infAdic");
            escritor.WriteElementString("infAdFisco", Entity.INFOCOMPLEM);
            escritor.WriteEndElement();
            //Fim infAdic

            escritor.WriteEndElement();
            //Fimr Transporte


            //fechando o documento 
            escritor.WriteEndDocument();
            escritor.Close();
        }

        private string GeraChaveAcessoNFE(string CNPJ, string NumeroNFe)
        {

            string result = string.Empty;

            //  nfec.nfecsharp nfe2 = new nfec.nfecsharp();
            //nfe2.ChaveAcessoNFe(Convert.ToInt32(CONFISISTEMATy.CODUFIBGE).ToString(), Convert.ToDateTime(Entity.DTEMISSAO).ToString("yy"),
            //                    CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""),ONFISISTEMATy.MODELONFE.ToString().PadLeft(2, '0'),
            //                     CONFISISTEMATy.SERIENFE.ToString().PadLeft(3, '0'), NumeroNFe, 1,  codNum 

            //informar o conteúdo da tag cUF - código da UF na codificação do IBGE: Ex. 35=SP, 43=RS, etc.
            result = Convert.ToInt32(CONFISISTEMATy.CODUFIBGE).ToString();

            //informar o Ano da data de emissão com dois dígitos
            result += Convert.ToDateTime(Entity.DTEMISSAO).ToString("yy");

            //informar o Mês da data de emissão com dois dígitos
            result += Convert.ToDateTime(Entity.DTEMISSAO).ToString("MM");

            //informar o conteúdo da tag CNPJ - CNPJ do emissor
            result += CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

            //informar o conteúdo da tag modelo - modelo da NF-e (valor fixo: 55)
            result += CONFISISTEMATy.MODELONFE.ToString().PadLeft(2, '0'); ;

            //informar o conteúdo da tag serie - série da NF-e, informar zero se série única
            result += CONFISISTEMATy.SERIENFE.ToString().PadLeft(3, '0'); ;

            //informar o conteúdo da tag nNF - número da NF-e
            result += NumeroNFe;

            //informar uma literal que será utilizada para gerar o cNF - 
            //Código Numérico que compõe a Chave de Acesso, deve ser uma literal 
            //única para o emissor e dele depende o segredo da formação do cNF, 
            //pois é este código que vai individualizar o algoritmo de cálculo que é pública.
            //Gerando numero randomico     
            result += (GerarCodigoNumerico(Convert.ToInt32(NumeroNFe))).ToString();

            //retorna o DV da Chave de Acesso, deve ser informado na tag cDV           
            int dv = GerarDigito(result);

            result += dv;


            return result;



        }

        private void assinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionarCertificado();
                string ArquivoXMLEnvio = ConfigSistema1.Default.PathXmlEnvio + @"\NFe" + Entity.NOTAFISCALE + ".xml";
                string arquivoAssinado = AssinarXML(ArquivoXMLEnvio, "infNFe", oCertificado);
                _FLAGASSINATURA = "S";
                //Salvando a Chave de Acesso após criar o arquivo XML
                NOTAFISCALEP.Save(Entity);

                //Deleta o arquivo para criar um novo liberado
                File.Delete(ArquivoXMLEnvio);

                //Abre o arquivo
                StreamWriter valor = new StreamWriter(ArquivoXMLEnvio, true, Encoding.ASCII);
                valor.WriteLine(arquivoAssinado);

                //Fecha o arquivo
                valor.Close();
                MessageBox.Show("Arquivo de envio assinado com suceso!");
            }
            catch
            {

                MessageBox.Show("Houve um erro ao assinar o arquivo de envio!");
            }
        }

        public X509Certificate2 oCertificado { get; set; }

        public void SelecionarCertificado()
        {
            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

            int indice = 0;
            foreach (X509Certificate2 item in collection2)
            {
                if (item.SerialNumber == CONFISISTEMATy.SERIALCERTFDIGITAL)
                {
                    oX509Cert = collection2[indice];
                    oCertificado = oX509Cert;
                    break;
                }

                indice++;
            }

        }

        int vResultado;
        String vResultadoString;
        String vXMLStringAssinado;
        public string AssinarXML(string pArqXMLAssinar, string pUri, X509Certificate2 pCertificado)
        {
            // open the XML file 
            StreamReader SR = File.OpenText(pArqXMLAssinar);
            String vXMLString = SR.ReadToEnd();
            SR.Close();

            // return parameters
            this.vResultado = 0;
            this.vResultadoString = "Assinatura realizada com sucesso";
            this.vXMLStringAssinado = String.Empty;

            try
            {
                // checking if there is a certified used on xml sign
                string _xnome = "";
                if (pCertificado != null)
                    _xnome = pCertificado.Subject.ToString();

                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);

                if (collection1.Count == 0)
                {
                    this.vResultado = 2;
                    this.vResultadoString = "Problemas no certificado digital";
                }
                else
                {
                    _X509Cert = collection1[0];
                    string x;
                    x = _X509Cert.GetKeyAlgorithm().ToString();

                    // Create a new XML document.
                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it’s name.
                    try
                    {
                        doc.LoadXml(vXMLString);

                        // cheching the elemento will be sign 
                        int qtdeRefUri = doc.GetElementsByTagName(pUri).Count;

                        if (qtdeRefUri == 0)
                        {
                            this.vResultado = 4;
                            this.vResultadoString = "A tag de assinatura " + pUri.Trim() + " não existe";
                        }
                        else
                        {
                            if (qtdeRefUri > 1)
                            {
                                this.vResultado = 5;
                                this.vResultadoString = "A tag de assinatura " + pUri.Trim() + " não é unica";
                            }
                            else
                            {
                                try
                                {
                                    // Create a SignedXml object.
                                    SignedXml signedXml = new SignedXml(doc);

                                    // Add the key to the SignedXml document
                                    signedXml.SigningKey = _X509Cert.PrivateKey;

                                    // Create a reference to be signed
                                    Reference reference = new Reference();

                                    XmlAttributeCollection _Uri = doc.GetElementsByTagName(pUri).Item(0).Attributes;
                                    foreach (XmlAttribute _atributo in _Uri)
                                    {
                                        if (_atributo.Name == "Id")
                                            reference.Uri = "#" + _atributo.InnerText;
                                    }

                                    // Add an enveloped transformation to the reference.
                                    XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                    reference.AddTransform(env);

                                    XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                    reference.AddTransform(c14);

                                    // Add the reference to the SignedXml object.
                                    signedXml.AddReference(reference);

                                    // Create a new KeyInfo object
                                    KeyInfo keyInfo = new KeyInfo();

                                    // Load the certificate into a KeyInfoX509Data object
                                    // and add it to the KeyInfo object.
                                    keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                    // Add the KeyInfo object to the SignedXml object.
                                    signedXml.KeyInfo = keyInfo;
                                    signedXml.ComputeSignature();

                                    // Get the XML representation of the signature and save
                                    // it to an XmlElement object.
                                    XmlElement xmlDigitalSignature = signedXml.GetXml();

                                    // save element on XML 
                                    doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                    XmlDocument XMLDoc = new XmlDocument();
                                    XMLDoc.PreserveWhitespace = false;
                                    XMLDoc = doc;

                                    // XML document already signed 
                                    vXMLStringAssinado = XMLDoc.OuterXml;
                                }
                                catch (Exception caught)
                                {
                                    this.vResultado = 6;
                                    this.vResultadoString = "Erro ao assinar o documento - " + caught.Message;
                                }
                            }
                        }
                    }
                    catch (Exception caught)
                    {
                        this.vResultado = 3;
                        this.vResultadoString = "XML mal formado - " + caught.Message;
                    }
                }
            }
            catch (Exception caught)
            {
                this.vResultado = 1;
                this.vResultadoString = "Problema ao acessar o certificado digital" + caught.Message;
            }

            return vXMLStringAssinado;
        }

        private void enviarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  string FLAGAMBIENTENFE = rbProducaoNFe.Checked == true ? "1" : "2";
            //1 - Produção
            int AMBIENTE = CONFISISTEMATy.FLAGAMBIENTENFE == "1" ? 1 : 2;
            obterEnderecoWebService(Convert.ToInt32(CONFISISTEMATy.CODUFIBGE), AMBIENTE);
        }

        string endRecepcao;
        string endConsulta;
        string endCancelamento;
        string endInutilizacao;
        string endRetRecepcao;
        string endStatusServico;
        public void obterEnderecoWebService(int codUF, int ambiente)
        {
            string uf = "";
            switch (codUF)
            {
                case 12: uf = "AC"; break;
                case 27: uf = "AL"; break;
                case 16: uf = "AP"; break;
                case 13: uf = "AM"; break;
                case 29: uf = "BA"; break;
                case 23: uf = "CE"; break;
                case 53: uf = "DF"; break;
                case 52: uf = "GO"; break;
                case 21: uf = "MA"; break;
                case 31: uf = "MG"; break;
                case 50: uf = "MS"; break;
                case 51: uf = "MT"; break;
                case 15: uf = "PA"; break;
                case 25: uf = "PB"; break;
                case 26: uf = "PE"; break;
                case 22: uf = "PI"; break;
                case 41: uf = "PR"; break;
                case 33: uf = "RJ"; break;
                case 24: uf = "RN"; break;
                case 11: uf = "RO"; break;
                case 14: uf = "RR"; break;
                case 43: uf = "RS"; break;
                case 42: uf = "SC"; break;
                case 28: uf = "SE"; break;
                case 35: uf = "SP"; break;
                case 17: uf = "TO"; break;
            }

            string nomeAmbiente = "";
            if (ambiente == 1)
                nomeAmbiente = "URLProducao";
            else
                nomeAmbiente = "URLHomologacao";

            XmlDocument documento = new XmlDocument();
            if (!File.Exists(ConfigSistema1.Default.PathInstall + @"\webservice\webservice.xml"))
                MessageBox.Show("Arquivo com endereço do WebService não foi encontrado, verifique a existência do arquivo webservice.xml na pasta do sistema");

            documento.Load(ConfigSistema1.Default.PathInstall + @"\webservice\webservice.xml"); //deve estar na mesma pasta do sistema

            XmlNodeList nodeEstado = documento.GetElementsByTagName("Estado");
            if (nodeEstado.Count == 0)
                MessageBox.Show("Estado informado não está com os endereços de WebService cadastrado.");

            try
            {
                int i = 0;
                foreach (XmlElement element in nodeEstado)
                {
                    if (element.GetAttribute("UF") == uf)
                    {
                        endRecepcao = nodeEstado.Item(i)[nomeAmbiente]["NFeRecepcao"].InnerText;
                        endConsulta = nodeEstado.Item(i)[nomeAmbiente]["NFeConsulta"].InnerText;
                        endCancelamento = nodeEstado.Item(i)[nomeAmbiente]["NFeCancelamento"].InnerText;
                        endInutilizacao = nodeEstado.Item(i)[nomeAmbiente]["NFeInutilizacao"].InnerText;
                        endRetRecepcao = nodeEstado.Item(i)[nomeAmbiente]["NFeRetRecepcao"].InnerText;
                        endStatusServico = nodeEstado.Item(i)[nomeAmbiente]["NFeStatusServico"].InnerText;

                        break;
                    }

                    i++;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Houve um erro ao enviar o arquivo XML.");
            }
        }

        private void linkMsgClassFiscal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int i = 0;
            //Criar o array q vai armazenar os idclassifical
            int[] IdClassFiscal = new int[LIS_PRODUTONFEColl.Count];

            //Percorre a coleção de produtos armazenando o idclassifical
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                IdClassFiscal[i] = Convert.ToInt32(item.IDCLASSFISCAL);
                i++;
            }

            //Eliminar os idclassifical repetidos
            var unicos = (from n in IdClassFiscal select n).Distinct();
            foreach (var item in unicos)
            {
                CLASSFISCALEntity CLASSFISCALty = new CLASSFISCALEntity();
                CLASSFISCALProvider CLASSFISCALP = new CLASSFISCALProvider();
                CLASSFISCALty = CLASSFISCALP.Read(item);

                //exibir as mensagens da classfiscal
                if (CLASSFISCALty != null)
                    GetMensagemClassFiscal(Convert.ToInt32(CLASSFISCALty.IDMENSAGEMNFE));
            }
        }

        public Int32 GerarDigito(string chave)
        {
            int i, j, Digito;
            const string PESO = "4329876543298765432987654329876543298765432";

            chave = chave.Replace("NFe", "");
            if (chave.Length != 43)
                throw new Exception("Erro na composição da chave para obter o DV (" + chave.Length.ToString() + ")");

            // Manual Integracao Contribuinte v2.02a - Página: 70 //
            j = 0;
            Digito = -1;
            try
            {
                for (i = 0; i < 43; ++i)
                    j += Convert.ToInt32(chave.Substring(i, 1)) * Convert.ToInt32(PESO.Substring(i, 1));
                Digito = 11 - (j % 11);
                if ((j % 11) < 2)
                    Digito = 0;
            }
            catch
            {
                Digito = -1;
            }
            if (Digito == -1)
                throw new Exception("Erro no cálculo do DV");
            return Digito;
        }

        public Int32 GerarCodigoNumerico(Int32 numeroNF)
        {
            string s;
            Int32 i, j, k;

            // Essa função gera um código numerico atravéz de calculos realizados sobre o parametro numero
            s = numeroNF.ToString("000000000");
            for (i = 0; i < 9; ++i)
            {
                k = 0;
                for (j = 0; j < 9; ++j)
                    k += Convert.ToInt32(s[j]) * (j + 1);
                s = (k % 11).ToString().Trim() + s;
            }
            return Convert.ToInt32(s.Substring(0, 9));


        }

        private void préVisualizarDANFESemValorFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirDANFE(false);
            }
        }


        private void GeraXMLNFeCompProduto(Boolean assinar)
        {
            try
            {
                //Armazena dados da Empresa Emitente
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                //Armazena dados da Configuração
                CONFISISTEMAProvider CONFISISTEMAGeraNFeCompP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAGeraNFeCompty = new CONFISISTEMAEntity();
                CONFISISTEMAGeraNFeCompty = CONFISISTEMAGeraNFeCompP.Read(1);

                //Armazena na coleção da Nota Fiscal Eletrônica

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.String", "=", Entity.IDNOTAFISCALE.ToString()));
                LIS_NOTAFISCALECollection LIS_NOTAFISCALECollPrint = new LIS_NOTAFISCALECollection();
                LIS_NOTAFISCALECollPrint = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                //Armazena Dados do Destinatario
                CLIENTEEntity ClienteTyDest = new CLIENTEEntity();
                CLIENTEProvider ClienteP = new CLIENTEProvider();
                ClienteTyDest = ClienteP.Read(Convert.ToInt32(cbCliente.SelectedValue));

                if (ClienteTyDest == null)
                {
                    MessageBox.Show("A cidade do cliente não foi selecionada!",
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error,
                                 MessageBoxDefaultButton.Button1);
                }

                //informar o nome do município
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", ClienteTyDest.COD_MUN_IBGE.ToString()));
                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                //string[] ide, emit, dest, total, transp, cobr, infAdic, autXML;
                string[] ide, emit, dest, total, transp, cobr, pag, infAdic, autXML;
                string[,] prod;

                ide = new string[200];// ide = new string[28]; 
                emit = new string[200];// emit = new string[15];
                dest = new string[200];//dest = new string[41]; 

                int QuantProdServico = LIS_PRODUTONFEColl.Count + LIS_SERVICONFEColl.Count;
                 prod = new string[Convert.ToInt32(Convert.ToInt32(QuantProdServico)), 200];  // prod = new string[Convert.ToInt32(Convert.ToInt32(QuantProdServico)), 155];
                total = new string[200];//total = new string[38]; 
                transp = new string[200];// transp = new string[28]; 
                cobr = new string[200];//cobr = new string[6]; 
                infAdic = new string[200];//                infAdic = new string[5]; 
                autXML = new string[200];//autXML = new string[1]; 
                pag = new string[200];
                

                /* gerar numero nNF aleatório */

                // Random r = new Random(6);
                // int nNF = (r.Next(6) + DateTime.Now.Millisecond);

                /*<ide> TAG de grupo das informações de identificação da NF-e*/
                ide[0] = Convert.ToString(CONFISISTEMAGeraNFeCompty.CODUFIBGE);            		//<cUF>

                // string NumNF = CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE == 1 ? Entity.NOTAFISCALE.ToString().PadLeft(9, '0') : Entity.NOTAFISCALE.ToString().PadLeft(8, '0');
                ide[1] = Entity.NOTAFISCALE.ToString().PadLeft(8, '0');	          //<cNF> //8 DIGITOS A PARTIR DA VERSAO 2.0 (MANUAL 4.01)
                ide[2] = LIS_NOTAFISCALECollPrint[0].DESCCFOP;                              //<natOp>

                //Apenas para a versao 3.10
                if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 5)//versao 3.10
                {
                    if (rbAvista.Checked)
                        ide[3] = "0";                       //<indPag> //0:Pagamento a vista - 1:Pagamento Prazo -2:Outrosx'
                    else if (rbPrazo.Checked)
                        ide[3] = "1";
                    else if (rbOutros.Checked)
                        ide[3] = "2";
                }
                else
                    ide[3] = "";

                ide[4] = CONFISISTEMATy.MODELONFE;												//<mod>
                ide[5] = CONFISISTEMATy.SERIENFE;												//<serie>

                int NumNF = Convert.ToInt32(Entity.NOTAFISCALE) * 1; //retira zero a esquerda
                ide[6] = NumNF.ToString();                                                                 //<nNF>
                //ide[7] = Convert.ToDateTime(Entity.DTEMISSAO).ToString("yyyy-MM-ddTHH:mm:sszzz");           //<dEmi>


               // ide[7] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");  
                ide[7] = Convert.ToDateTime(Entity.DTEMISSAO).ToString("yyyy-MM-ddTHH:mm:sszzz");           //<dEmi>

                if (Entity.DTSAIDA != null)
                    ide[8] = Convert.ToDateTime(Entity.DTSAIDA).ToString("yyyy-MM-ddTHH:mm:sszzz");         //<dSaiEnt>            
                else
                    ide[8] = Convert.ToDateTime(Entity.DTSAIDA).ToString("yyyy-MM-ddTHH:mm:sszzz");

               // ide[7] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");                       //<dhEmi>
               // ide[8] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");                       //<dhSaiEnt> yyyy-MM-dd 
                ide[8] = Convert.ToDateTime(Entity.DTSAIDA).ToString("yyyy-MM-ddT" + mkdHoraSaida.Text.Trim() + ":sszzz"); //mkdHoraSaida.Text.Trim();    //<dhSaiEnt> yyyy-MM-dd 

                string tpNFe = Entity.IDTIPOMOVIM == 2 ? "1" : "0";
                ide[9] = tpNFe;																	//<tpNF>
                ide[10] = Convert.ToString(CONFISISTEMAGeraNFeCompty.CODMUNIBGE);			    //<cMunFG>
                
                ide[11] = "1";                 													//<tpEmis>
                if (chkContigencia.Checked)
                    ide[11] = "2";   //2 – Contingência FS – emissão em contingência com impressão do DANFE em Formulário de Segurança;
                                     //4 – Contingência DPEC – Emissão da NF-e com Prévio Registro da DPEC no Ambiente Nacional (DPEC emitida anteriormente com informações desta nota);
                                    //55 – Contingência FS-DA - emissão em contingência com impressão do DANFE em Formulário de Segurança para Impressão de Documento Auxiliar de Documento FiscalEletrônico (FS-DA).

                ide[12] = "1";  //1=NF-e normal    //<finNFe> //1=NF-e normal; 2=NF-e complementar; 3=NF-e de ajuste; 4=Devolução de mercadoria.
                 if (chkDevolucao.Checked)
                     ide[12] = "4"; // 4=Devolução de mercadoria. 
                 else if(chkAjuste.Checked)
                    ide[12] = "3"; // 3=NF-e de ajuste.

                //if (CONFISISTEMATy.IDVERSAOXMLNFE == 2)
                //{
                //    if (Entity.HORASAIDA != string.Empty)
                //    {
                //        string HORASAIDA = String.Format("{0:HH:mm:ss}", Entity.HORASAIDA);
                //        ide[14] = HORASAIDA;           //<hSaiEnt> Formato “HH:MM:SS” (v.2.0)
                //    }
                //}

                ide[14] = "0";    //indFinal -> A Tag hSaiEnt foi removida, por isso, em sua posição,  foi adicionada a tag indFinal
                if (ClienteTyDest.IE.Trim() != string.Empty)
                {
                    ide[14] = "0";
                }
                else if (ClienteTyDest.IE.Trim().ToUpper() == "ISENTO")
                {
                    ide[14] = "0";
                }
                else
                {
                    ide[14] = "1";
                }


                ide[15] = "";                                                                   //dhCont (ex: 2010-08-27T08:55:33) v6.03 

                ide[16] = "";                                                                   //xJust v6.03

                /* Grupo de informação das NF-e e cupom referenciado - tag NFref */
                ide[13] = txtNFReferenciaDevolucao.Text;   //NFref: <refNFe> 'nf-e relacionadas' //NFref: refNFe ou refECF.nCOO
                ide[17] = "";                 													//NFref: refECF mod
                ide[18] = "";                 													//NFref: refECF nECF

                /* Grupo de informação da NF modelo 1/1A referenciada */
                ide[19] = "";            //cUF    --> Utilizar a Tabela do IBGE
                ide[20] = "";            //AAMM   --> AAMM da emissão da NF
                ide[21] = "";            //CNPJ   --> CNPJ do emitente da NF
                ide[22] = "";            //mod    --> Informar o código do modelo do Documento fiscal: 01 – modelo 01
                ide[23] = "";            //serie  --> nformar a série do documento fiscal
                ide[24] = "";            //nNF    --> 1 – 999999999     

                if (EMPRESATy.UF != LIS_MUNICIPIOSColl[0].UF)
                    ide[25] = "2"; //1 - Operação Interna  - 2 - Operação Interestadual 3 - Operação com Exterior
                else
                    ide[25] = "1"; //1 - Operação Interna  - 2 - Operação Interestadual 3 - Operação com Exterior

                ide[26] = "1";     //indPres
                //                1 - Operação presencial; 
                //2 - Operação não presencial, pela Internet; 
                //3 - Operação não presencial, Teleatendimento; 
                //4 - NFC-e em operação com entrega em domicílio; 
                //9 - Operação não presencial, outros.
                //Conteúdo: "0"

                /* Contingencia */
                ide[15] = "";//  "2014-07-01T17:04:00-03:00";                                                                   //dhCont (ex: 2010-08-27T08:55:33) v6.03 
                ide[16] = "";//  "Estou testando para ver se funciona.";                                                                   //xJust v6.03

                /* VerProc = Versão do aplicativo do cliente */
                //ide[27] = "3.10";
               if(Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 5)
                    ide[27] = "3.10";
                else
                    ide[27] = "4.00";

                    /* Grupo de informação da NFP (modelo 4) referenciada para Notas de Produtores Rurais*/
                    ide[28] = "";            //<cUF>    --> Utilizar a Tabela do IBGE
                ide[29] = "";            //<AAMM>   --> AAMM da emissão da NF
                ide[30] = "";            //<CNPJ>   --> CNPJ do emitente da NF
                ide[31] = "";            //<CPF>    --> CPF do emitente da NF
                ide[32] = "";            //<IE>     --> Inscrição Estadual do emitente da NF
                ide[33] = "";            //<mod>    --> Informar o código do modelo do Documento fiscal: 01 – modelo 01
                ide[34] = "";            //<serie>  --> nformar a série do documento fiscal
                ide[35] = "";            //<nNF>    --> 1 – 999999999
                ide[36] = "";            //<refCTe> --> Referencia ao Cte    

                /*<emit>TAG de grupo de identificação do emitente da NF-e*/

                emit[0] = Util.LimiterText(EMPRESATy.NOMECLIENTE.TrimEnd(),50);			 				 	            //<xNome>
                emit[1] = CONFISISTEMAGeraNFeCompty.NOMEFANTASIA.TrimEnd();				  		 	//<xFant>
                emit[2] = EMPRESATy.ENDERECO.TrimEnd();				 				//<xLgr>
                emit[3] = EMPRESATy.NUMERO.TrimEnd();	    														//<nro>
                emit[4] = EMPRESATy.COMPLEMENTO.TrimEnd();   														    //<xCpl>
                emit[5] = EMPRESATy.BAIRRO.TrimEnd();     									        //<xBairro>
                emit[6] = Convert.ToString(CONFISISTEMAGeraNFeCompty.CODMUNIBGE);			//<cMun>
                emit[7] = EMPRESATy.CIDADE;//Util.CodigodeUFIBGE(EMPRESATy.UF.TrimEnd()).ToString();             									//<xMun>
                emit[8] = EMPRESATy.CEP.Replace("-", "").Replace(".", "");  				//<CEP>
                emit[9] = EMPRESATy.TELEFONE.Replace("-", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace(" ", "");  		//<fone>

                if (EMPRESATy.IE.ToUpper() != "ISENTO")
                    emit[10] = Util.RetiraLetras(EMPRESATy.IE);		//<IE>
                else
                    emit[10] = EMPRESATy.IE;		//<IE>

                emit[11] = CONFISISTEMAGeraNFeCompty.INSCMUNICIPAL;							//<IM>
                emit[12] = CONFISISTEMAGeraNFeCompty.CNAE;									//<CNAE>
                emit[13] = CONFISISTEMAGeraNFeCompty.IEST;									//<IEST>
                emit[14] = CONFISISTEMAGeraNFeCompty.CRT;									//<CRT> 1 – Simples Nacional; 2 – Simples Nacional – excesso de sublimite de receita bruta; 3 – Regime Normal


                /*<dest> TAG de grupo de identificação do Destinatário da NF-e*/
                if (ClienteTyDest.CNPJ == "  .   .   /    -" && ClienteTyDest.CPF == "   .   .   -")
                {
                    dest[0] = "ISENTO";
                }
                else
                {

                    string CNPJDest = ClienteTyDest.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
                    string CPFDest = ClienteTyDest.CPF.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
                    if (CNPJDest.Length > 0)
                        dest[0] = CNPJDest;
                    else
                        //Informar o CPF do emitente, sem formatação ou máscara, 
                        //utilizado apenas quando o fisco emite a nota fiscal
                        dest[0] = CPFDest;
                }

                dest[38] = "";							                //<idEstrangeiro>

                //NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO – SEM VALOR FISCAL
                // Ambiente - 1 Produção - 2 Homologação
                if (CONFISISTEMAP.Read(1).FLAGAMBIENTENFE.TrimEnd() == "2")
                    dest[1] = Util.LimiterText(ClienteTyDest.NOME.TrimEnd(),50);	  //<xNome>
                else
                    dest[1] = Util.LimiterText(ClienteTyDest.NOME.TrimEnd(),50);       					//<xNome>    

                dest[38] = "";							                //<idEstrangeiro>

                dest[2] = ClienteTyDest.ENDERECO1.TrimEnd();	    				//<xLgr>
                dest[3] = ClienteTyDest.NUMEROENDER.TrimEnd();					//<nro>
                dest[4] = ClienteTyDest.COMPLEMENTO1.TrimEnd();					//<xCpl>
                dest[5] = ClienteTyDest.BAIRRO1.TrimEnd();  					    //<xBairro>
                dest[6] = Convert.ToString(ClienteTyDest.COD_MUN_IBGE);	//<cMun>

                //informar o nome do município
                string CidadeCliente = Util.RemoverAcentos(LIS_MUNICIPIOSColl[0].MUNICIPIO);
                dest[7] = CidadeCliente; //	//<xMun>

                dest[8] = LIS_MUNICIPIOSColl[0].UF.Trim();						//<UF>
                dest[9] = ClienteTyDest.CEP1.Replace("-", "");			//<CEP>
                dest[10] = "1058";                        				//<cPais>
                dest[11] = "BRASIL";                       				//<xPais>
                dest[12] = ClienteTyDest.TELEFONE1; //ClienteTyDest.TELEFONE1.Replace("-", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace(" ", "");     					//<fone>

                if (ClienteTyDest.IE.ToUpper() != "ISENTO")
                    dest[13] = Util.RetiraLetras(ClienteTyDest.IE);//<IE>
                else
                    dest[13] = ClienteTyDest.IE;//<IE>

                dest[14] = "";                							//<ISUF>

                dest[40] = "";                                          //<IM>

                /* Grupo de Exportação v6.03 */
                dest[15] = "";														//UFEmbarq
                dest[16] = "";														//xLocEmbarq
                dest[39] = "";                                                      //xLocDespacho

                /* Grupo de Compra v6.03 */
                dest[17] = "";														//xNEmp
                dest[18] = txtNumPedido.Text;										//xPed
                dest[19] = "";														//xCont

                dest[20] = "";														//email

                /* Grupo de identificação do Local de RETIRADA */
                /* Informar apenas quando for diferente do endereço do remetente. */

                dest[21] = "";               					//RETIRADA <CNPJ> ou <CPF>
                dest[22] = "";                          		//RETIRADA <xLgr>
                dest[23] = "";   								//RETIRADA <nro>
                dest[24] = "";           						//RETIRADA <xCpl>
                dest[25] = "";              					//RETIRADA <xBairro>
                dest[26] = "";                           		//RETIRADA <cMun>
                dest[27] = "";                            		//RETIRADA <xMun>
                dest[28] = "";  								//RETIRADA <UF>

                /* Grupo de identificação do Local de ENTREGA */
                /* Informar apenas quando for diferente do endereço do remetente. */
               dest[29] = ""; //ENTREGA <CNPJ> ou <CPF>
                dest[30] = "";                            		//ENTREGA <xLgr>
                dest[31] = "";   								//ENTREGA <nro>
                dest[32] = "";            						//ENTREGA <xCpl>
                dest[33] = "";            						//ENTREGA <xBairro>
                dest[34] = "";                           		//ENTREGA <cMun>
                dest[35] = "";                            		//ENTREGA <xMun>
                dest[36] = "";  								//ENTREGA <UF>


                //<indIEDest> 1=Contribuinte ICMS (informar a IE do destinatário);
                //2=Contribuinte isento de Inscrição no cadastro de Contribuintes do ICMS;
                //9=Não Contribuinte, que pode ou não possuir Inscrição
                if (ClienteTyDest.IE.Trim().ToUpper() == "ISENTO" )
                    dest[37] = "2"; 
                else if (ClienteTyDest.IE.Trim() != string.Empty)
                    dest[37] = "1"; 
                else
                    dest[37] = "9";

                dest[40] = "";                                          //<IM>

                autXML[0] = "";

                //Dados do Contador
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGATIVO",  "System.String", "=", "S"));
                CONTADORCollection CONTADORColl = new CONTADORCollection();
                CONTADORProvider CONTADORProvider = new BMSworks.Firebird.CONTADORProvider();
                CONTADORColl = CONTADORProvider.ReadCollectionByParameter(RowRelatorio);
                if (CONTADORColl.Count > 0 && Util.RetiraLetras(CONTADORColl[0].CNPJ).Length > 0)
                    autXML[0] = CONTADORColl[0].CNPJ;


                /*<prod> TAG de grupo do detalhamento de Produtos e Serviços da NF-e*/
              
                decimal ValorFrete = Convert.ToDecimal(Entity.VALORFRETE) / (QuantProdServico + 1);

                string NumCasasDecimas = CONFISISTEMAP.Read(1).CASADECPRINTDANFE;
                int numitem = 1;
                for (int x = 0; x < QuantProdServico; x++)
                {
                    //Armazena dados do produto
                    PRODUTOSEntity ProdutoNFe = new PRODUTOSEntity();
                    ProdutoNFe = PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTONFEColl[x].IDPRODUTO));

                    if (CONFISISTEMAGeraNFeCompty.FLAGCODREFNFE.Trim() == "S" && ProdutoNFe.CODPRODUTOFORNECEDOR.Trim().Length > 0)
                        prod[x, 0] = ProdutoNFe.CODPRODUTOFORNECEDOR.ToString();    								//<cProd>
                    else
                        prod[x, 0] = LIS_PRODUTONFEColl[x].IDPRODUTO.ToString();    								//<cProd>

                    prod[x, 1] = ProdutoNFe.CODBARRA.TrimEnd();                   										//<cEAN>
                    prod[x, 2] = ProdutoNFe.NOMEPRODUTO.TrimEnd(); // ProdutoNFe.NOMEPRODUTO;                             							//<xProd>
                    prod[x, 3] = ProdutoNFe.NCMSH.Replace(",", "").Replace("-", "").TrimEnd();  							//<NCM>
                    prod[x, 109] = "";                                           								//<NVE>

                    if(ProdutoNFe.CEST.Trim() == string.Empty)
                        prod[x, 146] = "1231237";// "1231237";  		//<CEST>   CEST (Código Especificador da Substituição Tributária)
                    else
                        prod[x, 146] = ProdutoNFe.CEST;// "1231237";  		//<CEST>   CEST (Código Especificador da Substituição Tributária)

                    //indEscala somente na versao 4.0
                    //<indEscala> Indicador de Escala Relevante - S - Produzido em Escala Relevante; N – Produzido em Escala NÃO Relevante.
                    if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 6)//versao 4.00 nfe
                    {
                        prod[x, 186] = "S";   //<indEscala>          

                        prod[x, 187] = "";// "09245135000191";   //<CNPJFab> CNPJ do Fabricante da Mercadoria, obrigatório para produto em escala NÃO relevante.
                        prod[x, 188] = "";//0123456789";        //<cBenef> Código de Benefício Fiscal utilizado pela UF, aplicado ao item.
                    }
                    else
                    {
                        prod[x, 186] = "";   //<indEscala>          

                        prod[x, 187] = "";// "09245135000191";   //<CNPJFab> CNPJ do Fabricante da Mercadoria, obrigatório para produto em escala NÃO relevante.
                        prod[x, 188] = "";//0123456789";        //<cBenef> Código de Benefício Fiscal utilizado pela UF, aplicado ao item.
                    }

                    string EXTIPI = LIS_PRODUTONFEColl[x].EXTIPI.Trim();
                    prod[x, 4] = EXTIPI;                     				            		//<EXTIPI> //Antes da vr 2.00, esta posicao era o GENERO.
                    prod[x, 5] = LIS_PRODUTONFEColl[x].CODCFOP.Replace(".", "");       							//<CFOP>
                    prod[x, 6] = LIS_PRODUTONFEColl[x].NOMEUNIDADE.TrimEnd();                    							//<uCom>


                    string qCom = Convert.ToDouble(LIS_PRODUTONFEColl[x].QUANTIDADE).ToString("n" + NumCasasDecimas).Replace(".", "").Replace(",", ".");
                    prod[x, 7] = qCom;  //Alterado 02/04/2015

                    string vUnCom = Convert.ToDouble(LIS_PRODUTONFEColl[x].VALORUNITARIO).ToString("n" + NumCasasDecimas).Replace(".", "").Replace(",", ".");
                    prod[x, 8] = vUnCom;                                        								//<vUnCom>

                    string vProd = Convert.ToDouble(LIS_PRODUTONFEColl[x].VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 9] = vProd;                                            							    //<vProd>


                    prod[x, 10] = ProdutoNFe.CODBARRA;														//eantrib //<cEANTrib>
                    prod[x, 11] = LIS_PRODUTONFEColl[x].NOMEUNIDADE.TrimEnd();                							//<uTrib>

                    string qTrib = Convert.ToDouble(LIS_PRODUTONFEColl[x].QUANTIDADE).ToString("n" + NumCasasDecimas).Replace(".", "").Replace(",", ".");
                    prod[x, 12] = qTrib;                                        								//<qTrib>
                   

                    string vUnTrib = Convert.ToDouble(LIS_PRODUTONFEColl[x].VALORUNITARIO).ToString("n" + NumCasasDecimas).Replace(".", "").Replace(",", ".");
                    prod[x, 13] = vUnTrib;                                      								//<vUnTrib>


                    prod[x, 14] = Convert.ToDouble(LIS_PRODUTONFEColl[x].VLFRETE).ToString("n2").Replace(".", "").Replace(",", ".");
                    
                    
                    decimal ValorSeguro = Convert.ToDecimal(Entity.VALORSEGURO) / (LIS_PRODUTONFEColl.Count);
                    prod[x, 15] = Convert.ToDouble(ValorSeguro).ToString("n2").Replace(".", "").Replace(",", ".");  //<vSeg>

                    prod[x, 16] = Convert.ToDouble(LIS_PRODUTONFEColl[x].DESCONTOPRODUTO).ToString("n2").Replace(".", "").Replace(",", ".");//<vDesc>   

                    /* tag ISSQN */
                    prod[x, 39] = ""; 				                    //ISSQN <vBC>
                    prod[x, 40] = "";                					//ISSQN <vAliq>
                    prod[x, 41] = "";                					//ISSQN <vISSQN>
                    prod[x, 42] = Convert.ToString(CONFISISTEMAGeraNFeCompty.CODMUNIBGE); //ISSQN <cMunFG>
                    prod[x, 43] = "";            						//ISSQN <cListServ>				
                    prod[x, 70] = "";

                    //NF-e 3.10
                    prod[x, 119] = "";            						//ISSQN <vDeducao>
                    prod[x, 120] = "";            						//ISSQN <vOutro>
                    prod[x, 121] = "";            						//ISSQN <vDescIncond>
                    prod[x, 122] = "";            						//ISSQN <vDescCond>
                    prod[x, 123] = "";            						//ISSQN <vISSRet>
                    prod[x, 124] = "";            						//ISSQN <indISS>
                    prod[x, 125] = "";            						//ISSQN <cMun>
                    prod[x, 126] = "";            						//ISSQN <cPais>
                    prod[x, 127] = "";            						//ISSQN <nProcesso>
                    prod[x, 128] = "";            						//ISSQN <indIncentivo>
                    prod[x, 134] = "";                                  //ISSQN <cServico>

                    //ICMtxtValorTotalProdutosS
                    //Busca a tributação
                    CSTEntity CSTNFeTy = new CSTEntity();
                    CSTProvider CSTP = new CSTProvider();
                    CSTNFeTy = CSTP.Read(Convert.ToInt32(LIS_PRODUTONFEColl[x].IDCST));

                    //Busca Origem da Mercadoria
                    ORIGEMMERCADORIAEntity ORIGEMMERCADORIANFeTy = new ORIGEMMERCADORIAEntity();
                    ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                    ORIGEMMERCADORIANFeTy = ORIGEMMERCADORIAP.Read(Convert.ToInt32(CSTNFeTy.IDORIGEM));
                    //Origem da mercadoria:
                    //0 – Nacional; 
                    //1 – Estrangeira – Importação direta; 
                    //2 – Estrangeira – Adquirida no mercado interno

                    /* tag ICMS */
                    prod[x, 17] = ORIGEMMERCADORIANFeTy.CODIGO;           //<orig>
                    prod[x, 18] = CSTNFeTy.CODIGO;//         			//<CST>
                    //Modalidade de determinação da BC do ICMS 
                    //0 - Margem Valor Agregado (%); 
                    //1 - Pauta (Valor); 
                    //2 - Preço Tabelado Máx. (valor); 
                    //3 - valor da operação.
                    prod[x, 19] = "3";									//<modBC>
                    string ICMS_vBC = Convert.ToDecimal(LIS_PRODUTONFEColl[x].BASEICMS).ToString("n2").Replace(".", "").Replace(",", ".");
                    string ICMS_pICMS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].ALICMS).ToString("n2").Replace(".", "").Replace(",", ".");

                    if (Convert.ToDecimal(ICMS_pICMS) > 0)
                        prod[x, 20] = ICMS_vBC;								//<vBC>
                    else
                        prod[x, 20] = "0";

                    prod[x, 21] = ICMS_pICMS;							 //<pICMS>
                    string ICMS_vICMS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORICMS).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 22] = ICMS_vICMS;								//<vICMS>

                    //=============================================================================
                    //<modBCST>	Modalidade de determinação da BC do ICMS ST 
                    // 0 – Preço tabelado ou máximo sugerido; 
                    // 1 - Lista Negativa (valor); 
                    // 2 - Lista Positiva (valor); 
                    // 3 - Lista Neutra (valor); 
                    // 4 - Margem Valor Agregado (%); 
                    // 5 - Pauta (valor); 
                    prod[x, 46] = "0";	//<modBCST>		
                    //=============================================================================

                    prod[x, 47] = "";									//<pMVAST>	Percentual da margem de valor Adicionado do ICMS ST 		
                    prod[x, 48] = "";									//<pRedBCST> //Percentual da Redução de BC do ICMS ST 		

                    prod[x, 49] = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLBASEST).ToString("n2").Replace(".", "").Replace(",", ".");		//<vBCSTRet>	//foi modificado, antes vBCST; v6.01 = vBCSTRet

                    decimal AlqICMSST = 0;
                    if (LIS_PRODUTONFEColl[x].VLBASEST > 0)
                        AlqICMSST = ((Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLICMSST) / Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLBASEST)) * 100);
                    
                    prod[x, 50] = Convert.ToDecimal(AlqICMSST).ToString("n2").Replace(".", "").Replace(",", ".");           //<vICMSSTRet>		//foi modificado, antes vICMSST; v6.01 = vICMSSTRet


                    prod[x, 190] = "0";                                 //<pFCP> Percentual do Fundo de Combate à Pobreza (FCP) 
                    prod[x, 191] = "0.00";                              //<vFCP> Valor do Fundo de Combate à Pobreza (FCP)

                    prod[x, 195] = "";                                  //<vBCFCP> Valor da Base de Cálculo do FCP
                    prod[x, 192] = "";                                  //<vBCFCPST> Valor da Base de Cálculo do FCP retido por Substituição Tributária
                    prod[x, 193] = "";                                  //<pFCPST> Percentual do FCP retido por Substituição Tributária
                    prod[x, 194] = "";                                  //<vFCPST> Valor do FCP retido por Substituição Tributária
                    prod[x, 196] = "";                                  //<pFCPSTRet> Percentual do FCP retido anteriormente por Substituição Tributária
                    prod[x, 197] = "";                                  //<vFCPSTRet> Valor do FCP retido anteriormente por Substituição Tributária
                    prod[x, 199] = "";                                  //<vBCFCPSTRet> Valor da Base de Cálculo do FCP retido anteriormente

                    // NF - e v4.0
                    //  if (CSTNFeTy.CODIGO != "500")
                    prod[x, 198] = "";                                  //<pST> Alíquota suportada pelo Consumidor Final


                    prod[x, 51] = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLICMSST).ToString("n2").Replace(".", "").Replace(",", ".");// TxtValorICMSSubst.Text.Replace(".", "").Replace(",", ".");//									//<vICMSST>							

                    prod[x, 52] = Convert.ToDecimal(LIS_PRODUTONFEColl[x].REDICMS).ToString("n2").Replace(".", "").Replace(",", ".");  			//<pRedBC>	
		
                     prod[x, 142] = "";                                  //<vBCSTDest>
                     prod[x, 143] = "";                                  //<vICMSSTDest>

                    prod[x, 80] = txtAliqCredICMS.Text.Replace(".", "").Replace(",", ".");								//<pCredSN>							
                    prod[x, 81] = txtValorCredICMS.Text.Replace(".", "").Replace(",", ".");	           					//<vCredICMSSN>		

                    prod[x, 85] = "";             					    //<motDesICMS> Informar o motivo da desoneração: 0 a 9, ver tabela no manual do contribuinte NF-e.

                    prod[x, 86] = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLOUTROS).ToString("n2").Replace(".", "").Replace(",", ".");  	//<vOutro> 


                    /* tag IPI */
                    if (ProdutoNFe.CSTPIS != string.Empty)
                        prod[x, 23] = ProdutoNFe.CSTIPI;//IPI <CST>
                    else
                        // prod[x, 23] = "99";				//IPI <CST>
                        prod[x, 23] = "";				//IPI <CST>



                    /* obs: Informar os campos INDEX 24 e 25 caso o cálculo do IPI seja por alíquota ou os campos INDEX
                       78 e 79 caso o cálculo do IPI seja valor por unidade. */
                    prod[x, 78] = string.Empty;                             //IPI qUnid v6.03
                    prod[x, 79] = string.Empty;                             //IPI vUnid v6.03

                    // string vBC_IPI = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", ".");
                    // prod[x, 24] = vBC_IPI;                                           		//IPI <vBC>
                    //  prod[x, 24] = "0.00";                                           		//IPI <vBC>
                    prod[x, 24] = "";                                                   //IPI <vBC>

                    // string pIPI_IPI = Convert.ToDecimal(LIS_PRODUTONFEColl[x].ALIPI).ToString("n2").Replace(".", "").Replace(",", ".");
                    //prod[x, 25] = pIPI_IPI;         //IPI <pIPI>
                    prod[x, 25] = "";         //IPI <pIPI>
                                                    /* fim obs */

                    if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 5)//versao 3.10
                    {
                        prod[x, 87] = "0";                                  //IPI <clEnq> Classe de enquadramento do IPI para Cigarros e Bebidas
                        prod[x, 88] = "00000000000000";                     //IPI <CNPJProd> CNPJ do produtor da mercadoria, quando diferente do emitente. Somente para os casos de exportação direta ou indireta.
                        prod[x, 89] = "0";                                  //IPI <cSelo> Código do selo de controle IPI
                        prod[x, 90] = "0";                                  //IPI <qSelo> Quantidade de selo de controle
                        prod[x, 91] = "999";                                //IPI <cEnq> Código de Enquadramento Legal do IPI
                    }
                    else
                    {
                        prod[x, 87] = "0";                                  //IPI <clEnq> Classe de enquadramento do IPI para Cigarros e Bebidas
                        prod[x, 88] = "00000000000000";                     //IPI <CNPJProd> CNPJ do produtor da mercadoria, quando diferente do emitente. Somente para os casos de exportação direta ou indireta.
                        prod[x, 89] = "0";                                  //IPI <cSelo> Código do selo de controle IPI
                        prod[x, 90] = "0";                                  //IPI <qSelo> Quantidade de selo de controle
                        prod[x, 91] = "999";                                //IPI <cEnq> Código de Enquadramento Legal do IPI
                    }


                    // if (ProdutoNFe.ENQUADRALEGALIPI > 0)
                    //     prod[x, 91] = ProdutoNFe.ENQUADRALEGALIPI.ToString();  //IPI <cEnq>
                    // else
                    //    prod[x, 91] = "999";								   //IPI <cEnq>                  

                    // string vIPI_IPI = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORIPI).ToString("n2").Replace(".", "").Replace(",", ".");
                    // prod[x, 26] = vIPI_IPI;			//IPI <vIPI>
                    prod[x, 26] = "";			//IPI <vIPI>

                    /* tag II */
                    prod[x, 27] = "0"; //II <vBC>
                    prod[x, 28] = "0";               						 //II <vDespAdu>
                    prod[x, 29] = "0";               						 //II <vII>
                    prod[x, 30] = "0";               						 //II <vIOF>


                    /* tag PIS */
                    //Alterado 15/01/2014 - Por Rafael Miranda
                    //=============================================================
                    //prod[x, 31] = LIS_PRODUTONFEColl[x].CSTPISCOFINS;	      //<CST>
                    if (ProdutoNFe.CSTPIS != string.Empty)
                        prod[x, 31] = ProdutoNFe.CSTPIS;// "00";				<CST>
                    else
                        prod[x, 31] = "99";             //IPI <CST>
                   //=============================================================

                    // string vBC_PIS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", ".");
                    //  prod[x, 32] = vBC_PIS;                                      			  //<vBC>
                    prod[x, 32] = "0.00";                                      			  //<vBC>

                    string pPIS_PIS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].ALIQPIS).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 33] = pPIS_PIS;    		  //<pPIS>
                    string vPis_PIS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORPIS).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 34] = vPis_PIS;    		  //<vPis>
                    prod[x, 45] = "";               //<vAliqProd>     { campo novo }

                    /* tag COFINS */
                    prod[x, 35] = LIS_PRODUTONFEColl[x].CSTPISCOFINS;       //<CST>

                    //  string vBC_COFINS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORTOTAL).ToString("n2").Replace(".", "").Replace(",", ".");
                    // prod[x, 36] = vBC_COFINS;			//<vBC>
                    prod[x, 36] = "0.00";			//<vBC>

                    string pCOFINS_COFINS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].ALIQCOFINS).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 37] = pCOFINS_COFINS;  		//<pCOFINS>

                    string vCOFINS_COFINS = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VALORCOFINS).ToString("n2").Replace(".", "").Replace(",", ".");
                    prod[x, 38] = vCOFINS_COFINS;		//<vCOFINS>
                    prod[x, 44] = "";									//<vAliqProd>     { campo novo }

                    /*tag PISST*/
                    prod[x, 54] = "";								//vBC
                    prod[x, 55] = "";								//pPIS
                    prod[x, 56] = "";								//vPIS


                    /* tag COFINSST */
                    prod[x, 57] = "";								//vBC
                    prod[x, 58] = "";								//pCOFINS
                    prod[x, 59] = "";								//vCOFINS

                    /* Tag da Declaração de Importação | DI */
                    prod[x, 60] = "";                             //nDI
                    prod[x, 61] = "";                             //dDI
                    prod[x, 62] = "";                             //xLocDesemb
                    prod[x, 63] = "";                             //UFDesemb
                    prod[x, 64] = "";                             //dDesemb
                    prod[x, 65] = "";                             //cExportador
                    prod[x, 66] = "";                             //adi: nAdicao
                    prod[x, 67] = "";                             //adi: nSeqAdic
                    prod[x, 68] = "";                             //adi: cFabricante
                    prod[x, 69] = "";                             //adi: vDescDI

                    prod[x, 53] = LIS_PRODUTONFEColl[x].DESCPRODUTO2.TrimEnd().TrimStart();       						//infAdProd


                    /* Grupo do detalhamento de Medicamentos e de matériasprimas farmacêuticas */
                    prod[x, 71] = ""; //"12345671234564654";            //nLote
                    prod[x, 72] = ""; //"123.123";                      //qLote
                    prod[x, 73] = ""; //"2010-01-01";                   //dFab
                    prod[x, 74] = ""; //"2011-01-01";                   //dVal
                    prod[x, 75] = ""; //"222.123";                      //vPMC

                    prod[x, 76] = "1";          //indTot v6.03 --> Indica se valor do Item (vProd) entra no valor total da NF-e (vProd)

                    //NF-e 3.10
                    prod[x, 119] = "";            						//ISSQN <vDeducao>
                    prod[x, 120] = "";            						//ISSQN <vOutro>
                    prod[x, 121] = "";            						//ISSQN <vDescIncond>
                    prod[x, 122] = "";            						//ISSQN <vDescCond>
                    prod[x, 123] = "";            						//ISSQN <vISSRet>
                    prod[x, 124] = "";            						//ISSQN <indISS>
                    prod[x, 125] = "";            						//ISSQN <cMun>
                    prod[x, 126] = "";            						//ISSQN <cPais>
                    prod[x, 127] = "";            						//ISSQN <nProcesso>
                    prod[x, 128] = "";            						//ISSQN <indIncentivo>
                    prod[x, 134] = "";                                  //ISSQN <cServico>

                    prod[x, 83] = txtNumPedido.Text;                             //xPed
                    prod[x, 84] = numitem.ToString();                             //nItemPed
                    numitem++;

                    /* Grupo do detalhamento de Combustíveis */
                    prod[x, 92] = "";                 // cProdANP (ocorrência 1-1) --> Nota: se não for informada essa posição, não será gerado o grupo <comb>
                    prod[x, 93] = "";                          // CODIF    (ocorrência 0-1)
                    prod[x, 94] = "";                          // qTemp    (ocorrência 0-1)
                    prod[x, 95] = "";                        // UFCons   (ocorrência 1-1)

                    //Lei da Transparência
                    prod[x, 96] = Convert.ToDecimal(LIS_PRODUTONFEColl[x].VLTRIBUTOAPROX).ToString("n2").Replace(".", "").Replace(",", ".");  //<vTotTrib>

                    prod[x, 105] = ""; //"B01F70AF-10BF-4B1F-848C-65FF57F616FE"; // nFCI (ocorrência 0-1) Nota:2013/006 Número de controle da FCI - Ficha de Conteúdo de Importação
                    prod[x, 106] = ""; //"0.00";                         //vICMSDeson (ocorrência 1-1) Nota:2013/005 Informar apenas nos motivos de desoneração documentados abaixo.       
                    prod[x, 107] = "7";                              //<tpViaTransp>
                    prod[x, 108] = "1";                              //<tpIntermedio>


                    //<detExport> - Tag destinada a Exportação
                    prod[x, 110] = "";                              //<nDraw> - Tag destinada a Exportação
                    prod[x, 111] = "";                              //<chNFe> - Tag destinada a Exportação
                    prod[x, 112] = "";                              //<nRE> - Tag destinada a Exportação
                    prod[x, 113] = "";                              //<qExport> - Tag destinada a Exportação
                    prod[x, 114] = "";                              //<nRECOPI> - Tag para operações com Papel Imune.

                    /* Grupo do detalhamento de Armas */
                    prod[x, 115] = "";                              //<tpArma>
                    prod[x, 116] = "";                              //<nSerie>
                    prod[x, 117] = "";                              //<nCano>
                    prod[x, 118] = "";                              //<desc>

                    //Gruno para detalhamento de Devolução. <finNFe> igual a 4.
                    prod[x, 129] = "";                              //<pDevol>
                    prod[x, 130] = "";                              //<IPI>
                    prod[x, 131] = "";                              //<vIPIDevol>
                    prod[x, 132] = "";                              //<pMixGN>
                    prod[x, 133] = "";                              //<vAFRMM>

                    prod[x, 135] = "";                              //<cnpj>
                    prod[x, 136] = "";                              //<ufterceiro>

                    //ICMS 51
                    prod[x, 137] = "";                              //<vICMSOp>
                    prod[x, 138] = "";                                 //<pDif>
                    prod[x, 139] = "";                              //<vICMSDif>


                   //  grupo 'ICMSUFDest' em uma NF-e com emitente e destinatário diferente
                    if (EMPRESATy.UF != LIS_MUNICIPIOSColl[0].UF)
                    {
                        if (ClienteTyDest.IE.Trim() != string.Empty && ClienteTyDest.IE.Trim().ToUpper() != "ISENTO")
                        {
                            //ICMSUFDest
                            // Para gerar o ICMSUFDest todos os indices do vetor devem conter dados
                           // prod[x, 147] = "36.30";       //<vBCUFDest>
                           // prod[x, 148] = "0.00";        //<pFCPUFDest>
                           // prod[x, 149] = "18.00";       //<pICMSUFDest>
                           // prod[x, 150] = "12.00";       //<pICMSInter>
                           // prod[x, 151] = "40.00";       //<pICMSInterPart>
                           // prod[x, 152] = "0.00";        //<vFCPUFDest>
                           // prod[x, 153] = "1.31";        //<vICMSUFDest>
                           // prod[x, 154] = "0.87";        //<vICMSUFRemet>
                        }
                    }

                    if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 6)
                    {
                        //Para gerar o ICMSUFDest todos os indices do vetor devem conter dados
                        prod[x, 147] = "0.00";       //<vBCUFDest> Valor da BC do ICMS na UF de destino
                        prod[x, 148] = "0.00";         //<pFCPUFDest> Percentual do ICMS relativo ao Fundo de Combate à Pobreza (FCP) na UF de destino
                        prod[x, 149] = "0.00";        //<pICMSUFDest> Alíquota interna da UF de destino
                        prod[x, 150] = "";        //<pICMSInter> Alíquota interestadual das UF envolvidas
                        prod[x, 151] = "0.00";        //<pICMSInterPart> Percentual provisório de partilha do ICMS Interestadual
                        prod[x, 152] = "0.00";         //<vFCPUFDest> Valor do ICMS relativo ao Fundo de Combate à Pobreza (FCP) da UF de destino
                        prod[x, 153] = "0.00";        //<vICMSUFDest> Valor do ICMS Interestadual para a UF de destino
                        prod[x, 154] = "0.00";         //<vICMSUFRemet> Valor do ICMS Interestadual para a UF do remetente
                        prod[x, 179] = "0.00";         //<vBCFCPUFDest> Valor da BC FCP na UF de destino
                    }

                    /* ultimo index 76 */
                }//for fim do laço do produto

                /*<total> TAG de grupo de Valores Totais da NF-eI*/

                string vBC_ICMSTot = Convert.ToDecimal(Entity.BASECALCICMS + Convert.ToDecimal(TxtValorICMSSubst.Text)).ToString("n2").Replace(".", "").Replace(",", ".");

                string vICMS_ICMSTot = Convert.ToDecimal(Entity.VALORICMS).ToString("n2").Replace(".", "").Replace(",", ".");
                if (chkAlterar.Checked)
                {
                    vICMS_ICMSTot = Convert.ToDecimal(TxtValorICMS.Text).ToString("n2").Replace(".", "").Replace(",", ".");
                    vBC_ICMSTot = Convert.ToDecimal(TxtBaseCalcICMS.Text).ToString("n2").Replace(".", "").Replace(",", ".");
                }

                if (Convert.ToDecimal(vICMS_ICMSTot) > 0)
                    total[0] = vBC_ICMSTot;      //ICMSTot <vBC>
                else
                    total[0] = "0";

                total[1] = vICMS_ICMSTot;   //ICMSTot <vICMS>

                string vBCST_ICMSTot = txtBaseCalcICMSSubs.Text; // 
                total[2] = vBCST_ICMSTot.Replace(".", "").Replace(",", ".");           //ICMSTot '

                string vST_ICMSTot = TxtValorICMSSubst.Text; 
                total[3] = vST_ICMSTot.Replace(".", "").Replace(",", ".");;     //ICMSTot <vST>

                string vProd_ICMSTot = Convert.ToDecimal(Entity.TOTALPRODUTOS).ToString("n2").Replace(".", "").Replace(",", ".");
                total[4] = vProd_ICMSTot;   //ICMSTot <vProd>

                //Total Frete
                decimal vFrete_ICMSTot = 0;
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    vFrete_ICMSTot += Convert.ToDecimal(item.VLFRETE);
                }    
                total[5] = Convert.ToDecimal(vFrete_ICMSTot).ToString("n2").Replace(".", "").Replace(",", ".");          //ICMSTot <vFrete>

                string vSeg_ICMSTot = Convert.ToDecimal(Entity.VALORSEGURO).ToString("n2").Replace(".", "").Replace(",", ".");
                total[6] = vSeg_ICMSTot;          //ICMSTot <vSeg>

                string vDesc_ICMSTot = BuscaDescontoProduto().ToString("n2").Replace(".", "").Replace(",", ".");
                total[7] = vDesc_ICMSTot;   //ICMSTot <vDesc>

                total[8] = "0.00";          //ICMSTot <vII>

                string vIPI_ICMSTot = Convert.ToDecimal(Entity.TOTALIPI).ToString("n2").Replace(".", "").Replace(",", ".");
                total[9] = vIPI_ICMSTot;       //ICMSTot <vIPI>

                string vPIS_ICMSTot = Convert.ToDecimal(Entity.VALORPIS).ToString("n2").Replace(".", "").Replace(",", ".");
                total[10] = vPIS_ICMSTot;         //ICMSTot <vPIS>

                string vCOFINS_ICMSTot = Convert.ToDecimal(Entity.VALORCONFINS).ToString("n2").Replace(".", "").Replace(",", ".");
                total[11] = vCOFINS_ICMSTot;        //ICMSTot <vCOFINS>

                string vOutro_ICMSTot = Convert.ToDecimal(Entity.OUTRADESPES).ToString("n2").Replace(".", "").Replace(",", ".");
                total[12] = vOutro_ICMSTot;         //ICMSTot <vOutro>

                //ICMSTot <vNF> //Alterado Rafael 05/12/2014 - 10:13
                string vNF_ICMSTot = (Convert.ToDecimal(txtTotalNota.Text) + Convert.ToDecimal(TxtValorICMSSubst.Text)).ToString("n2");
                vNF_ICMSTot = vNF_ICMSTot.Replace(".", "").Replace(",", ".");//Convert.ToDecimal(Entity.TOTALNOTA).ToString("n2").Replace(".", "").Replace(",", ".");//ICMSTot <vNF> 
                total[13] = vNF_ICMSTot;        

                string vNF_vServ = Convert.ToDecimal(Entity.VALORTOTALSERVICO).ToString("n2").Replace(".", "").Replace(",", ".");
                total[14] = Convert.ToDecimal(vNF_vServ) > 0 ? vNF_vServ : string.Empty;      //ISSQNtot <vServ>

                string vBC_vServ = Convert.ToDecimal(Entity.VALORTOTALSERVICO).ToString("n2").Replace(".", "").Replace(",", ".");
                total[15] = vBC_vServ;      //ISSQNtot <vBC>

                string vISS_vServ = Convert.ToDecimal(Entity.VALORISSQN).ToString("n2").Replace(".", "").Replace(",", ".");
                total[16] = vISS_vServ;      //ISSQNtot <vISS>

                string vPIS_vServ = Convert.ToDecimal(Entity.VALORPIS).ToString("n2").Replace(".", "").Replace(",", ".");
                total[17] = vPIS_vServ;      //ISSQNtot <vPIS>

                string vCOFINS_vServ = Convert.ToDecimal(Entity.VALORCONFINS).ToString("n2").Replace(".", "").Replace(",", ".");
                total[18] = vCOFINS_vServ;      //ISSQNtot <vCOFINS>

                /* retTrib: Grupo de Retenções de Tributos */
                total[19] = "";                 //vRetPIS
                total[20] = "";                 //vRetCOFINS
                total[21] = "";                 //vRetCSLL
                total[22] = "";                 //vBCIRRF
                total[23] = "";                 //vIRRF
                total[24] = "";                 //vBCRetPrev
                total[25] = "";                 //vRetPrev

                total[27] = "0.00";                 //ICMSTot <vICMSDeson>

                //Lei da Transparência
                string vTotTrib = TotalTributoProdutos().ToString("n2");
                vTotTrib = vTotTrib.Replace(".", "").Replace(",", ".");
                total[26] = vTotTrib;            //vTotTrib  ocor. 0-1
                
                total[28] = "";      //ISSQNtot <dCompet>
                total[29] = "";      //ISSQNtot <vDeducao>
                total[30] = "";      //ISSQNtot <vOutro>
                total[31] = "";      //ISSQNtot <vDescIncond>
                total[32] = "";      //ISSQNtot <vDescCond>
                total[33] = "";      //ISSQNtot <vISSRet>
                total[34] = "";      //ISSQNtot <cRegTrib>

                if (EMPRESATy.UF != LIS_MUNICIPIOSColl[0].UF)
                {
                    if (ClienteTyDest.IE.Trim() != string.Empty && ClienteTyDest.IE.Trim().ToUpper() != "ISENTO")
                    {
                        // total[35] = "1.31";         //<vICMSUFDest>
                        // total[36] = "0.87";         //<vICMSUFRemet>
                        // total[37] = "0.00";         //<vFCPUFDest> vFCPUFDest
                       // total[38] = "0.00";         //<vFCP> Valor Total do FCP (Fundo de Combate à Pobreza)
                    }
                }

                //Versao 4.0
                if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 6)
                {
                     total[35] = "0.00";         //<vICMSUFDest>
                     total[36] = "0.00";         //<vICMSUFRemet>
                     total[37] = "0.00";         //<vFCPUFDest> vFCPUFDest
                     total[38] = "0.00";         //<vFCP> Valor Total do FCP (Fundo de Combate à Pobreza)
                     total[39] = "0.00";         //<vFCPST> Valor Total do FCP (Fundo de Combate à Pobreza) retido por substituição tributária
                     total[40] = "0.00";         //<vFCPSTRet> Valor Total do FCP retido anteriormente por Substituição Tributária
                     total[41] = "0.00";         //<vIPIDevol> Valor Total do IPI devolvido
                }

                /*<transp> Informações do Transporte da NF-e*/
                //informar a modalidade do frete:
                //0-por conta do emitente;
                //1-por conta do destinatário
                string modFrete = Entity.FRETE == 1 ? "1" : "0";
                transp[0] = modFrete;						//<modFrete>
                if (Convert.ToInt32(cbTransportadora.SelectedValue) > 0)
                {
                    TRANSPORTADORAEntity TRANSPORTADORANFeTy = new TRANSPORTADORAEntity();
                    TRANSPORTADORANFeTy = TRANSPORTADORAP.Read(Convert.ToInt32(cbTransportadora.SelectedValue));

                    string CNPJTranp = Util.RetiraLetras(TRANSPORTADORANFeTy.CNPJ);
                    transp[1] = CNPJTranp;             	        //<CNPJ> ou <CPF>
                    transp[2] = TRANSPORTADORANFeTy.NOME;			//<xNome>

                    if(TRANSPORTADORANFeTy.IE.ToUpper() != "ISENTO")
                        transp[3] = Util.RetiraLetras(TRANSPORTADORANFeTy.IE);
                    else
                        transp[3] = TRANSPORTADORANFeTy.IE;				//<IE>

                    transp[4] = TRANSPORTADORANFeTy.ENDERECO;  	//<xEnder>
                    transp[5] = TRANSPORTADORANFeTy.CIDADE; // Util.CodigodeUFIBGE(TRANSPORTADORANFeTy.UF).ToString();			//<xMun>
                    transp[6] = TRANSPORTADORANFeTy.UF;				//<UF>
                }
                else
                {
                    transp[1] = "";                          	//<CNPJ> ou <CPF>
                    transp[2] = "";		                      	//<xNome>
                    transp[3] = "";			                	//<IE>
                    transp[4] = "";  	//<xEnder>
                    transp[5] = "";				//<xMun>
                    transp[6] = "";
                }

                transp[7] = Entity.PLACA;						//<placa>
                transp[8] = Entity.UFTRANSPORTE;    			//<UF>



                /* Grupo Volumes */
               // string qVol_Volumes = (Convert.ToDecimal(Entity.QUANT) * 1).ToString();
               // transp[9] = Convert.ToDecimal(qVol_Volumes) > 0 ? qVol_Volumes : string.Empty;					//<qVol>
                transp[9] = txtQuant.Text.Replace(".", "").Replace(",", ".");
                transp[10] = Entity.ESPECIE;				//<esp>
                transp[11] = Entity.MARCANFE;   			//<marca>
                transp[12] = Entity.NUMEROS;				//<nVol>
                string pesoL_Volumes = Convert.ToDecimal(Entity.PESOLIQUIDO).ToString("n2").Replace(".", "").Replace(",", ".");
                transp[13] = pesoL_Volumes;						//<pesoL>
                string pesoB_Volumes = Convert.ToDecimal(Entity.PESOBRUTO).ToString("n2").Replace(".", "").Replace(",", ".");
                transp[14] = pesoB_Volumes;						//<pesoB>

                transp[15] = Entity.CODANTT;						//RNTC/ANTT

                /* obs: Separe por ; para informar diversos volumes */

                /* retTransp: Grupo de Retenção do ICMS do transporte */
                transp[16] = "";                            //vServ
                transp[17] = "";                            //vBCRet
                transp[18] = "";                            //pICMSRet
                transp[19] = "";                            //vICMSRet
                transp[20] = "";                            //CFOP
                transp[21] = "";                           //cMunFG
            
                transp[25] = "";                            //balsa
                transp[26] = "";                            //vagao        
                transp[27] = "";                    		//<nLacre>  




                /*<cobr> Dados da Cobrança*/
                cobr[0] = Entity.NOTAFISCALE;         	//fat <nFat> //informar o número da fatura
                string vOrig_cobr = Convert.ToDecimal(Entity.TOTALNOTA).ToString("n2").Replace(".", "").Replace(",", ".");
                cobr[1] = vOrig_cobr;        	    //fat <vOrig>//informar o valor originário da fatura
                cobr[2] = vOrig_cobr;            	//fat <vLiq>//informar o valor Liquido da fatura


                string nDup = string.Empty;
                string dVenc = string.Empty;
                string vDup = string.Empty;

                if (LIS_DUPLICATARECEBERColl.Count > 0)
                {
                    foreach (LIS_DUPLICATARECEBEREntity itemRece in LIS_DUPLICATARECEBERColl)
                    {
                        nDup += itemRece.NUMERO + ";";
                        dVenc += Convert.ToDateTime(itemRece.DATAVECTO).ToString("yyyy-MM-dd") + ";";
                        string vDupl = Convert.ToDecimal(itemRece.VALORDUPLICATA).ToString("n2").Replace(".", "").Replace(",", ".");
                        vDup += vDupl + ";";
                    }
                    /* neste ex, existem 2 parcelas */
                    cobr[3] = nDup.Remove(nDup.Length - 1);									//dup <nDup>
                    cobr[4] = dVenc.Remove(dVenc.Length - 1); ;    				//dup <dVenc>
                    cobr[5] = vDup.Remove(vDup.Length - 1);						//dup <vDup>

                }
                else
                {
                    /*<cobr> Dados da Cobrança*/
                    cobr[0] = "";                   	//fat <nFat>
                    cobr[1] = "";                   	//fat <vOrig>
                    cobr[2] = "";            		//fat <vLiq>
                    /* neste ex, existem 2 parcelas */
                    cobr[3] = "";									//dup <nDup>
                    cobr[4] = "";    				//dup <dVenc>
                    cobr[5] = "";
                }

                /*<pag> Grupo Informações de Pagamento (Opcionar por UF)*/
                /*01=Dinheiro 
                 *02=Cheque 
                 *03=Cartão de Crédito 
                 *04=Cartão de Débito 
                 *05=Crédito Loja 
                 *10=Vale Alimentação 
                 *11=Vale Refeição 
                 *12=Vale Presente 
                 *13=Vale Combustível 
                 *14=Duplicata Mercantil 
                 *90= Sem pagamento 
                 *99=Outros */

                if (Convert.ToInt32(CONFISISTEMAGeraNFeCompty.IDVERSAOXMLNFE) == 6)
                {
                    pag[0] = "99";                          // <tPag> Forma de Pagamento
                    if(LIS_DUPLICATARECEBERColl.Count > 0)
                        pag[0] = "14";                          // <tPag> *14=Duplicata Mercantil 

                    if(chkDevolucao.Checked)
                        pag[0] ="90" ;//90= Sem pagamento    

                    if (chkAjuste.Checked)
                        pag[0] = "90";//90= Sem pagamento  

                    vNF_ICMSTot = (Convert.ToDecimal(txtTotalNota.Text) + Convert.ToDecimal(TxtValorICMSSubst.Text)).ToString("n2");// <vPag> Valor do Pagamento
                    vNF_ICMSTot = vNF_ICMSTot.Replace(".", "").Replace(",", ".");//Convert.ToDecimal(Entity.TOTALNOTA).ToString("n2").Replace(".", "").Replace(",", ".");//ICMSTot <vNF> 
                    pag[1] = vNF_ICMSTot;


                    /* 1=Pagamento integrado com o sistema de automação da empresa (Ex.: equipamento TEF, Comércio Eletrônico);
                     * 2= Pagamento não integrado com o sistema de automação da empresa (Ex.: equipamento POS); */
                    pag[2] = "";                    // <tpIntegra> Tipo de Integração do processo de pagamento com o sistema de automação da empresa

                    pag[3] = "";                    // <CNPJ> CNPJ da Credenciadora do Cartão
                    pag[4] = "";                    // <tBand> Bandeira da Operadora do Cartão
                    pag[5] = "";                    // <cAut> Numero de Autorização da Operação 
                    pag[6] = "";                    // <vTroco> Valor do Troco
                }
                else
                {
                    pag[0] = "";                         // <tPag> Forma de Pagamento
                   
                    pag[1] = "";                      // <vPag> Valor do Pagamento


                    /* 1=Pagamento integrado com o sistema de automação da empresa (Ex.: equipamento TEF, Comércio Eletrônico);
                     * 2= Pagamento não integrado com o sistema de automação da empresa (Ex.: equipamento POS); */
                    pag[2] = "";                    // <tpIntegra> Tipo de Integração do processo de pagamento com o sistema de automação da empresa

                    pag[3] = "";                    // <CNPJ> CNPJ da Credenciadora do Cartão
                    pag[4] = "";                    // <tBand> Bandeira da Operadora do Cartão
                    pag[5] = "";                    // <cAut> Numero de Autorização da Operação 
                    pag[6] = "";                    // <vTroco> Valor do Troco
                }

                /*<infAdic> Informações Adicionais da NF-e*/
                infAdic[0] = txtinfAdFisco.Text;  //infAdFisco
                
                infAdic[1] = Entity.INFOCOMPLEM.Replace("\n", "").Replace("\r", ""); 					 //infCpl
                
                /* obsCont - Grupo do campo de uso livre do contribuinte */
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    infAdic[2] = cbFuncionario.Text;      //Vendedor
                else
                    infAdic[2] = "";      //Vendedor

                infAdic[3] = string.Empty;       //Pedido
                infAdic[4] = string.Empty;      //Outros

                infAdic[5] = "";         //ProcRef <nProc>
                infAdic[6] = "9";         //ProcRef <indProc>  0=SEFAZ, 1=Justiça Federal, 2=Justiça Estadual, 3=Secex/RFB, 9=Outros
                infAdic[8] = "";
                infAdic[9] = "";
                infAdic[10] = "";
                infAdic[11] = "";
                infAdic[12] = "";
                infAdic[13] = "";
                infAdic[14] = "";
                infAdic[15] = "";
                infAdic[16] = "";

                // Ambiente - 1 Produção - 2 Homologação
                if (CONFISISTEMAP.Read(1).FLAGAMBIENTENFE.TrimEnd() == "1")
                {
                    //Remove caracteres especiais
                   // ide = Util.RemoveSpecialCharacters(ide);
                   // emit = Util.RemoveSpecialCharacters(emit);
                   // dest = Util.RemoveSpecialCharacters(dest);
                   // transp = Util.RemoveSpecialCharacters(transp);
                   // infAdic = Util.RemoveSpecialCharacters(infAdic);
                }

                /* chamar função para gerar a nf-e*/
                nfec.nfecsharp nfe = new nfec.nfecsharp();
                string ret = nfe.GeraNFe(ide, emit, dest, prod, total, transp, cobr, pag, infAdic, autXML, assinar);

                txtChaveAcesso.Text = ret;
                NOTAFISCALEP.Save(Entity);

                if (!ExibirMensagem)
                    MessageBox.Show("NFe Gerada sucesso.");
                else
                    Util.ExibirMSg("NFe Gerada sucesso.", "Blue");
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possível gerar XML da NFe");
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }

        }


        private void ExibirMsgNfeStatus(string MensagemStatus, Color Cor)
        {
            try
            {
                lblStatusEnvioNFe.Visible = true;
                lblStatusEnvioNFe.Text = "Status Nota: " + MensagemStatus;
                lblStatusEnvioNFe.ForeColor = Cor;
                Application.DoEvents();
                ExibirMensagem = false;
            }
            catch (Exception ex)
            {
                ExibirMensagem = false;
                 MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private decimal TotalTributoProdutos()
        {
            decimal resultDesconto = 0;

            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                resultDesconto += Convert.ToDecimal(item.VLTRIBUTOAPROX);
            }

            return resultDesconto;
        }


        private decimal BuscaDescontoProduto()
        {
            decimal resultDesconto = 0;

            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
            {
                resultDesconto += Convert.ToDecimal(item.DESCONTOPRODUTO);
            }

            return resultDesconto;
        }


        private void imprimirDanfeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void PrintDanfeComponente()
        {

            try
            {
                string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                Boolean NfeLoca = false;
               
                //Versao 3.10
                List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\assinado");
                
                foreach (string p in dirs)
                {
                    int LengthLine = p.Length;
                    int pos = p.IndexOf(NFe);
                    if (pos != -1)
                    {
                        NfeLoca = true;
                        string pathPDF = ConfigSistema1.Default.PathInstall + "\\nfe\\arquivos\\PDF";
                        nfec.nfecsharp nfe = new nfec.nfecsharp();
                        nfe.NFeDanfe(p, pathPDF, 3, false);

                        _FLAGENVIADA = "S";
                        _FLAGCANCELADA = "N";
                        NOTAFISCALEP.Save(Entity);
                    }
                }

                if (!NfeLoca)
                {
                    if (!ExibirMensagem)
                        MessageBox.Show("Não foi possível localizar arquivo processado!");
                    else
                        ExibirMsgNfeStatus("Não foi possível localizar arquivo processado!", Color.Red);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void PrintDanfeDireto()
        {
            //Consulta a situação da NFe antes de imprimir o danfe
            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            string CondSEFAZ = nfe.NfeConsulta(NFe);

            MessageBox.Show(CondSEFAZ);

            if (CondSEFAZ == "100" || CondSEFAZ == "Erro 100")
                ImprimirDANFE(true);
        }

        private void enviarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            //if (_IDNOTAFISCALE == -1)
            //{
            //    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            //    tabControl1.SelectTab(1);
            //    txtCriterioPesquisa.Focus();
            //}
            //else
            //{
            //    CreaterCursor Cr = new CreaterCursor();
            //    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            //    string NFe = _ARQUIVOLOTE;
            //    NfeRecepcao = false;
            //    List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\lotes");
            //    foreach (string p in dirs)
            //    {
            //        int LengthLine = p.Length;
            //        int pos = p.IndexOf(NFe);
            //        if (pos != -1)
            //        {
            //            nfec.nfecsharp nfe = new nfec.nfecsharp();
            //            _RECIBONFE = nfe.NfeRecepcao(p);


            //            if (_RECIBONFE.IndexOf("sucesso") != -1)
            //            {
            //                if (!ExibirMensagem)
            //                    MessageBox.Show(_RECIBONFE);
            //                else
            //                    ExibirMsgNfeStatus(_RECIBONFE, Color.Blue);

            //                int localRec = _RECIBONFE.IndexOf("#");//Pega somente o numero do recibo
            //                if (localRec != -1)
            //                {
            //                    NfeRecepcao = true;
            //                    int tamanhotexto = _RECIBONFE.Length - localRec - 1;
            //                    string RECIBOTEXTO = _RECIBONFE.Substring(localRec + 1, tamanhotexto);
            //                    _RECIBONFE = RECIBOTEXTO;

            //                    txtReciboRecp.Text = _RECIBONFE;
            //                    NOTAFISCALEP.Save(Entity);
            //                    break;
            //                }
            //                else
            //                {
            //                    NfeRecepcao = false;
            //                    _RECIBONFE = string.Empty;
            //                    NOTAFISCALEP.Save(Entity);
            //                }
            //            }


            //        }
            //    }

            //    this.Cursor = Cursors.Default;

            //    if (!NfeRecepcao)
            //    {
            //        if (!ExibirMensagem)
            //        {
            //            MessageBox.Show("Não foi possível realizar a Recepção do arquivo!");
            //            MessageBox.Show(_RECIBONFE);
            //        }
            //        else
            //        {
            //            ExibirMsgNfeStatus("Não foi possível realizar a Recepção do arquivo!", Color.Red);
            //            ExibirMsgNfeStatus(_RECIBONFE, Color.Red);
            //            MessageBox.Show(_RECIBONFE);
            //        }
            //    }
            //}


        }

        private void validarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {

                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    string NFe = txtChaveAcesso.Text + "-assinado"; ;
                    NfeValidar = false;
                    List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\assinado");
                    foreach (string p in dirs)
                    {
                        int LengthLine = p.Length;
                        int pos = p.IndexOf(NFe);
                        if (pos != -1)
                        {
                            nfec.nfecsharp nfe = new nfec.nfecsharp();
                            string schema = CONFISISTEMATy.SCHEMAXML;
                          
                            //string MSGValida = nfe.ValidarArquivoXML(p, schema, true);
                            if (nfe.ValidarArquivoXML(p, schema, true) == "OK")
                           // if (nfe.ValidarArquivoXML(p, schema, true))
                            {
                                NfeValidar = true;

                                if (!ExibirMensagem)
                                    MessageBox.Show("Validação concluída, nenhum erro identificado.");
                                else
                                    Util.ExibirMSg("Validação concluída, nenhum erro identificado.", "blue");
                               
                                _FLAGVALIDADA = "S";
                                NOTAFISCALEP.Save(Entity);
                            }
                            else
                            {
                                NfeValidar = false;
                                _FLAGVALIDADA = "N";
                                NOTAFISCALEP.Save(Entity);
                                MessageBox.Show("Problemas identificados na validação.");
                            }
                        }                     

                    }

                    this.Cursor = Cursors.Default;

                    if (!NfeValidar)
                    {
                        if (!ExibirMensagem)
                            MessageBox.Show("Não foi possível localizar arquivo assinado!");
                        else
                        {
                            MessageBox.Show("Não foi possível localizar arquivo assinado!");
                        }
                    }
                }

                catch (Exception)
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void criaLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                string NFe = txtChaveAcesso.Text + "-assinado";

                NfeCriarLote = false;
                string retStatus = string.Empty;
                List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\assinado");
                foreach (string p in dirs)
                {
                    int LengthLine = p.Length;
                    int pos = p.IndexOf(NFe);
                    if (pos != -1)
                    {
                        Random r = new Random(5);
                        _ARQUIVOLOTE = Convert.ToString((r.Next() + DateTime.Now.Millisecond));
                        nfec.nfecsharp nfe = new nfec.nfecsharp();
                        retStatus = nfe.GerarLote(p, Convert.ToInt32(_ARQUIVOLOTE));
                        NfeCriarLote = true;

                        if (!ExibirMensagem)
                        {
                            MessageBox.Show(retStatus);
                            lblLote.Text = "Lote: " + _ARQUIVOLOTE.ToString();
                        }
                        else
                        {
                            Util.ExibirMSg(retStatus, "Blue");
                            lblLote.Text = "Lote: " + _ARQUIVOLOTE.ToString();
                        }
                       
                        //salvando os caminho do arquivo em lote
                        NOTAFISCALEP.Save(Entity);                        
                    }

                    this.Cursor = Cursors.Default;	
                }

                if (!NfeCriarLote)
                {
                    this.Cursor = Cursors.Default;	
                    if (!ExibirMensagem)
                    {
                        MessageBox.Show("Não foi possível gerar arquivo de lote!");
                        MessageBox.Show("Erro técnico: " + retStatus);
                    }
                    else
                    {
                       // ExibirMsgNfeStatus("Não foi possível gerar arquivo de lote!", Color.Red);
                       // ExibirMsgNfeStatus("Erro técnico: " + retStatus,  Color.Red");
                        Util.ExibirMSg("Não foi possível gerar arquivo de lote!", "Red");
                        MessageBox.Show("Erro técnico: " + retStatus);
                    }
                }

                this.Cursor = Cursors.Default;	
            }

        }

        private void consultaSituaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //Exclui o arquivo PROCNfe
                 ExcluirArquivoXMProc();

                NfeSituacao = false;

                nfec.nfecsharp nfe = new nfec.nfecsharp();
                string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                string MensConsultSit = nfe.NfeConsulta(NFe);

                int pos = MensConsultSit.IndexOf("Autorizado o uso da NF-e");
                if (pos != -1)
                {
                    NfeSituacao = true;
                    _FLAGENVIADA = "S";
                    NOTAFISCALEP.Save(Entity);

                    if (!ExibirMensagem)
                        MessageBox.Show(MensConsultSit);
                    else
                        Util.ExibirMSg(MensConsultSit, "Blue");

                }
                else
                {
                    NfeSituacao = false;
                    _FLAGENVIADA = "N";
                    NOTAFISCALEP.Save(Entity);
                    MessageBox.Show(MensConsultSit);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void préVisualizarDANFESemValorFiscalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirDANFE(false);
            }
        }

        private void ArquivoDistribuicao()
        {
            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            nfe.DistribuicaoNFe(NFe);
        }

        private void GerarNFe()
        {
            //if (CONFISISTEMATy.FLAGNFESERVICOS == "S")
            //    GeraXMLNFeCompServico(true);
            //else
            GeraXMLNFeCompProduto(true);

            _FLAGARQUIVOXML = "S";
            _FLAGASSINATURA = "S";
            NOTAFISCALEP.Save(Entity);
        }

        private Boolean ValidaNFe()
        {
            Boolean result = false;
            nfec.nfecsharp nfe = new nfec.nfecsharp();

            string schema = CONFISISTEMATy.SCHEMAXML;
            string ArquivoValida = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\assinado\" + txtChaveAcesso.Text + "-assinado.xml";
            string ValidaXML = nfe.ValidarArquivoXML(ArquivoValida, schema, true);
            //if (nfe.ValidarArquivoXML(ArquivoValida, schema, true))
            {
                MessageBox.Show(ValidaXML);

                _FLAGVALIDADA = "S";
                NOTAFISCALEP.Save(Entity);
                result = true;
            }
           // else
            //{
             //   _FLAGVALIDADA = "N";
             //   NOTAFISCALEP.Save(Entity);
             //   result = false;

              //  MessageBox.Show("Problemas identificados na validação.");
           // }

            return result;
        }

        private void GeraLote()
        {
            string ArquivoLote = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\assinado\" + txtChaveAcesso.Text + "-assinado.xml";

            try
            {
                Random r = new Random(5);
                string NumLote = Convert.ToString((r.Next() + DateTime.Now.Millisecond));

                nfec.nfecsharp nfe = new nfec.nfecsharp();

                string retStatus = nfe.GerarLote(ArquivoLote, Convert.ToInt32(NumLote));

                //salvando os caminho do arquivo em lote
                _ARQUIVOLOTE = retStatus;
                NOTAFISCALEP.Save(Entity);
            }
            catch (Exception)
            {
                _ARQUIVOLOTE = string.Empty;
                NOTAFISCALEP.Save(Entity);
            }

            /* exemplo de lote com varias NF-e*/
            //retStatus = nfe.GerarLote(@"C:\nfe-app\nfe\arquivos\assinado\NFe42094521408936000545450010000003620000000996-assinado.xml;C:\nfe-app\nfe\arquivos\assinado\NFe42090454645684000164550010000003790000000993-assinado.xml;C:\nfe-app\nfe\arquivos\assinado\NFe42090545456544500164550010000004130000000997-assinado.xml;", Convert.ToInt32(txtLoteID.Text));				

        }

        private void Transmitir()
        {
            string ArquivoLote = NOTAFISCALEP.Read(_IDNOTAFISCALE).ARQUIVOLOTE;

            try
            {
                nfec.nfecsharp nfe = new nfec.nfecsharp();
                _RECIBONFE = nfe.NfeRecepcao(ArquivoLote);
                NOTAFISCALEP.Save(Entity);
            }
            catch (Exception)
            {
                _RECIBONFE = string.Empty;
                NOTAFISCALEP.Save(Entity);
            }
        }

        private void ConsultaSituacao()
        {
            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");

            _SITUACAONFE = nfe.NfeConsulta(NFe);
            NOTAFISCALEP.Save(Entity);
        }

        private void PrintDanfe()
        {
            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            string pathArquivo = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\procNFe\" + NFe + "-procNFe.xml";
            nfe.NFeDanfe(pathArquivo, pathArquivo, 3, false);
        }

        private void txtQuanProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para fazer cálculo de M2 pressione Ctrl+Q e para M3 Ctrl+M";
        }

        private void txtQuanProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.Q))
            {
                using (FormMedQuadrada frm = new FormMedQuadrada())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        txtQuanProduto.Text = result.ToString("n2");
                }
            }
            else if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.M))
            {
                using (FormMedCubico frm = new FormMedCubico())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        txtQuanProduto.Text = result.ToString("n2");
                }
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_NOTAFISCALEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void assinarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                openFileDialog1.InitialDirectory = Application.StartupPath + "\\nfe\\arquivos";
                openFileDialog1.Filter = "NF-e (*.xml)|";
                openFileDialog1.Title = "Assinar NF-e";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    nfec.nfecsharp nfe = new nfec.nfecsharp();

                    //string str = "#RETORNO: " + nfe.AssinarArquivoXML(openFileDialog1.FileName, "infNFe");
                    string str = nfe.AssinarArquivoXML(openFileDialog1.FileName, "infNFe");

                    if (str.ToLower().IndexOf("sucesso") > 0)
                        MessageBox.Show(str);
                    else
                        MessageBox.Show(str);

                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possivel assinar a NFe!.");
            }

        }

        private Boolean ConsultSiTNFe()
        {
            Boolean result = false;
            
            //if (txtChaveAcesso.Text != string.Empty)
            //{
            //    nfec.nfecsharp nfe = new nfec.nfecsharp();
            //    string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            //    string MensConsultSit = nfe.NfeConsulta(NFe);

            //    int pos = MensConsultSit.IndexOf("Autorizado o uso da NF-e");               

            //    if (pos != -1)
            //        result = true;
            //}

            if (_IDNOTAFISCALE != -1)
            {

                if (NOTAFISCALEP.Read(_IDNOTAFISCALE).FLAGCANCELADA.TrimEnd() == "S")
                    result = true;
                else if (_IDNOTAFISCALE != -1 && NOTAFISCALEP.Read(_IDNOTAFISCALE).FLAGENVIADA.TrimEnd() == "S")
                    result = true;
            }


            return result;

        }

        private string RetLoteConsultSiTNFe()
        {
            string result = string.Empty;
            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            string MensConsultSit = nfe.NfeConsulta(NFe);

            int pos = MensConsultSit.IndexOf("#");

            if (pos != -1)
            {
                result = MensConsultSit.Substring(pos + 1);
            }

            return result;

        }

        private void EnviarEmailUso()
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);

               // var toAddress = new MailAddress("contato@imexsistema.com.br");
                var toAddress = new MailAddress("contato@imexsistemas.com.br");
                //const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string subject = "Acesso ao IMEX Sistemas";
                string body = "O cliente abaixo acessou o IMEX Sistemas : " + DateTime.Now.ToString() + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += "DADOS DO CLIENTE:" + System.Environment.NewLine.ToString();
                body += "Nome: " + EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += "Telefone: " + EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += "Cidade/UF: " + EMPRESATy.CIDADE + " / " + EMPRESATy.UF + System.Environment.NewLine.ToString();
                body += "Email: " + EMPRESATy.EMAIL + System.Environment.NewLine.ToString();
                body += "CNPJ:" + EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
                body += "Computador:" + NomeComputador() + System.Environment.NewLine.ToString();
                 body += "Tela: Nota Fiscal Eletrônica" + System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();


                Boolean UsoConexaoSegura = true;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "N")
                    UsoConexaoSegura = false;

                var smtp = new SmtpClient
                {
                   // Host = "mail.imexsistema.com.br",
                    Host = "smtp.site.com.br",
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = UsoConexaoSegura,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                        smtp.Send(message);

                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na validação do email!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private string NomeComputador()
        {
            string result = "Computador não indentificado";
            try
            {
                result = System.Windows.Forms.SystemInformation.ComputerName;
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        private void gerarAssinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else if (lblSituacao.Text == "CANCELADA")
            {
                string Msg = "Nota Cancelada!";
                errorProvider1.SetError(lblSituacao, Msg);
                MessageBox.Show(Msg);
            }
            else if (!ValidacoesNFe())
            {

            }
            else
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                
                    //excluir arquivo caso exista
                    string path = ConfigSistema1.Default.PathInstall + @"\NFE\arquivos" + @"\" + txtChaveAcesso.Text + ".xml";
                    if (File.Exists(path))
                    {
                        Util.ExibirMSg("Excluindo Arquivo XML Anterior!", "Blue");
                        File.Delete(path);
                    }

                 

                    ////excluir arquivo caso exista
                string nome_arq = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "") + "-procNFe.xml";
                    //PesquisaArquivoXML(ConfigSistema1.Default.PathInstall + @"\NFE\arquivos\procNFe", nome_arq);

                    try
                    {
                        //Envia email para IMEX notificando qual empresa esta emitindo a NFe
                        if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                        {
                            EnviarEmailUso();
                        }

                        NOTAFISCALEP.Save(Entity);

                        GeraXMLNFeCompProduto(false);

                        _FLAGARQUIVOXML = "S";

                        NOTAFISCALEP.Save(Entity);

                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        _FLAGARQUIVOXML = "N";
                        NOTAFISCALEP.Save(Entity);

                        MessageBox.Show("Erro Técnico: " + ex.Message);
                        this.Cursor = Cursors.Default;
                    }

                
            }
        }

        private void ExcluirArquivoXMProc()
        {
            try
            {
                if ( _FLAGENVIADA == "N")
                {
                    string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    string caminhoData = Convert.ToDateTime(msktDataEmissao.Text).ToString("yyyy") + Convert.ToDateTime(msktDataEmissao.Text).ToString("MM");
                    string path = ConfigSistema1.Default.PathInstall + @"\NFE\arquivos\procNFe\" + caminhoData + @"\" + Chnfe + "-procNFe.xml";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        Util.ExibirMSg("Excluindo Arquivo XML PROCNFe Anterior!", "Blue");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        //private void PesquisaArquivoXML(string Caminho, string NomeArquivo)
        //{
        //    try
        //    {
        //        if (_FLAGENVIADA == "N")
        //        {
        //            string[] Lista_Diretorios = Directory.GetDirectories(Caminho);

        //            // Percorre o vetor armazenando o caminho de cada subdiretorio da pasta               
        //            for (int i = 0; i < Lista_Diretorios.Length; i++)
        //            {
        //                DirectoryInfo DirInfo = new DirectoryInfo(Lista_Diretorios[i].ToString());
        //                FileInfo[] AllFiles = DirInfo.GetFiles("*.xml");

        //                foreach (FileInfo FileXML in AllFiles)
        //                {
        //                    if (FileXML.Name.Trim() == NomeArquivo.Trim())
        //                    {
        //                        if (File.Exists(FileXML.Directory + @"\" + FileXML.Name.Trim()))
        //                        {
        //                            File.Delete(FileXML.Directory + @"\" + FileXML.Name.Trim());
        //                            Util.ExibirMSg("Excluindo Arquivo XML PROCNFe Anterior!", "Blue");
        //                        }
        //                    }
        //                }
        //            }
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro técnico: " + ex.Message);
        //    }     

        //}

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    nfec.nfecsharp nfe = new nfec.nfecsharp();  

                    string ConsultaProcessamento = nfe.NfeRetAutorizacao(txtReciboRecp.Text);
                    int localRec = ConsultaProcessamento.IndexOf("Autorizado");
                    int localRec2 = ConsultaProcessamento.IndexOf("Duplicidade");

                    if (localRec != -1)
                    {
                        NfeConsultaProcessamento = true;
                        _FLAGENVIADA = "S";
                        NOTAFISCALEP.Save(Entity);

                        if (!ExibirMensagem)
                            MessageBox.Show(ConsultaProcessamento);
                        else
                            Util.ExibirMSg(ConsultaProcessamento, "Blue");
                    }
                    else if (localRec2 != -1)
                    {
                        NfeConsultaProcessamento = true;
                        _FLAGENVIADA = "S";
                        NOTAFISCALEP.Save(Entity);

                        if (!ExibirMensagem)
                            MessageBox.Show(ConsultaProcessamento);
                        else
                            Util.ExibirMSg(ConsultaProcessamento, "Blue");
                    }
                    else
                    {
                        NfeConsultaProcessamento = false;
                        MessageBox.Show(ConsultaProcessamento);

                        _FLAGENVIADA = "N";
                        NOTAFISCALEP.Save(Entity);                      
                    }

                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    NfeConsultaProcessamento = false;
                    MessageBox.Show("Erro Técnico: " + ex.Message);
                    
                }
            }
        }

        private void assinarToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {
              
                ////excluir arquivo caso exista
               
              //  PesquisaArquivoXML(ConfigSistema1.Default.PathInstall + @"\NFE\arquivos\procNFe", nome_arq);


                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string NFe = txtChaveAcesso.Text;

                NfeAssinar = false;
                List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\");
                foreach (string p in dirs)
                {
                    int LengthLine = p.Length;
                    int pos = p.IndexOf(NFe);
                    if (pos != -1)
                    {
                        _FLAGASSINATURA = "S";
                        NOTAFISCALEP.Save(Entity);

                        NfeAssinar = true;
                        nfec.nfecsharp nfe = new nfec.nfecsharp();
                        string AssinaArquuivo = nfe.AssinarArquivoXML(p, "infNFe");

                        if (!ExibirMensagem)
                        {
                            MessageBox.Show(AssinaArquuivo);
                        }
                        else
                        {
                            Util.ExibirMSg(AssinaArquuivo, "Blue");
                        }                      

                        break;
                    }
                }

                this.Cursor = Cursors.Default;

                if (!NfeAssinar)
                {
                    if (!ExibirMensagem)
                    {
                        MessageBox.Show("Não foi possível assinar o arquivo!");
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível assinar o arquivo!");
                        ExibirMsgNfeStatus("Não foi possível assinar o arquivo!", Color.Red);
                    }
                }
            }
        } 


        private void inutilizaçãoNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmInutilizacaoNfe FrmInut = new FrmInutilizacaoNfe();
            FrmInut.ShowDialog();         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show("Antes de adicionar os produtos é necessário gravar a Nota Fiscal!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida! não é possível alterar!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmSearchPedidoNFe frm = new FrmSearchPedidoNFe())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        txtNumPedido.Text = result.ToString();
                        ListaProdutoPedido(result);
                        ListaProdutoPedidoMT2(result);
                    }
                }
            }
        }

        private void ListaProdutoPedido(int IDPEDIDO)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                if (item.FLAGEXIBIR.TrimEnd() == "S")
                {
                    cbProduto.SelectedValue = item.IDPRODUTO;
                    if (item.TOTALMT > 0)
                        txtQuanProduto.Text = (item.TOTALMT * item.QUANTIDADE).ToString();
                    else
                        txtQuanProduto.Text = item.QUANTIDADE.ToString();

                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n2");
                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                    
                    CalculoTributoAprox(txtNCM.Text);
                    
                    GravaProdutoListaProduto();
                }
            }
        }

        private void ListaProdutoPedidoMT2(int IDPEDIDO)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
            LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                if (item.FLAGEXIBIR.TrimEnd() == "S")
                {
                    cbProduto.SelectedValue = item.IDPRODUTO;
                    if (item.MT2 > 0)
                        txtQuanProduto.Text = (Convert.ToDecimal(item.MT2) * Convert.ToDecimal(item.QUANTIDADE)).ToString();
                    else
                        txtQuanProduto.Text = (Convert.ToDecimal(item.QUANTIDADE)).ToString(); ;

                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORMETRO).ToString("n4");
                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;

                    GravaProdutoListaProduto();
                }
            }
        }

        private void GravaProdutoListaProduto()
        {
            try
            {
                if (ValidacoesProdutos())
                {
                    PRODUTONFEP.Save(Entity2);
                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                    //Salva NOTAFISCAL
                    NOTAFISCALEP.Save(Entity);                   

                    Entity2 = null;
                    cbProduto.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja realmente excluir todos os registros?",
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    int IDPRODUTO = Convert.ToInt32(item.IDPRODUTO);
                    ExcluirEstoqueLote(IDPRODUTO);
                    PRODUTONFEP.Delete(Convert.ToInt32(item.IDPRODUTONFE));                 
                }

                ListaProdutoNotaFiscal(_IDNOTAFISCALE);

            }
        }


        private void siteDaReceitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("http://www.nfe.fazenda.gov.br/PORTAL/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
        }

        private void cancelamentoNFeManualmenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            //else if (!ConsultSiTNFe())
            //{
            //    MessageBox.Show("Não é possível cancelar, a NF-e não foi emitida!",
            //                 ConfigSistema1.Default.NomeEmpresa,
            //                 MessageBoxButtons.OK,
            //                 MessageBoxIcon.Error,
            //                 MessageBoxDefaultButton.Button1);
            //}
            else
            {

                DialogResult dr = MessageBox.Show("Deseja realmente cancelar a NFe?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    string ChaveNfe = InputBox("Chave de Acesso da NF-e ", ConfigSistema1.Default.NomeEmpresa, Chnfe);

                    string JustiCancNFe = InputBox("Justificativa de Cancelamento da NFe", ConfigSistema1.Default.NomeEmpresa, "");
                    //Chave de Acesso da NF-e 


                    if (ChaveNfe == string.Empty)
                    {
                        MessageBox.Show("Chave de Acesso da NF-enão poderá ser vazio!");
                    }
                    else if (JustiCancNFe == string.Empty)
                    {
                        MessageBox.Show("Campo de Justificativa não poderá ser vazio!");
                    }
                    else if (JustiCancNFe.Length < 15)
                    {
                        MessageBox.Show("Campo de Justificativa deverá conter igual ou superior a 15 caracteres!");
                    }
                    else
                    {
                        CreaterCursor Cr = new CreaterCursor();
                        this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                        string NumLote = string.Empty;
                        NumLote = RetLoteConsultSiTNFe();
                        NumLote = InputBox("Nº Lote: ", ConfigSistema1.Default.NomeEmpresa, NumLote);
                        nfec.nfecsharp nfe = new nfec.nfecsharp();
                        string MsgCancel = nfe.NfeCancelamento(ChaveNfe, NumLote, JustiCancNFe);

                        _FLAGCANCELADA = "S";
                        NOTAFISCALEP.Save(Entity);

                        MessageBox.Show(MsgCancel);

                        btnPesquisa_Click(null, null);

                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void gerarNfeAssinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (ConsultSiTNFe())
            {
                MessageBox.Show("NFe já emitida!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else if (lblSituacao.Text == "CANCELADA")
            {
                string Msg = "Nota Cancelada!";
                errorProvider1.SetError(lblSituacao, Msg);
                MessageBox.Show(Msg);
            }
            else if (!ValidacoesNFe())
            {

            }
            else
            {
                ExibirMensagem = true;
                NfeAssinar = false;
                NfeValidar = false;
                NfeCriarLote = false;
                NfeSituacao = false;
                NfeAutorizacao = false;
                NfeConsultaProcessamento = false;
                
                //1 - Gerar
                gerarAssinarToolStripMenuItem_Click(null, null);
                System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                //2 - Assinar
                assinarToolStripMenuItem1_Click_1(null, null);
                System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                 if (NfeAssinar)
                 {
                       //3 - Validar
                     validarToolStripMenuItem_Click(null, null);
                     System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                     if (NfeValidar)
                     {
                         //4 - Gerar Lote Único
                         criaLoteToolStripMenuItem_Click(null, null);
                         System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos
                          if (NfeCriarLote)
                          {
                              //5 - Autorização
                              autorizaçãoToolStripMenuItem_Click(null, null);
                              System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                              if(NfeAutorizacao)
                              {
                                  //6 - Consulta Processamento
                                  toolStripMenuItem3_Click(null, null);
                                  System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                                  if(NfeConsultaProcessamento)
                                  {
                                      //7 - Consulta Situação
                                      consultaSituaçãoToolStripMenuItem_Click(null, null);
                                      System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos

                                      if(NfeSituacao)
                                      {
                                          //8 Imprimir Danfe
                                          System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos
                                          imprimirDanfeEmPDFToolStripMenuItem_Click(null, null);
                                      }

                                  }
                              }
                          }
                     }

                 }

            }
        }

        private void imprimirDanfeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (txtChaveAcesso.Text.TrimEnd().TrimStart() == string.Empty)
            {
                errorProvider1.SetError(txtChaveAcesso, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
            }
            else if (_FLAGCANCELADA.Trim() == "S")
            {
                string Msg = "Nota Cancelada, não é possível imprimir DANFE!";
                errorProvider1.SetError(lblSituacao, Msg);
                MessageBox.Show(Msg);
            }           
            else
            {
                PrintDanfeComponentePDF(3);
            }

        }

        private void préVisualizarDANFESemValorFiscalToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //string ArquivoXML = ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\" + txtChaveAcesso.Text + ".xml";
                //if (File.Exists(ArquivoXML))
                //{
                //    string pathPDF = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\PDF\ARQUIVOTEMP.pdf";
                //    if (File.Exists(pathPDF))
                //        File.Delete(pathPDF);

                //    nfec.nfecsharp nfe = new nfec.nfecsharp();

                //    if (!nfe.NFeDanfe(ArquivoXML, pathPDF, 2, true))
                //    {
                //        this.Cursor = Cursors.Default;
                //        MessageBox.Show("Erro ao Imprimir DANFE!");
                //    }
                //    else
                //    {
                //        this.Cursor = Cursors.Default;
                //        Util.ExibirMSg("Aguarde... DANFE em pdf abrindo...", "Blue");

                //        System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(pathPDF);
                //    }

                //    this.Cursor = Cursors.Default;
                //}
                //else
                {
                    this.Cursor = Cursors.Default;
                    OpenFileDialog fdlg = new OpenFileDialog();
                    fdlg.Title = "Selecione o arquivo XML";
                    fdlg.InitialDirectory = ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\assinado";

                    fdlg.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
                    fdlg.FilterIndex = 1;
                    fdlg.RestoreDirectory = true;
                    string NomeArquivo = string.Empty;

                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        NomeArquivo = fdlg.FileName;
                        
                        string pathPDF = ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\PDF\ARQUIVOTEMP.pdf";
                        if (File.Exists(pathPDF))
                            File.Delete(pathPDF);

                        nfec.nfecsharp nfe = new nfec.nfecsharp();

                        this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
                        if (!nfe.NFeDanfe(NomeArquivo, pathPDF, 3 ,  true))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Erro ao Imprimir DANFE!");
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            Util.ExibirMSg("Aguarde... DANFE em pdf abrindo...", "Blue");
                            
                          //  System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(pathPDF);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }


        }

        private void salvarArquivoXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {

                string path = ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\gerados" + @"\" + txtChaveAcesso.Text + ".xml";
                if (File.Exists(path))
                {
                    System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(path);
                }
                else
                {
                    MessageBox.Show("Arquivo XML não localizado!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                }
            }
        }


        private void cbTransportadora_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
        }

        private void cbTransportadora_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchTransportadora frm = new FrmSearchTransportadora())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        cbTransportadora.SelectedValue = result;
                }
            }
        }

        private void abrirArquivoXMLAssinadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {

                string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                string path = ConfigSistema1.Default.PathInstall + @"\NFE\retornos" + @"\" + Chnfe + "-sit.xml";


                if (File.Exists(path))
                {
                    System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(path);
                }
                else
                {
                    MessageBox.Show("Arquivo XML não localizado!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                }
            }
        }

        private void abrirArquivoXMLGeradoRetornoProtocoloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {

                string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                string caminhoData = Convert.ToDateTime(msktDataEmissao.Text).ToString("yyyy") + Convert.ToDateTime(msktDataEmissao.Text).ToString("MM");
                string path = ConfigSistema1.Default.PathInstall + @"\NFE\arquivos\procNFe\" + caminhoData + @"\" + Chnfe + "-procNFe.xml";


                if (File.Exists(path))
                {
                    System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(path);
                }
                else
                {
                    MessageBox.Show("Arquivo XML não localizado!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                }
            }
        }

        private void txtAliqCredICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (TxtBaseCalcICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliqCredICMS.Text))
                {
                    errorProvider1.SetError(txtAliqCredICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtAliqCredICMS.Text);
                    txtAliqCredICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliqCredICMS, "");
                }
            }
            else
                txtAliqCredICMS.Text = "0,00";
        }

        private void txtValorCredICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorCredICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCredICMS.Text))
                {
                    errorProvider1.SetError(txtValorCredICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorCredICMS.Text);
                    txtValorCredICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorCredICMS, "");
                }
            }
            else
                txtValorCredICMS.Text = "0,00";
        }

        private void txtAliqCredICMS_Leave(object sender, EventArgs e)
        {
            txtValorCredICMS.Text = ((Convert.ToDecimal(TxtBaseCalcICMS.Text) * Convert.ToDecimal(txtAliqCredICMS.Text)) / 100).ToString("n2");
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

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show("Antes de adicionar o Caixa é necessário gravar a Nota Fiscal!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (Convert.ToInt32(cbTipo.SelectedValue) < 1)
                {
                    errorProvider1.SetError(cbTipo, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else if (Convert.ToDecimal(txtValorPago.Text) <= 0)
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Deseja realmente lançar o valor de " + txtValorPago.Text + " no caixa?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                        EntradaCaixa();
                }
            }
        }

        private void EntradaCaixa()
        {
            try
            {
                if (!VerificaCaixaNF("NFe" + txtNotaFiscal.Text))
                {
                    CAIXAEntity CaixaTy = new CAIXAEntity();
                    CAIXAProvider CaixaP = new CAIXAProvider();

                    CaixaTy.IDCAIXA = -1;
                    CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
                    CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
                    CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                    CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

                    if (cbCentroCusto.SelectedIndex > 0)
                        CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

                    CaixaTy.NDOCUMENTO = "NFe" + txtNotaFiscal.Text;
                    CaixaTy.OBSERVACAO = "Nota Fiscal Eletrôncia nº " + "NFe" + txtNotaFiscal.Text + " Cliente: " + cbCliente.SelectedValue + " - " + cbCliente.Text;

                    try
                    {
                        CaixaP.Save(CaixaTy);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível fazer o movimento de caixa!");
                        MessageBox.Show("Erro Técnico: " + ex.Message);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean VerificaCaixaNF(string NDOCUMENTO)
        {
            Boolean result = false;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));
            LIS_CAIXACollection LIS_CAIXAColl = new LIS_CAIXACollection();
            LIS_CAIXAProvider LIS_CAIXAP = new LIS_CAIXAProvider();

            LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_CAIXAColl.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Já existe lançamento referente a " + NDOCUMENTO + ", deseja fazer novo lancamento?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                    result = true;

            }

            return result;
        }

        private Boolean LocalizarCFOP(string CFOP)
        {
            Boolean result = false;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODCFOP", "System.String", "like", CFOP));
                CFOPCollection CFOPColl_2 = new CFOPCollection();
                CFOPColl_2 = CFOPP.ReadCollectionByParameter(RowRelatorio);

                if (CFOPColl_2.Count > 0)
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void importarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string CFOP_Digitado = InputBox("Digite o CFOP para emissão desta NF-e ", ConfigSistema1.Default.NomeEmpresa, "5102");
            string CST_CSOSN = InputBox("Digite o CST/CSOSN para emissão desta NF-e ", ConfigSistema1.Default.NomeEmpresa, "");

            if (CFOP_Digitado.Trim() == string.Empty)
            {
                MessageBox.Show("CFOP Inválido");
            }
            else if (!LocalizarCFOP(CFOP_Digitado))
            {
                MessageBox.Show("CFOP não localizado!");
            }
            else if (!VerificaPlanos())
            {

            }
            else
            {

                using (FrmSearchPedidoNFe frm = new FrmSearchPedidoNFe())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    DialogResult dr = MessageBox.Show("Deseja realmente gerar Nota Fiscal pelo pedido nº " + result + " ?",
                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        if (result > 0)
                        {
                            try
                            {

                                CreaterCursor Cr = new CreaterCursor();
                                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                                Entity = null;
                                Entity2 = null;

                                GetConfiSistema();
                                tabControl1.SelectTab(0);
                                tabControl2.SelectTab(0);

                                cbCFOP.SelectedIndex = cbCFOP.FindString(CFOP_Digitado);
                                cbCFOPProduto.SelectedIndex = cbCFOPProduto.FindString(CFOP_Digitado);

                                //Armazena Dados do Pedido
                                PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                                PEDIDOProvider PEDIDOP = new PEDIDOProvider();
                                PEDIDOTy = PEDIDOP.Read(result);

                                //Cliente
                                cbCliente.SelectedValue = PEDIDOTy.IDCLIENTE;

                                //CFOP
                                string UFEmpresa = EMPRESAP.Read(1).UF;
                                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", CLIENTEP.Read(Convert.ToInt32(PEDIDOTy.IDCLIENTE)).COD_MUN_IBGE.ToString()));
                                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                                string UFCliente = LIS_MUNICIPIOSColl[0].UF;

                                //if (UFEmpresa != UFCliente)
                                //    cbCFOP.SelectedValue = 4;
                                //else
                                //    cbCFOP.SelectedValue = 2;

                               cbCFOP.SelectedIndex = cbCFOP.FindString(CFOP_Digitado);

                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                                //Produto
                                //Armazena dados Produto do Pedido
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", result.ToString()));
                                txtNumPedido.Text = result.ToString();

                                LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
                                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

                                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                                {
                                    PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
                                    PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                                    cbProduto.SelectedValue = item.IDPRODUTO;
                                    txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");
                                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n4");
                                    txtInformAddProduto.Text = item.DADOSADICIONAIS;
                                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;

                                    if (CST_CSOSN.Trim().Length == 0)
                                    {
                                        if (PRODUTOStY.IDCST != null)
                                            cbSTrib.SelectedValue = PRODUTOStY.IDCST;
                                        else
                                            MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem CST/CSON cadastrado!");
                                    }
                                    else
                                    {
                                        cbSTrib.SelectedIndex = cbSTrib.FindString(CST_CSOSN);
                                    }

                                    if (PRODUTOStY.IDUNIDADE!= null)
                                        cbUnd.SelectedValue = PRODUTOStY.IDUNIDADE;
                                    else
                                        MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem Unidade cadastrada!");


                                    if (txtNCM.Text.Trim() != string.Empty)
                                        CalculoTributoAprox(txtNCM.Text);
                                    else
                                      MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem NCM cadastrado!");

                                    SaveLoteEstoque();
                                    PRODUTONFEP.Save(Entity2);
                                    Entity2 = null;
                                }

                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", result.ToString()));

                                LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                                LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

                                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                                {
                                    PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
                                    PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                                    if (item.FLAGEXIBIR.TrimEnd() == "S")
                                    {
                                        cbProduto.SelectedValue = item.IDPRODUTO;
                                        if (item.MT2 > 0 && item.FLAGBAIXAESTMT == "S")
                                        {
                                            txtQuanProduto.Text = (Convert.ToDecimal(item.MT2) * Convert.ToDecimal(item.QUANTIDADE)).ToString();
                                            txtValorUnitProd.Text = Convert.ToDecimal(item.VALORMETRO).ToString("n4");

                                        }
                                        else
                                        {
                                            txtQuanProduto.Text = (Convert.ToDecimal(item.QUANTIDADE)).ToString(); ;
                                            txtValorUnitProd.Text = (Convert.ToDecimal(item.VALORTOTAL) / Convert.ToDecimal(item.QUANTIDADE)).ToString("n4");
                                        }

                                        cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;

                                        if (txtNCM.Text.Trim() != string.Empty)
                                            CalculoTributoAprox(txtNCM.Text);
                                        else
                                            MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem NCM cadastrado!");


                                        if (PRODUTOStY.IDCST != null)
                                            cbSTrib.SelectedValue = PRODUTOStY.IDCST;
                                        else
                                            MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem CST/CSON cadastrado!");

                                        if (PRODUTOStY.IDUNIDADE != null)
                                            cbUnd.SelectedValue = PRODUTOStY.IDUNIDADE;
                                        else
                                            MessageBox.Show("O produto: " + item.IDPRODUTO.ToString() + " - " + item.NOMEPRODUTO + " não tem Unidade cadastrada!");


                                        
                                        PRODUTONFEP.Save(Entity2);
                                        Entity2 = null;
                                    }
                                }

                                ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                                //Cobrança
                                if (PEDIDOTy.IDFORMAPAGAMENTO != null)
                                    cbFormaPagto.SelectedValue = PEDIDOTy.IDFORMAPAGAMENTO;

                                if (PEDIDOTy.IDLOCALCOBRANCA != null)
                                    cbLocalCobranca.SelectedValue = PEDIDOTy.IDLOCALCOBRANCA;

                                if (PEDIDOTy.IDFORMAPAGAMENTO != null && PEDIDOTy.IDLOCALCOBRANCA != null)
                                {
                                    DialogResult dr2 = MessageBox.Show("Deseja gerar Duplicatas?",
                                    ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                                    if (dr2 == DialogResult.Yes)
                                        SaveDuplicatas();
                                }

                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                                this.Cursor = Cursors.Default;

                                MessageBox.Show("Nota Fiscal Gerada com sucesso!");

                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Não foi possível gerar Nota Fiscal!");
                                MessageBox.Show("Erro Técnico: " + ex.Message);

                            }
                        }

                    }
                }
            }
        }

        private void txtPorcDesconProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcDesconProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcDesconProduto.Text))
                {
                    errorProvider1.SetError(txtPorcDesconProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcDesconProduto.Text);
                    txtPorcDesconProduto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconProduto, "");

                    decimal valorTotal = Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text);
                    txtValorDescontoProduto.Text = ((Convert.ToDecimal(valorTotal) * Convert.ToDecimal(txtPorcDesconProduto.Text)) / 100).ToString("n2");
                }
            }
            else
                txtPorcDesconProduto.Text = "0,00";
        }

        private void txtValorDescontoProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorDescontoProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorDescontoProduto.Text))
                {
                    errorProvider1.SetError(txtValorDescontoProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorDescontoProduto.Text);
                    txtValorDescontoProduto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorDescontoProduto, "");
                }
            }
            else
                txtValorDescontoProduto.Text = "0,00";
        }
      
        private void DGDadosProduto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (ConsultSiTNFe())
                {
                    MessageBox.Show("Não é possível alterar NFe já emitida!",
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                }
                else
                {
                    if (e.ColumnIndex == 6)
                    {
                        string ValueCell = DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                        PRODUTONFEEntity PRODUTONFETy = new PRODUTONFEEntity();
                        PRODUTONFETy = PRODUTONFEP.Read(Convert.ToInt32(LIS_PRODUTONFEColl[e.RowIndex].IDPRODUTONFE));
                        PRODUTONFETy.DESCPRODUTO2 = ValueCell;
                        PRODUTONFEP.Save(PRODUTONFETy);
                    }
                    else if (e.ColumnIndex == 10)
                    {
                        string ValueCell2 = DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                        PRODUTONFEEntity PRODUTONFETy = new PRODUTONFEEntity();
                        PRODUTONFETy = PRODUTONFEP.Read(Convert.ToInt32(LIS_PRODUTONFEColl[e.RowIndex].IDPRODUTONFE));
                        PRODUTONFETy.VALORICMS = Convert.ToDecimal(ValueCell2);
                        PRODUTONFEP.Save(PRODUTONFETy);
                    }

                }
            }
        }

        private void cbFormaPagto_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbFormaPagto.SelectedValue) > 0)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", Convert.ToInt32(cbFormaPagto.SelectedValue).ToString()));
                    ITENSFORMAPAGTOCollection ITENSFORMAPAGTOColl = new ITENSFORMAPAGTOCollection();
                    ITENSFORMAPAGTOProvider ITENSFORMAPAGTOP = new ITENSFORMAPAGTOProvider();
                    ITENSFORMAPAGTOColl = ITENSFORMAPAGTOP.ReadCollectionByParameter(RowRelatorio, "IDITENSFORMAPAGTO");
                    if (ITENSFORMAPAGTOColl.Count > 0)
                        rbPrazo.Checked = true;
                    else
                        rbAvista.Checked = true;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao consultar forma de pagamento!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }
      

        private void TxtValorICMSSubst_Leave(object sender, EventArgs e)
        {
            try
            {
               // txtTotalNota.Text = (Convert.ToDecimal(txtTotalNota.Text) + Convert.ToDecimal(TxtValorICMSSubst.Text)).ToString("n2");
                txtTotalNota.Text = (Convert.ToDecimal(txtTotalProduto.Text) + (Convert.ToDecimal(TxtValorICMSSubst.Text) * LIS_PRODUTONFEColl.Count)).ToString("n2");
                txtTotalNota2.Text = (Convert.ToDecimal(txtTotalProduto.Text) + (Convert.ToDecimal(TxtValorICMSSubst.Text) * LIS_PRODUTONFEColl.Count)).ToString("n2");
                
            }
            catch (Exception)
            {

                MessageBox.Show("Erro no cálculo");
            }
        }

        private void cancelamentoDeNFePorEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            //else if (!ConsultSiTNFe())
            //{
            //    MessageBox.Show("Não é possível cancelar, a NF-e não foi emitida!",
            //                 ConfigSistema1.Default.NomeEmpresa,
            //                 MessageBoxButtons.OK,
            //                 MessageBoxIcon.Error,
            //                 MessageBoxDefaultButton.Button1);
            //}
            else
            {

                DialogResult dr = MessageBox.Show("Deseja realmente cancelar a NFe?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    string ChaveNfe = InputBox("Chave de Acesso da NF-e ", ConfigSistema1.Default.NomeEmpresa, Chnfe);

                    string JustiCancNFe = InputBox("Justificativa de Cancelamento da NFe", ConfigSistema1.Default.NomeEmpresa, "");
                    string EventoNFe = InputBox("Número do Evento", ConfigSistema1.Default.NomeEmpresa, "1");
                    //Chave de Acesso da NF-e                    

                    if (ChaveNfe == string.Empty)
                    {
                        MessageBox.Show("Chave de Acesso da NF-enão poderá ser vazio!");
                    }
                    else if (JustiCancNFe == string.Empty)
                    {
                        MessageBox.Show("Campo de Justificativa não poderá ser vazio!");
                    }
                    else if (JustiCancNFe.Length < 15)
                    {
                        MessageBox.Show("Campo de Justificativa deverá conter igual ou superior a 15 caracteres!");
                    }
                    else
                    {
                        CreaterCursor Cr = new CreaterCursor();
                        this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                        string protocolo = string.Empty;
                        protocolo = RetLoteConsultSiTNFe();
                        protocolo = InputBox("Protocolo: ", ConfigSistema1.Default.NomeEmpresa, protocolo);
                        nfec.nfecsharp nfe = new nfec.nfecsharp();

                        //Mensagem de retorno
                        // 128 Lote de Evento Processado
                        //  135 Evento registrado e vinculado a NF-e
                        //   136 Evento registrado, mas não vinculado a NF-e 

                        Random r = new Random(8);
                        string idlote = Convert.ToString((r.Next() + DateTime.Now.Millisecond));
                        idlote = InputBox("idlote: ", ConfigSistema1.Default.NomeEmpresa, idlote);

                        string MsgCancel = nfe.NfeCancelamentoEvento(idlote, ChaveNfe, EventoNFe, protocolo, JustiCancNFe);

                        if (MsgCancel.IndexOf("135") != -1)
                        {
                            _FLAGCANCELADA = "S";
                            NOTAFISCALEP.Save(Entity);
                            MessageBox.Show(MsgCancel);
                            MessageBox.Show("Consulte o site da Receita para verificar o cancelamento desta NFe!");
                        }
                        else if (MsgCancel.IndexOf("136") != -1)
                        {
                            _FLAGCANCELADA = "S";
                            NOTAFISCALEP.Save(Entity);
                            MessageBox.Show(MsgCancel);
                            MessageBox.Show("Consulte o site da Receita para verificar o cancelamento desta NFe!");
                        }                       
                        else if (MsgCancel.IndexOf("vinculado") != -1)
                        {
                            _FLAGCANCELADA = "S";
                            NOTAFISCALEP.Save(Entity);
                            MessageBox.Show(MsgCancel);
                            MessageBox.Show("Consulte o site da Receita para verificar o cancelamento desta NFe!");
                        }
                        else
                        {
                            _FLAGCANCELADA = "N";
                            MessageBox.Show(MsgCancel);
                            NOTAFISCALEP.Save(Entity);
                            MessageBox.Show("Não foi possível cancelar a Nota Fiscal, favor verificar a situação no site da Receita Federal");                            
                        }

                        if (_FLAGCANCELADA == "S")
                            AlterarEstoqueLote(txtNotaFiscal.Text);

                        btnPesquisa_Click(null, null);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void AlterarEstoqueLote(string NUMERODOC)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "=", NUMERODOC));

                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                int Contador = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    ESTOQUELOTEEntity ESTOQUELOTETy = new ESTOQUELOTEEntity();
                    ESTOQUELOTETy = ESTOQUELOTEP.Read(Convert.ToInt32(item.IDESTOQUELOTE));
                    if (ESTOQUELOTETy != null)
                    {
                        ESTOQUELOTETy.FLAGATIVO = "N";
                        ESTOQUELOTEP.Save(ESTOQUELOTETy);
                        Contador++;
                    }
                }

                if (Contador > 0)
                    Util.ExibirMSg("Estoque Lote Cancelado com Sucesso!", "blue");

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void chkAlterar_Click(object sender, EventArgs e)
        {
            TxtValorICMS.ReadOnly = !chkAlterar.Checked;
            TxtBaseCalcICMS.ReadOnly = !chkAlterar.Checked;
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmEmailNFe frm = new FrmEmailNFe())
                {
                    string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    string caminhoData = Convert.ToDateTime(msktDataEmissao.Text).ToString("yyyy") + Convert.ToDateTime(msktDataEmissao.Text).ToString("MM");
                    string path = ConfigSistema1.Default.PathInstall + @"\NFE\arquivos\procNFe\" + caminhoData + @"\" + Chnfe + "-procNFe.xml";

                    frm.Email = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).EMAILCLIENTE;
                    frm.ChaveNFe = Chnfe;

                    if (File.Exists(path))
                        frm.ArquivoNFe = path;

                    frm.ShowDialog();

                }
            }
        }

        private void btnCadMensagem_Click(object sender, EventArgs e)
        {
            using (FrmMensagem frm = new FrmMensagem())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbMensagem.SelectedValue);
                GetDropMensagem();

                cbMensagem.SelectedValue = CodSelec;
            }
        }

        private void GetDropMensagem()
        {
            MENSAGEMCollection MENSAGEMColl = new MENSAGEMCollection();
            MENSAGEMColl = MENSAGEMNP.ReadCollectionByParameter(null);

            cbMensagem.DisplayMember = "NOME";
            cbMensagem.ValueMember = "IDMENSAGEM";

            MENSAGEMEntity MENSAGEMNFETy = new MENSAGEMEntity();
            MENSAGEMNFETy.NOME = ConfigMessage.Default.MsgDrop;
            MENSAGEMNFETy.IDMENSAGEM = -1;
            MENSAGEMColl.Add(MENSAGEMNFETy);

            Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity>(cbMensagem.DisplayMember);

            MENSAGEMColl.Sort(comparer.Comparer);
            cbMensagem.DataSource = MENSAGEMColl;

            // cbMensagem.SelectedIndex = 0;
        }

        private void cbMensagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMensagem.SelectedValue) > 0)
               txtObservacao.Text+= " "+ MENSAGEMNP.Read(Convert.ToInt32(cbMensagem.SelectedValue)).MENSAGEM;
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario.Location = new Point(230, 55);
            FormCalendario.Name = "FrmCalendario";
            FormCalendario.Text = "Calendário";
            FormCalendario.ResumeLayout(false);
            FormCalendario.Controls.Add(monthCalendar2);
            FormCalendario.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(230, 55);
            FormCalendario3.Name = "FrmCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
            
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }      

        private void txtVlFrete_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtVlFreteProd.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlFreteProd.Text))
                {
                    errorProvider1.SetError(txtVlFreteProd, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtVlFreteProd.Text);
                    txtVlFreteProd.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlFreteProd, "");
                }
            }
            else
                txtVlFreteProd.Text = "0,00";
        }

        private void enviarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (!ConsultSiTNFe())
            {
                MessageBox.Show("NFe não foi emitida, não é possível criar a carta de correção!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmCartaCorrecao frm = new FrmCartaCorrecao())
                {
                    frm.ChaveNFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    frm.ShowDialog();

                }
            }
        }

        private void detalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (!ConsultSiTNFe())
            {
                MessageBox.Show("NFe não foi emitida, não é possível enviar email!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmEmailNFeCCe frm = new FrmEmailNFeCCe())
                {
                    string Chnfe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                    string path = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\" + Chnfe + "-CCe.xml";

                    frm.Email = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).EMAILCLIENTE;
                    frm.ChaveNFe = Chnfe;

                    if (File.Exists(path))
                        frm.ArquivoNFe = path;

                    frm.ShowDialog();

                }
            }
        }

        private void configuraçãoDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
            }
        }
      
        private void txtVlAproxTributos_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtVlAproxTributos.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlAproxTributos.Text))
                {
                    errorProvider1.SetError(txtVlAproxTributos, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtVlAproxTributos.Text);
                    txtVlAproxTributos.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlAproxTributos, "");
                }
            }
            else
                txtVlAproxTributos.Text = "0,00";
        }

        private void btnCadNCM_Click(object sender, EventArgs e)
        {
            using (FrmNCM frm = new FrmNCM())
            {
                if (txtNCM.Text.TrimEnd().TrimStart() != string.Empty)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("CODNCM", "System.String", "=", txtNCM.Text));
                    NCMCollection NCMColl = new NCMCollection();
                    NCMProvider NCMP = new NCMProvider();
                    NCMColl = NCMP.ReadCollectionByParameter(RowRelatorio);
                    if (NCMColl.Count > 0)
                        frm._IDNCM = NCMColl[0].IDNCM;
                }

                frm.ShowDialog();
            }
        }

        private void txtQuanProduto_Leave(object sender, EventArgs e)
        {
            CalculoTributoAprox(txtNCM.Text);
            SomaTotaProduto();
            CalculoICMSST();
        }

        private void SomaTotaProduto()
        {
            try
            {
                txtVlTotal.Text = (Convert.ToDecimal(txtValorUnitProd.Text) * Convert.ToDecimal(txtQuanProduto.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                string msgerro = "Erro no cálculo";
                errorProvider1.SetError(txtValorUnitProd, msgerro);
                errorProvider1.SetError(txtQuanProduto, msgerro);
                MessageBox.Show(msgerro);
            }
        }

        private void CalculoTributoAprox(string CODNCM)
        {
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
                    if (txtQuanProduto.Text.TrimEnd().TrimStart() == string.Empty)
                        txtQuanProduto.Text = "0,00";

                    if (txtValorUnitProd.Text.TrimEnd().TrimStart() == string.Empty)
                        txtValorUnitProd.Text = "0,00";

                    decimal TotalProduto = Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text);
                    decimal TributoAprox = (TotalProduto * Convert.ToDecimal(NCMColl[0].ALNACIONAL)) / 100;

                    txtVlAproxTributos.Text = TributoAprox.ToString("n2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível efetuar cálculo de tributos aproximados!");
            }
        }

        private void txtValorUnitProd_Leave(object sender, EventArgs e)
        {
            CalculoTributoAprox(txtNCM.Text);
            SomaTotaProduto();
                CalculoICMSST();
        }

        private void txtVlAproxTributos_Enter(object sender, EventArgs e)
        {
            CalculoTributoAprox(txtNCM.Text);
        }

        private void txtBaseST_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtBaseST.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseST.Text))
                {
                    errorProvider1.SetError(txtBaseST, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtBaseST.Text);
                    txtBaseST.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtBaseST, "");
                }
            }
            else
                txtBaseST.Text = "0,00";
        }

        private void txtICMSST_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtICMSST.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtICMSST.Text))
                {
                    errorProvider1.SetError(txtICMSST, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtICMSST.Text);
                    txtICMSST.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtICMSST, "");
                }
            }
            else
                txtICMSST.Text = "0,00";
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void importarPedidoMT3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchPedidoNFePDMT3 frm = new FrmSearchPedidoNFePDMT3())
            {
                frm.ShowDialog();
                var result = frm.Result;

                DialogResult dr = MessageBox.Show("Deseja realmente gerar Nota Fiscal pelo pedido nº " + result + " ?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    if (result > 0)
                    {
                        try
                        {
                            CreaterCursor Cr = new CreaterCursor();
                            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                            Entity = null;
                            Entity2 = null;

                            GetConfiSistema();
                            tabControl1.SelectTab(0);
                            tabControl2.SelectTab(0);

                            //Armazena Dados do Pedido
                            PEDIDOMARCEntity PEDIDOTy = new PEDIDOMARCEntity();
                            PEDIDOMARCProvider PEDIDOP = new PEDIDOMARCProvider();
                            PEDIDOTy = PEDIDOP.Read(result);

                            //Cliente
                            cbCliente.SelectedValue = PEDIDOTy.IDCLIENTE;

                            //CFOP
                            string UFEmpresa = EMPRESAP.Read(1).UF;
                            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", CLIENTEP.Read(Convert.ToInt32(PEDIDOTy.IDCLIENTE)).COD_MUN_IBGE.ToString()));
                            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                            string UFCliente = LIS_MUNICIPIOSColl[0].UF;

                            if (UFEmpresa != UFCliente)
                                cbCFOP.SelectedValue = 4;
                            else
                                cbCFOP.SelectedValue = 2;

                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                            //Produto
                            //Armazena dados Produto do Pedido
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("PEDIDOMARC", "System.Int32", "=", result.ToString()));
                            txtNumPedido.Text = result.ToString();
                            
                            LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();
                            LIS_PRODUTOPEDMARC2Provider LIS_PRODUTOPEDMARC2P = new LIS_PRODUTOPEDMARC2Provider();

                            LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2P.ReadCollectionByParameter(RowRelatorio);

                            foreach (LIS_PRODUTOPEDMARC2Entity item in LIS_PRODUTOPEDMARC2Coll)
                            {
                                cbProduto.SelectedValue = item.IDPRODUTO;
                                txtQuanProduto.Text = Convert.ToDecimal(item.TOTALMT3).ToString("n4");
                                txtValorUnitProd.Text = Convert.ToDecimal(item.VLUNITARIO).ToString("n4");
                                txtInformAddProduto.Text = item.DADOSADICIONAIS;
                                cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                                CalculoTributoAprox(txtNCM.Text);
                                PRODUTONFEP.Save(Entity2);
                                Entity2 = null;
                            }

                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", result.ToString()));

                            LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
                            LIS_PRODUTOSPEDMARCProvider LIS_PRODUTOSPEDMARCP = new LIS_PRODUTOSPEDMARCProvider();
                            
                            LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

                            foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                            {
                                cbProduto.SelectedValue = item.IDPRODUTO;
                                txtQuanProduto.Text = (Convert.ToDecimal(item.QUANTIDADE)).ToString(); ;

                                txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n4");
                                cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                                CalculoTributoAprox(txtNCM.Text);
                                PRODUTONFEP.Save(Entity2);
                                Entity2 = null;
                            }

                            ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                            //Cobrança
                            if (PEDIDOTy.IDFORMAPAGAMENTO != null)
                                cbFormaPagto.SelectedValue = PEDIDOTy.IDFORMAPAGAMENTO;

                            if (PEDIDOTy.IDLOCALCOBRANCA != null)
                                cbLocalCobranca.SelectedValue = PEDIDOTy.IDLOCALCOBRANCA;

                            if (PEDIDOTy.IDFORMAPAGAMENTO != null && PEDIDOTy.IDLOCALCOBRANCA != null)
                            {
                                DialogResult dr2 = MessageBox.Show("Deseja gerar Duplicatas?",
                                ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                                if (dr2 == DialogResult.Yes)
                                    SaveDuplicatas();
                            }

                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                            this.Cursor = Cursors.Default;

                            MessageBox.Show("Nota Fiscal Gerada com sucesso!");

                        }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Não foi possível gerar Nota Fiscal!");
                            MessageBox.Show("Erro Técnico: " + ex.Message);

                        }
                    }

                }
            }
        }

        private void txtOutrosProdutos_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtOutrosProdutos.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtOutrosProdutos.Text))
                {
                    errorProvider1.SetError(txtOutrosProdutos, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtOutrosProdutos.Text);
                    txtOutrosProdutos.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtOutrosProdutos, "");
                }
            }
            else
                txtOutrosProdutos.Text = "0,00";
        }

        private void importarPedidoMatériaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchPedidoNFePDMatPrima frm = new FrmSearchPedidoNFePDMatPrima())
            {
                frm.ShowDialog();
                var result = frm.Result;
                var tipoproduto = frm.TipoProduto; //0 Todos - 1 Produto Final - 2 Matéria Prima

                DialogResult dr = MessageBox.Show("Deseja realmente gerar Nota Fiscal pelo pedido nº " + result + " ?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    if (result > 0)
                    {
                        try
                        {
                            CreaterCursor Cr = new CreaterCursor();
                            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                            Entity = null;
                            Entity2 = null;

                            GetConfiSistema();
                            tabControl1.SelectTab(0);
                            tabControl2.SelectTab(0);

                            //Armazena Dados do Pedido
                            PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                            PEDIDOProvider PEDIDOP = new PEDIDOProvider();
                            PEDIDOTy = PEDIDOP.Read(result);

                            //Cliente
                            cbCliente.SelectedValue = PEDIDOTy.IDCLIENTE;

                            //CFOP
                            string UFEmpresa = EMPRESAP.Read(1).UF;
                            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", CLIENTEP.Read(Convert.ToInt32(PEDIDOTy.IDCLIENTE)).COD_MUN_IBGE.ToString()));
                            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                            string UFCliente = LIS_MUNICIPIOSColl[0].UF;

                            if (UFEmpresa != UFCliente)
                                cbCFOP.SelectedValue = 4;
                            else
                                cbCFOP.SelectedValue = 2;

                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                            //Produto
                            //Armazena dados Produto do Pedido
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", result.ToString()));
                            txtNumPedido.Text = result.ToString();

                            LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                            LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                            if (tipoproduto == 1 && LIS_PRODUTOSPEDIDOMTQColl.Count > 0) // 1 Produto Final
                            {
                                cbProduto.SelectedValue = LIS_PRODUTOSPEDIDOMTQColl[0].IDPRODUTOMASTER;
                                txtQuanProduto.Text = "1";
                                txtValorUnitProd.Text = Convert.ToDecimal(PEDIDOTy.TOTALPEDIDO).ToString("n2");
                                txtInformAddProduto.Text = string.Empty;
                                cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                                CalculoTributoAprox(txtNCM.Text);
                                PRODUTONFEP.Save(Entity2);
                                Entity2 = null;
                            }
                            else // 2 Matéria Prima
                            { 
                                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                                {

                                    if (tipoproduto == 2 )
                                    {
                                        cbProduto.SelectedValue = item.IDPRODUTO;
                                        txtQuanProduto.Text = (Convert.ToDecimal(item.MT2) * Convert.ToDecimal(item.QUANTIDADE)).ToString("n4");
                                        txtValorUnitProd.Text = Convert.ToDecimal(item.VALORMETRO).ToString("n4");
                                        txtInformAddProduto.Text = item.DADOADICIONAIS;
                                        cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                                        CalculoTributoAprox(txtNCM.Text);
                                        PRODUTONFEP.Save(Entity2);
                                        Entity2 = null;
                                    }                              
                                }


                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", result.ToString()));
                                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                                LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

                                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);                              
                                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                                {
                                    cbProduto.SelectedValue = item.IDPRODUTO;
                                    txtQuanProduto.Text = (Convert.ToDecimal(item.QUANTIDADE)).ToString(); ;

                                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n4");
                                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;
                                    CalculoTributoAprox(txtNCM.Text);
                                    PRODUTONFEP.Save(Entity2);
                                    Entity2 = null;
                                }                              
                            }                           

                            ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                            //Cobrança
                            if (PEDIDOTy.IDFORMAPAGAMENTO != null)
                                cbFormaPagto.SelectedValue = PEDIDOTy.IDFORMAPAGAMENTO;

                            if (PEDIDOTy.IDLOCALCOBRANCA != null)
                                cbLocalCobranca.SelectedValue = PEDIDOTy.IDLOCALCOBRANCA;

                            if (PEDIDOTy.IDFORMAPAGAMENTO != null && PEDIDOTy.IDLOCALCOBRANCA != null)
                            {
                                DialogResult dr2 = MessageBox.Show("Deseja gerar Duplicatas?",
                                ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                                if (dr2 == DialogResult.Yes)
                                    SaveDuplicatas();
                            }

                            _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                            this.Cursor = Cursors.Default;

                            MessageBox.Show("Nota Fiscal Gerada com sucesso!");

                        }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Não foi possível gerar Nota Fiscal!");
                            MessageBox.Show("Erro Técnico: " + ex.Message);

                        }
                    }

                }
            }
        }

        private void printDocument3_BeginPrint(object sender, PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void componenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex > 0)
            {
              //  VerificaDebitoCliente(Convert.ToInt32(cbCliente.SelectedValue));

                if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
                {

                    using (FrmBloqueado frm = new FrmBloqueado())
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void duplicarNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (!VerificaPlanos())
            {

            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente duplicar esta NFe?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    _IDNOTAFISCALE = -1;
                    txtChaveAcesso.Text = string.Empty;

                    //Proximo Numero da Nota Fiscal
                    GetNextNF();
                    _FLAGENVIADA = "N";
                    _FLAGCANCELADA = "N";
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    mskDtSaida.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    mkdHoraSaida.Text = DateTime.Now.ToString("HH:mm");
                    _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                    //Salva Produtos
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                    {
                        PRODUTONFEEntity PRODUTONFETy = new PRODUTONFEEntity();
                        PRODUTONFETy = PRODUTONFEP.Read(Convert.ToInt32(item.IDPRODUTONFE));
                        PRODUTONFETy.IDPRODUTONFE = -1;
                        PRODUTONFETy.IDNOTAFISCALE = _IDNOTAFISCALE;
                        PRODUTONFEP.Save(PRODUTONFETy);
                    }

                    Entity = NOTAFISCALEP.Read(_IDNOTAFISCALE);
                    ListaProdutoNotaFiscal(_IDNOTAFISCALE);
                    GridDuplicatasNF(Convert.ToInt32(cbCliente.SelectedValue), txtNotaFiscal.Text);

                    MessageBox.Show("Nota Fiscal Nº " + txtNotaFiscal.Text + " criada com sucesso!");

                    Entity2 = null;
                    Entity = null;

                    tabControl1.SelectTab(1);
                    txtCriterioPesquisa.Focus();

                    btnPesquisa_Click(null, null);
                }
            }
        }

        private void imprimirDanfeEmPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (txtChaveAcesso.Text.TrimEnd().TrimStart() == string.Empty)
            {
                errorProvider1.SetError(txtChaveAcesso, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
            }
            else if (_FLAGCANCELADA.Trim() == "S")
            {
                string Msg = "Nota Cancelada, não é possível imprimir DANFE!";
                errorProvider1.SetError(lblSituacao, Msg);
                MessageBox.Show(Msg);
            }
            else
            {
                  PrintDanfeComponentePDF(2);
            }
        }
        private void PrintDanfeComponentePDF(int TipoImpressao)
        {
            //TipoImpressao
            //1 - direto para impressora default; 2 - PDF; 3 - preview em tela
            try
            {
                string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");

                Boolean NfeLoca = false;
                List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\procNFe");
                foreach (string p in dirs)
                {
                    int LengthLine = p.Length;
                    int pos = p.IndexOf(NFe);
                    if (pos != -1)
                    {
                        
                        NfeLoca = true;//(1 - direto para impressora default; 2 - PDF; 3 - preview em tela
                        string pathPDF = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\PDF\" + NFe+".pdf"; 
                        nfec.nfecsharp nfe = new nfec.nfecsharp();

                        if (!nfe.NFeDanfe(p, pathPDF, TipoImpressao, false))
                        {
                            MessageBox.Show("Erro ao Imprimir DANFE!");
                        }
                        else
                        {
                            //Atualiza Lista da Pesquisa
                            btnPesquisa_Click(null, null);
                            Util.ExibirMSg("Aguarde... DANFE em pdf abrindo...", "Blue");
                        }
                      

                        _FLAGENVIADA = "S";
                        _FLAGCANCELADA = "N";
                        NOTAFISCALEP.Save(Entity);

                        if (TipoImpressao == 2)
                        {
                            if (File.Exists(pathPDF))
                            {
                                ExibirMensagem = false;
                                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(pathPDF);
                            }
                            else
                            {
                                MessageBox.Show("Arquivo: " + pathPDF + " não localizado!");
                            }
                        }
                            
                    }
                }

                if (!NfeLoca)
                {
                    if (!ExibirMensagem)
                        MessageBox.Show("Não foi possível localizar arquivo processado!");
                    else
                        ExibirMsgNfeStatus("Não foi possível localizar arquivo processado!", Color.Red);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void imprimirDanfeEmPDFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (txtChaveAcesso.Text.TrimEnd().TrimStart() == string.Empty)
            {
                errorProvider1.SetError(txtChaveAcesso, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
            }
            else if (_FLAGCANCELADA.Trim() == "S")
            {
                string Msg = "Nota Cancelada, não é possível imprimir DANFE!";
                errorProvider1.SetError(lblSituacao, Msg);
                MessageBox.Show(Msg);
            }
            else
                imprimirDanfeEmPDFToolStripMenuItem_Click(null, null);
        }

        private void txtBaseST_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "BCST = Valor mercadoria + frete + IPI + outras despesas + margem de lucro";
            CalculoICMSST();
        }

        private void CalculoICMSST()
        {
            try
            {
                PRODUTOSEntity PRODUTOS_Calc_ST = new PRODUTOSEntity();
                PRODUTOS_Calc_ST = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                if (PRODUTOS_Calc_ST != null && PRODUTOS_Calc_ST.FLAGICMSST.Trim() == "S")
                {

                    //ICMS da operação própria
                    decimal ICMS = (Convert.ToDecimal(txtVlTotal.Text) * Convert.ToDecimal(PRODUTOS_Calc_ST.ICMS)) / 100;
                    
                    //BCST = (Valor mercadoria + frete + IPI + outras despesas) + margem de lucro
                    decimal ValorIPI = ((Convert.ToDecimal(txtVlTotal.Text) * Convert.ToDecimal(PRODUTOS_Calc_ST.IPI)) / 100);
                    decimal MargemLucro = Convert.ToDecimal(PRODUTOS_Calc_ST.PORCMARGEMLUCRO);
                    decimal TotalGeralST = Convert.ToDecimal(txtVlTotal.Text) + Convert.ToDecimal(txtVlFreteProd.Text) + ValorIPI + Convert.ToDecimal(txtOutrosProdutos.Text);
                    TotalGeralST = TotalGeralST + ((TotalGeralST * MargemLucro) /100 );
                    txtBaseST.Text = TotalGeralST.ToString("n2");

                     //ICMS ST
                    decimal ICMSST = (Convert.ToDecimal(TotalGeralST) * Convert.ToDecimal(PRODUTOS_Calc_ST.ICMS)) / 100;
                    txtICMSST.Text = (Convert.ToDecimal(ICMSST) - Convert.ToDecimal(ICMS)).ToString("n2");
                }
                else
                {
                    txtBaseST.Text = "0,00";
                    txtICMSST.Text = "0,00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                MessageBox.Show("Erro no cálculo do ICMS ST");
            }
        }

        private void txtICMSST_Enter(object sender, EventArgs e)
        {
            CalculoICMSST();
        }

        private void txtVlFreteProd_Leave(object sender, EventArgs e)
        {
            CalculoICMSST();
        }

        private void txtOutrosProdutos_Leave(object sender, EventArgs e)
        {
            CalculoICMSST();
        }

        private void txtAlqICMSST_Validating(object sender, CancelEventArgs e)
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void vendasPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaporCidadeNFe frm = new FrmVendaporCidadeNFe())
            {
                frm.ShowDialog();
            }
        }

        private void vendasDeProdutoPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoVendaCidadeNFe frm = new FrmProdutoVendaCidadeNFe())
            {
                frm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Nota Fiscal";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void importarOrdemDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CFOP_Digitado = InputBox("Digite o CFOP para emissão desta NF-e ", ConfigSistema1.Default.NomeEmpresa, "5102");

            if (CFOP_Digitado.Trim() == string.Empty)
            {
                MessageBox.Show("CFOP Inválido");
            }
            else if (!LocalizarCFOP(CFOP_Digitado))
            {
                MessageBox.Show("CFOP não localizado!");
            }
            else if (!VerificaPlanos())
            {

            }
            else
            {
                using (FrmSearchOS frm = new FrmSearchOS())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    DialogResult dr = MessageBox.Show("Deseja realmente gerar Nota Fiscal pela Ordem de Serviço nº " + result + " ?",
                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        if (result > 0)
                        {
                            try
                            {

                                CreaterCursor Cr = new CreaterCursor();
                                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                                Entity = null;
                                Entity2 = null;
                                GetConfiSistema();
                                tabControl1.SelectTab(0);
                                tabControl2.SelectTab(0);

                                //Armazena Dados do Pedido
                                ORDEMSERVICOSFECHEntity ORDEMSERVICOSFECHTy = new ORDEMSERVICOSFECHEntity();
                                ORDEMSERVICOSFECHProvider ORDEMSERVICOSFECHP = new ORDEMSERVICOSFECHProvider();
                                ORDEMSERVICOSFECHTy = ORDEMSERVICOSFECHP.Read(result);

                                //Cliente
                                cbCliente.SelectedValue = ORDEMSERVICOSFECHTy.IDCLIENTE;

                                //CFOP
                                string UFEmpresa = EMPRESAP.Read(1).UF;
                                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", CLIENTEP.Read(Convert.ToInt32(ORDEMSERVICOSFECHTy.IDCLIENTE)).COD_MUN_IBGE.ToString()));
                                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                                string UFCliente = LIS_MUNICIPIOSColl[0].UF;

                                if (UFEmpresa != UFCliente)
                                    cbCFOP.SelectedValue = 4;
                                else
                                    cbCFOP.SelectedValue = 2;

                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                                //Produto
                                //Armazena dados Produto do Pedido
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", result.ToString()));

                                LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
                                LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
                                LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                                foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                                {
                                    cbProduto.SelectedValue = item.IDPRODUTO;
                                    txtQuanProduto.Text = item.QUANTIDADE.ToString();
                                    txtValorUnitProd.Text = Convert.ToDecimal(item.VALORUNITARIO).ToString("n2");
                                    cbCFOPProduto.SelectedValue = cbCFOP.SelectedValue;

                                    PRODUTONFEP.Save(Entity2);
                                    Entity2 = null;
                                }

                                ListaProdutoNotaFiscal(_IDNOTAFISCALE);

                                //Cobrança
                                if (ORDEMSERVICOSFECHTy.IDFORMAPAGAMENTO != null)
                                    cbFormaPagto.SelectedValue = ORDEMSERVICOSFECHTy.IDFORMAPAGAMENTO;


                                _IDNOTAFISCALE = NOTAFISCALEP.Save(Entity);

                                this.Cursor = Cursors.Default;

                                MessageBox.Show("Nota Fiscal Gerada com sucesso!");

                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Não foi possível gerar Nota Fiscal!");
                                MessageBox.Show("Erro Técnico: " + ex.Message);
                            }
                        }

                    }
                }
            }
        }

        private void autorizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {


            try
            {
                // WS Autorização ( Arquivo da pasta lote ).
                  nfec.nfecsharp nfe = new nfec.nfecsharp();

                  CreaterCursor Cr = new CreaterCursor();
                  this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                if (File.Exists(ConfigSistema1.Default.PathInstall + @"\nfe\lotes\" + _ARQUIVOLOTE.ToString().PadLeft(12, '0') + "-env-lot.xml"))
                {
                    string MsgAutorizacao = nfe.NfeAutorizacao(ConfigSistema1.Default.PathInstall + @"\nfe\lotes\" + _ARQUIVOLOTE.ToString().PadLeft(12, '0') + "-env-lot.xml");

                    int localRec = MsgAutorizacao.IndexOf("sucesso");//Pega somente o numero do recibo
                      if (localRec != -1)
                      {
                          this.Cursor = Cursors.Default;
                          NfeAutorizacao = true;                          
                          int tamanhotexto = MsgAutorizacao.Length - localRec - 1;
                          _RECIBONFE = MsgAutorizacao.Substring(localRec + 1, tamanhotexto);
                          _RECIBONFE = Util.RetiraLetras(_RECIBONFE);
                          txtReciboRecp.Text = _RECIBONFE;
                        
                          NOTAFISCALEP.Save(Entity);

                          if (!ExibirMensagem)
                              MessageBox.Show(MsgAutorizacao);
                          else
                              Util.ExibirMSg(MsgAutorizacao, "Blue");                         
                          
                      }
                      else
                      {
                          this.Cursor = Cursors.Default;
                          MessageBox.Show(MsgAutorizacao);
                          NfeAutorizacao = false;                       
                          _RECIBONFE = string.Empty;
                          txtReciboRecp.Text = _RECIBONFE;
                          NOTAFISCALEP.Save(Entity);
                          MessageBox.Show("Recibo não localizado na Autorização (nfe.NfeAutorizacao)!");
                      }

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Arquivo não localizado: " + ConfigSistema1.Default.PathInstall + @"\nfe\lotes\"+ _ARQUIVOLOTE.ToString().PadLeft(12, '0') + "-env-lot.xml");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro em Autorização!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string PastaOrigem = ConfigSistema1.Default.PathInstall;
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("http://www.nfe.fazenda.gov.br/portal/disponibilidade.aspx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ChaveNFe = txtChaveAcesso.Text;
                string Chave = ChaveNFe.Replace("N", "").Replace("F", "").Replace("e", "");
                string[] Lista_Arquivos = Directory.GetFiles(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe");
                string arquivocce = Chave+".pdf";
                    

                if (Lista_Arquivos.Length > 0)
                {
                    foreach (string Arquivo in Lista_Arquivos)
                    {
                        int pos = Arquivo.IndexOf(Chave);
                        if (pos != -1)
                            arquivocce = Arquivo.ToString();
                    }
                }    

                nfec.nfecsharp nfe = new nfec.nfecsharp();
                if (!nfe.ImpCCe(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\" + Chave + "-CCe.xml", arquivocce, 2))
                {
                     MessageBox.Show("Erro ao imprimir a Carta de Correção:");

                     openFileDialog1.InitialDirectory = Application.StartupPath + @"\nfe\arquivos\CCe";
                     openFileDialog1.Filter = "NF-e (*.xml)|";
                     openFileDialog1.Title = "Arquivo de Carta de Correção";


                     if (openFileDialog1.ShowDialog() == DialogResult.OK)
                     {
                         if (!nfe.ImpCCe(openFileDialog1.FileName, arquivocce, 2))
                             MessageBox.Show("Erro ao imprimir a Carta de Correção do arquivo selecionado");
                     }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
           

        }

        private void imprimirDanfeEmPDFToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\nfe\\arquivos\\assinado";
            openFileDialog1.Filter = "NF-e (*.xml)|";
            openFileDialog1.Title = "Localizar NF-e";

            string NFe = txtChaveAcesso.Text.Replace("N", "").Replace("F", "").Replace("e", "");
            string pathPDF = ConfigSistema1.Default.PathInstall + @"\\nfe\\arquivos\\PDF\" + NFe + ".pdf"; 

            nfec.nfecsharp nfe = new nfec.nfecsharp();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                nfe.NFeDanfe(openFileDialog1.FileName, pathPDF, 2, false);
                if (File.Exists(pathPDF))
                {
                    System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(pathPDF);
                }

            }
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
          
        }

        private void alterarStatusNotaFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_NOTAFISCALEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmAlterarStatusNFe frm = new FrmAlterarStatusNFe())
                {
                    frm.LIS_NOTAFISCALEColl = LIS_NOTAFISCALEColl;
                    frm.ShowDialog();
                    btnPesquisa_Click(null, null);
                }
            }
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            imprimirDanfeEmPDFToolStripMenuItem1_Click(null, null);
        }

        private void modelo1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void sICOOBCNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void arquivosDeRemessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRemessaBanco().ShowDialog();
        }

        private void cNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void cNAB240ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Duplicatas não selecionadas!");
            }
            else
            {
                Util.ArquivoRemessaSICOOB_CNAB240(LIS_DUPLICATARECEBERColl, "txt");
            }
        }

        private void dowloandDaNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               if(_FLAGENVIADA.Trim() == "S" && _FLAGCANCELADA.Trim() == "N")
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Title = "Selecione a pasta";
                    openFileDialog1.CheckFileExists = false;

                    openFileDialog1.FileName = "[Obter Pasta…]";
                    openFileDialog1.Filter = "Folders|no.files";
                    openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string pathNFe = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                        nfec.nfecsharp nfe = new nfec.nfecsharp();
                        string chNFe = Util.RetiraLetras(txtChaveAcesso.Text);
                        nfe.DonwloadNFe(chNFe, pathNFe);
                    }
                }
                else
                {
                    MessageBox.Show("Nota Fiscal não foi enviada!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fazer Download da NFe!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void button5_Click_3(object sender, EventArgs e)
        {
          //  string caminhoArquivoxml = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\" + txtChaveAcesso.Text + ".xml";
          //// AlteraXml(caminhoArquivoxml, "<emit>", "<fone>", "<jow></jow>");
          ////  AlteraXml(caminhoArquivoxml, "<prod>", "<indTot>", "<jow></jow>");

          //  PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
          //  PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
          //  foreach (var item in LIS_PRODUTONFEColl)
          //  {
          //      PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

          //      if (CONFISISTEMATy.FLAGCODREFNFE.Trim() == "S")
          //          AlteraXmlProduto(caminhoArquivoxml, "<cProd>" + PRODUTOSTy.CODPRODUTOFORNECEDOR, "<jow></jow>");
          //      else
          //          AlteraXmlProduto(caminhoArquivoxml, "<cProd>" + PRODUTOSTy.IDPRODUTO, "<jow></jow>");
                
          //  }

          // // AlteraXml(caminhoArquivoxml, "<emit>", "<fone>", "<jow></jow>");
          //  //AlteraXml(caminhoArquivoxml,  "<indTot>", "<jow></jow>");
          //  //AlteraXml(caminhoArquivoxml, "<det nItem="2">", "<indTot>", "<jow></jow>");
            
        }

        private void AlteraXmlProduto(string caminhoArquivoxml, string PTag, string AddTag)
        {
            try
            {
                Boolean achou = false;
                using (StreamReader lendo = new StreamReader(caminhoArquivoxml))
                {
                    //Lendo arquivo e atribuindo em um array de string
                    string[] arquivo = File.ReadAllLines(caminhoArquivoxml);
                    int linha = File.ReadAllLines(caminhoArquivoxml).GetLength(0) - 1;

                    while (lendo.Peek() != -1)
                    {
                        //Percorre o array
                        for (int i = 0; i <= linha; i++)
                        {
                            if (arquivo[i].ToString().IndexOf(PTag) != -1)

                               // for (int h = 0; h <= linha; h++)
                                {
                                    //if (arquivo[h].ToString().IndexOf(STag) != -1)
                                    {
                                        //gravando o conteúdo por cima do arquivo,porem trava nessa linha falando que ja esta em uso como faço para editar a linha com o nome gustavo?
                                        arquivo[i] += AddTag;

                                        //Fecha o arquivo xml aberto
                                        lendo.Close();

                                        //Altera o arquivo xml
                                        System.IO.File.WriteAllLines(caminhoArquivoxml, arquivo);

                                        achou = true;
                                        break;
                                    }
                                }

                            if (achou)
                                break;
                        }

                        // if (achou)
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AlteraXml(string caminhoArquivoxml, string PTag, string STag, string AddTag)
        {
            try
            {
                Boolean achou = false;
                using (StreamReader lendo = new StreamReader(caminhoArquivoxml))
                {
                    //Lendo arquivo e atribuindo em um array de string
                    string[] arquivo = File.ReadAllLines(caminhoArquivoxml);
                    int linha = File.ReadAllLines(caminhoArquivoxml).GetLength(0) - 1;

                    while (lendo.Peek() != -1)
                    {
                        //Percorre o array
                        for (int i = 0; i <= linha; i++)
                        {
                            if (arquivo[i].ToString().IndexOf(PTag) != -1)

                                for (int h = 0; h <= linha; h++)
                                {
                                    if (arquivo[h].ToString().IndexOf(STag) != -1)
                                    {
                                        //gravando o conteúdo por cima do arquivo,porem trava nessa linha falando que ja esta em uso como faço para editar a linha com o nome gustavo?
                                        arquivo[h] += AddTag;
                                        
                                        //Fecha o arquivo xml aberto
                                        lendo.Close();

                                        //Altera o arquivo xml
                                        System.IO.File.WriteAllLines(caminhoArquivoxml, arquivo);

                                        achou = true;
                                        break;
                                    }
                                }

                             if (achou)
                                break;
                        }

                        // if (achou)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void duplicatasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void carnêDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                using (FrmCarne frm = new FrmCarne())
                {
                    frm.LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERColl;
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                Util.ExibirMSg("Não Existem Duplicatas Lançadas Para Este Pedido", "Red");
            }
        }

        private void txtPesquisaRapida_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NFISCALE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEVENDEDOR", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_NOTAFISCALEColl;

                    lblTotalPesquisa.Text = LIS_NOTAFISCALEColl.Count.ToString();

                    if (LIS_NOTAFISCALEColl.Count > 0)
                        ColorGrid();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void DGDadosProduto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_4(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtDescontoProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void registro50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmResumoSintegraReg50 frm = new FrmResumoSintegraReg50())
                {

                    frm.ShowDialog();
                }
            
        }

        private void ativarEmpresaEmissoraDeNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEmpresaEmissora frm = new FrmEmpresaEmissora())
            {
                frm.ShowDialog();
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);
                if (EMPRESATy != null)
                {
                    this.Text = "Nota Fiscal Eletronica - NFe " + EMPRESATy.NOMEFANTASIA + " " + EMPRESATy.CNPJCPF;
                }
             }
        }

        private void visualizaDANFESemValorFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTAFISCALE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    //printDocument6.DefaultPageSettings.PaperSize = new
                    // System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                    objPrintPreview.Document = printDocument2;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void enviarEmailNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enviarEmailToolStripMenuItem_Click(null, null);
        }

        private void vendasPorCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaCFOP frm = new FrmVendaCFOP())
            {
                frm.DataInicial = msktDataInicial.Text;
                frm.DataFinal = msktDataFinal.Text;
                frm.ShowDialog();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
                 }

        private void estoqueLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEstoqueLote frm = new FrmEstoqueLote())
            {
                frm._NumeroDoc = "NF"+txtNotaFiscal.Text;
                frm.ShowDialog();
            }
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }
    }
}