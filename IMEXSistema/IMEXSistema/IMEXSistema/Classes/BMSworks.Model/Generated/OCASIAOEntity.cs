using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class OCASIAOEntity
	{
		private int _IDOCASIAO;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public OCASIAOEntity() {
		}

		public OCASIAOEntity(int IDOCASIAO, string DESCRICAO) {

			this._IDOCASIAO = IDOCASIAO;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDOCASIAO
		{
			get { return _IDOCASIAO; }
			set { _IDOCASIAO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}
