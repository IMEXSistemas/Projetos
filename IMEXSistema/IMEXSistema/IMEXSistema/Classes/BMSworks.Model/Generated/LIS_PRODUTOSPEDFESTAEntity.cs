using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDFESTAEntity
	{
		private int? _IDPRODPEDFESTA;
		private int? _IDPEDIDOFESTA;
		private int? _IDPRODUTOS;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEPRODUTO;
		private string _FLAGORCAMENTO;
		private DateTime? _DTEMISSAO;
		private int? _IDVENDEDOR;
		private int? _IDSTATUS;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDFESTAEntity() { }

		public LIS_PRODUTOSPEDFESTAEntity(int? IDPRODPEDFESTA, int? IDPEDIDOFESTA, int? IDPRODUTOS, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEPRODUTO, string FLAGORCAMENTO, DateTime? DTEMISSAO, int? IDVENDEDOR, int? IDSTATUS)		{

			this._IDPRODPEDFESTA = IDPRODPEDFESTA;
			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDPRODUTOS = IDPRODUTOS;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._DTEMISSAO = DTEMISSAO;
			this._IDVENDEDOR = IDVENDEDOR;
			this._IDSTATUS = IDSTATUS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODPEDFESTA
		{
			get { return _IDPRODPEDFESTA; }
			set { _IDPRODPEDFESTA = value; }
		}

		public int? IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
		}

		public int? IDPRODUTOS
		{
			get { return _IDPRODUTOS; }
			set { _IDPRODUTOS = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		#endregion
	}
}
