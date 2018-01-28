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
using System.Drawing.Printing;
using BoletoNet;
using BmsSoftware.UI;
using BmsSoftware.Modulos.Operacional;
using VVX;
using System.Diagnostics;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmContasReceber : Form
    {
        Utility Util = new Utility();

        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        DUPLICATARECEBERCollection DUPLICATARECEBERColl = new DUPLICATARECEBERCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        string ObsRecibo = string.Empty;

        public int CodDuplicataSelec = -1;
        public FrmContasReceber()
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

        int _IDDUPLICATARECEBER = -1;
        public DUPLICATARECEBEREntity Entity
        {
            get
            {
                string NUMERO = txtDuplicata.Text;
		        int? IDFORNECEDOR = Convert.ToInt32(cbCliente.SelectedValue);
                
                int? IDCENTROCUSTO = null;
                if(cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

		        DateTime? DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
		        DateTime? DATAVECTO = Convert.ToDateTime(msktDataVecto.Text);
		        
                DateTime? DATAPAGTO = null;
                if (_IDDUPLICATARECEBER != -1)
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

                if (_IDDUPLICATARECEBER != -1)
                {
                    if (txtValorPago.Text == string.Empty)
                        txtValorPago.Text = "0,00";
                }

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
                if(_IDDUPLICATARECEBER == -1)
                     VALORDEVEDOR = Convert.ToDecimal(txtValor.Text);

		        string NOTAFISCAL = txtNotaFiscal.Text;
                if (txtNotaFiscal.Text == string.Empty)
                    NOTAFISCAL = "DP" + txtDuplicata.Text;

                string NCHEQUE = txtncheque.Text; 

                int? IDLOCALCOBRANCA = null;
                if (cbLocalCobranca.SelectedIndex > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                string OBSERVACAO = txtObservacao.Text;


               // if (_IDDUPLICATARECEBER == -1)
               // {
                    if (Convert.ToInt32(cbStatusDuplicata.SelectedValue) != 3 && Convert.ToInt32(cbStatusDuplicata.SelectedValue) != 4)//Pago //Pago Parcial
                        if (DATAVECTO <  Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                            cbStatusDuplicata.SelectedValue = 2; // 2  - Vencida
                        else
                            cbStatusDuplicata.SelectedValue = 1;//Aberto
                //}
           
                int? IDSTATUS = Convert.ToInt32(cbStatusDuplicata.SelectedValue);

                TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(msktDataVecto.Text);
                int DIASATRASO = date.Days;

                if (DIASATRASO < 0)
                    DIASATRASO = 0;

                DateTime? DATAATJUROS = DateTime.Now;

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                decimal COMISSAO = Convert.ToDecimal(txtComissao.Text);

                
                return new DUPLICATARECEBEREntity(_IDDUPLICATARECEBER, NUMERO, IDFORNECEDOR, IDCENTROCUSTO,
                                                DATAEMISSAO, DATAVECTO, DATAPAGTO, IDTIPODUPLICATA,
                                                VALORDUPLICATA, VALORDESCONTO, VALORMULTA,
                                                VALORPAGO, VALORJUROS, VALORDEVEDOR, NOTAFISCAL, NCHEQUE,
                                                IDLOCALCOBRANCA, OBSERVACAO, IDSTATUS,
                                                DIASATRASO, DATAATJUROS,IDFUNCIONARIO, COMISSAO);
            }
            set
            {

                if (value != null)
                {
                    _IDDUPLICATARECEBER = value.IDDUPLICATARECEBER;
                    txtDuplicata.Text = value.NUMERO;
                    cbCliente.SelectedValue = value.IDCLIENTE;

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
                    else
                        cbLocalCobranca.SelectedIndex = -1;


                    txtObservacao.Text = value.OBSERVACAO;
                    cbStatusDuplicata.SelectedValue = value.IDSTATUS;


                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                     else
                        cbFuncionario.SelectedIndex = -1;

                    txtComissao.Text = Convert.ToDecimal(value.COMISSAO).ToString("n2");

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDUPLICATARECEBER = -1;
                    txtDuplicata.Text = string.Empty;
                    cbCliente.SelectedIndex = 0;
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
                    txtComissao.Text = "0,00";
                    txtNotaFiscal.Text = string.Empty;
                    txtncheque.Text = string.Empty;
                    cbLocalCobranca.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    cbStatusDuplicata.SelectedValue = 1;//1 = Aberto
                    cbFuncionario.SelectedIndex = -1;
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
            cbVendedor.Image = Util.GetAddressImage(6);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            GetDropCliente();
            GetDropCentroCusto();
            GetDropCentroCusto2();
            GetDropTipoDuplicata();
            GetDropLocalCobranca();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetDropStatusDuplicata();
            GetDropStatusDuplicata2();
            GetFuncionario();

            txtDuplicata.Focus();

            if (CodDuplicataSelec != -1)
                Entity = DUPLICATARECEBERP.Read(CodDuplicataSelec);

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

        private void GetDropStatusDuplicata()
        {
            try
            {
                STATUSDUPLICATAProvider STATUSDUPLICATAP = new STATUSDUPLICATAProvider();
                cbStatusDuplicata.DisplayMember = "NOMESTATUS";
                cbStatusDuplicata.ValueMember = "IDSTATUSDUPLICATA";

                cbStatusDuplicata.DataSource = STATUSDUPLICATAP.ReadCollectionByParameter(null, "IDSTATUSDUPLICATA");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void GetDropStatusDuplicata2()
        {
            try
            {
                STATUSDUPLICATAProvider STATUSDUPLICATAP = new STATUSDUPLICATAProvider();
                STATUSDUPLICATACollection STATUSDUPLICATAColl = new STATUSDUPLICATACollection();

                STATUSDUPLICATAColl = STATUSDUPLICATAP.ReadCollectionByParameter(null, "NOMESTATUS");

                cbStatusDuplicata2.DisplayMember = "NOMESTATUS";
                cbStatusDuplicata2.ValueMember = "IDSTATUSDUPLICATA";

                STATUSDUPLICATAEntity STATUSDUPLICATATy = new STATUSDUPLICATAEntity();
                STATUSDUPLICATATy.NOMESTATUS = ConfigMessage.Default.MsgDrop;
                STATUSDUPLICATATy.IDSTATUSDUPLICATA = -1;
                STATUSDUPLICATAColl.Add(STATUSDUPLICATATy);

                Phydeaux.Utilities.DynamicComparer<STATUSDUPLICATAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSDUPLICATAEntity>(cbStatusDuplicata2.DisplayMember);

                STATUSDUPLICATAColl.Sort(comparer.Comparer);
                cbStatusDuplicata2.DataSource = STATUSDUPLICATAColl;

                cbStatusDuplicata2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

            cbCentroCusto.SelectedIndex = 0;
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
            //this.Close();
            this.Dispose();

        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.Dispose();
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

                    cbCliente.SelectedValue = result;
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

        private void btnCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetDropCentroCusto();
                cbCentroCusto.SelectedValue = CodSelec;
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
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                GetDropTipoDuplicata();
                cbTipo.SelectedValue = CodSelec;
            }
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
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
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
                    if (_IDDUPLICATARECEBER == -1)
                    {
                        _IDDUPLICATARECEBER = DUPLICATARECEBERP.Save(Entity);
                        Entity = DUPLICATARECEBERP.Read(_IDDUPLICATARECEBER);
                    }
                    else
                    {
                        _IDDUPLICATARECEBER = DUPLICATARECEBERP.Save(Entity);
                        btnPesquisa_Click(null, null);
                        Entity = DUPLICATARECEBERP.Read(_IDDUPLICATARECEBER);
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
            else if (cbCliente.SelectedIndex == 0)
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
            else if ((_IDDUPLICATARECEBER == -1) && (txtValorDevedor.Text.Trim().Length == 0 || txtValorDevedor.Text == "0,00"))
            {
                errorProvider1.SetError(txtValorDevedor, ConfigMessage.Default.CampoObrigatorio);
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
            if (_IDDUPLICATARECEBER == -1)
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
                        DUPLICATARECEBERP.Delete(_IDDUPLICATARECEBER);
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

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(Filtro, "DATAVECTO DESC, IDDUPLICATARECEBER");

                //Calculando juros de atraso
                SumJuroDuplicata();

                //Colocando somatorio no final da lista
                LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
                LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
                LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_DUPLICATARECEBERTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_DUPLICATARECEBERTy.VALORMULTA = SumTotalPesquisa("VALORMULTA");
                LIS_DUPLICATARECEBERTy.VALORJUROS = SumTotalPesquisa("VALORJUROS");
                LIS_DUPLICATARECEBERTy.COMISSAO = SumTotalPesquisa("COMISSAO");
                LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_DUPLICATARECEBERColl;

                lblTotalPesquisa.Text = (LIS_DUPLICATARECEBERColl.Count -1).ToString();
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
            JUROSDUPLICATASty = JUROSDUPLICATASP.Read(2);
           
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                //Somente calcula juros de duplicatas que não foram atualizada no dia
                // e vencidas
                string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                string DataAtDupl = Convert.ToDateTime(item.DATAATJUROS).ToString("dd/MM/yyyy");
                if (item.DATAVECTO < DateTime.Now && item.IDSTATUS != 3)
                   {
                       //Calculo de dias de vencimento
                       TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(item.DATAVECTO);
                       int DIASATRASO = date.Days;
                       item.DIASATRASO = DIASATRASO;
                       item.DATAATJUROS = DateTime.Now;

                    //Calculo o juros de atraso
                       if (JUROSDUPLICATASty.FLAGCALCULAR == "S")
                       {

                           //Calculo o juros de atraso
                           decimal PorcJuros = Convert.ToDecimal(JUROSDUPLICATASty.JUROSDIA * item.DIASATRASO);
                           PorcJuros = PorcJuros / 100;
                           item.VALORJUROS = item.VALORDUPLICATA * PorcJuros;
                           item.VALORDEVEDOR = item.VALORJUROS + JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                               + JUROSDUPLICATASty.OUTRAS + item.VALORDUPLICATA;

                           item.VALORMULTA = JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                               + JUROSDUPLICATASty.OUTRAS;

                           //Salvando no banco
                           DUPLICATARECEBEREntity DUPLICATARECEBER_Sav_Ty = new DUPLICATARECEBEREntity();
                           DUPLICATARECEBER_Sav_Ty = DUPLICATARECEBERP.Read(Convert.ToInt32(item.IDDUPLICATARECEBER));
                           DUPLICATARECEBER_Sav_Ty.VALORJUROS = item.VALORJUROS;
                           DUPLICATARECEBER_Sav_Ty.VALORDEVEDOR = item.VALORDEVEDOR;
                           DUPLICATARECEBER_Sav_Ty.VALORMULTA = item.VALORMULTA;
                           DUPLICATARECEBER_Sav_Ty.DIASATRASO = item.DIASATRASO;
                           DUPLICATARECEBER_Sav_Ty.DATAATJUROS = item.DATAATJUROS;
                           DUPLICATARECEBERP.Save(DUPLICATARECEBER_Sav_Ty);
                       }
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
                /// referente ao tipo de campo
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_DUPLICATARECEBERColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATARECEBERColl;
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

                    LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(Filtro, "DATAVECTO DESC, IDDUPLICATARECEBER");


                    //Calculando juros de atraso
                    SumJuroDuplicata();

                    //Colocando somatorio no final da lista
                    LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
                    LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
                    LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                    LIS_DUPLICATARECEBERTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                    LIS_DUPLICATARECEBERTy.VALORMULTA = SumTotalPesquisa("VALORMULTA");
                    LIS_DUPLICATARECEBERTy.VALORJUROS = SumTotalPesquisa("VALORJUROS");
                    LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);


                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATARECEBERColl;

                    lblTotalPesquisa.Text = (LIS_DUPLICATARECEBERColl.Count - 1).ToString();
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
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORMULTA")
                    valortotal += Convert.ToDecimal(item.VALORMULTA);
                else if (NomeCampo == "VALORJUROS")
                    valortotal += Convert.ToDecimal(item.VALORJUROS);
                else if (NomeCampo == "COMISSAO")
                    valortotal += Convert.ToDecimal(item.COMISSAO);
            }

            return valortotal;
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

                int NumerorRegistros = LIS_DUPLICATARECEBERColl.Count;

                while (IndexRegistro < LIS_DUPLICATARECEBERColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_DUPLICATARECEBERColl[IndexRegistro].NUMERO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORDUPLICATA).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATARECEBERColl[IndexRegistro].DATAVECTO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 180, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].NOMECLIENTE, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].NOMELOCALCOBRANCA, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].NOMETIPODUPLICATA, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 600, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DUPLICATARECEBERColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString(SumCollRelatorio().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 50);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DUPLICATARECEBERColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);


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
            foreach ( LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Duplicatas a Receber");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
            using (FrmJurosContasReceber frm = new FrmJurosContasReceber())
            {
                frm.ShowDialog();
            }
        }       

         private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_DUPLICATARECEBERColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_DUPLICATARECEBERColl.Count.ToString();
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
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity>(orderBy);

                    LIS_DUPLICATARECEBERColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_DUPLICATARECEBERColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void ImprimirDuplicata2Vias()
        {
            ////  'define o objeto para visualizar a impressao
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
     
        private void ImprimirDuplicata1Via()
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

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 470);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita,100);

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
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10 , 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120,180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 140, config.MargemDireita - 560 , 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 145);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 155);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda +15, 200);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 200);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 215);

                //Dados do Cliente
                //Armazena dados do cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

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
                NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORDEVEDOR));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 385);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 415);
                e.Graphics.DrawString("que pagarei(mos) á "+ EMPRESATy.NOMECLIENTE +" ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void printDocument3_2Vias_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

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
                
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

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
                NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORDEVEDOR));
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
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 660);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 620);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 660);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 620);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 660);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 620);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 635);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 620, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(Entity.VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 660);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 620);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 660);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 620, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 625);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 635);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 685);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 685);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15,700);


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
                    NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORDEVEDOR));
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

        private void printDocument4_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Recibo 1 um via
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 330);
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

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(Entity.VALORPAGO).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORPAGO));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 140);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: " , config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 160);
                e.Graphics.DrawString("Nome:     " + LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 180);

                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString("CNPJ/CPF: " + CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 200);

                e.Graphics.DrawString("Endereço: " + LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 220);
                e.Graphics.DrawString("Cidade:   " + LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 240);
                e.Graphics.DrawString("UF: " + LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 240);
                e.Graphics.DrawString("CEP: " + LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 240);

                e.Graphics.DrawString("Referente Duplicata nº: " +  Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 260);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 280, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 280);
                e.Graphics.DrawString(Util.QuebraString(ObsRecibo,60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 295);
 
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 280, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 310);
                e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 325);
                

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void printDocument5_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Recibo 2 um via
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio da impressão - 1º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 340);
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

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(Entity.VALORPAGO).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORPAGO));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 140);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 160);
                e.Graphics.DrawString("Nome:     " + LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 180);

                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString("CNPJ/CPF: " + CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 200);

                e.Graphics.DrawString("Endereço: " + LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 220);
                e.Graphics.DrawString("Cidade:   " + LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 240);
                e.Graphics.DrawString("UF: " + LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 240);
                e.Graphics.DrawString("CEP: " + LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 240);

                e.Graphics.DrawString("Referente Duplicata nº: " + Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 260);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 280, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 280);
                e.Graphics.DrawString(Util.QuebraString(ObsRecibo, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 295);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 280, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 310);
                e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 325);
                e.Graphics.DrawString("1º Via", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 720, 355);


                //Inicio da 2º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 380, config.MargemDireita, 340);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 380, config.MargemDireita, 100);

                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 388);
                    }
                }

                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 388);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 403);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 418);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 418);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 433);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 448);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 463);

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 388);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 403);
                e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAEMISSAO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 418);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(Entity.VALORPAGO).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 490);
                NpExtenso.SetNumero(Convert.ToDecimal(Entity.VALORPAGO));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 490);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 510);
                e.Graphics.DrawString("Nome:     " + LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 530);

                e.Graphics.DrawString("CNPJ/CPF: " + CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 550);

                e.Graphics.DrawString("Endereço: " + LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 570);
                e.Graphics.DrawString("Cidade:   " + LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 590);
                e.Graphics.DrawString("UF: " + LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 590);
                e.Graphics.DrawString("CEP: " + LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 590);

                e.Graphics.DrawString("Referente Duplicata nº: " + Entity.NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 610);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10,630, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 630);
                e.Graphics.DrawString(Util.QuebraString(ObsRecibo, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20,645);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 630, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 660);
                e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 675);
                e.Graphics.DrawString("2º Via", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 720, 705);


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }
        
        private void ImprimirRecibo1Via()
        {
            ObsRecibo = InputBox("Observação do Recibo", ConfigSistema1.Default.NomeEmpresa, "");
           
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument4.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 400);

                objPrintPreview.Document = printDocument4;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void umaViaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
                ImprimirRecibo1Via();
        }

        private void duasViasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
                ImprimirRecibo2Via();
        }

        private void ImprimirRecibo2Via()
        {
            ObsRecibo = InputBox("Observação do Recibo", ConfigSistema1.Default.NomeEmpresa, "");

            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument5.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 750);

                objPrintPreview.Document = printDocument5;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
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

                try
                {
                    switch (CONFIGBOLETATy.IDBANCO)
                    {
                        case 5:
                            //Banco Brasil.
                            PrintBoletoBancario.ImprimiBoletaBrasil(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                    Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                    ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 3:
                            // Banco Basa. 
                            PrintBoletoBancario.ImprimiBoletaBasa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 12:
                            // Banco Santander.                            
                            PrintBoletoBancario.ImprimiBoletaSantander(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 2:
                            //Banco Banrisul
                            PrintBoletoBancario.ImprimiBoletaBanrisul(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 7:
                            //Banco BRB
                            PrintBoletoBancario.ImprimiBoletaBRB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 6:
                            //Banco Caixa
                            PrintBoletoBancario.ImprimiBoletaCaixa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                     Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                     ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 4:
                            //Bradesco
                            PrintBoletoBancario.ImprimiBoletaBradesco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 9:
                            //Itau
                            PrintBoletoBancario.ImprimiBoletaItau(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 13:
                            //Sudameris();
                            PrintBoletoBancario.ImprimiBoletaSudameris(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 10:
                            //Real
                            PrintBoletoBancario.ImprimiBoletaReal(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                       Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                       ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 8:
                            //HSBC
                            PrintBoletoBancario.ImprimiBoletaHSBC(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 14:
                            //Unibanco
                            PrintBoletoBancario.ImprimiBoletaUnibanco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 11:
                            //Safra
                            PrintBoletoBancario.ImprimiBoletaSafra(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 15:
                            //SICOOB
                            PrintBoletoBancario.ImprimirBoletaSICOOB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      Entity.IDDUPLICATARECEBER);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
       
        private void boletaBancariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else ImprimirBoleta();
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[indice].IDDUPLICATARECEBER);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = DUPLICATARECEBERP.Read(CodigoSelect);

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
                            DUPLICATARECEBERP.Delete(CodigoSelect);
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

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void avulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmReciboAvulso frm = new FrmReciboAvulso())
            {
                frm.ShowDialog();
            }
        }

        private void umaViaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
                ImprimirDuplicata1Via();
        }

        private void duasViasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
                ImprimirDuplicata2Vias();
        }       

        private void ImprimirDuplicata1ViaComposta()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

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

        private void printDocument6_PrintPage(object sender, PrintPageEventArgs e)
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
                RowDuplicata.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", txtNotaFiscal.Text));
                DUPLICATARECEBERCollection DUPLICATARECEBERCollC = new DUPLICATARECEBERCollection();
                DUPLICATARECEBERCollC = DUPLICATARECEBERP.ReadCollectionByParameter(RowDuplicata, "DATAVECTO");

                //Busca o ultimo vecto
                //e soma os totais da duplicata
                Decimal TotalDuplicata = 0;
                DateTime UltimoVecto = Convert.ToDateTime(Entity.DATAVECTO);
                foreach (DUPLICATARECEBEREntity item in DUPLICATARECEBERCollC)
                {
                    TotalDuplicata += Convert.ToDecimal(item.VALORDEVEDOR);
                    UltimoVecto = Convert.ToDateTime(item.DATAVECTO);                 
                }


                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(Entity.NOTAFISCAL, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(Entity.NOTAFISCAL, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


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

                CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
                if(DUPLICATARECEBERCollC[0].IDCENTROCUSTO != null)
                {
                    CENTROCUSTOSTy = CENTROCUSTOSP.Read(Convert.ToInt32(DUPLICATARECEBERCollC[0].IDCENTROCUSTO));
                    e.Graphics.DrawString("CENTRO DE CUSTO: " + CENTROCUSTOSTy.DESCRICAO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 230);
                 }

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Entity.IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

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
                e.Graphics.DrawString("Vecto", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 150, 510);

                //2º Coluna
                if (DUPLICATARECEBERCollC.Count > 3)
                {
                    e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 220, 510);
                    e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 310, 510);
                    e.Graphics.DrawString("Vecto", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 360, 510);
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

        private void compostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
                ImprimirDuplicata1ViaComposta();
        }

       
        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void txtComissao_Validating(object sender, CancelEventArgs e)
        {
            if (txtComissao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtComissao.Text))
                {
                    errorProvider1.SetError(txtComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtComissao.Text);
                    txtComissao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtComissao, "");
                }
            }
            else
            {
                txtComissao.Text = "0,00";
                errorProvider1.SetError(txtComissao, "");
            }
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

        private void variosLançamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVariosLancamentoReceberPedido Frm = new FrmVariosLancamentoReceberPedido();
            Frm.ShowDialog();
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
                frm.TituloSelec = "Relação de Contas a Receber";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void baixaDeDuplicataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDDUPLICATARECEBER == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCadDuplicata.SelectTab(1);
            }
            else
            {
                using (FrmBaixarTotalReceber FrmBaixar = new FrmBaixarTotalReceber())
                {
                    FrmBaixar._idDuplicata = _IDDUPLICATARECEBER;
                    FrmBaixar.ShowDialog();
                    Entity = DUPLICATARECEBERP.Read(_IDDUPLICATARECEBER);
                }
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATARECEBERColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                    Entity = DUPLICATARECEBERP.Read(CodigoSelect);

                    tabControlCadDuplicata.SelectTab(0);
                    txtDuplicata.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_DUPLICATARECEBERColl[rowindex].NUMERO + " - " + LIS_DUPLICATARECEBERColl[rowindex].NOMECLIENTE,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodigoSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                            //Delete Pedido
                            DUPLICATARECEBERP.Delete(CodigoSelect);

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
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEVEND", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMECENTROCUSTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO DESC, IDDUPLICATARECEBER");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_DUPLICATARECEBERColl;

                    lblTotalPesquisa.Text = LIS_DUPLICATARECEBERColl.Count.ToString();
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
