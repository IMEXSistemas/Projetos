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
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmVendaProdutoNFe : Form
    {
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

        LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
      

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int _IDPRODUTO = -1;

        public FrmVendaProdutoNFe()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
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
            GetDropProduto();

            cbProduto.SelectedValue = _IDPRODUTO;
        }       

        private void GetDropProduto()
        {
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
            PRODUTOSColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProduto.DisplayMember = "NOMEPRODUTO";
            cbProduto.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProduto.DisplayMember);

            PRODUTOSColl.Sort(comparer.Comparer);
            cbProduto.DataSource = PRODUTOSColl;

            cbProduto.SelectedIndex = 0;
        }

        
        private void PreencheGrid()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            foreach (var LIS_PRODUTOSTy in LIS_PRODUTOSColl)
            {
                
                string DataEmissao = string.Empty;
                
                DataGridViewRow row2 = new DataGridViewRow();
                string CODPRODUTO = LIS_PRODUTOSTy.IDPRODUTO.ToString();
                string NOMEPRODUTO = LIS_PRODUTOSTy.NOMEPRODUTO;
                string ENTRADA = RetornaQuantEntrada(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO)).ToString();
                string SAIDA = RetornaQuantSaida(Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO)).ToString();
                string SALDO = (Convert.ToDecimal(ENTRADA) - Convert.ToDecimal(SAIDA)).ToString();

                if (!chkProdSemMov.Checked)
                {
                    if (SALDO != "0")
                    {
                        row2.CreateCells(DataGriewDados, CODPRODUTO, NOMEPRODUTO, ENTRADA, SAIDA, SALDO);
                        row2.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row2);
                    }
                }
                else
                {
                    row2.CreateCells(DataGriewDados, CODPRODUTO, NOMEPRODUTO, ENTRADA, SAIDA, SALDO);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                }
            }

            this.Cursor = Cursors.Default;
        }
      
     
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas de Produto por Período :" + msktDataInicial.Text + " á " + msktDataFinal.Text);

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

                    if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Convert.ToInt32(cbProduto.SelectedValue).ToString()));

                    LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");

                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: "  + EX.Message);
                }
            }
        }

        private decimal RetornaQuantSaida(int Idproduto)
        {
            decimal result = 0;

            if (chkNFe.Checked)
            {
                LIS_PRODUTONFECollection LIS_PRODUTONFEColl2 = new LIS_PRODUTONFECollection();

                RowRelatorio.Clear();

                if (msktDataInicial.Text != "  /  /")
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));

                if (msktDataFinal.Text != "  /  /")
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));

                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Idproduto.ToString()));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                LIS_PRODUTONFEColl2 = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE DESC");

                NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl2)
                {
                    if (NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGCANCELADA.TrimEnd() == "N" && NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGENVIADA.TrimEnd() == "S")//Desconsidera nota fiscal cancelada
                        result += Convert.ToDecimal(item.QUANTIDADE);
                }
            }

             //Saida pelo Pedido de Venda
             if (chkSaidaPedido.Checked)
             {
                 LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                 LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
                 
                 RowRelatorio.Clear();
                 RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Idproduto.ToString()));

                 if (msktDataInicial.Text != "  /  /")
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));

                 if (msktDataFinal.Text != "  /  /")
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));
                 
                 LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);
                 PEDIDOProvider PEDIDOP = new PEDIDOProvider();

                 foreach (LIS_PRODUTOSPEDIDOEntity item2 in LIS_PRODUTOSPEDIDOColl)
                 {
                     if (PEDIDOP.Read(Convert.ToInt32(item2.IDPEDIDO)).FLAGORCAMENTO.TrimEnd() == "N")
                     {
                         result += Convert.ToDecimal(item2.QUANTIDADE);
                     }
                 }

             }


            //Saida Pedido2
             LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
             LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();
             RowRelatorio.Clear();
             RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Idproduto.ToString()));

             if (msktDataInicial.Text != "  /  /")
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));

             if (msktDataFinal.Text != "  /  /")
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));

             LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);
            
             foreach (LIS_PRODUTOSPEDIDOMTQEntity item2 in LIS_PRODUTOSPEDIDOMTQColl)
             {
                 if (item2.FLAGORCAMENTO.TrimEnd() == "N")
                 {
                     result += Convert.ToDecimal(item2.MT2) * Convert.ToDecimal(item2.QUANTIDADE);
                 }
             }

             ////Estoque Digisat 
             CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
             if (CONFISISTEMAP.Read(1).FLAGCPDIGISAT.TrimEnd() == "S")
             {
                 ITEVENDAS_ECFCollection ITEVENDAS_ECFColl = new ITEVENDAS_ECFCollection();
                 ITEVENDAS_ECFProvider ITEVENDAS_ECFP = new ITEVENDAS_ECFProvider();

                 RowRelatorio.Clear();
                 RowRelatorio.Add(new RowsFiltro("CANCELADO", "System.Int32", "=", "0"));
                 RowRelatorio.Add(new RowsFiltro("CODIGO", "System.Int32", "=", Idproduto.ToString()));

                 ITEVENDAS_ECFColl = ITEVENDAS_ECFP.ReadCollectionByParameter(RowRelatorio, "CUPOM DESC");
                 foreach (ITEVENDAS_ECFEntity item in ITEVENDAS_ECFColl)
                 {
                     result += Convert.ToDecimal(item.QTD);
                 }
             }

            return result;
        }

        private decimal RetornaQuantEntrada(int Idproduto)
        {
            decimal result = 0;

            //Movimentacao de Estoque
            LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
            LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=",  Idproduto.ToString()));
           if (msktDataInicial.Text != "  /  /")
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text)));

           if (msktDataFinal.Text != "  /  /")
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text)));
            
            LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
            {

                //Somente movimentação de entrada
                if (item.IDTIPOMOVIMENTACAO == 1)
                    result += Convert.ToDecimal(item.QUANTIDADE);
            }

            return result;
        }


        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (msktDataInicial.Text != "  /  /" && !ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text))
            {
                errorProvider1.SetError(msktDataInicial, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataFinal.Text != "  /  /" && !ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
            {
                errorProvider1.SetError(msktDataFinal, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbProduto.SelectedValue = result;
                    }
                }
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Vendas de Produto por Período :" + msktDataInicial.Text + " á " + msktDataFinal.Text;
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas de Produto por Período :" + msktDataInicial.Text + " á " + msktDataFinal.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
      
    }
}
