using System.Collections;
using System.Xml;

namespace EmuladorDemoNFCe.Useful
{
    public class XmlUtils
    {
        public static string TrocarUTF8_ISO8859(string dados)
        {
            dados = dados.Replace("utf-8", "iso-8859-1");
            dados = dados.Replace("UTF-8", "ISO-8859-1");
            return dados;
        }
        public static XmlNode FindFirstXmlNode(XmlNode inicial, string nome)
        {
            foreach (XmlNode xn in inicial.ChildNodes)
            {
                if (xn.Name == nome)
                    return xn;
            }
            return null;
        }

        public static XmlNode FindXmlNode(XmlNode inicial, string subNodePath)
        {
            return FindXmlNode(inicial, subNodePath, "");
        }

        public static XmlNode FindXmlNode(XmlNode inicial, string subNodePath, string subNodeContent)
        {
            string caminho;
            return FindXmlNode(inicial, subNodePath, subNodeContent, out caminho);
        }

        public static XmlNode FindXmlNode(XmlNode inicial, string subNodePath, string subNodeContent, out string caminhoNumerico)
        {
            string[] path = subNodePath.Split("/\\-".ToCharArray());
            int posicao = 0;
            foreach (XmlNode xn in inicial.ChildNodes)
            {
                if (xn.Name == path[0])
                {
                    caminhoNumerico = posicao.ToString();
                    if (path.Length == 1)
                    {
                        if (subNodeContent == null || subNodeContent == "")
                            return xn;

                        if (subNodeContent == xn.InnerText)
                            return xn.ParentNode;

                        posicao++;
                        continue;
                    }
                    XmlNode xn2 = FindXmlNode(xn, subNodePath.Substring(path[0].Length + 1), subNodeContent, out caminhoNumerico);
                    caminhoNumerico = posicao.ToString() + "/" + caminhoNumerico;
                    if (xn2 != null)
                        return xn2;
                }
                posicao++;
            }
            caminhoNumerico = "";
            return null;
        }

        public static XmlNode FindXmlNodeByAttribute(XmlNode inicial, string subNodePath, string attributeValue)
        {
            string caminho;
            return FindXmlNodeByAttribute(inicial, subNodePath, attributeValue, out caminho);
        }

        public static XmlNode FindXmlNodeByAttribute(XmlNode inicial, string subNodePath, string attributeValue, out string caminhoNumerico)
        {
            string[] path = subNodePath.Split("/\\-".ToCharArray());
            int posicao = 0;
            if (path.Length > 1)
            {
                foreach (XmlNode xn in inicial.ChildNodes)
                {
                    if (xn.Name == path[0])
                    {
                        caminhoNumerico = posicao.ToString();
                        XmlNode xn2 = FindXmlNodeByAttribute(xn, subNodePath.Substring(path[0].Length + 1), attributeValue, out caminhoNumerico);
                        caminhoNumerico = posicao.ToString() + "/" + caminhoNumerico;
                        if (xn2 != null)
                            return xn2;
                    }
                    posicao++;
                }
            }
            else
                foreach (XmlAttribute xa in inicial.Attributes)
                {
                    if (xa.Name == path[0] && xa.Value == attributeValue)
                    {
                        caminhoNumerico = "";
                        return inicial;
                    }
                }
            caminhoNumerico = "";
            return null;
        }

        public static string GetInnerXml(XmlNode inicial, string subNodePath)
        {
            string[] path = subNodePath.Split("/\\-".ToCharArray());
            foreach (XmlNode xn in inicial.ChildNodes)
            {
                if (xn.Name == path[0])
                {
                    if (path.Length == 1)
                    {
                        return xn.InnerXml;
                    }
                    string inner = GetInnerXml(xn, subNodePath.Substring(path[0].Length + 1));
                    if (inner != null)
                        return inner;
                }
            }
            return null;
        }

        public static XmlNode CriarElementoComSubElementos(XmlDocument document, string nome, string[] subElementos)
        {
            XmlNode xn = document.CreateElement(nome);
            foreach (string subElemento in subElementos)
            {
                if (subElemento != "")
                {
                    string[] itens = subElemento.Split("=".ToCharArray());
                    XmlNode sub = document.CreateElement(itens[0]);
                    sub.InnerXml = itens[1];
                    xn.AppendChild(sub);
                }
            }
            return xn;
        }

        public static XmlNode SubstituirSubElementos(XmlDocument doc, XmlNode xmlNode, string[] subElementos)
        {
            //XmlNode xn = document.CreateElement(nome);
            foreach (string subElemento in subElementos)
            {
                if (subElemento != "")
                {
                    string[] itens = subElemento.Split("=".ToCharArray());
                    XmlNode xmlSub = FindXmlNode(xmlNode, itens[0]);
                    if (xmlSub != null)
                        xmlSub.InnerXml = itens[1];
                    else
                    {
                        XmlNode newNode = doc.CreateElement(itens[0]);
                        newNode.InnerXml = itens[1];
                        xmlNode.AppendChild(newNode);
                    }
                }
            }
            return xmlNode;
        }

        public static void ApagarElemento(XmlDocument doc, string caminho, string conteudo)
        {
            XmlNode node = XmlUtils.FindXmlNode(doc, caminho, conteudo);
            if (node != null)
                node.ParentNode.RemoveChild(node);
        }

        public static ArrayList GetAllXmlNodes(XmlNode inicial, string nodeName)
        {
            ArrayList ar = new ArrayList();
            foreach (XmlNode xn in inicial.ChildNodes)
            {
                if (xn.Name == nodeName)
                    ar.Add(xn);
                ArrayList childs = GetAllXmlNodes(xn, nodeName);
                ar.AddRange(childs);
            }
            return ar;
        }

        public static string GetAtributeValue(XmlNode inicial, string nodeName, string attributeName)
        {
            foreach (XmlNode xn in inicial.ChildNodes)
            {
                if (xn.Name == nodeName)
                {
                    for (int i = 0; i < xn.Attributes.Count; i++)
                    {
                        if (xn.Attributes.Item(i).Name == attributeName)
                            return xn.Attributes.Item(i).InnerText;
                    }

                }
                string res = GetAtributeValue(xn, nodeName, attributeName);
                if (res != null)
                    return res;
            }
            return null;
        }

        /// <summary>
        /// Localiza o Atributo a partir de uma string XML
        /// </summary>
        /// <param name="inicial"></param>
        /// <param name="nodeName"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetAtributeValue(string xml, string nodeName, string attributeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.LoadXml(xml);

            XmlNode root = doc.DocumentElement;
            string value = XmlUtils.GetAtributeValue(root.FirstChild, nodeName, attributeName);
            return value;
        }
    }
}