﻿using System;
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
    public partial class FrmVendaVendedorPedNormal : Form
    {
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaVendedorPedNormal()
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

            GetFuncionario();
            GetDropStatus();
            GetDropCentroCusto();
            GetDropCliente();
        }

        private void GetDropCliente()
        {
            try
            {
                CLIENTEProvider CLIENTEP = new BMSworks.Firebird.CLIENTEProvider();
                CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null, "NOME");

                cbCliente.DisplayMember = "NOME";
                cbCliente.ValueMember = "IDCLIENTE";

                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                CLIENTETy.IDCLIENTE = -1;
                CLIENTEColl.Add(CLIENTETy);

                Phydeaux.Utilities.DynamicComparer<CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLIENTEEntity>(cbCliente.DisplayMember);

                CLIENTEColl.Sort(comparer.Comparer);
                cbCliente.DataSource = CLIENTEColl;

                cbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCentroCusto()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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
            TotalGeralPedido = 0;
            TotalGeralComissao = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            //DataGridViewRow rowTop = new DataGridViewRow();
            //rowTop.CreateCells(DataGriewDados, "_________", "__________", "_________________________________________", "_____________________", "_______________________", "__________", "__________", "______________");
            //rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(rowTop);

            //DataGridViewRow row1 = new DataGridViewRow();
            //row1.CreateCells(DataGriewDados, "PEDIDO", "EMISSÃO", "CLIENTE", "STATUS", "FUNCIONÁRIO", "TOTAL", "COMISSÃO", "CENTRO CUSTO");
            //row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(row1);

            foreach (var LIS_PEDIDOTy in LIS_PEDIDOColl)
            {
                string DataEmissao = string.Empty;
                if (LIS_PEDIDOTy.IDPEDIDO != null)
                    DataEmissao = Convert.ToDateTime(LIS_PEDIDOTy.DTEMISSAO).ToString("dd/MM/yyyy");

                string TotalPedido = Convert.ToDecimal(LIS_PEDIDOTy.TOTALPEDIDO).ToString("n2");
                string TotalComissao = Convert.ToDecimal(LIS_PEDIDOTy.VALORCOMISSAO).ToString("n2");            


                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, LIS_PEDIDOTy.IDPEDIDO, DataEmissao, LIS_PEDIDOTy.NOMECLIENTE, LIS_PEDIDOTy.NOMESTATUS, LIS_PEDIDOTy.NOMEVENDEDOR, TotalPedido, TotalComissao, LIS_PEDIDOTy.CENTROCUSTO);
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);

                TotalGeralPedido += Convert.ToDecimal(TotalPedido);
                TotalGeralComissao += Convert.ToDecimal(TotalComissao);

                if (chkExibirProdutos.Checked)
                {
                    //Produtos
                    DataGridViewRow row3 = new DataGridViewRow();
                    row3.CreateCells(DataGriewDados, "PRODUTOS");
                    row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row3);

                    //Cabeçalho do produto
                    DataGridViewRow row4 = new DataGridViewRow();
                    row4.CreateCells(DataGriewDados, "Quant.", "Total", "Produtos");
                    row4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row4);

                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOColl = ProdutoRel(Convert.ToInt32(LIS_PEDIDOTy.IDPEDIDO));
                    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        DataGridViewRow row5 = new DataGridViewRow();
                        row5.CreateCells(DataGriewDados, Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), item.NOMEPRODUTO);
                        row5.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row5);
                    }


                    DataGridViewRow rowLinha_2 = new DataGridViewRow();
                    rowLinha_2.CreateCells(DataGriewDados, "_________", "__________", "________________________________________", "______________________", "_____________________", "__________", "__________", "______________"); 
                    rowLinha_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowLinha_2);
                }
            }

            DataGridViewRow rowLinha_1 = new DataGridViewRow();
            rowLinha_1.CreateCells(DataGriewDados, "", "", "", "", "", "", "", "");
            rowLinha_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha_1);

            DataGridViewRow rowLinha = new DataGridViewRow();
           // rowLinha.CreateCells(DataGriewDados, "_________", "__________", "___________________________________________", "_________________________", "TOTAL GERAL:", TotalGeralPedido, TotalGeralComissao, "______________");
            rowLinha.CreateCells(DataGriewDados, "", "", "", "", "TOTAL GERAL:", TotalGeralPedido, TotalGeralComissao, "");
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
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
       
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Vendas por Vendedor");

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

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTOS", "System.Int32", "=", Convert.ToInt32(cbCentroCusto.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Convert.ToInt32(cbCliente.SelectedValue).ToString()));


                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=","S"));
                    else if (rdVenda.Checked)
                            RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));


                    LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO DESC");
                    
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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
             string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas por Vendedor");

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
                frm.TituloSelec = "Vendas por Vendedor";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
      
    }
}
