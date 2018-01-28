using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class AGENDAEntity
	{
		private int _IDAGENDA;
		private int? _IDSTATUS;
		private DateTime? _DATA;
		private string _HORA;
		private int? _IDGRUPOAGENDA;
		private int? _IDEVENTO;
		private string _COMENTARIO;
		private int? _IDFUNCIONARIO;
		private int? _IDCLIENTE;

		#region Construtores

		//Construtor default
		public AGENDAEntity() {
			this._IDSTATUS = null;
			this._DATA = null;
			this._IDGRUPOAGENDA = null;
			this._IDEVENTO = null;
			this._IDFUNCIONARIO = null;
			this._IDCLIENTE = null;
		}

		public AGENDAEntity(int IDAGENDA, int? IDSTATUS, DateTime? DATA, string HORA, int? IDGRUPOAGENDA, int? IDEVENTO, string COMENTARIO, int? IDFUNCIONARIO, int? IDCLIENTE) {

			this._IDAGENDA = IDAGENDA;
			this._IDSTATUS = IDSTATUS;
			this._DATA = DATA;
			this._HORA = HORA;
			this._IDGRUPOAGENDA = IDGRUPOAGENDA;
			this._IDEVENTO = IDEVENTO;
			this._COMENTARIO = COMENTARIO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDCLIENTE = IDCLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDAGENDA
		{
			get { return _IDAGENDA; }
			set { _IDAGENDA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string HORA
		{
			get { return _HORA; }
			set { _HORA = value; }
		}

		public int? IDGRUPOAGENDA
		{
			get { return _IDGRUPOAGENDA; }
			set { _IDGRUPOAGENDA = value; }
		}

		public int? IDEVENTO
		{
			get { return _IDEVENTO; }
			set { _IDEVENTO = value; }
		}

		public string COMENTARIO
		{
			get { return _COMENTARIO; }
			set { _COMENTARIO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		#endregion
	}
}
