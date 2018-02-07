using System;
using System.Collections.Generic;
using ACBrFramework.Sped;
using System.IO;
using System.Collections;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSworks.UI;
using System.Windows.Forms;


namespace SpedService.Model
{

    public class SpedFiscalDAL
    {
        public Boolean errolog = false;
        public string pathLog = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\logSped.txt";
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        EMPRESAEntity EMPRESATy = new EMPRESAEntity();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();

        LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();


        LIS_ESTOQUEESCollection LIS_ESTOQUEESColl = new LIS_ESTOQUEESCollection();
        LIS_ESTOQUEESProvider LIS_ESTOQUEESP = new LIS_ESTOQUEESProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();

        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

        UNIDADECollection UNIDADEColl = new UNIDADECollection();
        UNIDADEProvider UNIDADEP = new UNIDADEProvider();

         LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
         LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        List<ProdutoDTO> ListaProduto = new List<ProdutoDTO>();
        List<TributOperacaoFiscalDTO> ListaOperacaoFiscal = new List<TributOperacaoFiscalDTO>();

        //private static string path =  System.Reflection.Assembly.GetExecutingAssembly().Location + @"\\LogSped.txt";
        private static string path =  BmsSoftware.ConfigSistema1.Default.PathInstall + @"\\LogSped.txt";
        public ACBrFramework.Sped.ACBrSpedFiscal ACBrSpedFiscal { get; set; }

       // private EmpresaDTO Empresa;
        private int VersaoLeiaute, FinalidadeArquivo, PerfilApresentacao, IdEmpresa, IdContador, Inventario;
        private String DataInicial, DataFinal, Arquivo, FiltroLocal;
        //private ISession session;
        int perfil = 0;
      

        public SpedFiscalDAL()
        {
            ACBrSpedFiscal = new ACBrFramework.Sped.ACBrSpedFiscal();

            ACBrSpedFiscal.Arquivo = "";
            ACBrSpedFiscal.CurMascara = "#0.00";
            ACBrSpedFiscal.Delimitador = "|";
            ACBrSpedFiscal.DT_FIN = new System.DateTime(1899, 12, 30, 0, 0, 0, 0);
            ACBrSpedFiscal.DT_INI = new System.DateTime(1899, 12, 30, 0, 0, 0, 0);
            ACBrSpedFiscal.LinhasBuffer = 1000;
            ACBrSpedFiscal.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ACBrSpedFiscal.Arquivo = "SpedFiscal.txt";
            ACBrSpedFiscal.TrimString = true;
            ACBrSpedFiscal.OnError += new System.EventHandler<ACBrFramework.Sped.ErrorEventArgs>(ACBrSpedFiscal_OnError);
        }

        private void ACBrSpedFiscal_OnError(object sender, ACBrFramework.Sped.ErrorEventArgs e)
        {
            File.AppendAllText(path, DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToLongTimeString() + ":\t" + e.MsgErro + Environment.NewLine);
        }


        #region BLOCO 0: ABERTURA, IDENTIFICAÇÃO E REFERÊNCIAS
        public void GerarBloco0()
        {
            //EmpresaDTO Empresa = new EmpresaDAL(session).selectId(IdEmpresa);
          //  ViewPessoaContadorDTO Contador = new NHibernateDAL<ViewPessoaContadorDTO>(session).selectId<ViewPessoaContadorDTO>(IdContador);

            //Dados da Empresa
            EMPRESAEntity EMPRESATy = new EMPRESAEntity();
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESATy = EMPRESAP.Read(1);

            //Dados do Contador
            CONTADOREntity CONTADORTy = new CONTADOREntity();
            CONTADORProvider CONTADORP = new CONTADORProvider();
            CONTADORTy = CONTADORP.Read(IdContador);
            
          //  IList<ViewSpedNfeEmitenteDTO> ListaEmitente = new NHibernateDAL<ViewSpedNfeEmitenteDTO>(session).select(new ViewSpedNfeEmitenteDTO());
       //     IList<ViewSpedNfeDestinatarioDTO> ListaDestinatario = new NHibernateDAL<ViewSpedNfeDestinatarioDTO>(session).select(new ViewSpedNfeDestinatarioDTO());
          //  IList<ProdutoDTO> ListaProduto = new NHibernateDAL<ProdutoDTO>(session).select(new ProdutoDTO());
           // IList<ProdutoAlteracaoItemDTO> ListaProdutoAlterado = new NHibernateDAL<ProdutoAlteracaoItemDTO>(session).select(new ProdutoAlteracaoItemDTO());
          //  IList<TributOperacaoFiscalDTO> ListaOperacaoFiscal = new NHibernateDAL<TributOperacaoFiscalDTO>(session).select(new TributOperacaoFiscalDTO());

            var bloco0 = ACBrSpedFiscal.Bloco_0;

            // Registro 0000: ABERTURA DO ARQUIVO DIGITAL E IDENTIFICAÇÃO DA ENTIDADE
            var Registro0000 = bloco0.Registro0000;

            switch (VersaoLeiaute)
            {
                case 0:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao100; break;
                case 1:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao101; break;
                case 2:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao102; break;
                case 3:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao103; break;
                case 4:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao104; break;
                case 5:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao105; break;
                case 6:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao106; break;
                case 7:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao107; break;
                case 8:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao108; break;
                 case 9:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao109; break;
                case 10:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao110; break;
                case 11:
                     Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao111;  break;
                   
            }

            switch (FinalidadeArquivo)
            {
                case 0:
                    Registro0000.COD_FIN = ACBrFramework.Sped.CodFinalidade.Original; break;
                case 1:
                    Registro0000.COD_FIN = ACBrFramework.Sped.CodFinalidade.Substituto; break;
            }

            switch (PerfilApresentacao)
            {
                case 0:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilA; break;
                case 1:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilB; break;
                case 2:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilC; break;
            }

            Registro0000.DT_INI = Convert.ToDateTime(DataInicial);
            Registro0000.DT_FIN = Convert.ToDateTime(DataFinal);
            Registro0000.NOME = EMPRESATy.NOMECLIENTE;
            Registro0000.CNPJ =Util.RetiraLetras(EMPRESATy.CNPJCPF);
          //  Registro0000.CPF = string.Empty;
            Registro0000.UF = EMPRESATy.UF;
            Registro0000.IE =Util.RetiraLetras(EMPRESATy.IE);
            Registro0000.COD_MUN =BuscaCodIBGE(EMPRESATy.CIDADE, EMPRESATy.UF);

            if (Registro0000.COD_MUN < 0)
                GeraArquivoLog("Código do município não localizado para a empresa: " + EMPRESATy.NOMECLIENTE);

           // Registro0000.IM = string.Empty;
           // Registro0000.SUFRAMA = string.Empty; 
            Registro0000.IND_ATIV = ACBrFramework.Sped.Atividade.Outros;

            // Registro 0001: ABERTURA DO BLOCO 0
            var Registro0001 = bloco0.Registro0001;
            Registro0001.IND_MOV = IndicadorMovimento.ComDados;

            // Registro 0005: DADOS COMPLEMENTARES DA ENTIDADE
            var Registro0005 = Registro0001.Registro0005;
            Registro0005.FANTASIA = EMPRESATy.NOMEFANTASIA;
            Registro0005.CEP = Util.RetiraLetras(EMPRESATy.CEP);
            Registro0005.ENDERECO = EMPRESATy.ENDERECO;
            Registro0005.NUM = EMPRESATy.NUMERO;
            Registro0005.COMPL = EMPRESATy.COMPLEMENTO;
            Registro0005.BAIRRO = EMPRESATy.BAIRRO;
            Registro0005.FONE = Util.RetiraLetras(EMPRESATy.TELEFONE);
            Registro0005.FAX = Util.RetiraLetras(EMPRESATy.FAX);
            Registro0005.EMAIL = EMPRESATy.EMAIL;

            // REGISTRO 0015: DADOS DO CONTRIBUINTE SUBSTITUTO
            //{ Implementado a critério do Participante do T2Ti ERP }

            // REGISTRO 0100: DADOS DO CONTABILISTA
            var Registro0100 = Registro0001.Registro0100;
            Registro0100.NOME = CONTADORTy.NOME;
            
            if (CONTADORTy.CPF != "   .   .   -")
                Registro0100.CPF = Util.RetiraLetras(CONTADORTy.CPF);
            else
                Registro0100.CPF = string.Empty;

            Registro0100.CRC = CONTADORTy.CRC;

            if (CONTADORTy.CNPJ != "  .   .   /    -")
                Registro0100.CNPJ = Util.RetiraLetras(CONTADORTy.CNPJ);
            else
                Registro0100.CNPJ = string.Empty;

            Registro0100.CEP = Util.RetiraLetras(CONTADORTy.CEP);
            Registro0100.ENDERECO = CONTADORTy.ENDERECO;
            Registro0100.NUM = CONTADORTy.NUMERO;
            
            if (CONTADORTy.COMPLEMENTO.Length > 0)
                Registro0100.COMPL = CONTADORTy.COMPLEMENTO;

            if (CONTADORTy.BAIRRO.Length > 0)
                Registro0100.BAIRRO = CONTADORTy.BAIRRO;

            string FoneContador = Util.RetiraLetras(CONTADORTy.FONE);
            if (FoneContador.Length > 0)
                Registro0100.FONE = FoneContador;
           
            string FaxContador = Util.RetiraLetras(CONTADORTy.FAX);
            if (FaxContador.Length > 0)
                Registro0100.FAX = FaxContador;


            if (CONTADORTy.EMAIL.Length > 0)
                Registro0100.EMAIL = CONTADORTy.EMAIL;

            Registro0100.COD_MUN = Convert.ToInt32(CONTADORTy.COD_MUN);


            // REGISTRO 0150: TABELA DE CADASTRO DO PARTICIPANTE
            //Clientes            
                FiltraNotaSaidaCliente_REGISTRO_0150(DataInicial, DataFinal);
                foreach (LIS_NOTAFISCALEEntity Emitente in LIS_NOTAFISCALEColl)
                {
                        var Registro0150 = new Registro0150();

                        //Dados do Cliente
                        CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                        CLIENTETy = CLIENTEP.Read(Convert.ToInt32(Emitente.IDCLIENTE));

                        Registro0150.COD_PART = "CLI" + Emitente.IDCLIENTE.ToString().PadLeft(10, '0');
                        Registro0150.NOME = Emitente.NOMECLIENTE.Trim();
                        Registro0150.COD_PAIS = "01058";
                        Registro0150.CNPJ = Util.RetiraLetras(Emitente.CNPJ);
                        Registro0150.CPF = Util.RetiraLetras(Emitente.CPF);
                        Registro0150.IE = Util.RetiraLetras(CLIENTETy.IE);
                        Registro0150.COD_MUN = Convert.ToInt32(CLIENTETy.COD_MUN_IBGE);
                        //Registro0150.SUFRAMA = //Emitente.Suframa;
                        Registro0150.ENDERECO = CLIENTETy.ENDERECO1.Trim();
                        Registro0150.NUM = CLIENTETy.NUMEROENDER.Trim();
                        Registro0150.COMPL = CLIENTETy.COMPLEMENTO1.Trim();
                        Registro0150.BAIRRO = CLIENTETy.BAIRRO1.Trim();

                        Registro0001.Registro0150.Add(Registro0150);
                }
            

           
                // REGISTRO 0150: TABELA DE CADASTRO DO PARTICIPANTE
                //Fornecedor
                FiltraNotaEntradaFornecedoresR0150(DataInicial, DataFinal);
                foreach (LIS_ESTOQUEESEntity ListaDestinatario in LIS_ESTOQUEESColl)
                {
                    var Registro0150 = new Registro0150();

                    //Dados do Fornecedor
                    FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                    FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                    FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(ListaDestinatario.IDFORNECEDOR));

                    Registro0150.COD_PART = "FOR" + ListaDestinatario.IDFORNECEDOR.ToString().PadLeft(10, '0');
                    Registro0150.NOME = FORNECEDORTy.NOME.Trim();
                    Registro0150.COD_PAIS = "01058";
                    Registro0150.CNPJ = Util.RetiraLetras(FORNECEDORTy.CNPJ);
                    Registro0150.CPF = string.Empty;
                    Registro0150.IE = Util.RetiraLetras(FORNECEDORTy.IE);
                    Registro0150.COD_MUN = BuscaCodIBGE(FORNECEDORTy.CIDADE, FORNECEDORTy.UF);

                    if (Registro0150.COD_MUN < 1)
                        GeraArquivoLog("Código do município não localizado para o fornecedor: " + FORNECEDORTy.NOME);

                    //Registro0150.SUFRAMA = string.Empty;
                    Registro0150.ENDERECO = FORNECEDORTy.ENDERECO.Trim();
                    Registro0150.NUM = FORNECEDORTy.NUMERO.Trim();
                    Registro0150.COMPL = string.Empty;
                    Registro0150.BAIRRO = FORNECEDORTy.BAIRRO.Trim();

                    Registro0001.Registro0150.Add(Registro0150);
                }
           


            // REGISTRO 0175: ALTERAÇÃO DA TABELA DE CADASTRO DE PARTICIPANTE
            //{ Implementado a critério do Participante do T2Ti ERP }
    
               // Remove IDUnidade Repetido
                //BuscaUnidade();
                //foreach (UNIDADEEntity item in UNIDADEColl)
                //{
                //    var Registro0190 = new Registro0190();
                //   // Registro0190.UNID = Convert.ToString(item.IDUNIDADE);
                //    Registro0190.UNID = Util.LimiterText(item.NOME,3);
                //    Registro0190.DESCR = item.NOME+"_"; // usando "_" para o NOME nao ficar igual ao UNID
                //    Registro0001.Registro0190.Add(Registro0190);
                //}

             

            //REGISTRO 0200: TABELA DE IDENTIFICAÇÃO DO ITEM (PRODUTO E SERVIÇOS)
           // ArrayList ListaSiglaUnidade = new ArrayList();
           // List<UnidadeProdutoDTO> ListaUnidade = new List<UnidadeProdutoDTO>();
            
           
                BuscaProdutoEntrada(DataInicial, DataFinal);
                
                if(PerfilApresentacao == 0) //Perfil A
                    BuscaProdutoSaidaNFe(DataInicial, DataFinal);

            if (ListaProduto != null)
            {
                //Remove IDProduto Repetido
                List<ProdutoDTO> ListaProduto2 = new List<ProdutoDTO>();
                foreach (ProdutoDTO item in ListaProduto)
                {

                    if (ListaProduto2.Find(delegate(ProdutoDTO item2) { return (item2.Id == item.Id); }) == null)
                    {
                        ListaProduto2.Add(item);
                    }
                }

                ListaProduto = ListaProduto2;

                //Remove unidade Repetido
                List<ProdutoDTO> ListaProduto_Und = new List<ProdutoDTO>();
                foreach (ProdutoDTO item in ListaProduto)
                {

                    if (ListaProduto_Und.Find(delegate(ProdutoDTO item2) { return (item2.UnidadeProduto == item.UnidadeProduto); }) == null)
                    {
                        ListaProduto_Und.Add(item);
                    }
                }

                // REGISTRO 0190: IDENTIFICAÇÃO DAS UNIDADES DE MEDIDA
                foreach (ProdutoDTO Produto in ListaProduto_Und)
                {
                    var Registro0190 = new Registro0190();
                    Registro0190.UNID = Util.LimiterText(Produto.UnidadeProduto, 3);
                    Registro0190.DESCR = Produto.UnidadeProduto +"_"; // Usando "_" para o nome nao se igual ao UNID
                    Registro0001.Registro0190.Add(Registro0190);
                    
                }

                    foreach (ProdutoDTO Produto in ListaProduto)
                    {
                        var Registro0200 = new Registro0200();
                    
                        Registro0200.COD_ITEM = Convert.ToString(Produto.Id).ToString().PadLeft(10, '0');
                        Registro0200.DESCR_ITEM = Produto.Nome;
                        Registro0200.COD_BARRA = Produto.Gtin;
                        Registro0200.COD_ANT_ITEM = "";
                        Registro0200.UNID_INV = Convert.ToString(Produto.UnidadeProduto);
                    

                        switch (Convert.ToInt32(Produto.TipoItemSped))
                        {
                            case 0:
                                Registro0200.TIPO_ITEM = TipoItem.MercadoriaRevenda; break;
                            case 1:
                                Registro0200.TIPO_ITEM = TipoItem.MateriaPrima; break;
                            case 2:
                                Registro0200.TIPO_ITEM = TipoItem.Embalagem; break;
                            case 3:
                                Registro0200.TIPO_ITEM = TipoItem.ProdutoProcesso; break;
                            case 4:
                                Registro0200.TIPO_ITEM = TipoItem.ProdutoAcabado; break;
                            case 5:
                                Registro0200.TIPO_ITEM = TipoItem.Subproduto; break;
                            case 6:
                                Registro0200.TIPO_ITEM = TipoItem.ProdutoIntermediario; break;
                            case 7:
                                Registro0200.TIPO_ITEM = TipoItem.MaterialConsumo; break;
                            case 8:
                                Registro0200.TIPO_ITEM = TipoItem.AtivoImobilizado; break;
                            case 9:
                                Registro0200.TIPO_ITEM = TipoItem.Servicos; break;
                            case 10:
                                Registro0200.TIPO_ITEM = TipoItem.OutrosInsumos; break;
                            case 99:
                                Registro0200.TIPO_ITEM = TipoItem.Outras; break;
                        }

                        Registro0200.COD_NCM = Produto.Ncm;
                      
                        Registro0200.EX_IPI = "";

                        if (Produto.Ncm.Length > 3)
                            Registro0200.COD_GEN = Produto.Ncm.Substring(2, 1);
                        else
                        {
                            GeraArquivoLog("O código NCM não cadastrado para o produto " + Produto.Nome);
                        }

                        Registro0200.COD_LST = "";
                        Registro0200.ALIQ_ICMS = Produto.AliquotaIcmsPaf;              


                    Registro0001.Registro0200.Add(Registro0200);

                        //var Registro0220 = new Registro0220();
                        //Registro0220.UNID_CONV = Util.LimiterText(Produto.UnidadeProduto, 6);
                        //Registro0220.FAT_CONV = 0;
                        //Registro0200.Registro0220.Add(Registro0220);
                

                        
                    }
                
            }


           
             
            

            // REGISTRO 0205: ALTERAÇÃO DO ITEM
            //foreach (ProdutoAlteracaoItemDTO ProdutoAlterado in ListaProdutoAlterado)
            //{
            //    var Registro0205 = new Registro0205();
            //    Registro0205.DESCR_ANT_ITEM = ProdutoAlterado.Nome;
            //    Registro0205.DT_INI = ProdutoAlterado.DataInicial;
            //    Registro0205.DT_FIN = ProdutoAlterado.DataFinal;
            //    Registro0205.COD_ANT_ITEM = ProdutoAlterado.Codigo;
            //}

            // REGISTRO 0206: CÓDIGO DE PRODUTO CONFORME TABELA PUBLICADA PELA ANP (COMBUSTÍVEIS)
            //{ Implementado a critério do Participante do T2Ti ERP }

            // REGISTRO 0300: CADASTRO DE BENS OU COMPONENTES DO ATIVO IMOBILIZADO
            // REGISTRO 0305: INFORMAÇÃO SOBRE A UTILIZAÇÃO DO BEM
            //{ Implementado a critério do Participante do T2Ti ERP - versão 1.0 não possui controle CIAP }

            // REGISTRO 0400: TABELA DE NATUREZA DA OPERAÇÃO/PRESTAÇÃO
             //Remove ID Repetido
             if (ListaOperacaoFiscal != null)
             {
                 //Remove IDProduto Repetido
                 List<TributOperacaoFiscalDTO> ListaOperacaoFiscal2 = new List<TributOperacaoFiscalDTO>();
                 foreach (TributOperacaoFiscalDTO OperacaoFiscal in ListaOperacaoFiscal)
                 {

                     if (ListaOperacaoFiscal2.Find(delegate(TributOperacaoFiscalDTO item2) { return (item2.Id == OperacaoFiscal.Id); }) == null)
                     {
                         ListaOperacaoFiscal2.Add(OperacaoFiscal);
                     }
                 }

                 ListaOperacaoFiscal = ListaOperacaoFiscal2;

                 //foreach (TributOperacaoFiscalDTO OperacaoFiscal in ListaOperacaoFiscal)
                 //{
                 //    var Registro0400 = new Registro0400();
                 //    Registro0400.COD_NAT = Convert.ToString(OperacaoFiscal.Id);
                 //    Registro0400.DESCR_NAT = OperacaoFiscal.Descricao;
                 //    Registro0001.Registro0400.Add(Registro0400);
               //  }

               
             }

            // REGISTRO 0450: TABELA DE INFORMAÇÃO COMPLEMENTAR DO DOCUMENTO FISCAL
            //{ Implementado a critério do Participante do T2Ti ERP }

            // REGISTRO 0460: TABELA DE OBSERVAÇÕES DO LANÇAMENTO FISCAL
            //{ Implementado a critério do Participante do T2Ti ERP }

            // REGISTRO 0500: PLANO DE CONTAS CONTÁBEIS
            //{ Implementado a critério do Participante do T2Ti ERP }

            // REGISTRO 0600: CENTRO DE CUSTOS
            //{ Implementado a critério do Participante do T2Ti ERP }
        }

      
        #endregion

        decimal Soma_RegistroC190_VL_BC_ICMS = 0;
        #region BLOCO C: DOCUMENTOS FISCAIS I - MERCADORIAS (ICMS/IPI)
        public void GerarBlocoC()
        {
            int i, j;
            i = 0;
            j = 0;

            // IList<EcfNotaFiscalCabecalhoDTO> ListaNF2Cabecalho = new NHibernateDAL<EcfNotaFiscalCabecalhoDTO>(session).select(new EcfNotaFiscalCabecalhoDTO());
            //  IList<EcfNotaFiscalCabecalhoDTO> ListaNF2CabecalhoCanceladas = new NHibernateDAL<EcfNotaFiscalCabecalhoDTO>(session).select(new EcfNotaFiscalCabecalhoDTO());
            //  IList<NfeCabecalhoDTO> ListaNFeCabecalho = new NHibernateDAL<NfeCabecalhoDTO>(session).select(new NfeCabecalhoDTO());

            var BlocoC = ACBrSpedFiscal.Bloco_C;

            // REGISTRO C001: ABERTURA DO BLOCO C
            var RegistroC001 = BlocoC.RegistroC001;
            RegistroC001.IND_MOV = IndicadorMovimento.ComDados;


            // / ///////////
            //  Perfil A  //
            // / ///////////
            // if (PerfilApresentacao == 0)
            // {
            //if (ListaNFeCabecalho != null)
            // {
            //Filtra Produtos Nota Fiscal

            foreach (LIS_NOTAFISCALEEntity NFeCabecalho in LIS_NOTAFISCALEColl)
            {
                var RegistroC100 = new RegistroC100();
                i++;

                // REGISTRO C100: NOTA FISCAL (CÓDIGO 01), NOTA FISCAL AVULSA (CÓDIGO 1B), NOTA FISCAL DE PRODUTOR (CÓDIGO 04), NF-e (CÓDIGO 55) e NFC-e (CÓDIGO 65)
               
                    RegistroC100.IND_OPER = TipoOperacao.SaidaPrestacao;
                RegistroC100.IND_EMIT = Emitente.EmissaoPropria;

                //  if (NFeCabecalho.Cliente != null)
                RegistroC100.COD_PART = "CLI" + NFeCabecalho.IDCLIENTE.ToString().PadLeft(10, '0'); ;
                // else
                //   RegistroC100.COD_PART = "F" + NFeCabecalho.Fornecedor.Id;

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                RegistroC100.COD_MOD = CONFISISTEMATy.MODELONFE.Trim();

                if (NFeCabecalho.FLAGENVIADA.TrimEnd() == "S" && NFeCabecalho.FLAGCANCELADA.TrimEnd() == "N")
                    RegistroC100.COD_SIT = SituacaoDocto.Regular;
                else
                    RegistroC100.COD_SIT = SituacaoDocto.Cancelado;

                RegistroC100.SER = CONFISISTEMATy.SERIENFE;
                RegistroC100.NUM_DOC = NFeCabecalho.NFISCALE;
                RegistroC100.CHV_NFE = Util.RetiraLetras(NFeCabecalho.CHAVEACESSO);
                RegistroC100.DT_DOC = Convert.ToDateTime(NFeCabecalho.DTEMISSAO);
                RegistroC100.DT_E_S = Convert.ToDateTime(NFeCabecalho.DTSAIDA);
                RegistroC100.VL_DOC = Convert.ToDecimal(NFeCabecalho.TOTALNOTA);

                if (NFeCabecalho.FLAGTIPOPAGAMENTO == "0")
                    RegistroC100.IND_PGTO = TipoPagamento.Vista;     				//<indPag> //0:Pagamento a vista - 1:Pagamento Prazo -2:Outrosx'
                else if (NFeCabecalho.FLAGTIPOPAGAMENTO == "1")
                    RegistroC100.IND_PGTO = TipoPagamento.Prazo;
                else if (NFeCabecalho.FLAGTIPOPAGAMENTO == "2")
                    RegistroC100.IND_PGTO = TipoPagamento.Nenhum;

                RegistroC100.VL_DESC = Convert.ToDecimal(NFeCabecalho.VALORDESCONTO);
                RegistroC100.VL_ABAT_NT = 0;
                RegistroC100.VL_MERC = Convert.ToDecimal(NFeCabecalho.TOTALPRODUTOS);

                //NfeTransporteDTO Transporte = new NHibernateDAL<NfeTransporteDTO>(session).selectId<NfeTransporteDTO>(1);

                if (NFeCabecalho.FRETE == 1)
                    RegistroC100.IND_FRT = TipoFrete.PorContaEmitente; //Transporte.ModalidadeFrete;
                else
                    RegistroC100.IND_FRT = TipoFrete.SemCobrancaFrete;

                RegistroC100.VL_FRT = Convert.ToDecimal(NFeCabecalho.VALORFRETE);
                RegistroC100.VL_SEG = Convert.ToDecimal(NFeCabecalho.VALORSEGURO);
                RegistroC100.VL_OUT_DA = Convert.ToDecimal(NFeCabecalho.OUTRADESPES);
                RegistroC100.VL_BC_ICMS = Convert.ToDecimal(NFeCabecalho.BASECALCICMS);
                RegistroC100.VL_ICMS = Convert.ToDecimal(NFeCabecalho.VALORICMS);
                RegistroC100.VL_BC_ICMS_ST = 0;// NFeCabecalho.BaseCalculoIcmsSt;
                RegistroC100.VL_ICMS_ST = 0;// NFeCabecalho.ValorIcmsSt;
                RegistroC100.VL_IPI = Convert.ToDecimal(NFeCabecalho.TOTALIPI);
                RegistroC100.VL_PIS = 0;// NFeCabecalho.ValorPis;
                RegistroC100.VL_COFINS = 0;// NFeCabecalho.ValorCofins;
                RegistroC100.VL_PIS_ST = 0;
                RegistroC100.VL_COFINS_ST = 0;


                //Somente CFOP de saida
                CFOPProvider CFOPP = new CFOPProvider();
                string CODCFOP_ = CFOPP.Read(Convert.ToInt32(NFeCabecalho.IDCFOP)).CODCFOP;
                int CFOP_ = Convert.ToInt32(CODCFOP_.Substring(0, 1));
                if (CFOP_ > 3)
                {
                    RegistroC001.RegistroC100.Add(RegistroC100);
                }


                // REGISTRO C170: ITENS DO DOCUMENTO (CÓDIGO 01, 1B, 04 e 55).
                // IList<ViewSpedNfeDetalheDTO> ListaNFeDetalhe = new NHibernateDAL<ViewSpedNfeDetalheDTO>(session).select(new ViewSpedNfeDetalheDTO());
                //Nota de Saida
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", NFeCabecalho.IDNOTAFISCALE.ToString()));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTONFE, NOTAFISCALE");

                int itemCount = 1;
                foreach (LIS_PRODUTONFEEntity NFeDetalhe in LIS_PRODUTONFEColl)
                {
                    if (PerfilApresentacao == 0) //Perfil a
                    {
                        var RegistroC170 = new RegistroC170();
                        RegistroC170.NUM_ITEM = Convert.ToString(itemCount);
                        RegistroC170.COD_ITEM = Convert.ToString(NFeDetalhe.IDPRODUTO).PadLeft(10, '0');
                        RegistroC170.DESCR_COMPL = NFeDetalhe.NOMEPRODUTO.Trim();
                        RegistroC170.QTD = Convert.ToDecimal(NFeDetalhe.QUANTIDADE);
                        RegistroC170.UNID = NFeDetalhe.NOMEUNIDADE;
                        RegistroC170.VL_ITEM = Convert.ToDecimal(NFeDetalhe.VALORTOTAL);
                        RegistroC170.VL_DESC = Convert.ToDecimal(NFeDetalhe.DESCONTOPRODUTO);
                        RegistroC170.IND_MOV = MovimentacaoFisica.Sim;
                        RegistroC170.CST_ICMS = NFeDetalhe.CODCOMPL.Trim();

                        //Dado CFOP
                        // CFOPEntity CFOPTy = new CFOPEntity();
                        // CFOPProvider CFOPP = new CFOPProvider();
                        // CFOPTy = CFOPP.Read(Convert.ToInt32(NFeDetalhe.IDCFOP));
                        RegistroC170.CFOP = NFeDetalhe.CODCFOP;
                        // if (CFOPTy != null)
                        //    RegistroC170.COD_NAT = Util.RetiraLetras(CFOPTy.CODCFOP);

                        RegistroC170.COD_NAT = string.Empty;
                        RegistroC170.VL_BC_ICMS = Convert.ToDecimal(NFeDetalhe.BASEICMS);
                        RegistroC170.ALIQ_ICMS = Convert.ToDecimal(NFeDetalhe.ALICMS);
                        RegistroC170.VL_ICMS = Convert.ToDecimal(NFeDetalhe.VALORICMS);
                        RegistroC170.VL_BC_ICMS_ST = Convert.ToDecimal(NFeDetalhe.VLBASEST);
                        RegistroC170.ALIQ_ST = 0;
                        RegistroC170.VL_ICMS_ST = Convert.ToDecimal(NFeDetalhe.VLICMSST);
                        RegistroC170.IND_APUR = ApuracaoIPI.Mensal;

                        //Dados do produto
                        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                        PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(NFeDetalhe.IDPRODUTO));

                        if (PRODUTOSTy != null)
                            RegistroC170.CST_IPI = PRODUTOSTy.CSTIPI;

                        RegistroC170.COD_ENQ = "";
                        RegistroC170.VL_BC_IPI = Convert.ToDecimal(NFeDetalhe.VALORTOTAL);
                        RegistroC170.ALIQ_IPI = Convert.ToDecimal(NFeDetalhe.ALIPI);
                        RegistroC170.VL_IPI = Convert.ToDecimal(NFeDetalhe.VALORIPI);

                        if (PRODUTOSTy != null)
                            RegistroC170.CST_PIS = PRODUTOSTy.CSTPIS;

                        RegistroC170.VL_BC_PIS = 0;
                        RegistroC170.ALIQ_PIS_PERC = 0;
                        RegistroC170.QUANT_BC_PIS = 0;
                        RegistroC170.ALIQ_PIS_R = 0;
                        RegistroC170.VL_PIS = 0;
                        RegistroC170.CST_COFINS = "";
                        RegistroC170.VL_BC_COFINS = 0;
                        RegistroC170.ALIQ_COFINS_PERC = 0;
                        RegistroC170.QUANT_BC_COFINS = 0;
                        RegistroC170.ALIQ_COFINS_R = 0;
                        RegistroC170.VL_COFINS = 0;
                        RegistroC170.COD_CTA = "";
                        RegistroC100.RegistroC170.Add(RegistroC170);

                        itemCount++;
                    }
                }

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
                RowRelatorio.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", NFeCabecalho.IDNOTAFISCALE.ToString()));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTONFE, NOTAFISCALE");

                if (LIS_PRODUTONFEColl.Count > 0)
                {
                    var RegistroC190 = new RegistroC190();

                    //Somente CFOP de saida
                     CFOP_ = Convert.ToInt32(LIS_PRODUTONFEColl[0].CODCFOP.Substring(0, 1));
                    if (CFOP_ > 3)
                    {
                        RegistroC190.CST_ICMS = LIS_PRODUTONFEColl[0].CODCOMPL.Trim();
                        RegistroC190.CFOP = LIS_PRODUTONFEColl[0].CODCFOP.Trim();
                        RegistroC190.ALIQ_ICMS = Convert.ToDecimal(LIS_PRODUTONFEColl[0].ALICMS);
                        RegistroC190.VL_OPR = SomaC190("VL_OPR", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_BC_ICMS = SomaC190("VL_BC_ICMS", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_ICMS = SomaC190("VL_ICMS", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_BC_ICMS_ST = SomaC190("VL_BC_ICMS_ST", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_ICMS_ST = SomaC190("VL_ICMS_ST", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_RED_BC = SomaC190("VL_RED_BC", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.VL_IPI = SomaC190("VL_IPI", LIS_PRODUTONFEColl[0].NOTAFISCALE);
                        RegistroC190.COD_OBS = "";
                        RegistroC100.RegistroC190.Add(RegistroC190);
                    }
                    
                }


            }

            //foreach (LIS_PRODUTONFEEntity ListaNFeAnalitico in LIS_PRODUTONFEColl)
            //{
            //    var RegistroC190 = new RegistroC190();
            //    RegistroC190.CST_ICMS = ListaNFeAnalitico.CODCOMPL;
            //    RegistroC190.CFOP = ListaNFeAnalitico.CODCFOP;
            //    RegistroC190.ALIQ_ICMS = Convert.ToDecimal(ListaNFeAnalitico.ALICMS);itemCount2
            //    RegistroC190.VL_OPR = SomaC190("VL_OPR");
            //    RegistroC190.VL_BC_ICMS = SomaC190("VL_BC_ICMS");
            //    RegistroC190.VL_ICMS = SomaC190("VL_ICMS");
            //    RegistroC190.VL_BC_ICMS_ST = SomaC190("VL_BC_ICMS_ST");
            //    RegistroC190.VL_ICMS_ST = SomaC190("VL_ICMS_ST");
            //    RegistroC190.VL_RED_BC = SomaC190("VL_RED_BC");
            //    RegistroC190.VL_IPI = SomaC190("VL_IPI");
            //    RegistroC190.COD_OBS = "";
            //    RegistroC100.RegistroC190.Add(RegistroC190);
            //}

            //Nota de Entrada
            //Movimentação de Entrada               
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
            RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
            RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

            if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
            {
                RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
            }
            
            LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio);
            
            int ContadorEstoque = 0;
            foreach (LIS_ESTOQUEESEntity LIS_ESTOQUEESTy in LIS_ESTOQUEESColl)
            {

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", LIS_ESTOQUEESTy.NDOCUMENTO));
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);


                // REGISTRO C100: NOTA FISCAL (CÓDIGO 01), NOTA FISCAL AVULSA (CÓDIGO 1B), NOTA FISCAL DE PRODUTOR (CÓDIGO 04), NF-e (CÓDIGO 55) e NFC-e (CÓDIGO 65)
                var RegistroC100 = new RegistroC100();
                RegistroC100.IND_OPER = TipoOperacao.EntradaAquisicao;
                RegistroC100.IND_EMIT = Emitente.Terceiros;

                RegistroC100.COD_PART = "FOR" + LIS_ESTOQUEESTy.IDFORNECEDOR.ToString().PadLeft(10, '0'); ;

                RegistroC100.COD_MOD = LIS_ESTOQUEESTy.MODELONF;
                RegistroC100.COD_SIT = SituacaoDocto.Regular;

                RegistroC100.SER = LIS_ESTOQUEESTy.SERIENF;
                RegistroC100.NUM_DOC = LIS_ESTOQUEESTy.NDOCUMENTO;
                RegistroC100.CHV_NFE = Util.RetiraLetras(LIS_ESTOQUEESTy.CHAVEACESSO);
                RegistroC100.DT_DOC = Convert.ToDateTime(LIS_ESTOQUEESTy.DATAMOVIM);
                RegistroC100.DT_E_S = Convert.ToDateTime(LIS_ESTOQUEESTy.DATAMOVIM);
                // RegistroC100.VL_DOC = Convert.ToDecimal(LIS_ESTOQUEESTy.TOTALMOVIMENTACAO);
                RegistroC100.VL_DOC = SomaC100Entrada("VL_MERC", LIS_ESTOQUEESTy.NDOCUMENTO);

                RegistroC100.IND_PGTO = TipoPagamento.Vista;     				//<indPag> //0:Pagamento a vista - 1:Pagamento Prazo -2:Outrosx'
                //RegistroC100.VL_DESC = Convert.ToDecimal(LIS_ESTOQUEESColl[0].VALORDESCONTO);
                RegistroC100.VL_ABAT_NT = 0;
                //  RegistroC100.VL_MERC = Convert.ToDecimal(LIS_ESTOQUEESTy.TOTALMOVIMENTACAO);
                RegistroC100.VL_MERC = SomaC100Entrada("VL_MERC", LIS_ESTOQUEESTy.NDOCUMENTO);
                

                RegistroC100.IND_FRT = TipoFrete.PorContaEmitente; //Transporte.ModalidadeFrete;

                RegistroC100.VL_FRT = Convert.ToDecimal(LIS_ESTOQUEESTy.VALORFRETE);
                RegistroC100.VL_SEG = 0;
                RegistroC100.VL_OUT_DA = 0;
                RegistroC100.VL_BC_ICMS = Convert.ToDecimal(LIS_ESTOQUEESTy.VALORBASEICMS);
                RegistroC100.VL_ICMS = Convert.ToDecimal(LIS_ESTOQUEESTy.VALORICMS);
                RegistroC100.VL_BC_ICMS_ST = 0;// NFeCabecalho.BaseCalculoIcmsSt;
                RegistroC100.VL_ICMS_ST = 0;// NFeCabecalho.ValorIcmsSt;
                RegistroC100.VL_IPI = Convert.ToDecimal(LIS_ESTOQUEESTy.VALORIPI);
                RegistroC100.VL_PIS = 0;// NFeCabecalho.ValorPis;
                RegistroC100.VL_COFINS = 0;// NFeCabecalho.ValorCofins;
                RegistroC100.VL_PIS_ST = 0;
                RegistroC100.VL_COFINS_ST = 0;
                RegistroC001.RegistroC100.Add(RegistroC100);

                int itemCountFornec = 1;
                foreach (LIS_MOVPRODUTOESEntity NFeDetalhe_Entrada in LIS_MOVPRODUTOESColl)
                {

                    var RegistroC170 = new RegistroC170();
                    RegistroC170.NUM_ITEM = Convert.ToString(itemCountFornec);
                    RegistroC170.COD_ITEM = Convert.ToString(NFeDetalhe_Entrada.IDPRODUTO).PadLeft(10, '0');
                    RegistroC170.DESCR_COMPL = NFeDetalhe_Entrada.NOMEPRODUTO;
                    RegistroC170.QTD = Convert.ToDecimal(NFeDetalhe_Entrada.QUANTIDADE);

                    //Dados Do produto
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(NFeDetalhe_Entrada.IDPRODUTO));

                    //Dados da Unidade
                    UNIDADEEntity UNIDADETy = new UNIDADEEntity();
                    UNIDADEProvider UNIDADEP = new UNIDADEProvider();
                    UNIDADETy = UNIDADEP.Read(Convert.ToInt32(PRODUTOSTy.IDUNIDADE));

                    if (UNIDADETy != null)
                        RegistroC170.UNID = UNIDADETy.NOME.Trim();

                    RegistroC170.VL_ITEM = Convert.ToDecimal(NFeDetalhe_Entrada.VALORTOTAL);
                    RegistroC170.VL_DESC = 0;
                    RegistroC170.IND_MOV = MovimentacaoFisica.Sim;
                    RegistroC170.CST_ICMS = NFeDetalhe_Entrada.CST_CSOSN;
                    

                    //Dado CFOP
                    CFOPEntity CFOPTy = new CFOPEntity();
                    CFOPProvider CFOPP = new CFOPProvider();
                    CFOPTy = CFOPP.Read(Convert.ToInt32(NFeDetalhe_Entrada.IDCFOP));
                    if (CFOPTy != null)
                    {
                        RegistroC170.CFOP = Util.RetiraLetras(CFOPTy.CODCFOP);
                        //RegistroC170.COD_NAT = Util.RetiraLetras(CFOPTy.CODCFOP);
                    }

                    RegistroC170.VL_BC_ICMS = Convert.ToDecimal(NFeDetalhe_Entrada.BASEICMS);
                    RegistroC170.ALIQ_ICMS = Convert.ToDecimal(NFeDetalhe_Entrada.ALQICMS);
                    RegistroC170.VL_ICMS = Convert.ToDecimal(NFeDetalhe_Entrada.VLICMS);
                    RegistroC170.VL_BC_ICMS_ST = Convert.ToDecimal(NFeDetalhe_Entrada.VLICMSST);
                    RegistroC170.ALIQ_ST = 0;
                    RegistroC170.VL_ICMS_ST = Convert.ToDecimal(NFeDetalhe_Entrada.VLICMSST);
                    RegistroC170.IND_APUR = ApuracaoIPI.Mensal;

                    //Dados do produto
                   // if (PRODUTOSTy != null)
                      //  RegistroC170.CST_IPI = PRODUTOSTy.CSTIPI;
                   // else
                     //   RegistroC170.CST_IPI = string.Empty;

                    RegistroC170.COD_ENQ = "";
                    RegistroC170.VL_BC_IPI = Convert.ToDecimal(NFeDetalhe_Entrada.VALORTOTAL);
                    RegistroC170.ALIQ_IPI = 0;
                    RegistroC170.VL_IPI = Convert.ToDecimal(NFeDetalhe_Entrada.VLIPI);

                    //if (PRODUTOSTy != null)
                    //    RegistroC170.CST_PIS = PRODUTOSTy.CSTPIS;
                    //else
                    //    RegistroC170.CST_PIS = string.Empty;

                    RegistroC170.VL_BC_PIS = 0;
                    RegistroC170.ALIQ_PIS_PERC = 0;
                    RegistroC170.QUANT_BC_PIS = 0;
                    RegistroC170.ALIQ_PIS_R = 0;
                    RegistroC170.VL_PIS = 0;
                    RegistroC170.CST_COFINS = "";
                    RegistroC170.VL_BC_COFINS = 0;
                    RegistroC170.ALIQ_COFINS_PERC = 0;
                    RegistroC170.QUANT_BC_COFINS = 0;
                    RegistroC170.ALIQ_COFINS_R = 0;
                    RegistroC170.VL_COFINS = 0;
                    RegistroC170.COD_CTA = "";
                    RegistroC100.RegistroC170.Add(RegistroC170);                  

                    itemCountFornec++;
                }

                var RegistroC190 = new RegistroC190();

                 RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", LIS_ESTOQUEESColl[ContadorEstoque].NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                if(LIS_MOVPRODUTOESColl.Count > 0)
                {
                    RegistroC190.CST_ICMS = LIS_MOVPRODUTOESColl[0].CST_CSOSN;
                    RegistroC190.CFOP = LIS_MOVPRODUTOESColl[0].CODCFOP;
                    RegistroC190.ALIQ_ICMS = Convert.ToDecimal(LIS_MOVPRODUTOESColl[0].ALQICMS);
                    RegistroC190.VL_OPR = SomaC190Entrada("VL_OPR", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_BC_ICMS = SomaC190Entrada("VL_BC_ICMS", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_ICMS = SomaC190Entrada("VL_ICMS", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_BC_ICMS_ST = SomaC190Entrada("VL_BC_ICMS_ST", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_ICMS_ST = SomaC190Entrada("VL_ICMS_ST", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_RED_BC = SomaC190Entrada("VL_RED_BC", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    RegistroC190.VL_IPI = SomaC190Entrada("VL_IPI", LIS_MOVPRODUTOESColl[0].NDOCUMENTO);
                    
                    RegistroC190.COD_OBS = "";
                    RegistroC100.RegistroC190.Add(RegistroC190);
                }

                ContadorEstoque++;

            }

           // if (PerfilApresentacao == 1) //Perfil A
            //{ 
//                var RegistroC350 = new RegistroC350();
            //}
            //else if (PerfilApresentacao == 1) //Perfil A
           // {
                //var RegistroC300 = new RegistroC300();
            //}
            

            //REGISTRO C500: NOTA FISCAL/CONTA DE ENERGIA ELÉTRICA (CÓDIGO 06), 
            //NOTA FISCAL/CONTA DE FORNECIMENTO D'ÁGUA CANALIZADA (CÓDIGO 29) E 
            //NOTA FISCAL CONSUMO FORNECIMENTO DE GÁS (CÓDIGO 28).           
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
            RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
            RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

            if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
            {
                RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
            }
            
            LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio);
          
            foreach (LIS_ESTOQUEESEntity LIS_ESTOQUEESTy in LIS_ESTOQUEESColl)
            {
                ESTOQUEESEntity ESTOQUEESTy = new ESTOQUEESEntity();
                ESTOQUEESTy = ESTOQUEESP.Read(Convert.ToInt32(LIS_ESTOQUEESTy.IDESTOQUEES));
                if (ESTOQUEESTy.FLAGENERGIATELECOM.Trim() == "S")
                {
                    var RegistroC500 = new RegistroC500();
                    RegistroC500.IND_OPER = TipoOperacao.EntradaAquisicao;
                    RegistroC500.IND_EMIT = Emitente.Terceiros;
                    RegistroC500.COD_PART = "FOR" + LIS_ESTOQUEESTy.IDFORNECEDOR.ToString().PadLeft(10, '0'); //Código do participante (campo 02 do Registro 0150): - do adquirente, no caso das saídas; - do fornecedor no caso de entradas
                    RegistroC500.COD_MOD = LIS_ESTOQUEESTy.MODELONF; //Código do modelo do documento fiscal, conforme a Tabela 4.1.1
                    RegistroC500.COD_SIT = SituacaoDocto.Regular; //Código da situação do documento fiscal, conforme a Tabela 4.1.2
                    RegistroC500.SER = LIS_ESTOQUEESTy.SERIENF;

                    RegistroC500.COD_CONS = "01";  
                    // Código de classe de consumo de energia elétrica ou gás:
                    //01 - Comercial
                    //02 - Consumo Próprio
                    //03 - Iluminação Pública
                    //04 - Industrial
                    //05 - Poder Público
                    //06 - Residencial
                    //07 - Rural

                    RegistroC500.NUM_DOC = Util.RetiraLetras(LIS_ESTOQUEESTy.NDOCUMENTO.Trim());//Número do documento fiscal
                    RegistroC500.DT_DOC = Convert.ToDateTime(LIS_ESTOQUEESTy.DATAMOVIM);//Data da emissão do documento fiscal N 008 
                    RegistroC500.DT_E_S = Convert.ToDateTime(LIS_ESTOQUEESTy.DATAMOVIM);//Data da entrada ou da saída
                    RegistroC500.VL_DOC = Convert.ToDecimal(LIS_ESTOQUEESTy.TOTALMOVIMENTACAO); //Valor total do documento fiscal
                    RegistroC500.VL_DESC = 0;//Valor total do desconto
                    RegistroC500.VL_FORN = Convert.ToDecimal(LIS_ESTOQUEESTy.TOTALMOVIMENTACAO); //Valor total fornecido/consumido
                    RegistroC500.VL_SERV_NT = 0; //Valor total dos serviços não-tributados pelo ICMS
                    RegistroC500.VL_TERC = 0; //Valor total cobrado em nome de terceiros
                    RegistroC500.VL_DA = 0; //Valor total de despesas acessórias indicadas no documento fiscal
                    RegistroC500.VL_BC_ICMS =Convert.ToDecimal(LIS_ESTOQUEESTy.VALORBASEICMS); //Valor acumulado da base de cálculo do ICMS
                    RegistroC500.VL_ICMS = Convert.ToDecimal(LIS_ESTOQUEESTy.VALORICMS); //Valor acumulado do ICMS
                    RegistroC500.VL_BC_ICMS_ST = 0; //Valor acumulado da base de cálculo do ICMS substituição tributária
                    RegistroC500.VL_ICMS_ST = 0; //Valor acumulado do ICMS retido por substituição tributária
                    RegistroC500.COD_INF =""; //Código da informação complementar do documento fiscal (campo 02 do Registro 0450)
                    RegistroC500.VL_PIS =0; //Valor do PIS
                    RegistroC500.VL_COFINS = 0; //Valor da COFINS
                    
                    RegistroC500.TP_LIGACAO = TipoLigacao.Nenhum;
                    //;Código de tipo de Ligação
                    //1 - Monofásico
                    //2 - Bifásico
                   //3 - Trifásico
                    RegistroC500.COD_GRUPO_TENSAO = GrupoTensao.Nenhum;

                    RegistroC001.RegistroC500.Add(RegistroC500);
                }
                
            }
        }
    
            
                  
        #endregion

        #region BLOCO E: APURAÇÃO DO ICMS E DO IPI
        public void GerarBlocoE()
        {
            var BlocoE = ACBrSpedFiscal.Bloco_E;

            // REGISTRO E001: ABERTURA DO BLOCO E
            var RegistroE001 = BlocoE.RegistroE001;
            //RegistroE001.IND_MOV = IndicadorMovimento.SemDados;
            BlocoE.RegistroE001.IND_MOV = IndicadorMovimento.ComDados;

            // REGISTRO E100: PERÍODO DA APURAÇÃO DO ICMS.
            var RegistroE100 = new RegistroE100();
            RegistroE100.DT_INI = Convert.ToDateTime(DataInicial);
            RegistroE100.DT_FIN = Convert.ToDateTime(DataFinal);            
            BlocoE.RegistroE001.RegistroE100.Add(RegistroE100);
            

            // REGISTRO E110: APURAÇÃO DO ICMS – OPERAÇÕES PRÓPRIAS.
            //FiscalApuracaoIcmsDTO ApuracaoIcms = new NHibernateDAL<FiscalApuracaoIcmsDTO>(session).selectId<FiscalApuracaoIcmsDTO>(1); //"COMPETENCIA=" DataInicial
            //if (ApuracaoIcms != null)
            {

                var RegistroE110 = RegistroE100.RegistroE110;

                RegistroE110.VL_TOT_DEBITOS = SomaICMS_E110_Debito(DataInicial, DataFinal);//Valor total dos débitos por "Saídas e prestações com débito do imposto".
                RegistroE110.VL_AJ_DEBITOS = 0; //Valor total dos ajustes a débito decorrentes do documento fiscal.
                RegistroE110.VL_TOT_AJ_DEBITOS = 0;//Valor total de "Ajustes a débito".
                RegistroE110.VL_ESTORNOS_CRED = 0;//Valor total de Ajustes "Estornos de créditos"
                RegistroE110.VL_TOT_CREDITOS = SomaICMS_E110_Credito(DataInicial, DataFinal); //Valor total dos créditos por "Entradas e aquisições com crédito do imposto".
                RegistroE110.VL_AJ_CREDITOS = 0; //Valor total dos ajustes a crédito decorrentes do documento fiscal.
                RegistroE110.VL_TOT_AJ_CREDITOS = 0; //Valor total de "Ajustes a crédito".
                RegistroE110.VL_ESTORNOS_DEB = 0;//Valor total de Ajustes "Estornos de Débitos".
                RegistroE110.VL_SLD_CREDOR_ANT = 0;//Valor total de "Saldo credor do período anterior".
                RegistroE110.VL_SLD_APURADO =0;//Valor do saldo devedor apurado.
                RegistroE110.VL_TOT_DED = 0;//Valor total de "Deduções".
                RegistroE110.VL_ICMS_RECOLHER = RegistroE110.VL_TOT_CREDITOS - RegistroE110.VL_TOT_DEBITOS;//	Valor total de "ICMS a recolher" (11-12).
                RegistroE110.VL_SLD_CREDOR_TRANSPORTAR = 0;//Valor total de "Saldo credor a transportar para o período seguinte".
                RegistroE110.DEB_ESP = 0;//Valores recolhidos ou a recolher, extraapuração..

                var RegistroE116 = new RegistroE116();
                RegistroE116.COD_OR = "000";
                RegistroE116.VL_OR = 0;//ApuracaoIcms.ValorIcmsRecolher;
                RegistroE116.COD_REC = "1";
                RegistroE116.NUM_PROC = "";
                RegistroE116.PROC = "";
                RegistroE116.TXT_COMPL = "";
                RegistroE116.MES_REF = "";
            }

          
            
        }
        #endregion

        #region BLOCO E: BLOCO H: INVENTÁRIO FÍSICO
        public void GerarBlocoH()
        {
            var BlocoH = ACBrSpedFiscal.Bloco_H;
            //string Data = Convert.ToDateTime(DataFinal).ToString("dd/MM/yyyy");
            string Data = DataFinal;

            // REGISTRO H001: ABERTURA DO BLOCO H
            var RegistroH001 = BlocoH.RegistroH001;
            if (Inventario == 0)
                RegistroH001.IND_MOV = IndicadorMovimento.SemDados;
            else
                RegistroH001.IND_MOV = IndicadorMovimento.ComDados;


           // IList<ProdutoDTO> ListaProduto = new NHibernateDAL<ProdutoDTO>(session).select(new ProdutoDTO());
            LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
            LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
            LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");
            decimal TotalGeral = 0;

            for (int i = 0; i < LIS_PRODUTOSColl.Count; i++)
            {
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSColl[i].IDPRODUTO), Data, true);
                if (ESTOQUEATUAL > 0)
                    TotalGeral += ESTOQUEATUAL * Convert.ToDecimal(LIS_PRODUTOSColl[i].VALORCUSTOFINAL);
            }


            // REGISTRO H005: TOTAIS DO INVENTÁRIO
            var RegistroH005 = new RegistroH005();        
            RegistroH005.DT_INV = Convert.ToDateTime(Data);
            RegistroH005.VL_INV = TotalGeral;
            RegistroH005.MOT_INV = MotivoInventario.FinalPeriodo;
            BlocoH.RegistroH001.RegistroH005.Add(RegistroH005);
            
            // REGISTRO H010: INVENTÁRIO.
          //  foreach (ProdutoDTO Produto in ListaProduto)
            foreach (LIS_PRODUTOSEntity Produto in LIS_PRODUTOSColl)
            {
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(Produto.IDPRODUTO), Data, true);
                 if (ESTOQUEATUAL > 0)
                 {
                     var RegistroH010 = new RegistroH010();
                     RegistroH010.COD_ITEM = Convert.ToString(Produto.IDPRODUTO).PadLeft(10, '0'); 
                     RegistroH010.UNID = Convert.ToString(Produto.NOMEUNIDADE);
                     RegistroH010.QTD = ESTOQUEATUAL;
                     RegistroH010.VL_UNIT = Convert.ToInt32(Produto.VALORCUSTOFINAL);
                     RegistroH010.VL_ITEM = RegistroH010.QTD * RegistroH010.VL_UNIT;
                     RegistroH010.IND_PROP = PosseItem.Informante;
                     RegistroH005.RegistroH010.Add(RegistroH010);
                 }
            }

            BlocoH.RegistroH001.RegistroH005.Add(RegistroH005);

            // REGISTRO H020: Informação complementar do Inventário.
            //{ Implementado a critério do Participante do T2Ti ERP }

        }
        #endregion

        #region BLOCO 1: OUTRAS INFORMAÇÕES
        public void GerarBloco1()
        {
            var Bloco1 = ACBrSpedFiscal.Bloco_1;

            // REGISTRO 1001: ABERTURA DO BLOCO 1
            var Registro1001 = Bloco1.Registro1001;
            Bloco1.Registro1001.IND_MOV = IndicadorMovimento.ComDados;

            // REGISTRO 1010: OBRIGATORIEDADE DE REGISTROS DO BLOCO 1
            var Registro1010 = new Registro1010();
            Registro1010.IND_EXP = "N";        //1100
            Registro1010.IND_CCRF = "N";       //1200
            Registro1010.IND_COMB = "N";       //1300
            Registro1010.IND_USINA = "N";      //1390
            Registro1010.IND_VA = "N";         //1400
            Registro1010.IND_EE = "N";         //1500
            Registro1010.IND_CART = "N";       //1600
            Registro1010.IND_FORM = "N";       //1700
            Registro1010.IND_AER = "N";        //1800
            Bloco1.Registro1001.Registro1010.Add(Registro1010);
            
        }
        #endregion

        #region Gerar Arquivo       
       
        public bool GerarArquivoSpedFiscal(string pDataIni, string pDataFim, int pVersao, int pFinalidade, int pPerfil, 
                                           int pIdEmpresa, int pInventario, int pIdContador)
        {
            errolog = false;
            VersaoLeiaute = pVersao;
            FinalidadeArquivo = pFinalidade;
            PerfilApresentacao = pPerfil;
            DataInicial = pDataIni;
            DataFinal = pDataFim;
            IdEmpresa = pIdEmpresa;
            Inventario = pInventario;
            IdContador = pIdContador;

            EMPRESATy = EMPRESAP.Read(1);

            ACBrSpedFiscal.DT_INI = Convert.ToDateTime(pDataIni);
            ACBrSpedFiscal.DT_FIN = Convert.ToDateTime(pDataFim);

           // using (session = NHibernateHelper.getSessionFactory().OpenSession())
            {
                if (pPerfil == 2)
                    GerarBloco0();
                else
                    GerarBloco0();

                //NOTA DE SAIDA
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                //Nota de Entrada
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGENERGIATELECOM", "System.String", "=", "N"));
                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");
                
                if (LIS_NOTAFISCALEColl.Count > 0 || LIS_MOVPRODUTOESColl.Count > 0)
                    GerarBlocoC();
                
                // BLOCO D: DOCUMENTOS FISCAIS II - SERVIÇOS (ICMS).
                // Bloco de registros dos dados relativos à emissão ou ao recebimento de documentos fiscais que acobertam as prestações de serviços de comunicação, transporte intermunicipal e interestadual.
                //{ Implementado a critério do Participante do T2Ti ERP }

                GerarBlocoE();

                // REGISTRO D001: ABERTURA DO BLOCO D
                var BlocoD = ACBrSpedFiscal.Bloco_D;
                var RegistroD001 = BlocoD.RegistroD001;
                RegistroD001.IND_MOV = IndicadorMovimento.SemDados;

                // BLOCO G – CONTROLE DO CRÉDITO DE ICMS DO ATIVO PERMANENTE CIAP
                var BlocoG = ACBrSpedFiscal.Bloco_G;
                var RegistroG001 = BlocoG.RegistroG001;
                RegistroG001.IND_MOV = IndicadorMovimento.SemDados;

                // REGISTRO H001: ABERTURA DO BLOCO H
                var BlocoH = ACBrSpedFiscal.Bloco_H;
                var RegistroH001 = BlocoH.RegistroH001;
                RegistroH001.IND_MOV = IndicadorMovimento.SemDados; 


                if (Inventario > 0)
                    GerarBlocoH();

                GerarBloco1();
            }

            ACBrSpedFiscal.Path = BmsSoftware.ConfigSistema1.Default.PathInstall;
            ACBrSpedFiscal.SaveFileTXT();
            return true;
        }
        #endregion

        private void GeraArquivoLog(string Messagem)
        {
            try
            {
                errolog = true;
                StreamWriter valor = new StreamWriter(pathLog, true, System.Text.Encoding.ASCII);
                valor.WriteLine(Messagem);
                valor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }

        //BUsca o codigo do ibge
        private int BuscaCodIBGE(string Municipio, string UF)
        {
            int result = -1;

            try
            {
                //Busca a cidade
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", Municipio.Replace("'", "")));
                RowRelatorio.Add(new RowsFiltro("uf", "System.String", "=", UF));
                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_MUNICIPIOSColl.Count > 0)
                    result = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        //Filtra as Nfe emitidas no periodo FiltraNotaSaida REGISTRO 0150: CADASTRO DO PARTICIPANTE
        public void FiltraNotaSaidaCliente_REGISTRO_0150(string DataInicial, string DataFinal)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "IDCLIENTE");
                
                //Remove IDCliente Repetido
                LIS_NOTAFISCALECollection LIS_NOTAFISCALE2Coll = new LIS_NOTAFISCALECollection();
                foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
                {
                    if (LIS_NOTAFISCALE2Coll.Find(delegate(LIS_NOTAFISCALEEntity item2) { return (item2.IDCLIENTE == item.IDCLIENTE); }) == null)
                    {
                        LIS_NOTAFISCALE2Coll.Add(item);
                    }
                }

                LIS_NOTAFISCALEColl.Clear();
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALE2Coll;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        //Filtra as Nfe emitidas no periodo
        public void FiltraNotaSaida(string DataInicial, string DataFinal)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S" ));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                //Remove IDCliente Repetido
                LIS_NOTAFISCALECollection LIS_NOTAFISCALE2Coll = new LIS_NOTAFISCALECollection();
                foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
                {
                    //Armazena CFOP
                    TributOperacaoFiscalDTO TributOperacaoFiscaldto = new TributOperacaoFiscalDTO();
                    TributOperacaoFiscaldto.Id =Convert.ToInt32(item.IDCFOP);
                    TributOperacaoFiscaldto.Descricao = item.DESCCFOP;
                    TributOperacaoFiscaldto.Cfop = Convert.ToInt32(Util.RetiraLetras(item.CODCFOP));
                    ListaOperacaoFiscal.Add(TributOperacaoFiscaldto);
                    
                    if (LIS_NOTAFISCALE2Coll.Find(delegate(LIS_NOTAFISCALEEntity item2) { return (item2.IDCLIENTE == item.IDCLIENTE); }) == null)
                    {
                        LIS_NOTAFISCALE2Coll.Add(item);
                    }
                }

                LIS_NOTAFISCALEColl.Clear();
                LIS_NOTAFISCALEColl = LIS_NOTAFISCALE2Coll;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        //Filtra as os fornecedores REGISTRO 0150:
        public void FiltraNotaEntradaFornecedoresR0150(string DataInicial, string DataFinal)
        {
            try
            {
                //Movimentação de Entrada               
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio, "IDFORNECEDOR");
                
                //  Remove idfornecedor repetido                  
                LIS_ESTOQUEESCollection LIS_ESTOQUEESColl2 = new LIS_ESTOQUEESCollection();
                foreach (LIS_ESTOQUEESEntity item in LIS_ESTOQUEESColl)
                {
                    if (LIS_ESTOQUEESColl2.Find(delegate(LIS_ESTOQUEESEntity item2) { return (item2.IDFORNECEDOR == item.IDFORNECEDOR); }) == null)
                    {
                        LIS_ESTOQUEESColl2.Add(item);
                    }
                }

                LIS_ESTOQUEESColl.Clear();
                LIS_ESTOQUEESColl = LIS_ESTOQUEESColl2;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        //Filtra as Nota de Entrada no periodo
        public void FiltraNotaEntrada(string DataInicial, string DataFinal)
        {
            try
            {
                //Movimentação de Entrada               
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                //  Remove idfornecedor repetido                  
                LIS_ESTOQUEESCollection LIS_ESTOQUEESColl2 = new LIS_ESTOQUEESCollection();
                foreach (LIS_ESTOQUEESEntity item in LIS_ESTOQUEESColl)
                {
                    //Armazena CFOP
                    TributOperacaoFiscalDTO TributOperacaoFiscaldto = new TributOperacaoFiscalDTO();
                    TributOperacaoFiscaldto.Id = Convert.ToInt32(item.IDCFOP);
                    TributOperacaoFiscaldto.Descricao = item.DESCCFOP;
                    TributOperacaoFiscaldto.Cfop = Convert.ToInt32(Util.RetiraLetras(item.CODCFOP));
                    ListaOperacaoFiscal.Add(TributOperacaoFiscaldto);

                    if (LIS_ESTOQUEESColl2.Find(delegate(LIS_ESTOQUEESEntity item2) { return (item2.IDFORNECEDOR == item.IDFORNECEDOR); }) == null)
                    {
                        LIS_ESTOQUEESColl2.Add(item);
                    }
                }

                LIS_ESTOQUEESColl.Clear();
                LIS_ESTOQUEESColl = LIS_ESTOQUEESColl2;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public void BuscaProdutoSaidaNFe(string DataInicial, string DataFinal)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);
                

                //Remove IDPRODUTO Repetido
                LIS_PRODUTONFECollection LIS_PRODUTONFE2Coll = new LIS_PRODUTONFECollection();
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    if (LIS_PRODUTONFE2Coll.Find(delegate(LIS_PRODUTONFEEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                    {
                        LIS_PRODUTONFE2Coll.Add(item);
                    }
                }

                LIS_PRODUTONFEColl.Clear();
                LIS_PRODUTONFEColl = LIS_PRODUTONFE2Coll;

                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    //Classe de produto do sistema
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                    //Classe de produto para geração do Sped
                    ProdutoDTO Produtodto = new ProdutoDTO();
                    Produtodto.Id = Convert.ToInt32(item.IDPRODUTO);
                    Produtodto.Nome = item.NOMEPRODUTO;
                    Produtodto.Gtin = PRODUTOSTy.CODBARRA;

                    if (item.NOMEUNIDADE.Length > 0)
                        Produtodto.UnidadeProduto = Util.LimiterText(item.NOMEUNIDADE, 6);
                    else
                        GeraArquivoLog("Unidade não cadastrada para o produto: Cód.: " + item.IDPRODUTO.ToString() + " " + item.NOMEPRODUTO);

                    if (PRODUTOSTy.TIPOITEM.Trim() != string.Empty)
                        Produtodto.TipoItemSped = PRODUTOSTy.TIPOITEM;
                    else
                        Produtodto.TipoItemSped = "99";

                    Produtodto.Ncm = Util.RetiraLetras(PRODUTOSTy.NCMSH);
                    ListaProduto.Add(Produtodto);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public void BuscaProdutoEntrada(string DataInicial, string DataFinal)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGENERGIATELECOM", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                //Remove IDPRODUTO Repetido
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOES2Coll = new LIS_MOVPRODUTOESCollection();
                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {
                    if (LIS_MOVPRODUTOES2Coll.Find(delegate(LIS_MOVPRODUTOESEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                    {
                        LIS_MOVPRODUTOES2Coll.Add(item);
                    }
                }

                LIS_MOVPRODUTOESColl.Clear();
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOES2Coll;

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {
                    //Classe de produto do sistema
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                    //Classe de produto para geração do Sped
                    ProdutoDTO Produtodto = new ProdutoDTO();
                    Produtodto.Id = Convert.ToInt32(item.IDPRODUTO);
                    Produtodto.Nome = item.NOMEPRODUTO;
                    Produtodto.Gtin = PRODUTOSTy.CODBARRA;

                    UNIDADEEntity UNIDADETy = new UNIDADEEntity();
                    UNIDADEProvider UNIDADEP = new UNIDADEProvider();
                    UNIDADETy = UNIDADEP.Read(Convert.ToInt32(PRODUTOSTy.IDUNIDADE));

                    if (UNIDADETy != null)
                        Produtodto.UnidadeProduto = Util.LimiterText(UNIDADETy.NOME, 6);
                    else
                        GeraArquivoLog("Unidade não cadastrada para o produto: Cód.: " + item.IDPRODUTO.ToString() + " " +item.NOMEPRODUTO);

                    if (PRODUTOSTy.TIPOITEM.Trim() != string.Empty)
                        Produtodto.TipoItemSped = PRODUTOSTy.TIPOITEM;
                    else
                        Produtodto.TipoItemSped = "99";

                    Produtodto.Ncm = Util.RetiraLetras(PRODUTOSTy.NCMSH);
                    ListaProduto.Add(Produtodto);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

          public void BuscaUnidade()
        {
            try
            {
                RowRelatorio.Clear();                
                UNIDADEColl = UNIDADEP.ReadCollectionByParameter(RowRelatorio);

                //Remove IDUNIDADE Repetido
                UNIDADECollection UNIDADE2Coll = new UNIDADECollection();
                foreach (UNIDADEEntity item in UNIDADEColl)
                {
                    if (UNIDADE2Coll.Find(delegate(UNIDADEEntity item2) { return (item2.IDUNIDADE == item.IDUNIDADE); }) == null)
                    {
                        UNIDADE2Coll.Add(item);
                    }
                }

                UNIDADEColl.Clear();
                UNIDADEColl = UNIDADE2Coll; 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

          private Decimal SomaC190(string Tipo, string NOTAFISCALE)
          {
              decimal result = 0;
              try
              {

                  //Filtra Produtos Nota Fiscal
                  RowRelatorio.Clear();
                  RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial.ToString())));
                  RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal.ToString())));
                  RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                  RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
                  RowRelatorio.Add(new RowsFiltro("NOTAFISCALE", "System.String", "=", NOTAFISCALE));

                  if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                  {
                      RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                  }

                  LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
                  LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO");
    
                  // Remove CFOP, Aliq. ICMS  e nota fiscal repetido
                  //LIS_PRODUTONFECollection LIS_PRODUTONFE_2_2Coll = new LIS_PRODUTONFECollection();
                  //foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                  //{

                  //    if (LIS_PRODUTONFE_2_2Coll.Find(delegate(LIS_PRODUTONFEEntity item2) { return (item2.IDCFOP == item.IDCFOP && item2.ALICMS == item.ALICMS && item2.IDCST == item.IDCST); }) == null)
                  //    {
                  //        LIS_PRODUTONFE_2_2Coll.Add(item);
                  //    }
                  //}
                  //LIS_PRODUTONFE_2Coll.Clear();
                  //LIS_PRODUTONFE_2Coll = LIS_PRODUTONFE_2_2Coll;

                  foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                  {
                      if (Tipo == "VL_OPR")
                          result += Convert.ToDecimal(item.VALORTOTAL);
                      else if (Tipo == "VL_BC_ICMS")
                      {
                          if (item.VALORICMS > 0)
                              result += Convert.ToDecimal(item.BASEICMS);
                          else
                              result += 0;
                      }
                      else if (Tipo == "VL_ICMS")
                          result += Convert.ToDecimal(item.VALORICMS);
                      else if (Tipo == "VL_BC_ICMS_ST")
                          result += Convert.ToDecimal(item.VLBASEST);
                      else if (Tipo == "VL_ICMS_ST")
                          result += Convert.ToDecimal(item.VLICMSST);
                      else if (Tipo == "VL_RED_BC")
                          result += Convert.ToDecimal(item.REDICMS);
                      else if (Tipo == "VL_IPI")
                          result += Convert.ToDecimal(item.VALORIPI);
                  }

                  return result;
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Erro técnico: " + ex.Message);
                  return result;
              }

          }

        private Decimal SomaC100Entrada(string Tipo, string NDOCUMENTO)
        {
            decimal result = 0;
            try
            {

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGENERGIATELECOM", "System.String", "=", "N"));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl_SomaC190 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
                LIS_MOVPRODUTOESColl_SomaC190 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");


                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl_SomaC190)
                {
                    if (Tipo == "VL_OPR")
                         result += Convert.ToDecimal(item.VALORTOTAL);
                    else if (Tipo == "VL_BC_ICMS")
                        result += Convert.ToDecimal(item.BASEICMS);
                    else if (Tipo == "VL_ICMS")
                        result += Convert.ToDecimal(item.VLICMS);
                    else if (Tipo == "VL_BC_ICMS_ST")
                        result += Convert.ToDecimal(item.VLBASEICMSST);
                    else if (Tipo == "VL_ICMS_ST")
                        result += Convert.ToDecimal(item.VLICMSST);
                    else if (Tipo == "VL_RED_BC")
                        result += Convert.ToDecimal(item.BASEICMS);
                    else if (Tipo == "VL_IPI")
                        result += Convert.ToDecimal(item.VLIPI);
                    else if (Tipo == "VL_MERC")
                        //  result += ((Convert.ToDecimal(item.VALORCUNITARIO) * Convert.ToDecimal(item.QUANTIDADE)) - Convert.ToDecimal(item.VLDESCONTOPRODUTO));
                        result += (Convert.ToDecimal(item.VALORCUNITARIO) * Convert.ToDecimal(item.QUANTIDADE));
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

        private Decimal SomaC190Entrada(string Tipo, string NDOCUMENTO)
        {
            decimal result = 0;
            try
            {

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGENERGIATELECOM", "System.String", "=", "N"));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }
                
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl_SomaC190 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
                LIS_MOVPRODUTOESColl_SomaC190 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");
                
                //// Remove CFOP, Aliq. ICMS  e nota fiscal repetido
                //LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl2 = new LIS_MOVPRODUTOESCollection();
                //foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl_SomaC190)
                //{

                //    if (LIS_MOVPRODUTOESColl2.Find(delegate(LIS_MOVPRODUTOESEntity item2) { return (item2.IDCFOP == item.IDCFOP && item2.ALQICMS == item.ALQICMS && item2.CST_CSOSN == item.CST_CSOSN); }) == null)
                //    {
                //        LIS_MOVPRODUTOESColl2.Add(item);
                //    }
                //}
                //LIS_MOVPRODUTOESColl_SomaC190.Clear();
                //LIS_MOVPRODUTOESColl_SomaC190 = LIS_MOVPRODUTOESColl2;

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl_SomaC190)
                {
                    if (Tipo == "VL_OPR")
                        result += Convert.ToDecimal(item.VALORTOTAL);
                    else if (Tipo == "VL_BC_ICMS")
                        result += Convert.ToDecimal(item.BASEICMS);
                    else if (Tipo == "VL_ICMS")
                        result += Convert.ToDecimal(item.VLICMS);
                    else if (Tipo == "VL_BC_ICMS_ST")
                        result += Convert.ToDecimal(item.VLBASEICMSST);
                    else if (Tipo == "VL_ICMS_ST")
                        result += Convert.ToDecimal(item.VLICMSST);
                    else if (Tipo == "VL_RED_BC")
                        result += Convert.ToDecimal(item.BASEICMS);
                    else if (Tipo == "VL_IPI")
                        result += Convert.ToDecimal(item.VLIPI);
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }
      

        private Decimal SomaC460(string Tipo, DateTime Data)
        {
            decimal result = 0;

            try
            {
                //Soma os impostos ECF
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "=", Util.ConverStringDateSearch(Data.ToString())));
                ITEVENDAS_ECFCollection ITEVENDAS_ECFColl = new ITEVENDAS_ECFCollection();
                ITEVENDAS_ECFProvider ITEVENDAS_ECFP = new ITEVENDAS_ECFProvider();
                ITEVENDAS_ECFColl = ITEVENDAS_ECFP.ReadCollectionByParameter(RowRelatorio, "DATA");

                foreach (ITEVENDAS_ECFEntity item in ITEVENDAS_ECFColl)
                {
                    if (Tipo == "TOT_PIS")
                        result += Convert.ToDecimal(item.TOT_PIS);
                    else if (Tipo == "TOT_COFINS")
                        result += Convert.ToDecimal(item.TOT_COFINS);
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

        private Decimal SomaICMS_E110_Debito(string DataInicial, string DataFinal)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=",Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=",Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                {
                    result += Convert.ToDecimal(item.VALORICMS);
                }

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

        private Decimal SomaICMS_E110_Credito(string DataInicial, string DataFinal)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=",Util.ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(DataFinal)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGENERGIATELECOM", "System.String", "=", "N"));

                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                {
                    RowRelatorio.Add(new RowsFiltro("CNPJEMISSOR", "System.String", "=", EMPRESATy.CNPJCPF.Trim()));
                }

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);
                
                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {
                    result += Convert.ToDecimal(item.VLICMS);
                }

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

    

    }

}
