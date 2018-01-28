using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDMARCEntity
	{
		private int _IDPRODUTOSPEDMARC;
		private int? _IDPEDIDOMARC;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;

		#region Construtores

		//Construtor default
		public PRODUTOSPEDMARCEntity() {
			this._IDPEDIDOMARC = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
		}

		public PRODUTOSPEDMARCEntity(int IDPRODUTOSPEDMARC, int? IDPEDIDOMARC, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO) {

			this._IDPRODUTOSPEDMARC = IDPRODUTOSPEDMARC;
			this._IDPEDIDOMARC = IDPEDIDOMARC;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOSPEDMARC
		{
			get { return _IDPRODUTOSPEDMARC; }
			set { _IDPRODUTOSPEDMARC = value; }
		}

		public int? IDPEDIDOMARC
		{
			get { return _IDPEDIDOMARC; }
			set { _IDPEDIDOMARC = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
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
