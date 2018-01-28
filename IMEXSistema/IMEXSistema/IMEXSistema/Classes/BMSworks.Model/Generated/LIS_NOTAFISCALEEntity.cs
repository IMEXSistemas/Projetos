using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_NOTAFISCALEEntity
	{
		private int? _IDNOTAFISCALE;
		private string _NFISCALE;
		private string _SERIE;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTSAIDA;
		private string _HORASAIDA;
		private int? _IDTIPOMOVIM;
		private string _NOMEMOVIMESTOQUE;
		private int? _IDCFOP;
		private string _DESCCFOP;
		private string _CODCFOP;
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
		private string _NOMEVENDEDOR;
		private decimal? _VALORCOMISSAO;
		private int? _IDTRANSPORTES;
		private string _NOMETRANSPORTADORA;
		private string _PLACA;
		private string _UFTRANSPORTE;
		private decimal? _QUANT;
		private string _ESPECIE;
		private string _MARCANFE;
		private string _NUMEROS;
		private decimal? _PESOBRUTO;
		private decimal? _PESOLIQUIDO;
		private string _INFOCOMPLEM;
		private int? _IDCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _DESCCENTROCUSTO;
		private int? _IDFORMAPAGTO;
		private string _NOMEFORMAPAGTO;
		private int? _IDLOCALCOBRANCA;
		private string _NOMELOCCOBRANCA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private decimal? _VALORPAGO;
		private decimal? _VALORDEVEDOR;
		private int? _FRETE;
		private decimal? _PORCDESCONTO;
		private decimal? _VALORDESCONTO;
		private decimal? _PORCACRESCIMO;
		private decimal? _VALORACRESCIMO;
		private decimal? _VALORPIS;
		private decimal? _VALORCONFINS;
		private string _CODANTT;
		private decimal? _VALORTOTALSERVICO;
		private decimal? _BASECALCISSQN;
		private decimal? _ALIQISSQN;
		private decimal? _VALORISSQN;
		private string _CHAVEACESSO;
		private string _FLAGARQUIVOXML;
		private string _FLAGASSINATURA;
		private string _FLAGCANCELADA;
		private string _FLAGENVIADA;
		private string _FLAGINUTILIZADO;
		private string _FLAGVALIDADA;
		private string _ARQUIVOLOTE;
		private string _RECIBONFE;
		private string _SITUACAONFE;
		private decimal? _ALIQCREDICMS;
		private decimal? _VALORCREDICMS;
		private string _FLAGTIPOPAGAMENTO;
		private string _CPF;
		private string _CNPJ;
		private string _NUMPEDIDO;
		private int? _COD_MUN_IBGE;
		private string _CNPJEMISSOR;

		#region Construtores

		//Construtor default
		public LIS_NOTAFISCALEEntity() { }

		public LIS_NOTAFISCALEEntity(int? IDNOTAFISCALE, string NFISCALE, string SERIE, int? IDCLIENTE, string NOMECLIENTE, DateTime? DTEMISSAO, DateTime? DTSAIDA, string HORASAIDA, int? IDTIPOMOVIM, string NOMEMOVIMESTOQUE, int? IDCFOP, string DESCCFOP, string CODCFOP, string INSCESTSTRIB, decimal? BASECALCICMS, decimal? VALORICMS, decimal? BASECALCICMSLSUB, decimal? VALORICMSSUB, decimal? VALORFRETE, decimal? VALORSEGURO, decimal? OUTRADESPES, decimal? TOTALIPI, decimal? TOTALPRODUTOS, decimal? TOTALNOTA, int? IDVENDEDOR, string NOMEVENDEDOR, decimal? VALORCOMISSAO, int? IDTRANSPORTES, string NOMETRANSPORTADORA, string PLACA, string UFTRANSPORTE, decimal? QUANT, string ESPECIE, string MARCANFE, string NUMEROS, decimal? PESOBRUTO, decimal? PESOLIQUIDO, string INFOCOMPLEM, int? IDCENTROCUSTO, string CENTROCUSTO, string DESCCENTROCUSTO, int? IDFORMAPAGTO, string NOMEFORMAPAGTO, int? IDLOCALCOBRANCA, string NOMELOCCOBRANCA, int? IDSTATUS, string NOMESTATUS, decimal? VALORPAGO, decimal? VALORDEVEDOR, int? FRETE, decimal? PORCDESCONTO, decimal? VALORDESCONTO, decimal? PORCACRESCIMO, decimal? VALORACRESCIMO, decimal? VALORPIS, decimal? VALORCONFINS, string CODANTT, decimal? VALORTOTALSERVICO, decimal? BASECALCISSQN, decimal? ALIQISSQN, decimal? VALORISSQN, string CHAVEACESSO, string FLAGARQUIVOXML, string FLAGASSINATURA, string FLAGCANCELADA, string FLAGENVIADA, string FLAGINUTILIZADO, string FLAGVALIDADA, string ARQUIVOLOTE, string RECIBONFE, string SITUACAONFE, decimal? ALIQCREDICMS, decimal? VALORCREDICMS, string FLAGTIPOPAGAMENTO, string CPF, string CNPJ, string NUMPEDIDO, int? COD_MUN_IBGE, string CNPJEMISSOR)		{

			this._IDNOTAFISCALE = IDNOTAFISCALE;
			this._NFISCALE = NFISCALE;
			this._SERIE = SERIE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTSAIDA = DTSAIDA;
			this._HORASAIDA = HORASAIDA;
			this._IDTIPOMOVIM = IDTIPOMOVIM;
			this._NOMEMOVIMESTOQUE = NOMEMOVIMESTOQUE;
			this._IDCFOP = IDCFOP;
			this._DESCCFOP = DESCCFOP;
			this._CODCFOP = CODCFOP;
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
			this._NOMEVENDEDOR = NOMEVENDEDOR;
			this._VALORCOMISSAO = VALORCOMISSAO;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._NOMETRANSPORTADORA = NOMETRANSPORTADORA;
			this._PLACA = PLACA;
			this._UFTRANSPORTE = UFTRANSPORTE;
			this._QUANT = QUANT;
			this._ESPECIE = ESPECIE;
			this._MARCANFE = MARCANFE;
			this._NUMEROS = NUMEROS;
			this._PESOBRUTO = PESOBRUTO;
			this._PESOLIQUIDO = PESOLIQUIDO;
			this._INFOCOMPLEM = INFOCOMPLEM;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DESCCENTROCUSTO = DESCCENTROCUSTO;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._NOMELOCCOBRANCA = NOMELOCCOBRANCA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._VALORPAGO = VALORPAGO;
			this._VALORDEVEDOR = VALORDEVEDOR;
			this._FRETE = FRETE;
			this._PORCDESCONTO = PORCDESCONTO;
			this._VALORDESCONTO = VALORDESCONTO;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._VALORACRESCIMO = VALORACRESCIMO;
			this._VALORPIS = VALORPIS;
			this._VALORCONFINS = VALORCONFINS;
			this._CODANTT = CODANTT;
			this._VALORTOTALSERVICO = VALORTOTALSERVICO;
			this._BASECALCISSQN = BASECALCISSQN;
			this._ALIQISSQN = ALIQISSQN;
			this._VALORISSQN = VALORISSQN;
			this._CHAVEACESSO = CHAVEACESSO;
			this._FLAGARQUIVOXML = FLAGARQUIVOXML;
			this._FLAGASSINATURA = FLAGASSINATURA;
			this._FLAGCANCELADA = FLAGCANCELADA;
			this._FLAGENVIADA = FLAGENVIADA;
			this._FLAGINUTILIZADO = FLAGINUTILIZADO;
			this._FLAGVALIDADA = FLAGVALIDADA;
			this._ARQUIVOLOTE = ARQUIVOLOTE;
			this._RECIBONFE = RECIBONFE;
			this._SITUACAONFE = SITUACAONFE;
			this._ALIQCREDICMS = ALIQCREDICMS;
			this._VALORCREDICMS = VALORCREDICMS;
			this._FLAGTIPOPAGAMENTO = FLAGTIPOPAGAMENTO;
			this._CPF = CPF;
			this._CNPJ = CNPJ;
			this._NUMPEDIDO = NUMPEDIDO;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._CNPJEMISSOR = CNPJEMISSOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDNOTAFISCALE
		{
			get { return _IDNOTAFISCALE; }
			set { _IDNOTAFISCALE = value; }
		}

		public string NFISCALE
		{
			get { return _NFISCALE; }
			set { _NFISCALE = value; }
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

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
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

		public string NOMEMOVIMESTOQUE
		{
			get { return _NOMEMOVIMESTOQUE; }
			set { _NOMEMOVIMESTOQUE = value; }
		}

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string DESCCFOP
		{
			get { return _DESCCFOP; }
			set { _DESCCFOP = value; }
		}

		public string CODCFOP
		{
			get { return _CODCFOP; }
			set { _CODCFOP = value; }
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

		public string NOMEVENDEDOR
		{
			get { return _NOMEVENDEDOR; }
			set { _NOMEVENDEDOR = value; }
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

		public string NOMETRANSPORTADORA
		{
			get { return _NOMETRANSPORTADORA; }
			set { _NOMETRANSPORTADORA = value; }
		}

		public string PLACA
		{
			get { return _PLACA; }
			set { _PLACA = value; }
		}

		public string UFTRANSPORTE
		{
			get { return _UFTRANSPORTE; }
			set { _UFTRANSPORTE = value; }
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

		public string MARCANFE
		{
			get { return _MARCANFE; }
			set { _MARCANFE = value; }
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

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DESCCENTROCUSTO
		{
			get { return _DESCCENTROCUSTO; }
			set { _DESCCENTROCUSTO = value; }
		}

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public string NOMELOCCOBRANCA
		{
			get { return _NOMELOCCOBRANCA; }
			set { _NOMELOCCOBRANCA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
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

		public decimal? VALORPIS
		{
			get { return _VALORPIS; }
			set { _VALORPIS = value; }
		}

		public decimal? VALORCONFINS
		{
			get { return _VALORCONFINS; }
			set { _VALORCONFINS = value; }
		}

		public string CODANTT
		{
			get { return _CODANTT; }
			set { _CODANTT = value; }
		}

		public decimal? VALORTOTALSERVICO
		{
			get { return _VALORTOTALSERVICO; }
			set { _VALORTOTALSERVICO = value; }
		}

		public decimal? BASECALCISSQN
		{
			get { return _BASECALCISSQN; }
			set { _BASECALCISSQN = value; }
		}

		public decimal? ALIQISSQN
		{
			get { return _ALIQISSQN; }
			set { _ALIQISSQN = value; }
		}

		public decimal? VALORISSQN
		{
			get { return _VALORISSQN; }
			set { _VALORISSQN = value; }
		}

		public string CHAVEACESSO
		{
			get { return _CHAVEACESSO; }
			set { _CHAVEACESSO = value; }
		}

		public string FLAGARQUIVOXML
		{
			get { return _FLAGARQUIVOXML; }
			set { _FLAGARQUIVOXML = value; }
		}

		public string FLAGASSINATURA
		{
			get { return _FLAGASSINATURA; }
			set { _FLAGASSINATURA = value; }
		}

		public string FLAGCANCELADA
		{
			get { return _FLAGCANCELADA; }
			set { _FLAGCANCELADA = value; }
		}

		public string FLAGENVIADA
		{
			get { return _FLAGENVIADA; }
			set { _FLAGENVIADA = value; }
		}

		public string FLAGINUTILIZADO
		{
			get { return _FLAGINUTILIZADO; }
			set { _FLAGINUTILIZADO = value; }
		}

		public string FLAGVALIDADA
		{
			get { return _FLAGVALIDADA; }
			set { _FLAGVALIDADA = value; }
		}

		public string ARQUIVOLOTE
		{
			get { return _ARQUIVOLOTE; }
			set { _ARQUIVOLOTE = value; }
		}

		public string RECIBONFE
		{
			get { return _RECIBONFE; }
			set { _RECIBONFE = value; }
		}

		public string SITUACAONFE
		{
			get { return _SITUACAONFE; }
			set { _SITUACAONFE = value; }
		}

		public decimal? ALIQCREDICMS
		{
			get { return _ALIQCREDICMS; }
			set { _ALIQCREDICMS = value; }
		}

		public decimal? VALORCREDICMS
		{
			get { return _VALORCREDICMS; }
			set { _VALORCREDICMS = value; }
		}

		public string FLAGTIPOPAGAMENTO
		{
			get { return _FLAGTIPOPAGAMENTO; }
			set { _FLAGTIPOPAGAMENTO = value; }
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

		public string NUMPEDIDO
		{
			get { return _NUMPEDIDO; }
			set { _NUMPEDIDO = value; }
		}

		public int? COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
		}

		public string CNPJEMISSOR
		{
			get { return _CNPJEMISSOR; }
			set { _CNPJEMISSOR = value; }
		}

		#endregion
	}
}
