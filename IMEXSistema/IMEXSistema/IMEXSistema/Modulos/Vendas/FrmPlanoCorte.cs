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
using optimal2dx;
using BmsSoftware.Modulos.Cadastros;
using VVX;
using System.Drawing.Drawing2D;
using YLScsImage;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPlanoCorte : Form
    {
        static optimal2dx.Optimization2DX Optimization2DX1;
        static int iteration;
        int _FOLHA = 1;

        ImagePanel ImagePanel1 = new ImagePanel();

        Utility Util = new Utility();
        LIS_PLANOCORTEProvider LIS_PLANOCORTEP = new LIS_PLANOCORTEProvider();
        PLANOCORTEProvider PLANOCORTEP = new PLANOCORTEProvider();

        MEDPLANOCORTEProvider MEDPLANOCORTEP = new MEDPLANOCORTEProvider();

        LIS_PLANOCORTECollection LIS_PLANOCORTEColl = new LIS_PLANOCORTECollection();
        MEDPLANOCORTECollection MEDPLANOCORTEColl = new MEDPLANOCORTECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        int ComprimentoChapa = 0;
        int LarguraChapa = 0;

        public FrmPlanoCorte()
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


        int _IDPLANOCORTE = -1;
        public PLANOCORTEEntity Entity
        {
            get
            {
                string DESCRICAO = txtDescricaoPlanoCorte.Text;
                int COMPCHAPA = Convert.ToInt32(txtCompChapaCM.Text);
                int LARGURACHAPA = Convert.ToInt32(txtLarguraChapaCM.Text);
                int LARGLAMINA = 0;
                int NIVELOTIMIZACAO = 100;
                int IDMADEIRA = Convert.ToInt32(cbProduto.SelectedValue);

                int EXIBIRDADOS = 0;
                if (rdExibirNome.Checked)
                    EXIBIRDADOS = 1;
                else if (rdExibirCodigo.Checked)
                    EXIBIRDADOS = 2;
                else if (rdExibiLegenda.Checked)
                    EXIBIRDADOS = 3;

                string EXIBIRMEDIDAS = chkExibiMedidas.Checked ? "S" : "N";
                DateTime DATACORTE = Convert.ToDateTime(maskedtxtData.Text);

                int TAMZOOM = trackBar1.Value;

                return new PLANOCORTEEntity(_IDPLANOCORTE, COMPCHAPA, LARGURACHAPA, LARGLAMINA,
                                            NIVELOTIMIZACAO, IDMADEIRA, DESCRICAO,
                                            EXIBIRDADOS, EXIBIRMEDIDAS, DATACORTE, TAMZOOM);
            }
            set
            {

                if (value != null)
                {
                    _IDPLANOCORTE = value.IDPLANOCORTE;
                    _FOLHA = 1;

                    ListaMediPlCorte(_IDPLANOCORTE);

                    txtDescricaoPlanoCorte.Text = value.DESCRICAO;
                    txtCompChapaCM.Text = value.COMPCHAPA.ToString();
                    txtLarguraChapaCM.Text = value.LARGURACHAPA.ToString();
                    cbProduto.SelectedValue = value.IDPRODUTO;

                    if (value.EXIBIRDADOS == 0)
                        rbNaoExibir.Checked = true;
                    else if (value.EXIBIRDADOS == 1)
                        rdExibirNome.Checked = true;
                    else if (value.EXIBIRDADOS == 2)
                        rdExibirCodigo.Checked = true;
                    else if (value.EXIBIRDADOS == 3)
                        rdExibiLegenda.Checked = true;

                    maskedtxtData.Text = Convert.ToDateTime(value.DATACORTE).ToString("dd/MM/yyyy");

                    chkExibiMedidas.Checked = value.EXIBIRMEDIDAS == "S" ? true : false;

                    LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
                    bStart_Click(null, null);
                    trackBar1.Value = Convert.ToInt32(value.TAMZOOM);
                    pictureFolha1.Zoom = trackBar1.Value * 0.02f;

                    ListaMediPlCorte(_IDPLANOCORTE);
                    errorProvider2.Clear();
                }
                else
                {
                    _IDPLANOCORTE = -1;
                    ListaMediPlCorte(_IDPLANOCORTE);
                    txtDescricaoPlanoCorte.Text = string.Empty;
                    txtCompChapaCM.Text = string.Empty;
                    txtLarguraChapaCM.Text = string.Empty;

                    CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                    CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                    CONFISISTEMATy = CONFISISTEMAP.Read(1);
                    cbProduto.SelectedIndex = 0;


                    System.Drawing.Bitmap bmp = null;
                    pictureFolha1.Image = bmp;
                    pictureFolha2.Image = bmp;
                    pictureFolha3.Image = bmp;
                    pictureFolha4.Image = bmp;
                    pictureFolha5.Image = bmp;

                    rbNaoExibir.Checked = false;

                    maskedtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    trackBar1.Value = 100;
                    pictureFolha1.Zoom = 1;
                    _FOLHA = 1;

                    errorProvider2.Clear();
                }
            }
        }

        int _IDMEDPLANOCORTE = -1;
        public MEDPLANOCORTEEntity Entity2
        {
            get
            {
                int QUANTPECA = Convert.ToInt32(txtQuanPeca.Text);
                string NOMEPECA = txtNomePeca.Text;
                int COMPPECA = Convert.ToInt32(txtComprimento.Text);
                int LARGPECA = Convert.ToInt32(txtLargura.Text);
                string LEGENDA = txtLegenda.Text;

                int? CODIGOMEDIA = null;
                if (txtCodigoPeca.Text != string.Empty)
                    CODIGOMEDIA = Convert.ToInt32(txtCodigoPeca.Text);

                int FOLHA = 0;

                return new MEDPLANOCORTEEntity(_IDMEDPLANOCORTE, _IDPLANOCORTE, QUANTPECA, NOMEPECA, COMPPECA, LARGPECA, CODIGOMEDIA,
                                               LEGENDA, FOLHA);
            }
            set
            {

                if (value != null)
                {
                    _IDMEDPLANOCORTE = value.IDMEDPLANOCORTE;
                    txtQuanPeca.Text = value.QUANTPECA.ToString();
                    txtNomePeca.Text = value.NOMEPECA;
                    txtComprimento.Text = value.COMPPECA.ToString();
                    txtLargura.Text = value.LARGPECA.ToString();
                    txtLegenda.Text = value.LEGENDA;

                    if (value.CODIGOMEDIDA != null)
                        txtCodigoPeca.Text = value.CODIGOMEDIDA.ToString();
                    else
                        txtCodigoPeca.Text = string.Empty;
                    errorProvider2.Clear();
                }
                else
                {
                    _IDMEDPLANOCORTE = -1;
                    txtQuanPeca.Text = string.Empty;
                    txtNomePeca.Text = string.Empty;
                    txtComprimento.Text = string.Empty;
                    txtLargura.Text = string.Empty;
                    txtLegenda.Text = string.Empty;
                    txtCodigoPeca.Text = ProxCodPeca().ToString();
                    errorProvider2.Clear();
                }
            }
        }

        private int ProxCodPeca()
        {
            int result = 0;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
            {
                if (item.CODIGOMEDIDA != null)
                    result = Convert.ToInt32(item.CODIGOMEDIDA);
            }

            result += 1;
            return result;

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
                    _IDPLANOCORTE = PLANOCORTEP.Save(Entity);
                    LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
                    bStart_Click(null, null);
                    ListaMediPlCorte(_IDPLANOCORTE);
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


            errorProvider2.Clear();
            if (txtDescricaoPlanoCorte.Text.Trim().Length == 0)
            {
                errorProvider2.SetError(txtDescricaoPlanoCorte, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }

            else if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider2.SetError(maskedtxtData, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtCompChapaCM.Text.Trim().Length == 0 || Convert.ToInt32(txtCompChapaCM.Text) < 0)
            {
                errorProvider2.SetError(txtCompChapaCM, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtLarguraChapaCM.Text.Trim().Length == 0 || Convert.ToInt32(txtLarguraChapaCM.Text) < 0)
            {
                errorProvider2.SetError(txtLarguraChapaCM, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            {
                errorProvider2.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider2.Clear();


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnCadMtProd.Image = Util.GetAddressImage(6);
            bntDateSelec.Image = Util.GetAddressImage(11);

            GetToolStripButtonCadastro();
            GetDropProdutos();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            this.Cursor = Cursors.Default;
            VerificaAcesso();

            try
            {
                Optimization2DX1 = new Optimization2DX();
                Optimization2DX1.Identification = 123454321;

                Optimization2DX1.OnProgress += new IOptimization2DXEvents_OnProgressEventHandler(Optimization2DX1_OnProgress);
                Optimization2DX1.OnFinish += new IOptimization2DXEvents_OnFinishEventHandler(Optimization2DX1_OnFinish);

            }
            catch (Exception ex)
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Dll do Plano de corte não registrada!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                MessageBox.Show("Erro Técnico: " + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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
            tabControlPlanoCorte.SelectTab(0);
            txtDescricaoPlanoCorte.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlPlanoCorte.SelectTab(0);
            txtDescricaoPlanoCorte.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDPLANOCORTE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                //  imagepanel.Visible = false;
                tabControlPlanoCorte.SelectTab(2);
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
                        PLANOCORTEP.Delete(_IDPLANOCORTE);
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
            tabControlPlanoCorte.SelectTab(2);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlPlanoCorte.SelectTab(2);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_PLANOCORTEColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    Entity = null;
                    Entity2 = null;

                    int CodigoSelect = Convert.ToInt32(LIS_PLANOCORTEColl[rowindex].IDPLANOCORTE);

                    Entity = PLANOCORTEP.Read(CodigoSelect);

                    tabControlPlanoCorte.SelectTab(0);
                    txtDescricaoPlanoCorte.Focus();

                    this.Cursor = Cursors.Default;
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
            if (LIS_PLANOCORTEColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PLANOCORTEColl[indice].IDPLANOCORTE);

                if (e.KeyCode == Keys.Enter)
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    System.Drawing.Bitmap bmp = null;
                    pictureFolha2.Image = bmp;
                    pictureFolha3.Image = bmp;
                    pictureFolha4.Image = bmp;
                    pictureFolha5.Image = bmp;

                    Entity = PLANOCORTEP.Read(CodigoSelect);

                    tabControlPlanoCorte.SelectTab(0);
                    txtDescricaoPlanoCorte.Focus();

                    this.Cursor = Cursors.Default;

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            PLANOCORTEP.Delete(CodigoSelect);
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

        private void bStart_Click(object sender, EventArgs e)
        {
            try
            {
                _FOLHA = 1;
                if (MEDPLANOCORTEColl.Count > 0)
                {

                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);


                    pictureFolha1.Visible = true;

                    Optimization2DX1.NumberOfRepositoryPieces = 1;//Número de peças Repository
                    ComprimentoChapa = Convert.ToInt32(txtCompChapaCM.Text) / 10;
                    LarguraChapa = Convert.ToInt32(txtLarguraChapaCM.Text) / 10;
                    Optimization2DX1.SetRepositoryPiece(0, ComprimentoChapa, LarguraChapa, 0, 0, 0, 0, 0, 0, 0);//Peça Repositório conjunto
                    Optimization2DX1.BladeWidth = 0;//Largura da lâmina
                    Optimization2DX1.OptimizationLevel = 100;//Nível de otimização

                    Optimization2DX1.NumberOfDemandPieces = MEDPLANOCORTEColl.Count;

                    int i = 0;
                    foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
                    {
                       // Optimization2DX1.SetDemandPiece(i, Convert.ToInt32(item.COMPPECA) / 10, Convert.ToInt32(item.LARGPECA) / 10, 0, item.IDMEDPLANOCORTE, 0);
                        Optimization2DX1.SetDemandPiece(i, Convert.ToInt32(item.COMPPECA) / 10, Convert.ToInt32(item.LARGPECA) / 10, 1, item.IDMEDPLANOCORTE, 0);
                        //HRESULT SetDemandPiece(Index: long /*[in]*/, Length: long /*[in]*/, Width: long /*[in]*/, CanRotate: long /*[in]*/,  ExternalID: long /*[in]*/,Priority: long/*[in]*/,);
                        //Optimization2DX1.SetRepositoryPiece(Index, Length, Width, TrimTop, TrimLeft, trimbotton, trimrightm, externaid, Priority, NumberOfHoles);
                        i++;
                    }


                    Optimization2DX1.Identification = 123454321;
                    Optimization2DX1.StartGuillotine();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    // imagepanel.Visible = false;
                }

            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro ao efetuar cálculo!",
                           ConfigSistema1.Default.NomeEmpresa,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);


            }

        }

        private void ContinuaPlanoCorte()
        {
            try
            {

                if (MEDPLANOCORTEColl.Count > 0)
                {

                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);


                    pictureFolha1.Visible = true;

                    Optimization2DX1.NumberOfRepositoryPieces = 1;//Número de peças Repository
                    ComprimentoChapa = Convert.ToInt32(txtCompChapaCM.Text) / 10;
                    LarguraChapa = Convert.ToInt32(txtLarguraChapaCM.Text) / 10;
                    Optimization2DX1.SetRepositoryPiece(0, ComprimentoChapa, LarguraChapa, 0, 0, 0, 0, 0, 0, 0);//Peça Repositório conjunto
                    Optimization2DX1.BladeWidth = 0;//Largura da lâmina

                    Optimization2DX1.NumberOfDemandPieces = MEDPLANOCORTEColl.Count;

                    int i = 0;
                    foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
                    {
                      
                        Optimization2DX1.SetDemandPiece(i, Convert.ToInt32(item.COMPPECA) / 10, Convert.ToInt32(item.LARGPECA) / 10, 1, item.IDMEDPLANOCORTE, 0);
                        i++;
                    }

                    Optimization2DX1.Identification = 123454321;
                    Optimization2DX1.StartGuillotine();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    // imagepanel.Visible = false;
                }

            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro ao efetuar cálculo!",
                           ConfigSistema1.Default.NomeEmpresa,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);


            }

        }

        private void Optimization2DX1_OnProgress()
        {
            iteration++;
        }

        private void Optimization2DX1_OnFinish()
        {
            // if you want to obtain information about all sheets just iterate through all of them
            // sheets are indexed from 0 to TotalNumberOfUtilizedRepositoryPieces
            // usually the first parameter of the methods is the sheet index
            int SheetIndex = 0; // this is the first sheet;

            int top_x, top_y, bottom_x, bottom_y, thick;
            int repository_Index, repository_ID, demand_Index, demand_ID, rotated;
            System.Drawing.SizeF text_size;
            double surf_covered;

            Optimization2DX1.SurfaceCovered(SheetIndex, out surf_covered, out repository_Index, out repository_ID);

            if (_FOLHA == 1)
            {
                tbUtilizedSurface1.Text = (surf_covered).ToString("n2");
                txtPerda1.Text = (100 - surf_covered).ToString("n2");
            }
            else if (_FOLHA == 2)
            {
                tbUtilizedSurface2.Text = (surf_covered).ToString("n2");
                txtPerda2.Text = (100 - surf_covered).ToString("n2");
            }
            else if (_FOLHA == 3)
            {
                tbUtilizedSurface3.Text = (surf_covered).ToString("n2");
                txtPerda3.Text = (100 - surf_covered).ToString("n2");
            }
            else if (_FOLHA == 4)
            {
                tbUtilizedSurface4.Text = (surf_covered).ToString("n2");
                txtPerda4.Text = (100 - surf_covered).ToString("n2");
            }
            else if (_FOLHA == 5)
            {
                tbUtilizedSurface5.Text = (surf_covered).ToString("n2");
                txtPerda5.Text = (100 - surf_covered).ToString("n2");
            }

            System.Drawing.Bitmap bmp;
            bmp = new System.Drawing.Bitmap(pictureFolha1.Width, pictureFolha1.Height);
            // bmp = new System.Drawing.Bitmap(LarguraChapa, ComprimentoChapa);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);


            g.DrawRectangle(Pens.Black, 0, 0, ComprimentoChapa, LarguraChapa);


            int NumCuts;
            Optimization2DX1.NumberOfCuttings(SheetIndex, out NumCuts, out repository_Index, out repository_ID);
            for (int i = 0; i < NumCuts; i++)
            {
                Optimization2DX1.GetCut(SheetIndex, i, out  top_x, out  top_y, out bottom_x, out bottom_y, out thick, out repository_Index, out repository_ID);
                g.DrawLine(Pens.Black, top_x, top_y, bottom_x, bottom_y);
            }


            int NumPieces;
            Optimization2DX1.NumberOfUtilizedDemandPieces(SheetIndex, out NumPieces, out  repository_Index, out  repository_ID);
            for (int i = 0; i < NumPieces; i++)
            {
                 Optimization2DX1.GetUtilizedDemandPiece(SheetIndex, i, out top_x, out top_y, out bottom_x, out bottom_y, out rotated, out repository_Index, out demand_Index, out repository_ID, out demand_ID);
                text_size = g.MeasureString(Convert.ToString(System.Math.Abs(bottom_x - top_x)) + "x" + Convert.ToString(System.Math.Abs(bottom_y - top_y)), Font);

                //Salva terminada folha
                SalvaPecaCorte(demand_ID, _FOLHA);

                string TextoMedida = Convert.ToString(System.Math.Abs(bottom_x - top_x) * 10) + "x" + Convert.ToString(System.Math.Abs(bottom_y - top_y) * 10);
                if (!chkExibiMedidas.Checked)
                    TextoMedida = string.Empty;

                if (rbNaoExibir.Checked)
                    g.DrawString(TextoMedida, Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                else if (rdExibirNome.Checked)
                {
                    if (TextoMedida == string.Empty)
                        g.DrawString(RetornaDados(demand_ID, 1), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                    else
                        g.DrawString(TextoMedida + "\n" + RetornaDados(demand_ID, 1), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                }
                else if (rdExibirCodigo.Checked)
                {
                    if (TextoMedida == string.Empty)
                        g.DrawString("    " + RetornaDados(demand_ID, 2), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                    else
                        g.DrawString(TextoMedida + "\n" + RetornaDados(demand_ID, 2), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);

                }
                else if (rdExibiLegenda.Checked)
                {
                    if (TextoMedida == string.Empty)
                        g.DrawString("    " + RetornaDados(demand_ID, 0), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                    else
                        g.DrawString(TextoMedida + "\n" + RetornaDados(demand_ID, 0), Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
                }
            }

            int NumWastes;
            Optimization2DX1.NumberOfWastePieces(SheetIndex, out NumWastes, out  repository_Index, out  repository_ID);
            for (int i = 0; i < NumWastes; i++)
            {
                Optimization2DX1.GetWastePiece(SheetIndex, i, out top_x, out  top_y, out bottom_x, out bottom_y, out repository_Index, out repository_ID);
                text_size = g.MeasureString("r", Font);
                g.DrawString("r", Font, Brushes.Black, (bottom_x + top_x) / 2 - text_size.Width / 2, (bottom_y + top_y) / 2 - text_size.Height / 2);
            }


            if (_FOLHA == 1)
                pictureFolha1.Image = bmp;
            else if (_FOLHA == 2)
            {
                pictureFolha2.Image = bmp;
            }
            else if (_FOLHA == 3)
            {
                pictureFolha3.Image = bmp;
            }
            else if (_FOLHA == 4)
            {
                pictureFolha4.Image = bmp;
            }
            else if (_FOLHA == 5)
            {
                pictureFolha5.Image = bmp;
            }

            _FOLHA++;
            FiltraFolha(0);
        }

        private void FiltraFolha(int FOLHA)
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", FOLHA.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));

            MEDPLANOCORTEColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio, "CODIGOMEDIDA");


            if (_FOLHA <= 5)
                ContinuaPlanoCorte();
        }

        //0 - Legenda - 1 - NomePeça - 2 - CodigoMedida
        private string RetornaDados(int MEDPLANOCORTE, int indice)
        {
            string result = string.Empty;

            MEDPLANOCORTEEntity MEDPLANOCORTETy = new MEDPLANOCORTEEntity();
            MEDPLANOCORTETy = MEDPLANOCORTEP.Read(MEDPLANOCORTE);

            if (MEDPLANOCORTETy != null)
            {
                if (indice == 0)
                    result = MEDPLANOCORTETy.LEGENDA;
                else if (indice == 1)
                    result = MEDPLANOCORTETy.NOMEPECA;
                else if (indice == 2)
                    result = MEDPLANOCORTETy.CODIGOMEDIDA.ToString();
            }

            return result;
        }

        private void SalvaPecaCorte(int IDMEDPLANOCORTE, int FOLHA)
        {
            try
            {
                MEDPLANOCORTEEntity MEDPLANOCORTETy = new MEDPLANOCORTEEntity();
                MEDPLANOCORTETy = MEDPLANOCORTEP.Read(IDMEDPLANOCORTE);
                MEDPLANOCORTETy.FOLHA = FOLHA;
                MEDPLANOCORTEP.Save(MEDPLANOCORTETy);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao alterar as folhas da medida do plano de corte!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private void txtComprimento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Comprimento/altura em Milímetro";
        }

        private void txtLargura_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Largura em Milímetro";
        }

        private void txtCompChapaCM_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtCompChapaCM.Text))
            {
                errorProvider2.SetError(txtCompChapaCM, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void txtLarguraChapaCM_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtLarguraChapaCM.Text))
            {
                errorProvider2.SetError(txtLarguraChapaCM, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void txtComprimento_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtComprimento.Text))
            {
                errorProvider2.SetError(txtComprimento, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void txtLargura_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtLargura.Text))
            {
                errorProvider2.SetError(txtLargura, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void btnCadMtProd_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
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

        private void txtQuanPeca_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoInt32(txtQuanPeca.Text))
            {
                errorProvider2.SetError(txtQuanPeca, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PLANOCORTEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPlanoCorte.SelectTab(2);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (LIS_PLANOCORTEColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPlanoCorte.SelectTab(2);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_PLANOCORTEColl = LIS_PLANOCORTEP.ReadCollectionByParameter(Filtro, "IDPLANOCORTE DESC");


                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PLANOCORTEColl;

                lblTotalPesquisa.Text = LIS_PLANOCORTEColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlPlanoCorte.SelectedIndex == 2)
                {
                    errorProvider2.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
            }
            else
            {
                errorProvider2.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_PLANOCORTEColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PLANOCORTEColl;
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

                LIS_PLANOCORTEColl = LIS_PLANOCORTEP.ReadCollectionByParameter(Filtro, "IDPLANOCORTE DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PLANOCORTEColl;

                lblTotalPesquisa.Text = LIS_PLANOCORTEColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider2.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnAdMedi_Click(object sender, EventArgs e)
        {
            if (_IDPLANOCORTE == -1)
            {
                MessageBox.Show("Antes de adicionar as medidas é necessário gravar o plano de corte!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                GravaMedida();
                LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
                bStart_Click(null, null);
                ListaMediPlCorte(_IDPLANOCORTE);
            }
        }

        //Limpando para colocar Zero no campo FOLHA
        private void LimpaMedPlanCorte()
        {
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
            {
                MEDPLANOCORTEEntity MEDPLANOCORTETy = new MEDPLANOCORTEEntity();
                MEDPLANOCORTETy = MEDPLANOCORTEP.Read(item.IDMEDPLANOCORTE);
                MEDPLANOCORTETy.FOLHA = 0;
                MEDPLANOCORTEP.Save(MEDPLANOCORTETy);
            }
        }

        private void GravaMedida()
        {
            try
            {
                if (ValidacoesMedida())
                {
                    for (int i = 0; i < Convert.ToInt32(txtQuanPeca.Text); i++)
                    {
                        MEDPLANOCORTEP.Save(Entity2);
                    }

                    ListaMediPlCorte(_IDPLANOCORTE);
                    Entity2 = null;
                    tabControlPlanoCorte.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private void ListaMediPlCorte(int IDPLANOCORTE)
        {
            RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
            RowpProdPedido.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", IDPLANOCORTE.ToString()));
            MEDPLANOCORTEColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowpProdPedido, "CODIGOMEDIDA");

            dgMedPlanoCorte.AutoGenerateColumns = false;
            dgMedPlanoCorte.DataSource = MEDPLANOCORTEColl;
            lblTotalPecas.Text = "Total de peças: " + MEDPLANOCORTEColl.Count.ToString();
        }

        private Boolean ValidacoesMedida()
        {
            Boolean result = true;


            errorProvider2.Clear();

            if (txtCodigoPeca.Text.Trim().Length == 0)
            {
                errorProvider2.SetError(txtCodigoPeca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtNomePeca.Text.Trim().Length == 0)
            {
                errorProvider2.SetError(txtNomePeca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtQuanPeca.Text.Trim().Length == 0 || Convert.ToInt32(txtQuanPeca.Text) < 0)
            {
                errorProvider2.SetError(txtQuanPeca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtComprimento.Text.Trim().Length == 0 || Convert.ToInt32(txtComprimento.Text) < 0)
            {
                errorProvider2.SetError(txtComprimento, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtLargura.Text.Trim().Length == 0 || Convert.ToInt32(txtLargura.Text) < 0)
            {
                errorProvider2.SetError(txtLargura, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider2.Clear();


            return result;
        }

        private void dgMedPlanoCorte_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (MEDPLANOCORTEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {

                    CodSelect = Convert.ToInt32(MEDPLANOCORTEColl[rowindex].IDMEDPLANOCORTE);
                    Entity2 = MEDPLANOCORTEP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(MEDPLANOCORTEColl[rowindex].IDMEDPLANOCORTE);
                            MEDPLANOCORTEP.Delete(CodSelect);
                            ListaMediPlCorte(_IDPLANOCORTE);
                            Entity2 = null;
                            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
                            bStart_Click(null, null);
                            ListaMediPlCorte(_IDPLANOCORTE);

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

        private void rbNaoExibir_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
            bStart_Click(null, null);
            ListaMediPlCorte(_IDPLANOCORTE);

            this.Cursor = Cursors.Default;
        }

        private void rdExibirNome_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
            bStart_Click(null, null);
            ListaMediPlCorte(_IDPLANOCORTE);

            this.Cursor = Cursors.Default;
        }

        private void rdExibirCodigo_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
            bStart_Click(null, null);
            ListaMediPlCorte(_IDPLANOCORTE);

            this.Cursor = Cursors.Default;
        }

        private void rdExibiLegenda_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
            bStart_Click(null, null);
            ListaMediPlCorte(_IDPLANOCORTE);

            this.Cursor = Cursors.Default;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LIS_PLANOCORTEColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PLANOCORTEColl.Count.ToString();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtCompChapaCM_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Comprimento/Altura da chapa em Milímetro";
        }

        private void txtLarguraChapaCM_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Largura da chapa em Milímetro";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MEDPLANOCORTEColl.Count == 0)
            {
                MessageBox.Show("Não existe medida(s) cadastrada(s)!");
                errorProvider2.SetError(dgMedPlanoCorte, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);


                if (dr == DialogResult.Yes)
                {

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
                            {
                                MEDPLANOCORTEP.Delete(Convert.ToInt32(item.IDMEDPLANOCORTE));
                            }

                            bStart_Click(null, null);
                            ListaMediPlCorte(_IDPLANOCORTE);
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

        private void txtNivelOtimizacao_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Controla a velocidade e a qualidade da otimização, um valor maior levará a melhores resultados, mas eles são obtidas em um tempo mais longo.";
        }

        private void txtLargLamina_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Especificar a largura da lâmina utilizada para cortar o material.";
        }

        private void chkExibiMedidas_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            LimpaMedPlanCorte();//Limpando para colocar Zero no campo FOLHA
            bStart_Click(null, null);
            ListaMediPlCorte(_IDPLANOCORTE);

            this.Cursor = Cursors.Default;
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelec_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(190, 160);
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
            maskedtxtData.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        private void maskedtxtData_Validating(object sender, CancelEventArgs e)
        {
            errorProvider2.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider2.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void planoDeCoreteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPLANOCORTE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPlanoCorte.SelectTab(2);
            }
            else
            {
                ImprimirPlanoCorte();
            }

        }

        private void ImprimirPlanoCorte()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = prPlanoCorGeral;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                //Lista Geral
                objPrintPreview.Document = prPlanoCorGeral;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();

                //Folha 1
                printDocument2.PrinterSettings = prPlanoCorGeral.PrinterSettings;
                objPrintPreview.Document = printDocument2;
                objPrintPreview.ShowDialog();

                if (pictureFolha2.Image != null) //Folha 2
                {
                    prPlanoCort2.PrinterSettings = prPlanoCorGeral.PrinterSettings;
                    printDialog1.Document = prPlanoCort2;
                    objPrintPreview.Document = prPlanoCort2;
                    objPrintPreview.ShowDialog();
                }
                if (pictureFolha3.Image != null) //Folha 3
                {
                    prPlanoCort3.PrinterSettings = prPlanoCorGeral.PrinterSettings;
                    printDialog1.Document = prPlanoCort3;
                    objPrintPreview.Document = prPlanoCort3;
                    objPrintPreview.ShowDialog();
                }
                if (pictureFolha4.Image != null) //Folha 4
                {
                    prPlanoCort4.PrinterSettings = prPlanoCorGeral.PrinterSettings;
                    printDialog1.Document = prPlanoCort4;
                    objPrintPreview.Document = prPlanoCort4;
                    objPrintPreview.ShowDialog();
                }
                if (pictureFolha5.Image != null) //Folha 5
                {
                    prPlanoCort5.PrinterSettings = prPlanoCorGeral.PrinterSettings;
                    printDialog1.Document = prPlanoCort5;
                    objPrintPreview.Document = prPlanoCort5;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString() + " - Superfície Utilizada (%): " + tbUtilizedSurface1.Text + " - Perda (%): " + txtPerda1.Text + " - Folha 1", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 190);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 210);

            //Produtos
            int linha = 250;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "1"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                //Retorna a quantidade da coleção da legenda
                e.Graphics.DrawString(ReturnQuantPlanCorte(item.CODIGOMEDIDA, MEDPLANOCORTE2Coll, 1).ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

            linha = linha + 20;
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, SystemBrushes.ControlText, linha + 30, -40);
            e.Graphics.ResetTransform();

            pictureFolha1.Zoom = trackBar1.Value * 0.02f;
            int WidtImagPlan = Convert.ToInt32(pictureFolha1.Width * trackBar1.Value * 0.02f);
            int HeightImagPlan = Convert.ToInt32(pictureFolha1.Height * trackBar1.Value * 0.02f);
            e.Graphics.DrawImage(pictureFolha1.Image, config.MargemEsquerda + 20, linha + 20, WidtImagPlan, HeightImagPlan);

        }

        private int ReturnQuantPlanCorte(int? CODIGOMEDIDA, MEDPLANOCORTECollection ColMedPlanoCorte, int FOLHA)
        {
            int result = 0;

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("CODIGOMEDIDA", "System.Int32", "=", CODIGOMEDIDA.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", FOLHA.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            ColMedPlanoCorte = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            foreach (MEDPLANOCORTEEntity item in ColMedPlanoCorte)
            {
                result++;
            }

            return result;
        }

        private void txtCodigoPeca_Enter(object sender, EventArgs e)
        {
            txtCodigoPeca.Text = ProxCodPeca().ToString();
        }

        private void duplicataPlanoDeCorteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDPLANOCORTE == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                pictureFolha1.Visible = false;
                tabControlPlanoCorte.SelectTab(2);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente duplicar este plano de corte?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        _IDPLANOCORTE = -1;
                        txtDescricaoPlanoCorte.Text = txtDescricaoPlanoCorte.Text + "( Duplicado )";
                        _IDPLANOCORTE = PLANOCORTEP.Save(Entity);

                        //grava medida
                        foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEColl)
                        {
                            item.IDMEDPLANOCORTE = -1;
                            item.IDPLANOCORTE = _IDPLANOCORTE;

                            MEDPLANOCORTEP.Save(item);

                        }

                        MessageBox.Show("Plano de corte duplicado com sucesso!");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Não foi possível duplicar o Plano de corte!");

                    }

                }
            }
        }

        private void tZoom_Scroll(object sender, EventArgs e)
        {
            pictureFolha1.Zoom = trackBar1.Value * 0.02f;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pictureFolha2.Zoom = trackBar2.Value * 0.02f;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pictureFolha3.Zoom = trackBar3.Value * 0.02f;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            pictureFolha4.Zoom = trackBar4.Value * 0.02f;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            pictureFolha5.Zoom = trackBar5.Value * 0.02f;
        }

        private void prPlanoCort2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString() + " - Superfície Utilizada (%): " + tbUtilizedSurface2.Text + " - Perda (%): " + txtPerda2.Text + " - Folha 2", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 190);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 210);

            //Produtos
            int linha = 250;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "2"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                //Retorna a quantidade da coleção da legenda
                e.Graphics.DrawString(ReturnQuantPlanCorte(item.CODIGOMEDIDA, MEDPLANOCORTE2Coll, 2).ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

            linha = linha + 20;
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, SystemBrushes.ControlText, linha + 30, -40);
            e.Graphics.ResetTransform();

            pictureFolha2.Zoom = trackBar2.Value * 0.02f;
            int WidtImagPlan = Convert.ToInt32(pictureFolha2.Width * trackBar2.Value * 0.02f);
            int HeightImagPlan = Convert.ToInt32(pictureFolha2.Height * trackBar2.Value * 0.02f);
            e.Graphics.DrawImage(pictureFolha2.Image, config.MargemEsquerda + 20, linha + 20, WidtImagPlan, HeightImagPlan);


        }

        private void prPlanoCort3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString() + " - Superfície Utilizada (%): " + tbUtilizedSurface3.Text + " - Perda (%): " + txtPerda3.Text + " - Folha 3", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 190);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 210);

            //Produtos
            int linha = 250;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "3"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                //Retorna a quantidade da coleção da legenda
                e.Graphics.DrawString(ReturnQuantPlanCorte(item.CODIGOMEDIDA, MEDPLANOCORTE2Coll, 3).ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

            linha = linha + 20;
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, SystemBrushes.ControlText, linha + 30, -40);
            e.Graphics.ResetTransform();

            pictureFolha3.Zoom = trackBar3.Value * 0.02f;
            int WidtImagPlan = Convert.ToInt32(pictureFolha3.Width * trackBar3.Value * 0.02f);
            int HeightImagPlan = Convert.ToInt32(pictureFolha3.Height * trackBar3.Value * 0.02f);
            e.Graphics.DrawImage(pictureFolha3.Image, config.MargemEsquerda + 20, linha + 20, WidtImagPlan, HeightImagPlan);



        }

        private void prPlanoCort4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString() + " - Superfície Utilizada (%): " + tbUtilizedSurface4.Text + " - Perda (%): " + txtPerda4.Text + " - Folha 4", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 190);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 210);

            //Produtos
            int linha = 250;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "4"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                //Retorna a quantidade da coleção da legenda
                e.Graphics.DrawString(ReturnQuantPlanCorte(item.CODIGOMEDIDA, MEDPLANOCORTE2Coll, 4).ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

            linha = linha + 20;
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, SystemBrushes.ControlText, linha + 30, -40);
            e.Graphics.ResetTransform();

            pictureFolha4.Zoom = trackBar4.Value * 0.02f;
            int WidtImagPlan = Convert.ToInt32(pictureFolha4.Width * trackBar4.Value * 0.02f);
            int HeightImagPlan = Convert.ToInt32(pictureFolha4.Height * trackBar4.Value * 0.02f);
            e.Graphics.DrawImage(pictureFolha4.Image, config.MargemEsquerda + 20, linha + 20, WidtImagPlan, HeightImagPlan);




        }

        private void prPlanoCort5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 170);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString() + " - Superfície Utilizada (%): " + tbUtilizedSurface5.Text + " - Perda (%): " + txtPerda5.Text + " - Folha 5", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 190);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 210);

            //Produtos
            int linha = 250;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "5"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);

                //Retorna a quantidade da coleção da legenda
                e.Graphics.DrawString(ReturnQuantPlanCorte(item.CODIGOMEDIDA, MEDPLANOCORTE2Coll, 5).ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);

                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

            linha = linha + 20;
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, linha);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString(LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, SystemBrushes.ControlText, linha + 30, -40);
            e.Graphics.ResetTransform();

            pictureFolha5.Zoom = trackBar5.Value * 0.02f;
            int WidtImagPlan = Convert.ToInt32(pictureFolha5.Width * trackBar5.Value * 0.02f);
            int HeightImagPlan = Convert.ToInt32(pictureFolha5.Height * trackBar5.Value * 0.02f);
            e.Graphics.DrawImage(pictureFolha5.Image, config.MargemEsquerda + 20, linha + 20, WidtImagPlan, HeightImagPlan);




        }

        private void prPlanoCorGeral_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Armazena na coleção do Orçamento Selecionada
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            LIS_PLANOCORTECollection LIS_PLANOCORTECollPrint = new LIS_PLANOCORTECollection();
            LIS_PLANOCORTECollPrint = LIS_PLANOCORTEP.ReadCollectionByParameter(RowRelatorio);


            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("Nº Controle", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
            e.Graphics.DrawString(Entity.IDPLANOCORTE.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 53);

            e.Graphics.DrawString("LISTA GERAL DO CORTE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 150);
            e.Graphics.DrawString("Descrição: " + LIS_PLANOCORTECollPrint[0].DESCRICAO + " Data: " + Convert.ToDateTime(LIS_PLANOCORTECollPrint[0].DATACORTE).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 180);
            e.Graphics.DrawString("Comp./Altura Chapa (mm) : " + LIS_PLANOCORTECollPrint[0].COMPCHAPA.ToString() + " - Largura Chapa (mm): " + LIS_PLANOCORTECollPrint[0].LARGURACHAPA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString("Largura Lâmina : " + LIS_PLANOCORTECollPrint[0].LARGLAMINA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 220);
            e.Graphics.DrawString("Material : " + LIS_PLANOCORTECollPrint[0].NOMEPRODUTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 240);

            //Produtos
            int linha = 280;
            e.Graphics.DrawString("Legenda", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, linha);
            e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, linha);
            e.Graphics.DrawString("Quant.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 150, linha);
            e.Graphics.DrawString("Nome Peça", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, linha);
            e.Graphics.DrawString("Largura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, linha);
            e.Graphics.DrawString("Altura (MM)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, linha);


            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("FOLHA", "System.Int32", "=", "1"));
            RowRelatorio.Add(new RowsFiltro("IDPLANOCORTE", "System.Int32", "=", _IDPLANOCORTE.ToString()));
            MEDPLANOCORTECollection MEDPLANOCORTEPrintColl = new MEDPLANOCORTECollection();
            MEDPLANOCORTEPrintColl = MEDPLANOCORTEP.ReadCollectionByParameter(RowRelatorio);

            //Elimina codigo repetido
            MEDPLANOCORTECollection MEDPLANOCORTE2Coll = new MEDPLANOCORTECollection();
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTEPrintColl)
            {
                if (MEDPLANOCORTE2Coll.Find(delegate(MEDPLANOCORTEEntity item2) { return (item2.CODIGOMEDIDA == item.CODIGOMEDIDA); }) == null)
                {
                    MEDPLANOCORTE2Coll.Add(item);
                }
            }

            linha = linha + 20;
            foreach (MEDPLANOCORTEEntity item in MEDPLANOCORTE2Coll)
            {
                e.Graphics.DrawString(item.LEGENDA, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, linha);
                e.Graphics.DrawString(item.CODIGOMEDIDA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, linha);
                e.Graphics.DrawString(item.QUANTPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 150, linha);
                e.Graphics.DrawString(item.NOMEPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, linha);
                e.Graphics.DrawString(item.LARGPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, linha);
                e.Graphics.DrawString(item.COMPPECA.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 650, linha);
                linha = linha + 20;
            }

        }

        private void txtNivel_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Controla a velocidade e a qualidade da optimização, o valor crescente levará a melhores resultados, mas eles são obtidas em um tempo maior.";
        }
    }
}
