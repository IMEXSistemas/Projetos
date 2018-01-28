using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TABELAIBPTEntity
	{
		private int _IDTABELAIBPT;
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

		#region Construtores

		//Construtor default
		public TABELAIBPTEntity() {
			this._IDNCM = null;
			this._ALNACIONAL = null;
			this._ALIMPORTACAO = null;
			this._ALESTADUAL = null;
			this._ALMUNICIPAL = null;
			this._DTINICIO = null;
			this._DTFIM = null;
			this._IDCODUFIBGE = null;
			this._TIPO = null;
		}

		public TABELAIBPTEntity(int IDTABELAIBPT, int? IDNCM, decimal? ALNACIONAL, decimal? ALIMPORTACAO, decimal? ALESTADUAL, decimal? ALMUNICIPAL, DateTime? DTINICIO, DateTime? DTFIM, string CHAVE, string VERSAO, string FONTE, int? IDCODUFIBGE, int? TIPO, string EX) {

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
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTABELAIBPT
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

		#endregion
	}
}
