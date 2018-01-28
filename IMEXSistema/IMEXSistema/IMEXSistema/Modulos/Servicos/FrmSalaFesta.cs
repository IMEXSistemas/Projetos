using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BmsSoftware;
using BMSworks.Firebird;
using BMSworks.Model;
using System.IO;
using VVX;

namespace BMSSoftware.Modulos.Cadastros
{
    
    public partial class FrmSalaFesta : Form
    {
        Utility Util = new Utility();

        SALAOFESTAProvider SALAOFESTAP = new SALAOFESTAProvider();
        SALAOFESTACollection SALAOFESTAColl = new SALAOFESTACollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public FrmSalaFesta()
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


        int _IDSALAOFESTA = -1;
        public SALAOFESTAEntity Entity
        {
            get
            {
                string NOME = txtNome.Text;
                string NOMEFANTASIA = txtNomeFantasia.Text;

                string DATACADASTRO = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtDtaCadastro.Text != string.Empty)
                    DATACADASTRO = txtDtaCadastro.Text;  

                string TELEFONE1 = txtTelefone1.Text;
                string TELEFONE2 = txtTelefone2.Text;
                string CNPJ = maskedtxtCNPJ.Text;
                string IE = txtIE.Text;
                string ENDERECO = txtEndereco.Text;
                string BAIRRRO = txtBairro.Text;
                string CIDADE = txtCidade.Text;
                string UF = txtUF.Text;
                string CEP = mktxtCep.Text;
                string EMAIL = txtEmail.Text;
                string OBSERVACAO = txtObservacao.Text;              

                string CONTATO = txtContatoSalao.Text;
                string EMAILCONTATO = txtEmailContatoSalao.Text;

                return new SALAOFESTAEntity(_IDSALAOFESTA, NOME, NOMEFANTASIA, Convert.ToDateTime(DATACADASTRO),
                                            TELEFONE1, TELEFONE2, CNPJ, IE, ENDERECO, BAIRRRO,
                                            CIDADE, UF, CEP,  EMAIL, OBSERVACAO,
                                            CONTATO, EMAILCONTATO);   
                
            }
            set
            {
                if (value != null)
                {
                    _IDSALAOFESTA = value.IDSALAOFESTA;
                    txtNome.Text = value.RAZAOSOCIAL;
                    txtNomeFantasia.Text = value.NOME;
                    txtDtaCadastro.Text = Convert.ToDateTime(value.DTCADASTRO).ToString("dd/MM/yyyy");
                    txtTelefone1.Text =value.TELEFONE1;
                    txtTelefone2.Text =value.TELEFONE2;
                    maskedtxtCNPJ.Text = value.CNPJ;
                    txtIE.Text = value.IE;
                    txtEndereco.Text = value.ENDERECO;
                    txtBairro.Text = value.BAIRRO;
                    txtCidade.Text = value.CIDADE;
                    txtUF.Text = value.UF;
                    mktxtCep.Text = value.CEP;
                    txtEmail.Text = value.EMAIL;
                    txtObservacao.Text = value.OBSERVACAO;
                    txtContatoSalao.Text = value.CONTATO;
                    txtEmailContatoSalao.Text = value.EMAILCONTATO;             
                    
                    txtEmail.Text = value.EMAIL;
                    errorProvider1.Clear();
                }
                else
                {

                    _IDSALAOFESTA = -1;
                    txtNome.Text =string.Empty;
                    txtNomeFantasia.Text = string.Empty;
                    txtDtaCadastro.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    txtTelefone2.Text = string.Empty;
                    maskedtxtCNPJ.Text = string.Empty;
                    txtIE.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    txtUF.Text = string.Empty;
                    mktxtCep.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    txtContatoSalao.Text = string.Empty;
                    txtEmailContatoSalao.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }

        private void FrmFornecedor_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetToolStripButtonCadastro();
          
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            lblObsField.ForeColor = ConfigSistema1.Default.ColorFieldObs;

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
            tabControlFornecedor.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }


        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlFornecedor.SelectTab(1);
            txtCriterioPesquisa.Focus();
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
                    //Verificar CPF existe para novos cadastros
                    if (_IDSALAOFESTA == -1)
                    {
                        if (VerificaCNPJExistNew(maskedtxtCNPJ.Text))
                        {
                            DialogResult dr = MessageBox.Show(ConfigMessage.Default.CNPJDupl,
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                _IDSALAOFESTA = SALAOFESTAP.Save(Entity);
                                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            }
                        }
                        else
                        {
                            _IDSALAOFESTA = SALAOFESTAP.Save(Entity);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                    }
                    else
                    {
                        _IDSALAOFESTA = SALAOFESTAP.Save(Entity);
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
            if (maskedtxtCNPJ.Text != "  .   .   /    -")
                if (!ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CNPJErro);
                    maskedtxtCNPJ.Focus();
                    result = false;
                }


            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private Boolean VerificaCNPJExistNew(string CNPJ)
        {
            Boolean result = false;

            if (CNPJ != "  .   .   /    -")
            {
                RowsFiltro FiltroProfileCNPJ = new RowsFiltro("CNPJ", "System.String", "=", CNPJ);
                RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

                SALAOFESTACollection SALAOFESTACNPJ = new SALAOFESTACollection();
                FiltroCNPJ.Insert(0, FiltroProfileCNPJ);
                SALAOFESTACNPJ = SALAOFESTAP.ReadCollectionByParameter(FiltroCNPJ);

                if (SALAOFESTACNPJ.Count > 0)
                    result = true;
            }

            return result;
        }

        private void maskedtxtCNPJ_MouseEnter(object sender, EventArgs e)
        {

        }

        private void maskedtxtCNPJ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CNPJ do fornecedor, não será possível cadastrar CNPJ inválido.";
        }

        private void maskedtxtCNPJ_Leave(object sender, EventArgs e)
        {
            if (maskedtxtCNPJ.Text != "  .   .   /    -")
            {
                if (!ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CNPJErro);
                    maskedtxtCNPJ.Focus();
                    errorProvider1.SetError(maskedtxtCNPJ, ConfigMessage.Default.CNPJErro);
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    errorProvider1.SetError(maskedtxtCNPJ, "");
                }
            }
            else
            {
                lblObsField.Text = string.Empty;
                errorProvider1.SetError(maskedtxtCNPJ, "");
            }
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

                SALAOFESTAColl = SALAOFESTAP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = SALAOFESTAColl;

                lblTotalPesquisa.Text = SALAOFESTAColl.Count.ToString();
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
            if (SALAOFESTAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = SALAOFESTAColl;
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

                SALAOFESTAColl = SALAOFESTAP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = SALAOFESTAColl;

                lblTotalPesquisa.Text = SALAOFESTAColl.Count.ToString();
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
            SALAOFESTAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = SALAOFESTAColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SALAOFESTAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(SALAOFESTAColl[rowindex].IDSALAOFESTA);

                    Entity = SALAOFESTAP.Read(CodigoSelect);

                    tabControlFornecedor.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFornecedor.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFornecedor.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDSALAOFESTA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlFornecedor.SelectTab(1);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        SALAOFESTAP.Delete(_IDSALAOFESTA);
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

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SALAOFESTAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<SALAOFESTAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SALAOFESTAEntity>(orderBy);
                
                SALAOFESTAColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = SALAOFESTAColl;
                lblObsField.Text = string.Empty;
            }
        }

        private void cbUF_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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
		
		Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;

        int IndexRegistro = 0;


        private void FrmFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmFornecedor_KeyDown(object sender, KeyEventArgs e)
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
            if (SALAOFESTAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(SALAOFESTAColl[indice].IDSALAOFESTA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = SALAOFESTAP.Read(CodigoSelect);

                    tabControlFornecedor.SelectTab(0);
                    txtNome.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            SALAOFESTAP.Delete(CodigoSelect);
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
        
    }
}