using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITEVENDASEntity
	{
		private string _NOTA;
		private string _MODELO;
		private string _SERIE;
		private DateTime? _DATA_EMISSAO;
		private string _LOJA;
		private string _CAIXA;
		private int? _ITEM;
		private string _CODIGO;
		private string _BARRAS;
		private string _DESCRICAO;
		private string _COD_CFOP;
		private string _CFOP;
		private string _CF;
		private string _ST;
		private string _UND;
		private decimal? _QTD;
		private string _GRADE_QUA;
		private decimal? _VALOR_UNITA;
		private decimal? _VALOR_TOTAL;
		private decimal? _VALOR_CUSTO;
		private decimal? _VALOR_LISTA;
		private decimal? _VALOR_DESCO;
		private decimal? _VALOR_FRETE;
		private decimal? _VALOR_SEGUR;
		private decimal? _VALOR_OUTDE;
		private decimal? _ALIQ_ICMS;
		private decimal? _BASE_CAL_ICMS;
		private decimal? _VALOR_ICMS;
		private decimal? _BASE_CAL_ICMS_SUB;
		private decimal? _VALOR_ICMS_SUB;
		private decimal? _BASE_CAL_SIMPLES;
		private string _PIS_ST;
		private decimal? _PIS_BASE;
		private decimal? _PIS_ALIQ;
		private decimal? _PIS_TOT;
		private decimal? _PIS_SUB_BASE;
		private decimal? _PIS_SUB_ALIQ;
		private decimal? _PIS_SUB_TOT;
		private string _COFINS_ST;
		private decimal? _COFINS_BASE;
		private decimal? _COFINS_ALIQ;
		private decimal? _COFINS_TOT;
		private decimal? _COFINS_SUB_BASE;
		private decimal? _COFINS_SUB_ALIQ;
		private decimal? _COFINS_SUB_TOT;
		private decimal? _ALIQ_IPI;
		private decimal? _VALOR_TOT_PRO;
		private int? _CANCELADA;
        private int? _LIVRE;
		private string _FRETE_CONTA_1;
		private DateTime? _DATA_SAIDA;
		private TimeSpan? _HORA_SAIDA;

		#region Construtores

		//Construtor default
		public ITEVENDASEntity() {
            this._DATA_EMISSAO = null;
			this._QTD = null;
			this._VALOR_UNITA = null;
			this._VALOR_TOTAL = null;
			this._VALOR_CUSTO = null;
			this._VALOR_LISTA = null;
			this._VALOR_DESCO = null;
			this._VALOR_FRETE = null;
			this._VALOR_SEGUR = null;
			this._VALOR_OUTDE = null;
			this._ALIQ_ICMS = null;
			this._BASE_CAL_ICMS = null;
			this._VALOR_ICMS = null;
			this._BASE_CAL_ICMS_SUB = null;
			this._VALOR_ICMS_SUB = null;
			this._BASE_CAL_SIMPLES = null;
			this._PIS_BASE = null;
			this._PIS_ALIQ = null;
			this._PIS_TOT = null;
			this._PIS_SUB_BASE = null;
			this._PIS_SUB_ALIQ = null;
			this._PIS_SUB_TOT = null;
			this._COFINS_BASE = null;
			this._COFINS_ALIQ = null;
			this._COFINS_TOT = null;
			this._COFINS_SUB_BASE = null;
			this._COFINS_SUB_ALIQ = null;
			this._COFINS_SUB_TOT = null;
			this._ALIQ_IPI = null;
			this._VALOR_TOT_PRO = null;
			this._CANCELADA = null;
			this._LIVRE = null;
			this._DATA_SAIDA = null;
			this._HORA_SAIDA = null;
		}

        public ITEVENDASEntity(string NOTA, string MODELO, string SERIE, DateTime? DATA_EMISSAO, string LOJA, string CAIXA, int? ITEM, string CODIGO, string BARRAS, string DESCRICAO, string COD_CFOP, string CFOP, string CF, string ST, string UND, decimal? QTD, string GRADE_QUA, decimal? VALOR_UNITA, decimal? VALOR_TOTAL, decimal? VALOR_CUSTO, decimal? VALOR_LISTA, decimal? VALOR_DESCO, decimal? VALOR_FRETE, decimal? VALOR_SEGUR, decimal? VALOR_OUTDE, decimal? ALIQ_ICMS, decimal? BASE_CAL_ICMS, decimal? VALOR_ICMS, decimal? BASE_CAL_ICMS_SUB, decimal? VALOR_ICMS_SUB, decimal? BASE_CAL_SIMPLES, string PIS_ST, decimal? PIS_BASE, decimal? PIS_ALIQ, decimal? PIS_TOT, decimal? PIS_SUB_BASE, decimal? PIS_SUB_ALIQ, decimal? PIS_SUB_TOT, string COFINS_ST, decimal? COFINS_BASE, decimal? COFINS_ALIQ, decimal? COFINS_TOT, decimal? COFINS_SUB_BASE, decimal? COFINS_SUB_ALIQ, decimal? COFINS_SUB_TOT, decimal? ALIQ_IPI, decimal? VALOR_TOT_PRO, int? CANCELADA, int? LIVRE, string FRETE_CONTA_1, DateTime? DATA_SAIDA, TimeSpan? HORA_SAIDA)
        {

			this._NOTA = NOTA;
			this._MODELO = MODELO;
			this._SERIE = SERIE;
			this._DATA_EMISSAO = DATA_EMISSAO;
			this._LOJA = LOJA;
			this._CAIXA = CAIXA;
			this._ITEM = ITEM;
			this._CODIGO = CODIGO;
			this._BARRAS = BARRAS;
			this._DESCRICAO = DESCRICAO;
			this._COD_CFOP = COD_CFOP;
			this._CFOP = CFOP;
			this._CF = CF;
			this._ST = ST;
			this._UND = UND;
			this._QTD = QTD;
			this._GRADE_QUA = GRADE_QUA;
			this._VALOR_UNITA = VALOR_UNITA;
			this._VALOR_TOTAL = VALOR_TOTAL;
			this._VALOR_CUSTO = VALOR_CUSTO;
			this._VALOR_LISTA = VALOR_LISTA;
			this._VALOR_DESCO = VALOR_DESCO;
			this._VALOR_FRETE = VALOR_FRETE;
			this._VALOR_SEGUR = VALOR_SEGUR;
			this._VALOR_OUTDE = VALOR_OUTDE;
			this._ALIQ_ICMS = ALIQ_ICMS;
			this._BASE_CAL_ICMS = BASE_CAL_ICMS;
			this._VALOR_ICMS = VALOR_ICMS;
			this._BASE_CAL_ICMS_SUB = BASE_CAL_ICMS_SUB;
			this._VALOR_ICMS_SUB = VALOR_ICMS_SUB;
			this._BASE_CAL_SIMPLES = BASE_CAL_SIMPLES;
			this._PIS_ST = PIS_ST;
			this._PIS_BASE = PIS_BASE;
			this._PIS_ALIQ = PIS_ALIQ;
			this._PIS_TOT = PIS_TOT;
			this._PIS_SUB_BASE = PIS_SUB_BASE;
			this._PIS_SUB_ALIQ = PIS_SUB_ALIQ;
			this._PIS_SUB_TOT = PIS_SUB_TOT;
			this._COFINS_ST = COFINS_ST;
			this._COFINS_BASE = COFINS_BASE;
			this._COFINS_ALIQ = COFINS_ALIQ;
			this._COFINS_TOT = COFINS_TOT;
			this._COFINS_SUB_BASE = COFINS_SUB_BASE;
			this._COFINS_SUB_ALIQ = COFINS_SUB_ALIQ;
			this._COFINS_SUB_TOT = COFINS_SUB_TOT;
			this._ALIQ_IPI = ALIQ_IPI;
			this._VALOR_TOT_PRO = VALOR_TOT_PRO;
			this._CANCELADA = CANCELADA;
			this._LIVRE = LIVRE;
			this._FRETE_CONTA_1 = FRETE_CONTA_1;
			this._DATA_SAIDA = DATA_SAIDA;
			this._HORA_SAIDA = HORA_SAIDA;
		}
		#endregion

		#region Propriedades Get/Set

		public string NOTA
		{
			get { return _NOTA; }
			set { _NOTA = value; }
		}

		public string MODELO
		{
			get { return _MODELO; }
			set { _MODELO = value; }
		}

		public string SERIE
		{
			get { return _SERIE; }
			set { _SERIE = value; }
		}

		public DateTime? DATA_EMISSAO
		{
			get { return _DATA_EMISSAO; }
			set { _DATA_EMISSAO = value; }
		}

		public string LOJA
		{
			get { return _LOJA; }
			set { _LOJA = value; }
		}

		public string CAIXA
		{
			get { return _CAIXA; }
			set { _CAIXA = value; }
		}

		public int? ITEM
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

		public string COD_CFOP
		{
			get { return _COD_CFOP; }
			set { _COD_CFOP = value; }
		}

		public string CFOP
		{
			get { return _CFOP; }
			set { _CFOP = value; }
		}

		public string CF
		{
			get { return _CF; }
			set { _CF = value; }
		}

		public string ST
		{
			get { return _ST; }
			set { _ST = value; }
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

		public decimal? VALOR_UNITA
		{
			get { return _VALOR_UNITA; }
			set { _VALOR_UNITA = value; }
		}

		public decimal? VALOR_TOTAL
		{
			get { return _VALOR_TOTAL; }
			set { _VALOR_TOTAL = value; }
		}

		public decimal? VALOR_CUSTO
		{
			get { return _VALOR_CUSTO; }
			set { _VALOR_CUSTO = value; }
		}

		public decimal? VALOR_LISTA
		{
			get { return _VALOR_LISTA; }
			set { _VALOR_LISTA = value; }
		}

		public decimal? VALOR_DESCO
		{
			get { return _VALOR_DESCO; }
			set { _VALOR_DESCO = value; }
		}

		public decimal? VALOR_FRETE
		{
			get { return _VALOR_FRETE; }
			set { _VALOR_FRETE = value; }
		}

		public decimal? VALOR_SEGUR
		{
			get { return _VALOR_SEGUR; }
			set { _VALOR_SEGUR = value; }
		}

		public decimal? VALOR_OUTDE
		{
			get { return _VALOR_OUTDE; }
			set { _VALOR_OUTDE = value; }
		}

		public decimal? ALIQ_ICMS
		{
			get { return _ALIQ_ICMS; }
			set { _ALIQ_ICMS = value; }
		}

		public decimal? BASE_CAL_ICMS
		{
			get { return _BASE_CAL_ICMS; }
			set { _BASE_CAL_ICMS = value; }
		}

		public decimal? VALOR_ICMS
		{
			get { return _VALOR_ICMS; }
			set { _VALOR_ICMS = value; }
		}

		public decimal? BASE_CAL_ICMS_SUB
		{
			get { return _BASE_CAL_ICMS_SUB; }
			set { _BASE_CAL_ICMS_SUB = value; }
		}

		public decimal? VALOR_ICMS_SUB
		{
			get { return _VALOR_ICMS_SUB; }
			set { _VALOR_ICMS_SUB = value; }
		}

		public decimal? BASE_CAL_SIMPLES
		{
			get { return _BASE_CAL_SIMPLES; }
			set { _BASE_CAL_SIMPLES = value; }
		}

		public string PIS_ST
		{
			get { return _PIS_ST; }
			set { _PIS_ST = value; }
		}

		public decimal? PIS_BASE
		{
			get { return _PIS_BASE; }
			set { _PIS_BASE = value; }
		}

		public decimal? PIS_ALIQ
		{
			get { return _PIS_ALIQ; }
			set { _PIS_ALIQ = value; }
		}

		public decimal? PIS_TOT
		{
			get { return _PIS_TOT; }
			set { _PIS_TOT = value; }
		}

		public decimal? PIS_SUB_BASE
		{
			get { return _PIS_SUB_BASE; }
			set { _PIS_SUB_BASE = value; }
		}

		public decimal? PIS_SUB_ALIQ
		{
			get { return _PIS_SUB_ALIQ; }
			set { _PIS_SUB_ALIQ = value; }
		}

		public decimal? PIS_SUB_TOT
		{
			get { return _PIS_SUB_TOT; }
			set { _PIS_SUB_TOT = value; }
		}

		public string COFINS_ST
		{
			get { return _COFINS_ST; }
			set { _COFINS_ST = value; }
		}

		public decimal? COFINS_BASE
		{
			get { return _COFINS_BASE; }
			set { _COFINS_BASE = value; }
		}

		public decimal? COFINS_ALIQ
		{
			get { return _COFINS_ALIQ; }
			set { _COFINS_ALIQ = value; }
		}

		public decimal? COFINS_TOT
		{
			get { return _COFINS_TOT; }
			set { _COFINS_TOT = value; }
		}

		public decimal? COFINS_SUB_BASE
		{
			get { return _COFINS_SUB_BASE; }
			set { _COFINS_SUB_BASE = value; }
		}

		public decimal? COFINS_SUB_ALIQ
		{
			get { return _COFINS_SUB_ALIQ; }
			set { _COFINS_SUB_ALIQ = value; }
		}

		public decimal? COFINS_SUB_TOT
		{
			get { return _COFINS_SUB_TOT; }
			set { _COFINS_SUB_TOT = value; }
		}

		public decimal? ALIQ_IPI
		{
			get { return _ALIQ_IPI; }
			set { _ALIQ_IPI = value; }
		}

		public decimal? VALOR_TOT_PRO
		{
			get { return _VALOR_TOT_PRO; }
			set { _VALOR_TOT_PRO = value; }
		}

        public int? CANCELADA
		{
			get { return _CANCELADA; }
			set { _CANCELADA = value; }
		}

        public int? LIVRE
		{
			get { return _LIVRE; }
			set { _LIVRE = value; }
		}

		public string FRETE_CONTA_1
		{
			get { return _FRETE_CONTA_1; }
			set { _FRETE_CONTA_1 = value; }
		}

		public DateTime? DATA_SAIDA
		{
			get { return _DATA_SAIDA; }
			set { _DATA_SAIDA = value; }
		}

		public TimeSpan? HORA_SAIDA
		{
			get { return _HORA_SAIDA; }
			set { _HORA_SAIDA = value; }
		}

		#endregion
	}
}
