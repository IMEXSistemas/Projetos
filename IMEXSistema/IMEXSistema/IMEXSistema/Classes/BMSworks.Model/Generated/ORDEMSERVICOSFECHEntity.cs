using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ORDEMSERVICOSFECHEntity
	{
		private int _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private decimal? _VALORORCAMENTO;
		private int? _PRAZOENTREGA;
		private int? _IDSTATUS;
		private int? _IDFUNCIONARIO;
		private string _OBSERVACAO;
		private decimal? _TOTALITEMSERVICO;
		private decimal? _TOTALITEMPECA;
		private decimal? _MAOOBRA;
		private decimal? _OUTROVALOR;
		private decimal? _TOTALFECHOS;
		private DateTime? _GARANTIAVECTO;
		private int? _IDFORMAPAGAMENTO;
		private int? _IDCLIENTE;
		private string _CONTATO;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private string _FLAGORCAMENTO;
		private decimal? _PORCCOMISSAO;
		private decimal? _VLCOMISSAO;
		private string _FLAGTELABLOQUEADA;
		private string _PLACA;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private string _PROBLEMAINFORMADO;
		private string _PROBLEMACONSTATADO;
		private string _SERVICOEXECUTADO;
		private string _EQUIPAMENTO;
		private string _MODELO;
		private string _MARCA;
		private string _ACESSORIOS;

		#region Construtores

		//Construtor default
		public ORDEMSERVICOSFECHEntity() {
			this._DATAEMISSAO = null;
			this._VALORORCAMENTO = null;
			this._PRAZOENTREGA = null;
			this._IDSTATUS = null;
			this._IDFUNCIONARIO = null;
			this._TOTALITEMSERVICO = null;
			this._TOTALITEMPECA = null;
			this._MAOOBRA = null;
			this._OUTROVALOR = null;
			this._TOTALFECHOS = null;
			this._GARANTIAVECTO = null;
			this._IDFORMAPAGAMENTO = null;
			this._IDCLIENTE = null;
			this._VALORPAGO = null;
			this._VALORDEVEDOR = null;
			this._PORCCOMISSAO = null;
			this._VLCOMISSAO = null;
			this._PORCDESCONTO = null;
			this._VALORDESCONTO = null;
		}

		public ORDEMSERVICOSFECHEntity(int IDORDEMSERVICO, DateTime? DATAEMISSAO, decimal? VALORORCAMENTO, int? PRAZOENTREGA, int? IDSTATUS, int? IDFUNCIONARIO, string OBSERVACAO, decimal? TOTALITEMSERVICO, decimal? TOTALITEMPECA, decimal? MAOOBRA, decimal? OUTROVALOR, decimal? TOTALFECHOS, DateTime? GARANTIAVECTO, int? IDFORMAPAGAMENTO, int? IDCLIENTE, string CONTATO, decimal? VALORPAGO, decimal? VALORDEVEDOR, string FLAGORCAMENTO, decimal? PORCCOMISSAO, decimal? VLCOMISSAO, string FLAGTELABLOQUEADA, string PLACA, decimal? PORCDESCONTO, decimal? VALORDESCONTO, string PROBLEMAINFORMADO, string PROBLEMACONSTATADO, string SERVICOEXECUTADO, string EQUIPAMENTO, string MODELO, string MARCA, string ACESSORIOS) {

			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._VALORORCAMENTO = VALORORCAMENTO;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDSTATUS = IDSTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALITEMSERVICO = TOTALITEMSERVICO;
			this._TOTALITEMPECA = TOTALITEMPECA;
			this._MAOOBRA = MAOOBRA;
			this._OUTROVALOR = OUTROVALOR;
			this._TOTALFECHOS = TOTALFECHOS;
			this._GARANTIAVECTO = GARANTIAVECTO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._IDCLIENTE = IDCLIENTE;
			this._CONTATO = CONTATO;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._PORCCOMISSAO = PORCCOMISSAO;
			this._VLCOMISSAO = VLCOMISSAO;
			this._FLAGTELABLOQUEADA = FLAGTELABLOQUEADA;
			this._PLACA = PLACA;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._PROBLEMAINFORMADO = PROBLEMAINFORMADO;
			this._PROBLEMACONSTATADO = PROBLEMACONSTATADO;
			this._SERVICOEXECUTADO = SERVICOEXECUTADO;
			this._EQUIPAMENTO = EQUIPAMENTO;
			this._MODELO = MODELO;
			this._MARCA = MARCA;
			this._ACESSORIOS = ACESSORIOS;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public decimal? VALORORCAMENTO
		{
			get { return _VALORORCAMENTO; }
			set { _VALORORCAMENTO = value; }
		}

		public int? PRAZOENTREGA
		{
			get { return _PRAZOENTREGA; }
			set { _PRAZOENTREGA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALITEMSERVICO
		{
			get { return _TOTALITEMSERVICO; }
			set { _TOTALITEMSERVICO = value; }
		}

		public decimal? TOTALITEMPECA
		{
			get { return _TOTALITEMPECA; }
			set { _TOTALITEMPECA = value; }
		}

		public decimal? MAOOBRA
		{
			get { return _MAOOBRA; }
			set { _MAOOBRA = value; }
		}

		public decimal? OUTROVALOR
		{
			get { return _OUTROVALOR; }
			set { _OUTROVALOR = value; }
		}

		public decimal? TOTALFECHOS
		{
			get { return _TOTALFECHOS; }
			set { _TOTALFECHOS = value; }
		}

		public DateTime? GARANTIAVECTO
		{
			get { return _GARANTIAVECTO; }
			set { _GARANTIAVECTO = value; }
		}

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string CONTATO
		{
			get { return _CONTATO; }
			set { _CONTATO = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public decimal? PORCCOMISSAO
		{
			get { return _PORCCOMISSAO; }
			set { _PORCCOMISSAO = value; }
		}

		public decimal? VLCOMISSAO
		{
			get { return _VLCOMISSAO; }
			set { _VLCOMISSAO = value; }
		}

		public string FLAGTELABLOQUEADA
		{
			get { return _FLAGTELABLOQUEADA; }
			set { _FLAGTELABLOQUEADA = value; }
		}

		public string PLACA
		{
			get { return _PLACA; }
			set { _PLACA = value; }
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

		public string PROBLEMAINFORMADO
		{
			get { return _PROBLEMAINFORMADO; }
			set { _PROBLEMAINFORMADO = value; }
		}

		public string PROBLEMACONSTATADO
		{
			get { return _PROBLEMACONSTATADO; }
			set { _PROBLEMACONSTATADO = value; }
		}

		public string SERVICOEXECUTADO
		{
			get { return _SERVICOEXECUTADO; }
			set { _SERVICOEXECUTADO = value; }
		}

		public string EQUIPAMENTO
		{
			get { return _EQUIPAMENTO; }
			set { _EQUIPAMENTO = value; }
		}

		public string MODELO
		{
			get { return _MODELO; }
			set { _MODELO = value; }
		}

		public string MARCA
		{
			get { return _MARCA; }
			set { _MARCA = value; }
		}

		public string ACESSORIOS
		{
			get { return _ACESSORIOS; }
			set { _ACESSORIOS = value; }
		}

		#endregion
	}
}
