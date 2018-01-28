using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using System.IO;


namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmFormasPagamento : Form
    {
        Utility Util = new Utility();
        
        FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
        ITENSFORMAPAGTOProvider ITENSFORMAPAGTOP = new ITENSFORMAPAGTOProvider();

        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        ITENSFORMAPAGTOCollection ITENSFORMAPAGTOColl = new ITENSFORMAPAGTOCollection();

        public FrmFormasPagamento()
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

        private void FrmFormasPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        public int _IDFORMAPAGAMENTO = -1;
        public FORMAPAGAMENTOEntity Entity
        {
            get
            {
                string NOME = txtNome.Text;  

                if (txtPorcDesconto.Text == string.Empty)
                    txtPorcDesconto.Text = "0,00";

                decimal PORCDESCONTO = Convert.ToDecimal(txtPorcDesconto.Text);
                return new FORMAPAGAMENTOEntity(_IDFORMAPAGAMENTO, NOME, PORCDESCONTO);
            }
            set
            {

                if (value != null)
                {
                    _IDFORMAPAGAMENTO = value.IDFORMAPAGAMENTO;
                     txtPorcDesconto.Text = Convert.ToDecimal(value.PORCDESCONTO).ToString("n2");
                    txtNome.Text = value.NOME;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDFORMAPAGAMENTO = -1;
                    txtNome.Text = string.Empty;
                    txtPorcDesconto.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }

        int _IDITENSFORMAPAGTO = -1;
        public ITENSFORMAPAGTOEntity Entity2
        {
            get
            {
                int DIAS = Convert.ToInt32(TxtDias.Text);
                
                decimal PORCPAGTO = 0;
                if (txtPorPagto.Text != string.Empty)
                    PORCPAGTO =  Convert.ToDecimal(txtPorPagto.Text);

                decimal PORCJUROS = 0;
                if (txtPorPagto.Text != string.Empty)
                    PORCJUROS = Convert.ToDecimal(txtPorcJuros.Text);

                return new ITENSFORMAPAGTOEntity(_IDITENSFORMAPAGTO, _IDFORMAPAGAMENTO, DIAS, PORCPAGTO,
                                                 PORCJUROS);
            }
            set
            {

                if (value != null)
                {
                    _IDITENSFORMAPAGTO = value.IDITENSFORMAPAGTO;
                    TxtDias.Text = value.DIAS.ToString();
                    txtPorPagto.Text = Convert.ToDecimal(value.PORCPAGTO).ToString("n2");
                    txtPorcJuros.Text = Convert.ToDecimal(value.PORCJUROS).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDITENSFORMAPAGTO = -1;
                    TxtDias.Text = string.Empty;
                    txtPorPagto.Text = "0,00";
                    txtPorcJuros.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmFormasPagamento_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            GetToolStripButtonCadastro();
            GetAllFormaPagamento();
            VerificaAcesso();

            this.Cursor = Cursors.Default;

            if (_IDFORMAPAGAMENTO != -1)
            {
                Entity = FORMAPAGAMENTOP.Read(_IDFORMAPAGAMENTO);
            }
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

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);
            btnAdd2.Image = Util.GetAddressImage(15);
            btnlimpa2.Image = Util.GetAddressImage(16);
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidacoesFormaPagamento())
            {
                //Grava Forma de pagamento antes de inserir itens
                if (_IDFORMAPAGAMENTO == -1)
                    if(Validacoes())
                        _IDFORMAPAGAMENTO = FORMAPAGAMENTOP.Save(Entity);

                try
                {
                    if (_IDFORMAPAGAMENTO != -1)
                    {
                        _IDITENSFORMAPAGTO = ITENSFORMAPAGTOP.Save(Entity2);
                        GetItensFormaPagamento();
                        Entity2 = null;
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                }

            }
        }

        private Boolean ValidacoesFormaPagamento()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text) || Convert.ToInt32(TxtDias.Text) <= 0)
            {
                errorProvider1.SetError(TxtDias, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorPagto.Text) || Convert.ToDecimal(txtPorPagto.Text) > 100 || txtPorPagto.Text == "0,00")
            {
                errorProvider1.SetError(txtPorPagto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcJuros.Text) || Convert.ToDecimal(txtPorcJuros.Text) > 100)
            {
                errorProvider1.SetError(txtPorcJuros, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void txtPorPagto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorPagto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorPagto.Text) ||Convert.ToDecimal(txtPorPagto.Text) > 100)
                {
                    errorProvider1.SetError(txtPorPagto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorPagto.Text);
                    txtPorPagto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorPagto, "");
                }
            }
            else
                txtPorPagto.Text = "0,00";
        }

        private void txtPorcJuros_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcJuros.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcJuros.Text) || Convert.ToDecimal(txtPorcJuros.Text) > 100)
                {
                    errorProvider1.SetError(txtPorcJuros, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcJuros.Text);
                    txtPorcJuros.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcJuros, "");
                }
            }
            else
                txtPorcJuros.Text = "0,00";
        }

        private void TxtDias_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (TxtDias.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text) || Convert.ToDecimal(TxtDias.Text) <= 0)
                {
                    errorProvider1.SetError(TxtDias, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    errorProvider1.SetError(TxtDias, "");
                }
            }            
        }

        private void txtPorPagto_Enter(object sender, EventArgs e)
        {
            if (txtPorPagto.Text == "0,00")
                txtPorPagto.Text = string.Empty;
        }

        private void txtPorcJuros_Enter(object sender, EventArgs e)
        {
            if (txtPorcJuros.Text == "0,00")
                txtPorcJuros.Text = string.Empty;
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
                    _IDFORMAPAGAMENTO = FORMAPAGAMENTOP.Save(Entity);
                    GetAllFormaPagamento();
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void GetItensFormaPagamento()
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", _IDFORMAPAGAMENTO.ToString()));

            ITENSFORMAPAGTOColl = ITENSFORMAPAGTOP.ReadCollectionByParameter(RowRelatorio, "IDITENSFORMAPAGTO");
            DgItensFormaPagamento.AutoGenerateColumns = false;
            DgItensFormaPagamento.DataSource = ITENSFORMAPAGTOColl;
        }

        private void GetAllFormaPagamento()
        {
            FORMAPAGAMENTOColl = FORMAPAGAMENTOP.ReadCollectionByParameter(null, "NOME");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = FORMAPAGAMENTOColl;

            lblTotalPesquisa.Text = FORMAPAGAMENTOColl.Count.ToString();
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
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            GetItensFormaPagamento();
            tabControl1.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            GetItensFormaPagamento();
            tabControl1.SelectTab(0);
            txtNome.Focus();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FORMAPAGAMENTOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(FORMAPAGAMENTOColl[rowindex].IDFORMAPAGAMENTO);

                    Entity = FORMAPAGAMENTOP.Read(CodigoSelect);
                    GetItensFormaPagamento();

                    tabControl1.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (FORMAPAGAMENTOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(FORMAPAGAMENTOColl[indice].IDFORMAPAGAMENTO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = FORMAPAGAMENTOP.Read(CodigoSelect);

                    tabControl1.SelectTab(0);
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
                            FORMAPAGAMENTOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllFormaPagamento();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }

        }

        private void DgItensFormaPagamento_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (FORMAPAGAMENTOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(ITENSFORMAPAGTOColl[rowindex].IDITENSFORMAPAGTO);
                    Entity2 = ITENSFORMAPAGTOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(ITENSFORMAPAGTOColl[rowindex].IDITENSFORMAPAGTO);
                            ITENSFORMAPAGTOP.Delete(CodSelect);
                            GetItensFormaPagamento();                            
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

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDFORMAPAGAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
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
                        FORMAPAGAMENTOP.Delete(_IDFORMAPAGAMENTO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllFormaPagamento();
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
              tabControl1.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
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
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 550, 68);
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
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = FORMAPAGAMENTOColl.Count;

                while (IndexRegistro < FORMAPAGAMENTOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(FORMAPAGAMENTOColl[IndexRegistro].IDFORMAPAGAMENTO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(FORMAPAGAMENTOColl[IndexRegistro].NOME, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < FORMAPAGAMENTOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + FORMAPAGAMENTOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FORMAPAGAMENTOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControl1.SelectTab(1);
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


        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Formas de Pagamentos");
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

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtPorcDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcDesconto.Text) || Convert.ToDecimal(txtPorcDesconto.Text) > 100)
                {
                    errorProvider1.SetError(txtPorcDesconto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtPorcDesconto.Text);
                    txtPorcDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconto, "");
                }
            }
            else
                txtPorcDesconto.Text = "0,00";
        }

        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
            if (txtPorcDesconto.Text == "0,00")
                txtPorcDesconto.Text = string.Empty;
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            Grava();
            this.Cursor = Cursors.Default;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                _IDFORMAPAGAMENTO = FORMAPAGAMENTOP.Save(Entity);

                int Dias = 30;
                decimal TotalPorc = Convert.ToDecimal("100,00");
                for (int i = 0; i < Convert.ToInt32(txtXPrazos.Text); i++)
                {
                    TxtDias.Text = Convert.ToString(Dias);
                    decimal Porpagto = 100 / Convert.ToDecimal(txtXPrazos.Text);
                    txtPorPagto.Text = Porpagto.ToString("n2");
                    TotalPorc -= Convert.ToDecimal(txtPorPagto.Text);

                    //Calcula a diferença em Porcentagem de pagamento
                    if (i == Convert.ToInt32(txtXPrazos.Text) - 1)
                    {
                        Porpagto = Porpagto + TotalPorc;
                        txtPorPagto.Text = Porpagto.ToString("n2");
                    }

                    txtPorcJuros.Text = "0";
                    _IDITENSFORMAPAGTO = -1;
                    ITENSFORMAPAGTOP.Save(Entity2);

                    Dias += 30;
                }

             
                Entity2 = null;
                GetItensFormaPagamento();
            }
            
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
                DialogResult dr = MessageBox.Show("Deseja realmente remover todos os prazos de pagamentos?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    foreach (ITENSFORMAPAGTOEntity item in ITENSFORMAPAGTOColl)
                    {
                        ITENSFORMAPAGTOP.Delete(Convert.ToInt32(item.IDITENSFORMAPAGTO));
                    }


                    GetItensFormaPagamento();
                    MessageBox.Show("Registro(s) Deletado com sucesso!");
                }
        }

    }
}
