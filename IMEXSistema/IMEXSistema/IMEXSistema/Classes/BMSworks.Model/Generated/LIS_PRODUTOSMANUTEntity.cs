using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSMANUTEntity
	{
		private int? _IDPRODUTOSMANUT;
		private int? _IDMANUTEESQUIPAMENTO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEPRODUTO;
		private DateTime? _DATAMANUT;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSMANUTEntity() { }

		public LIS_PRODUTOSMANUTEntity(int? IDPRODUTOSMANUT, int? IDMANUTEESQUIPAMENTO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEPRODUTO, DateTime? DATAMANUT)		{

			this._IDPRODUTOSMANUT = IDPRODUTOSMANUT;
			this._IDMANUTEESQUIPAMENTO = IDMANUTEESQUIPAMENTO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._DATAMANUT = DATAMANUT;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOSMANUT
		{
			get { return _IDPRODUTOSMANUT; }
			set { _IDPRODUTOSMANUT = value; }
		}

		public int? IDMANUTEESQUIPAMENTO
		{
			get { return _IDMANUTEESQUIPAMENTO; }
			set { _IDMANUTEESQUIPAMENTO = value; }
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

		public decimal? COMISSAOPEDIDO
		{
			get { return _COMISSAOPEDIDO; }
			set { _COMISSAOPEDIDO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public DateTime? DATAMANUT
		{
			get { return _DATAMANUT; }
			set { _DATAMANUT = value; }
		}

		#endregion
	}
}
