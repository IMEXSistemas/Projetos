using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Saxon.Api;

namespace EmuladorNFCe.Useful
{
    public class Nfce
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Nfce));

        public string Run(string path, bool isContingencia)
        {
            try
            {
                string assembly = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Log.Info(string.Format("Caminho para gerar o arquivo 'NFCePrt.html' para o WebBVrowser: {0}", assembly));
                if (assembly != null)
                {
                   // string nfcePath = assembly.ToLower().Replace("\\bin\\debug", "\\nfce");
                    string nfcePath = BmsSoftware.ConfigSistema1.Default.PathInstall;

                    /* algum erro no parser para executar a identificação de nfce normal ou contingencia... criado outro arquivo para nao perder tempo neste exemplo */
                    string xsltInput = String.Concat(nfcePath, isContingencia ? "\\xsl\\NFCeContingencia.xsl" : "\\xsl\\NFCe.xsl");
                    Log.Debug(String.Format("xsltInput: [{0}]", xsltInput));

                    /* criado um html de exemplo...*/
                    string xsltOutput = String.Concat(nfcePath, "\\NFCePrt.html");
                    Log.Debug(String.Format("xsltOutput: [{0}]", xsltOutput));

                    var xslt = new FileInfo(xsltInput);
                    var input = new FileInfo(path);
                    var output = new FileInfo(xsltOutput);

                    Log.Debug(String.Format("input File XML: [{0}]", input));
                    // Compile stylesheet
                    var processor = new Processor();
                    var compiler = processor.NewXsltCompiler();
                    var executable = compiler.Compile(new Uri(xslt.FullName));

                    // Do transformation to a destination
                    var destination = new DomDestination();
                    using (var inputStream = input.OpenRead())
                    {
                        var transformer = executable.Load();
                        if (input.DirectoryName != null)
                            transformer.SetInputStream(inputStream, new Uri(input.DirectoryName));
                        transformer.Run(destination);
                    }

                    destination.XmlDocument.Save(output.FullName);
                    return (destination.XmlDocument.OuterXml);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Erro na geração do xslt. Erro:{0} Message: {1}", e.Message, e.InnerException),@"Gerar XSLT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Debug("ERROR MESSAGE: " + e.Message);
                Log.Debug("ERROR INNERMESSAGE: " + e.InnerException);
                return null;
            }
        }
    }
}