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
using BmsSoftware.Modulos.Servicos;
using System.Diagnostics;
using BmsSoftware.Classes.BMSworks.UI;
using System.Net.Mail;
using System.Net;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPedidoVendaOtica : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        string RelatorioTitulo = string.Empty;

        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        SERVICOCollection SERVICOColl = new SERVICOCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        MEDICOCollection MEDICOColl = new MEDICOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LIS_PEDIDOOTICACollection LIS_PEDIDOOTICAColl = new LIS_PEDIDOOTICACollection();
        LIS_SERVICOPEDOTICACollection LIS_SERVICOPEDOTICAColl = new LIS_SERVICOPEDOTICACollection();
        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();

        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();

        PEDIDOOTICAProvider PEDIDOOTICAP = new PEDIDOOTICAProvider();
        LIS_PEDIDOOTICAProvider LIS_PEDIDOOTICAP = new LIS_PEDIDOOTICAProvider();
        PRODUTOSPEDOTICAProvider PRODUTOSPEDOTICAP = new PRODUTOSPEDOTICAProvider();
        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDOTICAP = new LIS_PRODUTOSPEDOTICAProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        SERVICOPEDOTICAProvider SERVICOPEDOTICAP = new SERVICOPEDOTICAProvider();
        LIS_SERVICOPEDOTICAProvider LIS_SERVICOPEDOTICAP = new LIS_SERVICOPEDOTICAProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        DIAGPERTOPEDIDOProvider DIAGPERTOPEDIDOP = new DIAGPERTOPEDIDOProvider();
        DIAGMEDIOPEDIDOProvider DIAGMEDIOPEDIDOP = new DIAGMEDIOPEDIDOProvider();
        DIAGLONGEPEDIDOProvider DIAGLONGEPEDIDOP = new DIAGLONGEPEDIDOProvider();

        public Boolean ExibiDados = false;

        public FrmPedidoVendaOtica()
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

        public int _IDPEDIDOOTICA = -1;
        public PEDIDOOTICAEntity Entity
        {
            get
            {
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);

              
                DateTime? DTENTREGA = null;
                if(maskedtxtEntrega.Text != "  /  /")
                    DTENTREGA = Convert.ToDateTime(maskedtxtEntrega.Text);

                int IDSTATUS  = Convert.ToInt32(cbStatus.SelectedValue); ;
                string PRAZOENTREGA = txtPrazoEntrega.Text; 
                
                int? IDTRANSPORTES = null;
                if(cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                int? IDVENDEDOR = null;
                if (cbFuncionario.SelectedIndex > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

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
                decimal PORCACRESCIMO = Convert.ToDecimal(txtPorcAcrescimo.Text);

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

                int? IDCENTROCUSTO = null;
                if (cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int? IDMEDICO = null;
                if (cbMedico.SelectedIndex > 0)
                    IDMEDICO = Convert.ToInt32(cbMedico.SelectedValue);

                int? IDFORNECEDOR = null;
                if (cbLaboratorio.SelectedIndex > 0)
                    IDFORNECEDOR = Convert.ToInt32(cbLaboratorio.SelectedValue);

                string NUMREFERENCIA = txtNumReferencia.Text;
                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
               

                return new PEDIDOOTICAEntity( _IDPEDIDOOTICA, IDCLIENTE, DTEMISSAO, DTENTREGA,IDSTATUS,
                                             PRAZOENTREGA, IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO,
                                             OBSERVACAO, TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                             VALORDESCONTO, PORCACRESCIMO, VALORACRESCIMO, TOTALPEDIDO,
                                             IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR, IDLOCALCOBRANCA,
                                             IDCENTROCUSTO, IDMEDICO, IDFORNECEDOR, NUMREFERENCIA, FLAGORCAMENTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDOOTICA = value.IDPEDIDOOTICA;
                    ListaItensServico(_IDPEDIDOOTICA);
                    txtNPedido.Text = _IDPEDIDOOTICA.ToString().PadLeft(6, '0');
                    cbCliente.SelectedValue = value.IDCLIENTE;     
              
                    ListaDiagPertoPedido(_IDPEDIDOOTICA);
                    ListaDiagMedioPedido(_IDPEDIDOOTICA);
                    ListaDiagLongePedido(_IDPEDIDOOTICA);
                

                    if (value.DTENTREGA != null)
                        maskedtxtEntrega.Text = Convert.ToDateTime(value.DTENTREGA).ToString("dd/MM/yyyy");
                    else
                        maskedtxtEntrega.Text = "  /  /";
                        

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


                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    if (value.IDMEDICO != null)
                        cbMedico.SelectedValue = value.IDMEDICO;
                    else
                        cbMedico.SelectedIndex = 0;

                    if (value.IDFORNECEDOR != null)
                        cbLaboratorio.SelectedValue = value.IDFORNECEDOR;
                    else
                        cbLaboratorio.SelectedIndex = 0;

                    txtNumReferencia.Text = value.NUMREFERENCIA;

                    if (value.FLAGORCAMENTO != null && value.FLAGORCAMENTO.TrimEnd() == "S")
                        rdOrcamento.Checked = true;
                    else
                        rdOrcamento.Checked = false;

                    rdVenda.Checked = !rdOrcamento.Checked; 

                       
                    errorProvider1.Clear();
                }
                else
                {
                   _IDPEDIDOOTICA = -1;
                   ListaDiagPertoPedido(_IDPEDIDOOTICA);
                   ListaDiagMedioPedido(_IDPEDIDOOTICA);
                   ListaDiagLongePedido(_IDPEDIDOOTICA);
                
                    txtNPedido.Text = string.Empty;

                    //Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);

                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    maskedtxtEntrega.Text = "  /  /";
                    cbStatus.SelectedIndex = 0;
                    txtPrazoEntrega.Text = string.Empty;
                    cbTransportadora.SelectedIndex = 0;
                    cbFuncionario.SelectedIndex = 0;
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

                    cbMedico.SelectedIndex = 0;
                    txtPorComisVend.Text = "0,00";

                    cbLaboratorio.SelectedIndex = 0;
                    txtNumReferencia.Text = string.Empty;

                    rdOrcamento.Checked = false;
                    rdVenda.Checked = !rdOrcamento.Checked; 

                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODPEDOTICA = -1;
        public PRODUTOSPEDOTICAEntity Entity2
        {
            get
            {
              int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
              decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
              decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
              decimal VALORTOTAL = Convert.ToDecimal(txtVlTotalProduto.Text); 
              decimal COMISSAO = 0;

              //"S" para comissão sobre o total o pedido
              //"N" para comissão pelo total dos produto
              if (FLAGCOMISSAO == "N")
              {
                  decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                  COMISSAO = (VALORTOTAL * PorcComissao) / 100;
              }

              decimal PORCDECONTO = Convert.ToDecimal(txtDesconVlUnitaPorc.Text);
              decimal VLUNITLIQUIDO = Convert.ToDecimal(txtLiquido.Text);

              return new PRODUTOSPEDOTICAEntity(_IDPRODPEDOTICA, _IDPEDIDOOTICA, IDPRODUTO,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO,
                                                PORCDECONTO, VLUNITLIQUIDO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODPEDOTICA = value.IDPRODPEDOTICA;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtDesconVlUnitaPorc.Text = Convert.ToDecimal(value.PORCDECONTO).ToString("n2");
                    txtLiquido.Text = Convert.ToDecimal(value.VLUNITLIQUIDO).ToString("n2");
                    
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDOTICA = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = string.Empty;
                    txtValorUnitProd.Text = "1,00";
                    txtDesconVlUnitaPorc.Text = "0,00";
                    txtLiquido.Text = "0,00";
                    txtVlTotalProduto.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODPEDOTICA2 = -1;
        public SERVICOPEDOTICAEntity Entity2_1
        {
            get
            {
                 int IDSERVICO = Convert.ToInt32(cbServico.SelectedValue);//      INTEGER,
                 decimal QUANTIDADE = Convert.ToDecimal(txtQuanServico.Text);//     NUMERIC(15,2),
                 decimal VALORUNITARIO = Convert.ToDecimal(txtValorServico.Text);//     NUMERIC(15,2),
                 decimal VALORTOTAL = QUANTIDADE * VALORUNITARIO;

                 return new SERVICOPEDOTICAEntity(_IDPRODPEDOTICA2, _IDPEDIDOOTICA,  IDSERVICO, QUANTIDADE, VALORUNITARIO, VALORTOTAL);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODPEDOTICA2 = value.IDPRODPEDOTICA;
                    cbServico.SelectedValue = value.IDSERVICO;
                    txtQuanServico.Text = value.QUANTIDADE.ToString();
                    txtValorServico.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    CalculoSubTotalServico();
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDOTICA2 = -1;
                    ListaItensServico(_IDPEDIDOOTICA);
                    cbServico.SelectedValue = -1;
                    txtQuanServico.Text = "1";
                    txtValorServico.Text = "0,00";
                    CalculoSubTotalServico();
                    errorProvider1.Clear();
                }
            }
        }      
     

        int _IDDIAGPERTOPEDIDO = -1;
        public DIAGPERTOPEDIDOEntity Entity5
        {
            get
            {
                string DIRESFERICO = txtPertoEsfDire.Text;
                string DIRCILINDRICO = txtPertoDirCilindrico.Text;
                string DIREIXO = txtPertoDirEixo.Text;
                string DIRADICAO = txtDirPertoAdicao.Text;
                string DIRDNP = txtPertoDirDNP.Text;
                string DIRACO = txtPertoDirACO.Text;
                string ESQESFERICO = txtPertoEsqEsferico.Text;
                string ESQCILINDRICO = txtPertoEsqCilind.Text;
                string ESQEIXO = txtPertoEsqEixo.Text;
                string ESQADICAO = txtPertoEsqAdicao.Text;
                string ESQDNP = txtPertoEsqDNP.Text;
                string ESQACO = txtPertoEsqACO.Text;
                string LENTES = txtPertoLentes.Text;
                string ARMACAO = txtPertoArmacao.Text;
                string DISTANCIAPUPILAR = txtPertoDistPupilar.Text;
                string DIREITO = txtPertoDireito.Text;
                string ESQUERDO = txtPertoEsquerdo.Text;
                string DPA = txtPertoDPA.Text;
                string MD = txtPertoMD.Text;
                string MV = txtPertoMV.Text;

                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                return new DIAGPERTOPEDIDOEntity(_IDDIAGPERTOPEDIDO, _IDPEDIDOOTICA, DIRESFERICO, DIRCILINDRICO,
                                                  DIREIXO, DIRADICAO, DIRDNP, DIRACO, ESQESFERICO, ESQCILINDRICO,
                                                  ESQEIXO, ESQADICAO, ESQDNP, ESQACO, LENTES, ARMACAO,
                                                  DISTANCIAPUPILAR, DIREITO, ESQUERDO, DPA, MD, MV);


            }
            set
            {
                if (value != null)
                {
                    _IDDIAGPERTOPEDIDO = value.IDDIAGPERTOPEDIDO;
                    txtPertoEsfDire.Text = value.DIRESFERICO;
                    txtPertoDirCilindrico.Text = value.DIRCILINDRICO;
                    txtPertoDirEixo.Text = value.DIREIXO;
                    txtDirPertoAdicao.Text = value.DIRADICAO;
                    txtPertoDirDNP.Text = value.DIRDNP;
                    txtPertoDirACO.Text = value.DIRACO;
                    txtPertoEsqEsferico.Text = value.ESQESFERICO;
                    txtPertoEsqCilind.Text = value.ESQCILINDRICO;
                    txtPertoEsqEixo.Text = value.ESQEIXO;
                    txtPertoEsqAdicao.Text = value.ESQADICAO;
                    txtPertoEsqDNP.Text = value.ESQDNP;
                    txtPertoEsqACO.Text = value.ESQACO;
                    txtPertoLentes.Text = value.LENTES;
                    txtPertoArmacao.Text = value.ARMACAO;
                    txtPertoDistPupilar.Text = value.DISTANCIAPUPILAR;
                    txtPertoDireito.Text = value.DIREITO;
                    txtPertoEsquerdo.Text = value.ESQUERDO;
                    txtPertoDPA.Text = value.DPA;
                    txtPertoMD.Text = value.MD;
                    txtPertoMV.Text = value.MV;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDIAGPERTOPEDIDO = -1;
                    txtPertoEsfDire.Text = string.Empty;
                    txtPertoDirCilindrico.Text = string.Empty;
                    txtPertoDirEixo.Text = string.Empty;
                    txtDirPertoAdicao.Text = string.Empty;
                    txtPertoDirDNP.Text = string.Empty;
                    txtPertoDirACO.Text = string.Empty;
                    txtPertoEsqEsferico.Text = string.Empty;
                    txtPertoEsqCilind.Text = string.Empty;
                    txtPertoEsqEixo.Text = string.Empty;
                    txtPertoEsqAdicao.Text = string.Empty;
                    txtPertoEsqDNP.Text = string.Empty;
                    txtPertoEsqACO.Text = string.Empty;
                    txtPertoLentes.Text = string.Empty;
                    txtPertoArmacao.Text = string.Empty;
                    txtPertoDistPupilar.Text = string.Empty;
                    txtPertoDireito.Text = string.Empty;
                    txtPertoEsquerdo.Text = string.Empty;
                    txtPertoDPA.Text = string.Empty;
                    txtPertoMD.Text = string.Empty;
                    txtPertoMV.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }

        int _IDDIAGMEDIOPEDIDO = -1;
        public DIAGMEDIOPEDIDOEntity Entity6
        {
            get
            {
                string DIRESFERICO = txtMedDirEsferico.Text;
                string DIRCILINDRICO = txtMedDirCilindrico.Text;
                string DIREIXO = txtMedDirEixo.Text;
                string DIRADICAO = txtMedDireitaAdicao.Text;
                string DIRDNP = txtMedioDirDNP.Text;
                string DIRACO = txtMedioDirACO.Text; 
                string ESQESFERICO = txtMedEsqEsferico.Text;
                string ESQCILINDRICO = txtMedioEsqCilindrico.Text;
                string ESQEIXO = txtMedEsqEixo.Text;
                string ESQADICAO = txtMedEsqAdicao.Text;
                string ESQDNP = txtMedioEsqDNP.Text;
                string ESQACO = txtMedioEsqACO.Text;
                string LENTES = txtMedioLentes.Text;
                string ARMACAO = txtMedArmacao.Text;
                string DISTANCIAPUPILAR = txtMedioDistPupilar.Text;
                string DIREITO = txtMedDireito.Text;
                string ESQUERDO = txtMedEsquerdo.Text;
                string DPA = txtMedioDPA.Text;
                string MD = txtMedioMD.Text;
                string MV = txtMedioMV.Text;
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                return new DIAGMEDIOPEDIDOEntity(_IDDIAGMEDIOPEDIDO, _IDPEDIDOOTICA, DIRESFERICO, DIRCILINDRICO,
                                                  DIREIXO, DIRADICAO, DIRDNP, DIRACO, ESQESFERICO, ESQCILINDRICO,
                                                  ESQEIXO, ESQADICAO, ESQDNP, ESQACO, LENTES, ARMACAO,
                                                  DISTANCIAPUPILAR, DIREITO, ESQUERDO, DPA, MD, MV);


            }
            set
            {
                if (value != null)
                {
                    _IDDIAGMEDIOPEDIDO = value.IDDIAGMEDIOPEDIDO;
                    txtMedDirEsferico.Text = value.DIRESFERICO;
                    txtMedDirCilindrico.Text = value.DIRCILINDRICO;
                    txtMedDirEixo.Text = value.DIREIXO;
                    txtMedDireitaAdicao.Text = value.DIRADICAO;
                    txtMedioDirDNP.Text = value.DIRDNP;
                    txtMedioDirACO.Text = value.DIRACO;
                    txtMedEsqEsferico.Text = value.ESQESFERICO;
                    txtMedioEsqCilindrico.Text = value.ESQCILINDRICO;
                    txtMedEsqEixo.Text = value.ESQEIXO;
                    txtMedEsqAdicao.Text = value.ESQADICAO;
                    txtMedioEsqDNP.Text = value.ESQDNP;
                    txtMedioEsqACO.Text = value.ESQACO;
                    txtMedioLentes.Text = value.LENTES;
                    txtMedArmacao.Text = value.ARMACAO;
                    txtMedioDistPupilar.Text = value.DISTANCIAPUPILAR;
                    txtMedDireito.Text = value.DIREITO;
                    txtMedEsquerdo.Text = value.ESQUERDO;
                    txtMedioDPA.Text = value.DPA;
                    txtMedioMD.Text = value.MD;
                    txtMedioMV.Text = value.MV;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDIAGMEDIOPEDIDO = -1;
                    txtMedDirEsferico.Text = string.Empty;
                    txtMedDirCilindrico.Text = string.Empty;
                    txtMedDirEixo.Text = string.Empty;
                    txtMedDireitaAdicao.Text = string.Empty;
                    txtMedioDirDNP.Text = string.Empty;
                    txtMedioDirACO.Text = string.Empty; 
                    txtMedEsqEsferico.Text = string.Empty;
                    txtMedioEsqCilindrico.Text = string.Empty;
                    txtMedEsqEixo.Text = string.Empty;
                    txtMedEsqAdicao.Text = string.Empty;
                    txtMedioEsqDNP.Text = string.Empty;
                    txtMedioEsqACO.Text = string.Empty; 
                    txtMedioLentes.Text = string.Empty;
                    txtMedArmacao.Text = string.Empty;
                    txtMedioDistPupilar.Text = string.Empty;
                    txtMedDireito.Text = string.Empty;
                    txtMedEsquerdo.Text = string.Empty;
                    txtMedioDPA.Text = string.Empty;
                    txtMedioMD.Text = string.Empty;
                    txtMedioMV.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }

        int _IDDIAGLONGEPEDIDO = -1;
        public DIAGLONGEPEDIDOEntity Entity7
        {
            get
            {
                string DIRESFERICO = txtLongeDirEsferico.Text;
                string DIRCILINDRICO = txtLongeDireCilind.Text;
                string DIREIXO = txtLongeDireEixo.Text;
                string DIRADICAO = txtLongeDirAdicao.Text;
                string DIRDNP = txtLongeDirDNP.Text;
                string DIRACO = txtLongeDirAco.Text;
                string ESQESFERICO = txtLongeEsqEsferico.Text;
                string ESQCILINDRICO = txtLongeEsqCilindrico.Text;
                string ESQEIXO = txtLongeEsqEixo.Text;
                string ESQADICAO = txtLongeEsqAdicao.Text;
                string ESQDNP = txtLongeEsqDNP.Text;
                string ESQACO = txtLongeEsqACO.Text;
                string LENTES = txtLongeLentes.Text;
                string ARMACAO = txtLongeArmacao.Text;
                string DISTANCIAPUPILAR = txtLongeDistPupilar.Text;
                string DIREITO = txtLongeDireito.Text;
                string ESQUERDO = txtLongeEsquerdo.Text;
                string DPA = txtLongeDPA.Text;
                string MD = txtLongeMD.Text;
                string MV = txtLongeMV.Text;
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                return new DIAGLONGEPEDIDOEntity(_IDDIAGLONGEPEDIDO, _IDPEDIDOOTICA, DIRESFERICO, DIRCILINDRICO,
                                                  DIREIXO, DIRADICAO, DIRDNP, DIRACO, ESQESFERICO, ESQCILINDRICO,
                                                  ESQEIXO, ESQADICAO, ESQDNP, ESQACO, LENTES, ARMACAO,
                                                  DISTANCIAPUPILAR, DIREITO, ESQUERDO, DPA, MD, MV);


            }
            set
            {
                if (value != null)
                {
                    _IDDIAGLONGEPEDIDO = value.IDDIAGLONGEPEDIDO;
                    txtLongeDirEsferico.Text = value.DIRESFERICO;
                    txtLongeDireCilind.Text = value.DIRCILINDRICO;
                    txtLongeDireEixo.Text = value.DIREIXO;
                    txtLongeDirAdicao.Text = value.DIRADICAO;
                    txtLongeDirDNP.Text = value.DIRDNP;
                    txtLongeDirAco.Text = value.DIRACO;
                    txtLongeEsqEsferico.Text = value.ESQESFERICO;
                    txtLongeEsqCilindrico.Text = value.ESQCILINDRICO;
                    txtLongeEsqEixo.Text = value.ESQEIXO;
                    txtLongeEsqAdicao.Text = value.ESQADICAO;
                    txtLongeEsqDNP.Text = value.ESQDNP;
                    txtLongeEsqACO.Text = value.ESQACO;
                    txtLongeLentes.Text = value.LENTES;
                    txtLongeArmacao.Text = value.ARMACAO;
                    txtLongeDistPupilar.Text = value.DISTANCIAPUPILAR;
                    txtLongeDireito.Text = value.DIREITO;
                    txtLongeEsquerdo.Text = value.ESQUERDO;
                    txtLongeDPA.Text = value.DPA;
                    txtLongeMD.Text = value.MD;
                    txtLongeMV.Text = value.MV;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDIAGLONGEPEDIDO = -1;
                    txtLongeDirEsferico.Text = string.Empty;
                    txtLongeDireCilind.Text = string.Empty;
                    txtLongeDireEixo.Text = string.Empty;
                    txtLongeDirAdicao.Text = string.Empty;
                    txtLongeDirDNP.Text = string.Empty;
                    txtLongeDirAco.Text = string.Empty;
                    txtLongeEsqEsferico.Text = string.Empty;
                    txtLongeEsqCilindrico.Text = string.Empty;
                    txtLongeEsqEixo.Text = string.Empty;
                    txtLongeEsqAdicao.Text = string.Empty;
                    txtLongeEsqDNP.Text = string.Empty;
                    txtLongeEsqACO.Text = string.Empty;
                    txtLongeLentes.Text = string.Empty;
                    txtLongeArmacao.Text = string.Empty;
                    txtLongeDistPupilar.Text = string.Empty;
                    txtLongeDireito.Text = string.Empty;
                    txtLongeEsquerdo.Text = string.Empty;
                    txtLongeDPA.Text = string.Empty;
                    txtLongeMD.Text = string.Empty;
                    txtLongeMV.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }
     

      
        private void FrmPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
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
            GetMedico();
            GetDropServico();
            GetDropLaboratorio();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnMedico.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadServico.Image = Util.GetAddressImage(6);
            btnCadFornec.Image = Util.GetAddressImage(6);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();

            GetConfiSistema();

            this.Cursor = Cursors.Default;

            if (_IDPEDIDOOTICA != -1)
            {
                Entity = PEDIDOOTICAP.Read(_IDPEDIDOOTICA);
                ListaProdutoPedido(_IDPEDIDOOTICA);

                //Lista Duplicatas do Pedido
                GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                //Lista Comissao dos produtos
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                    SumTotalComissao();
            }

            if (ExibiDados)
            {
                tabControlPedidoVenda.SelectTab(1);
                rbVendasPesquisa.Checked = true;

                // Primeiro Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia igual a 1 
                DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                msktDataInicial.Text = primeiroDia.ToString("dd/MM/yyyy");

                // Ultimo Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia é a quantidade de dias que o mês corrente possui.
                //A função DateTime.DaysInMonth recebe como parametro o ano(int) e o mês(int) e retorna a quantidade de dias(int). 
                DateTime ultimoDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                msktDataFinal.Text = ultimoDia.ToString("dd/MM/yyyy");

                btnPesquisa_Click(null, null);
            }

            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
            }
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
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetMedico()
        {
            try
            {
                MEDICOProvider MEDICOP = new MEDICOProvider();
                MEDICOColl = MEDICOP.ReadCollectionByParameter(null, "NOME");

                cbMedico.DisplayMember = "NOME";
                cbMedico.ValueMember = "IDMEDICO";

                MEDICOEntity MEDICOTy = new MEDICOEntity();
                MEDICOTy.NOME = ConfigMessage.Default.MsgDrop;
                MEDICOTy.IDMEDICO = -1;
                MEDICOColl.Add(MEDICOTy);

                Phydeaux.Utilities.DynamicComparer<MEDICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MEDICOEntity>(cbMedico.DisplayMember);

                MEDICOColl.Sort(comparer.Comparer);
                cbMedico.DataSource = MEDICOColl;

                cbMedico.SelectedIndex = 0;
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

                    if (result > 0)
                    {
                        GetDropCliente();
                        cbCliente.SelectedValue = result;
                    }
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
               // frm._IDSTATUS = CodSelec;
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
           
        }

        private void VerificaDebitoCliente(int IdCliente)
        {
            try
            {
                int IDCLIENTE = Convert.ToInt32(IdCliente);

                string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.

                RowRelatorio.Clear();
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
                        cbProduto.SelectedValue = result;
                }
            }
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
             TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                    SomaDescontoProduto();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaDescontoProduto();
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
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
                    SomaDescontoProduto();
                }
            }
            else
                TextBoxSelec.Text = "0,00";
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

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                frm._IDCENTROCUSTOS = CodSelec;
                frm.ShowDialog();
                
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
                    if (_IDPEDIDOOTICA == -1)
                        _IDPEDIDOOTICA = PEDIDOOTICAP.Save(Entity);
                    else
                    {
                        _IDPEDIDOOTICA = PEDIDOOTICAP.Save(Entity);
                        btnPesquisa_Click(null, null);
                    }

                    txtNPedido.Text = _IDPEDIDOOTICA.ToString().PadLeft(6, '0');

                    _IDDIAGPERTOPEDIDO = DIAGPERTOPEDIDOP.Save(Entity5);
                    _IDDIAGMEDIOPEDIDO = DIAGMEDIOPEDIDOP.Save(Entity6);
                    _IDDIAGLONGEPEDIDO = DIAGLONGEPEDIDOP.Save(Entity7);                   
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
                this.Cursor = Cursors.Default;

            }
        }
       
        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbCliente.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
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
               ((Convert.ToDecimal(txtTotalProdAdicional.Text) * Convert.ToDecimal(txtPorcDesconto.Text)) / 100).ToString("n2");
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
              ((Convert.ToDecimal(txtTotalProdAdicional.Text) * Convert.ToDecimal(txtPorcAcrescimo.Text)) / 100).ToString("n2");
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

                if (rbOrcamentoPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                else if (rbVendasPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_PEDIDOOTICAColl = LIS_PEDIDOOTICAP.ReadCollectionByParameter(Filtro, "IDPEDIDOOTICA DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOOTICAEntity LIS_PEDIDOOTICATy = new LIS_PEDIDOOTICAEntity();
                LIS_PEDIDOOTICATy.TOTALIMPOSTOS = SumTotalPesquisa("TOTALIMPOSTOS");
                LIS_PEDIDOOTICATy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOOTICATy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOOTICATy.VALORACRESCIMO = SumTotalPesquisa("VALORACRESCIMO");
                LIS_PEDIDOOTICATy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_PEDIDOOTICATy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_PEDIDOOTICATy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_PEDIDOOTICATy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOOTICAColl.Add(LIS_PEDIDOOTICATy);


                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOOTICAColl;

                lblTotalPesquisa.Text = (LIS_PEDIDOOTICAColl.Count - 1).ToString();
            }
            else
                PesquisaFiltro();

            PaintGrid();

            this.Cursor = Cursors.Default;
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICAColl)
                {

                    if (item.FLAGORCAMENTO != null && item.FLAGORCAMENTO.Trim() == "S")
                    {
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else if (item.FLAGORCAMENTO != null && item.FLAGORCAMENTO.Trim() == "N")
                    {
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }

                    i++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICAColl)
            {
                if (NomeCampo == "TOTALIMPOSTOS")
                    valortotal += Convert.ToDecimal(item.TOTALIMPOSTOS);
                else if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "TOTALPRODUTOS")
                    valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
                else if (NomeCampo == "VALORACRESCIMO")
                    valortotal += Convert.ToDecimal(item.VALORACRESCIMO);
                else if (NomeCampo == "VALORCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VALORCOMISSAO);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
            }

            return valortotal;
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
            if (LIS_PEDIDOOTICAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOOTICAColl;
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

                if (rbOrcamentoPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                else if (rbVendasPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_PEDIDOOTICAColl = LIS_PEDIDOOTICAP.ReadCollectionByParameter(Filtro, "IDPEDIDOOTICA DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOOTICAEntity LIS_PEDIDOOTICATy = new LIS_PEDIDOOTICAEntity();
                LIS_PEDIDOOTICATy.TOTALIMPOSTOS = SumTotalPesquisa("TOTALIMPOSTOS");
                LIS_PEDIDOOTICATy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOOTICATy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOOTICATy.VALORACRESCIMO = SumTotalPesquisa("VALORACRESCIMO");
                LIS_PEDIDOOTICATy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");
                LIS_PEDIDOOTICATy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_PEDIDOOTICATy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_PEDIDOOTICATy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOOTICAColl.Add(LIS_PEDIDOOTICATy);               

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOOTICAColl;

                lblTotalPesquisa.Text = (LIS_PEDIDOOTICAColl.Count - 1).ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_PEDIDOOTICAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PEDIDOOTICAColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PEDIDOOTICAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOOTICAColl[rowindex].IDPEDIDOOTICA);
                    tabControlPedidoVenda.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = PEDIDOOTICAP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDOOTICA);
                    txtNPedido.Focus();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);


                    //Lista Comissao dos produtos
                    //"S" para comissão sobre o total o pedido
                    //"N" para comissão pelo total dos produto
                    if (FLAGCOMISSAO == "N")
                        SumTotalComissao();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_PEDIDOOTICAColl[rowindex].IDPEDIDOOTICA.ToString().PadLeft(6, '0') + " - " + LIS_PEDIDOOTICAColl[rowindex].NOMECLIENTE,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(LIS_PEDIDOOTICAColl[rowindex].IDPEDIDOOTICA);

                                //Lista  Produto do Pedido                            
                                ListaProdutoPedido(CodigoSelect);

                                //Exluir Produto do Pedido
                                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                                {
                                    PRODUTOSPEDOTICAP.Delete(Convert.ToInt32(item.IDPRODPEDOTICA));
                                }


                                ListaItensServico(CodigoSelect);
                                //Excluir Serviços
                                foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
                                {
                                    SERVICOPEDOTICAP.Delete(Convert.ToInt32(item.IDPRODPEDOTICA));
                                }

                                //Lista  Duplicatas do Pedido
                                GridDuplicatasPD(Convert.ToInt32(LIS_PEDIDOOTICAColl[rowindex].IDCLIENTE), LIS_PEDIDOOTICAColl[rowindex].IDPEDIDOOTICA.ToString().PadLeft(6, '0'));
                                //Exluir Duplicatas do Pedido
                                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                                {
                                    DUPLICATARECEBERP.Delete(Convert.ToInt32(item.IDDUPLICATARECEBER));
                                }

                                ListaDiagPertoPedido(CodigoSelect);
                                ListaDiagMedioPedido(CodigoSelect);
                                ListaDiagLongePedido(CodigoSelect);

                                DIAGPERTOPEDIDOP.Delete(_IDDIAGPERTOPEDIDO);
                                DIAGMEDIOPEDIDOP.Delete(_IDDIAGMEDIOPEDIDO);
                                DIAGLONGEPEDIDOP.Delete(_IDDIAGLONGEPEDIDO);

                                //Delete Pedido
                                PEDIDOOTICAP.Delete(CodigoSelect);

                                btnPesquisa_Click(null, null);

                                Entity = null;
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro técnico: " + ex.Message);
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    }
                }
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    try
                    {
                        CodigoSelect = Convert.ToInt32(LIS_PEDIDOOTICAColl[rowindex].IDPEDIDOOTICA);
                        Entity = PEDIDOOTICAP.Read(CodigoSelect);
                        ListaProdutoPedido(_IDPEDIDOOTICA);

                        //Lista Duplicatas do Pedido
                        GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                        TSBPrint_Click(null, null);
                        this.Cursor = Cursors.Default;
                        Entity = null;
                        Entity2 = null;
                    }
                    catch (Exception)
                    {

                        this.Cursor = Cursors.Default;
                    }

                }
            }
        }
     

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            Entity2_1 = null;
            
            Entity5 = null;
            Entity6 = null;
            Entity7 = null;         

            SumTotalComissao();
            ListaProdutoPedido(-1);
            tabControlPedidoVenda.SelectTab(0);
            tabControl1.SelectTab(0);
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PEDIDOOTICAColl[indice].IDPEDIDOOTICA);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlPedidoVenda.SelectTab(0);
                    Entity = PEDIDOOTICAP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDOOTICA);
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
                            PEDIDOOTICAP.Delete(CodigoSelect);
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
            Entity = null;
            Entity2 = null;
            Entity2_1 = null;    

            Entity5 = null;
            Entity6 = null;
            Entity7 = null;           

            SumTotalComissao();
            ListaProdutoPedido(-1);
            tabControlPedidoVenda.SelectTab(0);
            tabControl1.SelectTab(0);
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
              //  result = false;
            }
            else
            {
                SomaDescontoProduto();
                GravaProduto();

                //Lista Comissao dos produtos
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
               // if (FLAGCOMISSAO == "N")
                 //   SumTotalComissao();
            }
        }

        private void GravaProduto()
        {
            try
            {
                if (Validacoes() && ValidacoesProdutos())
                {
                     _IDPEDIDOOTICA = PEDIDOOTICAP.Save(Entity);
                    txtNPedido.Text = _IDPEDIDOOTICA.ToString().PadLeft(6, '0');

                    _IDDIAGPERTOPEDIDO = DIAGPERTOPEDIDOP.Save(Entity5);
                    _IDDIAGMEDIOPEDIDO = DIAGMEDIOPEDIDOP.Save(Entity6);
                    _IDDIAGLONGEPEDIDO = DIAGLONGEPEDIDOP.Save(Entity7);                   

                    PRODUTOSPEDOTICAP.Save(Entity2);
                    ListaProdutoPedido(_IDPEDIDOOTICA);                   

                    //Salva Pedido
                    PEDIDOOTICAP.Save(Entity);                  
                   
                    Entity2 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProduto.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }       

        private Boolean ValidacoesProdutos()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
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
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text) || Convert.ToDecimal(txtQuanProduto.Text) <= 0)
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (rdVenda.Checked && CONFISISTEMAP.Read(1).FLAGPEDBAIXAESTOQUE.TrimEnd() == "S" && (Util.EstoqueAtual(Convert.ToInt32(cbProduto.SelectedValue), false) < Convert.ToDecimal(txtQuanProduto.Text)))
            {
                string Msgerro = "Estoque não pode ficar negativo!";
                errorProvider1.SetError(txtQuanProduto, Msgerro);
                MessageBox.Show(Msgerro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void ListaProdutoPedido(int IDPEDIDOOTICA)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
                LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDOTICAColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDOTICAColl.Count.ToString();

                SumTotalProdutosPedido();

                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                    SumTotalComissao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalComissao()
        {
            decimal TotalComissao = 0;
            foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
            {
                TotalComissao += Convert.ToDecimal(item.COMISSAOPEDIDO);
            }

            txtValComissao.Text = TotalComissao.ToString("n2");
        }

        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
            txtTotalProdAdicional.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void SumTotalPedido()
        {
            decimal TotalPedido = 0;
            TotalPedido = (Convert.ToDecimal(txtTotalProdAdicional.Text) +
                             Convert.ToDecimal(txtTotalIPI.Text) +
                             Convert.ToDecimal(txtTotalIPI.Text) + Convert.ToDecimal(txtTotalServico.Text) +
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
           
             if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
            }           
            else
                errorProvider1.Clear();


            return result;
        }

        private void Delete()
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
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

                        //Exluir Produto do Pedido
                        foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                        {
                            PRODUTOSPEDOTICAP.Delete(Convert.ToInt32(item.IDPRODPEDOTICA));
                        }


                        ListaItensServico(_IDPEDIDOOTICA);
                        //Excluir Serviços
                        foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
                        {
                            SERVICOPEDOTICAP.Delete(Convert.ToInt32(item.IDPRODPEDOTICA));
                        }

                        //Lista  Duplicatas do Pedido
                        GridDuplicatasPD(Convert.ToInt32(LIS_PEDIDOOTICAColl[0].IDCLIENTE), LIS_PEDIDOOTICAColl[0].IDPEDIDOOTICA.ToString().PadLeft(6, '0'));
                        //Exluir Duplicatas do Pedido
                        foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                        {
                            DUPLICATARECEBERP.Delete(Convert.ToInt32(item.IDDUPLICATARECEBER));
                        }

                        DIAGPERTOPEDIDOP.Delete(_IDDIAGPERTOPEDIDO);
                        DIAGMEDIOPEDIDOP.Delete(_IDDIAGMEDIOPEDIDO);
                        DIAGLONGEPEDIDOP.Delete(_IDDIAGLONGEPEDIDO);

                        PEDIDOOTICAP.Delete(_IDPEDIDOOTICA);  

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
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
            int rowindex = e.RowIndex;
            if ( LIS_PRODUTOSPEDOTICAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32( LIS_PRODUTOSPEDOTICAColl[rowindex].IDPRODPEDOTICA);
                    Entity2 = PRODUTOSPEDOTICAP.Read(CodSelect);
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
                                CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDOTICAColl[rowindex].IDPRODPEDOTICA);
                                int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDOTICAColl[rowindex].IDPRODUTO);
                                PRODUTOSPEDOTICAP.Delete(CodSelect);

                                ListaProdutoPedido(_IDPEDIDOOTICA);
                                Entity2 = null;

                                PEDIDOOTICAP.Save(Entity);

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
       

        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void lkComp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show("Antes de adicionar composições é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                using (FrmSearchComposicao frm = new FrmSearchComposicao())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        AddProdutoComposicao(result);
                }
            }
        }

        private void AddProdutoComposicao(int IDCOMPOSICAO)
        {
             //Filtras os produtos da determinada composicao
            LIS_COMPOSPRODUTOProvider LIS_COMPOSPRODUTOP = new LIS_COMPOSPRODUTOProvider();
            LIS_COMPOSPRODUTOCollection LIS_COMPOSPRODUTOColl = new LIS_COMPOSPRODUTOCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCOMPOSICAO", "System.String", "like", IDCOMPOSICAO.ToString()));
            LIS_COMPOSPRODUTOColl = LIS_COMPOSPRODUTOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

            //Percorre os produtos da coleção
            foreach (LIS_COMPOSPRODUTOEntity item in LIS_COMPOSPRODUTOColl)
            {
                cbProduto.SelectedValue = item.IDPRODUTO;
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n2");
               
                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n2");              

                //Salva o produtos no Pedido
               PRODUTOSPEDOTICAP.Save(Entity2);                    
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDOOTICA);            

            //Grava os dados do Pedido
            PEDIDOOTICAP.Save(Entity);        
         
        }        

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                {
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                    decimal ValorVenda = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1);
                    txtValorUnitProd.Text = ValorVenda.ToString("n2");
                    decimal EstoqueAtual = Util.EstoqueAtual(PRODUTOSTy.IDPRODUTO, false);
                    lblEstoque.Text = "Estoque: " + EstoqueAtual.ToString();
                }
                else
                    lblEstoque.Text = "Estoque: 0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFuncionario.SelectedIndex > 0)
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);
                txtPorComisVend.Text = PorcComissVend.ToString("n2");
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
               // if (FLAGCOMISSAO == "S")
                {
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
                    if (FLAGCOMISSAO == "S")
                    {
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

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = txtNPedido.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                //DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());
                DateTime DataAtual = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DataAtual.AddDays(Convert.ToInt32(item.DIAS)).ToString());

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

                DUPLICATARECEBERty.DATAPAGTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORPAGO = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = 0;
                DUPLICATARECEBERty.IDSTATUS = 3; //Pago
                DUPLICATARECEBERty.NOTAFISCAL = "PO" + txtNPedido.Text;

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
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

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = txtNPedido.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);

                DateTime DataAtual = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DataAtual.AddDays(Convert.ToInt32(item.DIAS)).ToString());

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
                DUPLICATARECEBERty.NOTAFISCAL = "PO" + txtNPedido.Text;

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }               
            }
        }

        private void GridDuplicatasPD(int idcliente, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PO" + numero));

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

                if (ColumnSelecionada == 1)//Excluir
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
                else if (ColumnSelecionada == 0)//Editar
                {
                    FrmContasReceber FrmCR = new FrmContasReceber();
                    FrmCR.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                    FrmCR.ShowDialog();

                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

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
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                frm._IDLOCALCOBRANCA = CodSelec;
                frm.ShowDialog();
                
                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
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
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show("Antes de adicionar o Caixa é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
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

            CaixaTy.NDOCUMENTO = "PO"+txtNPedido.Text;
            CaixaTy.OBSERVACAO = "Pedido de Venda Ótica nº " + "PO" + txtNPedido.Text + " Cliente: " + cbCliente.SelectedValue + " - " + GetNameCliente(Convert.ToInt32(cbCliente.SelectedValue));

            try
            {
                CaixaP.Save(CaixaTy);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
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

       
        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
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

      
      
        private decimal SomaTotal()
        {
            decimal result = 0;

            foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICAColl)
            {
                result += Convert.ToDecimal(item.TOTALPEDIDO);
            }
            return result;
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
               
        }
       
        private void laserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirPedidoLJ();
        }

        private void ImprimirPedidoLJ()
        {
            ////  'define o objeto para visualizar a impressao
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

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Armazena na coleção do Orçamento Selecionada
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", _IDPEDIDOOTICA.ToString()));
                LIS_PEDIDOOTICACollection LIS_PEDIDOOTICACollPrint = new LIS_PEDIDOOTICACollection();
                LIS_PEDIDOOTICACollPrint = LIS_PEDIDOOTICAP.ReadCollectionByParameter(RowRelatorio);

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
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


                e.Graphics.DrawString("Nº PEDIDO ÓTICA", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDPEDIDOOTICA.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

                if (txtNumReferencia.Text.TrimEnd() != string.Empty)
                {
                    e.Graphics.DrawString("Nº Referência", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 68);
                    e.Graphics.DrawString(txtNumReferencia.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 83);
                }

                //Dados Cliente
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawString("Nome:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 140);
                e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

                e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.ENDERECO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
                e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

                e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
            //    e.Graphics.DrawString(Util.LimiterText(ClienteTy.CIDADE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
                e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
              //  e.Graphics.DrawString(Util.LimiterText(ClienteTy.UF1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

                e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
                e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);
                e.Graphics.DrawString("TSO: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 215);
              //  e.Graphics.DrawString(ClienteTy.TSO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 430, 215);
                e.Graphics.DrawString("Contrato: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 215);
               // e.Graphics.DrawString(ClienteTy.CONTRATO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 620, 215);


                //Produtos
                int linha = 240;
                int linhaBorda = 235;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);
                linha = linha + 5;
                e.Graphics.DrawString("Produtos/Serviços", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

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

                //Produtos;
                linha = linha + 25;
                linhaBorda = linhaBorda + 45;
                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VLUNITLIQUIDO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }

                foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDSERVICO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMESERVICO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }    
              

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 110);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 115);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Prod/Serv:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString((Convert.ToDecimal(txtTotalProdAdicional.Text) + Convert.ToDecimal(txtTotalServico.Text)).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString("Data Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                if (LIS_PEDIDOOTICACollPrint[0].DTENTREGA != null)
                    e.Graphics.DrawString(Convert.ToDateTime(LIS_PEDIDOOTICACollPrint[0].DTENTREGA).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;
                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;

                e.Graphics.DrawString("Medico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOOTICACollPrint[0].NOMEMEDICO,40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
                e.Graphics.DrawString("CRM " + LIS_PEDIDOOTICACollPrint[0].CRMMEDICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 380, linha);
                e.Graphics.DrawString("Laboratório/Fornecedor : " + LIS_PEDIDOOTICACollPrint[0].NOMEFORN, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha + 15);

                e.Graphics.DrawString(txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                e.Graphics.DrawString("Total Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalPedido.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Total Pago:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtValorPago.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Valor Devedor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtValorDev.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                //Diagnostico
                //linha = 240;
                linha = linha + 10;
                if (!chkRelPerto.Checked && !chkRelMedio.Checked && !chkRelGrande.Checked)
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 100, 500);
                
                e.Graphics.DrawString("Diagnóstico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);

                if (!chkRelPerto.Checked)
                {
                    //Perto
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString("PERTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                    e.Graphics.DrawString(Entity5.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                    e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                    e.Graphics.DrawString(Entity5.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                    e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                    e.Graphics.DrawString(Entity5.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                    e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity5.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                    e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                    e.Graphics.DrawString(Entity5.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                    e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                    e.Graphics.DrawString(Entity5.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);
                

               
                    linha = linha + 20;
                    e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString(Entity5.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawString(Entity5.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawString(Entity5.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawString(Entity5.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawString(Entity5.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawString(Entity5.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                    linha = linha + 25;
                    e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity5.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity5.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity5.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                    e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity5.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                    e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity5.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity5.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                    e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity5.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                    e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity5.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);

                }

                if (!chkRelMedio.Checked)
                {
               
                    //Medio
                    linha = linha + 5;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString("MÉDIO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                    e.Graphics.DrawString(Entity6.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                    e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                    e.Graphics.DrawString(Entity6.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                    e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                    e.Graphics.DrawString(Entity6.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                    e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity6.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                    e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                    e.Graphics.DrawString(Entity6.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                    e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                    e.Graphics.DrawString(Entity6.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                    linha = linha + 20;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawString(Entity6.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawString(Entity6.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawString(Entity6.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawString(Entity6.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawString(Entity6.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                    e.Graphics.DrawString(Entity6.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                    linha = linha + 25;
                    e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity6.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity6.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity6.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                    e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity6.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                    e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity6.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity6.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                    e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity6.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                    e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity6.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);
                }

                if (!chkRelGrande.Checked)
                {
                    //longe
                    linha = linha + 5;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString("LONGE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                    e.Graphics.DrawString(Entity7.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                    e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                    e.Graphics.DrawString(Entity7.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                    e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                    e.Graphics.DrawString(Entity7.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                    e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity7.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                    e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                    e.Graphics.DrawString(Entity7.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                    e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                    e.Graphics.DrawString(Entity7.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                    linha = linha + 20;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawString(Entity7.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawString(Entity7.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawString(Entity7.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawString(Entity7.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawString(Entity7.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                    e.Graphics.DrawString(Entity7.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                    e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                    linha = linha + 25;
                    e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity7.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity7.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity7.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);

                    e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity7.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);

                    e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity7.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                    linha = linha + 20;
                    e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                    e.Graphics.DrawString(Entity7.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                    e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                    e.Graphics.DrawString(Entity7.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                    e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                    e.Graphics.DrawString(Entity7.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                    linha = linha + 25;
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);

                }

                //Observação
                linha = linha + 35;
                if(!chkRelPerto.Checked && !chkRelMedio.Checked && !chkRelGrande.Checked)
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 100);

                linha = linha + 5;
                e.Graphics.DrawString("Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 450), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                linha = linha + 80;
                if(!chkRelPerto.Checked && !chkRelMedio.Checked && !chkRelGrande.Checked)
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }

        }

        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
      
        private void ImprimirListaGeralProduto()
        {
            
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private LIS_PRODUTOSPEDOTICACollection ProdutoRel(int IDPEDIDOOTICA)
        {
             LIS_PRODUTOSPEDOTICACollection  LIS_PRODUTOSPEDOTICAColl = new  LIS_PRODUTOSPEDOTICACollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));

             LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

            return  LIS_PRODUTOSPEDOTICAColl;
        }

         int IndexRegistro = 0;
         int paginaAtual = 0;
        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOOTICAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOOTICAEntity>(orderBy);

                LIS_PEDIDOOTICAColl.Sort(comparer.Comparer);              
                 
                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_PEDIDOOTICAColl;
               
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
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
              //  e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
               // e.Graphics.DrawString(ClienteTy.UF1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(ClienteTy.CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(ClienteTy.TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(ClienteTy.IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


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
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                //e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                //e.Graphics.DrawString(ClienteTy.UF1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(ClienteTy.CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(ClienteTy.TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(ClienteTy.IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


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
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 755);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 770);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 770);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 785);
                //e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 785);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 785);
                //e.Graphics.DrawString(ClienteTy.UF1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 785);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 785);
                e.Graphics.DrawString(ClienteTy.CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 785);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 800);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 800);
                e.Graphics.DrawString(ClienteTy.TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 800);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 815);
                //Condição para exibir o CPF ou CNPJ
                CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 815);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 815);
                e.Graphics.DrawString(ClienteTy.IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 815);

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
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PO" + txtNPedido.Text));
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
                e.Graphics.DrawString("PO" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString("PO" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


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
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
              //  e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
               // e.Graphics.DrawString(ClienteTy.UF1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(ClienteTy.CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(ClienteTy.TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(ClienteTy.IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


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

        private void btnMedico_Click(object sender, EventArgs e)
        {
            using (FrmMedico frm = new FrmMedico())
            {
                int CodSelec = Convert.ToInt32(cbMedico.SelectedValue);
                frm._IDMEDICO = CodSelec;
                frm.ShowDialog();
               
                GetMedico();
                cbMedico.SelectedValue = CodSelec;
            }
        }

        private void txtPertoDirDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtPertoEsqDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtMedioDirDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtMedioEsqDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtLongeDirDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtLongeEsqDNP_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Distância naso-pupilar";
        }

        private void txtPertoDirACO_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void txtPertoEsqACO_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void txtLongeEsqACO_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void txtLongeDirAco_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Altura centro óptico";
        }

        private void txtMedioLentes_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void txtMedioLentes_KeyDown(object sender, KeyEventArgs e)
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
                        txtMedioLentes.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtPertoLentes_KeyDown(object sender, KeyEventArgs e)
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
                        txtPertoLentes.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtPertoArmacao_KeyDown(object sender, KeyEventArgs e)
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
                        txtPertoArmacao.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtMedArmacao_KeyDown(object sender, KeyEventArgs e)
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
                        txtMedArmacao.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtLongeLentes_KeyDown(object sender, KeyEventArgs e)
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
                        txtLongeLentes.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtLongeArmacao_KeyDown(object sender, KeyEventArgs e)
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
                        txtLongeArmacao.Text = PRODUTOSP.Read(Convert.ToInt32(result)).NOMEPRODUTO;
                    }
                }
            }
        }

        private void txtPertoLentes_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void txtPertoArmacao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void txtMedArmacao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void txtLongeLentes_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void txtLongeArmacao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para selecionar algum Produto pressione Ctrl+E para pesquisar.";
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(3); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void carnêToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ImprimirCarneJato()
        {
            
        }

        private void printDocument7_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void orçamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirOrcamento();
        }

        private void ImprimirOrcamento()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument8;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                objPrintPreview.Document = printDocument8;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument8_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Armazena na coleção do Orçamento Selecionada
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", _IDPEDIDOOTICA.ToString()));
                LIS_PEDIDOOTICACollection LIS_PEDIDOOTICACollPrint = new LIS_PEDIDOOTICACollection();
                LIS_PEDIDOOTICACollPrint = LIS_PEDIDOOTICAP.ReadCollectionByParameter(RowRelatorio);

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
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


                e.Graphics.DrawString("ORÇAMENTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDPEDIDOOTICA.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

                //Dados Cliente
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawString("Nome:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 140);
                e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

                e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.ENDERECO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
                e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

                e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
              //  e.Graphics.DrawString(Util.LimiterText(ClienteTy.CIDADE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
                e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
               // e.Graphics.DrawString(Util.LimiterText(ClienteTy.UF1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

                e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
                e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);
                e.Graphics.DrawString("TSO: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 215);
             //   e.Graphics.DrawString(ClienteTy.TSO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 430, 215);
                e.Graphics.DrawString("Contrato: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 215);
               // e.Graphics.DrawString(ClienteTy.CONTRATO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 620, 215);


                //Peças
                int linha = 240;
                int linhaBorda = 235;
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

                //Produtos;
                linha = linha + 25;
                linhaBorda = linhaBorda + 45;
                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VLUNITLIQUIDO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 80);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 115);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;

                e.Graphics.DrawString("Medico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOOTICACollPrint[0].NOMEMEDICO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
                e.Graphics.DrawString("CRM " + LIS_PEDIDOOTICACollPrint[0].CRMMEDICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 380, linha);

                e.Graphics.DrawString(txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                e.Graphics.DrawString("Total Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalPedido.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Total Pago:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtValorPago.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Valor Devedor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtValorDev.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                //Diagnostico
                //linha = 240;
                linha = linha + 10;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 100, 500);
                e.Graphics.DrawString("Diagnóstico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);

                //Perto
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("PERTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity5.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity5.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity5.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity5.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity5.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);


                linha = linha + 20;
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString(Entity5.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity5.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity5.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity5.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity5.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity5.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity5.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity5.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);


                //Medio
                linha = linha + 5;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("MÉDIO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity6.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity6.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity6.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity6.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity6.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                linha = linha + 20;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity6.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity6.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity6.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity6.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity6.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                e.Graphics.DrawString(Entity6.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity6.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity6.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);

                //longe
                linha = linha + 5;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("LONGE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity7.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity7.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity7.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity7.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity7.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                linha = linha + 20;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity7.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity7.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity7.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity7.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity7.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                e.Graphics.DrawString(Entity7.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);

                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity7.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);

                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity7.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);


                //Observação
                linha = linha + 35;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 100);
                linha = linha + 5;
                e.Graphics.DrawString("Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 450), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                linha = linha + 80;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void printDocument8_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void laserJatoDeTintaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(2);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);
            }
            else
                ImprimirCarneJato();
        }

        private void matricialToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void dadosOcultosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirPedidoDadosOcultos();
        }

        private void ImprimirPedidoDadosOcultos()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument9;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                objPrintPreview.Document = printDocument9;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument9_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                //Armazena na coleção do Orçamento Selecionada
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", _IDPEDIDOOTICA.ToString()));
                LIS_PEDIDOOTICACollection LIS_PEDIDOOTICACollPrint = new LIS_PEDIDOOTICACollection();
                LIS_PEDIDOOTICACollPrint = LIS_PEDIDOOTICAP.ReadCollectionByParameter(RowRelatorio);

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
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


                e.Graphics.DrawString("Nº PEDIDO ÓTICA", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDPEDIDOOTICA.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

                if (txtNumReferencia.Text.TrimEnd() != string.Empty)
                {
                    e.Graphics.DrawString("Nº Referência", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 68);
                    e.Graphics.DrawString(txtNumReferencia.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 83);
                }

                //Dados Cliente
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawString("Nome:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 140);
                e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

                e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
              // e.Graphics.DrawString(Util.LimiterText(ClienteTy.ENDERECO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
                e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
                //e.Graphics.DrawString(Util.LimiterText(ClienteTy.BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

                e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
               // e.Graphics.DrawString(Util.LimiterText(ClienteTy.CIDADE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
                e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
               // e.Graphics.DrawString(Util.LimiterText(ClienteTy.UF1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

                e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
              //  e.Graphics.DrawString(Util.LimiterText(ClienteTy.TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                //e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
                e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
               // e.Graphics.DrawString(Util.LimiterText(ClienteTy.EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
               // e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);
                e.Graphics.DrawString("TSO: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 215);
                //e.Graphics.DrawString(ClienteTy.TSO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 430, 215);
                e.Graphics.DrawString("Contrato: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 215);
                //e.Graphics.DrawString(ClienteTy.CONTRATO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 620, 215);


                //Peças
                int linha = 240;
                int linhaBorda = 235;
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

                //Produtos;
                linha = linha + 25;
                linhaBorda = linhaBorda + 45;
                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                   // e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    //e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 80);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 115);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
              //  e.Graphics.DrawString(txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOOTICACollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;

                e.Graphics.DrawString("Medico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOOTICACollPrint[0].NOMEMEDICO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
                e.Graphics.DrawString("CRM " + LIS_PEDIDOOTICACollPrint[0].CRMMEDICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 380, linha);

                e.Graphics.DrawString(txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                e.Graphics.DrawString("Total Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
               // e.Graphics.DrawString(txtTotalPedido.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Total Pago:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
               // e.Graphics.DrawString(txtValorPago.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Valor Devedor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                //e.Graphics.DrawString(txtValorDev.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                //Diagnostico
                //linha = 240;
                linha = linha + 10;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 100, 500);
                e.Graphics.DrawString("Diagnóstico:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);

                //Perto
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("PERTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity5.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity5.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity5.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity5.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity5.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);


                linha = linha + 20;
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString(Entity5.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity5.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity5.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity5.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity5.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity5.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity5.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity5.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity5.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity5.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);


                //Medio
                linha = linha + 5;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("MÉDIO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity6.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity6.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity6.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity6.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity6.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                linha = linha + 20;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity6.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity6.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity6.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity6.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity6.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                e.Graphics.DrawString(Entity6.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);
                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity6.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity6.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity6.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity6.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);

                //longe
                linha = linha + 5;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 20);
                e.Graphics.DrawString("LONGE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawString("ESF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 110, linha + 25);
                e.Graphics.DrawString(Entity7.DIRESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 20);

                e.Graphics.DrawString("CIL", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha + 25);
                e.Graphics.DrawString(Entity7.DIRCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 20);

                e.Graphics.DrawString("EIXO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha + 25);
                e.Graphics.DrawString(Entity7.DIREIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 20);

                e.Graphics.DrawString("Adição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.DIRADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 20);

                e.Graphics.DrawString("DNP", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha + 25);
                e.Graphics.DrawString(Entity7.DIRDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 45);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 20);

                e.Graphics.DrawString("ACO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, linha + 25);
                e.Graphics.DrawString(Entity7.DIRACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 45);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 20);

                linha = linha + 20;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OD", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawString(Entity7.ESQESFERICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawString(Entity7.ESQCILINDRICO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 210, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawString(Entity7.ESQEIXO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 310, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawString(Entity7.ESQADICAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 410, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawString(Entity7.ESQDNP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 510, linha + 50);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);
                e.Graphics.DrawString(Entity7.ESQACO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 610, linha + 50);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 690, 25);
                e.Graphics.DrawString("OE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 600, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 500, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 400, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 300, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 200, 25);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 25);

                linha = linha + 25;
                e.Graphics.DrawString("Lentes:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.LENTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Armação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.ARMACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("Distância Pupilar:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.DISTANCIAPUPILAR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 140, linha + 25);

                e.Graphics.DrawString("Direito:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity7.DIREITO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha + 25);

                e.Graphics.DrawString("Esquerdo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.ESQUERDO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, linha + 25);

                linha = linha + 20;
                e.Graphics.DrawString("DPA:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, linha + 25);
                e.Graphics.DrawString(Entity7.DPA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha + 25);
                e.Graphics.DrawString("MD:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, linha + 25);
                e.Graphics.DrawString(Entity7.MD, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 280, linha + 25);
                e.Graphics.DrawString("MV:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, linha + 25);
                e.Graphics.DrawString(Entity7.MV, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 440, linha + 25);

                linha = linha + 25;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha + 20, config.MargemDireita - 100, 5);


                //Observação
                linha = linha + 35;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 100);
                linha = linha + 5;
                e.Graphics.DrawString("Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 450), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                linha = linha + 80;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }


        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void PrintTicket()
        {
            try
            {
                Ticket ticket = new Ticket();

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine("Pedido N." + Entity.IDPEDIDOOTICA.ToString().PadLeft(6, '0'));
                ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                ticket.AddSubHeaderLine("VENDEDOR: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalProdAdicional.Text);
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

        private void ListaDiagPertoPedido(int IDPEDIDOOTICA)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDOOTICA.ToString()));

            DIAGPERTOPEDIDOCollection DIAGPERTOPEDIDOColl = new DIAGPERTOPEDIDOCollection();
            DIAGPERTOPEDIDOColl = DIAGPERTOPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            if (DIAGPERTOPEDIDOColl.Count > 0)
            {
                Entity5 = DIAGPERTOPEDIDOP.Read(DIAGPERTOPEDIDOColl[0].IDDIAGPERTOPEDIDO);
            }
            else
                Entity5 = null;

        }


        private void ListaDiagMedioPedido(int IDPEDIDOOTICA)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDOOTICA.ToString()));

            DIAGMEDIOPEDIDOCollection DIAGMEDIOPEDIDOColl = new DIAGMEDIOPEDIDOCollection();
            DIAGMEDIOPEDIDOColl = DIAGMEDIOPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            if (DIAGMEDIOPEDIDOColl.Count > 0)
            {
                Entity6 = DIAGMEDIOPEDIDOP.Read(DIAGMEDIOPEDIDOColl[0].IDDIAGMEDIOPEDIDO);
            }
            else
                Entity6 = null;
        }

        private void ListaDiagLongePedido(int IDPEDIDOOTICA)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));

            DIAGLONGEPEDIDOCollection DIAGLONGEPEDIDOColl = new DIAGLONGEPEDIDOCollection();
            DIAGLONGEPEDIDOColl = DIAGLONGEPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            if (DIAGLONGEPEDIDOColl.Count > 0)
            {
                Entity7 = DIAGLONGEPEDIDOP.Read(DIAGLONGEPEDIDOColl[0].IDDIAGLONGEPEDIDO);
            }
            else
                Entity7 = null;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
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
                    frm.NotaFiscal = "PO" + txtNPedido.Text;
                    frm.ShowDialog();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
            } 

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (ValidaDuplicatas())
                {
                    if (chkCartaoCredito.Checked)
                    {
                        DialogResult dr2 = MessageBox.Show("A opção Cartão de Crédito/Cheque está selecionada, as duplicatas já serão lançadas como pagas!, deseja continuar?",
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr2 == DialogResult.Yes)
                        {
                            if (ValidaDuplicatasVencidas())
                            {
                                try
                                {
                                    SaveDuplicatasPaga();

                                    //Salva a forma de pagamento no Pedido
                                    PEDIDOOTICAP.Save(Entity);

                                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
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
                            PEDIDOOTICAP.Save(Entity);

                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        }
                    }
                }
            }
        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
            }
            else
            {
                LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
                int i = 1;
                foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICAColl)
                {
                    LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                    LIS_PEDIDOTy.IDPEDIDO = item.IDPEDIDOOTICA;
                    LIS_PEDIDOTy.IDCLIENTE = item.IDCLIENTE;
                    LIS_PEDIDOTy.NOMECLIENTE= item.NOMECLIENTE;
                    LIS_PEDIDOTy.DTEMISSAO= item.DTEMISSAO;
                    LIS_PEDIDOTy.IDSTATUS= item.IDSTATUS;
                    LIS_PEDIDOTy.NOMESTATUS= item.NOMESTATUS;
                    LIS_PEDIDOTy.PRAZOENTREGA= item.PRAZOENTREGA;
                    LIS_PEDIDOTy.IDTRANSPORTES= item.IDTRANSPORTES;
                    LIS_PEDIDOTy.NOMETRANSPORTES= item.NOMETRANSPORTES;
                    LIS_PEDIDOTy.IDVENDEDOR= Convert.ToInt32(item.IDVENDEDOR);
                    LIS_PEDIDOTy.NOMEVENDEDOR= item.NOMEVENDEDOR;
                    LIS_PEDIDOTy.VALORCOMISSAO= item.VALORCOMISSAO;
                    LIS_PEDIDOTy.OBSERVACAO= item.OBSERVACAO;
                    LIS_PEDIDOTy.TOTALPRODUTOS= item.TOTALPRODUTOS;
                    LIS_PEDIDOTy.TOTALIMPOSTOS= item.TOTALIMPOSTOS;
                    LIS_PEDIDOTy.PORCDESCONTO= item.PORCDESCONTO;
                    LIS_PEDIDOTy.VALORDESCONTO= item.VALORDESCONTO;
                    LIS_PEDIDOTy.PORCACRESCIMO= item.PORCACRESCIMO;
                    LIS_PEDIDOTy.VALORACRESCIMO= item.VALORACRESCIMO;
                    LIS_PEDIDOTy.TOTALPEDIDO= item.TOTALPEDIDO;
                    LIS_PEDIDOTy.IDFORMAPAGAMENTO= item.IDFORMAPAGAMENTO;
                    LIS_PEDIDOTy.NOMEFORMAPAGTO= item.NOMEFORMAPAGTO;
                    LIS_PEDIDOTy.VALORPAGO= item.VALORPAGO;
                    LIS_PEDIDOTy.VALORDEVEDOR= item.VALORDEVEDOR;
                    LIS_PEDIDOTy.IDLOCALCOBRANCA= item.IDLOCALCOBRANCA;
                    LIS_PEDIDOTy.NOMELOCALCOBRANCA= item.NOMELOCALCOBRANCA;
                    LIS_PEDIDOTy.IDCENTROCUSTOS= item.IDCENTROCUSTO;
                    LIS_PEDIDOTy.DESCCENTROCUSTO= item.DESCRICAOCENTROCUSTO;
                    LIS_PEDIDOTy.CENTROCUSTO = item.CENTROCUSTO;

                    if (i < LIS_PEDIDOOTICAColl.Count)//Nao adiciona o ultimo item de somatorio
                        LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                    i++;
                }

                FrmVendaGrupo FrmVendaG = new FrmVendaGrupo();
                FrmVendaG.LIS_PEDIDOFiltroPrintColl = LIS_PEDIDOColl;
                FrmVendaG.ShowDialog();
            }
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

                    LIS_PEDIDOOTICACollection LIS_PEDIDOOTICA2Coll = new LIS_PEDIDOOTICACollection();
                    LIS_PEDIDOOTICA2Coll = LIS_PEDIDOOTICAP.ReadCollectionByParameter(RowFiltroCaixa, "DTEMISSAO");

                    LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
                    foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICA2Coll)
                    {
                        LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                        LIS_PEDIDOTy.IDPEDIDO = item.IDPEDIDOOTICA;
                        LIS_PEDIDOTy.IDCLIENTE = item.IDCLIENTE;
                        LIS_PEDIDOTy.NOMECLIENTE = item.NOMECLIENTE;
                        LIS_PEDIDOTy.DTEMISSAO = item.DTEMISSAO;
                        LIS_PEDIDOTy.IDSTATUS = item.IDSTATUS;
                        LIS_PEDIDOTy.NOMESTATUS = item.NOMESTATUS;
                        LIS_PEDIDOTy.PRAZOENTREGA = item.PRAZOENTREGA;
                        LIS_PEDIDOTy.IDTRANSPORTES = item.IDTRANSPORTES;
                        LIS_PEDIDOTy.NOMETRANSPORTES = item.NOMETRANSPORTES;
                        LIS_PEDIDOTy.IDVENDEDOR = Convert.ToInt32(item.IDVENDEDOR);
                        LIS_PEDIDOTy.NOMEVENDEDOR = item.NOMEVENDEDOR;
                        LIS_PEDIDOTy.VALORCOMISSAO = item.VALORCOMISSAO;
                        LIS_PEDIDOTy.OBSERVACAO = item.OBSERVACAO;
                        LIS_PEDIDOTy.TOTALPRODUTOS = item.TOTALPRODUTOS;
                        LIS_PEDIDOTy.TOTALIMPOSTOS = item.TOTALIMPOSTOS;
                        LIS_PEDIDOTy.PORCDESCONTO = item.PORCDESCONTO;
                        LIS_PEDIDOTy.VALORDESCONTO = item.VALORDESCONTO;
                        LIS_PEDIDOTy.PORCACRESCIMO = item.PORCACRESCIMO;
                        LIS_PEDIDOTy.VALORACRESCIMO = item.VALORACRESCIMO;
                        LIS_PEDIDOTy.TOTALPEDIDO = item.TOTALPEDIDO;
                        LIS_PEDIDOTy.IDFORMAPAGAMENTO = item.IDFORMAPAGAMENTO;
                        LIS_PEDIDOTy.NOMEFORMAPAGTO = item.NOMEFORMAPAGTO;
                        LIS_PEDIDOTy.VALORPAGO = item.VALORPAGO;
                        LIS_PEDIDOTy.VALORDEVEDOR = item.VALORDEVEDOR;
                        LIS_PEDIDOTy.IDLOCALCOBRANCA = item.IDLOCALCOBRANCA;
                        LIS_PEDIDOTy.NOMELOCALCOBRANCA = item.NOMELOCALCOBRANCA;
                        LIS_PEDIDOTy.IDCENTROCUSTOS = item.IDCENTROCUSTO;
                        LIS_PEDIDOTy.DESCCENTROCUSTO = item.DESCRICAOCENTROCUSTO;
                        LIS_PEDIDOTy.CENTROCUSTO = item.CENTROCUSTO;
                    
                        LIS_PEDIDOColl.Add(LIS_PEDIDOTy);
                    }

                    FrmVendaGrupo FrmVendaG = new FrmVendaGrupo();
                    FrmVendaG.LIS_PEDIDOFiltroPrintColl = LIS_PEDIDOColl;
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

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count < 2)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
            }
            else
            {
                foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOOTICAColl)
                {
                    if (item.IDPEDIDOOTICA != -1 || item.IDPEDIDOOTICA == null)
                    {
                        DialogResult dr = MessageBox.Show("Deseja imprimir o pedido nº " + item.IDPEDIDOOTICA + " Cliente: " + item.NOMECLIENTE + " ?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNoCancel);

                        if (dr == DialogResult.Yes)
                        {

                            Entity = PEDIDOOTICAP.Read(Convert.ToInt32(item.IDPEDIDOOTICA));
                            ListaProdutoPedido(_IDPEDIDOOTICA);

                            //Lista Duplicatas do Pedido
                            GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                            ImprimirPedidoLJ();
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void btnCadServico_Click(object sender, EventArgs e)
        {
            using (FrmServico frm = new FrmServico())
            {
                int CodSelec = Convert.ToInt32(cbServico.SelectedValue);
                frm._idservico = CodSelec;
                frm.ShowDialog();                
                GetDropServico();
                cbServico.SelectedValue = CodSelec;
            }
        }

        private void GetDropServico()
        {
            SERVICOProvider SERVICOP = new SERVICOProvider();
            SERVICOColl = SERVICOP.ReadCollectionByParameter(null, "NOME");

            cbServico.DisplayMember = "NOME";
            cbServico.ValueMember = "IDSERVICO";

            SERVICOEntity SERVICOTy = new SERVICOEntity();
            SERVICOTy.NOME = ConfigMessage.Default.MsgDrop;
            SERVICOTy.IDSERVICO = -1;
            SERVICOColl.Add(SERVICOTy);

            Phydeaux.Utilities.DynamicComparer<SERVICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SERVICOEntity>(cbServico.DisplayMember);

            SERVICOColl.Sort(comparer.Comparer);
            cbServico.DataSource = SERVICOColl;

            cbServico.SelectedIndex = 0;
        }

        private Boolean ValidacoesServico()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbServico.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorServico.Text))
            {
                errorProvider1.SetError(txtValorServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanServico.Text) || Convert.ToDecimal(txtQuanServico.Text) <= 0)
            {
                errorProvider1.SetError(txtQuanServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void btnAddServico_Click(object sender, EventArgs e)
        {
                if (Validacoes() && ValidacoesServico())
                {

                    try
                    {
                        _IDPEDIDOOTICA = PEDIDOOTICAP.Save(Entity);
                        txtNPedido.Text = _IDPEDIDOOTICA.ToString().PadLeft(6, '0');

                        _IDDIAGPERTOPEDIDO = DIAGPERTOPEDIDOP.Save(Entity5);
                        _IDDIAGMEDIOPEDIDO = DIAGMEDIOPEDIDOP.Save(Entity6);
                        _IDDIAGLONGEPEDIDO = DIAGLONGEPEDIDOP.Save(Entity7);   

                        //Grava itens de serviços
                        SERVICOPEDOTICAP.Save(Entity2_1);

                        //Lista os itens de serviços cadastrados;
                        ListaItensServico(_IDPEDIDOOTICA);

                        SumTotalPedido();

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Entity2_1 = null;
                        cbServico.Focus();
                        PEDIDOOTICAP.Save(Entity);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    }

                
                }
        }

        private void ListaItensServico(int IDPEDIDOOTICA)
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOOTICA", "System.Int32", "=", IDPEDIDOOTICA.ToString()));
            LIS_SERVICOPEDOTICAColl = LIS_SERVICOPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

            DGDadosServicos.AutoGenerateColumns = false;
            DGDadosServicos.DataSource = LIS_SERVICOPEDOTICAColl;

            SumTotalItemsServico();
        }

        private void SumTotalItemsServico()
        {
            Decimal total = 0;
            foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalServico.Text = total.ToString("n2");
        }

        private void btnCancelEdiServico_Click(object sender, EventArgs e)
        {
            Entity2_1 = null;
        }

        private void DGDadosServicos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SERVICOPEDOTICAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                       CodSelect = Convert.ToInt32(LIS_SERVICOPEDOTICAColl[rowindex].IDPRODPEDOTICA);
                       Entity2_1 = SERVICOPEDOTICAP.Read(CodSelect);
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
                                CodSelect = Convert.ToInt32(LIS_SERVICOPEDOTICAColl[rowindex].IDPRODPEDOTICA);
                                SERVICOPEDOTICAP.Delete(CodSelect);
                                ListaItensServico(_IDPEDIDOOTICA);
                                SumTotalPedido();
                                PEDIDOOTICAP.Save(Entity);
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

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            //laserToolStripMenuItem_Click(null, null);
            modelo2ToolStripMenuItem_Click(null, null);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void btnCadFornec_Click(object sender, EventArgs e)
        {
            using (FrmFornecedor frm = new FrmFornecedor())
            {
                int CodSelec = Convert.ToInt32(cbLaboratorio.SelectedValue);

                frm.ShowDialog();

                GetDropLaboratorio();
                cbLaboratorio.SelectedValue = CodSelec;
            }
        }

        private void GetDropLaboratorio()
        {
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
            FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbLaboratorio.DisplayMember = "NOME";
            cbLaboratorio.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FORNECEDORColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbLaboratorio.DisplayMember);

            FORNECEDORColl.Sort(comparer.Comparer);
            cbLaboratorio.DataSource = FORNECEDORColl;

            cbLaboratorio.SelectedIndex = 0;
        }

        private void vendaPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorPedOtica FrmVenOtica = new FrmVendaVendedorPedOtica();
            FrmVenOtica.ShowDialog();
        }

        private void produtosMaisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidosOtica FrmP = new FrmProdutosMaisVendidosOtica();
            FrmP.ShowDialog();			
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        public MonthCalendar monthCalendar5 = new MonthCalendar();
        Form FormCalendario5 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar5.Name = "monthCalendar5";
            monthCalendar5.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar5_DateSelected);

            FormCalendario5.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario5.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario5.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario5.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario5.Location = new Point(230, 55);
            FormCalendario5.Name = "FrmCalendario5";
            FormCalendario5.Text = "Calendário";
            FormCalendario5.ResumeLayout(false);
            FormCalendario5.Controls.Add(monthCalendar5);
            FormCalendario5.ShowDialog();
        }

        private void monthCalendar5_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar5.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario5.Close();
        }

        private void geralComProdutos2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOOTICAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                //FrmListaServicoProd2 Frm = new FrmListaServicoProd2();
                //Frm.LIS_PEDIDOOTICAColl = LIS_PEDIDOOTICAColl;
                //Frm.ShowDialog();
            }
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PrintTicket();
            }
        }

        private void térmicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                PrintTicketTermica();
        }

        private void PrintTicketTermica()
        {
            try
            {
                ImprimeTexto imp = new ImprimeTexto();
                string PortaImpressora = BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALLOCAL;

                if (PortaImpressora != string.Empty)
                {
                    try
                    {
                        imp.Inicio(PortaImpressora);

                        //Dados da empresa
                        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                        EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                        string Traço = string.Empty.PadRight(35, '=');
                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.LimiterText(EMPRESATy.NOMECLIENTE, 35));
                        imp.ImpLF(Util.LimiterText("CNPJ: " + EMPRESATy.CNPJCPF + "IE:" + EMPRESATy.IE, 35));
                        imp.ImpLF(Util.LimiterText(EMPRESATy.ENDERECO, 35));

                        if (EMPRESATy.BAIRRO != null)
                            imp.ImpLF(Util.LimiterText("Bairro: " + EMPRESATy.BAIRRO, 35));

                        imp.ImpLF(Util.LimiterText(EMPRESATy.TELEFONE, 35));
                        imp.ImpLF((Util.LimiterText(EMPRESATy.CIDADE, 30) + "-" + EMPRESATy.UF));
                        imp.ImpLF((DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()));
                        imp.ImpLF(("Pedido N." + txtNPedido.Text));
                        imp.ImpLF(Traço);

                        imp.ImpLF("CLIENTE: " + Util.LimiterText(cbCliente.Text, 35));
                        imp.ImpLF("VENDEDOR.: " + Util.LimiterText(cbFuncionario.Text, 35));
                        imp.ImpLF(Traço);

                        imp.ImpLF("Produto          " + "    Qto" + "     Total");
                        imp.ImpLF(Traço);

                        foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                        {
                            string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                            imp.ImpLF(Util.LimiterText("Cod:" + item.IDPRODUTO.ToString() + "-" + item.NOMEPRODUTO, 35));
                            imp.ImpCol(15, item.QUANTIDADE.ToString().PadLeft(8, ' '));
                            imp.ImpCol(27, ValorTotal.PadLeft(8, ' '));
                            imp.Pula(1);
                        }

                        imp.Pula(1);
                        imp.ImpLF("No Itens: " + LIS_PRODUTOSPEDOTICAColl.Count.ToString());
                        imp.ImpLF(Traço);

                        imp.ImpCol(0, "SUBTOTAL:");
                        imp.ImpCol(27, txtTotalPedido.Text.PadLeft(8, ' '));
                        imp.Pula(1);

                        imp.ImpCol(00, "DESCONTO:");
                        imp.ImpCol(27, txtTotalDesconto.Text.PadLeft(8, ' '));
                        imp.Pula(1);

                        imp.ImpCol(0, "ACRESCIMO:");
                        imp.ImpCol(27, txtTotalAcrescimo.Text.PadLeft(8, ' '));
                        imp.Pula(1);

                        imp.ImpCol(0, "TOTAL:");
                        imp.ImpCol(27, txtValorDev.Text.PadLeft(8, ' '));
                        imp.Pula(4);

                        //Duplicata
                        if (LIS_DUPLICATARECEBERColl.Count > 0)
                        {
                            imp.ImpLF("DUPLICATA");
                            imp.ImpLF(Traço);

                            imp.ImpCol(0, "No Parc.");
                            imp.ImpCol(15, "Dt. Vencto");
                            imp.ImpCol(30, "Valor");
                            imp.Pula(1);

                            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                            {
                                imp.ImpCol(0, item.NUMERO);
                                imp.ImpCol(15, Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"));
                                imp.ImpCol(25, Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2").PadLeft(8, ' '));
                                imp.Pula(1);
                            }
                        }

                        imp.Pula(2);

                        imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg1ticket);
                        imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg2ticket);
                        imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg3ticket);
                        imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg4ticket);

                        imp.Pula(2);
                        imp.Fim();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Erro:  " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                    }



                }
                else
                {
                    MessageBox.Show("Porta não localizada",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                }

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

        private void txtCodBarra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisaProduto(txtCodBarra.Text);
            }
        }

        private void PesquisaProduto(string IDPRODUTO)
        {
            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy = null;

            if (ValidacoesLibrary.ValidaTipoInt32(IDPRODUTO))
            {
                PRODUTOSTy = PesquisaCodBarra(IDPRODUTO);

                if (PRODUTOSTy == null)
                    PRODUTOSTy = PesquisaCodProduto(IDPRODUTO);               
            }

            if (PRODUTOSTy == null)
                PRODUTOSTy = PesquisaCodRefeencia(IDPRODUTO);

            if (PRODUTOSTy == null)
            {
                txtValorUnitProd.Focus();
                DialogResult dr = MessageBox.Show("Código inválido, deseja efetuar a pesquisa?",
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
                            cbProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                            txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                            txtQuanProduto.Focus();
                        }
                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                cbProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                txtQuanProduto.Focus();
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

                return PRODUTOSPESBARRATY;
            }
            catch (Exception ex)
            {
                return PRODUTOSPESBARRATY;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }           

        }

        private PRODUTOSEntity PesquisaCodProduto(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();    
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Pesquisa));
                
                PRODUTOSCollection PRODUTOPESQUISAColl = new PRODUTOSCollection();
                PRODUTOPESQUISAColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOPESQUISAColl.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOPESQUISAColl[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;

                return PRODUTOSPESBARRATY;
            }
            catch (Exception ex)
            {
                return PRODUTOSPESBARRATY;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private PRODUTOSEntity PesquisaCodRefeencia(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();    
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", Pesquisa));

                PRODUTOSCollection PRODUTOPESQUISAColl = new PRODUTOSCollection();
                PRODUTOPESQUISAColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                
                if (PRODUTOPESQUISAColl.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOPESQUISAColl[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;

                return PRODUTOSPESBARRATY;
            }
            catch (Exception ex)
            {
                return PRODUTOSPESBARRATY;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void vendasDeProdutoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaProduto FrmV = new FrmVendaProduto();
            FrmV.ShowDialog();
        }

        private void únicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void loteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void cbServico_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbServico.SelectedValue)> 0)
            {
                SERVICOEntity SERVICOTy = new SERVICOEntity();
                SERVICOProvider SERVICOP = new SERVICOProvider();
                SERVICOTy = SERVICOP.Read(Convert.ToInt32(cbServico.SelectedValue));
                decimal ValorVenda = Convert.ToDecimal(SERVICOTy.VALOR);
                txtValorServico.Text = ValorVenda.ToString("n2");               
            }
            
        }

        private void dataGridDupl_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaPotica Frm = new FrmVendaDiariaPotica();
            Frm.ShowDialog();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
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
                    SomaDescontoProduto();
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void SomaDescontoProduto()
        {
            try
            {
                if (txtQuanProduto.Text == string.Empty)
                    txtQuanProduto.Text = "1,00";

                if (Convert.ToDecimal(txtDesconVlUnitaPorc.Text) > 0)
                {
                    txtLiquido.Text = (Convert.ToDecimal(txtValorUnitProd.Text) * Convert.ToDecimal(txtDesconVlUnitaPorc.Text) / 100).ToString("n2");
                    txtLiquido.Text = (Convert.ToDecimal(txtValorUnitProd.Text) - Convert.ToDecimal(txtLiquido.Text)).ToString("n2");
                    txtVlTotalProduto.Text = (Convert.ToDecimal(txtLiquido.Text) * (Convert.ToDecimal(txtQuanProduto.Text))).ToString("n2");
                }
                else
                { 
                    txtLiquido.Text = txtValorUnitProd.Text;
                    txtVlTotalProduto.Text = (Convert.ToDecimal(txtLiquido.Text) * (Convert.ToDecimal(txtQuanProduto.Text))).ToString("n2");
                }

                txtVlTotalProduto.Text = (Convert.ToDecimal(txtLiquido.Text) * Convert.ToDecimal(txtQuanProduto.Text)).ToString("n2");;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void economicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PEDIDOOTICAP.Save(Entity);

                using (FrmRelatPedidoOticaEconomico frm = new FrmRelatPedidoOticaEconomico())
                {
                    frm.LIS_SERVICOPEDOTICAColl = LIS_SERVICOPEDOTICAColl;
                    frm.LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAColl;
                    frm.IDPEDIDOOTICA = _IDPEDIDOOTICA;
                    frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.ShowDialog();
                }
            }
        }

        private void chkPesquisaCodReferencia_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void AlterarPesquisaProduto(CheckBox CHKPesquisaProduto)
        {
           
        }

        private void chkPesquisaCodProduto_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkPesquisaCodBarra_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkPesquisaCodBarra_Click(object sender, EventArgs e)
        {
        }

        private void chkPesquisaCodProduto_Click(object sender, EventArgs e)
        {
            

        }

        private void rbCodBarra_Click(object sender, EventArgs e)
        {
           
        }

        private void rbCodiProduto_Click(object sender, EventArgs e)
        {
          
        }

        private void rbCodReferencia_Click(object sender, EventArgs e)
        {
           
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PEDIDOOTICAP.Save(Entity);

                using (FrmRelatPedidoOticaMod2 frm = new FrmRelatPedidoOticaMod2())
                {
                    frm.LIS_SERVICOPEDOTICAColl = LIS_SERVICOPEDOTICAColl;
                    frm.LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAColl;
                    frm.IDPEDIDOOTICA = _IDPEDIDOOTICA;

                    if (Convert.ToInt32(cbLaboratorio.SelectedValue) > 0)
                        frm.NomeLaboratorio = cbLaboratorio.Text;
                    else
                        frm.NomeLaboratorio = " ";

                    frm.ExibirPerto =chkRelPerto.Checked ? "true" : "false";
                    frm.ExibirMedio = chkRelMedio.Checked ? "true" : "false";
                    frm.ExibirLonge = chkRelGrande.Checked ? "true" : "false";

                    frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.ShowDialog();
                }
            }
        }

        private void txtNPedido_TextChanged(object sender, EventArgs e)
        {

        }

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
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
                frm.TituloSelec = "Relação de Pedidos";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOOTICA == -1)
            {
                tabControlPedidoVenda.SelectTab(1);
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

               // var fromAddress = new MailAddress("imexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                //var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var toAddress = new MailAddress(CLIENTETy.EMAILCLIENTE, CLIENTETy.NOME);
              //  const string fromPassword = "rmr877701c#";
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
                foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }
               
                
                foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMESERVICO + "</td>");
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
                  //  Host = "mail.imexsistema.com.br",
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

        private void carnêDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void carnêToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void capaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            using (FrmCapaCarnecs frm = new FrmCapaCarnecs())
            {
                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }      
        }

        private void txtQuanServico_Enter(object sender, EventArgs e)
        {
            CalculoSubTotalServico();
        }

        private void CalculoSubTotalServico()
        {
            try
            {
                if(ValidacoesLibrary.ValidaTipoDecimal(txtQuanServico.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtValorServico.Text))
                    txtSubTotalServico.Text = (Convert.ToDecimal(txtQuanServico.Text) * Convert.ToDecimal(txtValorServico.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private void txtQuanServico_Leave(object sender, EventArgs e)
        {
            CalculoSubTotalServico();
        }

        private void txtValorServico_Leave(object sender, EventArgs e)
        {
            CalculoSubTotalServico();
        }
    }
}
