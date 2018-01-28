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
using System.Diagnostics;
using BmsSoftware.Modulos.Relatorio;
using VVX;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPedidoVendaMarc : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        PRODUTOSCollection PRODUTOS2Coll = new PRODUTOSCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LIS_PEDIDOMARCCollection LIS_PEDIDOMARCColl = new LIS_PEDIDOMARCCollection();
        LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        MATERIALCollection MATERIALColl = new MATERIALCollection();
        LIS_MATERIALPEDIDOCollection LIS_MATERIALPEDIDOColl = new LIS_MATERIALPEDIDOCollection();
        LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();


        PEDIDOMARCProvider PEDIDOMARCP = new PEDIDOMARCProvider();
        LIS_PEDIDOMARCProvider LIS_PEDIDOMARCP = new LIS_PEDIDOMARCProvider();
        PRODUTOSPEDMARCProvider PRODUTOSPEDMARCP = new PRODUTOSPEDMARCProvider();
        LIS_PRODUTOSPEDMARCProvider LIS_PRODUTOSPEDMARCP = new LIS_PRODUTOSPEDMARCProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        MATERIALProvider MATERIALP = new MATERIALProvider();
        LIS_MATERIALPEDIDOProvider LIS_MATERIALPEDIDOP = new LIS_MATERIALPEDIDOProvider();
        MATERIALPEDIDOProvider MATERIALPEDIDOP = new MATERIALPEDIDOProvider();
        MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();
        ESTADOProvider ESTADOP = new ESTADOProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        PRODUTOPEDMARC2Provider PRODUTOPEDMARC2P = new PRODUTOPEDMARC2Provider();
        LIS_PRODUTOPEDMARC2Provider LIS_PRODUTOPEDMARC2P = new LIS_PRODUTOPEDMARC2Provider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmPedidoVendaMarc()
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

        int _IDPEDIDOMARC = -1;
        public PEDIDOMARCEntity Entity
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

                decimal TOTALMADEIRAS = Convert.ToDecimal(txtTotMadeira.Text);

                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";

                return new PEDIDOMARCEntity(_IDPEDIDOMARC, IDCLIENTE, DTEMISSAO, IDSTATUS, PRAZOENTREGA,
                                        IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO, OBSERVACAO,
                                        TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                        VALORDESCONTO, PORACRESCIMO, VALORACRESCIMO,
                                        TOTALPEDIDO, IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR,
                                        IDLOCALCOBRANCA, IDCENTROCUSTOS,
                                        TOTALMADEIRAS, FLAGORCAMENTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDOMARC = value.IDPEDIDOMARC;
                    txtNPedido.Text = _IDPEDIDOMARC.ToString().PadLeft(6, '0');
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

                    txtTotMadeira.Text = Convert.ToDecimal(value.TOTALMATERIAL).ToString("n2");

                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S"? true : false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPEDIDOMARC = -1;
                    txtNPedido.Text = string.Empty;

                    //Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);
                    ListaProdutoPedido(-1);
                    ListaMaterialPedido(-1);
                    ListaProdutos2(-1);

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
                    txtTotMadeira.Text = "0,00";

                    cbFormaPagto.SelectedIndex = 0;

                    //Preenche Mensagem Salvo na configuração do Sistema
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    txtObservacao.Text = CONFISISTEMAP.Read(1).MSGPEDIDO;

                    cbLocalCobranca.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;

                    rdOrcamento.Checked = false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    
                    errorProvider1.Clear();
                }
            }
        }

        public int _IDPRODUTOSPEDMARC = -1;
        public PRODUTOSPEDMARCEntity Entity2
        {
            get
            {
              int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
              decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
              decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
              decimal VALORTOTAL =  VALORUNITARIO * QUANTIDADE;
              decimal COMISSAO = 0;

              //"S" para comissão sobre o total o pedido
              //"N" para comissão pelo total dos produto
              if (FLAGCOMISSAO == "N")
              {
                  decimal PorcComissao = Convert.ToDecimal(PRODUTOSP.Read(IDPRODUTO).COMISSAO);
                  COMISSAO = (VALORTOTAL * PorcComissao) / 100;
              }

              return new PRODUTOSPEDMARCEntity(_IDPRODUTOSPEDMARC, _IDPEDIDOMARC, IDPRODUTO,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOSPEDMARC = Convert.ToInt32(value.IDPRODUTOSPEDMARC);
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOSPEDMARC = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = string.Empty;
                    txtValorUnitProd.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }

        int _IDMATERIALPEDIDO = -1;
        public MATERIALPEDIDOEntity Entity5
        {
            get
            {
                int IDMADEIRA = Convert.ToInt32(cbMaterial.SelectedValue);

                if (txtM3.Text == string.Empty)
                    txtM3.Text = "0,00";
                decimal QUANTIDADE = Convert.ToDecimal(txtM3.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValoUnitMad.Text);
                decimal VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                decimal COMISSAO = 0;

                //"S" para comissão sobre o total o pedido
                //"N" para comissão pelo total dos produto
                if (FLAGCOMISSAO == "N")
                {
                    decimal PorcComissao = Convert.ToDecimal(MATERIALP.Read(IDMADEIRA).COMISSAO);
                    COMISSAO = (VALORTOTAL * PorcComissao) / 100;
                }

                if (txtAltura.Text == string.Empty)
                    txtAltura.Text = "0,00";
                decimal ALTURA  = Convert.ToDecimal(txtAltura.Text);

                if (txtLargura.Text == string.Empty)
                    txtLargura.Text = "0,00";
                decimal LARGURA = Convert.ToDecimal(txtLargura.Text);

                if (txtComprimento.Text == string.Empty)
                    txtComprimento.Text = "0,00";
                decimal COMPRIMENTO = Convert.ToDecimal(txtComprimento.Text);

                return new MATERIALPEDIDOEntity(_IDMATERIALPEDIDO, _IDPEDIDOMARC, IDMADEIRA,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO,
                                                ALTURA, LARGURA, COMPRIMENTO);
            }
            set
            {

                if (value != null)
                {
                    _IDMATERIALPEDIDO = value.IDMATERIALPEDIDO;
                    cbMaterial.SelectedValue = value.IDMATERIAL;
                    txtM3.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n5");
                    txtValoUnitMad.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");

                    txtAltura.Text = Convert.ToDecimal(value.ALTURA).ToString("n5");
                    txtLargura.Text = Convert.ToDecimal(value.LARGURA).ToString("n5");
                    txtComprimento.Text = Convert.ToDecimal(value.COMPRIMENTO).ToString("n5");

                    errorProvider1.Clear();
                }
                else
                {
                    _IDMATERIALPEDIDO = -1;
                    cbMaterial.SelectedIndex = 0;
                    txtM3.Text = string.Empty;
                    txtValoUnitMad.Text = "0,00";
                    txtAltura.Text = "0,00";
                    txtLargura.Text = "0,00";
                    txtComprimento.Text = "0,00";
                    txtM3.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODUTOPEDMARC2 = -1;
        public PRODUTOPEDMARC2Entity Entity3
        {
            get
            {
               decimal  QUANT = Convert.ToDecimal(txtQuantMTQ_2.Text);
               decimal  ALTURA= Convert.ToDecimal(txtAltura2.Text);
               decimal  LARGURA= Convert.ToDecimal(txtLargura2.Text);
               decimal  COMPRIMENTO= Convert.ToDecimal(txtComprimento.Text);
               decimal  TOTALMT3= Convert.ToDecimal(txtMT3_2.Text);
               decimal VLUNITARIO = Convert.ToDecimal(txtvalorunitMTQ_2.Text);
               decimal VLTOTAL = Convert.ToDecimal(txtVlTotalMTQ_2.Text);
               string    DADOSADICIONAIS= txtDadosAdicionais2.Text;
               int IDPRODUTO = Convert.ToInt32(cbProdutoMTQ.SelectedValue); 

                return new PRODUTOPEDMARC2Entity(_IDPRODUTOPEDMARC2, _IDPEDIDOMARC, QUANT,  ALTURA,  LARGURA,  COMPRIMENTO,
                                                 TOTALMT3,  VLUNITARIO,  VLTOTAL,  DADOSADICIONAIS,   IDPRODUTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOPEDMARC2 = value.IDPRODUTOPEDMARC2;
                    txtQuantMTQ_2.Text = Convert.ToDecimal(value.QUANT).ToString("n2");
                    txtAltura2.Text = Convert.ToDecimal(value.ALTURA).ToString("n3");
                    txtLargura2.Text = Convert.ToDecimal(value.LARGURA).ToString("n3");
                    txtComprimento.Text = Convert.ToDecimal(value.COMPRIMENTO).ToString("n3");
                    txtMT3_2.Text = Convert.ToDecimal(value.TOTALMT3).ToString("n3");
                    txtvalorunitMTQ_2.Text = Convert.ToDecimal(value.VLUNITARIO).ToString("n3");
                    txtVlTotalMTQ_2.Text = Convert.ToDecimal(value.VLTOTAL).ToString("n2");
                    txtDadosAdicionais2.Text = value.DADOSADICIONAIS;
                    cbProdutoMTQ.SelectedValue = value.IDPRODUTO;


                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOPEDMARC2 = -1;
                    txtQuantMTQ_2.Text ="1,00";
                    txtAltura2.Text = "0,000";
                    txtLargura2.Text = "0,000";
                    txtComprimento.Text = "0,000";
                    txtMT3_2.Text ="0,000";
                    txtvalorunitMTQ_2.Text = "0,000";
                    txtVlTotalMTQ_2.Text ="0,00";
                    txtDadosAdicionais2.Text = string.Empty;
                    cbProdutoMTQ.SelectedValue =-1;

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
            GetDropProdutos2();
            GetTransporte();
            GetFuncionario();
            GetDropCentroCusto();
            GetDropFormaPgto();
            GetDropLocalCobranca();
            GetDropTipoDuplicata();
            GetDropMaterial();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnCadProdMTQ.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            bntCadMovim.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadMadeira.Image = Util.GetAddressImage(6);

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            PreencheDropTipoMovimento();
            PreencheDropCodMovimento();

            GetConfiSistema();
            VerificaAcesso();

            if (_IDPRODUTOSPEDMARC != -1)
            { 
                Entity = Entity = PEDIDOMARCP.Read(_IDPRODUTOSPEDMARC);
                ListaProdutos2(_IDPEDIDOMARC);
                ListaProdutoPedido(_IDPEDIDOMARC);
                ListaMaterialPedido(_IDPEDIDOMARC);
            }

            this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void PreencheDropCodMovimento()
        {
            CODMOVESTOQUEProvider CODMOVESTOQUEPP = new CODMOVESTOQUEProvider();

            cbCodMov.DataSource = CODMOVESTOQUEPP.ReadCollectionByParameter(null, "NOME");
            cbCodMov.DisplayMember = "CODIGO";
            cbCodMov.ValueMember = "IDCODMOVESTOQUE";
        }

        private void GetConfiSistema()
        {
            CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMATy = CONFISISTEMAP.Read(1);
            FLAGPEDBAIXAESTOQUE = CONFISISTEMATy.FLAGPEDBAIXAESTOQUE;
            
            //"S" para comissão sobre o total o pedido
            //"N" para comissão pelo total dos produto
            FLAGCOMISSAO = CONFISISTEMATy.FLAGCOMISSAO;
        }
       

        private void PreencheDropTipoMovimento()
        {
            TIPOMOVESTOQUEProvider TIPOMOVESTOQUEP = new TIPOMOVESTOQUEProvider();

            cbTipoMov.DataSource = TIPOMOVESTOQUEP.ReadCollectionByParameter(null, "NOME");
            cbTipoMov.DisplayMember = "NOME";
            cbTipoMov.ValueMember = "IDTIPOMOVESTOQUE";

            cbTipoMov.SelectedValue = 2;//Saida
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
        }

        private void GetDropCliente()
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

        private void GetDropStatus()
        {
            //14 Pedido de Venda Marcenaria
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "14");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

        private void GetDropProdutos()
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

        private void GetDropProdutos2()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOS2Coll = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProdutoMTQ.DisplayMember = "NOMEPRODUTO";
            cbProdutoMTQ.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOS2Coll.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProdutoMTQ.DisplayMember);

            PRODUTOS2Coll.Sort(comparer.Comparer);
            cbProdutoMTQ.DataSource = PRODUTOSColl;

            cbProdutoMTQ.SelectedIndex = 0;
        }

        private void GetTransporte()
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

        private void GetFuncionario()
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
        }

        private void GetDropFormaPgto()
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
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
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
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
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

            if(LIS_DUPLICATARECEBERColl.Count > 0)
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
                    if (_IDPEDIDOMARC == -1)
                        _IDPEDIDOMARC = PEDIDOMARCP.Save(Entity);
                    else
                    {
                        _IDPEDIDOMARC = PEDIDOMARCP.Save(Entity);
                        btnPesquisa_Click(null, null);
                    }

                    txtNPedido.Text = _IDPEDIDOMARC.ToString().PadLeft(6, '0');

                   
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

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

                LIS_PEDIDOMARCColl = LIS_PEDIDOMARCP.ReadCollectionByParameter(Filtro, "IDPEDIDOMARC DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOMARCEntity LIS_PEDIDOMARCTy = new LIS_PEDIDOMARCEntity();
                LIS_PEDIDOMARCTy.TOTALMATERIAL = SumTotalPesquisa("TOTALMATERIAL");
                LIS_PEDIDOMARCTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOMARCTy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOMARCTy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");

                LIS_PEDIDOMARCColl.Add(LIS_PEDIDOMARCTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOMARCColl;

                lblTotalPesquisa.Text = LIS_PEDIDOMARCColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOMARCEntity item in LIS_PEDIDOMARCColl)
            {
                if (NomeCampo == "TOTALMATERIAL")
                    valortotal += Convert.ToDecimal(item.TOTALMATERIAL);
                else if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "TOTALPRODUTOS")
                    valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
                else if (NomeCampo == "VALORCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VALORCOMISSAO);               
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
            if (LIS_PEDIDOMARCColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOMARCColl;
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

                LIS_PEDIDOMARCColl = LIS_PEDIDOMARCP.ReadCollectionByParameter(Filtro, "IDPEDIDOMARC DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOMARCColl;

                lblTotalPesquisa.Text = LIS_PEDIDOMARCColl.Count.ToString();
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
            LIS_PEDIDOMARCColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PEDIDOMARCColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_PEDIDOMARCColl.Count > 1)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_PEDIDOMARCColl[rowindex].IDPEDIDOMARC);
                    tabControlPedidoVenda.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = PEDIDOMARCP.Read(CodigoSelect);

                    ListaProdutos2(_IDPEDIDOMARC);
                    ListaProdutoPedido(_IDPEDIDOMARC);
                    ListaMaterialPedido(_IDPEDIDOMARC);
                  
                    txtNPedido.Focus();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                 
                }
            }
        }


        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            Entity3 = null;          
            
            tabControlPedidoVenda.SelectTab(0);
            tabControl1.SelectTab(0);
        }       

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_PEDIDOMARCColl.Count > 1)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PEDIDOMARCColl[indice].IDPEDIDOMARC);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlPedidoVenda.SelectTab(0);
                    Entity = PEDIDOMARCP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDOMARC);
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
                            PEDIDOMARCP.Delete(CodigoSelect);
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
            Entity3 = null;          
           
            tabControlPedidoVenda.SelectTab(0);
            tabControl1.SelectTab(0);
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show("Antes de adicionar os produtos é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
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
                    PRODUTOSPEDMARCP.Save(Entity2);
                    ListaProdutoPedido(_IDPEDIDOMARC);                   

                    //Salva Pedido
                    PEDIDOMARCP.Save(Entity);
                  
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
            else
                errorProvider1.Clear();


            return result;
        }

        private void ListaProdutoPedido(int IDPEDIDOMARC)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDOMARC.ToString()));
                LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDMARCColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDMARCColl.Count.ToString();

                SumTotalProdutosPedido();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico " + ex.Message);
            }              
        }      

        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            foreach (LIS_PRODUTOPEDMARC2Entity item in LIS_PRODUTOPEDMARC2Coll)
            {
                total += Convert.ToDecimal(item.VLTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
            txtTotalProdAdicional.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void SumTotalPedido()
        {
            decimal TotalPedido = 0;
            TotalPedido = (Convert.ToDecimal(txtTotalProdAdicional.Text) + Convert.ToDecimal(txtTotalMadeira.Text) +
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
            if (LIS_MATERIALPEDIDOColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar o Pedido é necessário remover os materiais!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(dtGrMadeira, ConfigMessage.Default.CampoObrigatorio);

                tabControlPedidoVenda.SelectTab(1);
                tabControl1.SelectTab(1);
                result = false;
            }
            else if (LIS_PRODUTOSPEDMARCColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar o Pedido é necessário remover os Produtos!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);

                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(3);
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
                tabControl1.SelectTab(4);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }


        private void Delete()
        {
            if (_IDPEDIDOMARC == -1)
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
                        PEDIDOMARCP.Delete(_IDPEDIDOMARC);                       

                      
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

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
            if (LIS_PRODUTOSPEDMARCColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    //Movimento de Estoque
                    //Podera alterar somente se nao estiver controlando o estoque
                    if (FLAGPEDBAIXAESTOQUE == "N")
                    {
                        CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDMARCColl[rowindex].IDPRODUTOSPEDMARC);
                        Entity2 = PRODUTOSPEDMARCP.Read(CodSelect);
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
                            CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDMARCColl[rowindex].IDPRODUTOSPEDMARC);
                            decimal QUANTMOV =Convert.ToDecimal(LIS_PRODUTOSPEDMARCColl[rowindex].QUANTIDADE);
                            int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDMARCColl[rowindex].IDPRODUTO);
                            PRODUTOSPEDMARCP.Delete(CodSelect); 
                            
                            ListaProdutoPedido(_IDPEDIDOMARC);
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
            if (_IDPEDIDOMARC == -1)
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

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
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
                PRODUTOSPEDMARCP.Save(Entity2);                    
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDOMARC);            

            //Grava os dados do Pedido
            PEDIDOMARCP.Save(Entity);         
        
        }

        private void bntCadMovim_Click(object sender, EventArgs e)
        {

            using (FrmCodMovEstoque frm = new FrmCodMovEstoque())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCodMov.SelectedValue);
                PreencheDropCodMovimento();
                cbCodMov.SelectedValue = CodSelec;
            }
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                decimal ValorVenda =Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue)).VALORVENDA1); 
                txtValorUnitProd.Text = ValorVenda.ToString("n2");
            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
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


                // Valor = (Convert.ToDecimal(txtTotalOS.Text) * Convert.ToDecimal(item.PORCPAGTO)) / 100;
                Valor = Valor * Convert.ToDecimal(item.PORCPAGTO) / 100;

                //Calculando juros
                Valor = ((Valor * Convert.ToDecimal(item.PORCJUROS)) / 100) + Valor;

                DUPLICATARECEBERty.DATAPAGTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORPAGO = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = 0;
                DUPLICATARECEBERty.IDSTATUS = 3; //Pago
                DUPLICATARECEBERty.NOTAFISCAL = "PM" + txtNPedido.Text;

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
                DUPLICATARECEBERty.NOTAFISCAL = "PM" + txtNPedido.Text;

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
                catch (Exception)
                {

                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }               
            }
        }

        private void GridDuplicatasPD(int idcliente, string numero)
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
            RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PM"+numero));

            LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");
            dataGridDupl.AutoGenerateColumns = false;
            dataGridDupl.DataSource = LIS_DUPLICATARECEBERColl;
            SumFinanceiroPD();
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
                            CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                            DUPLICATARECEBERP.Delete(CodSelect);
                            GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            SumFinanceiroPD();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
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
            if (_IDPEDIDOMARC == -1)
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

            CaixaTy.NDOCUMENTO = "PM"+txtNPedido.Text;
            CaixaTy.OBSERVACAO = "Pedido de Venda nº " + "PM" + txtNPedido.Text + " Cliente: " + cbCliente.SelectedValue + " - " + GetNameCliente(Convert.ToInt32(cbCliente.SelectedValue));

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
            if (LIS_PEDIDOMARCColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(3);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Pedidos");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            try
            {
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument1;
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

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            laserToolStripMenuItem_Click(null, null);
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

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                int NumerorRegistros = LIS_PEDIDOMARCColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_PEDIDOMARCColl.Count)
                {
                    if (LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_PEDIDOMARCColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMEVENDEDOR, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_PEDIDOMARCColl[IndexRegistro].TOTALPEDIDO).ToString("n2");
                        e.Graphics.DrawString(TotalFOS, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);
                    }
                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_PEDIDOMARCColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + (LIS_PEDIDOMARCColl.Count - 1), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

        private decimal SomaTotal()
        {
            decimal result = 0;

            int i = 1;
            foreach (LIS_PEDIDOMARCEntity item in LIS_PEDIDOMARCColl)
            {
                if (i < LIS_PEDIDOMARCColl.Count)
                    result += Convert.ToDecimal(item.TOTALPEDIDO);

                i++;
            }
            return result;
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOMARCColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(3);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                //using (FrmRelatorioPPedMarc frm = new FrmRelatorioPPedMarc())
                //{
                //    frm.LIS_PEDIDOMARCCollRelatPers = LIS_PEDIDOMARCColl;
                //    frm.ShowDialog();
                //    btnPesquisa_Click(null, null);
                //}
            }       
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            cbTipoMov.SelectedValue = 2;
        }

        private void laserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirReport();
        }

        private void ImprimirReport()
        {
            using (FrmPedidoMT3 frm = new FrmPedidoMT3())
            {
                PEDIDOMARCP.Save(Entity);
                frm.LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCColl;
                frm.LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2Coll;
                frm.LIS_MATERIALPEDIDOColl = LIS_MATERIALPEDIDOColl;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDOMARC = _IDPEDIDOMARC;
                frm.ShowDialog();
            }

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
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", _IDPEDIDOMARC.ToString()));
                LIS_PEDIDOMARCCollection LIS_PEDIDOMARCCollPrint = new LIS_PEDIDOMARCCollection();
                LIS_PEDIDOMARCCollPrint = LIS_PEDIDOMARCP.ReadCollectionByParameter(RowRelatorio);

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
                e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


                e.Graphics.DrawString("Nº PEDIDO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDPEDIDOMARC.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

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
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.ENDERECO1 + " " + ClienteTy.NUMEROENDER, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
                e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

                e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);

                e.Graphics.DrawString(Util.LimiterText(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).MUNICIPIO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
                e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
                e.Graphics.DrawString(Util.LimiterText(ESTADOP.Read(Convert.ToInt32(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).COD_UF_IBGE)).UF, 2), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

                e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

                string CPFCNPJ = (ClienteTy.CNPJ == "  .   .   /    -" || ClienteTy.CNPJ == string.Empty) ? ClienteTy.CPF : ClienteTy.CNPJ;
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
                e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
                e.Graphics.DrawString(Util.LimiterText(ClienteTy.EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
                e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
                e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);

                //"Produtos/Acessórios
                int linha = 240;
                int linhaBorda = 235;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);
                linha = linha + 5;
                e.Graphics.DrawString("Produtos/Acessórios", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

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
                foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);

                    if (CONFISISTEMAP.Read(1).FLAGCODREFERENCIA.TrimEnd() == "N")
                        e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);
                    else
                    {
                        string CodReferencia = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO)).CODPRODUTOFORNECEDOR;
                        e.Graphics.DrawString(CodReferencia, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);
                    }

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }

                //Madeiras
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);
                linha = linha + 5;
                e.Graphics.DrawString("Madeiras", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                linha = linha + 15;
                e.Graphics.DrawString("MT3", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 5);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 690, 20);

                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha + 5);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 630, 20);

                e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha + 5);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 220, 20);

                e.Graphics.DrawString("Vlr.Unit.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, linha + 5);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 130, 20);

                e.Graphics.DrawString("Vlr.Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha + 5);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 20);

                //Madeiras;
                linha = linha + 25;
                linhaBorda = linhaBorda + 45;
                foreach (LIS_MATERIALPEDIDOEntity item in LIS_MATERIALPEDIDOColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDMATERIAL.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEMADEIRA, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                    //Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }



                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 80);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 125);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Situação Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].NOMESTATUS, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 135, linha);

                e.Graphics.DrawString("Madeiras:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotMadeira.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOMARCCollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;
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

                //Observação
                linha = linha + 10;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 120);
                linha = linha + 5;
                e.Graphics.DrawString("Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 450), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);


                //Final da linha
                linha = linha + 110;
                e.Graphics.DrawString("Assinatura do Cliente: _________________________________________________", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha +10 );
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

                int NumerorRegistros = LIS_PEDIDOMARCColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_PEDIDOMARCColl.Count)
                {
                    if (LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_PEDIDOMARCColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOMARCColl[IndexRegistro].NOMEVENDEDOR, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_PEDIDOMARCColl[IndexRegistro].TOTALPEDIDO).ToString("n2");
                        e.Graphics.DrawString(TotalFOS, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    //Listar os produtos
                    LIS_MATERIALPEDIDOCollection LIS_MATERIALPEDIDOPrintColl = new LIS_MATERIALPEDIDOCollection();
                    LIS_MATERIALPEDIDOPrintColl = MaterialRel(Convert.ToInt32(LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC));
                    e.Graphics.DrawString("Cod.Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Produtos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    foreach (LIS_MATERIALPEDIDOEntity item in LIS_MATERIALPEDIDOPrintColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Util.LimiterText(item.IDMATERIAL.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.NOMEMADEIRA, 25), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.QUANTIDADE.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                    }

                    //Listar os produtos
                    LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCPrintColl = new LIS_PRODUTOSPEDMARCCollection();
                    LIS_PRODUTOSPEDMARCPrintColl = ProdutoRel(Convert.ToInt32(LIS_PEDIDOMARCColl[IndexRegistro].IDPEDIDOMARC));
                    foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCPrintColl)
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
                if (IndexRegistro < LIS_PEDIDOMARCColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + (LIS_PEDIDOMARCColl.Count - 1), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

        private LIS_MATERIALPEDIDOCollection MATERIALRel(int IDPEDIDO)
        {
            LIS_MATERIALPEDIDOCollection LIS_MATERIALPEDIDOColl = new LIS_MATERIALPEDIDOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_MATERIALPEDIDOColl = LIS_MATERIALPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_MATERIALPEDIDOColl;
        }

        private LIS_PRODUTOSPEDMARCCollection ProdutoRel(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDMARCColl;
        }

        private LIS_MATERIALPEDIDOCollection MaterialRel(int IDPEDIDO)
        {
            LIS_MATERIALPEDIDOCollection LIS_MATERIALPEDIDOPPRODColl = new LIS_MATERIALPEDIDOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_MATERIALPEDIDOPPRODColl = LIS_MATERIALPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_MATERIALPEDIDOPPRODColl;
        }
      

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PEDIDOMARCColl.Count > 1)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOMARCEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOMARCEntity>(orderBy);

                LIS_PEDIDOMARCColl.Sort(comparer.Comparer);              
                 
                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_PEDIDOMARCColl;
               
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
                e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
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
                e.Graphics.DrawString(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(ESTADOP.Read(Convert.ToInt32(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).COD_UF_IBGE)).UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

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
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                e.Graphics.DrawString(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(ESTADOP.Read(Convert.ToInt32(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).COD_UF_IBGE)).UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

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
                e.Graphics.DrawString(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 785);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 785);
                e.Graphics.DrawString(ESTADOP.Read(Convert.ToInt32(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).COD_UF_IBGE)).UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 785);

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
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PM" + txtNPedido.Text));
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
                e.Graphics.DrawString("PM" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString("PM" + txtNPedido.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


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
                e.Graphics.DrawString(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(ESTADOP.Read(Convert.ToInt32(MUNICIPIOSP.Read(Convert.ToInt32(ClienteTy.COD_MUN_IBGE)).COD_UF_IBGE)).UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

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

        private void txtAltura_Enter(object sender, EventArgs e)
        {
            if (txtAltura.Text == "0,00")
                txtAltura.Text = string.Empty;

            lblObsField.Text = "Altura em cm";
        }

        private void txtLargura_Enter(object sender, EventArgs e)
        {
            if (txtLargura.Text == "0,00")
                txtLargura.Text = string.Empty;

            lblObsField.Text = "Largura em cm";
        }

        private void txtComprimento_Enter(object sender, EventArgs e)
        {
            if (txtComprimento.Text == "0,00")
                txtComprimento.Text = string.Empty;

            lblObsField.Text = "Comprimento em cm";
        }

        private void txtAltura_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAltura.Text == string.Empty)
                txtAltura.Text = "0,00";

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtAltura.Text))
            {
                errorProvider1.SetError(txtAltura, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtAltura.Text);
                txtAltura.Text = string.Format("{0:n5}", f);
                errorProvider1.SetError(txtAltura, "");

                txtM3.Text = (Convert.ToDecimal(txtAltura.Text) * Convert.ToDecimal(txtLargura.Text)
                           * Convert.ToDecimal(txtComprimento.Text)).ToString("n5");
            } 
        }

        private void txtLargura_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtLargura.Text == string.Empty)
                txtLargura.Text = "0,00";
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtLargura.Text))
            {
                errorProvider1.SetError(txtLargura, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtLargura.Text);
                txtLargura.Text = string.Format("{0:n5}", f);
                errorProvider1.SetError(txtLargura, "");

                txtM3.Text = (Convert.ToDecimal(txtAltura.Text) * Convert.ToDecimal(txtLargura.Text)
                          * Convert.ToDecimal(txtComprimento.Text)).ToString("n5");
            } 
        }

        private void txtComprimento_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtComprimento.Text == string.Empty)
                txtComprimento.Text = "0,00";
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtComprimento.Text))
            {
                errorProvider1.SetError(txtComprimento, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtComprimento.Text);
                txtComprimento.Text = string.Format("{0:n5}", f);
                errorProvider1.SetError(txtComprimento, "");

                txtM3.Text = (Convert.ToDecimal(txtAltura.Text) * Convert.ToDecimal(txtLargura.Text)
                          * Convert.ToDecimal(txtComprimento.Text)).ToString("n5");
            } 
        }

        private void btnCadMadeira_Click(object sender, EventArgs e)
        {
            using (FrmMaterial frm = new FrmMaterial())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbMaterial.SelectedValue);
                GetDropMaterial();
                cbMaterial.SelectedValue = CodSelec;
            }
        }

        private void GetDropMaterial()
        {
            MATERIALProvider MATERIALP = new MATERIALProvider();
            MATERIALColl = MATERIALP.ReadCollectionByParameter(null, "NOMEMATERIAL");

            cbMaterial.DisplayMember = "NOMEMATERIAL";
            cbMaterial.ValueMember = "IDMATERIAL";

            MATERIALEntity MATERIALTy = new MATERIALEntity();
            MATERIALTy.NOMEMATERIAL = ConfigMessage.Default.MsgDrop;
            MATERIALTy.IDMATERIAL = -1;
            MATERIALColl.Add(MATERIALTy);

            Phydeaux.Utilities.DynamicComparer<MATERIALEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MATERIALEntity>(cbMaterial.DisplayMember);

            MATERIALColl.Sort(comparer.Comparer);
            cbMaterial.DataSource = MATERIALColl;

            cbMaterial.SelectedIndex = 0;
        }

        private void cbMadeira_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaterial.SelectedIndex > 0)
            {
                decimal ValorVenda = Convert.ToDecimal(MATERIALP.Read(Convert.ToInt32(cbMaterial.SelectedValue)).VALORVENDA1);
                txtValoUnitMad.Text = ValorVenda.ToString("n2");
            }
        }

        private void txtValoUnitMad_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValoUnitMad.Text))
            {
                errorProvider1.SetError(txtComprimento, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtValoUnitMad.Text);
                txtValoUnitMad.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValoUnitMad, "");
            } 
        }

        private void cbMadeira_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchMaterial frm = new FrmSearchMaterial())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                       cbMaterial.SelectedValue = result;
                }
            }
        }

        private void cbMadeira_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione a Madeira ou pressione Ctrl+E para pesquisar.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show("Antes de adicionar a madeira é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                GravaMadeira();
               
            }
        }

        private void GravaMadeira()
        {
            try
            {
                if (ValidacoesMadeira())
                {
                   MATERIALPEDIDOP.Save(Entity5);
                   ListaMaterialPedido(_IDPEDIDOMARC);

                    //Salva Pedido
                    PEDIDOMARCP.Save(Entity);

                    Entity5 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbMaterial.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean ValidacoesMadeira()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbMaterial.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbMaterial, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValoUnitMad.Text))
            {
                errorProvider1.SetError(txtValoUnitMad, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
           
            else
                errorProvider1.Clear();


            return result;
        }

        private void ListaMaterialPedido(int IDPEDIDOMARC)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC", "System.Int32", "=", IDPEDIDOMARC.ToString()));
                LIS_MATERIALPEDIDOColl = LIS_MATERIALPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                dtGrMadeira.AutoGenerateColumns = false;
                dtGrMadeira.DataSource = LIS_MATERIALPEDIDOColl;

                SumTotalMaterialedido();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalMaterialedido()
        {
            decimal total = 0;
            foreach (LIS_MATERIALPEDIDOEntity item in LIS_MATERIALPEDIDOColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalMadeira.Text = total.ToString("n2");
            txtTotMadeira.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void dtGrMadeira_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_MATERIALPEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_MATERIALPEDIDOColl[rowindex].IDMATERIALPEDIDO);
                    Entity5 = MATERIALPEDIDOP.Read(CodSelect);
                   
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_MATERIALPEDIDOColl[rowindex].IDMATERIALPEDIDO);
                            MATERIALPEDIDOP.Delete(CodSelect);

                           ListaMaterialPedido(_IDPEDIDOMARC);
                            Entity5 = null;

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

        private void txtM3_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtM3.Text == string.Empty)
                txtM3.Text = "0,00";
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtM3.Text))
            {
                errorProvider1.SetError(txtM3, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtM3.Text);
                txtM3.Text = string.Format("{0:n5}", f);
                errorProvider1.SetError(txtM3, "");               
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Entity5 = null;
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                PrintTicket();
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

                ticket.AddSubHeaderLine("Pedido N." + Entity.IDPEDIDOMARC.ToString().PadLeft(6, '0'));
                ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                ticket.AddSubHeaderLine("VENDEDOR: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                foreach (LIS_MATERIALPEDIDOEntity item in LIS_MATERIALPEDIDOColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEMADEIRA, Util.LimiterText(ValorTotal, 20));
                }

                foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalProdAdicional.Text) + Convert.ToDecimal(txtTotMadeira.Text);
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

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
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

                                    //Salva a forma de pagamento no Pedido
                                    PEDIDOMARCP.Save(Entity);

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
                            PEDIDOMARCP.Save(Entity);

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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
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
                    frm.NotaFiscal = "PM" + txtNPedido.Text;
                    frm.ShowDialog();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
            }
        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOMARCColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(3);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeralProduto();
            }
        }

        string DataMovimentacaoInicial = string.Empty;
        string DataMovimentacaoFinal = string.Empty;
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

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));

                LIS_PEDIDOMARCColl.Clear();
                LIS_PEDIDOMARCColl = LIS_PEDIDOMARCP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDOMARC DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOMARCEntity LIS_PEDIDOMARCTy = new LIS_PEDIDOMARCEntity();
                LIS_PEDIDOMARCTy.TOTALMATERIAL = SumTotalPesquisa("TOTALMATERIAL");
                LIS_PEDIDOMARCTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOMARCTy.TOTALPRODUTOS = SumTotalPesquisa("TOTALPRODUTOS");
                LIS_PEDIDOMARCTy.VALORCOMISSAO = SumTotalPesquisa("VALORCOMISSAO");

                LIS_PEDIDOMARCColl.Add(LIS_PEDIDOMARCTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOMARCColl;

                if (LIS_PEDIDOMARCColl.Count > 1)
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnCadProdMTQ_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProdutoMTQ.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutos2();
                cbProdutoMTQ.SelectedValue = CodSelec;
            }
        }

        private void txtQuantMTQ_2_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaProdutoMT3_2();
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtAltura2_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaProdutoMT3_2();
                }
            }
            else
                TextBoxSelec.Text = "0,000";
        }

        private void SomaProdutoMT3_2()
        {
            try
            {
                txtMT3_2.Text = (Convert.ToDecimal(txtAltura2.Text) * Convert.ToDecimal(txtLargura2.Text)
                                 * Convert.ToDecimal(txtComprimento2.Text)).ToString("n3");

                //Soma Valor Total                
                txtVlTotalMTQ_2.Text = ((Convert.ToDecimal(txtMT3_2.Text) * Convert.ToDecimal(txtQuantMTQ_2.Text)) * Convert.ToDecimal(txtvalorunitMTQ_2.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void txtLargura2_Validated(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaProdutoMT3_2();
                }
            }
            else
                TextBoxSelec.Text = "0,000";
        }

        private void txtComprimento2_TextChanged(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaProdutoMT3_2();
                }
            }
            else
                TextBoxSelec.Text = "0,000";
        }

        private void txtVlUnitMT3_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,000";
        }

        private void txtvalorunitMTQ_2_Validated(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    SomaProdutoMT3_2();
                }
            }
            else
                TextBoxSelec.Text = "0,000";
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
                        txtvalorunitMTQ_2.Text = frm.ResultPreco.ToString("n2");
                    }

                }
            }
        }

        private void cbProdutoMTQ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void cbProdutoMTQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProdutoMTQ.SelectedIndex > 0)
            {
                decimal ValorVenda = Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue)).VALORVENDA1);
                txtvalorunitMTQ_2.Text = ValorVenda.ToString("n2");
            }
        }

        private void btnAdicionaMTQ_2_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show("Antes de adicionar o produto é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                GravaProduto2();

            }
        }

        private void GravaProduto2()
        {
            try
            {
                if (ValidacoesProduto2())
                {
                    PRODUTOPEDMARC2P.Save(Entity3);
                    ListaProdutos2(_IDPEDIDOMARC);

                    //Salva Pedido
                    PEDIDOMARCP.Save(Entity);

                    Entity3 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProdutoMTQ.Focus();
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void ListaProdutos2(int IDPEDIDOMARC)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("PEDIDOMARC", "System.Int32", "=", IDPEDIDOMARC.ToString()));
                LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2P.ReadCollectionByParameter(RowRelatorio);

                dtgProdMTQ.AutoGenerateColumns = false;
                dtgProdMTQ.DataSource = LIS_PRODUTOPEDMARC2Coll;

                SumTotalProdutosPedido();
            }
             catch (Exception EX)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }

        }

        private Boolean ValidacoesProduto2()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProdutoMTQ.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProdutoMTQ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtvalorunitMTQ_2.Text))
            {
                errorProvider1.SetError(txtvalorunitMTQ_2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantMTQ_2.Text))
            {
                errorProvider1.SetError(txtQuantMTQ_2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtAltura2.Text))
            {
                errorProvider1.SetError(txtAltura2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtComprimento2.Text))
            {
                errorProvider1.SetError(txtComprimento2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtvalorunitMTQ_2.Text))
            {
                errorProvider1.SetError(txtvalorunitMTQ_2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void btnCancelMTQ_2_Click(object sender, EventArgs e)
        {
            Entity3 = null;
        }

        private void dtgProdMTQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOPEDMARC2Coll.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                
                    CodSelect = Convert.ToInt32(LIS_PRODUTOPEDMARC2Coll[rowindex].IDPRODUTOPEDMARC2);
                    Entity3 = PRODUTOPEDMARC2P.Read(CodSelect);
                   
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOPEDMARC2Coll[rowindex].IDPRODUTOPEDMARC2);
                            PRODUTOPEDMARC2P.Delete(CodSelect);
                            ListaProdutos2(_IDPEDIDOMARC);
                            
                            Entity3 = null;

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

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirPedidoLJ();
        }

        private void modelo3EconomicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOMARC == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
                ImprimirReportModeloEconomico();
        }

        private void ImprimirReportModeloEconomico()
        {
            using (FrmPedidoMT3_Economico frm = new FrmPedidoMT3_Economico())
            {
                PEDIDOMARCP.Save(Entity);
                frm.LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCColl;
                frm.LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2Coll;
                frm.LIS_MATERIALPEDIDOColl = LIS_MATERIALPEDIDOColl;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDOMARC = _IDPEDIDOMARC;
                frm.ShowDialog();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

    }
}
