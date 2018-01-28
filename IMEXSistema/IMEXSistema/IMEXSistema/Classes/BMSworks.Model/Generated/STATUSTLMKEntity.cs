using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class STATUSTLMKEntity
	{
		private int _IDSTATUSTLMK;
		private string _NOME;
		private string _OBSERVACAO;
		private string _FLAGEXIBIR;
		private int? _COLORA;
		private int? _COLOR;
		private int? _COLORG;
		private int? _COLORB;
		private string _FLAGDATACONTATO;
		private string _FLAGHORACONTATO;

		#region Construtores

		//Construtor default
		public STATUSTLMKEntity() {
			this._COLORA = null;
			this._COLOR = null;
			this._COLORG = null;
			this._COLORB = null;
		}

		public STATUSTLMKEntity(int IDSTATUSTLMK, string NOME, string OBSERVACAO, string FLAGEXIBIR, int? COLORA, int? COLOR, int? COLORG, int? COLORB, string FLAGDATACONTATO, string FLAGHORACONTATO) {

			this._IDSTATUSTLMK = IDSTATUSTLMK;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._COLORA = COLORA;
			this._COLOR = COLOR;
			this._COLORG = COLORG;
			this._COLORB = COLORB;
			this._FLAGDATACONTATO = FLAGDATACONTATO;
			this._FLAGHORACONTATO = FLAGHORACONTATO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSTATUSTLMK
		{
			get { return _IDSTATUSTLMK; }
			set { _IDSTATUSTLMK = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public int? COLORA
		{
			get { return _COLORA; }
			set { _COLORA = value; }
		}

		public int? COLOR
		{
			get { return _COLOR; }
			set { _COLOR = value; }
		}

		public int? COLORG
		{
			get { return _COLORG; }
			set { _COLORG = value; }
		}

		public int? COLORB
		{
			get { return _COLORB; }
			set { _COLORB = value; }
		}

		public string FLAGDATACONTATO
		{
			get { return _FLAGDATACONTATO; }
			set { _FLAGDATACONTATO = value; }
		}

		public string FLAGHORACONTATO
		{
			get { return _FLAGHORACONTATO; }
			set { _FLAGHORACONTATO = value; }
		}

		#endregion
	}
}
