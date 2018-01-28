using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using VVX;
using BMSworks.UI;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmComissaoPorOS2 : Form
    {
        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();
        LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmComissaoPorOS2()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            GetFuncionario();
            GetDropStatus();            
        }

        private void GetDropStatus()
        {
            //11 Pedido de Venda
            RowsFiltro FiltroProfile = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "11");
            RowsFiltroCollection Filtro = new RowsFiltroCollection();

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";

            Filtro.Insert(0, FiltroProfile);

            STATUSProvider STATUSP = new STATUSProvider();
            STATUSCollection STATUSColl = new STATUSCollection();
            STATUSColl = STATUSP.ReadCollectionByParameter(Filtro);

            STATUSEntity STATUSTy = new STATUSEntity();
            STATUSTy.NOME = ConfigMessage.Default.MsgDrop;
            STATUSTy.IDSTATUS = -1;
            STATUSColl.Add(STATUSTy);

            Phydeaux.Utilities.DynamicComparer<STATUSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSEntity>(cbStatus.DisplayMember);

            STATUSColl.Sort(comparer.Comparer);
            cbStatus.DataSource = STATUSColl;
            cbStatus.SelectedIndex = 0;
        }

        private void GetFuncionario()
        {
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

        decimal TotalGeralPedido = 0;
        decimal TotalGeralComissao = 0;
        decimal SubTotalComissaoFuncionario = 0;
        private void PreencheGrid()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                TotalGeralPedido = 0;
                TotalGeralComissao = 0;
                SubTotalComissaoFuncionario = 0;

                DataGriewDados.Rows.Clear();

                DataGridViewRow rowTop = new DataGridViewRow();
                rowTop.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "____________________________", "__________", "__________");
                rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowTop);

                DataGridViewRow row1 = new DataGridViewRow();
                row1.CreateCells(DataGriewDados, "O.S", "EMISSÃO", "CLIENTE", "STATUS", "FUNCIONÁRIO", "TOTAL O.S", "COMISSÃO");
                row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(row1);

                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();

                string ComissaoporOS = "0";
                
                foreach (var LIS_PEDIDOTy in LIS_ORDEMSERVICOSFECHColl)
                {
                    decimal? ComissaoFuncionario = 0;                  

                    string DataEmissao = string.Empty;
                    if (LIS_PEDIDOTy.IDORDEMSERVICO != null)
                        DataEmissao = Convert.ToDateTime(LIS_PEDIDOTy.DATAEMISSAO).ToString("dd/MM/yyyy");

                    string TotalPedido = Convert.ToDecimal(LIS_PEDIDOTy.TOTALFECHOS).ToString("n2");
                    string TotalComissao = "0"; //Convert.ToDecimal(LIS_PEDIDOTy.VLCOMISSAO).ToString("n2");                    

                    ComissaoFuncionario = 0;
                    FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                    FUNCIONARIOTy = FUNCIONARIOP.Read(Convert.ToInt32(LIS_PEDIDOTy.IDFUNCIONARIO));
                    if (FUNCIONARIOTy != null)
                        ComissaoFuncionario = FUNCIONARIOTy.COMISSAO;

                    ComissaoporOS = (Convert.ToDecimal(LIS_PEDIDOTy.TOTALFECHOS * Convert.ToDecimal(ComissaoFuncionario)) / 100).ToString("n2");
                    SubTotalComissaoFuncionario += Convert.ToDecimal(ComissaoporOS);

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, LIS_PEDIDOTy.IDORDEMSERVICO, DataEmissao, LIS_PEDIDOTy.NOMECLIENTE, LIS_PEDIDOTy.NOMESTATUS, LIS_PEDIDOTy.NOMEFUNCIONARIO, TotalPedido, ComissaoporOS);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeralPedido += Convert.ToDecimal(TotalPedido); 
                    
                }                   
                

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "TOTAL GERAL:", TotalGeralPedido, SubTotalComissaoFuncionario.ToString("n2"));
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                this.Cursor = Cursors.Default;
            }
        }

        private LIS_PRODUTOSPEDIDOMTQOSCollection ProdutoRelMTQ(int IDORDEMSERVICO)
        {
            LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
            LIS_PRODUTOSPEDIDOMTQOSProvider LIS_PRODUTOSPEDIDOMTQOSP = new LIS_PRODUTOSPEDIDOMTQOSProvider();

            try
            {
                RowRelatorio.Clear();

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));

                LIS_PRODUTOSPEDIDOMTQOSColl = LIS_PRODUTOSPEDIDOMTQOSP.ReadCollectionByParameter(RowRelatorio);

                return LIS_PRODUTOSPEDIDOMTQOSColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_PRODUTOSPEDIDOMTQOSColl;

            }
           
        }

        private LIS_PRODUTOOSFECHCollection ProdutoRel(int IDORDEMSERVICO)
        {
            LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
            LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();

            try
            {
                RowRelatorio.Clear();
               
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                
                LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                return LIS_PRODUTOOSFECHColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_PRODUTOOSFECHColl;

            }
        }

        private LIS_SERVICOOSFECHCollection ServicoRel(int IDORDEMSERVICO)
        {
            LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
            LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();
            try
            {

                RowRelatorio.Clear();

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));
                
                LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                return LIS_SERVICOOSFECHColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_SERVICOOSFECHColl;
            }
        }
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Comissão Por Ordem de Serviço");

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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    else if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));
                    
                    LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio, "IDORDEMSERVICO DESC");

                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: "  + EX.Message);
                }
            }
        }


        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            //if (Convert.ToInt32(cbFuncionario.SelectedValue) < 0)
            //{
            //    errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
            //    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            //    result = false;
            //}
            //else
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Comissão por Ordem de Serviço");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Comissão por Ordem de Serviço";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
      
    }
}
