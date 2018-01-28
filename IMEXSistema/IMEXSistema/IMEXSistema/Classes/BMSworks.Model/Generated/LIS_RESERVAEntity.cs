using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_RESERVAEntity
	{
		private int? _IDRESERVA;
		private DateTime? _DATARETIRADA;
		private DateTime? _DATAENTREGA;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _OBSERVACAO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private decimal? _VLTOTAL;
		private DateTime? _DATAEMISSAO;
		private decimal? _VLPAGO;

		#region Construtores

		//Construtor default
		public LIS_RESERVAEntity() { }

		public LIS_RESERVAEntity(int? IDRESERVA, DateTime? DATARETIRADA, DateTime? DATAENTREGA, int? IDCLIENTE, string NOMECLIENTE, string OBSERVACAO, int? IDSTATUS, string NOMESTATUS, decimal? VLTOTAL, DateTime? DATAEMISSAO, decimal? VLPAGO)		{

			this._IDRESERVA = IDRESERVA;
			this._DATARETIRADA = DATARETIRADA;
			this._DATAENTREGA = DATAENTREGA;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._VLTOTAL = VLTOTAL;
			this._DATAEMISSAO = DATAEMISSAO;
			this._VLPAGO = VLPAGO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDRESERVA
		{
			get { return _IDRESERVA; }
			set { _IDRESERVA = value; }
		}

		public DateTime? DATARETIRADA
		{
			get { return _DATARETIRADA; }
			set { _DATARETIRADA = value; }
		}

		public DateTime? DATAENTREGA
		{
			get { return _DATAENTREGA; }
			set { _DATAENTREGA = value; }
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

		public decimal? VLTOTAL
		{
			get { return _VLTOTAL; }
			set { _VLTOTAL = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public decimal? VLPAGO
		{
			get { return _VLPAGO; }
			set { _VLPAGO = value; }
		}

		#endregion
	}
}
