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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmRelatComissaoTerceiros : Form
    {
        LIS_COMISSAOTERCCollection LIS_COMISSAOTERCColl = new LIS_COMISSAOTERCCollection();
        LIS_COMISSAOTERCProvider LIS_COMISSAOTERCP = new LIS_COMISSAOTERCProvider();

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmRelatComissaoTerceiros()
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
        private void PreencheGrid()
        {
            try
            {
                TotalGeralPedido = 0;
                TotalGeralComissao = 0;

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DataGriewDados.Rows.Clear();
                string TotalComissao = "0";

                foreach (var LIS_COMISSAOTERCTy in LIS_COMISSAOTERCColl)
                {
                    //Topo Nome do funcionario
                    DataGridViewRow rowTop = new DataGridViewRow();
                    rowTop.CreateCells(DataGriewDados, "Funcionário", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowTop);

                    string NOMEFunc = LIS_COMISSAOTERCTy.NOMEFUNC;
                    DataGridViewRow rowTop2 = new DataGridViewRow();
                    rowTop2.CreateCells(DataGriewDados, NOMEFunc, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    rowTop2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowTop2);

                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, "Pedido   Emissão   Cliente", "Status", "Vl.Pedido", "Vl.Comissão");
                    row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row1);

                    //Pedidos do Funcionario
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(LIS_COMISSAOTERCTy.IDFUNCIONARIO).ToString()));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    LIS_COMISSAOTERCCollection LIS_COMISSAOTERC3Coll = new LIS_COMISSAOTERCCollection();
                    LIS_COMISSAOTERCProvider LIS_COMISSAOTERCP = new LIS_COMISSAOTERCProvider();
                    LIS_COMISSAOTERC3Coll = LIS_COMISSAOTERCP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO");

                    foreach (var LIS_COMISSAOTERC3Ty in LIS_COMISSAOTERC3Coll)
                    {
                        //Dados do Pedido
                        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
                        LIS_PEDIDOProvider LIS_PEDIDOProvider = new LIS_PEDIDOProvider();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", Convert.ToInt32(LIS_COMISSAOTERC3Ty.IDPEDIDO).ToString()));
                        LIS_PEDIDOColl = LIS_PEDIDOProvider.ReadCollectionByParameter(RowRelatorio);
                        string DataEmissao = Convert.ToDateTime(LIS_PEDIDOColl[0].DTEMISSAO).ToString("dd/MM/yyyy");

                        string TotalPedido = Convert.ToDecimal(LIS_PEDIDOColl[0].TOTALPEDIDO).ToString("n2");
                        TotalComissao = Convert.ToDecimal(LIS_COMISSAOTERC3Ty.VALOR).ToString("n2");

                        DataGridViewRow row2 = new DataGridViewRow();
                        row2.CreateCells(DataGriewDados, LIS_PEDIDOColl[0].IDPEDIDO.ToString().PadLeft(6, '0') + "   " + DataEmissao + " " + LIS_PEDIDOColl[0].NOMECLIENTE, LIS_PEDIDOColl[0].NOMESTATUS, TotalPedido, TotalComissao);
                        row2.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row2);
                        TotalGeralComissao += Convert.ToDecimal(TotalComissao);
                    }

                }

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, string.Empty, string.Empty, "TOTAL ", TotalGeralComissao);
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private LIS_PRODUTOSPEDIDOCollection ProdutoRel(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
            LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOColl;
        }

        private LIS_PRODUTOSPEDIDOMTQCollection ProdutoRel2(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
            LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOMTQColl;
        }
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação das Comissões de Terceiros");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);
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

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    if(rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    
                    LIS_COMISSAOTERCColl = LIS_COMISSAOTERCP.ReadCollectionByParameter(RowRelatorio);

                    //Remove Funcionario Repetido
                    LIS_COMISSAOTERCCollection LIS_COMISSAOTERC2Coll = new LIS_COMISSAOTERCCollection();
                    foreach (LIS_COMISSAOTERCEntity item in LIS_COMISSAOTERCColl)
                    {

                        if (LIS_COMISSAOTERC2Coll.Find(delegate(LIS_COMISSAOTERCEntity item2) { return (item2.IDFUNCIONARIO == item.IDFUNCIONARIO); }) == null)
                        {
                            LIS_COMISSAOTERC2Coll.Add(item);
                        }
                    }

                    LIS_COMISSAOTERCColl = LIS_COMISSAOTERC2Coll;

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
      
    }
}
