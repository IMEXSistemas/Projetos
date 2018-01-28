using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class JORNALEntity
	{
		private int _IDJORNAL;
		private string _NOME;
		private string _CODIGO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public JORNALEntity() {
		}

		public JORNALEntity(int IDJORNAL, string NOME, string CODIGO, string OBSERVACAO) {

			this._IDJORNAL = IDJORNAL;
			this._NOME = NOME;
			this._CODIGO = CODIGO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDJORNAL
		{
			get { return _IDJORNAL; }
			set { _IDJORNAL = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
