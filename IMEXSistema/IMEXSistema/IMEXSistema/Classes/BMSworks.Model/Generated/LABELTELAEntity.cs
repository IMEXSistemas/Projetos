using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LABELTELAEntity
	{
		private int _IDLABELTELA;
		private string _NOMELABEL;
		private string _TEXTOLABEL;
		private int? _IDFORMULARIO;

		#region Construtores

		//Construtor default
		public LABELTELAEntity() {
			this._IDFORMULARIO = null;
		}

		public LABELTELAEntity(int IDLABELTELA, string NOMELABEL, string TEXTOLABEL, int? IDFORMULARIO) {

			this._IDLABELTELA = IDLABELTELA;
			this._NOMELABEL = NOMELABEL;
			this._TEXTOLABEL = TEXTOLABEL;
			this._IDFORMULARIO = IDFORMULARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDLABELTELA
		{
			get { return _IDLABELTELA; }
			set { _IDLABELTELA = value; }
		}

		public string NOMELABEL
		{
			get { return _NOMELABEL; }
			set { _NOMELABEL = value; }
		}

		public string TEXTOLABEL
		{
			get { return _TEXTOLABEL; }
			set { _TEXTOLABEL = value; }
		}

		public int? IDFORMULARIO
		{
			get { return _IDFORMULARIO; }
			set { _IDFORMULARIO = value; }
		}

		#endregion
	}
}
