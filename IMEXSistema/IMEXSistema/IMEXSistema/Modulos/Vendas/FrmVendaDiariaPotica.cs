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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmVendaDiariaPotica : Form
    {
        Utility Util = new Utility();

        LIS_PEDIDOOTICACollection LIS_PEDIDOColl = new LIS_PEDIDOOTICACollection();
        LIS_PEDIDOOTICA2Collection LIS_PEDIDO2CFormPagtoColl = new LIS_PEDIDOOTICA2Collection();

        LIS_PEDIDOOTICAProvider LIS_PEDIDOP = new LIS_PEDIDOOTICAProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaDiariaPotica()
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
            FormCalendario1.ClientSize = new System.Drawing.Size(157, 156);
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
            FormCalendario2.ClientSize = new System.Drawing.Size(157, 156);
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

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            mkDtInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkdatafinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));

            LIS_PEDIDOColl.Clear();

            LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "IDPEDIDOOTICA, DTEMISSAO DESC");

            //Colocando somatorio no final da lista
            LIS_PEDIDOOTICAEntity LIS_PEDIDOTy = new LIS_PEDIDOOTICAEntity();
            LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");

            LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

            lblTotalRegistros.Text = "Total de Registros: " + (LIS_PEDIDOColl.Count - 1).ToString();                     

            DataGridRelaPedido.AutoGenerateColumns = false;
            DataGridRelaPedido.DataSource = LIS_PEDIDOColl;

            AddGridFormaPagto();
        }

        private void AddGridFormaPagto()
        {
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));


            LIS_PEDIDOOTICA2Provider LIS_PEDIDO2P = new LIS_PEDIDOOTICA2Provider();
            LIS_PEDIDO2CFormPagtoColl.Clear();
            LIS_PEDIDO2CFormPagtoColl = LIS_PEDIDO2P.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO DESC");

            LIS_PEDIDOOTICA2Entity LIS_PEDIDO2Ty = new LIS_PEDIDOOTICA2Entity();
            LIS_PEDIDO2Ty.TOTALPEDIDO = SumTotalPesquisaFPagto("TOTALPEDIDO");

            LIS_PEDIDO2CFormPagtoColl.Add(LIS_PEDIDO2Ty);

            dtgFormPagto.AutoGenerateColumns = false;
            dtgFormPagto.DataSource = LIS_PEDIDO2CFormPagtoColl;   
        }

        private decimal SumTotalPesquisaFPagto(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOOTICA2Entity item in LIS_PEDIDO2CFormPagtoColl)
            {
                if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);

            }

            return valortotal;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOOTICAEntity item in LIS_PEDIDOColl)
            {
                 if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
               
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
            if (LIS_PEDIDOColl.Count == 0)
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
