using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;
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
    public partial class FrmRankingOcorroenciasTLMK : Form
    {
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        LIS_OCORRENCIATLMKCollection LIS_OCORRENCIATLMKColl = new LIS_OCORRENCIATLMKCollection();
        LIS_OCORRENCIATLMKProvider LIS_OCORRENCIATLMKP = new LIS_OCORRENCIATLMKProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmRankingOcorroenciasTLMK()
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

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
			
        }    

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                try
                {
                    RowRelatorio.Clear();

                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);                  

                    RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", "<=", DataFinal));

                    LIS_OCORRENCIATLMKColl = LIS_OCORRENCIATLMKP.ReadCollectionByParameter(RowRelatorio);

                    //Remove ID  repetido  
                    LIS_OCORRENCIATLMKCollection LIS_OCORRENCIATLMKColl2 = new LIS_OCORRENCIATLMKCollection();
                    foreach (LIS_OCORRENCIATLMKEntity item in LIS_OCORRENCIATLMKColl)
                    {
                        if (LIS_OCORRENCIATLMKColl2.Find(delegate(LIS_OCORRENCIATLMKEntity item2)
                        {
                            return
                                (item2.IDFUNCIONARIO == item.IDFUNCIONARIO);
                        }) == null)
                        {
                            LIS_OCORRENCIATLMKColl2.Add(item);
                        }
                    }

                    LIS_OCORRENCIATLMKColl.Clear();
                    LIS_OCORRENCIATLMKColl = LIS_OCORRENCIATLMKColl2;

                    PreencheGrid();

                    this.Cursor = Cursors.Default;
                }
                catch (Exception EX)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        decimal QuantSubTotal = 0;
        decimal QuantTotal = 0;
        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                QuantTotal = 0;
                QuantSubTotal = 0;                

                DataGriewDados.Rows.Clear();

                foreach (var LIS_OCORRENCIATLMKTy in LIS_OCORRENCIATLMKColl)
                {
                    QuantSubTotal = QuantOcorrencias(Convert.ToInt32(LIS_OCORRENCIATLMKTy.IDFUNCIONARIO));
                    QuantTotal += QuantSubTotal;

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, "", LIS_OCORRENCIATLMKTy.NOMEFUNCIONARIO, QuantSubTotal);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                }

                DataGriewDados.Sort(DataGriewDados.Columns["quantidade"], ListSortDirection.Descending);

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "_________", "__________________________________________________________________ TOTAL GERAL: ", QuantTotal.ToString());
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private decimal QuantOcorrencias(int IDFUNCIOANRIO)
        {
            decimal result = 0;
            LIS_OCORRENCIATLMKCollection LIS_OCORRENCIATLMKColl2 = new LIS_OCORRENCIATLMKCollection();            
            
            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", "<=", DataFinal));
            RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", IDFUNCIOANRIO.ToString()));
            
            LIS_OCORRENCIATLMKColl2 = LIS_OCORRENCIATLMKP.ReadCollectionByParameter(RowRelatorio);

            foreach (var LIS_OCORRENCIATLMTy in LIS_OCORRENCIATLMKColl2)
            {
                result += 1;
             }


             return result;

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

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Ranking de Ocorências do Telemarketing");

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
                frm.TituloSelec = "Ranking de Ocorências do Telemarketing";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }


    }
}
