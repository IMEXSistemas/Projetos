using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICONPSEntity
	{
		private int _IDSERVICONPS;
		private int? _IDNOTASERVICO;
		private int? _IDSERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _VALORTRIBUTO;

		#region Construtores

		//Construtor default
		public SERVICONPSEntity() {
			this._IDNOTASERVICO = null;
			this._IDSERVICO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._VALORTRIBUTO = null;
		}

		public SERVICONPSEntity(int IDSERVICONPS, int? IDNOTASERVICO, int? IDSERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? VALORTRIBUTO) {

			this._IDSERVICONPS = IDSERVICONPS;
			this._IDNOTASERVICO = IDNOTASERVICO;
			this._IDSERVICO = IDSERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._VALORTRIBUTO = VALORTRIBUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSERVICONPS
		{
			get { return _IDSERVICONPS; }
			set { _IDSERVICONPS = value; }
		}

		public int? IDNOTASERVICO
		{
			get { return _IDNOTASERVICO; }
			set { _IDNOTASERVICO = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
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

		public decimal? VALORTRIBUTO
		{
			get { return _VALORTRIBUTO; }
			set { _VALORTRIBUTO = value; }
		}

		#endregion
	}
}
