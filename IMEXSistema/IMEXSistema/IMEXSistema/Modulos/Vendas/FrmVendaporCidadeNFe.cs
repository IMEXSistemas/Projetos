﻿using System;
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
    public partial class FrmVendaporCidadeNFe : Form
    {
        LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();

        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();

        LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
        LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();

         Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        int _COD_MUN_IBGE = -1;

        public FrmVendaporCidadeNFe()
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

            GetFuncionario();
            GetDropStatus();
            GetTransporte();

            msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void GetTransporte()
        {
            try
            {
                TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
                TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

                cbTransportador.DisplayMember = "NOME";
                cbTransportador.ValueMember = "IDTRANSPORTADORA";

                TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
                TRANSPORTADORATy.NOME = ConfigMessage.Default.MsgDrop;
                TRANSPORTADORATy.IDTRANSPORTADORA = -1;
                TRANSPORTADORAColl.Add(TRANSPORTADORATy);

                Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(cbTransportador.DisplayMember);

                TRANSPORTADORAColl.Sort(comparer.Comparer);
                cbTransportador.DataSource = TRANSPORTADORAColl;

                cbTransportador.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void GetFuncionario()
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

        decimal  TotalGeral = 0;
        private void PreencheGrid2()
        {
            TotalGeral = 0;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();

            //Remove as Cidades Repetidas
            LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl_2 = new LIS_NOTAFISCALECollection();
            foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
            {

                if (LIS_NOTAFISCALEColl_2.Find(delegate(LIS_NOTAFISCALEEntity item2) { return (item2.COD_MUN_IBGE == item.COD_MUN_IBGE); }) == null)
                {
                    LIS_NOTAFISCALEColl_2.Add(item);
                }
            }

            //Cabeçalho Nome Cidade
            DataGridViewRow row1_2 = new DataGridViewRow();
            row1_2.CreateCells(DataGriewDados, "CIDADE/UF","", "", "", "");
            row1_2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(row1_2);

            foreach (var LIS_NOTAFISCALETy in LIS_NOTAFISCALEColl_2)
            {
                LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl3 = new LIS_NOTAFISCALECollection();
                LIS_NOTAFISCALEColl3 = BuscaNFePelaCidade(Convert.ToInt32(LIS_NOTAFISCALETy.COD_MUN_IBGE));

              LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
              LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
              RowRelatorio.Clear();
              RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", LIS_NOTAFISCALETy.COD_MUN_IBGE.ToString()));
              LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

              DataGridViewRow row1_3 = new DataGridViewRow();
              row1_3.CreateCells(DataGriewDados, LIS_MUNICIPIOSColl[0].MUNIUF, "", "", "", "");
              row1_3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
              DataGriewDados.Rows.Add(row1_3);

              DataGridViewRow row1_4 = new DataGridViewRow();
              row1_4.CreateCells(DataGriewDados, "CLIENTE", "TELEFONE", "DATA", "Nº PEDIDO", "TOTAL");
              row1_4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
              DataGriewDados.Rows.Add(row1_4);



              foreach (var LIS_NOTAFISCALETy_3 in LIS_NOTAFISCALEColl3)
                {
                    CLIENTEEntity CLIENTEtY = new CLIENTEEntity();
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    CLIENTEtY = CLIENTEP.Read(Convert.ToInt32(LIS_NOTAFISCALETy_3.IDCLIENTE));

                    string DataEmissao = Convert.ToDateTime(LIS_NOTAFISCALETy_3.DTEMISSAO).ToString("dd/MM/yyyy");
                    string NomeCliente = LIS_NOTAFISCALETy_3.NOMECLIENTE;
                    string NumPedido = LIS_NOTAFISCALETy_3.NFISCALE.ToString().PadLeft(6, '0'); ;
                    string TotalPedido = Convert.ToDecimal(LIS_NOTAFISCALETy_3.TOTALNOTA).ToString("n2");
                    string TELEFONE1 = CLIENTEtY.TELEFONE1;

                    DataGridViewRow row2 = new DataGridViewRow();
                    row2.CreateCells(DataGriewDados, NomeCliente, TELEFONE1, DataEmissao, NumPedido, TotalPedido);
                    row2.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGriewDados.Rows.Add(row2);

                    TotalGeral += Convert.ToDecimal(LIS_NOTAFISCALETy_3.TOTALNOTA);

                }

              DataGridViewRow row1_5 = new DataGridViewRow();
              row1_5.CreateCells(DataGriewDados, "-------------------------------------------------------", "---------", "---------", "---------", "---------");
              row1_5.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
              DataGriewDados.Rows.Add(row1_5);
              
            }

            DataGridViewRow rowLinha = new DataGridViewRow();
            rowLinha.CreateCells(DataGriewDados, "-------------------------------------------------------", "---------", "---------", "Total geral:", TotalGeral);
            rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowLinha);

            this.Cursor = Cursors.Default;
        }

        private LIS_NOTAFISCALECollection BuscaNFePelaCidade(int COD_MUN_IBGE)
        {
            LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl4 = new LIS_NOTAFISCALECollection();
            RowRelatorio.Clear();
            string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
            string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

            RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", COD_MUN_IBGE.ToString()));

            if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

            if (Convert.ToInt32(cbTransportador.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDTRANSPORTES", "System.Int32", "=", Convert.ToInt32(cbTransportador.SelectedValue).ToString()));


            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
            RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

            if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

            if (txtCidade1.Text.Trim() != string.Empty)
                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", _COD_MUN_IBGE.ToString()));

            if (chkNFeEnviada.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
            }
            
            if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                LIS_NOTAFISCALEColl4 = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "IDTRANSPORTES, IDPEDIDO");
            else
                LIS_NOTAFISCALEColl4 = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

            return LIS_NOTAFISCALEColl4;
        }     
       
      
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Vendas por Cidade - NFe");

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

                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDVENDEDOR", "System.Int32", "=", Convert.ToInt32(cbFuncionario.SelectedValue).ToString()));

                    if (Convert.ToInt32(cbTransportador.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDTRANSPORTES", "System.Int32", "=", Convert.ToInt32(cbTransportador.SelectedValue).ToString()));


                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", DataFinal));

                    if (txtCidade1.Text.Trim() != string.Empty)
                        RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", _COD_MUN_IBGE.ToString()));

                    if(Convert.ToInt32(cbStatus.SelectedValue) > 0)
                        RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", Convert.ToInt32(cbStatus.SelectedValue).ToString()));

                    if (chkNFeEnviada.Checked)
                    {
                        RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                        RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));
                    }
                    
                    if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                        LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio, "IDTRANSPORTES, IDPEDIDO");
                    else
                        LIS_NOTAFISCALEColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                    

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
            using (FrmSearchCidade frm = new FrmSearchCidade())
            {
                frm.ShowDialog();

                var result = frm.Result;

                if (result > 0)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", result.ToString()));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_MUNICIPIOSColl.Count > 0)
                    {
                        txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO + "/" + LIS_MUNICIPIOSColl[0].UF;
                        _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
                    }

                }
            }
        }

        private void txtCidade1_Enter(object sender, EventArgs e)
        {

            using (FrmSearchCidade frm = new FrmSearchCidade())
            {
                frm.ShowDialog();

                var result = frm.Result;

                if (result > 0)
                {
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", result.ToString()));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_MUNICIPIOSColl.Count > 0)
                    {
                        txtCidade1.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO + "/" + LIS_MUNICIPIOSColl[0].UF;
                        _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
                    }

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCidade1.Text = string.Empty;
            _COD_MUN_IBGE = -1;
        }
      
    }
}
