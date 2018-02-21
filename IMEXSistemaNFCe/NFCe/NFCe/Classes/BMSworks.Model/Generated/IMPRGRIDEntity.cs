using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class IMPRGRIDEntity
	{
		private int _IDIMPRGRID;
		private string _NOMETELA;
		private string _NOMEGRID;
		private string _CAMPOSSELECIONADOS;
		private string _FLAGAJUSTA;
		private string _FLAGMODOPAISAGEM;
		private string _FLAGEXIBIRDATA;
		private int? _IDFUNCIONARIO;

		#region Construtores

		//Construtor default
		public IMPRGRIDEntity() {
			this._IDFUNCIONARIO = null;
		}

		public IMPRGRIDEntity(int IDIMPRGRID, string NOMETELA, string NOMEGRID, string CAMPOSSELECIONADOS, string FLAGAJUSTA, string FLAGMODOPAISAGEM, string FLAGEXIBIRDATA, int? IDFUNCIONARIO) {

			this._IDIMPRGRID = IDIMPRGRID;
			this._NOMETELA = NOMETELA;
			this._NOMEGRID = NOMEGRID;
			this._CAMPOSSELECIONADOS = CAMPOSSELECIONADOS;
			this._FLAGAJUSTA = FLAGAJUSTA;
			this._FLAGMODOPAISAGEM = FLAGMODOPAISAGEM;
			this._FLAGEXIBIRDATA = FLAGEXIBIRDATA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDIMPRGRID
		{
			get { return _IDIMPRGRID; }
			set { _IDIMPRGRID = value; }
		}

		public string NOMETELA
		{
			get { return _NOMETELA; }
			set { _NOMETELA = value; }
		}

		public string NOMEGRID
		{
			get { return _NOMEGRID; }
			set { _NOMEGRID = value; }
		}

		public string CAMPOSSELECIONADOS
		{
			get { return _CAMPOSSELECIONADOS; }
			set { _CAMPOSSELECIONADOS = value; }
		}

		public string FLAGAJUSTA
		{
			get { return _FLAGAJUSTA; }
			set { _FLAGAJUSTA = value; }
		}

		public string FLAGMODOPAISAGEM
		{
			get { return _FLAGMODOPAISAGEM; }
			set { _FLAGMODOPAISAGEM = value; }
		}

		public string FLAGEXIBIRDATA
		{
			get { return _FLAGEXIBIRDATA; }
			set { _FLAGEXIBIRDATA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		#endregion
	}
}
