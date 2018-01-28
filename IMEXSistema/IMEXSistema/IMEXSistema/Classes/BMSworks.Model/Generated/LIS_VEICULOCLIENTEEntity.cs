using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_VEICULOCLIENTEEntity
	{
		private int? _IDVEICULOCLIENTE;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _PLACA;
		private int? _ANOFABR;
		private int? _ANOMODELO;
		private string _MARCAMODELO;

		#region Construtores

		//Construtor default
		public LIS_VEICULOCLIENTEEntity() { }

		public LIS_VEICULOCLIENTEEntity(int? IDVEICULOCLIENTE, int? IDCLIENTE, string NOMECLIENTE, string PLACA, int? ANOFABR, int? ANOMODELO, string MARCAMODELO)		{

			this._IDVEICULOCLIENTE = IDVEICULOCLIENTE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._PLACA = PLACA;
			this._ANOFABR = ANOFABR;
			this._ANOMODELO = ANOMODELO;
			this._MARCAMODELO = MARCAMODELO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDVEICULOCLIENTE
		{
			get { return _IDVEICULOCLIENTE; }
			set { _IDVEICULOCLIENTE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public string PLACA
		{
			get { return _PLACA; }
			set { _PLACA = value; }
		}

		public int? ANOFABR
		{
			get { return _ANOFABR; }
			set { _ANOFABR = value; }
		}

		public int? ANOMODELO
		{
			get { return _ANOMODELO; }
			set { _ANOMODELO = value; }
		}

		public string MARCAMODELO
		{
			get { return _MARCAMODELO; }
			set { _MARCAMODELO = value; }
		}

		#endregion
	}
}
