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
    
    public partial class FrmFornecedor : Form
    {
        Utility Util = new Utility();
        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FORNECEDORCollection FornecedorColl = new FORNECEDORCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodFornecedorSelec = -1;

        public FrmFornecedor()
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
                string NOME = txtNome.Text;
                string NOMEFANTASIA = txtNomeFantasia.Text;

                string DATACADASTRO = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtDtaCadastro.Text != string.Empty)
                    DATACADASTRO = txtDtaCadastro.Text;  

                string TELEFONE1 = txtTelefone1.Text;
                string TELEFONE2 = txtTelefone2.Text;
                string CNPJ = txtCNPJ_CPF.Text;
                string IE = txtIE.Text;
                string ENDERECO = txtEndereco.Text;
                string BAIRRRO = txtBairro.Text;
                string CIDADE = txtCidade.Text;
                string UF = cbUF.Text;
                string CEP = mktxtCep.Text;
                string EMAILFORNECEDOR = txtEmail.Text;
                string OBSERVACAO = txtObservacao.Text;

                int? IDTRANSPORTADORA = null;

                if (cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTADORA = TRANSPORTADORAColl[cbTransportadora.SelectedIndex -1 ].IDTRANSPORTADORA;

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
                    txtNome.Text= value.NOME;
                    txtNomeFantasia.Text = value.NOMEFANTASIA;
                    txtDtaCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");
                    txtTelefone1.Text =value.TELEFONE1;
                    txtTelefone2.Text =value.TELEFONE2;
                    txtCNPJ_CPF.Text = value.CNPJ;
                    txtIE.Text = value.IE;
                    txtEndereco.Text = value.ENDERECO;
                    txtBairro.Text = value.BAIRRO;
                    txtCidade.Text = value.CIDADE;

                    if (value.UF != string.Empty)
                        cbUF.SelectedIndex = cbUF.FindStringExact(value.UF);
                  
                    mktxtCep.Text = value.CEP;
                    txtEmail.Text = value.EMAILFORNECEDOR;
                    txtObservacao.Text = value.OBSERVACAO;

                    if (value.IDTRANSPORTADORA != null)
                    {
                            int i=0;
                            foreach (TRANSPORTADORAEntity item in TRANSPORTADORAColl)
	                        {
                                if (item.IDTRANSPORTADORA == Convert.ToInt32(value.IDTRANSPORTADORA))
                                {
                                    cbTransportadora.SelectedIndex = i +1 ;
                                    break;
                                }
                                    i++;
	                        }
                    }
                 
                    
                    txtContatoFornecedor.Text= value.CONTATOTRANPORTADORA;
                    txtEmailContatoFornecedor.Text = value.EMAILTRANSPORTADORA;
                    txtNumero.Text= value.NUMERO;
                    errorProvider1.Clear();
                }
                else
                {

                    _IDFORNECEDOR = -1;
                    txtNome.Text =string.Empty;
                    txtNomeFantasia.Text = string.Empty;
                    txtDtaCadastro.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    txtTelefone2.Text = string.Empty;
                    txtCNPJ_CPF.Text = string.Empty;
                    txtIE.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect);
                    mktxtCep.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    cbTransportadora.SelectedIndex = 0;
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
            GetUFDrop();
            GetUFTransportadora();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            bntCadTransportadora.Image = Util.GetAddressImage(6);
            lblObsField.ForeColor = ConfigSistema1.Default.ColorFieldObs;
            if (_IDFORNECEDOR != -1)
                Entity = FORNECEDORP.Read(_IDFORNECEDOR);

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

        private void GetUFDrop()
        {
            LIS_ESTADOProvider LIS_ESTADOP = new LIS_ESTADOProvider();

            cbUF.DisplayMember = "UF";
            cbUF.ValueMember = "CODIGO_UF_IBGE";
            cbUF.DataSource = LIS_ESTADOP.ReadCollectionByParameter(null, "UF");
            
            cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect); 
        }

        private void GetUFTransportadora()
        {
            TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
            TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

            cbTransportadora.Items.Clear();
            cbTransportadora.Items.Insert(0, ConfigMessage.Default.MsgDrop);

            cbTransportadora.DisplayMember = "NOME";
            cbTransportadora.ValueMember = "IDTRANSPORTADORA";
            
            foreach (TRANSPORTADORAEntity item in TRANSPORTADORAColl)
            {
                cbTransportadora.Items.Add(item);               
            }           
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
           
            Grava();
            
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
            if (Util.RetiraLetras(txtCNPJ_CPF.Text).Length > 11 && !ValidacoesLibrary.ValidaCNPJ(txtCNPJ_CPF.Text))
            {
                errorProvider1.SetError(label30, ConfigMessage.Default.CNPJErro);
                Util.ExibirMSg(ConfigMessage.Default.CNPJErro, "Red");
                txtCNPJ_CPF.Focus();
                result = false;
            }
            if (Util.RetiraLetras(txtCNPJ_CPF.Text).Length > 0 && Util.RetiraLetras(txtCNPJ_CPF.Text).Length == 11 && !ValidacoesLibrary.ValidaCPF(txtCNPJ_CPF.Text))
            {
                errorProvider1.SetError(label30, ConfigMessage.Default.CPFErro);
                Util.ExibirMSg(ConfigMessage.Default.CPFErro, "Red");
                txtCNPJ_CPF.Focus();
                result = false;
            }
            else  if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
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

        private Boolean VerificaCNPJExistNew(string CNPJ)
        {
            Boolean result = false;

            if (CNPJ != "  .   .   /    -")
            {
                RowsFiltro FiltroProfileCNPJ = new RowsFiltro("CNPJ", "System.String", "=", CNPJ);
                RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

                FORNECEDORCollection FORNECEDORColCNPJ = new FORNECEDORCollection();
                FiltroCNPJ.Insert(0, FiltroProfileCNPJ);
                FORNECEDORColCNPJ = FORNECEDORP.ReadCollectionByParameter(FiltroCNPJ);

                if (FORNECEDORColCNPJ.Count > 0)
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
            GetUFTransportadora();
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
            e.Handled = false;
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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
		
		Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Fornecedores");
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
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("CNPJ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
                e.Graphics.DrawString("Telefone", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Cidade", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("UF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = FornecedorColl.Count;

                while (IndexRegistro < FornecedorColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(FornecedorColl[IndexRegistro].IDFORNECEDOR.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(FornecedorColl[IndexRegistro].NOME, 32), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(FornecedorColl[IndexRegistro].CNPJ, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(FornecedorColl[IndexRegistro].TELEFONE1, 12), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(FornecedorColl[IndexRegistro].CIDADE, 27), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(FornecedorColl[IndexRegistro].UF, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 730, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < FornecedorColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (FornecedorColl.Count > 0)
                    {
                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                        e.Graphics.DrawString("Total da pesquisa: " + FornecedorColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
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

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void FrmFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
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
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
                frm.TituloSelec = "Relação de Fornecedores";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEnviarEmail frm = new FrmEnviarEmail())
            {
                frm.EmailSelecionado =txtEmail.Text;
                frm.ShowDialog();
            }
        }

        private void txtCidade_Enter(object sender, EventArgs e)
        {
            using (FrmSearchCidade frm = new FrmSearchCidade())
            {
                frm.ShowDialog();

                var result = frm.Result;

                if (result > 0)
                {
                    LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                    LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                    RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                    RowRelatorioCliente.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", result.ToString()));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorioCliente);
                    txtCidade.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                    cbUF.SelectedIndex =cbUF.FindString(LIS_MUNICIPIOSColl[0].UF);
                }
            }
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
                    txtNome.Focus();

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
            lblObsField.Text = "Ctrl+E ou duplo clique para pesquisar CEP!";
        }

        private void mktxtCep_DoubleClick(object sender, EventArgs e)
        {
            using (FrmSearchEnderecoCEP frm = new FrmSearchEnderecoCEP())
            {
                frm.EnderecoSelecionado = txtEndereco.Text.Trim();
                frm.CidadSelecionado = txtCidade.Text.Trim();
                frm.UFSelecionado = cbUF.Text.Trim();
                frm.ShowDialog();
                mktxtCep.Text = frm.CEPSelecionado;
            }
        }

        private void mktxtCep_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchEnderecoCEP frm = new FrmSearchEnderecoCEP())
                {
                    frm.EnderecoSelecionado = txtEndereco.Text.Trim();
                    frm.CidadSelecionado = txtCidade.Text.Trim();
                    frm.UFSelecionado = cbUF.Text.Trim();
                    frm.ShowDialog();
                    mktxtCep.Text = frm.CEPSelecionado;
                }
            }
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
               // RowRelatorio.Add(new RowsFiltro("CNPJ", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               // RowRelatorio.Add(new RowsFiltro("TELEFONE1", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               // RowRelatorio.Add(new RowsFiltro("TELEFONE2", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               // RowRelatorio.Add(new RowsFiltro("CIDADE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

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

        private void txtCNPJ_CPF_Leave(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (Util.RetiraLetras(txtCNPJ_CPF.Text).Length > 11)
                {

                    string formatado = Convert.ToUInt64(Util.RetiraLetras(txtCNPJ_CPF.Text)).ToString(@"00\.000\.000\/0000\-00");
                    txtCNPJ_CPF.Text = formatado;

                     if(!ValidacoesLibrary.ValidaCNPJ(txtCNPJ_CPF.Text))
                     {
                         errorProvider1.SetError(label30, ConfigMessage.Default.CNPJErro);
                     }
                }
                else
                {
                    if (Util.RetiraLetras(txtCNPJ_CPF.Text).Length == 11)
                    {
                        string formatado = Convert.ToUInt64(Util.RetiraLetras(txtCNPJ_CPF.Text)).ToString(@"000\.000\.000\-00");
                        txtCNPJ_CPF.Text = formatado;

                        if (!ValidacoesLibrary.ValidaCPF(txtCNPJ_CPF.Text))
                        {
                            errorProvider1.SetError(label30, ConfigMessage.Default.CPFErro);
                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}