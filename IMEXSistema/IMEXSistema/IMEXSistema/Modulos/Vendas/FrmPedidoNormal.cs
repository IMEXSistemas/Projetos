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
using System.Diagnostics;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.Relatorio;
using System.Runtime.InteropServices;
using BmsSoftware.Classes.BMSworks.UI;
using System.Net.Mail;
using System.Net;
using winfit.Modulos.Outros;
using System.Text.RegularExpressions;
using BmsSoftware.Modulos.Lote;
using BmsSoftware.Modulos.IMEXApp;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPedidoNormal : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        Boolean FLAGORCAMENTO = false;
        Utility Util = new Utility();
        public Boolean FLAGPEDSIMPLES = false;
        public int CodClienteSelec = -1;
        public int CodFuncionario = -1;

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl2 = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        MATERIALCollection MATERIALColl = new MATERIALCollection();
        PRODUTOSCollection PRODUTOSMTColl = new PRODUTOSCollection();
        CORCollection CORColl = new CORCollection();
        CORCollection COR2Coll = new CORCollection();
        LIS_PRODUTOCOMPOSICAOCollection LIS_PRODUTOCOMPOSICAOColl = new LIS_PRODUTOCOMPOSICAOCollection();

        MENSAGEMProvider MENSAGEMP = new MENSAGEMProvider();
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
        LIS_PRODUTOSPEDIDOMTProvider LIS_PRODUTOSPEDIDOMTP = new LIS_PRODUTOSPEDIDOMTProvider();
        MATERIALProvider MATERIALP = new MATERIALProvider();
        PRODUTOSPEDIDOMTQProvider PRODUTOSPEDIDOMTQP = new PRODUTOSPEDIDOMTQProvider();
        CORProvider CORP = new CORProvider();
        LIS_PRODUTOCOMPOSICAOProvider LIS_PRODUTOCOMPOSICAOP = new LIS_PRODUTOCOMPOSICAOProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        ESTOQUELOTEProvider ESTOQUELOTEP = new ESTOQUELOTEProvider();

        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();

        string CasasDecimais = string.Empty;
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public Boolean ExibiDados = false;

        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        public FrmPedidoNormal()
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
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);

                string PRAZOENTREGA = txtPrazoEntrega.Text;

                int? IDTRANSPORTES = null;
                if (Convert.ToInt32(cbTransportadora.SelectedValue) > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                int? IDVENDEDOR = null;
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

               // if (!ValidacoesLibrary.ValidaTipoDecimal(txtValComissao.Text))
            //        txtValComissao.Text = "0,00";

                decimal VALORCOMISSAO = 0;//Convert.ToDecimal(txtValComissao.Text);

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
                if (Convert.ToInt32(cbLocalCobranca.SelectedValue) > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                int? IDCENTROCUSTOS = null;
                if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

                string FLAGPRODIMPRESSAO ="S";
                string PRODUTOFINAL = string.Empty;
                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
                string NREFERENCIA = txtNumReferencia.Text;
                string FLAGVLMETRO = "N";

                DateTime? DataEntrega = null;
                if (dateTimePicker1.Checked)
                    DataEntrega = Convert.ToDateTime(dateTimePicker1.Value);

                string FLAGTELABLOQUEADA = chkTelaBloqueada.Checked ? "S" : "N";

                decimal TIPOPAGTODINHEIRO = Convert.ToDecimal(txtTipoPagamentoDinheiro.Text);
                decimal TIPOPAGTOCHEQUE = Convert.ToDecimal(txtTipoPagamentoCheque.Text);
                decimal TIPOPAGTOCARTAODEBITO = Convert.ToDecimal(txtTipoPagamentoCartaoDebito.Text);
                decimal TIPOPAGTOCARTAOCREDITO = Convert.ToDecimal(txtTipoPagamentoCartaoCredito.Text);

                DateTime? DATAVECTO = null;
                if (mkdDataVecto.Text != "  /  /")
                    DATAVECTO = Convert.ToDateTime(mkdDataVecto.Text);

                int? IDSUPERVISOR = null;
                if (Convert.ToInt32(cbSupervisor.SelectedValue) > 0)
                    IDSUPERVISOR = Convert.ToInt32(cbSupervisor.SelectedValue);
                int? IDMESA = null;               

                return new PEDIDOEntity(_IDPEDIDO, IDCLIENTE, DTEMISSAO, IDSTATUS, PRAZOENTREGA,
                                        IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO, OBSERVACAO,
                                        TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                        VALORDESCONTO, PORACRESCIMO, VALORACRESCIMO,
                                        TOTALPEDIDO, IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR,
                                        IDLOCALCOBRANCA, IDCENTROCUSTOS, FLAGPRODIMPRESSAO, PRODUTOFINAL,
                                        FLAGORCAMENTO, NREFERENCIA, FLAGVLMETRO, string.Empty, DataEntrega,
                                        FLAGTELABLOQUEADA, TIPOPAGTODINHEIRO, TIPOPAGTOCHEQUE,
                                        TIPOPAGTOCARTAODEBITO, TIPOPAGTOCARTAOCREDITO, DATAVECTO, IDSUPERVISOR,
                                        IDMESA);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDO = value.IDPEDIDO;
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                    cbCliente.SelectedValue = value.IDCLIENTE;

                    txtStatusFinaCliente.Text = string.Empty;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");
                    txtPrazoEntrega.Text = value.PRAZOENTREGA;

                    if (value.IDSTATUS != null)
                        cbStatus.SelectedValue = value.IDSTATUS;
                    else
                        cbStatus.SelectedIndex = 0;

                    if (value.IDTRANSPORTES != null)
                        cbTransportadora.SelectedValue = value.IDTRANSPORTES;
                    else
                        cbTransportadora.SelectedIndex = 0;

                    if (value.IDVENDEDOR != null)
                        cbFuncionario.SelectedValue = value.IDVENDEDOR;
                    else
                        cbFuncionario.SelectedIndex = 0;

                  //  txtValComissao.Text = Convert.ToDecimal(value.VALORCOMISSAO).ToString("n2");
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
                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S" ? true : false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = value.NREFERENCIA;

                    if(value.DATAENTREGA != null)
                    {
                        dateTimePicker1.Text = Convert.ToDateTime(value.DATAENTREGA).ToString("dd/MM/yyyy");
                        dateTimePicker1.Checked = true;

                    }
                     else
                        dateTimePicker1.Checked = false;

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


                    txtTipoPagamentoDinheiro.Text = Convert.ToDecimal(value.TIPOPAGTODINHEIRO).ToString("n2");
                    txtTipoPagamentoCheque.Text = Convert.ToDecimal(value.TIPOPAGTOCHEQUE).ToString("n2");
                    txtTipoPagamentoCartaoDebito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAODEBITO).ToString("n2");
                    txtTipoPagamentoCartaoCredito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAOCREDITO).ToString("n2");

                    if (value.DATAVECTO != null)
                        mkdDataVecto.Text = Convert.ToDateTime(value.DATAVECTO).ToString("dd/MM/yyy");
                    else
                        mkdDataVecto.Text = "  /  /";

                    if (value.IDSUPERVISOR != null)
                        cbSupervisor.SelectedValue = value.IDSUPERVISOR;
                    else
                        cbSupervisor.SelectedIndex = 0;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPEDIDO = -1;
                    txtNPedido.Text = string.Empty;

                    //Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);

                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbStatus.SelectedIndex = 0;
                    txtPrazoEntrega.Text = string.Empty;
                    cbTransportadora.SelectedIndex = 0;
                    cbFuncionario.SelectedIndex = 0;
                   // txtValComissao.Text = string.Empty;
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
                   // txtValComissao.Text = "0,00";
                    txtValorPago.Text = "0,00";
                    txtValorDev.Text = "0,00";

                  
                    cbFormaPagto.SelectedIndex = 0;

                    //Preenche Mensagem Salvo na configuração do Sistema
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    txtObservacao.Text = CONFISISTEMAP.Read(1).MSGPEDIDO;

                    cbLocalCobranca.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;               

                    dateTimePicker1.Checked = false;

                    rdOrcamento.Checked = true;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = string.Empty;

                    chkTelaBloqueada.Checked = false;
                    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;

                    txtTipoPagamentoDinheiro.Text = "0,00";
                    txtTipoPagamentoCheque.Text = "0,00";
                    txtTipoPagamentoCartaoDebito.Text = "0,00";
                    txtTipoPagamentoCartaoCredito.Text = "0,00";

                    mkdDataVecto.Text = "  /  /";

                    cbSupervisor.SelectedIndex = 0;
                    
                    txtNumReferencia.Focus();
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
                decimal VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                decimal COMISSAO = 0;

                SomaUnitMTLinear();
                         
                decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                COMISSAO = (VALORTOTAL * PorcComissao) / 100;
               

                int? IDCOR = null;
                if (Convert.ToInt32(cbCor.SelectedValue) > 0)
                    IDCOR = Convert.ToInt32(cbCor.SelectedValue);

                decimal? TOTALMT = 0;
                if (_IDPRODPEDIDO != -1)
                {
                    PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                    PRODUTOSPEDIDOTy = PRODUTOSPEDIDOP.Read(_IDPRODPEDIDO);
                    
                    if(PRODUTOSPEDIDOTy != null)
                        TOTALMT = PRODUTOSPEDIDOTy.TOTALMT;
                }

                string DADOSADICIONAIS = txtDadosAdicionais.Text;

                string FLAGEXIBIR = chkExibirProd.Checked ? "N" : "S";

                decimal BUSTO = 0;
                decimal CINTURA = 0;
                decimal QUADRIL = 0;
                decimal COLARINHO = 0;
                decimal MANGA = 0;
                decimal BARRA = 0;

                return new PRODUTOSPEDIDOEntity(_IDPRODPEDIDO, _IDPEDIDO, IDPRODUTO,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO, IDCOR, TOTALMT, FLAGEXIBIR,
                                                DADOSADICIONAIS, 0, 0, null, null,0,0,
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
                    SomaUnitMTLinear();

                    if (value.IDCOR != null)
                        cbCor.SelectedValue = value.IDCOR;
                    else
                        cbCor.SelectedValue = -1;

                    txtDadosAdicionais.Text = value.DADOSADICIONAIS;
                    chkExibirProd.Checked = value.FLAGEXIBIR.TrimEnd().TrimStart() == "N" ? true : false;
                    string ExibirProduto = chkExibirProd.Checked ? "N" : "S";

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDIDO = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = "1";
                    txtValorUnitProd.Text = "0,0000";
                    txtDadosAdicionais.Text = string.Empty;
                    chkExibirProd.Checked = false;
                    cbCor.SelectedValue = -1;
                    SomaUnitMTLinear();

                    errorProvider1.Clear();
                }
            }
        }
      

        private void FrmPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.F7)
            {
                btnAddPecas_Click(null, null);
            }           

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmPedidoVenda_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

           // this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            EMPRESATy = EMPRESAP.Read(1);

            GetToolStripButtonCadastro();

            GetDropCliente();
            GetDropStatus();
            GetDropStatus();
            GetDropProdutos();
            GetTransporte();
            GetFuncionario();
            GetFuncionario2();
            GetSupervisor();
            GetDropCentroCusto();
            GetDropCentroCusto2();
            GetDropStatus2();
            GetDropFormaPgto();
            GetDropLocalCobranca();
            GetDropTipoDuplicata();
            GetDropCor();
            GetDropMensagem();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnCadMensagem.Image = Util.GetAddressImage(6);  
          
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadCor.Image = Util.GetAddressImage(6);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
          

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
              
              
            }
            else
            {
                cbCliente.SelectedValue = CodClienteSelec;
                cbFuncionario.SelectedValue = CodFuncionario;
                rdVenda.Checked = true;
                rdOrcamento.Checked = false;
            }

            chkPesqCodBarra.Checked = false;
            chkCodRef.Checked = false;
            if (BmsSoftware.ConfigSistema1.Default.TipoPesquisaProduto == "1")//Código de Referencia
                chkCodRef.Checked = true;
            else if (BmsSoftware.ConfigSistema1.Default.TipoPesquisaProduto == "2")//Código de Barra
                chkPesqCodBarra.Checked = true;

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


            this.Cursor = Cursors.Default;

            VerificaAcesso();

            cbCamposPesquisa.SelectedIndex = 2;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);
        }

        private void GetDropCliente()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

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

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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

        private void GetDropStatus2()
        {
            try
            {
                //11 Pedido de Venda
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

                STATUSProvider STATUSP = new STATUSProvider();
                STATUSCollection STATUSColl2 = new STATUSCollection();
                STATUSColl2 = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus2.DisplayMember = "NOME";
                cbStatus2.ValueMember = "IDSTATUS";

                STATUSEntity STATUSTy = new STATUSEntity();
                STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTy.IDSTATUS = -1;
                STATUSColl2.Add(STATUSTy);

                Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus2.DisplayMember);

                STATUSColl2.Sort(comparer.Comparer);
                cbStatus2.DataSource = STATUSColl2; 
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
                LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));
                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTOCOD");

                cbProduto.DisplayMember = "NOMEPRODUTOCOD";
                cbProduto.ValueMember = "IDPRODUTO";

                LIS_PRODUTOSEntity PRODUTOSTy = new LIS_PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTOCOD = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                LIS_PRODUTOSColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>(cbProduto.DisplayMember);

                LIS_PRODUTOSColl.Sort(comparer.Comparer);
                cbProduto.DataSource = LIS_PRODUTOSColl;

                cbProduto.SelectedIndex = 0;
                this.Cursor = Cursors.Default;	
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;	
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

        private void GetSupervisor()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbSupervisor.DisplayMember = "NOME";
                cbSupervisor.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbSupervisor.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbSupervisor.DataSource = FUNCIONARIOColl;

                cbSupervisor.SelectedIndex = 0;
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

        private void GetFuncionario2()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbFuncionario2.DisplayMember = "NOME";
                cbFuncionario2.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario2.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbFuncionario2.DataSource = FUNCIONARIOColl;

                cbFuncionario2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCentroCusto2()
        {
            try
            {
                CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
                CENTROCUSTOSColl2 = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

                cbCentroCusto2.DisplayMember = "DESCRICAO";
                cbCentroCusto2.ValueMember = "IDCENTROCUSTOS";

                CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
                CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
                CENTROCUSTOSColl2.Add(CENTROCUSTOSTy);

                Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto2.DisplayMember);

                CENTROCUSTOSColl2.Sort(comparer.Comparer);
                cbCentroCusto2.DataSource = CENTROCUSTOSColl2;
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
                        cbCliente.SelectedValue = result;
                        CLIENTEEntity CLIENTEtY = new CLIENTEEntity();
                        CLIENTEtY = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue));

                        if (CLIENTEtY != null)
                        {
                            if (CLIENTEtY.IDFUNCIONARIO != null)
                                cbSupervisor.SelectedValue = CLIENTEtY.IDFUNCIONARIO;

                            if (CLIENTEtY.IDTRANSPORTADORA != null)
                                cbTransportadora.SelectedValue = CLIENTEtY.IDTRANSPORTADORA;
                        }
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

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUS = CodSelec;
                frm.ShowDialog();
                
                GetDropStatus();
                GetDropStatus2();
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

                CLIENTEEntity CLIENTEtY = new CLIENTEEntity();
                CLIENTEtY = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue));

                if (CLIENTEtY != null)
                {
                    if (CLIENTEtY.IDFUNCIONARIO != null)
                    cbSupervisor.SelectedValue = CLIENTEtY.IDFUNCIONARIO;

                    if (CLIENTEtY.IDTRANSPORTADORA != null)
                    cbTransportadora.SelectedValue = CLIENTEtY.IDTRANSPORTADORA;
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
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
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
            else
            {
                using (FrmProduto2 frm = new FrmProduto2())
                {
                    int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    frm._IDPRODUTO = CodSelec;
                    frm.ShowDialog();
                    GetDropProdutos();
                    cbProduto.SelectedValue = CodSelec;
                }
            }
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkPesqCodBarra.Checked || chkCodRef.Checked)
                    PesquisaProduto(cbProduto.Text);
            }

            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.TipoPreco = 1;
                    if (TipoPreco2.Checked)
                        frm.TipoPreco = 2;
                    else if (TipoPreco3.Checked)
                        frm.TipoPreco = 3;

                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                        txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");

                        //PRODUTOSTy = PRODUTOSP.Read(result);
                        //if (PRODUTOSTy != null)
                        //{
                        //    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                        //    if (TipoPreco2.Checked)
                        //        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA2).ToString("n2");
                        //    else if (TipoPreco3.Checked)
                        //        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA3).ToString("n2");

                        //    txtQuanProduto.Focus();
                        //}
                    }
                }
            }
        }

        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
        private void PesquisaProduto(string IDPRODUTO)
        {
            PRODUTOSTy = null;
            if (chkPesqCodBarra.Checked)
                PRODUTOSTy = PesquisaCodBarra(cbProduto.Text);
            else if (chkCodRef.Checked)
                PRODUTOSTy = PesquisaCodReferencia(cbProduto.Text);
            else
            {
                if (ValidacoesLibrary.ValidaTipoInt32(cbProduto.Text))
                {
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(IDPRODUTO));
                    if(PRODUTOSTy != null)
                        cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                }
            }

            if (PRODUTOSTy == null)
            {
                cbProduto.Focus();
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
                            if (PRODUTOSTy != null)
                            {
                                cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                               
                                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                                if (TipoPreco2.Checked)
                                    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA2).ToString("n2");
                                else if (TipoPreco3.Checked)
                                    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA3).ToString("n2");

                                txtQuanProduto.Focus();
                                Beep(1000, 300);
                                btnAddPecas_Click(null, null);
                            }
                        }
                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                if (PRODUTOSTy != null)
                {
                    cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;

                    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                    if (TipoPreco2.Checked)
                        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA2).ToString("n2");
                    else if (TipoPreco3.Checked)
                        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA3).ToString("n2");

                    txtQuanProduto.Focus();
                    Beep(1000, 300);
                    btnAddPecas_Click(null, null);
                }
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

        private PRODUTOSEntity PesquisaCodReferencia(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", Pesquisa));

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

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }

            SomaUnitMTLinear();
        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                txtValorUnitProd.Text = "0,00";
            }
            else
            {
                Double f = Convert.ToDouble(txtValorUnitProd.Text);
                txtValorUnitProd.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnitProd, "");
                SomaUnitMTLinear();
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
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                
                GetFuncionario();
                GetFuncionario2();
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
                GetDropCentroCusto2();

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
           
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
                Grava();
           
        }

        private void Grava()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
               

                if (Validacoes())
                {
                    SumTotalPedido();

                    if (_IDPEDIDO == -1)
                        _IDPEDIDO = PEDIDOP.Save(Entity);
                    else
                    {
                        _IDPEDIDO = PEDIDOP.Save(Entity);
                        btnPesquisa_Click(null, null);
                    }

                    this.Cursor = Cursors.Default;
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro ténico: " + ex.Message);

            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbCliente.SelectedValue) < 0)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");                
                result = false;
            }
            else if(_IDPEDIDO == -1 && !VerificaPlanos())
            {
                result = false;
            }
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 0 || cbStatus.SelectedValue == null)
            {
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }            
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
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
          
            else if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagVendedorObrigatorio == "S" && Convert.ToInt32(cbFuncionario.SelectedValue) < 0)
            {
                tabControl1.SelectTab(1);
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio == "S" && Convert.ToInt32(cbCentroCusto.SelectedValue) < 0)
            {
                tabControl1.SelectTab(3);
                errorProvider1.SetError(cbCentroCusto, ConfigMessage.Default.CampoObrigatorio);
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
                    txtPorcDesconto.Text = "0,00";
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
                    txtTotalDesconto.Text = "0,00";
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
                    txtPorcAcrescimo.Text = "0,00";
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
                    txtTotalAcrescimo.Text = "0,00";
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
                    txtTotalIPI.Text = "0,00";
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

                if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario2.SelectedValue.ToString());
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

                lblTotalPesquisa.Text = (LIS_PEDIDOColl.Count-1).ToString();
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
                foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
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
            try
            {
                // referente ao tipo de campo
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

                    if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario2.SelectedValue.ToString());
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

                    lblTotalPesquisa.Text = (LIS_PEDIDOColl.Count - 1).ToString();
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
           
        }     

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
              
                ListaProdutoPedido(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            }
        }

        private Boolean VerificaPlanos()
        {
            Boolean result = true;

            try
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S")
                {

                    PEDIDOCollection PEDIDOCollColl_Total = new PEDIDOCollection();
                    PEDIDOCollColl_Total = PEDIDOP.ReadCollectionByParameter(null);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim()));

                    if (RECURSOSPLANOTy != null)
                    {
                        int Quant = Convert.ToInt32(RECURSOSPLANOTy.PEDIDO);

                        if (PEDIDOCollColl_Total.Count < Quant)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            MessageBox.Show("Limite de Pedido atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
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
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
              
                ListaProdutoPedido(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            }
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
                Grava();
            
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
           // if (_IDPEDIDO == -1)
           // {

              //  Util.ExibirMSg("Antes de adicionar os produtos é necessário gravar o Pedido!", "Red");
           // }
           // else 
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
            }
            else
            {
                GravaProduto();

                //Lista Comissao dos produtos
                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                //if (FLAGCOMISSAO == "N")
                 //   SumTotalComissao();

                
            }
        }
       

        private void GravaProduto()
        {
            try
            {
                if (Validacoes() && ValidacoesProdutos())
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
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
                MessageBox.Show("Erro ténico: " + ex.Message);
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
             
                if (Convert.ToDecimal(txtEstoqueAtual.Text) < Convert.ToDecimal(txtQuanProduto.Text))
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

        private Boolean VerificaCredito()
        {
            Boolean result = false;
            decimal LimiteCredito = Convert.ToDecimal(CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).CREDITOCLIENTE);

            if (CONFISISTEMAP.Read(1).FLAGLIMITECREDITO.TrimEnd().TrimStart() == "S" && LimiteCredito > 0)
            {

                decimal ValorCompraAtual = Convert.ToDecimal(txtTotalProduto.Text) + Convert.ToDecimal(txtValorMtLinearTotal.Text);
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
                RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
                RowpProdPedido.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

                if (BmsSoftware.ConfigSistema1.Default.FlagOrdenarProdutoPedido == "S")
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowpProdPedido, "IDPRODUTO");
                else
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowpProdPedido, "IDPRODPEDIDO");
                
                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

                SumTotalProdutosPedido();

                //Vai para ultima linha do grid
                if (LIS_PRODUTOSPEDIDOColl.Count > 1)
                    DGDadosProduto.CurrentCell = DGDadosProduto.Rows[DGDadosProduto.Rows.Count - 1].Cells[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }      


        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
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
            Filtro.Clear();
            Filtro.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_CONTROLEENTREGACollection LIS_CONTROLEENTREGAColl = new LIS_CONTROLEENTREGACollection();
            LIS_CONTROLEENTREGAProvider LIS_CONTROLEENTREGAP = new LIS_CONTROLEENTREGAProvider();
            LIS_CONTROLEENTREGAColl = LIS_CONTROLEENTREGAP.ReadCollectionByParameter(Filtro, "nomeproduto, DATAENTREGA");

            result = LIS_CONTROLEENTREGAColl.Count;

            return result;          
        }


        private void Delete()
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
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
                        PEDIDOP.Delete(_IDPEDIDO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        
                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro ténico: " + ex.Message);
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
            if (LIS_PRODUTOSPEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    //Movimento de Estoque
                    //Podera alterar somente se nao estiver controlando o estoque
                    if (FLAGPEDBAIXAESTOQUE == "N")
                    {
                        CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO);
                        Entity2 = PRODUTOSPEDIDOP.Read(CodSelect);
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel alterar o itens do Pedido!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                    }
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO);
                            decimal QUANTMOV = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].QUANTIDADE);
                            int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODUTO);
                            PRODUTOSPEDIDOP.Delete(CodSelect);
                         
                            ListaProdutoPedido(_IDPEDIDO);
                            Entity2 = null;

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
            RowRelatorio.Add(new RowsFiltro("IDCOMPOSICAO", "System.String", "like", IDCOMPOSICAO.ToString()));
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

                if (PRODUTOStY != null)
                {
                    txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                    if (TipoPreco2.Checked)
                        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA2).ToString("n2");
                    else if (TipoPreco3.Checked)
                        txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA3).ToString("n2");
                }

                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;

                //Salva o produtos no Pedido
                PRODUTOSPEDIDOP.Save(Entity2);             
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);
           
        }

        private void bntCadMovim_Click(object sender, EventArgs e)
        {

        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                if (PRODUTOS_EstoqTy != null)
                {
                    decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);
                    if (TipoPreco2.Checked)
                        ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA2);
                    else if (TipoPreco3.Checked)
                        ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA3);

                    txtValorUnitProd.Text = ValorVenda.ToString("n2");
                    decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(PRODUTOS_EstoqTy.IDPRODUTO), false);
                    txtEstoqueAtual.Text = Convert.ToDecimal(ESTOQUEATUAL).ToString("n2");

                    txtEstMinimo.Text = Convert.ToDecimal(PRODUTOS_EstoqTy.QUANTIDADEMINIMA).ToString("n3");
                }
            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPorComisVend_Validating(object sender, CancelEventArgs e)
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

                decimal Valor = Convert.ToDecimal(txtValorDev.Text) == 0 ? Convert.ToDecimal(txtTotalFinanceiro.Text) : Convert.ToDecimal(txtValorDev.Text);
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
                DUPLICATARECEBERty.NOTAFISCAL = "PD" + txtNPedido.Text;

                //Comissao Vendedor               
                 // DUPLICATARECEBERty.COMISSAO = (Valor * Convert.ToDecimal(txtPorComisVend.Text)) / 100;

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
                 //    DUPLICATARECEBERty.COMISSAO = (Valor * Convert.ToDecimal(txtPorComisVend.Text)) / 100;

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
                else if (ColumnSelecionada == 2) //Boleto
                {
                    ImprimirBoletaDireto(Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER));
                }
                else if (ColumnSelecionada == 3) //Baixa
                {
                    using (FrmBaixarTotalReceber FrmBaixar = new FrmBaixarTotalReceber())
                    {
                        FrmBaixar._idDuplicata = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                        FrmBaixar.ShowDialog();
                        GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                    }
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
                    txtValorPago.Text = "0,00";
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
                    txtValorDev.Text = "0,00";
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
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                frm.ShowDialog();
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
                MessageBox.Show("Antes de adicionar o Caixa é necessário gravar o Pedido!",
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
            if (!VerificaCaixaPD("PD" + txtNPedido.Text))
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

                CaixaTy.NDOCUMENTO = "PD" + txtNPedido.Text;
                CaixaTy.OBSERVACAO = "Pedido de Venda nº " + "PD" + txtNPedido.Text + " Cliente: " + cbCliente.SelectedValue + " - " + GetNameCliente(Convert.ToInt32(cbCliente.SelectedValue));

                try
                {
                    CaixaP.Save(CaixaTy);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possível fazer o movimento de caixa!");
                }
            }
        }

        private Boolean VerificaCaixaPD(string NDOCUMENTO)
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
       

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    PEDIDOP.Save(Entity);
                    ImprimirReceitaReport();
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

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
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
           
        }

       
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
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


        private LIS_PRODUTOSPEDIDOCollection ProdutoRel(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOColl;
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (LIS_PEDIDOColl.Count > 0)
                {
                    string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                    if (orderBy.Trim() != string.Empty)
                    {
                        Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity>(orderBy);

                        LIS_PEDIDOColl.Sort(comparer.Comparer);

                        DataGriewDados.DataSource = null;
                        DataGriewDados.DataSource = LIS_PEDIDOColl;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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


        private void orçamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }


        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
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
                tabControlPedidoVenda.SelectTab(1);
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

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
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


                decimal SUBTOTAL = Convert.ToDecimal(txtTotalFinanceiro.Text);
                ticket.AddTotal("SUBTOTAL", SUBTOTAL.ToString("n2"));
                ticket.AddTotal("DESCONTO", txtTotalDesconto.Text);
                ticket.AddTotal("ACRESCIMO:", txtTotalAcrescimo.Text);
                ticket.AddTotal("PAGO", txtValorPago.Text);
                ticket.AddTotal("TOTAL", txtValorDev.Text);

                ticket.AddFooterLine(txtObservacao.Text.Replace("\r\n", " "));
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


        private void chkDetalProdFinal_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void txtValorTotal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMtLinearTotal.Text))
            {
                errorProvider1.SetError(txtValorMtLinearTotal, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                SomaUnitMTLinear();
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

        private void SomaUnitMTLinear()
        {
            try
            {
                if (txtQuanProduto.Text.Trim() == string.Empty)
                    txtQuanProduto.Text = "1";

                if (txtValorUnitProd.Text.Trim() == string.Empty)
                    txtValorUnitProd.Text = "0,00";

                txtValorMtLinearTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o cálculo!");
            }

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
                            ExcluirEstoqueLote(Convert.ToInt32(item.IDPRODUTO));
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
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (chkTelaBloqueada.Checked)
                    {
                        string msgerro = "Tela bloqueada!";
                        errorProvider1.SetError(chkTelaBloqueada, msgerro);
                        Util.ExibirMSg(msgerro, "Red");
                    }
                    else
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
                                    ExcluirEstoqueLote(IDPRODUTO);
                                    PRODUTOSPEDIDOP.Delete(CodSelect);
                                    ListaProdutoPedido(_IDPEDIDO);

                                    Entity2 = null;

                                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                }
                            }
                            catch (Exception ex)
                            {
                                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                                MessageBox.Show("Erro técnico: " + ex.Message);

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
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "=", "PD" + _IDPEDIDO.ToString().PadLeft(6, '0')));
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                
                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);
                
                int Contador = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    ESTOQUELOTEP.Delete(Convert.ToInt32(item.IDESTOQUELOTE));
                    Contador++;
                }

                if (Contador > 0)
                    Util.ExibirMSg("Estoque Lote Excluido com Sucesso!", "blue");

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void modelo2DiversosItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
       
        private void printDocument7_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        
        }

        private void printDocument7_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
        }

        private void printDocument_Prod3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
        }

        private void printDocument_Prod3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


            if (!FLAGORCAMENTO)
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
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
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
            //Inicio Material MT2
            //Material MT2
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);
            linha = linha + 5;
            e.Graphics.DrawString("Produtos MT2", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

            linha = linha + 15;
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 690, 20);

            e.Graphics.DrawString("Altura", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 630, 20);

            e.Graphics.DrawString("Largura", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 560, 20);

            e.Graphics.DrawString("M2", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 220, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 490, 20);

            e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 290, linha + 5);
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 200, 20);

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


        }

        private void printDocumentRodape_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
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

        private void baixarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
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

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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

                                        txtValorPago.Text = txtValorDev.Text;
                                        txtValorDev.Text = "0,00";
                                        //Salva a forma de pagamento no Pedido
                                        PEDIDOP.Save(Entity);

                                        if (LIS_DUPLICATARECEBERColl.Count > 0)
                                            mkdDataVecto.Text = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");

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
                                PEDIDOP.Save(Entity);

                                if (LIS_DUPLICATARECEBERColl.Count > 0)
                                    mkdDataVecto.Text = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");


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
                    frm.ValorPedido = txtValorDev.Text == "0,00" ? txtTotalFinanceiro.Text : txtValorDev.Text;
                    frm.NotaFiscal = "PD" + txtNPedido.Text;
                    frm.ShowDialog();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
            }
        }

        private void produtosPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoCliente frm = new FrmProdutoCliente())
            {
                frm.ShowDialog();
            }
        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (LIS_PEDIDOColl.Count == 0)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                    tabControlPedidoVenda.SelectTab(1);
                    txtCriterioPesquisa.Focus();
                }
                else
                {
                    ImprimirListaGeralProduto();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        string DataMovimentacaoFinal = string.Empty;
        string DataMovimentacaoInicial = string.Empty;
        private void períodoToolStripMenuItem_Click(object sender, EventArgs e)
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

                if (LIS_PEDIDOColl.Count > 1)
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

        private void controleDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                        AddProdutoComposicaoProduto(result);
                }
            }
        }

        private void AddProdutoComposicaoProduto(int IDPRODUTOMAIN)
        {
            //Filtras os produtos da determinada composicao
            RowsFiltroCollection ProdutoComposicao = new RowsFiltroCollection();
            ProdutoComposicao.Add(new RowsFiltro("IDPRODUTOMAIN", "System.Int32", "=", IDPRODUTOMAIN.ToString()));
            LIS_PRODUTOCOMPOSICAOColl = LIS_PRODUTOCOMPOSICAOP.ReadCollectionByParameter(ProdutoComposicao, "IDPRODUTOMAIN");

            //Adicionar O Produto Principal
            ///// inicio \\\\\\
            PRODUTOSEntity PRODUTOSTy_Master = new PRODUTOSEntity();
            PRODUTOSTy_Master = PRODUTOSP.Read(IDPRODUTOMAIN);
            cbProduto.SelectedValue = PRODUTOSTy_Master.IDPRODUTO;

            //string QuantidadeProduto = InputBox("Digite a quantidade doo produto: " + PRODUTOSTy_Master.NOMEPRODUTO, ConfigSistema1.Default.NomeEmpresa, "1");
            //if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text)) QuantidadeProduto = "1";
            //if (QuantidadeProduto.Trim() == string.Empty) QuantidadeProduto = "1";

            //txtQuanProduto.Text = Convert.ToDecimal(QuantidadeProduto).ToString("n4");
            //Pega o total geral
            decimal TotalGeral = 0;
            foreach (LIS_PRODUTOCOMPOSICAOEntity item in LIS_PRODUTOCOMPOSICAOColl)
            {
                   PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                   TotalGeral += (Convert.ToDecimal(item.QUANTIDADE) * Convert.ToDecimal(PRODUTOStY.VALORVENDA1));
            }

            txtValorUnitProd.Text = TotalGeral.ToString("n4");
            btnAddPecas_Click(null, null);
            ////Adicionar O Produto Principal
            ///// Fim \\\\\\

            //Percorre os produtos da coleção
            foreach (LIS_PRODUTOCOMPOSICAOEntity item in LIS_PRODUTOCOMPOSICAOColl)
            {
                cbProduto.SelectedValue = item.IDPRODUTO;
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");
                chkExibirProd.Checked = item.FLAGEXIBIR.Trim() == "N" ? true : false;

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;


                //Obtem o valor de venda do produto
               // PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = "0";//Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;

                //Salva o produtos no Pedido
                PRODUTOSPEDIDOP.Save(Entity2);              
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDO);

            //Grava os dados do Pedido
            PEDIDOP.Save(Entity);

            cbProduto.SelectedValue = IDPRODUTOMAIN;
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

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
            }
        }

        private void vendasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void produtosMaisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidos FrmP = new FrmProdutosMaisVendidos();
            FrmP.ShowDialog();
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
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

        private void vendasDeProdutoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
             FrmProdutoClientePedidoSimples FrmV = new FrmProdutoClientePedidoSimples();
            FrmV.ShowDialog();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void modelo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    PEDIDOP.Save(Entity);
                    ImprimirReceitaReport();
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

        private void ImprimirReceitaReport()
        {
            using (FrmRelatPedidoSimples frm = new FrmRelatPedidoSimples())
            {
                frm.codreferencia = txtNumReferencia.Text;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void lnkSelecCliente_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //Busca Enderço de Entrega
                ENDENTREGARCLIENTECollection ENDENTREGARCLIENTEColl = new ENDENTREGARCLIENTECollection();
                ENDENTREGARCLIENTEProvider ENDENTREGARCLIENTEP = new ENDENTREGARCLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
                ENDENTREGARCLIENTEColl = ENDENTREGARCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                if (ENDENTREGARCLIENTEColl.Count > 0)
                {
                    txtObservacao.Text += "Dados da Entrega:" + System.Environment.NewLine.ToString();
                    txtObservacao.Text += ENDENTREGARCLIENTEColl[0].NOME + System.Environment.NewLine.ToString();
                    txtObservacao.Text += ENDENTREGARCLIENTEColl[0].ENDERECO + " " + ENDENTREGARCLIENTEColl[0].NUMERO;
                    txtObservacao.Text += ENDENTREGARCLIENTEColl[0].COMPLEMENTO + " " + ENDENTREGARCLIENTEColl[0].BAIRRO + " " + ENDENTREGARCLIENTEColl[0].CEP + System.Environment.NewLine.ToString();
                    txtObservacao.Text += ENDENTREGARCLIENTEColl[0].CIDADE + " " + ENDENTREGARCLIENTEColl[0].UF + System.Environment.NewLine.ToString();
                }
                else
                    MessageBox.Show("Endereço de entrega não localizado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void BloqueoTela()
        {
            try
            {
                //RowRelatorio.Clear();
                //RowRelatorio.Add(new RowsFiltro("nomeformulario", "System.String", "=", this.Name));
                //LIS_BLOQUEIOTELACollection LIS_BLOQUEIOTELAColl = new LIS_BLOQUEIOTELACollection();
                //LIS_BLOQUEIOTELAProvider LIS_BLOQUEIOTELAP = new LIS_BLOQUEIOTELAProvider();
                //LIS_BLOQUEIOTELAColl = LIS_BLOQUEIOTELAP.ReadCollectionByParameter(RowRelatorio);

                //if (LIS_BLOQUEIOTELAColl.Count > 0)
                //{
                //    PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                //    PEDIDOTy = PEDIDOP.Read(_IDPEDIDO);
                //    PEDIDOTy.FLAGTELABLOQUEADA = "S";
                //    PEDIDOP.Save(PEDIDOTy);

                //    chkTelaBloqueada.Checked = true;
                //    chkTelaBloqueada.Text = "Tela Bloqueada";
                //    chkTelaBloqueada.ForeColor = System.Drawing.Color.Red;
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

     

        private void chkTelaBloqueada_Click(object sender, EventArgs e)
        {
           // if (!chkTelaBloqueada.Checked)
           // {
           //     if (!chkTelaBloqueada.Checked)
           //     {
           //         DialogResult dr = MessageBox.Show("Deseja desbloquear esta tela?",
           //                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

           //         if (dr == DialogResult.Yes)
           //         {
           //             DesBloqueoTela();
           //         }
           //         else
           //             chkTelaBloqueada.Checked = !chkTelaBloqueada.Checked;
           //     }
           // }
           // //else
           ////     chkTelaBloqueada.Checked = !chkTelaBloqueada.Checked;

            DesBloqueoTela(chkTelaBloqueada.Checked);
        }

        private void DesBloqueoTela(Boolean Bloqueia)
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
                    if (Bloqueia)
                        PEDIDOTy.FLAGTELABLOQUEADA = "S";
                    else
                        PEDIDOTy.FLAGTELABLOQUEADA = "N";

                    PEDIDOP.Save(PEDIDOTy);

                    chkTelaBloqueada.Checked = Bloqueia;
                    if (Bloqueia)
                        chkTelaBloqueada.Text = "Tela Bloqueada";
                    else
                        chkTelaBloqueada.Text = "Tela Desbloqueada";

                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;

                    if (Bloqueia)
                        Util.ExibirMSg("Tela bloqueada com sucesso", "Blue");
                    else
                        Util.ExibirMSg("Tela Desbloqueada com sucesso", "Blue");


                }
                else
                {
                    chkTelaBloqueada.Checked = false;
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

        private void duplicarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
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

                        //Busca Produto
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
                        PRODUTOSPEDIDOCollection PRODUTOSPEDIDOColl = new PRODUTOSPEDIDOCollection();
                        PRODUTOSPEDIDOColl = PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                        _IDPEDIDO = -1;
                        _IDPEDIDO = PEDIDOP.Save(Entity);

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
                        ListaProdutoPedido(_IDPEDIDO);

                        MessageBox.Show("Pedido Nº " + _IDPEDIDO.ToString().PadLeft(6, '0') + " criado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível duplicar o pedido selecionado!");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }

        private void modelo3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
       

        private void vendasPorTransportadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaTransportador frm = new FrmVendaTransportador())
            {
                frm.ShowDialog();
            }
        }

        private void vendasPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaporCidade frm = new FrmVendaporCidade())
            {
                frm.ShowDialog();
            }
        }

        private void totalDeVendaPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTotalVendaporProduto frm = new FrmTotalVendaporProduto())
            {
                frm.ShowDialog();
            }
        }

        private void gerarNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente gerar a NFe deste pedido?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmNotaFiscalEletronica frm = new FrmNotaFiscalEletronica())
                    {
                        frm.NumPedidoSimples = _IDPEDIDO;
                        frm.FlagGerarNFePedidoSimples = true;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void excluirItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmExcluirItensPedido frm = new FrmExcluirItensPedido())
                {
                    frm._IDPEDIDO = _IDPEDIDO;
                    frm.ShowDialog();
                    ListaProdutoPedido(_IDPEDIDO);
                }

            }
        }

        private void vendasDeProdutosPorCidadePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoVendaCidadeProdutoPedido frm = new FrmProdutoVendaCidadeProdutoPedido())
            {
                frm.ShowDialog();
            }
        }

        private void txtTipoPagamentoDinheiro_Validating(object sender, CancelEventArgs e)
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

        private void txtTipoPagamentoCheque_Validating(object sender, CancelEventArgs e)
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

        private void txtTipoPagamentoCartaoDebito_Validating(object sender, CancelEventArgs e)
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

        private void txtTipoPagamentoCartaoCredito_Validating(object sender, CancelEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
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
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).EMAILCLIENTE.Trim() == string.Empty)
            {
                MessageBox.Show("Email do cliente não selecionado!");
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

                if (rdOrcamento.Checked)
                    subject = "ORÇAMENTO Nº " + txtNPedido.Text;
                else
                    subject = "PEDIDO Nº " + txtNPedido.Text;

                string body = string.Empty;
                body = "Caro(a) cliente " + CLIENTETy.NOME + ", segue abaixo dados do Pedido/Orçamento <br>";        
                body += " " + "<br>";
                body += "Dados do produtos" + "<br>";

                StringBuilder html = new StringBuilder();
                html.Append("<html>");
                html.Append("<head>");
                html.Append("<body>");
                html.Append("<table");
                //add header row
                html.Append("<tr>");
                html.Append("<td>Produto </td>");
                html.Append("<td>Total</td>");
                html.Append("<td>Valor</td>");
                html.Append("</tr>");
                //add rows
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
                html.Append("<td>Total; </td>");
                html.Append("<td align=right >" + txtTotalPedido.Text + "</td>");
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

        private void completoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaVendedorPedNormal frm = new FrmVendaVendedorPedNormal())
            {
                frm.ShowDialog();
            }
        }

        private void resumoPorCentroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaVendedorPedNormalCentroCusto frm = new FrmVendaVendedorPedNormalCentroCusto())
            {
                frm.ShowDialog();
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

            MENSAGEMColl = MENSAGEMP.ReadCollectionByParameter(null);

            cbMensagem.DisplayMember = "NOME";
            cbMensagem.ValueMember = "IDMENSAGEM";

            MENSAGEMEntity MENSAGEMTy = new MENSAGEMEntity();
            MENSAGEMTy.NOME = ConfigMessage.Default.MsgDrop;
            MENSAGEMTy.IDMENSAGEM = -1;
            MENSAGEMColl.Add(MENSAGEMTy);

            Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity>(cbMensagem.DisplayMember);

            MENSAGEMColl.Sort(comparer.Comparer);
            cbMensagem.DataSource = MENSAGEMColl;

        }

        private void cbMensagem_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMensagem.SelectedValue) > 0)
                txtObservacao.Text += " " + MENSAGEMP.Read(Convert.ToInt32(cbMensagem.SelectedValue)).MENSAGEM;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmDadosAdicionalPedido frm = new FrmDadosAdicionalPedido())
            {
                frm.TextoAdicional = txtDadosAdicionais.Text;
                frm.ShowDialog();
                txtDadosAdicionais.Text = frm.TextoAdicional;
            }
        }

        private void vendaPorFormaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaPorFormaPagto frm = new FrmVendaPorFormaPagto())
            {
                frm.ShowDialog();
            }
        }

        private void vendasPorOutrosTipoDePagamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmOutrosTipodePagamento frm = new FrmOutrosTipodePagamento())
            {
                frm.ShowDialog();
            }
        }

        private void sICOOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void arquivosDeRemessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRemessaBanco().ShowDialog();
        }

        private void sICOOBCNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void arquivosDeRemessaToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                    tabControlPedidoVenda.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = PEDIDOP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDO);
                    txtNPedido.Focus();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);                   

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_PEDIDOColl[rowindex].IDPEDIDO.ToString().PadLeft(6, '0') + " - " + LIS_PEDIDOColl[rowindex].NOMECLIENTE,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                //Lista  Produto do Pedido                            
                                ListaProdutoPedido(Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO));

                                //Exluir Produto do Pedido
                                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                                {
                                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
                                }

                                //Lista  Produto do Pedido MT2
                                RowRelatorio.Clear();
                                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", LIS_PEDIDOColl[rowindex].IDPEDIDO.ToString()));
                                LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                                LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
                                LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                                //Excluir Produtos do Pedido MT2
                                foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                                {
                                    PRODUTOSPEDIDOMTQP.Delete(Convert.ToInt32(item.IDPRODUTOSPEDIDOMTQ));
                                }

                                //Lista  Duplicatas do Pedido
                                GridDuplicatasPD(Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDCLIENTE), LIS_PEDIDOColl[rowindex].IDPEDIDO.ToString().PadLeft(6, '0'));
                                //Exluir Duplicatas do Pedido
                                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                                {
                                    DUPLICATARECEBERP.Delete(Convert.ToInt32(item.IDDUPLICATARECEBER));
                                }


                                CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                                //Delete Pedido
                                PEDIDOP.Delete(CodigoSelect);

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
                        CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                        Entity = PEDIDOP.Read(CodigoSelect);
                        ListaProdutoPedido(_IDPEDIDO);

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

        private void modelo4EconômicoToolStripMenuItem_Click(object sender, EventArgs e)
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
                    ImprimirReceitaReport4();
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

        private void ImprimirReceitaReport4()
        {
            using (FrmRelatPedidoVendas2 frm = new FrmRelatPedidoVendas2())
            {
                PEDIDOP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
             
            }
        }

        private void modeloEconômicoToolStripMenuItem_Click(object sender, EventArgs e)
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
                    ImprimirReceitaReportEconomico();
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

        private void ImprimirModelo3()
        {
            using (FrmRelatPedidoVendasModelo3 frm = new FrmRelatPedidoVendasModelo3())
            {
                PEDIDOP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void ImprimirReceitaReportEconomico()
        {
            using (FrmRelatPedidoVendas2 frm = new FrmRelatPedidoVendas2())
            {
                PEDIDOP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void exportarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count < 2)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                Entity = null;
                Entity2 = null;
                ExportaPedidos();
            }
        }

        private void ExportaPedidos()
        {
            try
            {
                if (LIS_PEDIDOColl.Count > 1)
                {

                    DialogResult dr = MessageBox.Show("Deseja Exportar os Pedidos?",
                                   ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        System.IO.StreamWriter sw = null;
                        //Caractere delimitador
                        //string delimitador = "\t"; //tab
                        string delimitador = ";";

                        //Escolher onde salvar o arquivo
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = "PedidosExportados.csv";
                        //sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        sfd.InitialDirectory = BmsSoftware.ConfigSistema1.Default.PathInstall;
                        sfd.Filter = "arquivos csv (*. csv)|*.csv";

                        //Se usuário escolher nome corretamente e clicar em salvar
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                //Pega o caminho do arquivo
                                string caminho = sfd.FileName;
                                //Cria um StreamWriter no local
                                sw = new System.IO.StreamWriter(caminho, false, System.Text.Encoding.GetEncoding(1252));

                                int qtdColunas = DataGriewDados.Columns.Count;
                                string NomeVendedor = string.Empty;
                                foreach (var item in LIS_PEDIDOColl)
                                {
                                    //Dados do Pedido
                                    if (item.IDPEDIDO != null)
                                    {
                                        string NumPedido = Convert.ToInt32(item.IDPEDIDO).ToString().PadLeft(6, '0');
                                        string DataPedido = Convert.ToDateTime(item.DTEMISSAO).ToString("dd/MM/yyyy");
                                        string IDCliente = Convert.ToInt32(item.IDCLIENTE).ToString().PadLeft(6, '0');

                                        //Dados do Vendedor
                                        string CPFVENDEDOR = "00000000000";
                                        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                                        FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                                        FUNCIONARIOTy = FUNCIONARIOP.Read(Convert.ToInt32(item.IDVENDEDOR));
                                        if (FUNCIONARIOTy != null)
                                        {
                                            CPFVENDEDOR = FUNCIONARIOTy.CPF;
                                            NomeVendedor = FUNCIONARIOTy.NOME;
                                            if (!ValidacoesLibrary.ValidaCPF(CPFVENDEDOR))
                                            {
                                                MessageBox.Show("Erro: CPF do Vendedor: " + FUNCIONARIOTy.NOME + " inválido!");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erro: Vendedor não selecionado no pedido: " + NumPedido);
                                        }


                                        string IDFORMAPAGAMENTO = "000000";
                                        if (item.IDFORMAPAGAMENTO != null)
                                            IDFORMAPAGAMENTO = Convert.ToInt32(item.IDFORMAPAGAMENTO).ToString().PadLeft(6, '0');

                                        string IDLOCALCOBRANCA = "000000";
                                        if (item.IDLOCALCOBRANCA != null)
                                            IDLOCALCOBRANCA = Convert.ToInt32(item.IDLOCALCOBRANCA).ToString().PadLeft(6, '0');

                                        string IDSTATUS = Convert.ToInt32(item.IDSTATUS).ToString().PadLeft(6, '0');
                                        string OBSERVACAO = item.OBSERVACAO.Trim();
                                        OBSERVACAO = Regex.Replace(OBSERVACAO, @"\t|\n|\r", "");  //remove espaços                                     

                                        //grava dados do Pedido
                                        sw.WriteLine("P" + ";" + NumPedido + ";" + DataPedido + ";" + IDCliente + ";" + CPFVENDEDOR + ";" + IDFORMAPAGAMENTO + ";" +
                                                                 IDLOCALCOBRANCA + ";" + IDSTATUS + ";" + OBSERVACAO);

                                        //Dados do Produto
                                        RowRelatorio.Clear();
                                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", item.IDPEDIDO.ToString()));
                                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTO");

                                        if (LIS_PRODUTOSPEDIDOColl.Count > 0)
                                        {
                                            string QuantItens = LIS_PRODUTOSPEDIDOColl.Count.ToString().PadLeft(6, '0');
                                            foreach (var item_Produto in LIS_PRODUTOSPEDIDOColl)
                                            {
                                                string CodProduto = Convert.ToInt32(item_Produto.IDPRODUTO).ToString().PadLeft(6, '0');
                                                string QuantProduto = Convert.ToDecimal(item_Produto.QUANTIDADE).ToString("n2").PadLeft(10, '0');
                                                string PrecoUnitario = Convert.ToDecimal(item_Produto.VALORUNITARIO).ToString("n2").PadLeft(10, '0');

                                                //Grava Dados do produto do pedido
                                                sw.WriteLine(QuantItens + ";" + NumPedido + ";" + CodProduto + ";" + QuantProduto + ";" + PrecoUnitario);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erro: Não Exitem Produtos no Pedido: " + Convert.ToInt32(item.IDPEDIDO).ToString().PadLeft(6, '0'));
                                        }

                                    }
                                }

                                //Mensagem de confirmação
                                MessageBox.Show("Pedidos Exportado com Sucesso", "Exportado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                sw.Close();

                                using (FrmEnviarEmail frm = new FrmEnviarEmail())
                                {
                                    frm.NomeVendedor = NomeVendedor;
                                    frm.ArquivoAnexo = caminho;
                                    frm.EmailSelecionado = EMPRESATy.EMAIL;
                                    frm.EmailLote = false;
                                    frm.ShowDialog();
                                }

                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                //Fechar stream SEMPRE
                                sw.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Não existe pesquisa selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Exportar Pedido!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void importarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja Importar os Pedidos?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    ImportarPedidos();
                }
        }

        private void ImportarPedidos()
        {
            StreamReader rd = null;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 


            try
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = BmsSoftware.ConfigSistema1.Default.PathInstall;
                openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.csv)|*.csv";
                openFileDialog1.FilterIndex = 2 ;
                openFileDialog1.RestoreDirectory = true ;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                    rd = new StreamReader(openFileDialog1.OpenFile());
                    //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                    string linha = null;
                    //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                    string[] linhaseparada = null;
                    //realizo o while para ler o conteudo da linha 

                    int _IDPEDIDO = -1;
                    int ContadorPedidos = 0;
                    decimal? TotalGeral = 0;
                    PEDIDOEntity PEDIDO_Import = new PEDIDOEntity();
                    while ((linha = rd.ReadLine()) != null)
                    {
                        //com o split adiciono a string 'quebrada' dentro do array 
                        linhaseparada = linha.Split(';');
                        //aqui incluo o método necessário para continuar o trabalho 

                        if (linhaseparada[0] == "P") //Salva Dados do Pedido
                        {
                            //Salva o Pedido
                            
                            PEDIDO_Import.IDPEDIDO = -1;
                            PEDIDO_Import.NREFERENCIA = linhaseparada[1];
                            PEDIDO_Import.DTEMISSAO = Convert.ToDateTime(linhaseparada[2].ToString());
                            PEDIDO_Import.IDCLIENTE = Convert.ToInt32(linhaseparada[3]);
                            string CPFVENDEDOR = linhaseparada[4];

                            //Dados do Vendedor
                            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("CPF", "System.String", "=", CPFVENDEDOR));
                            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(RowRelatorio);
                            if (FUNCIONARIOColl.Count > 0)
                            {
                                PEDIDO_Import.IDVENDEDOR = FUNCIONARIOColl[0].IDFUNCIONARIO;
                            }
                            else
                            {
                                MessageBox.Show("Erro: Vendedor do CPF: " + CPFVENDEDOR + " Não localizado!");
                            }

                            if (linhaseparada[5].ToString() != "000000")
                                PEDIDO_Import.IDFORMAPAGAMENTO = Convert.ToInt32(linhaseparada[5].ToString());
                            else
                                PEDIDO_Import.IDFORMAPAGAMENTO = null;

                            if (linhaseparada[6].Trim().ToString() != "000000")
                                PEDIDO_Import.IDLOCALCOBRANCA = Convert.ToInt32(linhaseparada[6].ToString());
                            else
                                PEDIDO_Import.IDLOCALCOBRANCA = null;

                            PEDIDO_Import.IDSTATUS = Convert.ToInt32(linhaseparada[7].ToString());
                            PEDIDO_Import.OBSERVACAO = linhaseparada[8];

                            //Salva o Pedido
                            TotalGeral = 0;
                            PEDIDO_Import.TOTALPEDIDO = TotalGeral;
                            PEDIDO_Import.TOTALPRODUTOS = TotalGeral;
                            PEDIDO_Import.FLAGORCAMENTO = "N";
                            _IDPEDIDO = PEDIDOP.Save(PEDIDO_Import);

                            ContadorPedidos++;
                        }
                        else
                        {
                            decimal? TotalProduto = 0;

                            //Salva Dados do Produto
                            PRODUTOSPEDIDOEntity PRODUTOSPEDIDO_Import = new PRODUTOSPEDIDOEntity();
                            PRODUTOSPEDIDO_Import.IDPRODPEDIDO = -1;
                            PRODUTOSPEDIDO_Import.IDPEDIDO = _IDPEDIDO;
                            PRODUTOSPEDIDO_Import.IDPRODUTO = Convert.ToInt32(linhaseparada[2]);
                            PRODUTOSPEDIDO_Import.QUANTIDADE = Convert.ToDecimal(linhaseparada[3]);
                            PRODUTOSPEDIDO_Import.VALORUNITARIO = Convert.ToDecimal(linhaseparada[4]);
                            TotalProduto = Convert.ToDecimal(PRODUTOSPEDIDO_Import.QUANTIDADE * PRODUTOSPEDIDO_Import.VALORUNITARIO);
                            TotalGeral += TotalProduto;
                            PRODUTOSPEDIDO_Import.VALORTOTAL = TotalProduto;
                            PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDO_Import);

                            //Salva Total de Pedido                           
                            PEDIDO_Import = PEDIDOP.Read(_IDPEDIDO);
                            PEDIDO_Import.TOTALPEDIDO = TotalGeral;
                            PEDIDO_Import.TOTALPRODUTOS = TotalGeral;
                            PEDIDOP.Save(PEDIDO_Import);
                        }
                    }

                    rd.Close();
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Total de Pedidos Adicionados: " + ContadorPedidos.ToString());
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                rd.Close();
                MessageBox.Show("Erro ao Importar Pedido!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void utilitáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mkdDataVecto_Validated(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataVecto.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkdDataVecto, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void mkdDataVecto_Enter(object sender, EventArgs e)
        {
            try
            {
                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                if (msktDataEmissao.Text != "  /  /")
                {
                    DateTime DataEmissao = Convert.ToDateTime(msktDataEmissao.Text);
                    DataEmissao = DataEmissao.AddDays(Convert.ToDouble(CONFISISTEMATy.PRAZOOS));
                    mkdDataVecto.Text = DataEmissao.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: "+ ex.Message);
            }

        }

        private void carnêDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void carnêToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void duplicatasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void extratosDeContasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExtratoDuplReceber Frm = new FrmExtratoDuplReceber();
            Frm.CodClienteSelec = Convert.ToInt32(cbCliente.SelectedValue);
            Frm.ShowDialog();
        }

        private void totalDeProdutosPorPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTotalVendaporProduto frm = new FrmTotalVendaporProduto())
            {
                frm.ShowDialog();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

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
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEVENDEDOR", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NREFERENCIA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                if(txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDO DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_PEDIDOColl;

                    lblTotalPesquisa.Text = LIS_PEDIDOColl.Count.ToString();

                    if (LIS_PEDIDOColl.Count > 0)
                        PaintGrid();
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

        private void comissãoPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmComissaoVendedor3 frm = new FrmComissaoVendedor3())
            {
                frm.ShowDialog();
            }
        }

        private void modelo3ToolStripMenuItem1_Click_1(object sender, EventArgs e)
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
                    ImprimirModelo3();
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

        private void controleDeLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoVenda.SelectTab(1);
            }
            else if (LIS_PRODUTOSPEDIDOColl.Count == 0)
            {
                string Msgerro = "Não Existe Produtos Adicionados!";
                Util.ExibirMSg(Msgerro, "Red");
                errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);
                tabControlPedidoVenda.SelectTab(0);
            }
            else
            {
                using (FrmPedidoLote frm = new FrmPedidoLote())
                {
                    frm._IDPEDIDO = _IDPEDIDO;
                    frm.LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOColl;
                    frm.ShowDialog();
                    ListaProdutoPedido(_IDPEDIDO);
                }
            }
        }

        private void sincronizaIMEXAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmPedidoIMEXApp frm = new FrmPedidoIMEXApp())
            {
                frm.ShowDialog();
            }
        }
    }

}