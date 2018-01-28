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
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using System.IO;
using BmsSoftware.Modulos.FrmSearch;
using VVX;
using System.Diagnostics;
using BmsSoftware.Modulos.Etiqueta;


namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmContador : Form
    {
        Utility Util = new Utility();

        CONTADORProvider CONTADORP = new CONTADORProvider();
        CONTADORCollection CONTADORColl = new CONTADORCollection();       

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmContador()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int _CONTADORID = -1;
        int _COD_MUN_IBGE = -1;
        public CONTADOREntity Entity
        {
            get
            {
    
                string NOME = txtNome.Text.TrimEnd().TrimStart();//VARCHAR(50),
                string CPF = maskedtxtCPF.Text; //VARCHAR(20),
                string CNPJ = maskedtxtCNPJ.Text; //  VARCHAR(20),
                string CRC  = txtCRC.Text; // VARCHAR(20),
                string CEP = mktxtCep1.Text;//   VARCHAR(12),
                string ENDERECO = txtEnd1.Text; // VARCHAR(50),
                string NUMERO = txtNumEndereco.Text; // VARCHAR(10)
                string COMPLEMENTO = txtComplemento1.Text; //VARCHAR(20)
                string BAIRRO = txtBairro.Text; // VARCHAR(20)
                string FONE = txtTelefone1.Text; // VARCHAR(15),
                string FAX = txtFax.Text; // VARCHAR(15),
                string EMAIL = txtEmailCliente.Text;
                string OBSERVACAO = txtObservacao.Text;
                string FLAGATIVO = chkAtivo.Checked ? "S" : "N";

                return new CONTADOREntity(_CONTADORID, NOME, CPF, CNPJ, CRC, CEP, ENDERECO,
                                          NUMERO, COMPLEMENTO, BAIRRO, FONE, FAX, EMAIL,
                                          _COD_MUN_IBGE, OBSERVACAO, FLAGATIVO);

        
            }
            set
            {
                if (value != null)
                {
                    _CONTADORID = value.CONTADORID;
                    txtNome.Text = value.NOME;
                    maskedtxtCPF.Text = value.CPF;
                    maskedtxtCNPJ.Text = value.CNPJ;
                    txtCRC.Text = value.CRC;
                    mktxtCep1.Text = value.CEP;
                    txtEnd1.Text = value.ENDERECO;
                    txtNumEndereco.Text = value.NUMERO;
                    txtComplemento1.Text = value.COMPLEMENTO;
                    txtBairro.Text = value.BAIRRO;
                    txtTelefone1.Text = value.FONE;
                    txtFax.Text = value.FAX;
                    txtEmailCliente.Text = value.EMAIL;

                    //Busca a cidade
                    MUNICIPIOSEntity MUNICIPIOSTTY = new MUNICIPIOSEntity();
                    MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();
                    MUNICIPIOSTTY = MUNICIPIOSP.Read(Convert.ToInt32(value.COD_MUN));

                    if (MUNICIPIOSTTY != null)
                    {
                        txtCidade1.Text = MUNICIPIOSTTY.MUNICIPIO;
                        ESTADOProvider ESTADOP = new ESTADOProvider();
                        txtUF1.Text = ESTADOP.Read(Convert.ToInt32(MUNICIPIOSTTY.COD_UF_IBGE)).UF;
                        _COD_MUN_IBGE = Convert.ToInt32(value.COD_MUN);  
                    }

                    txtObservacao.Text = value.OBSERVACAO;

                    chkAtivo.Checked  = value.FLAGATIVO == "S" ? true : false;

                    errorProvider1.Clear();
                }
                else
                {
                    _CONTADORID = -1;
                    txtNome.Text = string.Empty;
                    maskedtxtCPF.Text = "   .   .   -";
                    maskedtxtCNPJ.Text = "  .   .   /    -";
                    txtCRC.Text = string.Empty;
                    mktxtCep1.Text = "     -";
                    txtEnd1.Text = string.Empty;
                    txtNumEndereco.Text = string.Empty;
                    txtComplemento1.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    txtFax.Text = string.Empty;
                    txtEmailCliente.Text = string.Empty;            

                    //Busca a cidade
                    LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                    LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                    EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", EMPRESAP.Read(1).CIDADE.Replace("'", "")));
                    RowRelatorio.Add(new RowsFiltro("uf", "System.String", "=", EMPRESAP.Read(1).UF));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                    if(LIS_MUNICIPIOSColl.Count > 0)
                    {
                        _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
                        txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                        txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                    }
                    else
                    {
                        _COD_MUN_IBGE = -1;
                        txtCidade1.Text = string.Empty;
                        txtUF1.Text = string.Empty;
                    }

                    txtObservacao.Text = string.Empty;
                    chkAtivo.Checked = false;
                    errorProvider1.Clear();
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
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }
        

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente.ActiveForm.Close();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 

            
            GetToolStripButtonCadastro();        
            PreencheDropTipoPesquisa();          
         
            PreencheDropCamposPesquisa();       

            //Exibir dados do cliente consultado em outra tela
            if (_CONTADORID != -1)
                Entity = CONTADORP.Read(_CONTADORID);
            else
                Entity = null;

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

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
        }     


        private void TSBVolta_Click(object sender, EventArgs e)
        {
            FrmCliente.ActiveForm.Close();
        }

        private void txtbNome_Enter(object sender, EventArgs e)
        {
         //   txtbNome.BackColor = Config.Default.ColorEnterTxtBox;
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void cbUF_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUF2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUF2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void bntCadClassificacao_Click(object sender, EventArgs e)
        {
            using (FrmClassificacao frm = new FrmClassificacao())
            {
               frm.ShowDialog();
            }
        }

        private void cbClassificacao_Enter(object sender, EventArgs e)
        {
           
        }

        private void cbTipoRegiao_Enter(object sender, EventArgs e)
        {
            
        }

       
        private void cbProfissaoAtividade_Enter(object sender, EventArgs e)
        {
           
        }

        private void btnProfiRamos_Click(object sender, EventArgs e)
        {
            using (FrmProfissaoRamoAtividade frm = new FrmProfissaoRamoAtividade())
            {
                frm.ShowDialog();
            }
        }

        private void maskedtxtCPF_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CPF do cliente, não será possível cadastrar CPF inválido.";
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(errorProvider1.ToString());
        }

        private void maskedtxtCNPJ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CNPJ do cliente, não será possível cadastrar CNPJ inválido.";
        }
       
        private void makCPFConjuge_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CPF do cônjuge, não será possível cadastrar CPF inválido.";
        }

        private void maskedtxtDataAd_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data de nascimento do cliente.";
        }

        private void mskDtAdmissao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data de admissão do cônjuge na empresa referida.";
        }

        private void maskDtaNascConjuge_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data de nascimento do cônjuge.";
        }

        private void mskDtAdmissao_Leave(object sender, EventArgs e)
        {
            
        }

        private void mskDataRetorno_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data de retorno do contato com o cliente.";
        }
       

        private void TxtCredito_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Crédito do cliente. Exemplo: 1.592,29";
        }

        private void txtRGConjuge_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "RG do Cônjuge";
        }

        private void textBox77_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Renda do Cônjuge. Exemplo: 1.592,29";
        }

        private void textBox73_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nome do cônjuge.";
        }

        private void textBox78_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Empresa/Emprego do cônjuge.";
        }

        private void textBox80_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Cargo do cônjuge na empresa referida.";
        }

        private void textBox81_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Telefone do cônjuge.";
        }
       
        private void gravaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
               Grava(); 
        }
       
        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                     _CONTADORID = CONTADORP.Save(Entity);
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
            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtCidade1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label135, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (_COD_MUN_IBGE == -1)
            {
                errorProvider1.SetError(label27, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }          
            else
            {
                errorProvider1.SetError(txtNome, "");
            }

            return result;
        }


        public void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                CONTADORColl = CONTADORP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = CONTADORColl;

                lblTotalPesquisa.Text = CONTADORColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlCliente.SelectedIndex == 2)
                {
                    errorProvider2.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
            }
            else
            {
                errorProvider2.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (CONTADORColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = CONTADORColl;
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

                CONTADORColl = CONTADORP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = CONTADORColl;

                lblTotalPesquisa.Text = CONTADORColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CONTADORColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                //Exibir o índice da linha atual
                int CodigoSelect = Convert.ToInt32(CONTADORColl[rowindex].CONTADORID);

                Entity = CONTADORP.Read(CodigoSelect);

                tabControlCliente.SelectTab(0);
                txtNome.Focus();
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCliente.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCliente.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_CONTADORID == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlCliente.SelectTab(1);
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
                        CONTADORP.Delete(_CONTADORID);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        btnPesquisa_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Blue");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void cbUF2_KeyDown_1(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUF2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void pesquisaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click_1(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            CONTADORColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = CONTADORColl.Count.ToString();
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
            if (CONTADORColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCliente.SelectTab(1);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }


        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (CONTADORColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                // tabVeiculo.SelectTab(1);
            }
            else
                FichadoClienteloLote();
        }       

        private void maskedtxtCNPJ_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtCNPJ.Text != "  .   .   /    -")
            {
                if (!ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CNPJErro);
                    maskedtxtCNPJ.Focus();
                    errorProvider1.SetError(this, ConfigMessage.Default.CNPJErro);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(0);
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    errorProvider1.SetError(this, "");
                    e.Cancel = false;
                }
            }
            else
            {
                lblObsField.Text = string.Empty;
                errorProvider1.SetError(maskedtxtCNPJ, "");
            }
        }

        private void maskedtxtCNPJ_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(maskedtxtCNPJ, string.Empty);
        }

        private void maskedtxtCPF_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtCPF.Text != "   .   .   -")
            {
                if (!ValidacoesLibrary.ValidaCPF(maskedtxtCPF.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CPFErro);
                    maskedtxtCPF.Focus();
                    errorProvider1.SetError(maskedtxtCPF, ConfigMessage.Default.CPFErro);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(0);
                }
                else
                {
                    lblObsField.Text = string.Empty;
                    errorProvider1.SetError(maskedtxtCPF, "");
                    e.Cancel = false;
                }
            }
            else
            {
                lblObsField.Text = string.Empty;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Boolean ErroProviderValid = true;
            foreach (Control ctrl in this.Controls)
            {
                ErroProviderValid = Validate();
                if (!ErroProviderValid)
                    break;
            }

            if (ErroProviderValid)
            {
                  CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava(); this.Cursor = Cursors.Default;
            }
        }

        private void TxtRendaCliente_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void TxtCreditoCliente_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void maskedtxtDataNascimento_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void maskDtaNascConjuge_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void makCPFConjuge_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtRendaConjuge_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void mskDtAdmissao_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void mskDataRetornoContato_Validating(object sender, CancelEventArgs e)
        {
        }

        private void FrmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void voltaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CONTADORColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<CONTADOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTADOREntity>(orderBy);

                    CONTADORColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = CONTADORColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void FrmCliente_KeyDown(object sender, KeyEventArgs e)
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
            if (CONTADORColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(CONTADORColl[indice].CONTADORID);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CONTADORP.Read(CodigoSelect);

                    tabControlCliente.SelectTab(0);
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
                            CONTADORP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            btnPesquisa_Click(null, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnTipoRegiao_Click(object sender, EventArgs e)
        {
            using (FrmTipoRegiao frm = new FrmTipoRegiao())
            {
                  frm.ShowDialog();
            }
        }

        private void txtCidade1_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Duplo click para pesquisar a cidade.";
          
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
                    txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                    txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                    _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);

                }
            }
           
        }

        private void txtCidade1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
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
                        txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                        txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                        _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);

                    }
                }
            }
        }

        private void txtUF1_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Pressione Ctrl+E para pesquisar a cidade.";
        }

        private void txtUF1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
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
                        txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                        txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                        _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
                    }
                }
            }
        }

        private void btnCadParentesco_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCadOcasiao_Click(object sender, EventArgs e)
        {
           
        }

        private void mkDataAniv_Validating(object sender, CancelEventArgs e)
        {
        }

        private void btnAddAnivers_Click(object sender, EventArgs e)
        {
           
        }   

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridDataComem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void datasComemorativasGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimiDataComemorativaGeral();
        }

        private void ImprimiDataComemorativaGeral()
        {
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        string DataInicial = string.Empty;
        string DataFim = string.Empty;
        private void dataComemorativasFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataInicial = InputBox("Data Inicial", ConfigSistema1.Default.NomeEmpresa, string.Empty);
            DataFim = InputBox("Data Final", ConfigSistema1.Default.NomeEmpresa, string.Empty);

            if (!ValidacoesLibrary.ValidaTipoDateTime(DataInicial))
                MessageBox.Show("Erro na Data Inicial: " + ConfigMessage.Default.MsgDataInvalida);
            else if (!ValidacoesLibrary.ValidaTipoDateTime(DataFim))
                MessageBox.Show("Erro na Data Final: " + ConfigMessage.Default.MsgDataInvalida);
            else
                ImprimiDataComemorativaFiltro();
        }

        private void ImprimiDataComemorativaFiltro()
        {
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument3;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }


        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          

        }
        private LIS_DATACOMEMORATIVACollection DataRel(int CONTADORID)
        {
            LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAColl = new LIS_DATACOMEMORATIVACollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            if (DataInicial != string.Empty)
            {
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial), "and"));
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFim), "and"));
                RowRelatorio.Add(new RowsFiltro("CONTADORID", "System.Int32", "=", CONTADORID.ToString()));
            }
            else
            {
                RowRelatorio.Add(new RowsFiltro("CONTADORID", "System.Int32", "=", CONTADORID.ToString()));
            }

            LIS_DATACOMEMORATIVAProvider LIS_DATACOMEMORATIVAP = new LIS_DATACOMEMORATIVAProvider();
            LIS_DATACOMEMORATIVAColl = LIS_DATACOMEMORATIVAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_DATACOMEMORATIVAColl;
        }
      

        private void datasComemorativasFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataInicial = InputBox("Data Inicial", ConfigSistema1.Default.NomeEmpresa, string.Empty);
            DataFim = InputBox("Data Final", ConfigSistema1.Default.NomeEmpresa, string.Empty);

            if (!ValidacoesLibrary.ValidaTipoDateTime(DataInicial))
                MessageBox.Show("Erro na Data Inicial: " + ConfigMessage.Default.MsgDataInvalida);
            else if (!ValidacoesLibrary.ValidaTipoDateTime(DataFim))
                MessageBox.Show("Erro na Data Final: " + ConfigMessage.Default.MsgDataInvalida);
            else
                ImprimiDataComemorativaFiltro();
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            

        }
        private void fichaDoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CONTADORColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                // tabVeiculo.SelectTab(1);
            }
            else
                FichadoClienteloLote();
        }

        private void FichadoClienteloLote()
        {
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument4;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDocument4;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public string FLAGSUPORTE { get; set; }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CONTADORColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlCliente.SelectTab(1);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void salvaEmLoteGdoorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        } 
     
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void porMêsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MesAniversario = InputBox("Mês de aniversário (Exemplo: Janeiro Digite 01)", ConfigSistema1.Default.NomeEmpresa, string.Empty);

            if (!ValidacoesLibrary.ValidaTipoInt32(MesAniversario) ||  Convert.ToInt32(MesAniversario) > 12 )
                MessageBox.Show("Erro no dia do mês do aniversario: " + ConfigMessage.Default.MsgDataInvalida);
            else
            {
                ImprimDataAniversario(MesAniversario);
            }
        }

        string MesAniversario = string.Empty;
        private void ImprimDataAniversario(String MesAniversario)
        {
            tabControlCliente.SelectTab(1);
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("extract(month from (DATANASCIMENTOCLIENTE))", "System.DateTime", "=", MesAniversario));

            CONTADORColl = CONTADORP.ReadCollectionByParameter(RowRelatorio, "NOME");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = CONTADORColl;
        }

        int _Line = 0;
        private void printDocument4_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
            paginaAtual = 0;
        }

        private void txtCidade1_MouseDoubleClick(object sender, MouseEventArgs e)
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
                    txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                    txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                    _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);

                }
            }
        }

        private void DataGriewDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mktxtCep1_DoubleClick(object sender, EventArgs e)
        {
            using (FrmSearchEnderecoCEP frm = new FrmSearchEnderecoCEP())
            {
                frm.EnderecoSelecionado = txtEnd1.Text.Trim();
                frm.CidadSelecionado = txtCidade1.Text.Trim();
                frm.UFSelecionado = txtUF1.Text.Trim();
                frm.ShowDialog();
                mktxtCep1.Text = frm.CEPSelecionado;
            }
        }

        private void mktxtCep1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchEnderecoCEP frm = new FrmSearchEnderecoCEP())
                {
                    frm.EnderecoSelecionado = txtEnd1.Text.Trim();
                    frm.CidadSelecionado = txtCidade1.Text.Trim();
                    frm.UFSelecionado = txtUF1.Text.Trim();
                    frm.ShowDialog();
                    mktxtCep1.Text = frm.CEPSelecionado;
                }
            }
        }

        private int RetornoCidade(string Cidade, string UF)
        {
            int result = -1;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", Cidade.ToUpper().ToString()));
            RowRelatorio.Add(new RowsFiltro("UF", "System.String", "=", UF.ToUpper().ToString()));
            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_MUNICIPIOSColl.Count > 0)
            {
                result = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
                txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
            }
            else
                result = -1;

            return result;
        }

        private void mktxtCep1_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Ctrl+E ou duplo clique para pesquisar CEP!";
        }

        private void migrarCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMigraCidade frm = new FrmMigraCidade())
            {
                frm.ShowDialog();
            }
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
             
        }
        

        private void CONTADORPorFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClienteFuncionario Frm = new FrmClienteFuncionario();
            Frm.ShowDialog();
        }

        private void pesquisaDeAniversárioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salvaEmLoteDigiSatCupomFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void únicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void enviarTorpedoSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pDEtiqueta6080_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

       private void pDEtiqueta6080_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       private void pimaco6080PesquisaToolStripMenuItem_Click(object sender, EventArgs e)
       {
         
       }

      

       private void pimaco6080ToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
       }

       private void btnMaquina_Click(object sender, EventArgs e)
       {
           openFileDialog1.Filter = "Arquivos  (*.pdf)|*.pdf"; // Filtra os tipos de arquivos desejados
           openFileDialog1.ShowDialog();
       }

       private void btnAddImagem_Click(object sender, EventArgs e)
       {
          
       }    

       private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
       {
          
       }

       public static byte[] GetFoto(string caminhoArquivoFoto)
       {
           FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
           BinaryReader br = new BinaryReader(fs);

           byte[] foto = br.ReadBytes((int)fs.Length);

           br.Close();
           fs.Close();

           return foto;
       }

       private void dtgImag_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
          

           
       }

       private void txtTelefone1_Leave(object sender, EventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           TextBoxSelec.Text = Util.RetiraLetras(TextBoxSelec.Text);

           if (TextBoxSelec.Text.Length == 10)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(TelefoneForm));
           }
           else if (TextBoxSelec.Text.Length == 11)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(TelefoneForm));
           }
          
       }

       private void txtCelular_Leave(object sender, EventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           TextBoxSelec.Text = Util.RetiraLetras(TextBoxSelec.Text);

           if (TextBoxSelec.Text.Length == 10)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(TelefoneForm));
           }
           else if (TextBoxSelec.Text.Length == 11)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(TelefoneForm));
           }
          
       }

       private void txtFax_Leave(object sender, EventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           TextBoxSelec.Text = Util.RetiraLetras(TextBoxSelec.Text);

           if (TextBoxSelec.Text.Length == 10)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(TelefoneForm));
           }
           else if (TextBoxSelec.Text.Length == 11)
           {
               object TelefoneForm = TextBoxSelec.Text;
               TextBoxSelec.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(TelefoneForm));
           }
         
       }

       private void lblObsField_Click(object sender, EventArgs e)
       {

       }

    }
}