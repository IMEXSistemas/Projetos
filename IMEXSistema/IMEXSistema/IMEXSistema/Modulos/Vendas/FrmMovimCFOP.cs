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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmMovimCFOP : Form
    {
        CFOPCollection CFOPColl = new CFOPCollection();
        CFOPProvider CFOPP = new CFOPProvider();

        Utility Util = new Utility();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
        LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

        public FrmMovimCFOP()
        {
            InitializeComponent();
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar4";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }


        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        public MonthCalendar monthCalendar5 = new MonthCalendar();
        Form FormCalendario5 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar5.Name = "monthCalendar5";
            monthCalendar5.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar5_DateSelected);

            FormCalendario5.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario5.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario5.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario5.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario5.Location = new Point(230, 55);
            FormCalendario5.Name = "FrmCalendario5";
            FormCalendario5.Text = "Calendário";
            FormCalendario5.ResumeLayout(false);
            FormCalendario5.Controls.Add(monthCalendar5);
            FormCalendario5.ShowDialog();
        }

        private void monthCalendar5_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar5.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario5.Close();
        }

        private void FrmMovimCFOP_Load(object sender, EventArgs e)
        {
            GetDropCFOP();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
        }

        private void GetDropCFOP()
        {
            try
            {
                CFOPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

                cbCFOP.DisplayMember = "CODCFOP";
                cbCFOP.ValueMember = "IDCFOP";

                CFOPEntity CFOPTy = new CFOPEntity();
                CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
                CFOPTy.IDCFOP = -1;
                CFOPColl.Add(CFOPTy);

                Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>(cbCFOP.DisplayMember);

                CFOPColl.Sort(comparer.Comparer);
                cbCFOP.DataSource = CFOPColl;

                cbCFOP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbCFOP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCFOP.SelectedIndex > 0)
            {
                txtDesCFOP.Text = (CFOPP.Read(Convert.ToInt32(cbCFOP.SelectedValue)).DESCRICAO);
            }
            else
                txtDesCFOP.Text = string.Empty;
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", DataFinal));

                    if (chkFlagSintegraPesquisa.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                    if(Convert.ToInt32(cbCFOP.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", cbCFOP.SelectedValue.ToString()));

                    LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIM");

                  

                    //Remove ID  repetido  
                    LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOES2Coll = new LIS_MOVPRODUTOESCollection();
                    foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                    {
                        if (LIS_MOVPRODUTOES2Coll.Find(delegate(LIS_MOVPRODUTOESEntity item2)
                        {
                            return
                                (item2.IDCFOP == item.IDCFOP);
                        }) == null)
                        {
                            LIS_MOVPRODUTOES2Coll.Add(item);
                        }
                    }

                    LIS_MOVPRODUTOESColl.Clear();
                    LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOES2Coll;

                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        private void PreencheGrid()
        {
            decimal ValotTotalGeral = 0;
            decimal ValotTotalGeralVLFRETE = 0;
            decimal ValotTotalGeralVLDESCONTOPRODUTO = 0;
            decimal ValotTotalGeralVLIPI = 0;  

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            foreach (var LIS_MOVPRODUTOESTy in LIS_MOVPRODUTOESColl)
            {
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", DataFinal));
                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", LIS_MOVPRODUTOESTy.IDCFOP.ToString()));

                if (chkFlagSintegraPesquisa.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOES3Coll = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOES3Coll = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIM");

                DataGridViewRow row2 = new DataGridViewRow();

                string NomeCFOPSelec = string.Empty;
                NomeCFOPSelec = NomeCFOP(Convert.ToInt32(LIS_MOVPRODUTOESTy.IDCFOP));

                row2.CreateCells(DataGriewDados, LIS_MOVPRODUTOESTy.CODCFOP, NomeCFOPSelec, string.Empty);
                row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row2);

                foreach (var LIS_MOVPRODUTOESTy2 in LIS_MOVPRODUTOES3Coll)
                {
                   string VLFRETE =Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLFRETE).ToString("n2");
                   string VLDESCONTOPRODUTO =Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLDESCONTOPRODUTO).ToString("n2");
                   string VLIPI =Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLIPI).ToString("n2");
                            
                    DataGridViewRow row3 = new DataGridViewRow();
                    row3.CreateCells(DataGriewDados, Convert.ToDateTime(LIS_MOVPRODUTOESTy2.DATAMOVIMENTACAO).ToString("dd/MM/yyyy"), "Doc.: " + LIS_MOVPRODUTOESTy2.NDOCUMENTO + " Produto: " + LIS_MOVPRODUTOESTy2.NOMEPRODUTO, Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VALORTOTAL).ToString("n2"), VLFRETE, VLDESCONTOPRODUTO, VLIPI);
                    row3.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row3);

                    ValotTotalGeral += Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VALORTOTAL);
                    ValotTotalGeralVLFRETE += Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLFRETE);
                    ValotTotalGeralVLDESCONTOPRODUTO += Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLDESCONTOPRODUTO);
                    ValotTotalGeralVLIPI += Convert.ToDecimal(LIS_MOVPRODUTOESTy2.VLIPI);
                }
            }


            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "_________", "Total Geral :", ValotTotalGeral.ToString("n2"), ValotTotalGeralVLFRETE.ToString("n2"), ValotTotalGeralVLDESCONTOPRODUTO.ToString("n2"), ValotTotalGeralVLIPI.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private string NomeCFOP(int IDCFOP)
        {
            string result = string.Empty;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));

                CFOPCollection CFOPColl = new CFOPCollection();
                CFOPColl = CFOPP.ReadCollectionByParameter(RowRelatorio);

                if (CFOPColl.Count > 0)
                    result = CFOPColl[0].DESCRICAO;

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
            
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Movimentação por CFOP");

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
                frm.TituloSelec = "Movimentação por CFOP";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }


    }
}
