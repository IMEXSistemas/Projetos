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
    public partial class FrmOutrosTipodePagamento : Form
    {
       
        PEDIDOCollection PEDIDOColl = new PEDIDOCollection();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();     

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmOutrosTipodePagamento()
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

        private void FrmProdutosMaisVendidos_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            GetFuncionario();
            //Busca o Funcionario logado
            USUARIOEntity USUARIOTY = new USUARIOEntity();
            USUARIOProvider USUARIOP = new USUARIOProvider();
            USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
            cbFuncionario.SelectedValue = USUARIOTY.IDFUNCIONARIO;
        }

        private void GetFuncionario()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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
                  
                    if(Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                    if (mkdHoraInicial.Text != "  :")
                    {
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial + " " + mkdHoraInicial.Text));
                    }
                    else
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));

                    if (mkdHoraFinal.Text != "  :")
                    {
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal + " " + mkdHoraFinal.Text));
                                           }
                    else
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (rdOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    else if (rdVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    PEDIDOColl = PEDIDOP.ReadCollectionByParameter(RowRelatorio);
                    
                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        decimal ValorTotalGeral = 0;
        decimal TIPOPAGTODINHEIRO = 0;
        decimal TIPOPAGTOCHEQUE = 0;
        decimal TIPOPAGTOCARTAODEBITO = 0;
        decimal TIPOPAGTOCARTAOCREDITO = 0;
        private void PreencheGrid()
        {
            try
            {
                ValorTotalGeral = 0;
                TIPOPAGTODINHEIRO = 0;
                TIPOPAGTOCHEQUE = 0;
                TIPOPAGTOCARTAODEBITO = 0;
                TIPOPAGTOCARTAOCREDITO = 0;

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DataGriewDados.Rows.Clear();                

                //Soma total de outros tipos
                foreach (var PEDIDOTy in PEDIDOColl)
                {
                    TIPOPAGTODINHEIRO += Convert.ToDecimal(PEDIDOTy.TIPOPAGTODINHEIRO);
                    TIPOPAGTOCHEQUE += Convert.ToDecimal(PEDIDOTy.TIPOPAGTOCHEQUE);
                    TIPOPAGTOCARTAODEBITO += Convert.ToDecimal(PEDIDOTy.TIPOPAGTOCARTAODEBITO);
                    TIPOPAGTOCARTAOCREDITO += Convert.ToDecimal(PEDIDOTy.TIPOPAGTOCARTAOCREDITO);
                }


                //Dinheiro
                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, "Dinheiro", TIPOPAGTODINHEIRO.ToString("n2"));
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);
                ValorTotalGeral += TIPOPAGTODINHEIRO;

                //Cheque
                DataGridViewRow row3 = new DataGridViewRow();
                row3.CreateCells(DataGriewDados, "Cheque", TIPOPAGTOCHEQUE.ToString("n2"));
                row3.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row3);
                ValorTotalGeral += TIPOPAGTOCHEQUE;

                //Cartão de Debito
                DataGridViewRow row4 = new DataGridViewRow();
                row4.CreateCells(DataGriewDados, "Cartão de Débito", TIPOPAGTOCARTAODEBITO.ToString("n2"));
                row4.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row4);
                ValorTotalGeral += TIPOPAGTOCARTAODEBITO;

                //Cartão de Credito
                DataGridViewRow row5 = new DataGridViewRow();
                row5.CreateCells(DataGriewDados, "Cartão de Crédito", TIPOPAGTOCARTAOCREDITO.ToString("n2"));
                row5.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row5);
                ValorTotalGeral += TIPOPAGTOCARTAOCREDITO;

                DataGridViewRow rowRodape0 = new DataGridViewRow();
                rowRodape0.CreateCells(DataGriewDados, string.Empty, string.Empty);
                rowRodape0.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowRodape0);

                DataGridViewRow rowRodape = new DataGridViewRow();
                rowRodape.CreateCells(DataGriewDados, "VALOR TOTAL ========================================================>", ValorTotalGeral.ToString("n2"));
                rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowRodape);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (chkImpressaoTicket.Checked)
            {
                PrintTicketModelo2();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo do Caixa Diário (Tipos de Pagamentos)");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);
            }
        }

        private void PrintTicketModelo2()
        {
            try
            {
                Ticket ticket = new Ticket();
                ticket.DrawItems_b = false;

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("Resumo do Caixa Diário");
                ticket.AddSubHeaderLine("Tipos de Pagamentos");
                ticket.AddSubHeaderLine("Vendedor: " + cbFuncionario.Text);
                ticket.AddSubHeaderLine(msktDataInicial.Text + " a " + msktDataFinal.Text);

                ticket.AddTotal("Dinheiro..........", TIPOPAGTODINHEIRO.ToString("n2"));
                ticket.AddTotal("Cheque............", TIPOPAGTOCHEQUE.ToString("n2"));                
                ticket.AddTotal("Cartão de Débito..", TIPOPAGTOCARTAODEBITO.ToString("n2"));
                ticket.AddTotal("Cartão de Crédito.", TIPOPAGTOCARTAOCREDITO.ToString("n2"));
                ticket.AddTotal("TOTAL.............", ValorTotalGeral.ToString("n2"));              

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

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void mkdHoraInicial_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraInicial.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraInicial, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void mkdHoraFinal_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mkdHoraFinal.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mkdHoraFinal.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora);
                errorProvider1.SetError(mkdHoraFinal, ConfigMessage.Default.MsgErroHora);
            }
        }
    }
}
