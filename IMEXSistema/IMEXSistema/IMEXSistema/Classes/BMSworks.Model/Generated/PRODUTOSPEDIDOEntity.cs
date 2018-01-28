using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDIDOEntity
	{
		private int _IDPRODPEDIDO;
		private int? _IDPEDIDO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private int? _IDCOR;
		private decimal? _TOTALMT;
		private string _FLAGEXIBIR;
		private string _DADOSADICIONAIS;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private int? _IDAMBIENTE;
		private int? _IDPRODUTOMASTER;
        private decimal? _PORCPERDAMT;
        private decimal? _TOTALPERDAMT;
        private decimal? _BUSTO;
        private decimal? _QUADRIL;
        private decimal? _COLARINHO;
        private decimal? _MANGA;
        private decimal? _BARRA;
        private decimal? _CINTURA;
        private int? _IDTAMANHO;


		#region Construtores

		//Construtor default
		public PRODUTOSPEDIDOEntity() {
			this._IDPEDIDO = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._IDCOR = null;
			this._TOTALMT = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._IDAMBIENTE = null;
			this._IDPRODUTOMASTER = null;
		}

        public PRODUTOSPEDIDOEntity(int IDPRODPEDIDO, int? IDPEDIDO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, int? IDCOR, decimal? TOTALMT, string FLAGEXIBIR, string DADOSADICIONAIS, decimal? ALTURA, decimal? LARGURA, int? IDAMBIENTE, int? IDPRODUTOMASTER, decimal? PORCPERDAMT, decimal? TOTALPERDAMT, decimal? BUSTO, decimal? QUADRIL, decimal? COLARINHO, decimal? MANGA, decimal? BARRA, decimal? CINTURA, int? IDTAMANHO)
        {

			this._IDPRODPEDIDO = IDPRODPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._IDCOR = IDCOR;
			this._TOTALMT = TOTALMT;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._DADOSADICIONAIS = DADOSADICIONAIS;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._IDAMBIENTE = IDAMBIENTE;
			this._IDPRODUTOMASTER = IDPRODUTOMASTER;
            this._PORCPERDAMT = PORCPERDAMT;
            this._TOTALPERDAMT = TOTALPERDAMT;
            this._BUSTO = BUSTO;
            this._QUADRIL = QUADRIL;
            this._COLARINHO = COLARINHO;
            this._MANGA = MANGA;
            this._BARRA = BARRA;
            this._CINTURA = CINTURA;
            this._IDTAMANHO = IDTAMANHO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODPEDIDO
		{
			get { return _IDPRODPEDIDO; }
			set { _IDPRODPEDIDO = value; }
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

		public decimal? TOTALMT
		{
			get { return _TOTALMT; }
			set { _TOTALMT = value; }
		}

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public string DADOSADICIONAIS
		{
			get { return _DADOSADICIONAIS; }
			set { _DADOSADICIONAIS = value; }
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

        public decimal? PORCPERDAMT
        {
            get { return _PORCPERDAMT; }
            set { _PORCPERDAMT = value; }
        }

        public decimal? TOTALPERDAMT
		{
            get { return _TOTALPERDAMT; }
            set { _TOTALPERDAMT = value; }
		}

        public decimal? BUSTO
        {
            get { return _BUSTO; }
            set { _BUSTO = value; }
        }       

         public decimal? QUADRIL
         {
             get { return _QUADRIL; }
             set { _QUADRIL = value; }

         }

        public decimal? COLARINHO
        {
            get { return _COLARINHO; }
            set { _COLARINHO = value; }
        }

          public decimal? MANGA
        {
            get { return _MANGA; }
            set { _MANGA = value; }
        }

          public decimal? BARRA
        {
            get { return _BARRA; }
            set { _BARRA = value; }
        }

          public decimal? CINTURA
          {
              get { return _CINTURA; }
              set { _CINTURA = value; }
          }


          public int? IDTAMANHO
		{
            get { return _IDTAMANHO; }
            set { _IDTAMANHO = value; }
		}
        
		#endregion
	}
}
