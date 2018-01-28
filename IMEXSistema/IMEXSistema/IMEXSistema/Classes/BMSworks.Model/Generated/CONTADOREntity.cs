using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONTADOREntity
	{
		private int _CONTADORID;
		private string _NOME;
		private string _CPF;
		private string _CNPJ;
		private string _CRC;
		private string _CEP;
		private string _ENDERECO;
		private string _NUMERO;
		private string _COMPLEMENTO;
		private string _BAIRRO;
		private string _FONE;
		private string _FAX;
		private string _EMAIL;
		private int? _COD_MUN;
        private string _OBSERVACAO;
        private string _FLAGATIVO;

		#region Construtores

		//Construtor default
		public CONTADOREntity() {
			this._COD_MUN = null;
		}

        public CONTADOREntity(int CONTADORID, string NOME, string CPF, string CNPJ, string CRC, string CEP, string ENDERECO, string NUMERO, string COMPLEMENTO,
                               string BAIRRO, string FONE, string FAX, string EMAIL, int? COD_MUN, string OBSERVACAO, string FLAGATIVO)
        {

			this._CONTADORID = CONTADORID;
			this._NOME = NOME;
			this._CPF = CPF;
			this._CNPJ = CNPJ;
			this._CRC = CRC;
			this._CEP = CEP;
			this._ENDERECO = ENDERECO;
			this._NUMERO = NUMERO;
			this._COMPLEMENTO = COMPLEMENTO;
			this._BAIRRO = BAIRRO;
			this._FONE = FONE;
			this._FAX = FAX;
			this._EMAIL = EMAIL;
			this._COD_MUN = COD_MUN;
            this._OBSERVACAO = OBSERVACAO;
            this._FLAGATIVO = FLAGATIVO;
		}
		#endregion

		#region Propriedades Get/Set

		public int CONTADORID
		{
			get { return _CONTADORID; }
			set { _CONTADORID = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string CPF
		{
			get { return _CPF; }
			set { _CPF = value; }
		}

		public string CNPJ
		{
			get { return _CNPJ; }
			set { _CNPJ = value; }
		}

		public string CRC
		{
			get { return _CRC; }
			set { _CRC = value; }
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

		public string FONE
		{
			get { return _FONE; }
			set { _FONE = value; }
		}

		public string FAX
		{
			get { return _FAX; }
			set { _FAX = value; }
		}

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public int? COD_MUN
		{
			get { return _COD_MUN; }
			set { _COD_MUN = value; }
		}
        

        public string OBSERVACAO
		{
            get { return _OBSERVACAO; }
            set { _OBSERVACAO = value; }
		}

        public string FLAGATIVO
        {
            get { return _FLAGATIVO; }
            set { _FLAGATIVO = value; }
        }

        

		#endregion
	}
}
