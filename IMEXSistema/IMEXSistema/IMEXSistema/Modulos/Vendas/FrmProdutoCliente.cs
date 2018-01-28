using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSworks.UI;
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmProdutoCliente : Form
    {
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();

        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
        MARCACollection MarcaColl = new MARCACollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();                    

        LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

        Utility Util = new Utility();

        public FrmProdutoCliente()
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
            toolStripStatusLabel1.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmServicoCliente_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
           
            bntDateSelec.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
          
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnpdf2.Image = Util.GetAddressImage(17);
            btnExcel2.Image = Util.GetAddressImage(18);
            btnPrint2.Image = Util.GetAddressImage(19);

            GetDropProdutos();
            GetDropGrupoCategoria();
            GetDropMarca();
            GetDropCliente();
            GetFuncionario();

            mkdDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkdDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Busca o Funcionario logado
            USUARIOEntity USUARIOTY = new USUARIOEntity();
            USUARIOProvider USUARIOP = new USUARIOProvider();
            USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
            cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;
        }

        private void GetFuncionario()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCliente()
        {
            try
            {
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTECollection CLIENTEColl = new CLIENTECollection();
                CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null, "NOME");

                cbCliente.DisplayMember = "NOME";
                cbCliente.ValueMember = "IDCLIENTE";

                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                CLIENTETy.IDCLIENTE = -1;
                CLIENTEColl.Add(CLIENTETy);

                Phydeaux.Utilities.DynamicComparer<CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLIENTEEntity>(cbCliente.DisplayMember);

                CLIENTEColl.Sort(comparer.Comparer);
                cbCliente.DataSource = CLIENTEColl;

                cbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        public MonthCalendar monthCalendar1 = new MonthCalendar();
        Form FormCalendario1 = new Form();
        private void bntDateSelec_Click(object sender, EventArgs e)
        {
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar1_DateSelected);

            FormCalendario1.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario1.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario1.ClientSize = new System.Drawing.Size(220, 160);
            FormCalendario1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario1.Location = new Point(230, 55);
            FormCalendario1.Name = "FrmCalendario1";
            FormCalendario1.Text = "Calendário";
            FormCalendario1.ResumeLayout(false);
            FormCalendario1.Controls.Add(monthCalendar1);
            FormCalendario1.ShowDialog();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkdDataInicial.Text = monthCalendar1.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario1.Close();
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario2 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar2";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario2.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario2.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario2.ClientSize = new System.Drawing.Size(220, 160);
            FormCalendario2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario2.Location = new Point(230, 55);
            FormCalendario2.Name = "FrmCalendario2";
            FormCalendario2.Text = "Calendário";
            FormCalendario2.ResumeLayout(false);
            FormCalendario2.Controls.Add(monthCalendar2);
            FormCalendario2.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkdDataFinal.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario2.Close();
        }

        private void btnCadServico_Click(object sender, EventArgs e)
        {
            
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

        private void cbServico_KeyDown(object sender, KeyEventArgs e)
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
                    }
                }
            }
        }

        private void cbServico_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Selecione o produto ou pressione Ctrl+E para pesquisar.";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (ValidacoesItensServicos())
            {
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    RowRelatorio.Clear();
                    if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                         RowRelatorio.Add(new RowsFiltro("idproduto", "System.Int32", "=", cbProduto.SelectedValue.ToString()));

                    if(Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDGRUPOCATEGORIA", "System.Int32", "=", cbGrupoCategoria.SelectedValue.ToString()));

                    if (Convert.ToInt32(cbMarca.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", cbMarca.SelectedValue.ToString()));

                    if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkdDataInicial.Text) + " " + mkdHoraInicial.Text));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdDataFinal.Text)+ " " + mkdHoraFinal.Text));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    else if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                  
                    //Dados produto MT linear
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                    //Colocando somatorio no final da lista
                    LIS_PRODUTOSPEDIDOEntity LIS_PRODUTOSPEDIDOTy = new LIS_PRODUTOSPEDIDOEntity();
                    LIS_PRODUTOSPEDIDOTy.VALORTOTAL = SumTotalPesquisa("VALORTOTAL");
                    LIS_PRODUTOSPEDIDOTy.QUANTIDADE = SumTotalPesquisa("QUANTIDADE");
                    LIS_PRODUTOSPEDIDOTy.TOTALMT = SumTotalPesquisa("TOTALMT");                    
                    LIS_PRODUTOSPEDIDOColl.Add(LIS_PRODUTOSPEDIDOTy);

                    DGDadosServicos.AutoGenerateColumns = false;
                    DGDadosServicos.DataSource = LIS_PRODUTOSPEDIDOColl;

                    lbltTotalMtLinear.Text = (LIS_PRODUTOSPEDIDOColl.Count - 1).ToString();
                    ///////////////////////////////////////////////////////////////////////////////////////////

                      //Dados produto MT2
                    /////////////////////////////////////////////////////////////////////////////////////////// 
                   LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                    //Colocando somatorio no final da lista
                    LIS_PRODUTOSPEDIDOMTQEntity LIS_PRODUTOSPEDIDOMTQTy = new LIS_PRODUTOSPEDIDOMTQEntity();
                    LIS_PRODUTOSPEDIDOMTQTy.VALORTOTAL = SumTotalPesquisa2("VALORTOTAL");
                    LIS_PRODUTOSPEDIDOMTQTy.QUANTIDADE = SumTotalPesquisa2("QUANTIDADE");
                    LIS_PRODUTOSPEDIDOMTQTy.MT2 = SumTotalPesquisa2("MT2");
                    LIS_PRODUTOSPEDIDOMTQColl.Add(LIS_PRODUTOSPEDIDOMTQTy);

                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = LIS_PRODUTOSPEDIDOMTQColl;

                    lblTotalPesquisa.Text = (LIS_PRODUTOSPEDIDOMTQColl.Count - 1).ToString();

                    this.Cursor = Cursors.Default;	
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                if (NomeCampo == "VALORTOTAL")
                    valortotal += Convert.ToDecimal(item.VALORTOTAL);
                else if (NomeCampo == "QUANTIDADE")
                    valortotal += Convert.ToDecimal(item.QUANTIDADE);
                else if (NomeCampo == "TOTALMT")
                    valortotal += Convert.ToDecimal(item.TOTALMT);        
                
            }

            return valortotal;
        }

        private decimal SumTotalPesquisa2(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
            {
                if (NomeCampo == "VALORTOTAL")
                    valortotal += Convert.ToDecimal(item.VALORTOTAL);
                else if (NomeCampo == "QUANTIDADE")
                    valortotal += Convert.ToDecimal(item.QUANTIDADE);
                else if (NomeCampo == "MT2")
                    valortotal += Convert.ToDecimal(item.MT2);
                
            }

            return valortotal;
        }

        private Boolean ValidacoesItensServicos()
        {
            Boolean result = true;

            errorProvider1.Clear();            
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataInicial.Text))
            {
                errorProvider1.SetError(label42, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataFinal.Text))
            {
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            } 
            else
                errorProvider1.Clear();


            return result;
        }

        private void DGDadosServicos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PRODUTOSPEDIDOColl.Count > 0)
            {
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                    //Retira o ultimo registro
                    LIS_PRODUTOSPEDIDOColl.RemoveAt(LIS_PRODUTOSPEDIDOColl.Count - 1);

                    string orderBy = DGDadosServicos.Columns[e.ColumnIndex].DataPropertyName;
                    Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSPEDIDOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSPEDIDOEntity>(orderBy);

                    LIS_PRODUTOSPEDIDOColl.Sort(comparer.Comparer);

                    //Colocando somatorio no final da lista
                    LIS_PRODUTOSPEDIDOEntity LIS_PRODUTOSPEDIDOTy = new LIS_PRODUTOSPEDIDOEntity();
                    LIS_PRODUTOSPEDIDOTy.VALORTOTAL = SumTotalPesquisa("VALORTOTAL");
                    LIS_PRODUTOSPEDIDOColl.Add(LIS_PRODUTOSPEDIDOTy);

                    DGDadosServicos.AutoGenerateColumns = false;
                    DGDadosServicos.DataSource = null;

                    DGDadosServicos.AutoGenerateColumns = false;
                    DGDadosServicos.DataSource = LIS_PRODUTOSPEDIDOColl;

                    this.Cursor = Cursors.Default;	
                }
                catch (Exception)
                {
                    this.Cursor = Cursors.Default;	
                }

            }
        }

        private void DGDadosServicos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel1.Text = "Duplo click no cód. Cliente para consultar.";
        }

        private void DGDadosServicos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSPEDIDOColl.Count > 0 && rowindex > -1)
            {
                    int ColumnSelecionada = e.ColumnIndex;
                    int CodSelect = -1;

                    if (ColumnSelecionada == 3)//Cliente
                    {
                        CodSelect = Convert.ToInt32(LIS_PRODUTOSPEDIDOColl[rowindex].IDCLIENTE);

                        using (FrmCliente frm = new FrmCliente())
                        {
                            frm.CodClienteSelec = CodSelect;
                            frm.ShowDialog();
                        }
                    }

               }
        }

        private void mkdDataInicial_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkdDataInicial, ConfigMessage.Default.MsgDataInvalida);
            }    
        }

        private void mkdDataFinal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataFinal.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkdDataFinal, ConfigMessage.Default.MsgDataInvalida);
            }    
        }

        private void btnImprimir2_Click(object sender, EventArgs e)
        {
           
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbCliente.SelectedValue = result;
                }
            }    
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
        }

        private void btnpdf2_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Resumo do Caixa Diário (Produtos por Cliente)";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DGDadosServicos;
                frm.ShowDialog();
            }
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DGDadosServicos, "csv");
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo do Caixa Diário (Produtos por Cliente)");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosServicos, RelatorioTitulo, this.Name);
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relatório de Produtos por Cliente - MT2";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = dataGridView1;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(dataGridView1, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relatório de Produtos por Cliente - MT2");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dataGridView1, RelatorioTitulo, this.Name);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (chkImpressaoTicket.Checked)
            {
                PrintTicketModelo2();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo do Caixa Diário (Produtos por Cliente)");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DGDadosServicos, RelatorioTitulo, this.Name);
            }
        }


        private void PrintTicketModelo2()
        {
            try
            {
                Ticket ticket = new Ticket();
                decimal TotalGeralRel = 0;

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("Resumo do Caixa Diário por Produtos");
                ticket.AddSubHeaderLine("Vendedor: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(mkdDataInicial.Text + " a " + mkdDataFinal.Text);

                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {
                    if (item.IDPEDIDO != null)
                    {
                        string ValorTotal = Convert.ToDecimal(item.VALORTOTAL).ToString("n2");
                        TotalGeralRel += Convert.ToDecimal(item.VALORTOTAL);
                        ticket.AddItem(item.QUANTIDADE.ToString(), item.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                    }
                }

                ticket.AddTotal("Total Geral", TotalGeralRel.ToString("n2"));
                
                if (ticket.PrinterExists(BmsSoftware.ConfigSistema1.Default.impressoraticket))
                    ticket.PrintTicket(BmsSoftware.ConfigSistema1.Default.impressoraticket); //Nome da impresora , o caminho completo
                else
                    MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
            }

        }

        private void mkdHoraInicial_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraInicial.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraInicial, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void mkdHoraFinal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraFinal.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraFinal.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraFinal, ConfigMessage.Default.MsgErroHora);
            }
        }
    }
}
