using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class STATUSDUPLICATAEntity
	{
		private int _IDSTATUSDUPLICATA;
		private string _NOMESTATUS;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public STATUSDUPLICATAEntity() {
		}

		public STATUSDUPLICATAEntity(int IDSTATUSDUPLICATA, string NOMESTATUS, string OBSERVACAO) {

			this._IDSTATUSDUPLICATA = IDSTATUSDUPLICATA;
			this._NOMESTATUS = NOMESTATUS;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSTATUSDUPLICATA
		{
			get { return _IDSTATUSDUPLICATA; }
			set { _IDSTATUSDUPLICATA = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
