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
using BmsSoftware.Modulos.Operacional;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Hospedagem
{
    public partial class FrmReserva2 : Form
    {
        Utility Util = new Utility();

        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        LIS_RESERVACollection LIS_RESERVAColl = new LIS_RESERVACollection();
        LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl = new LIS_PRODUTORESERVACollection();

        PRODUTORESERVAProvider PRODUTORESERVAP = new PRODUTORESERVAProvider();
        LIS_PRODUTORESERVAProvider LIS_PRODUTORESERVAP = new LIS_PRODUTORESERVAProvider();
        LIS_RESERVAProvider LIS_RESERVAP = new LIS_RESERVAProvider();        
        RESERVAProvider RESERVAP = new RESERVAProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();        

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public string DataConsultaSelec = string.Empty;
        public FrmReserva2()
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
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

       public int _IDRESERVA = -1;
        public RESERVAEntity Entity
        {
            get
            {

                DateTime? DATARETIRADA = Convert.ToDateTime(dateTimePickerRetirada.Text);  // DATE
                DateTime? DATAENTREGA = Convert.ToDateTime(dateTimePickerEntrega.Text);  // DATE
                string OBSERVACAO = txtObservacao.Text;   //VARCHAR(1000) CHARACTER SET ISO8859_1
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                decimal VLTOTAL = Convert.ToDecimal(txtTotalProduto.Text);
                DateTime? DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                decimal VLPago = Convert.ToDecimal(txtVlPago.Text);

                return new RESERVAEntity(_IDRESERVA, DATARETIRADA, DATAENTREGA, IDCLIENTE, OBSERVACAO,
                                        IDSTATUS, VLTOTAL, DATAEMISSAO, VLPago);

            }
            set
            {

                if (value != null)
                {
                    _IDRESERVA = Convert.ToInt32(value.IDRESERVA);
                    ListaReservaProduto(_IDRESERVA);
                    lblControle.Text = _IDRESERVA.ToString().PadLeft(6, '0');
                    dateTimePickerRetirada.Text = Convert.ToDateTime(value.DATARETIRADA).ToString("dd/MM/yyyy");
                    dateTimePickerEntrega.Text = Convert.ToDateTime(value.DATAENTREGA).ToString("dd/MM/yyyy");
                    txtObservacao.Text = value.OBSERVACAO;
                    cbCliente.SelectedValue = value.IDCLIENTE;
                    cbStatus.SelectedValue = value.IDSTATUS;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAEMISSAO).ToString("dd/MM/yyyy");
                    txtVlPago.Text = Convert.ToDecimal(value.VLPAGO).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDRESERVA = -1;
                    ListaReservaProduto(_IDRESERVA);
                    lblControle.Text = "000000";
                    txtObservacao.Text = string.Empty;
                    cbCliente.SelectedValue = -1;
                    msktDataEmissao.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
                    txtVlPago.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }


        int _IDPRODUTORESERVA = -1;
        public PRODUTORESERVAEntity Entity2
        {
            get
            {
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                int QUANTIDADE = Convert.ToInt32(txtQuanProduto.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
                decimal VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                string FLAGNOVARESERVA = chkNaoReserva.Checked ? "N" : "S";

                return new PRODUTORESERVAEntity(_IDPRODUTORESERVA, _IDRESERVA, QUANTIDADE,
                                                VALORUNITARIO, VALORTOTAL, IDPRODUTO, FLAGNOVARESERVA);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTORESERVA = value.IDPRODUTORESERVA;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuanProduto.Text = Convert.ToInt32(value.QUANT).ToString();
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VLUNITARIO).ToString("n2");
                    decimal VALORTOTAL = Convert.ToDecimal(value.VLUNITARIO) * Convert.ToDecimal(value.QUANT);
                    txtValorTotal.Text = Convert.ToDecimal(VALORTOTAL).ToString("n2");
                    chkNaoReserva.Checked = value.FLAGNOVARESERVA.TrimEnd() == "N" ? true : false;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTORESERVA = -1;
                    cbProduto.SelectedValue = 1;
                    txtQuanProduto.Text = "1";
                    txtValorUnitProd.Text = "0,00";
                    txtValorTotal.Text = "0,00";
                    chkNaoReserva.Checked = false;
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
                    _IDRESERVA = RESERVAP.Save(Entity);
                    lblControle.Text = _IDRESERVA.ToString().PadLeft(6, '0');
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
            if (cbCliente.SelectedIndex == 0)
            {
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            //Validações da Gravação		
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (Convert.ToDateTime(dateTimePickerRetirada.Text) > Convert.ToDateTime(dateTimePickerEntrega.Text))
            {
                string MSG = "Data de retirada não pode maior que a data da entrega";
                errorProvider1.SetError(lblSaida, MSG);
                errorProvider1.SetError(lblDataEntrada, MSG);
                MessageBox.Show(MSG);
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.CampoObrigatorio);
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

            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            
            GetToolStripButtonCadastro();
            GetDropProdutos();
            GetDropStatus();    
            GetDropStatus2();
            GetDropCliente();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnCadProduto.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);


            if (_IDRESERVA != -1)
            {
                Entity = RESERVAP.Read(_IDRESERVA);
            }
            else if (DataConsultaSelec != string.Empty)
            {
                tabControlMarca.SelectTab(1);
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATARETIRADA", "System.DateTime", "=", Util.ConverStringDateSearch(DataConsultaSelec)));
                LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(RowRelatorio, "IDRESERVA DESC");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_RESERVAColl;
            }

            this.Cursor = Cursors.Default;

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
            }
  
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
            tabControlMarca.SelectTab(0);            
        }      

        
        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);

            cbProduto.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if ( _IDRESERVA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
            }
            //Validações de Delete
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
                        foreach (LIS_PRODUTORESERVAEntity item in LIS_PRODUTORESERVAColl)
                        {
                            PRODUTORESERVAP.Delete(Convert.ToInt32(item.IDPRODUTORESERVA));
                        }

                        RESERVAP.Delete(_IDRESERVA);
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
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_RESERVAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_RESERVAColl[rowindex].IDRESERVA);
                    Entity = RESERVAP.Read(CodSelect);
                    tabControlMarca.SelectTab(0);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_RESERVAColl[rowindex].IDRESERVA);
                            //Excluir Produto da reserva
                            RowRelatorio.Clear();
                            RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", CodSelect.ToString()));
                            LIS_PRODUTORESERVAColl = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);
                            foreach (LIS_PRODUTORESERVAEntity item in LIS_PRODUTORESERVAColl)
                            {
                                PRODUTORESERVAP.Delete(Convert.ToInt32(item.IDPRODUTORESERVA));
                            }
                          
                            RESERVAP.Delete(CodSelect);
                            Entity = null;
                            btnPesquisa_Click(null, null);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                            MessageBox.Show("Erro Técnico: " + ex.Message);

                        }
                    }
                }
                else if (ColumnSelecionada == 2)//Imprimir
                {

                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    CodSelect = Convert.ToInt32(LIS_RESERVAColl[rowindex].IDRESERVA);
                    Entity = RESERVAP.Read(CodSelect);
                    reservaDeRoupaToolStripMenuItem_Click(null, null);
                    this.Cursor = Cursors.Default;
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
            reservaDeRoupaToolStripMenuItem_Click(null, null);
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
           
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            if (LIS_RESERVAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_RESERVAColl[indice].IDRESERVA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = RESERVAP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            RESERVAP.Delete(CodigoSelect);
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

        private void txtNMesa_Validating(object sender, CancelEventArgs e)
        {
                  
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnCadQuarto_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();
                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutos()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));
            PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

            cbProduto.DisplayMember = "NOMEPRODUTO";
            cbProduto.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

            PRODUTOSColl.Sort(comparer.Comparer);
            cbProduto.DataSource = PRODUTOSColl;

            cbProduto.SelectedIndex = 0;
        }       

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                if (!chkpesquisaperiodo.Checked)
                {
                    if (dateTimePickerEntradaFiltro.Text != "  /  /" && dateTimePickerSaidaFiltro.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(dateTimePickerEntradaFiltro.Text) && ValidacoesLibrary.ValidaTipoDateTime(dateTimePickerSaidaFiltro.Text))
                    {
                        filtroProfile = new RowsFiltro("DATARETIRADA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerEntradaFiltro.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATAENTREGA", "System.DateTime", "<=", Util.ConverStringDateSearch(dateTimePickerSaidaFiltro.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }
                }

                if(Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                     filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32" , "=", cbStatus2.SelectedValue.ToString());
                     Filtro.Insert(Filtro.Count, filtroProfile);
                }


                LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(Filtro, "IDRESERVA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_RESERVAColl;

                lblTotalPesquisa.Text = LIS_RESERVAColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
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
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_RESERVAColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_RESERVAColl;
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

                if (!chkpesquisaperiodo.Checked)
                {
                    if (dateTimePickerEntradaFiltro.Text != "  /  /" && dateTimePickerSaidaFiltro.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(dateTimePickerEntradaFiltro.Text) && ValidacoesLibrary.ValidaTipoDateTime(dateTimePickerSaidaFiltro.Text))
                    {
                        filtroProfile = new RowsFiltro("DATARETIRADA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerEntradaFiltro.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATAENTREGA", "System.DateTime", "<=", Util.ConverStringDateSearch(dateTimePickerSaidaFiltro.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }
                }

                 if(Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                     filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32" , "=", cbStatus2.SelectedValue.ToString());
                     Filtro.Insert(Filtro.Count, filtroProfile);
                }

              
                LIS_RESERVAColl = LIS_RESERVAP.ReadCollectionByParameter(Filtro, "IDRESERVA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_RESERVAColl;

                lblTotalPesquisa.Text = LIS_RESERVAColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
        {
            using (FrmCliente frm = new FrmCliente())
            {

                int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                frm.CodClienteSelec = CodSelec;
                frm.ShowDialog();

                GetDropCliente();

                cbCliente.SelectedValue = CodSelec;
            }
        }

        private void GetDropCliente()
        {
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

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cbCliente.SelectedValue) > 0)
                txtTelefone1.Text =  CLIENTEP.Read(Convert.ToInt32(cbCliente.SelectedValue)).TELEFONE1;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                GetDropStatus();
                 GetDropStatus2();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void GetDropStatus()
        {
            //11 Pedido de Venda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "16"); //16 Reserva
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

          private void GetDropStatus2()
        {
            //11 Pedido de Venda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "16"); //16 Reserva
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSCollection STATUSColl = new STATUSCollection();
            STATUSProvider STATUSP = new STATUSProvider();
            STATUSColl= STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus2.DisplayMember = "NOME";
            cbStatus2.ValueMember = "IDSTATUS";

            STATUSEntity STATUSTy = new STATUSEntity();
            STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
            STATUSTy.IDSTATUS = -1;
            STATUSColl.Add(STATUSTy);

            Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus2.DisplayMember);

            STATUSColl.Sort(comparer.Comparer);
            cbStatus2.DataSource = STATUSColl;
            cbStatus2.SelectedIndex = 0;
                         
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(TextBoxSelec.Text))
            {
                errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }          
        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
            {
                errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(TextBoxSelec.Text);
                TextBoxSelec.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnitProd, "");
                SomaProduto();
            }
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
                        txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void btnAddProduto_Click(object sender, EventArgs e)
        {
            GravaProduto();
        }

        private void GravaProduto()
        {
            try
            {
                if (ValidacoesProdutos() && Validacoes())
                {
                    _IDRESERVA = RESERVAP.Save(Entity);
                    lblControle.Text = _IDRESERVA.ToString().PadLeft(6, '0');
                    PRODUTORESERVAP.Save(Entity2);
                    ListaReservaProduto(_IDRESERVA);

                    //Salva Reserva
                    RESERVAP.Save(Entity);

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    cbProduto.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void ListaReservaProduto(int IDRESERVA)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDRESERVA", "System.Int32", "=", IDRESERVA.ToString()));
            LIS_PRODUTORESERVAColl = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            DGDadosProduto.AutoGenerateColumns = false;
            DGDadosProduto.DataSource = LIS_PRODUTORESERVAColl;
            lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTORESERVAColl.Count.ToString();

            SomaTotalProduto();
        }

        private void SomaTotalProduto()
        {
            decimal total = 0;
            foreach (LIS_PRODUTORESERVAEntity item in LIS_PRODUTORESERVAColl)
            {
                total+=Convert.ToDecimal(item.VLTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
        }

        private Boolean ValidacoesProdutos()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
            {
                errorProvider1.SetError(lblquarto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(label54, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(txtQuanProduto.Text) < 1)
            {
                errorProvider1.SetError(label59, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanProduto.Text))
            {
                errorProvider1.SetError(label59, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (VerificaProdutoReserva())
            {
                errorProvider1.SetError(lblquarto, ConfigMessage.Default.CampoObrigatorio);
                result = false;

            }
            else if (VerificaProdutoReserva2())
            {
                errorProvider1.SetError(lblquarto, ConfigMessage.Default.CampoObrigatorio);
                result = false;

            }  
            else
                errorProvider1.Clear();


            return result;
        }       

        private Boolean VerificaProdutoReserva()
        {
            Boolean result = false;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "<>", cbCliente.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerRetirada.Text)));
            RowRelatorio.Add(new RowsFiltro("DATARETIRADA", "System.DateTime", "<=", Util.ConverStringDateSearch(dateTimePickerEntrega.Text)));            

            LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl2 = new LIS_PRODUTORESERVACollection();
            LIS_PRODUTORESERVAColl2 = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_PRODUTORESERVAColl2.Count > 0)
            {
                MessageBox.Show("Existe reserva para o produto " + cbProduto.Text + ", Data da Retirada: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATARETIRADA).ToString("dd/MM/yyyy") + " e entrega: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATAENTREGA).ToString("dd/MM/yyyy") + " - Controle nº: " + LIS_PRODUTORESERVAColl2[0].IDRESERVA.ToString().PadLeft(6, '0') + "!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);


                result = true;
            }

            return result;
        }

        private Boolean VerificaProdutoReserva2()
        {
            Boolean result = false;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "<>", cbCliente.SelectedValue.ToString()));
            RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", Util.ConverStringDateSearch(dateTimePickerRetirada.Text)));
            RowRelatorio.Add(new RowsFiltro("FLAGNOVARESERVA", "System.String", "=", "N"));

            LIS_PRODUTORESERVACollection LIS_PRODUTORESERVAColl2 = new LIS_PRODUTORESERVACollection();
            LIS_PRODUTORESERVAColl2 = LIS_PRODUTORESERVAP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_PRODUTORESERVAColl2.Count > 0)
            {
                MessageBox.Show("O produto: " + cbProduto.Text + ", está reservado exclusivamente até a data de entrega: " + Convert.ToDateTime(LIS_PRODUTORESERVAColl2[0].DATAENTREGA).ToString("dd/MM/yyyy") + " - Controle nº: " + LIS_PRODUTORESERVAColl2[0].IDRESERVA.ToString().PadLeft(6, '0') + "!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);


                result = true;
            }

            return result;
        }

        private void DGDadosProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTORESERVAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                //if (ColumnSelecionada == 0)//Editar
                //{
                //    CodSelect = Convert.ToInt32(LIS_PRODUTORESERVAColl[rowindex].IDPRODUTORESERVA);
                //    Entity2 = PRODUTORESERVAP.Read(CodSelect);

                //}
                //else 
                if (ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_PRODUTORESERVAColl[rowindex].IDPRODUTORESERVA);
                                PRODUTORESERVAP.Delete(CodSelect);
                                ListaReservaProduto(_IDRESERVA);
                                Entity2 = null;
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Desejar excluir todos os produtos?",
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);



            if (dr == DialogResult.Yes)
            {
                try
                {
                    foreach (LIS_PRODUTORESERVAEntity item in LIS_PRODUTORESERVAColl)
                    {
                        PRODUTORESERVAP.Delete(Convert.ToInt32(item.IDPRODUTORESERVA));
                    }

                    ListaReservaProduto(_IDRESERVA);
                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                }
            }
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);
                txtValorUnitProd.Text = ValorVenda.ToString("n2");
                SomaProduto();
            }
        }

        private void txtQuanProduto_Leave(object sender, EventArgs e)
        {
            SomaProduto();
        }

        private void SomaProduto()
        {
            try
            {
                txtValorTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o cálculo!");
            }
        }

        private void reservaDeRoupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDRESERVA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                RESERVAP.Save(Entity);
                ImprimirReceitaReport();
            }
        }

        private void ImprimirReceitaReport()
        {
            using (FrmRelaReserva_2 frm = new FrmRelaReserva_2())
            {
                frm.vlpagoSelec = txtVlPago.Text;
                frm.codclienteSelec = Convert.ToInt32(cbCliente.SelectedValue);
                frm.controleSelec = lblControle.Text;
                frm.IDRESERVASELEC = _IDRESERVA;
                frm.ShowDialog();
            }
        }

        private void txtVlPago_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
            {
                errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(TextBoxSelec.Text);
                TextBoxSelec.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnitProd, "");
                SomaProduto();
            }
        }

        private void listaDeReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaReserva Frm = new FrmListaReserva();
            Frm.ShowDialog();
        }

        private void txtVlPago_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtVlPago_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtVlPago.Text != "0,00" && txtVlPago.Text != string.Empty)
            {
                DialogResult dr = MessageBox.Show("Desejar dar entrada no caixa de: " + txtVlPago.Text + " ?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmEntradaCaixa frm = new FrmEntradaCaixa())
                    {
                        frm.ValorPago = Convert.ToDecimal(txtVlPago.Text);
                        frm.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                        frm.IDRESERVA = _IDRESERVA;
                        frm.ShowDialog();
                    }

                }
            }
            else
            {
                MessageBox.Show("Valor deve ser maior que 0?");
            }
        }

        private void txtVlPago_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Duplo clique para dar entrada no caixa";
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Reserva");

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
                frm.TituloSelec = "Lista de Reserva";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void adicionarAluguelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDRESERVA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
            }
            else
            {
                using (FrmControleAluguel frm = new FrmControleAluguel())
                {
                    frm._IDRESULTADO = _IDRESERVA;
                    frm.ShowDialog();

                }
            }
        }
    }
}
