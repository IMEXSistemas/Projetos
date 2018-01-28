using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDOTICAEntity
	{
		private int _IDPRODPEDOTICA;
		private int? _IDPEDIDOOTICA;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private decimal? _PORCDECONTO;
		private decimal? _VLUNITLIQUIDO;

		#region Construtores

		//Construtor default
		public PRODUTOSPEDOTICAEntity() {
			this._IDPEDIDOOTICA = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._PORCDECONTO = null;
			this._VLUNITLIQUIDO = null;
		}

		public PRODUTOSPEDOTICAEntity(int IDPRODPEDOTICA, int? IDPEDIDOOTICA, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? PORCDECONTO, decimal? VLUNITLIQUIDO) {

			this._IDPRODPEDOTICA = IDPRODPEDOTICA;
			this._IDPEDIDOOTICA = IDPEDIDOOTICA;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._PORCDECONTO = PORCDECONTO;
			this._VLUNITLIQUIDO = VLUNITLIQUIDO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODPEDOTICA
		{
			get { return _IDPRODPEDOTICA; }
			set { _IDPRODPEDOTICA = value; }
		}

		public int? IDPEDIDOOTICA
		{
			get { return _IDPEDIDOOTICA; }
			set { _IDPEDIDOOTICA = value; }
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

		public decimal? PORCDECONTO
		{
			get { return _PORCDECONTO; }
			set { _PORCDECONTO = value; }
		}

		public decimal? VLUNITLIQUIDO
		{
			get { return _VLUNITLIQUIDO; }
			set { _VLUNITLIQUIDO = value; }
		}

		#endregion
	}
}
