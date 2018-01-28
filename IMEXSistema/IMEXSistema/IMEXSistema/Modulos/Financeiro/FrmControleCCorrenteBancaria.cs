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
using VVX;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmControleCCorrenteBancaria : Form
    {
        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
        LIS_MOVCONTACORRENTEProvider LIS_MOVCONTACORRENTEP = new LIS_MOVCONTACORRENTEProvider();

        LIS_MOVCONTACORRENTECollection LIS_MOVCONTACORRENTEColl = new LIS_MOVCONTACORRENTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        int _IDMOVCTCORRENTE = -1;        
        public MOVCONTACORRENTEEntity Entity
        {
            get
            {
                string NUMMOVIMENTACAOM = txtNumMovimentacao.Text;
                int IDCONTACORRENTE = Convert.ToInt32(cbContaCorrente.SelectedValue);
                int IDMOVIMENTACAO = Convert.ToInt32(cbNomeMovimentacao.SelectedValue);
                int IDTIPOMOVCAIXA = Convert.ToInt32(cbCrediDebito.SelectedValue);
                decimal VALOR = IDTIPOMOVCAIXA == 2 ? (Convert.ToDecimal(txtValor.Text) * -1) : Convert.ToDecimal(txtValor.Text);

                DateTime DATAMOVIMENTACAO = Convert.ToDateTime(msktDataEmissao.Text);
               
                string OBSERVACAO = txtObservacao.Text;

                return new MOVCONTACORRENTEEntity(_IDMOVCTCORRENTE, NUMMOVIMENTACAOM, IDCONTACORRENTE, IDMOVIMENTACAO,
                                                   IDTIPOMOVCAIXA, OBSERVACAO,VALOR, DATAMOVIMENTACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDMOVCTCORRENTE = value.IDMOVCTCORRENTE;
                    txtNumMovimentacao.Text = value.NUMMOVIMENTACAO;
                    cbContaCorrente.SelectedValue = value.IDCONTACORRENTE;
                    cbNomeMovimentacao.SelectedValue = value.IDMOVIMENTACAO;
                    txtValor.Text = Convert.ToDecimal(value.VALOR).ToString("n2");
                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAMOVIMENTACAO).ToString("dd/MM/yyyy");
                    cbCrediDebito.SelectedValue  = value.IDTIPOMOVCAIXA;
                    txtObservacao.Text = value.OBSERVACAO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDMOVCTCORRENTE = -1;
                    txtNumMovimentacao.Text = string.Empty;
                    cbContaCorrente.SelectedIndex = 0;
                    cbNomeMovimentacao.SelectedIndex = 0;
                    txtValor.Text = "0,00";
                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbCrediDebito.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                }
            }
        }

        public FrmControleCCorrenteBancaria()
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
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
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
                   
                    _IDMOVCTCORRENTE = MOVCONTACORRENTEP.Save(Entity);
                    SalvaSaldo();

                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void SalvaSaldo()
        {
            try
            {
                CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
                CONTACORRENTEEntity CONTACORRENTETy = new CONTACORRENTEEntity();
                CONTACORRENTETy = CONTACORRENTEP.Read(Convert.ToInt32(cbContaCorrente.SelectedValue));

                //decimal VALOR = Convert.ToInt32(cbCrediDebito.SelectedValue) == 2 ? (Convert.ToDecimal(txtValor.Text) * -1) : Convert.ToDecimal(txtValor.Text);

                //if (Tipo == 0) //0 Soma // 1 Subtrai
                //    CONTACORRENTETy.SALDO = CONTACORRENTETy.SALDO + Convert.ToDecimal(VALOR);
                //else if (Tipo == 1)
                //    CONTACORRENTETy.SALDO = CONTACORRENTETy.SALDO - Convert.ToDecimal(VALOR);

                CONTACORRENTETy.SALDO = SaldoExtrato();

                CONTACORRENTEP.Save(CONTACORRENTETy);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal SaldoExtrato()
        {
            decimal result = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCONTACORRENTE", "System.Int32", "=", (cbContaCorrente.SelectedValue).ToString()));

            LIS_MOVCONTACORRENTECollection LIS_MOVCONTACORRENTE2Coll = new LIS_MOVCONTACORRENTECollection();
            LIS_MOVCONTACORRENTE2Coll = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO desc");

            foreach (LIS_MOVCONTACORRENTEEntity item in LIS_MOVCONTACORRENTE2Coll)
            {
                if (item.IDTIPOMOVCAIXA == 2)
                    result -= Convert.ToDecimal(item.VALOR * -1);
                else
                    result += Convert.ToDecimal(item.VALOR);
            }

            return result;
        }     

   
        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (Convert.ToInt32(cbContaCorrente.SelectedValue) < 0)
            {
                errorProvider1.SetError(cbContaCorrente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbNomeMovimentacao.SelectedValue) < 0)
            {
                errorProvider1.SetError(cbNomeMovimentacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
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
             GetDropContaCorrente();
            GetDropMoviConta();
            GetDropTipo();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();

            btnCadContCorrent.Image = Util.GetAddressImage(6);
            btnNomeMoviment.Image = Util.GetAddressImage(6);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            this.Cursor = Cursors.Default;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);


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

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNumMovimentacao.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNumMovimentacao.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDMOVCTCORRENTE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
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
                        MOVCONTACORRENTEP.Delete(_IDMOVCTCORRENTE);
                        SalvaSaldo();
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
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

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( LIS_MOVCONTACORRENTEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_MOVCONTACORRENTEColl[rowindex].IDMOVCTCORRENTE);
                    Entity = MOVCONTACORRENTEP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNumMovimentacao.Focus();
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
            ImprimirListaGeral();
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
   
        private void ImprimirListaGeral()
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
      
        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            if (LIS_MOVCONTACORRENTEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_MOVCONTACORRENTEColl[indice].IDMOVCTCORRENTE);

                if (e.KeyCode == Keys.Enter)
                {
                    //Entity = MOVCONTACORRENTEP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNumMovimentacao.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            MOVCONTACORRENTEP.Delete(CodigoSelect);
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

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void btnCadContCorrent_Click(object sender, EventArgs e)
        {
            using (FrmContaBancaria frm = new FrmContaBancaria())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbContaCorrente.SelectedValue);
                GetDropContaCorrente();
                cbContaCorrente.SelectedValue = CodSelec;
            }
        }

        private void GetDropContaCorrente()
        {
            CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
            CONTACORRENTECollection CONTACORRENTEColl = new CONTACORRENTECollection();
            CONTACORRENTEColl = CONTACORRENTEP.ReadCollectionByParameter(null, "NOMECONTA");

            cbContaCorrente.DisplayMember = "NOMECONTA";
            cbContaCorrente.ValueMember = "IDCONTACORRENTE";

            CONTACORRENTEEntity CONTACORRENTETy = new CONTACORRENTEEntity();
            CONTACORRENTETy.NOMECONTA = ConfigMessage.Default.MsgDrop;
            CONTACORRENTETy.IDCONTACORRENTE = -1;
            CONTACORRENTEColl.Add(CONTACORRENTETy);

            Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity>(cbContaCorrente.DisplayMember);

            CONTACORRENTEColl.Sort(comparer.Comparer);
            cbContaCorrente.DataSource = CONTACORRENTEColl;

            cbContaCorrente.SelectedIndex = 0;
        }

        private void GetDropTipo()
        {
            try
            {
                TIPOMOVCAIXAProvider TIPOMOVCAIXAP = new TIPOMOVCAIXAProvider();
                TIPOMOVCAIXACollection TIPOMOVCAIXAColl = new TIPOMOVCAIXACollection();
                TIPOMOVCAIXAColl = TIPOMOVCAIXAP.ReadCollectionByParameter(null, "NOME");

                cbCrediDebito.DisplayMember = "NOME";
                cbCrediDebito.ValueMember = "IDTIPOMOVCAIXA";

                cbCrediDebito.DataSource = TIPOMOVCAIXAColl;
            }
            catch (Exception ex)
            {
               MessageBox.Show("Erro técnico: " + ex.Message);
            }

          //  cbCrediDebito.SelectedIndex = 0;
        }

        private void btnNomeMoviment_Click(object sender, EventArgs e)
        {
            using (FrmMovimentacaoConta frm = new FrmMovimentacaoConta())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbNomeMovimentacao.SelectedValue);
                GetDropMoviConta();
                cbNomeMovimentacao.SelectedValue = CodSelec;
            }
        }

        private void GetDropMoviConta()
        {
            MOVIMENTACAOCONTAProvider MOVIMENTACAOCONTAP = new MOVIMENTACAOCONTAProvider();
            MOVIMENTACAOCONTACollection MOVIMENTACAOCONTAColl = new MOVIMENTACAOCONTACollection();
            MOVIMENTACAOCONTAColl = MOVIMENTACAOCONTAP.ReadCollectionByParameter(null, "NOMEMOVIMENTACAO");

            cbNomeMovimentacao.DisplayMember = "NOMEMOVIMENTACAO";
            cbNomeMovimentacao.ValueMember = "IDMOVICONTA";

            MOVIMENTACAOCONTAEntity MOVIMENTACAOCONTATy = new MOVIMENTACAOCONTAEntity();
            MOVIMENTACAOCONTATy.NOMEMOVIMENTACAO = ConfigMessage.Default.MsgDrop;
            MOVIMENTACAOCONTATy.IDMOVICONTA = -1;
            MOVIMENTACAOCONTAColl.Add(MOVIMENTACAOCONTATy);

            Phydeaux.Utilities.DynamicComparer<MOVIMENTACAOCONTAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MOVIMENTACAOCONTAEntity>(cbNomeMovimentacao.DisplayMember);

            MOVIMENTACAOCONTAColl.Sort(comparer.Comparer);
            cbNomeMovimentacao.DataSource = MOVIMENTACAOCONTAColl;

            cbNomeMovimentacao.SelectedIndex = 0;
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

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_MOVCONTACORRENTEColl = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(Filtro, "DATAMOVIMENTACAO desc");
                lblTotalPesquisa.Text = LIS_MOVCONTACORRENTEColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_MOVCONTACORRENTEEntity LIS_MOVCONTACORRENTETy = new LIS_MOVCONTACORRENTEEntity();
                LIS_MOVCONTACORRENTETy.VALOR = SumTotalPesquisa("VALOR");
                LIS_MOVCONTACORRENTEColl.Add(LIS_MOVCONTACORRENTETy);              
                

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MOVCONTACORRENTEColl;               
            }
            else
                PesquisaFiltro();
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_MOVCONTACORRENTEEntity item in LIS_MOVCONTACORRENTEColl)
            {
                if (NomeCampo == "VALOR")
                {
                    if (item.IDTIPOMOVCAIXA == 2) //2 Debito
                        valortotal -= (Convert.ToDecimal(item.VALOR) * -1);
                    else
                        valortotal += Convert.ToDecimal(item.VALOR);
                        
                }
            }

            return valortotal;
        }

       
        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_MOVCONTACORRENTEColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_MOVCONTACORRENTEColl.Count.ToString();
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlMarca.SelectedIndex == 2)
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
            if (LIS_MOVCONTACORRENTEColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MOVCONTACORRENTEColl;
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
                    filtroProfile = new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_MOVCONTACORRENTEColl = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(Filtro, "DATAMOVIMENTACAO desc");
                lblTotalPesquisa.Text = LIS_MOVCONTACORRENTEColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_MOVCONTACORRENTEEntity LIS_MOVCONTACORRENTETy = new LIS_MOVCONTACORRENTEEntity();
                LIS_MOVCONTACORRENTETy.VALOR = SumTotalPesquisa("VALOR");
                LIS_MOVCONTACORRENTEColl.Add(LIS_MOVCONTACORRENTETy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MOVCONTACORRENTEColl;

                
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void extratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExtratoContaBancaria FrmExtr = new FrmExtratoContaBancaria();
            FrmExtr.ShowDialog();
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
                frm.TituloSelec = "Relação de Controle Bancaria";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }


       
    }
}
