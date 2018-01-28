using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using System.IO;
using System.Collections;
using FirebirdSql.Data.FirebirdClient;
using System.Drawing.Printing;
using BMSworks.UI.BMSworks.UI;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmConfigMaquina : Form
    {
        Utility Util = new Utility();

        public FrmConfigMaquina()
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
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }
        

        private void FrmConfigMaquina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }  
      

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                
                BmsSoftware.ConfigSistema1.Default.UrlBd = txtLocalBancoDados.Text;               
                BmsSoftware.ConfigSistema1.Default.PathImage = txtLocalImagens.Text;
                BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALLOCAL = txtPortaMatricial.Text;
                BmsSoftware.ConfigSistema1.Default.PORTAMATLOCAL = txtPortaRede.Text;
                BmsSoftware.ConfigSistema1.Default.CAMINHOMATREDE = txtPortaRede.Text;
                BmsSoftware.ConfigSistema1.Default.PathInstall = txtLocalInstall.Text;

                //Dados Ticket
                BmsSoftware.ConfigSistema1.Default.impressoraticket = cbImpressoraTicket.Text;
                BmsSoftware.ConfigSistema1.Default.msg1ticket = txtMsg1Ticket.Text;
                BmsSoftware.ConfigSistema1.Default.msg2ticket = txtMsg2Ticket.Text;
                BmsSoftware.ConfigSistema1.Default.msg3ticket = txtMsg3Ticket.Text;
                BmsSoftware.ConfigSistema1.Default.msg4ticket = txtMsg4Ticket.Text;

               // passwordBox Pas = new passwordBox();
               // string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

               // if (SenhaScript == "IMEX8777")
                //{
                    BmsSoftware.ConfigSistema1.Default.Save();
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                //}
               // else
               // {
                //    MessageBox.Show("Acesso Negado!");
               // }

                
            }
            catch (Exception)
            {
                
               MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void FrmConfigMaquina_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false; 
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                bntCadBanco.Image = Util.GetAddressImage(13);
                btImagens.Image = Util.GetAddressImage(13);
                btnInstall.Image = Util.GetAddressImage(13);               
                btnAdd.Image = Util.GetAddressImage(15);
                btnSair.Image = Util.GetAddressImage(21);

                txtPortaMatricial.Text = BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALLOCAL;
                txtPortaRede.Text = BmsSoftware.ConfigSistema1.Default.CAMINHOMATREDE;
                txtLocalBancoDados.Text = BmsSoftware.ConfigSistema1.Default.UrlBd;              
                
                txtLocalImagens.Text = BmsSoftware.ConfigSistema1.Default.PathImage;
                txtLocalInstall.Text = BmsSoftware.ConfigSistema1.Default.PathInstall;
                BmsSoftware.ConfigSistema1.Default.PORTAMATLOCAL = txtPortaRede.Text;
                BmsSoftware.ConfigSistema1.Default.CAMINHOMATREDE = txtPortaRede.Text;
           
                 foreach (String printer in PrinterSettings.InstalledPrinters)
                {
                    cbImpressoraTicket.Items.Add(printer.ToString());
                }

                cbImpressoraTicket.SelectedIndex = cbImpressoraTicket.FindString(BmsSoftware.ConfigSistema1.Default.impressoraticket);
                txtMsg1Ticket.Text = BmsSoftware.ConfigSistema1.Default.msg1ticket;
                txtMsg2Ticket.Text = BmsSoftware.ConfigSistema1.Default.msg2ticket;
                txtMsg3Ticket.Text = BmsSoftware.ConfigSistema1.Default.msg3ticket;
                txtMsg4Ticket.Text = BmsSoftware.ConfigSistema1.Default.msg4ticket;
               

            }
            catch (Exception)
            {
                
               MessageBox.Show("Não foi possível exibir imagens, verifique  as configurações!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }
     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntCadBanco_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Banco de Dados (*.gdb)|*.gdb"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtLocalBancoDados.Text = openFileDialog1.FileName.ToString();
        }

        private void txtLocalBancoDados_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = @"Para uso na rede, exemplo: nomecomputador:caminhobd\NOMEBD.GDB";
        }

        private void btImagens_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Selecione a pasta";
            openFileDialog2.CheckFileExists = false;

            openFileDialog2.FileName = "[Obter Pasta…]";
            openFileDialog2.Filter = "Folders|no.files";

            openFileDialog2.ShowDialog(); 
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(openFileDialog2.FileName);
            txtLocalImagens.Text = path;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Selecione a pasta";
            openFileDialog3.CheckFileExists = false;

            openFileDialog3.FileName = "[Obter Pasta…]";
            openFileDialog3.Filter = "Folders|no.files";

            openFileDialog3.ShowDialog(); 
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(openFileDialog3.FileName);
            txtLocalInstall.Text = path;
        }

        private void lkTestConexao_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + txtLocalBancoDados.Text;
                FbConnection connection = new FbConnection(connectionString);
                connection.Open();

                Util.ExibirMSg("Conexão efetuada com sucesso!", "Blue");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possível conectar ao Banco de Dados!",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void lklRestaurar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DialogResult dr = MessageBox.Show("Deseja realmente restaurar para configuração padrão?",
            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    txtLocalBancoDados.Text = Path.GetDirectoryName(Application.ExecutablePath) + @"\Bd\IMEXSISTEMAS.GDB";
                    txtLocalImagens.Text =  Path.GetDirectoryName(Application.ExecutablePath)+@"\Images";
                    txtLocalInstall.Text =  Path.GetDirectoryName(Application.ExecutablePath);                   

                    btnSalvar_Click(null, null);
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível Restaurar Configuração Padrão!",
                       ConfigSistema1.Default.NomeEmpresa,
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);

                }
            }
        }

        private void txtLocalInstall_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lklRestaurar_LocationChanged(object sender, EventArgs e)
        {

        }

        private void bdCadScript_Click(object sender, EventArgs e)
        {
            openFileDialog4.Filter = "Arquivos de Banco de Dados (*.gdb)|*.gdb"; // Filtra os tipos de arquivos desejados
            openFileDialog4.ShowDialog();
        }

        private void openFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

         
    }
}
