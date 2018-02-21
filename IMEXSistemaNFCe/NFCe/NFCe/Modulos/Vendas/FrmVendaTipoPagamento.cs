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
    public partial class FrmVendaTipoPagamento : Form
    {
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

         LIS_CUPOMELETRONICOProvider LIS_CUPOMELETRONICOP = new LIS_CUPOMELETRONICOProvider();
         LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl = new LIS_CUPOMELETRONICOCollection();
        LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_4 = new LIS_CUPOMELETRONICOCollection();

        decimal SaldoAbertura = 0;
        decimal TotalDinheiro = 0;
        decimal TotalCartaoCredito = 0;
        decimal TotalCartaoDebito = 0;
        decimal TotalCheque = 0;
        decimal TotalSangria = 0;        

        public FrmVendaTipoPagamento()
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

                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);

                btnPesquisa.Image = Util.GetAddressImage(20);
                btnSair.Image = Util.GetAddressImage(21);

                msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                GetFuncionario();
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
                    SaldoAbertura = BuscaSaldoAbertura();
                    TotalSangria = BuscaSangria();
                    TotalGeral = 0;
                    TotalDinheiro = 0;
                    TotalCartaoCredito = 0;
                    TotalCheque = 0;                    
                    DataGriewDados.Rows.Clear();

                    if (rbDinheiro.Checked)
                        PreencheGrid2(1); //1=Dinheiro
                    else if(rbCheque.Checked)
                        PreencheGrid2(2); //2=Cheque 
                    else if(rbCartaoCredito.Checked)
                        PreencheGrid2(3); //3=Cartão de Crédito
                    else if (rbCartaoDebito.Checked)
                        PreencheGrid2(4); //4=Cartão de Débito
                    else if (rbOutros.Checked)
                    {
                        PreencheGrid2(1); //1=Dinheiro
                        PreencheGrid2(2); //2=Cheque 
                        PreencheGrid2(3); //3=Cartão de Crédito
                        PreencheGrid2(4); //4=Cartão de Débito
                        PreencheGrid2(11); //11=Vale RefeiçãO
                        PreencheGrid2(99); ////99=Outros
                    }

                    DataGridViewRow rowRodape = new DataGridViewRow();
                    rowRodape.CreateCells(DataGriewDados, string.Empty, "Total Geral: ", TotalGeral.ToString("n2"));
                    rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape);

                    DataGridViewRow rowRodape_2 = new DataGridViewRow();
                    rowRodape_2.CreateCells(DataGriewDados, "---------------------------------------------------", "-----------------------", "-----------------------", "-----------------------");
                    rowRodape_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape_2);

                    DataGridViewRow rowRodape_Saldo = new DataGridViewRow();
                    decimal Saldo = (SaldoAbertura + TotalDinheiro + TotalCheque ) - TotalSangria;
                    rowRodape_Saldo.CreateCells(DataGriewDados, "Total da Abertura: " + SaldoAbertura.ToString("n2"), "", "");
                    rowRodape_Saldo.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape_Saldo);

                    DataGridViewRow rowRodape_Saldo2 = new DataGridViewRow();
                    rowRodape_Saldo2.CreateCells(DataGriewDados, "Total de Sangria: " + TotalSangria.ToString("n2"),"", "", "");
                    rowRodape_Saldo2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape_Saldo2);

                    DataGridViewRow rowRodape_Saldo3 = new DataGridViewRow();
                    rowRodape_Saldo3.CreateCells(DataGriewDados, "Saldo: " +  Saldo.ToString("n2"), "","","");
                    rowRodape_Saldo3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowRodape_Saldo3);

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        decimal SubTotalGeral = 0;
        decimal TotalGeral = 0;
        private void PreencheGrid2(int TIPO)
        {
            SubTotalGeral = 0;          

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
      
            DataGridViewRow rowTitulo = new DataGridViewRow();
            rowTitulo.CreateCells(DataGriewDados, "Tipo de Pagamento", string.Empty, string.Empty, string.Empty);
            rowTitulo.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowTitulo);          
                
            ///Dinheiro
            
            LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(TIPO);

            SubTotalGeral = 0;

            DataGridViewRow rowSubTitulo = new DataGridViewRow();
            rowSubTitulo.CreateCells(DataGriewDados, "", "Data", "Vl. Total", "Situação");
            rowSubTitulo.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowSubTitulo);

            string TituloForma = string.Empty;
            if (TIPO == 1) // 1=Dinheiro
                TituloForma = "Dinheiro";
            else if (TIPO == 2)  //2=Cheque 
                TituloForma = "Cheque";
            else if (TIPO == 3)  //3=Cartão de Crédito
                TituloForma = "Cartão de Crédito";
            else if (TIPO == 4)  //4=Cartão de Débito
                TituloForma = "Cartão de Débito";
            else if (TIPO == 11)  //11=Vale Refeição
                TituloForma = "11=Vale Refeição";
            else if (TIPO == 99)  //99=Outros
                TituloForma = "Outros";

            DataGridViewRow rowHeader = new DataGridViewRow();
            rowHeader.CreateCells(DataGriewDados, TituloForma, string.Empty, string.Empty, string.Empty);
            rowHeader.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowHeader);

            foreach (var LIS_CUPOMELETRONICOTy_4 in LIS_CUPOMELETRONICOColl_4)
            {
                string NumCupom = LIS_CUPOMELETRONICOTy_4.NUMERONFCE.ToString();
                string Data = Convert.ToDateTime(LIS_CUPOMELETRONICOTy_4.DTEMISSAO).ToString("dd/MM/yyyy");
                string VlTotalPago = "0";  Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.TOTALNOTA).ToString("n2");
                if (TIPO == 1) // 1=Dinheiro
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGODINHEIRO - LIS_CUPOMELETRONICOTy_4.VALORTROCO).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGODINHEIRO - LIS_CUPOMELETRONICOTy_4.VALORTROCO);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGODINHEIRO - LIS_CUPOMELETRONICOTy_4.VALORTROCO);
                    TotalDinheiro += Convert.ToDecimal(VlTotalPago);
                }
                else if (TIPO == 2)  //2=Cheque 
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALORPAGOCHEQUE).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALORPAGOCHEQUE);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALORPAGOCHEQUE);
                    TotalCheque += Convert.ToDecimal(VlTotalPago);
                }
                else if (TIPO == 3)  //3=Cartão de Crédito
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAOCREDITO).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAOCREDITO);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAOCREDITO);
                    TotalCartaoCredito += Convert.ToDecimal(VlTotalPago);
                }
                else if (TIPO == 4)  //4=Cartão de Débito
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAODEBITO).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAODEBITO);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOCARTAODEBITO);
                    TotalCartaoDebito += Convert.ToDecimal(VlTotalPago);
                }
                else if (TIPO == 11)  //11=Vale Refeição
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOVALEREFEICAO).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOVALEREFEICAO);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOVALEREFEICAO);
                }
                else if (TIPO == 99)  //99=Outros
                {
                    VlTotalPago = Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOOUTROS).ToString("n2");
                    SubTotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOOUTROS);
                    TotalGeral += Convert.ToDecimal(LIS_CUPOMELETRONICOTy_4.VALOPAGOOUTROS);
                }

                string Status = LIS_CUPOMELETRONICOTy_4.NOMESTATUS;


                DataGridViewRow row_BODY = new DataGridViewRow();
                row_BODY.CreateCells(DataGriewDados, NumCupom, Data, VlTotalPago, Status);
                row_BODY.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row_BODY);
            }

            DataGridViewRow rowSubTotal = new DataGridViewRow();
            rowSubTotal.CreateCells(DataGriewDados, string.Empty, "Sub Total: ", SubTotalGeral.ToString("n2"));
            rowSubTotal.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowSubTotal);
           

            DataGridViewRow rowRodape_1 = new DataGridViewRow();
            rowRodape_1.CreateCells(DataGriewDados, "---------------------------------------------------", "-----------------------", "-----------------------", "-----------------------");
            rowRodape_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowRodape_1);

          

            //DataGridViewRow rowRodape = new DataGridViewRow();
            //rowRodape.CreateCells(DataGriewDados, string.Empty, "Total Geral: ", TotalGeral.ToString("n2"));
            //rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            //DataGriewDados.Rows.Add(rowRodape);


            this.Cursor = Cursors.Default;
        }

        private decimal  BuscaSaldoAbertura()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            decimal result = 0;
            ABERTURACAIXACollection ABERTURACAIXAColl = new ABERTURACAIXACollection();
            ABERTURACAIXAProvider ABERTURACAIXAP = new ABERTURACAIXAProvider();
            
            try
            {

                RowRelatorio.Clear();
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "<=", DataFinal));

                ABERTURACAIXAColl = ABERTURACAIXAP.ReadCollectionByParameter(RowRelatorio);

                foreach (var item in ABERTURACAIXAColl)
                {
                    result += Convert.ToDecimal(item.VALOR);
                }

                return result;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private decimal BuscaSangria()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            decimal result = 0;
            SANGRIACAIXACollection SANGRIACAIXAColl = new SANGRIACAIXACollection();
            SANGRIACAIXAProvider SANGRIACAIXAP = new SANGRIACAIXAProvider();

            try
            {
               

                RowRelatorio.Clear();
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "<=", DataFinal));

                SANGRIACAIXAColl = SANGRIACAIXAP.ReadCollectionByParameter(RowRelatorio);

                foreach (var item in SANGRIACAIXAColl)
                {
                    result += Convert.ToDecimal(item.VALOR);
                }

                return result;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private LIS_CUPOMELETRONICOCollection BuscaTipoPagtoCupom(int TIPO)
        {
            LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_3 = new LIS_CUPOMELETRONICOCollection();

            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                RowRelatorio.Clear();
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                if (TIPO == 1) // 1=Dinheiro
                    RowRelatorio.Add(new RowsFiltro("ValoPagoDinheiro", "System.Decimal", ">", "0"));
                else if (TIPO == 2)  //2=Cheque 
                    RowRelatorio.Add(new RowsFiltro("ValorPagoCheque", "System.Decimal", ">", "0"));
                else if (TIPO == 3)  //3=Cartão de Crédito
                    RowRelatorio.Add(new RowsFiltro("ValoPagoCartaoCredito", "System.Decimal", ">", "0"));
                else if (TIPO == 4)  //4=Cartão de Débito
                    RowRelatorio.Add(new RowsFiltro("ValoPagoCartaoDebito", "System.Decimal", ">", "0"));
                else if (TIPO == 11)  //11=Vale Refeiçã
                    RowRelatorio.Add(new RowsFiltro("ValoPagoValeRefeicao", "System.Decimal", ">", "0"));
                else if (TIPO == 99)  //99=Outros
                    RowRelatorio.Add(new RowsFiltro("ValoPagoOutros", "System.Decimal", ">", "0"));


                if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                LIS_CUPOMELETRONICOColl_3 = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID desc");

                return LIS_CUPOMELETRONICOColl_3;

                this.Cursor = Cursors.Default;	
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;	
                return LIS_CUPOMELETRONICOColl_3;
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
                frm.TituloSelec = "Resumo Diário (Vendas Por Tipo de Pagamento)";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Diário (Vendas Por Tipo de Pagamento)");

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
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo Diário (Vendas Por Tipo de Pagamento)");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void PrintTicketModelo2()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                Ticket ticket = new Ticket();
                ticket.DrawItems_b = false;

                
                if(rbDinheiro.Checked)
                    LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(1);
                else if (rbCheque.Checked)
                    LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(2);
                else if (rbCartaoCredito.Checked)
                    LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(3);
                else if (rbCartaoDebito.Checked)
                    LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(4);
                else if (rbOutros.Checked)
                    LIS_CUPOMELETRONICOColl_4 = BuscaTipoPagtoCupom(5);

                SaldoAbertura = BuscaSaldoAbertura();
                TotalSangria = BuscaSangria();                            

                //Dados da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.NOMECLIENTE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.CIDADE, 50) + "-" + EMPRESATy.UF);
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.TELEFONE, 50));
                ticket.AddHeaderLine(Util.LimiterText(EMPRESATy.EMAIL, 50));

                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("Resumo Diário");
                ticket.AddSubHeaderLine("Vendas Por Tipo de Pagamento");
                ticket.AddSubHeaderLine(msktDataInicial.Text + " a " + msktDataFinal.Text);

                if (rbDinheiro.Checked) // 1=Dinheiro
                {
                    ticket.AddSubHeaderLine("Dinheiro: " + TotalDinheiro.ToString("n2"));
                }
                else if (rbCheque.Checked)  //2=Cheque 
                {
                    ticket.AddSubHeaderLine("Cheque: " + TotalCheque.ToString("n2"));
                }
                else if (rbCartaoCredito.Checked)  //3=Cartão de Crédito
                {
                    ticket.AddSubHeaderLine("Cartão de Crédito " + TotalCartaoCredito.ToString("n2"));
                }
                else if (rbCartaoDebito.Checked)  //4=Cartão de Débito
                {
                    ticket.AddSubHeaderLine("Cartão de Débito: " + TotalCartaoDebito.ToString("n2"));
                }
                else
                {
                    //Dinheiro
                    ticket.AddSubHeaderLine("Dinheiro: " + TotalDinheiro.ToString("n2"));

                    //cheque
                    ticket.AddSubHeaderLine("Cheque: " + TotalCheque.ToString("n2"));

                    //Cartao de Credito
                    ticket.AddSubHeaderLine("Cartão de Crédito " + TotalCartaoCredito.ToString("n2"));

                    //Cartao de Debito
                    ticket.AddSubHeaderLine("Cartão de Débito: " + TotalCartaoDebito.ToString("n2"));
                }                  
                

                decimal Saldo = (SaldoAbertura + TotalDinheiro + TotalCheque) - TotalSangria;

                ticket.AddTotal("Total Geral", TotalGeral.ToString("n2"));
                ticket.AddTotal("Abertura", SaldoAbertura.ToString("n2"));
                ticket.AddTotal("Sangria", TotalSangria.ToString("n2"));
                ticket.AddTotal("Saldo", Saldo.ToString("n2"));

                if (ticket.PrinterExists(BmsSoftware.ConfigSistema1.Default.impressoraticket))
                    ticket.PrintTicket(BmsSoftware.ConfigSistema1.Default.impressoraticket); //Nome da impresora , o caminho completo
                else
                    MessageBox.Show("Impressora não localizada",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
               this.Cursor = Cursors.Default;

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
