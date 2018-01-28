using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSMANUTEntity
	{
		private int _IDPRODUTOSMANUT;
		private int? _IDMANUTEESQUIPAMENTO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;

		#region Construtores

		//Construtor default
		public PRODUTOSMANUTEntity() {
			this._IDMANUTEESQUIPAMENTO = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
		}

		public PRODUTOSMANUTEntity(int IDPRODUTOSMANUT, int? IDMANUTEESQUIPAMENTO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO) {

			this._IDPRODUTOSMANUT = IDPRODUTOSMANUT;
			this._IDMANUTEESQUIPAMENTO = IDMANUTEESQUIPAMENTO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOSMANUT
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

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		#endregion
	}
}
