using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTORESERVAEntity
	{
		private int _IDPRODUTORESERVA;
		private int? _IDRESERVA;
		private int? _QUANT;
		private decimal? _VLUNITARIO;
		private decimal? _VLTOTAL;
		private int? _IDPRODUTO;
		private string _FLAGNOVARESERVA;

		#region Construtores

		//Construtor default
		public PRODUTORESERVAEntity() {
			this._IDRESERVA = null;
			this._QUANT = null;
			this._VLUNITARIO = null;
			this._VLTOTAL = null;
			this._IDPRODUTO = null;
		}

		public PRODUTORESERVAEntity(int IDPRODUTORESERVA, int? IDRESERVA, int? QUANT, decimal? VLUNITARIO, decimal? VLTOTAL, int? IDPRODUTO, string FLAGNOVARESERVA) {

			this._IDPRODUTORESERVA = IDPRODUTORESERVA;
			this._IDRESERVA = IDRESERVA;
			this._QUANT = QUANT;
			this._VLUNITARIO = VLUNITARIO;
			this._VLTOTAL = VLTOTAL;
			this._IDPRODUTO = IDPRODUTO;
			this._FLAGNOVARESERVA = FLAGNOVARESERVA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTORESERVA
		{
			get { return _IDPRODUTORESERVA; }
			set { _IDPRODUTORESERVA = value; }
		}

		public int? IDRESERVA
		{
			get { return _IDRESERVA; }
			set { _IDRESERVA = value; }
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

		public string FLAGNOVARESERVA
		{
			get { return _FLAGNOVARESERVA; }
			set { _FLAGNOVARESERVA = value; }
		}

		#endregion
	}
}
