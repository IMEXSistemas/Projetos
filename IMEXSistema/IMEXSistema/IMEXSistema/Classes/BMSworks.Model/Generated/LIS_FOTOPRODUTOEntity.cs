using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_FOTOPRODUTOEntity
	{
		private int? _IDFOTO;
		private string _NOMEFOTO;
		private byte[] _FOTO;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _VALORVENDA1;
		private string _CODPRODUTOFORNECEDOR;

		#region Construtores

		//Construtor default
		public LIS_FOTOPRODUTOEntity() { }

		public LIS_FOTOPRODUTOEntity(int? IDFOTO, string NOMEFOTO, byte[] FOTO, int? IDPRODUTO, string NOMEPRODUTO, decimal? VALORVENDA1, string CODPRODUTOFORNECEDOR)		{

			this._IDFOTO = IDFOTO;
			this._NOMEFOTO = NOMEFOTO;
			this._FOTO = FOTO;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._VALORVENDA1 = VALORVENDA1;
			this._CODPRODUTOFORNECEDOR = CODPRODUTOFORNECEDOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDFOTO
		{
			get { return _IDFOTO; }
			set { _IDFOTO = value; }
		}

		public string NOMEFOTO
		{
			get { return _NOMEFOTO; }
			set { _NOMEFOTO = value; }
		}

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
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

		public decimal? VALORVENDA1
		{
			get { return _VALORVENDA1; }
			set { _VALORVENDA1 = value; }
		}

		public string CODPRODUTOFORNECEDOR
		{
			get { return _CODPRODUTOFORNECEDOR; }
			set { _CODPRODUTOFORNECEDOR = value; }
		}

		#endregion
	}
}
