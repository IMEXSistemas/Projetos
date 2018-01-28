using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_FECHOSERVICOEntity
	{
		private int? _IDFECHOSERVICO;
		private DateTime? _DATAEMISSAO;
		private int? _IDORDEMSERVICO;
		private decimal? _VALORORCAMENTO;
		private int? _PRAZOENTREGA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _OBSERVACAO;
		private decimal? _TOTALITEMSERVICO;
		private decimal? _TOTALITEMPECA;
		private decimal? _MAOOBRA;
		private decimal? _OUTROVALOR;
		private decimal? _TOTALFECHOS;
		private DateTime? _GARANTIAVECTO;
		private int? _IDFORMAPAGAMENTO;
		private string _NOMEFORMAPAGTO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;

		#region Construtores

		//Construtor default
		public LIS_FECHOSERVICOEntity() { }

		public LIS_FECHOSERVICOEntity(int? IDFECHOSERVICO, DateTime? DATAEMISSAO, int? IDORDEMSERVICO, decimal? VALORORCAMENTO, int? PRAZOENTREGA, int? IDSTATUS, string NOMESTATUS, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string OBSERVACAO, decimal? TOTALITEMSERVICO, decimal? TOTALITEMPECA, decimal? MAOOBRA, decimal? OUTROVALOR, decimal? TOTALFECHOS, DateTime? GARANTIAVECTO, int? IDFORMAPAGAMENTO, string NOMEFORMAPAGTO, int? IDCLIENTE, string NOMECLIENTE)		{

			this._IDFECHOSERVICO = IDFECHOSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._VALORORCAMENTO = VALORORCAMENTO;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALITEMSERVICO = TOTALITEMSERVICO;
			this._TOTALITEMPECA = TOTALITEMPECA;
			this._MAOOBRA = MAOOBRA;
			this._OUTROVALOR = OUTROVALOR;
			this._TOTALFECHOS = TOTALFECHOS;
			this._GARANTIAVECTO = GARANTIAVECTO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDFECHOSERVICO
		{
			get { return _IDFECHOSERVICO; }
			set { _IDFECHOSERVICO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		public decimal? VALORORCAMENTO
		{
			get { return _VALORORCAMENTO; }
			set { _VALORORCAMENTO = value; }
		}

		public int? PRAZOENTREGA
		{
			get { return _PRAZOENTREGA; }
			set { _PRAZOENTREGA = value; }
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

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALITEMSERVICO
		{
			get { return _TOTALITEMSERVICO; }
			set { _TOTALITEMSERVICO = value; }
		}

		public decimal? TOTALITEMPECA
		{
			get { return _TOTALITEMPECA; }
			set { _TOTALITEMPECA = value; }
		}

		public decimal? MAOOBRA
		{
			get { return _MAOOBRA; }
			set { _MAOOBRA = value; }
		}

		public decimal? OUTROVALOR
		{
			get { return _OUTROVALOR; }
			set { _OUTROVALOR = value; }
		}

		public decimal? TOTALFECHOS
		{
			get { return _TOTALFECHOS; }
			set { _TOTALFECHOS = value; }
		}

		public DateTime? GARANTIAVECTO
		{
			get { return _GARANTIAVECTO; }
			set { _GARANTIAVECTO = value; }
		}

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		#endregion
	}
}
