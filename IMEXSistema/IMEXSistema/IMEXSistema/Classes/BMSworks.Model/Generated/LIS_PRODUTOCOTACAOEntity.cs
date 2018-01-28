using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOCOTACAOEntity
	{
		private int? _IDPRODUTOCOTACAO;
		private int? _IDCOTACAOCOMPRA;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOCOTACAOEntity() { }

		public LIS_PRODUTOCOTACAOEntity(int? IDPRODUTOCOTACAO, int? IDCOTACAOCOMPRA, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDPRODUTO, string NOMEPRODUTO)		{

			this._IDPRODUTOCOTACAO = IDPRODUTOCOTACAO;
			this._IDCOTACAOCOMPRA = IDCOTACAOCOMPRA;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOCOTACAO
		{
			get { return _IDPRODUTOCOTACAO; }
			set { _IDPRODUTOCOTACAO = value; }
		}

		public int? IDCOTACAOCOMPRA
		{
			get { return _IDCOTACAOCOMPRA; }
			set { _IDCOTACAOCOMPRA = value; }
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

		#endregion
	}
}
