using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MATCOTACAOFORNECEDOREntity
	{
		private int? _IDMATCOTACAOFORNECEDOR;
		private int? _IDMATERIAL;
		private int? _IDFORNECEDOR;
		private string _TELEFONEFORNECEDOR;
		private string _PRAZOENTREGA;
		private string _CONTATOFORNECEDOR;
		private decimal? _VALORCOMPRA;
		private string _CONDPAGTO;
		private string _NOMEMATERIAL;
		private string _NOMEFORNECEDOR;
		private DateTime? _DATACOTACAO;

		#region Construtores

		//Construtor default
		public LIS_MATCOTACAOFORNECEDOREntity() { }

		public LIS_MATCOTACAOFORNECEDOREntity(int? IDMATCOTACAOFORNECEDOR, int? IDMATERIAL, int? IDFORNECEDOR, string TELEFONEFORNECEDOR, string PRAZOENTREGA, string CONTATOFORNECEDOR, decimal? VALORCOMPRA, string CONDPAGTO, string NOMEMATERIAL, string NOMEFORNECEDOR, DateTime? DATACOTACAO)		{

			this._IDMATCOTACAOFORNECEDOR = IDMATCOTACAOFORNECEDOR;
			this._IDMATERIAL = IDMATERIAL;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._TELEFONEFORNECEDOR = TELEFONEFORNECEDOR;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._CONTATOFORNECEDOR = CONTATOFORNECEDOR;
			this._VALORCOMPRA = VALORCOMPRA;
			this._CONDPAGTO = CONDPAGTO;
			this._NOMEMATERIAL = NOMEMATERIAL;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._DATACOTACAO = DATACOTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMATCOTACAOFORNECEDOR
		{
			get { return _IDMATCOTACAOFORNECEDOR; }
			set { _IDMATCOTACAOFORNECEDOR = value; }
		}

		public int? IDMATERIAL
		{
			get { return _IDMATERIAL; }
			set { _IDMATERIAL = value; }
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

		public string NOMEMATERIAL
		{
			get { return _NOMEMATERIAL; }
			set { _NOMEMATERIAL = value; }
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
