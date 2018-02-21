//Template gerado utilizando o MyGeneration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;
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

namespace BMSworks.UI
{
    public partial class Utility
    {

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


    }
}