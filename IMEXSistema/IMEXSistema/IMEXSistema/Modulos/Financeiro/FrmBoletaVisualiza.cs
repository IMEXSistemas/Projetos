using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBoletaVisualiza : Form
    {
        Utility Util = new Utility();  
        public Boolean BoletaPHP = false;
        public string ArquivoPHP = string.Empty;
        public FrmBoletaVisualiza()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBoletaVisualizaTeste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmBoletaVisualizaTeste_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnSair.Image = Util.GetAddressImage(21);
            btnImprimir.Image = Util.GetAddressImage(19);

            if(!BoletaPHP)
            { 
                string PathRegistro = ConfigSistema1.Default.PathInstall;

                //abrir o arquivo selecionado caminho e nome do arquivo
                PathRegistro = PathRegistro.ToLower();
                string arquivo = PathRegistro + @"\boletobancaria.html";
                // vamos verificar se este arquivo existe
                if (File.Exists(arquivo))
                {
                    string url = arquivo;
                    webBrowser1.Navigate(new Uri(url));
                    webBrowser1.Refresh();
                }
            }
            else
            {
                string url = ArquivoPHP;
                webBrowser1.Navigate(new Uri(url));
                webBrowser1.Refresh();
            }

            this.Cursor = Cursors.Default;	
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPageSetupDialog();
            webBrowser1.ShowPrintPreviewDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ArquivoBoleto = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\boletobancaria.html";
                if (BoletaPHP)
                    ArquivoBoleto = ArquivoPHP;

                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(ArquivoBoleto);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
               
        }
    }
}
