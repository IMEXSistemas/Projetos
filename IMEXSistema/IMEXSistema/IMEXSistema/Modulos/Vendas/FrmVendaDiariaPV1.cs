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
using System.Diagnostics;
using System.IO;
using BmsSoftware.Modulos.Relatorio;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmVendaDiariaPV1 : Form
    {
        Utility Util = new Utility();
        LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();
        LIS_PEDIDO2Collection LIS_PEDIDO2CFormPagtoColl = new LIS_PEDIDO2Collection();

        LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVendaDiariaPV1()
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmVendaDiariaPV1_Load(object sender, EventArgs e)
        {
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            mkDtInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkdatafinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text) + "00:00"));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text) + "23:59"));

                if (rbOrcamentoPesquisa.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "S"));
                else if (rbVendasPesquisa.Checked)
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));


                LIS_PEDIDOColl.Clear();

                LIS_PEDIDOColl = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO DESC");

                //Colocando somatorio no final da lista
                LIS_PEDIDOEntity LIS_PEDIDOTy = new LIS_PEDIDOEntity();
                LIS_PEDIDOTy.TOTALPEDIDO = SumTotalPesquisa("TOTALPEDIDO");
                LIS_PEDIDOTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");

                LIS_PEDIDOColl.Add(LIS_PEDIDOTy);

                lblTotalRegistros.Text = "Total de Registros: " + (LIS_PEDIDOColl.Count - 1).ToString();

                DataGridRelaPedido.AutoGenerateColumns = false;
                DataGridRelaPedido.DataSource = LIS_PEDIDOColl;

                SomaValorPago();

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
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));


                LIS_PEDIDO2Provider LIS_PEDIDO2P = new LIS_PEDIDO2Provider();
                LIS_PEDIDO2CFormPagtoColl.Clear();
                LIS_PEDIDO2CFormPagtoColl = LIS_PEDIDO2P.ReadCollectionByParameter(RowRelatorio, "DTEMISSAO DESC");
                
                LIS_PEDIDO2Entity LIS_PEDIDO2Ty = new LIS_PEDIDO2Entity();
                LIS_PEDIDO2Ty.TOTALPEDIDO = SumTotalPesquisaFPagto("TOTALPEDIDO");
                LIS_PEDIDO2Ty.VALORPAGO = SumTotalPesquisaFPagto("VALORPAGO");

                LIS_PEDIDO2CFormPagtoColl.Add(LIS_PEDIDO2Ty);
               

                dtgFormPagto.AutoGenerateColumns = false;
                dtgFormPagto.DataSource = LIS_PEDIDO2CFormPagtoColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal SumTotalPesquisaFPagto(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDO2Entity item in LIS_PEDIDO2CFormPagtoColl)
            {
                if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);

            }

            return valortotal;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                 if (NomeCampo == "TOTALPEDIDO")
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                 else if (NomeCampo == "VALORPAGO")
                     valortotal += Convert.ToDecimal(item.VALORPAGO);
            }
          

            return valortotal;
        }

        private void SomaValorPago()
        {
            decimal valortotalpago = 0;
            decimal valortotal = 0;
            foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl)
            {
                if (item.IDPEDIDO > 0)
                {
                    valortotalpago += Convert.ToDecimal(item.VALORPAGO);
                    valortotal += Convert.ToDecimal(item.TOTALPEDIDO);
                }
            }

            txtTotal.Text = valortotal.ToString("n2");
            txtTotalPago.Text = valortotalpago.ToString("n2");
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
                
                using (FrmRelatVendaDiaria frm = new FrmRelatVendaDiaria())
                {
                    //Retira o ultimo registro
                    LIS_PEDIDOColl.RemoveAt(LIS_PEDIDOColl.Count - 1);
                    LIS_PEDIDO2CFormPagtoColl.RemoveAt(LIS_PEDIDO2CFormPagtoColl.Count - 1);

                    frm.LIS_PEDIDOColl = LIS_PEDIDOColl;
                    frm.LIS_PEDIDO2Coll = LIS_PEDIDO2CFormPagtoColl;
                    frm.ShowDialog();

                    btnConsultar_Click(null, null);
                }
                
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}


