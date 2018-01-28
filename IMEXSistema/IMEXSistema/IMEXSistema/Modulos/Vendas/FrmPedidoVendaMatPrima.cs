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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Operacional;
using System.IO;
using CDSSoftware;
using BmsSoftware.UI;
using VVX;
using BmsSoftware.Modulos.Relatorio;
using System.Diagnostics;
using BmsSoftware.Modulos.Servicos;
using System.Drawing.Printing;
using System.Threading;
using BmsSoftware.Classes.BMSworks.UI;
using System.Net.Mail;
using System.Net;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPedidoVendaMatPrima : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        Utility Util = new Utility();        
        public Boolean FLAGPEDSIMPLES = false;   

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();       

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        MATERIALCollection MATERIALColl = new MATERIALCollection();
        LIS_PRODUTOSPEDIDOMTCollection LIS_PRODUTOSPEDIDOMTColl = new LIS_PRODUTOSPEDIDOMTCollection();
        PRODUTOSCollection PRODUTOSMTColl = new PRODUTOSCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
        CORCollection CORColl = new CORCollection();
        CORCollection COR2Coll = new CORCollection();
        LIS_PRODUTOCOMPOSICAOCollection LIS_PRODUTOCOMPOSICAOColl = new LIS_PRODUTOCOMPOSICAOCollection();
        ARQUIVOPEDIDOCollection ARQUIVOPEDIDOColl = new ARQUIVOPEDIDOCollection();
        PRODUTOSCollection PRODUTOSFinalColl = new PRODUTOSCollection();
        
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
        PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        PRODUTOSPEDIDOMTProvider PRODUTOSPEDIDOMTP = new PRODUTOSPEDIDOMTProvider();
        LIS_PRODUTOSPEDIDOMTProvider LIS_PRODUTOSPEDIDOMTP = new LIS_PRODUTOSPEDIDOMTProvider();
        MATERIALProvider MATERIALP = new MATERIALProvider();
        PRODUTOSPEDIDOMTQProvider PRODUTOSPEDIDOMTQP = new PRODUTOSPEDIDOMTQProvider();
        LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
        CORProvider CORP = new CORProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        LIS_PRODUTOCOMPOSICAOProvider LIS_PRODUTOCOMPOSICAOP = new LIS_PRODUTOCOMPOSICAOProvider();
        ARQUIVOPEDIDOProvider ARQUIVOPEDIDOP = new ARQUIVOPEDIDOProvider();
        

        string CasasDecimais = string.Empty;
        Boolean _FLAGORCAMENTO = false;
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmPedidoVendaMatPrima()
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

       public int _IDPEDIDO = -1;
        public PEDIDOEntity Entity
        {
            get
            {
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text); 
                int IDSTATUS  = Convert.ToInt32(cbStatus.SelectedValue); ;
                string PRAZOENTREGA = txtPrazoEntrega.Text; 
                
                int? IDTRANSPORTES = null;
                if(cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                int? IDVENDEDOR = null;
                if (cbFuncionario.SelectedIndex > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

                
                if(!ValidacoesLibrary.ValidaTipoDecimal(txtValComissao.Text))
                    txtValComissao.Text = "0,00";

                decimal VALORCOMISSAO = Convert.ToDecimal(txtValComissao.Text);

                string OBSERVACAO = txtObservacao.Text;

                if (txtTotalProdAdicional.Text == string.Empty)
                    txtTotalProdAdicional.Text = "0,00";
                decimal TOTALPRODUTOS = Convert.ToDecimal(txtTotalProdAdicional.Text);

                if (txtTotalIPI.Text == string.Empty)
                    txtTotalIPI.Text = "0,00";
                decimal TOTALIMPOSTOS = Convert.ToDecimal(txtTotalIPI.Text);

                if (txtPorcDesconto.Text == string.Empty)
                    txtPorcDesconto.Text = "0,00";
                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconto.Text);

                if (txtTotalDesconto.Text == string.Empty)
                    txtTotalDesconto.Text = "0,00";
                decimal VALORDESCONTO = Convert.ToDecimal(txtTotalDesconto.Text);

                if (txtPorcAcrescimo.Text == string.Empty)
                    txtPorcAcrescimo.Text = "0,00";
                decimal PORACRESCIMO = Convert.ToDecimal(txtPorcAcrescimo.Text);

                if (txtTotalAcrescimo.Text == string.Empty)
                    txtTotalAcrescimo.Text = "0,00";
                decimal VALORACRESCIMO = Convert.ToDecimal(txtTotalAcrescimo.Text);

                if (txtTotalPedido.Text == string.Empty)
                    txtTotalPedido.Text = "0,00";
                decimal TOTALPEDIDO = Convert.ToDecimal(txtTotalPedido.Text);

                int? IDFORMAPAGAMENTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                    IDFORMAPAGAMENTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                if (txtValorPago.Text == string.Empty)
                    txtValorPago.Text = "0,00";
                decimal VALORPAGO = Convert.ToDecimal(txtValorPago.Text);

                if (txtValorDev.Text == string.Empty)
                    txtValorDev.Text = "0,00";
                decimal VALORDEVEDOR = Convert.ToDecimal(txtValorDev.Text);
 
                int? IDLOCALCOBRANCA = null;
                if (cbLocalCobranca.SelectedIndex > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                int? IDCENTROCUSTOS = null;
                if (cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

                string FLAGPRODIMPRESSAO = chkDetalProdFinal.Checked ? "S" : "N";
                string PRODUTOFINAL = txtDetalhesProdFinal.Text;
                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
                string NREFERENCIA = txtNumReferencia.Text;
                string FLAGVLMETRO = chkValorMetro.Checked ? "S" : "N";
                string OBSANEXO = txtObsAnexo.Text.TrimEnd();

                DateTime? DataEntrega = null;
                if (dtDataEntrega.Checked)
                    DataEntrega = Convert.ToDateTime(dtDataEntrega.Value);

                  string FLAGTELABLOQUEADA = chkTelaBloqueada.Checked ? "S" : "N"; 
                    DateTime? DATAVECTO = null;
                    int? IDSUPERVISOR = null;
                int? IDMESA = null;              

                return new PEDIDOEntity(_IDPEDIDO, IDCLIENTE, DTEMISSAO, IDSTATUS, PRAZOENTREGA,
                                        IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO, OBSERVACAO,
                                        TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                        VALORDESCONTO, PORACRESCIMO, VALORACRESCIMO,
                                        TOTALPEDIDO, IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR,
                                        IDLOCALCOBRANCA, IDCENTROCUSTOS, FLAGPRODIMPRESSAO, PRODUTOFINAL,
                                        FLAGORCAMENTO, NREFERENCIA, FLAGVLMETRO, OBSANEXO, DataEntrega, FLAGTELABLOQUEADA,
                                        0, 0, 0, 0, DATAVECTO, IDSUPERVISOR, IDMESA);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDO = value.IDPEDIDO;
                    ListaArquivoPedido(_IDPEDIDO);
                    ListaProdutoPedidoMTQ(_IDPEDIDO);
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                    cbCliente.SelectedValue = value.IDCLIENTE;

                    txtStatusFinaCliente.Text = string.Empty;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");
                    cbStatus.SelectedValue = value.IDSTATUS;
                    txtPrazoEntrega.Text = value.PRAZOENTREGA;

                    if (value.IDTRANSPORTES != null)
                        cbTransportadora.SelectedValue = value.IDTRANSPORTES;
                    else
                        cbTransportadora.SelectedIndex = 0;

                    if (value.IDVENDEDOR != null)
                        cbFuncionario.SelectedValue = value.IDVENDEDOR;
                    else
                        cbFuncionario.SelectedIndex = 0;

                    txtValComissao.Text = Convert.ToDecimal(value.VALORCOMISSAO).ToString("n2");
                    txtObservacao.Text = value.OBSERVACAO;

                    txtTotalProdAdicional.Text = Convert.ToDecimal(value.TOTALPRODUTOS).ToString("n2");
                    txtTotalIPI.Text = Convert.ToDecimal(value.TOTALIMPOSTOS).ToString("n2");
                    txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtTotalDesconto.Text = Convert.ToDecimal(value.VALORDESCONTO).ToString("n2");
                    txtPorcAcrescimo.Text = Convert.ToDecimal(value.PORCACRESCIMO).ToString("n2"); ;
                    txtTotalAcrescimo.Text = Convert.ToDecimal(value.VALORACRESCIMO).ToString("n2");
                    txtTotalPedido.Text = Convert.ToDecimal(value.TOTALPEDIDO).ToString("n2");
                    txtTotalFinanceiro.Text = Convert.ToDecimal(value.TOTALPEDIDO).ToString("n2");


                    if (value.IDFORMAPAGAMENTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGAMENTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    txtValorPago.Text = Convert.ToDecimal(value.VALORPAGO).ToString("n2");
                    txtValorDev.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");

                    if (value.IDLOCALCOBRANCA != null)
                       cbLocalCobranca.SelectedValue = value.IDLOCALCOBRANCA;
                    else
                        cbLocalCobranca.SelectedIndex = 0;


                    if (value.IDCENTROCUSTOS != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTOS;
                    else
                        cbCentroCusto.SelectedIndex = 0;


                    chkDetalProdFinal.Checked = value.FLAGPRODIMPRESSAO == "S" ? true : false;
                    txtDetalhesProdFinal.Enabled = chkDetalProdFinal.Checked;
                    txtDetalhesProdFinal.Text = value.PRODUTOFINAL;

                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S" ? true : false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = value.NREFERENCIA;

                    chkValorMetro.Checked = value.FLAGVLMETRO.TrimEnd() == "S" ? true : false;
                    txtObsAnexo.Text = value.OBSANEXO.TrimEnd();

                    int TotalCaracte = txtObsAnexo.MaxLength;
                    lblCaractere.Text = "Total de caractere restante: " + (TotalCaracte - txtObsAnexo.Text.Length).ToString();

                    if (value.DATAENTREGA != null)
                    {
                        dtDataEntrega.Text = Convert.ToDateTime(value.DATAENTREGA).ToString("dd/MM/yyyy");
                        dtDataEntrega.Checked = true;

                    }
                    else
                        dtDataEntrega.Checked = false;

                    if (value.FLAGTELABLOQUEADA != null && value.FLAGTELABLOQUEADA.Trim() == "S")
                    {
                        chkTelaBloqueada.Text = "Tela Bloqueada";
                        chkTelaBloqueada.ForeColor = System.Drawing.Color.Red;
                        chkTelaBloqueada.Checked = true;
                    }
                    else
                    {
                        chkTelaBloqueada.Text = "Tela Desbloqueada";
                        chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;
                        chkTelaBloqueada.Checked = false;
                    }
                    

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPEDIDO = -1;
                    ListaProdutoPedidoMTQ(_IDPEDIDO);
                    ListaArquivoPedido(_IDPEDIDO);
                    txtNPedido.Text = string.Empty;

                    //Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);

                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbStatus.SelectedIndex = 0;
                    txtPrazoEntrega.Text = string.Empty;
                    cbTransportadora.SelectedIndex = 0;
                    cbFuncionario.SelectedIndex = 0;
                    txtValComissao.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    txtStatusFinaCliente.Text = string.Empty;
                    txtStatusFinaCliente.BackColor = System.Drawing.Color.White;
                    txtTotalProdAdicional.Text = "0,00";
                    txtTotalIPI.Text = "0,00";
                    txtPorcDesconto.Text = "0,00";
                    txtTotalDesconto.Text = "0,00";
                    txtPorcAcrescimo.Text = "0,00";
                    txtTotalAcrescimo.Text = "0,00";
                    txtTotalPedido.Text = "0,00";
                    txtTotalFinanceiro.Text = "0,00";
                    txtValComissao.Text = "0,00";
                    txtValorPago.Text = "0,00";
                    txtValorDev.Text = "0,00";

                    cbFormaPagto.SelectedIndex = 0;

                    //Preenche Mensagem Salvo na configuração do Sistema
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    txtObservacao.Text = CONFISISTEMAP.Read(1).MSGPEDIDO;

                    cbLocalCobranca.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;

                    chkDetalProdFinal.Checked = false;
                    txtDetalhesProdFinal.Enabled = chkDetalProdFinal.Checked;
                    txtDetalhesProdFinal.Text = string.Empty;
                    rdOrcamento.Checked = false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = string.Empty;
                    chkValorMetro.Checked = true;
                    txtObsAnexo.Text = string.Empty;
                    lblCaractere.Text = "Total de caractere restante: 0";


                    chkTelaBloqueada.Checked = false;
                    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;

                    dtDataEntrega.Checked = false;
                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODPEDIDO = -1;
        public PRODUTOSPEDIDOEntity Entity2
        {
            get
            {
              int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
              decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
              decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
              decimal VALORTOTAL = Convert.ToDecimal(txtValorMtLinearTotal.Text);
              decimal COMISSAO = 0;

              //"S" para comissão sobre o total o pedido
              //"N" para comissão pelo total dos produto
              if (FLAGCOMISSAO == "N")
              {
                  decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                  COMISSAO = (VALORTOTAL * PorcComissao) / 100;
              }

              int? IDCOR = null;
                if(Convert.ToInt32(cbCor.SelectedValue) > 0)
                    IDCOR = Convert.ToInt32(cbCor.SelectedValue);

                decimal? TOTALMT = 0;
                string FLAGEXIBIR = chkExibirProd.Checked ? "N" : "S";

                string DADOSADICIONAIS = txtDadosAdicionais1.Text;

                decimal ALTURA  = 0;
                decimal LARGURA = 0;

                int? IDAMBIENTE = null;
                if (Convert.ToInt32(cbAmbiente.SelectedValue) > 0)
                    IDAMBIENTE = Convert.ToInt32(cbAmbiente.SelectedValue);

                int? IDPRODUTOMASTER = null;
                if (Convert.ToInt32(cbProdutoFinal.SelectedValue) > 0)
                    IDPRODUTOMASTER = Convert.ToInt32(cbProdutoFinal.SelectedValue);

                decimal BUSTO = 0;
                decimal CINTURA = 0;
                decimal QUADRIL = 0;
                decimal COLARINHO = 0;
                decimal MANGA = 0;
                decimal BARRA = 0;

                return new PRODUTOSPEDIDOEntity(_IDPRODPEDIDO, _IDPEDIDO, IDPRODUTO,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO, IDCOR, TOTALMT,
                                                FLAGEXIBIR, DADOSADICIONAIS, ALTURA, LARGURA,
                                                IDAMBIENTE, IDPRODUTOMASTER,0,0,
                                                BUSTO, QUADRIL, COLARINHO, MANGA, BARRA, CINTURA, null);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODPEDIDO = value.IDPRODPEDIDO;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n4");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n4");

                    if (value.IDCOR != null)
                        cbCor.SelectedValue = value.IDCOR;
                    else
                        cbCor.SelectedValue = -1;

                    chkExibirProd.Checked = value.FLAGEXIBIR.TrimEnd() == "N" ? true : false;
                    txtDadosAdicionais1.Text = value.DADOSADICIONAIS;

                    if(value.IDAMBIENTE != null)
                        cbAmbiente.SelectedValue = value.IDAMBIENTE;
                    else
                        cbAmbiente.SelectedValue = -1;

                    if (value.IDPRODUTOMASTER != null)
                        cbProdutoFinal.SelectedValue = value.IDPRODUTOMASTER;
                    else
                        cbProdutoFinal.SelectedValue = -1;
                    


                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDIDO = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = "1,00";
                    txtValorUnitProd.Text = "0,0000";
                    txtValorMtLinearTotal.Text = "0,00";

                    chkExibirProd.Checked = false;
                    cbCor.SelectedValue = -1;
                    txtDadosAdicionais1.Text = string.Empty;                   

                    errorProvider1.Clear();
                }
            }
        }

        int _IDARQUIVOPEDIDO = -1;
        byte[] _ArquivoPDF = null;
        public ARQUIVOPEDIDOEntity Entity3
        {
            get
            {
                string NOME = txtNomeImagem.Text;
                string TIPO = lblNameFile.Text;

                return new ARQUIVOPEDIDOEntity(_IDARQUIVOPEDIDO, _IDPEDIDO, NOME, TIPO, _ArquivoPDF);
            }
            set
            {
                if (value != null)
                {

                }
                else
                {
                    txtNomeImagem.Text = string.Empty;
                    lblNameFile.Text = "Nenhum Arquivo Selecionado";
                    _IDARQUIVOPEDIDO = -1;
                    _ArquivoPDF = null;
                }
            }
        }

      

        int _IDPRODUTOSPEDIDOMTQ = -1;
        
        public PRODUTOSPEDIDOMTQEntity Entity6
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProdutoMTQ.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuantMTQ.Text);
                decimal ALTURA = Convert.ToDecimal(txtAlturaMTQ.Text);
                decimal LARGURA = Convert.ToDecimal(txtLarguraMTQ.Text);
                decimal MT2 = Convert.ToDecimal(txtTotalMTQ.Text);
                decimal VALORMETRO = Convert.ToDecimal(txtVlMtrMTQ.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtvalorunitMTQ.Text);
                decimal VALORTOTAL = Convert.ToDecimal(txtVlTotalMTQ.Text);
                decimal COMISSAO = 0;

                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                {
                    decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                    COMISSAO = (VALORTOTAL * PorcComissao) / 100;
                }
                txtValComissao.Text += COMISSAO;

                int? IDCOR = null;
                if(Convert.ToInt32(cbCor2.SelectedValue) > 0)
                    IDCOR = Convert.ToInt32(cbCor2.SelectedValue);

                string FLAGEXIBIR = chkExibirProd2.Checked ? "N" : "S";
                string DADOSADICIONAIS = txtDadosAdicionais2.Text;


                int? IDAMBIENTE = null;
                if (Convert.ToInt32(cbambiente2.SelectedValue) > 0)
                    IDAMBIENTE = Convert.ToInt32(cbambiente2.SelectedValue); 
              
                int? IDPRODUTOMASTER = null;
                if (Convert.ToInt32(cbProdutoFinal.SelectedValue) > 0)
                    IDPRODUTOMASTER = Convert.ToInt32(cbProdutoFinal.SelectedValue);

                decimal PORCPERDA = Convert.ToDecimal(txtPorcPerda.Text);
                decimal TOTALPERDA = Convert.ToDecimal(txtPerdaMT2.Text);

                return new PRODUTOSPEDIDOMTQEntity(_IDPRODUTOSPEDIDOMTQ, _IDPEDIDO, IDPRODUTO,
                                                  QUANTIDADE, ALTURA, LARGURA, MT2, VALORMETRO,
                                                  VALORUNITARIO, VALORTOTAL, COMISSAO, IDCOR,
                                                  FLAGEXIBIR, DADOSADICIONAIS, IDAMBIENTE, IDPRODUTOMASTER, PORCPERDA, TOTALPERDA);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOSPEDIDOMTQ = value.IDPRODUTOSPEDIDOMTQ;
                    cbProdutoMTQ.SelectedValue = value.IDPRODUTO;
                    txtQuantMTQ.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtvalorunitMTQ.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtAlturaMTQ.Text = Convert.ToDecimal(value.ALTURA).ToString("n4");
                    txtLarguraMTQ.Text = Convert.ToDecimal(value.LARGURA).ToString("n4");
                    txtTotalMTQ.Text = Convert.ToDecimal(value.MT2).ToString("n4");
                    txtVlMtrMTQ.Text = Convert.ToDecimal(value.VALORMETRO).ToString("n2");
                    txtVlTotalMTQ.Text = Convert.ToDecimal(value.VALORTOTAL).ToString("n2");

                    if(value.IDCOR != null)
                         cbCor2.SelectedValue = value.IDCOR;
                    else
                        cbCor2.SelectedValue = -1;

                     chkExibirProd2.Checked = value.FLAGEXIBIR.TrimEnd() ==  "N" ? true: false;

                     txtDadosAdicionais2.Text = value.DADOADICIONAIS;

                     if (value.IDAMBIENTE != null)
                         cbambiente2.SelectedValue = value.IDAMBIENTE;
                     else
                         cbambiente2.SelectedValue = -1;

                     if (value.IDPRODUTOMASTER != null)
                         cbProdutoFinal.SelectedValue = value.IDPRODUTOMASTER;
                     else
                         cbProdutoFinal.SelectedValue = -1;

                     txtPorcPerda.Text = Convert.ToDecimal(value.PORCPERDA).ToString("n2");
                     txtPerdaMT2.Text = Convert.ToDecimal(value.TOTALPERDA).ToString("n4");
                     SomaPerdaMT2();
                        
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOSPEDIDOMTQ = -1;
                    cbProdutoMTQ.SelectedIndex = 0;
                    txtQuantMTQ.Text = "1,00";
                    txtvalorunitMTQ.Text = "0,00";
                    txtAlturaMTQ.Text = "0,00";
                    txtLarguraMTQ.Text = "0,00";
                    txtTotalMTQ.Text = "0,00";
                    txtVlMtrMTQ.Text = "0,00";
                    txtVlTotalMTQ.Text = "0,00";
                    cbCor2.SelectedValue = -1;
                    txtDadosAdicionais2.Text = string.Empty;
                    txtPerdaMT2.Text = "0,0000";
                    txtPorcPerda.Text = "0,00";
                    SomaPerdaMT2();

                    errorProvider1.Clear();
                }
            }
        }      



        private void FrmPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao" && this.ActiveControl.Name != "txtObsAnexo")
                {
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                    this.Focus();
                }
                    
            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmPedidoVenda_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();

            GetDropCliente();
            GetDropStatus();
            GetDropProdutos();
            GetTransporte();
            GetFuncionario();
            GetDropCentroCusto();
            GetDropFormaPgto();
            GetDropLocalCobranca();
            GetDropTipoDuplicata();
            GetDropProdutosMTQ();
            GetDropCor();
            GetDropCor2();
            GetDropAmbiente();
            GetDropAmbiente2();
            GetDropProdutoFinal();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnCadAmbiente.Image = Util.GetAddressImage(6);
            btnCadAmbiente2.Image = Util.GetAddressImage(6);
            btnCadProdFinal.Image = Util.GetAddressImage(6);
          
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadProdMTQ.Image = Util.GetAddressImage(6);
            btnCadCor.Image = Util.GetAddressImage(6);
            btnCor2.Image = Util.GetAddressImage(6);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();           

            GetConfiSistema();


            if (_IDPEDIDO != -1)
            {
                int CodigoSelect = _IDPEDIDO;
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);

                Entity = PEDIDOP.Read(CodigoSelect);
                ListaProdutoPedido(_IDPEDIDO);
                txtNPedido.Focus();

                //Lista Duplicatas do Pedido
                GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                //Lista Comissao dos produtos
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                    SumTotalComissao();
            }
            else
                Entity = null;


            this.Cursor = Cursors.Default;

            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }
      

        private void GetConfiSistema()
        {
            try
            {
                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                FLAGPEDBAIXAESTOQUE = CONFISISTEMATy.FLAGPEDBAIXAESTOQUE;

                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                FLAGCOMISSAO = CONFISISTEMATy.FLAGCOMISSAO;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }   

      

        private void PreencheDropTipoPesquisa()
        {
            try
            {
                Utility Util = new Utility();
                cbTipoPesquisa.DataSource = Util.GetSearchType();
                cbTipoPesquisa.DisplayMember = "nomecampo";
                cbTipoPesquisa.ValueMember = "tipocampo";
                cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos"); cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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
        }

        private void GetDropCliente()
        {
            try
            {
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

                cbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropStatus()
        {
            try
            {
                //11 Pedido de Venda
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
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

        private void GetDropProdutos()
        {
            try
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

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

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
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

                    if(result > 0)
                        cbCliente.SelectedValue = result;
                }
            }

            e.Handled = false;
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
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

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
            }    
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUS = CodSelec;
                frm.ShowDialog();
               
                GetDropStatus();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void btnExtratoCliente_Click(object sender, EventArgs e)
        {
            using (FrmExtratoDuplReceber frm = new FrmExtratoDuplReceber())
            {
                frm.CodClienteSelec = Convert.ToInt32(cbCliente.SelectedValue);
                frm.ShowDialog();
            }
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex > 0)
            {
                VerificaDebitoCliente(Convert.ToInt32(cbCliente.SelectedValue));

                if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
                {

                    using (FrmBloqueado frm = new FrmBloqueado())
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void VerificaDebitoCliente(int IdCliente)
        {
            try
            {
                int IDCLIENTE = Convert.ToInt32(IdCliente);

                string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));

                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<", DataAtual));
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
                LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                if (LIS_DUPLICATARECEBERColl.Count > 0)
                {
                    txtStatusFinaCliente.Text = "Existe débito para o cliente!";
                    txtStatusFinaCliente.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtStatusFinaCliente.Text = "Não existe débito para o cliente!";
                    txtStatusFinaCliente.BackColor = System.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutosMTQ();
                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
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
                        txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
                SomaProdutoAba2();
        }

        private void SomaProdutoAba2()
        {
            try
            {
                txtValorMtLinearTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception)
            {
                errorProvider1.SetError(txtQuanProduto, "Erro no cálculo!");
                errorProvider1.SetError(txtValorUnitProd, "Erro no cálculo!");
                MessageBox.Show("Erro no cálculo!");
            }

        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtValorUnitProd.Text);
                txtValorUnitProd.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnitProd, "");
                SomaProdutoAba2();
            }
        }

        private void cadTransportadora_Click(object sender, EventArgs e)
        {
            using (FrmTransportadora frm = new FrmTransportadora())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbTransportadora.SelectedValue);
                GetTransporte();
                cbTransportadora.SelectedValue = CodSelec;
            }
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
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
                frm._IDFORMAPAGAMENTO = CodSelec;
                frm.ShowDialog();
                GetDropFormaPgto();
                cbFormaPagto.SelectedValue = CodSelec;
            }
        }

        private void txtNOrcamento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nº Pedido, código gerado automaticamente.";
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

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            Grava();
            this.Cursor = Cursors.Default;
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    if (_IDPEDIDO == -1)
                        _IDPEDIDO = PEDIDOP.Save(Entity);
                    else
                    {
                        _IDPEDIDO = PEDIDOP.Save(Entity);
                        btnPesquisa_Click(null, null);
                    }

                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }
       
        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbCliente.SelectedIndex == 0)
            {
                errorProvider1.SetError(label42, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
            {
                result = false;
                using (FrmBloqueado frm = new FrmBloqueado())
                {
                    frm.ShowDialog();
                }
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

      
        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
            if (txtPorcDesconto.Text == "0,00")
                txtPorcDesconto.Text = string.Empty;
        }

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcDesconto.Text))
                {
                    errorProvider1.SetError(txtPorcDesconto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcDesconto.Text);
                    txtPorcDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconto, "");

                    txtTotalDesconto.Text =
               ((Convert.ToDecimal(txtTotalPedido.Text) * Convert.ToDecimal(txtPorcDesconto.Text)) / 100).ToString("n2");
                    SumTotalPedido();
                }
            }
            else
                txtPorcDesconto.Text = "0,00";
        }

        private void txtTotalDesconto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalDesconto.Text))
                {
                    errorProvider1.SetError(txtTotalDesconto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtTotalDesconto.Text);
                    txtTotalDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalDesconto, "");

                    SumTotalPedido();
                }
            }
            else
                txtTotalDesconto.Text = "0,00";
        }

        private void txtPorcAcrescimo_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcAcrescimo.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcAcrescimo.Text))
                {
                    errorProvider1.SetError(txtPorcAcrescimo, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcAcrescimo.Text);
                    txtPorcAcrescimo.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcAcrescimo, "");

                    txtTotalAcrescimo.Text =
              ((Convert.ToDecimal(txtTotalPedido.Text) * Convert.ToDecimal(txtPorcAcrescimo.Text)) / 100).ToString("n2");
                   SumTotalPedido();
                }
            }
            else
                txtPorcAcrescimo.Text = "0,00";
        }

        private void txtTotalAcrescimo_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalAcrescimo.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalAcrescimo.Text))
                {
                    errorProvider1.SetError(txtTotalAcrescimo, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtTotalAcrescimo.Text);
                    txtTotalAcrescimo.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalAcrescimo, "");

                    SumTotalPedido();
                }
            }
            else
                txtTotalAcrescimo.Text = "0,00";
        }

        private void txtTotalIPI_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalIPI.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalIPI.Text))
                {
                    errorProvider1.SetError(txtTotalIPI, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtTotalIPI.Text);
                    txtTotalIPI.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalIPI, "");

                    SumTotalPedido();
                }
            }
            else
                txtTotalIPI.Text = "0,00";
        }

        private void txtTotalDesconto_Enter(object sender, EventArgs e)
        {
            if (txtTotalDesconto.Text == "0,00")
                txtTotalDesconto.Text = string.Empty;
        }

        private void txtPorcAcrescimo_Enter(object sender, EventArgs e)
        {
            if (txtPorcAcrescimo.Text == "0,00")
                txtPorcAcrescimo.Text = string.Empty;
        }

        private void txtTotalAcrescimo_Enter(object sender, EventArgs e)
        {
            if (txtTotalAcrescimo.Text == "0,00")
                txtTotalAcrescimo.Text = string.Empty;
        }

        private void txtTotalIPI_Enter(object sender, EventArgs e)
        {
            if (txtTotalIPI.Text == "0,00")
                txtTotalIPI.Text = string.Empty;
        }

        private void txtValorUnitProd_Enter(object sender, EventArgs e)
        {
            if (txtValorUnitProd.Text == "0,00")
                txtValorUnitProd.Text = string.Empty;
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

                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(Filtro, "IDPEDIDO DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_PEDIDOTy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_PEDIDOTy.VALORACRESCIMO = SumTotalPesquisa("VALORACRESCIMO");
                LIS_PEDIDOTy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOColl;

                lblTotalPesquisa.Text = LIS_PEDIDOColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlPedidoVenda.SelectedIndex == 2)
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
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_PEDIDOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOColl;
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

                
                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(Filtro, "IDPEDIDO DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_PEDIDOTy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_PEDIDOTy.VALORACRESCIMO = SumTotalPesquisa("VALORACRESCIMO");
                LIS_PEDIDOTy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOColl;

                lblTotalPesquisa.Text = LIS_PEDIDOColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "TOTALPRODUTOS")
                    valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
                else if (NomeCampo == "VALORACRESCIMO")
                    valortotal += Convert.ToDecimal(item.VALORACRESCIMO);
                else if (NomeCampo == "VALORCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VALORCOMISSAO);
            }

            return valortotal;
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_PEDIDOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PEDIDOColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_PEDIDOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);

                    if (CodigoSelect > 0)
                    {
                        tabControlPedidoVenda.SelectTab(0);
                        tabControl1.SelectTab(0);

                        Entity = PEDIDOP.Read(CodigoSelect);
                        ListaProdutoPedido(_IDPEDIDO);
                        txtNPedido.Focus();

                        //Lista Duplicatas do Pedido
                        GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                   

                        //Lista Comissao dos produtos
                        //"S" para comissão sobre o total o pedido
                        //"N" para comissão pelo total dos produto
                        if (FLAGCOMISSAO == "N")
                            SumTotalComissao();
                    }
                }
            }
        }     

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Entity = null;
                Entity2 = null;
                SumTotalComissao();
                ListaProdutoPedido(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_PEDIDOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[indice].IDPEDIDO);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlPedidoVenda.SelectTab(0);
                    Entity = PEDIDOP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDO);
                    txtNPedido.Focus();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);                   

                    //Lista Comissao dos produtos
                    //"S" para comissão sobre o total o pedido
                    //"N" para comissão pelo total dos produto
                    if (FLAGCOMISSAO == "N")
                        SumTotalComissao();

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            PEDIDOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            btnPesquisa_Click(null, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            try
            {
                Entity = null;
                Entity2 = null;
                
                SumTotalComissao();
                ListaProdutoPedido(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            }
            catch (Exception ex)
            {
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg("Antes de adicionar os produtos é necessário gravar o Pedido!", "Red");
            }
            else
            {
                GravaProduto();

            }
        }

        private void GravaProduto()
        {
            try
            {
                if (ValidacoesProdutos())
                {
                    PRODUTOSPEDIDOP.Save(Entity2);
                    ListaProdutoPedido(_IDPEDIDO);                   

                    //Salva Pedido
                    PEDIDOP.Save(Entity);                 
                   
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

        private Boolean ValidacoesProdutos()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (rdVenda.Checked && VerificaCredito())
            {
                string msgerro = "O limite de crédito do cliente foi atingido!";
                errorProvider1.SetError(cbCliente, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (rdVenda.Checked && CONFISISTEMAP.Read(1).ESTOQUENEGATIVO.TrimEnd() == "S")
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOS_EstoqueTy = new PRODUTOSEntity();
                PRODUTOS_EstoqueTy = PRODUTOSP.Read(Convert.ToInt32(Convert.ToInt32(cbProduto.SelectedValue)));
               
                if (PRODUTOS_EstoqueTy.FLAGBAIXAESTMT.TrimEnd() != "S" && Convert.ToDecimal(txtEstoqueMTLinear.Text) < Convert.ToDecimal(txtQuanProduto.Text))
                {
                    string Msgerro = "Estoque não pode ficar negativo!";
                    errorProvider1.SetError(txtQuanProduto, Msgerro);
                    Util.ExibirMSg(Msgerro, "Red");
                    result = false;
                }
               
            }
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }           
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean VerificaCredito()
        {
            Boolean result = false;
            decimal LimiteCredito = Convert.ToDecimal(CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).CREDITOCLIENTE);

            if (CONFISISTEMAP.Read(1).FLAGLIMITECREDITO.TrimEnd().TrimStart() == "S" && LimiteCredito > 0)
            {

                decimal ValorCompraAtual = Convert.ToDecimal(txtTotalProduto.Text) + Convert.ToDecimal(txtValorMtLinearTotal.Text) + Convert.ToDecimal(txtTotalProdMTQ.Text) + Convert.ToDecimal(txtVlTotalMTQ.Text); 
                decimal ValorDebitoContaReceber = 0;

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl2 = new LIS_DUPLICATARECEBERCollection();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", cbCliente.SelectedValue.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "<>", "PD" + txtNPedido.Text));
                LIS_DUPLICATARECEBERColl2 = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO");
               

                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl2)
                {
                    if (item.DATAVECTO < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) && item.IDSTATUS != 3)//3 - Pago
                        ValorDebitoContaReceber += Convert.ToDecimal(item.VALORDEVEDOR);
                }

                ValorDebitoContaReceber += ValorCompraAtual;

                if (LimiteCredito < ValorDebitoContaReceber)
                    result = true;
            }

            return result;
        }


        private void ListaProdutoPedido(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

                SumTotalProdutosPedido();

                PaintGrid2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            } 
        }

        private void PaintGrid2()
        {
            //try
            //{

            //    int i = 0;

            //    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            //    {
            //        if (item.IDPRODUTOMASTER != 0)//Aberto
            //        {
            //            //dtgProdMTQ.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
            //            dtgProdMTQ.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
            //        }
            //        else
            //        {
            //            //dtgProdMTQ.Rows[i].DefaultCellStyle.BackColor = Color.Black;
            //            dtgProdMTQ.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            //        }

            //        i++;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Erro Técnico: " + ex.Message,
            //                  ConfigSistema1.Default.NomeEmpresa,
            //                  MessageBoxButtons.OK,
            //                  MessageBoxIcon.Error,
            //                  MessageBoxDefaultButton.Button1);

            //    MessageBox.Show("Erro ao pintar o Grid!",
            //                   ConfigSistema1.Default.NomeEmpresa,
            //                   MessageBoxButtons.OK,
            //                   MessageBoxIcon.Error,
            //                   MessageBoxDefaultButton.Button1);
            //}
        }

        private void SumTotalComissao()
        {
            decimal TotalComissao = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                TotalComissao += Convert.ToDecimal(item.COMISSAOPEDIDO);
            }

            txtValComissao.Text = TotalComissao.ToString("n2");
        }
       

        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");

            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }
            
            txtTotalProdAdicional.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void SumTotalPedido()
        {
            decimal TotalPedido = 0;
            TotalPedido = (Convert.ToDecimal(txtTotalProduto.Text) +
                           Convert.ToDecimal(txtTotalIPI.Text) +
                           Convert.ToDecimal(txtTotalProdMTQ.Text) +
                           Convert.ToDecimal(txtTotalAcrescimo.Text)) -
                           Convert.ToDecimal(txtTotalDesconto.Text);

            txtTotalPedido.Text = TotalPedido.ToString("n2");
            txtTotalFinanceiro.Text = TotalPedido.ToString("n2");
            txtValorDev.Text = (Convert.ToDecimal(txtTotalFinanceiro.Text) - Convert.ToDecimal(txtValorPago.Text)).ToString("n2");
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
            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar o Pedido é necessário remover os Produtos!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);

                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
                result = false;
            }
            else if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar o Pedido é necessário remover as Duplicatas!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(2);
                result = false;
            }
            else if (ListaProdutoEntrega(_IDPEDIDO) > 0)
            {
                MessageBox.Show("Antes de Apagar o Pedido é necessário remover a lista de entrega!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                result = false;
            }
            else if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private int ListaProdutoEntrega(int IDPEDIDO)
        {
            int result = 0;

            try
            {
                Filtro.Clear();
                Filtro.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_CONTROLEENTREGACollection LIS_CONTROLEENTREGAColl = new LIS_CONTROLEENTREGACollection();
                LIS_CONTROLEENTREGAProvider LIS_CONTROLEENTREGAP = new LIS_CONTROLEENTREGAProvider();
                LIS_CONTROLEENTREGAColl = LIS_CONTROLEENTREGAP.ReadCollectionByParameter(Filtro, "nomeproduto, DATAENTREGA");

                result = LIS_CONTROLEENTREGAColl.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }


        private void Delete()
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");

                tabControlPedidoVenda.SelectTab(2);
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

                        PEDIDOP.Delete(_IDPEDIDO);

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

        private void DGDadosProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }   

        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void lkComp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void AddProdutoComposicao(int IDCOMPOSICAO)
        {
             //Filtras os produtos da determinada composicao
            LIS_COMPOSPRODUTOProvider LIS_COMPOSPRODUTOP = new LIS_COMPOSPRODUTOProvider();
            LIS_COMPOSPRODUTOCollection LIS_COMPOSPRODUTOColl = new LIS_COMPOSPRODUTOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDCOMPOSICAO", "System.Int32", "=", IDCOMPOSICAO.ToString()));
            LIS_COMPOSPRODUTOColl = LIS_COMPOSPRODUTOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

            //Percorre os produtos da coleção
            foreach (LIS_COMPOSPRODUTOEntity item in LIS_COMPOSPRODUTOColl)
            {
                cbProduto.SelectedValue = item.IDPRODUTO;
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");
               
                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;
               

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;
                txtValorMtLinearTotal.Text = Convert.ToDecimal(PRODORCAMENTOTy.VALORTOTAL).ToString("n2");
                

                //Salva o produtos no Pedido
               PRODUTOSPEDIDOP.Save(Entity2);                         
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDO);            

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);         
        }
      
        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);
                txtValorUnitProd.Text = ValorVenda.ToString("n2");
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(PRODUTOS_EstoqTy.IDPRODUTO), false);
                txtEstoqueMTLinear.Text = Convert.ToDecimal(ESTOQUEATUAL).ToString("n2");

            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbFuncionario.SelectedIndex > 0)
                {
                    FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                    decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);
                    txtPorComisVend.Text = PorcComissVend.ToString("n2");
                    txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * PorcComissVend) / 100).ToString("n2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular a comissão!");
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

                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);

                if (PorcComissVend != Convert.ToDecimal(txtPorComisVend.Text))
                {
                    txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * Convert.ToDecimal(txtPorComisVend.Text)) / 100).ToString("n2");
                }
                else
                {
                    txtPorComisVend.Text = PorcComissVend.ToString("n2");
                    txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * PorcComissVend) / 100).ToString("n2");
                }
                
            } 
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
            
        }

        private Boolean ValidaDuplicatasVencidas()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbFormaPagto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFormaPagto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (cbLocalCobranca.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbLocalCobranca, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }


        private void SaveDuplicatasPaga()
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
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
                DUPLICATARECEBERty.NUMERO = txtNPedido.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());

                decimal Valor = Convert.ToDecimal(txtValorDev.Text);
                //Calculando desconto
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                decimal PorcDesconto = Convert.ToDecimal(FORMAPAGAMENTOP.Read(Convert.ToInt32(item.IDFORMAPAGAMENTO)).PORCDESCONTO);
                if (PorcDesconto > 0)
                    Valor -= ((Valor * PorcDesconto) / 100);

                Valor = Valor * Convert.ToDecimal(item.PORCPAGTO) / 100;

                //Calculando juros
                Valor = ((Valor * Convert.ToDecimal(item.PORCJUROS)) / 100) + Valor;

                DUPLICATARECEBERty.DATAPAGTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORPAGO = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = 0;
                DUPLICATARECEBERty.IDSTATUS = 3; //Pago
                DUPLICATARECEBERty.NOTAFISCAL = "PD" + txtNPedido.Text;

                //Comissao Vendedor
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                {
                    DUPLICATARECEBERty.COMISSAO = Convert.ToDecimal(txtValComissao.Text) / ITENSFORMAPAGTOColl.Count;
                }
                else
                    DUPLICATARECEBERty.COMISSAO = (Valor * Convert.ToDecimal(txtPorComisVend.Text)) / 100;

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }
       
       
        private void SaveDuplicatas()
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
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
                DUPLICATARECEBERty.NUMERO = txtNPedido.Text + "-" + NumTotalDupl.ToString();
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
                DUPLICATARECEBERty.NOTAFISCAL = "PD" + txtNPedido.Text;

                //Comissao Vendedor
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                {
                    DUPLICATARECEBERty.COMISSAO = Convert.ToDecimal(txtValComissao.Text) / ITENSFORMAPAGTOColl.Count;
                }
                else
                    DUPLICATARECEBERty.COMISSAO = (Valor * Convert.ToDecimal(txtPorComisVend.Text)) / 100;

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }               
            }
        }

        private void GridDuplicatasPD(int idcliente, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PD" + numero));

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");
                dataGridDupl.AutoGenerateColumns = false;
                dataGridDupl.DataSource = LIS_DUPLICATARECEBERColl;
                SumFinanceiroPD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumFinanceiroPD()
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
            else if (Convert.ToDecimal(txtValorDev.Text) < 1)
            {
                errorProvider1.SetError(txtValorDev, ConfigMessage.Default.FieldErro);
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
                            if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                                DUPLICATARECEBERP.Delete(CodSelect);
                                GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                SumFinanceiroPD();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //editar
                {
                    FrmContasReceber FrmCR = new FrmContasReceber();
                    FrmCR.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                    FrmCR.ShowDialog();

                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
            }
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

        private void txtValorDev_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorDev.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorDev.Text))
                {
                    errorProvider1.SetError(txtValorDev, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorDev.Text);
                    txtValorDev.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorDev, "");                   
                }
            }
            else
                txtValorDev.Text = "0,00";
        }

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            if (txtValorPago.Text == "0,00")
                txtValorPago.Text = string.Empty;
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

        private void GetDropLocalCobranca()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg("Antes de adicionar o Caixa é necessário gravar o Pedido!",  "Red");
                
            }
            else if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
            }
            else
            {
                if (Convert.ToInt32(cbTipo.SelectedValue) < 1 )
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
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
            CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
            CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = "PD"+txtNPedido.Text;
            CaixaTy.OBSERVACAO = "Pedido de Venda nº " + "PD" + txtNPedido.Text + " Cliente: " + cbCliente.SelectedValue + " - " + GetNameCliente(Convert.ToInt32(cbCliente.SelectedValue));

            try
            {
                CaixaP.Save(CaixaTy);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {

                Util.ExibirMSg("Não foi possível fazer o movimento de caixa!", "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private string GetNameCliente(int IdCliente)
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            return CLIENTEP.Read(IdCliente).NOME;
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
           
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(2);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                modelo5MatériaPrimaToolStripMenuItem_Click(null, null);
            }
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(2);
            txtCriterioPesquisa.Focus();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        int IndexRegistro = 0;
        int IndexRegistro2 = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private decimal SomaTotal()
        {
            decimal result = 0;
            int i = 1;
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                if (i < LIS_PEDIDOColl.Count)
                    result += Convert.ToDecimal(item.TOTALPEDIDO);

                i++;
            }
            return result;
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
              
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
          
        }

        private void laserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                modelo3ToolStripMenuItem_Click(null, null);
            }
        }


        private void ImprimiObsAnexo()
        {
            try
            {
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {

                    objPrintPreview.Document = printDocument2;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro tecnico " + ex.Message);

            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
            LIS_PEDIDOCollection LIS_PEDIDOCollPrint = new LIS_PEDIDOCollection();
            LIS_PEDIDOCollPrint = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

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
            config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


            if (!_FLAGORCAMENTO)
                e.Graphics.DrawString("Nº PEDIDO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            else
                e.Graphics.DrawString("Nº ORÇAMENTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPEDIDO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            //Dados Cliente
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);


            //Armazena dados do cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            e.Graphics.DrawString("Nome:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 140);
            e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
            e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

            e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
            e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

            e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].MUNICIPIO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
            e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].UF, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

            e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

            string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
            e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);

            int linha = 240;
            int linhaBorda = 235;
            //Inicio dados de produtos e material

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);
            linha = linha + 5;
            e.Graphics.DrawString("Produtos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

            linha = linha + 15;
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 690, 20);

            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 630, 20);

            e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 220, 20);

            e.Graphics.DrawString("Vlr.Unit.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 130, 20);

            e.Graphics.DrawString("Vlr.Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);

            //Alinhamento dos valores
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;

            Font FonteDescr = new System.Drawing.Font("Arial", 7);

            config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

            int NumerorRegistros = LIS_PRODUTOSPEDIDOColl.Count;
            linha = linha + 25;
            linhaBorda = linhaBorda + 45;

            //Produtos
            while (IndexRegistro < LIS_PRODUTOSPEDIDOColl.Count)
            {
                 e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                 e.Graphics.DrawString(LIS_PRODUTOSPEDIDOColl[IndexRegistro].QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                 e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);

                 if (CONFISISTEMAP.Read(1).FLAGCODREFERENCIA.TrimEnd() == "N")
                     e.Graphics.DrawString(LIS_PRODUTOSPEDIDOColl[IndexRegistro].IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);
                 else
                 {
                     string CodReferencia = PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[IndexRegistro].IDPRODUTO)).CODPRODUTOFORNECEDOR;
                     e.Graphics.DrawString(CodReferencia, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);
                 }

                 e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);


                 if (LIS_PRODUTOSPEDIDOColl[IndexRegistro].IDCOR > 0)
                 {
                     string StrCor =" - Cor: " +  LIS_PRODUTOSPEDIDOColl[IndexRegistro].NOMECOR;
                     e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[IndexRegistro].NOMEPRODUTO + StrCor, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);
                 }
                 else
                     e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[IndexRegistro].NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                 //Alinhar a direita
                 e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                 e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOColl[IndexRegistro].VALORUNITARIO).ToString("n4"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                 e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                 e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOColl[IndexRegistro].VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                 linha = linha + 20;
                 linhaBorda = linhaBorda + 20;

                 IndexRegistro++;
                 config.LinhaAtual++;

                 if (config.LinhaAtual > 30)
                     break;

            }

            //'verifica se continua imprimindo
            if (IndexRegistro < LIS_PRODUTOSPEDIDOColl.Count)
                e.HasMorePages = true;
        }



        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
      
        private void ImprimirListaGeralProduto()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Pedidos com Produtos");
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

                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
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
                config.NomeEmpresa = EMPRESATy.NOMEFANTASIA + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 68);

                //Titulo
                e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Ped.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 120, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 340, 170);
                e.Graphics.DrawString("Vendedor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, 170);
                e.Graphics.DrawString("Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_PEDIDOColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_PEDIDOColl.Count)
                {
                    if (LIS_PEDIDOColl[IndexRegistro].IDPEDIDO != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_PEDIDOColl[IndexRegistro].IDPEDIDO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_PEDIDOColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOColl[IndexRegistro].NOMEVENDEDOR, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_PEDIDOColl[IndexRegistro].TOTALPEDIDO).ToString("n2");
                        e.Graphics.DrawString(TotalFOS, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    //Listar os produtos
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOPrintColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOColl = ProdutoRel(Convert.ToInt32(LIS_PEDIDOColl[IndexRegistro].IDPEDIDO));
                    e.Graphics.DrawString("Cod.Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Produtos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                        if (CONFISISTEMAP.Read(1).FLAGCODREFERENCIA.TrimEnd() == "N")
                            e.Graphics.DrawString(Util.LimiterText(item.IDPRODUTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        else
                        {
                            string CodReferencia = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO)).CODPRODUTOFORNECEDOR;
                            e.Graphics.DrawString(Util.LimiterText(CodReferencia, 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        }
                        

                        e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 25), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.QUANTIDADE.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                    }

                    //Listar os produtos
                    LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQPrintColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                    LIS_PRODUTOSPEDIDOMTQPrintColl = ProdutoRel2(Convert.ToInt32(LIS_PEDIDOColl[IndexRegistro].IDPEDIDO));
                    foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQPrintColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                        if (CONFISISTEMAP.Read(1).FLAGCODREFERENCIA.TrimEnd() == "N")
                            e.Graphics.DrawString(Util.LimiterText(item.IDPRODUTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        else
                        {
                            string CodReferencia = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO)).CODPRODUTOFORNECEDOR;
                            e.Graphics.DrawString(Util.LimiterText(item.IDPRODUTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        }

                        
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
               

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_PEDIDOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + (LIS_PEDIDOColl.Count - 1), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

        private LIS_PRODUTOSPEDIDOCollection ProdutoRel(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOColl;
        }

        private LIS_PRODUTOSPEDIDOMTQCollection ProdutoRel2(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOMTQColl;
        }

        

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
        }  

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PEDIDOColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity>(orderBy);

                LIS_PEDIDOColl.Sort(comparer.Comparer);              
                 
                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_PEDIDOColl;               
            }
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
                config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
                e.Graphics.DrawString(EMPRESATy.ENDERECO + " " +EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
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
                RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

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
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMEFANTASIA + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
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
                config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
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
                RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

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
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMEFANTASIA + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

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

                config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
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
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMEFANTASIA + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 910);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 940);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 955);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 955);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }     
       
        }

        private void compostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(2);
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
                config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
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
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PD" + txtNPedido.Text));
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
                e.Graphics.DrawString("PD" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString("PD" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


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
                RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

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
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMEFANTASIA + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

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

        private void txtQuanProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para fazer cálculo de M2 pressione Ctrl+Q e para M3 Ctrl+T";
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
                        txtQuanProduto.Text = result.ToString("n4");
                }
            }
            else if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.T))
            {
                using (FormMedCubico frm = new FormMedCubico())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        txtQuanProduto.Text = result.ToString("n4");
                }
            }
        }

        private void lnkSelecCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmSearchCliente frm = new FrmSearchCliente())
            {
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    CLIENTEEntity ClienteSelecTy = new CLIENTEEntity();
                    CLIENTEProvider ClienteP = new CLIENTEProvider();
                    ClienteSelecTy = ClienteP.Read(Convert.ToInt32(result));
                    MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();
                    string NomeCidade = MUNICIPIOSP.Read(Convert.ToInt32(ClienteSelecTy.COD_MUN_IBGE)).MUNICIPIO; 
                    txtObservacao.Text = " Dados da Entrega: " + ClienteSelecTy.NOME + " " +
                                          " Endereço: " + ClienteSelecTy.ENDERECO1 + ", " +
                                          NomeCidade + " " +
                                          " Telefone: " + ClienteSelecTy.TELEFONE1;
                }
            }
        }   
       
        private void SumTotalProdutosPedidoMT()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOMTEntity item in LIS_PRODUTOSPEDIDOMTColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }
          
            SumTotalPedido();
        }
      

        private void orçamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                _FLAGORCAMENTO = rdOrcamento.Checked;
                modelo3ToolStripMenuItem_Click(null, null);
            }
        }
       

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }   

        private void btnCadProdMTQ_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProdutoMTQ.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutos();
                GetDropProdutosMTQ();
                cbProdutoMTQ.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutosMTQ()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSMTColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProdutoMTQ.DisplayMember = "NOMEPRODUTO";
            cbProdutoMTQ.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSMTColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProdutoMTQ.DisplayMember);

            PRODUTOSMTColl.Sort(comparer.Comparer);
            cbProdutoMTQ.DataSource = PRODUTOSMTColl;

            cbProdutoMTQ.SelectedIndex = 0;
        }

        private void cbProdutoMTQ_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProdutoMTQ.SelectedIndex > 0)
            {
                 PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue));

                decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);
                txtVlMtrMTQ.Text = ValorVenda.ToString("n2");
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(PRODUTOS_EstoqTy.IDPRODUTO), false);
                txtEstoqueMT2.Text = Convert.ToDecimal(ESTOQUEATUAL).ToString("n2");

                txtEstMinimo.Text = Convert.ToDecimal(PRODUTOS_EstoqTy.QUANTIDADEMINIMA).ToString("n3");

                if (PRODUTOS_EstoqTy.PORCPERDAPROD != null)
                    txtPorcPerda.Text = Convert.ToDecimal(PRODUTOS_EstoqTy.PORCPERDAPROD).ToString("n3");

            }
        }

        private void cbProdutoMTQ_KeyDown(object sender, KeyEventArgs e)
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
                        cbProdutoMTQ.SelectedValue = result;
                        txtVlMtrMTQ.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void txtQuantMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtQuantMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantMTQ.Text))
                {
                    errorProvider1.SetError(txtQuantMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtQuantMTQ.Text);
                    txtQuantMTQ.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtQuantMTQ, "");
                }
            }
            else
                txtQuantMTQ.Text = "0,00";
        }

        private void txtQuantMTQ_Enter(object sender, EventArgs e)
        {
            if (txtQuantMTQ.Text == "0,00")
                txtQuantMTQ.Text = string.Empty;
        }

        private void txtAlturaMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAlturaMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAlturaMTQ.Text))
                {
                    errorProvider1.SetError(txtAlturaMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtAlturaMTQ.Text);
                    txtAlturaMTQ.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtAlturaMTQ, "");
                    SomaUnitMTQ();
                }
            }
            else
                txtAlturaMTQ.Text = "0,0000";
        }

        private void SomaUnitMTQ()
        {
            PRODUTOSEntity PRODUTOSTyEn = new PRODUTOSEntity();

            //Arredondamento
            if (Convert.ToInt32(cbProdutoMTQ.SelectedValue) > 0)
            {
                PRODUTOSTyEn = PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue));

                if (PRODUTOSTyEn.MULTAREND > 0)
                {
                    txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n4");

                    if (PRODUTOSTyEn.FLAGDECIMALREND == "S")
                    {
                        ////Pegando somente os decimais
                        //int pos = txtTotalMTQ.Text.IndexOf(",");
                        //string valorinicial = txtTotalMTQ.Text.Substring(0, pos);
                        //decimal valorret = Convert.ToDecimal(txtTotalMTQ.Text.Substring(pos + 1, 3));

                        //if (valorret % PRODUTOSTyEn.MULTAREND != 0)
                        //    do
                        //    {
                        //        valorret += Convert.ToDecimal("1");

                        //    }
                        //    while (Convert.ToDecimal(valorret) % PRODUTOSTyEn.MULTAREND != 0);

                        //txtTotalMTQ.Text = valorinicial + "," + valorret.ToString();
                        decimal MULTAREND = (Convert.ToDecimal(PRODUTOSTyEn.MULTAREND) / 10);
                        txtTotalMTQ.Text = (Convert.ToDecimal(txtTotalMTQ.Text) + MULTAREND).ToString();
                    }
                    else
                    {
                        ////Numero inteiros
                        //txtTotalMTQ.Text = (Math.Round(Convert.ToDecimal(txtTotalMTQ.Text))).ToString("n4");
                        //decimal valorret = Convert.ToDecimal(txtTotalMTQ.Text);

                        //if (valorret % PRODUTOSTyEn.MULTAREND != 0)
                        //    do
                        //    {
                        //        valorret += Convert.ToDecimal("1");
                        //    }
                        //    while (Convert.ToDecimal(valorret) % PRODUTOSTyEn.MULTAREND != 0);


                        //txtTotalMTQ.Text = valorret.ToString("n4");

                        decimal MULTAREND = Convert.ToDecimal(PRODUTOSTyEn.MULTAREND);
                        txtTotalMTQ.Text = (Convert.ToDecimal(txtTotalMTQ.Text) + MULTAREND).ToString();
                    }
                }
                else
                    txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n4");
            }
            else
                txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n4");

            txtvalorunitMTQ.Text = (Convert.ToDecimal(txtVlMtrMTQ.Text) * (Convert.ToDecimal(txtTotalMTQ.Text) + Convert.ToDecimal(txtPerdaMT2.Text))).ToString("n2");
            txtVlTotalMTQ.Text = (Convert.ToDecimal(txtvalorunitMTQ.Text) * Convert.ToDecimal(txtQuantMTQ.Text)).ToString("n2"); //
        }

        private void txtLarguraMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtLarguraMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtLarguraMTQ.Text))
                {
                    errorProvider1.SetError(txtLarguraMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtLarguraMTQ.Text);
                    txtLarguraMTQ.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtLarguraMTQ, "");

                    SomaUnitMTQ();
                }
            }
            else
                txtLarguraMTQ.Text = "0,0000";
        }

        private void txtTotalMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalMTQ.Text))
                {
                    errorProvider1.SetError(txtTotalMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtTotalMTQ.Text);
                    txtTotalMTQ.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtTotalMTQ, "");

                    SomaUnitMTQ();
                }
            }
            else
                txtTotalMTQ.Text = "0,0000";
        }

        private void txtVlMtrMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtVlMtrMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlMtrMTQ.Text))
                {
                    errorProvider1.SetError(txtVlMtrMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtVlMtrMTQ.Text);
                    txtVlMtrMTQ.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlMtrMTQ, "");

                    SomaUnitMTQ();
                }
            }
            else
                txtVlMtrMTQ.Text = "0,00";
        }

        private void txtvalorunitMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtvalorunitMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtvalorunitMTQ.Text))
                {
                    errorProvider1.SetError(txtvalorunitMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtvalorunitMTQ.Text);
                    txtvalorunitMTQ.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtvalorunitMTQ, "");

                    SomaUnitMTQ();
                }
            }
            else
                txtvalorunitMTQ.Text = "0,00";
        }

        private void txtVlTotalMTQ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtVlTotalMTQ.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlTotalMTQ.Text))
                {
                    errorProvider1.SetError(txtVlTotalMTQ, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtVlTotalMTQ.Text);
                    txtVlTotalMTQ.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlTotalMTQ, "");

                    SomaUnitMTQ();
                }
            }
            else
                txtVlTotalMTQ.Text = "0,00";
        }

        private void btnAdicionaMTQ_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {

                Util.ExibirMSg("Antes de adicionar os produtos é necessário gravar o Pedido!", "Red");
            }
            else
            {
                GravaProdutoMTQ();
            }
        }

        private void GravaProdutoMTQ()
        {
            try
            {
                if (ValidacoesProdutosMTQ())
                {
                    PRODUTOSPEDIDOMTQP.Save(Entity6);
                    ListaProdutoPedidoMTQ(_IDPEDIDO);

                    //Salva Pedido
                    PEDIDOP.Save(Entity);

                    Entity6 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProdutoMTQ.Focus();
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaProdutoPedidoMTQ(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                dtgProdMTQ.AutoGenerateColumns = false;
                dtgProdMTQ.DataSource = LIS_PRODUTOSPEDIDOMTQColl;
                lblTotalCountMTQ.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOMTQColl.Count.ToString();

                PaintGrid();

                SumTotalProdutosPedidoMTQ();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
   
        }

        private void PaintGrid()
        {
            //try
            //{

            //    int i = 0;

            //    foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            //    {
            //        if (item.IDPRODUTOMASTER != 0)
            //        {
            //            //dtgProdMTQ.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
            //            dtgProdMTQ.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
            //        }
            //        else
            //        {
            //            //dtgProdMTQ.Rows[i].DefaultCellStyle.BackColor = Color.Black;
            //            dtgProdMTQ.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            //        }

            //        i++;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Erro Técnico: " + ex.Message,
            //                  ConfigSistema1.Default.NomeEmpresa,
            //                  MessageBoxButtons.OK,
            //                  MessageBoxIcon.Error,
            //                  MessageBoxDefaultButton.Button1);

            //    MessageBox.Show("Erro ao pintar o Grid!",
            //                   ConfigSistema1.Default.NomeEmpresa,
            //                   MessageBoxButtons.OK,
            //                   MessageBoxIcon.Error,
            //                   MessageBoxDefaultButton.Button1);
            //}
        }


        private Boolean ValidacoesProdutosMTQ()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProdutoMTQ.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProdutoMTQ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtvalorunitMTQ.Text))
            {
                errorProvider1.SetError(txtvalorunitMTQ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlMtrMTQ.Text))
            {
                errorProvider1.SetError(txtVlMtrMTQ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantMTQ.Text) || Convert.ToDecimal(txtQuantMTQ.Text) <= 0)
            {
                errorProvider1.SetError(txtQuantMTQ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (rdVenda.Checked && VerificaCredito())
            {
                string msgerro = "O limite de crédito do cliente foi atingido!";
                errorProvider1.SetError(cbCliente, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (rdVenda.Checked && CONFISISTEMAP.Read(1).ESTOQUENEGATIVO.TrimEnd() == "S")
            {
                if (Convert.ToDecimal(txtEstoqueMT2.Text) < (Convert.ToDecimal(txtTotalMt2Perda.Text) * Convert.ToDecimal(txtQuantMTQ.Text)))
                {
                    string Msgerro = "Estoque não pode ficar negativo!";
                    errorProvider1.SetError(txtEstoqueMT2, Msgerro);
                    errorProvider1.SetError(txtTotalMt2Perda, Msgerro);
                    Util.ExibirMSg(Msgerro, "Red");
                    result = false;
                }              

            }
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void SumTotalProdutosPedidoMTQ()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProdMTQ.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void dtgProdMTQ_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOMTQColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].IDPRODUTOSPEDIDOMTQ);
                    Entity6 = PRODUTOSPEDIDOMTQP.Read(CodSelect);

                }
                else if (chkTelaBloqueada.Checked)
                {
                    string msgerro = "Tela bloqueada!";
                    errorProvider1.SetError(chkTelaBloqueada, msgerro);
                    Util.ExibirMSg(msgerro, "Red");
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {

                            if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].IDPRODUTOSPEDIDOMTQ);
                                int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].IDPRODUTO);

                                decimal QUANTMOV = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].QUANTIDADE);
                                //Baixa pelo MT
                                if (PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].IDPRODUTO)).FLAGBAIXAESTMT == "S")
                                    QUANTMOV = Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[rowindex].MT2) * Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[rowindex].QUANTIDADE);

                                PRODUTOSPEDIDOMTQP.Delete(CodSelect);
                                ListaProdutoPedidoMTQ(_IDPEDIDO);

                                Entity6 = null;

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
            }
        }

        private void btnCancelMTQ_Click(object sender, EventArgs e)
        {
            Entity6 = null;
        }

        private void cbProdutoMTQ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaPV1 FrmVendaDiaria = new FrmVendaDiariaPV1();
            FrmVendaDiaria.ShowDialog();
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PrintTicket();
            }

        }

        private void PrintTicket()
        {
            try
            {
                Ticket ticket = new Ticket();

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMEFANTASIA, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine("Pedido N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0'));
                ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                ticket.AddSubHeaderLine("VENDEDOR: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                foreach (LIS_PRODUTOSPEDIDOMTEntity item in LIS_PRODUTOSPEDIDOMTColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEMATERIAL, Util.LimiterText(ValorTotal, 20));
                }

                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalProduto.Text) +  Convert.ToDecimal(txtTotalProdMTQ.Text); 
                ticket.AddTotal("SUBTOTAL", SUBTOTAL.ToString("n2"));
                ticket.AddTotal("DESCONTO", txtTotalDesconto.Text);
                ticket.AddTotal("ACRESCIMO:", txtTotalAcrescimo.Text);
                ticket.AddTotal("PAGO", txtValorPago.Text);
                ticket.AddTotal("TOTAL", txtValorDev.Text);

                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg1ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg2ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg3ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg4ticket);

                if (ticket.PrinterExists(BmsSoftware.ConfigSistema1.Default.impressoraticket))
                    ticket.PrintTicket(BmsSoftware.ConfigSistema1.Default.impressoraticket); //Nome da impresora , o caminho completo
                else
                    MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
            }

        }
        private void SomaUnitMTLinear()
        {
           
        }

        private void chkDetalProdFinal_CheckedChanged(object sender, EventArgs e)
        {
            txtDetalhesProdFinal.Enabled = !txtDetalhesProdFinal.Enabled;
        }

        private void txtAltLinear_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtAltLinear_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtLargLinear_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtLargLinear_Leave(object sender, EventArgs e)
        {
          
        }

        private void txtTotalMTLinear_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtTotalMTLinear_Leave(object sender, EventArgs e)
        {
            SomaUnitMTLinear();
        }

        private void txtValorUnitProd_Leave(object sender, EventArgs e)
        {
            SomaUnitMTLinear();
        }

        private void txtValorTotal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMtLinearTotal.Text))
            {
                errorProvider1.SetError(txtValorMtLinearTotal, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtValorMtLinearTotal.Text);
                txtValorMtLinearTotal.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorMtLinearTotal, "");
            }
        }

        private void txtQuanProduto_Leave(object sender, EventArgs e)
        {
            SomaUnitMTLinear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                
            }
            else
            {
                DialogResult dr = MessageBox.Show("Desejar excluir todos os produtos?",
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);



                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                        {
                            PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
                        }

                        ListaProdutoPedido(_IDPEDIDO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                    }
                }
            }
        }

        private void DGDadosProduto_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                  
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO);
                    Entity2 = PRODUTOSPEDIDOP.Read(CodSelect);

                }
                else if (chkTelaBloqueada.Checked)
                {
                    string msgerro = "Tela bloqueada!";
                    errorProvider1.SetError(chkTelaBloqueada, msgerro);
                    Util.ExibirMSg(msgerro, "Red");
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO);
                                int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODUTO);

                                decimal QUANTMOV = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].QUANTIDADE);
                                //Baixa pelo MT
                                if (PRODUTOSP.Read(IDPRODUTO).FLAGBAIXAESTMT == "S")
                                    QUANTMOV = Convert.ToDecimal(LIS_PRODUTOSPEDIDOColl[rowindex].TOTALMT) * Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].QUANTIDADE);


                                PRODUTOSPEDIDOP.Delete(CodSelect);
                                ListaProdutoPedidoMTQ(_IDPEDIDO);
                                ListaProdutoPedido(_IDPEDIDO);

                                Entity2 = null;

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
           }

        }

        private void pedido2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                _FLAGORCAMENTO = false;
                ImprimirPedido();
            }
        }

        private void ImprimirPedido()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument_Prod1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                objPrintPreview.Document = printDocument_Prod1;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }      

      

        int linhaPrint = 0; 
       
   
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");

            }
            else
            {
                DialogResult dr = MessageBox.Show("Desejar excluir todos os Produtos MT2?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);



                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                        {
                            PRODUTOSPEDIDOMTQP.Delete(Convert.ToInt32(item.IDPRODUTOSPEDIDOMTQ));
                        }

                        ListaProdutoPedidoMTQ(_IDPEDIDO);

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                    }
                }
            }
        }

        private void btnCadCor_Click(object sender, EventArgs e)
        {
            using (FrmCor frm = new FrmCor())
            {
                int CodSelec = Convert.ToInt32(cbCor.SelectedValue);
                frm._IDCOR = CodSelec; 
                frm.ShowDialog();                
                GetDropCor();
                cbCor.SelectedValue = CodSelec;
            }
        }

        private void GetDropCor()
        {

            cbCor.DisplayMember = "NOME";
            cbCor.ValueMember = "IDCOR";

            CORColl = CORP.ReadCollectionByParameter(null, "NOME");

            COREntity CORTy = new COREntity();
            CORTy.NOME = ConfigMessage.Default.MsgDrop;
            CORTy.IDCOR = -1;
            CORColl.Add(CORTy);

            Phydeaux.Utilities.DynamicComparer<COREntity> comparer = new Phydeaux.Utilities.DynamicComparer<COREntity>(cbCor.DisplayMember);

            CORColl.Sort(comparer.Comparer);
            cbCor.DataSource = CORColl;

            cbCor.SelectedIndex = 0;
        }

        private void GetDropCor2()
        {
            cbCor2.DisplayMember = "NOME";
            cbCor2.ValueMember = "IDCOR";

            COR2Coll = CORP.ReadCollectionByParameter(null, "NOME");

            COREntity CORTy = new COREntity();
            CORTy.NOME = ConfigMessage.Default.MsgDrop;
            CORTy.IDCOR = -1;
            COR2Coll.Add(CORTy);

            Phydeaux.Utilities.DynamicComparer<COREntity> comparer = new Phydeaux.Utilities.DynamicComparer<COREntity>(cbCor2.DisplayMember);

            COR2Coll.Sort(comparer.Comparer);
            cbCor2.DataSource = COR2Coll;

            cbCor2.SelectedIndex = 0;
        }

        private void btnCor2_Click(object sender, EventArgs e)
        {
            using (FrmCor frm = new FrmCor())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCor2.SelectedValue);
                GetDropCor2();
                cbCor2.SelectedValue = CodSelec;
            }
        }

        private void baixaDeEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void períodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string DataMovimentacaoInicial = InputBox("Data Movimentação Inicial", ConfigSistema1.Default.NomeEmpresa, DateTime.Now.ToString("dd/MM/yyyy"));
                string DataMovimentacaoFinal = InputBox("Data Movimentação Final", ConfigSistema1.Default.NomeEmpresa, DateTime.Now.ToString("dd/MM/yyyy"));

                if (!ValidacoesLibrary.ValidaTipoDateTime(DataMovimentacaoInicial))
                    MessageBox.Show("Erro na Data Inicial: " + ConfigMessage.Default.MsgDataInvalida);
                else if (!ValidacoesLibrary.ValidaTipoDateTime(DataMovimentacaoFinal))
                    MessageBox.Show("Erro na Data Final: " + ConfigMessage.Default.MsgDataInvalida);
                else
                {
                    string RelatorioTitulo = "Movimentação do Periodo: De: " + DataMovimentacaoInicial + " Até: " + DataMovimentacaoFinal;

                    RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
                    RowFiltroCaixa.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
                    RowFiltroCaixa.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));


                    FrmVendaGrupo FrmVendaG = new FrmVendaGrupo();
                    FrmVendaG.LIS_PEDIDOFiltroPrintColl = LIS_PEDIDOP.ReadCollectionByParameter(RowFiltroCaixa, "DTEMISSAO");
                    FrmVendaG.ShowDialog();

                }
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint,
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
            }

        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                 tabControlPedidoVenda.SelectTab(2);
            }
            else
            {
                FrmVendaGrupo FrmVendaG = new FrmVendaGrupo();
                FrmVendaG.LIS_PEDIDOFiltroPrintColl = LIS_PEDIDOColl;
                FrmVendaG.ShowDialog();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg("Antes de adicionar o Financeiro é necessário gravar o Pedido!", "Red");
                
            }
            else
            {
                if (ValidaDuplicatas())
                {
                    if (chkCartaoCredito.Checked)
                    {
                        DialogResult dr2 = MessageBox.Show("A opção Cartão de Crédito/Cheque está selecionada, as duplicatas serão lançadas como pagas!, deseja continuar?",
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr2 == DialogResult.Yes)
                        {
                            if (ValidaDuplicatasVencidas())
                            {
                                try
                                {
                                    SaveDuplicatasPaga();

                                    //Salva a forma de pagamento no Pedido
                                    PEDIDOP.Save(Entity);

                                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                                }
                                catch (Exception ex)
                                {
                                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                                    MessageBox.Show("Erro técnico: " + ex.Message);
                                }
                            }
                        }

                    }
                    else
                    {
                        try
                        {
                            SaveDuplicatas();

                            //Salva a forma de pagamento no Pedido
                            PEDIDOP.Save(Entity);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmVariosLancamentoReceberPedido frm = new FrmVariosLancamentoReceberPedido())
                {
                    frm.CodCliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.NumPedido = txtNPedido.Text;
                    frm.DataPedido = msktDataEmissao.Text;
                    frm.ValorPedido = txtValorDev.Text;
                    frm.NotaFiscal = "PD" + txtNPedido.Text;
                    frm.ShowDialog();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
            }
        }

        private void produtoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoCliente frm = new FrmProdutoCliente())
            {
                frm.ShowDialog();
            }
        }

        private void pesquisaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeralProduto();
            }
        }

        string DataMovimentacaoInicial = string.Empty;
        string DataMovimentacaoFinal = string.Empty;
        private void períodoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataMovimentacaoInicial = InputBox("Data Movimentação Inicial", ConfigSistema1.Default.NomeEmpresa, DateTime.Now.ToString("dd/MM/yyyy"));
            DataMovimentacaoFinal = InputBox("Data Movimentação Final", ConfigSistema1.Default.NomeEmpresa, DateTime.Now.ToString("dd/MM/yyyy"));

            if (!ValidacoesLibrary.ValidaTipoDateTime(DataMovimentacaoInicial))
                MessageBox.Show("Erro na Data Inicial: " + ConfigMessage.Default.MsgDataInvalida);
            else if (!ValidacoesLibrary.ValidaTipoDateTime(DataMovimentacaoFinal))
                MessageBox.Show("Erro na Data Final: " + ConfigMessage.Default.MsgDataInvalida);
            else
            {
                RelatorioTitulo = "Movimentação do Periodo: De: " + DataMovimentacaoInicial + " Até: " + DataMovimentacaoFinal;

                RowsFiltroCollection RowFiltro = new RowsFiltroCollection();
                RowFiltro.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
                RowFiltro.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));

                LIS_PEDIDOColl.Clear();
                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowFiltro, "DTEMISSAO");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_PEDIDOTy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_PEDIDOTy.VALORACRESCIMO = SumTotalPesquisa("VALORACRESCIMO");
                LIS_PEDIDOTy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                if (LIS_PEDIDOColl.Count > 0)
                    ImprimirListaGeralProduto();
                else
                {
                    MessageBox.Show("Não existem registros neste período!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                }
            }
            
        }

        private void DataGriewDados_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {


            MessageBox.Show(e.Exception.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
        }

        private void controleDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmControleEntrega frm = new FrmControleEntrega())
                {
                    frm._IDPEDIDO = _IDPEDIDO;
                    frm.DatePedido = msktDataEmissao.Text;
                    frm.ShowDialog();
                }
            }
        }

        private void controleDeEntregaMT2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmControleEntregaMT2 frm = new FrmControleEntregaMT2())
                {
                    frm._IDPEDIDO = _IDPEDIDO;
                    frm.DatePedido = msktDataEmissao.Text;
                    frm.ShowDialog();
                }
            }
        }

        private void modelo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                _FLAGORCAMENTO = false;
                ImprimirPedidoModelo2();
            }
        }

        private void ImprimirPedidoModelo2()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument7_1;
          

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                
                objPrintPreview.Document = printDocument7_1;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }

            if (txtObsAnexo.Text.TrimEnd().Length > 0)
                ImprimiObsAnexo();
        }

        int _line = 0;
        int _line_1 = 0;
        int itemproduto = 1;
        private void printDocument7_1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //inicio Cabeçalho
            ConfigReportStandard config = new ConfigReportStandard();
        


            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
            LIS_PEDIDOCollection LIS_PEDIDOCollPrint = new LIS_PEDIDOCollection();
            LIS_PEDIDOCollPrint = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            config.MargemDireita = 760;

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
                 
                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 530, 35, 180, 80);
                   
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE + " Bairro: " + EMPRESATy.BAIRRO + " CEP: " + EMPRESATy.CEP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            
            if (CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == "N" || CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == string.Empty)
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


            if (!rdOrcamento.Checked)
            {
                e.Graphics.DrawString("Nº PEDIDO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 38);
                e.Graphics.DrawString(Entity.IDPEDIDO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 53);
            }
            else
            {
                e.Graphics.DrawString("Nº ORÇAMENTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 38);
                e.Graphics.DrawString(Entity.IDPEDIDO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 53);
            }


            if (txtNumReferencia.Text.TrimEnd() != string.Empty)
            {
                e.Graphics.DrawString("Nº Referência", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 68);
                e.Graphics.DrawString(txtNumReferencia.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 83);
            }

            //Dados Cliente
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);


            //Armazena dados do cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            e.Graphics.DrawString("Nome:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 140);
            e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
            e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

            e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
            e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

            e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].MUNICIPIO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
            e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].UF, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

            e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

            e.Graphics.DrawString("Compl.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 185);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].COMPLEMENTO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 185);

            string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
            e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);

            //Fim Cabeçalho


            // inicio Corpo Pedido
            //Alinhamento dos valores
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;

            float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
            float yLineTop = e.MarginBounds.Top + 80;         
          

            e.Graphics.DrawString("Quant", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 250);
            e.Graphics.DrawString("MT Lin", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, 250);
            e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, 250);
            e.Graphics.DrawString("Vlr.Unit", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, 250);
            e.Graphics.DrawString("Vlr.Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, 250);
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 270, config.MargemDireita + 30, 270);
            
            StringFormat formataString = new StringFormat();
            formataString.Alignment = StringAlignment.Far;
            formataString.LineAlignment = StringAlignment.Far;

            if (!chkDetalProdFinal.Checked)
            {
                for (; _line < LIS_PRODUTOSPEDIDOColl.Count; _line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 200) - 100)
                    {
                        //Rodape
                        paginaAtual++;
                        // e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                        //e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 120));

                        e.HasMorePages = true;
                        return;
                    }

                    if (chkTodoProdImpressao.Checked || LIS_PRODUTOSPEDIDOColl[_line].FLAGEXIBIR.ToUpper().TrimEnd() == "S" || LIS_PRODUTOSPEDIDOColl[_line].FLAGEXIBIR.TrimEnd() == string.Empty)
                    {
                        yLineTop += lineHeight;
  
                        e.Graphics.DrawString(LIS_PRODUTOSPEDIDOColl[_line].QUANTIDADE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 30, yLineTop + 80);
                        e.Graphics.DrawString(LIS_PRODUTOSPEDIDOColl[_line].TOTALMT.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 90, yLineTop + 80);

                        if (LIS_PRODUTOSPEDIDOColl[_line].IDCOR > 0)
                        {
                            if (LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS.Length > 0)
                                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.ToUpper() + " " + LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS + " - Cor: " + LIS_PRODUTOSPEDIDOColl[_line].NOMECOR.ToUpper(), 230), 53), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 80);
                            else
                                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.ToUpper() + " - Cor: " + LIS_PRODUTOSPEDIDOColl[_line].NOMECOR.ToUpper(), 200), 55), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 80);
                        }
                        else
                            e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.ToUpper() + " " + LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS, 200), 55), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 80);


                        e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOColl[_line].VALORUNITARIO).ToString("n4"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 580, yLineTop + 80);
                        e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOColl[_line].VALORTOTAL).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 650, yLineTop + 80);

 
                        if (LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOColl[_line].NOMECOR.Length + LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS.Length > 35 && LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOColl[_line].NOMECOR.Length + LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS.Length <= 90)
                        {
                            yLineTop += lineHeight;
                        }
                        if ((LIS_PRODUTOSPEDIDOColl[_line].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOColl[_line].NOMECOR.Length + LIS_PRODUTOSPEDIDOColl[_line].DADOSADICIONAIS.Length) >= 90)
                        {
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                        }
                    }
                }
            }
            else
            {
                yLineTop += lineHeight;
                e.Graphics.DrawString(txtDetalhesProdFinal.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 80);
            }

              if (!chkDetalProdFinal.Checked)
            {

            yLineTop += lineHeight;
            if (LIS_PRODUTOSPEDIDOMTQColl.Count > 0)
            {
                //campos a serem impressos            
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 110, config.MargemDireita + 30, yLineTop + 110);
                e.Graphics.DrawString("Quant", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + 120);
                e.Graphics.DrawString("Altura", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, yLineTop + 120);
                e.Graphics.DrawString("Largura", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 120);
                e.Graphics.DrawString("Total M2", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 220, yLineTop + 120);
                e.Graphics.DrawString("Descricao", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 290, yLineTop + 120);

                if (chkValorMetro.Checked)
                    e.Graphics.DrawString("Vl. MT", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, yLineTop + 120);
                else
                    e.Graphics.DrawString("Vlr.Unit", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, yLineTop + 120);

                e.Graphics.DrawString("Vlr.Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, yLineTop + 120);
             //   e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 140, config.MargemDireita + 30, yLineTop + 140);


                for (; _line_1 < LIS_PRODUTOSPEDIDOMTQColl.Count; _line_1++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 200) - 110)
                    {
                        //Rodape
                        paginaAtual++;
                        // e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 130));
                        e.HasMorePages = true;
                        return;
                    }

                    yLineTop += lineHeight;
                    if (chkTodoProdImpressao2.Checked || LIS_PRODUTOSPEDIDOMTQColl[_line_1].FLAGEXIBIR.ToUpper().TrimEnd() == "S" || LIS_PRODUTOSPEDIDOMTQColl[_line_1].FLAGEXIBIR.TrimEnd() == string.Empty)
                    {

           
                        e.Graphics.DrawString(LIS_PRODUTOSPEDIDOMTQColl[_line_1].QUANTIDADE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 30, yLineTop + 130);
                        e.Graphics.DrawString(Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].ALTURA).ToString("n4"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 85, yLineTop + 130);
                        e.Graphics.DrawString(Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].LARGURA).ToString("n4"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 130);
                        e.Graphics.DrawString((Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].MT2) * Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].QUANTIDADE)).ToString("n4"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 220, yLineTop + 130);


                        if (LIS_PRODUTOSPEDIDOMTQColl[_line_1].IDCOR > 0)
                        {
                            if (LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS.Length > 0)
                                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.ToUpper() + " " + LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS + " - Cor: " + LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMECOR.ToUpper(), 250), 35), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 290, yLineTop + 130);
                            else
                                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.ToUpper() + "- Cor: " + LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMECOR.ToUpper(), 250), 35), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 290, yLineTop + 130);
                        }
                        else
                            e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.ToUpper() + LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS, 250), 35), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 290, yLineTop + 130);


                        if (!chkValorMetro.Checked)
                            e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].VALORUNITARIO).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 630, yLineTop + 145, stringFormat);
                        else

                            e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].VALORMETRO).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 630, yLineTop + 145, stringFormat);


                        e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQColl[_line_1].VALORTOTAL).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 730, yLineTop + 145, stringFormat);


                        if ((LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMECOR.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS.Length) > 35 && (LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMECOR.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS.Length) < 90)
                        {
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                        }
                        else if ((LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMEPRODUTO.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].NOMECOR.Length + LIS_PRODUTOSPEDIDOMTQColl[_line_1].DADOADICIONAIS.Length) >= 100)
                        {
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                        }
                        else
                            yLineTop += lineHeight;                   

                    }
                }
            }
            }
            // Fim Corpo Pedido

            
            //Ultima Pagina
           // e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 130));
          //  e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);

            int linha = 170;
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, yLineTop + linha, config.MargemDireita - 250, 80);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, yLineTop + linha, 225, 130);
            linha = linha + 5;
            e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, yLineTop + linha);

            e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            linha = linha + 15;
            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, yLineTop + linha);


            linha = linha + 15;

            e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, yLineTop + linha);

            e.Graphics.DrawString("Produto MT2:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            linha = linha + 15;

            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtTotalProdMTQ.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha);
            e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, yLineTop + linha);

            linha = linha + 15;
            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);

            linha = linha + 15;
            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);


            e.Graphics.DrawString("Total Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            linha = linha + 15;

            if (chkvalortotal.Checked)
                e.Graphics.DrawString(txtTotalPedido.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtTotalPedido.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            e.Graphics.DrawString("Total Pago:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            linha = linha + 15;

            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtValorPago.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            e.Graphics.DrawString("Valor Devedor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, yLineTop + linha);
            linha = linha + 15;

            if (!chkvalortotal.Checked)
                e.Graphics.DrawString(chkExibirValores.Checked ? "0,00" : txtValorDev.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);
            else
                e.Graphics.DrawString("0,00", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, yLineTop + linha, stringFormat);

            //Observação
            linha = linha + 10;
          
            //Final da linha
            //linha = linha + 110;
            e.Graphics.DrawString("Assinatura do Cliente: _________________________________________________", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha + 10);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, yLineTop + linha);

            linha = linha +45;
            e.Graphics.DrawString("Local de Entrega/Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 10, yLineTop + linha);
            linha = linha + 15;

             e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 450), 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 10, yLineTop + linha);
         //   e.Graphics.DrawString(txtObservacao.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, yLineTop + linha);

           

            e.HasMorePages = false;
        }

        private void printDocument7_1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _line = 0;
            _line_1 = 0;
            itemproduto = 1;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show("Antes de adicionar composições é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmSearchProdComposicao frm = new FrmSearchProdComposicao())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        //AddProduto(result);
                        AddProdutoComposicaoProduto(result);
                    }
                }
            }
        }

        private void AddProduto(int idproduto)
        {
                cbProduto.SelectedValue = idproduto;
                txtQuanProduto.Text = Convert.ToDecimal("1").ToString("n4");

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = idproduto;
                PRODORCAMENTOTy.QUANTIDADE = 1;


                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(idproduto));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = 1 * PRODUTOStY.VALORVENDA1;
                txtValorMtLinearTotal.Text = Convert.ToDecimal(PRODORCAMENTOTy.VALORTOTAL).ToString("n2");
                cbProdutoFinal.SelectedValue = idproduto;

                //Salva o produtos no Pedido
                PRODUTOSPEDIDOP.Save(Entity2);             


            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);
           
            cbProduto.SelectedValue = idproduto;
        }

        private void AddProdutoComposicaoProduto(int IDPRODUTOMAIN)
        {
            //Filtras os produtos da determinada composicao
            RowsFiltroCollection ProdutoComposicao = new RowsFiltroCollection();
            ProdutoComposicao.Add(new RowsFiltro("IDPRODUTOMAIN", "System.Int32", "=", IDPRODUTOMAIN.ToString()));
            LIS_PRODUTOCOMPOSICAOColl = LIS_PRODUTOCOMPOSICAOP.ReadCollectionByParameter(ProdutoComposicao, "IDPRODUTOMAIN");
            

            //Percorre os produtos da coleção
            foreach (LIS_PRODUTOCOMPOSICAOEntity item in LIS_PRODUTOCOMPOSICAOColl)
            {
                cbProduto.SelectedValue = item.IDPRODUTO;
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;
                txtValorMtLinearTotal.Text = Convert.ToDecimal(PRODORCAMENTOTy.VALORTOTAL).ToString("n2");
                cbProdutoFinal.SelectedValue = item.IDPRODUTOMAIN;
                chkExibirProd.Checked = item.FLAGEXIBIR.TrimEnd() == "N" ? true : false;

                //Salva o produtos no Pedido
                PRODUTOSPEDIDOP.Save(Entity2);               
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);         

            cbProduto.SelectedValue = IDPRODUTOMAIN;
        }

        private void DGDadosProduto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ValueCell = DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                if(e.ColumnIndex == 2 )
                {
                    if ( ValueCell != "S" && ValueCell != "N" )
                    {
                        MessageBox.Show("Digite apenas S ou N!");
                        DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                    }
                    else
                    {
                        DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ValueCell;

                        PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy2 = new PRODUTOSPEDIDOEntity();
                        PRODUTOSPEDIDOTy2 = PRODUTOSPEDIDOP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[e.RowIndex].IDPRODPEDIDO));
                        PRODUTOSPEDIDOTy2.FLAGEXIBIR = ValueCell;
                        PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void reciboAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmReciboAvulso frm = new FrmReciboAvulso())
            {
                frm.CodClienteSelec = Convert.ToInt32(cbCliente.SelectedValue);
                frm.valorRecibo = Convert.ToDecimal(txtValorPago.Text);
                frm.ShowDialog();
            }
        }

        private void dtgProdMTQ_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ValueCell = dtgProdMTQ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                if (ValueCell != "S" && ValueCell != "N")
                {
                    MessageBox.Show("Digite apenas S ou N!");
                    DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                }
                else
                {
                    //DGDadosProduto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ValueCell;

                    PRODUTOSPEDIDOMTQEntity PRODUTOSPEDIDOMTQETy2 = new PRODUTOSPEDIDOMTQEntity();
                    PRODUTOSPEDIDOMTQETy2 = PRODUTOSPEDIDOMTQP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQColl[e.RowIndex].IDPRODUTOSPEDIDOMTQ));
                    PRODUTOSPEDIDOMTQETy2.FLAGEXIBIR = ValueCell.ToUpper();
                    PRODUTOSPEDIDOMTQP.Save(PRODUTOSPEDIDOMTQETy2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }      

        private void printDocument2_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;
            ConfigReportStandard config = new ConfigReportStandard();

            if (rdVenda.Checked)
                e.Graphics.DrawString("Nº PEDIDO : " + txtNPedido.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            else
                e.Graphics.DrawString("Nº ORÇAMENTO : " + txtNPedido.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);

            e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 38);
            e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 290, 38);

            string stringToPrint = txtObsAnexo.Text;

            // Sets the value of charactersOnPage to the number of characters  
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void txtObsAnexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int TotalCaracte = txtObsAnexo.MaxLength;
            lblCaractere.Text = "Total de caractere restante: " + (TotalCaracte - txtObsAnexo.Text.Length).ToString();
        }
            

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
            }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show("Antes de adicionar composições é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmSearchProdComposicao frm = new FrmSearchProdComposicao())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                       // AddProduto2(result);
                        AddProdutoComposicaoProduto2(result);
                    }
                }
            }
        }

        private void AddProduto2(int idproduto)
        {
            cbProdutoMTQ.SelectedValue = idproduto;
            txtQuantMTQ.Text = Convert.ToDecimal("1").ToString("n4");

            PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
            PRODORCAMENTOTy.IDPRODUTO = idproduto;
            PRODORCAMENTOTy.QUANTIDADE = 1;


            //Obtem o valor de venda do produto
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(idproduto));
            txtvalorunitMTQ.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
            PRODORCAMENTOTy.VALORTOTAL = 1 * PRODUTOStY.VALORVENDA1;
            txtVlTotalMTQ.Text = Convert.ToDecimal(PRODORCAMENTOTy.VALORTOTAL).ToString("n2");

            cbProdutoFinal.SelectedValue = idproduto;

            PRODUTOSPEDIDOMTQP.Save(Entity6);         

            ////Lista os produtos do Pedido
            ListaProdutoPedidoMTQ(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);
                   

            cbProduto.SelectedValue = idproduto;
        }

        private void AddProdutoComposicaoProduto2(int IDPRODUTOMAIN)
        {
            //Filtras os produtos da determinada composicao
            RowsFiltroCollection ProdutoComposicao = new RowsFiltroCollection();
            ProdutoComposicao.Add(new RowsFiltro("IDPRODUTOMAIN", "System.Int32", "=", IDPRODUTOMAIN.ToString()));
            LIS_PRODUTOCOMPOSICAOColl = LIS_PRODUTOCOMPOSICAOP.ReadCollectionByParameter(ProdutoComposicao, "IDPRODUTOMAIN");


            //Percorre os produtos da coleção
            foreach (LIS_PRODUTOCOMPOSICAOEntity item in LIS_PRODUTOCOMPOSICAOColl)
            {
                cbProdutoMTQ.SelectedValue = item.IDPRODUTO;
                txtQuantMTQ.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;


                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtvalorunitMTQ.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;
                txtVlTotalMTQ.Text = Convert.ToDecimal(PRODORCAMENTOTy.VALORTOTAL).ToString("n2");
                chkExibirProd.Checked = item.FLAGEXIBIR.TrimEnd() == "N" ? true : false;
                cbProdutoFinal.SelectedValue = item.IDPRODUTOMAIN;

                //Salva o produtos no Pedido
                PRODUTOSPEDIDOMTQP.Save(Entity6);              
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedidoMTQ(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);         

            cbProdutoMTQ.SelectedValue = IDPRODUTOMAIN;
        }

        private void vendaPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorPed1 FrmVenOtica = new FrmVendaVendedorPed1();
            FrmVenOtica.ShowDialog();
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
        {
            monthCalendar3.Name = "monthCalendar1";
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtTotalMTQ_Enter(object sender, EventArgs e)
        {
            //PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            //PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue));

            //if (PRODUTOSTy != null)
            //{
            //    if(PRODUTOSTy.FLAGDECIMALREND.TrimEnd() == "S");
            //    {
            //       // txtTotalMTQ.Text = Convert.ToDecimal(txtTotalMTQ.Text) PRODUTOSTy.MULTAREND 
            //    }
            //}

        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirReceitaReport();
            }
        }

        private void ImprimirReceitaReport()
        {
            using (FrmRelatPedidoVenda3 frm = new FrmRelatPedidoVenda3())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();

                if (txtObsAnexo.Text.TrimEnd().Length > 0)
                    ImprimiObsAnexo();
            }
        }

        private void ImprimirReceitaReport2()
        {
            using (FrmRelatPedidoVendas2 frm = new FrmRelatPedidoVendas2())
            {
                PEDIDOP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();

                if (txtObsAnexo.Text.TrimEnd().Length > 0)
                    ImprimiObsAnexo();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnCadAmbiente_Click(object sender, EventArgs e)
        {
            using (FrmAmbiente frm = new FrmAmbiente())
            {
                int CodSelec = Convert.ToInt32(cbAmbiente.SelectedValue);
                frm._IDAMBIENTE = CodSelec;
                frm.ShowDialog();
                GetDropAmbiente();
                GetDropAmbiente2();
                cbAmbiente.SelectedValue = CodSelec;
            }
        }

        private void GetDropAmbiente()
        {
            try
            {
                AMBIENTECollection AMBIENTEColl = new AMBIENTECollection();
                AMBIENTEProvider AMBIENTEP = new AMBIENTEProvider();

                cbAmbiente.DisplayMember = "NOME";
                cbAmbiente.ValueMember = "IDAMBIENTE";

                AMBIENTEColl = AMBIENTEP.ReadCollectionByParameter(null, "NOME");

                AMBIENTEEntity AMBIENTETy = new AMBIENTEEntity();
                AMBIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                AMBIENTETy.IDAMBIENTE = -1;
                AMBIENTEColl.Add(AMBIENTETy);

                Phydeaux.Utilities.DynamicComparer<AMBIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<AMBIENTEEntity>(cbAmbiente.DisplayMember);

                AMBIENTEColl.Sort(comparer.Comparer);
                cbAmbiente.DataSource = AMBIENTEColl;

                cbAmbiente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCadAmbiente2_Click(object sender, EventArgs e)
        {
            using (FrmAmbiente frm = new FrmAmbiente())
            {
                int CodSelec = Convert.ToInt32(cbambiente2.SelectedValue);
                frm._IDAMBIENTE = CodSelec;
                frm.ShowDialog();
                
                GetDropAmbiente2();
                GetDropAmbiente();
                cbambiente2.SelectedValue = CodSelec;
            }
        }

        private void GetDropAmbiente2()
        {
            try
            {
                AMBIENTECollection AMBIENTE2Coll = new AMBIENTECollection();
                AMBIENTEProvider AMBIENTEP = new AMBIENTEProvider();

                cbambiente2.DisplayMember = "NOME";
                cbambiente2.ValueMember = "IDAMBIENTE";

                AMBIENTE2Coll = AMBIENTEP.ReadCollectionByParameter(null, "NOME");

                AMBIENTEEntity AMBIENTETy = new AMBIENTEEntity();
                AMBIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                AMBIENTETy.IDAMBIENTE = -1;
                AMBIENTE2Coll.Add(AMBIENTETy);

                Phydeaux.Utilities.DynamicComparer<AMBIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<AMBIENTEEntity>(cbambiente2.DisplayMember);

                AMBIENTE2Coll.Sort(comparer.Comparer);
                cbambiente2.DataSource = AMBIENTE2Coll;

                cbambiente2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void modelo3GrupoAmbienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirReceitaReportGrupo();
            }
        }

        private void ImprimirReceitaReportGrupo()
        {
            using (FrmRelatPedidoVendasGrupo frm = new FrmRelatPedidoVendasGrupo())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImprimiObsAnexo();
        }

        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos  (*.*)|*.*"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                lblNameFile.Text = openFileDialog1.FileName.ToString();
                _ArquivoPDF = GetFoto(openFileDialog1.FileName);
                txtNomeImagem.Text = System.IO.Path.GetFileName(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnAddImagem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show("Antes de adicionar o arquivo PDF é necessário gravar o Cliente!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                try
                {
                    if (txtNomeImagem.Text != string.Empty)
                    {
                        ARQUIVOPEDIDOP.Save(Entity3);
                        ListaArquivoPedido(_IDPEDIDO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Entity3 = null;
                        errorProvider1.Clear();
                    }
                    else
                    {
                        errorProvider1.SetError(txtNomeImagem, ConfigMessage.Default.CampoObrigatorio);
                        Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private void ListaArquivoPedido(int _IDPEDIDO)
        {
            try
            {
                
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                ARQUIVOPEDIDOColl = ARQUIVOPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                dtgImag.AutoGenerateColumns = false;
                dtgImag.DataSource = ARQUIVOPEDIDOColl;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void dtgImag_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (ARQUIVOPEDIDOColl.Count > 0 && rowindex > -1)
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
                            CodSelect = Convert.ToInt32(ARQUIVOPEDIDOColl[rowindex].IDARQUIVOPEDIDO);
                            ARQUIVOPEDIDOP.Delete(CodSelect);
                            ListaArquivoPedido(_IDPEDIDO);
                            Entity2 = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //Abrir
                {
                    CodSelect = Convert.ToInt32(ARQUIVOPEDIDOColl[rowindex].IDARQUIVOPEDIDO);
                    _ArquivoPDF = ARQUIVOPEDIDOP.Read(CodSelect).FOTO;
                    string NomeArquivo = ARQUIVOPEDIDOColl[rowindex].NOME;

                    string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                    StreamWriter oStreamWriter = new StreamWriter(strPath + @"\" + NomeArquivo);

                    oStreamWriter.BaseStream.Write(_ArquivoPDF, 0, _ArquivoPDF.Length);

                    oStreamWriter.Close();
                    oStreamWriter.Dispose();

                    if (File.Exists(strPath + @"\" + NomeArquivo))
                    {
                        System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(strPath + @"\" + NomeArquivo);
                    }

                }

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void modelo412FolhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirReceitaReport2();
            }
        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void comissãoDeTerceirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            { 
                using (FrmComissaoTerceiro frm = new FrmComissaoTerceiro())
                {
                    frm._IDPEDIDO = _IDPEDIDO;
                    frm.ShowDialog();
                }
            }
        }

        private void comissãoDeTerceirosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRelatComissaoTerceiros Frm = new FrmRelatComissaoTerceiros();
            Frm.ShowDialog();
        }

        private void modelo5MatériaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    PEDIDOP.Save(Entity);
                    ImprimirReceitaMateriaPrima();
                    BloqueoTela();
                }
                else
                {
                    string msgerro = "Tela bloqueada!";
                    errorProvider1.SetError(chkTelaBloqueada, msgerro);
                    Util.ExibirMSg(msgerro, "Red");
                }
            }
        }

        private void BloqueoTela()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("nomeformulario", "System.String", "=", this.Name));
                LIS_BLOQUEIOTELACollection LIS_BLOQUEIOTELAColl = new LIS_BLOQUEIOTELACollection();
                LIS_BLOQUEIOTELAProvider LIS_BLOQUEIOTELAP = new LIS_BLOQUEIOTELAProvider();
                LIS_BLOQUEIOTELAColl = LIS_BLOQUEIOTELAP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_BLOQUEIOTELAColl.Count > 0)
                {
                    PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                    PEDIDOTy = PEDIDOP.Read(_IDPEDIDO);
                    PEDIDOTy.FLAGTELABLOQUEADA = "S";
                    PEDIDOP.Save(PEDIDOTy);

                    chkTelaBloqueada.Checked = true;
                    chkTelaBloqueada.Text = "Tela Bloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void ImprimirReceitaMateriaPrima()
        {
            using (FrmRelatPedidoMateriaPrima frm = new FrmRelatPedidoMateriaPrima())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ExibirValorProduto = chkExibirValores.Checked ? "true" : "false";
                frm.ExibirMatePrima = chkDetalProdFinal.Checked ? "false" : "true";
                frm.ShowDialog();
            }
        }

        private void btnCadProdFinal_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProdutoFinal.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutoFinal();
                cbProdutoFinal.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutoFinal()
        {
            try
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSFinalColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

                cbProdutoFinal.DisplayMember = "NOMEPRODUTO";
                cbProdutoFinal.ValueMember = "IDPRODUTO";

                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                PRODUTOSFinalColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProdutoFinal.DisplayMember);

                PRODUTOSFinalColl.Sort(comparer.Comparer);
                cbProdutoFinal.DataSource = PRODUTOSFinalColl;

                cbProdutoFinal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbProdutoFinal_KeyDown(object sender, KeyEventArgs e)
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
                        cbProdutoFinal.SelectedValue = result;
                    }
                }
            }
        }

        private void cbProdutoFinal_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void produtoPorMatériPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void duplicarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente duplicar este pedido?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {

                        //Busca Produto MT2
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                        PRODUTOSPEDIDOMTQCollection PRODUTOSPEDIDOMTQColl = new PRODUTOSPEDIDOMTQCollection();
                        PRODUTOSPEDIDOMTQColl = PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                        //Busca Produto Linear
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                        PRODUTOSPEDIDOCollection PRODUTOSPEDIDOColl = new PRODUTOSPEDIDOCollection();
                        PRODUTOSPEDIDOColl = PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                        _IDPEDIDO = -1;
                        _IDPEDIDO = PEDIDOP.Save(Entity);

                        //Salva Produtos MT2
                        foreach (PRODUTOSPEDIDOMTQEntity item in PRODUTOSPEDIDOMTQColl)
                        {
                            PRODUTOSPEDIDOMTQEntity PRODUTOSPEDIDOMTQTy = new PRODUTOSPEDIDOMTQEntity();
                            PRODUTOSPEDIDOMTQTy = PRODUTOSPEDIDOMTQP.Read(Convert.ToInt32(item.IDPRODUTOSPEDIDOMTQ));
                            PRODUTOSPEDIDOMTQTy.IDPRODUTOSPEDIDOMTQ = -1;
                            PRODUTOSPEDIDOMTQTy.IDPEDIDO = _IDPEDIDO;
                            PRODUTOSPEDIDOMTQP.Save(PRODUTOSPEDIDOMTQTy);
                        }

                        //Salva Produtos Linear
                        foreach (PRODUTOSPEDIDOEntity item in PRODUTOSPEDIDOColl)
                        {
                            PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                            PRODUTOSPEDIDOTy = PRODUTOSPEDIDOP.Read(Convert.ToInt32(item.IDPRODPEDIDO));
                            PRODUTOSPEDIDOTy.IDPRODPEDIDO = -1;
                            PRODUTOSPEDIDOTy.IDPEDIDO = _IDPEDIDO;
                            PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy);

                        }

                        Entity = PEDIDOP.Read(_IDPEDIDO);
                        ListaProdutoPedidoMTQ(_IDPEDIDO);
                        ListaProdutoPedido(_IDPEDIDO);

                        MessageBox.Show("Pedido Nº e " + _IDPEDIDO.ToString().PadLeft(6, '0') + " criado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível duplicar o pedido selecionado!");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }

        private void linkLabel4_LocationChanged(object sender, EventArgs e)
        {

        }

        private void txtPorcPerda_Validating(object sender, CancelEventArgs e)
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

        private void txtPerdaMT2_Validating(object sender, CancelEventArgs e)
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
                    TextBoxSelec.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,0000";
        }

        private void txtPorcPerda_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }
        private void SomaPerdaMT2()
        {
            try
            {
                if (ValidacoesLibrary.ValidaTipoDecimal(txtTotalMTQ.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtPorcPerda.Text) & ValidacoesLibrary.ValidaTipoDecimal(txtPerdaMT2.Text))
                {
                    decimal TotalMTQ = Convert.ToDecimal(txtTotalMTQ.Text);
                    decimal PorcPerda = Convert.ToDecimal(txtPorcPerda.Text);
                    decimal TotalPerda = Convert.ToDecimal(txtPerdaMT2.Text);
                    decimal TotalMT2Perda = 0;

                    if (PorcPerda > 0)
                        TotalPerda = (TotalMTQ * PorcPerda) / 100;

                    txtPerdaMT2.Text = TotalPerda.ToString("n2");
                    TotalMT2Perda = TotalPerda + TotalMTQ;

                    txtTotalMt2Perda.Text = TotalMT2Perda.ToString("n2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void txtAlturaMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtLarguraMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtPerdaMT2_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtTotalMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void chkTelaBloqueada_Click(object sender, EventArgs e)
        {
            if (!chkTelaBloqueada.Checked)
            {
                DialogResult dr = MessageBox.Show("Deseja desbloquear esta tela?",
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DesBloqueoTela();
                }
                else
                    chkTelaBloqueada.Checked = !chkTelaBloqueada.Checked;
            }
            else
                chkTelaBloqueada.Checked = !chkTelaBloqueada.Checked;
        }

        private void DesBloqueoTela()
        {
            try
            {
                int IDUSUARIO = -1;
                using (FrmLiberacaoTela frm = new FrmLiberacaoTela())
                {
                    frm.ShowDialog();
                    IDUSUARIO = frm._idusuario;
                }

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("nomeformulario", "System.String", "=", this.Name));
                RowRelatorio.Add(new RowsFiltro("IDUSUARIO", "System.Int32", "=", IDUSUARIO.ToString()));
                LIS_BLOQUEIOTELACollection LIS_BLOQUEIOTELAColl = new LIS_BLOQUEIOTELACollection();
                LIS_BLOQUEIOTELAProvider LIS_BLOQUEIOTELAP = new LIS_BLOQUEIOTELAProvider();
                LIS_BLOQUEIOTELAColl = LIS_BLOQUEIOTELAP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_BLOQUEIOTELAColl.Count > 0)
                {
                    PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                    PEDIDOTy = PEDIDOP.Read(_IDPEDIDO);
                    PEDIDOTy.FLAGTELABLOQUEADA = "N";
                    PEDIDOP.Save(PEDIDOTy);

                    chkTelaBloqueada.Checked = false;
                    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;

                    Util.ExibirMSg("Tela desbloqueada com sucesso", "Blue");

                }
                else
                {
                    chkTelaBloqueada.Checked = !chkTelaBloqueada.Checked;
                    MessageBox.Show("Usuário sem permissão para desbloqueio da tela!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void cbProdutoFinal_KeyDown_1(object sender, KeyEventArgs e)
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
                        cbProdutoFinal.SelectedValue = result;
                    }
                }
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
                frm.TituloSelec = "Relação de Pedidos";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                EnviarEmail();
            }
        }

        private void EnviarEmail()
        {
            try
            {
                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTETy = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue));

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESATy = EMPRESAP.Read(1);

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //var fromAddress = new MailAddress("imexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
               // var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var toAddress = new MailAddress(CLIENTETy.EMAILCLIENTE, CLIENTETy.NOME);
                //const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string subject = string.Empty;
                subject = "Pedido Nº " + txtNPedido.Text;


                string body = string.Empty;
                body = "Caro(a) cliente " + CLIENTETy.NOME + ", segue abaixo dados do Pedido/Orçamento <br>";
                body += " " + "<br>";
                body += "Dados do Serviços/Produtos" + "<br>";

                StringBuilder html = new StringBuilder();
                html.Append("<html>");
                html.Append("<head>");
                html.Append("<body>");
                html.Append("<table>");
                //add header row
                html.Append("<tr>");
                html.Append("<td>Produto/Serviço </td>");
                html.Append("<td>Quant.</td>");
                html.Append("<td>Valor</td>");
                html.Append("</tr>");
                //add rows
                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }
                html.Append("<tr>");
                html.Append("<td></td>");
                html.Append("<td>Total: </td>");
                html.Append("<td align=right >" + txtTotalFinanceiro.Text + "</td>");
                html.Append("</tr>");

                html.Append("</table>");

                html.Append("</head>");
                html.Append("</body>");
                html.Append("</html>");
                body += html;

                body += "<br>";
                body += "--------------------------------------------------------------------------------------------------" + "<br>";

                if (txtPrazoEntrega.Text.Trim() != string.Empty)
                    body += "Prazo de Entrega: " + txtPrazoEntrega.Text + "<br>";

                if (Convert.ToInt32(cbFormaPagto.SelectedValue) > 0)
                {
                    body += "Forma de Pagto: " + cbFormaPagto.Text + "<br>";
                }

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                {
                    body += "Funcionário: " + cbFuncionario.Text + "<br>";
                }

                body += "--------------------------------------------------------------------------------------------------" + "<br>";
                body += EMPRESATy.NOMEFANTASIA + "<br>";
                body += EMPRESATy.TELEFONE + "<br>";
                body += EMPRESATy.EMAIL + "<br>";

                Boolean seguranEmail = false;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "S")
                    seguranEmail = true;
                var smtp = new SmtpClient
                {
                    //Host = "mail.imexsistema.com.br",
                    Host = "smtp.site.com.br",
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = seguranEmail, 
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {

                    DialogResult dr = MessageBox.Show("Deseja enviar o email para: " + CLIENTETy.EMAILCLIENTE + " ?",
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        smtp.Send(message);
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Email enviado com sucesso!");
                    }

                    this.Cursor = Cursors.Default;
                }




                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Não foi possível enviar o email!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information,
                             MessageBoxDefaultButton.Button1);


            }
        }
      
    }
}
