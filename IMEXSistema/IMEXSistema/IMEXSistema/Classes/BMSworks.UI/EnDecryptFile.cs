using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMSworks.Model;
using System.IO;
using BmsSoftware;
using System.Collections;
using System.Windows.Forms;

namespace BMSworks.UI.BMSworks.UI
{
    public partial class EnDecryptFile
    {

        public EMPRESAEntity DecryptFile()
        {
            EMPRESAEntity EMPRESAty = new EMPRESAEntity();
            try
            {
                //Local do arquivo
                string PathRegistro = ConfigSistema1.Default.PathInstall;
                //abrir o arquivo selecionado
                StreamReader objReader = new StreamReader(PathRegistro + @"\registro.dll", Encoding.Default);

                string sLine = "";
                ArrayList arrText = new ArrayList();

                //percorre o arquivo por linha
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    int Local = -1;
                    if (sLine != null) // localiza o valor "=" adicionado o valor a seguir na coleção
                    {
                        int LengthLine = sLine.Length;
                        Local = sLine.IndexOf("=");
                        Local++;
                        if (Local != -1)
                        {
                            int posFinal = LengthLine - Local;
                            string result = sLine.Substring(Local, posFinal);

                            arrText.Add(result);
                        }
                    }
                }

                objReader.Close();

                if (arrText.Count > 0)
                {
                    EMPRESAty.IDEMPRESA = 1;
                    EMPRESAty.NOMECLIENTE = arrText[1].ToString();
                    EMPRESAty.ENDERECO = arrText[2].ToString();
                    EMPRESAty.BAIRRO = arrText[3].ToString();
                    EMPRESAty.CEP = arrText[4].ToString();
                    EMPRESAty.CIDADE = arrText[5].ToString();
                    EMPRESAty.UF = arrText[6].ToString();
                    EMPRESAty.TELEFONE = arrText[7].ToString();
                    EMPRESAty.FAX = arrText[8].ToString();
                    EMPRESAty.CNPJCPF = arrText[9].ToString();
                    EMPRESAty.IE = arrText[10].ToString();
                    EMPRESAty.EMAIL = arrText[11].ToString();
                    EMPRESAty.REGISTRO = arrText[13].ToString();
                    EMPRESAty.NUMERO = arrText[14].ToString();
                    EMPRESAty.COMPLEMENTO = arrText[15].ToString();
                    EMPRESAty.NOMEFANTASIA = arrText[16].ToString();
                }
                else
                    EMPRESAty = null;

                return EMPRESAty;
            }
            catch (Exception)
            {

                MessageBox.Show("Erro no arquivo Registro.dll");
              
            }

            return EMPRESAty;
            
        }   
    }
}
