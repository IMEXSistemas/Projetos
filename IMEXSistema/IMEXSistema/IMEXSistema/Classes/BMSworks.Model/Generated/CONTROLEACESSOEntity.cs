using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONTROLEACESSOEntity
	{
		private int _IDCONTROLEACESSO;
		private string _FLAGALTERA;
		private string _FLAGAPAGA;
		private string _FLAGACESSA;
		private int? _IDNIVEL;
		private int? _IDFORMULARIO;

		#region Construtores

		//Construtor default
		public CONTROLEACESSOEntity() {
			this._IDNIVEL = null;
			this._IDFORMULARIO = null;
		}

		public CONTROLEACESSOEntity(int IDCONTROLEACESSO, string FLAGALTERA, string FLAGAPAGA, string FLAGACESSA, int? IDNIVEL, int? IDFORMULARIO) {

			this._IDCONTROLEACESSO = IDCONTROLEACESSO;
			this._FLAGALTERA = FLAGALTERA;
			this._FLAGAPAGA = FLAGAPAGA;
			this._FLAGACESSA = FLAGACESSA;
			this._IDNIVEL = IDNIVEL;
			this._IDFORMULARIO = IDFORMULARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONTROLEACESSO
		{
			get { return _IDCONTROLEACESSO; }
			set { _IDCONTROLEACESSO = value; }
		}

		public string FLAGALTERA
		{
			get { return _FLAGALTERA; }
			set { _FLAGALTERA = value; }
		}

		public string FLAGAPAGA
		{
			get { return _FLAGAPAGA; }
			set { _FLAGAPAGA = value; }
		}

		public string FLAGACESSA
		{
			get { return _FLAGACESSA; }
			set { _FLAGACESSA = value; }
		}

		public int? IDNIVEL
		{
			get { return _IDNIVEL; }
			set { _IDNIVEL = value; }
		}

		public int? IDFORMULARIO
		{
			get { return _IDFORMULARIO; }
			set { _IDFORMULARIO = value; }
		}

		#endregion
	}
}
