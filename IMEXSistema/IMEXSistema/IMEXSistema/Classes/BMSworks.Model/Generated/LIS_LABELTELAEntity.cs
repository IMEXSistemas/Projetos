using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_LABELTELAEntity
	{
		private int? _IDLABELTELA;
		private string _NOMELABEL;
		private string _TEXTOLABEL;
		private int? _IDFORMULARIO;
		private string _NOMEFORMULARIO;
		private string _NOMETELA;

		#region Construtores

		//Construtor default
		public LIS_LABELTELAEntity() { }

		public LIS_LABELTELAEntity(int? IDLABELTELA, string NOMELABEL, string TEXTOLABEL, int? IDFORMULARIO, string NOMEFORMULARIO, string NOMETELA)		{

			this._IDLABELTELA = IDLABELTELA;
			this._NOMELABEL = NOMELABEL;
			this._TEXTOLABEL = TEXTOLABEL;
			this._IDFORMULARIO = IDFORMULARIO;
			this._NOMEFORMULARIO = NOMEFORMULARIO;
			this._NOMETELA = NOMETELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDLABELTELA
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

		public string NOMEFORMULARIO
		{
			get { return _NOMEFORMULARIO; }
			set { _NOMEFORMULARIO = value; }
		}

		public string NOMETELA
		{
			get { return _NOMETELA; }
			set { _NOMETELA = value; }
		}

		#endregion
	}
}
