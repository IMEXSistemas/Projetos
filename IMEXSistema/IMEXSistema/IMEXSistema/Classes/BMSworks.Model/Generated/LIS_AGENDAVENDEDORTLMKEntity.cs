using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_AGENDAVENDEDORTLMKEntity
	{
		private int? _IDAGENDAVENDEDORTLMK;
		private int? _IDSTATUSTLMK;
		private int? _IDCLIENTE;
		private DateTime? _DATACONTATO;
		private string _HORA;
		private int? _IDFUNCIONARIO;
		private string _NOMECLIENTE;
		private string _NOMESTATUSTLMK;
		private string _NOMEFUNCIONARIO;
		private string _FLAGEXIBIR;
		private int? _IDGRUPOAGENDATLMK;
		private string _NOMEGRUPO;
        private int? _COD_MUN_IBGE;

		#region Construtores

		//Construtor default
		public LIS_AGENDAVENDEDORTLMKEntity() { }

        public LIS_AGENDAVENDEDORTLMKEntity(int? IDAGENDAVENDEDORTLMK, int? IDSTATUSTLMK, int? IDCLIENTE, DateTime? DATACONTATO, string HORA, int? IDFUNCIONARIO, string NOMECLIENTE, string NOMESTATUSTLMK, string NOMEFUNCIONARIO, string FLAGEXIBIR, int? IDGRUPOAGENDATLMK, string NOMEGRUPO, int? COD_MUN_IBGE)
        {

			this._IDAGENDAVENDEDORTLMK = IDAGENDAVENDEDORTLMK;
			this._IDSTATUSTLMK = IDSTATUSTLMK;
			this._IDCLIENTE = IDCLIENTE;
			this._DATACONTATO = DATACONTATO;
			this._HORA = HORA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMECLIENTE = NOMECLIENTE;
			this._NOMESTATUSTLMK = NOMESTATUSTLMK;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._IDGRUPOAGENDATLMK = IDGRUPOAGENDATLMK;
			this._NOMEGRUPO = NOMEGRUPO;
            this._COD_MUN_IBGE = COD_MUN_IBGE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDAGENDAVENDEDORTLMK
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

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public string NOMESTATUSTLMK
		{
			get { return _NOMESTATUSTLMK; }
			set { _NOMESTATUSTLMK = value; }
		}

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public int? IDGRUPOAGENDATLMK
		{
			get { return _IDGRUPOAGENDATLMK; }
			set { _IDGRUPOAGENDATLMK = value; }
		}

		public string NOMEGRUPO
		{
			get { return _NOMEGRUPO; }
			set { _NOMEGRUPO = value; }
		}

        public int? COD_MUN_IBGE
		{
            get { return _COD_MUN_IBGE; }
            set { _COD_MUN_IBGE = value; }
		}

        

		#endregion
	}
}
