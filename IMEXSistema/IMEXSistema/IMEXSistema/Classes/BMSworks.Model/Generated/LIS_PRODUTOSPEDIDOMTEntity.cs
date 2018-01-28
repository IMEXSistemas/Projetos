using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDIDOMTEntity
	{
		private int? _IDPRODUTOSPEDIDOMT;
		private int? _IDPEDIDO;
		private int? _IDMATERIAL;
		private decimal? _QUANTIDADE;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _MT2;
		private decimal? _VALORMETRO;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private string _NOMEMATERIAL;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDIDOMTEntity() { }

		public LIS_PRODUTOSPEDIDOMTEntity(int? IDPRODUTOSPEDIDOMT, int? IDPEDIDO, int? IDMATERIAL, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, string NOMEMATERIAL)		{

			this._IDPRODUTOSPEDIDOMT = IDPRODUTOSPEDIDOMT;
			this._IDPEDIDO = IDPEDIDO;
			this._IDMATERIAL = IDMATERIAL;
			this._QUANTIDADE = QUANTIDADE;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._MT2 = MT2;
			this._VALORMETRO = VALORMETRO;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._NOMEMATERIAL = NOMEMATERIAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOSPEDIDOMT
		{
			get { return _IDPRODUTOSPEDIDOMT; }
			set { _IDPRODUTOSPEDIDOMT = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
		}

		public int? IDMATERIAL
		{
			get { return _IDMATERIAL; }
			set { _IDMATERIAL = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
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

		public decimal? MT2
		{
			get { return _MT2; }
			set { _MT2 = value; }
		}

		public decimal? VALORMETRO
		{
			get { return _VALORMETRO; }
			set { _VALORMETRO = value; }
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

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public string NOMEMATERIAL
		{
			get { return _NOMEMATERIAL; }
			set { _NOMEMATERIAL = value; }
		}

		#endregion
	}
}
