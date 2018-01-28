using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FONTEEntity
	{
		private int _IDFONTE;
		private string _NOME;

		#region Construtores

		//Construtor default
		public FONTEEntity() {
		}

		public FONTEEntity(int IDFONTE, string NOME) {

			this._IDFONTE = IDFONTE;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFONTE
		{
			get { return _IDFONTE; }
			set { _IDFONTE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}
