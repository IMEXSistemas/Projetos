using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class GRUPOAGENDAEntity
	{
		private int _IDGRUPOAGENDA;
		private string _NOMEGRUPOAGENDA;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public GRUPOAGENDAEntity() {
		}

		public GRUPOAGENDAEntity(int IDGRUPOAGENDA, string NOMEGRUPOAGENDA, string OBSERVACAO) {

			this._IDGRUPOAGENDA = IDGRUPOAGENDA;
			this._NOMEGRUPOAGENDA = NOMEGRUPOAGENDA;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDGRUPOAGENDA
		{
			get { return _IDGRUPOAGENDA; }
			set { _IDGRUPOAGENDA = value; }
		}

		public string NOMEGRUPOAGENDA
		{
			get { return _NOMEGRUPOAGENDA; }
			set { _NOMEGRUPOAGENDA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
