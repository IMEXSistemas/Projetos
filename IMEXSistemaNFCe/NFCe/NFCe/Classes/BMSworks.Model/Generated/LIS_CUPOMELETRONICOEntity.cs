using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CUPOMELETRONICOEntity
	{
		private int? _CUPOMELETRONICOID;
		private int? _NUMERONFCE;
		private string _SERIE;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _CPF;
		private string _CNPJ;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTSAIDA;
		private string _HORASAIDA;
		private int? _IDCFOP;
		private decimal? _TOTALNOTA;
		private int? _IDVENDEDOR;
		private string _NOMEVENDEDOR;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private decimal? _VALORTROCO;
		private string _CHAVEACESSO;
		private string _OBSERVACAO;
		private int? _IDSTATUSNFCE;
		private string _NOMESTATUS;
		private int? _IDTIPOPAGAMENTO;
		private string _NOMETIPOPAGAMENTO;
		private int? _IDLOCALCOBRANCA;
		private string _NOMELOCALCOBRANCA;
		private int? _IDFORMAPAGTO;
		private string _NOMEFORMAPAGTO;
		private decimal? _VALORFINAL;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private string _FLAGCONTINGENCIA;
		private string _AMBIENTE;
		private string _PROTOCOLO;
		private string _PROTOCOLOCANCEL;
		private int? _CODOPERADORACARTAO;
		private string _NOMEOPERADORACARTAO;
		private string _NUMEROAUTORIZACARTAO;
		private decimal? _VALOPAGODINHEIRO;
		private decimal? _VALOPAGOCARTAOCREDITO;
		private decimal? _VALOPAGOVALEREFEICAO;
		private decimal? _VALORPAGOCHEQUE;
		private decimal? _VALOPAGOCARTAODEBITO;
		private decimal? _VALOPAGOOUTROS;

		#region Construtores

		//Construtor default
		public LIS_CUPOMELETRONICOEntity() { }

		public LIS_CUPOMELETRONICOEntity(int? CUPOMELETRONICOID, int? NUMERONFCE, string SERIE, int? IDCLIENTE, string NOMECLIENTE, string CPF, string CNPJ, DateTime? DTEMISSAO, DateTime? DTSAIDA, string HORASAIDA, int? IDCFOP, decimal? TOTALNOTA, int? IDVENDEDOR, string NOMEVENDEDOR, decimal? VALORPAGO, decimal? VALORDEVEDOR, decimal? VALORTROCO, string CHAVEACESSO, string OBSERVACAO, int? IDSTATUSNFCE, string NOMESTATUS, int? IDTIPOPAGAMENTO, string NOMETIPOPAGAMENTO, int? IDLOCALCOBRANCA, string NOMELOCALCOBRANCA, int? IDFORMAPAGTO, string NOMEFORMAPAGTO, decimal? VALORFINAL, decimal? PORCDESCONTO, decimal? VALORDESCONTO, string FLAGCONTINGENCIA, string AMBIENTE, string PROTOCOLO, string PROTOCOLOCANCEL, int? CODOPERADORACARTAO, string NOMEOPERADORACARTAO, string NUMEROAUTORIZACARTAO, decimal? VALOPAGODINHEIRO, decimal? VALOPAGOCARTAOCREDITO, decimal? VALOPAGOVALEREFEICAO, decimal? VALORPAGOCHEQUE, decimal? VALOPAGOCARTAODEBITO, decimal? VALOPAGOOUTROS)		{

			this._CUPOMELETRONICOID = CUPOMELETRONICOID;
			this._NUMERONFCE = NUMERONFCE;
			this._SERIE = SERIE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._CPF = CPF;
			this._CNPJ = CNPJ;
			this._DTEMISSAO = DTEMISSAO;
			this._DTSAIDA = DTSAIDA;
			this._HORASAIDA = HORASAIDA;
			this._IDCFOP = IDCFOP;
			this._TOTALNOTA = TOTALNOTA;
			this._IDVENDEDOR = IDVENDEDOR;
			this._NOMEVENDEDOR = NOMEVENDEDOR;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._VALORTROCO = VALORTROCO;
			this._CHAVEACESSO = CHAVEACESSO;
			this._OBSERVACAO = OBSERVACAO;
			this._IDSTATUSNFCE = IDSTATUSNFCE;
			this._NOMESTATUS = NOMESTATUS;
			this._IDTIPOPAGAMENTO = IDTIPOPAGAMENTO;
			this._NOMETIPOPAGAMENTO = NOMETIPOPAGAMENTO;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._NOMELOCALCOBRANCA = NOMELOCALCOBRANCA;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._VALORFINAL = VALORFINAL;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._FLAGCONTINGENCIA = FLAGCONTINGENCIA;
			this._AMBIENTE = AMBIENTE;
			this._PROTOCOLO = PROTOCOLO;
			this._PROTOCOLOCANCEL = PROTOCOLOCANCEL;
			this._CODOPERADORACARTAO = CODOPERADORACARTAO;
			this._NOMEOPERADORACARTAO = NOMEOPERADORACARTAO;
			this._NUMEROAUTORIZACARTAO = NUMEROAUTORIZACARTAO;
			this._VALOPAGODINHEIRO = VALOPAGODINHEIRO;
			this._VALOPAGOCARTAOCREDITO = VALOPAGOCARTAOCREDITO;
			this._VALOPAGOVALEREFEICAO = VALOPAGOVALEREFEICAO;
			this._VALORPAGOCHEQUE = VALORPAGOCHEQUE;
			this._VALOPAGOCARTAODEBITO = VALOPAGOCARTAODEBITO;
			this._VALOPAGOOUTROS = VALOPAGOOUTROS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? CUPOMELETRONICOID
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

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public string CPF
		{
			get { return _CPF; }
			set { _CPF = value; }
		}

		public string CNPJ
		{
			get { return _CNPJ; }
			set { _CNPJ = value; }
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

		public string NOMEVENDEDOR
		{
			get { return _NOMEVENDEDOR; }
			set { _NOMEVENDEDOR = value; }
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

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public int? IDTIPOPAGAMENTO
		{
			get { return _IDTIPOPAGAMENTO; }
			set { _IDTIPOPAGAMENTO = value; }
		}

		public string NOMETIPOPAGAMENTO
		{
			get { return _NOMETIPOPAGAMENTO; }
			set { _NOMETIPOPAGAMENTO = value; }
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

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
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

		public string FLAGCONTINGENCIA
		{
			get { return _FLAGCONTINGENCIA; }
			set { _FLAGCONTINGENCIA = value; }
		}

		public string AMBIENTE
		{
			get { return _AMBIENTE; }
			set { _AMBIENTE = value; }
		}

		public string PROTOCOLO
		{
			get { return _PROTOCOLO; }
			set { _PROTOCOLO = value; }
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

		public decimal? VALOPAGODINHEIRO
		{
			get { return _VALOPAGODINHEIRO; }
			set { _VALOPAGODINHEIRO = value; }
		}

		public decimal? VALOPAGOCARTAOCREDITO
		{
			get { return _VALOPAGOCARTAOCREDITO; }
			set { _VALOPAGOCARTAOCREDITO = value; }
		}

		public decimal? VALOPAGOVALEREFEICAO
		{
			get { return _VALOPAGOVALEREFEICAO; }
			set { _VALOPAGOVALEREFEICAO = value; }
		}

		public decimal? VALORPAGOCHEQUE
		{
			get { return _VALORPAGOCHEQUE; }
			set { _VALORPAGOCHEQUE = value; }
		}

		public decimal? VALOPAGOCARTAODEBITO
		{
			get { return _VALOPAGOCARTAODEBITO; }
			set { _VALOPAGOCARTAODEBITO = value; }
		}

		public decimal? VALOPAGOOUTROS
		{
			get { return _VALOPAGOOUTROS; }
			set { _VALOPAGOOUTROS = value; }
		}

		#endregion
	}
}
