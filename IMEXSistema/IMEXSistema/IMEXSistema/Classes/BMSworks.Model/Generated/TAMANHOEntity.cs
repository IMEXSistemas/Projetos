using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TAMANHOEntity
	{
		private int _IDTAMANHO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TAMANHOEntity() {
		}

		public TAMANHOEntity(int IDTAMANHO, string NOME, string OBSERVACAO) {

			this._IDTAMANHO = IDTAMANHO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTAMANHO
		{
			get { return _IDTAMANHO; }
			set { _IDTAMANHO = value; }
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
