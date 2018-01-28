using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Vendas;
using BMSSoftware.Modulos.Cadastros;
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
using winfit.Modulos.Outros;

namespace BmsSoftware.Modulos.Telemarketing
{
    public partial class FrmAgendaTLMK : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
        STATUSTLMKCollection STATUSTLMKColl = new STATUSTLMKCollection();
        LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl = new LIS_AGENDAVENDEDORTLMKCollection();
        LIS_AGENDAVENDEDORTLMKProvider LIS_AGENDAVENDEDORTLMKP = new LIS_AGENDAVENDEDORTLMKProvider();
        AGENDAVENDEDORTLMKProvider AGENDAVENDEDORTLMKP = new AGENDAVENDEDORTLMKProvider();
        LIS_OCORRENCIATLMKCollection LIS_OCORRENCIATLMKColl = new LIS_OCORRENCIATLMKCollection();
        LIS_OCORRENCIATLMKProvider LIS_OCORRENCIATLMKP = new LIS_OCORRENCIATLMKProvider();
        AVISOAGENDATLMKProvider AVISOAGENDATLMKP = new AVISOAGENDATLMKProvider();
        LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();

        int _IDAGENDAVENDEDORTLMK = -1;
        int _IDCLIENTE = -1;
        int _IDFUNCIONARIO = -1;

        public FrmAgendaTLMK()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FrmAgendaTLMK_Load(object sender, EventArgs e)
        {
            GetDropStatus();
            GetDropStatus2();
            GetDropVendedor();
            GetDropGrupo();
            tabControl1.SelectTab(1);
            GetDropCidade();

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);
            btnDataProxContato.Image = Util.GetAddressImage(11);
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnGrupo.Image = Util.GetAddressImage(6);
            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19); 
            btnSalvar.Image = Util.GetAddressImage(15);
            btnLimpa.Image = Util.GetAddressImage(16);
            btnSair.Image = Util.GetAddressImage(21);
            btnSair2.Image = Util.GetAddressImage(21);
            btnSeach.Image = Util.GetAddressImage(20);
            btnPesquisa.Image = Util.GetAddressImage(15);

            Pesquisa();
            AdicionaAvisos();

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();           
     
        }

        private void Pesquisa()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSTLMK", "System.Int32", "=", cbStatus.SelectedValue.ToString()));

                if (Convert.ToInt32(cbGrupo.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDGRUPOAGENDATLMK", "System.Int32", "=", cbGrupo.SelectedValue.ToString()));

                if (Convert.ToInt32(cbCidade.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", cbCidade.SelectedValue.ToString()));


                if ((msktDataInicial.Text != "  /  /") && (msktDataFinal.Text != "  /  /"))
                {
                    string DataInicial = Util.ConverStringDateSearch(msktDataInicial.Text);
                    string DataFinal = Util.ConverStringDateSearch(msktDataFinal.Text);

                    RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", ">=", DataInicial));
                    RowRelatorio.Add(new RowsFiltro("DATACONTATO", "System.DateTime", "<=", DataFinal));
                }

                RowRelatorio.Add(new RowsFiltro("FLAGEXIBIR", "System.String", "=", "S"));

                if (!chkOrdenarDataHora.Checked)
                    LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "NOMECLIENTE");                
                else
                    LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "DATACONTATO, HORA ");                

                //Remove cliente repetido na agenda
                LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl_2 = new LIS_AGENDAVENDEDORTLMKCollection();
                foreach (LIS_AGENDAVENDEDORTLMKEntity item in LIS_AGENDAVENDEDORTLMKColl)
                {
                    if (LIS_AGENDAVENDEDORTLMKColl_2.Find(delegate(LIS_AGENDAVENDEDORTLMKEntity item2)
                    { return (item2.IDCLIENTE == item.IDCLIENTE ); }) == null)
                    {
                        LIS_AGENDAVENDEDORTLMKColl_2.Add(item);
                    }
                }

                LIS_AGENDAVENDEDORTLMKColl.Clear();
                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKColl_2;

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;

                lblTotalPesquisa.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();

                PaintGrid();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PaintGrid()
        {
           
            try
            {
                int i = 0;
                foreach (LIS_AGENDAVENDEDORTLMKEntity item in LIS_AGENDAVENDEDORTLMKColl)
                {

                    STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                    STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                    STATUSTLMKTy = STATUSTLMKP.Read(Convert.ToInt32(item.IDSTATUSTLMK));

                    //Busca Cor
                    int a = Convert.ToInt32(STATUSTLMKTy.COLORA);
                    int r = Convert.ToInt32(STATUSTLMKTy.COLOR);
                    int g = Convert.ToInt32(STATUSTLMKTy.COLORG);
                    int b = Convert.ToInt32(STATUSTLMKTy.COLORB);

                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(a, r, g, b);

                    i++;
                }
               
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void GetDropCidade()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(null, "MUNICIPIO");

                cbCidade.DisplayMember = "MUNICIPIO";
                cbCidade.ValueMember = "COD_MUN_IBGE";

                LIS_MUNICIPIOSEntity LIS_MUNICIPIOSTy = new LIS_MUNICIPIOSEntity();
                LIS_MUNICIPIOSTy.MUNICIPIO = ConfigMessage.Default.MsgDrop;
                LIS_MUNICIPIOSTy.COD_MUN_IBGE = -1;
                LIS_MUNICIPIOSColl.Add(LIS_MUNICIPIOSTy);

                Phydeaux.Utilities.DynamicComparer<LIS_MUNICIPIOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_MUNICIPIOSEntity>(cbCidade.DisplayMember);

                LIS_MUNICIPIOSColl.Sort(comparer.Comparer);
                cbCidade.DataSource = LIS_MUNICIPIOSColl;

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropVendedor()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
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

                //Busca Funcionario
                USUARIOEntity USUARIOTY = new USUARIOEntity();
                USUARIOProvider USUARIOP = new USUARIOProvider();
                USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                if (USUARIOTY != null)
                {
                    _IDFUNCIONARIO = Convert.ToInt32(USUARIOTY.IDFUNCIONARIO);
                    cbFuncionario.SelectedValue = _IDFUNCIONARIO;
                }

                this.Cursor = Cursors.Default;	
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;		
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropStatus()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                STATUSTLMKColl = STATUSTLMKP.ReadCollectionByParameter(null, "NOME");
                
                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUSTLMK";

                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTLMKTy.IDSTATUSTLMK = -1;
                STATUSTLMKColl.Add(STATUSTLMKTy);

                Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity>(cbStatus.DisplayMember);

                STATUSTLMKColl.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSTLMKColl;   
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropStatus2()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                STATUSTLMKColl = STATUSTLMKP.ReadCollectionByParameter(null, "NOME");

                cbStatus2.DisplayMember = "NOME";
                cbStatus2.ValueMember = "IDSTATUSTLMK";

                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTLMKTy.IDSTATUSTLMK = -1;
                STATUSTLMKColl.Add(STATUSTLMKTy);

                Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity>(cbStatus2.DisplayMember);

                STATUSTLMKColl.Sort(comparer.Comparer);
                cbStatus2.DataSource = STATUSTLMKColl;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Agenda do Telemarketing";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Agenda do Telemarketing");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatusTLMK frm = new FrmStatusTLMK())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUSTLMK = CodSelec;
                frm.ShowDialog();

                GetDropStatus();
                GetDropStatus2();
                cbStatus.SelectedValue = CodSelec;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSair2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public MonthCalendar monthCalendar4 = new MonthCalendar();
        Form FormCalendario4 = new Form();

        private void btnDataProxContato_Click(object sender, EventArgs e)
        {
            monthCalendar4.Name = "monthCalendar3";
            monthCalendar4.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar4_DateSelected);

            FormCalendario4.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario4.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario4.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario4.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario4.Location = new Point(230, 55);
            FormCalendario4.Name = "FrmCalendario4";
            FormCalendario4.Text = "Calendário";
            FormCalendario4.ResumeLayout(false);
            FormCalendario4.Controls.Add(monthCalendar4);
            FormCalendario4.ShowDialog();
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            mkdDataProxContato.Text = monthCalendar4.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario4.Close();
        }

        private void btnemail_Click(object sender, EventArgs e)
        {
            using (FrmEnviarEmail Frm = new FrmEnviarEmail())
            {
                Frm._IDCLIENTE = _IDCLIENTE;
                Frm.ShowDialog();
            }
        }

        private void cbStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
            {
                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                STATUSTLMKTy = STATUSTLMKP.Read(Convert.ToInt32(cbStatus.SelectedValue));

                if (STATUSTLMKTy != null)
                {
                    //Busca Cor
                    int _COLORA = Convert.ToInt32(STATUSTLMKTy.COLORA);
                    int _COLOR = Convert.ToInt32(STATUSTLMKTy.COLOR);
                    int _COLORG = Convert.ToInt32(STATUSTLMKTy.COLORG);
                    int _COLORB = Convert.ToInt32(STATUSTLMKTy.COLORB);
                    Color TipoCor = Color.FromArgb(_COLORA, _COLOR, _COLORG, _COLORB);
                    ColorDialog cd = new ColorDialog();
                    cd.Color = TipoCor;
                    label2.ForeColor = cd.Color;
                    label2.Text = "Status: " + STATUSTLMKTy.NOME;
                }
            }
            else
            {
                label2.ForeColor = Color.Black;
                 label2.Text = "Status:";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Pesquisa();
        }

        private void DataGriewDados_Paint(object sender, PaintEventArgs e)
        {
           
        }
      

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PesquisaRapida();
            
        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "And"));

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                RowRelatorio.Add(new RowsFiltro("FLAGEXIBIR", "System.String", "=", "S"));
                
                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "NOMECLIENTE");
                DataGriewDados.AutoGenerateColumns = false;

                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;
                lblTotalPesquisa.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();

                PaintGrid();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
            {
                using (FrmCliente frm = new FrmCliente())
                {
                    frm.CodClienteSelec = _IDCLIENTE;
                    frm.ShowDialog();
                }
            }
            else
            {
                using (FrmCliente2 frm = new FrmCliente2())
                {
                    frm.CodClienteSelec = _IDCLIENTE;
                    frm.ShowDialog();
                }
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             int rowindex = e.RowIndex;
             if (LIS_AGENDAVENDEDORTLMKColl.Count > 0 && rowindex > -1)
             {
                 int ColumnSelecionada = e.ColumnIndex;
                 if (ColumnSelecionada == 0)//Editar
                 {
                     LimpaOcorrencia();

                     _IDAGENDAVENDEDORTLMK = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDAGENDAVENDEDORTLMK);
                     _IDCLIENTE = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDCLIENTE);
                     tabControl1.SelectTab(0);

                     //Dados do Clientes
                     LIS_CLIENTECollection LIS_CLIENTE2Coll = new LIS_CLIENTECollection();
                     LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                     
                     RowRelatorio.Clear();
                     RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", _IDCLIENTE.ToString()));
                     LIS_CLIENTE2Coll = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                     if(LIS_CLIENTE2Coll.Count > 0)
                     {
                         txtNome.Text = LIS_CLIENTE2Coll[0].NOME;

                         txtTelefone1.Text = Util.RetiraLetras(LIS_CLIENTE2Coll[0].TELEFONE1);                         
                         //Formata o telefone
                         if (txtTelefone1.Text.Length == 10)
                             txtTelefone1.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(txtTelefone1.Text));
                         else if (txtTelefone1.Text.Length == 11)
                             txtTelefone1.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(txtTelefone1.Text));
                        
                         txtTelefone2.Text = Util.RetiraLetras(LIS_CLIENTE2Coll[0].FAX);
                         //Formata o telefone
                         if (txtTelefone2.Text.Length == 10)
                             txtTelefone2.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(txtTelefone2.Text));
                         else if (txtTelefone2.Text.Length == 11)
                             txtTelefone2.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(txtTelefone2.Text));

                         txtCelular.Text = Util.RetiraLetras(LIS_CLIENTE2Coll[0].TELEFONE2);
                         //Formata o telefone
                         if (txtCelular.Text.Length == 10)
                             txtCelular.Text = string.Format("{0:(##) ####-####}", Convert.ToInt64(txtCelular.Text));
                         else if (txtCelular.Text.Length == 11)
                             txtCelular.Text = string.Format("{0:(##) #####-####}", Convert.ToInt64(txtCelular.Text));

                         txtBairro.Text = LIS_CLIENTE2Coll[0].BAIRRO1;
                         txtCidade1.Text = LIS_CLIENTE2Coll[0].MUNICIPIO;
                         txtUF1.Text = LIS_CLIENTE2Coll[0].UF;
                         txtEmailCliente.Text = LIS_CLIENTE2Coll[0].EMAILCLIENTE;

                         PesquisaOcorrencia();
                     }
                     
                 }
             }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(Validacoes())
            {
                GravaAgenda();
                GravaOcorrencia();

                if (chkAviso.Checked)
                    GravaAviso();

                //Limpa Dados
                txtNomeContato.Text = string.Empty;
                txtConversa.Text = string.Empty;
                mkdDataProxContato.Text = "  /  /";
                mktHora.Text = "  :";
                chkAviso.Checked = false;
                cbStatus2.SelectedValue = -1;

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
        }

        private void GravaAviso()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                AVISOAGENDATLMKEntity AVISOAGENDATLMKTy = new AVISOAGENDATLMKEntity();
                AVISOAGENDATLMKTy.IDAVISOAGENDATLMK = -1;
                AVISOAGENDATLMKTy.DATAPROXCONTATO = Convert.ToDateTime(mkdDataProxContato.Text);
                AVISOAGENDATLMKTy.HORAPROXCONTATO = mktHora.Text;
                AVISOAGENDATLMKTy.IDCLIENTE = _IDCLIENTE;
                AVISOAGENDATLMKTy.IDFUNCIONARIO = _IDFUNCIONARIO;
                AVISOAGENDATLMKTy.FLAGVISUALIZADO = "N";
                AVISOAGENDATLMKTy.FLAGADICIONADO = "N";
                AVISOAGENDATLMKP.Save(AVISOAGENDATLMKTy);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GravaAgenda()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                AGENDAVENDEDORTLMKEntity AGENDAVENDEDORTLMKTy = new AGENDAVENDEDORTLMKEntity();
                AGENDAVENDEDORTLMKTy = AGENDAVENDEDORTLMKP.Read(_IDAGENDAVENDEDORTLMK);

                if (AGENDAVENDEDORTLMKTy != null)
                {
                    AGENDAVENDEDORTLMKTy.IDSTATUSTLMK = Convert.ToInt32(cbStatus2.SelectedValue);
                    
                    if (mkdDataProxContato.Text != "  /  /")
                        AGENDAVENDEDORTLMKTy.DATACONTATO = Convert.ToDateTime(mkdDataProxContato.Text);

                    AGENDAVENDEDORTLMKTy.HORA = mktHora.Text;
                    AGENDAVENDEDORTLMKP.Save(AGENDAVENDEDORTLMKTy);              
                         
                }

                this.Cursor = Cursors.Default;
                    
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GravaOcorrencia()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                OCORRENCIATLMKEntity OCORRENCIATLMKTy = new OCORRENCIATLMKEntity();
                OCORRENCIATLMKProvider OCORRENCIATLMKP = new OCORRENCIATLMKProvider();

                OCORRENCIATLMKTy.IDOCORRENCIATLMK = -1;
                OCORRENCIATLMKTy.IDFUNCIONARIO = _IDFUNCIONARIO;
                OCORRENCIATLMKTy.IDSTATUSTLMK  =  Convert.ToInt32(cbStatus2.SelectedValue);
                OCORRENCIATLMKTy.DATACONTATO  = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                OCORRENCIATLMKTy.HORACONTATO   = DateTime.Now.ToString("HH:mm");

                 if (mkdDataProxContato.Text != "  /  /")
                        OCORRENCIATLMKTy.DATAPROXCONTATO = Convert.ToDateTime(mkdDataProxContato.Text);
                
                OCORRENCIATLMKTy.HORAPROXCONTATO  = mktHora.Text;
                OCORRENCIATLMKTy.NOMECONTATO  = txtNomeContato.Text;
                OCORRENCIATLMKTy.CONVERSA = txtConversa.Text;
                OCORRENCIATLMKTy.IDCLIENTE = _IDCLIENTE;

                OCORRENCIATLMKP.Save(OCORRENCIATLMKTy);

                PesquisaOcorrencia();
                Pesquisa();                 

                tabControl1.SelectTab(1);

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void PesquisaOcorrencia()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();                
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", _IDCLIENTE.ToString()));
            
                LIS_OCORRENCIATLMKColl = LIS_OCORRENCIATLMKP.ReadCollectionByParameter(RowRelatorio, "IDOCORRENCIATLMK DESC");               

                dtOcorrencias.AutoGenerateColumns = false;
                dtOcorrencias.DataSource = LIS_OCORRENCIATLMKColl;

                label13.Text = LIS_OCORRENCIATLMKColl.Count.ToString();

                if (LIS_OCORRENCIATLMKColl.Count > 0)
                    txtNomeContato.Text = LIS_OCORRENCIATLMKColl[0].NOMECONTATO;
                
                this.Cursor = Cursors.Default;            

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (_IDAGENDAVENDEDORTLMK == -1)
            {
                tabControl1.SelectTab(1);
                Util.ExibirMSg("Agenda não selecionada!", "Red");
                errorProvider1.SetError(DataGriewDados, "Agenda não selecionada!");
                result = false;
            }
            else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (txtNomeContato.Text.Trim().Count() == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label4, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            } 
            else if (Convert.ToInt32(cbStatus2.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label5, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (txtConversa.Text.Trim().Count() == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label8, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }     
            else if (mktHora.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mktHora.Text))
            {
                Util.ExibirMSg("Hora inválida", "Red");
                errorProvider1.SetError(label7, "Hora inválida");
                result = false;
            }
            else if (mkdDataProxContato.Text != "  /  /" && !ValidacoesLibrary.ValidaTipoDateTime(mkdDataProxContato.Text))
            {
                errorProvider1.SetError(label6, "Data inválida");
                Util.ExibirMSg("Data inválida", "Red");
                result = false;
            }
            else if(chkAviso.Checked && mkdDataProxContato.Text == "  /  /" )
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label6, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (chkAviso.Checked && mktHora.Text == "  :")
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if ((mkdDataProxContato.Text != "  /  /")&& (Convert.ToDateTime(mkdDataProxContato.Text) < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"))))
            {
                Util.ExibirMSg("Data não pode ser menor que : " + DateTime.Now.ToString("dd/MM/yyyy"), "Red");
                errorProvider1.SetError(label6, "Data não pode ser menor que : " + DateTime.Now.ToString("dd/MM/yyyy"));
                result = false;    
            }
            else if (VerificaStatus(Convert.ToInt32(cbStatus2.SelectedValue), 1) && mkdDataProxContato.Text == "  /  /") //Verifica se Data Obrigatorio
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label6, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (VerificaStatus(Convert.ToInt32(cbStatus2.SelectedValue), 2) && mktHora.Text == "  :") //Verifica se Hora Obrigatoorio Obrigatorio
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label7, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            return result;
        }

        private Boolean VerificaStatus(int IDSTATUSTLMK, int Tipo)
        {
            Boolean result = false;

            try
            {
                STATUSTLMKCollection STATUSTLMKColl = new STATUSTLMKCollection();
                RowRelatorio.Clear();

                if (Tipo == 1) // Data Contato
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGDATACONTATO", "System.String", "=", "S", "and"));
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSTLMK", "System.Int32", "=", IDSTATUSTLMK.ToString()));

                    STATUSTLMKColl = STATUSTLMKP.ReadCollectionByParameter(RowRelatorio);
                }
                else if (Tipo == 2) // Hora Contato
                {
                    RowRelatorio.Add(new RowsFiltro("FLAGHORACONTATO", "System.String", "=", "S", "and"));
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSTLMK", "System.Int32", "=", IDSTATUSTLMK.ToString()));

                    STATUSTLMKColl = STATUSTLMKP.ReadCollectionByParameter(RowRelatorio);
                }
              
                
                if (STATUSTLMKColl.Count > 0)
                    result = true;
                
                if(Tipo == 3) // Nenhum
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private void cbStatus2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbStatus2.SelectedValue) > 0)
            {
                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                STATUSTLMKTy = STATUSTLMKP.Read(Convert.ToInt32(cbStatus2.SelectedValue));

                if (STATUSTLMKTy != null)
                {
                    //Busca Cor
                    int _COLORA = Convert.ToInt32(STATUSTLMKTy.COLORA);
                    int _COLOR = Convert.ToInt32(STATUSTLMKTy.COLOR);
                    int _COLORG = Convert.ToInt32(STATUSTLMKTy.COLORG);
                    int _COLORB = Convert.ToInt32(STATUSTLMKTy.COLORB);
                    Color TipoCor = Color.FromArgb(_COLORA, _COLOR, _COLORG, _COLORB);
                    ColorDialog cd = new ColorDialog();
                    cd.Color = TipoCor;
                    label5.ForeColor = cd.Color;
                    label5.Text = "Status: " + STATUSTLMKTy.NOME;
                }
            }
            else
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Status:";
            }
        }

        private void mktHora_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (mktHora.Text != "  :" && !ValidacoesLibrary.ValidaTipoHoraValida(mktHora.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroHora + " " + mktHora.Text);
                errorProvider1.SetError(mktHora, ConfigMessage.Default.MsgErroHora);
            }
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            using (FrmGrupoAgendaTLMK frm = new FrmGrupoAgendaTLMK())
            {
                int CodSelec = Convert.ToInt32(cbGrupo.SelectedValue);
                frm._IDGRUPOAGENDATLMK = CodSelec;
                frm.ShowDialog();

                GetDropGrupo();
                cbGrupo.SelectedValue = CodSelec;
            }
        }

        private void GetDropGrupo()
        {
            try
            {
                GRUPOAGENDATLMKCollection GRUPOAGENDATLMKColl = new GRUPOAGENDATLMKCollection();
                GRUPOAGENDATLMKProvider GRUPOAGENDATLMKP = new GRUPOAGENDATLMKProvider();
                GRUPOAGENDATLMKColl = GRUPOAGENDATLMKP.ReadCollectionByParameter(null, "NOME");

                cbGrupo.DisplayMember = "NOME";
                cbGrupo.ValueMember = "IDGRUPOAGENDATLMK";

                GRUPOAGENDATLMKEntity GRUPOAGENDATLMKTy = new GRUPOAGENDATLMKEntity();
                GRUPOAGENDATLMKTy.NOME = ConfigMessage.Default.MsgDrop;
                GRUPOAGENDATLMKTy.IDGRUPOAGENDATLMK = -1;
                GRUPOAGENDATLMKColl.Add(GRUPOAGENDATLMKTy);

                Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity>(cbGrupo.DisplayMember);

                GRUPOAGENDATLMKColl.Sort(comparer.Comparer);
                cbGrupo.DataSource = GRUPOAGENDATLMKColl;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AdicionaAvisos()
        {
            try
            {

                AVISOAGENDATLMKCollection AVISOAGENDATLMKColl = new AVISOAGENDATLMKCollection();

                RowRelatorio.Clear();
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                AVISOAGENDATLMKColl = AVISOAGENDATLMKP.ReadCollectionByParameter(RowRelatorio, "DATAPROXCONTATO, HORAPROXCONTATO");

                int Contador = Convert.ToInt32(btnAviso.Text);
                foreach (AVISOAGENDATLMKEntity item in AVISOAGENDATLMKColl)
                {
                    if (item.FLAGVISUALIZADO == "N")
                    {
                        DateTime DataAtual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        string Hora = DateTime.Now.ToString("HH:mm");

                        if (DataAtual >= item.DATAPROXCONTATO && Convert.ToDateTime(Hora) >= Convert.ToDateTime(item.HORAPROXCONTATO)) //adicionado os novos
                        {
                            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                            CLIENTETy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));

                            if (CLIENTETy != null && item.FLAGVISUALIZADO == "N")
                            {
                                Contador++;
                                btnAviso.Text = Contador.ToString();

                                //Altera Adicionado
                                AVISOAGENDATLMKEntity AVISOAGENDATLMKTy = new AVISOAGENDATLMKEntity();
                                AVISOAGENDATLMKTy = AVISOAGENDATLMKP.Read(item.IDAVISOAGENDATLMK);
                                AVISOAGENDATLMKTy.FLAGADICIONADO = "S";
                                AVISOAGENDATLMKTy.FLAGVISUALIZADO = "S";
                                AVISOAGENDATLMKP.Save(AVISOAGENDATLMKTy);

                                MessageBox.Show("Aviso de contato para o cliente:  " + item.IDCLIENTE + " " + CLIENTETy.NOME,
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);

                                break;
                            } 
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                AVISOAGENDATLMKCollection AVISOAGENDATLMKColl = new AVISOAGENDATLMKCollection();                
                
                RowRelatorio.Clear();
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));
                
                AVISOAGENDATLMKColl = AVISOAGENDATLMKP.ReadCollectionByParameter(RowRelatorio, "DATAPROXCONTATO, HORAPROXCONTATO");

                int Contador = Convert.ToInt32(btnAviso.Text);
                foreach (AVISOAGENDATLMKEntity item in AVISOAGENDATLMKColl)
                {
                    if (item.FLAGADICIONADO == "N")
                    {
                        DateTime DataAtual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        string Hora = DateTime.Now.ToString("HH:mm");

                        if (DataAtual >= item.DATAPROXCONTATO && Convert.ToDateTime(Hora) >= Convert.ToDateTime(item.HORAPROXCONTATO)) //adicionado os novos
                        {
                            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                            CLIENTETy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));

                            Contador++;
                            btnAviso.Text = Contador.ToString();

                            if (CLIENTETy != null && item.FLAGVISUALIZADO == "N")
                            {
                                //Altera Adicionado
                                AVISOAGENDATLMKEntity AVISOAGENDATLMKTy = new AVISOAGENDATLMKEntity();
                                AVISOAGENDATLMKTy = AVISOAGENDATLMKP.Read(item.IDAVISOAGENDATLMK);
                                AVISOAGENDATLMKTy.FLAGADICIONADO = "S";
                                AVISOAGENDATLMKTy.FLAGVISUALIZADO = "S";
                                AVISOAGENDATLMKP.Save(AVISOAGENDATLMKTy);

                                MessageBox.Show("Aviso de contato para o cliente:  " +item.IDCLIENTE + " " + CLIENTETy.NOME , 
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);
                            }
                          
                        }                        
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }

        private void btnAviso_Click(object sender, EventArgs e)
        {
            PesquisaAviso();
        }

        private void PesquisaAviso()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);


            try
            {
                AVISOAGENDATLMKCollection AVISOAGENDATLMKColl = new AVISOAGENDATLMKCollection();

                RowRelatorio.Clear();
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

                RowRelatorio.Add(new RowsFiltro("FLAGVISUALIZADO", "System.String", "=", "N"));

                AVISOAGENDATLMKColl = AVISOAGENDATLMKP.ReadCollectionByParameter(RowRelatorio, "DATAPROXCONTATO, HORAPROXCONTATO");

                RowRelatorio.Clear();
                foreach (AVISOAGENDATLMKEntity item in AVISOAGENDATLMKColl)
                {
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", item.IDCLIENTE.ToString(), "And"));

                    //Altera Visualizado
                    AVISOAGENDATLMKEntity AVISOAGENDATLMKTy = new AVISOAGENDATLMKEntity();
                    AVISOAGENDATLMKTy = AVISOAGENDATLMKP.Read(item.IDAVISOAGENDATLMK);
                    AVISOAGENDATLMKTy.FLAGVISUALIZADO = "S";
                    AVISOAGENDATLMKP.Save(AVISOAGENDATLMKTy);
                }

                
                RowRelatorio.Add(new RowsFiltro("FLAGEXIBIR", "System.String", "=", "S"));

                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "NOMECLIENTE");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;

                lblTotalPesquisa.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();

                PaintGrid();

                btnAviso.Text = "0";

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             int rowindex = e.RowIndex;
             if (LIS_OCORRENCIATLMKColl.Count > 0 && rowindex > -1)
             {
                 int ColumnSelecionada = e.ColumnIndex;

                 if (ColumnSelecionada == 0)//Editar
                 {
                     txtConversa.Text = LIS_OCORRENCIATLMKColl[rowindex].CONVERSA;
                     txtNomeContato.Text = LIS_OCORRENCIATLMKColl[rowindex].NOMECONTATO;
                     mkdDataProxContato.Text = LIS_OCORRENCIATLMKColl[rowindex].DATAPROXCONTATO.ToString();
                     mktHora.Text = LIS_OCORRENCIATLMKColl[rowindex].HORAPROXCONTATO;
                     cbStatus2.SelectedValue = LIS_OCORRENCIATLMKColl[rowindex].IDSTATUSTLMK;
;
                 }
             }
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            using (FrmPedidoNormal frm = new FrmPedidoNormal())
            {
               frm.CodClienteSelec =_IDCLIENTE;
               frm.CodFuncionario = Convert.ToInt32(cbFuncionario.SelectedValue);
                frm.ShowDialog();
            }
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            using (FrmSearchProduto frm = new FrmSearchProduto())
            {
                frm.ShowDialog();
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            
        }

        private void cbCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchCidade frm = new FrmSearchCidade())
                {
                    frm.ShowDialog();

                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbCidade.SelectedValue = result;
                    }
                }
            }
        }

        private void cbCidade_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbCidade.SelectedValue) > 0)
            {
                
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", cbCidade.SelectedValue.ToString()));
                LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);
                

                if (LIS_MUNICIPIOSColl.Count > 0)
                    label16.Text = "Cidade: " + LIS_MUNICIPIOSColl[0].MUNICIPIO + " - " + LIS_MUNICIPIOSColl[0].UF;
                else
                    label16.Text = "Cidade: ";
            }
            else
                label16.Text = "Cidade: ";
            
        }

        private void cbCidade_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Pressione Ctrl+E para Pesquisar a Cidade.";
        }

        private void cbCidade_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = "";
        }

        private void mktHora_Enter(object sender, EventArgs e)
        {
            try
            {
                mktHora.Focus();
                mktHora.SelectionStart = 0;
                mktHora.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnDesbloqueia_Click(object sender, EventArgs e)
        {
            if (Util.Acessa_Tela("FrmAgendaTLMK2", FrmLogin._IdNivel))
                cbFuncionario.Enabled = true;
            else
                cbFuncionario.Enabled = false;
        }

        private void mkdDataProxContato_Enter(object sender, EventArgs e)
        {
            try
            {
                mkdDataProxContato.Focus();
                mkdDataProxContato.SelectionStart = 0;
                mkdDataProxContato.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            LimpaOcorrencia();
        }

        private void LimpaOcorrencia()
        {
            txtConversa.Text = string.Empty;
            txtNomeContato.Text = string.Empty;
            mkdDataProxContato.Text = "  /  /";
            mktHora.Text = "  :";
            cbStatus2.SelectedValue = -1;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPesquisaRapida_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Para Efetuar a Pesquisa Rápida pressione ENTER!";
        }

        private void txtPesquisaRapida_Leave(object sender, EventArgs e)
        {
            lblObsField.Text = "";
        }
    }
}


 