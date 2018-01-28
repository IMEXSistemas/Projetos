using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using VVX;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Relatorio;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmFluxoContas : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        public FrmFluxoContas()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkDtInicial.Text))
            {
                errorProvider1.SetError(mkDtInicial, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdatafinal.Text))
            {
                errorProvider1.SetError(mkdatafinal, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {
                lblValorTotalReceber.Text = "0";
                lblValorTotalPagar.Text = "0";
                PesquisaDuplicatasReceber();
                PesquisaDuplicatasPagar();

                lblValorSaldo.Text = (Convert.ToDecimal(lblValorTotalReceber.Text) - Convert.ToDecimal(lblValorTotalPagar.Text)).ToString("n2");
            }
        }

        private void PesquisaDuplicatasReceber()
        {

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

            if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));


            if (rbVencimento.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                if (rbPaga.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                else if (rbVencidasVencer.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
            }
            else if (rbEmissao.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));              

                if (rbPaga.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                else if (rbVencidasVencer.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO");
            }
            else if (rdPagamento.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAPAGTO");
            }           

        
            //Colocando somatorio no final da lista
            LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
            LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
            LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
       
            LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);


            DataGridRelaDupl.AutoGenerateColumns = false;
            DataGridRelaDupl.DataSource = LIS_DUPLICATARECEBERColl;

            lblTotalreceber.Text = "Total de registros: " +(LIS_DUPLICATARECEBERColl.Count - 1).ToString();
        }

        private void PesquisaDuplicatasPagar()
        {
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

            if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));

            if (rbVencimento.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                if (rbPaga.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                else if (rbVencidasVencer.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
            }
            else if (rbEmissao.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                if (rbPaga.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago  
                else if (rbVencidasVencer.Checked)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO");
            }
            else if (rdPagamento.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAPAGTO");
            }

            //Colocando somatorio no final da lista
            LIS_DUPLICATAPAGAREntity LIS_DUPLICATAPAGARTy = new LIS_DUPLICATAPAGAREntity();
            LIS_DUPLICATAPAGARTy.VALORDUPLICATA = SumTotalPesquisa2("VALORDUPLICATA");
            LIS_DUPLICATAPAGARTy.VALORPAGO = SumTotalPesquisa2("VALORPAGO");
            LIS_DUPLICATAPAGARColl.Add(LIS_DUPLICATAPAGARTy);

            dgrDuplcPagar.AutoGenerateColumns = false;
            dgrDuplcPagar.DataSource = LIS_DUPLICATAPAGARColl;

            lblTotalPagar.Text = "Total de registros: " + (LIS_DUPLICATAPAGARColl.Count - 1).ToString();
        }

        private decimal SumTotalPesquisa2(string NomeCampo)
        {
            decimal valortotal = 0;
            decimal valortotal2 = 0;
 
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                {
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);

                    if (rbPaga.Checked)
                        valortotal2 += Convert.ToDecimal(item.VALORPAGO);
                    else
                        valortotal2 += Convert.ToDecimal(item.VALORDUPLICATA);

                    lblValorTotalPagar.Text = valortotal2.ToString("n2");
                }
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);                
            }
           
           

            return valortotal;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            decimal valortotal2 = 0;
            
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                {
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);

                    if (rbPaga.Checked)
                        valortotal2 += Convert.ToDecimal(item.VALORPAGO);
                    else
                        valortotal2 += Convert.ToDecimal(item.VALORDUPLICATA);

                    lblValorTotalReceber.Text = valortotal2.ToString("n2");
                }
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);  
            }

            
            return valortotal;
        }

        public MonthCalendar monthCalendar1 = new MonthCalendar();
        Form FormCalendario1 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar1_DateSelected);

            FormCalendario1.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario1.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario1.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario1.Location = new Point(230, 55);
            FormCalendario1.Name = "FrmCalendario";
            FormCalendario1.Text = "Calendário";
            FormCalendario1.ResumeLayout(false);
            FormCalendario1.Controls.Add(monthCalendar1);
            FormCalendario1.ShowDialog();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkDtInicial.Text = monthCalendar1.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario1.Close();
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario2 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar2";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario2.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario2.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario2.ClientSize = new System.Drawing.Size(190, 160);
            FormCalendario2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario2.Location = new Point(230, 55);
            FormCalendario2.Name = "FrmCalendario2";
            FormCalendario2.Text = "Calendário 2";
            FormCalendario2.ResumeLayout(false);
            FormCalendario2.Controls.Add(monthCalendar2);
            FormCalendario2.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkdatafinal.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario2.Close();
        }

        private void FrmFluxoContas_Load(object sender, EventArgs e)
        {
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropCentroCusto();
            VerificaAcesso();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void GetDropCentroCusto()
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

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (FrmRelatFluxoCaixa frm = new FrmRelatFluxoCaixa())
            {
                frm.LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARColl;
                frm.LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERColl;

                frm.TotalReceber = lblValorTotalReceber.Text;
                frm.TotalPagar = lblValorTotalPagar.Text;
                frm.Saldo = lblValorSaldo.Text;

                frm.ShowDialog();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();
            config.MargemDireita = 760;

            e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

            //Logomarca
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
            if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
            {
                if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                {
                    ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                    ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                    MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                    e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                }
            }

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
            config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
            e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
            e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
            e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
            e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
            e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

            e.Graphics.DrawString("FLUXO DE CONTAS A RECEBER/PAGAR - RESUMO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 140);

            e.Graphics.DrawString("Total de Contas a Receber: " , config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 165);
            e.Graphics.DrawString(lblValorTotalReceber.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 165);
            e.Graphics.DrawString("Total de Contas a Pagar: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 185);
            e.Graphics.DrawString(lblValorTotalPagar.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 185);
            e.Graphics.DrawString("Saldo: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 205);
            e.Graphics.DrawString(lblValorSaldo.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 200, 205);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
       
    }
}
