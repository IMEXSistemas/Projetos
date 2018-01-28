using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using System.IO;
using BoletoNet;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBoletaBancaria : Form
    {
        Utility Util = new Utility();

        BANCOCollection BANCOColl = new BANCOCollection();
        LIS_CONFIGBOLETACollection LIS_CONFIGBOLETAColl = new LIS_CONFIGBOLETACollection();

        CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
        LIS_CONFIGBOLETAProvider LIS_CONFIGBOLETAP = new LIS_CONFIGBOLETAProvider();
        BANCOProvider BANCOP = new BANCOProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public FrmBoletaBancaria()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        string NumeroBancoTeste = string.Empty;
        string NossoNumero = string.Empty;
        Decimal ValorTeste = 0;
        public int _IDCONFIGBOLETA = -1;
        public CONFIGBOLETAEntity Entity
        {
            get
            {
                string NOME = txtNome.Text;
                int IDBANCO = Convert.ToInt32(cbBanco.SelectedValue);
                string CARTEIRA = txtCarteira.Text;
                string CONVENIO = txtConvenio.Text;
                string TIPOMODALIDADE = txtTipoModalidade.Text;
                
                int? CODCEDENTE = null;
                if(txtCodCedente.Text != string.Empty)
                    CODCEDENTE = Convert.ToInt32(txtCodCedente.Text);

                string NOMECEDENTE = txtNomeCedente.Text;
                string CPFCNPJCEDENTE = txtCPFCNPJ.Text;
                string AGENCIA = txtAgenciaCendente.Text;
                string DIGAGENCIA = txtDigAgCedente.Text;
                string CONTA = txtContaCedente.Text;
                string DIGCONTA = txtDigContaCedente.Text;
                string ESPECIEDOC = txtEspecieDocumento.Text;
                string ACEITE = txtAceite.Text;
                decimal VALORTAXA = Convert.ToDecimal(txtTaxaBancaria.Text);
                string OBSERVACAO = txtObservacao.Text;
                string INSTRUCAO1 = txtInstrucao1.Text;
                string INSTRUCAO2 = txtInstrucao2.Text;
                string INSTRUCAO3 = txtInstrucao3.Text;
                string INSTRUCAO4 = txtInstrucao4.Text;
                string INSTRUCAO5 = txtInstrucao5.Text;
                string INSTRUCAO6 = txtInstrucao6.Text;
                string INSTRUCAO7 = txtInstrucao7.Text;
                string INSTRUCAO8 = txtInstrucao8.Text;
                string INSTRUCAO9 = txtInstrucao9.Text;
                string DIGCEDENTE = txtDigitoCodCedente.Text;

                return new CONFIGBOLETAEntity(_IDCONFIGBOLETA, NOME, IDBANCO,
                                              CARTEIRA, CONVENIO, TIPOMODALIDADE,
                                              CODCEDENTE,
                                              NOMECEDENTE, CPFCNPJCEDENTE, AGENCIA,
                                              DIGAGENCIA, CONTA, DIGCONTA, ESPECIEDOC,
                                              ACEITE, VALORTAXA, OBSERVACAO,INSTRUCAO1,
                                              INSTRUCAO2,INSTRUCAO3,
                                              INSTRUCAO4,INSTRUCAO5,INSTRUCAO6,
                                              INSTRUCAO7, INSTRUCAO8, INSTRUCAO9, DIGCEDENTE);
            }
            set
            {

                if (value != null)
                {
                    _IDCONFIGBOLETA = value.IDCONFIGBOLETA;
                    txtNome.Text = value.NOME;
                    cbBanco.SelectedValue = value.IDBANCO;

                    //Seleciona o numero do banco
                   // NumeroBancoTeste = BANCOP.Read(Convert.ToInt32(Entity.IDBANCO)).NUMEROBANCO;
                    NumeroBancoTeste = BANCOP.Read(Convert.ToInt32(value.IDBANCO)).NUMEROBANCO;

                    txtCarteira.Text = value.CARTEIRA;
                    txtConvenio.Text = value.CONVENIO;
                    txtTipoModalidade.Text = value.TIPOMODALIDADE;
                    txtCodCedente.Text = value.CODCEDENTE.ToString();
                    txtNomeCedente.Text = value.NOMECEDENTE;
                    txtCPFCNPJ.Text = value.CPFCNPJCEDENTE;
                    txtAgenciaCendente.Text = value.AGENCIA;
                    txtDigAgCedente.Text = value.DIGAGENCIA;
                    txtContaCedente.Text = value.CONTA;
                    txtDigContaCedente.Text = value.DIGCONTA;
                    txtEspecieDocumento.Text = value.ESPECIEDOC;
                    txtAceite.Text = value.ACEITE;

                    if (value.VALORTAXA != null)
                        txtTaxaBancaria.Text = Convert.ToDecimal(value.VALORTAXA).ToString("n2");
                    else
                        txtTaxaBancaria.Text = "0,00";

                    txtObservacao.Text = value.OBSERVACAO;
                    txtInstrucao1.Text= value.INSTRUCAO1;
                    txtInstrucao2.Text = value.INSTRUCAO2;
                    txtInstrucao3.Text = value.INSTRUCAO3;
                    txtInstrucao4.Text = value.INSTRUCAO4;
                    txtInstrucao5.Text = value.INSTRUCAO5;
                    txtInstrucao6.Text = value.INSTRUCAO6;
                    txtInstrucao7.Text = value.INSTRUCAO7;
                    txtInstrucao8.Text = value.INSTRUCAO8;
                    txtInstrucao9.Text = value.INSTRUCAO9;
                    txtDigitoCodCedente.Text = value.DIGCEDENTE;
                    
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCONFIGBOLETA = -1;
                    txtNome.Text = string.Empty;
                    cbBanco.SelectedIndex = 0;
                    txtCarteira.Text = string.Empty;
                    txtConvenio.Text = string.Empty;
                    txtTipoModalidade.Text = string.Empty;
                    txtCodCedente.Text = string.Empty;
                    txtNomeCedente.Text = string.Empty;
                    txtCPFCNPJ.Text = string.Empty;
                    txtAgenciaCendente.Text = string.Empty;
                    txtDigAgCedente.Text = string.Empty;
                    txtContaCedente.Text = string.Empty;
                    txtDigContaCedente.Text = string.Empty;
                    txtEspecieDocumento.Text = string.Empty;
                    txtAceite.Text = "S";
                    txtTaxaBancaria.Text = "0,00";
                    txtObservacao.Text = string.Empty;
                    txtNome.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    txtInstrucao1.Text = string.Empty;
                    txtInstrucao2.Text = string.Empty;
                    txtInstrucao3.Text = string.Empty;
                    txtInstrucao4.Text = string.Empty;
                    txtInstrucao5.Text = string.Empty;
                    txtInstrucao6.Text = string.Empty;
                    txtInstrucao7.Text = string.Empty;
                    txtInstrucao8.Text = string.Empty;
                    txtInstrucao9.Text = string.Empty;
                    txtDigitoCodCedente.Text = string.Empty;
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

        private void FrmBoletaBancaria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmBoletaBancaria_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetToolStripButtonCadastro();
            GetDropBanco();
            txtCarteira.Focus();
            VerificaAcesso();

            if (_IDCONFIGBOLETA != -1)
                Entity = CONFIGBOLETAP.Read(_IDCONFIGBOLETA);

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

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
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

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetDropBanco()
        {
            BANCOProvider BANCOP = new BANCOProvider();
            BANCOColl = BANCOP.ReadCollectionByParameter(null, "NOMEBANCO");

            cbBanco.DisplayMember = "NOMEBANCO";
            cbBanco.ValueMember = "IDBANCO";

            BANCOEntity BANCOTy = new BANCOEntity();
            BANCOTy.NOMEBANCO = ConfigMessage.Default.MsgDrop;
            BANCOTy.IDBANCO = -1;
            BANCOColl.Add(BANCOTy);

            Phydeaux.Utilities.DynamicComparer<BANCOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<BANCOEntity>(cbBanco.DisplayMember);

            BANCOColl.Sort(comparer.Comparer);
            cbBanco.DataSource = BANCOColl;

            cbBanco.SelectedIndex = 0;
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
                    
                    _IDCONFIGBOLETA = CONFIGBOLETAP.Save(Entity);
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
            if (txtCarteira.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCarteira, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtNomeCedente.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNomeCedente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            //else if (txtCodCedente.Text != string.Empty && !ValidacoesLibrary.ValidaTipoInt32(txtCodCedente.Text))
            //{
            //    errorProvider1.SetError(txtCodCedente, ConfigMessage.Default.FieldErro);
            //    MessageBox.Show(ConfigMessage.Default.FieldErro);
            //    result = false;
           // }
            else if (txtCPFCNPJ.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCPFCNPJ, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            } 
            else if (cbBanco.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbBanco, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtAgenciaCendente.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtAgenciaCendente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtContaCedente.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtContaCedente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }           
            else
                errorProvider1.Clear();

            return result;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_CONFIGBOLETAColl = LIS_CONFIGBOLETAP.ReadCollectionByParameter(Filtro, "NOME");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CONFIGBOLETAColl;

                lblTotalPesquisa.Text = LIS_CONFIGBOLETAColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlBoleta.SelectedIndex == 2)
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
            if (LIS_CONFIGBOLETAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CONFIGBOLETAColl;
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

                LIS_CONFIGBOLETAColl = LIS_CONFIGBOLETAP.ReadCollectionByParameter(Filtro, "NOME");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CONFIGBOLETAColl;

                lblTotalPesquisa.Text = LIS_CONFIGBOLETAColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlBoleta.SelectTab(0);
            txtCarteira.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDCONFIGBOLETA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlBoleta.SelectTab(2);
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
                        CONFIGBOLETAP.Delete(_IDCONFIGBOLETA);
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

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlBoleta.SelectTab(2);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlBoleta.SelectTab(2);
            txtCriterioPesquisa.Focus();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_CONFIGBOLETAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_CONFIGBOLETAColl[rowindex].IDCONFIGBOLETA);

                    Entity = CONFIGBOLETAP.Read(CodigoSelect);

                    tabControlBoleta.SelectTab(0);
                    txtCarteira.Focus();
                }
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_CONFIGBOLETAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_CONFIGBOLETAColl.Count.ToString();
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_CONFIGBOLETAColl.Count > 0)
                ImprimirListaGeral();
            else
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroImprimirPesquisa,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                tabControlBoleta.SelectTab(2);
                txtCriterioPesquisa.Focus();
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Geral de Boletas");

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
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Banco", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 210, 170);
                e.Graphics.DrawString("Agência", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 360, 170);
                e.Graphics.DrawString("Dg", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 430, 170);
                e.Graphics.DrawString("Conta", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, 170);
                e.Graphics.DrawString("Dg", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("Carteira", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 570, 170);
                
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_CONFIGBOLETAColl.Count;

                while (IndexRegistro < LIS_CONFIGBOLETAColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].NOME, 35), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].NOMEBANCO, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 210, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].AGENCIA, 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 360, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].DIGAGENCIA, 2), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 430, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].CONTA,10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].DIGCONTA, 2), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_CONFIGBOLETAColl[IndexRegistro].CARTEIRA, 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 570, config.PosicaoDaLinha);
                  

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_CONFIGBOLETAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_CONFIGBOLETAColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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

        private void boletaTesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCONFIGBOLETA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                tabControlBoleta.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirBoleta();
            }
        }

        private Boolean ValidaBanco()
        {
            Boolean result = true;

            //Seleciona dados da boleta configurada
            //Seleciona o numero do banco
            NumeroBancoTeste = BANCOP.Read(Convert.ToInt32(Entity.IDBANCO)).NUMEROBANCO;

            //Nosso numero para teste
            NossoNumero = InputBox("Nosso Número para teste", ConfigSistema1.Default.NomeEmpresa, "00000001");
            ValorTeste = Convert.ToDecimal(InputBox("Valor para teste", ConfigSistema1.Default.NomeEmpresa, "1"));

            //Validação.
            switch (NumeroBancoTeste)
            {
                case "001":
                    //Validação para o banco do Brasil
                    //Banco do Brasil.

                    //Agência com 4 caracteres.
                    if (Entity.AGENCIA.Length > 4)
                    {
                        MessageBox.Show("A Agência deve conter até 4 dígitos",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);
                          result = false;
                    }


                    //Conta com 8 caracteres.
                    if (Entity.CONTA.Length > 8)
                    {
                        MessageBox.Show("A Conta deve conter até 8 dígitos.",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);

                        result = false;
                    }


                    //Nosso Número deve ser no maximo de 10 dígitos.
                    if (NossoNumero.Length  > 10 )
                    {
                        MessageBox.Show("O Nosso Número deve ter no máximo 10 dígitos.",
                         ConfigSistema1.Default.NomeEmpresa,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    break;


                case "033":
                    //Santander.
                    break;

                case "070":
                    //Banco BRB.
                    break;

                case "104":
                    //Caixa Econômica Federal.
                    break;

                case "237":
                    //Banco Bradesco.
                    break;

                case "275":
                    //Validação Banco Real
                    //Banco Real.                      
                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (NossoNumero.Length < 7 || NossoNumero.Length > 13)
                    {
                        MessageBox.Show("O Nosso Número deve ser entre 7 e 13 caracteres.",
                           ConfigSistema1.Default.NomeEmpresa,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    //Carteira
                    if (Entity.CARTEIRA != "00" && Entity.CARTEIRA != "20" && Entity.CARTEIRA != "31"
                        && Entity.CARTEIRA != "42" && Entity.CARTEIRA != "47"
                        && Entity.CARTEIRA != "85" && Entity.CARTEIRA != "57")
                    {

                        MessageBox.Show("A Carteira deve ser 00,20,31,42,47,57 ou 85.",
                           ConfigSistema1.Default.NomeEmpresa,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada
                    //57 - última implementação ?

                    //Agência 4 dígitos.
                    if (Entity.AGENCIA.Length > 4)
                    {
                        MessageBox.Show("A Agência deve conter até 4 dígitos.",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    //Número da conta 6 dígitos.
                    if (Entity.CONTA.Length > 6)
                    {
                        MessageBox.Show("A Agência deve conter até 4 dígitos.",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    break;
                case "291":
                    //Banco BCN.
                    break;

                case "341":
                    //Banco Itaú.
                    break;

                case "347":
                    //Banco Sudameris.
                    break;

                case "356":
                    //Validação do Banco Real
                    //Banco Real.                       
                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (NossoNumero.Length < 7 || NossoNumero.Length > 13)
                    {
                        MessageBox.Show("O Nosso Número deve ser entre 7 e 13 caracteres.",
                         ConfigSistema1.Default.NomeEmpresa,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                        result = false;
                    }


                    //Carteira
                    if (Entity.CARTEIRA != "00" && Entity.CARTEIRA != "20" && Entity.CARTEIRA != "31"
                        && Entity.CARTEIRA != "42"
                        && Entity.CARTEIRA != "47" && Entity.CARTEIRA != "85" && Entity.CARTEIRA != "57")
                    {
                        MessageBox.Show("A Carteira deve ser 00,20,31,42,47,57 ou 85.",
                         ConfigSistema1.Default.NomeEmpresa,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada

                    //Agência 4 dígitos.
                    if (Entity.AGENCIA.Length > 4)
                    {
                        MessageBox.Show("A Agência deve conter até 4 dígitos.",
                        ConfigSistema1.Default.NomeEmpresa,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    //Número da conta 6 dígitos.
                    if (Entity.CONTA.Length > 6)
                    {
                        MessageBox.Show("A Conta Corrente deve conter até 6 dígitos.",
                        ConfigSistema1.Default.NomeEmpresa,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        result = false;
                    }

                    break;
                case "409":
                    //Banco Unibanco.
                    break;

                case "422":
                    //Banco Safra.
                    break;

                default:

                    break;
            }

            return result;
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
                try
                {
                    if (ValidaBanco())
                    {
                        switch (NumeroBancoTeste)
                        {
                            case "001":
                                //Banco Brasil.
                                ImprimiBoletaBrasil();
                                break;
                            case "003":
                                //Banco Basa.                            
                                ImprimiBoletaBasa();
                                break;
                            case "033":
                                // Banco Santander.                            
                                ImprimiBoletaSantander();
                                break;
                            case "041":
                                //Banco Banrisul
                                ImprimiBoletaBanrisul();
                                break;
                            case "070":
                                //Banco BRB
                                ImprimiBoletaBRB();
                                break;
                            case "104":
                                //Banco Caixa
                                ImprimiBoletaCaixa();
                                break;
                            case "237":
                                //Bradesco
                                ImprimiBoletaBradesco();
                                break;
                            case "341":
                                //Itau
                                ImprimiBoletaItau();
                                break;
                            case "347":
                                //Itau
                                ImprimiBoletaSudameris();
                                break;
                            case "356":
                                //Real
                                ImprimiBoletaReal();
                                break;
                            case "399":
                                //HSBC
                                ImprimiBoletaHSBC();
                                break;
                            case "409":
                                //Unibanco
                                ImprimiBoletaUnibanco();
                                break;
                            case "422":
                                //Safra
                                ImprimiBoletaSafra();
                                break;
                            case "756":
                                //SICOOB
                               ImprimirBoletaSICOOB();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ImprimirBoletaSICOOB()
        {


            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            //Dados para emitir boleto
            string data_vencimento = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = txtAgenciaCendente.Text;// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = txtDigAgCedente.Text; // 1 Digito da Agência
            string codigo_cedente = txtCodCedente.Text; // Numero do Convenio 
            string digito_codigo_cedente = txtDigContaCedente.Text; 	// Digito da Conta Corrente 1 Digito
            string nosso_numero = NossoNumero;	// Nosso Numero
            string carteira = txtCarteira.Text; // Código da Carteira
            string data_documento =  DateTime.Now.ToString("dd/MM/yyyy"); // Data de emissão do Boleto dd/MM/yyyy
            string valor =  ValorTeste.ToString("n2") ;// Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = NossoNumero;// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
            string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
            string cedente = EMPRESATy.NOMECLIENTE;

            //===Dados do seu Cliente (Opcional)===============

            string sacado = "Nome do seu Cliente";
            string endereco1 =  "Endereço do seu Cliente ";
            string endereco2 = "Cidade - MG";

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = string.Empty;
            string instrucoes1 =string.Empty;
            string instrucoes2 = string.Empty;
            string instrucoes3 = string.Empty;
            string instrucoes4 = string.Empty;
            string instrucoes5 = string.Empty;


            string arquivo = "http://www.bmsltda.com.br/boletobancaria/Boleto_SICOOB_PHP/boleto-sicoob.php?data_vencimento=" + data_vencimento + "&agencia=" +
                                agencia + "&digito_agencia=" + digito_agencia + "&codigo_cedente=" + codigo_cedente + "&digito_codigo_cedente=" + digito_codigo_cedente +
                                "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                                "&cpf_cnpj_cedente=" + cpf_cnpj_cedente +
                                "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
                                "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";


            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaUnibanco()
        {
            //Instruções
            Instrucao_Santander item1 = new Instrucao_Santander();
            Instrucao_Santander item3 = new Instrucao_Santander();
            Instrucao_Santander item2 = new Instrucao_Santander();
            Instrucao_Santander item4 = new Instrucao_Santander();
            Instrucao_Santander item5 = new Instrucao_Santander();
            Instrucao_Santander item6 = new Instrucao_Santander();
            Instrucao_Santander item7 = new Instrucao_Santander();
            Instrucao_Santander item8 = new Instrucao_Santander();
            Instrucao_Santander item9 = new Instrucao_Santander();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 409;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaSudameris()
        {
            //Instruções
            Instrucao_Santander item1 = new Instrucao_Santander();
            Instrucao_Santander item3 = new Instrucao_Santander();
            Instrucao_Santander item2 = new Instrucao_Santander();
            Instrucao_Santander item4 = new Instrucao_Santander();
            Instrucao_Santander item5 = new Instrucao_Santander();
            Instrucao_Santander item6 = new Instrucao_Santander();
            Instrucao_Santander item7 = new Instrucao_Santander();
            Instrucao_Santander item8 = new Instrucao_Santander();
            Instrucao_Santander item9 = new Instrucao_Santander();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 347;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaSantander()
        {
            //Instruções
            Instrucao_Santander item1 = new Instrucao_Santander();
            Instrucao_Santander item3 = new Instrucao_Santander();
            Instrucao_Santander item2 = new Instrucao_Santander();
            Instrucao_Santander item4 = new Instrucao_Santander();
            Instrucao_Santander item5 = new Instrucao_Santander();
            Instrucao_Santander item6 = new Instrucao_Santander();
            Instrucao_Santander item7 = new Instrucao_Santander();
            Instrucao_Santander item8 = new Instrucao_Santander();
            Instrucao_Santander item9 = new Instrucao_Santander();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 33;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaSafra()
        {
            //Instruções
            Instrucao_Safra item1 = new Instrucao_Safra();
            Instrucao_Safra item3 = new Instrucao_Safra();
            Instrucao_Safra item2 = new Instrucao_Safra();
            Instrucao_Safra item4 = new Instrucao_Safra();
            Instrucao_Safra item5 = new Instrucao_Safra();
            Instrucao_Safra item6 = new Instrucao_Safra();
            Instrucao_Safra item7 = new Instrucao_Safra();
            Instrucao_Safra item8 = new Instrucao_Safra();
            Instrucao_Safra item9 = new Instrucao_Safra();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 422  ;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaReal()
        {
            //Instruções
            Instrucao_Real item1 = new Instrucao_Real();
            Instrucao_Real item3 = new Instrucao_Real();
            Instrucao_Real item2 = new Instrucao_Real();
            Instrucao_Real item4 = new Instrucao_Real();
            Instrucao_Real item5 = new Instrucao_Real();
            Instrucao_Real item6 = new Instrucao_Real();
            Instrucao_Real item7 = new Instrucao_Real();
            Instrucao_Real item8 = new Instrucao_Real();
            Instrucao_Real item9 = new Instrucao_Real();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();
            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c, new EspecieDocumento(356,"9"));

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 356;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaHSBC()
        {
            //Instruções
            Instrucao_Caixa item1 = new Instrucao_Caixa();
            Instrucao_Caixa item3 = new Instrucao_Caixa();
            Instrucao_Caixa item2 = new Instrucao_Caixa();
            Instrucao_Caixa item4 = new Instrucao_Caixa();
            Instrucao_Caixa item5 = new Instrucao_Caixa();
            Instrucao_Caixa item6 = new Instrucao_Caixa();
            Instrucao_Caixa item7 = new Instrucao_Caixa();
            Instrucao_Caixa item8 = new Instrucao_Caixa();
            Instrucao_Caixa item9 = new Instrucao_Caixa();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 399;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        public void ImprimiBoletaCaixa()
        {
            CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
            CONFIGBOLETATy = CONFIGBOLETAP.Read(5);

            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);


            //Dados para emitir boleto
            string DataVencimentoTeste = DateTime.Now.AddDays(2).ToString();
            string data_vencimento = Convert.ToDateTime(DataVencimentoTeste).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string conta = CONFIGBOLETATy.CONTA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string codcedente = CONFIGBOLETATy.CODCEDENTE.ToString(); // Numero do Convenio 
            string dac_conta = CONFIGBOLETATy.DIGCONTA.TrimEnd().TrimStart(); 	// Digito da Conta Corrente 1 Digito
            string nosso_numero = "000001"; //"3175233"; 	// Nosso Numero
            string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
            string data_documento = DateTime.Now.ToString("dd/MM/yyyy");
            string valor = "1.00"; // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = "000001";// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string cn_pj = Util.LimiterText(Util.RetiraLetras(EMPRESATy.CNPJCPF), 3);
            string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
            string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
            string cedente = EMPRESATy.NOMECLIENTE;

            //===Dados do seu Cliente (Opcional)===============

            string sacado = "Nome do seu Cliente ";
            string endereco1 = "Endereço do seu Cliente ";
            string endereco2 = "Cidade" + "UF";

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = txtInstrucao1.Text;//Instruçoes para o Cliente
            string instrucoes1 = txtInstrucao2.Text; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = txtInstrucao3.Text;
            string instrucoes3 = txtInstrucao4.Text;
            string instrucoes4 = txtInstrucao5.Text;
            string instrucoes5 = txtInstrucao6.Text;


            string arquivo = "http://www.bmsltda.com.br/boletobancaria/Boleto_CEF_PHP/boleto-caixa.php?data_vencimento=" + data_vencimento + "&agencia=" +
                                agencia + "&conta=" + conta + "&digito_agencia=" + digito_agencia + "&codcedente=" + codcedente + "&dac_conta=" + dac_conta +
                                "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                                "&cpf_cnpj_cedente=" + cpf_cnpj_cedente + "&cn_pj=" + cn_pj +
                                "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
                                "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";


            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }


        private void ImprimiBoletaBasa()
        {
            //Instruções
            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5);
            Instrucao_Banrisul item3 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item2 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item4 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item5 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item6 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item7 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item8 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item9 = new Instrucao_Banrisul(81, 10);

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.DIGAGENCIA,
                                   Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 003;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaBRB()
        {
            //Instruções
            Instrucao_BRB item1 = new Instrucao_BRB();
            Instrucao_BRB item3 = new Instrucao_BRB();
            Instrucao_BRB item2 = new Instrucao_BRB();
            Instrucao_BRB item4 = new Instrucao_BRB();
            Instrucao_BRB item5 = new Instrucao_BRB();
            Instrucao_BRB item6 = new Instrucao_BRB();
            Instrucao_BRB item7 = new Instrucao_BRB();
            Instrucao_BRB item8 = new Instrucao_BRB();
            Instrucao_BRB item9 = new Instrucao_BRB();

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.DIGAGENCIA,
                                   Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 070;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaBanrisul()
        {
            //Instruções
            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5);
            Instrucao_Banrisul item3 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item2 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item4 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item5 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item6 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item7 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item8 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item9 = new Instrucao_Banrisul(81, 10);

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.DIGAGENCIA,
                                   Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();
            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c, new EspecieDocumento(341, "1"));

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 041;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo,"");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        //private void ImprimiBoletaItau()
        //{
        //    //Instruções
        //    Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
        //    Instrucao_Itau item3 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item4 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item5 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item6 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item7 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item8 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item9 = new Instrucao_Itau(81, 10);

        //    Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.CONTA);
        //    //Na carteira 198 o código do Cedente é a conta bancária

        //    if (txtConvenio.Text != string.Empty)
        //        c.Convenio = Convert.ToInt32(Entity.CONVENIO);

        //    if (txtCodCedente.Text != string.Empty)
        //        c.Codigo = Entity.CODCEDENTE.ToString();         

        //    string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();
        //    Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
        //                          Entity.CARTEIRA, NossoNumero, c, new EspecieDocumento(341, "1"));

        //    if (txtTipoModalidade.Text != string.Empty)
        //        b.TipoModalidade = Entity.TIPOMODALIDADE;

        //    b.NumeroDocumento = "00000001";
        //    b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
        //    b.Sacado.Endereco.End = "Endereço do seu Cliente ";
        //    b.Sacado.Endereco.Bairro = "Bairro";
        //    b.Sacado.Endereco.Cidade = "Cidade";
        //    b.Sacado.Endereco.CEP = "00000000";
        //    b.Sacado.Endereco.UF = "UF";

        //    // Exemplo de como adicionar mais informações ao sacado
        //    b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

        //    item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
        //    b.Instrucoes.Add(item1);
        //    item1.Descricao = txtInstrucao1.Text;
        //    b.Instrucoes.Add(item2);
        //    item2.Descricao = txtInstrucao2.Text;
        //    b.Instrucoes.Add(item3);
        //    item3.Descricao = txtInstrucao3.Text;
        //    b.Instrucoes.Add(item4);
        //    item4.Descricao = txtInstrucao4.Text;
        //    b.Instrucoes.Add(item5);
        //    item5.Descricao = txtInstrucao5.Text;
        //    b.Instrucoes.Add(item6);
        //    item6.Descricao = txtInstrucao6.Text;
        //    b.Instrucoes.Add(item7);
        //    item7.Descricao = txtInstrucao7.Text;
        //    b.Instrucoes.Add(item8);
        //    item8.Descricao = txtInstrucao8.Text;
        //    b.Instrucoes.Add(item9);
        //    item9.Descricao = txtInstrucao9.Text;

        //    BoletoBancario BolB = new BoletoBancario();
        //    BolB.CodigoBanco = 341;
        //    BolB.ID = "BolB";

        //    BolB.Boleto = b;
        //    BolB.Boleto.Valida();
            
        //    string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
        //    BolB.MontaHtml(arquivo, "");
        //    BolB.MontaHtmlNoArquivoLocal(arquivo);

        //    using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
        //    {
        //        frm.ShowDialog();
        //    }

        //}

        public void ImprimiBoletaItau()
        {
            CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
            CONFIGBOLETATy = CONFIGBOLETAP.Read(3);

            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);


            //Dados para emitir boleto
            string DataVencimentoTeste = DateTime.Now.AddDays(2).ToString();
            string data_vencimento = Convert.ToDateTime(DataVencimentoTeste).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string conta = CONFIGBOLETATy.CONTA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string codcedente = CONFIGBOLETATy.CODCEDENTE.ToString(); // Numero do Convenio 
            string digito_conta = CONFIGBOLETATy.DIGCONTA.TrimEnd().TrimStart(); 	// Digito da Conta Corrente 1 Digito
            string nosso_numero = "000001"; //"3175233"; 	// Nosso Numero
            string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
            string data_documento = DateTime.Now.ToString("dd/MM/yyyy");
            string valor = "1.00"; // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = "000001";// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string cn_pj = Util.LimiterText(Util.RetiraLetras(EMPRESATy.CNPJCPF), 3);
            string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
            string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
            string cedente = EMPRESATy.NOMECLIENTE;

            //===Dados do seu Cliente (Opcional)===============

            string sacado = "Nome do seu Cliente ";
            string endereco1 = "Endereço do seu Cliente ";
            string endereco2 = "Cidade" + "UF";

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = txtInstrucao1.Text;//Instruçoes para o Cliente
            string instrucoes1 = txtInstrucao2.Text; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = txtInstrucao3.Text;
            string instrucoes3 = txtInstrucao4.Text;
            string instrucoes4 = txtInstrucao5.Text;
            string instrucoes5 = txtInstrucao6.Text;


            string arquivo = "http://www.bmsltda.com.br/boletobancaria/Boleto_ITAU_PHP/boleto-itau.php?data_vencimento=" + data_vencimento + "&agencia=" +
                           agencia + "&conta=" + conta + "&digito_conta=" + digito_conta + "&codcedente=" + codcedente + 
                           "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                           "&cpf_cnpj_cedente=" + cpf_cnpj_cedente + "&cn_pj=" + cn_pj +
                           "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
                           "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";



            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }

        private void ImprimiBoletaBrasil()
        {
            //Instruções
            Instrucao_BancoBrasil item1 = new Instrucao_BancoBrasil(9, 5);
            Instrucao_BancoBrasil item3 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item2 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item4 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item5 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item6 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item7 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item8 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item9 = new Instrucao_BancoBrasil(81, 10);

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.DIGAGENCIA,
                                    Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();
            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 001;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }

        }

        private void ImprimiBoletaBradesco()
        {
            //Instruções

            Instrucao_Bradesco item1 = new Instrucao_Bradesco(9, 5);
            Instrucao_Bradesco item3 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item2 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item4 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item5 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item6 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item7 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item8 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item9 = new Instrucao_Bradesco(81, 10);

            Cedente c = new Cedente(Entity.CPFCNPJCEDENTE, Entity.NOMECEDENTE, Entity.AGENCIA, Entity.DIGAGENCIA,
                                    Entity.CONTA, Entity.DIGCONTA);

            if (txtConvenio.Text != string.Empty)
                c.Convenio = Convert.ToInt32(Entity.CONVENIO);

            if (txtCodCedente.Text != string.Empty)
                c.Codigo = Entity.CODCEDENTE.ToString();

            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();
            Boleto b = new Boleto(Convert.ToDateTime(DataVencimentoTeste), Convert.ToDecimal(ValorTeste),
                                  Entity.CARTEIRA, NossoNumero, c);

            if (txtTipoModalidade.Text != string.Empty)
                b.TipoModalidade = Entity.TIPOMODALIDADE;

            b.NumeroDocumento = "00000001";
            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            item1.Descricao = txtInstrucao1.Text;
            b.Instrucoes.Add(item2);
            item2.Descricao = txtInstrucao2.Text;
            b.Instrucoes.Add(item3);
            item3.Descricao = txtInstrucao3.Text;
            b.Instrucoes.Add(item4);
            item4.Descricao = txtInstrucao4.Text;
            b.Instrucoes.Add(item5);
            item5.Descricao = txtInstrucao5.Text;
            b.Instrucoes.Add(item6);
            item6.Descricao = txtInstrucao6.Text;
            b.Instrucoes.Add(item7);
            item7.Descricao = txtInstrucao7.Text;
            b.Instrucoes.Add(item8);
            item8.Descricao = txtInstrucao8.Text;
            b.Instrucoes.Add(item9);
            item9.Descricao = txtInstrucao9.Text;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 237;
            BolB.ID = "BolB";

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo, "");
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }

        }

        private void txtCodCedente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para alguns bancos o código do Cedente é obrigatório.";
        }       
       
        private void txtConvenio_Enter(object sender, EventArgs e)
        {
            if (NumeroBancoTeste == "001")//Banco do Brasil
                if (txtCarteira.Text == "16" || txtCarteira.Text == "18")
                    lblObsField.Text = "Para carteira 16 ou 18, se necessário o número do convênio deve ser de 6 posições.";
        }

        private void txtTipoModalidade_Enter(object sender, EventArgs e)
        {
            if (NumeroBancoTeste == "001")//Banco do Brasil
                if (txtCarteira.Text == "16" || txtCarteira.Text == "18")
                    lblObsField.Text = "Para carteira 16 ou 18, se necessário informar o tipo da modalidade 21.";
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_CONFIGBOLETAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_CONFIGBOLETAColl[indice].IDCONFIGBOLETA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CONFIGBOLETAP.Read(CodigoSelect);

                    tabControlBoleta.SelectTab(0);
                    txtCarteira.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CONFIGBOLETAP.Delete(CodigoSelect);
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

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void txtTaxaBancaria_Enter(object sender, EventArgs e)
        {
            if (txtTaxaBancaria.Text == "0,00")
                txtTaxaBancaria.Text = string.Empty;
        }

        private void txtTaxaBancaria_Validating(object sender, CancelEventArgs e)
        {
            if (txtTaxaBancaria.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTaxaBancaria.Text))
                {
                    errorProvider1.SetError(txtTaxaBancaria, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtTaxaBancaria.Text);
                    txtTaxaBancaria.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTaxaBancaria, "");
                }
            }
            else
            {
                txtTaxaBancaria.Text = "0,00";
                errorProvider1.SetError(txtTaxaBancaria, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
