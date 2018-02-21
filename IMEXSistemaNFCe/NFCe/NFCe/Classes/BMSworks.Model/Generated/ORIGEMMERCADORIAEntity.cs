using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ORIGEMMERCADORIAEntity
	{
		private int _IDORIGEMMERC;
		private string _CODIGO;
		private string _DESCRICAO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public ORIGEMMERCADORIAEntity() {
		}

		public ORIGEMMERCADORIAEntity(int IDORIGEMMERC, string CODIGO, string DESCRICAO, string OBSERVACAO) {

			this._IDORIGEMMERC = IDORIGEMMERC;
			this._CODIGO = CODIGO;
			this._DESCRICAO = DESCRICAO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDORIGEMMERC
		{
			get { return _IDORIGEMMERC; }
			set { _IDORIGEMMERC = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
