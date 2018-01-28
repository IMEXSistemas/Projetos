using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MOVPRODUTOESEntity
	{
		private int? _IDMOVPRODUTOES;
		private int? _IDESTOQUEES;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORCUNITARIO;
		private decimal? _VALORTOTAL;
		private string _FLAGATUALIZACUSTO;
		private DateTime? _DATAMOVIMENTACAO;
		private decimal? _SALDOESTOQUE;
		private int? _IDTIPOMOVIMENTACAO;
		private decimal? _ALQICMS;
		private int? _IDCFOP;
		private string _DESCCFOP;
		private string _CODCFOP;
		private DateTime? _DATAMOVIM;
		private decimal? _BASEICMS;
		private decimal? _VLICMS;
		private string _FLAGSINTEGRA;
		private string _NDOCUMENTO;
		private string _CST_CSOSN;
		private decimal? _VLIPI;
		private string _FLAGENERGIATELECOM;
		private decimal? _VLFRETE;
		private decimal? _VLBASEICMSST;
		private decimal? _VLICMSST;
		private decimal? _VLDESCONTOPRODUTO;
		private int? _IDFORNECEDOR;
		private int? _IDGRUPOCATEGORIA;
		private string _CNPJEMISSOR;

		#region Construtores

		//Construtor default
		public LIS_MOVPRODUTOESEntity() { }

		public LIS_MOVPRODUTOESEntity(int? IDMOVPRODUTOES, int? IDESTOQUEES, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORCUNITARIO, decimal? VALORTOTAL, string FLAGATUALIZACUSTO, DateTime? DATAMOVIMENTACAO, decimal? SALDOESTOQUE, int? IDTIPOMOVIMENTACAO, decimal? ALQICMS, int? IDCFOP, string DESCCFOP, string CODCFOP, DateTime? DATAMOVIM, decimal? BASEICMS, decimal? VLICMS, string FLAGSINTEGRA, string NDOCUMENTO, string CST_CSOSN, decimal? VLIPI, string FLAGENERGIATELECOM, decimal? VLFRETE, decimal? VLBASEICMSST, decimal? VLICMSST, decimal? VLDESCONTOPRODUTO, int? IDFORNECEDOR, int? IDGRUPOCATEGORIA, string CNPJEMISSOR)		{

			this._IDMOVPRODUTOES = IDMOVPRODUTOES;
			this._IDESTOQUEES = IDESTOQUEES;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORCUNITARIO = VALORCUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._FLAGATUALIZACUSTO = FLAGATUALIZACUSTO;
			this._DATAMOVIMENTACAO = DATAMOVIMENTACAO;
			this._SALDOESTOQUE = SALDOESTOQUE;
			this._IDTIPOMOVIMENTACAO = IDTIPOMOVIMENTACAO;
			this._ALQICMS = ALQICMS;
			this._IDCFOP = IDCFOP;
			this._DESCCFOP = DESCCFOP;
			this._CODCFOP = CODCFOP;
			this._DATAMOVIM = DATAMOVIM;
			this._BASEICMS = BASEICMS;
			this._VLICMS = VLICMS;
			this._FLAGSINTEGRA = FLAGSINTEGRA;
			this._NDOCUMENTO = NDOCUMENTO;
			this._CST_CSOSN = CST_CSOSN;
			this._VLIPI = VLIPI;
			this._FLAGENERGIATELECOM = FLAGENERGIATELECOM;
			this._VLFRETE = VLFRETE;
			this._VLBASEICMSST = VLBASEICMSST;
			this._VLICMSST = VLICMSST;
			this._VLDESCONTOPRODUTO = VLDESCONTOPRODUTO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._CNPJEMISSOR = CNPJEMISSOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMOVPRODUTOES
		{
			get { return _IDMOVPRODUTOES; }
			set { _IDMOVPRODUTOES = value; }
		}

		public int? IDESTOQUEES
		{
			get { return _IDESTOQUEES; }
			set { _IDESTOQUEES = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? VALORCUNITARIO
		{
			get { return _VALORCUNITARIO; }
			set { _VALORCUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public string FLAGATUALIZACUSTO
		{
			get { return _FLAGATUALIZACUSTO; }
			set { _FLAGATUALIZACUSTO = value; }
		}

		public DateTime? DATAMOVIMENTACAO
		{
			get { return _DATAMOVIMENTACAO; }
			set { _DATAMOVIMENTACAO = value; }
		}

		public decimal? SALDOESTOQUE
		{
			get { return _SALDOESTOQUE; }
			set { _SALDOESTOQUE = value; }
		}

		public int? IDTIPOMOVIMENTACAO
		{
			get { return _IDTIPOMOVIMENTACAO; }
			set { _IDTIPOMOVIMENTACAO = value; }
		}

		public decimal? ALQICMS
		{
			get { return _ALQICMS; }
			set { _ALQICMS = value; }
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

		public DateTime? DATAMOVIM
		{
			get { return _DATAMOVIM; }
			set { _DATAMOVIM = value; }
		}

		public decimal? BASEICMS
		{
			get { return _BASEICMS; }
			set { _BASEICMS = value; }
		}

		public decimal? VLICMS
		{
			get { return _VLICMS; }
			set { _VLICMS = value; }
		}

		public string FLAGSINTEGRA
		{
			get { return _FLAGSINTEGRA; }
			set { _FLAGSINTEGRA = value; }
		}

		public string NDOCUMENTO
		{
			get { return _NDOCUMENTO; }
			set { _NDOCUMENTO = value; }
		}

		public string CST_CSOSN
		{
			get { return _CST_CSOSN; }
			set { _CST_CSOSN = value; }
		}

		public decimal? VLIPI
		{
			get { return _VLIPI; }
			set { _VLIPI = value; }
		}

		public string FLAGENERGIATELECOM
		{
			get { return _FLAGENERGIATELECOM; }
			set { _FLAGENERGIATELECOM = value; }
		}

		public decimal? VLFRETE
		{
			get { return _VLFRETE; }
			set { _VLFRETE = value; }
		}

		public decimal? VLBASEICMSST
		{
			get { return _VLBASEICMSST; }
			set { _VLBASEICMSST = value; }
		}

		public decimal? VLICMSST
		{
			get { return _VLICMSST; }
			set { _VLICMSST = value; }
		}

		public decimal? VLDESCONTOPRODUTO
		{
			get { return _VLDESCONTOPRODUTO; }
			set { _VLDESCONTOPRODUTO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public int? IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
		}

		public string CNPJEMISSOR
		{
			get { return _CNPJEMISSOR; }
			set { _CNPJEMISSOR = value; }
		}

		#endregion
	}
}
