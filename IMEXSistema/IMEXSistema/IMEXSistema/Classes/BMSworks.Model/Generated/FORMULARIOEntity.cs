using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FORMULARIOEntity
	{
		private int _IDFORMULARIO;
		private string _NOMEFORMULARIO;
		private string _NOMETELA;
		private int? _IDGRUPOFORMLARIO;

		#region Construtores

		//Construtor default
		public FORMULARIOEntity() {
			this._IDGRUPOFORMLARIO = null;
		}

		public FORMULARIOEntity(int IDFORMULARIO, string NOMEFORMULARIO, string NOMETELA, int? IDGRUPOFORMLARIO) {

			this._IDFORMULARIO = IDFORMULARIO;
			this._NOMEFORMULARIO = NOMEFORMULARIO;
			this._NOMETELA = NOMETELA;
			this._IDGRUPOFORMLARIO = IDGRUPOFORMLARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFORMULARIO
		{
			get { return _IDFORMULARIO; }
			set { _IDFORMULARIO = value; }
		}

		public string NOMEFORMULARIO
		{
			get { return _NOMEFORMULARIO; }
			set { _NOMEFORMULARIO = value; }
		}

		public string NOMETELA
		{
			get { return _NOMETELA; }
			set { _NOMETELA = value; }
		}

		public int? IDGRUPOFORMLARIO
		{
			get { return _IDGRUPOFORMLARIO; }
			set { _IDGRUPOFORMLARIO = value; }
		}

		#endregion
	}
}
