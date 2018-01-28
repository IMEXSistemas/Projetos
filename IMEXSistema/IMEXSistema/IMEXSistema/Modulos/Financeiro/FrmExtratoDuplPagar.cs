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
using VVX;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmExtratoDuplPagar : Form
    {
        Utility Util = new Utility();

        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodFornecedor = -1;

        public FrmExtratoDuplPagar()
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


        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
           
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

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbFornecedor.SelectedValue = result;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnPesquisa_Click(null, null);
                dataGridDuplicatas.Focus();
            }

           // e.Handled = false;
        }

        private void cbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {

                TotalSelecionado = 0;
                txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                PesquisaDuplicatas();
                SumTotalPesquisada();

                this.Cursor = Cursors.Default;
            }
          
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
                TotalDuplicata += Convert.ToDecimal(item.VALORDUPLICATA);
                TotalDevedor += Convert.ToDecimal(item.VALORDEVEDOR);
                TotalPago += Convert.ToDecimal(item.VALORPAGO);
                TotalMulta += Convert.ToDecimal(item.VALORMULTA);
                TotalDesconto += Convert.ToDecimal(item.VALORDESCONTO);
            }

            lblTotalDuplicata.Text = TotalDuplicata.ToString("n2");
            lblTotalDuplicata.TextAlign = ContentAlignment.MiddleRight;
            lblTotalDevedor.Text = TotalDevedor.ToString("n2");
            lblTotalDevedor.TextAlign = ContentAlignment.MiddleRight;
            lblTotalRecebido.Text = TotalPago.ToString("n2");
            lblTotalRecebido.TextAlign = ContentAlignment.MiddleRight;
        }


        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (cbFornecedor.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFornecedor, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void PesquisaDuplicatas()
        {
            int IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);

            string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
            DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", IDFORNECEDOR.ToString()));

            if (rbVencidas.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "<", DataAtual));
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 
            }
            else if (rbVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", ">=", DataAtual));
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 
            }
            else if (rbPagas.Checked)
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "3"));//3 - Pago              

            LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

            //Percorre a coleção calculando juros de atraso
            SumJuroDuplicata();
          
            dataGridDuplicatas.AutoGenerateColumns = false;
            dataGridDuplicatas.DataSource = LIS_DUPLICATAPAGARColl;
        }

        //Percorre a coleção calculando juros de atraso
        public void SumJuroDuplicata()
        {
            JUROSDUPLICATASEntity JUROSDUPLICATASty = new JUROSDUPLICATASEntity();
            JUROSDUPLICATASProvider JUROSDUPLICATASP = new JUROSDUPLICATASProvider();
            JUROSDUPLICATASty = JUROSDUPLICATASP.Read(1);//1 Contas a Pagar

           
                foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
                {
                    //Somente calcula juros de duplicatas que não foram atualizada no dia
                    // e vencidas
                    //string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    //string DataAtDupl = Convert.ToDateTime(item.DATAATJUROS).ToString("dd/MM/yyyy");
                    //if (item.DATAVECTO < DateTime.Now && Convert.ToDateTime(DataAtDupl) < Convert.ToDateTime(DataAtual)
                    //    && item.IDSTATUS != 3)
                    if (item.DATAVECTO < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) && item.IDSTATUS != 3)
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

        private void dataGridDuplicatas_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmContasPagar FrmContasPagar = new FrmContasPagar())
                    {
                        int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmContasPagar.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATAPAGARColl[linha].IDDUPLICATAPAGAR);
                        FrmContasPagar.ShowDialog();
                    }
                }
                else
                if ((Control.ModifierKeys == Keys.Control) &&
                   (e.KeyCode == Keys.B))//Baixa total
                {
                    button1_Click(null, null);
                }
                if ((Control.ModifierKeys == Keys.Control) &&
                  (e.KeyCode == Keys.P))//Baixa parcial
                {
                    button2_Click(null, null);
                }
                if ((Control.ModifierKeys == Keys.Control) &&
                  (e.KeyCode == Keys.L))//Baixa em lote
                {
                    button3_Click(null, null);
                }
            }
			
        }

        private void dataGridDuplicatas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(LIS_DUPLICATAPAGARColl.Count > 0)
                lblObsField.Text = "Pressione Ctrl+D para maiores detalhes da duplicata, Ctrl+B para baixa total, Ctrl+P para baixa parcial e Ctrl+L para baixa em lote.";
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //'Cabecalho
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 60, config.MargemDireita + 250, 60);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 160, config.MargemDireita + 250, 160);

                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 550, 68);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 68);

                //Titulo
                e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 120);

                e.Graphics.DrawString("Fornecedor: " + LIS_DUPLICATAPAGARColl[0].IDFORNECEDOR + " - " + LIS_DUPLICATAPAGARColl[0].NOMEFORNECEDOR, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Duplicata", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
                e.Graphics.DrawString("Vecto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 170, 170);
                e.Graphics.DrawString("Pagto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 240, 170);
                e.Graphics.DrawString("Valor Dupl.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 310, 170);
                e.Graphics.DrawString("Valor Desc.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
                e.Graphics.DrawString("Valor Multa", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 490, 170);
                e.Graphics.DrawString("Valor Pago", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, 170);
                e.Graphics.DrawString("Valor Devedor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 670, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 760, 170);
                e.Graphics.DrawString("Dias Atraso", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 870, 170);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_DUPLICATAPAGARColl.Count;

                StringFormat formataString = new StringFormat();
                formataString.Alignment = StringAlignment.Near;

                //formataString.LineAlignment = StringAlignment.Near;

                while (IndexRegistro < LIS_DUPLICATAPAGARColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].NUMERO.ToString(),20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATAPAGARColl[IndexRegistro].DATAEMISSAO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATAPAGARColl[IndexRegistro].DATAVECTO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 170, config.PosicaoDaLinha);

                    string DataPagto = string.Empty;
                    if (LIS_DUPLICATAPAGARColl[IndexRegistro].DATAPAGTO != null)
                        DataPagto = Convert.ToDateTime(LIS_DUPLICATAPAGARColl[IndexRegistro].DATAPAGTO).ToString("dd/MM/yyyy");
                    e.Graphics.DrawString(DataPagto, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 240, config.PosicaoDaLinha);

                    string valorDuplicata = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORDUPLICATA).ToString("n2");
                    //Alinha a direita
                    valorDuplicata = String.Format("{0,10}", valorDuplicata);  
                    e.Graphics.DrawString(valorDuplicata, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 310, config.PosicaoDaLinha, formataString);

                    string valorDesconto = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORDESCONTO).ToString("n2");
                    //Alinha a direita
                    valorDesconto = String.Format("{0,10}", valorDesconto);
                    e.Graphics.DrawString(valorDesconto, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha, formataString);

                    string valorMulta = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORMULTA).ToString("n2");
                    //Alinha a direita
                    valorMulta = String.Format("{0,10}", valorMulta);
                    e.Graphics.DrawString(valorMulta, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 490, config.PosicaoDaLinha, formataString);

                    string valorPago = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORPAGO).ToString("n2");
                    //Alinha a direita
                    valorPago = String.Format("{0,10}", valorPago);
                    e.Graphics.DrawString(valorPago, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 580, config.PosicaoDaLinha, formataString);

                    string valorDevedor = Convert.ToDecimal(LIS_DUPLICATAPAGARColl[IndexRegistro].VALORDEVEDOR).ToString("n2");
                    //Alinha a direita
                    valorDevedor = String.Format("{0,10}", valorDevedor);
                    e.Graphics.DrawString(valorDevedor, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 670, config.PosicaoDaLinha, formataString);

                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 760, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATAPAGARColl[IndexRegistro].DIASATRASO.ToString(), 5), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 870, config.PosicaoDaLinha);


                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DUPLICATAPAGARColl.Count)
                    e.HasMorePages = true;
                else
                {

                    string valorDuplicata = String.Format("{0,10}", TotalDuplicata.ToString("n2"));
                    string VALORDESCONTO = String.Format("{0,10}", TotalDesconto.ToString("n2"));
                    string VALORMULTA = String.Format("{0,10}", TotalMulta.ToString("n2"));
                    string VALORPAGO = String.Format("{0,10}", TotalPago.ToString("n2"));
                    string VALORDEVEDOR = String.Format("{0,10}", TotalDevedor.ToString("n2"));


                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.PosicaoDaLinha + 40, config.MargemDireita + 250, config.PosicaoDaLinha + 40);

                    e.Graphics.DrawString("TOTAL: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 240, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(valorDuplicata, config.FonteNegrito, Brushes.Black, config.MargemEsquerda +310, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORDESCONTO, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORMULTA, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 490, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORPAGO, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORDEVEDOR, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 670, config.PosicaoDaLinha + 50);
                    

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DUPLICATAPAGARColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);

                    //Rodape                  
                    config.MargemInferior = 757;

                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita +250, config.MargemInferior);
                    e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                    config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                    config.LinhaAtual++;
                    e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Extrato de Duplicatas por Fornecdor");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
                printDocument1.DefaultPageSettings.Landscape = true;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
                ImprimirListaGeral();
            else
            {
                MessageBox.Show("Faça a pesquisa antes de imprimir!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            LIS_DUPLICATAPAGARColl.Clear();

            dataGridDuplicatas.AutoGenerateColumns = false;
            dataGridDuplicatas.DataSource = null;
        }

        private void dataGridDuplicatas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                string orderBy = dataGridDuplicatas.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATAPAGAREntity>(orderBy);

                    LIS_DUPLICATAPAGARColl.Sort(comparer.Comparer);

                    dataGridDuplicatas.DataSource = null;
                    dataGridDuplicatas.DataSource = LIS_DUPLICATAPAGARColl;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                RowsFiltroCollection Filtro = new RowsFiltroCollection();
                using (FrmBaixarTotalPagar FrmBaixar = new FrmBaixarTotalPagar())
                {

                    int i = 0;
                    foreach (DataGridViewRow dr in dataGridDuplicatas.Rows)
                    {
                        if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value) != false)
                        {
                            if (Convert.ToInt32(LIS_DUPLICATAPAGARColl[i].IDSTATUS) != 3)//3 pago
                            {
                                filtroProfile = new RowsFiltro("IDDUPLICATAPAGAR", "System.Int32", "=", LIS_DUPLICATAPAGARColl[i].IDDUPLICATAPAGAR.ToString(), "or");
                                Filtro.Insert(Filtro.Count, filtroProfile);
                            }
                        }

                        i++;
                    }

                    if (Filtro.Count > 0)
                    {
                        FrmBaixar.LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(Filtro);
                        FrmBaixar.ShowDialog();
                        btnPesquisa_Click(null, null);//Atualiza a coleção após a baixa
                    }
                    else
                        MessageBox.Show("Duplicata não selecionada!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Faça a pesquisa antes de fazer a baixa!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else 
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {

                using (FrmBaixaParcialPagar FrmBaixaParcial = new FrmBaixaParcialPagar())
                {
                    int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
                    FrmBaixaParcial._idDuplicata = Convert.ToInt32(LIS_DUPLICATAPAGARColl[linha].IDDUPLICATAPAGAR);
                    FrmBaixaParcial.ShowDialog();
                    btnPesquisa_Click(null, null);//Atualiza a coleção após a baixa
                }

            }
            else
            {
                MessageBox.Show("Faça a pesquisa antes de fazer a baixa!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else 
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                using (FrmBaixaLotePagar FrmBaixaLote = new FrmBaixaLotePagar())
                {
                    int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
                    FrmBaixaLote._idFornecedor = Convert.ToInt32(LIS_DUPLICATAPAGARColl[linha].IDFORNECEDOR);
                    FrmBaixaLote.ShowDialog();
                    btnPesquisa_Click(null, null);//Atualiza a coleção após a baixa
                }

            }
            else
            {
                MessageBox.Show("Faça a pesquisa antes de fazer a baixa!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void FrmExtratoDuplReceber_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropFornecedor();
            cbFornecedor.Focus();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            btnLimpaPesquisa.Image = Util.GetAddressImage(16);

            //Exibir dados do cliente consultado em outra tela
            if (CodFornecedor != -1)
            {
                rbTodas.Checked = true;
                cbFornecedor.SelectedValue = CodFornecedor;
                btnPesquisa_Click(null, null);
            }
            

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();

            this.Cursor = Cursors.Default;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else 
            if (LIS_DUPLICATAPAGARColl.Count > 0)
            {
                EstornoDuplicata();
            }
            else
            {
                MessageBox.Show("Faça a pesquisa antes de fazer o estorno!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void EstornoDuplicata()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente estornar esta duplicata?",
                       ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
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

                        ExcluiContaCorrente(DUPLICATAPAGARBT.NUMERO);

                        DUPLICATAPAGARP.Save(DUPLICATAPAGARBT);
                        
                        MessageBox.Show("Duplicata Estornada com sucesso!");
                        btnPesquisa_Click(null, null);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível estornar a duplicata selecionada!");
                    MessageBox.Show("Erro técnico: " + ex.Message);
                }
            }
        }

        private void ExcluiContaCorrente(string NUMMOVIMENTACAO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMMOVIMENTACAO", "System.String", "=", NUMMOVIMENTACAO.ToString()));

                MOVCONTACORRENTECollection MOVCONTACORRENTE2Coll = new MOVCONTACORRENTECollection();
                MOVCONTACORRENTEProvider MOVCONTACORRENTEP = new MOVCONTACORRENTEProvider();
                MOVCONTACORRENTE2Coll = MOVCONTACORRENTEP.ReadCollectionByParameter(RowRelatorio, "DATAMOVIMENTACAO desc");
                
                foreach (MOVCONTACORRENTEEntity item in MOVCONTACORRENTE2Coll)
                {
                    MOVCONTACORRENTEP.Delete(item.IDMOVCTCORRENTE);
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridDuplicatas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 7 && e.Value.Equals("ABERTO"))
            {
                dataGridDuplicatas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }
            
            if (e.Value != null && e.ColumnIndex == 7 && e.Value.Equals("VENCIDA"))
            {
                dataGridDuplicatas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
           
            if (e.Value != null && e.ColumnIndex == 7 && e.Value.Equals("PAGO"))
            {
                dataGridDuplicatas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }

             if (e.Value != null && e.ColumnIndex == 7 && e.Value.Equals("PAGO PARCIAL"))
            {
                dataGridDuplicatas.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
            }

        }

        decimal TotalSelecionado = 0;
        private void dataGridDuplicatas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridDuplicatas.Rows[dataGridDuplicatas.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    {
                        ch1.Value = false;
                        TotalSelecionado -= Convert.ToDecimal(LIS_DUPLICATAPAGARColl[e.RowIndex].VALORDEVEDOR);
                        txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");
                    }
                    break;
                case "False":
                    {
                        ch1.Value = true;
                        TotalSelecionado += Convert.ToDecimal(LIS_DUPLICATAPAGARColl[e.RowIndex].VALORDEVEDOR);
                        txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");
                    }
                    break;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Extrato de Contas a Pagar + " + cbFornecedor.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dataGridDuplicatas, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(dataGridDuplicatas, "csv");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Extrato de Contas a Pagar + " + cbFornecedor.Text;
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = dataGridDuplicatas;
                frm.ShowDialog();
            }
        }

        
    }
}
