using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class NOTAFISCALEntity
	{
		private int _IDNOTAFISCAL;
		private string _NOTAFISCAL;
		private string _SERIE;
		private int? _IDCLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTSAIDA;
		private string _HORASAIDA;
		private int? _IDTIPOMOVIM;
		private int? _IDCFOP;
		private string _INSCESTSTRIB;
		private decimal? _BASECALCICMS;
		private decimal? _VALORICMS;
		private decimal? _BASECALCICMSLSUB;
		private decimal? _VALORICMSSUB;
		private decimal? _VALORFRETE;
		private decimal? _VALORSEGURO;
		private decimal? _OUTRADESPES;
		private decimal? _TOTALIPI;
		private decimal? _TOTALPRODUTOS;
		private decimal? _TOTALNOTA;
		private int? _IDVENDEDOR;
		private decimal? _VALORCOMISSAO;
		private int? _IDTRANSPORTES;
		private string _PLACA;
		private int? _IDUF;
		private decimal? _QUANT;
		private string _ESPECIE;
		private string _MARCA;
		private string _NUMEROS;
		private decimal? _PESOBRUTO;
		private decimal? _PESOLIQUIDO;
		private string _INFOCOMPLEM;
		private int? _IDCENTROCUSTO;
		private int? _IDFORMAPAGTO;
		private int? _IDLOCALCOBRANCA;
		private int? _IDSTATUS;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _FRETE;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private decimal? _PORCACRESCIMO;
		private decimal? _VALORACRESCIMO;

		#region Construtores

		//Construtor default
		public NOTAFISCALEntity() {
			this._IDCLIENTE = null;
			this._DTEMISSAO = null;
			this._DTSAIDA = null;
			this._IDTIPOMOVIM = null;
			this._IDCFOP = null;
			this._BASECALCICMS = null;
			this._VALORICMS = null;
			this._BASECALCICMSLSUB = null;
			this._VALORICMSSUB = null;
			this._VALORFRETE = null;
			this._VALORSEGURO = null;
			this._OUTRADESPES = null;
			this._TOTALIPI = null;
			this._TOTALPRODUTOS = null;
			this._TOTALNOTA = null;
			this._IDVENDEDOR = null;
			this._VALORCOMISSAO = null;
			this._IDTRANSPORTES = null;
			this._IDUF = null;
			this._QUANT = null;
			this._PESOBRUTO = null;
			this._PESOLIQUIDO = null;
			this._IDCENTROCUSTO = null;
			this._IDFORMAPAGTO = null;
			this._IDLOCALCOBRANCA = null;
			this._IDSTATUS = null;
			this._VALORPAGO = null;
			this._VALORDEVEDOR = null;
			this._FRETE = null;
			this._PORCDESCONTO = null;
			this._VALORDESCONTO = null;
			this._PORCACRESCIMO = null;
			this._VALORACRESCIMO = null;
		}

		public NOTAFISCALEntity(int IDNOTAFISCAL, string NOTAFISCAL, string SERIE, int? IDCLIENTE, DateTime? DTEMISSAO, DateTime? DTSAIDA, string HORASAIDA, int? IDTIPOMOVIM, int? IDCFOP, string INSCESTSTRIB, decimal? BASECALCICMS, decimal? VALORICMS, decimal? BASECALCICMSLSUB, decimal? VALORICMSSUB, decimal? VALORFRETE, decimal? VALORSEGURO, decimal? OUTRADESPES, decimal? TOTALIPI, decimal? TOTALPRODUTOS, decimal? TOTALNOTA, int? IDVENDEDOR, decimal? VALORCOMISSAO, int? IDTRANSPORTES, string PLACA, int? IDUF, decimal? QUANT, string ESPECIE, string MARCA, string NUMEROS, decimal? PESOBRUTO, decimal? PESOLIQUIDO, string INFOCOMPLEM, int? IDCENTROCUSTO, int? IDFORMAPAGTO, int? IDLOCALCOBRANCA, int? IDSTATUS, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? FRETE, decimal? PORCDESCONTO, decimal? VALORDESCONTO, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO) {

			this._IDNOTAFISCAL = IDNOTAFISCAL;
			this._NOTAFISCAL = NOTAFISCAL;
			this._SERIE = SERIE;
			this._IDCLIENTE = IDCLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTSAIDA = DTSAIDA;
			this._HORASAIDA = HORASAIDA;
			this._IDTIPOMOVIM = IDTIPOMOVIM;
			this._IDCFOP = IDCFOP;
			this._INSCESTSTRIB = INSCESTSTRIB;
			this._BASECALCICMS = BASECALCICMS;
			this._VALORICMS = VALORICMS;
			this._BASECALCICMSLSUB = BASECALCICMSLSUB;
			this._VALORICMSSUB = VALORICMSSUB;
			this._VALORFRETE = VALORFRETE;
			this._VALORSEGURO = VALORSEGURO;
			this._OUTRADESPES = OUTRADESPES;
			this._TOTALIPI = TOTALIPI;
			this._TOTALPRODUTOS = TOTALPRODUTOS;
			this._TOTALNOTA = TOTALNOTA;
			this._IDVENDEDOR = IDVENDEDOR;
			this._VALORCOMISSAO = VALORCOMISSAO;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._PLACA = PLACA;
			this._IDUF = IDUF;
			this._QUANT = QUANT;
			this._ESPECIE = ESPECIE;
			this._MARCA = MARCA;
			this._NUMEROS = NUMEROS;
			this._PESOBRUTO = PESOBRUTO;
			this._PESOLIQUIDO = PESOLIQUIDO;
			this._INFOCOMPLEM = INFOCOMPLEM;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._IDSTATUS = IDSTATUS;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._FRETE = FRETE;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._VALORACRESCIMO = VALORACRESCIMO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDNOTAFISCAL
		{
			get { return _IDNOTAFISCAL; }
			set { _IDNOTAFISCAL = value; }
		}

		public string NOTAFISCAL
		{
			get { return _NOTAFISCAL; }
			set { _NOTAFISCAL = value; }
		}

		public string SERIE
		{
			get { return _SERIE; }
			set { _SERIE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public DateTime? DTSAIDA
		{
			get { return _DTSAIDA; }
			set { _DTSAIDA = value; }
		}

		public string HORASAIDA
		{
			get { return _HORASAIDA; }
			set { _HORASAIDA = value; }
		}

		public int? IDTIPOMOVIM
		{
			get { return _IDTIPOMOVIM; }
			set { _IDTIPOMOVIM = value; }
		}

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string INSCESTSTRIB
		{
			get { return _INSCESTSTRIB; }
			set { _INSCESTSTRIB = value; }
		}

		public decimal? BASECALCICMS
		{
			get { return _BASECALCICMS; }
			set { _BASECALCICMS = value; }
		}

		public decimal? VALORICMS
		{
			get { return _VALORICMS; }
			set { _VALORICMS = value; }
		}

		public decimal? BASECALCICMSLSUB
		{
			get { return _BASECALCICMSLSUB; }
			set { _BASECALCICMSLSUB = value; }
		}

		public decimal? VALORICMSSUB
		{
			get { return _VALORICMSSUB; }
			set { _VALORICMSSUB = value; }
		}

		public decimal? VALORFRETE
		{
			get { return _VALORFRETE; }
			set { _VALORFRETE = value; }
		}

		public decimal? VALORSEGURO
		{
			get { return _VALORSEGURO; }
			set { _VALORSEGURO = value; }
		}

		public decimal? OUTRADESPES
		{
			get { return _OUTRADESPES; }
			set { _OUTRADESPES = value; }
		}

		public decimal? TOTALIPI
		{
			get { return _TOTALIPI; }
			set { _TOTALIPI = value; }
		}

		public decimal? TOTALPRODUTOS
		{
			get { return _TOTALPRODUTOS; }
			set { _TOTALPRODUTOS = value; }
		}

		public decimal? TOTALNOTA
		{
			get { return _TOTALNOTA; }
			set { _TOTALNOTA = value; }
		}

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public decimal? VALORCOMISSAO
		{
			get { return _VALORCOMISSAO; }
			set { _VALORCOMISSAO = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
		}

		public string PLACA
		{
			get { return _PLACA; }
			set { _PLACA = value; }
		}

		public int? IDUF
		{
			get { return _IDUF; }
			set { _IDUF = value; }
		}

		public decimal? QUANT
		{
			get { return _QUANT; }
			set { _QUANT = value; }
		}

		public string ESPECIE
		{
			get { return _ESPECIE; }
			set { _ESPECIE = value; }
		}

		public string MARCA
		{
			get { return _MARCA; }
			set { _MARCA = value; }
		}

		public string NUMEROS
		{
			get { return _NUMEROS; }
			set { _NUMEROS = value; }
		}

		public decimal? PESOBRUTO
		{
			get { return _PESOBRUTO; }
			set { _PESOBRUTO = value; }
		}

		public decimal? PESOLIQUIDO
		{
			get { return _PESOLIQUIDO; }
			set { _PESOLIQUIDO = value; }
		}

		public string INFOCOMPLEM
		{
			get { return _INFOCOMPLEM; }
			set { _INFOCOMPLEM = value; }
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

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public decimal? VALORPAGO
		{
			get { return _VALORPAGO; }
			set { _VALORPAGO = value; }
		}

		public decimal? VALORDEVEDOR
		{
			get { return _VALORDEVEDOR; }
			set { _VALORDEVEDOR = value; }
		}

		public int? FRETE
		{
			get { return _FRETE; }
			set { _FRETE = value; }
		}

		public decimal? PORCDESCONTO
		{
			get { return _PORCDESCONTO; }
			set { _PORCDESCONTO = value; }
		}

		public decimal? VALORDESCONTO
		{
			get { return _VALORDESCONTO; }
			set { _VALORDESCONTO = value; }
		}

		public decimal? PORCACRESCIMO
		{
			get { return _PORCACRESCIMO; }
			set { _PORCACRESCIMO = value; }
		}

		public decimal? VALORACRESCIMO
		{
			get { return _VALORACRESCIMO; }
			set { _VALORACRESCIMO = value; }
		}

		#endregion
	}
}
