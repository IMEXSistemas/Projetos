using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PEDIDOFESTAEntity
	{
		private int _IDPEDIDOFESTA;
		private int? _IDCLIENTE;
		private DateTime? _DTEMISSAO;
		private int? _IDSTATUS;
		private int? _IDTRANSPORTES;
		private int? _IDVENDEDOR;
		private decimal? _VALORCOMISSAO;
		private string _OBSERVACAO;
		private decimal? _TOTALPRODUTOS;
		private decimal? _TOTALIMPOSTOS;
		private decimal? _PORCDESCONTOS;
		private decimal? _VALORDESCONTOS;
		private decimal? _PORCACRESCIMO;
		private decimal? _VALORACRESCIMO;
		private decimal? _TOTALPEDIDO;
		private int? _IDFORMAPAGAMENTO;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _IDLOCALCOBRANCA;
		private int? _IDCENTROSCUSTOS;
		private string _HOMENAGEADO;
		private int? _IDTEMA;
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
		private string _FLAGORCAMENTO;

		#region Construtores

		//Construtor default
		public PEDIDOFESTAEntity() {
			this._IDCLIENTE = null;
			this._DTEMISSAO = null;
			this._IDSTATUS = null;
			this._IDTRANSPORTES = null;
			this._IDVENDEDOR = null;
			this._VALORCOMISSAO = null;
			this._TOTALPRODUTOS = null;
			this._TOTALIMPOSTOS = null;
			this._PORCDESCONTOS = null;
			this._VALORDESCONTOS = null;
			this._PORCACRESCIMO = null;
			this._VALORACRESCIMO = null;
			this._TOTALPEDIDO = null;
			this._IDFORMAPAGAMENTO = null;
			this._VALORPAGO = null;
			this._VALORDEVEDOR = null;
			this._IDLOCALCOBRANCA = null;
			this._IDCENTROSCUSTOS = null;
			this._IDTEMA = null;
			this._DATAFESTA = null;
			this._ADULTOS = null;
			this._CRIANCAS = null;
			this._IDTIPOFESTA = null;
		}

		public PEDIDOFESTAEntity(int IDPEDIDOFESTA, int? IDCLIENTE, DateTime? DTEMISSAO, int? IDSTATUS, int? IDTRANSPORTES, int? IDVENDEDOR, decimal? VALORCOMISSAO, string OBSERVACAO, decimal? TOTALPRODUTOS, decimal? TOTALIMPOSTOS, decimal? PORCDESCONTOS, decimal? VALORDESCONTOS, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO, decimal? TOTALPEDIDO, int? IDFORMAPAGAMENTO, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? IDLOCALCOBRANCA, int? IDCENTROSCUSTOS, string HOMENAGEADO, int? IDTEMA, string COR, DateTime? DATAFESTA, string HORAINICIO, string HORAFIM, int? ADULTOS, int? CRIANCAS, string NOMESALAO, string TELEFONESALAO, string ENDSALAO, string BAIRROSALAO, string CIDADESALAO, string UF, string CONTATOSALAO, int? IDTIPOFESTA, string FLAGORCAMENTO) {

			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDCLIENTE = IDCLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._IDSTATUS = IDSTATUS;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._IDVENDEDOR = IDVENDEDOR;
			this._VALORCOMISSAO = VALORCOMISSAO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALPRODUTOS = TOTALPRODUTOS;
			this._TOTALIMPOSTOS = TOTALIMPOSTOS;
			this._PORCDESCONTOS = PORCDESCONTOS;
			this._VALORDESCONTOS = VALORDESCONTOS;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._VALORACRESCIMO = VALORACRESCIMO;
			this._TOTALPEDIDO = TOTALPEDIDO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._IDCENTROSCUSTOS = IDCENTROSCUSTOS;
			this._HOMENAGEADO = HOMENAGEADO;
			this._IDTEMA = IDTEMA;
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
			this._FLAGORCAMENTO = FLAGORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
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

		public decimal? PORCDESCONTOS
		{
			get { return _PORCDESCONTOS; }
			set { _PORCDESCONTOS = value; }
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

		public int? IDCENTROSCUSTOS
		{
			get { return _IDCENTROSCUSTOS; }
			set { _IDCENTROSCUSTOS = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		#endregion
	}
}
