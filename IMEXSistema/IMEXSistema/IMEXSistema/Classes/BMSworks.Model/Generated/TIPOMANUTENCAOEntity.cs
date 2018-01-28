using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOMANUTENCAOEntity
	{
		private int _IDTIPOMANUTENCAO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOMANUTENCAOEntity() {
		}

		public TIPOMANUTENCAOEntity(int IDTIPOMANUTENCAO, string NOME, string OBSERVACAO) {

			this._IDTIPOMANUTENCAO = IDTIPOMANUTENCAO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOMANUTENCAO
		{
			get { return _IDTIPOMANUTENCAO; }
			set { _IDTIPOMANUTENCAO = value; }
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
