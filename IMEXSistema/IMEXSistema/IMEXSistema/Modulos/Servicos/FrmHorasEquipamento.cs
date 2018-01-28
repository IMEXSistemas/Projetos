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
    public partial class FrmHorasEquipamento : Form
    {
        LIS_EQUIPAMENTOOSFECHProvider LIS_EQUIPAMENTOOSFECHP = new LIS_EQUIPAMENTOOSFECHProvider();
        FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
        ORDEMSERVICOSFECHProvider ORDEMSERVICOSFECHP = new ORDEMSERVICOSFECHProvider();

        EQUIPAMENTOCollection EQUIPAMENTOColl = new EQUIPAMENTOCollection();
        LIS_EQUIPAMENTOOSFECHCollection LIS_EQUIPAMENTOOSFECHColl = new LIS_EQUIPAMENTOOSFECHCollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

        Utility Util = new Utility();

        public FrmHorasEquipamento()
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

            GetDropEquipamento();
            GetFuncionario();
            GetDropCliente();

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
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

        private void GetDropEquipamento()
        {
            EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
            EQUIPAMENTOColl = EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbEquipamento.DisplayMember = "NOME";
            cbEquipamento.ValueMember = "IDEQUIPAMENTO";

            EQUIPAMENTOEntity EQUIPAMENTOTy = new EQUIPAMENTOEntity();
            EQUIPAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            EQUIPAMENTOTy.IDEQUIPAMENTO = -1;
            EQUIPAMENTOColl.Add(EQUIPAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity>(cbEquipamento.DisplayMember);

            EQUIPAMENTOColl.Sort(comparer.Comparer);
            cbEquipamento.DataSource = EQUIPAMENTOColl;

            cbEquipamento.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Horas de Equipamentos");

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
            if ((Control.ModifierKeys == Keys.Control) &&
        (e.KeyCode == Keys.E))
            {
                using (FrmSearchEquipamento frm = new FrmSearchEquipamento())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbEquipamento.SelectedValue = result;
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
                ListaEquipamentoServico(); 
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
        private void ListaEquipamentoServico()
        {
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(maskedtxtData.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mdkDataFinal.Text)));

                if (Convert.ToInt32(cbEquipamento.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", cbEquipamento.SelectedValue.ToString()));


                if (Convert.ToInt32(cbFuncionario2.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario2.SelectedValue.ToString()));

                if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));

                LIS_EQUIPAMENTOOSFECHColl = LIS_EQUIPAMENTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                PreencheGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeralHora = 0;
        decimal totalGeralOS = 0;
        decimal totalEquipamentoOS = 0;
        decimal totalGeralEquipamentoOS = 0;
        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                DataGriewDados.Rows.Clear();
                TotalGeralHora = 0;
                totalGeralOS = 0;
                totalEquipamentoOS = 0;
                totalGeralEquipamentoOS = 0;

                foreach (var LIS_EQUIPAMENTOOSFECHTy in LIS_EQUIPAMENTOOSFECHColl)
                {
                    string DataEmissao = string.Empty;
                    if (LIS_EQUIPAMENTOOSFECHTy.IDORDEMSERVICO != null)
                        DataEmissao = Convert.ToDateTime(LIS_EQUIPAMENTOOSFECHTy.DATAEMISSAO).ToString("dd/MM/yyyy");

                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(LIS_EQUIPAMENTOOSFECHTy.IDCLIENTE));

                    string NomeCliente = CLIENTETy.NOME;
                    decimal TotalOS = Convert.ToDecimal(ORDEMSERVICOSFECHP.Read(Convert.ToInt32(LIS_EQUIPAMENTOOSFECHTy.IDORDEMSERVICO)).TOTALFECHOS);

                    totalEquipamentoOS = TotalEquipamentoServico(Convert.ToInt32(LIS_EQUIPAMENTOOSFECHTy.IDORDEMSERVICO), Convert.ToInt32(LIS_EQUIPAMENTOOSFECHTy.IDEQUIPAMENTO));
                    totalGeralEquipamentoOS += totalEquipamentoOS;

                    DataGridViewRow row2 = new DataGridViewRow();
                    string OSNumero = LIS_EQUIPAMENTOOSFECHTy.IDORDEMSERVICO.ToString().PadLeft(6, '0');
                    row2.CreateCells(DataGriewDados, OSNumero, DataEmissao, NomeCliente, LIS_EQUIPAMENTOOSFECHTy.NOMEEQUIPAMENTO, LIS_EQUIPAMENTOOSFECHTy.QUANTLOCACAO, LIS_EQUIPAMENTOOSFECHTy.NOMEFUNCIONARIO, totalEquipamentoOS, TotalOS);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeralHora += Convert.ToDecimal(LIS_EQUIPAMENTOOSFECHTy.QUANTLOCACAO);
                    totalGeralOS += TotalOS;

                }

                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "_________", "__________", "________________________", "____________________________________________", "_________", "____________________________", "__________", "__________");
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                DataGridViewRow rowRodape = new DataGridViewRow();
                rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                rowRodape.CreateCells(DataGriewDados, string.Empty, string.Empty, string.Empty, "TOTAL GERAL                    ", TotalGeralHora, string.Empty, totalGeralEquipamentoOS.ToString("n2"), totalGeralOS.ToString("n2"));
                rowRodape.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowRodape);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                
            }
            }
        

        private decimal TotalEquipamentoServico(int IDORDEMSERVICO, int IDEQUIPAMENTO)
        {
            decimal Result = 0;

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDORDEMSERVICO", "System.Int32", "=", IDORDEMSERVICO.ToString()));

            if (IDEQUIPAMENTO != -1)
                RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", IDEQUIPAMENTO.ToString()));
            
            LIS_EQUIPAMENTOOSFECHColl = LIS_EQUIPAMENTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

            foreach (LIS_EQUIPAMENTOOSFECHEntity item in LIS_EQUIPAMENTOOSFECHColl)
            {
                Result += Convert.ToDecimal(item.VALORTOTAL);
            }

            return Result;
        }
    }
}
