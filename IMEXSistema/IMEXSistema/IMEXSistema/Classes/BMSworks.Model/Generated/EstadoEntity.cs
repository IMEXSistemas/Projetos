using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ESTADOEntity
	{
		private string _UF;
		private string _DESCRICAO_UF;
		private int _CODIGO_UF_IBGE;

		#region Construtores

		//Construtor default
		public ESTADOEntity() {
		}

		public ESTADOEntity(string UF, string DESCRICAO_UF, int CODIGO_UF_IBGE) {

			this._UF = UF;
			this._DESCRICAO_UF = DESCRICAO_UF;
			this._CODIGO_UF_IBGE = CODIGO_UF_IBGE;
		}
		#endregion

		#region Propriedades Get/Set

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public string DESCRICAO_UF
		{
			get { return _DESCRICAO_UF; }
			set { _DESCRICAO_UF = value; }
		}

		public int CODIGO_UF_IBGE
		{
			get { return _CODIGO_UF_IBGE; }
			set { _CODIGO_UF_IBGE = value; }
		}

		#endregion
	}
}
