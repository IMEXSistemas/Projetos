using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODORCAMENTOEntity
	{
		private int _IDPRODORCAMENTO;
		private int? _IDORCAMENTO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public PRODORCAMENTOEntity() {
			this._IDORCAMENTO = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
		}

		public PRODORCAMENTOEntity(int IDPRODORCAMENTO, int? IDORCAMENTO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL) {

			this._IDPRODORCAMENTO = IDPRODORCAMENTO;
			this._IDORCAMENTO = IDORCAMENTO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODORCAMENTO
		{
			get { return _IDPRODORCAMENTO; }
			set { _IDPRODORCAMENTO = value; }
		}

		public int? IDORCAMENTO
		{
			get { return _IDORCAMENTO; }
			set { _IDORCAMENTO = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? VALORUNITARIO
		{
			get { return _VALORUNITARIO; }
			set { _VALORUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		#endregion
	}
}
