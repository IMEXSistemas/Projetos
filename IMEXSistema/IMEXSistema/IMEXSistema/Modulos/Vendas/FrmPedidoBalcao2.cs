using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSworks.UI;

using BmsSoftware.Modulos.FrmSearch;
using System.IO;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Cadastros;
using CDSSoftware;
using BmsSoftware.UI;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Modulos.Servicos;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using System.Runtime.InteropServices;
using winfit.Modulos.Outros;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPedidoBalcao2 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        Utility Util = new Utility();

        string FLAGCOMISSAO = string.Empty;
        string FLAGPEDBAIXAESTOQUE = string.Empty;

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        CAIXACollection CAIXAColl = new CAIXACollection();        
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();

        CAIXAProvider CAIXAP = new CAIXAProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        CONFIGPEDBALCAOProvider CONFIGPEDBALCAOP = new CONFIGPEDBALCAOProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();

        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
        CONFIGPEDBALCAOEntity CONFIGPEDBALCAOTy = new CONFIGPEDBALCAOEntity();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int? _IDMESA = null;        
        DateTime? _DATAHORAEMISSAO = null;

        public FrmPedidoBalcao2()
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

        int _IDPEDIDO = -1;
        int _IDCAIXA = -1;
        public PEDIDOEntity Entity
        {
            get
            {
                int? IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);                 

                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                if (_IDPEDIDO == -1)
                {
                    DTEMISSAO = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    _DATAHORAEMISSAO = DTEMISSAO;
                }
                else
                {
                    _DATAHORAEMISSAO = Convert.ToDateTime(_DATAHORAEMISSAO);
                    DTEMISSAO = Convert.ToDateTime(_DATAHORAEMISSAO);
                }


                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                string PRAZOENTREGA = string.Empty;

                int? IDTRANSPORTES = null;
               
                if (CONFIGPEDBALCAOTy.IDTRANSPORTE != null)
                    IDTRANSPORTES = Convert.ToInt32(CONFIGPEDBALCAOTy.IDTRANSPORTE);

                int? IDVENDEDOR = null;
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    IDVENDEDOR = Convert.ToInt32(cbFuncionario.SelectedValue);

                decimal VALORCOMISSAO = 0;
                string OBSERVACAO = txtObservacao.Text;

                if (txtTotalPedFech.Text == string.Empty)
                    txtTotalPedFech.Text = "0,00";
                decimal TOTALPRODUTOS = Convert.ToDecimal(txtTotalPedFech.Text);

                decimal TOTALIMPOSTOS = 0;

                if (txtPorcDesconto.Text == string.Empty)
                    txtPorcDesconto.Text = "0,00";
                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconto.Text);

                if (txtTotalDesconto.Text == string.Empty)
                    txtTotalDesconto.Text = "0,00";
                decimal VALORDESCONTO = Convert.ToDecimal(txtTotalDesconto.Text);

                decimal PORACRESCIMO = Convert.ToDecimal(txtPorcAcrescimo.Text);
                decimal VALORACRESCIMO = Convert.ToDecimal(txtValorAcrescimo.Text) ;

                if (txtTotalPedFech.Text == string.Empty)
                    txtTotalPedFech.Text = "0,00";
                decimal TOTALPEDIDO = Convert.ToDecimal(txtTotalPedFech.Text);

                int? IDFORMAPAGAMENTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                    IDFORMAPAGAMENTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                if (txtValorPago.Text == string.Empty)
                    txtValorPago.Text = "0,00";
                decimal VALORPAGO = Convert.ToDecimal(txtValorPago.Text)  - Convert.ToDecimal(txtTroco.Text);

                if (txtValorDev.Text == string.Empty)
                    txtValorDev.Text = "0,00";
                decimal VALORDEVEDOR = Convert.ToDecimal(txtValorDev.Text) - Convert.ToDecimal(txtTotalDesconto.Text);
                if (VALORDEVEDOR < 0)
                    VALORDEVEDOR = 0;

                int? IDLOCALCOBRANCA = null;              

                int? IDCENTROCUSTOS = null;
                if (CONFIGPEDBALCAOTy.IDCENTROCUSTO != null)
                    IDCENTROCUSTOS = Convert.ToInt32(CONFIGPEDBALCAOTy.IDCENTROCUSTO);  
            
               string FLAGPRODIMPRESSAO = "S";
               string PRODUTOFINAL = string.Empty;
               string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
               string NREFERENCIA = string.Empty;
               string FLAGVLMETRO = "N";
               DateTime? Dataentrega = null;

               string FLAGTELABLOQUEADA = chkTelaBloqueada.Checked ? "S" : "N";

               decimal TIPOPAGTODINHEIRO = Convert.ToDecimal(txtTipoPagamentoDinheiro.Text);
               decimal TIPOPAGTOCHEQUE = Convert.ToDecimal(txtTipoPagamentoCheque.Text);
               decimal TIPOPAGTOCARTAODEBITO = Convert.ToDecimal(txtTipoPagamentoCartaoDebito.Text);
               decimal TIPOPAGTOCARTAOCREDITO = Convert.ToDecimal(txtTipoPagamentoCartaoCredito.Text);
               DateTime? DATAVECTO = null;
               int? IDSUPERVISOR = null;                             

                return new PEDIDOEntity(_IDPEDIDO, IDCLIENTE, DTEMISSAO, IDSTATUS, PRAZOENTREGA,
                                        IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO, OBSERVACAO,
                                        TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                        VALORDESCONTO, PORACRESCIMO, VALORACRESCIMO,
                                        TOTALPEDIDO, IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR,
                                        IDLOCALCOBRANCA, IDCENTROCUSTOS, NREFERENCIA, "N", FLAGORCAMENTO, "", "", "", Dataentrega,
                                        FLAGTELABLOQUEADA, TIPOPAGTODINHEIRO, TIPOPAGTOCHEQUE,
                                        TIPOPAGTOCARTAODEBITO, TIPOPAGTOCARTAOCREDITO, DATAVECTO, IDSUPERVISOR,
                                        _IDMESA);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDO = value.IDPEDIDO;
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                    _IDMESA = value.IDMESA;                  
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");
                    _DATAHORAEMISSAO = Convert.ToDateTime(value.DTEMISSAO);
                    this.Text = "Pedido Balcão - " + "Nº: " + _IDPEDIDO.ToString().PadLeft(6, '0');

                    if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                        this.Text = "Pedido Balcão - " + "Nº: " + _IDPEDIDO.ToString().PadLeft(6, '0') + " Mesa Nº: " + _IDMESA.ToString().PadLeft(3, '0');
                    
                    cbCliente.SelectedValue = value.IDCLIENTE;
                    cbStatus.SelectedValue = value.IDSTATUS;
                   
                    
                  

                    if (value.IDVENDEDOR != null)
                        cbFuncionario.SelectedValue = value.IDVENDEDOR;
                    else
                        cbFuncionario.SelectedIndex = 0;
                   
                    txtObservacao.Text = value.OBSERVACAO;

                    txtTotalPedFech.Text = Convert.ToDecimal(value.TOTALPEDIDO).ToString("n2");
                    txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtTotalDesconto.Text = Convert.ToDecimal(value.VALORDESCONTO).ToString("n2");
                
                    txtValorDev.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");

                    if (value.IDFORMAPAGAMENTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGAMENTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    txtValorPago.Text = Convert.ToDecimal(value.VALORPAGO).ToString("n2");
                    txtValorDev.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");

                    txtPorcAcrescimo.Text = Convert.ToDecimal(value.PORCACRESCIMO).ToString("n2");
                    txtValorAcrescimo.Text = Convert.ToDecimal(value.VALORACRESCIMO).ToString("n2");


                    if (value.FLAGTELABLOQUEADA != null)
                        chkTelaBloqueada.Checked = value.FLAGTELABLOQUEADA.TrimEnd() == "S" ? true : false;

                    if (value.FLAGTELABLOQUEADA != null && value.FLAGTELABLOQUEADA.Trim() == "S")
                    {
                        chkTelaBloqueada.Text = "Tela Bloqueada";
                        chkTelaBloqueada.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        chkTelaBloqueada.Text = "Tela Desbloqueada";
                        chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;
                    }

                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S" ? true : false;
                    rdVenda.Checked = !rdOrcamento.Checked;

                    txtTipoPagamentoDinheiro.Text = Convert.ToDecimal(value.TIPOPAGTODINHEIRO).ToString("n2");
                    txtTipoPagamentoCheque.Text = Convert.ToDecimal(value.TIPOPAGTOCHEQUE).ToString("n2");
                    txtTipoPagamentoCartaoDebito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAODEBITO).ToString("n2");
                    txtTipoPagamentoCartaoCredito.Text = Convert.ToDecimal(value.TIPOPAGTOCARTAOCREDITO).ToString("n2");

                    //Busca informações de Caixa
                    GetInfoCaixa();

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPEDIDO = -1;                    
                    _DATAHORAEMISSAO = null;

                    _IDCAIXA = -1;
                    txtNPedido.Text = string.Empty;
                    this.Text = "Pedido Balcão";

                    // Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);
                    //Limpa Produtos
                    //Limpa Produtos
                    LIS_PRODUTOSPEDIDOColl.Clear();
                    DGDadosProduto.DataSource = null;

                    cbCliente.SelectedValue = 1;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                 
                 
                    cbFuncionario.SelectedIndex = 0;
                 
                    txtObservacao.Text = string.Empty;
                 
                    txtPorcDesconto.Text = "0,00";
                    txtTotalDesconto.Text = "0,00";

                    txtPorcAcrescimo.Text = "0,00";
                    txtValorAcrescimo.Text = "0,00";

                    if (BmsSoftware.ConfigSistema1.Default.AcrescimoPedido.Trim() != string.Empty)
                        txtPorcAcrescimo.Text = BmsSoftware.ConfigSistema1.Default.AcrescimoPedido;

                    txtValorDev.Text = "0,00";
                    txtTotalPedFech.Text = "0,00";
                   
                    txtValorPago.Text = "0,00";
                    txtValorDev.Text = "0,00";
                    lblTotalPedido.Text = "0,00";

                    cbFormaPagto.SelectedValue = 1;
                 
                    //Preenche Mensagem Salvo na configuração do Sistema
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    txtObservacao.Text = CONFISISTEMAP.Read(1).MSGPEDIDO;                    

                    chkTelaBloqueada.Checked = false;
                    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;
                    chkTelaBloqueada.Checked = false;

                    txtTipoPagamentoDinheiro.Text = "0,00";
                    txtTipoPagamentoCheque.Text = "0,00";
                    txtTipoPagamentoCartaoDebito.Text = "0,00";
                    txtTipoPagamentoCartaoCredito.Text = "0,00";
                    txtTroco.Text = "0,00";

                    rdVenda.Checked = true;
                    rdOrcamento.Checked = !rdVenda.Checked;

                    //Busca o Funcionario logado
                    USUARIOEntity USUARIOTY = new USUARIOEntity();
                    USUARIOProvider USUARIOP = new USUARIOProvider();
                    USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                    cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;
                    this.Cursor = Cursors.Default;

                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODPEDIDO = -1;
        int? IDPRODUTOMASTER = null;
        public PRODUTOSPEDIDOEntity Entity3
        {
            get
            {

                int IDPRODUTO = Convert.ToInt32(PRODUTOSTy.IDPRODUTO);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuant.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnit.Text);
                decimal VALORTOTAL = Convert.ToDecimal(txtValorUnit.Text) * Convert.ToDecimal(txtQuant.Text);

                decimal PORCDESCONTO = 0;

                decimal VALORLIQUIDO = 0;

                decimal BUSTO = 0;
                decimal CINTURA = 0;
                decimal QUADRIL = 0;
                decimal COLARINHO = 0;
                decimal MANGA = 0;
                decimal BARRA = 0;

                return new PRODUTOSPEDIDOEntity(  _IDPRODPEDIDO, _IDPEDIDO , IDPRODUTO ,    QUANTIDADE,   VALORUNITARIO ,
                                                VALORTOTAL , 0, null, 0, "S", "", 0, 0, null, null,0,0,
                                                BUSTO, QUADRIL, COLARINHO, MANGA, BARRA, CINTURA, null);
            }
            set
            {

                if (value != null)
                {
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDIDO = -1;
                    errorProvider1.Clear();
                }
            }
        }     


        private void GetInfoCaixa()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", "PD" + _IDPEDIDO.ToString().PadLeft(6, '0')));
                CAIXACollection CAIXAColl = new CAIXACollection();
                CAIXAProvider CAIXAP = new CAIXAProvider();
                CAIXAColl = CAIXAP.ReadCollectionByParameter(RowRelatorio);

                if (CAIXAColl.Count > 0)
                    _IDCAIXA = CAIXAColl[0].IDCAIXA;
                else
                    _IDCAIXA = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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

        private void FrmPedidoBalcao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
                {
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                    this.Focus();
                }

            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }

            if (e.KeyCode == Keys.F5) 
            {
                btnFechaVenda_Click(null, null);
                btnGravaPedido2_Click(null, null);            
            }

            if (e.KeyCode == Keys.F6)
            {
                btnNovaVenda_Click(null, null);
            }

            if (e.KeyCode == Keys.F7)
              {
                  button2_Click(null, null);
              }

            //iMPRIMIR
            if (e.KeyCode == Keys.F8)
            {
                button1_Click_1(null, null);
            }

            

        }

        private void btnFechaVenda_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
            {
                try
                {
                    if (VerificaPlanos())
                    {
                        if (ValidaPedido2())
                        {
                            _IDPEDIDO = PEDIDOP.Save(Entity);
                            txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');

                            ListaProdutoPedido(_IDPEDIDO);
                            SomaPedido();

                            tabControlPedidoBalcao.SelectTab(1);
                            txtValorPago.Focus();

                            txtPorcAcrescimo_Validating_1(null, null);

                            this.Cursor = Cursors.Default;
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                        else
                            this.Cursor = Cursors.Default;
                    }

                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                Util.ExibirMSg("Não existem produtos lançados!", "Red");
            }
        }       

        private void FrmPedidoBalcao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                txtDescProduto.Select();
                PreencheDropTipoPesquisa();
                PreencheDropCamposPesquisa();
                ExibirLogo();
                GetDropCliente();
                GetDropFormaPgto();
                GetFuncionario();
                GetFuncionario2();
                GetDropStatus();
                GetDropStatus2();
                GetMesa();

                btnCadCliente.Image = Util.GetAddressImage(6);
                btnCadStatus.Image = Util.GetAddressImage(6);
                btnFormPagamento.Image = Util.GetAddressImage(6);
                cbVendedor.Image = Util.GetAddressImage(6);

                btnLimpaPesquisa.Image = Util.GetAddressImage(16);
                btnPesquisa.Image = Util.GetAddressImage(20);
                btnSeach.Image = Util.GetAddressImage(20);

                bntDateSelecFinal2.Image = Util.GetAddressImage(11);
                bntDateSelecInicial2.Image = Util.GetAddressImage(11);

                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);

                msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetConfiSistema();

                cbCliente.SelectedValue = 1;//Codigo 1 Venda a vista           

                VerificaAcesso();               

                msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");                

                cbCamposPesquisa.SelectedIndex = 2;

                //Busca o Funcionario logado
                USUARIOEntity USUARIOTY = new USUARIOEntity();
                USUARIOProvider USUARIOP = new USUARIOProvider();
                USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;
                cbFuncionario2.SelectedValue = USUARIOTY.IDFUNCIONARIO;
                this.Cursor = Cursors.Default;

                mudançaDeMesaToolStripMenuItem.Visible = false;
                this.Text = "Pedido Balcão";
                if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                {
                    cbMesa.Visible = true;
                    label16.Visible = true;
                    btnCaixa.Visible = false;
                    btnLancarCaixa.Visible = false;
                    label8.Visible = false;
                    lblTotalFinanceiro.Visible = false;
                    cbFormaPagto.Visible = false;
                    btnFormPagamento.Visible = false;
                    label28.Visible = false;
                    linkLabel4.Visible = false;
                    dataGridDupl.Visible = false;
                    mudançaDeMesaToolStripMenuItem.Visible = true;

                    if (_IDMESA == null)
                        MessageBox.Show("Mesa Não Selecionada!",
                         ConfigSistema1.Default.NomeEmpresa,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                    else
                    {
                        linkLabel4.Visible = false;
                        label28.Visible = false;
                        dataGridDupl.Visible = false;
                        this.Text = "Pedido Balcão - Mesa Nº: " + _IDMESA.ToString().PadLeft(3, '0');
                        AbrirPedidoMesa(Convert.ToInt32(_IDMESA));
                    }
                }
                else
                    Entity = null;

               

                btnPesquisa_Click(null, null);
            }
            catch (Exception)
            {
               this.Cursor = Cursors.Default;
            }

        }

        private void AbrirPedidoMesa(int NUMEROMESA)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDMESA", "System.Int32", "=", NUMEROMESA.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "47"));
                PEDIDOCollection PEDIDOColl_M = new PEDIDOCollection();
                PEDIDOColl_M = PEDIDOP.ReadCollectionByParameter(RowRelatorio);
                if(PEDIDOColl_M.Count > 0)
                {
                    tabControlPedidoBalcao.SelectTab(0);
                    Entity = PEDIDOP.Read(PEDIDOColl_M[0].IDPEDIDO);
                    ListaProdutoPedido(PEDIDOColl_M[0].IDPEDIDO);
                    txtDescProduto.Focus();
                }
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
                STATUSCollection STATUSColl = new STATUSCollection();
                STATUSColl = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus2.DisplayMember = "NOME";
                cbStatus2.ValueMember = "IDSTATUS";

                STATUSEntity STATUSTy = new STATUSEntity();
                STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTy.IDSTATUS = -1;
                STATUSColl.Add(STATUSTy);

                Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus2.DisplayMember);

                STATUSColl.Sort(comparer.Comparer);
                cbStatus2.DataSource = STATUSColl;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetMesa()
        {
            try
            {
                MESAProvider MESAP = new MESAProvider();
                MESACollection MESAColL = new MESACollection();
                MESAColL = MESAP.ReadCollectionByParameter(null, "NUMERO");
                
                cbMesa.DisplayMember = "NUMERO";
                cbMesa.ValueMember = "IDMESA";

                MESAEntity FMESATy = new MESAEntity();
                FMESATy.NUMERO = -1;
                FMESATy.IDMESA = -1;
                MESAColL.Add(FMESATy);

                Phydeaux.Utilities.DynamicComparer<MESAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MESAEntity>(cbMesa.DisplayMember);

                MESAColL.Sort(comparer.Comparer);
                cbMesa.DataSource = MESAColL;

                cbMesa.SelectedIndex = 0;
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

        private void ExibirLogo()
        {
            try
            {
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

                        pictureBox1.Image = Image.FromStream(stream);

                        pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
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

        private void txtCodProduto_Enter(object sender, EventArgs e)
        {
          
        }

        private void txtCodProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    PesquisaProduto(txtDescProduto.Text);
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
                    }
                }
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                DGDadosProduto.Focus();
            }
        }

        private void PesquisaProduto(string IDPRODUTO)
        {
            PRODUTOSTy = null;
            
            PRODUTOSTy = PesquisaCodBarra(txtDescProduto.Text);
            
            if(PRODUTOSTy  == null)
                PRODUTOSTy = PesquisaCodReferencia(txtDescProduto.Text);
            if (PRODUTOSTy == null && ValidacoesLibrary.ValidaTipoInt32(txtDescProduto.Text))
                PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(IDPRODUTO));

            if (PRODUTOSTy == null)
            {
                txtDescProduto.Focus();
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
                             txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                            // txtCodProduto.Text = PRODUTOSTy.IDPRODUTO.ToString();
                             txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                             txtQuant.Focus();
                         }
                     }

                 }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                if (PRODUTOSTy.FLAGINATIVO.Trim() != "S")
                {
                    txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                    txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                    txtQuant.Focus();
                }
                else
                {
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
                                txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                                //txtCodProduto.Text = PRODUTOSTy.IDPRODUTO.ToString();
                                txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                                txtQuant.Focus();
                            }
                        }

                    }
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
                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));

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
                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));

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
        private void txtDescProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Código e Descrição do Produto, Pressione Ctrl+E para Pesquisar.";
        }

        private void txtDescProduto_KeyDown(object sender, KeyEventArgs e)
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
                        PRODUTOSTy = PRODUTOSP.Read(result);
                        txtDescProduto.Text = PRODUTOSTy.NOMEPRODUTO;
                       // txtCodProduto.Text = PRODUTOSTy.IDPRODUTO.ToString();
                        txtValorUnit.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                        txtQuant.Focus();
                    }
                }
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                DGDadosProduto.Focus();
            }
        }

        private void FrmPedidoBalcao_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void txtQuant_Validating(object sender, CancelEventArgs e)
        {
            //errorProvider1.Clear();           
            //if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text) || Convert.ToDecimal(txtQuant.Text) <= 0)
            //{
            //    errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
            //    Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
            //    txtQuant.Focus();
            //}
            //else
            //{
            //    Double f = Convert.ToDouble(txtQuant.Text);
            //    txtQuant.Text = string.Format("{0:n2}", f);
            //    errorProvider1.SetError(txtQuant, "");
            //}            
        }

        private void txtValorUnit_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnit.Text) || Convert.ToDecimal(txtValorUnit.Text) < 0)
            {
                errorProvider1.SetError(txtValorUnit, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                txtValorUnit.Focus();
            }
            else
            {
                Double f = Convert.ToDouble(txtValorUnit.Text);
                txtValorUnit.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnit, "");
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
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text) + " " + mkdHoraInicial.Text);
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)+ " " + mkdHoraFinal.Text);
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

                if (Convert.ToInt32(cbMesa.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDMESA", "System.Int32", "=", cbMesa.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
              

                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(Filtro, "IDPEDIDO DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");                
                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOColl;

                lblTotalPesquisa.Text = (LIS_PEDIDOColl.Count -1).ToString();
                
                PaintGrid();
            }
            else
                PesquisaFiltro();

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

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);

            }

            return valortotal;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlPedidoBalcao.SelectedIndex == 3)
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

                if (mkdHoraInicial.Text != "  :")
                {
                    filtroProfile = new RowsFiltro("HORAVENDA", "System.DateTime", ">=", mkdHoraInicial.Text);
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (mkdHoraFinal.Text != "  :")
                {
                    filtroProfile = new RowsFiltro("HORAVENDA", "System.DateTime", "<=", mkdHoraFinal.Text);
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(Filtro, "IDPEDIDO DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_PEDIDOTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");                
                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOColl;

                lblTotalPesquisa.Text = (LIS_PEDIDOColl.Count -1).ToString();


                PaintGrid();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Salva Pedido
            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);

            this.Close();
        }

        private void txtValorUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValidaPedido())
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);

                    this.Text = "Pedido Balcão - " + "Nº: " + _IDPEDIDO.ToString().PadLeft(6, '0');
                    if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                        this.Text = "Pedido Balcão - " + "Nº: " + _IDPEDIDO.ToString().PadLeft(6, '0') + " Mesa Nº: " + _IDMESA.ToString().PadLeft(3, '0');

                    AddProdutos();
                    txtDescProduto.Focus();
                    txtDescProduto.Text = string.Empty;
                    txtQuant.Text = "1";
                    txtValorUnit.Text = "0,00";
                    txtDescProduto.Text = string.Empty;
                    ListaProdutoPedido(_IDPEDIDO);
                }
            }
                
        }

        private Boolean ValidaPedido2()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                tabControlPedidoBalcao.SelectTab(1);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

      
          private Boolean ValidaPedido()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnit.Text))
            {
                errorProvider1.SetError(txtValorUnit, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }   
            else
                errorProvider1.Clear();


            return result;
        }

        private void AddProdutos()
        {
            try
            {
                PRODUTOSPEDIDOP.Save(Entity3);
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();
                SomaPedido();

                Beep(1000, 300);
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void SomaPedido()
        {
            try
            {
                decimal TotalPedido = 0;
                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    TotalPedido += Convert.ToDecimal(item.VALORTOTAL);
                }

                lblTotalPedido.Text = TotalPedido.ToString("n2");
                txtTotalPedFech.Text = lblTotalPedido.Text;

                decimal TotalPedidoSelec = Convert.ToDecimal(txtTotalPedFech.Text);
                decimal TotalPagoDesc = Convert.ToDecimal(txtTotalDesconto.Text) + Convert.ToDecimal(txtValorPago.Text);
                txtValorDev.Text = (TotalPedidoSelec - TotalPagoDesc).ToString("n2");
                if (Convert.ToDecimal(txtValorDev.Text) < 0)
                    txtValorDev.Text = "0,00";

                txtTroco.Text = (Convert.ToDecimal(txtValorPago.Text) - (Convert.ToDecimal(txtTotalPedFech.Text) - Convert.ToDecimal(txtTotalDesconto.Text) ) ).ToString("n2");
                
                if (Convert.ToDecimal(txtTroco.Text) < 0)
                    txtTroco.Text = "0,00";
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
           
       }

        private void DGDadosProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para apagar o registro pressione Delete";
        }

        private void DGDadosProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 0)//Excluir
                {
                    if (Util.Apaga_RegistroSenha(this.Name, FrmLogin._IdNivel))
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                if (_IDPEDIDO != -1)
                                {
                                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDPRODPEDIDO));
                                    ListaProdutoPedido(_IDPEDIDO);

                                    if(LIS_PRODUTOSPEDIDOColl.Count == 0)
                                    {
                                        dr = MessageBox.Show("Deseja Excluir o Pedido Nº " + _IDPEDIDO.ToString().PadLeft(6, '0') ,
                                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                                        if (dr == DialogResult.Yes)
                                        {
                                            PEDIDOP.Delete(_IDPEDIDO);
                                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                            Entity = null;
                                            Entity3 = null;
                                            this.Text = "Pedido Balcão";

                                            if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                                               this.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    LIS_PRODUTOSPEDIDOColl.RemoveAt(rowindex);

                                    DGDadosProduto.AutoGenerateColumns = false;
                                    DGDadosProduto.DataSource = null;
                                    DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;

                                    //Vai para ultima linha do grid
                                    DGDadosProduto.CurrentCell = DGDadosProduto.Rows[DGDadosProduto.Rows.Count - 1].Cells[0];
                                }

                                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();
                                SomaPedido();

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");

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

        private void DGDadosProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DGDadosProduto.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;

                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            LIS_PRODUTOSPEDIDOColl.RemoveAt(indice);

                            DGDadosProduto.AutoGenerateColumns = false;
                            DGDadosProduto.DataSource = null;
                            DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;

                            SomaPedido();
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            btnPesquisa_Click(null, null);

                            //Vai para ultima linha do grid
                            DGDadosProduto.CurrentCell = DGDadosProduto.Rows[DGDadosProduto.Rows.Count - 1].Cells[0];
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

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
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

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
            {
                using (FrmCliente frm = new FrmCliente())
                {
                    frm.ShowDialog();
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
            else
            {
                using (FrmCliente2 frm = new FrmCliente2())
                {
                    frm.ShowDialog();
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex > 0)
            {
                
                if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
                {
                    using (FrmBloqueado frm = new FrmBloqueado())
                    {
                        frm.ShowDialog();
                    }
                }
                
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

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            if (txtValorPago.Text == "0,00")
                txtValorPago.Text = string.Empty;
        }

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorPago.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text))
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                    Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Blue");
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorPago.Text);
                    txtValorPago.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPago, "");

                    txtValorDev.Text = ((Convert.ToDecimal(txtTotalPedFech.Text) + Convert.ToDecimal(txtValorAcrescimo.Text)) - Convert.ToDecimal(txtValorPago.Text)).ToString("n2");
                    if (Convert.ToDecimal(txtValorDev.Text) < 0)
                        txtValorDev.Text = "0,00";

                    txtTroco.Text = (Convert.ToDecimal(txtValorPago.Text) - ((Convert.ToDecimal(txtTotalPedFech.Text) - Convert.ToDecimal(txtTotalDesconto.Text)) + Convert.ToDecimal(txtValorAcrescimo.Text))).ToString("n2");

                    if (Convert.ToDecimal(txtTroco.Text) < 0)
                        txtTroco.Text = "0,00";

                }
            }
            else
                txtValorPago.Text = "0,00";

            txtTipoPagamentoDinheiro.Text = (Convert.ToDecimal(txtValorPago.Text) - Convert.ToDecimal(txtTroco.Text)).ToString("n2");

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
        }          

        private string GetNameCliente(int IdCliente)
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            return CLIENTEP.Read(IdCliente).NOME;
        }       

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcDesconto.Text))
                {
                    errorProvider1.SetError(txtPorcDesconto, ConfigMessage.Default.FieldErro);
                    Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcDesconto.Text);
                    txtPorcDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconto, "");

                    txtTotalDesconto.Text =
                                            ((Convert.ToDecimal(txtTotalPedFech.Text) *
                                            Convert.ToDecimal(txtPorcDesconto.Text)) / 100).ToString("n2");

                    SumTotalPedido();


                }
            }
            else
                txtPorcDesconto.Text = "0,00";

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
        }

        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
            if (txtPorcDesconto.Text == "0,00")
                txtPorcDesconto.Text = string.Empty;
        }

        private void txtPorcAcrescimo_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtPorcAcrescimo_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtTotalDesconto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtTotalDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalDesconto.Text))
                {
                    errorProvider1.SetError(txtTotalDesconto, ConfigMessage.Default.FieldErro);
                    Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
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

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
        }

        private void SumTotalPedido()
        {
            try
            {
                if (txtValorPago.Text.Trim() == string.Empty)
                    txtValorPago.Text = "0,00";

                decimal ValorDev = 0;
                ValorDev = (Convert.ToDecimal(txtTotalPedFech.Text) - Convert.ToDecimal(txtTotalDesconto.Text) -
                                 Convert.ToDecimal(txtValorPago.Text));

                ValorDev += Convert.ToDecimal(txtValorAcrescimo.Text);

                txtValorDev.Text = ValorDev.ToString("n2");

                if (Convert.ToDecimal(txtValorDev.Text) < 0)
                {
                    txtTroco.Text = (Convert.ToDecimal(txtValorDev.Text) * -1).ToString("n2");
                    txtValorDev.Text = "0,00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtTotalAcrescimo_Validating(object sender, CancelEventArgs e)
        {
           
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

        private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigPedBalcao frm = new FrmConfigPedBalcao())
            {
                frm.ShowDialog();
            }
        }

        private void BtnSair2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            
        }

        private void txtPorComisVend_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtValComissao_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtNPedido_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nº Pedido, código gerado automaticamente.";
        }
       
        private void ListaProdutoPedido(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPRODPEDIDO");

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

                SumTotalProdutosPedido();

                //Vai para ultima linha do grid
                if (LIS_PRODUTOSPEDIDOColl.Count > 0)
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

            lblTotalPedido.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
            
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

                //Busca Configuração do Pedido Balcão
                CONFIGPEDBALCAOEntity CONFIGPEDBALCAOTy = new CONFIGPEDBALCAOEntity();
                CONFIGPEDBALCAOTy = CONFIGPEDBALCAOP.Read(1);
                DUPLICATARECEBERty.IDLOCALCOBRANCA = CONFIGPEDBALCAOTy.IDLOCALCOBRANCA;
                DUPLICATARECEBERty.IDCENTROCUSTO = CONFIGPEDBALCAOTy.IDCENTROCUSTO;

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

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro técnico:  " + ex.Message);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                }
            }
        }

        private Boolean ValidaDuplicatas()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbFormaPagto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFormaPagto, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (Convert.ToDecimal(txtValorDev.Text) < 1)
            {
                errorProvider1.SetError(txtValorDev, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }    

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaPlanos())
                {
                    if (_IDPEDIDO != -1)
                        PEDIDOP.Save(Entity);

                    tabControlPedidoBalcao.SelectTab(0);
                    txtDescProduto.Focus();
                    Entity = null;
                    _IDMESA = null;
                    ListaProdutoPedido(_IDPEDIDO);
                }

                if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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


        private void btnGravaPedido2_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
            {
                try
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    txtNPedido.Text = _IDPEDIDO.ToString().PadLeft(6, '0');
                }
                catch (Exception ex)
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }            
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
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
                else if (ColumnSelecionada == 1) //editar
                {
                    FrmContasReceber FrmCR = new FrmContasReceber();
                    FrmCR.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                    FrmCR.ShowDialog();

                                        //Lista Duplicatas do Pedido
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
		

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PEDIDOColl.Count > 0)
            {
                int ColumnSelecionada = e.ColumnIndex;
                if (ColumnSelecionada > 1)
                {
                    string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                    Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOEntity>(orderBy);

                    LIS_PEDIDOColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_PEDIDOColl;
                }

            }
        }

        private void laserJatoDeTintaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
              

                if (!chkTelaBloqueada.Checked)
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    ImprimirPedidoLJ();
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

        private void ImprimirPedidoLJ()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

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
                e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);

                if (CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == "N" || CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == string.Empty)
                    e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


                e.Graphics.DrawString("Nº PEDIDO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
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
                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                   
                     e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

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

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 80);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 115);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalPedFech.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
               // e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOCollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;
                //e.Graphics.DrawString(txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);


                e.Graphics.DrawString("Total Pedido:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalPedFech.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

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
                e.Graphics.DrawString("Assinatura do Cliente: _________________________________________________", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha + 10);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgErroPrint, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void matricialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    PrintTicketNormal();
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

        private void PrintTicketNormal()
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

                        string Traço = string.Empty.PadRight(40, '=');
                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.RemoverAcentos((Util.LimiterText(EMPRESATy.NOMEFANTASIA, 40))));
                        imp.ImpLF("CNPJ: " + EMPRESATy.CNPJCPF);
                        imp.ImpLF(Util.RemoverAcentos(Util.LimiterText(EMPRESATy.ENDERECO, 40)));

                        if (EMPRESATy.BAIRRO != null)
                            imp.ImpLF(Util.RemoverAcentos(Util.LimiterText("Bairro: " + EMPRESATy.BAIRRO, 40)));

                        imp.ImpLF(Util.LimiterText("Fone: " + EMPRESATy.TELEFONE, 40));
                        imp.ImpLF(Util.RemoverAcentos((Util.LimiterText(EMPRESATy.CIDADE, 35) + "-" + EMPRESATy.UF)));
                        imp.ImpLF("Data: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        imp.ImpLF(("Pedido N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0')));
                        imp.ImpLF(Traço);

                        imp.ImpLF(Util.RemoverAcentos("CLIENTE: " + Util.LimiterText(cbCliente.Text, 40)));

                        if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                            imp.ImpLF(Util.RemoverAcentos("VENDEDOR.: " + Util.LimiterText(cbFuncionario.Text, 40)));

                        imp.ImpLF(Traço);
                        imp.Pula(1);
                        imp.ImpCol(0, "Produto");
                        imp.ImpCol(28, "Qtd");
                        imp.ImpCol(36, "Total");
                        imp.Pula(1);
                        imp.ImpLF(Traço);

                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                        {
                            string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2").PadLeft(9, ' ');
                            string NOMEPRODUTO = item.NOMEPRODUTO.PadRight(40, ' ');
                            imp.ImpCol(0, Util.RemoverAcentos(Util.LimiterText(NOMEPRODUTO, 40)));
                            imp.Pula(1);
                            imp.ImpCol(28, item.QUANTIDADE.ToString().PadLeft(3, ' '));
                            imp.ImpCol(31, ValorTotal);
                            imp.Pula(1);
                        }

                        imp.Pula(1);
                        imp.ImpLF("No Itens: " + LIS_PRODUTOSPEDIDOColl.Count.ToString());
                        imp.ImpLF(Traço);

                        imp.ImpCol(0, "SUBTOTAL......:");
                        imp.ImpCol(20, txtTotalPedFech.Text.PadLeft(9, ' '));
                        imp.Pula(1);

                        imp.ImpCol(0, "DESCONTO......:");
                        imp.ImpCol(20, txtTotalDesconto.Text.PadLeft(9, ' '));
                        imp.Pula(1);

                        //imp.ImpCol(0, "ACRESCIMO.....:");
                        //imp.ImpCol(20, txtTotalAcrescimo.Text.PadLeft(9, ' '));
                        //imp.Pula(1);

                        if (txtValorDev.Text == "0,00")
                        {
                            imp.ImpCol(0, "TOTAL.........:");
                            imp.ImpCol(20, txtTotalPedFech.Text.PadLeft(9, ' '));
                        }
                        else
                        {
                            imp.ImpCol(0, "TOTAL.........:");
                            imp.ImpCol(20, txtValorDev.Text.PadLeft(9, ' '));
                        }

                        imp.Pula(2);

                        //Duplicata
                        if (LIS_DUPLICATARECEBERColl.Count > 0)
                        {
                            imp.ImpLF("DUPLICATA");
                            imp.ImpLF(Traço);

                            imp.ImpCol(0, "No Parc.");
                            imp.ImpCol(15, "Dt.Vencto");
                            imp.ImpCol(28, "Valor");
                            imp.Pula(1);

                            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                            {
                                imp.ImpCol(0, item.NUMERO);
                                imp.ImpCol(15, Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"));
                                imp.ImpCol(28, Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2").PadLeft(9, ' '));
                                imp.Pula(1);
                            }
                        }

                        imp.Pula(2);

                        if (BmsSoftware.ConfigSistema1.Default.msg1ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg1ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg2ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg2ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg3ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg3ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg4ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg4ticket));

                        imp.Pula(2);

                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.RemoverAcentos("CLIENTE: " + Util.LimiterText(cbCliente.Text, 40)));
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

                        string Traço = string.Empty.PadRight(40, '=');
                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.LimiterText(EMPRESATy.NOMECLIENTE, 40));
                        imp.ImpLF(Util.LimiterText("CNPJ: " + EMPRESATy.CNPJCPF + "IE:" + EMPRESATy.IE, 40));
                        imp.ImpLF(Util.LimiterText(EMPRESATy.ENDERECO, 40));

                        if (EMPRESATy.BAIRRO != null)
                            imp.ImpLF(Util.LimiterText("Bairro: " + EMPRESATy.BAIRRO, 40));

                        imp.ImpLF(Util.LimiterText("Fone: " + EMPRESATy.TELEFONE, 40));
                        imp.ImpLF((Util.LimiterText(EMPRESATy.CIDADE, 35) + "-" + EMPRESATy.UF));
                        imp.ImpLF("Data: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        imp.ImpLF(("Pedido N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0')));
                        imp.ImpLF(Traço);

                        imp.ImpLF("CLIENTE: " + Util.LimiterText(cbCliente.Text, 40));
                        imp.ImpLF("VENDEDOR.: " + Util.LimiterText(cbFuncionario.Text, 40));
                        imp.ImpLF(Traço);

                        imp.ImpLF("Produto          " + "          Qto" + "     Total");
                        imp.ImpLF(Traço);

                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                        {
                            string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                            imp.ImpLF(Util.LimiterText("Cod:" + item.IDPRODUTO.ToString() + "-" + item.NOMEPRODUTO, 40));
                            imp.Pula(1);
                            imp.ImpCol(15, item.QUANTIDADE.ToString().PadLeft(8, ' '));
                            imp.ImpCol(32, ValorTotal.PadLeft(8, ' '));
                            imp.Pula(1);
                        }

                        imp.Pula(1);
                        imp.ImpLF("No Itens: " + LIS_PRODUTOSPEDIDOColl.Count.ToString());
                        imp.ImpLF(Traço);

                        imp.ImpCol(0, "SUBTOTAL:");
                        imp.ImpCol(32, txtTotalPedFech.Text.PadLeft(8, ' '));
                        imp.Pula(1);

                        imp.ImpCol(00, "DESCONTO:");
                        imp.ImpCol(32, txtTotalDesconto.Text.PadLeft(8, ' '));
                        imp.Pula(1);

                        //imp.ImpCol(0, "ACRESCIMO:");
                        //imp.ImpCol(32, txtTotalAcrescimo.Text.PadLeft(8, ' '));
                        //imp.Pula(1);

                        imp.ImpCol(0, "TOTAL:");
                        imp.ImpCol(32, txtValorDev.Text.PadLeft(8, ' '));
                        imp.Pula(2);

                        //Duplicata
                        if (LIS_DUPLICATARECEBERColl.Count > 0)
                        {
                            imp.ImpLF("DUPLICATA");
                            imp.ImpLF(Traço);

                            imp.ImpCol(0, "No Parc.");
                            imp.ImpCol(15, "Dt.Vencto");
                            imp.ImpCol(32, "Valor");
                            imp.Pula(1);

                            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                            {
                                imp.ImpCol(0, item.NUMERO);
                                imp.ImpCol(15, Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"));
                                imp.ImpCol(32, Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2").PadLeft(8, ' '));
                                imp.Pula(1);
                            }
                        }

                        imp.Pula(2);

                        if (BmsSoftware.ConfigSistema1.Default.msg1ticket.Length > 0)
                            imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg1ticket);

                        if (BmsSoftware.ConfigSistema1.Default.msg2ticket.Length > 0)
                            imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg2ticket);

                        if (BmsSoftware.ConfigSistema1.Default.msg3ticket.Length > 0)
                            imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg3ticket);

                        if (BmsSoftware.ConfigSistema1.Default.msg4ticket.Length > 0)
                            imp.ImpLF(BmsSoftware.ConfigSistema1.Default.msg4ticket);

                        imp.Pula(2);

                        imp.ImpLF(Traço);
                        imp.ImpLF("CLIENTE: " + Util.LimiterText(cbCliente.Text, 40));
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

      

        private void boletaBancáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoBalcao.SelectTab(1);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

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
                                //SICOOB
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

        private void viaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoBalcao.SelectTab(1);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

            }
            else
                ImprimirDuplicata1Via();
        }

        int _CodDuplicata = -1;
        private void ImprimirDuplicata1Via()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
                    printDocument1.DefaultPageSettings.PaperSize = new
                    System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                    objPrintPreview.Document = printDocument1;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

        private void viasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoBalcao.SelectTab(1);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);
            }
            else
                ImprimirDuplicata2Vias();
        }

        private void ImprimirDuplicata2Vias()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument3;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
                    objPrintPreview.Document = printDocument3;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

        private void compostaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabControlPedidoBalcao.SelectTab(1);
                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);
            }
            else
                ImprimirDuplicata1ViaComposta();
        }

        private void ImprimirDuplicata1ViaComposta()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument4;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument4.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                objPrintPreview.Document = printDocument4;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
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
                CLIENTEEntity ClienteTy = new CLIENTEEntity();
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
            catch (Exception ex)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void DataGriewDados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PEDIDOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                    tabControlPedidoBalcao.SelectTab(0);

                    Entity = PEDIDOP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDO);
                    txtNPedido.Focus();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);                  
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                     if (Util.Apaga_RegistroSenha(this.Name, FrmLogin._IdNivel))
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);

                                //Delete Financeiro                          
                                ApagaDuplicata(CodigoSelect);

                                //Excluir Movimentaçao do caixa
                                ExcluirCaixa(CodigoSelect.ToString().PadLeft(6, '0'));

                                //Apaga movimentação de produto
                                ApagaProdutoPedido(CodigoSelect);

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
                else if (ColumnSelecionada == 2)//Print
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOColl[rowindex].IDPEDIDO);
                    Entity = PEDIDOP.Read(CodigoSelect);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNPedido.Text);

                    button1_Click_1(null, null);
                    Entity = null;
                    Entity3 = null;
                }
            }
        }

        private void ApagaDuplicata(int CodPedido)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "=", "PD" + CodPedido.ToString()));
                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERPgtoColl = new LIS_DUPLICATARECEBERCollection();
                LIS_DUPLICATARECEBERPgtoColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERPgtoColl)
                {
                    DUPLICATARECEBERP.Delete(Convert.ToInt32(item.IDDUPLICATARECEBER));
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ApagaProdutoPedido(int IDPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                foreach (var item in LIS_PRODUTOSPEDIDOColl)
                {
                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro técnico: " +EX.Message);
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_PEDIDOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PEDIDOColl.Count.ToString();
        }

        private void txtValorPago_Leave(object sender, EventArgs e)
        {
            if (txtValorPago.Text == string.Empty)
                txtValorPago.Text = "0,00";


            if (Convert.ToDecimal(txtValorPago.Text) > 0  && _IDPEDIDO != -1)
            {
               if(CONFIGPEDBALCAOTy.FLAGENTRADACAIXA == "S")
                {
                    decimal ValorPago = Convert.ToDecimal(txtValorPago.Text);
                    
                    DialogResult dr = MessageBox.Show("Deseja fazer entrada no caixa o valor de " + ValorPago.ToString("n2") + " ?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {

                        CAIXAEntity CaixaTy = new CAIXAEntity();
                        CAIXAProvider CaixaP = new CAIXAProvider();

                        CaixaTy.IDCAIXA = _IDCAIXA;
                        CaixaTy.IDTIPODUPLICATA = CONFIGPEDBALCAOTy.IDTIPOPAGTO;
                        CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
                        CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        CaixaTy.IDTIPOMOVCAIXA = 1;// Credito
                        CaixaTy.IDCENTROCUSTOS = CONFIGPEDBALCAOTy.IDCENTROCUSTO;

                        CaixaTy.NDOCUMENTO = "PD" + txtNPedido.Text;
                        CaixaTy.OBSERVACAO = "Pedido Balcão nº " + "PD" + txtNPedido.Text + " Cliente: " + cbCliente.SelectedValue + " - " + GetNameCliente(Convert.ToInt32(cbCliente.SelectedValue));

                        try
                        {
                            _IDCAIXA = CaixaP.Save(CaixaTy);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Não foi possível fazer o movimento de caixa!");
                        }
                    }
                }

                cbStatus.SelectedValue = 61;//Pago
            }   
            else
                cbStatus.SelectedValue = 47;//Aberto

            if (_IDPEDIDO != -1)
             PEDIDOP.Save(Entity);
        }

        private void btnCaixa_Click_1(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void txtTotalDesconto_Enter(object sender, EventArgs e)
        {
            if (txtTotalDesconto.Text == "0,00")
                txtTotalDesconto.Text = string.Empty;
        }

        private void txtTotalAcrescimo_Enter(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                Util.ExibirMSg("Antes de adicionar o Caixa é necessário gravar o Pedido!", "Red");
            }
            else
            {
                if (Convert.ToDecimal(txtValorPago.Text) <= 0)
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
                CaixaTy.IDTIPODUPLICATA = 2;//Dinheiro
                CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
                CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                CaixaTy.IDTIPOMOVCAIXA = 1;// Credito
            
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

        private void ExcluirCaixa(string Pedido)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", "PD" + Pedido));
                CAIXAColl.Clear();
                CAIXAColl = CAIXAP.ReadCollectionByParameter(RowRelatorio);

                if (CAIXAColl.Count > 0)
                {
                    CAIXAP.Delete(CAIXAColl[0].IDCAIXA);

                    MessageBox.Show("Movimentação de caixa excluído com sucesso!");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Erro técnio: " + ex.Message);
            }
        }

        private Boolean VerificaExisteCaixa(string Pedido)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", "PD" + Pedido));
                CAIXAColl.Clear();
                CAIXAColl = CAIXAP.ReadCollectionByParameter(RowRelatorio);

                if (CAIXAColl.Count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show("Erro técnio: " + ex.Message);
            }

            return result;
        }



        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            
        }

      
        private void chkCodRef_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {

        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
            }
            else
            {
                FrmVendaGrupo FrmVendaG = new FrmVendaGrupo();
                FrmVendaG.LIS_PEDIDOFiltroPrintColl = LIS_PEDIDOColl;
                FrmVendaG.ShowDialog();
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

        private void btnCadLocaCobranca_Click(object sender, EventArgs e)
        {
           
        }

     

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário Gravar o Pedido!",
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
                      
                        //Salva a forma de pagamento no fechamento
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

        private void produtosPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoCliente frm = new FrmProdutoCliente())
            {
                frm.ShowDialog();
            }
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!chkTelaBloqueada.Checked)
                {
                    CONFIGPEDBALCAOEntity CONFIGPEDBALCAOTy = new CONFIGPEDBALCAOEntity();
                    CONFIGPEDBALCAOTy = CONFIGPEDBALCAOP.Read(1);

                    //Salva Pedido
                    if (_IDPEDIDO != -1)
                        PEDIDOP.Save(Entity);

                    if (CONFIGPEDBALCAOTy != null)
                    {
                        if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "0")   //Modelo 1 ( Porta Serial ) 
                            matricialToolStripMenuItem1_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "1") // Modelo 2 
                            ticketModelo2ToolStripMenuItem_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "2")//- Modelo 3 
                            ticketModelo3ToolStripMenuItem_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "3") //Modelo 4 ( Porta Serial )
                            ticketModelo4PortaSerialToolStripMenuItem_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "4") //Modelo 5 
                            ticketModelo5ToolStripMenuItem_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "5") //Modelo 6 ( Carta/A4)
                            modelo6ToolStripMenuItem_Click(null, null);
                        else if (CONFIGPEDBALCAOTy.TIPOMODELOTICKET.TrimEnd().TrimStart() == "6") // - Modelo 7 ( Carta/A4) Ecônomic 
                            modelo7CartaA4EcônomicoToolStripMenuItem_Click(null, null);

                        if (BmsSoftware.ConfigPedidoBalcao.Default.AbreGaveta.Trim() == "S")
                            AbreGaveta();
                    }

                    BloqueoTela();
                }
                else
                {
                    string msgerro = "Tela bloqueada!";
                    errorProvider1.SetError(chkTelaBloqueada, msgerro);
                    Util.ExibirMSg(msgerro, "Red");
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public ACBrFramework.ACBrDevice Device { get; set; }
        private void AbreGaveta()
        {
            try
            {
               
                ACBrFramework.ECF.ACBrECF Abre_Gaveta2 = new ACBrFramework.ECF.ACBrECF();
                
                Device.Porta = BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALLOCAL;
               // if (!Abre_Gaveta2.GavetaAberta)
                    Abre_Gaveta2.AbreGaveta();               

       // ACBr.Net.ACBrECF AbreGaveta_ = new ACBr.Net.ACBrECF();
       // AbreGaveta_.Ativo = true;
       //  AbreGaveta_.GavetaSinalInvertido = true;

                //if (!AbreGaveta_.GavetaAberta)
                //  AbreGaveta_.AbreGaveta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Abrir Gaveta!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void listaGeralProdutosPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                FrmListaProdPedido FrmLSP = new FrmListaProdPedido();
                FrmLSP.LIS_PEDIDOColl = LIS_PEDIDOColl;
                FrmLSP.ShowDialog();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidos FrmP = new FrmProdutosMaisVendidos();
            FrmP.ShowDialog();
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecInicial2_Click(object sender, EventArgs e)
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
        private void bntDateSelecFinal2_Click(object sender, EventArgs e)
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

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void ticketModelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    PrintTicketModelo2();
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


        private void PrintTicketModelo2()
        {
            try
            {
                Ticket ticket = new Ticket();
                
                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                string NomeEmpresa = Util.LimiterText(EMPRESATy.NOMECLIENTE, 35);
                NomeEmpresa = Util.centeredString(NomeEmpresa, 35);
                ticket.AddHeaderLine(NomeEmpresa);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 35));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 32) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 35));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 35));

                ticket.AddSubHeaderLine("Pedido Nº" + Entity.IDPEDIDO.ToString().PadLeft(6, '0'));
               
                //Exibe endereço do cliente
                if (Convert.ToInt32(cbCliente.SelectedValue) > 1)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
                    LIS_CLIENTECollection LIS_CLIENTEColl_T = new LIS_CLIENTECollection();
                    LIS_CLIENTEColl_T = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_CLIENTEColl_T.Count > 0)
                    {
                        ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                        ticket.AddSubHeaderLine(LIS_CLIENTEColl_T[0].ENDERECO1 + " " + LIS_CLIENTEColl_T[0].NUMEROENDER + " " +
                                                LIS_CLIENTEColl_T[0].COMPLEMENTO1 + " " + LIS_CLIENTEColl_T[0].BAIRRO1 + " " +
                                                LIS_CLIENTEColl_T[0].MUNICIPIO + " " +
                                                LIS_CLIENTEColl_T[0].UF + " " + LIS_CLIENTEColl_T[0].TELEFONE1 + " " +
                                                LIS_CLIENTEColl_T[0].TELEFONE2);
                    }
                }
                else
                    ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);

                ticket.AddSubHeaderLine("FUNCIONÁRIO: " + cbFuncionario.Text);

                if(BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
                    ticket.AddSubHeaderLine("MESA Nº:  " + _IDMESA.ToString().PadLeft(3, '0'));

                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                decimal SUBTOTAL = Convert.ToDecimal(lblTotalPedido.Text);
                ticket.AddTotal("SUBTOTAL", SUBTOTAL.ToString("n2"));
                ticket.AddTotal("DESCONTO", txtTotalDesconto.Text);

                if (Convert.ToDecimal(txtValorAcrescimo.Text) > 0)
                    ticket.AddTotal("ACRÉSCIMO " + txtPorcAcrescimo.Text +  "%", txtValorAcrescimo.Text);

                ticket.AddTotal("TOTAL", (Convert.ToDecimal(txtTotalPedFech.Text) + Convert.ToDecimal(txtValorAcrescimo.Text)).ToString("n2"));
                ticket.AddTotal("PAGO", txtValorPago.Text);
                ticket.AddTotal("TROCO", txtTroco.Text); 

                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg1ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg2ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg3ticket);
                ticket.AddFooterLine(BmsSoftware.ConfigSistema1.Default.msg4ticket);

                if (txtObservacao.Text.Trim() != string.Empty)
                {
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine(txtObservacao.Text);
                }

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

        private void ticketModelo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {

                if (!chkTelaBloqueada.Checked)
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    using (FrmRelatTicket frm = new FrmRelatTicket())
                    {
                        frm.IDPEDIDO = _IDPEDIDO;
                        frm.ShowDialog();
                    }
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

        private void ticketModelo4PortaSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    _IDPEDIDO = PEDIDOP.Save(Entity);
                    PrintTicketModelo4();
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

        private void PrintTicketModelo4()
        {
            try
            {
                ImprimeTexto imp = new ImprimeTexto();
                string PortaImpressora = BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALLOCAL;

                if (PortaImpressora.TrimEnd().TrimStart() != string.Empty)
                {
                    try
                    {
                        //System.IO.Ports.SerialPort Serial = new System.IO.Ports.SerialPort();
                        //Serial.Close();
                        ////ou a porta que vc quer
                        //Serial.PortName = PortaImpressora; 
                        //Serial.Open();

                        imp.Inicio(PortaImpressora);

                        //Dados da empresa
                        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                        EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                        string Traço = string.Empty.PadRight(48, '-');


                        imp.ImpLF("COMPROVANTE SEM VALOR FISCAL");
                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.RemoverAcentos((Util.LimiterText(EMPRESATy.NOMEFANTASIA, 50))));
                        imp.ImpLF("CNPJ: " + EMPRESATy.CNPJCPF);
                        imp.ImpLF(Util.RemoverAcentos(Util.LimiterText(EMPRESATy.ENDERECO, 50)));

                        if (EMPRESATy.BAIRRO != null)
                            imp.ImpLF(Util.RemoverAcentos(Util.LimiterText("Bairro: " + EMPRESATy.BAIRRO, 50)));

                        imp.ImpLF(Util.LimiterText("Fone: " + EMPRESATy.TELEFONE, 50));
                        imp.ImpLF(Util.RemoverAcentos((Util.LimiterText(EMPRESATy.CIDADE, 35) + "-" + EMPRESATy.UF)));
                        imp.ImpLF("Data: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        imp.ImpLF(("Pedido N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0')));
                        imp.ImpLF(Traço);

                        imp.ImpLF(Util.RemoverAcentos("CLIENTE: " + Util.LimiterText(cbCliente.Text, 50)));

                        if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                            imp.ImpLF(Util.RemoverAcentos("VENDEDOR.: " + Util.LimiterText(cbFuncionario.Text, 50)));

                        imp.ImpLF(Traço);
                        imp.Pula(1);
                        imp.ImpCol(0, "Codigo");
                        imp.ImpCol(8, "Produto");
                        imp.Pula(1);
                        imp.ImpCol(0, "Qtd");
                        imp.ImpCol(10, "P.Unitario");
                        imp.ImpCol(20, "Total");
                        imp.Pula(1);
                        imp.ImpLF(Traço);

                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                        {
                            string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2").PadLeft(9, ' ');
                            string NOMEPRODUTO = item.NOMEPRODUTO.PadRight(40, ' ');

                            imp.ImpCol(0, item.IDPRODUTO.ToString().PadLeft(5, '0'));
                            imp.ImpCol(8, Util.RemoverAcentos(Util.LimiterText(NOMEPRODUTO, 32)));
                            imp.Pula(1);
                            imp.ImpCol(0, item.QUANTIDADE.ToString().PadLeft(3, ' '));
                            imp.ImpCol(10, Convert.ToDecimal(item.VALORUNITARIO).ToString("n2").PadLeft(3, ' '));
                            imp.ImpCol(20, ValorTotal);
                            imp.Pula(1);
                        }

                        imp.Pula(1);
                        imp.ImpLF("N. Itens: " + LIS_PRODUTOSPEDIDOColl.Count.ToString());
                        imp.ImpLF(Traço);

                        imp.ImpCol(0, "SUBTOTAL......:");
                        imp.ImpCol(20, txtTotalPedFech.Text.PadLeft(9, ' '));
                        imp.Pula(1);

                        imp.ImpCol(0, "DESCONTO......:");
                        imp.ImpCol(20, txtTotalDesconto.Text.PadLeft(9, ' '));
                        imp.Pula(1);

                        //imp.ImpCol(0, "ACRESCIMO.....:");
                        //imp.ImpCol(20, txtTotalAcrescimo.Text.PadLeft(9, ' '));
                        //imp.Pula(1);

                        imp.ImpCol(0, "VL PAGO......:");
                        imp.ImpCol(20, txtValorPago.Text.PadLeft(9, ' '));
                        imp.Pula(1);

                        imp.ImpCol(0, "TOTAL.........:");
                        imp.ImpCol(20, txtValorDev.Text.PadLeft(9, ' '));

                        imp.Pula(2);

                        //Duplicata
                        if (LIS_DUPLICATARECEBERColl.Count > 0)
                        {
                            imp.ImpLF("DUPLICATA");
                            imp.ImpLF(Traço);

                            imp.ImpCol(0, "No Parc.");
                            imp.ImpCol(15, "Dt.Vencto");
                            imp.ImpCol(28, "Valor");
                            imp.Pula(1);

                            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                            {
                                imp.ImpCol(0, item.NUMERO);
                                imp.ImpCol(15, Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"));
                                imp.ImpCol(28, Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2").PadLeft(9, ' '));
                                imp.Pula(1);
                            }
                        }

                        imp.Pula(2);

                        if (BmsSoftware.ConfigSistema1.Default.msg1ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg1ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg2ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg2ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg3ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg3ticket));

                        if (BmsSoftware.ConfigSistema1.Default.msg4ticket.Length > 0)
                            imp.ImpLF(Util.RemoverAcentos(BmsSoftware.ConfigSistema1.Default.msg4ticket));

                        imp.Pula(2);

                        imp.ImpLF(Traço);
                        imp.ImpLF(Util.RemoverAcentos("CLIENTE: " + Util.LimiterText(cbCliente.Text, 50)));
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

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaPV1 FrmVendaDiaria = new FrmVendaDiariaPV1();
            FrmVendaDiaria.ShowDialog();
        }

        private void PctProduto_Click(object sender, EventArgs e)
        {

        }

        private void ticketModelo5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else if (!chkTelaBloqueada.Checked)
            {
                PrintTicketModelo5();
                BloqueoTela();
            }
            else
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
            }
        }

        private void PrintTicketModelo5()
        {
            try
            {
                Ticket ticket = new Ticket();

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                ticket.FontSize = 7;
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMEFANTASIA, 20));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 20));
                ticket.AddHeaderLine((Util.LimiterText(EMPRESATy.CIDADE, 20) + "-" + EMPRESATy.UF));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 20));

                ticket.AddSubHeaderLine(("Pedido N." + Entity.IDPEDIDO.ToString().PadLeft(6, '0')));

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    ticket.AddSubHeaderLine("CLIENTE: " + Util.LimiterText(cbCliente.Text, 20));
                else
                    ticket.AddSubHeaderLine("CLIENTE: ");

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    ticket.AddSubHeaderLine("VENDEDOR.: " + Util.LimiterText(cbFuncionario.Text, 20));
                else
                    ticket.AddSubHeaderLine("VENDEDOR.: ");

                ticket.AddSubHeaderLine((DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()));

                //   ticket.AddItem("Quant", "Produto", "Total");

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO + " " + Util.LimiterText(ValorTotal, 20), string.Empty);
                }

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalPedFech.Text);

                ticket.AddFooterLine("SUBTOTAL....: " + SUBTOTAL.ToString("n2"));
                ticket.AddFooterLine("DESCONTO....: " + txtTotalDesconto.Text);
                //txtValorDev ticket.AddFooterLine("ACRESCIMO...: " + txtTotalAcrescimo.Text);
                ticket.AddFooterLine("TOTAL.......: " + txtValorDev.Text);
                ticket.AddFooterLine("PAGO........: " + txtValorPago.Text);
                ticket.AddFooterLine("TROCO.......: " + txtTroco.Text);
                

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

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
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

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
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

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
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

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
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

        private void vendaPorFormaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaPorFormaPagto frm = new FrmVendaPorFormaPagto())
            {
                frm.ShowDialog();
            }
        }

        private void vendasPorOutrosTiposDePagamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmOutrosTipodePagamento frm = new FrmOutrosTipodePagamento())
            {
                frm.ShowDialog();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void buscarClientePorTelefoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchClienteTelefone frm = new FrmSearchClienteTelefone())
            {
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    cbCliente.SelectedValue = result;
                    tabControlPedidoBalcao.SelectTab(1);
                }
            }
        }

        private void modelo6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                txtCriterioPesquisa.Focus();
            }
            else
            {
              
                 PEDIDOP.Save(Entity);
                 ImprimirModelo6();

            }
        }

        private void ImprimirModelo6()
        {
            using (FrmRelatPedidoSimples frm = new FrmRelatPedidoSimples())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void ImprimirModelo7()
        {
            using (FrmRelatPedidoVendas2 frm = new FrmRelatPedidoVendas2())
            {
                PEDIDOP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDO = _IDPEDIDO;
                frm.ShowDialog();
            }
        }

        private void modelo7CartaA4EcônomicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                txtCriterioPesquisa.Focus();
            }
            else
            {

                PEDIDOP.Save(Entity);
                ImprimirModelo7();

            }
        }

        private void exportarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PEDIDOColl.Count < 2)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoBalcao.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                Entity = null;
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

                                        //grava dados do Pedido
                                        sw.WriteLine("P" + ";" + NumPedido + ";" + DataPedido + ";" + IDCliente + ";" + CPFVENDEDOR + ";" + IDFORMAPAGAMENTO + ";" + IDLOCALCOBRANCA + ";" + IDSTATUS);

                                        //Dados do Produto
                                        RowRelatorio.Clear();
                                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", item.IDPEDIDO.ToString()));
                                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTO");

                                        int ContadorItens = 1;
                                        if (LIS_PRODUTOSPEDIDOColl.Count > 0)
                                        {
                                            foreach (var item_Produto in LIS_PRODUTOSPEDIDOColl)
                                            {
                                                string CodProduto = Convert.ToInt32(item_Produto.IDPRODUTO).ToString().PadLeft(6, '0');
                                                string QuantProduto = Convert.ToDecimal(item_Produto.QUANTIDADE).ToString("n2").PadLeft(10, '0');
                                                string PrecoUnitario = Convert.ToDecimal(item_Produto.VALORUNITARIO).ToString("n2").PadLeft(10, '0');

                                                //Grava Dados do produto do pedido
                                                sw.WriteLine(ContadorItens.ToString().PadLeft(6, '0') + ";" + NumPedido + ";" + CodProduto + ";" + QuantProduto + ";" + PrecoUnitario);
                                                ContadorItens++;
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
                                    EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                                    EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                                    EMPRESATy = EMPRESAP.Read(1);

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

        private void carnêDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
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
                if (Convert.ToDecimal(txtValorPago.Text) <= 0)
                {
                    errorProvider1.SetError(label25, ConfigMessage.Default.CampoObrigatorio);
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

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void btnCadStatus_Click(object sender, EventArgs e)
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DataGriewDados_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPorcAcrescimo_Validating_1(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcAcrescimo.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcAcrescimo.Text))
                {
                    errorProvider1.SetError(txtPorcAcrescimo, ConfigMessage.Default.FieldErro);
                    Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcAcrescimo.Text);
                    txtPorcAcrescimo.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcAcrescimo, "");

                    txtValorAcrescimo.Text = ((Convert.ToDecimal(txtTotalPedFech.Text) * Convert.ToDecimal(txtPorcAcrescimo.Text)) / 100).ToString("n2");

                    SumTotalPedido();


                }
            }
            else
                txtPorcAcrescimo.Text = "0,00";

            if (_IDPEDIDO != -1)
                PEDIDOP.Save(Entity);
        }

        private void mudançaDeMesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDMESA == -1)
            {
                String MsgErro = "Mesa Não Selecionada!";
                Util.ExibirMSg(MsgErro, "Red");
            }
            else
            {
                string NumeroMesaDestino = InputBox("Nº Mesa Destino", ConfigSistema1.Default.NomeEmpresa, "");
                if (NumeroMesaDestino.Trim() != string.Empty)
                {
                    if (NumeroMesaDestino.ToString().PadLeft(3, '0') == _IDMESA.ToString().PadLeft(3, '0'))
                    {
                        MessageBox.Show("Mesa Origem com Mesmo Nº da Mesa Destino!");
                    }
                    else
                        MudancaMesa(NumeroMesaDestino);
                }
            }
        }

        private void MudancaMesa(string NumeroMesaDestino)
        {
            try
            {

                if (Convert.ToInt32(NumeroMesaDestino) > 0 && Convert.ToInt32(NumeroMesaDestino) < 97)
                {
                    if (LIS_PRODUTOSPEDIDOColl.Count > 0)
                    {
                        //Pesquisa da mesa Destino
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDMESA", "System.Int32", "=", NumeroMesaDestino.ToString()));
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "47")); //aberta
                        LIS_PEDIDOCollection LIS_PEDIDO_M_Coll = new LIS_PEDIDOCollection();
                        LIS_PEDIDO_M_Coll = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);
                        if (LIS_PEDIDO_M_Coll.Count > 0)
                        {
                            //Adiciona Produtos na mesa destino
                            AdicionaProdutoMesaDestino(Convert.ToInt32(LIS_PEDIDO_M_Coll[0].IDPEDIDO), Convert.ToInt32(_IDMESA), NumeroMesaDestino);
                        }
                        else
                        {
                            MessageBox.Show("Mesa Nº: " + NumeroMesaDestino.ToString().PadLeft(3, '0') + " Não Está Aberta!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não Existem Produtos na Mesa Nº: " + _IDMESA.ToString().PadLeft(3, '0'));
                    }
                    
                 }
                else
                    MessageBox.Show("Mesa Não Localizada!");
            }
            catch (Exception ex)
            {

               MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void AdicionaProdutoMesaDestino( int IDPEDIDODESTINO, int MesaOrigem, string MesaDestino)
        {
            try
            {

                int contador = 0;
                foreach (var item in LIS_PRODUTOSPEDIDOColl)
                {
                    PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy_M = new PRODUTOSPEDIDOEntity();
                    PRODUTOSPEDIDOTy_M.IDPRODPEDIDO = -1;
                    PRODUTOSPEDIDOTy_M.QUANTIDADE = item.QUANTIDADE;
                    PRODUTOSPEDIDOTy_M.IDPRODUTO = item.IDPRODUTO;
                    PRODUTOSPEDIDOTy_M.VALORUNITARIO= item.VALORUNITARIO;
                    PRODUTOSPEDIDOTy_M.VALORTOTAL = item.VALORUNITARIO * item.QUANTIDADE;
                    PRODUTOSPEDIDOTy_M.IDPEDIDO = IDPEDIDODESTINO;
                    PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy_M);
                    contador++;
                }

                MessageBox.Show("Foram Adicionado(s) : " + contador + "  Produto(s) na Mesa Nº: " + MesaDestino.ToString().PadLeft(3, '0'));
                FechaMesaAtual();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message,
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error,
                                 MessageBoxDefaultButton.Button1);
            }
        }

        //Exclui o pedido apos a tranferencia de mesa
        private void FechaMesaAtual()
        {
            try
            {
                //Deleta os produtos da mesa transferida
                foreach (var item in LIS_PRODUTOSPEDIDOColl)
                {
                    PRODUTOSPEDIDOP.Delete(Convert.ToInt32(item.IDPRODPEDIDO));
                }

                //Deleta o pedido da Mesa transferida
                PEDIDOP.Delete(_IDPEDIDO);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message,
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error,
                                 MessageBoxDefaultButton.Button1);
            }
        }

        private void mkdHoraInicial_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraInicial.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraInicial, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void mkdHoraFinal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraFinal.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraFinal.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraFinal, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void vendasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaVendedorPedNormal frm = new FrmVendaVendedorPedNormal())
            {
                frm.ShowDialog();
            }
        }
    }
}
