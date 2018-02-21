using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Vendas;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmConfiguarcaoNFCe : Form
    {
        Utility Util = new Utility();

        CLIENTECollection CLIENTEColl = new CLIENTECollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();

        CFOPProvider CFOPP = new CFOPProvider();

        public FrmConfiguarcaoNFCe()
        {
            InitializeComponent();
        }

        private void FrmConfiguarcaoNFCe_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetDropCliente();
            GetDropFormaPgto();
            GetDropCFOP();
            GetDropTipoDuplicata();
            GetDropLocalCobranca();           

            btnFormPagamento.Image = Util.GetAddressImage(6);
            btnCadCFOP.Image = Util.GetAddressImage(6);
            btcadlocalcobranca.Image = Util.GetAddressImage(6);

            btnSair.Image = Util.GetAddressImage(21);
            btnSalvar.Image = Util.GetAddressImage(15);

            OpenConfig();

            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void OpenConfig()
        {
            try
            {
                chkEstoqueNegativo.Checked = BmsSoftware.ConfigNFCe.Default.NaoPermitirEstoqueNegativo == "S" ? true : false;
                chkNaoValorUnitario.Checked = BmsSoftware.ConfigNFCe.Default.NaoPermitirAlteraValorUnitário == "S" ? true : false;
                chkSalvaAposCodBarra.Checked = BmsSoftware.ConfigNFCe.Default.AdicionaProdutoAposLeituraCódigoBarra == "S" ? true : false;
                chkMaxTelaAbri.Checked = BmsSoftware.ConfigNFCe.Default.MaximizarTelaAbrir == "S" ? true : false;
                chkSomProduto.Checked = BmsSoftware.ConfigNFCe.Default.EmitirSomAdicionarProduto == "S" ? true : false;
                chkPermitirDescontoFechamento.Checked = BmsSoftware.ConfigNFCe.Default.PermitirDescontoFechamento == "S" ? true : false;
                chkValorUnitarioZerado.Checked = BmsSoftware.ConfigNFCe.Default.NaoPermitirValorUnitarioZerado == "S" ? true : false;
                chkNaoPermitirAlteQuant.Checked = BmsSoftware.ConfigNFCe.Default.NaoPermitirAlterarQuantidade == "S" ? true : false;

                if (BmsSoftware.ConfigNFCe.Default.IdCliente.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdCliente))
                    cbCliente.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCliente);

                if (BmsSoftware.ConfigNFCe.Default.IdTipoPagamento.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdTipoPagamento))
                    cbTipo.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdTipoPagamento);

                if (BmsSoftware.ConfigNFCe.Default.IdFormaPagto.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdFormaPagto))
                    cbFormaPagto.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdFormaPagto);

                if (BmsSoftware.ConfigNFCe.Default.IdCFOP.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP))
                    cbCFOP.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdCFOP);

                if (BmsSoftware.ConfigNFCe.Default.IdLocalCobranca.Trim() != string.Empty && ValidacoesLibrary.ValidaTipoInt32(BmsSoftware.ConfigNFCe.Default.IdLocalCobranca))
                    cbLocalCobranca.SelectedValue = Convert.ToInt32(BmsSoftware.ConfigNFCe.Default.IdLocalCobranca);

               cbVersaoSchema.SelectedIndex =  cbVersaoSchema.FindString(BmsSoftware.ConfigNFCe.Default.VersaoSchema);

                if(BmsSoftware.ConfigNFCe.Default.SerieNFCe.Trim() == string.Empty)
                    TxtSerieNFCe.Text ="8";
                else
                    TxtSerieNFCe.Text = BmsSoftware.ConfigNFCe.Default.SerieNFCe;                
             
                txtNumeroInicialNFCe.Text = BmsSoftware.ConfigNFCe.Default.NInicialNFCe;               

                if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente.Trim() == "P")
                    rbProducaoNFe.Checked = true;
                else
                    rbHomologacaoNFe.Checked = true;

                cbVersaoSchema.SelectedIndex = cbVersaoSchema.FindString(BmsSoftware.ConfigNFCe.Default.VersaoSchema);

               //1 – Simples Nacional
               //2 - Simples Nacional – excesso de sublimite da receita bruta 
               //3 - Regime Normal NOTAS EXPLICATIVAS:
               if (BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT == "1")
                   cbCRT.SelectedIndex = 0;
               else if (BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT == "2")
                   cbCRT.SelectedIndex = 1;
               else if (BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT == "3")
                   cbCRT.SelectedIndex = 2;

                txtNomeFoto.Text = BmsSoftware.ConfigNFCe.Default.fundologo;

                if (txtNomeFoto.Text != string.Empty)
                {
                    _FOTO = GetFoto(txtNomeFoto.Text);

                    MemoryStream stream = new MemoryStream(_FOTO);
                    pictureBox1.Image = Image.FromStream(stream);

                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;                }

                txtLocalInstallNEwSystem.Text =  BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems;

                if (BmsSoftware.ConfigNFCe.Default.DiferencaFuxoHorario.Trim() == string.Empty)
                    txtDiferencaFuxoHorario.Text = "-03:00";
                else
                    txtDiferencaFuxoHorario.Text = BmsSoftware.ConfigNFCe.Default.DiferencaFuxoHorario;

                if (BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems.Trim() == string.Empty)
                    txtVersaoAplicativo.Text = "1.10";
                else
                    txtVersaoAplicativo.Text = BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems;
                          

               txtCNPJCartaoVisa.Text = BmsSoftware.ConfigNFCe.Default.CNPJCartaoVisa;
               txtCNPJCartaoMarterCard.Text = BmsSoftware.ConfigNFCe.Default.CNPJCartaoMarterCard;
               txtCNPJCartaoAmericanExpress.Text = BmsSoftware.ConfigNFCe.Default.CNPJCartaoAmericanExpress;
               txtCNPJCartaoSorocred.Text = BmsSoftware.ConfigNFCe.Default.CNPJCartaoSorocred;
               txtCNPJCartaoOutros.Text = BmsSoftware.ConfigNFCe.Default.CNPJCartaoOutros;

                if(BmsSoftware.ConfigNFCe.Default.ModeloDocFiscal.Trim() == string.Empty)
                    txtModeloDocFiscal.Text = "65";
                else
                    txtModeloDocFiscal.Text = BmsSoftware.ConfigNFCe.Default.ModeloDocFiscal;

                BmsSoftware.ConfigNFCe.Default.Transmitir = chkTransmCupom.Checked ? "N" : "S";

               if (BmsSoftware.ConfigNFCe.Default.Transmitir.Trim() == "S")
                   chkTransmCupom.Checked = true;
               else
                   chkTransmCupom.Checked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "  + ex.Message);
            }
        }

        private void GetDropTipoDuplicata()
        {
            try
            {
                TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

                cbTipo.DisplayMember = "NOME";
                cbTipo.ValueMember = "IDTIPODUPLICATA";

                cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "+ ex.Message);
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkEstoqueNegativo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnFormPagamento_Click(object sender, EventArgs e)
        {
            using (FrmFormasPagamento frm = new FrmFormasPagamento())
            {
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
                frm._IDFORMAPAGAMENTO = CodSelec;
                frm.ShowDialog();

                GetDropFormaPgto();
                cbFormaPagto.SelectedValue = CodSelec;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FrmCFOP frm = new FrmCFOP())
            {
                int CodSelec = Convert.ToInt32(cbCFOP.SelectedValue);
                frm._IdCFOP = CodSelec;
                frm.ShowDialog();

                GetDropCFOP();
                cbCFOP.SelectedValue = CodSelec;
            }
        }

        private void GetDropCFOP()
        {
            CFOPCollection CFOPColl = new CFOPCollection();
            CFOPColl = CFOPP.ReadCollectionByParameter(null, "CODCFOP");

            cbCFOP.DisplayMember = "CODCFOP";
            cbCFOP.ValueMember = "IDCFOP";

            CFOPEntity CFOPTy = new CFOPEntity();
            CFOPTy.CODCFOP = ConfigMessage.Default.MsgDrop;
            CFOPTy.IDCFOP = -1;
            CFOPColl.Add(CFOPTy);

            Phydeaux.Utilities.DynamicComparer<CFOPEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CFOPEntity>(cbCFOP.DisplayMember);

            CFOPColl.Sort(comparer.Comparer);
            cbCFOP.DataSource = CFOPColl;

            cbCFOP.SelectedIndex = 0;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                BmsSoftware.ConfigNFCe.Default.NaoPermitirEstoqueNegativo = chkEstoqueNegativo.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.NaoPermitirAlteraValorUnitário = chkNaoValorUnitario.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.AdicionaProdutoAposLeituraCódigoBarra = chkSalvaAposCodBarra.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.MaximizarTelaAbrir = chkMaxTelaAbri.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.EmitirSomAdicionarProduto = chkSomProduto.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.PermitirDescontoFechamento = chkPermitirDescontoFechamento.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.NaoPermitirValorUnitarioZerado = chkValorUnitarioZerado.Checked ? "S" : "N";
                BmsSoftware.ConfigNFCe.Default.NaoPermitirAlterarQuantidade = chkNaoPermitirAlteQuant.Checked ? "S" : "N";                
                
                BmsSoftware.ConfigNFCe.Default.IdCliente = cbCliente.SelectedValue.ToString();
                BmsSoftware.ConfigNFCe.Default.IdTipoPagamento = cbCliente.SelectedValue.ToString();
                BmsSoftware.ConfigNFCe.Default.IdFormaPagto = cbFormaPagto.SelectedValue.ToString();
                BmsSoftware.ConfigNFCe.Default.IdCFOP = cbCFOP.SelectedValue.ToString();
                BmsSoftware.ConfigNFCe.Default.IdLocalCobranca = cbLocalCobranca.SelectedValue.ToString();
                BmsSoftware.ConfigNFCe.Default.VersaoSchema = cbVersaoSchema.Text;                
                BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente = rbProducaoNFe.Checked ? "P" : "H";
                BmsSoftware.ConfigNFCe.Default.Transmitir = chkTransmCupom.Checked ? "S" : "N"; 

                BmsSoftware.ConfigNFCe.Default.VersaoSchema = cbVersaoSchema.Text;
                BmsSoftware.ConfigNFCe.Default.SerieNFCe = TxtSerieNFCe.Text;
                BmsSoftware.ConfigNFCe.Default.NInicialNFCe = txtNumeroInicialNFCe.Text;
                BmsSoftware.ConfigNFCe.Default.ModeloDocFiscal = txtModeloDocFiscal.Text;


                //1 – Simples Nacional
                //2 - Simples Nacional – excesso de sublimite da receita bruta 
                //3 - Regime Normal NOTAS EXPLICATIVAS:
                if (cbCRT.SelectedIndex == 0)
                    BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT = "1";
                else if (cbCRT.SelectedIndex == 1)
                    BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT = "2";
                else if (cbCRT.SelectedIndex == 2)
                    BmsSoftware.ConfigNFCe.Default.RegimeTributarioCRT = "3";

                BmsSoftware.ConfigNFCe.Default.fundologo = txtNomeFoto.Text;
                BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems = txtLocalInstallNEwSystem.Text;
                BmsSoftware.ConfigNFCe.Default.DiferencaFuxoHorario = txtDiferencaFuxoHorario.Text;

                if(BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems.Trim() == string.Empty)
                    BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems = "1.10";
                else
                    BmsSoftware.ConfigNFCe.Default.VersaoAplicativoNewsSystems = txtVersaoAplicativo.Text;             

                BmsSoftware.ConfigNFCe.Default.CNPJCartaoVisa = txtCNPJCartaoVisa.Text;
                BmsSoftware.ConfigNFCe.Default.CNPJCartaoMarterCard = txtCNPJCartaoMarterCard.Text;
                BmsSoftware.ConfigNFCe.Default.CNPJCartaoAmericanExpress = txtCNPJCartaoAmericanExpress.Text;
                BmsSoftware.ConfigNFCe.Default.CNPJCartaoSorocred = txtCNPJCartaoSorocred.Text;
                BmsSoftware.ConfigNFCe.Default.CNPJCartaoOutros = txtCNPJCartaoOutros.Text;
                BmsSoftware.ConfigNFCe.Default.ModeloDocFiscal = txtModeloDocFiscal.Text;

                BmsSoftware.ConfigNFCe.Default.Save();

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);                
            }
        }

        private void btcadlocalcobranca_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);               
                frm.ShowDialog();

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCadService_Click(object sender, EventArgs e)
        {
           
        }    

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cbVersaoSchema_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        byte[] _FOTO = null;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtNomeFoto.Text = openFileDialog1.FileName.ToString();

                _FOTO = GetFoto(openFileDialog1.FileName);

                MemoryStream stream = new MemoryStream(_FOTO);
                pictureBox1.Image = Image.FromStream(stream);

                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                //Redimensionando Imagens
                if (txtNomeFoto.Text != string.Empty)
                {
                    _FOTO = GetFoto(txtNomeFoto.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public static byte[] GetFoto(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Selecione a pasta";
            openFileDialog3.CheckFileExists = false;

            openFileDialog3.FileName = "[Obter Pasta…]";
            openFileDialog3.Filter = "Folders|no.files";

            openFileDialog3.ShowDialog(); 
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(openFileDialog3.FileName);
            txtLocalInstallNEwSystem.Text = path;
        }

        private void btnMaquina_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void txtCNPJCartaoVisa_Leave(object sender, EventArgs e)
        {
            if (!ValidaCNPJConfig(txtCNPJCartaoVisa.Text))
                txtCNPJCartaoVisa.Focus();
        }

        private Boolean ValidaCNPJConfig(string _CNPJ)
        {
            Boolean result = true;

            if (Util.RetiraLetras(_CNPJ).Length > 0)
            {
                if (!ValidacoesLibrary.ValidaCNPJ(_CNPJ))
                {
                    result = false;
                    string MsgErro = "CNPJ Inválido!";
                    MessageBox.Show(MsgErro);
                }              
            }

            return result;
        }

        private void txtCNPJCartaoAmericanExpress_Leave(object sender, EventArgs e)
        {
            if (!ValidaCNPJConfig(txtCNPJCartaoAmericanExpress.Text))
                txtCNPJCartaoAmericanExpress.Focus();
        }

        private void txtCNPJCartaoOutros_Leave(object sender, EventArgs e)
        {
            if (!ValidaCNPJConfig(txtCNPJCartaoOutros.Text))
                txtCNPJCartaoOutros.Focus();
        }

        private void txtCNPJCartaoMarterCard_Leave(object sender, EventArgs e)
        {
            if (!ValidaCNPJConfig(txtCNPJCartaoMarterCard.Text))
                txtCNPJCartaoMarterCard.Focus();
        }

        private void txtCNPJCartaoSorocred_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void txtCNPJCartaoSorocred_Leave(object sender, EventArgs e)
        {
            if (!ValidaCNPJConfig(txtCNPJCartaoSorocred.Text))
                txtCNPJCartaoSorocred.Focus();
        }
    }
}

