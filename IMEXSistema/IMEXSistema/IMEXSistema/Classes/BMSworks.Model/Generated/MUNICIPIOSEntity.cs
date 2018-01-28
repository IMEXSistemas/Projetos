using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MUNICIPIOSEntity
	{
		private int _COD_MUN_IBGE;
		private int? _COD_UF_IBGE;
		private string _MUNICIPIO;

		#region Construtores

		//Construtor default
		public MUNICIPIOSEntity() {
			this._COD_UF_IBGE = null;
		}

		public MUNICIPIOSEntity(int COD_MUN_IBGE, int? COD_UF_IBGE, string MUNICIPIO) {

			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._COD_UF_IBGE = COD_UF_IBGE;
			this._MUNICIPIO = MUNICIPIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
		}

		public int? COD_UF_IBGE
		{
			get { return _COD_UF_IBGE; }
			set { _COD_UF_IBGE = value; }
		}

		public string MUNICIPIO
		{
			get { return _MUNICIPIO; }
			set { _MUNICIPIO = value; }
		}

		#endregion
	}
}
