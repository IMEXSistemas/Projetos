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
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmLoginTela : Form
    {
        Utility Util = new Utility();
        public Boolean Retorno = false;
        public string NomeFormulario;
        public int IDNIVEL;

        public FrmLoginTela()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLoginTela_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            pBoxLoginLogo.Image = Util.GetAddressImage(12);           
        }

        /// <summary>
        /// Verifica a liberação de Apaga de registro
        /// </summary>
        public Boolean Apaga_RegistroSenha(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGAPAGA == "S")
                    result = true;
                else
                    result = false;                   
            }

            return result;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                if (Validacoes() && VerificaLogin())
                {
                    // this.Cursor = Cursors.Default;
                    // this.DialogResult = DialogResult.OK;
                    this.Close();
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
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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
                    Retorno = Apaga_RegistroSenha(NomeFormulario, Convert.ToInt32(USUARIOColl[0].IDNIVELUSUARIO));
                    result = true;
                }
                else
                {
                    Retorno = false;
                    result = false;
                }

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
                result = false;

                return result;
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

        private void FrmLoginTela_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
