using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CABDAVEntity
	{
		private string _NUMERO;
		private string _CLIENTE;
		private string _NOME;
		private string _CNPJ_CNPF;
		private DateTime? _DATA_DAV;
		private string _NUMERO_COO;
		private string _NUMERO_COO_DAV;
		private string _NUMERO_SEQ_ECF;
		private string _TITULO;
		private string _VENDEDOR;
		private string _OBSERVACOES;
		private decimal? _VALOR_TOT_DES;
		private decimal? _VALOR_TOT_PRO;
		private decimal? _VALOR_TOT_DAV;
		private int? _IMPRESSO;
		private string _CHAVE;

		#region Construtores

		//Construtor default
		public CABDAVEntity() {
			this._DATA_DAV = DateTime.Now;
			this._VALOR_TOT_DES = null;
			this._VALOR_TOT_PRO = null;
			this._VALOR_TOT_DAV = null;
			this._IMPRESSO = null;
		}

        public CABDAVEntity(string NUMERO, string CLIENTE, string NOME, string CNPJ_CNPF, DateTime? DATA_DAV, string NUMERO_COO, string NUMERO_COO_DAV, string NUMERO_SEQ_ECF, string TITULO, string VENDEDOR, string OBSERVACOES, decimal? VALOR_TOT_DES, decimal? VALOR_TOT_PRO, decimal? VALOR_TOT_DAV, int? IMPRESSO, string CHAVE)
        {

			this._NUMERO = NUMERO;
			this._CLIENTE = CLIENTE;
			this._NOME = NOME;
			this._CNPJ_CNPF = CNPJ_CNPF;
			this._DATA_DAV = DATA_DAV;
			this._NUMERO_COO = NUMERO_COO;
			this._NUMERO_COO_DAV = NUMERO_COO_DAV;
			this._NUMERO_SEQ_ECF = NUMERO_SEQ_ECF;
			this._TITULO = TITULO;
			this._VENDEDOR = VENDEDOR;
			this._OBSERVACOES = OBSERVACOES;
			this._VALOR_TOT_DES = VALOR_TOT_DES;
			this._VALOR_TOT_PRO = VALOR_TOT_PRO;
			this._VALOR_TOT_DAV = VALOR_TOT_DAV;
			this._IMPRESSO = IMPRESSO;
			this._CHAVE = CHAVE;
		}
		#endregion

		#region Propriedades Get/Set

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public string CLIENTE
		{
			get { return _CLIENTE; }
			set { _CLIENTE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string CNPJ_CNPF
		{
			get { return _CNPJ_CNPF; }
			set { _CNPJ_CNPF = value; }
		}

		public DateTime? DATA_DAV
		{
			get { return _DATA_DAV; }
			set { _DATA_DAV = value; }
		}

		public string NUMERO_COO
		{
			get { return _NUMERO_COO; }
			set { _NUMERO_COO = value; }
		}

		public string NUMERO_COO_DAV
		{
			get { return _NUMERO_COO_DAV; }
			set { _NUMERO_COO_DAV = value; }
		}

		public string NUMERO_SEQ_ECF
		{
			get { return _NUMERO_SEQ_ECF; }
			set { _NUMERO_SEQ_ECF = value; }
		}

		public string TITULO
		{
			get { return _TITULO; }
			set { _TITULO = value; }
		}

		public string VENDEDOR
		{
			get { return _VENDEDOR; }
			set { _VENDEDOR = value; }
		}

		public string OBSERVACOES
		{
			get { return _OBSERVACOES; }
			set { _OBSERVACOES = value; }
		}

		public decimal? VALOR_TOT_DES
		{
			get { return _VALOR_TOT_DES; }
			set { _VALOR_TOT_DES = value; }
		}

		public decimal? VALOR_TOT_PRO
		{
			get { return _VALOR_TOT_PRO; }
			set { _VALOR_TOT_PRO = value; }
		}

		public decimal? VALOR_TOT_DAV
		{
			get { return _VALOR_TOT_DAV; }
			set { _VALOR_TOT_DAV = value; }
		}

        public int? IMPRESSO
		{
			get { return _IMPRESSO; }
			set { _IMPRESSO = value; }
		}

		public string CHAVE
		{
			get { return _CHAVE; }
			set { _CHAVE = value; }
		}

		#endregion
	}
}
