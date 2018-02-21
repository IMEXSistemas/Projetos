using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace EmuladorNFCe.Useful
{
    public class HelperAcme
    {
        public static string GetValorCampo(string xml, string path)
        {
            string valor = "";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XPathNavigator nav = xd.CreateNavigator();
            XmlNamespaceManager ns = new XmlNamespaceManager(new NameTable());
            XPathExpression expr = nav.Compile(path);
            expr.SetContext(ns);
            XPathNodeIterator nodes = nav.Select(expr);
            nodes.MoveNext();
            if (nodes.CurrentPosition > 0)
                valor = nodes.Current.Value;
            return valor;
        }

        /// <summary>
        /// Formata um XmlDocument e identa o XML retornando a string formatada.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetFormatXml(XmlDocument doc)
        {
            string xmlFormated = "";
            //// Transforma de UTF-8 para UTF-16...
            XmlTextWriter xmlWriter;
            StringWriter textWriter;
            // Format the Xml document with indentation and save it to a string.
            textWriter = new StringWriter();
            xmlWriter = new XmlTextWriter(textWriter);
            xmlWriter.Formatting = Formatting.Indented;
            doc.Save(xmlWriter);
            xmlFormated = textWriter.ToString();
            xmlFormated = xmlFormated.Replace("utf-16", "utf-8").Replace("UTF-16", "UTF-8");
            return (textWriter.ToString());
        }

        public static string GetFormatXml(XmlNode doc)
        {
            string xmlFormated = "";
            //// Transforma de UTF-8 para UTF-16...
            XmlTextWriter xmlWriter;
            StringWriter textWriter;
            // Format the Xml document with indentation and save it to a string.
            textWriter = new StringWriter();
            xmlWriter = new XmlTextWriter(textWriter);
            xmlWriter.Formatting = Formatting.Indented;
            // XmlNode não tem PI
            xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            doc.WriteTo(xmlWriter);
            xmlFormated = textWriter.ToString();
            //xmlFormated = xmlFormated.Replace("utf-16", "utf-8").Replace("UTF-16", "UTF-8");
            return (xmlFormated);
        }

        /*
        C#: The Complete Reference 
        by Herbert Schildt 
        Publisher: Osborne/McGraw-Hill (March 8, 2002)
        ISBN: 0072134852
        */
        public static string GetFormatXml(string xml)
        {
            string xmlFormated = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                xmlFormated = GetFormatXml(doc);
            }
            catch { return (xml); } // Se gerar uma exceção, retorna o xml recebido.
            return xmlFormated;
        }

        public string Base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public static string TrocarUTF16_UTF8(string dados)
        {
            dados = dados.Replace("utf-16", "utf-8");
            dados = dados.Replace("UTF-16", "UTF-8");
            return dados;
        }

        public static string iso8859_unicode(string src)
        {
            Encoding iso = Encoding.GetEncoding("iso8859-1");
            Encoding unicode = Encoding.UTF8;
            byte[] isoBytes = iso.GetBytes(src);
            return unicode.GetString(isoBytes);
        }

        public static bool IsBase64String(string s)
        {
            if ((s.Length % 4) != 0) { return false; }
            try
            {
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(s));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Modulo11(string str)
        {
            int digito = 0;
            int peso = 9;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                digito += Convert.ToInt32(str[i].ToString()) * peso;
                if (peso == 2)
                    peso = 9;
                else
                    peso--;
            }
            digito = digito % 11;
            if (digito == 10)
                return "0";
            else
                return digito.ToString();
        }

        /// <summary>
        /// Converte um string em um array de bytes (byte[]).
        /// </summary>
        /// <param name="str">String a ser convertido.</param>
        /// <returns>Array de bytes.</returns>
        public static byte[] ConvertStringToByte(string str)
        {
            if (str == null)
                return null;
            byte[] b = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                b[i] = (byte)str[i];
            return b;
        }

        /// <summary>
        /// Converte um array de bytes (byte[]) em um string.
        /// </summary>
        /// <param name="b">Array a ser convertido.</param>
        /// <returns>String resultado.</returns>
        public static string ConvertByteToString(byte[] b)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length && b[i] != 0; i++)
                sb.Append((char)b[i]);
            return sb.ToString();
        }

    }
}
