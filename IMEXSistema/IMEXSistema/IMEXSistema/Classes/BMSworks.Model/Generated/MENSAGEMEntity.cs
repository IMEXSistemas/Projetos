using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MENSAGEMEntity
	{
		private int _IDMENSAGEM;
		private string _MENSAGEM;
		private string _NOME;

		#region Construtores

		//Construtor default
		public MENSAGEMEntity() {
		}

		public MENSAGEMEntity(int IDMENSAGEM, string MENSAGEM, string NOME) {

			this._IDMENSAGEM = IDMENSAGEM;
			this._MENSAGEM = MENSAGEM;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMENSAGEM
		{
			get { return _IDMENSAGEM; }
			set { _IDMENSAGEM = value; }
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
