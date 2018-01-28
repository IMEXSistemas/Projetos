using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOREGIAOEntity
	{
		private int _IDTIPOREGIAO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOREGIAOEntity() {
		}

		public TIPOREGIAOEntity(int IDTIPOREGIAO, string NOME, string OBSERVACAO) {

			this._IDTIPOREGIAO = IDTIPOREGIAO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOREGIAO
		{
			get { return _IDTIPOREGIAO; }
			set { _IDTIPOREGIAO = value; }
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
