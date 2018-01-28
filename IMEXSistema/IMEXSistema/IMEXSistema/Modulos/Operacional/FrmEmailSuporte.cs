using BmsSoftware;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using BMSSoftware.Modulos.Cadastros;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace winfit.Modulos.Outros
{
    public partial class FrmEmailSuporte : Form
    {
        Utility Util = new Utility();  
        CLIENTEEntity CLIENTETy = new CLIENTEEntity();
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();

        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();

        FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();

        MENSAGEMProvider MENSAGEMP = new MENSAGEMProvider();

        public LIS_CLIENTECollection LIS_ClienteEmailLoteColl = new LIS_CLIENTECollection();

        public int _IDCLIENTE = -1;
        public string EmailSelecionado = string.Empty;
        public bool EmailLote = false;
        int ContadorArquivo = 0;
        public string ArquivoAnexo = string.Empty;
        public string NomeVendedor = string.Empty;

        public FrmEmailSuporte()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
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
            (sender as Control).BackColor = BmsSoftware.ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = BmsSoftware.ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmContato_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                txtPorta.Text = BmsSoftware.ConfigSistema1.Default.PortaEmail;

                btnSair.Image = Util.GetAddressImage(21);
                
                chkConSegSSL.Checked = false;
                if(BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "S")
                    chkConSegSSL.Checked = true;

                GetDadosEmpresa();               

             
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }    
       

        private void GetDadosEmpresa()
        {
            try
            {
                EMPRESATy = EMPRESAP.Read(1);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void FrmContato_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtmensagem")
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            
        }
       

        private Boolean ValidacoesEmail()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (txtAssuntoEmail.Text.TrimEnd() == string.Empty)
            {
                errorProvider1.SetError(txtAssuntoEmail, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtMensagem.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtMensagem, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtParaEmail.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtParaEmail, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }  
            else
                errorProvider1.Clear();


            return result;
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            
        }

       

        private void cbMensagem_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnAnexoArquivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidacoesEmail())
            {
                try
                {
                    this.Text = "Aguarde...";
                    Application.DoEvents();

                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    //Salva Configuração do E-mail
                    BmsSoftware.ConfigSistema1.Default.PortaEmail = txtPorta.Text;
                    if (chkConSegSSL.Checked == true)
                        BmsSoftware.ConfigSistema1.Default.SegurancaEmail = "S";
                    else
                        BmsSoftware.ConfigSistema1.Default.SegurancaEmail = "N";

                    BmsSoftware.ConfigSistema1.Default.Save();    

                    //var fromAddress = new MailAddress("imexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                   // var fromAddress = new MailAddress("suporteimexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                   // var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    
                    var toAddress = new MailAddress(txtParaEmail.Text);
                    //const string fromPassword = "rmr877701c#";
                    const string fromPassword = "Rmr877701c";
                  
                    string subject = txtAssuntoEmail.Text;
                    string body = txtMensagem.Text.Trim();
                    
                    body += System.Environment.NewLine.ToString();
                    body += "--------------------------------------------------------------------------------------------------" + System.Environment.NewLine.ToString();
                    body += EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                    body += EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                    body += EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
                    body += EMPRESATy.EMAIL + System.Environment.NewLine.ToString();

                    var smtp = new SmtpClient
                    {
                      //  Host = "mail.imexsistema.com.br",                        
                        Host = "smtp.site.com.br",                        
                        Port = Convert.ToInt32(txtPorta.Text),
                        EnableSsl = chkConSegSSL.Checked,
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
                        MessageBox.Show("Email enviado com sucesso!");
                    }


                    this.Text = "Email de Suporte";
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    this.Text = "Email de Suporte";
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
        }

        private void DGAnexo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FrmCliente frm = new FrmCliente())
            {
                frm.CodClienteSelec = _IDCLIENTE;
                frm.ShowDialog();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtAssuntoEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
