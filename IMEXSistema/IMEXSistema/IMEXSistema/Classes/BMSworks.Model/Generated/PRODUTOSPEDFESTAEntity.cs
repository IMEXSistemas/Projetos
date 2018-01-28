using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDFESTAEntity
	{
		private int _IDPRODPEDFESTA;
		private int? _IDPEDIDOFESTA;
		private int? _IDPRODUTOS;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;


		#region Construtores

		//Construtor default
		public PRODUTOSPEDFESTAEntity() {
			this._IDPEDIDOFESTA = null;
			this._IDPRODUTOS = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
		}

		public PRODUTOSPEDFESTAEntity(int IDPRODPEDFESTA, int? IDPEDIDOFESTA, int? IDPRODUTOS, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO) {

			this._IDPRODPEDFESTA = IDPRODPEDFESTA;
			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDPRODUTOS = IDPRODUTOS;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODPEDFESTA
		{
			get { return _IDPRODPEDFESTA; }
			set { _IDPRODPEDFESTA = value; }
		}

		public int? IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
		}

		public int? IDPRODUTOS
		{
			get { return _IDPRODUTOS; }
			set { _IDPRODUTOS = value; }
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
	

		#endregion
	}
}
