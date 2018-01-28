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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmComissaoVendedor2 : Form
    {
        LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
        LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
        LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
        LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();
        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmComissaoVendedor2()
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

        private void FrmComissaoVendedor_Load(object sender, EventArgs e)
        {
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            GetFuncionario();
        }

        private void GetFuncionario()
        {
            FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Comissão por Produto/Serviço");

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
                frm.TituloSelec = "Comissão por Produto/Serviço";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
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
            else if(Convert.ToInt32(cbFuncionario.SelectedValue) < 1)
            {
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
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

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", DataInicial, "and"));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", DataFinal, "and"));

                    if (rbOrcamentoPesquisa.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S", "and"));

                    if (rbVendasPesquisa.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N", "and"));

                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString(), "and"));

                    DataGriewDados.Rows.Clear();
                    TotalGeral = 0;
                    TotalGeralComissao = 0;

                    LIS_PRODUTOOSFECHColl.Clear();
                    LIS_SERVICOOSFECHColl.Clear();
                    if (chkProduto.Checked)
                    {
                        LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");
                        lblTotalRegistros.Text = "Total de Registros: " + LIS_PRODUTOOSFECHColl.Count.ToString();
                        PreencheGridProduto();
                    }

                    if (ChkServiço.Checked)
                    {
                        LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");
                        lblTotalRegistros.Text = "Total de Registros: " + (LIS_SERVICOOSFECHColl.Count + LIS_PRODUTOOSFECHColl.Count).ToString();
                        PreencheGridServico();
                    }

                    DataGriewDados.Sort(DataGriewDados.Columns["ordemservico"], ListSortDirection.Descending);

                    DataGridViewRow row3 = new DataGridViewRow();
                    row3.CreateCells(DataGriewDados, "", "", "Total Geral:", TotalGeral.ToString("n2"), TotalGeralComissao.ToString("n2"));
                    row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row3);

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

        decimal TotalGeral = 0;
        decimal TotalGeralComissao = 0;
        private void PreencheGridProduto()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var LIS_PRODUTOOSFECH2Ty in LIS_PRODUTOOSFECHColl)
                {
                    string ORDEMServico = LIS_PRODUTOOSFECH2Ty.IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    string CODIGO = LIS_PRODUTOOSFECH2Ty.IDPRODUTO.ToString().PadLeft(6, '0');
                    string Valor = Convert.ToDecimal(LIS_PRODUTOOSFECH2Ty.VALORTOTAL).ToString("n2");
                    TotalGeral += Convert.ToDecimal(Valor);

                    string NOMEVENDEDOR  = string.Empty;
                    Decimal ComissaoFuncionario = 0;
                    FUNCIONARIOEntity FUNCIONARIOTY = new FUNCIONARIOEntity();
                    FUNCIONARIOTY = FUNCIONARIOP.Read(Convert.ToInt32(LIS_PRODUTOOSFECH2Ty.IDFUNCIONARIO));
                    if(FUNCIONARIOTY != null)
                    {
                        NOMEVENDEDOR = FUNCIONARIOTY.NOME;
                        ComissaoFuncionario = Convert.ToDecimal(FUNCIONARIOTY.COMISSAO);
                    }   

                    Decimal SubTotalComissaoFuncionario = (Convert.ToDecimal(LIS_PRODUTOOSFECH2Ty.VALORTOTAL) * ComissaoFuncionario) /100;
                    TotalGeralComissao+= SubTotalComissaoFuncionario;

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, ORDEMServico, NOMEVENDEDOR, CODIGO + " - " +LIS_PRODUTOOSFECH2Ty.NOMEPRODUTO, Valor, SubTotalComissaoFuncionario.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);                  
                }
                             

                this.Cursor = Cursors.Default;

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void PreencheGridServico()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {

                foreach (var LIS_SERVICOOSFECHTy in LIS_SERVICOOSFECHColl)
                {
                    string ORDEMServico = LIS_SERVICOOSFECHTy.IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    string CODIGO = LIS_SERVICOOSFECHTy.IDSERVICO.ToString().PadLeft(6, '0');
                    string Valor = Convert.ToDecimal(LIS_SERVICOOSFECHTy.VALORTOTAL).ToString("n2");
                    TotalGeral += Convert.ToDecimal(Valor);

                    string NOMEVENDEDOR  = string.Empty;
                    Decimal ComissaoFuncionario = 0;
                    FUNCIONARIOEntity FUNCIONARIOTY = new FUNCIONARIOEntity();
                    FUNCIONARIOTY = FUNCIONARIOP.Read(Convert.ToInt32(LIS_SERVICOOSFECHTy.IDFUNCIONARIO));
                    if(FUNCIONARIOTY != null)
                    {
                        NOMEVENDEDOR = FUNCIONARIOTY.NOME;
                        ComissaoFuncionario = Convert.ToDecimal(FUNCIONARIOTY.COMISSAO);
                    }   

                    Decimal SubTotalComissaoFuncionario = (Convert.ToDecimal(LIS_SERVICOOSFECHTy.VALORTOTAL) * ComissaoFuncionario) / 100;
                    TotalGeralComissao += SubTotalComissaoFuncionario;

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, ORDEMServico, LIS_SERVICOOSFECHTy.NOMEFUNCIONARIO, CODIGO + " - " + LIS_SERVICOOSFECHTy.NOMESERVICO, Valor, SubTotalComissaoFuncionario.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                    
                }              

                this.Cursor = Cursors.Default;

            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Comissão por Produto/Serviço");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
