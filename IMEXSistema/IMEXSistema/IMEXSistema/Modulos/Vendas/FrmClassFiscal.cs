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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmClassFiscal : Form
    {
        Utility Util = new Utility();

        CLASSFISCALProvider CLASSFISCALP = new CLASSFISCALProvider();
        CLASSFISCALCollection CLASSFISCALColl = new CLASSFISCALCollection();
        MENSAGEMNFECollection MENSAGEMNFEColl = new MENSAGEMNFECollection();

        public FrmClassFiscal()
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

        int _IDClassFiscal = -1;
        public CLASSFISCALEntity Entity
        {
            get
            {
                string CODIGO =txtClassiFical.Text;
                decimal AliqICMS = Convert.ToDecimal(txtAliICMS.Text);
                decimal BaseReduzida = Convert.ToDecimal(txtBaseReduzida.Text);
                
                int? IDMENSAGEMNFE = null;
                if(cbMensagem.SelectedIndex > 0)
                    IDMENSAGEMNFE = Convert.ToInt32(cbMensagem.SelectedValue);

                return new CLASSFISCALEntity(_IDClassFiscal, CODIGO, AliqICMS, BaseReduzida,
                                             IDMENSAGEMNFE   );
            }
            set
            {

                if (value != null)
                {
                    _IDClassFiscal = value.IDCLASSFISCAL;
                    txtClassiFical.Text = value.CODIGO;
                    txtAliICMS.Text = Convert.ToDecimal(value.ALIQICMS).ToString("n2");
                    txtBaseReduzida.Text = Convert.ToDecimal(value.BASEREDUZIDA).ToString("n2");

                    if (value.IDMENSAGEMNFE != null)
                        cbMensagem.SelectedValue = value.IDMENSAGEMNFE;
                    else
                        cbMensagem.SelectedIndex = 0;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDClassFiscal = -1;
                    txtClassiFical.Text = string.Empty;
                    txtAliICMS.Text = "0,00";
                    txtBaseReduzida.Text = "0,00";
                    cbMensagem.SelectedIndex = 0;
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
                    _IDClassFiscal = CLASSFISCALP.Save(Entity);
                    GetAllCST();
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void GetAllCST()
        {
            CLASSFISCALColl  = CLASSFISCALP.ReadCollectionByParameter(null, "CODIGO");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = CLASSFISCALColl ;

            lblTotalPesquisa.Text = CLASSFISCALColl .Count.ToString();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtClassiFical.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtClassiFical, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtClassiFical, "");


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            GetAllCST();
            GetDropMensagem();
            btnCadMensagem.Image = Util.GetAddressImage(6);

            this.Cursor = Cursors.Default;
        }

        private void GetDropMensagem()
        {
            MENSAGEMNFEProvider MENSAGEMNFEP = new MENSAGEMNFEProvider();
            MENSAGEMNFEColl = MENSAGEMNFEP.ReadCollectionByParameter(null);

            cbMensagem.DisplayMember = "NOME";
            cbMensagem.ValueMember = "IDMENSAGEMNFE";

            MENSAGEMNFEEntity MENSAGEMNFETy = new MENSAGEMNFEEntity();
            MENSAGEMNFETy.NOME = ConfigMessage.Default.MsgDrop;
            MENSAGEMNFETy.IDMENSAGEMNFE = -1;
            MENSAGEMNFEColl.Add(MENSAGEMNFETy);

            Phydeaux.Utilities.DynamicComparer<MENSAGEMNFEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MENSAGEMNFEEntity>(cbMensagem.DisplayMember);

            MENSAGEMNFEColl.Sort(comparer.Comparer);
            cbMensagem.DataSource = MENSAGEMNFEColl;

           // cbMensagem.SelectedIndex = 0;
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
            txtClassiFical.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtClassiFical.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDClassFiscal == -1)
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
                        CLASSFISCALP.Delete(_IDClassFiscal);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllCST();
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
            if (CLASSFISCALColl .Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(CLASSFISCALColl [rowindex].IDCLASSFISCAL);

                    Entity = CLASSFISCALP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtClassiFical.Focus();
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Classificação Fiscal");
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
                e.Graphics.DrawString("Class. Fiscal", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Al. ICMS", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, 170);
                e.Graphics.DrawString("Redu. ICMS", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = CLASSFISCALColl .Count;

                while (IndexRegistro < CLASSFISCALColl .Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(CLASSFISCALColl [IndexRegistro].IDCLASSFISCAL.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDecimal(CLASSFISCALColl[IndexRegistro].ALIQICMS).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 150, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDecimal(CLASSFISCALColl[IndexRegistro].BASEREDUZIDA).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 350, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < CLASSFISCALColl .Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + CLASSFISCALColl .Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            if (CLASSFISCALColl .Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(CLASSFISCALColl [indice].IDCLASSFISCAL);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = CLASSFISCALP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
                    txtClassiFical.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CLASSFISCALP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllCST();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void txtAliICMS_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();

            if (txtAliICMS.Text == string.Empty)
                txtAliICMS.Text = "0,00";

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliICMS.Text))
            {
                errorProvider1.SetError(txtAliICMS, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtAliICMS.Text);
                txtAliICMS.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtAliICMS, "");
            } 
        }

        private void txtBaseReduzida_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtBaseReduzida.Text == string.Empty)
                txtBaseReduzida.Text = "0,00";


            if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseReduzida.Text))
            {
                errorProvider1.SetError(txtBaseReduzida, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtBaseReduzida.Text);
                txtBaseReduzida.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtBaseReduzida, "");
            } 
        }

        private void txtAliICMS_Enter(object sender, EventArgs e)
        {
            if (txtAliICMS.Text == "0,00")
                txtAliICMS.Text = string.Empty;
        }

        private void txtBaseReduzida_Enter(object sender, EventArgs e)
        {
            if (txtBaseReduzida.Text == "0,00")
                txtBaseReduzida.Text = string.Empty;
        }

        private void btnCadMensagem_Click(object sender, EventArgs e)
        {
            using (FrmMensagem frm = new FrmMensagem())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbMensagem.SelectedValue);
                GetDropMensagem();

               cbMensagem.SelectedValue = CodSelec;
            }
        }              
      
    }
}
