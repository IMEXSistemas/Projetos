using BmsSoftware.Classes.BMSworks.UI;
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

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmFluxoReceberPagarCentroCusto : Form
    {
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();

        CENTROCUSTOSCollection CENTROCUSTOSColl2 = new CENTROCUSTOSCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmFluxoReceberPagarCentroCusto()
        {
            InitializeComponent();
        }

        private void FrmFluxoReceberPagarCentroCusto_Load(object sender, EventArgs e)
        {
            GetDropCentroCusto();

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);
        }

        private void GetDropCentroCusto()
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
                JuntaCentroCusto();
            }
        }

        private void JuntaCentroCusto()
        {
            try
            {
                CENTROCUSTOSColl2.Clear();
                foreach (var item in LIS_DUPLICATAPAGARColl)
                {
                    CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                    CENTROCUSTOSTy.IDCENTROCUSTOS = Convert.ToInt32(item.IDCENTROCUSTO);
                    CENTROCUSTOSTy.CENTROCUSTO = item.CENTROCUSTO;
                    CENTROCUSTOSColl2.Add(CENTROCUSTOSTy);
                }

                foreach (var item in LIS_DUPLICATARECEBERColl)
                {
                    CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                    CENTROCUSTOSTy.IDCENTROCUSTOS = Convert.ToInt32(item.IDCENTROCUSTO);
                    CENTROCUSTOSTy.CENTROCUSTO = item.CENTROCUSTO;
                    CENTROCUSTOSColl2.Add(CENTROCUSTOSTy);
                }

                //remover centro custo duplicado
                CENTROCUSTOSCollection CENTROCUSTOSColl3 = new CENTROCUSTOSCollection();
                foreach (CENTROCUSTOSEntity item in CENTROCUSTOSColl2)
                {

                    if (CENTROCUSTOSColl3.Find(delegate(CENTROCUSTOSEntity item2) { return (item2.IDCENTROCUSTOS == item.IDCENTROCUSTOS); }) == null)
                    {
                        CENTROCUSTOSColl3.Add(item);
                    }
                }

                CENTROCUSTOSColl2 = CENTROCUSTOSColl3;

                PreencheGrid2();              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeral = 0;
        private void PreencheGrid2()
        {
            TotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            ////Cabeçalho Nome Cidade
            //DataGridViewRow row1 = new DataGridViewRow();
            //row1.CreateCells(DataGriewDados, "Centro de Custo", "A Receber", "A Pagar", "Total");
            //row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(row1);

            foreach (var CENTROCUSTOSy in CENTROCUSTOSColl2)
            {
                if (CENTROCUSTOSy.IDCENTROCUSTOS > 0)
                {
                    decimal TotalReceber = FiltraCentroCustoReceber(CENTROCUSTOSy.IDCENTROCUSTOS);
                    decimal TotalPagar = FiltraCentroCustoPagar(CENTROCUSTOSy.IDCENTROCUSTOS);
                    decimal Saldo = TotalReceber - TotalPagar;

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, CENTROCUSTOSy.CENTROCUSTO, TotalReceber.ToString("n2"), TotalPagar.ToString("n2"), Saldo.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeral += Saldo;
                }
            }

            DataGridViewRow row3 = new DataGridViewRow();
            row3.CreateCells(DataGriewDados, "Total", "", "", TotalGeral.ToString("N2"));
            row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(row3);

            this.Cursor = Cursors.Default;
        }
        private decimal FiltraCentroCustoReceber(int CENTROCUSTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", CENTROCUSTO.ToString()));

                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                }

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl2 = new BMSworks.Model.LIS_DUPLICATARECEBERCollection();
                LIS_DUPLICATARECEBERColl2 = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                foreach (var item in LIS_DUPLICATARECEBERColl2)
                {
                    result += Convert.ToDecimal(item.VALORDUPLICATA);
                }


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

        private decimal FiltraCentroCustoPagar(int CENTROCUSTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", CENTROCUSTO.ToString()));

                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                }

                LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl2 = new BMSworks.Model.LIS_DUPLICATAPAGARCollection();
                LIS_DUPLICATAPAGARColl2 = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                foreach (var item in LIS_DUPLICATAPAGARColl2)
                {
                    result += Convert.ToDecimal(item.VALORDUPLICATA);
                }


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }

        }

        private void PesquisaDuplicatasPagar()
        {
            try
            {
                RowRelatorio.Clear();

                if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));

                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                }

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PesquisaDuplicatasReceber()
        {
            try
            {
                RowRelatorio.Clear();

                if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));

                if (rbVencimento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rbEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                    if (rbPaga.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                    else if (rbVencidasVencer.Checked)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                }
                else if (rdPagamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));
                }

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAPAGTO");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Fluxo de Receber/Pagar por CentroCusto");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void DataGriewDados_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Fluxo de Receber/Pagar por CentroCusto";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Fluxo de Receber/Pagar por CentroCusto");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }       

        
    }
}
