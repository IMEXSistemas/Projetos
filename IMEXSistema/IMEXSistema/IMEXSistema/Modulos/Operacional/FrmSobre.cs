using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BMSworks.Firebird;
using BMSworks.Model;
using System.IO;
using BMSworks.UI;

namespace winfit.Modulos.Operacional
{
    public partial class FrmSobre : Form
    {
        Utility Util = new Utility(); 
        public FrmSobre()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSobre_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnSair.Image = Util.GetAddressImage(21);

           // lblanoDireito.Text = DateTime.Now.ToString("yyyy");
            //lblanoDireito.Text = "09/2015";

            //Imagem inicial
            //if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logo bms - sem fundo.png"))
            //{
            //    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logo bms - sem fundo.png");
            //    MemoryStream stream = new MemoryStream(Logo);
            //    pictureBox1.Image = Image.FromStream(stream);
            //}           

            // lblNome.Text = BmsSoftware.ConfigSistema1.Default.NomeEmpresa;
               // lblemail.Text = BmsSoftware.ConfigSistema1.Default.email;
               // lnksite.Text = BmsSoftware.ConfigSistema1.Default.site;

            Versao();
        }

        public void Versao()
        {

            try
            {
                VERSAOCollection VERSAOBdOldColl = new VERSAOCollection();
                VERSAOProvider VERSAOP = new VERSAOProvider();
                VERSAOBdOldColl = VERSAOP.ReadCollectionByParameter(null, "NUMEROVERSAO desc");
                if (VERSAOBdOldColl.Count > 0)
                {
//                    label3.Text = "Versão: " + VERSAOBdOldColl[0].NUMEROVERSAO;
                    //label3.Text = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(label3.Text));
                   // label3.Text = label3.Text.Replace("/",".");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://www.imexsistemas.com.br");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void txtNomeRevenda_Leave(object sender, EventArgs e)
        {
           

        }
    }
}
