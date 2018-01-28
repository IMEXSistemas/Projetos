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
using BmsSoftware.Modulos.Operacional;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using winfit.Modulos.Outros;
using BmsSoftware.Modulos.FrmSearch;

namespace BMSSoftware.Modulos.Cadastros
{
    
    public partial class FrmContratoCliente : Form
    {
        Utility Util = new Utility();
        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FORNECEDORCollection FornecedorColl = new FORNECEDORCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodFornecedorSelec = -1;

        public FrmContratoCliente()
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
        

        public int _IDFORNECEDOR = -1;
        public FORNECEDOREntity Entity
        {
            get
            {
                string NOME = txtNomeContrato.Text;
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
                string UF = cbUF.Text;
                string CEP = mktxtCep.Text;
                string EMAILFORNECEDOR = txtEmail.Text;
                string OBSERVACAO = "";

                int? IDTRANSPORTADORA = null;

                string CONTATOTRANSPORTADORA = txtContatoFornecedor.Text;
                string EMAILTRANSPORTADORA = txtEmailContatoFornecedor.Text;
                string NUMERO = txtNumero.Text;

                return new FORNECEDOREntity(_IDFORNECEDOR, NOME, NOMEFANTASIA, Convert.ToDateTime(DATACADASTRO),
                                            TELEFONE1, TELEFONE2, CNPJ, IE, ENDERECO, BAIRRRO,
                                            CIDADE, UF, CEP, EMAILFORNECEDOR, OBSERVACAO, IDTRANSPORTADORA,
                                            CONTATOTRANSPORTADORA, EMAILTRANSPORTADORA, NUMERO);
            }
            set
            {
                if (value != null)
                {
                    _IDFORNECEDOR = value.IDFORNECEDOR;
                    //txtNome.Text= value.NOME;
                    txtNomeFantasia.Text = value.NOMEFANTASIA;
                    txtDtaCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");
                    txtTelefone1.Text =value.TELEFONE1;
                    txtTelefone2.Text =value.TELEFONE2;
                    maskedtxtCNPJ.Text = value.CNPJ;
                    txtIE.Text = value.IE;
                    txtEndereco.Text = value.ENDERECO;
                    txtBairro.Text = value.BAIRRO;
                    txtCidade.Text = value.CIDADE;

                    if (value.UF != string.Empty)
                        cbUF.SelectedIndex = cbUF.FindStringExact(value.UF);
                  
                    mktxtCep.Text = value.CEP;
                    txtEmail.Text = value.EMAILFORNECEDOR;
                 //   txtObservacao.Text = value.OBSERVACAO;                 
                 
                    
                    txtContatoFornecedor.Text= value.CONTATOTRANPORTADORA;
                    txtEmailContatoFornecedor.Text = value.EMAILTRANSPORTADORA;
                    txtNumero.Text= value.NUMERO;
                    errorProvider1.Clear();
                }
                else
                {

                    _IDFORNECEDOR = -1;
                //    txtNome.Text =string.Empty;
                    txtNomeFantasia.Text = string.Empty;
                    txtDtaCadastro.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    txtTelefone2.Text = string.Empty;
                    maskedtxtCNPJ.Text = string.Empty;
                    txtIE.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect);
                    mktxtCep.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                  //  txtObservacao.Text = string.Empty;
                  //  cbTransportadora.SelectedIndex = 0;
                    txtContatoFornecedor.Text = string.Empty;
                    txtEmailContatoFornecedor.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                    errorProvider1.Clear();
                }


            }
        }

        private void FrmFornecedor_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetToolStripButtonCadastro();           
            
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            
            lblObsField.ForeColor = ConfigSistema1.Default.ColorFieldObs;
           

            this.Cursor = Cursors.Default;

            VerificaAcesso();
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
      

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);
            btnAdd.Image = Util.GetAddressImage(15);

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
             btnSeach.Image = Util.GetAddressImage(20);
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

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            PrintListaGeral();
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
                    _IDFORNECEDOR = FORNECEDORP.Save(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    btnPesquisa_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (maskedtxtCNPJ.Text != "  .   .   /    -" && !ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
            {
                Util.ExibirMSg(ConfigMessage.Default.CNPJErro, "Red");
                maskedtxtCNPJ.Focus();
                result = false;
            }
            else  if (txtNomeContrato.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNomeContrato, ConfigMessage.Default.CampoObrigatorio);
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

        

        private void maskedtxtCNPJ_MouseEnter(object sender, EventArgs e)
        {

        }

        private void maskedtxtCNPJ_Enter(object sender, EventArgs e)
        {
            
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

        private void bntCadTransportadora_Click(object sender, EventArgs e)
        {
            using (FrmTransportadora frm = new FrmTransportadora())
            {
                frm.ShowDialog();
            }
        }

        private void cbTransportadora_Enter(object sender, EventArgs e)
        {
          
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

                FornecedorColl = FORNECEDORP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = FornecedorColl;

                lblTotalPesquisa.Text = FornecedorColl.Count.ToString();
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
            try
            {
                // Nome campo que sera filtrado
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (FornecedorColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = FornecedorColl;
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

                    FornecedorColl = FORNECEDORP.ReadCollectionByParameter(Filtro, "NOME");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = FornecedorColl;

                    lblTotalPesquisa.Text = FornecedorColl.Count.ToString();
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

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            FornecedorColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = FornecedorColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFornecedor.SelectTab(0);
            txtNomeContrato.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFornecedor.SelectTab(0);
            txtNomeContrato.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDFORNECEDOR == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlFornecedor.SelectTab(1);
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
                        FORNECEDORP.Delete(_IDFORNECEDOR);
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

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (FornecedorColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(orderBy);

                    FornecedorColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = FornecedorColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void cbUF_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintListaGeral();
        }        

        private void PrintListaGeral()
        {
            if (FornecedorColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlFornecedor.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirListaGeral();
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
		
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
        }

   
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void FrmFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void FrmFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
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
            if (FornecedorColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(FornecedorColl[indice].IDFORNECEDOR);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = FORNECEDORP.Read(CodigoSelect);

                    tabControlFornecedor.SelectTab(0);
                    txtNomeContrato.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            FORNECEDORP.Delete(CodigoSelect);
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
            if (FornecedorColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlFornecedor.SelectTab(1);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Contrato Cliente");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Contrato Cliente");

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
                frm.TituloSelec = "Relação de Contrato Cliente";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void txtCidade_Enter(object sender, EventArgs e)
        {
           
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (FornecedorColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(FornecedorColl[rowindex].IDFORNECEDOR);

                    Entity = FORNECEDORP.Read(CodigoSelect);

                    tabControlFornecedor.SelectTab(0);
                    txtNomeContrato.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {
                             DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + FornecedorColl[rowindex].NOME,
                                      ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                try
                                {
                                    CodigoSelect = Convert.ToInt32(FornecedorColl[rowindex].IDFORNECEDOR);
                                    //Delete Pedido
                                    FORNECEDORP.Delete(CodigoSelect);

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
        }

        private void mktxtCep_Enter(object sender, EventArgs e)
        {
            
        }

        private void mktxtCep_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void mktxtCep_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
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
                RowRelatorio.Add(new RowsFiltro("NOME", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFANTASIA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
   
                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    FornecedorColl = FORNECEDORP.ReadCollectionByParameter(RowRelatorio, "NOME");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = FornecedorColl;

                    lblTotalPesquisa.Text = FornecedorColl.Count.ToString();
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

        private void txtPesquisaRapida_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }
    }
}