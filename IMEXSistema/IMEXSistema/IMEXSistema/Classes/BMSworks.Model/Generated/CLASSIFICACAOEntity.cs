using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CLASSIFICACAOEntity
	{
		private int _IDCLASSIFICACAO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public CLASSIFICACAOEntity() {
		}

		public CLASSIFICACAOEntity(int IDCLASSIFICACAO, string NOME, string OBSERVACAO) {

			this._IDCLASSIFICACAO = IDCLASSIFICACAO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCLASSIFICACAO
		{
			get { return _IDCLASSIFICACAO; }
			set { _IDCLASSIFICACAO = value; }
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
