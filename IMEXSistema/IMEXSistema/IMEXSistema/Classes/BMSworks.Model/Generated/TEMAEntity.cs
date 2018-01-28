using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TEMAEntity
	{
		private int _IDTEMA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TEMAEntity() {
		}

		public TEMAEntity(int IDTEMA, string NOME, string OBSERVACAO) {

			this._IDTEMA = IDTEMA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTEMA
		{
			get { return _IDTEMA; }
			set { _IDTEMA = value; }
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
