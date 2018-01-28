using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MATERIALPEDIDOEntity
	{
		private int? _IDMATERIALPEDIDO;
		private int? _IDPEDIDOMARC;
		private int? _IDMATERIAL;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEMADEIRA;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _COMPRIMENTO;

		#region Construtores

		//Construtor default
		public LIS_MATERIALPEDIDOEntity() { }

		public LIS_MATERIALPEDIDOEntity(int? IDMATERIALPEDIDO, int? IDPEDIDOMARC, int? IDMATERIAL, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEMADEIRA, decimal? ALTURA, decimal? LARGURA, decimal? COMPRIMENTO)		{

			this._IDMATERIALPEDIDO = IDMATERIALPEDIDO;
			this._IDPEDIDOMARC = IDPEDIDOMARC;
			this._IDMATERIAL = IDMATERIAL;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEMADEIRA = NOMEMADEIRA;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._COMPRIMENTO = COMPRIMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMATERIALPEDIDO
		{
			get { return _IDMATERIALPEDIDO; }
			set { _IDMATERIALPEDIDO = value; }
		}

		public int? IDPEDIDOMARC
		{
			get { return _IDPEDIDOMARC; }
			set { _IDPEDIDOMARC = value; }
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

		public string NOMEMADEIRA
		{
			get { return _NOMEMADEIRA; }
			set { _NOMEMADEIRA = value; }
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

		public decimal? COMPRIMENTO
		{
			get { return _COMPRIMENTO; }
			set { _COMPRIMENTO = value; }
		}

		#endregion
	}
}
