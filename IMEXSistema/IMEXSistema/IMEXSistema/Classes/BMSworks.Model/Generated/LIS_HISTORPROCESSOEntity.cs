using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_HISTORPROCESSOEntity
	{
		private int? _IDHISTORPROCESSO;
		private int? _IDPROCESSO;
		private string _NUMEROPROCESO;
		private DateTime? _DATA;
		private string _OBSERVACAO;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNC;

		#region Construtores

		//Construtor default
		public LIS_HISTORPROCESSOEntity() { }

		public LIS_HISTORPROCESSOEntity(int? IDHISTORPROCESSO, int? IDPROCESSO, string NUMEROPROCESO, DateTime? DATA, string OBSERVACAO, int? IDFUNCIONARIO, string NOMEFUNC)		{

			this._IDHISTORPROCESSO = IDHISTORPROCESSO;
			this._IDPROCESSO = IDPROCESSO;
			this._NUMEROPROCESO = NUMEROPROCESO;
			this._DATA = DATA;
			this._OBSERVACAO = OBSERVACAO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNC = NOMEFUNC;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDHISTORPROCESSO
		{
			get { return _IDHISTORPROCESSO; }
			set { _IDHISTORPROCESSO = value; }
		}

		public int? IDPROCESSO
		{
			get { return _IDPROCESSO; }
			set { _IDPROCESSO = value; }
		}

		public string NUMEROPROCESO
		{
			get { return _NUMEROPROCESO; }
			set { _NUMEROPROCESO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEFUNC
		{
			get { return _NOMEFUNC; }
			set { _NOMEFUNC = value; }
		}

		#endregion
	}
}
