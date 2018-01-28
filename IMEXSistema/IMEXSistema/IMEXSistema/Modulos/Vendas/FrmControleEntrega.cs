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
using BMSSoftware.Modulos.Cadastros;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmControleEntrega : Form
    {
        Utility Util = new Utility();

        PEDIDOEntity PEDIDOTY = new PEDIDOEntity();
        CLIENTEEntity CLIENTETy = new CLIENTEEntity();
        
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_CONTROLEENTREGACollection LIS_CONTROLEENTREGAColl = new LIS_CONTROLEENTREGACollection();

        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();
        CONTROLEENTREGAProvider CONTROLEENTREGAP = new CONTROLEENTREGAProvider();

        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        LIS_CONTROLEENTREGAProvider LIS_CONTROLEENTREGAP = new LIS_CONTROLEENTREGAProvider();

        public int _IDPEDIDO = -1;
        public string DatePedido = string.Empty;

        RowsFiltroCollection RowsFiltroColl = new RowsFiltroCollection();

        public FrmControleEntrega()
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
      

        int _IDCONTROLEENTREGA = -1;
        public CONTROLEENTREGAEntity Entity2
        {
            get
            {
                DateTime DATAPEDIDO = Convert.ToDateTime(mkdtPedido.Text);
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
                int IDPRODUTO = Convert.ToInt32(cbProduto.SelectedValue);
                decimal QUANTPEDIDO = Convert.ToDecimal(txtQuantPedido.Text);
                decimal QUANTENTREGUE  = Convert.ToDecimal(txtQuanEntregue.Text);
                decimal QUANTRESTANTE = 0;
                DateTime DATAENTREGA = Convert.ToDateTime(msktDataEntrega.Text);

                return new CONTROLEENTREGAEntity(_IDCONTROLEENTREGA, _IDPEDIDO, DATAPEDIDO, IDFUNCIONARIO, IDPRODUTO,
                                            QUANTPEDIDO, QUANTENTREGUE, QUANTRESTANTE,  DATAENTREGA);
            }
            set
            {

                if (value != null)
                {
                    _IDCONTROLEENTREGA = value.IDCONTROLEENTREGA;
                    _IDPEDIDO = Convert.ToInt32(value.IDPEDIDO);
                    mkdtPedido.Text = Convert.ToDateTime(value.DATAPEDIDO).ToString("dd/MM/yyyy");
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    cbProduto.SelectedValue = value.IDPRODUTO;
                    txtQuantPedido.Text = Convert.ToDecimal(value.QUANTPEDIDO).ToString("n2");
                    txtQuanEntregue.Text = Convert.ToDecimal(value.QUANTENTREGUE).ToString("n2");
                    msktDataEntrega.Text = Convert.ToDateTime(value.DATAENTREGA).ToString("dd/MM/yyyy");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDCONTROLEENTREGA = -1;
                    txtQuanEntregue.Text = "0,00";
                    cbProduto.SelectedValue = -1;
                    msktDataEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
      

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToDecimal(txtQuanEntregue.Text) <= 0 )
            {
                errorProvider1.SetError(txtQuanEntregue, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbFuncionario.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataEntrega.Text == "  /  /")
            {
                errorProvider1.SetError(msktDataEntrega, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }    
           
            else
                errorProvider1.Clear();


            return result;
        }

        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            cbVendedor.Image = Util.GetAddressImage(6);          

            txtCodPedido.Text = _IDPEDIDO.ToString();
            mkdtPedido.Text = DatePedido;
            ListaProdutoEntrega(_IDPEDIDO);
            
            GetDropProdutos();
            GetFuncionario();

            msktDataEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");

            this.Cursor = Cursors.Default;
        }

        private void GetDropProdutos()
        {
            RowsFiltroColl.Clear();
            RowsFiltroColl.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowsFiltroColl);

            cbProduto.DisplayMember = "NOMEPRODUTO";
            cbProduto.ValueMember = "IDPRODUTO";

            LIS_PRODUTOSPEDIDOEntity PRODUTOSTy = new LIS_PRODUTOSPEDIDOEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            LIS_PRODUTOSPEDIDOColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSPEDIDOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSPEDIDOEntity>(cbProduto.DisplayMember);

            LIS_PRODUTOSPEDIDOColl.Sort(comparer.Comparer);
            cbProduto.DataSource = LIS_PRODUTOSPEDIDOColl;

            cbProduto.SelectedIndex = 0;

          //  int indx = 0;
          //  cbProduto.Items.Add(ConfigMessage.Default.MsgDrop);
          //  foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
          //  {
          ////      cbProduto.Items.Add(item.NOMEPRODUTO + " - " + indx.ToString());
          //      indx++;
//}
            
        }      
      

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
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
    
        private void cbProduto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbProduto.SelectedIndex) > 0)
            {
                //txtQuantPedido.Text = LIS_PRODUTOSPEDIDOColl[cbProduto.SelectedIndex -1].QUANTIDADE.ToString();
                txtQuantPedido.Text = LIS_PRODUTOSPEDIDOColl[cbProduto.SelectedIndex].QUANTIDADE.ToString();
                decimal TotalEntregue = CalcEntregue(Convert.ToInt32(cbProduto.SelectedValue));

                if (txtQuantPedido.Text == string.Empty)
                    txtQuantPedido.Text = "0";

                decimal totalpedido = Convert.ToDecimal(txtQuantPedido.Text);
                decimal rest = totalpedido - TotalEntregue;
                txtQuantPend.Text = rest.ToString();
            }
            else
            {
                txtQuantPedido.Text = "0,00";
                txtQuantPend.Text = "0,00";
            }
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
        }

        private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncionario.DataSource = FUNCIONARIOColl;

            cbFuncionario.SelectedIndex = 0;
        }

        private void msktDataEmissao_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataEntrega.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(msktDataEntrega, ConfigMessage.Default.MsgDataInvalida);
            }
        }

        private void txtQuanProduto_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanEntregue.Text))
            {
                errorProvider1.SetError(txtQuanEntregue, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacoes())
                {
                    decimal TotalEntregue = CalcEntregue(Convert.ToInt32(cbProduto.SelectedValue));
                    decimal totalpedido = Convert.ToDecimal(txtQuantPedido.Text);
                    decimal rest = totalpedido - TotalEntregue;

                    if (Convert.ToDecimal(txtQuanEntregue.Text) > rest)
                    {
                        string msgErro = "A quantidade de entrega está fora do limite!";
                        MessageBox.Show(msgErro,
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

                        errorProvider1.SetError(txtQuanEntregue,msgErro);
                    }
                    else
                    {
                        CONTROLEENTREGAP.Save(Entity2);
                        ListaProdutoEntrega(_IDPEDIDO);
                        Entity2 = null;
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void ListaProdutoEntrega(int IDPEDIDO)
        {
            RowsFiltroColl.Clear();
            RowsFiltroColl.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
            LIS_CONTROLEENTREGAColl = LIS_CONTROLEENTREGAP.ReadCollectionByParameter(RowsFiltroColl, "nomeproduto, DATAENTREGA desc");

          
            DGDadosProduto.AutoGenerateColumns = false;
            DGDadosProduto.DataSource = LIS_CONTROLEENTREGAColl;
            lblNumRegistros.Text = "Nº de Registros: " + LIS_CONTROLEENTREGAColl.Count.ToString();
        }

        private void DGDadosProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (LIS_CONTROLEENTREGAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if(ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_CONTROLEENTREGAColl[rowindex].IDCONTROLEENTREGA);
                       
                            CONTROLEENTREGAP.Delete(CodSelect);
                            ListaProdutoEntrega(_IDPEDIDO);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Entity2 = null;
        }
        

        int IndexRegistro = 0;
        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Inserir um ultimo registro
                LIS_CONTROLEENTREGAEntity LIS_CONTROLEENTREGATy = new LIS_CONTROLEENTREGAEntity();
                LIS_CONTROLEENTREGATy.IDPEDIDO = -1;
                LIS_CONTROLEENTREGAColl.Add(LIS_CONTROLEENTREGATy);

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
                e.Graphics.DrawString("DT. Entregue", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Produto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 120, 170);
                e.Graphics.DrawString("Qt. Entregue", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
                e.Graphics.DrawString("Funcionário", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 500, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_CONTROLEENTREGAColl.Count;

                //Alinhamento dos valores
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_CONTROLEENTREGAColl.Count)
                {
                    if(LIS_CONTROLEENTREGAColl[IndexRegistro].IDPEDIDO != -1)
                    {
                           config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                           e.Graphics.DrawString(Convert.ToDateTime(LIS_CONTROLEENTREGAColl[IndexRegistro].DATAENTREGA).ToString("dd/MM/yyyy"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                           e.Graphics.DrawString(Util.LimiterText(LIS_CONTROLEENTREGAColl[IndexRegistro].NOMEPRODUTO, 30), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                           e.Graphics.DrawString(Convert.ToDecimal(LIS_CONTROLEENTREGAColl[IndexRegistro].QUANTENTREGUE).ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha);
                           e.Graphics.DrawString(Util.LimiterText(LIS_CONTROLEENTREGAColl[IndexRegistro].NOMEFUNCIONARIO, 30) , config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 500, config.PosicaoDaLinha);
                    }

                    if (LIS_CONTROLEENTREGAColl[IndexRegistro].IDPEDIDO != -1 && (LIS_CONTROLEENTREGAColl[IndexRegistro].IDPRODUTO != LIS_CONTROLEENTREGAColl[IndexRegistro + 1].IDPRODUTO))
                    {
                        e.Graphics.DrawString("Total do Pedido: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha + 15);
                        e.Graphics.DrawString(Convert.ToDecimal(LIS_CONTROLEENTREGAColl[IndexRegistro].QUANTPEDIDO).ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda +220, config.PosicaoDaLinha + 15);


                              //Total entregue
                        e.Graphics.DrawString("Total Entregue: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 280, config.PosicaoDaLinha + 15);
                        decimal TotalEntregue = CalcEntregue(Convert.ToInt32(LIS_CONTROLEENTREGAColl[IndexRegistro].IDPRODUTO));
                        e.Graphics.DrawString( Convert.ToDecimal(TotalEntregue).ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 15);

                       
                        e.Graphics.DrawString("Total Rest: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha + 15);
                        e.Graphics.DrawString(Convert.ToDecimal(LIS_CONTROLEENTREGAColl[IndexRegistro].QUANTPEDIDO - TotalEntregue).ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda +550, config.PosicaoDaLinha + 15);

                        config.LinhaAtual++;
                        config.LinhaAtual++;                      
                    }
                    

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }

                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_CONTROLEENTREGAColl.Count)
                    e.HasMorePages = true;
                else
                {
                    //Soma 
                   // e.Graphics.DrawString("Total: " + SomaTotal().ToString("n2"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 700, config.PosicaoDaLinha + 60, stringFormat);

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                   //e.Graphics.DrawString("Total da pesquisa: " + (LIS_CONTROLEENTREGAColl.Count - 1), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);


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

        private decimal CalcEntregue(int idproduto)
        {
            decimal result = 0;

            RowsFiltroColl.Clear();
            RowsFiltroColl.Add(new RowsFiltro("idproduto", "System.Int32", "=", idproduto.ToString()));
            RowsFiltroColl.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", _IDPEDIDO.ToString()));
            
            LIS_CONTROLEENTREGACollection LIS_CONTROLEENTREGA2Coll = new LIS_CONTROLEENTREGACollection();
            LIS_CONTROLEENTREGA2Coll = LIS_CONTROLEENTREGAP.ReadCollectionByParameter(RowsFiltroColl);

            foreach (LIS_CONTROLEENTREGAEntity item in LIS_CONTROLEENTREGA2Coll)
            {
                result += Convert.ToDecimal(item.QUANTENTREGUE);
            }

            return result;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void completoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PEDIDOTY = PEDIDOP.Read(_IDPEDIDO);
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Entrega  - Pedido: " + txtCodPedido.Text + " Cliente: " + CLIENTEP.Read(Convert.ToInt32(PEDIDOTY.IDCLIENTE)).NOME);
            ////define o titulo do relatorio
            IndexRegistro = 0;

            try
            {
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (LIS_CONTROLEENTREGAColl.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Desejar excluir todos os produtos?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);



                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        foreach (LIS_CONTROLEENTREGAEntity item in LIS_CONTROLEENTREGAColl)
                        {
                            CONTROLEENTREGAP.Delete(Convert.ToInt32(item.IDCONTROLEENTREGA));
                        }

                        ListaProdutoEntrega(_IDPEDIDO);

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe produto cadastrado!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }

        private void cbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void txtQuanEntregue_Enter(object sender, EventArgs e)
        {
           
        }
    }
}
