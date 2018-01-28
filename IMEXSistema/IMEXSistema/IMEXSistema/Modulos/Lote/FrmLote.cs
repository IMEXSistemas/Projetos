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
using BmsSoftware.Modulos.Lote;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmLote : Form
    {
        Utility Util = new Utility();
        LOTEProvider LOTEPP = new LOTEProvider();
        LOTECollection LOTEColl = new LOTECollection();
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();
        LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl = new LIS_ESTOQUELOTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmLote()
        {
			//Teste git - 28/01/2018 - 10:03
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

        public int _IDLOTE = -1;
        public LOTEEntity Entity
        {
            get
            {
                string DESCRICAO = txtDescricao.Text;   //VARCHAR(50) CHARACTER SET ISO8859_1 COLLATE PT_BR,
                
                DateTime? DATAVALIDADE = null;
                if (maskedtxtData.Text != "  /  /")
                    DATAVALIDADE = Convert.ToDateTime(maskedtxtData.Text); //DATE,

                DateTime? DATAFABRICACAO = null;
                if (mkDataFabricacao.Text != "  /  /")
                    DATAFABRICACAO = Convert.ToDateTime(mkDataFabricacao.Text); //DATE,

                string OBSERVACAO = txtObservacao.Text;   //VARCHAR(500) CHARACTER SET ISO8859_1 COLLATE PT_BR
                string CODLOTE = txtCodigo.Text;
                return new LOTEEntity(_IDLOTE, DESCRICAO, DATAVALIDADE, OBSERVACAO, 
                                      CODLOTE, DATAFABRICACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDLOTE = value.IDLOTE;
                    txtDescricao.Text = value.DESCRICAO;

                    if (value.DATAVALIDADE != null)
                        maskedtxtData.Text = Convert.ToDateTime(value.DATAVALIDADE).ToString("dd/MM/yyyy");
                    else
                        maskedtxtData.Text = "  /  /";

                    if (value.DATAFABRICACAO != null)
                        mkDataFabricacao.Text = Convert.ToDateTime(value.DATAFABRICACAO).ToString("dd/MM/yyyy");
                    else
                        mkDataFabricacao.Text = "  /  /";

                    txtObservacao.Text = value.OBSERVACAO;
                    txtCodigo.Text = value.CODLOTE;
                    HistoricoLote(value.CODLOTE);
                    errorProvider1.Clear();
                }
                else
                {
                    _IDLOTE = -1;
                    HistoricoLote("");
                    txtDescricao.Text = string.Empty;
                    maskedtxtData.Text = "  /  /";
                    mkDataFabricacao.Text = "  /  /";
                    txtObservacao.Text = string.Empty;
                    txtCodigo.Text = string.Empty;
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
                    _IDLOTE =  LOTEPP.Save(Entity);
                    GetAllRegistros();
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }           
        }

        private void GetAllRegistros()
        {
            try
            {
                LOTEColl = LOTEPP.ReadCollectionByParameter(null, "DATAVALIDADE DESC");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LOTEColl;

                lblTotalPesquisa.Text = LOTEColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtDescricao.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            if (txtCodigo.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label5, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }            
            else if (mkDataFabricacao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(mkDataFabricacao.Text))
            {
                errorProvider1.SetError(label6, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (maskedtxtData.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (VerificaExisteLote(txtCodigo.Text) && _IDLOTE == -1)
            {
                string Msgerro = "Lote já Cadastrado!";
                errorProvider1.SetError(label5, Msgerro);
                Util.ExibirMSg(Msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtDescricao, "");

            return result;
        }

        private Boolean VerificaExisteLote(string CodLote)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote.ToString()));
                LOTECollection LOTEColl_2 = new LOTECollection();
                LOTEColl_2 = LOTEPP.ReadCollectionByParameter(RowRelatorio);
                
                if (LOTEColl_2.Count > 0)
                    result = true;

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return result;
            }
          
            
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelec.Image = Util.GetAddressImage(11);
            bntDateSelecFabri.Image = Util.GetAddressImage(11);
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnpdf2.Image = Util.GetAddressImage(17);
            btnExcel2.Image = Util.GetAddressImage(18);
            btnPrint2.Image = Util.GetAddressImage(19);

            GetToolStripButtonCadastro();
            GetAllRegistros();

            if (_IDLOTE != -1)
                Entity = LOTEPP.Read(_IDLOTE);
            else
                Entity = null;

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
            txtDescricao.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtDescricao.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDLOTE == -1)
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
                        LOTEPP.Delete(_IDLOTE);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllRegistros();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        MessageBox.Show("Erro Técnico: " + ex.Message);
                    }                 

                }
            }
        }

        private void HistoricoLote(string CodLote)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote.ToString()));
                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATA DESC");
                DataGriewHistorico.AutoGenerateColumns = false;
                DataGriewHistorico.DataSource = LIS_ESTOQUELOTEColl;

                label4.Text = "Total da pesquisa: " + LIS_ESTOQUELOTEColl.Count.ToString();

                PaintGrid();
            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_ESTOQUELOTEEntity item in LIS_ESTOQUELOTEColl)
                {
                    if (item.FLAGTIPO != null && item.FLAGTIPO.Trim() == "S")
                        DataGriewHistorico.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    else if (item.FLAGTIPO != null && item.FLAGTIPO.Trim() == "E")
                        DataGriewHistorico.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
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

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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
            if (LOTEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LOTEColl[indice].IDLOTE);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = LOTEPP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtDescricao.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            LOTEPP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllRegistros();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelec_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario.Location = new Point(230, 55);
            FormCalendario.Name = "FrmCalendario";
            FormCalendario.Text = "Calendário";
            FormCalendario.ResumeLayout(false);
            FormCalendario.Controls.Add(monthCalendar2);
            FormCalendario.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedtxtData.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecFabri_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar3";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(230, 55);
            FormCalendario3.Name = "FrmCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkDataFabricacao.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Lote";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lote");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
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
                RowRelatorio.Add(new RowsFiltro("DESCRICAO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("OBSERVACAO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LOTEColl = LOTEPP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LOTEColl;

                    lblTotalPesquisa.Text = LOTEColl.Count.ToString();                   
                }
                else
                {
                    LOTEColl = LOTEPP.ReadCollectionByParameter(null, "DATAVALIDADE DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LOTEColl;

                    lblTotalPesquisa.Text = LOTEColl.Count.ToString();
                }


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

        private void estoqueDoLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEstoqueLote frm = new FrmEstoqueLote())
            {
              //  frm._IDLOTE = _IDLOTE;
                frm.ShowDialog();
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            PaintGrid();
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LOTEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LOTEColl[rowindex].IDLOTE);

                    Entity = LOTEPP.Read(CodigoSelect);
                    tabControlMarca.SelectTab(0);
                    txtDescricao.Focus();
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodigoSelect = Convert.ToInt32(LOTEColl[rowindex].IDLOTE);
                            //Delete Pedido
                            LOTEPP.Delete(CodigoSelect);
                            GetAllRegistros();

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

        private void saldoLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSaldoLote frm = new FrmSaldoLote())
            {
                frm._CodLote = txtCodigo.Text;
                frm.ShowDialog();
            }
        }
    }
}
