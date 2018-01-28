using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MATERIALPEDIDOEntity
	{
		private int _IDMATERIALPEDIDO;
		private int? _IDPEDIDO;
		private int? _IDMATERIAL;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _COMPRIMENTO;

		#region Construtores

		//Construtor default
		public MATERIALPEDIDOEntity() {
			this._IDPEDIDO = null;
			this._IDMATERIAL = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._COMPRIMENTO = null;
		}

		public MATERIALPEDIDOEntity(int IDMATERIALPEDIDO, int? IDPEDIDO, int? IDMATERIAL, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? ALTURA, decimal? LARGURA, decimal? COMPRIMENTO) {

			this._IDMATERIALPEDIDO = IDMATERIALPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._IDMATERIAL = IDMATERIAL;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._COMPRIMENTO = COMPRIMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMATERIALPEDIDO
		{
			get { return _IDMATERIALPEDIDO; }
			set { _IDMATERIALPEDIDO = value; }
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
