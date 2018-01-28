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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Operacional;
using System.IO;
using CDSSoftware;
using BmsSoftware.UI;
using VVX;
using System.Diagnostics;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.Relatorio;
using System.Runtime.InteropServices;
using BmsSoftware.Classes.BMSworks.UI;
using System.Net.Mail;
using System.Net;
using winfit.Modulos.Outros;
using System.Text.RegularExpressions;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmCotacaoCompra : Form
    {
        Utility Util = new Utility();
        
        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();

        LIS_COTACAOCOMPRACollection LIS_COTACAOCOMPRAColl = new LIS_COTACAOCOMPRACollection();

        LIS_PRODUTOCOTACAOCollection LIS_PRODUTOCOTACAOColl = new LIS_PRODUTOCOTACAOCollection();
        LIS_PRODUTOCOTACAOProvider LIS_PRODUTOCOTACAOP = new LIS_PRODUTOCOTACAOProvider();

        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        PRODUTOCOTACAOProvider PRODUTOCOTACAOP = new PRODUTOCOTACAOProvider();
        COTACAOCOMPRAProvider COTACAOCOMPRAP = new COTACAOCOMPRAProvider();
        MENSAGEMProvider MENSAGEMP = new MENSAGEMProvider();
        
        LIS_COTACAOCOMPRAProvider LIS_COTACAOCOMPRAP = new LIS_COTACAOCOMPRAProvider();
        
        ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
        
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();

        string CasasDecimais = string.Empty;
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        [DllImport("kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        public FrmCotacaoCompra()
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

        public int _IDCOTACAOCOMPRA = -1;
        public COTACAOCOMPRAEntity Entity
        {
            get
            {
                string NUMREFERENCIA = txtNumReferencia.Text;
                int IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);
                DateTime DATAEMISSAO = Convert.ToDateTime(msktDataEmissao.Text);
                string FLAGCOTACAO = rdCotacao.Checked ? "S" : "N";
                int IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue); 
                string OBSERVACAO = txtObservacao.Text;
                decimal TOTALCOTACAO = Convert.ToDecimal(txtTotalCotacao.Text);

                return new COTACAOCOMPRAEntity(_IDCOTACAOCOMPRA, NUMREFERENCIA, IDFORNECEDOR, DATAEMISSAO, FLAGCOTACAO, IDSTATUS,
                                        OBSERVACAO, TOTALCOTACAO);
            }
            set
            {

                if (value != null)
                {
                    _IDCOTACAOCOMPRA = value.IDCOTACAOCOMPRA;
                    txtNControle.Text = _IDCOTACAOCOMPRA.ToString().PadLeft(6, '0');
                    txtNumReferencia.Text = value.NUMREFERENCIA;
                    cbFornecedor.SelectedValue = value.IDFORNECEDOR;
                    msktDataEmissao.Text = Convert.ToDateTime(value.DATAEMISSAO).ToString("dd/MM/yyyy");

                    rdCotacao.Checked = value.FLAGCOTACAO.TrimEnd() == "S" ? true : false;
                    rdCompra.Checked = !rdCotacao.Checked;                    

                    cbStatus.SelectedValue = value.IDSTATUS;
                    txtObservacao.Text = value.OBSERVACAO;
                    txtTotalCotacao.Text = Convert.ToDecimal(value.TOTALCOTACAO).ToString("n2");

                    errorProvider1.Clear();
                }
                else
                {
                    _IDCOTACAOCOMPRA = -1;
                    txtNControle.Text = string.Empty;
                    txtNumReferencia.Text = string.Empty;
                    cbFornecedor.SelectedValue =-1;

                    rdCotacao.Checked = true;
                    rdCompra.Checked = !rdCotacao.Checked;

                    cbStatus.SelectedValue = -1;
                    txtObservacao.Text =  string.Empty;
                    txtTotalCotacao.Text = "0,00";

                    errorProvider1.Clear();
                }
            }
        }

        int _IDPRODUTOCOTACAO = -1;
        public PRODUTOCOTACAOEntity Entity2
        {
            get
            {
                
                decimal? QUANTIDADE = Convert.ToDecimal(txtQuanProduto.Text);
                decimal? VALORUNITARIO = Convert.ToDecimal(txtValorUnitProd.Text);
                decimal? VALORTOTAL = VALORUNITARIO * QUANTIDADE;
                int? IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);

                return new PRODUTOCOTACAOEntity(_IDPRODUTOCOTACAO, _IDCOTACAOCOMPRA, QUANTIDADE,
                                                VALORUNITARIO, VALORTOTAL, IDPRODUTO);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOCOTACAO = value.IDPRODUTOCOTACAO;
                    txtQuanProduto.Text = Convert.ToDecimal(value.QUANTIDADE).ToString("n4");
                    txtValorUnitProd.Text = Convert.ToDecimal(value.VALORUNITARIO).ToString("n4");
                    SomaValorUnitario();
                    cbProduto.SelectedValue = value.IDPRODUTO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOCOTACAO = -1;
                    txtQuanProduto.Text = "1";
                    txtValorUnitProd.Text = "0,0000";
                    SomaValorUnitario();
                    cbProduto.SelectedIndex = 0;
                    txtCodPesquisa.Text = string.Empty;
                    errorProvider1.Clear();
                }
            }
        }
      

        private void FrmPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao")
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }


            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmPedidoVenda_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

           // this.MinimizeBox = false;
           // this.FormBorderStyle = FormBorderStyle.FixedDialog;

            EMPRESATy = EMPRESAP.Read(1);

            GetToolStripButtonCadastro();

            GetDropFornecedor();
            GetDropStatus();
            GetDropStatus();
            GetDropProdutos();
            GetDropStatus2();
            GetDropMensagem();

            btnCadCliente.Image = Util.GetAddressImage(6);
            btnStatus.Image = Util.GetAddressImage(6);
            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnCadMensagem.Image = Util.GetAddressImage(6);  
          
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            PreencheDropTipoPesquisa();
            PreencheDropCamposPesquisa();           

            if (_IDCOTACAOCOMPRA != -1)
            {
                int CodigoSelect = _IDCOTACAOCOMPRA;
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);

                Entity = COTACAOCOMPRAP.Read(CodigoSelect);
                ListaProdutoCotacao(_IDCOTACAOCOMPRA);
                txtNControle.Focus();

            }

            this.Cursor = Cursors.Default;

            VerificaAcesso();
            cbCamposPesquisa.SelectedIndex = 2;
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

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);
        }

        private void GetDropFornecedor()
        {
            try
            {
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

        private void GetDropStatus()
        {
            try
            {
                //11 Pedido de Venda
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "17");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

                STATUSProvider STATUSP = new STATUSProvider();
                cbStatus.DataSource = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUS";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropStatus2()
        {
            try
            {
                //11 Pedido de Venda
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "17");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

                STATUSProvider STATUSP = new STATUSProvider();
                STATUSCollection STATUSColl2 = new STATUSCollection();
                STATUSColl2 = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus2.DisplayMember = "NOME";
                cbStatus2.ValueMember = "IDSTATUS";

                STATUSEntity STATUSTy = new STATUSEntity();
                STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTy.IDSTATUS = -1;
                STATUSColl2.Add(STATUSTy);

                Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus2.DisplayMember);

                STATUSColl2.Sort(comparer.Comparer);
                cbStatus2.DataSource = STATUSColl2; 
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropProdutos()
        {
            try
            {
                LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGINATIVO", "System.String", "<>", "S"));
                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTOCOD");

                cbProduto.DisplayMember = "NOMEPRODUTOCOD";
                cbProduto.ValueMember = "IDPRODUTO";

                LIS_PRODUTOSEntity PRODUTOSTy = new LIS_PRODUTOSEntity();
                PRODUTOSTy.NOMEPRODUTOCOD = ConfigMessage.Default.MsgDrop;
                PRODUTOSTy.IDPRODUTO = -1;
                LIS_PRODUTOSColl.Add(PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>(cbProduto.DisplayMember);

                LIS_PRODUTOSColl.Sort(comparer.Comparer);
                cbProduto.DataSource = LIS_PRODUTOSColl;

                cbProduto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
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

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
             if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbFornecedor.SelectedValue = result;
                    }
                }
            }

            e.Handled = false;
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
        }

        private void btnCadCliente_Click(object sender, EventArgs e)
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

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUS = CodSelec;
                frm.ShowDialog();
                
                GetDropStatus();
                GetDropStatus2();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void btnExtratoCliente_Click(object sender, EventArgs e)
        {
         
        }

        private void cbCliente_SelectedValueChanged(object sender, EventArgs e)
        {
        }
   

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                using (FrmProduto frm = new FrmProduto())
                {
                    int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    frm._IDPRODUTO = CodSelec;
                    frm.ShowDialog();
                    GetDropProdutos();
                    cbProduto.SelectedValue = CodSelec;
                }
            }
            else
            {
                using (FrmProduto2 frm = new FrmProduto2())
                {
                    int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                    frm._IDPRODUTO = CodSelec;
                    frm.ShowDialog();
                    GetDropProdutos();
                    cbProduto.SelectedValue = CodSelec;
                }
            }
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {

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

     

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
                SomaValorUnitario();

        }

        private void SomaValorUnitario()
        {
            try
            {
                txtValorMtLinearTotal.Text = (Convert.ToDecimal(txtQuanProduto.Text) * Convert.ToDecimal(txtValorUnitProd.Text)).ToString("n2");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txtQuanProduto,"Erro");
                errorProvider1.SetError(txtValorUnitProd, "Erro");                
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorUnitProd_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                txtValorUnitProd.Text = "0,00";
            }
            else
            {
                Double f = Convert.ToDouble(txtValorUnitProd.Text);
                txtValorUnitProd.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorUnitProd, "");
                SomaValorUnitario();
            }
        }

        private void cadTransportadora_Click(object sender, EventArgs e)
        {
           
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
          
        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
           
        }

        private void btnFormPagamento_Click(object sender, EventArgs e)
        {
           
        }

        private void txtNOrcamento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Nº controle, código gerado automaticamente.";
        }

        private void txtValComissao_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Grava();
           
        }

        private void Grava()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {

                if (Validacoes())
                {
                    
                     _IDCOTACAOCOMPRA = COTACAOCOMPRAP.Save(Entity);

                    this.Cursor = Cursors.Default;
                    txtNControle.Text = _IDCOTACAOCOMPRA.ToString().PadLeft(6, '0');
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro ténico: " + ex.Message);

            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbFornecedor.SelectedValue) < 0)
            {
                errorProvider1.SetError(label14, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");                
                result = false;
            }           
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 0)
            {
                errorProvider1.SetError(label24, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(label5, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }           
            else
                errorProvider1.Clear();


            return result;
        }

        private void txtPorcDesconto_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
        {
      
        }

        private void txtTotalDesconto_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtPorcAcrescimo_Validating(object sender, CancelEventArgs e)
        {
      
        }

        private void txtTotalAcrescimo_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtTotalIPI_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtTotalDesconto_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtPorcAcrescimo_Enter(object sender, EventArgs e)
        {

        }

        private void txtTotalAcrescimo_Enter(object sender, EventArgs e)
        {
        }

        private void txtTotalIPI_Enter(object sender, EventArgs e)
        {
        }

        private void txtValorUnitProd_Enter(object sender, EventArgs e)
        {
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
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (rbCotacaoPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGCOTACAO", "System.String", "=", "S");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                else if (rbCompraPesquisa.Checked)
                {
                    filtroProfile = new RowsFiltro("FLAGCOTACAO", "System.String", "=", "N");
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
               

                if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                
                LIS_COTACAOCOMPRAColl = LIS_COTACAOCOMPRAP.ReadCollectionByParameter(Filtro, "IDCOTACAOCOMPRA DESC");

                //Colocando somatorio no final da lista
                LIS_COTACAOCOMPRAEntity LIS_COTACAOCOMPRATy = new LIS_COTACAOCOMPRAEntity();
                LIS_COTACAOCOMPRATy.TOTALCOTACAO = SumTotalPesquisa("TOTALCOTACAO");
                LIS_COTACAOCOMPRAColl.Add(LIS_COTACAOCOMPRATy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_COTACAOCOMPRAColl;

                lblTotalPesquisa.Text = (LIS_COTACAOCOMPRAColl.Count-1).ToString();
            }
            else
                PesquisaFiltro();


            PaintGrid();
            this.Cursor = Cursors.Default;
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_COTACAOCOMPRAEntity item in LIS_COTACAOCOMPRAColl)
                {

                    if (item.FLAGCOTACAO != null && item.FLAGCOTACAO.Trim() == "S")
                    {
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else if (item.FLAGCOTACAO != null && item.FLAGCOTACAO.Trim() == "N")
                    {
                        DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }                   

                    i++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                if (tabControlPedidoVenda.SelectedIndex == 2)
                {
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                }
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
                // referente ao tipo de campo
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_COTACAOCOMPRAColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_COTACAOCOMPRAColl;
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
                        filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                        filtroProfile = new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }

                    if (rbCotacaoPesquisa.Checked)
                    {
                        filtroProfile = new RowsFiltro("FLAGCOTACAO", "System.String", "=", "S");
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }
                    else if (rbCompraPesquisa.Checked)
                    {
                        filtroProfile = new RowsFiltro("FLAGCOTACAO", "System.String", "=", "N");
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }                  

                    if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
                    {
                        filtroProfile = new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus2.SelectedValue.ToString());
                        Filtro.Insert(Filtro.Count, filtroProfile);
                    }
                    

                    LIS_COTACAOCOMPRAColl = LIS_COTACAOCOMPRAP.ReadCollectionByParameter(Filtro, "IDCOTACAOCOMPRA DESC");

                    //Colocando somatorio no final da lista
                    LIS_COTACAOCOMPRAEntity LIS_COTACAOCOMPRAETy = new LIS_COTACAOCOMPRAEntity();
                    LIS_COTACAOCOMPRAETy.TOTALCOTACAO = SumTotalPesquisa("TOTALCOTACAO");
                    LIS_COTACAOCOMPRAColl.Add(LIS_COTACAOCOMPRAETy);

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_COTACAOCOMPRAColl;

                    lblTotalPesquisa.Text = (LIS_COTACAOCOMPRAColl.Count - 1).ToString();
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

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_COTACAOCOMPRAEntity item in LIS_COTACAOCOMPRAColl)
            {
                if (NomeCampo == "TOTALCOTACAO")
                    valortotal += Convert.ToDecimal(item.TOTALCOTACAO);
                
            }

            return valortotal;
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_COTACAOCOMPRAColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_COTACAOCOMPRAColl.Count.ToString();
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }     

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                Entity = null;
                Entity2 = null;

                ListaProdutoCotacao(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            
        }  


        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_COTACAOCOMPRAColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_COTACAOCOMPRAColl[indice].IDCOTACAOCOMPRA);

                if (e.KeyCode == Keys.Enter)
                {
                    tabControlPedidoVenda.SelectTab(0);
                    Entity = COTACAOCOMPRAP.Read(CodigoSelect);
                    ListaProdutoCotacao(_IDCOTACAOCOMPRA);
                    txtNControle.Focus();

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            COTACAOCOMPRAP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            btnPesquisa_Click(null, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
           
                Entity = null;
                Entity2 = null;
              
                ListaProdutoCotacao(-1);
                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
            
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
             Grava();
            
        }

        private void btnAddPecas_Click(object sender, EventArgs e)
        {
              GravaProduto();
        }       

        private void GravaProduto()
        {
            try
            {
                if (Validacoes() && ValidacoesProdutos())
                {
                    _IDCOTACAOCOMPRA = COTACAOCOMPRAP.Save(Entity);
                    txtNControle.Text = _IDCOTACAOCOMPRA.ToString().PadLeft(6, '0');
                    PRODUTOCOTACAOP.Save(Entity2);
                    
                   ListaProdutoCotacao(_IDCOTACAOCOMPRA);

                    //Salva Pedido
                    COTACAOCOMPRAP.Save(Entity);

                    Entity2 = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    
                    cbProduto.Focus();
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        private Boolean ValidacoesProdutos()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbProduto.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;

            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorUnitProd.Text))
            {
                errorProvider1.SetError(txtValorUnitProd, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanProduto.Text))
            {
                errorProvider1.SetError(txtQuanProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }           
            else if (rdCompra.Checked && CONFISISTEMAP.Read(1).ESTOQUENEGATIVO.TrimEnd() == "S")
            {
             
                if (Convert.ToDecimal(txtEstoqueAtual.Text) < Convert.ToDecimal(txtQuanProduto.Text))
                {
                    string Msgerro = "Estoque não pode ficar negativo!";
                    errorProvider1.SetError(txtQuanProduto, Msgerro);
                    Util.ExibirMSg(Msgerro, "Red");
                    result = false;
                }

            }
            else
                errorProvider1.Clear();


            return result;
        }
    
        private void ListaProdutoCotacao(int IDCOTACAOCOMPRA)
        {
            try
            {
                RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
                RowpProdPedido.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "=", IDCOTACAOCOMPRA.ToString()));

                LIS_PRODUTOCOTACAOColl = LIS_PRODUTOCOTACAOP.ReadCollectionByParameter(RowpProdPedido, "IDPRODUTOCOTACAO");
                
                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOCOTACAOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOCOTACAOColl.Count.ToString();

                SumTotalProdutosPedido();

                //Vai para ultima linha do grid
                if (LIS_PRODUTOCOTACAOColl.Count > 1)
                    DGDadosProduto.CurrentCell = DGDadosProduto.Rows[DGDadosProduto.Rows.Count - 1].Cells[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }      


        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOCOTACAOEntity item in LIS_PRODUTOCOTACAOColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalCotacao.Text = total.ToString("n2");            
        }      

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
        }

        private Boolean ValidacoesDelete()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (LIS_PRODUTOCOTACAOColl.Count > 0)
            {
                MessageBox.Show("Antes de Apagar a Cotação é necessário remover os Produtos!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(DGDadosProduto, ConfigMessage.Default.CampoObrigatorio);

                tabControlPedidoVenda.SelectTab(0);
                tabControl1.SelectTab(0);
                result = false;
            }                  
            else
                errorProvider1.Clear();


            return result;
        }

        private int ListaProdutoEntrega(int IDCOTACAOCOMPRA)
        {
            int result = 0;
            Filtro.Clear();
            Filtro.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "=", IDCOTACAOCOMPRA.ToString()));
            LIS_CONTROLEENTREGACollection LIS_CONTROLEENTREGAColl = new LIS_CONTROLEENTREGACollection();
            LIS_CONTROLEENTREGAProvider LIS_CONTROLEENTREGAP = new LIS_CONTROLEENTREGAProvider();
            LIS_CONTROLEENTREGAColl = LIS_CONTROLEENTREGAP.ReadCollectionByParameter(Filtro, "nomeproduto, DATAENTREGA");

            result = LIS_CONTROLEENTREGAColl.Count;

            return result;          
        }


        private void Delete()
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
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
                        COTACAOCOMPRAP.Delete(_IDCOTACAOCOMPRA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        
                        btnPesquisa_Click(null, null);
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro ténico: " + ex.Message);
                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            if (ValidacoesDelete())
                Delete();
        }

        private void DGDadosProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOCOTACAOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                      CodSelect = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTOCOTACAO);
                      Entity2 = PRODUTOCOTACAOP.Read(CodSelect);
                    
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTOCOTACAO);
                            decimal QUANTMOV = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].QUANTIDADE);
                            int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTO);
                            PRODUTOCOTACAOP.Delete(CodSelect);

                            ListaProdutoCotacao(_IDCOTACAOCOMPRA);
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

        private void lkComp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void AddProdutoComposicao(int IDCOMPOSICAO)
        {
            //Filtras os produtos da determinada composicao
            LIS_COMPOSPRODUTOProvider LIS_COMPOSPRODUTOP = new LIS_COMPOSPRODUTOProvider();
            LIS_COMPOSPRODUTOCollection LIS_COMPOSPRODUTOColl = new LIS_COMPOSPRODUTOCollection();

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDCOMPOSICAO", "System.String", "like", IDCOMPOSICAO.ToString()));
            LIS_COMPOSPRODUTOColl = LIS_COMPOSPRODUTOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

            //Percorre os produtos da coleção
            foreach (LIS_COMPOSPRODUTOEntity item in LIS_COMPOSPRODUTOColl)
            {
                cbProduto.SelectedValue = item.IDPRODUTO;
                txtQuanProduto.Text = Convert.ToDecimal(item.QUANTIDADE).ToString("n4");

                PRODORCAMENTOEntity PRODORCAMENTOTy = new PRODORCAMENTOEntity();
                PRODORCAMENTOTy.IDPRODUTO = item.IDPRODUTO;
                PRODORCAMENTOTy.QUANTIDADE = item.QUANTIDADE;
                

                //Obtem o valor de venda do produto
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSEntity PRODUTOStY = PRODUTOSP.Read(Convert.ToInt32(item.IDPRODUTO));
                txtValorUnitProd.Text = Convert.ToDecimal(PRODUTOStY.VALORVENDA1).ToString("n4");
                PRODORCAMENTOTy.VALORTOTAL = item.QUANTIDADE * PRODUTOStY.VALORVENDA1;

                //Salva o produtos no Pedido
                PRODUTOCOTACAOP.Save(Entity2);             
            }

            ////Lista os produtos do Pedido
            ListaProdutoCotacao(_IDCOTACAOCOMPRA);

            //Grava os dados do Pedido
            COTACAOCOMPRAP.Save(Entity);
           
        }

        private void bntCadMovim_Click(object sender, EventArgs e)
        {

        }

        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbProduto.SelectedIndex > 0)
            {
                PRODUTOSEntity PRODUTOS_EstoqTy = new PRODUTOSEntity();
                PRODUTOS_EstoqTy = PRODUTOSP.Read(Convert.ToInt32(cbProduto.SelectedValue));
                
                decimal ValorVenda = Convert.ToDecimal(PRODUTOS_EstoqTy.VALORVENDA1);               

                txtValorUnitProd.Text = ValorVenda.ToString("n2");
                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(PRODUTOS_EstoqTy.IDPRODUTO), false);
                txtEstoqueAtual.Text = Convert.ToDecimal(ESTOQUEATUAL).ToString("n2");
                SomaValorUnitario();
            }
        }

        private void cbFuncionario_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPorComisVend_Validating(object sender, CancelEventArgs e)
        {
            
        }        

  

        private void dataGridDupl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
        private void txtValorPago_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtValorDev_Validating(object sender, CancelEventArgs e)
        {
           
           
        }

        private void txtValorPago_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
          
        }

  

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
           
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
          }

     
      

        private void btnCaixa_Click(object sender, EventArgs e)
        {
           
        }

        private void geralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                orçamentoToolStripMenuItem_Click(null, null);
                   
            }
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlPedidoVenda.SelectTab(1);
            txtCriterioPesquisa.Focus();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
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

        int IndexRegistro = 0;
        int IndexRegistro2 = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private decimal SomaTotal()
        {
            decimal result = 0;
            int i = 1;
            foreach (LIS_COTACAOCOMPRAEntity item in LIS_COTACAOCOMPRAColl)
            {
                if (i < LIS_COTACAOCOMPRAColl.Count)
                    result += Convert.ToDecimal(item.TOTALCOTACAO);

                i++;
            }
            return result;
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
        }

        private void laserToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

       
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void geralComProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ImprimirListaGeralProduto()
        {
           
        }
      

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (LIS_COTACAOCOMPRAColl.Count > 0)
                {
                    string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                    if (orderBy.Trim() != string.Empty)
                    {
                        Phydeaux.Utilities.DynamicComparer<LIS_COTACAOCOMPRAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_COTACAOCOMPRAEntity>(orderBy);

                        LIS_COTACAOCOMPRAColl.Sort(comparer.Comparer);

                        DataGriewDados.DataSource = null;
                        DataGriewDados.DataSource = LIS_COTACAOCOMPRAColl;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void boletasToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }       

        private void viaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

      

        private void printDocument4_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void viaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void ImprimirDuplicata2Vias()
        {
           
        }

        private void printDocument5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void compostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ImprimirDuplicata1ViaComposta()
        {
           
        }

        private void printDocument6_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void txtQuanProduto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para fazer cálculo de M2 pressione Ctrl+Q e para M3 Ctrl+T";
        }

        private void txtQuanProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.Q))
            {
                using (FormMedQuadrada frm = new FormMedQuadrada())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        txtQuanProduto.Text = result.ToString("n4");
                }
            }
            else if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.T))
            {
                using (FormMedCubico frm = new FormMedCubico())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                        txtQuanProduto.Text = result.ToString("n4");
                }
            }
        }

        private void lnkSelecCliente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }


        private void orçamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }


        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_COTACAOCOMPRAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void cbProdutoMTQ_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o Produto ou pressione Ctrl+E para pesquisar.";
        }

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
                      

        }

        private void chkDetalProdFinal_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void txtValorTotal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMtLinearTotal.Text))
            {
                errorProvider1.SetError(txtValorMtLinearTotal, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtValorMtLinearTotal.Text);
                txtValorMtLinearTotal.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtValorMtLinearTotal, "");
            }
        }

        private void txtQuanProduto_Leave(object sender, EventArgs e)
        {
           
        }
       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                DialogResult dr = MessageBox.Show("Desejar excluir todos os produtos?",
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);



                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        foreach (LIS_PRODUTOCOTACAOEntity item in LIS_PRODUTOCOTACAOColl)
                        {
                            PRODUTOCOTACAOP.Delete(Convert.ToInt32(item.IDPRODUTOCOTACAO));
                        }

                        ListaProdutoCotacao(_IDCOTACAOCOMPRA);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                    }
                }           
        }

        private void DGDadosProduto_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (LIS_PRODUTOCOTACAOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTOCOTACAO);
                    Entity2 = PRODUTOCOTACAOP.Read(CodSelect);

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                  
                   
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                                {
                                    CodSelect = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTOCOTACAO);
                                    int IDPRODUTO = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].IDPRODUTO);
                                    decimal QUANTMOV = Convert.ToInt32(LIS_PRODUTOCOTACAOColl[rowindex].QUANTIDADE);
                                    PRODUTOCOTACAOP.Delete(CodSelect);
                                    ListaProdutoCotacao(_IDCOTACAOCOMPRA);

                                    Entity2 = null;

                                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                }
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

        }

        private void modelo2DiversosItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
       
        private void printDocument7_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        
        }

        private void printDocument7_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
        }

        private void printDocument_Prod3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void printDocument_Prod3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            

        }

        private void printDocumentRodape_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void btnCadCor_Click(object sender, EventArgs e)
        {
           
        }
     

        private void baixarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void produtosPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void pesquisaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }


        private void períodoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controleDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {        }


        private void reciboAvulsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void configuraçãoDeSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConfigSistema frm = new FrmConfigSistema())
            {
                frm.ShowDialog();
            }
        }

        private void vendasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void produtosMaisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(230, 160);
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
            msktDataInicial.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(230, 55);
            FormCalendario3.Name = "FrmCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void vendasDeProdutoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void modelo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void lnkSelecCliente_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }      
     

        private void duplicarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);

                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente duplicar esta cotação?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {

                        //Busca Produto
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "=", _IDCOTACAOCOMPRA.ToString()));
                        PRODUTOCOTACAOCollection PRODUTOCOTACAOColl = new PRODUTOCOTACAOCollection();
                        PRODUTOCOTACAOColl = PRODUTOCOTACAOP.ReadCollectionByParameter(RowRelatorio);

                        _IDCOTACAOCOMPRA = -1;
                        _IDCOTACAOCOMPRA = COTACAOCOMPRAP.Save(Entity);

                        //Salva Produtos Linear
                        foreach (PRODUTOCOTACAOEntity item in PRODUTOCOTACAOColl)
                        {
                            PRODUTOCOTACAOEntity PRODUTOCOTACAOTy = new PRODUTOCOTACAOEntity();
                            PRODUTOCOTACAOTy = PRODUTOCOTACAOP.Read(Convert.ToInt32(item.IDCOTACAOCOMPRA));
                            PRODUTOCOTACAOTy.IDPRODUTOCOTACAO = -1;
                            PRODUTOCOTACAOTy.IDCOTACAOCOMPRA = _IDCOTACAOCOMPRA;
                            PRODUTOCOTACAOP.Save(PRODUTOCOTACAOTy);
                        }

                        Entity = COTACAOCOMPRAP.Read(_IDCOTACAOCOMPRA);
                        ListaProdutoCotacao(_IDCOTACAOCOMPRA);

                        MessageBox.Show("Pedido Nº " + _IDCOTACAOCOMPRA.ToString().PadLeft(6, '0') + " criado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível duplicar o pedido selecionado!");
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
        }

        private void modelo3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
       

        private void vendasPorTransportadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void vendasPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void totalDeVendaPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void gerarNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void excluirItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void vendasDeProdutosPorCidadePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void txtTipoPagamentoDinheiro_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
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

        private void txtTipoPagamentoCheque_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
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

        private void txtTipoPagamentoCartaoDebito_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
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

        private void txtTipoPagamentoCartaoCredito_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
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
            if (LIS_COTACAOCOMPRAColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1); ;
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Pedidos";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else if (FORNECEDORP.Read(Convert.ToInt32(cbFornecedor.SelectedValue)).EMAILFORNECEDOR.Trim() == string.Empty)
            {
                MessageBox.Show("Email do fornecedor não selecionado!");
            }
            else
            {
                EnviarEmail();
            }
        }

        private void EnviarEmail()
        {
            try
            {
                FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(cbFornecedor.SelectedValue));

                CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESATy = EMPRESAP.Read(1);

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //var fromAddress = new MailAddress("imexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
               // var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                var toAddress = new MailAddress(FORNECEDORTy.EMAILFORNECEDOR, FORNECEDORTy.NOME);
              //  const string fromPassword = "rmr877701c#";
                const string fromPassword = "Rmr877701c";

                string subject = string.Empty;

                if (rdCotacao.Checked)
                    subject = "COTAÇÃO Nº " + txtNControle.Text;
                else
                    subject = "COMPRA " + txtNControle.Text;

                string body = string.Empty;
                body = "Caro(a) fornecedor " + FORNECEDORTy.NOME + ", segue abaixo dados da Cotação<br>";        
                body += " " + "<br>";
                body += "Dados do produtos" + "<br>";

                StringBuilder html = new StringBuilder();
                html.Append("<html>");
                html.Append("<head>");
                html.Append("<body>");
                html.Append("<table");
                //add header row
                html.Append("<tr>");
                html.Append("<td>Produto </td>");
                html.Append("<td>Total</td>");
                html.Append("<td>Valor</td>");
                html.Append("</tr>");
                //add rows
                foreach (LIS_PRODUTOCOTACAOEntity item in LIS_PRODUTOCOTACAOColl)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + item.NOMEPRODUTO + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.QUANTIDADE).ToString("n2") + "</td>");
                    html.Append("<td align=right >" + Convert.ToDecimal(item.VALORTOTAL).ToString("n2") + "</td>");
                    html.Append("</tr>");
                }

                html.Append("<tr>");
                html.Append("<td></td>");
                html.Append("<td>Total; </td>");
                html.Append("<td align=right >" + txtTotalCotacao.Text + "</td>");
                html.Append("</tr>");

                html.Append("</table>");

                html.Append("</head>");
                html.Append("</body>");
                html.Append("</html>");
                body += html;

                body += "<br>";
                body += "--------------------------------------------------------------------------------------------------" + "<br>";               

                body += "--------------------------------------------------------------------------------------------------" + "<br>";
                body += EMPRESATy.NOMEFANTASIA + "<br>";
                body += EMPRESATy.TELEFONE + "<br>";
                body += EMPRESATy.EMAIL + "<br>";

                Boolean seguranEmail = false;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "S")
                    seguranEmail = true;

                var smtp = new SmtpClient
                {
                    //Host = "mail.imexsistema.com.br",
                    Host = "smtp.site.com.br",
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = seguranEmail, 
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {

                    DialogResult dr = MessageBox.Show("Deseja enviar o email para: " + FORNECEDORTy.EMAILFORNECEDOR + " ?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        smtp.Send(message);
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Email enviado com sucesso!");
                    }

                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Não foi possível enviar o email!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information,
                             MessageBoxDefaultButton.Button1);


            }
        }

        private void completoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void resumoPorCentroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
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

        private void GetDropMensagem()
        {

            MENSAGEMCollection MENSAGEMColl = new MENSAGEMCollection();

            MENSAGEMColl = MENSAGEMP.ReadCollectionByParameter(null);

            cbMensagem.DisplayMember = "NOME";
            cbMensagem.ValueMember = "IDMENSAGEM";

            MENSAGEMEntity MENSAGEMTy = new MENSAGEMEntity();
            MENSAGEMTy.NOME = ConfigMessage.Default.MsgDrop;
            MENSAGEMTy.IDMENSAGEM = -1;
            MENSAGEMColl.Add(MENSAGEMTy);

            Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MENSAGEMEntity>(cbMensagem.DisplayMember);

            MENSAGEMColl.Sort(comparer.Comparer);
            cbMensagem.DataSource = MENSAGEMColl;

        }

        private void cbMensagem_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbMensagem.SelectedValue) > 0)
                txtObservacao.Text += " " + MENSAGEMP.Read(Convert.ToInt32(cbMensagem.SelectedValue)).MENSAGEM;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void vendaPorFormaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void vendasPorOutrosTipoDePagamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void sICOOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void arquivosDeRemessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRemessaBanco().ShowDialog();
        }

        private void sICOOBCNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void arquivosDeRemessaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void cNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void cNAB240ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }      

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_COTACAOCOMPRAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA);
                    tabControlPedidoVenda.SelectTab(0);
                    tabControl1.SelectTab(0);

                    Entity = COTACAOCOMPRAP.Read(CodigoSelect);
                    ListaProdutoCotacao(_IDCOTACAOCOMPRA);
                    txtNControle.Focus();


                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {

                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA.ToString().PadLeft(6, '0') + " - " + LIS_COTACAOCOMPRAColl[rowindex].NOMEFORNECEDOR,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                //Lista  Produto da Cotação                            
                               ListaProdutoCotacao(Convert.ToInt32(LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA));

                                //Exluir Produto da cotação
                               foreach (LIS_PRODUTOCOTACAOEntity item in LIS_PRODUTOCOTACAOColl)
                                {
                                    PRODUTOCOTACAOP.Delete(Convert.ToInt32(item.IDPRODUTOCOTACAO));
                                }

                               ListaProdutoCotacao(Convert.ToInt32(LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA));


                                CodigoSelect = Convert.ToInt32(LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA);
                                //Delete Pedido
                                COTACAOCOMPRAP.Delete(CodigoSelect);

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
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    try
                    {
                        CodigoSelect = Convert.ToInt32(LIS_COTACAOCOMPRAColl[rowindex].IDCOTACAOCOMPRA);
                        Entity = COTACAOCOMPRAP.Read(CodigoSelect);
                       ListaProdutoCotacao(_IDCOTACAOCOMPRA);

                        TSBPrint_Click(null, null);
                        this.Cursor = Cursors.Default;
                        Entity = null;
                        Entity2 = null;
                    }
                    catch (Exception)
                    {

                        this.Cursor = Cursors.Default;
                    } 

                }
            }
        }

        private void modelo4EconômicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(2);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                    ImprimirReceitaReport4();
                
            }
        }

        private void ImprimirReceitaReport4()
        {
            using (FrmRelatPedidoVendas2 frm = new FrmRelatPedidoVendas2())
            {
                COTACAOCOMPRAP.Save(Entity);
                frm.idcliente = Convert.ToInt32(cbFornecedor.SelectedValue);
           //     frm.IDCOTACAOCOMPRA = _IDCOTACAOCOMPRA;
                frm.ShowDialog();
             
            }
        }

        private void modeloEconômicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
                  
        }

        private void ImprimirReceitaReportEconomico()
        {
           
        }

        private void exportarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }   

        private void importarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
   

        private void utilitáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mkdDataVecto_Validated(object sender, EventArgs e)
        {
          
        }

        private void mkdDataVecto_Enter(object sender, EventArgs e)
        {
           

        }

        private void carnêDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void carnêToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void capaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                      
               
        }

        private void duplicatasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void extratosDeContasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExtratoDuplPagar Frm = new FrmExtratoDuplPagar();
            Frm.CodFornecedor = Convert.ToInt32(cbFornecedor.SelectedValue);
            Frm.ShowDialog();
        }

        private void totalDeProdutosPorPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTotalVendaporProduto frm = new FrmTotalVendaporProduto())
            {
                frm.ShowDialog();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
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
                RowRelatorio.Add(new RowsFiltro("NUMREFERENCIA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDCOTACAOCOMPRA", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                if(txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_COTACAOCOMPRAColl = LIS_COTACAOCOMPRAP.ReadCollectionByParameter(RowRelatorio, "IDCOTACAOCOMPRA DESC");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_COTACAOCOMPRAColl;

                    lblTotalPesquisa.Text = LIS_COTACAOCOMPRAColl.Count.ToString();
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

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void cbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void txtValorUnitProd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtValorMtLinearTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DGDadosProduto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCodPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCodPesquisa.Text.Trim() != string.Empty)
                    PesquisaProduto(txtCodPesquisa.Text);
            }

        }

        private void AbrePesquisaProduto()
        {
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
                        cbProduto.SelectedValue = result;
                        txtQuanProduto.Focus();
                    }
                }
            }
        }

        private void PesquisaProduto(string CODIGO)
        {
           //Verifica se numero inteiro
            LIS_PRODUTOSCollection LIS_PRODUTOSColl_Pesq = new LIS_PRODUTOSCollection();
            LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

            if (ValidacoesLibrary.ValidaTipoInt32(txtCodPesquisa.Text))
             {
                 //Pesquisa pelo codigo do produto
                 RowRelatorio.Clear();
                 RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", CODIGO.ToString()));
                 LIS_PRODUTOSColl_Pesq = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                 if(LIS_PRODUTOSColl_Pesq.Count > 0)
                 {
                     cbProduto.SelectedValue = LIS_PRODUTOSColl_Pesq[0].IDPRODUTO;
                     txtQuanProduto.Focus();
                 }
                 else 
                 {
                     //Pesquisa por código de referencia
                     RowRelatorio.Clear();
                     RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String" , "=", CODIGO.ToString()));
                     LIS_PRODUTOSColl_Pesq = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                     
                     if (LIS_PRODUTOSColl_Pesq.Count > 0)
                     {
                         cbProduto.SelectedValue = LIS_PRODUTOSColl_Pesq[0].IDPRODUTO;
                         txtQuanProduto.Focus();
                     }
                     else
                     {
                         AbrePesquisaProduto();
                     }
                 }
             }
            else
             {
                  //Pesquisa por código de referencia
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", CODIGO.ToString()));
                    LIS_PRODUTOSColl_Pesq = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                     
                    if (LIS_PRODUTOSColl_Pesq.Count > 0)
                    {
                        cbProduto.SelectedValue = LIS_PRODUTOSColl_Pesq[0].IDPRODUTO;
                        txtQuanProduto.Focus();
                    }
                    else
                    {
                       AbrePesquisaProduto();
                    }
             }
        }

        private void orçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_IDCOTACAOCOMPRA == -1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlPedidoVenda.SelectTab(1);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmRelatCotacaoCompra frm = new FrmRelatCotacaoCompra())
                {
                    frm._IDCOTACAOCOMPRA = _IDCOTACAOCOMPRA;
                    frm.ShowDialog();
                }
            }
        }

    }

}