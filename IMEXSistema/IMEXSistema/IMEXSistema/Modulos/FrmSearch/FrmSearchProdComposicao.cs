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
    public partial class FrmSearchProdComposicao : Form
    {
        Utility Util = new Utility();

        LIS_PRODUTOCOMPOSICAOCollection LIS_PRODUTOCOMPOSICAOColl = new LIS_PRODUTOCOMPOSICAOCollection();
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();

        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
        LIS_PRODUTOCOMPOSICAOProvider LIS_PRODUTOCOMPOSICAOP = new LIS_PRODUTOCOMPOSICAOProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public decimal ResultPreco { get; set; }

        public FrmSearchProdComposicao()
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
            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(Filtro, "NOMEPRODUTO");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;

                lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();
            }
            else
                PesquisaFiltro();
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                errorProvider1.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            // Nome campo que sera filtrado
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_PRODUTOSColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;
            }

            // Retorna o tipo da coluna para pesquisa Ex.: String, Integer, Date...
            string Tipo = DataGriewDados.Columns[cbCamposPesquisa.SelectedValue.ToString()].ValueType.FullName;

            if (Tipo.Length > 20)
                Tipo = Util.GetTypeCell(Tipo);//Retorna o texto resumido do tipo

            string Valor = txtCriterioPesquisa.Text;

            //Verifica se o valor digitado e compativel com
            // o tipo de pesquisa
            if (ValidacoesLibrary.ValidaTipoPesquisa(Tipo, Valor))
            {
                if (Tipo == "System.DateTime")//formata data para pesquisa.
                    Valor = Util.ConverStringDateSearch(txtCriterioPesquisa.Text);
                else if (Tipo == "System.Decimal")//formata Numeric para pesquisa.
                    Valor = Util.ConverStringDecimalSearch(txtCriterioPesquisa.Text);

                filtroProfile = new RowsFiltro(campo, Tipo, cbTipoPesquisa.SelectedValue.ToString(), Valor);

                if (!chkBoxAcumulaPesquisa.Checked)//Acumular pesquisa
                    Filtro.Clear();

                Filtro.Insert(Filtro.Count, filtroProfile);

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(Filtro, "NOMEPRODUTO");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;

                lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnCadProduto.Image = Util.GetAddressImage(6);
            txtCriterioPesquisa.Focus();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            cbTipoPesquisa.SelectedIndex = 0;

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }


        private void PreencheDropCamposPesquisa()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("descricao", typeof(string)));
            list.Columns.Add(new DataColumn("nomecampo_tipo", typeof(string)));

            for (int i = 0; i < DataGriewDados.ColumnCount; i++)
            {
                list.Rows.Add(list.NewRow());
            }

            int indexCol = 0;
            int Col = 0;
            foreach (DataGridViewColumn Columns in DataGriewDados.Columns)
            {
                list.Rows[indexCol][Col] = Columns.HeaderText;
                list.Rows[indexCol][Col + 1] = Columns.DataPropertyName;
                indexCol++;
            }


            cbCamposPesquisa.DataSource = list;
            cbCamposPesquisa.DisplayMember = "descricao";
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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
            if (LIS_PRODUTOSColl.Count > 0 && LIS_PRODUTOCOMPOSICAOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1 )
                {
                    DialogResult dr = MessageBox.Show("Deseja realmente adicionar as composições do produto: " + LIS_PRODUTOSColl[rowindex].NOMEPRODUTO,
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
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
            using (FrmProduto frm = new FrmProduto())
            {
                frm.ShowDialog();
            }
        }

        private void DataGriewDados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PRODUTOSColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    GetProdutoComposicao(Convert.ToInt32(LIS_PRODUTOSColl[rowindex].IDPRODUTO));

                }
            }
        }

        public void GetProdutoComposicao(int CodProduto)
        {
            RowsFiltroCollection ProdutoComposicao = new RowsFiltroCollection();
            ProdutoComposicao.Add(new RowsFiltro("IDPRODUTOMAIN", "System.Int32", "=", CodProduto.ToString()));

            LIS_PRODUTOCOMPOSICAOColl = LIS_PRODUTOCOMPOSICAOP.ReadCollectionByParameter(ProdutoComposicao, "IDPRODUTOMAIN");

            dataGridDadosComposicao.AutoGenerateColumns = false;
            dataGridDadosComposicao.DataSource = LIS_PRODUTOCOMPOSICAOColl;
           
        }

        private void dataGridDadosComposicao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGriewDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
