using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;
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
    public partial class FrmVendaProduto : Form
    {
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
        LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();

        public FrmVendaProduto()
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

        private void FrmVendaProduto_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                bntDateSelecFinal.Image = Util.GetAddressImage(11);
                bntDateSelecInicial.Image = Util.GetAddressImage(11);

                msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);

                btnPesquisa.Image = Util.GetAddressImage(20);
                btnSair.Image = Util.GetAddressImage(21);

                GetFuncionario();
                GetDropProdutos();
                GetDropStatus();

                cbStatus.SelectedValue = 1;// Status Envaido

                USUARIOProvider USUARIOP = new USUARIOProvider();
                int idvendedor = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);
                cbFuncionario.SelectedValue = idvendedor;
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

                cbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }     

        private void GetDropProdutos()
        {
            try
            {
                PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
                PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetFuncionario()
        {
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
          
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

                    if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Convert.ToInt32(cbProduto.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                   

                    LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID desc");
                    
                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        decimal ValotTotalGeral = 0;
        private void PreencheGrid()
        {
            ValotTotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();
            CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
            foreach (var LIS_PRODUTONFCETy in LIS_PRODUTONFCEColl)
            {
                CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();
                CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(Convert.ToInt32(LIS_PRODUTONFCETy.CUPOMELETRONICOID));
                string NumCupom = CUPOMELETRONICOTy.NUMERONFCE.ToString();
                string Data = Convert.ToDateTime(CUPOMELETRONICOTy.DTEMISSAO).ToString("dd/MM/yyyy");
                string Produto = LIS_PRODUTONFCETy.NOMEPRODUTO;
                string Quantidade = Convert.ToDecimal(LIS_PRODUTONFCETy.QUANTIDADE).ToString("n3");
                string VlUnit = Convert.ToDecimal(LIS_PRODUTONFCETy.VALORUNITARIO).ToString("n3");
                string VlTotal = Convert.ToDecimal(LIS_PRODUTONFCETy.VALORTOTAL).ToString("n2");

                ValotTotalGeral += Convert.ToDecimal(LIS_PRODUTONFCETy.VALORTOTAL);

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, NumCupom, Data, Produto, Quantidade, VlUnit, VlTotal);
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);
            }

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "", "", "", "", "Total Geral: ", ValotTotalGeral.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text))
            {
                errorProvider1.SetError(msktDataInicial, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
            {
                errorProvider1.SetError(msktDataFinal, ConfigMessage.Default.CampoObrigatorio);
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Resumo Diário Caixa(Vendas Por Produto)";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Diário Caixa (Vendas Por Produto)");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChkTickePrint.Checked)
            {
                PrintTicketModelo2();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Diário Caixa (Vendas Por Produto)");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void PrintTicketModelo2()
        {
            try
            {
                Ticket ticket = new Ticket();
                decimal TotalGeralRel = 0;

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("Resumo Diário Caixa");
                ticket.AddSubHeaderLine("Vendas Por Produto");
                ticket.AddSubHeaderLine(msktDataInicial.Text + " a " + msktDataFinal.Text);

                foreach (var LIS_PRODUTONFCETy in LIS_PRODUTONFCEColl)
                {
                    
                     string ValorTotal = Convert.ToDecimal(LIS_PRODUTONFCETy.VALORTOTAL).ToString("n2");
                     TotalGeralRel += Convert.ToDecimal(LIS_PRODUTONFCETy.VALORTOTAL);
                     ticket.AddItem(LIS_PRODUTONFCETy.QUANTIDADE.ToString(), LIS_PRODUTONFCETy.NOMEPRODUTO, Util.LimiterText(ValorTotal, 20));
                    
                }

                ticket.AddTotal("Total Geral", TotalGeralRel.ToString("n2"));

                if (ticket.PrinterExists(BmsSoftware.ConfigSistema1.Default.impressoraticket))
                    ticket.PrintTicket(BmsSoftware.ConfigSistema1.Default.impressoraticket); //Nome da impresora , o caminho completo
                else
                    MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
            }

        }
    }
}
