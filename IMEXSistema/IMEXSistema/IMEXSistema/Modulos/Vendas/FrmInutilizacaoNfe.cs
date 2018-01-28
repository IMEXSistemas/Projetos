using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmInutilizacaoNfe : Form
    {
        Utility Util = new Utility();  
        public FrmInutilizacaoNfe()
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

                if (control is ComboBox)
                {
                    control.KeyDown += new KeyEventHandler(controlFocus_KeyDown);
                    control.KeyPress += new KeyPressEventHandler(controlFocus_KeyPress);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        void controlFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmNotaFiscalEspelho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }

        }

        private void FrmInutilizacaoNfe_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                Inutilizar();
            }
        }

        private void AtualizaDadosNFE()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();
                LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl2 = new LIS_NOTAFISCALECollection();
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("NFISCALE", "System.String", ">=", txtIni.Text.PadLeft(8, '0')));
                RowRelatorio.Add(new RowsFiltro("NFISCALE", "System.String", "<=", txtFim.Text.PadLeft(8, '0')));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "N"));
                
                LIS_NOTAFISCALEColl2 = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_NOTAFISCALEColl2.Count > 0)
                {
                    foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl2)
                    {
                        NOTAFISCALEEntity NOTAFISCALETy = new NOTAFISCALEEntity();
                        NOTAFISCALETy = NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE));
                        NOTAFISCALETy.FLAGINUTILIZADO = "S";
                        NOTAFISCALEP.Save(NOTAFISCALETy);
                    }

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Não foi possível alterar as notas selecionadas!!");
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
            if (txtAno.Text == string.Empty)
            {
                errorProvider1.SetError(txtAno, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (txtIni.Text == string.Empty)
            {
                errorProvider1.SetError(txtIni, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtFim.Text == string.Empty)
            {
                errorProvider1.SetError(txtFim, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtJustInut.Text == string.Empty)
            {
                errorProvider1.SetError(txtJustInut, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtJustInut.Text.Length < 15)
            {
                string msgJust = "Campo de Justificativa deverá conter igual ou superior a 15 caracteres!";
                errorProvider1.SetError(txtJustInut, msgJust);
                MessageBox.Show(msgJust);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void Inutilizar()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                nfec.nfecsharp nfe = new nfec.nfecsharp();
                string MsgnInutilizacao = nfe.NfeInutilizacao(
                    txtAno.Text,
                    txtIni.Text,
                    txtFim.Text,
                    txtJustInut.Text);

                if (MsgnInutilizacao != string.Empty)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(MsgnInutilizacao);
                    AtualizaDadosNFE();
                }
                else
                    MessageBox.Show("Houve algum erro para inutilizar as NFe´s");

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInutilizacaoNfe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}

