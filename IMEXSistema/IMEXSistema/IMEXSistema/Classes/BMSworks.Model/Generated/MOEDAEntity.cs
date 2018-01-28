using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MOEDAEntity
	{
		private int _IDMOEDA;
		private string _NOME;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public MOEDAEntity() {
		}

		public MOEDAEntity(int IDMOEDA, string NOME, string DESCRICAO) {

			this._IDMOEDA = IDMOEDA;
			this._NOME = NOME;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMOEDA
		{
			get { return _IDMOEDA; }
			set { _IDMOEDA = value; }
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

		#endregion
	}
}
