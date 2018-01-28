using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOCOMPOSICAOEntity
	{
		private int _IDPRODUTOCOMPOSICAO;
		private int? _IDFORNECEDOR;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private int? _IDUNIDADE;
		private decimal? _VALOR;
		private int? _IDPRODUTOMAIN;
		private decimal? _VALORTOTAL;
		private string _FLAGEXIBIR;
		private string _FLAGMATERIAPRIMA;
        private string _FLAGMETRO2;
        private decimal? _ALTURA;
        private decimal? _LARGURA;

		#region Construtores

		//Construtor default
		public PRODUTOCOMPOSICAOEntity() {
			this._IDFORNECEDOR = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._IDUNIDADE = null;
			this._VALOR = null;
			this._IDPRODUTOMAIN = null;
			this._VALORTOTAL = null;
		}

		public PRODUTOCOMPOSICAOEntity(int IDPRODUTOCOMPOSICAO, int? IDFORNECEDOR, int? IDPRODUTO, decimal? QUANTIDADE, int? IDUNIDADE, decimal? VALOR, 
            int? IDPRODUTOMAIN, decimal? VALORTOTAL, string FLAGEXIBIR, string FLAGMATERIAPRIMA,
             string FLAGMETRO2, decimal? ALTURA, decimal? LARGURA)
        {

			this._IDPRODUTOCOMPOSICAO = IDPRODUTOCOMPOSICAO;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._IDUNIDADE = IDUNIDADE;
			this._VALOR = VALOR;
			this._IDPRODUTOMAIN = IDPRODUTOMAIN;
			this._VALORTOTAL = VALORTOTAL;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._FLAGMATERIAPRIMA = FLAGMATERIAPRIMA;
            this._FLAGMETRO2 = FLAGMETRO2;
            this._ALTURA = ALTURA;
            this._LARGURA = LARGURA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOCOMPOSICAO
		{
			get { return _IDPRODUTOCOMPOSICAO; }
			set { _IDPRODUTOCOMPOSICAO = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
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

		public int? IDUNIDADE
		{
			get { return _IDUNIDADE; }
			set { _IDUNIDADE = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public int? IDPRODUTOMAIN
		{
			get { return _IDPRODUTOMAIN; }
			set { _IDPRODUTOMAIN = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public string FLAGMATERIAPRIMA
		{
			get { return _FLAGMATERIAPRIMA; }
			set { _FLAGMATERIAPRIMA = value; }
		}

        public string FLAGMETRO2
        {
            get { return _FLAGMETRO2; }
            set { _FLAGMETRO2 = value; }
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

		#endregion
	}
}
