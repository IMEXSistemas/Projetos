using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MOVCONTACORRENTEEntity
	{
		private int _IDMOVCTCORRENTE;
		private string _NUMMOVIMENTACAO;
		private int? _IDCONTACORRENTE;
		private int? _IDMOVIMENTACAO;
		private int? _IDTIPOMOVCAIXA;
		private string _OBSERVACAO;
		private decimal? _VALOR;
		private DateTime? _DATAMOVIMENTACAO;

		#region Construtores

		//Construtor default
		public MOVCONTACORRENTEEntity() {
			this._IDCONTACORRENTE = null;
			this._IDMOVIMENTACAO = null;
			this._IDTIPOMOVCAIXA = null;
			this._VALOR = null;
			this._DATAMOVIMENTACAO = null;
		}

		public MOVCONTACORRENTEEntity(int IDMOVCTCORRENTE, string NUMMOVIMENTACAO, int? IDCONTACORRENTE, int? IDMOVIMENTACAO, int? IDTIPOMOVCAIXA, string OBSERVACAO, decimal? VALOR, DateTime? DATAMOVIMENTACAO) {

			this._IDMOVCTCORRENTE = IDMOVCTCORRENTE;
			this._NUMMOVIMENTACAO = NUMMOVIMENTACAO;
			this._IDCONTACORRENTE = IDCONTACORRENTE;
			this._IDMOVIMENTACAO = IDMOVIMENTACAO;
			this._IDTIPOMOVCAIXA = IDTIPOMOVCAIXA;
			this._OBSERVACAO = OBSERVACAO;
			this._VALOR = VALOR;
			this._DATAMOVIMENTACAO = DATAMOVIMENTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMOVCTCORRENTE
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

		#endregion
	}
}
