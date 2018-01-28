using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PEDIDOOTICAEntity
	{
		private int? _IDPEDIDOOTICA;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTENTREGA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private string _PRAZOENTREGA;
		private int? _IDTRANSPORTES;
		private string _NOMETRANSPORTES;
		private int? _IDVENDEDOR;
		private string _NOMEVENDEDOR;
		private decimal? _VALORCOMISSAO;
		private string _OBSERVACAO;
		private decimal? _TOTALPRODUTOS;
		private decimal? _TOTALIMPOSTOS;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private decimal? _PORCACRESCIMO;
		private decimal? _VALORACRESCIMO;
		private decimal? _TOTALPEDIDO;
		private int? _IDFORMAPAGAMENTO;
		private string _NOMEFORMAPAGTO;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _IDLOCALCOBRANCA;
		private string _NOMELOCALCOBRANCA;
		private int? _IDCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _DESCRICAOCENTROCUSTO;
		private int? _IDMEDICO;
		private string _NOMEMEDICO;
		private string _CRMMEDICO;
		private int? _IDFORNECEDOR;
		private string _NOMEFORN;
		private string _FLAGORCAMENTO;

		#region Construtores

		//Construtor default
		public LIS_PEDIDOOTICAEntity() { }

		public LIS_PEDIDOOTICAEntity(int? IDPEDIDOOTICA, int? IDCLIENTE, string NOMECLIENTE, DateTime? DTEMISSAO, DateTime? DTENTREGA, int? IDSTATUS, string NOMESTATUS, string PRAZOENTREGA, int? IDTRANSPORTES, string NOMETRANSPORTES, int? IDVENDEDOR, string NOMEVENDEDOR, decimal? VALORCOMISSAO, string OBSERVACAO, decimal? TOTALPRODUTOS, decimal? TOTALIMPOSTOS, decimal? PORCDESCONTO, decimal? VALORDESCONTO, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO, decimal? TOTALPEDIDO, int? IDFORMAPAGAMENTO, string NOMEFORMAPAGTO, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? IDLOCALCOBRANCA, string NOMELOCALCOBRANCA, int? IDCENTROCUSTO, string CENTROCUSTO, string DESCRICAOCENTROCUSTO, int? IDMEDICO, string NOMEMEDICO, string CRMMEDICO, int? IDFORNECEDOR, string NOMEFORN, string FLAGORCAMENTO)		{

			this._IDPEDIDOOTICA = IDPEDIDOOTICA;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTENTREGA = DTENTREGA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._NOMETRANSPORTES = NOMETRANSPORTES;
			this._IDVENDEDOR = IDVENDEDOR;
			this._NOMEVENDEDOR = NOMEVENDEDOR;
			this._VALORCOMISSAO = VALORCOMISSAO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALPRODUTOS = TOTALPRODUTOS;
			this._TOTALIMPOSTOS = TOTALIMPOSTOS;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._VALORACRESCIMO = VALORACRESCIMO;
			this._TOTALPEDIDO = TOTALPEDIDO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._NOMELOCALCOBRANCA = NOMELOCALCOBRANCA;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DESCRICAOCENTROCUSTO = DESCRICAOCENTROCUSTO;
			this._IDMEDICO = IDMEDICO;
			this._NOMEMEDICO = NOMEMEDICO;
			this._CRMMEDICO = CRMMEDICO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._NOMEFORN = NOMEFORN;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPEDIDOOTICA
		{
			get { return _IDPEDIDOOTICA; }
			set { _IDPEDIDOOTICA = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public DateTime? DTENTREGA
		{
			get { return _DTENTREGA; }
			set { _DTENTREGA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public string PRAZOENTREGA
		{
			get { return _PRAZOENTREGA; }
			set { _PRAZOENTREGA = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
		}

		public string NOMETRANSPORTES
		{
			get { return _NOMETRANSPORTES; }
			set { _NOMETRANSPORTES = value; }
		}

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public string NOMEVENDEDOR
		{
			get { return _NOMEVENDEDOR; }
			set { _NOMEVENDEDOR = value; }
		}

		public decimal? VALORCOMISSAO
		{
			get { return _VALORCOMISSAO; }
			set { _VALORCOMISSAO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALPRODUTOS
		{
			get { return _TOTALPRODUTOS; }
			set { _TOTALPRODUTOS = value; }
		}

		public decimal? TOTALIMPOSTOS
		{
			get { return _TOTALIMPOSTOS; }
			set { _TOTALIMPOSTOS = value; }
		}

		public decimal? PORCDESCONTO
		{
			get { return _PORCDESCONTO; }
			set { _PORCDESCONTO = value; }
		}

		public decimal? VALORDESCONTO
		{
			get { return _VALORDESCONTO; }
			set { _VALORDESCONTO = value; }
		}

		public decimal? PORCACRESCIMO
		{
			get { return _PORCACRESCIMO; }
			set { _PORCACRESCIMO = value; }
		}

		public decimal? VALORACRESCIMO
		{
			get { return _VALORACRESCIMO; }
			set { _VALORACRESCIMO = value; }
		}

		public decimal? TOTALPEDIDO
		{
			get { return _TOTALPEDIDO; }
			set { _TOTALPEDIDO = value; }
		}

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
		}

		public decimal? VALORPAGO
		{
			get { return _VALORPAGO; }
			set { _VALORPAGO = value; }
		}

		public decimal? VALORDEVEDOR
		{
			get { return _VALORDEVEDOR; }
			set { _VALORDEVEDOR = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public string NOMELOCALCOBRANCA
		{
			get { return _NOMELOCALCOBRANCA; }
			set { _NOMELOCALCOBRANCA = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DESCRICAOCENTROCUSTO
		{
			get { return _DESCRICAOCENTROCUSTO; }
			set { _DESCRICAOCENTROCUSTO = value; }
		}

		public int? IDMEDICO
		{
			get { return _IDMEDICO; }
			set { _IDMEDICO = value; }
		}

		public string NOMEMEDICO
		{
			get { return _NOMEMEDICO; }
			set { _NOMEMEDICO = value; }
		}

		public string CRMMEDICO
		{
			get { return _CRMMEDICO; }
			set { _CRMMEDICO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string NOMEFORN
		{
			get { return _NOMEFORN; }
			set { _NOMEFORN = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		#endregion
	}
}
