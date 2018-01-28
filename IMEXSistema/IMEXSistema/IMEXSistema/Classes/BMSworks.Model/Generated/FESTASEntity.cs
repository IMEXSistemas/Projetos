using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FESTASEntity
	{
		private int _IDFESTAS;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public FESTASEntity() {
		}

		public FESTASEntity(int IDFESTAS, string NOME, string OBSERVACAO) {

			this._IDFESTAS = IDFESTAS;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFESTAS
		{
			get { return _IDFESTAS; }
			set { _IDFESTAS = value; }
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
