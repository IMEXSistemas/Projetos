using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSFESTASEntity
	{
		private int _IDPRODFESTA;
		private int? _IDITENSFESTAS;
		private int? _IDPRODUTO;
		private decimal? _VALOR;
		private decimal? _QUANTIDADE;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public PRODUTOSFESTASEntity() {
			this._IDITENSFESTAS = null;
			this._IDPRODUTO = null;
			this._VALOR = null;
			this._QUANTIDADE = null;
			this._VALORTOTAL = null;
		}

		public PRODUTOSFESTASEntity(int IDPRODFESTA, int? IDITENSFESTAS, int? IDPRODUTO, decimal? VALOR, decimal? QUANTIDADE, decimal? VALORTOTAL) {

			this._IDPRODFESTA = IDPRODFESTA;
			this._IDITENSFESTAS = IDITENSFESTAS;
			this._IDPRODUTO = IDPRODUTO;
			this._VALOR = VALOR;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODFESTA
		{
			get { return _IDPRODFESTA; }
			set { _IDPRODFESTA = value; }
		}

		public int? IDITENSFESTAS
		{
			get { return _IDITENSFESTAS; }
			set { _IDITENSFESTAS = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		#endregion
	}
}
