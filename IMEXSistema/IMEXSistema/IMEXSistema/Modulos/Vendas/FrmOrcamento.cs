using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using System.IO;
using BmsSoftware.Modulos.Servicos;
using CDSSoftware;
using VVX;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmOrcamento : Form
    {
        Utility Util = new Utility();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        LIS_ORCAMENTOCollection LIS_ORCAMENTOColl = new LIS_ORCAMENTOCollection();
        LIS_PRODORCAMENTOCollection LIS_PRODORCAMENTOColl = new LIS_PRODORCAMENTOCollection();

        ORCAMENTOProvider ORCAMENTOP = new ORCAMENTOProvider();
        LIS_ORCAMENTOProvider LIS_ORCAMENTOP = new LIS_ORCAMENTOProvider();
        PRODORCAMENTOProvider PRODORCAMENTOP = new PRODORCAMENTOProvider();
        LIS_PRODORCAMENTOProvider LIS_PRODORCAMENTOP = new LIS_PRODORCAMENTOProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public FrmOrcamento()
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

        int _IDORCAMENTO = -1;
        public ORCAMENTOEntity Entity
        {
            get
            {
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                DateTime DTEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                DateTime DTVALIDADE = Convert.ToDateTime(mskValidade.Text); ;
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                string PRAZOENTREGA = txtPrazoEntrega.Text;

                int? IDFORMAPAGTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                   IDFORMAPAGTO =  Convert.ToInt32(cbFormaPagto.SelectedValue);

                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                int? IDTRANSPORTES = null;
                if (cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTES = Convert.ToInt32(cbTransportadora.SelectedValue);

                string OBSEVACAO = txtObservacao.Text;

                if (txtTotalIPI.Text == string.Empty)
                    txtTotalIPI.Text = "0,00";
                decimal TOTALIPI = Convert.ToDecimal(txtTotalIPI.Text);

                if (txtPorcDesconto.Text == string.Empty)
                    txtPorcDesconto.Text = "0,00";
                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconto.Text);

                if (txtTotalDesconto.Text == string.Empty)
                    txtTotalDesconto.Text = "0,00";
                decimal TOTALDESCONTO = Convert.ToDecimal(txtTotalDesconto.Text);

                if (txtPorcAcrescimo.Text == string.Empty)
                    txtPorcAcrescimo.Text = "0,00";
                decimal PORCACRESCIMO = Convert.ToDecimal(txtPorcAcrescimo.Text);

                if (txtTotalAcrescimo.Text == string.Empty)
                    txtTotalAcrescimo.Text = "0,00";
                decimal TOTALACRESCIMO = Convert.ToDecimal(txtTotalAcrescimo.Text);

                if (txtTotalOrcamento.Text == string.Empty)
                    txtTotalOrcamento.Text = "0,00";
                decimal TOTAORCAMENTO = Convert.ToDecimal(txtTotalOrcamento.Text);


                return new ORCAMENTOEntity(_IDORCAMENTO, IDCLIENTE, DTEMISSAO, DTVALIDADE, IDSTATUS,
                                            PRAZOENTREGA, IDFORMAPAGTO, IDFUNCIONARIO, IDTRANSPORTES,
                                            OBSEVACAO, TOTALIPI, PORCDESCONTO, TOTALDESCONTO, PORCACRESCIMO,
                                            TOTALACRESCIMO,TOTAORCAMENTO);

            }
            set
            {

                if (value != null)
                {
                    _IDORCAMENTO = value.IDORCAMENTO;

                    //Lista Produtos
                    ListaProdutoOrcamento(_IDORCAMENTO);

                    txtNOrcamento.Text = _IDORCAMENTO.ToString().PadLeft(6, '0');
                    cbCliente.SelectedValue = value.IDCLIENTE;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DTEMISSAO).ToString("dd/MM/yyyy");
                    mskValidade.Text = Convert.ToDateTime(value.DTVALIDADE).ToString("dd/MM/yyyy");
                    cbStatus.SelectedValue = value.IDSTATUS;
                    txtPrazoEntrega.Text = value.PRAZOENTREGA;

                    if (value.IDFORMAPAGTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;

                    if (value.IDTRANSPORTES != null)
                        cbTransportadora.SelectedValue = value.IDTRANSPORTES;
                    else
                        cbTransportadora.SelectedIndex = 0;

                    txtObservacao.Text = value.OBSERVACAO;

                    txtTotalIPI.Text = Convert.ToDecimal(value.TOTALIPI).ToString("n2");
                    txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtTotalDesconto.Text = Convert.ToDecimal(value.TOTALDESCONTO).ToString("n2");
                    txtPorcAcrescimo.Text = Convert.ToDecimal(value.PORCACRESCIMO).ToString("n2");
                    txtTotalAcrescimo.Text = Convert.ToDecimal(value.TOTALACRESCIMO).ToString("n2");
                    txtTotalOrcamento.Text = Convert.ToDecimal(value.TOTAORCAMENTO).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDORCAMENTO = -1;

                    ListaProdutoOrcamento(-1);

                    txtNOrcamento.Text = string.Empty;
                    cbCliente.SelectedIndex = 0;
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    //Validade do Orçamento
                    CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    CONFISISTEMATy = CONFISISTEMAP.Read(1);
                    mskValidade.Text = DateTime.Now.AddDays(Convert.ToDouble(CONFISISTEMATy.PRAZOORCAMENTO)).ToString("dd/MM/yyyy");

                    cbStatus.SelectedIndex = 0;
                    txtPrazoEntrega.Text = string.Empty;
                    cbFormaPagto.SelectedIndex = 0;
                    cbFuncionario.SelectedIndex = 0;
                    cbTransportadora.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    txtTotalIPI.Text = "0,00";
                    txtPorcDesconto.Text = "0,00";
                    txtTotalDesconto.Text = "0,00";
                    txtPorcAcrescimo.Text = "0,00";
                    txtTotalAcrescimo.Text = "0,00";
                    txtTotalOrcamento.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODORCAMENTO = -1;
        public PRODORCAMENTOEntity Entity2
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
                decimal VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                return new PRODORCAMENTOEntity(_IDPRODORCAMENTO, _IDORCAMENTO, IDPRODUTO,
                                                QUANTIDADE, VALORUNITARIO, VALORTOTAL);

            }
            set
            {

                if (value != null)
                {
                    _IDPRODORCAMENTO = value.IDPRODORCAMENTO;
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n2");
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString();
                    cbProduto.SelectedValue = value.IDPRODUTO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODORCAMENTO = -1;
                  
                    txtValorUnitProd.Text = "0,00";
                    txtQuanProduto.Text = "0";
                    cbProduto.SelectedIndex = 0;
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmOrcamento_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnFormPagamento.Image = Util.GetAddressImage(6);
            cbVendedor.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            GetDropCliente();
            GetDropStatus();
            GetDropProdutos();
            GetDropFormaPgto();
            GetFuncionario();
            GetTransporte();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            Entity = null;

            this.Cursor = Cursors.Default;
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

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
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

        private void GetDropStatus()
        {
            //10 Orçamento
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "10");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
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

            e.Handled = false;
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

        private void txtNOrcamento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nº Orçamento, código gerado automaticamente.";
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

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
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

                    if(result > 0)
                        cbProduto.SelectedValue = result;
                }
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

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

        private void FrmOrcamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
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
                    _IDORCAMENTO = ORCAMENTOP.Save(Entity);
                    txtNOrcamento.Text = _IDORCAMENTO.ToString().PadLeft(6, '0');
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
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (mskValidade.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(mskValidade.Text))
            {
                errorProvider1.SetError(mskValidade, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            if (cbFuncionario.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                tabControl1.SelectTab(1);
                result = false;
            }                
            else
                errorProvider1.Clear();


            return result;
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlOrcamento.SelectTab(0);
            tabControl1.SelectTab(0);
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlOrcamento.SelectTab(0);
            tabControl1.SelectTab(0);
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_ORCAMENTOColl = LIS_ORCAMENTOP.ReadCollectionByParameter(Filtro, "IDORCAMENTO DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ORCAMENTOColl;

                lblTotalPesquisa.Text = LIS_ORCAMENTOColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlOrcamento.SelectedIndex == 2)
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
            if (LIS_ORCAMENTOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ORCAMENTOColl;
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

                LIS_ORCAMENTOColl = LIS_ORCAMENTOP.ReadCollectionByParameter(Filtro, "IDORCAMENTO DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ORCAMENTOColl;

                lblTotalPesquisa.Text = LIS_ORCAMENTOColl.Count.ToString();
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
            LIS_ORCAMENTOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_ORCAMENTOColl.Count.ToString();
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_ORCAMENTOColl[indice].IDORCAMENTO);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlOrcamento.SelectTab(0);
                    Entity = ORCAMENTOP.Read(CodigoSelect);
                    txtNOrcamento.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            ORCAMENTOP.Delete(CodigoSelect);
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

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_ORCAMENTOColl[rowindex].IDORCAMENTO);
                    tabControlOrcamento.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = ORCAMENTOP.Read(CodigoSelect);                 
                    txtNOrcamento.Focus();
                }
            }
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDORCAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlOrcamento.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (LIS_PRODORCAMENTOColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar o Orçamento é necessário remover os Produtos!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                tabControlOrcamento.SelectTab(0);
                tabControl1.SelectTab(0);
                DGDadosProduto.Focus();
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        ORCAMENTOP.Delete(_IDORCAMENTO);
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
            Delete();
        }

        private void mskValidade_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidacoesLibrary.ValidaTipoDateTime(mskValidade.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mskValidade, ConfigMessage.Default.MsgDataInvalida);
            }    
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
            if (_IDORCAMENTO == -1)
            {
                MessageBox.Show("Antes de adicionar os produtos é necessário gravar o Orçamento!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else if (ValidacoesProdutos())
            {
                try
                {
                    //Grava Produto do Orçamento
                    PRODORCAMENTOP.Save(Entity2);

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    Entity2 = null;
                    cbProduto.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }

                //Lista os produtos do orçamento
                ListaProdutoOrcamento(_IDORCAMENTO);

                Grava();
            }
        }

        private void ListaProdutoOrcamento(int IDORCAMENTO)
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDORCAMENTO", "System.Int32", "=", IDORCAMENTO.ToString()));
            LIS_PRODORCAMENTOColl = LIS_PRODORCAMENTOP.ReadCollectionByParameter(RowRelatorio);

            DGDadosProduto.AutoGenerateColumns = false;
            DGDadosProduto.DataSource = LIS_PRODORCAMENTOColl;

            SumTotalItemsProduto();
            SumTotalOrcamento();
        }

        private void SumTotalItemsProduto()
        {
            decimal total = 0;
            foreach (LIS_PRODORCAMENTOEntity item in LIS_PRODORCAMENTOColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
            txtTotalProdAdicional.Text = total.ToString("n2");
            lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODORCAMENTOColl.Count.ToString();

            SumTotalOrcamento();
        }

        private void SumTotalOrcamento()
        {
            Decimal TotalOrcamento = 0;
            TotalOrcamento = (Convert.ToDecimal(txtTotalProdAdicional.Text) +
                             Convert.ToDecimal(txtTotalIPI.Text) +
                             Convert.ToDecimal(txtTotalAcrescimo.Text)) -
                             Convert.ToDecimal(txtTotalDesconto.Text);
            txtTotalOrcamento.Text = TotalOrcamento.ToString("n2");
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
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanProduto.Text) || Convert.ToInt32(txtQuanProduto.Text) <= 0)
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;
            cbProduto.SelectedIndex = 0;
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
                    txtValorUnitProd.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorUnitProd, "");
                }
            }
            else
                txtValorUnitProd.Text = "0,00";
        }

        private void txtValorUnitProd_Enter(object sender, EventArgs e)
        {
            if (txtValorUnitProd.Text == "0,00")
                txtValorUnitProd.Text = string.Empty;

        }

        private void DGDadosProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODORCAMENTOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODORCAMENTOColl[rowindex].IDPRODORCAMENTO);
                    Entity2 = PRODORCAMENTOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODORCAMENTOColl[rowindex].IDPRODORCAMENTO);
                            PRODORCAMENTOP.Delete(CodSelect);
                            ListaProdutoOrcamento(_IDORCAMENTO);
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
                    SumTotalOrcamento();
                }
            }
            else
                txtPorcDesconto.Text = "0,00";
        }

        private void txtTotalDesconto_Validating(object sender, CancelEventArgs e)
        {
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

                    SumTotalOrcamento();
                }
            }
            else
                txtTotalDesconto.Text = "0,00";
        }

        private void txtPorcAcrescimo_Validating(object sender, CancelEventArgs e)
        {
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
                    SumTotalOrcamento();
                }
            }
            else
                txtPorcAcrescimo.Text = "0,00";
        }

        private void txtTotalAcrescimo_Validating(object sender, CancelEventArgs e)
        {
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

                    SumTotalOrcamento();
                }
            }
            else
                txtTotalAcrescimo.Text = "0,00";
        }

        private void txtTotalIPI_Validating(object sender, CancelEventArgs e)
        {
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

                    SumTotalOrcamento();
                }
            }
            else
                txtTotalIPI.Text = "0,00";
        }        

        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
            if (txtPorcDesconto.Text == "0,00" )
                txtPorcDesconto.Text = string.Empty;
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

        private void laserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDORCAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlOrcamento.SelectTab(1);
            }
            else
                ImprimirOrcamentoLJ();
        }

        private void ImprimirOrcamentoLJ()
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
                RowRelatorio.Add(new RowsFiltro("IDORCAMENTO", "System.Int32", "=", _IDORCAMENTO.ToString()));
                LIS_ORCAMENTOCollection LIS_ORCAMENTOCollPrint = new LIS_ORCAMENTOCollection();
                LIS_ORCAMENTOCollPrint = LIS_ORCAMENTOP.ReadCollectionByParameter(RowRelatorio);

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


                e.Graphics.DrawString("Nº ORÇAMENTO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString(Entity.IDORCAMENTO.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

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
                e.Graphics.DrawString(LIS_ORCAMENTOCollPrint[0].NOMEFORMAPGTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 215);
               
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

                //Itens de Peças;
                linha = linha + 25;
                linhaBorda = linhaBorda + 45;
                foreach (LIS_PRODORCAMENTOEntity item in LIS_PRODORCAMENTOColl)
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
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 525, linha, 225, 80);
                linha = linha + 5;
                e.Graphics.DrawString("Validade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DTVALIDADE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                e.Graphics.DrawString("Produtos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalProdAdicional.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Vendedor / Atendente:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(LIS_ORCAMENTOCollPrint[0].NOMEFUNCIONARIO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 170, linha);
                e.Graphics.DrawString("IPI/Impostos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalIPI.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Prazo Entrega:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString("Descontos:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_ORCAMENTOCollPrint[0].PRAZOENTREGA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtPorcDesconto.Text + "% " + txtTotalDesconto.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                e.Graphics.DrawString("Transporte:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);

                e.Graphics.DrawString("Acréscimo:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                e.Graphics.DrawString(LIS_ORCAMENTOCollPrint[0].NOMETRANSPORTE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 125, linha);

                linha = linha + 15;
                e.Graphics.DrawString(txtPorcAcrescimo.Text + "% " + txtTotalAcrescimo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);
                

                e.Graphics.DrawString("Total Orçamento:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 530, linha);
                linha = linha + 15;
                e.Graphics.DrawString(txtTotalOrcamento.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 730, linha, stringFormat);

                //Observação
                linha = linha + 5;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, linha, config.MargemDireita - 20, 100);
                linha = linha + 5;
                e.Graphics.DrawString("Observação:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
                linha = linha + 15;
                e.Graphics.DrawString(Util.QuebraString(Util.LimiterText(Entity.OBSERVACAO, 390), 118), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);

                //Final da linha
                linha = linha + 55;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, linha);

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        
        }

        private void lkComp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDORCAMENTO == -1)
            {
                MessageBox.Show("Antes de adicionar composições é necessário gravar o Orçamento!",
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
                    
                    if(result > 0)
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
            foreach ( LIS_COMPOSPRODUTOEntity item in LIS_COMPOSPRODUTOColl)
            {
                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                PRODORCAMENTOTy.VALORUNITARIO = PRODUTOStY.VALORVENDA1;

                PRODORCAMENTOTy.VALORTOTAL = PRODUTOStY.VALORVENDA1 * item.QUANTIDADE;
                PRODORCAMENTOTy.IDORCAMENTO = _IDORCAMENTO;
                PRODORCAMENTOTy.IDPRODORCAMENTO = -1;

                //Salva o produtos no orcamento
                PRODORCAMENTOP.Save(PRODORCAMENTOTy);
            }

            ////Lista os produtos do orçamento
            ListaProdutoOrcamento(_IDORCAMENTO);
            
            //Grava os dados do orçamento
            CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava(); this.Cursor = Cursors.Default;           
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

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlOrcamento.SelectTab(1);
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Orçamentos");
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
                e.Graphics.DrawString("Nº", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 120, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 340, 170);
                e.Graphics.DrawString("Funcionário", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, 170);
                e.Graphics.DrawString("Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 630, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_ORCAMENTOColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_ORCAMENTOColl.Count)
                {
                    if (LIS_ORCAMENTOColl[IndexRegistro].IDORCAMENTO != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_ORCAMENTOColl[IndexRegistro].IDORCAMENTO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_ORCAMENTOColl[IndexRegistro].DTEMISSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORCAMENTOColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 120, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORCAMENTOColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 340, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ORCAMENTOColl[IndexRegistro].NOMEFUNCIONARIO, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_ORCAMENTOColl[IndexRegistro].TOTAORCAMENTO).ToString("n2");
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
                if (IndexRegistro < LIS_ORCAMENTOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_ORCAMENTOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

            foreach (LIS_ORCAMENTOEntity item in LIS_ORCAMENTOColl)
            {
                result += Convert.ToDecimal(item.TOTAORCAMENTO);
            }
            return result;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlOrcamento.SelectTab(1);
            }
            else
            {
                using (FrmRelatorioPOrcamento frm = new FrmRelatorioPOrcamento())
                {
                    frm.LIS_ORCAMENTOCollRelatPers = LIS_ORCAMENTOColl;
                    frm.ShowDialog();
                    btnPesquisa_Click(null, null);
                }

            }     
        }

        private void matricialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlOrcamento.SelectTab(1);
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
            ImprimeTexto imp = new ImprimeTexto();

            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDORCAMENTO", "System.Int32", "=", _IDORCAMENTO.ToString()));
            LIS_ORCAMENTOCollection LIS_ORCAMENTOCollPrint = new LIS_ORCAMENTOCollection();
            LIS_ORCAMENTOCollPrint = LIS_ORCAMENTOP.ReadCollectionByParameter(RowRelatorio);

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
            imp.ImpLF("ORCAMENTO N." + Entity.IDORCAMENTO.ToString().PadLeft(6, '0'));

            //Dados Cliente
            //Armazena dados do cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            imp.ImpLF("-------------------------------------------------");
            imp.ImpLF(Util.RemoverAcentos("Cliente: " + LIS_CLIENTEColl[0].IDCLIENTE + " " + Util.LimiterText(LIS_CLIENTEColl[0].NOME, 40)));
            imp.ImpLF(Util.RemoverAcentos("Endereco: " + Util.LimiterText(LIS_CLIENTEColl[0].ENDERECO1, 40)));
            imp.ImpLF(Util.RemoverAcentos("Cidade: " + Util.LimiterText(LIS_CLIENTEColl[0].MUNICIPIO, 40)) + " UF: " + LIS_CLIENTEColl[0].UF);
            imp.ImpLF("Tel.: " + LIS_CLIENTEColl[0].TELEFONE1);
            imp.ImpLF("-------------------------------------------------");

            //Produtos
            imp.ImpLF("Produtos");
            imp.ImpCol(0, "Quant.");
            imp.ImpCol(6, "Descricao");
            imp.ImpCol(28, "Vl. Unit");
            imp.ImpCol(39, "Vl. Total");
            imp.Pula(1);
            foreach (LIS_PRODORCAMENTOEntity item in LIS_PRODORCAMENTOColl)
            {
                imp.ImpCol(0, item.QUANTIDADE.ToString());
                imp.ImpCol(6, Util.RemoverAcentos(Util.LimiterText(item.NOMEPRODUTO, 20)));
                imp.ImpCol(24, String.Format("{0,12}", Convert.ToDecimal(item.VALORUNITARIO).ToString("n2")));
                imp.ImpCol(36, String.Format("{0,12}", Convert.ToDecimal(item.VALORTOTAL).ToString("n2")));
                imp.Pula(1);
            }

            imp.ImpLF("-------------------------------------------------");

            imp.ImpCol(20, "Produtos:");
            imp.ImpCol(36, String.Format("{0,12}", txtTotalProdAdicional.Text));

            imp.Pula(1);
            imp.ImpCol(20, "IPI/Impostos:");
            imp.ImpCol(36, String.Format("{0,12}", txtTotalIPI.Text));

            imp.Pula(1);
            imp.ImpCol(20, "Descontos:");
            imp.ImpCol(36, String.Format("{0,12}", txtTotalDesconto.Text));

            imp.Pula(1);
            imp.ImpCol(20, "Acrescimo:");
            imp.ImpCol(36,  String.Format("{0,12}", txtTotalAcrescimo.Text));

            imp.Pula(1);
            imp.ImpCol(20, "Total Orcamento:");
            imp.ImpCol(36, String.Format("{0,12}", txtTotalOrcamento.Text));

            imp.Pula(2);


            imp.ImpLF("Garantia: " + Convert.ToDateTime(LIS_ORCAMENTOCollPrint[0].DTVALIDADE).ToString("dd/MM/yyyy"));
            imp.ImpLF("Vendedor / Atendente: " + Util.RemoverAcentos(LIS_ORCAMENTOCollPrint[0].NOMEFUNCIONARIO));
            imp.ImpLF("Prazo Entrega: " + Util.RemoverAcentos(LIS_ORCAMENTOCollPrint[0].PRAZOENTREGA));
            imp.ImpLF("Transporte: " + Util.RemoverAcentos(LIS_ORCAMENTOCollPrint[0].NOMETRANSPORTE));
            imp.ImpLF("Observacao: " );
            imp.ImpLF(Util.QuebraString(Util.RemoverAcentos(Util.LimiterText(LIS_ORCAMENTOCollPrint[0].OBSERVACAO, 50)),500));
            imp.Pula(2);

            imp.Fim();
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanProduto.Text))
                {
                    errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }  
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                decimal ValorVenda = Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue)).VALORVENDA1);
                txtValorUnitProd.Text = ValorVenda.ToString("n2");
            }
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_ORCAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_ORCAMENTOEntity>(orderBy);

                LIS_ORCAMENTOColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_ORCAMENTOColl;
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOrcamento.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (LIS_ORCAMENTOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlOrcamento.SelectTab(1);
            }
            else
            {
                ImprimirListaGeral();
            }
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlOrcamento.SelectTab(1);
            txtCriterioPesquisa.Focus();
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
        
    }
}
