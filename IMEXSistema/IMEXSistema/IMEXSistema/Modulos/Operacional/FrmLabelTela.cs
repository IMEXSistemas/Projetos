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
using BmsSoftware.Classes.BMSworks.UI;
using VVX;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmLabelTela : Form
    {
        Utility Util = new Utility();

        LABELTELAProvider LABELTELAP = new LABELTELAProvider();
        LIS_LABELTELAProvider LIS_LABELTELAP = new LIS_LABELTELAProvider();
        LIS_LABELTELACollection LIS_LABELTELAColl = new LIS_LABELTELACollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmLabelTela()
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

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        public int _IDLABELTELA = -1;
        public LABELTELAEntity Entity
        {
            get
            {
                string NOMELABEL = txtNomeLabel.Text;
                string TEXTOLABEL = txtTextoLabel.Text;
                
                int IDFORMULARIO = -1;
                if (Convert.ToInt32(cbTelas.SelectedValue) > 0)
                    IDFORMULARIO =Convert.ToInt32(cbTelas.SelectedValue);

                return new LABELTELAEntity(_IDLABELTELA, NOMELABEL, TEXTOLABEL, IDFORMULARIO);
            }
            set
            {

                if (value != null)
                {
                    _IDLABELTELA = value.IDLABELTELA;
                    txtNomeLabel.Text = value.NOMELABEL;
                    txtTextoLabel.Text = value.TEXTOLABEL;

                    if (value.IDFORMULARIO != null)
                        cbTelas.SelectedValue = value.IDFORMULARIO;
                    else
                        cbTelas.SelectedValue = -1;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDLABELTELA = -1;
                    txtNomeLabel.Text = string.Empty;
                    txtTextoLabel.Text = string.Empty;
                    cbTelas.SelectedValue = -1;
                    errorProvider1.Clear();
                }
            }
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
                    _IDLABELTELA =  LABELTELAP.Save(Entity);
                    GetAllFormulario();
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void GetAllFormulario()
        {
            try
            {
                LIS_LABELTELAColl = LIS_LABELTELAP.ReadCollectionByParameter(null, "NOMETELA");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_LABELTELAColl;

                lblTotalPesquisa.Text = LIS_LABELTELAColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNomeLabel.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtTextoLabel.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbTelas.SelectedValue) < 1)
            {
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtNomeLabel, "");

            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetToolStripButtonCadastro();
            GetAllFormulario();
            GetDropFormulario();
            GetDropFormulario2();

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
            btnSeach.Image = Util.GetAddressImage(20);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnTela.Image = Util.GetAddressImage(6);

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
            txtNomeLabel.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNomeLabel.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDLABELTELA == -1)
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
                        //Deleta os controle de acesso referente ao nivel
                        DeleteControleAcesso(_IDLABELTELA);

                        LABELTELAP.Delete(_IDLABELTELA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                       GetAllFormulario();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }

        private void DeleteControleAcesso(int IDNIVEL)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString()));
            CONTROLEACESSOCollection CONTROLEACESSOColl = new CONTROLEACESSOCollection();
            CONTROLEACESSOProvider CONTROLEACESSOP = new CONTROLEACESSOProvider();
            CONTROLEACESSOColl = CONTROLEACESSOP.ReadCollectionByParameter(RowRelatorio);

            foreach (CONTROLEACESSOEntity item in CONTROLEACESSOColl)
            {
                CONTROLEACESSOP.Delete(item.IDCONTROLEACESSO);
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

             int rowindex = e.RowIndex;
             if (LIS_LABELTELAColl.Count > 0 && rowindex > -1)
             {
                 int ColumnSelecionada = e.ColumnIndex;
                 int CodigoSelect = -1;

                 if (ColumnSelecionada == 0)//Editar
                 {
                     CodigoSelect = Convert.ToInt32(LIS_LABELTELAColl[rowindex].IDLABELTELA);
                     Entity = LABELTELAP.Read(CodigoSelect);
                     tabControlMarca.SelectTab(0);                     
                 }
                 else if (ColumnSelecionada == 1)//Excluir
                 {

                     if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                     {
                         DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                         if (dr == DialogResult.Yes)
                         {
                             try
                             {
                                 CodigoSelect = Convert.ToInt32(LIS_LABELTELAColl[rowindex].IDLABELTELA);
                                 LABELTELAP.Delete(CodigoSelect);
                                 Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                 GetAllFormulario();
                             }
                             catch (Exception ex)
                             {
                                 MessageBox.Show("Erro técnico: " + ex.Message);
                                 MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                             }
                         }
                     }
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Formulários");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

      
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Formulários");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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
            if (LIS_LABELTELAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_LABELTELAColl[indice].IDFORMULARIO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = LABELTELAP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNomeLabel.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            LABELTELAP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllFormulario();
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
           
        }    

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapidaDrop()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                if (Convert.ToInt32(cbtelas2.SelectedValue) > 0)
                {
                    RowRelatorio.Add(new RowsFiltro("IDFORMULARIO", "System.Int32", "=", cbtelas2.SelectedValue.ToString(), "or"));
                }

                LIS_LABELTELAColl = LIS_LABELTELAP.ReadCollectionByParameter(RowRelatorio, "NOMETELA");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_LABELTELAColl;

                lblTotalPesquisa.Text = LIS_LABELTELAColl.Count.ToString();


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMELABEL", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("TEXTOLABEL", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMETELA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDLABELTELA", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }
                
                LIS_LABELTELAColl = LIS_LABELTELAP.ReadCollectionByParameter(RowRelatorio, "NOMETELA");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_LABELTELAColl;

                    lblTotalPesquisa.Text = LIS_LABELTELAColl.Count.ToString();


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Formulários";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Formulários");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnTela_Click(object sender, EventArgs e)
        {
            using (FrmFormulario frm = new FrmFormulario())
            {
                int CodSelec = Convert.ToInt32(cbTelas.SelectedValue);
                frm._IDFORMULARIO = CodSelec;
                frm.ShowDialog();
                GetDropFormulario();
                GetDropFormulario2();
                cbTelas.SelectedValue = CodSelec;
            }
        }

        private void GetDropFormulario2()
        {
            FORMULARIOCollection FORMULARIOColl = new FORMULARIOCollection();
            FORMULARIOProvider FORMULARIOP = new FORMULARIOProvider();
            FORMULARIOColl = FORMULARIOP.ReadCollectionByParameter(null, "NOMETELA");

            cbtelas2.DisplayMember = "NOMETELA";
            cbtelas2.ValueMember = "IDFORMULARIO";

            FORMULARIOEntity FORMULARIOTy = new FORMULARIOEntity();
            FORMULARIOTy.NOMETELA = ConfigMessage.Default.MsgDrop;
            FORMULARIOTy.IDFORMULARIO = -1;
            FORMULARIOColl.Add(FORMULARIOTy);

            Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity>(cbtelas2.DisplayMember);

            FORMULARIOColl.Sort(comparer.Comparer);
            cbtelas2.DataSource = FORMULARIOColl;

            cbtelas2.SelectedIndex = 0;
        }

        private void GetDropFormulario()
        {
            FORMULARIOCollection FORMULARIOColl = new FORMULARIOCollection();
            FORMULARIOProvider FORMULARIOP = new FORMULARIOProvider();
            FORMULARIOColl = FORMULARIOP.ReadCollectionByParameter(null, "NOMETELA");

            cbTelas.DisplayMember = "NOMETELA";
            cbTelas.ValueMember = "IDFORMULARIO";

            FORMULARIOEntity FORMULARIOTy = new FORMULARIOEntity();
            FORMULARIOTy.NOMETELA = ConfigMessage.Default.MsgDrop;
            FORMULARIOTy.IDFORMULARIO = -1;
            FORMULARIOColl.Add(FORMULARIOTy);

            Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity>(cbTelas.DisplayMember);

            FORMULARIOColl.Sort(comparer.Comparer);
            cbTelas.DataSource = FORMULARIOColl;

            cbTelas.SelectedIndex = 0;
        }

        private void cbtelas2_SelectedValueChanged(object sender, EventArgs e)
        {
            PesquisaRapidaDrop();
        }
    }
}
