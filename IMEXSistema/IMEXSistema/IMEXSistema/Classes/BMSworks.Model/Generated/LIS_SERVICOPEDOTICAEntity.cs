using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SERVICOPEDOTICAEntity
	{
		private int? _IDPRODPEDOTICA;
		private int? _IDPEDIDOOTICA;
		private int? _IDSERVICO;
		private string _NOMESERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private DateTime? _DTEMISSAO;
		private int? _IDCLIENTE;

		#region Construtores

		//Construtor default
		public LIS_SERVICOPEDOTICAEntity() { }

		public LIS_SERVICOPEDOTICAEntity(int? IDPRODPEDOTICA, int? IDPEDIDOOTICA, int? IDSERVICO, string NOMESERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, DateTime? DTEMISSAO, int? IDCLIENTE)		{

			this._IDPRODPEDOTICA = IDPRODPEDOTICA;
			this._IDPEDIDOOTICA = IDPEDIDOOTICA;
			this._IDSERVICO = IDSERVICO;
			this._NOMESERVICO = NOMESERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._DTEMISSAO = DTEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
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

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
		}

		public string NOMESERVICO
		{
			get { return _NOMESERVICO; }
			set { _NOMESERVICO = value; }
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

		#endregion
	}
}
