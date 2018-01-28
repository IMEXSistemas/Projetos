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
using BmsSoftware.Modulos.Cadastros;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using CDSSoftware;
using BmsSoftware.UI;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Modulos.Relatorio;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using System.Net.Mail;
using System.Net;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmOrdemServicoSFech : Form
    {
        string FLAGFECHOSESTOQUE = string.Empty;

        Utility Util = new Utility();

        SERVICOCollection SERVICOColl = new SERVICOCollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FECHOSERVICOCollection FECHOSERVICOColl = new FECHOSERVICOCollection();
        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();
        LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
        LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl2 = new FUNCIONARIOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl3 = new FUNCIONARIOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl4 = new FUNCIONARIOCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHPrinColl = new LIS_ORDEMSERVICOSFECHCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSMTColl = new PRODUTOSCollection();
        LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
        EQUIPAMENTOCollection EQUIPAMENTOColl = new EQUIPAMENTOCollection();
        LIS_EQUIPAMENTOOSFECHCollection LIS_EQUIPAMENTOOSFECHColl = new LIS_EQUIPAMENTOOSFECHCollection();
        LIS_EQUIPAMENTOOSFECHProvider LIS_EQUIPAMENTOOSFECHP = new LIS_EQUIPAMENTOOSFECHProvider();
        MENSAGEMProvider MENSAGEMP = new MENSAGEMProvider();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();

        LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
        EQUIPAMENTOOSFECHProvider EQUIPAMENTOOSFECHP = new EQUIPAMENTOOSFECHProvider();
        ORDEMSERVICOSFECHProvider ORDEMSERVICOSFECHP = new ORDEMSERVICOSFECHProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();
        SERVICOOSFECHProvider SERVICOOSFECHP = new SERVICOOSFECHProvider();
        LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();
        LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
        PRODUTOOSFECHProvider PRODUTOOSFECHP = new PRODUTOOSFECHProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        PRODUTOSPEDIDOMTQOSProvider PRODUTOSPEDIDOMTQOSP = new PRODUTOSPEDIDOMTQOSProvider();
        LIS_PRODUTOSPEDIDOMTQOSProvider LIS_PRODUTOSPEDIDOMTQOSP = new LIS_PRODUTOSPEDIDOMTQOSProvider();
        LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();

        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmOrdemServicoSFech()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        private void RegisterFocusEvents(Control.ControlCollection controls)
        {
            //Filtra para alterar Label
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", this.Name.ToString()));
            LIS_LABELTELACollection LIS_LABELTELAColl = new LIS_LABELTELACollection();
            LIS_LABELTELAProvider LIS_LABELTELAP = new LIS_LABELTELAProvider();
            LIS_LABELTELAColl = LIS_LABELTELAP.ReadCollectionByParameter(RowRelatorio);

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

                //Altera o texto dos label
                if (control is Label)
                {
                
                    foreach (var item in LIS_LABELTELAColl)
                    {
                        if (control.Name == item.NOMELABEL)
                            {
                                control.Text = item.TEXTOLABEL;
                                break;
                            }
                    }

                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        void controlFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            
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

        public int _IDORDEMSERVICO = -1;
        public ORDEMSERVICOSFECHEntity Entity
        {
            get
            {
                DateTime DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                decimal VALORORCAMENTO = 0;
                int PRAZOENTREGA = Convert.ToInt32(txtPrazoEntrega.Text);
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
                string OBSERVACAO = txtObservacao.Text;

                decimal TOTALITEMSERVICO = 0;
                if (txtTotalServicos.Text!= string.Empty)
                   TOTALITEMSERVICO = Convert.ToDecimal(txtTotalServicos.Text);

                decimal TOTALITEMPECA = 0;
                if (txtTotalPecas.Text != string.Empty)
                    TOTALITEMPECA = Convert.ToDecimal(txtTotalPecas.Text);

                decimal MAOOBRA = 0;
                if (TotalMaoObra.Text != string.Empty)
                    MAOOBRA = Convert.ToDecimal(TotalMaoObra.Text);

                decimal OUTROVALOR = 0;
                if (txtValorOutros.Text != string.Empty)
                    OUTROVALOR = Convert.ToDecimal(txtValorOutros.Text);

                decimal TOTALFECHOS = 0;
                if (txtTotalOS.Text != string.Empty)
                    TOTALFECHOS = Convert.ToDecimal(txtTotalOS.Text);

                DateTime? GARANTIAVECTO = null;
                if (mkdEntrega.Text != "  /  /")
                    GARANTIAVECTO = Convert.ToDateTime(mkdEntrega.Text);

                int? IDFORMAPAGAMENTO = null;
                if (Convert.ToInt32(cbFormaPagto.SelectedValue) > 0)
                    IDFORMAPAGAMENTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                string CONTATO = string.Empty;

               decimal VALORPAGO = Convert.ToDecimal(txtValorPago.Text);
               decimal VALORDEVEDOR = Convert.ToDecimal(txtValorDev.Text);

                string FLAGORCAMENTO = rdOrcamento.Checked ? "S" : "N";

                decimal PORCCOMISSAO = 0;
                decimal VLCOMISSAO = 0;
                string FLAGTELABLOQUEADA = chkTelaBloqueada.Checked ? "S" : "N";

                string PLACA ="";
                if (cbVeiculo.SelectedIndex > 0)
                     PLACA = cbVeiculo.Text;

                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconto.Text);
                decimal VALORDESCONTO = Convert.ToDecimal(txtTotalDesconto.Text);

                string PROBLEMAINFORMADO = txtProblemaInformado.Text;
                string PROBLEMACONSTATADO = txtProblemaConstatado.Text;
                string SERVICOEXECUTADO = txtServiçoExecutado.Text;

                string EQUIPAMENTO = txtEquipamento.Text;
                string MODELO = txtModelo.Text;
                string MARCA = txtMarca.Text;
                string ACESSORIOS = txtAcessorios.Text;

                return new ORDEMSERVICOSFECHEntity(_IDORDEMSERVICO, DATAEMISSAO, VALORORCAMENTO, PRAZOENTREGA,
                                              IDSTATUS, IDFUNCIONARIO, OBSERVACAO,
                                              TOTALITEMSERVICO, TOTALITEMPECA, MAOOBRA, OUTROVALOR,
                                              TOTALFECHOS, GARANTIAVECTO, IDFORMAPAGAMENTO, IDCLIENTE,
                                              CONTATO, VALORPAGO, VALORDEVEDOR, FLAGORCAMENTO,
                                              PORCCOMISSAO, VLCOMISSAO, FLAGTELABLOQUEADA, PLACA,
                                              PORCDESCONTO, VALORDESCONTO, PROBLEMAINFORMADO, PROBLEMACONSTATADO, SERVICOEXECUTADO,
                                              EQUIPAMENTO, MODELO, MARCA, ACESSORIOS);

            }
            set
            {

                if (value != null)
                {
                    _IDORDEMSERVICO = value.IDORDEMSERVICO;
                    this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    ListaProdutoPedidoMTQOS(_IDORDEMSERVICO);
                    ListaEquipamentoServico(_IDORDEMSERVICO);

                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAEMISSAO).ToString("dd/MM/yyyy");                   
                    txtPrazoEntrega.Text = value.PRAZOENTREGA.ToString();
                    txtObservacao.Text = value.OBSERVACAO;
                    cbStatus.SelectedValue= value.IDSTATUS;
                    cbFuncionario.SelectedValue= value.IDFUNCIONARIO;

                    txtTotalServicos.Text = Convert.ToDecimal(value.TOTALITEMSERVICO).ToString("n2");
                    txtTotalPecas.Text = Convert.ToDecimal(value.TOTALITEMPECA).ToString("n2");
                    TotalMaoObra.Text = Convert.ToDecimal(value.MAOOBRA).ToString("n2");
                    txtValorOutros.Text = Convert.ToDecimal(value.OUTROVALOR).ToString("n2");
                    txtTotalOS.Text = Convert.ToDecimal(value.TOTALFECHOS).ToString("n2");

                    if (value.GARANTIAVECTO != null)
                        mkdEntrega.Text = Convert.ToDateTime(value.GARANTIAVECTO).ToString("dd/MM/yyyy");
                    else
                        mkdEntrega.Text = "  /  /";

                    if (value.IDFORMAPAGAMENTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGAMENTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    cbCliente.SelectedValue = value.IDCLIENTE;

                    if (value.VALORPAGO != null)
                        txtValorPago.Text = Convert.ToDecimal(value.VALORPAGO).ToString("n2");
                    else
                        txtValorPago.Text = "0,00";

                    if (value.VALORPAGO != null)
                         txtValorDev.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");
                    else
                        txtValorDev.Text = "0,00";

                    rdOrcamento.Checked = value.FLAGORCAMENTO.TrimEnd() == "S" ? true : false;             

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

                    if (value.PLACA.Trim() != string.Empty)
                        cbVeiculo.SelectedIndex = cbVeiculo.FindString(value.PLACA);
                    else
                        cbVeiculo.SelectedIndex = -1;

                    txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtTotalDesconto.Text = Convert.ToDecimal(value.VALORDESCONTO).ToString("n2");

                    txtProblemaInformado.Text = value.PROBLEMAINFORMADO;
                    txtProblemaConstatado.Text= value.PROBLEMACONSTATADO;
                    txtServiçoExecutado.Text= value.SERVICOEXECUTADO;

                    txtEquipamento.Text = value.EQUIPAMENTO;
                    txtModelo.Text = value.MODELO;
                    txtMarca.Text = value.MARCA;
                    txtAcessorios.Text = value.ACESSORIOS;                    

                    errorProvider1.Clear();
                }
                else
                {
                    this.Text = "Ordem de Serviço ";
                    _IDORDEMSERVICO = -1;
                    _IDSERVICOOSFECH = -1;
                    ListaProdutoPedidoMTQOS(_IDORDEMSERVICO);
                    ListaEquipamentoServico(_IDORDEMSERVICO);
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");                 

                    txtTotalServicos.Text = "0,00";
                    txtTotalPecas.Text = "0,00";
                    TotalMaoObra.Text = "0,00";
                    txtValorOutros.Text = "0,00";
                    txtTotalOS.Text = "0,00";

                    //Busca prazo de entrega conforme o config do sistema
                    CONFISISTEMATy = CONFISISTEMAP.Read(1);
                    txtPrazoEntrega.Text = CONFISISTEMATy.PRAZOOS.ToString();

                    cbStatus.SelectedIndex = 0;
                   
                    //Busca o Funcionario logado
                    USUARIOEntity USUARIOTY = new USUARIOEntity();
                    USUARIOProvider USUARIOP = new USUARIOProvider();
                    USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                    cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;

                    txtObservacao.Text = string.Empty;

                      mkdEntrega.Text = "  /  /";

                    cbFormaPagto.SelectedIndex = 0;

                    //Preenche Mensagem Salvo na configuração do Sistema
                    txtObservacao.Text = CONFISISTEMATy.MSGFECHOS;

                    cbCliente.SelectedValue = 1;

                    txtValorPago.Text = "0,00";                   
                    txtValorDev.Text = "0,00";

                    rdOrcamento.Checked = false;
                    rdVenda.Checked = true;                 

                    chkTelaBloqueada.Checked = false;
                    chkTelaBloqueada.Text = "Tela Desbloqueada";
                    chkTelaBloqueada.ForeColor = System.Drawing.Color.Black;
                    chkTelaBloqueada.Checked = false;

                    cbVeiculo.SelectedValue = -1;

                    txtPorcDesconto.Text = "0,00";
                    txtTotalDesconto.Text = "0,00";

                    txtProblemaInformado.Text = string.Empty;
                    txtProblemaConstatado.Text = string.Empty;
                    txtServiçoExecutado.Text = string.Empty;

                    txtEquipamento.Text = string.Empty;
                    txtModelo.Text = string.Empty;
                    txtMarca.Text = string.Empty;
                    txtAcessorios.Text = string.Empty; 

                    msktDataEmissao.Focus();
                    errorProvider1.Clear();
                }
            }
        }        

        private void FrmFechaOrdemServico_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            GetDropTipoDuplicata();

            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnCadServico.Image = Util.GetAddressImage(6);
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnFuncionario.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCadCliente.Image = Util.GetAddressImage(6);
            btnCadProdMTQ.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            
            btnDataInicial.Image = Util.GetAddressImage(11);
            btnDataFinal.Image = Util.GetAddressImage(11);
      
            GetDropStatus();
            GetDropStatus2();
            GetFuncionario();
            GetFuncionario2();
            GetFuncionario3();
            GetFuncionario4();
            GetFuncionario5();
            GetDropServico();
            GetDropPecas();
            GetDropCentroCusto();
            GetDropFormaPgto();
            GetDropCliente();
            GetDropLocalCobranca();
            GetDropProdutosMTQOS();
            GetFuncionarioComissao();
            GetDropEquipamento();
            GetDropMensagem();

            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();

            GetConfiSistema();


            if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.TrimEnd().TrimStart() == "N" && BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "N")
            {
                tabFechOS.TabPages.RemoveAt(2);
                tabFechOS.TabPages.RemoveAt(2);
            }
            else if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.TrimEnd().TrimStart() == "N")
            {
                tabFechOS.TabPages.RemoveAt(2);
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "N")
            {
                tabFechOS.TabPages.RemoveAt(3);
            }          

            if(BmsSoftware.ConfigSistema1.Default.FlagAutoMecanica == "S")
            {
                label38.Visible = true;
                cbVeiculo.Visible = true;
                checkListToolStripMenuItem.Visible = true;
                pesquisarOClientePelaPlacaToolStripMenuItem.Visible = true;
            }

            if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "S")
            {
                label62.Visible = true;
                txtTotalEquip.Visible = true;
                horasPorEquipamentoToolStripMenuItem.Visible = true;
            }
            else
            {
                label62.Visible = false;
                txtTotalEquip.Visible = false;
                horasPorEquipamentoToolStripMenuItem.Visible = false;
            }

            VerificaAcesso();
            this.Cursor = Cursors.Default;

            if (_IDORDEMSERVICO != -1)
                Entity = ORDEMSERVICOSFECHP.Read(_IDORDEMSERVICO);
            else
                Entity = null;
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

                FLAGFECHOSESTOQUE = CONFISISTEMATy.FLAGFECHOSESTOQUE;
            }
            catch (Exception)
            {
               this.Cursor = Cursors.Default;
            }
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

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void GetDropStatus()
        {
            //9 Serviços
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "9");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

        private void GetDropStatus2()
        {
            //9 Serviços
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "9");
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

        private void GetDropPecas()
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

        private void GetFuncionario3()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl3 = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario3.DisplayMember = "NOME";
            cbFuncionario3.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl3.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario3.DisplayMember);

            FUNCIONARIOColl3.Sort(comparer.Comparer);
            cbFuncionario3.DataSource = FUNCIONARIOColl3;

            cbFuncionario3.SelectedIndex = 0;
        }

        private void GetFuncionario4()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl4 = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbfuncionario4.DisplayMember = "NOME";
            cbfuncionario4.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl4.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbfuncionario4.DisplayMember);

            FUNCIONARIOColl4.Sort(comparer.Comparer);
            cbfuncionario4.DataSource = FUNCIONARIOColl4;

            cbfuncionario4.SelectedIndex = 0;
        }

        private void GetFuncionario5()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl4 = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario5.DisplayMember = "NOME";
            cbFuncionario5.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl4.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario5.DisplayMember);

            FUNCIONARIOColl4.Sort(comparer.Comparer);
            cbFuncionario5.DataSource = FUNCIONARIOColl4;

            cbFuncionario5.SelectedIndex = 0;
        }  

        private void GetFuncionario2()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl2 = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario2.DisplayMember = "NOME";
            cbFuncionario2.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl2.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario2.DisplayMember);

            FUNCIONARIOColl2.Sort(comparer.Comparer);
            cbFuncionario2.DataSource = FUNCIONARIOColl2;

            cbFuncionario2.SelectedIndex = 0;
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
            btnAdd3.Image = Util.GetAddressImage(15);
            btnlimpa3.Image = Util.GetAddressImage(16);
            btnAdd4.Image = Util.GetAddressImage(15);
            btnlimpa4.Image = Util.GetAddressImage(16);

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

        private void FrmFechaOrdemServico_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmFechaOrdemServico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }
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

        private void btnFuncionario_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                
                GetFuncionario();
                GetFuncionario2();
                GetFuncionario3();
                GetFuncionario4();
                GetFuncionario5();
                cbFuncionario.SelectedValue = CodSelec;
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

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            

            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                using (FrmProduto frm = new FrmProduto())
                {
                    int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    frm._IDPRODUTO = CodSelec;
                    frm.ShowDialog();
                    GetDropPecas();
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
                    GetDropPecas();
                    cbProduto.SelectedValue = CodSelec;
                }
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

        private void cbServico_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o serviço ou pressione Ctrl+E para pesquisar.";
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione a Peça/Produto ou pressione Ctrl+E para pesquisar.";           
        }       

        private void txtValorServico_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorServico.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorServico.Text))
                {
                    errorProvider1.SetError(txtValorServico, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorServico.Text);
                    txtValorServico.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorServico, "");
                }
            }
            else
                txtValorServico.Text = "0,00";
        }

        private void txtValorUnitPecas_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorUnitPecas.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitPecas.Text))
                {
                    errorProvider1.SetError(txtValorUnitPecas, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorUnitPecas.Text);
                    txtValorUnitPecas.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorUnitPecas, "");
                }
            }
            else
                txtValorUnitPecas.Text = "0,00";
        }

        private void txtQuanServico_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanServico.Text))
            {
                errorProvider1.SetError(txtQuanServico, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                errorProvider1.SetError(txtQuanServico, "");
            }     
        }

        private void txtQuanPeca_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanPeca.Text))
            {
                errorProvider1.SetError(txtQuanPeca, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                errorProvider1.SetError(txtQuanPeca, "");
            }     
        }      

       

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
                Grava();
           
        }
      

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {

                   _IDORDEMSERVICO = ORDEMSERVICOSFECHP.Save(Entity);
                    this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
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

                    ORDEMSERVICOSFECHCollection ORDEMSERVICOSFECHColl_Total = new ORDEMSERVICOSFECHCollection();
                    ORDEMSERVICOSFECHColl_Total = ORDEMSERVICOSFECHP.ReadCollectionByParameter(null);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim()));

                    if (RECURSOSPLANOTy != null)
                    {
                        int Quant = Convert.ToInt32(RECURSOSPLANOTy.ORDEMSERVICO);

                        if (ORDEMSERVICOSFECHColl_Total.Count < Quant)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            MessageBox.Show("Limite de Ordem de Serviço atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
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

        private Boolean Validacoes()
        {
            Boolean result = true;
            
            errorProvider1.Clear();
            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                tabFechOS.SelectTab(0);
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (_IDORDEMSERVICO == -1 && !VerificaPlanos())
            {
                result = false;
            }
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                tabFechOS.SelectTab(0);
                errorProvider1.SetError(cbStatus, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");

                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }           
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtPrazoEntrega.Text))
            {
                tabFechOS.SelectTab(0);
                string MSGErro = "Campo com o Valor Inválido!";
                errorProvider1.SetError(txtPrazoEntrega, MSGErro);
                Util.ExibirMSg(MSGErro, "Red");
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                tabFechOS.SelectTab(0);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbFuncionario.SelectedValue) < 1)
            {
                tabFechOS.SelectTab(0);
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
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

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {
            if (msktDataEmissao.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
                    msktDataEmissao.Focus();
                    errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(msktDataEmissao, "");
                }
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
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
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
                
                if(Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.String", "=", cbStatus2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(Filtro, "IDORDEMSERVICO DESC");

                //Colocando somatorio no final da lista
                LIS_ORDEMSERVICOSFECHEntity LIS_ORDEMSERVICOSFECHETy = new LIS_ORDEMSERVICOSFECHEntity();
                LIS_ORDEMSERVICOSFECHETy.TOTALFECHOS = SumTotalPesquisa("TOTALFECHOS");
                LIS_ORDEMSERVICOSFECHETy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_ORDEMSERVICOSFECHETy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_ORDEMSERVICOSFECHETy.VLCOMISSAO = SumTotalPesquisa("VLCOMISSAO");
                LIS_ORDEMSERVICOSFECHColl.Add(LIS_ORDEMSERVICOSFECHETy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ORDEMSERVICOSFECHColl;

                lblTotalPesquisa.Text = (LIS_ORDEMSERVICOSFECHColl.Count - 1).ToString();
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
                foreach (LIS_ORDEMSERVICOSFECHEntity item in LIS_ORDEMSERVICOSFECHColl)
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
            foreach (LIS_ORDEMSERVICOSFECHEntity item in LIS_ORDEMSERVICOSFECHColl)
            {
                if (NomeCampo == "TOTALFECHOS")
                    valortotal += Convert.ToDecimal(item.TOTALFECHOS);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VLCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VLCOMISSAO);
            }

            return valortotal;
        }


        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabFechOS.SelectedIndex == 2)
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
                if (LIS_ORDEMSERVICOSFECHColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ORDEMSERVICOSFECHColl;
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
                        filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
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
                        filtroProfile = new RowsFiltro("IDSTATUS", "System.String", "=", cbStatus2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(Filtro, "IDORDEMSERVICO DESC");

                    //Colocando somatorio no final da lista
                    LIS_ORDEMSERVICOSFECHEntity LIS_ORDEMSERVICOSFECHETy = new LIS_ORDEMSERVICOSFECHEntity();
                    LIS_ORDEMSERVICOSFECHETy.TOTALFECHOS = SumTotalPesquisa("TOTALFECHOS");
                    LIS_ORDEMSERVICOSFECHETy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                    LIS_ORDEMSERVICOSFECHETy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                    LIS_ORDEMSERVICOSFECHETy.VLCOMISSAO = SumTotalPesquisa("VLCOMISSAO");
                    LIS_ORDEMSERVICOSFECHColl.Add(LIS_ORDEMSERVICOSFECHETy);

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ORDEMSERVICOSFECHColl;

                    lblTotalPesquisa.Text = (LIS_ORDEMSERVICOSFECHColl.Count - 1).ToString();
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

        private void txtNOrdServico_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Digite o número da Ordem de Serviço e pressione Ctrl+E para pesquisar.";
        }       

        private void FrmFechaOrdemServico_Shown(object sender, EventArgs e)
        {
            msktDataEmissao.Focus();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
                Entity3 = null;
                tabFechOS.SelectTab(0);
                GridDuplicatasOSr(-1, _IDORDEMSERVICO);
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
                Entity3 = null;
                tabFechOS.SelectTab(0);
                GridDuplicatasOSr(-1, _IDORDEMSERVICO);
            }
          
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
                Grava();
        }

        private void btnAddServico_Click(object sender, EventArgs e)
        {
            
            if (Validacoes() &&  ValidacoesItensServicos())
            {

                try
                {
                    _IDORDEMSERVICO = ORDEMSERVICOSFECHP.Save(Entity);
                    this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');

                    //Grava itens de serviços
                    SERVICOOSFECHP.Save(Entity2);

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    Entity2 = null;
                    cbServico.Focus();

                    //Lista os itens de serviços cadastrados;
                    ListaItensServico(_IDORDEMSERVICO);
                    txtPorComisVend_Validating(null, null);
                    ORDEMSERVICOSFECHP.Save(Entity);
                }
                catch (Exception ex)
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }

            }
           
        }

        private void SumTotalItemsServico()
        {
            decimal total = 0;
            foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalServicos.Text = total.ToString("n2");
            SumTotalFechOS();
        }

        private Boolean ValidacoesItensServicos()
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
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanServico.Text))
            {
                errorProvider1.SetError(txtQuanServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (rdVenda.Checked && VerificaCredito())
            {
                string msgerro = "O limite de crédito do cliente foi atingido!";
                errorProvider1.SetError(cbCliente, msgerro);
                MessageBox.Show(msgerro);
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

        private Boolean VerificaCredito()
        {
            Boolean result = false;
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            decimal LimiteCredito = Convert.ToDecimal(CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).CREDITOCLIENTE);

            if (CONFISISTEMAP.Read(1).FLAGLIMITECREDITO.TrimEnd().TrimStart() == "S" && LimiteCredito > 0)
            {

                decimal TotalServico = Convert.ToDecimal(txtQuanServico.Text) * Convert.ToDecimal(txtValorServico.Text);
                decimal TotalProduto = Convert.ToDecimal(txtQuanPeca.Text) * Convert.ToDecimal(txtValorUnitPecas.Text);
                decimal TotalProdutoM2 = Convert.ToDecimal(txtVlTotalMTQ.Text);
                decimal ValorCompraAtual = TotalServico + TotalProduto + TotalProdutoM2;
                decimal ValorDebitoContaReceber = 0;

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl2 = new LIS_DUPLICATARECEBERCollection();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", cbCliente.SelectedValue.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "<>", "OS" + _IDORDEMSERVICO.ToString()));
                
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

        private void ListaItensServico(int IDORDEMSERVICO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                DGDadosServicos.AutoGenerateColumns = false;
                DGDadosServicos.DataSource = LIS_SERVICOOSFECHColl;

                SumTotalItemsServico();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        int _IDSERVICOOSFECH = -1;
        public SERVICOOSFECHEntity Entity2
        {
            get
            {
                int IDSERVICO = Convert.ToInt32(cbServico.SelectedValue);
                int QUANTIDADE = Convert.ToInt32(txtQuanServico.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorServico.Text);
                decimal VALORTOTAL = Convert.ToDecimal(txtValorServico.Text) * Convert.ToInt32(txtQuanServico.Text);

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario2.SelectedValue);

                string DADOSADICIONALSERVICO = txtDadosAdicionalServico.Text;

                return new SERVICOOSFECHEntity(_IDSERVICOOSFECH, IDSERVICO, QUANTIDADE,
                                                 VALORUNITARIO, VALORTOTAL, _IDORDEMSERVICO,
                                                 IDFUNCIONARIO, DADOSADICIONALSERVICO);
            }
            set
            {

                if (value != null)
                {
                    _IDSERVICOOSFECH = Convert.ToInt32(value.IDSERVICOOSFECH);
                     cbServico.SelectedValue  = value.IDSERVICO;
                    txtQuanServico.Text = Convert.ToInt32(value.QUANTIDADE).ToString();
                    txtValorServico.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");

                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario2.SelectedValue = value.IDFUNCIONARIO;
                    else
                        cbFuncionario2.SelectedValue = -1;

                    txtDadosAdicionalServico.Text = value.DADOSADICIONALSERVICO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDSERVICOOSFECH = -1;
                    ListaItensServico(_IDORDEMSERVICO);
                    cbServico.SelectedIndex = 0;
                    txtQuanServico.Text = "1";
                    txtValorServico.Text = "0,00";
                    cbFuncionario2.SelectedValue = -1;
                    txtDadosAdicionalServico.Text = string.Empty;
                    errorProvider1.Clear();
                }
            }
        }   
      
        private void txtValorServico_Enter(object sender, EventArgs e)
        {
            if (txtValorServico.Text == "0,00")
                txtValorServico.Text = string.Empty;

        }

        private void DGDadosServicos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SERVICOOSFECHColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_SERVICOOSFECHColl[rowindex].IDSERVICOOSFECH);
                    Entity2 = SERVICOOSFECHP.Read(CodSelect);
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

                                            CodSelect = Convert.ToInt32(LIS_SERVICOOSFECHColl[rowindex].IDSERVICOOSFECH);
                                            SERVICOOSFECHP.Delete(CodSelect);
                                            ListaItensServico(_IDORDEMSERVICO);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
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

        private void Delete()
        {
            if (_IDORDEMSERVICO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");

                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

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
                        //Apaga serviços
                        foreach (var item in LIS_SERVICOOSFECHColl)
                        {
                          SERVICOOSFECHP.Delete(Convert.ToInt32(item.IDSERVICOOSFECH));
                        }

                        //Apaga Peças/Produtos
                        foreach (var item2 in LIS_PRODUTOOSFECHColl)
                        {
                            PRODUTOOSFECHP.Delete(Convert.ToInt32(item2.IDPRODUTOOSFECH));
                        }

                        //Apaga Duplicatas
                        foreach (var item4 in LIS_DUPLICATARECEBERColl)
                        {
                            DUPLICATARECEBERP.Delete(Convert.ToInt32(item4.IDDUPLICATARECEBER));
                        }

                        //Apaga Produto Mt2
                        foreach (var item5 in LIS_PRODUTOSPEDIDOMTQOSColl)
                        {
                            PRODUTOSPEDIDOMTQOSP.Delete(Convert.ToInt32(item5.IDPRODUTOSPEDIDOMTQOS));
                        }

                        //Apaga Equipamentos
                        foreach (var item6 in LIS_EQUIPAMENTOOSFECHColl)
                        {
                            EQUIPAMENTOOSFECHP.Delete(Convert.ToInt32(item6.IDEQUIPAMENTOOSFECH));
                        }

                        ORDEMSERVICOSFECHP.Delete(_IDORDEMSERVICO);

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

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_ORDEMSERVICOSFECHColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_ORDEMSERVICOSFECHColl.Count.ToString();
        }

        private void cbServico_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchServico frm = new FrmSearchServico())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbServico.SelectedValue = result;
                }
            }
        }

        private void TotalMaoObra_Enter(object sender, EventArgs e)
        {
            if (TotalMaoObra.Text == "0,00")
                TotalMaoObra.Text = string.Empty;
        }

        private void txtValorOutros_Enter(object sender, EventArgs e)
        {
            if (txtValorOutros.Text == "0,00")
                txtValorOutros.Text = string.Empty;
        }

        private void txtTotalOS_Enter(object sender, EventArgs e)
        {
            if (txtTotalOS.Text == "0,00")
                txtTotalOS.Text = string.Empty;
        }

        private void TotalMaoObra_Validating(object sender, CancelEventArgs e)
        {
            if (TotalMaoObra.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TotalMaoObra.Text))
                {
                    errorProvider1.SetError(TotalMaoObra, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TotalMaoObra.Text);
                    TotalMaoObra.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TotalMaoObra, "");
                    SumTotalFechOS();
                }
            }
            else
                TotalMaoObra.Text = "0,00";
        }

        private void txtValorOutros_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorOutros.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorOutros.Text))
                {
                    errorProvider1.SetError(txtValorOutros, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorOutros.Text);
                    txtValorOutros.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorOutros, "");
                    SumTotalFechOS();
                }
            }
            else
                txtValorOutros.Text = "0,00";
        }

        private void txtTotalOS_Validating(object sender, CancelEventArgs e)
        {
            if (txtTotalOS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalOS.Text))
                {
                    errorProvider1.SetError(txtTotalOS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtTotalOS.Text);
                    txtTotalOS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalOS, "");
                }
            }
            else
                txtTotalOS.Text = "0,00";
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
                        txtValorUnitPecas.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void cbServico_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbServico.SelectedIndex > 0)
            {
                SERVICOProvider SERVICOP = new SERVICOProvider();
                SERVICOEntity ServicoTy = SERVICOP.Read(Convert.ToInt32(cbServico.SelectedValue));

                if (ServicoTy.VALOR != null)
                    txtValorServico.Text = Convert.ToDecimal(ServicoTy.VALOR).ToString("N2");
            }

        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                if (PRODUTOSTy.VALORVENDA1 != null)
                    txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("N2");
            }
        }

        int _IDPRODUTOSOSFECH = -1;
        public PRODUTOOSFECHEntity Entity3
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanPeca.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitPecas.Text);
                decimal VALORTOTAL = Convert.ToDecimal(txtValorUnitPecas.Text) * Convert.ToDecimal(txtQuanPeca.Text);

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbFuncionario3.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario3.SelectedValue);

                string DADOSADICIONALPRODUTO = txtDadosAdicionalProduto.Text;

                return new PRODUTOOSFECHEntity(_IDPRODUTOSOSFECH, IDPRODUTO, QUANTIDADE,
                                                 VALORUNITARIO, VALORTOTAL, _IDORDEMSERVICO, IDFUNCIONARIO, DADOSADICIONALPRODUTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOSOSFECH = Convert.ToInt32(value.IDPRODUTOOSFECH);
                    ListaItensPecas(_IDORDEMSERVICO);
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanPeca.Text = Convert.ToInt32(value.QUANTIDADE).ToString("n2");
                    txtValorUnitPecas.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");


                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario3.SelectedValue = value.IDFUNCIONARIO;
                    else
                        cbFuncionario3.SelectedValue = -1;

                    txtDadosAdicionalProduto.Text = value.DADOSADICIONALPRODUTO;
                    TotalProdutoServico();

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOSOSFECH = -1;
                    ListaItensPecas(_IDORDEMSERVICO);
                    cbProduto.SelectedIndex = 0;
                    txtQuanPeca.Text = "1";
                    txtValorUnitPecas.Text = "0,00";
                    cbFuncionario3.SelectedValue = -1;
                    txtDadosAdicionalProduto.Text = string.Empty;
                    TotalProdutoServico();
                    txtCodigoProduto.Text = string.Empty;
                    txtCodigoProduto.Focus();
                    errorProvider1.Clear();
                }
            }
        }

        int _IDEQUIPAMENTOOSFECH = -1;
        public EQUIPAMENTOOSFECHEntity Entity4
        {
            get
            {
                int IDEQUIPAMENTO = Convert.ToInt32(cbEquipamento.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuantEqui.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValoUNitTiLoca.Text);
                string TIPOLOCACAO = Convert.ToString(cbTipoLocacao.SelectedItem);
                decimal QUANTLOCACAO = Convert.ToDecimal(txtQuantTipoLocacao.Text);
                decimal VALORTOTA = VALORUNITARIO * QUANTLOCACAO;

                if (TIPOLOCACAO == "Hora")
                {
                    decimal hours = Math.Floor(QUANTLOCACAO);
                    int pos = QUANTLOCACAO.ToString().IndexOf(",");
                    decimal minutes = 0;

                    //  if (txtQuantTipoLocacao.Text.Length > 3)
                    minutes = Convert.ToDecimal(QUANTLOCACAO.ToString().Substring(pos + 1, 2));
                    // else
                    //     minutes = Convert.ToDecimal(QUANTLOCACAO.ToString().Substring(pos, 2));

                    decimal minutoHora = minutes / 60;

                    VALORTOTA = VALORUNITARIO * hours;
                    VALORTOTA += VALORUNITARIO * minutoHora;
                }


                DateTime? DATAINICIAL = null;
                if (maskedtxtData.Text != "  /  /")
                    DATAINICIAL = Convert.ToDateTime(maskedtxtData.Text);

                DateTime? DATAFINAL = null;
                if (mdkDataFinal.Text != "  /  /")
                    DATAFINAL = Convert.ToDateTime(mdkDataFinal.Text);

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario2.SelectedValue);

                string HORAINICIAL = mkdHoraInicial.Text;
                string HORAFINAL = mkdHoraFinal.Text;

                string HORIMETROINICIAL = txtHorimetroInicial.Text.TrimEnd().TrimStart();
                string HORIMETROFINAL = txtHorimetroFinal.Text.TrimEnd().TrimStart();
                string HORIMETROTOTAL = txtHorimetroTotal.Text.TrimEnd().TrimStart();

                return new EQUIPAMENTOOSFECHEntity(_IDEQUIPAMENTOOSFECH, IDEQUIPAMENTO, QUANTIDADE,
                                              VALORUNITARIO, VALORTOTA, _IDORDEMSERVICO, TIPOLOCACAO,
                                              DATAINICIAL, DATAFINAL, QUANTLOCACAO, IDFUNCIONARIO,
                                              HORAINICIAL, HORAFINAL, HORIMETROINICIAL, HORIMETROFINAL, HORIMETROTOTAL);
            }
            set
            {
                if (value != null)
                {
                    _IDEQUIPAMENTOOSFECH = Convert.ToInt32(value.IDEQUIPAMENTOOSFECH);
                    cbEquipamento.SelectedValue = value.IDEQUIPAMENTO;
                    txtQuantEqui.Text = Convert.ToInt32(value.QUANTIDADE).ToString();
                    txtValoUNitTiLoca.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");

                    // cbTipoLocacao.SelectedValue = value.TIPOLOCACAO;
                    cbTipoLocacao.SelectedItem = value.TIPOLOCACAO;

                    if (value.DATAINICIAL != null)
                        maskedtxtData.Text = Convert.ToDateTime(value.DATAINICIAL).ToString("dd/MM/yyyy");
                    else
                        maskedtxtData.Text = "  /  /";

                    if (value.DATAFINAL != null)
                        mdkDataFinal.Text = Convert.ToDateTime(value.DATAFINAL).ToString("dd/MM/yyyy");
                    else
                        mdkDataFinal.Text = "  /  /";

                    txtQuantTipoLocacao.Text = Convert.ToDecimal(value.QUANTLOCACAO).ToString("n2");

                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario2.SelectedValue = value.IDFUNCIONARIO;
                    else
                        cbFuncionario2.SelectedValue = -1;

                    mkdHoraInicial.Text = value.HORAINICIAL;
                    mkdHoraFinal.Text = value.HORAFINAL;

                    txtHorimetroInicial.Text = value.HORIMETROINICIAL.Trim();
                    txtHorimetroFinal.Text = value.HORIMETROFINAL.Trim();
                    txtHorimetroTotal.Text = value.HORIMETROTOTAL.Trim();


                    errorProvider1.Clear();
                }
                else
                {
                    _IDEQUIPAMENTOOSFECH = -1;
                    ListaEquipamentoServico(_IDORDEMSERVICO);
                    cbEquipamento.SelectedIndex = 0;
                    txtQuantEqui.Text = "1";
                    txtValoUNitTiLoca.Text = "0,00";
                    cbTipoLocacao.SelectedIndex = 0;
                    maskedtxtData.Text = "  /  /";
                    mdkDataFinal.Text = "  /  /";
                    txtQuantTipoLocacao.Text = "0,00";
                    txtValoUNitTiLoca.Text = "0,00";
                    cbFuncionario2.SelectedValue = -1;
                    errorProvider1.Clear();

                    txtHorimetroInicial.Text = "0";
                    txtHorimetroFinal.Text = "0";
                    txtHorimetroTotal.Text = "0";

                    mkdHoraInicial.Text = "  :";
                    mkdHoraFinal.Text = "  :";
                }

            }
        }
     
        int _IDPRODUTOSPEDIDOMTQOS = -1;
        public PRODUTOSPEDIDOMTQOSEntity Entity6
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
                decimal PORCPERDAMT = Convert.ToDecimal(txtPorcPerda.Text);
                decimal TOTALPERDAMT = Convert.ToDecimal(txtPerdaMT2.Text);

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbFuncionario5.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario5.SelectedValue);

                return new PRODUTOSPEDIDOMTQOSEntity(_IDPRODUTOSPEDIDOMTQOS, _IDORDEMSERVICO, IDPRODUTO,
                                                  QUANTIDADE, ALTURA, LARGURA, MT2, VALORMETRO,
                                                  VALORUNITARIO, VALORTOTAL, COMISSAO, PORCPERDAMT, TOTALPERDAMT,
                                                  IDFUNCIONARIO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOSPEDIDOMTQOS = value.IDPRODUTOSPEDIDOMTQOS;
                    cbProdutoMTQ.SelectedValue = value.IDPRODUTO;
                    txtQuantMTQ.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtvalorunitMTQ.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtAlturaMTQ.Text = Convert.ToDecimal(value.ALTURA).ToString("n4");
                    txtLarguraMTQ.Text = Convert.ToDecimal(value.LARGURA).ToString("n4");
                    txtTotalMTQ.Text = Convert.ToDecimal(value.MT2).ToString("n4");
                    txtVlMtrMTQ.Text = Convert.ToDecimal(value.VALORMETRO).ToString("n2");
                    txtVlTotalMTQ.Text = Convert.ToDecimal(value.VALORTOTAL).ToString("n2");
                    txtPorcPerda.Text = Convert.ToDecimal(value.PORCPERDAMT).ToString("n2");
                    txtPerdaMT2.Text = Convert.ToDecimal(value.TOTALPERDAMT).ToString("n4");
                    SomaPerdaMT2();

                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario5.SelectedValue = value.IDFUNCIONARIO;
                    
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOSPEDIDOMTQOS = -1;
                    cbProdutoMTQ.SelectedIndex = 0;
                    txtQuantMTQ.Text = "1,00";
                    txtvalorunitMTQ.Text = "0,00";
                    txtAlturaMTQ.Text = "0,00";
                    txtLarguraMTQ.Text = "0,00";
                    txtTotalMTQ.Text = "0,00";
                    txtVlMtrMTQ.Text = "0,00";
                    txtPorcPerda.Text = "0,00";
                    txtPerdaMT2.Text = "0,00";
                    txtVlTotalMTQ.Text = "0,0000";

                    txtPorcPerda.Text = "0.00";
                    txtPerdaMT2.Text = "0,0000";
                    SomaPerdaMT2();
                    cbFuncionario5.SelectedValue = -1;
                    errorProvider1.Clear();
                }
            }
        }     

        private void ListaItensPecas(int IDORDEMSERVICO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                DGDadosPecas.AutoGenerateColumns = false;
                DGDadosPecas.DataSource = LIS_PRODUTOOSFECHColl;

                SumTotalItemsPecas();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalItemsPecas()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalPecas.Text = total.ToString("n2");
            SumTotalFechOS();
        }

        private void SumTotalFechOS()
        {
            decimal TotalOS = Convert.ToDecimal(txtTotalServicos.Text) + Convert.ToDecimal(txtTotalPecas.Text) +  Convert.ToDecimal(txtTotalProdMTQ.Text) +
                              Convert.ToDecimal(TotalMaoObra.Text) + Convert.ToDecimal(txtValorOutros.Text) + Convert.ToDecimal(txtTotalEquip.Text) - Convert.ToDecimal(txtTotalDesconto.Text);
            
            txtTotalOS.Text = TotalOS.ToString("n2");
            txtTotalFinanceiro.Text = txtTotalOS.Text;
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (Validacoes() && ValidacoesItensPecas())
            {

                try
                {
                    TotalProdutoServico();
                    _IDORDEMSERVICO = ORDEMSERVICOSFECHP.Save(Entity);
                    this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');

                    //Grava os produtos
                    PRODUTOOSFECHP.Save(Entity3);                       

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    Entity3 = null;
                    txtCodigoProduto.Focus();
                    
                    //Lista os produtos da O.S;
                    ListaItensPecas(_IDORDEMSERVICO);

                    txtPorComisVend_Validating(null, null);

                    //Salva a O.S
                    ORDEMSERVICOSFECHP.Save(Entity);
                }
                catch (Exception ex)
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
                  
            }
           
        }       

        private Boolean ValidacoesItensPecas()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitPecas.Text))
            {
                errorProvider1.SetError(txtValorUnitPecas, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanPeca.Text))
            {
                errorProvider1.SetError(txtQuanPeca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (rdVenda.Checked && VerificaCredito())
            {
                string msgerro = "O limite de crédito do cliente foi atingido!";
                errorProvider1.SetError(cbCliente, msgerro);
                MessageBox.Show(msgerro);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Entity3 = null;
        }

        private void DGDadosPecas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOOSFECHColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOOSFECHColl[rowindex].IDPRODUTOOSFECH);
                    Entity3 = PRODUTOOSFECHP.Read(CodSelect);                   
                   
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
                                    CodSelect = Convert.ToInt32(LIS_PRODUTOOSFECHColl[rowindex].IDPRODUTOOSFECH);
                                    int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOOSFECHColl[rowindex].IDPRODUTO);
                                    PRODUTOOSFECHP.Delete(CodSelect);

                                    ListaItensPecas(_IDORDEMSERVICO);
                                    Entity3 = null;
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
        }
    

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
             if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário gravar a Ordem de Serviço!",
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
                        if (txtValorPago.Text == "0,00")
                            txtValorDev.Text = txtTotalFinanceiro.Text;

                        SaveDuplicatas();
                        
                        //Salva a forma de pagamento no fechamento
                        ORDEMSERVICOSFECHP.Save(Entity);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }            
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

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = _IDORDEMSERVICO.ToString() + "-" + NumTotalDupl.ToString();
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
                Valor = ((Valor * Convert.ToDecimal(item.PORCJUROS))/100) + Valor; 
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = Valor;
                DUPLICATARECEBERty.IDSTATUS = 1;//Aberto
                DUPLICATARECEBERty.NOTAFISCAL = "OS" + _IDORDEMSERVICO.ToString();

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }

             
            }
            
        }

        private void GridDuplicatasOSr(int idcliente, int numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.Int32", "like", "OS", "and"));
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "like", numero.ToString()));

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");
                dataGridDupl.AutoGenerateColumns = false;
                dataGridDupl.DataSource = LIS_DUPLICATARECEBERColl;
                SumFinanceiroOS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumFinanceiroOS()
        {
            decimal valorTotal = 0;
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                valorTotal += Convert.ToDecimal(item.VALORDUPLICATA);
            }

            lblTotalFinanceiro.Text = valorTotal.ToString();
            Double f = Convert.ToDouble(lblTotalFinanceiro.Text);
            lblTotalFinanceiro.Text = string.Format("{0:n2}", f);

            lblTotalFinanceiro.Text = "Total: " + lblTotalFinanceiro.Text;
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
                             if (chkTelaBloqueada.Checked)
                            {
                                string msgerro = "Tela bloqueada!";
                                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                                Util.ExibirMSg(msgerro, "Red");
                            }
                            else if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                                DUPLICATARECEBERP.Delete(CodSelect);
                                GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                SumFinanceiroOS();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
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
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);
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

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ORDEMSERVICOSFECHColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeral();
            }
        }

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private decimal SomaTotal()
        {
            decimal result = 0;

            foreach (LIS_ORDEMSERVICOSFECHEntity item in LIS_ORDEMSERVICOSFECHColl)
            {
                result += Convert.ToDecimal(item.TOTALFECHOS);
            }
            return result;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
              
        }

        private void matricialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
            {
                if (BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALREDE == "S")
                    ImprimirMatricialBobina("rede");
                else
                    ImprimirMatricialBobina("local");
            }
        }

        private void ImprimirMatricialBobina(string local)
          {
              try
              {
                  ImprimeTexto imp = new ImprimeTexto();

                  //Armazena na coleção do Fechamento O.S Selecionada
                  RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                  RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", _IDORDEMSERVICO.ToString()));
                  LIS_ORDEMSERVICOSFECHPrinColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio);


                  string PathRegistro = ConfigSistema1.Default.PathInstall + @"\CapturaPorta.bat";
                  FileInfo t = new FileInfo(PathRegistro);
                  StreamWriter Tex = t.CreateText();

                  //Porta da impressora
                  if (local == "local")
                  {
                      //Limpa alguma configuração anterior
                      Tex.WriteLine("NET USE LPT1: /DELETE");
                      Tex.Close();

                      //Executa o bat para captura a porta da impressora na rede
                      System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro);
                      Processo1.WaitForExit(); //aguarda o termino do processo.

                      imp.Inicio(BmsSoftware.ConfigSistema1.Default.PORTAMATLOCAL);
                  }
                  else if (local == "rede")
                  {
                      Tex.WriteLine("NET USE LPT1: /DELETE");
                      Tex.WriteLine("NET USE LPT1: " + BmsSoftware.ConfigSistema1.Default.CAMINHOMATREDE + "  persistent:yes");
                      Tex.Close();

                      //Executa o bat para captura a porta da impressora na rede
                      System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro);
                      Processo1.WaitForExit(); //aguarda o termino do processo.

                      imp.Inicio("LPT1");
                  }


                  //Cabeçalho
                  EMPRESAEntity EmpresaTy = new EMPRESAEntity();
                  EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                  EmpresaTy = EMPRESAP.Read(1);

                  //1º Via
                  imp.ImpLF(imp.Comprimido);
                  imp.ImpLF(Util.RemoverAcentos(EmpresaTy.NOMECLIENTE));
                  imp.ImpLF(Util.RemoverAcentos(EmpresaTy.ENDERECO) + " - " + EmpresaTy.BAIRRO);
                  imp.ImpLF(EmpresaTy.CIDADE + " - " + EmpresaTy.UF);
                  imp.ImpLF("Telefone: " + EmpresaTy.TELEFONE);
                  imp.ImpLF("CNPJ: " + EmpresaTy.CNPJCPF + "  " + EmpresaTy.IE);
                  imp.ImpLF("Data: " + DateTime.Now.ToString("dd/MM/yyyy HH:MM"));

                  imp.ImpLF("-------------------------------------------------");
                  imp.ImpLF("ORDEM DE SERVICO N." + Entity.IDORDEMSERVICO.ToString().PadLeft(6, '0'));

                 //Armazena dados do cliente
                  LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                  LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                  RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                  RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                  LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

                  imp.ImpLF("-------------------------------------------------");
                  imp.ImpLF(Util.RemoverAcentos("Cliente: " + LIS_CLIENTEColl[0].IDCLIENTE + " - " + Util.LimiterText(LIS_CLIENTEColl[0].NOME, 35)));
                  imp.ImpLF(Util.RemoverAcentos("Endereco: " + Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1, 40)));
                  imp.ImpLF(Util.RemoverAcentos("Cidade: " + Util.LimiterText(LIS_CLIENTEColl[0].MUNICIPIO, 40)) + " UF: " + LIS_CLIENTEColl[0].UF);
                  imp.ImpLF("Tel.: " + LIS_CLIENTEColl[0].TELEFONE1);

                  //Itens de Serviços
                  imp.ImpLF("Servicos");
                  imp.ImpCol(0, "Quant.");
                  imp.ImpCol(6, "Descricao");
                  imp.ImpCol(24, "Vl. Unit");
                  imp.ImpCol(39, "Vl. Total");
                  imp.Pula(1);
                  foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHColl)
                  {
                      imp.ImpCol(0, item.QUANTIDADE.ToString());
                      imp.ImpCol(6, Util.RemoverAcentos(Util.LimiterText(item.NOMESERVICO, 20)));
                      imp.ImpCol(24, String.Format("{0,12}", Convert.ToDecimal(item.VALORUNITARIO).ToString("n2")));
                      imp.ImpCol(36, String.Format("{0,12}", Convert.ToDecimal(item.VALORTOTAL).ToString("n2")));
                      imp.Pula(1);
                  }

                  imp.ImpLF("-------------------------------------------------");

                  //Itens de Produtos
                  imp.ImpLF("Produtos");
                  imp.ImpCol(0, "Quant.");
                  imp.ImpCol(6, "Descricao");
                  imp.ImpCol(24, "Vl. Unit");
                  imp.ImpCol(39, "Vl. Total");
                  imp.Pula(1);
                  foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                  {
                      imp.ImpCol(0, item.QUANTIDADE.ToString());
                      imp.ImpCol(6, Util.RemoverAcentos(Util.LimiterText(item.NOMEPRODUTO, 20)));
                      imp.ImpCol(24, String.Format("{0,12}", Convert.ToDecimal(item.VALORUNITARIO).ToString("n2")));
                      imp.ImpCol(36, String.Format("{0,12}", Convert.ToDecimal(item.VALORTOTAL).ToString("n2")));
                      imp.Pula(1);
                  }

                  imp.ImpLF("-------------------------------------------------");

                  imp.ImpCol(20, "Total de Servicos:");
                  imp.ImpCol(36, String.Format("{0,12}", txtTotalServicos.Text));

                  imp.Pula(1);
                  imp.ImpCol(20, "Total de Produtos:");
                  imp.ImpCol(36, String.Format("{0,12}", txtTotalPecas.Text));

                  imp.Pula(1);
                  imp.ImpCol(20, "Mao de Obra:");
                  imp.ImpCol(36, String.Format("{0,12}", TotalMaoObra.Text));

                  imp.Pula(1);
                  imp.ImpCol(20, "Outros:");
                  imp.ImpCol(36, String.Format("{0,12}", txtValorOutros.Text));

                  imp.Pula(1);
                  imp.ImpCol(20, "Total de O.S:");
                  imp.ImpCol(36, String.Format("{0,12}", txtTotalOS.Text));

                  imp.Pula(1);
                  // imp.ImpLF("Funcionario: " +Util.LimiterText(Util.RemoverAcentos(LIS_ORDEMSERVICOPrint[0].NOMEFUNCIONARIO),30));
                  imp.ImpCol(0, "Garantia");
                  imp.ImpCol(15, "Cond. de Pagamento");
                  imp.Pula(1);
                  imp.ImpCol(0, mkdEntrega.Text);
                  imp.ImpCol(15, LIS_ORDEMSERVICOSFECHPrinColl[0].NOMEFORMAPAGTO);
                  imp.Pula(5);

                  imp.Fim();
              }
              catch (Exception)
              {

                  MessageBox.Show("Não foi possível imprimir!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
              }
          }

        private void lasesJatoDeTintaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ImprimirOrdemServico2()
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
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

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
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

        private void boletaBancáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não existe duplicata lançada!");
                tabFechOS.SelectTab(2);
                dataGridDupl.Focus();                
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
                                //Safra
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

            printDialog1.Document = printDocument3;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
                    printDocument3.DefaultPageSettings.PaperSize = new
                    System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

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
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                DUPLICATARECEBEREntity item =  DUPLICATARECEBERP.Read(_CodDuplicata);

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

        private void viasToolStripMenuItem_Click(object sender, EventArgs e)
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

            printDialog1.Document = printDocument4;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    _CodDuplicata = Convert.ToInt32(item.IDDUPLICATARECEBER);
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
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 548);

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
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 770);

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

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
                txtCriterioPesquisa.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            Grava();
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
            {
                ORDEMSERVICOSFECHP.Save(Entity);

                if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "1")
                    modelo2ToolStripMenuItem_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "2")
                    modelo2ToolStripMenuItem1_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "3")
                    modelo3ToolStripMenuItem_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "4")
                    modelo4ToolStripMenuItem_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "5")
                    modelo5ToolStripMenuItem_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "6")
                    modelo6ToolStripMenuItem_Click(null, null);
                else if (BmsSoftware.ConfigSistema1.Default.ModeloImpressaoOS == "7")
                    PrintTicketModelo2();
                else
                    modelo2ToolStripMenuItem_Click(null, null);
            }

               
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
                txtCriterioPesquisa.Focus();
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

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            int CodigoSelect = -1;
            if (LIS_ORDEMSERVICOSFECHColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[rowindex].IDORDEMSERVICO);
                    tabFechOS.SelectTab(0);

                    Entity = ORDEMSERVICOSFECHP.Read(CodigoSelect);
                    ListaItensServico(_IDORDEMSERVICO);
                    ListaItensPecas(_IDORDEMSERVICO);
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO); 

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    CodigoSelect = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[rowindex].IDORDEMSERVICO);

                    Entity = ORDEMSERVICOSFECHP.Read(CodigoSelect);
                    ListaItensServico(_IDORDEMSERVICO);
                    ListaItensPecas(_IDORDEMSERVICO);
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);

                    apagaToolStripMenuItem_Click(null, null);

                    Entity = null;
                    Entity2 = null;

                }
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CodigoSelect = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[rowindex].IDORDEMSERVICO);

                    Entity = ORDEMSERVICOSFECHP.Read(CodigoSelect);
                    ListaItensServico(_IDORDEMSERVICO);
                    ListaItensPecas(_IDORDEMSERVICO);
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);

                    TSBPrint_Click(null, null);

                    Entity = null;
                    Entity2 = null;

                }
            }
        }
       

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_ORDEMSERVICOSFECHColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[indice].IDORDEMSERVICO);

                if (e.KeyCode == Keys.Enter)  
                {
                    tabFechOS.SelectTab(0);
                    Entity = ORDEMSERVICOSFECHP.Read(CodigoSelect);
                   
                    ListaItensServico(_IDORDEMSERVICO);
                    ListaItensPecas(_IDORDEMSERVICO);
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);
                  
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            ORDEMSERVICOSFECHP.Delete(CodigoSelect);
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
        }        

        private void compostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(1);
            }
            else
                ImprimirDuplicata1ViaComposta();
        }

        private void ImprimirDuplicata1ViaComposta()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument5;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument5.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                objPrintPreview.Document = printDocument5;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                //Filtro das duplicatas compostas
                RowsFiltroCollection RowDuplicata = new RowsFiltroCollection();
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "OF" + _IDORDEMSERVICO.ToString()));
                DUPLICATARECEBERCollection DUPLICATARECEBERCollC = new DUPLICATARECEBERCollection();
                DUPLICATARECEBERCollC = DUPLICATARECEBERP.ReadCollectionByParameter(RowDuplicata, "DATAVECTO");

                //Busca o ultimo vecto
                //e soma os totais da duplicata
                Decimal TotalDuplicata = 0;
                DateTime UltimoVecto = Convert.ToDateTime(DUPLICATARECEBERCollC[DUPLICATARECEBERCollC.Count -1 ].DATAVECTO);
                foreach (DUPLICATARECEBEREntity item in DUPLICATARECEBERCollC)
                {
                    TotalDuplicata += Convert.ToDecimal(item.VALORDEVEDOR);
                    UltimoVecto = Convert.ToDateTime(item.DATAVECTO);
                }


                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString("OF" + _IDORDEMSERVICO.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString("OF" + _IDORDEMSERVICO.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


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

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbCliente.SelectedValue = result;
                }
            }
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
        } 

        private void listaGeralServiçosEProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaServico FrmListaServicoP = new FrmListaServico();
            //FrmListaServicoP.LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHColl;
            FrmListaServicoP.ShowDialog();
        }

        private void ImprimirListaGeralProduto()
        {
            
            ////define o titulo do relatorio
            IndexRegistro = 0;

            try
            {

                RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Serviços - Serviços e Produtos");
                ////define o titulo do relatorio
                IndexRegistro = 0;

                ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
                try
                {
                    ////  'define o objeto para visualizar a impressao
                    PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                    printDialog1.Document = printDocument6;
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        objPrintPreview.Document = printDialog1.Document;
                        objPrintPreview.WindowState = FormWindowState.Maximized;
                        objPrintPreview.PrintPreviewControl.Zoom = 1;
                        objPrintPreview.Text = RelatorioTitulo;
                        objPrintPreview.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

                }

              

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument6_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
                e.Graphics.DrawString("O.S", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 120, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 340, 170);
                e.Graphics.DrawString("Funcionario", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, 170);
                e.Graphics.DrawString("Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_ORDEMSERVICOSFECHColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_ORDEMSERVICOSFECHColl.Count)
                {
                    if (LIS_ORDEMSERVICOSFECHColl[IndexRegistro].IDORDEMSERVICO != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].IDORDEMSERVICO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].NOMEFUNCIONARIO, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].TOTALFECHOS).ToString("n2");
                        e.Graphics.DrawString(TotalFOS, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);
                   

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    //Listar os produtos
                    LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHPrintColl = new LIS_PRODUTOOSFECHCollection();
                    LIS_PRODUTOOSFECHPrintColl = ProdutoRel(Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].IDORDEMSERVICO));
                    e.Graphics.DrawString("Cod.Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Produtos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHPrintColl)
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


                    //Listar os Serviços
                    LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHPrintColl = new LIS_SERVICOOSFECHCollection();
                    LIS_SERVICOOSFECHPrintColl = ServicoRel(Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[IndexRegistro].IDORDEMSERVICO));
                    e.Graphics.DrawString("Cod.Serviço", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Serviço", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHPrintColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Util.LimiterText(item.IDSERVICO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.NOMESERVICO, 25), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.QUANTIDADE.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    config.LinhaAtual++;                    
                    string linhasepar = "------------------------------------------------------------------------------------------";
                    string linhasepar2 = "------------------------------------------------------------------------------------------";
                    e.Graphics.DrawString(linhasepar + linhasepar2, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 10);

                    IndexRegistro++;
                    config.LinhaAtual++;

                  }
                    IndexRegistro++;
                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_ORDEMSERVICOSFECHColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_ORDEMSERVICOSFECHColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

        private LIS_PRODUTOOSFECHCollection ProdutoRel(int IDORDEMSERVICO)
        {
            LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));

            LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOOSFECHColl;
        }

        private LIS_SERVICOOSFECHCollection ServicoRel(int IDORDEMSERVICO)
        {
            LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));

            LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            return LIS_SERVICOOSFECHColl;
        }

        private void printDocument6_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument5_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument4_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_ORDEMSERVICOSFECHColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_ORDEMSERVICOSFECHEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_ORDEMSERVICOSFECHEntity>(orderBy);

                LIS_ORDEMSERVICOSFECHColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_ORDEMSERVICOSFECHColl;
            }
        }

        private void lnkSelecCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnCadProdMTQ_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProdutoMTQ.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();
                GetDropProdutosMTQOS();
                cbProdutoMTQ.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutosMTQOS()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));
            PRODUTOSMTColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

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
                        txtvalorunitMTQ.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void cbProdutoMTQ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void cbProdutoMTQ_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProdutoMTQ.SelectedIndex > 0)
            {
                decimal ValorVenda = Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue)).VALORVENDA1);
                txtVlMtrMTQ.Text = ValorVenda.ToString("n2");

                PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProdutoMTQ.SelectedValue));

                if (PRODUTOS_EstoqTy.PORCPERDAPROD != null)
                    txtPorcPerda.Text = Convert.ToDecimal(PRODUTOS_EstoqTy.PORCPERDAPROD).ToString("n3");
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
                    SomaUnitMTQ();
                }
            }
            else
                txtQuantMTQ.Text = "0,00";
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
                txtAlturaMTQ.Text = "0,00";
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
                    txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n3");

                    if (PRODUTOSTyEn.FLAGDECIMALREND == "S")
                    {
                        //Pegando somente os decimais
                        int pos = txtTotalMTQ.Text.IndexOf(",");
                        string valorinicial = txtTotalMTQ.Text.Substring(0, pos);
                        decimal valorret = Convert.ToDecimal(txtTotalMTQ.Text.Substring(pos + 1, 3));

                        if (valorret % PRODUTOSTyEn.MULTAREND != 0)
                            do
                            {
                                valorret += Convert.ToDecimal("1");

                            }
                            while (Convert.ToDecimal(valorret) % PRODUTOSTyEn.MULTAREND != 0);

                        txtTotalMTQ.Text = valorinicial + "," + valorret.ToString();
                    }
                    else
                    {
                        //Numero inteiros
                        txtTotalMTQ.Text = (Math.Round(Convert.ToDecimal(txtTotalMTQ.Text))).ToString("n3");
                        decimal valorret = Convert.ToDecimal(txtTotalMTQ.Text);

                        if (valorret % PRODUTOSTyEn.MULTAREND != 0)
                            do
                            {
                                valorret += Convert.ToDecimal("1");
                            }
                            while (Convert.ToDecimal(valorret) % PRODUTOSTyEn.MULTAREND != 0);


                        txtTotalMTQ.Text = valorret.ToString("n3");
                    }
                }
                else
                    txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n3");
            }
            else
                   txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n4");

            txtvalorunitMTQ.Text = (Convert.ToDecimal(txtVlMtrMTQ.Text) * (Convert.ToDecimal(txtTotalMTQ.Text) + Convert.ToDecimal(txtPerdaMT2.Text))).ToString("n2");
            txtVlTotalMTQ.Text = (Convert.ToDecimal(txtvalorunitMTQ.Text) * Convert.ToDecimal(txtQuantMTQ.Text)).ToString("n2");
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
                txtVlMtrMTQ.Text = "0,0000";
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
                txtvalorunitMTQ.Text = "0,0000";
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
             GravaProdutoMTQOS();
        }

        private void GravaProdutoMTQOS()
        {
            try
            {
                if (Validacoes() && ValidacoesProdutosMTQ())
                {
                    _IDORDEMSERVICO = ORDEMSERVICOSFECHP.Save(Entity);
                   this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');

                    PRODUTOSPEDIDOMTQOSP.Save(Entity6);
                    ListaProdutoPedidoMTQOS(_IDORDEMSERVICO);

                    //Salva a O.S
                    ORDEMSERVICOSFECHP.Save(Entity);

                    Entity6 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProdutoMTQ.Focus();

                    txtPorComisVend_Validating(null, null);
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaProdutoPedidoMTQOS(int IDORDEMSERVICO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                LIS_PRODUTOSPEDIDOMTQOSColl = LIS_PRODUTOSPEDIDOMTQOSP.ReadCollectionByParameter(RowRelatorio);

                dtgProdMTQOS.AutoGenerateColumns = false;
                dtgProdMTQOS.DataSource = LIS_PRODUTOSPEDIDOMTQOSColl;
                lblTotalCountMTQ.Text = "Nº de Produtos MT2: " + LIS_PRODUTOSPEDIDOMTQOSColl.Count.ToString();

                SumTotalProdutosPedidoMTQOS();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalProdutosPedidoMTQOS()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOMTQOSEntity item in LIS_PRODUTOSPEDIDOMTQOSColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProdMTQ.Text = total.ToString("n2");
            txtTotalProdMTQ2.Text = total.ToString("n2");
            SumTotalFechOS();
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

        private void dtgProdMTQOS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOMTQOSColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQOSColl[rowindex].IDPRODUTOSPEDIDOMTQOS);
                    Entity6 = PRODUTOSPEDIDOMTQOSP.Read(CodSelect);

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
                                    CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQOSColl[rowindex].IDPRODUTOSPEDIDOMTQOS);
                                    int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOSPEDIDOMTQOSColl[rowindex].IDPRODUTO);
                                    PRODUTOSPEDIDOMTQOSP.Delete(CodSelect);
                                    ListaProdutoPedidoMTQOS(_IDORDEMSERVICO);

                                    Entity6 = null;

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
        }

        private void btnCancelMTQ_Click(object sender, EventArgs e)
        {
            Entity6 = null;
        }

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaOS1 FrmVendaDiariaOS = new FrmVendaDiariaOS1();
            FrmVendaDiariaOS.ShowDialog();
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
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

                ticket.AddSubHeaderLine("O.S N." + Entity.IDORDEMSERVICO.ToString().PadLeft(6, '0'));
                ticket.AddSubHeaderLine("CLIENTE: " + cbCliente.Text);
                ticket.AddSubHeaderLine("FUNC.: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());


                ticket.AddItem("Quant", "Produto/Servico", "Total");

                foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMESERVICO, Util.LimiterText(ValorTotal, 20));
                }

                foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalServicos.Text) + Convert.ToDecimal(txtTotalPecas.Text);
                ticket.AddTotal("SUBTOTAL", SUBTOTAL.ToString("n2"));
                ticket.AddTotal("MAO OBRA", TotalMaoObra.Text);
                ticket.AddTotal("OUTROS", txtValorOutros.Text);
                ticket.AddTotal("TOTAL", txtTotalOS.Text);

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

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void EntradaCaixa()
        {
            try
            {
                if (!VerificaCaixaOS("OS" + _IDORDEMSERVICO))
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

                    CaixaTy.NDOCUMENTO = "OS" + _IDORDEMSERVICO;
                    CaixaTy.OBSERVACAO = "Ordem de Serviço nº " + "OS" + _IDORDEMSERVICO + " Cliente: " + cbCliente.SelectedValue + " - " + cbCliente.Text;

                    CaixaP.Save(CaixaTy);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }


        private Boolean VerificaCaixaOS(string NDOCUMENTO)
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

        private void produtosMaisVendisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidos2 FrmP = new FrmProdutosMaisVendidos2();
            FrmP.ShowDialog();
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

        private void vendaDeProdutoPorGrupoCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaGrupoCategoria2 Frm = new FrmVendaGrupoCategoria2();
            Frm.ShowDialog();
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
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
                    ORDEMSERVICOSFECHEntity ORDEMSERVICOSFECHTy = new ORDEMSERVICOSFECHEntity();
                    ORDEMSERVICOSFECHTy = ORDEMSERVICOSFECHP.Read(_IDORDEMSERVICO);
                    ORDEMSERVICOSFECHTy.FLAGTELABLOQUEADA = "S";
                    ORDEMSERVICOSFECHP.Save(ORDEMSERVICOSFECHTy);

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

        private void ImprimirReceitaReport()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            using (FrmRelatOrdemServico frm = new FrmRelatOrdemServico())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;

                if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
                {
                    frm.PLACA = "Placa: " + cbVeiculo.Text;

                    //Busca Marca Modelo
                    LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();
                    LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                    LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_VEICULOCLIENTEColl.Count > 0)
                        frm.MARCAMODELO = LIS_VEICULOCLIENTEColl[0].MARCAMODELO;
                }
                else
                {
                    frm.PLACA = " ";
                    frm.MARCAMODELO = " ";
                }


                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

            this.Cursor = Cursors.Default;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário gravar a Ordem de Serviço!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            if (chkTelaBloqueada.Checked)
            {
                string msgerro = "Tela bloqueada!";
                errorProvider1.SetError(chkTelaBloqueada, msgerro);
                Util.ExibirMSg(msgerro, "Red");
            }
            else
            {
                using (FrmVariosLancamentoReceberPedido frm = new FrmVariosLancamentoReceberPedido())
                {
                    frm.CodCliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.NumPedido = _IDORDEMSERVICO.ToString();
                    frm.DataPedido = msktDataEmissao.Text;
                    frm.ValorPedido = txtValorDev.Text == "0,00" ? txtTotalFinanceiro.Text : txtValorDev.Text;
                    frm.NotaFiscal = "OS" + _IDORDEMSERVICO.ToString();
                    frm.ShowDialog();

                    //Lista Duplicatas do Pedido
                    GridDuplicatasOSr(Convert.ToInt32(cbCliente.SelectedValue), _IDORDEMSERVICO);
                }
            }
        }

        private void modelo2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {

                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
                    ImprimirReceitaReportMod2();
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

        private void ImprimirReceitaReportMod2()
        {
            using (FrmRelatOrdemServicoMod2 frm = new FrmRelatOrdemServicoMod2())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;
                frm.ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void ordemDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
           
        }

        private void GetFuncionarioComissao()
        {
           
        }

        private void cbComissaoFunci_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPorComisVend_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void cbFuncionario_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtValComissao_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbFuncionario.SelectedIndex > 0)
                {
                    FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                    decimal PorcComissVend = Convert.ToDecimal(FUNCIONARIOP.Read(Convert.ToInt32(cbFuncionario.SelectedValue)).COMISSAO);

                    cbFuncionario2.SelectedValue = cbFuncionario.SelectedValue;
                    cbFuncionario3.SelectedValue = cbFuncionario.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular a comissão!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void vendaPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
             FrmComissaoporOS Frm = new FrmComissaoporOS();
             Frm.ShowDialog();
        }

        private void txtValComissao_Enter(object sender, EventArgs e)
        {
           
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
                    ORDEMSERVICOSFECHEntity ORDEMSERVICOSFECHTy = new ORDEMSERVICOSFECHEntity();
                    ORDEMSERVICOSFECHTy = ORDEMSERVICOSFECHP.Read(_IDORDEMSERVICO);
                    ORDEMSERVICOSFECHTy.FLAGTELABLOQUEADA = "S";
                    ORDEMSERVICOSFECHP.Save(ORDEMSERVICOSFECHTy);

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

        private void txtTotalMTQ_Leave(object sender, EventArgs e)
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

        private void txtPorcPerda_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtPerdaMT2_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtQuantMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtAlturaMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void txtLarguraMTQ_Leave(object sender, EventArgs e)
        {
            SomaPerdaMT2();
        }

        private void serviçosProdutoPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorOS2 Frm = new FrmVendaVendedorOS2();
            Frm.ShowDialog();
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex > 0)
            {
                VerificaDebitoCliente(Convert.ToInt32(cbCliente.SelectedValue));
                
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                if (CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).FLAGBLOQUEADO.TrimEnd() == "S")
                {
                    using (FrmBloqueado frm = new FrmBloqueado())
                    {
                        frm.ShowDialog();
                    }
                }

                GetDropVeiculoCliente(Convert.ToInt32(cbCliente.SelectedValue));
            }
            else
                GetDropVeiculoCliente(-1);
        }

        private void GetDropVeiculoCliente(int IDCLIENTE)
        {
            try
            {
                
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio, "PLACA");
                cbVeiculo.DisplayMember = "PLACA";
                cbVeiculo.ValueMember = "IDVEICULOCLIENTE";

                LIS_VEICULOCLIENTEEntity LIS_VEICULOCLIENTETy = new LIS_VEICULOCLIENTEEntity();
                LIS_VEICULOCLIENTETy.PLACA = ConfigMessage.Default.MsgDrop;
                LIS_VEICULOCLIENTETy.IDVEICULOCLIENTE = -1;
                LIS_VEICULOCLIENTEColl.Add(LIS_VEICULOCLIENTETy);

                Phydeaux.Utilities.DynamicComparer<LIS_VEICULOCLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_VEICULOCLIENTEEntity>(cbVeiculo.DisplayMember);

                LIS_VEICULOCLIENTEColl.Sort(comparer.Comparer);
                cbVeiculo.DataSource = LIS_VEICULOCLIENTEColl;

                cbVeiculo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
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
                frm.TituloSelec = "Relação de Ordem de Serviços";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void listaGeralDeVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaServicoProd FrmLSP = new FrmListaServicoProd();
            FrmLSP.ShowDialog();
        }

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

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
                const string fromPassword = "Rmr877701c";

                string subject = string.Empty;
                subject = "ORDEM SERVIÇO Nº " + _IDORDEMSERVICO;
                

                string body = string.Empty;
                body = "Caro(a) cliente " + CLIENTETy.NOME + ", segue abaixo dados do sua Ordem de Serviço <br>";              
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
                foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMESERVICO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                foreach (LIS_PRODUTOSPEDIDOMTQOSEntity item in LIS_PRODUTOSPEDIDOMTQOSColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                foreach (LIS_EQUIPAMENTOOSFECHEntity item in LIS_EQUIPAMENTOOSFECHColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEEQUIPAMENTO + "</td>");
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
                   // Host = "mail.imexsistema.com.br",
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

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void checkListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
            printDocument1.DefaultPageSettings.Landscape = true;

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

        private void printDocument1_BeginPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();

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

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 850, 30);

                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50) + " CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);


            e.Graphics.DrawString("Placa: " + cbVeiculo.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 80);
            e.Graphics.DrawString("O.S: " + _IDORDEMSERVICO, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, 80);
            e.Graphics.DrawString("Data: " + msktDataEmissao.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 80);
            e.Graphics.DrawString("Cliente: " + cbCliente.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 95);
            e.Graphics.DrawString("Assinatura: ______________________________________________ ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 150);
            

            if(File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage+ @"\Carro.png"))
            {
                MemoryStream streamVeicu = new MemoryStream(GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\Carro.png"));
                 e.Graphics.DrawImage(Image.FromStream(streamVeicu), config.MargemEsquerda - 20, 200, 958, 346);//958; 346
            }
        }

        public byte[] GetFoto(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }

        private void btnCadEsquip_Click(object sender, EventArgs e)
        {
            using (FrmEquipamento frm = new FrmEquipamento())
            {
                int CodSelec = Convert.ToInt32(cbEquipamento.SelectedValue);
                frm._IDEQUIPAMENTO = CodSelec;
                frm.ShowDialog();

                GetDropEquipamento();
                cbEquipamento.SelectedValue = CodSelec;
            }
        }

        private void GetDropEquipamento()
        {
            EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
            EQUIPAMENTOColl = EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbEquipamento.DisplayMember = "NOME";
            cbEquipamento.ValueMember = "IDEQUIPAMENTO";

            EQUIPAMENTOEntity EQUIPAMENTOTy = new EQUIPAMENTOEntity();
            EQUIPAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            EQUIPAMENTOTy.IDEQUIPAMENTO = -1;
            EQUIPAMENTOColl.Add(EQUIPAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity>(cbEquipamento.DisplayMember);

            EQUIPAMENTOColl.Sort(comparer.Comparer);
            cbEquipamento.DataSource = EQUIPAMENTOColl;

            cbEquipamento.SelectedIndex = 0;
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void button6_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);
            monthCalendar4.ShowWeekNumbers = true;

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(250, 156);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedtxtData.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }


        public MonthCalendar monthCalendar5 = new MonthCalendar();
        Form FormCalendario5 = new Form();
        private void button5_Click(object sender, EventArgs e)
        {
            monthCalendar5.Name = "monthCalendar5";
            monthCalendar5.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar5_DateSelected);
            monthCalendar5.ShowWeekNumbers = true;
            FormCalendario5.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario5.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario5.ClientSize = new System.Drawing.Size(250, 156);
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
            mdkDataFinal.Text = monthCalendar5.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario5.Close();
        }

        private void btnAdicionaEquip_Click(object sender, EventArgs e)
        {
            
                if (Validacoes() && ValidacoesEquipamento())
                {

                    try
                    {
                        _IDORDEMSERVICO = ORDEMSERVICOSFECHP.Save(Entity);
                        this.Text = "Ordem de Serviço - Nº " + _IDORDEMSERVICO.ToString().PadLeft(6, '0');

                    //Grava Equipamento
                    EQUIPAMENTOOSFECHP.Save(Entity4);

                        //Lista os itens de serviços cadastrados;
                        ListaEquipamentoServico(_IDORDEMSERVICO);

                        ORDEMSERVICOSFECHP.Save(Entity);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Entity4 = null;
                        cbEquipamento.Focus();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }


                }
            
        }

        private void ListaEquipamentoServico(int IDORDEMSERVICO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                LIS_EQUIPAMENTOOSFECHColl = LIS_EQUIPAMENTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                dtEquipamento.AutoGenerateColumns = false;
                dtEquipamento.DataSource = LIS_EQUIPAMENTOOSFECHColl;

                SumTotalEquipamentoServico();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalEquipamentoServico()
        {
            decimal total = 0;
            foreach (LIS_EQUIPAMENTOOSFECHEntity item in LIS_EQUIPAMENTOOSFECHColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalEquip.Text = total.ToString("n2");
            SumTotalFechOS();
        }


        private Boolean ValidacoesEquipamento()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbEquipamento.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbEquipamento, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValoUNitTiLoca.Text))
            {
                errorProvider1.SetError(txtValoUNitTiLoca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtQuantEqui.Text) || Convert.ToInt32(txtQuantEqui.Text) < 1)
            {
                errorProvider1.SetError(txtQuantEqui, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantTipoLocacao.Text) || Convert.ToDecimal(txtQuantTipoLocacao.Text) < 0)
            {
                errorProvider1.SetError(txtQuantTipoLocacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
          
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.Clear();

            if (Convert.ToInt32(cbEquipamento.SelectedValue) > 0)
            {
                EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
                int IDstatus = Convert.ToInt32(EQUIPAMENTOP.Read(Convert.ToInt32(cbEquipamento.SelectedValue)).IDSTATUS);
                STATUSProvider STATUSP = new STATUSProvider();
                string FlagSatus = STATUSP.Read(IDstatus).FLAGMOVIMENTACAO.TrimEnd().TrimStart();

                if (FlagSatus == "S" && _IDORDEMSERVICO == -1)
                {
                    STATUSEntity STATUSTy = new STATUSEntity();
                    STATUSTy = STATUSP.Read(IDstatus);
                    string MSGerro = "Não é possível efetuar a logação, equipamento está com o status: " + STATUSTy.NOME;
                    result = false;
                    errorProvider1.SetError(cbProduto, MSGerro);
                    MessageBox.Show(MSGerro);
                }
            }


            return result;
        }

        private void cbTipoLocacao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTipoLocacao.SelectedIndex == 0)
            {
                lblQuantTipoLocacao.Text = "Tempo/Hora:";
                lblValUnitTipoLocacao.Text = "Vl.Unit/Hora:";
            }
            else if (cbTipoLocacao.SelectedIndex == 1)
            {
                lblQuantTipoLocacao.Text = "Tempo/Dia:";
                lblValUnitTipoLocacao.Text = "Vl.Unit/Dia:";
            }
            else if (cbTipoLocacao.SelectedIndex == 2)
            {
                lblQuantTipoLocacao.Text = "Tempo/Mês:";
                lblValUnitTipoLocacao.Text = "Vl.Unit/Mês:";
            }

            if (cbEquipamento.SelectedIndex > 0)
            {
              // if (!_EditRegistro)
                {
                    EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
                    EQUIPAMENTOEntity EQUIPAMENTOTy = EQUIPAMENTOP.Read(Convert.ToInt32(cbEquipamento.SelectedValue));

                    if (cbTipoLocacao.SelectedIndex == 0)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALOR).ToString("N2");
                    else if (cbTipoLocacao.SelectedIndex == 1)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALORDIA).ToString("N2");
                    else if (cbTipoLocacao.SelectedIndex == 2)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALORMES).ToString("N2");
                }
            }
        }

        private void cbEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEquipamento.SelectedIndex > 0)
            {
                EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
                EQUIPAMENTOEntity EQUIPAMENTOTy = EQUIPAMENTOP.Read(Convert.ToInt32(cbEquipamento.SelectedValue));

                if (EQUIPAMENTOTy.VALOR != null)
                {
                    if (cbTipoLocacao.SelectedIndex == 0)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALOR).ToString("N2");
                    else if (cbTipoLocacao.SelectedIndex == 1)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALORDIA).ToString("N2");
                    else if (cbTipoLocacao.SelectedIndex == 2)
                        txtValoUNitTiLoca.Text = Convert.ToDecimal(EQUIPAMENTOTy.VALORMES).ToString("N2");

                    STATUSProvider STATUSP = new STATUSProvider();
                    lblSituacaoEquip.Text = STATUSP.Read(Convert.ToInt32(EQUIPAMENTOTy.IDSTATUS)).NOME;
                }
            }
            else
                lblSituacaoEquip.Text = "Nenhum Selecionado";
        }

        private void txtQuantTipoLocacao_Validating(object sender, CancelEventArgs e)
        {
            if (txtQuantTipoLocacao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantTipoLocacao.Text))
                {
                    errorProvider1.SetError(txtQuantTipoLocacao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtQuantTipoLocacao.Text);
                    txtQuantTipoLocacao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtQuantTipoLocacao, "");
                }
            }
            else
                txtQuantTipoLocacao.Text = "0,00";
        }

        private void txtValoUNitTiLoca_Validating(object sender, CancelEventArgs e)
        {
            if (txtValoUNitTiLoca.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValoUNitTiLoca.Text))
                {
                    errorProvider1.SetError(txtValoUNitTiLoca, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValoUNitTiLoca.Text);
                    txtValoUNitTiLoca.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValoUNitTiLoca, "");
                }
            }
            else
                txtValoUNitTiLoca.Text = "0,00";
        }

        private void txtHorimetroInicial_Leave(object sender, EventArgs e)
        {
            SomaHorimetro();
        }

        private void SomaHorimetro()
        {
            try
            {
                //txtHorimetroInicial.Text = Util.RetiraLetras(txtHorimetroInicial.Text);
                // txtHorimetroFinal.Text = Util.RetiraLetras(txtHorimetroFinal.Text);

                if (ValidacoesLibrary.ValidaTipoDecimal(txtHorimetroFinal.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtHorimetroInicial.Text))
                {
                    decimal totalhorimetro = 0;
                    totalhorimetro = Convert.ToDecimal(txtHorimetroFinal.Text) - Convert.ToDecimal(txtHorimetroInicial.Text);
                    txtHorimetroTotal.Text = totalhorimetro.ToString("n2");

                    int pos = txtHorimetroTotal.Text.IndexOf(",");
                    if (pos != -1)
                    {
                        Double f = Convert.ToDouble(txtHorimetroTotal.Text);
                        txtQuantTipoLocacao.Text = string.Format("{0:n2}", f);
                    }
                    else
                    {
                        txtQuantTipoLocacao.Text = txtHorimetroTotal.Text;
                    }


                }
            }
            catch (Exception ex)
            {
                txtHorimetroTotal.Text = "0";
                MessageBox.Show("Não foi possível efetuar cálculo de horímetro!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtHorimetroFinal_Leave(object sender, EventArgs e)
        {
            SomaHorimetro();
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

        private void horasPorEquipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmHorasEquipamento frm = new FrmHorasEquipamento())
            {
                frm.ShowDialog();
            }
        }

        private void dtEquipamento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_EQUIPAMENTOOSFECHColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_EQUIPAMENTOOSFECHColl[rowindex].IDEQUIPAMENTOOSFECH);
                    Entity4 = EQUIPAMENTOOSFECHP.Read(CodSelect);
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
                                CodSelect = Convert.ToInt32(LIS_EQUIPAMENTOOSFECHColl[rowindex].IDEQUIPAMENTOOSFECH);
                                EQUIPAMENTOOSFECHP.Delete(CodSelect);
                                ListaEquipamentoServico(_IDORDEMSERVICO);
                                Entity4 = null;
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

        private void btnCancelEquip_Click(object sender, EventArgs e)
        {
            Entity4 = null;
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

        private void cbMensagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMensagem.SelectedValue) > 0)
                txtObservacao.Text += " " + MENSAGEMP.Read(Convert.ToInt32(cbMensagem.SelectedValue)).MENSAGEM;
        }

        private void pesquisarOClientePelaPlacaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSearchPlaca frm = new FrmSearchPlaca())
            {
                frm.ShowDialog();
                var result = frm.Result;

                cbCliente.SelectedValue = result;
            }
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
                else;
                {

                    Double f = Convert.ToDouble(txtPorcDesconto.Text);
                    txtPorcDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconto, "");

                    txtTotalDesconto.Text =
               ((Convert.ToDecimal(txtTotalOS.Text) * Convert.ToDecimal(txtPorcDesconto.Text)) / 100).ToString("n2");
                    SumTotalFechOS();
                }
            }
            else
                txtPorcDesconto.Text = "0,00";
        }

        private void txtTotalDesconto_Validated(object sender, EventArgs e)
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

                    SumTotalFechOS();
                }
            }
            else
                txtTotalDesconto.Text = "0,00";
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

        private void DataGriewDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                RowRelatorio.Add(new RowsFiltro("NOMEFUNCIONARIO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMESTATUS", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }


                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ORDEMSERVICOSFECHColl;

                    lblTotalPesquisa.Text = LIS_ORDEMSERVICOSFECHColl.Count.ToString();

                    if (LIS_ORDEMSERVICOSFECHColl.Count > 0)
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

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void cbVeiculo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio, "PLACA");

                if(LIS_VEICULOCLIENTEColl.Count > 0)
                {
                    LblDadosVeiculo.Visible = true;
                    LblDadosVeiculo.Text = Util.LimiterText(LIS_VEICULOCLIENTEColl[0].ANOFABR +  "/" + LIS_VEICULOCLIENTEColl[0].ANOMODELO + " - " +  LIS_VEICULOCLIENTEColl[0].MARCAMODELO, 40);
                }
            }
            else
            {
                LblDadosVeiculo.Text = "";
                LblDadosVeiculo.Visible = false;
            }
        }

        private void modelo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
                ImprimirOrdemServico3();
        }

        private void ImprimirOrdemServico3()
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
                    ImprimirReceitaReport3();
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

        private void ImprimirReceitaReport3()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            using (FrmRelatOrdemServico3 frm = new FrmRelatOrdemServico3())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;

                if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
                {
                    frm.PLACA = "Placa: " + cbVeiculo.Text;

                    //Busca Marca Modelo
                    LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();
                    LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                    LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_VEICULOCLIENTEColl.Count > 0)
                        frm.MARCAMODELO = LIS_VEICULOCLIENTEColl[0].MARCAMODELO;
                }
                else
                {
                    frm.PLACA = " ";
                    frm.MARCAMODELO = " ";
                }


                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

            this.Cursor = Cursors.Default;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();

            }
            else
            {
                if (Convert.ToInt32(cbTipo.SelectedValue) < 1)
                {
                    errorProvider1.SetError(cbTipo, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else if (chkTelaBloqueada.Checked)
                {
                    string msgerro = "Tela bloqueada!";
                    errorProvider1.SetError(chkTelaBloqueada, msgerro);
                    Util.ExibirMSg(msgerro, "Red");
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

        private void ordemDeServiçoPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmOSPorProduto frm = new FrmOSPorProduto())
            {
                frm.ShowDialog();                        
             }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FrmOSPorServivo frm = new FrmOSPorServivo())
            {
                frm.ShowDialog();
            }

        }

        private void modelo4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
                ImprimirOrdemServico4();
        }

        private void ImprimirOrdemServico4()
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
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

        private void ImprimirOrdemServico5()
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
                    ImprimirReceitaReport5();
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

        private void ImprimirOrdemServico6()
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);

                txtCriterioPesquisa.Focus();
            }
            else
            {
                if (!chkTelaBloqueada.Checked)
                {
                    ORDEMSERVICOSFECHP.Save(Entity);
                    ImprimirReceitaReport6();
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

        private void ImprimirReceitaReport5()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            using (FrmRelatOrdemServico5 frm = new FrmRelatOrdemServico5())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;

                if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
                {
                    frm.PLACA = "Placa: " + cbVeiculo.Text;

                    //Busca Marca Modelo
                    LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();
                    LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                    LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_VEICULOCLIENTEColl.Count > 0)
                        frm.MARCAMODELO = LIS_VEICULOCLIENTEColl[0].MARCAMODELO;
                }
                else
                {
                    frm.PLACA = " ";
                    frm.MARCAMODELO = " ";
                }


                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

            this.Cursor = Cursors.Default;
        }

        private void ImprimirReceitaReport6()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            using (FrmRelatOrdemServico6 frm = new FrmRelatOrdemServico6())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;

                if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
                {
                    frm.PLACA = "Placa: " + cbVeiculo.Text;

                    //Busca Marca Modelo
                    LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();
                    LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                    LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_VEICULOCLIENTEColl.Count > 0)
                        frm.MARCAMODELO = LIS_VEICULOCLIENTEColl[0].MARCAMODELO;
                }
                else
                {
                    frm.PLACA = " ";
                    frm.MARCAMODELO = " ";
                }


                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

            this.Cursor = Cursors.Default;
        }


        private void ImprimirReceitaReport4()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            using (FrmRelatOrdemServico4 frm = new FrmRelatOrdemServico4())
            {
                frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                frm.IDORDEMSERVICO = _IDORDEMSERVICO;

                if (Convert.ToInt32(cbVeiculo.SelectedValue) > 0)
                {
                    frm.PLACA = "Placa: " + cbVeiculo.Text;

                    //Busca Marca Modelo
                    LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();
                    LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", cbVeiculo.Text));
                    LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_VEICULOCLIENTEColl.Count > 0)
                        frm.MARCAMODELO = LIS_VEICULOCLIENTEColl[0].MARCAMODELO;
                }
                else
                {
                    frm.PLACA = " ";
                    frm.MARCAMODELO = " ";
                }


                frm.ShowDialog();
                this.Cursor = Cursors.Default;
            }

            this.Cursor = Cursors.Default;
        }

        private void configuraçaõDeOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void modelo5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
                ImprimirOrdemServico5();
        }

        private void modelo6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORDEMSERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabFechOS.SelectTab(tabFechOS.TabCount - 1);
            }
            else
                ImprimirOrdemServico6();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (FrmComissaoVendedor2 frm = new FrmComissaoVendedor2())
            {
                frm.ShowDialog();
            }
        }

        private void comissaoPorOrdemDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmComissaoPorOS2 frm = new FrmComissaoPorOS2())
            {
                frm.ShowDialog();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void modelo7TicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintTicketModelo2();
        }

        private void PrintTicketModelo2()
        {
            try
            {
                Entity = ORDEMSERVICOSFECHP.Read(_IDORDEMSERVICO);
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

                ticket.AddSubHeaderLine("Ordem de Serviço Nº: " + Entity.IDORDEMSERVICO.ToString().PadLeft(6, '0'));              
                
                //Exibe endereço do cliente
                if (Convert.ToInt32(cbCliente.SelectedValue) > 1)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32" , "=", cbCliente.SelectedValue.ToString()));
                    LIS_CLIENTECollection LIS_CLIENTEColl_T = new LIS_CLIENTECollection();
                    LIS_CLIENTEColl_T = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if(LIS_CLIENTEColl_T.Count > 0)
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
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());


                if (LIS_PRODUTOOSFECHColl.Count > 0 || LIS_SERVICOOSFECHColl.Count > 0)
                    ticket.AddSubHeaderLine("SERVIÇOS/PRODUTOS");
                else
                    ticket.DrawItems_b = false;

                foreach (LIS_SERVICOOSFECHEntity item in LIS_SERVICOOSFECHColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMESERVICO, Util.LimiterText(ValorTotal, 20));
                }

                foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                {
                    string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                    ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                }              

                ticket.AddTotal("", "");

                if (Convert.ToDecimal(TotalMaoObra.Text) > 0)
                    ticket.AddTotal("MÃO DE OBRA:", TotalMaoObra.Text);

                if (Convert.ToDecimal(txtValorOutros.Text) > 0)
                    ticket.AddTotal("OUTROS:", txtValorOutros.Text);

                decimal SUBTOTAL = Convert.ToDecimal(txtTotalOS.Text);
                ticket.AddTotal("SUBTOTAL", SUBTOTAL.ToString("n2"));
                ticket.AddTotal("DESCONTO", txtTotalDesconto.Text);
                ticket.AddTotal("TOTAL", txtValorDev.Text);
                ticket.AddTotal("PAGO", txtValorPago.Text);

                decimal troco = 0;
                troco = Convert.ToDecimal(txtValorPago.Text) - Convert.ToDecimal(txtValorDev.Text);
                if(troco > 0)
                    ticket.AddTotal("TROCO", troco.ToString("N2"));

                if (txtProblemaInformado.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Problema Informado: " + txtProblemaInformado.Text);

                if (txtProblemaConstatado.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Problema Constatado: " + txtProblemaConstatado.Text);

                if (txtServiçoExecutado.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Serviço Executado: " + txtServiçoExecutado.Text);

                if (txtEquipamento.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Equipamento: " + txtEquipamento.Text);

                if (txtModelo.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Modelo: " + txtModelo.Text);

                if (txtMarca.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Marca: " + txtMarca.Text);

                if (txtAcessorios.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Acessórios: " + txtAcessorios.Text);

                if (txtObservacao.Text.Trim() != string.Empty)
                    ticket.AddFooterLine("Observação: " + txtObservacao.Text);

                ticket.AddFooterLine("");

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
        
       

        private void lblSituacaoEquip_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisaProduto(txtCodigoProduto.Text);
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
                        PRODUTOSTy = null;
                         PRODUTOSTy = PRODUTOSP.Read(result);
                        cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                        txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                        TotalProdutoServico();
                        txtQuanPeca.Focus();
                    }
                }
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                DGDadosPecas.Focus();
            }
        }

        private void PesquisaProduto(string IDPRODUTO)
        {
            PRODUTOSTy = null;
            PRODUTOSTy = PesquisaCodBarra(txtCodigoProduto.Text);

            if (PRODUTOSTy == null)
                PRODUTOSTy = PesquisaCodReferencia(txtCodigoProduto.Text);
            if (PRODUTOSTy == null && ValidacoesLibrary.ValidaTipoInt32(txtCodigoProduto.Text))
                PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(IDPRODUTO));

            if (PRODUTOSTy == null)
            {
                txtCodigoProduto.Focus();
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
                            cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                            txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                            TotalProdutoServico();
                            txtQuanPeca.Focus();                            
                        }
                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                if (PRODUTOSTy.FLAGINATIVO.Trim() != "S")
                {
                    cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                    txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                    TotalProdutoServico();
                    txtQuanPeca.Focus();
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
                                cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                                txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("n2");
                                TotalProdutoServico();
                                txtCodigoProduto.Focus();
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

        private void TotalProdutoServico()
        {
            try
            {
                if (ValidacoesLibrary.ValidaTipoDecimal(txtQuanPeca.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitPecas.Text))
                    txtTotalProduto.Text = (Convert.ToDecimal(txtQuanPeca.Text) * Convert.ToDecimal(txtValorUnitPecas.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            txtCodigoProduto.Focus();
        }

        private void txtQuanPeca_Leave(object sender, EventArgs e)
        {
            TotalProdutoServico();
        }

        private void txtValorUnitPecas_Leave(object sender, EventArgs e)
        {
            TotalProdutoServico();
        }
    }
}
