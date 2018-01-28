using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class UNIDADEEntity
	{
		private int _IDUNIDADE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public UNIDADEEntity() {
		}

		public UNIDADEEntity(int IDUNIDADE, string NOME, string OBSERVACAO) {

			this._IDUNIDADE = IDUNIDADE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDUNIDADE
		{
			get { return _IDUNIDADE; }
			set { _IDUNIDADE = value; }
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
