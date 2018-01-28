using BmsSoftware.Classes.BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VVX;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBaixaArquivoRemessa : Form
    {
        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        int contador = 1;

        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl2 = new LIS_DUPLICATARECEBERCollection();

        public FrmBaixaArquivoRemessa()
        {
            InitializeComponent();
        }

        private void FrmBaixaArquivoRemessa_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtcolunaInicial.Text = BmsSoftware.ConfigSistema1.Default.ColunaArquivoRemessa;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Relação de Duplicatas a Receber");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGridRelaDupl, RelatorioTitulo, this.Name);
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


        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGridRelaDupl, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Duplicatas a Receber";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGridRelaDupl;
                frm.ShowDialog();
            }
        }

        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.txt;*.csv;*.*)|*.txt;*.csv;*.*"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtArquivoRemessa.Text = openFileDialog1.FileName.ToString();
                AbrirArquivoRetorno(txtArquivoRemessa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AbrirArquivoRetorno(string NomeArquivo)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                LIS_DUPLICATARECEBERColl2.Clear();
                //Limpa dados do grid
                DataGridRelaDupl.Rows.Clear();
                TotalGeral = 0;

                string PathSystem = NomeArquivo;
                StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                    {
                        //Nosso numero
                        sLine = sLine.Substring(Convert.ToInt32(txtcolunaInicial.Text), 7);

                        //Pegar o ultimo digito
                        string sLineEnd = sLine.Substring(sLine.Length - 1, 1);
                        sLine = sLine.Substring(0, sLine.Length - 1);

                        sLine = sLine + "-" + sLineEnd;

                        //Verifica se a duplicata existe e adiciona no grid
                        VerificaExisteDuplicata(sLine);
                    }
                }

                //Somatorio
                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGridRelaDupl, "", "", "", "", "", TotalGeral.ToString("n2"), "", "", "","");
                row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGridRelaDupl.Rows.Add(row2);

                if (TotalGeral == 0)
                    MessageBox.Show("Não Foram Encontrados Registros");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void VerificaExisteDuplicata(string NumeroDuplicata)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "like", NumeroDuplicata.ToString()));
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_DUPLICATARECEBERColl.Count > 0)
                {
                    LIS_DUPLICATARECEBERColl2.AddRange(LIS_DUPLICATARECEBERColl);
                    PreencheGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        decimal TotalGeral = 0;
        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            
            try
            {
                foreach (var LIS_DUPLICATARECEBERTy in LIS_DUPLICATARECEBERColl)
                {
                    DataGridViewRow row1 = new DataGridViewRow();

                    string DataPagto = "";
                    TotalGeral = TotalGeral + Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORDUPLICATA);

                    if (LIS_DUPLICATARECEBERTy.DATAPAGTO != null)
                        DataPagto = Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAPAGTO).ToString("dd/MM/yyyy");

                    row1.CreateCells(DataGridRelaDupl, LIS_DUPLICATARECEBERTy.NUMERO, LIS_DUPLICATARECEBERTy.NOMECLIENTE, Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAEMISSAO).ToString("dd/MM/yyyy"), 
                                                       Convert.ToDateTime(LIS_DUPLICATARECEBERTy.DATAVECTO).ToString("dd/MM/yyyy"), DataPagto, Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORDUPLICATA).ToString("n2"),
                                                       Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORDEVEDOR).ToString("n2"), Convert.ToDecimal(LIS_DUPLICATARECEBERTy.VALORPAGO).ToString("n2"), LIS_DUPLICATARECEBERTy.NOMESTATUS);
                    row1.DefaultCellStyle.Font = new Font("Arial", 8);
                    DataGridRelaDupl.Rows.Add(row1);

                    contador++;
                }               

                lblObs.Text = "Total de Registros: " + contador.ToString();
                this.Cursor = Cursors.Default;               
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);                
            }
        }

        private void txtcolunaInicial_Leave(object sender, EventArgs e)
        {
            BmsSoftware.ConfigSistema1.Default.ColunaArquivoRemessa = txtcolunaInicial.Text;
            BmsSoftware.ConfigSistema1.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                BaixaDuplicatas();
                AbrirArquivoRetorno(txtArquivoRemessa.Text);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtArquivoRemessa.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label27, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (mkDtPagto.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(mkDtPagto.Text))
            {
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }   
            else if (TotalGeral == 0)
            {
                string msgerro = "Não Existem Duplicatas Selecionadas!";
                errorProvider1.SetError(label1, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();

            return result;
        }

        private void BaixaDuplicatas()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var LIS_DUPLICATARECEBERTy in LIS_DUPLICATARECEBERColl2)
                {
                    DUPLICATARECEBEREntity DUPLICATARECEBERTy = new DUPLICATARECEBEREntity();
                    DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(Convert.ToInt32(LIS_DUPLICATARECEBERTy.IDDUPLICATARECEBER));  

                    if(DUPLICATARECEBERTy != null)
                    { 
                        DUPLICATARECEBERTy.DATAPAGTO = Convert.ToDateTime(mkDtPagto.Text);
                        DUPLICATARECEBERTy.IDSTATUS = 3; //Pago
                        DUPLICATARECEBERTy.VALORPAGO = DUPLICATARECEBERTy.VALORDUPLICATA;
                        DUPLICATARECEBERTy.VALORDEVEDOR = 0;

                        //Calculo de dias de atraso
                        TimeSpan date = Convert.ToDateTime(mkDtPagto.Text) - Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO);
                        int DIASATRASO = date.Days;

                        if (DIASATRASO < 0)
                            DUPLICATARECEBERTy.DIASATRASO = 0;
                        else
                            DUPLICATARECEBERTy.DIASATRASO = DIASATRASO;

                            DUPLICATARECEBERP.Save(DUPLICATARECEBERTy);
                    }
                }

                MessageBox.Show("Duplicatas Baixadas com Sucesso!");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}
