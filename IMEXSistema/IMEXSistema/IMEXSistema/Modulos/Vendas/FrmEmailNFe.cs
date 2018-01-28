using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
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
    public partial class FrmEmailNFe : Form
    {
        Utility Util = new Utility();  
        MENSAGEMProvider MENSAGEMNP = new MENSAGEMProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        EMPRESAEntity EMPRESTy = new EMPRESAEntity();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        public string ChaveNFe = string.Empty;
        public string Email = string.Empty;
        public string ArquivoNFe = string.Empty;
        public FrmEmailNFe()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEmailNFe_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtEmailDest.Text = Email;
            txtPathAnexo.Text = ArquivoNFe;

            GetDropMensagem();

            //Arquivo PDF
            string NFe = ChaveNFe.Replace("N", "").Replace("F", "").Replace("e", "");
            List<string> dirs = FileHelper.GetFilesRecursive(ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\procNFe");
            foreach (string p in dirs)
            {
                int LengthLine = p.Length;
                int pos = p.IndexOf(NFe);
                if (pos != -1)
                {
                    string pathPDF = ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\pdf\" + NFe + ".pdf";                 

                    if (File.Exists(pathPDF))
                    {
                        txtArquivoPDF.Text = pathPDF;
                    }
                    else
                    {
                       MessageBox.Show("Arquivo: " + pathPDF + " não localizado!");
                       
                       nfec.nfecsharp nfe = new nfec.nfecsharp();
                       if(nfe.NFeDanfe(p, pathPDF, 2, false))
                       {
                           txtArquivoPDF.Text = pathPDF;
                           MessageBox.Show("Arquivo: " + pathPDF + " criado com sucesso!");                           
                       }
                    }
                    
                }
            }


            EMPRESTy = EMPRESAP.Read(1);
            txtMensagem.Text += "Prezado cliente, a Nota Fiscal Eletrônica (NF-e) foi gerada com sucesso!" + System.Environment.NewLine;
            txtMensagem.Text += "você receberá uma representação simplificada da Nota Fiscal Eletrônica chamada DANFE " + System.Environment.NewLine;
            txtMensagem.Text += "(Documento Auxiliar da Nota Fiscal Eletrônica) e o XML da mesma " + System.Environment.NewLine;
            txtMensagem.Text += " " + System.Environment.NewLine;
            txtMensagem.Text += "O DANFE em papel pode ser arquivado para apresentação ao fisco quando solicitado. Contudo, caso sua compra tenha sido efetuada em " + System.Environment.NewLine;
            txtMensagem.Text += "nome de Pessoa Jurídica e sua empresa também for emitente de NF-e, o arquivamento eletrônico do XML de seus fornecedores é obrigatório," + System.Environment.NewLine;
            txtMensagem.Text += " " + System.Environment.NewLine;
            txtMensagem.Text += "Assim, você recebe também em anexo o arquivo XML da Nota Fiscal Eletrônica. Este arquivo deve ser armazenado eletronicamente por sua empresa " + System.Environment.NewLine;
            txtMensagem.Text += "pelo prazo de 05 (cinco) anos, conforme previsto na legislação tributária (Art. 173 do Código Tributário Nacional e § 4º da Lei 5.172 de 25/10/1966)." + System.Environment.NewLine;
            txtMensagem.Text += " " + System.Environment.NewLine;
            txtMensagem.Text += "sendo passível de fiscalização." + System.Environment.NewLine;
            txtMensagem.Text += " " + System.Environment.NewLine;
            txtMensagem.Text += "===========================================================" + System.Environment.NewLine;
            txtMensagem.Text += EMPRESTy.NOMEFANTASIA + System.Environment.NewLine;
            txtMensagem.Text += EMPRESTy.CIDADE + "/" + EMPRESTy.UF + System.Environment.NewLine;
            txtMensagem.Text += EMPRESTy.EMAIL + System.Environment.NewLine; 
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
             try 
	        {	        
		         openFileDialog1.InitialDirectory = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\procNFe"; 
                 openFileDialog1.Filter = "Arquivos de Imagem (*.xml)|*.xml"; // Filtra os tipos de arquivos desejados
                 openFileDialog1.ShowDialog();
	        }
	        catch (Exception ex)
	        {
		        MessageBox.Show("Erro técnico: "+ ex.Message);
	        }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtPathAnexo.Text = openFileDialog1.FileName.ToString();    
            }
            catch (Exception ex)
	        {
		        MessageBox.Show("Erro técnico: "+ ex.Message);
	        }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacoes())
                {
                   // if (CONFISISTEMAP.Read(1).SMTP.Trim() != string.Empty)
                      //  ModeloASSComp();
                    //else
                        ModeloEnvio2();
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

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);


                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESATy = EMPRESAP.Read(1);

                  var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                //var fromAddress = new MailAddress(EMPRESATy.EMAIL, EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);


                var toAddress = new MailAddress(txtEmailDest.Text);
                //const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string subject ="Envio da Nota Fiscal Eletrônica - NFe";
                string body = txtMensagem.Text.Trim();

                body += System.Environment.NewLine.ToString();
                body += "--------------------------------------------------------------------------------------------------" + System.Environment.NewLine.ToString();
                body += EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += EMPRESATy.EMAIL + System.Environment.NewLine.ToString();

                bool segurancaemail = false;
                if(BmsSoftware.ConfigSistema1.Default.SegurancaEmail == "S")
                    segurancaemail = true;

                var smtp = new SmtpClient
                {
                    //Host = "mail.imexsistema.com.br",
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
                    message.Attachments.Add(new Attachment(txtArquivoPDF.Text));
                   

                    smtp.Send(message);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Email enviado com sucesso!");
                }


                this.Text = "Enviar Email";
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Text = "Enviar Email pelo mail.imexsistema.com.br";
                Application.DoEvents();

                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível enviar o email!",
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
            
        }

        private void ModeloASSComp()
        {
            try
            {
                lblStatus.Text = "Aguarde, enviando email...";
                Application.DoEvents();

                string ArquivoAnexo = txtPathAnexo.Text;
                if (txtArquivoPDF.Text.Trim() != string.Empty)
                    ArquivoAnexo = txtPathAnexo.Text + ";" + txtArquivoPDF.Text;

                nfec.nfecsharp nfe = new nfec.nfecsharp();

               // if(nfe.EnviaEmail( txtEmailDest.Text, txtAssunto.Text, txtMensagem.Text, ArquivoAnexo))
               // {
                   // lblStatus.Text = "Emaill enviado : " + txtEmailDest.Text + " com sucesso!";
               // }
              //  else
              //  {
                //    lblStatus.Text = "ERRO ao enviar o Email :" + txtEmailDest.Text;
                 //   MessageBox.Show(lblStatus.Text);
               // }
                   
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Falha no envio do email.";
                MessageBox.Show("Erro técnico: "  + ex.Message);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try 
	        {	        
		         openFileDialog2.InitialDirectory = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\pdf";
                 openFileDialog2.Filter = "Arquivos de Imagem (*.PDF)|*.PDF"; // Filtra os tipos de arquivos desejados
                 openFileDialog2.ShowDialog();
	        }
	        catch (Exception ex)
	        {
		        MessageBox.Show("Erro técnico: "+ ex.Message);
	        }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
               txtArquivoPDF.Text = openFileDialog2.FileName.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnCadMensagem_Click(object sender, EventArgs e)
        {
            using (FrmMensagem frm = new FrmMensagem())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbMensagem.SelectedValue);
                GetDropMensagem();

                cbMensagem.SelectedValue = CodSelec;
            }
        }


        private void GetDropMensagem()
        {
            MENSAGEMCollection MENSAGEMColl = new MENSAGEMCollection();
            MENSAGEMColl = MENSAGEMNP.ReadCollectionByParameter(null);

            cbMensagem.DisplayMember = "NOME";
            cbMensagem.ValueMember = "IDMENSAGEM";

            MENSAGEMEntity MENSAGEMNFETy = new MENSAGEMEntity();
            MENSAGEMNFETy.NOME = ConfigMessage.Default.MsgDrop;
            MENSAGEMNFETy.IDMENSAGEM = -1;
            MENSAGEMColl.Add(MENSAGEMNFETy);

            Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity>(cbMensagem.DisplayMember);

            MENSAGEMColl.Sort(comparer.Comparer);
            cbMensagem.DataSource = MENSAGEMColl;

            // cbMensagem.SelectedIndex = 0;
        }

        private void cbMensagem_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMensagem.SelectedValue) > 0)
            {
                txtMensagem.Text = "";
                txtMensagem.Text += " " + MENSAGEMNP.Read(Convert.ToInt32(cbMensagem.SelectedValue)).MENSAGEM;
                txtMensagem.Text += System.Environment.NewLine;
                txtMensagem.Text += "=====================================================================" + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.NOMEFANTASIA + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.CIDADE + "/" + EMPRESTy.UF + System.Environment.NewLine;
                txtMensagem.Text += EMPRESTy.EMAIL + System.Environment.NewLine; 
            }
        }
        
    }
}
