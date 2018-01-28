using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CLASSFISCALEntity
	{
		private int _IDCLASSFISCAL;
		private string _CODIGO;
		private decimal? _ALIQICMS;
		private decimal? _BASEREDUZIDA;
		private int? _IDMENSAGEMNFE;

		#region Construtores

		//Construtor default
		public CLASSFISCALEntity() {
			this._ALIQICMS = null;
			this._BASEREDUZIDA = null;
			this._IDMENSAGEMNFE = null;
		}

		public CLASSFISCALEntity(int IDCLASSFISCAL, string CODIGO, decimal? ALIQICMS, decimal? BASEREDUZIDA, int? IDMENSAGEMNFE) {

			this._IDCLASSFISCAL = IDCLASSFISCAL;
			this._CODIGO = CODIGO;
			this._ALIQICMS = ALIQICMS;
			this._BASEREDUZIDA = BASEREDUZIDA;
			this._IDMENSAGEMNFE = IDMENSAGEMNFE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCLASSFISCAL
		{
			get { return _IDCLASSFISCAL; }
			set { _IDCLASSFISCAL = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public decimal? ALIQICMS
		{
			get { return _ALIQICMS; }
			set { _ALIQICMS = value; }
		}

		public decimal? BASEREDUZIDA
		{
			get { return _BASEREDUZIDA; }
			set { _BASEREDUZIDA = value; }
		}

		public int? IDMENSAGEMNFE
		{
			get { return _IDMENSAGEMNFE; }
			set { _IDMENSAGEMNFE = value; }
		}

		#endregion
	}
}
