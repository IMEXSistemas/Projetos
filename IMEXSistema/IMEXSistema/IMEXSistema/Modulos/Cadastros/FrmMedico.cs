using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using BmsSoftware;
using System.IO;
using BmsSoftware.Modulos.Cadastros;

namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmMedico : Form
    {
        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        
        Utility Util = new Utility();
        
        STATUSProvider STATUSP = new STATUSProvider();
        MEDICOProvider MEDICOP = new MEDICOProvider();
        LIS_MEDICOProvider LIS_MEDICOP = new LIS_MEDICOProvider();
        LIS_MEDICOCollection LIS_MEDICOColl = new LIS_MEDICOCollection();

        

        public FrmMedico()
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

       public int _IDMEDICO = -1;
        public MEDICOEntity Entity
        {
            get
            {
                string NOME = txtNome.Text.TrimEnd().TrimStart();

                decimal? COMISSAO = null;
                if (maskedtxtComissao.Text != string.Empty)
                    COMISSAO = Convert.ToDecimal(maskedtxtComissao.Text);
               

                DateTime? DATAADMISSAO = null;
                if (maskedtxtDataAd.Text != "  /  /")
                    DATAADMISSAO = Convert.ToDateTime(maskedtxtDataAd.Text);

                string CRM = txtCRM.Text;
                
                int CODSTATUS = Convert.ToInt32(cbStatus.SelectedValue.ToString());
                string FUNCAO = txtFuncao.Text;
                string DEPARTAMENTO = txtDepto.Text;
                string SETOR = txtSetor.Text;

                string CARTEIRATRABALHO = txtCarteiraTrabalho.Text;
                string CARTEIRAMOTORISTA = txtCarteiraMotorista.Text;
                string CPF = maskedtxtCPF.Text;
                string RG = txtRG.Text;
                string ENDERECO = txtEndereco.Text;
                string BAIRRO = txtBairro.Text;
                string CIDADE = txtCidade.Text;
                string CEP = mktxtCep.Text;
                string UF = cbUF.Text;
                string TELEFONE1 = txtTelefone1.Text;
                string TELEFONE2 = txtTelefone2.Text;
                string EMAIL = txtEmail.Text;
                string OBSERVACAO = txtObs.Text;
                
                DateTime? DTANIVERSARIO = null;
                if (maskedtxtAniv.Text != "  /  /")
                    DTANIVERSARIO = Convert.ToDateTime(maskedtxtAniv.Text);



                return new MEDICOEntity(_IDMEDICO, NOME, COMISSAO, DATAADMISSAO, CRM,
                                       CODSTATUS, FUNCAO, DEPARTAMENTO, SETOR, CARTEIRATRABALHO,
                                       CARTEIRAMOTORISTA, CPF, RG, ENDERECO, BAIRRO, CIDADE, CEP,
                                       UF, TELEFONE1,TELEFONE2, EMAIL, OBSERVACAO, DTANIVERSARIO);
            }
            set
            {

                if (value != null)
                {
                    _IDMEDICO = value.IDMEDICO;
                    txtNome.Text = value.NOME;

                    if(value.COMISSAO != null)
                        maskedtxtComissao.Text = Convert.ToDecimal(value.COMISSAO).ToString("N2");

                    if (value.DATAADMISSAO != null)
                          maskedtxtDataAd.Text = Convert.ToDateTime(value.DATAADMISSAO).ToString("dd/MM/yyyy");
                        
                    cbStatus.SelectedValue = Convert.ToInt32(value.CODSTATUS);

                    txtFuncao.Text = value.FUNCAO;
                    txtDepto.Text = value.DEPARTAMENTO;
                    txtSetor.Text = value.SETOR;
                    txtCRM.Text = value.CRM;                  

                    txtCarteiraTrabalho.Text = value.CARTEIRATRABALHO;
                    txtCarteiraMotorista.Text = value.CARTEIRAMOTORISTA;
                    
                    if(value.CPF != null)
                        maskedtxtCPF.Text = value.CPF;

                    txtRG.Text = value.RG;
                    txtEndereco.Text = value.ENDERECO;
                    txtBairro.Text = value.BAIRRO;
                    txtCidade.Text = value.CIDADE;

                    if(value.CEP != string.Empty)
                        mktxtCep.Text = value.CEP;

                    cbUF.SelectedValue = value.UF;
                    
                    txtTelefone1.Text = value.TELEFONE1;
                    txtTelefone2.Text = value.TELEFONE2;
                    txtEmail.Text = value.EMAIL;
                    txtObs.Text = value.OBSERVACAO;
                    
                    if (value.DTANIVERSARIO != null)
                        maskedtxtAniv.Text = Convert.ToDateTime(value.DTANIVERSARIO).ToString("dd/MM/yyyy");
                }
                else
                {
                    _IDMEDICO = -1;
                    txtNome.Text = string.Empty;
                    maskedtxtComissao.Text = string.Empty;
                    maskedtxtDataAd.Text = string.Empty;
                    txtCRM.Text = string.Empty;
                    cbStatus.SelectedIndex = 0;

                    txtFuncao.Text = string.Empty;
                    txtDepto.Text = string.Empty;
                    txtSetor.Text = string.Empty;
                    txtCRM.Text = string.Empty;
                    txtCarteiraTrabalho.Text = string.Empty;
                    txtCarteiraMotorista.Text = string.Empty;
                    maskedtxtCPF.Text = string.Empty;

                    txtRG.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    mktxtCep.Text = string.Empty;
                    cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect);
                    txtTelefone1.Text = string.Empty; 
                    txtTelefone2.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtObs.Text = string.Empty;
                    maskedtxtAniv.Text = "  /  /";
                    errorProvider1.Clear();
                }


            }
        }

        private void FrmFuncionario_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            GetToolStripButtonCadastro();
            GetUFDrop();
            GetStatus();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();

            bntCadClassificacao.Image = Util.GetAddressImage(6);
            lblObsField.ForeColor = ConfigSistema1.Default.ColorFieldObs;

            if (_IDMEDICO != -1)
                Entity = MEDICOP.Read(_IDMEDICO);

            this.Cursor = Cursors.Default;
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

            //3 Funcionário
            RowsFiltro FiltroProfileCNPJ = new RowsFiltro("IDGRUPOSTATUS", "System.String", "=", "3");
            RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();
            
            FiltroCNPJ.Insert(0, FiltroProfileCNPJ);

            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(FiltroCNPJ);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
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

        private void GetUFDrop()
        {
            ESTADOProvider EstadoP = new ESTADOProvider();
            RowsFiltroCollection filtrosUF = new RowsFiltroCollection();

            cbUF.DataSource = EstadoP.ReadCollectionByParameter(filtrosUF);
            cbUF.DisplayMember = "UF";
            cbUF.ValueMember = "UF";
            cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect);
            
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            PrintListaGeral();
            this.Cursor = Cursors.Default;	
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlFuncionario.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlFuncionario.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFuncionario.SelectTab(0);
            txtNome.Focus();
        }

        private void bntCadClassificacao_Click(object sender, EventArgs e)
        {
             using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
            }
        }

        private void cbStatus_Enter(object sender, EventArgs e)
        {
            GetStatus();
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
                    if (_IDMEDICO  == -1)
                    {
                        if (VerificaCPFExistNew(maskedtxtCPF.Text))
                        {
                            DialogResult dr = MessageBox.Show(ConfigMessage.Default.CPFDupl,
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                _IDMEDICO = MEDICOP.Save(Entity);
                                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            }
                        }
                        else
                        {
                            _IDMEDICO = MEDICOP.Save(Entity);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        }
                    }
                    else
                    {
                        _IDMEDICO = MEDICOP.Save(Entity);
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

        private Boolean VerificaCPFExistNew(string CPF)
        {
            Boolean result = false;

            if (CPF != "   .   .   -")
            {
                RowsFiltro FiltroProfileCNPJ = new RowsFiltro("CPF", "System.String", "=", CPF);
                RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

                MEDICOCollection MEDICOColllCPF = new MEDICOCollection();
                FiltroCNPJ.Insert(0, FiltroProfileCNPJ);
                MEDICOColllCPF = MEDICOP.ReadCollectionByParameter(FiltroCNPJ);

                if (MEDICOColllCPF.Count > 0)
                    result = true;
            }

            return result;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;
            errorProvider1.Clear();
            if (maskedtxtCPF.Text != "   .   .   -")
                if (!ValidacoesLibrary.ValidaCPF(maskedtxtCPF.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CPFErro);
                    maskedtxtCPF.Focus();
                    result = false;
                }


            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
           
            if (maskedtxtDataAd.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataAd.Text))
                {
                    errorProvider1.SetError(maskedtxtDataAd, ConfigMessage.Default.MsgDataInvalida);
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    result = false;
                }
                
            }
            
            if (maskedtxtComissao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(maskedtxtComissao.Text))
                {
                    errorProvider1.SetError(maskedtxtComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    result = false;
                }
                else if (ValidacoesLibrary.ValidaTipoPorc(maskedtxtComissao.Text)) 
                {
                    errorProvider1.SetError(maskedtxtComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    result = false;
                }

            }       

            else
                errorProvider1.SetError(txtNome, "");
              

            return result;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_MEDICOColl = LIS_MEDICOP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MEDICOColl;

                lblTotalPesquisa.Text = LIS_MEDICOColl.Count.ToString();
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
            if (LIS_MEDICOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MEDICOColl;
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

                LIS_MEDICOColl = LIS_MEDICOP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MEDICOColl;

                lblTotalPesquisa.Text = LIS_MEDICOColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_MEDICOColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_MEDICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MEDICOEntity>(orderBy);

                    LIS_MEDICOColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_MEDICOColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_MEDICOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_MEDICOColl[rowindex].IDMEDICO);

                    Entity = MEDICOP.Read(CodigoSelect);

                    tabControlFuncionario.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDMEDICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlFuncionario.SelectTab(1);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                         MEDICOP.Delete(_IDMEDICO);
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

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlFuncionario.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_MEDICOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_MEDICOColl.Count.ToString();
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            
        }

        private void cbUF_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void maskedtxtComissao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Comissão do funcionário, este valor não poderá ser maior que 100. Exemplo: 5,00";
        }
        
        private void maskedtxtSalario_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Salário do funcionário. Exemplo: 1.592,29";
        }

        private void maskedtxtCPF_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CPF do funcionário, não será possível cadastrar CPF inválido.";
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            PrintListaGeral();

            this.Cursor = Cursors.Default;	
        }

        private void PrintListaGeral()
        {
            if (LIS_MEDICOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlFuncionario.SelectTab(1);
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Médicos");

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
                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("CPF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
                e.Graphics.DrawString("Telefone", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Cidade", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("UF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_MEDICOColl.Count;

                while (IndexRegistro < LIS_MEDICOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_MEDICOColl[IndexRegistro].IDMEDICO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MEDICOColl[IndexRegistro].NOME, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MEDICOColl[IndexRegistro].CPF, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MEDICOColl[IndexRegistro].TELEFONE1, 12), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MEDICOColl[IndexRegistro].CIDADE, 27), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MEDICOColl[IndexRegistro].UF, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 730, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_MEDICOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (LIS_MEDICOColl.Count > 0)
                    {
                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                        e.Graphics.DrawString("Total da pesquisa: " + LIS_MEDICOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
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

        private void maskedtxtComissao_Move(object sender, EventArgs e)
        {

        }        

        private void maskedtxtComissao_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedtxtComissao_Leave(object sender, EventArgs e)
        {

            if (maskedtxtComissao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(maskedtxtComissao.Text))
                {
                    errorProvider1.SetError(maskedtxtComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    maskedtxtComissao.Focus();
                }
                else if (ValidacoesLibrary.ValidaTipoPorc(maskedtxtComissao.Text))
                {
                    errorProvider1.SetError(maskedtxtComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    maskedtxtComissao.Focus();
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    Double f = Convert.ToDouble(maskedtxtComissao.Text);
                    maskedtxtComissao.Text = string.Format("{0:n2}", f);
                    lblObsField.Text = string.Empty;
                    lblObsField.Text = string.Empty;
                }
            }

        }        

        private void maskedtxtCPF_Leave(object sender, EventArgs e)
        {
            if (maskedtxtCPF.Text != "   .   .   -")
            {
                if (!ValidacoesLibrary.ValidaCPF(maskedtxtCPF.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CPFErro);
                    maskedtxtCPF.Focus();
                    errorProvider1.SetError(maskedtxtCPF, ConfigMessage.Default.CPFErro);
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                lblObsField.Text = string.Empty;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
            
        }

        private void maskedtxtDataAd_Leave(object sender, EventArgs e)
        {
            if (maskedtxtDataAd.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataAd.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskedtxtDataAd.Focus();
                    errorProvider1.SetError(maskedtxtDataAd, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                lblObsField.Text = string.Empty;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void FrmFuncionario_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmFuncionario_KeyDown(object sender, KeyEventArgs e)
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
            if (LIS_MEDICOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_MEDICOColl[indice].IDMEDICO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = MEDICOP.Read(CodigoSelect);

                    tabControlFuncionario.SelectTab(0);
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
                            MEDICOP.Delete(CodigoSelect);
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

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedtxtAniv_Leave(object sender, EventArgs e)
        {
            if (maskedtxtAniv.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtAniv.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    errorProvider1.SetError(maskedtxtAniv, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(maskedtxtAniv, "");
                }
            }
            else
            {
                errorProvider1.SetError(maskedtxtAniv, "");
            }
        }




        
    }
}