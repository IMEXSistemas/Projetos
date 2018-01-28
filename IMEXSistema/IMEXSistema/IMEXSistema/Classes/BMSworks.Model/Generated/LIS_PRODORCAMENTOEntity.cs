using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODORCAMENTOEntity
	{
		private int? _IDPRODORCAMENTO;
		private int? _IDORCAMENTO;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public LIS_PRODORCAMENTOEntity() { }

		public LIS_PRODORCAMENTOEntity(int? IDPRODORCAMENTO, int? IDORCAMENTO, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL)		{

			this._IDPRODORCAMENTO = IDPRODORCAMENTO;
			this._IDORCAMENTO = IDORCAMENTO;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODORCAMENTO
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

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
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
