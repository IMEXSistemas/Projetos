using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PARENTESCOEntity
	{
		private int _IDPARENTESCO;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public PARENTESCOEntity() {
		}

		public PARENTESCOEntity(int IDPARENTESCO, string DESCRICAO) {

			this._IDPARENTESCO = IDPARENTESCO;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPARENTESCO
		{
			get { return _IDPARENTESCO; }
			set { _IDPARENTESCO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}
