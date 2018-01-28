using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class GRUPOAGENDATLMKEntity
	{
		private int _IDGRUPOAGENDATLMK;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public GRUPOAGENDATLMKEntity() {
		}

		public GRUPOAGENDATLMKEntity(int IDGRUPOAGENDATLMK, string NOME, string OBSERVACAO) {

			this._IDGRUPOAGENDATLMK = IDGRUPOAGENDATLMK;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDGRUPOAGENDATLMK
		{
			get { return _IDGRUPOAGENDATLMK; }
			set { _IDGRUPOAGENDATLMK = value; }
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
