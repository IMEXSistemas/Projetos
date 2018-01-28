using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_AGENDAEntity
	{
		private int? _IDAGENDA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private DateTime? _DATA;
		private string _HORA;
		private int? _IDGRUPOAGENDA;
		private string _NOMEGRUPOAGENDA;
		private int? _IDEVENTO;
		private string _NOMEEVENTO;
		private string _COMENTARIO;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private int? _IDCLIENTE;
		private string _NOME;

		#region Construtores

		//Construtor default
		public LIS_AGENDAEntity() { }

		public LIS_AGENDAEntity(int? IDAGENDA, int? IDSTATUS, string NOMESTATUS, DateTime? DATA, string HORA, int? IDGRUPOAGENDA, string NOMEGRUPOAGENDA, int? IDEVENTO, string NOMEEVENTO, string COMENTARIO, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, int? IDCLIENTE, string NOME)		{

			this._IDAGENDA = IDAGENDA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._DATA = DATA;
			this._HORA = HORA;
			this._IDGRUPOAGENDA = IDGRUPOAGENDA;
			this._NOMEGRUPOAGENDA = NOMEGRUPOAGENDA;
			this._IDEVENTO = IDEVENTO;
			this._NOMEEVENTO = NOMEEVENTO;
			this._COMENTARIO = COMENTARIO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDAGENDA
		{
			get { return _IDAGENDA; }
			set { _IDAGENDA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
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

		public string NOMEGRUPOAGENDA
		{
			get { return _NOMEGRUPOAGENDA; }
			set { _NOMEGRUPOAGENDA = value; }
		}

		public int? IDEVENTO
		{
			get { return _IDEVENTO; }
			set { _IDEVENTO = value; }
		}

		public string NOMEEVENTO
		{
			get { return _NOMEEVENTO; }
			set { _NOMEEVENTO = value; }
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

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}
