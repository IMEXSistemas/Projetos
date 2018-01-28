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
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmMaterial : Form
    {
        MATERIALProvider MATERIALP = new MATERIALProvider();
        LIS_MATERIALProvider LIS_MATERIALP = new LIS_MATERIALProvider();
        FOTOMATERIALProvider FOTOMATERIALP = new FOTOMATERIALProvider();
        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        MATCOTACAOFORNECEDORProvider MATCOTACAOFORNECEDORP = new MATCOTACAOFORNECEDORProvider();
        LIS_MATCOTACAOFORNECEDORProvider LIS_MATCOTACAOFORNECEDORP = new LIS_MATCOTACAOFORNECEDORProvider();

        MARCACollection MarcaColl = new MARCACollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
        LIS_MATERIALCollection LIS_MATERIALColl = new LIS_MATERIALCollection();
        MATERIALCollection MATERIALComposicaoColl = new MATERIALCollection();
        FORNECEDORCollection FornecedorColl = new FORNECEDORCollection();
        FORNECEDORCollection FornecedorComposicaoColl = new FORNECEDORCollection();
        LIS_MATCOTACAOFORNECEDORCollection Lis_MATCOTACAOFORNECEDORColl = new LIS_MATCOTACAOFORNECEDORCollection();
        CLASSFISCALCollection CLASSFISCALColl = new CLASSFISCALCollection();
        LIS_CSTCollection LIS_CSTColl = new LIS_CSTCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmMaterial()
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
            lblobsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDMATERIAL = -1;
      
        public MATERIALEntity Entity
        {
            get
            {
               string NOMEPRODUTO = txtNome.Text;
               string CODPRODUTOFORNECEDOR = txtCodMadFornecedor.Text;
               string CODBARRA = txtCodBarra.Text;
               string LOCALIZACAO = txtLocalizacao.Text;

               string DATACADASTRO = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtDtCadastro.Text != string.Empty)
                    DATACADASTRO = txtDtCadastro.Text;

                int IDUNIDADE = Convert.ToInt32(cbUnidade.SelectedValue.ToString()); 
                
               int? IDMARCA = null;
                    if(cbMarca.SelectedIndex > 0)
                        IDMARCA =Convert.ToInt32(cbMarca.SelectedValue.ToString());

               int IDMOEDA = Convert.ToInt32(cbMoeda.SelectedValue.ToString()); 

               decimal? VALORCUSTOINICIAL = null;
                    if(txtValorCustoInicial.Text != string.Empty)
                        VALORCUSTOINICIAL= Convert.ToDecimal(txtValorCustoInicial.Text);

               decimal? FRETEPRODUTO = null;
                    if(txtValorFrete.Text != string.Empty)
                        FRETEPRODUTO= Convert.ToDecimal(txtValorFrete.Text); 

               decimal? ENCARGOSPRODUTOS = null;
                    if(txtValorEncargos.Text != string.Empty)
                        ENCARGOSPRODUTOS= Convert.ToDecimal(txtValorEncargos.Text); 

               decimal? VALORCUSTOFINAL = null;
                    if(txtValorCustoFinal.Text != string.Empty)
                        VALORCUSTOFINAL= Convert.ToDecimal(txtValorCustoFinal.Text); 

               decimal? MARGEMLUCRO = null;
                    if(txtValorMargemLucro.Text != string.Empty)
                        MARGEMLUCRO= Convert.ToDecimal(txtValorMargemLucro.Text); 

              decimal? VALORVENDA1 = null;
                    if(txtValorVenda1.Text != string.Empty)
                        VALORVENDA1= Convert.ToDecimal(txtValorVenda1.Text); 

              decimal? VALORVENDA2 = null;
                    if(txtValorVenda2.Text != string.Empty)
                        VALORVENDA2= Convert.ToDecimal(txtValorVenda2.Text); 


              decimal? VALORVENDA3 = null;
                    if(txtValorVenda3.Text != string.Empty)
                        VALORVENDA3= Convert.ToDecimal(txtValorVenda3.Text); 

              decimal? COMISSAO = null;
                    if(txtPorComissao.Text != string.Empty)
                        COMISSAO= Convert.ToDecimal(txtPorComissao.Text); 
               
              decimal? IPI = null;
                    if(txtPorcIPI.Text != string.Empty)
                        IPI= Convert.ToDecimal(txtPorcIPI.Text); 
              
              decimal? ICMS = null;
                    if(txtPorcICMS.Text != string.Empty)
                        ICMS= Convert.ToDecimal(txtPorcICMS.Text);


              decimal? QUANTIDADEMINIMA = null;
                    if(txtQuantMinima.Text != string.Empty)
                        QUANTIDADEMINIMA = Convert.ToDecimal(txtQuantMinima.Text);

              decimal? ESTOQUEATUAL = null;
                    if(txtEstoqueAtual.Text != string.Empty)
                        ESTOQUEATUAL = Convert.ToDecimal(txtEstoqueAtual.Text); 

              int? IDGRUPOCATEGORIA = null;
                    if(cbGrupoCategoria.SelectedIndex > 0)
                        IDGRUPOCATEGORIA =Convert.ToInt32(cbGrupoCategoria.SelectedValue.ToString());


               int? IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue.ToString());
               string OBSERVACAO = txtObservacao.Text;

               decimal? PORCFRETE = null;
               if (txtPorcFrete.Text != string.Empty)
                   PORCFRETE = Convert.ToDecimal(txtPorcFrete.Text);  

               decimal? PORCENCARGOS= null;
               if (txtPorcEncargos.Text != string.Empty)
                   PORCENCARGOS = Convert.ToDecimal(txtPorcEncargos.Text);             

               decimal? PORCMARGEMLUCRO= null;
               if (txtPorcMargemLucro.Text != string.Empty)
                   PORCMARGEMLUCRO = Convert.ToDecimal(txtPorcMargemLucro.Text);             

               decimal? PORCVENDA2= null;
               if (txtPorcValorVenda2.Text != string.Empty)
                   PORCVENDA2 = Convert.ToDecimal(txtPorcValorVenda2.Text);

               decimal? PORCVENDA3 = null;
               if (txtPorcValorVenda3.Text != string.Empty)
                   PORCVENDA3 = Convert.ToDecimal(txtPorcValorVenda3.Text);

               decimal? PESO = null;
               if (txtPesoProduto.Text != string.Empty)
                   PESO = Convert.ToDecimal(txtPesoProduto.Text);  

               int? IDCLASSIFICACAO = null;
                if(cbClassFiscal.SelectedIndex > 0)
                    IDCLASSIFICACAO = Convert.ToInt32(cbClassFiscal.SelectedValue);

               int? IDCST = null;
                if(cbSitTributaria.SelectedIndex > 0)
                    IDCST = Convert.ToInt32(cbSitTributaria.SelectedValue);

                string NOMECIENTIFICO = txtNomeCient.Text;


                return new MATERIALEntity( _IDMATERIAL, NOMEPRODUTO, CODPRODUTOFORNECEDOR,
                                          CODBARRA, LOCALIZACAO,Convert.ToDateTime(DATACADASTRO),
                                          IDUNIDADE, IDMARCA, 
                                          IDMOEDA, VALORCUSTOINICIAL, FRETEPRODUTO, ENCARGOSPRODUTOS,
                                          VALORCUSTOFINAL, MARGEMLUCRO, VALORVENDA1, VALORVENDA2, VALORVENDA3, 
                                          COMISSAO, IPI, ICMS, QUANTIDADEMINIMA, ESTOQUEATUAL, IDGRUPOCATEGORIA,
                                          IDSTATUS, OBSERVACAO, 
                                          PORCFRETE, PORCENCARGOS,
                                          PORCMARGEMLUCRO, PORCVENDA2, PORCVENDA3, PESO,
                                          IDCLASSIFICACAO, IDCST, NOMECIENTIFICO);
            }
            set
            {

                if (value != null)
                {
                    _IDMATERIAL = value.IDMATERIAL;
                    txtCodMaterial.Text = value.IDMATERIAL.ToString();
                    txtCodMaterial.Focus();
                    txtNome.Text = value.NOMEMATERIAL;

                    //Dados para tela de Cotação
                    txtCodMadeiraFornecCotacao.Text = value.IDMATERIAL.ToString();
                    txtNomeMadeiraFornecCotacao.Text = value.NOMEMATERIAL;
                    GetProdutoFornecedorCotacao(value.IDMATERIAL);

                    txtCodMadFornecedor.Text = value.CODMATERIALFORNECEDOR;
                    txtCodBarra.Text = value.CODBARRA;
                    txtLocalizacao.Text = value.LOCALIZACAO;
                    txtDtCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");
                    cbUnidade.SelectedValue = value.IDUNIDADE;

                    if (value.IDMARCA != null)
                        cbMarca.SelectedValue = value.IDMARCA;
                    else
                        cbMarca.SelectedIndex = 0;

                    cbMoeda.SelectedValue = value.IDMOEDA;

                    txtValorCustoInicial.Text = Convert.ToString(value.VALORCUSTOINICIAL);
                    if (value.VALORCUSTOINICIAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorCustoInicial.Text);
                        txtValorCustoInicial.Text = string.Format("{0:n2}", f);
                    }

                    txtValorFrete.Text = Convert.ToString(value.FRETEMATERIAL);
                    if (value.FRETEMATERIAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorFrete.Text);
                        txtValorFrete.Text = string.Format("{0:n2}", f);
                    }

                    txtValorEncargos.Text = Convert.ToString(value.ENCARGOSMATERIAL);
                    if (value.ENCARGOSMATERIAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorEncargos.Text);
                        txtValorEncargos.Text = string.Format("{0:n2}", f);
                    }

                    txtValorCustoFinal.Text = Convert.ToString(value.VALORCUSTOFINAL);
                    if (value.VALORCUSTOFINAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorCustoFinal.Text);
                        txtValorCustoFinal.Text = string.Format("{0:n2}", f);
                    }

                    txtValorMargemLucro.Text = Convert.ToString(value.MARGEMLUCRO);
                    if (value.MARGEMLUCRO != null)
                    {
                        Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                        txtValorMargemLucro.Text = string.Format("{0:n2}", f);
                    }

                    txtValorVenda1.Text = Convert.ToString(value.VALORVENDA1);
                    if (value.VALORVENDA1 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda1.Text);
                        txtValorVenda1.Text = string.Format("{0:n2}", f);
                    }

                    txtValorVenda2.Text = Convert.ToString(value.VALORVENDA2);
                    if (value.VALORVENDA2 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda2.Text);
                        txtValorVenda2.Text = string.Format("{0:n2}", f);
                    }

                    txtValorVenda3.Text = Convert.ToString(value.VALORVENDA3);
                    if (value.VALORVENDA3 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda3.Text);
                        txtValorVenda3.Text = string.Format("{0:n2}", f);
                    }

                    txtPorComissao.Text = Convert.ToString(value.COMISSAO);
                    if (value.COMISSAO != null)
                    {
                        Double f = Convert.ToDouble(txtPorComissao.Text);
                        txtPorComissao.Text = string.Format("{0:n2}", f);
                    }

                    txtPorcIPI.Text = Convert.ToString(value.IPI);
                    if (value.IPI != null)
                    {
                        Double f = Convert.ToDouble(txtPorcIPI.Text);
                        txtPorcIPI.Text = string.Format("{0:n2}", f);
                    }

                    txtPorcICMS.Text = Convert.ToString(value.ICMS);
                    if (value.ICMS != null)
                    {
                        Double f = Convert.ToDouble(txtPorcICMS.Text);
                        txtPorcICMS.Text = string.Format("{0:n2}", f);
                    }

                    txtQuantMinima.Text = Convert.ToString(value.QUANTIDADEMINIMA);
                    if (value.QUANTIDADEMINIMA != null)
                    {
                        Double f = Convert.ToDouble(txtQuantMinima.Text);
                        txtQuantMinima.Text = string.Format("{0:n3}", f);
                    }


                    txtEstoqueAtual.Text = Convert.ToString(value.ESTOQUEATUAL);
                    if (value.ESTOQUEATUAL != null)
                    {
                        Double f = Convert.ToDouble(txtEstoqueAtual.Text);
                        txtEstoqueAtual.Text = string.Format("{0:n3}", f);
                    }
                    
                    if(value.IDGRUPOCATEGORIA != null)
                        cbGrupoCategoria.SelectedValue = value.IDGRUPOCATEGORIA;
                    else
                        cbGrupoCategoria.SelectedIndex = 0;

                    cbStatus.SelectedValue = value.IDSTATUS;
                    txtObservacao.Text = value.OBSERVACAO;

                    if (value.PORCFRETE != null)
                    {
                        txtPorcFrete.Text = value.PORCFRETE.ToString();
                         Double f = Convert.ToDouble(txtPorcFrete.Text);
                         txtPorcFrete.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcFrete.Text = string.Empty;

                    if (value.PORCENCARGOS != null)
                    {
                        txtPorcEncargos.Text = value.PORCENCARGOS.ToString();
                        Double f = Convert.ToDouble(txtPorcEncargos.Text);
                        txtPorcEncargos.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcEncargos.Text = string.Empty;

                    if (value.PORCMARGEMLUCRO != null)
                    {
                        txtPorcMargemLucro.Text = value.PORCMARGEMLUCRO.ToString();
                        Double f = Convert.ToDouble(txtPorcMargemLucro.Text);
                        txtPorcMargemLucro.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcMargemLucro.Text = string.Empty;

                    if (value.PORCVENDA2 != null)
                    {
                        txtPorcValorVenda2.Text = value.PORCVENDA2.ToString();
                        Double f = Convert.ToDouble(txtPorcValorVenda2.Text);
                        txtPorcValorVenda2.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcValorVenda2.Text = string.Empty;

                    if (value.PORCVENDA3 != null)
                    {
                        txtPorcValorVenda3.Text = value.PORCVENDA3.ToString();
                        Double f = Convert.ToDouble(txtPorcValorVenda3.Text);
                        txtPorcValorVenda3.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcValorVenda3.Text = string.Empty;

                    if (value.PESO != null)
                    {
                        txtPesoProduto.Text = value.PESO.ToString();
                        Double f = Convert.ToDouble(txtPesoProduto.Text);
                        txtPesoProduto.Text = string.Format("{0:n3}", f);
                    }
                    else
                        txtPesoProduto.Text = "0,00";


                    if (value.IDCLASSIFICACAO != null)
                        cbClassFiscal.SelectedValue = value.IDCLASSIFICACAO;
                    else
                        cbClassFiscal.SelectedIndex = 0;

                    if (value.IDCST != null)
                        cbSitTributaria.SelectedValue = value.IDCST;
                    else
                        cbSitTributaria.SelectedIndex = 0;

                    txtNomeCient.Text = value.NOMECIENTIFICO;
                  

                    errorProvider1.SetError(txtNome, "");
                }
                else
                {
                   _IDMATERIAL = -1;
                    txtCodMaterial.Text = string.Empty;
                    txtNome.Text = string.Empty;

                    //Dados para tela de Cotação
                    txtCodMadeiraFornecCotacao.Text = string.Empty;
                    txtNomeMadeiraFornecCotacao.Text = string.Empty;
                    GetProdutoFornecedorCotacao(-1);
                    txtCodMadFornecedor.Text = string.Empty;
                    txtCodBarra.Text = string.Empty;
                    txtLocalizacao.Text = string.Empty;
                    txtDtCadastro.Text = string.Empty;
                    cbUnidade.SelectedIndex = 0;
                    cbMarca.SelectedIndex = 0;
                    cbMoeda.SelectedValue = ConfigSistema1.Default.CodMoedaSelec;
                    txtValorCustoInicial.Text = "0,00";
                    txtValorFrete.Text = "0,00";
                    txtValorEncargos.Text = "0,00";
                    txtValorCustoFinal.Text = "0,00";
                    txtValorMargemLucro.Text = "0,00";
                    txtValorVenda1.Text = "0,00";
                    txtValorVenda2.Text = "0,00";
                    txtValorVenda3.Text = "0,00";
                    txtPorComissao.Text = "0,00";
                    txtPorcIPI.Text = "0,00";
                    txtPorcICMS.Text = "0,00";
                    txtQuantMinima.Text = "0,000";
                    txtEstoqueAtual.Text = "0,000";
                    cbGrupoCategoria.SelectedIndex = 0;
                    txtPorcFrete.Text = "0,00";
                    txtPorcEncargos.Text = "0,00";
                    txtPorcMargemLucro.Text = "0,00";
                    txtPorcValorVenda2.Text = "0,00";
                    txtPorcValorVenda3.Text = "0,00";
                    cbStatus.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    txtPesoProduto.Text = "0,00";
                    txtNomeCient.Text = string.Empty;

                    cbClassFiscal.SelectedIndex = 0;
                    cbSitTributaria.SelectedIndex = 0;

                    errorProvider1.SetError(txtNome, "");                    
                }


            }
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

         byte[] _FOTO = null;
         int _IDFOTOMATERIAL = -1;
       
        public FOTOMATERIALEntity Entity2
        {
            get
            {
                string NOME = openFileDialog1.FileName.ToString();
                string TIPO = openFileDialog1.FileName.ToString();
                string OBSERVACAO = string.Empty;

                return new FOTOMATERIALEntity(_IDFOTOMATERIAL, NOME, TIPO, OBSERVACAO, _IDMATERIAL, _FOTO);
            }
            set
            {

                if (value != null)
                {
                    _IDFOTOMATERIAL = value.IDFOTOMATERIAL;
                    txtNomeFoto.Text = value.NOME;

                    _FOTO = value.FOTO;
                    MemoryStream stream = new MemoryStream(_FOTO);
                    pictureBox1.Image = Image.FromStream(stream);

                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                
                }
                else
                {
                   _IDFOTOMATERIAL = -1;
                    _FOTO = null;
                    pictureBox1.Image = null;
                    txtNomeFoto.Text = string.Empty;
                }
            }
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetToolStripButtonCadastro();
            GetDropUnidade();
            GetDropMarca();
            GetDropMoeda();
            GetDropGrupoCategoria();
            GetDropStatus();
            GetDropFornecedor();
            GetDropClassFiscal();
            GetDropSitTrib();

            btnCadMarca.Image = Util.GetAddressImage(6);
            bntCadUnidade.Image = Util.GetAddressImage(6);
            btnCadGrupoCategoria.Image = Util.GetAddressImage(6);
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnMaquina.Image = Util.GetAddressImage(7);
            btnCadSitTributaria.Image = Util.GetAddressImage(6);
            btnCadClass.Image = Util.GetAddressImage(6);

            txtCodMaterial.Focus();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            VerificaAcesso();

            this.Cursor = Cursors.Default;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        
        private void GetDropClassFiscal()
        {
            CLASSFISCALProvider CLASSFISCALP = new CLASSFISCALProvider();
            CLASSFISCALColl = CLASSFISCALP.ReadCollectionByParameter(null, "CODIGO");

            cbClassFiscal.DisplayMember = "CODIGO";
            cbClassFiscal.ValueMember = "IDCLASSFISCAL";

            CLASSFISCALEntity CLASSFISCALTy = new CLASSFISCALEntity();
            CLASSFISCALTy.CODIGO = ConfigMessage.Default.MsgDrop;
            CLASSFISCALTy.IDCLASSFISCAL = -1;
            CLASSFISCALColl.Add(CLASSFISCALTy);

            Phydeaux.Utilities.DynamicComparer<CLASSFISCALEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLASSFISCALEntity>(cbClassFiscal.DisplayMember);

            CLASSFISCALColl.Sort(comparer.Comparer);
            cbClassFiscal.DataSource = CLASSFISCALColl;

            cbClassFiscal.SelectedIndex = 0;
        }

        private void GetDropSitTrib()
        {
           
            LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();
            LIS_CSTColl = LIS_CSTP.ReadCollectionByParameter(null, "CODIGO");

            cbSitTributaria.DisplayMember = "CODCOMPL";
            cbSitTributaria.ValueMember = "IDCST";

            LIS_CSTEntity LIS_CSTTy = new LIS_CSTEntity();
            LIS_CSTTy.CODCOMPL = ConfigMessage.Default.MsgDrop;
            LIS_CSTTy.IDCST = -1;
            LIS_CSTColl.Add(LIS_CSTTy);

            Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity>(cbSitTributaria.DisplayMember);

            LIS_CSTColl.Sort(comparer.Comparer);
            cbSitTributaria.DataSource = LIS_CSTColl;

            cbSitTributaria.SelectedIndex = 0;
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

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void GetDropUnidade()
        {
            UNIDADEProvider UNIDADEP = new UNIDADEProvider();
            cbUnidade.DataSource = UNIDADEP.ReadCollectionByParameter(null, "NOME");

            cbUnidade.DisplayMember = "NOME";
            cbUnidade.ValueMember = "IDUNIDADE";

            cbUnidade.SelectedIndex = 0;
        }

        private void GetDropMoeda()
        {
            MOEDAProvider MOEDAP = new MOEDAProvider();

            cbMoeda.DisplayMember = "NOME";
            cbMoeda.ValueMember = "IDMOEDA";
           
            cbMoeda.DataSource = MOEDAP.ReadCollectionByParameter(null, "NOME");
            cbMoeda.SelectedValue = ConfigSistema1.Default.CodMoedaSelec;
        }

        private void GetDropMarca()
        {
            MARCAProvider MARCAP = new MARCAProvider();
            MarcaColl = MARCAP.ReadCollectionByParameter(null, "NOME");

            cbMarca.DisplayMember = "NOME";
            cbMarca.ValueMember = "IDMARCA";

            MARCAEntity MARCATy = new MARCAEntity();
            MARCATy.NOME = ConfigMessage.Default.MsgDrop;
            MARCATy.IDMARCA = -1;
            MarcaColl.Add(MARCATy);

            Phydeaux.Utilities.DynamicComparer<MARCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MARCAEntity>(cbMarca.DisplayMember);

            MarcaColl.Sort(comparer.Comparer);
            cbMarca.DataSource = MarcaColl;

            cbMarca.SelectedIndex = 0;
        }

        private void GetDropFornecedor()
        {
            FornecedorColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbFornecedCotacao.DisplayMember = "NOME";
            cbFornecedCotacao.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FornecedorColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedCotacao.DisplayMember);

            FornecedorColl.Sort(comparer.Comparer);
            cbFornecedCotacao.DataSource = FornecedorColl;

            cbFornecedCotacao.SelectedIndex = 0;
        }
        private void GetDropGrupoCategoria()
        {
            GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
            GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null, "NOME");

            cbGrupoCategoria.DisplayMember = "NOME";
            cbGrupoCategoria.ValueMember = "IDGRUPOCATEGORIA";

            GRUPOCATEGORIAEntity GRUPOCATEGORIATy = new GRUPOCATEGORIAEntity();
            GRUPOCATEGORIATy.NOME = ConfigMessage.Default.MsgDrop;
            GRUPOCATEGORIATy.IDGRUPOCATEGORIA = -1;
            GRUPOCATEGORIAColl.Add(GRUPOCATEGORIATy);

            Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity>(cbGrupoCategoria.DisplayMember);

            GRUPOCATEGORIAColl.Sort(comparer.Comparer);
            cbGrupoCategoria.DataSource = GRUPOCATEGORIAColl;

            cbGrupoCategoria.SelectedIndex = 0;
        }

        private void GetDropStatus()
        {
            //14 Material
            RowsFiltro FiltroProfileCNPJ = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "14");
            RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

            FiltroCNPJ.Insert(0, FiltroProfileCNPJ);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(FiltroCNPJ);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";

        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntCadUnidade_Click(object sender, EventArgs e)
        {
            using (FrmUnidade frm = new FrmUnidade())
            {
                frm.ShowDialog();
            }
        }

        private void btnCadMarca_Click(object sender, EventArgs e)
        {
            using (FrmMarca frm = new FrmMarca())
            {
                frm.ShowDialog();
            }
        }

        private void cbUnidade_Enter(object sender, EventArgs e)
        {
            GetDropUnidade();
        }

        private void cbMarca_Enter(object sender, EventArgs e)
        {
            GetDropMarca();
        }

        private void cbGrupoCategoria_Enter(object sender, EventArgs e)
        {
            GetDropGrupoCategoria();
        }

        private void btnCadGrupoCategoria_Click(object sender, EventArgs e)
        {
            using (FrmGrupoCategoria frm = new FrmGrupoCategoria())
            {
                frm.ShowDialog();
            }
        }

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
            }
        }

        private void cbStatus_Enter(object sender, EventArgs e)
        {
            GetDropStatus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código do material, digite um valor e pressione Ctrl+E para pesquisar.";
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodMaterial.Text))
                {
                    errorProvider1.SetError(txtCodMaterial, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodMaterial.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtCodMaterial, "");

                    MATERIALEntity MATERIALCons = new MATERIALEntity();
                    MATERIALCons = MATERIALP.Read(Convert.ToInt32(txtCodMaterial.Text));

                    if (MATERIALCons != null)
                    {
                        Entity = MATERIALCons;
                        MessageBox.Show(ConfigMessage.Default.MsgSearchSucess);
                    }
                    else
                        MessageBox.Show(ConfigMessage.Default.MsgSearchErro);
                }               

                e.SuppressKeyPress = true;
            }
        }

        private void cbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUnidade_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbMarca_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbMoeda_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbMoeda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbGrupoCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbGrupoCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcFrete.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcFrete.Text))
                {
                    errorProvider1.SetError(txtPorcFrete, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcFrete.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcFrete.Text);
                    txtPorcFrete.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcFrete, "");
                }
            }
        }

        private void txtPorcEncargos_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcEncargos.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcEncargos.Text))
                {
                    errorProvider1.SetError(txtPorcEncargos, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcEncargos.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcEncargos.Text);
                    txtPorcEncargos.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcEncargos, "");
                }
            }
        }

        private void txtPorcValorVenda2_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcValorVenda2.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcValorVenda2.Text))
                {
                    errorProvider1.SetError(txtPorcValorVenda2, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcValorVenda2.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcValorVenda2.Text);
                    txtPorcValorVenda2.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcValorVenda2, "");
                }
            }
        }

        private void txtPorcValorVenda3_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcValorVenda3.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcValorVenda3.Text))
                {
                    errorProvider1.SetError(txtPorcValorVenda3, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcValorVenda3.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcValorVenda3.Text);
                    txtPorcValorVenda3.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcValorVenda3, "");
                }
            }
        }

        private void txtPorComissao_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorComissao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorComissao.Text))
                {
                    errorProvider1.SetError(txtPorComissao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorComissao.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorComissao.Text);
                    txtPorComissao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorComissao, "");
                }
            }
        }

        private void txtPorcIPI_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcIPI.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcIPI.Text))
                {
                    errorProvider1.SetError(txtPorcIPI, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcIPI.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcIPI.Text);
                    txtPorcIPI.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcIPI, "");
                }
            }
        }

        private void txtPorcICMS_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcICMS.Text))
                {
                    errorProvider1.SetError(txtPorcICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcICMS.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcICMS.Text);
                    txtPorcICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcICMS, "");
                }
            }
        }

        private void txtValorCustoInicial_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorCustoInicial.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCustoInicial.Text))
                {
                    errorProvider1.SetError(txtValorCustoInicial, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorCustoInicial.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorCustoInicial.Text);
                    txtValorCustoInicial.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorCustoInicial, "");
                }
            }
        }

        private void txtValorFrete_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorFrete.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorFrete.Text))
                {
                    errorProvider1.SetError(txtValorFrete, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorFrete.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorFrete.Text);
                    txtValorFrete.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorFrete, "");
                }
            }
        }

        private void txtValorEncargos_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorEncargos.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorEncargos.Text))
                {
                    errorProvider1.SetError(txtValorEncargos, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorEncargos.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorEncargos.Text);
                    txtValorEncargos.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorVenda1, "");
                }
            }
        }

        private void txtValorVenda1_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorVenda1.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda1.Text))
                {
                    errorProvider1.SetError(txtValorVenda1, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorVenda1.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorVenda1.Text);
                    txtValorVenda1.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorVenda1, "");
                }
            }
        }

        private void txtValorVenda2_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorVenda2.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda2.Text))
                {
                    errorProvider1.SetError(txtValorVenda2, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorVenda2.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorVenda2.Text);
                    txtValorVenda2.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorVenda2, "");
                }
            }
        }

        private void txtValorVenda3_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorVenda3.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda3.Text))
                {
                    errorProvider1.SetError(txtValorVenda3, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorVenda3.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorVenda3.Text);
                    txtValorVenda3.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorVenda3, "");
                }
            }
        }

        private void txtValorCustoFinal_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorCustoFinal.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCustoFinal.Text))
                {
                    errorProvider1.SetError(txtValorCustoFinal, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorCustoFinal.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorCustoFinal.Text);
                    txtValorCustoFinal.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorCustoFinal, "");
                }
            }
        }        

       
        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png" ; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtNomeFoto.Text = openFileDialog1.FileName.ToString();
                _FOTO = GetFoto(openFileDialog1.FileName);

                MemoryStream stream = new MemoryStream(_FOTO);
                pictureBox1.Image = Image.FromStream(stream);

                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
            }
        }

        private void lkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtNomeFoto.Text != string.Empty)
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

                    MemoryStream stream = new MemoryStream(_FOTO);
                    PcBox.Image = Image.FromStream(stream);

                    PcBox.Dock = System.Windows.Forms.DockStyle.Fill;
                    PcBox.Location = new System.Drawing.Point(0, 0);

                    PcBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    FormFoto.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    FormFoto.Controls.Add(PcBox);
                    FormFoto.ShowDialog();
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
                
            }
        }

        

        private void txtValorMargemLucro_Validating(object sender, CancelEventArgs e)
        {

            if (txtValorMargemLucro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMargemLucro.Text))
                {
                    errorProvider1.SetError(txtValorMargemLucro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorMargemLucro.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                    txtValorMargemLucro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorMargemLucro, "");
                }
            }
        }

        private void txtQuantMinima_Validating(object sender, CancelEventArgs e)
        {
            if (txtQuantMinima.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantMinima.Text))
                {
                    errorProvider1.SetError(txtQuantMinima, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtQuantMinima.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtQuantMinima.Text);
                    txtQuantMinima.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtQuantMinima, "");
                }
            }
        }

        private void txtEstoqueAtual_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstoqueAtual.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtEstoqueAtual.Text))
                {
                    errorProvider1.SetError(txtEstoqueAtual, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtEstoqueAtual.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtEstoqueAtual.Text);
                    txtEstoqueAtual.Text = string.Format("{0:n3}", f);

                    errorProvider1.SetError(txtEstoqueAtual, "");
                }
            }
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Boolean ErroProviderValid = true;
            foreach (Control ctrl in this.Controls)
            {
                ErroProviderValid = Validate();
                if (!ErroProviderValid)
                {
                    ErroProviderValid = false;
                    break;
                }

            }

            if (ErroProviderValid)
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
                Grava();
                this.Cursor = Cursors.Default;
            }
        }

        private void TSBGrava_Click_1(object sender, EventArgs e)
        {
            Boolean ErroProviderValid = true;
            foreach (Control ctrl in this.Controls)
            {
                ErroProviderValid = Validate();
                if (!ErroProviderValid)
                    break;

            }

            if (ErroProviderValid)
                Grava();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    if (_IDMATERIAL == -1)
                    {
                        _IDMATERIAL = MATERIALP.Save(Entity);
                         
                        //Condição para salvar foto
                        if (txtNomeFoto.Text != string.Empty)
                            _IDFOTOMATERIAL = FOTOMATERIALP.Save(Entity2);


                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        txtCodMaterial.Text = Entity.IDMATERIAL.ToString();
                    }
                    else
                    {
                        _IDMATERIAL = MATERIALP.Save(Entity);

                        if (txtNomeFoto.Text != string.Empty)
                            _IDFOTOMATERIAL = FOTOMATERIALP.Save(Entity2);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        button4_Click(null, null);
                    }
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
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbStatus, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.SetError(txtNome, "");


            return result;
        }

        private void txtPorcMargemLucro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcMargemLucro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcMargemLucro.Text))
                {
                    errorProvider1.SetError(txtPorcMargemLucro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcMargemLucro.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcMargemLucro.Text);
                    txtPorcMargemLucro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcMargemLucro, "");
                }
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_MATERIALColl = LIS_MATERIALP.ReadCollectionByParameter(Filtro, "NOMEMATERIAL");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MATERIALColl;

                lblTotalPesquisa.Text = LIS_MATERIALColl.Count.ToString();
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
            // Nome campo que sera filtrado
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_MATERIALColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MATERIALColl;
            }

            // Retorna o tipo da coluna para pesquisa Ex.: String, Integer, Date...
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

                LIS_MATERIALColl = LIS_MATERIALP.ReadCollectionByParameter(Filtro, "NOMEMATERIAL");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MATERIALColl;

                lblTotalPesquisa.Text = LIS_MATERIALColl.Count.ToString();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_MATERIALColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    int CodigoSelect = Convert.ToInt32(LIS_MATERIALColl[rowindex].IDMATERIAL);

                    Entity = MATERIALP.Read(CodigoSelect);

                    int CodFotoProduto = GetFotoProduto(Entity.IDMATERIAL);
                    if (CodFotoProduto != -1)
                        Entity2 = FOTOMATERIALP.Read(Convert.ToInt32(CodFotoProduto));
                    else
                    {
                        Entity2 = null;
                        pictureBox1.Image = null;
                        txtNomeFoto.Text = string.Empty;
                    }

                    tabControlMaterial.SelectTab(0);
                    txtNome.Focus();
                }
            }
        }

        private int GetFotoProduto(int CodMATERIAL)
        {
            int result = -1;

            RowsFiltro filtroFotoProduto = new RowsFiltro();
            filtroFotoProduto = new RowsFiltro("IDMATERIAL", "System.Int32", "=", CodMATERIAL.ToString());
            FOTOMATERIALCollection FOTOMATERIALColl = new FOTOMATERIALCollection();

            RowsFiltroCollection RowsFiltroFotoProduto = new RowsFiltroCollection();
            RowsFiltroFotoProduto.Add(filtroFotoProduto);

           FOTOMATERIALColl = FOTOMATERIALP.ReadCollectionByParameter(RowsFiltroFotoProduto, "IDFOTOMATERIAL");

           if (FOTOMATERIALColl.Count > 0)
               result = FOTOMATERIALColl[0].IDFOTOMATERIAL;

            return result;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            tabControlMaterial.SelectTab(2);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMaterial.SelectTab(2);
        }

        private void txtCodProduto_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodMaterial.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodMaterial.Text))
                {
                    errorProvider1.SetError(txtCodMaterial, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodMaterial.Focus();
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtCodMaterial, "");
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControlMaterial.SelectTab(0);
            txtNome.Focus();
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            Entity2 = null;
            tabControlMaterial.SelectTab(0);
            txtNome.Focus();
        }

        private void txtPorcFrete_Leave(object sender, EventArgs e)
        {
            if (txtPorcFrete.Text == string.Empty)
                txtPorcFrete.Text = "0,00";

            //if (txtValorCustoInicial.Text != string.Empty)
            {
               txtValorFrete.Text = ((Convert.ToDecimal(txtValorCustoInicial.Text) * Convert.ToDecimal(txtPorcFrete.Text)) / 100).ToString();
               Double f = Convert.ToDouble(txtValorFrete.Text);
               txtValorFrete.Text = string.Format("{0:n2}", f);              
            }

          
        }

        private void txtPorcEncargos_Leave(object sender, EventArgs e)
        {
            if (txtPorcEncargos.Text == string.Empty)
                txtPorcEncargos.Text = "0,00";

           // if (txtValorCustoInicial.Text != string.Empty)
            {
                txtValorEncargos.Text = ((Convert.ToDecimal(txtValorCustoInicial.Text) * Convert.ToDecimal(txtPorcEncargos.Text)) / 100).ToString();
                Double f = Convert.ToDouble(txtValorEncargos.Text);
                txtValorEncargos.Text = string.Format("{0:n2}", f);
            }

           
        }

        private void txtValorCustoFinal_Enter(object sender, EventArgs e)
        {
            txtValorCustoInicial.Text = txtValorCustoInicial.Text != string.Empty ? txtValorCustoInicial.Text : "0,00";
            txtValorFrete.Text = txtValorFrete.Text != string.Empty ? txtValorFrete.Text : "0,00";
            txtValorEncargos.Text = txtValorEncargos.Text != string.Empty ? txtValorEncargos.Text : "0,00";
            
            txtValorCustoFinal.Text = (Convert.ToDecimal(txtValorCustoInicial.Text) +
                                       Convert.ToDecimal(txtValorFrete.Text) +
                                       Convert.ToDecimal(txtValorEncargos.Text)).ToString();

            
            //txtValorCustoFinal.Text = (Convert.ToDecimal(txtValorCustoInicial.Text) +
            //                           Convert.ToDecimal(txtValorFrete.Text) +
            //                           Convert.ToDecimal(txtValorEncargos.Text)).ToString();

            double f = Convert.ToDouble(txtValorCustoFinal.Text);
            txtValorCustoFinal.Text = string.Format("{0:n2}", f);
        }

        private void txtPorcMargemLucro_Leave(object sender, EventArgs e)
        {
            if (txtPorcMargemLucro.Text == string.Empty)
                txtPorcMargemLucro.Text = "0,00";

                txtValorMargemLucro.Text = ((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcMargemLucro.Text)) / 100).ToString();
                Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                txtValorMargemLucro.Text = string.Format("{0:n2}", f);
            
        }

        private void txtValorVenda1_Enter(object sender, EventArgs e)
        {
            txtValorVenda1.Text = (Convert.ToDecimal(txtValorCustoFinal.Text) + Convert.ToDecimal(txtValorMargemLucro.Text)).ToString();
            Double f = Convert.ToDouble(txtValorVenda1.Text);
            txtValorVenda1.Text = string.Format("{0:n2}", f);
        }

        private void txtPorcValorVenda2_Leave(object sender, EventArgs e)
        {
            if (txtPorcValorVenda2.Text == string.Empty)
                txtPorcValorVenda2.Text = "0,00";

            txtValorVenda2.Text = (((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcValorVenda2.Text)) / 100) + Convert.ToDecimal(txtValorCustoFinal.Text)).ToString();
            Double f = Convert.ToDouble(txtValorVenda2.Text);
            txtValorVenda2.Text = string.Format("{0:n2}", f);            
        }

        private void txtValorCustoInicial_Leave(object sender, EventArgs e)
        {
            if (txtValorCustoInicial.Text == string.Empty)
                txtValorCustoInicial.Text = "0,00";
        }

        private void txtValorFrete_Leave(object sender, EventArgs e)
        {
            if (txtValorFrete.Text == string.Empty)
                txtValorFrete.Text = "0,00";
        }

        private void txtValorEncargos_Leave(object sender, EventArgs e)
        {
            if (txtValorEncargos.Text == string.Empty)
                txtValorEncargos.Text = "0,00";
        }

        private void txtValorCustoFinal_Leave(object sender, EventArgs e)
        {
            if (txtValorCustoFinal.Text == string.Empty)
                txtValorCustoFinal.Text = "0,00";
        }

        private void txtValorMargemLucro_Leave(object sender, EventArgs e)
        {
            if (txtValorMargemLucro.Text == string.Empty)
                txtValorMargemLucro.Text = "0,00";
        }

        private void txtValorVenda1_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda1.Text == string.Empty)
                txtValorVenda1.Text = "0,00";
        }

        private void txtValorVenda2_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda2.Text == string.Empty)
                txtValorVenda2.Text = "0,00";
        }

        private void txtPorcValorVenda3_Leave(object sender, EventArgs e)
        {
            if (txtPorcValorVenda3.Text == string.Empty)
                txtPorcValorVenda3.Text = "0,00";

            txtValorVenda3.Text = (((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcValorVenda3.Text)) / 100) + Convert.ToDecimal(txtValorCustoFinal.Text)).ToString();
            Double f = Convert.ToDouble(txtValorVenda3.Text);
            txtValorVenda3.Text = string.Format("{0:n2}", f);            

        }

        private void txtValorVenda3_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda3.Text == string.Empty)
                txtValorVenda3.Text = "0,00";
        }

        private void txtPorComissao_Leave(object sender, EventArgs e)
        {
            if (txtPorComissao.Text == string.Empty)
                txtPorComissao.Text = "0,00";
        }

        private void txtPorcIPI_Leave(object sender, EventArgs e)
        {
            if (txtPorcIPI.Text == string.Empty)
                txtPorcIPI.Text = "0,00";
        }

        private void txtPorcICMS_Leave(object sender, EventArgs e)
        {
            if (txtPorcICMS.Text == string.Empty)
                txtPorcICMS.Text = "0,00";

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDMATERIAL == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlMaterial.SelectTab(2);
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
                        MATERIALP.Delete(_IDMATERIAL);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        button4_Click(null, null);
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

        private void linkExcluirFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDFOTOMATERIAL == -1)
            {
                if (txtNomeFoto.Text != string.Empty)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        _FOTO = null;
                        txtNomeFoto.Text = string.Empty;
                        pictureBox1.Image = null;
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                       FOTOMATERIALP.Delete(_IDFOTOMATERIAL);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity2 = null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }


        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (txtPrecoFornCotacao.Text == string.Empty)
                txtPrecoFornCotacao.Text = "0,00";
        }

        private void txtPrecoFornCotacao_Validating(object sender, CancelEventArgs e)
        {
            if (txtPrecoFornCotacao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPrecoFornCotacao.Text))
                {
                    errorProvider1.SetError(txtPrecoFornCotacao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPrecoFornCotacao.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtPrecoFornCotacao.Text);
                    txtPrecoFornCotacao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPrecoFornCotacao, "");
                }
            }
        }

        private void cbFornecedCotacao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbFornecedCotacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnAddCotacaoFornec_Click(object sender, EventArgs e)
        {
            if (cbFornecedCotacao.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFornecedCotacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");       
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtPrecoFornCotacao.Text))
            {
                errorProvider1.SetError(txtPrecoFornCotacao, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else if (_IDMATERIAL == -1)
                MessageBox.Show("Antes de adicionar cotação é necessário gravar a material!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

            else
            {
                errorProvider1.SetError(cbFornecedCotacao, "");
                SalvaCotacaoFornecedor();
                GetProdutoFornecedorCotacao(_IDMATERIAL);
                Entity3 = null;
            }  
        }

        public void SalvaCotacaoFornecedor()
        {
            try
            {
                MATCOTACAOFORNECEDORP.Save(Entity3);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
      
        }

        public void GetProdutoFornecedorCotacao(int IDMATERIAL)
        {
            RowsFiltroCollection RowsFiltroProdutoFornecedorCotacao = new RowsFiltroCollection();
            RowsFiltroProdutoFornecedorCotacao.Add(new RowsFiltro("IDMATERIAL", "System.Int32", "=", IDMATERIAL.ToString()));


            Lis_MATCOTACAOFORNECEDORColl = LIS_MATCOTACAOFORNECEDORP.ReadCollectionByParameter(RowsFiltroProdutoFornecedorCotacao, "DATACOTACAO DESC");
            dataGridFornCotacao.AutoGenerateColumns = false;
            dataGridFornCotacao.DataSource = Lis_MATCOTACAOFORNECEDORColl; 
            
        }

        int _IDMATCOTACAOFORNECEDOR = -1;
        public MATCOTACAOFORNECEDOREntity Entity3
        {
            get
            {
                int IDFORNECEDOR = Convert.ToInt32(cbFornecedCotacao.SelectedValue.ToString());
                string TELEFONEFORNECEDOR = txtTelFornecCotacao.Text;
                string PRAZOENTREGA = txtPrazoForneCotacao.Text;
                string CONTATOFORNECEDOR = txtContatoFornCotacao.Text;
                decimal VALORCOMPRA = Convert.ToDecimal(txtPrecoFornCotacao.Text);
                string CONDPAGTO = txtCondFornCotacao.Text;
                string DATACOTACAO = DateTime.Now.ToString("dd/MM/yyyy");

                return new MATCOTACAOFORNECEDOREntity(_IDMATCOTACAOFORNECEDOR, _IDMATERIAL, IDFORNECEDOR, 
                                                          TELEFONEFORNECEDOR, PRAZOENTREGA, CONTATOFORNECEDOR,
                                                          VALORCOMPRA, CONDPAGTO, Convert.ToDateTime(DATACOTACAO));
            }
            set
            {

                if (value != null)
                {
                    _IDMATCOTACAOFORNECEDOR = value.IDMATCOTACAOFORNECEDOR;
                    cbFornecedCotacao.SelectedValue = value.IDFORNECEDOR;
                    txtTelFornecCotacao.Text = value.TELEFONEFORNECEDOR;
                    txtPrazoForneCotacao.Text = value.PRAZOENTREGA;
                    txtContatoFornCotacao.Text = value.CONTATOFORNECEDOR;

                    txtPrecoFornCotacao.Text = value.VALORCOMPRA.ToString();
                    Double f = Convert.ToDouble(txtPrecoFornCotacao.Text);
                    txtPrecoFornCotacao.Text = string.Format("{0:n2}", f);

                    txtCondFornCotacao.Text = value.CONDPAGTO;
                    
                    errorProvider1.SetError(txtNome, "");
                }
                else
                {
                   _IDMATCOTACAOFORNECEDOR = -1;
                    cbFornecedCotacao.SelectedIndex = 0;
                    txtTelFornecCotacao.Text = string.Empty;
                    txtPrazoForneCotacao.Text = string.Empty;
                    txtContatoFornCotacao.Text = string.Empty;
                    txtPrecoFornCotacao.Text = "0,00";
                    txtCondFornCotacao.Text = string.Empty;                    
                    errorProvider1.SetError(txtNome, "");
                }
            }
        }
       
        private void cbFornecedCotacao_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbFornecedCotacao.SelectedIndex > 0)
            {
                FORNECEDOREntity FornecedorCotaTy = new FORNECEDOREntity();
                FornecedorCotaTy = FORNECEDORP.Read(Convert.ToInt32(cbFornecedCotacao.SelectedValue.ToString()));

                txtTelFornecCotacao.Text =  FornecedorCotaTy.TELEFONE1;
                txtContatoFornCotacao.Text = FornecedorCotaTy.CONTATOTRANPORTADORA;
            }
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_MATERIALColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_MATERIALEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MATERIALEntity>(orderBy);

                    LIS_MATERIALColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_MATERIALColl;
                }
            }
        }

        private void dataGridFornCotacao_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Lis_MATCOTACAOFORNECEDORColl.Count > 0)
            {
                string orderBy = dataGridFornCotacao.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_MATCOTACAOFORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MATCOTACAOFORNECEDOREntity>(orderBy);

                    Lis_MATCOTACAOFORNECEDORColl.Sort(comparer.Comparer);

                    dataGridFornCotacao.DataSource = null;
                    dataGridFornCotacao.DataSource = Lis_MATCOTACAOFORNECEDORColl;
                }
            }
        }     
            
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Entity3 = null;
        }

        private void dataGridFornCotacao_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if ( Lis_MATCOTACAOFORNECEDORColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(Lis_MATCOTACAOFORNECEDORColl[rowindex].IDMATCOTACAOFORNECEDOR);
                    Entity3 = MATCOTACAOFORNECEDORP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(Lis_MATCOTACAOFORNECEDORColl[rowindex].IDMATCOTACAOFORNECEDOR);
                            MATCOTACAOFORNECEDORP.Delete(CodSelect);
                            GetProdutoFornecedorCotacao(_IDMATERIAL);
                            Entity3 = null;
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

        private void txtCodProdComposicao_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código do produto, digite um valor e pressione Ctrl+E para pesquisar.";
        }

        private void cbFornComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbFornComposicao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUnidadeComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUnidadeComposicao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPesoProduto_Validating(object sender, CancelEventArgs e)
        {
            if (txtPesoProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPesoProduto.Text))
                {
                    errorProvider1.SetError(txtPesoProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPesoProduto.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtPesoProduto.Text);
                    txtPesoProduto.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtPesoProduto, "");
                }
            }
        }

        private void txtPesoProduto_Leave(object sender, EventArgs e)
        {
            if (txtPesoProduto.Text == string.Empty)
                txtPesoProduto.Text = "0,000";
        }

        private void txtQuantMinima_Leave(object sender, EventArgs e)
        {
            if (txtQuantMinima.Text == string.Empty)
                txtQuantMinima.Text = "0,000";
        }

        private void txtEstoqueAtual_Leave(object sender, EventArgs e)
        {
            if (txtEstoqueAtual.Text == string.Empty)
                txtEstoqueAtual.Text = "0,000";
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
                e.Graphics.DrawString("Estoque", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 300, 170);
                e.Graphics.DrawString("Valor Venda 1", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 170);
                e.Graphics.DrawString("Custo Final", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("Peso", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 730, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_MATERIALColl.Count;

                while (IndexRegistro < LIS_MATERIALColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_MATERIALColl[IndexRegistro].IDMATERIAL.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MATERIALColl[IndexRegistro].NOMEMATERIAL, 32), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MATERIALColl[IndexRegistro].ESTOQUEATUAL.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 300, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MATERIALColl[IndexRegistro].VALORVENDA1.ToString(), 12), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 450, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_MATERIALColl[IndexRegistro].VALORCUSTOFINAL.ToString(), 27), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550, config.PosicaoDaLinha);
                    e.Graphics.DrawString(LIS_MATERIALColl[IndexRegistro].PESO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 730, config.PosicaoDaLinha);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_MATERIALColl.Count)
                    e.HasMorePages = true;
                else
                {
                    if (LIS_MATERIALColl.Count > 0)
                    {
                        e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                        e.Graphics.DrawString("Total da pesquisa: " + LIS_MATERIALColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    }

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

        Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Material");
            ////define o titulo do relatorio
           
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

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }
       
        public void button3_Click(object sender, EventArgs e)
        {
            LIS_MATERIALColl.Clear();
            Filtro.Clear();
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_MATERIALColl.Count.ToString();
        }

        private void FrmProduto_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblobsField.Text = string.Empty;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_MATERIALColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_MATERIALColl[indice].IDMATERIAL);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = MATERIALP.Read(CodigoSelect);

                    int CodFotoProduto = GetFotoProduto(Entity.IDMATERIAL);
                    if (CodFotoProduto != -1)
                        Entity2 = FOTOMATERIALP.Read(Convert.ToInt32(CodFotoProduto));
                    else
                    {
                        Entity2 = null;
                        pictureBox1.Image = null;
                        txtNomeFoto.Text = string.Empty;
                    }

                    tabControlMaterial.SelectTab(0);
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
                            MATERIALP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            button4_Click(null, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }
       

        private void btnCadClass_Click(object sender, EventArgs e)
        {
            using (FrmClassFiscal frm = new FrmClassFiscal())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbClassFiscal.SelectedValue);
                GetDropClassFiscal();

                cbClassFiscal.SelectedValue = CodSelec;
            }
        }

        private void btnCadSitTributaria_Click(object sender, EventArgs e)
        {
            using (FrmCST frm = new FrmCST())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbSitTributaria.SelectedValue);
                GetDropSitTrib();

                cbSitTributaria.SelectedValue = CodSelec;
            }
        }
                
    }
}