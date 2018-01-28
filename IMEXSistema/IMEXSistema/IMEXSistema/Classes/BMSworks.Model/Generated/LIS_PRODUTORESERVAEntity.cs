using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTORESERVAEntity
	{
		private int? _IDPRODUTORESERVA;
		private int? _IDRESERVA;
		private DateTime? _DATARETIRADA;
		private DateTime? _DATAENTREGA;
		private int? _QUANT;
		private decimal? _VLUNITARIO;
		private decimal? _VLTOTAL;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private string _FLAGNOVARESERVA;
        private int? _IDCLIENTE;
		

		#region Construtores

		//Construtor default
		public LIS_PRODUTORESERVAEntity() { }

		public LIS_PRODUTORESERVAEntity(int? IDPRODUTORESERVA, int? IDRESERVA, DateTime? DATARETIRADA, DateTime? DATAENTREGA, int? QUANT, decimal? VLUNITARIO, decimal? VLTOTAL, int? IDPRODUTO, string NOMEPRODUTO, string FLAGNOVARESERVA, int? IDCLIENTE)		{

			this._IDPRODUTORESERVA = IDPRODUTORESERVA;
			this._IDRESERVA = IDRESERVA;
			this._DATARETIRADA = DATARETIRADA;
			this._DATAENTREGA = DATAENTREGA;
			this._QUANT = QUANT;
			this._VLUNITARIO = VLUNITARIO;
			this._VLTOTAL = VLTOTAL;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._FLAGNOVARESERVA = FLAGNOVARESERVA;
            this._IDCLIENTE = IDCLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTORESERVA
		{
			get { return _IDPRODUTORESERVA; }
			set { _IDPRODUTORESERVA = value; }
		}

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

		public int? QUANT
		{
			get { return _QUANT; }
			set { _QUANT = value; }
		}

		public decimal? VLUNITARIO
		{
			get { return _VLUNITARIO; }
			set { _VLUNITARIO = value; }
		}

		public decimal? VLTOTAL
		{
			get { return _VLTOTAL; }
			set { _VLTOTAL = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public string FLAGNOVARESERVA
		{
			get { return _FLAGNOVARESERVA; }
			set { _FLAGNOVARESERVA = value; }
		}

        public int? IDCLIENTE
        {
            get { return _IDCLIENTE; }
            set { _IDCLIENTE = value; }
        }

		#endregion
	}
}
