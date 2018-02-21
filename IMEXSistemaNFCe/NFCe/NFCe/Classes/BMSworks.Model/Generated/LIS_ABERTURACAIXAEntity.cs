using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ABERTURACAIXAEntity
	{
		private int? _IDABERTURACAIXA;
		private DateTime? _DATA;
		private int? _IDFUNCIONARIO;
		private decimal? _VALOR;
		private string _NOMEFUNCIONARIO;

		#region Construtores

		//Construtor default
		public LIS_ABERTURACAIXAEntity() { }

		public LIS_ABERTURACAIXAEntity(int? IDABERTURACAIXA, DateTime? DATA, int? IDFUNCIONARIO, decimal? VALOR, string NOMEFUNCIONARIO)		{

			this._IDABERTURACAIXA = IDABERTURACAIXA;
			this._DATA = DATA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._VALOR = VALOR;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDABERTURACAIXA
		{
			get { return _IDABERTURACAIXA; }
			set { _IDABERTURACAIXA = value; }
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
