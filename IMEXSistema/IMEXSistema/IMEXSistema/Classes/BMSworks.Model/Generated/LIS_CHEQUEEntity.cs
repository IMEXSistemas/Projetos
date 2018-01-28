using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CHEQUEEntity
	{
		private int? _IDCHEQUE;
		private string _NUMERO;
		private string _AGENCIA;
		private string _CONTA;
		private string _DIGCONTA;
		private decimal? _VALOR;
		private DateTime? _ENTRADA;
		private DateTime? _BOMPARA;
		private int? _IDCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _DESCCENTROCUSTO;
		private int? _IDBANCO;
		private string _NOMEBANCO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNC;
		private string _TIPORECEBIMENTO;
		private string _NOMECLIENTEFORNEC;
		private int? _IDCLIENTE;
		private int? _IDFORNECEDOR;
		private string _TITULAR;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public LIS_CHEQUEEntity() { }

		public LIS_CHEQUEEntity(int? IDCHEQUE, string NUMERO, string AGENCIA, string CONTA, string DIGCONTA, decimal? VALOR, DateTime? ENTRADA, DateTime? BOMPARA, int? IDCENTROCUSTO, string CENTROCUSTO, string DESCCENTROCUSTO, int? IDBANCO, string NOMEBANCO, int? IDSTATUS, string NOMESTATUS, int? IDFUNCIONARIO, string NOMEFUNC, string TIPORECEBIMENTO, string NOMECLIENTEFORNEC, int? IDCLIENTE, int? IDFORNECEDOR, string TITULAR, string OBSERVACAO)		{

			this._IDCHEQUE = IDCHEQUE;
			this._NUMERO = NUMERO;
			this._AGENCIA = AGENCIA;
			this._CONTA = CONTA;
			this._DIGCONTA = DIGCONTA;
			this._VALOR = VALOR;
			this._ENTRADA = ENTRADA;
			this._BOMPARA = BOMPARA;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DESCCENTROCUSTO = DESCCENTROCUSTO;
			this._IDBANCO = IDBANCO;
			this._NOMEBANCO = NOMEBANCO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNC = NOMEFUNC;
			this._TIPORECEBIMENTO = TIPORECEBIMENTO;
			this._NOMECLIENTEFORNEC = NOMECLIENTEFORNEC;
			this._IDCLIENTE = IDCLIENTE;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._TITULAR = TITULAR;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCHEQUE
		{
			get { return _IDCHEQUE; }
			set { _IDCHEQUE = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
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

		public string DIGCONTA
		{
			get { return _DIGCONTA; }
			set { _DIGCONTA = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public DateTime? ENTRADA
		{
			get { return _ENTRADA; }
			set { _ENTRADA = value; }
		}

		public DateTime? BOMPARA
		{
			get { return _BOMPARA; }
			set { _BOMPARA = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DESCCENTROCUSTO
		{
			get { return _DESCCENTROCUSTO; }
			set { _DESCCENTROCUSTO = value; }
		}

		public int? IDBANCO
		{
			get { return _IDBANCO; }
			set { _IDBANCO = value; }
		}

		public string NOMEBANCO
		{
			get { return _NOMEBANCO; }
			set { _NOMEBANCO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEFUNC
		{
			get { return _NOMEFUNC; }
			set { _NOMEFUNC = value; }
		}

		public string TIPORECEBIMENTO
		{
			get { return _TIPORECEBIMENTO; }
			set { _TIPORECEBIMENTO = value; }
		}

		public string NOMECLIENTEFORNEC
		{
			get { return _NOMECLIENTEFORNEC; }
			set { _NOMECLIENTEFORNEC = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string TITULAR
		{
			get { return _TITULAR; }
			set { _TITULAR = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
