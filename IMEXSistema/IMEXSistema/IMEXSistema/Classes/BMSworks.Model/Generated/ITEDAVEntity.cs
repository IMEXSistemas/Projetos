using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITEDAVEntity
	{
		private string _NUMERO;
		private DateTime _DATA_DAV;
		private string _CLIENTE;
		private int _ITEM;
		private string _CODIGO;
		private string _BARRAS;
		private string _DESCRICAO;
		private string _UND;
		private decimal? _QTD;
		private string _GRADE_QUA;
		private string _GRADE_DIS;
		private int? _CANCELADO;
		private string _TOTALIZADOR;
		private decimal? _VALOR_UNITA;
		private decimal? _DESCONTO;
		private decimal? _ACRESCIMO;
		private decimal? _VALOR_TOTAL;
		private string _ST;
		private string _ALIQUOTA;
		private string _CHAVE;
        private string _TITULO;

		#region Construtores

		//Construtor default
		public ITEDAVEntity() {
			this._DATA_DAV = DateTime.Now;
			this._QTD = null;
			this._CANCELADO = null;
			this._VALOR_UNITA = null;
			this._DESCONTO = null;
			this._ACRESCIMO = null;
			this._VALOR_TOTAL = null;
		}

        public ITEDAVEntity(string NUMERO, DateTime DATA_DAV, string CLIENTE, int ITEM, string CODIGO, string BARRAS, string DESCRICAO, string UND, decimal? QTD, string GRADE_QUA, string GRADE_DIS, int? CANCELADO, string TOTALIZADOR, decimal? VALOR_UNITA, decimal? DESCONTO, decimal? ACRESCIMO, decimal? VALOR_TOTAL, string ST, string ALIQUOTA, string CHAVE, string TITULO)
        {

			this._NUMERO = NUMERO;
			this._DATA_DAV = DATA_DAV;
			this._CLIENTE = CLIENTE;
			this._ITEM = ITEM;
			this._CODIGO = CODIGO;
			this._BARRAS = BARRAS;
			this._DESCRICAO = DESCRICAO;
			this._UND = UND;
			this._QTD = QTD;
			this._GRADE_QUA = GRADE_QUA;
			this._GRADE_DIS = GRADE_DIS;
			this._CANCELADO = CANCELADO;
			this._TOTALIZADOR = TOTALIZADOR;
			this._VALOR_UNITA = VALOR_UNITA;
			this._DESCONTO = DESCONTO;
			this._ACRESCIMO = ACRESCIMO;
			this._VALOR_TOTAL = VALOR_TOTAL;
			this._ST = ST;
			this._ALIQUOTA = ALIQUOTA;
			this._CHAVE = CHAVE;
            this._TITULO = TITULO;
		}
		#endregion

		#region Propriedades Get/Set

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public DateTime DATA_DAV
		{
			get { return _DATA_DAV; }
			set { _DATA_DAV = value; }
		}

		public string CLIENTE
		{
			get { return _CLIENTE; }
			set { _CLIENTE = value; }
		}

		public int ITEM
		{
			get { return _ITEM; }
			set { _ITEM = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public string BARRAS
		{
			get { return _BARRAS; }
			set { _BARRAS = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string UND
		{
			get { return _UND; }
			set { _UND = value; }
		}

		public decimal? QTD
		{
			get { return _QTD; }
			set { _QTD = value; }
		}

		public string GRADE_QUA
		{
			get { return _GRADE_QUA; }
			set { _GRADE_QUA = value; }
		}

		public string GRADE_DIS
		{
			get { return _GRADE_DIS; }
			set { _GRADE_DIS = value; }
		}

        public int? CANCELADO
		{
			get { return _CANCELADO; }
			set { _CANCELADO = value; }
		}

		public string TOTALIZADOR
		{
			get { return _TOTALIZADOR; }
			set { _TOTALIZADOR = value; }
		}

		public decimal? VALOR_UNITA
		{
			get { return _VALOR_UNITA; }
			set { _VALOR_UNITA = value; }
		}

		public decimal? DESCONTO
		{
			get { return _DESCONTO; }
			set { _DESCONTO = value; }
		}

		public decimal? ACRESCIMO
		{
			get { return _ACRESCIMO; }
			set { _ACRESCIMO = value; }
		}

		public decimal? VALOR_TOTAL
		{
			get { return _VALOR_TOTAL; }
			set { _VALOR_TOTAL = value; }
		}

		public string ST
		{
			get { return _ST; }
			set { _ST = value; }
		}

		public string ALIQUOTA
		{
			get { return _ALIQUOTA; }
			set { _ALIQUOTA = value; }
		}

		public string CHAVE
		{
			get { return _CHAVE; }
			set { _CHAVE = value; }
		}

        public string TITULO
		{
            get { return _TITULO; }
            set { _TITULO = value; }
		}

        
		#endregion
	}
}
