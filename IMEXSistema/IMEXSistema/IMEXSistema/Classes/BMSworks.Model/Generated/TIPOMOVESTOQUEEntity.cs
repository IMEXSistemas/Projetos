using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOMOVESTOQUEEntity
	{
		private int _IDTIPOMOVESTOQUE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOMOVESTOQUEEntity() {
		}

		public TIPOMOVESTOQUEEntity(int IDTIPOMOVESTOQUE, string NOME, string OBSERVACAO) {

			this._IDTIPOMOVESTOQUE = IDTIPOMOVESTOQUE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOMOVESTOQUE
		{
			get { return _IDTIPOMOVESTOQUE; }
			set { _IDTIPOMOVESTOQUE = value; }
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
