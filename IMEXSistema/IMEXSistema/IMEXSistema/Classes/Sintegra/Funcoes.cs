﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public static class Funcoes
    {
        public static string TirarAcento(string palavra)
        {
            string palavraSemAcento = "";
            string caracterComAcento = "áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ";
            string caracterSemAcento = "aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC";

            for (int i = 0; i < palavra.Length; i++)
            {
                if (caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1))) >= 0)
                {
                    int car = caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1)));
                    palavraSemAcento += caracterSemAcento.Substring(car, 1);
                }
                else
                {
                    palavraSemAcento += palavra.Substring(i, 1);
                }
            }

            return palavraSemAcento;
        }

        public static string Formatar(string texto, int tamanhoDesejado, bool acrescentarDireita, char caracterAcrescentar)
        {
            string textoRetorno = "";
            //if (string.IsNullOrWhiteSpace(texto))
            //  texto = "";

            // if (!string.IsNullOrWhiteSpace(texto))
            if (texto.TrimEnd() != string.Empty)
            {
                textoRetorno = removeFormatacao(texto);
                textoRetorno = TirarAcento(textoRetorno);
                textoRetorno = textoRetorno.ToUpper().Trim();
            }

            int tamanhoTexto = textoRetorno.Length;

            int qtdAcrescentar = tamanhoDesejado - tamanhoTexto;

            if (qtdAcrescentar < 0)
                qtdAcrescentar = 0;

            int posicaoInicial;

            if (tamanhoTexto >= tamanhoDesejado)
                posicaoInicial = tamanhoTexto - tamanhoDesejado;
            else
                posicaoInicial = 0;

            if (acrescentarDireita)
                textoRetorno = textoRetorno.Substring(0, (tamanhoDesejado > tamanhoTexto) ? tamanhoTexto : tamanhoDesejado) + stringOfChar(caracterAcrescentar, qtdAcrescentar);
            else
                textoRetorno = stringOfChar(caracterAcrescentar, qtdAcrescentar) +
                               textoRetorno.Substring(posicaoInicial, (tamanhoDesejado > tamanhoTexto) ? tamanhoTexto : tamanhoDesejado);

            return textoRetorno;
        }

        public static string removeFormatacao(string texto)
        {
            string txt = "";

            txt = texto.Replace(".", "");
            txt = txt.Replace("-", "");
            txt = txt.Replace("/", "");
            txt = txt.Replace("(", "");
            txt = txt.Replace(")", "");
            //txt = txt.Replace("&", "E");
            txt = txt.Replace("º", "");
            txt = txt.Replace("ª", "");
            txt = txt.Replace(",", "");

            return txt;
        }

        public static string stringOfChar(char caractere, int tamanho)
        {
            string Resultado = "";
            for (int i = 1; (i <= tamanho); i++)
            {
                Resultado = (Resultado + caractere);
            }
            return Resultado;
        }

    }
}
