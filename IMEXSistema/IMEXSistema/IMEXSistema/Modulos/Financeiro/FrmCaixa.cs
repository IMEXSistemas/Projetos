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
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Cadastros;
using System.IO;
using VVX;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmCaixa : Form
    {
        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        LIS_CAIXACollection LIS_CAIXAColl = new LIS_CAIXACollection();
        LIS_CAIXACollection LIS_CAIXAColl_Now = new LIS_CAIXACollection();

        CAIXAProvider CAIXAP = new CAIXAProvider();
        LIS_CAIXAProvider LIS_CAIXAP = new LIS_CAIXAProvider();

        Decimal ValorDebito = 0;
        Decimal Credito = 0;
        Decimal TotalValorDebito = 0;
        Decimal TotalValorCredito = 0;

        string DataMovimentacaoInicial = string.Empty;
        string DataMovimentacaoFinal = string.Empty;
        string DataMovimentacao = string.Empty;

        public FrmCaixa()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        int _IDCAIXA = -1;
        public CAIXAEntity Entity
        {
            get
            {
               int IDTIPOMOVCAIXA = Convert.ToInt32(cbOperacao.SelectedValue);
               string NDOCUMENTO = txtNDocumento.Text;
               int IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);

               int? IDCENTROCUSTOS = null; Convert.ToInt32(cbCentroCustos.SelectedValue);
                if(cbCentroCustos.SelectedIndex > 0)
                    IDCENTROCUSTOS = Convert.ToInt32(cbCentroCustos.SelectedValue);

               decimal VALOR = Convert.ToDecimal(txtValor.Text);
               string OBSERVACAO = txtObservacao.Text;
               DateTime DATAMOV = Convert.ToDateTime(maskedtxtData.Text);

                return new CAIXAEntity(_IDCAIXA, IDTIPOMOVCAIXA, NDOCUMENTO, IDTIPODUPLICATA,
                                        IDCENTROCUSTOS, VALOR, OBSERVACAO, DATAMOV);
            }
            set
            {

                if (value != null)
                {
                    _IDCAIXA = value.IDCAIXA;
                    cbOperacao.SelectedValue = value.IDTIPOMOVCAIXA;
                    txtNDocumento.Text = value.NDOCUMENTO;
                    cbTipo.SelectedValue = value.IDTIPODUPLICATA;

                    if (value.IDCENTROCUSTOS != null)
                        cbCentroCustos.SelectedValue = value.IDCENTROCUSTOS;
                    else
                        cbCentroCustos.SelectedIndex = 0;

                    txtValor.Text = Convert.ToDecimal(value.VALOR).ToString("n2");

                    txtObservacao.Text = value.OBSERVACAO;
                    maskedtxtData.Text = Convert.ToDateTime(value.DATAMOV).ToString("dd/MM/yyyy");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCAIXA = -1;
                    cbOperacao.SelectedIndex = 0;
                    txtNDocumento.Text = string.Empty;
                    cbTipo.SelectedIndex = 0;
                    cbCentroCustos.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    txtValor.Text = string.Empty;
                    maskedtxtData.Text =  DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
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
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }
      

        private void FrmCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            //}

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }     

        private void FrmCaixa_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetDropCentroCusto();
            GetDropCentroCusto2();
            GetDropTipoDuplicata();
            PreencheDropTipoCaixa();         

            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadCentroCus.Image = Util.GetAddressImage(6);
            lblDataAtual.Text =  DateTime.Now.ToLongDateString();

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            FiltraCaixaDia();

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
            }

            maskedtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            VerificaAcesso();

            Entity = null;
            this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void FiltraCaixaDia()
        {
            string Date = Util.ConverStringDateSearch(lblDataAtual.Text);
              
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", "=", Date.ToString()));


            LIS_CAIXAColl_Now.Clear();
            LIS_CAIXAColl_Now = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio, "IDCAIXA desc");
            dsGrdCaixaDia.AutoGenerateColumns = false;
            dsGrdCaixaDia.DataSource = LIS_CAIXAColl_Now;

            SumCaixa();
        }

        private void SumCaixa()
        {
            Decimal DebitoCaixaDia = 0;
            Decimal CreditoCaixaDia = 0;
            Decimal SaldoDia = 0;
            foreach (LIS_CAIXAEntity item in LIS_CAIXAColl_Now)
            {
                if (item.IDTIPOMOVCAIXA == 1)
                    CreditoCaixaDia += Convert.ToDecimal(item.VALOR);
                else if (item.IDTIPOMOVCAIXA == 2)
                    DebitoCaixaDia += Convert.ToDecimal(item.VALOR);
            }

            txtDebitos.Text = DebitoCaixaDia.ToString("n2");
            txtCreditos.Text = CreditoCaixaDia.ToString("n2");

            SaldoDia = CreditoCaixaDia - DebitoCaixaDia;
            txtSaldoDia.Text = SaldoDia.ToString("n2");
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

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);

        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
                GetDropTipoDuplicata();
            }
        }

        private void btnCadCentroCus_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                GetDropCentroCusto();
                GetDropCentroCusto2();
            }
        }

        private void GetDropCentroCusto()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCustos.DisplayMember = "DESCRICAO";
            cbCentroCustos.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCustos.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCustos.DataSource = CENTROCUSTOSColl;

            cbCentroCustos.SelectedIndex = 0;
        }

        private void GetDropCentroCusto2()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCustos2.DisplayMember = "DESCRICAO";
            cbCentroCustos2.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCustos2.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCustos2.DataSource = CENTROCUSTOSColl;

            cbCentroCustos2.SelectedIndex = 0;
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

        private void PreencheDropTipoCaixa()
        {
            TIPOMOVCAIXAProvider TIPOMOVCAIXAP = new TIPOMOVCAIXAProvider();

            cbOperacao.DataSource = TIPOMOVCAIXAP.ReadCollectionByParameter(null, "NOME");
            cbOperacao.DisplayMember = "NOME";
            cbOperacao.ValueMember = "IDTIPOMOVCAIXA";
        }
      
        private void cbTipo_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbCentroCustos_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchCentroCusto frm = new FrmSearchCentroCusto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbCentroCustos.SelectedValue = result;
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

       
        private void cbCentroCustos_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void maskedtxtData_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(maskedtxtData, "");
            }      
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            if (txtValor.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValor.Text))
                {
                    errorProvider1.SetError(txtValor, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
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

        private void txtValor_Enter(object sender, EventArgs e)
        {
            if (txtValor.Text == "0,00")
                txtValor.Text = string.Empty;
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
                    _IDCAIXA = CAIXAP.Save(Entity);
                    FiltraCaixaDia();
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValor.Text) || Convert.ToDecimal(txtValor.Text) <= 0)
            {
                errorProvider1.SetError(txtValor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (cbTipo.SelectedIndex == 0 )
            {
                errorProvider1.SetError(cbTipo, ConfigMessage.Default.CampoObrigatorio);
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if(Convert.ToInt32(cbCentroCustos2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", cbCentroCustos2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                
                LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(Filtro, "DATAMOV desc");


                //Colocando somatorio no final da lista
                LIS_CAIXAEntity LIS_CAIXATy = new LIS_CAIXAEntity();
                LIS_CAIXATy.VALOR = SumTotalPesquisa("VALOR");
                LIS_CAIXAColl.Add(LIS_CAIXATy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CAIXAColl;

                lblTotalPesquisa.Text = (LIS_CAIXAColl.Count - 1).ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlCaixa.SelectedIndex == 2)
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
            if (LIS_CAIXAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CAIXAColl;
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
                    filtroProfile = new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbCentroCustos2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", cbCentroCustos2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(Filtro, "DATAMOV desc");

                //Colocando somatorio no final da lista
                LIS_CAIXAEntity LIS_CAIXATy = new LIS_CAIXAEntity();
                LIS_CAIXATy.VALOR = SumTotalPesquisa("VALOR");
                LIS_CAIXAColl.Add(LIS_CAIXATy);


                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CAIXAColl;

                lblTotalPesquisa.Text = (LIS_CAIXAColl.Count - 1).ToString();
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
            foreach (LIS_CAIXAEntity item in LIS_CAIXAColl)
            {
                if (NomeCampo == "VALOR" && item.IDTIPOMOVCAIXA == 1)
                    valortotal += Convert.ToDecimal(item.VALOR);
                else if(NomeCampo == "VALOR" && item.IDTIPOMOVCAIXA == 2)
                    valortotal -= Convert.ToDecimal(item.VALOR);
            }

            return valortotal;
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCaixa.SelectTab(0);
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCaixa.SelectTab(0);
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDCAIXA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlCaixa.SelectTab(1);
            }
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        CAIXAP.Delete(_IDCAIXA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        btnPesquisa_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro técnico: " + ex.Message);

                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlCaixa.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlCaixa.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_CAIXAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_CAIXAColl[rowindex].IDCAIXA);

                    Entity = CAIXAP.Read(CodigoSelect);

                    tabControlCaixa.SelectTab(0);
                    maskedtxtData.Focus();
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_CAIXAColl_Now.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_CAIXAColl_Now[rowindex].IDCAIXA);
                    Entity = CAIXAP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {

                            if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                            }
                            else
                            {
                                CodSelect = Convert.ToInt32(LIS_CAIXAColl_Now[rowindex].IDCAIXA);
                                CAIXAP.Delete(CodSelect);
                                FiltraCaixaDia();
                                Entity = null;
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

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_CAIXAColl_Now.Count > 0)
            {
                string orderBy = dsGrdCaixaDia.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_CAIXAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CAIXAEntity>(orderBy);

                    LIS_CAIXAColl_Now.Sort(comparer.Comparer);

                    dsGrdCaixaDia.DataSource = null;
                    dsGrdCaixaDia.DataSource = LIS_CAIXAColl_Now;
                }
            }
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_CAIXAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_CAIXAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CAIXAEntity>(orderBy);

                    LIS_CAIXAColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_CAIXAColl;
                }
            }
        }

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Geral de Caixas");

            ////define o titulo do relatorio
            IndexRegistro = 0;
            //'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.

            try
            {
                ////  'define o objeto para visualizar a impressao
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

        Decimal TotalCredito = 0;
        Decimal TotalDebito = 0;
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
                e.Graphics.DrawString("Data", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Operação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 70, 170);
                e.Graphics.DrawString("N. Documento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 140, 170);
                e.Graphics.DrawString("Tipo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 280, 170);
                e.Graphics.DrawString("Valor Crédito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
                e.Graphics.DrawString("Valor Débito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, 170);
                e.Graphics.DrawString("Centro de Custos", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, 170);

                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_CAIXAColl.Count;

                while (IndexRegistro < LIS_CAIXAColl.Count)
                {
                    if (LIS_CAIXAColl[IndexRegistro].IDCAIXA != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Convert.ToDateTime(LIS_CAIXAColl[IndexRegistro].DATAMOV).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAColl[IndexRegistro].NOMEMOVCAIXA, 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 70, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAColl[IndexRegistro].NDOCUMENTO, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 140, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAColl[IndexRegistro].NOMETIPODUPLICATA, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 280, config.PosicaoDaLinha);

                        Decimal ValorDebito = 0;
                        Decimal Credito = 0;

                        if (LIS_CAIXAColl[IndexRegistro].IDTIPOMOVCAIXA == 1)
                            Credito = Convert.ToDecimal(LIS_CAIXAColl[IndexRegistro].VALOR);
                        else
                            ValorDebito = Convert.ToDecimal(LIS_CAIXAColl[IndexRegistro].VALOR);

                        e.Graphics.DrawString(Util.LimiterText(Credito.ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(ValorDebito.ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 500, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAColl[IndexRegistro].DESCENTROCUSTO, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 600, config.PosicaoDaLinha);
                    }
                        IndexRegistro++;
                        config.LinhaAtual++;

                        if (config.LinhaAtual > config.LinhasPorPagina)
                            break;
                   
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_CAIXAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma total de Credito e Debito
                    SumCollRelatorio();
                    e.Graphics.DrawString(TotalCredito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(TotalDebito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, config.PosicaoDaLinha + 50);
                    Decimal SaldoCredito = 0;
                    SaldoCredito = TotalCredito - TotalDebito;
                    e.Graphics.DrawString("Saldo: " + SaldoCredito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 600, config.PosicaoDaLinha + 50);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_CAIXAColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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

        public void SumCollRelatorio()
        {
            TotalCredito = 0;
            TotalDebito = 0;
            foreach (LIS_CAIXAEntity item in LIS_CAIXAColl)
            {
                if (item.IDTIPOMOVCAIXA == 1 && item.IDCAIXA != null)
                    TotalCredito += Convert.ToDecimal(item.VALOR);
                else if (item.IDTIPOMOVCAIXA == 2 && item.IDCAIXA != null)
                    TotalDebito += Convert.ToDecimal(item.VALOR);
            }
          
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ImprimirListaGeral();
            }
            catch (Exception)
            {
                
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint ,
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_CAIXAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_CAIXAColl.Count.ToString();
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_CAIXAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_CAIXAColl[indice].IDCAIXA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CAIXAP.Read(CodigoSelect);

                    tabControlCaixa.SelectTab(0);
                    maskedtxtData.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CAIXAP.Delete(CodigoSelect);
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

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (LIS_CAIXAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCaixa.SelectTab(1);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Clientes");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }
        
        private void movimentaçãoDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataMovimentacao = InputBox("Data Movimentação", ConfigSistema1.Default.NomeEmpresa, DateTime.Now.ToString("dd/MM/yyyy"));

                if (!ValidacoesLibrary.ValidaTipoDateTime(DataMovimentacao))
                    MessageBox.Show("Erro na Data Inicial: " + ConfigMessage.Default.MsgDataInvalida);
                else
                {
                    RelatorioTitulo = "Movimentação do dia: " + DataMovimentacao;
                    ImprimirMovimentação();
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

        private void ImprimirMovimentação()
        {

            ////define o titulo do relatorio
            IndexRegistro = 0;
            ValorDebito = 0;
            Credito = 0;
            TotalValorDebito = 0;
            TotalValorCredito = 0;
            //'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.

            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument2;
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

        LIS_CAIXACollection LIS_CAIXAFiltroColl = new LIS_CAIXACollection();
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
                RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", "=", Util.ConverStringDateSearch(DataMovimentacao)));
                LIS_CAIXAFiltroColl = LIS_CAIXAP.ReadCollectionByParameter(RowFiltroCaixa, "DATAMOV");

                //campos a serem impressos 
                e.Graphics.DrawString("Nº Documento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Data", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
                e.Graphics.DrawString("Valor Crédito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, 170);
                e.Graphics.DrawString("Valor Débito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 320, 170);
                e.Graphics.DrawString("Tipo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 170);
                e.Graphics.DrawString("Centro de Custo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 520, 170);

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;

                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_CAIXAFiltroColl.Count;

                while (IndexRegistro < LIS_CAIXAFiltroColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAFiltroColl[IndexRegistro].NDOCUMENTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    if (LIS_CAIXAFiltroColl[IndexRegistro].IDTIPOMOVCAIXA == 1)
                    {
                        Credito = Convert.ToDecimal(LIS_CAIXAFiltroColl[IndexRegistro].VALOR);
                        TotalValorCredito += Credito;
                        ValorDebito = 0;
                    }
                    else
                    {
                        ValorDebito = Convert.ToDecimal(LIS_CAIXAFiltroColl[IndexRegistro].VALOR);
                        TotalValorDebito += ValorDebito;
                        Credito = 0;
                    }

                    e.Graphics.DrawString(Convert.ToDateTime(LIS_CAIXAFiltroColl[IndexRegistro].DATAMOV).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);

                    if (Credito > 0)
                        e.Graphics.DrawString(Credito.ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 200 + 70, config.PosicaoDaLinha + 16, stringFormat);

                    if (ValorDebito > 0)
                        e.Graphics.DrawString(ValorDebito.ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 320 + 70, config.PosicaoDaLinha +16 , stringFormat);

                    e.Graphics.DrawString(LIS_CAIXAFiltroColl[IndexRegistro].NOMETIPODUPLICATA, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 420, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_CAIXAFiltroColl[IndexRegistro].IDCENTROCUSTOS > 0 ? Util.LimiterText(LIS_CAIXAFiltroColl[IndexRegistro].CENTROCUSTO + "/" + LIS_CAIXAFiltroColl[IndexRegistro].DESCENTROCUSTO, 40) : "", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 520, config.PosicaoDaLinha);


                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_CAIXAFiltroColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (LIS_CAIXAFiltroColl.Count > 0)
                    {
                        e.Graphics.DrawString("TOTAL GERAL DE CRÉDITO: " + TotalValorCredito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 10, config.PosicaoDaLinha + 30);
                        e.Graphics.DrawString("TOTAL GERAL DE DÉBITO: " + TotalValorDebito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 30);
                        decimal SaldoGeral = TotalValorCredito - TotalValorDebito;
                        e.Graphics.DrawString("SALDO GERAL " + SaldoGeral.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha + 30);

                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 100);
                        e.Graphics.DrawString("Total da pesquisa: " + LIS_CAIXAFiltroColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 100);
                    }

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

        private LIS_CAIXACollection VerifyRemove(LIS_CAIXACollection collection)
        {
            LIS_CAIXACollection result = new LIS_CAIXACollection();

            foreach (LIS_CAIXAEntity el in collection)
            {
                if (result.Find(delegate(LIS_CAIXAEntity item)
                { return (item.IDTIPODUPLICATA == el.IDTIPODUPLICATA); }) == null)
                    result.Add(el);
            }

            return result;
        }

        private LIS_CAIXACollection TipoValores(int IDTIPODUPLICATA)
        {
            LIS_CAIXACollection LIS_CAIXAColl = new LIS_CAIXACollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", "=", Util.ConverStringDateSearch(DataMovimentacao), "and"));
            RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", IDTIPODUPLICATA.ToString()));

            LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_CAIXAColl;
        }

       
        decimal TotalGeralCaixaCredito = 0;
        decimal TotalGeralCaixaDebitO = 0;
        private void SomaTotalGeralCaixa()
        {
            TotalGeralCaixaCredito = 0;
            TotalGeralCaixaDebitO = 0;
            LIS_CAIXACollection LisCaixaColl = new LIS_CAIXACollection();
            RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
            RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", "=", Util.ConverStringDateSearch(DataMovimentacao)));
            LisCaixaColl = LIS_CAIXAP.ReadCollectionByParameter(RowFiltroCaixa);

            foreach (LIS_CAIXAEntity item in LisCaixaColl)
            {
                if (item.IDTIPOMOVCAIXA == 1)
                {
                    TotalGeralCaixaCredito += Convert.ToDecimal(item.VALOR);
                }
                else
                {
                    TotalGeralCaixaDebitO += Convert.ToDecimal(item.VALOR);
                }
            }
        }

        private void movimentaçãoDoPeríodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
                    ImprimirMovimentaçãoPeriodo();
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

        private void ImprimirMovimentaçãoPeriodo()
        {

            ////define o titulo do relatorio
            IndexRegistro = 0;
            ValorDebito = 0;
            Credito = 0;
            TotalValorDebito = 0;
            TotalValorCredito = 0;

            //'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.

            try
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
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }


        private void SomaTotalGeralCaixaPeriodo()
        {
            TotalGeralCaixaCredito = 0;
            TotalGeralCaixaDebitO = 0;
            LIS_CAIXACollection LisCaixaColl = new LIS_CAIXACollection();
            RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
            RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
            RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));
            LisCaixaColl = LIS_CAIXAP.ReadCollectionByParameter(RowFiltroCaixa);

            foreach (LIS_CAIXAEntity item in LisCaixaColl)
            {
                if (item.IDTIPOMOVCAIXA == 1)
                {
                    TotalGeralCaixaCredito += Convert.ToDecimal(item.VALOR);
                }
                else
                {
                    TotalGeralCaixaDebitO += Convert.ToDecimal(item.VALOR);
                }
            }
        }

        private LIS_CAIXACollection TipoValoresPeriodo(int IDTIPODUPLICATA)
        {
            LIS_CAIXACollection LIS_CAIXAColl = new LIS_CAIXACollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal), "and"));
            RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", IDTIPODUPLICATA.ToString()));

            LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio, "DATAMOV desc");

            return LIS_CAIXAColl;
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

                RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
                RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
                RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));
                LIS_CAIXAFiltroColl = LIS_CAIXAP.ReadCollectionByParameter(RowFiltroCaixa, "DATAMOV");


                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;

                //campos a serem impressos 
                e.Graphics.DrawString("Nº Documento", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Data", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
                e.Graphics.DrawString("Valor Crédito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, 170);
                e.Graphics.DrawString("Valor Débito", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 320, 170);
                e.Graphics.DrawString("Tipo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 420, 170);
                e.Graphics.DrawString("Centro de Custo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 520, 170);
                
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);


                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_CAIXAFiltroColl.Count;
          
                while (IndexRegistro < LIS_CAIXAFiltroColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    e.Graphics.DrawString(Util.LimiterText(LIS_CAIXAFiltroColl[IndexRegistro].NDOCUMENTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    if (LIS_CAIXAFiltroColl[IndexRegistro].IDTIPOMOVCAIXA == 1)
                    {
                        Credito = Convert.ToDecimal(LIS_CAIXAFiltroColl[IndexRegistro].VALOR);
                        TotalValorCredito += Credito;
                        ValorDebito = 0;
                    }
                    else
                    {
                        ValorDebito = Convert.ToDecimal(LIS_CAIXAFiltroColl[IndexRegistro].VALOR);
                        TotalValorDebito += ValorDebito;
                        Credito = 0;
                    }

                    e.Graphics.DrawString(Convert.ToDateTime(LIS_CAIXAFiltroColl[IndexRegistro].DATAMOV).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);

                    if (Credito > 0)
                        e.Graphics.DrawString(Credito.ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 200 + 70, config.PosicaoDaLinha + 16, stringFormat);

                    if (ValorDebito > 0)
                        e.Graphics.DrawString(ValorDebito.ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 320 + 70, config.PosicaoDaLinha + 16, stringFormat);

                    e.Graphics.DrawString(LIS_CAIXAFiltroColl[IndexRegistro].NOMETIPODUPLICATA, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 420, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_CAIXAFiltroColl[IndexRegistro].IDCENTROCUSTOS > 0 ? Util.LimiterText(LIS_CAIXAFiltroColl[IndexRegistro].CENTROCUSTO + "/" + LIS_CAIXAFiltroColl[IndexRegistro].DESCENTROCUSTO, 40): "", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 520, config.PosicaoDaLinha);


                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_CAIXAFiltroColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (LIS_CAIXAFiltroColl.Count > 0)
                    {
                        e.Graphics.DrawString("TOTAL GERAL DE CRÉDITO: " + TotalValorCredito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 10, config.PosicaoDaLinha + 30);
                        e.Graphics.DrawString("TOTAL GERAL DE DÉBITO: " + TotalValorDebito.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 30);
                        decimal SaldoGeral = TotalValorCredito - TotalValorDebito;
                        e.Graphics.DrawString("SALDO GERAL " + SaldoGeral.ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha + 30);

                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 100);
                        e.Graphics.DrawString("Total da pesquisa: " + LIS_CAIXAFiltroColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 100);
                    }

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

            ValorDebito = 0;
            Credito = 0;
            TotalValorDebito = 0;
            TotalValorCredito = 0;


                   }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;

            ValorDebito = 0;
            Credito = 0;
            TotalValorDebito = 0;
            TotalValorCredito = 0;
        }
      

        private void ImprimirMovimentaçãoPeriodoGrupo()
        {

            ////define o titulo do relatorio
            IndexRegistro = 0;
            ValorDebito = 0;
            Credito = 0;
            TotalValorDebito = 0;
            TotalValorCredito = 0;

            //'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.

            try
            {
                //////  'define o objeto para visualizar a impressao
                //PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                //printDialog1.Document = printDocument4;
                //if (printDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    objPrintPreview.Text = RelatorioTitulo;
                //    objPrintPreview.Document = printDocument4;
                //    objPrintPreview.WindowState = FormWindowState.Maximized;
                //    objPrintPreview.PrintPreviewControl.Zoom = 1;
                //    objPrintPreview.ShowDialog();
                //}

             
                
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void períodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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

                    RowsFiltroCollection RowFiltroCaixa = new RowsFiltroCollection();
                    RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", Util.ConverStringDateSearch(DataMovimentacaoInicial), "and"));
                    RowFiltroCaixa.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", Util.ConverStringDateSearch(DataMovimentacaoFinal)));

                    LIS_CAIXA2Provider LIS_CAIXA2P = new LIS_CAIXA2Provider();

                    LIS_CAIXA2Collection LIS_CAIXA2Coll = new LIS_CAIXA2Collection();
                    LIS_CAIXA2Coll = LIS_CAIXA2P.ReadCollectionByParameter(RowFiltroCaixa, "DATAMOV");


                    FrmGrupoCaixa FrmGrupoC = new FrmGrupoCaixa();
                    foreach (LIS_CAIXA2Entity item in LIS_CAIXA2Coll)
                    {
                        if (item.IDCAIXA != null)
                        {

                            LIS_CAIXA2Entity LIS_CAIXA2tY = new LIS_CAIXA2Entity();
                            LIS_CAIXA2tY.IDCAIXA = item.IDCAIXA;
                            LIS_CAIXA2tY.IDTIPOMOVCAIXA = item.IDTIPOMOVCAIXA;
                            LIS_CAIXA2tY.NDOCUMENTO = item.NDOCUMENTO;
                            LIS_CAIXA2tY.IDTIPODUPLICATA = item.IDTIPODUPLICATA;
                            LIS_CAIXA2tY.IDCENTROCUSTOS = item.IDCENTROCUSTOS;
                            LIS_CAIXA2tY.VALOR = item.VALOR;
                            LIS_CAIXA2tY.OBSERVACAO = item.OBSERVACAO;
                            LIS_CAIXA2tY.NOMEMOVCAIXA = item.NOMEMOVCAIXA;
                            LIS_CAIXA2tY.NOMETIPODUPLICATA = item.NOMETIPODUPLICATA;
                            LIS_CAIXA2tY.CENTROCUSTO = item.CENTROCUSTO;
                            LIS_CAIXA2tY.DESCENTROCUSTO = item.DESCENTROCUSTO;
                            LIS_CAIXA2tY.DATAMOV = item.DATAMOV;
                            LIS_CAIXA2tY.VALORCREDITO = LIS_CAIXA2tY.NOMEMOVCAIXA == "DÉBITO" ? 0 : item.VALOR;
                            LIS_CAIXA2tY.VALORDEBITO = LIS_CAIXA2tY.NOMEMOVCAIXA == "DÉBITO" ? item.VALOR : 0;
                            FrmGrupoC.LIS_CAIXAFiltroPrintColl.Add(LIS_CAIXA2tY);
                        }
                    }

                    FrmGrupoC.ShowDialog();
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

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_CAIXAColl.Count == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCaixa.SelectTab(1);
            }
            else
            {
                      FrmGrupoCaixa FrmGrupoC = new FrmGrupoCaixa();
                      foreach (LIS_CAIXAEntity item in LIS_CAIXAColl)
                      {
                          if(item.IDCAIXA != null)
                          {
                                LIS_CAIXA2Entity LIS_CAIXA2tY = new LIS_CAIXA2Entity();
                                LIS_CAIXA2tY.IDCAIXA = item.IDCAIXA;
                                LIS_CAIXA2tY.IDTIPOMOVCAIXA = item.IDTIPOMOVCAIXA;
                                LIS_CAIXA2tY.NDOCUMENTO = item.NDOCUMENTO;
                                LIS_CAIXA2tY.IDTIPODUPLICATA = item.IDTIPODUPLICATA;
                                LIS_CAIXA2tY.IDCENTROCUSTOS = item.IDCENTROCUSTOS;
                                LIS_CAIXA2tY.VALOR = item.VALOR;
                                LIS_CAIXA2tY.OBSERVACAO = item.OBSERVACAO;
                                LIS_CAIXA2tY.NOMEMOVCAIXA = item.NOMEMOVCAIXA;
                                LIS_CAIXA2tY.NOMETIPODUPLICATA = item.NOMETIPODUPLICATA;
                                LIS_CAIXA2tY.CENTROCUSTO = item.CENTROCUSTO;
                                LIS_CAIXA2tY.DESCENTROCUSTO = item.DESCENTROCUSTO;
                                LIS_CAIXA2tY.DATAMOV = item.DATAMOV;
                                LIS_CAIXA2tY.VALORCREDITO = LIS_CAIXA2tY.NOMEMOVCAIXA == "DÉBITO" ? 0 : item.VALOR;
                                LIS_CAIXA2tY.VALORDEBITO = LIS_CAIXA2tY.NOMEMOVCAIXA == "DÉBITO" ? item.VALOR : 0;
                                FrmGrupoC.LIS_CAIXAFiltroPrintColl.Add(LIS_CAIXA2tY);
                          }
                        }

                        FrmGrupoC.ShowDialog();
                    }
            }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 3 && e.Value.Equals("CRÉDITO"))
            {
                dsGrdCaixaDia.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }

            if (e.Value != null && e.ColumnIndex == 3 && e.Value.Equals("DÉBITO"))
            {
                dsGrdCaixaDia.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1 && e.Value.Equals("CRÉDITO"))
            {
                DataGriewDados.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }

            if (e.Value != null && e.ColumnIndex == 1 && e.Value.Equals("DÉBITO"))
            {
                DataGriewDados.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void btnAvanca_Click(object sender, EventArgs e)
        {
            
        }

        private void btnVolta_Click(object sender, EventArgs e)
        {
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            lblDataAtual.Text = Convert.ToDateTime(lblDataAtual.Text).AddDays(1).ToString("dd/MM/yyyy");
            FiltraCaixaDia();
            lblDataAtual.Text = Convert.ToDateTime(lblDataAtual.Text).ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblDataAtual.Text = Convert.ToDateTime(lblDataAtual.Text).AddDays(-1).ToString("dd/MM/yyyy");
            FiltraCaixaDia();
            lblDataAtual.Text = Convert.ToDateTime(lblDataAtual.Text).ToLongDateString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Caixa - " + txtDebitos.Text + " - Crédito do dia:" + txtCreditos.Text + " - Saldo do dia:" + txtSaldoDia.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dsGrdCaixaDia, RelatorioTitulo, this.Name);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Caixa";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void movimentaçãoPorCentroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMovCaixaCentroCusto frm = new FrmMovCaixaCentroCusto())
            {
                frm.ShowDialog();
            }
        }

        
    }
}
