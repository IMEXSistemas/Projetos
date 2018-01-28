using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOMOVCAIXAEntity
	{
		private int _IDTIPOMOVCAIXA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOMOVCAIXAEntity() {
		}

		public TIPOMOVCAIXAEntity(int IDTIPOMOVCAIXA, string NOME, string OBSERVACAO) {

			this._IDTIPOMOVCAIXA = IDTIPOMOVCAIXA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOMOVCAIXA
		{
			get { return _IDTIPOMOVCAIXA; }
			set { _IDTIPOMOVCAIXA = value; }
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
