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
    public partial class FrmVendaVendedorPedNormal : Form
    {
        LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl = new LIS_CUPOMELETRONICOCollection();
        LIS_CUPOMELETRONICOProvider LIS_CUPOMELETRONICOP = new LIS_CUPOMELETRONICOProvider();

        LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();

        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
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

        private void GetDropStatus()
        {
            try
            {
                STATUSNFCEProvider STATUSNFCEP = new STATUSNFCEProvider();
                STATUSNFCECollection STATUSNFCEColl = new STATUSNFCECollection();
                STATUSNFCEColl = STATUSNFCEP.ReadCollectionByParameter(null, "NOME");

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "STATUSNFCEID";

                STATUSNFCEEntity STATUSNFCETy = new STATUSNFCEEntity();
                STATUSNFCETy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSNFCETy.STATUSNFCEID = -1;
                STATUSNFCEColl.Add(STATUSNFCETy);

                Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity>(cbStatus.DisplayMember);

                STATUSNFCEColl.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSNFCEColl;

                cbStatus.SelectedValue = 1;
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

        decimal TotalGeralPedido = 0;
        decimal TotalGeralComissao = 0;
        private void PreencheGrid()
        {
            TotalGeralPedido = 0;
            TotalGeralComissao = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();


            foreach (var LIS_CUPOMELETRONICOTy in LIS_CUPOMELETRONICOColl)
            {
                string DataEmissao = string.Empty;
                if (LIS_CUPOMELETRONICOTy.CUPOMELETRONICOID != null)
                    DataEmissao = Convert.ToDateTime(LIS_CUPOMELETRONICOTy.DTEMISSAO).ToString("dd/MM/yyyy");

                string TotalPedido = Convert.ToDecimal(LIS_CUPOMELETRONICOTy.TOTALNOTA).ToString("n2");

                //Comissao Vendedor
                decimal? Porcetagem = 0;
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOEntity FUNCIONARIOtY = new FUNCIONARIOEntity();
                FUNCIONARIOtY = FUNCIONARIOP.Read(Convert.ToInt32(LIS_CUPOMELETRONICOTy.IDVENDEDOR));
                if (FUNCIONARIOtY != null)
                    Porcetagem = FUNCIONARIOtY.COMISSAO;
                // string TotalComissao = Convert.ToDecimal(LIS_PEDIDOTy.VALORCOMISSAO).ToString("n2");            
                string TotalComissao = Convert.ToDecimal((LIS_CUPOMELETRONICOTy.TOTALNOTA * Porcetagem)/100).ToString("n2");


                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, LIS_CUPOMELETRONICOTy.NUMERONFCE, DataEmissao, LIS_CUPOMELETRONICOTy.NOMECLIENTE, LIS_CUPOMELETRONICOTy.NOMESTATUS, LIS_CUPOMELETRONICOTy.NOMEVENDEDOR, TotalPedido, TotalComissao);
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

                    LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                    LIS_PRODUTONFCEColl = ProdutoRel(Convert.ToInt32(LIS_CUPOMELETRONICOTy.CUPOMELETRONICOID));
                    foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
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
            rowLinha.CreateCells(DataGriewDados, "", "", "", "", "TOTAL GERAL:", TotalGeralPedido, TotalGeralComissao, "");
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private LIS_PRODUTONFCECollection ProdutoRel(int CUPOMELETRONICOID)
        {
            LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl_2 = new LIS_PRODUTONFCECollection();
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CUPOMELETRONICOID.ToString()));

                LIS_PRODUTONFCEColl_2 = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                return LIS_PRODUTONFCEColl_2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return LIS_PRODUTONFCEColl_2;
            }
        }
       
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Vendas por Vendedor - NFCe");

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
                        RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));
                 
                    if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", Convert.ToInt32(cbCliente.SelectedValue).ToString()));

                    LIS_CUPOMELETRONICOColl = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO DESC");
                    
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
             string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas por Vendedor - NFCe");

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
                frm.TituloSelec = "Vendas por Vendedor - NFCe";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
      
    }
}
