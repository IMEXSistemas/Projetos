using BmsSoftware.Modulos.Financeiro;
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
    public partial class FrmEntradaCaixa : Form
    {
        Utility Util = new Utility();   
        public FrmEntradaCaixa()
        {
            InitializeComponent();
        }

        public int IDCLIENTE = -1;
        public int IDRESERVA = -1;
        public decimal ValorPago = 0;
        private void FrmEntradaCaixa_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtVlPago.Text = ValorPago.ToString("n2");

            GetDropTipoDuplicata();
        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                frm._IDTIPODUPLICATA = CodSelec;
                frm.ShowDialog();

                GetDropTipoDuplicata();
                cbTipo.SelectedValue = CodSelec;
            }
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbTipo.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbTipo, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente lançar o valor de " + txtVlPago.Text + " no caixa?",
                            ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    EntradaCaixa();
            }
        }

        private void EntradaCaixa()
        {
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
            CaixaTy.VALOR = Convert.ToDecimal(txtVlPago.Text);
            CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            CLIENTETy = CLIENTEP.Read(IDCLIENTE);
            if (CLIENTETy != null)
                CaixaTy.OBSERVACAO = "Reserva nº " + IDRESERVA.ToString().PadLeft(6, '0') + " Cliente: " + CLIENTETy.NOME;
            else
                CaixaTy.OBSERVACAO = "Reserva nº " + IDRESERVA.ToString().PadLeft(6, '0');

            CaixaTy.NDOCUMENTO = "RV" + IDRESERVA.ToString().PadLeft(6, '0');
            
            try
            {
                CaixaP.Save(CaixaTy);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private void txtVlPago_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
            {
                errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(TextBoxSelec.Text);
                TextBoxSelec.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(TextBoxSelec, "");
                
            }
        }
    }
}
