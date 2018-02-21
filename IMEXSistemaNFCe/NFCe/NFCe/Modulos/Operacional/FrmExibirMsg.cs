using BMSworks.Firebird;
using BMSworks.UI;
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
    public partial class FrmExibirMsg : Form
    {
        public string Titulo = ConfigSistema1.Default.NameSytem;
        public string Mensagem = string.Empty;
        public string Cor = "Blue";
        Utility Util = new Utility();

        public FrmExibirMsg()
        {
            InitializeComponent();
        }

        private void FrmExibirMsg_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = Titulo;

                this.lblmsg.Text = Util.LimiterText(Mensagem, 80);

                if (Mensagem.Length > 80)
                {
                    lblmsg.Text = lblmsg.Text + " ...";
                    this.Size = new Size(653, 138); 
                }

                if (Cor == "Blue")
                {
                    pictureBox1_Salvo.Visible = true;
                    this.BackColor = Color.Blue;
                    lblStatus.Text = "Sucesso";
                }
                else if (Cor == "Red")
                {
                    pictureBox2_erro.Visible = true;
                    pictureBox2_erro.Location = new Point(625, 10);
                    this.BackColor = Color.Red;
                    lblStatus.Text = "Erro";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
            
        }

        private void FrmExibirMsg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Salvo_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_erro_Click(object sender, EventArgs e)
        {

        }
    }
}
