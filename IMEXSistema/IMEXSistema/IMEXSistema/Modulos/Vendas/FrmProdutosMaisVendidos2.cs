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
    public partial class FrmProdutosMaisVendidos2 : Form
    {
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
        LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmProdutosMaisVendidos2()
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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
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
                    RowRelatorio.Clear();

                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Add(new RowsFiltro("dataemissao", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("dataemissao", "System.DateTime", "<=", DataFinal));

                    LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                    //Remove ID  repetido  
                    LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECH2Coll = new LIS_PRODUTOOSFECHCollection();
                    foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                    {
                        if (LIS_PRODUTOOSFECH2Coll.Find(delegate(LIS_PRODUTOOSFECHEntity item2)
                        {
                            return
                                (item2.IDPRODUTO == item.IDPRODUTO);
                        }) == null)
                        {
                            LIS_PRODUTOOSFECH2Coll.Add(item);
                        }
                    }

                    LIS_PRODUTOOSFECHColl.Clear();
                    LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECH2Coll;

                    PreencheGrid();

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

            foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOOSFECHColl)
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

       

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "_________", "___________________________________________________________", "Total Geral", ValotTotalGeral.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private void QuantProduto(int IDPRODUTO)
        {
            LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
            LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
          
            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("dataemissao", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("dataemissao", "System.DateTime", "<=", DataFinal));
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));

            LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOOSFECHColl)
            {
                QuantTotal += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.QUANTIDADE);
                ValorTotal += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy.VALORTOTAL);
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produtos Mais Vendidos");

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


    }
}
