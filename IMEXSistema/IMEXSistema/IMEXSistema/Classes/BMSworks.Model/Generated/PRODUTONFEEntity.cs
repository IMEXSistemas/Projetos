using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTONFEEntity
	{
		private int _IDPRODUTONFE;
		private int? _IDNOTAFISCALE;
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
		private int? _IDCFOP;
		private string _CSTPISCOFINS;
		private decimal? _ALIQPIS;
		private decimal? _ALIQCOFINS;
		private decimal? _VALORPIS;
		private decimal? _VALORCOFINS;
		private decimal? _PORCDESCONTO;
		private decimal? _DESCONTOPRODUTO;
		private string _DESCPRODUTO2;
		private decimal? _VLFRETE;
		private decimal? _VLTRIBUTOAPROX;
		private decimal? _VLBASEST;
		private decimal? _VLICMSST;
		private decimal? _VLOUTROS;

		#region Construtores

		//Construtor default
		public PRODUTONFEEntity() {
			this._IDNOTAFISCALE = null;
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
			this._IDCFOP = null;
			this._ALIQPIS = null;
			this._ALIQCOFINS = null;
			this._VALORPIS = null;
			this._VALORCOFINS = null;
			this._PORCDESCONTO = null;
			this._DESCONTOPRODUTO = null;
			this._VLFRETE = null;
			this._VLTRIBUTOAPROX = null;
			this._VLBASEST = null;
			this._VLICMSST = null;
			this._VLOUTROS = null;
		}

		public PRODUTONFEEntity(int IDPRODUTONFE, int? IDNOTAFISCALE, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? ALICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDCLASSFISCAL, int? IDCST, decimal? BASEICMS, int? IDUNIDADE, int? IDCFOP, string CSTPISCOFINS, decimal? ALIQPIS, decimal? ALIQCOFINS, decimal? VALORPIS, decimal? VALORCOFINS, decimal? PORCDESCONTO, decimal? DESCONTOPRODUTO, string DESCPRODUTO2, decimal? VLFRETE, decimal? VLTRIBUTOAPROX, decimal? VLBASEST, decimal? VLICMSST, decimal? VLOUTROS) {

			this._IDPRODUTONFE = IDPRODUTONFE;
			this._IDNOTAFISCALE = IDNOTAFISCALE;
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
			this._IDCFOP = IDCFOP;
			this._CSTPISCOFINS = CSTPISCOFINS;
			this._ALIQPIS = ALIQPIS;
			this._ALIQCOFINS = ALIQCOFINS;
			this._VALORPIS = VALORPIS;
			this._VALORCOFINS = VALORCOFINS;
			this._PORCDESCONTO = PORCDESCONTO;
			this._DESCONTOPRODUTO = DESCONTOPRODUTO;
			this._DESCPRODUTO2 = DESCPRODUTO2;
			this._VLFRETE = VLFRETE;
			this._VLTRIBUTOAPROX = VLTRIBUTOAPROX;
			this._VLBASEST = VLBASEST;
			this._VLICMSST = VLICMSST;
			this._VLOUTROS = VLOUTROS;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTONFE
		{
			get { return _IDPRODUTONFE; }
			set { _IDPRODUTONFE = value; }
		}

		public int? IDNOTAFISCALE
		{
			get { return _IDNOTAFISCALE; }
			set { _IDNOTAFISCALE = value; }
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

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string CSTPISCOFINS
		{
			get { return _CSTPISCOFINS; }
			set { _CSTPISCOFINS = value; }
		}

		public decimal? ALIQPIS
		{
			get { return _ALIQPIS; }
			set { _ALIQPIS = value; }
		}

		public decimal? ALIQCOFINS
		{
			get { return _ALIQCOFINS; }
			set { _ALIQCOFINS = value; }
		}

		public decimal? VALORPIS
		{
			get { return _VALORPIS; }
			set { _VALORPIS = value; }
		}

		public decimal? VALORCOFINS
		{
			get { return _VALORCOFINS; }
			set { _VALORCOFINS = value; }
		}

		public decimal? PORCDESCONTO
		{
			get { return _PORCDESCONTO; }
			set { _PORCDESCONTO = value; }
		}

		public decimal? DESCONTOPRODUTO
		{
			get { return _DESCONTOPRODUTO; }
			set { _DESCONTOPRODUTO = value; }
		}

		public string DESCPRODUTO2
		{
			get { return _DESCPRODUTO2; }
			set { _DESCPRODUTO2 = value; }
		}

		public decimal? VLFRETE
		{
			get { return _VLFRETE; }
			set { _VLFRETE = value; }
		}

		public decimal? VLTRIBUTOAPROX
		{
			get { return _VLTRIBUTOAPROX; }
			set { _VLTRIBUTOAPROX = value; }
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

		public decimal? VLOUTROS
		{
			get { return _VLOUTROS; }
			set { _VLOUTROS = value; }
		}

		#endregion
	}
}
