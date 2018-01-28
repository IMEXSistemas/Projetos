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
using BMSworks.Collection;
using BMSworks.UI;
using BmsSoftware.Modulos.FrmSearch;
using System.IO;
using BmsSoftware.Modulos.Operacional;
using CDSSoftware;
using BMSSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Modulos.Cadastros;
using System.Diagnostics;
using BmsSoftware.Modulos.SMS;
using winfit.Modulos.Outros;
using BmsSoftware.UI;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmRelacaoDuplicatas : Form
    {
        Utility Util = new Utility();

        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        CLIENTECollection CLIENTEColl = new CLIENTECollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodClienteSelec = -1;
        public Boolean ExibiDados = false;
        public string DataConsultaSelec = string.Empty;
        public FrmRelacaoDuplicatas()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        private void RegisterFocusEvents(Control.ControlCollection controls)
        {

            foreach (Control control in controls)
            {
                if ((control is TextBox) ||
                  (control is RichTextBox) ||
                  (control is ComboBox) ||
                  (control is MaskedTextBox))
                {
                    control.Enter += new EventHandler(controlFocus_Enter);
                    control.Leave += new EventHandler(controlFocus_Leave);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            lblObsField.Text = "Obs.:";
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }


        private void FrmExtratoDuplReceber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmExtratoDuplReceber_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void cbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            PesquisaExtrato();
        }

        private void PesquisaExtrato()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            PesquisaDuplicatas();
            SumTotalPesquisada();
            PaintGrid();


            this.Cursor = Cursors.Default;
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {

                    if (item.NOMESTATUS == "ABERTO")
                    {
                        DataGridRelaDupl.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else if (item.NOMESTATUS == "VENCIDA")
                    {
                        DataGridRelaDupl.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (item.NOMESTATUS == "PAGO")
                    {
                        DataGridRelaDupl.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (item.NOMESTATUS == "PAGO PARCIAL")
                    {
                        DataGridRelaDupl.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }

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

        decimal TotalDuplicata = 0;
        decimal TotalDevedor = 0;
        decimal TotalPago = 0;
        decimal TotalMulta = 0;
        decimal TotalDesconto = 0;
        public void SumTotalPesquisada()
        {
            try
            {
                TotalDuplicata = 0;
                TotalDevedor = 0;
                TotalPago = 0;
                TotalDesconto = 0;
                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    if (item.IDCLIENTE != null)
                    {
                        TotalDuplicata += Convert.ToDecimal(item.VALORDUPLICATA);
                        TotalDevedor += Convert.ToDecimal(item.VALORDEVEDOR);
                        TotalPago += Convert.ToDecimal(item.VALORPAGO);
                        TotalMulta += Convert.ToDecimal(item.VALORMULTA);
                        TotalDesconto += Convert.ToDecimal(item.VALORDESCONTO);
                    }
                }

                lblTotalDuplicata.Text = TotalDuplicata.ToString("n2");
                lblTotalDuplicata.TextAlign = ContentAlignment.MiddleRight;
                lblTotalDevedor.Text = TotalDevedor.ToString("n2");
                lblTotalDevedor.TextAlign = ContentAlignment.MiddleRight;
                lblTotalRecebido.Text = TotalPago.ToString("n2");
                lblTotalRecebido.TextAlign = ContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PesquisaDuplicatas()
        {

            string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
            DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.


            RowRelatorio.Clear();
            if (rbVencidas.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 

                if (mkDtInicial.Text == "  /  /")
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<", DataAtual));

                //if (rdDataEmissao.Checked)
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}
                //else if (rdDataPagto.Checked)
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}
                //else
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}

            }
            else if (rbVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago

                if (mkDtInicial.Text == "  /  /")
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", DataAtual));
                }
                //if (rdDataEmissao.Checked)
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}
                //else if (rdDataPagto.Checked)
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}
                //else
                //{
                //    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", DataAtual));
                //    RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                //}

            }
            else if (rbVencidasVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
            }
            else if (rbPagas.Checked)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "in(", "3,4)")); // 3 Pago - 4 Pago Parcial

            if (mkDtInicial.Text != "  /  /")
            {
                if (rdDataVencto.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                }
                else if (rdDataEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                }
                else if (rdDataPagto.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", ">=", Util.ConverStringDateSearch(mkDtInicial.Text)));
                }

            }

            if (mkdatafinal.Text != "  /  /")
            {
                if (rdDataVencto.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));
                }
                if (rdDataEmissao.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));
                }
                else if (rdDataPagto.Checked)
                {
                    RowRelatorio.Add(new RowsFiltro("DATAPAGTO", "System.DateTime", "<=", Util.ConverStringDateSearch(mkdatafinal.Text)));
                }

            }

            if (Convert.ToInt32(cbLocalCobranca.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDLOCALCOBRANCA", "System.Int32", "=", cbLocalCobranca.SelectedValue.ToString()));

            if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbFuncionario.SelectedValue.ToString()));

            if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));

            if (Convert.ToInt32(cbTipo.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", cbTipo.SelectedValue.ToString()));

            if (Convert.ToInt32(cbCliente.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));


            //Ordem da exibição no grid
            if (rdDataEmissao.Checked)
            {
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO");
            }
            else if (rdDataVencto.Checked)
            {
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
            }
            else if (rdDataPagto.Checked)
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAPAGTO");

            //Percorre a coleção calculando juros de atraso
            SumJuroDuplicata();


            //Colocando somatorio no final da lista
            LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
            LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
            LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
            LIS_DUPLICATARECEBERTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
            LIS_DUPLICATARECEBERTy.COMISSAO = SumTotalPesquisa("COMISSAO");
            LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);

            DataGridRelaDupl.AutoGenerateColumns = false;
            DataGridRelaDupl.DataSource = LIS_DUPLICATARECEBERColl;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);
                else if (NomeCampo == "COMISSAO")
                    valortotal += Convert.ToDecimal(item.COMISSAO);
            }

            return valortotal;
        }

        //Percorre a coleção calculando juros de atraso
        public void SumJuroDuplicata()
        {
            JUROSDUPLICATASEntity JUROSDUPLICATASty = new JUROSDUPLICATASEntity();
            JUROSDUPLICATASProvider JUROSDUPLICATASP = new JUROSDUPLICATASProvider();
            JUROSDUPLICATASty = JUROSDUPLICATASP.Read(2);//2 Contas a Receber


            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                //Somente calcula juros de duplicatas que não foram atualizada no dia
                // e vencidas
                //string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                //string DataAtDupl = Convert.ToDateTime(item.DATAATJUROS).ToString("dd/MM/yyyy");
                //if (item.DATAVECTO < DateTime.Now && Convert.ToDateTime(DataAtDupl) < Convert.ToDateTime(DataAtual)
                //    && item.IDSTATUS != 3)

                if (item.IDSTATUS != 3)
                {
                    //Calculo de dias de vencimento
                    TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(item.DATAVECTO);
                    int DIASATRASO = date.Days;

                    if (DIASATRASO > 0)
                        item.DIASATRASO = DIASATRASO;
                    else
                        item.DIASATRASO = 0;


                    item.DATAATJUROS = DateTime.Now;

                    //Calculo o juros de atraso
                    if (JUROSDUPLICATASty.FLAGCALCULAR == "S")
                    {
                        //Calculo o juros de atraso
                        decimal PorcJuros = Convert.ToDecimal(JUROSDUPLICATASty.JUROSDIA * item.DIASATRASO);
                        PorcJuros = PorcJuros / 100;

                        if (item.IDSTATUS != 4)//4 Parcial
                            item.VALORJUROS = item.VALORDUPLICATA * PorcJuros;
                        else
                            item.VALORJUROS = item.VALORDEVEDOR * PorcJuros;

                        if (item.IDSTATUS != 4)//4 Parcial
                            item.VALORDEVEDOR = item.VALORJUROS + JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                            + JUROSDUPLICATASty.OUTRAS + item.VALORDUPLICATA;
                        else
                            item.VALORDEVEDOR = item.VALORJUROS + JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                            + JUROSDUPLICATASty.OUTRAS + item.VALORDEVEDOR;

                        item.VALORMULTA = JUROSDUPLICATASty.MULTAATRASO + JUROSDUPLICATASty.TAXA
                                            + JUROSDUPLICATASty.OUTRAS;
                    }

                    //Salvando no banco
                    DUPLICATARECEBEREntity DUPLICATARECEBER_Sav_Ty = new DUPLICATARECEBEREntity();
                    DUPLICATARECEBER_Sav_Ty = DUPLICATARECEBERP.Read(Convert.ToInt32(item.IDDUPLICATARECEBER));


                    if (DUPLICATARECEBER_Sav_Ty.IDSTATUS != 3 && DUPLICATARECEBER_Sav_Ty.IDSTATUS != 4) //4 Pago Parcial // 3 Pago
                    {
                        if (DUPLICATARECEBER_Sav_Ty.DATAVECTO < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                            DUPLICATARECEBER_Sav_Ty.IDSTATUS = 2; // 2  - Vencida
                        else
                            DUPLICATARECEBER_Sav_Ty.IDSTATUS = 1;//Aberto
                    }


                    DUPLICATARECEBER_Sav_Ty.VALORJUROS = item.VALORJUROS;
                    DUPLICATARECEBER_Sav_Ty.VALORDEVEDOR = item.VALORDEVEDOR;
                    DUPLICATARECEBER_Sav_Ty.VALORMULTA = item.VALORMULTA;
                    DUPLICATARECEBER_Sav_Ty.DIASATRASO = item.DIASATRASO;
                    DUPLICATARECEBER_Sav_Ty.DATAATJUROS = item.DATAATJUROS;
                    DUPLICATARECEBERP.Save(DUPLICATARECEBER_Sav_Ty);
                }
            }
        }

        private void dataGridDuplicatas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                lblObsField.Text = "Duplo click para detalhes de clientes e duplicatas";
            }
        }

        public decimal SumCollRelatorio()
        {
            decimal result = 0;
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                result += Convert.ToDecimal(item.VALORDUPLICATA);
            }

            return result;
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


        private void dataGridDuplicatas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                string orderBy = DataGridRelaDupl.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity>(orderBy);

                LIS_DUPLICATARECEBERColl.Sort(comparer.Comparer);

                DataGridRelaDupl.DataSource = null;
                DataGridRelaDupl.DataSource = LIS_DUPLICATARECEBERColl;
            }
        }



        private void FrmExtratoDuplReceber_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            // this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            btnLimpa.Image = Util.GetAddressImage(16);

            GetDropLocalCobranca();
            GetFuncionario();
            GetCentroCusto();
            GetDropTipoDuplicata();
            GetDropCliente();

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
            {
                MessageBox.Show(ConfigMessage.Default.MsgPerm);
                this.Close();
            }

            if (ExibiDados == true)
            {
                mkDtInicial.Text = DateTime.Now.ToString();
                mkdatafinal.Text = DateTime.Now.ToString();
                rbVencer.Checked = true;
                btnPesquisa_Click(null, null);
            }

            if (DataConsultaSelec != string.Empty)
            {
                mkDtInicial.Text = DataConsultaSelec;
                mkdatafinal.Text = DataConsultaSelec;
                rbVencer.Checked = true;
                PesquisaExtrato();
            }


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

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();
            TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
            TIPODUPLICATAColl = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            TIPODUPLICATAEntity TIPODUPLICATATy = new TIPODUPLICATAEntity();
            TIPODUPLICATATy.NOME = ConfigMessage.Default.MsgDrop;
            TIPODUPLICATATy.IDTIPODUPLICATA = -1;
            TIPODUPLICATAColl.Add(TIPODUPLICATATy);

            Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPODUPLICATAEntity>(cbTipo.DisplayMember);

            TIPODUPLICATAColl.Sort(comparer.Comparer);
            cbTipo.DataSource = TIPODUPLICATAColl;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EstornoDuplicata()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente estornar esta duplicata?",
                       ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    int linha = DataGridRelaDupl.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
                    int CodDuplicata = Convert.ToInt32(LIS_DUPLICATARECEBERColl[linha].IDDUPLICATARECEBER);

                    DUPLICATARECEBEREntity DUPLICATARECEBERBT = new DUPLICATARECEBEREntity();
                    DUPLICATARECEBERBT = DUPLICATARECEBERP.Read(CodDuplicata);

                    if (DUPLICATARECEBERBT.IDSTATUS == 1 || DUPLICATARECEBERBT.IDSTATUS == 2)
                    {
                        MessageBox.Show("Não é possível fazer o estorno da duplicata!");
                    }
                    else
                    {
                        DUPLICATARECEBERBT.DATAPAGTO = null;
                        DUPLICATARECEBERBT.VALORPAGO = null;
                        DUPLICATARECEBERBT.VALORDEVEDOR = Convert.ToDecimal(DUPLICATARECEBERBT.VALORDUPLICATA) + Convert.ToDecimal(DUPLICATARECEBERBT.VALORJUROS) + Convert.ToDecimal(DUPLICATARECEBERBT.VALORMULTA);
                        DUPLICATARECEBERBT.OBSERVACAO += "( Duplicata Estornada dia: " + DateTime.Now + " )";

                        if (DUPLICATARECEBERBT.DATAVECTO > DateTime.Now)
                            DUPLICATARECEBERBT.IDSTATUS = 1;//Aberto
                        else
                            DUPLICATARECEBERBT.IDSTATUS = 2;//Vencida

                        DUPLICATARECEBERP.Save(DUPLICATARECEBERBT);
                        MessageBox.Show("Duplicata Estornada com sucesso!");
                        btnPesquisa_Click(null, null);
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível estornar a duplicata selecionada!");
                }
            }
        }

        private void dataGridDuplicatas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                int ColumnSelecionada = e.ColumnIndex;
                if (rowindex != -1)
                {
                    if (ColumnSelecionada == 0)//Baixa Duplicata
                    {
                        if (LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER != -1 || LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER != null)
                        {
                            using (FrmBaixarTotalReceber FrmBaixar = new FrmBaixarTotalReceber())
                            {
                                FrmBaixar._idDuplicata = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                                FrmBaixar.ShowDialog();
                                btnPesquisa_Click(null, null);//Atualiza a coleção após a baixa
                            }
                        }
                    }
                    if (ColumnSelecionada == 1)//Email
                    {
                        FrmEnviarEmail FrmCl = new FrmEnviarEmail();
                        FrmCl._IDCLIENTE = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDCLIENTE);
                        FrmCl.ShowDialog();
                    }
                    if (ColumnSelecionada == 2)//Boleto
                    {
                        if (LIS_DUPLICATARECEBERColl[rowindex].IDSTATUS != 3) //3 - Pago
                            ImprimirBoletaDireto(Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER));
                        else
                            MessageBox.Show("Duplicata já foi pago, não será possível emitir boleto bancario!");
                    }
                    else if (ColumnSelecionada == 3)//Excluir Duplicata
                    {
                        if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                        {

                            DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_DUPLICATARECEBERColl[rowindex].NUMERO + " - " + LIS_DUPLICATARECEBERColl[rowindex].NOMECLIENTE,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                try
                                {
                                    int CodigoSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                                    //Delete Pedido
                                    DUPLICATARECEBERP.Delete(CodigoSelect);
                                    btnPesquisa_Click(null, null);
                                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Erro técnico: " + ex.Message);
                                    MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                                }
                            }
                        }
                    }
                    else if (ColumnSelecionada == 4)//Produto da Duplicata
                    {
                        using (FrmListaProdutosCompra frm = new FrmListaProdutosCompra())
                        {
                            frm.NFSelec = LIS_DUPLICATARECEBERColl[rowindex].NOTAFISCAL;
                            frm.ShowDialog();
                        }
                    }
                    else if (ColumnSelecionada == 5)//Dados Duplicata
                    {
                        FrmContasReceber FrmContasR = new FrmContasReceber();
                        FrmContasR.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                        FrmContasR.ShowDialog();
                    }
                    else if (ColumnSelecionada == 6)//Dados Cliente
                    {
                        if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                        {
                            FrmCliente FrmCl = new FrmCliente();
                            FrmCl.CodClienteSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDCLIENTE);
                            FrmCl.ShowDialog();
                        }
                        else
                        {
                            FrmCliente2 FrmCl = new FrmCliente2();
                            FrmCl.CodClienteSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDCLIENTE);
                            FrmCl.ShowDialog();
                        }
                    }


                }
            }
        }

        private void ImprimirBoletaDireto(int IDDUPLICATARECEBER)
        {
            //Selecionar a boleta do config
            CONFISISTEMAEntity ConfigSistemaTy = new CONFISISTEMAEntity();
            CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
            ConfigSistemaTy = CONFISISTEMAP.Read(1);
            if (ConfigSistemaTy.IDCONFIGBOLETA == null)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroBoletaSelec,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Busca qual banco sera impresso o boleto
                CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
                CONFIGBOLETAEntity CONFIGBOLETATy = CONFIGBOLETAP.Read(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA));

                PrintBoletoBancario PrintBoletoBancario = new PrintBoletoBancario();

                try
                {
                    switch (CONFIGBOLETATy.IDBANCO)
                    {
                        case 5:
                            //Banco Brasil.
                            PrintBoletoBancario.ImprimiBoletaBrasil(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                    IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                    ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 3:
                            // Banco Basa. 
                            PrintBoletoBancario.ImprimiBoletaBasa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 12:
                            // Banco Santander.                            
                            PrintBoletoBancario.ImprimiBoletaSantander(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 2:
                            //Banco Banrisul
                            PrintBoletoBancario.ImprimiBoletaBanrisul(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 7:
                            //Banco BRB
                            PrintBoletoBancario.ImprimiBoletaBRB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 6:
                            //Banco Caixa
                            PrintBoletoBancario.ImprimiBoletaCaixa(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                     IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                     ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 4:
                            //Bradesco
                            PrintBoletoBancario.ImprimiBoletaBradesco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 9:
                            //Itau
                            PrintBoletoBancario.ImprimiBoletaItau(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 13:
                            //Sudameris();
                            PrintBoletoBancario.ImprimiBoletaSudameris(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 10:
                            //Real
                            PrintBoletoBancario.ImprimiBoletaReal(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                       IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                       ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 8:
                            //HSBC
                            PrintBoletoBancario.ImprimiBoletaHSBC(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 14:
                            //Unibanco
                            PrintBoletoBancario.ImprimiBoletaUnibanco(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 11:
                            //Safra
                            PrintBoletoBancario.ImprimiBoletaSafra(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER, ConfigSistemaTy.FLAGCOMPENTREGABOLETA,
                                                                      ConfigSistemaTy.FLAGCARNEBOLETA);
                            break;
                        case 15:
                            //SICOOB
                            PrintBoletoBancario.ImprimirBoletaSICOOB(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA),
                                                                      IDDUPLICATARECEBER);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            }
            else
            {
                DataGridRelaDupl.CurrentCell = DataGridRelaDupl.Rows[DataGridRelaDupl.Rows.Count - 1].Cells[0];

                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Contas a Receber");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);
            }
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

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            try
            {
                LIS_DUPLICATARECEBERColl.Clear();

                DataGridRelaDupl.AutoGenerateColumns = false;
                DataGridRelaDupl.DataSource = null;

                SumTotalPesquisada();
                mkDtInicial.Text = "  /  /";
                mkdatafinal.Text = "  /  /";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
            }
        }

        private void GetDropLocalCobranca()
        {
            LOCALCOBRANCAProvider LOCALCOBRANCAP = new LOCALCOBRANCAProvider();
            LOCALCOBRANCAColl = LOCALCOBRANCAP.ReadCollectionByParameter(null, "NOME");

            cbLocalCobranca.DisplayMember = "NOME";
            cbLocalCobranca.ValueMember = "IDLOCALCOBRANCA";

            LOCALCOBRANCAEntity LOCALCOBRANCATy = new LOCALCOBRANCAEntity();
            LOCALCOBRANCATy.NOME = ConfigMessage.Default.MsgDrop;
            LOCALCOBRANCATy.IDLOCALCOBRANCA = -1;
            LOCALCOBRANCAColl.Add(LOCALCOBRANCATy);

            Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity>(cbLocalCobranca.DisplayMember);

            LOCALCOBRANCAColl.Sort(comparer.Comparer);
            cbLocalCobranca.DataSource = LOCALCOBRANCAColl;

            cbLocalCobranca.SelectedIndex = 0;
        }

        private void cbVendedor_Click(object sender, EventArgs e)
        {
            using (FrmFuncionario frm = new FrmFuncionario())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFuncionario.SelectedValue);
                GetFuncionario();
                cbFuncionario.SelectedValue = CodSelec;
            }
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

        private void DataGridRelaDupl_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetCentroCusto();
                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void GetCentroCusto()
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

            cbCentroCusto.SelectedIndex = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (LIS_DUPLICATARECEBERColl.Count == 0)
            //{
            //    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            //}
            //else
            //{
            //    using (FrmEnvioSMScs Frm = new FrmEnvioSMScs())
            //    {
            //        Frm.EnvioUnicoSMS = false;
            //        CLIENTECollection CLIENTEColl = new CLIENTECollection();
            //        CLIENTEProvider CLIENTEP = new CLIENTEProvider();

            //        foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            //        {
            //            CLIENTEEntity CLIENTETy = new CLIENTEEntity();
            //            CLIENTETy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));
            //            CLIENTEColl.Add(CLIENTETy);
            //        }

            //        //Remover IDCliente repetido
            //        CLIENTECollection CLIENTE2Coll = new CLIENTECollection();
            //        foreach (CLIENTEEntity item in CLIENTEColl)
            //        {

            //            if (CLIENTE2Coll.Find(delegate(CLIENTEEntity item2) { return (item2.IDCLIENTE == item.IDCLIENTE); }) == null)
            //            {
            //                CLIENTE2Coll.Add(item);
            //            }
            //        }

            //        CLIENTEColl.Clear();
            //        CLIENTEColl = CLIENTE2Coll;

            //        Frm.CLIENTEColl = CLIENTEColl;
            //        Frm.ShowDialog();
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Duplicatas a Receber");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGridRelaDupl, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Duplicatas a Receber";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGridRelaDupl;
                frm.ShowDialog();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sicoobToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void remessaBancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRemessaBanco().ShowDialog();
        }

        private void cNAB400ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cNAB240ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        string MensagemValida = "";
        private Boolean Valida()
        {
            Boolean result = true;
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            MensagemValida = "";
            foreach (var item in LIS_DUPLICATARECEBERColl)
            {
                if (item.IDCLIENTE != null)
                {
                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));
                    
                    if (CLIENTETy.ENDERECO1.Trim() == string.Empty)
                    {
                        MensagemValida += "Erro no cliente: " + CLIENTETy.NOME + "\n";
                        MensagemValida += "Endereço inválido: \n";
                        result = false;                        
                    }
                    if (CLIENTETy.BAIRRO1.Trim() == string.Empty)
                    {
                        MensagemValida += "Erro no cliente: " + CLIENTETy.NOME + "\n";
                        MensagemValida += "Bairro inválido: \n";
                        result = false;
                    }
                    if (CLIENTETy.NUMEROENDER.Trim() == string.Empty)
                    {
                        MensagemValida += "Erro no cliente: " + CLIENTETy.NOME + "\n";
                        MensagemValida += "Número inválido: \n";
                        result = false;
                    }
                    if (Util.RetiraLetras(CLIENTETy.CEP1).Trim() == string.Empty)
                    {
                        MensagemValida += "Erro no cliente: " + CLIENTETy.NOME + "\n";
                        MensagemValida += "CEP inválido: \n";
                        result = false;
                    }
                    if (Util.RetiraLetras(CLIENTETy.CPF).Trim() == string.Empty && Util.RetiraLetras(CLIENTETy.CNPJ).Trim() == string.Empty)
                    {
                        MensagemValida += "Erro no cliente: " + CLIENTETy.NOME + "\n";
                        MensagemValida += "CPF/CNPJ inválido: \n";
                        result = false;                        
                    }
                }
            }

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void txtLocalizarDuplicata_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtLocalizarDuplicata.Text.Trim() != string.Empty)
                PesquisaDuplicata();
        }

        private void PesquisaDuplicata()
        {
            try
            {
                RowRelatorio.Clear();
                string NUMERO = txtLocalizarDuplicata.Text.ToUpper();
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "collate pt_br like", "%" + NUMERO.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + NUMERO.Replace("'", "") + "%", "or"));
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "NUMERO");

                //Percorre a coleção calculando juros de atraso
                SumJuroDuplicata();

                //Colocando somatorio no final da lista
                LIS_DUPLICATARECEBEREntity LIS_DUPLICATARECEBERTy = new LIS_DUPLICATARECEBEREntity();
                LIS_DUPLICATARECEBERTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
                LIS_DUPLICATARECEBERTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_DUPLICATARECEBERTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_DUPLICATARECEBERTy.COMISSAO = SumTotalPesquisa("COMISSAO");
                LIS_DUPLICATARECEBERColl.Add(LIS_DUPLICATARECEBERTy);

                DataGridRelaDupl.AutoGenerateColumns = false;
                DataGridRelaDupl.DataSource = LIS_DUPLICATARECEBERColl;

                SumTotalPesquisada();
                PaintGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa da duplicata!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void produtosPorDuplicataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutosDuplicata frm = new FrmProdutosDuplicata())
            {
                frm.LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERColl;
                frm.ShowDialog();
            }
        }

        private void cNAB240ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count < 2)
            {
                MessageBox.Show("Duplicatas não selecionadas!");
            }
            else if (!Valida())
            {
                MessageBox.Show(MensagemValida);
            }
            else
            {
                this.Text = "Aguarde...";
                Application.DoEvents();

                //Selecionar a boleta do config
                CONFISISTEMAEntity ConfigSistemaTy = new CONFISISTEMAEntity();
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                ConfigSistemaTy = CONFISISTEMAP.Read(1);

                //Busca qual banco sera impresso o boleto
                CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
                CONFIGBOLETAEntity CONFIGBOLETATy = CONFIGBOLETAP.Read(Convert.ToInt32(ConfigSistemaTy.IDCONFIGBOLETA));
                switch (CONFIGBOLETATy.IDBANCO)
                {

                    case 6:
                        //Banco Caixa
                        DialogResult dr = MessageBox.Show("Deseja Gerar Protesto das Duplicatas?",
                                ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                            Util.ArquivoRemessaCaixa_CNAB240(LIS_DUPLICATARECEBERColl, "txt", true);
                        else
                            Util.ArquivoRemessaCaixa_CNAB240(LIS_DUPLICATARECEBERColl, "txt", false);
                        break;
                    case 15:
                        //SICOOB
                        Util.ArquivoRemessaSICOOB_CNAB240(LIS_DUPLICATARECEBERColl, "txt");
                        break;
                }                

                Application.DoEvents();
                this.Text = "Relação de Contas a Receber";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void baixaDuplicataPorArquivoDeRemessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmBaixaArquivoRemessa().ShowDialog();
        }
    }
}
