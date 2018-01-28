using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using BmsSoftware.Modulos.FrmSearch;
using BMSworks.UI;
using System.IO;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmReciboAvulsoFornec : Form
    {
        Utility Util = new Utility();

        FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
        public FrmReciboAvulsoFornec()
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
            lblObsField.Text = string.Empty;

        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void FrmReciboAvulso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void FrmReciboAvulso_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetDropFornecedor();
            msktDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

            this.Cursor = Cursors.Default;	
        }

        private void cbCliente_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
            GetDropFornecedor();
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
            e.Handled = false;
        }

        private void cbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtValorRecibo_Enter(object sender, EventArgs e)
        {
            if (txtValorRecibo.Text == "0,00")
                txtValorRecibo.Text = string.Empty;
        }

        private void txtValorRecibo_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorRecibo.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorRecibo.Text))
                {
                    errorProvider1.SetError(txtValorRecibo, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtValorRecibo.Text);
                    txtValorRecibo.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorRecibo, "");
                }
            }
            else
            {
                txtValorRecibo.Text = "0,00";
                errorProvider1.SetError(txtValorRecibo, "");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                if (rbumavia.Checked)
                    ImprimirReciboAvulso1Via();
                else
                    ImprimirReciboAvulso2Via();
            }
        }

        private void ImprimirReciboAvulso2Via()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument5;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument5.DefaultPageSettings.PaperSize = new
                
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 750);

                objPrintPreview.Document = printDocument5;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void ImprimirReciboAvulso1Via()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings.PaperSize = new
               System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 400);

                objPrintPreview.Document = printDocument1;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
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
            else if (txtValorRecibo.Text.Trim().Length == 0 || txtValorRecibo.Text == "0,00")
            {
                errorProvider1.SetError(txtValorRecibo, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (msktDataEmissao.Text == "  /  /" || !ValidacoesLibrary.ValidaTipoDateTime(msktDataEmissao.Text))
            {
                errorProvider1.SetError(msktDataEmissao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void printDocument5_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Recibo 2 um via
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio da impressão - 1º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 340);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(msktDataEmissao.Text).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(txtValorRecibo.Text).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(txtValorRecibo.Text));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 140);

                //Dados do Fornecedor
                //Armazena dados do Fornecedor

                LIS_FORNECEDORCollection LIS_FORNECEDORColl = new LIS_FORNECEDORCollection();
                LIS_FORNECEDORProvider LIS_FORNECEDORP = new LIS_FORNECEDORProvider();
                RowsFiltroCollection RowRelatorioFornec = new RowsFiltroCollection();
                RowRelatorioFornec.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", (cbFornecedor.SelectedValue).ToString()));
                LIS_FORNECEDORColl = LIS_FORNECEDORP.ReadCollectionByParameter(RowRelatorioFornec);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 160);
                e.Graphics.DrawString("Nome:     " + LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 180);

                e.Graphics.DrawString("CNPJ: " + LIS_FORNECEDORColl[0].CNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 200);

                e.Graphics.DrawString("Endereço: " + LIS_FORNECEDORColl[0].ENDERECO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 220);
                e.Graphics.DrawString("Cidade:   " + LIS_FORNECEDORColl[0].CIDADE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 240);
                e.Graphics.DrawString("UF: " + LIS_FORNECEDORColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 240);
                e.Graphics.DrawString("CEP: " + LIS_FORNECEDORColl[0].CEP, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 240);

                e.Graphics.DrawString("Referente Duplicata nº: " + txtReferente.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 260);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 280, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 280);
                e.Graphics.DrawString(Util.QuebraString(txtObs.Text, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 295);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 280, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 310);
                e.Graphics.DrawString(LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 325);
                e.Graphics.DrawString("1º Via", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 720, 355);


                //Inicio da 2º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 380, config.MargemDireita, 340);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 380, config.MargemDireita, 100);

                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 388);
                    }
                }

                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 388);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 403);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 418);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 418);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 433);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 448);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 463);

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 388);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 403);
                e.Graphics.DrawString(Convert.ToDateTime(msktDataEmissao.Text).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 418);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(txtValorRecibo.Text).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 490);
                NpExtenso.SetNumero(Convert.ToDecimal(txtValorRecibo.Text));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 490);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 510);
                e.Graphics.DrawString("Nome:     " + LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 530);

                e.Graphics.DrawString("CNPJ/CPF: " + LIS_FORNECEDORColl[0].CNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 550);

                e.Graphics.DrawString("Endereço: " + LIS_FORNECEDORColl[0].ENDERECO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 570);
                e.Graphics.DrawString("Cidade:   " + LIS_FORNECEDORColl[0].CIDADE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 590);
                e.Graphics.DrawString("UF: " + LIS_FORNECEDORColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 590);
                e.Graphics.DrawString("CEP: " + LIS_FORNECEDORColl[0].CEP, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 590);

                e.Graphics.DrawString("Referente Duplicata nº: " + txtReferente.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 610);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 630, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 630);
                e.Graphics.DrawString(Util.QuebraString(txtObs.Text, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 645);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 630, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 660);
                e.Graphics.DrawString(LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 675);
                e.Graphics.DrawString("2º Via", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 720, 705);


            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Recibo 1 um via
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;

                //Inicio da impressão - 1º Via
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 340);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 100);

                //Logomarca
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE;
                e.Graphics.DrawString(Util.LimiterText(config.NomeEmpresa, 50), config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 30, 38);
                e.Graphics.DrawString(EMPRESATy.ENDERECO, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 53);
                e.Graphics.DrawString(EMPRESATy.CIDADE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 68);
                e.Graphics.DrawString(EMPRESATy.UF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 320, 68);
                e.Graphics.DrawString(EMPRESATy.TELEFONE, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 83);
                e.Graphics.DrawString(EMPRESATy.EMAIL, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 98);
                e.Graphics.DrawString("CNPJ/CPF: " + EMPRESATy.CNPJCPF, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 30, 113);

                e.Graphics.DrawString("R E C I B O", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(Convert.ToDateTime(msktDataEmissao.Text).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                e.Graphics.DrawString("R$ " + Convert.ToDecimal(txtValorRecibo.Text).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 140);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(txtValorRecibo.Text));
                e.Graphics.DrawString("Valor: ( " + NpExtenso.ToString() + " )", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 130, 140);

                //Dados do Fornecedor
                //Armazena dados do Fornecedor

                LIS_FORNECEDORCollection LIS_FORNECEDORColl = new LIS_FORNECEDORCollection();
                LIS_FORNECEDORProvider LIS_FORNECEDORP = new LIS_FORNECEDORProvider();
                RowsFiltroCollection RowRelatorioFornec = new RowsFiltroCollection();
                RowRelatorioFornec.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", cbFornecedor.SelectedValue.ToString()));
                LIS_FORNECEDORColl = LIS_FORNECEDORP.ReadCollectionByParameter(RowRelatorioFornec);

                e.Graphics.DrawString("Recebi(emos)a importância acima de: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 160);
                e.Graphics.DrawString("Nome:     " + LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 180);

                e.Graphics.DrawString("CNPJ/CPF: " + LIS_FORNECEDORColl[0].CNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 200);

                e.Graphics.DrawString("Endereço: " + LIS_FORNECEDORColl[0].ENDERECO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 220);
                e.Graphics.DrawString("Cidade:   " + LIS_FORNECEDORColl[0].CIDADE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 240);
                e.Graphics.DrawString("UF: " + LIS_FORNECEDORColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 240);
                e.Graphics.DrawString("CEP: " + LIS_FORNECEDORColl[0].CEP, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 240);

                e.Graphics.DrawString("Referente Duplicata nº: " + txtReferente.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 30, 260);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 280, config.MargemDireita - 400, 70);
                e.Graphics.DrawString("Obs.:", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 20, 280);
                e.Graphics.DrawString(Util.QuebraString(txtObs.Text, 60), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 20, 295);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 380, 280, config.MargemDireita - 390, 70);
                e.Graphics.DrawString("______________________________________________________", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 310);
                e.Graphics.DrawString(LIS_FORNECEDORColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 384, 325);
                e.Graphics.DrawString("1º Via", config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 720, 355);

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }
    }
}
