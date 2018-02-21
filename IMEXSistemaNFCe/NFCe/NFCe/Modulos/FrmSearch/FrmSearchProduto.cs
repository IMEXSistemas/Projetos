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

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchProduto : Form
    {
        Utility Util = new Utility();

        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
       

        public FrmSearchProduto()
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
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
             PesquisaFiltro();
        }

        private void PesquisaFiltro()
        {
            if (txtNomePesquisa.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNomePesquisa, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
            }
            else
            {
                try
                {
                    PesquisaProduto();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private void PesquisaProduto()
        {
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMEPRODUTO", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODBARRA", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%"));
                
                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;
                txtNomePesquisa.Focus();
                lblTotalPesquisa.Text = "Total da pesquisa: " + LIS_PRODUTOSColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

     

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;        
                   
            txtNomePesquisa.Focus();
           
        }

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                btnPesquisa_Click(null, null);
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewDados.Focus();
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
            if (LIS_PRODUTOSColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(LIS_PRODUTOSColl[rowindex].IDPRODUTO);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
           {
               if (LIS_PRODUTOSColl.Count > 0)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_PRODUTOSColl[indice].IDPRODUTO);  

                   this.Close();
               }
           }
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
        {
           
        }

        private void txtCriterioPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNomePesquisa.Text.TrimEnd() != string.Empty)
                PesquisaProduto();
        }

       

       
    }
}
