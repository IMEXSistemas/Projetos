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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Classes.BMSworks.UI;
using VVX;
using BmsSoftware.Modulos.Relatorio;
using System.Globalization;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmNotaFiscalServico : Form
    {
        Utility Util = new Utility();
        SERVICONPSProvider SERVICONPSP = new SERVICONPSProvider();
        NOTASERVICOProvider NOTASERVICOP = new NOTASERVICOProvider();
        LIS_NOTASERVICOProvider LIS_NOTASERVICOP = new LIS_NOTASERVICOProvider();
        LIS_SERVICONPSProvider LIS_SERVICONPSP = new LIS_SERVICONPSProvider();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();

        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        LIS_SERVICONPSCollection LIS_SERVICONPSColl = new LIS_SERVICONPSCollection();
        LIS_NOTASERVICOCollection LIS_NOTASERVICOColl = new LIS_NOTASERVICOCollection();
        SERVICOProvider SERVICOP = new SERVICOProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        int _IDNOTASERVICO = -1;
        decimal _TOTALSERVICO = 0;
        decimal VLTOTALSERVICOS = 0;
        decimal VLTOTALIMPOSTOSO = 0;

        public FrmNotaFiscalServico()
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


        public NOTASERVICOEntity Entity
        {
            get
            {
                int NPS = Convert.ToInt32(txtNRPS.Text);
                DateTime DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                int CODNATUREZA = cbCodNatureza.SelectedIndex + 1;
                int REGIMETRIBUTACAO= cbRegimeTributacao.SelectedIndex + 1;
                int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                decimal DEDUCAO  = Convert.ToDecimal(txtDeducao.Text);
                decimal PIS = Convert.ToDecimal(txtPIS.Text);
                decimal COFINS = Convert.ToDecimal(txtConfins.Text);
                decimal INSS  = Convert.ToDecimal(txtINSS.Text);
                decimal IMPOSTORENDA  = Convert.ToDecimal(txtImpostoRenda.Text);
                decimal CONTRIBUICAOSOCIAL = Convert.ToDecimal(txtConstribuicaoSocial.Text);
                decimal ISS = Convert.ToDecimal(txtISS.Text);
                decimal ISSRETIDO = Convert.ToDecimal(txtISSRetido.Text);
                decimal OUTRASRETENCOES = Convert.ToDecimal(txtOutrasRetencoes.Text);
                decimal BASECALCULO = Convert.ToDecimal(txtBaseCalculo.Text);
                decimal ALIQSERVICO = Convert.ToDecimal(txtAliqServico.Text);
                decimal DESCONTO = Convert.ToDecimal(txtDesconto.Text);
                string OBSERVACAO = txtObservacao.Text;
                string DESCRICAODETALSERVICO = txtDescricaoDetalhaServico.Text;
                int Lote = Convert.ToInt32(txtLote.Text);
                
                int? IDCENTROCUSTO = null;
                if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int? IDFORMAPAGTO = null;
                if (Convert.ToInt32(cbFormaPagto.SelectedValue) > 0)
                    IDFORMAPAGTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                int? IDLOCALCOBRANCA = null;
                if (Convert.ToInt32(cbLocalCobranca.SelectedValue) > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);
             

                return new NOTASERVICOEntity( _IDNOTASERVICO, NPS, DATAEMISSAO, CODNATUREZA, REGIMETRIBUTACAO, IDCLIENTE,
                                            DEDUCAO, PIS, COFINS, INSS, IMPOSTORENDA, CONTRIBUICAOSOCIAL, ISS, ISSRETIDO,
                                            OUTRASRETENCOES, BASECALCULO, ALIQSERVICO, DESCONTO, OBSERVACAO, DESCRICAODETALSERVICO,
                                            _TOTALSERVICO, IDCENTROCUSTO, IDFORMAPAGTO, IDLOCALCOBRANCA);
            }
            set
            {

                if (value != null)
                {
                    _IDNOTASERVICO = value.IDNOTASERVICO;
                    ListaServicoNotaFiscal(_IDNOTASERVICO);
                    txtNRPS.Enabled = false;
                    txtNRPS.Text =  Convert.ToInt32(value.NPS).ToString().PadLeft(6, '0');
                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAEMISSAO).ToString("dd/MM/yyyy");
                    cbCodNatureza.SelectedIndex = Convert.ToInt32(value.CODNATUREZA) - 1;
                    cbRegimeTributacao.SelectedIndex = Convert.ToInt32(value.REGIMETRIBUTACAO)-1;
                    cbCliente.SelectedValue = value.IDCLIENTE;

                    txtDeducao.Text = Convert.ToDecimal(value.DEDUCAO).ToString("n2");
                    txtPIS.Text = Convert.ToDecimal(value.PIS).ToString("n2");
                    txtConfins.Text = Convert.ToDecimal(value.COFINS).ToString("n2");
                    txtINSS.Text = Convert.ToDecimal(value.INSS).ToString("n2");
                    txtImpostoRenda.Text = Convert.ToDecimal(value.IMPOSTORENDA).ToString("n2");
                    txtConstribuicaoSocial.Text = Convert.ToDecimal(value.CONTRIBUICAOSOCIAL).ToString("n2");
                    txtISS.Text = Convert.ToDecimal(value.ISS).ToString("n2");
                    txtISSRetido.Text = Convert.ToDecimal(value.ISSRETIDO).ToString("n2");
                    txtOutrasRetencoes.Text = Convert.ToDecimal(value.OUTRASRETENCOES).ToString("n2");
                    txtBaseCalculo.Text = Convert.ToDecimal(value.BASECALCULO).ToString("n2");
                    txtAliqServico.Text = Convert.ToDecimal(value.ALIQSERVICO).ToString("n2");
                    txtDesconto.Text = Convert.ToDecimal(value.DESCONTO).ToString("n2");

                    txtObservacao.Text = value.OBSERVACAO;
                    txtDescricaoDetalhaServico.Text = value.DESCRICAODETALSERVICO;

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;
                    else
                        cbCentroCusto.SelectedValue = -1;

                    if (value.IDFORMAPAGTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGTO;
                    else
                        cbFormaPagto.SelectedValue = -1;

                    if (value.IDLOCALCOBRANCA != null)
                        cbLocalCobranca.SelectedValue = value.IDLOCALCOBRANCA;
                    else
                        cbLocalCobranca.SelectedValue = -1;

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNRPS.Text);
                    tabControl1.SelectTab(0);

                    errorProvider1.Clear();
                }
                else
                {
                    _IDNOTASERVICO = -1;
                    ListaServicoNotaFiscal(_IDNOTASERVICO);
                    txtNRPS.Text = "000000";
                    txtNRPS.Enabled = true;
                    //Proximo Numero da Nota Fiscal
                    GetNextNF();

                    msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    cbCliente.SelectedValue = -1;

                    txtDeducao.Text = "0,00";
                    txtPIS.Text = "0,00";
                    txtConfins.Text = "0,00";
                    txtINSS.Text = "0,00";
                    txtImpostoRenda.Text = "0,00";
                    txtConstribuicaoSocial.Text = "0,00"; ;
                    txtISS.Text = "0,00";
                    txtISSRetido.Text = "0,00";
                    txtOutrasRetencoes.Text = "0,00";
                    txtBaseCalculo.Text = "0,00";
                    txtAliqServico.Text = "0,00";
                    txtDesconto.Text = "0,00";
                    txtLote.Text = "1";

                    txtObservacao.Text = string.Empty;
                    txtDescricaoDetalhaServico.Text = string.Empty;

                     cbCentroCusto.SelectedValue = -1;
                     cbFormaPagto.SelectedValue = -1;
                     cbLocalCobranca.SelectedValue = -1;

                     //Lista Duplicatas do Pedido
                     GridDuplicatasPD(-1, "");
                     tabControl1.SelectTab(0);
                    errorProvider1.Clear();
                }
            }
        }

        int _IDSERVICONPS = -1;
        public SERVICONPSEntity Entity2
        {
            get
            {
                int IDSERVICO = Convert.ToInt32(cbServico.SelectedValue);
                decimal QUANTIDADE = Convert.ToDecimal(txtQuanServico.Text);
                decimal VALORUNITARIO = Convert.ToDecimal(txtValorServico.Text);
                decimal VALORTOTAL = QUANTIDADE * VALORUNITARIO;

                decimal ALIQISS = Convert.ToDecimal(SERVICOP.Read(IDSERVICO).ALIQISSQN);
                decimal VALORTRIBUTO = (VALORTOTAL * ALIQISS) / 100;

                return new SERVICONPSEntity(_IDSERVICONPS, _IDNOTASERVICO, IDSERVICO, QUANTIDADE,
                                            VALORUNITARIO, VALORTOTAL, VALORTRIBUTO);

            }
            set
            {
                if (value != null)
                {
                    _IDSERVICONPS = value.IDSERVICONPS;
                    cbServico.SelectedValue = value.IDSERVICO;
                    txtQuanServico.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("N2");
                    txtValorServico.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("N2");
                    errorProvider1.Clear();
                }
                else
                {

                    _IDSERVICONPS = -1; ;
                    cbServico.SelectedValue = -1;
                    txtQuanServico.Text = "0,00";
                    txtValorServico.Text = "0,00";
                    errorProvider1.Clear();

                    errorProvider1.Clear();
                }
            }
        }

        private void GetNextNF()
        {
            try
            {

                NOTASERVICOCollection NOTASERVICOColllNextNF = new NOTASERVICOCollection();
                NOTASERVICOColllNextNF = NOTASERVICOP.ReadCollectionByParameter(null, "IDNOTASERVICO");
                if (NOTASERVICOColllNextNF.Count > 0)
                    {
                        int indice = NOTASERVICOColllNextNF.Count - 1;
                        int UltNF = Convert.ToInt32(NOTASERVICOColllNextNF[indice].NPS);
                        txtNRPS.Text = Convert.ToString(UltNF + 1);
                        txtNRPS.Text = txtNRPS.Text.PadLeft(8, '0');
                    }
                    else
                        txtNRPS.Text = "1".PadLeft(8, '0');
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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
                   _IDNOTASERVICO = NOTASERVICOP.Save(Entity);
                   txtNRPS.Text = txtNRPS.Text.ToString().PadLeft(6, '0');
                   txtNRPS.Enabled = false;
                   tabControlMarca.SelectTab(0);
                   Util.ExibirMSg(BmsSoftware.ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(BmsSoftware.ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }
      
        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNRPS.Text))
            {
                errorProvider1.SetError(txtNRPS, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtLote.Text))
            {
                errorProvider1.SetError(label30, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 0)
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


            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                GetToolStripButtonCadastro();
                GetDropServico();
                GetDropCliente();
                GetDropCentroCusto();
                GetDropFormaPgto();
                GetDropLocalCobranca();

                btnCadServico.Image = Util.GetAddressImage(6);
                btnCadCliente.Image = Util.GetAddressImage(6);
                bntDateSelecInicial.Image = Util.GetAddressImage(11);
                bntDateSelecFinal.Image = Util.GetAddressImage(11);

                btnFormPagamento.Image = Util.GetAddressImage(6);
                btnCadCentroCusto.Image = Util.GetAddressImage(6);
                btnCadLocaPagto.Image = Util.GetAddressImage(6);

                PreencheDropCamposPesquisa();
                PreencheDropTipoPesquisa();

               Entity = null;

                if (BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza != string.Empty)
                    cbCodNatureza.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza) - 1;
                if (BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza != string.Empty)
                    cbRegimeTributacao.SelectedIndex = Convert.ToInt32(BmsSoftware.ConfigNotaFiscalServico.Default.RegimeTributacao) - 1;

                txtAliqServico.Text = BmsSoftware.ConfigNotaFiscalServico.Default.Aliquota;

                cbRetencaoFonte.SelectedIndex = 0;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo"; cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos"); 
            cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }       

        private void GetDropCliente()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropServico()
        {
            try
            {
                SERVICOProvider SERVICOP = new SERVICOProvider();
                SERVICOCollection SERVICOColl = new SERVICOCollection();

                SERVICOColl = SERVICOP.ReadCollectionByParameter(null, "NOME");

                cbServico.DisplayMember = "NOME";
                cbServico.ValueMember = "IDSERVICO";

                SERVICOEntity SERVICOTy = new SERVICOEntity();
                SERVICOTy.NOME = ConfigMessage.Default.MsgDrop;
                SERVICOTy.IDSERVICO = -1;
                SERVICOColl.Add(SERVICOTy);

                Phydeaux.Utilities.DynamicComparer<SERVICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SERVICOEntity>(cbServico.DisplayMember);

                SERVICOColl.Sort(comparer.Comparer);
                cbServico.DataSource = SERVICOColl;

                cbServico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);

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
            txtNRPS.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNRPS.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDNOTASERVICO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlMarca.SelectTab(1);
            }
            else if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar a Nota Fiscal é Necessário Remover as Duplicatas!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(dataGridDupl, ConfigMessage.Default.CampoObrigatorio);

                tabControl1.SelectTab(2);
                tabControlMarca.SelectTab(0);
                
            }
            else if (LIS_SERVICONPSColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar a Nota Fiscal é Necessário Remover os Serviços!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(DGDadosServicos, ConfigMessage.Default.CampoObrigatorio);

                tabControlMarca.SelectTab(0);
                tabControl1.SelectTab(0);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                       NOTASERVICOP.Delete(_IDNOTASERVICO);
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
            if (LIS_NOTASERVICOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_NOTASERVICOColl[rowindex].IDNOTASERVICO);

                    Entity = NOTASERVICOP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNRPS.Focus();
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
            notaFiscalDeServiçoToolStripMenuItem_Click(null, null);
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Marcas");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument1;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
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
                if (this.ActiveControl.Name != "txtObservacao")
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
            if (LIS_NOTASERVICOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_NOTASERVICOColl[indice].IDNOTASERVICO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = NOTASERVICOP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNRPS.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                           NOTASERVICOP.Delete(CodigoSelect);
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

        private void configuraçãoDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfiguraServico Frm = new FrmConfiguraServico();
            Frm.ShowDialog();
        }

        private void txtDeducao_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtPIS_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtConfins_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtINSS_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtImpostoRenda_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtConstribuicaoSocial_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtISS_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }

            lblObsField.Text = "Imposto Sobre Serviços de Qualquer Natureza";
        }

        private void txtISSRetido_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtOutrasRetencoes_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtBaseCalculo_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtAliqServico_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtDesconto_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
        }

        private void txtDeducao_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                    
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtPIS_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtConfins_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtINSS_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtImpostoRenda_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtConstribuicaoSocial_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtISS_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtISSRetido_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtOutrasRetencoes_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtBaseCalculo_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtAliqServico_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtDesconto_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtValorServico_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtQuanServico_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            errorProvider1.Clear();
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Text = "0,00";
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "0,0000";
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbCliente.SelectedValue = result;
                }
            }
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
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

        private void btnCadServico_Click(object sender, EventArgs e)
        {
            using (FrmServico frm = new FrmServico())
            {
                int CodSelec = Convert.ToInt32(cbServico.SelectedValue);
                frm._idservico = CodSelec;
                frm.ShowDialog();
                GetDropServico();
                cbServico.SelectedValue = CodSelec;
            }
        }

        private void txtValorServico_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text.Trim() != string.Empty && Convert.ToDecimal(TextBoxSelec.Text) == 0)
            {
                TextBoxSelec.Text = string.Empty;
            }
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
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                //if (rbOrcamentoPesquisa.Checked)
                //{
                //    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S");
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}
                //else if (rbVendasPesquisa.Checked)
                //{
                //    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N");
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}

                //if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                //{
                //    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}

                LIS_NOTASERVICOColl = LIS_NOTASERVICOP.ReadCollectionByParameter(Filtro, "NPS DESC");

                //Colocando somatorio no final da lista
                LIS_NOTASERVICOEntity LIS_NOTASERVICOy = new LIS_NOTASERVICOEntity();
                LIS_NOTASERVICOy.TOTALSERVICO = SumTotalPesquisa("TOTALSERVICO");
                LIS_NOTASERVICOColl.Add(LIS_NOTASERVICOy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_NOTASERVICOColl;

                lblTotalPesquisa.Text = (LIS_NOTASERVICOColl.Count - 1).ToString();
            }
            else
                PesquisaFiltro();

           // PaintGrid();

            this.Cursor = Cursors.Default;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_NOTASERVICOEntity item in LIS_NOTASERVICOColl)
            {
                if (NomeCampo == "TOTALSERVICO")
                    valortotal += Convert.ToDecimal(item.TOTALSERVICO);           
            }

            return valortotal;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
              //  if (tabControlPedidoVenda.SelectedIndex == 2)
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

            //ecessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_NOTASERVICOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_NOTASERVICOColl;
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
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                //if (rbOrcamentoPesquisa.Checked)
                //{
                //    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S");
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}
                //else if (rbVendasPesquisa.Checked)
                //{
                //    filtroProfile = new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N");
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}

                //if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                //{
                //    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                //    Filtro.Insert(Filtro.Count, filtroProfile);
                //}


                LIS_NOTASERVICOColl = LIS_NOTASERVICOP.ReadCollectionByParameter(Filtro, "NPS DESC");

                //Colocando somatorio no final da lista
                LIS_NOTASERVICOEntity LIS_NOTASERVICOy = new LIS_NOTASERVICOEntity();
                LIS_NOTASERVICOy.TOTALSERVICO = SumTotalPesquisa("TOTALSERVICO");
                LIS_NOTASERVICOColl.Add(LIS_NOTASERVICOy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_NOTASERVICOColl;

                lblTotalPesquisa.Text = (LIS_NOTASERVICOColl.Count - 1).ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(230, 160);
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
            msktDataInicial.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(230, 160);
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
            msktDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_NOTASERVICOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_NOTASERVICOColl.Count.ToString();
        }

        private void btnAddServico_Click(object sender, EventArgs e)
        {
            GravaServico();
        }

        private void GravaServico()
        {
            try
            {
                if (ValidacoesServico())
                {
                    SERVICONPSP.Save(Entity2);
                    ListaServicoNotaFiscal(_IDNOTASERVICO);

                    //Salva NOTAFISCAL
                    NOTASERVICOP.Save(Entity);

                    Entity2 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                    cbServico.Focus();
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ListaServicoNotaFiscal(int IDNOTASERVICO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDNOTASERVICO", "System.Int32", "=", IDNOTASERVICO.ToString()));
                LIS_SERVICONPSColl = LIS_SERVICONPSP.ReadCollectionByParameter(RowRelatorio);
                
                DGDadosServicos.AutoGenerateColumns = false;
                DGDadosServicos.DataSource = LIS_SERVICONPSColl;

                SumTotalServico();              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }    
        }

        private void SumTotalServico()
        {
            try
            {
                VLTOTALSERVICOS = 0;
                VLTOTALIMPOSTOSO = 0;
               
                foreach (LIS_SERVICONPSEntity item in LIS_SERVICONPSColl)
                {
                    VLTOTALSERVICOS += Convert.ToDecimal(item.VALORTOTAL);
                    VLTOTALIMPOSTOSO += Convert.ToDecimal(item.VALORTRIBUTO);
                }

                txtISS.Text = VLTOTALIMPOSTOSO.ToString("n2");
                lblTotalServico.Text = "Total de Serviços: " + VLTOTALSERVICOS.ToString("n2");
                lblTotalImposto.Text = "Total de Impostos: " + VLTOTALIMPOSTOSO.ToString("n2");
                _TOTALSERVICO = VLTOTALSERVICOS;             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "+ ex.Message);
            }
        }
       
        
        private Boolean ValidacoesServico()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if(_IDNOTASERVICO == -1)
            {
                Util.ExibirMSg("Antes de adicionar os serviços é necessário gravar a Nota Fiscal!", "Red");
                result = false;
            }
            else if (cbServico.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");

                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorServico.Text))
            {
                errorProvider1.SetError(txtValorServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanServico.Text) || Convert.ToDecimal(txtQuanServico.Text) == 0)
            {
                errorProvider1.SetError(txtQuanServico, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }           
            else

                errorProvider1.Clear();


            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Nota Fiscal de Serviço";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void DGDadosServicos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SERVICONPSColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;
                if (ColumnSelecionada == 1)//Excluir
                {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodSelect = Convert.ToInt32(LIS_SERVICONPSColl[rowindex].IDSERVICONPS);
                                SERVICONPSP.Delete(CodSelect);

                                ListaServicoNotaFiscal(_IDNOTASERVICO);
                                Entity2 = null;

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    
                }
                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_SERVICONPSColl[rowindex].IDSERVICONPS);
                    Entity2 = SERVICONPSP.Read(CodSelect);

                }

            }
        }

        private void cbServico_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbServico.SelectedIndex > 0)
            {
                SERVICOEntity ServicoTy = SERVICOP.Read(Convert.ToInt32(cbServico.SelectedValue));

                if (ServicoTy != null || ServicoTy.VALOR != null)
                    txtValorServico.Text = Convert.ToDecimal(ServicoTy.VALOR).ToString("N2");
            }
        }

        private void btnCancelEdiServico_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void notaFiscalDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDNOTASERVICO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmNotaServicoRelatorio Frm = new FrmNotaServicoRelatorio())
                {
                    Frm._IDNOTASERVICO = _IDNOTASERVICO;
                    Frm.ShowDialog();

                }
            }
        }

        private void sPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidacoesArquivoNFS() && ValidaCliente(Convert.ToInt32(cbCliente.SelectedValue)))
               GerarArquivoNotaFiscalServico(txtNRPS.Text);
        }

        private Boolean ValidaCliente(int IDCLIENTE)
        {
            Boolean Result = true;

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_CLIENTEColl.Count > 0)
            {
                if (LIS_CLIENTEColl[0].CPF == "   .   .   -" && LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -")
                {
                    MessageBox.Show("CPF/CNPJ Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].ENDERECO1.Trim() == string.Empty)
                {
                    MessageBox.Show("Endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].NUMEROENDER.Trim() == string.Empty)
                {
                    MessageBox.Show("Número do endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].BAIRRO1.Trim() == string.Empty)
                {
                    MessageBox.Show("Número do endereço Inválido");
                    Result = false;
                }
                else if (LIS_CLIENTEColl[0].CEP1.Trim() == "     -")
                {
                    MessageBox.Show("CEP Inválido");
                    Result = false;
                }
            }

            return Result;
        }

        private Boolean ValidacoesArquivoNFS()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if(_IDNOTASERVICO ==  -1)
            {
                errorProvider1.SetError(label23, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (LIS_SERVICONPSColl.Count == 0)
            {
                errorProvider1.SetError(DGDadosServicos, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void GerarArquivoNotaFiscalServico(string NumeroNota)
        {
            //Dados da Empresa
            EMPRESAEntity EMPRESATy = new EMPRESAEntity();
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESATy = EMPRESAP.Read(1);

            //Dados da Configuração
            CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMATy = CONFISISTEMAP.Read(1);

            //Dados do Cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
          
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            //O arquivo possui a extensão “txt” e seu nome segue o seguinte padrão:
            //CNPJ do contribuinte + anoatual + mesatual + diaatual + horaatual + minutoatual + segundoatual.
            string NomeArquivo = Util.RetiraLetras(EMPRESATy.CNPJCPF) + Convert.ToDateTime(msktDataEmissao.Text).ToString("yyyy") + Convert.ToDateTime(msktDataEmissao.Text).ToString("MM") + Convert.ToDateTime(msktDataEmissao.Text).ToString("dd") + Convert.ToDateTime(DateTime.Now).ToString("HHmmss");
            string Local = BmsSoftware.ConfigNotaFiscalServico.Default.LocalArquivoTexto + @"\" + NomeArquivo + ".txt";

            StreamWriter escrever = new StreamWriter(Local, false, Encoding.GetEncoding(1252));

            //ESTRUTURA DO ARQUIVO
            //0|versao_layout|
            //1|cnpj|inscricao_municipal|es_municipio|numero|serie|dt_competencia|...
            //2|descricao|qt_item|vl_unitario|sn_iss_tributavel|
           // 0 - Cabeçalho
           //1- Corpo do arquivo
           //2- Lista de itens de serviço

            //Tipo: tipo de dados utilizados pelo campo:
            //C: Caractere;
            // N: Número;
            // D: Data;

            try 
	        {
                //Cabeçalho
                escrever.WriteLine("0|1.06|");

                //1- Corpo do arquivo
                //string Identificador = "1";// Número correspondente ao conteúdo da nota - 1 – Fixo / TAMANHO: 1
                string lote_rps = "0";// Número do lote enviado que deverá ser sequencial e único. Deverá ser iniciado no número 1 e continuar essa sequência nos demais arquivos enviados posteriormente. Cada arquivo deverá ser composto por um único lote  TIPO: N / TAMANHO: 15
                string cnpj = Util.RetiraLetras(EMPRESATy.CNPJCPF); // CPF/CNPJ do Prestador / TIPO: C / TAMANHO: 14
                string inscricao_municipal = BmsSoftware.ConfigNotaFiscalServico.Default.InscricaoMunicipalPrestador; // Inscrição Municipal do Prestador / TIPO: C / TAMANHO: 15
                string es_municipio = CONFISISTEMATy.CODMUNIBGE.ToString();//Código do município do Prestador conforme a tabela do IBGE / TIPO: N / TAMANHO: 7
                string numero = txtNRPS.Text;// Número da NFS-e - TIPO: N
                string serie = BmsSoftware.ConfigNotaFiscalServico.Default.Serie; // Série da NFS-e / TIPO: C / TAMANHO: 5
                string dt_emissao = Convert.ToDateTime(msktDataEmissao.Text).ToString("yyyy-MM-dd"); //Data da emissão do RPS -  / TIPO: D / TAMANHO: 5 /Formato AAAA-MM-DD

                string cd_natureza_operacao = BmsSoftware.ConfigNotaFiscalServico.Default.CodNatureza;//Código da Natureza da Operação
                // 1 - Tributação no município
                // 2 – Tributação fora do município
                // 3 – Isenção
                // 4 – Imune
                // 5 – Exigibilidade  suspensa por decisão Judicial
                // 6 – Exigibilidade suspensa por procedimento administrativo

                string cd_regime_especial_tributacao = BmsSoftware.ConfigNotaFiscalServico.Default.RegimeTributacao; // Código de identificação do Regime Especial de  Tributação que descreve o tipo em que se enquadra o contribuinte.
                // 1 - Microempresa Municipal
                // 2 – Estimativa
                // 3 – Sociedade de Profissionais
                // 4 – Cooperativa
                // 5 – Microempresário Individual (MEI)
                // 6 – Microempresário e Empresa de Pequeno Porte (ME EPP)
                // 7 – Tributação por Faturamento (Variável)
                // 8 – Fixo
                // 9 – Isenção
                // 10 – Imune
                // 11 - Exigibilidade suspensa por decisão judicial
                // 12 - Exigibilidade suspensa por procedimento

                string sn_optante_simples_nacional = BmsSoftware.ConfigNotaFiscalServico.Default.OptanteSimples;// Optante pelo Simples Nacional / 1 – Sim 2 – Não
                string cd_status = "1"; // Status do RPS / 1 – Ativa 2 – Cancelada
                string es_item_lista_servico = BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificaçãoAtividadeFederal; // Código de especificação da Atividade Federal /Tamanho 10
                string cd_tributacao_municipio = BmsSoftware.ConfigNotaFiscalServico.Default.CodigoEspecificacaoAtividadeMunicipal;//Código de especificação de Atividade Municipal
                string es_cnae = CONFISISTEMATy.CNAE; // Código CNAE. Use apenas números nesse campo. //Tam:  7
                string discriminacao = Util.LimiterText(LIS_SERVICONPSColl[0].NOMESERVICO + " " + txtDescricaoDetalhaServico.Text,2000);// TAMA: 2000
                string vl_servicos = Convert.ToDecimal(_TOTALSERVICO).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));//Valor total do serviço /  Valor monetário Formato: 0000.00 (ponto separando casa decimal) 
                string vl_deducao = Convert.ToDecimal(txtDeducao.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor de dedução / Valor monetário. Se valor for zero, colocar 0.00
                string vl_pis =  Convert.ToDecimal(txtPIS.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor do PIS / Valor monetário
                string vl_cofins =  Convert.ToDecimal(txtConfins.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor do COFINS / Valor monetário
                string vl_inss  =  Convert.ToDecimal(txtINSS.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor do INSS / Valor monetário
                string vl_ir = Convert.ToDecimal(txtImpostoRenda.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor do Imposto de Renda / Valor monetário
                string vl_csll = Convert.ToDecimal(txtConstribuicaoSocial.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); ;//Valor da Contribuição Social / Valor monetário

                string sn_iss_retido = (cbRetencaoFonte.SelectedIndex + 1).ToString(); //Retenção na fonte  //1 – Sim – tomador pagará  //2 – Não – prestador pagará
                string vl_iss = Convert.ToDecimal(_TOTALSERVICO * Convert.ToDecimal(txtAliqServico.Text)).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));; //Valor do ISS - Imposto /  Valor monetário vl_iss = (vl_aliquota * vl_base_calculo)/100
                
                string vl_iss_retido = "0.00";//Valor do ISS retido / sn_iss_retido for 1: recebe o valor do ISS, sn_iss_retido for 2: recebe 0.00
                if (sn_iss_retido == "1")
                    vl_iss_retido = vl_iss;

                string vl_outras_retencoes = Convert.ToDecimal(txtOutrasRetencoes.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US")); // Valor de outras retenções Valor monetário. Se valor for zero, colocar 0.00
                string vl_base_calculo =  Convert.ToDecimal(Convert.ToDecimal(vl_servicos) -  Convert.ToDecimal(vl_deducao)).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));//Valor da base de cálculo /  Valor monetário Expressão: vl_base_calculo = vl_servicos – vl_deducao 
                string vl_aliquota = Convert.ToDecimal(txtAliqServico.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));// Valor da alíquota do erviço / Valor percentual. De 0% a  5 %
                string vl_liquido_nfse = Convert.ToDecimal(Convert.ToDecimal(vl_servicos) - (Convert.ToDecimal(vl_pis) + Convert.ToDecimal(vl_cofins) + Convert.ToDecimal(vl_inss) + Convert.ToDecimal(vl_ir) + Convert.ToDecimal(vl_csll) + Convert.ToDecimal(vl_outras_retencoes) + Convert.ToDecimal(vl_iss_retido))).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));  //Valor líquido da Nota Fiscal // Valor monetário. vl_liquido_nfse = vl_servicos - (vl_pis +  vl_cofins + vl_inss + vl_ir + vl_csll +vl_outras_retencoes +vl_iss_retido + vl_desconto_incondiciona do + vl_desconto_condicionado)
                string vl_desconto_incondicionado = "0.00";// Valor de desconto incondicionado
                string vl_desconto_condicionado = "0.00";// Valor de desconto incondicionado
                string tom_cd_cpf_cnpj_tipo =  LIS_CLIENTEColl[0].CNPJ.Length > 15 ? "2" : "1";//Tipo do CPF/CNPJ do Tomador do Serviço // 1– CPF 2 – CNPJ  3 – Exterior
                string tom_cpf_cnpj =  LIS_CLIENTEColl[0].CNPJ.Length > 15 ?  Util.RetiraLetras(LIS_CLIENTEColl[0].CNPJ) : Util.RetiraLetras(LIS_CLIENTEColl[0].CPF); //CPF/CNPJ do Tomador do Serviço
                string tom_inscricao_municipal = ""; // Inscrição Municipal do Tomador do Serviço
                string tom_razao_social = LIS_CLIENTEColl[0].NOME;//Razão Social do Tomador do Serviço
                string tom_endereco = LIS_CLIENTEColl[0].ENDERECO1;//Endereço do Tomador do Serviço 
                string tom_endereco_numero =  LIS_CLIENTEColl[0].NUMEROENDER;//Número do endereço do Tomador do Serviço
                string tom_endereco_complemento =  LIS_CLIENTEColl[0].COMPLEMENTO1;//Complemento do endereço do Tomador do Serviço
                string tom_endereco_bairro =  LIS_CLIENTEColl[0].BAIRRO1;//Bairro do endereço do Tomador do Serviço
                string tom_endereco_uf = LIS_CLIENTEColl[0].UF;//Estado do endereço do Tomador do Serviço
                string tom_endereco_cep = Util.RetiraLetras(LIS_CLIENTEColl[0].CEP1);//CEP do endereço do Tomador do Serviço
                string tom_endereco_es_municipio = BuscaCodigoCidade(LIS_CLIENTEColl[0].MUNICIPIO, LIS_CLIENTEColl[0].UF).ToString(); //Código do município do Tomador do Serviço conforme a tabela do IBGE
                string tom_telefone =  Util.RetiraLetras(LIS_CLIENTEColl[0].TELEFONE1);
                string tom_email = LIS_CLIENTEColl[0].EMAILCLIENTE;

                escrever.WriteLine("1" + lote_rps + "|" + cnpj + "|" + inscricao_municipal + "|" + es_municipio + "|" + numero + "|" +
                                   serie + "|" + dt_emissao + "|" + cd_natureza_operacao + "|" + cd_regime_especial_tributacao + "|" +
                                   sn_optante_simples_nacional + "|" + cd_status + "|" + es_item_lista_servico + "|" + cd_tributacao_municipio + "|" +
                                   es_cnae + "|" + discriminacao + "|" + vl_servicos + "|" + vl_deducao + "|" + vl_pis + "|" + vl_cofins + "|" +
                                   vl_inss + "|" + vl_ir + "|" + vl_csll + "|" + sn_iss_retido + "|" + vl_iss + "|" + vl_iss_retido + "|" +
                                   vl_outras_retencoes + "|" + vl_base_calculo + "|" + vl_aliquota + "|" + vl_liquido_nfse + "|" +
                                   vl_desconto_incondicionado + "|" + vl_desconto_condicionado + "|" + tom_cd_cpf_cnpj_tipo + "|" +
                                   tom_cpf_cnpj + "|" + tom_inscricao_municipal + "|" + tom_razao_social + "|" + tom_endereco +  "|" +
                                   tom_endereco_numero + "|" + tom_endereco_complemento +  "|" + tom_endereco_bairro + "|" +
                                   tom_endereco_uf + "|" + tom_endereco_cep + "|" + tom_endereco_es_municipio +"|" + tom_telefone + "|" +
                                   tom_email + "|");

                //2|DESCRIÇÃO SERVIÇO 1|1.00000|5.00|1|
                string sn_iss_tributavel = "1"; //Incidência de recolhimento de ISS. Somente poderão declarar esse campo como “Não”  tributável os contribuintes que possuam autorização prévia do município para essa operação.
                //1– Sim - Valores declarados serão somados no total de serviços
                // 2 – Não - Valores declarados não serão somados no total de serviços
                foreach (LIS_SERVICONPSEntity item in LIS_SERVICONPSColl)
                {
                    string qt_item = Convert.ToDecimal(item.QUANTIDADE).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
                    string vl_unitario = Convert.ToDecimal(item.VALORUNITARIO).ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
                    escrever.WriteLine("2|" + item.NOMESERVICO + "|" + qt_item + "|" + vl_unitario + "|" + sn_iss_tributavel + "|");
                }

                escrever.Close();

                Util.ExibirMSg("Arquivo: " + Local + " gerado com sucesso!", "Blue");
	        }
	        catch (Exception ex)
	        {
		        MessageBox.Show("Erro técnico: " + ex.Message);
                escrever.Close();
	        }


        }


        private int BuscaCodigoCidade(string MUNICIPIO, string UF)
        {
            int result = -1;

            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "collate pt_br like", "%" + MUNICIPIO.Replace("'", "") + "%"));
            RowRelatorio.Add(new RowsFiltro("UF", "System.String", "=", UF));
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_MUNICIPIOSColl.Count > 0)
            {
                result = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
            }

            return result;
        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                frm._IDCENTROCUSTOS = CodSelec;
                frm.ShowDialog();

                GetDropCentroCusto();
                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void GetDropCentroCusto()
        {
            try
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
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnFormPagamento_Click(object sender, EventArgs e)
        {
            using (FrmFormasPagamento frm = new FrmFormasPagamento())
            {
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
                frm._IDFORMAPAGAMENTO = CodSelec;
                frm.ShowDialog();
                GetDropFormaPgto();
                cbFormaPagto.SelectedValue = CodSelec;
            }
        }

        private void GetDropFormaPgto()
        {
            try
            {
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                FORMAPAGAMENTOColl = FORMAPAGAMENTOP.ReadCollectionByParameter(null, "NOME");

                cbFormaPagto.DisplayMember = "NOME";
                cbFormaPagto.ValueMember = "IDFORMAPAGAMENTO";

                FORMAPAGAMENTOEntity FORMAPAGAMENTOTy = new FORMAPAGAMENTOEntity();
                FORMAPAGAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
                FORMAPAGAMENTOTy.IDFORMAPAGAMENTO = -1;
                FORMAPAGAMENTOColl.Add(FORMAPAGAMENTOTy);

                Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity>(cbFormaPagto.DisplayMember);

                FORMAPAGAMENTOColl.Sort(comparer.Comparer);
                cbFormaPagto.DataSource = FORMAPAGAMENTOColl;

                cbFormaPagto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                frm._IDLOCALCOBRANCA = CodSelec;

                frm.ShowDialog();

                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
            }
        }

        private void GetDropLocalCobranca()
        {
            LOCALCOBRANCAProvider LOCALCOBRANCAP = new LOCALCOBRANCAProvider();
            LOCALCOBRANCAColl = LOCALCOBRANCAP.ReadCollectionByParameter(null, "NOME");

            cbLocalCobranca.DisplayMember = "NOME";
            cbLocalCobranca.ValueMember = "IDLOCALCOBRANCA";

            LOCALCOBRANCAEntity LOCALCOBRANCATy = new LOCALCOBRANCAEntity();
            LOCALCOBRANCATy.NOME = ConfigMessage.Default.MsgDrop;
            LOCALCOBRANCATy.IDLOCALCOBRANCA = -1;
            LOCALCOBRANCAColl.Add(LOCALCOBRANCATy);

            Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity>(cbLocalCobranca.DisplayMember);

            LOCALCOBRANCAColl.Sort(comparer.Comparer);
            cbLocalCobranca.DataSource = LOCALCOBRANCAColl;

            cbLocalCobranca.SelectedIndex = 0;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDNOTASERVICO == -1)
            {
                MessageBox.Show("Antes de adicionar o Financeiro é necessário gravar a Nota Fiscal!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else if(Convert.ToInt32(cbFormaPagto.SelectedValue) < 1)
            {
                errorProvider1.SetError(label32, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                try
                {
                    SaveDuplicatas();

                    //Salva a forma de pagamento na Nota Fiscal
                    NOTASERVICOP.Save(Entity);

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }
            }
        }

        private void SaveDuplicatas()
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", cbFormaPagto.SelectedValue.ToString()));

            ITENSFORMAPAGTOProvider ITENSFORMAPAGTOP = new ITENSFORMAPAGTOProvider();
            ITENSFORMAPAGTOCollection ITENSFORMAPAGTOColl = new ITENSFORMAPAGTOCollection();

            ITENSFORMAPAGTOColl = ITENSFORMAPAGTOP.ReadCollectionByParameter(RowRelatorio, "IDITENSFORMAPAGTO");

            foreach (ITENSFORMAPAGTOEntity item in ITENSFORMAPAGTOColl)
            {
                DUPLICATARECEBEREntity DUPLICATARECEBERty = new DUPLICATARECEBEREntity();
                DUPLICATARECEBERty.IDDUPLICATARECEBER = -1;
                DUPLICATARECEBERty.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

             //   if (cbFuncionario.SelectedIndex > 0)
             //       DUPLICATARECEBERty.IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);

                if (cbLocalCobranca.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                if (cbCentroCusto.SelectedIndex > 0)
                    DUPLICATARECEBERty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                DUPLICATARECEBERty.NUMERO = txtNRPS.Text + "-" + NumTotalDupl.ToString();
                DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                DUPLICATARECEBERty.DATAVECTO = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt32(item.DIAS)).ToString());

                decimal Valor = _TOTALSERVICO;
                //Calculando desconto
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                decimal PorcDesconto = Convert.ToDecimal(FORMAPAGAMENTOP.Read(Convert.ToInt32(item.IDFORMAPAGAMENTO)).PORCDESCONTO);
                if (PorcDesconto > 0)
                    Valor -= ((Valor * PorcDesconto) / 100);


                // Valor = (Convert.ToDecimal(txtTotalOS.Text) * Convert.ToDecimal(item.PORCPAGTO)) / 100;
                Valor = Valor * Convert.ToDecimal(item.PORCPAGTO) / 100;

                //Calculando juros
                Valor = ((Valor * Convert.ToDecimal(item.PORCJUROS)) / 100) + Valor;
                DUPLICATARECEBERty.VALORDUPLICATA = Valor;
                DUPLICATARECEBERty.VALORDEVEDOR = Valor;
                DUPLICATARECEBERty.IDSTATUS = 1;//Aberto
                DUPLICATARECEBERty.NOTAFISCAL = "NFS" + txtNRPS.Text;              

                try
                {
                    DUPLICATARECEBERP.Save(DUPLICATARECEBERty);

                    //Lista Duplicatas do Pedido
                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNRPS.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private void GridDuplicatasPD(int idcliente, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("idcliente", "System.Int32", "=", idcliente.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "like", "NFS" + numero));

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");
                dataGridDupl.AutoGenerateColumns = false;
                dataGridDupl.DataSource = LIS_DUPLICATARECEBERColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void dataGridDupl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATARECEBERColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

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
                                CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                                DUPLICATARECEBERP.Delete(CodSelect);
                                GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNRPS.Text);
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //editar
                {
                    FrmContasReceber FrmCR = new FrmContasReceber();
                    FrmCR.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                    FrmCR.ShowDialog();

                    GridDuplicatasPD(Convert.ToInt32(cbCliente.SelectedValue), txtNRPS.Text);
                }
                else if (ColumnSelecionada == 2) //Boleto
                {
                    ImprimirBoletaDireto(Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER));
                }
            }
        }

        private void ImprimirBoletaDireto(int IDDUPLICATARECEBER)
        {
            //Selecionar a boleta do config
            CONFISISTEMAEntity ConfigSistemaTy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            ConfigSistemaTy = CONFISISTEMAP.Read(1);
            if (ConfigSistemaTy.IDCONFIGBOLETA == null)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroBoletaSelec,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Busca qual banco sera impresso o boleto
                CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
                CONFIGBOLETAEntity CONFIGBOLETATy = CONFIGBOLETAP.Read(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA));

                PrintBoletoBancario PrintBoletoBancario = new PrintBoletoBancario();

                try
                {
                    switch (CONFIGBOLETATy.IDBANCO)
                    {
                        case 5:
                            //Banco Brasil.
                            PrintBoletoBancario.ImprimiBoletaBrasil(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                    IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                    ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 3:
                            // Banco Basa. 
                            PrintBoletoBancario.ImprimiBoletaBasa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 12:
                            // Banco Santander.                            
                            PrintBoletoBancario.ImprimiBoletaSantander(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 2:
                            //Banco Banrisul
                            PrintBoletoBancario.ImprimiBoletaBanrisul(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 7:
                            //Banco BRB
                            PrintBoletoBancario.ImprimiBoletaBRB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 6:
                            //Banco Caixa
                            PrintBoletoBancario.ImprimiBoletaCaixa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                     IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                     ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 4:
                            //Bradesco
                            PrintBoletoBancario.ImprimiBoletaBradesco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 9:
                            //Itau
                            PrintBoletoBancario.ImprimiBoletaItau(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 13:
                            //Sudameris();
                            PrintBoletoBancario.ImprimiBoletaSudameris(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 10:
                            //Real
                            PrintBoletoBancario.ImprimiBoletaReal(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                       IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                       ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 8:
                            //HSBC
                            PrintBoletoBancario.ImprimiBoletaHSBC(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 14:
                            //Unibanco
                            PrintBoletoBancario.ImprimiBoletaUnibanco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 11:
                            //Safra
                            PrintBoletoBancario.ImprimiBoletaSafra(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 15:
                            //SICOOB
                            PrintBoletoBancario.ImprimirBoletaSICOOB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
    }
}

