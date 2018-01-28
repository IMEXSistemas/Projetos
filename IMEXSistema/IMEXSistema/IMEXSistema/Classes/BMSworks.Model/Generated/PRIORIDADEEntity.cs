using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRIORIDADEEntity
	{
		private int _IDPRIORIDADE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public PRIORIDADEEntity() {
		}

		public PRIORIDADEEntity(int IDPRIORIDADE, string NOME, string OBSERVACAO) {

			this._IDPRIORIDADE = IDPRIORIDADE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRIORIDADE
		{
			get { return _IDPRIORIDADE; }
			set { _IDPRIORIDADE = value; }
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
