using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_BLOQUEIOTELAEntity
	{
		private int? _IDBLOQUEIOTELA;
		private int? _IDUSUARIO;
		private int? _IDFORMULARIO;
		private string _NOMEUSUARIO;
		private string _NOMEFORMULARIO;
		private string _NOMETELA;

		#region Construtores

		//Construtor default
		public LIS_BLOQUEIOTELAEntity() { }

		public LIS_BLOQUEIOTELAEntity(int? IDBLOQUEIOTELA, int? IDUSUARIO, int? IDFORMULARIO, string NOMEUSUARIO, string NOMEFORMULARIO, string NOMETELA)		{

			this._IDBLOQUEIOTELA = IDBLOQUEIOTELA;
			this._IDUSUARIO = IDUSUARIO;
			this._IDFORMULARIO = IDFORMULARIO;
			this._NOMEUSUARIO = NOMEUSUARIO;
			this._NOMEFORMULARIO = NOMEFORMULARIO;
			this._NOMETELA = NOMETELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDBLOQUEIOTELA
		{
			get { return _IDBLOQUEIOTELA; }
			set { _IDBLOQUEIOTELA = value; }
		}

		public int? IDUSUARIO
		{
			get { return _IDUSUARIO; }
			set { _IDUSUARIO = value; }
		}

		public int? IDFORMULARIO
		{
			get { return _IDFORMULARIO; }
			set { _IDFORMULARIO = value; }
		}

		public string NOMEUSUARIO
		{
			get { return _NOMEUSUARIO; }
			set { _NOMEUSUARIO = value; }
		}

		public string NOMEFORMULARIO
		{
			get { return _NOMEFORMULARIO; }
			set { _NOMEFORMULARIO = value; }
		}

		public string NOMETELA
		{
			get { return _NOMETELA; }
			set { _NOMETELA = value; }
		}

		#endregion
	}
}
