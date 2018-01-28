using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PROCESSO1Entity
	{
		private int? _IDPROCESSO;
		private string _NUMERO;
		private DateTime? _DATAPUBLICACAO;
		private DateTime? _DATAPREVISAO;
		private DateTime? _DATAFINAL;
		private string _PROCESSO;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private int? _IDTRIBUNAL;
		private string _NOMETRIBUNAL;
		private int? _IDJORNAL;
		private string _NOMEJORNAL;
		private int? _IDSECRETARIA;
		private string _NOMESECRETARIA;
		private int? _IDFUNCIONARIO;
		private string _NOMEADVOGADO;
		private int? _IDAUTOR;
		private string _NOMEAUTOR;
		private int? _IDREU;
		private string _NOMEREU;
		private string _OBSERVACAO;
		private string _CODIGO;
		private int? _IDFUNCCAD;
		private string _NOMEFUNCAD;

		#region Construtores

		//Construtor default
		public LIS_PROCESSO1Entity() { }

		public LIS_PROCESSO1Entity(int? IDPROCESSO, string NUMERO, DateTime? DATAPUBLICACAO, DateTime? DATAPREVISAO, DateTime? DATAFINAL, string PROCESSO, int? IDSTATUS, string NOMESTATUS, int? IDTRIBUNAL, string NOMETRIBUNAL, int? IDJORNAL, string NOMEJORNAL, int? IDSECRETARIA, string NOMESECRETARIA, int? IDFUNCIONARIO, string NOMEADVOGADO, int? IDAUTOR, string NOMEAUTOR, int? IDREU, string NOMEREU, string OBSERVACAO, string CODIGO, int? IDFUNCCAD, string NOMEFUNCAD)		{

			this._IDPROCESSO = IDPROCESSO;
			this._NUMERO = NUMERO;
			this._DATAPUBLICACAO = DATAPUBLICACAO;
			this._DATAPREVISAO = DATAPREVISAO;
			this._DATAFINAL = DATAFINAL;
			this._PROCESSO = PROCESSO;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._IDTRIBUNAL = IDTRIBUNAL;
			this._NOMETRIBUNAL = NOMETRIBUNAL;
			this._IDJORNAL = IDJORNAL;
			this._NOMEJORNAL = NOMEJORNAL;
			this._IDSECRETARIA = IDSECRETARIA;
			this._NOMESECRETARIA = NOMESECRETARIA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEADVOGADO = NOMEADVOGADO;
			this._IDAUTOR = IDAUTOR;
			this._NOMEAUTOR = NOMEAUTOR;
			this._IDREU = IDREU;
			this._NOMEREU = NOMEREU;
			this._OBSERVACAO = OBSERVACAO;
			this._CODIGO = CODIGO;
			this._IDFUNCCAD = IDFUNCCAD;
			this._NOMEFUNCAD = NOMEFUNCAD;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPROCESSO
		{
			get { return _IDPROCESSO; }
			set { _IDPROCESSO = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public DateTime? DATAPUBLICACAO
		{
			get { return _DATAPUBLICACAO; }
			set { _DATAPUBLICACAO = value; }
		}

		public DateTime? DATAPREVISAO
		{
			get { return _DATAPREVISAO; }
			set { _DATAPREVISAO = value; }
		}

		public DateTime? DATAFINAL
		{
			get { return _DATAFINAL; }
			set { _DATAFINAL = value; }
		}

		public string PROCESSO
		{
			get { return _PROCESSO; }
			set { _PROCESSO = value; }
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

		public int? IDTRIBUNAL
		{
			get { return _IDTRIBUNAL; }
			set { _IDTRIBUNAL = value; }
		}

		public string NOMETRIBUNAL
		{
			get { return _NOMETRIBUNAL; }
			set { _NOMETRIBUNAL = value; }
		}

		public int? IDJORNAL
		{
			get { return _IDJORNAL; }
			set { _IDJORNAL = value; }
		}

		public string NOMEJORNAL
		{
			get { return _NOMEJORNAL; }
			set { _NOMEJORNAL = value; }
		}

		public int? IDSECRETARIA
		{
			get { return _IDSECRETARIA; }
			set { _IDSECRETARIA = value; }
		}

		public string NOMESECRETARIA
		{
			get { return _NOMESECRETARIA; }
			set { _NOMESECRETARIA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEADVOGADO
		{
			get { return _NOMEADVOGADO; }
			set { _NOMEADVOGADO = value; }
		}

		public int? IDAUTOR
		{
			get { return _IDAUTOR; }
			set { _IDAUTOR = value; }
		}

		public string NOMEAUTOR
		{
			get { return _NOMEAUTOR; }
			set { _NOMEAUTOR = value; }
		}

		public int? IDREU
		{
			get { return _IDREU; }
			set { _IDREU = value; }
		}

		public string NOMEREU
		{
			get { return _NOMEREU; }
			set { _NOMEREU = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public int? IDFUNCCAD
		{
			get { return _IDFUNCCAD; }
			set { _IDFUNCCAD = value; }
		}

		public string NOMEFUNCAD
		{
			get { return _NOMEFUNCAD; }
			set { _NOMEFUNCAD = value; }
		}

		#endregion
	}
}
