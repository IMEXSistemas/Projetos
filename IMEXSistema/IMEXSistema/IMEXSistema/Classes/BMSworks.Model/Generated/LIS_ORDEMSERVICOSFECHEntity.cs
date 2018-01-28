using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ORDEMSERVICOSFECHEntity
	{
		private int? _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private decimal? _VALORORCAMENTO;
		private int? _PRAZOENTREGA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _OBSERVACAO;
		private decimal? _TOTALITEMSERVICO;
		private decimal? _TOTALITEMPECA;
		private decimal? _MAOOBRA;
		private decimal? _OUTROVALOR;
		private decimal? _TOTALFECHOS;
		private DateTime? _GARANTIAVECTO;
		private int? _IDFORMAPAGAMENTO;
		private string _NOMEFORMAPAGTO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _CONTATO;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private string _FLAGORCAMENTO;
		private decimal? _PORCCOMISSAO;
		private decimal? _VLCOMISSAO;
		private string _PLACA;
		private string _PROBLEMAINFORMADO;
		private string _PROBLEMACONSTATADO;
		private string _SERVICOEXECUTADO;
        private string _EQUIPAMENTO;
		private string _MODELO;
		private string _MARCA;
		private string _ACESSORIOS;

		#region Construtores

		//Construtor default
		public LIS_ORDEMSERVICOSFECHEntity() { }

        public LIS_ORDEMSERVICOSFECHEntity(int? IDORDEMSERVICO, DateTime? DATAEMISSAO, decimal? VALORORCAMENTO, int? PRAZOENTREGA, int? IDSTATUS, string NOMESTATUS, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string OBSERVACAO, decimal? TOTALITEMSERVICO, decimal? TOTALITEMPECA, decimal? MAOOBRA, decimal? OUTROVALOR, decimal? TOTALFECHOS, DateTime? GARANTIAVECTO, int? IDFORMAPAGAMENTO, string NOMEFORMAPAGTO, int? IDCLIENTE, string NOMECLIENTE, string CONTATO, decimal? VALORPAGO, decimal? VALORDEVEDOR, string FLAGORCAMENTO, decimal? PORCCOMISSAO, decimal? VLCOMISSAO, string PLACA, string PROBLEMAINFORMADO, string PROBLEMACONSTATADO, string SERVICOEXECUTADO, string MODELO, string MARCA, string ACESSORIOS, string EQUIPAMENTO)
        {

			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._VALORORCAMENTO = VALORORCAMENTO;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALITEMSERVICO = TOTALITEMSERVICO;
			this._TOTALITEMPECA = TOTALITEMPECA;
			this._MAOOBRA = MAOOBRA;
			this._OUTROVALOR = OUTROVALOR;
			this._TOTALFECHOS = TOTALFECHOS;
			this._GARANTIAVECTO = GARANTIAVECTO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._CONTATO = CONTATO;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._PORCCOMISSAO = PORCCOMISSAO;
			this._VLCOMISSAO = VLCOMISSAO;
			this._PLACA = PLACA;
			this._PROBLEMAINFORMADO = PROBLEMAINFORMADO;
			this._PROBLEMACONSTATADO = PROBLEMACONSTATADO;
			this._SERVICOEXECUTADO = SERVICOEXECUTADO;
			this._MODELO = MODELO;
			this._MARCA = MARCA;
			this._ACESSORIOS = ACESSORIOS;
            this._EQUIPAMENTO = EQUIPAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDORDEMSERVICO
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

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
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

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
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

		public string PLACA
		{
			get { return _PLACA; }
			set { _PLACA = value; }
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

        public string EQUIPAMENTO
		{
            get { return _EQUIPAMENTO; }
            set { _EQUIPAMENTO = value; }
		}

        

		#endregion
	}
}
