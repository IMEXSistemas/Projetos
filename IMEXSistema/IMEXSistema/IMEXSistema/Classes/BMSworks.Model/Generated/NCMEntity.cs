using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class NCMEntity
	{
		private int _IDNCM;
		private string _CODNCM;
		private decimal? _ALNACIONAL;
		private decimal? _ALIMPORTACAO;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public NCMEntity() {
			this._ALNACIONAL = null;
			this._ALIMPORTACAO = null;
		}

		public NCMEntity(int IDNCM, string CODNCM, decimal? ALNACIONAL, decimal? ALIMPORTACAO, string DESCRICAO) {

			this._IDNCM = IDNCM;
			this._CODNCM = CODNCM;
			this._ALNACIONAL = ALNACIONAL;
			this._ALIMPORTACAO = ALIMPORTACAO;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDNCM
		{
			get { return _IDNCM; }
			set { _IDNCM = value; }
		}

		public string CODNCM
		{
			get { return _CODNCM; }
			set { _CODNCM = value; }
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

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}
