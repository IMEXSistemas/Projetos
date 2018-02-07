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
using System.Net.Http;
using Newtonsoft.Json;
using BMSworks.IMEXAppClass;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmGrupoCategoria : Form
    {
        Utility Util = new Utility();

        GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();

        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int _IDGRUPOCATEGORIA = -1;

        public FrmGrupoCategoria()
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
        

        public GRUPOCATEGORIAEntity Entity
        {
            get
            {
                string NOME = txtNome.Text;
                string OBSERVACAO = txtObservacao.Text;
                return new GRUPOCATEGORIAEntity(_IDGRUPOCATEGORIA, NOME, OBSERVACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDGRUPOCATEGORIA = value.IDGRUPOCATEGORIA;
                    txtNome.Text = value.NOME;
                    txtObservacao.Text = value.OBSERVACAO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDGRUPOCATEGORIA = -1;
                    txtNome.Text = string.Empty;
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
                    _IDGRUPOCATEGORIA = GRUPOCATEGORIAP.Save(Entity);
                    SalveIMEXAPP(Entity);
                    GetAllGrupoCategoria();                  
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }  
        
        private void SalveIMEXAPP(GRUPOCATEGORIAEntity GRUPOCATEGORIATy)
        {
            try
            {
                if(CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    CATEGORIAPRODUTOIMEXAPPProvider CATEGORIAPRODUTOIMEXAPPP = new CATEGORIAPRODUTOIMEXAPPProvider();
                    CATEGORIAPRODUTOIMEXAPPEntity CATEGORIAPRODUTOIMEXAPPTy = new CATEGORIAPRODUTOIMEXAPPEntity();
                    CATEGORIAPRODUTOIMEXAPPTy.XMEUID = GRUPOCATEGORIATy.IDGRUPOCATEGORIA.ToString();
                    CATEGORIAPRODUTOIMEXAPPTy.XCATEGORIA = GRUPOCATEGORIATy.NOME;
                    CATEGORIAPRODUTOIMEXAPPP.Save(CATEGORIAPRODUTOIMEXAPPTy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: "+ ex.Message);


            }
        }

        private void GetAllGrupoCategoria()
        {
            GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null, "NOME");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = GRUPOCATEGORIAColl;

            lblTotalPesquisa.Text = GRUPOCATEGORIAColl.Count.ToString();
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
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            
            GetToolStripButtonCadastro();
            GetAllGrupoCategoria();

            if (_IDGRUPOCATEGORIA != -1)
                Entity = GRUPOCATEGORIAP.Read(_IDGRUPOCATEGORIA);

            CONFISISTEMATy = CONFISISTEMAP.Read(1);

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

            btnSeach.Image = Util.GetAddressImage(20);
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabcontrolGrupoCategoria.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabcontrolGrupoCategoria.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDGRUPOCATEGORIA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabcontrolGrupoCategoria.SelectTab(1);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        GRUPOCATEGORIAP.Delete(_IDGRUPOCATEGORIA);
                        DeleteIMEXAPP(_IDGRUPOCATEGORIA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllGrupoCategoria();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }

        private void DeleteIMEXAPP(int IDREGISTRO)
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    CATEGORIAPRODUTOIMEXAPPProvider CATEGORIAPRODUTOIMEXAPPP = new CATEGORIAPRODUTOIMEXAPPProvider();
                    int result = CATEGORIAPRODUTOIMEXAPPP.GetID(IDREGISTRO);
                    CATEGORIAPRODUTOIMEXAPPP.Delete(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);


            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabcontrolGrupoCategoria.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabcontrolGrupoCategoria.SelectTab(1);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GRUPOCATEGORIAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(GRUPOCATEGORIAColl[rowindex].IDGRUPOCATEGORIA);

                    Entity = GRUPOCATEGORIAP.Read(CodigoSelect);

                    tabcontrolGrupoCategoria.SelectTab(0);
                    txtNome.Focus();
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Grupo/Categoria");
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

                int NumerorRegistros = GRUPOCATEGORIAColl.Count;

                while (IndexRegistro < GRUPOCATEGORIAColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(GRUPOCATEGORIAColl[IndexRegistro].IDGRUPOCATEGORIA.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(GRUPOCATEGORIAColl[IndexRegistro].NOME, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < GRUPOCATEGORIAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + GRUPOCATEGORIAColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            ImprimirListaGeral();
        }

        private void FrmGrupoCategoria_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmGrupoCategoria_KeyDown(object sender, KeyEventArgs e)
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
            if (GRUPOCATEGORIAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(GRUPOCATEGORIAColl[indice].IDGRUPOCATEGORIA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = GRUPOCATEGORIAP.Read(CodigoSelect);

                    tabcontrolGrupoCategoria.SelectTab(0);
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
                            GRUPOCATEGORIAP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllGrupoCategoria();
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

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(RowRelatorio, "NOME");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = GRUPOCATEGORIAColl;

                lblTotalPesquisa.Text = GRUPOCATEGORIAColl.Count.ToString(); 

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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Grupo/Categoria dos Produtos";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Grupo/Categoria dos Produtos");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
