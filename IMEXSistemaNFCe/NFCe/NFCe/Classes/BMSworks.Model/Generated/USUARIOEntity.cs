using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class USUARIOEntity
	{
		private int _IDUSUARIO;
		private int? _IDFUNCIONARIO;
		private int? _IDNIVELUSUARIO;
		private string _FLAGATIVO;
		private string _NOMEUSUARIO;
		private string _SENHAUSUARIO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
        public USUARIOEntity()
        {
			this._IDFUNCIONARIO = null;
			this._IDNIVELUSUARIO = null;
		}

        public USUARIOEntity(int IDUSUARIO, int? IDFUNCIONARIO, int? IDNIVELUSUARIO, string FLAGATIVO, string NOMEUSUARIO, string SENHAUSUARIO, string OBSERVACAO)
        {

			this._IDUSUARIO = IDUSUARIO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDNIVELUSUARIO = IDNIVELUSUARIO;
			this._FLAGATIVO = FLAGATIVO;
			this._NOMEUSUARIO = NOMEUSUARIO;
			this._SENHAUSUARIO = SENHAUSUARIO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDUSUARIO
		{
			get { return _IDUSUARIO; }
			set { _IDUSUARIO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
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
