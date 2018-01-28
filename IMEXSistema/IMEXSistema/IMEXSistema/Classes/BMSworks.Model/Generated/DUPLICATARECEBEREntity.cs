using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DUPLICATARECEBEREntity
	{
		private int _IDDUPLICATARECEBER;
		private string _NUMERO;
		private int? _IDCLIENTE;
		private int? _IDCENTROCUSTO;
		private DateTime? _DATAEMISSAO;
		private DateTime? _DATAVECTO;
		private DateTime? _DATAPAGTO;
		private int? _IDTIPODUPLICATA;
		private decimal? _VALORDUPLICATA;
		private decimal? _VALORDESCONTO;
		private decimal? _VALORMULTA;
		private decimal? _VALORPAGO;
		private decimal? _VALORJUROS;
		private decimal? _VALORDEVEDOR;
		private string _NOTAFISCAL;
		private string _NCHEQUE;
		private int? _IDLOCALCOBRANCA;
		private string _OBSERVACAO;
		private int? _IDSTATUS;
		private int? _DIASATRASO;
		private DateTime? _DATAATJUROS;
		private int? _IDFUNCIONARIO;
		private decimal? _COMISSAO;

		#region Construtores

		//Construtor default
		public DUPLICATARECEBEREntity() {
			this._IDCLIENTE = null;
			this._IDCENTROCUSTO = null;
			this._DATAEMISSAO = null;
			this._DATAVECTO = null;
			this._DATAPAGTO = null;
			this._IDTIPODUPLICATA = null;
			this._VALORDUPLICATA = null;
			this._VALORDESCONTO = null;
			this._VALORMULTA = null;
			this._VALORPAGO = null;
			this._VALORJUROS = null;
			this._VALORDEVEDOR = null;
			this._IDLOCALCOBRANCA = null;
			this._IDSTATUS = null;
			this._DIASATRASO = null;
			this._DATAATJUROS = null;
			this._IDFUNCIONARIO = null;
			this._COMISSAO = null;
		}

		public DUPLICATARECEBEREntity(int IDDUPLICATARECEBER, string NUMERO, int? IDCLIENTE, int? IDCENTROCUSTO, DateTime? DATAEMISSAO, DateTime? DATAVECTO, DateTime? DATAPAGTO, int? IDTIPODUPLICATA, decimal? VALORDUPLICATA, decimal? VALORDESCONTO, decimal? VALORMULTA, decimal? VALORPAGO, decimal? VALORJUROS, decimal? VALORDEVEDOR, string NOTAFISCAL, string NCHEQUE, int? IDLOCALCOBRANCA, string OBSERVACAO, int? IDSTATUS, int? DIASATRASO, DateTime? DATAATJUROS, int? IDFUNCIONARIO, decimal? COMISSAO) {

			this._IDDUPLICATARECEBER = IDDUPLICATARECEBER;
			this._NUMERO = NUMERO;
			this._IDCLIENTE = IDCLIENTE;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._DATAVECTO = DATAVECTO;
			this._DATAPAGTO = DATAPAGTO;
			this._IDTIPODUPLICATA = IDTIPODUPLICATA;
			this._VALORDUPLICATA = VALORDUPLICATA;
			this._VALORDESCONTO = VALORDESCONTO;
			this._VALORMULTA = VALORMULTA;
			this._VALORPAGO = VALORPAGO;
			this._VALORJUROS = VALORJUROS;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._NOTAFISCAL = NOTAFISCAL;
			this._NCHEQUE = NCHEQUE;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUS = IDSTATUS;
			this._DIASATRASO = DIASATRASO;
			this._DATAATJUROS = DATAATJUROS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._COMISSAO = COMISSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDUPLICATARECEBER
		{
			get { return _IDDUPLICATARECEBER; }
			set { _IDDUPLICATARECEBER = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
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

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		#endregion
	}
}
