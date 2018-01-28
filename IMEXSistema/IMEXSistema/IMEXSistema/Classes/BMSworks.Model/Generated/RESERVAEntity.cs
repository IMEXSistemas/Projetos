using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class RESERVAEntity
	{
		private int _IDRESERVA;
		private DateTime? _DATARETIRADA;
		private DateTime? _DATAENTREGA;
		private int? _IDCLIENTE;
		private string _OBSERVACAO;
		private int? _IDSTATUS;
		private decimal? _VLTOTAL;
		private DateTime? _DATAEMISSAO;
		private decimal? _VLPAGO;

		#region Construtores

		//Construtor default
		public RESERVAEntity() {
			this._DATARETIRADA = null;
			this._DATAENTREGA = null;
			this._IDCLIENTE = null;
			this._IDSTATUS = null;
			this._VLTOTAL = null;
			this._DATAEMISSAO = null;
			this._VLPAGO = null;
		}

		public RESERVAEntity(int IDRESERVA, DateTime? DATARETIRADA, DateTime? DATAENTREGA, int? IDCLIENTE, string OBSERVACAO, int? IDSTATUS, decimal? VLTOTAL, DateTime? DATAEMISSAO, decimal? VLPAGO) {

			this._IDRESERVA = IDRESERVA;
			this._DATARETIRADA = DATARETIRADA;
			this._DATAENTREGA = DATAENTREGA;
			this._IDCLIENTE = IDCLIENTE;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUS = IDSTATUS;
			this._VLTOTAL = VLTOTAL;
			this._DATAEMISSAO = DATAEMISSAO;
			this._VLPAGO = VLPAGO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDRESERVA
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
