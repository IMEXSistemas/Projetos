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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmEmailNFeCCe : Form
    {
        Utility Util = new Utility();  
        public string ChaveNFe = string.Empty;
        public string Email = string.Empty;
        public string ArquivoNFe = string.Empty;
        public FrmEmailNFeCCe()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEmailNFe_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                txtEmailDest.Text = Email;

                string Chave = ChaveNFe.Replace("N", "").Replace("F", "").Replace("e", "");
                string[] Lista_Arquivos = Directory.GetFiles(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe");

                if (Lista_Arquivos.Length > 0)
                {
                    foreach (string Arquivo in Lista_Arquivos)
                    {
                        int pos = Arquivo.IndexOf(Chave);
                        if (pos != -1)
                            txtPathAnexo.Text = Arquivo.ToString();
                    }
                }

                if (txtPathAnexo.Text.Trim() == string.Empty)
                    MessageBox.Show("Arquivo XML da carta de correção não encontrado!");

                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESTy = new EMPRESAEntity();
                EMPRESTy = EMPRESAP.Read(1);
                txtAssunto.Text = "Envio da Carta de Correção";
                txtMensagem.Text = "Prezado cliente, segue em anexo o xml da carta de correção. " + System.Environment.NewLine;
                txtMensagem.Text += System.Environment.NewLine;
                txtMensagem.Text += "===================================================================================" + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.NOMEFANTASIA + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.CIDADE + " " + EMPRESTy.UF + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.EMAIL + System.Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = BmsSoftware.ConfigSistema1.Default.PathInstall + "\nfe"; 
            openFileDialog1.Filter = "Arquivos de Imagem (*.xml)|*.xml"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtPathAnexo.Text = openFileDialog1.FileName.ToString();    
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacoes())
                {
                    lblStatus.Text = "Aguarde, enviando email...";
                    Application.DoEvents();

                    //nfec.nfecsharp nfe = new nfec.nfecsharp();
                    //if (
                    //nfe.EnviaEmail(
                    //    txtEmailDest.Text,
                    //    txtAssunto.Text,
                    //    txtMensagem.Text,
                    //    txtPathAnexo.Text) == true)
                    //    lblStatus.Text = "eMail enviado p/ " + txtEmailDest.Text;
                    //else
                    //    lblStatus.Text = "Falha no envio do email.";

                    ModeloEnvio2();

                    //MessageBox.Show(lblStatus.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        private void ModeloEnvio2()
        {
            try
            {
                this.Text = "Aguarde...";
                Application.DoEvents();


                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();

                //var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);

                var toAddress = new MailAddress(txtEmailDest.Text);
               // const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string subject = "Envio da Carta de Correção da Nota Fiscal Eletrônica - NFe";
                string body = txtMensagem.Text.Trim();

                body += System.Environment.NewLine.ToString();
                body += "--------------------------------------------------------------------------------------------------" + System.Environment.NewLine.ToString();
                body += EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += EMPRESATy.EMAIL + System.Environment.NewLine.ToString();

                bool segurancaemail = false;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail == "S")
                    segurancaemail = true;

                var smtp = new SmtpClient
                {
                 //   Host = "mail.imexsistema.com.br",
                    Host = "smtp.site.com.br",
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = segurancaemail,
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

                    message.Attachments.Add(new Attachment(txtPathAnexo.Text));

                    smtp.Send(message);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Email enviado com sucesso!");
                }


                this.Text = "Enviar Email";
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Text = "Enviar Email";
                Application.DoEvents();

                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível enviar o email!  pelo smtp.site.com.br",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro Técnico: " + ex.Message);
            }

        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (txtEmailDest.Text == string.Empty)
            {
                errorProvider1.SetError(txtEmailDest, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (txtPathAnexo.Text == string.Empty)
            {
                errorProvider1.SetError(txtPathAnexo, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = Application.StartupPath + @"\nfe\arquivos\CCe";
            openFileDialog1.Filter = "NF-e (*.xml)|";
            openFileDialog1.Title = "Arquivo de Carta de Correção";


            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string arquivocce = @"c:\temp\cartacorrecao.pdf";
                ////nfec.nfecsharp nfe = new nfec.nfecsharp();
                //string arquivocce = txtPathAnexo.Text + ".pdf";

            try
            {
                

                //if (!nfe.ImpCCe(txtPathAnexo.Text, arquivocce, 2))
                //{
                //    MessageBox.Show("Erro ao imprimir a Carta de Correção do arquivo selecionado");
                //}

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!nfe.ImpCCe(openFileDialog1.FileName, arquivocce,1))
                        MessageBox.Show("Erro ao imprimir a Carta de Correção do arquivo selecionado");
                }
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);

                //if (openFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    if (!nfe.ImpCCe(openFileDialog1.FileName, arquivocce, 2))
                //        MessageBox.Show("Erro ao imprimir a Carta de Correção do arquivo selecionado");
                //}
            }
        }
        
    }
}
