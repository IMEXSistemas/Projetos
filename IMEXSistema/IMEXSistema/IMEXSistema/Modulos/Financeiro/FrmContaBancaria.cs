using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;
using System.IO;
using VVX;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmContaBancaria : Form
    {
        BANCOCollection BANCOColl = new BANCOCollection();

        LIS_CONTACORRENTECollection LIS_CONTACORRENTEColl = new LIS_CONTACORRENTECollection();
        LIS_CONTACORRENTEProvider LIS_CONTACORRENTEP = new LIS_CONTACORRENTEProvider();

        Utility Util = new Utility();

        CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmContaBancaria()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

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
            }        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDCONTACORRENTE = -1;
        public CONTACORRENTEEntity Entity
        {
            get
            {
                int IDBANCO = Convert.ToInt32(cbBanco.SelectedValue);
                string AGENCIA = txtAgencia.Text;
                string CONTACORRENTE = txtContaCorrente.Text;
                
                string OBSERVACAO = txtObservacao.Text;
                string NOMECONTA = txtNomeContaCorrente.Text;
                decimal SALDO = Convert.ToDecimal(txtSaldo.Text);

                return new CONTACORRENTEEntity(_IDCONTACORRENTE, IDBANCO, AGENCIA, CONTACORRENTE, OBSERVACAO, NOMECONTA, SALDO);
            }
            set
            {

                if (value != null)
                {
                    _IDCONTACORRENTE = value.IDCONTACORRENTE;
                    cbBanco.SelectedValue  = value.IDBANCO;
                    txtAgencia.Text = value.AGENCIA;
                    txtContaCorrente.Text = value.CONTACORRENTE;
                    txtObservacao.Text = value.OBSERVACAO;
                    txtNomeContaCorrente.Text = value.NOMECONTA;
                    txtSaldo.Text = SaldoExtrato().ToString("n2");// Convert.ToDecimal(value.SALDO).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCONTACORRENTE = -1;
                    cbBanco.SelectedIndex = -1;
                    txtAgencia.Text = string.Empty;
                    txtContaCorrente.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    txtNomeContaCorrente.Text = string.Empty;
                    txtSaldo.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }

        private decimal SaldoExtrato()
        {
            decimal result = 0;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCONTACORRENTE", "System.Int32", "=", _IDCONTACORRENTE.ToString()));

            LIS_MOVCONTACORRENTECollection LIS_MOVCONTACORRENTE2Coll = new LIS_MOVCONTACORRENTECollection();
            LIS_MOVCONTACORRENTEProvider LIS_MOVCONTACORRENTEP = new LIS_MOVCONTACORRENTEProvider();
            LIS_MOVCONTACORRENTE2Coll = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO desc");

            foreach (LIS_MOVCONTACORRENTEEntity item in LIS_MOVCONTACORRENTE2Coll)
            {
                if (item.IDTIPOMOVCAIXA == 2)
                    result -= Convert.ToDecimal(item.VALOR * -1);
                else
                    result += Convert.ToDecimal(item.VALOR);
            }

            return result;
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            Grava();
            this.Cursor = Cursors.Default;
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDCONTACORRENTE = CONTACORRENTEP.Save(Entity);
                    GetAllConta();
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void GetAllConta()
        {
            LIS_CONTACORRENTEColl = LIS_CONTACORRENTEP.ReadCollectionByParameter(null, "NOMECONTA");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = LIS_CONTACORRENTEColl;

            lblTotalPesquisa.Text = LIS_CONTACORRENTEColl.Count.ToString();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNomeContaCorrente.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNomeContaCorrente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbBanco.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbBanco, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtAgencia.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtAgencia, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtContaCorrente.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtContaCorrente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            GetToolStripButtonCadastro();
            GetAllConta();
            GetDropBanco();

            btnCadBanco.Image = Util.GetAddressImage(6);
           

            this.Cursor = Cursors.Default;
        }

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNomeContaCorrente.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNomeContaCorrente.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDCONTACORRENTE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        CONTACORRENTEP.Delete(_IDCONTACORRENTE);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllConta();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( LIS_CONTACORRENTEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_CONTACORRENTEColl[rowindex].IDCONTACORRENTE);

                    Entity = CONTACORRENTEP.Read(CodigoSelect);

                    //Salva o saldo
                    CONTACORRENTEP.Save(Entity);

                    tabControlMarca.SelectTab(0);
                    txtNomeContaCorrente.Focus();
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        public static string InputBox(string prompt, string title, string defaultValue)
        {
            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }

       
        private void ImprimirListaGeral()
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }     
      

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void FrmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_CONTACORRENTEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_CONTACORRENTEColl[indice].IDCONTACORRENTE);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CONTACORRENTEP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNomeContaCorrente.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CONTACORRENTEP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllConta();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            using (FrmBanco frm = new FrmBanco())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbBanco.SelectedValue);
                GetDropBanco();
                cbBanco.SelectedValue = CodSelec;             
                
            }
        }

        private void GetDropBanco()
        {
            BANCOProvider BANCOP = new BANCOProvider();
            BANCOColl = BANCOP.ReadCollectionByParameter(null, "NOMEBANCO");

            cbBanco.DisplayMember = "NOMEBANCO";
            cbBanco.ValueMember = "IDBANCO";

            BANCOEntity BANCOTy = new BANCOEntity();
            BANCOTy.NOMEBANCO = ConfigMessage.Default.MsgDrop;
            BANCOTy.IDBANCO = -1;
            BANCOColl.Add(BANCOTy);

            Phydeaux.Utilities.DynamicComparer<BANCOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<BANCOEntity>(cbBanco.DisplayMember);

            BANCOColl.Sort(comparer.Comparer);
            cbBanco.DataSource = BANCOColl;

            cbBanco.SelectedIndex = 0;
        }

       
    }
}
