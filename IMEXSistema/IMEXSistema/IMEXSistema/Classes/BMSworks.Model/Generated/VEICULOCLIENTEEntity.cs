using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VEICULOCLIENTEEntity
	{
		private int _IDVEICULOCLIENTE;
		private int? _IDCLIENTE;
		private string _PLACA;
		private int? _ANOFABR;
		private int? _ANOMODELO;
		private int? _IDCOR;
		private string _CHASSIS;
		private string _MARCAMODELO;

		#region Construtores

		//Construtor default
		public VEICULOCLIENTEEntity() {
			this._IDCLIENTE = null;
			this._ANOFABR = null;
			this._ANOMODELO = null;
			this._IDCOR = null;
		}

		public VEICULOCLIENTEEntity(int IDVEICULOCLIENTE, int? IDCLIENTE, string PLACA, int? ANOFABR, int? ANOMODELO, int? IDCOR, string CHASSIS, string MARCAMODELO) {

			this._IDVEICULOCLIENTE = IDVEICULOCLIENTE;
			this._IDCLIENTE = IDCLIENTE;
			this._PLACA = PLACA;
			this._ANOFABR = ANOFABR;
			this._ANOMODELO = ANOMODELO;
			this._IDCOR = IDCOR;
			this._CHASSIS = CHASSIS;
			this._MARCAMODELO = MARCAMODELO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDVEICULOCLIENTE
		{
			get { return _IDVEICULOCLIENTE; }
			set { _IDVEICULOCLIENTE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
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

		public int? IDCOR
		{
			get { return _IDCOR; }
			set { _IDCOR = value; }
		}

		public string CHASSIS
		{
			get { return _CHASSIS; }
			set { _CHASSIS = value; }
		}

		public string MARCAMODELO
		{
			get { return _MARCAMODELO; }
			set { _MARCAMODELO = value; }
		}

		#endregion
	}
}
