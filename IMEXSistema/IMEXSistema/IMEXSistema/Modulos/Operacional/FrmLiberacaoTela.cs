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


namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmLiberacaoTela : Form
    {
        string DiretorioAtual = System.Environment.CurrentDirectory;

        Utility Util = new Utility();
        public Boolean AcessoLiberado = false;
        public int _idusuario = -1;

        public FrmLiberacaoTela()
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
                    AcessoLiberado = true;
                    this.Close();
                }
                else
                {
                    AcessoLiberado = false;
                    MessageBox.Show(ConfigMessage.Default.MsgErroSenha,
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);

                    txtNome.Focus();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
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
                        result = true;
                        _idusuario = USUARIOColl[0].IDUSUARIO;
                    }
                    else
                        result = false;

                    return result;
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Não foi possível acessar o Banco de Dados!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro técnico: " + ex.Message);

                result = false;

                return result;                
            }
           
	    }        

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false; 
                this.FormBorderStyle = FormBorderStyle.FixedDialog;                

                pBoxLoginLogo.Image = Util.GetAddressImage(12);             

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
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string PastaOrigem = ConfigSistema1.Default.PathInstall;
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PastaOrigem + @"\AcessoBMS.exe");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
