using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using Sintegra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BmsSoftware.UI;


namespace BmsSoftware.Modulos.FrmSintegra
{
    public partial class FrmGerarArquivoSintegra : Form
    {
        LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        LIS_CONHECIMENTOTRANSPCollection LIS_CONHECIMENTOTRANSPColl = new LIS_CONHECIMENTOTRANSPCollection();

        NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
        LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();
        LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();
        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
        LIS_CONHECIMENTOTRANSPProvider LIS_CONHECIMENTOTRANSP = new LIS_CONHECIMENTOTRANSPProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        EMPRESAEntity EMPRESATy = new EMPRESAEntity();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
        int ContadorItens = 0;
        public FrmGerarArquivoSintegra()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaSintegra())
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    ContadorItens = 0;
                    int FinalidadeArquivo = Convert.ToInt32(cbFinalidadeArquivo.SelectedValue);
                    int NaturezaInformacao = Convert.ToInt32(cbNaturezaInformacao.SelectedValue);
                    int CodigoConvenio = Convert.ToInt32(cbCodigoConvenio.SelectedValue);

                    EMPRESATy = EMPRESAP.Read(1);
                    
                    gerarArquivoSintegra(Convert.ToDateTime(dtpkInicial.Text), Convert.ToDateTime(dtpkFim.Text),
                            FinalidadeArquivo, NaturezaInformacao, CodigoConvenio);
                    

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private Boolean ValidaSintegra()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNomeContato.Text.TrimEnd() == string.Empty)
            {
                errorProvider1.SetError(label7, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }


        public DataTable GetFinalidade()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("nome", typeof(string)));
            list.Columns.Add(new DataColumn("codigo", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());

            list.Rows[0][0] = "Normal";
            list.Rows[0][1] = "1";
            list.Rows[1][0] = "Retificação total de arquivo";
            list.Rows[1][1] = "2";
            list.Rows[2][0] = "Retificação aditiva de arquivo";
            list.Rows[2][1] = "3";
            list.Rows[3][0] = "Desfazimento";
            list.Rows[3][1] = "5";

            return list;
        }

        public DataTable GetNatInfo()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("nome", typeof(string)));
            list.Columns.Add(new DataColumn("codigo", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "Totalidade das operações do informante";
            list.Rows[0][1] = "3";
            list.Rows[1][0] = "Interestaduais - Operações com ou sem Substituição Tributária";
            list.Rows[1][1] = "2";
            list.Rows[2][0] = "Interestaduais - Somente operações sujeitas ao regime de Substituição Tributária";
            list.Rows[2][1] = "1";


            return list;
        }

        public DataTable GetCodConvenio()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("nome", typeof(string)));
            list.Columns.Add(new DataColumn("codigo", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "Convênio 57/95 Versão 69/02 Alt. 142/02";
            list.Rows[0][1] = "2";
            list.Rows[1][0] = "Convênio 57/95 Versão 31/99 Alt. 30/02";
            list.Rows[1][1] = "1";
            list.Rows[2][0] = "Convênio 57/95 Alt. 76/03";
            list.Rows[2][1] = "3";

            return list;
        }


        private void FrmGerarArquivoSintegra_Load(object sender, EventArgs e)
        {

            btnSair.Image = Util.GetAddressImage(21);

            //Preenche Finalidade
            cbFinalidadeArquivo.DataSource = GetFinalidade();
            cbFinalidadeArquivo.DisplayMember = "nome";
            cbFinalidadeArquivo.ValueMember = "codigo";
            cbFinalidadeArquivo.SelectedIndex = 0;
            cbFinalidadeArquivo.DropDownStyle = ComboBoxStyle.DropDownList;

            //Codigo Convenio
            cbCodigoConvenio.DataSource = GetCodConvenio();
            cbCodigoConvenio.DisplayMember = "nome";
            cbCodigoConvenio.ValueMember = "codigo";
            cbCodigoConvenio.SelectedIndex = 2;
            cbCodigoConvenio.DropDownStyle = ComboBoxStyle.DropDownList;

            //Codigo Convenio
            cbNaturezaInformacao.DataSource = GetNatInfo();
            cbNaturezaInformacao.DisplayMember = "nome";
            cbNaturezaInformacao.ValueMember = "codigo";
            cbNaturezaInformacao.SelectedIndex = 0;
            cbNaturezaInformacao.DropDownStyle = ComboBoxStyle.DropDownList;

            cbCodigoConvenio.SelectedIndex = 2;
            cbFinalidadeArquivo.SelectedIndex = 0;
            cbNaturezaInformacao.SelectedIndex = 0;
            cbNaturezaInformacao.DropDownStyle = ComboBoxStyle.DropDownList;

            if (CONFISISTEMAP.Read(1).FLAGCPDIGISAT.Trim() == "S")
            {
                chkRegistro60M.Checked = true;
                chkRegistro60A.Checked = true;
                chkRegistro60D.Checked = false;
                chkRegistro60I.Checked = false;
                chkRegistro60R.Checked = false;
            }
            else
            {
                chkRegistro60M.Checked = false;
                chkRegistro60A.Checked = false;
                chkRegistro60D.Checked = false;
                chkRegistro60I.Checked = false;
                chkRegistro60R.Checked = false;

                chkRegistro60M.Enabled = false;
                chkRegistro60A.Enabled = false;
                chkRegistro60D.Enabled = false;
                chkRegistro60I.Enabled = false;
                chkRegistro60R.Enabled = false;
            }
        }

        string NumeroNotaErro = string.Empty;
        string NumeroNotaErroEntrada = string.Empty;
        public void gerarArquivoSintegra(DateTime dataInicio, DateTime dataFim, int finalidadeArquivo, int naturezaInformacao, int codigoConvenio)
        {
            try
            {
                Sintegra.Sintegra sintegra = new Sintegra.Sintegra(dataInicio, dataFim, 1);

                CFOPProvider CFOPP = new CFOPProvider();

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

                CONFISISTEMATy = CONFISISTEMAP.Read(1);               

                Reg10 registro10 = new Reg10();
                registro10.cnpj = EMPRESATy.CNPJCPF;
                registro10.inscricaoestadual = EMPRESATy.IE;
                registro10.nomecontribuinte = EMPRESATy.NOMECLIENTE;
                registro10.municipio = EMPRESATy.CIDADE;
                registro10.uf = EMPRESATy.UF;
                registro10.fax = string.Empty;
                registro10.codigoFinalidadeArqMagnetico = finalidadeArquivo;
                registro10.codigoIdentificacaoNatOp = naturezaInformacao;
                registro10.codigoIdentificacaoConvenio = codigoConvenio;
                registro10.dataInicial = dataInicio;
                registro10.dataFinal = dataFim;
                registro10.tipo = 10;

                sintegra.reg10 = registro10;

                Reg11 registro11 = new Reg11();
                registro11.bairro = EMPRESATy.BAIRRO;
                registro11.cep = EMPRESATy.CEP;
                registro11.complemento = EMPRESATy.COMPLEMENTO;
                registro11.logradouro = EMPRESATy.ENDERECO;

                if (!ValidacoesLibrary.ValidaTipoInt32(EMPRESATy.NUMERO))
                {
                    MessageBox.Show("Erro no Registro 11 e campo Numero",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

                    registro11.numero = 1;
                }
                else
                    registro11.numero = Convert.ToInt32(EMPRESATy.NUMERO);

                registro11.nomeContato = txtNomeContato.Text;
                string telefone = Util.RetiraLetras(EMPRESATy.TELEFONE);
                registro11.telefone = telefone;
                registro11.tipo = 11;

                sintegra.reg11 = registro11;

                List<Reg50> listaRegistro50 = new List<Reg50>();

                //Filtra Produtos Nota Fiscal
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO");
                

                // Remove CFOP, Aliq. ICMS  e nota fiscal repetido
                LIS_PRODUTONFECollection LIS_PRODUTONFE_2_2Coll = new LIS_PRODUTONFECollection();
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {

                     if (LIS_PRODUTONFE_2_2Coll.Find(delegate(LIS_PRODUTONFEEntity item2)
                     { return (item2.CODCFOP == item.CODCFOP && item2.ALICMS == item.ALICMS && item2.NOTAFISCALE == item.NOTAFISCALE); }) == null)
                    {
                        LIS_PRODUTONFE_2_2Coll.Add(item);
                    }
                }
                LIS_PRODUTONFE_2Coll.Clear();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFE_2_2Coll;


                decimal total = 0;             

                foreach (LIS_PRODUTONFEEntity item_2 in LIS_PRODUTONFE_2Coll)
                {

                    NumeroNotaErro = item_2.NOTAFISCALE;

                    Reg50 registro50 = new Reg50();

                    NOTAFISCALEEntity NOTAFISCALETY = new NOTAFISCALEEntity();
                    NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                    NOTAFISCALETY = NOTAFISCALEP.Read(Convert.ToInt32(item_2.IDNOTAFISCALE));

                    //Dados do Cliente
                    CLIENTEEntity CLIENTETY = new CLIENTEEntity();
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    CLIENTETY = CLIENTEP.Read(Convert.ToInt32(NOTAFISCALETY.IDCLIENTE));

                    if (Util.RetiraLetras(CLIENTETY.CNPJ).Count() > 0)
                    {
                        registro50.cnpj = Util.RetiraLetras(CLIENTETY.CNPJ);

                        if(CLIENTETY.IE.Trim() != string.Empty)
                            registro50.inscricaoestadual = CLIENTETY.IE;
                        else
                            registro50.inscricaoestadual = "ISENTO";
                    }
                    else
                    {
                        registro50.cnpj = Util.RetiraLetras(CLIENTETY.CPF);
                        registro50.inscricaoestadual = "ISENTO";
                    }

                    

                   // if (CLIENTETY.IE != string.Empty)
                   //     registro50.inscricaoestadual = Util.RetiraLetras(CLIENTETY.IE);
                    //else
                    //    registro50.inscricaoestadual = "ISENTO";
                   
                    registro50.dataEmissaoRecebimento = Convert.ToDateTime(NOTAFISCALETY.DTEMISSAO);

                    if (CLIENTETY.COD_MUN_IBGE != null)
                    {
                        MUNICIPIOSProvider MUNICIPIOSP = new MUNICIPIOSProvider();
                        MUNICIPIOSEntity MUNICIPIOSTy = new MUNICIPIOSEntity();
                        MUNICIPIOSTy = MUNICIPIOSP.Read(Convert.ToInt32(CLIENTETY.COD_MUN_IBGE));

                        ESTADOProvider ESTADOP = new ESTADOProvider();
                        string ESTADO = ESTADOP.Read(Convert.ToInt32(MUNICIPIOSTy.COD_UF_IBGE)).UF;
                        registro50.uf = ESTADO;
                    }
                    else
                        registro50.uf = EMPRESATy.UF.TrimEnd();

                    registro50.modelo = CONFISISTEMATy.MODELONFE;
                    registro50.numero = Convert.ToInt32(NOTAFISCALETY.NOTAFISCALE);
                    registro50.serie = CONFISISTEMATy.SERIENFE;
                    registro50.cfop = item_2.CODCFOP;
                    registro50.emitente = "P"; //P Proprio ; T Terceiros

                    if (NOTAFISCALETY.FLAGCANCELADA.TrimEnd() == "N" && NOTAFISCALETY.FLAGINUTILIZADO == "N" && NOTAFISCALETY.FLAGENVIADA == "S")
                    {
                        registro50.valorTotal = TotalNFSaidaReg50(item_2.CODCFOP, Convert.ToDecimal(item_2.ALICMS), item_2.NOTAFISCALE);
                        //registro50.valorTotal = Convert.ToDecimal(NOTAFISCALETY.TOTALNOTA);
                        //registro50.baseCalculoICMS = TotalBASECALCULO(Convert.ToInt32(item_2.IDCFOP), Convert.ToDecimal(item_2.ALICMS), item_2.NOTAFISCALE);
                        registro50.baseCalculoICMS = TotalBASECALCULO(Convert.ToInt32(item_2.IDCFOP), Convert.ToDecimal(item_2.ALICMS), item_2.NOTAFISCALE);
                        registro50.valorICMS = TotalICMSMFE(Convert.ToInt32(item_2.IDCFOP), Convert.ToDecimal(item_2.ALICMS), item_2.NOTAFISCALE);
                    }
                    else
                    {
                        registro50.valorTotal = 0;
                        registro50.baseCalculoICMS = 0;
                        registro50.valorICMS = 0;
                    }

                    registro50.isentaOuNaoTributada = 0;
                    registro50.outras = Convert.ToDecimal(NOTAFISCALETY.OUTRADESPES);
                    registro50.aliquota = Convert.ToDecimal(item_2.ALICMS);
                    registro50.situacaoCancelamento = NOTAFISCALETY.FLAGCANCELADA.TrimEnd();

                    listaRegistro50.Add(registro50);
                }               

                //Movimentação de Entrada               
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl2 = new LIS_MOVPRODUTOESCollection();
                //  Remove CFOP repetido e Aliq. ICMS                   
                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {

                    if (LIS_MOVPRODUTOESColl2.Find(delegate(LIS_MOVPRODUTOESEntity item2)
                    { return (item2.CODCFOP == item.CODCFOP && item2.ALQICMS == item.ALQICMS && item2.NDOCUMENTO == item.NDOCUMENTO); }) == null)
                    {
                        LIS_MOVPRODUTOESColl2.Add(item);
                    }
                }

                LIS_MOVPRODUTOESColl.Clear();
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESColl2;


                foreach (LIS_MOVPRODUTOESEntity item2 in LIS_MOVPRODUTOESColl)
                {
                    Reg50 registro50 = new Reg50();

                    NumeroNotaErroEntrada = item2.NDOCUMENTO;

                    if (item2.NDOCUMENTO == "585647")
                        NumeroNotaErroEntrada = item2.NDOCUMENTO;

                    //Dados do MovEstoque
                    ESTOQUEESEntity ESTOQUEESTy = new ESTOQUEESEntity();
                    ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
                    ESTOQUEESTy = ESTOQUEESP.Read(Convert.ToInt32(item2.IDESTOQUEES));

                    //Dados do Cliente
                    FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                    FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                    FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(ESTOQUEESTy.IDFORNECEDOR));

                    registro50.cnpj =Util.RetiraLetras(FORNECEDORTy.CNPJ);

                    registro50.inscricaoestadual = Util.RetiraLetras(FORNECEDORTy.IE);
                    registro50.dataEmissaoRecebimento = Convert.ToDateTime(item2.DATAMOVIM);
                    registro50.uf = FORNECEDORTy.UF;
                    registro50.modelo = CONFISISTEMATy.MODELONFE;
                    registro50.numero = Convert.ToInt32(Util.RetiraLetras(ESTOQUEESTy.NDOCUMENTO));
                    registro50.serie = CONFISISTEMATy.SERIENFE;
                    registro50.cfop = item2.CODCFOP;
                    registro50.emitente = "T"; //P Proprio ; T Terceiros
                    registro50.valorTotal = TotalNFEntradaReg50(item2.CODCFOP, Convert.ToDecimal(item2.ALQICMS), item2.NDOCUMENTO);
                    registro50.baseCalculoICMS = BaseCalculoNFEntrada(Convert.ToInt32(item2.IDCFOP), Convert.ToDecimal(item2.ALQICMS), item2.NDOCUMENTO);
                    registro50.valorICMS = ICMSNFEntrada(Convert.ToInt32(item2.IDCFOP), Convert.ToDecimal(item2.ALQICMS), item2.NDOCUMENTO);
                    registro50.isentaOuNaoTributada = 0;
                    registro50.outras = Convert.ToDecimal(ESTOQUEESTy.VALORFRETE);
                    registro50.aliquota = Convert.ToDecimal(item2.ALQICMS);
                    registro50.situacaoCancelamento = "N";

                    listaRegistro50.Add(registro50);
                }

                sintegra.regs50 = listaRegistro50;

                List<Reg54> listaRegistro54 = new List<Reg54>();
                List<Reg75> listaRegistro75 = new List<Reg75>();
                
                //Registro 54
                if (chkregistro54.Checked)
                {
                    //Nota fiscal de saida  registro 54 
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));
                    RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                    if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    {
                        RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                    }

                    LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO DESC");
                    

                    int NotaAnterior = 0;
                    foreach (LIS_PRODUTONFEEntity item_5 in LIS_PRODUTONFE_2Coll)
                    {

                        PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
                        PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item_5.IDPRODUTO));

                        // if (PRODUTOStY.FLAGNAOSINTEGRASPED.Trim() != "N")
                        {
                            NumeroNotaErro = item_5.NOTAFISCALE.ToString();
                            NOTAFISCALEEntity NOTAFISCALETY = new NOTAFISCALEEntity();
                            NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                            NOTAFISCALETY = NOTAFISCALEP.Read(Convert.ToInt32(item_5.IDNOTAFISCALE));

                            //Dados do Cliente
                            CLIENTEEntity CLIENTETY = new CLIENTEEntity();
                            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                            CLIENTETY = CLIENTEP.Read(Convert.ToInt32(NOTAFISCALETY.IDCLIENTE));

                            Reg54 registro54 = new Reg54();
                            
                            registro54.tipo = 54;

                            if(Util.RetiraLetras(CLIENTETY.CNPJ).Count() > 0 )
                                registro54.cnpj = CLIENTETY.CNPJ;
                            else
                                registro54.cnpj = CLIENTETY.CPF;

                            registro54.modelo = CONFISISTEMATy.MODELONFE;
                            registro54.serie = CONFISISTEMATy.SERIENFE;
                            registro54.numero = Convert.ToInt32(Util.RetiraLetras(item_5.NOTAFISCALE));
                            registro54.cfop = item_5.CODCFOP;
                            registro54.cst = item_5.CODCOMPL;

                            if (NotaAnterior != registro54.numero)
                                ContadorItens = 1;
                            else
                                ContadorItens++;

                            registro54.numeroItem = ContadorItens.ToString().PadLeft(3, '0');
                            NotaAnterior = registro54.numero;

                            registro54.codigoProduto = item_5.IDPRODUTO.ToString();
                            registro54.qtd = Convert.ToDecimal(item_5.QUANTIDADE);
                         
                            // registro54.valorProduto = Convert.ToDecimal(item_5.VALORTOTAL);
                           //Vaçpr dp produto menos o desconto
                            registro54.valorProduto = Convert.ToDecimal(item_5.VALORTOTAL) - Convert.ToDecimal(item_5.DESCONTOPRODUTO);

                            registro54.valorDescontoDespAcessoria = Convert.ToDecimal(item_5.DESCONTOPRODUTO);
                            
                            
                            //registro54.baseCalcICMS = Convert.ToDecimal(item_5.BASEICMS) ;
                            //Base de Calculo com desconto do produto
                            registro54.baseCalcICMS = Convert.ToDecimal(item_5.BASEICMS) - Convert.ToDecimal(item_5.DESCONTOPRODUTO);

                            registro54.baseCalcIcmsST = Convert.ToDecimal(item_5.VLBASEST);
                            registro54.valorIPI = Convert.ToDecimal(item_5.VALORIPI);
                            registro54.aliqICMS = Convert.ToDecimal(item_5.ALICMS);

                            listaRegistro54.Add(registro54);

                        }
                    }

                    sintegra.regs54 = listaRegistro54;


                    //Nota fiscal de Entrada  registro 54     
                    //Movimentação de Entrada               
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                    if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    {
                        RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                    }

                    LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");
                    LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl_2 = new LIS_MOVPRODUTOESCollection();
                    //Remove Energia e telecomunicação
                    foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl)
                    {
                        if (item_2.FLAGENERGIATELECOM != "S")
                            LIS_MOVPRODUTOESColl_2.Add(item_2);
                    }

                    LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESColl_2;

                    NotaAnterior = 0;

                    foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl)
                    {
                        PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
                        PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item_2.IDPRODUTO));

                       
                            NumeroNotaErroEntrada = item_2.NDOCUMENTO;
                            ESTOQUEESEntity ESTOQUETy = new ESTOQUEESEntity();
                            ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
                            ESTOQUETy = ESTOQUEESP.Read(Convert.ToInt32(item_2.IDESTOQUEES));

                            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                            FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(ESTOQUETy.IDFORNECEDOR));


                            Reg54 registro54 = new Reg54();
                            registro54.tipo = 54;
                            registro54.cnpj = Util.RetiraLetras(FORNECEDORTy.CNPJ);
                            registro54.modelo = CONFISISTEMATy.MODELONFE;
                            registro54.serie = CONFISISTEMATy.SERIENFE;
                            registro54.numero = Convert.ToInt32(Util.RetiraLetras(Util.RetiraLetras(ESTOQUETy.NDOCUMENTO)));
                            registro54.cfop = item_2.CODCFOP;
                            registro54.cst = item_2.CST_CSOSN;

                            if (NotaAnterior != registro54.numero)
                                ContadorItens = 1;
                            else
                                ContadorItens++;

                            registro54.numeroItem = ContadorItens.ToString().PadLeft(3, '0');
                            NotaAnterior = registro54.numero;

                            registro54.codigoProduto = item_2.IDPRODUTO.ToString();
                            registro54.qtd = Convert.ToDecimal(item_2.QUANTIDADE);
                        
                            //Valor do produto menos o desconto
                             registro54.valorProduto = Convert.ToDecimal(item_2.VALORTOTAL) - Convert.ToDecimal(item_2.VLDESCONTOPRODUTO);

                            registro54.valorDescontoDespAcessoria = 0;
                            
                            //registro54.baseCalcICMS = Convert.ToDecimal(item_2.BASEICMS);
                            //Base de Calculo menos o desconto
                            registro54.baseCalcICMS = Convert.ToDecimal(item_2.BASEICMS) - Convert.ToDecimal(item_2.VLDESCONTOPRODUTO);

                            registro54.baseCalcIcmsST = Convert.ToDecimal(item_2.VLBASEICMSST);

                            if (item_2.VLIPI != null)
                                registro54.valorIPI = Convert.ToDecimal(item_2.VLIPI);
                            else
                                registro54.valorIPI = 0;

                            registro54.aliqICMS = Convert.ToDecimal(item_2.ALQICMS);

                            listaRegistro54.Add(registro54);                          

                    }

                    sintegra.regs54 = listaRegistro54;
                    
                }


                //Registro 75 
                if (chkregistro54.Checked)
                {
                    //Array para armazenar ID de produto
                    int[] IDProduto = new int[LIS_MOVPRODUTOESColl.Count + LIS_PRODUTONFE_2Coll.Count];

                    int i = 0;
                    foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl)
                    {
                        IDProduto[i] = Convert.ToInt32(item_2.IDPRODUTO);
                        i++;
                    }

                    foreach (LIS_PRODUTONFEEntity item_6 in LIS_PRODUTONFE_2Coll)
                    {
                        IDProduto[i] = Convert.ToInt32(item_6.IDPRODUTO);
                        i++;
                    }

                    //elimina ID repetido no array
                    int[] IDProduto2 = IDProduto.Distinct().ToArray();

                    //Percorre o Array

                    PRODUTOSEntity PRODUTOSTy2 = new PRODUTOSEntity();
                    for (int j = 0; j < IDProduto2.Length; j++)
                    {
                        //Registro 74                       
                        PRODUTOSTy2 = PRODUTOSP.Read(IDProduto2[j]);

                        if (PRODUTOSTy2 != null)
                        {
                            Reg75 registro75 = new Reg75();
                            registro75.dataInicial = Convert.ToDateTime(dtpkInicial.Text);
                            registro75.dataFinal = Convert.ToDateTime(dtpkFim.Text);
                            registro75.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);
                            registro75.codigoNCM = PRODUTOSTy2.NCMSH;
                            registro75.descricao = PRODUTOSTy2.NOMEPRODUTO.Replace("´", "");

                            if (PRODUTOStY.IDUNIDADE != null)
                            {
                                //unidade
                                UNIDADEEntity UNIDADETy = new UNIDADEEntity();
                                UNIDADEProvider UNIDADEProvider = new UNIDADEProvider();
                                UNIDADETy = UNIDADEProvider.Read(Convert.ToInt32(PRODUTOSTy2.IDUNIDADE));
                                registro75.unidadeMedidaComercializacao = UNIDADETy.NOME;
                            }
                            else
                            {
                                registro75.unidadeMedidaComercializacao = "UND";
                            }

                            registro75.aliqIPI = Convert.ToDecimal(PRODUTOSTy2.IPI);
                            registro75.aliqICMS = Convert.ToDecimal(PRODUTOSTy2.ICMS);
                            registro75.redBaseCalcICMS = 0; ;
                            registro75.baseCalcIcmsST = 0;

                            listaRegistro75.Add(registro75);

                        }
                    }

                    sintegra.regs75 = listaRegistro75;
                }


                
                //Registro 70 - Conhecimento de transporte
                List<Reg70> listaRegistro70 = new List<Reg70>();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));

                LIS_CONHECIMENTOTRANSPColl = LIS_CONHECIMENTOTRANSP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATA");

                foreach (LIS_CONHECIMENTOTRANSPEntity item_2 in LIS_CONHECIMENTOTRANSPColl)
                {
                    Reg70 registro70 = new Reg70();

                    TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
                    TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
                    TRANSPORTADORATy = TRANSPORTADORAP.Read(Convert.ToInt32(item_2.IDTRANSPORTADORA));

                    CFOPEntity CFOPTy = new CFOPEntity();
                    CFOPTy = CFOPP.Read(Convert.ToInt32(item_2.CODCFOP));

                    registro70.cnpj = Util.RetiraLetras(TRANSPORTADORATy.CNPJ);

                    if (Util.RetiraLetras(TRANSPORTADORATy.CNPJ).Count() > 0)
                    {
                        if (TRANSPORTADORATy.IE.Trim() != string.Empty)
                            registro70.inscricaoEstadual = TRANSPORTADORATy.IE;
                        else
                            registro70.inscricaoEstadual = "ISENTO";
                    }
                    else
                        MessageBox.Show("Transportadora: " + TRANSPORTADORATy.NOME + " sem CNPJ!");
                    
                    registro70.dataEmissao = Convert.ToDateTime(item_2.DATA);
                    registro70.UnidadeFederacao = TRANSPORTADORATy.UF;
                    registro70.Modelo = item_2.MODELO;
                    registro70.Serie = item_2.SERIE;
                    registro70.Subserie = string.Empty;
                    registro70.Numero = Convert.ToInt32(Util.RetiraLetras(item_2.NDOCUMENTO));
                    registro70.CFOP = item_2.CODCFOP;
                    registro70.valorTotal = Convert.ToDecimal(item_2.VLTOTAL);
                    registro70.BaseCalculo = Convert.ToDecimal(item_2.VLBASEICMS);
                    registro70.ValorICMS = Convert.ToDecimal(item_2.VLICMS);
                    registro70.Isenta_nao_tributada = 0;
                    registro70.Outras = Convert.ToDecimal(item_2.OUTRAS);

                    if (item_2.MODALIDADE != string.Empty)
                        registro70.Modalidade = Convert.ToInt32(item_2.MODALIDADE);
                    else
                        registro70.Modalidade = 1;

                    registro70.Situacao = "N";
                    listaRegistro70.Add(registro70);
                }

                sintegra.regs70 = listaRegistro70;

                List<Reg74> listaRegistro74 = new List<Reg74>();
                //Somente se marcar registro 54 vai exibir registro 75

                //List<Reg75> listaRegistro75 = new List<Reg75>();
                if (chkInventario.Checked)
                {

                    RowRelatorio.Clear();
                    string DataInicial = string.Empty;
                    string DataFinal = string.Empty;
                    string AnoInventario = string.Empty;

                    AnoInventario = InputBox("Ano do Inventário", ConfigSistema1.Default.NomeEmpresa, "Digita o Ano do do Inventario");
                    DataInicial = "01/01/" + AnoInventario;
                    DataFinal = "31/12/" + AnoInventario;

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                    if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    {
                        RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                    }
                    
                    LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO");

                    //Registro 75 
                    //Produto da Nota Fiscal
                    //Remove Produto repetido
                    LIS_PRODUTONFECollection LIS_PRODUTONFE_3Coll = new LIS_PRODUTONFECollection();
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                    {

                        if (LIS_PRODUTONFE_3Coll.Find(delegate(LIS_PRODUTONFEEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                        {
                            LIS_PRODUTONFE_3Coll.Add(item);
                        }
                    }

                    //Movimentação de Entrada   
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                    if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    {
                        RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                    }

                    LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                    //Remove Produto repetido
                    LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOES2Coll = new LIS_MOVPRODUTOESCollection();
                    foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl)
                    {

                        if (LIS_MOVPRODUTOES2Coll.Find(delegate(LIS_MOVPRODUTOESEntity item2) { return (item2.IDPRODUTO == item_2.IDPRODUTO); }) == null)
                        {
                            LIS_MOVPRODUTOES2Coll.Add(item_2);
                        }
                    }

                    //Array para armazenar ID de produto
                    int[] IDProduto = new int[LIS_MOVPRODUTOES2Coll.Count + LIS_PRODUTONFE_3Coll.Count];

                    int i = 0;
                    foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOES2Coll)
                    {
                        IDProduto[i] = Convert.ToInt32(item_2.IDPRODUTO);
                        i++;
                    }

                    foreach (LIS_PRODUTONFEEntity item_6 in LIS_PRODUTONFE_3Coll)
                    {
                        IDProduto[i] = Convert.ToInt32(item_6.IDPRODUTO);
                        i++;
                    }

                    //elimina ID repetido no array
                    int[] IDProduto2 = IDProduto.Distinct().ToArray();


                    //Percorre o Array
                    PRODUTOSEntity PRODUTOSTy2 = new PRODUTOSEntity();
                    for (int j = 0; j < IDProduto2.Length; j++)
                    {
                        //Registro 74                       
                        PRODUTOSTy2 = PRODUTOSP.Read(IDProduto2[j]);

                        if (PRODUTOSTy2 != null)
                        {
                            Decimal EstoqueAtual = Util.EstoqueAtual(PRODUTOSTy2.IDPRODUTO, DataFinal, true);
                            ErroCampo = "Erro no produto: " + PRODUTOSTy2.IDPRODUTO.ToString() + " - " + PRODUTOSTy2.NOMEPRODUTO;
                            if (PRODUTOSTy2.FLAGNAOSINTEGRASPED != null && PRODUTOSTy2.FLAGNAOSINTEGRASPED.Trim() != "S")
                            {
                                if (EstoqueAtual > 0)
                                {
                                    Reg74 registro74 = new Reg74();
                                    registro74.tipo = 74;
                                    registro74.dataInventario = Convert.ToDateTime(DataFinal);
                                    registro74.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);

                                    if (registro74.codigoProduto == 23)
                                    {
                                        registro74.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);
                                    }

                                    registro74.qtd = EstoqueAtual;
                                    string ValorCustoinicial = Convert.ToDecimal(PRODUTOSTy2.VALORCUSTOINICIAL).ToString("n4");
                                    registro74.valorProduto = Convert.ToDecimal(ValorCustoinicial) * EstoqueAtual;

                                    if (chkInventario.Checked && Convert.ToDecimal(PRODUTOSTy2.VALORCUSTOINICIAL) == 0)
                                    {
                                        MessageBox.Show("Produto: " + PRODUTOSTy2.IDPRODUTO.ToString() + " - " + PRODUTOSTy2.NOMEPRODUTO + " com o valor de custo inicial zero!");
                                    }

                                    registro74.codigoPoseMercadoriaInventariadas = 1;
                                    registro74.cnpjPossuidorMercadoria = Util.RetiraLetras(EMPRESATy.CNPJCPF);
                                    registro74.inscricaoEstadualProprietario = Util.RetiraLetras(EMPRESATy.IE);
                                    registro74.ufProprietario = EMPRESATy.UF;
                                    listaRegistro74.Add(registro74);

                                    Reg75 registro75 = new Reg75();
                                    //Registro 75
                                    registro75.dataInicial = Convert.ToDateTime(DataInicial);
                                    registro75.dataFinal = Convert.ToDateTime(DataFinal);
                                    registro75.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);
                                    registro75.codigoNCM = PRODUTOSTy2.NCMSH;
                                    registro75.descricao = PRODUTOSTy2.NOMEPRODUTO.Replace("´", "");

                                    if (PRODUTOStY.IDUNIDADE != null)
                                    {
                                        //unidade
                                        UNIDADEEntity UNIDADETy = new UNIDADEEntity();
                                        UNIDADEProvider UNIDADEProvider = new UNIDADEProvider();
                                        UNIDADETy = UNIDADEProvider.Read(Convert.ToInt32(PRODUTOSTy2.IDUNIDADE));
                                        registro75.unidadeMedidaComercializacao = UNIDADETy.NOME;
                                    }
                                    else
                                    {
                                        registro75.unidadeMedidaComercializacao = "UND";
                                    }


                                    registro75.aliqIPI = Convert.ToDecimal(PRODUTOSTy2.IPI);
                                    registro75.aliqICMS = Convert.ToDecimal(PRODUTOSTy2.ICMS);
                                    registro75.redBaseCalcICMS = 0; ;
                                    registro75.baseCalcIcmsST = 0;

                                    listaRegistro75.Add(registro75);
                                }
                            }
                        }
                    }

                    sintegra.regs75 = listaRegistro75;
                    sintegra.regs74 = listaRegistro74;

                }


                Reg90 registro90 = new Reg90();
                registro90.tipo = 90;
                registro90.cnpj = EMPRESATy.CNPJCPF;
                registro90.inscricaoEstadual = EMPRESATy.IE;
                registro90.numeroRegistrosTipo90 = "1";
                registro90.tipoTotalizado = 50;
                registro90.totalRegistos = listaRegistro50.Count.ToString();

                registro90.totalGeralRegistros = listaRegistro50.Count + 3 + listaRegistro54.Count + listaRegistro70.Count + listaRegistro75.Count + listaRegistro74.Count;

                sintegra.regs90 = new List<Reg90> { registro90 };

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Arquivos SINTEGRA (*.txt) | *.txt";
                dialog.Title = "Selecione o arquivo";
                dialog.FileName = "SINTEGRA.txt";
                dialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    sintegra.gerarArquivoSintegra(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);

                if (NumeroNotaErro != string.Empty)
                    MessageBox.Show("Erro na Nota Fiscal Saída : " + NumeroNotaErro);

                if (NumeroNotaErroEntrada != string.Empty)
                    MessageBox.Show("Erro na Nota Fiscal Entrada : " + NumeroNotaErroEntrada);
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

        string ErroCampo = string.Empty;
        public void gerarArquivoSintegraInventario(DateTime dataInicio, DateTime dataFim, int finalidadeArquivo, int naturezaInformacao, int codigoConvenio)
        {
            try
            {
                Sintegra.Sintegra sintegra = new Sintegra.Sintegra(dataInicio, dataFim, 1);

                CFOPProvider CFOPP = new CFOPProvider();

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                Reg10 registro10 = new Reg10();
                registro10.cnpj = EMPRESATy.CNPJCPF;
                registro10.inscricaoestadual = EMPRESATy.IE;
                registro10.nomecontribuinte = EMPRESATy.NOMECLIENTE;
                registro10.municipio = EMPRESATy.CIDADE;
                registro10.uf = EMPRESATy.UF;
                registro10.fax = string.Empty;
                registro10.codigoFinalidadeArqMagnetico = finalidadeArquivo;
                registro10.codigoIdentificacaoNatOp = naturezaInformacao;
                registro10.codigoIdentificacaoConvenio = codigoConvenio;
                registro10.dataInicial = dataInicio;
                registro10.dataFinal = dataFim;
                registro10.tipo = 10;

                sintegra.reg10 = registro10;

                Reg11 registro11 = new Reg11();
                registro11.bairro = EMPRESATy.BAIRRO;
                registro11.cep = EMPRESATy.CEP;
                registro11.complemento = EMPRESATy.COMPLEMENTO;
                registro11.logradouro = EMPRESATy.ENDERECO;

                if (!ValidacoesLibrary.ValidaTipoInt32(EMPRESATy.NUMERO))
                {
                    MessageBox.Show("Erro no Registro 11 e campo Numero",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

                    registro11.numero = 1;
                }
                else
                    registro11.numero = Convert.ToInt32(EMPRESATy.NUMERO);

                registro11.nomeContato = txtNomeContato.Text;
                string telefone = Util.RetiraLetras(EMPRESATy.TELEFONE);
                registro11.telefone = telefone;
                registro11.tipo = 11;

                sintegra.reg11 = registro11;

                List<Reg50> listaRegistro50 = new List<Reg50>();
                List<Reg54> listaRegistro54 = new List<Reg54>();
                List<Reg70> listaRegistro70 = new List<Reg70>();

                List<Reg75> listaRegistro75 = new List<Reg75>();
                List<Reg74> listaRegistro74 = new List<Reg74>();

                RowRelatorio.Clear();

                string AnoInventario = InputBox("Ano do Inventário", ConfigSistema1.Default.NomeEmpresa, "Digita o Ano do do Inventario");
                string DataInicial = "01/01/" + AnoInventario;
                string DataFinal = "31/12/" + AnoInventario;

                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO");

                //Registro 75 
                //Produto da Nota Fiscal
                //Remove Produto repetido
                LIS_PRODUTONFECollection LIS_PRODUTONFE_3Coll = new LIS_PRODUTONFECollection();
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {

                    if (LIS_PRODUTONFE_3Coll.Find(delegate(LIS_PRODUTONFEEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                    {
                        LIS_PRODUTONFE_3Coll.Add(item);
                    }
                }

                //Movimentação de Entrada   
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                //Remove Produto repetido
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOES2Coll = new LIS_MOVPRODUTOESCollection();
                foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl)
                {

                    if (LIS_MOVPRODUTOES2Coll.Find(delegate(LIS_MOVPRODUTOESEntity item2) { return (item2.IDPRODUTO == item_2.IDPRODUTO); }) == null)
                    {
                        LIS_MOVPRODUTOES2Coll.Add(item_2);
                    }
                }

                //Array para armazenar ID de produto
                int[] IDProduto = new int[LIS_MOVPRODUTOES2Coll.Count + LIS_PRODUTONFE_3Coll.Count];

                int i = 0;
                foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOES2Coll)
                {
                    IDProduto[i] = Convert.ToInt32(item_2.IDPRODUTO);
                    i++;
                }

                foreach (LIS_PRODUTONFEEntity item_6 in LIS_PRODUTONFE_3Coll)
                {
                    IDProduto[i] = Convert.ToInt32(item_6.IDPRODUTO);
                    i++;
                }

                //elimina ID repetido no array
                int[] IDProduto2 = IDProduto.Distinct().ToArray();


                //Percorre o Array
                PRODUTOSEntity PRODUTOSTy2 = new PRODUTOSEntity();
                for (int j = 0; j < IDProduto2.Length; j++)
                {
                    //Registro 74                       
                    PRODUTOSTy2 = PRODUTOSP.Read(IDProduto2[j]);

                    if (PRODUTOSTy2 != null)
                    {
                        Decimal EstoqueAtual = Util.EstoqueAtual(PRODUTOSTy2.IDPRODUTO, true);
                        ErroCampo = "Erro no produto: " + PRODUTOSTy2.IDPRODUTO.ToString() + " - " + PRODUTOSTy2.NOMEPRODUTO;
                        if (PRODUTOSTy2.FLAGNAOSINTEGRASPED != null && PRODUTOSTy2.FLAGNAOSINTEGRASPED.Trim() != "S")
                        {
                            if (EstoqueAtual > 0)
                            {
                                Reg74 registro74 = new Reg74();
                                registro74.tipo = 74;
                                registro74.dataInventario = Convert.ToDateTime(dtpkFim.Text);
                                registro74.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);

                                if (registro74.codigoProduto == 23)
                                {
                                    registro74.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);
                                }

                                registro74.qtd = EstoqueAtual;
                                registro74.valorProduto = Convert.ToDecimal(PRODUTOSTy2.VALORCUSTOINICIAL) * EstoqueAtual;

                                if (chkInventario.Checked && Convert.ToDecimal(PRODUTOSTy2.VALORCUSTOINICIAL) == 0)
                                {
                                    MessageBox.Show("Produto: " + PRODUTOSTy2.IDPRODUTO.ToString() + " - " + PRODUTOSTy2.NOMEPRODUTO + " com o valor de custo inicial zero!");
                                }

                                registro74.codigoPoseMercadoriaInventariadas = 1;
                                registro74.cnpjPossuidorMercadoria = Util.RetiraLetras(EMPRESATy.CNPJCPF);
                                registro74.inscricaoEstadualProprietario = Util.RetiraLetras(EMPRESATy.IE);
                                registro74.ufProprietario = EMPRESATy.UF;
                                listaRegistro74.Add(registro74);

                                Reg75 registro75 = new Reg75();
                                //Registro 75
                                registro75.dataInicial = Convert.ToDateTime(DataInicial);
                                registro75.dataFinal = Convert.ToDateTime(DataFinal);
                                registro75.codigoProduto = Convert.ToInt32(PRODUTOSTy2.IDPRODUTO);
                                registro75.codigoNCM = PRODUTOSTy2.NCMSH;
                                registro75.descricao = PRODUTOSTy2.NOMEPRODUTO.Replace("´", "");

                                if (PRODUTOStY.IDUNIDADE != null)
                                {
                                    //unidade
                                    UNIDADEEntity UNIDADETy = new UNIDADEEntity();
                                    UNIDADEProvider UNIDADEProvider = new UNIDADEProvider();
                                    UNIDADETy = UNIDADEProvider.Read(Convert.ToInt32(PRODUTOSTy2.IDUNIDADE));
                                    registro75.unidadeMedidaComercializacao = UNIDADETy.NOME;
                                }
                                else
                                {
                                    registro75.unidadeMedidaComercializacao = "UND";
                                }


                                registro75.aliqIPI = Convert.ToDecimal(PRODUTOSTy2.IPI);
                                registro75.aliqICMS = Convert.ToDecimal(PRODUTOSTy2.ICMS);
                                registro75.redBaseCalcICMS = 0; ;
                                registro75.baseCalcIcmsST = 0;

                                listaRegistro75.Add(registro75);
                            }
                        }
                    }
                }

                sintegra.regs75 = listaRegistro75;
                sintegra.regs74 = listaRegistro74;




                Reg90 registro90 = new Reg90();
                registro90.tipo = 90;
                registro90.cnpj = EMPRESATy.CNPJCPF;
                registro90.inscricaoEstadual = EMPRESATy.IE;
                registro90.numeroRegistrosTipo90 = "1";
                registro90.tipoTotalizado = 50;
                registro90.totalRegistos = listaRegistro50.Count.ToString();

                registro90.totalGeralRegistros = listaRegistro50.Count + 3 + listaRegistro54.Count + listaRegistro70.Count + listaRegistro75.Count + listaRegistro74.Count;

                sintegra.regs90 = new List<Reg90> { registro90 };

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Arquivos SINTEGRA (*.txt) | *.txt";
                dialog.Title = "Selecione o arquivo";
                dialog.FileName = "SINTEGRA.txt";
                dialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    sintegra.gerarArquivoSintegra(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ErroCampo);

                MessageBox.Show("Erro técnico: " + ex.Message);

                if (NumeroNotaErro != string.Empty)
                    MessageBox.Show("Erro na Nota Fiscal Saída : " + NumeroNotaErro);

                if (NumeroNotaErroEntrada != string.Empty)
                    MessageBox.Show("Erro na Nota Fiscal Entrada : " + NumeroNotaErroEntrada);
            }
        }

        Decimal TotalRegistro60RQuantidade = 0;
        private List<Reg60R> Registro60RDigisat(DateTime dataInicio, DateTime dataFim)
        {
            List<Reg60R> listaRegistro60R = new List<Reg60R>();
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));


                ITEVENDAS_ECFCollection ITEVENDAS_ECFColl = new ITEVENDAS_ECFCollection();
                ITEVENDAS_ECFProvider ITEVENDAS_ECFP = new ITEVENDAS_ECFProvider();
                ITEVENDAS_ECFColl = ITEVENDAS_ECFP.ReadCollectionByParameter(RowRelatorio, "DATA");

                foreach (var item in ITEVENDAS_ECFColl)
                {
                    //Total I - Isento
                    Reg60R registro60R_TRIBI = new Reg60R();
                    registro60R_TRIBI.MesAnoEmissão = Convert.ToDateTime(item.DATA).ToString("MMyyyy");
                    registro60R_TRIBI.codigoProdutoServiço = item.CODIGO.ToString().PadLeft(14, '0');
                    registro60R_TRIBI.quantidade = TotalRegistro60RQuantidade;
                    registro60R_TRIBI.valorProdutoServiço = TotalRegistro60D(dataInicio, dataFim, 0);
                    registro60R_TRIBI.baseCalculoICMS = 0;
                    registro60R_TRIBI.situacaoTributariaAliquotaProdutoServiço = "I   ";

                    if (registro60R_TRIBI.valorProdutoServiço > 0)
                        listaRegistro60R.Add(registro60R_TRIBI);

                    //Total ICMS 7
                    Reg60R registro60R_TRI07 = new Reg60R();
                    registro60R_TRI07.MesAnoEmissão = Convert.ToDateTime(item.DATA).ToString("MMyyyy");
                    registro60R_TRI07.codigoProdutoServiço = item.CODIGO.ToString().PadLeft(14, '0');
                    registro60R_TRI07.quantidade = TotalRegistro60RQuantidade;
                    registro60R_TRI07.valorProdutoServiço = TotalRegistro60D(dataInicio, dataFim, 7);
                    registro60R_TRI07.baseCalculoICMS = registro60R_TRI07.valorProdutoServiço;
                    registro60R_TRI07.situacaoTributariaAliquotaProdutoServiço = "0700";

                    if (registro60R_TRI07.valorProdutoServiço > 0)
                        listaRegistro60R.Add(registro60R_TRI07);

                    //Total ICMS 12
                    Reg60R registro60R_TRI12 = new Reg60R();
                    registro60R_TRI12.MesAnoEmissão = Convert.ToDateTime(item.DATA).ToString("MMyyyy");
                    registro60R_TRI12.codigoProdutoServiço = item.CODIGO.ToString().PadLeft(14, '0');
                    registro60R_TRI12.quantidade = TotalRegistro60RQuantidade;
                    registro60R_TRI12.valorProdutoServiço = TotalRegistro60D(dataInicio, dataFim, 12);
                    registro60R_TRI12.baseCalculoICMS = registro60R_TRI12.valorProdutoServiço;
                    registro60R_TRI12.situacaoTributariaAliquotaProdutoServiço = "1200";

                    if (registro60R_TRI12.valorProdutoServiço > 0)
                        listaRegistro60R.Add(registro60R_TRI12);


                    //Total ICMS 18                    
                    Reg60R registro60R_TRI18 = new Reg60R();
                    registro60R_TRI18.MesAnoEmissão = Convert.ToDateTime(item.DATA).ToString("MMyyyy");
                    registro60R_TRI18.codigoProdutoServiço = item.CODIGO.ToString().PadLeft(14, '0');
                    registro60R_TRI18.quantidade = TotalRegistro60RQuantidade;
                    registro60R_TRI18.valorProdutoServiço = TotalRegistro60D(dataInicio, dataFim, 18);
                    registro60R_TRI18.baseCalculoICMS = registro60R_TRI18.valorProdutoServiço;
                    registro60R_TRI18.situacaoTributariaAliquotaProdutoServiço = "1800";

                    if (registro60R_TRI18.valorProdutoServiço > 0)
                        listaRegistro60R.Add(registro60R_TRI18);

                    //Total ICMS 25
                    Reg60R registro60R_TRI25 = new Reg60R();
                    registro60R_TRI25.MesAnoEmissão = Convert.ToDateTime(item.DATA).ToString("MMyyyy");
                    registro60R_TRI25.codigoProdutoServiço = item.CODIGO.ToString().PadLeft(14, '0');
                    registro60R_TRI25.quantidade = TotalRegistro60RQuantidade;
                    registro60R_TRI25.valorProdutoServiço = TotalRegistro60D(dataInicio, dataFim, 25);
                    registro60R_TRI25.baseCalculoICMS = registro60R_TRI25.valorProdutoServiço;
                    registro60R_TRI25.situacaoTributariaAliquotaProdutoServiço = "2500";

                    if (registro60R_TRI25.valorProdutoServiço > 0)
                        listaRegistro60R.Add(registro60R_TRI25);


                    break;

                }

                return listaRegistro60R;
            }
            catch (Exception ex)
            {
                return listaRegistro60R;
                MessageBox.Show("Erro Registro 60D Digisat");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Decimal TotalRegistro60D(DateTime dataInicio, DateTime dataFim, Decimal ICMSSelec)
        {
            decimal result = 0;
            //TotalRegistro60RQuantidade = 0;
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", ">=", Util.ConverStringDateSearch(dataInicio.ToString())));
            RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "<=", Util.ConverStringDateSearch(dataFim.ToString())));
            RowRelatorio.Add(new RowsFiltro("ICMS", "System.Decimal", "=", ICMSSelec.ToString()));

            ITEVENDAS_ECFCollection ITEVENDAS_ECFColl = new ITEVENDAS_ECFCollection();
            ITEVENDAS_ECFProvider ITEVENDAS_ECFP = new ITEVENDAS_ECFProvider();
            ITEVENDAS_ECFColl = ITEVENDAS_ECFP.ReadCollectionByParameter(RowRelatorio, "DATA");

            foreach (var item in ITEVENDAS_ECFColl)
            {
                result += Convert.ToDecimal(item.TOTAL);

                // TotalRegistro60RQuantidade += Convert.ToDecimal(item.QTD);
            }

            return result;
        }

        //Verifica se existe produto igual na coleção de emissao de nota fiscal eletronica com a movimentação de entrada
        //no registro 75 não é permitido repetir o produto
        private Boolean VerificaExisProduto75(int idproduto, LIS_PRODUTONFECollection LIS_PRODUTONFE_3Coll)
        {
            Boolean result = false;

            foreach (LIS_PRODUTONFEEntity item_6 in LIS_PRODUTONFE_3Coll)
            {
                if (item_6.IDPRODUTO == idproduto)
                {
                    result = true;
                    break;

                }
            }

            return result;
        }

        //Retorna a ordem do item na entrada de estoque
        private string ItensProdutoEstoqueEntrada(string NDOCUMENTO, DateTime DataMov, int IDPRODUTO)
        {
            int result = 1;

            try
            {
                int contador = 1;
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "=", Util.ConverStringDateSearch(DataMov.ToString("dd/MM/yyyy"))));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl3 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESColl3 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIM,IDMOVPRODUTOES");

                foreach (LIS_MOVPRODUTOESEntity item_2 in LIS_MOVPRODUTOESColl3)
                {
                    if (item_2.IDPRODUTO == IDPRODUTO)
                    {
                        result = contador;
                        break;
                    }
                    contador++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result.ToString().PadLeft(3, '0');
        }  

        //Retorna a ordem do item na Nota fiscal de saida
        private string ItensProdutoEstoqueSaida(string NOTAFISCALE, DateTime DTEMISSAO, int IDPRODUTO)
        {
            int result = 1;

            try
            {
                int contador = 1; ;
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NOTAFISCALE));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "=", Util.ConverStringDateSearch(DTEMISSAO.ToString("dd/MM/yyyy"))));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFEColl_3 = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEColl_3 = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO,IDPRODUTONFE");

                foreach (LIS_PRODUTONFEEntity item_5 in LIS_PRODUTONFEColl_3)
                {
                    if (item_5.IDPRODUTO == IDPRODUTO)
                    {
                        result = contador;
                        break;
                    }
                    contador++;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result.ToString().PadLeft(3, '0');
        }

        private decimal TotalNFEntradaReg50(string CODCFOP, decimal ALQICMS, string NDOCUMENTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODCFOP", "System.String", "=", CODCFOP.ToString()));
                RowRelatorio.Add(new RowsFiltro("ALQICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl3 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESColl3 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);
                
                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl3)
                {
                    result += Convert.ToDecimal(item.VALORTOTAL) + Convert.ToDecimal(item.VLIPI) + Convert.ToDecimal(item.VLFRETE) + Convert.ToDecimal(item.VLICMSST) - Convert.ToDecimal(item.VLDESCONTOPRODUTO);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }

        private decimal BaseCalculoNFEntrada(int IDCFOP, decimal ALQICMS, string NDOCUMENTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));
                RowRelatorio.Add(new RowsFiltro("ALQICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl3 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESColl3 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl3)
                {
                    //result += Convert.ToDecimal(item.BASEICMS);
                    //Base de Calculo  - desconto
                    result += Convert.ToDecimal(item.BASEICMS) - Convert.ToDecimal(item.VLDESCONTOPRODUTO);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }

        private decimal ICMSNFEntrada(int IDCFOP, decimal ALQICMS, string NDOCUMENTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));
                RowRelatorio.Add(new RowsFiltro("ALQICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl3 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESColl3 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl3)
                {
                    result += Convert.ToDecimal(item.VLICMS);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }
     

        private decimal TotalNFSaidaReg50(string CODCFOP, decimal ALQICMS, string NOTAFISCALE)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODCFOP", "System.String", "=", CODCFOP.ToString()));
                RowRelatorio.Add(new RowsFiltro("ALICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NOTAFISCALE));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);
                
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {
                    //result += Convert.ToDecimal(item.VALORTOTAL) + Convert.ToDecimal(item.VALORIPI) - Convert.ToDecimal(item.DESCONTOPRODUTO);
                    //result += (Convert.ToDecimal(item.VALORTOTAL) + Convert.ToDecimal(item.VALORIPI)) - Convert.ToDecimal(item.DESCONTOPRODUTO);
                    result += Convert.ToDecimal(item.VALORTOTAL)  - Convert.ToDecimal(item.DESCONTOPRODUTO);
                  // result += Convert.ToDecimal(item.VALORTOTAL);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }


        private decimal TotalBASECALCULO(int IDCFOP, decimal ALQICMS, string NOTAFISCALE)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                // RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));
                // RowRelatorio.Add(new RowsFiltro("ALICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NOTAFISCALE));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {
                    result += Convert.ToDecimal(item.BASEICMS) - Convert.ToDecimal(item.DESCONTOPRODUTO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }

        private decimal TotalICMSMFE(int IDCFOP, decimal ALQICMS, string NOTAFISCALE)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                // RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));
                // RowRelatorio.Add(new RowsFiltro("ALICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NOTAFISCALE));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {
                    result += Convert.ToDecimal(item.VALORICMS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dtpkInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpkFim_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkInventario_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkregistro54_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
