using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ENDENTREGARCLIENTEEntity
	{
		private int _IDENDENTREGARCLIENTE;
		private int? _IDCLIENTE;
		private string _NOME;
		private string _ENDERECO;
		private string _NUMERO;
		private string _COMPLEMENTO;
		private string _BAIRRO;
		private string _CEP;
		private string _CIDADE;
		private string _UF;

		#region Construtores

		//Construtor default
		public ENDENTREGARCLIENTEEntity() {
			this._IDCLIENTE = null;
		}

		public ENDENTREGARCLIENTEEntity(int IDENDENTREGARCLIENTE, int? IDCLIENTE, string NOME, string ENDERECO, string NUMERO, string COMPLEMENTO, string BAIRRO, string CEP, string CIDADE, string UF) {

			this._IDENDENTREGARCLIENTE = IDENDENTREGARCLIENTE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOME = NOME;
			this._ENDERECO = ENDERECO;
			this._NUMERO = NUMERO;
			this._COMPLEMENTO = COMPLEMENTO;
			this._BAIRRO = BAIRRO;
			this._CEP = CEP;
			this._CIDADE = CIDADE;
			this._UF = UF;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDENDENTREGARCLIENTE
		{
			get { return _IDENDENTREGARCLIENTE; }
			set { _IDENDENTREGARCLIENTE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
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

		#endregion
	}
}
