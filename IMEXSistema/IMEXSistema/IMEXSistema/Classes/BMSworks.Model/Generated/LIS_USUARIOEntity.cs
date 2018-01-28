using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_USUARIOEntity
	{
		private int? _IDUSUARIO;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private int? _IDNIVELUSUARIO;
		private string _FLAGATIVO;
		private string _NOMENIVELUSUARIO;
		private string _NOMESTATUS;
		private string _NOMEUSUARIO;
		private string _SENHAUSUARIO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public LIS_USUARIOEntity() { }

		public LIS_USUARIOEntity(int? IDUSUARIO, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, int? IDNIVELUSUARIO, string FLAGATIVO, string NOMENIVELUSUARIO, string NOMESTATUS, string NOMEUSUARIO, string SENHAUSUARIO, string OBSERVACAO)		{

			this._IDUSUARIO = IDUSUARIO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._IDNIVELUSUARIO = IDNIVELUSUARIO;
			this._FLAGATIVO = FLAGATIVO;
			this._NOMENIVELUSUARIO = NOMENIVELUSUARIO;
			this._NOMESTATUS = NOMESTATUS;
			this._NOMEUSUARIO = NOMEUSUARIO;
			this._SENHAUSUARIO = SENHAUSUARIO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDUSUARIO
		{
			get { return _IDUSUARIO; }
			set { _IDUSUARIO = value; }
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

		public int? IDNIVELUSUARIO
		{
			get { return _IDNIVELUSUARIO; }
			set { _IDNIVELUSUARIO = value; }
		}

		public string FLAGATIVO
		{
			get { return _FLAGATIVO; }
			set { _FLAGATIVO = value; }
		}

		public string NOMENIVELUSUARIO
		{
			get { return _NOMENIVELUSUARIO; }
			set { _NOMENIVELUSUARIO = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public string NOMEUSUARIO
		{
			get { return _NOMEUSUARIO; }
			set { _NOMEUSUARIO = value; }
		}

		public string SENHAUSUARIO
		{
			get { return _SENHAUSUARIO; }
			set { _SENHAUSUARIO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}
