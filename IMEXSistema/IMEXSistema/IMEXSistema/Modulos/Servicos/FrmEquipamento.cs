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
using VVX;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmEquipamento : Form
    {
        Utility Util = new Utility();

        EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
        LIS_EQUIPAMENTOProvider LIS_EQUIPAMENTOP = new LIS_EQUIPAMENTOProvider();
        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
        LIS_EQUIPAMENTOOSFECHProvider LIS_EQUIPAMENTOOSFECHP = new LIS_EQUIPAMENTOOSFECHProvider();
        DESTINOEQUIPAMENTOProvider DESTINOEQUIPAMENTOP = new DESTINOEQUIPAMENTOProvider();

        LIS_EQUIPAMENTOCollection LIS_EQUIPAMENTOColl = new LIS_EQUIPAMENTOCollection();
        LIS_EQUIPAMENTOOSFECHCollection LIS_EQUIPAMENTOOSFECHColl = new LIS_EQUIPAMENTOOSFECHCollection();
        FUNCIONARIOCollection FUNCIONARIO2Coll = new FUNCIONARIOCollection();
        EQUIPAMENTOCollection EQUIPAMENTO2Coll = new EQUIPAMENTOCollection();
        DESTINOEQUIPAMENTOCollection DESTINOEQUIPAMENTOColl = new DESTINOEQUIPAMENTOCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

       public int _IDEQUIPAMENTO = -1;

        public FrmEquipamento()
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

        byte[] _FOTO1 = null;
        public EQUIPAMENTOEntity Entity
        {
            get
            {
                string NOME = txtNome.Text.TrimEnd().TrimStart();
                string OBSERVACAO = txtObservacao.Text;
                string CACAMBA = txtCacamba.Text;
                string PESOOPERACIONAL = txtpesooperacional.Text;
                string CONSUMOHORA = txtconsumohora.Text;
                string PROFESCAVACAO = txtProfEscavacao.Text;
                string POTENCIAHP = txtPotenciahp.Text;
               
                if (txtValorHora.Text == string.Empty)
                    txtValorHora.Text = "0,00";

                decimal VALOR =Convert.ToDecimal(txtValorHora.Text);

                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue);
                string LOCALIZACAO = txtLocalizacao.Text;

                if (txtValorDia.Text == string.Empty)
                    txtValorDia.Text = "0,00";
                decimal VALORDIA = Convert.ToDecimal(txtValorDia.Text);

                if (txtValorMes.Text == string.Empty)
                    txtValorMes.Text = "0,00";
                decimal VALORMES = Convert.ToDecimal(txtValorMes.Text);
                string IDENTIFICACAO = txtIdentificacao.Text;

                return new EQUIPAMENTOEntity(_IDEQUIPAMENTO, NOME, OBSERVACAO, CACAMBA, PESOOPERACIONAL,
                                             CONSUMOHORA, PROFESCAVACAO, POTENCIAHP, VALOR,
                                             IDSTATUS, LOCALIZACAO, _FOTO1, VALORDIA, VALORMES, IDENTIFICACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDEQUIPAMENTO = value.IDEQUIPAMENTO;
                    ListaDestinoEquipamento(_IDEQUIPAMENTO);
                    txtNome.Text = value.NOME;

                    txtCacamba.Text = value.CACAMBA;
                    txtpesooperacional.Text = value.PESOOPERACIONAL;
                    txtconsumohora.Text = value.CONSUMOHORA;
                    txtProfEscavacao.Text = value.PROFESCAVACAO;
                    txtPotenciahp.Text = value.POTENCIAHP;
                    txtValorHora.Text = Convert.ToDecimal(value.VALOR).ToString("n2");
                    txtValorDia.Text = Convert.ToDecimal(value.VALORDIA).ToString("n2");
                    txtValorMes.Text = Convert.ToDecimal(value.VALORMES).ToString("n2");

                    cbStatus.SelectedValue = value.IDSTATUS;
                    txtLocalizacao.Text = value.LOCALIZACAO;
                    txtIdentificacao.Text = value.IDENTIFICACAO;

                    if (value.FOTO != null)
                    {
                        _FOTO1 = value.FOTO;                      
                    }
                    else
                    {
                        _FOTO1 = null;
                        
                    }

                    ListaHistEquip(value.IDEQUIPAMENTO);

                    txtObservacao.Text = value.OBSERVACAO;

                    cbEquipamento.SelectedValue = value.IDEQUIPAMENTO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDEQUIPAMENTO = -1;
                    ListaDestinoEquipamento(_IDEQUIPAMENTO);
                    txtNome.Text = string.Empty;
                    txtObservacao.Text = string.Empty;
                    txtCacamba.Text = string.Empty;
                    txtpesooperacional.Text = string.Empty;
                    txtconsumohora.Text = string.Empty; ;
                    txtProfEscavacao.Text = string.Empty;
                    txtPotenciahp.Text = string.Empty;
                    cbStatus.SelectedIndex = 0;
                    txtLocalizacao.Text = string.Empty;
                    txtValorHora.Text = "0,00";
                    txtValorDia.Text = "0,00";
                    txtValorMes.Text = "0,00";
                    ListaHistEquip(-1);
                    txtIdentificacao.Text = string.Empty;
                    cbEquipamento.SelectedValue = -1;

                    _FOTO1 = null;                  
                    errorProvider1.Clear();
                }
            }
        }

        int _IDDESTINOEQUIPAMENTO = -1;
        public DESTINOEQUIPAMENTOEntity Entity2
        {
            get
            {
                 
               string DESTINO  = txtDestino.Text; //             VARCHAR(50),
               string  OBSERVACAO  = txtObservacaoLocalizacao.Text;
               DateTime DATA  = DateTime.Now;
               string HORA = DateTime.Now.ToString("HH:mm");  //                CHAR(5))

               return new DESTINOEQUIPAMENTOEntity(_IDDESTINOEQUIPAMENTO, _IDEQUIPAMENTO, DESTINO, OBSERVACAO, DATA, HORA);
            }
            set
            {

                if (value != null)
                {
                    _IDDESTINOEQUIPAMENTO = value.IDDESTINOEQUIPAMENTO;
                   
                    txtDestino.Text = value.DESTINO;
                    txtObservacaoLocalizacao.Text = value.OBSERVACAO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDDESTINOEQUIPAMENTO = -1;
                    txtDestino.Text = string.Empty;
                    txtObservacaoLocalizacao.Text = string.Empty;

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
                    _IDEQUIPAMENTO = EQUIPAMENTOP.Save(Entity);
                    tabControlMarca.SelectTab(0);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
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
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                GetToolStripButtonCadastro();
                GetDropStatus();
                GetFuncionario2();
                GetDropEquipamento();

                btnCadStatus.Image = Util.GetAddressImage(6);              
                bntDateSelecInicial.Image = Util.GetAddressImage(11);
                bntDateSelecFinal.Image = Util.GetAddressImage(11);
                btnCadEsquip.Image = Util.GetAddressImage(6);

                PreencheDropCamposPesquisa();
                PreencheDropTipoPesquisa();

                if (_IDEQUIPAMENTO != -1)
                    Entity = EQUIPAMENTOP.Read(_IDEQUIPAMENTO);

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {

                this.Cursor = Cursors.Default;
            }            
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

        private void GetDropEquipamento()
        {
            EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
            EQUIPAMENTO2Coll = EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbEquipamento.DisplayMember = "NOME";
            cbEquipamento.ValueMember = "IDEQUIPAMENTO";

            EQUIPAMENTOEntity EQUIPAMENTOTy = new EQUIPAMENTOEntity();
            EQUIPAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            EQUIPAMENTOTy.IDEQUIPAMENTO = -1;
            EQUIPAMENTO2Coll.Add(EQUIPAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity>(cbEquipamento.DisplayMember);

            EQUIPAMENTO2Coll.Sort(comparer.Comparer);
            cbEquipamento.DataSource = EQUIPAMENTO2Coll;

            cbEquipamento.SelectedIndex = 0;
        }

        private void GetFuncionario2()
        {
            FUNCIONARIO2Coll = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario2.DisplayMember = "NOME";
            cbFuncionario2.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIO2Coll.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario2.DisplayMember);

            FUNCIONARIO2Coll.Sort(comparer.Comparer);
            cbFuncionario2.DataSource = FUNCIONARIO2Coll;

            cbFuncionario2.SelectedIndex = 0;
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
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlMarca.SelectTab(0);
            txtNome.Focus();
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDEQUIPAMENTO == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(3);
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        EQUIPAMENTOP.Delete(_IDEQUIPAMENTO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }

                }
            }
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(3);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(3);
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_EQUIPAMENTOColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_EQUIPAMENTOColl[rowindex].IDEQUIPAMENTO);

                    Entity = EQUIPAMENTOP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
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
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Equipamentos");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dgLocalizacao, RelatorioTitulo, this.Name);
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
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
            if (LIS_EQUIPAMENTOColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_EQUIPAMENTOColl[indice].IDEQUIPAMENTO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = EQUIPAMENTOP.Read(CodigoSelect);

                    tabControlMarca.SelectTab(0);
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
                            EQUIPAMENTOP.Delete(CodigoSelect);
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

        private void txtValorHora_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorHora.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorHora.Text))
                {
                    errorProvider1.SetError(txtValorHora, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorHora.Text);
                    txtValorHora.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorHora, "");
                }
            }
            else
                txtValorHora.Text = "0,00";
        }

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUS = CodSelec;
                frm.ShowDialog();
                
                GetDropStatus();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void GetDropStatus()
        {
            //14 Equipamento
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "14");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";
        }

        private void btnMaquinaFt_Click(object sender, EventArgs e)
        {
            if (_IDEQUIPAMENTO == -1)
            {
                MessageBox.Show("Antes de adicionar a fotos é necessário gravar o Equipamento!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                OFileFoto1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"; // Filtra os tipos de arquivos desejados
                OFileFoto1.ShowDialog();
            }
        }


        private void OFileFoto1_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        public static byte[] GetFoto(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }

        private void GetFotoEquipamento(byte[] FOTOANIMALEntity, PictureBox NamePicture)
        {
            MemoryStream stream = new MemoryStream(FOTOANIMALEntity);
            NamePicture.Image = Image.FromStream(stream);

            NamePicture.SizeMode = PictureBoxSizeMode.AutoSize;
            NamePicture.SizeMode = PictureBoxSizeMode.CenterImage;
            NamePicture.SizeMode = PictureBoxSizeMode.Normal;
            NamePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            NamePicture.SizeMode = PictureBoxSizeMode.Zoom;
            NamePicture.Name = NamePicture.Name;
        }

        private void lkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (_IDEQUIPAMENTO == -1)
                {
                    MessageBox.Show("Antes de visualizar a fotos é necessário gravar o Equipamento!",
                                    ConfigSistema1.Default.NomeEmpresa,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                }
                else if (_FOTO1 != null)
                {
                    ExibirImagemGrande(_FOTO1);
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);

            }
        }

        private void ExibirImagemGrande(byte[] FOTOANIMALEntity)
        {
            Form FormFoto = new Form();
            FormFoto.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormFoto.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormFoto.ClientSize = new System.Drawing.Size(573, 376);
            FormFoto.Text = "Visualizar Imagem";

            PictureBox PcBox = new PictureBox();
            PcBox.Location = new System.Drawing.Point(3, 3);
            PcBox.Name = "pictureBox1";
            PcBox.Size = new System.Drawing.Size(558, 343);

            MemoryStream stream = new MemoryStream(FOTOANIMALEntity);
            PcBox.Image = Image.FromStream(stream);

            PcBox.Dock = System.Windows.Forms.DockStyle.Fill;
            PcBox.Location = new System.Drawing.Point(0, 0);

            PcBox.SizeMode = PictureBoxSizeMode.StretchImage;

            FormFoto.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormFoto.Controls.Add(PcBox);
            FormFoto.ShowDialog();
        }

        private void linkExcluirFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void txtValorDia_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorDia.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorDia.Text))
                {
                    errorProvider1.SetError(txtValorDia, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorDia.Text);
                    txtValorDia.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorDia, "");
                }
            }
            else
                txtValorDia.Text = "0,00";
        }

        private void txtValorMes_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorMes.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMes.Text))
                {
                    errorProvider1.SetError(txtValorMes, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorMes.Text);
                    txtValorMes.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorMes, "");
                }
            }
            else
                txtValorMes.Text = "0,00";
        }

        private void ListaHistEquip(int IDEQUIPAMENTO)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", IDEQUIPAMENTO.ToString()));
            LIS_EQUIPAMENTOOSFECHColl = LIS_EQUIPAMENTOOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");

            //Colocando somatorio no final da lista
            LIS_EQUIPAMENTOOSFECHEntity LIS_EQUIPAMENTOOSFECHTy = new LIS_EQUIPAMENTOOSFECHEntity();
            LIS_EQUIPAMENTOOSFECHTy.VALORTOTAL = SumTotalPesquisa("VALORTOTAL");
            LIS_EQUIPAMENTOOSFECHColl.Add(LIS_EQUIPAMENTOOSFECHTy);

            DSHistorico.AutoGenerateColumns = false;
            DSHistorico.DataSource = LIS_EQUIPAMENTOOSFECHColl;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_EQUIPAMENTOColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_EQUIPAMENTOEntity>(orderBy);

                LIS_EQUIPAMENTOColl.Sort(comparer.Comparer);

                DataGriewDados.DataSource = null;
                DataGriewDados.DataSource = LIS_EQUIPAMENTOColl;

            }
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);
            monthCalendar2.ShowWeekNumbers = true;

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(178, 156);
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

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario2 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar3";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);
            monthCalendar3.ShowWeekNumbers = true;
            FormCalendario2.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario2.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario2.ClientSize = new System.Drawing.Size(178, 156);
            FormCalendario2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario2.Location = new Point(230, 55);
            FormCalendario2.Name = "FrmCalendario";
            FormCalendario2.Text = "Calendário";
            FormCalendario2.ResumeLayout(false);
            FormCalendario2.Controls.Add(monthCalendar3);
            FormCalendario2.ShowDialog();
        }


        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            mdkDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario2.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DSHistorico, RelatorioTitulo, this.Name);
        }

        private void maskedtxtData_Validating(object sender, CancelEventArgs e)
        {
            if (maskedtxtData.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    maskedtxtData.Focus();
                    errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(maskedtxtData, "");
                }
            }
        }

        private void mdkDataFinal_Validating(object sender, CancelEventArgs e)
        {
            if (mdkDataFinal.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mdkDataFinal.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    mdkDataFinal.Focus();
                    errorProvider1.SetError(mdkDataFinal, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(mdkDataFinal, "");
                }
            }
        }

        private void btnLimpaFiltro_Click(object sender, EventArgs e)
        {
             if (_IDEQUIPAMENTO != -1)
                ListaHistEquip(_IDEQUIPAMENTO);
             else
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMarca.SelectTab(3);
            }

        }

        private void btnFiltra_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbEquipamento.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbEquipamento, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                    RowRelatorio.Clear();
                    if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario2.SelectedValue.ToString()));

                    if (maskedtxtData.Text != "  /  /")
                        RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(maskedtxtData.Text)));

                    if (mdkDataFinal.Text != "  /  /")
                        RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mdkDataFinal.Text)));

                      RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", cbEquipamento.SelectedValue.ToString()));

                    LIS_EQUIPAMENTOOSFECHColl = LIS_EQUIPAMENTOOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");

                    //Colocando somatorio no final da lista
                    LIS_EQUIPAMENTOOSFECHEntity LIS_EQUIPAMENTOOSFECHTy = new LIS_EQUIPAMENTOOSFECHEntity();
                    LIS_EQUIPAMENTOOSFECHTy.VALORTOTAL = SumTotalPesquisa("VALORTOTAL");
                    LIS_EQUIPAMENTOOSFECHTy.QUANTIDADE = SumTotalPesquisa("QUANTIDADE");
                    LIS_EQUIPAMENTOOSFECHTy.QUANTLOCACAO = SumTotalPesquisa("QUANTLOCACAO");
            
                    LIS_EQUIPAMENTOOSFECHColl.Add(LIS_EQUIPAMENTOOSFECHTy);

                    DSHistorico.AutoGenerateColumns = false;
                    DSHistorico.DataSource = LIS_EQUIPAMENTOOSFECHColl;
                }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_EQUIPAMENTOOSFECHEntity item in LIS_EQUIPAMENTOOSFECHColl)
            {
                if (NomeCampo == "VALORTOTAL")
                    valortotal += Convert.ToDecimal(item.VALORTOTAL);
                if (NomeCampo == "QUANTIDADE")
                    valortotal += Convert.ToDecimal(item.QUANTIDADE);
                if (NomeCampo == "QUANTLOCACAO")
                    valortotal += Convert.ToDecimal(item.QUANTLOCACAO);
               }

            return valortotal;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbTipoPesquisa.Text == "Todos")
            {
                LIS_EQUIPAMENTOColl = LIS_EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_EQUIPAMENTOColl;

                lblTotalPesquisa.Text = LIS_EQUIPAMENTOColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
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
            if (LIS_EQUIPAMENTOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_EQUIPAMENTOColl;
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

                LIS_EQUIPAMENTOColl = LIS_EQUIPAMENTOP.ReadCollectionByParameter(Filtro, "NOME");
             

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_EQUIPAMENTOColl;

                lblTotalPesquisa.Text = LIS_EQUIPAMENTOColl.Count.ToString();
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
            LIS_EQUIPAMENTOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_EQUIPAMENTOColl.Count.ToString();
        }

        private void btnAddDestino_Click(object sender, EventArgs e)
        {
            if (Validacoes2())
            {
                try
                {
                    DESTINOEQUIPAMENTOP.Save(Entity2);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    ListaDestinoEquipamento(_IDEQUIPAMENTO);
                    Entity2 = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }

            }

        }

        private void ListaDestinoEquipamento(int IDEQUIPAMENTO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", IDEQUIPAMENTO.ToString()));
                DESTINOEQUIPAMENTOColl = DESTINOEQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio, "DATA, HORA desc");

                dgLocalizacao.AutoGenerateColumns = false;
                dgLocalizacao.DataSource = DESTINOEQUIPAMENTOColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
            
        }

        private Boolean Validacoes2()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtDestino.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtDestino, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (_IDEQUIPAMENTO == -1)
            {
                MessageBox.Show("Antes de adicionar a localização é necessário gravar o equipamento!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Destino do Equipamento: " + txtNome.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dgLocalizacao, RelatorioTitulo, this.Name);
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Equipamentos");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dgLocalizacao, RelatorioTitulo, this.Name);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpaLocaliza_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void dgLocalizacao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (DESTINOEQUIPAMENTOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(DESTINOEQUIPAMENTOColl[rowindex].IDDESTINOEQUIPAMENTO);
                    Entity2 = DESTINOEQUIPAMENTOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                           
                            CodSelect = Convert.ToInt32(DESTINOEQUIPAMENTOColl[rowindex].IDDESTINOEQUIPAMENTO);
                            DESTINOEQUIPAMENTOP.Delete(CodSelect);
                            ListaDestinoEquipamento(_IDEQUIPAMENTO);
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

        private void btnCadEsquip_Click(object sender, EventArgs e)
        {
            using (FrmEquipamento frm = new FrmEquipamento())
            {
                int CodSelec = Convert.ToInt32(cbEquipamento.SelectedValue);
                frm._IDEQUIPAMENTO = CodSelec;
                frm.ShowDialog();

                GetDropEquipamento();
                cbEquipamento.SelectedValue = CodSelec;
            }
        }
    }
}
