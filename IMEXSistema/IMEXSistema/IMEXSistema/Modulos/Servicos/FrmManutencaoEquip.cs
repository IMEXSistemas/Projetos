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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmManutencaoEquip : Form
    {
        Utility Util = new Utility();
        MANUTESQUIPAMENTOProvider MANUTESQUIPAMENTOP = new MANUTESQUIPAMENTOProvider();
        LIS_MANUTESQUIPAMENTOProvider LIS_MANUTESQUIPAMENTOP = new LIS_MANUTESQUIPAMENTOProvider();
        LIS_PRODUTOSMANUTProvider LIS_PRODUTOSMANUTP = new LIS_PRODUTOSMANUTProvider();
        PRODUTOSMANUTProvider PRODUTOSMANUTP = new PRODUTOSMANUTProvider();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();
        CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        LIS_MANUTESQUIPAMENTOCollection LIS_MANUTESQUIPAMENTOColl = new LIS_MANUTESQUIPAMENTOCollection();
        EQUIPAMENTOCollection EQUIPAMENTOColl = new EQUIPAMENTOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        FUNCIONARIOCollection FUNCIONARIOExecColl = new FUNCIONARIOCollection();
        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        TIPOMANUTENCAOCollection TIPOMANUTENCAOColl = new TIPOMANUTENCAOCollection();
        LIS_MANUTESQUIPAMENTOCollection LIS_MANUTESQUIPAMENTOPrintColl = new LIS_MANUTESQUIPAMENTOCollection();
        LIS_PRODUTOSMANUTCollection LIS_PRODUTOSMANUTColl = new LIS_PRODUTOSMANUTCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();

        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public string DataConsultaSelec = string.Empty;

        public FrmManutencaoEquip()
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

        int _IDMANUTEESQUIPAMENTO = -1;
        public MANUTESQUIPAMENTOEntity Entity
        {
            get
            {
                DateTime DATAMANUT = Convert.ToDateTime(maskedtxtData.Text);
                
                DateTime? DATAPROXIMAMANUT = null;
                if (maskedtxtDataProxManut.Text != "  /  /")
                    DATAPROXIMAMANUT = Convert.ToDateTime(maskedtxtDataProxManut.Text);
                int IDTIPOMANUTENCAO = Convert.ToInt32(cbTipoManutencao.SelectedValue);
                int IDSITUACAO = Convert.ToInt32(cbStatus.SelectedValue);
                int IDFUNCSOLICITANTE = Convert.ToInt32(cbFuncSolicitante.SelectedValue);

                int? IDFUNCEXECUTOR = null;
                if (cbFuncExecutor.SelectedIndex > 0)
                    IDFUNCEXECUTOR = Convert.ToInt32(cbFuncExecutor.SelectedValue);

                int? IDFORNECEDOR = null;
                if (cbFornecedor.SelectedIndex > 0)
                    IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);

                if(txtValorManutencao.Text == string.Empty)
                    txtValorManutencao.Text = "0,00";
                decimal VALORMANUTENCAO = Convert.ToDecimal(txtValorManutencao.Text) + Convert.ToDecimal(txtTotalProdutos.Text);

                if (txtKmManutencao.Text == string.Empty)
                    txtKmManutencao.Text = "0";
                int KMMANUTENCAO = Convert.ToInt32(txtKmManutencao.Text);

                if (txtKmProximManut.Text == string.Empty)
                    txtKmProximManut.Text = "0";
                int KMPROXMANUT = Convert.ToInt32(txtKmProximManut.Text);

                string OBSERVACAO = txtObservacao.Text;

                int IDEQUIPAMENTO = Convert.ToInt32(cbEquipamento.SelectedValue);

                int? IDCENTROCUSTO = null;
                if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                string CODREFERENCIA = txtCodReferencia.Text;

                  if(txtAtual.Text == string.Empty)
                    txtAtual.Text = "0";
                  int KMATUAL = Convert.ToInt32(txtAtual.Text);
  
                return new MANUTESQUIPAMENTOEntity(_IDMANUTEESQUIPAMENTO, DATAMANUT,
                                                   DATAPROXIMAMANUT, IDTIPOMANUTENCAO, IDSITUACAO,
                                                   IDFUNCSOLICITANTE, IDFUNCEXECUTOR, IDFORNECEDOR,
                                                   VALORMANUTENCAO, KMMANUTENCAO, KMPROXMANUT, OBSERVACAO, 
                                                   IDEQUIPAMENTO, IDCENTROCUSTO, CODREFERENCIA, KMATUAL);
            }
            set
            {

                if (value != null)
                {
                    _IDMANUTEESQUIPAMENTO = value.IDMANUTEESQUIPAMENTO;
                    txtNumDuplicata.Text = _IDMANUTEESQUIPAMENTO.ToString();
                    ListaItensProduto(_IDMANUTEESQUIPAMENTO);
                    cbEquipamento.SelectedValue = value.IDEQUIPAMENTO;
                    maskedtxtData.Text =Convert.ToDateTime(value.DATAMANUT).ToString("dd/MM/yyyy");

                    

                    if(value.DATAPROXIMAMANUT != null)
                        maskedtxtDataProxManut.Text =Convert.ToDateTime(value.DATAPROXIMAMANUT).ToString("dd/MM/yyyy");
                    else
                        maskedtxtDataProxManut.Text = "  /  /";
                    
                    cbTipoManutencao.SelectedValue = value.IDTIPOMANUTENCAO;
                    cbStatus.SelectedValue = value.IDSITUACAO;
                    cbFuncSolicitante.SelectedValue = value.IDFUNCSOLICITANTE;

                    if (value.IDFUNCEXECUTOR != null)
                        cbFuncExecutor.SelectedValue = value.IDFUNCEXECUTOR;
                    else
                        cbFuncExecutor.SelectedIndex = 0;

                    if (value.IDFORNECEDOR != null)
                        cbFornecedor.SelectedValue = value.IDFORNECEDOR;
                    else
                        cbFornecedor.SelectedIndex = 0;

                    txtValorManutencao.Text = Convert.ToDecimal(value.VALORMANUTENCAO - Convert.ToDecimal(txtTotalProdutos.Text)).ToString("n2");
                    txtKmManutencao.Text = Convert.ToString(value.KMMANUTENCAO);
                    txtKmProximManut.Text = Convert.ToString(value.KMPROXMANUT);
                    txtObservacao.Text = value.OBSERVACAO;

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), _IDMANUTEESQUIPAMENTO.ToString());

                    txtCodReferencia.Text = value.CODREFERENCIA;

                    txtAtual.Text = Convert.ToString(value.KMATUAL);

                    errorProvider1.Clear();
                }
                else
                {
                    _IDMANUTEESQUIPAMENTO = -1;
                    txtNumDuplicata.Text = _IDMANUTEESQUIPAMENTO.ToString();
                    ListaItensProduto(_IDMANUTEESQUIPAMENTO);
                    maskedtxtData.Text = "  /  /";
                    maskedtxtDataProxManut.Text = "  /  /";
                    cbTipoManutencao.SelectedIndex = 0;
                    cbStatus.SelectedIndex = 0;
                    cbFuncSolicitante.SelectedIndex = 0;
                    cbFuncExecutor.SelectedIndex = 0;
                    cbFornecedor.SelectedIndex = 0;
                    txtValorManutencao.Text = "0,00";
                    txtKmManutencao.Text = "0";
                    txtKmProximManut.Text = "0";
                    txtObservacao.Text = string.Empty;
                    cbEquipamento.SelectedIndex = 0;
                    GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), _IDMANUTEESQUIPAMENTO.ToString());
                    cbCentroCusto.SelectedIndex = 0;
                    txtCodReferencia.Text = string.Empty;
                    txtAtual.Text = "0";
                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODUTOSMANUT = -1;
        public PRODUTOSMANUTEntity Entity2
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanPeca.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitPecas.Text);
                decimal VALORTOTAL = Convert.ToDecimal(txtValorUnitPecas.Text) * Convert.ToDecimal(txtQuanPeca.Text);


                return new PRODUTOSMANUTEntity(_IDPRODUTOSMANUT, _IDMANUTEESQUIPAMENTO,  IDPRODUTO, QUANTIDADE,
                                                 VALORUNITARIO, VALORTOTAL, 0);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOSMANUT = Convert.ToInt32(value.IDPRODUTOSMANUT);
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanPeca.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtValorUnitPecas.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOSMANUT = -1;
                   
                    cbProduto.SelectedIndex = 0;
                    txtQuanPeca.Text = "1";
                    txtValorUnitPecas.Text = "0,00";
                    errorProvider1.Clear();
                }
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
                    
                    _IDMANUTEESQUIPAMENTO = MANUTESQUIPAMENTOP.Save(Entity);
                    txtNumDuplicata.Text = _IDMANUTEESQUIPAMENTO.ToString();
                    tabControManEquip.SelectTab(0);
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
            if (cbEquipamento.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbEquipamento, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (cbTipoManutencao.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbTipoManutencao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (cbFuncSolicitante.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFuncSolicitante, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (maskedtxtData.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (maskedtxtDataProxManut.Text != "  /  /" && (Convert.ToDateTime(maskedtxtDataProxManut.Text) < Convert.ToDateTime(maskedtxtData.Text)))
            {
                errorProvider1.SetError(maskedtxtDataProxManut, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show("A data da próxima manutenção não pode ser menor que a data da manutenção.",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                result = false;
            }
            else if (txtKmManutencao.Text == string.Empty || !ValidacoesLibrary.ValidaTipoInt32(txtKmManutencao.Text))
            {
                errorProvider1.SetError(txtKmManutencao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtKmProximManut.Text == string.Empty || !ValidacoesLibrary.ValidaTipoInt32(txtKmProximManut.Text))
            {
                errorProvider1.SetError(txtKmProximManut, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if ((Convert.ToInt32(txtKmProximManut.Text) < Convert.ToInt32(txtKmManutencao.Text)))
            {
                errorProvider1.SetError(txtKmProximManut, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show("O KM da Próxima Manutenção não pode ser menor que o KM da Manutenção.",
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

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            GetDropEquipamento();
            GetFuncionarioSolicitante();
            GetFuncionarioExecutor();
            GetDropFornecedor();
            GetDropStatus();
            GetDropTipoEquipamento();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetDropPecas();
            GetDropCentroCusto();
            GetDropGrupoCategoria();

            btnCadEsquip.Image = Util.GetAddressImage(6);
            btnCadTipoManut.Image = Util.GetAddressImage(6);
            btnCadSituacao.Image = Util.GetAddressImage(6);
            btnCadFuncSolic.Image = Util.GetAddressImage(6);
            btnCadFuncExecutor.Image = Util.GetAddressImage(6);
            btnEmpreContrat.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecProxManut.Image = Util.GetAddressImage(11);
            btnCadGrupoCategoria.Image = Util.GetAddressImage(6);

            bntDateSelecFinal2.Image = Util.GetAddressImage(11);
            bntDateSelecInicial2.Image = Util.GetAddressImage(11);

            this.Cursor = Cursors.Default;

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();

            if(DataConsultaSelec!= string.Empty)
            {
                string Date = Util.ConverStringDateSearch(DateTime.Now.ToString());
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAPROXIMAMANUT", "System.DateTime", "=", Date));
               
                LIS_MANUTESQUIPAMENTOColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio, "DATAPROXIMAMANUT");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;

                lblTotalPesquisa.Text = LIS_MANUTESQUIPAMENTOColl.Count.ToString();

                tabControManEquip.SelectTab(3);
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
        }       

        private void GetDropTipoEquipamento()
        {
            TIPOMANUTENCAOProvider TIPOMANUTENCAOP = new TIPOMANUTENCAOProvider();
            TIPOMANUTENCAOColl = TIPOMANUTENCAOP.ReadCollectionByParameter(null, "NOME");

            cbTipoManutencao.DisplayMember = "NOME";
            cbTipoManutencao.ValueMember = "IDTIPOMANUTENCAO";

            TIPOMANUTENCAOEntity TIPOMANUTENCAOTy = new TIPOMANUTENCAOEntity();
            TIPOMANUTENCAOTy.NOME = ConfigMessage.Default.MsgDrop;
            TIPOMANUTENCAOTy.IDTIPOMANUTENCAO = -1;
            TIPOMANUTENCAOColl.Add(TIPOMANUTENCAOTy);

            Phydeaux.Utilities.DynamicComparer<TIPOMANUTENCAOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPOMANUTENCAOEntity>(cbTipoManutencao.DisplayMember);

            TIPOMANUTENCAOColl.Sort(comparer.Comparer);
            cbTipoManutencao.DataSource = TIPOMANUTENCAOColl;

            cbTipoManutencao.SelectedIndex = 0;
        }

        private void GetFuncionarioSolicitante()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncSolicitante.DisplayMember = "NOME";
            cbFuncSolicitante.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncSolicitante.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncSolicitante.DataSource = FUNCIONARIOColl;

            cbFuncSolicitante.SelectedIndex = 0;
        }

        private void GetFuncionarioExecutor()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOExecColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncExecutor.DisplayMember = "NOME";
            cbFuncExecutor.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOExecColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncExecutor.DisplayMember);

            FUNCIONARIOExecColl.Sort(comparer.Comparer);
            cbFuncExecutor.DataSource = FUNCIONARIOExecColl;

            cbFuncExecutor.SelectedIndex = 0;
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
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControManEquip.SelectTab(0);
            cbEquipamento.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControManEquip.SelectTab(0);
            cbEquipamento.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDMANUTEESQUIPAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControManEquip.SelectTab(3);
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
                        MANUTESQUIPAMENTOP.Delete(_IDMANUTEESQUIPAMENTO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControManEquip.SelectTab(3);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControManEquip.SelectTab(3);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_MANUTESQUIPAMENTOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_MANUTESQUIPAMENTOColl[rowindex].IDMANUTEESQUIPAMENTO);

                    Entity = MANUTESQUIPAMENTOP.Read(CodigoSelect);

                    tabControManEquip.SelectTab(0);
                    cbEquipamento.Focus();
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            laserJatoDeTintaToolStripMenuItem_Click(null, null);
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista Manutenção Equipamento");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
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

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
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
                e.Graphics.DrawString("Cód.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Data Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Prox.Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, 170);
                e.Graphics.DrawString("Situação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 170);
                e.Graphics.DrawString("Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_MANUTESQUIPAMENTOColl.Count;

                while (IndexRegistro < LIS_MANUTESQUIPAMENTOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOColl[IndexRegistro].IDMANUTEESQUIPAMENTO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOColl[IndexRegistro].DATAMANUT).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOColl[IndexRegistro].DATAPROXIMAMANUT).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 150, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOColl[IndexRegistro].NOMESITUACAO, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOColl[IndexRegistro].NOMEEQUIPAMENTO, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 350, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_MANUTESQUIPAMENTOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_MANUTESQUIPAMENTOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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

     
        private void FrmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_MANUTESQUIPAMENTOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_MANUTESQUIPAMENTOColl[indice].IDMANUTEESQUIPAMENTO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = MANUTESQUIPAMENTOP.Read(CodigoSelect);

                    tabControManEquip.SelectTab(0);
                    cbEquipamento.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            MANUTESQUIPAMENTOP.Delete(CodigoSelect);
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

        private void cbEquipamento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
        (e.KeyCode == Keys.E))
            {
                using (FrmSearchEquipamento frm = new FrmSearchEquipamento())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbEquipamento.SelectedValue = result;
                }
            }
        }

        private void cbEquipamento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o equipamento ou pressione Ctrl+E para pesquisar."; 
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);
            monthCalendar2.ShowWeekNumbers = true;

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(240, 165);
            FormCalendario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario.Location = new Point(230, 55); ;
            FormCalendario.Name = "FrmCalendario";
            FormCalendario.Text = "Calendário";
            FormCalendario.ResumeLayout(false);
            FormCalendario.Controls.Add(monthCalendar2);
            FormCalendario.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedtxtData.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        private void maskedtxtData_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtData.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskedtxtData.Focus();
                    errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
            else
                errorProvider1.Clear();
        }

        private void maskedtxtDataProxManut_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtDataProxManut.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataProxManut.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskedtxtDataProxManut.Focus();
                    errorProvider1.SetError(maskedtxtDataProxManut, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
            else
                errorProvider1.Clear();

        }

        private void txtValorManutencao_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorManutencao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorManutencao.Text))
                {
                    errorProvider1.SetError(txtValorManutencao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorManutencao.Text);
                    txtValorManutencao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorManutencao, "");
                }
            }
            else
                txtValorManutencao.Text = "0,00";
        }

        private void btnCadFuncSolic_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                int CodSelec = Convert.ToInt32(cbFuncSolicitante.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                
                GetFuncionarioSolicitante();
                cbFuncSolicitante.SelectedValue = CodSelec;
            }
        }

        private void btnCadFuncExecutor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                int CodSelec = Convert.ToInt32(cbFuncExecutor.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                
                GetFuncionarioExecutor();
                cbFuncExecutor.SelectedValue = CodSelec;
            }
        }

        private void btnEmpreContrat_Click(object sender, EventArgs e)
        {
            using (FrmFornecedor frm = new FrmFornecedor())
            {
                int CodSelec = Convert.ToInt32(cbFornecedor.SelectedValue);
                frm._IDFORNECEDOR = CodSelec;
                frm.ShowDialog();
            
                GetDropFornecedor();
                cbFornecedor.SelectedValue = CodSelec;
            }
        }

        private void GetDropFornecedor()
        {
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
            FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbFornecedor.DisplayMember = "NOME";
            cbFornecedor.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FORNECEDORColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedor.DisplayMember);

            FORNECEDORColl.Sort(comparer.Comparer);
            cbFornecedor.DataSource = FORNECEDORColl;

            cbFornecedor.SelectedIndex = 0;
        }

        private void cbFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbFornecedor.SelectedValue = result;
                }
            }
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecProxManut_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);
            monthCalendar3.ShowWeekNumbers = true;

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(240, 165);
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
            maskedtxtDataProxManut.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void btnCadSituacao_Click(object sender, EventArgs e)
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

        private void GetDropStatus()
        {
            //15 Manutenção de Equipamento
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "15");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

        private void cbFornecedor_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar."; 
        }

        private void btnCadTipoManut_Click(object sender, EventArgs e)
        {
            using (FrmTipoManutencao frm = new FrmTipoManutencao())
            {
                int CodSelec = Convert.ToInt32(cbTipoManutencao.SelectedValue);
                frm._IDTIPOMANUTENCAO = CodSelec;
                frm.ShowDialog();
                
                GetDropTipoEquipamento();
                cbTipoManutencao.SelectedValue = CodSelec;
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
                    filtroProfile = new RowsFiltro("DATAMANUT", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMANUT", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_MANUTESQUIPAMENTOColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(Filtro, "IDMANUTEESQUIPAMENTO DESC");

                //Colocando somatorio no final da lista
                LIS_MANUTESQUIPAMENTOEntity LIS_MANUTESQUIPAMENTOTy = new LIS_MANUTESQUIPAMENTOEntity();
                LIS_MANUTESQUIPAMENTOTy.VALORMANUTENCAO = SumTotalPesquisa("VALORMANUTENCAO");
                LIS_MANUTESQUIPAMENTOColl.Add(LIS_MANUTESQUIPAMENTOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;

                lblTotalPesquisa.Text = LIS_MANUTESQUIPAMENTOColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_MANUTESQUIPAMENTOEntity item in LIS_MANUTESQUIPAMENTOColl)
            {
                if (NomeCampo == "VALORMANUTENCAO")
                    valortotal += Convert.ToDecimal(item.VALORMANUTENCAO);              
            }

            return valortotal;
        }



        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControManEquip.SelectedIndex == 1)
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
            if (LIS_MANUTESQUIPAMENTOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;
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
                    filtroProfile = new RowsFiltro("DATAMANUT", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMANUT", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }


                LIS_MANUTESQUIPAMENTOColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(Filtro, "IDMANUTEESQUIPAMENTO DESC");

                //Colocando somatorio no final da lista
                LIS_MANUTESQUIPAMENTOEntity LIS_MANUTESQUIPAMENTOTy = new LIS_MANUTESQUIPAMENTOEntity();
                LIS_MANUTESQUIPAMENTOTy.VALORMANUTENCAO = SumTotalPesquisa("VALORMANUTENCAO");
                LIS_MANUTESQUIPAMENTOColl.Add(LIS_MANUTESQUIPAMENTOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;

                lblTotalPesquisa.Text = LIS_MANUTESQUIPAMENTOColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }
       

        private void laserJatoDeTintaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDMANUTEESQUIPAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControManEquip.SelectTab(3);
            }
            else
                ImprimirOrdemServico2();
        }

        private void ImprimirOrdemServico2()
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

        private void ImprimirOrdemServico2Via()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument3;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                objPrintPreview.Document = printDocument3;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Armazena na coleção do Fechamento O.S Selecionada
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDMANUTEESQUIPAMENTO", "System.Int32", "=", _IDMANUTEESQUIPAMENTO.ToString()));
                LIS_MANUTESQUIPAMENTOPrintColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio);

                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio da impressão - 1º Via

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


                e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDMANUTEESQUIPAMENTO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

                int linha = 113;
                linha += 20;
                e.Graphics.DrawString("Cod. Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Nome Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString("Data Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString("Data Próx.Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha += 15;
                e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOPrintColl[0].IDEQUIPAMENTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha );
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEEQUIPAMENTO,50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAPROXIMAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 530, linha);

                linha += 15;
                e.Graphics.DrawString("Tipo Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Situação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMETIPOMANUTENCAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMESITUACAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha);

                linha += 15;
                e.Graphics.DrawString("Funcionário Solicitante", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Funcionário Executor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCSOLICITANTE, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCEXECUTOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);

                linha += 15;
                e.Graphics.DrawString("Empresa Contratada", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Valor da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString("KM da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString("KM da Prox. Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFORNECEDOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(txtValorManutencao.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMMANUTENCAO.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMPROXMANUT.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha);

                linha += 15;
                e.Graphics.DrawString("Observação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha += 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].OBSERVACAO, 470), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                linha = 335;
                int linhaBorda = 330;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;

                linha += 35;
                //Produtos
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

                //Itens de Produtos;
                linha = linha + 25;
                linhaBorda = linhaBorda + 80;
                foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
                {
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 690, 20);
                    e.Graphics.DrawString(item.QUANTIDADE.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 630, 20);
                    e.Graphics.DrawString(item.IDPRODUTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 220, 20);
                    e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTO, 100), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                   // Alinhar a direita
                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 130, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORUNITARIO).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);

                    e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linhaBorda, config.MargemDireita - 20, 20);
                    e.Graphics.DrawString(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);
                    linha = linha + 20;
                    linhaBorda = linhaBorda + 20;
                }

                linha = linha + 10;
                e.Graphics.DrawString("Total Produto:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);
                e.Graphics.DrawString(SomaProduto().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);


                linha = linha + 20;
                e.Graphics.DrawString("Total Geral:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, linha + 15, stringFormat);
                e.Graphics.DrawString((SomaProduto() + Convert.ToDecimal(txtValorManutencao.Text)).ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, linha + 15, stringFormat);


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private decimal SomaProduto()
        {
            decimal result = 0;

            foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
            {
                result += Convert.ToDecimal(item.VALORTOTAL);
            }

            return result;
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Armazena na coleção do Fechamento O.S Selecionada
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDMANUTEESQUIPAMENTO", "System.Int32", "=", _IDMANUTEESQUIPAMENTO.ToString()));
                LIS_MANUTESQUIPAMENTOPrintColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio);

                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio da impressão - 1º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 350);


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


                e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDMANUTEESQUIPAMENTO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

                int linha = 113;
                linha += 15;
                e.Graphics.DrawString("Cod. Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Nome Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString("Data Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString("Data Próx.Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha += 15;
                e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOPrintColl[0].IDEQUIPAMENTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEEQUIPAMENTO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAPROXIMAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 530, linha);

                linha += 15;
                e.Graphics.DrawString("Tipo Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Situação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMETIPOMANUTENCAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMESITUACAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha);

                linha += 15;
                e.Graphics.DrawString("Funcionário Solicitante", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Funcionário Executor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCSOLICITANTE, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCEXECUTOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);

                linha += 15;
                e.Graphics.DrawString("Empresa Contratada", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Valor da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString("KM da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString("KM da Prox. Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFORNECEDOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(LIS_MANUTESQUIPAMENTOPrintColl[0].VALORMANUTENCAO).ToString("n2"), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMMANUTENCAO.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMPROXMANUT.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha);

                linha += 15;
                e.Graphics.DrawString("Observação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha += 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].OBSERVACAO, 470), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("1º Via", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 380);

                //Inicio para imprimir a 2º via 
                linha = 580;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 510, config.MargemDireita, 350);
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

                linha += 15;
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 518);
                e.Graphics.DrawString(Entity.IDMANUTEESQUIPAMENTO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 533);

                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 518);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 533);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 548);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 548);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 563);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 578);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 593);


                linha += 15;
                e.Graphics.DrawString("Cod. Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Nome Equipamento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString("Data Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString("Data Próx.Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha += 15;
                e.Graphics.DrawString(LIS_MANUTESQUIPAMENTOPrintColl[0].IDEQUIPAMENTO.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEEQUIPAMENTO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, linha);
                e.Graphics.DrawString(Convert.ToDateTime(LIS_MANUTESQUIPAMENTOPrintColl[0].DATAPROXIMAMANUT).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 530, linha);

                linha += 15;
                e.Graphics.DrawString("Tipo Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Situação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMETIPOMANUTENCAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMESITUACAO, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, linha);

                linha += 15;
                e.Graphics.DrawString("Funcionário Solicitante", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Funcionário Executor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCSOLICITANTE, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFUNCEXECUTOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);

                linha += 15;
                e.Graphics.DrawString("Empresa Contratada", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Valor da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString("KM da Manutenção", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString("KM da Prox. Manut.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, linha);
                linha += 15;
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].NOMEFORNECEDOR, 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(LIS_MANUTESQUIPAMENTOPrintColl[0].VALORMANUTENCAO).ToString("n2"), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 350, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMMANUTENCAO.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 500, linha);
                e.Graphics.DrawString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].KMPROXMANUT.ToString(), 20), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 630, linha);

                linha += 15;
                e.Graphics.DrawString("Observação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha += 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(LIS_MANUTESQUIPAMENTOPrintColl[0].OBSERVACAO, 470), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("2º Via", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 860);

                

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void laserJatoDeTinta2ViaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDMANUTEESQUIPAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControManEquip.SelectTab(3);
            }
            else
                ImprimirOrdemServico2Via();
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
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

        private void GetDropPecas()
        {
        
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

        private void txtQuanPeca_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanPeca.Text))
            {
                errorProvider1.SetError(txtQuanPeca, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
                errorProvider1.Clear();
            
        }

        private void txtValorUnitPecas_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorUnitPecas.Text == string.Empty)
                txtValorUnitPecas.Text = "0,00";

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitPecas.Text))
            {
                errorProvider1.SetError(txtValorUnitPecas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
                errorProvider1.Clear();
        }

        private void txtValorUnitPecas_Enter(object sender, EventArgs e)
        {
            if (txtValorUnitPecas.Text == "0,00")
                txtValorUnitPecas.Text = string.Empty;
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (_IDMANUTEESQUIPAMENTO == -1)
            {
                MessageBox.Show("Antes de adicionar os Produtoss é necessário gravar a Manutenção!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (ValidacoesProdutos())
                {

                    try
                    {
                        //Grava itens de serviços
                        PRODUTOSMANUTP.Save(Entity2);

                        //Salva a Manutenção
                        MANUTESQUIPAMENTOP.Save(Entity);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Entity2 = null;
                        cbProduto.Focus();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    }

                    //Lista os itens de  peças cadastrados;
                    ListaItensProduto(_IDMANUTEESQUIPAMENTO);
                }
            }
        }

        private void ListaItensProduto(int IDMANUTEESQUIPAMENTO)
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDMANUTEESQUIPAMENTO", "System.Int32", "=", IDMANUTEESQUIPAMENTO.ToString()));
            LIS_PRODUTOSMANUTColl = LIS_PRODUTOSMANUTP.ReadCollectionByParameter(RowRelatorio);

            DGDadosPecas.AutoGenerateColumns = false;
            DGDadosPecas.DataSource = LIS_PRODUTOSMANUTColl;

            SumTotalItemsProdutos();
        }

        private void SumTotalItemsProdutos()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProdutos.Text = total.ToString("n2");
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
            else if ( CONFISISTEMAP.Read(1).ESTOQUENEGATIVO.TrimEnd() == "S")
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOS_EstoqueTy = new PRODUTOSEntity();
                PRODUTOS_EstoqueTy = PRODUTOSP.Read(Convert.ToInt32(Convert.ToInt32(cbProduto.SelectedValue)));
                if (Convert.ToDecimal(txtEstoque.Text) < Convert.ToDecimal(txtQuanPeca.Text))
                {
                    string Msgerro = "Estoque não pode ficar negativo!";
                    errorProvider1.SetError(txtQuanPeca, Msgerro);
                    MessageBox.Show(Msgerro);
                    result = false;
                }

            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void DGDadosPecas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSMANUTColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                        cbGrupoCategoria.SelectedValue = -1;
                        CodSelect = Convert.ToInt32(LIS_PRODUTOSMANUTColl[rowindex].IDPRODUTOSMANUT);
                        Entity2 = PRODUTOSMANUTP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOSMANUTColl[rowindex].IDPRODUTOSMANUT);
                            PRODUTOSMANUTP.Delete(CodSelect);

                            ListaItensProduto(_IDMANUTEESQUIPAMENTO);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosPecas, RelatorioTitulo, this.Name);
        }

        private void listaDaManutençãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaManutencao FrmLista = new FrmListaManutencao();
            FrmLista.ShowDialog();
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
             if (LIS_MANUTESQUIPAMENTOColl.Count > 0)
            {

                //Retira o ultimo registro
                LIS_MANUTESQUIPAMENTOColl.RemoveAt(LIS_MANUTESQUIPAMENTOColl.Count - 1);

                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_MANUTESQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MANUTESQUIPAMENTOEntity>(orderBy);

                LIS_MANUTESQUIPAMENTOColl.Sort(comparer.Comparer);

                //Colocando somatorio no final da lista
                LIS_MANUTESQUIPAMENTOEntity LIS_MANUTESQUIPAMENTOTy = new LIS_MANUTESQUIPAMENTOEntity();
                LIS_MANUTESQUIPAMENTOTy.VALORMANUTENCAO = SumTotalPesquisa("VALORMANUTENCAO");
                LIS_MANUTESQUIPAMENTOColl.Add(LIS_MANUTESQUIPAMENTOTy);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;

            }
        }       

        private void btnCadCentroCusto_Click_1(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                int CodSelec = Convert.ToInt32(cbTipoManutencao.SelectedValue);
                frm._IDCENTROCUSTOS = CodSelec;
                frm.ShowDialog();
                
                GetDropTipoEquipamento();
                cbTipoManutencao.SelectedValue = CodSelec;
            }
        }

        private void GetDropCentroCusto()
        {
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

        private void txtNParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtVlParcelas, "");
            }   
        }

        private void txtVlParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (txtVlParcelas.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text))
                {
                    errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtVlParcelas.Text);
                    txtVlParcelas.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlParcelas, "");
                }
            }
            else
            {
                txtVlParcelas.Text = "0,00";
                errorProvider1.SetError(txtVlParcelas, "");

            }
        }

        private void TxtDias_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text) || TxtDias.Text == "0")
            {
                errorProvider1.SetError(TxtDias, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                DateTime DateVectro = DateTime.Now.AddDays(Convert.ToInt32(TxtDias.Text));
                mskDataVecto.Text = DateVectro.ToString("dd/MM/yyyy");
                errorProvider1.SetError(TxtDias, "");
            }   
        }

        private void mskDataVecto_Leave(object sender, EventArgs e)
        {
            if (mskDataVecto.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataVecto.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    errorProvider1.SetError(mskDataVecto, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(mskDataVecto, "");
                }
            }
            else
            {
                errorProvider1.SetError(mskDataVecto, "");
            }
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
            if (_IDMANUTEESQUIPAMENTO == -1)
                MessageBox.Show("Antes de adicionar é necessário gravar a manutenção!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            else
            {
                if (ValidaDuplicatas())
                {
                    try
                    {
                        DateTime DATAVENCIT = Convert.ToDateTime(mskDataVecto.Text);
                        for (int i = 0; i < Convert.ToInt32(txtNParcelas.Text); i++)
                        {
                            if(i > 0)
                                DATAVENCIT = DATAVENCIT.AddDays(Convert.ToInt32(TxtDias.Text));

                            int DIAVECTO = Convert.ToInt32(DATAVENCIT.Day);
                            int MESVECTO = DATAVENCIT.Month;
                            int ANOVECTO = DATAVENCIT.Year;
                            string DATAFIXO = DIAVECTO + "/" + MESVECTO + "/" + ANOVECTO;
                            DATAVENCIT = Convert.ToDateTime(DATAFIXO);
                            mskDataVecto.Text = DATAVENCIT.ToString("dd/MM/yyyy");

                            DUPLICATAPAGAREntity DUPLICATAPAGARty = new DUPLICATAPAGAREntity();
                            DUPLICATAPAGARty.IDDUPLICATAPAGAR = -1;
                            DUPLICATAPAGARty.IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);

                            if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                                DUPLICATAPAGARty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                            int NumTotalDupl = LIS_DUPLICATAPAGARColl.Count + 1;
                            DUPLICATAPAGARty.NUMERO = txtNumDuplicata.Text + "-" + (i + 1).ToString();
                            DUPLICATAPAGARty.DATAEMISSAO = Convert.ToDateTime(maskedtxtData.Text);
                            DUPLICATAPAGARty.DATAVECTO = DATAVENCIT;

                            //Acerta Valor Final
                            if (i == (Convert.ToInt32(txtNParcelas.Text) - 1))
                            {
                                decimal TotalCal = Convert.ToDecimal(txtVlParcelas.Text) * Convert.ToInt32(txtNParcelas.Text);
                                decimal sobratot = Convert.ToDecimal(lbltTotalManut.Text) - TotalCal;

                                DUPLICATAPAGARty.VALORDUPLICATA = Convert.ToDecimal(txtVlParcelas.Text) + sobratot;
                                DUPLICATAPAGARty.VALORDEVEDOR = Convert.ToDecimal(txtVlParcelas.Text) + sobratot;
                            }
                            else
                            {
                                DUPLICATAPAGARty.VALORDUPLICATA = Convert.ToDecimal(txtVlParcelas.Text);
                                DUPLICATAPAGARty.VALORDEVEDOR = Convert.ToDecimal(txtVlParcelas.Text);
                            }

                            DUPLICATAPAGARty.IDSTATUS = 1;//Aberto
                            DUPLICATAPAGARty.NOTAFISCAL = txtNumNotaFiscal.Text;

                            DUPLICATAPAGARP.Save(DUPLICATAPAGARty);

                        }

                        GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), _IDMANUTEESQUIPAMENTO.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    }
                }
            }
        }

        private void GridDuplicatasFornecedor(int idFornecedor, string numero)
        {
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", idFornecedor.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "like", numero));

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATAPAGAR");
                dataGridDuplFornecedor.AutoGenerateColumns = false;
                dataGridDuplFornecedor.DataSource = LIS_DUPLICATAPAGARColl;
                SumFinanceiroForne();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumFinanceiroForne()
        {
            decimal valorTotal = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                valorTotal += Convert.ToDecimal(item.VALORDUPLICATA);
            }

            lblTotalFinanceiro.Text = valorTotal.ToString();
            Double f = Convert.ToDouble(lblTotalFinanceiro.Text);
            lblTotalFinanceiro.Text = string.Format("{0:n2}", f);

            lblTotalFinanceiro.Text = "Total de Duplicata: " + lblTotalFinanceiro.Text;
        }

        private Boolean ValidaDuplicatas()
        {
            Boolean result = true;

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text) || txtVlParcelas.Text == "0,00")
            {
                errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text) || Convert.ToInt32(txtNParcelas.Text) < 1)
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text) || Convert.ToInt32(TxtDias.Text) < 1)
            {
                errorProvider1.SetError(TxtDias, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataVecto.Text))
            {
                errorProvider1.SetError(mskDataVecto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if ( Convert.ToInt32(cbFornecedor.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbFornecedor, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                tabControManEquip.SelectTab(0);
                result = false;
            }
            else if (txtNumDuplicata.Text.TrimEnd() == string.Empty)
            {
                errorProvider1.SetError(txtNumDuplicata, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void dataGridDuplFornecedor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATAPAGARColl.Count > 0 && rowindex > -1)
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
                            CodSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                            DUPLICATAPAGARP.Delete(CodSelect);
                            GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtNumDuplicata.Text);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //editar
                {
                    FrmContasPagar FrmCP = new FrmContasPagar();
                    FrmCP.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                    FrmCP.ShowDialog();

                    GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtNumDuplicata.Text);
                }

            }
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void button4_Click(object sender, EventArgs e)
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
        private void button3_Click(object sender, EventArgs e)
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

        private void resumoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumoHorimetro Frm = new FrmResumoHorimetro();
            Frm.ShowDialog();
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                if (PRODUTOSTy != null)
                {
                    txtValorUnitPecas.Text = Convert.ToDecimal(PRODUTOSTy.VALORVENDA1).ToString("N2");
                    txtEstoque.Text = Util.EstoqueAtual(Convert.ToInt32(PRODUTOSTy.IDPRODUTO), false).ToString();
                }
            }          
            
        }

        private void txtNParcelas_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text) || txtNParcelas.Text == "0")
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                if (txtValorManutencao.Text == string.Empty)
                    txtValorManutencao.Text = "0,00";

                decimal TotalManut = Convert.ToDecimal(txtTotalProdutos.Text) + Convert.ToDecimal(txtValorManutencao.Text);
                txtVlParcelas.Text = (TotalManut / Convert.ToDecimal(txtNParcelas.Text)).ToString("n2");
                errorProvider1.Clear();
            }    

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtValorManutencao.Text == string.Empty)
                    txtValorManutencao.Text = "0,00";

                decimal TotalManut = Convert.ToDecimal(txtTotalProdutos.Text) + Convert.ToDecimal(txtValorManutencao.Text);
                lbltTotalManut.Text = TotalManut.ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no calculo!");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnCadGrupoCategoria_Click(object sender, EventArgs e)
        {
            using (FrmGrupoCategoria frm = new FrmGrupoCategoria())
            {
                frm._IDGRUPOCATEGORIA = Convert.ToInt32(cbGrupoCategoria.SelectedValue);
                GetDropGrupoCategoria();
                frm.ShowDialog();
            }
        }

        private void GetDropGrupoCategoria()
        {
            GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
            GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null, "NOME");

            cbGrupoCategoria.DisplayMember = "NOME";
            cbGrupoCategoria.ValueMember = "IDGRUPOCATEGORIA";

            GRUPOCATEGORIAEntity GRUPOCATEGORIATy = new GRUPOCATEGORIAEntity();
            GRUPOCATEGORIATy.NOME = ConfigMessage.Default.MsgDrop;
            GRUPOCATEGORIATy.IDGRUPOCATEGORIA = -1;
            GRUPOCATEGORIAColl.Add(GRUPOCATEGORIATy);

            Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity>(cbGrupoCategoria.DisplayMember);

            GRUPOCATEGORIAColl.Sort(comparer.Comparer);
            cbGrupoCategoria.DataSource = GRUPOCATEGORIAColl;

            cbGrupoCategoria.SelectedIndex = 0;
        }

        private void cbGrupoCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)
                FiltraGrupoCategoriaProduto(Convert.ToInt32(cbGrupoCategoria.SelectedValue));
            else
                FiltraGrupoCategoriaProduto(Convert.ToInt32(cbGrupoCategoria.SelectedValue));
        }

        private void FiltraGrupoCategoriaProduto(int idgrupocategoria)
        {
            try
            {
                RowRelatorio.Clear();

                if (idgrupocategoria > 0)
                { 
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", idgrupocategoria.ToString()));
                    PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                }
                else
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

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
         (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbProduto.SelectedValue = result;
                }
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione a Peça/Produto ou pressione Ctrl+E para pesquisar.";  
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Manutenção de Equipamento";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
               
    }
}
