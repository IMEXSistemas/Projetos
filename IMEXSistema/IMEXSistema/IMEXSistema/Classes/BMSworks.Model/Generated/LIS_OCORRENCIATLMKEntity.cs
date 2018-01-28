using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_OCORRENCIATLMKEntity
	{
		private int? _IDOCORRENCIATLMK;
		private int? _IDFUNCIONARIO;
		private int? _IDSTATUSTLMK;
		private DateTime? _DATACONTATO;
		private string _HORACONTATO;
		private DateTime? _DATAPROXCONTATO;
		private string _HORAPROXCONTATO;
		private string _NOMECONTATO;
		private string _CONVERSA;
		private string _NOMEFUNCIONARIO;
		private string _NOMESTATUSTLMK;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;

		#region Construtores

		//Construtor default
		public LIS_OCORRENCIATLMKEntity() { }

		public LIS_OCORRENCIATLMKEntity(int? IDOCORRENCIATLMK, int? IDFUNCIONARIO, int? IDSTATUSTLMK, DateTime? DATACONTATO, string HORACONTATO, DateTime? DATAPROXCONTATO, string HORAPROXCONTATO, string NOMECONTATO, string CONVERSA, string NOMEFUNCIONARIO, string NOMESTATUSTLMK, int? IDCLIENTE, string NOMECLIENTE)		{

			this._IDOCORRENCIATLMK = IDOCORRENCIATLMK;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDSTATUSTLMK = IDSTATUSTLMK;
			this._DATACONTATO = DATACONTATO;
			this._HORACONTATO = HORACONTATO;
			this._DATAPROXCONTATO = DATAPROXCONTATO;
			this._HORAPROXCONTATO = HORAPROXCONTATO;
			this._NOMECONTATO = NOMECONTATO;
			this._CONVERSA = CONVERSA;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._NOMESTATUSTLMK = NOMESTATUSTLMK;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDOCORRENCIATLMK
		{
			get { return _IDOCORRENCIATLMK; }
			set { _IDOCORRENCIATLMK = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDSTATUSTLMK
		{
			get { return _IDSTATUSTLMK; }
			set { _IDSTATUSTLMK = value; }
		}

		public DateTime? DATACONTATO
		{
			get { return _DATACONTATO; }
			set { _DATACONTATO = value; }
		}

		public string HORACONTATO
		{
			get { return _HORACONTATO; }
			set { _HORACONTATO = value; }
		}

		public DateTime? DATAPROXCONTATO
		{
			get { return _DATAPROXCONTATO; }
			set { _DATAPROXCONTATO = value; }
		}

		public string HORAPROXCONTATO
		{
			get { return _HORAPROXCONTATO; }
			set { _HORAPROXCONTATO = value; }
		}

		public string NOMECONTATO
		{
			get { return _NOMECONTATO; }
			set { _NOMECONTATO = value; }
		}

		public string CONVERSA
		{
			get { return _CONVERSA; }
			set { _CONVERSA = value; }
		}

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public string NOMESTATUSTLMK
		{
			get { return _NOMESTATUSTLMK; }
			set { _NOMESTATUSTLMK = value; }
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

		#endregion
	}
}
