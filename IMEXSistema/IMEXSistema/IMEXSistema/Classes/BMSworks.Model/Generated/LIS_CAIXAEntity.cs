using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CAIXAEntity
	{
		private int? _IDCAIXA;
		private int? _IDTIPOMOVCAIXA;
		private string _NDOCUMENTO;
		private int? _IDTIPODUPLICATA;
		private int? _IDCENTROCUSTOS;
		private decimal? _VALOR;
		private string _OBSERVACAO;
		private string _NOMEMOVCAIXA;
		private string _NOMETIPODUPLICATA;
		private string _CENTROCUSTO;
		private string _DESCENTROCUSTO;
		private DateTime? _DATAMOV;

		#region Construtores

		//Construtor default
		public LIS_CAIXAEntity() { }

		public LIS_CAIXAEntity(int? IDCAIXA, int? IDTIPOMOVCAIXA, string NDOCUMENTO, int? IDTIPODUPLICATA, int? IDCENTROCUSTOS, decimal? VALOR, string OBSERVACAO, string NOMEMOVCAIXA, string NOMETIPODUPLICATA, string CENTROCUSTO, string DESCENTROCUSTO, DateTime? DATAMOV)		{

			this._IDCAIXA = IDCAIXA;
			this._IDTIPOMOVCAIXA = IDTIPOMOVCAIXA;
			this._NDOCUMENTO = NDOCUMENTO;
			this._IDTIPODUPLICATA = IDTIPODUPLICATA;
			this._IDCENTROCUSTOS = IDCENTROCUSTOS;
			this._VALOR = VALOR;
			this._OBSERVACAO = OBSERVACAO;
			this._NOMEMOVCAIXA = NOMEMOVCAIXA;
			this._NOMETIPODUPLICATA = NOMETIPODUPLICATA;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DESCENTROCUSTO = DESCENTROCUSTO;
			this._DATAMOV = DATAMOV;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCAIXA
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

		public string NOMEMOVCAIXA
		{
			get { return _NOMEMOVCAIXA; }
			set { _NOMEMOVCAIXA = value; }
		}

		public string NOMETIPODUPLICATA
		{
			get { return _NOMETIPODUPLICATA; }
			set { _NOMETIPODUPLICATA = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DESCENTROCUSTO
		{
			get { return _DESCENTROCUSTO; }
			set { _DESCENTROCUSTO = value; }
		}

		public DateTime? DATAMOV
		{
			get { return _DATAMOV; }
			set { _DATAMOV = value; }
		}

		#endregion
	}
}
