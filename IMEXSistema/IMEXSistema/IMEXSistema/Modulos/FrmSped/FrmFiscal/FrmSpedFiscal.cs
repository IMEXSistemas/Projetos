using BMSSoftware.Modulos.Cadastros;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using SpedService.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.FrmSped.FrmFiscal
{
    public partial class FrmSpedFiscal : Form
    {

        Utility Util = new Utility();   
       
       

        public FrmSpedFiscal()
        {
            InitializeComponent();
        }

        private void FrmSpedFiscal_Load(object sender, EventArgs e)
        {
            try
            {
                btnSair.Image = Util.GetAddressImage(21);

                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                cbVersaoLayout.SelectedIndex = 0;
                cbVersaoLayout.DropDownStyle = ComboBoxStyle.DropDownList;
                cbFinalidadeArquivo.SelectedIndex = 0;
                cbFinalidadeArquivo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbPerfilApreArquivo.SelectedIndex = 1;
                cbPerfilApreArquivo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbVersaoLayout.SelectedIndex = 8;
                cbVersaoLayout.DropDownStyle = ComboBoxStyle.DropDownList;
                cbInventario.SelectedIndex = 0;
                btnCadContador.Image = Util.GetAddressImage(6);

                GetDropContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropContador()
        {
            try
            {
                CONTADORProvider CONTADORP = new CONTADORProvider();
                CONTADORCollection CONTADORColl = new CONTADORCollection();
                CONTADORColl = CONTADORP.ReadCollectionByParameter(null, "NOME");

                cbContador.DisplayMember = "NOME";
                cbContador.ValueMember = "CONTADORID";

                CONTADOREntity CONTADORTy = new CONTADOREntity();
                CONTADORTy.NOME = ConfigMessage.Default.MsgDrop;
                CONTADORTy.CONTADORID = -1;
                CONTADORColl.Add(CONTADORTy);

                Phydeaux.Utilities.DynamicComparer<CONTADOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTADOREntity>(cbContador.DisplayMember);

                CONTADORColl.Sort(comparer.Comparer);
                cbContador.DataSource = CONTADORColl;              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public ArquivoDTO documentoSelected;
        public static string CaminhoArquivo;
        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacoes())
                {
                    this.Text = "Aguarde... Gerando Arquivo!";
                    lblObervacao.Text = "Aguarde... Gerando Arquivo!";
                    Application.DoEvents();

                    SpedFiscalDAL Sp = new SpedFiscalDAL();
                    Sp.errolog = false;
                    Sp.GerarArquivoSpedFiscal(dtpkInicial.Text, dtpkFim.Text, cbVersaoLayout.SelectedIndex, cbFinalidadeArquivo.SelectedIndex, cbPerfilApreArquivo.SelectedIndex,
                       1, cbInventario.SelectedIndex, Convert.ToInt32(cbContador.SelectedValue.ToString()));

                   // CaminhoArquivo = salvaArquivoTempLocal(documentoSelected);

                    this.Text = "Sped Fiscal";
                    lblObervacao.Text = string.Empty;
                    Application.DoEvents();

                    if (Sp.errolog)
                    {
                        if(File.Exists(Sp.pathLog))
                        {
                            System.Diagnostics.Process.Start(Sp.pathLog);
                            this.Close();
                        }
                    }
                    else if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall+@"\SpedFiscal.txt"))
                        System.Diagnostics.Process.Start(BmsSoftware.ConfigSistema1.Default.PathInstall+@"\SpedFiscal.txt");
                }
            }
            catch (Exception ex)
            {
                this.Text = "Sped Fiscal";
                lblObervacao.Text = string.Empty;
                Application.DoEvents();
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbContador.SelectedIndex == 0)
            {
                errorProvider1.SetError(label8, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");

                result = false;
            }           
            else
                errorProvider1.Clear();


            return result;
        }

        private string salvaArquivoTempLocal(ArquivoDTO arquivo)
        {
            try
            {
                FileInfo fi = arquivo.fileInfo;
                string caminhoTemp = System.IO.Path.GetTempPath() + arquivo.fileInfo.Name;

                if (!File.Exists(caminhoTemp))
                {
                    using (FileStream fs = new FileStream(caminhoTemp, FileMode.Create, FileAccess.ReadWrite))
                    {
                        arquivo.memoryStream.WriteTo(fs);
                        fs.Close();
                    }
                }
                return caminhoTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCadContador_Click(object sender, EventArgs e)
        {
            using (FrmContador frm = new FrmContador())
            {
                int CodSelec = Convert.ToInt32(cbContador.SelectedValue);
                frm.ShowDialog();
                GetDropContador();
                cbContador.SelectedValue = CodSelec;
            }
        }

        private void cbPerfilApreArquivo_Enter(object sender, EventArgs e)
        {
            if (cbPerfilApreArquivo.SelectedIndex == 0)
                lblObervacao.Text = "Perfil “A” determina a apresentação dos registros mais detalhado";
            else if (cbPerfilApreArquivo.SelectedIndex == 1)
                lblObervacao.Text = "perfil “B” trata as informações de forma sintética(totalizações por período:)";
        }

        private void cbPerfilApreArquivo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPerfilApreArquivo.SelectedIndex == 0)
                lblObervacao.Text = "Perfil “A” determina a apresentação dos registros mais detalhado";
            else if (cbPerfilApreArquivo.SelectedIndex == 1)
                lblObervacao.Text = "perfil “B” trata as informações de forma sintética(totalizações por período:)";
        }

        private void cbPerfilApreArquivo_Leave(object sender, EventArgs e)
        {
            lblObervacao.Text = string.Empty;
        }

       
    }
}
