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

namespace BmsSoftware.Modulos.Mesas
{
    public partial class FrmMesa : Form
    {
        Utility Util = new Utility();

        MESAProvider MESAP = new MESAProvider();
        MESACollection MESAColl = new MESACollection();

        public FrmMesa()
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

        int _IDMESA = -1;
        public MESAEntity Entity
        {
            get
            {
                int NUMERO = Convert.ToInt32(txtNMesa.Text);
                string OBSERVACAO = txtObservacao.Text;
                string FLAGOCUPADA = chkOcupada.Checked ? "S" : "N";
                return new MESAEntity(_IDMESA, NUMERO, OBSERVACAO, FLAGOCUPADA);
            }
            set
            {

                if (value != null)
                {
                    _IDMESA = value.IDMESA;
                    txtNMesa.Text = value.NUMERO.ToString();
                    txtObservacao.Text = value.OBSERVACAO;
                    chkOcupada.Checked = value.FLAGOCUPADA == "S" ? true : false;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDMESA = -1;
                    txtNMesa.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    chkOcupada.Checked = false;
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
                    if (_IDMESA == -1)
                    {
                        //Verifique se ja existe o numero da mesa cadastrada
                        if (!VerifaNumMesaExiste(Convert.ToInt32(txtNMesa.Text)))
                        {
                            _IDMESA = MESAP.Save(Entity);
                            GetAllMesa();
                            tabControlMarca.SelectTab(0);
                            MessageBox.Show(ConfigMessage.Default.MsgSave);
                        }
                        else
                        {

                            MessageBox.Show("Número de mesa já cadastrada!",
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        _IDMESA = MESAP.Save(Entity);
                        GetAllMesa();
                        tabControlMarca.SelectTab(0);
                        MessageBox.Show(ConfigMessage.Default.MsgSave);                       
                    }                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro tecnico: " + ex.Message);

            }
        }

        private void GetAllMesa()
        {
            MESAColl = MESAP.ReadCollectionByParameter(null, "NUMERO");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = MESAColl;

            lblTotalPesquisa.Text = MESAColl.Count.ToString();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNMesa.Text) || Convert.ToInt32(txtNMesa.Text) <= 0)
            {
                errorProvider1.SetError(txtNMesa, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            //Validações da Gravação		
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.SetError(txtNMesa, "");


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            GetAllMesa();

            this.Cursor = Cursors.Default;

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
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
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            GeraProximaMesa();
            txtNMesa.Focus();
        }

        private void GeraProximaMesa()
        {
            MESACollection MesaProxColl = new MESACollection();
            MesaProxColl = MESAP.ReadCollectionByParameter(null, "NUMERO DESC");
            if (MesaProxColl.Count > 0)
            {
                txtNMesa.Text = Convert.ToString(MesaProxColl[0].NUMERO + 1);
            }
            else
            {
                txtNMesa.Text = "1";
            }
        }

        private Boolean VerifaNumMesaExiste(int NumMesa)
        {
            Boolean result = false;

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("NUMERO", "System.Int32", "=", NumMesa.ToString()));

            MESACollection MesaProxColl = new MESACollection();
            MesaProxColl = MESAP.ReadCollectionByParameter(RowRelatorio, "NUMERO DESC");
            if (MesaProxColl.Count > 0)
                result = true;
            else
                result = false;

            return result;
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            GeraProximaMesa();
            txtNMesa.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDMESA == -1)
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
                        MESAP.Delete(_IDMESA);
                        MessageBox.Show(ConfigMessage.Default.MsgDelete2);
                        Entity = null;
                        GetAllMesa();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        MessageBox.Show("Erro tecnico: " + ex.Message);
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
            if (MESAColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(MESAColl[rowindex].IDMESA);

                    Entity = MESAP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNMesa.Focus();
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Mesa");
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
                e.Graphics.DrawString("Número", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Ocupado", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = MESAColl.Count;

                while (IndexRegistro < MESAColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(MESAColl[IndexRegistro].IDMESA.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(MESAColl[IndexRegistro].NUMERO.ToString(), 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(MESAColl[IndexRegistro].FLAGOCUPADA, 1), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < MESAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + MESAColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            if (MESAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(MESAColl[indice].IDMESA);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = MESAP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtNMesa.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            MESAP.Delete(CodigoSelect);
                            MessageBox.Show(ConfigMessage.Default.MsgDelete2);
                            GetAllMesa();
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
             errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNMesa.Text))
            {
                errorProvider1.SetError(txtNMesa, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }            
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
