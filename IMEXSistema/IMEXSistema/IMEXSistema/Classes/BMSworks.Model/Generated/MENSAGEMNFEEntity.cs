using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MENSAGEMNFEEntity
	{
		private int _IDMENSAGEMNFE;
		private string _MENSAGEM;
		private string _NOME;

		#region Construtores

		//Construtor default
		public MENSAGEMNFEEntity() {
		}

		public MENSAGEMNFEEntity(int IDMENSAGEMNFE, string MENSAGEM, string NOME) {

			this._IDMENSAGEMNFE = IDMENSAGEMNFE;
			this._MENSAGEM = MENSAGEM;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMENSAGEMNFE
		{
			get { return _IDMENSAGEMNFE; }
			set { _IDMENSAGEMNFE = value; }
		}

		public string MENSAGEM
		{
			get { return _MENSAGEM; }
			set { _MENSAGEM = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}
