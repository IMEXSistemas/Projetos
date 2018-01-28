using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITENSFORMAPAGTOEntity
	{
		private int _IDITENSFORMAPAGTO;
		private int? _IDFORMAPAGAMENTO;
		private int? _DIAS;
		private decimal? _PORCPAGTO;
		private decimal? _PORCJUROS;

		#region Construtores

		//Construtor default
		public ITENSFORMAPAGTOEntity() {
			this._IDFORMAPAGAMENTO = null;
			this._DIAS = null;
			this._PORCPAGTO = null;
			this._PORCJUROS = null;
		}

		public ITENSFORMAPAGTOEntity(int IDITENSFORMAPAGTO, int? IDFORMAPAGAMENTO, int? DIAS, decimal? PORCPAGTO, decimal? PORCJUROS) {

			this._IDITENSFORMAPAGTO = IDITENSFORMAPAGTO;
			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._DIAS = DIAS;
			this._PORCPAGTO = PORCPAGTO;
			this._PORCJUROS = PORCJUROS;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDITENSFORMAPAGTO
		{
			get { return _IDITENSFORMAPAGTO; }
			set { _IDITENSFORMAPAGTO = value; }
		}

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public int? DIAS
		{
			get { return _DIAS; }
			set { _DIAS = value; }
		}

		public decimal? PORCPAGTO
		{
			get { return _PORCPAGTO; }
			set { _PORCPAGTO = value; }
		}

		public decimal? PORCJUROS
		{
			get { return _PORCJUROS; }
			set { _PORCJUROS = value; }
		}

		#endregion
	}
}
