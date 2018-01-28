using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ESTOQUEESEntity
	{
		private int _IDESTOQUEES;
		private int? _IDTIPOMOVIMENTACAO;
		private DateTime? _DATAMOVIM;
		private int? _IDCODMOVIMENTACAO;
		private string _NDOCUMENTO;
		private int? _IDFORNECEDOR;
		private decimal? _TOTALMOVIMENTACAO;
		private decimal? _VALORIPI;
		private decimal? _VALORICMS;
		private decimal? _VALORFRETE;
		private int? _IDCLIENTE;
		private int? _IDCFOP;
		private string _MODELONF;
		private string _SERIENF;
		private decimal? _VALORBASEICMS;
		private string _FLAGSINTEGRA;
		private string _FLAGENERGIATELECOM;
		private string _CHAVEACESSO;
		private string _CNPJEMISSOR;

		#region Construtores

		//Construtor default
		public ESTOQUEESEntity() {
			this._IDTIPOMOVIMENTACAO = null;
			this._DATAMOVIM = null;
			this._IDCODMOVIMENTACAO = null;
			this._IDFORNECEDOR = null;
			this._TOTALMOVIMENTACAO = null;
			this._VALORIPI = null;
			this._VALORICMS = null;
			this._VALORFRETE = null;
			this._IDCLIENTE = null;
			this._IDCFOP = null;
			this._VALORBASEICMS = null;
		}

		public ESTOQUEESEntity(int IDESTOQUEES, int? IDTIPOMOVIMENTACAO, DateTime? DATAMOVIM, int? IDCODMOVIMENTACAO, string NDOCUMENTO, int? IDFORNECEDOR, decimal? TOTALMOVIMENTACAO, decimal? VALORIPI, decimal? VALORICMS, decimal? VALORFRETE, int? IDCLIENTE, int? IDCFOP, string MODELONF, string SERIENF, decimal? VALORBASEICMS, string FLAGSINTEGRA, string FLAGENERGIATELECOM, string CHAVEACESSO, string CNPJEMISSOR) {

			this._IDESTOQUEES = IDESTOQUEES;
			this._IDTIPOMOVIMENTACAO = IDTIPOMOVIMENTACAO;
			this._DATAMOVIM = DATAMOVIM;
			this._IDCODMOVIMENTACAO = IDCODMOVIMENTACAO;
			this._NDOCUMENTO = NDOCUMENTO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._TOTALMOVIMENTACAO = TOTALMOVIMENTACAO;
			this._VALORIPI = VALORIPI;
			this._VALORICMS = VALORICMS;
			this._VALORFRETE = VALORFRETE;
			this._IDCLIENTE = IDCLIENTE;
			this._IDCFOP = IDCFOP;
			this._MODELONF = MODELONF;
			this._SERIENF = SERIENF;
			this._VALORBASEICMS = VALORBASEICMS;
			this._FLAGSINTEGRA = FLAGSINTEGRA;
			this._FLAGENERGIATELECOM = FLAGENERGIATELECOM;
			this._CHAVEACESSO = CHAVEACESSO;
			this._CNPJEMISSOR = CNPJEMISSOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDESTOQUEES
		{
			get { return _IDESTOQUEES; }
			set { _IDESTOQUEES = value; }
		}

		public int? IDTIPOMOVIMENTACAO
		{
			get { return _IDTIPOMOVIMENTACAO; }
			set { _IDTIPOMOVIMENTACAO = value; }
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

		public string FLAGSINTEGRA
		{
			get { return _FLAGSINTEGRA; }
			set { _FLAGSINTEGRA = value; }
		}

		public string FLAGENERGIATELECOM
		{
			get { return _FLAGENERGIATELECOM; }
			set { _FLAGENERGIATELECOM = value; }
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
