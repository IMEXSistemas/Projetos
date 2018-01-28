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
using BmsSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Relatorio;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmItensFestas : Form
    {
        Utility Util = new Utility();

        FESTASProvider FESTASP = new FESTASProvider();
        ITENSFESTASProvider ITENSFESTASP = new ITENSFESTASProvider();
        LIS_ITENSFESTASProvider LIS_ITENSFESTASP = new LIS_ITENSFESTASProvider();
        PRODUTOSFESTASProvider PRODUTOSFESTASP = new PRODUTOSFESTASProvider();
        LIS_PRODUTOSFESTASProvider LIS_PRODUTOSFESTASP = new LIS_PRODUTOSFESTASProvider();

        FESTASCollection FESTASColl = new FESTASCollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_ITENSFESTASCollection LIS_ITENSFESTASColl = new LIS_ITENSFESTASCollection();
        LIS_PRODUTOSFESTASCollection LIS_PRODUTOSFESTASColl = new LIS_PRODUTOSFESTASCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public FrmItensFestas()
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

                if (control is ComboBox)
                {
                    control.KeyDown += new KeyEventHandler(controlFocus_KeyDown);
                    control.KeyPress += new KeyPressEventHandler(controlFocus_KeyPress);
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

        int _IDITENSFESTAS = -1;
        public ITENSFESTASEntity Entity
        {
            get
            {
                int IDFESTA = Convert.ToInt32(cbFestas.SelectedValue);
                string OBSERVACAO = txtObservacao.Text;
                decimal TOTALITENS = Convert.ToDecimal(lblTotalItens.Text);
                return new ITENSFESTASEntity(_IDITENSFESTAS, IDFESTA, OBSERVACAO, TOTALITENS);
            }
            set
            {

                if (value != null)
                {
                    _IDITENSFESTAS = value.IDITENSFESTAS;
                    ListaProdutoFesta(_IDITENSFESTAS);
                    cbFestas.SelectedValue = value.IDFESTA;
                    txtObservacao.Text = value.OBSERVACAO;
                    SomaTotalItens();
                    errorProvider1.Clear();
                }
                else
                {
                    _IDITENSFESTAS = -1;
                    ListaProdutoFesta(_IDITENSFESTAS);
                    cbFestas.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    txtValorTotal.Text = "0,00";
                    txtValor.Text = "0,00";
                    txtValor.Text = "1,00";
                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODFESTA = -1;
        public PRODUTOSFESTASEntity Entity2
        {
            get
            {
                int IDPRODUTOS = Convert.ToInt32(cbProduto.SelectedValue);
                decimal VALOR = Convert.ToDecimal(txtValor.Text);              
                decimal QUANTIDADE = Convert.ToDecimal(txtQuant.Text);
                decimal VALORTOTAL = QUANTIDADE * VALOR;
                
                return new PRODUTOSFESTASEntity(_IDPRODFESTA,_IDITENSFESTAS, IDPRODUTOS, VALOR, 
                                                QUANTIDADE, VALORTOTAL);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODFESTA = value.IDPRODFESTA;
                    txtValor.Text = Convert.ToDecimal(value.VALOR).ToString("n2");
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuant.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n2");
                    txtValorTotal.Text = (Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuant.Text)).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODFESTA = -1;
                    txtValor.Text = "0,00";
                    txtQuant.Text = "0,00";
                    cbProduto.SelectedIndex = 0;
                    txtValorTotal.Text = "0,00";
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
                    _IDITENSFESTAS = ITENSFESTASP.Save(Entity);
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
            if (cbFestas.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFestas, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtValor, "");


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            GetDropFesta();
            GetDropProdutos();            

            btnCadFesta.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);

            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();

            this.Cursor = Cursors.Default;
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
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
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtValor.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtValor.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ValidacoesDelete())
                Delete();
        }

        private Boolean ValidacoesDelete()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (LIS_PRODUTOSFESTASColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar é necessário remover os Produtos/Itens!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(dataGridProdutos, ConfigMessage.Default.CampoObrigatorio);

                tabControlMarca.SelectTab(0);
                result = false;
            }

            return result;
        }

        private void Delete()
        {
            if (_IDITENSFESTAS == -1)
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
                        ITENSFESTASP.Delete(_IDITENSFESTAS);
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
            if (LIS_ITENSFESTASColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_ITENSFESTASColl[rowindex].IDITENSFESTAS);

                    Entity = ITENSFESTASP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtValor.Focus();
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            using (RelatItensProdutoFesta frm = new RelatItensProdutoFesta())
            {
                frm.IDITENSFESTAS = _IDITENSFESTAS;
                frm.ShowDialog();
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Itens das Festas");
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

                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
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
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //'Cabecalho
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 60, config.MargemDireita, 60);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 160, config.MargemDireita, 160);

                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        //'Imagem
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 600, 68);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 68);

                //Titulo
                e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome Festa", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_ITENSFESTASColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_ITENSFESTASColl.Count)
                {
                    if (LIS_ITENSFESTASColl[IndexRegistro].IDITENSFESTAS != null)
                    {
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                        e.Graphics.DrawString(LIS_ITENSFESTASColl[IndexRegistro].IDITENSFESTAS.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                        e.Graphics.DrawString(Util.LimiterText(LIS_ITENSFESTASColl[IndexRegistro].NOMEFESTA, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);

                        string TotalFOS = Convert.ToDecimal(LIS_ITENSFESTASColl[IndexRegistro].TOTALITENS).ToString("n2");
                        e.Graphics.DrawString("Total dos Itens: "+TotalFOS, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 17, stringFormat);

                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));

                        //Listar os produtos
                        LIS_PRODUTOSFESTASCollection LIS_PRODUTOSFESTASPrintColl = new LIS_PRODUTOSFESTASCollection();
                        LIS_PRODUTOSFESTASPrintColl = ProdutoRel(Convert.ToInt32(LIS_ITENSFESTASColl[IndexRegistro].IDITENSFESTAS));
                        e.Graphics.DrawString("Cod.Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 1);
                        e.Graphics.DrawString("Produtos/Itens", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 1);
                        e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha + 1);
                        e.Graphics.DrawString("Vl.Unitário.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 1);
                        e.Graphics.DrawString("Vl.Total.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, config.PosicaoDaLinha + 1);
                        foreach (LIS_PRODUTOSFESTASEntity item in LIS_PRODUTOSFESTASPrintColl)
                        {
                            config.LinhaAtual++;
                            config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                            e.Graphics.DrawString(Util.LimiterText(item.IDPRODUTO.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                            e.Graphics.DrawString(Util.LimiterText(item.NOMEPRODUTOS, 25), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                            e.Graphics.DrawString(Util.LimiterText(item.QUANTIDADE.ToString(), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                            e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALOR).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                            e.Graphics.DrawString(Util.LimiterText(Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 500, config.PosicaoDaLinha);
                        }
                        config.LinhaAtual++;
                        config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));


                        
                        config.LinhaAtual++;
                        string linhasepar = "------------------------------------------------------------------------------------------";
                        string linhasepar2 = "------------------------------------------------------------------------------------------";
                        e.Graphics.DrawString(linhasepar + linhasepar2, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 10);

                        IndexRegistro++;
                        config.LinhaAtual++;

                    }

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;

                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_ITENSFESTASColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                    e.Graphics.DrawString("Total Geral: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_ITENSFESTASColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


                    //Rodape
                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                    e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                    config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                    config.LinhaAtual++;
                    e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }

        }

        private decimal SomaTotal()
        {
            decimal result = 0;

            foreach (LIS_ITENSFESTASEntity item in LIS_ITENSFESTASColl)
            {
                result += Convert.ToDecimal(item.TOTALITENS);
            }
            return result;
        }



        private LIS_PRODUTOSFESTASCollection ProdutoRel(int IDITENSFESTAS)
        {
            LIS_PRODUTOSFESTASCollection LIS_PRODUTOSFESTASColl = new LIS_PRODUTOSFESTASCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDITENSFESTAS", "System.Int32", "=", IDITENSFESTAS.ToString()));

            LIS_PRODUTOSFESTASColl = LIS_PRODUTOSFESTASP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSFESTASColl;
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
            if (e.KeyCode == Keys.Escape) { this.Close(); }

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
            if (LIS_ITENSFESTASColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_ITENSFESTASColl[indice].IDITENSFESTAS);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = ITENSFESTASP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtValor.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            ITENSFESTASP.Delete(CodigoSelect);
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

        private void GetDropFesta()
        {
            FESTASProvider FESTASP = new FESTASProvider();

            FESTASColl = FESTASP.ReadCollectionByParameter(null, "NOME");

            cbFestas.DisplayMember = "NOME";
            cbFestas.ValueMember = "idfestas";

            FESTASEntity FESTASTy = new FESTASEntity();
            FESTASTy.NOME = ConfigMessage.Default.MsgDrop;
            FESTASTy.IDFESTAS = -1;
            FESTASColl.Add(FESTASTy);

            Phydeaux.Utilities.DynamicComparer<FESTASEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FESTASEntity>(cbFestas.DisplayMember);

            FESTASColl.Sort(comparer.Comparer);
            cbFestas.DataSource = FESTASColl;

            cbFestas.SelectedIndex = 0;
        }     

        private void btnCadFesta_Click(object sender, EventArgs e)
        {
            using (FrmTipoFestas frm = new FrmTipoFestas())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFestas.SelectedValue);
                GetDropFesta();
                cbFestas.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutos()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

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

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
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
                        cbProduto.SelectedValue = result;
                }
            }
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void txtValor_Validated(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtValor.Text == string.Empty)
                txtValor.Text = "0,00";

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValor.Text))
            {
                errorProvider1.SetError(txtValor, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtValor.Text);
                txtValor.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValor, "");
            } 
        }

        private void txtValor_Enter(object sender, EventArgs e)
        {
            if (txtValor.Text == "0,00")
                txtValor.Text = string.Empty;
        }

        private void btnLanca_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidacoesProduto())
                {
                    SomaTotalItens();

                    if (_IDITENSFESTAS == -1)
                        Grava();

                    PRODUTOSFESTASP.Save(Entity2);
                    ListaProdutoFesta(_IDITENSFESTAS);
                    Entity2 = null;
                    ITENSFESTASP.Save(Entity);

                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void ListaProdutoFesta(int IDITENSFESTAS)
        {
            RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
            RowpProdPedido.Add(new RowsFiltro("IDITENSFESTAS", "System.Int32", "=", IDITENSFESTAS.ToString()));
            LIS_PRODUTOSFESTASColl = LIS_PRODUTOSFESTASP.ReadCollectionByParameter(RowpProdPedido);

            dataGridProdutos.AutoGenerateColumns = false;
            dataGridProdutos.DataSource = LIS_PRODUTOSFESTASColl;

            SumTotalProdutos();
        }

        private void SumTotalProdutos()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSFESTASEntity item in LIS_PRODUTOSFESTASColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            lblTotalItens.Text = total.ToString("n2");
        }

        private Boolean ValidacoesProduto()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (cbFestas.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFestas, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text) || Convert.ToDecimal(txtQuant.Text) <= 0)
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }  
            else if ( !ValidacoesLibrary.ValidaTipoDecimal(txtValor.Text))
            {
                errorProvider1.SetError(txtValor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }           

            return result;
        }

        private void dataGridProdutos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSFESTASColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOSFESTASColl[rowindex].IDPRODFESTA);
                    Entity2 = PRODUTOSFESTASP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOSFESTASColl[rowindex].IDPRODFESTA);
                            PRODUTOSFESTASP.Delete(CodSelect);
                            ListaProdutoFesta(_IDITENSFESTAS);
                            ITENSFESTASP.Save(Entity);
                            Entity2 = null;

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void txtQuant_Validated(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtQuant.Text == string.Empty)
                txtQuant.Text = "0,00";

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtQuant.Text);
                txtQuant.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtQuant, "");
            } 
        }

        private void txtQuant_Enter(object sender, EventArgs e)
        {
            if (txtQuant.Text == "0,00")
                txtQuant.Text = string.Empty;
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSTY =  PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));

                if (PRODUTOSTY != null)
                {
                    txtValor.Text = Convert.ToInt32(PRODUTOSTY.VALORVENDA1).ToString("n2");
                   // cbRetorno.SelectedIndex = PRODUTOSTY.RETORNO == "S" ? 0 : 1;
                }
                else
                {
                    txtValor.Text = "0,00";  
                }
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_ITENSFESTASColl = LIS_ITENSFESTASP.ReadCollectionByParameter(Filtro, "IDFESTA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ITENSFESTASColl;

                lblTotalPesquisa.Text = LIS_ITENSFESTASColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlMarca.SelectedIndex == 2)
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
            if (LIS_ITENSFESTASColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ITENSFESTASColl;
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

                LIS_ITENSFESTASColl = LIS_ITENSFESTASP.ReadCollectionByParameter(Filtro, "IDFESTA DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ITENSFESTASColl;

                lblTotalPesquisa.Text = LIS_ITENSFESTASColl.Count.ToString();
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
            LIS_ITENSFESTASColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_ITENSFESTASColl.Count.ToString();
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void txtQuant_Leave(object sender, EventArgs e)
        {
            SomaTotalItens();
        }

        private void SomaTotalItens()
        {
            try
            {
                if (txtValor.Text.Trim() == string.Empty)
                    txtValor.Text = "0,00";

                if (txtQuant.Text.Trim() == string.Empty)
                    txtQuant.Text = "0,00";

                txtValorTotal.Text = (Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuant.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técico: " + ex.Message);
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            SomaTotalItens();
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
                frm.TituloSelec = "Relação de Itens/Produtos Festas";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }


        
    }
}
