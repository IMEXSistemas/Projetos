using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BmsSoftware;
using BMSworks.Firebird;
using BMSworks.Model;
using BmsSoftware.Modulos.Cadastros;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Servicos;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchServico : Form
    {
        Utility Util = new Utility();

        SERVICOCollection SERVICOColl = new SERVICOCollection();

        public FrmSearchServico()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
          }

        public int Result { get; set; }

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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
          CreaterCursor Cr = new CreaterCursor();
          this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
          PesquisaServico();
          this.Cursor = Cursors.Default;
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            txtNomePesquisa.Focus();

            btnCadFornecedor.Image = Util.GetAddressImage(6);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                PesquisaServico();
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }

        private void PesquisaServico()
        {
            try
            {
                SERVICOProvider SERVICOP = new SERVICOProvider();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("NOME", "System.String", "like", txtNomePesquisa.Text));

                SERVICOColl = SERVICOP.ReadCollectionByParameter(RowRelatorio, "NOME");
                DataGriewSearch.AutoGenerateColumns = false;
                DataGriewSearch.DataSource = SERVICOColl;
                txtNomePesquisa.Focus();
            }
            catch (Exception)
            {
               MessageBox.Show(ConfigMessage.Default.searchFieldType,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSearchFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }            

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SERVICOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(SERVICOColl[rowindex].IDSERVICO);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (SERVICOColl.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(SERVICOColl[indice].IDSERVICO);
                   this.Close();

               }
                else if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmCliente FrmCliente = new FrmCliente())
                    {
                        int linha = DataGriewSearch.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmCliente.CodClienteSelec = Convert.ToInt32(SERVICOColl[linha].IDSERVICO);
                        FrmCliente.ShowDialog();
                    }
                }
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            using (FrmServico frm = new FrmServico())
            {
                frm.ShowDialog();
            }
        }     

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para maiores detalhes do cliente pressione Ctrl+D.";
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

       

       
    }
}
