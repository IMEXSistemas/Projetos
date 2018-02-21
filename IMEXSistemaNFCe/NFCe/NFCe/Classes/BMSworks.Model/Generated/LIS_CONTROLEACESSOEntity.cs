using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CONTROLEACESSOEntity
	{
		private int? _IDCONTROLEACESSO;
		private string _FLAGALTERA;
		private string _FLAGAPAGA;
		private string _FLAGACESSA;
		private int? _IDNIVEL;
		private string _NOMENIVEL;
		private int? _IDFORMULARIO;
		private string _NOMEFORMULARIO;
		private string _NOMETELA;

		#region Construtores

		//Construtor default
		public LIS_CONTROLEACESSOEntity() { }

		public LIS_CONTROLEACESSOEntity(int? IDCONTROLEACESSO, string FLAGALTERA, string FLAGAPAGA, string FLAGACESSA, int? IDNIVEL, string NOMENIVEL, int? IDFORMULARIO, string NOMEFORMULARIO, string NOMETELA)		{

			this._IDCONTROLEACESSO = IDCONTROLEACESSO;
			this._FLAGALTERA = FLAGALTERA;
			this._FLAGAPAGA = FLAGAPAGA;
			this._FLAGACESSA = FLAGACESSA;
			this._IDNIVEL = IDNIVEL;
			this._NOMENIVEL = NOMENIVEL;
			this._IDFORMULARIO = IDFORMULARIO;
			this._NOMEFORMULARIO = NOMEFORMULARIO;
			this._NOMETELA = NOMETELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCONTROLEACESSO
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

		public string NOMENIVEL
		{
			get { return _NOMENIVEL; }
			set { _NOMENIVEL = value; }
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
