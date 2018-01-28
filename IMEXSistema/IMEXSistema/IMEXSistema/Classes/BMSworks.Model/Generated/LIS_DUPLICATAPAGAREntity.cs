using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_DUPLICATAPAGAREntity
	{
		private int? _IDDUPLICATAPAGAR;
		private string _NUMERO;
		private int? _IDFORNECEDOR;
		private string _NOMEFORNECEDOR;
		private int? _IDCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _NOMECENTROCUSTO;
		private DateTime? _DATAEMISSAO;
		private DateTime? _DATAVECTO;
		private DateTime? _DATAPAGTO;
		private int? _IDTIPODUPLICATA;
		private string _NOMETIPODUPLICATA;
		private decimal? _VALORDUPLICATA;
		private decimal? _VALORDESCONTO;
		private decimal? _VALORMULTA;
		private decimal? _VALORPAGO;
		private decimal? _VALORJUROS;
		private decimal? _VALORDEVEDOR;
		private string _NOTAFISCAL;
		private string _NCHEQUE;
		private int? _IDLOCALCOBRANCA;
		private string _NOMELOCALCOBRANCA;
		private string _OBSERVACAO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _DIASATRASO;
		private DateTime? _DATAATJUROS;

		#region Construtores

		//Construtor default
		public LIS_DUPLICATAPAGAREntity() { }

		public LIS_DUPLICATAPAGAREntity(int? IDDUPLICATAPAGAR, string NUMERO, int? IDFORNECEDOR, string NOMEFORNECEDOR, int? IDCENTROCUSTO, string CENTROCUSTO, string NOMECENTROCUSTO, DateTime? DATAEMISSAO, DateTime? DATAVECTO, DateTime? DATAPAGTO, int? IDTIPODUPLICATA, string NOMETIPODUPLICATA, decimal? VALORDUPLICATA, decimal? VALORDESCONTO, decimal? VALORMULTA, decimal? VALORPAGO, decimal? VALORJUROS, decimal? VALORDEVEDOR, string NOTAFISCAL, string NCHEQUE, int? IDLOCALCOBRANCA, string NOMELOCALCOBRANCA, string OBSERVACAO, int? IDSTATUS, string NOMESTATUS, int? DIASATRASO, DateTime? DATAATJUROS)		{

			this._IDDUPLICATAPAGAR = IDDUPLICATAPAGAR;
			this._NUMERO = NUMERO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._NOMECENTROCUSTO = NOMECENTROCUSTO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._DATAVECTO = DATAVECTO;
			this._DATAPAGTO = DATAPAGTO;
			this._IDTIPODUPLICATA = IDTIPODUPLICATA;
			this._NOMETIPODUPLICATA = NOMETIPODUPLICATA;
			this._VALORDUPLICATA = VALORDUPLICATA;
			this._VALORDESCONTO = VALORDESCONTO;
			this._VALORMULTA = VALORMULTA;
			this._VALORPAGO = VALORPAGO;
			this._VALORJUROS = VALORJUROS;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._NOTAFISCAL = NOTAFISCAL;
			this._NCHEQUE = NCHEQUE;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._NOMELOCALCOBRANCA = NOMELOCALCOBRANCA;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._DIASATRASO = DIASATRASO;
			this._DATAATJUROS = DATAATJUROS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDDUPLICATAPAGAR
		{
			get { return _IDDUPLICATAPAGAR; }
			set { _IDDUPLICATAPAGAR = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string NOMEFORNECEDOR
		{
			get { return _NOMEFORNECEDOR; }
			set { _NOMEFORNECEDOR = value; }
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

		public string NOMECENTROCUSTO
		{
			get { return _NOMECENTROCUSTO; }
			set { _NOMECENTROCUSTO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public DateTime? DATAVECTO
		{
			get { return _DATAVECTO; }
			set { _DATAVECTO = value; }
		}

		public DateTime? DATAPAGTO
		{
			get { return _DATAPAGTO; }
			set { _DATAPAGTO = value; }
		}

		public int? IDTIPODUPLICATA
		{
			get { return _IDTIPODUPLICATA; }
			set { _IDTIPODUPLICATA = value; }
		}

		public string NOMETIPODUPLICATA
		{
			get { return _NOMETIPODUPLICATA; }
			set { _NOMETIPODUPLICATA = value; }
		}

		public decimal? VALORDUPLICATA
		{
			get { return _VALORDUPLICATA; }
			set { _VALORDUPLICATA = value; }
		}

		public decimal? VALORDESCONTO
		{
			get { return _VALORDESCONTO; }
			set { _VALORDESCONTO = value; }
		}

		public decimal? VALORMULTA
		{
			get { return _VALORMULTA; }
			set { _VALORMULTA = value; }
		}

		public decimal? VALORPAGO
		{
			get { return _VALORPAGO; }
			set { _VALORPAGO = value; }
		}

		public decimal? VALORJUROS
		{
			get { return _VALORJUROS; }
			set { _VALORJUROS = value; }
		}

		public decimal? VALORDEVEDOR
		{
			get { return _VALORDEVEDOR; }
			set { _VALORDEVEDOR = value; }
		}

		public string NOTAFISCAL
		{
			get { return _NOTAFISCAL; }
			set { _NOTAFISCAL = value; }
		}

		public string NCHEQUE
		{
			get { return _NCHEQUE; }
			set { _NCHEQUE = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public string NOMELOCALCOBRANCA
		{
			get { return _NOMELOCALCOBRANCA; }
			set { _NOMELOCALCOBRANCA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
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

		public int? DIASATRASO
		{
			get { return _DIASATRASO; }
			set { _DIASATRASO = value; }
		}

		public DateTime? DATAATJUROS
		{
			get { return _DATAATJUROS; }
			set { _DATAATJUROS = value; }
		}

		#endregion
	}
}
