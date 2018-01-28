using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class OCORRENCIATLMKEntity
	{
		private int _IDOCORRENCIATLMK;
		private int? _IDFUNCIONARIO;
		private int? _IDSTATUSTLMK;
		private DateTime? _DATACONTATO;
		private string _HORACONTATO;
		private DateTime? _DATAPROXCONTATO;
		private string _HORAPROXCONTATO;
		private string _NOMECONTATO;
		private string _CONVERSA;
		private int? _IDCLIENTE;

		#region Construtores

		//Construtor default
		public OCORRENCIATLMKEntity() {
			this._IDFUNCIONARIO = null;
			this._IDSTATUSTLMK = null;
			this._DATACONTATO = null;
			this._DATAPROXCONTATO = null;
			this._IDCLIENTE = null;
		}

		public OCORRENCIATLMKEntity(int IDOCORRENCIATLMK, int? IDFUNCIONARIO, int? IDSTATUSTLMK, DateTime? DATACONTATO, string HORACONTATO, DateTime? DATAPROXCONTATO, string HORAPROXCONTATO, string NOMECONTATO, string CONVERSA, int? IDCLIENTE) {

			this._IDOCORRENCIATLMK = IDOCORRENCIATLMK;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDSTATUSTLMK = IDSTATUSTLMK;
			this._DATACONTATO = DATACONTATO;
			this._HORACONTATO = HORACONTATO;
			this._DATAPROXCONTATO = DATAPROXCONTATO;
			this._HORAPROXCONTATO = HORAPROXCONTATO;
			this._NOMECONTATO = NOMECONTATO;
			this._CONVERSA = CONVERSA;
			this._IDCLIENTE = IDCLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDOCORRENCIATLMK
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

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		#endregion
	}
}
