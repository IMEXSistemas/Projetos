using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MARCAEntity
	{
		private int _IDMARCA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public MARCAEntity() {
		}

		public MARCAEntity(int IDMARCA, string NOME, string OBSERVACAO) {

			this._IDMARCA = IDMARCA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
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
