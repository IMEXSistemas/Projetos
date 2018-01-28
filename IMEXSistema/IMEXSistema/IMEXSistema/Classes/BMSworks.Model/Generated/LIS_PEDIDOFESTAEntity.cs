using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PEDIDOFESTAEntity
	{
		private int? _IDPEDIDOFESTA;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private DateTime? _DTEMISSAO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDTRANSPORTES;
		private string _NOMETRANSPORTES;
		private int? _IDVENDEDOR;
		private string _NOMEVENDEDOR;
		private decimal? _VALORCOMISSAO;
		private string _OBSERVACAO;
		private decimal? _TOTALPRODUTOS;
		private decimal? _TOTALIMPOSTOS;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTOS;
		private decimal? _PORCACRESCIMO;
		private decimal? _VALORACRESCIMO;
		private decimal? _TOTALPEDIDO;
		private int? _IDFORMAPAGAMENTO;
		private string _NOMEFORMAPAGTO;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _IDLOCALCOBRANCA;
		private string _NOMELOCALCOBRANCA;
		private int? _IDCENTROSCUSTOS;
		private string _DESCCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _HOMENAGEADO;
		private int? _IDTEMA;
		private string _NOMETEMA;
		private string _COR;
		private DateTime? _DATAFESTA;
		private string _HORAINICIO;
		private string _HORAFIM;
		private int? _ADULTOS;
		private int? _CRIANCAS;
		private string _NOMESALAO;
		private string _TELEFONESALAO;
		private string _ENDSALAO;
		private string _BAIRROSALAO;
		private string _CIDADESALAO;
		private string _UF;
		private string _CONTATOSALAO;
		private int? _IDTIPOFESTA;
		private string _NOMEFESTA;
		private string _FLAGORCAMENTO;

		#region Construtores

		//Construtor default
		public LIS_PEDIDOFESTAEntity() { }

		public LIS_PEDIDOFESTAEntity(int? IDPEDIDOFESTA, int? IDCLIENTE, string NOMECLIENTE, DateTime? DTEMISSAO, int? IDSTATUS, string NOMESTATUS, int? IDTRANSPORTES, string NOMETRANSPORTES, int? IDVENDEDOR, string NOMEVENDEDOR, decimal? VALORCOMISSAO, string OBSERVACAO, decimal? TOTALPRODUTOS, decimal? TOTALIMPOSTOS, decimal? PORCDESCONTO, decimal? VALORDESCONTOS, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO, decimal? TOTALPEDIDO, int? IDFORMAPAGAMENTO, string NOMEFORMAPAGTO, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? IDLOCALCOBRANCA, string NOMELOCALCOBRANCA, int? IDCENTROSCUSTOS, string DESCCENTROCUSTO, string CENTROCUSTO, string HOMENAGEADO, int? IDTEMA, string NOMETEMA, string COR, DateTime? DATAFESTA, string HORAINICIO, string HORAFIM, int? ADULTOS, int? CRIANCAS, string NOMESALAO, string TELEFONESALAO, string ENDSALAO, string BAIRROSALAO, string CIDADESALAO, string UF, string CONTATOSALAO, int? IDTIPOFESTA, string NOMEFESTA, string FLAGORCAMENTO)		{

			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._NOMETRANSPORTES = NOMETRANSPORTES;
			this._IDVENDEDOR = IDVENDEDOR;
			this._NOMEVENDEDOR = NOMEVENDEDOR;
			this._VALORCOMISSAO = VALORCOMISSAO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALPRODUTOS = TOTALPRODUTOS;
			this._TOTALIMPOSTOS = TOTALIMPOSTOS;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTOS = VALORDESCONTOS;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._VALORACRESCIMO = VALORACRESCIMO;
			this._TOTALPEDIDO = TOTALPEDIDO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._NOMELOCALCOBRANCA = NOMELOCALCOBRANCA;
			this._IDCENTROSCUSTOS = IDCENTROSCUSTOS;
			this._DESCCENTROCUSTO = DESCCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._HOMENAGEADO = HOMENAGEADO;
			this._IDTEMA = IDTEMA;
			this._NOMETEMA = NOMETEMA;
			this._COR = COR;
			this._DATAFESTA = DATAFESTA;
			this._HORAINICIO = HORAINICIO;
			this._HORAFIM = HORAFIM;
			this._ADULTOS = ADULTOS;
			this._CRIANCAS = CRIANCAS;
			this._NOMESALAO = NOMESALAO;
			this._TELEFONESALAO = TELEFONESALAO;
			this._ENDSALAO = ENDSALAO;
			this._BAIRROSALAO = BAIRROSALAO;
			this._CIDADESALAO = CIDADESALAO;
			this._UF = UF;
			this._CONTATOSALAO = CONTATOSALAO;
			this._IDTIPOFESTA = IDTIPOFESTA;
			this._NOMEFESTA = NOMEFESTA;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
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

		public decimal? VALORDESCONTOS
		{
			get { return _VALORDESCONTOS; }
			set { _VALORDESCONTOS = value; }
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

		public int? IDCENTROSCUSTOS
		{
			get { return _IDCENTROSCUSTOS; }
			set { _IDCENTROSCUSTOS = value; }
		}

		public string DESCCENTROCUSTO
		{
			get { return _DESCCENTROCUSTO; }
			set { _DESCCENTROCUSTO = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string HOMENAGEADO
		{
			get { return _HOMENAGEADO; }
			set { _HOMENAGEADO = value; }
		}

		public int? IDTEMA
		{
			get { return _IDTEMA; }
			set { _IDTEMA = value; }
		}

		public string NOMETEMA
		{
			get { return _NOMETEMA; }
			set { _NOMETEMA = value; }
		}

		public string COR
		{
			get { return _COR; }
			set { _COR = value; }
		}

		public DateTime? DATAFESTA
		{
			get { return _DATAFESTA; }
			set { _DATAFESTA = value; }
		}

		public string HORAINICIO
		{
			get { return _HORAINICIO; }
			set { _HORAINICIO = value; }
		}

		public string HORAFIM
		{
			get { return _HORAFIM; }
			set { _HORAFIM = value; }
		}

		public int? ADULTOS
		{
			get { return _ADULTOS; }
			set { _ADULTOS = value; }
		}

		public int? CRIANCAS
		{
			get { return _CRIANCAS; }
			set { _CRIANCAS = value; }
		}

		public string NOMESALAO
		{
			get { return _NOMESALAO; }
			set { _NOMESALAO = value; }
		}

		public string TELEFONESALAO
		{
			get { return _TELEFONESALAO; }
			set { _TELEFONESALAO = value; }
		}

		public string ENDSALAO
		{
			get { return _ENDSALAO; }
			set { _ENDSALAO = value; }
		}

		public string BAIRROSALAO
		{
			get { return _BAIRROSALAO; }
			set { _BAIRROSALAO = value; }
		}

		public string CIDADESALAO
		{
			get { return _CIDADESALAO; }
			set { _CIDADESALAO = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public string CONTATOSALAO
		{
			get { return _CONTATOSALAO; }
			set { _CONTATOSALAO = value; }
		}

		public int? IDTIPOFESTA
		{
			get { return _IDTIPOFESTA; }
			set { _IDTIPOFESTA = value; }
		}

		public string NOMEFESTA
		{
			get { return _NOMEFESTA; }
			set { _NOMEFESTA = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		#endregion
	}
}
