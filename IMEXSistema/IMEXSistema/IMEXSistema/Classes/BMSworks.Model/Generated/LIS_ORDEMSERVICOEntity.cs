using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ORDEMSERVICOEntity
	{
		private int? _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _CONTATO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private int? _IDTIPOATENDIMENTO;
		private string _NOMEATENDIMENTO;
		private string _NUMSERIE;
		private int? _IDEQUIPAMENTO;
		private string _NOMEEQUIPAMENTO;
		private int? _IDMARCA;
		private string _NOMEMARCA;
		private string _MODELO;
		private DateTime? _VECTOGARANTIA;
		private DateTime? _DATACOMPRA;
		private string _NOTAFISCAL;
		private string _PROBLEMA;
		private string _OBSERVACAO;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public LIS_ORDEMSERVICOEntity() { }

		public LIS_ORDEMSERVICOEntity(int? IDORDEMSERVICO, DateTime? DATAEMISSAO, int? IDCLIENTE, string NOMECLIENTE, string CONTATO, int? IDSTATUS, string NOMESTATUS, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, int? IDTIPOATENDIMENTO, string NOMEATENDIMENTO, string NUMSERIE, int? IDEQUIPAMENTO, string NOMEEQUIPAMENTO, int? IDMARCA, string NOMEMARCA, string MODELO, DateTime? VECTOGARANTIA, DateTime? DATACOMPRA, string NOTAFISCAL, string PROBLEMA, string OBSERVACAO, string DESCRICAO)		{

			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._CONTATO = CONTATO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._IDTIPOATENDIMENTO = IDTIPOATENDIMENTO;
			this._NOMEATENDIMENTO = NOMEATENDIMENTO;
			this._NUMSERIE = NUMSERIE;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._NOMEEQUIPAMENTO = NOMEEQUIPAMENTO;
			this._IDMARCA = IDMARCA;
			this._NOMEMARCA = NOMEMARCA;
			this._MODELO = MODELO;
			this._VECTOGARANTIA = VECTOGARANTIA;
			this._DATACOMPRA = DATACOMPRA;
			this._NOTAFISCAL = NOTAFISCAL;
			this._PROBLEMA = PROBLEMA;
			this._OBSERVACAO = OBSERVACAO;
			this._DESCRICAO = DESCRICAO;
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

		public int? IDTIPOATENDIMENTO
		{
			get { return _IDTIPOATENDIMENTO; }
			set { _IDTIPOATENDIMENTO = value; }
		}

		public string NOMEATENDIMENTO
		{
			get { return _NOMEATENDIMENTO; }
			set { _NOMEATENDIMENTO = value; }
		}

		public string NUMSERIE
		{
			get { return _NUMSERIE; }
			set { _NUMSERIE = value; }
		}

		public int? IDEQUIPAMENTO
		{
			get { return _IDEQUIPAMENTO; }
			set { _IDEQUIPAMENTO = value; }
		}

		public string NOMEEQUIPAMENTO
		{
			get { return _NOMEEQUIPAMENTO; }
			set { _NOMEEQUIPAMENTO = value; }
		}

		public int? IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
		}

		public string NOMEMARCA
		{
			get { return _NOMEMARCA; }
			set { _NOMEMARCA = value; }
		}

		public string MODELO
		{
			get { return _MODELO; }
			set { _MODELO = value; }
		}

		public DateTime? VECTOGARANTIA
		{
			get { return _VECTOGARANTIA; }
			set { _VECTOGARANTIA = value; }
		}

		public DateTime? DATACOMPRA
		{
			get { return _DATACOMPRA; }
			set { _DATACOMPRA = value; }
		}

		public string NOTAFISCAL
		{
			get { return _NOTAFISCAL; }
			set { _NOTAFISCAL = value; }
		}

		public string PROBLEMA
		{
			get { return _PROBLEMA; }
			set { _PROBLEMA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}
