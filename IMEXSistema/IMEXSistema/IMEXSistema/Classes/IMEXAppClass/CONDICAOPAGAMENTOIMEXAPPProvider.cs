using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data;


namespace BMSworks.IMEXAppClass
{
    public partial class CONDICAOPAGAMENTOIMEXAPPProvider
    {
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        IList<CONDICAOPAGAMENTOIMEXAPPEntity> CONDICAOPAGAMENTOIMEXAPPColl;

        Utility Util = new Utility();

        public async void Save(CONDICAOPAGAMENTOIMEXAPPEntity Entity)
        {
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostPrazo;

                Entity.IDCONDICAOPAGAMENTO = null;
                Entity.IDEMPRESA = Convert.ToInt32(CONFISISTEMATy.IDEMPRESAIMEXAPP);
                Entity.IDASPNETUSERSINCLUSAO =  CONFISISTEMATy.IDASPNETUSERSINCLUSAO.Trim();
                Entity.DTULTIMAALTERACAO = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Entity.STATIVO = true;

                using (var client = new System.Net.Http.HttpClient())
                {
                    var serializedObjeto = JsonConvert.SerializeObject(Entity);

                    string RegistroStr = "\"Registro\"";
                    string xToken = "\"xToken\"";
                    token = "\"" + token + "\"";
                    serializedObjeto = "{ " + RegistroStr + ": " + serializedObjeto + ", " + xToken + ": " + token + " }";
                    var content = new StringContent(serializedObjeto, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(URI, content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        } 

        public async void Delete(int CodRegistro)
        {
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosPrazo + token + "/";

                //exclui o registro
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    HttpResponseMessage responseMessage = await client.DeleteAsync(String.Format("{0}/{1}", URI, CodRegistro));

                    if (!responseMessage.IsSuccessStatusCode)
                        MessageBox.Show("Falha ao Exxcluir Registro: " + responseMessage.StatusCode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        public int GetID(int CodRegistro)
        {
            int Result = -1;
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosPrazo;
                URI = URI + token + "/" + "2016-06-19T00:00:00";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(URI).Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;

                    int tamanhostring = stringData.Length;
                    int posinicio = stringData.IndexOf("[");
                    string ProdutoJsonString2 = stringData.ToString().Substring(posinicio, tamanhostring - posinicio);
                    int posifim = ProdutoJsonString2.IndexOf("Message");
                    ProdutoJsonString2 = ProdutoJsonString2.ToString().Substring(0, posifim - 2);
                    string jsonString = ProdutoJsonString2;

                    CONDICAOPAGAMENTOIMEXAPPColl = DeserializeToList<CONDICAOPAGAMENTOIMEXAPPEntity>(jsonString);
                }

                //Localiza o ID
                Result = BuscaID(CodRegistro);
                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return Result;
            }
        }

        public int GetID()
        {
            int Result = -1;
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosPrazo;
                URI = URI + token + "/" + "2016-06-19T00:00:00";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(URI).Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;

                    int tamanhostring = stringData.Length;
                    int posinicio = stringData.IndexOf("[");
                    string ProdutoJsonString2 = stringData.ToString().Substring(posinicio, tamanhostring - posinicio);
                    int posifim = ProdutoJsonString2.IndexOf("Message");
                    ProdutoJsonString2 = ProdutoJsonString2.ToString().Substring(0, posifim - 2);
                    string jsonString = ProdutoJsonString2;

                    CONDICAOPAGAMENTOIMEXAPPColl = DeserializeToList<CONDICAOPAGAMENTOIMEXAPPEntity>(jsonString);
                }

                //Localiza o ID
                if (CONDICAOPAGAMENTOIMEXAPPColl.Count > 0)
                    Result = Convert.ToInt32(CONDICAOPAGAMENTOIMEXAPPColl[0].IDCONDICAOPAGAMENTO);

                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return Result;
            }
        }

        public IList<CONDICAOPAGAMENTOIMEXAPPEntity> GetID2()
        {
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);
                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosPrazo;
                URI = URI + token + "/" + "2016-06-19T00:00:00";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URI);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(URI).Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;

                    int tamanhostring = stringData.Length;
                    int posinicio = stringData.IndexOf("[");
                    string ProdutoJsonString2 = stringData.ToString().Substring(posinicio, tamanhostring - posinicio);
                    int posifim = ProdutoJsonString2.IndexOf("Message");
                    ProdutoJsonString2 = ProdutoJsonString2.ToString().Substring(0, posifim - 2);
                    string jsonString = ProdutoJsonString2;

                    CONDICAOPAGAMENTOIMEXAPPColl = DeserializeToList<CONDICAOPAGAMENTOIMEXAPPEntity>(jsonString);
                }

                return CONDICAOPAGAMENTOIMEXAPPColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return CONDICAOPAGAMENTOIMEXAPPColl;
            }
        }


        public int BuscaID(int IDRegistro)
        {
            int result = -1;

            try
            {
                foreach (var item in CONDICAOPAGAMENTOIMEXAPPColl)
                {
                    if (item.XMEUID == IDRegistro.ToString())
                    {
                        result = Convert.ToInt32(item.IDCONDICAOPAGAMENTO);
                        break;
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return result;
            }
        }

        public static List<string> InvalidJsonElements;
        public static IList<T> DeserializeToList<T>(string jsonString)
        {
            InvalidJsonElements = null;
            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                try
                {
                    // CorrectElements
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                    MessageBox.Show("Erro Técnico: " + ex.Message);
                }
            }

            return objectsList;
        }


    }
}
