using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BmsSoftware.UI
{
    public partial class BoletoCloud
    {
        private string BoletoCloudTokenAPI = "api-key_RTZ8Cz55fNX8RCcDThOuRMNNM5CRKjvO3nmALq7sVAI=";
        private string BoletoCloudTokenConta = "api-key_Ue61b94-O2uZ9yYy8FONw3HN_u8PxLJzIuAGUdZAaGU=";

        public void GerarBoleto()
        {
            var client = new RestClient();

            var url = @"https://sandbox.boletocloud.com/api/v1/boletos";

            var request = new RestRequest(url, Method.POST);

            request.AddHeader("Authorization", "Basic " + StrToBase64(BoletoCloudTokenAPI + ":token"));
            request.AddHeader("Content-Type", "application/pdf;charset=UTF-8");
            request.AddHeader("Location", "/api/v1/boletos/" + BoletoCloudTokenAPI);
            request.AddHeader("X-BoletoCloud-Token", BoletoCloudTokenAPI);
            request.AddHeader("X-BoletoCloud-Version", "0.4.5");

            request.AddParameter("boleto.conta.token", BoletoCloudTokenConta);
            
            request.AddParameter("boleto.conta.banco" , "104");
            request.AddParameter("boleto.conta.agencia" , "1879");
            request.AddParameter("boleto.conta.convenio", "685669");
            request.AddParameter("boleto.conta.carteira", "14");
            request.AddParameter("boleto.conta.registro", "true");

            request.AddParameter("boleto.beneficiario.nome = ", "MARMORARIA DESTAQUE");
            request.AddParameter("boleto.beneficiario.cprf = ", "01.087.764/0001-29");
            request.AddParameter("boleto.beneficiario.endereco.cep", "88371-199");
            request.AddParameter("boleto.beneficiario.endereco.uf", "SC");
            request.AddParameter("boleto.beneficiario.endereco.localidade", "Navegantes");
            request.AddParameter("boleto.beneficiario.endereco.bairro", "Nossa Senhora das Graças");
            request.AddParameter("boleto.beneficiario.endereco.logradouro", "Rua Waldemiro Adolfo da Luz");
            request.AddParameter("boleto.beneficiario.endereco.numero", "187");
            request.AddParameter("boleto.beneficiario.endereco.complemento", "");
            request.AddParameter("boleto.emissao", "2017-08-11");
            request.AddParameter("boleto.vencimento", "2020-05-30");
            request.AddParameter("boleto.documento", "EX1");
            request.AddParameter("boleto.sequencial", "1");
            request.AddParameter("boleto.titulo",  "DM");
            request.AddParameter("boleto.valor",  "1250.43");
            request.AddParameter("boleto.pagador.nome",   "Alberto Santos Dumont");
            request.AddParameter("boleto.pagador.cprf",   "161.397.608-95");
            request.AddParameter("boleto.pagador.endereco.cep",  "36240-000");
            request.AddParameter("boleto.pagador.endereco.uf",  "MG");
            request.AddParameter("boleto.pagador.endereco.localidade",  "Santos Dumont");
            request.AddParameter("boleto.pagador.endereco.bairro",  "Casa Natal");
            request.AddParameter("boleto.pagador.endereco.logradouro",   "BR-499");
            request.AddParameter("boleto.pagador.endereco.numero",  "s /n");
            request.AddParameter("boleto.pagador.endereco.complemento",  "Sítio - Subindo a serra da Mantiqueira");
            request.AddParameter("boleto.instrucao", "Atenção! NÃO RECEBER ESTE BOLETO.");
            request.AddParameter("boleto.instrucao", "Este é apenas um teste utilizando a API Boleto Cloud");
            request.AddParameter("boleto.instrucao",  "Mais info em http://www.boletocloud.com/app/dev/api");

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var pasta = "c:\\BoletoCloudPastaBoletos";

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                var resultado = response.Headers.ToList();

                string numeroBoleto = resultado.Find(x => x.Name == "X-BoletoCloud-NIB-Nosso-Numero").Value.ToString();

                string linkDownload = resultado.Find(x => x.Name == "Location").Value.ToString();

                string arquivo = pasta + $"\\Boleto-{numeroBoleto}.pdf";

                if (!File.Exists(arquivo))
                    File.WriteAllBytes(arquivo, response.RawBytes);

                System.Diagnostics.Process.Start(arquivo);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var erro = ConverteErro(response.Content);
                //retorno com os erros
                throw new Exception(erro._causas);
            }
        }

        private Erro ConverteErro(string resposta)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Erro));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resposta));
            Erro obj = (Erro)serializer.ReadObject(ms);
            return obj;
        }

        public static string StrToBase64(string str)
        {
            return str == null ? "" : System.Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(str));
        }

   

    public class Parcela
    {
        public string numeroParcela { get; set; }
        public DateTime dataVencimento { get; set; }
        public double valorDevido { get; set; }
        public Cliente cliente { get; set; }
    }

    public class Cliente
    {
        public string nome_razao { get; set; }
        public string cpf_cnpj { get; set; }
        public string endereco_cep { get; set; }
        public string sigla_uf { get; set; }
        public string endereco_cidade { get; set; }
        public string endereco_bairro { get; set; }
        public string endereco_logradouro { get; set; }
        public string endereco_numero { get; set; }
        public string endereco_complemento { get; set; }
    }

    public class RetornoBoletoCloud
    {
        public Erro erro { get; set; }
    }

    public class Erro
    {
        public int status { get; set; }
        public string tipo { get; set; }
        public Causa[] causas { get; set; }

        public string _causas
        {
            get
            {
                string c = "";

                foreach (var item in causas)
                {
                    c += item.mensagem + "\r\n";
                }

                return c;
            }
        }
    }

    public class Causa
    {
        public string codigo { get; set; }
        public string mensagem { get; set; }
        public string suporte { get; set; }
    }

  }
}
