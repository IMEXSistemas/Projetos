using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SANGRIACAIXAEntity
    {
		private int? _IDSANGRIACAIXA;
		private DateTime? _DATA;
		private int? _IDFUNCIONARIO;
		private decimal? _VALOR;
		private string _NOMEFUNCIONARIO;

		#region Construtores

		//Construtor default
		public LIS_SANGRIACAIXAEntity() { }

		public LIS_SANGRIACAIXAEntity(int? IDSANGRIACAIXA, DateTime? DATA, int? IDFUNCIONARIO, decimal? VALOR, string NOMEFUNCIONARIO)		{

			this._IDSANGRIACAIXA = IDSANGRIACAIXA;
			this._DATA = DATA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._VALOR = VALOR;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSANGRIACAIXA
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

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		#endregion
	}
}
