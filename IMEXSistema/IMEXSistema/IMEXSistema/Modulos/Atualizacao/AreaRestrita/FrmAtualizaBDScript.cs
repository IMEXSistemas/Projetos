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
using FirebirdSql.Data.FirebirdClient;
using System.Net;

namespace BmsSoftware.Modulos.Atualizacao
{
    public partial class FrmAtualizaBDScript : Form
    {
        Utility Util = new Utility();

        SCRIPTVERSAOSCRIPTProvider SCRIPTVERSAOP = new SCRIPTVERSAOSCRIPTProvider();
        LIS_SCRIPTVERSAOSCRIPTProvider LIS_SCRIPTVERSAOP = new LIS_SCRIPTVERSAOSCRIPTProvider();
        VERSAOSCRIPTProvider VERSAOP = new VERSAOSCRIPTProvider();


        LIS_SCRIPTVERSAOCollection LIS_SCRIPTVERSAOColl = new LIS_SCRIPTVERSAOCollection();
        VERSAOCollection VERSAOColl = new VERSAOCollection();

        public FrmAtualizaBDScript()
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

        int _IDSCRIPT = -1;
        public SCRIPTVERSAOEntity Entity
        {
            get
            {
                int IDVERSAO = Convert.ToInt32(cbVersao.SelectedValue);
                string DESCRICAO = txtDescricao.Text;
                string FLAGEXECUTADO = chkExecutado.Checked ? "S" : "N";
                return new SCRIPTVERSAOEntity(_IDSCRIPT, IDVERSAO, DESCRICAO, FLAGEXECUTADO);
            }
            set
            {
                if (value != null)
                {
                    _IDSCRIPT = value.IDSCRIPT;
                    txtDescricao.Text = value.DESCRICAO;
                    chkExecutado.Checked  = value.FLAGEXECUTADO.TrimEnd() == "S" ? true : false;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDSCRIPT = -1;
                    txtDescricao.Text = string.Empty;
                    cbVersao.SelectedIndex = 0;
                    chkExecutado.Checked  = false;
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
            Grava();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDSCRIPT = SCRIPTVERSAOP.Save(Entity);
                    GetAllAtualizaBD();
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void GetAllAtualizaBD()
        {
            LIS_SCRIPTVERSAOColl = LIS_SCRIPTVERSAOP.ReadCollectionByParameter(null, "IDVERSAO desc");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = LIS_SCRIPTVERSAOColl;

            lblTotalPesquisa.Text = LIS_SCRIPTVERSAOColl.Count.ToString();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbVersao.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbVersao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtDescricao.Text == string.Empty)
            {
                errorProvider1.SetError(txtDescricao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(cbVersao, string.Empty);


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            btnCadVersao.Image = Util.GetAddressImage(6);            
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            GetToolStripButtonCadastro();
            GetAllAtualizaBD();
            GetDropVersao();

            this.Cursor = Cursors.Default;	
        }

        private void GetDropVersao()
        {
            VERSAOColl = VERSAOP.ReadCollectionByParameter(null, "NUMEROVERSAO desc");

            cbVersao.DisplayMember = "NUMEROVERSAO";
            cbVersao.ValueMember = "IDVERSAO";
            
            cbVersao.DataSource = VERSAOColl;
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
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDSCRIPT == -1)
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
                        SCRIPTVERSAOP.Delete(_IDSCRIPT);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        GetAllAtualizaBD();
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
            if (LIS_SCRIPTVERSAOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                int ColumnSelecionada = e.ColumnIndex;
                if (rowindex != -1)
                {
                    if (ColumnSelecionada == 2)//Exceutar Script
                    {
                        SCRIPTVERSAOEntity SCRIPTVERSAOTy = new SCRIPTVERSAOEntity();
                        SCRIPTVERSAOTy = SCRIPTVERSAOP.Read(Convert.ToInt32(LIS_SCRIPTVERSAOColl[rowindex].IDSCRIPT));

                        try
                        {
                            ComandoScript(LIS_SCRIPTVERSAOColl[rowindex].DESCRICAO);
                            SCRIPTVERSAOTy.FLAGEXECUTADO = "S";
                            SCRIPTVERSAOP.Save(SCRIPTVERSAOTy);

                            MessageBox.Show("Script Executado com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            SCRIPTVERSAOTy.FLAGEXECUTADO = "N";
                            SCRIPTVERSAOP.Save(SCRIPTVERSAOTy); ;
                            MessageBox.Show("Erro ao executar o Script");
                            MessageBox.Show("Erro técnico: " + ex.Message);

                        }

                        GetAllAtualizaBD();

                    }
                    else if (ColumnSelecionada == 0)//Editar
                    {
                        int CodigoSelect = Convert.ToInt32(LIS_SCRIPTVERSAOColl[rowindex].IDSCRIPT);

                        Entity = SCRIPTVERSAOP.Read(CodigoSelect);

                        tabControlMarca.SelectTab(0);
                    }
                    else if (ColumnSelecionada == 1)//Excluir
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            int CodigoSelect = Convert.ToInt32(LIS_SCRIPTVERSAOColl[rowindex].IDSCRIPT);
                            
                            SCRIPTVERSAOP.Delete(CodigoSelect);
                            GetAllAtualizaBD();
                            Entity = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }

                    }
                }
            }
        }

        public void ComandoScript(string SQL)
        {
            try
            {
                string connString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
                FbConnection connection = new FbConnection(connString);
                connection.Open();
                FbTransaction transaction = connection.BeginTransaction();

                FbCommand command = new FbCommand(SQL, connection, transaction);
                command.CommandType = CommandType.Text;

                command.ExecuteScalar();

                transaction.Commit();
                connection.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível conectar ao banco de dados!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1);
            }
        }  

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor(); this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); Grava();this.Cursor = Cursors.Default;
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Atualização");
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
                e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 80, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_SCRIPTVERSAOColl.Count;

                while (IndexRegistro < LIS_SCRIPTVERSAOColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_SCRIPTVERSAOColl[IndexRegistro].IDSCRIPT.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_SCRIPTVERSAOColl[IndexRegistro].DESCRICAO, 100), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 80, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_SCRIPTVERSAOColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_SCRIPTVERSAOColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

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
            if (LIS_SCRIPTVERSAOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_SCRIPTVERSAOColl[indice].IDSCRIPT);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = SCRIPTVERSAOP.Read(CodigoSelect);

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
                            SCRIPTVERSAOP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            GetAllAtualizaBD();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void btnCadVersao_Click(object sender, EventArgs e)
        {
            using (FrmVersaoScript frm = new FrmVersaoScript())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbVersao.SelectedValue);
                GetDropVersao();

                cbVersao.SelectedValue = CodSelec;
            }
        }

        private void uploadBDScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Deseja enviar o BD de SCRIPT para o servidor em nuvem?",
                                   ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    //Copia o BD para outra pasta para enviar em nuvem
                    File.Copy(BmsSoftware.ConfigSistema1.Default.BDScript, BmsSoftware.ConfigSistema1.Default.PathInstall + @"\SCRIPTBDIMEX.GDB");

                    //Enviar o arquivo para nuvem
                    Upload(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\SCRIPTBDIMEX.GDB");

                    //Deleta o arquivo apos enviar para nuvem
                    File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\SCRIPTBDIMEX.GDB");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar o arquivo SCRIPTBDIMEX.GDB para a nuvem!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void Upload(string filename)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://192.185.170.200/www/arquivos/" + fileInf.Name;
            FtpWebRequest reqFTP;

            //Criar objeto FtpWebRequest do Uri, desde
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://192.185.170.200/www/arquivos/" + "/" + fileInf.Name));

            // Fornecer os Credintiais de permissao
            string UsuarioFTP = "imexsist";
            string SenhaFTP = "rmr877701";
            reqFTP.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);

            // Por padrão KeepAlive é verdadeiro, em que a ligação de controle não está fechado
            // após um comando é executado.
            reqFTP.KeepAlive = false;

            //Especifique o comando a ser executado.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Especifique o tipo de transferência de dados.
            reqFTP.UseBinary = true;

            // Notificar o servidor sobre o tamanho do arquivo enviado
            reqFTP.ContentLength = fileInf.Length;

            // O tamanho do buffer é definido para 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();

                this.Cursor = Cursors.Default;

                MessageBox.Show("Arquivo enviado com sucesso!");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Não foi possível enviar o arquivo de registro");
            }
        }

        private void uploadEXEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Deseja enviar o EXE para o servidor em nuvem?",
                                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    //Caminho de execução do EXE
                    string LocalExecucao = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

                    OpenFileDialog Open = new OpenFileDialog();
                    Open.InitialDirectory = LocalExecucao;
                    Open.FileName = "IMEXSistema.exe";

                    SaveFileDialog save = new SaveFileDialog();
                    save.InitialDirectory = LocalExecucao;
                    save.FileName = "IMEXSistema.zip";

                    //Compacta o arquivo
                    //ZipFile ZFile = new ZipFile();
                    //ZFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    //ZFile.AddSelectedFiles(Open.FileName);
                    //ZFile.Comment = "IMEX Sistemas";
                    //ZFile.Save(save.FileName);
                    //ZFile.Dispose();

                    ////Enviar o arquivo para nuvem
                    //if (File.Exists(LocalExecucao + @"\IMEXSistema.zip"))
                    //{
                    //    Upload(LocalExecucao + @"\IMEXSistema.zip");
                    //}
                    //else
                    //    MessageBox.Show("Arquivo IMEXSistema.zip não localizado na pasta: " + LocalExecucao);

                    //Deleta o arquivo apos enviar para nuvem
                    //File.Delete(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\SCRIPTBDIMEX.GDB");

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao enviar o arquivo ZIP para a nuvem!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
            
        }

        private void uploadSCHEMASNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Deseja enviar os arquivos SCHEMA NFe para o servidor em nuvem?",
                                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    //Caminho de execução do EXE
                    string PathInstall = BmsSoftware.ConfigSistema1.Default.PathInstall;

                    //Compacta o arquivo
                    //ZipFile ZFile = new ZipFile();
                    //ZFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    //ZFile.AddDirectory(PathInstall + @"\nfe\schemas\");
                    
                    //ZFile.Comment = "Arquivo SCHEMAS da NFe";
                    ////ZFile.Save(save.FileName);
                    //ZFile.Save(PathInstall + @"\nfe\schemas\SCHEMANFE.zip");
                    //ZFile.Dispose();

                    //Enviar o arquivo para nuvem
                    if (File.Exists(PathInstall + @"\nfe\schemas\SCHEMANFE.zip"))
                    {
                        Upload(PathInstall + @"\nfe\schemas\SCHEMANFE.zip");
                    }
                    else
                        MessageBox.Show("Arquivo SCHEMANFE.zip não localizado na pasta: " + PathInstall + @"\nfe\schemas");

                  //  Deleta o arquivo apos enviar para nuvem
                     if (File.Exists(PathInstall + @"\nfe\schemas\SCHEMANFE.zip"))
                        File.Delete(PathInstall + @"\nfe\schemas\SCHEMANFE.zip");

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao enviar o arquivo SCHEMANFE.zip para a nuvem!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}
