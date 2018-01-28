using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class HELPDESKEntity
	{
		private int _IDHELPDESK;
		private DateTime? _DATAINCLUSAO;
		private DateTime? _DATASOLUCAO;
		private int? _IDTIPOSOLICITANTE;
		private int? _IDDEPARTAMENTO;
		private int? _IDSTATUS;
		private int? _IDPRIORIDADE;
		private int? _IDFUNCIONARIO;
		private string _NOMESOLICITANTE;
		private string _EMAILSOLICITANTE;
		private string _TELEFONESOLICITANTE;
		private string _SOLICITACAO;
		private string _SOLUCAO;

		#region Construtores

		//Construtor default
		public HELPDESKEntity() {
			this._DATAINCLUSAO = null;
			this._DATASOLUCAO = null;
			this._IDTIPOSOLICITANTE = null;
			this._IDDEPARTAMENTO = null;
			this._IDSTATUS = null;
			this._IDPRIORIDADE = null;
			this._IDFUNCIONARIO = null;
		}

		public HELPDESKEntity(int IDHELPDESK, DateTime? DATAINCLUSAO, DateTime? DATASOLUCAO, int? IDTIPOSOLICITANTE, int? IDDEPARTAMENTO, int? IDSTATUS, int? IDPRIORIDADE, int? IDFUNCIONARIO, string NOMESOLICITANTE, string EMAILSOLICITANTE, string TELEFONESOLICITANTE, string SOLICITACAO, string SOLUCAO) {

			this._IDHELPDESK = IDHELPDESK;
			this._DATAINCLUSAO = DATAINCLUSAO;
			this._DATASOLUCAO = DATASOLUCAO;
			this._IDTIPOSOLICITANTE = IDTIPOSOLICITANTE;
			this._IDDEPARTAMENTO = IDDEPARTAMENTO;
			this._IDSTATUS = IDSTATUS;
			this._IDPRIORIDADE = IDPRIORIDADE;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMESOLICITANTE = NOMESOLICITANTE;
			this._EMAILSOLICITANTE = EMAILSOLICITANTE;
			this._TELEFONESOLICITANTE = TELEFONESOLICITANTE;
			this._SOLICITACAO = SOLICITACAO;
			this._SOLUCAO = SOLUCAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDHELPDESK
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

		public int? IDDEPARTAMENTO
		{
			get { return _IDDEPARTAMENTO; }
			set { _IDDEPARTAMENTO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public int? IDPRIORIDADE
		{
			get { return _IDPRIORIDADE; }
			set { _IDPRIORIDADE = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
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
