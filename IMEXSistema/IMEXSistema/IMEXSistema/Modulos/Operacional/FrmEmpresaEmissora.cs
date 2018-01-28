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
using winfit.Modulos.Outros;
using BmsSoftware.Modulos;
using BmsSoftware.Modulos.Financeiro;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using System.Net;
using System.Xml;
using System.Net.Mail;


namespace BMSSoftware.Modulos.Operacional
{
    public partial class FrmEmpresaEmissora : Form
    {
        Utility Util = new Utility();

        LIS_EMPRESAEMISSORACollection LIS_EMPRESAEMISSORAColl = new LIS_EMPRESAEMISSORACollection();
        LIS_EMPRESAEMISSORAProvider LIS_EMPRESAEMISSORAP = new LIS_EMPRESAEMISSORAProvider();
        EMPRESAEMISSORAProvider EMPRESAEMISSORAP = new EMPRESAEMISSORAProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        string MensagemErroBloqueio = string.Empty;

        public FrmEmpresaEmissora()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int CodClienteSelec = -1;

        int _IDEMPRESAEMISSORA = -1;
        int _COD_MUN_IBGE = -1;
        public EMPRESAEMISSORAEntity Entity
        {
            get
            {
                   string RAZAOSOCIAL  = txtNome.Text; //      VARCHAR(50),
                   string NOMEFANTASIA = txtNomeFantasia.Text; //       VARCHAR(50),
                   string  TELEFONE = txtTelefone1.Text;    //        VARCHAR(20),
                   string  CNPJ = maskedtxtCNPJ.Text;        //        VARCHAR(20),
                   string IE=txtIE.Text;            //       VARCHAR(20),
                   string EMAIL =  txtEmailCliente.Text;      //       VARCHAR(50),
                   string ENDERECO =  txtEnd1.Text;   //       VARCHAR(100),
                   string NUMERO  =  txtNumEndereco.Text;    //       VARCHAR(20),
                   string COMPLEMENTO =  txtComplemento1.Text;//       VARCHAR(20),
                   string BAIRRO   =  txtBairro.Text;   //       VARCHAR(20),
                   string CEP  =    mktxtCep1.Text;     //       VARCHAR(10),
                   string IMUNICIPAL = txtInscMunicipal.Text;  //       VARCHAR(20),
                   string CRT   =  txtCRT.Text;      //       VARCHAR(1),
                   string IEST =   txtIEST.Text;      //       VARCHAR(20),
                   string CNAE =  TxtCNAE.Text;       //       VARCHAR(30),
                   string NOMECERTIFICADO = txtNameCertDigital.Text;//    VARCHAR(100),
                   string SERIACERTIFICADO= txtSerialCertDigital.Text; //   VARCHAR(100),
                   string VALIDADECERTIFICADO = txtValidadeCertDigital.Text; //VARCHAR(100),

                   return new EMPRESAEMISSORAEntity(_IDEMPRESAEMISSORA, RAZAOSOCIAL, NOMEFANTASIA, TELEFONE, CNPJ,
                                                    IE, EMAIL, ENDERECO, NUMERO, COMPLEMENTO, BAIRRO, CEP, IMUNICIPAL,
                                                    CRT, IEST, CNAE, NOMECERTIFICADO, SERIACERTIFICADO, VALIDADECERTIFICADO, _COD_MUN_IBGE);

        
            }
            set
            {
                if (value != null)
                {
                    _IDEMPRESAEMISSORA = value.IDEMPRESAEMISSORA;
                    txtNome.Text = value.RAZAOSOCIAL;
                    txtNomeFantasia.Text = value.NOMEFANTASIA;
                    txtTelefone1.Text = value.TELEFONE;
                    maskedtxtCNPJ.Text= value.CNPJ;
                    txtIE.Text = value.IE;
                    txtEmailCliente.Text = value.EMAIL;
                    txtEnd1.Text = value.ENDERECO;
                    txtNumEndereco.Text = value.NUMERO;
                    txtComplemento1.Text = value.COMPLEMENTO;
                    txtBairro.Text = value.BAIRRO;
                    mktxtCep1.Text = value.CEP;
                    txtInscMunicipal.Text = value.IMUNICIPAL;
                    txtCRT.Text = value.CRT;
                    txtIEST.Text = value.IEST;
                    TxtCNAE.Text = value.CNAE;
                    txtNameCertDigital.Text = value.NOMECERTIFICADO;
                    txtSerialCertDigital.Text = value.SERIACERTIFICADO;
                    txtValidadeCertDigital.Text = value.VALIDADECERTIFICADO;

                    //Busca a cidade
                    LIS_EMPRESAEMISSORACollection LIS_EMPRESAEMISSORAColl = new LIS_EMPRESAEMISSORACollection();
                    LIS_EMPRESAEMISSORAProvider LIS_EMPRESAEMISSORAP = new LIS_EMPRESAEMISSORAProvider();
                    RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                    RowRelatorioCliente.Add(new RowsFiltro("IDEMPRESAEMISSORA", "System.Int32", "=", _IDEMPRESAEMISSORA.ToString()));
                    LIS_EMPRESAEMISSORAColl = LIS_EMPRESAEMISSORAP.ReadCollectionByParameter(RowRelatorioCliente);
                    if (LIS_EMPRESAEMISSORAColl.Count > 0)
                    {
                        txtCidade1.Text = LIS_EMPRESAEMISSORAColl[0].MUNICIPIO;
                        txtUF1.Text = LIS_EMPRESAEMISSORAColl[0].UF;
                        _COD_MUN_IBGE = Convert.ToInt32(value.COD_MUN_IBGE);
                    }

                    errorProvider1.Clear();
                }
                else
                {
                    _IDEMPRESAEMISSORA = -1;
                    txtNome.Text = string.Empty;
                    txtNomeFantasia.Text = string.Empty;
                    txtTelefone1.Text = string.Empty;
                    maskedtxtCNPJ.Text = "  .   .   /    -"; 
                    txtIE.Text = string.Empty;
                    txtEmailCliente.Text = string.Empty;
                    txtEnd1.Text = string.Empty;
                    txtNumEndereco.Text = string.Empty;
                    txtComplemento1.Text = string.Empty;
                    txtBairro.Text = string.Empty;
                    mktxtCep1.Text = string.Empty;
                    txtInscMunicipal.Text = string.Empty;
                    txtCRT.Text = string.Empty;
                    txtIEST.Text = string.Empty;
                    TxtCNAE.Text = string.Empty; ;
                    txtNameCertDigital.Text = string.Empty;
                    txtSerialCertDigital.Text = string.Empty;
                    txtValidadeCertDigital.Text = string.Empty;

                    //Busca a cidade
                    LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                    LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                    EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", EMPRESAP.Read(1).CIDADE.Replace("'", "")));
                    RowRelatorio.Add(new RowsFiltro("uf", "System.String", "=", EMPRESAP.Read(1).UF));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_MUNICIPIOSColl.Count > 0)
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

                    errorProvider1.Clear();
                    txtNome.Focus();
                    
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

            if (_IDEMPRESAEMISSORA != -1)
                Entity = EMPRESAEMISSORAP.Read(_IDEMPRESAEMISSORA);
            

            this.Cursor = Cursors.Default;

            VerificaAcesso();

            cbCamposPesquisa.SelectedIndex = 2;
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";
            cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        } 

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
            cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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
            
        }

        private void cbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbUF2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void cbUF2_KeyDown(object sender, KeyEventArgs e)
        {
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
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void maskedtxtCNPJ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "CNPJ do cliente, não será possível cadastrar CNPJ inválido.";
        }
       
        private void makCPFConjuge_Enter(object sender, EventArgs e)
        {
        }

        private void maskedtxtDataAd_Enter(object sender, EventArgs e)
        {
            
        }

        private void mskDtAdmissao_Enter(object sender, EventArgs e)
        {
            
        }

        private void maskDtaNascConjuge_Enter(object sender, EventArgs e)
        {
            
        }

        private void mskDtAdmissao_Leave(object sender, EventArgs e)
        {
           
        }

        private void mskDataRetorno_Enter(object sender, EventArgs e)
        {
            
        }

        private void TxtRenda_Enter(object sender, EventArgs e)
        {
            
        }

        private void TxtCredito_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtRGConjuge_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox77_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox73_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox78_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox80_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox81_Enter(object sender, EventArgs e)
        {
            
        }
       
        private void gravaToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                  
                _IDEMPRESAEMISSORA = EMPRESAEMISSORAP.Save(Entity);
                    this.Cursor = Cursors.Default;
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                  
                }               

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

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
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            if (maskedtxtCNPJ.Text == "  .   .   /    -" || !ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
            {
                Util.ExibirMSg(ConfigMessage.Default.CNPJErro, "Red");
                errorProvider1.SetError(label15, ConfigMessage.Default.CNPJErro);
                result = false;
            }
            else if (txtCidade1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCidade1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }
            else if (txtNumEndereco.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label30, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }
            else if (txtBairro.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label28, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            } 
            else if (txtCRT.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(CRT, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }
            else if (TxtCNAE.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }                
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (_COD_MUN_IBGE == -1)
            {
                errorProvider1.SetError(label27, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
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

                LIS_EMPRESAEMISSORAColl = LIS_EMPRESAEMISSORAP.ReadCollectionByParameter(Filtro, "RAZAOSOCIAL");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_EMPRESAEMISSORAColl;

                lblTotalPesquisa.Text = LIS_EMPRESAEMISSORAColl.Count.ToString();
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
            try
            {
                // referente ao tipo de campo
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_EMPRESAEMISSORAColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_EMPRESAEMISSORAColl;
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

                    LIS_EMPRESAEMISSORAColl = LIS_EMPRESAEMISSORAP.ReadCollectionByParameter(Filtro, "RAZAOSOCIAL");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_EMPRESAEMISSORAColl;

                    lblTotalPesquisa.Text = LIS_EMPRESAEMISSORAColl.Count.ToString();
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

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
            if (_IDEMPRESAEMISSORA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlCliente.SelectTab(1);
            }
            else if (_IDEMPRESAEMISSORA == 1)
            {
                string msgerro = "O codigo 1 não pode ser excluído!";
                Util.ExibirMSg(msgerro, "Red");
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
                       
                        EMPRESAEMISSORAP.Delete(_IDEMPRESAEMISSORA);

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
            LIS_EMPRESAEMISSORAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_EMPRESAEMISSORAColl.Count.ToString();
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
            if (LIS_EMPRESAEMISSORAColl.Count == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
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
            if (LIS_EMPRESAEMISSORAColl.Count == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
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
                    Util.ExibirMSg(ConfigMessage.Default.CNPJErro, "Red");
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
           
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            
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
            if (LIS_EMPRESAEMISSORAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_EMPRESAEMISSORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_EMPRESAEMISSORAEntity>(orderBy);

                    LIS_EMPRESAEMISSORAColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_EMPRESAEMISSORAColl;
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
            if (LIS_EMPRESAEMISSORAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_EMPRESAEMISSORAColl[indice].IDEMPRESAEMISSORA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = EMPRESAEMISSORAP.Read(CodigoSelect);

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
                            EMPRESAEMISSORAP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
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
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
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
            
        }

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }        

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_EMPRESAEMISSORAColl.Count == 0)
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


        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            //foreach (LLIS_EMPRESAEMISSOREntity item in LIS_EMPRESAEMISSORAP)
            //{
            //    if (NomeCampo == "TOTALPEDIDO")
            //        valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
            //    else if (NomeCampo == "TOTALPRODUTOS")
            //        valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
            //    else if (NomeCampo == "VALORPAGO")
            //        valortotal += Convert.ToDecimal(item.VALORPAGO);
            //    else if (NomeCampo == "VALORDEVEDOR")
            //        valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
            //    else if (NomeCampo == "VALORDESCONTO")
            //        valortotal += Convert.ToDecimal(item.VALORDESCONTO);
            //    else if (NomeCampo == "VALORACRESCIMO")
            //        valortotal += Convert.ToDecimal(item.VALORACRESCIMO);
            //    else if (NomeCampo == "VALORCOMISSAO")
            //        valortotal += Convert.ToDecimal(item.VALORCOMISSAO);
            //}

            return valortotal;
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

      
        private void printDocument4_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
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
                frm.EnderecoSelecionado = txtEnd1.Text;
                frm.CidadSelecionado = txtCidade1.Text;
                frm.UFSelecionado = txtUF1.Text;
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
                    frm.EnderecoSelecionado = txtEnd1.Text;
                    frm.CidadSelecionado = txtCidade1.Text;
                    frm.UFSelecionado = txtUF1.Text;
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
           
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
             
        }    

    
     
        private void EMPRESAEMISSORAPorFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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

       private void txtCidadeEntrega_Enter(object sender, EventArgs e)
       {
          
       }

       private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
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
               frm.TituloSelec = "Relação de Clientes";
               frm.NometelaSelec = this.Name;
               frm.DataGridExport = DataGriewDados;
               frm.ShowDialog();
           }
       }

       private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
       }

       private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
       {
           using (FrmTabelConsultaSerasa frm = new FrmTabelConsultaSerasa())
           {
               frm.ShowDialog();
           }
       }

       private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
       {
           using (FrmTabelConsultaSerasa frm = new FrmTabelConsultaSerasa())
           {
               frm.ShowDialog();
           }
       }

       private void consultaSerasaToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
       }

       private void pesquisarOEMPRESAEMISSORAPelaPlacaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           
       }

       private void únicoToolStripMenuItem1_Click(object sender, EventArgs e)
       {
          
       }

       private void emLoteToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
       }

       private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
       {

           int rowindex = e.RowIndex;
           if (LIS_EMPRESAEMISSORAColl.Count > 0 && rowindex > -1)
           {
               int ColumnSelecionada = e.ColumnIndex;
               int CodigoSelect = -1;

               if (ColumnSelecionada == 0)//Editar
               {
                   CodigoSelect = Convert.ToInt32(LIS_EMPRESAEMISSORAColl[rowindex].IDEMPRESAEMISSORA);
                   Entity = EMPRESAEMISSORAP.Read(CodigoSelect);

                   tabControlCliente.SelectTab(0);
                   txtNome.Focus();

               }
               else if (ColumnSelecionada == 1)//Excluir
               {

                   if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                   {
                       DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_EMPRESAEMISSORAColl[rowindex].NOMEFANTASIA,
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                       if (dr == DialogResult.Yes)
                       {
                           try
                           {
                               
                               CodigoSelect = Convert.ToInt32(LIS_EMPRESAEMISSORAColl[rowindex].IDEMPRESAEMISSORA);
                               EMPRESAEMISSORAP.Delete(CodigoSelect);

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

       private void tabPage2_Enter(object sender, EventArgs e)
       {
           
       }

       private void button1_Click(object sender, EventArgs e)
       {
         
       }

       private void button2_Click(object sender, EventArgs e)
       {
          
       }

       private void button5_Click(object sender, EventArgs e)
       {
       }

       private void button6_Click(object sender, EventArgs e)
       {
          
       }

       private void uploadDeSicronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
       {
          			 
       }

       private void extratoDeContasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
       {
          
       }

       private void button6_Click_1(object sender, EventArgs e)
       {
          
       }

       private void button7_Click(object sender, EventArgs e)
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
                RowRelatorio.Add(new RowsFiltro("RAZAOSOCIAL", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFANTASIA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%","or"));
                RowRelatorio.Add(new RowsFiltro("ENDERECO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("EMAIL", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CNPJ", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CPF", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("TELEFONE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_EMPRESAEMISSORAColl = LIS_EMPRESAEMISSORAP.ReadCollectionByParameter(RowRelatorio, "NOME");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_EMPRESAEMISSORAColl;
                    lblTotalPesquisa.Text = LIS_EMPRESAEMISSORAColl.Count.ToString();
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
               MessageBox.Show("Erro técnico: "+ ex.Message,
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

        private void button3_Click_1(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void btcadvendedor_Click(object sender, EventArgs e)
        {
           
        }     

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void btnCertDigital_Click(object sender, EventArgs e)
        {

        }

        private void rbProducaoNFe_Click(object sender, EventArgs e)
        {

        }

        private void txtCRT_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "1 – Simples Nacional; 2 – Simples Nacional – excesso de sublimite de receita bruta; 3 – Regime Normal";
        }

        private void btnCertDigital_Click_1(object sender, EventArgs e)
        {
            SelecionarCertificado();
        }

        public void SelecionarCertificado()
        {
            X509Certificate2 oCertificado;
            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
                MessageBox.Show(msgResultado, "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                oX509Cert = scollection[0];
                oCertificado = oX509Cert;
                txtNameCertDigital.Text = oCertificado.IssuerName.Name;
                txtSerialCertDigital.Text = oCertificado.SerialNumber;
                txtValidadeCertDigital.Text = oCertificado.NotBefore + " à " + oCertificado.NotAfter;
            }
        }

        private void btnAtivaNfe_Click(object sender, EventArgs e)
        {
            if (_IDEMPRESAEMISSORA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlCliente.SelectTab(1);
            }
            else if(!VerificaUso3())
            {
                if (MensagemErroBloqueio.Length < 1)
                    MensagemErroBloqueio = "Erro ao Validar Empresa Emissora!";

                MessageBox.Show(MensagemErroBloqueio);
            }
            else
            {
                AtivaEmpresaEmissoraNFe();
            }
        }

        private Boolean VerificaUso3()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string sCaminhoDoArquivo = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\securitysoftware3.xml";
                Util.BaixarArquivoFTP("registros/securitysoftware3.xml", sCaminhoDoArquivo); 

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                xmlDoc.LoadXml(xmlDoc.InnerXml);
                // xmlDoc.LoadXml(sCaminhoDoArquivo);

                //Pegando elemento pelo nome da TAG
                XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                //Usando foreach para localizar o cnpj
                foreach (XmlNode xn in xnList)
                {
                    if (xn["cnpjcpf"].InnerText.Trim() == EMPRESATy.CNPJCPF.Trim())
                    {
                        MensagemErroBloqueio = xn["mensagem"].InnerText.Trim();
                        if (xn["bloqueiototal"].InnerText.Trim() == "S")
                            result = false;
                        else
                            result = true;

                        break;
                    }

                }

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                result = false;
                return result;

            }

        }

        private void EnviarEmailUso()
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                // var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);

                var toAddress = new MailAddress("contato@imexsistemas.com.br");
                const string fromPassword = "Rmr877701c";

                string subject = "Acesso ao IMEX Sistemas";
                string body = "O cliente abaixo acessou o IMEX Sistemas : " + DateTime.Now.ToString() + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += "DADOS DO CLIENTE:" + System.Environment.NewLine.ToString();
                body += "Nome: " + EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += "Telefone: " + EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += "Cidade/UF: " + EMPRESATy.CIDADE + " / " + EMPRESATy.UF + System.Environment.NewLine.ToString();
                body += "Email: " + EMPRESATy.EMAIL + System.Environment.NewLine.ToString();
                body += "CNPJ:" + EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
               // body += "Computador:" + NomeComputador() + System.Environment.NewLine.ToString();
               // body += "Vencimento Suporte:" + DataVectoSuporte + System.Environment.NewLine.ToString();
                body += "Ativando Empresa para Emitir NFE" + System.Environment.NewLine.ToString();
                //body += "Versão: 2017" + System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();


                Boolean UsoConexaoSegura = true;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "N")
                    UsoConexaoSegura = false;

                var smtp = new SmtpClient
                {
                    Host = "smtp.site.com.br",
                    Timeout = 4000,
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = UsoConexaoSegura,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na validação do email!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

     
        private void AtivaEmpresaEmissoraNFe()
        {
            DialogResult dr = MessageBox.Show("Deseja Realmente Ativar a Empresa  "+ txtNome.Text +" para Emitir a NFe?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    SalvaEmpresa();
                    SalvaConfigSistema();
                    SaveRegistro();
                    EnviarEmailUso();
                    Util.ExibirMSg("Empresa Ativada com Sucesso!", "Blue");
                }
        }

        private void SaveRegistro()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("nfe", true);
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"nfe\eMail", true);
            RegistryKey key3 = Registry.CurrentUser.OpenSubKey(@"nfe\Proxy", true);
            
            try
            {
                if (key == null)
                    key = Registry.CurrentUser.CreateSubKey("nfe");
                else
                {

                    //Dados Empresa
                    EMPRESAEntity EmpresaTy = new EMPRESAEntity();
                    EmpresaTy = EMPRESAP.Read(1);

                    string CNPJ = EmpresaTy.CNPJCPF.Replace(".", "").Replace("-", "").Replace("/", "");
                    key.SetValue("CNPJ", CNPJ);

                    key.SetValue("NoSerieCertificado", txtSerialCertDigital.Text);
                   
                    MUNICIPIOSEntity MUNICIPIOSTy = new MUNICIPIOSEntity();
                    MUNICIPIOSTy = MUNICIPIOSP.Read(_COD_MUN_IBGE);
                    ESTADOProvider ESTADOP = new ESTADOProvider();
                    if (MUNICIPIOSTy != null)
                    {
                        //Busca dados do Estado
                        string _UFEmit = ESTADOP.Read(Convert.ToInt32(MUNICIPIOSTy.COD_UF_IBGE)).UF;
                        key.SetValue("UnidadeFederada", _UFEmit);
                    }

                    key.SetValue("UnidadeFederadaCodigo", CodigodeUFIBGE(EmpresaTy.UF).ToString().TrimEnd());
                    key3.SetValue("CodMunicipio", MUNICIPIOSTy.COD_MUN_IBGE.ToString());
                    key3.SetValue("IE", EmpresaTy.IE.Replace(".", "").Replace("-", ""));
                    key3.SetValue("Municipio", MUNICIPIOSTy.MUNICIPIO);

                }
            }
            finally
            {
                key.Close();
                key2.Close();
                key3.Close();
            }
        }

        public int CodigodeUFIBGE(string UF)
        {
            int uf = -1;
            switch (UF)
            {
                case "AC": uf = 12; break;
                case "AL": uf = 27; break;
                case "AP": uf = 16; break;
                case "AM": uf = 13; break;
                case "BA": uf = 29; break;
                case "CE": uf = 23; break;
                case "DF": uf = 53; break;
                case "GO": uf = 52; break;
                case "MA": uf = 21; break;
                case "MG": uf = 31; break;
                case "ES": uf = 32; break;
                case "MS": uf = 50; break;
                case "MT": uf = 51; break;
                case "PA": uf = 15; break;
                case "PB": uf = 25; break;
                case "PE": uf = 26; break;
                case "PI": uf = 22; break;
                case "PR": uf = 41; break;
                case "RJ": uf = 33; break;
                case "RN": uf = 24; break;
                case "RO": uf = 11; break;
                case "RR": uf = 14; break;
                case "RS": uf = 43; break;
                case "SC": uf = 42; break;
                case "SE": uf = 28; break;
                case "SP": uf = 35; break;
                case "TO": uf = 17; break;
            }

            return uf;
        }     

        private void SalvaConfigSistema()
        {
            try
            {
                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                int CODMUNIBGE = _COD_MUN_IBGE;

              //  int CODUFIBGE = Convert.ToInt32(txtCodUFIBGE.Text);

                CONFISISTEMATy.SERIALCERTFDIGITAL = txtSerialCertDigital.Text;
                CONFISISTEMATy.NAMECERTFDIGITAL = txtNameCertDigital.Text;
                CONFISISTEMATy.VALIDADECERTDIGITAL = txtValidadeCertDigital.Text;

                CONFISISTEMATy.NOMEFANTASIA = txtNomeFantasia.Text;
                CONFISISTEMATy.CNAE = TxtCNAE.Text;
                CONFISISTEMATy.IEST = txtIEST.Text;
                CONFISISTEMATy.CRT = txtCRT.Text;

                CONFISISTEMAP.Save(CONFISISTEMATy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar Configuração de Sistema!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SalvaEmpresa()
        {
            try
            {
                
                EMPRESAEntity EmpresaTyAl = new EMPRESAEntity();
                EmpresaTyAl = EMPRESAP.Read(1);

                EmpresaTyAl.NOMECLIENTE = txtNome.Text;
                EmpresaTyAl.NOMEFANTASIA = txtNomeFantasia.Text;
                EmpresaTyAl.ENDERECO = txtEnd1.Text;
                EmpresaTyAl.BAIRRO = txtBairro.Text;
                EmpresaTyAl.CEP = mktxtCep1.Text;
                EmpresaTyAl.CIDADE = txtCidade1.Text;
                EmpresaTyAl.UF = txtUF1.Text;
                EmpresaTyAl.TELEFONE = txtTelefone1.Text;
                EmpresaTyAl.EMAIL = txtEmailCliente.Text;
                EmpresaTyAl.NUMERO = txtNumEndereco.Text;
                EmpresaTyAl.COMPLEMENTO = txtComplemento1.Text;
                EmpresaTyAl.CNPJCPF = maskedtxtCNPJ.Text;
                EmpresaTyAl.IE = txtIE.Text;

                EMPRESAP.Save(EmpresaTyAl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar Empresa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

    }
}