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
    public partial class FrmVendaCFOP : Form
    {
        LIS_PRODUTONFECollection LIS_PRODUTONFEColl_1 = new LIS_PRODUTONFECollection();
        LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider(); 
     
        PRODUTOSCollection PRODUTOSColl = new PRODUTOSCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public string DataInicial = string.Empty;
        public string DataFinal = string.Empty;

        public FrmVendaCFOP()
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
        
            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            msktDataInicial.Text = DataInicial;
            msktDataFinal.Text = DataFinal;

            GetDropCFOP();
        }

        private void GetDropCFOP()
        {
            try
            {
                CFOPCollection CFOPColl = new CFOPCollection();
                CFOPProvider CFOPP = new CFOPProvider();
                CFOPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

                cbCFOP.DisplayMember = "CODCFOP";
                cbCFOP.ValueMember = "IDCFOP";

                CFOPEntity CFOPTy = new CFOPEntity();
                CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
                CFOPTy.IDCFOP = -1;
                CFOPColl.Add(CFOPTy);

                Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>(cbCFOP.DisplayMember);

                CFOPColl.Sort(comparer.Comparer);
                cbCFOP.DataSource = CFOPColl;

                cbCFOP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    

        decimal  TotalGeral = 0;
        decimal TotalQuantidade = 0;
        decimal SubTotal = 0;
        private void PreencheGrid2()
        {
            TotalGeral = 0;
            TotalQuantidade = 0;
            SubTotal = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            //Remove as Cidades Repetidas
            LIS_PRODUTONFECollection LIS_PRODUTONFEColl_2 = new LIS_PRODUTONFECollection();
            foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl_1)
            {

                if (LIS_PRODUTONFEColl_2.Find(delegate(LIS_PRODUTONFEEntity item2) { return (item2.IDCFOP == item.IDCFOP); }) == null)
                {
                    LIS_PRODUTONFEColl_2.Add(item);
                }
            }

            //Cabeçalho Nome Cidade
            DataGridViewRow row1_2 = new DataGridViewRow();
            row1_2.CreateCells(DataGriewDados, "CFOP:","", "", "", "");
            row1_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(row1_2);

            foreach (var LIS_PRODUTONFETy in LIS_PRODUTONFEColl_2)
            {
               
                //Busca Dados do produto por cidade
                LIS_PRODUTONFECollection LIS_PRODUTONFEColl3 = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEColl3 = BuscaProdutoCFOP(Convert.ToInt32(LIS_PRODUTONFETy.IDCFOP));

                DataGridViewRow row_header = new DataGridViewRow();
                row_header.CreateCells(DataGriewDados, LIS_PRODUTONFETy.CODCFOP, "", "", "", "");
                row_header.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row_header);

                DataGridViewRow row_header2 = new DataGridViewRow();
                row_header2.CreateCells(DataGriewDados, "Produto", "Quant.", "DT Emissão", "Nota Fiscal", "Total");
                row_header2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row_header2);

                SubTotal = 0;
              foreach (var LIS_PRODUTONFETy_3 in LIS_PRODUTONFEColl3)
                {
                    string DataEmissao = Convert.ToDateTime(LIS_PRODUTONFETy_3.DTEMISSAO).ToString("dd/MM/yyyy");
                    string NomeProduto = LIS_PRODUTONFETy_3.NOMEPRODUTO;
                    string QuantProduto = LIS_PRODUTONFETy_3.QUANTIDADE.ToString();
                    TotalQuantidade += Convert.ToDecimal(LIS_PRODUTONFETy_3.QUANTIDADE);
                    string NumNF = LIS_PRODUTONFETy_3.NOTAFISCALE.ToString().PadLeft(6, '0');
                    string TotalProduto = Convert.ToDecimal(LIS_PRODUTONFETy_3.VALORTOTAL).ToString("n2");
                    SubTotal += Convert.ToDecimal(LIS_PRODUTONFETy_3.VALORTOTAL);
                    TotalGeral += Convert.ToDecimal(LIS_PRODUTONFETy_3.VALORTOTAL);

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, NomeProduto, QuantProduto, DataEmissao, NumNF, TotalProduto);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);
                }

              if (LIS_PRODUTONFEColl3.Count > 0)
              {

                  DataGridViewRow row1_5 = new DataGridViewRow();
                  row1_5.CreateCells(DataGriewDados, "-------------------------------------------------------", TotalQuantidade.ToString() , "---------", "Sub-Total", SubTotal.ToString("n2"));
                  row1_5.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                  DataGriewDados.Rows.Add(row1_5);
              }
              
            }

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "-------------------------------------------------------", "---------", "---------", "Total geral:", TotalGeral.ToString("n2"));
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }
               

        private LIS_PRODUTONFECollection BuscaProdutoCFOP(int IDCFOP)
        {
            LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
            try
            {               
                LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();
                RowRelatorio.Clear();
                string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.Int32", "=", IDCFOP.ToString()));     

                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));
            
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));                
               

                LIS_PRODUTONFECollection LIS_PRODUTONFEColl_2 = new LIS_PRODUTONFECollection();
                LIS_PRODUTONFEColl_2 = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);
                return LIS_PRODUTONFEColl_2;                
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "+ ex.Message);
                return LIS_PRODUTONFEColl;
                
            }
           
        }
  
       
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Vendas de Produtos por Cidade - NFe");

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

                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));                   

                    RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                    if(Convert.ToInt32(cbCFOP.SelectedValue) > 0)
                    {
                        RowRelatorio.Add(new RowsFiltro("IDCFOP", "System.String", "=", cbCFOP.SelectedValue.ToString()));
                    }
                    
                    LIS_PRODUTONFEColl_1 = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio);

                    PreencheGrid2();
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

        private void txtCidade1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void txtCidade1_Enter(object sender, EventArgs e)
        {

          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Vendas por CFOP");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Vendas por CFOP";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }
      
    }
}
