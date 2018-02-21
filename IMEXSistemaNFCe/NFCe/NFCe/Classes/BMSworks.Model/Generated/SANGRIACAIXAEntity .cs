using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SANGRIACAIXAEntity
    {
        private int _IDSANGRIACAIXA;

        private DateTime? _DATA;
		private int? _IDFUNCIONARIO;
		private decimal? _VALOR;

		#region Construtores

		//Construtor default
		public SANGRIACAIXAEntity() {
			this._DATA = null;
			this._IDFUNCIONARIO = null;
			this._VALOR = null;
		}

		public SANGRIACAIXAEntity(int IDSANGRIACAIXA, DateTime? DATA, int? IDFUNCIONARIO, decimal? VALOR) {

			this._IDSANGRIACAIXA = IDSANGRIACAIXA;
			this._DATA = DATA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._VALOR = VALOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSANGRIACAIXA
        {
			get { return _IDSANGRIACAIXA; }
			set { _IDSANGRIACAIXA = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		#endregion
	}
}
