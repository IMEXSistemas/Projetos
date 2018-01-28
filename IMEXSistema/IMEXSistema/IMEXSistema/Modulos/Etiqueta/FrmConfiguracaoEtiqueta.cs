using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Etiqueta
{
    public partial class FrmConfiguracaoEtiqueta : Form
    {
        Utility Util = new Utility();

        public FrmConfiguracaoEtiqueta()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmConfiguracaoEtiqueta_Load(object sender, EventArgs e)
        {
            try
            {
                cbModeloEtiqueta.SelectedIndex = 0;
                NUpD_Left_Esquerda.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Left_Esquerda_POLIFIX_2060);
                NUpD_Right_Direito.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Right_Direito_POLIFIX_2060);
                NUpD_Top_Topo.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Top_Topo_POLIFIX_2060);
                NUpD_Botto_Inferior.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Bottom_Inferior_POLIFIX_2060);
                NUpD_Width_Largura.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Width_Largura_POLIFIX_2060);
                NUpD_Height_Altura.Value = Convert.ToInt32(BmsSoftware.ConfigEtiqueta.Default.Height_Altura_POLIFIX_2060);

                btnAdd.Image = Util.GetAddressImage(15);
                btnSair.Image = Util.GetAddressImage(21);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                BmsSoftware.ConfigEtiqueta.Default.Left_Esquerda_POLIFIX_2060 = NUpD_Left_Esquerda.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Right_Direito_POLIFIX_2060 = NUpD_Right_Direito.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Top_Topo_POLIFIX_2060 = NUpD_Top_Topo.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Bottom_Inferior_POLIFIX_2060 = NUpD_Botto_Inferior.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Width_Largura_POLIFIX_2060 = NUpD_Width_Largura.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Height_Altura_POLIFIX_2060 = NUpD_Height_Altura.Value.ToString();
                BmsSoftware.ConfigEtiqueta.Default.Save();

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }      
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
