using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.Vendas;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using BMSworks.UI;
using System.Collections;


namespace BMSSoftware
{
    static class Program
    {
        static Utility Util = new Utility();

        static Boolean released = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);           

             FrmLogin FrmLogin = new FrmLogin();
               if (FrmLogin.ShowDialog() == DialogResult.OK)
               {
                   //Application.Run(new FrmPrincipal());
                   if (BmsSoftware.ConfigSistema1.Default.FlagEmissorNFe == "S")
                       Application.Run(new FrmEmissorNFe());
                   else
                     Application.Run(new FrmPrincipal2());
                
               }               
           
        }

        static void checkKeyBms()
        {
            //Descripta o arquivo para verificação de validade da senha
            DescKey();

            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";
            string PathSystem = @"C:\keybms.dll";

            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            string sLine = "";
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

                        if (result == "S")
                        {
                            released = true;
                            break;
                        }
                        else 
                            released = false;

                    }
                }
            }

            objReader.Close();

            //Descripta o arquivo para verificação de validade da senha
            EncriKey();
        }

        //Encripta o arquivo keybms.dll
        static void EncriKey()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";
            string PathSystem = @"C:\keybms.dll";


            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            ArrayList arrText = new ArrayList();
            string sLine = "";
            //percorre o arquivo por linha
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    arrText.Add(Util.ProtectString(sLine));

                }
            }

            //Fecha o arquivo
            objReader.Close();

            //Adiciona o texto
            if (arrText.Count > 0)
            {
                File.Delete(PathSystem);

                ///Abre o arquivo
                StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                valor.WriteLine(arrText[0].ToString());
                valor.WriteLine(arrText[1].ToString());
                //Fecha o arquivo
                valor.Close();
            }


        }


        //Descripta o arquivo keybms.dll
        static void DescKey()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";
            string PathSystem = @"C:\keybms.dll";

            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            ArrayList arrText = new ArrayList();
            string sLine = "";
             //percorre o arquivo por linha
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    try
                    {
                        arrText.Add(Util.UnprotectString(sLine));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Erro na leitura do arquivo de liberação!",
                                "IMEX Sistemas",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);


                        //Fecha o arquivo
                        objReader.Close();

                        break;

                        //deleta o arquivo de erro
                        File.Delete(PathSystem);
                    }
                   
                }
            }

            try
            {
                //Fecha o arquivo
                objReader.Close();

                //Adiciona o texto
                if (arrText.Count > 0)
                {
                    File.Delete(PathSystem);

                    ///Abre o arquivo
                    StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                    valor.WriteLine(arrText[0].ToString());
                    valor.WriteLine(arrText[1].ToString());
                    //Fecha o arquivo
                    valor.Close();
                }

            }
            catch (Exception)
            {
                
               
            }
          
        }

        static void NotesKeyBms()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";

            string PathSystem = @"C:\keybms.dll";

            //Verifica se existe o arquivo keybms.dll, caso não existe
            //o mesmo sera criado
            if (!File.Exists(PathSystem))
            {
                ///Abre o arquivo
                StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                valor.WriteLine(Util.ProtectString("Liberado=N"));

                //Adiciona 7 dias para o uso do sistema
                valor.WriteLine(Util.ProtectString("Fim=" + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy")));
 
                //Fecha o arquivo
                valor.Close();
            }
        }       
        
    }
}