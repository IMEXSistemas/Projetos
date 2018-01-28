using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class BANCOEntity
	{
		private int _IDBANCO;
		private string _NOMEBANCO;
		private string _NUMEROBANCO;

		#region Construtores

		//Construtor default
		public BANCOEntity() {
		}

		public BANCOEntity(int IDBANCO, string NOMEBANCO, string NUMEROBANCO) {

			this._IDBANCO = IDBANCO;
			this._NOMEBANCO = NOMEBANCO;
			this._NUMEROBANCO = NUMEROBANCO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDBANCO
		{
			get { return _IDBANCO; }
			set { _IDBANCO = value; }
		}

		public string NOMEBANCO
		{
			get { return _NOMEBANCO; }
			set { _NOMEBANCO = value; }
		}

		public string NUMEROBANCO
		{
			get { return _NUMEROBANCO; }
			set { _NUMEROBANCO = value; }
		}

		#endregion
	}
}
