using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CAIXAEntity
	{
		private int _IDCAIXA;
		private int? _IDTIPOMOVCAIXA;
		private string _NDOCUMENTO;
		private int? _IDTIPODUPLICATA;
		private int? _IDCENTROCUSTOS;
		private decimal? _VALOR;
		private string _OBSERVACAO;
		private DateTime? _DATAMOV;

		#region Construtores

		//Construtor default
		public CAIXAEntity() {
			this._IDTIPOMOVCAIXA = null;
			this._IDTIPODUPLICATA = null;
			this._IDCENTROCUSTOS = null;
			this._VALOR = null;
			this._DATAMOV = null;
		}

		public CAIXAEntity(int IDCAIXA, int? IDTIPOMOVCAIXA, string NDOCUMENTO, int? IDTIPODUPLICATA, int? IDCENTROCUSTOS, decimal? VALOR, string OBSERVACAO, DateTime? DATAMOV) {

			this._IDCAIXA = IDCAIXA;
			this._IDTIPOMOVCAIXA = IDTIPOMOVCAIXA;
			this._NDOCUMENTO = NDOCUMENTO;
			this._IDTIPODUPLICATA = IDTIPODUPLICATA;
			this._IDCENTROCUSTOS = IDCENTROCUSTOS;
			this._VALOR = VALOR;
			this._OBSERVACAO = OBSERVACAO;
			this._DATAMOV = DATAMOV;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCAIXA
		{
			get { return _IDCAIXA; }
			set { _IDCAIXA = value; }
		}

		public int? IDTIPOMOVCAIXA
		{
			get { return _IDTIPOMOVCAIXA; }
			set { _IDTIPOMOVCAIXA = value; }
		}

		public string NDOCUMENTO
		{
			get { return _NDOCUMENTO; }
			set { _NDOCUMENTO = value; }
		}

		public int? IDTIPODUPLICATA
		{
			get { return _IDTIPODUPLICATA; }
			set { _IDTIPODUPLICATA = value; }
		}

		public int? IDCENTROCUSTOS
		{
			get { return _IDCENTROCUSTOS; }
			set { _IDCENTROCUSTOS = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public DateTime? DATAMOV
		{
			get { return _DATAMOV; }
			set { _DATAMOV = value; }
		}

		#endregion
	}
}
