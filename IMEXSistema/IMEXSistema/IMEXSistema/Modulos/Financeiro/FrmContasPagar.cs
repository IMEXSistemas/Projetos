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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using VVX;
using System.Diagnostics;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmContasPagar : Form
    {
        Utility Util = new Utility();

        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        DUPLICATAPAGARCollection DUPLICATAPAGARColl = new DUPLICATAPAGARCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public int CodDuplicataSelec = -1;

        public FrmContasPagar()
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
            lblObsField.Text = "Obs.:";
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDDUPLICATAPAGAR = -1;
        public DUPLICATAPAGAREntity Entity
        {
            get
            {
                string NUMERO = txtDuplicata.Text;
		        int? IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);
                
                int? IDCENTROCUSTO = null;
                if(cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

		        DateTime? DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
		        DateTime? DATAVECTO = Convert.ToDateTime(msktDataVecto.Text);

                DateTime? DATAPAGTO = null;
                if (_IDDUPLICATAPAGAR != -1)
                {
                    if (msktDataPagto.Text != "  /  /")
                        DATAPAGTO = Convert.ToDateTime(msktDataPagto.Text);
                }

                int? IDTIPODUPLICATA = null;
                if (cbTipo.SelectedIndex > 0)
                IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);


                if (txtValorDesconto.Text == string.Empty)
                    txtValorDesconto.Text = "0,00";

                if (txtValorMulta.Text == string.Empty)
                    txtValorMulta.Text = "0,00";

               // if (txtValorPago.Text == string.Empty)
                    txtValorPago.Text = "0,00";

                if (txtJuros.Text == string.Empty)
                    txtJuros.Text = "0,00";

                if (txtValorDevedor.Text == string.Empty)
                    txtValorDevedor.Text = "0,00";

                decimal? VALORDUPLICATA = Convert.ToDecimal(txtValor.Text);
                decimal? VALORDESCONTO = Convert.ToDecimal(txtValorDesconto.Text);
                decimal? VALORMULTA = Convert.ToDecimal(txtValorMulta.Text);
                decimal? VALORPAGO = Convert.ToDecimal(txtValorPago.Text);
                decimal? VALORJUROS = Convert.ToDecimal(txtJuros.Text);

                decimal? VALORDEVEDOR = Convert.ToDecimal(txtValorDevedor.Text);
                if(_IDDUPLICATAPAGAR == -1)
                     VALORDEVEDOR = Convert.ToDecimal(txtValor.Text);

		        string NOTAFISCAL = txtNotaFiscal.Text;
                string NCHEQUE = txtncheque.Text; 

                int? IDLOCALCOBRANCA = null;
                if (cbLocalCobranca.SelectedIndex > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                string OBSERVACAO = txtObservacao.Text;

                    if (Convert.ToInt32(cbStatusDuplicata.SelectedValue) != 3)//Pago
                        if (DATAVECTO < DateTime.Now)
                            cbStatusDuplicata.SelectedValue = 2; // 2  - Vencida
                        else
                            cbStatusDuplicata.SelectedValue = 1;//Aberto
              
                int? IDSTATUS = Convert.ToInt32(cbStatusDuplicata.SelectedValue);

                TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(msktDataVecto.Text);
                int DIASATRASO = date.Days;

                if (DIASATRASO < 0)
                    DIASATRASO = 0;

                DateTime? DATAATJUROS = DateTime.Now;             
                
                return new DUPLICATAPAGAREntity(_IDDUPLICATAPAGAR, NUMERO, IDFORNECEDOR, IDCENTROCUSTO,
                                                DATAEMISSAO, DATAVECTO, DATAPAGTO, IDTIPODUPLICATA,
                                                VALORDUPLICATA, VALORDESCONTO, VALORMULTA,
                                                VALORPAGO, VALORJUROS, VALORDEVEDOR, NOTAFISCAL, NCHEQUE,
                                                IDLOCALCOBRANCA, OBSERVACAO, IDSTATUS,
                                                DIASATRASO, DATAATJUROS);
            }
            set
            {

                if (value != null)
                {
                    _IDDUPLICATAPAGAR = value.IDDUPLICATAPAGAR;
                    txtDuplicata.Text = value.NUMERO;
                    cbFornecedor.SelectedValue = value.IDFORNECEDOR;

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;

                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAEMISSAO).ToString("dd/MM/yyyy");
                    msktDataVecto.Text = Convert.ToDateTime(value.DATAVECTO).ToString("dd/MM/yyyy");

                    if (value.DATAPAGTO != null)
                        msktDataPagto.Text = Convert.ToDateTime(value.DATAPAGTO).ToString("dd/MM/yyyy");
                    else
                        msktDataPagto.Text = "  /  /";

                    if (value.IDTIPODUPLICATA != null)
                        cbTipo.SelectedValue = value.IDTIPODUPLICATA;

                    txtValor.Text = Convert.ToDecimal(value.VALORDUPLICATA).ToString("n2");

                    if (txtValorDesconto.Text == string.Empty)
                        txtValorDesconto.Text = "0,00";
                    txtValorDesconto.Text = Convert.ToDecimal(value.VALORDESCONTO).ToString("n2");

                    if (txtValorMulta.Text == string.Empty)
                        txtValorMulta.Text = "0,00";
                    txtValorMulta.Text = Convert.ToDecimal(value.VALORMULTA).ToString("n2");

                    if (txtValorPago.Text == string.Empty)
                        txtValorPago.Text = "0,00";
                    txtValorPago.Text = Convert.ToDecimal(value.VALORPAGO).ToString("n2");

                    if (txtJuros.Text == string.Empty)
                        txtJuros.Text = "0,00";
                    txtJuros.Text = Convert.ToDecimal(value.VALORJUROS).ToString("n2");

                    if (txtValorDevedor.Text == string.Empty)
                        txtValorDevedor.Text = "0,00";
                    txtValorDevedor.Text = Convert.ToDecimal(value.VALORDEVEDOR).ToString("n2");

                    txtNotaFiscal.Text = value.NOTAFISCAL;
                    txtncheque.Text = value.NCHEQUE;

                    if (value.IDLOCALCOBRANCA != null)
                        cbLocalCobranca.SelectedValue = value.IDLOCALCOBRANCA;

                    txtObservacao.Text = value.OBSERVACAO;
                    cbStatusDuplicata.SelectedValue = value.IDSTATUS;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDUPLICATAPAGAR = -1;
                    txtDuplicata.Text = string.Empty;
                    cbFornecedor.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;
                    msktDataEmissao.Text = string.Empty;
                    msktDataVecto.Text = string.Empty;
                    msktDataPagto.Text = string.Empty;
                    cbTipo.SelectedIndex = 0;
                    txtValor.Text ="0,00";
                    txtValorDevedor.Text = "0,00";
                    txtValorDesconto.Text = "0,00";
                    txtValorMulta.Text = "0,00";
                    txtValorPago.Text = "0,00";
                    txtJuros.Text = "0,00";
                    txtValorDesconto.Text = "0,00";
                    txtNotaFiscal.Text = string.Empty;
                    txtncheque.Text = string.Empty;
                    cbLocalCobranca.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    cbStatusDuplicata.SelectedValue = 1;//1 = Aberto
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmContasPagar_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            btnCadCliente.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);
            btnCentroCusto.Image = Util.GetAddressImage(6);
            btnTipo.Image = Util.GetAddressImage(6);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            GetDropFornecedor();
            GetDropCentroCusto();
            GetDropCentroCusto2();
            GetDropTipoDuplicata();
            GetDropLocalCobranca();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetDropStatusDuplicata();
            GetDropStatusDuplicata2();

            txtDuplicata.Focus();

            if (CodDuplicataSelec != -1)
                 Entity = DUPLICATAPAGARP.Read(CodDuplicataSelec);

            VerificaAcesso();

            this.Cursor = Cursors.Default;
        }
        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

        private void GetDropStatusDuplicata()
        {
            STATUSDUPLICATAProvider STATUSDUPLICATAP = new STATUSDUPLICATAProvider();
            cbStatusDuplicata.DisplayMember = "NOMESTATUS";
            cbStatusDuplicata.ValueMember = "IDSTATUSDUPLICATA";

            cbStatusDuplicata.DataSource = STATUSDUPLICATAP.ReadCollectionByParameter(null, "IDSTATUSDUPLICATA");
        }


        private void GetDropStatusDuplicata2()
        {
            STATUSDUPLICATAProvider STATUSDUPLICATAP = new STATUSDUPLICATAProvider();
            STATUSDUPLICATACollection STATUSDUPLICATAColl = new STATUSDUPLICATACollection();

            cbStatusDuplicata2.DisplayMember = "NOMESTATUS";
            cbStatusDuplicata2.ValueMember = "IDSTATUSDUPLICATA";

            STATUSDUPLICATAColl = STATUSDUPLICATAP.ReadCollectionByParameter(null);

            STATUSDUPLICATAEntity STATUSDUPLICATATy = new STATUSDUPLICATAEntity();
            STATUSDUPLICATATy.NOMESTATUS = ConfigMessage.Default.MsgDrop;
            STATUSDUPLICATATy.IDSTATUSDUPLICATA = -1;
            STATUSDUPLICATAColl.Add(STATUSDUPLICATATy);

            Phydeaux.Utilities.DynamicComparer<STATUSDUPLICATAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSDUPLICATAEntity>(cbStatusDuplicata2.DisplayMember);

            STATUSDUPLICATAColl.Sort(comparer.Comparer);
            cbStatusDuplicata2.DataSource = STATUSDUPLICATAColl;

            cbStatusDuplicata2.SelectedIndex = 0;
        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();
            TIPODUPLICATAColl = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            TIPODUPLICATAEntity TIPODUPLICATATy = new TIPODUPLICATAEntity();
            TIPODUPLICATATy.NOME = ConfigMessage.Default.MsgDrop;
            TIPODUPLICATATy.IDTIPODUPLICATA = -1;
            TIPODUPLICATAColl.Add(TIPODUPLICATATy);

            Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity>(cbTipo.DisplayMember);

            TIPODUPLICATAColl.Sort(comparer.Comparer);
            cbTipo.DataSource = TIPODUPLICATAColl;

            cbTipo.SelectedIndex = 0;
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

            cbCentroCusto.SelectedIndex = 0;
        }

        private void GetDropCentroCusto2()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCusto2.DisplayMember = "DESCRICAO";
            cbCentroCusto2.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto2.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCusto2.DataSource = CENTROCUSTOSColl;

            cbCentroCusto2.SelectedIndex = 0;
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
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmContasPagar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmContasPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbFornecedor.SelectedValue = result;
                }
            }
            e.Handled = false;
        }

        private void cbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            using (FrmFornecedor frm = new FrmFornecedor())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFornecedor.SelectedValue);
                GetDropFornecedor();
                cbFornecedor.SelectedValue = CodSelec;
            }
        }

        private void cbCentroCusto_Enter(object sender, EventArgs e)
        {
            GetDropCentroCusto();
        }

        private void btnCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                GetDropCentroCusto();
                GetDropCentroCusto2();
            }
        }

        private void msktDataEmissao_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(msktDataEmissao, "");
            }           
        }

        private void msktDataVecto_Leave(object sender, EventArgs e)
        {
           
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataVecto.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataVecto, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(msktDataVecto, "");
            }            
        }

        private void msktDataPagto_Leave(object sender, EventArgs e)
        {
            if (msktDataPagto.Text != "  /  /")
                if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataPagto.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    errorProvider1.SetError(msktDataPagto, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(msktDataPagto, "");
                }
        }

        private void txtValor_Validating(object sender, CancelEventArgs e)
        {
            if (txtValor.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValor.Text))
                {
                    errorProvider1.SetError(txtValor, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValor.Text);
                    txtValor.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValor, "");
                }
            }
            else
            {
                txtValor.Text = "0,00";
                errorProvider1.SetError(txtValor, "");
            }
        }

        private void txtValorDesconto_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorDesconto.Text))
                {
                    errorProvider1.SetError(txtValorDesconto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorDesconto.Text);
                    txtValorDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorDesconto, "");
                }
            }
            else
            {
                txtValorDesconto.Text = "0,00";
                errorProvider1.SetError(txtValorDesconto, "");
            }
        }

        private void txtValorMulta_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorMulta.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMulta.Text))
                {
                    errorProvider1.SetError(txtValorMulta, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorMulta.Text);
                    txtValorMulta.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorMulta, "");
                }
            }
            else
            {
                txtValorMulta.Text = "0,00";
                errorProvider1.SetError(txtValorMulta, "");
            }
        }

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorPago.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text))
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorPago.Text);
                    txtValorPago.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPago, "");
                }
            }
            else
            {
                txtValorPago.Text = "0,00";
                errorProvider1.SetError(txtValorPago, "");
            }
        }

        private void txtJuros_Validating(object sender, CancelEventArgs e)
        {
            if (txtJuros.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtJuros.Text))
                {
                    errorProvider1.SetError(txtJuros, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtJuros.Text);
                    txtJuros.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtJuros, "");
                }
            }
            else
            {
                txtJuros.Text = "0,00";
                errorProvider1.SetError(txtJuros, "");
            }
        }

        private void btnTipo_Click(object sender, EventArgs e)
        {
             using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
            }
        }

        private void cbTipo_Enter(object sender, EventArgs e)
        {
            GetDropTipoDuplicata();
        }

        private void txtValor_Enter(object sender, EventArgs e)
        {
            if (txtValor.Text == "0,00")
                txtValor.Text = string.Empty;
        }

        private void txtValorDesconto_Enter(object sender, EventArgs e)
        {
            if (txtValorDesconto.Text == "0,00")
                txtValorDesconto.Text = string.Empty;
        }

        private void txtValorMulta_Enter(object sender, EventArgs e)
        {
            if (txtValorMulta.Text == "0,00")
                txtValorMulta.Text = string.Empty;
        }

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            if (txtValorPago.Text == "0,00")
                txtValorPago.Text = string.Empty;
        }

        private void txtJuros_Enter(object sender, EventArgs e)
        {
            if (txtJuros.Text == "0,00")
                txtJuros.Text = string.Empty;
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                frm.ShowDialog();
            }
        }

        private void cbLocalPagto_Enter(object sender, EventArgs e)
        {
            GetDropLocalCobranca();
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
                    if (_IDDUPLICATAPAGAR == -1)
                    {
                        _IDDUPLICATAPAGAR = DUPLICATAPAGARP.Save(Entity);
                        Entity = DUPLICATAPAGARP.Read(_IDDUPLICATAPAGAR);
                    }
                    else
                    {
                        _IDDUPLICATAPAGAR = DUPLICATAPAGARP.Save(Entity);
                        btnPesquisa_Click(null, null);
                        Entity = DUPLICATAPAGARP.Read(_IDDUPLICATAPAGAR);
                    }

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
            if (txtDuplicata.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDuplicata, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (cbFornecedor.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFornecedor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataVecto.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataVecto.Text))
            {
                errorProvider1.SetError(msktDataVecto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtValor.Text.Trim().Length == 0 || txtValor.Text == "0,00")
            {
                errorProvider1.SetError(txtValor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCadDuplicata.SelectTab(0);
            txtDuplicata.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCadDuplicata.SelectTab(0);
            txtDuplicata.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDDUPLICATAPAGAR == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
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
                        DUPLICATAPAGARP.Delete(_IDDUPLICATAPAGAR);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        btnPesquisa_Click(null, null);
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

        public void btnPesquisa_Click(object sender, EventArgs e)
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

                if(Convert.ToInt32(cbStatusDuplicata2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatusDuplicata2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(Filtro, "DATAVECTO DESC, IDDUPLICATAPAGAR");

                //Calculando juros de atraso
                SumJuroDuplicata();

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_DUPLICATAPAGARColl;

                lblTotalPesquisa.Text = LIS_DUPLICATAPAGARColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        //Percorre a coleção calculando juros de atraso
        public void SumJuroDuplicata()
        {
            JUROSDUPLICATASEntity JUROSDUPLICATASty = new JUROSDUPLICATASEntity();
            JUROSDUPLICATASProvider JUROSDUPLICATASP = new JUROSDUPLICATASProvider();
            JUROSDUPLICATASty = JUROSDUPLICATASP.Read(1);

            if(JUROSDUPLICATASty.FLAGCALCULAR == "S")
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                //Somente calcula juros de duplicatas que não foram atualizada no dia
                // e vencidas
                string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                string DataAtDupl = Convert.ToDateTime(item.DATAATJUROS).ToString("dd/MM/yyyy");
                if (item.DATAVECTO < DateTime.Now && Convert.ToDateTime(DataAtDupl) <  Convert.ToDateTime(DataAtual)
                    && item.IDSTATUS != 3)
                   {
                       //Calculo de dias de vencimento
                       TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(item.DATAVECTO);
                       int DIASATRASO = date.Days;
                       item.DIASATRASO = DIASATRASO;
                       item.DATAATJUROS = DateTime.Now;

                       //Calculo o juros de atraso
                       decimal PorcJuros = Convert.ToDecimal(JUROSDUPLICATASty.JUROSDIA * item.DIASATRASO);
                       PorcJuros = PorcJuros / 100;
                       item.VALORJUROS = item.VALORDUPLICATA * PorcJuros;
                       item.VALORDEVEDOR = item.VALORJUROS + JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                           + JUROSDUPLICATASty.OUTRAS + item.VALORDUPLICATA;

                       item.VALORMULTA = JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                           + JUROSDUPLICATASty.OUTRAS;

                       //Salvando no banco
                       DUPLICATAPAGAREntity DUPLICATAPAGAR_Sav_Ty = new DUPLICATAPAGAREntity();
                       DUPLICATAPAGAR_Sav_Ty = DUPLICATAPAGARP.Read(Convert.ToInt32(item.IDDUPLICATAPAGAR));
                       DUPLICATAPAGAR_Sav_Ty.VALORJUROS = item.VALORJUROS;
                       DUPLICATAPAGAR_Sav_Ty.VALORDEVEDOR = item.VALORDEVEDOR;
                       DUPLICATAPAGAR_Sav_Ty.VALORMULTA = item.VALORMULTA;
                       DUPLICATAPAGAR_Sav_Ty.DIASATRASO = item.DIASATRASO;
                       DUPLICATAPAGAR_Sav_Ty.DATAATJUROS = item.DATAATJUROS;
                       DUPLICATAPAGARP.Save(DUPLICATAPAGAR_Sav_Ty);
                   }                
            }
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlCadDuplicata.SelectedIndex == 2)
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
                if (LIS_DUPLICATAPAGARColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATAPAGARColl;
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

                    if (Convert.ToInt32(cbStatusDuplicata2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatusDuplicata2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                     if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    
                    LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(Filtro, "DATAVECTO DESC, IDDUPLICATAPAGAR");

                    //Calculando juros de atraso
                    SumJuroDuplicata();

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATAPAGARColl;

                    lblTotalPesquisa.Text = LIS_DUPLICATAPAGARColl.Count.ToString();
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

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
                e.Graphics.DrawString("Nº Duplicata", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Valor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
                e.Graphics.DrawString("Vencto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 180, 170);
                e.Graphics.DrawString("Fornecedor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 170);
                e.Graphics.DrawString("Local Cobrança", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Tipo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_DUPLICATAPAGARColl.Count;

                while (IndexRegistro < LIS_DUPLICATAPAGARColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_DUPLICATAPAGARColl[IndexRegistro].NUMERO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORDUPLICATA).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATAPAGARColl[IndexRegistro].DATAVECTO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 180, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].NOMEFORNECEDOR, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].NOMELOCALCOBRANCA, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].NOMETIPODUPLICATA, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 600, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DUPLICATAPAGARColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString(SumCollRelatorio().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 50);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DUPLICATAPAGARColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);


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

        public decimal SumCollRelatorio()
        {
            decimal result = 0;
            foreach ( LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                result += Convert.ToDecimal(item.VALORDUPLICATA);
            }

            return result;
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Duplicatas a Pagar");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.AllowSelection = true;
                printDialog1.AllowSomePages = true;
                printDialog1.AllowCurrentPage = true;
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

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void listaDeMovimentaçãoPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void txtValorDevedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorDevedor.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorDevedor.Text))
                {
                    errorProvider1.SetError(txtValorDevedor, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorDevedor.Text);
                    txtValorDevedor.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorDevedor, "");
                }
            }
            else
            {
                txtValorDevedor.Text = "0,00";
                errorProvider1.SetError(txtValorDevedor, "");
            }
        }

        private void txtValorDevedor_Enter(object sender, EventArgs e)
        {
            if (txtValorDevedor.Text == "0,00")
                txtValorDevedor.Text = string.Empty;
        }

        private void jurosPorAtrasoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmJurosContasPagar frm = new FrmJurosContasPagar())
            {
                frm.ShowDialog();
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_DUPLICATAPAGARColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_DUPLICATAPAGARColl.Count.ToString();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlCadDuplicata.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlCadDuplicata.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity>(orderBy);

                    LIS_DUPLICATAPAGARColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_DUPLICATAPAGARColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[indice].IDDUPLICATAPAGAR);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = DUPLICATAPAGARP.Read(CodigoSelect);

                    tabControlCadDuplicata.SelectTab(0);
                    txtDuplicata.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            DUPLICATAPAGARP.Delete(CodigoSelect);
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

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lançamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVariosLancamento FrmVar = new FrmVariosLancamento();
            FrmVar.ShowDialog();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
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
                frm.TituloSelec = "Relação de Contas a Pagar";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (LIS_DUPLICATAPAGARColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                    Entity = DUPLICATAPAGARP.Read(CodigoSelect);

                    tabControlCadDuplicata.SelectTab(0);
                    txtDuplicata.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_DUPLICATAPAGARColl[rowindex].NUMERO + " - " + LIS_DUPLICATAPAGARColl[rowindex].NOMEFORNECEDOR,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodigoSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                            //Delete Pedido
                            DUPLICATAPAGARP.Delete(CodigoSelect);

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
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFORNECEDOR", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMECENTROCUSTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO DESC, IDDUPLICATAPAGAR");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATAPAGARColl;

                    lblTotalPesquisa.Text = LIS_DUPLICATAPAGARColl.Count.ToString();
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
       

    }
}
