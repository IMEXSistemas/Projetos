using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TRIBUNALEntity
	{
		private int _IDTRIBUNAL;
		private string _NOME;
		private string _CODIGO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TRIBUNALEntity() {
		}

		public TRIBUNALEntity(int IDTRIBUNAL, string NOME, string CODIGO, string OBSERVACAO) {

			this._IDTRIBUNAL = IDTRIBUNAL;
			this._NOME = NOME;
			this._CODIGO = CODIGO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTRIBUNAL
		{
			get { return _IDTRIBUNAL; }
			set { _IDTRIBUNAL = value; }
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
