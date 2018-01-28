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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmProdutoClientePedidoSimples : Form
    {
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
        MARCACollection MarcaColl = new MARCACollection();

        STATUSProvider STATUSP = new STATUSProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

        Utility Util = new Utility();

        public FrmProdutoClientePedidoSimples()
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
            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
             
                bntDateSelec.Image = Util.GetAddressImage(11);
                bntDateSelecFinal.Image = Util.GetAddressImage(11);
                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);

                btnPesquisa.Image = Util.GetAddressImage(20);
                btnImprimir.Image = Util.GetAddressImage(19);
                btnSair.Image = Util.GetAddressImage(21);

                bntDateSelec.Image = Util.GetAddressImage(22);
                bntDateSelecFinal.Image = Util.GetAddressImage(22);

                GetDropProdutos();
                GetDropGrupoCategoria();
                GetDropMarca();
                GetDropCliente();
                GetFuncionario();
                GetSupervisor();
                GetDropStatus();
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
                RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
                RowsFiltroCollection Filtro = new RowsFiltroCollection();

                Filtro.Insert(0, FiltroProfile);

               
                STATUSCollection STATUSColl2 = new STATUSCollection();
                STATUSColl2 = STATUSP.ReadCollectionByParameter(Filtro);

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUS";

                STATUSEntity STATUSTy = new STATUSEntity();
                STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTy.IDSTATUS = -1;
                STATUSColl2.Add(STATUSTy);

                Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus.DisplayMember);

                STATUSColl2.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSColl2;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetSupervisor()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbSupervisor.DisplayMember = "NOME";
                cbSupervisor.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbSupervisor.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbSupervisor.DataSource = FUNCIONARIOColl;

                cbSupervisor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetFuncionario()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCliente()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
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

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropMarca()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropGrupoCategoria()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
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

                this.Cursor = Cursors.Default;	
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;	
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSPEDIDOColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                errorProvider1.SetError(DGDadosServicos, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relatório de produtos por Cliente  - Data Inicial: " + mkdDataInicial.Text +  " - Data Final: " +mkdDataFinal.Text);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DGDadosServicos, RelatorioTitulo, this.Name);
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (ValidacoesItensServicos())
            {
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                    RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                    
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

                    if (Convert.ToInt32(cbSupervisor.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSUPERVISOR", "System.Int32", "=", cbSupervisor.SelectedValue.ToString()));

                    if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", cbStatus.SelectedValue.ToString()));


                    if (mkdDataInicial.Text != "  /  /")
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkdDataInicial.Text)));

                    if (mkdDataFinal.Text != "  /  /")
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdDataFinal.Text)));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    else if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    
                    ///////////////////////////////////////////////////////////////////////////////////////////

                    if (rbOrdenarPedido.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDO desc");
                    else if (rbOrdenarProduto.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                     else if (rbDataEmissao.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO");
                    else if (rbDtVecto.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
                        
                    
                    LIS_PRODUTOSPEDIDOEntity LIS_PRODUTOSPEDIDOTy = new LIS_PRODUTOSPEDIDOEntity();
                    LIS_PRODUTOSPEDIDOTy.VALORTOTAL = SumTotalPesquisa("VALORTOTAL");
                    LIS_PRODUTOSPEDIDOTy.QUANTIDADE = SumTotalPesquisa("QUANTIDADE");
                    LIS_PRODUTOSPEDIDOColl.Add(LIS_PRODUTOSPEDIDOTy);
  
                    DGDadosServicos.AutoGenerateColumns = false;
                    DGDadosServicos.DataSource = LIS_PRODUTOSPEDIDOColl;
                    
                    lbltTotalMtLinear.Text = (LIS_PRODUTOSPEDIDOColl.Count - 1).ToString();
                

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
            }

            return valortotal;
        }

        private Boolean ValidacoesItensServicos()
        {
            Boolean result = true;

            //errorProvider1.Clear();
            ////if (Convert.ToInt32(cbProduto.SelectedValue) < 1)
            ////{
            ////    errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
            ////    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            ////    result = false;

            ////}
            ////else 
            //    if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataInicial.Text))
            //{
            //    errorProvider1.SetError(mkdDataInicial, ConfigMessage.Default.CampoObrigatorio);
            //    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            //    result = false;
            //}
            //else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdDataFinal.Text))
            //{
            //    errorProvider1.SetError(mkdDataFinal, ConfigMessage.Default.CampoObrigatorio);
            //    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            //    result = false;
            //} 
            //else
            //    errorProvider1.Clear();


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

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos por Cliente");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DGDadosServicos, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DGDadosServicos, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Produtos por Cliente";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DGDadosServicos;
                frm.ShowDialog();
            }
        }

        private void DGDadosServicos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{
            //    if (e.Value != null)
            //    {
            //        int? IDStatus = LIS_PRODUTOSPEDIDOColl[e.RowIndex].IDSTATUS;
            //        if (IDStatus > 0)
            //        {
            //            string NomeStatus = STATUSP.Read(Convert.ToInt32(IDStatus)).NOME;
            //            DGDadosServicos.Rows[e.RowIndex].Cells[7].Value = NomeStatus;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro na busca do estoque!");
            //    MessageBox.Show("Erro técnico: " + ex.Message);
            //}
        }
    }
}
