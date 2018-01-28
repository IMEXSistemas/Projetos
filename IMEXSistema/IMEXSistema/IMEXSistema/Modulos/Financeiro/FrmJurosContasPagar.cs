using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;


namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmJurosContasPagar : Form
    {
        Utility Util = new Utility();   
        JUROSDUPLICATASProvider JUROSDUPLICATASP = new JUROSDUPLICATASProvider();

        public FrmJurosContasPagar()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }
        int _IDJUROSDUPLICATA = 1;
        public JUROSDUPLICATASEntity Entity
        {
            get
            {
                if (txtMultaAtraso.Text == string.Empty)
                    txtMultaAtraso.Text = "0,00";

                if (txtJurosDiario.Text == string.Empty)
                    txtJurosDiario.Text = "0,00";

                if (txtTaxa.Text == string.Empty)
                    txtTaxa.Text = "0,00";

                if (txtOutrasTaxa.Text == string.Empty)
                    txtOutrasTaxa.Text = "0,00";       

                decimal? MULTAATRASO= Convert.ToDecimal(txtMultaAtraso.Text);
                decimal? JUROSDIA = Convert.ToDecimal(txtJurosDiario.Text);
                decimal? TAXA = Convert.ToDecimal(txtTaxa.Text);
                decimal? OUTRAS = Convert.ToDecimal(txtOutrasTaxa.Text);
                string FLAGCALCULAR = chkCalculoJuro.Checked ? "S" : "N";

                return new JUROSDUPLICATASEntity(_IDJUROSDUPLICATA, MULTAATRASO, JUROSDIA, TAXA, OUTRAS,
                                                 FLAGCALCULAR  );
            }
            set
            {
                if (value != null)
                {
                    _IDJUROSDUPLICATA = value.IDJUROSDUPLICATA;
                    txtMultaAtraso.Text = Convert.ToDecimal(value.MULTAATRASO).ToString("n2");
                    txtJurosDiario.Text = Convert.ToDecimal(value.JUROSDIA).ToString("n3"); ;
                    txtTaxa.Text = Convert.ToDecimal(value.TAXA).ToString("n2");
                    txtOutrasTaxa.Text = Convert.ToDecimal(value.OUTRAS).ToString("n2");
                    chkCalculoJuro.Checked = value.FLAGCALCULAR == "S" ? true : false;
                    errorProvider1.Clear();
                }                
            }
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

        private void FrmJurosContasPagar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmJurosContasPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmJurosContasPagar_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //1 Juros de Contas a Pagar
            Entity =  JUROSDUPLICATASP.Read(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void Grava()
        {
            try
            {
                JUROSDUPLICATASP.Save(Entity);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void txtMultaAtraso_Validating(object sender, CancelEventArgs e)
        {
            if (txtMultaAtraso.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtMultaAtraso.Text))
                {
                    errorProvider1.SetError(txtMultaAtraso, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtMultaAtraso.Text);
                    txtMultaAtraso.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtMultaAtraso, "");
                }
            }
            else
            {
                txtMultaAtraso.Text = "0,00";
                errorProvider1.SetError(txtMultaAtraso, "");
            }
        }

        private void txtJurosDiario_Validating(object sender, CancelEventArgs e)
        {
            if (txtJurosDiario.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtJurosDiario.Text))
                {
                    errorProvider1.SetError(txtJurosDiario, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtJurosDiario.Text);
                    txtJurosDiario.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtJurosDiario, "");
                }
            }
            else
            {
                txtJurosDiario.Text = "0,000";
                errorProvider1.SetError(txtJurosDiario, "");
            }
        }

        private void txtTaxa_Validating(object sender, CancelEventArgs e)
        {
            if (txtTaxa.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTaxa.Text))
                {
                    errorProvider1.SetError(txtTaxa, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtTaxa.Text);
                    txtTaxa.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTaxa, "");
                }
            }
            else
            {
                txtTaxa.Text = "0,00";
                errorProvider1.SetError(txtTaxa, "");
            }
        }

        private void txtOutrasTaxa_Validating(object sender, CancelEventArgs e)
        {
            if (txtOutrasTaxa.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtOutrasTaxa.Text))
                {
                    errorProvider1.SetError(txtOutrasTaxa, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtOutrasTaxa.Text);
                    txtOutrasTaxa.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtOutrasTaxa, "");
                }
            }
            else
            {
                txtOutrasTaxa.Text = "0,00";
                errorProvider1.SetError(txtOutrasTaxa, "");
            }
        }

        private void txtMultaAtraso_Enter(object sender, EventArgs e)
        {
            if (txtMultaAtraso.Text == "0,00")
                txtMultaAtraso.Text = string.Empty;
        }

        private void txtJurosDiario_Enter(object sender, EventArgs e)
        {
            if (txtJurosDiario.Text == "0,000")
                txtJurosDiario.Text = string.Empty;
        }

        private void txtTaxa_Enter(object sender, EventArgs e)
        {
            if (txtTaxa.Text == "0,00")
                txtTaxa.Text = string.Empty;
        }

        private void txtOutrasTaxa_Enter(object sender, EventArgs e)
        {
            if (txtOutrasTaxa.Text == "0,00")
                txtOutrasTaxa.Text = string.Empty;
        }

       
    }
}
