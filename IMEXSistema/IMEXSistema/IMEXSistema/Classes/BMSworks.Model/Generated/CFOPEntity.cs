using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CFOPEntity
	{
		private int _IDCFOP;
		private string _CODCFOP;
		private string _DESCRICAO;
		private string _FLAGBAIXAESTOQUE;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public CFOPEntity() {
		}

		public CFOPEntity(int IDCFOP, string CODCFOP, string DESCRICAO, string FLAGBAIXAESTOQUE, string OBSERVACAO) {

			this._IDCFOP = IDCFOP;
			this._CODCFOP = CODCFOP;
			this._DESCRICAO = DESCRICAO;
			this._FLAGBAIXAESTOQUE = FLAGBAIXAESTOQUE;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string CODCFOP
		{
			get { return _CODCFOP; }
			set { _CODCFOP = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string FLAGBAIXAESTOQUE
		{
			get { return _FLAGBAIXAESTOQUE; }
			set { _FLAGBAIXAESTOQUE = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
