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

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchProduto : Form
    {
        Utility Util = new Utility();
      
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public int TipoPreco = 1;

        public decimal ResultPreco { get; set; }   

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

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMEPRODUTO", "System.String", "collate pt_br like", "%" + txtCriterioPesquisa.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "collate pt_br like", "%" + txtCriterioPesquisa.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODBARRA", "System.String", "collate pt_br like", "%" + txtCriterioPesquisa.Text.Replace("'", "") + "%"));
                

                if (ValidacoesLibrary.ValidaTipoInt32(txtCriterioPesquisa.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "like", txtCriterioPesquisa.Text.Replace("'", "")));
                }

                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "=", "N"));

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

                LIS_PRODUTOSCollection LIS_PRODUTOSColl_2 = new LIS_PRODUTOSCollection();

                //retira produto inativo
                foreach (var item in LIS_PRODUTOSColl)
                {
                    if (item.FLAGINATIVO == "N")
                        LIS_PRODUTOSColl_2.Add(item);
                }


                LIS_PRODUTOSColl.Clear();
                LIS_PRODUTOSColl = LIS_PRODUTOSColl_2;
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;

                lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {

            
            
        }   
  

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnCadProduto.Image = Util.GetAddressImage(6);
            txtCriterioPesquisa.Focus();          

            btnCancel.Image = Util.GetAddressImage(21);

            txtCriterioPesquisa.Focus();

            if (TipoPreco == 1)
                rb1.Checked = true;
            else if (TipoPreco == 2)
                rb2.Checked = true;
            else if (TipoPreco == 3)
                rb3.Checked = true;
        }      

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                PesquisaRapida();
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

                    if (rb1.Checked)
                        ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[rowindex].VALORVENDA1);
                    else if (rb2.Checked)
                        ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[rowindex].VALORVENDA2);
                    else if
                         (rb3.Checked)
                        ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[rowindex].VALORVENDA3);


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

                   if (rb1.Checked)
                       ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[indice].VALORVENDA1);
                   else if (rb2.Checked)
                       ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[indice].VALORVENDA2);
                   else if
                        (rb3.Checked)
                       ResultPreco = Convert.ToDecimal(LIS_PRODUTOSColl[indice].VALORVENDA3);


                   this.Close();
               }
           }
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                (new FrmProduto()).Show();
            }
            else
            {
                (new FrmProduto2()).Show();
            }
        }

        private void txtCriterioPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCriterioPesquisa.Text.TrimEnd() != string.Empty )
                PesquisaRapida();
        }

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //*
           // try
          //  {
           //     if (e.Value != null && e.ColumnIndex == 2)
           //     {
             //       int CodProdutoSelec = Convert.ToInt32(DataGriewDados.Rows[e.RowIndex].Cells[2].Value.ToString());
             //       decimal EstoqueAtualSelec = Util.EstoqueAtual(CodProdutoSelec, false);
            //        DataGriewDados.Rows[e.RowIndex].Cells[4].Value = EstoqueAtualSelec;
             //   }
          //  }
          ///  catch (Exception ex)
          //  {
          ///      MessageBox.Show("Erro na busca do estoque!");
//MessageBox.Show("Erro técnico: " + ex.Message);
         //   }

            //
        }

       

       
    }
}
