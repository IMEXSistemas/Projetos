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

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchPlaca : Form
    {
        Utility Util = new Utility();

        LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();

        public FrmSearchPlaca()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
          }

        public int Result { get; set; }
        public int CodClienteSelec = -1;

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
            
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
           CreaterCursor Cr = new CreaterCursor();
           this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
           PesquisaCliente();
           this.Cursor = Cursors.Default;
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            txtPlaca.Focus();

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }
        

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                PesquisaCliente();
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }

        private void PesquisaCliente()
        {
            try
            {

                LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "like ", "%" + txtPlaca.Text.ToUpper() + "%"));
                LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio, "PLACA");
                DataGriewSearch.AutoGenerateColumns = false;
                DataGriewSearch.DataSource = LIS_VEICULOCLIENTEColl;
                txtPlaca.Focus();                
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
            if (LIS_VEICULOCLIENTEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(LIS_VEICULOCLIENTEColl[rowindex].IDCLIENTE);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_VEICULOCLIENTEColl.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_VEICULOCLIENTEColl[indice].IDCLIENTE);
                   this.Close();

               }
                else if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmCliente FrmCliente = new FrmCliente())
                    {
                        int linha = DataGriewSearch.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmCliente.CodClienteSelec = Convert.ToInt32(LIS_VEICULOCLIENTEColl[linha].IDCLIENTE);
                        FrmCliente.ShowDialog();
                    }
                }
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            FrmCliente FrmCliente = new FrmCliente();
            FrmCliente.ShowDialog();
        }

      

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
           
        }

       

       
    }
}
