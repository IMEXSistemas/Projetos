using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PROCESSO1Entity
	{
		private int _IDPROCESSO;
		private string _NUMERO;
		private DateTime? _DATAPUBLICACAO;
		private DateTime? _DATAPREVISAO;
		private DateTime? _DATAFINAL;
		private string _PROCESSO;
		private int? _IDSTATUS;
		private int? _IDTRIBUNAL;
		private int? _IDJORNAL;
		private int? _IDSECRETARIA;
		private int? _IDFUNCIONARIO;
		private int? _IDAUTOR;
		private int? _IDREU;
		private string _OBSERVACAO;
		private string _CODIGO;
		private int? _IDFUNCCAD;

		#region Construtores

		//Construtor default
		public PROCESSO1Entity() {
			this._DATAPUBLICACAO = null;
			this._DATAPREVISAO = null;
			this._DATAFINAL = null;
			this._IDSTATUS = null;
			this._IDTRIBUNAL = null;
			this._IDJORNAL = null;
			this._IDSECRETARIA = null;
			this._IDFUNCIONARIO = null;
			this._IDAUTOR = null;
			this._IDREU = null;
			this._IDFUNCCAD = null;
		}

		public PROCESSO1Entity(int IDPROCESSO, string NUMERO, DateTime? DATAPUBLICACAO, DateTime? DATAPREVISAO, DateTime? DATAFINAL, string PROCESSO, int? IDSTATUS, int? IDTRIBUNAL, int? IDJORNAL, int? IDSECRETARIA, int? IDFUNCIONARIO, int? IDAUTOR, int? IDREU, string OBSERVACAO, string CODIGO, int? IDFUNCCAD) {

			this._IDPROCESSO = IDPROCESSO;
			this._NUMERO = NUMERO;
			this._DATAPUBLICACAO = DATAPUBLICACAO;
			this._DATAPREVISAO = DATAPREVISAO;
			this._DATAFINAL = DATAFINAL;
			this._PROCESSO = PROCESSO;
			this._IDSTATUS = IDSTATUS;
			this._IDTRIBUNAL = IDTRIBUNAL;
			this._IDJORNAL = IDJORNAL;
			this._IDSECRETARIA = IDSECRETARIA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDAUTOR = IDAUTOR;
			this._IDREU = IDREU;
			this._OBSERVACAO = OBSERVACAO;
			this._CODIGO = CODIGO;
			this._IDFUNCCAD = IDFUNCCAD;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPROCESSO
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

		public int? IDTRIBUNAL
		{
			get { return _IDTRIBUNAL; }
			set { _IDTRIBUNAL = value; }
		}

		public int? IDJORNAL
		{
			get { return _IDJORNAL; }
			set { _IDJORNAL = value; }
		}

		public int? IDSECRETARIA
		{
			get { return _IDSECRETARIA; }
			set { _IDSECRETARIA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDAUTOR
		{
			get { return _IDAUTOR; }
			set { _IDAUTOR = value; }
		}

		public int? IDREU
		{
			get { return _IDREU; }
			set { _IDREU = value; }
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

		#endregion
	}
}
