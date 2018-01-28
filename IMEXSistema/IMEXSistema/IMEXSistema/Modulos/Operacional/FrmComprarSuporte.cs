using BmsSoftware.Modulos.Financeiro;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmComprarSuporte : Form
    {
        Utility Util = new Utility();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();
        string TipoSuporte = "1";
        string ValorSuporte = "890.00";
        string VectoSuporte = string.Empty;

        public FrmComprarSuporte()
        {
            InitializeComponent();
        }

        private void brnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                //Salva Dados Empresa
                EMPRESATy = EMPRESAP.Read(1);
                EMPRESATy.NOMEFANTASIA = txtNome.Text;
                EMPRESATy.NOMECLIENTE = txtNome.Text;
                EMPRESATy.EMAIL = txtEmailCliente.Text;
                EMPRESATy.CNPJCPF=  maskedtxtCNPJ.Text;
                EMPRESATy.TELEFONE = txtTelefone1.Text;
                EMPRESAP.Save(EMPRESATy);

                EnviarEmail();
                GerarBoletoCaixa2();
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNome.Text.Trim().Length == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (maskedtxtCNPJ.Text.Trim() == "  .   .   /    -" != !ValidacoesLibrary.ValidaCNPJ(maskedtxtCNPJ.Text))
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label15, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (maskedtxtCNPJ.Text.Trim() == "18.183.803/0001-94")
            {
                string msg = "Coloque o CNPJ da Sua Empresa!";
                Util.ExibirMSg(msg, "Red");
                errorProvider1.SetError(label15, msg);
                result = false;
            }
            else if (txtEmailCliente.Text.Trim() == "contato@imexsistemas.com.br")
            {
                string msg = "Coloque o E-mail da Sua Empresa!";
                Util.ExibirMSg(msg, "Red");
                errorProvider1.SetError(label40, msg);
                result = false;
            }
            else if (txtTelefone1.Text.Trim() == "(31) 3892-7307" || txtTelefone1.Text.Trim() == "(31)3892-7307")
            {
                string msg = "Coloque o Telefone da Sua Empresa!";
                Util.ExibirMSg(msg, "Red");
                errorProvider1.SetError(label12, msg);
                result = false;
            }
            else if (txtEmailCliente.Text.Trim().Length == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label40, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (txtTelefone1.Text.Trim().Length == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label12, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");
           

            return result;
        }

        private void EnviarEmail()
        {
            try
            {

                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    EMPRESATy = EMPRESAP.Read(1);
            
                    //var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    
                    //var toAddress = new MailAddress("contato@imexsistema.com.br");
                    var toAddress = new MailAddress("contato@imexsistemas.com.br");
                  //  const string fromPassword = "rmr877701c#";
                    const string fromPassword = "Rmr877701c";

                   string valor = ValorSuporte;
                  
                    string subject = "Boleto de Renovação de Suporte";
                    string body = "Foi gerado o boleto no valor de: " + valor + " referente: suporte e consultoria da IMEX Sistemas" + System.Environment.NewLine.ToString();
                    body += System.Environment.NewLine.ToString();
                    
                   
                    body += "Prazo: 1x" + System.Environment.NewLine.ToString();
                   
                    body += "====================================================================" + System.Environment.NewLine.ToString();
                    body +="DADOS DO CLIENTE:" + System.Environment.NewLine.ToString();
                    body += "Nome: " + EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                    body += "Telefone: " + EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                    body += "Email: " + EMPRESATy.EMAIL + System.Environment.NewLine.ToString();
                    body += "CNPJ:" + EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
                    body += "====================================================================" + System.Environment.NewLine.ToString();
                    body += System.Environment.NewLine.ToString();
                    

                    var smtp = new SmtpClient
                    {
                       // Host = "mail.imexsistema.com.br",                        
                        Host = "smtp.site.com.br",                        
                        Port = 587,
                        EnableSsl = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
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
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao envial email de aviso de cobrança!");
                EnviarEmail2();
            }
        }

        private void EnviarEmail2()
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                EMPRESATy = EMPRESAP.Read(1);

                //var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);

                //var toAddress = new MailAddress("contato@imexsistema.com.br");
                var toAddress = new MailAddress("contato@imexsistemas.com.br");
                //const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string valor = ValorSuporte;
                string instrucoes2 = "";              

                string subject = "Boleto de Renovação de Suporte";
                string body = "Foi gerado o boleto no valor de: " + valor + " referente: " + instrucoes2 + System.Environment.NewLine.ToString();
                
                 body += "Prazo: 1x" + System.Environment.NewLine.ToString();

                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += "DADOS DO CLIENTE:" + System.Environment.NewLine.ToString();
                body += "Nome: " + EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += "Telefone: " + EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += "Email: " + EMPRESATy.EMAIL + System.Environment.NewLine.ToString();
                body += "CNPJ:" + EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();
                    

                Boolean UsoConexaoSegura = true;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "N")
                    UsoConexaoSegura = false;

                var smtp = new SmtpClient
                {
                    //Host = "mail.imexsistema.com.br",
                    Host = "smtp.site.com.br",
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
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao envial email de aviso de cobrança!");
            }
        }

        private void GerarBoletoCaixa2()
        {
            try
            {

                //Dados da Empresa Registro
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                //Dados para emitir boleto
                //  string data_vencimento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
                string data_vencimento = VectoSuporte;
                if (!ValidacoesLibrary.ValidaTipoDateTime(data_vencimento))
                    data_vencimento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto

                string nosso_numero = Util.RetiraLetras(EMPRESATy.CNPJCPF.Substring(0, 5) + DateTime.Now.ToString("yyyy/MM/dd"));   // Nosso Numero
                
                //==Os Campos Abaixo são Opcionais=================
                string instrucoes = "";//;//Instruçoes para o Cliente
                string instrucoes1 = "";// "CAIXA NAO RECEBER APOS O VENCIMENTO"; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
                string instrucoes2 = "APOS O VENCIMENTO MULTA DE R$ 5,90";
                string instrucoes3 = "E JUROS DE R$ 0,08 AO DIA";
                string instrucoes4 = "";

                string valor =  ValorSuporte.Replace(".", "").Replace(",",".");
                string numero_documento = nosso_numero;// Numero do Pedido ou Nosso Numero

                //===Dados do seu Cliente (Opcional)===============
                string CPFCNPJ = EMPRESATy.CNPJCPF;
                string sacado = EMPRESATy.NOMEFANTASIA;
                string endereco1 = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO;
                string endereco2 = EMPRESATy.CIDADE + " " + EMPRESATy.UF;

                // string arquivo = "https://www.boletobancario.com/boletofacil/integration/api/v1/issue-charge?token=D1D5DDDB110085CADEC57652A01F6FF3465B51FC2219775E2B9F2FE812CDF718&description=Renovação de suporte&amount=" + valor + "&payerName=" + sacado + "&payerCpfCnpj=" + CPFCNPJ + "&dueDate=" + data_vencimento;
                string arquivo = "https://www.boletobancario.com/boletofacil/integration/api/v1/issue-charge?token=3BCD01EBBE5A5ED816C48390E581FF1A3FF4F46D566B7609EE21440E5629F43B&description=Renovação de suporte&amount=" + valor + "&payerName=" + sacado + "&payerCpfCnpj=" + CPFCNPJ + "&dueDate=" + data_vencimento;

                string json = new System.Net.WebClient().DownloadString(arquivo);
                    string[] words = json.Split(',');
                    string link ="";
                    foreach (string word in words)
                    {
                        int Pos1 = word.IndexOf(":");
                        if (word.IndexOf("link") != -1)
                        {
                            link = word.Substring(Pos1 + 2);
                            link = link.Substring(0, link.Length - 1 ) ;
                            System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(link);
                        }
                    }

             }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar boleto!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private  void GerarBoletoCaixa_()
        {
            try
            {
            
           //Dados da Empresa Registro
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1); 

            //Dados para emitir boleto
          //  string data_vencimento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string data_vencimento = VectoSuporte;
            if(!ValidacoesLibrary.ValidaTipoDateTime(data_vencimento))
                data_vencimento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto

            string agencia = "0164";// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = ""; // 1 Digito da Agência
            string conta = "415192"; // 1 Digito da Agência
            string codcedente = "4151925";// CONFIGBOLETATy.CODCEDENTE.ToString(); // Numero do Convenio 
            string dac_conta = "5"; 	// Digito da Conta Corrente 1 Digito
            string nosso_numero = Util.RetiraLetras(EMPRESATy.CNPJCPF.Substring(0, 5) + DateTime.Now.ToString("yyyy/MM/dd")); 	// Nosso Numero
            string carteira = "24"; // Código da Carteira
            string data_documento =  DateTime.Now.ToString("dd/MM/yyyy");// Data de Vencimento do Boleto // Data de emissão do Boleto dd/MM/yyyy

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = "";//;//Instruçoes para o Cliente
            string instrucoes1 = "";// "CAIXA NAO RECEBER APOS O VENCIMENTO"; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = "APOS O VENCIMENTO MULTA DE R$ 5,90";
            string instrucoes3 = "E JUROS DE R$ 0,08 AO DIA";
            string instrucoes4 = "";

            string valor = ValorSuporte;

            valor = Convert.ToDecimal(valor).ToString("n2");

            string numero_documento = nosso_numero;// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = "18.183.803/0001-94";
            string cn_pj = Util.LimiterText(Util.RetiraLetras("18.183.803/0001-94"), 3);
            string endereco = "Rua Doutor Milton Bandeira, 346 Sala 303 - Centro";
            string cidade = "Viçosa / MG";
            string cedente = "IMEX Sistemas";

            //===Dados do seu Cliente (Opcional)===============

            string CPFCNPJ = EMPRESATy.CNPJCPF;

            string sacado =  EMPRESATy.NOMEFANTASIA + " - " + CPFCNPJ;
            string endereco1 = EMPRESATy.ENDERECO + " " +EMPRESATy.NUMERO;
            string endereco2 =EMPRESATy.CIDADE + " " +EMPRESATy.UF;
          

            string arquivo = "http://boletocefphp.imexsistema.com.br/boleto-caixa.php?data_vencimento=" + data_vencimento + "&agencia=" +
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar boleto!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void FrmComprarSuporte_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
              //  DownloadSegu3FTP();
              //  VerificaUso3();

                btnSair.Image = Util.GetAddressImage(21); 

                EMPRESATy = EMPRESAP.Read(1);
                txtNome.Text = EMPRESATy.NOMECLIENTE;
                txtEmailCliente.Text = EMPRESATy.EMAIL;
                maskedtxtCNPJ.Text = EMPRESATy.CNPJCPF;
                txtTelefone1.Text = EMPRESATy.TELEFONE;

                Lblvalor.Text = ValorSuporte;
                VerificaUso4();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
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

              //  FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://imexsist@192.185.170.200/www/arquivos/securitysoftware3.xml"));
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://ftp.gratisphphost.info/htdocs/registros/securitysoftware3.xml"));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                //reqFTP.Credentials = new NetworkCredential("imexsist", "rmr877701");
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

        private Boolean VerificaUso4()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                WebRequest req = WebRequest.Create("http://143.95.75.225/~imexsist/registros/securitysoftware3.xml");
                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string sCaminhoDoArquivo = reader.ReadToEnd();

                reader.Close();
                res.Close();
              
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                XmlDocument xmlDoc = new XmlDocument();
                // xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                //xmlDoc.LoadXml(xmlDoc.InnerXml);
                xmlDoc.LoadXml(sCaminhoDoArquivo);

                //Pegando elemento pelo nome da TAG
                XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                //Usando foreach para localizar o cnpj
                foreach (XmlNode xn in xnList)
                {
                    if (Util.RetiraLetras(xn["cnpjcpf"].InnerText.Trim()) == Util.RetiraLetras(EMPRESATy.CNPJCPF.Trim()))
                    {
                        ValorSuporte = xn["valorsuporte"].InnerText.Trim();
                        Lblvalor.Text = ValorSuporte;
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
                result = true;
                return result;

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
                        TipoSuporte = xn["tiposuporte"].InnerText.Trim();
                        ValorSuporte = xn["valorsuporte"].InnerText.Trim();
                        Lblvalor.Text = ValorSuporte;
                        VectoSuporte = xn["dtsuporte"].InnerText.Trim();
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
                result = true;
                return result;

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbPlanoSuporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
