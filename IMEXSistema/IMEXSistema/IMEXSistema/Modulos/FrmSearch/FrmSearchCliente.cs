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
    public partial class FrmSearchCliente : Form
    {
        Utility Util = new Utility();

        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();


        public FrmSearchCliente()
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
          
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
           
            btnCadFornecedor.Image = Util.GetAddressImage(6);
            btnCancel.Image = Util.GetAddressImage(21);

            txtNomePesquisa.Focus();
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
                
              //  RowRelatorio.Add(new RowsFiltro(cbCamposPesquisa.SelectedValue.ToString(), "System.String", "like ", "%" + txtNomePesquisa.Text.ToUpper() + "%"));
                if (txtNomePesquisa.Text.Trim() != string.Empty)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("NOME", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%" , "or"));
                    RowRelatorio.Add(new RowsFiltro("APELIDO", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("TELEFONE1", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("TELEFONE2", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("FAX", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                    RowRelatorio.Add(new RowsFiltro("ENDERECO1", "System.String", "collate pt_br like", "%" + txtNomePesquisa.Text.Replace("'", "") + "%", "or"));
                    
                    LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio, "NOME");
                    DataGriewSearch.AutoGenerateColumns = false;
                    DataGriewSearch.DataSource = LIS_CLIENTEColl;
                    txtNomePesquisa.Focus();
                    lblObsField.Text = "Total da pesquisa: " + LIS_CLIENTEColl.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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
            if (LIS_CLIENTEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Result = Convert.ToInt32(LIS_CLIENTEColl[rowindex].IDCLIENTE);
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_CLIENTEColl.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
               {
                   //Obter a linha da célula selecionada
                   DataGridViewRow linhaAtual = DataGriewSearch.CurrentRow;

                   //Exibir o índice da linha atual
                   int indice = linhaAtual.Index;
                   Result = Convert.ToInt32(LIS_CLIENTEColl[indice].IDCLIENTE);
                   this.Close();

               }
                else if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmCliente FrmCliente = new FrmCliente())
                    {
                        int linha = DataGriewSearch.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmCliente.CodClienteSelec = Convert.ToInt32(LIS_CLIENTEColl[linha].IDCLIENTE);
                        FrmCliente.ShowDialog();
                    }
                }
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }      

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para maiores detalhes do cliente pressione Ctrl+D.";
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void txtNomePesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtNomePesquisa.Text.TrimEnd() != string.Empty)
                PesquisaCliente();
        }

       

       
    }
}
