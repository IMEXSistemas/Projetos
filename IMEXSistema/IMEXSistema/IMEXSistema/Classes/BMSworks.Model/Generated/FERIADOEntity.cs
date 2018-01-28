using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FERIADOEntity
	{
		private int _IDFERIADO;
		private string _NOME;
		private DateTime? _DATA;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public FERIADOEntity() {
			this._DATA = null;
		}

		public FERIADOEntity(int IDFERIADO, string NOME, DateTime? DATA, string OBSERVACAO) {

			this._IDFERIADO = IDFERIADO;
			this._NOME = NOME;
			this._DATA = DATA;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFERIADO
		{
			get { return _IDFERIADO; }
			set { _IDFERIADO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
