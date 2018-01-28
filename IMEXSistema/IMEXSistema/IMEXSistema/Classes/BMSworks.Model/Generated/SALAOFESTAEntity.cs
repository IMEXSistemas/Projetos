using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SALAOFESTAEntity
	{
		private int _IDSALAOFESTA;
		private string _NOME;
		private string _RAZAOSOCIAL;
		private DateTime? _DTCADASTRO;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _CNPJ;
		private string _IE;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _UF;
		private string _CEP;
		private string _EMAIL;
		private string _OBSERVACAO;
		private string _CONTATO;
		private string _EMAILCONTATO;

		#region Construtores

		//Construtor default
		public SALAOFESTAEntity() {
			this._DTCADASTRO = null;
		}

		public SALAOFESTAEntity(int IDSALAOFESTA, string NOME, string RAZAOSOCIAL, DateTime? DTCADASTRO, string TELEFONE1, string TELEFONE2, string CNPJ, string IE, string ENDERECO, string BAIRRO, string CIDADE, string UF, string CEP, string EMAIL, string OBSERVACAO, string CONTATO, string EMAILCONTATO) {

			this._IDSALAOFESTA = IDSALAOFESTA;
			this._NOME = NOME;
			this._RAZAOSOCIAL = RAZAOSOCIAL;
			this._DTCADASTRO = DTCADASTRO;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._CNPJ = CNPJ;
			this._IE = IE;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._CEP = CEP;
			this._EMAIL = EMAIL;
			this._OBSERVACAO = OBSERVACAO;
			this._CONTATO = CONTATO;
			this._EMAILCONTATO = EMAILCONTATO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSALAOFESTA
		{
			get { return _IDSALAOFESTA; }
			set { _IDSALAOFESTA = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string RAZAOSOCIAL
		{
			get { return _RAZAOSOCIAL; }
			set { _RAZAOSOCIAL = value; }
		}

		public DateTime? DTCADASTRO
		{
			get { return _DTCADASTRO; }
			set { _DTCADASTRO = value; }
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

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string CONTATO
		{
			get { return _CONTATO; }
			set { _CONTATO = value; }
		}

		public string EMAILCONTATO
		{
			get { return _EMAILCONTATO; }
			set { _EMAILCONTATO = value; }
		}

		#endregion
	}
}
