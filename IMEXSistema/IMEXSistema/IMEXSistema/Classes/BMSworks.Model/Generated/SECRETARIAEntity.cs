using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SECRETARIAEntity
	{
		private int _IDSECRETARIA;
		private string _NOME;
		private string _CODIGO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public SECRETARIAEntity() {
		}

		public SECRETARIAEntity(int IDSECRETARIA, string NOME, string CODIGO, string OBSERVACAO) {

			this._IDSECRETARIA = IDSECRETARIA;
			this._NOME = NOME;
			this._CODIGO = CODIGO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSECRETARIA
		{
			get { return _IDSECRETARIA; }
			set { _IDSECRETARIA = value; }
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
