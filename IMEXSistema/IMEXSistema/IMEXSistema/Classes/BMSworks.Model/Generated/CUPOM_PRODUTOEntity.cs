using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CUPOM_PRODUTOEntity
	{
		private int _CODIGO_EMPRESA;
		private int _CODIGO_CUPOM;
		private int _CUPOM_ITEM;
		private int _CODIGO_PRODUTO;
		private string _REFERENCIA_PRODUTO;
		private string _NOME_PRODUTO;
		private decimal? _QTDADE_PRODUTO;
		private decimal? _VALOR_UNITARIO;
		private decimal? _VALOR_TOTAL;
		private string _UNIDADE_PRODUTO;
		private string _CSTICMS_PRODUTO;
		private decimal? _ALIQUOTAICMS_PRODUTO;
		private string _CFOP_PRODUTO;
		private decimal? _BASEICMS_PRODUTO;
		private decimal? _VALORICMS_PRODUTO;
		private decimal? _ALIQUOTASUBST_PRODUTO;
		private decimal? _VALORSUBST_PRODUTO;
		private decimal? _BASESUBST_PRODUTO;
		private decimal? _ALIQUOTARED_PRODUTO;

		#region Construtores

		//Construtor default
		public CUPOM_PRODUTOEntity() {
			this._QTDADE_PRODUTO = null;
			this._VALOR_UNITARIO = null;
			this._VALOR_TOTAL = null;
			this._ALIQUOTAICMS_PRODUTO = null;
			this._BASEICMS_PRODUTO = null;
			this._VALORICMS_PRODUTO = null;
			this._ALIQUOTASUBST_PRODUTO = null;
			this._VALORSUBST_PRODUTO = null;
			this._BASESUBST_PRODUTO = null;
			this._ALIQUOTARED_PRODUTO = null;
		}

		public CUPOM_PRODUTOEntity(int CODIGO_EMPRESA, int CODIGO_CUPOM, int CUPOM_ITEM, int CODIGO_PRODUTO, string REFERENCIA_PRODUTO, string NOME_PRODUTO, decimal? QTDADE_PRODUTO, decimal? VALOR_UNITARIO, decimal? VALOR_TOTAL, string UNIDADE_PRODUTO, string CSTICMS_PRODUTO, decimal? ALIQUOTAICMS_PRODUTO, string CFOP_PRODUTO, decimal? BASEICMS_PRODUTO, decimal? VALORICMS_PRODUTO, decimal? ALIQUOTASUBST_PRODUTO, decimal? VALORSUBST_PRODUTO, decimal? BASESUBST_PRODUTO, decimal? ALIQUOTARED_PRODUTO) {

			this._CODIGO_EMPRESA = CODIGO_EMPRESA;
			this._CODIGO_CUPOM = CODIGO_CUPOM;
			this._CUPOM_ITEM = CUPOM_ITEM;
			this._CODIGO_PRODUTO = CODIGO_PRODUTO;
			this._REFERENCIA_PRODUTO = REFERENCIA_PRODUTO;
			this._NOME_PRODUTO = NOME_PRODUTO;
			this._QTDADE_PRODUTO = QTDADE_PRODUTO;
			this._VALOR_UNITARIO = VALOR_UNITARIO;
			this._VALOR_TOTAL = VALOR_TOTAL;
			this._UNIDADE_PRODUTO = UNIDADE_PRODUTO;
			this._CSTICMS_PRODUTO = CSTICMS_PRODUTO;
			this._ALIQUOTAICMS_PRODUTO = ALIQUOTAICMS_PRODUTO;
			this._CFOP_PRODUTO = CFOP_PRODUTO;
			this._BASEICMS_PRODUTO = BASEICMS_PRODUTO;
			this._VALORICMS_PRODUTO = VALORICMS_PRODUTO;
			this._ALIQUOTASUBST_PRODUTO = ALIQUOTASUBST_PRODUTO;
			this._VALORSUBST_PRODUTO = VALORSUBST_PRODUTO;
			this._BASESUBST_PRODUTO = BASESUBST_PRODUTO;
			this._ALIQUOTARED_PRODUTO = ALIQUOTARED_PRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int CODIGO_EMPRESA
		{
			get { return _CODIGO_EMPRESA; }
			set { _CODIGO_EMPRESA = value; }
		}

		public int CODIGO_CUPOM
		{
			get { return _CODIGO_CUPOM; }
			set { _CODIGO_CUPOM = value; }
		}

		public int CUPOM_ITEM
		{
			get { return _CUPOM_ITEM; }
			set { _CUPOM_ITEM = value; }
		}

		public int CODIGO_PRODUTO
		{
			get { return _CODIGO_PRODUTO; }
			set { _CODIGO_PRODUTO = value; }
		}

		public string REFERENCIA_PRODUTO
		{
			get { return _REFERENCIA_PRODUTO; }
			set { _REFERENCIA_PRODUTO = value; }
		}

		public string NOME_PRODUTO
		{
			get { return _NOME_PRODUTO; }
			set { _NOME_PRODUTO = value; }
		}

		public decimal? QTDADE_PRODUTO
		{
			get { return _QTDADE_PRODUTO; }
			set { _QTDADE_PRODUTO = value; }
		}

		public decimal? VALOR_UNITARIO
		{
			get { return _VALOR_UNITARIO; }
			set { _VALOR_UNITARIO = value; }
		}

		public decimal? VALOR_TOTAL
		{
			get { return _VALOR_TOTAL; }
			set { _VALOR_TOTAL = value; }
		}

		public string UNIDADE_PRODUTO
		{
			get { return _UNIDADE_PRODUTO; }
			set { _UNIDADE_PRODUTO = value; }
		}

		public string CSTICMS_PRODUTO
		{
			get { return _CSTICMS_PRODUTO; }
			set { _CSTICMS_PRODUTO = value; }
		}

		public decimal? ALIQUOTAICMS_PRODUTO
		{
			get { return _ALIQUOTAICMS_PRODUTO; }
			set { _ALIQUOTAICMS_PRODUTO = value; }
		}

		public string CFOP_PRODUTO
		{
			get { return _CFOP_PRODUTO; }
			set { _CFOP_PRODUTO = value; }
		}

		public decimal? BASEICMS_PRODUTO
		{
			get { return _BASEICMS_PRODUTO; }
			set { _BASEICMS_PRODUTO = value; }
		}

		public decimal? VALORICMS_PRODUTO
		{
			get { return _VALORICMS_PRODUTO; }
			set { _VALORICMS_PRODUTO = value; }
		}

		public decimal? ALIQUOTASUBST_PRODUTO
		{
			get { return _ALIQUOTASUBST_PRODUTO; }
			set { _ALIQUOTASUBST_PRODUTO = value; }
		}

		public decimal? VALORSUBST_PRODUTO
		{
			get { return _VALORSUBST_PRODUTO; }
			set { _VALORSUBST_PRODUTO = value; }
		}

		public decimal? BASESUBST_PRODUTO
		{
			get { return _BASESUBST_PRODUTO; }
			set { _BASESUBST_PRODUTO = value; }
		}

		public decimal? ALIQUOTARED_PRODUTO
		{
			get { return _ALIQUOTARED_PRODUTO; }
			set { _ALIQUOTARED_PRODUTO = value; }
		}

		#endregion
	}
}
