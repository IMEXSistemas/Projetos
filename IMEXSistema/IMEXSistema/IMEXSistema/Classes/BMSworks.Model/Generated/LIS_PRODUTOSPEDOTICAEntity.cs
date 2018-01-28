using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDOTICAEntity
	{
		private int? _IDPRODPEDOTICA;
		private int? _IDPEDIDOOTICA;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private string _NOMEPRODUTO;
		private decimal? _COMISSAOPEDIDO;
		private DateTime? _DTEMISSAO;
		private int? _IDCLIENTE;
		private string _NUMREFERENCIA;
		private int? _IDSTATUS;
		private decimal? _PORCDECONTO;
		private decimal? _VLUNITLIQUIDO;
		private string _FLAGORCAMENTO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDOTICAEntity() { }

		public LIS_PRODUTOSPEDOTICAEntity(int? IDPRODPEDOTICA, int? IDPEDIDOOTICA, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, string NOMEPRODUTO, decimal? COMISSAOPEDIDO, DateTime? DTEMISSAO, int? IDCLIENTE, string NUMREFERENCIA, int? IDSTATUS, decimal? PORCDECONTO, decimal? VLUNITLIQUIDO, string FLAGORCAMENTO)		{

			this._IDPRODPEDOTICA = IDPRODPEDOTICA;
			this._IDPEDIDOOTICA = IDPEDIDOOTICA;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._DTEMISSAO = DTEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._NUMREFERENCIA = NUMREFERENCIA;
			this._IDSTATUS = IDSTATUS;
			this._PORCDECONTO = PORCDECONTO;
			this._VLUNITLIQUIDO = VLUNITLIQUIDO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODPEDOTICA
		{
			get { return _IDPRODPEDOTICA; }
			set { _IDPRODPEDOTICA = value; }
		}

		public int? IDPEDIDOOTICA
		{
			get { return _IDPEDIDOOTICA; }
			set { _IDPEDIDOOTICA = value; }
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

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public decimal? COMISSAOPEDIDO
		{
			get { return _COMISSAOPEDIDO; }
			set { _COMISSAOPEDIDO = value; }
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

		public string NUMREFERENCIA
		{
			get { return _NUMREFERENCIA; }
			set { _NUMREFERENCIA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public decimal? PORCDECONTO
		{
			get { return _PORCDECONTO; }
			set { _PORCDECONTO = value; }
		}

		public decimal? VLUNITLIQUIDO
		{
			get { return _VLUNITLIQUIDO; }
			set { _VLUNITLIQUIDO = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		#endregion
	}
}
