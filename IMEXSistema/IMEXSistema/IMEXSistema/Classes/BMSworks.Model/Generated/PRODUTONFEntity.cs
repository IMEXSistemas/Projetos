using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTONFEntity
	{
		private int _IDPRODUTONF;
		private int? _IDNOTAFISCAL;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private decimal? _ALICMS;
		private decimal? _REDICMS;
		private decimal? _VALORICMS;
		private decimal? _ALIPI;
		private decimal? _VALORIPI;
		private int? _IDCLASSFISCAL;
		private int? _IDCST;
		private decimal? _BASEICMS;
		private int? _IDUNIDADE;

		#region Construtores

		//Construtor default
		public PRODUTONFEntity() {
			this._IDNOTAFISCAL = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._ALICMS = null;
			this._REDICMS = null;
			this._VALORICMS = null;
			this._ALIPI = null;
			this._VALORIPI = null;
			this._IDCLASSFISCAL = null;
			this._IDCST = null;
			this._BASEICMS = null;
			this._IDUNIDADE = null;
		}

		public PRODUTONFEntity(int IDPRODUTONF, int? IDNOTAFISCAL, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? ALICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDCLASSFISCAL, int? IDCST, decimal? BASEICMS, int? IDUNIDADE) {

			this._IDPRODUTONF = IDPRODUTONF;
			this._IDNOTAFISCAL = IDNOTAFISCAL;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._ALICMS = ALICMS;
			this._REDICMS = REDICMS;
			this._VALORICMS = VALORICMS;
			this._ALIPI = ALIPI;
			this._VALORIPI = VALORIPI;
			this._IDCLASSFISCAL = IDCLASSFISCAL;
			this._IDCST = IDCST;
			this._BASEICMS = BASEICMS;
			this._IDUNIDADE = IDUNIDADE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTONF
		{
			get { return _IDPRODUTONF; }
			set { _IDPRODUTONF = value; }
		}

		public int? IDNOTAFISCAL
		{
			get { return _IDNOTAFISCAL; }
			set { _IDNOTAFISCAL = value; }
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

		public decimal? ALICMS
		{
			get { return _ALICMS; }
			set { _ALICMS = value; }
		}

		public decimal? REDICMS
		{
			get { return _REDICMS; }
			set { _REDICMS = value; }
		}

		public decimal? VALORICMS
		{
			get { return _VALORICMS; }
			set { _VALORICMS = value; }
		}

		public decimal? ALIPI
		{
			get { return _ALIPI; }
			set { _ALIPI = value; }
		}

		public decimal? VALORIPI
		{
			get { return _VALORIPI; }
			set { _VALORIPI = value; }
		}

		public int? IDCLASSFISCAL
		{
			get { return _IDCLASSFISCAL; }
			set { _IDCLASSFISCAL = value; }
		}

		public int? IDCST
		{
			get { return _IDCST; }
			set { _IDCST = value; }
		}

		public decimal? BASEICMS
		{
			get { return _BASEICMS; }
			set { _BASEICMS = value; }
		}

		public int? IDUNIDADE
		{
			get { return _IDUNIDADE; }
			set { _IDUNIDADE = value; }
		}

		#endregion
	}
}
