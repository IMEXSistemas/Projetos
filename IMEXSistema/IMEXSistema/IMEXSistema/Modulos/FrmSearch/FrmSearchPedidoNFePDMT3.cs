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
using BmsSoftware.Modulos.Vendas;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchPedidoNFePDMT3 : Form
    {
        Utility Util = new Utility();

        LIS_PEDIDOMARCProvider LIS_PEDIDOMARCP = new LIS_PEDIDOMARCProvider();
        LIS_PEDIDOMARCCollection LIS_PEDIDOMARCColl = new LIS_PEDIDOMARCCollection();

        public FrmSearchPedidoNFePDMT3()
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
            if (txtNomePesquisa.Text.Trim().Length > 0 && !ValidacoesLibrary.ValidaTipoInt32(txtNomePesquisa.Text))
            {
                errorProvider1.SetError(txtNomePesquisa, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                PesquisaPedido();
            }
            
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            txtNomePesquisa.Focus();

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
               PesquisaPedido();
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }

        private void PesquisaPedido()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                if(txtNomePesquisa.Text.Trim().Length > 0)
                    RowRelatorio.Add(new RowsFiltro("IDPEDIDOMARC",  "System.Int32", "=", txtNomePesquisa.Text));

                LIS_PEDIDOMARCColl = LIS_PEDIDOMARCP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDOMARC desc");
                DataGriewSearch.AutoGenerateColumns = false;
                DataGriewSearch.DataSource = LIS_PEDIDOMARCColl;
                txtNomePesquisa.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {

                this.Cursor = Cursors.Default;

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
            if (LIS_PEDIDOMARCColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(LIS_PEDIDOMARCColl[rowindex].IDPEDIDOMARC);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
           {
               if (LIS_PEDIDOMARCColl.Count > 0)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_PEDIDOMARCColl[indice].IDPEDIDOMARC);
                   this.Close();
               }
           }
        }

        private void DataGriewSearch_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PEDIDOMARCColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                if (ColumnSelecionada == 0)//Editar
                {
                    FrmPedidoVendaMarc FrmPedidoV = new FrmPedidoVendaMarc();
                    FrmPedidoV._IDPRODUTOSPEDMARC = Convert.ToInt32(LIS_PEDIDOMARCColl[rowindex].IDPEDIDOMARC);
                    FrmPedidoV.ShowDialog();                        
                }               
                
            }
        }

       
       
    }
}
