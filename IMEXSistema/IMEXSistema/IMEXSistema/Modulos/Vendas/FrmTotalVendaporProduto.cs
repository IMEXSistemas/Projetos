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
    public partial class FrmTotalVendaporProduto : Form
    {
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        MARCAProvider MARCAP = new MARCAProvider();

        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmTotalVendaporProduto()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropMarca();

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

            GetFuncionario();          
            GetTransporte();

            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void GetDropMarca()
        {
            MARCAProvider MARCAP = new MARCAProvider();
            MARCACollection MarcaColl = new MARCACollection();
            MarcaColl = MARCAP.ReadCollectionByParameter(null, "NOME");

            cbMarca.DisplayMember = "NOME";
            cbMarca.ValueMember = "IDMARCA";

            MARCAEntity MARCATy = new MARCAEntity();
            MARCATy.NOME = ConfigMessage.Default.MsgDrop;
            MARCATy.IDMARCA = -1;
            MarcaColl.Add(MARCATy);

            Phydeaux.Utilities.DynamicComparer<MARCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MARCAEntity>(cbMarca.DisplayMember);

            MarcaColl.Sort(comparer.Comparer);
            cbMarca.DataSource = MarcaColl;

            cbMarca.SelectedIndex = 0;
        }


        private void GetTransporte()
        {
            try
            {
                TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
                TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

                cbTransportador.DisplayMember = "NOME";
                cbTransportador.ValueMember = "IDTRANSPORTADORA";

                TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
                TRANSPORTADORATy.NOME = ConfigMessage.Default.MsgDrop;
                TRANSPORTADORATy.IDTRANSPORTADORA = -1;
                TRANSPORTADORAColl.Add(TRANSPORTADORATy);

                Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(cbTransportador.DisplayMember);

                TRANSPORTADORAColl.Sort(comparer.Comparer);
                cbTransportador.DataSource = TRANSPORTADORAColl;

                cbTransportador.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        decimal TOTALGERALQUANTIDADE = 0;
        decimal TOTALGERALPESO = 0;
        decimal TOTALVENDA = 0;
        decimal TOTALGERALVENDA = 0;
        private void PreencheGrid2()
        {
            TOTALGERALQUANTIDADE = 0;
            TOTALGERALPESO = 0;
            TOTALVENDA = 0;
            TOTALGERALVENDA = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                DataGriewDados.Rows.Clear();

                //Remove as Cidades Repetidas
                LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl_2 = new LIS_PRODUTOSPEDIDOCollection();
                foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                {

                    if (LIS_PRODUTOSPEDIDOColl_2.Find(delegate(LIS_PRODUTOSPEDIDOEntity item2) { return (item2.IDPRODUTO == item.IDPRODUTO); }) == null)
                    {
                        LIS_PRODUTOSPEDIDOColl_2.Add(item);
                    }
                }

                DataGridViewRow row1_2 = new DataGridViewRow();

                foreach (var LIS_PRODUTOSPEDIDOTy in LIS_PRODUTOSPEDIDOColl_2)
                {
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl_5 = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOColl_5 = BuscaPedidoPeloProduto(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDPRODUTO));
                    PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                    PRODUTOSTy = PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDPRODUTO));

                    string NomeMarca = string.Empty;
                    if (LIS_PRODUTOSPEDIDOTy.IDMARCA != null && LIS_PRODUTOSPEDIDOTy.IDMARCA > 0)
                    {
                        NomeMarca = MARCAP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDMARCA)).NOME;
                    }

                    decimal QUANTIDADE = 0;
                    decimal PESO = 0;
                    TOTALVENDA = 0;
                    foreach (var LIS_PRODUTOSPEDIDOTy_2 in LIS_PRODUTOSPEDIDOColl_5)
                    {
                        QUANTIDADE += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy_2.QUANTIDADE);
                        //PESO += Convert.ToDecimal(PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSPEDIDOTy.IDPRODUTO)).PESO) * QUANTIDADE;
                        TOTALVENDA += Convert.ToDecimal(LIS_PRODUTOSPEDIDOTy_2.VALORTOTAL);
                    }

                    TOTALGERALVENDA += TOTALVENDA;

                    TOTALGERALQUANTIDADE += QUANTIDADE;
                    TOTALGERALPESO += PESO;

                    string Codigo = LIS_PRODUTOSPEDIDOTy.IDPRODUTO.ToString();
                    string Cod_Referencia = PRODUTOSTy.CODPRODUTOFORNECEDOR;
                    string Cod_Barra = PRODUTOSTy.CODBARRA;
                    string NOMEPRODUTO = LIS_PRODUTOSPEDIDOTy.NOMEPRODUTO;
                    string VLUNITARIOMEDIO = Convert.ToDecimal(TOTALVENDA / QUANTIDADE).ToString("n2");


                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, Codigo, Cod_Referencia, Cod_Barra, LIS_PRODUTOSPEDIDOTy.NOMEPRODUTO, NomeMarca, VLUNITARIOMEDIO, QUANTIDADE.ToString("n2"), TOTALVENDA.ToString("n2"), PESO.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                }

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "", "", "", "", "", "TOTAL GERAL", TOTALGERALQUANTIDADE.ToString("n2"), TOTALGERALVENDA.ToString("n2"), TOTALGERALPESO.ToString("n2"));
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

        private LIS_PRODUTOSPEDIDOCollection BuscaPedidoPeloProduto(int IDPRODUTO)
        {

            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl_4 = new LIS_PRODUTOSPEDIDOCollection();
          

            try
            {
                RowRelatorio.Clear();
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                if (Convert.ToInt32(cbTransportador.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDTRANSPORTES", "System.Int32", "=", Convert.ToInt32(cbTransportador.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));

                if (Convert.ToInt32(cbMarca.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", cbMarca.SelectedValue.ToString()));


                if (rbDataEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
                }
                else
                {
                    RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", "<=", DataFinal));
                }

                if (rdOrcamento.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                }
                else if (rdVenda.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                }       

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    LIS_PRODUTOSPEDIDOColl_4 = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDTRANSPORTES, IDPEDIDO");
                else
                    LIS_PRODUTOSPEDIDOColl_4 = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                return LIS_PRODUTOSPEDIDOColl_4;
            }
            catch (Exception ex)
            {
                return LIS_PRODUTOSPEDIDOColl_4;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
           
           
        }
     
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Total de Venda por Produto");

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

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            
            
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbTransportador.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDTRANSPORTES", "System.Int32", "=", Convert.ToInt32(cbTransportador.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbMarca.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDMARCA", "System.Int32", "=", cbMarca.SelectedValue.ToString()));

                    if(rdOrcamento.Checked)
                    {
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO",  "System.String", "=", "S"));
                    }
                    else if (rdVenda.Checked)
                    {
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO",  "System.String", "=", "N"));
                    }                    

                    if(rbDataEmissao.Checked)
                    {
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
                    }
                    else
                    {
                        RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", ">=", DataInicial));
                        RowRelatorio.Add(new RowsFiltro("DATAENTREGA", "System.DateTime", "<=", DataFinal));
                    }

                    if(rbOrdenarProduto.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                    else if(rbOrdenarCodigo.Checked)
                        LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPRODUTO");

                    
                    PreencheGrid2();

                    this.Cursor = Cursors.Default;
                }
                catch (Exception EX)
                {
                    this.Cursor = Cursors.Default;
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

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Total de Venda por Produto";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Total de Venda por Produto");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
      
    }
}
