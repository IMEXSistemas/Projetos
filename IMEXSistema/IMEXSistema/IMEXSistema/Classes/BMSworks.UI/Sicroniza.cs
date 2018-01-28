using BMSworks.Firebird;
using BMSworks.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BMSworks.UI
{
    public partial class Sicroniza
    {
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        STATUSProvider STATUSP = new STATUSProvider();
        GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
        UNIDADEProvider UNIDADEP = new UNIDADEProvider();
        MARCAProvider MARCAP = new MARCAProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        AMBIENTEProvider AMBIENTEP = new AMBIENTEProvider();
        CORProvider CORP = new CORProvider();
        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();

        EMPRESAEntity EMPRESAtY = new EMPRESAEntity();
        STATUSEntity STATUSTy = new STATUSEntity();
        UNIDADEEntity UNIDADETy = new UNIDADEEntity();
        MARCAEntity MARCATy = new MARCAEntity();
        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
        CLIENTEEntity CLIENTETy = new CLIENTEEntity();
        GRUPOCATEGORIAEntity GRUPOCATEGORIATy = new GRUPOCATEGORIAEntity();
        AMBIENTEEntity AMBIENTETy = new AMBIENTEEntity();
        COREntity CORTy = new COREntity();
        FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();

        private void Upload(string filename, string CNPJ)
        {
            FileInfo fileInf = new FileInfo(filename);
           // string uri = "ftp://192.185.170.200/www/arquivos/" + "/" + CNPJ + "/"+ fileInf.Name;
            FtpWebRequest reqFTP;

            //Criar objeto FtpWebRequest do Uri, desde
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://192.185.170.200/www/arquivos/" + "/" + CNPJ + "/" + fileInf.Name));

            // Fornecer os Credintiais de permissao
            string UsuarioFTP = "imexsist";
            string SenhaFTP = "rmr877701";
            reqFTP.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);

            // Por padrão KeepAlive é verdadeiro, em que a ligação de controle não está fechado
            // após um comando é executado.
            reqFTP.KeepAlive = false;

            //Especifique o comando a ser executado.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Especifique o tipo de transferência de dados.
            reqFTP.UseBinary = true;

            // Notificar o servidor sobre o tamanho do arquivo enviado
            reqFTP.ContentLength = fileInf.Length;

            // O tamanho do buffer é definido para 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();

                
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Não foi possível enviar o arquivo de sicronização");
            }
        }

        public void CriaArquivoCSV()
        {
            try
            {
                System.IO.StreamWriter sw = null;

                try
                {
                    //Pega o caminho do arquivo
                    string caminho = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\sicronizacao.csv";
                    //Cria um StreamWriter no local
                    sw = new System.IO.StreamWriter(caminho, false, System.Text.Encoding.GetEncoding(1252));

                    //Dados de Status                  
                    STATUSCollection STATUSColl = new STATUSCollection();
                    STATUSColl = STATUSP.ReadCollectionByParameter(null);
                    foreach (var item in STATUSColl)
                    {
                        //Dados de Status
                        string IDSTATUS = Convert.ToInt32(item.IDSTATUS).ToString().PadLeft(6, '0');
                        string IDGRUPOSTATUS = "000000";
                        if (ValidacoesLibrary.ValidaTipoInt32(item.IDGRUPOSTATUS.ToString()))
                        {
                            IDGRUPOSTATUS = Convert.ToInt32(item.IDGRUPOSTATUS).ToString().PadLeft(6, '0');
                            string NOMESTATUS = item.NOME;
                            //grava dados de Status
                            sw.WriteLine("STA" + ";" + IDSTATUS + ";" + IDGRUPOSTATUS + ";" + NOMESTATUS);
                        }
                    }

                    //Categoria Produto                  
                    GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
                    GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null);
                    foreach (var item2 in GRUPOCATEGORIAColl)
                    {
                        //Dados de Categoria Produto
                        string IDGRUPOCATEGORIA = Convert.ToInt32(item2.IDGRUPOCATEGORIA).ToString().PadLeft(6, '0');
                        string NOMEGRUPOCATEGORIA = item2.NOME;

                        //grava dados de Categoria Produto
                        sw.WriteLine("GSA" + ";" + IDGRUPOCATEGORIA + ";" + NOMEGRUPOCATEGORIA);
                    }

                    //Categoria Produto                   
                    UNIDADECollection UNIDADEColl = new UNIDADECollection();
                    UNIDADEColl = UNIDADEP.ReadCollectionByParameter(null);
                    foreach (var item3 in UNIDADEColl)
                    {
                        //Dados Unidade
                        string IDUNIDADE = Convert.ToInt32(item3.IDUNIDADE).ToString().PadLeft(6, '0');
                        string NOMEUNIDADE = item3.NOME;

                        //grava dados da Unidade
                        sw.WriteLine("UNI" + ";" + IDUNIDADE + ";" + NOMEUNIDADE);
                    }

                   //Dados da Marca
                    MARCACollection MARCAColl = new MARCACollection();
                    MARCAColl = MARCAP.ReadCollectionByParameter(null);
                    foreach (var item5 in MARCAColl)
                    {
                        //Dados MARCA
                        string IDMARCA = Convert.ToInt32(item5.IDMARCA).ToString().PadLeft(6, '0');
                        string NOMEMARCA = item5.NOME;

                        //grava dados da MARCA
                        sw.WriteLine("MAR" + ";" + IDMARCA + ";" + NOMEMARCA);
                    }

                    //Cor
                    CORCollection CORColl = new CORCollection();                                      
                    CORColl = CORP.ReadCollectionByParameter(null);
                    foreach (var item9 in CORColl)
                    {
                        //Dados Cor
                        string IDCOR = Convert.ToInt32(item9.IDCOR).ToString().PadLeft(6, '0');
                        string NOMECOR = item9.NOME;

                        //grava dados da Cor
                        sw.WriteLine("COR" + ";" + IDCOR + ";" + NOMECOR);
                    }

                    //Ambiente
                    AMBIENTECollection AMBIENTEColl = new AMBIENTECollection();
                    AMBIENTEColl = AMBIENTEP.ReadCollectionByParameter(null);
                    foreach (var item8 in AMBIENTEColl)
                    {
                        //Dados AMBIENTE
                        string IDAMBIENTE = Convert.ToInt32(item8.IDAMBIENTE).ToString().PadLeft(6, '0');
                        string NOMEAMBIENTE = item8.NOME;

                        //grava dados do Ambiente
                        sw.WriteLine("AMB" + ";" + IDAMBIENTE + ";" + NOMEAMBIENTE);
                    }

                    //Vendedor
                    FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
                    FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null);
                    foreach (var item10 in FUNCIONARIOColl)
                    {
                        //Dados FUNCIONARIO
                        string IDFUNCIONARIO = Convert.ToInt32(item10.IDFUNCIONARIO).ToString().PadLeft(6, '0');
                        string NOMEFUNCINAROIO = item10.NOME;
                        string ENDERECO = item10.ENDERECO;
                        string CIDADE = item10.CIDADE;
                        string UF = item10.UF;
                        string CEP = item10.CEP;
                        string BAIRRO = item10.BAIRRO;
                        string CPF = item10.CPF;
                        string EMAIL = item10.EMAIL;
                        string TELEFONE1 = item10.TELEFONE1;
                        string TELEFONE2 = item10.TELEFONE2;
                        string FUNCAO = item10.FUNCAO;

                        //grava dados do FUNCIONARIO
                        sw.WriteLine("FUNC" +  ";" + IDFUNCIONARIO + ";" + NOMEFUNCINAROIO + ";" + ENDERECO + ";" + CIDADE + ";" + UF + ";" + CEP + ";" + BAIRRO + ";" + CPF + ";" +
                                                EMAIL + ";" + TELEFONE1 + ";" + TELEFONE2 + ";" + FUNCAO);
                    }                    

                    //Cadastro de Produto                   
                    PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
                    PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(null);
                    foreach (var item4 in PRODUTOSColl)
                    {
                        //Dados Produto
                        string IDPRODUTO = Convert.ToInt32(item4.IDPRODUTO).ToString().PadLeft(6, '0');
                        string NOMEPRODUTO = item4.NOMEPRODUTO;
                        string CODPRODUTOFORNECEDOR = item4.CODPRODUTOFORNECEDOR.ToString().PadLeft(10, '0');
                        string VALORVENDA1 = Convert.ToDecimal(item4.VALORVENDA1).ToString("n2");

                        string IDUNIDADE = "000000";
                         if (ValidacoesLibrary.ValidaTipoInt32(item4.IDUNIDADE.ToString()))
                            IDUNIDADE = item4.IDUNIDADE.ToString().PadLeft(6, '0');

                         string IDGRUPOCATEGORIA = "000000";
                         if (ValidacoesLibrary.ValidaTipoInt32(item4.IDGRUPOCATEGORIA.ToString()))
                             IDGRUPOCATEGORIA = item4.IDGRUPOCATEGORIA.ToString().PadLeft(6, '0');

                         string IDSTATUS = "000000";
                         if (ValidacoesLibrary.ValidaTipoInt32(item4.IDSTATUS.ToString()))
                             IDSTATUS = item4.IDSTATUS.ToString().PadLeft(6, '0');

                         string IDMARCA = "000000";
                         if (ValidacoesLibrary.ValidaTipoInt32(item4.IDMARCA.ToString()))
                             IDMARCA = item4.IDMARCA.ToString().PadLeft(6, '0');

                         string FLAGINATIVO = item4.FLAGINATIVO;
                        

                        //grava dados da Produto
                        sw.WriteLine("PRO" + ";" + IDPRODUTO + ";" + NOMEPRODUTO + ";" + CODPRODUTOFORNECEDOR + ";" + VALORVENDA1 + ";" + IDUNIDADE + ";"
                                    + IDGRUPOCATEGORIA + ";" + IDSTATUS + ";" + IDMARCA + ";" + FLAGINATIVO);
                    }

                    //Cadastro de Cliente
                    CLIENTECollection CLIENTEColl = new CLIENTECollection();                   
                    CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null);
                    foreach (var item6 in CLIENTEColl)
                    {
                        string IDCLIENTE = Convert.ToInt32(item6.IDCLIENTE).ToString().PadLeft(6, '0');
                        string NOMECLIENTE = item6.NOME;
                        string TELEFONE1 = item6.TELEFONE1;
                        string TELEFONE2 = item6.TELEFONE2;
                        string CPF = item6.CPF;
                        string CNPJ = item6.CNPJ;
                        string IE = item6.IE;
                        string ENDERECO1 = item6.ENDERECO1;
                        string NUMEROENDER = item6.NUMEROENDER;
                        string COMPLEMENTO1 = item6.COMPLEMENTO1;
                        string BAIRRO1 = item6.BAIRRO1;
                        string CEP1 = item6.CEP1;
                        string COD_MUN_IBGE = item6.COD_MUN_IBGE.ToString();
                        string EMAILCLIENTE = item6.EMAILCLIENTE;
                        string FLAGBLOQUEADO = item6.FLAGBLOQUEADO;

                        sw.WriteLine("CLI" + ";" + IDCLIENTE + ";" + NOMECLIENTE + ";" + TELEFONE1 + ";" + TELEFONE2 + ";" + CPF + ";" +
                                    CNPJ + ";" + IE + ";" + ENDERECO1 + ";" + NUMEROENDER + ";" + COMPLEMENTO1 + ";" + BAIRRO1 + ";" + CEP1 +";" +
                                    COD_MUN_IBGE + ";" + EMAILCLIENTE + ";" + FLAGBLOQUEADO);
                    }


                    sw.Close();

                    //Dados da Empresa
                    EMPRESAtY = EMPRESAP.Read(1);

                    //faz o upload do arquivo sicronizado
                    if (File.Exists(caminho))
                        Upload(caminho, Util.RetiraLetras(EMPRESAtY.CNPJCPF));
                    else
                        MessageBox.Show("Arquivo: " + caminho + " Não Localizado!");
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //Fechar stream SEMPRE
                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar arquivo de sicronização!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public void Download(string Filename)
        {
            WebClient webClient = new WebClient();

            try
            {
                //Dados da Empresa               
                EMPRESAtY = EMPRESAP.Read(1);
                string CNPJ = Util.RetiraLetras(EMPRESAtY.CNPJCPF);
          
                webClient.DownloadFile("http://arquivos.imexsistema.com.br/" + CNPJ + "/" + Filename, BmsSoftware.ConfigSistema1.Default.PathInstall + @"\" + Filename);
                webClient.Dispose();


                AtualizaBD(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\" + Filename);
            }
            catch (Exception ex)
            {
                webClient.Dispose();

                MessageBox.Show("Não foi possível fazer o download do " + Filename);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AtualizaBD(string FileName)
        {
            StreamReader rd = null;
            Stream myStream = null;
            string MsgErro = string.Empty;
            try
            {
                //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                rd = new StreamReader(FileName);
                //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                string linha = null;
                //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                string[] linhaseparada = null;
                //realizo o while para ler o conteudo da linha 

                while ((linha = rd.ReadLine()) != null)
                {
                    //com o split adiciono a string 'quebrada' dentro do array 
                    linhaseparada = linha.Split(';');
                    //aqui incluo o método necessário para continuar o trabalho 

                    //STA
                    if (linhaseparada[0] == "STA") //Salva Dados de Status
                     {
                        //Verifica se o Status Existe no Banco de Dados                        
                         if (VerificaExisteStatus(Convert.ToInt32(linhaseparada[1])))
                         {
                             MsgErro = "STA " + linhaseparada[1];
                             STATUSTy = STATUSP.Read(Convert.ToInt32(linhaseparada[1]));
                             if (Convert.ToInt32(linhaseparada[2]) > 0)
                             {
                                 STATUSTy.IDGRUPOSTATUS = Convert.ToInt32(linhaseparada[2]);
                                 STATUSTy.NOME = linhaseparada[3];
                                 STATUSP.Save(STATUSTy);
                             }
                         }
                        else
                         {
                             MsgErro = "STA " + linhaseparada[1];
                             STATUSTy.IDSTATUS = -1;
                             STATUSTy.IDGRUPOSTATUS = Convert.ToInt32(linhaseparada[2]);
                             STATUSTy.NOME = linhaseparada[3];
                             STATUSP.Save(STATUSTy);
                         }
                     }
                    else if (linhaseparada[0] == "UNI") //Salva Dados Unidade
                    {
                        //Verifica se a Unidade Existe no Banco de Dados                      
                        if (VerificaExisteUnidade(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "UNI " + linhaseparada[1];
                            UNIDADETy = UNIDADEP.Read(Convert.ToInt32(linhaseparada[1]));
                            UNIDADETy.NOME = linhaseparada[2];
                            UNIDADEP.Save(UNIDADETy);
                        }
                        else
                        {
                            MsgErro = "UNI " + linhaseparada[1];
                            UNIDADETy.IDUNIDADE = -1;
                            UNIDADETy.NOME = linhaseparada[2];
                            UNIDADEP.Save(UNIDADETy);
                        }
                    }
                    else if (linhaseparada[0] == "GSA") //Grupo Categoria de Produto
                    {
                        //Verifica se a Categoria de Produto Existe no Banco de Dados                       
                        if (VerificaExisteGrupoCategoria(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "GSA " + linhaseparada[1];
                            GRUPOCATEGORIATy = GRUPOCATEGORIAP.Read(Convert.ToInt32(linhaseparada[1]));
                            GRUPOCATEGORIATy.NOME = linhaseparada[2];
                            GRUPOCATEGORIAP.Save(GRUPOCATEGORIATy);
                        }
                        else
                        {
                            MsgErro = "GSA " + linhaseparada[1];
                            GRUPOCATEGORIATy.IDGRUPOCATEGORIA = -1;
                            GRUPOCATEGORIATy.NOME = linhaseparada[2];
                            GRUPOCATEGORIAP.Save(GRUPOCATEGORIATy);
                        }
                    }
                    else if (linhaseparada[0] == "MAR") //Salva Dados Marca
                    {
                        //Verifica se a Marca Existe no Banco de Dados                       
                        if (VerificaExisteMarca(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "MAR " + linhaseparada[1];
                            MARCATy = MARCAP.Read(Convert.ToInt32(linhaseparada[1]));
                            MARCATy.NOME = linhaseparada[2];
                            MARCAP.Save(MARCATy);
                        }
                        else
                        {
                            MsgErro = "MAR " + linhaseparada[1];
                            MARCATy.IDMARCA = -1;
                            MARCATy.NOME = linhaseparada[2];
                            MARCAP.Save(MARCATy);
                        }
                    }
                    else if (linhaseparada[0] == "COR") //Salva Dados COR
                    {
                        //Verifica se a COR Existe no Banco de Dados                       
                        if (VerificaExisteCor(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "COR " + linhaseparada[1];
                            CORTy = CORP.Read(Convert.ToInt32(linhaseparada[1]));
                            CORTy.NOME = linhaseparada[2];
                            CORP.Save(CORTy);
                        }
                        else
                        {
                            MsgErro = "COR " + linhaseparada[1];
                            CORTy.IDCOR = -1;
                            CORTy.NOME = linhaseparada[2];
                            CORP.Save(CORTy);
                        }
                    }
                    else if (linhaseparada[0] == "AMB") //Salva Dados Ambiente
                    {
                        //Verifica se o Ambiente Existe no Banco de Dados                       
                        if (VerificaExisteAmbiente(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "AMB " + linhaseparada[1];
                            AMBIENTETy = AMBIENTEP.Read(Convert.ToInt32(linhaseparada[1]));
                            AMBIENTETy.NOME = linhaseparada[2];
                            AMBIENTEP.Save(AMBIENTETy);
                        }
                        else
                        {
                            MsgErro = "AMB " + linhaseparada[1];
                            AMBIENTETy.IDAMBIENTE = -1;
                            AMBIENTETy.NOME = linhaseparada[2];
                            AMBIENTEP.Save(AMBIENTETy);
                        }
                    }
                    else if (linhaseparada[0] == "FUNC") //Salva Dados Funcionario
                    {
                        //Verifica se o Funcionario Existe no Banco de Dados                       
                        if (VerificaExisteFuncionario(Convert.ToInt32(linhaseparada[1])))
                        {
                            //Dados FUNCIONARIO 
                            MsgErro = "FUNC " + linhaseparada[1];
                            FUNCIONARIOTy = FUNCIONARIOP.Read(Convert.ToInt32(linhaseparada[1]));
                            FUNCIONARIOTy.NOME = linhaseparada[2];
                            FUNCIONARIOTy.ENDERECO = linhaseparada[3];
                            FUNCIONARIOTy.CIDADE = linhaseparada[4];
                            FUNCIONARIOTy.UF = linhaseparada[5];
                            FUNCIONARIOTy.CEP = linhaseparada[6];
                            FUNCIONARIOTy.BAIRRO = linhaseparada[7];
                            FUNCIONARIOTy.CPF = linhaseparada[8];
                            FUNCIONARIOTy.EMAIL = linhaseparada[9];
                            FUNCIONARIOTy.TELEFONE1 = linhaseparada[10];
                            FUNCIONARIOTy.TELEFONE2 = linhaseparada[11];
                            FUNCIONARIOTy.FUNCAO = linhaseparada[12];
                            FUNCIONARIOP.Save(FUNCIONARIOTy);
                        }
                        else
                        {
                            MsgErro = "FUNC " + linhaseparada[1];
                            FUNCIONARIOTy.IDFUNCIONARIO= -1;
                            FUNCIONARIOTy.NOME = linhaseparada[2];
                            FUNCIONARIOTy.ENDERECO = linhaseparada[3];
                            FUNCIONARIOTy.CIDADE = linhaseparada[4];
                            FUNCIONARIOTy.UF = linhaseparada[5];
                            FUNCIONARIOTy.CEP = linhaseparada[6];
                            FUNCIONARIOTy.BAIRRO = linhaseparada[7];
                            FUNCIONARIOTy.CPF = linhaseparada[8];
                            FUNCIONARIOTy.EMAIL = linhaseparada[9];
                            FUNCIONARIOTy.TELEFONE1 = linhaseparada[10];
                            FUNCIONARIOTy.TELEFONE2 = linhaseparada[11];
                            FUNCIONARIOTy.FUNCAO = linhaseparada[12];
                            FUNCIONARIOTy.CODSTATUS = 10;
                            FUNCIONARIOP.Save(FUNCIONARIOTy);
                        }
                    }
                    else if (linhaseparada[0] == "PRO") //Salva Dados Produto
                    {
                        //Verifica se a Produto Existe no Banco de Dados                       
                        if (VerificaExisteProduto(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "PRO " + linhaseparada[1];
                            PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(linhaseparada[1]));
                            PRODUTOSTy.NOMEPRODUTO = linhaseparada[2];
                            PRODUTOSTy.CODPRODUTOFORNECEDOR = linhaseparada[3];
                            PRODUTOSTy.VALORVENDA1 =Convert.ToDecimal(linhaseparada[4]);

                            if (Convert.ToInt32(linhaseparada[5]) > 0)
                                PRODUTOSTy.IDUNIDADE = Convert.ToInt32(linhaseparada[5]);

                            if (Convert.ToInt32(linhaseparada[6]) > 0)
                                PRODUTOSTy.IDGRUPOCATEGORIA = Convert.ToInt32(linhaseparada[6]);

                            if (Convert.ToInt32(linhaseparada[7]) > 0)
                                PRODUTOSTy.IDSTATUS = Convert.ToInt32(linhaseparada[7]);

                            if (Convert.ToInt32(linhaseparada[8]) > 0)
                                PRODUTOSTy.IDMARCA = Convert.ToInt32(linhaseparada[8]);

                            PRODUTOSTy.FLAGINATIVO = linhaseparada[9];

                            PRODUTOSP.Save(PRODUTOSTy);
                        }
                        else
                        {
                            MsgErro = "PRO " + linhaseparada[1];
                            PRODUTOSTy.IDPRODUTO = -1;
                            PRODUTOSTy.NOMEPRODUTO = linhaseparada[2];
                            PRODUTOSTy.CODPRODUTOFORNECEDOR = linhaseparada[3];
                            PRODUTOSTy.VALORVENDA1 =Convert.ToDecimal(linhaseparada[4]);

                            if (Convert.ToInt32(linhaseparada[5]) > 0)
                                PRODUTOSTy.IDUNIDADE =Convert.ToInt32(linhaseparada[5]);

                            if (Convert.ToInt32(linhaseparada[6]) > 0)
                                PRODUTOSTy.IDGRUPOCATEGORIA = Convert.ToInt32(linhaseparada[6]);

                            if (Convert.ToInt32(linhaseparada[7]) > 0)
                                PRODUTOSTy.IDSTATUS = Convert.ToInt32(linhaseparada[7]);

                            if (Convert.ToInt32(linhaseparada[8]) > 0)
                                PRODUTOSTy.IDMARCA = Convert.ToInt32(linhaseparada[8]);

                            PRODUTOSTy.FLAGINATIVO = linhaseparada[9];

                            PRODUTOSP.Save(PRODUTOSTy);
                        }
                    }
                    else if (linhaseparada[0] == "CLI") //Salva Dados de Cliente
                    {
                        //Verifica se o cleinte Existe no Banco de Dados
                      
                        if (VerificaExisteCliente(Convert.ToInt32(linhaseparada[1])))
                        {
                            MsgErro = "CLI " + linhaseparada[1];
                            CLIENTETy = CLIENTEP.Read(Convert.ToInt32(linhaseparada[1]));
                            CLIENTETy.NOME = linhaseparada[2];
                            CLIENTETy.TELEFONE1 = linhaseparada[3];
                            CLIENTETy.TELEFONE2 = linhaseparada[4];
                            CLIENTETy.CPF = linhaseparada[5];
                            CLIENTETy.CNPJ = linhaseparada[6];
                            CLIENTETy.IE = linhaseparada[7];
                            CLIENTETy.ENDERECO1 = linhaseparada[8];
                            CLIENTETy.NUMEROENDER = linhaseparada[9];
                            CLIENTETy.COMPLEMENTO1 = linhaseparada[10];
                            CLIENTETy.BAIRRO1 = linhaseparada[11];
                            CLIENTETy.CEP1 = linhaseparada[12];
                            CLIENTETy.COD_MUN_IBGE = Convert.ToInt32(linhaseparada[13]);
                            CLIENTETy.EMAILCLIENTE = linhaseparada[14];
                            CLIENTETy.FLAGBLOQUEADO = linhaseparada[15];
                            CLIENTEP.Save(CLIENTETy);
                        }
                        else
                        {
                            MsgErro = "CLI " + linhaseparada[1];
                            CLIENTETy.IDCLIENTE = -1;
                            CLIENTETy.NOME = linhaseparada[2];
                            CLIENTETy.TELEFONE1 = linhaseparada[3];
                            CLIENTETy.TELEFONE2 = linhaseparada[4];
                            CLIENTETy.CPF = linhaseparada[5];
                            CLIENTETy.CNPJ = linhaseparada[6];
                            CLIENTETy.IE = linhaseparada[7];
                            CLIENTETy.ENDERECO1 = linhaseparada[8];
                            CLIENTETy.NUMEROENDER = linhaseparada[9];
                            CLIENTETy.COMPLEMENTO1 = linhaseparada[10];
                            CLIENTETy.BAIRRO1 = linhaseparada[11];
                            CLIENTETy.CEP1 = linhaseparada[12];
                            CLIENTETy.COD_MUN_IBGE = Convert.ToInt32(linhaseparada[13]);
                            CLIENTETy.EMAILCLIENTE = linhaseparada[14];
                            CLIENTETy.FLAGBLOQUEADO = linhaseparada[15];
                            CLIENTEP.Save(CLIENTETy);
                        }
                    }

                }

                rd.Close();

            }
            catch (Exception ex)
            {
                rd.Close();
                MessageBox.Show("Erro ao Sicronizar o Banco de dados pelo arquivo scv");
                MessageBox.Show("Erro em: " + MsgErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean VerificaExisteStatus(int IDSTATUS)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", IDSTATUS.ToString()));
                STATUSCollection STATUSColl = new STATUSCollection();
                STATUSColl = STATUSP.ReadCollectionByParameter(RowRelatorio);

                if (STATUSColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se Status Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteUnidade(int IDUNIDADE)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDUNIDADE", "System.Int32", "=", IDUNIDADE.ToString()));
                UNIDADECollection UNIDADEColl = new UNIDADECollection();
                UNIDADEColl = UNIDADEP.ReadCollectionByParameter(RowRelatorio);

                if (UNIDADEColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se Unidade Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteMarca(int IDMARCA)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", IDMARCA.ToString()));
                MARCACollection MARCAColl = new MARCACollection();
                MARCAColl = MARCAP.ReadCollectionByParameter(RowRelatorio);

                if (MARCAColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se Marca Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteCor(int IDCOR)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCOR", "System.Int32", "=", IDCOR.ToString()));
                CORCollection CORColl = new CORCollection();
                CORColl = CORP.ReadCollectionByParameter(RowRelatorio);

                if (CORColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se COR Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteAmbiente(int IDAMBIENTE)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDAMBIENTE", "System.Int32", "=", IDAMBIENTE.ToString()));
                AMBIENTECollection AMBIENTEColl = new AMBIENTECollection();
                AMBIENTEColl = AMBIENTEP.ReadCollectionByParameter(RowRelatorio);

                if (AMBIENTEColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se AMBIENTE Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteFuncionario(int IDFUNCIONARIO)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", IDFUNCIONARIO.ToString()));
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(RowRelatorio);
                
                if (FUNCIONARIOColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se AMBIENTE Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteGrupoCategoria(int IDGRUPOCATEGORIA)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", IDGRUPOCATEGORIA.ToString()));
                GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
                GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(RowRelatorio);
                
                if (GRUPOCATEGORIAColl.Count > 0)
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se Grupo Categoria de Produtos Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteProduto(int IDPRODUTO)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
                PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOSColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar se Produto Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private Boolean VerificaExisteCliente(int IDCLIENTE)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                CLIENTECollection CLIENTEColl = new CLIENTECollection();               
                CLIENTEColl = CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                if (CLIENTEColl.Count > 0)
                    result = true;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Verificar so Cliente Existe");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }
    }
}
