using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TRANSPORTADORAEntity
	{
		private int _IDTRANSPORTADORA;
		private string _NOME;
		private string _NOMEFANTASIA;
		private DateTime? _DATACADASTRO;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _FAX;
		private string _CNPJ;
		private string _IE;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _UF;
		private string _CEP;
		private string _OBSERVACAO;
		private string _SITE;
		private string _CODANTT;

		#region Construtores

		//Construtor default
		public TRANSPORTADORAEntity() {
			this._DATACADASTRO = null;
		}

		public TRANSPORTADORAEntity(int IDTRANSPORTADORA, string NOME, string NOMEFANTASIA, DateTime? DATACADASTRO, string TELEFONE1, string TELEFONE2, string FAX, string CNPJ, string IE, string ENDERECO, string BAIRRO, string CIDADE, string UF, string CEP, string OBSERVACAO, string SITE, string CODANTT) {

			this._IDTRANSPORTADORA = IDTRANSPORTADORA;
			this._NOME = NOME;
			this._NOMEFANTASIA = NOMEFANTASIA;
			this._DATACADASTRO = DATACADASTRO;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._FAX = FAX;
			this._CNPJ = CNPJ;
			this._IE = IE;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._CEP = CEP;
			this._OBSERVACAO = OBSERVACAO;
			this._SITE = SITE;
			this._CODANTT = CODANTT;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTRANSPORTADORA
		{
			get { return _IDTRANSPORTADORA; }
			set { _IDTRANSPORTADORA = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string NOMEFANTASIA
		{
			get { return _NOMEFANTASIA; }
			set { _NOMEFANTASIA = value; }
		}

		public DateTime? DATACADASTRO
		{
			get { return _DATACADASTRO; }
			set { _DATACADASTRO = value; }
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

		public string FAX
		{
			get { return _FAX; }
			set { _FAX = value; }
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

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string SITE
		{
			get { return _SITE; }
			set { _SITE = value; }
		}

		public string CODANTT
		{
			get { return _CODANTT; }
			set { _CODANTT = value; }
		}

		#endregion
	}
}
