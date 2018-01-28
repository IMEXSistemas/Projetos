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
using BmsSoftware.Modulos.Operacional;
using System.IO;
using BMSSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Classes.BMSworks.UI;
using winfit.Modulos.Outros;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmAgenda : Form
    {
        AGENDAProvider AGENDAP = new AGENDAProvider();
        LIS_AGENDAProvider LIS_AGENDAP = new LIS_AGENDAProvider();

        GRUPOAGENDACollection GRUPOAGENDAColl = new GRUPOAGENDACollection();
        LIS_AGENDACollection LIS_AGENDAColl = new LIS_AGENDACollection();
        LIS_AGENDACollection LIS_AGENDAColl0 = new LIS_AGENDACollection();
        LIS_AGENDACollection LIS_AGENDAColl1 = new LIS_AGENDACollection();
        LIS_AGENDACollection LIS_AGENDAColl2 = new LIS_AGENDACollection();
        LIS_AGENDACollection LIS_AGENDAColl3 = new LIS_AGENDACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection(); 

        Utility Util = new Utility();
        FrmLogin FrmLogin = new FrmLogin();

        public FrmAgenda()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        int _IDAGENDA = -1;
        public AGENDAEntity Entity
        {
            get
            {
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                int IDGRUPOAGENDA = -1;
                DateTime DATA = Convert.ToDateTime(maskedtxtData.Text);
                string HORA = cbhora.Text ;
                int IDEVENTO = -1;
                string COMENTARIO = txtComentario.Text;
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                int? IDCLIENTE = null;
                if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                    IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

                return new AGENDAEntity(_IDAGENDA, IDSTATUS, DATA, HORA, IDGRUPOAGENDA, IDEVENTO, 
                                        COMENTARIO,IDFUNCIONARIO, IDCLIENTE);
            }
            set
            {
                if (value != null)
                {
                    _IDAGENDA = value.IDAGENDA;
                    lblControle.Text = "Controle: " + _IDAGENDA.ToString().PadLeft(6, '0');
                    cbStatus.SelectedValue = Convert.ToInt32(value.IDSTATUS);
                   maskedtxtData.Text = Convert.ToDateTime(value.DATA).ToString("dd/MM/yyyy");
                    
                    string hora  =  value.HORA;
                    cbhora.SelectedIndex = cbhora.FindString(hora);
                   
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    txtComentario.Text = value.COMENTARIO;

                    if (value.IDCLIENTE != null)
                        cbCliente.SelectedValue = value.IDCLIENTE;
                    else
                        cbCliente.SelectedValue = -1;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDAGENDA = -1;
                    lblControle.Text = "Controle: 000000"; 
                    cbStatus.SelectedIndex = 0;
                  
                    maskedtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbhora.SelectedIndex = 0;

                    //Busca o Funcionario logado
                    USUARIOEntity USUARIOTY = new USUARIOEntity();
                    USUARIOProvider USUARIOP = new USUARIOProvider();
                    USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                    cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;

                    txtComentario.Text = string.Empty;
                    cbCliente.SelectedValue = -1;
                    errorProvider1.Clear();
                }
            }
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


        private void FrmAgenda_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetToolStripButtonCadastro();
            GetStatus();           
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            GetFuncionario();
            GetCliente();

            bntDateSelec.Image = Util.GetAddressImage(11);       
            btnCadPosicao.Image = Util.GetAddressImage(6);
            btnFuncionario.Image = Util.GetAddressImage(6);
            btnCadCliente.Image = Util.GetAddressImage(6);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            bntDateSelec.Image = Util.GetAddressImage(22);
            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            lblDataAtual.Text = DateTime.Now.ToLongDateString();
         
            FiltraAgendaDia(0);           
            VerificaAcesso();

            cbhora.SelectedIndex = 0;

            this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void GetCliente()
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            CLIENTECollection CLIENTEColl = new CLIENTECollection();
            CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null, "NOME");

            cbCliente.DisplayMember = "NOME";
            cbCliente.ValueMember = "IDCLIENTE";

            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
            CLIENTETy.NOME = ConfigMessage.Default.MsgDrop;
            CLIENTETy.IDCLIENTE = -1;
            CLIENTEColl.Add(CLIENTETy);

            Phydeaux.Utilities.DynamicComparer<CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLIENTEEntity>(cbCliente.DisplayMember);

            CLIENTEColl.Sort(comparer.Comparer);
            cbCliente.DataSource = CLIENTEColl;

            cbCliente.SelectedIndex = 0;
        }

        private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            Filtro.Clear();
            Filtro.Add(new RowsFiltro("FLAGEXIBIRAGENDA", "System.String", "=", "S"));
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(Filtro, "NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncionario.DataSource = FUNCIONARIOColl;

            cbFuncionario.SelectedIndex = 0;
        }

        private void FiltraAgendaDia(int Dias)
        {
           // DateTime Datual = DateTime.Now.AddDays(Dias);
            DateTime Datual = Convert.ToDateTime(lblDataAtual.Text).AddDays(Dias);
            lblDataAtual.Text = Datual.ToLongDateString();
            string Date = Util.ConverStringDateSearch(Datual.ToString("dd/MM/yyyy"));
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "=", Date.ToString()));

            LIS_AGENDAColl0.Clear();
            LIS_AGENDAColl0 = LIS_AGENDAP.ReadCollectionByParameter(RowRelatorio, "HORA");
            dataGridView0.AutoGenerateColumns = false;
            dataGridView0.DataSource = LIS_AGENDAColl0;
        }       

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PreencheDropCamposPesquisa()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("descricao", typeof(string)));
            list.Columns.Add(new DataColumn("nomecampo_tipo", typeof(string)));

            for (int i = 0; i < DataGriewDados.ColumnCount  ; i++)
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

        private void GetStatus()
        {
            //7 Agenda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.String", "=", "7");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }     
       
        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);
            TSBCalendario.Image = Util.GetAddressImage(10);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);
            TSBCalendario.ToolTipText = Util.GetToolTipButton(7);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
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

        private void btnCadPosicao_Click(object sender, EventArgs e)
        {

            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                GetStatus();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void btnCadGrupo_Click(object sender, EventArgs e)
        {
            
        }
      
        private void maskedtxtData_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text) && maskedtxtData.Text !="  /  /")
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(maskedtxtData, "");
            }
        }

        private void FrmAgenda_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void cbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbGrupoAgenda_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }
        
        private void cbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbGrupoAgenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void btnCadEvento_Click(object sender, EventArgs e)
        {
          
        }

        private void feriadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmFeriado frm = new FrmFeriado())
            {
                frm.ShowDialog();
            }
        }

        private void TSBCalendario_Click(object sender, EventArgs e)
        {
            using (FrmFeriado frm = new FrmFeriado())
            {
                frm.ShowDialog();
            }
        }

        private void FrmAgenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtComentario")
                {
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                    this.Focus();
                }

            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerFeriado())
            {
                string MsgFeriado = "Data selecionada é um feriado, deseja continuar?";
                DialogResult dr = MessageBox.Show(MsgFeriado,
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava(); this.Cursor = Cursors.Default;
                }
                else
                    maskedtxtData.Focus();
            }
            else
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
                Grava();
                this.Cursor = Cursors.Default;
            }
        }

        private Boolean VerFeriado()
        {
            Boolean result = false;

            if (maskedtxtData.Text != "  /  /")
            {

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

                //formata data para pesquisa.
                string DataSelecionada = Convert.ToDateTime(maskedtxtData.Text).ToString("dd/MM");
                DataSelecionada = DataSelecionada + "/0001";

                //Formata para yyyy/MM/dd
                DataSelecionada = Util.ConverStringDateSearch(DataSelecionada); ;

                RowsFiltroCollection RowFeriado = new RowsFiltroCollection();
                RowFeriado.Add(new RowsFiltro("DATA", "System.DateTime", "=", DataSelecionada));
                FERIADOProvider FERIADOP = new FERIADOProvider();
                FERIADOCollection FeriadoColl = FERIADOP.ReadCollectionByParameter(RowFeriado);

                if (FeriadoColl.Count > 0)
                    result = true;
            }

            return result;
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDAGENDA = AGENDAP.Save(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                  

                    FiltraAgendaDia(0);
                   
                    if (_IDAGENDA != -1)
                        btnPesquisa_Click(null, null);

                    Entity = null;

                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;
            
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgDataInvalida, "Red");
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }			
            else if (cbhora.SelectedIndex == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(cbhora, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }         
            else if (cbFuncionario.SelectedIndex == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if(VerificaExisteAgenda(cbhora.Text, cbFuncionario.Text))
            {
                string Msg = "Já existe compromisso nesta data e hora para o funcionário!";
                Util.ExibirMSg(Msg, "Red");
                errorProvider1.SetError(cbhora, Msg);
                errorProvider1.SetError(cbFuncionario, Msg);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean VerificaExisteAgenda(string hora, string nomefuncionario)
        {
            Boolean result = false;

            try
            {
                LIS_AGENDACollection LIS_AGENDAColl3 = new LIS_AGENDACollection();
                string DataAtual = Convert.ToDateTime(lblDataAtual.Text).ToString("dd/MM/yyyy");
                string Date = Util.ConverStringDateSearch(DataAtual);

                Filtro.Clear();
                Filtro.Add(new RowsFiltro("DATA", "System.DateTime", "=", Date.ToString()));
                Filtro.Add(new RowsFiltro("HORA", "System.String", "=", hora));
                Filtro.Add(new RowsFiltro("NOMEFUNCIONARIO", "System.String", "=", nomefuncionario));

                if (_IDAGENDA != -1)
                    Filtro.Add(new RowsFiltro("IDAGENDA", "System.String", "<>", _IDAGENDA.ToString()));

                LIS_AGENDAColl3.Clear();
                LIS_AGENDAColl3 = LIS_AGENDAP.ReadCollectionByParameter(Filtro, "NOMEFUNCIONARIO, HORA");

                if (LIS_AGENDAColl3.Count > 0)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }        


        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if (VerFeriado())
            {
                string MsgFeriado = "Data selecionada é um feriado, deseja continuar?";
                DialogResult dr = MessageBox.Show(MsgFeriado,
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava(); this.Cursor = Cursors.Default;
                }
                else
                    maskedtxtData.Focus();
            }
            else
            {
                CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava(); this.Cursor = Cursors.Default;
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlAgenda.SelectTab(0);
            cbStatus.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlAgenda.SelectTab(0);
            cbStatus.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDAGENDA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlAgenda.SelectTab(1);
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
                        AGENDAP.Delete(_IDAGENDA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;

                        FiltraAgendaDia(1);
                        FiltraAgendaDia(-1);
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro técnico: " + ex.Message);                        
                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlAgenda.SelectTab(1);
            txtCriterioPesquisa.Focus();

        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (_IDAGENDA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlAgenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmRelatAgenda frm = new FrmRelatAgenda())
                {
                    frm.titulo = _IDAGENDA.ToString().PadLeft(6, '0');
                    frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.ShowDialog();
                }

            }
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlAgenda.SelectTab(1);
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DATA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_AGENDAColl = LIS_AGENDAP.ReadCollectionByParameter(null, "DATA DESC");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAColl;

                lblTotalPesquisa.Text = LIS_AGENDAColl.Count.ToString();
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
            try
            {
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_AGENDAColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_AGENDAColl;
                }

                // Retorna o tipo de campo para pesquisa Ex.: String, Integer, Date...
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

                    if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                    {
                        filtroProfile = new RowsFiltro("DATA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    Filtro.Insert(Filtro.Count, filtroProfile);

                    LIS_AGENDAColl = LIS_AGENDAP.ReadCollectionByParameter(Filtro);
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_AGENDAColl;

                    lblTotalPesquisa.Text = LIS_AGENDAColl.Count.ToString();
                }
                else
                {
                    MessageBox.Show(ConfigMessage.Default.searchFieldType);
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                    txtCriterioPesquisa.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_AGENDAColl1.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_AGENDAColl1[rowindex].IDAGENDA);

                    Entity = AGENDAP.Read(CodigoSelect);

                    tabControlAgenda.SelectTab(0);
                    maskedtxtData.Focus();
                }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_AGENDAColl2.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_AGENDAColl2[rowindex].IDAGENDA);

                    Entity = AGENDAP.Read(CodigoSelect);

                    tabControlAgenda.SelectTab(0);
                    maskedtxtData.Focus();
                }
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_AGENDAColl3.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_AGENDAColl3[rowindex].IDAGENDA);

                    Entity = AGENDAP.Read(CodigoSelect);

                    tabControlAgenda.SelectTab(0);
                    maskedtxtData.Focus();
                }
            }
        }

        private void dataGridView0_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            int CodSelect = -1;
            if (LIS_AGENDAColl0.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_AGENDAColl0[rowindex].IDAGENDA);
                    Entity = AGENDAP.Read(CodSelect);

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_AGENDAColl0[rowindex].IDAGENDA);
                            AGENDAP.Delete(CodSelect);
                            FiltraAgendaDia(1);
                            FiltraAgendaDia(-1);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            Entity = null;
                        }
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
                else if (ColumnSelecionada == 2)//Email
                {
                    CodSelect = Convert.ToInt32(LIS_AGENDAColl0[rowindex].IDCLIENTE);
                    using (FrmEnviarEmail frm = new FrmEnviarEmail())
                    {
                        frm._IDCLIENTE = CodSelect;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }
       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
      
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_AGENDAColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_AGENDAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_AGENDAEntity>(orderBy);

                    LIS_AGENDAColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_AGENDAColl;
                    lblTotalPesquisa.Text = string.Empty;
                }
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_AGENDAColl.Clear();
            DataGriewDados.DataSource = null;

            lblTotalPesquisa.Text = LIS_AGENDAColl.Count.ToString();
        }

        private void dataGridView0_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_AGENDAColl.Count > 0)
            {
                string orderBy = dataGridView0.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_AGENDAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_AGENDAEntity>(orderBy);

                    LIS_AGENDAColl0.Sort(comparer.Comparer);

                    dataGridView0.DataSource = null;
                    dataGridView0.DataSource = LIS_AGENDAColl0;
                }
            }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_AGENDAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_AGENDAColl[indice].IDAGENDA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = AGENDAP.Read(CodigoSelect);
                    
                    tabControlAgenda.SelectTab(0);
                    maskedtxtData.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            AGENDAP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            btnPesquisa_Click(null, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void btnFuncionario_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm._IDFUNCIONARIO = CodSelec;
                frm.ShowDialog();
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
          
        }

        private void linkAvanca_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FiltraAgendaDia(1);
        }

        private void linkRetorna_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FiltraAgendaDia(-1);
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        public MonthCalendar monthCalendar5 = new MonthCalendar();
        Form FormCalendario5 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar5.Name = "monthCalendar5";
            monthCalendar5.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar5_DateSelected);

            FormCalendario5.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario5.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario5.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario5.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario5.Location = new Point(230, 55);
            FormCalendario5.Name = "FrmCalendario5";
            FormCalendario5.Text = "Calendário";
            FormCalendario5.ResumeLayout(false);
            FormCalendario5.Controls.Add(monthCalendar5);
            FormCalendario5.ShowDialog();
        }

        private void monthCalendar5_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar5.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario5.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Agenda: " + lblDataAtual.Text);

            //PrintDGV PRt = new PrintDGV();
            //PRt.Print_DataGridView(dataGridView0, RelatorioTitulo, this.Name);

           
            using (FrmRelatAgendaLote frm = new FrmRelatAgendaLote())
            {
                frm.LIS_AGENDAColl = LIS_AGENDAColl0;
                frm.ShowDialog();
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_AGENDAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlAgenda.SelectTab(1);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void linkLabel1_Leave(object sender, EventArgs e)
        {

        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {

            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
            {
                using (FrmCliente frm = new FrmCliente())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;
                    frm.ShowDialog();
                    GetCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
            else
            {
                using (FrmCliente2 frm = new FrmCliente2())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;
                    frm.ShowDialog();
                    GetCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDAGENDA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlAgenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmRelatAgenda frm = new FrmRelatAgenda())
                {
                    frm.titulo = _IDAGENDA.ToString().PadLeft(6, '0');
                    frm.idcliente = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.ShowDialog();
                }
                
                
            }
        }

        private void únicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void agendaEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmRelatAgendaLote frm = new FrmRelatAgendaLote())
            {
                frm.LIS_AGENDAColl = LIS_AGENDAColl;
                frm.ShowDialog();
            }

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cbCliente.SelectedValue) > 0)
            {
                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTETy = CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue));
                if (CLIENTETy != null)
                    lblTelefone.Text = "Telefone(s): " + CLIENTETy.TELEFONE1 + " / " + CLIENTETy.TELEFONE2;
                else
                    lblTelefone.Text = "Telefone(s): ";
            }
            else
            {
                lblTelefone.Text = "Telefone(s): ";
            }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            FiltraAgendaDia(1);
        }

        private void btnVolta_Click(object sender, EventArgs e)
        {
            FiltraAgendaDia(-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FrmRelatAgendaLote frm = new FrmRelatAgendaLote())
            {
                frm.LIS_AGENDAColl = LIS_AGENDAColl0;
                frm.ShowDialog();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (Agenda2 frm = new Agenda2())
            {
                frm.DataAtual = lblDataAtual.Text;
                frm.ShowDialog();
                if (frm.CodControle != -1)
                {
                    Entity = AGENDAP.Read(frm.CodControle);
                }
                else
                {
                    cbhora.SelectedIndex = cbhora.FindString(frm.HoraSelec);
                    cbFuncionario.SelectedIndex = cbFuncionario.FindString(frm.FuncSelec);
                    maskedtxtData.Text = frm.DataSelec;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Agenda";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEnviarEmail frm = new FrmEnviarEmail())
            {
                frm._IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                frm.ShowDialog();
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_AGENDAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_AGENDAColl[rowindex].IDAGENDA);
                    Entity = AGENDAP.Read(CodigoSelect);
                    tabControlAgenda.SelectTab(0);
                    maskedtxtData.Focus();

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
                                CodigoSelect = Convert.ToInt32(LIS_AGENDAColl[rowindex].IDAGENDA);
                                //Delete Pedido
                                AGENDAP.Delete(CodigoSelect);

                                btnPesquisa_Click(null, null);

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

       
    }
}
