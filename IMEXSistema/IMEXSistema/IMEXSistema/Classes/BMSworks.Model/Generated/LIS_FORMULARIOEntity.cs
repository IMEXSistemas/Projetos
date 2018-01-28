using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_FORMULARIOEntity
	{
		private int? _IDFORMULARIO;
		private string _NOMEFORMULARIO;
		private string _NOMETELA;
		private int? _IDGRUPOFORMLARIO;
		private string _NOMEGRUPO;

		#region Construtores

		//Construtor default
		public LIS_FORMULARIOEntity() { }

		public LIS_FORMULARIOEntity(int? IDFORMULARIO, string NOMEFORMULARIO, string NOMETELA, int? IDGRUPOFORMLARIO, string NOMEGRUPO)		{

			this._IDFORMULARIO = IDFORMULARIO;
			this._NOMEFORMULARIO = NOMEFORMULARIO;
			this._NOMETELA = NOMETELA;
			this._IDGRUPOFORMLARIO = IDGRUPOFORMLARIO;
			this._NOMEGRUPO = NOMEGRUPO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDFORMULARIO
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

		public string NOMEGRUPO
		{
			get { return _NOMEGRUPO; }
			set { _NOMEGRUPO = value; }
		}

		#endregion
	}
}
