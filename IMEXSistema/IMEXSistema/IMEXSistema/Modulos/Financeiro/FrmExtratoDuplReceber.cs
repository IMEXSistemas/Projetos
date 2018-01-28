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
using VVX;
using winfit.Modulos.Outros;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmExtratoDuplReceber : Form
    {
        Utility Util = new Utility();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodClienteSelec = -1;
        public FrmExtratoDuplReceber()
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
            lblObsField.Text = "Selecione o cliente ou pressione Ctrl+E para pesquisar.";
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
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbCliente.SelectedValue = result;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnPesquisa_Click(null, null);
                dataGridDuplicatas.Focus();
            }

          //  e.Handled = false;
        }

        private void cbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Pesquisa();          
        }

        private void Pesquisa()
        {
            if (Validacoes())
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                TotalSelecionado = 0;
                txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");

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
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
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
            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void PesquisaDuplicatas()
        {
            int IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);

            string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
            DataAtual = Util.ConverStringDateSearch(DataAtual);//formata data para pesquisa.

            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));

            if (rbVencidaVencer.Checked)
            {
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago  
            }
            else if (rbVencidas.Checked)
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

            LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

            //Percorre a coleção calculando juros de atraso
            SumJuroDuplicata();
          
            dataGridDuplicatas.AutoGenerateColumns = false;
            dataGridDuplicatas.DataSource = LIS_DUPLICATARECEBERColl;
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
                    string DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    string DataAtDupl = Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy");
                    //if (item.DATAVECTO < DateTime.Now && Convert.ToDateTime(DataAtDupl) < Convert.ToDateTime(DataAtual)
                    //    && item.IDSTATUS != 3)

                    int DIASATRASO = 0;
                    if (item.IDSTATUS != 3)
                    {
                        //Calculo de dias de vencimento
                        TimeSpan date = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(item.DATAVECTO);
                        DIASATRASO = date.Days;
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
                        DUPLICATARECEBEREntity DUPLICATARECEBER_Sav_Ty = new DUPLICATARECEBEREntity();
                        DUPLICATARECEBER_Sav_Ty = DUPLICATARECEBERP.Read(Convert.ToInt32(item.IDDUPLICATARECEBER));

                        if (DUPLICATARECEBER_Sav_Ty.IDSTATUS != 3 && DUPLICATARECEBER_Sav_Ty.IDSTATUS != 4)//Pago //4 Pago Parcial 
                            if (DUPLICATARECEBER_Sav_Ty.DATAVECTO < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                                DUPLICATARECEBER_Sav_Ty.IDSTATUS = 2; // 2  - Vencida
                            else
                                DUPLICATARECEBER_Sav_Ty.IDSTATUS = 1;//Aberto

                        DUPLICATARECEBER_Sav_Ty.VALORJUROS = item.VALORJUROS;
                        DUPLICATARECEBER_Sav_Ty.VALORDEVEDOR = item.VALORDEVEDOR;
                        DUPLICATARECEBER_Sav_Ty.VALORMULTA = item.VALORMULTA;
                        DUPLICATARECEBER_Sav_Ty.DIASATRASO = item.DIASATRASO;
                        DUPLICATARECEBER_Sav_Ty.DATAATJUROS = item.DATAATJUROS;
                        DUPLICATARECEBERP.Save(DUPLICATARECEBER_Sav_Ty);
                    }
                }
        }

        private void dataGridDuplicatas_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                if ((Control.ModifierKeys == Keys.Control) &&
                (e.KeyCode == Keys.D))
                {
                    using (FrmContasReceber FrmContasReceber = new FrmContasReceber())
                    {
                        int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        FrmContasReceber.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[linha].IDDUPLICATARECEBER);
                        FrmContasReceber.ShowDialog();
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
                if ((Control.ModifierKeys == Keys.Control) &&
                 (e.KeyCode == Keys.T))//Baixa em lote
                {
                    using (FrmListaProdutosCompra frm = new FrmListaProdutosCompra())
                    {
                        int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA 
                        frm.NFSelec =LIS_DUPLICATARECEBERColl[linha].NOTAFISCAL;
                        frm.ShowDialog();
                    }
                }
            }
			
        }

        private void dataGridDuplicatas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                lblObsField.Text = "Pressione Ctrl+D para maiores detalhes da duplicata, Ctrl+B para baixa total, Ctrl+P para baixa parcial e Ctrl+L para baixa em lote";
                lblObsField2.Text = "Pressione Ctrl+T para listar os produtos da compra.";
            }
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
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 160, config.MargemDireita +250, 160);

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

                e.Graphics.DrawString("Cliente: " +LIS_DUPLICATARECEBERColl[0].IDCLIENTE + " - "+ LIS_DUPLICATARECEBERColl[0].NOMECLIENTE , config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Duplicata", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Emissão", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 100, 170);
                e.Graphics.DrawString("Vecto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 170, 170);
                e.Graphics.DrawString("Pagto", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 240, 170);
                e.Graphics.DrawString("Valor Dupl.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 320, 170);
                e.Graphics.DrawString("Valor Desc.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 400, 170);
                e.Graphics.DrawString("Valor Multa", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 490, 170);
                e.Graphics.DrawString("Valor Pago", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, 170);
                e.Graphics.DrawString("Valor Devedor", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 670, 170);
                e.Graphics.DrawString("Status", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 760, 170);
                e.Graphics.DrawString("Dias Atraso", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 870, 170);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_DUPLICATARECEBERColl.Count;

                //Alinhamento dos valores
                StringFormat formataString = new StringFormat();
                formataString.Alignment = StringAlignment.Far;
                formataString.LineAlignment = StringAlignment.Far;

                while (IndexRegistro < LIS_DUPLICATARECEBERColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].NUMERO.ToString(),20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATARECEBERColl[IndexRegistro].DATAEMISSAO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 100, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(Convert.ToDateTime(LIS_DUPLICATARECEBERColl[IndexRegistro].DATAVECTO).ToString("dd/MM/yyyy"), 10), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 170, config.PosicaoDaLinha);

                    string DataPagto = string.Empty;
                    if (LIS_DUPLICATARECEBERColl[IndexRegistro].DATAPAGTO != null)
                        DataPagto = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[IndexRegistro].DATAPAGTO).ToString("dd/MM/yyyy");
                    e.Graphics.DrawString(DataPagto, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 240, config.PosicaoDaLinha);

                    string valorDuplicata = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORDUPLICATA).ToString("n2");
                    e.Graphics.DrawString(valorDuplicata, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 380, config.PosicaoDaLinha + 15, formataString);

                    string valorDesconto = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORDESCONTO).ToString("n2");
                     e.Graphics.DrawString(valorDesconto, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 470, config.PosicaoDaLinha + 15, formataString);

                    string valorMulta = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORMULTA).ToString("n2");
                    e.Graphics.DrawString(valorMulta, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 560, config.PosicaoDaLinha + 15, formataString);

                    string valorPago = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORPAGO).ToString("n2");
        
                    e.Graphics.DrawString(valorPago, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 650, config.PosicaoDaLinha + 15, formataString);

                    string valorDevedor = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[IndexRegistro].VALORDEVEDOR).ToString("n2");
                    e.Graphics.DrawString(valorDevedor, config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 750, config.PosicaoDaLinha + 15, formataString);

                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].NOMESTATUS, 20), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 760, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_DUPLICATARECEBERColl[IndexRegistro].DIASATRASO.ToString(), 5), config.FonteConteudo, Brushes.Black, config.MargemEsquerda +930, config.PosicaoDaLinha + 15, formataString);


                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_DUPLICATARECEBERColl.Count)
                    e.HasMorePages = true;
                else
                {

                  

                    //Total do Relatorio
                    string valorDuplicata = String.Format("{0,10}", TotalDuplicata.ToString("n2"));
                    string VALORDESCONTO = String.Format("{0,10}", TotalDesconto.ToString("n2"));
                    string VALORMULTA = String.Format("{0,10}", TotalMulta.ToString("n2"));
                    string VALORPAGO = String.Format("{0,10}", TotalPago.ToString("n2"));
                    string VALORDEVEDOR = String.Format("{0,10}", TotalDevedor.ToString("n2"));


                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.PosicaoDaLinha + 40, config.MargemDireita + 250, config.PosicaoDaLinha + 40);

                    e.Graphics.DrawString("TOTAL: ", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 240, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(valorDuplicata, config.FonteNegrito, Brushes.Black, config.MargemEsquerda +310, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORDESCONTO, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 410, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORMULTA, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 510, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORPAGO, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 580, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString(VALORDEVEDOR, config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 690, config.PosicaoDaLinha + 50);
                    

                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_DUPLICATARECEBERColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 70);


                    //Rodape                  
                    config.MargemInferior = 757;

                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita + 250, config.MargemInferior);
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
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Extrato de Duplicatas por Cliente");
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
            ImprimirExtrato();
        }      

        private void ImprimirMatricial(string local)
        {
            ImprimeTexto imp = new ImprimeTexto();

            string PathRegistro = ConfigSistema1.Default.PathInstall + @"\CapturaPorta.bat";
            FileInfo t = new FileInfo(PathRegistro);
            StreamWriter Tex = t.CreateText();

            //Porta da impressora
            if (local == "local")
            {
                //Limpa alguma configuração anterior
                Tex.WriteLine("NET USE LPT1: /DELETE");
                Tex.Close();

                //Executa o bat para captura a porta da impressora na rede
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro);
                Processo1.WaitForExit(); //aguarda o termino do processo.

                imp.Inicio(BmsSoftware.ConfigSistema1.Default.PORTAMATLOCAL);
            }
            else if (local == "rede")
            {
                Tex.WriteLine("NET USE LPT1: /DELETE");
                Tex.WriteLine("NET USE LPT1: " + BmsSoftware.ConfigSistema1.Default.CAMINHOMATREDE + "  persistent:yes");
                Tex.Close();

                //Executa o bat para captura a porta da impressora na rede
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro);
                Processo1.WaitForExit(); //aguarda o termino do processo.

                imp.Inicio("LPT1");
            }

            //Cabeçalho
            EMPRESAEntity EmpresaTy = new EMPRESAEntity();
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EmpresaTy = EMPRESAP.Read(1);
            
            imp.ImpLF(imp.Comprimido); 
            imp.ImpLF(Util.RemoverAcentos(EmpresaTy.NOMECLIENTE));
            imp.ImpLF(Util.RemoverAcentos(EmpresaTy.ENDERECO) + " - " + EmpresaTy.BAIRRO);
            imp.ImpLF( EmpresaTy.CIDADE + " - " + EmpresaTy.UF);
            imp.ImpLF("Telefone: " + EmpresaTy.TELEFONE);
            imp.ImpLF("CNPJ: " + EmpresaTy.CNPJCPF + "  " + EmpresaTy.IE);
            imp.ImpLF("Data: " + DateTime.Now.ToString("dd/MM/yyyy HH:MM"));

            imp.ImpLF("-------------------------------------------------");
            imp.ImpLF("Extrato de Duplicatas");
            imp.ImpLF("-------------------------------------------------");
            imp.ImpLF(Util.RemoverAcentos("Cliente: " + LIS_DUPLICATARECEBERColl[0].IDCLIENTE +" "+ LIS_DUPLICATARECEBERColl[0].NOMECLIENTE));
            imp.ImpLF("-------------------------------------------------");
            imp.ImpCol(0, "Duplicata");
            imp.ImpCol(12, "Emissao");
            imp.ImpCol(24, "Vecto");
            imp.ImpCol(36, "Valor Devedor"); 
            imp.Pula(1);
            imp.ImpLF("-------------------------------------------------");            

            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                imp.ImpCol(0, item.NUMERO);
                imp.ImpCol(12, Convert.ToDateTime(item.DATAEMISSAO).ToString("dd/MM/yyyy"));
                imp.ImpCol(24, Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"));
                
                //Alinhar a direita
                string ValorDevedor = String.Format("{0,12}", Convert.ToDecimal(item.VALORDEVEDOR).ToString("n2"));
                imp.ImpCol(36, ValorDevedor);
                imp.Pula(1);
            }

            imp.ImpLF("-------------------------------------------------");
            imp.ImpCol(0,"Total Devedor: ");
            imp.ImpCol(36, String.Format("{0,12}",  lblTotalDevedor.Text));
            imp.Pula(1);
            imp.ImpLF(imp.Comprimido);
           
            imp.Pula(2);
            imp.Fim();
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            LIS_DUPLICATARECEBERColl.Clear();

            dataGridDuplicatas.AutoGenerateColumns = false;
            dataGridDuplicatas.DataSource = null;
        }

        private void dataGridDuplicatas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                string orderBy = dataGridDuplicatas.Columns[e.ColumnIndex].DataPropertyName;
                Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_DUPLICATARECEBEREntity>(orderBy);

                LIS_DUPLICATARECEBERColl.Sort(comparer.Comparer);

                dataGridDuplicatas.DataSource = null;
                dataGridDuplicatas.DataSource = LIS_DUPLICATARECEBERColl;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {
            
            }
            else if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                RowsFiltroCollection Filtro = new RowsFiltroCollection();       
                using (FrmBaixarTotalReceber FrmBaixar = new FrmBaixarTotalReceber())
                {
                   
                    int i = 0;
                    foreach (DataGridViewRow dr in dataGridDuplicatas.Rows)
                    {
                        if (dr.Cells[0].Value != null && Convert.ToBoolean(dr.Cells[0].Value) != false)
                        {
                                if (Convert.ToInt32(LIS_DUPLICATARECEBERColl[i].IDSTATUS) != 3)//3 pago
                                {
                                    filtroProfile = new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", LIS_DUPLICATARECEBERColl[i].IDDUPLICATARECEBER.ToString(), "or");
                                    Filtro.Insert(Filtro.Count, filtroProfile); 
                                }
                        }

                        i++;
                    }

                    if (Filtro.Count > 0)
                    {
                        FrmBaixar.LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(Filtro);
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
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
             
                using (FrmBaixaParcialReceber FrmBaixaParcial = new FrmBaixaParcialReceber())
                {
                    int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
                    FrmBaixaParcial._idDuplicata = Convert.ToInt32(LIS_DUPLICATARECEBERColl[linha].IDDUPLICATARECEBER);
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
            if (LIS_DUPLICATARECEBERColl.Count > 0)
            {
                using (FrmBaixaLoteReceber FrmBaixaLote = new FrmBaixaLoteReceber())
                {
                    int linha = dataGridDuplicatas.CurrentRow.Index; //PEGA LINHA SELECIONADA DO GRID
                    FrmBaixaLote._idCliente = Convert.ToInt32(LIS_DUPLICATARECEBERColl[linha].IDCLIENTE);
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
            GetDropCliente();
            cbCliente.Focus();

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
            btnLimpa.Image = Util.GetAddressImage(16);

            //Exibir dados do cliente consultado em outra tela
            if (CodClienteSelec != -1)
            {
                rbTodas.Checked = true;
                cbCliente.SelectedValue = CodClienteSelec;
                Pesquisa();
            }

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();

            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
            {

            }
            else
            if (LIS_DUPLICATARECEBERColl.Count > 0)
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
                        DUPLICATARECEBERBT.VALORPAGO = Convert.ToDecimal("0,00");
                        DUPLICATARECEBERBT.VALORDEVEDOR = Convert.ToDecimal(DUPLICATARECEBERBT.VALORDUPLICATA) + Convert.ToDecimal(DUPLICATARECEBERBT.VALORJUROS) + Convert.ToDecimal(DUPLICATARECEBERBT.VALORMULTA);
                        DUPLICATARECEBERBT.OBSERVACAO += "( Duplicata Estornada dia: " + DateTime.Now + " )";

                        if (DUPLICATARECEBERBT.DATAVECTO > DateTime.Now)
                            DUPLICATARECEBERBT.IDSTATUS = 1;//Aberto
                        else
                            DUPLICATARECEBERBT.IDSTATUS = 2;//Vencida

                        ExcluiContaCorrente(DUPLICATARECEBERBT.NUMERO);
                        ExcluiCaixa(DUPLICATARECEBERBT.NUMERO);

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

        private void ExcluiCaixa(string NDOCUMENTO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NDOCUMENTO", "System.String", "=", NDOCUMENTO.ToString()));

                CAIXACollection CAIXAColl = new CAIXACollection();
                CAIXAProvider CAIXAP = new CAIXAProvider();
                CAIXAColl = CAIXAP.ReadCollectionByParameter(RowRelatorio);

                foreach (CAIXAEntity item in CAIXAColl)
                {
                    CAIXAP.Delete(item.IDCAIXA);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Caixa!");
                MessageBox.Show("Erro técnico: " + ex.Message);
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
                MessageBox.Show("Erro ao Excluir Conta Corrente!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
       

        private void extratoDeClienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ImprimirExtrato();
        }

        private void ImprimirExtrato()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                if (LIS_DUPLICATARECEBERColl.Count > 0)
                {
                    if (BmsSoftware.ConfigSistema1.Default.FLAGPERGMODO == "S")
                    {
                        FrmModoImpressao FrmModoImpressao = new FrmModoImpressao();
                        FrmModoImpressao.ShowDialog();
                        var result = FrmModoImpressao.Result;
                        var local = FrmModoImpressao.local;
                        if (result == "grafico")
                            ImprimirListaGeral();
                        else if (result == "matricial")
                        {
                            ImprimirMatricial(local);
                        }
                    }
                    else
                    {
                        if (BmsSoftware.ConfigSistema1.Default.FLAGMODOGRAFICO == "S")
                            ImprimirListaGeral();
                        else if (BmsSoftware.ConfigSistema1.Default.FLAGMODOMATRICIAL == "S")
                        {
                            if (BmsSoftware.ConfigSistema1.Default.FLAGMATRICIALREDE == "S")
                                ImprimirMatricial("rede");
                            else
                                ImprimirMatricial("local");

                        }

                    }

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Faça a pesquisa antes de imprimir!",
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint,
                                  ConfigSistema1.Default.NomeEmpresa,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error,
                                  MessageBoxDefaultButton.Button1);
                this.Cursor = Cursors.Default;
            }
        }    

        private void avisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbCliente.SelectedIndex < 1)
            {
                MessageBox.Show("Selecione o cliente!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);


            }
            else if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não Existe duplicata pesquisada!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
                ImprimirAviso();

            this.Cursor = Cursors.Default;
        }

        private void ImprimirAviso()
        {
            IndexRegistro = 0;
            try
            {
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                printDialog1.Document = printDocument2;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument2;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            int LinhaInicial = 150;

            e.Graphics.DrawString("REMETENTE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial);
            e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.BAIRRO, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.CIDADE + " - " + EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.CEP, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            LinhaInicial += 120;
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, LinhaInicial, config.MargemDireita, LinhaInicial);

            LinhaInicial += 200;
            //Armazena Dados do Cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);


            e.Graphics.DrawString("DESTINATÁRIO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1 , config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].BAIRRO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO + " - " + LIS_CLIENTEColl[0].UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            LinhaInicial += 100;
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, LinhaInicial, config.MargemDireita, LinhaInicial);
            e.Graphics.DrawString(EMPRESATy.CIDADE + ", " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")), config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            e.Graphics.DrawString("Prezado(a) Senhor(a) " + LIS_CLIENTEColl[0].NOME, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString("Vimos lembrar-lhe do vencimento de sua duplicata correspondente á sua compra em nossa empresa.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);
            e.Graphics.DrawString("Temos certeza que somente a falta de tempo ou natural esquecimento fez que V. Sa. deixasse de saldar seu débito na data certa.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString("Solicitamos sua presença para negociarmos seu débito dentro de suas condições.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString("Atenciosamente  agradecemos sua atenção.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            e.Graphics.DrawString("Obs.: Caso seu débito já tenho sido quitado, favor desconsiderar este aviso.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);

            e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

        }

        private void avisoComValorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            if (cbCliente.SelectedIndex < 1)
            {
                MessageBox.Show("Selecione o cliente!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);


            }
            else if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show("Não Existe duplicata pesquisada!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
                PrintAvisoValor();

            this.Cursor = Cursors.Default;
        }

        private void PrintAvisoValor()
        {

            IndexRegistro = 0;
            try
            {
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                printDialog1.Document = printDocument3;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument3;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();

            //'nome da empresa
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            int LinhaInicial = 150;

            e.Graphics.DrawString("REMETENTE", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial);
            e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.BAIRRO, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.CIDADE + " - " + EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(EMPRESATy.CEP, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            LinhaInicial += 120;
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, LinhaInicial, config.MargemDireita, LinhaInicial);

            LinhaInicial += 200;
            //Armazena Dados do Cliente
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);


            e.Graphics.DrawString("DESTINATÁRIO", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1 , config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].BAIRRO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO + " - " + LIS_CLIENTEColl[0].UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            LinhaInicial += 100;
            e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, LinhaInicial, config.MargemDireita, LinhaInicial);
            e.Graphics.DrawString(EMPRESATy.CIDADE + ", " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")), config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            e.Graphics.DrawString("Prezado(a) Senhor(a) " + LIS_CLIENTEColl[0].NOME, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
            e.Graphics.DrawString("Comunicamos que nesta data foram constatados os seguintes débitos:", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);
            e.Graphics.DrawString("VALOR          VENCIMENTO           DUPLICATA           CENTRO DE CUSTO", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);
            e.Graphics.DrawString("----------------------------------------------------------------------------------------------------------------------------------------", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            //Alinhamento dos valores
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;

            LinhaInicial += 20;
            foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
            {
                LinhaInicial += 15;
                e.Graphics.DrawString(Convert.ToDecimal(item.VALORDEVEDOR).ToString("N2"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 60, LinhaInicial, stringFormat);
                e.Graphics.DrawString(Convert.ToDateTime(item.DATAVECTO).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 160, LinhaInicial, stringFormat);
                e.Graphics.DrawString(item.NUMERO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 270, LinhaInicial, stringFormat);
                e.Graphics.DrawString(item.NOMECENTROCUSTO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, LinhaInicial - 20);
            }
            e.Graphics.DrawString("----------------------------------------------------------------------------------------------------------------------------------------", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial);
            e.Graphics.DrawString("R$ " + lblTotalDevedor.Text + " TOTAL ATE: " + DateTime.Now.ToString("dd/MM/yyyy"), config.FonteNegrito, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);

            e.Graphics.DrawString("Aguardamos a sua presença para regularização dos seu crédito e desde já agradecemos a prefência.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);

            e.Graphics.DrawString("Obs.: Caso seu débito já tenho sido quitado, favor desconsiderar este aviso.", config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);

            e.Graphics.DrawString(EMPRESATy.NOMECLIENTE, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 25);
            e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda, LinhaInicial += 15);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Extrato de Contas a Receber - Cliente: " + cbCliente.Text);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridDuplicatas, RelatorioTitulo, this.Name);
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            using (FrmEnviarEmail Frm = new FrmEnviarEmail())
            {
                Frm._IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                Frm.ShowDialog();
            }

        }

        private void dataGridDuplicatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                PercorreGrid(e.RowIndex);
            }
            else if (e.ColumnIndex == 1)//Dados Duplicata
            {
                using (FrmContasReceber frm = new FrmContasReceber())
                 {
                     frm.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATARECEBERColl[e.RowIndex].IDDUPLICATARECEBER);
                     frm.ShowDialog();
                     Pesquisa();
                 }
            }
            else if (e.ColumnIndex == 2)//Produto da Duplicata
            {
                using (FrmListaProdutosCompra frm = new FrmListaProdutosCompra())
                {
                    frm.NFSelec = LIS_DUPLICATARECEBERColl[e.RowIndex].NOTAFISCAL;
                    frm.ShowDialog();
                    Pesquisa();
                }
            }
            
        }


        private void PercorreGrid(int linha)
        {
            //decimal TotalSelecionado = 0;
            //int i = 0;
            ////percorre as linhas do controle DataGridView  
            foreach (DataGridViewRow dr in dataGridDuplicatas.Rows)
            {
            //   // DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            //   // ch1 = (DataGridViewCheckBoxCell)dataGridDuplicatas.Rows[dataGridDuplicatas.CurrentRow.Index].Cells[0];

            //    //if( i == linha )
            //    {
            //       // dr.Cells[0].Value = !Convert.ToBoolean(dr.Cells[0].Value);

            //       // if (ch1.Value.ToString() == "True")
            //        if (dr.Cells[0].Value.ToString() == "True")
            //        {
            //            if (LIS_DUPLICATARECEBERColl[linha].IDSTATUS != 3) //3 PAGO
            //                TotalSelecionado += Convert.ToDecimal(LIS_DUPLICATARECEBERColl[i].VALORDEVEDOR);
            //        }
            //    }

            //    i++;
            }

            //txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");
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
                        TotalSelecionado -= Convert.ToDecimal(LIS_DUPLICATARECEBERColl[e.RowIndex].VALORDEVEDOR);
                        txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");
                    }
                    break;
                case "False":
                    {
                        ch1.Value = true;
                        TotalSelecionado += Convert.ToDecimal(LIS_DUPLICATARECEBERColl[e.RowIndex].VALORDEVEDOR);
                        txtTotalSelecionado.Text = TotalSelecionado.ToString("n2");
                    }
                    break;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Extrato de Contas a Receber + " + cbCliente.Text);

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
                frm.TituloSelec = "Extrato de Contas a Receber + " + cbCliente.Text;
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = dataGridDuplicatas;
                frm.ShowDialog();
            }
        }
    }
}
