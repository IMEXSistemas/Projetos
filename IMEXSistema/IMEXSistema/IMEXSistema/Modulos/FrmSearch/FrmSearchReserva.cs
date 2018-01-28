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
using BmsSoftware.Modulos.Hospedagem;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchReserva : Form
    {
        Utility Util = new Utility();

        LIS_RESERVACollection LIS_RESERVAColl = new LIS_RESERVACollection();
        LIS_RESERVAProvider LIS_RESERVAP = new LIS_RESERVAProvider();

        public FrmSearchReserva()
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
           PesquisaReserva();
           this.Cursor = Cursors.Default;
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            PreencheDropCamposPesquisa();
            btnCadFornecedor.Image = Util.GetAddressImage(6);
            txtNomePesquisa.Focus();

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }

        private void PreencheDropCamposPesquisa()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("descricao", typeof(string)));
            list.Columns.Add(new DataColumn("nomecampo_tipo", typeof(string)));

            for (int i = 0; i < DataGriewSearch.ColumnCount; i++)
            {
                list.Rows.Add(list.NewRow());
            }

            int indexCol = 0;
            int Col = 0;
            foreach (DataGridViewColumn Columns in DataGriewSearch.Columns)
            {
                list.Rows[indexCol][Col] = Columns.HeaderText;
                list.Rows[indexCol][Col + 1] = Columns.DataPropertyName;
                indexCol++;
            }


            cbCamposPesquisa.DataSource = list;
            cbCamposPesquisa.DisplayMember = "descricao";
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }		

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                PesquisaReserva();
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }

        private void PesquisaReserva()
        {
            try
            {

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro(cbCamposPesquisa.SelectedValue.ToString(), "System.String", "like ", "%" + txtNomePesquisa.Text.ToUpper() + "%"));

                LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(RowRelatorio, "IDRESERVA desc");
                DataGriewSearch.AutoGenerateColumns = false;
                DataGriewSearch.DataSource = LIS_RESERVAColl;
                txtNomePesquisa.Focus();
                lblObsField.Text = "Total da pesquisa: " + LIS_RESERVAColl.Count.ToString();
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
            if (LIS_RESERVAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(LIS_RESERVAColl[rowindex].IDRESERVA);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_RESERVAColl.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_RESERVAColl[indice].IDRESERVA);
                   this.Close();

               }
                else if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmReserva2 FrmCliente = new FrmReserva2())
                    {
                        int linha = DataGriewSearch.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmCliente.ShowDialog();
                    }
                }
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            FrmReserva2 Frm = new FrmReserva2();
            Frm.ShowDialog();
        }

      

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para maiores detalhes da reserva pressione Ctrl+D.";
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void txtNomePesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtNomePesquisa.Text.TrimEnd() != string.Empty)
                PesquisaReserva();
        }

       

       
    }
}
