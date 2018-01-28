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
    public partial class FrmRelatorioPersonalizado : Form
    {
        CAMPOSTELACollection CAMPOSTELAColl = new CAMPOSTELACollection();
        TELASCollection TELASColl = new TELASCollection();
        RELATORIOPERSONALIZADOCollection RELATORIOPERSONALIZADOColl = new RELATORIOPERSONALIZADOCollection();
        LIS_CAMPOSRELATPERSCollection LIS_CAMPOSRELATPERSColl = new LIS_CAMPOSRELATPERSCollection();

        LIS_CAMPOSRELATPERSProvider LIS_CAMPOSRELATPERSP = new LIS_CAMPOSRELATPERSProvider();
        CAMPOSTELAProvider CAMPOSTELAP = new CAMPOSTELAProvider();
        RELATORIOPERSONALIZADOProvider RELATORIOPERSONALIZADOP = new RELATORIOPERSONALIZADOProvider();
        CAMPOSRELATORIOPERSONALIZADOProvider CAMPOSRELATORIOPERSONALIZADOP = new CAMPOSRELATORIOPERSONALIZADOProvider();


        Utility Util = new Utility();
        public FrmRelatorioPersonalizado()
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
        

        int _IDRELATORIOPERSONALIZADO = -1;
        public RELATORIOPERSONALIZADOEntity Entity
        {
            get
            {
                string NOMERELATORIO = txtNome.Text;
                string OBSERVACAO = txtObservacao.Text;
                int ORIENTACAO = rdbRetrato.Checked ? 1 : 0; //true = 1 ; false = 0
                int IDTELA = Convert.ToInt32(cbTela.SelectedValue.ToString());               

                return new RELATORIOPERSONALIZADOEntity(_IDRELATORIOPERSONALIZADO,
                                                        NOMERELATORIO, OBSERVACAO, ORIENTACAO, IDTELA);
            }
            set
            {

                if (value != null)
                {
                    _IDRELATORIOPERSONALIZADO = value.IDRELATORIOPERSONALIZADO;
                    txtNome.Text = value.NOMERELATORIO ;
                    txtObservacao.Text = value.OBSERVACAO;
                    rdbRetrato.Checked = value.ORIENTACAO == 1 ? true : false;//true = 1 ; false = 0
                    rdbPaisagem.Checked = !rdbRetrato.Checked;

                    if (rdbRetrato.Checked)
                        pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(8));
                    else
                        pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(9));

                    cbTela.SelectedValue =value.IDTELA;
                    GetCamposTela(Convert.ToInt32(value.IDTELA));
                    GetCamposRelatPersonalizado(value.IDRELATORIOPERSONALIZADO);
                    errorProvider1.SetError(txtNome, "");
                }
                else
                {
                    _IDRELATORIOPERSONALIZADO = -1;
                    txtNome.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    rdbRetrato.Checked = true;
                    rdbPaisagem.Checked = !rdbRetrato.Checked;
                    pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(8));
                    cbTela.SelectedIndex = 0;
                    GetCamposTela(-1);
                    GetCamposRelatPersonalizado(-1);
                    errorProvider1.SetError(txtNome, "");
                }
            }
        }

        int _IDCAMPOSRELATPERSONAZ = -1;
        public CAMPOSRELATORIOPERSONALIZADOEntity Entity2
        {
            get
            {
                int IDCAMPO = Convert.ToInt32(cbCamposTela.SelectedValue.ToString());
                int TAMANHO = Convert.ToInt32(txtTamanho.Text);
                int ORDEM = ckordenar.Checked ? 1 : 0 ; //true = 1 ; false = 0 
                string SOMATORIO = ckSomatorio.Checked ? "1" : "0"; //true = 1 ; false = 0

                return new CAMPOSRELATORIOPERSONALIZADOEntity(_IDCAMPOSRELATPERSONAZ, IDCAMPO, TAMANHO, ORDEM,
                                                              SOMATORIO, _IDRELATORIOPERSONALIZADO);
            }
            set
            {

                if (value != null)
                {
                    _IDCAMPOSRELATPERSONAZ = value.IDCAMPOSRELATPERSONAZ;
                    cbCamposTela.SelectedValue = value.IDCAMPO;
                    PreencheTipoCampo(Convert.ToInt32(value.IDCAMPO));
                    txtTamanho.Text = value.TAMANHO.ToString();
                    ckSomatorio.Checked = value.SOMATORIO == "1" ? true : false;//true = 1 ; false = 0 
                    ckordenar.Checked = value.ORDEM == 1 ? true : false;//true = 1 ; false = 0 
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCAMPOSRELATPERSONAZ = -1;
                    cbCamposTela.SelectedIndex = 0;
                    ckSomatorio.Checked = false;
                    ckordenar.Checked = false; 
                    txtTamanho.Text = string.Empty;
                    rdbRetrato.Checked = false;
                    errorProvider1.Clear();
                }
            }
        }


        private void FrmRelatorioPersonalizado_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            GetDropTela();
            cbCamposTela.SelectedIndex = 0;
            GetAllRelatPersonalizado();

            this.Cursor = Cursors.Default;
        }

        private void GetDropTela()
        {
            TELASProvider TELASP = new TELASProvider();

            cbTela.DisplayMember = "NOME";
            cbTela.ValueMember = "IDTELA";

            TELASColl = TELASP.ReadCollectionByParameter(null, "NOME");

            TELASEntity TELASTy = new TELASEntity();
            TELASTy.NOME = ConfigMessage.Default.MsgDrop;
            TELASTy.IDTELA = -1;
            TELASColl.Add(TELASTy);

            Phydeaux.Utilities.DynamicComparer<TELASEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TELASEntity>(cbTela.DisplayMember);

            TELASColl.Sort(comparer.Comparer);
            cbTela.DataSource = TELASColl;
            cbTela.SelectedIndex = 0;
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

            pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(8));
           
        }       

        private void rdbRetrato_Click(object sender, EventArgs e)
        {
            rdbRetrato.Checked = true;
            pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(8));
            rdbPaisagem.Checked = false;
        }

        private void rdbPaisagem_Click(object sender, EventArgs e)
        {
            rdbRetrato.Checked = false;
            rdbPaisagem.Checked = true;
            pBoxOrientacao.Image = new Bitmap(Util.GetAddressImage(9));
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTela_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCamposTela(Convert.ToInt32(cbTela.SelectedValue.ToString()));
        }

        private void GetCamposTela(int IdTela)
        {
            cbCamposTela.DisplayMember = "NOMECAMPOS";
            cbCamposTela.ValueMember = "IDCAMPOSTELA";

            RowsFiltroCollection RowsFiltroCamposTela = new RowsFiltroCollection();
            RowsFiltroCamposTela.Add(new RowsFiltro("IDTELA", "System.Int32", "=", IdTela.ToString()));

            CAMPOSTELAColl = CAMPOSTELAP.ReadCollectionByParameter(RowsFiltroCamposTela, "NOMECAMPOS");

            CAMPOSTELAEntity CAMPOSTELATy = new CAMPOSTELAEntity();
            CAMPOSTELATy.NOMECAMPOS = ConfigMessage.Default.MsgDrop;
            CAMPOSTELATy.IDCAMPOSTELA = -1;
            CAMPOSTELAColl.Add(CAMPOSTELATy);

            Phydeaux.Utilities.DynamicComparer<CAMPOSTELAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CAMPOSTELAEntity>(cbCamposTela.DisplayMember);
            CAMPOSTELAColl.Sort(comparer.Comparer);

            cbCamposTela.DataSource = CAMPOSTELAColl;
            cbCamposTela.SelectedIndex = 0;

            
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (txtTamanho.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtTamanho.Text))
                {
                    errorProvider1.SetError(txtTamanho, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtTamanho.Focus();
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtTamanho, "");
                }
            }
        }

        private void cbCamposTela_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PreencheTipoCampo(Convert.ToInt32(cbCamposTela.SelectedValue.ToString()));
        }

        private void PreencheTipoCampo(int IdCampoTela)
        {
             CAMPOSTELAEntity CampoTelaTy = new CAMPOSTELAEntity();
             CampoTelaTy = CAMPOSTELAP.Read(IdCampoTela);

             if (CampoTelaTy.TIPO == "INTEGER" || CampoTelaTy.TIPO == "NUMERIC")
                 ckSomatorio.Enabled = true;
             else
             {
                 ckSomatorio.Enabled = false;
                 ckSomatorio.Checked = false;
             }


            lblTipo.Text = CampoTelaTy.TIPO;
            txtTamanho.Text = CampoTelaTy.TAMANHO.ToString();
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
                    _IDRELATORIOPERSONALIZADO = RELATORIOPERSONALIZADOP.Save(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    GetAllRelatPersonalizado();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void GetAllRelatPersonalizado()
        {
            RELATORIOPERSONALIZADOColl = RELATORIOPERSONALIZADOP.ReadCollectionByParameter(null, "NOMERELATORIO");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = RELATORIOPERSONALIZADOColl;

            lblTotalPesquisa.Text = RELATORIOPERSONALIZADOColl.Count.ToString();
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
            else if (cbTela.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbTela, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }           
            else
            {
                errorProvider1.SetError(txtNome, "");
                errorProvider1.SetError(cbTela, "");
                errorProvider1.SetError(cbCamposTela, "");
            }


            return result;
        }

        private void FrmRelatorioPersonalizado_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void txtNome_Validating(object sender, CancelEventArgs e)
        {

        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlRelatPersonalizada.SelectTab(0);
            txtNome.Focus();            
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlRelatPersonalizada.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDRELATORIOPERSONALIZADO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlRelatPersonalizada.SelectTab(2);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        RELATORIOPERSONALIZADOP.Delete(_IDRELATORIOPERSONALIZADO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllRelatPersonalizado();
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

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlRelatPersonalizada.SelectTab(2);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlRelatPersonalizada.SelectTab(2);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RELATORIOPERSONALIZADOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(RELATORIOPERSONALIZADOColl[rowindex].IDRELATORIOPERSONALIZADO);

                    Entity = RELATORIOPERSONALIZADOP.Read(CodigoSelect);

                    tabControlRelatPersonalizada.SelectTab(0);
                    txtNome.Focus();
                }
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
                e.Graphics.DrawString("Observação", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);
                 
                int NumerorRegistros =  RELATORIOPERSONALIZADOColl.Count;

                while (IndexRegistro < RELATORIOPERSONALIZADOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(RELATORIOPERSONALIZADOColl[IndexRegistro].IDRELATORIOPERSONALIZADO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(RELATORIOPERSONALIZADOColl[IndexRegistro].NOMERELATORIO, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(RELATORIOPERSONALIZADOColl[IndexRegistro].OBSERVACAO, 100), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < RELATORIOPERSONALIZADOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + RELATORIOPERSONALIZADOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Relatório Personalizado");
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

        private void btnIncluirCampo_Click(object sender, EventArgs e)
        {
            if (_IDRELATORIOPERSONALIZADO == -1)
                MessageBox.Show("Antes de adicionar os campos é necessário gravar o relatório!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

            else
            {
                try
                {
                    if (Validacoes2())
                    {
                        _IDCAMPOSRELATPERSONAZ = CAMPOSRELATORIOPERSONALIZADOP.Save(Entity2);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        GetCamposRelatPersonalizado(_IDRELATORIOPERSONALIZADO);
                        Entity2 = null;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

                }
            }
        }

        private void GetCamposRelatPersonalizado(int IDRELATORIOPERSONALIZADO)
        {
            RowsFiltroCollection RowsFiltroCamposRelatPersonalizado = new RowsFiltroCollection();
            RowsFiltroCamposRelatPersonalizado.Add(new RowsFiltro("IDRELATORIOPERSONALIZADO", "System.Int32", "=", IDRELATORIOPERSONALIZADO.ToString()));

            LIS_CAMPOSRELATPERSColl = LIS_CAMPOSRELATPERSP.ReadCollectionByParameter(RowsFiltroCamposRelatPersonalizado, "IDCAMPOSRELATPERSONAZ");
            dataGridCampos.AutoGenerateColumns = false;
            dataGridCampos.DataSource = LIS_CAMPOSRELATPERSColl;
        }

        private Boolean Validacoes2()
        {
            Boolean result = true;

            if (txtTamanho.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtTamanho, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtTamanho.Text))
            {
                    errorProvider1.SetError(txtTamanho, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtTamanho.Focus();
            }
            else
                errorProvider1.SetError(txtTamanho, "");


            return result;
        }

        private void dataGridCampos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_CAMPOSRELATPERSColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_CAMPOSRELATPERSColl[rowindex].IDCAMPOSRELATPERSONAZ);
                    Entity2 = CAMPOSRELATORIOPERSONALIZADOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_CAMPOSRELATPERSColl[rowindex].IDCAMPOSRELATPERSONAZ);
                            CAMPOSRELATORIOPERSONALIZADOP.Delete(CodSelect);
                            GetCamposRelatPersonalizado(_IDRELATORIOPERSONALIZADO);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Entity2 = null;

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
            if (RELATORIOPERSONALIZADOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(RELATORIOPERSONALIZADOColl[indice].IDRELATORIOPERSONALIZADO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = RELATORIOPERSONALIZADOP.Read(CodigoSelect);

                    tabControlRelatPersonalizada.SelectTab(0);
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
                            RELATORIOPERSONALIZADOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllRelatPersonalizado();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }
    }
}
