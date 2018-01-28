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
using BmsSoftware.Modulos.Cadastros;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
using VVX;
using BmsSoftware.Modulos.Operacional;
using System.Diagnostics;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmCheque : Form
    {
        Utility Util = new Utility();

        CHEQUEProvider CHEQUEP = new CHEQUEProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        LIS_CHEQUEProvider LIS_CHEQUEP = new LIS_CHEQUEProvider();
        DESTINOCHEQUEProvider DESTINOCHEQUEP = new DESTINOCHEQUEProvider();

        LIS_CHEQUECollection LIS_CHEQUEColl = new LIS_CHEQUECollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        BANCOCollection BANCOColl = new BANCOCollection();
        STATUSCollection STATUSColl3 = new STATUSCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        int _IDCHEQUE = -1;
        int _IDDESTINOCHEQUE = -1;
        int? _IDCLIENTE = null;
        int? _IDFORNECEDOR = null;
        int? _IDCLIENTE2 = null;
        int? _IDFORNECEDOR2 = null;

        public int _idClienteSelec = -1;
        public decimal _valorcheque = 0;

        public FrmCheque()
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
        

        public CHEQUEEntity Entity
        {
            get
            {
               
              string NUMERO  = txtNCheque.Text;
              string AGENCIA = txtAgencia.Text;
              string CONTA = txtConta.Text;
              string DIGCONTA = txtDigConta.Text;
              decimal VALOR= Convert.ToDecimal(txtValorCheque.Text);
              DateTime ENTRADA  = Convert.ToDateTime(msktDataEntrada.Text);
              DateTime BOMPARA  = Convert.ToDateTime(msktDataReceb.Text);
              
                int?  IDCENTROCUSTO = null;
                if(Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int?  IDBANCO = null;
                if(Convert.ToInt32(cbBanco.SelectedValue) > 0)
                    IDBANCO = Convert.ToInt32(cbBanco.SelectedValue);

                int?  IDSTATUS = null;
                if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                    IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);

                int?  IDFUNCIONARIO = null;
                if(Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                string TIPORECEBIMENTO = "O";// C Cliente  - F Fornecedor  - O Outros
                if(rbCliente.Checked)
                     TIPORECEBIMENTO = "C";
                else if(rbFornecedor.Checked)
                     TIPORECEBIMENTO = "F";
                
                string NOMECLIENTEFORNEC = txtNomeClienteForn.Text;
                string TITULAR = txtOutrosTercTitulCheque.Text;
                string OBSERVACAOO = TxtObs.Text;

                   return new CHEQUEEntity(_IDCHEQUE, NUMERO, AGENCIA, CONTA, DIGCONTA, VALOR, ENTRADA, BOMPARA,
                                     IDCENTROCUSTO, IDBANCO, IDSTATUS, IDFUNCIONARIO, TIPORECEBIMENTO,
                                        NOMECLIENTEFORNEC, _IDCLIENTE, _IDFORNECEDOR, TITULAR, OBSERVACAOO);
            }
            set
            {

                if (value != null)
                {
                    _IDCHEQUE = value.IDCHEQUE;

                    if (value.IDCLIENTE != null)
                        _IDCLIENTE = Convert.ToInt32(value.IDCLIENTE);
                    else
                        _IDCLIENTE = null;

                    if (value.IDFORNECEDOR != null)
                        _IDFORNECEDOR = Convert.ToInt32(value.IDFORNECEDOR);
                    else
                        _IDFORNECEDOR = null;


                    txtNCheque.Text = value.NUMERO;
                    txtAgencia.Text = value.AGENCIA;
                    txtConta.Text = value.CONTA;
                    txtDigConta.Text = value.DIGCONTA;
                    txtValorCheque.Text = Convert.ToDecimal(value.VALOR).ToString("n2");
                    msktDataEntrada.Text = Convert.ToDateTime(value.ENTRADA).ToString("dd/MM/yyyy");
                    msktDataReceb.Text = Convert.ToDateTime(value.BOMPARA).ToString("dd/MM/yyyy");

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = Convert.ToInt32(value.IDCENTROCUSTO);
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    if (value.IDBANCO != null)
                        cbBanco.SelectedValue = Convert.ToInt32(value.IDBANCO);
                    else
                        cbBanco.SelectedIndex = 0;

                    if (value.IDSTATUS != null)
                        cbStatus.SelectedValue = Convert.ToInt32(value.IDSTATUS);
                    else
                        cbStatus.SelectedIndex = 0;

                    if (value.IDFUNCIONARIO != null)
                        cbFuncionario.SelectedValue = Convert.ToInt32(value.IDFUNCIONARIO);
                    else
                        cbFuncionario.SelectedIndex = 0;

                    if (value.TIPORECEBIMENTO == "C")  // C Cliente  - F Fornecedor  - O Outros
                        rbCliente.Checked = true;
                    else if (value.TIPORECEBIMENTO == "F")
                        rbFornecedor.Checked = true;
                    else if (value.TIPORECEBIMENTO == "O")
                        rbOutros.Checked = true;

                    txtNomeClienteForn.Text  = value.NOMECLIENTEFORNEC;
                    txtOutrosTercTitulCheque.Text = value.TITULAR; 
                    TxtObs.Text = value.OBSERVACAO;

                    errorProvider1.Clear();
                }
                else
                {
                   _IDCHEQUE = -1;

                   if (chkLimpaDados.Checked)
                   {
                       _IDCLIENTE = null;
                       _IDFORNECEDOR = null;

                       txtNCheque.Text = string.Empty;
                       txtAgencia.Text = string.Empty;
                       txtConta.Text = string.Empty;
                       txtDigConta.Text = string.Empty;
                       txtValorCheque.Text = "0,00";
                       msktDataEntrada.Text = DateTime.Now.ToString("dd/MM/yyyy");
                       msktDataReceb.Text = "  /  /";

                       cbCentroCusto.SelectedIndex = 0;
                       cbBanco.SelectedIndex = 0;
                       
                       cbFuncionario.SelectedIndex = 0;
                       rbCliente.Checked = false;
                       rbFornecedor.Checked = false;
                       rbOutros.Checked = true;

                       txtNomeClienteForn.Text = string.Empty;
                       txtOutrosTercTitulCheque.Text = string.Empty;
                       TxtObs.Text = string.Empty;

                       txtNomeClienteFornDestino.Enabled = true;
                   }

                    errorProvider1.Clear();
                }
            }
        }

        public DESTINOCHEQUEEntity Entity2
        {
            get
            {
                string TIPORECEBIMENTO = "O";// C Cliente  - F Fornecedor  - O Outros
                if (rbClienteDestino.Checked)
                    TIPORECEBIMENTO = "C";
                else if (rbFornecDestino.Checked)
                    TIPORECEBIMENTO = "F";

                string NOMEDESTINO = txtNomeClienteFornDestino.Text;
                string OBSERVACAO = txtObsDestino.Text;
                
                DateTime? Data = null;
                if (mkdDataDestino.Text != "  /  /")
                    Data = Convert.ToDateTime(mkdDataDestino.Text);


                return new DESTINOCHEQUEEntity(_IDDESTINOCHEQUE, _IDCHEQUE, _IDCLIENTE2, _IDFORNECEDOR2, OBSERVACAO,
                                               TIPORECEBIMENTO, NOMEDESTINO, Data);
            }
            set
            {

                if (value != null)
                {
                    _IDDESTINOCHEQUE = value.IDDESTINOCHEQUE;

                    if (value.IDCLIENTE != null)
                        _IDCLIENTE2 = Convert.ToInt32(value.IDCLIENTE);
                    else
                        _IDCLIENTE2 = null;

                    if (value.IDFORNECEDOR != null)
                        _IDFORNECEDOR2 = Convert.ToInt32(value.IDFORNECEDOR);
                    else
                        _IDFORNECEDOR2 = null;

                    txtNomeClienteFornDestino.Text = value.NOMEDESTINO;


                    if (value.TIPORECEBIMENTO == "C")  // C Cliente  - F Fornecedor  - O Outros
                        rbClienteDestino.Checked = true;
                    else if (value.TIPORECEBIMENTO == "F")
                        rbFornecDestino.Checked = true;
                    else if (value.TIPORECEBIMENTO == "O")
                        rbOutroDestino.Checked = true;

                    txtObsDestino.Text = value.OBSERVACAO;

                    if (value.DATA != null)
                        mkdDataDestino.Text = Convert.ToDateTime(value.DATA).ToString("dd/MM/yyyy");
                    else
                        mkdDataDestino.Text = "  /  /";
                        

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDESTINOCHEQUE = -1;

                    txtNomeClienteFornDestino.Text = string.Empty;
                    rbClienteDestino.Checked = false;
                    rbFornecDestino.Checked = false;
                    rbOutroDestino.Checked = true;
                    txtObsDestino.Text = string.Empty;
                    mkdDataDestino.Text = "  /  /";
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
                   _IDCHEQUE =  CHEQUEP.Save(Entity);

                    //Salva Destino
                   _IDDESTINOCHEQUE = DESTINOCHEQUEP.Save(Entity2);

                    tabControl.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                    if (!chkLimpaDados.Checked)
                        btnPesquisa_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro + " - " + ex.Message);

            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtNCheque.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNCheque, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEntrada.Text))
            {
                errorProvider1.SetError(msktDataEntrada, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataReceb.Text))
            {
                errorProvider1.SetError(msktDataReceb, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio == "S" && Convert.ToInt32(cbCentroCusto.SelectedValue) < 0)
            {
                errorProvider1.SetError(cbCentroCusto, ConfigMessage.Default.CampoObrigatorio);
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

            try
            {
              //  this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                btnCentroCusto.Image = Util.GetAddressImage(6);
                cbVendedor.Image = Util.GetAddressImage(6);
                btnCadStatus.Image = Util.GetAddressImage(6);
                btnCadBanco.Image = Util.GetAddressImage(6);
                bntDateSelecEntrada.Image = Util.GetAddressImage(11);
                btnDateSelcBompara.Image = Util.GetAddressImage(11);
                btnDataDestn.Image = Util.GetAddressImage(11);
                bntDateSelecInicial.Image = Util.GetAddressImage(11);
                bntDateSelecFinal.Image = Util.GetAddressImage(11);

                bntDateSelecInicial.Image = Util.GetAddressImage(22);
                bntDateSelecFinal.Image = Util.GetAddressImage(22);

                GetToolStripButtonCadastro();
                GetDropCentroCusto();
                GetDropCentroCusto2();
                GetDropStatusDuplicata();
                GetDropStatusDuplicata2();
                GetFuncionario();
                GetDropBanco();

                PreencheDropCamposPesquisa();
                PreencheDropTipoPesquisa();

                VerificaAcesso();

                Entity = null;
                Entity2 = null;

                if (_idClienteSelec > 0)
                {
                    rbCliente.Checked = true;
                    _IDCLIENTE = _idClienteSelec;
                    txtNomeClienteForn.Text = CLIENTEP.Read(_idClienteSelec).NOME;
                }

                txtValorCheque.Text = _valorcheque.ToString("n2");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControl.SelectTab(0);
            txtNCheque.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControl.SelectTab(0);
            txtNCheque.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDCHEQUE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl.SelectTab(2);
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
                        //Apagar Destino
                        DESTINOCHEQUEP.Delete(_IDDESTINOCHEQUE);

                        CHEQUEP.Delete(_IDCHEQUE);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
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
            tabControl.SelectTab(2);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(2);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( LIS_CHEQUEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_CHEQUEColl[rowindex].IDCHEQUE);

                    Entity = CHEQUEP.Read(CodigoSelect);

                    //Busca dados do destino
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCHEQUE", "System.Int32", "=", _IDCHEQUE.ToString()));
                    DESTINOCHEQUECollection DESTINOCHEQUEColl2 = new DESTINOCHEQUECollection();
                    DESTINOCHEQUEColl2 = DESTINOCHEQUEP.ReadCollectionByParameter(RowRelatorio);
                    if (DESTINOCHEQUEColl2.Count > 0)
                        Entity2 = DESTINOCHEQUEP.Read(DESTINOCHEQUEColl2[0].IDDESTINOCHEQUE);
                    else
                        Entity2 = null;

                    tabControl.SelectTab(0);
                    txtNCheque.Focus();
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
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }     

     

        private void FrmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "TxtObs" || this.ActiveControl.Name != "txtObsDestino")
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
            if (LIS_CHEQUEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_CHEQUEColl[indice].IDCHEQUE);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CHEQUEP.Read(CodigoSelect);

                    tabControl.SelectTab(0);
                    txtNCheque.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CHEQUEP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void txtValorCheque_Enter(object sender, EventArgs e)
        {
            if (txtValorCheque.Text == "0,00")
                txtValorCheque.Text = string.Empty;
        }

        private void txtValorCheque_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorCheque.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCheque.Text))
                {
                    errorProvider1.SetError(txtValorCheque, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorCheque.Text);
                    txtValorCheque.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorCheque, "");
                }
            }
            else
            {
                txtValorCheque.Text = "0,00";
                errorProvider1.SetError(txtValorCheque, "");
            }
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecEntrada_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(220, 160);
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
            msktDataEntrada.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void btnDateSelcBompara_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar3";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(220, 160);
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
            msktDataReceb.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void btnCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetDropCentroCusto();
                GetDropCentroCusto2();
                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void GetDropCentroCusto()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCusto.DisplayMember = "DESCRICAO";
            cbCentroCusto.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCusto.DataSource = CENTROCUSTOSColl;

            cbCentroCusto.SelectedIndex = 0;
        }

        private void GetDropCentroCusto2()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCusto2.DisplayMember = "DESCRICAO";
            cbCentroCusto2.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto2.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCusto2.DataSource = CENTROCUSTOSColl;
            cbCentroCusto2.SelectedIndex = 0;
        }

        private void btnCadBanco_Click(object sender, EventArgs e)
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

        private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

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

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                GetDropStatusDuplicata();
                GetDropStatusDuplicata2();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void GetDropStatusDuplicata()
        {
            try
            {
                //15 Cheque
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "15");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

                STATUSProvider STATUSP = new STATUSProvider();
                STATUSCollection STATUSColl2 = new STATUSCollection();
                STATUSColl2 = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus.DataSource = STATUSColl2;

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUS";

                cbStatus.SelectedValue = 59;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }      

        private void GetDropStatusDuplicata2()
        {
            try
            {
                STATUSProvider STATUSP = new STATUSProvider();

                //15 Cheque
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "15"));
                STATUSColl3 = STATUSP.ReadCollectionByParameter(RowRelatorio, "NOME");

                cbStatus2.DisplayMember = "NOME";
                cbStatus2.ValueMember = "IDSTATUS";

                STATUSEntity STATUSETy = new STATUSEntity();
                STATUSETy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSETy.IDSTATUS = -1;
                STATUSColl3.Add(STATUSETy);

                Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus2.DisplayMember);

                STATUSColl3.Sort(comparer.Comparer);
                cbStatus2.DataSource = STATUSColl3;

                cbStatus2.SelectedValue = 59;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
        }

        private void rbCliente_Click(object sender, EventArgs e)
        {
            using (FrmSearchCliente frm = new FrmSearchCliente())
            {
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    txtNomeClienteForn.Text = CLIENTEP.Read(Convert.ToInt32(result)).NOME;
                    txtOutrosTercTitulCheque.Text = txtNomeClienteForn.Text;
                    _IDCLIENTE = result;
                    _IDFORNECEDOR = null;
                }
            }
        }

        private void rbFornecedor_Click(object sender, EventArgs e)
        {
            using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
            {
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    txtNomeClienteForn.Text = FORNECEDORP.Read(Convert.ToInt32(result)).NOME;
                    txtOutrosTercTitulCheque.Text = txtNomeClienteForn.Text;
                   _IDCLIENTE = null;
                   _IDFORNECEDOR = result;

                }
            }
        }

        private void rdOutros_Click(object sender, EventArgs e)
        {
           _IDCLIENTE = null;
           _IDFORNECEDOR = null;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("BOMPARA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("BOMPARA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if(Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                
                LIS_CHEQUEColl = LIS_CHEQUEP.ReadCollectionByParameter(Filtro, "NUMERO");

                lblTotalPesquisa.Text = LIS_CHEQUEColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_CHEQUEEntity LIS_CHEQUETy = new LIS_CHEQUEEntity();
                LIS_CHEQUETy.VALOR = SumTotalPesquisa("VALOR");
                LIS_CHEQUEColl.Add(LIS_CHEQUETy);


                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CHEQUEColl;

               
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_CHEQUEEntity item in LIS_CHEQUEColl)
            {
                if (NomeCampo == "VALOR")
                    valortotal += Convert.ToDecimal(item.VALOR);
            }

            return valortotal;
        }


        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControl.SelectedIndex == 1)
                {
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
            }
            else
            {
                errorProvider1.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_CHEQUEColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CHEQUEColl;
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

                Filtro.Insert(Filtro.Count, filtroProfile);

                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("BOMPARA", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("BOMPARA", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbCentroCusto2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }                

                LIS_CHEQUEColl = LIS_CHEQUEP.ReadCollectionByParameter(Filtro, "NUMERO");
                lblTotalPesquisa.Text = LIS_CHEQUEColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_CHEQUEEntity LIS_CHEQUETy = new LIS_CHEQUEEntity();
                LIS_CHEQUETy.VALOR = SumTotalPesquisa("VALOR");
                LIS_CHEQUEColl.Add(LIS_CHEQUETy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CHEQUEColl;

               
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_CHEQUEColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_CHEQUEColl.Count.ToString();
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            ImprimirListaGeral();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            using (FrmSearchCliente frm = new FrmSearchCliente())
            {
                txtNomeClienteFornDestino.Text = string.Empty;
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    txtNomeClienteFornDestino.Text = CLIENTEP.Read(Convert.ToInt32(result)).NOME;
                    txtOutrosTercTitulCheque.Text = txtNomeClienteForn.Text;
                    _IDCLIENTE2 = result;
                    _IDFORNECEDOR2 = null;
                }
            }

            _IDFORNECEDOR2 = null;

            txtNomeClienteFornDestino.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
            {
                txtNomeClienteFornDestino.Text = string.Empty;
                frm.ShowDialog();
                var result = frm.Result;

                if (result > 0)
                {
                    txtNomeClienteFornDestino.Text = FORNECEDORP.Read(Convert.ToInt32(result)).NOME;
                    txtOutrosTercTitulCheque.Text = txtNomeClienteForn.Text;
                    _IDCLIENTE2 = null;
                    _IDFORNECEDOR2 = result;

                }
            }

            txtNomeClienteFornDestino.Enabled = false;
        }

        private void rbOutroDestino_Click(object sender, EventArgs e)
        {
            txtNomeClienteFornDestino.Text = string.Empty;
            _IDCLIENTE2 = null;
            _IDFORNECEDOR2 = null;

            txtNomeClienteFornDestino.Enabled = true;
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void btnDataDestn_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(220, 160);
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
            mkdDataDestino.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        private void mkdDataDestino_Validating(object sender, CancelEventArgs e)
        {

            MaskedTextBox TextBoxSelec = sender as MaskedTextBox;
            if (TextBoxSelec.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }               
            }
            
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_CHEQUEColl.Count > 0)
            {
                //Retira o ultimo registro
                LIS_CHEQUEColl.RemoveAt(LIS_CHEQUEColl.Count - 1);

                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {

                    Phydeaux.Utilities.DynamicComparer<LIS_CHEQUEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CHEQUEEntity>(orderBy);

                    LIS_CHEQUEColl.Sort(comparer.Comparer);

                    //Colocando somatorio no final da lista
                    LIS_CHEQUEEntity LIS_CHEQUETy = new LIS_CHEQUEEntity();
                    LIS_CHEQUETy.VALOR = SumTotalPesquisa("VALOR");
                    LIS_CHEQUEColl.Add(LIS_CHEQUETy);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_CHEQUEColl;
                    lblObsField.Text = string.Empty;
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void entradaNoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCHEQUE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl.SelectTab(2);
            }
            else
            {
                if (Convert.ToDecimal(txtValorCheque.Text) <= 0)
                {
                    errorProvider1.SetError(txtValorCheque, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Deseja realmente lançar o valor de " + txtValorCheque.Text + " no caixa?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                        EntradaCaixa();
                }
            }
        }

        private void EntradaCaixa()
        {
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = 1;
            CaixaTy.VALOR = Convert.ToDecimal(txtValorCheque.Text);
            CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            CaixaTy.IDTIPOMOVCAIXA = 1;// Credito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = "CH" + txtNCheque.Text;
            
            string tipoC = "Outros";
            if (rbCliente.Checked)
                tipoC = "Cliente";
            else if (rbFornecedor.Checked)
                tipoC = "Fornecedor";

            CaixaTy.OBSERVACAO = "Cheque nº " + txtNCheque.Text + " " + tipoC + " " + txtNomeClienteForn.Text +  "  Banco: " + cbBanco.Text;

            try
            {
                CaixaP.Save(CaixaTy);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }

        private void destinoDoChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCHEQUE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                ImprimirDestinoCheque();
            }
        }

        private void ImprimirDestinoCheque()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument2;


            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                objPrintPreview.Document = printDocument2;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }

        }

        string RelatorioTitulo = string.Empty;
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //inicio Cabeçalho
            ConfigReportStandard config = new ConfigReportStandard();

            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 530, 35, 180, 80);

                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMEFANTASIA;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE + " Bairro: " + EMPRESATy.BAIRRO + " CEP: " + EMPRESATy.CEP, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);

            if (CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == "N" || CONFISISTEMAP.Read(1).FLAGCPFCNPJPEDIDO.TrimEnd() == string.Empty)
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


            //Titulo
            e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

            //campos a serem impressos 
            e.Graphics.DrawString("Nº Cheque:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
            e.Graphics.DrawString("Entrada:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
            e.Graphics.DrawString("Bom Para:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, 170);
            e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
            e.Graphics.DrawString("Valor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
            e.Graphics.DrawString("Banco", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, 170);
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

            config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

            e.Graphics.DrawString(txtNCheque.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda, 200);
            e.Graphics.DrawString(msktDataEntrada.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, 200);
            e.Graphics.DrawString(msktDataReceb.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 200);
            e.Graphics.DrawString(cbStatus.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, 200);
            e.Graphics.DrawString(txtValorCheque.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, 200);
            e.Graphics.DrawString(cbBanco.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 500, 200);

            string TipoRecebimento = "Outros";
            if (rbFornecedor.Checked)
                TipoRecebimento = "Fornecedor";
            else if (rbCliente.Checked)
                TipoRecebimento = "Cliente";
            e.Graphics.DrawString("Tipo de Recebimento: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 230);
            e.Graphics.DrawString(TipoRecebimento, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, 230);
            e.Graphics.DrawString(txtNomeClienteForn.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda, 255);

            e.Graphics.DrawString("Tipo de Destino: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 285);
            string TipoDestino = "Outros";
            if (rbFornecDestino.Checked)
                TipoDestino = "Fornecedor";
            else if (rbClienteDestino.Checked)
                TipoDestino = "Cliente";
            e.Graphics.DrawString(TipoDestino, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, 285);

            e.Graphics.DrawString("Data ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 285);
            e.Graphics.DrawString(mkdDataDestino.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, 285);
            e.Graphics.DrawString(txtNomeClienteFornDestino.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda, 305);

        }

        int IndexRegistro = 0;
        Int32 paginaAtual = 0;
        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void chequePorDestinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChequeDestino Frm = new FrmChequeDestino();
            Frm.ShowDialog();
        }

        public MonthCalendar monthCalendar6 = new MonthCalendar();
        Form FormCalendario6 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar6.Name = "monthCalendar6";
            monthCalendar6.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar6_DateSelected);

            FormCalendario6.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario6.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario6.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario6.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario6.Location = new Point(230, 55);
            FormCalendario6.Name = "FrmCalendario6";
            FormCalendario6.Text = "Calendário";
            FormCalendario6.ResumeLayout(false);
            FormCalendario6.Controls.Add(monthCalendar6);
            FormCalendario6.ShowDialog();
        }

        private void monthCalendar6_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar6.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario6.Close();
        }

        public MonthCalendar monthCalendar7 = new MonthCalendar();
        Form FormCalendario7 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar7.Name = "monthCalendar7";
            monthCalendar7.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar7_DateSelected);

            FormCalendario7.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario7.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario7.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario7.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario7.Location = new Point(230, 55);
            FormCalendario7.Name = "FrmCalendario7";
            FormCalendario7.Text = "Calendário";
            FormCalendario7.ResumeLayout(false);
            FormCalendario7.Controls.Add(monthCalendar7);
            FormCalendario7.ShowDialog();
        }

        private void monthCalendar7_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar7.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario7.Close();
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
                frm.TituloSelec = "Relação de Cheques";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
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
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("AGENCIA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CONTA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("DESCCENTROCUSTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMESTATUS", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFUNC", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTEFORNEC", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_CHEQUEColl = LIS_CHEQUEP.ReadCollectionByParameter(RowRelatorio, "NUMERO");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_CHEQUEColl;

                    lblTotalPesquisa.Text = LIS_CHEQUEColl.Count.ToString();
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

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }




    }
}
