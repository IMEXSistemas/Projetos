using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MOVPRODUTOESEntity
	{
		private int _IDMOVPRODUTOES;
		private int? _IDESTOQUEES;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORCUNITARIO;
		private decimal? _VALORTOTAL;
		private string _FLAGATUALIZACUSTO;
		private decimal? _SALDOESTOQUE;
		private decimal? _ALQICMS;
		private int? _IDCFOP;
		private decimal? _BASEICMS;
		private decimal? _VLICMS;
		private string _CST_CSOSN;
		private decimal? _VLIPI;
		private decimal? _VLFRETE;
		private decimal? _VLBASEICMSST;
		private decimal? _VLICMSST;
        private decimal? _VLDESCONTOPRODUTO;
        

		#region Construtores

		//Construtor default
		public MOVPRODUTOESEntity() {
			this._IDESTOQUEES = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORCUNITARIO = null;
			this._VALORTOTAL = null;
			this._SALDOESTOQUE = null;
			this._ALQICMS = null;
			this._IDCFOP = null;
			this._BASEICMS = null;
			this._VLICMS = null;
			this._VLIPI = null;
			this._VLFRETE = null;
			this._VLBASEICMSST = null;
			this._VLICMSST = null;
		}

        public MOVPRODUTOESEntity(int IDMOVPRODUTOES, int? IDESTOQUEES, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORCUNITARIO, decimal? VALORTOTAL, string FLAGATUALIZACUSTO, decimal? SALDOESTOQUE, decimal? ALQICMS, int? IDCFOP, decimal? BASEICMS, decimal? VLICMS, string CST_CSOSN, decimal? VLIPI, decimal? VLFRETE, decimal? VLBASEICMSST, decimal? VLICMSST, decimal? VLDESCONTOPRODUTO)
        {

			this._IDMOVPRODUTOES = IDMOVPRODUTOES;
			this._IDESTOQUEES = IDESTOQUEES;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORCUNITARIO = VALORCUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._FLAGATUALIZACUSTO = FLAGATUALIZACUSTO;
			this._SALDOESTOQUE = SALDOESTOQUE;
			this._ALQICMS = ALQICMS;
			this._IDCFOP = IDCFOP;
			this._BASEICMS = BASEICMS;
			this._VLICMS = VLICMS;
			this._CST_CSOSN = CST_CSOSN;
			this._VLIPI = VLIPI;
			this._VLFRETE = VLFRETE;
			this._VLBASEICMSST = VLBASEICMSST;
			this._VLICMSST = VLICMSST;
            this._VLDESCONTOPRODUTO = VLDESCONTOPRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMOVPRODUTOES
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

		public decimal? SALDOESTOQUE
		{
			get { return _SALDOESTOQUE; }
			set { _SALDOESTOQUE = value; }
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

        

		#endregion
	}
}
