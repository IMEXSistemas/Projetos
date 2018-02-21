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
using BmsSoftware.Modulos.FrmSearch;
using BMSworks.UI;
using BmsSoftware.Modulos.Cadastros;
using System.IO;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmVariosLancamentoReceberNFCe : Form
    {
        Utility Util = new Utility();

        LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
        TIPODUPLICATACollection TIPODUPLICATAColl = new TIPODUPLICATACollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();     

        LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int CodCliente = -1;
        public string NumPedido = string.Empty;
        public string DataPedido = string.Empty;
        public string ValorPedido = string.Empty;
        public string NotaFiscal = string.Empty;
        public string FormaPagto = string.Empty;
        public string TipoPagamento = string.Empty;

        public FrmVariosLancamentoReceberNFCe()
        {
            InitializeComponent();
        }

        private void FrmVariosLancamento_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                btnCadFornecedor.Image = Util.GetAddressImage(6);

                btnSalvar.Image = Util.GetAddressImage(15);
                btnSair.Image = Util.GetAddressImage(21);

                btnSair.Image = Util.GetAddressImage(21);
                btnSalvar.Image = Util.GetAddressImage(15);

                GetDropCliente();
                GetDropLocalCobranca();
                GetDropFormaPgto();
                GetDropTipoDuplicata();

                txtDuplicata.Text = NumPedido;
                mkdataInicial.Text = DataPedido;
                txtVlPedido.Text = ValorPedido;
                mkDataVecto.Text = Convert.ToDateTime(DataPedido).AddDays(30).ToString("dd/MM/yyyy");

                if (BmsSoftware.ConfigNFCe.Default.IdFormaPagto != string.Empty)
                    cbFormaPagto.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdFormaPagto);

                if (BmsSoftware.ConfigNFCe.Default.IdTipoPagamento != string.Empty)
                    cbTipo.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdTipoPagamento);

                cbCliente.SelectedValue = CodCliente;
                NumeroParcela();

                GridDuplicatasCliente(Convert.ToInt32(cbCliente.SelectedValue), NumPedido);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void NumeroParcela()
        {

            try
            {
                txtNParcelas.Text = "1";
                RowRelatorio.Clear();
                if (Convert.ToInt32(cbFormaPagto.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFORMAPAGAMENTO", "System.Int32", "=", cbFormaPagto.SelectedValue.ToString()));

                ITENSFORMAPAGTOProvider ITENSFORMAPAGTOP = new ITENSFORMAPAGTOProvider();
                ITENSFORMAPAGTOCollection ITENSFORMAPAGTOColl = new ITENSFORMAPAGTOCollection();
                ITENSFORMAPAGTOColl.Clear();
                ITENSFORMAPAGTOColl = ITENSFORMAPAGTOP.ReadCollectionByParameter(RowRelatorio, "IDITENSFORMAPAGTO");

                int i = 1;
                foreach (ITENSFORMAPAGTOEntity item in ITENSFORMAPAGTOColl)
                {
                    txtNParcelas.Text = i.ToString();
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropFormaPgto()
        {
            try
            {
                FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
                FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
                FORMAPAGAMENTOColl = FORMAPAGAMENTOP.ReadCollectionByParameter(null, "NOME");

                cbFormaPagto.DisplayMember = "NOME";
                cbFormaPagto.ValueMember = "IDFORMAPAGAMENTO";

                FORMAPAGAMENTOEntity FORMAPAGAMENTOTy = new FORMAPAGAMENTOEntity();
                FORMAPAGAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
                FORMAPAGAMENTOTy.IDFORMAPAGAMENTO = -1;
                FORMAPAGAMENTOColl.Add(FORMAPAGAMENTOTy);

                Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity>(cbFormaPagto.DisplayMember);

                FORMAPAGAMENTOColl.Sort(comparer.Comparer);
                cbFormaPagto.DataSource = FORMAPAGAMENTOColl;

                cbFormaPagto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropCliente()
        {
            try
            {
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTECollection CLIENTEColl = new CLIENTECollection();
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }       

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
           

        }

        private void cbFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbFornecedor_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
        }

        private void btnLancDupl_Click(object sender, EventArgs e)
        {
             if (ValidaDuplicatas())
                {
                    try
                    {
                        this.Text = "Vários Lançamento a Pagar - Processando... aguarde";

                        DateTime DATAVENCIT = Convert.ToDateTime(mkDataVecto.Text);
                        for (int i = 0; i < Convert.ToInt32(txtNParcelas.Text); i++)
                        {
                            if (!chkVectoFixo.Checked)
                            {
                                if (i > 0)
                                    DATAVENCIT = DATAVENCIT.AddDays(Convert.ToInt32(txtDiasVencimento.Text));
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    DATAVENCIT = DATAVENCIT.AddDays(30);
                                    int DIAVECTO = Convert.ToInt32(Convert.ToDateTime(mkDataVecto.Text).Day);
                                    int MESVECTO = DATAVENCIT.Month;
                                    int ANOVECTO = DATAVENCIT.Year;
                                    string DATAFIXO  =  DIAVECTO+ "/" + MESVECTO + "/" + ANOVECTO;
                                    DATAVENCIT = Convert.ToDateTime(DATAFIXO);
                                    mkDataVecto.Text = DATAVENCIT.ToString("dd/MM/yyyy");
                                }
                            }

                            DUPLICATARECEBEREntity DUPLICATARECEBERty = new DUPLICATARECEBEREntity();
                            DUPLICATARECEBERty.IDDUPLICATARECEBER = -1;
                            DUPLICATARECEBERty.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);                           

                            if (Convert.ToInt32(cbLocalCobranca.SelectedValue) > 0)
                                DUPLICATARECEBERty.IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                            if (Convert.ToInt32(cbTipo.SelectedValue) > 0)
                                DUPLICATARECEBERty.IDTIPODUPLICATA = Convert.ToInt32(cbTipo.SelectedValue);

                            int NumTotalDupl = LIS_DUPLICATARECEBERColl.Count + 1;
                            if (NumPedido != string.Empty)
                                DUPLICATARECEBERty.NUMERO = NumPedido + "-" + (i + 1).ToString();
                            else
                                DUPLICATARECEBERty.NUMERO = txtDuplicata.Text + "-" + (i + 1).ToString();

                            DUPLICATARECEBERty.DATAEMISSAO = Convert.ToDateTime(mkdataInicial.Text);
                            DUPLICATARECEBERty.DATAVECTO = DATAVENCIT;
                            DUPLICATARECEBERty.VALORDUPLICATA = Convert.ToDecimal(txtVlPedido.Text) /  Convert.ToDecimal(txtNParcelas.Text);
                            DUPLICATARECEBERty.VALORDEVEDOR = Convert.ToDecimal(txtVlPedido.Text) / Convert.ToDecimal(txtNParcelas.Text);
                            DUPLICATARECEBERty.IDSTATUS = 1;//Aberto

                            DUPLICATARECEBERty.NOTAFISCAL = txtDuplicata.Text;

                           DUPLICATARECEBERP.Save(DUPLICATARECEBERty);
                            
                        }

                        GridDuplicatasCliente(Convert.ToInt32(cbCliente.SelectedValue), txtDuplicata.Text);

                        lblTotalPesquisa.Text = LIS_DUPLICATARECEBERColl.Count.ToString();
                        this.Text = "Vários Lançamento Duplicata a Receber";

                        MessageBox.Show(ConfigMessage.Default.MsgSave);

                       
                    }
                    catch (Exception)
                    {
                        this.Text = "Vários Lançamento a Pagar";
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);                       
                    }            
                }
            }

        private void GridDuplicatasCliente(int IDCLIENTE, string numero)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "=", txtDuplicata.Text));              

                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
                dataGridDuplFornecedor.AutoGenerateColumns = false;
                dataGridDuplFornecedor.DataSource = LIS_DUPLICATARECEBERColl;

                lblTotalPesquisa.Text = LIS_DUPLICATARECEBERColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
        

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private Boolean ValidaDuplicatas()
        {
            errorProvider1.Clear();
            Boolean result = true;
            if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbCliente, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (txtDuplicata.Text.Trim().Length < 1)
            {
                errorProvider1.SetError(txtDuplicata, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlPedido.Text) || txtVlPedido.Text == "0,00")
            {
                errorProvider1.SetError(txtVlPedido, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text) || Convert.ToInt32(txtNParcelas.Text) < 1)
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtDiasVencimento.Text))
            {
                errorProvider1.SetError(txtDiasVencimento, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdataInicial.Text))
            {
                errorProvider1.SetError(mkdataInicial, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkDataVecto.Text))
            {
                errorProvider1.SetError(mkDataVecto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }else
                errorProvider1.Clear();


            return result;
        }

        private void dataGridDuplFornecedor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

   

        private void mkDataVecto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data do 1º vencimento da duplicata";
        }

        private void txtDiasVencimento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Números intervalos de dias para o vencimento das próximas duplicatas";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                    {
                        DUPLICATARECEBERP.Delete(Convert.ToInt32(item.IDDUPLICATARECEBER));
                    }

                    GridDuplicatasCliente(Convert.ToInt32(cbCliente.SelectedValue), txtDuplicata.Text);
                    MessageBox.Show(ConfigMessage.Default.MsgDelete2);
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

        private void chkVectoFixo_Click(object sender, EventArgs e)
        {
            txtDiasVencimento.Enabled = !txtDiasVencimento.Enabled;
        }

        private void txtVlParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlPedido.Text))
            {
                errorProvider1.SetError(txtVlPedido, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtVlPedido.Text);
                txtVlPedido.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtVlPedido, "");
            }
        }

        private void mkdataInicial_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdataInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkdataInicial, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(mkdataInicial, "");
            }
        }

        private void mkDataVecto_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkDataVecto.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkDataVecto, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(mkDataVecto, "");
            }
        }

        private void txtNParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                errorProvider1.SetError(txtNParcelas, "");
            }
        }

        private void txtDiasVencimento_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtDiasVencimento.Text))
            {
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                errorProvider1.SetError(txtDiasVencimento, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                errorProvider1.SetError(txtDiasVencimento, "");
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

        private void btnTipo_Click(object sender, EventArgs e)
        {
          
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

        private void printDocument6_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                config.MargemDireita = 760;
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda, 30, config.MargemDireita, 470);
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

                        //e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38);
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 570, 38, 160,80);
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

                e.Graphics.DrawString("D U P L I C A T A", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 450, 38);
                e.Graphics.DrawString("Data da Emissão", config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 53);
                e.Graphics.DrawString(mkdataInicial.Text, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 68);

                //Espaço para dados da duplicata 
                //Filtro das duplicatas compostas
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOTAFISCAL", "System.String", "=", txtDuplicata.Text));
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));                
           
                
                DUPLICATARECEBERCollection DUPLICATARECEBERCollC = new DUPLICATARECEBERCollection();
                DUPLICATARECEBERCollC = DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");
                
                //Busca o ultimo vecto
                //e soma os totais da duplicata
                Decimal TotalDuplicata = 0;
                DateTime UltimoVecto = Convert.ToDateTime(mkDataVecto.Text);
                foreach (DUPLICATARECEBEREntity item in DUPLICATARECEBERCollC)
                {
                    TotalDuplicata += Convert.ToDecimal(item.VALORDEVEDOR);
                    UltimoVecto = Convert.ToDateTime(item.DATAVECTO);
                }


                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 55);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 230, 30);

                e.Graphics.DrawString("Fatura Nº", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 140);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 660, 55);
                e.Graphics.DrawString(txtDuplicata.Text, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 180);

                e.Graphics.DrawString("Fatura", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 550, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 120, 180);

                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 140);
                e.Graphics.DrawString("Nº de Ordem", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 250, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 430, 55);
                e.Graphics.DrawString(txtDuplicata.Text , config.FonteRodape, Brushes.Black, config.MargemEsquerda + 230, 180);


                e.Graphics.DrawString("Duplicata", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 140);
                e.Graphics.DrawString("Valor", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 155);
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 140, config.MargemDireita - 330, 55);
                e.Graphics.DrawString(Convert.ToDecimal(TotalDuplicata).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, 180);

                e.Graphics.DrawString("Vencimento", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 140);
                e.Graphics.DrawString(Convert.ToDateTime(UltimoVecto).ToString("dd/MM/yyyy"), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 450, 180);

                //Uso instituição
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 550, 140, config.MargemDireita - 560, 120);
                e.Graphics.DrawString("PARA USO DA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 145);
                e.Graphics.DrawString("INSTITUIÇÃO FINANCEIRA ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 590, 155);

                e.Graphics.DrawString("DESCONTO DE: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 200);
                e.Graphics.DrawString("ATÉ: ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 450, 200);
                e.Graphics.DrawString("CONDIÇÕES ESPECIAIS", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 215);
                e.Graphics.DrawString("CENTRO DE CUSTO: " , config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 230);

                //Dados do Cliente
                //Armazena dados do cliente
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERCollC[0].IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 270, config.MargemDireita - 20, 90);
                e.Graphics.DrawString("SACADO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 275);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].NOME, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 275);

                e.Graphics.DrawString("ENDEREÇO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 290);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].ENDERECO1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 290);

                e.Graphics.DrawString("CIDADE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].MUNICIPIO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 305);

                e.Graphics.DrawString("UF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 450, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].UF, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 470, 305);

                e.Graphics.DrawString("CEP: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 305);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].CEP1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 580, 305);

                e.Graphics.DrawString("PRAÇA DE PGTO: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 320);

                e.Graphics.DrawString("TELEFONE: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 320);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].TELEFONE1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 620, 320);

                e.Graphics.DrawString("CNPJ/CPF: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 15, 335);
                //Condição para exibir o CPF ou CNPJ
                string CPFCNPJ = (LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" || LIS_CLIENTEColl[0].CNPJ == string.Empty) ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
                e.Graphics.DrawString(CPFCNPJ, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 110, 335);

                e.Graphics.DrawString("I.E/RG: ", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 550, 335);
                e.Graphics.DrawString(LIS_CLIENTEColl[0].IE, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, 335);


                //Valor por extenso
                e.Graphics.DrawRectangle(config.CanetaDaImpressora, config.MargemEsquerda + 10, 360, config.MargemDireita - 20, 50);
                e.Graphics.DrawString("VALOR POR", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 370);
                e.Graphics.DrawString("EXTENSO", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 385);
                NumeroPorExtenso NpExtenso = new NumeroPorExtenso();
                NpExtenso.SetNumero(Convert.ToDecimal(TotalDuplicata));
                e.Graphics.DrawString(NpExtenso.ToString(), config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 110, 385);

                e.Graphics.DrawString("Reconheço(emos) a exatidão desta DUPLICATA DE VENDA MERCANTIL/PRESTAÇÃO DE SERVIÇOS, na importância acima ", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 415);
                e.Graphics.DrawString("que pagarei(mos) á " + EMPRESATy.NOMECLIENTE + " ou à sua ordem na praça e vencimento indicados.", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 430);

                e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 460);
                e.Graphics.DrawString("Data do Aceite", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 15, 475);
                e.Graphics.DrawString("Assinatura do Sacado", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 500, 475);


                //Alinhamento dos valores
                StringFormat formataString = new StringFormat();
                formataString.Alignment = StringAlignment.Far;
                formataString.LineAlignment = StringAlignment.Far;

                //Rodape com a informação sobre todas as duplicatas
                //1º Coluna
                e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 10, 510);
                e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 100, 510);
                e.Graphics.DrawString("Vecto", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 150, 510);

                //2º Coluna
                if (DUPLICATARECEBERCollC.Count > 3)
                {
                    e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 220, 510);
                    e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 310, 510);
                    e.Graphics.DrawString("Vecto", config.FonteRodape, Brushes.Black, config.MargemEsquerda + 360, 510);
                }

                //3º Coluna
                if (DUPLICATARECEBERCollC.Count > 6)
                {
                    e.Graphics.DrawString("Nº Duplicata", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 430, 510);
                    e.Graphics.DrawString("Valor", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 520, 510);
                    e.Graphics.DrawString("Vecto", config.FonteRodapeNegrito, Brushes.Black, config.MargemEsquerda + 570, 510);
                }

                int linha = 525;
                int linha2 = 525;
                int linha3 = 525;
                for (int i = 0; i < DUPLICATARECEBERCollC.Count; i++)
                {
                    if (i < 3)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, linha);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 140, linha + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 150, linha);
                        linha = linha + 15;
                    }
                    else if (i < 6)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 220, linha2);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 350, linha2 + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 360, linha2);
                        linha2 = linha2 + 15;
                    }
                    else if (i < 9)
                    {
                        e.Graphics.DrawString(DUPLICATARECEBERCollC[i].NUMERO, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 430, linha3);
                        e.Graphics.DrawString(Convert.ToDecimal(DUPLICATARECEBERCollC[i].VALORDEVEDOR).ToString("n2"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 560, linha3 + 15, formataString);
                        e.Graphics.DrawString(Convert.ToDateTime(DUPLICATARECEBERCollC[i].DATAVECTO).ToString("dd/MM/yyyy"), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 570, linha3);
                        linha3 = linha3 + 15;
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LIS_DUPLICATARECEBERColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            }
            else
                ImprimirDuplicata1ViaComposta();
        }

        private void ImprimirDuplicata1ViaComposta()
        {
            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument6.DefaultPageSettings.PaperSize = new
                System.Drawing.Printing.PaperSize("PapelCustomizado", 800, 650);

                objPrintPreview.Document = printDocument6;
                objPrintPreview.WindowState = FormWindowState.Maximized;
                objPrintPreview.PrintPreviewControl.Zoom = 1;
                objPrintPreview.ShowDialog();
            }
        }

        private void dtgProdMTQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATARECEBERColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_DUPLICATARECEBERColl[rowindex].IDDUPLICATARECEBER);
                            DUPLICATARECEBERP.Delete(CodSelect);
                            GridDuplicatasCliente(Convert.ToInt32(cbCliente.SelectedValue), txtDuplicata.Text);
                            MessageBox.Show(ConfigMessage.Default.MsgDelete2);

                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }

            }
        }

        private void dtgProdMTQ_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ValueCell = dataGridDuplFornecedor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                DUPLICATARECEBEREntity DUPLICATARECEBERtY = new DUPLICATARECEBEREntity();                    
                DUPLICATARECEBERtY = DUPLICATARECEBERP.Read(Convert.ToInt32(LIS_DUPLICATARECEBERColl[e.RowIndex].IDDUPLICATARECEBER));
                DUPLICATARECEBERtY.DATAVECTO = Convert.ToDateTime(ValueCell.ToUpper());
                DUPLICATARECEBERP.Save(DUPLICATARECEBERtY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbFormaPagto_SelectedValueChanged(object sender, EventArgs e)
        {
            NumeroParcela();
        }

        private void cbFormaPagto_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
      
    }
}
