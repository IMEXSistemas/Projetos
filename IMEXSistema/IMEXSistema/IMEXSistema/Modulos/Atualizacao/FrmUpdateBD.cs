using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using FirebirdSql.Data.FirebirdClient;
using System.Net;
using System.IO;
using System.Xml;

namespace BmsSoftware.Modulos.Atualizacao
{
    public partial class FrmUpdateBD : Form
    {
        Utility Util = new Utility();

        VERSAOCollection VERSAOBdOldColl = new VERSAOCollection();
        LIS_SCRIPTVERSAOCollection LIS_SCRIPTVERSAOColl = new LIS_SCRIPTVERSAOCollection();

        VERSAOProvider VERSAOP = new VERSAOProvider();
        LIS_SCRIPTVERSAO2Provider LIS_SCRIPTVERSAO2P = new LIS_SCRIPTVERSAO2Provider();
        SCRIPTVERSAOProvider SCRIPTVERSAOP = new SCRIPTVERSAOProvider();

        public Boolean habilitaChek = false;

        public FrmUpdateBD()
        {
            InitializeComponent();
        }

        private void FrmAtualizaBD_Load(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                chkAtulizaEXE.Visible = habilitaChek;
                chkAtualizaBD.Visible = habilitaChek;

                if (!VerificaUso3())
                {
                    this.Cursor = Cursors.Default;

                    MessageBox.Show("Entre em contato com a " + BmsSoftware.ConfigSistema1.Default.NomeEmpresa + " " + BmsSoftware.ConfigSistema1.Default.site,
                                  ConfigSistema1.Default.NomeEmpresa,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error,
                                  MessageBoxDefaultButton.Button1);


                    this.Close();
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean VerificaUso3()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DownloadSegu3FTP();
                string sCaminhoDoArquivo = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\securitysoftware3.xml";

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
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
                    result = false;
                    if (xn["cnpjcpf"].InnerText.Trim() == EMPRESATy.CNPJCPF.Trim())
                    {
                        if(Convert.ToDateTime(xn["dtsuporte"].InnerText.Trim()) > DateTime.Now)
                        {
                            result = true;
                            break;
                        }    
                        else
                            result = false;
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


        FileStream outputStream;
        FtpWebResponse response;
        Stream ftpStream;
        private void DownloadSegu3FTP()
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //string filepath = Path.GetDirectoryName(Application.ExecutablePath);
                string filepath = BmsSoftware.ConfigSistema1.Default.PathInstall;
                string fileName = "securitysoftware3.xml";
                outputStream = new FileStream(filepath + "\\" + fileName, FileMode.Create);

                //FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://imexsist@192.185.170.200/www/arquivos/securitysoftware3.xml"));
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://ftp.gratisphphost.info/htdocs/registrossecuritysoftware3.xml"));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
               // reqFTP.Credentials = new NetworkCredential("imexsist", "rmr877701");
                reqFTP.Credentials = new NetworkCredential("phpgr_19308995", "rmr87770");
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();


                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);

                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível localizar arquivo de atualização!");
                MessageBox.Show("Erro técnico: " + ex.Message);


                outputStream.Close();
                response.Close();
            }


        }

        private void DownloadIonicZip()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do Ionic.Zip ... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/" + "Ionic.Zip.dll", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\Ionic.Zip.dll");
                webClient.Dispose();

                lblMsg.Text = "Download do Ionic.Zip realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do Ionic.Zip!";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o downloand do Ionic.Zip!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadUpdate()
        {
            WebClient webClient = new WebClient();

            try
            {

               // if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\UpdateIMEX.exe"))
                //    File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\UpdateIMEX.exe");

                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do UpdateIMEX... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/" + "UpdateIMEX.exe", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\UpdateIMEX.exe");
                webClient.Dispose();

                lblMsg.Text = "Download do UpdateIMEX realizado com sucesso!";                
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do UpdateIMEX!";

                MessageBox.Show("Não foi possível fazer o download do UpdateIMEX!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadEXE()
        {
            WebClient webClient = new WebClient();

            try
            {
             

              //  if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\IMEXSistema.zip"))
                 //   File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\IMEXSistema.zip");

                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\IMEXSistema.exe"))
                    File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\IMEXSistema.exe");

                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do IMEXSistema.zip... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/" + "IMEXSistema.zip", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\IMEXSistema.zip");
                webClient.Dispose();

                lblMsg.Text = "Download do IMEXSistema.zip realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do arquivo IMEXSistema.zip!";

                MessageBox.Show("Não foi possível fazer o download do arquivo IMEXSistema.zip!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadArquivoAguarde()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do arquivo Processando1.png... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/Processando1.png", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\Processando1.png");
            
                webClient.Dispose();

                lblMsg.Text = "Download do Processando1.png realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do Processando1.png";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download do Processando1.png");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadArquivoNFec_Dll()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do arquivo NFec.dll... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/nfec.dll", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\nfec.dll");
                webClient.DownloadFile("http://arquivos.imexsistema.com.br/ass.dll", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\ass.dll");

                webClient.Dispose();

                lblMsg.Text = "Download do NFec.dll realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do NFec.dll";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download do NFec.dll");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadArquivo_SCHEMA()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download dos arquivos SCHEMAS... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/SCHEMANFE.zip", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\SCHEMANFE.zip");
             
                webClient.Dispose();

                lblMsg.Text = "Download dos arquivos SCHEMAS realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download dos arquivos SCHEMAS";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download dos arquivos SCHEMAS");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadArquivo_xtr()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download dos arquivos .xtr... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/CCeDtPkt.xtr", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\CCeDtPkt.xtr");
                webClient.DownloadFile("http://arquivos.imexsistema.com.br/NfeDtPkt-FS.xtr", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\NfeDtPkt-FS.xtr");
                webClient.DownloadFile("http://arquivos.imexsistema.com.br/NfeDtPkt.xtr", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\NfeDtPkt.xtr");

                webClient.Dispose();

                lblMsg.Text = "Download dos arquivos .xtr realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download dos arquivos .xtr";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download dos arquivos .xtr");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadArquivoNFedel_DLL()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do arquivo NFedel.dll... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                webClient.DownloadFile("http://arquivos.imexsistema.com.br/nfedel.dll", BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\nfedel.dll");

                webClient.Dispose();

                lblMsg.Text = "Download do NFedel.dll realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do NFedel.dll";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download do NFedel.dll");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DownloadBD2()
        {
            WebClient webClient = new WebClient();

            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Download do banco de dados SCRIPT... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();
                
                webClient.DownloadFile("http://arquivos.imexsistema.com.br/" + "SCRIPTBDIMEX.GDB", BmsSoftware.ConfigSistema1.Default.PathInstall+ @"\BD\SCRIPTBDIMEX.GDB");
                webClient.Dispose();

                lblMsg.Text = "Download do Banco de Dados de Script realizado com sucesso!";
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                webClient.Dispose();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível fazer o download do Banco de dados!";
                Application.DoEvents();

                MessageBox.Show("Não foi possível fazer o download do Banco de dados!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void bntCadBDAtual_Click(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        private void bntCadBDNovo_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizaBD_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                progressBar1.Maximum = 100;

                //Deleta o diretorio para limpar
               // if (Directory.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE"))
                //    Directory.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE", true);

                //Cria o diretorio para salvar os arquivos
                if (!Directory.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE"))
                    Directory.CreateDirectory(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE");

                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\SCHEMANFE.zip"))
                    File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\AtualizaEXE\SCHEMANFE.zip");

                //Baixa o arquivo Aguarde2.gif
                DownloadArquivoAguarde();

                if (chkAtualizaNFe.Checked)
                {
                    //Download do Arquivo NFec.dll
                    DownloadArquivoNFec_Dll();

                    //Download do Arquivo NFedel.dll
                    DownloadArquivoNFedel_DLL();

                    //Download dos Arquivo extensao .xtr
                    DownloadArquivo_xtr();

                    //Download dos Arquivos SCHEMA
                    DownloadArquivo_SCHEMA();
                }
                    
                progressBar1.Step = 15;
                progressBar1.PerformStep();

                if (chkAtualizaBD.Checked)
                {
                    //Download o BD Script
                    DownloadBD2();

                    progressBar1.Step = 15;
                    progressBar1.PerformStep();

                    //Executa o scipt
                    AtualizaBancoDados();

                    progressBar1.Step = 15;
                    progressBar1.PerformStep();
                }

                if (chkAtulizaEXE.Checked)
                {
                    DownloadEXE();

                    progressBar1.Step = 15;
                    progressBar1.PerformStep();

                    //Dowload do arquivo Ionic.Zip.dll
                    DownloadIonicZip();

                    progressBar1.Step = 15;
                    progressBar1.PerformStep();

                    //Dowloado do Arquivo Update.exe
                    DownloadUpdate();

                    progressBar1.Step = 25;
                    progressBar1.PerformStep();

                    //Abre o arquivo UpateIMEX.exe
                    string PastaOrigem = ConfigSistema1.Default.PathInstall;
                    System.Diagnostics.Process Processo1 = null;
                    if (File.Exists(PastaOrigem + @"\UpdateIMEX.exe"))
                        Processo1 = System.Diagnostics.Process.Start(PastaOrigem + @"\UpdateIMEX.exe");

                    //Aguarda 5 segundos
                    System.Threading.Thread.Sleep(5 * 1000);

                    //Fecha o Sistema IMEXSistema.exe
                    Application.Exit();
                }  
                

                this.Cursor = Cursors.Default;
                MessageBox.Show("Sistema atualizado com sucesso!",
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information,
                                 MessageBoxDefaultButton.Button1);

                lblMsg.Text = "Sistema atualizado com sucesso!";
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível executar os script no Banco de dados!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Não foi possível executar os script no Banco de dados!";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
               
            }

            this.Cursor = Cursors.Default;
        }      

        int _IDVERSAO = -1;
        private void AtualizaBancoDados()
        {
            try
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Executando os script no Banco de Dados... Aguarde";
                lblMsg.ForeColor = System.Drawing.Color.Blue;

                //Busca a ultima versão do banco de dados atual
                VERSAOBdOldColl = VERSAOP.ReadCollectionByParameter(null, "NUMEROVERSAO");
                string NUMEROVERSAOATUAL = VERSAOBdOldColl[VERSAOBdOldColl.Count - 1].NUMEROVERSAO;

                //Filtra os script do banco de dados novos
                //que serão executados no banco de dados Atual
                string  BDScript =  BmsSoftware.ConfigSistema1.Default.BDScript.Trim() == string.Empty ? @"C:\IMEXSISTEMA\BD\SCRIPTBDIMEX.GDB" : BmsSoftware.ConfigSistema1.Default.BDScript.Trim();
                string connectionString = "User=SYSDBA;Password=masterkey;DataSource=localhost;Database=" +BDScript;
                                        

                RowsFiltroCollection RowBDAntigo = new RowsFiltroCollection();
                RowBDAntigo.Add(new RowsFiltro("NUMEROVERSAO", "System.String", ">", NUMEROVERSAOATUAL.ToString()));
                LIS_SCRIPTVERSAOColl = LIS_SCRIPTVERSAO2P.ReadCollectionByParameter(RowBDAntigo, "IDSCRIPT", connectionString);

                VERSAO2Provider VERSAO2P = new VERSAO2Provider();

                foreach (var item in LIS_SCRIPTVERSAOColl)
                {
                    string NUMEROVERSAO = item.NUMEROVERSAO;
                   
                    //Verifica se ja existe uma id para a versao
                    _IDVERSAO = RetornaIdVersao(NUMEROVERSAO);

                    if (_IDVERSAO == -1 )
                        _IDVERSAO = VERSAOP.Save(-1, NUMEROVERSAO);
    
                    try
                    {
                        if(ComandoScript2(item.DESCRICAO))
                            SCRIPTVERSAOP.Save(-1, _IDVERSAO, item.DESCRICAO, "S");
                        else
                            SCRIPTVERSAOP.Save(-1, _IDVERSAO, item.DESCRICAO, "N");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Erro ao executar o script da versão: " + NUMEROVERSAO,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);

                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }


                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Script executado com sucesso!";
                lblMsg.ForeColor = System.Drawing.Color.Blue;

            }
            catch (Exception ex)
            {

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Erro ao executar o script";
                lblMsg.ForeColor = System.Drawing.Color.Blue;

                MessageBox.Show("Erro ao executar o script!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information,
                             MessageBoxDefaultButton.Button1);


                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public int RetornaIdVersao(string NUMEROVERSAO)
        {
            int result = -1;

            try
            {
                RowsFiltroCollection RowCollection = new RowsFiltroCollection();
                RowCollection.Add(new RowsFiltro("NUMEROVERSAO", "System.String", "=", NUMEROVERSAO));
                LIS_SCRIPTVERSAOCollection LIS_SCRIPTVERSAOColl2 = new LIS_SCRIPTVERSAOCollection();
                LIS_SCRIPTVERSAOProvider LIS_SCRIPTVERSAOP = new LIS_SCRIPTVERSAOProvider();
                LIS_SCRIPTVERSAOColl2 = LIS_SCRIPTVERSAOP.ReadCollectionByParameter(RowCollection);

                if (LIS_SCRIPTVERSAOColl2.Count > 0)
                    result =Convert.ToInt32(LIS_SCRIPTVERSAOColl2[0].IDVERSAO);

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public Boolean ComandoScript2(string SQL)
        {
             string connString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
             FbConnection connection = new FbConnection(connString);

             connection.Open();
             FbTransaction transaction = connection.BeginTransaction();

            try
            {
                FbCommand command = new FbCommand(SQL, connection, transaction);
                command.CommandType = CommandType.Text;

                command.ExecuteScalar();

                transaction.Commit();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                transaction.Dispose();
                connection.Close();

                MessageBox.Show("Erro ao executar o comando: " + SQL,
                              BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro técnico: " + ex.Message,
                              BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                return false;
               
            }

        }

      
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtLocalBDNovo_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }  

    }
}
