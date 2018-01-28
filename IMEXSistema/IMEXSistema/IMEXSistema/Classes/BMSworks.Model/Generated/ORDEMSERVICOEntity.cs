using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ORDEMSERVICOEntity
	{
		private int _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private int? _IDCLIENTE;
		private string _CONTATO;
		private int? _IDSTATUS;
		private int? _IDFUNCIONARIO;
		private int? _IDTIPOATENDIMENTO;
		private string _NUMSERIE;
		private int? _IDEQUIPAMENTO;
		private int? _IDMARCA;
		private string _MODELO;
		private DateTime? _VECTOGARANTIA;
		private DateTime? _DATACOMPRA;
		private string _NOTAFISCAL;
		private string _PROBLEMA;
		private string _OBSERVACAO;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public ORDEMSERVICOEntity() {
			this._DATAEMISSAO = null;
			this._IDCLIENTE = null;
			this._IDSTATUS = null;
			this._IDFUNCIONARIO = null;
			this._IDTIPOATENDIMENTO = null;
			this._IDEQUIPAMENTO = null;
			this._IDMARCA = null;
			this._VECTOGARANTIA = null;
			this._DATACOMPRA = null;
		}

		public ORDEMSERVICOEntity(int IDORDEMSERVICO, DateTime? DATAEMISSAO, int? IDCLIENTE, string CONTATO, int? IDSTATUS, int? IDFUNCIONARIO, int? IDTIPOATENDIMENTO, string NUMSERIE, int? IDEQUIPAMENTO, int? IDMARCA, string MODELO, DateTime? VECTOGARANTIA, DateTime? DATACOMPRA, string NOTAFISCAL, string PROBLEMA, string OBSERVACAO, string DESCRICAO) {

			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._CONTATO = CONTATO;
			this._IDSTATUS = IDSTATUS;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDTIPOATENDIMENTO = IDTIPOATENDIMENTO;
			this._NUMSERIE = NUMSERIE;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._IDMARCA = IDMARCA;
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

		public int? IDTIPOATENDIMENTO
		{
			get { return _IDTIPOATENDIMENTO; }
			set { _IDTIPOATENDIMENTO = value; }
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

		public int? IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
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
