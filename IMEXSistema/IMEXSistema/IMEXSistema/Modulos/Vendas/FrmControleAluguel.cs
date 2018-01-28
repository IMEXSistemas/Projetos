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
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Hospedagem;


namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmControleAluguel : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        Boolean FLAGORCAMENTO = false;
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
        PRODUTOSCollection PRODUTOSMTColl = new PRODUTOSCollection();
        CORCollection CORColl = new CORCollection();
        CORCollection COR2Coll = new CORCollection();
        LIS_PRODUTOCOMPOSICAOCollection LIS_PRODUTOCOMPOSICAOColl = new LIS_PRODUTOCOMPOSICAOCollection();
        TAMANHOCollection TAMANHOColl = new TAMANHOCollection();

        CONTRATOProvider CONTRATOP = new CONTRATOProvider();
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
        STATUSProvider STATUSP = new STATUSProvider();
        

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        string CasasDecimais = string.Empty;
        public int _IDRESULTADO = -1;

        public FrmControleAluguel()
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
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue); ;
                string PRAZOENTREGA = txtPrazoEntrega.Text;

                int? IDTRANSPORTES = null;
                if (cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                int? IDVENDEDOR = null;
                if (cbFuncionario.SelectedIndex > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);


                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValComissao.Text))
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

                string FLAGPRODIMPRESSAO = "N";
                string PRODUTOFINAL = string.Empty;
                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
                string NREFERENCIA = txtNumReferencia.Text;
                string FLAGVLMETRO = "N";

                DateTime? DataEntrega = null;
                DataEntrega = Convert.ToDateTime(dateTimePickerEntrega.Value);

                DateTime? dataretirada = null;
                dataretirada = Convert.ToDateTime(dateTimePickerRetirada.Value);

                string OBSANEXO = txtContrato.Text;

                string FLAGTELABLOQUEADA =  "N";

                decimal TIPOPAGTODINHEIRO = Convert.ToDecimal(txtTipoPagamentoDinheiro.Text);
                decimal TIPOPAGTOCHEQUE = Convert.ToDecimal(txtTipoPagamentoCheque.Text);
                decimal TIPOPAGTOCARTAODEBITO = Convert.ToDecimal(txtTipoPagamentoCartaoDebito.Text);
                decimal TIPOPAGTOCARTAOCREDITO = Convert.ToDecimal(txtTipoPagamentoCartaoCredito.Text);

                DateTime? DATAVECTO = null;
                int? IDSUPERVISOR = null;
                int? IDMESA = -1;               

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
                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S" ? true : false;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = value.NREFERENCIA;

                    if (value.DATAENTREGA != null)
                    {
                        dateTimePickerEntrega.Text = Convert.ToDateTime(value.DATAENTREGA).ToString("dd/MM/yyyy");

                    }
                  //  else
                    //    dateTimePicker1.Checked = false;

                    //if (value.FLAGTELABLOQUEADA != null && value.FLAGTELABLOQUEADA.Trim() == "S")
                    //{
                    //    chkTelaBloqueada.Text = "Tela Bloqueada";
                    //    chkTelaBloqueada.ForeColor = System.Drawing.Color.Red;
                    //    chkTelaBloqueada.Checked = true;
                    //}
                    //else
                    //{
                    //    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    //    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;
                    //    chkTelaBloqueada.Checked = false;
                    //}


                    txtTipoPagamentoDinheiro.Text = Convert.ToDecimal(value.TIPOPAGTODINHEIRO).ToString("n2");
                    txtTipoPagamentoCheque.Text = Convert.ToDecimal(value.TIPOPAGTOCHEQUE).ToString("n2");
                    txtTipoPagamentoCartaoDebito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAODEBITO).ToString("n2");
                    txtTipoPagamentoCartaoCredito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAOCREDITO).ToString("n2");

                   // if (value.DATAVECTO != null)
                  //      mkdDataVecto.Text = Convert.ToDateTime(value.DATAVECTO).ToString("dd/MM/yyy");
                  //  else
                   //     mkdDataVecto.Text = "  /  /";

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

                    //dateTimePicker1.Checked = false;

                    rdOrcamento.Checked = true;
                    rdVenda.Checked = !rdOrcamento.Checked;
                    txtNumReferencia.Text = string.Empty;

                  //  chkTelaBloqueada.Checked = false;
                   // chkTelaBloqueada.Text = "Tela Desbloqueada";
                  //  chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;

                    txtTipoPagamentoDinheiro.Text = "0,00";
                    txtTipoPagamentoCheque.Text = "0,00";
                    txtTipoPagamentoCartaoDebito.Text = "0,00";
                    txtTipoPagamentoCartaoCredito.Text = "0,00";

                   // mkdDataVecto.Text = "  /  /";

                    txtNumReferencia.Focus();
                    errorProvider1.Clear();

                    BuscaContrato();
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
                decimal COMISSAO = Convert.ToDecimal(txtValComissao.Text);
                
                int? IDCOR = null;
                if (Convert.ToInt32(cbCor.SelectedValue) > 0)
                    IDCOR = Convert.ToInt32(cbCor.SelectedValue);

                string DADOSADICIONAIS = txtDadosAdicionais.Text;

                decimal BUSTO = Convert.ToDecimal(txtBustoTorax.Text);
                decimal CINTURA = Convert.ToDecimal(txtCintura.Text);
                decimal QUADRIL = Convert.ToDecimal(txtQuadril.Text);
                decimal COLARINHO = Convert.ToDecimal(txtColarinho.Text);
                decimal MANGA = Convert.ToDecimal(txtManga.Text);
                decimal ALTURA = Convert.ToDecimal(txtAltura.Text);
                decimal BARRA = Convert.ToDecimal(txtBarra.Text);

                int? IDTAMANHO = null;
                if (Convert.ToInt32(cbTamanho.SelectedValue) > 0)
                    IDTAMANHO = Convert.ToInt32(cbTamanho.SelectedValue);

               return new PRODUTOSPEDIDOEntity(_IDPRODPEDIDO, _IDPEDIDO, IDPRODUTO,
                                               QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO, IDCOR, 0, "S",
                                               DADOSADICIONAIS, ALTURA, 0, null, null, 0, 0,
                                               BUSTO, QUADRIL, COLARINHO, MANGA, BARRA, CINTURA, IDTAMANHO);
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

                    txtDadosAdicionais.Text = value.DADOSADICIONAIS;

                    txtBustoTorax.Text = Convert.ToDecimal(value.BUSTO).ToString("n2");
                    txtCintura.Text = Convert.ToDecimal(value.CINTURA).ToString("n2");
                    txtQuadril.Text = Convert.ToDecimal(value.QUADRIL).ToString("n2");

                    txtColarinho.Text = Convert.ToDecimal(value.COLARINHO).ToString("n2");
                    txtManga.Text = Convert.ToDecimal(value.MANGA).ToString("n2");
                    txtAltura.Text = Convert.ToDecimal(value.ALTURA).ToString("n2");
                    txtBarra.Text = Convert.ToDecimal(value.BARRA).ToString("n2");


                    if (value.IDTAMANHO != null)
                       cbTamanho.SelectedValue = value.IDTAMANHO;
                    else
                        cbTamanho.SelectedValue = -1;
                    
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDIDO = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = "1";
                    txtValorUnitProd.Text = "0,00";
                    txtDadosAdicionais.Text = string.Empty;

                    cbCor.SelectedValue = -1;

                    txtBustoTorax.Text = "0,00";
                    txtCintura.Text = "0,00";
                    txtQuadril.Text = "0,00";

                    txtColarinho.Text = "0,00";
                    txtManga.Text = "0,00";
                    txtAltura.Text = "0,00";
                    txtBarra.Text = "0,00";
                    cbTamanho.SelectedValue = -1;

                    errorProvider1.Clear();
                }
            }
        }
      

        private void FrmPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao" && this.ActiveControl.Name != "txtContrato")
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
            GetDropCor();
            GetDropContrato();
            BuscaContrato();
            GetDropTamanho();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnContrato.Image = Util.GetAddressImage(6);
            btCadTamanho.Image = Util.GetAddressImage(6);   
          
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadCor.Image = Util.GetAddressImage(6);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);


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

            if(_IDRESULTADO != -1)
            {
                DialogResult dr = MessageBox.Show("Deseja realmente gerar Controle de Aluguel pela Reserva nº " + _IDRESULTADO.ToString().PadLeft(6, '0') + " ?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    AddAluguelReserva(_IDRESULTADO);
                }
                else
                    this.Close();

            }


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
            CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMATy = CONFISISTEMAP.Read(1);
            FLAGPEDBAIXAESTOQUE = CONFISISTEMATy.FLAGPEDBAIXAESTOQUE;

            //"S" para comissão sobre o total o pedido
            //"N" para comissão pelo total dos produto
            FLAGCOMISSAO = CONFISISTEMATy.FLAGCOMISSAO;
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
            //11 Pedido de Venda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
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

                    if (result > 0)
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

            SomaUnitMTLinear();
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

                SomaUnitMTLinear();
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
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0'); 
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                                MessageBox.Show("Erro técnico: " + ex.Message);

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
            else if (Convert.ToDateTime(dateTimePickerRetirada.Text) > Convert.ToDateTime(dateTimePickerEntrega.Text))
            {
                string MSG = "Data de retirada não pode maior que a data da entrega";
                errorProvider1.SetError(lblSaida, MSG);
                errorProvider1.SetError(lblDataEntrada, MSG);
                MessageBox.Show(MSG);
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

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_PEDIDOColl[rowindex].IDPEDIDO.ToString().PadLeft(6, '0') + " - " + LIS_PEDIDOColl[rowindex].NOMECLIENTE,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                //Lista  Produto do Pedido     
                                CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                                ListaProdutoPedido(Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO));

                                //Exluir Produto do Pedido
                                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                                {
                                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
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
                        GridDuplicatasPD(-1, "");
                        ListaProdutoPedido(-1);

                        Entity = null;
                       

                        this.Cursor = Cursors.Default;
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
            Entity = null;
            Entity2 = null;
           
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
              GravaProduto();
        }

        private void GravaProduto()
        {
            try
            {
                if (ValidacoesProdutos() && Validacoes())
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
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (VerificaProdutoReserva())
            {
                 result = false;
            }
            else if (VerificaProdutoReserva2())
            {
                result = false;
            } 

           
            int IDstatus = Convert.ToInt32(PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue)).IDSTATUS);
            string FlagSatus = STATUSP.Read(IDstatus).FLAGMOVIMENTACAO.TrimEnd().TrimStart();

            if (FlagSatus == "S")
            {

                STATUSEntity STATUSTy = new STATUSEntity();
                STATUSTy = STATUSP.Read(IDstatus);
                string MSGerro = "Não é possível efetuar o aluguel, produto está com o status: " + STATUSTy.NOME;
                result = false;
                errorProvider1.SetError(cbProduto, MSGerro);
                MessageBox.Show(MSGerro);
            }
            


            return result;
        }

        private Boolean VerificaProdutoReserva()
        {
            Boolean result = false;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "<>", cbCliente.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerRetirada.Text)));
            RowRelatorio.Add(new RowsFiltro("DATARETIRADA", "System.DateTime", "<=", Util.ConverStringDateSearch(dateTimePickerEntrega.Text)));

            LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl2 = new LIS_PRODUTORESERVACollection();
            LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP = new LIS_PRODUTORESERVAProvider();
            LIS_PRODUTORESERVAColl2 = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_PRODUTORESERVAColl2.Count > 0)
            {
                MessageBox.Show("Existe reserva para o produto " + cbProduto.Text + ", Data da Retirada: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATARETIRADA).ToString("dd/MM/yyyy") + " e entrega: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATAENTREGA).ToString("dd/MM/yyyy") + " - Controle nº: " + LIS_PRODUTORESERVAColl2[0].IDRESERVA.ToString().PadLeft(6, '0') + " !",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                DialogResult dr = MessageBox.Show("Deseja abrir a tela de Reserva?",
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmReserva2 frm = new FrmReserva2())
                    {

                        frm._IDRESERVA = Convert.ToInt32(LIS_PRODUTORESERVAColl2[0].IDRESERVA);
                        frm.ShowDialog();
                    }
                }


                result = true;
            }

            return result;
        }

        private Boolean VerificaProdutoReserva2()
        {
            Boolean result = false;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "<>", cbCliente.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerRetirada.Text)));
            RowRelatorio.Add(new RowsFiltro("FLAGNOVARESERVA", "System.String", "=", "N"));

            LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl2 = new LIS_PRODUTORESERVACollection();
            LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP = new LIS_PRODUTORESERVAProvider();
            LIS_PRODUTORESERVAColl2 = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_PRODUTORESERVAColl2.Count > 0)
            {
                MessageBox.Show("O produto: " + cbProduto.Text + ", está reservado exclusivamente até a data de entrega: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATAENTREGA).ToString("dd/MM/yyyy") + " - Controle nº: " + LIS_PRODUTORESERVAColl2[0].IDRESERVA.ToString().PadLeft(6, '0') + "!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                DialogResult dr = MessageBox.Show("Deseja abrir a tela de Reserva?",
                       ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmReserva2 frm = new FrmReserva2())
                    {
                        frm._IDRESERVA = Convert.ToInt32(LIS_PRODUTORESERVAColl2[0].IDRESERVA);
                        frm.ShowDialog();
                    }
                }


                result = true;
            }

            return result;
        }

        private void ListaProdutoPedido(int IDPEDIDO)
        {
            RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
            RowpProdPedido.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowpProdPedido);

            DGDadosProduto.AutoGenerateColumns = false;
            DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
            lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

            SumTotalProdutosPedido();
            //"S" para comissão sobre o total o pedido
            //"N" para comissão pelo total dos produto
            if (FLAGCOMISSAO == "N")
                SumTotalComissao();
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
            if (LIS_DUPLICATARECEBERColl.Count > 0)
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
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

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
                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                        {
                            PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
                        }

                        PEDIDOP.Delete(_IDPEDIDO);                      
                    
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
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;
                

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
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
                decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);
                txtValorUnitProd.Text = ValorVenda.ToString("n2");               
            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFuncionario.SelectedIndex > 0)
            {
                    FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                    decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);
                    txtPorComisVend.Text = PorcComissVend.ToString("n2");
                    txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * PorcComissVend) / 100).ToString("n2");
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
            RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PD" + numero));

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
              //  frm._IDTIPODUPLICATA = CodSelec;
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
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(3);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeral();
            }
        }

      
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
                PEDIDOP.Save(Entity);
                ImprimirReceitaReport();
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
                FLAGORCAMENTO = false;
                ImprimirPedidoLJ();
            }
        }

        private void ImprimirPedidoLJ()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument7;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                objPrintPreview.Document = printDocument7;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }


        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        string RelatorioTitulo = string.Empty;
        int IndexRegistro = 0;
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
           
        }

        private void txtQuanProduto_KeyDown(object sender, KeyEventArgs e)
        {
           
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
                tabControlPedidoVenda.SelectTab(2); ;
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

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine("Controle N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0'));
                ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                ticket.AddSubHeaderLine("Funcionário: " + cbFuncionario.Text);
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
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotalProduto.Text))
            {
                errorProvider1.SetError(txtValorTotalProduto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                SomaUnitMTLinear();
            }
            else
            {

                Double f = Convert.ToDouble(txtValorTotalProduto.Text);
                txtValorTotalProduto.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorTotalProduto, "");
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
                txtValorTotalProduto.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o cálculo!");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                                PRODUTOSPEDIDOP.Delete(CodSelect);
                                ListaProdutoPedido(_IDPEDIDO);

                                Entity2 = null;

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

        private void modelo2DiversosItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                FLAGORCAMENTO = false;
                ImprimirPedidoDivItens();
            }
        }


        private void ImprimirPedidoDivItens()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                if (LIS_PRODUTOSPEDIDOColl.Count > 0)
                {
                    printDialog1.Document = printDocument_Prod1;
                    objPrintPreview.Document = printDocument_Prod1;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }


                printDialog1.Document = printDocumentRodape;
                objPrintPreview.Document = printDocumentRodape;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();

            }
        }

        int linhaPrint = 0;
        int paginaAtual = 0;
        private void printDocument7_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            linhaPrint = 0;
            IndexRegistro = 0;
            paginaAtual = 0;
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

                                        //Salva a forma de pagamento no Pedido
                                        PEDIDOP.Save(Entity);

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
            if (LIS_PEDIDOColl.Count == 0)
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
            using (FrmVendaVendedorPedNormal frm = new FrmVendaVendedorPedNormal())
            {
                frm.ShowDialog();
            }

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
            FrmVendaProduto FrmV = new FrmVendaProduto();
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
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PEDIDOP.Save(Entity);
                ImprimirReceitaReport();
            }
        }

        private void ImprimirReceitaReport()
        {
            using (FrmRelatPedidoSimpleAL frm = new FrmRelatPedidoSimpleAL())
            {
                frm.obsanexo = "CONTRATO: \n" + txtContrato.Text;
                frm.dataretiradaSelec = dateTimePickerRetirada.Text;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void txtQuanProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void adicionarPelaReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchReserva frm = new FrmSearchReserva())
            {
                frm.ShowDialog();
                var result = frm.Result;

                 DialogResult dr = MessageBox.Show("Deseja realmente gerar Controle de Aluguel pela Reserva nº " + result.ToString().PadLeft(6, '0') + " ?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                 if (dr == DialogResult.Yes)
                 {

                     AddAluguelReserva(result);
                 }

            }
        }

        private void AddAluguelReserva(int IDRESULTADO)
        {
            try
            {
                // Limpa Tudo
                Entity = null;
                Entity2 = null;

                // Foca nas primeiras abas
                tabControl1.SelectTab(0);
                tabControlPedidoVenda.SelectTab(0);

                //Armazena dados da Reserva
                RESERVAEntity RESERVATy = new RESERVAEntity();
                RESERVAProvider RESERVAP = new RESERVAProvider();
                RESERVATy = RESERVAP.Read(IDRESULTADO);

                // Atualizar Drop Cliente
                GetDropCliente();
                cbCliente.SelectedValue = RESERVATy.IDCLIENTE;
                dateTimePickerRetirada.Text = Convert.ToDateTime(RESERVATy.DATARETIRADA).ToString();
                dateTimePickerEntrega.Text = Convert.ToDateTime(RESERVATy.DATAENTREGA).ToString();

                // Grava o Pedido
                _IDPEDIDO = PEDIDOP.Save(Entity);
                txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                txtValorPago.Text = Convert.ToDecimal(RESERVATy.VLPAGO).ToString("n2");

                //Produto
                //Armazena dados Produto do Pedido
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", IDRESULTADO.ToString()));
                LIS_PRODUTORESERVACollection LIS_PRODUTORESERVA2Coll = new LIS_PRODUTORESERVACollection();
                LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP = new LIS_PRODUTORESERVAProvider();
                LIS_PRODUTORESERVA2Coll = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTORESERVAEntity item in LIS_PRODUTORESERVA2Coll)
                {
                    cbProduto.SelectedValue = item.IDPRODUTO;
                    txtQuanProduto.Text = item.QUANT.ToString();
                    txtValorUnitProd.Text = Convert.ToDecimal(item.VLUNITARIO).ToString("n2");

                    PRODUTOSPEDIDOP.Save(Entity2);
                    Entity2 = null;
                }

                ListaProdutoPedido(_IDPEDIDO);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao adicionar Aluguel pela Reserva!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void contratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            using (FrmContrato frm = new FrmContrato())
            {
                int CodSelec = Convert.ToInt32(cbContrato.SelectedValue);
                frm._IDCONTRATO = CodSelec;
                frm.ShowDialog();
                GetDropContrato();
                cbContrato.SelectedValue = CodSelec;
            }
        }

        private void GetDropContrato()
        {
            CONTRATOCollection CONTRATOColl = new CONTRATOCollection();
            CONTRATOProvider CONTRATOP = new CONTRATOProvider();
            CONTRATOColl = CONTRATOP.ReadCollectionByParameter(null, "NOME");

            cbContrato.DisplayMember = "NOME";
            cbContrato.ValueMember = "IDCONTRATO";

            CONTRATOEntity CONTRATOTy = new CONTRATOEntity();
            CONTRATOTy.NOME = ConfigMessage.Default.MsgDrop;
            CONTRATOTy.IDCONTRATO = -1;
            CONTRATOColl.Add(CONTRATOTy);

            Phydeaux.Utilities.DynamicComparer<CONTRATOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTRATOEntity>(cbContrato.DisplayMember);

            CONTRATOColl.Sort(comparer.Comparer);
            cbContrato.DataSource = CONTRATOColl;

            cbContrato.SelectedIndex = 0;
        }

        private void BuscaContrato()
        {
            CONTRATOCollection CONTRATOColl = new CONTRATOCollection();

            CONTRATOColl = CONTRATOP.ReadCollectionByParameter(null);

            foreach (CONTRATOEntity item in CONTRATOColl)
            {
                if (item.FLAGPRINCIPAL.TrimEnd() == "S")
                {
                    txtContrato.Text = item.DESCRICAO.TrimEnd(); ;
                    break;
                }
            }

        }

        private void cbContrato_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbContrato.SelectedValue) > 0)
            {
                txtContrato.Text = CONTRATOP.Read(Convert.ToInt32(cbContrato.SelectedValue)).DESCRICAO.TrimEnd();
            }
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtContrato.Text = string.Empty;
        }

        private void txtBustoTorax_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtCintura_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtQuadril_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void preçoCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
             ImprimirDuplicata1ViaPrecoCusto();
        }

        private void ImprimirDuplicata1ViaPrecoCusto()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument2;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument2.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                objPrintPreview.Document = printDocument2;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument2_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                //Busca o preço de custo final do produto
                //e soma os totais da duplicata
                Decimal TotalDuplicata = 0;
                DateTime UltimoVecto = Convert.ToDateTime(dateTimePickerEntrega.Text);
                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    TotalDuplicata += Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO)).VALORCUSTOFINAL);
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
                e.Graphics.DrawString("Nome Produto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 10, 510);
                e.Graphics.DrawString("Valor Custo", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 150, 510);

                //2º Coluna
                if (LIS_PRODUTOSPEDIDOColl.Count > 3)
                {
                    e.Graphics.DrawString("Nome Produto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 220, 510);
                    e.Graphics.DrawString("Valor Custo", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 360, 510);
                }

                //3º Coluna
                if (LIS_PRODUTOSPEDIDOColl.Count > 6)
                {
                    e.Graphics.DrawString("Nome Produto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 430, 510);
                    e.Graphics.DrawString("Valor Custo", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 570, 510);
                }

                int linha = 525;
                int linha2 = 525;
                int linha3 = 525;
                for (int i = 0; i < LIS_PRODUTOSPEDIDOColl.Count; i++)
                {
                    if (i < 3)
                    {
                        e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[i].NOMEPRODUTO,15), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, linha);
                        e.Graphics.DrawString(Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[i].IDPRODUTO)).VALORCUSTOFINAL).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 150, linha);
                        linha = linha + 15;
                    }
                    else if (i < 6)
                    {
                        e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[i].NOMEPRODUTO,15), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 220, linha2);
                        e.Graphics.DrawString(Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[i].IDPRODUTO)).VALORCUSTOFINAL).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 360, linha2);
                        linha2 = linha2 + 15;
                    }
                    else if (i < 9)
                    {
                        e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSPEDIDOColl[i].NOMEPRODUTO,15), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 430, linha3);
                        e.Graphics.DrawString(Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[i].IDPRODUTO)).VALORCUSTOFINAL).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 570, linha3);
                        linha3 = linha3 + 15;
                    }
                }


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void modelo2EconômicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PEDIDOP.Save(Entity);
                ImprimirReceitaReportEconomico();
            }
        }
        private void ImprimirReceitaReportEconomico()
        {
            using (FrmRelatPedidoSimplesEconomico frm = new FrmRelatPedidoSimplesEconomico())
            {
                frm.obsanexo = "CONTRATO: \n" + txtContrato.Text;
                frm.dataretiradaSelec = dateTimePickerRetirada.Text;
                //frm.datauso = dateTimePickerUSo.Text;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void txtColarinho_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtManga_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtAltura_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtBarra_Validating(object sender, CancelEventArgs e)
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
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void btCadTamanho_Click(object sender, EventArgs e)
        {
            using (FrmTamanho frm = new FrmTamanho())
            {
                int CodSelec = Convert.ToInt32(cbTamanho.SelectedValue);
                frm._IDTAMANHO = CodSelec;
                frm.ShowDialog();
                GetDropTamanho();
                cbTamanho.SelectedValue = CodSelec;
            }
        }

        private void GetDropTamanho()
        {
            cbTamanho.DisplayMember = "NOME";
            cbTamanho.ValueMember = "IDTAMANHO";

            TAMANHOProvider TAMANHOP = new TAMANHOProvider();
            TAMANHOColl = TAMANHOP.ReadCollectionByParameter(null, "NOME");

            TAMANHOEntity TAMANHOTy = new TAMANHOEntity();
            TAMANHOTy.NOME = ConfigMessage.Default.MsgDrop;
            TAMANHOTy.IDTAMANHO = -1;
            TAMANHOColl.Add(TAMANHOTy);

            Phydeaux.Utilities.DynamicComparer<TAMANHOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TAMANHOEntity>(cbTamanho.DisplayMember);

            TAMANHOColl.Sort(comparer.Comparer);
            cbTamanho.DataSource = TAMANHOColl;

            cbTamanho.SelectedIndex = 0;
        }

        private void modelo2Econômico2ViasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                PEDIDOP.Save(Entity);
                ImprimirReceitaReportEconomico2();
            }
        }

        private void ImprimirReceitaReportEconomico2()
        {
            using (FrmRelatPedidoSimplesEconomico2 frm = new FrmRelatPedidoSimplesEconomico2())
            {
                frm.obsanexo = "CONTRATO: \n" + txtContrato.Text;
                frm.dataretiradaSelec = dateTimePickerRetirada.Text;
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
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
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Aluguel");

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
                frm.TituloSelec = "Relação de Aluguel";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (FrmRelatContrato2 frm = new FrmRelatContrato2())
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCONTRATO", "System.Int32", "=", cbContrato.SelectedValue.ToString()));
                frm.CONTRATOColl = CONTRATOP.ReadCollectionByParameter(RowRelatorio);
                frm.ShowDialog();
            }
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


    }

}