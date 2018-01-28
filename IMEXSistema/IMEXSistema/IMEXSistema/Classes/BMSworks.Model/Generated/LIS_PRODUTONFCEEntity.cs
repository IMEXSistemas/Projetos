using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTONFCEEntity
	{
		private int? _PRODUTONFCEID;
		private int? _CUPOMELETRONICOID;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private string _CODBARRA;
		private string _CODPRODUTOFORNECEDOR;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _ALICMS;
		private decimal? _BASEICMS;
		private decimal? _REDICMS;
		private decimal? _VALORICMS;
		private decimal? _ALIPI;
		private decimal? _VALORIPI;
		private int? _IDUNIDADE;
		private string _NOMEUNIDADE;
		private int? _IDCFOP;
		private string _CODCFOP;
		private string _DESCCFOP;
		private decimal? _ALIQPIS;
		private decimal? _VALORPIS;
		private decimal? _ALIQCOFINS;
		private decimal? _VALORCOFINS;
		private decimal? _VLBASEST;
		private decimal? _VLICMSST;
		private decimal? _VLALIQST;
		private decimal? _VLOUTROS;
		private decimal? _VLTRIBUTOAPROX;
		private DateTime? _DTEMISSAO;
		private int? _IDVENDEDOR;
		private int? _IDSTATUSNFCE;
		private int? _ITEM;

		#region Construtores

		//Construtor default
		public LIS_PRODUTONFCEEntity() { }

		public LIS_PRODUTONFCEEntity(int? PRODUTONFCEID, int? CUPOMELETRONICOID, int? IDPRODUTO, string NOMEPRODUTO, string CODBARRA, string CODPRODUTOFORNECEDOR, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? ALICMS, decimal? BASEICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDUNIDADE, string NOMEUNIDADE, int? IDCFOP, string CODCFOP, string DESCCFOP, decimal? ALIQPIS, decimal? VALORPIS, decimal? ALIQCOFINS, decimal? VALORCOFINS, decimal? VLBASEST, decimal? VLICMSST, decimal? VLALIQST, decimal? VLOUTROS, decimal? VLTRIBUTOAPROX, DateTime? DTEMISSAO, int? IDVENDEDOR, int? IDSTATUSNFCE, int? ITEM)		{

			this._PRODUTONFCEID = PRODUTONFCEID;
			this._CUPOMELETRONICOID = CUPOMELETRONICOID;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._CODBARRA = CODBARRA;
			this._CODPRODUTOFORNECEDOR = CODPRODUTOFORNECEDOR;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._ALICMS = ALICMS;
			this._BASEICMS = BASEICMS;
			this._REDICMS = REDICMS;
			this._VALORICMS = VALORICMS;
			this._ALIPI = ALIPI;
			this._VALORIPI = VALORIPI;
			this._IDUNIDADE = IDUNIDADE;
			this._NOMEUNIDADE = NOMEUNIDADE;
			this._IDCFOP = IDCFOP;
			this._CODCFOP = CODCFOP;
			this._DESCCFOP = DESCCFOP;
			this._ALIQPIS = ALIQPIS;
			this._VALORPIS = VALORPIS;
			this._ALIQCOFINS = ALIQCOFINS;
			this._VALORCOFINS = VALORCOFINS;
			this._VLBASEST = VLBASEST;
			this._VLICMSST = VLICMSST;
			this._VLALIQST = VLALIQST;
			this._VLOUTROS = VLOUTROS;
			this._VLTRIBUTOAPROX = VLTRIBUTOAPROX;
			this._DTEMISSAO = DTEMISSAO;
			this._IDVENDEDOR = IDVENDEDOR;
			this._IDSTATUSNFCE = IDSTATUSNFCE;
			this._ITEM = ITEM;
		}
		#endregion

		#region Propriedades Get/Set

		public int? PRODUTONFCEID
		{
			get { return _PRODUTONFCEID; }
			set { _PRODUTONFCEID = value; }
		}

		public int? CUPOMELETRONICOID
		{
			get { return _CUPOMELETRONICOID; }
			set { _CUPOMELETRONICOID = value; }
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

		public string CODBARRA
		{
			get { return _CODBARRA; }
			set { _CODBARRA = value; }
		}

		public string CODPRODUTOFORNECEDOR
		{
			get { return _CODPRODUTOFORNECEDOR; }
			set { _CODPRODUTOFORNECEDOR = value; }
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

		public decimal? ALICMS
		{
			get { return _ALICMS; }
			set { _ALICMS = value; }
		}

		public decimal? BASEICMS
		{
			get { return _BASEICMS; }
			set { _BASEICMS = value; }
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

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string CODCFOP
		{
			get { return _CODCFOP; }
			set { _CODCFOP = value; }
		}

		public string DESCCFOP
		{
			get { return _DESCCFOP; }
			set { _DESCCFOP = value; }
		}

		public decimal? ALIQPIS
		{
			get { return _ALIQPIS; }
			set { _ALIQPIS = value; }
		}

		public decimal? VALORPIS
		{
			get { return _VALORPIS; }
			set { _VALORPIS = value; }
		}

		public decimal? ALIQCOFINS
		{
			get { return _ALIQCOFINS; }
			set { _ALIQCOFINS = value; }
		}

		public decimal? VALORCOFINS
		{
			get { return _VALORCOFINS; }
			set { _VALORCOFINS = value; }
		}

		public decimal? VLBASEST
		{
			get { return _VLBASEST; }
			set { _VLBASEST = value; }
		}

		public decimal? VLICMSST
		{
			get { return _VLICMSST; }
			set { _VLICMSST = value; }
		}

		public decimal? VLALIQST
		{
			get { return _VLALIQST; }
			set { _VLALIQST = value; }
		}

		public decimal? VLOUTROS
		{
			get { return _VLOUTROS; }
			set { _VLOUTROS = value; }
		}

		public decimal? VLTRIBUTOAPROX
		{
			get { return _VLTRIBUTOAPROX; }
			set { _VLTRIBUTOAPROX = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public int? IDSTATUSNFCE
		{
			get { return _IDSTATUSNFCE; }
			set { _IDSTATUSNFCE = value; }
		}

		public int? ITEM
		{
			get { return _ITEM; }
			set { _ITEM = value; }
		}

		#endregion
	}
}
