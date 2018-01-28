using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOPEDMARC2Entity
	{
		private int _IDPRODUTOPEDMARC2;
		private int? _PEDIDOMARC;
		private decimal? _QUANT;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _COMPRIMENTO;
		private decimal? _TOTALMT3;
		private decimal? _VLUNITARIO;
		private decimal? _VLTOTAL;
		private string _DADOSADICIONAIS;
		private int? _IDPRODUTO;

		#region Construtores

		//Construtor default
		public PRODUTOPEDMARC2Entity() {
			this._PEDIDOMARC = null;
			this._QUANT = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._COMPRIMENTO = null;
			this._TOTALMT3 = null;
			this._VLUNITARIO = null;
			this._VLTOTAL = null;
			this._IDPRODUTO = null;
		}

		public PRODUTOPEDMARC2Entity(int IDPRODUTOPEDMARC2, int? PEDIDOMARC, decimal? QUANT, decimal? ALTURA, decimal? LARGURA, decimal? COMPRIMENTO, decimal? TOTALMT3, decimal? VLUNITARIO, decimal? VLTOTAL, string DADOSADICIONAIS, int? IDPRODUTO) {

			this._IDPRODUTOPEDMARC2 = IDPRODUTOPEDMARC2;
			this._PEDIDOMARC = PEDIDOMARC;
			this._QUANT = QUANT;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._COMPRIMENTO = COMPRIMENTO;
			this._TOTALMT3 = TOTALMT3;
			this._VLUNITARIO = VLUNITARIO;
			this._VLTOTAL = VLTOTAL;
			this._DADOSADICIONAIS = DADOSADICIONAIS;
			this._IDPRODUTO = IDPRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOPEDMARC2
		{
			get { return _IDPRODUTOPEDMARC2; }
			set { _IDPRODUTOPEDMARC2 = value; }
		}

		public int? PEDIDOMARC
		{
			get { return _PEDIDOMARC; }
			set { _PEDIDOMARC = value; }
		}

		public decimal? QUANT
		{
			get { return _QUANT; }
			set { _QUANT = value; }
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

		public decimal? TOTALMT3
		{
			get { return _TOTALMT3; }
			set { _TOTALMT3 = value; }
		}

		public decimal? VLUNITARIO
		{
			get { return _VLUNITARIO; }
			set { _VLUNITARIO = value; }
		}

		public decimal? VLTOTAL
		{
			get { return _VLTOTAL; }
			set { _VLTOTAL = value; }
		}

		public string DADOSADICIONAIS
		{
			get { return _DADOSADICIONAIS; }
			set { _DADOSADICIONAIS = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		#endregion
	}
}
