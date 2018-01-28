using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOCOTACAOFORNECEDOREntity
	{
		private int _IDPRODUTOCOTACAOFORNECEDOR;
		private int? _IDPRODUTO;
		private int? _IDFORNECEDOR;
		private string _TELEFONEFORNECEDOR;
		private string _PRAZOENTREGA;
		private string _CONTATOFORNECEDOR;
		private decimal? _VALORCOMPRA;
		private string _CONDPAGTO;
		private DateTime? _DATACOTACAO;

		#region Construtores

		//Construtor default
		public PRODUTOCOTACAOFORNECEDOREntity() {
			this._IDPRODUTO = null;
			this._IDFORNECEDOR = null;
			this._VALORCOMPRA = null;
			this._DATACOTACAO = null;
		}

		public PRODUTOCOTACAOFORNECEDOREntity(int IDPRODUTOCOTACAOFORNECEDOR, int? IDPRODUTO, int? IDFORNECEDOR, string TELEFONEFORNECEDOR, string PRAZOENTREGA, string CONTATOFORNECEDOR, decimal? VALORCOMPRA, string CONDPAGTO, DateTime? DATACOTACAO) {

			this._IDPRODUTOCOTACAOFORNECEDOR = IDPRODUTOCOTACAOFORNECEDOR;
			this._IDPRODUTO = IDPRODUTO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._TELEFONEFORNECEDOR = TELEFONEFORNECEDOR;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._CONTATOFORNECEDOR = CONTATOFORNECEDOR;
			this._VALORCOMPRA = VALORCOMPRA;
			this._CONDPAGTO = CONDPAGTO;
			this._DATACOTACAO = DATACOTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOCOTACAOFORNECEDOR
		{
			get { return _IDPRODUTOCOTACAOFORNECEDOR; }
			set { _IDPRODUTOCOTACAOFORNECEDOR = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string TELEFONEFORNECEDOR
		{
			get { return _TELEFONEFORNECEDOR; }
			set { _TELEFONEFORNECEDOR = value; }
		}

		public string PRAZOENTREGA
		{
			get { return _PRAZOENTREGA; }
			set { _PRAZOENTREGA = value; }
		}

		public string CONTATOFORNECEDOR
		{
			get { return _CONTATOFORNECEDOR; }
			set { _CONTATOFORNECEDOR = value; }
		}

		public decimal? VALORCOMPRA
		{
			get { return _VALORCOMPRA; }
			set { _VALORCOMPRA = value; }
		}

		public string CONDPAGTO
		{
			get { return _CONDPAGTO; }
			set { _CONDPAGTO = value; }
		}

		public DateTime? DATACOTACAO
		{
			get { return _DATACOTACAO; }
			set { _DATACOTACAO = value; }
		}

		#endregion
	}
}
