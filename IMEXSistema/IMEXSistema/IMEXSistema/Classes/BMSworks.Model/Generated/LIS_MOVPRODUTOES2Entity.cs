using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MOVPRODUTOES2Entity
	{
		private int? _IDMOVPRODUTOES;
		private int? _IDESTOQUEES;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORCUNITARIO;
		private decimal? _VALORTOTAL;
		private string _FLAGATUALIZACUSTO;
		private DateTime? _DATAMOVIMENTACAO;
		private string _NOMEFORNECEDOR;
		private string _NOMEMOVIMENTACAO;
		private decimal? _SALDOESTOQUE;

		#region Construtores

		//Construtor default
		public LIS_MOVPRODUTOES2Entity() { }

		public LIS_MOVPRODUTOES2Entity(int? IDMOVPRODUTOES, int? IDESTOQUEES, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORCUNITARIO, decimal? VALORTOTAL, string FLAGATUALIZACUSTO, DateTime? DATAMOVIMENTACAO, string NOMEFORNECEDOR, string NOMEMOVIMENTACAO, decimal? SALDOESTOQUE)		{

			this._IDMOVPRODUTOES = IDMOVPRODUTOES;
			this._IDESTOQUEES = IDESTOQUEES;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORCUNITARIO = VALORCUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._FLAGATUALIZACUSTO = FLAGATUALIZACUSTO;
			this._DATAMOVIMENTACAO = DATAMOVIMENTACAO;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._NOMEMOVIMENTACAO = NOMEMOVIMENTACAO;
			this._SALDOESTOQUE = SALDOESTOQUE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMOVPRODUTOES
		{
			get { return _IDMOVPRODUTOES; }
			set { _IDMOVPRODUTOES = value; }
		}

		public int? IDESTOQUEES
		{
			get { return _IDESTOQUEES; }
			set { _IDESTOQUEES = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? VALORCUNITARIO
		{
			get { return _VALORCUNITARIO; }
			set { _VALORCUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public string FLAGATUALIZACUSTO
		{
			get { return _FLAGATUALIZACUSTO; }
			set { _FLAGATUALIZACUSTO = value; }
		}

		public DateTime? DATAMOVIMENTACAO
		{
			get { return _DATAMOVIMENTACAO; }
			set { _DATAMOVIMENTACAO = value; }
		}

		public string NOMEFORNECEDOR
		{
			get { return _NOMEFORNECEDOR; }
			set { _NOMEFORNECEDOR = value; }
		}

		public string NOMEMOVIMENTACAO
		{
			get { return _NOMEMOVIMENTACAO; }
			set { _NOMEMOVIMENTACAO = value; }
		}

		public decimal? SALDOESTOQUE
		{
			get { return _SALDOESTOQUE; }
			set { _SALDOESTOQUE = value; }
		}

		#endregion
	}
}
