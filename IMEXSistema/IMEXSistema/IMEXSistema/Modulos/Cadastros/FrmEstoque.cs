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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using VVX;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Modulos.Relatorio;
using System.Xml;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmEstoque : Form
    {
        Utility Util = new Utility();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        LIS_ESTOQUEESProvider LIS_ESTOQUEESP = new LIS_ESTOQUEESProvider();
        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();
        CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CFOPProvider CFOPP = new CFOPProvider();
        ESTOQUELOTEProvider ESTOQUELOTEP = new ESTOQUELOTEProvider();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        PRODUTOSCollection ProdutoColl = new PRODUTOSCollection();
        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        LIS_ESTOQUEESCollection LIS_ESTOQUEESColl = new LIS_ESTOQUEESCollection();
        DUPLICATAPAGARCollection DUPLICATAPAGARColl = new DUPLICATAPAGARCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();
        CFOPCollection CFOPColl = new CFOPCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Boolean ImportarXMLNfe = false;

        public FrmEstoque()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        public int _IDESTOQUEES = -1;
        public ESTOQUEESEntity Entity
        {
            get
            {
                int IDTIPOMOVIMENTACAO = Convert.ToInt32(cbTipoMov.SelectedValue);
                DateTime DATAMOVIM = Convert.ToDateTime(maskedtxtData.Text);
                int IDCODMOVIMENTACAO =  1;
                string NDOCUMENTO = txtNDocumento.Text;
                int IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);
                decimal TOTALMOVIMENTACAO = Convert.ToDecimal(txtTotalGeral.Text);
                decimal VALORIPI = Convert.ToDecimal(txtValoIPI.Text);
                decimal VALORICMS = Convert.ToDecimal(txtValorICMS.Text);
                decimal VALORFRETE = Convert.ToDecimal(txtValorFrete.Text);
                int? IDCLIENTE = null;

                int IDCFOP = Convert.ToInt32( cbCFOP.SelectedValue);
                string MODELONF = txtModeloNF.Text;
                string SERIENF = txtSerieNF.Text;

                decimal VALORBASEICMS = Convert.ToDecimal(txtBaseCalcICMS.Text);
                string FLAGSINTEGRA = chkSintegra.Checked ? "S" : "N";
                string FLAGENERGIATELECOM = chkEnergTelecom.Checked ? "S" : "N";
                string CHAVEACESSO = Util.RetiraLetras(txtChaveAcesso.Text);

                string CNPJEMISSOR = "";
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);
                CNPJEMISSOR = EMPRESATy.CNPJCPF.Trim();

                return new ESTOQUEESEntity(_IDESTOQUEES, IDTIPOMOVIMENTACAO, DATAMOVIM, IDCODMOVIMENTACAO,
                                           NDOCUMENTO, IDFORNECEDOR, TOTALMOVIMENTACAO, VALORIPI,
                                           VALORICMS, VALORFRETE, IDCLIENTE, IDCFOP, MODELONF, SERIENF,
                                           VALORBASEICMS, FLAGSINTEGRA, FLAGENERGIATELECOM, CHAVEACESSO,
                                           CNPJEMISSOR); 
            }
            set
            {

                if (value != null)
                {
                    _IDESTOQUEES = value.IDESTOQUEES;
                    maskedtxtData.Text = Convert.ToDateTime(value.DATAMOVIM).ToString("dd/MM/yyyy");
                   
                    txtNDocumento.Text = value.NDOCUMENTO;
                    if (value.IDFORNECEDOR != null)
                    {
                        cbFornecedor.SelectedValue = value.IDFORNECEDOR;
                        GridDuplicatasFornecedor(Convert.ToInt32(value.IDFORNECEDOR), value.NDOCUMENTO);
                    }

                    //Formatando o valor geral
                    txtTotalGeral.Text = value.TOTALMOVIMENTACAO.ToString();
                    Double f = Convert.ToDouble(txtTotalGeral.Text);
                    txtTotalGeral.Text = string.Format("{0:n2}", f);

                    txtTotalFinanceiro.Text = txtTotalGeral.Text;

                    txtValoIPI.Text = value.VALORIPI.ToString(); ;

                    txtValorICMS.Text = value.VALORICMS.ToString();
                    txtValorFrete.Text = value.VALORFRETE.ToString();
                   
                    if (value.IDCFOP != null)
                        cbCFOP.SelectedValue = value.IDCFOP;

                    txtModeloNF.Text = value.MODELONF;
                    txtSerieNF.Text = value.SERIENF;

                    txtBaseCalcICMS.Text = Convert.ToDecimal(value.VALORBASEICMS).ToString("n2");

                    chkSintegra.Checked = value.FLAGSINTEGRA.TrimEnd() == "S" ? true : false;
                    chkEnergTelecom.Checked = value.FLAGENERGIATELECOM.TrimEnd() == "S" ? true : false;

                    txtChaveAcesso.Text = value.CHAVEACESSO;
                   

                    errorProvider1.Clear();
                }
                else
                {
                    _IDESTOQUEES = -1;
                    GetGridProdutoMov(-1);
                    GridDuplicatasFornecedor(-1, "");
                    maskedtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                  
                    txtNDocumento.Text = string.Empty;
                    cbFornecedor.SelectedIndex = 0;
                    txtTotalGeral.Text ="0,00";
                    txtValoIPI.Text ="0,00";
                    txtValorICMS.Text ="0,00";
                    txtValorFrete.Text = "0,00";
                    txtTotalProdutos.Text = "0,00";
                    txtTotaItens.Text = "0";
                    cbFornecedor.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;
                    txtNParcelas.Text = "0";
                    txtVlParcelas.Text = "0,00";
                    TxtDias.Text = "0";
                    txtBaseCalcICMS.Text = "0,00";
                    mskDataVecto.Text = "  /  /";
                    txtModeloNF.Text = string.Empty;
                    txtSerieNF.Text =  string.Empty;
                    chkSintegra.Checked = false;
                    chkEnergTelecom.Checked = false;
                    txtChaveAcesso.Text = string.Empty;

                    errorProvider1.Clear();
                }
            }
        }

        int _IDMOVPRODUTOES = -1;
        public MOVPRODUTOESEntity Entity2
        {
            get
            {
                
              int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);//         INTEGER,
              decimal  QUANTIDADE = Convert.ToDecimal(txtQuant.Text);//      NUMERIC(15,3),

              if (txtCustoUnitario.Text == string.Empty)
                  txtCustoUnitario.Text = "0,00";

              decimal  VALORCUNITARIO   = Convert.ToDecimal(txtCustoUnitario.Text);//    NUMERIC(15,2),
              decimal VALORTOTAL = Convert.ToDecimal((QUANTIDADE * VALORCUNITARIO).ToString("n2")); //  NUMERIC(15,2),
              string  FLAGATUALIZACUSTO  = ckAtCusto.Checked == true ? "S" : "N";
              decimal ALIQICMS = Convert.ToDecimal(txtAliqICMSProd.Text);//    NUMERIC(15,2),
              int IDCFOP = Convert.ToInt32(cbCFOP.SelectedValue);// 
 
               decimal BASEICMS  = Convert.ToDecimal(txtBaseICMSProd.Text);
               decimal VLICMS = Convert.ToDecimal(txtVLICMSProd.Text);
               string CST_CSOSN = txtcstcsosn.Text;
               decimal VLIPI = Convert.ToDecimal(txtVLIPI.Text);
               decimal VLFRETE = Convert.ToDecimal(txtVlFreteItem.Text);
               decimal VLBASEICMSST = Convert.ToDecimal(txtBaseICMSST.Text);
               decimal VlICMSST = Convert.ToDecimal(txtVlICMSST.Text);
               decimal VLDESCONTOPRODUTO = Convert.ToDecimal(txtVlDescontoProduto.Text);                
                    
               return new MOVPRODUTOESEntity(_IDMOVPRODUTOES, _IDESTOQUEES, IDPRODUTO,
                                             QUANTIDADE, VALORCUNITARIO , VALORTOTAL , FLAGATUALIZACUSTO,
                                             0, ALIQICMS, IDCFOP, BASEICMS, VLICMS, CST_CSOSN, VLIPI,
                                             VLFRETE, VLBASEICMSST, VlICMSST, VLDESCONTOPRODUTO);
            }
            set
            {

                if (value != null)
                {

                    _IDMOVPRODUTOES = value.IDMOVPRODUTOES;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuant.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n3");
                    txtCustoUnitario.Text = Convert.ToDecimal(value.VALORCUNITARIO).ToString("n4"); 
                    ckAtCusto.Checked =  value.FLAGATUALIZACUSTO.TrimEnd() == "S" ? true : false;
                    txtAliqICMSProd.Text = Convert.ToDecimal(value.ALQICMS).ToString("n2");

                    txtBaseICMSProd.Text = Convert.ToDecimal(value.BASEICMS).ToString("n3");
                    txtVLICMSProd.Text = Convert.ToDecimal(value.VLICMS).ToString("n3");

                    txtcstcsosn.Text = value.CST_CSOSN;
                    txtVLIPI.Text = Convert.ToDecimal(value.VLIPI).ToString("n2");

                    if (value.IDCFOP != null)
                        cbCFOP.SelectedValue = value.IDCFOP;

                    txtVlFreteItem.Text = Convert.ToDecimal(value.VLFRETE).ToString("n2");
                    txtBaseICMSST.Text = Convert.ToDecimal(value.VLBASEICMSST).ToString("n2");
                    txtVlICMSST.Text = Convert.ToDecimal(value.VLICMSST).ToString("n2");
                    txtVlDescontoProduto.Text = Convert.ToDecimal(value.VLDESCONTOPRODUTO).ToString("n2");

                    //Bloqueia Estoque lote para nao salvar duas vezes
                    txtCodLote.Enabled = false;
                }
                else
                {
                    _IDMOVPRODUTOES = -1;
                     cbProduto.SelectedValue = -1;
                     txtEstoque.Text = "0";
                     txtQuant.Text = "1";
                     txtCustoUnitario.Text  = "0,0000";
                     txtBaseICMSProd.Text = "0,00";
                     txtVLICMSProd.Text = "0,00";
                     txtcstcsosn.Text = string.Empty;
                     txtVLIPI.Text = "0,00";
                     txtVlFreteItem.Text = "0,00";
                     txtBaseICMSST.Text = "0,00";
                     txtVlICMSST.Text = "0,00";
                     txtVlDescontoProduto.Text = "0,00";
                    txtCodLote.Text = string.Empty;

                    txtCodLote.Enabled = true;
                    errorProvider1.Clear();
                }
            }
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

        private void FrmEstoque_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();
            PreencheDropTipoMovimento();
           
            GetDropProduto();
            GetDropFornecedor();
            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();
            GetDropCentroCusto();
            GetDropCFOP();
            GetDropTipoDuplicata();
           
            btnCadProduto.Image = Util.GetAddressImage(6);
            btnCadFornecedor.Image = Util.GetAddressImage(6);
            btnLote.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            btnCadCOP.Image = Util.GetAddressImage(6);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22); 

            if(_IDESTOQUEES != -1)
            {
                Entity = ESTOQUEESP.Read(_IDESTOQUEES);
                GetGridProdutoMov(_IDESTOQUEES);
            }
            else
                Entity = null;


            this.Cursor = Cursors.Default;
        }

        private void PreencheDropTipoPesquisa()
        {
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
            cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }     


        private void PreencheDropTipoMovimento()
        {
            TIPOMOVESTOQUEProvider TIPOMOVESTOQUEP = new TIPOMOVESTOQUEProvider();

            cbTipoMov.DataSource = TIPOMOVESTOQUEP.ReadCollectionByParameter(null, "NOME");
            cbTipoMov.DisplayMember = "NOME";
            cbTipoMov.ValueMember = "IDTIPOMOVESTOQUE";          
        }      

        private void GetDropProduto()
        {
            try
            {
                ProdutoColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

                cbProduto.DisplayMember = "NOMEPRODUTO";
                cbProduto.ValueMember = "IDPRODUTO";

                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                ProdutoColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

                ProdutoColl.Sort(comparer.Comparer);
                cbProduto.DataSource = ProdutoColl;

                cbProduto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropFornecedor()
        {
            try
            {
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

                cbFornecedor.DisplayMember = "NOME";
                cbFornecedor.ValueMember = "IDFORNECEDOR";

                FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
                FORNECEDORTy.IDFORNECEDOR = -1;
                FORNECEDORColl.Add(FORNECEDORTy);

                Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedor.DisplayMember);

                FORNECEDORColl.Sort(comparer.Comparer);
                cbFornecedor.DataSource = FORNECEDORColl;

                cbFornecedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);
            btnAdd2.Image = Util.GetAddressImage(15);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);            
        }

        private void FrmEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedtxtData_Leave(object sender, EventArgs e)
        {
            if (maskedtxtData.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(maskedtxtData, "");
                }
            }
            else
            {
                errorProvider1.SetError(maskedtxtData, "");
            }
        }

        private void bntCadMovim_Click(object sender, EventArgs e)
        {
            using (FrmCodMovEstoque frm = new FrmCodMovEstoque())
            {
                frm.ShowDialog();
            }
        }

        private void cbCodMov_Enter(object sender, EventArgs e)
        {
           
        }

        private void cbTipoMov_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cbTipoMov_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbCodMov_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbCodMov_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cbFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
           if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbFornecedor.SelectedValue = result;
                } 
            }

        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                  PesquisaProduto(cbProduto.Text);
            }

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
                        //txtValorUnitProd.Text = frm.ResultPreco.ToString("n2");
                    }
                }
            }

        }


        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
        private void PesquisaProduto(string IDPRODUTO)
        {

            PRODUTOSTy = null;
            PRODUTOSTy = PesquisaCodBarra(cbProduto.Text);

            if (PRODUTOSTy == null)
                PRODUTOSTy = PesquisaCodReferencia(cbProduto.Text);
            else if (PRODUTOSTy == null)
            {
                if (ValidacoesLibrary.ValidaTipoInt32(cbProduto.Text))
                {
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(IDPRODUTO));
                    if (PRODUTOSTy != null)
                        cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                }
            }

            if (PRODUTOSTy == null)
            {
                cbProduto.Focus();
                DialogResult dr = MessageBox.Show("Código inválido, deseja efetuar a pesquisa?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    using (FrmSearchProduto frm = new FrmSearchProduto())
                    {
                        frm.ShowDialog();
                        var result = frm.Result;

                        if (result > 0)
                        {
                            PRODUTOSTy = PRODUTOSP.Read(result);
                            cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                            txtEstoque.Text = Util.EstoqueAtual(Convert.ToInt32(PRODUTOSTy.IDPRODUTO), false).ToString("n2");
                            txtCustoUnitario.Text = Convert.ToDecimal(PRODUTOSTy.VALORCUSTOINICIAL).ToString("n2");
                            txtCustoUnitario.Focus();
                        }

                    }

                }
            }
            else
            {
                PRODUTOSTy = PRODUTOSP.Read(PRODUTOSTy.IDPRODUTO);
                cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                txtEstoque.Text = Util.EstoqueAtual(Convert.ToInt32(PRODUTOSTy.IDPRODUTO), false).ToString("n2");
                txtCustoUnitario.Text = Convert.ToDecimal(PRODUTOSTy.VALORCUSTOINICIAL).ToString("n2");
                txtCustoUnitario.Focus();
            }
        }


        private PRODUTOSEntity PesquisaCodBarra(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODBARRA", "System.String", "=", Pesquisa));

                PRODUTOSCollection PRODUTOSPESCODBARRACOLL = new PRODUTOSCollection();
                PRODUTOSPESCODBARRACOLL = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOSPESCODBARRACOLL.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOSPESCODBARRACOLL[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return PRODUTOSPESBARRATY;

        }

        private PRODUTOSEntity PesquisaCodReferencia(string Pesquisa)
        {
            PRODUTOSEntity PRODUTOSPESBARRATY = new PRODUTOSEntity();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", Pesquisa));

                PRODUTOSCollection PRODUTOSPESCODBARRACOLL = new PRODUTOSCollection();
                PRODUTOSPESCODBARRACOLL = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (PRODUTOSPESCODBARRACOLL.Count > 0)
                    PRODUTOSPESBARRATY = PRODUTOSP.Read(PRODUTOSPESCODBARRACOLL[0].IDPRODUTO);
                else
                    PRODUTOSPESBARRATY = null;

                return PRODUTOSPESBARRATY;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return PRODUTOSPESBARRATY;
            }
            

        }

        private void getProduto(int IdProduto)
        {
            try
            {
                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSTy = PRODUTOSP.Read(IdProduto);
                if (PRODUTOSTy != null)
                {
                    txtEstoque.Text = Util.EstoqueAtual(IdProduto, false).ToString();
                    txtCustoUnitario.Text = PRODUTOSTy.VALORCUSTOINICIAL.ToString();
                    cbProduto.SelectedValue = PRODUTOSTy.IDPRODUTO;
                    if (txtCustoUnitario.Text != string.Empty)
                    {
                        Double f = Convert.ToDouble(txtCustoUnitario.Text);
                        txtCustoUnitario.Text = string.Format("{0:n2}", f);

                        ckAtCusto.Text = "Atualizar Custo Unitário do Produto: " + PRODUTOSTy.NOMEPRODUTO;
                    }

                    txtCustoUnitario.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não localizado!");
                    using (FrmSearchProduto frm = new FrmSearchProduto())
                    {
                        frm.ShowDialog();
                        var result = frm.Result;

                        if (result > 0)
                        {
                            cbProduto.SelectedValue = result;
                            getProduto(result);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar produto!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int Codproduto = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                 frm.ShowDialog();
                 GetDropProduto();
                cbProduto.SelectedValue = Codproduto;
            }
        }

        private void cbFornecedor_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";         
        }

        private void cbProduto_Enter(object sender, EventArgs e)
        {
           // GetDropProduto();
            lblObsField.Text = "Selecione o produto ou pressione Ctrl+E para pesquisar.";
        }

        private void txtEstoqueAtual_Validating(object sender, CancelEventArgs e)
        {
            if (txtQuant.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
                {
                    errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtQuant.Text);
                    txtQuant.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtQuant, "");
                }
            }
            else
                txtQuant.Text = "0,000";
        }

        private void FrmEstoque_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void txtCustoUnitario_Validating(object sender, CancelEventArgs e)
        {
            if (txtCustoUnitario.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtCustoUnitario.Text))
                {
                    errorProvider1.SetError(txtCustoUnitario, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtCustoUnitario.Text);
                    txtCustoUnitario.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(txtCustoUnitario, "");
                }
            }
            else
            {
                txtCustoUnitario.Text = "0,0000";
                errorProvider1.SetError(txtCustoUnitario, "");
                
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            using (FrmFornecedor frm = new FrmFornecedor())
            {
                int CodSelec = Convert.ToInt32(cbFornecedor.SelectedValue);
                frm._IDFORNECEDOR = CodSelec;
                frm.ShowDialog();
                GetDropFornecedor();
                cbFornecedor.SelectedValue = CodSelec;
            }
        }

        private void cbProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
                     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes() && ValidaProdutoColl())
            {
                _IDESTOQUEES = ESTOQUEESP.Save(Entity); 

                try
                {
                    int _IDPRODUTOSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    //Grava itens de serviços
                    MOVPRODUTOESP.Save(Entity2);

                    //Atualiza os custos do produto
                    if (ckAtCusto.Checked)
                    {
                        PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                        PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                        PRODUTOSTy.VALORCUSTOINICIAL = Convert.ToDecimal(txtCustoUnitario.Text);

                        if (PRODUTOSTy.PORCFRETE > 0)
                            PRODUTOSTy.FRETEPRODUTO = ((PRODUTOSTy.VALORCUSTOINICIAL * PRODUTOSTy.PORCFRETE) / 100);

                        if (PRODUTOSTy.PORCENCARGOS > 0)
                            PRODUTOSTy.ENCARGOSPRODUTOS = ((PRODUTOSTy.VALORCUSTOINICIAL * PRODUTOSTy.PORCENCARGOS) / 100);

                        PRODUTOSTy.VALORCUSTOFINAL = PRODUTOSTy.VALORCUSTOINICIAL + PRODUTOSTy.FRETEPRODUTO + PRODUTOSTy.ENCARGOSPRODUTOS;
                    
                        PRODUTOSTy.MARGEMLUCRO = (PRODUTOSTy.VALORCUSTOFINAL * PRODUTOSTy.PORCMARGEMLUCRO) / 100;
                        PRODUTOSTy.VALORVENDA1 = ((PRODUTOSTy.VALORCUSTOFINAL * PRODUTOSTy.PORCMARGEMLUCRO) / 100) + PRODUTOSTy.VALORCUSTOFINAL;
                        
                        if (Convert.ToDecimal(PRODUTOSTy.PORCVENDA2) > 0)
                            PRODUTOSTy.VALORVENDA2 = ((PRODUTOSTy.VALORCUSTOFINAL * PRODUTOSTy.PORCVENDA2) / 100) + PRODUTOSTy.VALORCUSTOFINAL;

                        if (Convert.ToDecimal(PRODUTOSTy.PORCVENDA3) > 0)
                            PRODUTOSTy.VALORVENDA3 = ((PRODUTOSTy.VALORCUSTOFINAL * PRODUTOSTy.PORCVENDA3) / 100) + PRODUTOSTy.VALORCUSTOFINAL;
                        
                        PRODUTOSP.Save(PRODUTOSTy);
                    }

                    if (!ImportarXMLNfe)
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                    SaveLoteEstoque();
                    Entity2 = null;
                    cbProduto.Focus();
                    GetGridProdutoMov(_IDESTOQUEES);
                    
                    SumMovProduto();
                    ESTOQUEESP.Save(Entity);                   
                }
                catch (Exception ex)
                {
                    Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                    MessageBox.Show("Erro técnico : " + ex.Message);
                }
              
            }
        }

        int _IdLote = -1;
        private void SaveLoteEstoque()
        {
            try
            {
                if (_IdLote != -1 && _IDMOVPRODUTOES == -1)                
                {
                    //Salva ESTOQUELOTE
                    ESTOQUELOTEEntity ESTOQUELOTETy = new ESTOQUELOTEEntity();                        
                    ESTOQUELOTETy.IDESTOQUELOTE = -1;
                    ESTOQUELOTETy.QUANTIDADE = Convert.ToDecimal(txtQuant.Text);
                    ESTOQUELOTETy.IDLOTE = _IdLote;
                    ESTOQUELOTETy.IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                    ESTOQUELOTETy.NUMERODOC = txtNDocumento.Text;
                    ESTOQUELOTETy.DATA = Convert.ToDateTime(maskedtxtData.Text);
                    ESTOQUELOTETy.FLAGTIPO = "E";
                    ESTOQUELOTETy.FLAGATIVO = "S";
                    ESTOQUELOTETy.OBSERVACAO = "";
                    ESTOQUELOTEP.Save(ESTOQUELOTETy);
                    _IdLote = -1;
                    Util.ExibirMSg("Estoque Lote Salvo com Sucesso!", "Blue");                    
                }              
                
            }
            catch (Exception ex)
            {
                Util.ExibirMSg("Erro ao Salvar Estoque Lote!", "Red");
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        //Salva arquivo em texto
        private void ArquivoProdutoRetarguardaDigiSat(int IDPRODUTOSELEC, string CaminhoRecpDigiSat)
        {
            string arquivo = CaminhoRecpDigiSat + "E" + IDPRODUTOSELEC.ToString().PadLeft(8, '0') + ".txt";

            StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252));
            try
            {
                Application.DoEvents();
                this.Text = "Cadastro de Produto - Aguarde... Gerando arquivo do Produto: " + IDPRODUTOSELEC.ToString();

                LIS_PRODUTOSCollection LIS_PRODUTOSColl2 = new LIS_PRODUTOSCollection();
                PRODUTOSEntity PRODUTOSTy2 = new PRODUTOSEntity();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTOSELEC.ToString()));
                LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider(); 
                LIS_PRODUTOSColl2 = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                PRODUTOSTy2 = PRODUTOSP.Read(IDPRODUTOSELEC);

                escrever.WriteLine(IDPRODUTOSELEC.ToString().PadLeft(8, '0'));//1=Código do Produto ( 8 digitos, obedecer zeros a esquerda

                if (LIS_PRODUTOSColl2[0].CODBARRA.TrimEnd().TrimStart().ToUpper() != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CODBARRA);//2=Código de barras (NULL se não usar)
                else
                    escrever.WriteLine("NULL");//2=Código de barras (NULL se não usar)

                escrever.WriteLine("NULL");//3=padrão do código de barra(EAN13, EAN18...) (NULL se não usar)

                escrever.WriteLine("NULL");//4=Fonte Código de barras(Campo que ao utilizar a fonte DigBarra gera o código de barras) (NULL se não usar)

                escrever.WriteLine(LIS_PRODUTOSColl2[0].NOMEPRODUTO.ToUpper());//5=Nome do Produto

                string NOMEUNIDADE = LIS_PRODUTOSColl2[0].NOMEUNIDADE.ToUpper().TrimStart().TrimEnd();
                if (NOMEUNIDADE != string.Empty)
                    escrever.WriteLine(NOMEUNIDADE);//6=Unidade de Medida ( NULL se não usar)
                else
                    escrever.WriteLine("NULL");//6=Unidade de Medida ( NULL se não usar)

                escrever.WriteLine("NULO");//7= Fator de conversão (NULL se não usar)

                escrever.WriteLine("NULL");//8=Grupo do Produto (NULL se não usar)

                escrever.WriteLine("0");//9=Utilizar grade (1 para Sim ou 0 para Não)
                escrever.WriteLine("NULL");//10 = Coluna de Grade, separado por;(NULL se não usar)
                escrever.WriteLine("NULL");//11 = Linhas da Grade, separado por; (NULL se não usar)
                escrever.WriteLine("NULL");//12 = Grade com Qtde Ex.: [Coluna, Linha1 = Qtde][Coluna, Linha2=Qtde] (NULL se não usar)

                string ESTOQUEATUAL = Util.EstoqueAtual(IDPRODUTOSELEC, false).ToString("n2").Replace(".", "");
                escrever.WriteLine(ESTOQUEATUAL);//13 = Quantidade (0 se não usar)

                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].VALORCUSTOFINAL).ToString("n2").Replace(".", ""));//14 = Preço de Custo (0 se não usar)
                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].VALORVENDA1).ToString("n2").Replace(".", ""));//15 = Preço de Venda consumidor
                escrever.WriteLine("0");//16 = Utilizar Indexador de Preços (1 para Sim ou 0 para Não)
                escrever.WriteLine("NULL");//17 = nome do arquivo da foto do produto (NULL se não usar)


                
                if (LIS_PRODUTOSColl2[0].SITUACAOTRIBUTARIA == "T")
                    escrever.WriteLine("Normal");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
                else if (LIS_PRODUTOSColl2[0].SITUACAOTRIBUTARIA == "F")
                    escrever.WriteLine("Substituição Tributária");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
                else if (LIS_PRODUTOSColl2[0].SITUACAOTRIBUTARIA == "I")
                    escrever.WriteLine("Isenção");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
                else if (LIS_PRODUTOSColl2[0].SITUACAOTRIBUTARIA == "N")
                    escrever.WriteLine("Não Incidência");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")

                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].ICMS).ToString("n2"));//19 = Percentual do ICMs (Formato 99,99) 
                escrever.WriteLine("0");//20 = Usar controle de Numero de Serie ( 1 para Sim ou 0 para não )

                if (LIS_PRODUTOSColl2[0].CSTPISCONFIS != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CSTPISCONFIS);//21 = Sit. Trib. PIS
                else
                    escrever.WriteLine("99");//21 = Sit. Trib. PIS

                escrever.WriteLine("0,00");//22 = BC PIS
                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].ALIQPIS).ToString("n2"));//23 = % PIS
                escrever.WriteLine("0,00");//24 = BC PIS Subst. Trib.
                escrever.WriteLine("0,00");//25 = % PIS Subst. Trib.

                if (LIS_PRODUTOSColl2[0].CSTPISCONFIS != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CSTPISCONFIS);//26 = Sit. trib. Confins
                else
                    escrever.WriteLine("99");//26 = Sit. trib. Confins

                escrever.WriteLine("0,00");//27 = BC Confis
                escrever.WriteLine("0,00");//28 = % Confins
                escrever.WriteLine("0,00");//29 = BC Cofins Subt. Trib.
                escrever.WriteLine("0,00");//30 = % Confins Subst. Trib.
                escrever.WriteLine(LIS_PRODUTOSColl2[0].NCMSH.Replace(".", "").Replace(" ", ""));//31 = NCM

                // Situação Tributária
                if (PRODUTOSTy2.IDCST != null)
                {
                    LIS_CSTCollection LIS_CSTColl2 = new LIS_CSTCollection();
                    LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCST", "System.Int32", "=", PRODUTOSTy2.IDCST.ToString()));
                    LIS_CSTColl2 = LIS_CSTP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_CSTColl2.Count > 0)
                        escrever.WriteLine(LIS_CSTColl2[0].CODCOMPL);//32 = Situação Tributária
                    else
                        escrever.WriteLine("00");//32 = Situação Tributária
                }
                else escrever.WriteLine("00");//32 = Situação Tributária

                escrever.WriteLine("0");//33 = Tipo do Produto (0= Normal, 1=Componente)


                escrever.Close();

                Application.DoEvents();
                this.Text = "Cadastro de Produto";
            }
            catch (Exception ex)
            {
                escrever.Close();
                MessageBox.Show("Erro ao salvar arquivo DigiSat");
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }

        private void SomaValorICMS()
        {
            decimal TotalICMS = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                TotalICMS += Convert.ToDecimal(item.VLICMS);
            }

            txtValorICMS.Text = TotalICMS.ToString("n2");
        }

        private void SomaValorIPI()
        {
            decimal TotalIPI = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                TotalIPI += Convert.ToDecimal(item.VLIPI);
            }

            txtValoIPI.Text = TotalIPI.ToString("n2");
        }

        private void SomaValorVLICMSST()
        {
            decimal VLICMSST = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                VLICMSST += Convert.ToDecimal(item.VLICMSST);
            }

            txtVlTotalICMSST.Text = VLICMSST.ToString("n2");
        }

        private void SomaValorFrete()
        {
            decimal TotalFrete = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                TotalFrete += Convert.ToDecimal(item.VLFRETE);
            }

            txtValorFrete.Text = TotalFrete.ToString("n2");
        }

        private void SomaValorBASEICMS()
        {
            decimal TotalBASEICMS = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                TotalBASEICMS += Convert.ToDecimal(item.BASEICMS);
            }

            txtBaseCalcICMS.Text = TotalBASEICMS.ToString("n2");
        }

        private Boolean ValidaProdutoColl()
        {
            Boolean result = true;

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuant.Text))
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
            if (txtQuant.Text == "0,000")
            {
                errorProvider1.SetError(txtQuant, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else
                if (Convert.ToDecimal(txtQuant.Text) <= 0)
                {
                    errorProvider1.SetError(txtQuant, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    result = false;
                }
            else
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtCustoUnitario.Text))
            {
                errorProvider1.SetError(txtCustoUnitario, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if(Convert.ToInt32(cbProduto.SelectedValue) < 1) 
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (Convert.ToInt32(cbCFOP.SelectedValue) < 1)
            {
                errorProvider1.SetError(lblCFOP, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }       

        private void SumMovProduto()
        {
            decimal valorTotal = 0;
            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {
                valorTotal += Convert.ToDecimal(item.QUANTIDADE * item.VALORCUNITARIO);
                valorTotal -= Convert.ToDecimal(item.VLDESCONTOPRODUTO);
            }

            txtTotalProdutos.Text = valorTotal.ToString();
            Double f = Convert.ToDouble(txtTotalProdutos.Text);
            txtTotalProdutos.Text = string.Format("{0:n2}", f);


            //Soma de frete, IPI e ICMS ST
            valorTotal += Convert.ToDecimal(txtValorFrete.Text) + Convert.ToDecimal(txtValoIPI.Text) + Convert.ToDecimal(txtVlTotalICMSST.Text);
            txtTotalGeral.Text = valorTotal.ToString();
            f = Convert.ToDouble(txtTotalGeral.Text);
            txtTotalGeral.Text = string.Format("{0:n2}", f);

            txtTotalFinanceiro.Text = txtTotalGeral.Text;

            txtTotaItens.Text = LIS_MOVPRODUTOESColl.Count.ToString();           
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
                    _IDESTOQUEES = ESTOQUEESP.Save(Entity);
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
      

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(lblData, ConfigMessage.Default.FieldErro);
                Util.ExibirMSg(ConfigMessage.Default.FieldErro, "Red");
                result = false;
            }
            else if (txtNDocumento.Text == string.Empty && !ValidacoesLibrary.ValidaTipoInt32(txtNDocumento.Text))
            {
                errorProvider1.SetError(lblNumDocumento, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");                
                result = false;
            }
            else if (txtSerieNF.Text == string.Empty && chkSintegra.Checked)
            {
                errorProvider1.SetError(lblSerie, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtModeloNF.Text == string.Empty && chkSintegra.Checked)
            {
                errorProvider1.SetError(lblModeloNF, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtQuant.Text == string.Empty)
            {
                errorProvider1.SetError(lblQuant, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }                
            else if (Convert.ToInt32(cbFornecedor.SelectedValue) < 1)
            {
                errorProvider1.SetError(lblFornecedor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbCFOP.SelectedValue) < 1)
            {
                errorProvider1.SetError(lblCFOP, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if ((chkSintegra.Checked || chkEnergTelecom.Checked) && !ValidacoesLibrary.ValidaTipoInt32(txtNDocumento.Text))
            {
                string Msg = "Campo inválido!";
                errorProvider1.SetError(lblNumDocumento, Msg);
                Util.ExibirMSg(Msg, "Red");
                result = false;
            }
            else if (txtChaveAcesso.Text.Length > 0 && txtChaveAcesso.Text.Length < 44)
            {
                string Msg = "Na chave de aceso é necessário ter 44 caracteres!";
                errorProvider1.SetError(label37, Msg);
                Util.ExibirMSg(Msg, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
                Grava();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();
               
                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (chkFlagSintegraPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                
                LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(Filtro, "IDESTOQUEES DESC");
                

                //Colocando somatorio no final da lista
                LIS_ESTOQUEESEntity LIS_ESTOQUEESTy = new LIS_ESTOQUEESEntity();
                LIS_ESTOQUEESTy.TOTALMOVIMENTACAO = SumTotalPesquisa("TOTALMOVIMENTACAO");
                LIS_ESTOQUEESTy.VALORIPI = SumTotalPesquisa("VALORIPI");
                LIS_ESTOQUEESTy.VALORFRETE = SumTotalPesquisa("VALORFRETE");
                LIS_ESTOQUEESColl.Add(LIS_ESTOQUEESTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ESTOQUEESColl;

                lblTotalPesquisa.Text = LIS_ESTOQUEESColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_ESTOQUEESEntity item in LIS_ESTOQUEESColl)
            {
                if (NomeCampo == "TOTALMOVIMENTACAO")
                    valortotal += Convert.ToDecimal(item.TOTALMOVIMENTACAO);
                else if (NomeCampo == "VALORIPI")
                    valortotal += Convert.ToDecimal(item.VALORIPI);
                else if (NomeCampo == "VALORFRETE")
                    valortotal += Convert.ToDecimal(item.VALORFRETE);
            }

            return valortotal;
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
            try
            {
                // Nome campo que sera filtrado
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_ESTOQUEESColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ESTOQUEESColl;
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


                    if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                    {
                        filtroProfile = new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    if (chkFlagSintegraPesquisa.Checked)
                    {
                        filtroProfile = new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S");
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    
                    LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(Filtro, "IDESTOQUEES DESC");

                    //Colocando somatorio no final da lista
                    LIS_ESTOQUEESEntity LIS_ESTOQUEESTy = new LIS_ESTOQUEESEntity();
                    LIS_ESTOQUEESTy.TOTALMOVIMENTACAO = SumTotalPesquisa("TOTALMOVIMENTACAO");
                    LIS_ESTOQUEESTy.VALORIPI = SumTotalPesquisa("VALORIPI");
                    LIS_ESTOQUEESTy.VALORFRETE = SumTotalPesquisa("VALORFRETE");
                    LIS_ESTOQUEESColl.Add(LIS_ESTOQUEESTy);


                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ESTOQUEESColl;

                    lblTotalPesquisa.Text = LIS_ESTOQUEESColl.Count.ToString();
                }
                else
                {
                    MessageBox.Show(ConfigMessage.Default.searchFieldType);
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                    txtCriterioPesquisa.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
              
        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void GetGridProdutoMov(int IDESTOQUEES)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDESTOQUEES", "System.Int32", "=", IDESTOQUEES.ToString()));
                
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);
                dataGridProdMov.DataSource = null;
                dataGridProdMov.AutoGenerateColumns = false;
                dataGridProdMov.DataSource = LIS_MOVPRODUTOESColl;

                SomaValorICMS();
                SomaValorBASEICMS();
                SomaValorIPI();
                SomaValorFrete();
                SomaValorVLICMSST();
                SumMovProduto();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlEstoque.SelectTab(0);
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            Entity = null;
            tabControlEstoque.SelectTab(0);
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        

        private void Delete()
        {
            if (_IDESTOQUEES == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlEstoque.SelectTab(2);
            }
            else if (LIS_MOVPRODUTOESColl.Count > 0)
            {
                string msg = "Antes de excluir o estoque e necessário excluir a movimentação de produto!";
                errorProvider1.SetError(dataGridProdMov, msg);
                Util.ExibirMSg(msg, "Red");
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {

                        //Deleta a movimentação
                        ESTOQUEESP.Delete(_IDESTOQUEES);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");

                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_ESTOQUEESColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_ESTOQUEESColl.Count.ToString();
        }

        private void txtQuant_Enter(object sender, EventArgs e)
        {
            if(txtQuant.Text == "0,000")
                txtQuant.Text = string.Empty;
        }

        private void txtCustoUnitario_Enter(object sender, EventArgs e)
        {
            if(txtCustoUnitario.Text == "0,00")
             txtCustoUnitario.Text = string.Empty;
        }

        private void txtCustoUnitario_Leave(object sender, EventArgs e)
        {

        }

        private void txtValorFrete_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorFrete.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorFrete.Text))
                {
                    errorProvider1.SetError(txtValorFrete, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorFrete.Text);
                    txtValorFrete.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorFrete, "");
                    CalTotalGeral();
                }
            }
            else
                txtValorFrete.Text = "0,00";
        }

        private void txtValoIPI_Validating(object sender, CancelEventArgs e)
        {
            if (txtValoIPI.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValoIPI.Text))
                {
                    errorProvider1.SetError(txtValoIPI, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValoIPI.Text);
                    txtValoIPI.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValoIPI, "");
                    CalTotalGeral(); 
                }
            }
            else
                txtValoIPI.Text = "0,00";
        }

        private void txtValorICMS_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorICMS.Text))
                {
                    errorProvider1.SetError(txtValorICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorICMS.Text);
                    txtValorICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorICMS, "");
                    CalTotalGeral(); 
                }
            }
            else
                txtValorICMS.Text = "0,00";
        }

        private void txtValorFrete_Enter(object sender, EventArgs e)
        {
            if (txtValorFrete.Text == "0,00")
                txtValorFrete.Text = string.Empty;
        }

        private void txtValoIPI_Enter(object sender, EventArgs e)
        {
            if (txtValoIPI.Text == "0,00")
                txtValoIPI.Text = string.Empty;
        }

        private void txtValorICMS_Enter(object sender, EventArgs e)
        {
            if (txtValorICMS.Text == "0,00")
                txtValorICMS.Text = string.Empty;
        }

        private void txtTotalGeral_Validating(object sender, CancelEventArgs e)
        {
            if (txtTotalGeral.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalGeral.Text))
                {
                    errorProvider1.SetError(txtTotalGeral, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                     Double f = Convert.ToDouble(txtTotalGeral.Text);
                    txtTotalGeral.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtTotalGeral, "");
                }
            }
            else
                txtTotalGeral.Text = "0,00";
        }

        private void txtTotalGeral_Enter(object sender, EventArgs e)
        {
            if (txtTotalGeral.Text == "0,00")
                txtTotalGeral.Text = string.Empty;
        }

        

        private void CalTotalGeral()
        {
            decimal valorTotal = 0;
            valorTotal += Convert.ToDecimal(txtValorFrete.Text) + Convert.ToDecimal(txtValoIPI.Text) +
                    Convert.ToDecimal(txtTotalProdutos.Text);


            txtTotalGeral.Text = Convert.ToString(valorTotal);
            Double f = Convert.ToDouble(txtTotalGeral.Text);
            txtTotalGeral.Text = string.Format("{0:n2}", f);

            txtTotalFinanceiro.Text = txtTotalGeral.Text;
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_ESTOQUEESColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_ESTOQUEESEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_ESTOQUEESEntity>(orderBy);

                    LIS_ESTOQUEESColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_ESTOQUEESColl;
                    lblObsField.Text = string.Empty;
                }
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

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        { 
        }

        Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;
       

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private LIS_MOVPRODUTOESCollection ProdutoRel(int IDESTOQUEES)
        {
            LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDESTOQUEES", "System.Int32", "=", IDESTOQUEES.ToString()));

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                return LIS_MOVPRODUTOESColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_MOVPRODUTOESColl;
            }
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            modelo1ToolStripMenuItem_Click(null, null);
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlEstoque.SelectTab(2);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlEstoque.SelectTab(2);
        }
        
        private void listaDeMovimentaçãoPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoMoviEstoque frm = new FrmProdutoMoviEstoque())
            {
                frm.ShowDialog();
            }
        }

        private void GetDropProdutoMovRela()
        {
            try
            {
                ProdutoColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

                cbProduto.DisplayMember = "NOMEPRODUTO";
                cbProduto.ValueMember = "IDPRODUTO";

                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                ProdutoColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

                ProdutoColl.Sort(comparer.Comparer);
                cbProduto.DataSource = ProdutoColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCentroCusto()
        {
            try
            {
                CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

                cbCentroCusto.DisplayMember = "DESCRICAO";
                cbCentroCusto.ValueMember = "IDCENTROCUSTOS";

                CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
                CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
                CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

                Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto.DisplayMember);

                CENTROCUSTOSColl.Sort(comparer.Comparer);
                cbCentroCusto.DataSource = CENTROCUSTOSColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void FrmProdutoEstoRela_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnCadTipo.Image = Util.GetAddressImage(6);

            GetDropProdutoMovRela();
            VerificaAcesso();

            this.Cursor = Cursors.Default;	
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
            }
        }

        private void cbCentroCusto_Enter(object sender, EventArgs e)
        {
            GetDropCentroCusto();
        }

        private void cbFornecedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
            if (_IDESTOQUEES == -1)
                MessageBox.Show("Antes de adicionar é necessário gravar a movimentação!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            else
            {
                if (ValidaDuplicatas())
                {
                    try
                    {

                         DateTime DATAVENCIT = Convert.ToDateTime(mskDataVecto.Text);
                         for (int i = 0; i < Convert.ToInt32(txtNParcelas.Text); i++)
                         {
                             if (!chkVectoFixo.Checked)
                             {
                                 if (i > 0)
                                     DATAVENCIT = DATAVENCIT.AddDays(Convert.ToInt32(TxtDias.Text));
                             }
                             else
                             {
                                 if (i > 0)
                                 {
                                     DATAVENCIT = DATAVENCIT.AddDays(30);
                                     int DIAVECTO = Convert.ToInt32(Convert.ToDateTime(mskDataVecto.Text).Day);
                                     int MESVECTO = DATAVENCIT.Month;
                                     int ANOVECTO = DATAVENCIT.Year;
                                     string DATAFIXO = DIAVECTO + "/" + MESVECTO + "/" + ANOVECTO;
                                     DATAVENCIT = Convert.ToDateTime(DATAFIXO);
                                     mskDataVecto.Text = DATAVENCIT.ToString("dd/MM/yyyy");
                                 }
                             }

                             DUPLICATAPAGAREntity DUPLICATAPAGARty = new DUPLICATAPAGAREntity();
                             DUPLICATAPAGARty.IDDUPLICATAPAGAR = -1;
                             DUPLICATAPAGARty.IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);

                             if (cbCentroCusto.SelectedIndex > 0)
                                 DUPLICATAPAGARty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                             int NumTotalDupl = LIS_DUPLICATAPAGARColl.Count + 1;
                             DUPLICATAPAGARty.NUMERO = txtNDocumento.Text + "-" + (i + 1).ToString();
                             DUPLICATAPAGARty.DATAEMISSAO = Convert.ToDateTime(maskedtxtData.Text);
                             DUPLICATAPAGARty.DATAVECTO = DATAVENCIT;
                             DUPLICATAPAGARty.VALORDUPLICATA = Convert.ToDecimal(txtVlParcelas.Text);
                             DUPLICATAPAGARty.VALORDEVEDOR = Convert.ToDecimal(txtVlParcelas.Text);
                             DUPLICATAPAGARty.IDSTATUS = 1;//Aberto

                             DUPLICATAPAGARP.Save(DUPLICATAPAGARty);
                         }

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                        GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtNDocumento.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        MessageBox.Show("Erro técnico: " + ex.Message); 
                    }            
                }
            }
        }

        private void SumFinanceiroForne()
        {
            decimal valorTotal = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                valorTotal += Convert.ToDecimal(item.VALORDUPLICATA);
            }

            lblTotalFinanceiro.Text = valorTotal.ToString();
            Double f = Convert.ToDouble(lblTotalFinanceiro.Text);
            lblTotalFinanceiro.Text = string.Format("{0:n2}", f);

            lblTotalFinanceiro.Text = "Total: " + lblTotalFinanceiro.Text;           
        }

        private void GridDuplicatasFornecedor(int idFornecedor, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", idFornecedor.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "like", numero));

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATAPAGAR");
                dataGridDuplFornecedor.AutoGenerateColumns = false;
                dataGridDuplFornecedor.DataSource = LIS_DUPLICATAPAGARColl;
                SumFinanceiroForne();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }    
        }

        private Boolean ValidaDuplicatas()
        {
            Boolean result = true;

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text) || txtVlParcelas.Text == "0,00")
            {
                errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataVecto.Text))
            {
                errorProvider1.SetError(mskDataVecto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean ValidacoesFinanceiro()
        {
            Boolean result = true;

            if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalFinanceiro.Text) || txtTotalFinanceiro.Text == "0,00")
            {
                errorProvider1.SetError(txtTotalFinanceiro, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text) || txtVlParcelas.Text == "0,00")
            {
                errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text))
            {
                errorProvider1.SetError(TxtDias, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataVecto.Text))
            {
                errorProvider1.SetError(mskDataVecto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void txtTotalFinanceiro_Validating(object sender, CancelEventArgs e)
        {
            if (txtTotalFinanceiro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtTotalFinanceiro.Text))
                {
                    errorProvider1.SetError(txtTotalFinanceiro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtTotalFinanceiro.Text);
                    txtTotalFinanceiro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtTotalFinanceiro, "");
                }
            }
            else
            {
                txtTotalFinanceiro.Text = "0,00";
                errorProvider1.SetError(txtTotalFinanceiro, "");

            }
        }

        private void txtVlParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (txtVlParcelas.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text))
                {
                    errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtVlParcelas.Text);
                    txtVlParcelas.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVlParcelas, "");
                }
            }
            else
            {
                txtVlParcelas.Text = "0,00";
                errorProvider1.SetError(txtVlParcelas, "");

            }
        }

        private void txtNParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtVlParcelas, "");
            }            
        }

        private void txtVlParcelas_Enter(object sender, EventArgs e)
        {
            if(txtVlParcelas.Text == "0,00")
                txtVlParcelas.Text = string.Empty;
        }

        private void txtNParcelas_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text) || txtNParcelas.Text == "0")
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                txtVlParcelas.Text = (Convert.ToDecimal(txtTotalFinanceiro.Text) / Convert.ToDecimal(txtNParcelas.Text)).ToString("n2");
                errorProvider1.SetError(txtNParcelas, "");
            }    

            
        }

        private void TxtDias_Leave(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(TxtDias.Text) || TxtDias.Text == "0")
            {
                errorProvider1.SetError(TxtDias, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                DateTime DateVectro = DateTime.Now.AddDays(Convert.ToInt32(TxtDias.Text)); 
                mskDataVecto.Text = DateVectro.ToString("dd/MM/yyyy");
                errorProvider1.SetError(TxtDias, "");
            }   
        }

        private void txtNParcelas_Enter(object sender, EventArgs e)
        {
            if (txtNParcelas.Text == "0")
                txtNParcelas.Text = string.Empty;

        }

        private void TxtDias_Enter(object sender, EventArgs e)
        {
            if (TxtDias.Text == "0")
                TxtDias.Text = string.Empty;
        }

        private void dataGridDuplFornecedor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATAPAGARColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                            DUPLICATAPAGARP.Delete(CodSelect);
                            GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtNDocumento.Text);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            SumFinanceiroForne();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }

                else if (ColumnSelecionada == 1)
                {
                    FrmContasPagar FrmCP = new FrmContasPagar();
                    FrmCP.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                    FrmCP.ShowDialog();
                    GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtNDocumento.Text);
                }

            }
        }       

        private void mskDataVecto_Leave(object sender, EventArgs e)
        {
            if (mskDataVecto.Text != "  /  /")
            {
                if (!ValidacoesLibrary.ValidaTipoDateTime(mskDataVecto.Text))
                {
                    MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                    errorProvider1.SetError(mskDataVecto, ConfigMessage.Default.MsgDataInvalida);
                }
                else
                {
                    errorProvider1.SetError(mskDataVecto, "");
                }
            }
            else
            {
                errorProvider1.SetError(mskDataVecto, "");
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_ESTOQUEESColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlEstoque.SelectTab(2);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void dataGridProdMov_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_MOVPRODUTOESColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_MOVPRODUTOESColl[rowindex].IDMOVPRODUTOES);
                    Entity2 = MOVPRODUTOESP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_MOVPRODUTOESColl[rowindex].IDMOVPRODUTOES);
                            MOVPRODUTOESP.Delete(CodSelect);
                            ExcluirEstoqueLote(Convert.ToInt32(LIS_MOVPRODUTOESColl[rowindex].IDPRODUTO), Convert.ToDecimal(LIS_MOVPRODUTOESColl[rowindex].QUANTIDADE));
                            GetGridProdutoMov(_IDESTOQUEES);
                            SomaValorICMS();
                            Entity2 = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            
                        }
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                            
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void ExcluirEstoqueLote(int IDPRODUTO, Decimal QUANTIDADE)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "=", txtNDocumento.Text));
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                RowRelatorio.Add(new RowsFiltro("QUANTIDADE", "System.Decimal", "=", QUANTIDADE.ToString()));

                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();                
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                if(ESTOQUELOTEColl.Count > 0)
                {
                    ESTOQUELOTEP.Delete(Convert.ToInt32(ESTOQUELOTEColl[0].IDESTOQUELOTE));
                    Util.ExibirMSg("Estoque Lote Excluido com Sucesso!", "blue");
                }
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);
                {
                    foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                    {
                        MOVPRODUTOESP.Delete(Convert.ToInt32(item.IDMOVPRODUTOES));
                    }

                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                }

                GetGridProdutoMov(_IDESTOQUEES);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }


        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }


        public MonthCalendar monthCalendar5 = new MonthCalendar();
        Form FormCalendario5 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar5.Name = "monthCalendar5";
            monthCalendar5.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar5_DateSelected);

            FormCalendario5.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario5.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario5.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario5.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario5.Location = new Point(230, 55);
            FormCalendario5.Name = "FrmCalendario5";
            FormCalendario5.Text = "Calendário";
            FormCalendario5.ResumeLayout(false);
            FormCalendario5.Controls.Add(monthCalendar5);
            FormCalendario5.ShowDialog();
        }

        private void monthCalendar5_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar5.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario5.Close();
        }

        private void btnCadCOP_Click(object sender, EventArgs e)
        {
            using (FrmCFOP frm = new FrmCFOP())
            {
                int CodSelec = Convert.ToInt32(cbCFOP.SelectedValue);
                frm.ShowDialog();               
                GetDropCFOP();               
                cbCFOP.SelectedValue = CodSelec;
            }
        }

        private void GetDropCFOP()
        {
            try
            {
                CFOPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

                cbCFOP.DisplayMember = "CODCFOP";
                cbCFOP.ValueMember = "IDCFOP";

                CFOPEntity CFOPTy = new CFOPEntity();
                CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
                CFOPTy.IDCFOP = -1;
                CFOPColl.Add(CFOPTy);

                Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>(cbCFOP.DisplayMember);

                CFOPColl.Sort(comparer.Comparer);
                cbCFOP.DataSource = CFOPColl;

                cbCFOP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }  
        }

        private void cbCFOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCFOP.SelectedIndex > 0)
            {
                txtDesCFOP.Text = (CFOPP.Read(Convert.ToInt32(cbCFOP.SelectedValue)).DESCRICAO);               
            }
            else
                txtDesCFOP.Text = string.Empty;
        }

        private void btnLimpaProduto_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }

        private void pedidoDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        int _line = 0;
        int itemproduto = 1;
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //inicio Cabeçalho
            ConfigReportStandard config = new ConfigReportStandard();

            //LIS_ESTOQUEESColl
            LIS_ESTOQUEESCollection LIS_ESTOQUEESPrintColl = new LIS_ESTOQUEESCollection();

            //Armazena na coleção do Orçamento Selecionada
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDESTOQUEES", "System.Int32", "=", _IDESTOQUEES.ToString()));
            LIS_ESTOQUEESPrintColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio);

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

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 530, 30);
                }
            }

            //'nome da empresa
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);


            e.Graphics.DrawString("Pedido Compra", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 430, 38);
            e.Graphics.DrawString(Entity.IDESTOQUEES.ToString().PadLeft(6, '0'), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 430, 53);

            //Dados Cliente
            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 135, config.MargemDireita - 20, 100);


            //Armazena dados do Fornecedor
            LIS_FORNECEDORCollection LIS_FORNECEDORColl = new LIS_FORNECEDORCollection();
            LIS_FORNECEDORProvider LIS_FORNECEDORP = new LIS_FORNECEDORProvider();
            RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
            RowRelatorioCliente.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", (cbFornecedor.SelectedValue).ToString()));
            LIS_FORNECEDORColl = LIS_FORNECEDORP.ReadCollectionByParameter(RowRelatorioCliente);

            e.Graphics.DrawString("Fornecedor:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);
            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 110, 140);

            e.Graphics.DrawString("Data:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 140);
            e.Graphics.DrawString(Convert.ToDateTime(Entity.DATAMOVIM).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 140);

            e.Graphics.DrawString("End.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 155);
            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].ENDERECO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 155);

            e.Graphics.DrawString("Bairro:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 155);
            
            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].BAIRRO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 155);

            e.Graphics.DrawString("Cidade:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 170);

            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].CIDADE, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 170);
            e.Graphics.DrawString("UF:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);

            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].UF, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 455, 170);

            e.Graphics.DrawString("Tel.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            
            if (LIS_FORNECEDORColl.Count > 0)
                e.Graphics.DrawString(Util.LimiterText(LIS_FORNECEDORColl[0].TELEFONE1, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 95, 185);

            e.Graphics.DrawString("Tipo: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 200);
            e.Graphics.DrawString("Documento: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 200, 200);

            e.Graphics.DrawString(LIS_ESTOQUEESPrintColl[0].NOMETIPOMOVIMENTACAO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 100, 200);
            e.Graphics.DrawString(LIS_ESTOQUEESPrintColl[0].NDOCUMENTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 300, 200);

            //Fim Cabeçalho


            // inicio Corpo Pedido
            //Alinhamento dos valores
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;

            float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
            float yLineTop = e.MarginBounds.Top + 80;

            //campos a serem impressos 
            e.Graphics.DrawString("Item", config.FonteNegrito, Brushes.Black, config.MargemEsquerda - 30, 250);

            e.Graphics.DrawString("Quant", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 250);
            e.Graphics.DrawString("Descrição", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 90, 250);
            e.Graphics.DrawString("Vl. Unitário", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 250);
            e.Graphics.DrawString("Total", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 650, 250);
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 270, config.MargemDireita + 30, 270);

            StringFormat formataString = new StringFormat();
            formataString.Alignment = StringAlignment.Far;
            formataString.LineAlignment = StringAlignment.Far;


            for (; _line < LIS_MOVPRODUTOESColl.Count; _line++)
            {
                if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 200) - 100)
                {
                    //Rodape
                    paginaAtual++;
                    // e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                    //e.Graphics.DrawString("Pagina: " + paginaAtual, config.FonteNormal, Brushes.Black, new PointF(config.MargemEsquerda, yLineTop + 120));

                    e.HasMorePages = true;
                    return;
                }

                yLineTop += lineHeight;

                e.Graphics.DrawString(itemproduto.ToString(), config.FonteNormal, Brushes.Black, config.MargemEsquerda - 20, yLineTop + 80);

                e.Graphics.DrawString(LIS_MOVPRODUTOESColl[_line].QUANTIDADE.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 30, yLineTop + 80);

                e.Graphics.DrawString(Util.LimiterText(LIS_MOVPRODUTOESColl[_line].NOMEPRODUTO.ToUpper(), 50), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 90, yLineTop + 80);

                e.Graphics.DrawString(Convert.ToDecimal(LIS_MOVPRODUTOESColl[_line].VALORCUNITARIO).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 550 + 45, yLineTop + 80 + 10, formataString);
                e.Graphics.DrawString(Convert.ToDecimal(LIS_MOVPRODUTOESColl[_line].VALORTOTAL).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 650 + 55, yLineTop + 80 + 10, formataString);

                itemproduto++;

            }

            yLineTop += lineHeight;

            e.Graphics.DrawString("Total R$: ", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 600, yLineTop + 80);
            e.Graphics.DrawString(txtTotalProdutos.Text, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, yLineTop + 80);

            yLineTop += lineHeight;

            // Fim Corpo Pedido
            //Ultima Pagina
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);


            e.HasMorePages = false;
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _line = 0;
            itemproduto = 1;
        }

        private void txtBaseCalcICMS_Validating(object sender, CancelEventArgs e)
        {
            if (txtBaseCalcICMS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseCalcICMS.Text))
                {
                    errorProvider1.SetError(txtBaseCalcICMS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtBaseCalcICMS.Text);
                    txtBaseCalcICMS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorICMS, "");                    
                }
            }
            else
                txtBaseCalcICMS.Text = "0,00";
        }

        private void txtAliqICMS_Validating(object sender, CancelEventArgs e)
        {
            if (txtAliqICMSProd.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliqICMSProd.Text))
                {
                    errorProvider1.SetError(txtAliqICMSProd, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtAliqICMSProd.Text);
                    txtAliqICMSProd.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliqICMSProd, "");
                }
            }
            else
                txtAliqICMSProd.Text = "0,00";
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDESTOQUEES == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlEstoque.SelectTab(2);
            }
            else
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDocument2;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
        }

        private void modelo1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDESTOQUEES == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlEstoque.SelectTab(2);
            }
            else
            {
                using (FrmRelatorioNotaCompra frm = new FrmRelatorioNotaCompra())
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDESTOQUEES", "System.Int32", "=", _IDESTOQUEES.ToString()));
                    frm.LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio);
                    frm.ShowDialog();
                }
            }
        }

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                GetDropTipoDuplicata();
                cbTipo.SelectedValue = CodSelec;
            }
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            using (FrmCaixa frm = new FrmCaixa())
            {
                frm.ShowDialog();
            }
        }


        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_IDESTOQUEES == -1)
            {
                MessageBox.Show("Antes de adicionar o Caixa é necessário gravar a Nota de Compra!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (Convert.ToInt32(cbTipo.SelectedValue) < 1)
                {
                    errorProvider1.SetError(cbTipo, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else if (Convert.ToDecimal(txtValorPago.Text) == 0)
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Deseja realmente lançar o valor de " + txtValorPago.Text + " no caixa?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                        EntradaCaixa();
                }
            }
        }

        private void EntradaCaixa()
        {
            CAIXAEntity CaixaTy = new CAIXAEntity();
            CAIXAProvider CaixaP = new CAIXAProvider();

            CaixaTy.IDCAIXA = -1;
            CaixaTy.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);
            CaixaTy.VALOR = Convert.ToDecimal(txtValorPago.Text);
            CaixaTy.DATAMOV = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            CaixaTy.IDTIPOMOVCAIXA = 2;// Debito

            if (cbCentroCusto.SelectedIndex > 0)
                CaixaTy.IDCENTROCUSTOS = Convert.ToInt32(cbCentroCusto.SelectedValue);

            CaixaTy.NDOCUMENTO = "NC" + _IDESTOQUEES.ToString();
            CaixaTy.OBSERVACAO = "Nota de Compra nº " + "NC" + _IDESTOQUEES.ToString() + " Fornecedor: " + cbFornecedor.Text;

            try
            {
                CaixaP.Save(CaixaTy);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {

                MessageBox.Show("Não foi possível fazer o movimento de caixa!");
            }
        }

        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtValorPago.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorPago.Text))
                {
                    errorProvider1.SetError(txtValorPago, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(txtValorPago.Text);
                    txtValorPago.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorPago, "");
                }
            }
            else
                txtValorPago.Text = "0,00";
        }

        private void txtBaseICMSProd_Validating(object sender, CancelEventArgs e)
        {
            if (txtBaseICMSProd.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseICMSProd.Text))
                {
                    errorProvider1.SetError(txtBaseICMSProd, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtBaseICMSProd.Text);
                    txtBaseICMSProd.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtBaseICMSProd, "");
                }
            }
            else
            {
                txtBaseICMSProd.Text = "0,00";
                errorProvider1.SetError(txtBaseICMSProd, "");

            }
        }

        private void txtVLICMSProd_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtVLIPI_Validating(object sender, CancelEventArgs e)
        {
            if (txtVLIPI.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtVLIPI.Text))
                {
                    errorProvider1.SetError(txtVLIPI, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtVLIPI.Text);
                    txtVLIPI.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtVLIPI, "");
                }
            }
            else
            {
                txtVLIPI.Text = "0,00";
                errorProvider1.SetError(txtVLIPI, "");

            }
        }

        private void importarXMLNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportarXMLNfe = false;

            if (_IDESTOQUEES == -1)
            {
                MessageBox.Show("Antes de adicionar os produtos e necessário gravar a movimentação!");
                //tabControlEstoque.SelectTab(2);
            }
            else if (Convert.ToInt32(cbCFOP.SelectedValue) < 1)
            {
                 errorProvider1.SetError(cbCFOP, ConfigMessage.Default.FieldErro);
                 MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Selecione o arquivo";
                fdlg.InitialDirectory = @"c:\";
                fdlg.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;

                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    GeraDsProd(fdlg.FileName);
                }
            }
        }

        private void BuscaProdutoCodigo(string Codigo)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", Codigo));

                PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
                PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                cbProduto.SelectedValue = -1;

                if (PRODUTOSColl.Count > 0)
                    cbProduto.SelectedValue = Convert.ToInt32(PRODUTOSColl[0].IDPRODUTO);
                else
                    MessageBox.Show("CODPRODUTOFORNECEDOR: " + Codigo + " não encontrado!",
                                 ConfigSistema1.Default.NomeEmpresa,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error,
                                 MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GeraDsProd(string caminhoXml)
        {
            //Instancia doc com o xml
            XmlDocument doc = new XmlDocument();
            try
            {
                ImportarXMLNfe = true;
                //le o xml no caminho correto
                doc.Load(caminhoXml);
                //le o elemento a ser trabalhado
                XmlNodeList No = doc.GetElementsByTagName("prod");
                //cria as colunas para serem colocandas no dataset.
              
                //Leitura dos nos selecionados
                foreach (XmlNode book in No)
                {
                    for (int i = 0; i < book.ChildNodes.Count; i++)
                    {
                        //Verifica o nome do no correspondente a ser colocado na dataset
                        switch (book.ChildNodes.Item(i).Name)
                        {
                            case "cProd":
                                BuscaProdutoCodigo(book.ChildNodes.Item(i).InnerXml);
                                break;
                            case "xProd":
                               // _row["xProd"] = book.ChildNodes.Item(i).InnerXml;
                                break;
                            case "NCM":
                                break;
                            case "CFOP":
                               // _row["CFOP"] = book.ChildNodes.Item(i).InnerXml;
                                break;
                            case "uCom":
                              //  _row["uCom"] = book.ChildNodes.Item(i).InnerXml;
                                break;
                            case "qCom":
                                txtQuant.Text = string.Format("{0:n2}", (book.ChildNodes.Item(i).InnerXml));
                                txtQuant.Text = txtQuant.Text.Replace(".", ",");
                                break;
                            case "vUnCom":
                                txtCustoUnitario.Text = string.Format("{0:n2}", book.ChildNodes.Item(i).InnerXml);
                                txtCustoUnitario.Text = txtCustoUnitario.Text.Replace(".", ",");
                                break;
                            case "vProd":
                                //_row["vProd"] = string.Format("{0:d}", (book.ChildNodes.Item(i).InnerXml));
                                break;
                            case "vUnTrib":
                              //  _row["vUnTrib"] = string.Format("{0:d}", (book.ChildNodes.Item(i).InnerXml));
                                break;
                            case "vDesc":
                                string Desconto = string.Format("{0:n2}", book.ChildNodes.Item(i).InnerXml).Replace(".", ",");
                                if (Desconto != string.Empty && Convert.ToDecimal(Desconto) > 0)
                                {
                                    decimal TotalDesconto = (Convert.ToDecimal(txtCustoUnitario.Text) * Convert.ToDecimal(txtQuant.Text)) - Convert.ToDecimal(Desconto);
                                    txtCustoUnitario.Text = (TotalDesconto / Convert.ToDecimal(txtQuant.Text)).ToString("n2").Replace(".", ",");
                                }
                                break;
                        }
                    }
                   
                    if (book.NextSibling.Name.ToString() == "imposto")
                    {
                        for (int i = 0; i < book.NextSibling.ChildNodes.Count; i++)
                        {
                            switch (book.NextSibling.ChildNodes.Item(i).Name.ToString())
                            {
                                case "ICMS":
                                    //verifica quantos filhos de icms estao dentro do pai
                                    for (int ii = 0; ii < book.NextSibling.ChildNodes.Item(i).ChildNodes.Count; ii++)
                                    {
                                        for (int iii = 0; iii < book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Count; iii++)
                                        {
                                            switch (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).Name.ToString())
                                            {
                                                case "orig":
                                                     txtcstcsosn.Text = String.Format("{0:0}", Convert.ToInt32(book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml));
                                                    break;
                                                case "CSOSN":
                                                    txtcstcsosn.Text += String.Format("{0:000}", Convert.ToInt32(book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml));
                                                    break;
                                                case "CST":
                                                    //if (_dsAux.Tables[0].Columns.Contains("CST"))
                                                    //{
                                                    //_row["CST"] = String.Format("{0:000}", Convert.ToInt32(book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml));
                                                    //}
                                                    break;
                                                case "vBC":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtBaseICMSProd.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtBaseICMSProd.Text = txtBaseICMSProd.Text.Replace(".", ",");
                                                    }
                                                    break;
                                                case "pICMS":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtAliqICMSProd.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtAliqICMSProd.Text = txtAliqICMSProd.Text.Replace(".", ",");
                                                    }
                                                    break;
                                                case "vICMS":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtVLICMSProd.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtVLICMSProd.Text = txtVLICMSProd.Text.Replace(".", ",");
                                                    }
                                                    break;
                                                case "vBCST":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtBaseICMSST.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtBaseICMSST.Text = txtBaseICMSST.Text.Replace(".", ",");
                                                    }
                                                    break;
                                                case "vICMSST":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtVlICMSST.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtVlICMSST.Text = txtVlICMSST.Text.Replace(".", ",");
                                                    }                                                    
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case "IPI":
                                    //verifica quantos filhos de icms estao dentro do pai
                                    for (int ii = 0; ii < book.NextSibling.ChildNodes.Item(i).ChildNodes.Count; ii++)
                                    {
                                        for (int iii = 0; iii < book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Count; iii++)
                                        {
                                            switch (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).Name.ToString())
                                            {
                                                case "vIPI":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        txtVLIPI.Text = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                        txtVLIPI.Text = txtVLIPI.Text.Replace(".", ",");
                                                    }
                                                    break;
                                                case "pIPI":
                                                    if (book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml != string.Empty)
                                                    {
                                                        //_row["pIPI"] = book.NextSibling.ChildNodes.Item(i).ChildNodes.Item(ii).ChildNodes.Item(iii).InnerXml;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }//for imposto
                    }//if imposto

                    if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                    { 
                        button1_Click(null, null);
                    }
                }

                MessageBox.Show("Total de produto(s) adicionado(s) com sucesso: " + LIS_MOVPRODUTOESColl.Count.ToString());

                ImportarXMLNfe = false;
            }
            catch (Exception ex)
            {
                ImportarXMLNfe = false;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void utilitárioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void movimentaçãoPorCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMovimCFOP Frm = new FrmMovimCFOP();
            Frm.ShowDialog();
        }

        private void txtVlFreteItem_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtVLICMSProd_Enter(object sender, EventArgs e)
        {
            try
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtBaseICMSProd.Text))
                     txtBaseICMSProd.Text = "0,00";

                 if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliqICMSProd.Text))
                     txtAliqICMSProd.Text = "0,00";

                txtVLICMSProd.Text = (Convert.ToDecimal(txtBaseICMSProd.Text) * Convert.ToDecimal(txtAliqICMSProd.Text) / 100).ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtBaseICMSST_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void VlICMSST_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void txtVlDescontoProduto_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Movimentação de Estoque";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void cbProduto_Click(object sender, EventArgs e)
        {

        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (LIS_ESTOQUEESColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_ESTOQUEESColl[rowindex].IDESTOQUEES);

                    Entity = ESTOQUEESP.Read(CodigoSelect);
                    GetGridProdutoMov(CodigoSelect);

                    tabControlEstoque.SelectTab(0);
                    cbTipoMov.Focus();
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_ESTOQUEESColl[rowindex].NDOCUMENTO + " - " + LIS_ESTOQUEESColl[rowindex].NOMEFORNECEDOR,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            //Lista Financeiro
                            GridDuplicatasFornecedor(Convert.ToInt32(LIS_ESTOQUEESColl[rowindex].IDFORNECEDOR), LIS_ESTOQUEESColl[rowindex].NDOCUMENTO);
                            //Excluir FInanceiro
                            foreach (var item in LIS_DUPLICATAPAGARColl)
                            {
                                DUPLICATAPAGARP.Delete(Convert.ToInt32(item.IDDUPLICATAPAGAR));
                            }

                            //Lista Produtos
                            GetGridProdutoMov( Convert.ToInt32(LIS_ESTOQUEESColl[rowindex].IDESTOQUEES));
                            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                            {
                                MOVPRODUTOESP.Delete(Convert.ToInt32(item.IDMOVPRODUTOES));
                            }


                            CodigoSelect = Convert.ToInt32(LIS_ESTOQUEESColl[rowindex].IDESTOQUEES);
                            //Delete Pedido
                            ESTOQUEESP.Delete(CodigoSelect);

                            btnPesquisa_Click(null, null);

                            Entity = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro técnico: " + ex.Message);
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
            }
        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
               getProduto(Convert.ToInt32(cbProduto.SelectedValue)); 
        }

        private void dataGridProdMov_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (LIS_MOVPRODUTOESColl.Count > 0)
                {
                    string orderBy = dataGridProdMov.Columns[e.ColumnIndex].DataPropertyName;
                    if (orderBy != string.Empty)
                    {
                        Phydeaux.Utilities.DynamicComparer<LIS_MOVPRODUTOESEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MOVPRODUTOESEntity>(orderBy);

                        LIS_MOVPRODUTOESColl.Sort(comparer.Comparer);

                        dataGridProdMov.DataSource = null;
                        dataGridProdMov.DataSource = LIS_MOVPRODUTOESColl;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message );
            }
        }

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
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
                RowRelatorio.Add(new RowsFiltro("NOMEFORNECEDOR", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("idestoquees", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_ESTOQUEESColl = LIS_ESTOQUEESP.ReadCollectionByParameter(RowRelatorio, "IDESTOQUEES DESC");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_ESTOQUEESColl;

                    lblTotalPesquisa.Text = LIS_ESTOQUEESColl.Count.ToString();
                }

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

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void importarProdutosCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDESTOQUEES == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlEstoque.SelectTab(2);
            }
            else
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                AbrirArquivoRetorno(openFileDialog1.FileName.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }               
            }
        }

        private void AbrirArquivoRetorno(string NomeArquivo)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            string PathSystem = NomeArquivo;
            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);
            string sLine = "";
            try
            {
                int contadorP = 0;
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                  
                    if (sLine != null)
                    {
                        string[] coluna = sLine.Split(';');

                        string CodigoProduto = string.Empty;
                        string EstoqueReal = string.Empty;

                        if (coluna.Length > 1 && coluna[0] != string.Empty)
                        {
                            CodigoProduto = coluna[0].Trim();
                            EstoqueReal = coluna[2].Trim();

                            //VERIFICAR SE O PRODUTO EXISTE NO CADASTRO
                            PRODUTOSEntity PRODUTOStY = new PRODUTOSEntity();
                            PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(CodigoProduto));
                            if (PRODUTOStY != null && Convert.ToDecimal(EstoqueReal) > 0)
                            {
                                   SalvaProduto(Convert.ToInt32(CodigoProduto), Convert.ToDecimal(EstoqueReal));
                                   contadorP++;
                            }
                        }                       
                    }
                }

                objReader.Dispose();

                GetGridProdutoMov(_IDESTOQUEES);
                this.Cursor = Cursors.Default;
                MessageBox.Show("Total de Produtos Adicionaos: " + contadorP.ToString());
            }
            catch (Exception ex)
            {
                objReader.Dispose();
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SalvaProduto(int CodProduto, decimal Estoque)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                MOVPRODUTOESEntity Entity_2 = new MOVPRODUTOESEntity();
                Entity_2.IDESTOQUEES = _IDESTOQUEES;
                Entity_2.IDMOVPRODUTOES = -1;
                Entity_2.IDPRODUTO = CodProduto;
                Entity_2.QUANTIDADE = Estoque;
                MOVPRODUTOESP.Save(Entity_2);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtCodLote_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Lote ou pressione Ctrl+E para pesquisar.";
        }

        private void txtCodLote_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = "";
            LoteExiste(txtCodLote.Text);
        }

        private Boolean LoteExiste(string CodLote)
        {
            Boolean Result = false;

            try
            {
                if (CodLote.Trim() != string.Empty)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("CODLOTE", "System.String", "=", CodLote));

                    LOTECollection LOTEColl = new LOTECollection();
                    LOTEProvider LOTEP = new LOTEProvider();

                    LOTEColl.Clear();
                    LOTEColl = LOTEP.ReadCollectionByParameter(RowRelatorio);

                    if (LOTEColl.Count > 0)
                    {
                        _IdLote = LOTEColl[0].IDLOTE;
                        Result = true;
                    }
                    else
                    {
                        _IdLote = -1;
                        MessageBox.Show("Lote: " + CodLote + " Não Existe!");
                        txtCodLote.Focus();
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return Result;
            }
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }

        private void estoqueLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEstoqueLote frm = new FrmEstoqueLote())
            {
                frm._NumeroDoc = txtNDocumento.Text;
                frm.ShowDialog();
            }
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }
    }
}

