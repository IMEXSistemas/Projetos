using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONHECIMENTOTRANSPEntity
	{
		private int _IDCONHECIMENTOTRANSP;
		private DateTime? _DATA;
		private string _NDOCUMENTO;
		private string _MODELO;
		private string _SERIE;
		private int? _IDTRANSPORTADORA;
		private int? _IDCFOP;
		private decimal? _VLTOTAL;
		private decimal? _VLBASEICMS;
		private decimal? _VLICMS;
		private decimal? _OUTRAS;
		private string _OBSERVACAO;
		private string _MODALIDADE;

		#region Construtores

		//Construtor default
		public CONHECIMENTOTRANSPEntity() {
			this._DATA = null;
			this._IDTRANSPORTADORA = null;
			this._IDCFOP = null;
			this._VLTOTAL = null;
			this._VLBASEICMS = null;
			this._VLICMS = null;
			this._OUTRAS = null;
		}

		public CONHECIMENTOTRANSPEntity(int IDCONHECIMENTOTRANSP, DateTime? DATA, string NDOCUMENTO, string MODELO, string SERIE, int? IDTRANSPORTADORA, int? IDCFOP, decimal? VLTOTAL, decimal? VLBASEICMS, decimal? VLICMS, decimal? OUTRAS, string OBSERVACAO, string MODALIDADE) {

			this._IDCONHECIMENTOTRANSP = IDCONHECIMENTOTRANSP;
			this._DATA = DATA;
			this._NDOCUMENTO = NDOCUMENTO;
			this._MODELO = MODELO;
			this._SERIE = SERIE;
			this._IDTRANSPORTADORA = IDTRANSPORTADORA;
			this._IDCFOP = IDCFOP;
			this._VLTOTAL = VLTOTAL;
			this._VLBASEICMS = VLBASEICMS;
			this._VLICMS = VLICMS;
			this._OUTRAS = OUTRAS;
			this._OBSERVACAO = OBSERVACAO;
			this._MODALIDADE = MODALIDADE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONHECIMENTOTRANSP
		{
			get { return _IDCONHECIMENTOTRANSP; }
			set { _IDCONHECIMENTOTRANSP = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string NDOCUMENTO
		{
			get { return _NDOCUMENTO; }
			set { _NDOCUMENTO = value; }
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

		public int? IDTRANSPORTADORA
		{
			get { return _IDTRANSPORTADORA; }
			set { _IDTRANSPORTADORA = value; }
		}

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public decimal? VLTOTAL
		{
			get { return _VLTOTAL; }
			set { _VLTOTAL = value; }
		}

		public decimal? VLBASEICMS
		{
			get { return _VLBASEICMS; }
			set { _VLBASEICMS = value; }
		}

		public decimal? VLICMS
		{
			get { return _VLICMS; }
			set { _VLICMS = value; }
		}

		public decimal? OUTRAS
		{
			get { return _OUTRAS; }
			set { _OUTRAS = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string MODALIDADE
		{
			get { return _MODALIDADE; }
			set { _MODALIDADE = value; }
		}

		#endregion
	}
}
