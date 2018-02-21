﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BmsSoftware;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace BMSworks.UI
{
     

    public partial class ValidacoesLibrary
    {

        // [DllImport("DllInscE32.dll")]

        //public static extern int ValidaIE(string vInsc, string vUF);

        public static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");
            valor = valor.Trim();          
 
            if (valor.Length != 11)
                return false;
 
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
 
            if (igual || valor == "12345678909")
                return false;
 
            int[] numeros = new int[11];
 
            for (int i = 0; i < 11; i++)
              numeros[i] = int.Parse(
                valor[i].ToString());
 
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
 
            int resultado = soma % 11;
 
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
 
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
 
            resultado = soma % 11;
 
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;
 
            return true;
        }

        public static bool ValidaCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            CNPJ = CNPJ.Trim();

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaTipoPesquisa(string vrTipo, string VrPesquisa)
        {
            bool result = true;
            switch (vrTipo)
            {
                case "System.String":
                    break;
                case "System.Int32":
                    result = ValidaTipoInt32(VrPesquisa);
                    break;
                case "System.DateTime":
                    result = ValidaTipoDateTime(VrPesquisa);
                    break;
                case "System.Decimal":
                    result = ValidaTipoDecimal(VrPesquisa);
                    break;
            }

            return result;
        }

        public static bool ValidaTipoInt32(string vrPesquisa)
        {
            int qtde;
            if (int.TryParse(vrPesquisa, out qtde) == false)
                return false;
            else
                return true;
        }

        public static bool ValidaTipoDateTime(string vrPesquisa)
        {
            DateTime qtde;
            if (DateTime.TryParse(vrPesquisa, out qtde) == false)
                return false;
            else
                return true;
        }

        public static bool ValidaTipoHoraValida(string vrPesquisa)
        {
            Regex r = new Regex(@"([0-1][0-9]|2[0-3]):[0-5][0-9]");
            Match m = r.Match(vrPesquisa);
            return m.Success;
        }       

        public static bool ValidaTipoDecimal(string vrPesquisa)
        {
            Decimal qtde;
            if (Decimal.TryParse(vrPesquisa, out qtde) == false)
                return false;
            else
                return true;
        }

        public static bool ValidaTipoPorc(string vrPesquisa)
        {
            Boolean result = false;

            if (Convert.ToDecimal(vrPesquisa) > 100)
                result  = true;

            return result;
        }


        /// <summary>
        /// Convert string no formato de data para pesquisa
        /// </summary>
        public string ConverStringDateSearch(string ValueField)
        {
            ValueField = System.DateTime.Parse(ValueField).ToString(ConfigSistema1.Default.FormatDateSearch);
            return ValueField;
        }

        /// <summary>
        /// Valida codigo de barra padrao EAN13
        /// </
        public static bool ValidarEAN13(string CodigoEAN13)
        {
            bool result = (CodigoEAN13.Length == 13);
            if (result)
            {
                const string checkSum = "131313131313";

                int digito = int.Parse(CodigoEAN13[CodigoEAN13.Length - 1].ToString());
                string ean = CodigoEAN13.Substring(0, CodigoEAN13.Length - 1);

                int sum = 0;
                for (int i = 0; i <= ean.Length - 1; i++)
                {
                    sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
                }
                int calculo = 10 - (sum % 10);
                result = (digito == calculo);
            }
            return result;
        }

    }
}
