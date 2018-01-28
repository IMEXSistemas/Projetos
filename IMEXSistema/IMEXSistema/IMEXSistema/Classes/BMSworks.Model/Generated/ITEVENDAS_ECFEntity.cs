using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITEVENDAS_ECFEntity
	{
		private string _CUPOM;
		private int? _N_CAIXA;
		private DateTime? _DATA;
		private TimeSpan? _HORA;
		private int _OPERADOR;
		private int _ITEM;
		private int _CODIGO;
		private string _BARRAS;
		private string _DESCRICAO;
		private decimal? _QTD;
		private decimal? _PRECO;
		private string _TRIBUTACAO;
		private decimal? _ICMS;
		private decimal? _ISS;
		private string _UND;
		private string _GRADE_X;
		private string _GRADE_Y;
		private string _GRADE_QUA;
		private string _GRADE_VENDIDA;
		private string _SERIAL;
		private decimal? _DESCONTO;
		private decimal? _ACRESCIMO;
		private decimal? _TOTAL;
		private decimal? _OUTRAS_DESP_ACRE;
        private int _CANCELADO;
		private int _OPERADOR_SUP;
		private string _LOTE;
        private int? _TIPO;
		private string _TABELA_PRECO;
		private string _PIS_ST;
		private decimal? _PIS_VALOR_BC;
		private decimal? _PIS_ALIQ;
		private decimal? _TOT_PIS;
		private string _COFINS_ST;
		private decimal? _COFINS_VALOR_BC;
		private decimal? _COFINS_ALIQ;
		private decimal? _TOT_COFINS;
		private string _CST_ICMS;
		private decimal? _PRECO_CUSTO;

		#region Construtores

		//Construtor default
		public ITEVENDAS_ECFEntity() {
			this._N_CAIXA = null;
			this._DATA = null;
			this._HORA = null;
			this._QTD = null;
			this._PRECO = null;
			this._ICMS = null;
			this._ISS = null;
			this._DESCONTO = null;
			this._ACRESCIMO = null;
			this._TOTAL = null;
			this._OUTRAS_DESP_ACRE = null;
			this._TIPO = null;
			this._PIS_VALOR_BC = null;
			this._PIS_ALIQ = null;
			this._TOT_PIS = null;
			this._COFINS_VALOR_BC = null;
			this._COFINS_ALIQ = null;
			this._TOT_COFINS = null;
			this._PRECO_CUSTO = null;
		}

        public ITEVENDAS_ECFEntity(string CUPOM, int? N_CAIXA, DateTime? DATA, TimeSpan? HORA, int OPERADOR, int ITEM, int CODIGO, string BARRAS, string DESCRICAO, decimal? QTD, decimal? PRECO, string TRIBUTACAO, decimal? ICMS, decimal? ISS, string UND, string GRADE_X, string GRADE_Y, string GRADE_QUA, string GRADE_VENDIDA, string SERIAL, decimal? DESCONTO, decimal? ACRESCIMO, decimal? TOTAL, decimal? OUTRAS_DESP_ACRE, int CANCELADO, int OPERADOR_SUP, string LOTE, int? TIPO, string TABELA_PRECO, string PIS_ST, decimal? PIS_VALOR_BC, decimal? PIS_ALIQ, decimal? TOT_PIS, string COFINS_ST, decimal? COFINS_VALOR_BC, decimal? COFINS_ALIQ, decimal? TOT_COFINS, string CST_ICMS, decimal? PRECO_CUSTO)
        {

			this._CUPOM = CUPOM;
			this._N_CAIXA = N_CAIXA;
			this._DATA = DATA;
			this._HORA = HORA;
			this._OPERADOR = OPERADOR;
			this._ITEM = ITEM;
			this._CODIGO = CODIGO;
			this._BARRAS = BARRAS;
			this._DESCRICAO = DESCRICAO;
			this._QTD = QTD;
			this._PRECO = PRECO;
			this._TRIBUTACAO = TRIBUTACAO;
			this._ICMS = ICMS;
			this._ISS = ISS;
			this._UND = UND;
			this._GRADE_X = GRADE_X;
			this._GRADE_Y = GRADE_Y;
			this._GRADE_QUA = GRADE_QUA;
			this._GRADE_VENDIDA = GRADE_VENDIDA;
			this._SERIAL = SERIAL;
			this._DESCONTO = DESCONTO;
			this._ACRESCIMO = ACRESCIMO;
			this._TOTAL = TOTAL;
			this._OUTRAS_DESP_ACRE = OUTRAS_DESP_ACRE;
			this._CANCELADO = CANCELADO;
			this._OPERADOR_SUP = OPERADOR_SUP;
			this._LOTE = LOTE;
			this._TIPO = TIPO;
			this._TABELA_PRECO = TABELA_PRECO;
			this._PIS_ST = PIS_ST;
			this._PIS_VALOR_BC = PIS_VALOR_BC;
			this._PIS_ALIQ = PIS_ALIQ;
			this._TOT_PIS = TOT_PIS;
			this._COFINS_ST = COFINS_ST;
			this._COFINS_VALOR_BC = COFINS_VALOR_BC;
			this._COFINS_ALIQ = COFINS_ALIQ;
			this._TOT_COFINS = TOT_COFINS;
			this._CST_ICMS = CST_ICMS;
			this._PRECO_CUSTO = PRECO_CUSTO;
		}
		#endregion

		#region Propriedades Get/Set

		public string CUPOM
		{
			get { return _CUPOM; }
			set { _CUPOM = value; }
		}

		public int? N_CAIXA
		{
			get { return _N_CAIXA; }
			set { _N_CAIXA = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public TimeSpan? HORA
		{
			get { return _HORA; }
			set { _HORA = value; }
		}

		public int OPERADOR
		{
			get { return _OPERADOR; }
			set { _OPERADOR = value; }
		}

		public int ITEM
		{
			get { return _ITEM; }
			set { _ITEM = value; }
		}

		public int CODIGO
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

		public decimal? QTD
		{
			get { return _QTD; }
			set { _QTD = value; }
		}

		public decimal? PRECO
		{
			get { return _PRECO; }
			set { _PRECO = value; }
		}

		public string TRIBUTACAO
		{
			get { return _TRIBUTACAO; }
			set { _TRIBUTACAO = value; }
		}

		public decimal? ICMS
		{
			get { return _ICMS; }
			set { _ICMS = value; }
		}

		public decimal? ISS
		{
			get { return _ISS; }
			set { _ISS = value; }
		}

		public string UND
		{
			get { return _UND; }
			set { _UND = value; }
		}

		public string GRADE_X
		{
			get { return _GRADE_X; }
			set { _GRADE_X = value; }
		}

		public string GRADE_Y
		{
			get { return _GRADE_Y; }
			set { _GRADE_Y = value; }
		}

		public string GRADE_QUA
		{
			get { return _GRADE_QUA; }
			set { _GRADE_QUA = value; }
		}

		public string GRADE_VENDIDA
		{
			get { return _GRADE_VENDIDA; }
			set { _GRADE_VENDIDA = value; }
		}

		public string SERIAL
		{
			get { return _SERIAL; }
			set { _SERIAL = value; }
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

		public decimal? TOTAL
		{
			get { return _TOTAL; }
			set { _TOTAL = value; }
		}

		public decimal? OUTRAS_DESP_ACRE
		{
			get { return _OUTRAS_DESP_ACRE; }
			set { _OUTRAS_DESP_ACRE = value; }
		}

        public int CANCELADO
		{
			get { return _CANCELADO; }
			set { _CANCELADO = value; }
		}

		public int OPERADOR_SUP
		{
			get { return _OPERADOR_SUP; }
			set { _OPERADOR_SUP = value; }
		}

		public string LOTE
		{
			get { return _LOTE; }
			set { _LOTE = value; }
		}

		public int? TIPO
		{
			get { return _TIPO; }
			set { _TIPO = value; }
		}

		public string TABELA_PRECO
		{
			get { return _TABELA_PRECO; }
			set { _TABELA_PRECO = value; }
		}

		public string PIS_ST
		{
			get { return _PIS_ST; }
			set { _PIS_ST = value; }
		}

		public decimal? PIS_VALOR_BC
		{
			get { return _PIS_VALOR_BC; }
			set { _PIS_VALOR_BC = value; }
		}

		public decimal? PIS_ALIQ
		{
			get { return _PIS_ALIQ; }
			set { _PIS_ALIQ = value; }
		}

		public decimal? TOT_PIS
		{
			get { return _TOT_PIS; }
			set { _TOT_PIS = value; }
		}

		public string COFINS_ST
		{
			get { return _COFINS_ST; }
			set { _COFINS_ST = value; }
		}

		public decimal? COFINS_VALOR_BC
		{
			get { return _COFINS_VALOR_BC; }
			set { _COFINS_VALOR_BC = value; }
		}

		public decimal? COFINS_ALIQ
		{
			get { return _COFINS_ALIQ; }
			set { _COFINS_ALIQ = value; }
		}

		public decimal? TOT_COFINS
		{
			get { return _TOT_COFINS; }
			set { _TOT_COFINS = value; }
		}

		public string CST_ICMS
		{
			get { return _CST_ICMS; }
			set { _CST_ICMS = value; }
		}

		public decimal? PRECO_CUSTO
		{
			get { return _PRECO_CUSTO; }
			set { _PRECO_CUSTO = value; }
		}

		#endregion
	}
}
