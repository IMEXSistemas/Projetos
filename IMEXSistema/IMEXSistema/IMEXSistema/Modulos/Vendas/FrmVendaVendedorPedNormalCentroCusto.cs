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
using VVX;
using BMSworks.UI;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmVendaVendedorPedNormalCentroCusto : Form
    {
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaVendedorPedNormalCentroCusto()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

           
            GetDropStatus();
            GetDropCentroCusto();            
        }     

        private void GetDropCentroCusto()
        {
            try
            {
                CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
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

        private void GetDropStatus()
        {
            //11 Pedido de Venda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            STATUSCollection STATUSColl = new STATUSCollection();
            STATUSColl = STATUSP.ReadCollectionByParameter(Filtro);

            STATUSEntity STATUSTy = new STATUSEntity();
            STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
            STATUSTy.IDSTATUS = -1;
            STATUSColl.Add(STATUSTy);

            Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus.DisplayMember);

            STATUSColl.Sort(comparer.Comparer);
            cbStatus.DataSource = STATUSColl;
            cbStatus.SelectedIndex = 0;
        }
   
        decimal TotalGeralPedido = 0;
        decimal TotalGeralCliente = 0;
        decimal TotalGeralComissao = 0;
        decimal TotalSubGeralPedido = 0;
        decimal TotalSubGeralComissao = 0;
        private void PreencheGrid()
        {
            TotalGeralPedido = 0;
            TotalGeralCliente = 0;
            TotalGeralComissao = 0;
            TotalSubGeralPedido = 0;
            TotalSubGeralComissao = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();        
         
            foreach (var LIS_PEDIDOTy in LIS_PEDIDOColl)
            {
                if (LIS_PEDIDOTy.IDCENTROCUSTOS != null && LIS_PEDIDOTy.IDCENTROCUSTOS > 0)
                {
                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, "Centro de Custo: " +LIS_PEDIDOTy.CENTROCUSTO, string.Empty, string.Empty, string.Empty, string.Empty);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row2);


                    //Filtra os vendedores do centro de custo
                    LIS_PEDIDOCollection LIS_PEDIDOColl_Vendedor= new BMSworks.Model.LIS_PEDIDOCollection();
                    LIS_PEDIDOColl_Vendedor = VendedorRel(Convert.ToInt32(LIS_PEDIDOTy.IDCENTROCUSTOS));

                    //Elimina os vendedores repetidos
                    LIS_PEDIDOCollection LIS_PEDIDOColl_Vendedor2 = new LIS_PEDIDOCollection();
                    foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl_Vendedor)
                    {

                        if (LIS_PEDIDOColl_Vendedor2.Find(delegate(LIS_PEDIDOEntity item2) { return (item2.IDVENDEDOR == item.IDVENDEDOR); }) == null)
                        {
                            LIS_PEDIDOColl_Vendedor2.Add(item);
                        }
                    }
                    LIS_PEDIDOColl_Vendedor = LIS_PEDIDOColl_Vendedor2;                 

                    //Exibi todos os vendedores
                    foreach (LIS_PEDIDOEntity itemVend in LIS_PEDIDOColl_Vendedor)
                    {
                        if (itemVend.IDVENDEDOR != null && itemVend.IDVENDEDOR > 0)
                        {
                            DataGridViewRow row2_2 = new DataGridViewRow();
                            row2_2.CreateCells(DataGriewDados, "Vendedor: " + itemVend.NOMEVENDEDOR, string.Empty);
                            row2_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row2_2);

                            //Filtra os Cliente por vendedor
                            LIS_PEDIDOCollection LIS_PEDIDOColl_Cliente= new BMSworks.Model.LIS_PEDIDOCollection();
                            LIS_PEDIDOColl_Cliente = ClienteRel(Convert.ToInt32(itemVend.IDVENDEDOR), Convert.ToInt32(itemVend.IDCENTROCUSTOS));

                            //Elimina os Cliente repetidos
                            LIS_PEDIDOCollection LIS_PEDIDOColl_Cliente2 = new LIS_PEDIDOCollection();
                            foreach (LIS_PEDIDOEntity item_1 in LIS_PEDIDOColl_Cliente)
                            {

                                if (LIS_PEDIDOColl_Cliente2.Find(delegate(LIS_PEDIDOEntity item2_2) { return (item2_2.IDCLIENTE == item_1.IDCLIENTE); }) == null)
                                {
                                    LIS_PEDIDOColl_Cliente2.Add(item_1);
                                }
                            }
                            LIS_PEDIDOColl_Cliente = LIS_PEDIDOColl_Cliente2;

                            //Cabeçalho com o nome do Cliente
                            DataGridViewRow rowTop_3 = new DataGridViewRow();
                            rowTop_3.CreateCells(DataGriewDados, "Cliente", "Total Pedido" );
                            rowTop_3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(rowTop_3);
                            foreach (LIS_PEDIDOEntity item_3 in LIS_PEDIDOColl_Cliente)
                            {
                                if (item_3.IDCLIENTE != null && item_3.IDCLIENTE > 0)
                                {

                                    //Total por cliente
                                    TotalGeralCliente += TotalCliente(Convert.ToInt32(item_3.IDCLIENTE), Convert.ToInt32(item_3.IDVENDEDOR), Convert.ToInt32(item_3.IDCENTROCUSTOS));
                                    TotalGeralComissao += TotalClienteComissao(Convert.ToInt32(item_3.IDCLIENTE), Convert.ToInt32(item_3.IDVENDEDOR), Convert.ToInt32(item_3.IDCENTROCUSTOS));

                                    DataGridViewRow row2_3 = new DataGridViewRow();
                                    row2_3.CreateCells(DataGriewDados, item_3.NOMECLIENTE, TotalGeralCliente.ToString("n2"));
                                    row2_3.DefaultCellStyle.Font = new Font("Arial", 8);
                                    DataGriewDados.Rows.Add(row2_3); 
                                }

                                TotalSubGeralPedido += TotalGeralCliente;
                                TotalSubGeralComissao += TotalGeralComissao;

                                TotalGeralCliente = 0;
                                TotalGeralComissao = 0;
                              
                            }

                            //SubGeral do PEdido
                            DataGridViewRow row2_4 = new DataGridViewRow();
                            row2_4.CreateCells(DataGriewDados, "Sub Total: ", TotalSubGeralPedido.ToString("n2"));
                            row2_4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row2_4);

                            TotalGeralPedido += TotalSubGeralPedido;
                            TotalSubGeralPedido = 0;
                            TotalSubGeralComissao = 0;
                          

                            //Rodape do Vendedor
                            DataGridViewRow rowRodape_2 = new DataGridViewRow();
                            rowRodape_2.CreateCells(DataGriewDados, "_____________________________________________________________________", "___________");
                            rowRodape_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(rowRodape_2);
                        }
                     
                    }

                    //Rodape do centro de custo
                    DataGridViewRow rowRodape = new DataGridViewRow();
                    rowRodape.CreateCells(DataGriewDados, "Total Geral: ", TotalGeralPedido.ToString("n2"));
                    rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape);

                }

                //Rodape Final
                DataGridViewRow rowRodape_3 = new DataGridViewRow();
                rowRodape_3.CreateCells(DataGriewDados, "_____________________________________________________________________", "___________");
                rowRodape_3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowRodape_3);
              
            }          


            this.Cursor = Cursors.Default;
        }

        private decimal TotalCliente(int IDCLIENTE, int IDVENDEDOR, int IDCENTROCUSTOS)
        {
            decimal total = 0;
            LIS_PEDIDOCollection LIS_PEDIDOColl_Vendedor = new LIS_PEDIDOCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", IDVENDEDOR.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", IDCENTROCUSTOS.ToString()));

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
            
            LIS_PEDIDOColl_Vendedor = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_PEDIDOEntity item_3 in LIS_PEDIDOColl_Vendedor)
            {
                total += Convert.ToDecimal(item_3.TOTALPEDIDO);
            }

            return total;
        }

        private decimal TotalClienteComissao(int IDCLIENTE, int IDVENDEDOR, int IDCENTROCUSTOS)
        {
            decimal total = 0;
            LIS_PEDIDOCollection LIS_PEDIDOColl_Vendedor = new LIS_PEDIDOCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", IDVENDEDOR.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", IDCENTROCUSTOS.ToString()));

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

            LIS_PEDIDOColl_Vendedor = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_PEDIDOEntity item_3 in LIS_PEDIDOColl_Vendedor)
            {
                total += Convert.ToDecimal(item_3.VALORCOMISSAO);
            }

            return total;
        }
     
        private LIS_PEDIDOCollection VendedorRel(int IDCENTROCUSTOS)
        {
            LIS_PEDIDOCollection LIS_PEDIDOColl_Vendedor = new LIS_PEDIDOCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", IDCENTROCUSTOS.ToString()));

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

            LIS_PEDIDOColl_Vendedor = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "NOMEVENDEDOR");

            return LIS_PEDIDOColl_Vendedor;
        }

        private LIS_PEDIDOCollection ClienteRel(int IDVENDEDOR, int IDCENTROCUSTOS)
        {
            LIS_PEDIDOCollection LIS_PEDIDOColl_ClienteRel = new LIS_PEDIDOCollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", IDVENDEDOR.ToString()));
            RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", IDCENTROCUSTOS.ToString()));

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
            
            LIS_PEDIDOColl_ClienteRel = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio,"NOMEVENDEDOR");

            return LIS_PEDIDOColl_ClienteRel;
        }       
       
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas por Vendedor - Resumo Centro de Custo");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);            
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

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", Convert.ToInt32(cbCentroCusto.SelectedValue).ToString()));
               
                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=","S"));
                    else if (rdOrcamento.Checked)
                            RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));


                    LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "CENTROCUSTO");

                    //Elimina os centros de custo repetidos
                    LIS_PEDIDOCollection LIS_PEDIDOColl2 = new LIS_PEDIDOCollection();
                    foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
                    {

                        if (LIS_PEDIDOColl2.Find(delegate(LIS_PEDIDOEntity item2) { return (item2.IDCENTROCUSTOS == item.IDCENTROCUSTOS); }) == null)
                        {
                            LIS_PEDIDOColl2.Add(item);
                        }
                    }

                    LIS_PEDIDOColl = LIS_PEDIDOColl2;
                    
                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: "  + EX.Message);
                }
            }
        }


        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            //if (Convert.ToInt32(cbFuncionario.SelectedValue) < 0)
            //{
            //    errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
            //    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            //    result = false;
            //}
            //else
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text))
            {
                errorProvider1.SetError(msktDataInicial, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
            {
                errorProvider1.SetError(msktDataFinal, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Vendas por Vendedor - Resumo Centro de Custo";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas por Vendedor - Resumo Centro de Custo");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
      
    }
}
