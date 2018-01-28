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
    public partial class FrmVendaProduto : Form
    {
        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDOTICAP = new LIS_PRODUTOSPEDOTICAProvider();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
      

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaProduto()
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

            GetDropProduto();
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

        decimal TotalGeralPedido = 0;
        decimal QuantProduto = 0;
        private void PreencheGrid()
        {
            TotalGeralPedido = 0;
            QuantProduto = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            try
            {
                foreach (var LIS_PRODUTOSPEDOTICATy in LIS_PRODUTOSPEDOTICAColl)
                {
                    string DataEmissao = string.Empty;
                    if (LIS_PRODUTOSPEDOTICATy.IDPEDIDOOTICA != null)
                        DataEmissao = Convert.ToDateTime(LIS_PRODUTOSPEDOTICATy.DTEMISSAO).ToString("dd/MM/yyyy");

                    string TotalPedido = Convert.ToDecimal(LIS_PRODUTOSPEDOTICATy.VALORTOTAL).ToString("n2");

                    DataGridViewRow row2 = new DataGridViewRow();
                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(LIS_PRODUTOSPEDOTICATy.IDCLIENTE));
                    string NomeCliente = CLIENTETy.NOME;
                    string Telefone = CLIENTETy.TELEFONE1;
                    string Data = Convert.ToDateTime(LIS_PRODUTOSPEDOTICATy.DTEMISSAO).ToString("dd/MM/yyyy");
                    string Quantidade = Convert.ToDecimal(LIS_PRODUTOSPEDOTICATy.QUANTIDADE).ToString("n2");
                    row2.CreateCells(DataGriewDados, LIS_PRODUTOSPEDOTICATy.IDPEDIDOOTICA.ToString(), NomeCliente, Telefone, Data, Quantidade, Convert.ToDecimal(LIS_PRODUTOSPEDOTICATy.VALORTOTAL).ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeralPedido += Convert.ToDecimal(LIS_PRODUTOSPEDOTICATy.VALORTOTAL);
                    QuantProduto += Convert.ToDecimal(LIS_PRODUTOSPEDOTICATy.QUANTIDADE);
                }

                DataGridViewRow row2_2 = new DataGridViewRow();

                row2_2.CreateCells(DataGriewDados, string.Empty, string.Empty, string.Empty, "Total: ", QuantProduto.ToString("n2"), TotalGeralPedido.ToString("n2"));
                row2_2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2_2);


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na pesquisa  :" + ex.Message);
            }
        }

        private LIS_PRODUTOSPEDOTICACollection ProdutoRel(int IDFECHOSERVICO)
        {
            LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICA2Coll = new LIS_PRODUTOSPEDOTICACollection();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDFECHOSERVICO", "System.Int32", "=", IDFECHOSERVICO.ToString()));

            LIS_PRODUTOSPEDOTICA2Coll = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDOTICA2Coll;
        }
     
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas de Produto :" + cbProduto.Text);

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
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (Convert.ToInt32(cbProduto.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", Convert.ToInt32(cbProduto.SelectedValue).ToString()));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDOOTICA DESC");

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
            if (Convert.ToInt32(cbProduto.SelectedValue) < 0)
            {
                errorProvider1.SetError(cbProduto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else  if (!ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text))
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
      
    }
}
