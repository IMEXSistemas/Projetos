using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class GRUPOFORMLARIOEntity
	{
		private int _IDGRUPOFORMLARIO;
		private string _NOME;

		#region Construtores

		//Construtor default
		public GRUPOFORMLARIOEntity() {
		}

		public GRUPOFORMLARIOEntity(int IDGRUPOFORMLARIO, string NOME) {

			this._IDGRUPOFORMLARIO = IDGRUPOFORMLARIO;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDGRUPOFORMLARIO
		{
			get { return _IDGRUPOFORMLARIO; }
			set { _IDGRUPOFORMLARIO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}
