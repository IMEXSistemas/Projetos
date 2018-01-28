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
using BmsSoftware.Modulos.Vendas;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Relatorio;
using System.Net.Mail;
using System.Net;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmPedidoFesta : Form
    {
        string FLAGPEDBAIXAESTOQUE = string.Empty;
        string FLAGCOMISSAO = string.Empty;
        Boolean EDITREGISTRO = false;

        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        SERVICOCollection SERVICOColl = new SERVICOCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LIS_PEDIDOFESTACollection LIS_PEDIDOFESTAColl = new LIS_PEDIDOFESTACollection();

        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();

        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        SALAOFESTACollection SALAOFESTAColl = new SALAOFESTACollection();
        FESTASCollection FESTASColl = new FESTASCollection();
        TEMACollection TEMAColl = new TEMACollection();

        PEDIDOFESTAProvider PEDIDOFESTAP = new PEDIDOFESTAProvider();
        LIS_PEDIDOFESTAProvider LIS_PEDIDOFESTAP = new LIS_PEDIDOFESTAProvider();

        PRODUTOSPEDFESTAProvider PRODUTOSPEDFESTAP = new PRODUTOSPEDFESTAProvider();

        LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
      
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();      
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        MENSAGEMProvider MENSAGEMP = new MENSAGEMProvider();
        SERVICOPEDIDOFESTAProvider SERVICOPEDIDOFESTAP = new SERVICOPEDIDOFESTAProvider();
        LIS_SERVICOPEDIDOFESTAProvider LIS_SERVICOPEDIDOFESTAP = new LIS_SERVICOPEDIDOFESTAProvider();
        LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl = new LIS_SERVICOPEDIDOFESTACollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public Boolean ExibiDados = false;

        public FrmPedidoFesta()
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
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDPEDIDOFESTA = -1;
        public PEDIDOFESTAEntity Entity
        {
            get
            {
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text); 
                int IDSTATUS  = Convert.ToInt32(cbStatus.SelectedValue); ;
                string PRAZOENTREGA = string.Empty;
                
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

                string HOMENAGEADO = txtHomenageado.Text;
                
                int? IDTEMA = null;
                if(cbtema.SelectedIndex > 0)
                    IDTEMA = Convert.ToInt32(cbtema.SelectedValue);

                string COR = txtcor.Text;

                DateTime DATAFESTA  = Convert.ToDateTime(maskedtxtDtFesta.Text);

                string HORAINICIO = cbhoraInicio.Text;
                string HORAFIM = cbHoraFim.Text;

                if (txtAdultos.Text == string.Empty)
                    txtAdultos.Text = "0";

                int ADULTOS = Convert.ToInt32(txtAdultos.Text);

                if (txtCriancas.Text == string.Empty)
                    txtCriancas.Text = "0";
                int CRIANCAS = Convert.ToInt32(txtCriancas.Text);

                string NOMESALAO = txtNomeSalao.Text;
                string TELEFONESALAO = txtTelefoneSalao.Text;
                string ENDSALAO = txtEnderecoSalao.Text;
                string BAIRROSALAO = txtBairroSalao.Text;
                string CIDADESALAO = txtCidadeSalao.Text;
                string UF = txtUFSalao.Text;
                string CONTATOSALAO = txtContatoSalao.Text;

                int? IDTIPOFESTA = null;
                if (cbFestas.SelectedIndex > 0)
                    IDTIPOFESTA = Convert.ToInt32(cbFestas.SelectedValue);

                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";
               
                return new PEDIDOFESTAEntity(_IDPEDIDOFESTA, IDCLIENTE, DTEMISSAO, IDSTATUS,
                                        IDTRANSPORTES, IDVENDEDOR, VALORCOMISSAO, OBSERVACAO,
                                        TOTALPRODUTOS, TOTALIMPOSTOS, PORCDESCONTO,
                                        VALORDESCONTO, PORACRESCIMO, VALORACRESCIMO,
                                        TOTALPEDIDO, IDFORMAPAGAMENTO, VALORPAGO, VALORDEVEDOR,
                                        IDLOCALCOBRANCA, IDCENTROCUSTOS, HOMENAGEADO, IDTEMA, COR,
                                        DATAFESTA, HORAINICIO, HORAFIM, ADULTOS, CRIANCAS, NOMESALAO,
                                        TELEFONESALAO, ENDSALAO, BAIRROSALAO, CIDADESALAO, UF, CONTATOSALAO,
                                        IDTIPOFESTA, FLAGORCAMENTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPEDIDOFESTA = value.IDPEDIDOFESTA;
                    ListaServicoPedido(_IDPEDIDOFESTA);
                    txtNPedido.Text = _IDPEDIDOFESTA.ToString().PadLeft(6, '0');
                    cbCliente.SelectedValue = value.IDCLIENTE;

                    txtStatusFinaCliente.Text = string.Empty;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");
                    cbStatus.SelectedValue = value.IDSTATUS;

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
                    txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTOS).ToString("n2");
                    txtTotalDesconto.Text = Convert.ToDecimal(value.VALORDESCONTOS).ToString("n2");
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


                    if (value.IDCENTROSCUSTOS != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROSCUSTOS;
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    txtHomenageado.Text = value.HOMENAGEADO;

                    txtcor.Text = value.COR;
                    if (value.IDTEMA != null)
                        cbtema.SelectedValue = value.IDTEMA;
                    else
                        cbtema.SelectedIndex = 0;

                    maskedtxtDtFesta.Text = Convert.ToDateTime(value.DATAFESTA).ToString("dd/MM/yyyy");

                    string horainicio = value.HORAINICIO;
                    cbhoraInicio.SelectedIndex = cbhoraInicio.FindString(horainicio);

                    string horafim = value.HORAFIM;
                    cbHoraFim.SelectedIndex = cbHoraFim.FindString(horafim);                   

                    txtAdultos.Text = value.ADULTOS.ToString();
                    txtCriancas.Text = value.CRIANCAS.ToString();

                    txtNomeSalao.Text = value.NOMESALAO;
                    txtTelefoneSalao.Text = value.TELEFONESALAO;
                    txtEnderecoSalao.Text = value.ENDSALAO;
                    txtBairroSalao.Text = value.BAIRROSALAO;
                    txtCidadeSalao.Text = value.CIDADESALAO;
                    txtUFSalao.Text = value.UF;
                    txtContatoSalao.Text = value.CONTATOSALAO;

                    EDITREGISTRO = true;
                    if (value.IDTIPOFESTA != null)
                        cbFestas.SelectedValue = value.IDTIPOFESTA;
                    else
                        cbFestas.SelectedIndex = 0;

                    if (value.FLAGORCAMENTO.TrimEnd() == "S")
                        rdOrcamento.Checked = true;
                    else
                        rdOrcamento.Checked = false;
                    
                        
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPEDIDOFESTA = -1;
                    ListaServicoPedido(_IDPEDIDOFESTA);
                    txtNPedido.Text = string.Empty;

                    //Limpa Grid de Duplicatas
                    GridDuplicatasPD(-1, txtNPedido.Text);

                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbStatus.SelectedIndex = 0;

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

                    txtHomenageado.Text = string.Empty;
                    cbtema.SelectedIndex = 0;

                    maskedtxtDtFesta.Text = "  /  /";

                    cbhoraInicio.SelectedIndex = 0; 
                    cbHoraFim.SelectedIndex = 0;

                    txtcor.Text = string.Empty;
                    txtAdultos.Text = string.Empty;
                    txtCriancas.Text = string.Empty;
                    cbSalaoFesta.SelectedIndex = 0;
                    txtNomeSalao.Text = string.Empty;
                    txtTelefoneSalao.Text = string.Empty;
                    txtEnderecoSalao.Text = string.Empty;
                    txtBairroSalao.Text = string.Empty;
                    txtCidadeSalao.Text = string.Empty;                   
                    txtContatoSalao.Text = string.Empty;
                    EDITREGISTRO = false;
                    cbFestas.SelectedIndex = 0;

                    rdOrcamento.Checked = true;
                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODPEDFESTA = -1;
        public PRODUTOSPEDFESTAEntity Entity2
        {
            get
            {
                int IDPRODUTOS = Convert.ToInt32(cbProduto.SelectedValue);
              decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
              decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
              decimal VALORTOTAL =  VALORUNITARIO * QUANTIDADE;
              decimal COMISSAO = 0;                   

              return new PRODUTOSPEDFESTAEntity(_IDPRODPEDFESTA, _IDPEDIDOFESTA, IDPRODUTOS,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL, COMISSAO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODPEDFESTA = value.IDPRODPEDFESTA;
                    cbProduto.SelectedValue = value.IDPRODUTOS;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtValorTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2"); ;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODPEDFESTA = -1;
                    cbProduto.SelectedIndex = 0;
                    txtQuanProduto.Text = "1,00";             
                    txtValorUnitProd.Text = "0,00";
                    txtValorTotal.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }

        int _IDSERVICOPEDIDOFESTA = -1;
        public SERVICOPEDIDOFESTAEntity Entity3
        {
            get
            {
                int IDSERVICO = Convert.ToInt32(cbServico.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanServico.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtVlUnitServicos.Text);
                decimal VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                decimal COMISSAO = 0;

                return new SERVICOPEDIDOFESTAEntity(_IDSERVICOPEDIDOFESTA, _IDPEDIDOFESTA, IDSERVICO,
                                                    QUANTIDADE, VALORUNITARIO, VALORTOTAL);
            }
            set
            {

                if (value != null)
                {
                    _IDSERVICOPEDIDOFESTA = value.IDSERVICOPEDIDOFESTA;
                    cbServico.SelectedValue = value.IDSERVICO;
                    txtQuanServico.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtVlUnitServicos.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtVlTotalServicos.Text = (Convert.ToDecimal(txtQuanServico.Text) * Convert.ToDecimal(txtVlUnitServicos.Text)).ToString("n2"); ;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDSERVICOPEDIDOFESTA = -1;
                    cbServico.SelectedIndex = 0;
                    txtQuanServico.Text = "1,00";
                    txtVlUnitServicos.Text = "0,00";
                    txtVlTotalServicos.Text = "0,00";

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
            GetTransporte();
            GetFuncionario();
            GetDropCentroCusto();
            GetDropFormaPgto();
            GetDropLocalCobranca();
            GetDropSalaoFesta();
            GetDropFesta();
            GetDropTema();
            GetDropTipoDuplicata();
            GetDropMensagem();
            GetDropServico();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
             btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnExtratoCliente.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCadSalao.Image = Util.GetAddressImage(6);
            btnTema.Image = Util.GetAddressImage(6);
            btnCadFesta.Image = Util.GetAddressImage(6);
            btnCadProduto.Image = Util.GetAddressImage(6);
            bntDateSelec.Image = Util.GetAddressImage(11);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCabServico.Image = Util.GetAddressImage(11);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();          

            GetConfiSistema();       


            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
            }

            if (ExibiDados)
            {
                tabControlPedidoVenda.SelectTab(1);
                rbVendasPesquisa.Checked = true;

                // Primeiro Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia igual a 1 
                DateTime primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                msktDataInicialFesta.Text = primeiroDia.ToString("dd/MM/yyyy");

                // Ultimo Dia: Criamos uma variavel DateTime com o ano atual, o mês atual e o dia é a quantidade de dias que o mês corrente possui.
                //A função DateTime.DaysInMonth recebe como parametro o ano(int) e o mês(int) e retorna a quantidade de dias(int). 
                DateTime ultimoDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                msktDataFinalFesta.Text = ultimoDia.ToString("dd/MM/yyyy");

                btnPesquisa_Click(null, null);
            }

            this.Cursor = Cursors.Default;
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
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";
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
            btnAdd2.Image = Util.GetAddressImage(15);
            btnlimpa2.Image = Util.GetAddressImage(16);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);

        }

        private void GetDropCliente()
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


        private void GetDropTema()
        {
            TEMAProvider TEMAPP = new TEMAProvider();
            TEMAColl = TEMAPP.ReadCollectionByParameter(null, "NOME");

            cbtema.DisplayMember = "NOME";
            cbtema.ValueMember = "IDTEMA";

            TEMAEntity TEMATy = new TEMAEntity();
            TEMATy.NOME = ConfigMessage.Default.MsgDrop;
            TEMATy.IDTEMA = -1;
            TEMAColl.Add(TEMATy);

            Phydeaux.Utilities.DynamicComparer<TEMAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TEMAEntity>(cbtema.DisplayMember);

            TEMAColl.Sort(comparer.Comparer);
            cbtema.DataSource = TEMAColl;

            cbtema.SelectedIndex = 0;
        }


        private void GetDropSalaoFesta()
        {
            SALAOFESTAProvider SALAOFESTAP = new SALAOFESTAProvider();
            SALAOFESTAColl = SALAOFESTAP.ReadCollectionByParameter(null, "NOME");

            cbSalaoFesta.DisplayMember = "NOME";
            cbSalaoFesta.ValueMember = "IDSALAOFESTA";

            SALAOFESTAEntity SALAOFESTATy = new SALAOFESTAEntity();
            SALAOFESTATy.NOME = ConfigMessage.Default.MsgDrop;
            SALAOFESTATy.IDSALAOFESTA = -1;
            SALAOFESTAColl.Add(SALAOFESTATy);

            Phydeaux.Utilities.DynamicComparer<SALAOFESTAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SALAOFESTAEntity>(cbSalaoFesta.DisplayMember);

            SALAOFESTAColl.Sort(comparer.Comparer);
            cbSalaoFesta.DataSource = SALAOFESTAColl;

            cbSalaoFesta.SelectedIndex = 0;
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
                frm.ShowDialog();
            }
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbCliente.SelectedIndex > 0)
                VerificaDebitoCliente(Convert.ToInt32(cbCliente.SelectedValue));
        }

        private void VerificaDebitoCliente(int IdCliente)
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
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text) || Convert.ToDecimal(txtValorUnitProd.Text) <= 0)
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
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
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
                    _IDPEDIDOFESTA = PEDIDOFESTAP.Save(Entity);

                    txtNPedido.Text = _IDPEDIDOFESTA.ToString().PadLeft(6, '0');

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
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
            //Validações da Gravação		
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
            else if (maskedtxtDtFesta.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDtFesta.Text))
            {
                errorProvider1.SetError(maskedtxtDtFesta, ConfigMessage.Default.CampoObrigatorio);
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

                if (msktDataInicialFesta.Text != "  /  /" && msktDataFinalFesta.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicialFesta.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinalFesta.Text))
                {
                    filtroProfile = new RowsFiltro("DATAFESTA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicialFesta.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAFESTA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinalFesta.Text));
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
     
                LIS_PEDIDOFESTAColl = LIS_PEDIDOFESTAP.ReadCollectionByParameter(Filtro, "IDPEDIDOFESTA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOFESTAColl;

                lblTotalPesquisa.Text = LIS_PEDIDOFESTAColl.Count.ToString();
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
            if (LIS_PEDIDOFESTAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOFESTAP;
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

                if (msktDataInicialFesta.Text != "  /  /" && msktDataFinalFesta.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicialFesta.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinalFesta.Text))
                {
                    filtroProfile = new RowsFiltro("DATAFESTA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicialFesta.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAFESTA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinalFesta.Text));
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

                LIS_PEDIDOFESTAColl = LIS_PEDIDOFESTAP.ReadCollectionByParameter(Filtro, "IDPEDIDOFESTA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PEDIDOFESTAColl;

                lblTotalPesquisa.Text = LIS_PEDIDOFESTAColl.Count.ToString();
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
            LIS_PEDIDOFESTAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PEDIDOFESTAColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            int CodigoSelect = -1;
            if (LIS_PEDIDOFESTAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOFESTAColl[rowindex].IDPEDIDOFESTA);
                    tabControlPedidoVenda.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = PEDIDOFESTAP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDOFESTA);
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
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOFESTAColl[rowindex].IDPEDIDOFESTA);

                    Entity = PEDIDOFESTAP.Read(CodigoSelect);
                    ListaProdutoPedido(CodigoSelect);
                    ListaServicoPedido(CodigoSelect);
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), CodigoSelect.ToString());

                    apagaToolStripMenuItem_Click(null, null);

                    Entity = null;
                    Entity2 = null;

                }
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CodigoSelect = Convert.ToInt32(LIS_PEDIDOFESTAColl[rowindex].IDPEDIDOFESTA);

                    Entity = PEDIDOFESTAP.Read(CodigoSelect);
                    TSBPrint_Click(null, null);

                    Entity = null;
                    Entity2 = null;

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
            if (LIS_PEDIDOFESTAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PEDIDOFESTAColl[indice].IDPEDIDOFESTA);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlPedidoVenda.SelectTab(0);
                    Entity = PEDIDOFESTAP.Read(CodigoSelect);
                    ListaProdutoPedido(_IDPEDIDOFESTA);
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
                            PEDIDOFESTAP.Delete(CodigoSelect);
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
            if (_IDPEDIDOFESTA == -1)
            {
                MessageBox.Show("Antes de adicionar os produtos é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                SomaTotalItens();
                GravaProduto();
            }
        }

        private void GravaProduto()
        {
            try
            {
                if (ValidacoesProdutos())
                {
                    PRODUTOSPEDFESTAP.Save(Entity2);
                    ListaProdutoPedido(_IDPEDIDOFESTA);                   

                    //Salva Pedido
                    PEDIDOFESTAP.Save(Entity);                 
                   
                    Entity2 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProduto.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

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

        private Boolean VerificaEstoque()
        {
            Boolean result = true;
            decimal EstoqueAtual = Convert.ToDecimal(Util.EstoqueAtual(Convert.ToInt32(cbProduto.SelectedValue), false));
            decimal quantidadeVend = Convert.ToDecimal(txtQuanProduto.Text);

            decimal vend = EstoqueAtual - quantidadeVend;
            if (vend < 0)
                return false;
            
            return result;
        }

        private void ListaProdutoPedido(int IDPEDIDOFESTA)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));
                LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDFESTAColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDFESTAColl.Count.ToString();

                SumTotalProdutosPedido();
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
            foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
            {
                TotalComissao += Convert.ToDecimal(item.COMISSAOPEDIDO);
            }

            txtValComissao.Text = TotalComissao.ToString("n2");
        }

        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
            txtTotalProdAdicional.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void SumTotalServioPedido()
        {
            decimal total = 0;
            foreach (LIS_SERVICOPEDIDOFESTAEntity item in LIS_SERVICOPEDIDOFESTAColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            TxtTotalServicosLista.Text = total.ToString("n2");
            txtTotalServicosAdicionais.Text = total.ToString("n2");
            SumTotalPedido();
        }

        private void SumTotalPedido()
        {
            decimal TotalPedido = 0;
            TotalPedido = (Convert.ToDecimal(txtTotalProdAdicional.Text) + Convert.ToDecimal(TxtTotalServicosLista.Text) +
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
            //if (LIS_PRODUTOSPEDFESTAColl.Count > 0)
            //{
            //    MessageBox.Show("Antes de Apagar o Pedido é necessário remover os Produtos!",
            //                  ConfigSistema1.Default.NomeEmpresa,
            //                  MessageBoxButtons.OK,
            //                  MessageBoxIcon.Error,
            //                  MessageBoxDefaultButton.Button1);

            //    errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);

            //    tabControlPedidoVenda.SelectTab(0);
            //    tabControl1.SelectTab(0);
            //    result = false;
            //}
            //else if (LIS_DUPLICATARECEBERColl.Count > 0)
            //{
            //    MessageBox.Show("Antes de Apagar o Pedido é necessário remover as Duplicatas!",
            //                  ConfigSistema1.Default.NomeEmpresa,
            //                  MessageBoxButtons.OK,
            //                  MessageBoxIcon.Error,
            //                  MessageBoxDefaultButton.Button1);

            //    errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

            //    tabControlPedidoVenda.SelectTab(0);
            //    tabControl1.SelectTab(2);
            //    result = false;
            //}
            //else
                errorProvider1.Clear();


            return result;
        }


        private void Delete()
        {
            if (_IDPEDIDOFESTA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            //Validações de Delete
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
                        //Apaga serviços
                        foreach (var item in LIS_PRODUTOSPEDFESTAColl)
                        {
                            PRODUTOSPEDFESTAP.Delete(Convert.ToInt32(item.IDPRODPEDFESTA));
                        }

                        //Apaga Peças/Produtos
                        foreach (var item2 in LIS_SERVICOPEDIDOFESTAColl)
                        {
                            SERVICOPEDIDOFESTAP.Delete(Convert.ToInt32(item2.IDSERVICOPEDIDOFESTA));
                        }

                        //Apaga Duplicatas
                        foreach (var item4 in LIS_DUPLICATARECEBERColl)
                        {
                            DUPLICATARECEBERP.Delete(Convert.ToInt32(item4.IDDUPLICATARECEBER));
                        }


                        PEDIDOFESTAP.Delete(_IDPEDIDOFESTA);                       

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
            if (LIS_PRODUTOSPEDFESTAColl.Count > 0 && rowindex > -1)
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
                            CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDFESTAColl[rowindex].IDPRODPEDFESTA);
                            int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDFESTAColl[rowindex].IDPRODUTOS);
                           
                            PRODUTOSPEDFESTAP.Delete(CodSelect);                            

                            ListaProdutoPedido(_IDPEDIDOFESTA);
                            Entity2 = null;

                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                 if (ColumnSelecionada == 0)//Editar
                 {
                     CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDFESTAColl[rowindex].IDPRODPEDFESTA);
                     Entity2 = PRODUTOSPEDFESTAP.Read(CodSelect);
                 }
                
            }
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void lkComp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDPEDIDOFESTA == -1)
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
                PRODUTOSPEDFESTAP.Save(Entity2);
                  
            }

            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDOFESTA);            

            //Grava os dados do Pedido
            PEDIDOFESTAP.Save(Entity);        
        }       

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
             if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
            {
                PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
                PRODUTOSTY = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                if(PRODUTOSTY != null)
                {
                    decimal ValorVenda = Convert.ToDecimal(PRODUTOSTY.VALORVENDA1); 
                    txtValorUnitProd.Text = ValorVenda.ToString("n2");
                
                    lblEstoque.Text = "Estoque Atual: " + Convert.ToDecimal(Util.EstoqueAtual(Convert.ToInt32(PRODUTOSTY.IDPRODUTO), false)).ToString("n2");
                }
            }
            else
                lblEstoque.Text = "Estoque Atual: 0";
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
                    txtPorComisVend.Text = PorcComissVend.ToString("n2");
                    txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * PorcComissVend) / 100).ToString("n2");
                    
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
                        txtPorComisVend.Text = PorcComissVend.ToString("n2");
                        txtValComissao.Text = ((Convert.ToDecimal(txtTotalPedido.Text) * PorcComissVend) / 100).ToString("n2");

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
            if (_IDPEDIDOFESTA == -1)
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
                    try
                    {
                        SaveDuplicatas();

                        //Salva a forma de pagamento no Pedido
                        PEDIDOFESTAP.Save(Entity);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
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

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = txtNPedido.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());

                decimal Valor = Convert.ToDecimal(txtValorDev.Text);
                if(Valor == 0)
                    Valor = Convert.ToDecimal(txtTotalFinanceiro.Text);

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
                DUPLICATARECEBERty.NOTAFISCAL = "PDF" + txtNPedido.Text;

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
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
            RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PDF"+numero));

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
                else if (ColumnSelecionada == 1)//Editar
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

        private string GetNameCliente(int IdCliente)
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            return CLIENTEP.Read(IdCliente).NOME;
        }       

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
            modelo2ToolStripMenuItem_Click(null, null);
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
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 600, 68);
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

                int NumerorRegistros = LIS_PEDIDOFESTAColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_PEDIDOFESTAColl.Count)
                {
                    if (LIS_PEDIDOFESTAColl[IndexRegistro].IDPEDIDOFESTA != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_PEDIDOFESTAColl[IndexRegistro].IDPEDIDOFESTA.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_PEDIDOFESTAColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTAColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTAColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTAColl[IndexRegistro].NOMEVENDEDOR, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_PEDIDOFESTAColl[IndexRegistro].TOTALPEDIDO).ToString("n2");
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
                if (IndexRegistro < LIS_PEDIDOFESTAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_PEDIDOFESTAColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

            foreach (LIS_PEDIDOFESTAEntity item in LIS_PEDIDOFESTAColl)
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
            if (_IDPEDIDOFESTA == -1)
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

        int _line = 0;
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //inicio Cabeçalho
            ConfigReportStandard config = new ConfigReportStandard();

            //Armazena na coleção do Orçamento Selecionada
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", _IDPEDIDOFESTA.ToString()));
            LIS_PEDIDOFESTACollection LIS_PEDIDOFESTACollPrint = new LIS_PEDIDOFESTACollection();
            LIS_PEDIDOFESTACollPrint = LIS_PEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

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

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 530, 32);
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


             

            //Dados Cliente
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);

            e.Graphics.DrawString("Nº PEDIDO FESTA", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 320, 98);
            e.Graphics.DrawString(Entity.IDPEDIDOFESTA.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 320, 113);


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
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1 , 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);
            e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].BAIRRO1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

            e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);
          //  e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].CIDADE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
            e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
           // e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].UF1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

            e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].TELEFONE1 + " - " + LIS_CLIENTEColl[0].TELEFONE2, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

            string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 200);
            e.Graphics.DrawString("Email:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 200);
            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTEColl[0].EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 200);
            e.Graphics.DrawString("CPF/CNPJ:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString("Forma Pagto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 215);
            e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].NOMEFORMAPAGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);

            //Fim Cabeçalho


            // inicio Corpo Pedido
            //Alinhamento dos valores
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;

            float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
            float yLineTop = e.MarginBounds.Top + 80;

            e.Graphics.DrawString("Quant", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 250);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, 250);
            e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, 250);
            e.Graphics.DrawString("Vlr.Unit", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, 250);
            e.Graphics.DrawString("Vlr.Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, 250);
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 270, config.MargemDireita + 30, 270);

            StringFormat formataString = new StringFormat();
            formataString.Alignment = StringAlignment.Far;
            formataString.LineAlignment = StringAlignment.Far;

            int pulopagina = 0;
            if (LIS_PRODUTOSPEDFESTAColl.Count > 26)
                pulopagina = 200;

            for (; _line < LIS_PRODUTOSPEDFESTAColl.Count; _line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 200) - pulopagina)
                    {
                        //Rodape
                        paginaAtual++;
                        // e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                        //e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 120));

                        e.HasMorePages = true;
                        return;
                    }

                        yLineTop += lineHeight;
                
                        e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda ,  yLineTop + 80,  config.MargemDireita - 670, 19);
                        e.Graphics.DrawString(LIS_PRODUTOSPEDFESTAColl[_line].QUANTIDADE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 30, yLineTop + 80 + 1);

                        e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 80, config.MargemDireita - 620, 19);
                        e.Graphics.DrawString(LIS_PRODUTOSPEDFESTAColl[_line].IDPRODUTOS.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 90, yLineTop + 80 + 1);

                        e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 80, config.MargemDireita - 230, 19);
                        e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_PRODUTOSPEDFESTAColl[_line].NOMEPRODUTO.ToUpper(), 200), 55), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, yLineTop + 80 + 1);

                        e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 80, config.MargemDireita - 120, 19);
                        if (Convert.ToDecimal(LIS_PRODUTOSPEDFESTAColl[_line].VALORUNITARIO) > 0 )
                            if (!chkValorPedido.Checked)
                                e.Graphics.DrawString(Convert.ToDecimal(LIS_PRODUTOSPEDFESTAColl[_line].VALORUNITARIO).ToString("n4"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 580 + 25, yLineTop + 80 + 15 + 1, formataString);

                        e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 80, config.MargemDireita , 19);
                        if (Convert.ToDecimal(LIS_PRODUTOSPEDFESTAColl[_line].VALORTOTAL) > 0 )
                            if (!chkValorPedido.Checked)
                                e.Graphics.DrawString(Convert.ToDecimal(LIS_PRODUTOSPEDFESTAColl[_line].VALORTOTAL).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 650 +85, yLineTop + 80 + 15 + 1, formataString);
               
                }

                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;
                yLineTop += lineHeight;

                int linha = Convert.ToInt32(yLineTop);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 250, 80);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 115);
                linha = linha + 5;
                e.Graphics.DrawString("Cobrança:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].NOMELOCALCOBRANCA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor :", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].NOMEVENDEDOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Tipo de Festa:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].NOMEFESTA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].NOMETRANSPORTES, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

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

                //Dados Festas
                linha = linha + 10;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 120);
                linha = linha + 5;

                e.Graphics.DrawString("DADOS DA FESTA", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString("Nome do Homenageado:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].HOMENAGEADO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 180, linha);

                e.Graphics.DrawString("Tema:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 470, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].NOMETEMA, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 520, linha);

                linha = linha + 15;
                e.Graphics.DrawString("Cor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].COR, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, linha);

                e.Graphics.DrawString("Data Festa:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 230, linha);
                e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_PEDIDOFESTACollPrint[0].DATAFESTA).ToString("dd/MM/yyyy"), 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha);

                e.Graphics.DrawString("Hora Inicio:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 380, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].HORAINICIO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString("Hora Fim:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(LIS_PEDIDOFESTACollPrint[0].HORAFIM, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 560, linha);

                linha = linha + 15;
                e.Graphics.DrawString("Nº Convidados:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Adultos: " + LIS_PEDIDOFESTACollPrint[0].ADULTOS.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 130, linha);
                e.Graphics.DrawString("Crianças: " + LIS_PEDIDOFESTACollPrint[0].CRIANCAS.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 230, linha);

                linha = linha + 15;
                e.Graphics.DrawString("Nome do Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].NOMESALAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 130, linha);
                e.Graphics.DrawString("Telefone do Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].TELEFONESALAO, 30), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 620, linha);

                linha = linha + 15;
                e.Graphics.DrawString("End. Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].ENDSALAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 130, linha);
                e.Graphics.DrawString("Contato do Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].CONTATOSALAO, 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 620, linha);

                linha = linha + 15;
                e.Graphics.DrawString("Cidade Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].CIDADESALAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 130, linha);
                e.Graphics.DrawString("UF do Salão:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_PEDIDOFESTACollPrint[0].UF, 2), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 600, linha);

                //Observação
                linha = linha + 30;
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

        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
      
        private void ImprimirListaGeralProduto()
        {
           
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private LIS_PRODUTOSPEDFESTACollection ProdutoRel(int IDPEDIDOFESTA)
        {
            LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));

            LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDFESTAColl;
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;        
            _line= 0;   
        }           

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PEDIDOFESTAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOFESTAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PEDIDOFESTAEntity>(orderBy);

                LIS_PEDIDOFESTAColl.Sort(comparer.Comparer);              
                 
                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_PEDIDOFESTAP;
               
            }
        }

        private void boletasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");

            }
            else
                ImprimirBoleta();
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
               // e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

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
               // e.Graphics.DrawString(ClienteTy.UF1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 785);

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
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "PDF" + txtNPedido.Text));
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
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                ClienteTy = CLIENTEP.Read(Convert.ToInt32(Entity.IDCLIENTE));

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(ClienteTy.NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(ClienteTy.ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
               // e.Graphics.DrawString(ClienteTy.CIDADE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

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

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelec_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(190, 160);
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
            maskedtxtDtFesta.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        private void btnCadSalao_Click(object sender, EventArgs e)
        {
            using (FrmSalaFesta frm = new FrmSalaFesta())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbSalaoFesta.SelectedValue);
                GetDropSalaoFesta();
                cbSalaoFesta.SelectedValue = CodSelec;
            }
        }

        private void btnCadFesta_Click(object sender, EventArgs e)
        {
            using (FrmTipoFestas frm = new FrmTipoFestas())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFestas.SelectedValue);
                GetDropFesta();
                cbFestas.SelectedValue = CodSelec;
            }
        }

        private void GetDropFesta()
        {
            FESTASProvider FESTASP = new FESTASProvider();

            FESTASColl = FESTASP.ReadCollectionByParameter(null, "NOME");

            cbFestas.DisplayMember = "NOME";
            cbFestas.ValueMember = "idfestas";

            FESTASEntity FESTASTy = new FESTASEntity();
            FESTASTy.NOME = ConfigMessage.Default.MsgDrop;
            FESTASTy.IDFESTAS = -1;
            FESTASColl.Add(FESTASTy);

            Phydeaux.Utilities.DynamicComparer<FESTASEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FESTASEntity>(cbFestas.DisplayMember);

            FESTASColl.Sort(comparer.Comparer);
            cbFestas.DataSource = FESTASColl;

            cbFestas.SelectedIndex = 0;
        }

        private void cbFestas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFestas.SelectedIndex > 0 && !EDITREGISTRO)
            {

                DialogResult dr = MessageBox.Show("Deseja adicionar os Produtos/Itens da: " + cbFestas.Text + " no pedido!",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    if (_IDPEDIDOFESTA == -1)
                    {
                        MessageBox.Show("Antes de adicionar Produtos/Itens é necessário gravar o Pedido!",
                                        ConfigSistema1.Default.NomeEmpresa,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                    }
                    else
                        AddProdutoFestas(Convert.ToInt32(cbFestas.SelectedValue));
                }
            }
            else
            {
                EDITREGISTRO = false;
            }
        }

        private void AddProdutoFestas(int IDITENSFESTAS)
        {
            //Filtras os produtos da determinada  festa
            LIS_PRODUTOSFESTASProvider LIS_PRODUTOSFESTASP = new LIS_PRODUTOSFESTASProvider();
            LIS_PRODUTOSFESTASCollection LIS_PRODUTOSFESTASColl = new LIS_PRODUTOSFESTASCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDITENSFESTAS", "System.String", "like", IDITENSFESTAS.ToString()));
            LIS_PRODUTOSFESTASColl = LIS_PRODUTOSFESTASP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTOS");

             //Percorre os produtos da coleção
            foreach (LIS_PRODUTOSFESTASEntity item in LIS_PRODUTOSFESTASColl)
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
                PRODUTOSPEDFESTAP.Save(Entity2);             
            }


            ////Lista os produtos do Pedido
            ListaProdutoPedido(_IDPEDIDOFESTA);

            //Grava os dados do Pedido
            PEDIDOFESTAP.Save(Entity);
        }

        private void cbSalaoFesta_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSalaoFesta.SelectedIndex > 0)
            {
                SALAOFESTAEntity SALAOFESTATy = new SALAOFESTAEntity();
                SALAOFESTAProvider SALAOFESTAP = new SALAOFESTAProvider();
                SALAOFESTATy = SALAOFESTAP.Read(Convert.ToInt32(cbSalaoFesta.SelectedValue));

                if (SALAOFESTATy != null)
                {
                    txtNomeSalao.Text = SALAOFESTATy.NOME;
                    txtTelefoneSalao.Text = SALAOFESTATy.TELEFONE1;
                    txtEnderecoSalao.Text = SALAOFESTATy.ENDERECO;
                    txtBairroSalao.Text = SALAOFESTATy.BAIRRO;
                    txtCidadeSalao.Text = SALAOFESTATy.CIDADE;
                    txtUFSalao.Text = SALAOFESTATy.UF;
                    txtContatoSalao.Text = SALAOFESTATy.CONTATO;
                }
            }
            else
            {
                txtNomeSalao.Text = string.Empty;
                txtTelefoneSalao.Text = string.Empty;
                txtEnderecoSalao.Text = string.Empty;
                txtBairroSalao.Text = string.Empty;
                txtCidadeSalao.Text = string.Empty;
                txtUFSalao.Text = string.Empty;
                txtContatoSalao.Text = string.Empty;
            }

        }

        private void btnTema_Click(object sender, EventArgs e)
        {
            using (FrmTema frm = new FrmTema())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbtema.SelectedValue);
                GetDropTema();
                cbtema.SelectedValue = CodSelec;
            }
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
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
            if (_IDPEDIDOFESTA == -1)
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

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void geralComProdutos2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaServicoProdPedidoFesta frm = new FrmListaServicoProdPedidoFesta();
                 frm.ShowDialog();
        }

        private void txtQuanProduto_Leave(object sender, EventArgs e)
        {
            SomaTotalItens();
        }

        private void SomaTotalItens()
        {
            try
            {
                if (txtQuanProduto.Text.TrimEnd() == string.Empty)
                    txtQuanProduto.Text = "0,00";

                if (txtValorUnitProd.Text == string.Empty)
                    txtValorUnitProd.Text = "0,00";

                txtValorTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SomaTotalItensServicos()
        {
            try
            {
                if (txtQuanServico.Text.TrimEnd() == string.Empty)
                    txtQuanServico.Text = "0,00";

                if (txtVlUnitServicos.Text == string.Empty)
                    txtVlUnitServicos.Text = "0,00";

                txtVlTotalServicos.Text = (Convert.ToDecimal(txtQuanServico.Text) * Convert.ToDecimal(txtVlUnitServicos.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorUnitProd_Leave(object sender, EventArgs e)
        {
            SomaTotalItens();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

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
                frm.TituloSelec = "Relação de Clientes";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
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

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar3";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(230, 55);
            FormCalendario3.Name = "FormCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicialFesta.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FormCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinalFesta.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOFESTA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else 
                ImprimirReceitaReport();
        }

        private void ImprimirReceitaReport()
        {
            PEDIDOFESTAP.Save(Entity);
            using (FrmRelatorioPedidoFesta frm = new FrmRelatorioPedidoFesta())
            {
                frm.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDPEDIDOFESTA = _IDPEDIDOFESTA;
                frm.ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtObservacao.Clear();
        }

        private void btnCabServico_Click(object sender, EventArgs e)
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

        private void txtQuanServico_Leave(object sender, EventArgs e)
        {
            SomaTotalItensServicos();
        }

        private void txtVlUnitServicos_Leave(object sender, EventArgs e)
        {
            SomaTotalItensServicos();
        }

        private void btnAdServico_Click(object sender, EventArgs e)
        {
            

            if (_IDPEDIDOFESTA == -1)
            {
                MessageBox.Show("Antes de adicionar o serviço é necessário gravar o Pedido!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                SomaTotalItensServicos();
                GravaServicoPedido();
            }
        }

        private Boolean ValidacoesServicoPedido()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbServico.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanServico.Text))
            {
                errorProvider1.SetError(txtQuanServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlUnitServicos.Text))
            {
                errorProvider1.SetError(txtVlUnitServicos, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }     
            else
                errorProvider1.Clear();


            return result;
        }

        private void ListaServicoPedido(int IDPEDIDOFESTA)
        {
            try
            {
               RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));
                LIS_SERVICOPEDIDOFESTAColl = LIS_SERVICOPEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

                dsGFridServicoPedidoFesta.AutoGenerateColumns = false;
                dsGFridServicoPedidoFesta.DataSource = LIS_SERVICOPEDIDOFESTAColl;
                label54.Text = "Nº de Serviços: " + LIS_SERVICOPEDIDOFESTAColl.Count.ToString();

                SumTotalServioPedido();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GravaServicoPedido()
        {
            try
            {
                if (ValidacoesServicoPedido())
                {
                    SERVICOPEDIDOFESTAP.Save(Entity3);
                    ListaServicoPedido(_IDPEDIDOFESTA);

                    //Salva Pedido
                    PEDIDOFESTAP.Save(Entity);

                    Entity3 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbServico.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void dsGFridServicoPedidoFesta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SERVICOPEDIDOFESTAColl.Count > 0 && rowindex > -1)
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
                            CodSelect = Convert.ToInt32(LIS_SERVICOPEDIDOFESTAColl[rowindex].IDSERVICOPEDIDOFESTA);

                            SERVICOPEDIDOFESTAP.Delete(CodSelect);
                            ListaServicoPedido(_IDPEDIDOFESTA);
                            Entity3 = null;

                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_SERVICOPEDIDOFESTAColl[rowindex].IDSERVICOPEDIDOFESTA);
                    Entity3 = SERVICOPEDIDOFESTAP.Read(CodSelect);
                }

            }
        }

        private void cbServico_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbServico.SelectedValue) > 0)
            {
                SERVICOProvider SERVICOP = new SERVICOProvider();
                SERVICOEntity SERVICOTY = new SERVICOEntity();
                SERVICOTY = SERVICOP.Read(Convert.ToInt32(cbServico.SelectedValue));

                if (SERVICOTY != null)
                {
                    decimal ValorVenda = Convert.ToDecimal(SERVICOTY.VALOR);
                    txtVlUnitServicos.Text = ValorVenda.ToString("n2");
                }
            }
           
             
        }

        private void vendasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorOS2PedidoFesta Frm = new FrmVendaVendedorOS2PedidoFesta();
            Frm.ShowDialog();
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPEDIDOFESTA == -1)
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
              //  var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
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
                foreach (LIS_SERVICOPEDIDOFESTAEntity item in LIS_SERVICOPEDIDOFESTAColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMESERVICO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
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

                body += "Data da Festa: " + maskedtxtDtFesta.Text + "<br>";

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

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
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


    }
}
