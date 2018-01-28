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
using BmsSoftware.Modulos.Cadastros;
using System.Security.Cryptography;
using BMSSoftware.Modulos.Cadastros;
using System.IO;
using BMSSoftware;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmUsuario : Form
    {
        Utility Util = new Utility();
        SecurityString SecurityS = new SecurityString();

        LIS_USUARIOCollection LIS_USUARIOColl = new LIS_USUARIOCollection();
        NIVELUSUARIOCollection NIVELUSUARIOColl = new NIVELUSUARIOCollection();

        USUARIOProvider USUARIOP = new USUARIOProvider();
        LIS_USUARIOProvider LIS_USUARIOP = new LIS_USUARIOProvider();

        public FrmUsuario()
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

        int _IDUSUARIO = -1;
        public USUARIOEntity Entity
        {
            get
            {
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
                int IDNIVELUSUARIO = Convert.ToInt32(cbnivel.SelectedValue);
                string FLAGATIVO = ckStatus.Checked ? "S" : "N";
                string NOMEUSUARIO = txtNome.Text;

                //Encripta a senha para ser enviado ao banco
                string SENHAUSUARIO = SecurityS.encrypt(txtSenha.Text);
                string OBSERVACAO = txtObservacao.Text;

                return new USUARIOEntity(_IDUSUARIO, IDFUNCIONARIO, IDNIVELUSUARIO, FLAGATIVO, NOMEUSUARIO,
                                         SENHAUSUARIO, OBSERVACAO);
            }
            set
            {
                if (value != null)
                {
                    _IDUSUARIO = value.IDUSUARIO;
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    cbnivel.SelectedValue = value.IDNIVELUSUARIO;
                    ckStatus.Checked = value.FLAGATIVO == "S" ? true: false;
                    txtNome.Text = value.NOMEUSUARIO;

                    //Descriptografa a senha do banco
                    txtSenha.Text = SecurityS.decrypt(value.SENHAUSUARIO);
                    txtSenhaConf.Text = SecurityS.decrypt(value.SENHAUSUARIO);

                    txtObservacao.Text = value.OBSERVACAO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDUSUARIO = -1;
                    cbFuncionario.SelectedIndex = 0;
                    cbnivel.SelectedIndex = 0;
                    ckStatus.Checked = true;
                    txtNome.Text = string.Empty;
                    txtSenha.Text = string.Empty;
                    txtSenhaConf.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetDropFuncionario();
            GetDropNivel();
            GetToolStripButtonCadastro();
            GetAllUsuario();

            bntCadFuncionairo.Image = Util.GetAddressImage(6);
            btnCadNivel.Image = Util.GetAddressImage(6);

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

        private void GetDropFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOCollection FuncionarioColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FuncionarioColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

            FuncionarioColl.Sort(comparer.Comparer);

            cbFuncionario.DataSource = FuncionarioColl;
        }

        private void FrmUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
        }     

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_IDUSUARIO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
            {
                Grava();
            }
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDUSUARIO = USUARIOP.Save(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");                    
                    GetAllUsuario();
                    errorProvider1.Clear();
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
            if (txtNome.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtNome, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtSenha.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtSenha, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtSenha.Text != txtSenhaConf.Text)
            {
                errorProvider1.SetError(txtSenhaConf, "Senhas não podem ser diferentes!");
                errorProvider1.SetError(txtSenha, "Senhas não podem ser diferentes!");
                MessageBox.Show("Senhas não podem ser diferentes!");
                result = false;
            }
            else if (cbFuncionario.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (_IDUSUARIO == -1 && VerificaUsuarioExiste(txtNome.Text))
            {
                errorProvider1.SetError(txtNome, "Usuário já cadastrado!");
                MessageBox.Show("Usuário já cadastrado!");
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

        private Boolean VerificaUsuarioExiste(string NomeUsuario)
        {
            Boolean result = false;
           
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("NOMEUSUARIO", "System.String", "=", NomeUsuario));

            LIS_USUARIOCollection LIS_USUARIO2Coll = new LIS_USUARIOCollection();
            LIS_USUARIO2Coll = LIS_USUARIOP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_USUARIO2Coll.Count > 0)
                result = true;

            return result;
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetAllUsuario()
        {
            try
            {
                LIS_USUARIOColl = LIS_USUARIOP.ReadCollectionByParameter(null, "NOMEUSUARIO");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_USUARIOColl;

                lblTotalPesquisa.Text = LIS_USUARIOColl.Count.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_USUARIOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_USUARIOColl[rowindex].IDUSUARIO);

                    Entity = USUARIOP.Read(CodigoSelect);

                    tabControlUsuario.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                tabControlUsuario.SelectTab(0);
                cbFuncionario.Focus();
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                tabControlUsuario.SelectTab(0);
                cbFuncionario.Focus();
            }
        }

        private Boolean VerificaPlanos()
        {
            Boolean result = true;

            try
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S")
                {
                    USUARIOCollection USUARIOColl_Total = new USUARIOCollection();
                    USUARIOColl_Total = USUARIOP.ReadCollectionByParameter(null);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos));

                    if (RECURSOSPLANOTy != null)
                    {
                        int QuantUsuarios = Convert.ToInt32(RECURSOSPLANOTy.USUARIOS);

                        if (USUARIOColl_Total.Count < QuantUsuarios)
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show("Limite de usuários atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

                            result = false;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }



        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDUSUARIO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlUsuario.SelectTab(1);
            }
            else if (_IDUSUARIO == 1)
            {
                MessageBox.Show("O Usuário Administrador não é possível excluir!");
                tabControlUsuario.SelectTab(1);
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
                        USUARIOP.Delete(_IDUSUARIO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllUsuario();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        MessageBox.Show("Erro técnico: " + ex.Message); 
                    }
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlUsuario.SelectTab(1);
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlUsuario.SelectTab(1);
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
                e.Graphics.DrawString("Nome do Usuário", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Nível", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 250, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 350, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_USUARIOColl.Count;

                while (IndexRegistro < LIS_USUARIOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_USUARIOColl[IndexRegistro].IDUSUARIO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_USUARIOColl[IndexRegistro].NOMEUSUARIO, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_USUARIOColl[IndexRegistro].NOMENIVELUSUARIO, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 250, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_USUARIOColl[IndexRegistro].NOMESTATUS, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 350, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_USUARIOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_USUARIOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Usuários");
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

        private void bntCadFuncionairo_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                frm._IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm.ShowDialog();
            }
        }

        private void cbFuncionario_Enter(object sender, EventArgs e)
        {
            //tabControlUsuario.SelectTab(0);
            int teste = tabControlUsuario.SelectedIndex;
            GetDropFuncionario();
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_USUARIOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_USUARIOColl[indice].IDUSUARIO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = USUARIOP.Read(CodigoSelect);

                    tabControlUsuario.SelectTab(0);
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
                            USUARIOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllUsuario();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void lkbAcesso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmControleAcesso2()).Show();
            else
                (new FrmControleAcesso2()).ShowDialog();   
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            if (_IDUSUARIO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
            {
                Grava();
            }
        }

        private void btnCadNivel_Click(object sender, EventArgs e)
        {
            using (FrmNivel frm = new FrmNivel())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbnivel.SelectedValue);
                GetDropNivel();
                cbnivel.SelectedValue = CodSelec;
            }
        }

        private void GetDropNivel()
        {
            NIVELUSUARIOProvider NIVELUSUARIOP = new NIVELUSUARIOProvider();
            NIVELUSUARIOColl = NIVELUSUARIOP.ReadCollectionByParameter(null, "NOME");

            cbnivel.DisplayMember = "NOME";
            cbnivel.ValueMember = "IDNIVELUSUARIO";

            NIVELUSUARIOEntity NIVELUSUARIOTy = new NIVELUSUARIOEntity();
            NIVELUSUARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            NIVELUSUARIOTy.IDNIVELUSUARIO = -1;
            NIVELUSUARIOColl.Add(NIVELUSUARIOTy);

            Phydeaux.Utilities.DynamicComparer<NIVELUSUARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<NIVELUSUARIOEntity>(cbnivel.DisplayMember);

            NIVELUSUARIOColl.Sort(comparer.Comparer);
            cbnivel.DataSource = NIVELUSUARIOColl;

            cbnivel.SelectedIndex = 0;
        }
        

              
    }
}
