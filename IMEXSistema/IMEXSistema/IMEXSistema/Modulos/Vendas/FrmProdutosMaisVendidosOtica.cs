using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VVX;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmProdutosMaisVendidosOtica : Form
    {
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDOTICACollection();
        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDOTICAProvider();

        LIS_SERVICOPEDOTICACollection LIS_SERVICOPEDOTICAColl = new LIS_SERVICOPEDOTICACollection();
        LIS_SERVICOPEDOTICAProvider LIS_SERVICOPEDOTICAP = new LIS_SERVICOPEDOTICAProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmProdutosMaisVendidosOtica()
        {
            InitializeComponent();
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

        private void FrmProdutosMaisVendidos_Load(object sender, EventArgs e)
        {
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnPrintServico.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);
        }    

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    //Filtra  Produtos
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
                    
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);
                    
                    //Remove ID  repetido  
                    LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDIDO2Coll = new LIS_PRODUTOSPEDOTICACollection();
                    foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        if (LIS_PRODUTOSPEDIDO2Coll.Find(delegate(LIS_PRODUTOSPEDOTICAEntity item2)
                        {
                            return
                                (item2.IDPRODUTO == item.IDPRODUTO);
                        }) == null)
                        {
                            LIS_PRODUTOSPEDIDO2Coll.Add(item);
                        }
                    }

                    LIS_PRODUTOSPEDIDOColl.Clear();
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDO2Coll;

                    //Filtra Serviços
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
                    LIS_SERVICOPEDOTICAColl = LIS_SERVICOPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

                    //Remove ID  repetido  
                    LIS_SERVICOPEDOTICACollection LIS_SERVICOPEDOTICA2Coll = new LIS_SERVICOPEDOTICACollection();
                    foreach (LIS_SERVICOPEDOTICAEntity item in LIS_SERVICOPEDOTICAColl)
                    {
                        if (LIS_SERVICOPEDOTICA2Coll.Find(delegate(LIS_SERVICOPEDOTICAEntity item2)
                        {
                            return
                                (item2.IDSERVICO == item.IDSERVICO);
                        }) == null)
                        {
                            LIS_SERVICOPEDOTICA2Coll.Add(item);
                        }
                    }

                    LIS_SERVICOPEDOTICAColl.Clear();
                    LIS_SERVICOPEDOTICAColl = LIS_SERVICOPEDOTICA2Coll;

                    PreencheGrid();
                    PreencheGridServico();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        decimal QuantTotal = 0;
        decimal ValorTotal = 0;
        decimal ValotTotalGeral = 0;
        private void PreencheGrid()
        {
            QuantTotal = 0;
            ValorTotal = 0;
            ValotTotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            //DataGridViewRow rowTop = new DataGridViewRow();
            //rowTop.CreateCells(DataGriewDados, "_________", "___________________________________________________________", "_______", "_______");
            //rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(rowTop);

            //DataGridViewRow row1 = new DataGridViewRow();
            //row1.CreateCells(DataGriewDados, "Ranking", "Descrição", "Quantidade", "Total R$");
            //row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(row1);

            foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOSPEDIDOColl)
            {
               QuantTotal = 0;
               ValorTotal = 0;
               QuantProduto(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDPRODUTO));
               ValotTotalGeral += ValorTotal;

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, "", LIS_PRODUTOSPEDIDOTy.NOMEPRODUTO, QuantTotal, ValorTotal.ToString("n2"));
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);           
            }

            DataGriewDados.Sort(DataGriewDados.Columns["quantidade"], ListSortDirection.Descending);

            //DataGridViewRow rowLinha_1 = new DataGridViewRow();
            //rowLinha_1.CreateCells(DataGriewDados, "_________", "___________________________________________________________", "_______", "_______");
            //rowLinha_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(rowLinha_1);

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "_________", "___________________________________________________________", "Total Geral", ValotTotalGeral.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private void PreencheGridServico()
        {
            QuantTotal = 0;
            ValorTotal = 0;
            ValotTotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDadosServico.Rows.Clear();


            foreach (var LIS_SERVICOPEDOTICATy in LIS_SERVICOPEDOTICAColl)
            {
                QuantTotal = 0;
                ValorTotal = 0;
                QuantSevico(Convert.ToInt32(LIS_SERVICOPEDOTICATy.IDSERVICO));
                ValotTotalGeral += ValorTotal;

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDadosServico, "", LIS_SERVICOPEDOTICATy.NOMESERVICO, QuantTotal, ValorTotal.ToString("n2"));
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDadosServico.Rows.Add(row2);
            }

            DataGriewDadosServico.Sort(DataGriewDadosServico.Columns["quantidade2"], ListSortDirection.Descending);


            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "_________", "___________________________________________________________", "Total Geral", ValotTotalGeral.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDadosServico.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private void QuantProduto(int IDPRODUTO)
        {
            LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDOTICACollection();
            LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDOTICAProvider();

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));

            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

             foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOSPEDIDOColl)
            {
                QuantTotal += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.QUANTIDADE);
                ValorTotal += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.VALORTOTAL);
            }

        }

        private void QuantSevico(int IDSERVICO)
        {
            LIS_SERVICOPEDOTICACollection LIS_SERVICOPEDOTICA2Coll = new LIS_SERVICOPEDOTICACollection();

            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
            RowRelatorio.Add(new RowsFiltro("IDSERVICO", "System.Int32", "=", IDSERVICO.ToString()));

            LIS_SERVICOPEDOTICA2Coll = LIS_SERVICOPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

            foreach (var LIS_SERVICOPEDOTICATy in LIS_SERVICOPEDOTICA2Coll)
            {
                QuantTotal += Convert.ToDecimal(LIS_SERVICOPEDOTICATy.QUANTIDADE);
                ValorTotal += Convert.ToDecimal(LIS_SERVICOPEDOTICATy.VALORTOTAL);
            }

        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();           
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos Mais Vendidos  - Data Inicial: " + msktDataInicial.Text + " Data Final: " + msktDataFinal.Text);

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

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 0)
            {
                if ((DataGriewDados.Rows.Count - 2) > e.RowIndex)
                    e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void btnPrintServico_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Serviços Mais Vendidos  - Data Inicial: " + msktDataInicial.Text + " Data Final: " + msktDataFinal.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDadosServico, RelatorioTitulo, this.Name, false);  
        }

        private void DataGriewDadosServico_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 0)
            {
                if ((DataGriewDadosServico.Rows.Count - 2) > e.RowIndex)
                    e.Value = (e.RowIndex + 1).ToString();
            }
        }


    }
}
