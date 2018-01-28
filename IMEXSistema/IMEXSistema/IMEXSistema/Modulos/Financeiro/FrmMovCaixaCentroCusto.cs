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
    public partial class FrmMovCaixaCentroCusto : Form
    {
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        LIS_CAIXACollection LIS_CAIXAColl = new LIS_CAIXACollection();
        LIS_CAIXAProvider LIS_CAIXAP = new LIS_CAIXAProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmMovCaixaCentroCusto()
        {
            InitializeComponent();
        }

        private void FrmMovCaixaCentroCusto_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            GetDropCentroCusto();
            GetDropTipoDuplicata();

            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();
            TIPODUPLICATAColl = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            TIPODUPLICATAEntity TIPODUPLICATATy = new TIPODUPLICATAEntity();
            TIPODUPLICATATy.NOME = ConfigMessage.Default.MsgDrop;
            TIPODUPLICATATy.IDTIPODUPLICATA = -1;
            TIPODUPLICATAColl.Add(TIPODUPLICATATy);

            Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity>(cbTipo.DisplayMember);

            TIPODUPLICATAColl.Sort(comparer.Comparer);
            cbTipo.DataSource = TIPODUPLICATAColl;

            cbTipo.SelectedIndex = 0;
        }

        private void GetDropCentroCusto()
        {
            CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
            CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

            cbCentroCustos.DisplayMember = "DESCRICAO";
            cbCentroCustos.ValueMember = "IDCENTROCUSTOS";

            CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
            CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
            CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

            Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCustos.DisplayMember);

            CENTROCUSTOSColl.Sort(comparer.Comparer);
            cbCentroCustos.DataSource = CENTROCUSTOSColl;

            cbCentroCustos.SelectedIndex = 0;
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    if (Convert.ToInt32(cbCentroCustos.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", Convert.ToInt32(cbCentroCustos.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbTipo.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", Convert.ToInt32(cbTipo.SelectedValue).ToString()));


                    RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", DataFinal));

                    LIS_CAIXAColl = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio, "DATAMOV");
                    
                    
                    PreencheGrid();
                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        private LIS_CAIXACollection BuscaMovCentroCusto(int IDCENTROCUSTO)
        {
            LIS_CAIXACollection LIS_CAIXAColl_4 = new LIS_CAIXACollection();
            
            RowRelatorio.Clear();
            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

             RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", IDCENTROCUSTO.ToString()));

            if (Convert.ToInt32(cbTipo.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", Convert.ToInt32(cbTipo.SelectedValue).ToString()));


            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DATAMOV", "System.DateTime", "<=", DataFinal));

            LIS_CAIXAColl_4 = LIS_CAIXAP.ReadCollectionByParameter(RowRelatorio, "NOMETIPODUPLICATA");

            return LIS_CAIXAColl_4;
        }

        decimal SubTotalCredito = 0;
        decimal SubTotalDebito = 0;
        decimal TotalGeralCredito = 0;
        decimal TotalGeralDebito = 0;
        private void PreencheGrid()
        {
            SubTotalCredito = 0;
            SubTotalDebito = 0;
            TotalGeralCredito = 0;
            TotalGeralDebito = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            //Remove os centros de custos Repetidos
            LIS_CAIXACollection LIS_CAIXAColl2 = new LIS_CAIXACollection();
            foreach (LIS_CAIXAEntity item in LIS_CAIXAColl)
            {

                if (LIS_CAIXAColl2.Find(delegate(LIS_CAIXAEntity item2) { return (item2.IDCENTROCUSTOS == item.IDCENTROCUSTOS); }) == null)
                {
                    LIS_CAIXAColl2.Add(item);
                }
            }

            //Cabeçalho Centro de Custo
            DataGridViewRow row1 = new DataGridViewRow();
            row1.CreateCells(DataGriewDados, "Centro de Custos", "Crédito", "Débito");
            row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(row1);

            
            foreach (var LIS_CAIXATy in LIS_CAIXAColl2)
            {
                SubTotalCredito = 0;

                //Busca Movimentaça por centro de custo
                LIS_CAIXACollection LIS_CAIXAColl3 = new LIS_CAIXACollection();
                LIS_CAIXAColl3 = BuscaMovCentroCusto(Convert.ToInt32(LIS_CAIXATy.IDCENTROCUSTOS));

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, LIS_CAIXATy.CENTROCUSTO, "", "");
                row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row2);

                foreach (var LIS_CAIXATy2 in LIS_CAIXAColl3)
                {
                    DataGridViewRow row3 = new DataGridViewRow();

                    if (LIS_CAIXATy2.IDTIPOMOVCAIXA == 1) //Credito
                        row3.CreateCells(DataGriewDados, LIS_CAIXATy2.NOMETIPODUPLICATA, Convert.ToDecimal(LIS_CAIXATy2.VALOR).ToString("n2"), "0,00");
                    else
                        row3.CreateCells(DataGriewDados, LIS_CAIXATy2.NOMETIPODUPLICATA, "0,00", Convert.ToDecimal(LIS_CAIXATy2.VALOR).ToString("n2"));

                    row3.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row3);

                    if (LIS_CAIXATy2.IDTIPOMOVCAIXA == 1) //Credito
                    {
                        SubTotalCredito += Convert.ToDecimal(LIS_CAIXATy2.VALOR);
                        TotalGeralCredito += Convert.ToDecimal(LIS_CAIXATy2.VALOR);
                    }
                    else
                    {
                        SubTotalDebito += Convert.ToDecimal(LIS_CAIXATy2.VALOR);
                        TotalGeralDebito += Convert.ToDecimal(LIS_CAIXATy2.VALOR);
                    }                   
                   
                }

                //Subtotal do Centro de Custo
                DataGridViewRow row4 = new DataGridViewRow();
                row4.CreateCells(DataGriewDados, "SubTotal: ", SubTotalCredito.ToString("n2"), SubTotalDebito.ToString("n2"));
                row4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row4);

                //Subtotal do Centro de Custo
                DataGridViewRow row4_1 = new DataGridViewRow();
                row4_1.CreateCells(DataGriewDados, "=========================", "=============", "=============");
                row4_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row4_1);
            }         

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "Total Geral: ", TotalGeralCredito.ToString("n2"), TotalGeralDebito.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            DataGridViewRow rowLinha_1 = new DataGridViewRow();
            rowLinha_1.CreateCells(DataGriewDados, "=========================", "=============", "=============");
            rowLinha_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha_1);

            DataGridViewRow rowLinha2 = new DataGridViewRow();
            rowLinha2.CreateCells(DataGriewDados, "Saldo: ", (TotalGeralCredito - TotalGeralDebito).ToString("n2"), "");
            rowLinha2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha2);

            this.Cursor = Cursors.Default;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Movimentação do Caixa: Data Inicial: " + msktDataInicial.Text + " Data Final: " + msktDataFinal.Text);

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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Movimentação Diária do Caixa";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Movimentação Diária do Caixa");
            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}
