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
using System.Diagnostics;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmRelacaoDuplicatasPagar : Form
    {
        Utility Util = new Utility();

        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();
        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodClienteSelec = -1;
        public Boolean ExibiDados = false;
        public string DataConsultaSelec = string.Empty;    
        public FrmRelacaoDuplicatasPagar()
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
            PaintGrid();
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
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

        private void PesquisaExtrato()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            PesquisaDuplicatas();
            SumTotalPesquisada();

            this.Cursor = Cursors.Default;
        }

        decimal TotalDuplicata = 0;
        decimal TotalDevedor = 0;
        decimal TotalPago = 0;
        decimal TotalMulta = 0;
        decimal TotalDesconto = 0;
        public void SumTotalPesquisada()
        {
            TotalDuplicata = 0;
            TotalDevedor = 0;
            TotalPago = 0;
            TotalDesconto = 0;
           
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                if (item.IDFORNECEDOR != null)
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

        private void PesquisaDuplicatas()
        {

            string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
            DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.

            
            RowRelatorio.Clear();

            if (rbVencidas.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<", DataAtual));               
            }
            else if (rbVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", DataAtual));
            }
            else if (rbVencidasVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
            }
            else if (rbPagas.Checked)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago 

            if (Convert.ToInt32(cbCentroCusto.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDCENTROCUSTO", "System.Int32", "=", cbCentroCusto.SelectedValue.ToString()));

            if (Convert.ToInt32(cbTipo.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDTIPODUPLICATA", "System.Int32", "=", cbTipo.SelectedValue.ToString()));

            if (Convert.ToInt32(cbFornecedor.SelectedValue) > 0)
                RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", cbFornecedor.SelectedValue.ToString()));


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

            if (rdDataEmissao.Checked)
            {
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAEMISSAO");
            }
            else if (rdDataVencto.Checked)
            {
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
            }
            else 
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAPAGTO");

            //Percorre a coleção calculando juros de atraso
            SumJuroDuplicata();

            //Colocando somatorio no final da lista
            LIS_DUPLICATAPAGAREntity LIS_DUPLICATAPAGARTy = new LIS_DUPLICATAPAGAREntity();
            LIS_DUPLICATAPAGARTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
            LIS_DUPLICATAPAGARTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
            LIS_DUPLICATAPAGARTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
            LIS_DUPLICATAPAGARColl.Add(LIS_DUPLICATAPAGARTy);

          
            DataGridRelaDupl.AutoGenerateColumns = false;
            DataGridRelaDupl.DataSource = LIS_DUPLICATAPAGARColl;
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
            {
                if (NomeCampo == "VALORDUPLICATA")
                    valortotal += Convert.ToDecimal(item.VALORDUPLICATA);
                else if (NomeCampo == "VALORPAGO")
                    valortotal += Convert.ToDecimal(item.VALORPAGO);
                else if (NomeCampo == "VALORDEVEDOR")
                    valortotal += Convert.ToDecimal(item.VALORDEVEDOR);             
            }

            return valortotal;
        }

        //Percorre a coleção calculando juros de atraso
        public void SumJuroDuplicata()
        {
            JUROSDUPLICATASEntity JUROSDUPLICATASty = new JUROSDUPLICATASEntity();
            JUROSDUPLICATASProvider JUROSDUPLICATASP = new JUROSDUPLICATASProvider();
            JUROSDUPLICATASty = JUROSDUPLICATASP.Read(2);//2 Contas a Receber

           
                foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
                {
                    //Somente calcula juros de duplicatas que não foram atualizada no dia
                    // e vencidas
                    string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    string DataAtDupl = Convert.ToDateTime(item.DATAATJUROS).ToString("dd/MM/yyyy");
                    if (item.DATAVECTO < DateTime.Now && Convert.ToDateTime(DataAtDupl) < Convert.ToDateTime(DataAtual)
                        && item.IDSTATUS != 3)
                    {
                        //Calculo de dias de vencimento
                        TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(item.DATAVECTO);
                        int DIASATRASO = date.Days;

                        if (DIASATRASO > 0)
                            item.DIASATRASO = DIASATRASO;
                        else
                            item.DIASATRASO = 0;

                        item.DATAATJUROS = DateTime.Now;

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
                        DUPLICATAPAGAREntity DUPLICATAPAGAR_Sav_Ty = new DUPLICATAPAGAREntity();
                        DUPLICATAPAGAR_Sav_Ty = DUPLICATAPAGARP.Read(Convert.ToInt32(item.IDDUPLICATAPAGAR));

                        if (DIASATRASO > 0 && DUPLICATAPAGAR_Sav_Ty.IDSTATUS != 4) // 4 Parcial
                            DUPLICATAPAGAR_Sav_Ty.IDSTATUS = 2; //Vencida

                        DUPLICATAPAGAR_Sav_Ty.VALORJUROS = item.VALORJUROS;
                        DUPLICATAPAGAR_Sav_Ty.VALORDEVEDOR = item.VALORDEVEDOR;
                        DUPLICATAPAGAR_Sav_Ty.VALORMULTA = item.VALORMULTA;
                        DUPLICATAPAGAR_Sav_Ty.DIASATRASO = item.DIASATRASO;
                        DUPLICATAPAGAR_Sav_Ty.DATAATJUROS = item.DATAATJUROS;
                        DUPLICATAPAGARP.Save(DUPLICATAPAGAR_Sav_Ty);
                    }
                }
        }      

        private void dataGridDuplicatas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                lblObsField.Text = "Duplo click para detalhes de clientes e duplicatas";
            }
        }
       
        public decimal SumCollRelatorio()
        {
            decimal result = 0;
            foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
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
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                string orderBy = DataGridRelaDupl.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity>(orderBy);

                LIS_DUPLICATAPAGARColl.Sort(comparer.Comparer);

                DataGridRelaDupl.DataSource = null;
                DataGridRelaDupl.DataSource = LIS_DUPLICATAPAGARColl;
            }
        }      

     

        private void FrmExtratoDuplReceber_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
          //  this.FormBorderStyle = FormBorderStyle.FixedDialog;

            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecFinal.Image = Util.GetAddressImage(11);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            btnLimpaPesquisa.Image = Util.GetAddressImage(16);

            GetCentroCusto();
            GetDropTipoDuplicata();
            GetDropFornecedor();
           
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

        private void GetDropFornecedor()
        {
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
            FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbFornecedor.DisplayMember = "NOME";
            cbFornecedor.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FORNECEDORColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedor.DisplayMember);

            FORNECEDORColl.Sort(comparer.Comparer);
            cbFornecedor.DataSource = FORNECEDORColl;

            cbFornecedor.SelectedIndex = 0;
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
                    int CodDuplicata = Convert.ToInt32(LIS_DUPLICATAPAGARColl[linha].IDDUPLICATAPAGAR);

                    DUPLICATAPAGAREntity DUPLICATAPAGARBT = new DUPLICATAPAGAREntity();
                    DUPLICATAPAGARBT = DUPLICATAPAGARP.Read(CodDuplicata);

                    if (DUPLICATAPAGARBT.IDSTATUS == 1 || DUPLICATAPAGARBT.IDSTATUS == 2)
                    {
                        MessageBox.Show("Não é possível fazer o estorno da duplicata!");
                    }
                    else
                    {
                        DUPLICATAPAGARBT.DATAPAGTO = null;
                        DUPLICATAPAGARBT.VALORPAGO = null;
                        DUPLICATAPAGARBT.VALORDEVEDOR = Convert.ToDecimal(DUPLICATAPAGARBT.VALORDUPLICATA) + Convert.ToDecimal(DUPLICATAPAGARBT.VALORJUROS) + Convert.ToDecimal(DUPLICATAPAGARBT.VALORMULTA);
                        DUPLICATAPAGARBT.OBSERVACAO += "( Duplicata Estornada dia: " + DateTime.Now + " )";

                        if (DUPLICATAPAGARBT.DATAVECTO > DateTime.Now)
                            DUPLICATAPAGARBT.IDSTATUS = 1;//Aberto
                        else
                            DUPLICATAPAGARBT.IDSTATUS = 2;//Vencida

                        DUPLICATAPAGARP.Save(DUPLICATAPAGARBT);
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
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                int rowindex = e.RowIndex;
                int ColumnSelecionada = e.ColumnIndex;
                if (rowindex != -1)
                {
                    if (ColumnSelecionada == 0)//Baixa da Duplicata
                    {

                        if (LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR != -1 || LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR != null)
                        {
                            using (FrmBaixarTotalPagar FrmBaixar = new FrmBaixarTotalPagar())
                            {
                                FrmBaixar._idDuplicata = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                                FrmBaixar.ShowDialog();
                                btnPesquisa_Click(null, null);//Atualiza a coleção após a baixa
                            }
                        }
                    }
                    else
                        if (ColumnSelecionada == 1)//Excluir
                        {
                            if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_DUPLICATAPAGARColl[rowindex].NUMERO + " - " + LIS_DUPLICATAPAGARColl[rowindex].NOMEFORNECEDOR,
                                   ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                                if (dr == DialogResult.Yes)
                                {
                                    try
                                    {
                                        int CodigoSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                                        //Delete Pedido
                                        DUPLICATAPAGARP.Delete(CodigoSelect);
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
                    else
                    if (ColumnSelecionada == 2)//Dados Duplicata
                    {
                        FrmContasPagar FrmContasP = new FrmContasPagar();
                        FrmContasP.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                        FrmContasP.ShowDialog();
                    }
                    else
                    if (ColumnSelecionada == 3)//Dados Fornecedor
                    {
                        FrmFornecedor FrmForn = new FrmFornecedor();
                        FrmForn._IDFORNECEDOR = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDFORNECEDOR);
                        FrmForn.ShowDialog();
                    }
                   
                }
            }       
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Contas a Pagar");

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
            LIS_DUPLICATAPAGARColl.Clear();
            DataGridRelaDupl.AutoGenerateColumns = false;
            DataGridRelaDupl.DataSource = null;

            SumTotalPesquisada();
            mkDtInicial.Text = "  /  /";
            mkdatafinal.Text = "  /  /";
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

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
                GetDropTipoDuplicata();
            }
        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();
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

            cbTipo.SelectedIndex = 0;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Duplicatas a Pagar");

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
                frm.TituloSelec = "Relação de Duplicatas a Pagar";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGridRelaDupl;
                frm.ShowDialog();
            }
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
                RowRelatorio.Add(new RowsFiltro("NOMEFORNECEDOR", "System.String", "collate pt_br like", "%" + NUMERO.Replace("'", "") + "%", "or"));
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "NUMERO");
                
                //Percorre a coleção calculando juros de atraso
                SumJuroDuplicata();

                //Colocando somatorio no final da lista
                LIS_DUPLICATAPAGAREntity LIS_DUPLICATAPAGARTy = new LIS_DUPLICATAPAGAREntity();
                LIS_DUPLICATAPAGARTy.VALORDUPLICATA = SumTotalPesquisa("VALORDUPLICATA");
                LIS_DUPLICATAPAGARTy.VALORPAGO = SumTotalPesquisa("VALORPAGO");
                LIS_DUPLICATAPAGARTy.VALORDEVEDOR = SumTotalPesquisa("VALORDEVEDOR");
                LIS_DUPLICATAPAGARColl.Add(LIS_DUPLICATAPAGARTy);

                DataGridRelaDupl.AutoGenerateColumns = false;
                DataGridRelaDupl.DataSource = LIS_DUPLICATAPAGARColl;

                SumTotalPesquisada();
                PaintGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa da duplicata!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

    }
}
