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
using System.Collections;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using BMSworks.IMEXAppClass;

namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmTransportadora : Form
    {
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        TRANSPORTADORAProvider TransportadorP = new TRANSPORTADORAProvider();
        TRANSPORTADORAIMEXAPPProvider TRANSPORTADORAIMEXAPPP = new TRANSPORTADORAIMEXAPPProvider();
        ENDERECOIMEXAPPProvider ENDERECOIMEXAPPP = new ENDERECOIMEXAPPProvider();

        TRANSPORTADORACollection TransportadorColl = new TRANSPORTADORACollection();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection(); 
        Utility Util = new Utility();

        public FrmTransportadora()
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

        public int _IDTRANSPORTADORA = -1;
        public TRANSPORTADORAEntity Entity
        {
            get
            {
               string NOME = txtNome.Text.TrimEnd().TrimStart();
               string NOMEFANTASIA = txtNomeFantasia.Text.TrimEnd().TrimStart();

               string DATACADASTRO = DateTime.Now.ToString("dd/MM/yyyy");
           
               
               if (maskedtxtDataCadastro.Text != "  /  /")
                    DATACADASTRO = maskedtxtDataCadastro.Text;  
               

               string TELEFONE1 = txtTelefone1.Text;
               string TELEFONE2 = txtTelefone2.Text;
               string FAX = txtFax.Text;
               string CNPJ = maskedtxtCNPJ.Text;
               string IE = txtIE.Text;
               string ENDERECO = txtEndereco.Text;
               string BAIRRO =txtBairro.Text; 
               string CIDADE = txtCidade.Text;
               string UF = cbUF.Text;
               string CEP = mktxtCep.Text;
               string OBSERVACAO = txtObservacao.Text;
               string SITE = txtSite.Text;
               string CODANTT = txtANTT.Text;

               return new TRANSPORTADORAEntity(_IDTRANSPORTADORA, NOME, NOMEFANTASIA, Convert.ToDateTime(DATACADASTRO), TELEFONE1,
                                                TELEFONE2, FAX, CNPJ, IE, ENDERECO, BAIRRO, CIDADE, UF,
                                                CEP, OBSERVACAO, SITE, CODANTT);
            }
            set
            {

                if (value != null)
                {
                    _IDTRANSPORTADORA = value.IDTRANSPORTADORA;
                    txtNome.Text = value.NOME;
                    txtNomeFantasia.Text = value.NOMEFANTASIA;
                    maskedtxtDataCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");
                    txtTelefone1.Text = value.TELEFONE1;
                    txtTelefone2.Text = value.TELEFONE2;
                    txtFax.Text = value.FAX;
                    maskedtxtCNPJ.Text = value.CNPJ;
                    txtIE.Text = value.IE;
                    txtEndereco.Text = value.ENDERECO;
                    txtBairro.Text = value.BAIRRO;
                    txtCidade.Text = value.CIDADE;
                    cbUF.SelectedValue = value.UF;
                    mktxtCep.Text = value.CEP;
                    txtObservacao.Text = value.OBSERVACAO;
                    txtSite.Text = value.SITE;
                    txtANTT.Text = value.CODANTT;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDTRANSPORTADORA = -1;
                    txtNome.Text = string.Empty;
                    txtNomeFantasia.Text = string.Empty;
                    maskedtxtDataCadastro.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    txtTelefone2.Text = string.Empty;
                    txtFax.Text = string.Empty;
                    maskedtxtCNPJ.Text = string.Empty;
                    txtIE.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    txtCidade.Text = string.Empty;
                    mktxtCep.Text = string.Empty;
                    cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect);
                    txtObservacao.Text = string.Empty;
                    txtSite.Text = string.Empty;
                    txtANTT.Text = string.Empty;
                    errorProvider1.Clear();
                }


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
        }

        private void FrmTransportadora_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            GetUFDrop();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            lblObsField.ForeColor = ConfigSistema1.Default.ColorFieldObs;

            if (_IDTRANSPORTADORA != -1)
                Entity = TransportadorP.Read(_IDTRANSPORTADORA);

            CONFISISTEMATy = CONFISISTEMAP.Read(1);

            this.Cursor = Cursors.Default;
            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlTransportadora.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlTransportadora.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlTransportadora.SelectTab(0);
            txtNome.Focus();
            
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void Grava()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                if (Validacoes())
                {
                  //Verificar CNPJ existe para novos cadastros
                    _IDTRANSPORTADORA = TransportadorP.Save(Entity);
                    SalveIMEXAPP(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    btnPesquisa_Click(null, null);
                    this.Cursor = Cursors.Default;
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        //Salva a transportadora no IMEX App
        private void SalveIMEXAPP(TRANSPORTADORAEntity TRANSPORTADORATy)
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    TRANSPORTADORAIMEXAPPEntity TRANSPORTADORAIMEXAPPtY = new TRANSPORTADORAIMEXAPPEntity();
                    TRANSPORTADORAIMEXAPPtY.IDTRANSPORTADORA = null;
                    TRANSPORTADORAIMEXAPPtY.XMEUID = TRANSPORTADORATy.IDTRANSPORTADORA.ToString();
                    TRANSPORTADORAIMEXAPPtY.XRAZAOSOCIAL = TRANSPORTADORATy.NOME;
                    TRANSPORTADORAIMEXAPPtY.XFANTASIA = TRANSPORTADORATy.NOMEFANTASIA;
                    TRANSPORTADORAIMEXAPPtY.XCNPJ = TRANSPORTADORATy.CNPJ;
                    TRANSPORTADORAIMEXAPPtY.XIE = TRANSPORTADORATy.IE;
                    TRANSPORTADORAIMEXAPPtY.XANOTACAO = TRANSPORTADORATy.OBSERVACAO;
                    TRANSPORTADORAIMEXAPPtY.XEMAILS = "";
                    TRANSPORTADORAIMEXAPPtY.XTELEFONES = TRANSPORTADORATy.TELEFONE1 + " " + TRANSPORTADORATy.TELEFONE2;
                   
                    TRANSPORTADORAIMEXAPPP.Save(TRANSPORTADORAIMEXAPPtY);

                    //Salva O endereço da transportadora
                    SalveIMEXAPP2(TRANSPORTADORATy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        //Salva o Endereço no IMEX App
        private void SalveIMEXAPP2(TRANSPORTADORAEntity TRANSPORTADORATy)
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    ENDERECOIMEXAPPEntity ENDERECOIMEXAPPTy = new ENDERECOIMEXAPPEntity();
                    ENDERECOIMEXAPPTy.IDENDERECO = null;
                    ENDERECOIMEXAPPTy.XMEUID = TRANSPORTADORATy.IDTRANSPORTADORA.ToString();
                    ENDERECOIMEXAPPTy.IDTRANSPORTADORA = TRANSPORTADORAIMEXAPPP.GetID(Convert.ToInt32(TRANSPORTADORATy.IDTRANSPORTADORA));
                    ENDERECOIMEXAPPTy.IDCLIENTE = null;
                    ENDERECOIMEXAPPTy.XCEP = TRANSPORTADORATy.CEP;
                    ENDERECOIMEXAPPTy.XENDERECO = TRANSPORTADORATy.ENDERECO;
                    ENDERECOIMEXAPPTy.CNUMERO = 0;
                    ENDERECOIMEXAPPTy.XCOMPLEMENTO = "";
                    ENDERECOIMEXAPPTy.XBAIRRO = TRANSPORTADORATy.BAIRRO;
                    ENDERECOIMEXAPPTy.XCIDADE = TRANSPORTADORATy.CIDADE;
                    ENDERECOIMEXAPPTy.XESTADO = TRANSPORTADORATy.UF;
                    ENDERECOIMEXAPPTy.XESTADO = TRANSPORTADORATy.UF;
                    ENDERECOIMEXAPPTy.IDCLIENTE = null;
                    ENDERECOIMEXAPPTy.XESTADO = TRANSPORTADORATy.UF;
                    ENDERECOIMEXAPPTy.IDREPRESENTADA = null;
                    ENDERECOIMEXAPPP.Save(ENDERECOIMEXAPPTy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtNomeFantasia.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }            
            else if (maskedtxtCNPJ.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label42, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");


             return result;
        }         

        private void GetUFDrop()
        {
           LIS_ESTADOProvider LIS_ESTADOP = new LIS_ESTADOProvider();

            cbUF.DisplayMember = "UF";
            cbUF.ValueMember = "UF";
            cbUF.DataSource = LIS_ESTADOP.ReadCollectionByParameter(null, "UF");

            cbUF.SelectedIndex = cbUF.FindStringExact(ConfigSistema1.Default.UFSelect); 
        }

        private void txtNome_Validating(object sender, CancelEventArgs e)
        {
            if (txtNome.Text.Trim().Length == 0)
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
            else
                errorProvider1.SetError(txtNome, "");
          
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();
             
                TransportadorColl = TransportadorP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = TransportadorColl;

                lblTotalPesquisa.Text = TransportadorColl.Count.ToString();
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
                if (TransportadorColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = TransportadorColl;
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

                    TransportadorColl = TransportadorP.ReadCollectionByParameter(Filtro);
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = TransportadorColl;

                    lblTotalPesquisa.Text = TransportadorColl.Count.ToString();
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
            TransportadorColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = TransportadorColl.Count.ToString();
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TransportadorColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(orderBy);

                    TransportadorColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = TransportadorColl;
                }
            }
        }
      
        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
            
        }

        private void Delete()
        {
            if (_IDTRANSPORTADORA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlTransportadora.SelectTab(1);
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
                        TransportadorP.Delete(_IDTRANSPORTADORA);
                        DeleteIMEXAPP(_IDTRANSPORTADORA);
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

        private void DeleteIMEXAPP(int IDREGISTRO)
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                   int result = TRANSPORTADORAIMEXAPPP.GetID(IDREGISTRO);
                    TRANSPORTADORAIMEXAPPP.Delete(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);


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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Transportadoras");
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
                MessageBox.Show( ConfigMessage.Default.MsgErroPrint);

            }
        }
       
        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintListaGeral();

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
                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("CNPJ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
                e.Graphics.DrawString("Telefone", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Cidade", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("UF", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = TransportadorColl.Count;

                while (IndexRegistro < TransportadorColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(TransportadorColl[IndexRegistro].IDTRANSPORTADORA.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(TransportadorColl[IndexRegistro].NOME, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(TransportadorColl[IndexRegistro].CNPJ, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(TransportadorColl[IndexRegistro].TELEFONE1, 12), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(TransportadorColl[IndexRegistro].CIDADE, 27), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(TransportadorColl[IndexRegistro].UF, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 730, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < TransportadorColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + TransportadorColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
                
                MessageBox.Show( ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlTransportadora.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            PrintListaGeral();
        }

        private void PrintListaGeral()
        {
            if (TransportadorColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlTransportadora.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUF_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void maskedtxtCNPJ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CPF/CNPJ da transportadora, não será possível cadastrar CPF/CNPJ inválido.";
        }       

        private void FrmTransportadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmTransportadora_KeyDown(object sender, KeyEventArgs e)
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
             if (TransportadorColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(TransportadorColl[indice].IDTRANSPORTADORA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = TransportadorP.Read(CodigoSelect);

                    tabControlTransportadora.SelectTab(0);
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
                            TransportadorP.Delete(CodigoSelect);
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

        private void maskedtxtCNPJ_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
             if(maskedtxtCNPJ.Text.Length > 15)
             {
                if (!ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CNPJErro);
                    errorProvider1.SetError(this, ConfigMessage.Default.CNPJErro);               
                }
             }
             else  if (!ValidacoesLibrary.ValidaCPF(maskedtxtCNPJ.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.CPFErro);
                    errorProvider1.SetError(maskedtxtCNPJ, ConfigMessage.Default.CPFErro);
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
                frm.TituloSelec = "Relação de Transportadoras";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (TransportadorColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(TransportadorColl[rowindex].IDTRANSPORTADORA);
                    Entity = TransportadorP.Read(CodigoSelect);

                    tabControlTransportadora.SelectTab(0);
                    txtNome.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + TransportadorColl[rowindex].NOME,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(TransportadorColl[rowindex].IDTRANSPORTADORA);
                                //Delete Pedido
                                TransportadorP.Delete(CodigoSelect);
                                DeleteIMEXAPP(_IDTRANSPORTADORA);
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

        
       
           

    }
}