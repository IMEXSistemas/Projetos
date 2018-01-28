using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using System.Windows.Forms;
using BoletoNet;
using BmsSoftware.Modulos.Financeiro;
using BMSworks.UI;

namespace BmsSoftware.UI
{
    public partial class PrintBoletoBancario
    {
        Utility Util = new Utility();

        CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
        DUPLICATARECEBEREntity DUPLICATARECEBERTy = new DUPLICATARECEBEREntity();

        CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
        CLIENTEProvider ClienteP = new CLIENTEProvider();
        DUPLICATARECEBERProvider DUPLICATARECEBERP = new DUPLICATARECEBERProvider();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();

        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();

        RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();

        /// <summary>
        /// Imprimir boleta bancaria do banco do brasil
        /// </summary>
        public void ImprimiBoletaBrasil(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                        string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_BancoBrasil item1 = new Instrucao_BancoBrasil(9, 5);
            Instrucao_BancoBrasil item3 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item2 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item4 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item5 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item6 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item7 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item8 = new Instrucao_BancoBrasil(81, 10);
            Instrucao_BancoBrasil item9 = new Instrucao_BancoBrasil(81, 10);

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.DIGAGENCIA,
                                    CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            Double valorboleto = Convert.ToDouble(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDouble(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  valorboleto,
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;
            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 001; //Codigo do banco
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            //Salva o numero da boleta
            DUPLICATARECEBERTy.OBSERVACAO = NossoNumero;
            DUPLICATARECEBERP.Save(DUPLICATARECEBERTy);

            string arquivo = ConfigSistema1.Default.PathInstall + @"\boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }




        /// <summary>
        /// Imprimir boleta bancaria do banco Basa
        /// </summary>
        public void ImprimiBoletaBasa(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                        string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5);
            Instrucao_Banrisul item3 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item2 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item4 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item5 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item6 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item7 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item8 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item9 = new Instrucao_Banrisul(81, 10);

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.DIGAGENCIA,
                                    CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                 Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;
            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 003;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do Santander
        /// </summary>
        public void ImprimiBoletaSantander(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                     string FormatoCarne)
        {

            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);

            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
            LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
            LIS_DUPLICATARECEBERColl.Clear();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", idDuplicata.ToString()));
            LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");

            //Dados para emitir boleto
            string data_vencimento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos 
            string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string conta = CONFIGBOLETATy.CONTA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string codcedente = Util.LimiterText(CONFIGBOLETATy.CODCEDENTE.ToString().PadLeft(7, '0'), 7);//Até 7 Digitos - Código Cedente
            string dac_conta = CONFIGBOLETATy.DIGCONTA.TrimEnd().TrimStart(); 	// Digito da Conta Corrente 1 Digito
            string nosso_numero = Util.RetiraLetras(LIS_DUPLICATARECEBERColl[0].NUMERO).PadLeft(7, '0'); //"3175233"; 	// Nosso Numero
            string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
            string data_documento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAEMISSAO).ToString("dd/MM/yyyy"); // Data de emissão do Boleto dd/MM/yyyy
            string valor = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[0].VALORDUPLICATA).ToString("n2").Replace(".", ""); // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = LIS_DUPLICATARECEBERColl[0].NUMERO;// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string cn_pj = Util.LimiterText(Util.RetiraLetras(EMPRESATy.CNPJCPF), 3);
            string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
            string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
            string cedente = EMPRESATy.NOMEFANTASIA;

            //===Dados do seu Cliente (Opcional)===============
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_DUPLICATARECEBERColl[0].IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            string CPFCNPJ = "CPF: " + LIS_CLIENTEColl[0].CPF;
            if (LIS_CLIENTEColl[0].CPF == "   .   .   -")
                CPFCNPJ = "CNPJ: " + LIS_CLIENTEColl[0].CNPJ;

            string sacado = LIS_DUPLICATARECEBERColl[0].NOMECLIENTE + " - " + CPFCNPJ;
            string endereco1 = LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER;
            string endereco2 = LIS_CLIENTEColl[0].MUNICIPIO + " " + LIS_CLIENTEColl[0].UF;

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = CONFIGBOLETATy.INSTRUCAO1;//Instruçoes para o Cliente
            string instrucoes1 = CONFIGBOLETATy.INSTRUCAO2; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = CONFIGBOLETATy.INSTRUCAO3;
            string instrucoes3 = CONFIGBOLETATy.INSTRUCAO4;
            string instrucoes4 = CONFIGBOLETATy.INSTRUCAO5;
            string instrucoes5 = CONFIGBOLETATy.INSTRUCAO6;

            // string arquivo = "http://boletosantander.imexsistema.com.br/boleto-santander-banespa.php?data_vencimento=" + data_vencimento + "&agencia=" +
            //  string arquivo = "http://imexsistemas.gratisphphost.info/boletos/Boleto_Santander/boleto-santander-banespa.php?data_vencimento=" + data_vencimento + "&agencia=" +
            string arquivo = "http://143.95.75.225/~imexsist/boletabancaria/Boleto_Santander/boleto-santander-banespa.php?data_vencimento=" + data_vencimento + "&agencia=" +
            agencia + "&digito_agencia=" + digito_agencia + "&codcedente=" + codcedente +
            "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
            "&cpf_cnpj_cedente=" + cpf_cnpj_cedente + "&cn_pj=" + cn_pj +
            "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
            "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";



            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }


        /// <summary>
        /// Imprimir boleta bancaria do Banrisul
        /// </summary>
        public void ImprimiBoletaBanrisul(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                        string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5);
            Instrucao_Banrisul item3 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item2 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item4 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item5 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item6 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item7 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item8 = new Instrucao_Banrisul(81, 10);
            Instrucao_Banrisul item9 = new Instrucao_Banrisul(81, 10);

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.DIGAGENCIA,
                                    CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            Double valorboleto = Convert.ToDouble(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDouble(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  valorboleto,
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c, new EspecieDocumento(341,1));

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;
            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 041;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do BRB
        /// </summary>
        public void ImprimiBoletaBRB(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                        string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_BRB item1 = new Instrucao_BRB();
            Instrucao_BRB item3 = new Instrucao_BRB();
            Instrucao_BRB item2 = new Instrucao_BRB();
            Instrucao_BRB item4 = new Instrucao_BRB();
            Instrucao_BRB item5 = new Instrucao_BRB();
            Instrucao_BRB item6 = new Instrucao_BRB();
            Instrucao_BRB item7 = new Instrucao_BRB();
            Instrucao_BRB item8 = new Instrucao_BRB();
            Instrucao_BRB item9 = new Instrucao_BRB();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.DIGAGENCIA,
                                    CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "00000001"));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 070;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }
        /// <summary>
        /// Imprimir boleta bancaria da Caixa
        /// </summary>
        public void ImprimiBoletaCaixa(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                        string FormatoCarne)
        {

            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);

            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
            LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
            LIS_DUPLICATARECEBERColl.Clear();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", idDuplicata.ToString()));
            LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");

            //Dados para emitir boleto
            string data_vencimento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string conta = CONFIGBOLETATy.CODCEDENTE.ToString().TrimEnd().TrimStart(); // 
            string dac_conta = CONFIGBOLETATy.DIGCEDENTE.TrimEnd().TrimStart(); 	// Digito da Conta Corrente 1 Digito
            string codcedente = CONFIGBOLETATy.CODCEDENTE.ToString().TrimEnd().TrimStart();// CONFIGBOLETATy.CODCEDENTE.ToString(); // Numero do Convenio 
            string nosso_numero = Util.RetiraLetras(LIS_DUPLICATARECEBERColl[0].NUMERO).PadLeft(7, '0'); //"3175233"; 	// Nosso Numero
            string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
            string data_documento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAEMISSAO).ToString("dd/MM/yyyy"); // Data de emissão do Boleto dd/MM/yyyy
            string valor = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[0].VALORDUPLICATA).ToString("n2").Replace(".", ""); // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = LIS_DUPLICATARECEBERColl[0].NUMERO;// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string cn_pj = Util.LimiterText(Util.RetiraLetras(EMPRESATy.CNPJCPF), 3);
            string enderecocedente = Util.LimiterText(EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO, 30);
            string cidadecedente = Util.LimiterText(EMPRESATy.CIDADE + " " + EMPRESATy.UF, 19);
            string cedente = Util.LimiterText(EMPRESATy.NOMEFANTASIA, 37);

            //===Dados do seu Cliente (Opcional)===============
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_DUPLICATARECEBERColl[0].IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            string CPFCNPJ = "CPF: " + LIS_CLIENTEColl[0].CPF;
            if (LIS_CLIENTEColl[0].CPF == "   .   .   -")
                CPFCNPJ = "CNPJ: " + LIS_CLIENTEColl[0].CNPJ;

            string sacado = LIS_DUPLICATARECEBERColl[0].NOMECLIENTE + " - " + CPFCNPJ;
            string endereco1cliente = LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER;
            string endereco2cliente = LIS_CLIENTEColl[0].MUNICIPIO + " " + LIS_CLIENTEColl[0].UF;

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = CONFIGBOLETATy.INSTRUCAO1;//Instruçoes para o Cliente
            string instrucoes1 = CONFIGBOLETATy.INSTRUCAO2; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = CONFIGBOLETATy.INSTRUCAO3;
            string instrucoes3 = CONFIGBOLETATy.INSTRUCAO4;
            string instrucoes4 = CONFIGBOLETATy.INSTRUCAO5;
            string instrucoes5 = CONFIGBOLETATy.INSTRUCAO6;

            //string arquivo = "http://imexsistemas.gratisphphost.info/boletos/Boleto_CEF_PHP/Boleto_CEF_PHP/boleto-caixa.php?data_vencimento=" + data_vencimento + "&agencia=" +
            //string arquivo = "http://143.95.75.225/~imexsist/imexsistemas.com.br/boletabancaria/Boleto_CEF_PHP/Boleto_CEF_PHP/boleto-caixa.php?data_vencimento=" + data_vencimento + "&agencia=" +
            string arquivo = "http://143.95.75.225/~imexsist/boletabancaria/Boleto_CEF_PHP/Boleto_CEF_PHP/boleto-caixa.php?data_vencimento=" + data_vencimento + "&agencia=" +
                            agencia + "&conta=" + conta + "&digito_agencia=" + digito_agencia + "&codcedente=" + codcedente + "&dac_conta=" + dac_conta +
                          "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                        "&cpf_cnpj_cedente=" + cpf_cnpj_cedente + "&cn_pj=" + cn_pj +
                      "&endereco=" + endereco1cliente + "&cidade=" + endereco2cliente + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1cliente=" + endereco1cliente + "&endereco2cliente=" + endereco2cliente + "&instrucoes=" + instrucoes +
                    "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&enderecocedente=" + enderecocedente + "&cidadecedente=" + cidadecedente;



            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do Bradesco
        /// </summary>
        public void ImprimiBoletaBradesco(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Bradesco item1 = new Instrucao_Bradesco(9, 5);
            Instrucao_Bradesco item3 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item2 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item4 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item5 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item6 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item7 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item8 = new Instrucao_Bradesco(81, 10);
            Instrucao_Bradesco item9 = new Instrucao_Bradesco(81, 10);

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.DIGAGENCIA,
                                    CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);


            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 237;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }

        }

        /// <summary>
        /// Imprimir boleta bancaria do Itau
        /// </summary>
        //public void ImprimiBoletaItau(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
        //                                  string FormatoCarne)
        //{
        //    CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
        //    DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

        //    //Instruções
        //    Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
        //    Instrucao_Itau item3 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item4 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item5 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item6 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item7 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item8 = new Instrucao_Itau(81, 10);
        //    Instrucao_Itau item9 = new Instrucao_Itau(81, 10);

        //    Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
        //                            CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA);

        //    //Na carteira 198 o código do Cedente é a conta bancária
        //    if (CONFIGBOLETATy.CONVENIO != string.Empty)
        //        c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

        //    if (CONFIGBOLETATy.CODCEDENTE != null)
        //        c.Codigo = CONFIGBOLETATy.CODCEDENTE.ToString();

        //    //Retira todas as letras e limita em 10 digitos o nosso numero
        //    string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

        //    decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

        //    Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
        //                          valorboleto,
        //                          CONFIGBOLETATy.CARTEIRA, NossoNumero, c, new EspecieDocumento(341, "1"));

        //    if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
        //        b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

        //    b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

        //    //Armazena dados do cliente
        //    RowRelatorioCliente.Clear();
        //    RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
        //    LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

        //    string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
        //    b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
        //    b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
        //    b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
        //    b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
        //    b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
        //    b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

        //    b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

        //    // Exemplo de como adicionar mais informações ao sacado
        //    b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

        //    b.Instrucoes.Add(item1);
        //    item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
        //    b.Instrucoes.Add(item2);
        //    item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
        //    b.Instrucoes.Add(item3);
        //    item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
        //    b.Instrucoes.Add(item4);
        //    item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
        //    b.Instrucoes.Add(item5);
        //    item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
        //    b.Instrucoes.Add(item6);
        //    item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
        //    b.Instrucoes.Add(item7);
        //    item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
        //    b.Instrucoes.Add(item8);
        //    item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
        //    b.Instrucoes.Add(item9);
        //    item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

        //    BoletoBancario BolB = new BoletoBancario();
        //    BolB.CodigoBanco = 341;
        //    BolB.ID = "BolB";

        //    if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
        //        BolB.MostrarContraApresentacaoNaDataVencimento = true;

        //    BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
        //    BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

        //    BolB.Boleto = b;
        //    BolB.Boleto.Valida();

        //    string arquivo = ConfigSistema1.Default.PathInstall + @"\boletobancaria.html";
        //    BolB.MontaHtml(arquivo, "");
        //    BolB.MontaHtmlNoArquivoLocal(arquivo);

        //    using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
        //    {
        //        frm.ShowDialog();
        //    }
        //}

        /// <summary>
        /// Imprimir boleta bancaria do Itau
        /// </summary>
        public void ImprimiBoletaItau(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                     string FormatoCarne)
        {

            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);

            //Dados da Empresa Registro
            EMPRESAProvider EMPRESAP = new EMPRESAProvider();
            EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

            LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
            LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
            LIS_DUPLICATARECEBERColl.Clear();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", idDuplicata.ToString()));
            LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");

            //Dados para emitir boleto
            string data_vencimento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
            string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos s/DAC
            string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
            string conta = CONFIGBOLETATy.CONTA.TrimEnd().TrimStart(); // 1 Numero da Conta
            string digito_conta = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); //  Digito da Conta
            string codcedente = CONFIGBOLETATy.CODCEDENTE.ToString(); // Numero do Convenio 
            string nosso_numero = Util.RetiraLetras(LIS_DUPLICATARECEBERColl[0].NUMERO).PadLeft(7, '0'); //"3175233"; 	// Nosso Numero
            string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
            string data_documento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAEMISSAO).ToString("dd/MM/yyyy"); // Data de emissão do Boleto dd/MM/yyyy
            string valor = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[0].VALORDUPLICATA).ToString("n2").Replace(".", ""); // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
            string numero_documento = LIS_DUPLICATARECEBERColl[0].NUMERO;// Numero do Pedido ou Nosso Numero

            //=============Dados da Sua empresa===============
            string cpf_cnpj_cedente = EMPRESATy.CNPJCPF;
            string cn_pj = Util.LimiterText(Util.RetiraLetras(EMPRESATy.CNPJCPF), 3);
            string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
            string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
            string cedente = EMPRESATy.NOMECLIENTE;

            //===Dados do seu Cliente (Opcional)===============
            LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
            LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_DUPLICATARECEBERColl[0].IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            string sacado = LIS_DUPLICATARECEBERColl[0].NOMECLIENTE;
            string endereco1 = LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER;
            string endereco2 = LIS_CLIENTEColl[0].MUNICIPIO + " " + LIS_CLIENTEColl[0].UF;

            //==Os Campos Abaixo são Opcionais=================
            string instrucoes = CONFIGBOLETATy.INSTRUCAO1;//Instruçoes para o Cliente
            string instrucoes1 = CONFIGBOLETATy.INSTRUCAO2; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
            string instrucoes2 = CONFIGBOLETATy.INSTRUCAO3;
            string instrucoes3 = CONFIGBOLETATy.INSTRUCAO4;
            string instrucoes4 = CONFIGBOLETATy.INSTRUCAO5;
            string instrucoes5 = CONFIGBOLETATy.INSTRUCAO6;

            // string arquivo = "http://www.bmsltda.com.br/boletobancaria/Boleto_ITAU_PHP/boleto-itau.php
            //string arquivo = "http://boletaitauphp.imexsistema.com.br/boleto-itau.php?data_vencimento=" + data_vencimento + "&agencia=" +
            string arquivo = "http://imexsistemas.gratisphphost.info/boletos/Boleto_Itau/Boleto_ITAU_PHP/boleto-itau.php?data_vencimento=" + data_vencimento + "&agencia=" +
                                agencia + "&conta=" + conta + "&digito_conta=" + digito_conta + "&codcedente=" + codcedente +
                                "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                                "&cpf_cnpj_cedente=" + cpf_cnpj_cedente + "&cn_pj=" + cn_pj +
                                "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
                                "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";


            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.BoletaPHP = true;
                frm.ArquivoPHP = arquivo;
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do Sudameris
        /// </summary>
        public void ImprimiBoletaSudameris(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Santander item1 = new Instrucao_Santander();
            Instrucao_Santander item3 = new Instrucao_Santander();
            Instrucao_Santander item2 = new Instrucao_Santander();
            Instrucao_Santander item4 = new Instrucao_Santander();
            Instrucao_Santander item5 = new Instrucao_Santander();
            Instrucao_Santander item6 = new Instrucao_Santander();
            Instrucao_Santander item7 = new Instrucao_Santander();
            Instrucao_Santander item8 = new Instrucao_Santander();
            Instrucao_Santander item9 = new Instrucao_Santander();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 347;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do Real
        /// </summary>
        public void ImprimiBoletaReal(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Real item1 = new Instrucao_Real();
            Instrucao_Real item3 = new Instrucao_Real();
            Instrucao_Real item2 = new Instrucao_Real();
            Instrucao_Real item4 = new Instrucao_Real();
            Instrucao_Real item5 = new Instrucao_Real();
            Instrucao_Real item6 = new Instrucao_Real();
            Instrucao_Real item7 = new Instrucao_Real();
            Instrucao_Real item8 = new Instrucao_Real();
            Instrucao_Real item9 = new Instrucao_Real();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c, new EspecieDocumento(356, 9));

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 356;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do HSBC
        /// </summary>
        public void ImprimiBoletaHSBC(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Caixa item1 = new Instrucao_Caixa();
            Instrucao_Caixa item3 = new Instrucao_Caixa();
            Instrucao_Caixa item2 = new Instrucao_Caixa();
            Instrucao_Caixa item4 = new Instrucao_Caixa();
            Instrucao_Caixa item5 = new Instrucao_Caixa();
            Instrucao_Caixa item6 = new Instrucao_Caixa();
            Instrucao_Caixa item7 = new Instrucao_Caixa();
            Instrucao_Caixa item8 = new Instrucao_Caixa();
            Instrucao_Caixa item9 = new Instrucao_Caixa();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);
            string DataVencimentoTeste = DateTime.Now.AddDays(1).ToString();

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 399;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }


        /// <summary>
        /// Imprimir boleta bancaria do Unibanco
        /// </summary>
        public void ImprimiBoletaUnibanco(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Santander item1 = new Instrucao_Santander();
            Instrucao_Santander item3 = new Instrucao_Santander();
            Instrucao_Santander item2 = new Instrucao_Santander();
            Instrucao_Santander item4 = new Instrucao_Santander();
            Instrucao_Santander item5 = new Instrucao_Santander();
            Instrucao_Santander item6 = new Instrucao_Santander();
            Instrucao_Santander item7 = new Instrucao_Santander();
            Instrucao_Santander item8 = new Instrucao_Santander();
            Instrucao_Santander item9 = new Instrucao_Santander();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA, CONFIGBOLETATy.DIGCONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 409;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }


        /// <summary>
        /// Imprimir boleta bancaria do Safra
        /// </summary>
        public void ImprimiBoletaSafra(int IDCONFIGBOLETA, int idDuplicata, string ComprovanteEntega,
                                          string FormatoCarne)
        {
            CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);
            DUPLICATARECEBERTy = DUPLICATARECEBERP.Read(idDuplicata);

            //Instruções
            Instrucao_Safra item1 = new Instrucao_Safra();
            Instrucao_Safra item3 = new Instrucao_Safra();
            Instrucao_Safra item2 = new Instrucao_Safra();
            Instrucao_Safra item4 = new Instrucao_Safra();
            Instrucao_Safra item5 = new Instrucao_Safra();
            Instrucao_Safra item6 = new Instrucao_Safra();
            Instrucao_Safra item7 = new Instrucao_Safra();
            Instrucao_Safra item8 = new Instrucao_Safra();
            Instrucao_Safra item9 = new Instrucao_Safra();

            Cedente c = new Cedente(CONFIGBOLETATy.CPFCNPJCEDENTE, CONFIGBOLETATy.NOMECEDENTE,
                                    CONFIGBOLETATy.AGENCIA, CONFIGBOLETATy.CONTA);

            if (CONFIGBOLETATy.CONVENIO != string.Empty)
                c.Convenio = Convert.ToInt32(CONFIGBOLETATy.CONVENIO);

            if (CONFIGBOLETATy.CODCEDENTE != null)
                c.Codigo = Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE);

            //Retira todas as letras e limita em 10 digitos o nosso numero
            string NossoNumero = Util.LimiterText(Util.RetiraLetras(DUPLICATARECEBERTy.NUMERO).ToString(), 10);

            decimal valorboleto = Convert.ToDecimal(DUPLICATARECEBERTy.VALORDEVEDOR) + Convert.ToDecimal(CONFIGBOLETATy.VALORTAXA);

            Boleto b = new Boleto(Convert.ToDateTime(DUPLICATARECEBERTy.DATAVECTO),
                                  Convert.ToDouble(valorboleto),
                                  CONFIGBOLETATy.CARTEIRA, NossoNumero, c);

            if (CONFIGBOLETATy.TIPOMODALIDADE != string.Empty)
                b.TipoModalidade = CONFIGBOLETATy.TIPOMODALIDADE;

            b.NumeroDocumento = DUPLICATARECEBERTy.NUMERO;

            //Dados do Cliente
            //Armazena dados do cliente
            RowRelatorioCliente.Clear();
            RowRelatorioCliente.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", DUPLICATARECEBERTy.IDCLIENTE.ToString()));
            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorioCliente);

            string CPFCNPJ = LIS_CLIENTEColl[0].CNPJ == "  .   .   /    -" ? LIS_CLIENTEColl[0].CPF : LIS_CLIENTEColl[0].CNPJ;
            b.Sacado = new Sacado(CPFCNPJ, LIS_CLIENTEColl[0].NOME);
            b.Sacado.Endereco.End = LIS_CLIENTEColl[0].ENDERECO1;
            b.Sacado.Endereco.Bairro = LIS_CLIENTEColl[0].BAIRRO1;
            b.Sacado.Endereco.Cidade = LIS_CLIENTEColl[0].MUNICIPIO;
            b.Sacado.Endereco.CEP = LIS_CLIENTEColl[0].CEP1;
            b.Sacado.Endereco.UF = LIS_CLIENTEColl[0].UF;

            // Exemplo de como adicionar mais informações ao sacado
            b.DataDocumento = Convert.ToDateTime(DUPLICATARECEBERTy.DATAEMISSAO);

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + DUPLICATARECEBERTy.NUMERO));

            b.Instrucoes.Add(item1);
            item1.Descricao = CONFIGBOLETATy.INSTRUCAO1;
            b.Instrucoes.Add(item2);
            item2.Descricao = CONFIGBOLETATy.INSTRUCAO2;
            b.Instrucoes.Add(item3);
            item3.Descricao = CONFIGBOLETATy.INSTRUCAO3;
            b.Instrucoes.Add(item4);
            item4.Descricao = CONFIGBOLETATy.INSTRUCAO4;
            b.Instrucoes.Add(item5);
            item5.Descricao = CONFIGBOLETATy.INSTRUCAO5;
            b.Instrucoes.Add(item6);
            item6.Descricao = CONFIGBOLETATy.INSTRUCAO6;
            b.Instrucoes.Add(item7);
            item7.Descricao = CONFIGBOLETATy.INSTRUCAO7;
            b.Instrucoes.Add(item8);
            item8.Descricao = CONFIGBOLETATy.INSTRUCAO8;
            b.Instrucoes.Add(item9);
            item9.Descricao = CONFIGBOLETATy.INSTRUCAO9;

            BoletoBancario BolB = new BoletoBancario();
            BolB.CodigoBanco = 422;
            BolB.ID = "BolB";

            if (DUPLICATARECEBERTy.DATAVECTO < DateTime.Now)
                BolB.MostrarContraApresentacaoNaDataVencimento = true;

            BolB.MostrarComprovanteEntrega = ComprovanteEntega == "S" ? true : false;
            BolB.FormatoCarne = FormatoCarne == "S" ? true : false;

            BolB.Boleto = b;
            BolB.Boleto.Valida();

            string arquivo = ConfigSistema1.Default.PathInstall + "boletobancaria.html";
            BolB.MontaHtml(arquivo);
            BolB.MontaHtmlNoArquivoLocal(arquivo);

            using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Imprimir boleta bancaria do SICOOB
        /// </summary>
        public void ImprimirBoletaSICOOB(int IDCONFIGBOLETA, int idDuplicata)
        {

            try
            {
                CONFIGBOLETATy = CONFIGBOLETAP.Read(IDCONFIGBOLETA);

                //Dados da Empresa Registro
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
                LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
                LIS_DUPLICATARECEBERColl.Clear();
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDDUPLICATARECEBER", "System.Int32", "=", idDuplicata.ToString()));
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATARECEBER");

                //Dados para emitir boleto
                string data_vencimento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAVECTO).ToString("dd/MM/yyyy");// Data de Vencimento do Boleto
                string agencia = CONFIGBOLETATy.AGENCIA.TrimEnd().TrimStart();// Numero da Agência até 4 Digitos s/DAC
                string digito_agencia = CONFIGBOLETATy.DIGAGENCIA.TrimEnd().TrimStart(); // 1 Digito da Agência
                string codigo_cedente = CONFIGBOLETATy.CODCEDENTE.ToString(); //Codigo do Conveneio 
                string digito_codigo_cedente = CONFIGBOLETATy.DIGCEDENTE.TrimEnd().TrimStart(); 	// Digito do cedente
                string nosso_numero = Util.RetiraLetras(LIS_DUPLICATARECEBERColl[0].NUMERO).TrimStart('0'); // Nosso Numero //Retira zero a esquerda
                string carteira = CONFIGBOLETATy.CARTEIRA.TrimEnd().TrimStart(); // Código da Carteira
                string data_documento = Convert.ToDateTime(LIS_DUPLICATARECEBERColl[0].DATAEMISSAO).ToString("dd/MM/yyyy"); // Data de emissão do Boleto dd/MM/yyyy
                string valor = Convert.ToDecimal(LIS_DUPLICATARECEBERColl[0].VALORDUPLICATA).ToString("n2").Replace(".", ""); // Valor do Boleto (Utilizar virgula como separador decimal, não use pontos)
                string numero_documento = LIS_DUPLICATARECEBERColl[0].NUMERO.TrimStart('0'); // Numero do Pedido ou Nosso Numero

                //=============Dados da Sua empresa===============   
                string cpf_cnpj_cedente = CONFIGBOLETATy.CPFCNPJCEDENTE;
                string endereco = EMPRESATy.ENDERECO + " " + EMPRESATy.NUMERO + " " + EMPRESATy.BAIRRO;
                string cidade = EMPRESATy.CIDADE + " " + EMPRESATy.UF;
                string cedente = EMPRESATy.NOMECLIENTE;

                //===Dados do seu Cliente (Opcional)===============
                LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
                LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", LIS_DUPLICATARECEBERColl[0].IDCLIENTE.ToString()));
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                string cpfcnpjcliente = LIS_CLIENTEColl[0].CPF;
                if (LIS_CLIENTEColl[0].CPF != "   .   .   -")
                    cpfcnpjcliente = "CPF: " + LIS_CLIENTEColl[0].CPF;
                else
                    cpfcnpjcliente = "CNPJ: " + LIS_CLIENTEColl[0].CNPJ;

                string sacado = LIS_DUPLICATARECEBERColl[0].NOMECLIENTE + " / " + cpfcnpjcliente;
                string endereco1 = LIS_CLIENTEColl[0].ENDERECO1 + " " + LIS_CLIENTEColl[0].NUMEROENDER;
                string endereco2 = LIS_CLIENTEColl[0].MUNICIPIO + " " + LIS_CLIENTEColl[0].UF;

                //==Os Campos Abaixo são Opcionais=================
                string instrucoes = CONFIGBOLETATy.INSTRUCAO1;//Instruçoes para o Cliente
                string instrucoes1 = CONFIGBOLETATy.INSTRUCAO2; // Por exemplo "Não receber apos o vencimento" ou "Cobrar Multa de 1% ao mês"
                string instrucoes2 = CONFIGBOLETATy.INSTRUCAO3;
                string instrucoes3 = CONFIGBOLETATy.INSTRUCAO4;
                string instrucoes4 = CONFIGBOLETATy.INSTRUCAO5;
                string instrucoes5 = CONFIGBOLETATy.INSTRUCAO6;


                // string arquivo = "http://boletasicoob.imexsistema.com.br/boleto-sicoob.php?data_vencimento=" + data_vencimento + "&agencia=" +
                string arquivo = "http://143.95.75.225/~imexsist/boletabancaria/Boleto_SICOOB_PHP/boleto-sicoob.php?data_vencimento=" + data_vencimento + "&agencia=" +
                agencia + "&digito_agencia=" + digito_agencia + "&codigo_cedente=" + codigo_cedente +
                                    "&nosso_numero=" + nosso_numero + "&carteira=" + carteira + "&data_documento=" + data_documento + "&valor=" + valor + "&numero_documento=" + numero_documento +
                                    "&cpf_cnpj_cedente=" + cpf_cnpj_cedente +
                                    "&endereco=" + endereco + "&cidade=" + cidade + "&cedente=" + cedente + "&sacado=" + sacado + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&instrucoes=" + instrucoes +
                                    "&instrucoes1=" + instrucoes1 + "&instrucoes2=" + instrucoes2 + "&instrucoes3=" + instrucoes3 + "&instrucoes4=" + instrucoes4 + "&instrucoes5";


                using (FrmBoletaVisualiza frm = new FrmBoletaVisualiza())
                {
                    frm.BoletaPHP = true;
                    frm.ArquivoPHP = arquivo;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar boleto bancario do SICOOB para a duplicata: " + idDuplicata.ToString());
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }




    }
}
