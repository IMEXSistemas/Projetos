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
using BmsSoftware.Classes.BMSworks.UI;


namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmTabelaIBPT : Form
    {
        Utility Util = new Utility();    

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
        LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();

        LIS_TABELAIBPTCollection LIS_TABELAIBPTColl = new LIS_TABELAIBPTCollection();
        LIS_TABELAIBPTProvider LIS_TABELAIBPTP = new LIS_TABELAIBPTProvider();
        TABELAIBPTProvider TABELAIBPTP = new TABELAIBPTProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmTabelaIBPT()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int _IDTABELAIBPT = -1;
        int _IDNCM = -1;
        int _IDCODUFIBGE = -1;
        public TABELAIBPTEntity Entity
        {
            get
            {
    
               decimal ALNACIONAL = Convert.ToDecimal(txtAlNacional.Text);   // NUMERIC(15,2),
               decimal ALIMPORTACAO  = Convert.ToDecimal(txtAlImportacao.Text); // NUMERIC(15,2),
               decimal ALESTADUAL   = Convert.ToDecimal(txtAliqEstadual.Text);  // NUMERIC(15,2),
               decimal ALMUNICIPAL  = Convert.ToDecimal(txtAliquotaMunicipal.Text); //  NUMERIC(15,2),
               DateTime? DTINICIO  = null;  //  DATE,
               DateTime? DTFIM  = null;       //  DATE,
               string CHAVE = txtChave.Text;       //  VARCHAR(100),
               string VERSAO = txtVersao.Text;      //  VARCHAR(100),
               string FONTE = txtFonte.Text;       //  VARCHAR(100),
               int TIPO = Convert.ToInt32(TxtTipo.Text);         // INTEGER,
               string EX = txtEX.Text;           // VARCHAR(10)

                return new TABELAIBPTEntity(_IDTABELAIBPT, _IDNCM, ALNACIONAL, ALIMPORTACAO, ALESTADUAL, ALMUNICIPAL,
                                             DTINICIO, DTFIM, CHAVE, VERSAO, FONTE, _IDCODUFIBGE, TIPO, EX);

        
            }
            set
            {
                if (value != null)
                {
                    _IDTABELAIBPT = value.IDTABELAIBPT;
                    txtAlNacional.Text = Convert.ToDecimal(value.ALNACIONAL).ToString("N2");
                    txtAlImportacao.Text = Convert.ToDecimal(value.ALIMPORTACAO).ToString("N2");
                    txtAliqEstadual.Text = Convert.ToDecimal(value.ALESTADUAL).ToString("N2");
                    txtAliquotaMunicipal.Text = Convert.ToDecimal(value.ALMUNICIPAL).ToString("N2");
                    txtChave.Text = value.CHAVE;
                    txtVersao.Text = value.VERSAO;
                    txtFonte.Text = value.FONTE;
                    TxtTipo.Text = value.TIPO.ToString();
                    txtEX.Text = value.EX;

                    //BUSCA O NCM
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDNCM", "System.Int32", "=", value.IDNCM.ToString()));
                    NCMCollection NCMColl = new NCMCollection();
                    NCMProvider NCMP = new NCMProvider();
                    NCMColl = NCMP.ReadCollectionByParameter(RowRelatorio);
                    if (NCMColl.Count > 0)
                    {
                        txtNCM.Text = NCMColl[0].CODNCM;
                        _IDNCM = Convert.ToInt32(value.IDNCM);
                    }
                    
                    //Busca o estado                  
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("COD_UF_IBGE", "System.Int32", "=", value.IDCODUFIBGE.ToString()));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_MUNICIPIOSColl.Count > 0)
                    {
                        _IDCODUFIBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_UF_IBGE);
                        txtUF1.Text = LIS_MUNICIPIOSColl[0].UF;
                    }

                    errorProvider1.Clear();

                    txtChave.Focus();
                }
                else
                {
                    _IDTABELAIBPT = -1;
                    _IDNCM = -1;
                    txtNCM.Text = string.Empty;
                    txtAlNacional.Text = "0,00";
                    txtAlImportacao.Text = "0,00";
                    txtAliqEstadual.Text = "0,00";
                    txtAliquotaMunicipal.Text = "0,00";                
                    errorProvider1.Clear();
                    txtNCM.Focus();
                    
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

            btnCadNCM.Image = Util.GetAddressImage(6);

            //Exibir dados do cliente consultado em outra tela
            if (_IDTABELAIBPT != -1)
                Entity = TABELAIBPTP.Read(_IDTABELAIBPT);
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

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

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
                    _IDTABELAIBPT = TABELAIBPTP.Save(Entity);
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
            if (txtNCM.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label38, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if(_IDNCM == -1)
            {
                errorProvider1.SetError(label38, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtCidade1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label42, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (_IDCODUFIBGE == -1)
            {
                errorProvider1.SetError(label41, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }          
            else
            {
                errorProvider1.Clear();
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

                LIS_TABELAIBPTColl = LIS_TABELAIBPTP.ReadCollectionByParameter(Filtro);
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_TABELAIBPTColl;

                lblTotalPesquisa.Text = LIS_TABELAIBPTColl.Count.ToString();
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
            if (LIS_TABELAIBPTColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_TABELAIBPTColl;
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

                LIS_TABELAIBPTColl = LIS_TABELAIBPTP.ReadCollectionByParameter(Filtro);
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_TABELAIBPTColl;

                lblTotalPesquisa.Text = LIS_TABELAIBPTColl.Count.ToString();
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
            if (LIS_TABELAIBPTColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                //Exibir o índice da linha atual
                int CodigoSelect = Convert.ToInt32(LIS_TABELAIBPTColl[rowindex].IDTABELAIBPT);

                Entity = TABELAIBPTP.Read(CodigoSelect);

                tabControlCliente.SelectTab(0);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCliente.SelectTab(0);
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlCliente.SelectTab(0);
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDTABELAIBPT == -1)
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
                        TABELAIBPTP.Delete(_IDTABELAIBPT);
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
            LIS_TABELAIBPTColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_TABELAIBPTColl.Count.ToString();
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
            if (LIS_TABELAIBPTColl.Count == 0)
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
            if (LIS_TABELAIBPTColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                // tabVeiculo.SelectTab(1);
            }
            else
                FichadoClienteloLote();
        }       

        private void maskedtxtCNPJ_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void maskedtxtCNPJ_Validated(object sender, EventArgs e)
        {
            
        }

        private void maskedtxtCPF_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if(Validacoes())
              Grava();
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
            if (LIS_TABELAIBPTColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_TABELAIBPTEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_TABELAIBPTEntity>(orderBy);

                    LIS_TABELAIBPTColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_TABELAIBPTColl;
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
            if (LIS_TABELAIBPTColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_TABELAIBPTColl[indice].IDTABELAIBPT);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = TABELAIBPTP.Read(CodigoSelect);

                    tabControlCliente.SelectTab(0);
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            TABELAIBPTP.Delete(CodigoSelect);
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
                    _IDCODUFIBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_UF_IBGE);

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
                        _IDCODUFIBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_UF_IBGE);  

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
                        _IDCODUFIBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_UF_IBGE);  
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
     

        private void datasComemorativasFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_TABELAIBPTColl.Count == 0)
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
           
        }

     
        private void ImprimDataAniversario(String MesAniversario)
        {
           
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
                    _IDCODUFIBGE= Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_UF_IBGE);

                }
            }
        }

        private void DataGriewDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mktxtCep1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void mktxtCep1_KeyDown(object sender, KeyEventArgs e)
        {
           
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

      

       private void dtgImag_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
          

           
       }

       private void txtTelefone1_Leave(object sender, EventArgs e)
       {
          
          
       }

       private void txtCelular_Leave(object sender, EventArgs e)
       {
          
          
       }

       private void txtFax_Leave(object sender, EventArgs e)
       {
         
         
       }

       private void txtAlNacional_Validating(object sender, CancelEventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           if (TextBoxSelec.Text != string.Empty)
           {
               if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
               {
                   errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                   MessageBox.Show(ConfigMessage.Default.FieldErro);
                   TextBoxSelec.Focus();
               }
               else
               {

                   Double f = Convert.ToDouble(TextBoxSelec.Text);
                   TextBoxSelec.Text = string.Format("{0:n2}", f);
                   errorProvider1.SetError(TextBoxSelec, "");
               }
           }
           else
               TextBoxSelec.Text = "0,00";
       }

       private void txtAlImportacao_Validating(object sender, CancelEventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           if (TextBoxSelec.Text != string.Empty)
           {
               if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
               {
                   errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                   MessageBox.Show(ConfigMessage.Default.FieldErro);
                   TextBoxSelec.Focus();
               }
               else
               {

                   Double f = Convert.ToDouble(TextBoxSelec.Text);
                   TextBoxSelec.Text = string.Format("{0:n2}", f);
                   errorProvider1.SetError(TextBoxSelec, "");
               }
           }
           else
               TextBoxSelec.Text = "0,00";
       }

       private void txtAliqEstadual_Validating(object sender, CancelEventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           if (TextBoxSelec.Text != string.Empty)
           {
               if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
               {
                   errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                   MessageBox.Show(ConfigMessage.Default.FieldErro);
                   TextBoxSelec.Focus();
               }
               else
               {

                   Double f = Convert.ToDouble(TextBoxSelec.Text);
                   TextBoxSelec.Text = string.Format("{0:n2}", f);
                   errorProvider1.SetError(TextBoxSelec, "");
               }
           }
           else
               TextBoxSelec.Text = "0,00";
       }

       private void txtAliquotaMunicipal_Validating(object sender, CancelEventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           if (TextBoxSelec.Text != string.Empty)
           {
               if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
               {
                   errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                   MessageBox.Show(ConfigMessage.Default.FieldErro);
                   TextBoxSelec.Focus();
               }
               else
               {

                   Double f = Convert.ToDouble(TextBoxSelec.Text);
                   TextBoxSelec.Text = string.Format("{0:n2}", f);
                   errorProvider1.SetError(TextBoxSelec, "");
               }
           }
           else
               TextBoxSelec.Text = "0,00";
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
               frm.TituloSelec = "Relação de Pedidos";
               frm.NometelaSelec = this.Name;
               frm.DataGridExport = DataGriewDados;
               frm.ShowDialog();
           }
       }

       private void TxtTipo_Validating(object sender, CancelEventArgs e)
       {
           TextBox TextBoxSelec = sender as TextBox;
           if (TextBoxSelec.Text != string.Empty)
           {
               if (!ValidacoesLibrary.ValidaTipoInt32(TextBoxSelec.Text))
               {
                   errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                   MessageBox.Show(ConfigMessage.Default.FieldErro);
                   TextBoxSelec.Focus();
               }
           }
           else
               TextBoxSelec.Text = "0";
       }

       private void txtNCM_Enter(object sender, EventArgs e)
       {

           lblObsField.Text = "Nomenclatura Comum do Mercosul/Sistema Harmonizado, CTrl+E para pesquisar!";

            using (FrmSearchNCM frm = new FrmSearchNCM())
            {
                frm.ShowDialog();
                var result = frm.Result;
                _IDNCM = frm.Result2;

                txtNCM.Text = result;
            }
           
       }

       private void txtNCM_KeyDown(object sender, KeyEventArgs e)
       {
           if ((Control.ModifierKeys == Keys.Control) &&
        (e.KeyCode == Keys.E))
           {
               using (FrmSearchNCM frm = new FrmSearchNCM())
               {
                   frm.ShowDialog();
                   var result = frm.Result;

                   txtNCM.Text = result;
                   _IDNCM = frm.Result2;
               }
           }
       }

       private void btnCadNCM_Click(object sender, EventArgs e)
       {
           using (FrmNCM frm = new FrmNCM())
           {
               frm.ShowDialog();
           }
       }

     
    }
}