using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ESTOQUEESEntity
	{
		private int? _IDESTOQUEES;
		private int? _IDTIPOMOVIMENTACAO;
		private string _NOMETIPOMOVIMENTACAO;
		private DateTime? _DATAMOVIM;
		private int? _IDCODMOVIMENTACAO;
		private string _NOMEMOVIMENTACAO;
		private string _CODIGOMOVIMENTACAO;
		private string _NDOCUMENTO;
		private int? _IDFORNECEDOR;
		private string _NOMEFORNECEDOR;
		private decimal? _TOTALMOVIMENTACAO;
		private decimal? _VALORIPI;
		private decimal? _VALORICMS;
		private decimal? _VALORFRETE;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private int? _IDCFOP;
		private string _MODELONF;
		private string _SERIENF;
		private decimal? _VALORBASEICMS;
		private string _DESCCFOP;
		private string _CODCFOP;
		private string _FLAGSINTEGRA;
		private string _CHAVEACESSO;
		private string _CNPJEMISSOR;

		#region Construtores

		//Construtor default
		public LIS_ESTOQUEESEntity() { }

		public LIS_ESTOQUEESEntity(int? IDESTOQUEES, int? IDTIPOMOVIMENTACAO, string NOMETIPOMOVIMENTACAO, DateTime? DATAMOVIM, int? IDCODMOVIMENTACAO, string NOMEMOVIMENTACAO, string CODIGOMOVIMENTACAO, string NDOCUMENTO, int? IDFORNECEDOR, string NOMEFORNECEDOR, decimal? TOTALMOVIMENTACAO, decimal? VALORIPI, decimal? VALORICMS, decimal? VALORFRETE, int? IDCLIENTE, string NOMECLIENTE, int? IDCFOP, string MODELONF, string SERIENF, decimal? VALORBASEICMS, string DESCCFOP, string CODCFOP, string FLAGSINTEGRA, string CHAVEACESSO, string CNPJEMISSOR)		{

			this._IDESTOQUEES = IDESTOQUEES;
			this._IDTIPOMOVIMENTACAO = IDTIPOMOVIMENTACAO;
			this._NOMETIPOMOVIMENTACAO = NOMETIPOMOVIMENTACAO;
			this._DATAMOVIM = DATAMOVIM;
			this._IDCODMOVIMENTACAO = IDCODMOVIMENTACAO;
			this._NOMEMOVIMENTACAO = NOMEMOVIMENTACAO;
			this._CODIGOMOVIMENTACAO = CODIGOMOVIMENTACAO;
			this._NDOCUMENTO = NDOCUMENTO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._TOTALMOVIMENTACAO = TOTALMOVIMENTACAO;
			this._VALORIPI = VALORIPI;
			this._VALORICMS = VALORICMS;
			this._VALORFRETE = VALORFRETE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._IDCFOP = IDCFOP;
			this._MODELONF = MODELONF;
			this._SERIENF = SERIENF;
			this._VALORBASEICMS = VALORBASEICMS;
			this._DESCCFOP = DESCCFOP;
			this._CODCFOP = CODCFOP;
			this._FLAGSINTEGRA = FLAGSINTEGRA;
			this._CHAVEACESSO = CHAVEACESSO;
			this._CNPJEMISSOR = CNPJEMISSOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDESTOQUEES
		{
			get { return _IDESTOQUEES; }
			set { _IDESTOQUEES = value; }
		}

		public int? IDTIPOMOVIMENTACAO
		{
			get { return _IDTIPOMOVIMENTACAO; }
			set { _IDTIPOMOVIMENTACAO = value; }
		}

		public string NOMETIPOMOVIMENTACAO
		{
			get { return _NOMETIPOMOVIMENTACAO; }
			set { _NOMETIPOMOVIMENTACAO = value; }
		}

		public DateTime? DATAMOVIM
		{
			get { return _DATAMOVIM; }
			set { _DATAMOVIM = value; }
		}

		public int? IDCODMOVIMENTACAO
		{
			get { return _IDCODMOVIMENTACAO; }
			set { _IDCODMOVIMENTACAO = value; }
		}

		public string NOMEMOVIMENTACAO
		{
			get { return _NOMEMOVIMENTACAO; }
			set { _NOMEMOVIMENTACAO = value; }
		}

		public string CODIGOMOVIMENTACAO
		{
			get { return _CODIGOMOVIMENTACAO; }
			set { _CODIGOMOVIMENTACAO = value; }
		}

		public string NDOCUMENTO
		{
			get { return _NDOCUMENTO; }
			set { _NDOCUMENTO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string NOMEFORNECEDOR
		{
			get { return _NOMEFORNECEDOR; }
			set { _NOMEFORNECEDOR = value; }
		}

		public decimal? TOTALMOVIMENTACAO
		{
			get { return _TOTALMOVIMENTACAO; }
			set { _TOTALMOVIMENTACAO = value; }
		}

		public decimal? VALORIPI
		{
			get { return _VALORIPI; }
			set { _VALORIPI = value; }
		}

		public decimal? VALORICMS
		{
			get { return _VALORICMS; }
			set { _VALORICMS = value; }
		}

		public decimal? VALORFRETE
		{
			get { return _VALORFRETE; }
			set { _VALORFRETE = value; }
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

		public int? IDCFOP
		{
			get { return _IDCFOP; }
			set { _IDCFOP = value; }
		}

		public string MODELONF
		{
			get { return _MODELONF; }
			set { _MODELONF = value; }
		}

		public string SERIENF
		{
			get { return _SERIENF; }
			set { _SERIENF = value; }
		}

		public decimal? VALORBASEICMS
		{
			get { return _VALORBASEICMS; }
			set { _VALORBASEICMS = value; }
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

		public string FLAGSINTEGRA
		{
			get { return _FLAGSINTEGRA; }
			set { _FLAGSINTEGRA = value; }
		}

		public string CHAVEACESSO
		{
			get { return _CHAVEACESSO; }
			set { _CHAVEACESSO = value; }
		}

		public string CNPJEMISSOR
		{
			get { return _CNPJEMISSOR; }
			set { _CNPJEMISSOR = value; }
		}

		#endregion
	}
}
