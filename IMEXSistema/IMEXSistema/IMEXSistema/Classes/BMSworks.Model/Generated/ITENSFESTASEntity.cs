using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITENSFESTASEntity
	{
		private int _IDITENSFESTAS;
		private int? _IDFESTA;
		private string _OBSERVACAO;
		private decimal? _TOTALITENS;

		#region Construtores

		//Construtor default
		public ITENSFESTASEntity() {
			this._IDFESTA = null;
			this._TOTALITENS = null;
		}

		public ITENSFESTASEntity(int IDITENSFESTAS, int? IDFESTA, string OBSERVACAO, decimal? TOTALITENS) {

			this._IDITENSFESTAS = IDITENSFESTAS;
			this._IDFESTA = IDFESTA;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALITENS = TOTALITENS;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDITENSFESTAS
		{
			get { return _IDITENSFESTAS; }
			set { _IDITENSFESTAS = value; }
		}

		public int? IDFESTA
		{
			get { return _IDFESTA; }
			set { _IDFESTA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALITENS
		{
			get { return _TOTALITENS; }
			set { _TOTALITENS = value; }
		}

		#endregion
	}
}
