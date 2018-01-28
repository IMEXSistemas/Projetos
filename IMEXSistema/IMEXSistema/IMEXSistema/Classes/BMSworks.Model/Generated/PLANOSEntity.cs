using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PLANOSEntity
	{
		private int _IDPLANO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public PLANOSEntity() {
		}

		public PLANOSEntity(int IDPLANO, string NOME, string OBSERVACAO) {

			this._IDPLANO = IDPLANO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPLANO
		{
			get { return _IDPLANO; }
			set { _IDPLANO = value; }
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
