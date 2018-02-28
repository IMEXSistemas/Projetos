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
using BMSworks.IMEXAppClass;

namespace BMSSoftware.Modulos.Cadastros
{
    public partial class FrmCliente : Form
    {
        Utility Util = new Utility();

        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FuncionarioColl = new FUNCIONARIOCollection();
        CLASSIFICACAOCollection CLASSIFICACAOColl = new CLASSIFICACAOCollection();
        TIPOREGIAOCollection TIPOREGIAOColl = new TIPOREGIAOCollection();
        PROFISSAORAMOATIVIDADECollection PROFISSAORAMOATIVIDADEColl = new PROFISSAORAMOATIVIDADECollection();
        public LIS_CLIENTECollection LIS_ClienteColl = new LIS_CLIENTECollection();
        LIS_ESTADOCollection LIS_ESTADOColl = new LIS_ESTADOCollection();
        PARENTESCOCollection PARENTESCOColl = new PARENTESCOCollection();
        OCASIAOCollection OCASIAOColl = new OCASIAOCollection();
        LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAColl = new LIS_DATACOMEMORATIVACollection();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
        LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
        ARQUIVOCLIENTECollection ARQUIVOCLIENTEColl = new ARQUIVOCLIENTECollection();

        ARQUIVOCLIENTEProvider ARQUIVOCLIENTEP = new ARQUIVOCLIENTEProvider();
        LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();
        LIS_CLIENTEProvider LIS_ClienteP = new LIS_CLIENTEProvider();
        CLIENTEProvider ClienteP = new CLIENTEProvider();
        DATACOMEMORATIVAProvider DATACOMEMORATIVAP = new DATACOMEMORATIVAProvider();
       
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        ENDERECOIMEXAPPProvider ENDERECOIMEXAPPP = new ENDERECOIMEXAPPProvider();
        ENDENTREGARCLIENTEProvider ENDENTREGARCLIENTEP = new ENDENTREGARCLIENTEProvider();
        CLIENTEIMEXAPPProvider CLIENTEIMEXAPPP = new CLIENTEIMEXAPPProvider();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
      
        public FrmCliente()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int CodClienteSelec = -1;

        int _IDCLIENTE = -1;
        int _COD_MUN_IBGE = -1;
        public string DataAniversario = string.Empty;
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
                string fax = txtTelefone2.Text;
                string ramal = txtRamal.Text;
                string cnpj  = maskedtxtCNPJ.Text;
                string cpf = maskedtxtCPF.Text;
                string ie  = txtIERG.Text;
                string endereco1 = txtEnd1.Text;
                string complemento1 = txtComplemento1.Text;
                string bairro1 = txtBairro.Text;
                string cep1 = mktxtCep1.Text;
                string endereco2 = txtEnd2.Text;
                string complemento2 = txtComplemento2.Text;
                string cidade2 = txtCidade2.Text;
                string uf2 = cbUF2.SelectedValue.ToString();
                string cep2 = mkdCep2.Text;
                string referencia1 = txtReferencia1.Text;
                string telefonereferencia1 = txtTelefoneRef1.Text;
                string emailcliente = txtEmailCliente.Text;

                DateTime? datanascimentocliente = null;
                    if(maskedtxtDataNascimento.Text!= "  /  /" )
                        datanascimentocliente = Convert.ToDateTime(maskedtxtDataNascimento.Text);
                        
               
                string flagserasa = ckSerasa.Checked ? "S" : "N";
                string flagspc = ckSPC.Checked ? "S" : "N";
                string flagtelecheque = ckTelecheque.Checked ? "S" : "N";
                string flagbloqueado = ckbloqueado.Checked ? "S" : "N";
                
                string referencia2 = txtReferen2.Text;
                string telefonereferencia2 = txtTelefoneRef2.Text;

                decimal? rendacliente = null;
                    if(TxtRendaCliente.Text != string.Empty)
                        rendacliente = Convert.ToDecimal(TxtRendaCliente.Text);

                decimal? creditocliente = null;
                    if(TxtCreditoCliente.Text != string.Empty)
                        creditocliente= Convert.ToDecimal(TxtCreditoCliente.Text);    
            
                string observacao = txtObservacao.Text;

                int? idclassificacao = null;
                    if(Convert.ToInt32(cbClassificacao.SelectedValue) > 0)
                        idclassificacao =Convert.ToInt32(cbClassificacao.SelectedValue.ToString());

                int? idtiporegiao =null;
                if (Convert.ToInt32(cbTipoRegiao.SelectedValue) > 0)
                    idtiporegiao = Convert.ToInt32(cbTipoRegiao.SelectedValue.ToString());

                int? idprofissaoatividade = null;
                if (Convert.ToInt32(cbProfissaoAtividade.SelectedValue) > 0)
                       idprofissaoatividade = Convert.ToInt32(cbProfissaoAtividade.SelectedValue.ToString());

                int? idtransportadora = null;
                if (Convert.ToInt32(cbTransportadora.SelectedValue) > 0)
                        idtransportadora =  Convert.ToInt32(cbTransportadora.SelectedValue.ToString());

                int? IDFUNCIONARIO = null;
                if (Convert.ToInt32(cbVendedor.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbVendedor.SelectedValue.ToString());
                
                string empregocliente  = txtEmpresa.Text;
                string enderecoempregocliente = txtEndEmpresa.Text;
                string telefoneempregocliente = txtTelefoneEmpresa.Text;
                string cargocliente = txtCargo.Text;
                string estadocivil = txtEstadoCivil.Text;
                string naturalidade = txtNaturalidade.Text;
                string conjuge = txtNomeConjuge.Text;

                DateTime? datanascconjuge = null;
                    if(maskDtaNascConjuge.Text != "  /  /" )
                        datanascconjuge = Convert.ToDateTime(maskDtaNascConjuge.Text);

                string cpfconjuge = makCPFConjuge.Text;
                string rgconjuge  = txtRGConjuge.Text;

                decimal? rendaconjuge = null;
                if (txtRendaConjuge.Text != string.Empty)
                    rendaconjuge = Convert.ToDecimal(txtRendaConjuge.Text);

                string empregoconjuge = txtEmpresaConjuge.Text;

                DateTime? dataadmissaoconjuge = null;
                    if(mskDtAdmissao.Text != "  /  /")
                        dataadmissaoconjuge = Convert.ToDateTime(mskDtAdmissao.Text);

                string cargoconjuge = txtCargoConjuge.Text;
                string telefonconjuge = txtTelefoneConjuge.Text;
                string filiacao = txtFiliacao.Text;
                string nomecontato = txtNomeContato.Text;
                string atendidocontato = txtAtendidoContato.Text;

                DateTime? dataretornocontato = null;
                    if(mskDataRetornoContato.Text != "  /  /")
                        dataretornocontato =  Convert.ToDateTime(mskDataRetornoContato.Text);

                string detalhescontato = txtObservacoContato.Text;
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
                    ListaArquivoCliente(_IDCLIENTE);

                    txtNome.Text = value.NOME.TrimEnd().TrimStart();
                    txtApelido.Text = value.APELIDO;
                    txtContato.Text = value.CONTATO;

                    if (value.DATACADASTRO.ToString() != "  /  /")
                        txtDataCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");

                    txtTelefone1.Text = value.TELEFONE1;
                    txtCelular.Text = value.TELEFONE2;
                    txtTelefone2.Text = value.FAX;
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
                    txtEnd2.Text = value.ENDERECO2;
                    txtComplemento2.Text = value.COMPLEMENTO2;
                    txtCidade2.Text = value.CIDADE2;

                    if (value.UF2 == string.Empty || value.UF2.Length == 0)
                        cbUF2.FindStringExact(ConfigSistema1.Default.UFSelect);
                    else
                        cbUF2.SelectedIndex = cbUF2.FindStringExact(value.UF2);

                    mkdCep2.Text = value.CEP2;
                    txtReferencia1.Text = value.REFERENCIA1;
                    txtTelefoneRef1.Text = value.TELEFONEREFERENCIA1;
                    txtEmailCliente.Text = value.EMAILCLIENTE;

                    if (value.DATANASCIMENTOCLIENTE != null)
                        maskedtxtDataNascimento.Text = Convert.ToDateTime(value.DATANASCIMENTOCLIENTE).ToString("dd/MM/yyyy");
                    else
                        maskedtxtDataNascimento.Text = string.Empty;

                    ckSerasa.Checked = value.FLAGSERASA == "S"? true : false;
                    ckSPC.Checked = value.FLAGSPC == "S"? true : false;
                    ckTelecheque.Checked = value.FLAGTELECHEQUE == "S"? true : false;
                    ckbloqueado.Checked = value.FLAGBLOQUEADO == "S"? true : false;
                  
                    txtReferen2.Text = value.REFERENCIA2;
                    txtTelefoneRef2.Text = value.TELEFONEREFERENCIA2;


                     //Formatando o campo de moeda
                    TxtRendaCliente.Text = Convert.ToString(value.RENDACLIENTE);
                    if (value.RENDACLIENTE != null)
                    {
                        Double f = Convert.ToDouble(TxtRendaCliente.Text);
                        TxtRendaCliente.Text = string.Format("{0:n2}", f);
                    }

                     //Formatando o campo de moeda
                    TxtCreditoCliente.Text = Convert.ToString(value.CREDITOCLIENTE);
                    if (value.CREDITOCLIENTE != null)
                    {
                        Double f = Convert.ToDouble(TxtCreditoCliente.Text);
                        TxtCreditoCliente.Text = string.Format("{0:n2}", f);
                    }

                    txtObservacao.Text =value.OBSERVACAO;

                    if (value.IDCLASSIFICACAO != null)
                        cbClassificacao.SelectedValue = value.IDCLASSIFICACAO;
                    else
                        cbClassificacao.SelectedValue = -1;

                    if (value.IDTIPOREGIAO != null)
                        cbTipoRegiao.SelectedValue = value.IDTIPOREGIAO;
                    else
                        cbTipoRegiao.SelectedValue = -1;
       
                     if (value.IDPROFISSAOATIVIDADE != null)
                         cbProfissaoAtividade.SelectedValue = value.IDPROFISSAOATIVIDADE;
                    else
                         cbProfissaoAtividade.SelectedValue = -1;

                     if (value.IDTRANSPORTADORA != null)
                         cbTransportadora.SelectedValue = value.IDTRANSPORTADORA;
                     else
                         cbTransportadora.SelectedValue = -1;

                     if (value.IDFUNCIONARIO != null)
                         cbVendedor.SelectedValue = value.IDFUNCIONARIO;
                    else
                         cbVendedor.SelectedValue = -1;                     
          
                    txtEmpresa.Text = value.EMPREGOCLIENTE;
                    txtEndEmpresa.Text = value.ENDERECOEMPREGOCLIENTE;
                    txtTelefoneEmpresa.Text = value.TELEFONEEMPREGOCLIENTE;
                    txtCargo.Text = value.CARGOCLIENTE;
                    txtEstadoCivil.Text = value.ESTADOCIVIL;
                    txtNaturalidade.Text = value.NATURALIDADE;
                    txtNomeConjuge.Text = value.CONJUGE;

                    maskDtaNascConjuge.Text = value.DATANASCCONJUGE != null ? Convert.ToDateTime(value.DATANASCCONJUGE).ToString("dd/MM/yyyy") : "  /  /";

                    makCPFConjuge.Text = value.CPFCONJUGE;
                    txtRGConjuge.Text = value.RGCONJUGE;

                       //Formatando o campo de moeda
                    txtRendaConjuge.Text = Convert.ToString(value.RENDACONJUGE);
                    if (value.RENDACONJUGE != null)
                    {
                        Double f = Convert.ToDouble(txtRendaConjuge.Text);
                        txtRendaConjuge.Text = string.Format("{0:n2}", f);
                    }

                    txtEmpresaConjuge.Text = value.EMPREGOCONJUGE;

                    mskDtAdmissao.Text = value.DATAADMISSAOCONJUGE != null ? Convert.ToDateTime(value.DATAADMISSAOCONJUGE).ToString("dd/MM/yyyy") : "  /  /";

                    txtCargoConjuge.Text = value.CARGOCONJUGE;
                    txtTelefoneConjuge.Text = value.TELEFONCONJUGE;
                    txtFiliacao.Text = value.FILIACAO;
                    txtNomeContato.Text = value.NOMECONTATO;
                    txtAtendidoContato.Text = value.ATENDIDOCONTATO;

                    mskDataRetornoContato.Text = value.DATARETORNOCONTATO != null ? Convert.ToDateTime(value.DATARETORNOCONTATO).ToString("dd/MM/yyyy") : "  /  /";

                    txtObservacoContato.Text = value.DETALHESCONTATO;
                    txtNumEndereco.Text = value.NUMEROENDER;

                    //Busca Enderço de Entrega
                    ENDENTREGARCLIENTECollection ENDENTREGARCLIENTEColl = new ENDENTREGARCLIENTECollection();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", _IDCLIENTE.ToString()));
                    ENDENTREGARCLIENTEColl = ENDENTREGARCLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    if (ENDENTREGARCLIENTEColl.Count > 0)
                        Entity4 = ENDENTREGARCLIENTEP.Read(Convert.ToInt32(ENDENTREGARCLIENTEColl[0].IDENDENTREGARCLIENTE));
                    else
                        Entity4 = null;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDCLIENTE = -1;
                    Entity4 = null;
                    ListaArquivoCliente(_IDCLIENTE);
                    txtNome.Text = string.Empty;
                    txtApelido.Text = string.Empty;
                    txtContato.Text = string.Empty;
                    txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTelefone1.Text = string.Empty;
                    txtCelular.Text = string.Empty;
                    txtTelefone2.Text = string.Empty;
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


                        //Dados da Entrega
                        txtCidadeEntrega.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                        txtUFEntrega.Text = LIS_MUNICIPIOSColl[0].UF;
                    }
                    else
                    {
                        _COD_MUN_IBGE = -1;
                        txtCidade1.Text = string.Empty;
                        txtUF1.Text = string.Empty;

                        //Dados da Entrega
                        txtCidadeEntrega.Text = string.Empty;
                        txtUFEntrega.Text = string.Empty;
                    }

                    mktxtCep1.Text = string.Empty;
                    txtEnd2.Text = string.Empty;
                    txtComplemento2.Text = string.Empty;
                    txtCidade2.Text = string.Empty;
                    cbUF2.SelectedIndex = cbUF2.FindStringExact(ConfigSistema1.Default.UFSelect);
                    mkdCep2.Text = "     -";
                    txtReferencia1.Text = string.Empty;
                    txtTelefoneRef1.Text = string.Empty;
                    txtEmailCliente.Text = string.Empty;
                    maskedtxtDataNascimento.Text ="  /  /";
                    ckSerasa.Checked = false;
                    ckSPC.Checked = false;
                    ckTelecheque.Checked = false;
                    ckbloqueado.Checked = false;
                    txtReferen2.Text = string.Empty;
                    txtTelefoneRef2.Text = string.Empty;
                    TxtRendaCliente.Text = "0,00";
                    TxtCreditoCliente.Text = "0,00";
                    txtObservacao.Text = string.Empty;
                    cbClassificacao.SelectedIndex = 0;
                    cbTipoRegiao.SelectedIndex = 0;
                    cbProfissaoAtividade.SelectedIndex =0;
                    cbTransportadora.SelectedIndex = 0;
                    cbVendedor.SelectedIndex = 0;
                    txtEmpresa.Text = string.Empty;
                    txtEndEmpresa.Text = string.Empty;
                    txtTelefoneEmpresa.Text = string.Empty;
                    txtCargo.Text = string.Empty;
                    txtEstadoCivil.Text = string.Empty;
                    txtNaturalidade.Text = string.Empty;
                    txtNomeConjuge.Text = string.Empty;
                    maskDtaNascConjuge.Text = "  /  /";
                    makCPFConjuge.Text = "   .   .   -";
                    txtRGConjuge.Text = string.Empty;
                    txtRendaConjuge.Text = string.Empty;
                    txtEmpresaConjuge.Text = string.Empty;
                    mskDtAdmissao.Text = "  /  /";
                    txtCargoConjuge.Text = string.Empty;
                    txtTelefoneConjuge.Text = string.Empty;
                    txtFiliacao.Text = string.Empty;
                    txtNomeContato.Text = string.Empty;
                    txtAtendidoContato.Text = string.Empty;
                    mskDataRetornoContato.Text = "  /  /";
                    txtObservacoContato.Text = string.Empty;
                    txtNumEndereco.Text = string.Empty;
                    errorProvider1.Clear();
                    txtNome.Focus();
                }


            }
        }

        int _IDARQUIVOCLIENTE = -1;
        byte[] _ArquivoPDF = null;
        public ARQUIVOCLIENTEEntity Entity2
        {
            get
            {
                string NOME = txtNomeImagem.Text;
                string TIPO = lblNameFile.Text;

                return new ARQUIVOCLIENTEEntity(_IDARQUIVOCLIENTE, _IDCLIENTE, NOME, TIPO, _ArquivoPDF);
            }
            set
            {
                if (value != null)
                {

                }
                else
                {
                    txtNomeImagem.Text = string.Empty;
                    lblNameFile.Text = "Nenhum Arquivo Selecionado";
                    _IDARQUIVOCLIENTE = -1;
                    _ArquivoPDF = null;
                }
            }
        }
     

        int _IDENDENTREGARCLIENTE = -1;
        public ENDENTREGARCLIENTEEntity Entity4
        {
            get
            {
               string NOME    = txtNomeEntrega.Text;             // VARCHAR(50),
               string ENDERECO= txtEnderEntrega.Text;             // VARCHAR(50),
               string NUMERO  = txtNumeroEntrega.Text;              //VARCHAR(10),
               string COMPLEMENTO =txtComplementoEntrega.Text;    //VARCHAR(50),
               string BAIRRO = txtBairroEntrega.Text;               //VARCHAR(50),
               string CEP   = txtCEPEntrega.Text;                //VARCHAR(15),
               string CIDADE =txtCidadeEntrega.Text;              //VARCHAR(50),
               string UF  = txtUFEntrega.Text;                  //VARCHAR(2))

                return new ENDENTREGARCLIENTEEntity( _IDENDENTREGARCLIENTE, _IDCLIENTE, NOME, ENDERECO, NUMERO, COMPLEMENTO,
                                                    BAIRRO, CEP, CIDADE, UF);
            }
            set
            {
                if (value != null)
                {
                    _IDENDENTREGARCLIENTE = value.IDENDENTREGARCLIENTE;
                    txtNomeEntrega.Text = value.NOME;
                    txtEnderEntrega.Text = value.ENDERECO;
                    txtNumeroEntrega.Text = value.NUMERO;
                    txtComplementoEntrega.Text = value.COMPLEMENTO;
                    txtBairroEntrega.Text = value.BAIRRO;
                    txtCEPEntrega.Text = value.CEP;
                    txtCidadeEntrega.Text = value.CIDADE;
                    txtUFEntrega.Text = value.UF;
                }
                else
                {
                   _IDENDENTREGARCLIENTE = -1;
                    txtNomeEntrega.Text = string.Empty;
                    txtEnderEntrega.Text = string.Empty;
                    txtNumeroEntrega.Text = string.Empty;
                    txtComplementoEntrega.Text = string.Empty;
                    txtBairroEntrega.Text = string.Empty;
                    txtCEPEntrega.Text = string.Empty;
                    txtCidadeEntrega.Text =string.Empty;
                    txtUFEntrega.Text = string.Empty;
                }
            }
        }


        public CLIENTEFASTEntity Entity5
        {
            get
            {
                   string NOME_CLIENTE = txtNome.Text;//varchar(80),
                   string FANTASIA_CLIENTE= txtNome.Text; // varchar(80),
                   string CPF_CLIENTE = maskedtxtCPF.Text;//varchar(18),
                   string DOC_CLIENTE = txtIERG.Text; //varchar(18),
                   string ENDERECO_CLIENTE = txtEnd1.Text;//varchar(50),
                   string NUMEROEND_CLIENTE =txtNumEndereco.Text; //char(10),
                   string COMPLEMENTOEND_CLIENTE = txtComplemento1.Text; //varchar(50),
                   string BAIRRO_CLIENTE = txtBairro.Text;// char(30),
                   string CEP_CLIENTE = mktxtCep1.Text; //char(9),
                   string EMAIL_CLIENTE = txtEmailCliente.Text;//char(60),
                   string TELEFONE1_CLIENTE =txtTelefone1.Text;// char(20),
                   string TELEFONE2_CLIENTE = txtTelefone2.Text;//char(20),
                   string CELULAR_CLIENTE = txtCelular.Text;//char(20)              //VARCHAR(2))

                return new CLIENTEFASTEntity( _IDCLIENTE, NOME_CLIENTE, FANTASIA_CLIENTE, CPF_CLIENTE, DOC_CLIENTE,
                                              ENDERECO_CLIENTE, NUMEROEND_CLIENTE, COMPLEMENTOEND_CLIENTE, BAIRRO_CLIENTE,
                                            CEP_CLIENTE, EMAIL_CLIENTE, TELEFONE1_CLIENTE, TELEFONE2_CLIENTE,  CELULAR_CLIENTE, _COD_MUN_IBGE);
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
            GetDropClassificacao();
            GetDropTipoRegiao();
            GetDropProfissaoAtividade();
            GetDropVendedor();
            GetDropTransportadora();
            GetDropUF2();
            PreencheDropTipoPesquisa();
            bntCadClassificacao.Image = Util.GetAddressImage(6);
            btnTipoRegiao.Image = Util.GetAddressImage(6);
            btnProfiRamos.Image = Util.GetAddressImage(6);
            btcadvendedor.Image = Util.GetAddressImage(6);
         
            PreencheDropCamposPesquisa();       

            //Exibir dados do cliente consultado em outra tela
            if (CodClienteSelec != -1)
                Entity = ClienteP.Read(CodClienteSelec);
            else if(VerificaPlanos())
                Entity = null;

            if (DataAniversario != string.Empty)
            {
                string Dia = DateTime.Now.ToString("dd");
                string Mes = DateTime.Now.ToString("MM");     
                RowRelatorio.Add(new RowsFiltro("extract(Day from (DATANASCIMENTOCLIENTE))", "System.DateTime", "=", Dia));
                RowRelatorio.Add(new RowsFiltro("extract(MONTH  from (DATANASCIMENTOCLIENTE))", "System.DateTime", "=", Mes));
                LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(RowRelatorio, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ClienteColl;
                tabControlCliente.SelectTab(3);
            }

            CONFISISTEMATy = CONFISISTEMAP.Read(1);

            VerificaAcesso();

            cbCamposPesquisa.SelectedIndex = 2;

            this.Cursor = Cursors.Default;
            
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

            btnPdf2.Image = Util.GetAddressImage(17);
            btnexcel2.Image = Util.GetAddressImage(18);
            btnImprimir2.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);
        }      

       private void GetDropClassificacao()
       {
           CLASSIFICACAOProvider CLASSIFICACAOP = new CLASSIFICACAOProvider();
           CLASSIFICACAOColl = CLASSIFICACAOP.ReadCollectionByParameter(null, "NOME");

           cbClassificacao.DisplayMember = "NOME";
           cbClassificacao.ValueMember = "IDCLASSIFICACAO";

           CLASSIFICACAOEntity ClassiTy = new CLASSIFICACAOEntity();
           ClassiTy.NOME = ConfigMessage.Default.MsgDrop;
           ClassiTy.IDCLASSIFICACAO = -1;
           CLASSIFICACAOColl.Add(ClassiTy);         

           Phydeaux.Utilities.DynamicComparer<CLASSIFICACAOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLASSIFICACAOEntity>(cbClassificacao.DisplayMember);

           CLASSIFICACAOColl.Sort(comparer.Comparer);
           cbClassificacao.DataSource = CLASSIFICACAOColl;         

           cbClassificacao.SelectedIndex = 0;
       }

       private void GetDropUF2()
       {
           LIS_ESTADOProvider LIS_ESTADOP = new LIS_ESTADOProvider();
           LIS_ESTADOColl = LIS_ESTADOP.ReadCollectionByParameter(null, "UF");
           cbUF2.DataSource = LIS_ESTADOColl;

           cbUF2.DisplayMember = "UF";
           cbUF2.ValueMember = "UF";

           cbUF2.SelectedIndex = cbUF2.FindStringExact(ConfigSistema1.Default.UFSelect);
       }

       private void GetDropTipoRegiao()
       {
           TIPOREGIAOProvider TIPOREGIAOP = new TIPOREGIAOProvider();
           TIPOREGIAOColl = TIPOREGIAOP.ReadCollectionByParameter(null, "NOME");

           cbTipoRegiao.DisplayMember = "NOME";
           cbTipoRegiao.ValueMember = "IDTIPOREGIAO";

           TIPOREGIAOEntity TipoRegiaoTy = new TIPOREGIAOEntity();
           TipoRegiaoTy.NOME = ConfigMessage.Default.MsgDrop;
           TipoRegiaoTy.IDTIPOREGIAO = -1;
           TIPOREGIAOColl.Add(TipoRegiaoTy);

           Phydeaux.Utilities.DynamicComparer<TIPOREGIAOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPOREGIAOEntity>(cbTipoRegiao.DisplayMember);

           TIPOREGIAOColl.Sort(comparer.Comparer);
           cbTipoRegiao.DataSource = TIPOREGIAOColl;

           
           cbTipoRegiao.SelectedIndex = 0;
       }

       private void GetDropProfissaoAtividade()
       {
           PROFISSAORAMOATIVIDADEProvider PROFISSAORAMOATIVIDADEP = new PROFISSAORAMOATIVIDADEProvider();
           PROFISSAORAMOATIVIDADEColl = PROFISSAORAMOATIVIDADEP.ReadCollectionByParameter(null, "NOME");

           cbProfissaoAtividade.DisplayMember = "NOME";
           cbProfissaoAtividade.ValueMember = "IDPROFISSAORAMOATIVIDADE";

           PROFISSAORAMOATIVIDADEEntity PROFISSAORAMOATIVIDADETy = new PROFISSAORAMOATIVIDADEEntity();
           PROFISSAORAMOATIVIDADETy.NOME = ConfigMessage.Default.MsgDrop;
           PROFISSAORAMOATIVIDADETy.IDPROFISSAORAMOATIVIDADE = -1;
           PROFISSAORAMOATIVIDADEColl.Add(PROFISSAORAMOATIVIDADETy);

           Phydeaux.Utilities.DynamicComparer<PROFISSAORAMOATIVIDADEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PROFISSAORAMOATIVIDADEEntity>(cbProfissaoAtividade.DisplayMember);

           PROFISSAORAMOATIVIDADEColl.Sort(comparer.Comparer);
           cbProfissaoAtividade.DataSource = PROFISSAORAMOATIVIDADEColl;      

           cbProfissaoAtividade.SelectedIndex = 0;
       }

       private void GetDropVendedor()
       {
           FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
           FuncionarioColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");
         
           cbVendedor.DisplayMember = "NOME";
           cbVendedor.ValueMember = "IDFUNCIONARIO";

           FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
           FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
           FUNCIONARIOTy.IDFUNCIONARIO = -1;
           FuncionarioColl.Add(FUNCIONARIOTy);

           Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbVendedor.DisplayMember);

           FuncionarioColl.Sort(comparer.Comparer);
           cbVendedor.DataSource = FuncionarioColl;              

           cbVendedor.SelectedIndex = 0;
       }

       private void GetDropTransportadora()
       {
           TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
           TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

           cbTransportadora.DisplayMember = "NOME";
           cbTransportadora.ValueMember = "IDTRANSPORTADORA";

           TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
           TRANSPORTADORATy.NOME = ConfigMessage.Default.MsgDrop;
           TRANSPORTADORATy.IDTRANSPORTADORA = -1;
           TRANSPORTADORAColl.Add(TRANSPORTADORATy);

           Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(cbTransportadora.DisplayMember);

           TRANSPORTADORAColl.Sort(comparer.Comparer);
           cbTransportadora.DataSource = TRANSPORTADORAColl;       

           cbTransportadora.SelectedIndex = 0;
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
            tabControlCliente.SelectTab(3);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(3);
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
            GetDropClassificacao();
        }

        private void cbTipoRegiao_Enter(object sender, EventArgs e)
        {
            GetDropTipoRegiao();
        }

       
        private void cbProfissaoAtividade_Enter(object sender, EventArgs e)
        {
            GetDropProfissaoAtividade();
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
            if (mskDtAdmissao.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mskDtAdmissao.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    mskDtAdmissao.Focus();
                    errorProvider1.SetError(mskDtAdmissao, ConfigMessage.Default.MsgDataInvalida);
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
            if(_IDCLIENTE != -1)
            {
                Grava();               
            }
            else if (VerificaPlanos())
            {
                Grava();                
            }
        }
       
        private void Grava()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

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

                    //Endereço de entrega
                    _IDENDENTREGARCLIENTE =  ENDENTREGARCLIENTEP.Save(Entity4);

                    SalveIMEXAPP(Entity);
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

        private void SalveIMEXAPP(CLIENTEEntity CLIENTETy)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    CLIENTEIMEXAPPEntity CLIENTEIMEXAPPTy = new CLIENTEIMEXAPPEntity();

                    CLIENTEIMEXAPPTy.STATIVO = CLIENTETy.FLAGBLOQUEADO == "S" ? false : true;   //BOOLEAN //flagbloqueado
                    CLIENTEIMEXAPPTy.XRAZAOSOCIAL = CLIENTETy.NOME;	//STRING
                    CLIENTEIMEXAPPTy.XFANTASIA = CLIENTETy.APELIDO; //STRING

                    CLIENTEIMEXAPPTy.STJURIDICO = 1;// 1 - juridico - 0 fisico )
                    CLIENTEIMEXAPPTy.XCPFCNPJ = CLIENTETy.CNPJ;	//STRING
                    if (Util.RetiraLetras(CLIENTETy.CPF).Length > 0)
                    {
                        CLIENTEIMEXAPPTy.XCPFCNPJ = CLIENTETy.CPF;  //STRING
                        CLIENTEIMEXAPPTy.STJURIDICO = 0;
                    }

                    CLIENTEIMEXAPPTy.XRGIE = CLIENTETy.IE;	//STRING
                    CLIENTEIMEXAPPTy.XANOTACAO = CLIENTETy.OBSERVACAO;  //STRING

                    DateTime _DEFETIVACAO = Convert.ToDateTime(CLIENTETy.DATACADASTRO);
                    CLIENTEIMEXAPPTy.DEFETIVACAO = Convert.ToDateTime(_DEFETIVACAO.ToString("yyyy-MM-dd"));	//DATE

                    CLIENTEIMEXAPPTy.XTELEFONES = CLIENTETy.TELEFONE1 + " " + CLIENTETy.TELEFONE2 + " " + CLIENTETy.FAX;	//STRING
                    CLIENTEIMEXAPPTy.XEMAIL = CLIENTETy.EMAILCLIENTE;	//STRING
                    CLIENTEIMEXAPPTy.DTCADASTRO = Convert.ToDateTime(_DEFETIVACAO.ToString("yyyy-MM-dd"));  //DATE
                    CLIENTEIMEXAPPTy.XMEUID = CLIENTETy.IDCLIENTE.ToString();	//STRING
                    CLIENTEIMEXAPPP.Save(CLIENTEIMEXAPPTy);
                    SalveIMEXAPP2(CLIENTETy);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        //Salva o Endereço no IMEX App
        private void SalveIMEXAPP2(CLIENTEEntity CLIENTETy)
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    ENDERECOIMEXAPPEntity ENDERECOIMEXAPPTy = new ENDERECOIMEXAPPEntity();
                    ENDERECOIMEXAPPTy.IDENDERECO = null;
                    ENDERECOIMEXAPPTy.XMEUID = CLIENTETy.IDCLIENTE.ToString();
                    ENDERECOIMEXAPPTy.IDCLIENTE = CLIENTEIMEXAPPP.GetID(Convert.ToInt32(CLIENTETy.IDCLIENTE));
                    ENDERECOIMEXAPPTy.IDTRANSPORTADORA = null;
                    ENDERECOIMEXAPPTy.XCEP = CLIENTETy.CEP1;
                    ENDERECOIMEXAPPTy.XENDERECO = CLIENTETy.ENDERECO1;
                    ENDERECOIMEXAPPTy.CNUMERO = 0;
                    ENDERECOIMEXAPPTy.XCOMPLEMENTO = CLIENTETy.COMPLEMENTO1;
                    ENDERECOIMEXAPPTy.XBAIRRO = CLIENTETy.BAIRRO1;

                    LIS_CLIENTECollection LIS_CLIENTEColl_2 = new LIS_CLIENTECollection();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", CLIENTETy.IDCLIENTE.ToString()));
                    LIS_CLIENTEColl_2 = LIS_ClienteP.ReadCollectionByParameter(RowRelatorio);
                    if (LIS_CLIENTEColl_2.Count > 0)
                    {
                        ENDERECOIMEXAPPTy.XCIDADE = LIS_CLIENTEColl_2[0].MUNICIPIO;
                        ENDERECOIMEXAPPTy.XESTADO = LIS_CLIENTEColl_2[0].UF;
                    }

                    ENDERECOIMEXAPPTy.IDREPRESENTADA = null;
                    ENDERECOIMEXAPPP.Save(ENDERECOIMEXAPPTy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
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
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (txtCidade1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label27, ConfigMessage.Default.CampoObrigatorio);
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
            else if (_IDCLIENTE == -1 && VerificaCPFExiste(maskedtxtCPF.Text))
            {
                string msgerro = "CPF já cadastrado!";
                errorProvider1.SetError(label14, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                
                result = false;
            }
            else if (_IDCLIENTE == -1 && VerificaCNPJExiste(maskedtxtCNPJ.Text))
            {
                string msgerro = "CNPJ já cadastrado!";
                errorProvider1.SetError(label15, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (_IDCLIENTE == -1 && VerificaTelefoneExiste(txtTelefone1.Text))
            {
                string msgerro = "Telefone já cadastrado!";
                errorProvider1.SetError(label12, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else if (_IDCLIENTE == -1 && VerificaTelefoneExiste(txtTelefone2.Text))
            {
                string msgerro = "Telefone já cadastrado!";
                errorProvider1.SetError(label10, msgerro);
                result = false;
            }
            else if (_IDCLIENTE == -1 && VerificaTelefoneExiste(txtCelular.Text))
            {
                string msgerro = "Telefone já cadastrado!";
                errorProvider1.SetError(label11, msgerro);
                result = false;
            }
            else
            {
                errorProvider1.SetError(txtNome, "");
            }

            return result;
        }

        public Boolean VerificaTelefoneExiste(string Telefone)
        {
            Boolean result = false;

            try
            {
                if (Telefone.Trim().Length > 0)
                {
                    CLIENTECollection CLIENTEColl_p = new CLIENTECollection();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("TELEFONE1", "System.String", "collate pt_br like", "%" + Telefone + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("TELEFONE2", "System.String", "collate pt_br like", "%" + Telefone + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("FAX", "System.String", "collate pt_br like", "%" + Telefone + "%", "or"));

                    CLIENTEColl_p = ClienteP.ReadCollectionByParameter(RowRelatorio);

                    if (CLIENTEColl_p.Count > 0 && (CLIENTEColl_p[0].IDCLIENTE != _IDCLIENTE))
                    {
                        DialogResult dr = MessageBox.Show("Telefone já existe para o cliente: " + CLIENTEColl_p[0].IDCLIENTE + " - " + CLIENTEColl_p[0].NOME + ", Deseja salvar assim mesmo?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                            result = false;
                        else
                            result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
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
            if (VerificaPlanos())
            {
                Entity = null;

                tabControlCliente.SelectTab(0);
                txtNome.Focus();
            }
        }

        private Boolean VerificaPlanos()
        {
            Boolean result = true;

            try
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S")
                {
                    CLIENTECollection Coll_Total = new CLIENTECollection();
                    Coll_Total = ClienteP.ReadCollectionByParameter(null);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos));

                    if (RECURSOSPLANOTy != null)
                    {
                        int QuantClientes = Convert.ToInt32(RECURSOSPLANOTy.CLIENTES);

                        if (Coll_Total.Count < QuantClientes)
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show("Limite de clientes atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

                            result = false;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                    return result;
            }


            
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;

                tabControlCliente.SelectTab(0);
                txtNome.Focus();
            }
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
                tabControlCliente.SelectTab(3);
            }
            else if (_IDCLIENTE == 1)
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
                        //Apaga Endereço de entrega
                        ENDENTREGARCLIENTEP.Delete(_IDENDENTREGARCLIENTE);

                        ClienteP.Delete(_IDCLIENTE);

                        DeleteIMEXAPP(_IDCLIENTE);

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
                    int result = CLIENTEIMEXAPPP.GetID(IDREGISTRO);
                    CLIENTEIMEXAPPP.Delete(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);


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
            tabControlCliente.SelectTab(3);
            txtCriterioPesquisa.Focus();
        }

        private void TSBFiltro_Click_1(object sender, EventArgs e)
        {
            tabControlCliente.SelectTab(3);
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
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlCliente.SelectTab(3);
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
            if (maskedtxtCPF.Text != "   .   .   -")
            {
                if (!ValidacoesLibrary.ValidaCPF(maskedtxtCPF.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.CPFErro, "Red");
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
            if (_IDCLIENTE != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
            {
                Grava();
            }
        }

        private void TxtRendaCliente_Validating(object sender, CancelEventArgs e)
        {
            if (TxtRendaCliente.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TxtRendaCliente.Text))
                {
                    errorProvider1.SetError(TxtRendaCliente, ConfigMessage.Default.FieldErro);
                    TxtRendaCliente.Focus();
                    e.Cancel = true;
                    tabControlCliente.SelectTab(0);
                }
                else
                {
                    
                    Double f = Convert.ToDouble(TxtRendaCliente.Text);
                    TxtRendaCliente.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TxtRendaCliente, "");
                    e.Cancel = false;
                 }
            }
        }

        private void TxtCreditoCliente_Validating(object sender, CancelEventArgs e)
        {
            if (TxtCreditoCliente.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TxtCreditoCliente.Text))
                {
                    errorProvider1.SetError(TxtCreditoCliente, ConfigMessage.Default.FieldErro);
                    TxtCreditoCliente.Focus();
                    e.Cancel = true;
                    tabControlCliente.SelectTab(0);
                }
                else
                {
                    
                    Double f = Convert.ToDouble(TxtCreditoCliente.Text);
                    TxtCreditoCliente.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TxtCreditoCliente, "");
                    e.Cancel = false;
                }
            }
        }

        private void maskedtxtDataNascimento_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtDataNascimento.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataNascimento.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
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
            if (maskDtaNascConjuge.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskDtaNascConjuge.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
                    maskDtaNascConjuge.Focus();
                    errorProvider1.SetError(maskDtaNascConjuge, ConfigMessage.Default.MsgDataInvalida);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(1);
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
        }

        private void makCPFConjuge_Validating(object sender, CancelEventArgs e)
        {
            if (makCPFConjuge.Text != "   .   .   -")
            {
                if (!ValidacoesLibrary.ValidaCPF(makCPFConjuge.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.CPFErro, "Red");
                    makCPFConjuge.Focus();
                    errorProvider1.SetError(maskedtxtCPF, ConfigMessage.Default.CPFErro);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(1);
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
        }

        private void txtRendaConjuge_Validating(object sender, CancelEventArgs e)
        {
            if (txtRendaConjuge.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtRendaConjuge.Text))
                {
                    errorProvider1.SetError(txtRendaConjuge, ConfigMessage.Default.FieldErro);
                    txtRendaConjuge.Focus();
                    e.Cancel = true;
                    tabControlCliente.SelectTab(1);
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtRendaConjuge.Text);
                    txtRendaConjuge.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtRendaConjuge, "");
                    e.Cancel = false;
                }
            }
        }

        private void mskDtAdmissao_Validating(object sender, CancelEventArgs e)
        {
            if (mskDtAdmissao.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mskDtAdmissao.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
                    mskDtAdmissao.Focus();
                    errorProvider1.SetError(mskDtAdmissao, ConfigMessage.Default.MsgDataInvalida);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(1);
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
        }

        private void mskDataRetornoContato_Validating(object sender, CancelEventArgs e)
        {
            if (mskDataRetornoContato.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataRetornoContato.Text))
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
                    mskDataRetornoContato.Focus();
                    errorProvider1.SetError(mskDataRetornoContato, ConfigMessage.Default.MsgDataInvalida);
                    e.Cancel = true;
                    tabControlCliente.SelectTab(1);
                }
                else
                {
                    e.Cancel = false;                    
                    errorProvider1.SetError(maskedtxtCPF, "");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(maskedtxtCPF, "");
            }
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
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity>(orderBy);

                    LIS_ClienteColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_ClienteColl;
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
            try
            {
                //Filtros pela data 

                LIS_DATACOMEMORATIVAProvider LIS_DATACOMEMORATIVAP = new LIS_DATACOMEMORATIVAProvider();
                LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAFiltroColl = new LIS_DATACOMEMORATIVACollection();
                LIS_DATACOMEMORATIVAFiltroColl = LIS_DATACOMEMORATIVAP.ReadCollectionByParameter(null, "DATACOM");

                //Remove idcliente repetidos
                LIS_DATACOMEMORATIVAFiltroColl = VerifyRemove(LIS_DATACOMEMORATIVAFiltroColl);
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
                e.Graphics.DrawString("Relação de Datas Comemorativas Geral", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Cód.Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome do Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 70, 170);
                e.Graphics.DrawString("Telefone ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 330, 170); e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_DATACOMEMORATIVAFiltroColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_DATACOMEMORATIVAFiltroColl.Count)
                {
                    if (LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].NOMECLIENTE, 32), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 70, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].TELEFONE1, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 330, config.PosicaoDaLinha);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    ////Listar os aniversário
                    LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAAnivColl = new LIS_DATACOMEMORATIVACollection();
                    LIS_DATACOMEMORATIVAAnivColl = DataRel(Convert.ToInt32(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE));
                    e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Parentesco", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Ocasião", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Data", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha + 1);
                    foreach (LIS_DATACOMEMORATIVAEntity item in LIS_DATACOMEMORATIVAAnivColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Util.LimiterText(item.NOMECOMEMORATIVO.ToString(), 50), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.DESCPARENTESCO, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 200, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.DESCOCASIAO.ToString(), 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(item.DATACOM).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    }
                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    string linhasepar = "------------------------------------------------------------------------------------------";
                    string linhasepar2 = "------------------------------------------------------------------------------------------";
                    e.Graphics.DrawString(linhasepar + linhasepar2, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    //IndexRegistro++;
                    //config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DATACOMEMORATIVAFiltroColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DATACOMEMORATIVAFiltroColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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
        private LIS_DATACOMEMORATIVACollection DataRel(int IDCLIENTE)
        {
            LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAColl = new LIS_DATACOMEMORATIVACollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            if (DataInicial != string.Empty)
            {
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial), "and"));
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFim), "and"));
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            }
            else
            {
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            }

            LIS_DATACOMEMORATIVAProvider LIS_DATACOMEMORATIVAP = new LIS_DATACOMEMORATIVAProvider();
            LIS_DATACOMEMORATIVAColl = LIS_DATACOMEMORATIVAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_DATACOMEMORATIVAColl;
        }

        private LIS_DATACOMEMORATIVACollection VerifyRemove(LIS_DATACOMEMORATIVACollection collection)
        {
            LIS_DATACOMEMORATIVACollection result = new LIS_DATACOMEMORATIVACollection();

            foreach (LIS_DATACOMEMORATIVAEntity el in collection)
            {
                if (result.Find(delegate(LIS_DATACOMEMORATIVAEntity item)
                { return (item.IDCLIENTE == el.IDCLIENTE); }) == null)
                    result.Add(el);
            }

            return result;
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
            try
            {
                //Filtros pela data 
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial), "and"));
                RowRelatorio.Add(new RowsFiltro("DATACOM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFim)));

                LIS_DATACOMEMORATIVAProvider LIS_DATACOMEMORATIVAP = new LIS_DATACOMEMORATIVAProvider();
                LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAFiltroColl = new LIS_DATACOMEMORATIVACollection();
                LIS_DATACOMEMORATIVAFiltroColl = LIS_DATACOMEMORATIVAP.ReadCollectionByParameter(RowRelatorio, "DATACOM");

                //Remove idcliente repetidos
                LIS_DATACOMEMORATIVAFiltroColl = VerifyRemove(LIS_DATACOMEMORATIVAFiltroColl);
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
                e.Graphics.DrawString("Relação de Datas Comemorativas - Data Inicial: " + DataInicial + " Data Final: " + DataFim, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Cód.Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome do Cliente", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 70, 170);
                e.Graphics.DrawString("Telefone ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 330, 170); e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_DATACOMEMORATIVAFiltroColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_DATACOMEMORATIVAFiltroColl.Count)
                {
                    if (LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].NOMECLIENTE, 32), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 70, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].TELEFONE1, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 330, config.PosicaoDaLinha);
                    }

                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                    ////Listar os aniversário
                    LIS_DATACOMEMORATIVACollection LIS_DATACOMEMORATIVAAnivColl = new LIS_DATACOMEMORATIVACollection();
                    LIS_DATACOMEMORATIVAAnivColl = DataRel(Convert.ToInt32(LIS_DATACOMEMORATIVAFiltroColl[IndexRegistro].IDCLIENTE));
                    e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Parentesco", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Ocasião", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                    e.Graphics.DrawString("Data", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha + 1);
                    foreach (LIS_DATACOMEMORATIVAEntity item in LIS_DATACOMEMORATIVAAnivColl)
                    {
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(Util.LimiterText(item.NOMECOMEMORATIVO.ToString(), 50), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.DESCPARENTESCO, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 200, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(item.DESCOCASIAO.ToString(), 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(item.DATACOM).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    }
                    config.LinhaAtual++;
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    string linhasepar = "------------------------------------------------------------------------------------------";
                    string linhasepar2 = "------------------------------------------------------------------------------------------";
                    e.Graphics.DrawString(linhasepar + linhasepar2, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    //IndexRegistro++;
                    //config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DATACOMEMORATIVAFiltroColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DATACOMEMORATIVAFiltroColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


                    //Rodape
                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                    e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                    config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                    config.LinhaAtual++;
                    e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

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
                    e.Graphics.DrawString("Bairro/Compl.: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                    e.Graphics.DrawString(LIS_ClienteColl[_Line].BAIRRO1 + " / " +LIS_ClienteColl[_Line].COMPLEMENTO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, yLineTop + 100);
                    e.Graphics.DrawString("Cidade: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                    e.Graphics.DrawString(Util.LimiterText(LIS_ClienteColl[_Line].MUNICIPIO + " - " + LIS_ClienteColl[_Line].UF + " - " + LIS_ClienteColl[_Line].CEP1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 470, yLineTop + 100);

                    yLineTop += lineHeight;//3 Linha
                    e.Graphics.DrawString("Telefone: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, yLineTop + 100);

                    if (LIS_ClienteColl[_Line].TELEFONE2.Trim() != string.Empty)
                        e.Graphics.DrawString(LIS_ClienteColl[_Line].TELEFONE1 + " / " + LIS_ClienteColl[_Line].TELEFONE2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 80, yLineTop + 100);
                    else
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
                tabControlCliente.SelectTab(3);
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
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "TOTALPRODUTOS")
                    valortotal += Convert.ToDecimal(item.TOTALPRODUTOS);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
                else if (NomeCampo == "VALORACRESCIMO")
                    valortotal += Convert.ToDecimal(item.VALORACRESCIMO);
                else if (NomeCampo == "VALORCOMISSAO")
                    valortotal += Convert.ToDecimal(item.VALORCOMISSAO);
            }

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
            tabControlCliente.SelectTab(3);
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
            using (FrmMigraCidade frm = new FrmMigraCidade())
            {
                frm.ShowDialog();
            }
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
             ListaProdutosClientes(_IDCLIENTE);
        }


        private void ListaProdutosClientes(int IDCLIENTE)
        {
            //Pedido
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO desc");

            ////Serviços O.S
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();
            LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO desc");

            //Produto O.S
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
            LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO desc");


            //Produto MTQ
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO desc");

            PreencheGrid();
        }

        decimal ValotTotalGeral = 0;
        private void PreencheGrid()
        {
            ValotTotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DGDadosProduto.Rows.Clear();

            foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOSPEDIDOColl)
            {
                string DTAEMISSAO = Convert.ToDateTime(LIS_PRODUTOSPEDIDOTy.DTEMISSAO).ToString("dd/MM/yyyy");
                string VALORTOTAL = Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.VALORTOTAL).ToString("n2");
                string QUANTIDADE = Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.QUANTIDADE).ToString("n2");

                ValotTotalGeral += Convert.ToDecimal(VALORTOTAL);

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DGDadosProduto, DTAEMISSAO, QUANTIDADE, VALORTOTAL, LIS_PRODUTOSPEDIDOTy.NOMEPRODUTO);
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DGDadosProduto.Rows.Add(row2);
            }

            foreach (var LIS_PRODUTOOSFECHTy in LIS_PRODUTOOSFECHColl)
            {
                string DTAEMISSAO = Convert.ToDateTime(LIS_PRODUTOOSFECHTy.DATAEMISSAO).ToString("dd/MM/yyyy");
                string VALORTOTAL = Convert.ToDecimal(LIS_PRODUTOOSFECHTy.VALORTOTAL).ToString("n2");
                string QUANTIDADE = Convert.ToDecimal(LIS_PRODUTOOSFECHTy.QUANTIDADE).ToString("n2");

                ValotTotalGeral += Convert.ToDecimal(VALORTOTAL);

                DataGridViewRow row2_1 = new DataGridViewRow();
                row2_1.CreateCells(DGDadosProduto, DTAEMISSAO, QUANTIDADE, VALORTOTAL, LIS_PRODUTOOSFECHTy.NOMEPRODUTO);
                row2_1.DefaultCellStyle.Font = new Font("Arial", 8);
                DGDadosProduto.Rows.Add(row2_1);
            }


            foreach (var LIS_SERVICOOSFECHTy in LIS_SERVICOOSFECHColl)
            {
                string DTAEMISSAO = Convert.ToDateTime(LIS_SERVICOOSFECHTy.DATAEMISSAO).ToString("dd/MM/yyyy");
                string VALORTOTAL = Convert.ToDecimal(LIS_SERVICOOSFECHTy.VALORTOTAL).ToString("n2");
                string QUANTIDADE = Convert.ToDecimal(LIS_SERVICOOSFECHTy.QUANTIDADE).ToString("n2");

                ValotTotalGeral += Convert.ToDecimal(VALORTOTAL);

                DataGridViewRow row2_2 = new DataGridViewRow();
                row2_2.CreateCells(DGDadosProduto, DTAEMISSAO, QUANTIDADE, VALORTOTAL, LIS_SERVICOOSFECHTy.NOMESERVICO);
                row2_2.DefaultCellStyle.Font = new Font("Arial", 8);
                DGDadosProduto.Rows.Add(row2_2);
            }

            foreach (var LIS_PRODUTOSPEDIDOMTQTy in LIS_PRODUTOSPEDIDOMTQColl)
            {
                string DTAEMISSAO = Convert.ToDateTime(LIS_PRODUTOSPEDIDOMTQTy.DTEMISSAO).ToString("dd/MM/yyyy");
                string VALORTOTAL = Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQTy.VALORTOTAL).ToString("n2");
                string QUANTIDADE = Convert.ToDecimal(LIS_PRODUTOSPEDIDOMTQTy.QUANTIDADE).ToString("n2");

                ValotTotalGeral += Convert.ToDecimal(VALORTOTAL);

                DataGridViewRow row2_2 = new DataGridViewRow();
                row2_2.CreateCells(DGDadosProduto, DTAEMISSAO, QUANTIDADE, VALORTOTAL, LIS_PRODUTOSPEDIDOMTQTy.NOMEPRODUTO);
                row2_2.DefaultCellStyle.Font = new Font("Arial", 8);
                DGDadosProduto.Rows.Add(row2_2);
            }
            

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DGDadosProduto, string.Empty, "Total Geral", ValotTotalGeral.ToString("n2"), string.Empty, string.Empty);
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DGDadosProduto.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private void clientePorFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
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
           if (LIS_ClienteColl.Count < 1)
           {
               MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
               tabControlCliente.SelectTab(3);
               txtCriterioPesquisa.Focus();
           }
           else
           {
               using (FrmEtiquetaCliente frm = new FrmEtiquetaCliente())
               {
                   frm.LIS_ClienteColl = LIS_ClienteColl;
                   frm.ShowDialog();

                   LIS_ClienteColl.Clear();
                   DataGriewDados.AutoGenerateColumns = false;
                   DataGriewDados.DataSource = null;

                   lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();
               }
           }
       }

       private void btnMaquina_Click(object sender, EventArgs e)
       {
           openFileDialog1.Filter = "Arquivos  (*.pdf)|*.pdf"; // Filtra os tipos de arquivos desejados
           openFileDialog1.ShowDialog();
       }

       private void btnAddImagem_Click(object sender, EventArgs e)
       {
           if (_IDCLIENTE == -1)
           {
               MessageBox.Show("Antes de adicionar o arquivo PDF é necessário gravar o Cliente!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
           }
           else
           {
               try
               {
                   if (txtNomeImagem.Text != string.Empty)
                   {
                       ARQUIVOCLIENTEP.Save(Entity2);
                       ListaArquivoCliente(_IDCLIENTE);
                       Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                       Entity2 = null;
                       errorProvider1.Clear();
                   }
                   else
                   {
                       errorProvider1.SetError(txtNomeImagem, ConfigMessage.Default.CampoObrigatorio);
                       Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                   }
               }
               catch (Exception)
               {

                   MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
               }
           }
       }

       private void ListaArquivoCliente(int IDCLIENTE)
       {
           RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
           RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", _IDCLIENTE.ToString()));
           ARQUIVOCLIENTEColl = ARQUIVOCLIENTEP.ReadCollectionByParameter(RowRelatorio);

           dtgImag.AutoGenerateColumns = false;
           dtgImag.DataSource = ARQUIVOCLIENTEColl;
       }

       private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
       {
           try
           {
               lblNameFile.Text = openFileDialog1.FileName.ToString();
               _ArquivoPDF = GetFoto(openFileDialog1.FileName);

           }
           catch (Exception)
           {

               MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
           }
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
           int rowindex = e.RowIndex;
           if (ARQUIVOCLIENTEColl.Count > 0 && rowindex > -1)
           {
               int ColumnSelecionada = e.ColumnIndex;
               int CodSelect = -1;

               if (ColumnSelecionada == 0)//Excluir
               {
                   DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                   if (dr == DialogResult.Yes)
                   {
                       try
                       {
                           CodSelect = Convert.ToInt32(ARQUIVOCLIENTEColl[rowindex].IDARQUIVOCLIENTE);
                           ARQUIVOCLIENTEP.Delete(CodSelect);
                           ListaArquivoCliente(_IDCLIENTE);
                           Entity2 = null;
                           Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                       }
                       catch (Exception)
                       {
                           MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                       }
                   }
               }
               else if (ColumnSelecionada == 1) //Abrir
               {
                   CodSelect = Convert.ToInt32(ARQUIVOCLIENTEColl[rowindex].IDARQUIVOCLIENTE);
                   _ArquivoPDF = ARQUIVOCLIENTEP.Read(CodSelect).FOTO;

                   string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                   StreamWriter oStreamWriter = new StreamWriter(strPath + @"\Copy_file.pdf");

                   oStreamWriter.BaseStream.Write(_ArquivoPDF, 0, _ArquivoPDF.Length);

                   oStreamWriter.Close();
                   oStreamWriter.Dispose();

                   if (File.Exists(strPath + @"\Copy_file.pdf"))
                   {
                       System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(strPath + @"\Copy_file.pdf");
                   }

               }

           }
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
                   txtCidadeEntrega.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                   txtUFEntrega.Text = LIS_MUNICIPIOSColl[0].UF;
               }
           }
       }

       private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (_IDCLIENTE == -1)
           {
               Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
               tabControlCliente.SelectTab(3);
           }
           else
           {
               using (FrmVeiculosCliente frm = new FrmVeiculosCliente())
               {
                   frm._IDCLIENTE = _IDCLIENTE;
                   frm.ShowDialog();
               }
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
          
       }

       private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
       {
          
       }

       private void consultaSerasaToolStripMenuItem_Click(object sender, EventArgs e)
       {
         
       }

       private void pesquisarOClientePelaPlacaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           using (FrmSearchPlaca frm = new FrmSearchPlaca())
           {
               frm.ShowDialog();
               var result = frm.Result;

               if (result > 0)
                Entity = ClienteP.Read(result);
           }
       }

       private void únicoToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           using (FrmEnviarEmail frm = new FrmEnviarEmail())
           {
               frm.EmailSelecionado = txtEmailCliente.Text;
               frm.EmailLote = false;
               frm.ShowDialog();
           }
       }

       private void emLoteToolStripMenuItem_Click(object sender, EventArgs e)
       {
           using (FrmEnviarEmail frm = new FrmEnviarEmail())
           {
               frm.EmailSelecionado = txtEmailCliente.Text;
               frm.EmailLote = true;
               frm.LIS_ClienteEmailLoteColl = LIS_ClienteColl;
               frm.ShowDialog();
           }
       }

       private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
       {

           int rowindex = e.RowIndex;
           if (LIS_ClienteColl.Count > 0 && rowindex > -1)
           {
               int ColumnSelecionada = e.ColumnIndex;
               int CodigoSelect = -1;

               if (ColumnSelecionada == 0)//Editar
               {
                   CodigoSelect = Convert.ToInt32(LIS_ClienteColl[rowindex].IDCLIENTE);
                   Entity = ClienteP.Read(CodigoSelect);

                   tabControlCliente.SelectTab(0);
                   txtNome.Focus();

               }
               else if (ColumnSelecionada == 1)//Excluir
               {

                   if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                   {
                       DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_ClienteColl[rowindex].NOME,
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                       if (dr == DialogResult.Yes)
                       {
                           try
                           {
                               //Busca Enderço de Entrega
                               ENDENTREGARCLIENTECollection ENDENTREGARCLIENTEColl = new ENDENTREGARCLIENTECollection();
                               RowRelatorio.Clear();
                               RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_ClienteColl[rowindex].IDCLIENTE.ToString()));
                               ENDENTREGARCLIENTEColl = ENDENTREGARCLIENTEP.ReadCollectionByParameter(RowRelatorio);

                               //Exclui Endereço de entrega
                               foreach (var item in ENDENTREGARCLIENTEColl)
                               {
                                   ENDENTREGARCLIENTEP.Delete(item.IDENDENTREGARCLIENTE);
                               }

                               CodigoSelect = Convert.ToInt32(LIS_ClienteColl[rowindex].IDCLIENTE);
                               //Delete Pedido
                               ClienteP.Delete(CodigoSelect);

                                //Deleta no IMEX App
                                DeleteIMEXAPP(CodigoSelect);
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

       private void btcadvendedor_Click(object sender, EventArgs e)
       {
           using (FrmFuncionario frm = new FrmFuncionario())
           {
               frm.ShowDialog();
               GetDropVendedor();
           }
       }

       private void button5_Click(object sender, EventArgs e)
       {
           try
           {
               string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Histórico do Cliente: " + txtNome.Text);

               PrintDGV PRt = new PrintDGV();
               PRt.Print_DataGridView(DGDadosProduto, RelatorioTitulo, this.Name);
           }
           catch (Exception ex)
           {
               MessageBox.Show("Erro técnico: " + ex.Message);
           }
       }

       private void button2_Click(object sender, EventArgs e)
       {
           Util.exportarDataGridViewArquivo(DGDadosProduto, "csv");
       }

       private void button1_Click(object sender, EventArgs e)
       {
           using (FomExportPDF frm = new FomExportPDF())
           {
               frm.TituloSelec = "Histórico do Cliente: " + txtNome.Text;
               frm.NometelaSelec = this.Name;
               frm.DataGridExport = DGDadosProduto;
               frm.ShowDialog();
           }
       }

       private void uploadDaSicronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           try
           {
                DialogResult dr = MessageBox.Show("Deseja Fazer a Sicronização no IMEX App?",
                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    int Contador = 0;
                    foreach (var item in LIS_ClienteColl)
                    {
                       // if (item.FLAGBLOQUEADO == "N")
                        {
                            CreaterCursor Cr = new CreaterCursor();
                            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                            CLIENTEEntity CLIENTETy2 = new CLIENTEEntity();
                            CLIENTETy2 = ClienteP.Read(Convert.ToInt32(item.IDCLIENTE));
                            _IDCLIENTE = CLIENTETy2.IDCLIENTE;
                            SalveIMEXAPP(CLIENTETy2);
                            Contador++;

                            this.Cursor = Cursors.Default;
                        }
                    }

                    MessageBox.Show("Total de Clientes Sicronizados: " + Contador.ToString());
                }
            }
           catch (Exception ex)
           {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na Sicronização de Dados!");
                MessageBox.Show("Erro ténico: " + ex.Message);
           }
       }

       private void extratoDeContaAReceberToolStripMenuItem_Click(object sender, EventArgs e)
       {
           FrmExtratoDuplReceber Frm = new FrmExtratoDuplReceber();
           Frm.CodClienteSelec = _IDCLIENTE;
           Frm.ShowDialog();
       }

       private void maskedtxtCNPJ_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
       {

       }

       private void txtPesquisaRapida_KeyUp(object sender, KeyEventArgs e)
       {
           PesquisaRapida();
       }
       private void PesquisaRapida()
       {

           try
           {
               RowRelatorio.Clear();
               RowRelatorio.Add(new RowsFiltro("NOME", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("BAIRRO1", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("EMAILCLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("CNPJ", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("CPF", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("TELEFONE1", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
               RowRelatorio.Add(new RowsFiltro("TELEFONE2", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

               if (txtPesquisaRapida.Text.Trim() != string.Empty)
               {
                   LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(RowRelatorio, "NOME");
                   DataGriewDados.AutoGenerateColumns = false;
                   DataGriewDados.DataSource = LIS_ClienteColl;

                   lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();
               }

           }
           catch (Exception ex)
           {
               MessageBox.Show("Erro técnico: " + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
           }

       }

       private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
       {
           PesquisaRapida();
       }

       private void linkLabel3_LocationChanged(object sender, EventArgs e)
       {

       }

    }
}