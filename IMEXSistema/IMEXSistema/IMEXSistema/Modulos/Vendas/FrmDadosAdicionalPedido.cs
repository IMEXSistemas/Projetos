using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmDadosAdicionalPedido : Form
    {
        Utility Util = new Utility();
        public string TextoAdicional = string.Empty;
        public FrmDadosAdicionalPedido()
        {
            InitializeComponent();
        }

        private void FrmDadosAdicionalPedido_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnSair.Image = Util.GetAddressImage(21);

            txtDadosAdicional.Text = TextoAdicional;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextoAdicional =  txtDadosAdicional.Text;
            this.Close();
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
           
        }
    }
}
