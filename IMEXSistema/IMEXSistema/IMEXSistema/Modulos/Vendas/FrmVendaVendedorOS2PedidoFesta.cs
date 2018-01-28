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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmVendaVendedorOS2PedidoFesta : Form
    {
        LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl = new LIS_SERVICOPEDIDOFESTACollection();
        LIS_SERVICOPEDIDOFESTAProvider LIS_SERVICOPEDIDOFESTAP = new LIS_SERVICOPEDIDOFESTAProvider();

        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();
        LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaVendedorOS2PedidoFesta()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            GetFuncionario();
            GetDropStatus();            
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




        private LIS_PRODUTOSPEDFESTACollection ProdutoRel(int idvendedor)
        {
            LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("idvendedor", "System.Int32", "=", idvendedor.ToString()));
            
            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

            if (rdOrcamento.Checked)
                RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
            else if (rdVenda.Checked)
                RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

            if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

            LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDFESTAColl;
        }

        private LIS_SERVICOPEDIDOFESTACollection ServicoRel(int idvendedor)
        {
            LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl = new LIS_SERVICOPEDIDOFESTACollection();
            LIS_SERVICOPEDIDOFESTAProvider LIS_SERVICOPEDIDOFESTAP = new LIS_SERVICOPEDIDOFESTAProvider();

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("idvendedor", "System.Int32", "=", idvendedor.ToString()));

            RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", "<=", DataFinal));

            if (rdOrcamento.Checked)
                RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
            else if (rdVenda.Checked)
                RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

            if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

            LIS_SERVICOPEDIDOFESTAColl = LIS_SERVICOPEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_SERVICOPEDIDOFESTAColl;
        }
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Serviços por Vendedor - Ordem de Serviço");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);

                RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Produtos por Vendedor - Ordem de Serviço");
                PRt.Print_DataGridView(dataGridView1, RelatorioTitulo, this.Name, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("idvendedor", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                    RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", "<=", DataFinal));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("flagorcamento", "System.String", "=", "S"));
                    else if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("flagorcamento", "System.String", "=", "N"));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    LIS_SERVICOPEDIDOFESTAColl = LIS_SERVICOPEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio, "dtemissao DESC");
                    LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio, "dtemissao DESC");

                    PreencheGrid2();
                    PreencheGrid3();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: "  + EX.Message);
                }
            }
        }

        decimal TotalGeralPedido = 0;
        decimal SubGeralPedido = 0;
        private void PreencheGrid2()
        {
            //Remove Vendedor Repetido
            LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTA2 = new LIS_SERVICOPEDIDOFESTACollection();
            foreach (LIS_SERVICOPEDIDOFESTAEntity item in LIS_SERVICOPEDIDOFESTAColl)
            {

                if (LIS_SERVICOPEDIDOFESTA2.Find(delegate(LIS_SERVICOPEDIDOFESTAEntity item2) { return (item2.IDVENDEDOR == item.IDVENDEDOR && item.IDVENDEDOR != null && item.IDVENDEDOR > 0); }) == null)
                {
                    LIS_SERVICOPEDIDOFESTA2.Add(item);
                }
            }

            LIS_SERVICOPEDIDOFESTAColl.Clear();
            LIS_SERVICOPEDIDOFESTAColl = LIS_SERVICOPEDIDOFESTA2;

            TotalGeralPedido = 0;


            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            foreach (var LIS_SERVICOPEDIDOFESTATy in LIS_SERVICOPEDIDOFESTAColl)
            {
                
                    LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECH2Coll = new LIS_SERVICOOSFECHCollection();

                    if (LIS_SERVICOPEDIDOFESTATy.IDVENDEDOR != null && LIS_SERVICOPEDIDOFESTATy.IDVENDEDOR > 0)
                    {
                        //Cabeçalho - Nome do Vendedor
                        DataGridViewRow rowCabec = new DataGridViewRow();
                        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                        string NOMEFUNCIONARIO = FUNCIONARIOP.Read(Convert.ToInt32(LIS_SERVICOPEDIDOFESTATy.IDVENDEDOR)).NOME;
                        rowCabec.CreateCells(DataGriewDados, NOMEFUNCIONARIO);
                        rowCabec.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(rowCabec);


                        DataGridViewRow row4_2 = new DataGridViewRow();
                        row4_2.CreateCells(DataGriewDados, "Serviço", "Quant.", "Total");
                        row4_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row4_2);

                        LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl2 = new LIS_SERVICOPEDIDOFESTACollection();
                        LIS_SERVICOPEDIDOFESTAColl2 = ServicoRel(Convert.ToInt32(LIS_SERVICOPEDIDOFESTATy.IDVENDEDOR));

                        foreach (LIS_SERVICOPEDIDOFESTAEntity item in LIS_SERVICOPEDIDOFESTAColl2)
                        {
                            DataGridViewRow row5 = new DataGridViewRow();
                            row5.CreateCells(DataGriewDados, item.NOMESERVICO, Convert.ToDecimal(item.QUANTIDADE).ToString(), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"));
                            row5.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGriewDados.Rows.Add(row5);

                            SubGeralPedido += Convert.ToDecimal(item.VALORTOTAL);                           
                        }

                        TotalGeralPedido += Convert.ToDecimal(SubGeralPedido);

                        DataGridViewRow rowLinhaSubTotal = new DataGridViewRow();
                        rowLinhaSubTotal.CreateCells(DataGriewDados, "", "Sub-Total", SubGeralPedido.ToString("n2"));
                        rowLinhaSubTotal.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(rowLinhaSubTotal);

                        DataGridViewRow rowLinha1 = new DataGridViewRow();
                        rowLinha1.CreateCells(DataGriewDados, "______________________________________", "_________", "_________");
                        rowLinha1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(rowLinha1);
                    }
          

            }

            //Total Geral
            DataGridViewRow rowTotalGeral = new DataGridViewRow();
            rowTotalGeral.CreateCells(DataGriewDados, "______________________________________", "_________", TotalGeralPedido.ToString("n2"));
            rowTotalGeral.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowTotalGeral);
         

            this.Cursor = Cursors.Default;
        }


        private void PreencheGrid3()
        {
            //Remove Vendedor Repetido
            LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl2 = new LIS_PRODUTOSPEDFESTACollection();
            foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
            {

                if (LIS_PRODUTOSPEDFESTAColl2.Find(delegate(LIS_PRODUTOSPEDFESTAEntity item2) { return (item2.IDVENDEDOR == item.IDVENDEDOR && item.IDVENDEDOR != null && item.IDVENDEDOR > 0); }) == null)
                {
                    LIS_PRODUTOSPEDFESTAColl2.Add(item);
                }
            }

            LIS_PRODUTOSPEDFESTAColl.Clear();
            LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAColl2;

            TotalGeralPedido = 0;


            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            dataGridView1.Rows.Clear();
          //LIS_PRODUTOOSFECHColl2.Clear();
            foreach (var LIS_PRODUTOSPEDFESTATy in LIS_PRODUTOSPEDFESTAColl)
            {

                if (LIS_PRODUTOSPEDFESTATy.IDVENDEDOR != null && LIS_PRODUTOSPEDFESTATy.IDVENDEDOR > 0)
                {
                    //Cabeçalho - Nome do Vendedor
                    DataGridViewRow rowCabec = new DataGridViewRow();
                    FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                    string NOMEFUNCIONARIO = FUNCIONARIOP.Read(Convert.ToInt32(LIS_PRODUTOSPEDFESTATy.IDVENDEDOR)).NOME;
                    rowCabec.CreateCells(dataGridView1, NOMEFUNCIONARIO);
                    rowCabec.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridView1.Rows.Add(rowCabec);


                    DataGridViewRow row4_2 = new DataGridViewRow();
                    row4_2.CreateCells(dataGridView1, "Produto", "Quant.", "Total");
                    row4_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridView1.Rows.Add(row4_2);

                    LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl3 = new LIS_PRODUTOSPEDFESTACollection();
                    LIS_PRODUTOSPEDFESTAColl3 = ProdutoRel(Convert.ToInt32(LIS_PRODUTOSPEDFESTATy.IDVENDEDOR));

                    foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl3)
                    {
                        DataGridViewRow row5 = new DataGridViewRow();
                        row5.CreateCells(dataGridView1, item.NOMEPRODUTO, Convert.ToDecimal(item.QUANTIDADE).ToString(), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"));
                        row5.DefaultCellStyle.Font = new Font("Arial", 8);
                        dataGridView1.Rows.Add(row5);

                        SubGeralPedido += Convert.ToDecimal(item.VALORTOTAL);
                    }

                    TotalGeralPedido += Convert.ToDecimal(SubGeralPedido);

                    DataGridViewRow rowLinhaSubTotal = new DataGridViewRow();
                    rowLinhaSubTotal.CreateCells(dataGridView1, "", "Sub-Total", SubGeralPedido.ToString("n2"));
                    rowLinhaSubTotal.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridView1.Rows.Add(rowLinhaSubTotal);

                    DataGridViewRow rowLinha1 = new DataGridViewRow();
                    rowLinha1.CreateCells(dataGridView1, "______________________________________", "_________", "_________");
                    rowLinha1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    dataGridView1.Rows.Add(rowLinha1);
                }


            }

            //Total Geral
            DataGridViewRow rowTotalGeral = new DataGridViewRow();
            rowTotalGeral.CreateCells(dataGridView1, "______________________________________", "_________", TotalGeralPedido.ToString("n2"));
            rowTotalGeral.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            dataGridView1.Rows.Add(rowTotalGeral);


            this.Cursor = Cursors.Default;
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
      
    }
}
