using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDIDOMTEntity
	{
		private int _IDPRODUTOSPEDIDOMT;
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

		#region Construtores

		//Construtor default
		public PRODUTOSPEDIDOMTEntity() {
			this._IDPEDIDO = null;
			this._IDMATERIAL = null;
			this._QUANTIDADE = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._MT2 = null;
			this._VALORMETRO = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
		}

		public PRODUTOSPEDIDOMTEntity(int IDPRODUTOSPEDIDOMT, int? IDPEDIDO, int? IDMATERIAL, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO) {

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
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOSPEDIDOMT
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

		#endregion
	}
}
