using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PEDIDOMARCEntity
	{
		private int _IDPEDIDOMARC;
		private int? _IDCLIENTE;
		private DateTime? _DTEMISSAO;
		private int? _IDSTATUS;
		private string _PRAZOENTREGA;
		private int? _IDTRANSPORTES;
		private int? _IDVENDEDOR;
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
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _IDLOCALCOBRANCA;
		private int? _IDCENTROCUSTOS;
		private decimal? _TOTALMATERIAL;
		private string _FLAGORCAMENTO;

		#region Construtores

		//Construtor default
		public PEDIDOMARCEntity() {
			this._IDCLIENTE = null;
			this._DTEMISSAO = null;
			this._IDSTATUS = null;
			this._IDTRANSPORTES = null;
			this._IDVENDEDOR = null;
			this._VALORCOMISSAO = null;
			this._TOTALPRODUTOS = null;
			this._TOTALIMPOSTOS = null;
			this._PORCDESCONTO = null;
			this._VALORDESCONTO = null;
			this._PORCACRESCIMO = null;
			this._VALORACRESCIMO = null;
			this._TOTALPEDIDO = null;
			this._IDFORMAPAGAMENTO = null;
			this._VALORPAGO = null;
			this._VALORDEVEDOR = null;
			this._IDLOCALCOBRANCA = null;
			this._IDCENTROCUSTOS = null;
			this._TOTALMATERIAL = null;
		}

		public PEDIDOMARCEntity(int IDPEDIDOMARC, int? IDCLIENTE, DateTime? DTEMISSAO, int? IDSTATUS, string PRAZOENTREGA, int? IDTRANSPORTES, int? IDVENDEDOR, decimal? VALORCOMISSAO, string OBSERVACAO, decimal? TOTALPRODUTOS, decimal? TOTALIMPOSTOS, decimal? PORCDESCONTO, decimal? VALORDESCONTO, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO, decimal? TOTALPEDIDO, int? IDFORMAPAGAMENTO, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? IDLOCALCOBRANCA, int? IDCENTROCUSTOS, decimal? TOTALMATERIAL, string FLAGORCAMENTO) {

			this._IDPEDIDOMARC = IDPEDIDOMARC;
			this._IDCLIENTE = IDCLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._IDSTATUS = IDSTATUS;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._IDVENDEDOR = IDVENDEDOR;
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
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._IDCENTROCUSTOS = IDCENTROCUSTOS;
			this._TOTALMATERIAL = TOTALMATERIAL;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPEDIDOMARC
		{
			get { return _IDPEDIDOMARC; }
			set { _IDPEDIDOMARC = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
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

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
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

		public int? IDCENTROCUSTOS
		{
			get { return _IDCENTROCUSTOS; }
			set { _IDCENTROCUSTOS = value; }
		}

		public decimal? TOTALMATERIAL
		{
			get { return _TOTALMATERIAL; }
			set { _TOTALMATERIAL = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		#endregion
	}
}
