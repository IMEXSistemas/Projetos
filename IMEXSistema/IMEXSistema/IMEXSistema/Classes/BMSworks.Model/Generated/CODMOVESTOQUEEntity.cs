using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CODMOVESTOQUEEntity
	{
		private int _IDCODMOVESTOQUE;
		private string _CODIGO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public CODMOVESTOQUEEntity() {
		}

		public CODMOVESTOQUEEntity(int IDCODMOVESTOQUE, string CODIGO, string NOME, string OBSERVACAO) {

			this._IDCODMOVESTOQUE = IDCODMOVESTOQUE;
			this._CODIGO = CODIGO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCODMOVESTOQUE
		{
			get { return _IDCODMOVESTOQUE; }
			set { _IDCODMOVESTOQUE = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
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
