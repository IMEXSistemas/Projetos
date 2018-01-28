using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmModoImpressao : Form
    {
        public FrmModoImpressao()
        {
            InitializeComponent();
        }

        public string Result { get; set; }
        public string local { get; set; }

        private void brnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (rbGrafico.Checked)
                Result = "grafico";
            else if (rbMatricial.Checked)
                Result = "matricial";

            if (rblocal.Checked)
                local = "local";
            else if (rbRede.Checked)
                local = "rede";

            this.Close();
        }

        private void rbGrafico_CheckedChanged(object sender, EventArgs e)
        {
            rblocal.Enabled = !rbGrafico.Checked;
            rblocal.Checked = !rbGrafico.Checked;
            rbRede.Enabled = !rbGrafico.Checked;
            rbRede.Checked = !rbGrafico.Checked;
        }

        private void FrmModoImpressao_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            rbGrafico.Checked = BmsSoftware.ConfigSistema1.Default.FLAGMODOGRAFICO == "S" ? true : false;
            rbMatricial.Checked = BmsSoftware.ConfigSistema1.Default.FLAGMODOMATRICIAL == "S" ? true : false;
        }
    }
}
