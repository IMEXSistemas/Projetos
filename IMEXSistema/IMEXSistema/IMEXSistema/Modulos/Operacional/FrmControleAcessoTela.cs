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

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmControleAcessoTela : Form
    {
        Utility Util = new Utility();
        CONTROLEACESSOProvider CONTROLEACESSOP = new CONTROLEACESSOProvider();
        FORMULARIOProvider FORMULARIOP = new FORMULARIOProvider();
        NIVELUSUARIOProvider NIVELUSUARIOP = new NIVELUSUARIOProvider();

        LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl2 = new LIS_CONTROLEACESSOCollection();
        LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
        LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
       // FORMULARIOCollection FORMULARIOColl2 = new FORMULARIOCollection();
        NIVELUSUARIOCollection NIVELUSUARIOCOLL = new NIVELUSUARIOCollection();
        NIVELUSUARIOCollection NIVELUSUARIO2COLL = new NIVELUSUARIOCollection();

        FORMULARIOCollection FORMULARIOCollDrop = new FORMULARIOCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        int _IDCONTROLEACESSO = -1;

        public FrmControleAcessoTela()
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
            }
        }

        void controlFocus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        void controlFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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
                    foreach (DataGridViewRow dg in dgvTelas.Rows)
                    {
                        int IDNIVEL = Convert.ToInt32(dg.Cells[0].Value.ToString());
                        string FLAGACESSA = dg.Cells[2].Value  == null ? "N" : dg.Cells[2].Value.ToString();
                        string FLAGALTERA = dg.Cells[3].Value == null ? "N" : dg.Cells[3].Value.ToString();
                        string FLAGAPAGA = dg.Cells[4].Value == null ? "N" : dg.Cells[4].Value.ToString();

                        int IDFORMULARIO = Convert.ToInt32(cbTela.SelectedValue);

                        try
                        {
                            _IDCONTROLEACESSO = BuscaIdControleAcesso(IDNIVEL, IDFORMULARIO);
                            CONTROLEACESSOP.Save(_IDCONTROLEACESSO, FLAGALTERA, FLAGAPAGA, FLAGACESSA, IDNIVEL, IDFORMULARIO);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                            MessageBox.Show("Erro Técnico: " + ex.Message);
                        }
                    }

                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }
     

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbTela.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbTela, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
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
            GetDropTela();
            OpenNivel();

            this.Cursor = Cursors.Default;
        }

        private void OpenNivel()
        {
            //FORMULARIOColl2 = FORMULARIOP.ReadCollectionByParameter(null, "NOMETELA");
            //dgvTelas.AutoGenerateColumns = false;
            //dgvTelas.DataSource = FORMULARIOColl2;

            NIVELUSUARIOCOLL = NIVELUSUARIOP.ReadCollectionByParameter(null, "NOME");
            dgvTelas.AutoGenerateColumns = false;
            dgvTelas.DataSource = NIVELUSUARIOCOLL;            
        }

        private void OpenTelas()
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDFORMULARIO", "System.Int32", "=", Convert.ToInt32(cbTela.SelectedValue).ToString()));
            LIS_CONTROLEACESSOColl2 = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowRelatorio, "NOMETELA");

            LimpaGrid();

            foreach (LIS_CONTROLEACESSOEntity item in LIS_CONTROLEACESSOColl2)
            {
                foreach (DataGridViewRow dg in dgvTelas.Rows)
                {
                    if (item.IDNIVEL == Convert.ToInt32(dg.Cells[0].Value))
                    {
                        dg.Cells[2].Value = item.FLAGACESSA;
                        dg.Cells[3].Value = item.FLAGALTERA;
                        dg.Cells[4].Value = item.FLAGAPAGA;
                    }                    
                }
            }
        }

        private void LimpaGrid()
        {
            foreach (DataGridViewRow dg in dgvTelas.Rows)
            {
                dg.Cells[2].Value = null;
                dg.Cells[3].Value = null;
                dg.Cells[4].Value = null;
            }
        }

        private void GetDropTela()
        {
            cbTela.DisplayMember = "NOMETELA";
            cbTela.ValueMember = "IDFORMULARIO";

            FORMULARIOCollDrop = FORMULARIOP.ReadCollectionByParameter(null, "NOMETELA");

            FORMULARIOEntity FORMULARIOTy = new FORMULARIOEntity();
            FORMULARIOTy.NOMEFORMULARIO = ConfigMessage.Default.MsgDrop;
            FORMULARIOTy.IDFORMULARIO = -1;
            FORMULARIOCollDrop.Add(FORMULARIOTy);

            Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity>(cbTela.DisplayMember);

            FORMULARIOCollDrop.Sort(comparer.Comparer);
            cbTela.DataSource = FORMULARIOCollDrop;

            cbTela.SelectedIndex = 0;
        }
      

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);
        }     

       

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
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
            PRt.Print_DataGridView(dgvTelas, RelatorioTitulo, this.Name);
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


        private void cbnivel2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbTela.SelectedValue) > 0)
                OpenTelas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private int BuscaIdControleAcesso(int IDNIVEL, int IDFORMULARIO)
        {
            int result = -1;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDFORMULARIO", "System.Int32", "=", IDFORMULARIO.ToString()));

            CONTROLEACESSOCollection CONTROLEACESSOColl = new CONTROLEACESSOCollection();
            CONTROLEACESSOColl = CONTROLEACESSOP.ReadCollectionByParameter(RowRelatorio);

            if (CONTROLEACESSOColl.Count > 0)
                result = CONTROLEACESSOColl[0].IDCONTROLEACESSO;

            return result;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dg in dgvTelas.Rows)
            {
                dg.Cells[2].Value = "S";
                dg.Cells[3].Value = "S";
                dg.Cells[4].Value = "S";
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dg in dgvTelas.Rows)
            {
                dg.Cells[2].Value = "N";
                dg.Cells[3].Value = "N";
                dg.Cells[4].Value = "N";
            }
        }
      
        private void dgvTelas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTelas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string ValueCell = dgvTelas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                if (ValueCell != "S" && ValueCell != "N")
                {
                    MessageBox.Show("Digite apenas S ou N!");
                    dgvTelas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                }
                else
                    dgvTelas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ValueCell;
            }
            else
            {
                MessageBox.Show("Digite apenas S ou N!");
                dgvTelas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
            }
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            using (FrmControleAcessoTela frm = new FrmControleAcessoTela())
            {
                frm.ShowDialog();
                tabControlMarca.SelectTab(0);
            }
        }
    }
}
