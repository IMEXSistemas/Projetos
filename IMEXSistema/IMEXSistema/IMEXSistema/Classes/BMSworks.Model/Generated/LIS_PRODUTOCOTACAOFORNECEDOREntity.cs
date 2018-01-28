using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOCOTACAOFORNECEDOREntity
	{
		private int? _IDPRODUTOCOTACAOFORNECEDOR;
		private int? _IDPRODUTO;
		private int? _IDFORNECEDOR;
		private string _TELEFONEFORNECEDOR;
		private string _PRAZOENTREGA;
		private string _CONTATOFORNECEDOR;
		private decimal? _VALORCOMPRA;
		private string _CONDPAGTO;
		private string _NOMEPRODUTO;
		private string _NOMEFORNECEDOR;
		private DateTime? _DATACOTACAO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOCOTACAOFORNECEDOREntity() { }

		public LIS_PRODUTOCOTACAOFORNECEDOREntity(int? IDPRODUTOCOTACAOFORNECEDOR, int? IDPRODUTO, int? IDFORNECEDOR, string TELEFONEFORNECEDOR, string PRAZOENTREGA, string CONTATOFORNECEDOR, decimal? VALORCOMPRA, string CONDPAGTO, string NOMEPRODUTO, string NOMEFORNECEDOR, DateTime? DATACOTACAO)		{

			this._IDPRODUTOCOTACAOFORNECEDOR = IDPRODUTOCOTACAOFORNECEDOR;
			this._IDPRODUTO = IDPRODUTO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._TELEFONEFORNECEDOR = TELEFONEFORNECEDOR;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._CONTATOFORNECEDOR = CONTATOFORNECEDOR;
			this._VALORCOMPRA = VALORCOMPRA;
			this._CONDPAGTO = CONDPAGTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._DATACOTACAO = DATACOTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOCOTACAOFORNECEDOR
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

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public string NOMEFORNECEDOR
		{
			get { return _NOMEFORNECEDOR; }
			set { _NOMEFORNECEDOR = value; }
		}

		public DateTime? DATACOTACAO
		{
			get { return _DATACOTACAO; }
			set { _DATACOTACAO = value; }
		}

		#endregion
	}
}
