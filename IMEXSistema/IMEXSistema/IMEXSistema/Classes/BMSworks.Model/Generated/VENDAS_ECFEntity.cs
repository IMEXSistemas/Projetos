using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VENDAS_ECFEntity
	{
		private string _CUPOM;
		private int? _N_CAIXA;
		private DateTime? _DATA;
		private TimeSpan? _HORA;
		private int? _OPERADOR;
		private int? _CLIENTE;
		private int? _VENDEDOR;
		private decimal? _COMISSAO;
		private decimal? _ACRESCIMO;
		private decimal? _DESCONTO;
		private decimal? _TOT_ISS;
		private decimal? _TOT_ISS_D;
		private decimal? _TOT_SERVICOS;
		private decimal? _TOT_ICMS;
		private decimal? _TOT_PRODUTOS;
		private decimal? _TOT_VENDA;
		private int _CANCELADO;
		private int? _SEQUENCIA;
		private decimal? _CONVENIO;
		private string _OBS;

		#region Construtores

		//Construtor default
		public VENDAS_ECFEntity() {
			this._N_CAIXA = null;
			this._DATA = null;
			this._HORA = null;
			this._OPERADOR = null;
			this._CLIENTE = null;
			this._VENDEDOR = null;
			this._COMISSAO = null;
			this._ACRESCIMO = null;
			this._DESCONTO = null;
			this._TOT_ISS = null;
			this._TOT_ISS_D = null;
			this._TOT_SERVICOS = null;
			this._TOT_ICMS = null;
			this._TOT_PRODUTOS = null;
			this._TOT_VENDA = null;
			this._SEQUENCIA = null;
			this._CONVENIO = null;
		}

		public VENDAS_ECFEntity(string CUPOM, int? N_CAIXA, DateTime? DATA, TimeSpan? HORA, int? OPERADOR, int? CLIENTE, int? VENDEDOR, decimal? COMISSAO, decimal? ACRESCIMO, decimal? DESCONTO, decimal? TOT_ISS, decimal? TOT_ISS_D, decimal? TOT_SERVICOS, decimal? TOT_ICMS, decimal? TOT_PRODUTOS, decimal? TOT_VENDA, short CANCELADO, int? SEQUENCIA, decimal? CONVENIO, string OBS) {

			this._CUPOM = CUPOM;
			this._N_CAIXA = N_CAIXA;
			this._DATA = DATA;
			this._HORA = HORA;
			this._OPERADOR = OPERADOR;
			this._CLIENTE = CLIENTE;
			this._VENDEDOR = VENDEDOR;
			this._COMISSAO = COMISSAO;
			this._ACRESCIMO = ACRESCIMO;
			this._DESCONTO = DESCONTO;
			this._TOT_ISS = TOT_ISS;
			this._TOT_ISS_D = TOT_ISS_D;
			this._TOT_SERVICOS = TOT_SERVICOS;
			this._TOT_ICMS = TOT_ICMS;
			this._TOT_PRODUTOS = TOT_PRODUTOS;
			this._TOT_VENDA = TOT_VENDA;
			this._CANCELADO = CANCELADO;
			this._SEQUENCIA = SEQUENCIA;
			this._CONVENIO = CONVENIO;
			this._OBS = OBS;
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

		public int? OPERADOR
		{
			get { return _OPERADOR; }
			set { _OPERADOR = value; }
		}

		public int? CLIENTE
		{
			get { return _CLIENTE; }
			set { _CLIENTE = value; }
		}

		public int? VENDEDOR
		{
			get { return _VENDEDOR; }
			set { _VENDEDOR = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public decimal? ACRESCIMO
		{
			get { return _ACRESCIMO; }
			set { _ACRESCIMO = value; }
		}

		public decimal? DESCONTO
		{
			get { return _DESCONTO; }
			set { _DESCONTO = value; }
		}

		public decimal? TOT_ISS
		{
			get { return _TOT_ISS; }
			set { _TOT_ISS = value; }
		}

		public decimal? TOT_ISS_D
		{
			get { return _TOT_ISS_D; }
			set { _TOT_ISS_D = value; }
		}

		public decimal? TOT_SERVICOS
		{
			get { return _TOT_SERVICOS; }
			set { _TOT_SERVICOS = value; }
		}

		public decimal? TOT_ICMS
		{
			get { return _TOT_ICMS; }
			set { _TOT_ICMS = value; }
		}

		public decimal? TOT_PRODUTOS
		{
			get { return _TOT_PRODUTOS; }
			set { _TOT_PRODUTOS = value; }
		}

		public decimal? TOT_VENDA
		{
			get { return _TOT_VENDA; }
			set { _TOT_VENDA = value; }
		}

		public int CANCELADO
		{
			get { return _CANCELADO; }
			set { _CANCELADO = value; }
		}

		public int? SEQUENCIA
		{
			get { return _SEQUENCIA; }
			set { _SEQUENCIA = value; }
		}

		public decimal? CONVENIO
		{
			get { return _CONVENIO; }
			set { _CONVENIO = value; }
		}

		public string OBS
		{
			get { return _OBS; }
			set { _OBS = value; }
		}

		#endregion
	}
}
