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
using BMSworks.UI;
using System.IO;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Telemarketing
{
    public partial class FrmGrupoAgendaTLMK : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmGrupoAgendaTLMK()
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

        GRUPOAGENDATLMKProvider GRUPOAGENDATLMKP = new GRUPOAGENDATLMKProvider();
        GRUPOAGENDATLMKCollection GRUPOAGENDATLMKColl = new GRUPOAGENDATLMKCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        Utility Util = new Utility();

        public int _IDGRUPOAGENDATLMK = -1;

        public GRUPOAGENDATLMKEntity Entity
        {
            get
            {
                string NOME = txtNome.Text.TrimEnd().TrimStart();
                string OBSERVACAO = txtObservacao.Text;

                return new GRUPOAGENDATLMKEntity(_IDGRUPOAGENDATLMK, NOME, OBSERVACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDGRUPOAGENDATLMK = value.IDGRUPOAGENDATLMK;
                    txtNome.Text = value.NOME;
                    txtObservacao.Text = value.OBSERVACAO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDGRUPOAGENDATLMK = -1;
                    txtNome.Text = string.Empty;
                    txtObservacao.Text = string.Empty;

                    errorProvider1.Clear();
                }


            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlStatus.SelectTab(0);
            txtNome.Focus();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDGRUPOAGENDATLMK = GRUPOAGENDATLMKP.Save(Entity);
                        GetAllStatus();
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.Clear();



            return result;
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            Grava();
            this.Cursor = Cursors.Default;
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void GetAllStatus()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                GRUPOAGENDATLMKColl = GRUPOAGENDATLMKP.ReadCollectionByParameter(null);
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = GRUPOAGENDATLMKColl;

                lblTotalPesquisa.Text = GRUPOAGENDATLMKColl.Count.ToString();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlStatus.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDGRUPOAGENDATLMK == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlStatus.SelectTab(1);
            }
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);
               

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        GRUPOAGENDATLMKP.Delete(_IDGRUPOAGENDATLMK);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllStatus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }

                }
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlStatus.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlStatus.SelectTab(1);
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStatus_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetAllStatus();
            GetToolStripButtonCadastro();

            if (_IDGRUPOAGENDATLMK != -1)
                Entity = GRUPOAGENDATLMKP.Read(_IDGRUPOAGENDATLMK);

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();

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

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (GRUPOAGENDATLMKColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(GRUPOAGENDATLMKColl[rowindex].IDGRUPOAGENDATLMK);
                    tabControlStatus.SelectTab(0);

                    Entity = GRUPOAGENDATLMKP.Read(CodigoSelect);

                    txtNome.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + GRUPOAGENDATLMKColl[rowindex].IDGRUPOAGENDATLMK.ToString().PadLeft(6, '0') + " - " + GRUPOAGENDATLMKColl[rowindex].NOME,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(GRUPOAGENDATLMKColl[rowindex].IDGRUPOAGENDATLMK);
                                //Delete Pedido
                                GRUPOAGENDATLMKP.Delete(CodigoSelect);

                                GetAllStatus();

                                Entity = null;
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
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

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GRUPOAGENDATLMKColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity>(orderBy);

                GRUPOAGENDATLMKColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = GRUPOAGENDATLMKColl;
            }
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GRUPOAGENDATLMKColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlStatus.SelectTab(1);
            }
            else
            {
                ImprimirListaGeral();
            }

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
           
        }       

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
          
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GRUPOAGENDATLMKColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlStatus.SelectTab(1);
            }
            else
            {
                ImprimirListaGeral();
            }
        }

        private void FrmStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmStatus_KeyDown(object sender, KeyEventArgs e)
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
            if (GRUPOAGENDATLMKColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(GRUPOAGENDATLMKColl[indice].IDGRUPOAGENDATLMK);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = GRUPOAGENDATLMKP.Read(CodigoSelect);

                    tabControlStatus.SelectTab(0);
                    txtNome.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            GRUPOAGENDATLMKP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllStatus();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void txtPesquisaRapida_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOME", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("OBSERVACAO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                
                GRUPOAGENDATLMKColl = GRUPOAGENDATLMKP.ReadCollectionByParameter(RowRelatorio);
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = GRUPOAGENDATLMKColl;
                lblTotalPesquisa.Text = GRUPOAGENDATLMKColl.Count.ToString();

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

        private void btnCor_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Status do Telemarketing");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Status do Telemarketing";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
           
        }

        private void DataGriewDados_Paint(object sender, PaintEventArgs e)
        {
            
        }

       
    }
}
