using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmConfiguraServico : Form
    {
        Utility Util = new Utility();

        public FrmConfiguraServico()
        {
            InitializeComponent();
        }

        private void FrmConfiguraServico_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                if (BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza != string.Empty)
                    cbCodNatureza.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza) - 1;
                if (BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza != string.Empty)
                    cbRegimeTributacao.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigNotaFiscalServico.Default.RegimeTributacao) - 1;


                txtSerie.Text = BmsSoftware.ConfigNotaFiscalServico.Default.Serie;
                txtInscMunicipalPrestador.Text = BmsSoftware.ConfigNotaFiscalServico.Default.InscricaoMunicipalPrestador;

                if (BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza != string.Empty)
                    cbOptanteSimples.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigNotaFiscalServico.Default.OptanteSimples) - 1;

                txtLocalInstall.Text = BmsSoftware.ConfigNotaFiscalServico.Default.LocalArquivoTexto;
                txtCodEspecAtividadeFederal.Text = BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificaçãoAtividadeFederal;
                txtCodEspecAtividadeMunicipal.Text = BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificacaoAtividadeMunicipal;

                txtInscricaoEstadual.Text = BmsSoftware.ConfigNotaFiscalServico.Default.InscricaoEstadualPrestador;

                txtAliquota.Text = BmsSoftware.ConfigNotaFiscalServico.Default.Aliquota;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCancelEdiServico_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnAddServico_Click(object sender, EventArgs e)
        {
            try
            {
                BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza = (cbCodNatureza.SelectedIndex + 1).ToString();
                BmsSoftware.ConfigNotaFiscalServico.Default.RegimeTributacao = (cbRegimeTributacao.SelectedIndex + 1).ToString();
                BmsSoftware.ConfigNotaFiscalServico.Default.Serie = txtSerie.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.InscricaoMunicipalPrestador = txtInscMunicipalPrestador.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.InscricaoEstadualPrestador = txtInscricaoEstadual.Text;                
                BmsSoftware.ConfigNotaFiscalServico.Default.OptanteSimples = (cbOptanteSimples.SelectedIndex + 1).ToString();
                BmsSoftware.ConfigNotaFiscalServico.Default.LocalArquivoTexto = txtLocalInstall.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificaçãoAtividadeFederal = txtCodEspecAtividadeFederal.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificacaoAtividadeMunicipal = txtCodEspecAtividadeMunicipal.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.Aliquota = txtAliquota.Text;
                BmsSoftware.ConfigNotaFiscalServico.Default.Save();

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtAliquota_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtAliquota_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

    }
}
