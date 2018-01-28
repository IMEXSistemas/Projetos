using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICONFEEntity
	{
		private int _IDSERVICONFE;
		private int? _IDSERVICO;
		private int? _IDNOTAFISCALE;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _ALIQISSQN;
		private decimal? _VALORISSQN;

		#region Construtores

		//Construtor default
		public SERVICONFEEntity() {
			this._IDSERVICO = null;
			this._IDNOTAFISCALE = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._ALIQISSQN = null;
			this._VALORISSQN = null;
		}

		public SERVICONFEEntity(int IDSERVICONFE, int? IDSERVICO, int? IDNOTAFISCALE, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? ALIQISSQN, decimal? VALORISSQN) {

			this._IDSERVICONFE = IDSERVICONFE;
			this._IDSERVICO = IDSERVICO;
			this._IDNOTAFISCALE = IDNOTAFISCALE;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._ALIQISSQN = ALIQISSQN;
			this._VALORISSQN = VALORISSQN;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSERVICONFE
		{
			get { return _IDSERVICONFE; }
			set { _IDSERVICONFE = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
		}

		public int? IDNOTAFISCALE
		{
			get { return _IDNOTAFISCALE; }
			set { _IDNOTAFISCALE = value; }
		}

		public int? QUANTIDADE
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

		public decimal? ALIQISSQN
		{
			get { return _ALIQISSQN; }
			set { _ALIQISSQN = value; }
		}

		public decimal? VALORISSQN
		{
			get { return _VALORISSQN; }
			set { _VALORISSQN = value; }
		}

		#endregion
	}
}
