using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODCONSIGNACAOEntity
	{
		private int? _IDPRODCONSIGNACAO;
		private int? _IDCONSIGNACAO;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public LIS_PRODCONSIGNACAOEntity() { }

		public LIS_PRODCONSIGNACAOEntity(int? IDPRODCONSIGNACAO, int? IDCONSIGNACAO, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL)		{

			this._IDPRODCONSIGNACAO = IDPRODCONSIGNACAO;
			this._IDCONSIGNACAO = IDCONSIGNACAO;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODCONSIGNACAO
		{
			get { return _IDPRODCONSIGNACAO; }
			set { _IDPRODCONSIGNACAO = value; }
		}

		public int? IDCONSIGNACAO
		{
			get { return _IDCONSIGNACAO; }
			set { _IDCONSIGNACAO = value; }
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

		#endregion
	}
}
