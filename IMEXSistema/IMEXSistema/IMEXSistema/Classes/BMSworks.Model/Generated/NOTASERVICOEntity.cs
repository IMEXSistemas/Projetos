using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class NOTASERVICOEntity
	{
		private int _IDNOTASERVICO;
		private int? _NPS;
		private DateTime? _DATAEMISSAO;
		private int? _CODNATUREZA;
		private int? _REGIMETRIBUTACAO;
		private int? _IDCLIENTE;
		private decimal? _DEDUCAO;
		private decimal? _PIS;
		private decimal? _COFINS;
		private decimal? _INSS;
		private decimal? _IMPOSTORENDA;
		private decimal? _CONTRIBUICAOSOCIAL;
		private decimal? _ISS;
		private decimal? _ISSRETIDO;
		private decimal? _OUTRASRETENCOES;
		private decimal? _BASECALCULO;
		private decimal? _ALIQSERVICO;
		private decimal? _DESCONTO;
		private string _OBSERVACAO;
		private string _DESCRICAODETALSERVICO;
		private decimal? _TOTALSERVICO;
		private int? _IDCENTROCUSTO;
		private int? _IDFORMAPAGTO;
		private int? _IDLOCALCOBRANCA;

		#region Construtores

		//Construtor default
		public NOTASERVICOEntity() {
			this._NPS = null;
			this._DATAEMISSAO = null;
			this._CODNATUREZA = null;
			this._REGIMETRIBUTACAO = null;
			this._IDCLIENTE = null;
			this._DEDUCAO = null;
			this._PIS = null;
			this._COFINS = null;
			this._INSS = null;
			this._IMPOSTORENDA = null;
			this._CONTRIBUICAOSOCIAL = null;
			this._ISS = null;
			this._ISSRETIDO = null;
			this._OUTRASRETENCOES = null;
			this._BASECALCULO = null;
			this._ALIQSERVICO = null;
			this._DESCONTO = null;
			this._TOTALSERVICO = null;
			this._IDCENTROCUSTO = null;
			this._IDFORMAPAGTO = null;
			this._IDLOCALCOBRANCA = null;
		}

		public NOTASERVICOEntity(int IDNOTASERVICO, int? NPS, DateTime? DATAEMISSAO, int? CODNATUREZA, int? REGIMETRIBUTACAO, int? IDCLIENTE, decimal? DEDUCAO, decimal? PIS, decimal? COFINS, decimal? INSS, decimal? IMPOSTORENDA, decimal? CONTRIBUICAOSOCIAL, decimal? ISS, decimal? ISSRETIDO, decimal? OUTRASRETENCOES, decimal? BASECALCULO, decimal? ALIQSERVICO, decimal? DESCONTO, string OBSERVACAO, string DESCRICAODETALSERVICO, decimal? TOTALSERVICO, int? IDCENTROCUSTO, int? IDFORMAPAGTO, int? IDLOCALCOBRANCA) {

			this._IDNOTASERVICO = IDNOTASERVICO;
			this._NPS = NPS;
			this._DATAEMISSAO = DATAEMISSAO;
			this._CODNATUREZA = CODNATUREZA;
			this._REGIMETRIBUTACAO = REGIMETRIBUTACAO;
			this._IDCLIENTE = IDCLIENTE;
			this._DEDUCAO = DEDUCAO;
			this._PIS = PIS;
			this._COFINS = COFINS;
			this._INSS = INSS;
			this._IMPOSTORENDA = IMPOSTORENDA;
			this._CONTRIBUICAOSOCIAL = CONTRIBUICAOSOCIAL;
			this._ISS = ISS;
			this._ISSRETIDO = ISSRETIDO;
			this._OUTRASRETENCOES = OUTRASRETENCOES;
			this._BASECALCULO = BASECALCULO;
			this._ALIQSERVICO = ALIQSERVICO;
			this._DESCONTO = DESCONTO;
			this._OBSERVACAO = OBSERVACAO;
			this._DESCRICAODETALSERVICO = DESCRICAODETALSERVICO;
			this._TOTALSERVICO = TOTALSERVICO;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDNOTASERVICO
		{
			get { return _IDNOTASERVICO; }
			set { _IDNOTASERVICO = value; }
		}

		public int? NPS
		{
			get { return _NPS; }
			set { _NPS = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public int? CODNATUREZA
		{
			get { return _CODNATUREZA; }
			set { _CODNATUREZA = value; }
		}

		public int? REGIMETRIBUTACAO
		{
			get { return _REGIMETRIBUTACAO; }
			set { _REGIMETRIBUTACAO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public decimal? DEDUCAO
		{
			get { return _DEDUCAO; }
			set { _DEDUCAO = value; }
		}

		public decimal? PIS
		{
			get { return _PIS; }
			set { _PIS = value; }
		}

		public decimal? COFINS
		{
			get { return _COFINS; }
			set { _COFINS = value; }
		}

		public decimal? INSS
		{
			get { return _INSS; }
			set { _INSS = value; }
		}

		public decimal? IMPOSTORENDA
		{
			get { return _IMPOSTORENDA; }
			set { _IMPOSTORENDA = value; }
		}

		public decimal? CONTRIBUICAOSOCIAL
		{
			get { return _CONTRIBUICAOSOCIAL; }
			set { _CONTRIBUICAOSOCIAL = value; }
		}

		public decimal? ISS
		{
			get { return _ISS; }
			set { _ISS = value; }
		}

		public decimal? ISSRETIDO
		{
			get { return _ISSRETIDO; }
			set { _ISSRETIDO = value; }
		}

		public decimal? OUTRASRETENCOES
		{
			get { return _OUTRASRETENCOES; }
			set { _OUTRASRETENCOES = value; }
		}

		public decimal? BASECALCULO
		{
			get { return _BASECALCULO; }
			set { _BASECALCULO = value; }
		}

		public decimal? ALIQSERVICO
		{
			get { return _ALIQSERVICO; }
			set { _ALIQSERVICO = value; }
		}

		public decimal? DESCONTO
		{
			get { return _DESCONTO; }
			set { _DESCONTO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string DESCRICAODETALSERVICO
		{
			get { return _DESCRICAODETALSERVICO; }
			set { _DESCRICAODETALSERVICO = value; }
		}

		public decimal? TOTALSERVICO
		{
			get { return _TOTALSERVICO; }
			set { _TOTALSERVICO = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
		}

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		#endregion
	}
}
