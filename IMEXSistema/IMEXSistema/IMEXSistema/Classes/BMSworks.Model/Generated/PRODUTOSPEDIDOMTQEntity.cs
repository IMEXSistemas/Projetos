using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDIDOMTQEntity
	{
		private int _IDPRODUTOSPEDIDOMTQ;
		private int? _IDPEDIDO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _MT2;
		private decimal? _VALORMETRO;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private int? _IDCOR;
		private string _FLAGEXIBIR;
		private string _DADOADICIONAIS;
		private int? _IDAMBIENTE;
		private int? _IDPRODUTOMASTER;
        private decimal? _PORCPERDA;
        private decimal? _TOTALPERDA;

		#region Construtores

		//Construtor default
		public PRODUTOSPEDIDOMTQEntity() {
			this._IDPEDIDO = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._MT2 = null;
			this._VALORMETRO = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._IDCOR = null;
			this._IDAMBIENTE = null;
			this._IDPRODUTOMASTER = null;
		}

        public PRODUTOSPEDIDOMTQEntity(int IDPRODUTOSPEDIDOMTQ, int? IDPEDIDO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, int? IDCOR, string FLAGEXIBIR, string DADOADICIONAIS, int? IDAMBIENTE, int? IDPRODUTOMASTER, decimal? PORCPERDA, decimal? TOTALPERDA)
        {

			this._IDPRODUTOSPEDIDOMTQ = IDPRODUTOSPEDIDOMTQ;
			this._IDPEDIDO = IDPEDIDO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._MT2 = MT2;
			this._VALORMETRO = VALORMETRO;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._IDCOR = IDCOR;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._DADOADICIONAIS = DADOADICIONAIS;
			this._IDAMBIENTE = IDAMBIENTE;
			this._IDPRODUTOMASTER = IDPRODUTOMASTER;
            this._PORCPERDA = PORCPERDA;
            this._TOTALPERDA = TOTALPERDA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOSPEDIDOMTQ
		{
			get { return _IDPRODUTOSPEDIDOMTQ; }
			set { _IDPRODUTOSPEDIDOMTQ = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
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

		public decimal? ALTURA
		{
			get { return _ALTURA; }
			set { _ALTURA = value; }
		}

		public decimal? LARGURA
		{
			get { return _LARGURA; }
			set { _LARGURA = value; }
		}

		public decimal? MT2
		{
			get { return _MT2; }
			set { _MT2 = value; }
		}

		public decimal? VALORMETRO
		{
			get { return _VALORMETRO; }
			set { _VALORMETRO = value; }
		}

		public decimal? VALORUNITARIO
		{
			get { return _VALORUNITARIO; }
			set { _VALORUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public int? IDCOR
		{
			get { return _IDCOR; }
			set { _IDCOR = value; }
		}

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public string DADOADICIONAIS
		{
			get { return _DADOADICIONAIS; }
			set { _DADOADICIONAIS = value; }
		}

		public int? IDAMBIENTE
		{
			get { return _IDAMBIENTE; }
			set { _IDAMBIENTE = value; }
		}

		public int? IDPRODUTOMASTER
		{
			get { return _IDPRODUTOMASTER; }
			set { _IDPRODUTOMASTER = value; }
		}

        public decimal? PORCPERDA
        {
            get { return _PORCPERDA; }
            set { _PORCPERDA = value; }
        }

        public decimal? TOTALPERDA
        {
            get { return _TOTALPERDA; }
            set { _TOTALPERDA = value; }
        }

		#endregion
	}
}
