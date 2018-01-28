using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDMARCEntity
	{
		private int? _IDPRODUTOSPEDMARC;
		private int? _IDPEDIDOMARC;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEPRODUTO;
		private string _FLAGORCAMENTO;
		private DateTime? _DTEMISSAO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDMARCEntity() { }

		public LIS_PRODUTOSPEDMARCEntity(int? IDPRODUTOSPEDMARC, int? IDPEDIDOMARC, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEPRODUTO, string FLAGORCAMENTO, DateTime? DTEMISSAO)		{

			this._IDPRODUTOSPEDMARC = IDPRODUTOSPEDMARC;
			this._IDPEDIDOMARC = IDPEDIDOMARC;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._DTEMISSAO = DTEMISSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOSPEDMARC
		{
			get { return _IDPRODUTOSPEDMARC; }
			set { _IDPRODUTOSPEDMARC = value; }
		}

		public int? IDPEDIDOMARC
		{
			get { return _IDPEDIDOMARC; }
			set { _IDPEDIDOMARC = value; }
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

		#endregion
	}
}
