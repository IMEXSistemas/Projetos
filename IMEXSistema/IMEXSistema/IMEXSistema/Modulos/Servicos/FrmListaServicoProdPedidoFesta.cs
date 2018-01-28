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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmListaServicoProdPedidoFesta : Form
    {

        LIS_PEDIDOFESTACollection LIS_PEDIDOFESTAColl = new LIS_PEDIDOFESTACollection();
        LIS_PEDIDOFESTAProvider LIS_PEDIDOFESTAP = new LIS_PEDIDOFESTAProvider();
        SERVICOProvider SERVICOP = new SERVICOProvider();

        SERVICOCollection SERVICOColl = new SERVICOCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmListaServicoProdPedidoFesta()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            
            GetDropServico(); 
            GetFuncionario();
            GetDropPecas();
            GetDropCliente();
            
        }

        private void GetDropCliente()
        {
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
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
 

        private void GetDropPecas()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetFuncionario()
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropServico()
        {
            try
            {
               
                SERVICOColl = SERVICOP.ReadCollectionByParameter(null, "NOME");

                cbServico.DisplayMember = "NOME";
                cbServico.ValueMember = "IDSERVICO";

                SERVICOEntity SERVICOTy = new SERVICOEntity();
                SERVICOTy.NOME = ConfigMessage.Default.MsgDrop;
                SERVICOTy.IDSERVICO = -1;
                SERVICOColl.Add(SERVICOTy);

                Phydeaux.Utilities.DynamicComparer<SERVICOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<SERVICOEntity>(cbServico.DisplayMember);

                SERVICOColl.Sort(comparer.Comparer);
                cbServico.DataSource = SERVICOColl;

                cbServico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheGrid()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {

                DataGriewDados.Rows.Clear();
                DataGridViewRow rowTop = new DataGridViewRow();
                rowTop.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "____________________________", "__________");
                rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowTop);

                decimal TotalGeralOS = 0;
                Decimal TotalGeralServico = 0;
                Decimal TotalGeralProduto = 0;

                foreach (var LIS_PEDIDOFESTATy in LIS_PEDIDOFESTAColl)
                {
                    string DataEmissao = string.Empty;
                    if (LIS_PEDIDOFESTATy.IDPEDIDOFESTA != null)
                        DataEmissao = Convert.ToDateTime(LIS_PEDIDOFESTATy.DTEMISSAO).ToString("dd/MM/yyyy");

                    string TotalOS = Convert.ToDecimal(LIS_PEDIDOFESTATy.TOTALPEDIDO).ToString("n2");
                    TotalGeralOS += Convert.ToDecimal(LIS_PEDIDOFESTATy.TOTALPEDIDO);

                    //Cabeçalho principal
                    if (LIS_PEDIDOFESTATy.IDPEDIDOFESTA != null)
                    {
                        DataGridViewRow row1 = new DataGridViewRow();
                        row1.CreateCells(DataGriewDados, "PEDIDO", "EMISSÃO", "CLIENTE", "STATUS", "FUNCIONÁRIO", "TOTAL");
                        row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row1);
                    }

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, LIS_PEDIDOFESTATy.IDPEDIDOFESTA.ToString().PadLeft(6, '0'), DataEmissao, LIS_PEDIDOFESTATy.NOMECLIENTE, LIS_PEDIDOFESTATy.NOMESTATUS, LIS_PEDIDOFESTATy.NOMEVENDEDOR, TotalOS);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);


                    if (LIS_PEDIDOFESTATy.IDPEDIDOFESTA != null)
                    {
                        //Dados do produto
                        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();

                        LIS_PRODUTOSPEDFESTAColl = ProdutoRel(Convert.ToInt32(LIS_PEDIDOFESTATy.IDPEDIDOFESTA));
                        if (LIS_PRODUTOSPEDFESTAColl.Count > 0 && chkExibirProdutos.Checked)
                        {
                            DataGridViewRow row3 = new DataGridViewRow();
                            row3.CreateCells(DataGriewDados, "PRODUTOS");
                            row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row3);

                            //Cabeçalho do produto
                            DataGridViewRow row4 = new DataGridViewRow();
                            row4.CreateCells(DataGriewDados, "Quant.", "Total", "Produtos");
                            row4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row4);

                            Decimal TotalProduto = 0;

                            foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
                            {
                                DataGridViewRow row5 = new DataGridViewRow();
                                row5.CreateCells(DataGriewDados, Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), item.NOMEPRODUTO);
                                row5.DefaultCellStyle.Font = new Font("Arial", 8);
                                DataGriewDados.Rows.Add(row5);
                                TotalProduto += Convert.ToDecimal(item.VALORTOTAL);
                            }

                            TotalGeralProduto += TotalProduto;

                            //Total de Produto
                            DataGridViewRow row5_2 = new DataGridViewRow();
                            row5_2.CreateCells(DataGriewDados, "Total: ", TotalProduto.ToString("n2"), string.Empty);
                            row5_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row5_2);
                        }


                        //Dados do Serviço                    
                        LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl = new LIS_SERVICOPEDIDOFESTACollection();
                        LIS_SERVICOPEDIDOFESTAColl = ServicoRel(Convert.ToInt32(LIS_PEDIDOFESTATy.IDPEDIDOFESTA));

                        if (LIS_SERVICOPEDIDOFESTAColl.Count > 0 && chkExibirServico.Checked)
                        {
                            DataGridViewRow row6 = new DataGridViewRow();
                            row6.CreateCells(DataGriewDados, "SERVIÇOS");
                            row6.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row6);
                            Decimal TotalServico = 0;

                            //Cabeçalho do Serviço
                            DataGridViewRow row7 = new DataGridViewRow();
                            row7.CreateCells(DataGriewDados, "Quant.", "Total", "Serviço");
                            row7.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row7);

                            foreach (LIS_SERVICOPEDIDOFESTAEntity item in LIS_SERVICOPEDIDOFESTAColl)
                            {
                                
                                DataGridViewRow row8 = new DataGridViewRow();
                                row8.CreateCells(DataGriewDados, Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), item.NOMESERVICO);
                                row8.DefaultCellStyle.Font = new Font("Arial", 8);
                                DataGriewDados.Rows.Add(row8);
                                TotalServico += Convert.ToDecimal(item.VALORTOTAL);
                                
                            }

                            TotalGeralServico += TotalServico;
                            //Total de Serviço
                            DataGridViewRow row8_2 = new DataGridViewRow();
                            row8_2.CreateCells(DataGriewDados, "Total: ", TotalServico.ToString("n2"), string.Empty);
                            row8_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                            DataGriewDados.Rows.Add(row8_2);

                        }

                    }


                    DataGridViewRow rowLinha = new DataGridViewRow();
                    rowLinha.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "____________________________", "__________");
                    rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowLinha);

                    this.Cursor = Cursors.Default;
                }

                if (chkExibirServico.Checked)
                {
                    //Total Geral de Servico
                    DataGridViewRow rowTotalGeralServico = new DataGridViewRow();
                    rowTotalGeralServico.CreateCells(DataGriewDados, "", "", "Total Geral de Serviço: " + TotalGeralServico.ToString("n2"));
                    rowTotalGeralServico.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowTotalGeralServico);
                }

                if (chkExibirProdutos.Checked)
                {
                    //Total Geral de Produto
                    DataGridViewRow rowTotalGeralProduto = new DataGridViewRow();
                    rowTotalGeralProduto.CreateCells(DataGriewDados, "", "", "Total Geral de Produtos: " + TotalGeralProduto.ToString("n2"));
                    rowTotalGeralProduto.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(rowTotalGeralProduto);
                }

                //Total Geral de OS
                DataGridViewRow rowTotalOS = new DataGridViewRow();
                rowTotalOS.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "Total Geral:", TotalGeralOS.ToString("n2"));
                rowTotalOS.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowTotalOS);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }  
        }

        private LIS_PRODUTOSPEDFESTACollection ProdutoRel(int IDPEDIDOFESTA)
        {
            LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl2 = new LIS_PRODUTOSPEDFESTACollection();
            LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

            try
            {
                RowRelatorio.Clear();
                if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Convert.ToInt32(cbProduto.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));

                LIS_PRODUTOSPEDFESTAColl2 = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

                return LIS_PRODUTOSPEDFESTAColl2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_PRODUTOSPEDFESTAColl2;
            }  
        }

        private LIS_SERVICOPEDIDOFESTACollection ServicoRel(int IDPEDIDOFESTA)
        {
            LIS_SERVICOPEDIDOFESTACollection LIS_SERVICOPEDIDOFESTAColl2 = new LIS_SERVICOPEDIDOFESTACollection();
            LIS_SERVICOPEDIDOFESTAProvider LIS_SERVICOPEDIDOFESTAP = new LIS_SERVICOPEDIDOFESTAProvider();
            try
            {
                RowRelatorio.Clear();

                if (Convert.ToInt32(cbServico.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDSERVICO", "System.Int32", "=", Convert.ToInt32(cbServico.SelectedValue).ToString()));

                RowRelatorio.Add(new RowsFiltro("IDPEDIDOFESTA", "System.Int32", "=", IDPEDIDOFESTA.ToString()));

                LIS_SERVICOPEDIDOFESTAColl2 = LIS_SERVICOPEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio);

                return LIS_SERVICOPEDIDOFESTAColl2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return LIS_SERVICOPEDIDOFESTAColl2;
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
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Serviço e Produtos");

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

        private void cbProduto_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Para pesquisar produto pressione CTRL+E";
        }

        private void cbFuncionario_Enter(object sender, EventArgs e)
        {
          
        }

        private void cbFuncionario_Leave(object sender, EventArgs e)
        {
           
        }

        private void cbProduto_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Empty;
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                try
                {
                    RowRelatorio.Clear();
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (rbOrcamento.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                    else if (rbVenda.Checked)
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                    if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));

                    LIS_PEDIDOFESTAColl = LIS_PEDIDOFESTAP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDOFESTA DESC");

                    PreencheGrid();

                }
                catch (Exception EX)
                {
                    MessageBox.Show("Erro na pesquisa!");
                    MessageBox.Show("Erro técnico: " + EX.Message);
                }
            }
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
         (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbCliente.SelectedValue = result;
                }
            }
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }



    }
}
