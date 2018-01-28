using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmAddProdutoPedMateriaPrimacs : Form
    {
        PRODUTOSCollection PRODUTOSMTColl = new PRODUTOSCollection();

        Utility Util = new Utility();        
        public FrmAddProdutoPedMateriaPrimacs()
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

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmAddProdutoPedMateriaPrimacs_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnCadProd.Image = Util.GetAddressImage(6);
            btnCadMatPrima.Image = Util.GetAddressImage(6);

            GetDropProdutosMTQ();
        }

        private void btnCadProdMTQ_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProdutoMTQ.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutosMTQ();
                cbProdutoMTQ.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutosMTQ()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSMTColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProdutoMTQ.DisplayMember = "NOMEPRODUTO";
            cbProdutoMTQ.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSMTColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProdutoMTQ.DisplayMember);

            PRODUTOSMTColl.Sort(comparer.Comparer);
            cbProdutoMTQ.DataSource = PRODUTOSMTColl;

            cbProdutoMTQ.SelectedIndex = 0;
        }

        private void cbProdutoMTQ_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProdutoMTQ.SelectedValue = result;
                    }
                }
            }
        }

        private void cbProdutoMTQ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }
    }
}
