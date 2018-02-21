using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Cadastro
{
    public partial class FrmAlteraNFCe : Form
    {
        CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();

        Utility Util = new Utility();
        public int _CUPOMELETRONICOID = -1;
        

        public FrmAlteraNFCe()
        {
            InitializeComponent();
        }

        private void FrmAlteraNFCe_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropStatus();

            if (_CUPOMELETRONICOID != -1)
            {
                CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(_CUPOMELETRONICOID);
                lblNotaFiscal.Text = CUPOMELETRONICOTy.NUMERONFCE.ToString().Trim();
                cbStatus.SelectedValue = CUPOMELETRONICOTy.IDSTATUSNFCE;
                txtChave.Text = CUPOMELETRONICOTy.CHAVEACESSO.Trim();
                txtProtocolo.Text = CUPOMELETRONICOTy.PROTOCOLO.Trim();
            }

            btnSair.Image = Util.GetAddressImage(21);
        }

        private void GetDropStatus()
        {
            try
            {
                STATUSNFCEProvider STATUSNFCEP = new STATUSNFCEProvider();
                STATUSNFCECollection STATUSNFCEColl = new STATUSNFCECollection();
                STATUSNFCEColl = STATUSNFCEP.ReadCollectionByParameter(null, "NOME");

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "STATUSNFCEID";

                STATUSNFCEEntity STATUSNFCETy = new STATUSNFCEEntity();
                STATUSNFCETy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSNFCETy.STATUSNFCEID = -1;
                STATUSNFCEColl.Add(STATUSNFCETy);

                Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity>(cbStatus.DisplayMember);

                STATUSNFCEColl.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSNFCEColl;

                cbStatus.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                CUPOMELETRONICOTy.IDSTATUSNFCE = Convert.ToInt32(cbStatus.SelectedValue);
                CUPOMELETRONICOTy.CHAVEACESSO = txtChave.Text;
                CUPOMELETRONICOTy.PROTOCOLO = txtProtocolo.Text;
                CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                MessageBox.Show(ConfigMessage.Default.MsgSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}
