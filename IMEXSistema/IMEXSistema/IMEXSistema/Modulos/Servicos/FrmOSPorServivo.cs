using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.FrmSearch;
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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmOSPorServivo : Form
    {
        LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
        LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmOSPorServivo()
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

        private void FrmVendaGrupoCategoria_Load(object sender, EventArgs e)
        {
            GetDropServico();

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
        }

        private void GetDropServico()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                SERVICOCollection SERVICOColl = new SERVICOCollection();
                SERVICOProvider SERVICOP = new SERVICOProvider();
                SERVICOColl = SERVICOP.ReadCollectionByParameter(null, "NOME");

                cbServico.DisplayMember = "NOME";
                cbServico.ValueMember = "IDSERVICO";

                SERVICOEntity SERVICOTy = new SERVICOEntity();
                SERVICOTy.NOME = ConfigMessage.Default.MsgDrop;
                SERVICOTy.IDSERVICO = -1;
                SERVICOColl.Add(SERVICOTy);

                Phydeaux.Utilities.DynamicComparer<SERVICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SERVICOEntity>(cbServico.DisplayMember);

                SERVICOColl.Sort(comparer.Comparer);
                cbServico.DataSource = SERVICOColl;

                cbServico.SelectedIndex = 0;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }  
       

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas de Produto Por Grupo/Categoria ");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, true);  
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
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", DataFinal));
             
                    if (Convert.ToInt32(cbServico.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSERVICO", "System.Int32", "=", Convert.ToInt32(cbServico.SelectedValue).ToString()));

                    if (rbOrcamentoPesquisa.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));

                    if (rbVendasPesquisa.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");

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

        decimal TotalGeralPedido = 0;
        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                TotalGeralPedido = 0;              

                DataGriewDados.Rows.Clear();

                foreach (var LIS_SERVICOOSFECHTy in LIS_SERVICOOSFECHColl)
                {
                    string DataEmissao = Convert.ToDateTime(LIS_SERVICOOSFECHTy.DATAEMISSAO).ToString("dd/MM/yyyy");
                    string TotalProduto = Convert.ToDecimal(LIS_SERVICOOSFECHTy.VALORTOTAL).ToString("n2");

                    DataGridViewRow row2 = new DataGridViewRow();
                    string Quantidade = Convert.ToDecimal(LIS_SERVICOOSFECHTy.QUANTIDADE).ToString("n2");
                    string ORDEMServico = LIS_SERVICOOSFECHTy.IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    row2.CreateCells(DataGriewDados, LIS_SERVICOOSFECHTy.IDSERVICO.ToString(), LIS_SERVICOOSFECHTy.NOMESERVICO, ORDEMServico, DataEmissao, Quantidade, TotalProduto);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeralPedido += Convert.ToDecimal(TotalProduto);
                }

                DataGridViewRow row2_3 = new DataGridViewRow();

                row2_3.CreateCells(DataGriewDados, string.Empty, string.Empty, string.Empty, string.Empty, "Total: ", TotalGeralPedido.ToString("n2"));
                row2_3.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2_3);

                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Ordem de Serviço por Serviço";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Ordem de Serviço por Serviço");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

    }
}
