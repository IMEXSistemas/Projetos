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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmVendaDiariaOS1 : Form
    {
        Utility Util = new Utility();

        LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();
        LIS_FECHOSERVICO2Collection LIS_FECHOSERVICO2COLL = new LIS_FECHOSERVICO2Collection();

        LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();
        LIS_FECHOSERVICO2Provider LIS_FECHOSERVICO2P = new LIS_FECHOSERVICO2Provider();


        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaDiariaOS1()
        {
            InitializeComponent();
        }

        public MonthCalendar monthCalendar1 = new MonthCalendar();
        Form FormCalendario1 = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar1_DateSelected);

            FormCalendario1.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario1.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario1.ClientSize = new System.Drawing.Size(190, 170);
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
            FormCalendario2.ClientSize = new System.Drawing.Size(190, 170);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmVendaDiariaPV1_Load(object sender, EventArgs e)
        {
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            mkDtInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkdatafinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                if (rdOrcamento.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=","S"));
                else if (rdVenda.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                
                LIS_ORDEMSERVICOSFECHColl.Clear();

                LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO DESC");

                //Colocando somatorio no final da lista
                LIS_ORDEMSERVICOSFECHEntity LIS_ORDEMSERVICOSFECHTy = new LIS_ORDEMSERVICOSFECHEntity();
                LIS_ORDEMSERVICOSFECHTy.TOTALFECHOS = SumTotalPesquisa("TOTALFECHOS");

                LIS_ORDEMSERVICOSFECHColl.Add(LIS_ORDEMSERVICOSFECHTy);

                lblTotalRegistros.Text = "Total de Registros: " + (LIS_ORDEMSERVICOSFECHColl.Count - 1).ToString();

                DataGridRelaPedido.AutoGenerateColumns = false;
                DataGridRelaPedido.DataSource = LIS_ORDEMSERVICOSFECHColl;

                AddGridFormaPagto();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AddGridFormaPagto()
        {
            try
            {
                //Remove ID  repetido  
                LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECH2Coll = new LIS_ORDEMSERVICOSFECHCollection();
                foreach (LIS_ORDEMSERVICOSFECHEntity item in LIS_ORDEMSERVICOSFECHColl)
                {
                    if (LIS_ORDEMSERVICOSFECH2Coll.Find(delegate(LIS_ORDEMSERVICOSFECHEntity item2)
                    {
                        return
                            (item2.IDFORMAPAGAMENTO == item.IDFORMAPAGAMENTO);
                    }) == null)
                    {
                        LIS_ORDEMSERVICOSFECH2Coll.Add(item);
                    }
                }

                decimal ValorTotal = 0;
                decimal TotalGeral = 0;

                dtgFormPagto.Rows.Clear();

                foreach (var LIS_ORDEMSERVICOSFECH2Ty in LIS_ORDEMSERVICOSFECH2Coll)
                {
                    ValorTotal = 0;
                    ValorTotal = QuantFormaPagto(Convert.ToInt32(LIS_ORDEMSERVICOSFECH2Ty.IDFORMAPAGAMENTO));
                    TotalGeral += ValorTotal;
                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(dtgFormPagto, LIS_ORDEMSERVICOSFECH2Ty.NOMEFORMAPAGTO, ValorTotal.ToString("n2"));
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    dtgFormPagto.Rows.Add(row2);
                }

                //Total Geral
                DataGridViewRow row3 = new DataGridViewRow();
                row3.CreateCells(dtgFormPagto, "Total: ", TotalGeral.ToString("n2"));
                row3.DefaultCellStyle.Font = new Font("Arial", 8);
                dtgFormPagto.Rows.Add(row3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal QuantFormaPagto(int IDFORMAPAGAMENTO)
        {
            decimal result = 0;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", IDFORMAPAGAMENTO.ToString()));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

                if (rdOrcamento.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                else if (rdVenda.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECH3Coll = new LIS_ORDEMSERVICOSFECHCollection();

                LIS_ORDEMSERVICOSFECH3Coll = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio);

                foreach (var LIS_ORDEMSERVICOSFECH3Ty in LIS_ORDEMSERVICOSFECH3Coll)
                {
                    result += Convert.ToDecimal(LIS_ORDEMSERVICOSFECH3Ty.TOTALFECHOS);
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }           

        }

        private decimal SumTotalPesquisaFPagto(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_FECHOSERVICO2Entity item in LIS_FECHOSERVICO2COLL)
            {
                if (NomeCampo == "TOTALFECHOS")
                    valortotal += Convert.ToDecimal(item.TOTALFECHOS);

            }

            return valortotal;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_ORDEMSERVICOSFECHEntity item in LIS_ORDEMSERVICOSFECHColl)
            {
                if (NomeCampo == "TOTALFECHOS")
                    valortotal += Convert.ToDecimal(item.TOTALFECHOS);
               
            }

            txtTotal.Text = valortotal.ToString("n2");

            return valortotal;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_ORDEMSERVICOSFECHColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            }
            else
            {
                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGridRelaPedido, "Relação de Venda Diária. Período: " + mkDtInicial.Text + " Á " + mkdatafinal.Text, this.Name);

                PRt.Print_DataGridView(dtgFormPagto, "Resumo por Formas de Pagamento. Período: " + mkDtInicial.Text + " Á " + mkdatafinal.Text, this.Name);
            }
        }
    }
}
