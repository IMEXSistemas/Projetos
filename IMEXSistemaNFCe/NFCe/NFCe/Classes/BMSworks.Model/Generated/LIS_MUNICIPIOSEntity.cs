using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MUNICIPIOSEntity
	{
		private int? _COD_MUN_IBGE;
		private int? _COD_UF_IBGE;
		private string _MUNICIPIO;
		private string _UF;
		private string _DESCRICAO_UF;
		private string _MUNIUF;

		#region Construtores

		//Construtor default
		public LIS_MUNICIPIOSEntity() { }

		public LIS_MUNICIPIOSEntity(int? COD_MUN_IBGE, int? COD_UF_IBGE, string MUNICIPIO, string UF, string DESCRICAO_UF, string MUNIUF)		{

			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._COD_UF_IBGE = COD_UF_IBGE;
			this._MUNICIPIO = MUNICIPIO;
			this._UF = UF;
			this._DESCRICAO_UF = DESCRICAO_UF;
			this._MUNIUF = MUNIUF;
		}
		#endregion

		#region Propriedades Get/Set

		public int? COD_MUN_IBGE
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

		public string MUNIUF
		{
			get { return _MUNIUF; }
			set { _MUNIUF = value; }
		}

		#endregion
	}
}
