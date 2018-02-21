using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ABERTURACAIXAEntity
	{
		private int _IDABERTURACAIXA;
		private DateTime? _DATA;
		private int? _IDFUNCIONARIO;
		private decimal? _VALOR;

		#region Construtores

		//Construtor default
		public ABERTURACAIXAEntity() {
			this._DATA = null;
			this._IDFUNCIONARIO = null;
			this._VALOR = null;
		}

		public ABERTURACAIXAEntity(int IDABERTURACAIXA, DateTime? DATA, int? IDFUNCIONARIO, decimal? VALOR) {

			this._IDABERTURACAIXA = IDABERTURACAIXA;
			this._DATA = DATA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._VALOR = VALOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDABERTURACAIXA
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

		#endregion
	}
}
