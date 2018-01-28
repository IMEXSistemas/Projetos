using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MOVCONTACORRENTEEntity
	{
		private int? _IDMOVCTCORRENTE;
		private string _NUMMOVIMENTACAO;
		private int? _IDCONTACORRENTE;
		private int? _IDMOVIMENTACAO;
		private decimal? _VALOR;
		private DateTime? _DATAMOVIMENTACAO;
		private int? _IDTIPOMOVCAIXA;
		private string _OBSERVACAO;
		private string _NOMECONTACORR;
		private string _NOMECONTA;
		private string _AGENCIA;
		private string _NOMEMOVIMENTACAO;
		private string _NOMETIPOMOVCAIXA;
		private string _NOMEBANCO;

		#region Construtores

		//Construtor default
		public LIS_MOVCONTACORRENTEEntity() { }

		public LIS_MOVCONTACORRENTEEntity(int? IDMOVCTCORRENTE, string NUMMOVIMENTACAO, int? IDCONTACORRENTE, int? IDMOVIMENTACAO, decimal? VALOR, DateTime? DATAMOVIMENTACAO, int? IDTIPOMOVCAIXA, string OBSERVACAO, string NOMECONTACORR, string NOMECONTA, string AGENCIA, string NOMEMOVIMENTACAO, string NOMETIPOMOVCAIXA, string NOMEBANCO)		{

			this._IDMOVCTCORRENTE = IDMOVCTCORRENTE;
			this._NUMMOVIMENTACAO = NUMMOVIMENTACAO;
			this._IDCONTACORRENTE = IDCONTACORRENTE;
			this._IDMOVIMENTACAO = IDMOVIMENTACAO;
			this._VALOR = VALOR;
			this._DATAMOVIMENTACAO = DATAMOVIMENTACAO;
			this._IDTIPOMOVCAIXA = IDTIPOMOVCAIXA;
			this._OBSERVACAO = OBSERVACAO;
			this._NOMECONTACORR = NOMECONTACORR;
			this._NOMECONTA = NOMECONTA;
			this._AGENCIA = AGENCIA;
			this._NOMEMOVIMENTACAO = NOMEMOVIMENTACAO;
			this._NOMETIPOMOVCAIXA = NOMETIPOMOVCAIXA;
			this._NOMEBANCO = NOMEBANCO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMOVCTCORRENTE
		{
			get { return _IDMOVCTCORRENTE; }
			set { _IDMOVCTCORRENTE = value; }
		}

		public string NUMMOVIMENTACAO
		{
			get { return _NUMMOVIMENTACAO; }
			set { _NUMMOVIMENTACAO = value; }
		}

		public int? IDCONTACORRENTE
		{
			get { return _IDCONTACORRENTE; }
			set { _IDCONTACORRENTE = value; }
		}

		public int? IDMOVIMENTACAO
		{
			get { return _IDMOVIMENTACAO; }
			set { _IDMOVIMENTACAO = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public DateTime? DATAMOVIMENTACAO
		{
			get { return _DATAMOVIMENTACAO; }
			set { _DATAMOVIMENTACAO = value; }
		}

		public int? IDTIPOMOVCAIXA
		{
			get { return _IDTIPOMOVCAIXA; }
			set { _IDTIPOMOVCAIXA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string NOMECONTACORR
		{
			get { return _NOMECONTACORR; }
			set { _NOMECONTACORR = value; }
		}

		public string NOMECONTA
		{
			get { return _NOMECONTA; }
			set { _NOMECONTA = value; }
		}

		public string AGENCIA
		{
			get { return _AGENCIA; }
			set { _AGENCIA = value; }
		}

		public string NOMEMOVIMENTACAO
		{
			get { return _NOMEMOVIMENTACAO; }
			set { _NOMEMOVIMENTACAO = value; }
		}

		public string NOMETIPOMOVCAIXA
		{
			get { return _NOMETIPOMOVCAIXA; }
			set { _NOMETIPOMOVCAIXA = value; }
		}

		public string NOMEBANCO
		{
			get { return _NOMEBANCO; }
			set { _NOMEBANCO = value; }
		}

		#endregion
	}
}
