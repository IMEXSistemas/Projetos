using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class REMESSABANCOEntity
	{
		private int _IDREMESSABANCO;
		private DateTime? _DATA;
		private byte[] _ARQUIVO;
		private int? _SEGUENCIA;
		private int? _IDBANCO;
		private string _FLAGENVIADO;

		#region Construtores

		//Construtor default
		public REMESSABANCOEntity() {
			this._DATA = null;
			this._SEGUENCIA = null;
			this._IDBANCO = null;
		}

		public REMESSABANCOEntity(int IDREMESSABANCO, DateTime? DATA, byte[] ARQUIVO, int? SEGUENCIA, int? IDBANCO, string FLAGENVIADO) {

			this._IDREMESSABANCO = IDREMESSABANCO;
			this._DATA = DATA;
			this._ARQUIVO = ARQUIVO;
			this._SEGUENCIA = SEGUENCIA;
			this._IDBANCO = IDBANCO;
			this._FLAGENVIADO = FLAGENVIADO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDREMESSABANCO
		{
			get { return _IDREMESSABANCO; }
			set { _IDREMESSABANCO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public byte[] ARQUIVO
		{
			get { return _ARQUIVO; }
			set { _ARQUIVO = value; }
		}

		public int? SEGUENCIA
		{
			get { return _SEGUENCIA; }
			set { _SEGUENCIA = value; }
		}

		public int? IDBANCO
		{
			get { return _IDBANCO; }
			set { _IDBANCO = value; }
		}

		public string FLAGENVIADO
		{
			get { return _FLAGENVIADO; }
			set { _FLAGENVIADO = value; }
		}

		#endregion
	}
}
