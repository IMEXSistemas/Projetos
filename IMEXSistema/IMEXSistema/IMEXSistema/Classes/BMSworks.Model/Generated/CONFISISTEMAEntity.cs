using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONFISISTEMAEntity
	{
		private int _IDCONFIGSISTEMA;
		private string _FLAGLOGORELATORIO;
		private int? _IDARQUIVOBINARIO1;
		private int? _IDCONFIGBOLETA;
		private string _FLAGCOMPENTREGABOLETA;
		private string _FLAGCARNEBOLETA;
		private int? _PRAZOOS;
		private int? _PRAZOORCAMENTO;
		private string _FLAGVENDADEBITO;
		private string _FLAGPEDBAIXAESTOQUE;
		private int? _TEMPOGARANTIA;
		private string _FLAGCOMISSAO;
		private string _MSGFECHOS;
		private string _MSGPEDIDO;
		private string _MSGCONSIGNACAO;
		private string _FLAGFECHOSESTOQUE;
		private string _SERIENF;
		private string _FLAGSOMAIPI;
		private string _FLAGSOMASEGURO;
		private string _FLAGJANELAS;
		private string _SERIENFE;
		private string _FLAGSOMAIPINFE;
		private string _FLAGSOMASEGURANFE;
		private string _FLAGCOMISSAONFE;
		private string _MODELONFE;
		private decimal? _ALISSQN;
		private string _INSCMUNICIPAL;
		private decimal? _ALIPIS;
		private decimal? _ALICOFINS;
		private string _FLAGBASEISSQN;
		private int? _CODMUNIBGE;
		private int? _CODUFIBGE;
		private string _FLAGAMBIENTENFE;
		private string _SERIALCERTFDIGITAL;
		private string _NAMECERTFDIGITAL;
		private string _VALIDADECERTDIGITAL;
		private string _FLAGLOGONFE;
		private string _USUARIOPROXY;
		private string _SENHAPROXY;
		private int? _IDVERSAOXMLNFE;
		private string _NOMEFANTASIA;
		private string _CNAE;
		private string _IEST;
		private string _CRT;
		private string _FLAGALIQIPICONFIS;
		private int? _PORTAEMAIL;
		private string _EMAIL;
		private string _SMTP;
		private string _SENHAEMAIL;
		private string _CONFSEGSSL;
		private string _HOSTPROXY;
		private string _PORTAPROXY;
		private string _FLAGNFESERVICOS;
		private string _NOTAFISCALINICIAL;
		private string _MSGINICIALNFE;
		private int? _LARGLAMINA;
		private int? _NIVELOTIMIZ;
		private string _SCHEMAXML;
		private string _CASADECPRINTDANFE;
		private string _FLAGPLANOCORTE;
		private string _FLAGCODREFERENCIA;
		private string _FLAGCUPOMFISCAL;
		private string _FLAGPEDIDOMT;
		private string _ESTOQUENEGATIVO;
		private string _FLAGCPFCNPJPEDIDO;
		private string _FLAGCPDIGISAT;
		private string _PATHRECEPDIGISAT;
		private string _FLAGBAIXAESTOQUENF;
		private string _OPERADORASMS;
		private string _FLAGLIMITECREDITO;
		private string _FLAGHABNFE;
		private string _FLAGMSGFECHA;
		private string _FLAGCUPOMFAST;
		private string _FLABACKUP;
		private string _FLAGCSTECF;
		private string _FLAGCODREFNFE;

		#region Construtores

		//Construtor default
		public CONFISISTEMAEntity() {
			this._IDARQUIVOBINARIO1 = null;
			this._IDCONFIGBOLETA = null;
			this._PRAZOOS = null;
			this._PRAZOORCAMENTO = null;
			this._TEMPOGARANTIA = null;
			this._ALISSQN = null;
			this._ALIPIS = null;
			this._ALICOFINS = null;
			this._CODMUNIBGE = null;
			this._CODUFIBGE = null;
			this._IDVERSAOXMLNFE = null;
			this._PORTAEMAIL = null;
			this._LARGLAMINA = null;
			this._NIVELOTIMIZ = null;
		}

		public CONFISISTEMAEntity(int IDCONFIGSISTEMA, string FLAGLOGORELATORIO, int? IDARQUIVOBINARIO1, int? IDCONFIGBOLETA, string FLAGCOMPENTREGABOLETA, string FLAGCARNEBOLETA, int? PRAZOOS, int? PRAZOORCAMENTO, string FLAGVENDADEBITO, string FLAGPEDBAIXAESTOQUE, int? TEMPOGARANTIA, string FLAGCOMISSAO, string MSGFECHOS, string MSGPEDIDO, string MSGCONSIGNACAO, string FLAGFECHOSESTOQUE, string SERIENF, string FLAGSOMAIPI, string FLAGSOMASEGURO, string FLAGJANELAS, string SERIENFE, string FLAGSOMAIPINFE, string FLAGSOMASEGURANFE, string FLAGCOMISSAONFE, string MODELONFE, decimal? ALISSQN, string INSCMUNICIPAL, decimal? ALIPIS, decimal? ALICOFINS, string FLAGBASEISSQN, int? CODMUNIBGE, int? CODUFIBGE, string FLAGAMBIENTENFE, string SERIALCERTFDIGITAL, string NAMECERTFDIGITAL, string VALIDADECERTDIGITAL, string FLAGLOGONFE, string USUARIOPROXY, string SENHAPROXY, int? IDVERSAOXMLNFE, string NOMEFANTASIA, string CNAE, string IEST, string CRT, string FLAGALIQIPICONFIS, int? PORTAEMAIL, string EMAIL, string SMTP, string SENHAEMAIL, string CONFSEGSSL, string HOSTPROXY, string PORTAPROXY, string FLAGNFESERVICOS, string NOTAFISCALINICIAL, string MSGINICIALNFE, int? LARGLAMINA, int? NIVELOTIMIZ, string SCHEMAXML, string CASADECPRINTDANFE, string FLAGPLANOCORTE, string FLAGCODREFERENCIA, string FLAGCUPOMFISCAL, string FLAGPEDIDOMT, string ESTOQUENEGATIVO, string FLAGCPFCNPJPEDIDO, string FLAGCPDIGISAT, string PATHRECEPDIGISAT, string FLAGBAIXAESTOQUENF, string OPERADORASMS, string FLAGLIMITECREDITO, string FLAGHABNFE, string FLAGMSGFECHA, string FLAGCUPOMFAST, string FLABACKUP, string FLAGCSTECF, string FLAGCODREFNFE) {

			this._IDCONFIGSISTEMA = IDCONFIGSISTEMA;
			this._FLAGLOGORELATORIO = FLAGLOGORELATORIO;
			this._IDARQUIVOBINARIO1 = IDARQUIVOBINARIO1;
			this._IDCONFIGBOLETA = IDCONFIGBOLETA;
			this._FLAGCOMPENTREGABOLETA = FLAGCOMPENTREGABOLETA;
			this._FLAGCARNEBOLETA = FLAGCARNEBOLETA;
			this._PRAZOOS = PRAZOOS;
			this._PRAZOORCAMENTO = PRAZOORCAMENTO;
			this._FLAGVENDADEBITO = FLAGVENDADEBITO;
			this._FLAGPEDBAIXAESTOQUE = FLAGPEDBAIXAESTOQUE;
			this._TEMPOGARANTIA = TEMPOGARANTIA;
			this._FLAGCOMISSAO = FLAGCOMISSAO;
			this._MSGFECHOS = MSGFECHOS;
			this._MSGPEDIDO = MSGPEDIDO;
			this._MSGCONSIGNACAO = MSGCONSIGNACAO;
			this._FLAGFECHOSESTOQUE = FLAGFECHOSESTOQUE;
			this._SERIENF = SERIENF;
			this._FLAGSOMAIPI = FLAGSOMAIPI;
			this._FLAGSOMASEGURO = FLAGSOMASEGURO;
			this._FLAGJANELAS = FLAGJANELAS;
			this._SERIENFE = SERIENFE;
			this._FLAGSOMAIPINFE = FLAGSOMAIPINFE;
			this._FLAGSOMASEGURANFE = FLAGSOMASEGURANFE;
			this._FLAGCOMISSAONFE = FLAGCOMISSAONFE;
			this._MODELONFE = MODELONFE;
			this._ALISSQN = ALISSQN;
			this._INSCMUNICIPAL = INSCMUNICIPAL;
			this._ALIPIS = ALIPIS;
			this._ALICOFINS = ALICOFINS;
			this._FLAGBASEISSQN = FLAGBASEISSQN;
			this._CODMUNIBGE = CODMUNIBGE;
			this._CODUFIBGE = CODUFIBGE;
			this._FLAGAMBIENTENFE = FLAGAMBIENTENFE;
			this._SERIALCERTFDIGITAL = SERIALCERTFDIGITAL;
			this._NAMECERTFDIGITAL = NAMECERTFDIGITAL;
			this._VALIDADECERTDIGITAL = VALIDADECERTDIGITAL;
			this._FLAGLOGONFE = FLAGLOGONFE;
			this._USUARIOPROXY = USUARIOPROXY;
			this._SENHAPROXY = SENHAPROXY;
			this._IDVERSAOXMLNFE = IDVERSAOXMLNFE;
			this._NOMEFANTASIA = NOMEFANTASIA;
			this._CNAE = CNAE;
			this._IEST = IEST;
			this._CRT = CRT;
			this._FLAGALIQIPICONFIS = FLAGALIQIPICONFIS;
			this._PORTAEMAIL = PORTAEMAIL;
			this._EMAIL = EMAIL;
			this._SMTP = SMTP;
			this._SENHAEMAIL = SENHAEMAIL;
			this._CONFSEGSSL = CONFSEGSSL;
			this._HOSTPROXY = HOSTPROXY;
			this._PORTAPROXY = PORTAPROXY;
			this._FLAGNFESERVICOS = FLAGNFESERVICOS;
			this._NOTAFISCALINICIAL = NOTAFISCALINICIAL;
			this._MSGINICIALNFE = MSGINICIALNFE;
			this._LARGLAMINA = LARGLAMINA;
			this._NIVELOTIMIZ = NIVELOTIMIZ;
			this._SCHEMAXML = SCHEMAXML;
			this._CASADECPRINTDANFE = CASADECPRINTDANFE;
			this._FLAGPLANOCORTE = FLAGPLANOCORTE;
			this._FLAGCODREFERENCIA = FLAGCODREFERENCIA;
			this._FLAGCUPOMFISCAL = FLAGCUPOMFISCAL;
			this._FLAGPEDIDOMT = FLAGPEDIDOMT;
			this._ESTOQUENEGATIVO = ESTOQUENEGATIVO;
			this._FLAGCPFCNPJPEDIDO = FLAGCPFCNPJPEDIDO;
			this._FLAGCPDIGISAT = FLAGCPDIGISAT;
			this._PATHRECEPDIGISAT = PATHRECEPDIGISAT;
			this._FLAGBAIXAESTOQUENF = FLAGBAIXAESTOQUENF;
			this._OPERADORASMS = OPERADORASMS;
			this._FLAGLIMITECREDITO = FLAGLIMITECREDITO;
			this._FLAGHABNFE = FLAGHABNFE;
			this._FLAGMSGFECHA = FLAGMSGFECHA;
			this._FLAGCUPOMFAST = FLAGCUPOMFAST;
			this._FLABACKUP = FLABACKUP;
			this._FLAGCSTECF = FLAGCSTECF;
			this._FLAGCODREFNFE = FLAGCODREFNFE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONFIGSISTEMA
		{
			get { return _IDCONFIGSISTEMA; }
			set { _IDCONFIGSISTEMA = value; }
		}

		public string FLAGLOGORELATORIO
		{
			get { return _FLAGLOGORELATORIO; }
			set { _FLAGLOGORELATORIO = value; }
		}

		public int? IDARQUIVOBINARIO1
		{
			get { return _IDARQUIVOBINARIO1; }
			set { _IDARQUIVOBINARIO1 = value; }
		}

		public int? IDCONFIGBOLETA
		{
			get { return _IDCONFIGBOLETA; }
			set { _IDCONFIGBOLETA = value; }
		}

		public string FLAGCOMPENTREGABOLETA
		{
			get { return _FLAGCOMPENTREGABOLETA; }
			set { _FLAGCOMPENTREGABOLETA = value; }
		}

		public string FLAGCARNEBOLETA
		{
			get { return _FLAGCARNEBOLETA; }
			set { _FLAGCARNEBOLETA = value; }
		}

		public int? PRAZOOS
		{
			get { return _PRAZOOS; }
			set { _PRAZOOS = value; }
		}

		public int? PRAZOORCAMENTO
		{
			get { return _PRAZOORCAMENTO; }
			set { _PRAZOORCAMENTO = value; }
		}

		public string FLAGVENDADEBITO
		{
			get { return _FLAGVENDADEBITO; }
			set { _FLAGVENDADEBITO = value; }
		}

		public string FLAGPEDBAIXAESTOQUE
		{
			get { return _FLAGPEDBAIXAESTOQUE; }
			set { _FLAGPEDBAIXAESTOQUE = value; }
		}

		public int? TEMPOGARANTIA
		{
			get { return _TEMPOGARANTIA; }
			set { _TEMPOGARANTIA = value; }
		}

		public string FLAGCOMISSAO
		{
			get { return _FLAGCOMISSAO; }
			set { _FLAGCOMISSAO = value; }
		}

		public string MSGFECHOS
		{
			get { return _MSGFECHOS; }
			set { _MSGFECHOS = value; }
		}

		public string MSGPEDIDO
		{
			get { return _MSGPEDIDO; }
			set { _MSGPEDIDO = value; }
		}

		public string MSGCONSIGNACAO
		{
			get { return _MSGCONSIGNACAO; }
			set { _MSGCONSIGNACAO = value; }
		}

		public string FLAGFECHOSESTOQUE
		{
			get { return _FLAGFECHOSESTOQUE; }
			set { _FLAGFECHOSESTOQUE = value; }
		}

		public string SERIENF
		{
			get { return _SERIENF; }
			set { _SERIENF = value; }
		}

		public string FLAGSOMAIPI
		{
			get { return _FLAGSOMAIPI; }
			set { _FLAGSOMAIPI = value; }
		}

		public string FLAGSOMASEGURO
		{
			get { return _FLAGSOMASEGURO; }
			set { _FLAGSOMASEGURO = value; }
		}

		public string FLAGJANELAS
		{
			get { return _FLAGJANELAS; }
			set { _FLAGJANELAS = value; }
		}

		public string SERIENFE
		{
			get { return _SERIENFE; }
			set { _SERIENFE = value; }
		}

		public string FLAGSOMAIPINFE
		{
			get { return _FLAGSOMAIPINFE; }
			set { _FLAGSOMAIPINFE = value; }
		}

		public string FLAGSOMASEGURANFE
		{
			get { return _FLAGSOMASEGURANFE; }
			set { _FLAGSOMASEGURANFE = value; }
		}

		public string FLAGCOMISSAONFE
		{
			get { return _FLAGCOMISSAONFE; }
			set { _FLAGCOMISSAONFE = value; }
		}

		public string MODELONFE
		{
			get { return _MODELONFE; }
			set { _MODELONFE = value; }
		}

		public decimal? ALISSQN
		{
			get { return _ALISSQN; }
			set { _ALISSQN = value; }
		}

		public string INSCMUNICIPAL
		{
			get { return _INSCMUNICIPAL; }
			set { _INSCMUNICIPAL = value; }
		}

		public decimal? ALIPIS
		{
			get { return _ALIPIS; }
			set { _ALIPIS = value; }
		}

		public decimal? ALICOFINS
		{
			get { return _ALICOFINS; }
			set { _ALICOFINS = value; }
		}

		public string FLAGBASEISSQN
		{
			get { return _FLAGBASEISSQN; }
			set { _FLAGBASEISSQN = value; }
		}

		public int? CODMUNIBGE
		{
			get { return _CODMUNIBGE; }
			set { _CODMUNIBGE = value; }
		}

		public int? CODUFIBGE
		{
			get { return _CODUFIBGE; }
			set { _CODUFIBGE = value; }
		}

		public string FLAGAMBIENTENFE
		{
			get { return _FLAGAMBIENTENFE; }
			set { _FLAGAMBIENTENFE = value; }
		}

		public string SERIALCERTFDIGITAL
		{
			get { return _SERIALCERTFDIGITAL; }
			set { _SERIALCERTFDIGITAL = value; }
		}

		public string NAMECERTFDIGITAL
		{
			get { return _NAMECERTFDIGITAL; }
			set { _NAMECERTFDIGITAL = value; }
		}

		public string VALIDADECERTDIGITAL
		{
			get { return _VALIDADECERTDIGITAL; }
			set { _VALIDADECERTDIGITAL = value; }
		}

		public string FLAGLOGONFE
		{
			get { return _FLAGLOGONFE; }
			set { _FLAGLOGONFE = value; }
		}

		public string USUARIOPROXY
		{
			get { return _USUARIOPROXY; }
			set { _USUARIOPROXY = value; }
		}

		public string SENHAPROXY
		{
			get { return _SENHAPROXY; }
			set { _SENHAPROXY = value; }
		}

		public int? IDVERSAOXMLNFE
		{
			get { return _IDVERSAOXMLNFE; }
			set { _IDVERSAOXMLNFE = value; }
		}

		public string NOMEFANTASIA
		{
			get { return _NOMEFANTASIA; }
			set { _NOMEFANTASIA = value; }
		}

		public string CNAE
		{
			get { return _CNAE; }
			set { _CNAE = value; }
		}

		public string IEST
		{
			get { return _IEST; }
			set { _IEST = value; }
		}

		public string CRT
		{
			get { return _CRT; }
			set { _CRT = value; }
		}

		public string FLAGALIQIPICONFIS
		{
			get { return _FLAGALIQIPICONFIS; }
			set { _FLAGALIQIPICONFIS = value; }
		}

		public int? PORTAEMAIL
		{
			get { return _PORTAEMAIL; }
			set { _PORTAEMAIL = value; }
		}

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public string SMTP
		{
			get { return _SMTP; }
			set { _SMTP = value; }
		}

		public string SENHAEMAIL
		{
			get { return _SENHAEMAIL; }
			set { _SENHAEMAIL = value; }
		}

		public string CONFSEGSSL
		{
			get { return _CONFSEGSSL; }
			set { _CONFSEGSSL = value; }
		}

		public string HOSTPROXY
		{
			get { return _HOSTPROXY; }
			set { _HOSTPROXY = value; }
		}

		public string PORTAPROXY
		{
			get { return _PORTAPROXY; }
			set { _PORTAPROXY = value; }
		}

		public string FLAGNFESERVICOS
		{
			get { return _FLAGNFESERVICOS; }
			set { _FLAGNFESERVICOS = value; }
		}

		public string NOTAFISCALINICIAL
		{
			get { return _NOTAFISCALINICIAL; }
			set { _NOTAFISCALINICIAL = value; }
		}

		public string MSGINICIALNFE
		{
			get { return _MSGINICIALNFE; }
			set { _MSGINICIALNFE = value; }
		}

		public int? LARGLAMINA
		{
			get { return _LARGLAMINA; }
			set { _LARGLAMINA = value; }
		}

		public int? NIVELOTIMIZ
		{
			get { return _NIVELOTIMIZ; }
			set { _NIVELOTIMIZ = value; }
		}

		public string SCHEMAXML
		{
			get { return _SCHEMAXML; }
			set { _SCHEMAXML = value; }
		}

		public string CASADECPRINTDANFE
		{
			get { return _CASADECPRINTDANFE; }
			set { _CASADECPRINTDANFE = value; }
		}

		public string FLAGPLANOCORTE
		{
			get { return _FLAGPLANOCORTE; }
			set { _FLAGPLANOCORTE = value; }
		}

		public string FLAGCODREFERENCIA
		{
			get { return _FLAGCODREFERENCIA; }
			set { _FLAGCODREFERENCIA = value; }
		}

		public string FLAGCUPOMFISCAL
		{
			get { return _FLAGCUPOMFISCAL; }
			set { _FLAGCUPOMFISCAL = value; }
		}

		public string FLAGPEDIDOMT
		{
			get { return _FLAGPEDIDOMT; }
			set { _FLAGPEDIDOMT = value; }
		}

		public string ESTOQUENEGATIVO
		{
			get { return _ESTOQUENEGATIVO; }
			set { _ESTOQUENEGATIVO = value; }
		}

		public string FLAGCPFCNPJPEDIDO
		{
			get { return _FLAGCPFCNPJPEDIDO; }
			set { _FLAGCPFCNPJPEDIDO = value; }
		}

		public string FLAGCPDIGISAT
		{
			get { return _FLAGCPDIGISAT; }
			set { _FLAGCPDIGISAT = value; }
		}

		public string PATHRECEPDIGISAT
		{
			get { return _PATHRECEPDIGISAT; }
			set { _PATHRECEPDIGISAT = value; }
		}

		public string FLAGBAIXAESTOQUENF
		{
			get { return _FLAGBAIXAESTOQUENF; }
			set { _FLAGBAIXAESTOQUENF = value; }
		}

		public string OPERADORASMS
		{
			get { return _OPERADORASMS; }
			set { _OPERADORASMS = value; }
		}

		public string FLAGLIMITECREDITO
		{
			get { return _FLAGLIMITECREDITO; }
			set { _FLAGLIMITECREDITO = value; }
		}

		public string FLAGHABNFE
		{
			get { return _FLAGHABNFE; }
			set { _FLAGHABNFE = value; }
		}

		public string FLAGMSGFECHA
		{
			get { return _FLAGMSGFECHA; }
			set { _FLAGMSGFECHA = value; }
		}

		public string FLAGCUPOMFAST
		{
			get { return _FLAGCUPOMFAST; }
			set { _FLAGCUPOMFAST = value; }
		}

		public string FLABACKUP
		{
			get { return _FLABACKUP; }
			set { _FLABACKUP = value; }
		}

		public string FLAGCSTECF
		{
			get { return _FLAGCSTECF; }
			set { _FLAGCSTECF = value; }
		}

		public string FLAGCODREFNFE
		{
			get { return _FLAGCODREFNFE; }
			set { _FLAGCODREFNFE = value; }
		}

		#endregion
	}
}
