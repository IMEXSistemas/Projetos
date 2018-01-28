using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class COREntity
	{
		private int _IDCOR;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public COREntity() {
		}

		public COREntity(int IDCOR, string NOME, string OBSERVACAO) {

			this._IDCOR = IDCOR;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCOR
		{
			get { return _IDCOR; }
			set { _IDCOR = value; }
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
