using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using VVX;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmExtratoContaBancaria : Form
    {
        Utility Util = new Utility();
        CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
        LIS_MOVCONTACORRENTEProvider LIS_MOVCONTACORRENTEP = new LIS_MOVCONTACORRENTEProvider();

        LIS_MOVCONTACORRENTECollection LIS_MOVCONTACORRENTEColl = new LIS_MOVCONTACORRENTECollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmExtratoContaBancaria()
        {
            InitializeComponent();
        }

        private void FrmExtratoContaBancaria_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            bntDateSelecInicial.Image = Util.GetAddressImage(22);
            bntDateSelecFinal.Image = Util.GetAddressImage(22);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropContaCorrente();
        }

        private void GetDropContaCorrente()
        {
            CONTACORRENTEProvider CONTACORRENTEP = new CONTACORRENTEProvider();
            CONTACORRENTECollection CONTACORRENTEColl = new CONTACORRENTECollection();
            CONTACORRENTEColl = CONTACORRENTEP.ReadCollectionByParameter(null, "NOMECONTA");

            cbContaCorrente.DisplayMember = "NOMECONTA";
            cbContaCorrente.ValueMember = "IDCONTACORRENTE";

            CONTACORRENTEEntity CONTACORRENTETy = new CONTACORRENTEEntity();
            CONTACORRENTETy.NOMECONTA = ConfigMessage.Default.MsgDrop;
            CONTACORRENTETy.IDCONTACORRENTE = -1;
            CONTACORRENTEColl.Add(CONTACORRENTETy);

            Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CONTACORRENTEEntity>(cbContaCorrente.DisplayMember);

            CONTACORRENTEColl.Sort(comparer.Comparer);
            cbContaCorrente.DataSource = CONTACORRENTEColl;

            cbContaCorrente.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbContaCorrente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbContaCorrente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkDtInicial.Text))
            {
                errorProvider1.SetError(mkDtInicial, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
             else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdatafinal.Text))
            {
                errorProvider1.SetError(mkdatafinal, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                Consulta();

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, "Extrato de Conta Bancária: " + cbContaCorrente.Text, this.Name);
            }

        }

        private void Consulta()
        {
            string DataInicial = Util.ConverStringDateSearch(mkDtInicial.Text);//formata data para pesquisa.
            try
            {
                string DataFinal = Util.ConverStringDateSearch(mkdatafinal.Text);//formata data para pesquisa.          

                RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", ">=", DataInicial));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "<=", DataFinal));
                RowRelatorio.Add(new RowsFiltro("IDCONTACORRENTE", "System.Int32", "=", (cbContaCorrente.SelectedValue).ToString()));

                LIS_MOVCONTACORRENTEColl = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO desc");

                //Colocando somatorio no final da lista
                LIS_MOVCONTACORRENTEEntity LIS_MOVCONTACORRENTRTy = new LIS_MOVCONTACORRENTEEntity();
                LIS_MOVCONTACORRENTRTy.VALOR = SaldoExtrato();
                LIS_MOVCONTACORRENTRTy.NUMMOVIMENTACAO = "Saldo Atual:";
                LIS_MOVCONTACORRENTEColl.Add(LIS_MOVCONTACORRENTRTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MOVCONTACORRENTEColl;
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_MOVCONTACORRENTEEntity item in LIS_MOVCONTACORRENTEColl)
            {
                if (NomeCampo == "VALOR")
                    valortotal += Convert.ToDecimal(item.VALOR);
              
            }

            return valortotal;
        }

        private decimal SaldoExtrato()
        {
            decimal result = 0;

            try
            {
                string DataInicial = Util.ConverStringDateSearch(mkDtInicial.Text);//formata data para pesquisa.
                string DataFinal = Util.ConverStringDateSearch(mkdatafinal.Text);//formata data para pesquisa.  

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCONTACORRENTE", "System.Int32", "=", (cbContaCorrente.SelectedValue).ToString()));

                if (!chkSaldoGeral.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATAMOVIMENTACAO", "System.DateTime", "<=", DataFinal));
                }


                LIS_MOVCONTACORRENTECollection LIS_MOVCONTACORRENTE2Coll = new LIS_MOVCONTACORRENTECollection();
                LIS_MOVCONTACORRENTE2Coll = LIS_MOVCONTACORRENTEP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO desc");

                foreach (LIS_MOVCONTACORRENTEEntity item in LIS_MOVCONTACORRENTE2Coll)
                {
                    if (item.IDTIPOMOVCAIXA == 2)
                        result -= Convert.ToDecimal(item.VALOR * -1);
                    else
                        result += Convert.ToDecimal(item.VALOR);


                }

                 return result;
            }
            catch (Exception ex)
            {
               return result;
               MessageBox.Show("Erro técnico: " + ex.Message);
            }

           
        }
       
    }
}
