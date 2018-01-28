using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg70
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public DateTime dataEmissao { get; set; }

        public string UnidadeFederacao { get; set; }

        public string Modelo { get; set; }

        public string Serie { get; set; }

        public string Subserie { get; set; }

        public int Numero { get; set; }

        public string CFOP { get; set; }

        public decimal valorTotal { get; set; }

        public decimal BaseCalculo { get; set; }

        public decimal ValorICMS { get; set; }

        public decimal Isenta_nao_tributada { get; set; }

        public decimal Outras { get; set; }

        public int Modalidade { get; set; }

        public string Situacao { get; set; }        


        public Reg70()
        {
            tipo = 70;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(cnpj, 14, false, '0');
            retorno += Funcoes.Formatar(inscricaoEstadual, 14, true, ' ');
            retorno += Funcoes.Formatar(dataEmissao.ToString("yyyyMMdd"), 8, true, '0');
            retorno += Funcoes.Formatar(UnidadeFederacao.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(Modelo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(Serie.ToString(), 1, true, '0');
            retorno += Funcoes.Formatar(Subserie.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(Numero.ToString(), 6, true, '0');
            retorno += Funcoes.Formatar(CFOP.ToString(), 4, true, '0');
            retorno += Funcoes.Formatar(valorTotal.ToString("n2"), 13, false, '0');
            retorno += Funcoes.Formatar(BaseCalculo.ToString("n2"), 14, false, '0');
            retorno += Funcoes.Formatar(ValorICMS.ToString("n2"), 14, false, '0');
            retorno += Funcoes.Formatar(Isenta_nao_tributada.ToString("n2"), 14, false, '0');
            retorno += Funcoes.Formatar(Outras.ToString("n2"), 14, false, '0');
            retorno += Funcoes.Formatar(Modalidade.ToString(), 1, true, '0');
            retorno += Funcoes.Formatar(Situacao.ToString(), 1, true, '0');
             
            
            
            return retorno;            
        }

    }
}
