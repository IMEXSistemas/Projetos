using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg60M
    {
        public int tipo { get; set; }
        public string subtipo { get; set; }
        public DateTime dataEmissao { get; set; }
        public string numeroSerieFabricacao { get; set; }
        public string numeroOrdemSequencialEquipamento { get; set; }
        public string modeloDocumentoFiscal { get; set; }
        public string numeroContadorOrdemOperacaoInicioDia  { get; set; }
        public string numeroContadorOrdemOperacaoFinalDia { get; set; }
        public string numeroContadorReducaoZ { get; set; }
        public string contadorReinicioOperacao { get; set; }
        public decimal valorVendaBruta { get; set; }
        public decimal valorTotalizadorGeralEquipamento { get; set; }
        public string brancos { get; set; }

        //Registro 60A
        public int tipoA { get; set; }
        public string subtipoA { get; set; }
        public DateTime dataEmissaoA { get; set; }
        public string numeroSerieFabricacaoA { get; set; }
        public string situacaoTributariaAliquotaA { get; set; }
        public decimal valorAcumuladoTotalizadorParcialA { get; set; }   
        

        //Registro 60D
        public int tipoD { get; set; }
        public string subtipoD { get; set; }
        public DateTime dataEmissaoD { get; set; }
        public string numeroSerieFabricacaoD { get; set; }
        public string codigoProdutoServiçoD { get; set; }
        public decimal quantidadeD { get; set; }
        public decimal valorMercadoriaProdutoServiçoD { get; set; }
        public decimal baseCalculoICMSD { get; set; }
        public string situacaoTributariaAliquotaProdutoServiçoD { get; set; }
        public decimal ValorICMSD { get; set; }
        public string brancosD { get; set; }


        //Registro 60I
        public int tipoI { get; set; }
        public string subtipoI { get; set; }
        public DateTime dataEmissaoI { get; set; }
        public string numeroSerieFabricacaoI { get; set; }
        public string modeloDocumentoFiscalI { get; set; }
        public string nOrdemDocumentoFiscalI { get; set; }
        public string numeroItemI { get; set; }
        public string codigoProdutoServiçoI { get; set; }
        public decimal quantidadeI { get; set; }
        public decimal valorUnitarioProdutoI { get; set; }
        public decimal baseCalculoICMSI { get; set; }
        public string situacaoTributariaAliquotaProdutoServiçoI { get; set; }
        public decimal ValorICMSI { get; set; }

        public Reg60M()
        {
            brancos = Funcoes.Formatar(string.Empty, 37, true, ' ');
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            //Registro 60M
            if (subtipo == "M")
            {
                retorno += Funcoes.Formatar(tipo.ToString(), 2, false, '0');
                retorno += Funcoes.Formatar(subtipo, 1, false, '0');
                retorno += Funcoes.Formatar(dataEmissao.ToString("yyyyMMdd"), 8, false, ' ');
                retorno += Funcoes.Formatar(numeroSerieFabricacao, 20, false, '0');
                retorno += Funcoes.Formatar(numeroOrdemSequencialEquipamento.ToString(), 3, true, '0');
                retorno += Funcoes.Formatar(modeloDocumentoFiscal, 2, true, '0');
                retorno += Funcoes.Formatar(numeroContadorOrdemOperacaoInicioDia, 6, true, '0');
                retorno += Funcoes.Formatar(numeroContadorOrdemOperacaoFinalDia, 6, true, '0');
                retorno += Funcoes.Formatar(numeroContadorReducaoZ, 6, true, '0');
                retorno += Funcoes.Formatar(contadorReinicioOperacao, 3, true, '0');
                retorno += Funcoes.Formatar(valorVendaBruta.ToString("n2"), 16, false, '0');
                retorno += Funcoes.Formatar(valorTotalizadorGeralEquipamento.ToString("n2"), 16, false, '0');
                retorno += Funcoes.Formatar(brancos, 37, true, ' ');
            }

            //Registro 60A
            if (subtipoA == "A")
            {
                retorno += Funcoes.Formatar(tipoA.ToString(), 2, false, '0');
                retorno += Funcoes.Formatar(subtipoA, 1, false, '0');
                retorno += Funcoes.Formatar(dataEmissaoA.ToString("yyyyMMdd"), 8, false, ' ');
                retorno += Funcoes.Formatar(numeroSerieFabricacaoA, 20, false, '0');
                retorno += Funcoes.Formatar(situacaoTributariaAliquotaA, 4, true, ' ');
                retorno += Funcoes.Formatar(valorAcumuladoTotalizadorParcialA.ToString("n2"), 12, false, '0');
                retorno += Funcoes.Formatar(brancos, 79, true, ' ');
            }

            if (subtipoD == "D")
            {
                retorno += Funcoes.Formatar(tipoD.ToString(), 2, false, '0');
                retorno += Funcoes.Formatar(subtipoD, 1, false, '0');
                retorno += Funcoes.Formatar(dataEmissaoD.ToString("yyyyMMdd"), 8, false, ' ');
                retorno += Funcoes.Formatar(numeroSerieFabricacaoD, 20, false, '0');
                retorno += Funcoes.Formatar(codigoProdutoServiçoD, 14, false, '0');
                retorno += Funcoes.Formatar(quantidadeD.ToString("n3"), 13, false, '0');
                retorno += Funcoes.Formatar(valorMercadoriaProdutoServiçoD.ToString("n2"), 16, false, '0');
                retorno += Funcoes.Formatar(baseCalculoICMSD.ToString("n2"), 16, false, '0');
                retorno += Funcoes.Formatar(situacaoTributariaAliquotaProdutoServiçoD, 4, false, ' ');
                retorno += Funcoes.Formatar(ValorICMSD.ToString("n2"), 13, false, '0');
                retorno += Funcoes.Formatar(brancosD, 19, true, ' ');
            }

            if (subtipoI == "I")
            {
                retorno += Funcoes.Formatar(tipoI.ToString(), 2, false, '0');
                retorno += Funcoes.Formatar(subtipoI, 1, false, '0');
                retorno += Funcoes.Formatar(dataEmissaoI.ToString("yyyyMMdd"), 8, false, ' ');
                retorno += Funcoes.Formatar(numeroSerieFabricacaoI, 20, false, '0');
                retorno += Funcoes.Formatar(modeloDocumentoFiscalI, 2, false, '0');
                retorno += Funcoes.Formatar(nOrdemDocumentoFiscalI, 6, false, '0');
                retorno += Funcoes.Formatar(numeroItemI, 3, false, '0');
                retorno += Funcoes.Formatar(codigoProdutoServiçoI, 14, false, '0');
                retorno += Funcoes.Formatar(quantidadeI.ToString("n3"), 13, false, '0');
                retorno += Funcoes.Formatar(valorUnitarioProdutoI.ToString("n2"), 13, false, '0');
                retorno += Funcoes.Formatar(baseCalculoICMSI.ToString("n2"), 12, false, '0');
                retorno += Funcoes.Formatar(situacaoTributariaAliquotaProdutoServiçoI, 4, false, ' ');
                retorno += Funcoes.Formatar(ValorICMSI.ToString("n2"), 12, false, '0');
                retorno += Funcoes.Formatar(brancos, 16, true, ' ');
            }
           

            return retorno;
        }
    }
}
