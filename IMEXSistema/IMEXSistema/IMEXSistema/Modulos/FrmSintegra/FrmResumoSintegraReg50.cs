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

namespace BmsSoftware.Modulos.FrmSintegra
{
    public partial class FrmResumoSintegraReg50 : Form
    {
        LIS_PRODUTONFECollection LIS_PRODUTONFE_2Coll = new LIS_PRODUTONFECollection();
        LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmResumoSintegraReg50()
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

        private void FrmResumoSintegraReg50_Load(object sender, EventArgs e)
        {
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Sintegra - Registro 50");

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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Resumo Sintegra - Registro 50";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (rbSaida.Checked)
                PesquisaSaida();
            else
                PesquisaeEntrada();
        }
        private void PesquisaeEntrada()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                //Filtra Produtos Nota Fiscal
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));
                RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "NDOCUMENTO, DATAMOVIM DESC");

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl2 = new LIS_MOVPRODUTOESCollection();
                //  Remove CFOP repetido e Aliq. ICMS                   
                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {

                    if (LIS_MOVPRODUTOESColl2.Find(delegate(LIS_MOVPRODUTOESEntity item2)
                    { return (item2.CODCFOP == item.CODCFOP && item2.ALQICMS == item.ALQICMS && item2.NDOCUMENTO == item.NDOCUMENTO); }) == null)
                    {
                        LIS_MOVPRODUTOESColl2.Add(item);
                    }
                }

                LIS_MOVPRODUTOESColl.Clear();
                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESColl2;

                PreencheGrid3();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheGrid3()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                TotalGeral = 0;
                DataGriewDados.Rows.Clear();

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {
                    FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                    FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                    FORNECEDORTy = FORNECEDORP.Read(Convert.ToInt32(item.IDFORNECEDOR));

                    string NotaFiscal = item.NDOCUMENTO;
                    decimal ValorTotal = Convert.ToDecimal(item.VALORTOTAL);
                    TotalGeral += TotalNFEntradaReg50(Convert.ToInt32(item.IDCFOP), Convert.ToDecimal(item.ALQICMS), item.NDOCUMENTO); ;
                   
                    string FlagEnviada = "S";
                    string FLAGINUTILIZADO = "S";
                    string FLAGCANCELADA = "S";                   

                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, NotaFiscal, FORNECEDORTy.NOME, FlagEnviada, FLAGINUTILIZADO, FLAGCANCELADA, ValorTotal.ToString("n2"));
                    row1.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row1);

                }

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "-------", "-----------------------------", "---------", "---------", "Total geral:", TotalGeral.ToString("n2"));
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private decimal TotalNFEntradaReg50(int IDCFOP, decimal ALQICMS, string NDOCUMENTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));
                RowRelatorio.Add(new RowsFiltro("ALQICMS", "System.Decimal", "=", Util.ConverStringDecimalSearch(ALQICMS.ToString("n2"))));
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO));
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl3 = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESColl3 = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl3)
                {
                    result += Convert.ToDecimal(item.VALORTOTAL) + Convert.ToDecimal(item.VLIPI) + Convert.ToDecimal(item.VLFRETE) + Convert.ToDecimal(item.VLICMSST) - Convert.ToDecimal(item.VLDESCONTOPRODUTO);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }

            return result;
        }

        private void PesquisaSaida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                //Filtra Produtos Nota Fiscal
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));


                if (rbCancelada.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "S"));

                }
                else if (rdbEnviada.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
                }

                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "NOTAFISCALE, DTEMISSAO");

                // Remove CFOP, Aliq. ICMS  e nota fiscal repetido
                LIS_PRODUTONFECollection LIS_PRODUTONFE_2_2Coll = new LIS_PRODUTONFECollection();
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFE_2Coll)
                {

                    if (LIS_PRODUTONFE_2_2Coll.Find(delegate(LIS_PRODUTONFEEntity item2)
                    { return (item2.CODCFOP == item.CODCFOP && item2.ALICMS == item.ALICMS && item2.NOTAFISCALE == item.NOTAFISCALE); }) == null)
                    {
                        LIS_PRODUTONFE_2_2Coll.Add(item);
                    }
                }
                LIS_PRODUTONFE_2Coll.Clear();
                LIS_PRODUTONFE_2Coll = LIS_PRODUTONFE_2_2Coll;

                PreencheGrid2();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeral = 0;
        private void PreencheGrid2()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                TotalGeral = 0;
                DataGriewDados.Rows.Clear();               

                foreach (var LIS_PRODUTONFEEntity in LIS_PRODUTONFE_2Coll)
                {
                    NOTAFISCALEEntity NOTAFISCALETY = new NOTAFISCALEEntity();
                    NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                    NOTAFISCALETY = NOTAFISCALEP.Read(Convert.ToInt32(LIS_PRODUTONFEEntity.IDNOTAFISCALE));

                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(LIS_PRODUTONFEEntity.IDCLIENTE));
                    
                    string NotaFiscal = NOTAFISCALETY.NOTAFISCALE;
                    decimal ValorTotal = Convert.ToDecimal(NOTAFISCALETY.TOTALNOTA);
                    TotalGeral += ValorTotal;
                    string FlagEnviada = NOTAFISCALETY.FLAGENVIADA;
                    string FLAGINUTILIZADO = NOTAFISCALETY.FLAGINUTILIZADO;
                    string FLAGCANCELADA = NOTAFISCALETY.FLAGCANCELADA;

                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, NotaFiscal, CLIENTETy.NOME,  FlagEnviada, FLAGINUTILIZADO, FLAGCANCELADA, ValorTotal.ToString("n2"));
                    row1.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row1); 

                }

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "-------", "-----------------------------","---------", "---------", "Total geral:", TotalGeral.ToString("n2"));
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                 this.Cursor = Cursors.Default;
                 MessageBox.Show("Erro técnico: " + ex.Message);
                
            }
        }

     

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Sintegra - Registro 50");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
               
    }
}
 