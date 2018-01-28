using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTONFEntity
	{
		private int? _IDPRODUTONF;
		private int? _IDNOTAFISCAL;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
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
		private string _NOMEUNIDADE;

		#region Construtores

		//Construtor default
		public LIS_PRODUTONFEntity() { }

		public LIS_PRODUTONFEntity(int? IDPRODUTONF, int? IDNOTAFISCAL, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? ALICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDCLASSFISCAL, int? IDCST, decimal? BASEICMS, int? IDUNIDADE, string NOMEUNIDADE)		{

			this._IDPRODUTONF = IDPRODUTONF;
			this._IDNOTAFISCAL = IDNOTAFISCAL;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
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
			this._NOMEUNIDADE = NOMEUNIDADE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTONF
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

		public string NOMEUNIDADE
		{
			get { return _NOMEUNIDADE; }
			set { _NOMEUNIDADE = value; }
		}

		#endregion
	}
}
