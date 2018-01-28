using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FORNECEDOREntity
	{
		private int _IDFORNECEDOR;
		private string _NOME;
		private string _NOMEFANTASIA;
		private DateTime? _DATACADASTRO;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _CNPJ;
		private string _IE;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _UF;
		private string _CEP;
		private string _EMAILFORNECEDOR;
		private string _OBSERVACAO;
		private int? _IDTRANSPORTADORA;
		private string _CONTATOTRANPORTADORA;
		private string _EMAILTRANSPORTADORA;
        private string _NUMERO;

		#region Construtores

		//Construtor default
		public FORNECEDOREntity() {
			this._DATACADASTRO = null;
			this._IDTRANSPORTADORA = null;
		}

        public FORNECEDOREntity(int IDFORNECEDOR, string NOME, string NOMEFANTASIA, DateTime? DATACADASTRO, string TELEFONE1, string TELEFONE2, string CNPJ, string IE, string ENDERECO, string BAIRRO, string CIDADE, string UF, string CEP, string EMAILFORNECEDOR, string OBSERVACAO, int? IDTRANSPORTADORA, string CONTATOTRANPORTADORA, string EMAILTRANSPORTADORA, string NUMERO)
        {

			this._IDFORNECEDOR = IDFORNECEDOR;
			this._NOME = NOME;
			this._NOMEFANTASIA = NOMEFANTASIA;
			this._DATACADASTRO = DATACADASTRO;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._CNPJ = CNPJ;
			this._IE = IE;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._CEP = CEP;
			this._EMAILFORNECEDOR = EMAILFORNECEDOR;
			this._OBSERVACAO = OBSERVACAO;
			this._IDTRANSPORTADORA = IDTRANSPORTADORA;
			this._CONTATOTRANPORTADORA = CONTATOTRANPORTADORA;
			this._EMAILTRANSPORTADORA = EMAILTRANSPORTADORA;
            this._NUMERO = NUMERO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
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

		public string EMAILFORNECEDOR
		{
			get { return _EMAILFORNECEDOR; }
			set { _EMAILFORNECEDOR = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDTRANSPORTADORA
		{
			get { return _IDTRANSPORTADORA; }
			set { _IDTRANSPORTADORA = value; }
		}

		public string CONTATOTRANPORTADORA
		{
			get { return _CONTATOTRANPORTADORA; }
			set { _CONTATOTRANPORTADORA = value; }
		}

		public string EMAILTRANSPORTADORA
		{
			get { return _EMAILTRANSPORTADORA; }
			set { _EMAILTRANSPORTADORA = value; }
		}
        public string NUMERO
        {
            get { return _NUMERO; }
            set { _NUMERO = value; }
        }


		#endregion
	}
}
