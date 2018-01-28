using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CUPOMEntity
	{
		private int _CODIGO_EMPRESA;
		private int _CODIGO_CUPOM;
		private DateTime? _DATA_CUPOM;
		private int? _CODIGO_CLIENTE;
		private string _NOME_CLIENTE;
		private string _CPF_CLIENTE;
		private string _ENDERECO_CLIENTE;
		private int? _CODIGO_USUARIO;
		private decimal? _VALOR_CUPOM;
		private int? _CODIGO_PEDIDO;
		private int? _CODIGO_CAIXA;
		private int? _NUMDOC_CUPOM;
		private string _GEROU_NFE;

		#region Construtores

		//Construtor default
		public CUPOMEntity() {
			this._DATA_CUPOM = null;
			this._CODIGO_CLIENTE = null;
			this._CODIGO_USUARIO = null;
			this._VALOR_CUPOM = null;
			this._CODIGO_PEDIDO = null;
			this._CODIGO_CAIXA = null;
			this._NUMDOC_CUPOM = null;
		}

		public CUPOMEntity(int CODIGO_EMPRESA, int CODIGO_CUPOM, DateTime? DATA_CUPOM, int? CODIGO_CLIENTE, string NOME_CLIENTE, string CPF_CLIENTE, string ENDERECO_CLIENTE, int? CODIGO_USUARIO, decimal? VALOR_CUPOM, int? CODIGO_PEDIDO, int? CODIGO_CAIXA, int? NUMDOC_CUPOM, string GEROU_NFE) {

			this._CODIGO_EMPRESA = CODIGO_EMPRESA;
			this._CODIGO_CUPOM = CODIGO_CUPOM;
			this._DATA_CUPOM = DATA_CUPOM;
			this._CODIGO_CLIENTE = CODIGO_CLIENTE;
			this._NOME_CLIENTE = NOME_CLIENTE;
			this._CPF_CLIENTE = CPF_CLIENTE;
			this._ENDERECO_CLIENTE = ENDERECO_CLIENTE;
			this._CODIGO_USUARIO = CODIGO_USUARIO;
			this._VALOR_CUPOM = VALOR_CUPOM;
			this._CODIGO_PEDIDO = CODIGO_PEDIDO;
			this._CODIGO_CAIXA = CODIGO_CAIXA;
			this._NUMDOC_CUPOM = NUMDOC_CUPOM;
			this._GEROU_NFE = GEROU_NFE;
		}
		#endregion

		#region Propriedades Get/Set

		public int CODIGO_EMPRESA
		{
			get { return _CODIGO_EMPRESA; }
			set { _CODIGO_EMPRESA = value; }
		}

		public int CODIGO_CUPOM
		{
			get { return _CODIGO_CUPOM; }
			set { _CODIGO_CUPOM = value; }
		}

		public DateTime? DATA_CUPOM
		{
			get { return _DATA_CUPOM; }
			set { _DATA_CUPOM = value; }
		}

		public int? CODIGO_CLIENTE
		{
			get { return _CODIGO_CLIENTE; }
			set { _CODIGO_CLIENTE = value; }
		}

		public string NOME_CLIENTE
		{
			get { return _NOME_CLIENTE; }
			set { _NOME_CLIENTE = value; }
		}

		public string CPF_CLIENTE
		{
			get { return _CPF_CLIENTE; }
			set { _CPF_CLIENTE = value; }
		}

		public string ENDERECO_CLIENTE
		{
			get { return _ENDERECO_CLIENTE; }
			set { _ENDERECO_CLIENTE = value; }
		}

		public int? CODIGO_USUARIO
		{
			get { return _CODIGO_USUARIO; }
			set { _CODIGO_USUARIO = value; }
		}

		public decimal? VALOR_CUPOM
		{
			get { return _VALOR_CUPOM; }
			set { _VALOR_CUPOM = value; }
		}

		public int? CODIGO_PEDIDO
		{
			get { return _CODIGO_PEDIDO; }
			set { _CODIGO_PEDIDO = value; }
		}

		public int? CODIGO_CAIXA
		{
			get { return _CODIGO_CAIXA; }
			set { _CODIGO_CAIXA = value; }
		}

		public int? NUMDOC_CUPOM
		{
			get { return _NUMDOC_CUPOM; }
			set { _NUMDOC_CUPOM = value; }
		}

		public string GEROU_NFE
		{
			get { return _GEROU_NFE; }
			set { _GEROU_NFE = value; }
		}

		#endregion
	}
}
