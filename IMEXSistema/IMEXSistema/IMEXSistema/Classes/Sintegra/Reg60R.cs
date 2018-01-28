using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg60R
    {
        public int tipo { get; set; }
        public string subtipo { get; set; }
        public string MesAnoEmissão { get; set; }
        public string codigoProdutoServiço { get; set; }
        public decimal quantidade { get; set; }
        public decimal valorProdutoServiço { get; set; }
        public decimal baseCalculoICMS { get; set; }
        public string situacaoTributariaAliquotaProdutoServiço { get; set; } 


        public Reg60R()
        {
            tipo = 60;
            subtipo = "R";
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, false, '0');
            retorno += Funcoes.Formatar(subtipo, 1, false, '0');
            retorno += Funcoes.Formatar(MesAnoEmissão,  6, false, ' ');
            retorno += Funcoes.Formatar(codigoProdutoServiço, 14, false, '0');
            retorno += Funcoes.Formatar(quantidade.ToString("n3"), 13, false, '0');
            retorno += Funcoes.Formatar(valorProdutoServiço.ToString("n2"), 16, false, '0');
            retorno += Funcoes.Formatar(baseCalculoICMS.ToString("n2"), 16, false, '0');
            retorno += Funcoes.Formatar(situacaoTributariaAliquotaProdutoServiço, 4, false, ' ');
            
            return retorno;
        }
    }
}
