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
    public partial class CLIENTEIMEXAPPProvider
    {
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

        RAMOATIVIDADEIMEXAPPProvider RAMOATIVIDADEIMEXAPPP = new RAMOATIVIDADEIMEXAPPProvider();
        CONDICAOPAGAMENTOIMEXAPPProvider CONDICAOPAGAMENTOIMEXAPPP = new CONDICAOPAGAMENTOIMEXAPPProvider();
        PRECOIMEXAPPProvider PRECOIMEXAPPP = new PRECOIMEXAPPProvider();
        TRANSPORTADORAIMEXAPPProvider TRANSPORTADORAIMEXAPPP = new TRANSPORTADORAIMEXAPPProvider();

        IList<CLIENTEIMEXAPPEntity> CLIENTEIMEXAPPColl;

        Utility Util = new Utility();

        public async void Save(CLIENTEIMEXAPPEntity Entity)
        {
            try
            {
                //Busca dados da Configuração
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                string token = CONFISISTEMATy.TOKENIMEXAPP.Trim();
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostClientes;

                Entity.CALTERNATIVO = String.Empty;	//STRING

                int _IDRAMOATIVIDADE = RAMOATIVIDADEIMEXAPPP.GetID();
                if (_IDRAMOATIVIDADE < 1)
                    MessageBox.Show("Ramo de Atividade Não Localizado!");
                Entity.IDRAMOATIVIDADE = _IDRAMOATIVIDADE;	//INTEGER // CONSUMIDOR FINAL "531021"

                Entity.STPROSPECCAO = String.Empty;	//STRING
                Entity.QVISITACLIENTE = null;//INTEGER
                Entity.STPERIODOVISITACLIENTE = null;//BYTE

                int _IDCONDICAOPAGAMENTO = CONDICAOPAGAMENTOIMEXAPPP.GetID();
                if (_IDCONDICAOPAGAMENTO < 1)
                    MessageBox.Show("Condição de Pagamento Não Localizado!");
                Entity.IDCONDICAOPAGAMENTO = _IDCONDICAOPAGAMENTO;//INTEGER //A VISTA "42586"

                int _IDTABELAPRECO = PRECOIMEXAPPP.GetID();
                if (_IDTABELAPRECO < 1)
                    MessageBox.Show("Tabela de Preço Não Localizado!");
                Entity.IDTABELAPRECO = _IDTABELAPRECO; //INTEGER //TABELA PADRÃO "10475"

                Entity.IDEMPRESA = Convert.ToInt32(CONFISISTEMATy.IDEMPRESAIMEXAPP);

                int _IDTRANSPORTADORA = TRANSPORTADORAIMEXAPPP.GetID();
                if (_IDTRANSPORTADORA < 1)
                    MessageBox.Show("Transportadora Não Localizado!");
                Entity.IDTRANSPORTADORA = _IDTRANSPORTADORA;//INTEGER //A VISTA "42586"

                Entity.DTULTIMAALTERACAO = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Entity.IDEMPRESA_ASPNETUSERS = null; //INTEGER
                Entity.IDASPNETUSERS = CONFISISTEMATy.IDASPNETUSERSINCLUSAO.Trim();	//STRING
                Entity.IDIMPORTACAO = null;	//INTEGER
                Entity.VLIMITECREDITO = null;	//DECIMAL NUMBER
                Entity.STATUALIZADO = null;	//BYTE
                Entity.XWEBSITE = String.Empty;	//STRING
                Entity.BEXIBIRANOTACAONOPEDIDO = null;	//BOOLEAN
                Entity.DTNASCIMENTO = null;	//DATE
                Entity.CNAE = String.Empty;	//STRING
                Entity.IDASPNETUSERSINCLUSAO = CONFISISTEMATy.IDASPNETUSERSINCLUSAO.Trim();
                Entity.IDINTEGRACAO = null;	//INTEGER
                Entity.STCONTRIBUINTE = null;	//BYTE
                Entity.XIDMAPS = String.Empty;	//STRING
                Entity.IDCNAE = null;	//INTEGER
                Entity.STCLIENTEAPLICACAO = null;//BYTE

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
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosClientes + token + "/";

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
                string URI = BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosClientes;
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

                    CLIENTEIMEXAPPColl = DeserializeToList<CLIENTEIMEXAPPEntity>(jsonString);
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


        public int BuscaID(int IDRegistro)
        {
            int result = -1;

            try
            {
                foreach (var item in CLIENTEIMEXAPPColl)
                {
                    if (item.XMEUID == IDRegistro.ToString())
                    {
                        result = Convert.ToInt32(item.IDCLIENTES);
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
