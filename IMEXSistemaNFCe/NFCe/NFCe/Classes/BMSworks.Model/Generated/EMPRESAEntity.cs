using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class EMPRESAEntity
	{
		private int _IDEMPRESA;
		private string _NOMECLIENTE;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CEP;
		private string _CIDADE;
		private string _UF;
		private string _TELEFONE;
		private string _FAX;
		private string _CNPJCPF;
		private string _IE;
		private string _EMAIL;
		private string _REGISTRO;
		private string _NUMERO;
		private string _COMPLEMENTO;
		private string _NOMEFANTASIA;

		#region Construtores

		//Construtor default
		public EMPRESAEntity() {
		}

		public EMPRESAEntity(int IDEMPRESA, string NOMECLIENTE, string ENDERECO, string BAIRRO, string CEP, string CIDADE, string UF, string TELEFONE, string FAX, string CNPJCPF, string IE, string EMAIL, string REGISTRO, string NUMERO, string COMPLEMENTO, string NOMEFANTASIA) {

			this._IDEMPRESA = IDEMPRESA;
			this._NOMECLIENTE = NOMECLIENTE;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CEP = CEP;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._TELEFONE = TELEFONE;
			this._FAX = FAX;
			this._CNPJCPF = CNPJCPF;
			this._IE = IE;
			this._EMAIL = EMAIL;
			this._REGISTRO = REGISTRO;
			this._NUMERO = NUMERO;
			this._COMPLEMENTO = COMPLEMENTO;
			this._NOMEFANTASIA = NOMEFANTASIA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDEMPRESA
		{
			get { return _IDEMPRESA; }
			set { _IDEMPRESA = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
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

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string CIDADE
		{
			get { return _CIDADE; }
			set { _CIDADE = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public string TELEFONE
		{
			get { return _TELEFONE; }
			set { _TELEFONE = value; }
		}

		public string FAX
		{
			get { return _FAX; }
			set { _FAX = value; }
		}

		public string CNPJCPF
		{
			get { return _CNPJCPF; }
			set { _CNPJCPF = value; }
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

		public string REGISTRO
		{
			get { return _REGISTRO; }
			set { _REGISTRO = value; }
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

		public string NOMEFANTASIA
		{
			get { return _NOMEFANTASIA; }
			set { _NOMEFANTASIA = value; }
		}

		#endregion
	}
}
