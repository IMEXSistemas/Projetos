using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDIDOMTQEntity
	{
		private int? _IDPRODUTOSPEDIDOMTQ;
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
		private string _NOMEPRODUTO;
		private int? _IDCOR;
		private string _NOMECOR;
		private string _FLAGEXIBIR;
		private string _DADOADICIONAIS;
		private string _FLAGORCAMENTO;
		private string _FLAGBAIXAESTMT;
		private DateTime? _DTEMISSAO;
		private int? _IDAMBIENTE;
		private string _NOMEAMBIENTE;
		private int? _IDCLIENTE;
		private int? _IDGRUPOCATEGORIA;
		private int? _IDMARCA;
		private string _NOMECLIENTE;
		private int? _IDPRODUTOMASTER;
		private string _NOMEPRODUTOMASTER;
        private byte[] _FOTO;
        private decimal? _PORCPERDA;
        private decimal? _TOTALPERDA;
        private string _DADOSADICIONAISPRODUTO;
        private int? _IDVENDEDOR;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDIDOMTQEntity() { }

        public LIS_PRODUTOSPEDIDOMTQEntity(int? IDPRODUTOSPEDIDOMTQ, int? IDPEDIDO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, string NOMEPRODUTO, int? IDCOR, string NOMECOR, string FLAGEXIBIR, string DADOADICIONAIS, string FLAGORCAMENTO, string FLAGBAIXAESTMT, DateTime? DTEMISSAO, int? IDAMBIENTE, string NOMEAMBIENTE, int? IDCLIENTE, int? IDGRUPOCATEGORIA, int? IDMARCA, string NOMECLIENTE, int? IDPRODUTOMASTER, string NOMEPRODUTOMASTER, byte[] FOTO, decimal? PORCPERDA, decimal? TOTALPERDA, string DADOSADICIONAISPRODUTO, int? IDVENDEDOR)
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
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._IDCOR = IDCOR;
			this._NOMECOR = NOMECOR;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._DADOADICIONAIS = DADOADICIONAIS;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._FLAGBAIXAESTMT = FLAGBAIXAESTMT;
			this._DTEMISSAO = DTEMISSAO;
			this._IDAMBIENTE = IDAMBIENTE;
			this._NOMEAMBIENTE = NOMEAMBIENTE;
			this._IDCLIENTE = IDCLIENTE;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._IDMARCA = IDMARCA;
			this._NOMECLIENTE = NOMECLIENTE;
			this._IDPRODUTOMASTER = IDPRODUTOMASTER;
			this._NOMEPRODUTOMASTER = NOMEPRODUTOMASTER;
            this._FOTO = FOTO;
            this._PORCPERDA = PORCPERDA;
            this._TOTALPERDA = TOTALPERDA;
            this._DADOSADICIONAISPRODUTO = DADOSADICIONAISPRODUTO;
            this._IDVENDEDOR = IDVENDEDOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOSPEDIDOMTQ
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

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public int? IDCOR
		{
			get { return _IDCOR; }
			set { _IDCOR = value; }
		}

		public string NOMECOR
		{
			get { return _NOMECOR; }
			set { _NOMECOR = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public string FLAGBAIXAESTMT
		{
			get { return _FLAGBAIXAESTMT; }
			set { _FLAGBAIXAESTMT = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public int? IDAMBIENTE
		{
			get { return _IDAMBIENTE; }
			set { _IDAMBIENTE = value; }
		}

		public string NOMEAMBIENTE
		{
			get { return _NOMEAMBIENTE; }
			set { _NOMEAMBIENTE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public int? IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
		}

		public int? IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public int? IDPRODUTOMASTER
		{
			get { return _IDPRODUTOMASTER; }
			set { _IDPRODUTOMASTER = value; }
		}

		public string NOMEPRODUTOMASTER
		{
			get { return _NOMEPRODUTOMASTER; }
			set { _NOMEPRODUTOMASTER = value; }
		}

        public byte[] FOTO
        {
            get { return _FOTO; }
            set { _FOTO = value; }

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

        public string DADOSADICIONAISPRODUTO
		{
            get { return _DADOSADICIONAISPRODUTO; }
            set { _DADOSADICIONAISPRODUTO = value; }
		}

        public int? IDVENDEDOR
		{
            get { return _IDVENDEDOR; }
            set { _IDVENDEDOR = value; }
        }        
        

		#endregion
	}
}
