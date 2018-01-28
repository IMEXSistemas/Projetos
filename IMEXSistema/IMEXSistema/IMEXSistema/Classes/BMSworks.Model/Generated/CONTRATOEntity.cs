using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONTRATOEntity
	{
		private int _IDCONTRATO;
		private string _NOME;
		private string _DESCRICAO;
		private string _FLAGPRINCIPAL;

		#region Construtores

		//Construtor default
		public CONTRATOEntity() {
		}

		public CONTRATOEntity(int IDCONTRATO, string NOME, string DESCRICAO, string FLAGPRINCIPAL) {

			this._IDCONTRATO = IDCONTRATO;
			this._NOME = NOME;
			this._DESCRICAO = DESCRICAO;
			this._FLAGPRINCIPAL = FLAGPRINCIPAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONTRATO
		{
			get { return _IDCONTRATO; }
			set { _IDCONTRATO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string FLAGPRINCIPAL
		{
			get { return _FLAGPRINCIPAL; }
			set { _FLAGPRINCIPAL = value; }
		}

		#endregion
	}
}
