using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CUPOMELETRONICOEntity
	{
		private int _CUPOMELETRONICOID;
		private int? _NUMERONFCE;
		private string _SERIE;
		private int? _IDCLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTSAIDA;
		private string _HORASAIDA;
		private int? _IDCFOP;
		private decimal? _TOTALNOTA;
		private int? _IDVENDEDOR;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private decimal? _VALORTROCO;
		private string _CHAVEACESSO;
		private string _OBSERVACAO;
		private int? _IDSTATUSNFCE;
		private int? _IDTIPOPAGAMENTO;
		private int? _IDLOCALCOBRANCA;
		private int? _IDFORMAPAGTO;
		private decimal? _VALORFINAL;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private string _FLAGENVIADO;
		private string _FLAGCONTINGENCIA;
		private string _STRQRCODE;
		private string _PROTOCOLO;
		private string _AMBIENTE;
		private string _PROTOCOLOCANCEL;
		private int? _CODOPERADORACARTAO;
		private string _NOMEOPERADORACARTAO;
		private string _NUMEROAUTORIZACARTAO;

		#region Construtores

		//Construtor default
		public CUPOMELETRONICOEntity() {
			this._NUMERONFCE = null;
			this._IDCLIENTE = null;
			this._DTEMISSAO = null;
			this._DTSAIDA = null;
			this._IDCFOP = null;
			this._TOTALNOTA = null;
			this._IDVENDEDOR = null;
			this._VALORPAGO = null;
			this._VALORDEVEDOR = null;
			this._VALORTROCO = null;
			this._IDSTATUSNFCE = null;
			this._IDTIPOPAGAMENTO = null;
			this._IDLOCALCOBRANCA = null;
			this._IDFORMAPAGTO = null;
			this._VALORFINAL = null;
			this._PORCDESCONTO = null;
			this._VALORDESCONTO = null;
			this._CODOPERADORACARTAO = null;
		}

		public CUPOMELETRONICOEntity(int CUPOMELETRONICOID, int? NUMERONFCE, string SERIE, int? IDCLIENTE, DateTime? DTEMISSAO, DateTime? DTSAIDA, string HORASAIDA, int? IDCFOP, decimal? TOTALNOTA, int? IDVENDEDOR, decimal? VALORPAGO, decimal? VALORDEVEDOR, decimal? VALORTROCO, string CHAVEACESSO, string OBSERVACAO, int? IDSTATUSNFCE, int? IDTIPOPAGAMENTO, int? IDLOCALCOBRANCA, int? IDFORMAPAGTO, decimal? VALORFINAL, decimal? PORCDESCONTO, decimal? VALORDESCONTO, string FLAGENVIADO, string FLAGCONTINGENCIA, string STRQRCODE, string PROTOCOLO, string AMBIENTE, string PROTOCOLOCANCEL, int? CODOPERADORACARTAO, string NOMEOPERADORACARTAO, string NUMEROAUTORIZACARTAO) {

			this._CUPOMELETRONICOID = CUPOMELETRONICOID;
			this._NUMERONFCE = NUMERONFCE;
			this._SERIE = SERIE;
			this._IDCLIENTE = IDCLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTSAIDA = DTSAIDA;
			this._HORASAIDA = HORASAIDA;
			this._IDCFOP = IDCFOP;
			this._TOTALNOTA = TOTALNOTA;
			this._IDVENDEDOR = IDVENDEDOR;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._VALORTROCO = VALORTROCO;
			this._CHAVEACESSO = CHAVEACESSO;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUSNFCE = IDSTATUSNFCE;
			this._IDTIPOPAGAMENTO = IDTIPOPAGAMENTO;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._VALORFINAL = VALORFINAL;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._FLAGENVIADO = FLAGENVIADO;
			this._FLAGCONTINGENCIA = FLAGCONTINGENCIA;
			this._STRQRCODE = STRQRCODE;
			this._PROTOCOLO = PROTOCOLO;
			this._AMBIENTE = AMBIENTE;
			this._PROTOCOLOCANCEL = PROTOCOLOCANCEL;
			this._CODOPERADORACARTAO = CODOPERADORACARTAO;
			this._NOMEOPERADORACARTAO = NOMEOPERADORACARTAO;
			this._NUMEROAUTORIZACARTAO = NUMEROAUTORIZACARTAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int CUPOMELETRONICOID
		{
			get { return _CUPOMELETRONICOID; }
			set { _CUPOMELETRONICOID = value; }
		}

		public int? NUMERONFCE
		{
			get { return _NUMERONFCE; }
			set { _NUMERONFCE = value; }
		}

		public string SERIE
		{
			get { return _SERIE; }
			set { _SERIE = value; }
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

		public DateTime? DTSAIDA
		{
			get { return _DTSAIDA; }
			set { _DTSAIDA = value; }
		}

		public string HORASAIDA
		{
			get { return _HORASAIDA; }
			set { _HORASAIDA = value; }
		}

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public decimal? TOTALNOTA
		{
			get { return _TOTALNOTA; }
			set { _TOTALNOTA = value; }
		}

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
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

		public decimal? VALORTROCO
		{
			get { return _VALORTROCO; }
			set { _VALORTROCO = value; }
		}

		public string CHAVEACESSO
		{
			get { return _CHAVEACESSO; }
			set { _CHAVEACESSO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDSTATUSNFCE
		{
			get { return _IDSTATUSNFCE; }
			set { _IDSTATUSNFCE = value; }
		}

		public int? IDTIPOPAGAMENTO
		{
			get { return _IDTIPOPAGAMENTO; }
			set { _IDTIPOPAGAMENTO = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public decimal? VALORFINAL
		{
			get { return _VALORFINAL; }
			set { _VALORFINAL = value; }
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

		public string FLAGENVIADO
		{
			get { return _FLAGENVIADO; }
			set { _FLAGENVIADO = value; }
		}

		public string FLAGCONTINGENCIA
		{
			get { return _FLAGCONTINGENCIA; }
			set { _FLAGCONTINGENCIA = value; }
		}

		public string STRQRCODE
		{
			get { return _STRQRCODE; }
			set { _STRQRCODE = value; }
		}

		public string PROTOCOLO
		{
			get { return _PROTOCOLO; }
			set { _PROTOCOLO = value; }
		}

		public string AMBIENTE
		{
			get { return _AMBIENTE; }
			set { _AMBIENTE = value; }
		}

		public string PROTOCOLOCANCEL
		{
			get { return _PROTOCOLOCANCEL; }
			set { _PROTOCOLOCANCEL = value; }
		}

		public int? CODOPERADORACARTAO
		{
			get { return _CODOPERADORACARTAO; }
			set { _CODOPERADORACARTAO = value; }
		}

		public string NOMEOPERADORACARTAO
		{
			get { return _NOMEOPERADORACARTAO; }
			set { _NOMEOPERADORACARTAO = value; }
		}

		public string NUMEROAUTORIZACARTAO
		{
			get { return _NUMEROAUTORIZACARTAO; }
			set { _NUMEROAUTORIZACARTAO = value; }
		}

		#endregion
	}
}
