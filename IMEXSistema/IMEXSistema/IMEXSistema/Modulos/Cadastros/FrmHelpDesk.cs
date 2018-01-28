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
using BMSworks.UI;
using System.IO;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmHelpDesk : Form
    {
        HELPDESKProvider HELPDESKP = new HELPDESKProvider();

        LIS_HELPDESKCollection LIS_HELPDESKColl = new LIS_HELPDESKCollection();
        LIS_HELPDESKProvider LIS_HELPDESKP = new LIS_HELPDESKProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection(); 
        Utility Util = new Utility();

        public FrmHelpDesk()
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
            lblobsfield.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDHELPDESK = -1;
        public HELPDESKEntity Entity
        {
            get
            {
                DateTime DATAINCLUSAO = Convert.ToDateTime(txtDataInc.Text);
                DateTime DATASOLUCAO = Convert.ToDateTime(maskDtaPrev.Text);
                int IDTIPOSOLICITANTE = Convert.ToInt32(cbTipoSolicitante.SelectedValue.ToString());
                int IDDEPARTAMENTO = Convert.ToInt32(cbDepartamento.SelectedValue.ToString());
                int IDSTATUS =  Convert.ToInt32(cbStatus.SelectedValue.ToString());
                int IDPRIORIDADE = Convert.ToInt32(cbPrioridade.SelectedValue.ToString());
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue.ToString());
                string NOMESOLICITANTE = txtnomeSolicitante.Text;
                string EMAILSOLICITANTE = txtEmailSolicitante.Text;
                string TELEFONESOLICITANTE = txtTelefoneSolicitante.Text;
                string SOLICITACAO = txtSolicitacao.Text;
                string SOLUCAO = txtSolucao.Text;

                return new HELPDESKEntity(_IDHELPDESK,  DATAINCLUSAO, DATASOLUCAO, IDTIPOSOLICITANTE, 
                                          IDDEPARTAMENTO, IDSTATUS, IDPRIORIDADE, IDFUNCIONARIO, NOMESOLICITANTE,
                                          EMAILSOLICITANTE, TELEFONESOLICITANTE, SOLICITACAO, SOLUCAO);
            }
            set
            {

                if (value != null)
                {
                    _IDHELPDESK = value.IDHELPDESK;
                    txtCodAtendimento.Text = value.IDHELPDESK.ToString();
                    txtDataInc.Text = Convert.ToDateTime(value.DATAINCLUSAO).ToString("dd/MM/yyyy");
                    maskDtaPrev.Text = Convert.ToDateTime(value.DATASOLUCAO).ToString("dd/MM/yyyy");
                    cbTipoSolicitante.SelectedValue = value.IDTIPOSOLICITANTE;
                    cbDepartamento.SelectedValue = value.IDDEPARTAMENTO;
                    cbStatus.SelectedValue = value.IDSTATUS;
                    cbPrioridade.SelectedValue = value.IDPRIORIDADE;
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    txtnomeSolicitante.Text = value.NOMESOLICITANTE;
                    txtEmailSolicitante.Text = value.EMAILSOLICITANTE;
                    txtTelefoneSolicitante.Text = value.TELEFONESOLICITANTE;
                    txtSolicitacao.Text = value.SOLICITACAO;
                    txtSolucao.Text = txtSolucao.Text = value.SOLUCAO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDHELPDESK = -1;
                    txtCodAtendimento.Text = string.Empty;
                    string datacadastro = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDataInc.Text = datacadastro;
                    maskDtaPrev.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbTipoSolicitante.SelectedIndex = 0;
                    cbDepartamento.SelectedIndex = 0;
                    cbStatus.SelectedIndex = 0;
                    cbPrioridade.SelectedIndex = 0;
                    cbFuncionario.SelectedIndex = 0;
                    txtnomeSolicitante.Text = string.Empty;
                    txtEmailSolicitante.Text = string.Empty;
                    txtTelefoneSolicitante.Text = string.Empty;
                    txtSolicitacao.Text = string.Empty;
                    txtSolucao.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }

        private void FrmHelpDesk_Load(object sender, EventArgs e)
        {
             CreaterCursor Cr = new CreaterCursor();
             this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

             this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog; this.FormBorderStyle = FormBorderStyle.FixedDialog;
             string datacadastro = DateTime.Now.ToString("dd/MM/yyyy");
             txtDataInc.Text = datacadastro;
             maskDtaPrev.Text = DateTime.Now.ToString("dd/MM/yyyy");

             PreencheDropTipoPesquisa();
             GetToolStripButtonCadastro();
             PreencheDropCamposPesquisa();

             GetStatus();
             GetPrioridade();
             GetDepartamento();
             GetTipoSolicitante();
             GetFuncionario();

             bntCadSituacao.Image = Util.GetAddressImage(6);
             btnTipoSolicitante.Image = Util.GetAddressImage(6);
             btnCadDeparta.Image = Util.GetAddressImage(6);
             VerificaAcesso();

             this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

        private void GetStatus()
        {
            //6 Help Desk
            RowsFiltroCollection RowStatus = new RowsFiltroCollection();
            RowStatus.Add(new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "6"));

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(RowStatus);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

        private void GetPrioridade()
        {
            PRIORIDADEProvider PRIORIDADEP = new PRIORIDADEProvider();
            cbPrioridade.DataSource = PRIORIDADEP.ReadCollectionByParameter(null);

            cbPrioridade.DisplayMember = "NOME";
            cbPrioridade.ValueMember = "IDPRIORIDADE";
        }

        private void GetDepartamento()
        {
            DEPARTAMENTOProvider DEPARTAMENTOP = new DEPARTAMENTOProvider();
            cbDepartamento.DataSource = DEPARTAMENTOP.ReadCollectionByParameter(null, "NOMEDEPARTAMENTO");

            cbDepartamento.DisplayMember = "NOMEDEPARTAMENTO";
            cbDepartamento.ValueMember = "IDDEPARTAMENTO";
        }

        private void GetTipoSolicitante()
        {
            TIPOSOLICITANTEProvider TIPOSOLICITANTEP = new TIPOSOLICITANTEProvider();
            cbTipoSolicitante.DataSource = TIPOSOLICITANTEP.ReadCollectionByParameter(null, "NOME");

            cbTipoSolicitante.DisplayMember = "NOME";
            cbTipoSolicitante.ValueMember = "IDTIPOSOLICITANTE";
        }

         private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            cbFuncionario.DataSource = FUNCIONARIOP.ReadCollectionByParameter(null,"NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";
        }        

        private void bntCadSituacao_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
            } 
        }

        private void btnCadDeparta_Click(object sender, EventArgs e)
        {
            using (FrmDepartamento frm = new FrmDepartamento())
            {
                frm.ShowDialog();
            }
        }

        private void btnTipoSolicitante_Click(object sender, EventArgs e)
        {
            using (FrmTipoSolicitante frm = new FrmTipoSolicitante())
            {
                frm.ShowDialog();
            }
        }

        private void cbStatus_Enter(object sender, EventArgs e)
        {
            GetStatus();
        }

        private void cbDepartamento_Enter(object sender, EventArgs e)
        {
            GetDepartamento();
        }

        private void cbTipoSolicitante_Enter(object sender, EventArgs e)
        {
            GetTipoSolicitante();
        }

        private void maskDtaPrev_Validating(object sender, CancelEventArgs e)
        {
            if (maskDtaPrev.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskDtaPrev.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskDtaPrev.Focus();
                    errorProvider1.SetError(maskDtaPrev, ConfigMessage.Default.MsgDataInvalida);
                    e.Cancel = true;
                    tabControlHelpDesk.SelectTab(0);
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(maskDtaPrev, "");
                }
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            lblobsfield.Text = "Código do atendimento, digite um valor e pressione Ctrl+E para pesquisar.";
        }

        private void txtCodAtendimento_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodAtendimento.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodAtendimento.Text))
                {
                    errorProvider1.SetError(txtCodAtendimento, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodAtendimento.Focus();
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtCodAtendimento, "");
                }
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
                    if (_IDHELPDESK == -1)
                    {
                        _IDHELPDESK = HELPDESKP.Save(Entity);
                        txtCodAtendimento.Text = _IDHELPDESK.ToString();
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                    else
                    {
                        HELPDESKP.Save(Entity);
                        txtCodAtendimento.Text = _IDHELPDESK.ToString();
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        btnPesquisa_Click(null, null);
                    }
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
            if (txtnomeSolicitante.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtnomeSolicitante, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
			
            else if (txtSolicitacao.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtSolicitacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                txtSolicitacao.Focus();
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtnomeSolicitante, "");
                errorProvider1.SetError(txtSolicitacao, "");
            }

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

                LIS_HELPDESKColl = LIS_HELPDESKP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_HELPDESKColl;

                lblTotalPesquisa.Text = LIS_HELPDESKColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                errorProvider1.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            // Nome campo que sera filtrado
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_HELPDESKColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_HELPDESKColl;
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

                LIS_HELPDESKColl = LIS_HELPDESKP.ReadCollectionByParameter(Filtro);
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_HELPDESKColl;

                lblTotalPesquisa.Text = LIS_HELPDESKColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlHelpDesk.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlHelpDesk.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlHelpDesk.SelectTab(0);
            txtnomeSolicitante.Focus();
        }

        private void txtSolicitacao_Validating(object sender, CancelEventArgs e)
        {
            if (txtSolicitacao.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtSolicitacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                e.Cancel = true;
            } 
        }

        private void txtnomeSolicitante_Validating(object sender, CancelEventArgs e)
        {
            if (txtnomeSolicitante.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtnomeSolicitante, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                e.Cancel = true;
            }           
        }

        private void txtnomeSolicitante_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtnomeSolicitante, "");
        }

        private void txtSolicitacao_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtSolicitacao, "");
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_HELPDESKColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_HELPDESKColl[rowindex].IDHELPDESK);

                    Entity = HELPDESKP.Read(CodigoSelect);

                    tabControlHelpDesk.SelectTab(0);
                    txtCodAtendimento.Focus();
                }
            }
        }

        private void FrmHelpDesk_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDHELPDESK == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlHelpDesk.SelectTab(1);
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
                        HELPDESKP.Delete(_IDHELPDESK);
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

        private void txtCodAtendimento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodAtendimento.Text))
                {
                    errorProvider1.SetError(txtCodAtendimento, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodAtendimento.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtCodAtendimento, "");

                    HELPDESKEntity HELPDESKCons = new HELPDESKEntity();
                    HELPDESKCons = HELPDESKP.Read(Convert.ToInt32(txtCodAtendimento.Text));

                    if (HELPDESKCons != null)
                    {
                        Entity = HELPDESKCons;
                        MessageBox.Show(ConfigMessage.Default.MsgSearchSucess);
                    }
                    else
                        MessageBox.Show(ConfigMessage.Default.MsgSearchErro);
                }

                e.SuppressKeyPress = true;
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlHelpDesk.SelectTab(0);
            txtnomeSolicitante.Focus();
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_HELPDESKColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_HELPDESKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_HELPDESKEntity>(orderBy);

                    LIS_HELPDESKColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_HELPDESKColl;
                }
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_HELPDESKColl.Clear();
            Filtro.Clear();
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_HELPDESKColl.Count.ToString();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Atendimentos");
            ////define o titulo do relatorio
            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
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
                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Solicitante", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Inclusão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
                e.Graphics.DrawString("Prioridade", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 370, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Tipo Solicitante", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("Funcionário", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_HELPDESKColl.Count;

                while (IndexRegistro < LIS_HELPDESKColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_HELPDESKColl[IndexRegistro].IDHELPDESK.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_HELPDESKColl[IndexRegistro].NOMESOLICITANTE, 32), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDateTime(LIS_HELPDESKColl[IndexRegistro].DATAINCLUSAO).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_HELPDESKColl[IndexRegistro].NOMEPRIORIDADE.ToString(), 12), config.FonteConteudo, Brushes.Black, config.MargemEsquerda +370, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_HELPDESKColl[IndexRegistro].NOMESTATUS.ToString(), 27), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_HELPDESKColl[IndexRegistro].TIPOSOLICITANTE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_HELPDESKColl[IndexRegistro].NOMEFUNCIONARIO.ToString(),20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 650, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_HELPDESKColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (LIS_HELPDESKColl.Count > 0)
                    {
                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                        e.Graphics.DrawString("Total da pesquisa: " + LIS_HELPDESKColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
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

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void FrmHelpDesk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblobsfield.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
           lblobsfield.Text = string.Empty;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_HELPDESKColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_HELPDESKColl[indice].IDHELPDESK);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = HELPDESKP.Read(CodigoSelect);

                    tabControlHelpDesk.SelectTab(0);
                    txtnomeSolicitante.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            HELPDESKP.Delete(CodigoSelect);
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
       
    }
}
