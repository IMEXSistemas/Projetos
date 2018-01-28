using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSFESTASEntity
	{
		private int? _IDPRODFESTA;
		private int? _IDITENSFESTAS;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTOS;
		private decimal? _VALOR;
		private decimal? _QUANTIDADE;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSFESTASEntity() { }

		public LIS_PRODUTOSFESTASEntity(int? IDPRODFESTA, int? IDITENSFESTAS, int? IDPRODUTO, string NOMEPRODUTOS, decimal? VALOR, decimal? QUANTIDADE, decimal? VALORTOTAL)		{

			this._IDPRODFESTA = IDPRODFESTA;
			this._IDITENSFESTAS = IDITENSFESTAS;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTOS = NOMEPRODUTOS;
			this._VALOR = VALOR;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODFESTA
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

		public string NOMEPRODUTOS
		{
			get { return _NOMEPRODUTOS; }
			set { _NOMEPRODUTOS = value; }
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
