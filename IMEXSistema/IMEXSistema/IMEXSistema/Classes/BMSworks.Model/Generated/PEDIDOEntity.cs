using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PEDIDOEntity
	{
		private int _IDPEDIDO;
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
		private string _FLAGPRODIMPRESSAO;
		private string _PRODUTOFINAL;
		private string _FLAGORCAMENTO;
		private string _NREFERENCIA;
		private string _FLAGVLMETRO;
		private string _OBSANEXO;
		private DateTime? _DATAENTREGA;
		private string _FLAGTELABLOQUEADA;
		private decimal? _TIPOPAGTODINHEIRO;
		private decimal? _TIPOPAGTOCHEQUE;
		private decimal? _TIPOPAGTOCARTAODEBITO;
		private decimal? _TIPOPAGTOCARTAOCREDITO;
		private DateTime? _DATAVECTO;
		private int? _IDSUPERVISOR;
        private int? _IDMESA;
       

        #region Construtores

        //Construtor default
        public PEDIDOEntity() {
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
			this._DATAENTREGA = null;
			this._TIPOPAGTODINHEIRO = null;
			this._TIPOPAGTOCHEQUE = null;
			this._TIPOPAGTOCARTAODEBITO = null;
			this._TIPOPAGTOCARTAOCREDITO = null;
			this._DATAVECTO = null;
			this._IDSUPERVISOR = null;
		}

		public PEDIDOEntity(int IDPEDIDO, int? IDCLIENTE, DateTime? DTEMISSAO, int? IDSTATUS, string PRAZOENTREGA, 
                            int? IDTRANSPORTES, int? IDVENDEDOR, decimal? VALORCOMISSAO, string OBSERVACAO, decimal? TOTALPRODUTOS,
                            decimal? TOTALIMPOSTOS, decimal? PORCDESCONTO, decimal? VALORDESCONTO, decimal? PORCACRESCIMO, 
                            decimal? VALORACRESCIMO, decimal? TOTALPEDIDO, int? IDFORMAPAGAMENTO, decimal? VALORPAGO, 
                            decimal? VALORDEVEDOR, int? IDLOCALCOBRANCA, int? IDCENTROCUSTOS, string FLAGPRODIMPRESSAO, 
                            string PRODUTOFINAL, string FLAGORCAMENTO, string NREFERENCIA, string FLAGVLMETRO, 
                            string OBSANEXO, DateTime? DATAENTREGA, string FLAGTELABLOQUEADA, decimal? TIPOPAGTODINHEIRO,
                            decimal? TIPOPAGTOCHEQUE, decimal? TIPOPAGTOCARTAODEBITO, decimal? TIPOPAGTOCARTAOCREDITO, 
                            DateTime? DATAVECTO, int? IDSUPERVISOR, int? IDMESA) {

			this._IDPEDIDO = IDPEDIDO;
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
			this._FLAGPRODIMPRESSAO = FLAGPRODIMPRESSAO;
			this._PRODUTOFINAL = PRODUTOFINAL;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._NREFERENCIA = NREFERENCIA;
			this._FLAGVLMETRO = FLAGVLMETRO;
			this._OBSANEXO = OBSANEXO;
			this._DATAENTREGA = DATAENTREGA;
			this._FLAGTELABLOQUEADA = FLAGTELABLOQUEADA;
			this._TIPOPAGTODINHEIRO = TIPOPAGTODINHEIRO;
			this._TIPOPAGTOCHEQUE = TIPOPAGTOCHEQUE;
			this._TIPOPAGTOCARTAODEBITO = TIPOPAGTOCARTAODEBITO;
			this._TIPOPAGTOCARTAOCREDITO = TIPOPAGTOCARTAOCREDITO;
			this._DATAVECTO = DATAVECTO;
			this._IDSUPERVISOR = IDSUPERVISOR;
            this._IDMESA = IDMESA;
           
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
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

		public string FLAGPRODIMPRESSAO
		{
			get { return _FLAGPRODIMPRESSAO; }
			set { _FLAGPRODIMPRESSAO = value; }
		}

		public string PRODUTOFINAL
		{
			get { return _PRODUTOFINAL; }
			set { _PRODUTOFINAL = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public string NREFERENCIA
		{
			get { return _NREFERENCIA; }
			set { _NREFERENCIA = value; }
		}

		public string FLAGVLMETRO
		{
			get { return _FLAGVLMETRO; }
			set { _FLAGVLMETRO = value; }
		}

		public string OBSANEXO
		{
			get { return _OBSANEXO; }
			set { _OBSANEXO = value; }
		}

		public DateTime? DATAENTREGA
		{
			get { return _DATAENTREGA; }
			set { _DATAENTREGA = value; }
		}

		public string FLAGTELABLOQUEADA
		{
			get { return _FLAGTELABLOQUEADA; }
			set { _FLAGTELABLOQUEADA = value; }
		}

		public decimal? TIPOPAGTODINHEIRO
		{
			get { return _TIPOPAGTODINHEIRO; }
			set { _TIPOPAGTODINHEIRO = value; }
		}

		public decimal? TIPOPAGTOCHEQUE
		{
			get { return _TIPOPAGTOCHEQUE; }
			set { _TIPOPAGTOCHEQUE = value; }
		}

		public decimal? TIPOPAGTOCARTAODEBITO
		{
			get { return _TIPOPAGTOCARTAODEBITO; }
			set { _TIPOPAGTOCARTAODEBITO = value; }
		}

		public decimal? TIPOPAGTOCARTAOCREDITO
		{
			get { return _TIPOPAGTOCARTAOCREDITO; }
			set { _TIPOPAGTOCARTAOCREDITO = value; }
		}

		public DateTime? DATAVECTO
		{
			get { return _DATAVECTO; }
			set { _DATAVECTO = value; }
		}

		public int? IDSUPERVISOR
		{
			get { return _IDSUPERVISOR; }
			set { _IDSUPERVISOR = value; }
		}


        public int? IDMESA
        {
            get { return _IDMESA; }
            set { _IDMESA = value; }
        }
       

        #endregion
    }
}
