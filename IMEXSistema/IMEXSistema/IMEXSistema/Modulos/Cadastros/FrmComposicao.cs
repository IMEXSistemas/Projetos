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
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmComposicao : Form
    {
        Utility Util = new Utility();

        COMPOSICAOProvider COMPOSICAOP = new COMPOSICAOProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        COMPOSPRODUTOProvider COMPOSPRODUTOP = new COMPOSPRODUTOProvider();
        LIS_COMPOSPRODUTOProvider LIS_COMPOSPRODUTOP = new LIS_COMPOSPRODUTOProvider();

        COMPOSICAOCollection COMPOSICAOColl = new COMPOSICAOCollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_COMPOSPRODUTOCollection LIS_COMPOSPRODUTOColl = new LIS_COMPOSPRODUTOCollection();

        public FrmComposicao()
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



        int _IDCOMPOSICAO = -1;
        public COMPOSICAOEntity Entity
        {
            get
            {
                string NOMECOMPOSICAO = txtNome.Text;
                string DESCRICAO = txtObservacao.Text;
                decimal? VALORTOTAL = Convert.ToDecimal(txtValorTotal.Text);

                return new COMPOSICAOEntity(_IDCOMPOSICAO, NOMECOMPOSICAO, VALORTOTAL, DESCRICAO);
            }
            set
            {

                if (value != null)
                {
                    _IDCOMPOSICAO = value.IDCOMPOSICAO;

                    //Preenche produtos da composição
                    GetAllProdutosComposicao(_IDCOMPOSICAO);

                    txtNome.Text = value.NOMECOMPOSICAO;

                    txtValorTotal.Text = Convert.ToString(value.VALORTOTAL);
                    if (value.VALORTOTAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorTotal.Text);
                        txtValorTotal.Text = string.Format("{0:n2}", f);
                    }


                    txtObservacao.Text = value.DESCRICAO;
                    errorProvider1.SetError(txtNome, "");
                }
                else
                {
                    _IDCOMPOSICAO = -1;
                    //limpa grid da composição
                    GetAllProdutosComposicao(-1);

                    txtNome.Text = string.Empty;
                    txtValorTotal.Text = "0,00";
                    txtObservacao.Text = string.Empty;
                    errorProvider1.SetError(txtNome, "");
                }
            }
        }


        int _IDCOMPOSPRODUTO = -1;
        public COMPOSPRODUTOEntity Entity2
        {
            get
            {
                decimal QUANTIDADE = Convert.ToDecimal(txtQuant.Text);
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);

                return new COMPOSPRODUTOEntity(_IDCOMPOSPRODUTO, QUANTIDADE, IDPRODUTO, _IDCOMPOSICAO);
            }
            set
            {

                if (value != null)
                {
                    _IDCOMPOSPRODUTO = value.IDCOMPOSPRODUTO;

                    txtQuant.Text = value.QUANTIDADE.ToString();
                    if (value.QUANTIDADE != null)
                    {
                        Double f = Convert.ToDouble(txtQuant.Text);
                        txtQuant.Text = string.Format("{0:n2}", f);
                    }

                    cbProduto.SelectedValue = value.IDPRODUTO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCOMPOSPRODUTO = -1;
                    txtQuant.Text = "0,00";
                    cbProduto.SelectedIndex = 0;
                    errorProvider1.Clear();
                }
            }
        }


        private void FrmComposicao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            GetToolStripButtonCadastro();
            GetAllComposicao();
            GetDropProdutoComposicao();

            btnCadProdutos.Image = Util.GetAddressImage(6);
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
                    _IDCOMPOSICAO = COMPOSICAOP.Save(Entity);
                    GetAllComposicao();
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
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotal.Text))
            {
                 errorProvider1.SetError(txtValorTotal, ConfigMessage.Default.FieldErro);
                 MessageBox.Show(ConfigMessage.Default.FieldErro);
                 txtValorTotal.Focus();
                 result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void GetAllComposicao()
        {
            COMPOSICAOColl = COMPOSICAOP.ReadCollectionByParameter(null, "NOMECOMPOSICAO");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = COMPOSICAOColl;

            lblTotalPesquisa.Text = COMPOSICAOColl.Count.ToString();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControlComposicao.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControlComposicao.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDCOMPOSICAO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlComposicao.SelectTab(2);
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
                        COMPOSICAOP.Delete(_IDCOMPOSICAO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllComposicao();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (COMPOSICAOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(COMPOSICAOColl[rowindex].IDCOMPOSICAO);

                    Entity = COMPOSICAOP.Read(CodigoSelect);

                    tabControlComposicao.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlComposicao.SelectTab(2);
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {

        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlComposicao.SelectTab(2);
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

                int NumerorRegistros = COMPOSICAOColl.Count;

                while (IndexRegistro < COMPOSICAOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(COMPOSICAOColl[IndexRegistro].IDCOMPOSICAO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(COMPOSICAOColl[IndexRegistro].NOMECOMPOSICAO, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < COMPOSICAOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + COMPOSICAOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (COMPOSICAOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlComposicao.SelectTab(2);
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Composições");
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

        private void txtQuant_Leave(object sender, EventArgs e)
        {
            if (txtQuant.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
                {
                    errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtQuant.Text);
                    txtQuant.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtQuant, "");
                }
            }
            else
            {
                txtQuant.Text = "0,000";
            }
        }

        private void txtValorTotal_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorTotal.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorTotal.Text))
                {
                    errorProvider1.SetError(txtValorTotal, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorTotal.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorTotal.Text);
                    txtValorTotal.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorTotal, "");
                    e.Cancel = false;
                }
            }
        }

        private void txtValorTotal_Leave(object sender, EventArgs e)
        {
            if (txtValorTotal.Text == string.Empty)
                txtValorTotal.Text = "0,00";
        }

        private void btnAddProduto_Click(object sender, EventArgs e)
        {
             if (_IDCOMPOSICAO == -1)
                MessageBox.Show("Antes de adicionar o produto é necessário gravar a composição!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
             else if (cbProduto.SelectedIndex == 0)
             {
                 errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                 Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
             }
             else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
             {
                 errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                 MessageBox.Show(ConfigMessage.Default.FieldErro);
             }
             else
             {
                 try
                 {
                     COMPOSPRODUTOP.Save(Entity2);
                     Entity2 = null;
                     GetAllProdutosComposicao(_IDCOMPOSICAO);

                     //Salva dados da composicao
                     COMPOSICAOP.Save(Entity);

                     Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                 }
                 catch (Exception)
                 {
                     MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                 }
             }
        }

        private void GetAllProdutosComposicao(int IDCOMPOSICAO)
        {
            RowsFiltroCollection RowFiltro = new RowsFiltroCollection();
            RowFiltro.Add(new RowsFiltro("IDCOMPOSICAO", "System.Int32", "=", IDCOMPOSICAO.ToString()));

            LIS_COMPOSPRODUTOColl = LIS_COMPOSPRODUTOP.ReadCollectionByParameter(RowFiltro, "NOMEPRODUTO");
            dataGridProdCompos.AutoGenerateColumns = false;
            dataGridProdCompos.DataSource = LIS_COMPOSPRODUTOColl;

            SomaComposiscao();
        }

        private void SomaComposiscao()
        {
            decimal result = 0;
            foreach (LIS_COMPOSPRODUTOEntity item in LIS_COMPOSPRODUTOColl)
            {
                   //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));

                result += Convert.ToDecimal(item.QUANTIDADE * PRODUTOStY.VALORVENDA1);
            }

            txtValorTotal.Text = result.ToString("n2");
        }


        private void GetDropProdutoComposicao()
        {
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

        private void dataGridProdCompos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_COMPOSPRODUTOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_COMPOSPRODUTOColl[rowindex].IDCOMPOSPRODUTO);
                    Entity2 = COMPOSPRODUTOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_COMPOSPRODUTOColl[rowindex].IDCOMPOSPRODUTO);
                            COMPOSPRODUTOP.Delete(CodSelect);
                            GetAllProdutosComposicao(_IDCOMPOSICAO);
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

        private void FrmComposicao_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Entity2 = null;
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
                    }
                }
            }
        }

        private void cbProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void FrmComposicao_KeyDown(object sender, KeyEventArgs e)
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
            if (COMPOSICAOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(COMPOSICAOColl[indice].IDCOMPOSICAO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = COMPOSICAOP.Read(CodigoSelect);

                    tabControlComposicao.SelectTab(0);
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
                            COMPOSICAOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllComposicao();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                GetDropProdutoComposicao();
                cbProduto.SelectedValue = CodSelec;
            }
        }

       
    }
}
