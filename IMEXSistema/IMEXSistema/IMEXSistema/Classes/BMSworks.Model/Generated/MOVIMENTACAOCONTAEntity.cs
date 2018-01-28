using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MOVIMENTACAOCONTAEntity
	{
		private int _IDMOVICONTA;
		private string _NOMEMOVIMENTACAO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public MOVIMENTACAOCONTAEntity() {
		}

		public MOVIMENTACAOCONTAEntity(int IDMOVICONTA, string NOMEMOVIMENTACAO, string OBSERVACAO) {

			this._IDMOVICONTA = IDMOVICONTA;
			this._NOMEMOVIMENTACAO = NOMEMOVIMENTACAO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMOVICONTA
		{
			get { return _IDMOVICONTA; }
			set { _IDMOVICONTA = value; }
		}

		public string NOMEMOVIMENTACAO
		{
			get { return _NOMEMOVIMENTACAO; }
			set { _NOMEMOVIMENTACAO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
