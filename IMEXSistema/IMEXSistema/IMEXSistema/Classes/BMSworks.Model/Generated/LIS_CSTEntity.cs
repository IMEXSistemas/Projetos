using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CSTEntity
	{
		private int? _IDCST;
		private int? _IDORIGEM;
		private string _CODIGO;
		private string _DESCRICAO;
		private string _OBSERVACAO;
		private string _CODCOMPL;

		#region Construtores

		//Construtor default
		public LIS_CSTEntity() { }

		public LIS_CSTEntity(int? IDCST, int? IDORIGEM, string CODIGO, string DESCRICAO, string OBSERVACAO, string CODCOMPL)		{

			this._IDCST = IDCST;
			this._IDORIGEM = IDORIGEM;
			this._CODIGO = CODIGO;
			this._DESCRICAO = DESCRICAO;
			this._OBSERVACAO = OBSERVACAO;
			this._CODCOMPL = CODCOMPL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCST
		{
			get { return _IDCST; }
			set { _IDCST = value; }
		}

		public int? IDORIGEM
		{
			get { return _IDORIGEM; }
			set { _IDORIGEM = value; }
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

		public string CODCOMPL
		{
			get { return _CODCOMPL; }
			set { _CODCOMPL = value; }
		}

		#endregion
	}
}
