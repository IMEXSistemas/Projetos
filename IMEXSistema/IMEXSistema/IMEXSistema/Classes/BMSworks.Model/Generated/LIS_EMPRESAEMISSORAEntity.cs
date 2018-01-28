using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_EMPRESAEMISSORAEntity
	{
		private int? _IDEMPRESAEMISSORA;
		private string _RAZAOSOCIAL;
		private string _NOMEFANTASIA;
		private string _TELEFONE;
		private string _CNPJ;
		private string _IE;
		private string _EMAIL;
		private string _ENDERECO;
		private string _NUMERO;
		private string _COMPLEMENTO;
		private string _BAIRRO;
		private string _CEP;
		private string _IMUNICIPAL;
		private string _CRT;
		private string _IEST;
		private string _CNAE;
		private string _NOMECERTIFICADO;
		private string _SERIACERTIFICADO;
		private string _VALIDADECERTIFICADO;
		private int? _COD_MUN_IBGE;
		private string _MUNICIPIO;
		private string _UF;

		#region Construtores

		//Construtor default
		public LIS_EMPRESAEMISSORAEntity() { }

		public LIS_EMPRESAEMISSORAEntity(int? IDEMPRESAEMISSORA, string RAZAOSOCIAL, string NOMEFANTASIA, string TELEFONE, string CNPJ, string IE, string EMAIL, string ENDERECO, string NUMERO, string COMPLEMENTO, string BAIRRO, string CEP, string IMUNICIPAL, string CRT, string IEST, string CNAE, string NOMECERTIFICADO, string SERIACERTIFICADO, string VALIDADECERTIFICADO, int? COD_MUN_IBGE, string MUNICIPIO, string UF)		{

			this._IDEMPRESAEMISSORA = IDEMPRESAEMISSORA;
			this._RAZAOSOCIAL = RAZAOSOCIAL;
			this._NOMEFANTASIA = NOMEFANTASIA;
			this._TELEFONE = TELEFONE;
			this._CNPJ = CNPJ;
			this._IE = IE;
			this._EMAIL = EMAIL;
			this._ENDERECO = ENDERECO;
			this._NUMERO = NUMERO;
			this._COMPLEMENTO = COMPLEMENTO;
			this._BAIRRO = BAIRRO;
			this._CEP = CEP;
			this._IMUNICIPAL = IMUNICIPAL;
			this._CRT = CRT;
			this._IEST = IEST;
			this._CNAE = CNAE;
			this._NOMECERTIFICADO = NOMECERTIFICADO;
			this._SERIACERTIFICADO = SERIACERTIFICADO;
			this._VALIDADECERTIFICADO = VALIDADECERTIFICADO;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._MUNICIPIO = MUNICIPIO;
			this._UF = UF;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDEMPRESAEMISSORA
		{
			get { return _IDEMPRESAEMISSORA; }
			set { _IDEMPRESAEMISSORA = value; }
		}

		public string RAZAOSOCIAL
		{
			get { return _RAZAOSOCIAL; }
			set { _RAZAOSOCIAL = value; }
		}

		public string NOMEFANTASIA
		{
			get { return _NOMEFANTASIA; }
			set { _NOMEFANTASIA = value; }
		}

		public string TELEFONE
		{
			get { return _TELEFONE; }
			set { _TELEFONE = value; }
		}

		public string CNPJ
		{
			get { return _CNPJ; }
			set { _CNPJ = value; }
		}

		public string IE
		{
			get { return _IE; }
			set { _IE = value; }
		}

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public string ENDERECO
		{
			get { return _ENDERECO; }
			set { _ENDERECO = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public string COMPLEMENTO
		{
			get { return _COMPLEMENTO; }
			set { _COMPLEMENTO = value; }
		}

		public string BAIRRO
		{
			get { return _BAIRRO; }
			set { _BAIRRO = value; }
		}

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string IMUNICIPAL
		{
			get { return _IMUNICIPAL; }
			set { _IMUNICIPAL = value; }
		}

		public string CRT
		{
			get { return _CRT; }
			set { _CRT = value; }
		}

		public string IEST
		{
			get { return _IEST; }
			set { _IEST = value; }
		}

		public string CNAE
		{
			get { return _CNAE; }
			set { _CNAE = value; }
		}

		public string NOMECERTIFICADO
		{
			get { return _NOMECERTIFICADO; }
			set { _NOMECERTIFICADO = value; }
		}

		public string SERIACERTIFICADO
		{
			get { return _SERIACERTIFICADO; }
			set { _SERIACERTIFICADO = value; }
		}

		public string VALIDADECERTIFICADO
		{
			get { return _VALIDADECERTIFICADO; }
			set { _VALIDADECERTIFICADO = value; }
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
