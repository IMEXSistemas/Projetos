using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class RECEBEREntity
	{
		private string _DOCUMENTO;
		private string _HISTORICO;
		private string _COD_CLIENTE;
		private string _NOM_CLIENTE;
		private DateTime? _EMISSAO;
		private DateTime? _VENCIMENTO;
		private decimal? _VALOR_DUP;
		private DateTime? _RECEBIMENTO;
		private decimal? _VALOR_REC;
		private decimal? _VALOR_JUR;
		private decimal? _VALOR_DES;
		private string _ESPECIE;
		private string _NUM_CONTA;
		private string _CTO_CUSTO;
		private string _PORTADOR;
		private string _TIPO_DOC;
		private string _COMP;
		private string _BANCO;
		private string _AGENCIA;
		private string _CONTA;
		private string _CHEQUE;
		private string _OBSERVACOES;
		private string _NOSSO_NUM;

		#region Construtores

		//Construtor default
		public RECEBEREntity() {
			this._EMISSAO = null;
			this._VENCIMENTO = null;
			this._VALOR_DUP = null;
			this._RECEBIMENTO = null;
			this._VALOR_REC = null;
			this._VALOR_JUR = null;
			this._VALOR_DES = null;
		}

		public RECEBEREntity(string DOCUMENTO, string HISTORICO, string COD_CLIENTE, string NOM_CLIENTE, DateTime? EMISSAO, DateTime? VENCIMENTO, decimal? VALOR_DUP, DateTime? RECEBIMENTO, decimal? VALOR_REC, decimal? VALOR_JUR, decimal? VALOR_DES, string ESPECIE, string NUM_CONTA, string CTO_CUSTO, string PORTADOR, string TIPO_DOC, string COMP, string BANCO, string AGENCIA, string CONTA, string CHEQUE, string OBSERVACOES, string NOSSO_NUM) {

			this._DOCUMENTO = DOCUMENTO;
			this._HISTORICO = HISTORICO;
			this._COD_CLIENTE = COD_CLIENTE;
			this._NOM_CLIENTE = NOM_CLIENTE;
			this._EMISSAO = EMISSAO;
			this._VENCIMENTO = VENCIMENTO;
			this._VALOR_DUP = VALOR_DUP;
			this._RECEBIMENTO = RECEBIMENTO;
			this._VALOR_REC = VALOR_REC;
			this._VALOR_JUR = VALOR_JUR;
			this._VALOR_DES = VALOR_DES;
			this._ESPECIE = ESPECIE;
			this._NUM_CONTA = NUM_CONTA;
			this._CTO_CUSTO = CTO_CUSTO;
			this._PORTADOR = PORTADOR;
			this._TIPO_DOC = TIPO_DOC;
			this._COMP = COMP;
			this._BANCO = BANCO;
			this._AGENCIA = AGENCIA;
			this._CONTA = CONTA;
			this._CHEQUE = CHEQUE;
			this._OBSERVACOES = OBSERVACOES;
			this._NOSSO_NUM = NOSSO_NUM;
		}
		#endregion

		#region Propriedades Get/Set

		public string DOCUMENTO
		{
			get { return _DOCUMENTO; }
			set { _DOCUMENTO = value; }
		}

		public string HISTORICO
		{
			get { return _HISTORICO; }
			set { _HISTORICO = value; }
		}

		public string COD_CLIENTE
		{
			get { return _COD_CLIENTE; }
			set { _COD_CLIENTE = value; }
		}

		public string NOM_CLIENTE
		{
			get { return _NOM_CLIENTE; }
			set { _NOM_CLIENTE = value; }
		}

		public DateTime? EMISSAO
		{
			get { return _EMISSAO; }
			set { _EMISSAO = value; }
		}

		public DateTime? VENCIMENTO
		{
			get { return _VENCIMENTO; }
			set { _VENCIMENTO = value; }
		}

		public decimal? VALOR_DUP
		{
			get { return _VALOR_DUP; }
			set { _VALOR_DUP = value; }
		}

		public DateTime? RECEBIMENTO
		{
			get { return _RECEBIMENTO; }
			set { _RECEBIMENTO = value; }
		}

		public decimal? VALOR_REC
		{
			get { return _VALOR_REC; }
			set { _VALOR_REC = value; }
		}

		public decimal? VALOR_JUR
		{
			get { return _VALOR_JUR; }
			set { _VALOR_JUR = value; }
		}

		public decimal? VALOR_DES
		{
			get { return _VALOR_DES; }
			set { _VALOR_DES = value; }
		}

		public string ESPECIE
		{
			get { return _ESPECIE; }
			set { _ESPECIE = value; }
		}

		public string NUM_CONTA
		{
			get { return _NUM_CONTA; }
			set { _NUM_CONTA = value; }
		}

		public string CTO_CUSTO
		{
			get { return _CTO_CUSTO; }
			set { _CTO_CUSTO = value; }
		}

		public string PORTADOR
		{
			get { return _PORTADOR; }
			set { _PORTADOR = value; }
		}

		public string TIPO_DOC
		{
			get { return _TIPO_DOC; }
			set { _TIPO_DOC = value; }
		}

		public string COMP
		{
			get { return _COMP; }
			set { _COMP = value; }
		}

		public string BANCO
		{
			get { return _BANCO; }
			set { _BANCO = value; }
		}

		public string AGENCIA
		{
			get { return _AGENCIA; }
			set { _AGENCIA = value; }
		}

		public string CONTA
		{
			get { return _CONTA; }
			set { _CONTA = value; }
		}

		public string CHEQUE
		{
			get { return _CHEQUE; }
			set { _CHEQUE = value; }
		}

		public string OBSERVACOES
		{
			get { return _OBSERVACOES; }
			set { _OBSERVACOES = value; }
		}

		public string NOSSO_NUM
		{
			get { return _NOSSO_NUM; }
			set { _NOSSO_NUM = value; }
		}

		#endregion
	}
}
