using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class EVENTOAGENDAEntity
	{
		private int _IDEVENTOAGENDA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public EVENTOAGENDAEntity() {
		}

		public EVENTOAGENDAEntity(int IDEVENTOAGENDA, string NOME, string OBSERVACAO) {

			this._IDEVENTOAGENDA = IDEVENTOAGENDA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDEVENTOAGENDA
		{
			get { return _IDEVENTOAGENDA; }
			set { _IDEVENTOAGENDA = value; }
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
