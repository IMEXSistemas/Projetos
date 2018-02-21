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


namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmClienteSimples : Form
    {
        Utility Util = new Utility();

        FUNCIONARIOCollection FuncionarioColl = new FUNCIONARIOCollection();
        public LIS_CLIENTECollection LIS_ClienteColl = new LIS_CLIENTECollection();
        LIS_ESTADOCollection LIS_ESTADOColl = new LIS_ESTADOCollection();

        LIS_CLIENTEProvider LIS_ClienteP = new LIS_CLIENTEProvider();
        CLIENTEProvider ClienteP = new CLIENTEProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmClienteSimples()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int CodClienteSelec = -1;

        int _IDCLIENTE = -1;
        int _COD_MUN_IBGE = -1;
        public CLIENTEEntity Entity
        {
            get
            {
                string nome = txtNome.Text.TrimEnd().TrimStart();
                string apelido = txtApelido.Text;
                string contato = txtContato.Text;

                string datacadastro = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtDataCadastro.Text != string.Empty)
                    datacadastro = txtDataCadastro.Text;

                string telefone1 = txtTelefone1.Text;
                string telefone2 = txtCelular.Text;
                string fax = txtFax.Text;
                string ramal = txtRamal.Text;
                string cnpj  = maskedtxtCNPJ.Text;
                string cpf = maskedtxtCPF.Text;
                string ie  = txtIERG.Text;
                string endereco1 = txtEnd1.Text;
                string complemento1 = txtComplemento1.Text;
                string bairro1 = txtBairro.Text;
                string cep1 = mktxtCep1.Text;
                string endereco2 = string.Empty;
                string complemento2 = string.Empty; ;
                string cidade2 = string.Empty;
                string uf2 = string.Empty;
                string cep2 = string.Empty;
                string referencia1 = string.Empty;
                string telefonereferencia1 = string.Empty;
                string emailcliente = txtEmailCliente.Text;

                DateTime? datanascimentocliente = null;
                        if(maskedtxtDataNascimento.Text!= "  /  /" )
                            datanascimentocliente = Convert.ToDateTime(maskedtxtDataNascimento.Text);
                        
               
                string flagserasa ="N";
                string flagspc = "N";
                string flagtelecheque = "N";
                string flagbloqueado = "N";

                string referencia2 = string.Empty;
                string telefonereferencia2 = string.Empty;

                decimal? rendacliente = null;
                decimal? creditocliente = null;
                string observacao = txtObservacao.Text;

                int? idclassificacao = null;

                int? idtiporegiao =null;

                int? idprofissaoatividade = null;

                int? idtransportadora = null;

                int? IDFUNCIONARIO = null;
                
                string empregocliente  = string.Empty;
                string enderecoempregocliente = string.Empty;
                string telefoneempregocliente = string.Empty;
                string cargocliente = string.Empty;
                string estadocivil = string.Empty;
                string naturalidade = string.Empty;
                string conjuge = string.Empty;

                DateTime? datanascconjuge = null;

                string cpfconjuge = string.Empty;
                string rgconjuge = string.Empty;

                decimal? rendaconjuge = null;
                string empregoconjuge = string.Empty;

                DateTime? dataadmissaoconjuge = null;
                string cargoconjuge = string.Empty;
                string telefonconjuge = string.Empty; ;
                string filiacao = string.Empty;
                string nomecontato = string.Empty;
                string atendidocontato = string.Empty;

                DateTime? dataretornocontato = null;

                string detalhescontato = string.Empty;
                string NUMEROENDER = txtNumEndereco.Text;
                

                return new CLIENTEEntity(_IDCLIENTE,  nome , apelido, contato ,
                                         Convert.ToDateTime(datacadastro), telefone1 , telefone2 ,
                                        fax , ramal, cnpj , cpf, ie ,  endereco1,
                                        complemento1, bairro1 , cep1 ,endereco2, complemento2,
                                        cidade2 , uf2 ,  cep2 ,  referencia1,
                                        telefonereferencia1, emailcliente,
                                        datanascimentocliente, flagserasa,
                                        flagspc,  flagtelecheque, flagbloqueado,
                                        referencia2, telefonereferencia2,
                                        rendacliente, creditocliente,
                                        observacao, idclassificacao, idtiporegiao,
                                        idprofissaoatividade ,idtransportadora ,
                                        IDFUNCIONARIO,
                                        empregocliente ,  enderecoempregocliente,
                                        telefoneempregocliente,  cargocliente,
                                        estadocivil,naturalidade, conjuge,
                                        datanascconjuge, cpfconjuge , rgconjuge ,
                                        rendaconjuge,  empregoconjuge, dataadmissaoconjuge,
                                        cargoconjuge, telefonconjuge, filiacao ,
                                        nomecontato, atendidocontato, dataretornocontato,
                                        detalhescontato, _COD_MUN_IBGE, NUMEROENDER);

        
            }
            set
            {
                if (value != null)
                {
                    _IDCLIENTE = value.IDCLIENTE;                

                    txtNome.Text = value.NOME.TrimEnd().TrimStart();
                    txtApelido.Text = value.APELIDO;
                    txtContato.Text = value.CONTATO;

                    if (value.DATACADASTRO.ToString() != "  /  /")
                        txtDataCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");

                    txtTelefone1.Text = value.TELEFONE1;
                    txtCelular.Text = value.TELEFONE2;
                    txtFax.Text = value.FAX;
                    txtRamal.Text = value.RAMAL;
                    maskedtxtCNPJ.Text = value.CNPJ;
                    maskedtxtCPF.Text = value.CPF;
                    txtIERG.Text = value.IE;
                    txtEnd1.Text = value.ENDERECO1;
                    txtComplemento1.Text = value.COMPLEMENTO1;
                    txtBairro.Text = value.BAIRRO1;

                    //Busca a cidade
                    LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                    LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                    RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                    RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", value.IDCLIENTE.ToString()));
                    LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);
                    txtCidade1.Text = LIS_CLIENTEColl[0].MUNICIPIO;
                    txtUF1.Text = LIS_CLIENTEColl[0].UF;
                    _COD_MUN_IBGE = Convert.ToInt32(value.COD_MUN_IBGE);

                    mktxtCep1.Text = value.CEP1;
                    txtEmailCliente.Text = value.EMAILCLIENTE;

                    if (value.DATANASCIMENTOCLIENTE != null)
                        maskedtxtDataNascimento.Text = Convert.ToDateTime(value.DATANASCIMENTOCLIENTE).ToString("dd/MM/yyyy");
                    else
                        maskedtxtDataNascimento.Text = string.Empty;

                    txtObservacao.Text =value.OBSERVACAO;                

                    txtNumEndereco.Text = value.NUMEROENDER;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDCLIENTE = -1;                    
                    txtNome.Text = string.Empty;
                    txtApelido.Text = string.Empty;
                    txtContato.Text = string.Empty;
                    txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTelefone1.Text = string.Empty;
                    txtCelular.Text = string.Empty;
                    txtFax.Text = string.Empty;
                    txtRamal.Text = string.Empty;
                    maskedtxtCNPJ.Text = "  .   .   /    -";
                    maskedtxtCPF.Text = "   .   .   -";
                    txtIERG.Text = string.Empty;
                    txtEnd1.Text = string.Empty;
                    txtComplemento1.Text = string.Empty;
                    txtBairro.Text = string.Empty;

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

                    mktxtCep1.Text = string.Empty;
                    txtEmailCliente.Text = string.Empty;
                    maskedtxtDataNascimento.Text ="  /  /";
                    txtObservacao.Text = string.Empty;                  
                    txtNumEndereco.Text = string.Empty;
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
            if (CodClienteSelec != -1)
                Entity = ClienteP.Read(CodClienteSelec);
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
        }     


        private void TSBVolta_Click(object sender, EventArgs e)
        {
           
        }

        private void txtbNome_Enter(object sender, EventArgs e)
        {
        
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

        private void TxtRenda_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Renda do cliente. Exemplo: 1.592,29";
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
                  
                    if (_IDCLIENTE == -1)
                    {
                        //Verificar CPF/CNPJ existe para novos cadastros
                        if (VerificaCPFExistNew(maskedtxtCPF.Text) || VerificaCNPJExistNew(maskedtxtCNPJ.Text))
                        {

                            DialogResult dr = MessageBox.Show(ConfigMessage.Default.CNPJCPFDupl,
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                _IDCLIENTE = ClienteP.Save(Entity);
                                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            }
                        }
                        else
                        {
                            _IDCLIENTE = ClienteP.Save(Entity);
                            Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                            
                        }
                    }
                    else
                    {
                        _IDCLIENTE = ClienteP.Save(Entity);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        btnPesquisa_Click(null, null);
                    }

                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void ArquivoClienteRetarguardaDigiSat(int IDCLIENTE, string CaminhoRecpDigiSat)
        {
            string arquivo = CaminhoRecpDigiSat + "C" + IDCLIENTE.ToString().PadLeft(5, '0') + ".txt";
            StreamWriter escrever = new StreamWriter(arquivo);

            try
            {
                
                CLIENTEEntity CLIENTEtY = new CLIENTEEntity();
                CLIENTEtY = ClienteP.Read(IDCLIENTE);
               
                escrever.WriteLine(IDCLIENTE.ToString().PadLeft(5, '0'));//1=Código do Cliente ( 5 digitos, obedecer zeros a esquerda
                escrever.WriteLine(CLIENTEtY.NOME);//2=Nome do Cliente
                
                if (CLIENTEtY.CNPJ != "  .   .   /    -")
                    escrever.WriteLine(CLIENTEtY.CNPJ);//3=CNPJ ( Null se não usar)
                else
                    escrever.WriteLine("Null");//3=CNPJ ( Null se não usar)

                if (CLIENTEtY.CPF != "   .   .   -")
                    escrever.WriteLine(CLIENTEtY.CPF);//4=CPF ( Null se não usar)
                else
                    escrever.WriteLine("Null");//4=CPF ( Null se não usar)

                if (CLIENTEtY.IE != string.Empty)
                    escrever.WriteLine(CLIENTEtY.IE);//5=IE ( Null se não usar)
                else
                    escrever.WriteLine("Null");//5=IE ( Null se não usar)

                 escrever.WriteLine("Null");//6=RG ( Null se não usar)

                 //Busca a cidade
                 LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                 LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                 RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                 RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                 LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

                 if (LIS_CLIENTEColl.Count > 0)
                 {
                     escrever.WriteLine(LIS_CLIENTEColl[0].UF);//7=UF
                 }
                 else
                 {
                     escrever.WriteLine(string.Empty);//7=UF
                 }

                 if (CLIENTEtY.TELEFONE1 != string.Empty)
                     escrever.WriteLine(CLIENTEtY.TELEFONE1);//8=Telefone ( Null se não usar)
                 else
                     escrever.WriteLine("Null");//8=Telefone ( Null se não usar)

                 if (CLIENTEtY.CNPJ != "  .   .   /    -")
                     escrever.WriteLine("J");//9= F para pessoa Fisica / J para pessoa Juridica ( Null se não usar)
                 else
                     escrever.WriteLine("F");//9= F para pessoa Fisica / J para pessoa Juridica ( Null se não usar)

                 escrever.WriteLine("Null");//10 = C para consumidor / R para Revenda ( Null se não usar)
                 escrever.WriteLine("0");//11 = Limite de Credito (0 se não usar)
                 escrever.WriteLine("Null");//12 = Codigo da Empresa convenio ( 5 digitos, obedecer zeros a esquerda) (Null se não usar )
                 escrever.WriteLine("0");//13 = Usar convenio ( 0 para Não e 1 para Sim)
                 escrever.WriteLine("0");//14 = Dia de Acerto ( 0 se não usar)
                 escrever.WriteLine("0");//15 = Percentual convenio ( Percentual de Desconto da empresa convenio)(0 se não usar)
                 escrever.WriteLine("0");//16 = Conceder crédito (0 para nãoe  1 para sim)

                 if (CLIENTEtY.ENDERECO1 != string.Empty)
                     escrever.WriteLine(CLIENTEtY.ENDERECO1);//17 = Endereco (Null se não usar)
                 else
                     escrever.WriteLine("Null");//17 = Endereco (Null se não usar)

                 if (CLIENTEtY.BAIRRO1 != string.Empty)
                     escrever.WriteLine(CLIENTEtY.BAIRRO1);//18 = Bairro (Null se não usar)
                 else
                     escrever.WriteLine("Null");//18 = Bairro (Null se não usar)

                 if (CLIENTEtY.CEP1 != "     -")
                     escrever.WriteLine(CLIENTEtY.CEP1);//19 = CEP (Null se não usar)
                 else
                     escrever.WriteLine("Null");//18 = CEP (Null se não usar)

                 if (LIS_CLIENTEColl.Count > 0)
                 {
                     escrever.WriteLine(LIS_CLIENTEColl[0].MUNICIPIO);//19=Cidade ( Null se não usar)
                 }
                 else
                 {
                     escrever.WriteLine(string.Empty);//19=Cidade ( Null se não usar)
                 }

                escrever.Close();
            }
            catch (Exception ex)
            {
                escrever.Close();
                MessageBox.Show("Erro ao salvar arquivo DigiSat o cliente: " + IDCLIENTE.ToString());
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }
       

        private Boolean VerificaCNPJExistNew(string CNPJ)
        {
            Boolean result = false;

            if (CNPJ != "  .   .   /    -")
            {
                RowsFiltro FiltroProfileCNPJ = new RowsFiltro("CNPJ", "System.String", "=", CNPJ);
                RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

                LIS_CLIENTECollection LIS_ClienteCollCNPJ = new LIS_CLIENTECollection();
                FiltroCNPJ.Insert(0, FiltroProfileCNPJ);
                LIS_ClienteCollCNPJ = LIS_ClienteP.ReadCollectionByParameter(FiltroCNPJ);

                if (LIS_ClienteCollCNPJ.Count > 0)
                    result = true;
            }

            return result;
        }

        private Boolean VerificaCPFExistNew(string CPF)
        {
            Boolean result = false;

            if (CPF != "   .   .   -")
            {
                RowsFiltro FiltroProfileCNPJ = new RowsFiltro("CPF", "System.String", "=", CPF);
                RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

                LIS_CLIENTECollection LIS_ClienteCollCPF = new LIS_CLIENTECollection();
                FiltroCNPJ.Insert(0, FiltroProfileCNPJ);
                LIS_ClienteCollCPF = LIS_ClienteP.ReadCollectionByParameter(FiltroCNPJ);

                if (LIS_ClienteCollCPF.Count > 0)
                    result = true;
            }

            return result;
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
            else if (_IDCLIENTE == -1 && VerificaCPFExiste(maskedtxtCPF.Text))
            {
                string msgerro = "CPF já cadastrado!";
                errorProvider1.SetError(maskedtxtCPF, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (_IDCLIENTE == -1 && VerificaCNPJExiste(maskedtxtCNPJ.Text))
            {
                string msgerro = "CNPJ já cadastrado!";
                errorProvider1.SetError(maskedtxtCNPJ, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (_IDCLIENTE == 1)
            {
                string msgerro = "O codigo 1 não pode ser excluído!";
                errorProvider1.SetError(maskedtxtCNPJ, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtNome, "");
            }

            return result;
        }

        public Boolean VerificaCPFExiste(string CPF)
        {
            Boolean result = false;

            CLIENTECollection CLIENTEColl_p = new CLIENTECollection();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CPF", "System.String" , "=", CPF));
            
            if (CPF != "   .   .   -")
                CLIENTEColl_p = ClienteP.ReadCollectionByParameter(RowRelatorio);

            if (CLIENTEColl_p.Count > 0)
                result = true;

            return result;
        }

        public Boolean VerificaCNPJExiste(string CNPJ)
        {
            Boolean result = false;

            CLIENTECollection CLIENTEColl_p = new CLIENTECollection();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("CNPJ", "System.String", "=", CNPJ));

            if (CNPJ != "  .   .   /    -")
                CLIENTEColl_p = ClienteP.ReadCollectionByParameter(RowRelatorio);

            if (CLIENTEColl_p.Count > 0)
                result = true;

            return result;
        }

        public void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ClienteColl;

                lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();
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
                    MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
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
            if (LIS_ClienteColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ClienteColl;
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

                LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(Filtro, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ClienteColl;

                lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();
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
            if (LIS_ClienteColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                //Exibir o índice da linha atual
                int CodigoSelect = Convert.ToInt32(LIS_ClienteColl[rowindex].IDCLIENTE);

                Entity = ClienteP.Read(CodigoSelect);

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
            if (_IDCLIENTE == -1)
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
                        ClienteP.Delete(_IDCLIENTE);                      

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
            LIS_ClienteColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();
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
            if (LIS_ClienteColl.Count == 0)
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
            if (LIS_ClienteColl.Count == 0)
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
            if (maskedtxtDataNascimento.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataNascimento.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskedtxtDataNascimento.Focus();
                    errorProvider1.SetError(maskedtxtDataNascimento, ConfigMessage.Default.MsgDataInvalida);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(0);
                }
                else
                {
                    errorProvider1.SetError(maskedtxtDataNascimento, "");
                    e.Cancel = false;
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(maskedtxtDataNascimento, "");
            }
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
            if (LIS_ClienteColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity>(orderBy);

                LIS_ClienteColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_ClienteColl;
                lblObsField.Text = string.Empty;
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
            if (LIS_ClienteColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_ClienteColl[indice].IDCLIENTE);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = ClienteP.Read(CodigoSelect);

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
                            ClienteP.Delete(CodigoSelect);
                            MessageBox.Show(ConfigMessage.Default.MsgDelete2);
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
            if (LIS_ClienteColl.Count == 0)
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
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
                float yLineTop = e.MarginBounds.Top;


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
                e.Graphics.DrawString("DADOS DO CLIENTE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);


                for (; _Line < LIS_ClienteColl.Count; _Line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                    {
                        //Rodape
                        paginaAtual++;
                        // e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                        e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 120));
                        e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda + 500, yLineTop + 120));
                        e.HasMorePages = true;
                        return;
                    }

                    //Dados Proprietario

                    e.Graphics.DrawString("Nome: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    e.Graphics.DrawString(Util.LimiterText(LIS_ClienteColl[_Line].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, yLineTop + 100);
                    e.Graphics.DrawString("Endereço: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    e.Graphics.DrawString(Util.LimiterText(LIS_ClienteColl[_Line].ENDERECO1 + ", " + LIS_ClienteColl[_Line].NUMEROENDER, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    yLineTop += lineHeight;//2 Linha
                    e.Graphics.DrawString("Compl.: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    e.Graphics.DrawString(LIS_ClienteColl[_Line].COMPLEMENTO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, yLineTop + 100);
                    e.Graphics.DrawString("Cidade: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    e.Graphics.DrawString(Util.LimiterText(LIS_ClienteColl[_Line].MUNICIPIO + " - " + LIS_ClienteColl[_Line].UF + " - " + LIS_ClienteColl[_Line].CEP1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    yLineTop += lineHeight;//3 Linha
                    e.Graphics.DrawString("Telefone: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    e.Graphics.DrawString(LIS_ClienteColl[_Line].TELEFONE1, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, yLineTop + 100);
                    e.Graphics.DrawString("Email: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    e.Graphics.DrawString(Util.LimiterText(LIS_ClienteColl[_Line].EMAILCLIENTE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    string CPFCNPJ = (LIS_ClienteColl[_Line].CNPJ == "  .   .   /    -" || LIS_ClienteColl[_Line].CNPJ == string.Empty) ? LIS_ClienteColl[_Line].CPF : LIS_ClienteColl[_Line].CNPJ;

                    yLineTop += lineHeight;//4 Linha
                    e.Graphics.DrawString("CNPJ/CPF: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    e.Graphics.DrawString(CPFCNPJ, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, yLineTop + 100);
                    e.Graphics.DrawString("I.E / RG: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    e.Graphics.DrawString(LIS_ClienteColl[_Line].IE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    //yLineTop += lineHeight;//5 Linha
                    //e.Graphics.DrawString("Marca/Modelo: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    //e.Graphics.DrawString(LIS_ClienteColl[_Line].NOMEMARCAMODELO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, yLineTop + 100);
                    //e.Graphics.DrawString("Cor: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    //e.Graphics.DrawString(LIS_ClienteColl[_Line].NOMECOR, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    //yLineTop += lineHeight;//6 Linha
                    //e.Graphics.DrawString("Ano Fab/Modelo", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    //e.Graphics.DrawString(LIS_ClienteColl[_Line].ANOFABRICACAO + "/" + LIS_ClienteColl[_Line].ANOMODELO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, yLineTop + 100);
                    //e.Graphics.DrawString("Combustivel: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    //e.Graphics.DrawString(LIS_ClienteColl[_Line].NOMECOMBUSTIVEL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 490, yLineTop + 100);


                    yLineTop += lineHeight;
                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, yLineTop + 100, config.MargemDireita, yLineTop + 100);
                    yLineTop += lineHeight;
                    //  yLineTop += lineHeight;
                }

                //Ultima Pagina
                //e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                paginaAtual++;
                e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, config.MargemEsquerda, 963 + 120);
                e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 500, 963 + 120);

                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public string FLAGSUPORTE { get; set; }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ClienteColl.Count == 0)
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

            LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(RowRelatorio, "NOME");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = LIS_ClienteColl;
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
           
        }

        private void mktxtCep1_KeyDown(object sender, KeyEventArgs e)
        {
          
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

        private void clientePorFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
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

    }
}