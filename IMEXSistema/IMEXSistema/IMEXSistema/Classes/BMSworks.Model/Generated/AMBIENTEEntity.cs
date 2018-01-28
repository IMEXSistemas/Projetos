using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class AMBIENTEEntity
	{
		private int _IDAMBIENTE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public AMBIENTEEntity() {
		}

		public AMBIENTEEntity(int IDAMBIENTE, string NOME, string OBSERVACAO) {

			this._IDAMBIENTE = IDAMBIENTE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDAMBIENTE
		{
			get { return _IDAMBIENTE; }
			set { _IDAMBIENTE = value; }
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
