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
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Lote;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmEstoqueLote : Form
    {
        Utility Util = new Utility();
        ESTOQUELOTEProvider ESTOQUELOTEP = new ESTOQUELOTEProvider();
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();
        LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl = new LIS_ESTOQUELOTECollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        LOTECollection LOTEColl = new LOTECollection();
        LOTEProvider LOTEP = new LOTEProvider();

        public string _NumeroDoc = string.Empty;

        public FrmEstoqueLote()
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

        public int _IDESTOQUELOTE = -1;
        public int _IDLOTE = -1;
        public ESTOQUELOTEEntity Entity
        {
            get
            {
                Decimal QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                string NUMERODOC = txtNotaFiscal.Text;

                DateTime? DATA = null;
                if (maskedtxtData.Text != "  /  /")
                    DATA = Convert.ToDateTime(maskedtxtData.Text); //DATE,

                string FLAGTIPO = RbSaida.Checked ? "S" : "E";
                string FLAGATIVO = rbSim.Checked ? "S" : "N";
                string OBSERVACAO = txtObservacao.Text;

                return new ESTOQUELOTEEntity(_IDESTOQUELOTE, QUANTIDADE, _IDLOTE, IDPRODUTO, NUMERODOC,
                                             DATA, FLAGTIPO, FLAGATIVO, OBSERVACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDESTOQUELOTE = Convert.ToInt32(value.IDESTOQUELOTE);
                    _IDLOTE = Convert.ToInt32(value.IDLOTE);
                    BuscaCodLote(_IDLOTE);

                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtNotaFiscal.Text = value.NUMERODOC;

                    if (value.DATA != null)
                        maskedtxtData.Text = Convert.ToDateTime(value.DATA).ToString("dd/MM/yyyy");
                    else
                        maskedtxtData.Text = "  /  /";

                    RbSaida.Checked = value.FLAGTIPO.Trim() == "S" ? true : false;
                    RbEntrada.Checked = !RbSaida.Checked;
                    rbSim.Checked = value.FLAGATIVO.Trim() == "S" ? true : false;
                    rbNao.Checked = !rbSim.Checked;
                    txtObservacao.Text = value.OBSERVACAO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDESTOQUELOTE = -1;
                    _IDLOTE = -1;
                    txtNumeroLote.Text = string.Empty;
                    txtQuanProduto.Text = "1";
                    cbProduto.SelectedValue = -1;
                    txtNotaFiscal.Text =  string.Empty;
                    maskedtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    RbSaida.Checked =  false;
                    rbSim.Checked = true ;
                    txtObservacao.Text = string.Empty;
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
                    _IDESTOQUELOTE =  ESTOQUELOTEP.Save(Entity);
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
                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(null, "DATA DESC");
                DataGriewDados.AutoGenerateColumns = false;

                DataGriewDados.DataSource = LIS_ESTOQUELOTEColl;

                lblTotalPesquisa.Text = LIS_ESTOQUELOTEColl.Count.ToString();               
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
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    else if (item.FLAGTIPO != null && item.FLAGTIPO.Trim() == "E")
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;

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

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNotaFiscal.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (txtNumeroLote.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }            
            else if (maskedtxtData.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            {
                errorProvider1.SetError(label14, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (!LoteExiste(txtNumeroLote.Text))
            {
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
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

            bntDateSelec.Image = Util.GetAddressImage(11);          
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnLote.Image = Util.GetAddressImage(6);

            GetToolStripButtonCadastro();
            GetAllRegistros();
            GetDropProduto();

            if (_IDESTOQUELOTE != -1)
                Entity = ESTOQUELOTEP.Read(_IDESTOQUELOTE);
            else
                Entity = null;

            if (_NumeroDoc != string.Empty)
            {
                txtPesquisaRapida.Text = _NumeroDoc;
                PesquisaRapida();
                tabControlMarca.SelectTab(1);
            }

            VerificaAcesso();

            this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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
            txtNotaFiscal.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNotaFiscal.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDESTOQUELOTE == -1)
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
                        ESTOQUELOTEP.Delete(_IDESTOQUELOTE);
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
            if (LIS_ESTOQUELOTEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_ESTOQUELOTEColl[rowindex].IDESTOQUELOTE);

                    Entity = ESTOQUELOTEP.Read(CodigoSelect);
                    tabControlMarca.SelectTab(0);
                    txtNotaFiscal.Focus();
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
                                CodigoSelect = Convert.ToInt32(LIS_ESTOQUELOTEColl[rowindex].IDESTOQUELOTE);
                                //Delete Pedido
                                ESTOQUELOTEP.Delete(CodigoSelect);
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
            if (LIS_ESTOQUELOTEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_ESTOQUELOTEColl[indice].IDESTOQUELOTE);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = ESTOQUELOTEP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNotaFiscal.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            ESTOQUELOTEP.Delete(CodigoSelect);
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
            maskedtxtData.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Estoque do Lote";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Estoque do Lote");

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
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEPRODUTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATA DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ESTOQUELOTEColl;

                    lblTotalPesquisa.Text = LIS_ESTOQUELOTEColl.Count.ToString();                   
                }
                else
                {
                    LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(null, "DATA DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ESTOQUELOTEColl;

                    lblTotalPesquisa.Text = LIS_ESTOQUELOTEColl.Count.ToString();
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

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int Codproduto = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                frm.ShowDialog();
                GetDropProduto();
                cbProduto.SelectedValue = Codproduto;
            }
        }

        private void GetDropProduto()
        {
            try
            {
                PRODUTOSCollection ProdutoColl = new PRODUTOSCollection();
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                ProdutoColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

                cbProduto.DisplayMember = "NOMEPRODUTO";
                cbProduto.ValueMember = "IDPRODUTO";

                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                ProdutoColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

                ProdutoColl.Sort(comparer.Comparer);
                cbProduto.DataSource = ProdutoColl;

                cbProduto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtNumeroLote_Leave(object sender, EventArgs e)
        {
            LoteExiste(txtNumeroLote.Text);
        }

        private void BuscaCodLote(int IDLOTE)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDLOTE", "System.Int32", "=", IDLOTE.ToString()));
                LOTEColl.Clear();
                LOTEColl = LOTEP.ReadCollectionByParameter(RowRelatorio);

                if (LOTEColl.Count > 0)
                    txtNumeroLote.Text  = LOTEColl[0].CODLOTE;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private Boolean LoteExiste(string CodLote)
        {
            Boolean Result = false;

            try
            {
                if (CodLote.Trim() != string.Empty)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote));
                    LOTEColl.Clear();
                    LOTEColl = LOTEP.ReadCollectionByParameter(RowRelatorio);

                    if (LOTEColl.Count > 0)
                    {
                        _IDLOTE = LOTEColl[0].IDLOTE;
                        Result = true;
                    }
                    else
                    {
                        MessageBox.Show("Lote: " + CodLote + " Não Existe!");
                    }
                }
               
                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return Result;
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o produto ou pressione Ctrl+E para pesquisar.";
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
           
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                        //txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            PaintGrid();
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }

        private void saldoDoLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSaldoLote frm = new FrmSaldoLote())
            {
                frm._CodLote = txtNumeroLote.Text;
                frm.ShowDialog();
            }
        }
    }
}
