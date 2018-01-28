using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.IO;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmBoleto : Form
    {
        public FrmBoleto()
        {
            InitializeComponent();
        }

        private void FrmBoleto_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
        }

        private void BoletoTeste1()
        {
            ////Informa os dados do cedente
            //string cpfcnpj = txtCPFCNPJ.Text;
            //string nome = txtNomeCedente.Text;
            //string agencia = txtAgenciaCendente.Text;
            //string cconta = txtContaCedente.Text;
            //Cedente Cedente = new Cedente(cpfcnpj, nome, agencia, cconta);
            ////Dependendo da carteira, é necessário informar o código do cedente (o banco que fornece)
            //Cedente.Codigo = Convert.ToInt32(txtCodigoCedente.Text);

            ////Dados para preenchimento do boleto (data de vencimento, valor, carteira e nosso número)
            //DateTime Vencimento = Convert.ToDateTime(txtVencimento.Text);
            //Decimal ValorBoleto = Convert.ToDecimal(txtValorBoleto.Text);
            //string nossonumero = txtNossoNumeroBoleto.Text;
            //string carteira = "102";
            //Boleto Boleto = new Boleto(Vencimento, Convert.ToDouble(ValorBoleto), carteira, nossonumero, Cedente);

            ////Dependendo da carteira, é necessário o número do documento
            //Boleto.NumeroDocumento = txtNumeroDocumentoBoleto.Text;

            ////'Informa os dados do sacado
            //Sacado Sacado = new Sacado(txtCPFCNPJSacado.Text, txtNomeSacado.Text);
            //Sacado.Endereco.End = txtEnderecoSacado.Text;
            //Sacado.Endereco.Bairro = txtBairroSacado.Text;
            //Sacado.Endereco.Cidade = txtCidadeSacado.Text;
            //Sacado.Endereco.CEP = txtCEPSacado.Text;
            //Sacado.Endereco.UF = txtUFSacado.Text;

            //// Dim i As New Instrucao_Santander()
            ////i.Descricao = "Não Receber após o vencimento"
            ////b.Instrucoes.Add(i)

            //Instrucao_Santander iSantander = new Instrucao_Santander();
            //iSantander.Descricao = "Não Receber após o vencimento";
            //Boleto.Instrucoes.Add(iSantander);

            ////'Espécie do Documento - [R] Recibo
            //Boleto.EspecieDocumento = new EspecieDocumento_Santander(17);

            //BoletoBancario BB = new BoletoBancario();
            //BB.CodigoBanco = 33; //'-> Referente ao código do Santander
            //BB.Boleto = Boleto;
            //BB.MostrarCodigoCarteira = true;
            //BB.Boleto.Valida();

            ////'true -> Mostra o compravante de entrega
            ////'false -> Oculta o comprovante de entrega
            //BB.MostrarComprovanteEntrega = true;

            ////if (panel1.Controls.Count == 0)
            ////    panel1.Controls.Add(BB);

            //// HtmlTable tabela = new HtmlTable();
            ////// string teste = BB.MontaHtml();
            //// BB.MontaHtmlNoArquivoLocal(@"C:\TESTE.html");

            ////MessageBox.Show(teste);
            ////  webBrowser1.DocumentText = BB.MontaHtml();
            //// webBrowser1.StatusText = "TESTE.html";

            //// ImpressaoBoleto fm = new ImpressaoBoleto();webBrowser1

            //string PathRegistro = ConfigSistema1.Default.PathInstall;
            ////abrir o arquivo selecionado
            //// StreamReader objReader = new StreamReader(PathRegistro + "TESTE.html", Encoding.Default);
            ////StreamWriter valor = new StreamWriter(PathRegistro + "TESTE.html", true, Encoding.ASCII);
            //int i = 0;
            ////for (i = 0; i < 10; i++)
            //{
            //    //valor.Write(i);
            //    // BB.MontaHtmlNoArquivoLocal(PathRegistro + "teste.txt");
            //    BB.MontaHtml();
            //}

            //// valor.Close();

            //// BB.MontaHtmlNoArquivoLocal();

            ////webBrowser1.Navigate(@"C:\TESTE.html");          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Boleto bollBB = new BoletoBrasil();

            ////DadosCedente
            //bollBB.Aceite = "S";
            //bollBB.CedenteAgencia = txtAgenciaCendente.Text;
            //bollBB.CedenteAgenciaDV = txtDigAgCedente.Text;
            //bollBB.CedenteConta = txtContaCedente.Text;
            //bollBB.CedenteContaDV = txtDigContaCedente.Text;
            //bollBB.CedenteNome = txtNomeCedente.Text;
            //bollBB.Carteira = Convert.ToInt32(txtCarteira.Text);
            //bollBB.Instrucao1 = txtInstrucao1.Text;
            //bollBB.Instrucao2 = txtInstrucao2.Text;
            //bollBB.Instrucao3 = txtInstrucao3.Text;
            //bollBB.Instrucao4 = txtInstrucao4.Text;
            //bollBB.Contrato = Convert.ToInt32(txtContrato.Text);

            ////DadosDocumento
            //bollBB.Sequencial =Convert.ToInt32(txtSequencial.Text);
            //bollBB.Documento = txtNumeroDocumentoBoleto.Text;
            //bollBB.DtDocumento = Convert.ToDateTime(txtDtaDocumento.Text);
            //bollBB.DtEmissao = Convert.ToDateTime(txtDtEmissao.Text);
            //bollBB.DtProcessamento =Convert.ToDateTime(txtDtProcessamento.Text);
            //bollBB.DtVencimento = Convert.ToDateTime(txtVencimento.Text);
            //bollBB.Valor = Convert.ToDecimal(txtValorBoleto.Text);

            ////DadosSacado
            //bollBB.SacadoNome = txtNomeSacado.Text;
            //bollBB.SacadoCPF_CNPJ = txtCPFCNPJSacado.Text;


            //string PathRegistro = ConfigSistema1.Default.PathInstall;
            //HTMLBoleto geraBoleto = new HTMLBoleto();
            //geraBoleto.ImagesFolder = @"images/imagesBoleto";
           
            //    geraBoleto.AddBoleto(bollBB);
           
            ////abrir o arquivo selecionado
            //// caminho e nome do arquivo
            //    PathRegistro = PathRegistro.ToLower();
            //    string arquivo = PathRegistro + "boletobancobrasil1.html";
            // // vamos verificar se este arquivo existe
            //if (File.Exists(arquivo))
            //    File.Delete(arquivo);

            //StreamWriter valor = new StreamWriter(arquivo, true, Encoding.ASCII); 
            //valor.WriteLine(geraBoleto.ToString());
            //valor.Close();
            //webBrowser1.Refresh();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        

      

      
    }
}
