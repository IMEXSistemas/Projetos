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
using VVX;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmFluxoContas2 : Form
    {
        Utility Util = new Utility();

        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();


        public FrmFluxoContas2()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkDtInicial.Text))
            {
                errorProvider1.SetError(mkDtInicial, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdatafinal.Text))
            {
                errorProvider1.SetError(mkdatafinal, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                PesquisaDuplicatasReceber();
                PesquisaDuplicatasPagar();

                //if (rbVencimento.Checked)
                //    DataGridRelaDupl.Sort(DataGridRelaDupl.Columns["DATAVECTO"], ListSortDirection.Ascending);
                //else if (rbVencimento.Checked)
                //    DataGridRelaDupl.Sort(DataGridRelaDupl.Columns["DATAEMISSAO"], ListSortDirection.Ascending);
                //else if (rdPagamento.Checked)
                //    DataGridRelaDupl.Sort(DataGridRelaDupl.Columns["DATAPAGTO"], ListSortDirection.Ascending);

                lblValorSaldo.Text = (Convert.ToDecimal(lblValorTotalReceber.Text) - Convert.ToDecimal(lblValorTotalPagar.Text)).ToString("n2");
            }
        }

        private void PesquisaDuplicatasReceber()
        {

            try
            {
                RowRelatorio.Clear();
                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (!chkDuplicataPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (!chkDuplicataPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                }

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                PreencheGridReceber();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
           
        }

        private void PreencheGridReceber()
        {
            try
            {
                decimal QuantTotal = 0;
                decimal ValorTotal = 0;
                decimal ValotTotalGeral = 0;

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DataGridRelaDupl.Rows.Clear();

                foreach (var LIS_DUPLICATARECEBERTy in LIS_DUPLICATARECEBERColl)
                {
                    QuantTotal = 0;
                    ValorTotal = 0;
                   // QuantProduto(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDPRODUTO));
                    ValotTotalGeral += ValorTotal;
                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGridRelaDupl, LIS_DUPLICATARECEBERTy.NUMERO, "Receber", LIS_DUPLICATARECEBERTy.NOMECLIENTE, Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAEMISSAO).ToString("dd/MM/yyyy"), Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAVECTO).ToString("dd/MM/yyyy"),Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAPAGTO).ToString("dd/MM/yyyy"), Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORDUPLICATA).ToString("n2"), Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORPAGO).ToString("n2"), LIS_DUPLICATARECEBERTy.NOMESTATUS);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGridRelaDupl.Rows.Add(row2);
                }

                //DataGridViewRow rowLinha = new DataGridViewRow();
                //rowLinha.CreateCells(DataGridRelaDupl, "_________", "___________________________________________________________", "Total Geral", ValotTotalGeral.ToString("n2"));
                //rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                //DataGridRelaDupl.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void PesquisaDuplicatasPagar()
        {

            try
            {
                
                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (!chkDuplicataPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (!chkDuplicataPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                }
                
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                //Colocando somatorio no final da lista
                LIS_DUPLICATAPAGAREntity LIS_DUPLICATAPAGARTy = new LIS_DUPLICATAPAGAREntity();
                LIS_DUPLICATAPAGARTy.VALORDUPLICATA = SumTotalPesquisa2("VALORDUPLICATA");
                LIS_DUPLICATAPAGARTy.VALORPAGO = SumTotalPesquisa2("VALORPAGO");
                LIS_DUPLICATAPAGARColl.Add(LIS_DUPLICATAPAGARTy);

                //Colocando somatorio no final da lista
                LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
                LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
                LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");

                LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);

                PreencheGridPagar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("erro técnico: " + ex.Message);
            } 
        }

        private void PreencheGridPagar()
        {
            try
            {
                decimal QuantTotal = 0;
                decimal ValorTotal = 0;
                decimal ValotTotalGeral = 0;

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //DataGridRelaDupl.Rows.Clear();

                foreach (var LIS_DUPLICATAPAGARTy in LIS_DUPLICATAPAGARColl)
                {
                    QuantTotal = 0;
                    ValorTotal = 0;
                    
                    ValotTotalGeral += ValorTotal;
                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGridRelaDupl, LIS_DUPLICATAPAGARTy.NUMERO, "Pagar", LIS_DUPLICATAPAGARTy.NOMEFORNECEDOR, Convert.ToDateTime(LIS_DUPLICATAPAGARTy.DATAEMISSAO).ToString("dd/MM/yyyy"), Convert.ToDateTime(LIS_DUPLICATAPAGARTy.DATAVECTO).ToString("dd/MM/yyyy"), Convert.ToDateTime(LIS_DUPLICATAPAGARTy.DATAPAGTO).ToString("dd/MM/yyyy"), Convert.ToDecimal(LIS_DUPLICATAPAGARTy.VALORDUPLICATA).ToString("n2"), Convert.ToDecimal(LIS_DUPLICATAPAGARTy.VALORPAGO).ToString("n2"), LIS_DUPLICATAPAGARTy.NOMESTATUS);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGridRelaDupl.Rows.Add(row2);
                }

                //DataGridViewRow rowLinha = new DataGridViewRow();
                //rowLinha.CreateCells(DataGridRelaDupl, "_________", "___________________________________________________________", "Total Geral", ValotTotalGeral.ToString("n2"));
                //rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                //DataGridRelaDupl.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal SumTotalPesquisa2(string NomeCampo)
        {
            decimal valortotal = 0;
            decimal valortotal2 = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                {
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);
                    valortotal2 += Convert.ToDecimal(item.VALORPAGO);
                    lblValorTotalPagar.Text = valortotal2.ToString("n2");
                }
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
            }



            return valortotal;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            decimal valortotal2 = 0;
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                {
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);
                    valortotal2 += Convert.ToDecimal(item.VALORPAGO);
                    lblValorTotalReceber.Text = valortotal2.ToString("n2");
                }
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
            }


            return valortotal;
        }

        public MonthCalendar monthCalendar1 = new MonthCalendar();
        Form FormCalendario1 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar1_DateSelected);

            FormCalendario1.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario1.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario1.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario1.Location = new Point(230, 55);
            FormCalendario1.Name = "FrmCalendario";
            FormCalendario1.Text = "Calendário";
            FormCalendario1.ResumeLayout(false);
            FormCalendario1.Controls.Add(monthCalendar1);
            FormCalendario1.ShowDialog();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkDtInicial.Text = monthCalendar1.SelectionStart.Date.ToString("dd/MM/yyyy");
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
            FormCalendario2.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario2.Location = new Point(230, 55);
            FormCalendario2.Name = "FrmCalendario2";
            FormCalendario2.Text = "Calendário 2";
            FormCalendario2.ResumeLayout(false);
            FormCalendario2.Controls.Add(monthCalendar2);
            FormCalendario2.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkdatafinal.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario2.Close();
        }

        private void FrmFluxoContas_Load(object sender, EventArgs e)
        {
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            VerificaAcesso();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDocument1;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();

                }

                if (rdBCompleto.Checked)
                {
                    string RelatorioTitulo = "Fluxo de Contas Receber/Pagar - ( Contas a Receber )";

                    PrintDGV PRt = new PrintDGV();
                    PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);

                   // RelatorioTitulo = "Fluxo de Contas Receber/Pagar - ( Contas a Pagar )";
                  //  PRt.Print_DataGridView(dgrDuplcPagar, RelatorioTitulo, this.Name);
                }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();
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

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("FLUXO DE CONTAS A RECEBER/PAGAR - RESUMO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);

            e.Graphics.DrawString("Total de Contas a Receber: " , config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 165);
            e.Graphics.DrawString(lblValorTotalReceber.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 165);
            e.Graphics.DrawString("Total de Contas a Pagar: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            e.Graphics.DrawString(lblValorTotalPagar.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 185);
            e.Graphics.DrawString("Saldo: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 205);
            e.Graphics.DrawString(lblValorSaldo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 205);

        }

        private void DataGridRelaDupl_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1 && e.Value.Equals("Receber"))
            {
                DataGridRelaDupl.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }

            if (e.Value != null && e.ColumnIndex == 1 && e.Value.Equals("Pagar"))
            {
                DataGridRelaDupl.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }

            
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Fluxo de Contas Receber/Pagar";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGridRelaDupl;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGridRelaDupl, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Fluxo de Contas Receber/Pagar");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);
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
       
    }
}
