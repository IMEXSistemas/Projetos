using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MEDPLANOCORTEEntity
	{
		private int _IDMEDPLANOCORTE;
		private int? _IDPLANOCORTE;
		private int? _QUANTPECA;
		private string _NOMEPECA;
		private int? _COMPPECA;
		private int? _LARGPECA;
		private int? _CODIGOMEDIDA;
		private string _LEGENDA;
		private int? _FOLHA;

		#region Construtores

		//Construtor default
		public MEDPLANOCORTEEntity() {
			this._IDPLANOCORTE = null;
			this._QUANTPECA = null;
			this._COMPPECA = null;
			this._LARGPECA = null;
			this._CODIGOMEDIDA = null;
			this._FOLHA = null;
		}

		public MEDPLANOCORTEEntity(int IDMEDPLANOCORTE, int? IDPLANOCORTE, int? QUANTPECA, string NOMEPECA, int? COMPPECA, int? LARGPECA, int? CODIGOMEDIDA, string LEGENDA, int? FOLHA) {

			this._IDMEDPLANOCORTE = IDMEDPLANOCORTE;
			this._IDPLANOCORTE = IDPLANOCORTE;
			this._QUANTPECA = QUANTPECA;
			this._NOMEPECA = NOMEPECA;
			this._COMPPECA = COMPPECA;
			this._LARGPECA = LARGPECA;
			this._CODIGOMEDIDA = CODIGOMEDIDA;
			this._LEGENDA = LEGENDA;
			this._FOLHA = FOLHA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMEDPLANOCORTE
		{
			get { return _IDMEDPLANOCORTE; }
			set { _IDMEDPLANOCORTE = value; }
		}

		public int? IDPLANOCORTE
		{
			get { return _IDPLANOCORTE; }
			set { _IDPLANOCORTE = value; }
		}

		public int? QUANTPECA
		{
			get { return _QUANTPECA; }
			set { _QUANTPECA = value; }
		}

		public string NOMEPECA
		{
			get { return _NOMEPECA; }
			set { _NOMEPECA = value; }
		}

		public int? COMPPECA
		{
			get { return _COMPPECA; }
			set { _COMPPECA = value; }
		}

		public int? LARGPECA
		{
			get { return _LARGPECA; }
			set { _LARGPECA = value; }
		}

		public int? CODIGOMEDIDA
		{
			get { return _CODIGOMEDIDA; }
			set { _CODIGOMEDIDA = value; }
		}

		public string LEGENDA
		{
			get { return _LEGENDA; }
			set { _LEGENDA = value; }
		}

		public int? FOLHA
		{
			get { return _FOLHA; }
			set { _FOLHA = value; }
		}

		#endregion
	}
}
