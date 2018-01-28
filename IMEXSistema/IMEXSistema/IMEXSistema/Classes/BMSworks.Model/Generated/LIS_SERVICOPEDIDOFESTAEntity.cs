using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SERVICOPEDIDOFESTAEntity
	{
		private int? _IDSERVICOPEDIDOFESTA;
		private int? _IDPEDIDOFESTA;
		private int? _IDSERVICO;
		private string _NOMESERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private string _FLAGORCAMENTO;
		private int? _IDSTATUS;
		private DateTime? _DTEMISSAO;
		private int? _IDVENDEDOR;

		#region Construtores

		//Construtor default
		public LIS_SERVICOPEDIDOFESTAEntity() { }

		public LIS_SERVICOPEDIDOFESTAEntity(int? IDSERVICOPEDIDOFESTA, int? IDPEDIDOFESTA, int? IDSERVICO, string NOMESERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, string FLAGORCAMENTO, int? IDSTATUS, DateTime? DTEMISSAO, int? IDVENDEDOR)		{

			this._IDSERVICOPEDIDOFESTA = IDSERVICOPEDIDOFESTA;
			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDSERVICO = IDSERVICO;
			this._NOMESERVICO = NOMESERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._IDSTATUS = IDSTATUS;
			this._DTEMISSAO = DTEMISSAO;
			this._IDVENDEDOR = IDVENDEDOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSERVICOPEDIDOFESTA
		{
			get { return _IDSERVICOPEDIDOFESTA; }
			set { _IDSERVICOPEDIDOFESTA = value; }
		}

		public int? IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
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

		#endregion
	}
}
