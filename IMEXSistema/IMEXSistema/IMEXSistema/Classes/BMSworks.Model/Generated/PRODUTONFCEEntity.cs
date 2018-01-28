using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTONFCEEntity
	{
		private int _PRODUTONFCEID;
		private int? _CUPOMELETRONICOID;
		private int? _IDPRODUTO;
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
		private int? _IDCFOP;
		private decimal? _ALIQPIS;
		private decimal? _VALORPIS;
		private decimal? _ALIQCOFINS;
		private decimal? _VALORCOFINS;
		private decimal? _VLBASEST;
		private decimal? _VLICMSST;
		private decimal? _VLALIQST;
		private decimal? _VLOUTROS;
		private decimal? _VLTRIBUTOAPROX;
		private int? _ITEM;

		#region Construtores

		//Construtor default
		public PRODUTONFCEEntity() {
			this._CUPOMELETRONICOID = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._ALICMS = null;
			this._BASEICMS = null;
			this._REDICMS = null;
			this._VALORICMS = null;
			this._ALIPI = null;
			this._VALORIPI = null;
			this._IDUNIDADE = null;
			this._IDCFOP = null;
			this._ALIQPIS = null;
			this._VALORPIS = null;
			this._ALIQCOFINS = null;
			this._VALORCOFINS = null;
			this._VLBASEST = null;
			this._VLICMSST = null;
			this._VLALIQST = null;
			this._VLOUTROS = null;
			this._VLTRIBUTOAPROX = null;
			this._ITEM = null;
		}

		public PRODUTONFCEEntity(int PRODUTONFCEID, int? CUPOMELETRONICOID, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? ALICMS, decimal? BASEICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDUNIDADE, int? IDCFOP, decimal? ALIQPIS, decimal? VALORPIS, decimal? ALIQCOFINS, decimal? VALORCOFINS, decimal? VLBASEST, decimal? VLICMSST, decimal? VLALIQST, decimal? VLOUTROS, decimal? VLTRIBUTOAPROX, int? ITEM) {

			this._PRODUTONFCEID = PRODUTONFCEID;
			this._CUPOMELETRONICOID = CUPOMELETRONICOID;
			this._IDPRODUTO = IDPRODUTO;
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
			this._IDCFOP = IDCFOP;
			this._ALIQPIS = ALIQPIS;
			this._VALORPIS = VALORPIS;
			this._ALIQCOFINS = ALIQCOFINS;
			this._VALORCOFINS = VALORCOFINS;
			this._VLBASEST = VLBASEST;
			this._VLICMSST = VLICMSST;
			this._VLALIQST = VLALIQST;
			this._VLOUTROS = VLOUTROS;
			this._VLTRIBUTOAPROX = VLTRIBUTOAPROX;
			this._ITEM = ITEM;
		}
		#endregion

		#region Propriedades Get/Set

		public int PRODUTONFCEID
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

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
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

		public int? ITEM
		{
			get { return _ITEM; }
			set { _ITEM = value; }
		}

		#endregion
	}
}
