using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDIDOEntity
	{
		private int? _IDPRODPEDIDO;
		private int? _IDPEDIDO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEPRODUTO;
		private int? _IDCOR;
		private string _NOMECOR;
		private decimal? _TOTALMT;
		private DateTime? _DTEMISSAO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _FLAGEXIBIR;
		private string _DADOSADICIONAIS;
		private string _FLAGORCAMENTO;
		private string _FLAGBAIXAESTMT;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private int? _IDAMBIENTE;
		private string _NOMEAMBIENTE;
		private int? _IDGRUPOCATEGORIA;
		private int? _IDMARCA;
		private int? _IDPRODUTOMASTER;
		private string _NOMEPRODUTOMASTER;
		private decimal? _PORCPERDAMT;
		private decimal? _TOTALPERDAMT;
		private int? _IDVENDEDOR;
		private int? _IDTRANSPORTES;
		private int? _COD_MUN_IBGE;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDIDOEntity() { }

		public LIS_PRODUTOSPEDIDOEntity(int? IDPRODPEDIDO, int? IDPEDIDO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEPRODUTO, int? IDCOR, string NOMECOR, decimal? TOTALMT, DateTime? DTEMISSAO, int? IDCLIENTE, string NOMECLIENTE, string FLAGEXIBIR, string DADOSADICIONAIS, string FLAGORCAMENTO, string FLAGBAIXAESTMT, decimal? ALTURA, decimal? LARGURA, int? IDAMBIENTE, string NOMEAMBIENTE, int? IDGRUPOCATEGORIA, int? IDMARCA, int? IDPRODUTOMASTER, string NOMEPRODUTOMASTER, decimal? PORCPERDAMT, decimal? TOTALPERDAMT, int? IDVENDEDOR, int? IDTRANSPORTES, int? COD_MUN_IBGE)		{

			this._IDPRODPEDIDO = IDPRODPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._IDCOR = IDCOR;
			this._NOMECOR = NOMECOR;
			this._TOTALMT = TOTALMT;
			this._DTEMISSAO = DTEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._DADOSADICIONAIS = DADOSADICIONAIS;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._FLAGBAIXAESTMT = FLAGBAIXAESTMT;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._IDAMBIENTE = IDAMBIENTE;
			this._NOMEAMBIENTE = NOMEAMBIENTE;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._IDMARCA = IDMARCA;
			this._IDPRODUTOMASTER = IDPRODUTOMASTER;
			this._NOMEPRODUTOMASTER = NOMEPRODUTOMASTER;
			this._PORCPERDAMT = PORCPERDAMT;
			this._TOTALPERDAMT = TOTALPERDAMT;
			this._IDVENDEDOR = IDVENDEDOR;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODPEDIDO
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

		public decimal? COMISSAOPEDIDO
		{
			get { return _COMISSAOPEDIDO; }
			set { _COMISSAOPEDIDO = value; }
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

		public decimal? TOTALMT
		{
			get { return _TOTALMT; }
			set { _TOTALMT = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
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

		public string NOMEAMBIENTE
		{
			get { return _NOMEAMBIENTE; }
			set { _NOMEAMBIENTE = value; }
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

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
		}

		public int? COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
		}

		#endregion
	}
}
