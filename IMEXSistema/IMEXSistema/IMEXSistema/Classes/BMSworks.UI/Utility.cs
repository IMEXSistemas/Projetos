//Template gerado utilizando o MyGeneration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;
using BMSSoftware;
using System.Drawing;
using BmsSoftware;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using BMSworks.Model;
using BMSworks.Firebird;
using System.IO;
using System.Text.RegularExpressions;
using BmsSoftware.Modulos.Operacional;
using System.Net;
using BmsSoftware.UI;
using Newtonsoft.Json.Linq;

namespace BMSworks.UI
{
    public partial class Utility
    {
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        MUNICIPIOSProvider MUNICIPIOSProvider = new MUNICIPIOSProvider();
        MUNICIPIOSEntity MUNICIPIOSTy = new MUNICIPIOSEntity();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        ///CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();

        /// <summary>
        /// Preenche drop com os tipo de pesquisas
        /// </summary>
        public DataTable GetSearchType()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("nomecampo", typeof(string)));
            list.Columns.Add(new DataColumn("tipocampo", typeof(string)));
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows.Add(list.NewRow());
            list.Rows[0][0] = "Contém";
            list.Rows[0][1] = "like";
            list.Rows[1][0] = "Igual";
            list.Rows[1][1] = "=";          
            list.Rows[2][0] = "Maior";
            list.Rows[2][1] = ">";
            list.Rows[3][0] = "Menor";
            list.Rows[3][1] = "<";
            list.Rows[4][0] = "Maior/Igual";
            list.Rows[4][1] = ">=";
            list.Rows[5][0] = "Menor/Igual";
            list.Rows[5][1] = "<=";
            list.Rows[6][0] = "Diferente";
            list.Rows[6][1] = "<>";
            list.Rows[7][0] = "Todos";
            list.Rows[7][1] = "";

            return list;
        }      

        //Retorna o tipo da celula / Integer/ Decimal/ String ou Date
        public string GetTypeCell(string FullName)
        {
            string result = FullName;
            int Local = FullName.IndexOf(",");
            int posFinal = Local - 19;
            result = FullName.Substring(19, posFinal);
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
        /// Convert string no formato de Numeric para pesquisa
        /// </summary>
        public string ConverStringDecimalSearch(string ValueField)
        {
            //ValueField = System.DateTime.Parse(ValueField).ToString(ConfigSistema1.Default.FormatDateSearch);
            ValueField = ValueField.Replace(".", "");
            ValueField = ValueField.Replace(",", ".");
            return ValueField;
        }

        /// <summary>
        /// Retira todas as letras deixando somente numeros
        /// </summary>
        public string RetiraLetras(string texto)
        {
            string textor = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].ToString() == "0") textor += texto[i].ToString();
                else if (texto[i].ToString() == "1") textor += texto[i].ToString();
                else if (texto[i].ToString() == "2") textor += texto[i].ToString();
                else if (texto[i].ToString() == "3") textor += texto[i].ToString();
                else if (texto[i].ToString() == "4") textor += texto[i].ToString();
                else if (texto[i].ToString() == "5") textor += texto[i].ToString();
                else if (texto[i].ToString() == "6") textor += texto[i].ToString();
                else if (texto[i].ToString() == "7") textor += texto[i].ToString();
                else if (texto[i].ToString() == "8") textor += texto[i].ToString();
                else if (texto[i].ToString() == "9") textor += texto[i].ToString();
            }

            return textor;
        }      

        /// <summary>
        /// Seleciona o icone de cadastro
        /// </summary>
        public Image GetAddressImage(int TypeButton)
        {
            Image ImageSelect = null;
            switch (TypeButton)
            {
                case 0:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Grava.png");
                    break;
                case 1:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Novo.png");
                    break;
                case 2:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Delete.png"); ;
                    break;
                case 3:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Filtros.png");
                    break;
                case 4:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Print.png");
                    break;
                case 5:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\Exit.png");
                    break;
                case 6:

                    if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnsoma16.png"))
                        ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnsoma16.png");
                    else
                        ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\icoNovaTarefa.gif");
                    break;
                case 7:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\maquina.gif"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\maquina.gif");
                        else
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\maquina.png");
                    }
                    break;
                case 8:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\retrato.png");
                    break;
                case 9:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\paisagem.png");
                    break;
                case 10:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\calendario.png");
                    break;
                  case 11:
                    if (File.Exists(ConfigSistema1.Default.PathImage + @"\calendar16.png"))
                        ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\calendar16.png");
                    else
                        ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\calendar.png");
                    break;
                  case 12:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\key2.png");
                    break; 
                  case 13:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\locaMaquina.png");
                    break;
                  case 14:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\folder.gif"))
                             ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\folder.gif");
                        else
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\folder.png");
                       
                        break;
                    }
                  case 15:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnadd16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnadd16.png");
                        break;
                    }
                    case 16:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnlimpa16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnlimpa16.png");
                        break;
                    }
                    case 17:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnpdf16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnpdf16.png");

                        break;
                    }
                    case 18:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnexcel16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnexcel16.png");

                        break;
                    }
                    case 19:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnprint16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnprint16.png");

                        break;
                    }
                    case 20:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnlocalizar16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnlocalizar16.png");

                        break;
                    }
                    case 21:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\btnexit16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\btnexit16.png");
                        break;
                    }
                    case 22:
                    {
                        if (File.Exists(ConfigSistema1.Default.PathImage + @"\calendar16.png"))
                            ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\calendar16.png");

                    }
                     break; 
                    

            }

            return ImageSelect;
        }

        public Image GetAddressImageMain(int TypeButton)
        {
            Image ImageSelect = null;
            switch (TypeButton)
            {
                case 0:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuCadastro.png");
                    break;
                case 1:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuVendas.png");
                    break;
                case 2:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuServico.png"); ;
                    break;
                case 3:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuFinanceiro.png");
                    break;
                case 4:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuOperacional.png");
                    break;
                case 5:
                    ImageSelect = Bitmap.FromFile(ConfigSistema1.Default.PathImage + @"\MenuSair.png");
                    break;
            }

            return ImageSelect;
        }

        /// <summary>
        /// Seleciona o texto do ToolTipText icone de cadastro
        /// </summary>
        public string GetToolTipButton(int TypeButton)
        {
            string TextToolTip = string.Empty;
            switch (TypeButton)
            {
                case 0:
                    TextToolTip = "Salva (F3)";
                    break;
                case 1:
                    TextToolTip = "Novo (F4)";
                    break;
                case 2:
                    TextToolTip = "Apagar (F5)";
                    break;
                case 3:
                    TextToolTip = "Pesquisar (F6)";
                    break;
                case 4:
                    TextToolTip = "Relatório";
                    break;
                case 5:
                    TextToolTip = "Voltar (Esc)";
                    break;
                case 6:
                    TextToolTip = "Consulta/Cadastra";
                    break;
                case 7:
                    TextToolTip = "Feriado";
                    break;
            }

            return TextToolTip;
        }

        public string GetToolTipMain(int TypeButton)
        {
            string TextToolTip = string.Empty;
            switch (TypeButton)
            {
                case 0:
                    TextToolTip = "Menu Cadastros";
                    break;
                case 1:
                    TextToolTip = "Menu Vendas";
                    break;
                case 2:
                    TextToolTip = "Menu Serviços";
                    break;
                case 3:
                    TextToolTip = "Menu Financeiro";
                    break;
                case 4:
                    TextToolTip = "Menu Operacional";
                    break;
                case 5:
                    TextToolTip = "Sair";
                    break;
            }

            return TextToolTip;
        }

       
        /// <summary>
        ///Limitar o tamanho do texto
        /// </summary>
        public string LimiterText(string TextPrint, int LengthText)
        {
            if (TextPrint != string.Empty)
                TextPrint = TextPrint.Substring(0, Math.Min(LengthText, TextPrint.Length));
            return TextPrint;
        }

        /// <summary>
        ///Centraliza Texto
        /// </summary>
        
        public string centeredString(string s, int width)
        {
            try
            {
                if (s.Length >= width)
                {
                    return s;
                }

                int leftPadding = (width - s.Length) / 2;
                int rightPadding = width - s.Length - leftPadding;

                return new string(' ', leftPadding) + s + new string(' ', rightPadding);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return s;
            }
        }



        /// <summary>
        /// método para criptografar uma string qualquer para o formato MD5W 
        /// </summary>
        public string GetCrypMD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Quebra a string para melhor visualização na impressão
        /// </summary>
        public string QuebraString(string Texto, int limite)
        {
            string result = string.Empty;
            string Quebralinha = string.Empty;
            int j = 0;
            for (int i = 0; i < Texto.Length; i++)
            {
                if (j >= limite && Texto[i].ToString() == " ")
                {
                    result = result.TrimEnd();
                    result += "\n ";
                    j = 0;
                    i--;
                }
                else
                {
                    Quebralinha = Texto[i].ToString();
                    if (Quebralinha == "\n")
                        j = limite;

                    result += Texto[i].ToString();
                    j++;
                }
            }

            return result;
        }

        /// <summary>
        ///   //Removendo aspas duplas e aspas inglesas de um texto
        /// </summary>
        public string RemoverAspas(string texto)
        {
            string s = Regex.Replace(texto, @"[\u2018\u2019\u201a\u201b\u0022\u201c\u201d\u201e\u201f\u301d\u301e\u301f]", "");

            return s.ToString();
        }


        /// <summary>
        /// Remove acentos para impressão em matricial
        /// </summary>
        public string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// encripta texto para segurança da informação
        /// </summary>
        public string ProtectString(string Texto)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Texto);
            byte[] protectedPassword = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedPassword);
           
        }
         /// <summary>
        /// Desencripta o texto
        /// </summary>
        public  string UnprotectString(string Texto)
        {
            byte[] bytes = Convert.FromBase64String(Texto);
            byte[] password = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(password);
        }

        int cols;
        /// <summary>
        /// Importar Informações do grid para excel
        /// </summary>
        public void ImportGridExcel(string NomeArquivo, DataGridView DataGridName, Boolean Cabecalho)
        {
            //Abre o Arquivo
            StreamWriter wr = new StreamWriter(ConfigSistema1.Default.PathInstall + @"\ArquivoExcel\" + NomeArquivo + ".xls", false, System.Text.Encoding.GetEncoding("ISO-8859-1"));

            //Determinar o número de colunas e escreve colunas para o arquivo
            cols = DataGridName.Columns.Count;
            if (Cabecalho)
            {
                for (int i = 0; i < cols; i++)
                {
                    wr.Write(DataGridName.Columns[i].HeaderText.ToString().ToUpper() + "\t");
                }

                wr.WriteLine();
            }

            // Escreve linhas de arquivo excel
            for (int i = 0; i < (DataGridName.Rows.Count - 1); i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (DataGridName.Rows[i].Cells[j].Value != null)
                        wr.Write(DataGridName.Rows[i].Cells[j].Value + "\t");
                    else
                    {
                        wr.Write("\t");
                    }
                }

                wr.WriteLine();
            }

            //Fecha o arquivo
            wr.Close();

            //abre o arquivo excel após a sua criação
            System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(ConfigSistema1.Default.PathInstall + @"\ArquivoExcel\" + NomeArquivo + ".xls");


        }

        /// <summary>
        /// Verifica a liberação da tela para o usuário
        /// </summary>
        public Boolean Acessa_Tela(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGACESSA == "S")
                    result = true;
                else
                {
                    result = false;
                    MessageBox.Show(ConfigMessage.Default.MsgPerm,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);                    
                    
                }
            }
            return result;
        }

        /// <summary>
        /// Verifica a liberação da tela para o usuário sem mensagem
        /// </summary>
        public Boolean Acessa_Tela2(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGACESSA == "S")
                    result = true;
                else
                    result = false;

            }
            return result;
        }

        /// <summary>
        /// Verifica a liberação de gravação de registro
        /// </summary>
        public Boolean Grava_Registro(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGALTERA == "S")
                    result = true;
                else
                {
                    result = false;
                    MessageBox.Show(ConfigMessage.Default.MsgPerm,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                }
            }
            return result;
        }

        /// <summary>
        /// Verifica a liberação de Apaga de registro
        /// </summary>
        public Boolean Apaga_RegistroSenha(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGAPAGA == "S")
                    result = true;
                else
                {
                    using (FrmLoginTela frm = new FrmLoginTela())
                    {
                        frm.NomeFormulario = NomeFormulario;
                        frm.IDNIVEL = IDNIVEL;
                        frm.ShowDialog();
                        result = frm.Retorno;
                    }

                   
                   if(!result)
                    MessageBox.Show(ConfigMessage.Default.MsgPerm,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                }
            }
            return result;
        }
      

        /// <summary>
        /// Verifica a liberação de Apaga de registro
        /// </summary>
        public Boolean Apaga_Registro(string NomeFormulario, int IDNIVEL)
        {
            Boolean result = true;

            RowsFiltroCollection RowFiltroAcess = new RowsFiltroCollection();
            RowFiltroAcess.Add(new RowsFiltro("IDNIVEL", "System.Int32", "=", IDNIVEL.ToString(), "and"));
            RowFiltroAcess.Add(new RowsFiltro("NOMEFORMULARIO", "System.String", "=", NomeFormulario));

            LIS_CONTROLEACESSOCollection LIS_CONTROLEACESSOColl = new LIS_CONTROLEACESSOCollection();
            LIS_CONTROLEACESSOProvider LIS_CONTROLEACESSOP = new LIS_CONTROLEACESSOProvider();
            LIS_CONTROLEACESSOColl = LIS_CONTROLEACESSOP.ReadCollectionByParameter(RowFiltroAcess);

            if (LIS_CONTROLEACESSOColl.Count > 0)
            {
                if (LIS_CONTROLEACESSOColl[0].FLAGAPAGA == "S")
                    result = true;
                else
                {
                    result = false;
                    MessageBox.Show(ConfigMessage.Default.MsgPerm,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                }
            }
            return result;
        }


        /// <summary>
        /// Remove Caracteres Especiais
        /// </summary>
        public String[] RemoveSpecialCharacters(String[] texto)
        {
            String[] retorno = new string[texto.Length];
            string  normalizedString = string.Empty;

            for (int i = 0; i < texto.Length; i++)
            {

                normalizedString = texto[i].ToString();
                normalizedString = normalizedString.ToLower();
     
                // Prepara a tabela de símbolos.
                var symbolTable = new Dictionary<char, char[]>();
                symbolTable.Add('a', new char[] { 'à', 'á', 'ä', 'â', 'ã' });
                symbolTable.Add('c', new char[] { 'ç' });
                symbolTable.Add('e', new char[] { 'è', 'é', 'ë', 'ê' });
                symbolTable.Add('i', new char[] { 'ì', 'í', 'ï', 'î' });
                symbolTable.Add('o', new char[] { 'ò', 'ó', 'ö', 'ô', 'õ' });
                symbolTable.Add('u', new char[] { 'ù', 'ú', 'ü', 'û' });
                symbolTable.Add(' ', new char[] { 'º', '´' });

                
                // Substitui os símbolos.
                foreach (var key in symbolTable.Keys)
                {
                    foreach (var symbol in symbolTable[key])
                    {
                        normalizedString = normalizedString.Replace(symbol, key);
                    }
                }
                             
                normalizedString = normalizedString.ToUpper();
                retorno[i] = normalizedString;

            }

            return retorno;
        }

        public string TiraCaracterEspecial(string valor)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char[] nome = valor.ToCharArray();

            foreach (char letra in nome)
            {
                //aceita somente letras do alfabeto, números e espaço
                if (Regex.IsMatch(letra.ToString(), @"^[a-zA-Z0-9 ]+$"))
                    stringBuilder.Append(letra);
            }

            return stringBuilder.ToString();
        }



        public decimal EstoqueAtual(int idproduto, Boolean Fiscal)
        {
            decimal result = 0;
            PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
          
            if (BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque == "N")
            {
                PRODUTOSTY = PRODUTOSP.Read(idproduto);

                if (!Fiscal)
                {
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.TOTALMT > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.TOTALMT + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos.Trim() == "S")
                    {
                        //Estoque  pedido Festa
                        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();
                        LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTOS", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem.Trim() == "S")
                    {
                        //Estoque Manutenção
                        LIS_PRODUTOSMANUTCollection LIS_PRODUTOSMANUTColl = new LIS_PRODUTOSMANUTCollection();
                        LIS_PRODUTOSMANUTProvider LIS_PRODUTOSMANUTP = new LIS_PRODUTOSMANUTProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        LIS_PRODUTOSMANUTColl = LIS_PRODUTOSMANUTP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
                        {
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                        }
                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
                    {

                        //Estoque Pedido Otica
                        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
                        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDOTICAP = new LIS_PRODUTOSPEDOTICAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }


                    //Estoque  pedido de Venda   MT2
                    LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                    LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                    {

                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDA));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }



                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
                    LIS_PRODUTOSPEDMARCProvider LIS_PRODUTOSPEDMARCP = new LIS_PRODUTOSPEDMARCProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Ordem de Serviço Produto -  Produtos
                    LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
                    LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();
                    LIS_PRODUTOPEDMARC2Provider LIS_PRODUTOPEDMARC2P = new LIS_PRODUTOPEDMARC2Provider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2P.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOPEDMARC2Entity item in LIS_PRODUTOPEDMARC2Coll)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANT);
                    }



                    //Estoque pelo Ordem de Serviço Produto -  Produtos MT2
                    LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
                    LIS_PRODUTOSPEDIDOMTQOSProvider LIS_PRODUTOSPEDIDOMTQOSP = new LIS_PRODUTOSPEDIDOMTQOSProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOSPEDIDOMTQOSColl = LIS_PRODUTOSPEDIDOMTQOSP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQOSEntity item in LIS_PRODUTOSPEDIDOMTQOSColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }
                }


                //Movimentacao de Estoque
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));

                if (Fiscal)
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {
                    //Somente movimentação de entrada
                    if (item.IDTIPOMOVIMENTACAO == 1)
                        result += Convert.ToDecimal(item.QUANTIDADE);
                }
               
                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENF.TrimEnd() == "S")
                {
                    LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                    LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("flagenviada", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("idproduto", "System.String", "=", idproduto.ToString()));
                    LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE DESC");
                    NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                    {
                        if (NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGCANCELADA.TrimEnd() == "N" && NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGENVIADA.TrimEnd() == "S")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }


                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENFCE.TrimEnd() == "S")
                {
                    //Cupom Eletronico   NFCe Nota Fiscal de Consumidor Eletronico         
                    LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                    LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "1")); //Enviado
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString())); 
                    
                    LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);
                    CUPOMProvider CUPOMProvider = new CUPOMProvider();
                    foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
                    {
                        result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }

            }

            if (PRODUTOSTY.FLAGCONTROLAESTOQUE != null)
            {
                if (PRODUTOSTY.FLAGCONTROLAESTOQUE.Trim() == "S")
                    result = 0;
            }

            return result;
        }

        public decimal EstoqueAtual(int idproduto, string Data, Boolean Fiscal)
        {
           // Utility Util = new Utility();

            PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

            decimal result = 0;

            if (BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque == "N")
            {
                PRODUTOSTY = PRODUTOSP.Read(idproduto);

                if (!Fiscal)
                {
                    //Estoque pelo Pedido Normal   e pedido de Venda   
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.TOTALMT > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.TOTALMT + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos.Trim() == "S")
                    {
                        //Estoque  pedido Festa
                        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();
                        LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTOS", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                        RowRelatorio.Add(new RowsFiltro("flagorcamento", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem.Trim() == "S")
                    {
                        //Estoque Manutenção
                        LIS_PRODUTOSMANUTCollection LIS_PRODUTOSMANUTColl = new LIS_PRODUTOSMANUTCollection();
                        LIS_PRODUTOSMANUTProvider LIS_PRODUTOSMANUTP = new LIS_PRODUTOSMANUTProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("DATAMANUT", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                        LIS_PRODUTOSMANUTColl = LIS_PRODUTOSMANUTP.ReadCollectionByParameter(RowRelatorio);
                        
                        foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
                        {
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                        }
                    }


                    if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
                    {

                        //Estoque Pedido Otica
                        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
                        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDOTICAP = new LIS_PRODUTOSPEDOTICAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }


                    //Estoque  pedido de Venda   MT2
                    LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                    LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                    {

                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDA));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }



                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
                    LIS_PRODUTOSPEDMARCProvider LIS_PRODUTOSPEDMARCP = new LIS_PRODUTOSPEDMARCProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();
                    LIS_PRODUTOPEDMARC2Provider LIS_PRODUTOPEDMARC2P = new LIS_PRODUTOPEDMARC2Provider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("dtemissao", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2P.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOPEDMARC2Entity item in LIS_PRODUTOPEDMARC2Coll)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANT);
                    }

                    //Estoque pelo Ordem de Serviço Produto -  Produtos
                    LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
                    LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Ordem de Serviço Produto -  Produtos MT2
                    LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
                    LIS_PRODUTOSPEDIDOMTQOSProvider LIS_PRODUTOSPEDIDOMTQOSP = new LIS_PRODUTOSPEDIDOMTQOSProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DATAEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOMTQOSColl = LIS_PRODUTOSPEDIDOMTQOSP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQOSEntity item in LIS_PRODUTOSPEDIDOMTQOSColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                }

                //Movimentacao de Estoque
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", ConverStringDateSearch(Data)));

                if (Fiscal)
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {

                    //Somente movimentação de entrada
                    if (item.IDTIPOMOVIMENTACAO == 1)
                        result += Convert.ToDecimal(item.QUANTIDADE);

                }

                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENF.TrimEnd() == "S")
                {
                    LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                    LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("flagenviada", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("idproduto", "System.String", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));

                    LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE DESC");
                    NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                    {
                        if (NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGCANCELADA.TrimEnd() == "N" && NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGENVIADA.TrimEnd() == "S")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }


                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENFCE.TrimEnd() == "S")
                {
                    //Cupom Eletronico            
                    LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                    LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "1")); //Enviado
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(Data)));
                    LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
                    {
                        result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }

            }

            if (PRODUTOSTY.FLAGCONTROLAESTOQUE != null)
            {
                if(PRODUTOSTY.FLAGCONTROLAESTOQUE.Trim() == "S")
                    result = 0;
            }

            return result;
        }


        public decimal EstoqueAtual(int idproduto, string DataInicial, string DataFinal,  Boolean Fiscal)
        {
            // Utility Util = new Utility();

            PRODUTOSEntity PRODUTOSTY = new PRODUTOSEntity();
            PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

            decimal result = 0;

            if (BmsSoftware.ConfigSistema1.Default.FlagControlaEstoque == "N")
            {
                PRODUTOSTY = PRODUTOSP.Read(idproduto);

                if (!Fiscal)
                {
                    //Estoque pelo Pedido Normal   e pedido de Venda   
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.TOTALMT > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.TOTALMT + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos.Trim() == "S")
                    {
                        //Estoque  pedido Festa
                        LIS_PRODUTOSPEDFESTACollection LIS_PRODUTOSPEDFESTAColl = new LIS_PRODUTOSPEDFESTACollection();
                        LIS_PRODUTOSPEDFESTAProvider LIS_PRODUTOSPEDFESTAP = new LIS_PRODUTOSPEDFESTAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTOS", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                        RowRelatorio.Add(new RowsFiltro("flagorcamento", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDFESTAColl = LIS_PRODUTOSPEDFESTAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDFESTAEntity item in LIS_PRODUTOSPEDFESTAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }

                    if (BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem.Trim() == "S")
                    {
                        //Estoque Manutenção
                        LIS_PRODUTOSMANUTCollection LIS_PRODUTOSMANUTColl = new LIS_PRODUTOSMANUTCollection();
                        LIS_PRODUTOSMANUTProvider LIS_PRODUTOSMANUTP = new LIS_PRODUTOSMANUTProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("DATAMANUT", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                        RowRelatorio.Add(new RowsFiltro("DATAMANUT", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));

                        LIS_PRODUTOSMANUTColl = LIS_PRODUTOSMANUTP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSMANUTEntity item in LIS_PRODUTOSMANUTColl)
                        {
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                        }
                    }


                    if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
                    {

                        //Estoque Pedido Otica
                        LIS_PRODUTOSPEDOTICACollection LIS_PRODUTOSPEDOTICAColl = new LIS_PRODUTOSPEDOTICACollection();
                        LIS_PRODUTOSPEDOTICAProvider LIS_PRODUTOSPEDOTICAP = new LIS_PRODUTOSPEDOTICAProvider();

                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                        RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                        RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                        LIS_PRODUTOSPEDOTICAColl = LIS_PRODUTOSPEDOTICAP.ReadCollectionByParameter(RowRelatorio);

                        foreach (LIS_PRODUTOSPEDOTICAEntity item in LIS_PRODUTOSPEDOTICAColl)
                        {

                            if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            {
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            }

                        }
                    }

                    //Estoque  pedido de Venda   MT2
                    LIS_PRODUTOSPEDIDOMTQCollection LIS_PRODUTOSPEDIDOMTQColl = new LIS_PRODUTOSPEDIDOMTQCollection();
                    LIS_PRODUTOSPEDIDOMTQProvider LIS_PRODUTOSPEDIDOMTQP = new LIS_PRODUTOSPEDIDOMTQProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOMTQColl = LIS_PRODUTOSPEDIDOMTQP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQEntity item in LIS_PRODUTOSPEDIDOMTQColl)
                    {

                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDA));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOSPEDMARCCollection LIS_PRODUTOSPEDMARCColl = new LIS_PRODUTOSPEDMARCCollection();
                    LIS_PRODUTOSPEDMARCProvider LIS_PRODUTOSPEDMARCP = new LIS_PRODUTOSPEDMARCProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDMARCColl = LIS_PRODUTOSPEDMARCP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDMARCEntity item in LIS_PRODUTOSPEDMARCColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Pedido MT3
                    LIS_PRODUTOPEDMARC2Collection LIS_PRODUTOPEDMARC2Coll = new LIS_PRODUTOPEDMARC2Collection();
                    LIS_PRODUTOPEDMARC2Provider LIS_PRODUTOPEDMARC2P = new LIS_PRODUTOPEDMARC2Provider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                    LIS_PRODUTOPEDMARC2Coll = LIS_PRODUTOPEDMARC2P.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOPEDMARC2Entity item in LIS_PRODUTOPEDMARC2Coll)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANT);
                    }

                    //Estoque pelo Ordem de Serviço Produto -  Produtos
                    LIS_PRODUTOOSFECHCollection LIS_PRODUTOOSFECHColl = new LIS_PRODUTOOSFECHCollection();
                    LIS_PRODUTOOSFECHProvider LIS_PRODUTOOSFECHP = new LIS_PRODUTOOSFECHProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOOSFECHColl = LIS_PRODUTOOSFECHP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOOSFECHEntity item in LIS_PRODUTOOSFECHColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }

                    //Estoque pelo Ordem de Serviço Produto -  Produtos MT2
                    LIS_PRODUTOSPEDIDOMTQOSCollection LIS_PRODUTOSPEDIDOMTQOSColl = new LIS_PRODUTOSPEDIDOMTQOSCollection();
                    LIS_PRODUTOSPEDIDOMTQOSProvider LIS_PRODUTOSPEDIDOMTQOSP = new LIS_PRODUTOSPEDIDOMTQOSProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));

                    LIS_PRODUTOSPEDIDOMTQOSColl = LIS_PRODUTOSPEDIDOMTQOSP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTOSPEDIDOMTQOSEntity item in LIS_PRODUTOSPEDIDOMTQOSColl)
                    {
                        if (item.FLAGORCAMENTO.TrimEnd() == "N")
                        {
                            if (item.FLAGBAIXAESTMT == "N")
                                result -= Convert.ToDecimal(item.QUANTIDADE);
                            else
                            {
                                if (item.MT2 > 0)
                                    result -= Convert.ToDecimal(item.QUANTIDADE * (item.MT2 + item.TOTALPERDAMT));
                                else
                                    result -= Convert.ToDecimal(item.QUANTIDADE);
                            }
                        }

                    }

                }

                //Movimentacao de Estoque
                LIS_MOVPRODUTOESCollection LIS_MOVPRODUTOESColl = new LIS_MOVPRODUTOESCollection();
                LIS_MOVPRODUTOESProvider LIS_MOVPRODUTOESP = new LIS_MOVPRODUTOESProvider();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                RowRelatorio.Add(new RowsFiltro("DATAMOVIM", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));


                if (Fiscal)
                    RowRelatorio.Add(new RowsFiltro("FLAGSINTEGRA", "System.String", "=", "S"));

                LIS_MOVPRODUTOESColl = LIS_MOVPRODUTOESP.ReadCollectionByParameter(RowRelatorio);

                foreach (LIS_MOVPRODUTOESEntity item in LIS_MOVPRODUTOESColl)
                {

                    //Somente movimentação de entrada
                    if (item.IDTIPOMOVIMENTACAO == 1)
                        result += Convert.ToDecimal(item.QUANTIDADE);

                }

                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENF.TrimEnd() == "S")
                {
                    LIS_PRODUTONFECollection LIS_PRODUTONFEColl = new LIS_PRODUTONFECollection();
                    LIS_PRODUTONFEProvider LIS_PRODUTONFEP = new LIS_PRODUTONFEProvider();

                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("flagenviada", "System.String", "=", "S"));
                    RowRelatorio.Add(new RowsFiltro("idproduto", "System.String", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));

                    LIS_PRODUTONFEColl = LIS_PRODUTONFEP.ReadCollectionByParameter(RowRelatorio, "IDNOTAFISCALE DESC");
                    NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();
                    foreach (LIS_PRODUTONFEEntity item in LIS_PRODUTONFEColl)
                    {
                        if (NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGCANCELADA.TrimEnd() == "N" && NOTAFISCALEP.Read(Convert.ToInt32(item.IDNOTAFISCALE)).FLAGENVIADA.TrimEnd() == "S")
                            result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }

                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENFCE.TrimEnd() == "S")
                {
                    //Cupom Eletronico            
                    LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                    LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "1")); //Enviado
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
                    {
                        result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }


                if (CONFISISTEMAP.Read(1).FLAGBAIXAESTOQUENFCE.TrimEnd() == "S")
                {
                    //Cupom Eletronico            
                    LIS_PRODUTONFCECollection LIS_PRODUTONFCEColl = new LIS_PRODUTONFCECollection();
                    LIS_PRODUTONFCEProvider LIS_PRODUTONFCEP = new LIS_PRODUTONFCEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "1")); //Enviado
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", idproduto.ToString()));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", ConverStringDateSearch(DataInicial)));
                    RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", ConverStringDateSearch(DataFinal)));
                    
                    LIS_PRODUTONFCEColl = LIS_PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);

                    foreach (LIS_PRODUTONFCEEntity item in LIS_PRODUTONFCEColl)
                    {
                        result -= Convert.ToDecimal(item.QUANTIDADE);
                    }
                }

            }

            if (PRODUTOSTY.FLAGCONTROLAESTOQUE != null)
            {
                if (PRODUTOSTY.FLAGCONTROLAESTOQUE.Trim() == "S")
                    result = 0;
            }

            return result;
        }
      
        public void ExibirMSg(string mensagemSelec, string cor)
        {
            try
            {
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                if (CONFISISTEMAP.Read(1).FLAGMSGFECHA.Trim() == "N")
                    MessageBox.Show(mensagemSelec);
                else
                { 
                    FrmExibirMsg Frm = new FrmExibirMsg();
                    Frm.Mensagem = mensagemSelec;
                    Frm.Cor = cor;
                    Frm.Show();
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(2000); //Aguarda 2 segundos
                    Frm.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        public string encurtarUrl(string LongUrl)
        {
            string username = "imexsistemas";
            string api = "R_5d2d667625274e9e93e01577ef470592";
            using (WebClient w = new WebClient())
            {
                string bitLyUrl = string.Format("http://api.bit.ly/v3/shorten?login={0}&apiKey={1}&uri={2}&format=txt", username, api, LongUrl);
                string ShortUrl = w.DownloadString(bitLyUrl);
                return ShortUrl;
            }
        }

        public void exportarDataGridViewArquivo(DataGridView dgv, string extensao)
        {
            if (dgv.Rows.Count > 0)
            {

                DialogResult dr = MessageBox.Show("Deseja exportar a pesquisa para excel (arquivo." + extensao + ") ?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    System.IO.StreamWriter sw = null;
                    //Caractere delimitador
                    //string delimitador = "\t"; //tab
                    string delimitador = ";";

                    //Escolher onde salvar o arquivo
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = "ListaPesquisa." + extensao;
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    sfd.Filter = "arquivos " + extensao + " (*." + extensao + ")|*." + extensao + "";

                    //Se usuário escolher nome corretamente e clicar em salvar
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            //Pega o caminho do arquivo
                            string caminho = sfd.FileName;
                            //Cria um StreamWriter no local
                            sw = new System.IO.StreamWriter(caminho, false, System.Text.Encoding.GetEncoding(1252));

                            //Cabeçalho
                            string linha = null;
                            foreach (DataGridViewColumn coluna in dgv.Columns)
                            {
                                string CabecaGrid = coluna.HeaderText;
                                linha += CabecaGrid + delimitador;
                            }

                            sw.WriteLine(linha);

                            int qtdColunas = dgv.Columns.Count;
                            //Loop em todas as linhas para escrever na stream já com o delimitador.
                            foreach (DataGridViewRow dgvLinha in dgv.Rows)
                            {

                                string linha2 = null;
                                for (int i = 0; i < qtdColunas; i++)
                                {
                                    string CelulaGrid = string.Empty;
                                    try
                                    {
                                        CelulaGrid = Convert.ToString(dgvLinha.Cells[i].Value.ToString());

                                        //Formata para moeda e data
                                        if (ValidacoesLibrary.ValidaTipoDecimal(CelulaGrid))
                                            CelulaGrid = Convert.ToDecimal(CelulaGrid).ToString("n2");
                                        else if (ValidacoesLibrary.ValidaTipoDateTime(CelulaGrid))
                                            CelulaGrid = Convert.ToDateTime(CelulaGrid).ToString("dd/MM/yyyy");

                                        linha2 += CelulaGrid + delimitador;
                                    }
                                    catch (Exception)
                                    {
                                        CelulaGrid = string.Empty;
                                        linha2 += CelulaGrid + delimitador;
                                    }
                                }
                                sw.WriteLine(linha2);
                            }

                            //Mensagem de confirmação
                            MessageBox.Show("Exportado com sucesso", "Exportado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            //Fechar stream SEMPRE
                            sw.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe pesquisa selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void ArquivoRemessaCaixa_CNAB240(LIS_DUPLICATARECEBERCollection DuplicatasColl, string extensao, Boolean Protesto)
        {
            CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
            CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
            CONFIGBOLETATy = CONFIGBOLETAP.Read(5); //caixa
            string caminho = string.Empty;
            int sequencia = BuscaSequenciaRemessa();
            string LocalErro = string.Empty;

            //Preencher com a Quantidade de registros no lote; trata -  se da somatória dos registros de tipo 1, 3, e 5
            int SomatorioArquivoLote = 0;
            
            if (CONFIGBOLETATy == null)
                MessageBox.Show("Configuração do boleto CAIXA não localizado!");

            if (DuplicatasColl.Count > 0 && CONFIGBOLETATy != null)
            {

                DialogResult dr = MessageBox.Show("Deseja Gerar o Arquivo em Formato CNAB240 (Remessa_CAIXA." + extensao + ") ?",
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    System.IO.StreamWriter sw = null;

                    //Escolher onde salvar o arquivo
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.FileName = "Remessa_CAIXA_" + sequencia.ToString().PadLeft(7, '0') + "." + extensao;
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    sfd.Filter = "arquivos " + extensao + " (*." + extensao + ")|*." + extensao + "";

                    //Se usuário escolher nome corretamente e clicar em salvar
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            //Pega o caminho do arquivo
                            caminho = sfd.FileName;
                            //Cria um StreamWriter no local
                            sw = new System.IO.StreamWriter(caminho, false, System.Text.Encoding.GetEncoding(1252));


                            string linha = null;

                            ///////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            //DESCRIÇÃO DE REGISTRO TIPO ‘0’ (Obrigatório)HEADER DE ARQUIVO REMESSA TAMANHO DO REGISTRO = 240 CARACTERES
                            //INICIO - HEADER	

                            /////////////////////////////////////////////C A I X A \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            //                              Posicao 
                            //Controle
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                Descrição
                            //01.0      Código do Banco     1       3           9(003)      Preencher '104’         G001 
                            linha += "104";

                            //Controle
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                Descrição
                            //02.0      Código do Lote      4       7           9(004)      Preencher '0000'        G002
                            linha += "0000";

                            //Controle
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                                        Descrição
                            //03.0      Tipo de Registro    8       8           9(001       Preencher '0' (equivale a Header de Arquivo)       G003
                            linha += "0";
                            SomatorioArquivoLote = SomatorioArquivoLote + 1;

                            //CNAB
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                Descrição
                            //04.0      Filler              9       17          X(009)      Preencher com espaços   G004
                            linha += string.Empty.ToString().PadRight(9, ' ');

                            //Empresa Beneficiária
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                                Descrição
                            //05.0      Tipo de Inscrição   18      18          9(001)      Preencher com o tipo de inscrição do    G005
                                                                                            //Beneficiário: '1', se CPF(pessoa
                                                                                            //física); ou '2' se CNPJ(pessoa
                                                                                            //jurídica)
                            //'1'  =  CPF
                            //'2'  =  CGC / CNPJ"
                            if (CONFIGBOLETATy.CPFCNPJCEDENTE.Length > 14)
                                linha += "2";
                            else
                                linha += "1";

                            //Empresa Beneficiária
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo                        Descrição
                            //06.0      Número de Inscrição 19      32          9(014)      Ver Nota Explicativa G006       G006 
                                        //do Beneficiário
                            linha += RetiraLetras(CONFIGBOLETATy.CPFCNPJCEDENTE).ToString().PadLeft(14, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúdo                     Descrição
                            //07.0      Uso Exclusivo CAIXA     33      52          9(020)       Preencher com zeros
                            linha += string.Empty.ToString().PadRight(20, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúdo                        G008
                            //08.0      Agência Mantenedora da  53      57          9(005)      Preencher com o código da agência
                                                                                                //detentora da conta, com um zero à esquerda
                            linha += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo                        
                            //09.0      Dígito Verificador da Agência   58      58          X(001)      Preencher com o dígito verificador da  agência, informado pela CAIXA
                            linha += CONFIGBOLETATy.DIGAGENCIA.ToString().PadRight(1, ' ');

                            //Empresa Beneficiária
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo                        
                            //10.0      Código do Beneficiário          59      64          9(006)      Ver Nota Explicativa G007
                            //G007 ==> Código do Convênio no Banco (Código do Beneficiário)
                            //Código fornecido pela CAIXA, através da agência de relacionamento do cliente.
                            //Deve ser preenchido com o código do Beneficiário(6 posições).
                            linha += CONFIGBOLETATy.CONVENIO.ToString().PadLeft(6, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo                        
                            //11.0      Uso Exclusivo CAIXA             65      71          9(007)      Zeros
                            linha += string.Empty.ToString().PadRight(7, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo                        
                            //12.0       Uso Exclusivo CAIXA            72      72          9(001)      Zeros 
                            linha += string.Empty.ToString().PadRight(1, '0');

                            //Empresa Beneficiária
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo  
                            //13.0      Nome da Empresa                 73      102         X(030)       Nome da empresa beneficiária 
                            linha += LimiterText(CONFIGBOLETATy.NOMECEDENTE, 30).PadRight(30, ' ');

                            //Banco Beneficiário
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo  
                            //14.0      Nome do Banco                   103     132         X(030)      CAIXA ECONOMICA FEDERAL    
                            linha += LimiterText("CAIXA ECONOMICA FEDERAL", 30).PadRight(30, ' ');

                            //CNAB 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo  
                            //15.0      Filler              133     142         X(010)      Espaços
                            linha += string.Empty.ToString().PadRight(10, ' ');

                            //Arquivo 
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //16.0      Código Remessa / Retorno    143     143         9(001)      Informado pela CAIXA de acordo com o resultado do processamento da remessa, conforme Nota Explicativa
                            linha += "1";

                            //Arquivo 
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //17.0      Data de Geração do Arquivo  144     151         9(008)      Data da criação do arquivo pela CAIXA, no formato DDMMAAAA(Dia,Mês e Ano)
                            linha += DateTime.Now.ToString("ddMMyyyy");

                            //Arquivo 
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //18.0      Hora de Geração do Arquivo  152     157         9(006)      Hora da geração do arquivo, no formato HHMMSS(Hora, Minuto e Segundos)
                            linha += DateTime.Now.ToString("HHmmss");

                            //Arquivo 
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //19.0      NSA                         158     163         9(0Ver Nota Explicativa C028 06)      Número Sequencial do Arquivo, de controle da CAIXA
                            linha += sequencia.ToString().PadLeft(6, '0');

                            //Arquivo 
                            //Campo     Nome do campo                         de     ate        "Picture"     Conteúdo  
                            //20.0      No da Versão do Layout do Arquivo      164  166         9(003)          '040' 
                            linha += "050";

                            //Arquivo 
                            //Campo     Nome do campo                          de     ate        "Picture"     Conteúdo  
                            //21.0      Densidade de Gravação do  Arquivo     167       171         9(005)      Zeros  
                            linha += "00000";

                            //Uso Exclusivo CAIXA
                            //Campo     Nome do campo                          de     ate        "Picture"     Conteúdo  
                            //22.0      Para Uso Reservado do Banco             172     191         X(020)      Espaços 
                            linha += string.Empty.ToString().PadRight(20, ' ');

                            //Reservado Empresa
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //23.0      Situação do Arquivo         192     211         X(020)      Campo a ser utilizado pela CAIXA para informação o status do beneficiário no arquivo retorno: -Durante a fase de testes(simulado)
                            //conterá a literal ‘RETORNO - TESTE’
                            //-Estando em produção conterá a
                            //literal ‘RETORNO - PRODUÇÃO’
                            //alterado dia 06/06/2017 13:11
                            // linha += "REMESSA-TESTE".PadRight(20, ' ');
                             linha += "REMESSA - PRODUCAO".PadRight(20, ' ');

                            //Versão do Aplicativo
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo  
                            //24.0      Versão do Aplicativo        212     215         X(004)      Espaços
                            linha += string.Empty.ToString().PadRight(4, ' ');

                            //CNAB 
                            //Campo     Nome do campo     de     ate        "Picture"     Conteúdo  
                            //25.0      Filler            216   225         X(025)          Espaços  
                            linha += string.Empty.ToString().PadRight(25, ' ');

                            sw.WriteLine(linha);
                            //FIM - HEADER					
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\                        

                            //INICIO - HEADER DO LOTE										
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            //DESCRIÇÃO DE REGISTRO TIPO ‘‘1’’: HEADER DE LOTE DE ARQUIVO RETORNO
                            //TAMANHO DO REGISTRO = 240 CARACTERES

                            string linha_HEADER_LOTE = null;

                            //Controle 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo  
                            //01.1      Código do Banco     1       3           9(003)      '104’
                            linha_HEADER_LOTE += "104";

                            // Controle
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo  
                            //02.1      Lote de Serviço     4       7           9(004)      No primeiro lote da pré-crítica será
                                                                                            //'0001', nos demais será o número do
                                                                                            // lote anterior acrescido de 1; exemplo:
                                                                                            //1º lote = '0001', 2º lote = '0002'
                            linha_HEADER_LOTE += "0001";

                            // Controle
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo  
                            //03.1      Tipo de Registro    8       8           9(001)      '1’ (Header de Lote) 
                            linha_HEADER_LOTE += "1";
                            

                            //Informar o Número do total de registros enviados no arquivo;
                            //tratase da somatória dos registros de tipo 1, 3, 5
                          //  SomatorioArquivoLote2 = SomatorioArquivoLote2 + 1;


                            //Preencher com a Quantidade de registros no lote; trata - 
                            //se da somatória dos registros de tipo 1, 3, e 5
                            SomatorioArquivoLote = SomatorioArquivoLote + 1;                            
                            // Serviço
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo 
                            //04.1      Tipo de Operação    9       9           X(001)      ‘T’ (Arquivo Retorno) 
                            linha_HEADER_LOTE += "R";

                            // Serviço
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo 
                            //05.1      Tipo de Serviço     10      11          9(002)      ‘01', se Cobrança Registrada; ou ‘02’,
                                                                                            //se Cobrança Sem Registro/ Serviços
                            linha_HEADER_LOTE += "01";

                            // Serviço
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo 
                            //06.1      Filler              12      13          9(002)      Zeros
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(2, '0');

                            // Serviço
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo 
                            //07.1      Nº da Versão do Layout Lote     14      16          9(003)      "060"
                            linha_HEADER_LOTE += "030";

                            // CNAB 
                            //Campo     Nome do campo      de     ate        "Picture"     Conteúdo 
                            //08.1      Filler              17      17          X(001)      Espaço 
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(1, ' ');

                            //Empresa
                            //Campo     Nome do campo                       de     ate        "Picture"     Conteúdo 
                            //09.1      Tipo de Inscrição do Beneficiário   18      18          9(001)      '1', se CPF (pessoa física); ou '2' se  CNPJ(pessoa jurídica)
                            //'1'  =  CPF
                            //'2'  =  CGC / CNPJ"
                            if (CONFIGBOLETATy.CPFCNPJCEDENTE.Length > 14)
                                linha_HEADER_LOTE += "2";
                            else
                                linha_HEADER_LOTE += "1";

                            //Empresa
                            //Campo     Nome do campo                           de     ate        "Picture"     Conteúdo 
                            //10.1      Número de Inscrição do Beneficiário     19      33          9(015)      Número de inscrição (CPF ou CNPJ) do Beneficiário
                            linha_HEADER_LOTE += RetiraLetras(CONFIGBOLETATy.CPFCNPJCEDENTE).ToString().PadLeft(15, '0');

                            //Empresa
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúdo 
                            //11.1      Código do Beneficiário  34      39          9(006)      Zeros   
                            linha_HEADER_LOTE += CONFIGBOLETATy.CODCEDENTE.ToString().PadLeft(6, '0');
                            

                            //Empresa
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúdo 
                            //11.1      Uso Exclusivo CAIXA      40     53          9(006)      Zeros    
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(14, '0');

                            //Empresa
                            //Campo     Nome do campo                    de     ate        "Picture"     Conteúdo
                            //12.1      Agência Mantenedora da  Conta   54      58          9(005)      Código da agência detentora da  conta, com um zero à esquerda
                            linha_HEADER_LOTE += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                            //Empresa
                            //Campo     Nome do campo                    de     ate        "Picture"     Conteúdo
                            //13.1      Dígito Verificador da Agência   59      59          X(001)          Dígito verificador da agência 
                            linha_HEADER_LOTE += CONFIGBOLETATy.DIGAGENCIA.ToString().PadRight(1, ' ');

                            //Empresa
                            //Campo     Nome do campo                    de     ate        "Picture"     Conteúdo
                            //14.1      Código do Convênio no  Banco    60      65          9(006)      Código de identificação do Beneficiário na Cobrança CAIXA(6 posições)
                            linha_HEADER_LOTE += CONFIGBOLETATy.CONVENIO.ToString().PadLeft(6, '0');

                            //Empresa
                            //Campo     Nome do campo                    de     ate        "Picture"     Conteúdo
                            //15.1      Código do Modelo Personalizado  66      72          9(007)      Informado somente quando o modelo  do boleto for personalizado
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(7, '0');

                            //Empresa
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo
                            //16.1      Uso Exclusivo CAIXA         73      73          9(001)      Zeros 
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(1, '0');

                            //Empresa
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //17.1      Nome da Empresa     74      103         X(030)      Nome da empresa beneficiária
                            linha_HEADER_LOTE += RemoverAcentos(LimiterText(CONFIGBOLETATy.NOMECEDENTE, 30).PadRight(30, ' '));

                            //Informações 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //18.1      Mensagem 1          104     143        X(040)        Espaços 
                            //"Mensagem 1: Texto referente a mensagens que serão impressas em todos os boletos
                            //referentes ao mesmo lote. Estes campos não serão utilizados no arquivo retorno."		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(40, ' ');

                            //Informações 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //19.1     Mensagem 2          144     183        X(040)        Espaços 
                            //"Mensagem 2: Texto referente a mensagens que serão impressas em todos os boletos
                            //referentes ao mesmo lote. Estes campos não serão utilizados no arquivo retorno."	
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(40, ' ');

                            //Controle da Cobrança
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //20.1      Número da Remessa   184     191         9(008)      Número Sequencial do Arquivo, de controle da CAIXA
                            linha_HEADER_LOTE += sequencia.ToString().PadLeft(8, '0');

                            //Controle da Cobrança
                            //Campo     Nome do campo               de     ate        "Picture"     Conteúdo
                            //21.1      Data de Gravação Retorno    192     199         9(008)      Data da gravação do arquivo, no formato DDMMAAAA(Dia, Mês e Ano)
                            linha_HEADER_LOTE += DateTime.Now.ToString("ddMMyyyy");

                            //Data do Crédito
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //22.1      Data do Crédit      200     207         9(008)      Data de  efetivação do crédito  referente ao pagamento do título de  cobrança, se for o caso
                           linha_HEADER_LOTE += string.Empty.ToString().PadRight(8, '0');

                            //CNAB 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //23.1      Filler              208     209          X(33)     Zeros
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(33, ' ');

                            //CNAB 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //24.1      Filler              210     235          X(026)    Espaços
                            ///linha_HEADER_LOTE += string.Empty.ToString().PadRight(26, ' ');

                            //CNAB 
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //25.1      Filler              236    237          X(02)       Zeros
                            //linha_HEADER_LOTE += string.Empty.ToString().PadRight(2, '0');

                            // CNAB
                            //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                            //26.1      Filler              238    240          X(03)       Espaços
                            //linha_HEADER_LOTE += string.Empty.ToString().PadRight(3, ' ');


                            sw.WriteLine(linha_HEADER_LOTE);
                            //FIM  - HEADER DO LOTE					
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\  
                            //DESCRIÇÃO DE REGISTRO TIPO ‘‘3’’, Segmento ‘‘T’’:
                            //MOVIMENTAÇÃO NA CARTEIRA
                            //TAMANHO DO REGISTRO = 240 CARACTERES
                            int contadorlinha = 0;
                            decimal ValorTotalDuplicatas = 0;
                            int ContadorSequencia = 1;
                            foreach (LIS_DUPLICATARECEBEREntity item in DuplicatasColl)
                            {
                                if (item.IDDUPLICATARECEBER != null && item.IDSTATUS != 3) //3 Pago 
                                {

                                    LocalErro = "Erro na Duplicata n. " + item.NUMERO + " Cliente: " + item.NOMECLIENTE;
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                    //INICIO - REGISTRO DETALHE SEGMENTO P	
                                    string LINHA_DETALHE_SEGMENTO_P = string.Empty;

                                    //Dados do Cliente
                                    CLIENTEEntity ClieteTy = new CLIENTEEntity();
                                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                                    ClieteTy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));

                                    //Controle
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                                    //01.31     Código do Banco     1       3          9(003)     '104’ 
                                    LINHA_DETALHE_SEGMENTO_P += "104";

                                    //Controle
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                                    //02.3T     Lote de Serviço     4       7           9(004)      Ver Nota Explicativa G002 
                                    LINHA_DETALHE_SEGMENTO_P += "0001";

                                    //Controle
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                                    //03.3P     Tipo de Registro    8       8           9(001)      Preencher '3’ (equivale a Detalhe de Lote) 
                                    LINHA_DETALHE_SEGMENTO_P += "3";

                                    //Preencher com a Quantidade de registros no lote; trata - 
                                    //se da somatória dos registros de tipo 1, 3, e 5
                                    SomatorioArquivoLote = SomatorioArquivoLote + 1;
                                   
                                    //Informar o Número do total de registros enviados no arquivo;
                                    //tratase da somatória dos registros de tipo 1, 3, 5 
                                  //  SomatorioArquivoLote2 = SomatorioArquivoLote2 + 1;

                                    //Serviço
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúdo
                                    //04.3P     Nº Sequencial do Registro  no Lote      9(005)      Ver Nota Explicativa G038 
                                    LINHA_DETALHE_SEGMENTO_P += ContadorSequencia.ToString().PadLeft(5, '0');
                                    ContadorSequencia++;

                                    //Serviço
                                    //Campo     Nome do campo                      de     ate        "Picture"     Conteúdo
                                    //05.3P     Cód. Segmento do Registro Detalhe   14      14          X(001)      Preencher 'P’
                                    LINHA_DETALHE_SEGMENTO_P += "P";

                                    //Serviço
                                    //Campo     Nome do campo   de     ate        "Picture"     Conteúdo
                                    //06.3P     Filler          15      15          X(001)      Preencher com espaços 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(01, ' ');

                                    //Serviço
                                    //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo
                                    //07.39     Código de Movimento Remessa     16      17          9(002)      Ver Nota Explicativa C004 
                                    LINHA_DETALHE_SEGMENTO_P += "01";

                                    //Código de identificação  Beneficiário
                                    //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo
                                    //08.3P     Agência Mantenedora da Conta    18      22          9(005)      Preencher com o código da agência  detentora da conta, com um zero à esquerda
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                                    //Código de identificação  Beneficiário
                                    //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo
                                    //09.3P     Dígito Verificador da gência    23      23          X(001)      Preencher com o dígito verificador da agência, informado pela CAIXA
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.DIGAGENCIA.ToString().PadLeft(1, ' ');

                                    //Código de identificação  Beneficiário
                                    //Campo     Nome do campo                   de     ate        "Picture"     Conteúdo
                                    //10.3P     Código do Convênio no Banco     24      29          9(006)      Código fornecido pela CAIXA, através da agência de relacionamento do cliente; trata - se do  código do Beneficiário(6 posições)
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.CONVENIO.ToString().PadLeft(6, '0');

                                    //Código de identificação  Beneficiário
                                    //Campo     Nome do campo           de     ate        "Picture"     Conteúdo
                                    //11.3P     Uso Exclusivo CAIXA     30      37          9(008)      Preencher com zeros 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(08, '0');

                                    //Código de identificação  Beneficiário
                                    //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                                    //12.3P     Filler                  38      39          9(002)      Preencher com zeros 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(2, '0');

                                    //Nosso Número
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //13.3P     Modalidade da Carteira  (SINCO)     40      40          9(001) 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(1, '0');

                                    //Nosso Número
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //13.3P     Modalidade da Carteira (SIGCB)      41      42          9(002)      
                                    LINHA_DETALHE_SEGMENTO_P += "14";//string.Empty.ToString().PadRight(2, '0');

                                    //Nosso Número
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //13.3P     Identificação do Título no  Banco    43      57         9(015)       
                                    LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NUMERO).ToString().PadLeft(15, '0');

                                    //Características da cobrança
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúd
                                    //14.3P     Código da Carteira  58      58          9(001)      
                                    LINHA_DETALHE_SEGMENTO_P += "1";

                                    //Características da cobrança
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //15.3P     Forma de Cadastramento do Título no Banco   59      59          9(001)       Preencher ‘1’ - Cobrança Registrada 
                                    LINHA_DETALHE_SEGMENTO_P += "1";

                                    //Características da cobrança
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //16.3P     Tipo de Documento                           60      60          X(001)      Preencher '2’ - Escritural 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(01, '2');

                                    //Características da cobrança
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //17.3P     Identificação da Emissão do Boleto          61      61          9(001)      Ver Nota Explicativa C009 
                                    LINHA_DETALHE_SEGMENTO_P += "2";

                                    //Características da cobrança
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //18.3P     Identificação da Entrega do Boleto          62      62          X(001)  Ver Nota Explicativa C010 
                                    LINHA_DETALHE_SEGMENTO_P += "0";

                                    //Nº do Documento (Seu Nº)
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //19.3P     Nº do Documento (Seu Nº)                    63      73          X(011)
                                    if (item.NUMERO.Trim().Length > 0)
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NUMERO).ToString().PadLeft(11, '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NOTAFISCAL).ToString().PadLeft(11, '0');

                                    //Uso Exclusivo CAIXA
                                    //Campo          Nome do campo          de     ate        "Picture"     Conteúd
                                    //19.3P          Uso Exclusivo CAIXA    74      77          X(004)      Preencher com espaços
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(04, ' ');

                                    //Vencimento 
                                    //Campo         Nome do campo          de     ate        "Picture"     Conteúd
                                    //20.3P         Vencimento              78   85             9(008)      Ver Nota Explicativa C012  
                                    LINHA_DETALHE_SEGMENTO_P += Convert.ToDateTime(item.DATAVECTO).ToString("ddMMyyyy");

                                    //Valor do Título
                                    //Campo         Nome do campo          de     ate        "Picture"     Conteúd
                                    //21.3P         Valor do Título         86      100         9(015)      Preencher com o valor original do título, utilizando 2 casas decimais (exemplo: título de valor 530,44 -  preencher 000000000053044)
                                    string ValorDuplicata = Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2");
                                    ValorDuplicata = RetiraLetras(ValorDuplicata).PadLeft(15, '0');
                                    LINHA_DETALHE_SEGMENTO_P += ValorDuplicata;

                                    //Ag. Cobradora
                                    //Campo         Nome do campo          de     ate        "Picture"     Conteúd
                                    //22.3P         Ag. Cobradora           101     105         9(005)      Preencher com zeros    
                                    LINHA_DETALHE_SEGMENTO_P += "00000";

                                    //DV
                                    //Campo         Nome do campo                       de     ate        "Picture"     Conteúd
                                    //23.3P         Dígito Verificador da Agência       106     106         X(001)      Preencher '0’ 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(01, '0');

                                    //Espécie de Título
                                    //Campo         Nome do campo                       de     ate        "Picture"     Conteúd
                                    //24.3P         Espécie de Título                   107     108         9(002)      Ver Nota Explicativa C015 
                                    LINHA_DETALHE_SEGMENTO_P += "02";

                                    //Aceite
                                    //Campo         Nome do campo                                   de     ate        "Picture"     Conteúd
                                    //25.3P         Identificação de Título Aceito / Não Aceito     109     109         X(001)      Ver Nota Explicativa C016 
                                    LINHA_DETALHE_SEGMENTO_P += "A";

                                    //Data Emissão do Título
                                    //Campo         Nome do campo               de     ate        "Picture"     Conteúd
                                    //26.3P         Data Emissão do Título      110     117         9(008)      Ver Nota Explicativa G071 
                                    LINHA_DETALHE_SEGMENTO_P += Convert.ToDateTime(item.DATAEMISSAO).ToString("ddMMyyyy");

                                    //Juros
                                    //Campo         Nome do campo               de     ate        "Picture"     Conteúd
                                    //27.3P         Código do Juros de Mora     118     118         9(001)      Ver Nota Explicativa C018 
                                    LINHA_DETALHE_SEGMENTO_P += "3";

                                    //Juros
                                    //Campo         Nome do campo               de     ate        "Picture"     Conteúd
                                    //28.3P         Data do Juros de Mora       119     126         9(008
                                    LINHA_DETALHE_SEGMENTO_P += Convert.ToDateTime(item.DATAVECTO).ToString("ddMMyyyy");

                                    //Juros
                                    //Campo         Nome do campo                   de     ate        "Picture"     Conteúd
                                    //29.3P         Juros de Mora por Dia / Taxa    127     141         9(015)  
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(15, '0');

                                    //Juros
                                    //Campo         Nome do campo                   de     ate        "Picture"     Conteúd
                                    //30.3P         Código do Desconto 1            142     142         9(001)      Ver Nota Explicativa C021
                                    LINHA_DETALHE_SEGMENTO_P += "0";

                                    //Juros
                                    //Campo         Nome do campo           de     ate        "Picture"     Conteúd
                                    //31.3P         Data do Desconto 1      143     150       9(008)        Ver Nota Explicativa C022 
                                    LINHA_DETALHE_SEGMENTO_P +="00000000";

                                    //Juros
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //32.3P     Valor/Percentual a ser Concedido     151    165         9(015)          Ver Nota Explicativa C023
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(15, '0');

                                    //Valor IOF
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //33.3P     Valor do IOF a ser Recolhido        166     180         9(015)      
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(15, '0');

                                    //Valor Abatimento
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //34.3P     Valor do Abatimento                 181     195         9(015)      Preencher com o valor do abatimento (redução do valor do documento)dado ao Pagador do título, expresso Em moeda corrente com duas casas decimais
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(15, '0');

                                    //Uso Empresa Beneficiário
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //35.3P     Uso Empresa Beneficiário            196     220         X(025)      Preencher igual ao campo 19.3P (Número do Documento de Cobrança)
                                    if (item.NUMERO.Trim().Length > 0)
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NUMERO).ToString().PadLeft(11, '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NOTAFISCAL).ToString().PadLeft(11, '0');

                                    //para completar o resto de 25 caracteres
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(14, ' ');

                                    //Código p/ Protesto
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //36.3P     Código para Protesto                221     221         (001)       Ver Nota Explicativa C026
                                    if (Protesto)
                                        LINHA_DETALHE_SEGMENTO_P += "1";
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += "3";

                                    //Prazo p/ Protesto
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //37.3P     Número de Dias para Protesto         222    223         9(002)      Ver Nota Explicativa C027 
                                    if (Protesto)
                                        LINHA_DETALHE_SEGMENTO_P += "10";
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += "00";

                                    //Código p/ Baixa / Devolução
                                    //Campo     Nome do campo                        de     ate        "Picture"     Conteúd
                                    //38.3P     Código para Baixa / Devolução       224     224         9(001)      Ver Nota Explicativa C028 
                                    LINHA_DETALHE_SEGMENTO_P += "1";

                                    //Prazo p/ Baixa / Devolução
                                    //Campo     Nome do campo                           de     ate        "Picture"     Conteúd
                                    //39.3P     Número de Dias para Baixa / Devolução   225     227         X(003)      Ver Nota Explicativa C029 
                                    LINHA_DETALHE_SEGMENTO_P += "028";

                                    //Código da Moeda 
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //40.3P     Código da Moeda             228     229         9(002)      Preencher ‘09’ (REAL)     
                                    LINHA_DETALHE_SEGMENTO_P += "09";

                                    //Uso Exclusivo CAIXA
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //41.3P     Filler                      230     239         9(010)       Preencher com zeros 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(10, '0');

                                    //CNAB
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //42.3P     Fille                       240     240         X(001)      Preencher com espaços 
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(1, ' ');

                                    sw.WriteLine(LINHA_DETALHE_SEGMENTO_P);

                                    //FIM - REGISTRO DETALHE SEGMENTO P	
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


                                    //INICIO - REGISTRO DETALHE SEGMENTO Q
                                   //DESCRIÇÃO DE REGISTRO TIPO “3”, Segmento “Q” (Obrigatório)
                                   //DADOS DO PAGADOR E SACADOR/ AVALISTA
                                   //TAMANHO DO REGISTRO = 240 CARACTERES
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                    string LINHA_DETALHE_SEGMENTO_Q = null;

                                    //Controle
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //01.3Q     Código do Banco             1       3           9(003)      Preencher '104’ 
                                    LINHA_DETALHE_SEGMENTO_Q += "104";

                                    //Controle
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //02.3Q     Lote de Serviço             4       7           9(004)      Ver Nota Explicativa G002 
                                    LINHA_DETALHE_SEGMENTO_Q += "0001";

                                    //Serviço 
                                    //Campo     Nome do campo                      de     ate        "Picture"     Conteúd
                                    //03.3Q    Tipo de Registro                   8       8           9(001)      Preencher '3’ (equivale a Detalhe de Lote)
                                    LINHA_DETALHE_SEGMENTO_Q += "3";

                                    //Preencher com a Quantidade de registros no lote; trata - 
                                    //se da somatória dos registros de tipo 1, 3, e 5
                                    SomatorioArquivoLote = SomatorioArquivoLote + 1;                                  

                                    //Serviço 
                                    //Campo     Nome do campo                         de     ate        "Picture"     Conteúd
                                    //04.3Q     Nº Sequencial do Registro no Lote       9   13          9(005)          Ver Nota Explicativa G038 
                                    LINHA_DETALHE_SEGMENTO_Q += ContadorSequencia.ToString().PadLeft(5, '0');
                                    ContadorSequencia++;

                                    //Serviço 
                                    //Campo     Nome do campo                         de     ate        "Picture"     Conteúd
                                    //05.3Q     Cód. Segmento do Registro Detalhe       14  14              X(001)      Preencher 'Q’ 
                                    LINHA_DETALHE_SEGMENTO_Q += "Q";

                                    //Serviço 
                                    //Campo     Nome do campo       de     ate        "Picture"     Conteúd
                                    //06.3Q     Filler              15      15          X(001)      Preencher com espaços
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(1, ' ');

                                    //Serviço 
                                    //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                    //07.3Q     Código de Movimento  Remessa    16      17          9(002)      Ver Nota Explicativa C004
                                    LINHA_DETALHE_SEGMENTO_Q += "01";

                                //Dados do Pagado
                                //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                //08.3Q     Tipo de Inscrição do Pagador    18      18          9(001)      Preencher com o tipo de inscrição do Pagador: '1', se CPF(pessoa física);                                     ou '2' se CNPJ(pessoa jurídica)
                                 if (ClieteTy.CPF != "   .   .   -")
                                     LINHA_DETALHE_SEGMENTO_Q += "1";
                                  else
                                     LINHA_DETALHE_SEGMENTO_Q += "2";

                                    //Dados do Pagado
                                    //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                    //09.3Q     Número de Inscrição do Pagador  19      33          9(015)      Preencher com o número do CNPJ ou CPF do Pagador, conforme o caso
                                    if (ClieteTy.CPF != "   .   .   -")
                                     LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CPF).ToString().PadLeft(15, '0');
                                 else
                                     LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CNPJ).ToString().PadLeft(15, '0');

                                    //Dados do Pagador
                                    //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                    //10.3Q     Nome do Pagador                 34      73          X(040           Preencher com Nome do Pagador 
                                    LINHA_DETALHE_SEGMENTO_Q += RemoverAcentos(LimiterText(ClieteTy.NOME, 40).ToString().PadRight(40, ' '));

                                    //Dados do Pagador
                                    //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                    //11.3Q     Endereço do Pagador             74      113         X(040
                                    LINHA_DETALHE_SEGMENTO_Q += RemoverAcentos(LimiterText(ClieteTy.ENDERECO1 + " " + ClieteTy.NUMEROENDER, 40).ToString().PadRight(40, ' '));

                                    //Dados do Pagador
                                    //Campo     Nome do campo                    de     ate        "Picture"     Conteúd
                                    //12.3Q     Bairro do Pagador               114     128         X(015)
                                    LINHA_DETALHE_SEGMENTO_Q += RemoverAcentos(LimiterText(ClieteTy.BAIRRO1, 15).ToString().PadRight(15, ' '));

                                    //Dados do Pagador
                                    //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                                    //13.3Q     CEP do Pagador          129     133         9(005)      
                                    LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(LimiterText(RetiraLetras(ClieteTy.CEP1).ToString().PadLeft(5, '0'), 5));

                                    //Dados do Pagador
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //14.3Q     Sufixo do CEP do Pagador    134     136         9(003)     
                                    if (ClieteTy.CEP1 != "     -")
                                    {
                                        int LocalTraco = ClieteTy.CEP1.IndexOf("-");
                                        string Sufixo = ClieteTy.CEP1.Substring(LocalTraco + 1, 3);
                                        LINHA_DETALHE_SEGMENTO_Q += Sufixo.PadLeft(3, '0');
                                    }
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadLeft(3, '0');

                                    //Dados do Pagador
                                    //Campo     Nome do campo               de     ate        "Picture"     Conteúd
                                    //15.3Q     Cidade do Pagador           137     151         X(015) 
                                    MUNICIPIOSTy = MUNICIPIOSProvider.Read(Convert.ToInt32(ClieteTy.COD_MUN_IBGE));
                                    LINHA_DETALHE_SEGMENTO_Q += RemoverAcentos(LimiterText(MUNICIPIOSTy.MUNICIPIO, 15).PadRight(15, ' '));

                                    //Dados do Pagador
                                    //Campo     Nome do campo                       de     ate        "Picture"     Conteúd
                                    //16.3Q     Unidade da Federação do Pagador     152     153         X(002) 
                                    ESTADOProvider ESTADOP = new ESTADOProvider();
                                    string UF = ESTADOP.Read(Convert.ToInt32(MUNICIPIOSTy.COD_UF_IBGE)).UF;
                                    LINHA_DETALHE_SEGMENTO_Q += UF;

                                    //Dados do Sacador / Avalista
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //17.3Q     Tipo de Inscrição do Sacador / Avalista     154     154         9(001) 
                                    if (ClieteTy.CPF != "   .   .   -")
                                        LINHA_DETALHE_SEGMENTO_Q += "1";
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += "2";

                                    //Dados do Sacador / Avalista
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //18.3Q     Número de Inscrição do Sacador / Avalista   155     169         9(015) 
                                    if (ClieteTy.CPF != "   .   .   -")
                                        LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CPF).ToString().PadLeft(15, '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CNPJ).ToString().PadLeft(15, '0');

                                    //Dados do Sacador / Avalista
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //19.3Q     Nome do Sacador/Avalista                    170     209         X(040)
                                    LINHA_DETALHE_SEGMENTO_Q += RemoverAcentos(LimiterText(ClieteTy.NOME, 40).ToString().PadRight(40, ' '));

                                    //Banco Correspondente
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //20.3Q     Cód. Bco. Corresp. na Compensação           210     212         9(003)  Preencher com zeros; campo exclusivo para troca de arquivos entre bancos
                                    LINHA_DETALHE_SEGMENTO_Q += "000";

                                    //Nosso Núm.Bco. Correspon dente
                                    //Campo     Nome do campo                               de     ate        "Picture"     Conteúd
                                    //21.3Q     Nosso Nº no Banco Correspondente            213     232         X(020)  Preencher com espaços; campo exclusivo para troca de arquivos entre bancos
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(20, ' ');

                                    //CNAB
                                    //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                                    //22.3Q     Filler                  233     240         X(008)       Preencher com espaços
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(8, ' ');

                                    sw.WriteLine(LINHA_DETALHE_SEGMENTO_Q);
                                    //FIM - REGISTRO DETALHE SEGMENTO Q
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                    contadorlinha++;
                                    ValorTotalDuplicatas += Convert.ToDecimal(item.VALORDUPLICATA);
                                }

                                // ContadorSequencia++;
                            }


                            //INICIO - REGISTRO TRAILLER DO LOTE
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                            string LINHA_TRAILLER_LOTE = null;

                            //Controle
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //01.5      Código do Banco         1       3           9(003)      Preencher '104  
                            LINHA_TRAILLER_LOTE += "104";

                            //Controle
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //02.5      Lote de Serviço         4       7           9(004)      Ver Nota Explicativa G002  
                            LINHA_TRAILLER_LOTE += "0001";

                            //Controle
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //03.5      Tipo de Registro        8       8           9(001)      Preencher '5’ (equivale a Trailer de Lote) 
                            LINHA_TRAILLER_LOTE += "5";

                            //Informar o Número do total de registros enviados no arquivo;
                            //tratase da somatória dos registros de tipo 1, 3,5
                          //  SomatorioArquivoLote2 = SomatorioArquivoLote2 + 1;

                            //CNAB
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //04.5      Filler                  9       17          X(009)      Preencher com espaços       
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(9, ' ');

                            //Qtde de Registros
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúd
                            //05.5      Quantidade de Registros no Lote 18      23          9(006)      Preencher com a Quantidade de registros no lote; trata - se da somatória dos registros de tipo 1, 3, e 5
                            LINHA_TRAILLER_LOTE += SomatorioArquivoLote.ToString().PadLeft(6, '0');

                            //Totalização da Cobrança Simples
                            //Campo     Nome do campo                                 de     ate        "Picture"     Conteúd
                            //06.5      Quantidade de Títulos em Cobrança Simples      24     29            9(006       Preencher com a Quantidade total de títulos informados no lote
                            LINHA_TRAILLER_LOTE += contadorlinha.ToString().PadLeft(6, '0');

                            //Totalização da Cobrança Simples
                            //Campo     Nome do campo                                               de     ate        "Picture"     Conteúd
                            //07.5      Valor Total dos Títulos em Carteiras de Cobrança Simples    30      46          9(017)      Preencher com o Valor total de títulos informados no lote
                            LINHA_TRAILLER_LOTE += RetiraLetras(ValorTotalDuplicatas.ToString("n2")).PadLeft(17, '0');

                            //Totalização da Cobrança Caucionada
                            //Campo     Nome do campo                                               de     ate        "Picture"     Conteúd
                            //08.5      Quantidade de Títulos em cobranças Caucionadas              47      52           9(006)     Preencher com zeros
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(6, '0');

                            //Totalização da Cobrança Caucionada
                            //Campo     Nome do campo                                               de     ate        "Picture"     Conteúd
                            //09.5      Valor Total dos Títulos em Carteiras Caucionadas            53      69          9(017)  Preencher com zeros 
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(17, '0');

                            //Totalização da Cobrança Descontada
                            //Campo     Nome do campo                                               de     ate        "Picture"     Conteúd
                            //10.5      Quantidade de títulos em Cobrança Descontada                70      75          9(006)      Preencher com zeros 
                           LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(6, '0');

                            //Totalização da Cobrança Descontada
                            //Campo     Nome do campo                                               de     ate        "Picture"     Conteúd
                            //11.5      Quantidade de Títulos em Carteiras Descontadas              76      92          9(006)      Preencher com zeros 
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(17, '0');


                            //CNAB 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //12.5      Filler              93      123         X(031)          Preencher com espaços 
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(31, ' ');

                            //CNAB 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //13.5      Filler              124      240         X(117)          Preencher com espaços 
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(117, ' ');

                            sw.WriteLine(LINHA_TRAILLER_LOTE);
                            //FIM  - REGISTRO TRAILLER DO LOTE		
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                            //INICIO - REGISTRO TRAILLER DO ARQUIVO	
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            string linha3 = null;

                            //Controle 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //01.9      Código do Banco     1       3           9(003)      Preencher '104’ 
                            linha3 += "104";

                            //Controle 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //02.9      Lote de Serviço     4       7           9(004)      Preencher '9999' 
                            linha3 += "9999";

                            //Controle 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //03.9      Tipo de Registro    8       8                       Preencher '9’ (equivale a Trailer de  Arquivo) 
                            linha3 += "9";

                            
                            //CNAB 
                            //Campo     Nome do campo        de     ate        "Picture"     Conteúd
                            //04.9      Filler              9       17          X(009)          Preencher com espaços 
                            linha3 += string.Empty.ToString().PadRight(9, ' ');

                            //Totais 
                            //Campo     Nome do campo                   de     ate        "Picture"     Conteúd
                            //05.9      Quantidade de Lotes do Arquivo  18      23          9(006)      Informar o Número total de lotes enviados no arquivo; trata - se da somatória dos registros de tipo 1, incluindo header e trailer
                            linha3 += "000001";

                            //Totais 
                            //Campo     Nome do campo                           de     ate        "Picture"     Conteúd
                            //06.9      Quantidade de Registros do Arquivo      24      29          9(006)      Somatória dos registros de tipo 1, 3, e 5. 
                            //  linha3 += SomatorioArquivoLote2.ToString().PadRight(6, '0'); //
                            SomatorioArquivoLote = SomatorioArquivoLote +2;
                            linha3 += SomatorioArquivoLote.ToString().PadLeft(6, '0'); //

                            //CNAB
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //07.9      Filler                  30      35          X(006)      Preencher com espaços 
                            linha3 += string.Empty.ToString().PadRight(6, ' ');


                            //CNAB
                            //Campo     Nome do campo           de     ate        "Picture"     Conteúd
                            //08.9      Filler                  36     240          X(205)       Preencher com espaços 
                            linha3 += string.Empty.ToString().PadRight(205, ' ');

                            sw.WriteLine(linha3);

                            //FIM - TRAILLER
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


                            //Mensagem de confirmação
                            MessageBox.Show("Exportado com sucesso", "Exportado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }

                        catch (Exception ex)
                        {
                            sw.Close();
                            MessageBox.Show(LocalErro);
                            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            //Fechar stream SEMPRE
                            sw.Close();

                            if (File.Exists(caminho))
                                SalvaArquivoRemessa(caminho);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe pesquisa selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ArquivoRemessaSICOOB_CNAB240(LIS_DUPLICATARECEBERCollection DuplicatasColl, string extensao)
        {
            CONFIGBOLETAEntity CONFIGBOLETATy = new CONFIGBOLETAEntity();
            CONFIGBOLETAProvider CONFIGBOLETAP = new CONFIGBOLETAProvider();
            CONFIGBOLETATy = CONFIGBOLETAP.Read(16); //16 Sicoob
            string caminho = string.Empty;
            int sequencia = BuscaSequenciaRemessa();
            string LocalErro = string.Empty;

            if (CONFIGBOLETATy == null)
                MessageBox.Show("Configuração do boleto SICOOB não localizado!");

            if (DuplicatasColl.Count > 0 && CONFIGBOLETATy != null)
            {

                DialogResult dr = MessageBox.Show("Deseja Gerar o Arquivo em Formato CNAB240 (Remessa_SICOOB." + extensao + ") ?",
                                 ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {

                    System.IO.StreamWriter sw = null;                  

                    //Escolher onde salvar o arquivo
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.FileName = "Remessa_SICOOB_" + sequencia.ToString().PadLeft(7, '0') + "." + extensao;
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    sfd.Filter = "arquivos " + extensao + " (*." + extensao + ")|*." + extensao + "";

                    int SomatorioArquivoLote = 0;
                    //Se usuário escolher nome corretamente e clicar em salvar
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            //Pega o caminho do arquivo
                            caminho = sfd.FileName;
                            //Cria um StreamWriter no local
                            sw = new System.IO.StreamWriter(caminho, false, System.Text.Encoding.GetEncoding(1252));


                            string linha = null;

                            ///////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            //INICIO - HEADER	

                            //SEQ	Posição		Nº Dig	Nº Dec	Form	Campo / Descrição / Conteúdo 							
                            //      De	Até

                            //ok - 01.0	001	003	    003	-	Num	Controle¹	Banco			Código do Sicoob na Compensação: "756"
                            linha += "756";

                            //ok - 02.0	004	007	    004	-	Num		Lote			        Lote de Serviço: "0000"
                            linha += "0000";

                            //ok - 03.0	008	008	    001	-	Num		Registro			    Tipo de Registro: "0"
                            linha += "0";
                            SomatorioArquivoLote = SomatorioArquivoLote + 1;

                            //ok -04.0	009	017	    009	-	Alfa	CNAB				Uso Exclusivo FEBRABAN / CNAB: Brancos		
                            linha += string.Empty.ToString().PadRight(9, ' ');

                            //ok - 05.0	018	018	    001	-	Num	Empresa²	Inscrição	Tipo		"Tipo de Inscrição da Empresa:
                            //'1'  =  CPF
                            //'2'  =  CGC / CNPJ"
                            if (CONFIGBOLETATy.CPFCNPJCEDENTE.Length > 14)
                                linha += "2";
                            else
                                linha += "1";

                           //ok - 06.0	019	032	    014	-	Num			Número		Número de Inscrição da Empresa		
                            linha += RetiraLetras(CONFIGBOLETATy.CPFCNPJCEDENTE).ToString().PadLeft(14, '0');

                            //ok - 07.0	033	052	    020	-	Alfa		Convênio			Código do Convênio no Sicoob: Brancos		
                            string CodigoCedente = CONFIGBOLETATy.CODCEDENTE.ToString() + CONFIGBOLETATy.DIGCEDENTE.ToString();
                            linha += CodigoCedente.ToString().PadRight(20, ' ');

                            //ok -  08.0	053	057	    005	-	Num	Prefixo da Cooperativa: vide planilha "Capa" deste arquivo		Conta Corrente³	Agência	Código	
                            linha += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                           //ok 09.0	058	058	    001	-	Alfa				DV	Dígito Verificador do Prefixo: vide planilha "Capa" deste arquivo	
                            linha += CONFIGBOLETATy.DIGAGENCIA.ToString().PadLeft(1, ' ');

                           //  10.0	059	070	    012	-	Num			Conta	Número	Conta Corrente: vide planilha "Capa" deste arquivo	
                            linha += CONFIGBOLETATy.CONTA.ToString().PadLeft(12, '0');

                           //  11.0	071	071	    001	-	Alfa				DV	Dígito Verificador da Conta: vide planilha "Capa" deste arquivo		
                            linha += CONFIGBOLETATy.DIGCONTA.ToString().PadLeft(1, ' ');

                           //  12.0	072	072	    001	-	Alfa			DV		Dígito Verificador da Ag/Conta: Brancos	
                            linha += string.Empty.ToString().PadRight(1, ' ');

                            //  13.0	073	    102	    030	-	Alfa		Nome			Nome da Empresa		
                            linha += LimiterText(CONFIGBOLETATy.NOMECEDENTE, 30).PadRight(30, ' '); 

                            //14.0	103	132	    030	-	Alfa	Nome do Banco				Nome do Banco: SICOOB		
                            linha += LimiterText("SICOOB", 30).PadRight(30, ' '); 

                           // 15.0	133	142	    010	-	Alfa	CNAB 				Uso Exclusivo FEBRABAN / CNAB: Brancos		
                            linha += string.Empty.ToString().PadRight(10, ' ');

                            //16.0	143	143	    001	-	Num	Arquivo	Código			Código Remessa / Retorno: "1"		
                            linha += "1";

                            //17.0	144	151	    008	-	Num		Data de Geração			Data de Geração do Arquivo		
                            linha += DateTime.Now.ToString("ddMMyyyy");

                            //18.0	152	157	    006	-	Num		Hora de Geração			Hora de Geração do Arquivo		
                            linha += DateTime.Now.ToString("HHmmss");

                            //124072015 1927 0100000308100000
                            //111092015 1454 000013 08100000
                          //  19.0	158	163	    006	-	Num		Seqüência (NSA)			
                            //Número Seqüencial do Arquivo: Número seqüencial adotado e controlado pelo responsável pela
                            //geração do arquivo para ordenar a disposição dos arquivos encaminhados. Evoluir um número seqüencial a cada header de arquivo.		
                            linha += sequencia.ToString().PadLeft(6, '0');

                            //20.0	164	166	    003	-	Num		Layout do Arquivo			No da Versão do Layout do Arquivo: "081"		
                            linha += "087";

                            //21.0	167	171	005	-	Num		Densidade			Densidade de Gravação do Arquivo: "00000"		
                            linha += "00000";

                            //22.0	172	191	    020	-	Alfa	Reservado Banco				Para Uso Reservado do Banco: Brancos		
                            linha += string.Empty.ToString().PadRight(20, ' ');

                           // 23.0	192	211	    020	-	Alfa	Reservado Empresa				Para Uso Reservado da Empresa: Brancos		
                            linha += string.Empty.ToString().PadRight(20, ' ');

                           // 24.0	212	240	    029	-	Alfa	CNAB				Uso Exclusivo FEBRABAN / CNAB: Brancos		
                            linha += string.Empty.ToString().PadRight(29, ' ');

                            sw.WriteLine(linha);
                            //FIM - HEADER					
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\                        

                            //INICIO - HEADER DO LOTE										
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            string linha_HEADER_LOTE = null;

                            //SEQ	Posição		NºDig	Nº 	Form	Campo / Descrição / Conteúdo	
                            //01.1	001	003	    003	-	Num	Controle	Banco			Código do Banco na Compensação: "756"		
                            linha_HEADER_LOTE += "756";
                          					
                           //02.1	004	007	    004	-	Num		Lote			
                            //"Lote de Serviço: Número seqüencial para identificar univocamente um lote de serviço. 
                            //Criado e controlado pelo responsável pela geração magnética dos dados contidos no arquivo.
                            // Preencher com '0001' para o primeiro lote do arquivo. Para os demais: número do
                            //lote anterior acrescido de 1. O número não poderá ser repetido dentro do arquivo."		
                            linha_HEADER_LOTE += "0001";

                            //03.1	008	008	    001	-	Num		Registro			Tipo de Registro: "1"		
                            linha_HEADER_LOTE += "1";

                           //04.1	009	009	    001	-	Alfa	Serviço	Operação			Tipo de Operação: "R"		
                            linha_HEADER_LOTE += "R";
                            SomatorioArquivoLote = SomatorioArquivoLote + 1;

                            //05.1	010	011	    002	-	Num		Serviço			Tipo de Serviço: "01"		
                            linha_HEADER_LOTE += "01";

                            //06.1	012	013	    002	-	Alfa		CNAB			Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(2, ' ');

                            //07.1	014	016	    003	-	Num		Layout do Lote			Nº da Versão do Layout do Lote: "040"		
                            linha_HEADER_LOTE += "045";

                            //08.1	017	017	    001	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(1, ' ');

                           //09.1	018	018	001	-	Num	Empresa	Inscrição	Tipo		"Tipo de Inscrição da Empresa:
                            //'1'  =  CPF
                            //'2'  =  CGC / CNPJ"
                            if (CONFIGBOLETATy.CPFCNPJCEDENTE.Length > 14)
                                linha_HEADER_LOTE += "2";
                            else
                                linha_HEADER_LOTE += "1";

                            //10.1	019	033	    015	-	Num			Número		Nº de Inscrição da Empresa		
                            linha_HEADER_LOTE += RetiraLetras(CONFIGBOLETATy.CPFCNPJCEDENTE).ToString().PadLeft(15, '0');

                            //11.1	034	053	    020	-	Alfa		Convênio			Código do Convênio no Banco: Brancos
                            linha_HEADER_LOTE += CodigoCedente.ToString().PadRight(20, ' ');

                            //12.1	054	058	    005	-	Num		C/C	Agência	Código	Prefixo da Cooperativa: vide planilha "Capa" deste arquivo		
                            linha_HEADER_LOTE += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                            //13.1	059	059	    001	-	Alfa				DV	Dígito Verificador do Prefixo: vide planilha "Capa" deste arquivo		
                            linha_HEADER_LOTE += CONFIGBOLETATy.DIGAGENCIA.ToString().PadRight(1, ' ');

                            //14.1	060	071	    012	-	Num			Conta	Número	Conta Corrente: vide planilha "Capa" deste arquivo		
                            linha_HEADER_LOTE += CONFIGBOLETATy.CONTA.ToString().PadLeft(12, '0');

                            //15.1	072	072	    001	-	Alfa				DV	Dígito Verificador da Conta: vide planilha "Capa" deste arquivo		
                            linha_HEADER_LOTE += CONFIGBOLETATy.DIGCONTA.ToString().PadRight(1, ' ');

                            //16.1	073	073	    001	-	Alfa			DV		Dígito Verificador da Ag/Conta: Brancos		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(1, ' ');

                            //17.1	074	103	    030	-	Alfa		Nome			Nome da Empresa		
                            linha_HEADER_LOTE += LimiterText(CONFIGBOLETATy.NOMECEDENTE, 30).PadRight(30, ' ');
 
                            //18.1	104	143	    040	-	Alfa	Informação 1				
                            //"Mensagem 1: Texto referente a mensagens que serão impressas em todos os boletos
                            //referentes ao mesmo lote. Estes campos não serão utilizados no arquivo retorno."		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(40, ' ');

                            //19.1	144	183	    040	-	Alfa	Informação 2				
                            //"Mensagem 2: Texto referente a mensagens que serão impressas em todos os boletos
                            //referentes ao mesmo lote. Estes campos não serão utilizados no arquivo retorno."	
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(40, ' ');

                            //20.1	184	191	    008	-	Num	Controle da Cobrança		
                            //Nº Rem./Ret.		Número Remessa/Retorno: Número adotado e controlado pelo 
                            //responsável pela geração magnética dos dados contidos no arquivo para identificar 
                            //a seqüência de envio ou devolução do arquivo entre o Cedente e o Sicoob.		
                            linha_HEADER_LOTE += sequencia.ToString().PadLeft(8, '0');

                           //21.1	192	199	    008	-	Num			Dt. Gravação		Data de Gravação Remessa/Retorno		
                            linha_HEADER_LOTE += DateTime.Now.ToString("ddMMyyyy");

                            //22.1	200	207	    008	-	Num	Data do Crédito				Data do Crédito: "00000000"		
                            linha_HEADER_LOTE += "00000000";

                            //23.1	208	240	    033	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            linha_HEADER_LOTE += string.Empty.ToString().PadRight(33, ' ');


                            sw.WriteLine(linha_HEADER_LOTE);
                            //FIM  - HEADER DO LOTE					
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\  

                            int contadorlinha = 0;
                            decimal ValorTotalDuplicatas = 0;
                            int ContadorSequencia = 1;
                            foreach (LIS_DUPLICATARECEBEREntity item in DuplicatasColl)
                            {
                                if (item.IDDUPLICATARECEBER != null && item.IDSTATUS != 3) //3 Pago 
                                {

                                    LocalErro = "Erro na Duplicata n. " + item.NUMERO + " Cliente: " + item.NOMECLIENTE;
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                    //INICIO - REGISTRO DETALHE SEGMENTO P	
                                    string LINHA_DETALHE_SEGMENTO_P = string.Empty;

                                    //Dados do Cliente
                                    CLIENTEEntity ClieteTy = new CLIENTEEntity();
                                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                                    ClieteTy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));

                                   //ok -  01.3P	001	003	003	-	Num	Controle¹	Banco			Código do Banco na Compensação: "756"		
                                    LINHA_DETALHE_SEGMENTO_P += "756";

                                    //ok - 02.3P	004	007	004	-	Num		Lote			
                                    //"Lote de Serviço: Número seqüencial para identificar univocamente um lote de serviço. 
                                    //Criado e controlado pelo responsável pela geração magnética dos dados contidos no arquivo.
                                    //Preencher com '0001' para o primeiro lote do arquivo. Para os demais: número do lote anterior 
                                    //acrescido de 1. O número não poderá ser repetido dentro do arquivo."		
                                    LINHA_DETALHE_SEGMENTO_P += "0001";

                                    //ok - 03.3P	008	008	001	-	Num		Registro			Tipo de Registro: "3"		
                                    LINHA_DETALHE_SEGMENTO_P += "3";
                                    
                                      //ok- 04.3P	009	013	005	-	Num	Serviço	Nº do Registro			
                                      //"Nº Sequencial do Registro no Lote: Número adotado e controlado pelo 
                                      //responsável pela geração magnética dos dados contidos no arquivo, para identificar a 
                                      //seqüência de registros encaminhados no lote. Deve ser inicializado sempre em '1', em cada novo lote."		
                                    LINHA_DETALHE_SEGMENTO_P += ContadorSequencia.ToString().PadLeft(5, '0');
                                      ContadorSequencia++;

                                    //ok - 05.3P	014	014	001	-	Alfa		Segmento			Cód. Segmento do Registro Detalhe: "P"		
                                    LINHA_DETALHE_SEGMENTO_P += "P";
                                    SomatorioArquivoLote = SomatorioArquivoLote + 1;

                                    //ok - 06.3P	015	015	001	-	Alfa		CNAB			Uso Exclusivo FEBRABAN/CNAB: Brancos		
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(1, ' ');

                                    //ok - 07.3P	016	017	002	-	Num		Cód. Mov.			"Código de Movimento Remessa:
                                    //'01'  =  Entrada de Títulos
                                    //'02'  =  Pedido de Baixa
                                    //'04'  =  Concessão de Abatimento
                                    //'05'  =  Cancelamento de Abatimento
                                    //'06'  =  Alteração de Vencimento
                                    //'07'  =  Concessão de Desconto
                                    //'08'  =  Cancelamento de Desconto
                                    //'09'  =  Protestar
                                    //'10'  =  Sustar Protesto e Baixar Título
                                    //'11'  =  Sustar Protesto e Manter em Carteira
                                    //‘12’ = Alteração de Juros de Mora
                                    //‘13’ = Dispensar Cobrança de Juros de Mora
                                    //‘14’ = Alteração de Valor/Percentual de Multa
                                    //‘15’ = Dispensar Cobrança de Multa
                                    //‘16’ = Alteração do Valor de Desconto
                                    //‘17’ = Não conceder Desconto
                                    //‘18’ = Alteração do Valor de Abatimento 
                                    //'30'  =  Recusa da Alegação do Sacado
                                    //'31'  =  Alteração de Outros Dados
                                    //'33'  =  Alteração dos Dados do Rateio de Crédito
                                    //'34'  =  Pedido de Cancelamento dos Dados do Rateio de Crédito
                                    //'35'  =  Pedido de Desagendamento do Débito Automático
                                    //'40'  =  Alteração de Carteira"
                                    LINHA_DETALHE_SEGMENTO_P += "01";

                                    //ok - 08.3P	018	022	005	-	Num	C/C	Agência	Código		Prefixo da Cooperativa: vide planilha "Capa" deste arquivo		
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.AGENCIA.ToString().PadLeft(5, '0');

                                    //ok - 09.3P	023	023	001	-	Alfa			DV		Dígito Verificador do Prefixo: vide planilha "Capa" deste arquivo		
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.DIGAGENCIA.ToString().PadRight(1, ' ');

                                    //ok - 10.3P	024	035	012	-	Num		Conta	Número		Conta Corrente: vide planilha "Capa" deste arquivo	
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.CONTA.ToString().PadLeft(12, '0');

                                     //ok - 11.3P	036	036	001	-	Alfa			DV		Dígito Verificador da Conta: vide planilha "Capa" deste arquivo		
                                    LINHA_DETALHE_SEGMENTO_P += CONFIGBOLETATy.DIGCONTA.ToString().PadRight(1, ' ');

                                     //ok - -12.3P	037	037	001	-	Alfa		DV			Dígito Verificador da Ag/Conta: Brancos		
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(1, ' ');

                                    //ok - 13.3P	038	057	020	-	Alfa	Nosso Número				"Nosso Número:
                                    //- Se emissão a cargo do Sicoob (vide planilha ""Capa"" deste arquivo): Brancos
                                    //- Se emissão a cargo do Cedente (vide planilha ""Capa"" deste arquivo):
                                    //     NumTitulo - 10 posições (1 a 10) 
                                    //     Parcela - 02 posições (11 a 12) - ""01"" se parcela única
                                    //     Modalidade - 02 posições (13 a 14) - vide planilha ""Capa"" deste arquivo
                                    //     Tipo Formulário - 01 posição  (15 a 15):
                                    //          ""1"" -auto-copiativo
                                    //          ""3""-auto-envelopável
                                    //          ""4""-A4 sem envelopamento
                                    //          ""6""-A4 sem envelopamento 3 vias
                                    //     Em branco - 05 posições (16 a 20)"                                    
                                     int LocalTraco = item.NUMERO.IndexOf("-");
                                     if (LocalTraco != -1)
                                     {
                                         string NumeroDuplicata = RetiraLetras(item.NUMERO).ToString().PadLeft(9, '0');
                                         LINHA_DETALHE_SEGMENTO_P += NumeroDuplicata;
                                         int DV =  DVNossoNumeroSicoob(CONFIGBOLETATy.AGENCIA, Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE), CONFIGBOLETATy.DIGCEDENTE, Convert.ToInt32(RetiraLetras(item.NUMERO)));
                                         LINHA_DETALHE_SEGMENTO_P += DV;
                                     }
                                     else
                                     {
                                         string NumeroDuplicata = RetiraLetras(item.NUMERO).ToString().PadLeft(9, '0');
                                         int DV = DVNossoNumeroSicoob(CONFIGBOLETATy.AGENCIA, Convert.ToInt32(CONFIGBOLETATy.CODCEDENTE), CONFIGBOLETATy.DIGCEDENTE, Convert.ToInt32(NumeroDuplicata));
                                         LINHA_DETALHE_SEGMENTO_P += NumeroDuplicata;
                                         LINHA_DETALHE_SEGMENTO_P += DV;
                                     }

                                     //Parcela
                                     if (LocalTraco != -1)
                                     {
                                         string NumeroParcela = item.NUMERO.Substring(LocalTraco + 1, 1);
                                         LINHA_DETALHE_SEGMENTO_P += NumeroParcela.PadLeft(2, '0');
                                     }
                                     else
                                         LINHA_DETALHE_SEGMENTO_P += "01";                                       

                                    //Modalidade
                                     LINHA_DETALHE_SEGMENTO_P += "01";
                                     // Tipo Formulário
                                     LINHA_DETALHE_SEGMENTO_P += "4";

                                     //Em branco - 05 posições (16 a 20)"
                                     LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(05, ' ');

                                    //ok - 14.3P	058	058	001	-	Num	Característica Cobrança	Carteira			Código da Carteira: vide planilha "Capa" deste arquivo		
                                     LINHA_DETALHE_SEGMENTO_P += "1";

                                   //ok - 15.3P	059	059	001	-	Num		Cadastramento			Forma de Cadastr. do Título no Banco: "0"		
                                     LINHA_DETALHE_SEGMENTO_P += "0";

                                    //ok- 16.3P	060	060	001	-	Alfa		Documento			Tipo de Documento: Brancos		
                                     LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(01, ' ');

                                     //ok - 17.3P	061	061	001	-	Num		Emissão Boleto			"Identificação da Emissão do Boleto: (vide planilha ""Capa"" deste arquivo)
                                     //     '1'  =  Sicoob Emite
                                     //     '2'  =  Cedente Emite"	
                                     LINHA_DETALHE_SEGMENTO_P += "2";

                                     //ok - 18.3P	062	062	001	-	Alfa		Distrib. Boleto			"Identificação da Distribuição do Boleto: (vide planilha ""Capa"" deste arquivo)
                                     // '1'  =  Sicoob Distribui
                                     // '2'  =  Cedente Distribui"	
                                     LINHA_DETALHE_SEGMENTO_P += "2";

                                     // ok - 19.3P	063	077	015	-	Alfa	Nº do Documento				
                                     //"Número do Documento de Cobrança: Número adotado e controlado pelo Cliente,
                                     //para identificar o título de cobrança. Informação utilizada pelo Sicoob para referenciar
                                     //a identificação do documento objeto de cobrança. Poderá conter número de duplicata, no caso
                                     //de cobrança de duplicatas; número da apólice, no caso de cobrança de seguros, etc"		
                                    if (item.NUMERO.Trim().Length > 0)
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NUMERO).ToString().PadLeft(15,  '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NOTAFISCAL).ToString().PadLeft(15, '0');

                                    //ok - 20.3P	078	085	008	-	Num	Vencimento				Data de Vencimento do Título
                                    LINHA_DETALHE_SEGMENTO_P += Convert.ToDateTime(item.DATAVECTO).ToString("ddMMyyyy");

                                    //ok - 21.3P	086	100	013	002	Num	Valor do Título				Valor Nominal do Título	
                                     string ValorDuplicata = Convert.ToDecimal(item.VALORDUPLICATA).ToString("n2");
                                     ValorDuplicata = RetiraLetras(ValorDuplicata).PadLeft(15, '0');
                                     LINHA_DETALHE_SEGMENTO_P += ValorDuplicata;

                                   //ok -  22.3P	101	105	005	-	Num	Ag. Cobradora				Agência Encarregada da Cobrança: "00000"		
                                     LINHA_DETALHE_SEGMENTO_P += "00000";
                                   
                                    //ok - 23.3P	106	106	001	-	Alfa	DV				Dígito Verificador da Agência: Brancos		
                                     LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(01, ' ');

                                    //ok - 24.3P	107	108	002	-	Num	Espécie de Título				"Espécie do Título:
                                    //'01'  =  CH Cheque
                                    //'02'  =  DM Duplicata Mercantil
                                    //'03'  =  DMI Duplicata Mercantil p/ Indicação
                                    //'04'  =  DS Duplicata de Serviço
                                    //'05'  =  DSI Duplicata de Serviço p/ Indicação
                                    //'06'  =  DR Duplicata Rural
                                    //'07'  =  LC Letra de Câmbio
                                    //'08'  =  NCC Nota de Crédito Comercial
                                    //'09'  =  NCE Nota de Crédito a Exportação
                                    //'10'  =  NCI Nota de Crédito Industrial
                                    //'11'  =  NCR Nota de Crédito Rural
                                    //'12'  =  NP Nota Promissória
                                    //'13'  =  NPR Nota Promissória Rural
                                    //'14'  =  TM Triplicata Mercantil
                                    //'15'  =  TS Triplicata de Serviço
                                    //'16'  =  NS Nota de Seguro
                                    //'17'  =  RC Recibo
                                    //'18'  =  FAT Fatura
                                    //'19'  =  ND Nota de Débito
                                    //'20'  =  AP Apólice de Seguro
                                    //'21'  =  ME Mensalidade Escolar
                                    //'22'  =  PC Parcela de Consórcio
                                    //'23'  =  NF Nota Fiscal
                                    //'24'  =  DD Documento de Dívida
                                    //‘25’ = Cédula de Produto Rural 
                                    //'99'  =  Outros"	
                                     LINHA_DETALHE_SEGMENTO_P += "02";

                                    //ok - 25.3P	109	109	001	-	Alfa	Aceite		
                                    //"Identific. de Título Aceito/Não Aceito: Código adotado pela FEBRABAN para identificar se
                                    //o título de cobrança foi aceito (reconhecimento da dívida pelo Sacado).
                                    //'A'  =  Aceite
                                    //'N'  =  Não Aceite"	
                                     LINHA_DETALHE_SEGMENTO_P += "N";

                                   //ok- 26.3P	110	117	008	-	Num	Data Emissão do Título				Data da Emissão do Título		
                                     LINHA_DETALHE_SEGMENTO_P += Convert.ToDateTime(item.DATAEMISSAO).ToString("ddMMyyyy");

                                    //ok - 27.3P	118	118	001	-	Num	Juros	Cód. Juros Mora			"Código do Juros de Mora:
                                    //'1'  =  Valor por Dia
                                    //'2'  =  Taxa Mensal
                                    //'3'  =  Isento"		
                                    // LINHA_DETALHE_SEGMENTO_P += "3";
                                    LINHA_DETALHE_SEGMENTO_P += "0";

                                    //ok - 28.3P	119	126	008	-	Num		Data Juros Mora 			Data do Juros de Mora		
                                    LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(08, '0');

                                    //ok - 29.3P	127	141	013	002	Num		Juros Mora			Juros de Mora por Dia/Taxa		
                                    LINHA_DETALHE_SEGMENTO_P += "000000000000000";

                                    //ok - 30.3P	142	142	001	-	Num	Desc 1	Cód. Desc. 1			"Código do Desconto 1
                                    //'1'  =  Valor Fixo Até a Data Informada
                                    //'2'  =  Percentual Até a Data Informada"		
                                     LINHA_DETALHE_SEGMENTO_P += "0";
                                   
                                    //ok - 31.3P	143	150	008	-	Num		Data Desc. 1			Data do Desconto 1		
                                     LINHA_DETALHE_SEGMENTO_P += "00000000";

                                    //ok - 32.3P	151	165	013	002	Num		Desconto 1			Valor/Percentual a ser Concedido	
                                     LINHA_DETALHE_SEGMENTO_P += "000000000000000";

                                    //ok - 33.3P	166	180	013	002	Num	Vlr IOF				Valor do IOF a ser Recolhido	
                                     LINHA_DETALHE_SEGMENTO_P += "000000000000000";

                                    //ok - 34.3P	181	195	013	002	Num	Vlr Abatimento				Valor do Abatimento	
                                     LINHA_DETALHE_SEGMENTO_P += "000000000000000";

                                    //ok - 35.3P	196	220	025	-	Alfa	Uso Empresa Cedente			
                                    //Identificação do Título na Empresa: Campo destinado para uso do Cedente para identificação do Título.		
                                    if (item.NUMERO.Trim().Length > 0)
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NUMERO).ToString().PadLeft(25, '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_P += RetiraLetras(item.NOTAFISCAL).ToString().PadLeft(25, '0');

                                    //ok - 36.3P	221	221	001	-	Num	Código p/ Protesto				Código para Protesto: "1"	
                                    LINHA_DETALHE_SEGMENTO_P += "1";

                                    //ok - 37.3P	222	223	002	-	Num	Prazo p/ Protesto				Número de Dias Corridos para Protesto
                                     LINHA_DETALHE_SEGMENTO_P += "00";

                                    //ok - 38.3P	224	224	001	-	Num	Código p/ Baixa/Devolução				Código para Baixa/Devolução: "0"		
                                     LINHA_DETALHE_SEGMENTO_P += "0";

                                    //ok - 39.3P	225	227	003	-	Alfa	Prazo p/ Baixa/Devolução				Número de Dias para Baixa/Devolução: Brancos		
                                     LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(03, ' ');

                                    //ok -  40.3P	228	229	002	-	Num	Código da Moeda				"Código da Moeda:
                                    //'02'  =  Dólar Americano Comercial (Venda)
                                    //'09'  = Real"	
                                    LINHA_DETALHE_SEGMENTO_P += "09";

                                    //41.3P	230	239	010	-	Num	Número do Contrato				Nº do Contrato da Operação de Créd.: "0000000000"		
                                     LINHA_DETALHE_SEGMENTO_P += "0000000000";

                                    //42.3P	240	240	001	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                                     LINHA_DETALHE_SEGMENTO_P += string.Empty.ToString().PadRight(1, ' ');
                                     sw.WriteLine(LINHA_DETALHE_SEGMENTO_P);
                                   
                                    //FIM - REGISTRO DETALHE SEGMENTO P	
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                                    //INICIO - REGISTRO DETALHE SEGMENTO Q
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                                    string LINHA_DETALHE_SEGMENTO_Q = null;

                                   //SEQ	Posição		Nº	    Nº	    Form	        Campo / Descrição / Conteúdo						
		                           //       De	Até	    Dig	    Dec								
                                   //01.3Q	1	3	    3	- 	        Num	Controle	Código do Banco na Compensação: "756"		
                                    LINHA_DETALHE_SEGMENTO_Q += "756";

                                    //02.3Q	4	7	    4	-	Num					
                                    //"Lote de Serviço: Número seqüencial para identificar univocamente um lote de serviço.
                                    //Criado e controlado pelo responsável pela geração magnética dos dados contidos no arquivo.
                                    //Preencher com '0001' para o primeiro lote do arquivo. Para os demais: número do lote anterior 
                                    //acrescido de 1. O número não poderá ser repetido dentro do arquivo."		
                                    LINHA_DETALHE_SEGMENTO_Q += "0001";

                                    //03.3Q	8	8	    1	-	Num					Tipo de Registro: "3"		
                                    LINHA_DETALHE_SEGMENTO_Q += "3";

                                    //04.3Q	9	13	    5	-	Num	Serviço			
                                    //"Nº Sequencial do Registro no Lote: Número adotado e controlado pelo responsável 
                                    //pela geração magnética dos dados contidos no arquivo, para identificar a 
                                    //seqüência de registros encaminhados no lote. Deve ser inicializado sempre em '1', em cada novo lote."		
                                   
                                    //Alterado dia 07/04/2017 16:48
                                    // LINHA_DETALHE_SEGMENTO_Q += sequencia.ToString().PadLeft(5, '0');
                                    LINHA_DETALHE_SEGMENTO_Q += ContadorSequencia.ToString().PadLeft(5, '0');
                                    ContadorSequencia++;

                                    //05.3Q	14	14	    1	-	Alfa					Cód. Segmento do Registro Detalhe: "Q"		
                                    LINHA_DETALHE_SEGMENTO_Q += "Q";
                                    SomatorioArquivoLote = SomatorioArquivoLote + 1;

                                    //06.3Q	15	15	    1	-	Alfa					Uso Exclusivo FEBRABAN/CNAB: Brancos		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(1, ' ');

                                    //07.3Q	16	17	    2	-	Num					"Código de Movimento Remessa:
                                    //'01'  =  Entrada de Títulos
                                    //'02'  =  Pedido de Baixa
                                    //'04'  =  Concessão de Abatimento
                                    //'05'  =  Cancelamento de Abatimento
                                    //'06'  =  Alteração de Vencimento
                                    //'07'  =  Concessão de Desconto
                                    //'08'  =  Cancelamento de Desconto
                                    //'09'  =  Protestar
                                    //'10'  =  Sustar Protesto e Baixar Título
                                    //'11'  =  Sustar Protesto e Manter em Carteira
                                    //‘12’ = Alteração de Juros de Mora
                                    //‘13’ = Dispensar Cobrança de Juros de Mora
                                    //‘14’ = Alteração de Valor/Percentual de Multa
                                    //‘15’ = Dispensar Cobrança de Multa
                                    //‘16’ = Alteração do Valor de Desconto
                                    //‘17’ = Não conceder Desconto
                                    //‘18’ = Alteração do Valor de Abatimento 
                                    //'30'  =  Recusa da Alegação do Sacado
                                    //'31'  =  Alteração de Outros Dados
                                    //'33'  =  Alteração dos Dados do Rateio de Crédito
                                    //'34'  =  Pedido de Cancelamento dos Dados do Rateio de Crédito
                                    //'35'  =  Pedido de Desagendamento do Débito Automático
                                    //'40'  =  Alteração de Carteira"		
                                    LINHA_DETALHE_SEGMENTO_Q += "01";

                                   //08.3Q	18	18	    1	-	Num	Dados do Sacado				"Tipo de Inscrição Sacado:
                                   //'1'  =  CPF
                                   //'2'  =  CGC / CNPJ"		
                                    if (ClieteTy.CPF != "   .   .   -")
                                        LINHA_DETALHE_SEGMENTO_Q += "1";
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += "2";

                                   //	09.3Q	19	33	    15	-	Num					Número de Inscrição		
                                    if (ClieteTy.CPF != "   .   .   -")
                                        LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CPF).ToString().PadLeft(15, '0');
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += RetiraLetras(ClieteTy.CNPJ).ToString().PadLeft(15, '0');

                                   //10.3Q	34	73	    40	-	Alfa					Nome		
                                    LINHA_DETALHE_SEGMENTO_Q += LimiterText(RemoverAcentos(ClieteTy.NOME), 40).ToString().PadRight(40, ' ');

                                    //11.3Q	74	113	40	-	Alfa					Endereço		
                                    LINHA_DETALHE_SEGMENTO_Q += LimiterText(RemoverAcentos(ClieteTy.ENDERECO1 + " " + ClieteTy.NUMEROENDER) , 40).ToString().PadRight(40, ' ');

                                   //12.3Q	114	128	15	-	Alfa					Bairro		
                                    LINHA_DETALHE_SEGMENTO_Q +=  LimiterText(RemoverAcentos(ClieteTy.BAIRRO1), 15).ToString().PadRight(15, ' ');

                                  //13.3Q	129	133	    5	-	Num					CEP		
                                    LINHA_DETALHE_SEGMENTO_Q += LimiterText(RemoverAcentos(ClieteTy.CEP1).ToString().PadLeft(5, '0') , 5);

                                    //14.3Q	134	136	3	-	Num					Sufixo do CEP	
                                    if (ClieteTy.CEP1 != "     -")
                                    {
                                        LocalTraco = ClieteTy.CEP1.IndexOf("-");
                                        string Sufixo = ClieteTy.CEP1.Substring(LocalTraco + 1, 3);
                                        LINHA_DETALHE_SEGMENTO_Q += Sufixo.PadLeft(3, '0'); 
                                    }
                                    else
                                        LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadLeft(3, '0');

                                    //	15.3Q	137	151 	15	-	Alfa					Cidade		
                                    MUNICIPIOSTy = MUNICIPIOSProvider.Read(Convert.ToInt32(ClieteTy.COD_MUN_IBGE));
                                    LINHA_DETALHE_SEGMENTO_Q += LimiterText(RemoverAcentos(MUNICIPIOSTy.MUNICIPIO), 15).PadRight(15, ' ');

                                    //16.3Q	152	153	2	-	Alfa					UF  - Unidade da Federação		
                                    ESTADOProvider ESTADOP = new ESTADOProvider();
                                    string UF = ESTADOP.Read(Convert.ToInt32(MUNICIPIOSTy.COD_UF_IBGE)).UF;
                                    LINHA_DETALHE_SEGMENTO_Q += LimiterText(UF, 2).PadRight(2, ' ');

                                    // 17.3Q	154	154	1	-	Num	Sac. / Aval.				"Tipo de Inscrição Sacador Avalista:
                                    //'1'  =  CPF
                                    //'2'  =  CGC / CNPJ"		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadLeft(1, '0');

                                    //	18.3Q	155	169	15	-	Num					Número de Inscrição		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadLeft(15, '0');

                                   //19.3Q	170	209	40	-	Alfa	Nome do Sacador/Avalista				Nome do Sacador/Avalista		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadLeft(40, ' ');

                                    //	20.3Q	210	212	3	-	Num	Cód. Bco. Corresp. na Compensação				"Cód. Bco. Corresp. na Compensação:
                                    //Caso o Cedente não tenha contratado a opção de Banco Correspondente com o Sicoob, preencher com ""000"";
                                    //Caso o Cedente tenha contratado a opção de Banco Correspondente com o Sicoob e a 
                                    //emissão seja a cargo do Sicoob (SEQ 17.3.P do Segmento P do Detalhe), preencher com ""001"" (Banco do Brasil)"		
                                    LINHA_DETALHE_SEGMENTO_Q +="000";

                                    //21.3Q	213	232	    20	-	Alfa	Nosso Nº no Banco Correspondente				Nosso Nº no Banco Correspondente: "Brancos"		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(20, ' ');

	                                //22.3Q	233	240	    8	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB		
                                    LINHA_DETALHE_SEGMENTO_Q += string.Empty.ToString().PadRight(8, ' ');

                                    sw.WriteLine(LINHA_DETALHE_SEGMENTO_Q);
                                    //FIM - REGISTRO DETALHE SEGMENTO Q
                                    //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                                   
                                   
                                    contadorlinha++;
                                    ValorTotalDuplicatas += Convert.ToDecimal(item.VALORDUPLICATA);
                                }

                               // ContadorSequencia++;
                            }


                            //INICIO - REGISTRO TRAILLER DO LOTE
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                            string LINHA_TRAILLER_LOTE = null;
                            //SEQ	Posição		Nº	    Nº	Form	Campo / Descrição / Conteúdo						
		                    //      De	Até	    Dig	    Dec								
                            //ok - 01.9	001	003	    003	-	Num	Controle		Banco		Código do Banco na Compensação: "756"		
                            LINHA_TRAILLER_LOTE += "756";
                            
                            //-ok 02.5	004	007	    004	-	Num		Lote			
                            //"Lote de Serviço: Número seqüencial para identificar univocamente um lote de serviço. 
                            //Criado e controlado pelo responsável pela geração magnética dos dados contidos no arquivo.
                             //Preencher com '0001' para o primeiro lote do arquivo. Para os demais: número do lote anterior 
                            //acrescido de 1. O número não poderá ser repetido dentro do arquivo."	
                            LINHA_TRAILLER_LOTE += "0001";

                            //ok -03.5	008	008	001	-	Num		Registro			Tipo de Registro: "5"
                            LINHA_TRAILLER_LOTE += "5";

                            //ok - 04.5	009	017	    009	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos	
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(9, ' ');

                            //ok - 05.5	018	023	006	-	Num	Qtde de Registros				Quantidade de Registros no Lote
                            LINHA_TRAILLER_LOTE += SomatorioArquivoLote.ToString().PadLeft(6, '0'); ;

                            //ok - 06.5	024	029	    006	-	Num	Totalização da Cobrança Simples				Quantidade de Títulos em Cobrança		
                            LINHA_TRAILLER_LOTE += contadorlinha.ToString().PadLeft(6, '0');

                            //ok - 07.5	030	046	    015	002	Num					Valor Total dosTítulos em Carteiras		
                            LINHA_TRAILLER_LOTE += RetiraLetras(ValorTotalDuplicatas.ToString("n2")).PadLeft(17, '0');

                            //ok - 08.5	047	052 	006	-	Num	Totalização da Cobrança Vinculada				Quantidade de Títulos em Cobrança		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadLeft(6, '0');

                            //ok - 09.5	053	069	    015	002	Num					Valor Total dosTítulos em Carteiras		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadLeft(17, '0');

                            //ok - 10.5	070	075	    006	-	Num	Totalização da Cobrança Caucionada				Quantidade de Títulos em Cobrança		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadLeft(6, '0');

                            //ok - 11.5	076	092	    015	002	Num					Quantidade de Títulos em Carteiras		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(17, '0');

                            //ok - 12.5	093	098	    006	-	Nim	Totalização da Cobrança Descontada				Quantidade de Títulos em Cobrança		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(6, '0');

                            //ok - 13.5	099	115	    015	002	Num					Valor Total dos Títulos em Carteiras
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(17, '0');

                            //ok - 14.5	116	123	    008	-	Alfa	N. do Aviso				Número do Aviso de Lançamento: Brancos		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(8, ' ');

                            //ok -15.5	124	240	    117	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            LINHA_TRAILLER_LOTE += string.Empty.ToString().PadRight(117, ' ');

                            sw.WriteLine(LINHA_TRAILLER_LOTE);
                            //FIM  - REGISTRO TRAILLER DO LOTE		
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                            //INICIO - REGISTRO TRAILLER DO ARQUIVO	
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                            string linha3 = null;
                            //01.9	001	003	003	-	Num	Controle		Banco		Código do Banco na Compensação: "756"		
                            linha3 += "756";
                            
                            //02.9	004	007	004	-	Num			Lote		Preencher com '9999'.		
                            linha3 += "9999";
                         
                            //03.9	008	008	001	-	Num			Registro		Tipo de Registro: "9"		
                            linha3 += "9";

                            //04.9	009	017	009	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            linha3 += string.Empty.ToString().PadRight(9, ' ');

                           // 05.9	018	023	006	-	Num	Totais		Qtde. de Lotes		Quantidade de Lotes do Arquivo		
                            linha3 += "000001";

                            //06.9	024	029	006	-	Num			Qtde. de Registros		Quantidade de Registros do Arquivo		
                            SomatorioArquivoLote = SomatorioArquivoLote + 2;
                            linha3 += SomatorioArquivoLote.ToString().PadLeft(6, '0');

                           // 07.9	030	035	006	-	Num			Qtde. de Contas Concil.		Qtde de Contas p/ Conc. (Lotes): "000000"		
                            linha3 += "000000";

                            //08.9	036	240	205	-	Alfa	CNAB				Uso Exclusivo FEBRABAN/CNAB: Brancos		
                            linha3 += string.Empty.ToString().PadRight(205, ' ');

                            sw.WriteLine(linha3);

                            //FIM - TRAILLER
                            //////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


                            //Mensagem de confirmação
                            MessageBox.Show("Exportado com sucesso", "Exportado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }

                        catch (Exception ex)
                        {
                            sw.Close();
                            MessageBox.Show(LocalErro);
                            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            //Fechar stream SEMPRE
                            sw.Close();

                            if (File.Exists(caminho))
                                SalvaArquivoRemessa(caminho);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe pesquisa selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        byte[] _ArquivoTxt = null;
        private void SalvaArquivoRemessa(string arquivo)
        {
            try
            {
                REMESSABANCOEntity REMESSABANCOTy = new REMESSABANCOEntity();
                REMESSABANCOProvider REMESSABANCOP = new REMESSABANCOProvider();
                REMESSABANCOTy.IDREMESSABANCO = -1;// INTEGER NOT NULL,
                REMESSABANCOTy.DATA = DateTime.Now;//           DATE,
              
                //Armazena Arquivo TXT
                _ArquivoTxt = GetTxt(arquivo);
                REMESSABANCOTy.ARQUIVO  = _ArquivoTxt;//       BLOB SUB_TYPE 0 SEGMENT SIZE 80,

                REMESSABANCOTy.SEGUENCIA = BuscaSequenciaRemessa();//     INTEGER,
                REMESSABANCOTy.IDBANCO = 15;//        INTEGER,//15 Sicoob
                REMESSABANCOTy.FLAGENVIADO   = "S";//  CHAR(1)

                //Desativado o salvamento do arquivo txt no banco de dados
                //Foi verificado que estava ocupado muito espado no banco de dados
                REMESSABANCOP.Save(REMESSABANCOTy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        public int BuscaSequenciaRemessa()
        {
            int result = 1;

            try
            {
                REMESSABANCOProvider REMESSABANCOP = new REMESSABANCOProvider();
                REMESSABANCOCollection REMESSABANCOColl = new REMESSABANCOCollection();

                REMESSABANCOColl = REMESSABANCOP.ReadCollectionByParameter(null, "SEGUENCIA DESC");

                if (REMESSABANCOColl.Count > 0)
                {
                    result = Convert.ToInt32(REMESSABANCOColl[0].SEGUENCIA) + 1;
                }

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar sequência da remessa");
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        public static byte[] GetTxt(string caminhoArquivoTxt)
        {
            FileStream fs = new FileStream(caminhoArquivoTxt, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }

      

        public int CodigodeUFIBGE(string UF)
        {
            int uf = -1;
            switch (UF)
            {
                case "AC": uf = 12; break;
                case "AL": uf = 27; break;
                case "AP": uf = 16; break;
                case "AM": uf = 13; break;
                case "BA": uf = 29; break;
                case "CE": uf = 23; break;
                case "DF": uf = 53; break;
                case "GO": uf = 52; break;
                case "MA": uf = 21; break;
                case "MG": uf = 31; break;
                case "ES": uf = 32; break;
                case "MS": uf = 50; break;
                case "MT": uf = 51; break;
                case "PA": uf = 15; break;
                case "PB": uf = 25; break;
                case "PE": uf = 26; break;
                case "PI": uf = 22; break;
                case "PR": uf = 41; break;
                case "RJ": uf = 33; break;
                case "RN": uf = 24; break;
                case "RO": uf = 11; break;
                case "RR": uf = 14; break;
                case "RS": uf = 43; break;
                case "SC": uf = 42; break;
                case "SE": uf = 28; break;
                case "SP": uf = 35; break;
                case "TO": uf = 17; break;
            }

            return uf;
        }

        public string  BuscaEstadoCodigoUF(int codUF)
        {
            string uf = "";
            switch (codUF)
            {
                case 12: uf = "ACRE"; break;
                case 27: uf = "ALAGOAS"; break;
                case 16: uf = "AMAPA"; break;
                case 13: uf = "AMAZONA"; break;
                case 29: uf = "BAHIA"; break;
                case 23: uf = "CEARA"; break;
                case 53: uf = "DISTRITO FEDERAL"; break;
                case 52: uf = "GOIAS"; break;
                case 21: uf = "MARANHAO"; break;
                case 31: uf = "MINAS GERAIS"; break;
                case 32: uf = "ESPIRITO SANTO"; break;
                case 50: uf = "MATO GROSSO DO SUL"; break;
                case 51: uf = "MATO GROSOO"; break;
                case 15: uf = "PARA"; break;
                case 25: uf = "PARAIBA"; break;
                case 26: uf = "PERNAMBUCO"; break;
                case 22: uf = "PIAUI"; break;
                case 41: uf = "PARANA"; break;
                case 33: uf = "RIO DE JANEIRO"; break;
                case 24: uf = "RIO GRANDE DO NORTE"; break;
                case 11: uf = "RONDONIA"; break;
                case 14: uf = "RORAIMA"; break;
                case 43: uf = "RIO GRANDE DO SUL"; break;
                case 42: uf = "SANTA CATARINA"; break;
                case 28: uf = "SERGIPE"; break;
                case 35: uf = "SÃO PAULO"; break;
                case 17: uf = "TOCANTIS"; break;
            }

            return uf;
        }

        public int DVCodCedenteCAIXA(string CodCedente)
        {
            int result = 0;

            try
            {
                CodCedente = CodCedente.PadLeft(6,'0');                
                //2ª Linha 7 6 5 4 3 2 //Índice Multiplicação

                string[] Sequencia = new string[6];
                string[] ConstanteCalculo = { "7", "6", "5", "4", "3", "2"};
                string Sequencia1 = CodCedente;

                int j = 0;
                for (int x = 0; x < Sequencia1.Length; x++)
                {
                    string valoradd = Sequencia1.Substring(j, 1);
                    Sequencia[x] = valoradd;
                    j++;
                }

                int resultadoCalculo = 0;
                int i = 0;
                for (int x = 0; x < ConstanteCalculo.Length; x++)
                {
                    resultadoCalculo += Convert.ToInt32(ConstanteCalculo[x]) * Convert.ToInt32(Sequencia[i]);
                    i++;
                }

                int resto = resultadoCalculo % 11;

                int DV = 0;
                if (resto > 1)
                    DV = 11 - resto;

                result = DV;

                MessageBox.Show("DV Calculado com sucesso: " + CodCedente + "-" + result);

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        public int DVNossoNumeroSicoob(string Agencia, int CodigoCedente, string DigitoCedente, int NossoNumero)
        {
            int result = 0;

            try
            {
                //Exemplos
                //Número da Cooperativa: Como exemplo 0001 - 4 digitos
                //Código do Cliente: Como exemplo 1-9 - 10 digitos
                //Nosso Número: Numero sequencial Iniciado em 1. Como exemplo usaremos o número 45 - 7 digitos
                //Constante para Cálculo: 3197
                //Sequência Exemplo: 000100000000190000045
                //Constante 319731973197319731973

                string[] ConstanteCalculo = { "3", "1", "9", "7", "3", "1", "9", "7", "3", "1", "9", "7", "3", "1", "9", "7", "3", "1", "9", "7", "3" };
                string[] Sequencia = new string[21];

                string Sequencia1 = Agencia.ToString() + CodigoCedente.ToString().PadLeft(9, '0') + DigitoCedente.ToString().PadLeft(1, '0') + NossoNumero.ToString().PadLeft(7, '0');

                int j = 0;
                for (int x = 0; x < Sequencia1.Length; x++)
                {
                    string valoradd = Sequencia1.Substring(j, 1);
                    Sequencia[x] = valoradd;
                    j++;
                }

                //Na primeira etapa é necessário alinhar a sequência de acordo com as informações 
                //acima com a constate 3197 (repetindo-a do início para o fim da linha):

                //Ex:
                //Coop  |Cód. de Cliente | N. Número
                //Sequência  0001      0000000019        0000045
                //Constante 319731973197319731973
                //A segunda etapa deve-se multiplicar cada componente da sequência com o seu correspondente da
                //constante e somar os resultados
                //Ex.: 1*7 + 1*3 + 9*1 + 4*7 + 5*3 = 62
                //Obs.: Lembrando que os valores multiplicados por zeros foram omitidos.
                int resultadoCalculo = 0;
                int i = 0;
                for (int x = 0; x < ConstanteCalculo.Length; x++)
                {
                    resultadoCalculo += Convert.ToInt32(ConstanteCalculo[x]) * Convert.ToInt32(Sequencia[i]);
                    i++;
                }

                //Na terceira etapa deve-se calcular o resultado encontrado na operação anterior para determinar o valor do
                //resto sendo que para isso terá que ser efetuada a divisão do resultado com o Módulo 11.
                //Nesse processo não pode ser utilizada calculadora para efetuar essa operação, pois dará divergência ao encontrar
                //o valor do resto da operação.

                int resto = resultadoCalculo % 11;

                //Na quarta e última etapa é necessário apenas subtrair o resto da operação com o número 11. 
                //O resultado dessa subtração será o Dígito Verificador (DV) do Nosso Número.
                //Ex.:
                //11 - 7 = 4
                //O número 4 é o DV do nosso número 45 sendo assim o NN será 45-4

                //Observações:
                //Quando o Resto for igual a 0 ou 1 então o DV é igual a 0, pois ao subtrairmos 0 ou 1 do número 11 os resultados serão 10 ou 11, 
                //sendo que o DV do nosso número é composto por apenas um dígito. Então:
                //11 - 0 = 11
                //11 - 1 = 10

                int DV = 0;
                if (resto > 1)
                    DV = 11 - resto;

                result = DV;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar o DV do nosso numero: " + NossoNumero + " do Banco SICOOB");
                MessageBox.Show("Erro técnico: " + ex.Message);

                return result;
            }
        }      


        /// <summary>
        /// Download de arquivos via FTP
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <param name="local"></param>
        public Boolean BaixarArquivoFTP(string FileUrl, string local)
        {
            Boolean result = false;
            string url = "ftp://ftp.gratisphphost.info/htdocs/" + FileUrl;
			string usuario = "phpgr_19308995";
			string senha = "rmr87770";
		
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(usuario, senha);

                //Testar esta duas linha para bloqueio de FTP
                  request.Proxy = null;
                  request.UsePassive = false; 
                /////////////////////////////////////////////////

                request.UseBinary = true;               
                
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        using (FileStream ws = new FileStream(local, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);
                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }

                result = true;
                return result;

            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro ao baixar o arquivo pelo FTP: " + url);
                MessageBox.Show("Erro tecnico: " + ex.Message);
                                
            }
        }

        /// <summary>
        /// Download de arquivos
        /// </summary>
        /// <param name="LocalArquivo"></param>
        /// <param name="DestinoArquivo"></param>
        public void DownloadFile(string LocalArquivo, string DestinoArquivo)
        {
           try 
	        {	        

		         using (var client = new WebClient())
                {
                    client.DownloadFile(new Uri(LocalArquivo), DestinoArquivo);
                
                }
	        }
	         catch (Exception ex)
            {
                MessageBox.Show("Erro ao baixar o arquivo: " + LocalArquivo);
                MessageBox.Show("Erro tecnico: " + ex.Message);
            }
        }


       
    }
}