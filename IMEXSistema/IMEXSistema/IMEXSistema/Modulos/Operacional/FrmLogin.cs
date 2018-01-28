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
using System.Diagnostics;
using System.IO;
using winfit.Modulos.Operacional;


namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmLogin : Form
    {
        string DiretorioAtual = System.Environment.CurrentDirectory;

        Utility Util = new Utility();
        public static int _IdUsuario =  -1;
        public static int _IdNivel = -1;

        public FrmLogin()
        {
            InitializeComponent();
            //RegisterFocusEvents(this.Controls);
        }
       
        //private void RegisterFocusEvents(Control.ControlCollection controls)
        //{

        //    foreach (Control control in controls)
        //    {
        //        if ((control is TextBox) ||
        //          (control is RichTextBox) ||
        //          (control is ComboBox) ||
        //          (control is MaskedTextBox))
        //        {
        //            control.Enter += new EventHandler(controlFocus_Enter);
        //            control.Leave += new EventHandler(controlFocus_Leave);
        //        }

        //        RegisterFocusEvents(control.Controls);
        //    }
        //}

        //void controlFocus_Leave(object sender, EventArgs e)
        //{
        //    (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        //}

        //void controlFocus_Enter(object sender, EventArgs e)
        //{
        //    (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        //}

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                if (Validacoes() && VerificaLogin())
                {
                    this.Cursor = Cursors.Default;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgErroSenha, "Red");
                    txtNome.Focus();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                RestaurarConfig();
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNome.Text == string.Empty)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtSenha.Text == string.Empty)
            {
                errorProvider1.SetError(txtSenha, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }


        private bool VerificaLogin()
        {
            Boolean result = false;
            try
            {
                    SecurityString SecurityS = new SecurityString();
                    string SENHAUSUARIO = SecurityS.encrypt(txtSenha.Text);

                    USUARIOProvider USUARIOP = new USUARIOProvider();

                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    RowRelatorio.Add(new RowsFiltro("NOMEUSUARIO", "System.String", "=", txtNome.Text, "and"));
                    RowRelatorio.Add(new RowsFiltro("SENHAUSUARIO", "System.String", "=", SENHAUSUARIO, "and"));
                    RowRelatorio.Add(new RowsFiltro("FLAGATIVO", "System.String", "=", "S"));

                    USUARIOCollection USUARIOColl = new USUARIOCollection();
                    USUARIOColl = USUARIOP.ReadCollectionByParameter(RowRelatorio);

                    if (USUARIOColl.Count > 0)
                    {
                        _IdUsuario = USUARIOColl[0].IDUSUARIO;
                        _IdNivel = Convert.ToInt32(USUARIOColl[0].IDNIVELUSUARIO);
                        result = true;

                    }
                    else
                        result = false;

                    return result;
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possível acessar o Banco de Dados!",
                               "IMEX Sistemas",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro técnico: " + ex.Message);

                RestaurarConfig();

                result = false;

                return result;
            }
           
	    } 
       
        private void RestaurarConfig()
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S" && BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim() == "5")//5 Versão Gratuita
            {
                DialogResult dr = MessageBox.Show("Deseja restaurar para configuração padrão?",
               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        string LocalBancoDados = @"C:\IMEXSISTEMA\Bd\IMEXSISTEMAS.GDB";
                        string LocalImagens = @"C:\IMEXSISTEMA\Images";
                        string LocalInstall = @"C:\IMEXSISTEMA";


                        BmsSoftware.ConfigSistema1.Default.UrlBd = LocalBancoDados;
                        BmsSoftware.ConfigSistema1.Default.PathImage = LocalImagens;
                        BmsSoftware.ConfigSistema1.Default.PathInstall = LocalInstall;
                        BmsSoftware.ConfigSistema1.Default.Save();

                        BmsSoftware.ConfigSistema1.Default.Save();

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        MessageBox.Show("Após salvar as configurações é necessario reiniciar o programa!");
                        this.Close();

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
        }


        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false; 
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                 lbSistemaEmpresa.Text = BmsSoftware.ConfigSistema1.Default.NameSytem;
                 btnSair.Image = Util.GetAddressImage(21);

                linksite.Text = BmsSoftware.ConfigSistema1.Default.site;

                pBoxLoginLogo.Image = Util.GetAddressImage(12);

                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S" && BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim() == "5")//5 Versão Gratuita
                {
                    txtNome.Text = "adm";
                    txtSenha.Text = "adm";
                }

                AlteraImagemInicial();
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível exibir imagens, verifique  as configurações!",
                                "IMEX Sistemas",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                RestaurarConfig();
            }
        }

        private void AlteraImagemInicial()
        {
            //Imagem inicial
            if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png"))
            {
                byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png");
                MemoryStream stream = new MemoryStream(Logo);
                pictureBox1.Image = Image.FromStream(stream);
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

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(null, null);
        }

        private void btnCance_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmConfigMaquina FrmConfigMaquina = new FrmConfigMaquina())
            {
                FrmConfigMaquina.ShowDialog();
                this.Close();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http:" + linksite.Text + "/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new FrmSobre()).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new FrmComprarSuporte()).ShowDialog();
        }
    }
}
