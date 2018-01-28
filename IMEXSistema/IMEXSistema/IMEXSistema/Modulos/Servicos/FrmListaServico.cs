using BmsSoftware.Modulos.FrmSearch;
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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmListaServico : Form
    {
        LIS_ORDEMSERVICOSFECHProvider LIS_ORDEMSERVICOSFECHP = new LIS_ORDEMSERVICOSFECHProvider();
        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
        SERVICOProvider SERVICOP = new SERVICOProvider();

        public LIS_ORDEMSERVICOSFECHCollection LIS_ORDEMSERVICOSFECHColl = new LIS_ORDEMSERVICOSFECHCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        SERVICOCollection SERVICOColl = new SERVICOCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

        Utility Util = new Utility();

        public FrmListaServico()
        {
            InitializeComponent();
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);
            monthCalendar2.ShowWeekNumbers = true;

           // FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(250, 160);
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
            maskedtxtData.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario2 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar3";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);
            monthCalendar3.ShowWeekNumbers = true;
         //   FormCalendario2.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario2.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario2.ClientSize = new System.Drawing.Size(250, 160);
            FormCalendario2.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario2.Location = new Point(230, 55);
            FormCalendario2.Name = "FrmCalendario";
            FormCalendario2.Text = "Calendário";
            FormCalendario2.ResumeLayout(false);
            FormCalendario2.Controls.Add(monthCalendar3);
            FormCalendario2.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            mdkDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario2.Close();
        }

        private void FrmHorasEquipamento_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;          
            
            GetFuncionario();
            GetDropCliente();

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
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

        private void GetFuncionario()
        {
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario2.DisplayMember = "NOME";
            cbFuncionario2.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario2.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncionario2.DataSource = FUNCIONARIOColl;

            cbFuncionario2.SelectedIndex = 0;
        }
       

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Serviços");

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

        private void cbEquipamento_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
                ListaServico(); 
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();

            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mdkDataFinal.Text))
            {
                errorProvider1.SetError(mdkDataFinal, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }
        private void ListaServico()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(maskedtxtData.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mdkDataFinal.Text)));

                if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario2.SelectedValue.ToString()));

                if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));

                LIS_ORDEMSERVICOSFECHColl = LIS_ORDEMSERVICOSFECHP.ReadCollectionByParameter(RowRelatorio);

                PreencheGrid();
            }
            catch (Exception EX)
            {

                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        decimal totalGeralOS = 0;
        decimal totalQuant = 0;
        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            
            DataGriewDados.Rows.Clear();
            
            totalGeralOS = 0;
            totalQuant = 0;
        
            DataGridViewRow rowTop = new DataGridViewRow();
          //  rowTop.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "_________", ____________________________", "__________"); 
            rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowTop);

            DataGridViewRow row1 = new DataGridViewRow();
            row1.CreateCells(DataGriewDados, "O.SERVIÇO", "EMISSÃO", "SERVIÇO",  "QUANT.","CLIENTE",  "TOTAL O.S", "FUNCIONÁRIO");
            row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(row1);

            foreach (var LIS_ORDEMSERVICOSFECHTy in LIS_ORDEMSERVICOSFECHColl)
            {
                LIS_SERVICOOSFECHCollection LIS_SERVICOOSFECHColl = new LIS_SERVICOOSFECHCollection();
                LIS_SERVICOOSFECHProvider LIS_SERVICOOSFECHP = new LIS_SERVICOOSFECHProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", LIS_ORDEMSERVICOSFECHTy.IDORDEMSERVICO.ToString()));
                LIS_SERVICOOSFECHColl = LIS_SERVICOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                foreach (var LIS_SERVICOOSFECHTy in LIS_SERVICOOSFECHColl)
                {
                    string DataEmissao = string.Empty;
                    if (LIS_ORDEMSERVICOSFECHTy.IDORDEMSERVICO != null)
                        DataEmissao = Convert.ToDateTime(LIS_ORDEMSERVICOSFECHTy.DATAEMISSAO).ToString("dd/MM/yyyy");

                    string TotalOS = Convert.ToDecimal(LIS_SERVICOOSFECHTy.VALORTOTAL).ToString("n2");           

                    DataGridViewRow row2 = new DataGridViewRow();
                    string OSNumero = LIS_ORDEMSERVICOSFECHTy.IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    row2.CreateCells(DataGriewDados, OSNumero, DataEmissao, LIS_SERVICOOSFECHTy.NOMESERVICO, LIS_SERVICOOSFECHTy.QUANTIDADE, LIS_ORDEMSERVICOSFECHTy.NOMECLIENTE,  TotalOS, LIS_ORDEMSERVICOSFECHTy.NOMEFUNCIONARIO);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    totalQuant += Convert.ToInt32(LIS_SERVICOOSFECHTy.QUANTIDADE);
                 }

                totalGeralOS += Convert.ToDecimal(LIS_ORDEMSERVICOSFECHTy.TOTALFECHOS);
                
            }

                DataGridViewRow rowLinha = new DataGridViewRow();
                //  rowLinha.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "_________",  "_________" "____________________________", "__________"); 
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                DataGridViewRow rowRodape = new DataGridViewRow();
                rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                rowRodape.CreateCells(DataGriewDados, string.Empty, string.Empty, "TOTAL GERAL                    ", totalQuant, string.Empty, totalGeralOS.ToString("n2"), string.Empty );
                rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowRodape);

                this.Cursor = Cursors.Default;
            }

    }
}
