using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_HELPDESKEntity
	{
		private int? _IDHELPDESK;
		private DateTime? _DATAINCLUSAO;
		private DateTime? _DATASOLUCAO;
		private int? _IDTIPOSOLICITANTE;
		private string _TIPOSOLICITANTE;
		private int? _IDDEPARTAMENTO;
		private string _NOMEDEPARTAMENTO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDPRIORIDADE;
		private string _NOMEPRIORIDADE;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _NOMESOLICITANTE;
		private string _EMAILSOLICITANTE;
		private string _TELEFONESOLICITANTE;
		private string _SOLICITACAO;
		private string _SOLUCAO;

		#region Construtores

		//Construtor default
		public LIS_HELPDESKEntity() { }

		public LIS_HELPDESKEntity(int? IDHELPDESK, DateTime? DATAINCLUSAO, DateTime? DATASOLUCAO, int? IDTIPOSOLICITANTE, string TIPOSOLICITANTE, int? IDDEPARTAMENTO, string NOMEDEPARTAMENTO, int? IDSTATUS, string NOMESTATUS, int? IDPRIORIDADE, string NOMEPRIORIDADE, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string NOMESOLICITANTE, string EMAILSOLICITANTE, string TELEFONESOLICITANTE, string SOLICITACAO, string SOLUCAO)		{

			this._IDHELPDESK = IDHELPDESK;
			this._DATAINCLUSAO = DATAINCLUSAO;
			this._DATASOLUCAO = DATASOLUCAO;
			this._IDTIPOSOLICITANTE = IDTIPOSOLICITANTE;
			this._TIPOSOLICITANTE = TIPOSOLICITANTE;
			this._IDDEPARTAMENTO = IDDEPARTAMENTO;
			this._NOMEDEPARTAMENTO = NOMEDEPARTAMENTO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDPRIORIDADE = IDPRIORIDADE;
			this._NOMEPRIORIDADE = NOMEPRIORIDADE;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._NOMESOLICITANTE = NOMESOLICITANTE;
			this._EMAILSOLICITANTE = EMAILSOLICITANTE;
			this._TELEFONESOLICITANTE = TELEFONESOLICITANTE;
			this._SOLICITACAO = SOLICITACAO;
			this._SOLUCAO = SOLUCAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDHELPDESK
		{
			get { return _IDHELPDESK; }
			set { _IDHELPDESK = value; }
		}

		public DateTime? DATAINCLUSAO
		{
			get { return _DATAINCLUSAO; }
			set { _DATAINCLUSAO = value; }
		}

		public DateTime? DATASOLUCAO
		{
			get { return _DATASOLUCAO; }
			set { _DATASOLUCAO = value; }
		}

		public int? IDTIPOSOLICITANTE
		{
			get { return _IDTIPOSOLICITANTE; }
			set { _IDTIPOSOLICITANTE = value; }
		}

		public string TIPOSOLICITANTE
		{
			get { return _TIPOSOLICITANTE; }
			set { _TIPOSOLICITANTE = value; }
		}

		public int? IDDEPARTAMENTO
		{
			get { return _IDDEPARTAMENTO; }
			set { _IDDEPARTAMENTO = value; }
		}

		public string NOMEDEPARTAMENTO
		{
			get { return _NOMEDEPARTAMENTO; }
			set { _NOMEDEPARTAMENTO = value; }
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

		public int? IDPRIORIDADE
		{
			get { return _IDPRIORIDADE; }
			set { _IDPRIORIDADE = value; }
		}

		public string NOMEPRIORIDADE
		{
			get { return _NOMEPRIORIDADE; }
			set { _NOMEPRIORIDADE = value; }
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

		public string NOMESOLICITANTE
		{
			get { return _NOMESOLICITANTE; }
			set { _NOMESOLICITANTE = value; }
		}

		public string EMAILSOLICITANTE
		{
			get { return _EMAILSOLICITANTE; }
			set { _EMAILSOLICITANTE = value; }
		}

		public string TELEFONESOLICITANTE
		{
			get { return _TELEFONESOLICITANTE; }
			set { _TELEFONESOLICITANTE = value; }
		}

		public string SOLICITACAO
		{
			get { return _SOLICITACAO; }
			set { _SOLICITACAO = value; }
		}

		public string SOLUCAO
		{
			get { return _SOLUCAO; }
			set { _SOLUCAO = value; }
		}

		#endregion
	}
}
