using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class NIVELUSUARIOEntity
	{
		private int _IDNIVELUSUARIO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public NIVELUSUARIOEntity() {
		}

		public NIVELUSUARIOEntity(int IDNIVELUSUARIO, string NOME, string OBSERVACAO) {

			this._IDNIVELUSUARIO = IDNIVELUSUARIO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDNIVELUSUARIO
		{
			get { return _IDNIVELUSUARIO; }
			set { _IDNIVELUSUARIO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
