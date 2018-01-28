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
using BmsSoftware.Modulos.Servicos;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchOS : Form
    {
        Utility Util = new Utility();

        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();

        public FrmSearchOS()
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

                LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                if(txtNomePesquisa.Text.Trim().Length > 0)
                    RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO",  "System.Int32", "=", txtNomePesquisa.Text));
                
                LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO , DATAEMISSAO desc");
                DataGriewSearch.AutoGenerateColumns = false;
                DataGriewSearch.DataSource = LIS_ORDEMSERVICOSFECHColl;
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
            if (LIS_ORDEMSERVICOSFECHColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                int ColumnSelecionada = e.ColumnIndex;
                if (rowindex != -1)
                {
                    if (ColumnSelecionada == 0)//Visualizar
                    {
                        using (FrmOrdemServicoSFech frm = new FrmOrdemServicoSFech())
                        {
                            frm._IDORDEMSERVICO = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[rowindex].IDORDEMSERVICO);
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        Result = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[rowindex].IDORDEMSERVICO);
                        this.Close();
                    }
                }               
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
           {
               if (LIS_ORDEMSERVICOSFECHColl.Count > 0)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_ORDEMSERVICOSFECHColl[indice].IDORDEMSERVICO);
                   this.Close();
               }
           }
        }

        private void DataGriewSearch_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

       
       
    }
}
