using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_TABELAIBPTEntity
	{
		private int? _IDTABELAIBPT;
		private int? _IDNCM;
		private decimal? _ALNACIONAL;
		private decimal? _ALIMPORTACAO;
		private decimal? _ALESTADUAL;
		private decimal? _ALMUNICIPAL;
		private DateTime? _DTINICIO;
		private DateTime? _DTFIM;
		private string _CHAVE;
		private string _VERSAO;
		private string _FONTE;
		private int? _IDCODUFIBGE;
		private int? _TIPO;
		private string _EX;
		private string _UF;
		private string _DESCRICAO_UF;
		private string _CODNCM;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public LIS_TABELAIBPTEntity() { }

		public LIS_TABELAIBPTEntity(int? IDTABELAIBPT, int? IDNCM, decimal? ALNACIONAL, decimal? ALIMPORTACAO, decimal? ALESTADUAL, decimal? ALMUNICIPAL, DateTime? DTINICIO, DateTime? DTFIM, string CHAVE, string VERSAO, string FONTE, int? IDCODUFIBGE, int? TIPO, string EX, string UF, string DESCRICAO_UF, string CODNCM, string DESCRICAO)		{

			this._IDTABELAIBPT = IDTABELAIBPT;
			this._IDNCM = IDNCM;
			this._ALNACIONAL = ALNACIONAL;
			this._ALIMPORTACAO = ALIMPORTACAO;
			this._ALESTADUAL = ALESTADUAL;
			this._ALMUNICIPAL = ALMUNICIPAL;
			this._DTINICIO = DTINICIO;
			this._DTFIM = DTFIM;
			this._CHAVE = CHAVE;
			this._VERSAO = VERSAO;
			this._FONTE = FONTE;
			this._IDCODUFIBGE = IDCODUFIBGE;
			this._TIPO = TIPO;
			this._EX = EX;
			this._UF = UF;
			this._DESCRICAO_UF = DESCRICAO_UF;
			this._CODNCM = CODNCM;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDTABELAIBPT
		{
			get { return _IDTABELAIBPT; }
			set { _IDTABELAIBPT = value; }
		}

		public int? IDNCM
		{
			get { return _IDNCM; }
			set { _IDNCM = value; }
		}

		public decimal? ALNACIONAL
		{
			get { return _ALNACIONAL; }
			set { _ALNACIONAL = value; }
		}

		public decimal? ALIMPORTACAO
		{
			get { return _ALIMPORTACAO; }
			set { _ALIMPORTACAO = value; }
		}

		public decimal? ALESTADUAL
		{
			get { return _ALESTADUAL; }
			set { _ALESTADUAL = value; }
		}

		public decimal? ALMUNICIPAL
		{
			get { return _ALMUNICIPAL; }
			set { _ALMUNICIPAL = value; }
		}

		public DateTime? DTINICIO
		{
			get { return _DTINICIO; }
			set { _DTINICIO = value; }
		}

		public DateTime? DTFIM
		{
			get { return _DTFIM; }
			set { _DTFIM = value; }
		}

		public string CHAVE
		{
			get { return _CHAVE; }
			set { _CHAVE = value; }
		}

		public string VERSAO
		{
			get { return _VERSAO; }
			set { _VERSAO = value; }
		}

		public string FONTE
		{
			get { return _FONTE; }
			set { _FONTE = value; }
		}

		public int? IDCODUFIBGE
		{
			get { return _IDCODUFIBGE; }
			set { _IDCODUFIBGE = value; }
		}

		public int? TIPO
		{
			get { return _TIPO; }
			set { _TIPO = value; }
		}

		public string EX
		{
			get { return _EX; }
			set { _EX = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public string DESCRICAO_UF
		{
			get { return _DESCRICAO_UF; }
			set { _DESCRICAO_UF = value; }
		}

		public string CODNCM
		{
			get { return _CODNCM; }
			set { _CODNCM = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}
