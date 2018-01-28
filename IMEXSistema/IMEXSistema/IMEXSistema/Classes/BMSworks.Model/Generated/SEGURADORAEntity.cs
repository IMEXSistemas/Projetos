using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SEGURADORAEntity
	{
		private int _IDSEGURADORA;
		private string _NOME;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _CNPJ;
		private string _IE;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _UF;
		private string _CEP;
		private string _EMAILSEGURADORA;
		private string _OBSERVACAO;
		private string _NUMERO;

		#region Construtores

		//Construtor default
		public SEGURADORAEntity() {
		}

		public SEGURADORAEntity(int IDSEGURADORA, string NOME, string TELEFONE1, string TELEFONE2, string CNPJ, string IE, string ENDERECO, string BAIRRO, string CIDADE, string UF, string CEP, string EMAILSEGURADORA, string OBSERVACAO, string NUMERO) {

			this._IDSEGURADORA = IDSEGURADORA;
			this._NOME = NOME;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._CNPJ = CNPJ;
			this._IE = IE;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._CEP = CEP;
			this._EMAILSEGURADORA = EMAILSEGURADORA;
			this._OBSERVACAO = OBSERVACAO;
			this._NUMERO = NUMERO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSEGURADORA
		{
			get { return _IDSEGURADORA; }
			set { _IDSEGURADORA = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string TELEFONE1
		{
			get { return _TELEFONE1; }
			set { _TELEFONE1 = value; }
		}

		public string TELEFONE2
		{
			get { return _TELEFONE2; }
			set { _TELEFONE2 = value; }
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

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string EMAILSEGURADORA
		{
			get { return _EMAILSEGURADORA; }
			set { _EMAILSEGURADORA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		#endregion
	}
}
