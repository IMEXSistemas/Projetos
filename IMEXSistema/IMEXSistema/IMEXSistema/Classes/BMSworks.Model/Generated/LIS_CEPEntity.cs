using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CEPEntity
	{
		private int? _IDCEP;
		private string _CEP;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _OBSERVACAO;
		private int? _COD_MUN_IBGE;
		private string _MUNICIPIO;
		private string _UF;

		#region Construtores

		//Construtor default
		public LIS_CEPEntity() { }

		public LIS_CEPEntity(int? IDCEP, string CEP, string ENDERECO, string BAIRRO, string OBSERVACAO, int? COD_MUN_IBGE, string MUNICIPIO, string UF)		{

			this._IDCEP = IDCEP;
			this._CEP = CEP;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._OBSERVACAO = OBSERVACAO;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._MUNICIPIO = MUNICIPIO;
			this._UF = UF;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCEP
		{
			get { return _IDCEP; }
			set { _IDCEP = value; }
		}

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string ENDERECO
		{
			get { return _ENDERECO; }
			set { _ENDERECO = value; }
		}

		public string BAIRRO
		{
			get { return _BAIRRO; }
			set { _BAIRRO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
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

		#endregion
	}
}
