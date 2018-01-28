using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FormMedQuadrada : Form
    {
        public decimal Result { get; set; }

        public FormMedQuadrada()
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMedQuadrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void txtAlturaProdM2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtAlturaProdM2.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAlturaProdM2.Text))
                {
                    errorProvider1.SetError(txtAlturaProdM2, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtAlturaProdM2.Text);
                    txtAlturaProdM2.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtAlturaProdM2, "");
                    SomaUnitMT2();
                }
            }
            else
                txtAlturaProdM2.Text = "0,0000";
        }

        private void txtLarguraProdM2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtLarguraProdM2.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtLarguraProdM2.Text))
                {
                    errorProvider1.SetError(txtLarguraProdM2, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtLarguraProdM2.Text);
                    txtLarguraProdM2.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtLarguraProdM2, "");
                    SomaUnitMT2();
                }
            }
            else
                txtLarguraProdM2.Text = "0,0000";
        }

        private void FormMedQuadrada_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void SomaUnitMT2()
        {
            txtTotalM2ProdM2.Text = (Convert.ToDecimal(txtAlturaProdM2.Text) * Convert.ToDecimal(txtLarguraProdM2.Text)).ToString("n4");
        }

        private void btnTransfere_Click(object sender, EventArgs e)
        {
            Result = Convert.ToDecimal(txtTotalM2ProdM2.Text);
            this.Close();
        }
    }
}
