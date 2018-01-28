using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class JUROSDUPLICATASEntity
	{
		private int _IDJUROSDUPLICATA;
		private decimal? _MULTAATRASO;
		private decimal? _JUROSDIA;
		private decimal? _TAXA;
		private decimal? _OUTRAS;
		private string _FLAGCALCULAR;

		#region Construtores

		//Construtor default
		public JUROSDUPLICATASEntity() {
			this._MULTAATRASO = null;
			this._JUROSDIA = null;
			this._TAXA = null;
			this._OUTRAS = null;
		}

		public JUROSDUPLICATASEntity(int IDJUROSDUPLICATA, decimal? MULTAATRASO, decimal? JUROSDIA, decimal? TAXA, decimal? OUTRAS, string FLAGCALCULAR) {

			this._IDJUROSDUPLICATA = IDJUROSDUPLICATA;
			this._MULTAATRASO = MULTAATRASO;
			this._JUROSDIA = JUROSDIA;
			this._TAXA = TAXA;
			this._OUTRAS = OUTRAS;
			this._FLAGCALCULAR = FLAGCALCULAR;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDJUROSDUPLICATA
		{
			get { return _IDJUROSDUPLICATA; }
			set { _IDJUROSDUPLICATA = value; }
		}

		public decimal? MULTAATRASO
		{
			get { return _MULTAATRASO; }
			set { _MULTAATRASO = value; }
		}

		public decimal? JUROSDIA
		{
			get { return _JUROSDIA; }
			set { _JUROSDIA = value; }
		}

		public decimal? TAXA
		{
			get { return _TAXA; }
			set { _TAXA = value; }
		}

		public decimal? OUTRAS
		{
			get { return _OUTRAS; }
			set { _OUTRAS = value; }
		}

		public string FLAGCALCULAR
		{
			get { return _FLAGCALCULAR; }
			set { _FLAGCALCULAR = value; }
		}

		#endregion
	}
}
