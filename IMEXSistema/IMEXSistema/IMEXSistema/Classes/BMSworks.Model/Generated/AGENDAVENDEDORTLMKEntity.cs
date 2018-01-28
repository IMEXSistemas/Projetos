using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class AGENDAVENDEDORTLMKEntity
	{
		private int _IDAGENDAVENDEDORTLMK;
		private int? _IDSTATUSTLMK;
		private int? _IDCLIENTE;
		private DateTime? _DATACONTATO;
		private string _HORA;
		private int? _IDFUNCIONARIO;
		private int? _IDGRUPOAGENDATLMK;

		#region Construtores

		//Construtor default
		public AGENDAVENDEDORTLMKEntity() {
			this._IDSTATUSTLMK = null;
			this._IDCLIENTE = null;
			this._DATACONTATO = null;
			this._IDFUNCIONARIO = null;
			this._IDGRUPOAGENDATLMK = null;
		}

		public AGENDAVENDEDORTLMKEntity(int IDAGENDAVENDEDORTLMK, int? IDSTATUSTLMK, int? IDCLIENTE, DateTime? DATACONTATO, string HORA, int? IDFUNCIONARIO, int? IDGRUPOAGENDATLMK) {

			this._IDAGENDAVENDEDORTLMK = IDAGENDAVENDEDORTLMK;
			this._IDSTATUSTLMK = IDSTATUSTLMK;
			this._IDCLIENTE = IDCLIENTE;
			this._DATACONTATO = DATACONTATO;
			this._HORA = HORA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDGRUPOAGENDATLMK = IDGRUPOAGENDATLMK;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDAGENDAVENDEDORTLMK
		{
			get { return _IDAGENDAVENDEDORTLMK; }
			set { _IDAGENDAVENDEDORTLMK = value; }
		}

		public int? IDSTATUSTLMK
		{
			get { return _IDSTATUSTLMK; }
			set { _IDSTATUSTLMK = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public DateTime? DATACONTATO
		{
			get { return _DATACONTATO; }
			set { _DATACONTATO = value; }
		}

		public string HORA
		{
			get { return _HORA; }
			set { _HORA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDGRUPOAGENDATLMK
		{
			get { return _IDGRUPOAGENDATLMK; }
			set { _IDGRUPOAGENDATLMK = value; }
		}

		#endregion
	}
}
