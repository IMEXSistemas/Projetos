using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FRENTE_MAPA_0002Entity
	{
		private string _N_SERIE;
		private string _N_CAIXA;
		private string _CONTADOR_REDUCAO;
		private string _CONTADOR_REINICIO;
		private DateTime? _DATA;
		private string _PRIMEIRO_CUPOM;
		private string _ULTIMO_CUPOM;
		private decimal? _VENDA_BRUTA;
		private decimal? _VALOR_INICIO_DIA;
		private decimal? _VALOR_FINAL_DIA;
		private decimal? _TRIBF;
		private decimal? _TRIBI;
		private decimal? _TRIBN;
		private decimal? _TRIBA;
		private decimal? _TRIBD;
		private decimal? _TRIBC;
		private decimal? _TRIBS;
		private decimal? _T0700;
		private decimal? _T1200;
		private decimal? _T1800;
		private decimal? _T2500;
		private decimal? _TRICS;
		private decimal? _TRIAS;
		private decimal? _TRIDS;
		private short? _SINCRONIZADO;

		#region Construtores

		//Construtor default
		public FRENTE_MAPA_0002Entity() {
			this._DATA = null;
			this._VENDA_BRUTA = null;
			this._VALOR_INICIO_DIA = null;
			this._VALOR_FINAL_DIA = null;
			this._TRIBF = null;
			this._TRIBI = null;
			this._TRIBN = null;
			this._TRIBA = null;
			this._TRIBD = null;
			this._TRIBC = null;
			this._TRIBS = null;
			this._T0700 = null;
			this._T1200 = null;
			this._T1800 = null;
			this._T2500 = null;
			this._TRICS = null;
			this._TRIAS = null;
			this._TRIDS = null;
			this._SINCRONIZADO = null;
		}

		public FRENTE_MAPA_0002Entity(string N_SERIE, string N_CAIXA, string CONTADOR_REDUCAO, string CONTADOR_REINICIO, DateTime? DATA, string PRIMEIRO_CUPOM, string ULTIMO_CUPOM, decimal? VENDA_BRUTA, decimal? VALOR_INICIO_DIA, decimal? VALOR_FINAL_DIA, decimal? TRIBF, decimal? TRIBI, decimal? TRIBN, decimal? TRIBA, decimal? TRIBD, decimal? TRIBC, decimal? TRIBS, decimal? T0700, decimal? T1200, decimal? T1800, decimal? T2500, decimal? TRICS, decimal? TRIAS, decimal? TRIDS, short? SINCRONIZADO) {

			this._N_SERIE = N_SERIE;
			this._N_CAIXA = N_CAIXA;
			this._CONTADOR_REDUCAO = CONTADOR_REDUCAO;
			this._CONTADOR_REINICIO = CONTADOR_REINICIO;
			this._DATA = DATA;
			this._PRIMEIRO_CUPOM = PRIMEIRO_CUPOM;
			this._ULTIMO_CUPOM = ULTIMO_CUPOM;
			this._VENDA_BRUTA = VENDA_BRUTA;
			this._VALOR_INICIO_DIA = VALOR_INICIO_DIA;
			this._VALOR_FINAL_DIA = VALOR_FINAL_DIA;
			this._TRIBF = TRIBF;
			this._TRIBI = TRIBI;
			this._TRIBN = TRIBN;
			this._TRIBA = TRIBA;
			this._TRIBD = TRIBD;
			this._TRIBC = TRIBC;
			this._TRIBS = TRIBS;
			this._T0700 = T0700;
			this._T1200 = T1200;
			this._T1800 = T1800;
			this._T2500 = T2500;
			this._TRICS = TRICS;
			this._TRIAS = TRIAS;
			this._TRIDS = TRIDS;
			this._SINCRONIZADO = SINCRONIZADO;
		}
		#endregion

		#region Propriedades Get/Set

		public string N_SERIE
		{
			get { return _N_SERIE; }
			set { _N_SERIE = value; }
		}

		public string N_CAIXA
		{
			get { return _N_CAIXA; }
			set { _N_CAIXA = value; }
		}

		public string CONTADOR_REDUCAO
		{
			get { return _CONTADOR_REDUCAO; }
			set { _CONTADOR_REDUCAO = value; }
		}

		public string CONTADOR_REINICIO
		{
			get { return _CONTADOR_REINICIO; }
			set { _CONTADOR_REINICIO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string PRIMEIRO_CUPOM
		{
			get { return _PRIMEIRO_CUPOM; }
			set { _PRIMEIRO_CUPOM = value; }
		}

		public string ULTIMO_CUPOM
		{
			get { return _ULTIMO_CUPOM; }
			set { _ULTIMO_CUPOM = value; }
		}

		public decimal? VENDA_BRUTA
		{
			get { return _VENDA_BRUTA; }
			set { _VENDA_BRUTA = value; }
		}

		public decimal? VALOR_INICIO_DIA
		{
			get { return _VALOR_INICIO_DIA; }
			set { _VALOR_INICIO_DIA = value; }
		}

		public decimal? VALOR_FINAL_DIA
		{
			get { return _VALOR_FINAL_DIA; }
			set { _VALOR_FINAL_DIA = value; }
		}

		public decimal? TRIBF
		{
			get { return _TRIBF; }
			set { _TRIBF = value; }
		}

		public decimal? TRIBI
		{
			get { return _TRIBI; }
			set { _TRIBI = value; }
		}

		public decimal? TRIBN
		{
			get { return _TRIBN; }
			set { _TRIBN = value; }
		}

		public decimal? TRIBA
		{
			get { return _TRIBA; }
			set { _TRIBA = value; }
		}

		public decimal? TRIBD
		{
			get { return _TRIBD; }
			set { _TRIBD = value; }
		}

		public decimal? TRIBC
		{
			get { return _TRIBC; }
			set { _TRIBC = value; }
		}

		public decimal? TRIBS
		{
			get { return _TRIBS; }
			set { _TRIBS = value; }
		}

		public decimal? T0700
		{
			get { return _T0700; }
			set { _T0700 = value; }
		}

		public decimal? T1200
		{
			get { return _T1200; }
			set { _T1200 = value; }
		}

		public decimal? T1800
		{
			get { return _T1800; }
			set { _T1800 = value; }
		}

		public decimal? T2500
		{
			get { return _T2500; }
			set { _T2500 = value; }
		}

		public decimal? TRICS
		{
			get { return _TRICS; }
			set { _TRICS = value; }
		}

		public decimal? TRIAS
		{
			get { return _TRIAS; }
			set { _TRIAS = value; }
		}

		public decimal? TRIDS
		{
			get { return _TRIDS; }
			set { _TRIDS = value; }
		}

		public short? SINCRONIZADO
		{
			get { return _SINCRONIZADO; }
			set { _SINCRONIZADO = value; }
		}

		#endregion
	}
}
