using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NFeApiClientCSharp{
    class NFeAPI{
        private String urlEnvioNFe;
	    private String urlStatusProcessamento;
	    private String urlDownloadNFe;

        public NFeAPI(){
            //Atribui as urls das operações
			this.urlEnvioNFe = "https://nfe.ns.eti.br/nfe/issue";
			this.urlStatusProcessamento = "https://nfe.ns.eti.br/nfe/issue/status";
			this.urlDownloadNFe = "https://nfe.ns.eti.br/nfe/get";
			
        }

        private String enviaConteudoParaAPI(String token, String conteudo, String url, String tpConteudo = "json"){
            String retorno = "";
            //Cria objeto para requisição e atribui as configurações necessárias para API
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["X-AUTH-TOKEN"] = token;
            if (tpConteudo == "txt") httpWebRequest.ContentType = "text/plain";
            else if (tpConteudo == "xml") httpWebRequest.ContentType = "application/xml";
            else httpWebRequest.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())){
                streamWriter.Write(conteudo);
                streamWriter.Flush();
                streamWriter.Close();
            }
            
            //Envia requisição
            try{
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) retorno = streamReader.ReadToEnd();
            }
            //Se der erro
            catch (WebException ex){
                if (ex.Status == WebExceptionStatus.ProtocolError){
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    using (var streamReader = new StreamReader(response.GetResponseStream())) retorno = streamReader.ReadToEnd();
                }
                
            }
            
            return retorno;
        }

        public String emitirNFe(String token, String conteudo, String tpConteudo){
            return enviaConteudoParaAPI(token, conteudo, this.urlEnvioNFe, tpConteudo);
        }

        public String consultaStatusProcessamento(String token, String CNPJ, String nsNRec){
            //Monta JSON e envia para API
            String JSON = "{" + 
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"CNPJ\": \"" + CNPJ + "\", " +
                    "\"nsNRec\": \"" + nsNRec + "\"" +
                "}";
            return enviaConteudoParaAPI(token, JSON, this.urlStatusProcessamento, "json");
        }

        public String downloadNFe(String token, String chNFe, String tpDown)
        {
            //Monta JSON e envia para API
            String JSON = "{" +
                    "\"X-AUTH-TOKEN\": \"" + token + "\", " +
                    "\"chNFe\": \"" + chNFe + "\", " +
                    "\"tpDown\": \"" + tpDown + "\"" +
                "}";

            return enviaConteudoParaAPI(token, JSON, this.urlDownloadNFe, "json");
        }

        public String downloadNFeAndSave(String token, String chNFe, String tpDown, String caminho = "", Boolean isShow = false){
            String retorno = downloadNFe(token, chNFe, tpDown);

            if (caminho != ""){
                if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                if (!caminho.EndsWith("\\")) caminho += "\\";
            }

            //Seta caminho para salvar, verifica se status 200 da API e faz o download do que foi solicitado
            String pathName = caminho + chNFe + "-procNfe";
            dynamic JSONRetorno = JsonConvert.DeserializeObject(retorno);
            String status = JSONRetorno.status;

            if (status == "200"){
                if (tpDown.ToUpper().Contains("X")){
                    String conteudoSalvar = JSONRetorno.xml;
                    salvaArquivo(conteudoSalvar, pathName, "X");
                }
                else{
                    if (tpDown.ToUpper().Contains("J")){
                        dynamic conteudoSalvar = JSONRetorno.nfeProc;
                        salvaArquivo(Convert.ToString(conteudoSalvar), pathName, "J");
                    }
                }

                if (tpDown.ToUpper().Contains("P")){
                    String conteudoSalvar = JSONRetorno.pdf;
                    salvaArquivo(conteudoSalvar, pathName, "P");
                    if (isShow) System.Diagnostics.Process.Start(@pathName + ".pdf");
                }
            }

            return retorno;
        }

        private void salvaArquivo(String conteudo, String pathName, String tpArq){
            //Se XML
            if (tpArq == "X") System.IO.File.WriteAllText(@pathName + ".xml", conteudo);
            //Se JSON
            else if (tpArq == "J") System.IO.File.WriteAllText(@pathName + ".json", conteudo);
            //Se PDF
            else {
                byte[] bytes = Convert.FromBase64String(conteudo);
                if (File.Exists(pathName + ".pdf")) File.Delete(pathName + ".pdf");
                System.IO.FileStream stream = new FileStream(@pathName + ".pdf", FileMode.CreateNew);
                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
        }

    }
}
