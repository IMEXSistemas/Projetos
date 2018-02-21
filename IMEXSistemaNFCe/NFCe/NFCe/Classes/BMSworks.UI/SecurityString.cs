using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

//Designed by Tim Shakarian ASP.NET MVP
//Reference: http://www.dotnetjunkies.com/HowTo/99201486-ACFD-4607-A0CC-99E75836DC72.dcik
//Comentários traduzidos por Cristóferson Bueno e código reduzido por Rafael Miranda

namespace BMSworks.UI
{
    public partial class SecurityString
    {
            /// <summary>
            /// Encripta uma string
            /// </summary>
            public string encrypt(string serializedQueryString)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(serializedQueryString);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = IV;
                return Convert.ToBase64String(
                    des.CreateEncryptor().TransformFinalBlock(
                        buffer,
                        0,
                        buffer.Length
                    )
                );
            }

            /// <summary>
            /// Descriptografa uma string
            /// </summary>
            public string decrypt(string encryptedQueryString)
            {
                
                    byte[] buffer = Convert.FromBase64String(encryptedQueryString);
                    TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                    MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                    des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                    des.IV = IV;
                    return Encoding.ASCII.GetString(
                        des.CreateDecryptor().TransformFinalBlock(
                            buffer,
                            0,
                            buffer.Length
                        )
                    );               
            }


            // A chave usada para geração da criptografia 
            // (Importante, esta chave não pode ser divulgada!)
            // Interessante mudar a mesma de tempos em tempos.
            private const string cryptoKey = "83aa4a4c554e861b2239b8a8b88bcbed"; //"ChangeThis!";

            // The Initialization Vector for the DES encryption routine
            // Inicialização do Vetor da rotina de DES criptografica 
            // -- Não sei dizer se ele está diretamente ligado a chave de criptografia
            // acredito que não. --
            private readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };
       
    }
}
