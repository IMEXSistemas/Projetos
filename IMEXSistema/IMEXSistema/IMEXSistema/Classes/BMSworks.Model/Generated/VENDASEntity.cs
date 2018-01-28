using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VENDASEntity
	{
		private string _NOTA;
		private string _MODELO;
		private string _SERIE;
		private DateTime? _DATA_EMISSAO;
		private string _LOJA;
		private string _CAIXA;
		private string _ENTRADA;
		private string _SAIDA;
		private string _CONSUMO;
		private string _SERVICO;
		private string _NATUREZA;
		private string _COD_CFOP;
		private string _CFOP;
		private string _CFPS;
		private string _CLIENTE;
		private string _PEDIDO;
		private string _ORCAMENTO;
		private string _OS;
		private DateTime? _DATA_SAIDA;
		private TimeSpan? _HORA_SAIDA;
		private string _IE_SUBSTITUTO;
		private string _OBSERVACOES;
		private string _OBS_LIV_FIS;
		private decimal? _BASE_CAL_ICMS;
		private decimal? _VALOR_ICMS;
		private decimal? _BASE_CAL_ICMS_SUB;
		private decimal? _VALOR_ICMS_SUB;
		private decimal? _VALOR_TOT_PRO;
		private decimal? _VALOR_FRETE;
		private decimal? _VALOR_SEGURO;
		private decimal? _OUTRAS_DESPESAS;
		private decimal? _VALOR_IPI;
		private decimal? _VALOR_TOT_SER;
		private decimal? _VALOR_ISS;
		private decimal? _PERCE_ISS;
		private decimal? _VALOR_IRRF;
		private decimal? _VALOR_INSS;
		private decimal? _VALOR_PIS;
		private decimal? _VALOR_PIS_SUB;
		private decimal? _VALOR_COFINS;
		private decimal? _VALOR_COFINS_SUB;
		private decimal? _VALOR_CS;
		private decimal? _VALOR_LIQ_SER;
		private decimal? _VALOR_TOT_NOTA;
		private decimal? _CRED_ICM_SIMPLES;
		private string _TRANSPORTADORA_1;
		private string _FRETE_CONTA_1;
		private string _QUANTIDADE;
		private string _ESPECIE;
		private string _MARCA;
		private string _NUMERO;
		private string _PESO_BRUTO;
		private string _PESO_LIQUIDO;
		private string _VENDEDOR;
		private decimal? _CUSTO_FINANCEIRO;
		private string _NFE_STATUS;
		private string _NFE_CHAVE;
		private string _NFE_RECENT;
		private string _NFE_PROENT;
		private DateTime? _NFE_DATHORENT;
		private string _NFE_PROCAN;
		private DateTime? _NFE_DATHORCAN;
		private string _NFE_AMBIENTE;
		private int? _CANCELADA;

		#region Construtores

		//Construtor default
		public VENDASEntity() {
            this._DATA_EMISSAO = null;
			this._DATA_SAIDA = null;
			this._HORA_SAIDA = null;
			this._BASE_CAL_ICMS = null;
			this._VALOR_ICMS = null;
			this._BASE_CAL_ICMS_SUB = null;
			this._VALOR_ICMS_SUB = null;
			this._VALOR_TOT_PRO = null;
			this._VALOR_FRETE = null;
			this._VALOR_SEGURO = null;
			this._OUTRAS_DESPESAS = null;
			this._VALOR_IPI = null;
			this._VALOR_TOT_SER = null;
			this._VALOR_ISS = null;
			this._PERCE_ISS = null;
			this._VALOR_IRRF = null;
			this._VALOR_INSS = null;
			this._VALOR_PIS = null;
			this._VALOR_PIS_SUB = null;
			this._VALOR_COFINS = null;
			this._VALOR_COFINS_SUB = null;
			this._VALOR_CS = null;
			this._VALOR_LIQ_SER = null;
			this._VALOR_TOT_NOTA = null;
			this._CRED_ICM_SIMPLES = null;
			this._CUSTO_FINANCEIRO = null;
			this._NFE_DATHORENT = null;
			this._NFE_DATHORCAN = null;
            this._CANCELADA = null;
			}

        public VENDASEntity(string NOTA, string MODELO, string SERIE, DateTime? DATA_EMISSAO, string LOJA, string CAIXA, string ENTRADA, string SAIDA, string CONSUMO, string SERVICO, string NATUREZA, string COD_CFOP, string CFOP, string CFPS, string CLIENTE, string PEDIDO, string ORCAMENTO, string OS, DateTime? DATA_SAIDA, TimeSpan? HORA_SAIDA, string IE_SUBSTITUTO, string OBSERVACOES, string OBS_LIV_FIS, decimal? BASE_CAL_ICMS, decimal? VALOR_ICMS, decimal? BASE_CAL_ICMS_SUB, decimal? VALOR_ICMS_SUB, decimal? VALOR_TOT_PRO, decimal? VALOR_FRETE, decimal? VALOR_SEGURO, decimal? OUTRAS_DESPESAS, decimal? VALOR_IPI, decimal? VALOR_TOT_SER, decimal? VALOR_ISS, decimal? PERCE_ISS, decimal? VALOR_IRRF, decimal? VALOR_INSS, decimal? VALOR_PIS, decimal? VALOR_PIS_SUB, decimal? VALOR_COFINS, decimal? VALOR_COFINS_SUB, decimal? VALOR_CS, decimal? VALOR_LIQ_SER, decimal? VALOR_TOT_NOTA, decimal? CRED_ICM_SIMPLES, string TRANSPORTADORA_1, string FRETE_CONTA_1, string QUANTIDADE, string ESPECIE, string MARCA, string NUMERO, string PESO_BRUTO, string PESO_LIQUIDO, string VENDEDOR, decimal? CUSTO_FINANCEIRO, string NFE_STATUS, string NFE_CHAVE, string NFE_RECENT, string NFE_PROENT, DateTime? NFE_DATHORENT, string NFE_PROCAN, DateTime? NFE_DATHORCAN, string NFE_AMBIENTE, int? CANCELADA)
        {

			this._NOTA = NOTA;
			this._MODELO = MODELO;
			this._SERIE = SERIE;
			this._DATA_EMISSAO = DATA_EMISSAO;
			this._LOJA = LOJA;
			this._CAIXA = CAIXA;
			this._ENTRADA = ENTRADA;
			this._SAIDA = SAIDA;
			this._CONSUMO = CONSUMO;
			this._SERVICO = SERVICO;
			this._NATUREZA = NATUREZA;
			this._COD_CFOP = COD_CFOP;
			this._CFOP = CFOP;
			this._CFPS = CFPS;
			this._CLIENTE = CLIENTE;
			this._PEDIDO = PEDIDO;
			this._ORCAMENTO = ORCAMENTO;
			this._OS = OS;
			this._DATA_SAIDA = DATA_SAIDA;
			this._HORA_SAIDA = HORA_SAIDA;
			this._IE_SUBSTITUTO = IE_SUBSTITUTO;
			this._OBSERVACOES = OBSERVACOES;
			this._OBS_LIV_FIS = OBS_LIV_FIS;
			this._BASE_CAL_ICMS = BASE_CAL_ICMS;
			this._VALOR_ICMS = VALOR_ICMS;
			this._BASE_CAL_ICMS_SUB = BASE_CAL_ICMS_SUB;
			this._VALOR_ICMS_SUB = VALOR_ICMS_SUB;
			this._VALOR_TOT_PRO = VALOR_TOT_PRO;
			this._VALOR_FRETE = VALOR_FRETE;
			this._VALOR_SEGURO = VALOR_SEGURO;
			this._OUTRAS_DESPESAS = OUTRAS_DESPESAS;
			this._VALOR_IPI = VALOR_IPI;
			this._VALOR_TOT_SER = VALOR_TOT_SER;
			this._VALOR_ISS = VALOR_ISS;
			this._PERCE_ISS = PERCE_ISS;
			this._VALOR_IRRF = VALOR_IRRF;
			this._VALOR_INSS = VALOR_INSS;
			this._VALOR_PIS = VALOR_PIS;
			this._VALOR_PIS_SUB = VALOR_PIS_SUB;
			this._VALOR_COFINS = VALOR_COFINS;
			this._VALOR_COFINS_SUB = VALOR_COFINS_SUB;
			this._VALOR_CS = VALOR_CS;
			this._VALOR_LIQ_SER = VALOR_LIQ_SER;
			this._VALOR_TOT_NOTA = VALOR_TOT_NOTA;
			this._CRED_ICM_SIMPLES = CRED_ICM_SIMPLES;
			this._TRANSPORTADORA_1 = TRANSPORTADORA_1;
			this._FRETE_CONTA_1 = FRETE_CONTA_1;
			this._QUANTIDADE = QUANTIDADE;
			this._ESPECIE = ESPECIE;
			this._MARCA = MARCA;
			this._NUMERO = NUMERO;
			this._PESO_BRUTO = PESO_BRUTO;
			this._PESO_LIQUIDO = PESO_LIQUIDO;
			this._VENDEDOR = VENDEDOR;
			this._CUSTO_FINANCEIRO = CUSTO_FINANCEIRO;
			this._NFE_STATUS = NFE_STATUS;
			this._NFE_CHAVE = NFE_CHAVE;
			this._NFE_RECENT = NFE_RECENT;
			this._NFE_PROENT = NFE_PROENT;
			this._NFE_DATHORENT = NFE_DATHORENT;
			this._NFE_PROCAN = NFE_PROCAN;
			this._NFE_DATHORCAN = NFE_DATHORCAN;
			this._NFE_AMBIENTE = NFE_AMBIENTE;
			this._CANCELADA = CANCELADA;
		}
		#endregion

		#region Propriedades Get/Set

		public string NOTA
		{
			get { return _NOTA; }
			set { _NOTA = value; }
		}

		public string MODELO
		{
			get { return _MODELO; }
			set { _MODELO = value; }
		}

		public string SERIE
		{
			get { return _SERIE; }
			set { _SERIE = value; }
		}

		public DateTime? DATA_EMISSAO
		{
			get { return _DATA_EMISSAO; }
			set { _DATA_EMISSAO = value; }
		}

		public string LOJA
		{
			get { return _LOJA; }
			set { _LOJA = value; }
		}

		public string CAIXA
		{
			get { return _CAIXA; }
			set { _CAIXA = value; }
		}

		public string ENTRADA
		{
			get { return _ENTRADA; }
			set { _ENTRADA = value; }
		}

		public string SAIDA
		{
			get { return _SAIDA; }
			set { _SAIDA = value; }
		}

		public string CONSUMO
		{
			get { return _CONSUMO; }
			set { _CONSUMO = value; }
		}

		public string SERVICO
		{
			get { return _SERVICO; }
			set { _SERVICO = value; }
		}

		public string NATUREZA
		{
			get { return _NATUREZA; }
			set { _NATUREZA = value; }
		}

		public string COD_CFOP
		{
			get { return _COD_CFOP; }
			set { _COD_CFOP = value; }
		}

		public string CFOP
		{
			get { return _CFOP; }
			set { _CFOP = value; }
		}

		public string CFPS
		{
			get { return _CFPS; }
			set { _CFPS = value; }
		}

		public string CLIENTE
		{
			get { return _CLIENTE; }
			set { _CLIENTE = value; }
		}

		public string PEDIDO
		{
			get { return _PEDIDO; }
			set { _PEDIDO = value; }
		}

		public string ORCAMENTO
		{
			get { return _ORCAMENTO; }
			set { _ORCAMENTO = value; }
		}

		public string OS
		{
			get { return _OS; }
			set { _OS = value; }
		}

		public DateTime? DATA_SAIDA
		{
			get { return _DATA_SAIDA; }
			set { _DATA_SAIDA = value; }
		}

		public TimeSpan? HORA_SAIDA
		{
			get { return _HORA_SAIDA; }
			set { _HORA_SAIDA = value; }
		}

		public string IE_SUBSTITUTO
		{
			get { return _IE_SUBSTITUTO; }
			set { _IE_SUBSTITUTO = value; }
		}

		public string OBSERVACOES
		{
			get { return _OBSERVACOES; }
			set { _OBSERVACOES = value; }
		}

		public string OBS_LIV_FIS
		{
			get { return _OBS_LIV_FIS; }
			set { _OBS_LIV_FIS = value; }
		}

		public decimal? BASE_CAL_ICMS
		{
			get { return _BASE_CAL_ICMS; }
			set { _BASE_CAL_ICMS = value; }
		}

		public decimal? VALOR_ICMS
		{
			get { return _VALOR_ICMS; }
			set { _VALOR_ICMS = value; }
		}

		public decimal? BASE_CAL_ICMS_SUB
		{
			get { return _BASE_CAL_ICMS_SUB; }
			set { _BASE_CAL_ICMS_SUB = value; }
		}

		public decimal? VALOR_ICMS_SUB
		{
			get { return _VALOR_ICMS_SUB; }
			set { _VALOR_ICMS_SUB = value; }
		}

		public decimal? VALOR_TOT_PRO
		{
			get { return _VALOR_TOT_PRO; }
			set { _VALOR_TOT_PRO = value; }
		}

		public decimal? VALOR_FRETE
		{
			get { return _VALOR_FRETE; }
			set { _VALOR_FRETE = value; }
		}

		public decimal? VALOR_SEGURO
		{
			get { return _VALOR_SEGURO; }
			set { _VALOR_SEGURO = value; }
		}

		public decimal? OUTRAS_DESPESAS
		{
			get { return _OUTRAS_DESPESAS; }
			set { _OUTRAS_DESPESAS = value; }
		}

		public decimal? VALOR_IPI
		{
			get { return _VALOR_IPI; }
			set { _VALOR_IPI = value; }
		}

		public decimal? VALOR_TOT_SER
		{
			get { return _VALOR_TOT_SER; }
			set { _VALOR_TOT_SER = value; }
		}

		public decimal? VALOR_ISS
		{
			get { return _VALOR_ISS; }
			set { _VALOR_ISS = value; }
		}

		public decimal? PERCE_ISS
		{
			get { return _PERCE_ISS; }
			set { _PERCE_ISS = value; }
		}

		public decimal? VALOR_IRRF
		{
			get { return _VALOR_IRRF; }
			set { _VALOR_IRRF = value; }
		}

		public decimal? VALOR_INSS
		{
			get { return _VALOR_INSS; }
			set { _VALOR_INSS = value; }
		}

		public decimal? VALOR_PIS
		{
			get { return _VALOR_PIS; }
			set { _VALOR_PIS = value; }
		}

		public decimal? VALOR_PIS_SUB
		{
			get { return _VALOR_PIS_SUB; }
			set { _VALOR_PIS_SUB = value; }
		}

		public decimal? VALOR_COFINS
		{
			get { return _VALOR_COFINS; }
			set { _VALOR_COFINS = value; }
		}

		public decimal? VALOR_COFINS_SUB
		{
			get { return _VALOR_COFINS_SUB; }
			set { _VALOR_COFINS_SUB = value; }
		}

		public decimal? VALOR_CS
		{
			get { return _VALOR_CS; }
			set { _VALOR_CS = value; }
		}

		public decimal? VALOR_LIQ_SER
		{
			get { return _VALOR_LIQ_SER; }
			set { _VALOR_LIQ_SER = value; }
		}

		public decimal? VALOR_TOT_NOTA
		{
			get { return _VALOR_TOT_NOTA; }
			set { _VALOR_TOT_NOTA = value; }
		}

		public decimal? CRED_ICM_SIMPLES
		{
			get { return _CRED_ICM_SIMPLES; }
			set { _CRED_ICM_SIMPLES = value; }
		}

		public string TRANSPORTADORA_1
		{
			get { return _TRANSPORTADORA_1; }
			set { _TRANSPORTADORA_1 = value; }
		}

		public string FRETE_CONTA_1
		{
			get { return _FRETE_CONTA_1; }
			set { _FRETE_CONTA_1 = value; }
		}

		public string QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public string ESPECIE
		{
			get { return _ESPECIE; }
			set { _ESPECIE = value; }
		}

		public string MARCA
		{
			get { return _MARCA; }
			set { _MARCA = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public string PESO_BRUTO
		{
			get { return _PESO_BRUTO; }
			set { _PESO_BRUTO = value; }
		}

		public string PESO_LIQUIDO
		{
			get { return _PESO_LIQUIDO; }
			set { _PESO_LIQUIDO = value; }
		}

		public string VENDEDOR
		{
			get { return _VENDEDOR; }
			set { _VENDEDOR = value; }
		}

		public decimal? CUSTO_FINANCEIRO
		{
			get { return _CUSTO_FINANCEIRO; }
			set { _CUSTO_FINANCEIRO = value; }
		}

		public string NFE_STATUS
		{
			get { return _NFE_STATUS; }
			set { _NFE_STATUS = value; }
		}

		public string NFE_CHAVE
		{
			get { return _NFE_CHAVE; }
			set { _NFE_CHAVE = value; }
		}

		public string NFE_RECENT
		{
			get { return _NFE_RECENT; }
			set { _NFE_RECENT = value; }
		}

		public string NFE_PROENT
		{
			get { return _NFE_PROENT; }
			set { _NFE_PROENT = value; }
		}

		public DateTime? NFE_DATHORENT
		{
			get { return _NFE_DATHORENT; }
			set { _NFE_DATHORENT = value; }
		}

		public string NFE_PROCAN
		{
			get { return _NFE_PROCAN; }
			set { _NFE_PROCAN = value; }
		}

		public DateTime? NFE_DATHORCAN
		{
			get { return _NFE_DATHORCAN; }
			set { _NFE_DATHORCAN = value; }
		}

		public string NFE_AMBIENTE
		{
			get { return _NFE_AMBIENTE; }
			set { _NFE_AMBIENTE = value; }
		}

        public int? CANCELADA
		{
			get { return _CANCELADA; }
			set { _CANCELADA = value; }
		}

		#endregion
	}
}
