using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CLIENTEFASTEntity
	{

        private int _CODIGO_CLIENTE;
        private string _NOME_CLIENTE;
        private string  _FANTASIA_CLIENTE;
        private string  _CPF_CLIENTE;
        private string _DOC_CLIENTE;
        private string _ENDERECO_CLIENTE;
        private string _NUMEROEND_CLIENTE;
        private string _COMPLEMENTOEND_CLIENTE;
        private string _BAIRRO_CLIENTE;
        private string _CEP_CLIENTE;
        private string _EMAIL_CLIENTE;
        private string _TELEFONE1_CLIENTE;
        private string _TELEFONE2_CLIENTE;
        private string _CELULAR_CLIENTE;
        private int _COD_CIDADE;

		#region Construtores

		//Construtor default
		public CLIENTEFASTEntity() {
           
		}

        public CLIENTEFASTEntity(int CODIGO_CLIENTE, string NOME_CLIENTE, string FANTASIA_CLIENTE, string CPF_CLIENTE, string DOC_CLIENTE, string ENDERECO_CLIENTE, string NUMEROEND_CLIENTE, string COMPLEMENTOEND_CLIENTE, string BAIRRO_CLIENTE, string CEP_CLIENTE, string EMAIL_CLIENTE, string TELEFONE1_CLIENTE, string TELEFONE2_CLIENTE, string CELULAR_CLIENTE, int COD_CIDADE)
        {

            this.CODIGO_CLIENTE = CODIGO_CLIENTE;
			this._NOME_CLIENTE = NOME_CLIENTE;
			this._FANTASIA_CLIENTE = FANTASIA_CLIENTE;
			this._CPF_CLIENTE = CPF_CLIENTE;
			this._DOC_CLIENTE = DOC_CLIENTE;
			this._ENDERECO_CLIENTE = ENDERECO_CLIENTE;
			this._NUMEROEND_CLIENTE = NUMEROEND_CLIENTE;
			this._COMPLEMENTOEND_CLIENTE = COMPLEMENTOEND_CLIENTE;
			this._BAIRRO_CLIENTE = BAIRRO_CLIENTE;
			this._CEP_CLIENTE = CEP_CLIENTE;
			this._EMAIL_CLIENTE = EMAIL_CLIENTE;
			this._TELEFONE1_CLIENTE = TELEFONE1_CLIENTE;
			this._TELEFONE2_CLIENTE = TELEFONE2_CLIENTE;
			this._CELULAR_CLIENTE = CELULAR_CLIENTE;
            this._COD_CIDADE = COD_CIDADE;
			
		}
		#endregion

		#region Propriedades Get/Set

		public int CODIGO_CLIENTE
		{
			get { return _CODIGO_CLIENTE; }
			set { _CODIGO_CLIENTE = value; }
		}

		public string NOME_CLIENTE
		{
			get { return _NOME_CLIENTE; }
			set { _NOME_CLIENTE = value; }
		}

		public string FANTASIA_CLIENTE
		{
			get { return _FANTASIA_CLIENTE; }
			set { _FANTASIA_CLIENTE = value; }
		}

		public string CPF_CLIENTE
		{
			get { return _CPF_CLIENTE; }
			set { _CPF_CLIENTE = value; }
		}

		public string DOC_CLIENTE
		{
			get { return _DOC_CLIENTE; }
			set { _DOC_CLIENTE = value; }
		}	


		public string ENDERECO_CLIENTE
		{
			get { return _ENDERECO_CLIENTE; }
			set { _ENDERECO_CLIENTE = value; }
		}

		public string NUMEROEND_CLIENTE
		{
			get { return _NUMEROEND_CLIENTE; }
			set { _NUMEROEND_CLIENTE = value; }
		}

		public string COMPLEMENTOEND_CLIENTE
		{
			get { return _COMPLEMENTOEND_CLIENTE; }
			set { _COMPLEMENTOEND_CLIENTE = value; }
		}

		public string BAIRRO_CLIENTE
		{
			get { return _BAIRRO_CLIENTE; }
			set { _BAIRRO_CLIENTE = value; }
		}

		public string CEP_CLIENTE
		{
			get { return _CEP_CLIENTE; }
			set { _CEP_CLIENTE = value; }
		}

		public string EMAIL_CLIENTE
		{
			get { return _EMAIL_CLIENTE; }
			set { _EMAIL_CLIENTE = value; }
		}
		
		public string TELEFONE1_CLIENTE
		{
			get { return _TELEFONE1_CLIENTE; }
			set { _TELEFONE1_CLIENTE = value; }
		}

		public string TELEFONE2_CLIENTE
		{
			get { return _TELEFONE2_CLIENTE; }
			set { _TELEFONE2_CLIENTE = value; }
		}

		public string CELULAR_CLIENTE
		{
			get { return _CELULAR_CLIENTE; }
			set { _CELULAR_CLIENTE = value; }
		}

        public int COD_CIDADE
		{
            get { return _COD_CIDADE; }
            set { _COD_CIDADE = value; }
		}	


        
		#endregion
	}
}
