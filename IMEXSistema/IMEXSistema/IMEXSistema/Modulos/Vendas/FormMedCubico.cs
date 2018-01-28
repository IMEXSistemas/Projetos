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
    public partial class FormMedCubico : Form
    {
        public decimal Result { get; set; }

        public FormMedCubico()
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
            if (txtAltura.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAltura.Text))
                {
                    errorProvider1.SetError(txtAltura, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtAltura.Text);
                    txtAltura.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtAltura, "");
                    SomaUnitMT3();
                }
            }
            else
                txtAltura.Text = "0,0000";
        }

        private void txtLarguraProdM2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtLargura.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtLargura.Text))
                {
                    errorProvider1.SetError(txtLargura, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtLargura.Text);
                    txtLargura.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtLargura, "");

                    SomaUnitMT3();
                }
            }
            else
                txtLargura.Text = "0,0000";
        }

        private void FormMedQuadrada_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void SomaUnitMT3()
        {
            txtTotalM3.Text = (Convert.ToDecimal(txtAltura.Text) * Convert.ToDecimal(txtLargura.Text)
                          * Convert.ToDecimal(txtComprimento.Text)).ToString("n4");
        }

        private void btnTransfere_Click(object sender, EventArgs e)
        {
            Result = Convert.ToDecimal(txtTotalM3.Text);
            this.Close();
        }

        private void txtComprimento_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtComprimento.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtComprimento.Text))
                {
                    errorProvider1.SetError(txtComprimento, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtComprimento.Text);
                    txtComprimento.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtComprimento, "");

                    SomaUnitMT3();
                }
            }
            else
                txtComprimento.Text = "0,0000";
        }

        

      
    }
}
