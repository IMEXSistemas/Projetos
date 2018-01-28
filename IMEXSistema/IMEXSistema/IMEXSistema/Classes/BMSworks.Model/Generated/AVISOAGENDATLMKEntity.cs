using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class AVISOAGENDATLMKEntity
	{
		private int _IDAVISOAGENDATLMK;
		private int? _IDCLIENTE;
		private string _FLAGVISUALIZADO;
		private DateTime? _DATAPROXCONTATO;
		private string _HORAPROXCONTATO;
		private int? _IDFUNCIONARIO;
		private string _FLAGADICIONADO;

		#region Construtores

		//Construtor default
		public AVISOAGENDATLMKEntity() {
			this._IDCLIENTE = null;
			this._DATAPROXCONTATO = null;
			this._IDFUNCIONARIO = null;
		}

		public AVISOAGENDATLMKEntity(int IDAVISOAGENDATLMK, int? IDCLIENTE, string FLAGVISUALIZADO, DateTime? DATAPROXCONTATO, string HORAPROXCONTATO, int? IDFUNCIONARIO, string FLAGADICIONADO) {

			this._IDAVISOAGENDATLMK = IDAVISOAGENDATLMK;
			this._IDCLIENTE = IDCLIENTE;
			this._FLAGVISUALIZADO = FLAGVISUALIZADO;
			this._DATAPROXCONTATO = DATAPROXCONTATO;
			this._HORAPROXCONTATO = HORAPROXCONTATO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._FLAGADICIONADO = FLAGADICIONADO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDAVISOAGENDATLMK
		{
			get { return _IDAVISOAGENDATLMK; }
			set { _IDAVISOAGENDATLMK = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string FLAGVISUALIZADO
		{
			get { return _FLAGVISUALIZADO; }
			set { _FLAGVISUALIZADO = value; }
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

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string FLAGADICIONADO
		{
			get { return _FLAGADICIONADO; }
			set { _FLAGADICIONADO = value; }
		}

		#endregion
	}
}
