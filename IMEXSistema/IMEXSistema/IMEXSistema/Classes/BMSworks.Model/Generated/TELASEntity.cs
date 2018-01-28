using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TELASEntity
	{
		private int _IDTELA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TELASEntity() {
		}

		public TELASEntity(int IDTELA, string NOME, string OBSERVACAO) {

			this._IDTELA = IDTELA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTELA
		{
			get { return _IDTELA; }
			set { _IDTELA = value; }
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
